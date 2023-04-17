using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Base;

namespace FS.SOC.Local.InpatientFee.GuangZhou
{
    public partial class frmPopAlterRate : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmPopAlterRate()
        {
            InitializeComponent();
        }

        #region  "变量"
        FS.HISFC.BizLogic.Fee.InPatient FeeInPatient = new FS.HISFC.BizLogic.Fee.InPatient();
        FS.HISFC.BizProcess.Integrate.Fee ProcessFeeInPatient = new FS.HISFC.BizProcess.Integrate.Fee();
        #endregion
        #region "属性"
        private FS.HISFC.Models.Fee.Inpatient.FeeItemList f = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
        public FS.HISFC.Models.Fee.Inpatient.FeeItemList ItemList
        {
            get { return f; }
            set
            {
                f = value;
                //this.dateTimePicker1.Value = f.FeeInfo.DtFee;
                this.dateTimePicker1.Value = f.FeeOper.OperTime;
            }
        }
        private FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();
        public FS.HISFC.Models.RADT.PatientInfo Pinfo
        {
            get { return p; }
            set { p = value; }
        }
        /// <summary>
        /// 是否为修改费用时间
        /// </summary>
        private bool isChangeFeeDate = false;
        public bool IsChangeFeeDate
        {
            get
            {
                return this.isChangeFeeDate;
            }
            set
            {
                isChangeFeeDate = value;
            }
        }
        /// <summary>
        /// 是否不更改日期
        /// </summary>
        private bool isChangeDate = false;
        /// <summary>
        /// 是否不更改日期
        /// </summary>
        public bool IsChangeDate
        {
            set
            {
                this.isChangeDate = value;
            }
        }

        private bool isConfirm = false;

