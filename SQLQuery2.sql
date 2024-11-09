create database QuanLyQuanAn
go
use QuanLyQuanAn
go

create table Restaurant
(
	idRes int identity not null primary key,
	RestaurantName nvarchar(50) not null
)
go

create table Account
(
	RestaurantName nvarchar(50) not null,
	idRes int not null,
	Username nvarchar(50) not null,
	Password nvarchar(20) not null,
	TypeAccount nvarchar(20) not null default N'Nhân viên',

	primary key(RestaurantName, idRes, Username),
	constraint AtoR foreign key (idRes) references dbo.Restaurant(idRes)
)
go

create table foodCategory
(
	idRes int not null,
	idFoodCtg int identity not null primary key,
	name nvarchar(50) not null default N'chưa có tên',
	constraint FCtoR foreign key (idRes) references dbo.Restaurant(idRes)
)
go

create table food
(
	idFood int identity not null primary key,
	idRes int not null,
	idFoodCtg int not null,
	name nvarchar(50) not null default N'chưa có tên',
	price int not null default 0,
	FoodImage VARBINARY(MAX),

	constraint FtoCtg foreign key (idFoodCtg) references dbo.foodCategory(idFoodctg),
	constraint FtoR foreign key (idRes) references dbo.Restaurant(idRes)
)
go

create table tableFood
(
	idTable int identity not null primary key,
	idRes int not null,
	tableName nvarchar(20),
	status nvarchar(20) not null default 'trống',
	constraint TtoR foreign key (idRes) references dbo.Restaurant(idRes)
)
go

create table Bill
(
	idBill int identity not null primary key,
	idRes int not null,
	idTable int not null default 0,
	TimeIn datetime not null default getdate() ,
	TimeOut datetime,
	discount int default 0,
	TotalPrice int not null default 0,
	
	constraint BtoT foreign key(idTable) references dbo.tableFood(idTable),
	constraint BtoR foreign key (idRes) references dbo.Restaurant(idRes)
)
go

create table BillInf
(
	idBillInf int identity not null primary key,
	idRes int not null,
	idFood int not null,
	count int not null default 0,
	idBill int not null,

	constraint BItoF foreign key(idFood) references dbo.food(idFood),
	constraint BItoB foreign key(idBill) references dbo.Bill(idBill),
	constraint BItoR foreign key (idRes) references dbo.Restaurant(idRes)
)
go

create proc USP_GetAccountByUserName
@nameRestaurant nvarchar(50),
@userName nvarchar(50)
as
begin
	select * from dbo.Account where Username = @userName and RestaurantName = @nameRestaurant
end
go

create proc USP_Login
@nameRestaurant nvarchar(50),
@userName nvarchar(50),
@password nvarchar(20)
as
begin
	select * from dbo.Account where Username = @userName and RestaurantName = @nameRestaurant and Password = @password
end
go

create proc USP_tableList
as select * from dbo.tableFood
go

alter table dbo.Bill add status nvarchar(50) not null default N'Chưa thanh toán'
go

create proc USP_InsertBill
@idtable int, @discount int = 0
as
begin
	declare @count int
	select @count = count(*) from dbo.Bill where idTable = @idtable and status = N'Chưa thanh toán'
	if(@count > 0)
		update dbo.Bill set discount = @discount where idTable = @idtable and status = N'Chưa thanh toán'
	else
		insert into dbo.Bill(idTable, TimeIn , TimeOut, status, discount) values(@idtable, getdate(), null, N'Chưa thanh toán', @discount)
end
go

create proc USP_InsertBillInf
@idbill int, @idfood int, @count int
as
begin
	declare @isExitsBillInf int = 0
	declare @foodCount int = 0

	select @isExitsBillInf = idBillInf, @foodCount = count from dbo.BillInf where idBill = @idbill and idFood = @idfood
	
	if(@isExitsBillInf >0)
	begin
		declare @newCount int = @foodCount + @count
		if (@newCount >0)
			update dbo.BillInf set count = @foodCount + @count where idBill = @idbill and idFood = @idfood
		else
			delete dbo.BillInf where idBill = @idbill and idFood = @idfood
	end
	else
	begin
		if(@count > 0)
			insert dbo.BillInf(idBill, idFood, count) values(@idbill, @idfood, @count)
	end
