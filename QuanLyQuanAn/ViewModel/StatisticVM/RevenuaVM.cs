using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyQuanAn.Model;
using LiveCharts;
using System.Windows.Data;
using System.Globalization;
using System.Diagnostics.Eventing.Reader;
using System.Windows;

namespace QuanLyQuanAn.ViewModel.StatisticVM
{
    internal class RevenuaVM:BaseViewModel
    {
        private string _typeRevenua="Hôm nay" ;
        private object _timeLable;
        private DateTime _begin;
        private DateTime _end;
        private Visibility _showdate;
        public Func<double, string> Formatter { get; set; }
        private ChartValues<double> _revenueData;

        public RevenuaVM()
        {
            // Khởi tạo Formatter để định dạng số tiền
            Formatter = value => value.ToString("N0");
            TypeRevenua = "Hôm nay";
        }

        public string TypeRevenua
        {
            get => _typeRevenua;
            set
            {
                _typeRevenua = value;
                OnPropertyChanged();
                switch (_typeRevenua)
                {
                    case "Hôm nay":
                        Begin = DateTime.Today;
                        End = DateTime.Today;
                        Showdate = Visibility.Hidden;
                        break;
                    case "7 ngày gần đây":
                        Begin = DateTime.Today.AddDays(-7);
                        End = DateTime.Today;
                        Showdate = Visibility.Hidden;
                        break;
                    case "Tháng này":
                        Begin = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                        End = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1);
                        Showdate = Visibility.Hidden;
                        break;
                    case "Năm nay":
                        Begin = new DateTime(DateTime.Today.Year, 1, 1);
                        End = new DateTime(DateTime.Today.Year, 12, 31);             
                        Showdate = Visibility.Hidden;
                        break;
                    case "Chọn khoảng thời gian":
                        // Xử lý khi người dùng chọn khoảng thời gian cụ thể (nếu có)
                        Showdate = Visibility.Visible;
                        break;
                    default:
                        break;
                }
                
            }
        }
        public object TimeLable
        {
            get => _timeLable;
            set
            {
                _timeLable = value;
                OnPropertyChanged();
            }
        }

        public ChartValues<double>  RevenueData
        {
            get => _revenueData;
            set
            {
                _revenueData = value;
                OnPropertyChanged();
            }
        }
        private void ShowRevenua()
        {
            int PeriodOfTime = (End.AddDays(1) - Begin).Days;
            var Bills = (BillDataprovider.Bill.GetBillByDate(Begin, End.AddDays(1)) as IEnumerable<dynamic>)?
                        .Select(b => (ThoiGian: b.ThoiGian, TotalPrice: (double)b.TongDoanhThu));
            if (PeriodOfTime <= 2)
            {
                TimeLable = Bills?.Select(d => $"{(int)d.ThoiGian}:00").ToList();
            }
            else if (PeriodOfTime > 2 && PeriodOfTime <= 60)
            {
                TimeLable = Bills?.Select(d => ((DateTime)d.ThoiGian).ToShortDateString()).ToList();
            }
            else if (PeriodOfTime > 60 && PeriodOfTime <= 730)
            {
                TimeLable = Bills?.Select(d => ((int)d.ThoiGian).ToString()).ToList();
            }
            else
            {
                TimeLable = Bills?.Select(d => ((int)d.ThoiGian).ToString()).ToList();
            }
            RevenueData = new ChartValues<double>(Bills?.Select(d => d.TotalPrice) ?? Enumerable.Empty<double>());
        }

        public DateTime Begin { get => _begin; set { _begin = value; OnPropertyChanged(); ShowRevenua(); } }
        public DateTime End { get => _end; set { _end = value; OnPropertyChanged(); ShowRevenua(); } }
        public Visibility Showdate { get => _showdate; set { _showdate = value; OnPropertyChanged(); } }
    }
    public class WidthOfDatePickerAcrossGrid : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value*2/5;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
