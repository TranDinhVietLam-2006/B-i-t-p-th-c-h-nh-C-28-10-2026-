
using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Bài_5._2
{
    public partial class Form1 : Form
    {
        // Kết nối SQL Server Object Explorer
        private readonly string connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;" +
            @"Initial Catalog=Bai_5_2;" +
            @"Integrated Security=True;" +
            @"TrustServerCertificate=True;";

        public Form1()
        {
            InitializeComponent();

            dtpBirthDate.MaxDate = DateTime.Today;
            dtpBirthDate.Value = DateTime.Today.AddYears(-18);
        }

        // Sự kiện khi mở Form
        private void Form1_Load(object? sender, EventArgs e)
        {
            try
            {
                TaoCoSoDuLieu();
                LoadKhoaHoc();
                LoadDanhSachDangKy();
            }
            catch (Exception ex)
            {
                btnDangKy.Enabled = false;

                MessageBox.Show(
                    "Lỗi kết nối SQL Server:\n" + ex.Message,
                    "Lỗi cơ sở dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Tạo database và bảng tự động
        private void TaoCoSoDuLieu()
        {
            SqlConnectionStringBuilder builder =
                new SqlConnectionStringBuilder(connectionString);

            builder.InitialCatalog = "master";

            // Tạo database
            using (SqlConnection conn =
                new SqlConnection(builder.ConnectionString))
            {
                conn.Open();

                string sql = @"
                    IF DB_ID(N'Bai_5_2') IS NULL
                    BEGIN
                        EXEC(N'CREATE DATABASE [Bai_5_2]')
                    END";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            // Tạo bảng trong database
            using (SqlConnection conn =
                new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                    IF OBJECT_ID(N'dbo.KhoaHoc', N'U') IS NULL
                    BEGIN
                        CREATE TABLE dbo.KhoaHoc
                        (
                            MaKhoaHoc INT IDENTITY(1,1)
                                PRIMARY KEY,
                            TenKhoaHoc NVARCHAR(100) NOT NULL
                        )
                    END

                    IF OBJECT_ID(N'dbo.DangKy', N'U') IS NULL
                    BEGIN
                        CREATE TABLE dbo.DangKy
                        (
                            MaDangKy INT IDENTITY(1,1)
                                PRIMARY KEY,
                            HoTen NVARCHAR(100) NOT NULL,
                            SoDienThoai VARCHAR(14) NOT NULL,
                            NgaySinh DATE NOT NULL,
                            GioiTinh NVARCHAR(10) NOT NULL,
                            MaKhoaHoc INT NOT NULL,
                            HocOnline BIT NOT NULL,
                            NhanThongBao BIT NOT NULL,
                            NgayDangKy DATETIME2 NOT NULL
                                DEFAULT SYSDATETIME(),

                            FOREIGN KEY (MaKhoaHoc)
                                REFERENCES dbo.KhoaHoc(MaKhoaHoc)
                        )
                    END

                    IF NOT EXISTS
                        (SELECT 1 FROM dbo.KhoaHoc)
                    BEGIN
                        INSERT INTO dbo.KhoaHoc (TenKhoaHoc)
                        VALUES
                            (N'Lập trình C#'),
                            (N'Lập trình Java'),
                            (N'Cơ sở dữ liệu SQL Server'),
                            (N'Lập trình Python'),
                            (N'Thiết kế Web')
                    END";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Nạp ComboBox bằng Binding Data
        private void LoadKhoaHoc()
        {
            try
            {
                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    string sql = @"
                        SELECT MaKhoaHoc, TenKhoaHoc
                        FROM dbo.KhoaHoc
                        ORDER BY MaKhoaHoc";

                    using (SqlDataAdapter adapter =
                        new SqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        cboCourse.DataSource = null;
                        cboCourse.DisplayMember = "TenKhoaHoc";
                        cboCourse.ValueMember = "MaKhoaHoc";
                        cboCourse.DataSource = dt;
                        cboCourse.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải khóa học:\n" + ex.Message,
                    "Lỗi CSDL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Hiển thị dữ liệu từ SQL Server
        private void LoadDanhSachDangKy()
        {
            try
            {
                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    string sql = @"
                        SELECT
                            dk.MaDangKy AS [Mã đăng ký],
                            dk.HoTen AS [Họ tên],
                            dk.SoDienThoai AS [Số điện thoại],
                            CONVERT(VARCHAR(10),
                                dk.NgaySinh, 103) AS [Ngày sinh],
                            dk.GioiTinh AS [Giới tính],
                            kh.TenKhoaHoc AS [Khóa học],

                            CASE
                                WHEN dk.HocOnline = 1 THEN N'Có'
                                ELSE N'Không'
                            END AS [Học Online],

                            CASE
                                WHEN dk.NhanThongBao = 1 THEN N'Có'
                                ELSE N'Không'
                            END AS [Nhận thông báo],

                            dk.NgayDangKy AS [Ngày đăng ký]

                        FROM dbo.DangKy dk
                        INNER JOIN dbo.KhoaHoc kh
                            ON dk.MaKhoaHoc = kh.MaKhoaHoc

                        ORDER BY dk.MaDangKy DESC";

                    using (SqlDataAdapter adapter =
                        new SqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgvDangKy.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi tải dữ liệu từ SQL Server: {ex.Message}",
                    "Lỗi CSDL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Nút Đăng ký
        private void btnDangKy_Click(
            object? sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();

            // Kiểm tra họ tên
            if (string.IsNullOrWhiteSpace(hoTen))
            {
                MessageBox.Show("Vui lòng nhập họ tên!");
                txtHoTen.Focus();
                return;
            }

            // Kiểm tra số điện thoại
            if (!mtxtPhone.MaskCompleted)
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ số điện thoại!");
                mtxtPhone.Focus();
                return;
            }

            // Kiểm tra khóa học
            if (cboCourse.SelectedValue is not int maKhoaHoc)
            {
                MessageBox.Show(
                    "Vui lòng chọn khóa học!");
                cboCourse.Focus();
                return;
            }

            string soDienThoai = mtxtPhone.Text;
            DateTime ngaySinh = dtpBirthDate.Value.Date;

            string gioiTinh =
                rdoNam.Checked ? "Nam" : "Nữ";

            string tenKhoaHoc = cboCourse.Text;

            bool hocOnline = chkHocOnline.Checked;
            bool nhanThongBao = chkNhanThongBao.Checked;

            try
            {
                int maDangKy;

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"
                        INSERT INTO dbo.DangKy
                        (
                            HoTen,
                            SoDienThoai,
                            NgaySinh,
                            GioiTinh,
                            MaKhoaHoc,
                            HocOnline,
                            NhanThongBao
                        )
                        OUTPUT INSERTED.MaDangKy
                        VALUES
                        (
                            @HoTen,
                            @SoDienThoai,
                            @NgaySinh,
                            @GioiTinh,
                            @MaKhoaHoc,
                            @HocOnline,
                            @NhanThongBao
                        )";

                    using (SqlCommand cmd =
                        new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add(
                            "@HoTen",
                            SqlDbType.NVarChar, 100
                        ).Value = hoTen;

                        cmd.Parameters.Add(
                            "@SoDienThoai",
                            SqlDbType.VarChar, 14
                        ).Value = soDienThoai;

                        cmd.Parameters.Add(
                            "@NgaySinh",
                            SqlDbType.Date
                        ).Value = ngaySinh;

                        cmd.Parameters.Add(
                            "@GioiTinh",
                            SqlDbType.NVarChar, 10
                        ).Value = gioiTinh;

                        cmd.Parameters.Add(
                            "@MaKhoaHoc",
                            SqlDbType.Int
                        ).Value = maKhoaHoc;

                        cmd.Parameters.Add(
                            "@HocOnline",
                            SqlDbType.Bit
                        ).Value = hocOnline;

                        cmd.Parameters.Add(
                            "@NhanThongBao",
                            SqlDbType.Bit
                        ).Value = nhanThongBao;

                        maDangKy =
                            Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }

                // Tổng hợp và hiển thị MessageBox
                string thongTin =
                    "ĐĂNG KÝ KHÓA HỌC THÀNH CÔNG!\n\n" +
                    $"Mã đăng ký: {maDangKy}\n" +
                    $"Họ tên: {hoTen}\n" +
                    $"Số điện thoại: {soDienThoai}\n" +
                    $"Ngày sinh: {ngaySinh:dd/MM/yyyy}\n" +
                    $"Giới tính: {gioiTinh}\n" +
                    $"Khóa học: {tenKhoaHoc}\n" +
                    $"Học Online: {(hocOnline ? "Có" : "Không")}\n" +
                    $"Nhận thông báo: " +
                    $"{(nhanThongBao ? "Có" : "Không")}";

                MessageBox.Show(
                    thongTin,
                    "Thông tin đăng ký",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Cập nhật dữ liệu sau khi lưu
                LoadDanhSachDangKy();
                LamMoiThongTin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi lưu dữ liệu vào SQL Server:\n"
                    + ex.Message,
                    "Lỗi CSDL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Làm mới thông tin
        private void LamMoiThongTin()
        {
            txtHoTen.Clear();
            mtxtPhone.Clear();

            dtpBirthDate.Value =
                DateTime.Today.AddYears(-18);

            cboCourse.SelectedIndex = -1;
            rdoNam.Checked = true;

            chkHocOnline.Checked = false;
            chkNhanThongBao.Checked = false;

            txtHoTen.Focus();
        }

        // Nút Làm mới
        private void btnLamMoi_Click(
            object? sender, EventArgs e)
        {
            LamMoiThongTin();
        }

        // Nút Tải danh sách
        private void btnTaiLai_Click(
            object? sender, EventArgs e)
        {
            LoadDanhSachDangKy();
        }
    }
}
