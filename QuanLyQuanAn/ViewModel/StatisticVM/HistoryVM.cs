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
            

            // Chuyển dữ liệu từ model vào ObservableCollection
            
        }

        

    }    
    public class HistoryItem
    {
        public int idTable { get; set; }
        public string tableName { get; set; }
        public DateTime TimeIn { get; set; }
        public double TotalPrice { get; set; }
    }
}
