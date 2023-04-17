using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using Neusoft.HISFC.Models.Base;
using System.Drawing.Printing;
using System.Drawing;
using Neusoft.HISFC.BizProcess.Interface.Fee;
using System.ComponentModel;

namespace Neusoft.SOC.Local.OutpatientFee.FoSi
{
    public partial class ucQueryFeeDetail : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, IPrintFeeList
    {
        /// <summary>
        /// {56DA1009-B0FD-42cf-AA91-5B054EA49D13}
        /// </summary>
        public ucQueryFeeDetail()
        {
            InitializeComponent();
            this.ucInvoiceTree1.Focus();
        }

        #region 变量
        /// <summary>
        /// 费用业务层
        /// </summary>
        Neusoft.HISFC.BizLogic.Fee.Outpatient outPatientManager = new Neusoft.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// 费用综合业务层
        /// </summary>
        protected Neusoft.HISFC.BizProcess.Integrate.Fee feeIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 患者挂号信息
        /// </summary>
        Neusoft.HISFC.BizLogic.Registration.Register registerManager = new Neusoft.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// 人员管理业务层
        /// </summary>
        Neusoft.HISFC.BizLogic.Manager.Person perManager = new Neusoft.HISFC.BizLogic.Manager.Person();

        /// <summary>
        /// 科室管理业务层
        /// </summary>
        Neusoft.HISFC.BizLogic.Manager.Department deptManager = new Neusoft.HISFC.BizLogic.Manager.Department();
        /// <summary>
        /// 打印纸张设置类
        /// </summary>
        Neusoft.HISFC.BizLogic.Manager.PageSize psManager = new Neusoft.HISFC.BizLogic.Manager.PageSize();
        /// <summary>
        /// 打印
        /// </summary>
        Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();

        Neusoft.HISFC.BizLogic.Pharmacy.Item phaManager = new Neusoft.HISFC.BizLogic.Pharmacy.Item();

        Neusoft.HISFC.BizLogic.Fee.Item undrugManager = new Neusoft.HISFC.BizLogic.Fee.Item();

        Neusoft.HISFC.BizProcess.Integrate.Order orderMgr = new Neusoft.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// 清单是否显示备注
        /// </summary>
        private bool isShowRemark = true;

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
        private bool isShowTogether = false;

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

        /// <summary>
        /// 是否启用权限设置
        /// </summary>
        private bool isUsePrivilegeSet = false;

        /// <summary>
        /// 是否启用权限设置
        /// </summary>
        [Category("控件设置"), Description("是否启用权限设置"), DefaultValue(false)]
        public bool IsUsePrivilegeSet
        {
            get
            {
                return this.isUsePrivilegeSet;
            }
            set
            {
                this.isUsePrivilegeSet = value;
            }
        }

        /// <summary>
        /// 设置权限工号，用;分割
        /// </summary>
        private string privilegeOperCode = "";

        /// <summary>
        /// 设置权限工号，用;分割
        /// </summary>
        [Category("控件设置"), Description("设置权限工号，用;分割"), DefaultValue(false)]
        public string PrivilegeOperCode
        {
            get
            {
                return this.privilegeOperCode;
            }
            set
            {
                this.privilegeOperCode = value;
            }
        }

        /// <summary>
        /// 是否在树里显示“终端发票”和“收费员发票”字样
        /// </summary>
        private bool isShowInvoiceCharacters = true;

        /// <summary>
        /// 是否在树里显示“终端发票”和“收费员发票”字样
        /// </summary>
        [Category("控件设置"), Description("是否在树里显示“终端发票”和“收费员发票”字样"), DefaultValue(false)]
        public bool IsShowInvoiceCharacters
        {
            get
            {
                return this.isShowInvoiceCharacters;
            }
            set
            {
                this.isShowInvoiceCharacters = value;
            }
        }

        /// <summary>
        /// 药品的发药科室
        /// </summary>
        private string pharmacyDept = string.Empty;

