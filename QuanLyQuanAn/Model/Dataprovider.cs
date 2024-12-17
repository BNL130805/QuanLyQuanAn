using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using QuanLyQuanAn.ViewModel.MenuVM;
using System.Collections.ObjectModel;
using QuanLyQuanAn.ViewModel;
using QuanLyQuanAn.ViewModel.StatisticVM;
using System.Xml.Linq;

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

        public List<dynamic> GetAccountToLogin(string NameRes, string UserName, string TypeAccount, string Password)
        {
            using (var QuenryAccount = new QuanLyQuanAnEntities())
            {
                var Account = (from Res in QuenryAccount.Restaurants
                               join A in QuenryAccount.Accounts
                               on Res.idRes equals A.idRes
                               where (Res.RestaurantName == NameRes && A.Username == UserName && A.TypeAccount == TypeAccount && A.Password == Password)
                               select new
                               {
                                   A.idAccout
                               })
                              .ToList<dynamic>();
                return Account;
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
        //
        public List<foodCategory> GetAllCategoryByName(string name)
        {
            using (var quenryCategory = new QuanLyQuanAnEntities())
            {
                return quenryCategory.foodCategories.Where(p => p.name == name).ToList();
            }
        }
        public List<foodCategory> GetAllCategory(string searchKeyword = null)
        {
            using (var dbContext = new QuanLyQuanAnEntities())
            {
                if (string.IsNullOrWhiteSpace(searchKeyword))
                {
                    // Trả về toàn bộ danh mục nếu không có từ khóa
                    return dbContext.foodCategories.ToList();
                }

                // Tìm kiếm danh mục theo tên (không phân biệt chữ hoa/chữ thường)
                return dbContext.foodCategories
                                .Where(category => category.name.ToLower().Contains(searchKeyword.ToLower()))
                                .ToList();
            }
        }



        public bool DeleteCategory(int idFoodCtg)
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
                        return false;
                    }
                    dbContext.foodCategories.Remove(categoryToDelete);

                    // Lưu thay đổi
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool AddCategory(CatagoryShow categoryAdd)
        {
            using(var QuenryCategory = new QuanLyQuanAnEntities())
            {
                if(QuenryCategory.foodCategories.Any(p=> p.name == categoryAdd.Name && p.idFoodCtg != categoryAdd.ID))
                {
                    return false;
                }
                else
                {
                    if (categoryAdd.ID == 0)
                    {
                        var category = (from CA in QuenryCategory.CurrentSessions
                                        join A in QuenryCategory.Accounts
                                        on CA.idAccount equals A.idAccout
                                        select new
                                        {
                                            A.idRes
                                        }
                                        ).ToList().Select(p => new foodCategory
                                        {
                                            idRes = p.idRes,
                                            name = categoryAdd.Name
                                        }).First();
                        QuenryCategory.foodCategories.Add(category);
                        QuenryCategory.SaveChanges();
                    }
                    else
                    {
                        var category = QuenryCategory.foodCategories.FirstOrDefault(p => p.idFoodCtg == categoryAdd.ID);
                        category.name = categoryAdd.Name;
                        QuenryCategory.SaveChanges();
                    }
                    return true;
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
        public List<dynamic> GetAllFood()
        {
            using (var FoodQuenry = new QuanLyQuanAnEntities())
            {
                var Food = (from foods in FoodQuenry.foods
                            join categories in FoodQuenry.foodCategories on foods.idFoodCtg equals categories.idFoodCtg
                            orderby categories.idFoodCtg
                            select new
                            {
                                foods.idFood,
                                foods.name,
                                foods.FoodImage,
                                foods.price,
                                foods.idFoodCtg,
                                CategoryName = categories.name
                            }
                            ).ToList<dynamic>();
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
                    var listBillInf = dbContext.BillInfs.Where(p => p.idFood == idFood);
                    if (listBillInf.Any())
                    {
                        dbContext.BillInfs.RemoveRange(listBillInf);
                        dbContext.SaveChanges();
                    }
                    dbContext.foods.Remove(foodToDelete);
                    dbContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Không tìm thấy món ăn với idFood: " + idFood);
                }
            }
        }
        public bool AddFood(FoodShow foodshow)
        {
            using (var QuenryFood = new QuanLyQuanAnEntities())
            {
                if (QuenryFood.foods.Any(p => p.name == foodshow.Name && p.idFood != foodshow.Id))
                {
                    return false;
                }
                else
                {
                    if(foodshow.Id == 0)
                    { 
                        var food = (from CA in QuenryFood.CurrentSessions
                                    join A in QuenryFood.Accounts
                                    on CA.idAccount equals A.idAccout
                                    select new
                                    {
                                        A.idRes
                                    }
                                            ).ToList().Select(p => new food
                                            {
                                                idRes = p.idRes,
                                                idFoodCtg = (int)foodshow.IdCategory,
                                                FoodImage = foodshow.FoodImage,
                                                name = foodshow.Name,
                                                price = foodshow.Price
                                            }).First();
                        QuenryFood.foods.Add(food);
                        QuenryFood.SaveChanges();
                    }
                    else
                    {
                        var food = QuenryFood.foods.FirstOrDefault(p=> p.idFood ==  foodshow.Id);
                        food.price = foodshow.Price;
                        food.name = foodshow.Name;
                        food.FoodImage = foodshow.FoodImage;
                        food.idFoodCtg = foodshow.IdCategory;
                        QuenryFood.SaveChanges();
                    }    
                    return true;
                }
            }
        }
        //
        public List<dynamic> SearchFoodByName(string name)
        {
            using (var FoodQuenry = new QuanLyQuanAnEntities())
            {
                var Food = (from foods in FoodQuenry.foods
                            join categories in FoodQuenry.foodCategories on foods.idFoodCtg equals categories.idFoodCtg
                            where foods.name.Contains(name)
                            orderby categories.idFoodCtg
                            select new
                            {
                                foods.idFood,
                                foods.name,
                                foods.FoodImage,
                                foods.price,
                                foods.idFoodCtg,
                                CategoryName = categories.name
                            }
                            ).ToList<dynamic>();
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
        public List<tableFood> GetAllTableByName(string name)
        {
            using(var TableQuenry = new QuanLyQuanAnEntities())
            {
                return TableQuenry.tableFoods.Where(p=>p.tableName == name).ToList();
            }
        }
        //
        public List<tableFood> GetAllTable(string name = null)
        {
            using (var TableQuenry = new QuanLyQuanAnEntities())
            {
                if (name == null)
                {
                    return TableQuenry.tableFoods.ToList();
                }    
                return TableQuenry.tableFoods
                                  .Where(p => p.tableName.Contains(name))
                                  .ToList();
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
                    TableQuenry.Bills.RemoveRange(TableQuenry.Bills.Where(p => p.idTable == idTable));
                    TableQuenry.SaveChanges();
                    TableQuenry.tableFoods.Remove(tableToDelete);
                    TableQuenry.SaveChanges();
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
        public bool AddTable(TableShow tableshow)
        {
            using (var QuenryTable = new QuanLyQuanAnEntities())
            {
                if (QuenryTable.tableFoods.Any(p => p.tableName == tableshow.Name && p.idTable != tableshow.IdTable))
                {
                    return false;
                }
                else
                {
                    if (tableshow.IdTable == 0)
                    {
                        var table = (from CA in QuenryTable.CurrentSessions
                                        join A in QuenryTable.Accounts
                                        on CA.idAccount equals A.idAccout
                                        select new
                                        {
                                            A.idRes
                                        }
                                        ).ToList().Select(p => new tableFood
                                        {
                                            idRes = p.idRes,
                                            status = "trống",
                                            tableName = tableshow.Name,
                                        }).First();
                        QuenryTable.tableFoods.Add(table);
                        QuenryTable.SaveChanges();
                    }
                    else
                    {
                        var table = QuenryTable.tableFoods.FirstOrDefault(p => p.idTable == tableshow.IdTable);
                        table.tableName = tableshow.Name;
                        QuenryTable.SaveChanges();
                    }
                    return true;
                }
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
        public void DeleteHuman(int idaccount)
        {
            using (var hmContext = new QuanLyQuanAnEntities())
            {
                var accountToDelete = hmContext.Accounts
                                               .FirstOrDefault(account => account.idAccout == idaccount );
                // Kiểm tra nếu tài khoản tồn tại
                if (accountToDelete != null)
                {
                    // Xóa tài khoản
                    hmContext.Accounts.Remove(accountToDelete);

                    // Lưu thay đổi vào cơ sở dữ liệu
                    hmContext.SaveChanges();
                }
            }

        }
        public bool AddHuman(HumanShow humanshow)
        {
            
            using (var QuenryHuman = new QuanLyQuanAnEntities())
            {
                var idRes = (from CA in QuenryHuman.CurrentSessions
                             join A in QuenryHuman.Accounts
                             on CA.idAccount equals A.idAccout
                             select new
                             {
                                 A.idRes
                             }).First();
                if (QuenryHuman.Accounts.Any(p => p.Username == humanshow.Name && p.idRes == idRes.idRes && p.idAccout != humanshow.IdAccount))
                {
                    return false;
                }
                else
                {
                    if (humanshow.IdAccount == 0)
                    {
                        var human = (from CA in QuenryHuman.CurrentSessions
                                     join A in QuenryHuman.Accounts
                                     on CA.idAccount equals A.idAccout
                                     select new
                                     {
                                         A.idRes
                                     }
                                        ).ToList().Select(p => new Account
                                        {
                                            idRes = p.idRes,
                                            Username = humanshow.Name,
                                            Password = humanshow.Password,
                                            TypeAccount = humanshow.TypeAccout,
                                        }).First();
                        QuenryHuman.Accounts.Add(human);
                        QuenryHuman.SaveChanges();
                    }
                    else
                    {
                        var human = QuenryHuman.Accounts.FirstOrDefault(p => p.idAccout == humanshow.IdAccount);
                        human.Username = humanshow.Name;
                        human.Password = humanshow.Password;
                        human.TypeAccount = humanshow.TypeAccout;
                        QuenryHuman.SaveChanges();
                    }
                    return true;
                }
            }
        }
        //
        public List<Account> SearchHumanByNameAndType(string username, string typeAccount)
        {
            using (var hmContext = new QuanLyQuanAnEntities())
            {
                return hmContext.Accounts
                                .Where(account => account.TypeAccount == typeAccount &&
                                                  account.Username.Contains(username))
                                .ToList();
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
        public object GetBillByDate(DateTime Begin, DateTime End)
        {
            using(var QuenryBill = new QuanLyQuanAnEntities())
            {
                int PeriodOfTime = (End - Begin).Days;
                if (PeriodOfTime <= 2)
                {
                    var doanhthu = QuenryBill.Bills
                        .Where(bill => bill.TimeIn >= Begin && bill.TimeIn <= End && bill.status == "Đã thanh toán")
                        .GroupBy(bill => bill.TimeIn.Hour)
                        .Select(listBill => new
                        {
                            ThoiGian = listBill.Key,
                            TongDoanhThu = listBill.Sum(bill => bill.TotalPrice)
                        })
                        .ToList();
                    return doanhthu;
                }
                else if (PeriodOfTime > 2 && PeriodOfTime<=60) 
                {
                    var doanhthu = QuenryBill.Bills
                        .Where(bill => bill.TimeIn >= Begin && bill.TimeIn <= End && bill.status == "Đã thanh toán")
                        .GroupBy(bill => DbFunctions.TruncateTime(bill.TimeIn))
                        .Select(listBill => new
                        {
                            ThoiGian = listBill.Key,
                            TongDoanhThu = listBill.Sum(bill => bill.TotalPrice)
                        })
                        .ToList();
                    return doanhthu;
                }
                else if(PeriodOfTime > 60 && PeriodOfTime <= 730)
                {
                    var doanhthu = QuenryBill.Bills
                        .Where(bill => bill.TimeIn >= Begin && bill.TimeIn <= End && bill.status == "Đã thanh toán")
                        .GroupBy(bill => bill.TimeIn.Month)
                        .Select(listBill => new
                        {
                            ThoiGian = listBill.Key,
                            TongDoanhThu = listBill.Sum(bill => bill.TotalPrice)
                        })
                        .ToList();
                    return doanhthu;
                }
                else
                {
                    var doanhthu = QuenryBill.Bills
                        .Where(bill => bill.TimeIn >= Begin && bill.TimeIn <= End && bill.status == "Đã thanh toán")
                        .GroupBy(bill => bill.TimeIn.Year)
                        .Select(listBill => new
                        {
                            ThoiGian = listBill.Key,
                            TongDoanhThu = listBill.Sum(bill => bill.TotalPrice)
                        })
                        .ToList();
                    return doanhthu;
                }
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
        public List<dynamic> GetBillInfByDate(DateTime Begin, DateTime End)
        {
            using (var QuenryBillInf = new QuanLyQuanAnEntities())
            {
                var listBillWithIdFood = (from b in QuenryBillInf.Bills
                                join bi in QuenryBillInf.BillInfs
                                on b.idBill equals bi.idBill
                                where (b.TimeIn >= Begin && b.TimeIn <= End)
                                select new
                                {
                                    bi.idFood,
                                    bi.count
                                }).ToList();
                var listBill = (from list in listBillWithIdFood
                                join f in QuenryBillInf.foods
                                on list.idFood equals f.idFood
                                select new
                                {
                                    list.count,
                                    f.name
                                }).ToList();
                return listBill.GroupBy(p=> p.name)
                    .Select(p => new
                    {
                        FoodName = p.Key,
                        Count = p.Sum(bi => bi.count)
                    }).ToList<dynamic>();
            }
        }

        
    }
    
}
