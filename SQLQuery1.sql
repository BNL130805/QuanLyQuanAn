﻿-- Thêm dữ liệu vào bảng Restaurant
INSERT INTO dbo.Restaurant (RestaurantName)
VALUES (N'Restaurant A');
GO

-- Thêm tài khoản cho nhà hàng Restaurant A
INSERT INTO dbo.Account (idRes, Username, Password, TypeAccount)
VALUES (1, N'admin', N'admin123', N'Quản lý');
GO

-- Thêm các danh mục món ăn (foodCategory)
INSERT INTO dbo.foodCategory (idRes, name)
VALUES 
    (1, N'Appetizers'), 
    (1, N'Main Courses'), 
    (1, N'Desserts'), 
    (1, N'Drinks');
GO

-- Thêm món ăn cho danh mục Appetizers
INSERT INTO dbo.food (idRes, idFoodCtg, name, price)
VALUES 
    (1, 1, N'Spring Rolls', 50000),
    (1, 1, N'Garlic Bread', 40000);
GO

-- Thêm món ăn cho danh mục Main Courses
INSERT INTO dbo.food (idRes, idFoodCtg, name, price)
VALUES 
    (1, 2, N'Grilled Chicken', 150000),
    (1, 2, N'Beef Steak', 200000);
GO

-- Thêm món ăn cho danh mục Desserts
INSERT INTO dbo.food (idRes, idFoodCtg, name, price)
VALUES 
    (1, 3, N'Chocolate Cake', 60000),
    (1, 3, N'Ice Cream', 40000);
GO

-- Thêm món ăn cho danh mục Drinks
INSERT INTO dbo.food (idRes, idFoodCtg, name, price)
VALUES 
    (1, 4, N'Coffee', 30000),
    (1, 4, N'Juice', 25000);
GO

-- Thêm các bàn ăn vào bảng tableFood
INSERT INTO dbo.tableFood (idRes, tableName, status)
VALUES 
    (1, N'Table 1', N'trống'), 
    (1, N'Table 2', N'trống'), 
    (1, N'Table 3', N'trống'), 
    (1, N'Table 4', N'trống'),
    (1, N'Table 5', N'trống'),
    (1, N'Table 6', N'trống'),
    (1, N'Table 7', N'trống'),
    (1, N'Table 8', N'trống'),
    (1, N'Table 9', N'trống'),
    (1, N'Table 10', N'trống'),
    (1, N'Table 11', N'trống'),
    (1, N'Table 12', N'trống'),
    (1, N'Table 13', N'trống'),
    (1, N'Table 14', N'trống'),
    (1, N'Table 15', N'trống'),
    (1, N'Table 16', N'trống'),
    (1, N'Table 17', N'trống'),
    (1, N'Table 18', N'trống'),
    (1, N'Table 19', N'trống'),
    (1, N'Table 20', N'trống');
GO

-- Cập nhật hình ảnh cho các món ăn
UPDATE dbo.food
SET FoodImage = (SELECT BulkColumn 
                 FROM OPENROWSET(BULK N'C:\Users\Hii\Documents\Do_An_Lttq\QuanLyQuanAn\food\Appetizers\Spring Rolls.jpg', SINGLE_BLOB) AS Image)
WHERE name = N'Spring Rolls'
AND idRes = 1;
GO

UPDATE dbo.food
SET FoodImage = (SELECT BulkColumn 
                 FROM OPENROWSET(BULK N'C:\Users\Hii\Documents\Do_An_Lttq\QuanLyQuanAn\food\Appetizers\Garlic Bread.jpg', SINGLE_BLOB) AS Image)
WHERE name = N'Garlic Bread'
AND idRes = 1;
GO

UPDATE dbo.food
SET FoodImage = (SELECT BulkColumn 
                 FROM OPENROWSET(BULK N'C:\Users\Hii\Documents\Do_An_Lttq\QuanLyQuanAn\food\Main Courses\Grilled Chicken.jpg', SINGLE_BLOB) AS Image)
WHERE name = N'Grilled Chicken'
AND idRes = 1;
GO

UPDATE dbo.food
SET FoodImage = (SELECT BulkColumn 
                 FROM OPENROWSET(BULK N'C:\Users\Hii\Documents\Do_An_Lttq\QuanLyQuanAn\food\Main Courses\Beef Steak.jpg', SINGLE_BLOB) AS Image)
WHERE name = N'Beef Steak'
AND idRes = 1;
GO

