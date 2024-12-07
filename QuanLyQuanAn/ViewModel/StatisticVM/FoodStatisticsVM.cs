using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts.Wpf;
using LiveCharts;
using QuanLyQuanAn.Model;
using System.Collections.ObjectModel;
using LiveCharts.Defaults;

namespace QuanLyQuanAn.ViewModel.StatisticVM
{
    internal class FoodStatisticsVM:BaseViewModel
    {
        private string _typeRevenua;
        private DateTime _begin;
        private DateTime _end;
        private SeriesCollection _seriesStatistic;
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
                        break;
                    case "7 ngày gần đây":
                        Begin = DateTime.Today.AddDays(-7);
                        End = DateTime.Today;
                        break;
                    case "Tháng này":
                        Begin = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                        End = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1);
                        break;
                    case "Năm nay":
                        Begin = new DateTime(DateTime.Today.Year, 1, 1);
                        End = new DateTime(DateTime.Today.Year, 12, 31);
                        break;
                    case "Chọn khoảng thời gian":
                        // Xử lý khi người dùng chọn khoảng thời gian cụ thể (nếu có)
                        break;
                    default:
                        break;
                }

            }
        }

        public FoodStatisticsVM()
        {
        }
        private void ShowStatistic()
        {
            if(Begin <= End)
            {
                var list = BillInfDataprovider.BillInf.GetBillInfByDate(Begin, End.AddDays(1)).Select(p => new PieSeries
                {
                    Title = p.FoodName,
                    Values = new ChartValues<double> { (double)p.Count }, // Bao bọc giá trị Count trong ChartValues
                    DataLabels = true,
                    LabelPoint = chartPoint =>
                    string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation)
                }).ToList<PieSeries>();
                SeriesStatistic = new SeriesCollection();
                foreach(PieSeries a in list)
                {
                    SeriesStatistic.Add(a);
                }    
            }
        }
        public SeriesCollection SeriesStatistic { get => _seriesStatistic; set { _seriesStatistic = value; OnPropertyChanged();} }
        public DateTime Begin { get => _begin; set { _begin = value; OnPropertyChanged(); ShowStatistic(); } }
        public DateTime End { get => _end; set { _end = value; OnPropertyChanged(); ShowStatistic(); } }
    }
}
