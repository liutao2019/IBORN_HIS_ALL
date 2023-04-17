using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using FS.HISFC.Models.Base;
using System.Drawing.Printing;
using System.Drawing;
using FS.HISFC.BizProcess.Interface.Fee;
using System.ComponentModel;

namespace FS.SOC.Local.OutpatientFee.FoSan
{
    public partial class ucQueryFeeDetail : FS.FrameWork.WinForms.Controls.ucBaseControl, IPrintFeeList
    {

        public ucQueryFeeDetail()
        {
            InitializeComponent();
            this.ucInvoiceTree1.Focus();
        }

        #region 变量
        /// <summary>
        /// 费用业务层
        /// </summary>
        FS.HISFC.BizLogic.Fee.Outpatient outPatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// 费用综合业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 患者挂号信息
        /// </summary>
        FS.HISFC.BizLogic.Registration.Register registerManager = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// 人员管理业务层
        /// </summary>
        FS.HISFC.BizLogic.Manager.Person perManager = new FS.HISFC.BizLogic.Manager.Person();

        /// <summary>
        /// 科室管理业务层
        /// </summary>
        FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        /// <summary>
        /// 打印纸张设置类
        /// </summary>
        FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();
        /// <summary>
        /// 打印
        /// </summary>
        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

        FS.HISFC.BizLogic.Pharmacy.Item phaManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        FS.HISFC.BizLogic.Fee.Item undrugManager = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// 清单是否显示备注
        /// </summary>
        private bool isShowRemark = false;

        /// <summary>
        /// 清单是否显示备注
        /// </summary>
        [Category("控件设置"), Description("清单是否显示备注"), DefaultValue(true)]
        public bool IsShowRemark
        {
            get
            {
                return this.isShowRemark;
            }
            set
            {
                this.isShowRemark = value;
            }
        }

        /// <summary>
        /// 相同项目是否汇总在一起打印
        /// </summary>
        private bool isShowTogether = true;

        /// <summary>
        /// 相同项目是否汇总在一起打印
        /// </summary>
        [Category("控件设置"), Description("相同项目是否汇总在一起打印"), DefaultValue(true)]
        public bool IsShowTogether
        {
            get
            {
                return this.isShowTogether;
            }
            set
            {
                this.isShowTogether = value;
            }
        }

        /// <summary>
        /// 是否按纸张打印，否则走哪打印到哪
        /// </summary>
        private bool isPrintByLatter = false;

        /// <summary>
        /// 是否按纸张打印，否则走哪打印到哪
        /// </summary>
        [Category("控件设置"), Description("是否按纸张打印，否则走哪打印到哪"), DefaultValue(false)]
        public bool IsPrintByLatter
        {
            get
            {
                return this.isPrintByLatter;
            }
            set
            {
                this.isPrintByLatter = value;
            }
        }

        #endregion

        private void txtInvoiceNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            Clear();
            string invoiceNO = this.txtInvoiceNO.Text.Trim();
            if(string.IsNullOrEmpty(invoiceNO))
            {
                MessageBox.Show("请输入发票号！");
                this.txtInvoiceNO.Focus();
                return;
            }
            invoiceNO = invoiceNO.PadLeft(12, '0');
            if (!this.SetInfo(invoiceNO))
            {
                Clear();
                return;
            }
        }

