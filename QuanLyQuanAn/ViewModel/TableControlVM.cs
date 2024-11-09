using MaterialDesignThemes.Wpf;
using QuanLyQuanAn.Model;
using QuanLyQuanAn.View.DialogHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyQuanAn.ViewModel
{
    internal class TableControlVM : BaseViewModel
    {
        private object _currentDialogContent;

        private List<tableFood> _tableList;

        public object CurrentDialogContent
        {
            get => _currentDialogContent;
            set
            {
                _currentDialogContent = value;
                OnPropertyChanged();
            }
        }

        private object _title = "xinchao";

        public ICommand ShowAddTableCommand { get; }
        public ICommand CloseAddTable { get; }
        public ICommand AddTable { get; }
        public object Title { get => _title; set => _title = value; }
        public List<tableFood> TableList { get => _tableList; set { _tableList = value; OnPropertyChanged(); } }

        public TableControlVM()
        {
            
            TableList = TableProvider.Table.GetAllTable();
        }

        

        
    }
}
