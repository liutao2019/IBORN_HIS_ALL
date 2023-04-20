using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Base;
using FS.FrameWork.WinForms.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.Local.InpatientFee.GuangZhou
{

    public enum FeeColumn
    {
        //Checked,
        UserCode,
        ItemName,
        ItemSpces,
        MinFee,
        Price,
        Qty,
        Unit,
        TotCost,
        OwnCost,
        OwnRate,
        PayCost,
        PayRate,
        PubCost,
        ChargeDate,
        FeeDate,
        ExecDept,
        AppOper,
        AppItem,
        Grade,
        RecipeNO,
        SequenceNO,
        SpellCode,
        WBCode
    }

    public partial class ucAlterFeeRate : FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucAlterFeeRate()
        {
            InitializeComponent();
        }

        #region "变量"
        /// <summary>
        /// 自费患者是否能修改费用比例
        /// </summary>
        bool isCanModifyRateForOwn = false;


        FS.HISFC.Models.RADT.PatientInfo PatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
        FS.HISFC.BizLogic.RADT.InPatient RadtInpatient = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.BizLogic.Fee.InPatient FeeInpatient = new FS.HISFC.BizLogic.Fee.InPatient();
        FS.HISFC.BizLogic.Fee.Item myUndrugItem = new FS.HISFC.BizLogic.Fee.Item();
        FS.HISFC.BizLogic.Pharmacy.Item myDrugItem = new FS.HISFC.BizLogic.Pharmacy.Item();
        FS.FrameWork.Public.ObjectHelper Minfee = new FS.FrameWork.Public.ObjectHelper();
        FS.HISFC.BizLogic.Fee.PactUnitInfo PactInfo = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
        FS.HISFC.Models.Base.PactInfo pactInfo = new PactInfo();

        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        #endregion

        public void ucAlterFeeRate_Load(object sender, System.EventArgs e)
        {
            this.txtInpatientNo.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(txtInpatientNo_myEvent);
            this.fpFeeDetail.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpFeeDetail_CellDoubleClick);
            this.fpFeeDetail.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(fpFeeDetail_CellClick);
            this.btnExit.Click += new EventHandler(btnExit_Click);
            this.txtFilter.TextChanged += new EventHandler(txtFilter_TextChanged);
            this.chkChangedate.Checked = false;
            this.cmbMinFee.SelectedIndexChanged += new EventHandler(cmbMinFee_SelectedIndexChanged);

            this.txtInpatientNo.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtInpatientNo.TextBox.Size = new System.Drawing.Size(116, 21);
            this.txtInpatientNo.TextBox.Location = new System.Drawing.Point(52, 5);
            this.txtInpatientNo.TextBox.BringToFront();

            this.Minfee.ArrayObject = new FS.HISFC.BizLogic.Manager.Constant().GetAllList("MINFEE");
            ArrayList alTemp = new FS.HISFC.BizLogic.Manager.Constant().GetAllList("MINFEE");
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "ALL";
            obj.Name = "全部";
            alTemp.Insert(0, obj);
            this.cmbMinFee.AddItems(alTemp);
            this.InitDataSet();



            //是否允许自费费用修改费用比例
            this.isCanModifyRateForOwn = this.controlParamIntegrate.GetControlParam<bool>("ZY0005", true, false);


            this.txtInpatientNo.Focus();
            this.txtInpatientNo.Select();
        }

        void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (this.fpFeeDetail.ActiveSheet == this.fpFeeDetail_Sheet2)
            {
                if (this.cmbMinFee.Text == "全部")
                {
                    this.dvUndrug.RowFilter = "分类 like '%%'";
                }
                else
                {
                    this.dvUndrug.RowFilter = "分类 like '%" + this.cmbMinFee.Text + "%'";
                }

                this.dvUndrug.RowFilter += " and ( 项目名称 like '%" + this.txtFilter.Text + "%'" +
                                                               " or  拼音码 like '%" + this.txtFilter.Text + "%'" +
                                                               " or  五笔码 like '%" + this.txtFilter.Text + "%')";
            }
            else
            {
                if (this.cmbMinFee.Text == "全部")
                {
                    this.dvDrug.RowFilter = "分类 like '%%'";
                }
                else
                {
                    this.dvDrug.RowFilter = "分类 like '%" + this.cmbMinFee.Text + "%'";
                }

                this.dvDrug.RowFilter += " and (  药品名称 like '%" + this.txtFilter.Text + "%'" +
                                                               " or  拼音码 like '%" + this.txtFilter.Text + "%'" +
                                                               " or  五笔码 like '%" + this.txtFilter.Text + "%')";
            }


            this.SetFormat();
        }

        #region "属性"
        private FS.FrameWork.WinForms.Forms.BaseForm FormParent
        {
            get
            {
                return this.FindForm() as FS.FrameWork.WinForms.Forms.BaseForm;
            }
        }

        /// <summary>
        /// 是否显示时间选择框
        /// </summary>
        public bool IsShowDate
        {
            set
            {
                this.dtBegin.Visible = value;
                this.dtEnd.Visible = value;
                this.lbDate.Visible = value;
            }
        }
        #endregion
        #region "函数"

        private void Clear()
        {
            this.PatientInfo = null;
            this.txtName.Text = "";
            this.txtDeptName.Text = "";
            this.txtBalanceType.Text = "";
            this.txtBedNo.Text = "";
            txtBirthday.Text = "";
            txtNurseStation.Text = "";
            txtDateIn.Text = "";
            txtDoctor.Text = "";
            txtBalanceType.Text = "";
            txtPayRate.Text = "";

        }

        private int GetColumnIndexFromNameForfpUndrug(string Name)
        {
            for (int i = 0; i < this.fpFeeDetail_Sheet2.Columns.Count; i++)
            {
                if (this.fpFeeDetail_Sheet2.Columns[i].Label == Name) return i;
            }
            MessageBox.Show("缺少列" + Name);
            this.FormParent.Close();
            return -1;
        }
        private int GetColumnIndexFromNameForfpDrug(string Name)
        {
            for (int i = 0; i < this.fpFeeDetail_Sheet1.Columns.Count; i++)
            {
                if (this.fpFeeDetail_Sheet1.Columns[i].Label == Name) return i;
            }
            MessageBox.Show("缺少列" + Name);
            this.FormParent.Close();
            return -1;
        }

        private DataSet dsDrug = new DataSet();
        private DataSet dsUndrug = new DataSet();
        private DataView dvDrug = new DataView();
        private DataView dvUndrug = new DataView();
        private void InitDataSet()
        {
            System.Type stStr = System.Type.GetType("System.String");
            System.Type stInt = System.Type.GetType("System.Int16");
            System.Type stDec = System.Type.GetType("System.Single");
            System.Type stDate = System.Type.GetType("System.DateTime");
            System.Type stBoolean = System.Type.GetType("System.Boolean");
            
            dsUndrug.Tables.Add();
            dsUndrug.Tables[0].Columns.AddRange(new DataColumn[]{   
                                                                    //new DataColumn("多选", typeof(System.Boolean)),
																	new DataColumn("自定义码",stStr),
																	new DataColumn("项目名称",stStr),
																	new DataColumn("套餐",stStr),
																	new DataColumn("分类",stStr),
																	new DataColumn("价格",stDec),
																	new DataColumn("数量",stDec),
																	new DataColumn("单位",stStr),															  
																	new DataColumn("总金额",stDec),
																	new DataColumn("自费金额",stDec),
																	new DataColumn("自费比例",stDec),
																	new DataColumn("自付金额",stDec),
																	new DataColumn("自付比例",stDec),
																	new DataColumn("记帐金额",stDec),
	                                                                new DataColumn("划价日期",stDate),
																	new DataColumn("记帐日期",stDate),
																	new DataColumn("执行科室",stStr),
																	new DataColumn("审批操作人",stStr),
																	new DataColumn("审批项目",stStr),
																	new DataColumn("医保等级",stStr),
																	new DataColumn("处方号",stStr),
																	new DataColumn("处方序列",stInt),
																	new DataColumn("拼音码",stStr),
																	new DataColumn("五笔码",stStr)
			});
            this.dsUndrug.Tables[0].PrimaryKey = new DataColumn[] { dsUndrug.Tables[0].Columns["处方号"], dsUndrug.Tables[0].Columns["处方序列"] };
            this.fpFeeDetail_Sheet2.DataAutoSizeColumns = false;
            this.dvUndrug = new DataView(dsUndrug.Tables[0]);
            this.fpFeeDetail_Sheet2.DataSource = dvUndrug;
            this.fpFeeDetail_Sheet2.DataAutoSizeColumns = false;

            dsDrug.Tables.Add();
            dsDrug.Tables[0].Columns.AddRange(new DataColumn[]{   
                                                                    //new DataColumn("多选",typeof(System.Boolean)),
																	new DataColumn("自定义码",stStr),
																	new DataColumn("药品名称",stStr),
																    new DataColumn("规格",stStr),
 																	new DataColumn("分类",stStr),
																	new DataColumn("价格",stDec),
																	new DataColumn("数量",stDec),
																	new DataColumn("单位",stStr),															  
																	new DataColumn("总金额",stDec),
																	new DataColumn("自费金额",stDec),
																	new DataColumn("自费比例",stDec),
																	new DataColumn("自付金额",stDec),
																	new DataColumn("自付比例",stDec),
																	new DataColumn("记帐金额",stDec),	
                                                                    new DataColumn("划价日期",stDate),
																	new DataColumn("记帐日期",stDate),
																	new DataColumn("执行科室",stStr),
																	new DataColumn("审批操作人",stStr),
																	new DataColumn("审批项目",stStr),
																	new DataColumn("医保等级",stStr),
																	new DataColumn("处方号",stStr),
																	new DataColumn("处方序列",stInt),
																	new DataColumn("拼音码",stStr),
																	new DataColumn("五笔码",stStr)
			});
            this.dsDrug.Tables[0].PrimaryKey = new DataColumn[] { dsDrug.Tables[0].Columns["处方号"], dsDrug.Tables[0].Columns["处方序列"] };
            this.fpFeeDetail_Sheet1.DataAutoSizeColumns = false;
            this.dvDrug = new DataView(this.dsDrug.Tables[0]);
            this.fpFeeDetail_Sheet1.DataSource = dvDrug;
            this.fpFeeDetail_Sheet1.DataAutoSizeColumns = false;
        }

        /// <summary>
        /// 查询
        /// </summary>
        public void Query()
        {
            if (this.PatientInfo == null)
            {
                MessageBox.Show("请输入住院号后回车选择患者");
                return;
            }

            if (this.QueryFeeList(this.PatientInfo) == -1)
            {
                this.Clear();
                this.fpFeeDetail_Sheet2.Rows.Count = 0;
                this.fpFeeDetail_Sheet1.Rows.Count = 0;
                this.txtInpatientNo.Focus();
                return;
            }
        }

        /// <summary>
        /// 检索费用明细
        /// </summary>
        /// <param name="Pinfo"></param>
        /// <returns></returns>
        private int QueryFeeList(FS.HISFC.Models.RADT.PatientInfo Pinfo)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询，请稍后...");
            Application.DoEvents();

            ArrayList alDrug = FeeInpatient.QueryMedItemListsCanQuit(Pinfo.ID, this.dtBegin.Value, this.dtEnd.Value, "1,2");
            if (alDrug == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("检索药品明细出错!" + this.FeeInpatient.Err);
                return -1;
            }
            this.fpFeeDetail_Sheet1.Rows.Count = 0;
            FS.HISFC.BizLogic.Manager.Spell spellMgr = new FS.HISFC.BizLogic.Manager.Spell();
            ArrayList alUndrug = this.FeeInpatient.QueryFeeItemListsCanQuit(Pinfo.ID, this.dtBegin.Value, this.dtEnd.Value);
            if (alUndrug == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("检索非药品明细出错!" + this.FeeInpatient.Err);
                return -1;
            }
            this.fpFeeDetail_Sheet2.Rows.Count = 0;

            string tempString = "";
            this.dsUndrug.Clear();
            this.dsDrug.Clear();
            decimal tot_cost = 0, price = 0, own_cost = 0;
            //非药品窗口赋值
            for (int i = 0; i < alUndrug.Count; i++)
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList UndrugItem = alUndrug[i] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                if (UndrugItem.SplitFeeFlag)
                {
                    continue;
                }

                if (UndrugItem.Item.DefPrice == 0 || UndrugItem.Item.DefPrice > UndrugItem.Item.Price)
                {
                    tot_cost = UndrugItem.FT.TotCost;
                    own_cost = UndrugItem.FT.OwnCost;
                    price = UndrugItem.Item.Price;
                }
                else
                {
                    tot_cost = FS.FrameWork.Public.String.FormatNumber(UndrugItem.Item.DefPrice * UndrugItem.Item.Qty, 2);
                    own_cost = (tot_cost - UndrugItem.FT.PayCost - UndrugItem.FT.PubCost);
                    price = UndrugItem.Item.DefPrice;
                }

                if (tot_cost == 0) continue;

                UndrugItem.Item.SpellCode = SOC.HISFC.BizProcess.CommonInterface.CommonController.Instance.GetItem(UndrugItem.Item.ID).SpellCode;
                UndrugItem.Item.WBCode = SOC.HISFC.BizProcess.CommonInterface.CommonController.Instance.GetItem(UndrugItem.Item.ID).WBCode;
                UndrugItem.Item.UserCode = SOC.HISFC.BizProcess.CommonInterface.CommonController.Instance.GetItem(UndrugItem.Item.ID).UserCode;
                UndrugItem.Item.Grade = this.GetItemGrade(UndrugItem.Item.ID);

                decimal strPayRatio = UndrugItem.FT.PayCost / ((tot_cost - own_cost) == 0 ? 1 : (tot_cost - own_cost));
                this.dsUndrug.Tables[0].Rows.Add(new object[]{  
                     UndrugItem.Item.UserCode,
                     UndrugItem.Item.Name,
                     UndrugItem.UndrugComb.Name,
                     CommonController.Instance.GetConstantName( EnumConstant.MINFEE,UndrugItem.Item.MinFee.ID),
                     price,
                     UndrugItem.Item.Qty,
                     UndrugItem.Item.PriceUnit,
                     tot_cost,
                     own_cost,
                     FS.FrameWork.Public.String.FormatNumberReturnString(own_cost/tot_cost,2),
                     UndrugItem.FT.PayCost,
                     FS.FrameWork.Public.String.FormatNumber(strPayRatio,2),
                     UndrugItem.FT.PubCost,
                     UndrugItem.ChargeOper.OperTime,//add by zhy
                     UndrugItem.FeeOper.OperTime,
                     CommonController.Instance.GetDepartmentName(UndrugItem.ExecOper.Dept.ID),
                     UndrugItem.Item.SpecialFlag4=="4"?CommonController.Instance.GetEmployeeName(UndrugItem.FeeOper.ID):string.Empty,
                     (UndrugItem.Item.SpecialFlag4=="4"?"已审批":string.Empty),
                     UndrugItem.Item.Grade,
                     UndrugItem.RecipeNO,
                     UndrugItem.SequenceNO,
                     UndrugItem.Item.SpellCode,
                     UndrugItem.Item.WBCode
                });
            }
            //药品窗口赋值
            for (int i = 0; i < alDrug.Count; i++)
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList DrugItem = alDrug[i] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                if (DrugItem.SplitFeeFlag)
                {
                    continue;
                }
                if (DrugItem.FT.TotCost == 0)
                {
                    //公费超标调整记录
                    continue;
                }
                DrugItem.Item.SpellCode = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(DrugItem.Item.ID).SpellCode;
                DrugItem.Item.WBCode = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(DrugItem.Item.ID).WBCode;
                DrugItem.Item.UserCode = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(DrugItem.Item.ID).UserCode;
                DrugItem.Item.Grade = this.GetItemGrade(DrugItem.Item.ID);

                decimal strPayRatio = DrugItem.FT.PayCost / ((DrugItem.FT.TotCost - DrugItem.FT.OwnCost) == 0 ? 1 : (DrugItem.FT.TotCost - DrugItem.FT.OwnCost));
                //if (DrugItem.Item.SpecialFlag4 != "4" && strPayRatio != this.pactInfo.Rate.PayRate)
                //{
                //    strPayRatio = this.pactInfo.Rate.PayRate;
                //}
                this.dsDrug.Tables[0].Rows.Add(new object[]{ 
                     DrugItem.Item.UserCode,
                    tempString + DrugItem.Item.Name,																  
                    DrugItem.Item.Specs,
                    CommonController.Instance.GetConstantName( EnumConstant.MINFEE,DrugItem.Item.MinFee.ID),
                    FS.FrameWork.Public.String.FormatNumber(DrugItem.Item.Price/DrugItem.Item.PackQty,4),
                    DrugItem.Item.Qty.ToString(),
                    DrugItem.Item.PriceUnit,
                    DrugItem.FT.TotCost,
                    DrugItem.FT.OwnCost,
                    FS.FrameWork.Public.String.FormatNumberReturnString(DrugItem.FT.OwnCost/DrugItem.FT.TotCost,2),
                    DrugItem.FT.PayCost,
                    FS.FrameWork.Public.String.FormatNumberReturnString(strPayRatio,2),
                    DrugItem.FT.PubCost,
                    DrugItem.ChargeOper.OperTime,//add by zhy
                    DrugItem.FeeOper.OperTime,
                    CommonController.Instance.GetDepartmentName(DrugItem.ExecOper.Dept.ID),
                    DrugItem.Item.SpecialFlag4=="4"?CommonController.Instance.GetEmployeeName(DrugItem.FeeOper.ID):string.Empty,
                   (DrugItem.Item.SpecialFlag4=="4"?"已审批":string.Empty),
                   DrugItem.Item.Grade,
                    DrugItem.RecipeNO,
                    DrugItem.SequenceNO,
                     DrugItem.Item.SpellCode,
                     DrugItem.Item.WBCode
                });

            }
            this.SetFormat();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 0;
        }

        private void EvalutePatient(FS.HISFC.Models.RADT.PatientInfo PatientInfo)
        {
            this.txtName.Text = PatientInfo.Name;
            this.txtDeptName.Text = PatientInfo.PVisit.PatientLocation.Dept.Name;
            this.txtBalanceType.Text = PatientInfo.Pact.Name;
            this.txtBedNo.Text = PatientInfo.PVisit.PatientLocation.Bed.ID;

            txtBirthday.Text = PatientInfo.Birthday.ToString("yyyy-MM-dd");
            txtNurseStation.Text = PatientInfo.PVisit.PatientLocation.NurseCell.Name;
            txtDateIn.Text = PatientInfo.PVisit.InTime.ToString();
            txtDoctor.Text = PatientInfo.PVisit.AdmittingDoctor.Name;
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            pactInfo = this.PactInfo.GetPactUnitInfoByPactCode(PatientInfo.Pact.ID);
            PatientInfo.Pact = pactInfo;
            this.PatientInfo.Pact = pactInfo;
            txtPayRate.Text = this.PatientInfo.Pact.Rate.PayRate.ToString();
        }

        public int Modify()
        {
            if (this.fpFeeDetail.ActiveSheet.RowCount == 0) return -1;
            FS.HISFC.Models.Fee.Inpatient.FeeItemList item;
            int index = this.fpFeeDetail.ActiveSheet.ActiveRowIndex;
            string recipe = "";
            int seq = 0;
            DataRow row = null;
            if (this.fpFeeDetail.ActiveSheet == this.fpFeeDetail_Sheet2)
            {
                recipe = this.fpFeeDetail_Sheet2.Cells[index, (int)FeeColumn.RecipeNO].Text;
                seq = FS.FrameWork.Function.NConvert.ToInt32(this.fpFeeDetail_Sheet2.Cells[index, (int)FeeColumn.SequenceNO].Text);
                item = this.FeeInpatient.GetItemListByRecipeNO(recipe, seq, EnumItemType.UnDrug);

                row = this.dsUndrug.Tables[0].Rows.Find(new object[] { recipe, seq });

            }
            else
            {
                recipe = this.fpFeeDetail_Sheet1.Cells[index, (int)FeeColumn.RecipeNO].Text;
                seq = FS.FrameWork.Function.NConvert.ToInt32(this.fpFeeDetail_Sheet1.Cells[index, (int)FeeColumn.SequenceNO].Text);
                item = this.FeeInpatient.GetItemListByRecipeNO(recipe, seq, EnumItemType.Drug);

                row = this.dsDrug.Tables[0].Rows.Find(new object[] { recipe, seq });

            }

            if (this.PatientInfo.Pact.PayKind.ID == "03")
            {
                #region 公费修改比例
                frmPopAlterRate F = new frmPopAlterRate();
                F.ItemList = item;
                F.Pinfo = this.PatientInfo;
                if (this.chkChangedate.Checked)
                {
                    F.IsChangeDate = true;
                }
                else
                {
                    F.IsChangeDate = false;
                }
                F.ShowDialog();

                if (F.IsConfirm)
                {
                    if (this.fpFeeDetail.ActiveSheet == this.fpFeeDetail_Sheet2)
                    {
                        if (row != null)
                        {
                            this.dsUndrug.Tables[0].Rows.Remove(row);
                        }
                    }
                    else
                    {
                        if (row != null)
                        {
                            this.dsDrug.Tables[0].Rows.Remove(row);
                        }
                    }
                }

                #endregion
            }
            else
            {
                #region 市公医修改比例

                string errInfo = string.Empty;

                ucApproveItem ucApproveItem = new ucApproveItem();
                if (ucApproveItem.SetInfo(this.PatientInfo, item, ref errInfo) < 0)
                {
                    MessageBox.Show(this, errInfo, "提示");
                    return -1;
                }
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucApproveItem);
                //刷新界面
                if (this.fpFeeDetail.ActiveSheet == this.fpFeeDetail_Sheet2)
                {
                    if (ucApproveItem.FeeItemList != null)
                    {
                        //查找费用信息
                        FS.HISFC.Models.Fee.Inpatient.FeeItemList itemListTemp = this.FeeInpatient.GetItemListByRecipeNO(ucApproveItem.FeeItemList.RecipeNO, ucApproveItem.FeeItemList.SequenceNO, ucApproveItem.FeeItemList.TransType, ucApproveItem.FeeItemList.Item.ItemType);
                        this.fpFeeDetail_Sheet2.Cells[index, (int)FeeColumn.RecipeNO].Text = itemListTemp.RecipeNO;
                        this.fpFeeDetail_Sheet2.Cells[index, (int)FeeColumn.SequenceNO].Value = itemListTemp.SequenceNO;
                        this.fpFeeDetail_Sheet2.Cells[index, (int)FeeColumn.OwnCost].Value = itemListTemp.FT.OwnCost;
                        this.fpFeeDetail_Sheet2.Cells[index, (int)FeeColumn.OwnRate].Value = itemListTemp.FT.OwnCost / itemListTemp.FT.TotCost;
                        this.fpFeeDetail_Sheet2.Cells[index, (int)FeeColumn.PayCost].Value = itemListTemp.FT.PayCost;
                        decimal strPayRatio = itemListTemp.FT.PayCost / ((itemListTemp.FT.TotCost - itemListTemp.FT.OwnCost) == 0 ? 1 : (itemListTemp.FT.TotCost - itemListTemp.FT.OwnCost));
                        this.fpFeeDetail_Sheet2.Cells[index, (int)FeeColumn.PayRate].Value = strPayRatio;
                        this.fpFeeDetail_Sheet2.Cells[index, (int)FeeColumn.PubCost].Value = itemListTemp.FT.PubCost;

                        if (itemListTemp.Item.SpecialFlag4 == "4")
                        {
                            this.fpFeeDetail_Sheet2.Cells[index, (int)FeeColumn.AppOper].Text = CommonController.Instance.GetEmployeeName(ucApproveItem.FeeItemList.FeeOper.ID);
                            this.fpFeeDetail_Sheet2.Cells[index, (int)FeeColumn.AppItem].Text = "已审批";
                        }
                        else
                        {
                            this.fpFeeDetail_Sheet2.Cells[index, (int)FeeColumn.AppOper].Text = "";
                            this.fpFeeDetail_Sheet2.Cells[index, (int)FeeColumn.AppItem].Text = "";
                        }
                    }
                }
                else
                {
                    if (ucApproveItem.FeeItemList != null)
                    {
                        //查找费用信息
                        FS.HISFC.Models.Fee.Inpatient.FeeItemList itemListTemp = this.FeeInpatient.GetItemListByRecipeNO(ucApproveItem.FeeItemList.RecipeNO, ucApproveItem.FeeItemList.SequenceNO, ucApproveItem.FeeItemList.TransType, ucApproveItem.FeeItemList.Item.ItemType);
                        this.fpFeeDetail_Sheet1.Cells[index, (int)FeeColumn.RecipeNO].Text = itemListTemp.RecipeNO;
                        this.fpFeeDetail_Sheet1.Cells[index, (int)FeeColumn.SequenceNO].Value = itemListTemp.SequenceNO;
                        this.fpFeeDetail_Sheet1.Cells[index, (int)FeeColumn.OwnCost].Value = itemListTemp.FT.OwnCost;
                        this.fpFeeDetail_Sheet1.Cells[index, (int)FeeColumn.OwnRate].Value = itemListTemp.FT.OwnCost / itemListTemp.FT.TotCost;
                        this.fpFeeDetail_Sheet1.Cells[index, (int)FeeColumn.PayCost].Value = itemListTemp.FT.PayCost;
                        decimal strPayRatio = itemListTemp.FT.PayCost / ((itemListTemp.FT.TotCost - itemListTemp.FT.OwnCost) == 0 ? 1 : (itemListTemp.FT.TotCost - itemListTemp.FT.OwnCost));
                        this.fpFeeDetail_Sheet1.Cells[index, (int)FeeColumn.PayRate].Value = strPayRatio;
                        this.fpFeeDetail_Sheet1.Cells[index, (int)FeeColumn.PubCost].Value = itemListTemp.FT.PubCost;

                        if (itemListTemp.Item.SpecialFlag4 == "4")
                        {
                            this.fpFeeDetail_Sheet1.Cells[index, (int)FeeColumn.AppOper].Text = CommonController.Instance.GetEmployeeName(ucApproveItem.FeeItemList.FeeOper.ID);
                            this.fpFeeDetail_Sheet1.Cells[index, (int)FeeColumn.AppItem].Text = "已审批";
                        }
                        else
                        {
                            this.fpFeeDetail_Sheet1.Cells[index, (int)FeeColumn.AppOper].Text = "";
                            this.fpFeeDetail_Sheet1.Cells[index, (int)FeeColumn.AppItem].Text = "";
                        }
                    }
                }
                #endregion
            }



            return 0;
        }

        public int ModifyFeeDate()
        {
            if (this.fpFeeDetail.ActiveSheet.RowCount == 0) return -1;
            FS.HISFC.Models.Fee.Inpatient.FeeItemList item;
            int index = this.fpFeeDetail.ActiveSheet.ActiveRowIndex;
            string recipe = "";
            int seq = 0;
            if (this.fpFeeDetail.ActiveSheet == this.fpFeeDetail_Sheet2)
            {
                recipe = this.fpFeeDetail_Sheet2.Cells[index, (int)FeeColumn.RecipeNO].Text;
                seq = FS.FrameWork.Function.NConvert.ToInt32(this.fpFeeDetail_Sheet2.Cells[index, (int)FeeColumn.SequenceNO].Text);
                item = this.FeeInpatient.GetItemListByRecipeNO(recipe, seq, EnumItemType.UnDrug);
                DataRow row = this.dsUndrug.Tables[0].NewRow();
                row = this.dsUndrug.Tables[0].Rows.Find(new object[] { recipe, seq });
                if (row != null)
                {
                    this.dsUndrug.Tables[0].Rows.Remove(row);
                }
            }
            else
            {
                recipe = this.fpFeeDetail_Sheet1.Cells[index, (int)FeeColumn.RecipeNO].Text;
                seq = FS.FrameWork.Function.NConvert.ToInt32(this.fpFeeDetail_Sheet1.Cells[index, (int)FeeColumn.SequenceNO].Text);
                item = this.FeeInpatient.GetItemListByRecipeNO(recipe, seq, EnumItemType.Drug);
                DataRow row = this.dsDrug.Tables[0].NewRow();
                row = this.dsDrug.Tables[0].Rows.Find(new object[] { recipe, seq });
                if (row != null)
                {
                    this.dsDrug.Tables[0].Rows.Remove(row);
                }
            }
            frmPopAlterRate F = new frmPopAlterRate();
            F.IsChangeFeeDate = true;
            F.ItemList = item;
            F.Pinfo = this.PatientInfo;
            if (this.chkChangedate.Checked)
            {
                F.IsChangeDate = true;
            }
            else
            {
                F.IsChangeDate = false;
            }
            F.ShowDialog();

            return 0;
        }

        public new void Focus()
        {
            this.txtInpatientNo.Focus();
        }

        public void SetFormat()
        {
            if (this.PatientInfo != null && this.PatientInfo.Pact.PayKind.ID == "03")
            {
                this.dvUndrug.Sort = "医保等级 desc";
                this.dvDrug.Sort = "医保等级 desc";
            }
            else
            {
                this.dvUndrug.Sort = "价格 desc";
                this.dvDrug.Sort = "价格 desc";
            }
            for (int i = 0; i < this.fpFeeDetail_Sheet2.RowCount; i++)
            {
                if (/*this.fpFeeDetail_Sheet2.Cells[i, (int)FeeColumn.AppItem] == "A已审批" &&*/
                    FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeDetail_Sheet2.Cells[i, (int)FeeColumn.PayRate].Value) != this.pactInfo.Rate.PayRate)
                {
                    this.fpFeeDetail_Sheet2.Rows[i].ForeColor = Color.Red;
                }
            }

            for (int i = 0; i < this.fpFeeDetail_Sheet1.RowCount; i++)
            {

                if (/*this.fpFeeDetail_Sheet1.Cells[i, (int)FeeColumn.AppItem] == "A已审批" &&*/
                    FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeDetail_Sheet1.Cells[i, (int)FeeColumn.PayRate].Value) != this.pactInfo.Rate.PayRate)
                {
                    this.fpFeeDetail_Sheet1.Rows[i].ForeColor = Color.Red;
                }
            }
        }

        private static DataSet dictionaryItemGrade = new DataSet();
        private string GetItemGrade(string itemCode)
        {
            string sql = string.Empty;

            DataRow[] drs = null;
            if (dictionaryItemGrade.Tables.Count > 0)
            {
                drs = dictionaryItemGrade.Tables[0].Select("HIS_CODE='" + itemCode + "'");
            }
            if (drs == null || drs.Length == 0)
            {
                sql = "select t.his_code,t.center_item_grade from fin_com_compare t where t.pact_code='2' ";

                FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
                dbMgr.ExecQuery(sql, ref dictionaryItemGrade);

                if (dictionaryItemGrade.Tables.Count > 0)
                {
                    drs = dictionaryItemGrade.Tables[0].Select("HIS_CODE='" + itemCode + "'");
                }
            }
            if (drs != null && drs.Length > 0)
            {
                switch (drs[0][1].ToString())
                {
                    case "1":
                        return "1甲类";
                    case "2":
                        return "2乙类";
                    case "3":
                        return "3丙类";
                }
            }
            return "4自费";
        }

        #endregion
        #region "事件"
        private void txtInpatientNo_myEvent()
        {
            this.fpFeeDetail_Sheet2.Rows.Count = 0;
            this.fpFeeDetail_Sheet1.Rows.Count = 0;
            //判断是否有该患者
            if (this.txtInpatientNo.InpatientNo == null || this.txtInpatientNo.InpatientNo.Trim() == "")
            {
                MessageBox.Show("不存在此住院号,或该患者不在院" + this.txtInpatientNo.Err);
                this.txtInpatientNo.Focus();
                return;
            }
            //获取住院号赋值给实体
            this.PatientInfo = RadtInpatient.QueryPatientInfoByInpatientNO(this.txtInpatientNo.InpatientNo);
            if (this.PatientInfo.PVisit.InState.ID.ToString() == FS.HISFC.Models.Base.EnumInState.N.ToString() ||
                this.PatientInfo.PVisit.InState.ID.ToString() == FS.HISFC.Models.Base.EnumInState.O.ToString())
            {
                MessageBox.Show("该患者已经出院!");
                this.PatientInfo.ID = null;
                this.txtInpatientNo.Focus();
                return;
            }
            //自费患者不允许调整费用
            if (this.PatientInfo.Pact.PayKind.ID == "01" && !isCanModifyRateForOwn)
            {
                MessageBox.Show("自费患者不允许调整费用比例!");
                this.PatientInfo.ID = null;
                this.txtInpatientNo.Focus();
                return;
            }
            if (this.PatientInfo.Pact.PayKind.ID == "02" && !isCanModifyRateForOwn)
            {
                MessageBox.Show("医保患者不允许调整费用比例!");
                this.PatientInfo.ID = null;
                this.txtInpatientNo.Focus();
                return;
            }

            //患者信息赋值
            this.EvalutePatient(this.PatientInfo);
            this.dtBegin.Value = this.PatientInfo.PVisit.InTime;
            this.dtEnd.Value = this.FeeInpatient.GetDateTimeFromSysDateTime();

            //检索患者费用明细信息
            if (this.QueryFeeList(this.PatientInfo) == -1)
            {
                this.Clear();
                this.fpFeeDetail_Sheet2.Rows.Count = 0;
                this.fpFeeDetail_Sheet1.Rows.Count = 0;
                this.txtInpatientNo.Focus();
                return;
            }
        }

        private void btnModify_Click(object sender, System.EventArgs e)
        {
            this.Modify();
        }

        private void fpFeeDetail_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column >= 0 && e.Row >= 0 && e.ColumnHeader == false)
            {
                this.Modify();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.FormParent.Close();
        }

        #endregion

        private void cmbMinFee_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.txtFilter_TextChanged(sender, e);

        }

        private void fpFeeDetail_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }

        /// <summary>
        /// toolBarService
        /// </summary>
        ToolBarService toolBarService = new ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("修改费用", "修改费用", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            toolBarService.AddToolButton("修改费用时间", "修改费用时间", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);

            return this.toolBarService;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return base.OnQuery(sender, neuObject);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "修改费用":
                    this.Modify();
                    break;
                case "修改费用时间":
                    this.ModifyFeeDate();

                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        public override int Export(object sender, object neuObject)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            //生成文件
            //查找当天的次数
            BizLogic.ApproveItemBizLogic approveMgr = new FS.SOC.Local.InpatientFee.GuangZhou.BizLogic.ApproveItemBizLogic();
            FS.HISFC.BizLogic.Fee.Interface interfaceMgr = new FS.HISFC.BizLogic.Fee.Interface();
            //查找所有未导出的项目
            List<BizLogic.ApproveItemModel> list = approveMgr.Query(this.PatientInfo.ID);
            if (list != null)
            {
                if (list.Count > 0)
                {
                    //生成文件名称
                    //获取患者的医保信息
                    FS.HISFC.Models.RADT.PatientInfo siPatientInfo = interfaceMgr.GetSIPersonInfo(this.PatientInfo.ID);
                    if (siPatientInfo == null || string.IsNullOrEmpty(siPatientInfo.ID))
                    {
                        MessageBox.Show(this, string.Format("未找到患者：{0}  的医保记录", this.PatientInfo.Name) + interfaceMgr.Err, "提示");
                        return -1;
                    }


                    DateTime dt = new DateTime();
                    if (this.PatientInfo.PVisit.OutTime < this.PatientInfo.PVisit.InTime)
                    {
                        dt = approveMgr.GetDateTimeFromSysDateTime();
                    }
                    else
                    {
                        dt = this.PatientInfo.PVisit.OutTime;
                    }
                    int count = approveMgr.GetMaxExportCount(this.PatientInfo.ID, dt);
                    count = count + 1;

                    dlg.FileName = this.PatientInfo.Name + "_" + siPatientInfo.SIMainInfo.RegNo.Substring(0, 6) + "_" + dt.ToString("yyyyMMdd") + "_" + count.ToString().PadLeft(3, '0');
                    dlg.Filter = "(*.txt)|*.txt";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        string txtInfo = string.Empty;
                        foreach (BizLogic.ApproveItemModel approveModel in list)
                        {
                            //就医登记号、医院编号、申请类别、医院项目编码、医院项目名称、中心项目编码、中心项目名称、申请数量、项目单价、用法、医院意见、申请理由、备注。
                            string importStr = "否";
                            string haveLocallyMaterailStr = "否";
                            string locallyPrice = string.Empty;
                            if (approveModel.ApplyType == "内置材料")
                            {
                                if (approveModel.ImportFlag)
                                {
                                    importStr = "是";
                                    if (approveModel.HaveLocallyMaterialFlag)
                                    {
                                        haveLocallyMaterailStr = "是";
                                        locallyPrice = approveModel.LocallyPrice.ToString("F2");
                                    }
                                }
                            }
                            decimal qty = approveModel.Qty;
                            decimal price = approveModel.Price;
                            if (FS.FrameWork.Public.String.IsPrecisionValid(qty, 10, 0)==false)
                            {
                                qty = 1;
                                price = approveModel.Price * approveModel.Qty;
                            }

                            txtInfo += approveModel.RegNO + "\t"
                                + approveModel.HosNO + "\t"
                                + approveModel.ApplyType + "\t"
                                + approveModel.Item.ID + "\t"
                                + approveModel.Item.Name + "\t"
                                + approveModel.Center.ID + "\t"
                                + approveModel.Center.Name + "\t"
                                + qty + "\t"
                                + price + "\t"
                                + approveModel.UseCode + "\t"
                                + importStr + "\t"
                                + haveLocallyMaterailStr + "\t"
                                + locallyPrice + "\t"
                                + approveModel.HosOpinion + "\t"
                                + approveModel.ApplyReason + "\t"
                                + approveModel.Memo + "\r\n";
                        }

                        //生成文件信息
                        System.IO.FileStream fileInfo = System.IO.File.Create(dlg.FileName);
                        Byte[] bts = System.Text.Encoding.Default.GetBytes(txtInfo);
                        fileInfo.Write(bts, 0, bts.Length);
                        fileInfo.Close();
                        //最后提示是否导出成功
                        if (MessageBox.Show(this, "请确认PJ3是否导入成功？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (approveMgr.UpdateExportFlag(this.PatientInfo.ID, dlg.FileName, dt) < 0)
                            {
                                MessageBox.Show(this, interfaceMgr.Err, "提示");
                                return -1;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show(this, string.Format("患者：{0}  没有需要导出的数据", this.PatientInfo.Name) + interfaceMgr.Err, "提示");
                    return -1;
                }
            }

            return base.Export(sender, neuObject);
        }

    }
}
