using System;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Neusoft.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer;
using System.Collections.Generic;

namespace GJLocal.HISFC.Components.OpGuide.RecipePrint
{
    /// <summary>
    /// 普通处方(含急诊处方、儿科处方、精二处方）
    /// </summary>
    public partial class ucRecipePrint : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.HISFC.BizProcess.Interface.IRecipePrint
    {
        public ucRecipePrint()
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
        private Neusoft.HISFC.Models.Registration.Register myReg;

        Neusoft.HISFC.BizLogic.Order.OutPatient.Order orderManager = new Neusoft.HISFC.BizLogic.Order.OutPatient.Order();

        Neusoft.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new Neusoft.HISFC.BizLogic.HealthRecord.Diagnose();

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
            ZH组号,
            ZBJ组标记,
            MC名称,
            //YF用法,
            ZHH组合号
        }

        private string GetName(Neusoft.HISFC.Models.Order.OutPatient.Order order, bool isShowDoseOnce)
        {
            string itemName = "";
            if (printItemNameType == -1)
            {
                Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();
                printItemNameType = controlMgr.GetControlParam<int>("HNMZ11", true, 1);
            }

            string strDoseOnce = isShowDoseOnce ? ("  /每次" + order.DoseOnce.ToString() + order.DoseUnit) : "";

            if (order.Item.ID == "999" && !itemName.Contains("自备"))
            {
                itemName = order.Item.Name + "[自备]";
            }
            else
            {
                Neusoft.HISFC.Models.Pharmacy.Item phaItem = Neusoft.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);

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

            return itemName +  strDoseOnce;
        }

        private void AddUsageShow(Neusoft.HISFC.Models.Order.OutPatient.Order order, int row, bool isComb)
        {
            //每次量+用法+频次+备注
            string show = "";

            string strDoseOnce = "";

            //外用药不显示每次用量
            if (//(order.Item.ID != "999"
                //&& SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).SpecialFlag2 == "1")
                //|| 
                isComb)
            {
            }
            else
            {
                strDoseOnce = "每次" + order.DoseOnce.ToString() + order.DoseUnit + "  ";
            }

            string freName = Neusoft.SOC.HISFC.BizProcess.Cache.Order.GetFrequencyName(order.Frequency.ID);

            show = "用法：" + strDoseOnce + order.Usage.Name + "  " + freName + (string.IsNullOrEmpty(order.Memo) ? "" : "  (" + order.Memo + "）");

            //if (Neusoft.SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(order.Usage.ID))
            //{
            //    show += " 余液弃去";
            //}
            fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Text = show;
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
            this.fpSpreadItemsSheet.RowCount = 0;

            Dictionary<string, int> dicCombNo = new Dictionary<string, int>();
            Neusoft.HISFC.Models.Order.OutPatient.Order preOrder = null;

            decimal cost = 0;
            for (int index = 0; index < alOrder.Count; index++)
            {
                Neusoft.HISFC.Models.Order.OutPatient.Order order = alOrder[index] as Neusoft.HISFC.Models.Order.OutPatient.Order;
                int row = fpSpreadItemsSheet.RowCount;
                fpSpreadItemsSheet.Rows.Add(row, 1);
                //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].ColumnSpan = 2;

                if (!dicCombNo.ContainsKey(order.Combo.ID))
                {
                    dicCombNo.Add(order.Combo.ID, 1);
                    if (preOrder != null)
                    {
                        AddUsageShow(preOrder, row, dicCombNo[preOrder.Combo.ID] > 1);

                        row = fpSpreadItemsSheet.RowCount;
                        fpSpreadItemsSheet.Rows.Add(row, 1);
                        //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].ColumnSpan = 2;
                    }
                    //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.ZH组号].Text = order.SubCombNO.ToString() + ".";
                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.ZH组号].Text = (dicCombNo.Keys.Count).ToString() + ".";
                }
                else
                {
                    dicCombNo[order.Combo.ID] += 1;
                }

                //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.ZBJ组标记].Text = "";

                string showName = GetName(order, false);
                if (GetCombCount(alOrder, order) > 1)
                {
                    showName = GetName(order, true);
                }

                string strTemp="哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈";
                if (showName.Length < strTemp.Length)
                {
                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Font = new System.Drawing.Font("微软雅黑", 10.5F, FontStyle.Bold);
                }
                else
                {
                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Font = new System.Drawing.Font("微软雅黑", 8.5F, FontStyle.Bold);
                }

                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Text = showName;
                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.ZHH组合号].Text = order.Combo.ID;

