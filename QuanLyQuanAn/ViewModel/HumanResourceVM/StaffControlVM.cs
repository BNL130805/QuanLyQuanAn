using QuanLyQuanAn.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn.ViewModel.HumanResourceVM
{
    internal class StaffControlVM : BaseViewModel
    {
        private object _staffList = HumanResouceDataProvider.Human.GetHuman("Nhân viên");

        public StaffControlVM()
        {

        }

        public object StaffList { get => _staffList; set { _staffList = value; OnPropertyChanged(); } }
    }
}
