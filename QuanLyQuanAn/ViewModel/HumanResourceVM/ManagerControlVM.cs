using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyQuanAn.Model;

namespace QuanLyQuanAn.ViewModel.HumanResourceVM
{
    internal class ManagerControlVM:BaseViewModel
    {
        private object _managerList = HumanResouceDataProvider.Human.GetHuman("Quản lý");

        public ManagerControlVM()
        {
           
        }

        public object ManagerList { get => _managerList; set { _managerList = value; OnPropertyChanged(); } }
    }  
}
