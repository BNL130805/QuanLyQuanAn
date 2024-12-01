using QuanLyQuanAn.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyQuanAn
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Kiểm tra trạng thái đăng nhập
            var currentAccount = CurrentAccoutDataprovider.CurrentAccout.GetCurrentAccoutByIdMachine();

            if (currentAccount.Count > 0)
            {
                int idAccount = (int)currentAccount[0].idAccount;
                var account = AccountDataprovider.Account.GetAccountById(idAccount);

                string typeAccount = account[0].TypeAccount;

                // Hiển thị cửa sổ phù hợp với loại tài khoản
                if (typeAccount == "Quản lý")
                {
                    Login loginWindow = new Login();
                    loginWindow.Hide();
                    Admin adminWindow = new Admin();
                    adminWindow.ShowDialog();
                    try
                    {
                        loginWindow.Show();
                    }
                    catch { }
                }
                else if (typeAccount == "Nhân viên")
                {
                    Login loginWindow = new Login();
                    loginWindow.Hide();
                    Staff staffWindow = new Staff();
                    staffWindow.ShowDialog();
                    try
                    {
                        loginWindow.Show();
                    }
                    catch { }
                    
                }
                else if (typeAccount == "Đầu bếp")
                {
                    Login loginWindow = new Login();
                    loginWindow.Hide();
                    Cheff cheffWindow = new Cheff();
                    cheffWindow.ShowDialog();
                    try
                    {
                        loginWindow.Show();
                    }
                    catch { }
                }
            }
            else
            {
                // Hiển thị cửa sổ đăng nhập nếu chưa đăng nhập
                Login loginWindow = new Login();
                loginWindow.Show();
            }
        }
    }
}
