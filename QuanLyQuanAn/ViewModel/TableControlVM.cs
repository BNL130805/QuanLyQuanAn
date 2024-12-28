using MaterialDesignThemes.Wpf;
using QuanLyQuanAn.Model;
using QuanLyQuanAn.View.DialogHost;
using QuanLyQuanAn.ViewModel.MenuVM;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace QuanLyQuanAn.ViewModel
{
    public abstract class TableControlVM : BaseViewModel
    {
        private object _currentDialogContent;
        private ObservableCollection<TableShow> _tableList;
        private bool _check;
        private bool _isAllChecked;
        private string _message;
        private TableShow _tableReadyToAdd;
        //thêm
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterTableList(); // Gọi hàm lọc danh sách mỗi khi từ khóa thay đổi
            }
        }
        private ObservableCollection<TableShow> _filteredTableList;
        public ObservableCollection<TableShow> FilteredTableList
        {
            get => _filteredTableList;
            set
            {
                _filteredTableList = value;
                OnPropertyChanged();
            }
        }

        public string TypeAdd { get; set; }
        public object CurrentDialogContent
        {
            get => _currentDialogContent;
            set
            {
                _currentDialogContent = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowAddTableCommand { get; }
        public ICommand CloseAddTable { get; }
        public ICommand AddTable { get; }
        public ICommand DeleteCommand { get; }
        public ICommand AllCheckCm { get; set; }
        public ICommand DeleteTables { get; }
        public ICommand FalseCm { get; set; }
        public ICommand TrueCm { get; set; }
        public ICommand AdjustTable { get; set; }
        public ObservableCollection<TableShow> TableList { get => _tableList; set { _tableList = value; OnPropertyChanged(); } }
        public string Message { get => _message; set { _message = value; OnPropertyChanged(); } }
        public bool IsAllChecked { get => _isAllChecked; set { _isAllChecked = value; OnPropertyChanged(); } }
        public bool Check { get => _check; set { _check = value; OnPropertyChanged(); } }

        public TableShow TableReadyToAdd {
            get 
            {
                if (_tableReadyToAdd == null)
                {
                    _tableReadyToAdd = new TableShow();
                }
                return _tableReadyToAdd;
            } 
            set { _tableReadyToAdd = value; OnPropertyChanged(); } }

        public TableControlVM()
        {
            ShowAddTableCommand = new RelayCommand(
                async (p) =>
                {
                    TypeAdd = "Thêm";
                    CurrentDialogContent = new AddTable(); // DialogContent1 là UserControl hoặc nội dung
                    await ShowDialogContent();
                },
                (p) => true);
            CloseAddTable = new RelayCommand(
                (p) =>
                {
                    CloseDialogHost();
                },
                (p) => true
                );
            AddTable = new RelayCommand(
                async (p) =>
                {
                    if (!TableProvider.Table.AddTable(TableReadyToAdd))
                    {
                        var addCatagory = CurrentDialogContent;
                        Message = $"Đã có bàn {TableReadyToAdd.Name}!";
                        CurrentDialogContent = new Message();
                        CloseDialogHost();
                        await ShowDialogContent();
                        CurrentDialogContent = addCatagory;
                        await ShowDialogContent();
                    }
                    else
                    {
                        if (TableReadyToAdd.IdTable == 0)
                        {
                            Message = $"Thêm thành công {TableReadyToAdd.Name}!";
                        }
                        else
                        {
                            Message = $"Đã sửa bàn {TableReadyToAdd.Name}";
                        }
                        CurrentDialogContent = new Message();
                        CloseDialogHost();
                        await ShowDialogContent();
                        LoadTable();
                        TableReadyToAdd = null;
                    }
                },
                (p) => (TableReadyToAdd.Name != null && TableReadyToAdd.Name != "")
                );
            FalseCm = new RelayCommand(
                p =>
                {
                    Check = false;
                    CloseDialogHost();
                },
                p => true
                );
            TrueCm = new RelayCommand(
                p =>
                {
                    Check = true;
                    CloseDialogHost();
                },
                p => true
                );
            // Khởi tạo lệnh xóa
            DeleteCommand = new RelayCommand(
                async (selectedTable) =>
                {
                    if (selectedTable is TableShow table)
                    {
                        Message = $"Bạn có chắc chắn muốn xóa bàn {table.Name} không?";
                        CurrentDialogContent = new MessageYesNo();
                        await ShowDialogContent();

                        if (Check)
                        {
                            TableProvider.Table.DeleteTable(table.IdTable);
                            LoadTable();
                        }
                    }
                },
                (selectedTable) => true);
            DeleteTables = new RelayCommand(
                async(p)=>
                {
                    var DeleteList = TableList.Where(l => l.IsChecked == true).ToList();
                    Message = $"Bạn có chắc chắn muốn xóa {DeleteList.Count} bàn này không?";
                    CurrentDialogContent = new MessageYesNo();
                    await ShowDialogContent();
                    if (Check == true)
                    {
                        foreach (var table in DeleteList)
                        {
                            TableProvider.Table.DeleteTable(table.IdTable);
                        }
                        LoadTable();
                        IsAllChecked = false;
                    }
                }
                ,
                p=> TableList.Any(t=> t.IsChecked));
            AllCheckCm = new RelayCommand(
                p =>
                {
                    if (IsAllChecked == true)
                    {
                        foreach (var table in FilteredTableList)
                        {
                            table.IsChecked = true;
                        }
                    }
                    else
                    {
                        foreach (var table in FilteredTableList)
                        {
                            table.IsChecked = false;
                        }
                    }
                },
                p => true
                );
            AdjustTable = new RelayCommand(
                async(p)=>
                {
                    if (p is TableShow table)
                    {
                        TableReadyToAdd = table;
                        TypeAdd = "Sửa";
                        CurrentDialogContent = new AddTable();
                        await ShowDialogContent();
                    }
                }
                ,
                p=>true);
            LoadTable();

        }
        private async Task ShowDialogContent()
        {
            await DialogHost.Show(CurrentDialogContent, "RootDialogHost"); // "RootDialogHost" là tên DialogHost Identifier
        }

        private void CloseDialogHost()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        private void LoadTable()
        {
            TableList = new ObservableCollection<TableShow>(TableProvider.Table.GetAllTable().Select(
                P =>
                new TableShow
                {
                    IdTable = P.idTable,
                    Name = P.tableName,
                    IsChecked = false,
                    Status = P.status
                }));
            //thêm
            FilteredTableList = new ObservableCollection<TableShow>(TableList);

            for (int i = 0; i < TableList.Count; i++) {
                TableList[i].No = i + 1;
                TableList[i].CountChecked += ConfirmCheckAll;
            }
        }
        private void ConfirmCheckAll()
        {
            IsAllChecked = !FilteredTableList.Any(p => p.IsChecked == false);
        }
        //thêm
        private void FilterTableList()
        {
            LoadTable();
            IsAllChecked = false;
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                // Nếu không có từ khóa, hiển thị toàn bộ danh sách
                FilteredTableList = new ObservableCollection<TableShow>(TableList);
            }
            else
            {
                // Lọc danh sách dựa trên từ khóa tìm kiếm
                FilteredTableList = new ObservableCollection<TableShow>(
                    TableList.Where(t => t.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0));
            }
        }

    }
    public class TableShow:BaseViewModel
    {
        private int _no;
        private int _idTable;
        private bool _isChecked;
        private string _name;
        private string _status;
        public int No { get => _no; set { _no = value; OnPropertyChanged(); } }
        public int IdTable { get => _idTable; set { _idTable = value; OnPropertyChanged(); } }
        public bool IsChecked { get => _isChecked; set { _isChecked = value; OnPropertyChanged(); CountChecked?.Invoke(); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public string Status { get => _status; set { _status = value; OnPropertyChanged(); } }

        public event Action CountChecked;
    }
}
