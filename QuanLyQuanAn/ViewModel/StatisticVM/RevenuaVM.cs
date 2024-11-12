using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyQuanAn.Model;

namespace QuanLyQuanAn.ViewModel.StatisticVM
{
    internal class RevenuaVM:BaseViewModel
    {
        private string _typeRevenua = "Hôm nay";
        private object _timeLable;
        public Func<double, string> Formatter { get; set; }
        private object _revenueData;

        public RevenuaVM()
        {
            // Khởi tạo Formatter để định dạng số tiền
            Formatter = value => value.ToString("N0");
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
                        // Lấy dữ liệu doanh thu theo giờ cho hôm nay
                        var todayBills = (BillDataprovider.Bill.GetBillInToday() as IEnumerable<dynamic>)?
                            .Select(b => (Gio: (int)b.Gio, TotalPrice: (double)b.TongDoanhThu));
                        TimeLable = todayBills?.Select(d => $"{d.Gio}:00").ToList();
                        RevenueData = todayBills?.Select(d => d.TotalPrice).ToList();
                        break;
                    case "7 ngày gần đây":
                        // Lấy dữ liệu doanh thu trong 7 ngày gần đây
                        var Lastest7DaysBill = (BillDataprovider.Bill.GetBillInLatest7Day() as IEnumerable<dynamic>)?
                             .Select(b => (Ngay: (DateTime)b.Ngay, TotalPrice: b.TongDoanhThu));
                        TimeLable = Lastest7DaysBill?.Select(d => d.Ngay.ToShortDateString()).ToList();
                        RevenueData = Lastest7DaysBill?.Select(d => d.TotalPrice).ToList();
                        break;
                    case "Tháng này":
                        // Lấy dữ liệu doanh thu trong tháng này
                        var ThisMonthBills = (BillDataprovider.Bill.GetBillInThisMonth() as IEnumerable<dynamic>)?
                             .Select(b => (Ngay: (DateTime)b.Ngay, TotalPrice: b.TongDoanhThu));
                        TimeLable = ThisMonthBills?.Select(d => d.Ngay.ToShortDateString()).ToList();
                        RevenueData = ThisMonthBills?.Select(d => d.TotalPrice).ToList();
                        break;
                    case "Năm nay":
                        // Lấy dữ liệu doanh thu trong năm nay
                        var ThisYearBills = (BillDataprovider.Bill.GetBillThisYear() as IEnumerable<dynamic>)?
                             .Select(b => (Thang: (int)b.Thang, TotalPrice: b.TongDoanhThu));
                        TimeLable = ThisYearBills?.Select(d => d.Thang.ToString()).ToList();
                        RevenueData = ThisYearBills?.Select(d => d.TotalPrice).ToList();
                        break;
                    case "Chọn khoảng thời gian":
                        // Xử lý khi người dùng chọn khoảng thời gian cụ thể (nếu có)
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

        public object RevenueData
        {
            get => _revenueData;
            set
            {
                _revenueData = value;
                OnPropertyChanged();
            }
        }
    }
}
