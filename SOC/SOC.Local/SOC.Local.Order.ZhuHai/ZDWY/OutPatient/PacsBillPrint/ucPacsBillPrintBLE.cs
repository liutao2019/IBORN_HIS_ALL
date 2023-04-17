using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.PacsBillPrint
{
    public partial class ucPacsBillPrintBLE : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {
        public ucPacsBillPrintBLE()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        private void Clear()
        {
            lblName.Text = "";
            lblSex.Text = "";
            //lblAge.Text = "";
            //lblCardNo.Text = "";
            //lblSeeDept.Text = "";
            //labelPhoneAddr.Text = "";
            labelSeeDate.Text = "";
            lblDiag.Text = "";
            labelTotalPrice.Text = "";

            Itemname.Text = "";//{87B4666F-6F35-4ca9-BBEB-38630C9BD20B}新增
            lblFindings.Text = "";//{87B4666F-6F35-4ca9-BBEB-38630C9BD20B}新增
            lblInvestigat.Text = "";//{87B4666F-6F35-4ca9-BBEB-38630C9BD20B}新增

            //this.chkOwn.Checked = false;
            //this.chkPay.Checked = false;
            //this.chkPub.Checked = false;
            //this.chkOth.Checked = false;
        }

        #region IOutPatientOrderPrint 成员

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

        public void PreviewOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
        }

        public void SetPage(string pageStr)
        {
        }

        #endregion

        /// <summary>
        /// 设置患者基本信息
        /// </summary>
        /// <param name="register"></param>
        public void SetPatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            if (register == null) return;

            this.lblName.Text = register.Name;

            //this.caseHistory = FS.HISFC.Components.Order.CacheManager.OutOrderMgr.QueryCaseHistoryByClinicCode(register.ID);
           // this.lblAge.Text = this.diagManager.GetAge(register.Birthday, false);
            this.lblBirthday.Text = register.Birthday.ToShortDateString();
            this.lblSex.Text = register.Sex.Name;
            //this.lblCardNo.Text = register.PID.CardNO;
            //this.labelPhoneAddr.Text = register.PhoneHome + (!string.IsNullOrEmpty(register.PhoneHome) && !string.IsNullOrEmpty(register.AddressHome) ? "/" : "") + register.AddressHome;
            lblDate.Text = diagManager.GetDateTimeFromSysDateTime().ToString("yyyy.MM.dd");

            if (register.Pact.PayKind.ID == "01")
            {
                //this.chkOwn.Checked = true;
            }
            else if (register.Pact.PayKind.ID == "02")
            {
                //this.chkPay.Checked = true;
            }
            else
            {
                //this.chkPub.Checked = true;
            }

            #region 诊断
            ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(register.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (al == null) return;

            string strDiag = "";
            int i = 1;
            foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
            {
                if (diag != null && diag.IsValid)
                {
                    strDiag += i.ToString() + "、" + diag.DiagInfo.ICD10.Name + "； " + "";
                    i++;
                    lblDiag.Text = strDiag;
                }
            }
            #endregion

            this.npbBarCode.Image = FS.SOC.Public.Function.CreateBarCode(register.PID.CardNO, this.npbBarCode.Width, this.npbBarCode.Height);
        }
        /// <summary>
        /// 病历实体
        /// </summary>
        private FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory caseHistory = new FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory();
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
            int iRow = -1;
            string strInvestigat1 = "";
            string Itemname1 = "";//{87B4666F-6F35-4ca9-BBEB-38630C9BD20B}新增
            string eName = "";
            foreach (FS.HISFC.Models.Order.OutPatient.Order order in orderList)
            {
                //orderList.GroupBy(o => o.Combo.ID).ToList().ForEach(g =>  //{87B4666F-6F35-4ca9-BBEB-38630C9BD20B}注释不要这个代码
                //{
                    ig++;

                //    g.OrderBy(s => s.SubCombNO)
                //        .ToList().ForEach(order =>
                //        {
                //            iRow++;
                //名称+备注
                //this.fpSpreadItemsSheet.Cells[iRow, 0].Text = "辅助检查：" + order.Item.Name + (order.IsEmergency ? "【急】" : "") + (string.IsNullOrEmpty(order.Memo) ? "" : "（备注:" + order.Memo + "）");

                //检查部位
                //this.fpSpreadItemsSheet.Cells[iRow, 1].Text = "部位：" + order.CheckPartRecord;

                //iRow++;

                //名称+备注
                //this.fpSpreadItemsSheet.Cells[iRow, 0].Text = "检查目的：";
                //strInvestigat += "辅助检查：" + order.Item.Name + (order.IsEmergency ? "【急】" : "") + (string.IsNullOrEmpty(order.Memo) ? "" : "（备注:" + order.Memo + "）") + "\t" + "部位：" + order.CheckPartRecord + "\n\n";
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
                    Itemname1 += ig.ToString() + "、 " + eName + (order.IsEmergency ? "【急】" : "") + (string.IsNullOrEmpty(order.Memo) ? "" : "（备注:" + order.Memo + "）") + "   ";//{87B4666F-6F35-4ca9-BBEB-38630C9BD20B}赋值形式修改
                    if (order.CheckPartRecord != null && order.CheckPartRecord != "")//{87B4666F-6F35-4ca9-BBEB-38630C9BD20B}赋值形式修改
                    {
                        strInvestigat1 += ig.ToString() + "、" + order.CheckPartRecord + "  ";
                    }
                //        });
                //});
            }
            this.Itemname.Text = Itemname1;//{87B4666F-6F35-4ca9-BBEB-38630C9BD20B}赋值
            if (this.caseHistory != null)
            {
                string caseMain = this.caseHistory.CaseMain;
                string checkBody = this.caseHistory.CheckBody;
                if (!string.IsNullOrEmpty(caseMain))
                {
                    this.lblFindings.Text = "Investigation:  " + caseMain;
                }
                if (!string.IsNullOrEmpty(checkBody))
                {
                    this.lblFindings.Text += "\nPhysical examination:  " + checkBody;
                }
            }
            this.lblInvestigat.Text = strInvestigat1;//{87B4666F-6F35-4ca9-BBEB-38630C9BD20B}赋值
            
            if (iRow < fpSpreadItemsSheet.RowCount - 1)
            {
                //fpSpreadItemsSheet.Cells[iRow + 1, 0].Text = "以下为空";
            }
            //lblExecDept.Text = orderList[0].ExeDept.Name;

            //this.lblSeeDept.Text = orderList[0].ReciptDept.Name;
            //this.labelSeeDate.Text = orderList[0].MOTime.Date.ToString("yyyy.MM.dd");
            this.lblDoctor.Text = orderList[0].ReciptDoctor.Name;
            //this.labelTotalPrice.Text = FS.FrameWork.Public.String.ToSimpleString(orderList.Sum(x => x.FT.OwnCost + x.FT.PubCost + x.FT.PayCost)) + "元";

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
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            print.IsLandScape = true;
            if (FS.SOC.Local.Order.ZhuHai.ZDWY.Function.IsPreview())
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }
    }
}

