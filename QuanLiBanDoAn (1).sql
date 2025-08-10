Create database QuanLyBanDoAn
Go

use QuanLyBanDoAn
Go

-- Bảng Nhân viên
CREATE TABLE NhanVien (
    MaNhanVien NVARCHAR(10) PRIMARY KEY,
    TenNhanVien NVARCHAR(100) NOT NULL,
	GioiTinh NVARCHAR(10),
    ChucVu NVARCHAR(50),
    SoDienThoai VARCHAR(15),
	NgaySinh DATE,
);

-- Bảng Bàn
CREATE TABLE Ban (
    MaBan NVARCHAR(10) PRIMARY KEY,
    TenBan NVARCHAR(50) NOT NULL,
    TrangThai NVARCHAR(20) NOT NULL CHECK (TrangThai IN ('Trống', 'Đang dùng', 'Đặt trước'))
);
CREATE TABLE TaiKhoan (
  MaTaiKhoan NVARCHAR(10) PRIMARY KEY,
    MaNhanVien NVARCHAR(10) NOT NULL FOREIGN KEY REFERENCES NhanVien(MaNhanVien),
    TenDangNhap NVARCHAR(50) NOT NULL UNIQUE,
    MatKhau NVARCHAR(255) NOT NULL,
    VaiTro NVARCHAR(20) NOT NULL,
    TrangThai NVARCHAR(20) NOT NULL DEFAULT N'Hoạt động',
    
    CONSTRAINT CK_TaiKhoan_VaiTro CHECK (VaiTro IN (N'Quản lý', N'Thu ngân', N'Phục vụ')),
    CONSTRAINT CK_TaiKhoan_TrangThai CHECK (TrangThai IN (N'Hoạt động', N'Khóa', N'Đã xóa'))
	);




-- Bảng Món ăn
CREATE TABLE MonAn (
    MaMonAn NVARCHAR(10) PRIMARY KEY,
    TenMonAn NVARCHAR(100) NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL CHECK (DonGia >= 0),
    MoTa NVARCHAR(255),
	SoLuong INT
);

-- Bảng Đơn hàng
CREATE TABLE DonHang (
    MaDonHang NVARCHAR(10) PRIMARY KEY,
    MaBan NVARCHAR(10) NOT NULL,
    MaNhanVien NVARCHAR(10) NOT NULL,
    NgayDat DATETIME NOT NULL DEFAULT GETDATE(),
    GhiChu NVARCHAR(255),
    FOREIGN KEY (MaBan) REFERENCES Ban(MaBan),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);

-- Bảng Hóa đơn
CREATE TABLE HoaDon (
    MaHoaDon NVARCHAR(10) PRIMARY KEY,
    MaDonHang NVARCHAR(10) NOT NULL,
    NgayThanhToan DATETIME NOT NULL DEFAULT GETDATE(),
    TongTien DECIMAL(18,2) NOT NULL CHECK (TongTien >= 0),
    FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang)
);

-- Bảng Chi tiết hóa đơn
CREATE TABLE ChiTietHoaDon (
    MaChiTiet NVARCHAR(10) PRIMARY KEY,
    MaHoaDon NVARCHAR(10) NOT NULL,
    MaMonAn NVARCHAR(10) NOT NULL,
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    DonGia DECIMAL(18,2) NOT NULL CHECK (DonGia >= 0),
    FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon),
    FOREIGN KEY (MaMonAn) REFERENCES MonAn(MaMonAn)
);

-- Bảng Thống kê (tùy mục đích, có thể là tổng hợp doanh thu, món ăn bán chạy,...)
CREATE TABLE ThongKe (
    MaThongKe NVARCHAR(10) PRIMARY KEY,
    Ngay DATE NOT NULL,
    TongDoanhThu DECIMAL(18,2) NOT NULL CHECK (TongDoanhThu >= 0),
    SoLuongDon INT NOT NULL CHECK (SoLuongDon >= 0),
    SoLuongMonAnBanRa INT NOT NULL CHECK (SoLuongMonAnBanRa >= 0)
);


-- NhanVien
INSERT INTO NhanVien (TenNhanVien, GioiTinh, ChucVu, SoDienThoai, NgaySinh)
VALUES 
(N'Nguyễn Thành Phát', N'Nam', N'Quản lý', '0901230001', '1990-01-10'),
(N'Nguyễn Hoàng Phúc', N'Nam', N'Quản lý', '0901230002', '1992-03-15');


INSERT INTO NhanVien (TenNhanVien, GioiTinh, ChucVu, SoDienThoai, NgaySinh)
VALUES 
(N'Võ Thị Quế Trân', N'Nữ', N'Thu ngân', '0901230003', '1998-05-20'),
(N'Nguyễn Thanh Ngân', N'Nữ', N'Thu ngân', '0901230004', '1997-07-25');

