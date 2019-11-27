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
    public partial class Subject : DevExpress.XtraEditors.XtraForm
    {
        int vitri = 0;
        public Subject()
        {
            InitializeComponent();
        }

        private void mONHOCBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsMONHOC.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dS);

        }

        private void Subject_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dS.MONHOC' table. You can move, or remove it, as needed.
            this.MONHOCTableAdapter.Fill(this.dS.MONHOC);
            groupBox1.Enabled = false;

        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            groupBox1.Enabled = true;
            btnDelete.Enabled = false;
            btnRepair.Enabled = false;
            btnAdd.Enabled = false;
            btnExit.Enabled = false;
            vitri = bdsMONHOC.Position;
            bdsMONHOC.AddNew();

        }

        private void btnWrite_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            groupBox1.Enabled = false;
            btnWrite.Enabled = false;

            if (txtMAMH.Text.Trim() == "" || txtTENMH.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng điền đủ thông tin.!", "", MessageBoxButtons.OK);
                if(txtMAMH.Text.Trim() == "")
                {
                    txtMAMH.Focus();
                } else
                {
                    txtTENMH.Focus();
                }
            }
            else
            {
                try
                {
                    bdsMONHOC.EndEdit();
                    bdsMONHOC.ResetCurrentItem();
                    this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.MONHOCTableAdapter.Update(this.dS.MONHOC);
                    MessageBox.Show("Ghi thành công.!", "", MessageBoxButtons.OK);
                    btnDelete.Enabled = true;
                    btnRepair.Enabled = true;
                    btnAdd.Enabled = true;
                    btnExit.Enabled = true;
                    return;
                } catch(Exception ex)
                {
                    MessageBox.Show("Ghi không thành công.!", "", MessageBoxButtons.OK);
                    return;

                }
            }
        }
    }
}