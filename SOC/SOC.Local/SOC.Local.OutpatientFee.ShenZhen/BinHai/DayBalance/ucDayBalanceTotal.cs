using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.OutpatientFee.ShenZhen.BinHai.DayBalance
{
    public partial class ucDayBalanceTotal : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDayBalanceTotal()
        {
            InitializeComponent();
        }
      private string  DayBalance ="' '";
     public string strDayBalance
    {
                    get
            {
                return DayBalance;
            }
            set
            {
                this.DayBalance = value;
            }
}
       
        private ucCollectDayBalanceInfo CollectDayBalanceInfo = new ucCollectDayBalanceInfo();  
        private void btnbalance_Click(object sender, EventArgs e)
        {
           FS.FrameWork.WinForms.Classes.Function.ShowControl(CollectDayBalanceInfo);
           if (CollectDayBalanceInfo != null)
           {
               DayBalance = CollectDayBalanceInfo.BalaceNO;
           }
           else
           {
               DayBalance = "' '";
           }

        }

    }
}
