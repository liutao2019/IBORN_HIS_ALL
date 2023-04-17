using System;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer;
using System.Collections.Generic;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.RecipePrint.IBORN
{
    /// <summary>
    /// 普通处方(含急诊处方、儿科处方、精二处方）{BB163312-E95F-42fc-97E1-B7A0E3CA01BA}
    /// </summary>
    public partial class ucRecipePrintIBORN : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.IRecipePrint
    {
        public ucRecipePrintIBORN()
        {
            InitializeComponent();
            this.Height = 800;
            this.fpSpreadItemsSheet.Rows.Default.Height = 30;
           
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

        public bool IsSpeRecipe = false;

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

        private string packid = "";
        List<string> packidlist = new List<string>();//{BB163312-E95F-42fc-97E1-B7A0E3CA01BA}
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

        private string GetName(FS.HISFC.Models.Order.OutPatient.Order order, bool isShowDoseOnce)
        {
            string itemName = "";
            if (printItemNameType == -1)
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                printItemNameType = controlMgr.GetControlParam<int>("HNMZ11", true, 1);
            }

            string strDoseOnce = isShowDoseOnce ? ("  /每次" + order.DoseOnce.ToString() + order.DoseUnit) : "";

            if (order.Item.ID == "999" && !itemName.Contains("自备"))
            {
                itemName = order.Item.Name + "[自备]" + (order.IsEmergency ? "【急】" : "");
            }
            else
            {
                FS.HISFC.Models.Pharmacy.Item phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);

                if (printItemNameType == 0)
                {
                    itemName = order.Item.Name + (order.IsEmergency ? "【急】" : "");
                }
                else
                {
                    if (string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                    {
                        itemName = phaItem.Name + (order.IsEmergency ? "【急】" : "");
                    }
                    else
                    {
                        itemName = phaItem.NameCollection.RegularName + (order.IsEmergency ? "【急】" : "");
                    }
                }

                //if (order.Unit == phaItem.MinUnit)
                //{
                //    itemName += "  " + phaItem.BaseDose.ToString() + phaItem.DoseUnit + "/" + phaItem.MinUnit + "×" + order.Qty.ToString() + order.Unit;
                //}
                //else
                {
                    //itemName += "  " + phaItem.BaseDose.ToString() + phaItem.DoseUnit + "*" + phaItem.PackQty.ToString() + phaItem.MinUnit + "/" + phaItem.PackUnit + "     ×" + order.Qty.ToString() + order.Unit;
                    itemName += "    " + phaItem.Specs + "     ×" + order.Qty.ToString() + order.Unit;
                }
            }

            //备注
            //itemName += (string.IsNullOrEmpty(outOrder.Memo) ? "" : " （" + outOrder.Memo + "）");

            return itemName +  strDoseOnce;
        }

        private void AddUsageShow(FS.HISFC.Models.Order.OutPatient.Order order, int row, bool isComb)
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
                //{EC294D39-F2C4-4351-835A-DD8288B3CC31}
                FS.HISFC.Models.Pharmacy.Item phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);
                if (order.DoseUnit == phaItem.DoseUnit && order.DoseOnce / phaItem.BaseDose == ((int)(order.DoseOnce / phaItem.BaseDose)))
                {
                    strDoseOnce = "每次" + ((int)(order.DoseOnce / phaItem.BaseDose)).ToString() + phaItem.MinUnit + " ";
                }
                else if (order.DoseUnit == phaItem.MinUnit && order.DoseOnce != (int)order.DoseOnce)
                {
                    strDoseOnce = "每次" + (order.DoseOnce * phaItem.BaseDose).ToString() + phaItem.DoseUnit + " ";
                }
                else
                {
                    strDoseOnce = "每次" + order.DoseOnce.ToString() + order.DoseUnit + " ";
                }
            } 
            
            string freName = SOC.HISFC.BizProcess.Cache.Order.GetFrequencyName(order.Frequency.ID);

            show = "用法：" + order.Usage.Name + "," + freName + (string.IsNullOrEmpty(strDoseOnce)? "": ","+strDoseOnce) + (string.IsNullOrEmpty(order.Memo) ? "" : "  (" + order.Memo + "）");

            //if (FS.SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(order.Usage.ID))
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
            packidlist.Clear();//{BB163312-E95F-42fc-97E1-B7A0E3CA01BA}
            packidlist.Add("2");
            packidlist.Add("6");
            packidlist.Add("4");
            //按照sortID排序
            alOrder.Sort(orderCompare);
            this.fpSpreadItemsSheet.RowCount = 0;
           

            Dictionary<string, int> dicCombNo = new Dictionary<string, int>();
            FS.HISFC.Models.Order.OutPatient.Order preOrder = null;

            decimal cost = 0;
            for (int index = 0; index < alOrder.Count; index++)
            {
                FS.HISFC.Models.Order.OutPatient.Order order = alOrder[index] as FS.HISFC.Models.Order.OutPatient.Order;
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

                string strTemp="哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈";
                if (showName.Length < strTemp.Length)
                {
                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Font = new System.Drawing.Font("宋体", 10F, FontStyle.Bold);
                }
                else
                {
                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Font = new System.Drawing.Font("宋体", 9F, FontStyle.Bold);
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

            ZhuHai.Classes.Function.DrawComboLeft(fpSpreadItemsSheet, (Int32)EnumCol.ZHH组合号, (Int32)EnumCol.ZBJ组标记);
            for (int row = 0; row < fpSpreadItemsSheet.RowCount; row++)
            {
                if (fpSpreadItemsSheet.Rows[row].Tag != null)
                {
                    fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                }
                else
                {
                    fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                }
            }
            if (fpSpreadItemsSheet.RowCount < 11)
            {
                //{A23C09D6-EAC0-4a34-9007-8BD08774A424}作废下面的方法，从所有药品中读取是否含有毒性药物
                int speLevl = 1;
                foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOrder)
                {
                    //{5FFE7DFF-358B-4e65-B798-1A2D7478A251}
                    int level = ZDWY.Function.GetItemQualityForPrint(order);
                    if (level > speLevl)
                    {
                        speLevl = level;
                    }
                }
                /*
                int speLevl = 1;
                FS.HISFC.Models.Order.OutPatient.Order order = alOrder[0] as FS.HISFC.Models.Order.OutPatient.Order;
                int level = ZDWY.Function.GetItemQaulity(order);
                if (level > speLevl)
                {
                    speLevl = level;
                }
                 * 
                 *///{BB163312-E95F-42fc-97E1-B7A0E3CA01BA}
                if(packidlist.Contains(this.packid))
                {
                    lbIdenNOM.Visible = true;
                    lbIdenNO.Visible = true;
                  
                    this.label16.Visible = true;
                    this.labelAgentIdenNO.Visible = true;
                }

                FS.HISFC.Models.Order.OutPatient.Order orderNew = new FS.HISFC.Models.Order.OutPatient.Order(); 
                orderNew=alOrder[0] as FS.HISFC.Models.Order.OutPatient.Order;
                FS.HISFC.Models.Pharmacy.Item itemNew = new FS.HISFC.Models.Pharmacy.Item();
                if (orderNew != null)
                {
                   //itemNew = orderNew.Item as FS.HISFC.Models.Pharmacy.Item;
                   FS.HISFC.BizLogic.Pharmacy.Item item = new FS.HISFC.BizLogic.Pharmacy.Item();
                   itemNew=item.GetItem(orderNew.Item.ID);
                   if (itemNew.SpecialFlag4 == null)
                       itemNew.SpecialFlag4 = "-";
                }

                //{564D4750-E736-432b-A5B6-B0B178AF1A11}
                if (speLevl == 3 || speLevl == 4 )
                {
                    this.lblDoc.Visible = false;
                    this.lblDoc.Height = 0;
                    this.panel4.Height = 119;
                    lbIdenNOM.Visible = true;
                    lbIdenNO.Visible = true;
                    this.lbIdenNO.Visible = true;
                    this.label23.Visible = true;
                    this.labelAgentName.Visible = true;
                    this.label16.Visible = true;
                    this.labelAgentIdenNO.Visible = true;
                    this.label17.Visible = true;
                    this.label18.Visible = true;
                    //this.panel4.BackColor = Color.Red;
                    //this.lblDoc.BackColor = Color.Pink;
                    //int row = fpSpreadItemsSheet.RowCount;

                    //fpSpreadItemsSheet.Rows.Add(row, 1);
                    //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Text = "余药量__________丢弃，确认人__________/__________";
                    //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                
                    //fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("楷体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    int row = fpSpreadItemsSheet.RowCount;
                    fpSpreadItemsSheet.Rows.Add(row, 1);
                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Text = "(以下为空)";
                    fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                } //重点品种精二和全麻
                //else if(false)
                else  if ((itemNew.SpecialFlag4.ToString() == "13" && itemNew.Quality.ID.ToString() == "Q") || (itemNew.SpecialFlag4.ToString() == "13" && itemNew.Quality.ID.ToString() == "P2"))
                 {
                        this.lblDoc.Visible = false;
                        this.lblDoc.Height = 0;
                        this.panel4.Height = 119;
                        lbIdenNOM.Visible = true;
                        lbIdenNO.Visible = true;
                        this.lbIdenNO.Visible = true;
                        this.label23.Visible = true;
                        this.labelAgentName.Visible = true;
                        this.label16.Visible = true;
                        this.labelAgentIdenNO.Visible = true;
                        this.label17.Visible = true;
                        this.label18.Visible = true;
                        //this.panel4.BackColor = Color.Red;
                        //this.lblDoc.BackColor = Color.Pink;
                        //int row = fpSpreadItemsSheet.RowCount;

                        //fpSpreadItemsSheet.Rows.Add(row, 1);
                        //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Text = "余药量__________丢弃，确认人__________/__________";
                        //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

                        //fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("楷体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                        int row = fpSpreadItemsSheet.RowCount;
                        fpSpreadItemsSheet.Rows.Add(row, 1);
                        fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Text = "(以下为空)";
                        fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    }
                else
                {
                    this.panel4.Visible = false;
                    this.panel4.Height = 0;
                    this.lblDoc.Height = 70;
                    //this.panel4.BackColor = Color.Red;
                    //this.lblDoc.BackColor = Color.Pink;
                    int row = fpSpreadItemsSheet.RowCount;

                    fpSpreadItemsSheet.Rows.Add(row, 1);
                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Text = "(以下为空)";
                    fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                }
            }

            this.lblPhaMoney.Text = Math.Round(cost, 2).ToString() + "元";
            //FS.FrameWork.Public.String.ToSimpleString(cost, 2)
            this.lblMPhaMoney.Text = Math.Round(cost, 2).ToString() + "元";
            FS.HISFC.Models.Order.OutPatient.Order firstOrder = alOrder[0] as FS.HISFC.Models.Order.OutPatient.Order;
            this.lblSeeDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(firstOrder.ReciptDept.ID);
            lblPhaDoc.Text = firstOrder.ReciptDoctor.ID.Substring(2, firstOrder.ReciptDoctor.ID.Length-2);
            lblPhaMDoc.Text = firstOrder.ReciptDoctor.ID.Substring(2, firstOrder.ReciptDoctor.ID.Length - 2);
            this.npbRecipeNo.Image = SOC.Public.Function.CreateBarCode(firstOrder.ReciptNO, this.npbRecipeNo.Width, this.npbRecipeNo.Height);

            lblMPrintDate.Text = orderManager.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm");
            this.labelAuthorityDoc.Text = " ";
            this.lblDrugDoct.Text = " ";
            this.lblSendDoct.Text = " ";

            SetSpeInfo(alOrder);
        }

        private int GetCombCount(ArrayList alOrder, FS.HISFC.Models.Order.OutPatient.Order order)
        {
            int count = 0;
            foreach (FS.HISFC.Models.Order.OutPatient.Order ord in alOrder)
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
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(FS.SOC.Local.Order.ZhuHai.ZDWY.Function.GetPrintPage(false));
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            //print.IsLandScape = false;

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
           // this.labelTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name;
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

            if (register == null)
            {
                return 1;
            }

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

            #region 麻精一显示代办人

            FS.HISFC.Models.RADT.PatientInfo tmpInfo = new FS.HISFC.Models.RADT.PatientInfo();
            if (!string.IsNullOrEmpty(register.PID.CardNO))
            {
                FS.HISFC.BizProcess.Integrate.RADT radtMgr = new FS.HISFC.BizProcess.Integrate.RADT();
                tmpInfo = radtMgr.QueryComPatientInfo(register.PID.CardNO);
            }
            if (tmpInfo != null)
            {
                labelAgentName.Text = tmpInfo.Kin.Name;//代办人
                labelAgentIdenNO.Text = tmpInfo.Kin.Memo;//代办人身份证
            }
            #endregion

            this.labelAge.Text = this.orderManager.GetAge(register.Birthday, false);
            this.labelGender.Text = register.Sex.Name;
            this.lblCardNo.Text = register.PID.CardNO;

            lblPrintDate.Text = orderManager.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm");
          
            this.lbIdenNO.Text = register.IDCard;
            this.lblTel.Text = register.AddressHome + (!string.IsNullOrEmpty(register.PhoneHome) && !string.IsNullOrEmpty(register.AddressHome) ? "/" : "") + register.PhoneHome;

            #region 就诊日期

            DateTime dtNow = orderManager.GetDateTimeFromSysDateTime();

            if (register.SeeDoct.OperTime < new DateTime(2000, 1, 1))
            {
                this.labelSeeDate.Text = dtNow.ToString();
            }
            else
            {
                this.labelSeeDate.Text = register.SeeDoct.OperTime.ToString();
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
            //{3A2E38C1-3A99-45a3-82BD-0A1055298E69}{BB163312-E95F-42fc-97E1-B7A0E3CA01BA}
            this.lblAccountType.Text = register.Pact.Name;
            this.packid = register.Pact.ID;
              
            this.myReg = register;

            return 1;
        }

        /// <summary>
        /// 设置特殊处方标记
        /// </summary>
        /// <param name="alOrder"></param>
        private void SetSpeInfo(ArrayList alOrder)
        {
            //{A99D2E0B-C0F4-4d7e-9C42-E8B5A1E9BDFD}
            FS.HISFC.Models.Order.OutPatient.Order firstOrder = alOrder[0] as FS.HISFC.Models.Order.OutPatient.Order;
            FS.HISFC.Models.Base.Department recipeDept = SOC.HISFC.BizProcess.Cache.Common.GetDept(firstOrder.ReciptDept.ID);

            int speLevl = 1;
            foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOrder)
            {
                //{5FFE7DFF-358B-4e65-B798-1A2D7478A251}
                int level = ZDWY.Function.GetItemQualityForPrint(order);
                if (level > speLevl)
                {
                    speLevl = level;
                }
            }

            //3、毒麻精一；2、精二；1、普通；0、非药品
            lblSpeInfo.Visible = false;
            switch (speLevl)
            {
                //{564D4750-E736-432b-A5B6-B0B178AF1A11}
                case 4:
                    lblSpeInfo.Text = "  毒  ";
                    lblSpeInfo.Visible = true;
                    IsSpeRecipe = true;
                    break;
                case 3:
                    lblSpeInfo.Text = "麻、精一";
                    lblSpeInfo.Visible = true;
                    IsSpeRecipe = true;
                    break;
                case 2:
                    lblSpeInfo.Text = " 精  二 ";
                    lblSpeInfo.Visible = true;
                    IsSpeRecipe = true;
                    break;
                case 1:
                    if (myReg.DoctorInfo.Templet.RegLevel.IsEmergency)
                    {
                        lblSpeInfo.Text = " 急  诊 ";
                        lblSpeInfo.Visible = true;
                    }
                    //{A99D2E0B-C0F4-4d7e-9C42-E8B5A1E9BDFD}
                    //else if (myReg.DoctorInfo.Templet.Dept.Name.Contains("儿"))
                    else if (recipeDept.Name.Contains("儿"))
                    {
                        lblSpeInfo.Text = " 儿  科 ";
                        lblSpeInfo.Visible = true;
                        lblHelthInfo.Visible = true;
                        lblHeight.Visible = true;
                        
                        


                        #region 显示体重信息

                        FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();
                        string height = "";
                        string weight = "";
                        string SBP = "";
                        string DBP = "";
                        string tem = "";//体温
                        string bloodGlu = ""; //血糖
                        if (outOrderMgr.GetHealthInfo(this.myReg.ID, ref height, ref weight, ref SBP, ref DBP, ref tem, ref bloodGlu) == -1)
                        {
                            this.lblHeight.Text = "       KG";
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(weight))
                            {
                                this.lblHeight.Text = "       KG";
                            }
                            else
                            {
                                this.lblHeight.Text = weight.ToString() + "KG";
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        lblSpeInfo.Text = " 普  通 ";
                        lblSpeInfo.Visible = true;
                    }
                    break;
                default:
                    break;
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