        public bool IsConfirm
        {
            get
            {
                return isConfirm;
            }
        }
        #endregion
        private void frmPopAlterRate_Load(object sender, System.EventArgs e)
        {
            try
            {
                //if (this.p.Pact.PayKind.ID !="03")
                //{
                //    this.checkBox1.Checked = false;
                //    this.checkBox1.Visible=false;
                //    this.button1.Visible = false;
                //    this.button2.Visible = false;
                //}
                //else
                //{
                //    this.checkBox1.Checked = true;
                //    this.checkBox1.Visible=true;
                //    this.button1.Visible = true;
                //    this.button2.Visible = true;
                //}
                FS.HISFC.Models.Fee.Inpatient.FeeItemList FeeList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                FeeList = (FS.HISFC.Models.Fee.Inpatient.FeeItemList)this.ItemList;
                //赋值
                this.txtItem.Text = FeeList.Item.Name; //项目名称
                this.txtQty.Text = FeeList.Item.Qty.ToString();//FeeList.Item.Amount.ToString(); //数量
                decimal totCost = 0, ownCost = 0, payCost = 0, pubCost = 0;
                if (FeeList.Item.DefPrice == 0)
                {
                    totCost = FeeList.FT.TotCost;
                    ownCost = FeeList.FT.OwnCost;
                    payCost = FeeList.FT.PayCost;
                    pubCost = FeeList.FT.PubCost;
                }
                else
                {
                    if (FeeList.Item.PackQty == 0)
                    {
                        FeeList.Item.PackQty = 1;
                    }
                    totCost = FS.FrameWork.Public.String.FormatNumber(FeeList.Item.DefPrice * FeeList.Item.Qty / FeeList.Item.PackQty, 2);
                    payCost = FeeList.FT.PayCost;
                    pubCost = FeeList.FT.PubCost;
                    ownCost = totCost - payCost - pubCost;
                }
                //费用总金额
                this.txtTot.Text = FS.FrameWork.Public.String.FormatNumber(totCost, 2).ToString("####.00");
                //自费金额
                this.txtOwnCost.Text = FS.FrameWork.Public.String.FormatNumber(ownCost, 2).ToString("####.00");
                //自付金额
                this.txtPayCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(payCost, 2);
                //记帐金额
                this.txtPubCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(pubCost, 2);
                //自费比例
                this.txtSelfRate.Text = FS.FrameWork.Public.String.FormatNumberReturnString(ownCost / totCost, 2);
                //自付比例
                if (totCost - ownCost == payCost)
                {
                    this.txtPayRate.Text = "1.00";
                }
                else
                {
                    this.txtPayRate.Text = FS.FrameWork.Public.String.FormatNumberReturnString(payCost / ((totCost - ownCost) == 0 ? 1 : (totCost - ownCost)), 2);

                }

                this.txtRebate.Text = FS.FrameWork.Public.String.FormatNumberReturnString(FeeList.FT.RebateCost, 2);
                if (ownCost == 0)
                {
                    this.txtRebateRate.Text = "1.00";
                }
                else
                {
                    this.txtRebateRate.Text = FS.FrameWork.Public.String.FormatNumberReturnString(FeeList.FT.RebateCost / (ownCost), 2);
                }
                string tpFlag = "";
                if (this.ItemList.Item.ItemType == EnumItemType.Drug)
                {
                    tpFlag = this.FeeInPatient.GetTPflag(FeeList.RecipeNO, FeeList.SequenceNO,1);
                }
                else 
                {
                    tpFlag = this.FeeInPatient.GetTPflag(FeeList.RecipeNO, FeeList.SequenceNO,0);
                }

                
                if (tpFlag == "4")
                {
                    this.lblMemo.Text = "备注：该项目已经改为特批项";
                    this.button2.Enabled = true;
                    this.button1.Enabled = false;
                }
                else 
                {
                    if (totCost - ownCost == 0 && this.p.Pact.PayKind.ID == "03")
                    {
                        this.txtPayRate.Text = this.p.Pact.Rate.PayRate.ToString();
                        this.txtPayCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(ownCost * this.p.Pact.Rate.PayRate, 2);
                        this.txtPubCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(ownCost- ownCost* this.p.Pact.Rate.PayRate, 2);
                        this.txtOwnCost.Text = "0.00";
                        this.txtSelfRate.Text = "0.00";
                    }

                    this.lblMemo.Visible = false;
                    this.button2.Enabled = false;
                    this.button1.Enabled = true;
                }

                foreach (Control c in this.Controls)
                {
                    c.KeyDown += new KeyEventHandler(c_KeyDown);
                }
                //				this.txtSelfRate.LostFocus+=new EventHandler(txtSelfRate_LostFocus);

                //				this.txtSelfRate.TextChanged+=new EventHandler(txtSelfRate_TextChanged);
                //				this.txtSelfRate.KeyPress+=new KeyPressEventHandler(txtSelfRate_KeyPress);
                this.txtSelfRate.Leave += new EventHandler(txtSelfRate_Leave);
                this.txtOwnCost.Leave += new EventHandler(txtOwnCost_Leave);
                this.txtPayRate.Leave += new EventHandler(txtPayRate_Leave);
                this.txtPayCost.Leave += new EventHandler(txtPayCost_Leave);
                this.txtRebateRate.Leave += new EventHandler(txtRebateRate_Leave);
                this.txtRebate.Leave += new EventHandler(txtRebate_Leave);

                if (this.isChangeFeeDate)
                {
                    this.txtTot.Enabled = false;
                    this.txtPayCost.Enabled = false;
                    this.txtPubCost.Enabled = false;
                    this.txtSelfRate.Enabled = false;
                    this.txtPayRate.Enabled = false;
                    this.txtOwnCost.Enabled = false;
                    this.dateTimePicker1.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        void txtRebate_Leave(object sender, EventArgs e)
        {
            if (decimal.Parse(this.txtRebate.Text) > decimal.Parse(this.txtOwnCost.Text))
            {
                MessageBox.Show("自费优惠金额维护错误,不应大于自费费用总额");
                this.txtRebate.Text = "0.00";
                return;
            }
            else
            {
                //按照自费优惠金额重新计算自费优惠比例
                if (decimal.Parse(this.txtRebate.Text) != 0)
                {
                    //decimal rate = 0m;
                    //rate = (FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtRebate.Text), 2) / FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtOwnCost.Text), 2));
                    this.txtRebateRate.Enabled = false;
                    this.txtRebateRate.Text = "0.00";

                }
               
            }
        }

        void txtRebateRate_Leave(object sender, EventArgs e)
        {
            try
            {

                if (FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtRebateRate.Text), 2) > 1 || FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtRebateRate.Text), 2) < 0)
                {
                    MessageBox.Show("自费费用优惠比例数值应在0-1之间");
                    this.txtSelfRate.Text = "0.00";
                    return;
                }
                else
                {
                    //按自费优惠比例重新计算优惠费用
                    if (decimal.Parse(this.txtRebateRate.Text) != 0)
                    {
                        this.txtRebate.Text = FS.FrameWork.Public.String.FormatNumberReturnString(decimal.Parse(this.txtOwnCost.Text) * decimal.Parse(this.txtRebateRate.Text), 2);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void c_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                System.Windows.Forms.SendKeys.Send("{tab}");
            }
        }
        private void btnQuit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (this.isChangeFeeDate)
            {
                if (this.Pinfo == null || this.ItemList == null)
                {
                    MessageBox.Show("传入的实体信息错误!");
                    return;
                }
                FS.HISFC.Models.Fee.Inpatient.FeeItemList FeeItemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                FeeItemList = (FS.HISFC.Models.Fee.Inpatient.FeeItemList)this.ItemList;

                //更新医嘱执行档用
                FS.HISFC.BizLogic.Order.Order ordMgr = new FS.HISFC.BizLogic.Order.Order();
                //建立事务
                //FS.neuFC.Management.Transaction t = new FS.neuFC.Management.Transaction(FS.neuFC.Management.Connection.Instance);
                //t.BeginTransaction();
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                this.ProcessFeeInPatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.FeeInPatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                ordMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //插入一条负记录
                FS.HISFC.Models.Fee.Inpatient.FeeItemList FClone1 = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                FClone1 = this.ItemList.Clone();

                //保存原来的处方号和流水号
                string OldRecipe = FClone1.RecipeNO;//FClone1.RecipeNO;
                int OldSequence = FClone1.SequenceNO;
                FClone1.IsNeedUpdateNoBackQty = true;//需要更新可退数量
                //if (this.FeeInPatient.FeeManagerReturn(this.Pinfo, FClone1) == -1)
                if (this.ProcessFeeInPatient.QuitItem(this.Pinfo, FClone1) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.FeeInPatient.Err);
                    return;
                }
                //插入一条正记录
                //赋值
                string NoteNo = "";
                if (FeeItemList.Item.ItemType == EnumItemType.Drug)
                {
                    NoteNo = this.FeeInPatient.GetDrugRecipeNO();
                }
                else
                {
                    NoteNo = this.FeeInPatient.GetUndrugRecipeNO();
                }
                this.ItemList.RecipeNO = NoteNo;
                this.ItemList.SequenceNO = 1;
                if (this.dateTimePicker1.Value < this.Pinfo.PVisit.InTime)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("费用时间不能小于入院时间！");
                    return;
                }

