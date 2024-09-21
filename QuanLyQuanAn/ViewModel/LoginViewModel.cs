using QuanLyQuanAn.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyQuanAn.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand RegisterCommand { get; set; }

        public ICommand BackCommand { get; set; }

        public ICommand CloseWindow {  get; set; }

        public ICommand LoginAccountCommand {  get; set; }
        public Visibility LoginVisibility { get => loginVisibility; 
            set
                { loginVisibility = value;
                OnPropertyChanged("LoginVisibility");
            } 
        }

        private string _username;
        private string _restaurantName;
        private ComboBoxItem _typeAccount;
        private string _password;
        public Visibility RegisterVisibility { get => registerVisibility; set { registerVisibility = value; OnPropertyChanged("RegisterVisibility"); } }

        public string Username { get => _username; set { _username = value; OnPropertyChanged(); } }

        public string RestaurantName { get => _restaurantName; set { _restaurantName = value; OnPropertyChanged(); } }

        public ComboBoxItem TypeAccount { get => _typeAccount; set{ _typeAccount = value; OnPropertyChanged(); } }

        private Visibility registerVisibility = Visibility.Collapsed;

        private Visibility loginVisibility = Visibility.Visible;

        public LoginViewModel() 
        {
            RegisterCommand = new RelayCommand(
                async (p) =>
                {
                    await Task.Delay(100);
                    LoginVisibility = Visibility.Collapsed;
                    RegisterVisibility = Visibility.Visible;
                },
                (p)=>true
                );
            BackCommand = new RelayCommand(
                async (p) =>
                {
                    await Task.Delay(100);
                    LoginVisibility = Visibility.Visible;
                    RegisterVisibility = Visibility.Collapsed;
                },
                (p) => true
                );
            CloseWindow = new RelayCommand(
                (p) =>
                {
                    if(p is Window w)
                    {
                        if (MessageBox.Show("Bạn có muốn đóng chương trình?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            w.Close();
                        }
                    }
                },
                (p) => true
                );
            LoginAccountCommand = new RelayCommand(
                (p) =>
                {
                    if (p is PasswordBox pw)
                    {
                        _password = pw.Password;
                        Window window = Window.GetWindow(pw);
                        if (TypeAccount?.Content is StackPanel TBtypeaccount)
                        {
                            TextBlock tb = TBtypeaccount.Children[1] as TextBlock;
                            if (AccountDataprovider.Account.GetAccountToLogin(RestaurantName, Username, tb.Text, _password).Count > 0)
                            {
                                window.Hide();
                                MainWindow w = new MainWindow();
                                w.ShowDialog();
                                window.Show();
                            }
                            else
                            {
                                MessageBox.Show("Thông tin đăng nhập không đúng!", "Thông báo");
                            }
                        }
                        else
                        {
                            MessageBox.Show("chưa hoạt động!", "Thông báo");
                        }
                    }
                    else
                    {
                        MessageBox.Show("chưa hoạt động!", "Thông báo");
                    }    
                },
                (p) =>
                {
                    return RestaurantName != null && Username != null && TypeAccount != null && RestaurantName != "" && Username != "";
                });
        }
    }
}
