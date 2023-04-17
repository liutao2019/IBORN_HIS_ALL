using System;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.RecipePrint
{
    /// <summary>
    /// 草药处方
    /// </summary>
    public partial class ucRecipePrintHerb : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.IRecipePrint
    {
        public ucRecipePrintHerb()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 特殊处方标记
        /// </summary>
        string speRecipeLabel = "";

        /// <summary>
        /// 医嘱排序
        /// </summary>
        OrderCompare orderCompare = new OrderCompare();

        /// <summary>
        /// 打印处方项目名称是否是通用名：1 通用名；0 商品名
        /// </summary>
        private int printItemNameType = -1;

        /// <summary>
        /// 患者挂号信息
        /// </summary>
        private FS.HISFC.Models.Registration.Register myReg;

        FS.HISFC.BizLogic.Order.OutPatient.Order orderManager = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        #endregion

        /// <summary>
        /// 查询医嘱
        /// </summary>
        private void Query()
        {
            //从处方表获取
            ArrayList alOrder = this.orderManager.QueryOrderByRecipeNO(this.myReg.ID, this.recipeNO);

            //没有的话从费用表获取
            if (alOrder == null)
            {
                MessageBox.Show("查询处方信息出错！\r\n" + orderManager.Err, "错误", MessageBoxButtons.OK);
                return;
            }
            else if (alOrder.Count == 0)
            {
                return;
            }

            this.MakaLabel(alOrder);
        }

        enum EnumCol
        {
            CY1草药,
            YL1用量,
            CY2草药,
            YL2用量,
            CY3草药,
            YL3用量
        }

        private string GetName(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            string itemName = "";
            if (printItemNameType == -1)
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                printItemNameType = controlMgr.GetControlParam<int>("HNMZ11", true, 1);
            }

            if (order.Item.ID == "999" && !itemName.Contains("自备"))
            {
                itemName = order.Item.Name + "[自备]";
            }
            else
            {
                FS.HISFC.Models.Pharmacy.Item phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);

                if (printItemNameType == 0)
                {
                    itemName = order.Item.Name;
                }
                else
                {
                    if (string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                    {
                        itemName = phaItem.Name;
                    }
                    else
                    {
                        itemName = phaItem.NameCollection.RegularName;
                    }
                }

                //if (order.Unit == phaItem.MinUnit)
                //{
                //    itemName += "  " + phaItem.BaseDose.ToString() + phaItem.DoseUnit + "/" + phaItem.MinUnit + "×" + order.Qty.ToString() + order.Unit;
                //}
                //else
                {
                    itemName += "  " + phaItem.BaseDose.ToString() + phaItem.DoseUnit + "*" + phaItem.PackQty.ToString() + phaItem.MinUnit + "/" + phaItem.PackUnit + "×" + order.Qty.ToString() + order.Unit;
                }
            }

            //备注
            //itemName += (string.IsNullOrEmpty(outOrder.Memo) ? "" : " （" + outOrder.Memo + "）");

            return itemName;
        }

        private void AddUsageShow(FS.HISFC.Models.Order.OutPatient.Order order, int row)
        {
            //每次量+用法+频次+备注
            string show = "";

            string freName = SOC.HISFC.BizProcess.Cache.Order.GetFrequencyName(order.Frequency.ID);

            //外用药不显示每次用量
            //if (order.Item.ID != "999"
            //    && SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).SpecialFlag2 == "1")
            //{
            //    show = "用法：" + order.Usage.Name + "  " + order.Frequency.ID.ToLower() + (string.IsNullOrEmpty(order.Memo) ? "" : "  (" + order.Memo + "）");
            //}
            //else
            {
                show = "用法：每次" + order.DoseOnce.ToString() + order.DoseUnit + "  " + order.Usage.Name + "  " + freName + (string.IsNullOrEmpty(order.Memo) ? "" : "  (" + order.Memo + "）");
            }
            //if (FS.SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(order.Usage.ID))
            //{
            //    show += " 余液弃去";
            //}
            //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Text = show;
        }

        /// <summary>
        /// 生成显示信息
        /// </summary>
        /// <param name="alOrder"></param>
        private void MakaLabel(ArrayList alOrder)
        {
            if (alOrder == null)
            {
                return;
            }

            //按照sortID排序
            alOrder.Sort(orderCompare);

            int rowCount = (int)Math.Ceiling(alOrder.Count / (double)3);
            for (int index = 0; index < rowCount; index++)
            {
                if (alOrder.Count >= index * 3 + 1)
                {
                    FS.HISFC.Models.Order.OutPatient.Order order = alOrder[index * 3 + 0] as FS.HISFC.Models.Order.OutPatient.Order;
                    string memo = string.IsNullOrEmpty(order.Memo) ? "" : "(" + order.Memo + ")";
                    fpSpreadItemsSheet.Cells[index, (Int32)EnumCol.CY1草药].Text = order.Item.Name + memo;
                    fpSpreadItemsSheet.Cells[index, (Int32)EnumCol.YL1用量].Text = order.DoseOnce.ToString() + order.DoseUnit;
                }
                if (alOrder.Count >= index * 3 + 2)
                {
                    FS.HISFC.Models.Order.OutPatient.Order order = alOrder[index * 3 + 1] as FS.HISFC.Models.Order.OutPatient.Order;
                    string memo = string.IsNullOrEmpty(order.Memo) ? "" : "(" + order.Memo + ")";
                    fpSpreadItemsSheet.Cells[index, (Int32)EnumCol.CY2草药].Text = order.Item.Name + memo;
                    fpSpreadItemsSheet.Cells[index, (Int32)EnumCol.YL2用量].Text = order.DoseOnce.ToString() + order.DoseUnit;
                }
                if (alOrder.Count >= index * 3 + 3)
                {
                    FS.HISFC.Models.Order.OutPatient.Order order = alOrder[index * 3 + 2] as FS.HISFC.Models.Order.OutPatient.Order;
                    string memo = string.IsNullOrEmpty(order.Memo) ? "" : "(" + order.Memo + ")";
                    fpSpreadItemsSheet.Cells[index, (Int32)EnumCol.CY3草药].Text = order.Item.Name + memo;
                    fpSpreadItemsSheet.Cells[index, (Int32)EnumCol.YL3用量].Text = order.DoseOnce.ToString() + order.DoseUnit;
                }
            }

            string usage = "";
            decimal herbalQty = 1;
            decimal cost = 0;
            for (int index = 0; index < alOrder.Count; index++)
            {
                FS.HISFC.Models.Order.OutPatient.Order order = alOrder[index] as FS.HISFC.Models.Order.OutPatient.Order;
                //cost += order.Qty * order.Item.Price;
                cost += order.FT.OwnCost + order.FT.PayCost + order.FT.PubCost + order.FT.RebateCost;

                if (index == 0)
                {
                    usage = order.Usage.Name;
                    herbalQty = order.HerbalQty;
                }
            }
            this.lblPhaMoney.Text = FS.FrameWork.Public.String.ToSimpleString(cost, 2) + "元";



            fpSpreadItemsSheet.Rows[rowCount].Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            fpSpreadItemsSheet.Cells[rowCount, (Int32)EnumCol.CY1草药].ColumnSpan = 2;
            fpSpreadItemsSheet.Cells[rowCount, (Int32)EnumCol.CY1草药].Text = "用法：" + usage;
            fpSpreadItemsSheet.Cells[rowCount, (Int32)EnumCol.CY2草药].ColumnSpan = 2;
            fpSpreadItemsSheet.Cells[rowCount, (Int32)EnumCol.CY2草药].Text = herbalQty.ToString()+"剂";

            FS.HISFC.Models.Order.OutPatient.Order firstOrder = alOrder[0] as FS.HISFC.Models.Order.OutPatient.Order;
            this.lblSeeDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(firstOrder.ReciptDept.ID);
            lblPhaDoc.Text = firstOrder.ReciptDoctor.Name;
            this.npbRecipeNo.Image = SOC.Public.Function.CreateBarCode(firstOrder.ReciptNO, this.npbRecipeNo.Width, this.npbRecipeNo.Height);

            this.labelAuthorityDoc.Text = "____________";
            this.lblDrugDoct.Text = "_____________";
            this.lblSendDoct.Text = "_____________";

            //SetSpeInfo(alOrder);
        }

        public int PrintRecipe()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            print.SetPageSize(FS.SOC.Local.Order.ZhuHai.ZDWY.Function.GetPrintPage(false));
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            //print.PrintDocument.DefaultPageSettings.Landscape = false;

            if (!string.IsNullOrEmpty(printer))
            {
                print.PrintDocument.DefaultPageSettings.PrinterSettings.PrinterName = this.printer;
            }
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPage(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
            return 1;
        }

        /// <summary>
        /// 当前处方号
        /// </summary>
        private string recipeNO = "";

        /// <summary>
        /// 当前处方号
        /// </summary>
        public string RecipeNO
        {
            get
            {
                return this.recipeNO;
            }
            set
            {
                this.recipeNO = value;
                this.speRecipeLabel = "";
                this.Query();
            }
        }

        /// <summary>
        /// 设置患者基本信息
        /// </summary>
        /// <param name="register"></param>
        public int SetPatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            this.labelTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name + "中药处方笺";
            if (register == null)
            {
                return 1;
            }

            this.lblName.Text = register.Name;

            #region 麻精一显示代办人

            //FS.HISFC.Models.RADT.PatientInfo tmpInfo = new FS.HISFC.Models.RADT.PatientInfo();
            //if (!string.IsNullOrEmpty(register.PID.CardNO))
            //{
            //    tmpInfo = this.radtMgr.QueryComPatientInfo(register.PID.CardNO);
            //}
            //if (tmpInfo != null)
            //{
            //    register.IDCard = tmpInfo.IDCard;
            //    register.PhoneHome = tmpInfo.PhoneHome;
            //    if (!string.IsNullOrEmpty(tmpInfo.Kin.Name))
            //    {
            //        label15.Text += tmpInfo.Kin.Name;//代办人
            //    }
            //}
            #endregion

            this.labelAge.Text = this.orderManager.GetAge(register.Birthday, false);
            this.labelGender.Text = register.Sex.Name;
            this.lblCardNo.Text = register.PID.CardNO;
            this.npbBarCode.Image = SOC.Public.Function.CreateBarCode(register.PID.CardNO, this.npbBarCode.Width, this.npbBarCode.Height);
            lblPrintDate.Text = orderManager.GetDateTimeFromSysDateTime().ToString();

            this.chkOwn.Checked = false;
            this.chkPay.Checked = false;
            this.chkPub.Checked = false;
            this.chkOth.Checked = false;

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

            this.lblTel.Text = register.AddressHome + (!string.IsNullOrEmpty(register.PhoneHome) && !string.IsNullOrEmpty(register.AddressHome) ? "/" : "") + register.PhoneHome;

            #region 就诊日期

            DateTime dtNow = orderManager.GetDateTimeFromSysDateTime();

            if (register.SeeDoct.OperTime < new DateTime(2000, 1, 1))
            {
                this.labelSeeDate.Text = dtNow.ToString("yyyy.MM.dd");
            }
            else
            {
                this.labelSeeDate.Text = register.SeeDoct.OperTime.ToString("yyyy.MM.dd");
            }

            #endregion 就诊日期

            #region 诊断

            ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(register.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (al == null)
            {
                MessageBox.Show("查询诊断信息错误！\r\n" + diagManager.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            string strDiag = "";
            foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
            {
                if (diag != null && diag.IsValid)
                {
                    strDiag += diag.DiagInfo.ICD10.Name + "、";
                }
            }
            lblDiag.Text = strDiag.TrimEnd('、');

            #endregion

            this.myReg = register;

            return 1;
        }

        /// <summary>
        /// 设置特殊处方标记
        /// </summary>
        /// <param name="alOrder"></param>
        private void SetSpeInfo(ArrayList alOrder)
        {
            int speLevl = 1;
            foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOrder)
            {
                int level = ZDWY.Function.GetItemQaulity(order);
                if (level > speLevl)
                {
                    speLevl = level;
                }
            }

            //3、毒麻精一；2、精二；1、普通；0、非药品
            //lblSpeInfo.Visible = false;
            //switch (speLevl)
            //{
            //    case 3:
            //        lblSpeInfo.Text = "麻/精一";
            //        lblSpeInfo.Visible = true;
            //        break;
            //    case 2:
            //        lblSpeInfo.Text = "精 二";
            //        lblSpeInfo.Visible = true;
            //        break;
            //    case 1:
            //        if (myReg.DoctorInfo.Templet.RegLevel.IsEmergency)
            //        {
            //            lblSpeInfo.Text = "急 诊";
            //            lblSpeInfo.Visible = true;
            //        }
            //        else if (myReg.DoctorInfo.Templet.Dept.Name.Contains("儿"))
            //        {
            //            //lblSpeInfo.Text = "儿 科";
            //            //lblSpeInfo.Visible = true;
            //            //lblHelthInfo.Visible = true;
            //            //lblHeight.Visible = true;

            //            //lblDiag.Size = new Size(338, 14);

            //            #region 显示体重信息

            //            //FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();
            //            //string height = "";
            //            //string weight = "";
            //            //string SBP = "";
            //            //string DBP = "";
            //            //string tem = "";//体温
            //            //string bloodGlu = ""; //血糖
            //            //if (outOrderMgr.GetHealthInfo(this.myReg.ID, ref height, ref weight, ref SBP, ref DBP, ref tem, ref bloodGlu) == -1)
            //            //{
            //            //    this.lblHeight.Text = "";
            //            //}
            //            //else
            //            //{
            //            //    if (string.IsNullOrEmpty(weight))
            //            //    {
            //            //        this.lblHeight.Text = "";
            //            //    }
            //            //    else
            //            //    {
            //            //        this.lblHeight.Text = weight.ToString() + "千克";
            //            //    }
            //            //}
            //            #endregion
            //        }
            //        else
            //        {
            //        }
            //        break;
            //    default:
            //        break;
            //}
        }

        /// <summary>
        /// 打印机名称
        /// </summary>
        private string printer = "";

        public string Printer
        {
            get
            {
                return printer;
            }
            set
            {
                printer = value;
            }
        }

        public int PrintRecipeView(System.Collections.ArrayList alRecipe)
        {
            this.MakaLabel(alRecipe);
            return 1;
        }
    }
}
