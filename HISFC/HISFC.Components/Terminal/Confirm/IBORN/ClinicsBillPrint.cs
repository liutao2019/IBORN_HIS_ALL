using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Terminal.Confirm.IBORN
{
    public partial class ClinicsBillPrint : UserControl
    {
        public ClinicsBillPrint()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Manager.DataBase dbMgr = new FS.HISFC.BizLogic.Manager.DataBase();

        HISFC.BizProcess.Integrate.Order orderManager = new FS.HISFC.BizProcess.Integrate.Order();

        private void Clear()
        {
            lblName.Text = "";
            lblSex.Text = "";
            lblAge.Text = "";
            lblCardNo.Text = "";
            lblSeeDept.Text = "";
            labelPhoneAddr.Text = "";
            labelSeeDate.Text = "";

        }

        /// <summary>
        /// 设置打印
        /// </summary>
        /// <param name="IList"></param>
        public void SetPrintValue(FS.HISFC.Models.Terminal.TerminalApply terminalApply, bool isPreview)
        {
            this.labelTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name;
            // {FCA8E55A-6BAD-4ed7-9641-B01D188C07EB}
            FS.HISFC.Models.Base.Department currDept = new FS.HISFC.Models.Base.Department();
            currDept = (FS.HISFC.Models.Base.Department)(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept);
            if (!string.IsNullOrEmpty(currDept.HospitalName))
            {
                this.labelTitle.Text = currDept.HospitalName;
            }
            else
            {
                this.labelTitle.Text = "广州爱博恩妇产医院";
            }
            if (null == terminalApply) return;

            string ReciptNO = "";
            int ig = 1;
            int iRow = 0;

            //组号
            this.fpSpreadItemsSheet.Cells[iRow, 0].Text = ig.ToString();


            HISFC.Models.Order.OutPatient.Order order = this.orderManager.GetOneOrder(terminalApply.Patient.PID.ID, terminalApply.Order.ID);

            //名称
            if (order.Item.ID != "999")
            {
                if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    FS.HISFC.Models.Pharmacy.Item phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);
                    string strSpecs = "";
                    if (!string.IsNullOrEmpty(phaItem.Specs))
                    {
                        strSpecs = "[" + phaItem.Specs + "]";
                    }
                    this.fpSpreadItemsSheet.Cells[iRow, 1].Text = ig + "、" + order.Item.Name + (order.IsEmergency ? "【急】" : "") + (string.IsNullOrEmpty(order.Memo) ? "" : "（备注:" + order.Memo + "）") + strSpecs;
                }
                else
                {
                    FS.HISFC.Models.Fee.Item.Undrug item = SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID);
                    string strSpecs = "";
                    if (!string.IsNullOrEmpty(item.Specs))
                    {
                        strSpecs = "[" + item.Specs + "]";
                    }
                    this.fpSpreadItemsSheet.Cells[iRow, 1].Text = ig + "、" + order.Item.Name + (order.IsEmergency ? "【急】" : "") + (string.IsNullOrEmpty(order.Memo) ? "" : "（备注:" + order.Memo + "）") + strSpecs;
                }
            }
            else
            {

                this.fpSpreadItemsSheet.Cells[iRow, 1].Text = ig + "、" + order.Item.Name + (order.IsEmergency ? "【急】" : "") + (string.IsNullOrEmpty(order.Memo) ? "" : "（备注:" + order.Memo + "）");
            }
            ReciptNO = order.ReciptNO;
            //用量
            this.fpSpreadItemsSheet.Cells[iRow, 2].Text = (terminalApply.Item.Item.Qty - terminalApply.AlreadyConfirmCount).ToString() + order.Unit;

            //用法
            this.fpSpreadItemsSheet.Cells[iRow, 3].Text = order.Usage.Name;

            //次数
            this.fpSpreadItemsSheet.Cells[iRow, 4].Text = string.Format("{0}×{1}", order.Frequency.ID, order.HerbalQty + "天");


            if (iRow < fpSpreadItemsSheet.RowCount - 1)
            {
                fpSpreadItemsSheet.Cells[iRow + 1, 1].Text = "(以下为空)";
            }
            //this.npbRecipeNo.Image = SOC.Public.Function.CreateBarCode(ReciptNO, this.npbRecipeNo.Width, this.npbRecipeNo.Height);

            this.lblSeeDept.Text = order.ExeDept.Name;
            this.labelSeeDate.Text = order.MOTime.ToString();
            //this.lblPhaDoc.Text = orderList[0].ReciptDoctor.ID + "(" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(orderList[0].ReciptDoctor.ID) + ")";
            this.lblPhaDoc.Text = order.ReciptDoctor.ID.Substring(2, order.ReciptDoctor.ID.Length - 2);

            if (!isPreview)
            {
                this.PrintPage();
            }
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
            if (register == null)
                return;

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
            this.lblAge.Text = this.dbMgr.GetAge(register.Birthday);
            this.lblSex.Text = register.Sex.Name;
            this.lblCardNo.Text = register.PID.CardNO;
            this.labelPhoneAddr.Text = register.AddressHome + (!string.IsNullOrEmpty(register.PhoneHome) && !string.IsNullOrEmpty(register.AddressHome) ? "/" : "") + register.PhoneHome;
            lblPrintDate.Text = dbMgr.GetDateTimeFromSysDateTime().ToString();
            FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
            FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
            FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
            accountCard = accountManager.GetAccountCardForOne(register.PID.CardNO);
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

            if (accountCard != null)
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj = managerIntegrate.GetConstansObj("MemCardType", accountCard.AccountLevel.ID);

                this.lblAccountType.Text = obj.Name;
            }
            else
            {
                this.lblAccountType.Text = "";
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="judPrint">初打OR补打</param>
        private void PrintPage()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(GetPrintPage(false));
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;

            print.PrintPage(5, 5, this);
        }

        public static FS.HISFC.Models.Base.PageSize GetPrintPage(bool isLandScape)
        {
            FS.HISFC.BizLogic.Manager.PageSize pageManager = new FS.HISFC.BizLogic.Manager.PageSize();


            FS.HISFC.Models.Base.PageSize pageSize = null;
            if (isLandScape)
            {
                // pageSize = pageManager.GetPageSize("RecipeLand");

                if (pageSize == null)
                {
                    //pageSize = new FS.HISFC.Models.Base.PageSize("A5", 895, 579);
                    pageSize = new FS.HISFC.Models.Base.PageSize("A5", 880, 550);
                }

                return pageSize;

            }

            if (pageSize == null)
            {
                //pageSize = new FS.HISFC.Models.Base.PageSize("A5", 579, 895);
                pageSize = new FS.HISFC.Models.Base.PageSize("A5", 550, 880);
            }

            return pageSize;
        }
    }
}
