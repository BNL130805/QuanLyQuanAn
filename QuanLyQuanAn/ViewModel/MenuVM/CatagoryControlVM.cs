using MaterialDesignThemes.Wpf;
using QuanLyQuanAn.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows.Input;
using System.Windows.Media;
using QuanLyQuanAn.View.DialogHost;

namespace QuanLyQuanAn.ViewModel.MenuVM
{

    
    internal class CatagoryControlVM : BaseViewModel
    {
        private object _currentDialogContent;
        public object CurrentDialogContent
        {
            get => _currentDialogContent;
            set
            {
                _currentDialogContent = value;
                OnPropertyChanged();
            }
        }

        private object _title="xinchao";

        public ICommand ShowAddCatagoryCommand { get; }
        public ICommand CloseAddCatagory {  get; }
        public ICommand AddCatagory { get; }
        public object Title { get => _title; set => _title = value; }

        public CatagoryControlVM()
        {
            ShowAddCatagoryCommand = new RelayCommand((p)=>ShowAddCatagory(), (p)=>true);
            CloseAddCatagory = new RelayCommand(
                (p) => CloseDialogHost()
                ,
                (p) => true
                );
            AddCatagory = new RelayCommand(
                (p) => 
                {
                    CloseDialogHost();
                },
                (p) => true
                );
        }

        private async void ShowAddCatagory()
        {
            CurrentDialogContent = new AddCatagory(); // DialogContent1 là UserControl hoặc nội dung
            await DialogHost.Show(CurrentDialogContent, "RootDialogHost"); // "RootDialogHost" là tên DialogHost Identifier
        }

        private void CloseDialogHost()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

    }
}
