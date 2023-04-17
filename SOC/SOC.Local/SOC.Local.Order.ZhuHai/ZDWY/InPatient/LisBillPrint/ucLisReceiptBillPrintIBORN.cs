using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.LisBillPrint
{
    public partial class ucLisReceiptBillPrintIBORN : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IInPatientOrderPrint
    {
        public ucLisReceiptBillPrintIBORN()
        {
            InitializeComponent();
        } 
        
        FS.HISFC.BizLogic.Manager.DataBase dbMgr = new FS.HISFC.BizLogic.Manager.DataBase();

        /// <summary>
        /// 患者挂号信息
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo myPatientInfo;

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


        #endregion  IInPatientOrderPrint 成员

        private void Clear()
        {
            lblName.Text = "";
            labelSeeDate.Text = "";
            lblSex.Text = "";
            lblDept.Text = "";
            lblExecDept.Text = "";
            lblAge.Text = "";
            lblCardNo.Text = "";
            lblTotalCost.Text = "";
            lblDocNo.Text = "";
            labelPhoneAddr.Text = "";
        }

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
            this.myPatientInfo = patientInfo;
            if (this.myPatientInfo == null) return;
            this.labelTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name;


            this.lblName.Text = this.myPatientInfo.Name;
            this.lblName1.Text = myPatientInfo.Name;

            int strLength = this.LengthString(myPatientInfo.Name);
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

            if (Regex.IsMatch(patientInfo.PID.PatientNO, @"^[+-]?\d*[.]?\d*$"))
            {
                this.npbRecipeNo.Image = SOC.Public.Function.CreateBarCode(patientInfo.PID.PatientNO, this.npbRecipeNo.Width, this.npbRecipeNo.Height);
            }
            else
            {
                this.labelRecipeNumber.Visible = false;
                this.npbRecipeNo.Visible = false;
            }

            this.lblSex.Text = this.myPatientInfo.Sex.Name; //性别
            this.lblCardNo.Text = myPatientInfo.PID.PatientNO; ;//住院号
            this.lblAge.Text = this.dbMgr.GetAge(this.myPatientInfo.Birthday); //年龄
            lblPrintDate.Text = dbMgr.GetDateTimeFromSysDateTime().ToString();
            this.labelPhoneAddr.Text = myPatientInfo.AddressHome + (!string.IsNullOrEmpty(myPatientInfo.PhoneHome) && !string.IsNullOrEmpty(myPatientInfo.AddressHome) ? "/" : "") + myPatientInfo.PhoneHome;

        }

        FS.HISFC.BizLogic.Fee.Item undrugManager = new FS.HISFC.BizLogic.Fee.Item();
        FS.HISFC.Models.Fee.Item.Undrug undrug = new FS.HISFC.Models.Fee.Item.Undrug();
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
            int iRow = 1;
            decimal totCost = 0m;
            this.fpSpreadItemsSheet.Cells[0, 0].Text = "以下时间仅供参考，实际出报告时间请以医院实际情况为准";
            this.fpSpreadItemsSheet.Cells[0, 1].Text = "";
            
            this.fpSpreadItemsSheet.Cells[1, 0].Text = "项目名称";
            this.fpSpreadItemsSheet.Cells[1, 1].Text = "出报告时间（以采样时间为起点）";
            foreach (FS.HISFC.Models.Order.Inpatient.Order order in orderList)
            {   ig++;
                iRow++;

                undrug = undrugManager.GetItemByUndrugCode(order.Item.ID);
                //名称
                this.fpSpreadItemsSheet.Cells[iRow, 0].Text = order.Item.Name + (order.IsEmergency ? "【急】" : "");

                //注意事项
                this.fpSpreadItemsSheet.Cells[iRow, 1].Text = undrug.Notice;
                 totCost += order.Qty;    
            }

            if (iRow < fpSpreadItemsSheet.RowCount - 1)
            {
                fpSpreadItemsSheet.Cells[iRow + 1, 0].Text = "(以下为空)";
            }
            FS.HISFC.Models.Order.Inpatient.Order order1 = new FS.HISFC.Models.Order.Inpatient.Order();
            order1 = orderList[0] as FS.HISFC.Models.Order.Inpatient.Order;

            this.lblDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(order1.ReciptDept.ID); 
            lblExecDept.Text = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(order1.ExeDept.ID);
            this.labelSeeDate.Text = order1.MOTime.ToString();
            this.lblDocNo.Text = order1.ReciptDoctor.ID.Substring(2, order1.ReciptDoctor.ID.Length -2);
            this.lblTotalCost.Text = FS.FrameWork.Public.String.ToSimpleString(totCost) + "元";
            this.lblDocName.Text = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(order1.ReciptDoctor.ID);
            
        }


        /// <summary>
        /// 打印
        /// </summary>
        private void PrintPage()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(FS.SOC.Local.Order.ZhuHai.ZDWY.Function.GetPrintPage(false));
            //print.IsLandScape = true;
            print.PrintDocument.DefaultPageSettings.Landscape = false;

            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            //print.IsDataAutoExtend = false;

            if (FS.SOC.Local.Order.ZhuHai.ZDWY.Function.IsPreview())
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
            }
        }
    }
}
