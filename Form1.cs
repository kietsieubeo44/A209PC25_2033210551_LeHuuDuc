using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace A209PC25_2033210551_LeHuuDuc
{
    public partial class Form1 : Form
    {
        private const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";


        public Form1()
        {
            InitializeComponent();
        }

        
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtMaNhanVIen.Text;
            string matKhau = txtMatKhau.Text;

            if (CheckLogin(maNhanVien, matKhau))
            {
                string role = GetRole(maNhanVien);

                if (role == "ROLE-QL")
                {
                    frmQuanLy quanLyForm = new frmQuanLy();
                    quanLyForm.Show();
                }
                else
                {
                    frmNhanVien nhanVienForm = new frmNhanVien();
                    nhanVienForm.Show();
                }

                this.Hide();
            }
            else
            {
                MessageBox.Show("Thông tin đăng nhập không chính xác. Vui lòng thử lại.", "Đăng Nhập Không Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckLogin(string maNhanVien, string matKhau)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM NHANVIEN WHERE MaNhanVien = @MaNhanVien AND MatKhau = @MatKhau";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                    cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private string GetRole(string maNhanVien)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Role FROM NHANVIEN WHERE MaNhanVien = @MaNhanVien";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                    object result = cmd.ExecuteScalar();
                    return result != null ? result.ToString() : "";
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
