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

namespace QuanLyQuanAn.ViewModel.MenuVM
{
    internal class OrderFoodVM : BaseViewModel
    {
        private ObservableCollection<foodCategory> _categoryNames = new ObservableCollection<foodCategory>(CategoryProvider.Category.GetAllCategory());
        private object _allFood = FoodDataprovider.Food.GetAllFood();
        public OrderFoodVM() 
        {
            CategoryNames.Insert(0, new foodCategory(){ name = "Tất cả" });
        }

        public ObservableCollection<foodCategory> CategoryNames { get => _categoryNames; set{ _categoryNames = value; OnPropertyChanged(); } }

        public object AllFood { get => _allFood; set { _allFood = value; OnPropertyChanged(); } }
    }
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

    public class IntToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
