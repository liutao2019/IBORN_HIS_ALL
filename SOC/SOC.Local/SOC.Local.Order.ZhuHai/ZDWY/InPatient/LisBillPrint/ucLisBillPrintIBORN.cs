using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.LisBillPrint
{
    public partial class ucLisBillPrintIBORN : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IInPatientOrderPrint
    {
        public ucLisBillPrintIBORN()
        {
            InitializeComponent();
        } 
        
        FS.HISFC.BizLogic.Manager.DataBase dbMgr = new FS.HISFC.BizLogic.Manager.DataBase();

        /// <summary>
        /// 患者住院信息
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

        public void SetPage(string pageStr)
        {
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

            this.lblSex.Text = this.myPatientInfo.Sex.Name; //性别
            this.lblCardNo.Text = myPatientInfo.PID.PatientNO; ;//住院号
            this.lblAge.Text = this.dbMgr.GetAge(this.myPatientInfo.Birthday); //年龄
            lblPrintDate.Text = dbMgr.GetDateTimeFromSysDateTime().ToString();
            this.labelPhoneAddr.Text = myPatientInfo.AddressHome + (!string.IsNullOrEmpty(myPatientInfo.PhoneHome) && !string.IsNullOrEmpty(myPatientInfo.AddressHome) ? "/" : "") + myPatientInfo.PhoneHome;

            if (Regex.IsMatch(patientInfo.PID.PatientNO, @"^[+-]?\d*[.]?\d*$"))
            {
                npxApplyNo.Image = FS.SOC.Public.Function.CreateBarCode(myPatientInfo.PID.PatientNO, this.npxApplyNo.Width, this.npxApplyNo.Height);
            }
            else
            {
                this.label1.Visible = false;
                this.npxApplyNo.Visible = false;
            }

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
            int iRow = 0;
            decimal totCost = 0m;
            this.fpSpreadItemsSheet.Cells[iRow, 0].Text = "检验项目：";
            this.fpSpreadItemsSheet.Cells[iRow, 1].Text = "";
            this.fpSpreadItemsSheet.Cells[iRow, 0].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            foreach (FS.HISFC.Models.Order.Inpatient.Order order in orderList)
            {
                ig++;
                iRow++;
                undrug = undrugManager.GetItemByUndrugCode(order.Item.ID);
                //名称+备注
                this.fpSpreadItemsSheet.Cells[iRow, 0].Text = ig + "、" + order.Item.Name + (order.IsEmergency ? "【急】" : "") + "   ×" + order.Qty.ToString();

                //数量+金额+样本类型
                this.fpSpreadItemsSheet.Cells[iRow, 1].Text = (order.Item.Price * order.Qty).ToString("F2")+ "元";
                iRow++;
                this.fpSpreadItemsSheet.Cells[iRow, 0].Text = (string.IsNullOrEmpty(order.Memo) ? "" : "备注:" + order.Memo) + (string.IsNullOrEmpty(order.Sample.Name) ? "  " : "  样本:" + order.Sample.Name) + (string.IsNullOrEmpty(undrug.Memo) ? "  " : "  【" + undrug.Memo + "】");
                this.fpSpreadItemsSheet.Cells[iRow, 0].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular);
                totCost += order.Qty * order.Item.Price;
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
            this.lblTotalCost.Text = totCost.ToString("F2") + "元";
            this.lblDocName.Text = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(order1.ReciptDoctor.ID);
            
            
            //if (!isReturnPrint)
            //{
            //    this.PrintPage();
            //}
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
