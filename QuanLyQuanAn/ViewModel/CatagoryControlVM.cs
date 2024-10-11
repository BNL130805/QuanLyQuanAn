using QuanLyQuanAn.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows.Media;

namespace QuanLyQuanAn.ViewModel
{

    
    internal class CatagoryControlVM : BaseViewModel
    {
        private ObservableCollection<Member> _members;
        public CatagoryControlVM() 
        {
            Members = new ObservableCollection<Member>();

           }
        public ObservableCollection<Member> Members { get => _members; set { _members = value; OnPropertyChanged() ; }}
    }

    public class Member
    {
        public string Character { get; set; }
        public Brush BgColor { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