        [Category("门诊注射单设置"), Description("药品的发药科室：为空的时候，打印全部的药品!")]
        public string PharmacyDept
        {
            get
            {
                return this.pharmacyDept;
            }
            set
            {
                this.pharmacyDept = value;
            }
        }

        /// <summary>
        /// 非药品的用法
        /// </summary>
        private string unDrugUsage = string.Empty;

        [Category("门诊注射单设置"), Description("非药品的用法为空：打印全部的非药品!")]
        public string UnDrugUsage
        {
            get
            {
                return this.unDrugUsage;
            }
            set
            {
                this.unDrugUsage = value;
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

        public bool SetInfo(string invoiceNO)
        {
             ArrayList alBalance = outPatientManager.QueryBalancesByInvoiceNO(invoiceNO);
             if (alBalance == null)
             {
                 MessageBox.Show("查询发票信息失败！");
                 return false;
             }
             if (alBalance.Count > 0)
             {
                 foreach (Neusoft.HISFC.Models.Fee.Outpatient.Balance invoice in alBalance)
                 {
                     if (invoice.TransType == TransTypes.Positive && invoice.CancelType == CancelTypes.Valid)
                     {
                         ArrayList alPatient = registerManager.QueryPatient(invoice.Patient.ID);
                         Neusoft.HISFC.Models.Registration.Register r = alPatient[0] as Neusoft.HISFC.Models.Registration.Register;
                         this.lblName.Text =  r.Name;
                         //修改成收费时间
                         this.lblDate.Text = invoice.BalanceOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss"); 
                         this.lblCardNO.Text = r.PID.CardNO;
                         this.lblPrintDate.Text = outPatientManager.GetSysDateTime();
                         this.lblTopTotCost.Text = invoice.FT.TotCost.ToString();
                         this.lblTopOwnCost.Text = (invoice.FT.OwnCost - invoice.FT.RebateCost).ToString();
                         this.lblTopPubCost.Text = invoice.FT.PubCost.ToString();
                         this.lblTopRebateCost.Text = invoice.FT.RebateCost.ToString();
                         this.lblTotCost.Text = invoice.FT.TotCost.ToString();
                         this.lblOwnCost.Text = (invoice.FT.OwnCost - invoice.FT.RebateCost).ToString();
                         this.lblPubCost.Text = invoice.FT.PubCost.ToString();
                         this.lblRebateCost.Text = invoice.FT.RebateCost.ToString();
                         this.lblDept.Text = r.DoctorInfo.Templet.Dept.ID.ToString();
                         Neusoft.FrameWork.Models.NeuObject obj = perManager.GetPersonByID(invoice.BalanceOper.ID);
                         if (obj != null)
                         {
                             this.lblFeeOper.Text = '('+obj.ID+')'+obj.Name;
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

        public void SetFormForPreview(string invoiceNO)
        {
            this.Clear();
            this.ucInvoiceTree1.Visible = false;
            this.neuGroupBox1.Visible = false;
            this.SetInfo(invoiceNO);
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
                Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList tempf = null;
                Neusoft.HISFC.Models.Pharmacy.Item item = null;
                Neusoft.HISFC.Models.Fee.Item.Undrug undrug = null;
                foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList f in alFee)
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
                                price = Neusoft.FrameWork.Public.String.FormatNumber(f.Item.Price / f.Item.PackQty, 2);
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
                                MessageBox.Show("查询非药品信息失败！" + phaManager.Err);
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
                        this.neuSpread1_Sheet1.Cells[count, 4].Text = Neusoft.FrameWork.Public.String.FormatNumber(qty, 2).ToString();
                        this.neuSpread1_Sheet1.Cells[count, 5].Text = Neusoft.FrameWork.Public.String.FormatNumber(price, 2).ToString();
                        this.neuSpread1_Sheet1.Cells[count, 6].Text = (f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost).ToString();
                        //备注
                        this.neuSpread1_Sheet1.Cells[count, 7].Text = this.SetRemark(f.RecipeNO, f.SequenceNO);
                        this.neuSpread1_Sheet1.Cells[count, 8].Text = f.Days.ToString();
                        this.neuSpread1_Sheet1.Cells[count, 9].Text = f.Order.Frequency.ID;
                        this.neuSpread1_Sheet1.Cells[count, 10].Text = f.Order.Usage.Name;
                        
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
                #region 暂时屏蔽（避免特点医保记账金额为0）
                //this.lblOwnCost.Text = (totOwnCost - totRebateCost).ToString();
                //this.lblPubCost.Text = totPubCost.ToString();
                //this.lblRebateCost.Text = totRebateCost.ToString();
                //this.lblTotCost.Text = cost.ToString();
                //this.lblTopOwnCost.Text = (totOwnCost - totRebateCost).ToString();
                //this.lblTopPubCost.Text = totPubCost.ToString();
                //this.lblTopRebateCost.Text = totRebateCost.ToString();
                //this.lblTopTotCost.Text = cost.ToString();
                #endregion

                Neusoft.FrameWork.Models.NeuObject obj = perManager.GetPersonByID(tempf.RecipeOper.ID);
                if (obj != null)
                {
                    lblDoct.Text = '('+obj.ID+')'+obj.Name;
                }

                obj = deptManager.GetDeptmentById(tempf.RecipeOper.Dept.ID);
                if (obj != null)
                {
                    lblDept.Text = obj.Name;
                }
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
                Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList tempf = null;
                Neusoft.HISFC.Models.Pharmacy.Item item = null;
                Neusoft.HISFC.Models.Fee.Item.Undrug undrug = null;
                foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList f in alFee)
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
                                price = Neusoft.FrameWork.Public.String.FormatNumber(f.Item.Price / f.Item.PackQty, 4);
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
                        this.neuSpread1_Sheet1.Cells[count, 4].Text = Neusoft.FrameWork.Public.String.FormatNumber(qty, 2).ToString();
                        this.neuSpread1_Sheet1.Cells[count, 5].Text = Neusoft.FrameWork.Public.String.FormatNumber(price, 4).ToString();
                        this.neuSpread1_Sheet1.Cells[count, 6].Text = (f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost).ToString();
                        //备注 --项目汇总打印不显示备注
                        this.neuSpread1_Sheet1.Cells[count, 7].Text = "";
                        this.neuSpread1_Sheet1.Cells[count, 8].Text = f.Days.ToString();
                        this.neuSpread1_Sheet1.Cells[count, 9].Text = f.Order.Frequency.ID;
                        this.neuSpread1_Sheet1.Cells[count, 10].Text = f.Order.Usage.Name;
                        
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

                Neusoft.FrameWork.Models.NeuObject obj = perManager.GetPersonByID(tempf.RecipeOper.ID);
                if (obj != null)
                {
                    lblDoct.Text = '('+obj.ID+')'+obj.Name;
                }

                obj = deptManager.GetDeptmentById(tempf.RecipeOper.Dept.ID);
                if (obj != null)
                {
                    lblDept.Text = obj.Name;
                }
            }

            //上面显示的费用实付、报销金额信息错误，此处从发票表重新查询
            ArrayList alBalance = outPatientManager.QueryBalancesByInvoiceNO(invoiceNO);
            if (alBalance == null)
            {
                MessageBox.Show("查询发票信息失败！");
                return false;
            }
            if (alBalance.Count > 0)
            {
                foreach (Neusoft.HISFC.Models.Fee.Outpatient.Balance invoice in alBalance)
                {
                    if (invoice.TransType == TransTypes.Positive && invoice.CancelType == CancelTypes.Valid)
                    {
                        //修改成收费时间
                        this.lblDate.Text = invoice.BalanceOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss");
                        this.lblPrintDate.Text = outPatientManager.GetSysDateTime();
                        this.lblTopTotCost.Text = invoice.FT.TotCost.ToString();
                        this.lblTopOwnCost.Text = (invoice.FT.OwnCost - invoice.FT.RebateCost).ToString();
                        this.lblTopPubCost.Text = invoice.FT.PubCost.ToString();
                        this.lblTopRebateCost.Text = invoice.FT.RebateCost.ToString();
                        this.lblTotCost.Text = invoice.FT.TotCost.ToString();
                        this.lblOwnCost.Text = (invoice.FT.OwnCost - invoice.FT.RebateCost).ToString();
                        this.lblPubCost.Text = invoice.FT.PubCost.ToString();
                        this.lblRebateCost.Text = invoice.FT.RebateCost.ToString();
                        return true;
                    }
                }
            }

            return true;
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
        private void ucQueryFeeDetail_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

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

            if (IsUsePrivilegeSet)
            {
                string opercode = this.outPatientManager.Operator.ID;
                if (this.privilegeOperCode.Contains(opercode))
                {

                    //this.txtCardNo.Enabled = true;
                    //this.ucInvoiceTree1.IsCardNoEnable = true;
                }
                else
                {
                    this.txtCardNo.Enabled = false;
                    this.ucInvoiceTree1.IsCardNoEnable = false;
                }
                
            }
            //判断是否在树里显示“终端发票”和“收费员发票”字样
            if(!this.IsShowInvoiceCharacters)
            {
                this.ucInvoiceTree1.IsShowInvoiceCharacters = false;
            }
        }

        void ucInvoiceTree1_evnInvoiceSelectChange(object sender, Neusoft.HISFC.Models.Fee.Outpatient.Balance invoice)
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

            this.plPrint.Size = new System.Drawing.Size(850, 550);
            Neusoft.HISFC.Models.Base.PageSize ps = new PageSize();
            if (IsPrintByLatter)
            {
                int iPage = 1;
                iPage = Neusoft.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Neusoft.FrameWork.Function.NConvert.ToDecimal(height.ToString())/550).ToString());
                ps = new PageSize("550", 850, iPage*550);
            }
            else
            {
                ps = new PageSize("550", 850, height);
            }

            print.SetPageSize(ps);

            this.neuPanel6.Dock = DockStyle.None;

            this.neuPanel6.Location = new Point(this.neuPanel2.Location.X, height - this.neuPanel6.Height-3 * (int)this.neuSpread1_Sheet1.Rows[0].Height);

            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            if (((Neusoft.HISFC.Models.Base.Employee)this.perManager.Operator).IsManager)
            {
                //预览时不需要显示天数，频次和用法这3列
                this.neuSpread1_Sheet1.Columns[8].Visible = false;
                this.neuSpread1_Sheet1.Columns[9].Visible = false;
                this.neuSpread1_Sheet1.Columns[10].Visible = false;
                print.PrintPreview(0, 0, this.plPrint);
                //预览后界面恢复显示
                this.neuSpread1_Sheet1.Columns[8].Visible = true;
                this.neuSpread1_Sheet1.Columns[9].Visible = true;
                this.neuSpread1_Sheet1.Columns[10].Visible = true;
            }
            else
            {
                //打印时不需要显示天数，频次和用法这3列
                this.neuSpread1_Sheet1.Columns[8].Visible = false;
                this.neuSpread1_Sheet1.Columns[9].Visible = false;
                this.neuSpread1_Sheet1.Columns[10].Visible = false;
                print.PrintPage(0, 0, this.plPrint);
                //打印后界面恢复显示
                this.neuSpread1_Sheet1.Columns[8].Visible = true;
                this.neuSpread1_Sheet1.Columns[9].Visible = true;
                this.neuSpread1_Sheet1.Columns[10].Visible = true;
            }
            this.neuPanel6.Dock = DockStyle.Bottom;
            this.plPrint.Dock = DockStyle.Fill;
            
            return base.OnPrint(sender, neuObject);
        }

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string strCard = txtCardNo.Text.Trim();
                if (string.IsNullOrEmpty(strCard))
                    return;

                Neusoft.HISFC.Models.Account.AccountCard objCard = new Neusoft.HISFC.Models.Account.AccountCard();
                int iTemp = feeIntegrate.ValidMarkNO(strCard, ref objCard);
                if (iTemp <= 0 || objCard == null)
                {
                    MessageBox.Show("无效卡号，请联系管理员！");

                    return;
                }

                this.ucInvoiceTree1.CardNo = objCard.Patient.PID.CardNO;
            }
        }

