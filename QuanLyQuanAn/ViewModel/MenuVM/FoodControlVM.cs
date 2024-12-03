using MaterialDesignThemes.Wpf;
using QuanLyQuanAn.Model;
using QuanLyQuanAn.View.DialogHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;



namespace QuanLyQuanAn.ViewModel.MenuVM
{
    internal class FoodControlVM : BaseViewModel
    {
        private object _catagoryList;
        private object _currentDiaLogContent;
        private string _message;
        private object _foodList;
        private bool _check;
        public object CurrentDialogContent
        {
            get => _currentDiaLogContent;
            set
            {
                _currentDiaLogContent = value;
                OnPropertyChanged();
            }
        }

        private object _title = "xinchao";

        public ICommand ShowAddFoodCommand { get; }
        public ICommand CloseAddFood { get; }
        public ICommand AddFood { get; }
        public ICommand DeleteCommand { get; }
        public ICommand FalseCm { get; set; }
        public ICommand TrueCm { get; set; }

        public object Title { get => _title; set => _title = value; }
        public object FoodList { get => _foodList; set { _foodList = value; OnPropertyChanged(); } }

        public object CatagoryList { get => _catagoryList; set { _catagoryList = value; OnPropertyChanged(); } }

        public string Message { get => _message; set { _message = value;OnPropertyChanged(); } }

        public bool Check { get => _check; set { _check = value;OnPropertyChanged(); } }

        public FoodControlVM()
        {
            ShowAddFoodCommand = new RelayCommand(async (p)=> { CurrentDialogContent = new AddFood(); await ShowDialogContent(); }, (p) => true);
            CloseAddFood = new RelayCommand(
                (p) => CloseDialogHost()
                ,
                (p) => true
                );
            AddFood = new RelayCommand(
                (p) =>
                {
                    CloseDialogHost();
                },
                (p) => true
                );
            DeleteCommand = new RelayCommand(
                (selectedFood) => DeleteFood(selectedFood),
                (selectedFood) => true // Chỉ kích hoạt nếu món ăn được chọn
            );
            FalseCm = new RelayCommand(
                p =>
                {
                    Check = false;
                    CloseDialogHost();
                },
                p => true
                );
            TrueCm = new RelayCommand(
                p =>
                {
                    Check = true;
                    CloseDialogHost();
                },
                p => true
                );
            FoodList = FoodDataprovider.Food.GetAllFood();
            CatagoryList = CategoryProvider.Category.GetAllCategory();

        }

        private async Task ShowDialogContent()
        {
             // DialogContent1 là UserControl hoặc nội dung
            await DialogHost.Show(CurrentDialogContent, "RootDialogHost"); // "RootDialogHost" là tên DialogHost Identifier
        }

        private void CloseDialogHost()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private async void DeleteFood(Object selectedFood)
        {
            dynamic food = selectedFood as dynamic;
            if (food != null)
            {
                // Hiển thị hộp thoại xác nhận
                Message = $"Bạn có chắc chắn muốn xóa món {food.name} không?";
                CurrentDialogContent = new MessageYesNo();
                await ShowDialogContent();

                if (Check)
                {
                    // Gọi phương thức xóa trong DataProvider
                    FoodDataprovider.Food.DeleteFood(food.idFood);

                    // Cập nhật lại danh sách món ăn
                    FoodList = FoodDataprovider.Food.GetAllFood();
                }
            }
        }

    }
}
