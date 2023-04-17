using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.MaterialsBillPrint
{
    public partial class ucMaterialsBillPrintIBORN : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {
        public ucMaterialsBillPrintIBORN()
        {
            InitializeComponent();
        } 
        
        FS.HISFC.BizLogic.Manager.DataBase dbMgr = new FS.HISFC.BizLogic.Manager.DataBase();

        /// <summary>
        /// 患者挂号信息
        /// </summary>
        FS.HISFC.Models.Registration.Register myReg;

        #region IOutPatientOrderPrint 成员

        public void PreviewOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.Generic.IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
        }

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

        public void SetPage(string pageStr)
        {
        }

        #endregion  IOutPatientOrderPrint 成员

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
        public void SetPatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            this.myReg = register;
            if (this.myReg == null) return;
            this.labelTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name;

            this.lblName.Text = this.myReg.Name;
            this.lblName1.Text = myReg.Name;

            int strLength = this.LengthString(myReg.Name);
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
            this.lblSex.Text = this.myReg.Sex.Name; //性别
            this.lblCardNo.Text = myReg.PID.CardNO; ;//门诊号
            this.lblAge.Text = this.dbMgr.GetAge(this.myReg.Birthday); //年龄
            lblPrintDate.Text = dbMgr.GetDateTimeFromSysDateTime().ToString();
            this.labelPhoneAddr.Text = register.AddressHome + (!string.IsNullOrEmpty(register.PhoneHome) && !string.IsNullOrEmpty(register.AddressHome) ? "/" : "") + register.PhoneHome;

        }
           FS.HISFC.Models.Pharmacy.Item item = new FS.HISFC.Models.Pharmacy.Item();

           string show = "";
           string strDoseOnce = "";
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

            //{2BA63245-A4F7-482b-9CD0-811BE5135D5E}
            try
            {
                FS.HISFC.Models.Base.Department currDept = (FS.HISFC.Models.Base.Department)(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept);
                this.labelTitle.Text = currDept.HospitalName;
            }
            catch
            {
 
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

                        item = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);
                        //名称+备注
                        // {21EB3CEB-F23D-45D3-AE13-43F47481D29B}
                        this.fpSpreadItemsSheet.Cells[iRow, 0].Text = ig + "、" + order.Item.Name + (order.IsEmergency ? "【急】" : "   ") + item.Specs + "   ×" + order.Qty.ToString() + "    " + order.Unit;

                        //数量+金额+样本类型
                        this.fpSpreadItemsSheet.Cells[iRow, 1].Text = order.Item.Price + "元" ;
                        iRow++;
                        string freName = SOC.HISFC.BizProcess.Cache.Order.GetFrequencyName(order.Frequency.ID);

                        strDoseOnce = "每次" + order.DoseOnce.ToString() + order.DoseUnit + " ";
                        show = "用法：" + order.Usage.Name + "," + freName + "," + strDoseOnce + (string.IsNullOrEmpty(order.Memo) ? "" : "  (" + order.Memo + "）");

                        this.fpSpreadItemsSheet.Cells[iRow, 0].Text = show;
                        this.fpSpreadItemsSheet.Cells[iRow, 0].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular);
                    });
            });

            if (iRow < fpSpreadItemsSheet.RowCount - 1)
            {
                fpSpreadItemsSheet.Cells[iRow + 1, 0].Text = "(以下为空)";
            }

            this.lblDept.Text = orderList[0].ReciptDept.Name;
            lblExecDept.Text = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(orderList[0].ExeDept.ID);
            this.labelSeeDate.Text = orderList[0].MOTime.ToString();
            this.lblDocNo.Text = orderList[0].ReciptDoctor.ID.Substring(2, orderList[0].ReciptDoctor.ID.Length -2);
            this.lblTotalCost.Text = FS.FrameWork.Public.String.ToSimpleString(orderList.Sum(x => x.FT.OwnCost + x.FT.PubCost + x.FT.PayCost)) + "元";
            this.lblDocName.Text = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(orderList[0].ReciptDoctor.ID);
            
            npxApplyNo.Image = FS.SOC.Public.Function.CreateBarCode(orderList[0].ReciptNO, this.npxApplyNo.Width, this.npxApplyNo.Height);

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
            //print.IsLandScape = true;
            print.PrintDocument.DefaultPageSettings.Landscape = false;

            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

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
