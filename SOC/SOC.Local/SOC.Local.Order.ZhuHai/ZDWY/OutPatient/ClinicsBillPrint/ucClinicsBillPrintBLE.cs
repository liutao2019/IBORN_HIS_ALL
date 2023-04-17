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
    public partial class ucClinicsBillPrintBLE : UserControl, FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {
        public ucClinicsBillPrintBLE()
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

            this.chkOwn.Checked = false;
            this.chkPay.Checked = false;
            this.chkPub.Checked = false;
            this.chkOth.Checked = false;
        }

        /// <summary>
        /// 设置打印
        /// </summary>
        /// <param name="IList"></param>
        public void SetPrintValue(IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            if (null == orderList || orderList.Count <= 0) return;

            int ig = 0;
            int iRow = 0;
            string eName = "";
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
                                    FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);
                                    string strSpecs = "";
                                    if (!string.IsNullOrEmpty(phaItem.Specs))
                                    {
                                        strSpecs = "[" + phaItem.Specs + "]";
                                    }
                                    eName = phaItem.NameCollection.EnglishName;
                                    if (string.IsNullOrEmpty(eName))
                                    {
                                        eName = order.Item.Name;
                                    }
                                    this.fpSpreadItemsSheet.Cells[iRow, 1].Text =eName + (order.IsEmergency ? "【急】" : "") + (string.IsNullOrEmpty(order.Memo) ? "" : "（备注:" + order.Memo + "）") + strSpecs;
                                }
                                else
                                {
                                    FS.HISFC.Models.Fee.Item.Undrug item = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID);
                                    string strSpecs = "";
                                    if (!string.IsNullOrEmpty(item.Specs))
                                    {
                                        strSpecs = "[" + item.Specs + "]";
                                    }
                                    eName = item.NameCollection.EnglishName;
                                    if (string.IsNullOrEmpty(eName))
                                    {
                                        eName = order.Item.Name;
                                    }
                                    this.fpSpreadItemsSheet.Cells[iRow, 1].Text = eName + (order.IsEmergency ? "【急】" : "") + (string.IsNullOrEmpty(order.Memo) ? "" : "（备注:" + order.Memo + "）") + strSpecs;
                                }
                            }
                            else
                            {
                                this.fpSpreadItemsSheet.Cells[iRow, 1].Text = order.Item.Name + (order.IsEmergency ? "【急】" : "") + (string.IsNullOrEmpty(order.Memo) ? "" : "（备注:" + order.Memo + "）");
                            }

                            this.fpSpreadItemsSheet.Rows[iRow].Height = this.fpSpreadItemsSheet.GetPreferredRowHeight(iRow, false)*1.3f;

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
                FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
                textCellType2.WordWrap = false;
                textCellType2.Multiline = false;
                this.fpSpreadItemsSheet.Cells[iRow + 1, 1].CellType = textCellType2;
                fpSpreadItemsSheet.Cells[iRow + 1, 1].Text = "以下为空 The following no text";
            }

            this.lblSeeDept.Text = orderList[0].ReciptDept.Name;
            this.labelSeeDate.Text = orderList[0].MOTime.Date.ToString("yyyy.MM.dd");
            this.lblPhaDoc.Text = orderList[0].ReciptDoctor.ID + "(" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(orderList[0].ReciptDoctor.ID) + ")";
            this.labelCost.Text = FS.FrameWork.Public.String.ToSimpleString(orderList.Sum(x => x.FT.OwnCost + x.FT.PubCost + x.FT.PayCost)) + "元";

            if (!isPreview)
            {
                this.PrintPage();
            }
        }

        /// <summary>
        /// 设置患者基本信息
        /// </summary>
        /// <param name="register"></param>
        public void SetPatientInfo(FS.HISFC.Models.Registration.Register register)
        {

            FS.HISFC.Models.Base.Department currDept = new FS.HISFC.Models.Base.Department();
            currDept = (FS.HISFC.Models.Base.Department)(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept);
            if (!string.IsNullOrEmpty(currDept.HospitalName))
            {
                this.labelTitle.Text = currDept.HospitalName + "治疗单";
            }
            else
            {
                this.labelTitle.Text = "贝利尔医疗中心治疗单";
            }
            //this.labelTitle.Text = (new FS.FrameWork.Management.DataBaseManger()).Hospital.Name+"治疗单";

            if (register == null) 
                return;

            this.lblName.Text = register.Name;
            this.lblAge.Text = this.dbMgr.GetAge(register.Birthday);
            this.lblSex.Text = register.Sex.Name;
            this.lblCardNo.Text = register.PID.CardNO;
            this.labelPhoneAddr.Text = register.PhoneHome + (!string.IsNullOrEmpty(register.PhoneHome) && !string.IsNullOrEmpty(register.AddressHome) ? "/" : "") + register.AddressHome;
            lblPrintDate.Text = dbMgr.GetDateTimeFromSysDateTime().ToString();

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
            npbBarCode.Image = FS.SOC.Public.Function.CreateBarCode(register.PID.CardNO, this.npbBarCode.Width, this.npbBarCode.Height);
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
