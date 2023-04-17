using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Order.GuangZhou.ZDLY.CircuitControl
{
    public partial class ucCircuitCardPrint : UserControl
    {

        public ucCircuitCardPrint()
        {
            InitializeComponent();
        }

        private ArrayList alOrders = new ArrayList();

        public ArrayList AlOrders
        {
            set
            {
                this.alOrders = value;
                this.SetItems();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetItems()//构造预览页面内容
        {
            string AuthorName = ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).Name;
            this.lblAuthor.Text =  AuthorName;

            this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.Rows.Count);
            foreach (string s in alOrders)
            {
                string[] items = s.Split('|');

                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);//在页尾往后增加1行

                for (int i = 0; i < items.Length - 1; i++)
                {
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, i].Text = items[i];
                }
            }


            #region 内容

            //只显示下面的边框  bevelBorder1---普通线   bevelBorder2---┏   bevelBorder3---┃ bevelBorder4---┗ 
            FarPoint.Win.BevelBorder bevelBorder1 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 1, true, true, true, true);

            FarPoint.Win.BevelBorder bevelBorder2 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 1, true, true, true, false);

            FarPoint.Win.BevelBorder bevelBorder3 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 1, true, false, true, false);

            FarPoint.Win.BevelBorder bevelBorder4 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 1, true, false, true, true);

            if (this.neuSpread1_Sheet1.Rows.Count > 0)//有数据
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    if (this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ComboMemo].Text.Trim() == "┏")//组内用空白线
                    {
                        this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder2;
                        this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ComboMemo].Border = bevelBorder2;
                    }
                    else if (this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ComboMemo].Text.Trim() == "┃")//不同组---普通线
                    {
                        this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder3;
                        this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ComboMemo].Border = bevelBorder3;

                        this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.FrequencyID].Text = string.Empty;
                        this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.Memo].Text = string.Empty;

                    }
                    else if (this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ComboMemo].Text.Trim() == "┗")//不同组---普通线
                    {
                        this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder4;
                        this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ComboMemo].Border = bevelBorder4;

                        this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.FrequencyID].Text = string.Empty;
                        this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.Memo].Text = string.Empty;

                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder1;
                        this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ComboMemo].Border = bevelBorder1;
                    }
                }
            }

            #endregion
        }

        /// <summary>
        /// 设置人员基本信息
        /// </summary>
        /// <param name="patientInfo"></param>
        public void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.lblDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(patientInfo.PVisit.PatientLocation.NurseCell.ID);
            this.lblBedNo.Text = patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4);
            this.lblPatientNo.Text = patientInfo.PID.PatientNO.TrimStart('0');
            this.lblName.Text = patientInfo.Name;
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="title"></param>
        /// <param name="titleDate"></param>
        /// <param name="printTime"></param>
        /// <param name="page"></param>
        public void SetHeader(string title, string titleDate, string printTime, int currentPage, int totalPag)
        {
            this.lblTitle.Text = title;
            this.neuLblExecTime.Text = "执行时间:" + titleDate;
            this.neuLblPrintTime.Text = printTime;
            this.lblPage.Text = "第" + currentPage.ToString() + "页 共" + totalPag.ToString() + "页";
        }
    }    
}
