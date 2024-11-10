using MaterialDesignThemes.Wpf;
using QuanLyQuanAn.Model;
using QuanLyQuanAn.View.DialogHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private object _title = "xinchao";

        public ICommand ShowAddTableCommand { get; }
        public ICommand CloseAddTable { get; }
        public ICommand AddTable { get; }
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