                fpSpreadItemsSheet.Rows[row].Tag = order;
                cost += order.FT.OwnCost + order.FT.PayCost + order.FT.PubCost + order.FT.RebateCost;
                preOrder = order;

                if (index == alOrder.Count - 1)
                {
                    row = fpSpreadItemsSheet.RowCount;
                    fpSpreadItemsSheet.Rows.Add(row, 1);
                    AddUsageShow(order, row, dicCombNo[preOrder.Combo.ID] > 1);
                }
            }

            GJLocal.HISFC.Components.OpGuide.OutPatient.Classes.Function.DrawComboLeft(fpSpreadItemsSheet, (Int32)EnumCol.ZHH组合号, (Int32)EnumCol.ZBJ组标记);
            for (int row = 0; row < fpSpreadItemsSheet.RowCount; row++)
            {
                if (fpSpreadItemsSheet.Rows[row].Tag != null)
                {
                    fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                }
                else
                {
                    fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                }
            }
            if (fpSpreadItemsSheet.RowCount < 11)
            {
                int row = fpSpreadItemsSheet.RowCount;
                fpSpreadItemsSheet.Rows.Add(row, 1);
                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Text = "以下为空"; 
                fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }

            this.lblRMB.Text = Neusoft.FrameWork.Public.String.ToSimpleString(cost, 2) + "元";

            Neusoft.HISFC.Models.Order.OutPatient.Order firstOrder = alOrder[0] as Neusoft.HISFC.Models.Order.OutPatient.Order;
            this.lblSeeDept.Text = Neusoft.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(firstOrder.ReciptDept.ID);
            lblDoctor.Text = firstOrder.ReciptDoctor.Name;
            //this.npbRecipeNo.Image = Neusoft.SOC.Public.Function.CreateBarCode(firstOrder.ReciptNO, this.npbRecipeNo.Width, this.npbRecipeNo.Height);

            //this.labelAuthorityDoc.Text = "____________";
            //this.lblDrugDoct.Text = "_____________";
            //this.lblSendDoct.Text = "_____________";

           // SetSpeInfo(alOrder);
        }

        private int GetCombCount(ArrayList alOrder, Neusoft.HISFC.Models.Order.OutPatient.Order order)
        {
            int count = 0;
            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order ord in alOrder)
            {
                if (order.Combo.ID == ord.Combo.ID)
                {
                    count += 1;
                }
            }
            return count;
        }

        public int PrintRecipe()
        {
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(Neusoft.SOC.Local.Order.ZhuHai.ZDWY.Function.GetPrintPage(false));
            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            //print.IsLandScape = false;

            if (!string.IsNullOrEmpty(printer))
            {
                print.PrintDocument.DefaultPageSettings.PrinterSettings.PrinterName = this.printer;
            }
            if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
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
                //this.recipeNO = value;
                //this.speRecipeLabel = "";
                this.Query();
            }
        }

        /// <summary>
        /// 设置患者基本信息
        /// </summary>
        /// <param name="register"></param>
        public int SetPatientInfo(Neusoft.HISFC.Models.Registration.Register register)
        {
            if (register == null)
            {
                return 1;
            }

            this.lblName.Text = register.Name;

            #region 麻精一显示代办人

            //Neusoft.HISFC.Models.RADT.PatientInfo tmpInfo = new Neusoft.HISFC.Models.RADT.PatientInfo();
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

            //this.labelAge.Text = this.orderManager.GetAge(register.Birthday, false);
            this.lblBirthday.Text = register.Birthday.ToShortDateString();
            this.lblSex.Text = register.Sex.Name;
            this.lblNationlity.Text = register.Nationality.Name;
            //this.labelGender.Text = register.Sex.Name;
            this.lblCardNo.Text = register.PID.CardNO;
            //this.npbBarCode.Image = Neusoft.SOC.Public.Function.CreateBarCode(register.PID.CardNO, this.npbBarCode.Width, this.npbBarCode.Height);

            //lblPrintDate.Text = orderManager.GetDateTimeFromSysDateTime().ToString();

            //this.chkOwn.Checked = false;
            //this.chkPay.Checked = false;
            //this.chkPub.Checked = false;
            //this.chkOth.Checked = false;

            //if (register.Pact.PayKind.ID == "01")
            //{
            //    this.chkOwn.Checked = true;
            //}
            //else if (register.Pact.PayKind.ID == "02")
            //{
            //    this.chkPay.Checked = true;
            //}
            //else
            //{
            //    this.chkPub.Checked = true;
            //}

            //this.lblTel.Text = register.AddressHome + (!string.IsNullOrEmpty(register.PhoneHome) && !string.IsNullOrEmpty(register.AddressHome) ? "/" : "") + register.PhoneHome;

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

            ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(register.ID, Neusoft.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (al == null)
            {
                MessageBox.Show("查询诊断信息错误！\r\n" + diagManager.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            string strDiag = "";
            foreach (Neusoft.HISFC.Models.HealthRecord.Diagnose diag in al)
            {
                if (diag != null && diag.IsValid)
                {
                    strDiag += diag.DiagInfo.ICD10.Name + "、";
                }
            }
            //lblDiag.Text = strDiag.TrimEnd('、');

            #endregion

            this.myReg = register;

            return 1;
        }

        /// <summary>
        /// 设置特殊处方标记
        /// </summary>
        /// <param name="alOrder"></param>
        //private void SetSpeInfo(ArrayList alOrder)
        //{
        //    int speLevl = 1;
        //    foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in alOrder)
        //    {
        //        int level = GJLocal.HISFC.Components.OpGuide.OutPatient.Function.GetItemQaulity(order);
        //        if (level > speLevl)
        //        {
        //            speLevl = level;
        //        }
        //    }

        //    //3、毒麻精一；2、精二；1、普通；0、非药品
        //    lblSpeInfo.Visible = false;
        //    switch (speLevl)
        //    {
        //        case 3:
        //            lblSpeInfo.Text = "麻/精一";
        //            lblSpeInfo.Visible = true;
        //            break;
        //        case 2:
        //            lblSpeInfo.Text = "精 二";
        //            lblSpeInfo.Visible = true;
        //            break;
        //        case 1:
        //            if (myReg.DoctorInfo.Templet.RegLevel.IsEmergency)
        //            {
        //                lblSpeInfo.Text = "急 诊";
        //                lblSpeInfo.Visible = true;
        //            }
        //            else if (myReg.DoctorInfo.Templet.Dept.Name.Contains("儿"))
        //            {
        //                lblSpeInfo.Text = "儿 科";
        //                lblSpeInfo.Visible = true;
        //                lblHelthInfo.Visible = true;
        //                lblHeight.Visible = true;

        //                lblDiag.Size = new Size(338, 14);

        //                #region 显示体重信息

        //                Neusoft.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new Neusoft.HISFC.BizLogic.Order.OutPatient.Order();
        //                string height = "";
        //                string weight = "";
        //                string SBP = "";
        //                string DBP = "";
        //                string tem = "";//体温
        //                string bloodGlu = ""; //血糖
        //                if (outOrderMgr.GetHealthInfo(this.myReg.ID, ref height, ref weight, ref SBP, ref DBP, ref tem, ref bloodGlu) == -1)
        //                {
        //                    this.lblHeight.Text = "";
        //                }
        //                else
        //                {
        //                    if (string.IsNullOrEmpty(weight))
        //                    {
        //                        this.lblHeight.Text = "";
        //                    }
        //                    else
        //                    {
        //                        this.lblHeight.Text = weight.ToString() + "千克";
        //                    }
        //                }
        //                #endregion
        //            }
        //            else
        //            {
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        //}

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
