using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.LisBillPrint
{
    public partial class ucLisBillPrintIBORN : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {
        public ucLisBillPrintIBORN()
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

            //{EB62A9BA-6EB5-46c7-B7E2-96C530D1B305}
            #region 
            //this.labelTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name;
            // {FCA8E55A-6BAD-4ed7-9641-B01D188C07EB} {92F1E785-D681-4a0d-8806-8A330BED5228}
            FS.HISFC.Models.Base.Department currDept = new FS.HISFC.Models.Base.Department();
            currDept = (FS.HISFC.Models.Base.Department)(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept);
            if (!string.IsNullOrEmpty(currDept.HospitalName))
            {
                this.labelTitle.Text = currDept.HospitalName;
            }
            else
            {
                this.labelTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name;
            }
            #endregion

            this.lblName.Text = this.myReg.Name;

            npxCardNo.Image = FS.SOC.Public.Function.CreateBarCode(myReg.PID.CardNO, this.npxCardNo.Width, this.npxCardNo.Height);

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

        FS.HISFC.BizLogic.Fee.Item undrugManager = new FS.HISFC.BizLogic.Fee.Item();
        FS.HISFC.Models.Fee.Item.Undrug undrug = new FS.HISFC.Models.Fee.Item.Undrug();
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
            int iRow = 0;
            this.fpSpreadItemsSheet.Cells[iRow, 0].Text = "检验项目：";
            this.fpSpreadItemsSheet.Cells[iRow, 1].Text = "";
            this.fpSpreadItemsSheet.Cells[iRow, 0].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            orderList.GroupBy(o => o.Combo.ID).ToList().ForEach(g =>
            {
                ig++;

                g.OrderBy(s => s.SubCombNO)
                    .ToList().ForEach(order =>
                    {
                        iRow++;

                        undrug = undrugManager.GetItemByUndrugCode(order.Item.ID);
                        //名称+备注
                        string memo = string.Empty;// {3FF25BA5-1251-4dbb-8895-F94D8FF26BAB}
                        memo = (string.IsNullOrEmpty(order.Memo) ? "" : "备注:" + order.Memo) + (string.IsNullOrEmpty(order.Sample.Name) ? "  " : "  样本:" + order.Sample.Name) + (string.IsNullOrEmpty(undrug.Memo) ? "  " : "  【" + undrug.Memo + "】");
                        this.fpSpreadItemsSheet.Cells[iRow, 0].Text = ig + "、" + order.Item.Name + (order.IsEmergency ? "【急】" : "") + "   ×" + order.Qty.ToString() + "   " + memo;

                        if (memo.Length >= 55)
                        {
                            this.fpSpreadItemsSheet.Rows[iRow].Height = 39F;
                        }

                        //数量+金额+样本类型
                        this.fpSpreadItemsSheet.Cells[iRow, 1].Text = order.Item.Price + "元" ;
                        //iRow++;
                        //this.fpSpreadItemsSheet.Cells[iRow, 0].Text = (string.IsNullOrEmpty(order.Memo) ? "" : "备注:" + order.Memo) + (string.IsNullOrEmpty(order.Sample.Name) ? "  " : "  样本:" + order.Sample.Name) + (string.IsNullOrEmpty(undrug.Memo) ? "  " : "  【" + undrug.Memo + "】");
                        //this.fpSpreadItemsSheet.Cells[iRow, 0].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular);
                       
                    });
            });

            if (iRow < fpSpreadItemsSheet.RowCount - 1)
            {
                fpSpreadItemsSheet.Cells[iRow + 1, 0].Text = "(以下为空)";
            }

            this.lblDept.Text = orderList[0].ReciptDept.Name;
            this.lblApplyNo.Text = orderList[0].ReciptNO;
            lblExecDept.Text = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(orderList[0].ExeDept.ID);
            this.labelSeeDate.Text = orderList[0].MOTime.ToString();
            this.lblDocNo.Text = orderList[0].ReciptDoctor.ID.Substring(2, orderList[0].ReciptDoctor.ID.Length -2);
            this.lblTotalCost.Text = FS.FrameWork.Public.String.ToSimpleString(orderList.Sum(x => x.FT.OwnCost + x.FT.PubCost + x.FT.PayCost)) + "元";
            this.lblDocName.Text = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(orderList[0].ReciptDoctor.ID);
            
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
