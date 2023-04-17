using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.LisBillPrint
{
    public partial class ucLisBillPrintBLE : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {
        public ucLisBillPrintBLE()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();//{CF366E13-90F2-42cb-80AF-8BA41E93D3D8}缺失诊断，从检查单搬过来
        
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
            //lblAge.Text = "";
            lblCardNo.Text = "";
            //lblClinic_Code.Text = "";
            lblTotalCost.Text = "";
            lblDocNo.Text = "";
            lblDoctor.Text = "";
            lblDoctor2.Text = "";
            labelSeeDate.Text = "";
            labelSeeDate2.Text = "";
            lblDiag.Text = "";
            lblTechnician.Text = "";
            lblReportDate.Text = "";
        }

        /// <summary>
        /// 设置患者基本信息
        /// </summary>
        /// <param name="register"></param>
        public void SetPatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            this.myReg = register;
            if (this.myReg == null) return;

            this.lblName.Text = this.myReg.Name;
            this.lblName2.Text = this.myReg.Name;
            this.lblSex.Text = this.myReg.Sex.Name; //性别
            this.lblSex2.Text = this.myReg.Sex.Name;
            //this.lblCardNo.Text = myReg.PID.CardNO; ;//门诊号
            this.lblBirthday.Text = this.myReg.Birthday.ToShortDateString();  //出生年月//{CF366E13-90F2-42cb-80AF-8BA41E93D3D8}修改显示格式
            this.lblBirthday2.Text = this.myReg.Birthday.ToShortDateString();//{CF366E13-90F2-42cb-80AF-8BA41E93D3D8}修改显示格式

            //this.lblClinic_Code.Text = myReg.ID;
            //this.npbBarCode.Image = FS.SOC.Public.Function.CreateBarCode(this.myReg.PID.CardNO, this.npbBarCode.Width, this.npbBarCode.Height);
            //this.lblAge.Text = this.dbMgr.GetAge(this.myReg.Birthday); //年龄
            //lblPrintDate.Text = dbMgr.GetDateTimeFromSysDateTime().ToString();
            #region 诊断
            //{CF366E13-90F2-42cb-80AF-8BA41E93D3D8}缺失诊断，从检查单搬过来
            ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(register.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (al == null) return;

            string strDiag = "";
            int i = 1;
            foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
            {
                if (diag != null && diag.IsValid)
                {
                    strDiag += i.ToString() + "、" + diag.DiagInfo.ICD10.Name + "\n";
                    i++;
                    lblDiag.Text = strDiag;
                }
            }//{CF366E13-90F2-42cb-80AF-8BA41E93D3D8}缺失诊断，从检查单搬过来
            #endregion
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
            int ii = 1;
            string labelitems = "";
            string eName = "";
            //if (orderList.Count > 4)
            //{
            //    this.lblItems.Font = new System.Drawing.Font("微软雅黑", 5F);
            //    this.lblItems2.Font = new System.Drawing.Font("微软雅黑",5F);
            //}
            orderList.GroupBy(o => o.Combo.ID).ToList().ForEach(g =>
            {
                ig++;

                g.OrderBy(s => s.SubCombNO)
                    .ToList().ForEach(order =>
                    {
                        //iRow++;

                        //名称+备注
                        //this.fpSpreadItemsSheet.Cells[iRow, 0].Text = order.Item.Name + (order.IsEmergency ? "【急】" : "") + (string.IsNullOrEmpty(order.Memo) ? "" : "（备注:" + order.Memo + "）");

                        //数量+金额+样本类型
                        //this.fpSpreadItemsSheet.Cells[iRow, 1].Text = "数量:" + order.Qty.ToString() + order.Unit + "  " + order.Qty * order.Item.Price + "元" + "  " + order.Sample.Name;
                        if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            eName = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).NameCollection.EnglishName;
                        }
                        else
                        {
                            eName = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID).NameCollection.EnglishName;
                        }
                        if (string.IsNullOrEmpty(eName))
                        {
                            eName = order.Item.Name;
                        }
                        labelitems +=ii + ".  " + eName  + (order.IsEmergency ? "【急】" : "") + (string.IsNullOrEmpty(order.Memo) ? "\n" : "（备注:" + order.Memo + "）") + " 数量:" + order.Qty.ToString() + order.Unit + " " + order.Qty * order.Item.Price + "元" + " " + order.Sample.Name + "\n";
                        ii++;
                    });
            });

            if (iRow < fpSpreadItemsSheet.RowCount - 1)
            {
                //fpSpreadItemsSheet.Cells[iRow + 1, 0].Text = "以下为空";
            }

            this.lblDept.Text = orderList[0].ReciptDept.Name;
            this.lblDoctor.Text = orderList[0].ReciptDoctor.Name;//{CF366E13-90F2-42cb-80AF-8BA41E93D3D8}添加所需显示数据
            this.lblDoctor2.Text = orderList[0].ReciptDoctor.Name;//{CF366E13-90F2-42cb-80AF-8BA41E93D3D8}添加所需显示数据
            lblExecDept.Text = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(orderList[0].ExeDept.ID);
            this.labelSeeDate.Text = orderList[0].MOTime.Date.ToString("yyyy.MM.dd");
            this.labelSeeDate2.Text = orderList[0].MOTime.Date.ToString("yyyy.MM.dd");
            //this.lblDocNo.Text = orderList[0].ReciptDoctor.ID + "(" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(orderList[0].ReciptDoctor.ID) + ")";
            this.lblTotalCost.Text = FS.FrameWork.Public.String.ToSimpleString(orderList.Sum(x => x.FT.OwnCost + x.FT.PubCost + x.FT.PayCost)) + "元";

            //npxApplyNo.Image = FS.SOC.Public.Function.CreateBarCode(orderList[0].ReciptNO, this.npxApplyNo.Width, this.npxApplyNo.Height);
            this.lblItems.Text = labelitems;
            this.lblItems2.Text = labelitems;

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
            print.IsLandScape = true;
            print.PrintDocument.DefaultPageSettings.Landscape = true;

            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;

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
