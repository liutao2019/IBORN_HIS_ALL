using System;
using System.Collections.Generic;
using System.Linq;   //后来添加的
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;

namespace GJLocal.HISFC.Components.OpGuide.InjectBillPrint
{
    /// <summary>
    /// 诊疗单
    /// </summary>
    public partial class ucInjectBillPrint : UserControl, FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {
        public ucInjectBillPrint()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();

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
            if (null == orderList || orderList.Count <= 0)
            {
                return;
            }

            int ig = 0;
            int iRow = 1;
            string str_hypotest="";
            orderList.GroupBy(o => o.Combo.ID).ToList().ForEach(g =>
                {
                    ig++;

                    g.OrderBy(s => s.SubCombNO)
                        .ToList().ForEach(order =>
                        {
                            iRow++;

                            this.fpSpreadItemsSheet.Cells[iRow, 1].ColumnSpan = 2;
                            this.fpSpreadItemsSheet.Cells[iRow, 1].Text = order.Item.Name + outOrderMgr.TransHypotest(order.HypoTest) + (order.IsEmergency ? "【急】" : "") + (string.IsNullOrEmpty(order.Memo) ? "" : "（备注:" + order.Memo + "）") + "  " + order.DoseOnce.ToString() + order.DoseUnit + "  " + order.Usage.Name;
                            if (order.HypoTest !=FS.HISFC.Models.Order.EnumHypoTest.FreeHypoTest 
                                || order.HypoTest != FS.HISFC.Models.Order.EnumHypoTest.NoHypoTest)
                            {
                                str_hypotest += order.Item.Name;
                            }

                            //组号
                            //this.fpSpreadItemsSheet.Cells[iRow, 0].Text = order.SubCombNO.ToString();

                            //名称
                            //this.fpSpreadItemsSheet.Cells[iRow, 1].Text = order.Item.Name + outOrderMgr.TransHypotest(order.HypoTest) + (order.IsEmergency ? "【急】" : "") + (string.IsNullOrEmpty(order.Memo) ? "" : "（备注:" + order.Memo + "）");

                            //用量
                            //this.fpSpreadItemsSheet.Cells[iRow, 2].Text = order.DoseOnce.ToString() + order.DoseUnit;

                            //用法
                            //this.fpSpreadItemsSheet.Cells[iRow, 3].Text = order.Usage.Name;

                            //次数
                            //this.fpSpreadItemsSheet.Cells[iRow, 4].Text = string.Format("{0}×{1}", order.Frequency.ID, order.HerbalQty);
                        });
                });
            iRow += 1;
            //if (iRow < fpSpreadItemsSheet.RowCount - 1)
            //{
            //    fpSpreadItemsSheet.Cells[iRow + 1, 1].Text = "以下为空";
            //}
            //this.lblSeeDept.Text = orderList[0].ReciptDept.Name;
            //this.labelSeeDate.Text = orderList[0].MOTime.Date.ToString("yyyy.MM.dd");
            //this.lblPhaDoc.Text = orderList[0].ReciptDoctor.ID + "(" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(orderList[0].ReciptDoctor.ID) + ")";
            //this.labelCost.Text = FS.FrameWork.Public.String.ToSimpleString(orderList.Sum(x => x.FT.OwnCost + x.FT.PubCost + x.FT.PayCost)) + "元";
            this.fpSpreadItemsSheet.Cells[0, 0].Text = "需要皮试的药品/Sensitivity Test Required:" + str_hypotest;
            if (iRow < 7) { iRow = 7; }
            int num = iRow - 2;
            this.fpSpreadItemsSheet.Cells[2, 0].RowSpan = num;
            this.fpSpreadItemsSheet.Cells[2, 0].Text = "医嘱内容(药名、剂量、用法)Prescripts";

            this.fpSpreadItemsSheet.Cells[iRow, 0].ColumnSpan = 2;
            this.fpSpreadItemsSheet.Cells[iRow, 0].Text = "医师/M.D:" + orderList[0].ReciptDoctor.Name;

            this.fpSpreadItemsSheet.Cells[iRow + 1, 0].ColumnSpan = 2;
            this.fpSpreadItemsSheet.Cells[iRow + 1, 0].Text = "药师/Pharmacist:       ";

            this.fpSpreadItemsSheet.AddSpanCell(iRow, 2, iRow + 1, 4);
            this.fpSpreadItemsSheet.Cells[iRow, 2].Text = "药费/Medicine Fee:       ";



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
            if (register == null)
                return;

            this.lblName.Text = register.Name;
            this.lblnation.Text = register.Nationality.Name;
            //this.lblAge.Text = this.outOrderMgr.GetAge(register.Birthday, false);
            this.lblBirthday.Text = register.Birthday.ToShortDateString();
            if (register.Sex.Name.Equals("男"))
            {
                this.lblSex.Text = "男/M";
            }
            else {
                this.lblSex.Text = "女/F";
            }
            //this.lblSex.Text = register.Sex.Name;
            this.lblCardNo.Text = register.PID.CardNO;
            lblPrintDate.Text = outOrderMgr.GetDateTimeFromSysDateTime().ToString();

            //this.labelPhoneAddr.Text = register.AddressHome;

            //this.npbBarCode.Image = FS.SOC.Public.Function.CreateBarCode(register.PID.CardNO, this.npbBarCode.Width, this.npbBarCode.Height);
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="judPrint">初打OR补打</param>
        private void PrintPage()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.Models.Base.PageSize pSize = FS.SOC.Local.Order.ZhuHai.ZDWY.Function.GetPrintPage(false);
            print.SetPageSize(pSize);
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            fpSpreadItemsSheet.PrintInfo.ShowBorder = false;
            fpSpreadFootSheet.PrintInfo.ShowBorder = false;

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
            List<FS.HISFC.Models.Order.OutPatient.Order> orderList = new List<FS.HISFC.Models.Order.OutPatient.Order>();
            foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOrder)
            {
                orderList.Add(order);
            }

            this.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, orderList, isPreview);
            return 1;
        }

        public void PreviewOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.Generic.IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, orderList, isPreview);
        }


        public void SetPage(string pageStr)
        {
        }

        #endregion

    }
}
