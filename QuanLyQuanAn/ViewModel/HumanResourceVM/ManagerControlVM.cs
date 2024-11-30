using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuanLyQuanAn.Model;

namespace QuanLyQuanAn.ViewModel.HumanResourceVM
{
    internal class ManagerControlVM:BaseViewModel
    {
        private object _managerList = HumanResouceDataProvider.Human.GetHuman("Quản lí");

        public ManagerControlVM()
        {
            DeleteManagerCommand = new RelayCommand(
                (p) =>
                {
                    HumanResouceDataProvider.Human.DeleteHuman(p.ToString(), "Quản lí");

                    // Cập nhật lại danh sách sau khi xóa
                    ManagerList = HumanResouceDataProvider.Human.GetHuman("Quản lí");
                }
                ,
                p => true
                );
        }

        public object ManagerList { get => _managerList; set { _managerList = value; OnPropertyChanged(); } }
        
        public ICommand DeleteManagerCommand { get; set; }

        private void DeleteManager(string username)
        {
            // Xóa đầu bếp từ cơ sở dữ liệu
            HumanResouceDataProvider.Human.DeleteHuman(username, "Quản lí");

            // Cập nhật lại danh sách sau khi xóa
            ManagerList = HumanResouceDataProvider.Human.GetHuman("Quản lí");
        }
    }   

}
