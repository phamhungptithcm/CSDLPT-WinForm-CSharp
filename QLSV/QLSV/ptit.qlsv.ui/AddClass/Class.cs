using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLSV
{
    public partial class Class : DevExpress.XtraEditors.XtraForm
    {
        int vitri = 0;
        string makhoa = "";

        public Class()
        {
            InitializeComponent();
        }

        private void lOPBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsLOP.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dS);

        }

        private void Class_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dS.SINHVIEN' table. You can move, or remove it, as needed.
           
            dS.EnforceConstraints = false;
            // TODO: This line of code loads data into the 'dS.LOP' table. You can move, or remove it, as needed.
            this.LOPTableAdapter.Fill(this.dS.LOP);
            this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
            this.LOPTableAdapter.Fill(this.dS.LOP);
            this.SINHVIENTableAdapter.Fill(this.dS.SINHVIEN);

            makhoa = ((DataRowView)bdsLOP[0])["MAKH"].ToString();
            groupBox.Enabled = false;

        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnRecovery.Enabled = false;
            btnRepair.Enabled = false;
            vitri = bdsLOP.Position;
            groupBox.Enabled = true;
            bdsLOP.AddNew();
            txtMAKH.Text = makhoa;
            btnAdd.Enabled = btnDelete.Enabled = btnRepair.Enabled = btnRecovery.Enabled = false;
        }

        private void btnWrite_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            groupBox.Enabled = false;
            btnWrite.Enabled = false;
            if (txtTENLOP.Text.Trim() == "" || txtMALOP.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng điền đủ thông tin.!", "", MessageBoxButtons.OK);
                txtMALOP.Focus();

                if (txtTENLOP.Text.Trim() == "")
                {
                    MessageBox.Show("Tên lớp không được trống.!", "", MessageBoxButtons.OK);
                    txtTENLOP.Focus();
                    return;
                }
                else
                {
                    MessageBox.Show("Mã lớp không được trống.!", "", MessageBoxButtons.OK);
                    txtMALOP.Focus();
                    return;
                }

            }

            try
            {
                bdsLOP.EndEdit();
                bdsLOP.ResetCurrentItem();
                this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
                this.LOPTableAdapter.Update(this.dS.LOP);
                MessageBox.Show("Ghi thành công.!", "", MessageBoxButtons.OK);
                btnAdd.Enabled = btnDelete.Enabled = btnRepair.Enabled = btnRecovery.Enabled = true;
                groupBox.Enabled = false;
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi lớp.!", "", MessageBoxButtons.OK);
                return;
            }      
        }
        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnDelete.Enabled = false;
            if (bdsSINHVIEN.Count > 0)
            {
                MessageBox.Show("Lớp này không thể xóa vì đã có sinh viên.!", "", MessageBoxButtons.OK);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa lớp không?",
                    "Xác nhận.",
                   MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    bdsLOP.RemoveCurrent();
                    this.LOPTableAdapter.Update(this.dS.LOP);
                    MessageBox.Show("Xóa lớp thành công.!", "", MessageBoxButtons.OK);
                    btnDelete.Enabled = true;
                    groupBox.Enabled = false;
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xóa lớp thất bại.!", "", MessageBoxButtons.OK);
                    btnDelete.Enabled = true;
                    groupBox.Enabled = false;
                    return;
                }

            }
        }

        private void btnRepair_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnAdd.Enabled = false;
            btnRepair.Enabled = false;
            btnDelete.Enabled = false;
            groupBox.Enabled = true;
            vitri = bdsLOP.Position;
        }

        private void btnRecovery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bdsLOP.CancelEdit();
            bdsLOP.Position = vitri;
            groupBox.Enabled = false;
            gcLOP.Enabled = true;
        }

     
    }
}