                if (this.dateTimePicker1.Value > ItemList.FeeOper.OperTime.AddDays(3))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("费用时间不能超过原费用时间3天！");
                    return;
                }

                if (this.dateTimePicker1.Value < ItemList.FeeOper.OperTime.AddDays(-3))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("费用时间不能小于原费用时间3天！");
                    return;
                }

                ItemList.FeeOper.OperTime = this.dateTimePicker1.Value;
                this.ItemList.User01 = "1";//用作判断标记在FeeManager中不重新调用计算比例函数

                if (this.ItemList.FT.TotCost != this.ItemList.FT.OwnCost + this.ItemList.FT.PayCost + this.ItemList.FT.PubCost+this.ItemList.FT.RebateCost)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("费用比例修改有误,总金额不等于分项之和");
                    return;
                }
                //Add By Maokb --当前操作员06-03-14
                this.ItemList.FeeOper.ID = this.FeeInPatient.Operator.ID;
                //插入调用组合业务收取
                if (this.ProcessFeeInPatient.FeeItem(this.Pinfo, this.ItemList) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.FeeInPatient.Err);
                    return;
                }
                //更新费用急诊审核标志
                if (this.ItemList.Item.ItemType == EnumItemType.Drug)
                {
                    FS.HISFC.BizLogic.Pharmacy.Item itemDrug = new FS.HISFC.BizLogic.Pharmacy.Item();
                    itemDrug.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    if (itemDrug.UpdateApplyOutRecipe(OldRecipe, OldSequence, this.ItemList.RecipeNO,
                        this.ItemList.SequenceNO) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新出库流水号失败！" + itemDrug.Err);
                        return;
                    }

                    if (this.FeeInPatient.UpdateEmergencyForDrug(this.ItemList.RecipeNO, this.ItemList.SequenceNO, true) < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新药品急诊标记" + this.FeeInPatient.Err);
                        return;
                    }
                    //更新医嘱执行档
                    if (ordMgr.UpdateExecRecipeNo(ItemList.Order.ID, true, ItemList.RecipeNO) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新医嘱执行档出错" + ordMgr.Err);
                        return;
                    }
                }
                else
                {
                    if (this.FeeInPatient.UpdateEmergencyForUndrug(this.ItemList.RecipeNO, this.ItemList.SequenceNO, true) < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新非药品急诊标记" + this.FeeInPatient.Err);
                        return;
                    }
                    //更新医嘱执行档
                    if (ordMgr.UpdateExecRecipeNo(ItemList.Order.ID, false, ItemList.RecipeNO) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新医嘱执行档出错" + ordMgr.Err);
                        return;
                    }
                }


                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("修改费用时间成功!", "提示");
                this.isConfirm = true;
                this.Close();
            }
            else
            {
                if (this.Pinfo == null || this.ItemList == null)
                {
                    MessageBox.Show("传入的实体信息错误!");
                    return;
                }
                FS.HISFC.Models.Fee.Inpatient.FeeItemList FeeItemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                FeeItemList = (FS.HISFC.Models.Fee.Inpatient.FeeItemList)this.ItemList;
                //判断是否更改了信息
                try
                {
                    decimal totCost = 0, ownCost = 0, payCost = 0, pubCost = 0;
                    if (FeeItemList.Item.DefPrice == 0)
                    {
                        totCost = FeeItemList.FT.TotCost;
                        ownCost = FeeItemList.FT.OwnCost;
                        payCost = FeeItemList.FT.PayCost;
                        pubCost = FeeItemList.FT.PubCost;
                    }
                    else
                    {
                        if (FeeItemList.Item.PackQty == 0)
                        {
                            FeeItemList.Item.PackQty = 1;
                        }
                        totCost = FS.FrameWork.Public.String.FormatNumber(FeeItemList.Item.DefPrice * FeeItemList.Item.Qty / FeeItemList.Item.PackQty, 2);
                        payCost = FeeItemList.FT.PayCost;
                        pubCost = FeeItemList.FT.PubCost;
                        ownCost = totCost - payCost - pubCost;
                    }
                    if ((decimal.Parse(this.txtPayCost.Text) == payCost) && (decimal.Parse(this.txtOwnCost.Text) == ownCost) && (decimal.Parse(this.txtRebate.Text) == FeeItemList.FT.RebateCost))
                    {
                        MessageBox.Show("比例和费用信息没有更改!请按退出按钮关闭窗口!");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                //更新医嘱执行档用
                FS.HISFC.BizLogic.Order.Order ordMgr = new FS.HISFC.BizLogic.Order.Order();
                //建立事务
                //FS.neuFC.Management.Transaction t = new FS.neuFC.Management.Transaction(FS.neuFC.Management.Connection.Instance);
                //t.BeginTransaction();
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                this.ProcessFeeInPatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.FeeInPatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                ordMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                //
                if (this.checkBox1.Checked)
                {
                    if (this.ItemList.Item.ItemType == EnumItemType.Drug)
                    {
                        ////清空肿瘤审药/高检费标志,防止多次调整，高检费翻倍
                        if (-1 == this.FeeInPatient.UpdatTPflag(this.ItemList.RecipeNO, this.ItemList.SequenceNO, "", 1))
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新肿瘤审药/高检费！" + FeeInPatient.Err);
                            return;
                        }
                    }
                    else 
                    {
                        //清空肿瘤审药/高检费标志，防止多次调整，高检费翻倍
                        if (-1 == this.FeeInPatient.UpdatTPflag(this.ItemList.RecipeNO, this.ItemList.SequenceNO, "", 0))
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新肿瘤审药/高检费！" + FeeInPatient.Err);
                            return;
                        }
                    }
                }

                //插入一条负记录
                FS.HISFC.Models.Fee.Inpatient.FeeItemList FClone1 = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                FClone1 = this.ItemList.Clone();
                FClone1.IsNeedUpdateNoBackQty = true;
                //if (FClone1.ExtFlag2.Length < 1)
                //{
                //    FClone1.ExtFlag2 = "04";					//比例调整标志 4 
                //}
                //else
                //{
                //    FClone1.ExtFlag2 = FClone1.ExtFlag2.Substring(0, 1) + "4";
                //}
                if (FClone1.FTSource.ToString().Length < 1)
                {
                    FClone1.FTSource = new FS.HISFC.Models.Fee.Inpatient.FTSource("04");				//比例调整标志 4 
                }
                else
                {
                    string extFlag2 = FClone1.FTSource.ToString().Substring(0, 1) + "4";
                    FClone1.FTSource = new FS.HISFC.Models.Fee.Inpatient.FTSource(extFlag2);
                }

                //保存原来的处方号和流水号
                string OldRecipe = FClone1.RecipeNO;
                int OldSequence = FClone1.SequenceNO;
                FClone1.IsNeedUpdateNoBackQty = true;//需要更新可退数量

                if (this.isChangeDate)
                {
                    //FClone1.FeeInfo.User03 = "NOCHANGEDATE";//不更改收费日期
                    FClone1.FT.User03 = "NOCHANGEDATE";//不更改收费日期
                }

                if (this.ProcessFeeInPatient.QuitItem(this.Pinfo, FClone1) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.ProcessFeeInPatient.Err);
                    return;
                }
                //插入一条正记录
                //赋值
                string NoteNo = "";
                if (ItemList.Item.ItemType == EnumItemType.Drug)
                {
                    NoteNo = this.FeeInPatient.GetDrugRecipeNO();
                }
                else
                {
                    NoteNo = this.FeeInPatient.GetUndrugRecipeNO();
                }
                this.ItemList.RecipeNO = NoteNo;
                this.ItemList.SequenceNO = 1;
                this.ItemList.FT.OwnCost = decimal.Parse(this.txtOwnCost.Text);
                this.ItemList.FT.PayCost = decimal.Parse(this.txtPayCost.Text);
                this.ItemList.FT.PubCost = decimal.Parse(this.txtPubCost.Text);
                this.ItemList.FT.RebateCost = decimal.Parse(this.txtRebate.Text);
                this.ItemList.FT.OwnCost = this.ItemList.FT.TotCost - this.ItemList.FT.PubCost - this.ItemList.FT.PayCost;
                this.ItemList.FT.OwnCost = this.ItemList.FT.OwnCost - this.ItemList.FT.RebateCost;
                if (!this.isChangeDate)
                {
                    ItemList.FeeOper.OperTime = this.FeeInPatient.GetDateTimeFromSysDateTime();
                }
                this.ItemList.User01 = "1";//用作判断标记在FeeManager中不重新调用计算比例函数
                //if (ItemList.ExtFlag2.Length < 1)
                //{
                //    this.ItemList.ExtFlag2 = "04";		//比例调整标志 4 
                //}
                //else
                //{
                //    ItemList.ExtFlag2 = ItemList.ExtFlag2.Substring(0, 1) + "4";
                //}
                if (ItemList.FTSource.ToString().Length < 1)
                {
                    ItemList.FTSource = new FS.HISFC.Models.Fee.Inpatient.FTSource("04");				//比例调整标志 4 
                }
                else
                {
                    string extFlag2 = ItemList.FTSource.ToString().Substring(0, 1) + "4";
                    ItemList.FTSource = new FS.HISFC.Models.Fee.Inpatient.FTSource(extFlag2);
                }

                if (this.ItemList.FT.TotCost != this.ItemList.FT.OwnCost + this.ItemList.FT.PayCost + this.ItemList.FT.PubCost + this.ItemList.FT.RebateCost)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("费用比例修改有误,总金额不等于分项之和");
                    return;
                }
                //Add By Maokb --当前操作员06-03-14
                this.ItemList.FeeOper.ID = this.FeeInPatient.Operator.ID;
                //插入调用组合业务收取
                if (this.ProcessFeeInPatient.FeeItem(this.Pinfo, this.ItemList) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.FeeInPatient.Err);
                    return;
                }
              
                //更新费用急诊审核标志
                if (this.ItemList.Item.ItemType == EnumItemType.Drug)
                {
                    FS.HISFC.BizLogic.Pharmacy.Item itemDrug = new FS.HISFC.BizLogic.Pharmacy.Item();
                    itemDrug.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                      //更新肿瘤审药/高检费
                if (this.checkBox1.Checked)
                {
                    if (-1 == this.FeeInPatient.UpdatTPflag( this.ItemList.RecipeNO, this.ItemList.SequenceNO, "4", 1))
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新肿瘤审药/高检费！" + FeeInPatient.Err);
                        return;
                    }
                }
                    if (itemDrug.UpdateApplyOutRecipe(OldRecipe, OldSequence, this.ItemList.RecipeNO,
                        this.ItemList.SequenceNO) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新出库流水号失败！" + itemDrug.Err);
                        return;
                    }

                    if (this.FeeInPatient.UpdateEmergencyForDrug(this.ItemList.RecipeNO, this.ItemList.SequenceNO, true) < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新药品急诊标记" + this.FeeInPatient.Err);
                        return;
                    }
                    //更新医嘱执行档
                    if (ordMgr.UpdateExecRecipeNo(ItemList.Order.ID, true, ItemList.RecipeNO) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新医嘱执行档出错" + ordMgr.Err);
                        return;
                    }
                }
                else
                {
                    //更新肿瘤审药/高检费
                    if (this.checkBox1.Checked)
                    {
                        if (-1 == this.FeeInPatient.UpdatTPflag(this.ItemList.RecipeNO, this.ItemList.SequenceNO, "4", 0))
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新肿瘤审药/高检费！" + FeeInPatient.Err);
                            return;
                        }
                    }
                    if (this.FeeInPatient.UpdateEmergencyForUndrug(this.ItemList.RecipeNO, this.ItemList.SequenceNO, true) < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新非药品急诊标记" + this.FeeInPatient.Err);
                        return;
                    }
                    //更新医嘱执行档
                    if (ordMgr.UpdateExecRecipeNo(ItemList.Order.ID, false, ItemList.RecipeNO) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新医嘱执行档出错" + ordMgr.Err);
                        return;
                    }
                }


                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("修改成功!", "提示");
                this.isConfirm = true;
                this.Close();
            }
        }

        private void txtSelfRate_Leave(object sender, EventArgs e)
        {
            try
            {

                if (FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtSelfRate.Text), 2) > 1 || FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtSelfRate.Text), 2) < 0)
                {
                    MessageBox.Show("自费费用比例数值应在0-1之间");
                    this.txtSelfRate.Text = (FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtOwnCost.Text), 2) / FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtTot.Text), 2)).ToString();
                    //					this.txtSelfRate.SelectAll();
                    return;
                }
                else
                {
                    //按自费比例重新计算own费用和pub费用
                    if (decimal.Parse(this.txtSelfRate.Text) != 0)
                    {
                        this.txtPayCost.Text = "0.00";
                        this.txtPayRate.Text = "0.00";
                        this.txtOwnCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(decimal.Parse(this.txtTot.Text) * decimal.Parse(this.txtSelfRate.Text), 2);
                        this.txtPubCost.Text = (decimal.Parse(this.txtTot.Text) - decimal.Parse(this.txtOwnCost.Text)).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtOwnCost_Leave(object sender, EventArgs e)
        {
            if (decimal.Parse(this.txtOwnCost.Text) > decimal.Parse(this.txtTot.Text))
            {
                MessageBox.Show("自费金额维护错误,不应大于费用总额");
                this.txtOwnCost.Text = (decimal.Parse(this.txtTot.Text) - decimal.Parse(this.txtPubCost.Text) - decimal.Parse(this.txtPayCost.Text)).ToString();
                return;
            }
            else
            {
                //按照自费金额重新计算own费用和pub费用
                if (decimal.Parse(this.txtOwnCost.Text) != 0)
                {
                    decimal rate = 0m;
                    rate = (FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtOwnCost.Text), 2) / FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtTot.Text), 2));
                    this.txtSelfRate.Text = FS.FrameWork.Public.String.FormatNumber(rate, 2).ToString();
                    this.txtPubCost.Text = (decimal.Parse(this.txtTot.Text) - decimal.Parse(this.txtOwnCost.Text)).ToString();
                    this.txtPayCost.Text = "0.00";
                    this.txtPayRate.Text = "0.00";
                }
                if (decimal.Parse(this.txtOwnCost.Text) == 0)
                {
                    this.txtPayCost.Text = "0.00";
                    this.txtPayRate.Text = "0.00";
                    this.txtSelfRate.Text = "0.00";
                    this.txtPubCost.Text = this.txtTot.Text;
                }

            }
        }

        private void txtPayRate_Leave(object sender, EventArgs e)
        {
            try
            {

                if (FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtPayRate.Text), 2) > 1 || FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtPayRate.Text), 2) < 0)
                {
                    MessageBox.Show("自付费用比例数值应在0-1之间");
                    this.txtPayRate.Text = (FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtPayCost.Text), 2) / FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtTot.Text), 2)).ToString();
                    return;
                }
                else
                {
                    //按自付比例重新计算pay费用和pub费用
                    if (decimal.Parse(this.txtPayRate.Text) != 0)
                    {
                        //this.txtOwnCost.Text="0.00";
                        //this.txtSelfRate.Text="0.00";
                        this.txtPayCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString((decimal.Parse(this.txtTot.Text) - FS.FrameWork.Function.NConvert.ToDecimal(this.txtOwnCost.Text)) * decimal.Parse(this.txtPayRate.Text), 2);
                        this.txtPubCost.Text = (decimal.Parse(this.txtTot.Text) - decimal.Parse(this.txtPayCost.Text) - FS.FrameWork.Function.NConvert.ToDecimal(this.txtOwnCost.Text)).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void txtPayCost_Leave(object sender, EventArgs e)
        {
            if (decimal.Parse(this.txtPayCost.Text) > decimal.Parse(this.txtTot.Text))
            {
                MessageBox.Show("自付金额维护错误,不应大于费用总额");
                this.txtPayCost.Text = (decimal.Parse(this.txtTot.Text) - decimal.Parse(this.txtPubCost.Text) - decimal.Parse(this.txtOwnCost.Text)).ToString();
                return;
            }
            if (FS.FrameWork.Function.NConvert.ToDecimal(this.txtPayCost.Text) >
                FS.FrameWork.Function.NConvert.ToDecimal(this.txtTot.Text) - FS.FrameWork.Function.NConvert.ToDecimal(this.txtOwnCost.Text))
            {
                MessageBox.Show("自付金额维护错误,不应大于记帐金额");
                this.txtPayCost.Text = (decimal.Parse(this.txtTot.Text) - decimal.Parse(this.txtPubCost.Text) - decimal.Parse(this.txtOwnCost.Text)).ToString();
                return;
            }
            //按照自付金额重新计算pay费用和pub费用
            if (decimal.Parse(this.txtPayCost.Text) != 0)
            {
                decimal rate = 0m;
                this.txtPubCost.Text = (FS.FrameWork.Function.NConvert.ToDecimal(this.txtTot.Text) - FS.FrameWork.Function.NConvert.ToDecimal(this.txtOwnCost.Text) - FS.FrameWork.Function.NConvert.ToDecimal(this.txtPayCost.Text)).ToString();
                rate = FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtPayCost.Text), 2) / (FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtPayCost.Text), 2) + FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtPubCost.Text), 2));
                this.txtPayRate.Text = FS.FrameWork.Public.String.FormatNumber(rate, 2).ToString();
                this.txtPubCost.Text = (decimal.Parse(this.txtTot.Text) - decimal.Parse(this.txtPayCost.Text) - FS.FrameWork.Function.NConvert.ToDecimal(this.txtOwnCost.Text)).ToString();
                //this.txtOwnCost.Text="0.00";
                this.txtSelfRate.Text = FS.FrameWork.Public.String.FormatNumber(FS.FrameWork.Function.NConvert.ToDecimal(this.txtOwnCost.Text) /
                    FS.FrameWork.Function.NConvert.ToDecimal(this.txtTot.Text), 2).ToString();
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.ItemList.Item.ItemType == EnumItemType.Drug)
            {
                if (-1 == this.FeeInPatient.UpdatTPflag(this.ItemList.RecipeNO, this.ItemList.SequenceNO, "", 1))
                {
                    MessageBox.Show("更新特批标志出错！");
                    return;
                }
               
            }
            else 
            {
                if (-1 == this.FeeInPatient.UpdatTPflag(this.ItemList.RecipeNO, this.ItemList.SequenceNO, "", 0))
                {
                    MessageBox.Show("更新特批标志出错！");
                    return;
                }
             
                
            }
            MessageBox.Show("取消特批标志成功！");
            decimal totCost = 0, ownCost = 0, payCost = 0, pubCost = 0;
            if (ItemList.Item.DefPrice == 0)
            {
                totCost = ItemList.FT.TotCost;
                ownCost = ItemList.FT.OwnCost;
                payCost = ItemList.FT.PayCost;
                pubCost = ItemList.FT.PubCost;
            }
            else
            {
                if (ItemList.Item.PackQty == 0)
                {
                    ItemList.Item.PackQty = 1;
                }
                totCost = FS.FrameWork.Public.String.FormatNumber(ItemList.Item.DefPrice * ItemList.Item.Qty / ItemList.Item.PackQty, 2);
                payCost = ItemList.FT.PayCost;
                pubCost = ItemList.FT.PubCost;
                ownCost = totCost - payCost - pubCost;
            }
            if (totCost - ownCost == 0)
            {
                this.txtPayRate.Text ="0.00";
                this.txtPayCost.Text = "0.00";
                this.txtPubCost.Text = "0.00";
                this.txtOwnCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(ownCost, 2);
                this.txtSelfRate.Text = "1.00";
            }
            this.lblMemo.Text = "";
            this.button1.Enabled = true;
            this.button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.ItemList.Item.ItemType == EnumItemType.Drug)
            {
                if (-1 == this.FeeInPatient.UpdatTPflag(this.ItemList.RecipeNO, this.ItemList.SequenceNO, "4", 1))
                {
                    MessageBox.Show("更新特批标志出错！");
                    return;
                }
               
            }
            else
            {
                if (-1 == this.FeeInPatient.UpdatTPflag(this.ItemList.RecipeNO, this.ItemList.SequenceNO, "4", 0))
                {
                    MessageBox.Show("更新特批标志出错！");
                    return;
                }
               

            }
            MessageBox.Show("确认特批成功！");
            this.lblMemo.Text = "备注：该项目已经改为特批项";

            decimal totCost = 0, ownCost = 0, payCost = 0, pubCost = 0;
            if (ItemList.Item.DefPrice == 0)
            {
                totCost = ItemList.FT.TotCost;
                ownCost = ItemList.FT.OwnCost;
                payCost = ItemList.FT.PayCost;
                pubCost = ItemList.FT.PubCost;
            }
            else
            {
                if (ItemList.Item.PackQty == 0)
                {
                    ItemList.Item.PackQty = 1;
                }
                totCost = FS.FrameWork.Public.String.FormatNumber(ItemList.Item.DefPrice * ItemList.Item.Qty / ItemList.Item.PackQty, 2);
                payCost = ItemList.FT.PayCost;
                pubCost = ItemList.FT.PubCost;
                ownCost = totCost - payCost - pubCost;
            }

            if (totCost -ownCost == 0)
            {
                this.txtPayRate.Text = this.p.Pact.Rate.PayRate.ToString();
                this.txtPayCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(ownCost * this.p.Pact.Rate.PayRate, 2);
                this.txtPubCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(ownCost - ownCost * this.p.Pact.Rate.PayRate, 2);
                this.txtOwnCost.Text = "0.00";
                this.txtSelfRate.Text = "0.00";
            }

            this.button2.Enabled = true;
            this.button1.Enabled = false;
        }

        private void validatedTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
