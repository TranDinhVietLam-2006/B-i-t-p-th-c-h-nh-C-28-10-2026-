using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bài_5._1
{
    public partial class Form1 : Form
    {
        // Chuỗi kết nối tới SQL Server LocalDB (database đã tạo sẵn trong SQL Server Object Explorer)
        private string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=Bai5_1_LoginDB;Integrated Security=True;";

        public Form1()
        {
            InitializeComponent();
        }

        // Khi mở form: thử kết nối CSDL, nếu lỗi thì báo ngay
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối SQL Server: {ex.Message}", "Lỗi CSDL",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tick -> hiện mật khẩu, bỏ tick -> ẩn mật khẩu
        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        // Xóa lỗi khi người dùng nhập lại
        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtUsername.Text))
                errorProvider.SetError(txtUsername, "");
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Text))
                errorProvider.SetError(txtPassword, "");
        }

        // Kiểm tra không để trống bằng ErrorProvider
        private bool ValidateInput()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                errorProvider.SetError(txtUsername, "Tên đăng nhập không được để trống!");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtUsername, "");
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                errorProvider.SetError(txtPassword, "Mật khẩu không được để trống!");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtPassword, "");
            }

            return isValid;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                    txtUsername.Focus();
                else
                    txtPassword.Focus();
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Truy vấn có tham số (@u, @p) để tránh SQL Injection
                    string sql = "SELECT Id, TenDangNhap FROM TaiKhoan WHERE TenDangNhap = @u AND MatKhau = @p";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@u", txtUsername.Text.Trim());
                    adapter.SelectCommand.Parameters.AddWithValue("@p", txtPassword.Text);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Đăng nhập thành công!\nXin chào " + dt.Rows[0]["TenDangNhap"],
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPassword.Clear();
                        txtPassword.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi truy vấn dữ liệu từ SQL Server: {ex.Message}", "Lỗi CSDL",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