-- 10 Nhân viên (2K4 – 2K5)
INSERT INTO NhanVien (MaNhanVien, TenNhanVien, GioiTinh, ChucVu, SoDienThoai, NgaySinh)
VALUES 
('NV001', N'Nguyễn Văn A', N'Nam', N'Phục vụ', '0901231001', '2004-01-01'),
('NV002', N'Lê Thị B', N'Nữ', N'Phục vụ', '0901231002', '2004-02-02'),
('NV003', N'Trần Văn C', N'Nam', N'Phục vụ', '0901231003', '2004-03-03'),
('NV004', N'Phạm Thị D', N'Nữ', N'Phục vụ', '0901231004', '2004-04-04'),
('NV005', N'Võ Văn E', N'Nam', N'Phục vụ', '0901231005', '2004-05-05'),
('NV006', N'Đỗ Thị F', N'Nữ', N'Phục vụ', '0901231006', '2005-01-01'),
('NV007', N'Lâm Văn G', N'Nam', N'Phục vụ', '0901231007', '2005-02-02'),
('NV008', N'Hồ Thị H', N'Nữ', N'Phục vụ', '0901231008', '2005-03-03'),
('NV009', N'Ngô Văn I', N'Nam', N'Phục vụ', '0901231009', '2005-04-04'),
('NV010', N'Tô Thị K', N'Nữ', N'Phục vụ', '0901231010', '2005-05-05');





-- Tài Khoản
INSERT INTO TaiKhoan (MaNhanVien, TenDangNhap, MatKhau, VaiTro, TrangThai)
VALUES 
(1, 'nguyenthanhphat', '123456', N'Quản lý', N'Hoạt động'),
(2, 'nguyenhoangphuc', '123456', N'Quản lý', N'Hoạt động'),
(3, 'vothiquetran', '123456', N'Thu ngân', N'Hoạt động'),
(4, 'nguyenthanhngan', '123456', N'Thu ngân', N'Hoạt động'),
(5, 'nguyenvana', '123456', N'Phục vụ', N'Hoạt động'),
(6, 'lethib', '123456', N'Phục vụ', N'Hoạt động'),
(7, 'tranvanc', '123456', N'Phục vụ', N'Hoạt động'),
(8, 'phamthid', '123456', N'Phục vụ', N'Hoạt động'),
(9, 'vovane', '123456', N'Phục vụ', N'Hoạt động'),
(10, 'dothif', '123456', N'Phục vụ', N'Hoạt động'),
(11, 'lamvang', '123456', N'Phục vụ', N'Hoạt động'),
(12, 'hothih', '123456', N'Phục vụ', N'Hoạt động'),
(13, 'ngovani', '123456', N'Phục vụ', N'Hoạt động'),
(14, 'tothik', '123456', N'Phục vụ', N'Hoạt động');


INSERT INTO TaiKhoan (MaTaiKhoan, MaNhanVien, TenDangNhap, MatKhau, VaiTro, TrangThai)
VALUES 
('TK001', 'NV001', N'nguyenthanhphat', N'123456', N'Phục vụ', N'Hoạt động'),
('TK002', 'NV002', N'lethib', N'123456', N'Phục vụ', N'Hoạt động'),
('TK003', 'NV003', N'tranvanc', N'123456', N'Phục vụ', N'Hoạt động'),
('TK004', 'NV004', N'phamthid', N'123456', N'Thu ngân', N'Hoạt động'),
('TK005', 'NV005', N'vovane', N'123456', N'Quản lý', N'Hoạt động');

INSERT INTO MonAn (MaMonAn, TenMonAn, DonGia, MoTa, SoLuong)
VALUES
('MA001', N'Phở bò', 45000, N'Phở nước dùng bò truyền thống',1),
('MA002', N'Cơm tấm sườn', 40000, N'Cơm tấm với sườn nướng, bì, chả', 1),
('MA003', N'Bún chả Hà Nội', 42000, N'Bún ăn kèm chả thịt nướng và nước mắm chua ngọt', 1),
('MA004', N'Mì xào hải sản', 50000, N'Mì xào với tôm, mực và rau củ', 1),
('MA005', N'Cơm chiên dương châu', 38000, N'Cơm chiên với lạp xưởng, trứng, rau củ', 1),
('MA006', N'Lẩu thái', 120000, N'Lẩu chua cay với hải sản, nấm và rau', 1),
('MA007', N'Gỏi cuốn', 15000, N'Cuốn tôm thịt ăn kèm nước chấm', 1),
('MA008', N'Bánh mì thịt', 20000, N'Bánh mì với pate, thịt nguội, rau sống', 1),
('MA009', N'Cháo gà', 35000, N'Cháo nấu từ gà ta, ăn kèm hành và tiêu', 1),
('MA010', N'Bánh xèo', 30000, N'Bánh xèo nhân tôm, thịt, ăn với rau sống và nước mắm', 1);

--Quyền 
SELECT * FROM TaiKhoan;



    

