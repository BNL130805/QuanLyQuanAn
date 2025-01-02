create database QuanLyQuanAn
go
use QuanLyQuanAn
go

create table Restaurant
(
	idRes int identity not null primary key,
	RestaurantName nvarchar(50) COLLATE SQL_Latin1_General_CP1_CS_AS not null 
)
go

create table Account
(
	RestaurantName nvarchar(50) not null,
	idRes int not null,
	Username nvarchar(50) COLLATE SQL_Latin1_General_CP1_CS_AS not null,
	Password nvarchar(20) COLLATE SQL_Latin1_General_CP1_CS_AS not null,
	TypeAccount nvarchar(20) not null default N'Nhân viên',

	constraint PK__Account__7F906AD957D88891 primary key (RestaurantName, idRes, Username),
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

alter table dbo.Bill add status nvarchar(50) not null default N'Chưa thanh toán'
go

ALTER TABLE Account
DROP CONSTRAINT PK__Account__7F906AD957D88891;

alter table Account drop column RestaurantName

ALTER TABLE Account
ADD CONSTRAINT PK__Account__7F906AD957D88891 PRIMARY KEY (idRes, Username);
go

CREATE TABLE CurrentSession
(
    SessionId INT IDENTITY constraint PK__CurrentS__C9F49290BABCA842 PRIMARY KEY,
    idRes INT NOT NULL,
    Username NVARCHAR(50) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL,
    LoginTime DATETIME DEFAULT GETDATE(),
    LogoutTime DATETIME NULL,
    constraint FK__CurrentSession__73BA3083 FOREIGN KEY (idRes, Username) REFERENCES Account(idRes, Username)
)
go
ALTER TABLE CurrentSession DROP CONSTRAINT PK__CurrentS__C9F49290BABCA842; -- Xóa ràng buộc khóa chính
go
ALTER TABLE CurrentSession DROP COLUMN SessionId;             -- Xóa cột SessionId
go
ALTER TABLE CurrentSession ADD MachineId NVARCHAR(50) NOT NULL constraint PK__CurrentS__C9F49290BABCA842  PRIMARY KEY;
go
ALTER TABLE Account Add idAccout INT identity not null
go
alter table CurrentSession drop constraint FK__CurrentSession__73BA3083
go
alter table CurrentSession drop column idRes
alter table CurrentSession drop column Username
go
alter table CurrentSession add idAccount int
go

ALTER TABLE Account
DROP CONSTRAINT PK__Account__7F906AD957D88891;
go

ALTER TABLE Account
ADD CONSTRAINT PK__Account__7F906AD957D88891 PRIMARY KEY (idRes, idAccout);
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

alter table dbo.Bill
add completion nvarchar(20)