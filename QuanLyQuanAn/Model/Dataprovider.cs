using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using QuanLyQuanAn.ViewModel.MenuVM;
using System.Collections.ObjectModel;
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
        public List<Account> GetAccountById(int Id)
        {
            using (var QuenryAccout = new QuanLyQuanAnEntities())
            {
                return QuenryAccout.Accounts.Where(p => p.idAccout == Id).ToList();
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

        public void DeleteCategory(int idFoodCtg)
        {
            using (var dbContext = new QuanLyQuanAnEntities())
            {
                // Lấy danh mục cần xóa
                var categoryToDelete = dbContext.foodCategories
                                                .FirstOrDefault(category => category.idFoodCtg == idFoodCtg);

                if (categoryToDelete != null)
                {
                    // Xóa các món ăn liên quan đến danh mục (nếu tồn tại)
                    var relatedFoods = dbContext.foods
                                                .Where(food => food.idFoodCtg == idFoodCtg)
                                                .ToList();

                    if (relatedFoods.Any())
                    {
                        dbContext.foods.RemoveRange(relatedFoods);
                    }

                    // Xóa danh mục
                    dbContext.foodCategories.Remove(categoryToDelete);

                    // Lưu thay đổi
                    dbContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Danh mục không tồn tại");
                }
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

        public void DeleteFood(int idFood)
        {
            using (var dbContext = new QuanLyQuanAnEntities())
            {
                var foodToDelete = dbContext.foods
                                            .FirstOrDefault(food => food.idFood == idFood);
                if (foodToDelete != null)
                {
                    dbContext.foods.Remove(foodToDelete);
                    dbContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Không tìm thấy món ăn với idFood: " + idFood);
                }
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
        public List<tableFood> GetAllTableByName(string name)
        {
            using(var TableQuenry = new QuanLyQuanAnEntities())
            {
                return TableQuenry.tableFoods.Where(p=>p.tableName == name).ToList();
            }
        }
        public List<tableFood> GetAllTable()
        {
            using (var TableQuenry = new QuanLyQuanAnEntities())
            {
                return TableQuenry.tableFoods.ToList();
            }
        }

        public object GetTableByStatus(string status)
        {
            using(var TableQuenry = new QuanLyQuanAnEntities())
            {
                var table = TableQuenry.tableFoods.Where(p => p.status == status).ToList();
                return table;
            }
        }

        public void DeleteTable(int idTable)
        {
            using (var TableQuenry = new QuanLyQuanAnEntities())
            {
                var tableToDelete = TableQuenry.tableFoods
                                               .FirstOrDefault(table => table.idTable == idTable);
                if (tableToDelete != null)
                {
                    TableQuenry.tableFoods.Remove(tableToDelete);

                    TableQuenry.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Tên bàn không tồn tại");
                }
            }
        }

        public void DeleteTables(List<int> idTables)
        {
            using (var TableQuery = new QuanLyQuanAnEntities())
            {
                var tablesToDelete = TableQuery.tableFoods
                                                .Where(table => idTables.Contains(table.idTable))
                                                .ToList();

                if (tablesToDelete.Any())
                {
                    TableQuery.tableFoods.RemoveRange(tablesToDelete);
                    TableQuery.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Không có bàn nào tồn tại để xóa.");
                }
            }
        }


        public object GetAllStatusTable()
        {
            using (var TableQuenry = new QuanLyQuanAnEntities())
            {
                var Status = (from status in TableQuenry.tableFoods
                             select new { status.status }).Distinct().ToList();
                return Status;
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
        public List<Account> GetHuman(string typeAccount)
        {
            using (var hmContext = new QuanLyQuanAnEntities())
            {
       
                return hmContext.Accounts
                                .Where(account => account.TypeAccount == typeAccount)
                                .ToList();
            }
        }

        public void DeleteHuman(string Username, string TypeAccount)
        {
            using (var hmContext = new QuanLyQuanAnEntities())
            {
                var accountToDelete = hmContext.Accounts
                                               .FirstOrDefault(account => account.Username == Username && account.TypeAccount == TypeAccount);

                // Kiểm tra nếu tài khoản tồn tại
                if (accountToDelete != null)
                {
                    // Xóa tài khoản
                    hmContext.Accounts.Remove(accountToDelete);

                    // Lưu thay đổi vào cơ sở dữ liệu
                    hmContext.SaveChanges();
                }
                else
                {
                    // Nếu không tìm thấy tài khoản, có thể ghi log hoặc thông báo lỗi
                    Console.WriteLine("Tài khoản không tồn tại.");
                }
            }

        }
        
    }
    public class BillDataprovider
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
        public void PayBillByIdTable(int idTable)
        {
            using (var quenryBill = new QuanLyQuanAnEntities())
            {
                Bill currentBill = quenryBill.Bills.FirstOrDefault(b => b.idTable == idTable && b.status == "Chưa thanh toán");
                tableFood currentTable = quenryBill.tableFoods.FirstOrDefault(t => t.idTable == idTable);
                currentTable.status = "trống";
                currentBill.status = "Đã thanh toán";
                quenryBill.SaveChanges();
            }
        }
        public void InsertBillByTable(ObservableCollection<ListBillInf> listBillInf, string table, int totalPrice)
        {
            using (var QuenryBill =  new QuanLyQuanAnEntities())
            {
                tableFood currentTable = TableProvider.Table.GetAllTableByName(table)[0];
                if (currentTable.status == "trống")
                {     
                    Bill newBill = new Bill();
                    int idCurrentAccout = (int)CurrentAccoutDataprovider.CurrentAccout.GetCurrentAccoutByIdMachine()[0].idAccount;
                    Account currentAccout = AccountDataprovider.Account.GetAccountById(idCurrentAccout)[0];
                    newBill.idRes = currentAccout.idRes;
                    newBill.idTable = currentTable.idTable;
                    newBill.TotalPrice = totalPrice;
                    newBill.TimeIn = DateTime.Now;
                    newBill.status = "Chưa thanh toán";
                    QuenryBill.Bills.Add(newBill);
                    QuenryBill.SaveChanges();
                    BillInfDataprovider.BillInf.InsertBillInfByIdBill(newBill.idBill, listBillInf);
                }
                else
                {
                    var currentBill = QuenryBill.Bills.FirstOrDefault(b => b.idTable == currentTable.idTable && b.status == "Chưa thanh toán");

                    if (currentBill != null)
                    {
                        // Cập nhật TotalPrice
                        currentBill.TotalPrice += totalPrice;

                        // Lưu thay đổi
                        QuenryBill.SaveChanges();
                    }
                    BillInfDataprovider.BillInf.InsertBillInfByIdBill(currentBill.idBill, listBillInf);
                }    
            }
        }
        public Bill GetBillUnpaidByTable(string table)
        {
            using(var QuenryBill = new QuanLyQuanAnEntities())
            {
                tableFood currentTable = TableProvider.Table.GetAllTableByName(table)[0];
                var bill = QuenryBill.Bills.Where(p => p.idTable == currentTable.idTable && p.status == "Chưa thanh toán").ToList()[0];
                return bill;
            }
        }
        public object GetBillInToday()
        {
            using (QuanLyQuanAnEntities quenryBill = new QuanLyQuanAnEntities())
            {
                var today = DateTime.Today;
                var tomorrow = today.AddDays(1);

                var doanhThuHomNay = quenryBill.Bills
                    .Where(bill => bill.TimeIn >= today && bill.TimeIn < tomorrow && bill.status == "Đã thanh toán") // So sánh phạm vi
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
                    .Where(bill => bill.TimeIn >= past7Days && bill.TimeIn <= endOfToday && bill.status == "Đã thanh toán")  // So sánh với phạm vi thời gian
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
                    .Where(bill => bill.TimeIn >= firstDayOfMonth && bill.TimeIn <= lastDayOfMonth && bill.status == "Đã thanh toán")
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
                    .Where(bill => bill.TimeIn >= firstDayOfYear && bill.status == "Đã thanh toán")
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
    public class CurrentAccoutDataprovider
    {
        static private CurrentAccoutDataprovider _currentAccout;
        static public CurrentAccoutDataprovider CurrentAccout {
            get
            {
                if(_currentAccout == null)
                {
                    _currentAccout = new CurrentAccoutDataprovider();
                }
                return _currentAccout;
                       } 
            private set => _currentAccout = value; }
        public List<CurrentSession> GetCurrentAccoutByIdMachine()
        {
            using (QuanLyQuanAnEntities QuenryCurrentAccout = new QuanLyQuanAnEntities())
            {
                return QuenryCurrentAccout.CurrentSessions.Where(p => p.MachineId == Environment.MachineName).ToList();
            }
        }
        public void UpdateCurrentAccout(string restaurantName, string username)
        {
            using (QuanLyQuanAnEntities QuenryCurrentAccout = new QuanLyQuanAnEntities())
            {
                var Restaurant = from account in QuenryCurrentAccout.Accounts
                                 join res in QuenryCurrentAccout.Restaurants
                                 on account.idRes equals res.idRes
                                 select new {account.idAccout, res.RestaurantName , account.Username};
                var accout = Restaurant.Where(p => p.RestaurantName == restaurantName && p.Username == username).ToList();
                int idAcount = (int)accout[0].idAccout;
                QuenryCurrentAccout.CurrentSessions.Add(new CurrentSession { idAccount = idAcount, MachineId = Environment.MachineName });
                QuenryCurrentAccout.SaveChanges();
            }
        }
        public void LogoutCurrentAccout()
        {
            using(var QuenryCurrentAccout = new QuanLyQuanAnEntities())
            {
                var CurrentAccout = QuenryCurrentAccout.CurrentSessions.FirstOrDefault(p => p.MachineId == Environment.MachineName);
                QuenryCurrentAccout.CurrentSessions.Remove(CurrentAccout);
                QuenryCurrentAccout.SaveChanges();

            }
        }
        private CurrentAccoutDataprovider() { }
    }
    public class BillInfDataprovider
    {
        private static BillInfDataprovider _billInf;

        public static BillInfDataprovider BillInf {
            get {
                if (_billInf == null)
                {
                    _billInf = new BillInfDataprovider();
                }
                return _billInf;
                    }
            private set => _billInf = value; }
        private BillInfDataprovider() { }
        public void InsertBillInfByIdBill(int idBill, ObservableCollection<ListBillInf> listBillInf) 
        {
            using (var QuenryBillInf = new QuanLyQuanAnEntities())
            {
                int idCurrentAccout = (int)CurrentAccoutDataprovider.CurrentAccout.GetCurrentAccoutByIdMachine()[0].idAccount;
                Account currentAccout = AccountDataprovider.Account.GetAccountById(idCurrentAccout)[0];

                foreach (var lbi in listBillInf)
                {
                    // Kiểm tra món ăn đã có trong BillInf chưa
                    var existingBillInf = QuenryBillInf.BillInfs
                        .FirstOrDefault(b => b.idBill == idBill && b.idFood == lbi.IdFood);

                    if (existingBillInf != null)
                    {
                        // Nếu món ăn đã có trong BillInf, cộng thêm count
                        existingBillInf.count += lbi.Count;
                    }
                    else
                    {
                        // Nếu chưa có, thêm món mới vào BillInf
                        BillInf newBillInf = new BillInf
                        {
                            idBill = idBill,
                            idFood = lbi.IdFood,
                            count = lbi.Count,
                            idRes = currentAccout.idRes
                        };
                        QuenryBillInf.BillInfs.Add(newBillInf);
                    }
                }

                // Lưu thay đổi
                QuenryBillInf.SaveChanges();
            }
        }
        public List<ListBillInf> GetBillInfByTable(int idTable)
        {
            using (var BillInfQuenry = new QuanLyQuanAnEntities())
            {
                var listBill = (from b in BillInfQuenry.Bills
                                join inf in BillInfQuenry.BillInfs
                                on b.idBill equals inf.idBill
                                join f in BillInfQuenry.foods
                                on inf.idFood equals f.idFood
                                where b.idTable == idTable && b.status == "Chưa thanh toán"
                                select new ListBillInf { Count = inf.count, FoodImage = f.FoodImage, IdFood = inf.idFood, Name = f.name, Price = f.price}).ToList();
                for (int i = 0; i< listBill.Count; i++)
                {
                    listBill[i].No = i + 1;
                }
                return listBill;
            }
        }
    }
    
}
