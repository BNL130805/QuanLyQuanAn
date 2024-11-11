using QuanLyQuanAn.View;

namespace QuanLyQuanAn.ViewModel
{
    internal class AdminVM : BaseViewModel
    {
        private string _selectedOption;
        private object _option=new Menu();
        private bool _isMaximumWindow = false;

        public string SelectedOption { get => _selectedOption; 
            set 
            { 
                _selectedOption = value; 
                OnPropertyChanged();
                switch (_selectedOption)
                {
                    case "Menu":
                        Option = new Menu();
                        break;
                    case "FoodTable":
                        Option = new TableControl();
                        break;
                    case "HumanResouces":
                        Option = new HumanResources();
                        break;
                    case "Statistic":
                        Option = new StatisticsControl();
                        break;
                    default:
                        Option = new Menu();
                        break;
                }
            }
        }

        public object Option { get => _option;
            set 
            { 
                _option = value;
                OnPropertyChanged();
            }
        }

        public bool IsMaximumWindow { get => _isMaximumWindow; set { _isMaximumWindow = value; OnPropertyChanged(); } }

        public AdminVM()
        {
            SelectedOption = "Menu";
        }
    }
}
