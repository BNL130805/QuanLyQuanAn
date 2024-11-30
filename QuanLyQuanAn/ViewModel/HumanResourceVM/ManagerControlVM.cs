using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using QuanLyQuanAn.Model;

namespace QuanLyQuanAn.ViewModel.HumanResourceVM
{
    internal class ManagerControlVM:BaseViewModel
    {
        private object _managerList = HumanResouceDataProvider.Human.GetHuman("Quản lí");

        public object ManagerList { get => _managerList; set { _managerList = value; OnPropertyChanged(); } }

        
        
        public ICommand DeleteCommand { get; set; }

        public ManagerControlVM()
        {
            //Lấy danh sách nhân viên từ cơ sở dữ liệu
            ManagerList = new ObservableCollection<Account>(HumanResouceDataProvider.Human.GetHuman("Quản lý"));

            // Khởi tạo lệnh xóa
            DeleteCommand = new RelayCommand(
                (selectedManager) =>
                {
                    if (selectedManager is Account manager)
                    {
                        var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa Quản lý {manager.Username} không?",
                                                    "Xác nhận",
                                                    MessageBoxButton.YesNo,
                                                    MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            HumanResouceDataProvider.Human.DeleteHuman(manager.Username, "Quản lý");
                            ((ObservableCollection<Account>)ManagerList).Remove(manager);
                        }
                    }
                },
                (selectedManager) => true);
        }
    }   

}