end
go

create proc USP_ChangeTable
@IDtablefirst int, @IDtablesecond int
as
begin
	declare @IDbillFirst int
	declare @IDbillSecond int
	select @IDbillFirst = idBill from dbo.Bill where idTable = @IDtablefirst
	select @IDbillSecond = idBill from dbo.Bill where idTable = @IDtablesecond
	
	if(@IDbillSecond is null)
	begin
		insert into dbo.Bill(idTable, TimeIn, TimeOut, status, discount) values(@IDtablesecond, getdate(), null, N'Chưa thanh toán', 0)
		select @IDbillSecond = idBill from dbo.Bill where idTable = @IDtablesecond
	end

	declare @statusFtable nvarchar(50)

	select @statusFtable = status from dbo.tablefood where idTable = @IDtablefirst
	if (@statusFtable <> N'trống')
	begin
		declare @idBillInfF int
		declare @idBillInfS int
		select idBill into BillInfF from dbo.BillInf where idBill = @IDbillFirst
		update dbo.BillInf set idBill = @IDbillSecond where idBill in(select * from BillInfF)
		update dbo.tableFood set status = N'trống' where idTable = @IDtablefirst
		delete dbo.Bill where idTable = @IDtablefirst and status = N'Chưa thanh toán'
		drop table BillInfF
	end
end
go

create proc USP_InsertAccount
@NameRt nvarchar(50), @UserN nvarchar(50), @Password nvarchar(50), @TypeAccount nvarchar(50)
as
begin
	insert into dbo.Account(RestaurantName, Username, Password, TypeAccount) values(@NameRt, @UserN, @Password, @TypeAccount)
end
go

create proc USP_DeleteAccount
@NameRt nvarchar(50), @UserN nvarchar(50)
as
begin
	delete dbo.Account where RestaurantName = @NameRt and Username = @UserN
end
go

create proc USP_InsertFood
@idfoodctg int, @name nvarchar(50), @price int
as
begin
	insert into dbo.food(idFoodCtg, name, price) values (@idfoodctg, @name, @price)
end
go

create proc USP_DeleteFood
@name nvarchar(50), @idFoodCtg int
as
begin
	delete dbo.food where name = @name and idFoodCtg = @idFoodCtg
end
go

create proc USP_CheckUserN
@NameRt nvarchar(50), @UserN nvarchar(50)
as
begin
	select * from dbo.Account where RestaurantName = @NameRt and UserName = @UserN
end
go

create proc USP_CheckNameRt
@NameRt nvarchar(50)
as
begin
	select * from dbo.Account where RestaurantName = @NameRt
end
go

create trigger UTG_UpdateBillInF
on dbo.BillInf for insert, update
as
begin
	declare @idBill int

	select @idBill = idBill from inserted
	
	declare @idTable int
	select @idTable = idTable from dbo.Bill where idBill = @idBill and status = N'Chưa thanh toán'
	update dbo.tableFood set status = N'có người' where idTable = @idTable
end
go

create trigger UTG_UpdateBill
on dbo.Bill for update
as
begin
	declare @idBill int
	select @idBill = idBill from inserted
	declare @idTable int
	select @idTable = idTable from dbo.Bill where idBill = @idBill
	declare @count int =0
	select @count = count(*) from dbo.Bill where idTable = @idTable and status = N'Chưa thanh toán'
	if(@count = 0)
		update dbo.TableFood set status = N'trống' where idTable = @idTable
end
go

ALTER TABLE Account
DROP CONSTRAINT PK__Account__7F906AD957D88891;

alter table Account drop column RestaurantName

ALTER TABLE Account
ADD CONSTRAINT PK__Account__7F906AD957D88891 PRIMARY KEY (idRes, Username);

SELECT 
    CONSTRAINT_NAME AS PrimaryKeyName
FROM 
    INFORMATION_SCHEMA.TABLE_CONSTRAINTS
WHERE 
    TABLE_NAME = 'Account' 
    AND CONSTRAINT_TYPE = 'PRIMARY KEY';