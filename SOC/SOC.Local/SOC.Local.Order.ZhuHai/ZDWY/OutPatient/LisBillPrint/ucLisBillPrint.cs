using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.LisBillPrint
{
    public partial class ucLisBillPrint : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {
        public ucLisBillPrint()
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
            lblClinic_Code.Text = "";
            lblTotalCost.Text = "";
            lblDocNo.Text = "";
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
            //this.labelTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name + "检验申请单";
            // {FCA8E55A-6BAD-4ed7-9641-B01D188C07EB}
            FS.HISFC.Models.Base.Department currDept = new FS.HISFC.Models.Base.Department();
            currDept = (FS.HISFC.Models.Base.Department)(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept);
            if (!string.IsNullOrEmpty(currDept.HospitalName))
            {
                this.labelTitle.Text = currDept.HospitalName + "检验申请单";
            }
            else
            {
                this.labelTitle.Text = "广州爱博恩妇产医院检验申请单";
            }
            #endregion 

            this.lblName.Text = this.myReg.Name;
            this.lblSex.Text = this.myReg.Sex.Name; //性别
            this.lblCardNo.Text = myReg.PID.CardNO; ;//门诊号
            this.lblClinic_Code.Text = myReg.ID;
            this.npbBarCode.Image = FS.SOC.Public.Function.CreateBarCode(this.myReg.PID.CardNO, this.npbBarCode.Width, this.npbBarCode.Height);
            this.lblAge.Text = this.dbMgr.GetAge(this.myReg.Birthday); //年龄
            lblPrintDate.Text = dbMgr.GetDateTimeFromSysDateTime().ToString();
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
            int iRow = 0;
            orderList.GroupBy(o => o.Combo.ID).ToList().ForEach(g =>
            {
                ig++;

                g.OrderBy(s => s.SubCombNO)
                    .ToList().ForEach(order =>
                    {
                        iRow++;

                        //名称+备注
                        this.fpSpreadItemsSheet.Cells[iRow, 0].Text = order.Item.Name + (order.IsEmergency ? "【急】" : "") + (string.IsNullOrEmpty(order.Memo) ? "" : "（备注:" + order.Memo + "）");

                        //数量+金额+样本类型
                        this.fpSpreadItemsSheet.Cells[iRow, 1].Text = "数量:" + order.Qty.ToString() + order.Unit + "  " + order.Qty * order.Item.Price + "元" + "  " + order.Sample.Name;
                    });
            });

            if (iRow < fpSpreadItemsSheet.RowCount - 1)
            {
                fpSpreadItemsSheet.Cells[iRow + 1, 0].Text = "以下为空";
            }

            this.lblDept.Text = orderList[0].ReciptDept.Name;
            lblExecDept.Text = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(orderList[0].ExeDept.ID);
            this.labelSeeDate.Text = orderList[0].MOTime.Date.ToString("yyyy.MM.dd");
            this.lblDocNo.Text = orderList[0].ReciptDoctor.ID + "(" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(orderList[0].ReciptDoctor.ID) + ")";
            this.lblTotalCost.Text = FS.FrameWork.Public.String.ToSimpleString(orderList.Sum(x => x.FT.OwnCost + x.FT.PubCost + x.FT.PayCost)) + "元";

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
