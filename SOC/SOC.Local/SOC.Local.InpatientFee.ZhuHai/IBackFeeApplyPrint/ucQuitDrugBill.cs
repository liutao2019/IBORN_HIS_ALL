using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.ZhuHai.IBackFeeApplyPrint
{
    /// <summary>
    /// 退费申请单
    /// </summary>
    public partial class ucQuitDrugBill : UserControl, FS.HISFC.BizProcess.Interface.FeeInterface.IBackFeeApplyPrint
    {
        /// <summary>
        /// 退费申请单构造函数
        /// </summary>
        public ucQuitDrugBill()
        {
            InitializeComponent();
            unDrugList.AddRange(new string[] { ""});
        }
        FS.HISFC.Models.RADT.PatientInfo patient;        
         /// <summary>
        /// 非药品
        /// </summary>
        List<string> unDrugList = new List<string>();

        private ArrayList allBackFee = new ArrayList();

        FS.HISFC.BizLogic.Manager.PageSize pageSet = new FS.HISFC.BizLogic.Manager.PageSize();

        //{BA2EDF81-FCD5-417b-814C-F5AEF336D24E}
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParam = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        private int maxPageCount = 12;
       
        bool isReprint = false;
        #region IBackFeeRecipePrint 成员
        /// <summary>
        /// 是否补打
        /// </summary>
        public bool IsRePrint
        {
            get { return isReprint; }
            set { isReprint = value; }
        }
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            set
            {
                patient = value;
            }

        }
        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            this.SetDatadetail(this.allBackFee); 
        }

        public void PrintPage()
        {
            // {D59C1D74-CE65-424a-9CB3-7F9174664504}
            string printerName = FS.HISFC.Components.Common.Classes.Function.ChoosePrinter();
            if (string.IsNullOrEmpty(printerName))
            {
                return;
            }
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
         
            FS.HISFC.Models.Base.PageSize ps = pageSet.GetPageSize("QuitDrugBill");
            if (ps == null)
            {
                //默认大小
                //{90671BD6-B853-4703-B908-ED7E1FF1B2BF}
                ps = new FS.HISFC.Models.Base.PageSize("QuitDrugBill", 810, 1100);
                ps.Left = 35;
            }
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.SetPageSize(ps);

            //print.IsLandScape = true;// {D59C1D74-CE65-424a-9CB3-7F9174664504}

            print.PrintDocument.PrinterSettings.PrinterName = printerName;
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(ps.Left, ps.Top, this);
            }
            else
            {
                print.PrintPage(ps.Left, ps.Top, this);
            }
        }

        public int SetData(System.Collections.ArrayList alBackFee)
        {
            this.allBackFee = alBackFee.Clone() as ArrayList;
            return 1;
        }

        public int SetDatadetail(System.Collections.ArrayList alBackFee)
        {
            this.lbRePrint.Visible = this.IsRePrint;     //是否补打
            this.lbName.Text = "姓名:" + patient.Name;
            this.lbCardNo.Text = "住院号:" + patient.PID.PatientNO;
            this.lblBedNO.Text = "床号:" + patient.PVisit.PatientLocation.Bed.ID.Substring(4);
            this.lbSex.Text = "性别:" + patient.Sex.Name;
            this.labArea.Text = "病区：" + patient.PVisit.PatientLocation.NurseCell;   //病区
            this.labPrintDate.Text = "打印时间:" + DateTime.Now.ToString();  //打印时间

            #region 退药原因
            FS.HISFC.Models.Fee.Inpatient.FeeItemList info = alBackFee[0] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
            if (!string.IsNullOrEmpty(info.Item.Memo))
            {
                this.labReason.Text = "退费原因:" + info.Item.Memo;
            }
            else
            {
                this.labReason.Text = "退费原因:" + this.GetQuitDrugReason(info.RecipeNO, info.SequenceNO);
            }
            #endregion

            this.neuSpread1_Sheet1.Rows.Count = 0;


            decimal totCount = 0;
            decimal totCost = 0m;
            decimal curPageNo = 1;
            decimal totPage = Math.Ceiling(FS.FrameWork.Function.NConvert.ToDecimal((double)alBackFee.Count / (double)maxPageCount));

            int index = 0;

            //{BA2EDF81-FCD5-417b-814C-F5AEF336D24E}
            try
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList tmpFee = (FS.HISFC.Models.Fee.Inpatient.FeeItemList)alBackFee[0];
                if (this.controlParam.GetControlParam("P01019", false, "").Contains(tmpFee.ExecOper.Dept.ID))
                {
                    this.labArea.Text = "病区：" + HISFC.BizProcess.Cache.Common.GetDept(tmpFee.ExecOper.Dept.ID).Name;   //病区
                }
            }
            catch { }



            for (int i = 0; i < alBackFee.Count; i++)
            {
                index++;

                if (index % 12 == 0)
                {
                    this.PrintPage();
                    index = 1;
                    curPageNo++;
                    this.Clear();
                }

                this.nlbPageNO.Text = "第" + curPageNo + "页，共" + totPage + "页";
                
                FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = (FS.HISFC.Models.Fee.Inpatient.FeeItemList)alBackFee[i];

                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);

                this.neuSpread1_Sheet1.Cells[index -1 , 0].Text = feeItemList.User03;//申请日期

            
                if (feeItemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    this.neuPanName.Text = "病区退药单" +"(" + feeItemList.User02 + ")";
                    this.neuSpread1_Sheet1.Cells[index -1, 1].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(feeItemList.Item.ID);//自定义码
                    this.neuSpread1_Sheet1.Cells[index -1, 3].Text = feeItemList.Item.Specs.ToString();//规格
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[index -1, 1].Text = SOC.HISFC.BizProcess.Cache.Fee.GetItemUserCode(feeItemList.Item.ID);//自定义码
                    this.neuPanName.Text = "住院退费申请单";
                }

                this.neuSpread1_Sheet1.Cells[index -1, 2].Text = feeItemList.Item.Name.ToString();

                this.neuSpread1_Sheet1.Cells[index - 1, 4].Text = feeItemList.Item.Qty.ToString();
                this.neuSpread1_Sheet1.Cells[index - 1, 5].Text = feeItemList.Item.PriceUnit;
               
                this.neuSpread1_Sheet1.Cells[index - 1, 6].Text = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Qty / feeItemList.Item.PackQty * feeItemList.Item.Price, 2).ToString();

                totCost += FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Qty / feeItemList.Item.PackQty * feeItemList.Item.Price, 2);
                
                totCount += feeItemList.Item.Qty;

                if (curPageNo == totPage)
                {
                    this.nlbTotLabel.Visible = true;
                    //this.nlbTotCount.Visible = true;
                    this.nlbTotCost.Visible = true;
                    this.nlbTotLabel.Text = "合计:";
                    this.nlbTotCost.Text = totCost.ToString("F2") + "元";
                    this.nlbTotCount.Text = totCount.ToString("F0");
                }
            }

            this.SetDockStyle();

            this.PrintPage();
           
            return 1;
        }

        private string GetQuitDrugReason(string recipeNO, int sequenceNO)
        {
            string strSql = @"select bbb.mark
         from pha_com_applyout bbb where bbb.recipe_no = '{0}' and bbb.sequence_no = {1} and bbb.valid_state = '1' and  bbb.class3_meaning_code = 'Z2' and rownum = 1
         ";
            strSql = string.Format(strSql, recipeNO, sequenceNO);
            return pageSet.ExecSqlReturnOne(strSql, "");
        }

        private void SetDockStyle()
        {
            this.neuSpread1.Dock = DockStyle.None;
            this.panel4.Dock = DockStyle.None;
            this.panel5.Dock = DockStyle.None;
            this.panel4.Height = (int)(this.neuSpread1_Sheet1.RowHeader.Rows[0].Height + this.neuSpread1_Sheet1.Rows.Count * this.neuSpread1_Sheet1.Rows.Default.Height);
            this.panel5.Height = this.Height - this.panel2.Height - this.panel4.Height;
            this.panel5.Dock = DockStyle.Bottom;
            this.panel4.Dock = DockStyle.Fill;
            this.neuSpread1.Dock = DockStyle.Fill;
        }

        private void Clear()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
            this.nlbTotLabel.Visible = false;
            this.nlbTotCount.Visible = false;
            this.nlbTotCost.Visible = false;
        }

        #endregion
    }
}
