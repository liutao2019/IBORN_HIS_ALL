using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.PacsBillPrint
{
    public partial class ucPacsInformedBillPrintIBORNA4 : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IInPatientOrderPrint
    {
        public ucPacsInformedBillPrintIBORNA4()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        /// <summary>
        /// 常数控制类
        /// </summary>
        private static FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();

        private void Clear()
        {
            lblName.Text = "";
            lblSex.Text = "";
            this.lblBedNo.Text = "";
            lblAge.Text = "";
            lblCardNo.Text = "";
            lblSeeDept.Text = "";
            labelPhoneAddr.Text = "";
            labelSeeDate.Text = "";
            lblDiag.Text = "";
            labelTotalPrice.Text = "";
            rtbInfo.Text = "";

        }

        #region IInPatientOrderPrint 成员
        public string ErrInfo
        {
            set { }
            get { return ""; }
        }


        public int PrintInPatientOrderInformedBill(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, List<FS.HISFC.Models.Order.Inpatient.Order> orderList, bool IsReprint)
        {
            //this.Clear();

            //this.SetPatientInfo(patientInfo);
            //this.SetPrintValue(orderList, IsReprint);
            //if (patientInfo.SIMainInfo.User03 == "PACS打印")
            //{
            //    this.PrintPage();
            //}
            return 1;
        }


        public int PrintInPatientOrderBill(FS.HISFC.Models.RADT.PatientInfo patientInfo, string type, FS.FrameWork.Models.NeuObject deptNeuObject, FS.FrameWork.Models.NeuObject doctNeuObject, System.Collections.ArrayList alOrder, bool IsReprint)
        {
            this.Clear();

            this.SetPatientInfo(patientInfo);
            this.SetPrintValue(alOrder, IsReprint);
            //{A2ACD07E-03C1-4b5e-B6B1-7F8DE370C256}
            if (patientInfo.SIMainInfo.User03 == "PACS打印")
            {
                this.PrintPage();
            }
            return 1;
        }

        public int PrintInPatientOrderBill(FS.HISFC.Models.RADT.PatientInfo patientInfo, string type, FS.FrameWork.Models.NeuObject deptNeuObject, FS.FrameWork.Models.NeuObject doctNeuObject, IList<FS.HISFC.Models.Order.Order> IList, bool IsReprint)
        {
            return 1;
        }
        #endregion

        public int LengthString(string str)
        {
            if (str == null || str.Length == 0) { return 0; }

            int l = str.Length;
            int realLen = l;

            #region 计算长度
            int clen = 0;//当前长度
            while (clen < l)
            {
                //每遇到一个中文，则将实际长度加一。
                if ((int)str[clen] > 128) { realLen++; }
                clen++;
            }
            #endregion

            return realLen;
        }
        /// <summary>
        /// 设置患者基本信息
        /// </summary>
        /// <param name="register"></param>
        public void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.labelTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name;


            if (patientInfo == null) return;

            this.lblName.Text = patientInfo.Name;
            this.lblName1.Text = patientInfo.Name;

            int strLength = this.LengthString(patientInfo.Name);
            if (strLength > 16)
            {
                this.lblName.Visible = true;
                this.lblName1.Visible = false;
            }
            else
            {

                this.lblName.Visible = false;
                this.lblName1.Visible = true;
            }
            this.lblAge.Text = this.diagManager.GetAge(patientInfo.Birthday, false);
            this.lblSex.Text = patientInfo.Sex.Name;
            lblBedNo.Text = patientInfo.PVisit.PatientLocation.Bed.ID.Length > 4 ? patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) : patientInfo.PVisit.PatientLocation.Bed.ID;
            this.lblCardNo.Text = patientInfo.PID.PatientNO;
            this.labelPhoneAddr.Text = patientInfo.AddressHome + (!string.IsNullOrEmpty(patientInfo.PhoneHome) && !string.IsNullOrEmpty(patientInfo.AddressHome) ? "/" : "") + patientInfo.PhoneHome;
            lblPrintDate.Text = diagManager.GetDateTimeFromSysDateTime().ToString();

            if (Regex.IsMatch(patientInfo.PID.PatientNO, @"^[+-]?\d*[.]?\d*$"))
            {
                this.npbRecipeNo.Image = SOC.Public.Function.CreateBarCode(patientInfo.PID.PatientNO, this.npbRecipeNo.Width, this.npbRecipeNo.Height);
            }
            else
            {
                this.labelRecipeNumber.Visible = false;
                this.npbRecipeNo.Visible = false;
            }

            #region 诊断
            ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(patientInfo.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (al == null) return;

            string strDiag = "";
            int i = 1;
            foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
            {
                if (diag != null && diag.IsValid)
                {
                    strDiag += i.ToString() + "、" + diag.DiagInfo.ICD10.Name + "； " + "";
                    i++;
                    lblDiag.Text = strDiag;
                }
            }
            #endregion

        }


        private void SetInforme(string docid)
        {
            if (string.IsNullOrEmpty(docid))
            {
                rtbInfo.Text = "";
            }
            else
            {
                FS.FrameWork.Models.NeuObject obj = constManager.GetConstant("PSCSDOC", docid);

                if (obj != null && !string.IsNullOrEmpty(obj.ID))
                {
                    rtbInfo.Text = obj.Memo;
                }
                else
                {
                    rtbInfo.Text = "";
                }
            }
        }

        /// <summary>
        /// 设置打印
        /// </summary>
        /// <param name="IList"></param>
        public void SetPrintValue(ArrayList orderList, bool IsReprint)
        {
            if (orderList == null || orderList.Count <= 0)
            {
                return;
            }

            FS.HISFC.Models.Order.Inpatient.Order ood = orderList[0] as FS.HISFC.Models.Order.Inpatient.Order;

            SetInforme(ood.RefundReason);

            int ig = 0;
            int iRow = 0;

            decimal totCost = 0m;
            this.fpSpreadItemsSheet.Cells[iRow, 0].Text = "辅助检查：";
            this.fpSpreadItemsSheet.Cells[iRow, 1].Text = "";
            foreach (FS.HISFC.Models.Order.Inpatient.Order order in orderList)
            {
                ig++;
                iRow++;
                this.fpSpreadItemsSheet.Rows[iRow].Height = 18;


                this.fpSpreadItemsSheet.Cells[iRow, 0].Text = ig + "、" + order.Item.Name + (order.IsEmergency ? "【急】" : "") + "   ×" + order.Qty.ToString();

                //检查部位
                this.fpSpreadItemsSheet.Cells[iRow, 1].Text = (order.Item.Price * order.Qty).ToString() + "元";
                iRow++;
                this.fpSpreadItemsSheet.Cells[iRow, 0].Text = (string.IsNullOrEmpty(order.Memo) ? "" : "（备注:" + order.Memo + "）") + (string.IsNullOrEmpty(order.CheckPartRecord) ? "" : " 部位：" + order.CheckPartRecord);
                this.fpSpreadItemsSheet.Cells[iRow, 0].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular);

                totCost += order.Qty * order.Item.Price;
            }

            if (iRow < fpSpreadItemsSheet.RowCount - 1)
            {
                fpSpreadItemsSheet.Cells[iRow + 1, 0].Text = "(以下为空)";
            }
            FS.HISFC.Models.Order.Inpatient.Order order1 = new FS.HISFC.Models.Order.Inpatient.Order();
            order1 = orderList[0] as FS.HISFC.Models.Order.Inpatient.Order;

            lblExecDept.Text = order1.ExeDept.Name;

            this.lblSeeDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(order1.ReciptDept.ID);
            this.labelSeeDate.Text = order1.MOTime.ToString();
            //this.lblDocNo.Text = orderList[0].ReciptDoctor.ID + "(" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(orderList[0].ReciptDoctor.ID) + ")";
            this.lblDocNo.Text = order1.ReciptDoctor.ID.Substring(2, order1.ReciptDoctor.ID.Length - 2);
            this.lblDocName.Text = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(order1.ReciptDoctor.ID);

            this.labelTotalPrice.Text = totCost.ToString("F2") + "元";

        }

        /// <summary>
        /// 打印
        /// </summary>
        private void PrintPage()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(this.GetPrintPage(false));
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            if (FS.SOC.Local.Order.ZhuHai.ZDWY.Function.IsPreview())
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }

        /// <summary>
        /// {C08A3A99-5BAD-4b18-9D05-899D0F1642EE}
        /// </summary>
        /// <param name="isLandScape"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.PageSize GetPrintPage(bool isLandScape)
        {
            FS.HISFC.BizLogic.Manager.PageSize pageManager = new FS.HISFC.BizLogic.Manager.PageSize();


            FS.HISFC.Models.Base.PageSize pageSize = null;
            if (isLandScape)
            {
                // pageSize = pageManager.GetPageSize("RecipeLand");

                if (pageSize == null)
                {
                    //pageSize = new FS.HISFC.Models.Base.PageSize("A5", 895, 579);
                    pageSize = new FS.HISFC.Models.Base.PageSize("A4", 1140, 830);
                }

                return pageSize;

            }

            if (pageSize == null)
            {
                //pageSize = new FS.HISFC.Models.Base.PageSize("A5", 579, 895);
                pageSize = new FS.HISFC.Models.Base.PageSize("A4", 830, 1140);
            }

            return pageSize;
        }

    }
}

