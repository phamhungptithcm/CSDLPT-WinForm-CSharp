using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            string chuoiketnoi = "Data Source=JAXZ;Initial Catalog=QLSV;Integrated Security=True";
            Program.conn.ConnectionString = chuoiketnoi;
            Program.conn.Open();
            DataTable dt = new DataTable();
            dt = Program.ExecSqlDataTable("SELECT * FROM V_DSPM");
            Program.bds_dspm.DataSource = dt;
            cmbKhoa.DataSource = dt;
            cmbKhoa.DisplayMember = "TENPM";
            cmbKhoa.ValueMember = "TENSERVER";
            cmbKhoa.SelectedIndex = -1;

        }

        private void cmbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Program.servername = cmbKhoa.SelectedValue.ToString();

            }
            catch (Exception) { };
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() == "" || txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Login name và mật mã không được trống", "", MessageBoxButtons.OK);
                return;
            }
            Program.mlogin = txtUsername.Text; Program.password = txtPassword.Text;
            if (Program.KetNoi() == 0) return;

            Program.mChinhanh = cmbKhoa.SelectedIndex;

            Program.mloginDN = Program.mlogin;
            Program.passwordDN = Program.password;
            string strLenh = "EXEC SP_THONGTINDANGNHAP '" + Program.mlogin + "'";

            Program.myReader = Program.ExecSqlDataReader(strLenh);
            if (Program.myReader == null) return;
            Program.myReader.Read();


            Program.username = Program.myReader.GetString(0);     // Lay user name
            if (Convert.IsDBNull(Program.username))
            {
                MessageBox.Show("Login bạn nhập không có quyền truy cập dữ liệu\n Bạn xem lại username, password", "", MessageBoxButtons.OK);
                return;
            }
            Program.mHoten = Program.myReader.GetString(1);
            Program.mGroup = Program.myReader.GetString(2);
            Program.myReader.Close();
            Program.conn.Close();
            MessageBox.Show("Nhan vien - Nhom : " + Program.mHoten + " - " + Program.mGroup, "", MessageBoxButtons.OK);
  
        }
    }
}
