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

namespace QuanLyQuanAn.View.Statistics
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : UserControl
    {
        public History()
        {
            InitializeComponent();

            var converter = new BrushConverter();
            ObservableCollection<Member> members = new ObservableCollection<Member>();

            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Bàn 1", Position = "13:36:00 03/11/2024", Email = "150.000đ", Phone = "415-954-1475" });
            members.Add(new Member { Number = "2", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Bàn 1", Position = "13:36:00 03/11/2024", Email = "150.000đ", Phone = "415-954-1475" });
            members.Add(new Member { Number = "3", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Bàn 1", Position = "13:36:00 03/11/2024", Email = "150.000đ", Phone = "415-954-1475" });
            members.Add(new Member { Number = "4", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Bàn 1", Position = "13:36:00 03/11/2024", Email = "150.000đ", Phone = "415-954-1475" });
            members.Add(new Member { Number = "5", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Bàn 1", Position = "13:36:00 03/11/2024", Email = "150.000đ", Phone = "415-954-1475" });
            members.Add(new Member { Number = "6", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Bàn 1", Position = "13:36:00 03/11/2024", Email = "150.000đ", Phone = "415-954-1475" });
            members.Add(new Member { Number = "7", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Bàn 1", Position = "13:36:00 03/11/2024", Email = "150.000đ", Phone = "415-954-1475" });
            members.Add(new Member { Number = "8", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Bàn 1", Position = "13:36:00 03/11/2024", Email = "150.000đ", Phone = "415-954-1475" });
            members.Add(new Member { Number = "9", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Bàn 1", Position = "13:36:00 03/11/2024", Email = "150.000đ", Phone = "415-954-1475" });


            membersDataGrid.ItemsSource = members;
        }
    }
}