        private bool SetInfo(string invoiceNO)
        {
             ArrayList alBalance = outPatientManager.QueryBalancesByInvoiceNO(invoiceNO);
             if (alBalance == null)
             {
                 MessageBox.Show("查询发票信息失败！");
                 return false;
             }
             if (alBalance.Count > 0)
             {
                 foreach (FS.HISFC.Models.Fee.Outpatient.Balance invoice in alBalance)
                 {
                     if (invoice.TransType == TransTypes.Positive && invoice.CancelType == CancelTypes.Valid)
                     {
                         ArrayList alPatient = registerManager.QueryPatient(invoice.Patient.ID);
                         FS.HISFC.Models.Registration.Register r = alPatient[0] as FS.HISFC.Models.Registration.Register;
                         this.lblName.Text =  r.Name;
                         //修改成收费时间
                         this.lblDate.Text = invoice.BalanceOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss"); 
                         this.lblCardNO.Text = r.PID.CardNO;
                         this.lblPrintDate.Text = outPatientManager.GetSysDateTime();
                         this.lblTopTotCost.Text = invoice.FT.TotCost.ToString();
                         this.lblTopOwnCost.Text = (invoice.FT.OwnCost - invoice.FT.RebateCost).ToString();
                         this.lblTopPubCost.Text = invoice.FT.PubCost.ToString();
                         this.lblTopRebateCost.Text = invoice.FT.RebateCost.ToString();
                         this.lblInvoiceNo.Visible = false;
                         this.nlbPactName.Text = r.Pact.Name;
                         this.lblTotCost.Text = invoice.FT.TotCost.ToString();
                         this.lblOwnCost.Text = (invoice.FT.OwnCost - invoice.FT.RebateCost).ToString();
                         this.lblPubCost.Text = invoice.FT.PubCost.ToString();
                         this.lblRebateCost.Text = invoice.FT.RebateCost.ToString();
                         this.lblDept.Text = r.DoctorInfo.Templet.Dept.ID.ToString();
                         this.neuLabel19.Text = invoice.Invoice.ID.ToString();
                         FS.FrameWork.Models.NeuObject obj = perManager.GetPersonByID(invoice.BalanceOper.ID);
                         if (obj != null)
                         {
                             this.lblFeeOper.Text = obj.ID;
                         }
                         if (!SetFeeInfo(invoiceNO))
                         {
                             return false;
                         }
                         int row = this.neuSpread1_Sheet1.RowCount;
                         this.neuSpread1_Sheet1.Cells[row-1,7].Text="'";
                         return true;
                     }
                 }
             }
             return false;

        }

