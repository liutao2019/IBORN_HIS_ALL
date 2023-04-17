using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.PacsBillPrint
{
    public partial class ucPacsBillPrintIBORN : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IInPatientOrderPrint
    {
        public ucPacsBillPrintIBORN()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        private void Clear()
        {
            lblName.Text = "";
            lblSex.Text = "";
            lblAge.Text = "";
            lblCardNo.Text = "";
            lblSeeDept.Text = "";
            labelPhoneAddr.Text = "";
            labelSeeDate.Text = "";
            lblDiag.Text = "";
            labelTotalPrice.Text = "";

        }

        #region IInPatientOrderPrint 成员
        public string ErrInfo
        {
            set { }
            get { return ""; }
        }
        public int PrintInPatientOrderBill(FS.HISFC.Models.RADT.PatientInfo patientInfo, string type, FS.FrameWork.Models.NeuObject deptNeuObject, FS.FrameWork.Models.NeuObject doctNeuObject, System.Collections.ArrayList alOrder, bool IsReprint)
        {
            this.Clear();

            this.SetPatientInfo(patientInfo);
            this.SetPrintValue(alOrder, IsReprint);

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

            int ig = 0;
            int iRow = 0;

            decimal totCost = 0m;
            this.fpSpreadItemsSheet.Cells[iRow, 0].Text = "辅助检查：";
            this.fpSpreadItemsSheet.Cells[iRow, 1].Text = "";
            foreach (FS.HISFC.Models.Order.Inpatient.Order order in orderList)
            {
                ig++;
                iRow++;

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
            print.SetPageSize(FS.SOC.Local.Order.ZhuHai.ZDWY.Function.GetPrintPage(false));
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

    }
}

