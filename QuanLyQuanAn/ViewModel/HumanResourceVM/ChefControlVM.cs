using MaterialDesignThemes.Wpf;
using QuanLyQuanAn.Model;
using QuanLyQuanAn.View.DialogHost;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyQuanAn.ViewModel.HumanResourceVM
{
    internal class ChefControlVM : BaseViewModel
    {
        private object _chefList = HumanResouceDataProvider.Human.GetHuman("Đầu bếp");


        public object ChefList { get => _chefList; set { _chefList = value; OnPropertyChanged(); } }

        public ICommand DeleteCommand { get; set; }

        public ChefControlVM()
        {
            //Lấy danh sách nhân viên từ cơ sở dữ liệu
            ChefList = new ObservableCollection<Account>(HumanResouceDataProvider.Human.GetHuman("Đầu bếp"));

            // Khởi tạo lệnh xóa
            DeleteCommand = new RelayCommand(
                (selectedChef) =>
                {
                    if (selectedChef is Account chef)
                    {
                        var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa đầu bếp {chef.Username} không?",
                                                    "Xác nhận",
                                                    MessageBoxButton.YesNo,
                                                    MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            HumanResouceDataProvider.Human.DeleteHuman(chef.Username, "Đầu bếp");
                            ((ObservableCollection<Account>)ChefList).Remove(chef);
                        }
                    }
                },
                (selectedChef) => true);

        }
    }
}
