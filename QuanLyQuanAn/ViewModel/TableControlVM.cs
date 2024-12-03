using MaterialDesignThemes.Wpf;
using QuanLyQuanAn.Model;
using QuanLyQuanAn.View.DialogHost;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyQuanAn.ViewModel
{
    internal class TableControlVM : BaseViewModel
    {
        private object _currentDialogContent;

        private object _tableList;

        public object CurrentDialogContent
        {
            get => _currentDialogContent;
            set
            {
                _currentDialogContent = value;
                OnPropertyChanged();
            }
        }
        //
        private ObservableCollection<tableFood> _selectedTables = new ObservableCollection<tableFood>();
        public ObservableCollection<tableFood> SelectedTables
        {
            get => _selectedTables;
            set
            {
                _selectedTables = value;
                OnPropertyChanged();
            }
        }
        private object _title = "xinchao";

        public ICommand ShowAddTableCommand { get; }
        public ICommand CloseAddTable { get; }
        public ICommand AddTable { get; }
        public ICommand DeleteCommand { get; }
        public object Title { get => _title; set => _title = value; }
        public object TableList { get => _tableList; set { _tableList = value; OnPropertyChanged(); } }

        public TableControlVM()
        {
            ShowAddTableCommand = new RelayCommand((p) => ShowAddFood(), (p) => true);
            CloseAddTable = new RelayCommand(
                (p) => CloseDialogHost()
                ,
                (p) => true
                );
            AddTable = new RelayCommand(
                (p) =>
                {
                    CloseDialogHost();
                },
                (p) => true
                );
            TableList = TableProvider.Table.GetAllTable();

            TableList = new ObservableCollection<tableFood>(TableProvider.Table.GetAllTable());

            // Khởi tạo lệnh xóa
            DeleteCommand = new RelayCommand(
                (selectedTable) =>
                {
                    if (selectedTable is tableFood table)
                    {
                        var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa bàn ăn {table.tableName} không?",
                                                    "Xác nhận",
                                                    MessageBoxButton.YesNo,
                                                    MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            TableProvider.Table.DeleteTable(table.idTable);
                            ((ObservableCollection<tableFood>)TableList).Remove(table);
                        }
                    }
                },
                (selectedTable) => true);
            DeleteCommand = new RelayCommand(
                (p) =>
                {
                    if (SelectedTables.Any())
                    {
                        var result = MessageBox.Show(
                            $"Bạn có chắc chắn muốn xóa {SelectedTables.Count} bàn đã chọn không?",
                            "Xác nhận",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            foreach (var table in SelectedTables.ToList())
                            {
                                TableProvider.Table.DeleteTable(table.idTable);
                                ((ObservableCollection<tableFood>)TableList).Remove(table);
                            }

                            // Xóa danh sách bàn đã chọn sau khi xóa thành công
                            SelectedTables.Clear();
                        }
                    }
                else
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một bàn để xóa!", "Thông báo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                }
        },
        (p) => true
    );

        }
        private async void ShowAddFood()
        {
            CurrentDialogContent = new AddTable(); // DialogContent1 là UserControl hoặc nội dung
            await DialogHost.Show(CurrentDialogContent, "RootDialogHost"); // "RootDialogHost" là tên DialogHost Identifier
        }

        private void CloseDialogHost()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

    }
}
