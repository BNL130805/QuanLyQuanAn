using QuanLyQuanAn.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using static QuanLyQuanAn.Model.CategoryProvider;

namespace QuanLyQuanAn.ViewModel
{
    internal class MenuFoodVM : BaseViewModel
    {
        private ObservableCollection<CategoryName> _categoryNames;
        private ObservableCollection<food> _allFood;
        public MenuFoodVM() 
        {
            CategoryNames = new ObservableCollection<CategoryName>(CategoryProvider.Category.GetAllCategory());
            CategoryNames.Insert(0, new CategoryName(){ Name = "Tất cả" });
            AllFood = new ObservableCollection<food>(FoodDataprovider.Food.GetAllFood());
            
        }

        public ObservableCollection<CategoryName> CategoryNames { get => _categoryNames; set{ _categoryNames = value; OnPropertyChanged(); } }

        public ObservableCollection<food> AllFood { get => _allFood; set { _allFood = value; OnPropertyChanged(); } }
    }

    public class CategoryName
    { public string Name { get; set; } }

    public class ByteToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] byteArray && byteArray.Length > 0)
            {
                BitmapImage bitmap = new BitmapImage();
                using (MemoryStream stream = new MemoryStream(byteArray))
                {
                    stream.Position = 0;
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                }
                return bitmap;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
