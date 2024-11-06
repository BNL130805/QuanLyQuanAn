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
    /// Interaction logic for TableControl.xaml
    /// </summary>
    public partial class TableControl : UserControl
    {
        public TableControl()
        {
            InitializeComponent();
            var converter = new BrushConverter();
            ObservableCollection<Member> members = new ObservableCollection<Member>();

            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Bàn 1", Position = "trống", Email = "john.doe@gmail.com", Phone = "415-954-1475" });

            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Bàn 2", Position = "trống", Email = "john.doe@gmail.com", Phone = "415-954-1475" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Bàn 3", Position = "trống", Email = "john.doe@gmail.com", Phone = "415-954-1475" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Bàn 4", Position = "trống", Email = "john.doe@gmail.com", Phone = "415-954-1475" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Bàn 5", Position = "trống", Email = "john.doe@gmail.com", Phone = "415-954-1475" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Bàn 6", Position = "trống", Email = "john.doe@gmail.com", Phone = "415-954-1475" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Bàn 7", Position = "trống", Email = "john.doe@gmail.com", Phone = "415-954-1475" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Bàn 8", Position = "trống", Email = "john.doe@gmail.com", Phone = "415-954-1475" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Bàn 9", Position = "trống", Email = "john.doe@gmail.com", Phone = "415-954-1475" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Bàn 10", Position = "trống", Email = "john.doe@gmail.com", Phone = "415-954-1475" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Bàn 11", Position = "trống", Email = "john.doe@gmail.com", Phone = "415-954-1475" });
            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "Bàn 12", Position = "trống", Email = "john.doe@gmail.com", Phone = "415-954-1475" });


            membersDataGrid.ItemsSource = members;
        }
    }
}