        private bool SetFeeInfo(string invoiceNO)
        {
            if (!IsShowTogether)
            {
                ArrayList alFee = outPatientManager.QueryFeeItemListsByInvoiceNO(invoiceNO);
                if (alFee == null)
                {
                    MessageBox.Show("查询费用明细失败！" + outPatientManager.Err);
                    return false;
                }
                if (alFee.Count == 0)
                {
                    MessageBox.Show("该发票号不存在，请重新输入！");
                    this.txtInvoiceNO.Focus();
                    this.txtInvoiceNO.SelectAll();
                    return false;
                }
                int count = 0;
                decimal cost = 0;
                decimal totOwnCost = 0;
                decimal totPubCost = 0;
                decimal totRebateCost = 0;
                string userCode = "";
                FS.HISFC.Models.Fee.Outpatient.FeeItemList tempf = null;
                FS.HISFC.Models.Pharmacy.Item item = null;
                FS.HISFC.Models.Fee.Item.Undrug undrug = null;
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in alFee)
                {
                    string priceUnit = string.Empty;
                    decimal price = 0m, qty = 0m;
                    if (f.PayType == PayTypes.Balanced && f.CancelType == CancelTypes.Valid)
                    {
                        price = f.Item.Price;
                        priceUnit = f.Item.PriceUnit;
                        qty = f.Item.Qty;
                        if (f.Item.ItemType == EnumItemType.Drug)
                        {
                            item = phaManager.GetItem(f.Item.ID);
                            if (item == null)
                            {
                                MessageBox.Show("查询药品信息失败！" + phaManager.Err);
                                return false;
                            }
                            userCode = item.NameCollection.UserCode;
                            if (f.Item.PriceUnit != item.PackUnit)
                            {
                                price = FS.FrameWork.Public.String.FormatNumber(f.Item.Price / f.Item.PackQty, 2);
                            }
                            else
                            {
                                qty = qty / item.PackQty;
                            }
                        }
                        else
                        {
                            undrug = undrugManager.GetValidItemByUndrugCode(f.Item.ID);
                            if (undrug == null)
                            {
                                MessageBox.Show("查询药品信息失败！" + phaManager.Err);
                                return false;
                            }
                            userCode = undrug.UserCode;
                        }

                        count = this.neuSpread1_Sheet1.Rows.Count;
                        this.neuSpread1_Sheet1.Rows.Add(count, 1);
                        this.neuSpread1_Sheet1.Cells[count, 0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                        this.neuSpread1_Sheet1.Cells[count, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        this.neuSpread1_Sheet1.Cells[count, 0].Text = userCode;
                        this.neuSpread1_Sheet1.Cells[count, 1].Text = f.Item.Name;
                        this.neuSpread1_Sheet1.Cells[count, 2].Text = f.Item.Specs;
                        this.neuSpread1_Sheet1.Cells[count, 3].Text = priceUnit;
                        this.neuSpread1_Sheet1.Cells[count, 4].Text = FS.FrameWork.Public.String.FormatNumber(qty, 2).ToString();
                        this.neuSpread1_Sheet1.Cells[count, 5].Text = FS.FrameWork.Public.String.FormatNumber(price, 2).ToString();
                        this.neuSpread1_Sheet1.Cells[count, 6].Text = (f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost).ToString();
                        //备注
                        this.neuSpread1_Sheet1.Cells[count, 7].Text = this.SetRemark(f.RecipeNO, f.SequenceNO);
                        cost += f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                        totOwnCost += f.FT.OwnCost + f.FT.PayCost;
                        totRebateCost += f.FT.RebateCost;
                        totPubCost += f.FT.PubCost;
                        if (tempf == null)
                        {
                            tempf = f;
                        }
                    }
                }
                this.lblOwnCost.Text = (totOwnCost - totRebateCost).ToString();
                this.lblPubCost.Text = totPubCost.ToString();
                this.lblRebateCost.Text = totRebateCost.ToString();
                this.lblTotCost.Text = cost.ToString();
                this.lblTopOwnCost.Text = (totOwnCost - totRebateCost).ToString();
                this.lblTopPubCost.Text = totPubCost.ToString();
                this.lblTopRebateCost.Text = totRebateCost.ToString();
                this.lblTopTotCost.Text = cost.ToString();

                FS.FrameWork.Models.NeuObject obj = perManager.GetPersonByID(tempf.RecipeOper.ID);
                if (obj != null)
                {
                    lblDoct.Text = obj.ID;
                }

                obj = deptManager.GetDeptmentById(tempf.RecipeOper.Dept.ID);
                if (obj != null)
                {
                    lblDept.Text = obj.Name;
                }

                return true;
            }
            else
            {
                ArrayList alFee = outPatientManager.QueryFeeItemListsTogetherByInvoiceNO(invoiceNO);
                if (alFee == null)
                {
                    MessageBox.Show("查询费用明细失败！" + outPatientManager.Err);
                    return false;
                }
                if (alFee.Count == 0)
                {
                    MessageBox.Show("该发票号不存在，请重新输入！");
                    this.txtInvoiceNO.Focus();
                    this.txtInvoiceNO.SelectAll();
                    return false;
                }
                int count = 0;
                decimal cost = 0;
                decimal totOwnCost = 0;
                decimal totPubCost = 0;
                decimal totRebateCost = 0;
                string userCode = "";
                FS.HISFC.Models.Fee.Outpatient.FeeItemList tempf = null;
                FS.HISFC.Models.Pharmacy.Item item = null;
                FS.HISFC.Models.Fee.Item.Undrug undrug = null;
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in alFee)
                {
                    string priceUnit = string.Empty;
                    decimal price = 0m, qty = 0m;
                    if (f.PayType == PayTypes.Balanced && f.CancelType == CancelTypes.Valid)
                    {
                        price = f.Item.Price;
                        priceUnit = f.Item.PriceUnit;
                        qty = f.Item.Qty;
                        if (f.Item.ItemType == EnumItemType.Drug)
                        {
                            item = phaManager.GetItem(f.Item.ID);
                            if (item == null)
                            {
                                MessageBox.Show("查询药品信息失败！" + phaManager.Err);
                                return false;
                            }
                            userCode = item.NameCollection.UserCode;
                            if (f.Item.PriceUnit != item.PackUnit)
                            {
                                price = FS.FrameWork.Public.String.FormatNumber(f.Item.Price / f.Item.PackQty, 4);
                            }
                            else
                            {
                                qty = qty / item.PackQty;
                            }
                        }
                        else
                        {
                            undrug = undrugManager.GetValidItemByUndrugCode(f.Item.ID);
                            if (undrug == null)
                            {
                                MessageBox.Show("查询药品信息失败！" + phaManager.Err);
                                return false;
                            }
                            userCode = undrug.UserCode;
                        }

                        count = this.neuSpread1_Sheet1.Rows.Count;
                        this.neuSpread1_Sheet1.Rows.Add(count, 1);
                        this.neuSpread1_Sheet1.Cells[count, 0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                        this.neuSpread1_Sheet1.Cells[count, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        this.neuSpread1_Sheet1.Cells[count, 0].Text = userCode;
                        this.neuSpread1_Sheet1.Cells[count, 1].Text = f.Item.Name;
                        this.neuSpread1_Sheet1.Cells[count, 2].Text = f.Item.Specs;
                        this.neuSpread1_Sheet1.Cells[count, 3].Text = priceUnit;
                        this.neuSpread1_Sheet1.Cells[count, 4].Text = FS.FrameWork.Public.String.FormatNumber(qty, 2).ToString();
                        this.neuSpread1_Sheet1.Cells[count, 5].Text = FS.FrameWork.Public.String.FormatNumber(price, 4).ToString();
                        this.neuSpread1_Sheet1.Cells[count, 6].Text = (f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost).ToString();
                        //备注 --项目汇总打印不显示备注
                        this.neuSpread1_Sheet1.Cells[count, 7].Text = "";
                        cost += f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                        totOwnCost += f.FT.OwnCost + f.FT.PayCost;
                        totRebateCost += f.FT.RebateCost;
                        totPubCost += f.FT.PubCost;
                        if (tempf == null)
                        {
                            tempf = f;
                        }
                    }
                }
                this.lblOwnCost.Text = (totOwnCost - totRebateCost).ToString();
                this.lblPubCost.Text = totPubCost.ToString();
                this.lblRebateCost.Text = totRebateCost.ToString();
                this.lblTotCost.Text = cost.ToString();
                this.lblTopOwnCost.Text = (totOwnCost - totRebateCost).ToString();
                this.lblTopPubCost.Text = totPubCost.ToString();
                this.lblTopRebateCost.Text = totRebateCost.ToString();
                this.lblTopTotCost.Text = cost.ToString();

                FS.FrameWork.Models.NeuObject obj = perManager.GetPersonByID(tempf.RecipeOper.ID);
                if (obj != null)
                {
                    lblDoct.Text = obj.ID;
                }

                obj = deptManager.GetDeptmentById(tempf.RecipeOper.Dept.ID);
                if (obj != null)
                {
                    lblDept.Text = obj.Name;
                }

                return true;
            }
        }

        private void Clear()
        {
            this.lblCardNO.Text = string.Empty;
            this.lblDate.Text = string.Empty;
            this.lblPrintDate.Text = string.Empty;
            this.lblDept.Text = string.Empty;
            this.lblName.Text = string.Empty;
            this.lblFeeOper.Text = string.Empty;
            this.lblDoct.Text = string.Empty;
            this.lblTopTotCost.Text = string.Empty;
            this.lblTopOwnCost.Text = string.Empty;
            this.lblTopPubCost.Text = string.Empty;
            this.lblTopRebateCost.Text = string.Empty;
            this.lblTotCost.Text = string.Empty;
            this.lblOwnCost.Text = string.Empty;
            this.lblPubCost.Text = string.Empty;
            this.lblRebateCost.Text = string.Empty;
            this.neuLabel19.Text = string.Empty;
            int count = this.neuSpread1_Sheet1.Rows.Count;
            if (count > 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, count);
            }
        }
        /// <summary>
        /// 获得处方备注
        /// </summary>
        private string SetRemark(string recipeNO, int sequenceNO)
        {
            string remark = null;
            string sql = @"select t.remark from met_ord_recipedetail t
                           where t.recipe_no ='{0}'
                           and   t.recipe_seq ='{1}'";
            try
            {
                sql = string.Format(sql, recipeNO, sequenceNO);
            }
            catch (Exception ex)
            {
                ;
                return null;
            }
            if (this.registerManager.ExecSqlReturnOne(sql) != "-1")
            {
                remark = this.registerManager.ExecSqlReturnOne(sql);
            }
            return remark;
        }

        /// <summary>
        /// 转换查询方式
        /// </summary>
        /// <returns></returns>
        private bool changeQueryType()
        {
            if (this.neuLabel2.Text == "卡号")
            {
                this.neuLabel2.Text = "姓名";
            }
            else if (this.neuLabel2.Text == "姓名")
            {
                this.neuLabel2.Text = "卡号";  
            }
            return true;
        }

        private void ucQueryFeeDetail_Load(object sender, EventArgs e)
        {
            this.ucInvoiceTree1.evnCardNoFind += new CardNoFind(ucInvoiceTree1_evnCardNoFind);
            this.ucInvoiceTree1.evnInvoiceNoFind += new InvoiceNoFind(ucInvoiceTree1_evnInvoiceNoFind);
            this.ucInvoiceTree1.evnInvoiceSelectChange += new InvoiceNodeSelectChange(ucInvoiceTree1_evnInvoiceSelectChange);
            this.Clear();
            this.ucInvoiceTree1.Focus();

            this.lblTitle.Text = this.phaManager.Hospital.Name + "门诊费用清单";
            if (this.IsShowRemark)
            {
                this.neuSpread1_Sheet1.Columns[7].Visible = true;
            }
            else
            {
                this.neuSpread1_Sheet1.Columns[7].Visible = false;
            }
        }

        void ucInvoiceTree1_evnInvoiceSelectChange(object sender, FS.HISFC.Models.Fee.Outpatient.Balance invoice)
        {
            this.Clear();

            if (invoice == null)
                return;

            this.txtInvoiceNO.Text = invoice.Invoice.ID;

            this.lblInvoiceNo.Text = invoice.Invoice.ID;

            txtInvoiceNO_KeyDown(null, new KeyEventArgs(Keys.Enter));

        }

        void ucInvoiceTree1_evnInvoiceNoFind()
        {
            this.Clear();

            this.txtInvoiceNO.Text = "";
        }

        void ucInvoiceTree1_evnCardNoFind()
        {
            this.Clear();

            this.txtInvoiceNO.Text = "";
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.plPrint.Dock = DockStyle.None;

            //字走到哪打到哪的方式，故不设置纸张名称，而是由计算得出
            int height = 10;

            int rowCount = this.neuSpread1_Sheet1.RowCount;
            //动态可变高度，设计器初始行数建议调整为0，即只有行头
            int addHeight = rowCount * (int)this.neuSpread1_Sheet1.Rows[0].Height;

            int addHeight1 = 4 * (int)this.neuSpread1_Sheet1.Rows[0].Height;

            //纸张实际高度由固定长度+动态可变长度组成
            height = (addHeight + this.neuPanel2.Height + this.neuPanel6.Height + addHeight1);

            this.plPrint.Size = new System.Drawing.Size(850, 400);
            FS.HISFC.Models.Base.PageSize ps = new PageSize();
            if (IsPrintByLatter)
            {
                int iPage = 1;
                iPage = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(FS.FrameWork.Function.NConvert.ToDecimal(height.ToString())/550).ToString());
                ps = new PageSize("222", 850, iPage*550);
            }
            else
            {
                ps = new PageSize("222", 850, height);
            }

            print.SetPageSize(ps);

            this.neuPanel6.Dock = DockStyle.None;

            this.neuPanel6.Location = new Point(this.neuPanel2.Location.X, height - this.neuPanel6.Height-3 * (int)this.neuSpread1_Sheet1.Rows[0].Height);

            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            if (((FS.HISFC.Models.Base.Employee)this.perManager.Operator).IsManager)
            {
                print.PrintPreview(0, 0, this.plPrint);
            }
            else
            {
                print.PrintPage(0, 0, this.plPrint);
            }
            this.neuPanel6.Dock = DockStyle.Bottom;
            this.plPrint.Dock = DockStyle.Fill;
            
            return base.OnPrint(sender, neuObject);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F2)
            {
                this.changeQueryType();
            }
            return base.ProcessDialogKey(keyData);
                
        }

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                switch (this.neuLabel2.Text)
                {
                    case "卡号":
                        string strCard = txtCardNo.Text.Trim();
                        if (string.IsNullOrEmpty(strCard))
                        {
                            return;
                        }

                        FS.HISFC.Models.Account.AccountCard objCard = new FS.HISFC.Models.Account.AccountCard();
                        int iTemp = feeIntegrate.ValidMarkNO(strCard, ref objCard);
                        if (iTemp <= 0 || objCard == null)
                        {
                            MessageBox.Show("无效卡号，请联系管理员！");

                            return;
                        }

                        this.ucInvoiceTree1.CardNo = objCard.Patient.PID.CardNO;
                        break;
                    case "姓名":
                        string name = txtCardNo.Text.Trim();
                        if (string.IsNullOrEmpty(name))
                        {
                            return;
                        }
                        string cardNO = this.GetCardNoByName(name);
                        if (string.IsNullOrEmpty(cardNO))
                        {
                            return;
                        }
                        if (!cardNO.StartsWith("9"))
                        {
                            this.ucInvoiceTree1.CardNo = cardNO;
                        }
                        else
                        {
                            ArrayList al = this.outPatientManager.QueryBalancesByCardNO(cardNO);
                            if (al.Count > 0 && al != null)
                            {
                                FS.HISFC.Models.Fee.Outpatient.Balance balance = al[0] as FS.HISFC.Models.Fee.Outpatient.Balance;
                                if (!string.IsNullOrEmpty(balance.Invoice.ID))
                                {
                                    this.SetInfo(balance.Invoice.ID);
                                }
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 通过患者姓名检索患者挂号信息
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private string GetCardNoByName(string Name)
        {
            frmQueryPatientByName f = new frmQueryPatientByName();

            f.QueryByName(Name);
            DialogResult dr = f.ShowDialog();

            if (dr == DialogResult.OK)
            {
                string CardNo = f.SelectedCardNo;
                f.Dispose();
                return CardNo;
            }

            f.Dispose();

            return "";
        }

        #region IPrintFeeList 成员

        public bool PrintFeeList()
        {
            this.OnPrint(null, null);
            return true;
        }

        public bool SetValue(FS.HISFC.Models.Fee.Outpatient.Balance invoice)
        {
            if (invoice == null)
            {
                return false;
            }

            this.ucQueryFeeDetail_Load(null, null);

            this.txtInvoiceNO.Text = invoice.Invoice.ID;

            if (!this.SetInfo(invoice.Invoice.ID))
            {
                Clear();
                return false;
            }

            return true;
        }

        #endregion
    }

}
