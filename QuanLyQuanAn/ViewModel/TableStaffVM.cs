using MaterialDesignThemes.Wpf;
using QuanLyQuanAn.Model;
using QuanLyQuanAn.View.DialogHost;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using QuanLyQuanAn.ViewModel.MenuVM;

namespace QuanLyQuanAn.ViewModel
{
    internal class TableStaffVM:BaseViewModel
    {
        private ObservableCollection<ListBillInf> _billInfList;
        private object _currentDialogContent;
        private object _tableList;
        private int _totalPrice;
        private int _currentIdTable;
        public object CurrentDialogContent
        {
            get => _currentDialogContent;
            set
            {
                _currentDialogContent = value;
                OnPropertyChanged();
            }
        }

        private object _title = "xinchao";

        public ICommand ShowBillTableCm { get; }
        public ICommand CloseShowBillTable { get; }
        public ICommand PayBill { get; }
        public ICommand DeleteCommand { get; }
        public object Title { get => _title; set => _title = value; }
        public object TableList { get => _tableList; set { _tableList = value; OnPropertyChanged(); } }

        public TableStaffVM()
        {
            ShowBillTableCm = new RelayCommand(
                (p) =>
                {
                    if (p is tableFood table)
                    {
                        _currentIdTable = table.idTable;
                        TotalPrice = BillDataprovider.Bill.GetBillUnpaidByTable(table).TotalPrice;
                        BillInfList = new ObservableCollection<ListBillInf>(BillInfDataprovider.BillInf.GetBillInfByTable(table.idTable));
                        ShowAddFood();
                    }
                }, 
                (p)=>
                {
                    if(p is tableFood table)
                    {
                        if (table.status == "có người")
                            return true;
                    }
                    return false;
                });
            CloseShowBillTable = new RelayCommand(
                (p) => CloseDialogHost()
                ,
                (p) => true
                );
            PayBill = new RelayCommand(
                (p) =>
                {
                    BillDataprovider.Bill.PayBillByIdTable(_currentIdTable);
                    TableList = TableProvider.Table.GetAllTable();
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
        }
        private async void ShowAddFood()
        {
            CurrentDialogContent = new ListBillInfShow(); // DialogContent1 là UserControl hoặc nội dung
            await DialogHost.Show(CurrentDialogContent, "RootDialogHost"); // "RootDialogHost" là tên DialogHost Identifier
        }

        private void CloseDialogHost()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        public ObservableCollection<ListBillInf> BillInfList
        {
            get => _billInfList;
            set
            {
                _billInfList = value;
                OnPropertyChanged();

            }
        }

        public int TotalPrice { get => _totalPrice; set { _totalPrice = value;OnPropertyChanged(); } }
    }
}
