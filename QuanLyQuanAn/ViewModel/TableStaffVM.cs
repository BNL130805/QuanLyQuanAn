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
    internal class TableStaffVM:TableControlVM
    {
        private ObservableCollection<ListBillInf> _billInfList;
        private int _totalPrice;
        private int _currentIdTable;

        private object _title = "xinchao";

        public ICommand ShowBillTableCm { get; }
        public ICommand CloseShowBillTable { get; }
        public ICommand PayBill { get; }
        public override ICommand DeleteCommand { get; }
        public object Title { get => _title; set => _title = value; }

        public TableStaffVM()
        {
            ShowBillTableCm = new RelayCommand(
                (p) =>
                {
                    if (p is TableShow table)
                    {
                        _currentIdTable = table.IdTable;
                        TotalPrice = BillDataprovider.Bill.GetBillUnpaidByTable(table).TotalPrice;
                        BillInfList = new ObservableCollection<ListBillInf>(BillInfDataprovider.BillInf.GetBillInfByTable(table.IdTable));
                        ShowAddFood();
                    }
                }, 
                (p)=>
                {
                    if(p is TableShow table)
                    {
                        if (table.Status == "có người")
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
                    LoadTable();
                    CloseDialogHost();
                },
                (p) => true
                );
            LoadTable();
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
