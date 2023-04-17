using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.RecipePrint.Controls
{
    public partial class ucTitleBase : UserControl, RecipePrint.Interface.ITitle
    {
        public ucTitleBase()
        {
            InitializeComponent();
        }

        public int SetTitleInfo(FS.HISFC.Models.Registration.Register regObj, string recipeNo, string speRecipeFlag)
        {
            lblSpeFlag.Visible = false;
            if (!string.IsNullOrEmpty(speRecipeFlag))
            {
                lblSpeFlag.Text = speRecipeFlag;
                lblSpeFlag.Visible = true;
            }

            npbRecipeNo.Image = SOC.Public.Function.CreateBarCode(recipeNo, this.npbRecipeNo.Width, this.npbRecipeNo.Height);

            npbBarCode.Image = SOC.Public.Function.CreateBarCode(regObj.PID.CardNO, this.npbBarCode.Width, this.npbBarCode.Height);


            this.chkOwn.Checked = false;
            this.chkPay.Checked = false;
            this.chkPub.Checked = false;
            this.chkOth.Checked = false;

            if (regObj.Pact.PayKind.ID == "01")
            {
                this.chkOwn.Checked = true;
            }
            else if (regObj.Pact.PayKind.ID == "02")
            {
                this.chkPay.Checked = true;
            }
            else if (regObj.Pact.PayKind.ID == "03")
            {
                chkPub.Checked = true;
            }
            else
            {
                this.chkOth.Checked = true;
            }

            return 1;
        }
    }
}
