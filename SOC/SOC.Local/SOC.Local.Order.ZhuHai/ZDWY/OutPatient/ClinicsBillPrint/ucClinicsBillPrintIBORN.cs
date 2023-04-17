using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer;
using FS.HISFC.Models.Fee.Outpatient;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.ClinicsBillPrint
{
    /// <summary>
    /// 治疗单打印
    /// </summary>
    public partial class ucClinicsBillPrintIBORN : UserControl, FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {
        public ucClinicsBillPrintIBORN()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Manager.DataBase dbMgr = new FS.HISFC.BizLogic.Manager.DataBase();

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
        public void SetPrintValue(IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            this.labelTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name ;
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
            if (null == orderList || orderList.Count <= 0) return;

            string ReciptNO = "";
            int ig = 0;
            int iRow = 0;
            orderList.GroupBy(o => o.Combo.ID)
                .ToList().ForEach(g =>
                {
                    ig++;

                    g.OrderBy(s => s.SubCombNO)
                        .ToList().ForEach(order =>
                        {
                            iRow++;

                            //组号
                            this.fpSpreadItemsSheet.Cells[iRow, 0].Text = ig.ToString();

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
                                    this.fpSpreadItemsSheet.Cells[iRow, 1].Text = ig+"、"+order.Item.Name + (order.IsEmergency ? "【急】" : "") + (string.IsNullOrEmpty(order.Memo) ? "" : "（备注:" + order.Memo + "）") + strSpecs;
                                }
                                else
                                {
                                    FS.HISFC.Models.Fee.Item.Undrug item = SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID);
                                    string strSpecs = "";
                                    if (!string.IsNullOrEmpty(item.Specs))
                                    {
                                        strSpecs = "[" + item.Specs + "]";
                                    }
                                    this.fpSpreadItemsSheet.Cells[iRow, 1].Text = ig+"、"+order.Item.Name + (order.IsEmergency ? "【急】" : "") + (string.IsNullOrEmpty(order.Memo) ? "" : "（备注:" + order.Memo + "）") + strSpecs;
                                    //{F42D581C-3406-46a9-973B-4C451F4E8CEB}
                                    this.lblExecDept.Text = "执行科室：" + order.ExeDept.Name;
                                }
                            }
                            else
                            {
                               
                                this.fpSpreadItemsSheet.Cells[iRow, 1].Text = ig+"、"+order.Item.Name + (order.IsEmergency ? "【急】" : "") + (string.IsNullOrEmpty(order.Memo) ? "" : "（备注:" + order.Memo + "）");
                            }
                            ReciptNO = order.ReciptNO;
                            //用量
                            this.fpSpreadItemsSheet.Cells[iRow, 2].Text = order.Qty.ToString() + order.Unit;

                            //用法
                            this.fpSpreadItemsSheet.Cells[iRow, 3].Text = order.Usage.Name;

                            //次数
                            this.fpSpreadItemsSheet.Cells[iRow, 4].Text = string.Format("{0}×{1}", order.Frequency.ID, order.HerbalQty + "天");
                        });
                });
            if (iRow < fpSpreadItemsSheet.RowCount - 1)
            {
                fpSpreadItemsSheet.Cells[iRow + 1, 1].Text = "(以下为空)";
            }
            this.npbRecipeNo.Image = SOC.Public.Function.CreateBarCode(ReciptNO, this.npbRecipeNo.Width, this.npbRecipeNo.Height);

            this.lblSeeDept.Text = orderList[0].ReciptDept.Name;
            this.labelSeeDate.Text = orderList[0].MOTime.ToString();
            //this.lblPhaDoc.Text = orderList[0].ReciptDoctor.ID + "(" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(orderList[0].ReciptDoctor.ID) + ")";
            this.lblPhaDoc.Text = orderList[0].ReciptDoctor.ID.Substring(2, orderList[0].ReciptDoctor.ID.Length - 2);
            this.labelCost.Text = FS.FrameWork.Public.String.ToSimpleString(orderList.Sum(x => x.FT.OwnCost + x.FT.PubCost + x.FT.PayCost)) + "元";

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

            /*
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
             */
            //{3A2E38C1-3A99-45a3-82BD-0A1055298E69}
            this.lblAccountType.Text = register.Pact.Name;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="judPrint">初打OR补打</param>
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

        #region IOutPatientOrderPrint 成员

        /// <summary>
        /// 医嘱保存完的后续操作
        /// </summary>
        /// <param name="regObj">挂号实体</param>
        /// <param name="reciptDept">开立科室</param>
        /// <param name="reciptDoct">开立医生</param>
        /// <param name="IList">医嘱列表</param>
        /// <returns></returns>
        public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            this.Clear();
            this.SetPatientInfo(regObj);
            this.SetPrintValue(orderList, isPreview);

            return 1;
        }

        /// <summary>
        /// 门诊处方保存完的后续操作
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder, bool isPreview)
        {
            return 1;
        }

        public void PreviewOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.Generic.IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
        }


        public void SetPage(string pageStr)
        {
        }

        #endregion
    }
}
