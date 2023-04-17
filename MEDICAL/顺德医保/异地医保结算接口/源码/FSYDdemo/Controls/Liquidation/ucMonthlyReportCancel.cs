using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FoShanYDSI.Controls.Liquidation
{
    public partial class ucMonthlyReportCancel : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucMonthlyReportCancel()
        {
            InitializeComponent();
        }

        FoShanYDSI.Business.InPatient.InPatientService prService = new FoShanYDSI.Business.InPatient.InPatientService();

        FoShanYDSI.Business.Common.YDStatReport reportMgr = new FoShanYDSI.Business.Common.YDStatReport();

        FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();

        private string settingFilePatch = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @"\Profile\YD\月度申报取消.xml";
 
        int year = 0;
        int month = 0;
        string type = string.Empty;

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("撤销", "撤销月度申报", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Y预览, true, false, null);

            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "撤销":
                    this.CancelApplyInfo();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override void OnLoad(EventArgs e)
        {
            ArrayList al = constMgr.GetList("YD140");

            this.cmbCustomType.AddItems(al);

            if (System.IO.File.Exists(this.settingFilePatch))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpTotal, this.settingFilePatch);
            }

            this.fpTotal.RowCount = 0;

            base.OnLoad(e);
        }

        //月度结算申请提交情况查询
        protected override int OnQuery(object sender, object neuObject)
        {
            year = this.dtMonth.Value.Date.Year;
            month = this.dtMonth.Value.Month;
            type = this.cmbCustomType.Tag.ToString();

            ArrayList al = reportMgr.QueryLiquidInfo(year.ToString(), month.ToString(), type);

            this.SetValueToFp(al);

            return 1;
        }

        private void SetValueToFp(ArrayList al)
        {
            this.fpTotal.RowCount = 0;

            int rowcount = 0;
            foreach (FS.FrameWork.Models.NeuFileInfo obj in al)
            {
                rowcount = this.fpTotal.RowCount;
                this.fpTotal.Rows.Add(rowcount, 1);
                this.fpTotal.Cells[rowcount, 0].Text = "";
                this.fpTotal.Cells[rowcount, 1].Text = this.dtMonth.Value.Date.Year.ToString();
                this.fpTotal.Cells[rowcount, 2].Text = this.dtMonth.Value.Month.ToString();
                this.fpTotal.Cells[rowcount, 3].Text = obj.ID;
                this.fpTotal.Cells[rowcount, 4].Text = obj.Name;
                this.fpTotal.Cells[rowcount, 5].Text = obj.Memo;
                this.fpTotal.Cells[rowcount, 6].Text = obj.User01;
                this.fpTotal.Cells[rowcount, 7].Text = obj.User02;
                this.fpTotal.Cells[rowcount, 8].Text = obj.User03;
                this.fpTotal.Cells[rowcount, 9].Text = obj.FilePath;
                this.fpTotal.Cells[rowcount, 10].Text = obj.FilePath;

                this.fpTotal.Rows[rowcount].Tag = obj;
            }
        
        }

        private void CancelApplyInfo()
        {
            ArrayList al = new ArrayList();
            FS.FrameWork.Models.NeuFileInfo obj = null;
            for (int i = 0; i < this.fpTotal.RowCount; i++)
            {
                if (this.fpTotal.Cells[i, 0].Value.ToString() == "True")
                {
                    obj = this.fpTotal.Rows[i].Tag as FS.FrameWork.Models.NeuFileInfo;
                    al.Add(obj);
                }
            }

            this.CancelApply(al);
        }


        private void CancelApply(ArrayList al)
        {
            //int rowIndex = this.fpTotal.ActiveRowIndex;
            //FS.FrameWork.Models.NeuFileInfo obj = this.fpTotal.Rows[rowIndex].Tag as FS.FrameWork.Models.NeuFileInfo;

            foreach (FS.FrameWork.Models.NeuFileInfo obj in al)
            {
                FS.FrameWork.Models.NeuObject tObj = new FS.FrameWork.Models.NeuObject();
                FS.FrameWork.Models.NeuFileInfo neuObj = new FS.FrameWork.Models.NeuFileInfo();
                neuObj.ID = this.year.ToString();
                neuObj.Name = this.month.ToString().PadLeft(2, '0');
                neuObj.Memo = obj.ID;
                neuObj.User01 = FS.FrameWork.Management.Connection.Operator.ID;
                neuObj.User02 = obj.Name;
                neuObj.User03 = new DateTime(this.year, this.month, 1).ToString();
                neuObj.FilePath = obj.FilePath;
                neuObj.FileFullPath = obj.FileFullPath;

                tObj.ID = this.year.ToString();
                tObj.Name = this.month.ToString().PadLeft(2, '0');
                tObj.Memo = obj.ID;
                tObj.User01 = FS.FrameWork.Management.Connection.Operator.ID;
                tObj.User02 = obj.Name;
                tObj.User03 = new DateTime(this.year, this.month, 1).ToString();

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                reportMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                int result = reportMgr.UpdateCancelLiquidInfo(tObj);

                if (result <= 0)
                {
                    MessageBox.Show("更新申报结算表出错！");
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return;
                }

                result = prService.MonthlyReportCancel(neuObj);

                if (result <= 0)
                {
                    MessageBox.Show("取消月度申请失败！");
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();

            }
            MessageBox.Show("取消月度申请成功！");
        }
    }
}
