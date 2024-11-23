﻿using QuanLyQuanAn.ViewModel.MenuVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyQuanAn.Model;
using System.Runtime.Remoting.Contexts;
using System.Data.Entity;

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
    class BillDataprovider
    {
        private static BillDataprovider _bill;

        public static BillDataprovider Bill
        {
            get
            {
                if (_bill == null)
                {
                    _bill = new BillDataprovider();
                }
                return _bill;
            }
            private set => _bill = value;
        }
        private BillDataprovider()
        {
        }

        public object GetBillInToday()
        {
            using (QuanLyQuanAnEntities quenryBill = new QuanLyQuanAnEntities())
            {
                var today = DateTime.Today;
                var tomorrow = today.AddDays(1);

                var doanhThuHomNay = quenryBill.Bills
                    .Where(bill => bill.TimeIn >= today && bill.TimeIn < tomorrow) // So sánh phạm vi
                    .GroupBy(bill => bill.TimeIn.Hour)
                    .Select(group => new
                    {
                        Gio = group.Key,
                        TongDoanhThu = group.Sum(bill => bill.TotalPrice)
                    })
                    .OrderBy(item => item.Gio)
                    .ToList();

                return doanhThuHomNay;
            }
        }

        public object GetBillInLatest7Day()
        {
            using (QuanLyQuanAnEntities quenryBill = new QuanLyQuanAnEntities())
            {
                var past7Days = DateTime.Today.AddDays(-7);
                var today = DateTime.Today;
                var endOfToday = today.AddDays(1).AddTicks(-1); // Đến 23:59:59 của ngày hôm nay

                var doanhThu7Ngay = quenryBill.Bills
                    .Where(bill => bill.TimeIn >= past7Days && bill.TimeIn <= endOfToday)  // So sánh với phạm vi thời gian
                    .GroupBy(bill => DbFunctions.TruncateTime(bill.TimeIn)) // Sử dụng DbFunctions.TruncateTime để loại bỏ giờ
                    .Select(group => new
                    {
                        Ngay = group.Key, // Trả về ngày (không có giờ)
                        TongDoanhThu = group.Sum(bill => bill.TotalPrice)
                    })
                    .OrderBy(item => item.Ngay)
                    .ToList();

                return doanhThu7Ngay;
            }
        }
        public object GetBillInThisMonth()
        {
            using (QuanLyQuanAnEntities quenryBill = new QuanLyQuanAnEntities())
            {
                var firstDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1); // Cuối tháng

                var doanhThuThangNay = quenryBill.Bills
                    .Where(bill => bill.TimeIn >= firstDayOfMonth && bill.TimeIn <= lastDayOfMonth)
                    .GroupBy(bill => DbFunctions.TruncateTime(bill.TimeIn)) // Sử dụng DbFunctions.TruncateTime để loại bỏ giờ
                    .Select(group => new
                    {
                        Ngay = group.Key, // Trả về ngày mà không có giờ
                        TongDoanhThu = group.Sum(bill => bill.TotalPrice)
                    })
                    .OrderBy(item => item.Ngay)
                    .ToList();

                return doanhThuThangNay;
            }
        }
        public object GetBillThisYear()
        {
            using (QuanLyQuanAnEntities quenryBill = new QuanLyQuanAnEntities())
            {
                var firstDayOfYear = new DateTime(DateTime.Today.Year, 1, 1);
                var doanhThuNamNay = quenryBill.Bills
                    .Where(bill => bill.TimeIn >= firstDayOfYear)
                    .GroupBy(bill => bill.TimeIn.Month)
                    .Select(group => new
                    {
                        Thang = group.Key,
                        TongDoanhThu = group.Sum(bill => bill.TotalPrice)
                    })
                    .OrderBy(item => item.Thang)
                    .ToList();

                return doanhThuNamNay;
            }
        }
    }
}
