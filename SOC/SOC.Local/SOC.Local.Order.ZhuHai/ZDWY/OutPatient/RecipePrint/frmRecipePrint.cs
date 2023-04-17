using System;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.RecipePrint
{
    public partial class frmRecipePrint : Form
    {
        public frmRecipePrint()
        {
            InitializeComponent();
        }

        public void AddPage(FS.HISFC.BizProcess.Interface.IRecipePrint recipe, int index)
        {
            TabPage page = new TabPage("处方" + index.ToString());

            ucRecipePrint uc = recipe as ucRecipePrint;

            uc.Dock = DockStyle.Fill;

            page.Controls.Add(uc);

            page.AutoScroll = true;

            uc.AutoScroll = true;

            this.tabControl1.TabPages.Add(page);
        }

        public int Pages
        {
            get
            {
                return this.tabControl1.TabPages.Count;
            }
        }

        private void btPrintCurrent_Click(object sender, EventArgs e)
        {
            if(this.tabControl1.TabPages.Count <= 0)
                return;

            FS.HISFC.BizProcess.Interface.IRecipePrint recipe = this.tabControl1.SelectedTab.Controls[0] as FS.HISFC.BizProcess.Interface.IRecipePrint;

            recipe.PrintRecipe();
        }

        private void btPrintAll_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.TabPages.Count <= 0)
                return;

            foreach (TabPage page in this.tabControl1.TabPages)
            {
                this.tabControl1.SelectedTab = page;

                this.btPrintCurrent_Click(null, null);
            }
        }

        public void ClearUC()
        {
            this.tabControl1.TabPages.Clear();
        }

        private void frmRecipePrint_Load(object sender, EventArgs e)
        {
            
        }
    }
}
