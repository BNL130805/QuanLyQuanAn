using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace QuanLyQuanAn.View
{
    /// <summary>
    /// Interaction logic for FoodControl.xaml
    /// </summary>
    public partial class FoodControl : UserControl
    {
        public FoodControl()
        {
            InitializeComponent();
            var converter = new BrushConverter();
            ObservableCollection<Member> members = new ObservableCollection<Member>();

            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Nước cam", Position = "Đồ uống", Email = "john.doe@gmail.com", Phone = "120.000đ" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Nước cam", Position = "Đồ uống", Email = "john.doe@gmail.com", Phone = "120.000đ" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Nước cam", Position = "Đồ uống", Email = "john.doe@gmail.com", Phone = "120.000đ" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Nước cam", Position = "Đồ uống", Email = "john.doe@gmail.com", Phone = "120.000đ" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Nước cam", Position = "Đồ uống", Email = "john.doe@gmail.com", Phone = "120.000đ" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Nước cam", Position = "Đồ uống", Email = "john.doe@gmail.com", Phone = "120.000đ" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Nước cam", Position = "Đồ uống", Email = "john.doe@gmail.com", Phone = "120.000đ" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Nước cam", Position = "Đồ uống", Email = "john.doe@gmail.com", Phone = "120.000đ" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Nước cam", Position = "Đồ uống", Email = "john.doe@gmail.com", Phone = "120.000đ" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Nước cam", Position = "Đồ uống", Email = "john.doe@gmail.com", Phone = "120.000đ" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Nước cam", Position = "Đồ uống", Email = "john.doe@gmail.com", Phone = "120.000đ" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Nước cam", Position = "Đồ uống", Email = "john.doe@gmail.com", Phone = "120.000đ" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Nước cam", Position = "Đồ uống", Email = "john.doe@gmail.com", Phone = "120.000đ" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Nước cam", Position = "Đồ uống", Email = "john.doe@gmail.com", Phone = "120.000đ" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Nước cam", Position = "Đồ uống", Email = "john.doe@gmail.com", Phone = "120.000đ" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Nước cam", Position = "Đồ uống", Email = "john.doe@gmail.com", Phone = "120.000đ" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Nước cam", Position = "Đồ uống", Email = "john.doe@gmail.com", Phone = "120.000đ" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Nước cam", Position = "Đồ uống", Email = "john.doe@gmail.com", Phone = "120.000đ" });



            membersDataGrid.ItemsSource = members;
        }
    }
}
