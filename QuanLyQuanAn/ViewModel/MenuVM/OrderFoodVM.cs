using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using QuanLyQuanAn.Model;
using QuanLyQuanAn.View.DialogHost;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Linq;
using static QuanLyQuanAn.Model.CategoryProvider;

namespace QuanLyQuanAn.ViewModel.MenuVM
{
    internal class OrderFoodVM : BaseViewModel
    {
        private ObservableCollection<foodCategory> _categoryNames = new ObservableCollection<foodCategory>(CategoryProvider.Category.GetAllCategory());
        private object _allFood;
        private ObservableCollection<ListBillInf> _billInfList;
        int _totalFood;
        private object _currentDialogContent;
        public ICommand AddBill { get; set; }
        public ICommand AddFoodCm { get; set; }
        public ICommand SendBillToChef { get; set; }
        public ICommand CloseAddBill { get; set; }
        public ICommand ClearBill { get; set; }
        public OrderFoodVM()
        {
            CategoryNames.Insert(0, new foodCategory() { name = "Tất cả" });
            AllFood = FoodDataprovider.Food.GetAllFood();
            SendBillToChef = new RelayCommand(
                (p)=>CloseDialogHost(),
                (p)=>true
                );
            ClearBill = new RelayCommand(
                (p)=>
                {
                    BillInfList?.Clear();
                    TotalFood = 0;
                },
                (p)=>true
                );
            CloseAddBill = new RelayCommand(
                (p) => CloseDialogHost(),
                (p)=>true
                );
            AddBill = new RelayCommand((p) =>
            ShowAddFood(),
            (p)=>
            {
                if (BillInfList != null && BillInfList?.Count > 0)
                    return true;
                else return false;
            });
            AddFoodCm = new RelayCommand(
                (p) =>
                {
                    var Food = p as dynamic;
                    if (BillInfList == null)
                    {
                        BillInfList = new ObservableCollection<ListBillInf>();
                    }
                    var existingBillInf = BillInfList.FirstOrDefault(ex => ex.IdFood == Food.idFood);
                    if(existingBillInf != null)
                    {
                        existingBillInf.Count++;
                    }
                    else
                    {
                        var newBillinf = new ListBillInf
                        {
                            No = BillInfList.Count +1,
                            Name = Food.name,
                            IdFood = Food.idFood,
                            FoodImage = Food.FoodImage,
                            Price = Food.price
                        };
                        newBillinf.CoutZeroed += (b) =>
                        {  
                            BillInfList.Remove(b);
                            for (int i=0; i<BillInfList.Count; i++)
                            {
                                BillInfList[i].No = i + 1;
                            }
                        };
                        newBillinf.Pay += (b) =>
                        {
                            TotalFood = 0;
                            foreach(var Bill in BillInfList)
                            {
                                TotalFood += (int)Bill.Count * (int)Bill.Price;
                            }
                        };
                        BillInfList.Add(newBillinf);
                        newBillinf.Count = 1;
                    }
                },
                (p) => { return true; }
                );
        }
        private async void ShowAddFood()
        {
            CurrentDialogContent = new ChooseTable(); // DialogContent1 là UserControl hoặc nội dung
            await DialogHost.Show(CurrentDialogContent, "RootDialogHost"); // "RootDialogHost" là tên DialogHost Identifier
        }
        private void CloseDialogHost()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        public ObservableCollection<foodCategory> CategoryNames { get => _categoryNames; set{ _categoryNames = value; OnPropertyChanged(); } }
        public object AllFood { get => _allFood; set { _allFood = value; OnPropertyChanged(); } }
        public ObservableCollection<ListBillInf> BillInfList { get => _billInfList;
            set 
            {
                _billInfList = value; 
                OnPropertyChanged();

            } }

        public int TotalFood { get => _totalFood; set { _totalFood = value; OnPropertyChanged(); } }

        public object CurrentDialogContent { get => _currentDialogContent; set { _currentDialogContent = value; OnPropertyChanged(); } }
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
            try
            {
                return int.Parse(value.ToString());
            }
            catch
            {
                return 0;
            }
        }
    }
    public class ListBillInf:BaseViewModel
    {
        private int _count;
        private int _no;
        public int IdFood { get; set; }
        public string Name { get; set; }
        public byte[] FoodImage { get; set; }
        public decimal Price { get; set; }
        public event Action<ListBillInf> CoutZeroed;
        public event Action<ListBillInf> Pay;
        public int Count { get => _count; 
            set 
            {
                _count = value; OnPropertyChanged();
                Pay.Invoke(this);
                if(_count==0)
                {
                    CoutZeroed.Invoke(this);
                }
            } }

        public int No { get => _no; set { _no = value;OnPropertyChanged(); } }
    }


}
