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

namespace QuanLyQuanAn.ViewModel.HumanResourceVM
{
    internal class ChefControlVM : BaseViewModel
    {
        private object _chefList = HumanResouceDataProvider.Human.GetHuman("Đầu bếp");

        public ChefControlVM()
        {

        }

        public object ChefList { get => _chefList; set { _chefList = value; OnPropertyChanged(); } }
    }
}