        /// <summary>
        /// 工具栏
        /// </summary>
        private Neusoft.FrameWork.WinForms.Forms.ToolBarService toolBar = new Neusoft.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 工具栏初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {

            toolBar.AddToolButton("注射单打印", "门诊注射单打印", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.D打印输液卡, true, false, null);
            toolBar.AddToolButton("检查检验单打印", "检查检验申请单打印", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);

            return this.toolBar;
        }

        /// <summary>
        /// 按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "注射单打印":
                    this.PrintInjectScoutCard();
                    break;
                case "检查检验单打印":
                    this.ReprintPacsAndLis();
                    break;
                default:
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// 打印门诊注射单和输液卡
        /// </summary>
        private void PrintInjectScoutCard()
        {
            string invoiceNO = this.txtInvoiceNO.Text.Trim();
            if (string.IsNullOrEmpty(invoiceNO))
            {
                MessageBox.Show("请输入发票号！");
                this.txtInvoiceNO.Focus();
                return;
            }
            invoiceNO = invoiceNO.PadLeft(12, '0');

            if (string.IsNullOrEmpty(this.PharmacyDept))
            {
                this.PharmacyDept = "ALL";
            }

            if (string.IsNullOrEmpty(this.UnDrugUsage))
            {
                this.UnDrugUsage = "ALL";
            }

            ArrayList listFeeDetails = this.outPatientManager.QueryFeeDetailByInvoiceNOAndDrugDept(invoiceNO, this.PharmacyDept, this.UnDrugUsage);
            
            if (listFeeDetails == null || listFeeDetails.Count <= 0)
            {
                MessageBox.Show("没有需要打印的数据!");
                return;
            }

            Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeDetail = null;
            Neusoft.HISFC.Models.Order.OutPatient.Order orderInfo = null;
            Neusoft.HISFC.Models.Nurse.Inject injectInfo = null;
            Neusoft.HISFC.Models.Pharmacy.Item drugInfo = null;

            Neusoft.HISFC.Models.Registration.Register regTemp = this.registerManager.GetByClinic((listFeeDetails[0] as Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList).Patient.ID);
            if (regTemp == null || string.IsNullOrEmpty(regTemp.ID))
            {
                MessageBox.Show("没有找到患者的挂号信息!");
                return;
            }

            bool isReprint = false;  //是否重打印标志
            ArrayList listInjectInfo = new ArrayList();

            for (int i = 0; i < listFeeDetails.Count; i++)
            {
                feeDetail = listFeeDetails[i] as Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList;
                if (feeDetail.Item.ItemType == EnumItemType.Drug)
                {
                    if (feeDetail.Item.ID != "999")  //不是自备药品
                    {
                        drugInfo = this.phaManager.GetItem(feeDetail.Item.ID);
                        if (drugInfo == null || string.IsNullOrEmpty(drugInfo.ID))
                        {
                            MessageBox.Show("获取药品信息失败!【" + feeDetail.Item.ID + "," + feeDetail.Item.Name + "】");
                            return;
                        }
                    }                    
                }

                orderInfo = new Neusoft.HISFC.Models.Order.OutPatient.Order();
                if (feeDetail.Item.ItemType == EnumItemType.Drug)
                {
                    orderInfo = this.orderMgr.GetOneOrder(feeDetail.Patient.ID, feeDetail.Order.ID);
                    if (orderInfo != null && !string.IsNullOrEmpty(orderInfo.ID))
                    {
                        //电子方
                    }
                    else
                    {
                        //手工方
                        orderInfo = new Neusoft.HISFC.Models.Order.OutPatient.Order();
                        if (feeDetail.Item.ID != "999" && drugInfo != null && !string.IsNullOrEmpty(drugInfo.ID))
                        {
                            if (drugInfo.IsAllergy)
                            {
                                orderInfo.Item = drugInfo;
                                orderInfo.HypoTest = Neusoft.HISFC.Models.Order.EnumHypoTest.FreeHypoTest;
                            }
                            else
                            {
                                orderInfo.Item = drugInfo;
                                orderInfo.HypoTest = Neusoft.HISFC.Models.Order.EnumHypoTest.FreeHypoTest;
                            }
                        }
                    }
                }

                injectInfo = new Neusoft.HISFC.Models.Nurse.Inject();
                injectInfo.Patient.ID = feeDetail.Patient.ID;
                injectInfo.Patient.Name = regTemp.Name;
                injectInfo.Patient.Sex.ID = regTemp.Sex.ID;
                injectInfo.Patient.Birthday = regTemp.Birthday;
                injectInfo.Patient.Card.ID = regTemp.PID.CardNO;

                injectInfo.Item = feeDetail;
                injectInfo.Item.InjectCount = feeDetail.InjectCount; //院注次数

                //开方医生基本信息
                try
                {
                    injectInfo.Item.Order.Doctor.Name = (this.perManager.GetPersonByID(feeDetail.RecipeOper.ID)).Name;
                    injectInfo.Item.Order.Doctor.ID = feeDetail.RecipeOper.ID;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("查询医生基本信息失败!" + ex.Message);
                    return;
                }
                injectInfo.Item.Name = feeDetail.Item.Name;
                injectInfo.Item.Days = feeDetail.Days;

                if (orderInfo != null && !string.IsNullOrEmpty(orderInfo.ID))
                {
                    injectInfo.Memo = orderInfo.Memo;
                    injectInfo.Hypotest = orderInfo.HypoTest;
                }

                if (feeDetail.ConfirmedInjectCount > 0)
                {
                    isReprint = true;
                }
                else
                {
                    isReprint = false;
                }

                listInjectInfo.Add(injectInfo);

            }

            if (listInjectInfo == null && listInjectInfo.Count <= 0)
            {
                MessageBox.Show("没有需要打印的数据!");
                return;
            }

            Neusoft.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint printInject = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as Neusoft.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            if (printInject == null)
            {
                MessageBox.Show("没有维护门诊注射单打印接口!");
                return;
            }

            printInject.IsReprint = isReprint;
            printInject.Init(listInjectInfo);

        }

