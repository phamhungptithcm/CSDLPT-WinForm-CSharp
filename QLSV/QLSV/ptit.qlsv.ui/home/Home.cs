using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV.ptit.qlsv.ui.home
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            cbbKHOA.DataSource = Program.bds_dspm;//sao chep bds
            cbbKHOA.DisplayMember = "TENKHOA";
            cbbKHOA.ValueMember = "TENSERVER";
            cbbKHOA.SelectedIndex = Program.mChinhanh;
            cbbKHOA.Enabled = true;

        }

        private void btnPoint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

           

        }

        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }


        private void btnClass_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            Form fm = this.CheckExists(typeof(Class));
            if(fm == null)
            {
                Class classForm = new Class();
                classForm.MdiParent = this;
                classForm.Show();
            } else
            {
                fm.Activate();
            }
        }

       
    }
}
