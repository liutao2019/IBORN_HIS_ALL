using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.PacsBillPrint
{
    public partial class ucPacsBillPrintEndoscope : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {
        public ucPacsBillPrintEndoscope()
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

            this.chkOwn.Checked = false;
            this.chkPay.Checked = false;
            this.chkPub.Checked = false;
            this.chkOth.Checked = false;
        }

        #region IOutPatientOrderPrint 成员

        public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            this.Clear();
            this.label17.Text = orderList[0].Item.Extend2 + "申请单";
            this.SetPatientInfo(regObj);
            this.SetPrintValue(orderList, isPreview);

            return 1;
        }

        public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder, bool isPreview)
        {
            return 1;
        }

        public void PreviewOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
        }

        public void SetPage(string pageStr)
        {
        }

        #endregion

        /// <summary>
        /// 设置患者基本信息
        /// </summary>
        /// <param name="register"></param>
        public void SetPatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            this.labelTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name;
            // {FCA8E55A-6BAD-4ed7-9641-B01D188C07EB}
            //FS.HISFC.Models.Base.Department currDept = new FS.HISFC.Models.Base.Department();
            //currDept = (FS.HISFC.Models.Base.Department)(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept);
            //if (!string.IsNullOrEmpty(currDept.HospitalName))
            //{
            //    this.labelTitle.Text = currDept.HospitalName ;
            //}
            //else
            //{
            //    this.labelTitle.Text = "广州爱博恩妇产医院";
            //}
            if (register == null) return;

            this.lblName.Text = register.Name;
            this.lblAge.Text = this.diagManager.GetAge(register.Birthday, false);
            this.lblSex.Text = register.Sex.Name;
            this.lblCardNo.Text = register.PID.CardNO;
            this.labelPhoneAddr.Text = register.PhoneHome + (!string.IsNullOrEmpty(register.PhoneHome) && !string.IsNullOrEmpty(register.AddressHome) ? "/" : "") + register.AddressHome;
            lblPrintDate.Text = diagManager.GetDateTimeFromSysDateTime().ToString();

            if (register.Pact.PayKind.ID == "01")
            {
                this.chkOwn.Checked = true;
            }
            else if (register.Pact.PayKind.ID == "02")
            {
                this.chkPay.Checked = true;
            }
            else
            {
                this.chkPub.Checked = true;
            }

            #region 诊断
            ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(register.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
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

            this.npbBarCode.Image = FS.SOC.Public.Function.CreateBarCode(register.PID.CardNO, this.npbBarCode.Width, this.npbBarCode.Height);
        }

        /// <summary>
        /// 设置打印
        /// </summary>
        /// <param name="IList"></param>
        public void SetPrintValue(IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            if (orderList == null || orderList.Count <= 0)
            {
                return;
            }

            int ig = 0;
            int iRow = -1;
            orderList.GroupBy(o => o.Combo.ID).ToList().ForEach(g =>
            {
                ig++;

                g.OrderBy(s => s.SubCombNO)
                    .ToList().ForEach(order =>
                    {
                        iRow++;
                        //名称+备注
                        this.fpSpreadItemsSheet.Cells[iRow, 0].Text = "辅助检查：" + order.Item.Name + (order.IsEmergency ? "【急】" : "") + (string.IsNullOrEmpty(order.Memo) ? "" : "（备注:" + order.Memo + "）");

                        //检查部位
                        this.fpSpreadItemsSheet.Cells[iRow, 1].Text = "部位：" + order.CheckPartRecord;

                        iRow++;

                        //名称+备注
                        this.fpSpreadItemsSheet.Cells[iRow, 0].Text = "检查目的：";
                    });
            });
            if (iRow < fpSpreadItemsSheet.RowCount - 1)
            {
                fpSpreadItemsSheet.Cells[iRow + 1, 0].Text = "以下为空";
            }
            lblExecDept.Text = orderList[0].ExeDept.Name;

            this.lblSeeDept.Text = orderList[0].ReciptDept.Name;
            this.labelSeeDate.Text = orderList[0].MOTime.Date.ToString("yyyy.MM.dd");
            this.lblDocNo.Text = orderList[0].ReciptDoctor.ID + "(" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(orderList[0].ReciptDoctor.ID) + ")";
            this.labelTotalPrice.Text = FS.FrameWork.Public.String.ToSimpleString(orderList.Sum(x => x.FT.OwnCost + x.FT.PubCost + x.FT.PayCost)) + "元";

            if (!isPreview)
            {
                this.PrintPage();
            }
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