UPDATE dbo.food
SET FoodImage = (SELECT BulkColumn 
                 FROM OPENROWSET(BULK N'C:\Users\Hii\Documents\Do_An_Lttq\QuanLyQuanAn\food\Desserts\Chocolate Cake.jpg', SINGLE_BLOB) AS Image)
WHERE name = N'Chocolate Cake'
AND idRes = 1;
GO

UPDATE dbo.food
SET FoodImage = (SELECT BulkColumn 
                 FROM OPENROWSET(BULK N'C:\Users\Hii\Documents\Do_An_Lttq\QuanLyQuanAn\food\Desserts\Ice Cream.jpg', SINGLE_BLOB) AS Image)
WHERE name = N'Ice Cream'
AND idRes = 1;
GO

UPDATE dbo.food
SET FoodImage = (SELECT BulkColumn 
                 FROM OPENROWSET(BULK N'C:\Users\Hii\Documents\Do_An_Lttq\QuanLyQuanAn\food\Drinks\Coffee.jpg', SINGLE_BLOB) AS Image)
WHERE name = N'Coffee'
AND idRes = 1;
GO

UPDATE dbo.food
SET FoodImage = (SELECT BulkColumn 
                 FROM OPENROWSET(BULK N'C:\Users\Hii\Documents\Do_An_Lttq\QuanLyQuanAn\food\Drinks\Juice.jpg', SINGLE_BLOB) AS Image)
WHERE name = N'Juice'
AND idRes = 1;
GO
INSERT INTO dbo.Bill (idRes, idTable, TimeIn, TimeOut, discount, TotalPrice, status)
VALUES 
(1, 1, '2024-10-10 12:00:00', '2024-10-10 13:00:00', 10, 150000, N'Đã thanh toán'),
(1, 2, '2024-10-11 18:00:00', '2024-10-11 19:30:00', 0, 200000, N'Đã thanh toán'),
(1, 3, '2024-10-12 14:00:00', NULL, 5, 100000, N'Chưa thanh toán');

-- Thêm một số dữ liệu ảo vào bảng BillInf
INSERT INTO dbo.BillInf (idRes, idFood, count, idBill)
VALUES 
(1, 1, 2, 1),  -- Bill 1 có 2 món Spring Rolls
(1, 2, 1, 1),  -- Bill 1 có 1 món Garlic Bread
(1, 3, 3, 2),  -- Bill 2 có 3 món Grilled Chicken
(1, 4, 1, 2),  -- Bill 2 có 1 món Beef Steak
(1, 5, 2, 3),  -- Bill 3 có 2 món Chocolate Cake
(1, 6, 1, 3);  -- Bill 3 có 1 món Ice Cream

DECLARE @i INT = 1;
DECLARE @startDate DATETIME = '2024-01-01'; -- Đổi từ DATE sang DATETIME
DECLARE @endDate DATETIME = GETDATE();
DECLARE @currentDate DATETIME = @startDate;

WHILE @i <= 1000
BEGIN
    -- Tạo giá trị ngẫu nhiên cho các trường
    DECLARE @idTable INT = 1 + (ABS(CHECKSUM(NEWID())) % 20); -- Giả sử có 20 bàn (id từ 1 đến 20)
    DECLARE @discount INT = (ABS(CHECKSUM(NEWID())) % 21); -- Discount từ 0 đến 20%
    DECLARE @totalPrice INT = 100000 + (ABS(CHECKSUM(NEWID())) % 500000); -- Giá từ 100000 đến 600000
    DECLARE @timeIn DATETIME = DATEADD(HOUR, ABS(CHECKSUM(NEWID()) % 12), @currentDate); -- Giờ ngẫu nhiên trong ngày
    DECLARE @timeOut DATETIME = DATEADD(MINUTE, 30 + (ABS(CHECKSUM(NEWID())) % 91), @timeIn); -- 30-120 phút sau giờ vào

    -- Thêm dữ liệu vào bảng Bill
    INSERT INTO dbo.Bill (idRes, idTable, TimeIn, TimeOut, discount, TotalPrice)
    VALUES (1, @idTable, @timeIn, @timeOut, @discount, @totalPrice);

    -- Tăng ngày lên (tạo hóa đơn trong khoảng từ 1/1/2024 đến hôm nay)
    SET @currentDate = DATEADD(DAY, ABS(CHECKSUM(NEWID()) % 7), @currentDate); -- Tăng từ 0-6 ngày
    IF @currentDate > @endDate
        SET @currentDate = @startDate;

    SET @i = @i + 1;
END;