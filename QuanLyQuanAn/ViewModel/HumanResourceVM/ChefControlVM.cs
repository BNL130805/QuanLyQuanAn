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
            DeleteChefCommand = new RelayCommand(
                (p) =>
                {
                    HumanResouceDataProvider.Human.DeleteHuman(p.ToString(), "Đầu bếp");

                    // Cập nhật lại danh sách sau khi xóa
                    ChefList = HumanResouceDataProvider.Human.GetHuman("Đầu bếp");
                }  
                , 
                p=>true
                );
        }

        public object ChefList { get => _chefList; set { _chefList = value; OnPropertyChanged(); } }

        public ICommand DeleteChefCommand { get; set; }

        private void DeleteChef(string username)
        {
            // Xóa đầu bếp từ cơ sở dữ liệu
            HumanResouceDataProvider.Human.DeleteHuman(username, "Đầu bếp");

            // Cập nhật lại danh sách sau khi xóa
            ChefList = HumanResouceDataProvider.Human.GetHuman("Đầu bếp");
        }
    }
}
