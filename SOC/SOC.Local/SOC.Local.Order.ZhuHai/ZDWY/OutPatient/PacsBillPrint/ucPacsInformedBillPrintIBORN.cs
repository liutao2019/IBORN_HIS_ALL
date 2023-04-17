using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.PacsBillPrint
{
    public partial class ucPacsInformedBillPrintIBORN : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {
        public ucPacsInformedBillPrintIBORN()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
        /// <summary>
        /// 医嘱扩展信息管理{97B9173B-834D-49a1-936D-E4D04F98E4BA}
        /// </summary>
        FS.HISFC.BizLogic.Order.OutPatient.OrderExtend orderExtMgr = new FS.HISFC.BizLogic.Order.OutPatient.OrderExtend();

        private FS.HISFC.Models.Registration.Register reg;
        /// <summary>
        /// 常数控制类
        /// </summary>
        private static FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();

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

        #region IOutPatientOrderPrint 成员

        public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            this.Clear();

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
        public void SetPatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            //{EB62A9BA-6EB5-46c7-B7E2-96C530D1B305}
            #region
            //this.labelTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name;


            // {FCA8E55A-6BAD-4ed7-9641-B01D188C07EB}
            FS.HISFC.Models.Base.Department currDept = new FS.HISFC.Models.Base.Department();
            currDept = (FS.HISFC.Models.Base.Department)(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept);
            if (!string.IsNullOrEmpty(currDept.HospitalName))
            {
                this.labelTitle.Text = currDept.HospitalName + "检查申请单";
            }
            else
            {
                this.labelTitle.Text = "广州爱博恩妇产医院检查申请单";
            }
            #endregion

            if (register == null) return;
            this.reg = register;

            this.lblName.Text = register.Name;
            this.lblName1.Text = register.Name;

            int strLength = this.LengthString(register.Name);
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
            this.lblAge.Text = this.diagManager.GetAge(register.Birthday, false);
            this.lblSex.Text = register.Sex.Name;
            this.lblCardNo.Text = register.PID.CardNO;
            this.labelPhoneAddr.Text = (string.IsNullOrEmpty(register.PhoneHome) ? register.AddressHome : register.PhoneHome) + "/" + register.AddressHome;// (!string.IsNullOrEmpty(register.PhoneHome) && !string.IsNullOrEmpty(register.AddressHome) ? "" : "") + register.PhoneHome + "/" + register.AddressHome;
            lblPrintDate.Text = diagManager.GetDateTimeFromSysDateTime().ToString();


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

            #region 主诉、现病史
            FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory caseHistory = new FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory();
            caseHistory = FS.SOC.Local.Order.ZhuHai.Classes.CacheManager.OutOrderMgr.QueryCaseHistoryByClinicCode(register.ID);

            this.lblCaseMain.Text = caseHistory.CaseMain;
            this.lblCaseNow.Text = caseHistory.CaseNow;

            #endregion
        }


        private void SetInforme(string docid)
        {
            if (string.IsNullOrEmpty(docid))
            {
                this.lblInfo.Text = "";
            }
            else
            {
                FS.FrameWork.Models.NeuObject obj = constManager.GetConstant("PSCSDOC", docid);

                if (obj != null && !string.IsNullOrEmpty(obj.ID))
                {
                    this.lblInfo.Text = obj.Memo;
                }
                else
                {
                    this.lblInfo.Text = "";
                }
            }
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
            //借用这个属性RefundReason，代表是否是含有知情同意书的

            SetInforme(orderList[0].RefundReason);



            int ig = 0;
            int iRow = 0;

            this.fpSpreadItemsSheet.Cells[iRow, 0].Text = "辅助检查：";
            this.fpSpreadItemsSheet.Cells[iRow, 1].Text = "";
            orderList.GroupBy(o => o.Combo.ID).ToList().ForEach(g =>
            {
                ig++;

                g.OrderBy(s => s.SubCombNO)
                    .ToList().ForEach(order =>
                    {
                        iRow++;
                        //名称+备注
                        string memo = string.Empty;// {3FF25BA5-1251-4dbb-8895-F94D8FF26BAB}
                        memo = (string.IsNullOrEmpty(order.Memo) ? "" : "（备注:" + order.Memo + "）") + (string.IsNullOrEmpty(order.CheckPartRecord) ? "" : " 部位：" + order.CheckPartRecord);
                        this.fpSpreadItemsSheet.Cells[iRow, 0].Text = ig + "、" + order.Item.Name + (order.IsEmergency ? "【急】" : "") + "   ×" + order.Qty.ToString() + "   " + memo;

                        //检查部位
                        this.fpSpreadItemsSheet.Cells[iRow, 1].Text = (order.FT.OwnCost + order.FT.PubCost + order.FT.PayCost).ToString() + "元";
                        //iRow++;
                        //this.fpSpreadItemsSheet.Cells[iRow, 0].Text = (string.IsNullOrEmpty(order.Memo) ? "" : "（备注:" + order.Memo + "）") + (string.IsNullOrEmpty(order.CheckPartRecord) ? "" : " 部位：" + order.CheckPartRecord) ;
                        //this.fpSpreadItemsSheet.Cells[iRow, 0].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular);
                        //名称+备注
                        //this.fpSpreadItemsSheet.Cells[iRow, 0].Text = "检查目的：";
                    });
            });
            if (iRow < fpSpreadItemsSheet.RowCount - 1)
            {
                fpSpreadItemsSheet.Cells[iRow + 1, 0].Text = "(以下为空)";
            }
            lblExecDept.Text = orderList[0].ExeDept.Name;
            this.npbRecipeNo.Image = SOC.Public.Function.CreateBarCode(orderList[0].ReciptNO, this.npbRecipeNo.Width, this.npbRecipeNo.Height);

            FS.HISFC.Models.Order.OutPatient.OrderExtend orderExtObj = orderExtMgr.QueryByClinicCodOrderID(this.reg.ID, orderList[0].ID);
            if (orderExtObj != null)
            {
                lblPurpose.Text = orderExtObj.Extend1;
            }
            this.lblSeeDept.Text = orderList[0].ReciptDept.Name;
            this.labelSeeDate.Text = orderList[0].MOTime.ToString();
            //this.lblDocNo.Text = orderList[0].ReciptDoctor.ID + "(" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(orderList[0].ReciptDoctor.ID) + ")";
            this.lblDocNo.Text = orderList[0].ReciptDoctor.ID.Substring(2, orderList[0].ReciptDoctor.ID.Length - 2);
            this.lblDocName.Text = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(orderList[0].ReciptDoctor.ID);

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

