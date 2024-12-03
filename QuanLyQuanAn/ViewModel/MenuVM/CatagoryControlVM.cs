using MaterialDesignThemes.Wpf;
using QuanLyQuanAn.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows.Input;
using System.Windows.Media;
using QuanLyQuanAn.View.DialogHost;
using QuanLyQuanAn.Model;
using System.Windows;

namespace QuanLyQuanAn.ViewModel.MenuVM
{

    
    internal class CatagoryControlVM : BaseViewModel
    {
        private object _currentDialogContent;
        private object _categoryList;
        private string _message;
        private bool _check;
        public object CurrentDialogContent
        {
            get => _currentDialogContent;
            set
            {
                _currentDialogContent = value;
                OnPropertyChanged();
            }
        }
        public ICommand ShowAddCatagoryCommand { get; }
        public ICommand CloseAddCatagory {  get; }
        public ICommand AddCatagory { get; }
        public ICommand DeleteCommand { get; }
        public ICommand FalseCm { get; set; }
        public ICommand TrueCm { get; set; }

        public object CategoryList { get => _categoryList; set { _categoryList = value; OnPropertyChanged(); } }

        public string Message { get => _message; set { _message = value; OnPropertyChanged(); } }

        public bool Check { get => _check; set { _check = value; OnPropertyChanged(); } }

        public CatagoryControlVM()
        {
            ShowAddCatagoryCommand = new RelayCommand(
                async (p)=>
                {
                    CurrentDialogContent = new AddCatagory();
                    await ShowDialogContent(); 
                }, 
                (p)=>true);
            CloseAddCatagory = new RelayCommand(
                (p) => CloseDialogHost()
                ,
                (p) => true
                );
            AddCatagory = new RelayCommand(
                (p) => 
                {
                    CloseDialogHost();
                },
                (p) => true
                );
            FalseCm = new RelayCommand(
                p=>
                {
                    Check = false;
                    CloseDialogHost();
                },
                p=> true
                );
            TrueCm = new RelayCommand(
                p =>
                {
                    Check = true;
                    CloseDialogHost();
                },
                p => true
                );
            CategoryList = CategoryProvider.Category.GetAllCategory();

            CategoryList = new ObservableCollection<foodCategory>(CategoryProvider.Category.GetAllCategory());

            // Khởi tạo lệnh xóa
            DeleteCommand = new RelayCommand(
                async (selectedCategory) =>
                {
                    if (selectedCategory is foodCategory category)
                    {
                        Message = $"Bạn có chắc chắn muốn xóa danh mục {category.name} không?";
                        CurrentDialogContent = new MessageYesNo();
                        await ShowDialogContent() ;
                        if (Check == true)
                        {
                            if(!CategoryProvider.Category.DeleteCategory(category.idFoodCtg))
                            {
                                CloseDialogHost();
                                Message = $"Bạn phải xóa hết món ăn có danh mục {category.name}!";
                                CurrentDialogContent = new Message();
                                await ShowDialogContent();
                            } 
                            else
                            {     
                               ((ObservableCollection<foodCategory>)CategoryList).Remove(category);
                            }
                        }
                    }
                },
                (selectedCategory) => true);

        }

        private async Task ShowDialogContent()
        {
            await DialogHost.Show(CurrentDialogContent, "RootDialogHost"); // "RootDialogHost" là tên DialogHost Identifier
        }

        private void CloseDialogHost()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

    }
}
