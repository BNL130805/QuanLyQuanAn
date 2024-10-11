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
using LiveCharts.Wpf;

namespace QuanLyQuanAn.View.Statistics
{
    /// <summary>
    /// Interaction logic for Revenue.xaml
    /// </summary>
    public partial class Revenue : UserControl
    {
        public ChartValues<double> RevenueValues { get; set; }
        public List<string> Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public Revenue()
        {
            InitializeComponent();
            LoadRevenueData("Giờ");

            // Định dạng giá trị trục Y
            Formatter = value => value.ToString("C");

            DataContext = this;
        }
        private void LoadRevenueData(string timePeriod)
        {
            // Làm sạch các giá trị cũ
            RevenueValues = new ChartValues<double>();
            Labels = new List<string>();

            // Giả sử dữ liệu doanh thu cho các khoảng thời gian khác nhau
            switch (timePeriod)
            {
                case "Giờ":
                    RevenueValues.AddRange(new double[] { 100, 200, 150, 400, 500 }); // Ví dụ
                    Labels.AddRange(new[] { "1 AM", "2 AM", "3 AM", "4 AM", "5 AM" }); // Ví dụ
                    break;

                case "Ngày":
                    RevenueValues.AddRange(new double[] { 3000, 5000, 7000, 10000, 15000 });
                    Labels.AddRange(new[] { "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6" });
                    break;

                case "Tháng":
                    RevenueValues.AddRange(new double[] { 20000, 25000, 30000, 40000 });
                    Labels.AddRange(new[] { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4" });
                    break;

                case "Năm":
                    RevenueValues.AddRange(new double[] { 100000, 150000, 200000 });
                    Labels.AddRange(new[] { "2021", "2022", "2023" });
                    break;
            }

            // Cập nhật biểu đồ
            revenueChart.Update(true, true); // Cập nhật biểu đồ ngay lập tức
        }

        // Xử lý sự kiện khi thay đổi lựa chọn từ ComboBox
        private void TimePeriodComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedPeriod = (TimePeriodComboBox.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString();
            if (selectedPeriod != null)
            {
                LoadRevenueData(selectedPeriod);
            }
        }
    }
}
