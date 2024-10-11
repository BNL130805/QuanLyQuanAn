using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyQuanAn.View.Statistics
{
    /// <summary>
    /// Interaction logic for FoodStatistics.xaml
    /// </summary>
    public partial class FoodStatistics : UserControl
    {
        public FoodStatistics()
        {
            InitializeComponent();
            foodPieChart.Series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Phở",
                    Values = new ChartValues<double> { 50 }, // Số lượng món Phở
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Bún bò",
                    Values = new ChartValues<double> { 40 }, // Số lượng món Bún bò
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Cơm tấm",
                    Values = new ChartValues<double> { 30 }, // Số lượng món Cơm tấm
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Hủ tiếu",
                    Values = new ChartValues<double> { 20 }, // Số lượng món Hủ tiếu
                    DataLabels = true
                }
            };

            // Tùy chỉnh hiển thị nhãn trên từng phần của biểu đồ tròn
            foreach (var series in foodPieChart.Series)
            {
                (series as PieSeries).LabelPoint = chartPoint =>
                    string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            }
        }
    }
}
