using QuanLyQuanAn.ViewModel.MenuVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn.Model
{
    public class AccountDataprovider
    {
        private static AccountDataprovider account;

        public static AccountDataprovider Account
        {
            get { 
                if (account == null)
                {
                    account = new AccountDataprovider();
                }
                return account;
            }
            private set { account = value; }
        }
        private AccountDataprovider()
        {

        }

        public List<Account> GetAccountToLogin(string NameRes, string UserName, string TypeAccount, string Password)
        {
            using (var QuenryAccount = new QuanLyQuanAnEntities())
            {
                return QuenryAccount.Accounts.Where(p =>  p.Username == UserName && p.TypeAccount == TypeAccount && p.Password == Password).ToList();
            }
        }
    }
    public class CategoryProvider
    {
        private static CategoryProvider _category;

        public static CategoryProvider Category { 
            get
            {
                if (_category == null) 
                {
                    _category = new CategoryProvider();
                }
                return _category; 
        }
            private set => _category = value; }
        private CategoryProvider()
        {
        }

        public List<CategoryName> GetAllCategory()
        {
            using (QuanLyQuanAnEntities quenryCategory = new QuanLyQuanAnEntities())
            {
                return quenryCategory.foodCategories.Select(p => new CategoryName { Name = p.name }).ToList();
            }
        }
        public class FoodDataprovider
        {
            private static FoodDataprovider _food;

            public static FoodDataprovider Food {
                get {
                    if (_food == null)
                    {
                        _food = new FoodDataprovider();
                    }
                    return _food; 
                }
                private set => _food = value; }

            private FoodDataprovider() { }
            public List<food> GetAllFood()
            {
                using(var FoodQuenry = new QuanLyQuanAnEntities())
                {
                    return FoodQuenry.foods.ToList();
                }
            }
        }
    }
}
