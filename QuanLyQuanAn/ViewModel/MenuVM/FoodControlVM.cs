using MaterialDesignThemes.Wpf;
using QuanLyQuanAn.Model;
using QuanLyQuanAn.View.DialogHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyQuanAn.ViewModel.MenuVM
{
    internal class FoodControlVM : BaseViewModel
    {
        private object _currentDiaLogContent;

        private List<food> _foodList;

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
        public object Title { get => _title; set => _title = value; }
        public List<food> FoodList { get => _foodList; set { _foodList = value; OnPropertyChanged(); } }

        public FoodControlVM()
        {
            ShowAddFoodCommand = new RelayCommand((p) => ShowAddFood(), (p) => true);
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
            FoodList = FoodDataprovider.Food.GetAllFood();
        }

        private async void ShowAddFood()
        {
            CurrentDialogContent = new AddFood(); // DialogContent1 là UserControl hoặc nội dung
            await DialogHost.Show(CurrentDialogContent, "RootDialogHost"); // "RootDialogHost" là tên DialogHost Identifier
        }

        private void CloseDialogHost()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

    }
}
