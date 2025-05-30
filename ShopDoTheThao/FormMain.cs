using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using ShopDoTheThao.Properties;
using System.IO;

namespace ShopDoTheThao
{
    public partial class FormMain : Form
    {
        string chuoikn = @"Data Source=DESKTOP-SPN8MQA\SQLEXPRESS;Initial Catalog=QuanLyGiayTheThao;Integrated Security=True";
        SqlConnection kn = new SqlConnection(@"Data Source=DESKTOP-SPN8MQA\SQLEXPRESS;Initial Catalog=QuanLyGiayTheThao;Integrated Security=True");
        SqlDataAdapter adapter;
        DataSet ds;
        BindingSource bs = new BindingSource();

        public FormMain()
        {
            InitializeComponent();

            // 🧪 Kiểm tra kết nối SQL Server
            KiemTraKetNoi();

            // 🔁 Nếu kết nối thành công, tiếp tục load sản phẩm
            LoadSanPhamNoiBat();
        }

        // 🧪 Hàm kiểm tra kết nối SQL
        private void KiemTraKetNoi()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(chuoikn))
                {
                    conn.Open();
                    MessageBox.Show("✅ Kết nối SQL Server thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi kết nối SQL Server: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSanPhamNoiBat()
        {
            flowLayoutPanel_SanPham.Controls.Clear();

            // Ảnh lớn trên cùng
            PictureBox bigBanner = new PictureBox();
            bigBanner.Size = new Size(flowLayoutPanel_SanPham.Width - 40, 300);
            bigBanner.SizeMode = PictureBoxSizeMode.StretchImage;
            bigBanner.Image = Image.FromFile(@"C:\Users\admin\source\repos\ShopDoTheThao\ShopDoTheThao\Resources\slideshow.jpg"); // Ví dụ: Application.StartupPath + @"\images\banner.png"
            bigBanner.Margin = new Padding(10);
            flowLayoutPanel_SanPham.Controls.Add(bigBanner);

            // Dữ liệu sản phẩm
            using (SqlConnection conn = new SqlConnection(chuoikn))
            {
                conn.Open();
                string query = "SELECT TOP 6 * FROM SanPham ORDER BY SoLuong DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Panel pnl = new Panel();
                    pnl.Size = new Size(150, 220);
                    pnl.BorderStyle = BorderStyle.FixedSingle;
                    pnl.Margin = new Padding(10);

                    PictureBox pic = new PictureBox();
                    string imgPath = reader["HinhAnh"].ToString().Trim();

                    if (File.Exists(imgPath))
                        pic.Image = Image.FromFile(imgPath);
                    else
                        pic.Image = Properties.Resources.slideshow;

                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic.Size = new Size(140, 120);
                    pic.Location = new Point(5, 5);

                    Label lblName = new Label();
                    lblName.Text = reader["TenSP"].ToString();
                    lblName.Location = new Point(5, 130);
                    lblName.Size = new Size(140, 40);
                    lblName.Font = new Font("Arial", 9, FontStyle.Bold);
                    lblName.TextAlign = ContentAlignment.MiddleCenter;

                    Label lblPrice = new Label();
                    lblPrice.Text = "Giá: " + string.Format("{0:N0}đ", reader["GiaKM"]);
                    lblPrice.ForeColor = Color.DarkGreen;
                    lblPrice.Location = new Point(5, 180);
                    lblPrice.Size = new Size(140, 30);
                    lblPrice.TextAlign = ContentAlignment.MiddleCenter;

                    pnl.Controls.Add(pic);
                    pnl.Controls.Add(lblName);
                    pnl.Controls.Add(lblPrice);

                    flowLayoutPanel_SanPham.Controls.Add(pnl);
                }
            }
        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Không cần xử lý gì ở đây hiện tại
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
