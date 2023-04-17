using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;

namespace FS.HISFC.Components.Manager.Items
{
    public partial class ucUnDrugItemZTPrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucUnDrugItemZTPrint()
        {
            InitializeComponent();
        }

        public void SetValue(List<FS.HISFC.Models.Fee.Item.UndrugComb> list)
        {
            for (int i=0;i<list.Count;i++)
            {
                this.neuSpread1_Sheet1.Rows.Add(i, 1);
                FS.HISFC.Models.Fee.Item.UndrugComb itemObj=list[i] as FS.HISFC.Models.Fee.Item.UndrugComb;
                this.neuSpread1_Sheet1.Cells[i, 0].Text = itemObj.Package.Name;
                this.neuSpread1_Sheet1.Cells[i, 1].Text = itemObj.Name;
                this.neuSpread1_Sheet1.Cells[i, 2].Text = itemObj.Qty.ToString();
                this.neuSpread1_Sheet1.Cells[i, 3].Text = itemObj.Price.ToString();
                this.neuSpread1_Sheet1.Cells[i, 4].Text = itemObj.ChildPrice.ToString();
                this.neuSpread1_Sheet1.Cells[i, 5].Text = itemObj.SpecialPrice.ToString();
            }
        }

        public int Print()
        {
            FS.HISFC.BizLogic.Manager.Constant cons = new FS.HISFC.BizLogic.Manager.Constant();
            FarPoint.Win.Spread.PrintInfo pi = new FarPoint.Win.Spread.PrintInfo();
            FarPoint.Win.Spread.PrintMargin pm = new FarPoint.Win.Spread.PrintMargin();
            //题头
            //pi.Header = " /n /n/c/fz\"12\"/fb1" + "无锡医院/n/c治疗单/n/n /n /n";
            //题头
            pi.Header = " /n /n/fz\"16\"/fb1" + "               复合项目明细单/n/fb0/n"
                + "/l/fz\"10\"" + "日期:" + cons.GetDateTimeFromSysDateTime().Date.ToShortDateString() + "/n  ";// +
            //"/l/fz\"10\"" + "药库类别:" + str_StockDept_Type + "出库类型:" + str_OutPut_Type + "单据编号:" + info.OutListNO + "/n" +
            //"/l/fz\"10\"" + "领药单位:" + strCompany + "出库日期:" + str_OutPut_Date + "打印日期:" + this.psMgr.GetSysDateTime("yyyy.MM.dd") + "   第/p页//共/pc页 /n /n";
            //设置打印
            pi.ShowColumnHeaders = true;
            pi.ShowRowHeaders = false;
            pi.ShowBorder = false;
            pi.ShowColor = true;
            pi.ShowGrid = true;
            pi.ShowShadows = true;
            pi.UseMax = true;

            pm.Right = 0;
            pm.Top = 0;
            pm.Bottom = 40;
            pm.Left = 80;

            pi.Margin = pm;
            //pi.PaperSize = new System.Drawing.Printing.PaperSize("ZLD", 800, 460);
            pi.Orientation = FarPoint.Win.Spread.PrintOrientation.Portrait;
            this.neuSpread1.Sheets[0].PrintInfo = pi;
            //this.neuSpread1.Sheets[0].PrintInfo.Preview = true;
            //this.neuSpread1.PrintSheet(0);
            try
            {
                this.Print(new HandleRef(this.neuSpread1, this.neuSpread1.Handle), this.neuSpread1.Sheets[0]);
            }
            catch (Exception ee)
            { }
            return 1;
        }
        public void Print(HandleRef hldRef, FarPoint.Win.Spread.SheetView sv)
        {
            FarPoint.Win.Spread.FpSpread fs;
            object obj = hldRef.Wrapper;
            fs = obj as FarPoint.Win.Spread.FpSpread;
            if (fs == null)
                return;
            fs.PrintSheet(sv);
        }

    }
}
