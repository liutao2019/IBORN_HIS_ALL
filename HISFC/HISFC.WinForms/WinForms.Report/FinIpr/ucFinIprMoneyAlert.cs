using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.FinIpr
{
    public partial class ucFinIprMoneyAlert : Report.Common.ucQueryBaseForDataWindow
    {
        public ucFinIprMoneyAlert()
        {
            InitializeComponent();
        }
        private string pactCode = string.Empty;
        private string pactName = string.Empty;
        System.Collections.ArrayList constantList = null;

        protected override void OnLoad()
        {
            this.Init();

            base.OnLoad();
            //�������
            FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            constantList = manager.GetConstantList("PACTUNIT");

            FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();
            con.ID = "ALL";
            con.Name = "ȫ��";
            con.SpellCode = "QB";
            cboPactCode.Items.Insert(0, con);
            constantList.Insert(0, con);
            this.cboPactCode.AddItems(constantList);
            cboPactCode.SelectedIndex = 0;
            pactCode = ((FS.HISFC.Models.Base.Const)constantList[0]).ID;
            pactName = ((FS.HISFC.Models.Base.Const)constantList[0]).Name;

        }
        protected override int OnRetrieve(params object[] objects)
        {
            return base.OnRetrieve(this.neuTextBox1.Text, pactName.ToString());
            //return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, pactName.ToString());
        }

        private void cboPactCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPactCode.SelectedIndex >= 0)
            {

                pactCode = ((FS.HISFC.Models.Base.Const)constantList[this.cboPactCode.SelectedIndex]).ID;
                pactName = ((FS.HISFC.Models.Base.Const)constantList[this.cboPactCode.SelectedIndex]).Name;
            }
        }

        private void neuTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.OnRetrieve();
            }
        }


        //private void neuTextBox1_TextChanged(object sender, EventArgs e)
        //{
             
        //}
    }
}
