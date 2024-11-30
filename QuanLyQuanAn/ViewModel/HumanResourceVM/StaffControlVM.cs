using QuanLyQuanAn.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;



namespace QuanLyQuanAn.ViewModel.HumanResourceVM
{
    internal class StaffControlVM : BaseViewModel
    {
        private object _staffList = HumanResouceDataProvider.Human.GetHuman("Nhân viên");

        public object StaffList { 
            get => _staffList; 
            set { 
                _staffList = value; 
                OnPropertyChanged(); 
            } 
        }
        public ICommand DeleteCommand { get; set; }

        public StaffControlVM()
        {
            //Lấy danh sách nhân viên từ cơ sở dữ liệu
            StaffList = new ObservableCollection<Account>(HumanResouceDataProvider.Human.GetHuman("Nhân viên"));

            // Khởi tạo lệnh xóa
            DeleteCommand = new RelayCommand(
                (selectedStaff) =>
                {
                    if (selectedStaff is Account staff)
                    {
                        var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên {staff.Username} không?",
                                                    "Xác nhận",
                                                    MessageBoxButton.YesNo,
                                                    MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            HumanResouceDataProvider.Human.DeleteHuman(staff.Username, "Nhân viên");
                            ((ObservableCollection<Account>)StaffList).Remove(staff);
                        }
                    }
                }, 
                (selectedStaff) => true);

        }

    }
}
