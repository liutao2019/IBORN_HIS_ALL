using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace SOC.Local.Pharmacy.ZhuHai.Print.ZDWY
{
    public partial class ucPrintBillTot : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPrintBillTot()
        {
            InitializeComponent();
        }

        private FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        public ucPrintBillTot(decimal alpurhanceCost, decimal alspecialpurhanceCost, Hashtable hsDrugType, int totPage,ArrayList allDrugType,bool isNeedPrintPrew)
        {
            InitializeComponent();
            this.Clear();
            this.SetData(alpurhanceCost, alspecialpurhanceCost, hsDrugType, totPage, allDrugType, isNeedPrintPrew);
        }

        private void SetData(decimal alpurhanceCost, decimal alspecialpurhanceCost, Hashtable hsDrugType, int totPage,ArrayList allDrugType,bool isNeedPrintPrew)
        {
            DateTime dtSysDate = consMgr.GetDateTimeFromSysDateTime();
            string memo = string.Empty;
            if(hsDrugType!= null && hsDrugType.Count >0)
            {
               for(int index = 0;index < allDrugType.Count;index++)
               {
                   memo += consMgr.GetConstant("ITEMTYPE", allDrugType[index].ToString()) + "预购：" + hsDrugType[allDrugType[index].ToString()] + "；";
               }
            }
            this.nlbTitle.Location = new Point((this.Width - this.nlbTitle.Width)/2, this.nlbTitle.Location.Y);
            this.nlbYear.Text = dtSysDate.Year.ToString();
            this.nlbTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name;// {7DBB85BF-547C-4230-8598-55A2A4AD83F4}
            this.nblYear1.Text = "年";
            this.nlbMonth1.Text = "月";
            this.nlbMonth.Text = dtSysDate.Month.ToString();
            this.nlbPageNO.Text = "（附" + totPage + "页）";
            this.nlbPrePurCost.Text = "预购金额：" + alpurhanceCost.ToString("F2");
            this.nlbContain.Text = "(其中腹透液及碘帽预购金额为" + alspecialpurhanceCost.ToString("F2") +"，其余为药品预购金额。" + memo.TrimEnd('；') + "。";
            this.Print(isNeedPrintPrew);
        }

        private void Print(bool isNeedPrintPrew)
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            p.IsHaveGrid = true;
            p.SetPageSize(new FS.HISFC.Models.Base.PageSize("planTot",870,1100));
            if (isNeedPrintPrew)
            {
                p.PrintPreview(5, 0, this.neuPanelMain);
            }
            else
            {
                p.PrintPage(5, 0, this.neuPanelMain);
            }
        }

        private void Clear()
        {
            this.nlbPrePurCost.Text = string.Empty;
            this.nlbContain.Text = string.Empty;
            this.nlbYear.Text = string.Empty;
            this.nlbMonth.Text = string.Empty;
            this.nlbPageNO.Text = string.Empty;
        }
    }
}
