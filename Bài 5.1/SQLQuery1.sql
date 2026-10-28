CREATE TABLE dbo.TaiKhoan
(
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(50)  NOT NULL UNIQUE,
    MatKhau     NVARCHAR(100) NOT NULL
);
GO

INSERT INTO dbo.TaiKhoan (TenDangNhap, MatKhau)
VALUES (N'admin', N'123456'),
       (N'user1',  N'abc123');
GO

SELECT * FROM dbo.TaiKhoan;