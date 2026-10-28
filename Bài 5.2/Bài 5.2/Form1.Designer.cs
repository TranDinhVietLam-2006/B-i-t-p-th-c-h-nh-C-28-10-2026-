
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Bài_5._2
{
    partial class Form1
    {
        private IContainer? components = null;

        private Label lblTieuDe = null!;
        private GroupBox grpThongTin = null!;
        private Label lblHoTen = null!;
        private Label lblPhone = null!;
        private Label lblBirthDate = null!;
        private Label lblCourse = null!;
        private Label lblGioiTinh = null!;
        private Label lblLuaChon = null!;
        private Label lblDanhSach = null!;

        private TextBox txtHoTen = null!;
        private MaskedTextBox mtxtPhone = null!;
        private DateTimePicker dtpBirthDate = null!;
        private ComboBox cboCourse = null!;

        private RadioButton rdoNam = null!;
        private RadioButton rdoNu = null!;

        private CheckBox chkHocOnline = null!;
        private CheckBox chkNhanThongBao = null!;

        private Panel pnlGioiTinh = null!;
        private Panel pnlLuaChon = null!;

        private Button btnDangKy = null!;
        private Button btnLamMoi = null!;
        private Button btnTaiLai = null!;

        private DataGridView dgvDangKy = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTieuDe = new Label();
            grpThongTin = new GroupBox();

            lblHoTen = new Label();
            lblPhone = new Label();
            lblBirthDate = new Label();
            lblCourse = new Label();
            lblGioiTinh = new Label();
            lblLuaChon = new Label();
            lblDanhSach = new Label();

            txtHoTen = new TextBox();
            mtxtPhone = new MaskedTextBox();
            dtpBirthDate = new DateTimePicker();
            cboCourse = new ComboBox();

            rdoNam = new RadioButton();
            rdoNu = new RadioButton();

            chkHocOnline = new CheckBox();
            chkNhanThongBao = new CheckBox();

            pnlGioiTinh = new Panel();
            pnlLuaChon = new Panel();

            btnDangKy = new Button();
            btnLamMoi = new Button();
            btnTaiLai = new Button();

            dgvDangKy = new DataGridView();

            grpThongTin.SuspendLayout();
            pnlGioiTinh.SuspendLayout();
            pnlLuaChon.SuspendLayout();

            ((ISupportInitialize)dgvDangKy).BeginInit();
            SuspendLayout();

            // Thiết lập Form
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;

            ClientSize = new Size(1030, 660);
            MinimumSize = new Size(960, 620);

            StartPosition = FormStartPosition.CenterScreen;

            Text = "Bài 5.2 - Đăng ký khóa học";
            Font = new Font("Segoe UI", 10F);

            Load += Form1_Load;

            // Tiêu đề
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Text = "ĐĂNG KÝ KHÓA HỌC";

            lblTieuDe.Font =
                new Font("Segoe UI", 18F, FontStyle.Bold);

            lblTieuDe.TextAlign =
                ContentAlignment.MiddleCenter;

            lblTieuDe.Location = new Point(15, 10);
            lblTieuDe.Size = new Size(1000, 49);

            lblTieuDe.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            // GroupBox thông tin
            grpThongTin.Name = "grpThongTin";
            grpThongTin.Text = "Thông tin đăng ký";

            grpThongTin.Location = new Point(20, 67);
            grpThongTin.Size = new Size(990, 206);

            grpThongTin.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            // Họ tên
            lblHoTen.Text = "Họ và tên:";
            lblHoTen.Location = new Point(20, 36);
            lblHoTen.Size = new Size(110, 28);

            txtHoTen.Name = "txtHoTen";
            txtHoTen.Location = new Point(135, 35);
            txtHoTen.Size = new Size(305, 30);
            txtHoTen.MaxLength = 100;

            // Số điện thoại
            lblPhone.Text = "Số điện thoại:";
            lblPhone.Location = new Point(505, 36);
            lblPhone.Size = new Size(120, 28);

            mtxtPhone.Name = "mtxtPhone";
            mtxtPhone.Location = new Point(640, 35);
            mtxtPhone.Size = new Size(310, 30);

            // Mask theo đề bài
            mtxtPhone.Mask = "(000) 000-0000";

            mtxtPhone.TextMaskFormat =
                MaskFormat.IncludeLiterals;

            // Ngày sinh
            lblBirthDate.Text = "Ngày sinh:";
            lblBirthDate.Location = new Point(20, 85);
            lblBirthDate.Size = new Size(110, 28);

            dtpBirthDate.Name = "dtpBirthDate";
            dtpBirthDate.Location = new Point(135, 84);
            dtpBirthDate.Size = new Size(305, 30);

            dtpBirthDate.Format =
                DateTimePickerFormat.Short;

            // Khóa học
            lblCourse.Text = "Khóa học:";
            lblCourse.Location = new Point(505, 85);
            lblCourse.Size = new Size(120, 28);

            cboCourse.Name = "cboCourse";
            cboCourse.Location = new Point(640, 84);
            cboCourse.Size = new Size(310, 30);

            cboCourse.DropDownStyle =
                ComboBoxStyle.DropDownList;

            // Giới tính
            lblGioiTinh.Text = "Giới tính:";
            lblGioiTinh.Location = new Point(20, 136);
            lblGioiTinh.Size = new Size(110, 28);

            pnlGioiTinh.Location = new Point(135, 133);
            pnlGioiTinh.Size = new Size(305, 39);

            rdoNam.Name = "rdoNam";
            rdoNam.Text = "Nam";
            rdoNam.Location = new Point(3, 7);
            rdoNam.AutoSize = true;
            rdoNam.Checked = true;

            rdoNu.Name = "rdoNu";
            rdoNu.Text = "Nữ";
            rdoNu.Location = new Point(100, 7);
            rdoNu.AutoSize = true;

            pnlGioiTinh.Controls.AddRange(
                new Control[]
                {
                    rdoNam,
                    rdoNu
                });

            // CheckBox
            lblLuaChon.Text = "Tùy chọn:";
            lblLuaChon.Location = new Point(505, 136);
            lblLuaChon.Size = new Size(120, 28);

            pnlLuaChon.Location = new Point(640, 133);
            pnlLuaChon.Size = new Size(310, 39);

            chkHocOnline.Name = "chkHocOnline";
            chkHocOnline.Text = "Học online";
            chkHocOnline.Location = new Point(3, 7);
            chkHocOnline.AutoSize = true;

            chkNhanThongBao.Name = "chkNhanThongBao";
            chkNhanThongBao.Text = "Nhận thông báo";
            chkNhanThongBao.Location = new Point(135, 7);
            chkNhanThongBao.AutoSize = true;

            pnlLuaChon.Controls.AddRange(
                new Control[]
                {
                    chkHocOnline,
                    chkNhanThongBao
                });

            // Thêm các điều khiển vào GroupBox
            grpThongTin.Controls.AddRange(
                new Control[]
                {
                    lblHoTen,
                    txtHoTen,
                    lblPhone,
                    mtxtPhone,
                    lblBirthDate,
                    dtpBirthDate,
                    lblCourse,
                    cboCourse,
                    lblGioiTinh,
                    pnlGioiTinh,
                    lblLuaChon,
                    pnlLuaChon
                });

            // Nút Đăng ký
            btnDangKy.Name = "btnDangKy";
            btnDangKy.Text = "Đăng ký";

            btnDangKy.Location = new Point(25, 287);
            btnDangKy.Size = new Size(140, 40);

            btnDangKy.Click += btnDangKy_Click;

            // Nút Làm mới
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Text = "Làm mới";

            btnLamMoi.Location = new Point(178, 287);
            btnLamMoi.Size = new Size(140, 40);

            btnLamMoi.Click += btnLamMoi_Click;

            // Nút Tải danh sách
            btnTaiLai.Name = "btnTaiLai";
            btnTaiLai.Text = "Tải danh sách";

            btnTaiLai.Location = new Point(332, 287);
            btnTaiLai.Size = new Size(160, 40);

            btnTaiLai.Click += btnTaiLai_Click;

            // Tiêu đề danh sách
            lblDanhSach.Text = "DANH SÁCH ĐÃ ĐĂNG KÝ";

            lblDanhSach.Font =
                new Font("Segoe UI", 11F, FontStyle.Bold);

            lblDanhSach.Location = new Point(23, 342);
            lblDanhSach.Size = new Size(500, 28);

            // DataGridView
            dgvDangKy.Name = "dgvDangKy";
            dgvDangKy.Location = new Point(20, 377);
            dgvDangKy.Size = new Size(990, 258);

            dgvDangKy.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            dgvDangKy.ReadOnly = true;

            dgvDangKy.AllowUserToAddRows = false;
            dgvDangKy.AllowUserToDeleteRows = false;

            dgvDangKy.RowHeadersVisible = false;

            dgvDangKy.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvDangKy.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.DisplayedCells;

            // Thêm điều khiển lên Form
            Controls.AddRange(
                new Control[]
                {
                    lblTieuDe,
                    grpThongTin,
                    btnDangKy,
                    btnLamMoi,
                    btnTaiLai,
                    lblDanhSach,
                    dgvDangKy
                });

            ((ISupportInitialize)dgvDangKy).EndInit();

            pnlLuaChon.ResumeLayout(false);
            pnlLuaChon.PerformLayout();

            pnlGioiTinh.ResumeLayout(false);
            pnlGioiTinh.PerformLayout();

            grpThongTin.ResumeLayout(false);
            grpThongTin.PerformLayout();

            ResumeLayout(false);
        }
    }
}
