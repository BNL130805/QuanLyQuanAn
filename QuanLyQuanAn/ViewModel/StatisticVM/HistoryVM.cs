using QuanLyQuanAn.Model;  
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace QuanLyQuanAn.ViewModel.StatisticVM
{
    internal class HistoryVM : BaseViewModel
    {
        // Thuộc tính chứa danh sách các mục lịch sử
        private ObservableCollection<HistoryItem> _historyList;
        public ObservableCollection<HistoryItem> HistoryList
        {
            get => _historyList;
            set
            {
                _historyList = value;
                OnPropertyChanged();
            }
        }

        
        public ICommand LoadHistoryCommand { get; set; }

        public HistoryVM()
        {
      
            LoadHistoryCommand = new RelayCommand(
                (p) => LoadHistory(),  
                (p) => true  
            );

            
            LoadHistory();
        }

        // Phương thức để tải dữ liệu lịch sử
        private void LoadHistory()
        {
            // Giả sử bạn có một phương thức lấy dữ liệu lịch sử từ database hoặc service
            var historyData = GetHistoryFromDatabase();

            // Chuyển dữ liệu từ model vào ObservableCollection
            HistoryList = new ObservableCollection<HistoryItem>(historyData);
        }

        // Phương thức giả lập lấy dữ liệu từ database
        private IEnumerable<HistoryItem> GetHistoryFromDatabase()
        {
            // Ở đây, bạn sẽ thay thế bằng cách gọi database hoặc service thực tế
            return new List<HistoryItem>
            {
                new HistoryItem { Id = 1, Description = "Bàn 1 được đặt", Date = DateTime.Now.AddDays(-1) },
                new HistoryItem { Id = 2, Description = "Bàn 2 đã thanh toán", Date = DateTime.Now.AddDays(-2) },
                new HistoryItem { Id = 3, Description = "Bàn 3 được đặt", Date = DateTime.Now.AddDays(-3) },
            };
        }
    }

    
    public class HistoryItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
