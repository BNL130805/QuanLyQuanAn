using QuanLyQuanAn.ViewModel.MenuVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyQuanAn.Model;
using System.Runtime.Remoting.Contexts;

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

        public List<foodCategory> GetAllCategory()
        {
            using (QuanLyQuanAnEntities quenryCategory = new QuanLyQuanAnEntities())
            {
                return quenryCategory.foodCategories.ToList();
            }
        }
        
    }
    public class FoodDataprovider
    {
        private static FoodDataprovider _food;

        public static FoodDataprovider Food
        {
            get
            {
                if (_food == null)
                {
                    _food = new FoodDataprovider();
                }
                return _food;
            }
            private set => _food = value;
        }

        private FoodDataprovider() { }
        public object GetAllFood()
        {
            using (var FoodQuenry = new QuanLyQuanAnEntities())
            {
                var Food = (from foods in FoodQuenry.foods
                            join categories in FoodQuenry.foodCategories on foods.idFoodCtg equals categories.idFoodCtg
                            select new
                            {
                                foods.idFood,
                                foods.name,
                                foods.FoodImage,
                                foods.price,
                                CategoryName = categories.name
                            }).ToList();
                return Food;
            }
        }
    }
    public class TableProvider
    {
        private static TableProvider _table;

        public static TableProvider Table
        {
            get
            {
                if (_table == null)
                {
                    _table = new TableProvider();
                }
                return _table;
            }
            private set => _table = value;
        }

        private TableProvider() { }
        public List<tableFood> GetAllTable()
        {
            using (var TableQuenry = new QuanLyQuanAnEntities())
            {
                return TableQuenry.tableFoods.ToList();
            }
        }
    }
    public class HumanResouceDataProvider
    {
        private static HumanResouceDataProvider _human;

        public static HumanResouceDataProvider Human
        {
            get
            {
                if (_human == null)
                {
                    _human = new HumanResouceDataProvider();
                }
                return _human;
            }
            private set => _human = value;
            
        }

        private HumanResouceDataProvider() { }
        public List<Account> GetAllChef()
        {
            using (var ChefQuenry = new QuanLyQuanAnEntities())
            {
                return ChefQuenry.Accounts.ToList();
            }
        }
        public List<Account> GetAllManager()
        {
            using (var ManagerQuenry = new QuanLyQuanAnEntities())
            {
                return ManagerQuenry.Accounts.ToList();
            }
        }
        public List<Account> GetAllStaff()
        {
            using (var StaffQuenry = new QuanLyQuanAnEntities())
            {
                return StaffQuenry.Accounts.ToList();
            }
        }
    }
}