        /// <summary>
        /// 重新打印检查检验单
        /// </summary>
        private void ReprintPacsAndLis()
        {
            //string invoiceNO = this.txtInvoiceNO.Text.Trim();
            //if (string.IsNullOrEmpty(invoiceNO))
            //{
            //    MessageBox.Show("请输入发票号！");
            //    this.txtInvoiceNO.Focus();
            //    return;
            //}
            //invoiceNO = invoiceNO.PadLeft(12, '0');
            //ArrayList feeItemLists = this.outPatientManager.QueryFeeItemListsByInvoiceNO(invoiceNO);
            
            //if (feeItemLists != null && feeItemLists.Count > 0)
            //{
            //    Neusoft.HISFC.BizProcess.Interface.Fee.IOutpatientAfterFee afterFee = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.OutpatientFee.FoSi.Controls.ucCharge), typeof(Neusoft.HISFC.BizProcess.Interface.Fee.IOutpatientAfterFee)) as Neusoft.HISFC.BizProcess.Interface.Fee.IOutpatientAfterFee;

            //    Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList itemTemp = feeItemLists[0] as Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList;
            //    if (itemTemp.CancelType == Neusoft.HISFC.Models.Base.CancelTypes.Valid)
            //    {
            //        afterFee.AfterFee(feeItemLists, "");
            //    }
            //}

        }

        #region IPrintFeeList 成员

        public bool PrintFeeList()
        {
            this.OnPrint(null, null);
            return true;
        }

        public bool SetValue(Neusoft.HISFC.Models.Fee.Outpatient.Balance invoice)
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
