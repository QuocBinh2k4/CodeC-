using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopDoTheThao
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            txt_TenDN.KeyDown += txt_TenDN_KeyDown;
            txt_Matkhau.KeyDown += txt_Matkhau_KeyDown;
        }

        private void txt_TenDN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Matkhau.Focus(); // chuyển xuống ô mật khẩu
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void txt_Matkhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Login.PerformClick(); // giả lập click nút đăng nhập
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }


        private void btn_Login_Click(object sender, EventArgs e)
        {
            //Kiểm tra đầy đủ thông tin chưa
            if(txt_TenDN.Text == string.Empty)
            {
                MessageBox.Show("Hãy nhập vào tài khoản của bạn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_TenDN.Focus();
                return;
            }

            if (txt_Matkhau.Text == string.Empty)
            {
                MessageBox.Show("Hãy nhập vào mật khẩu của bạn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_Matkhau.Focus();
                return;
            }

            //Kiểm tra tên đăng nhập có hợp lệ không?
            SqlConnection kn = new SqlConnection(@"Data Source=DESKTOP-SPN8MQA\SQLEXPRESS;Initial Catalog=QuanLyGiayTheThao;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT * FROM [TaiKhoan] WHERE TaiKhoan = @username AND MatKhau = @password", kn);
            cmd.Parameters.AddWithValue("@username", txt_TenDN.Text);
            cmd.Parameters.AddWithValue("@password", txt_Matkhau.Text);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtbuser = new DataTable();
            adapter.Fill(dtbuser);

            if (dtbuser.Rows.Count > 0)
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormMain mainForm = new FormMain();
                this.Hide(); // Ẩn form đăng nhập
                mainForm.ShowDialog(); // Hiện form chính dạng modal (chờ đóng mới quay lại login)
                //this.Show(); // Nếu muốn quay lại form login sau khi form chính đóng
                this.Close();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu sai!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
