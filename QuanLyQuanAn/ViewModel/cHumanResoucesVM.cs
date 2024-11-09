using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyQuanAn.View.HumanResouce;

namespace QuanLyQuanAn.ViewModel
{
    internal class cHumanResoucesVM : BaseViewModel
    {
        private object _option;
        private string _selectedOption;

        public object Option { get => _option; set { _option = value; OnPropertyChanged(); } }
        public string SelectedOption { get => _selectedOption;
            set
            {
                _selectedOption = value;
                OnPropertyChanged();
                switch (SelectedOption)
                {
                    case "Manager":
                        Option = new ManagerControl();
                        break;
                    case "Staff":
                        Option = new StaffControl();
                        break;
                    case "Chef":
                        Option = new ChefControl();
                        break;
                    default:
                        Option = new ManagerControl();
                        break;
                }
            }
        }
        public cHumanResoucesVM()
        {
            SelectedOption = "Manager";
        }
    }
}
