using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.InpatientFee.Balance
{
    public partial class ucChangePact : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 变量

        #region 业务层

        /// <summary>
        /// 入出转integrate层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 住院患者信息实体
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 更新后的实体
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo oENewPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 住院患者费用业务层
        /// </summary>
        FS.HISFC.BizLogic.Fee.InPatient InpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// 
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 合同单位
        /// </summary>
        FS.HISFC.BizLogic.Fee.PactUnitInfo myPactUnit = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        /// <summary>
        /// 费用中间层

        /// </summary>
        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        ///待遇接口类

        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        #endregion

        /// <summary>
        /// 欠费提示操作
        /// </summary>
        private FS.HISFC.Models.Base.MessType messType = FS.HISFC.Models.Base.MessType.N;

        /// <summary>
        /// 需要变更费用明细的合同单位
        /// </summary>
        private string needChangeFeeDetailPacts = "ALL";

        /// <summary>
        /// 保存重新获取的药品信息
        /// </summary>
        private Hashtable htPhaItem = null;
        
        /// <summary>
        /// 
        /// </summary>
        DataTable dtDrug = new DataTable();

        /// <summary>
        /// 
        /// </summary>
        DataTable dtUndrug = new DataTable();
       

        #endregion

        #region 属性

        /// <summary>
        /// 欠费提示操作
        /// </summary>
        [Category("控件设置"),Description("Y:提示欠费,不可以收费 M:提示欠费，还还可收费 N:不判断是否欠费")]
        public FS.HISFC.Models.Base.MessType MessageType
        {
            get
            {
                return messType;
            }
            set
            {
                messType = value;
            }
        }

        [Category("控件设置"), Description("需要变更费用明细的合同单位，以'|'分隔；ALL表示全部需要")]
        public string NeedChangeFeeDetailPacts
        {
            get
            {
                return this.needChangeFeeDetailPacts;
            }
            set
            {
                this.needChangeFeeDetailPacts = value;
            }
        }

        #endregion

        public ucChangePact()
        {
            InitializeComponent();
        }

        #region 方法

        /// <summary>
        /// 初始化


        /// </summary>
        protected virtual void Init()
        {
            //初始化合同单位


            this.InitPact();
            //
            this.initFPdrug();
            this.initFPUndrug();
 
        }

        /// <summary>
        /// 初始化药品


        /// </summary>
        protected virtual void initFPdrug()
        {
            this.dtDrug.Columns.AddRange(new DataColumn[]{
            new DataColumn("药品名称"),
            new DataColumn("规格"),
            new DataColumn("价格"),
            new DataColumn("单位"),
            new DataColumn("数量"),
            new DataColumn("金额"),
            new DataColumn("自费金额"),
            new DataColumn("自费比例"),
            new DataColumn("自付金额"),
            new DataColumn("自负比例"),
            new DataColumn("记帐金额"),
            new DataColumn("记帐日期")});
            this.fpDrug_Sheet1.DataSource = this.dtDrug;
            FarPoint.Win.Spread.CellType.NumberCellType numtype = new FarPoint.Win.Spread.CellType.NumberCellType();
            numtype.ButtonAlign = FarPoint.Win.ButtonAlign.Right;
          
            this.fpDrug_Sheet1.Columns[0].Width = 200;
            this.fpDrug_Sheet1.Columns[2].Width = 100;
            this.fpDrug_Sheet1.Columns[3].Width = 100;
            this.fpDrug_Sheet1.Columns[4].Width = 100;
            this.fpDrug_Sheet1.Columns[5].Width = 100;
            this.fpDrug_Sheet1.Columns[6].Width = 100;
            this.fpDrug_Sheet1.Columns[7].Width = 100;
            this.fpDrug_Sheet1.Columns[8].Width = 100;
            this.fpDrug_Sheet1.Columns[9].Width = 100;
            this.fpDrug_Sheet1.Columns[10].Width = 100;

             
            this.fpDrug_Sheet1.Columns[2].CellType = numtype;
            this.fpDrug_Sheet1.Columns[4].CellType = numtype;
            this.fpDrug_Sheet1.Columns[5].CellType = numtype;
            this.fpDrug_Sheet1.Columns[6].CellType = numtype;
            this.fpDrug_Sheet1.Columns[7].CellType = numtype;
            this.fpDrug_Sheet1.Columns[8].CellType = numtype;
            this.fpDrug_Sheet1.Columns[9].CellType = numtype;
            this.fpDrug_Sheet1.Columns[10].CellType = numtype;


        }

        /// <summary>
        /// 初始化非药品
        /// </summary>
        protected virtual void initFPUndrug()
        {
            this.dtUndrug .Columns.AddRange(new DataColumn[]{
            new DataColumn("项目名称"),
            new DataColumn ("价格"),
            new DataColumn("数量"),
            new DataColumn ("单位"),
            new DataColumn ("金额"),
            new DataColumn ("自费金额"),
            new DataColumn ("自费比例"),
            new DataColumn ("自付金额"),
            new DataColumn("自负比例"),
            new DataColumn("记帐金额"),
            new DataColumn("记帐日期")});

            this.fpUndrug_Sheet1.DataSource = this.dtUndrug;
            this.fpUndrug_Sheet1.Columns[0].Width = 200;
            this.fpUndrug_Sheet1.Columns[1].Width = 100;
            this.fpUndrug_Sheet1.Columns[2].Width = 100;
            this.fpUndrug_Sheet1.Columns[3].Width = 100;
            this.fpUndrug_Sheet1.Columns[4].Width = 100;
            this.fpUndrug_Sheet1.Columns[5].Width = 100;
            this.fpUndrug_Sheet1.Columns[6].Width = 100;
            this.fpUndrug_Sheet1.Columns[7].Width = 100;
            this.fpUndrug_Sheet1.Columns[8].Width = 100;
            this.fpUndrug_Sheet1.Columns[9].Width = 100; 
            FarPoint.Win.Spread.CellType.NumberCellType numtype = new FarPoint.Win.Spread.CellType.NumberCellType ();
            numtype.ButtonAlign = FarPoint.Win.ButtonAlign.Right;
            this.fpUndrug_Sheet1.Columns[1].CellType = numtype;
            this.fpUndrug_Sheet1.Columns[2].CellType = numtype;
            this.fpUndrug_Sheet1.Columns[4].CellType = numtype;
            this.fpUndrug_Sheet1.Columns[5].CellType = numtype;
            this.fpUndrug_Sheet1.Columns[6].CellType = numtype;
            this.fpUndrug_Sheet1.Columns[7].CellType = numtype;
            this.fpUndrug_Sheet1.Columns[8].CellType = numtype;
            this.fpUndrug_Sheet1.Columns[9].CellType = numtype;

  
        }

        /// <summary>
        /// 初始化合同单位


        /// </summary>
        /// <returns></returns>
        private void InitPact()
        {
            ArrayList al = new ArrayList();
            al = this.managerIntegrate.QueryPactUnitInPatient();
            if (al.Count > 0)
            {
                this.cmbNewPact.AddItems(al);
            } 
        }

        /// <summary>
        /// 界面显示基本信息
        /// </summary>
        /// <param name="patientInfo">患者信息实体</param>
        protected virtual void SetpatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.txtName.Text = patientInfo.Name;
            this.txtOldPact.Text = patientInfo.Pact.Name;
            this.txtOldPact.Tag = patientInfo.Pact.ID;
            this.txtDept.Text = patientInfo.PVisit.PatientLocation.Dept.Name;
            this.txtNurseStation.Text = patientInfo.PVisit.PatientLocation.NurseCell.Name;
            this.txtDoctor.Text = patientInfo.PVisit.AdmittingDoctor.Name;
            this.txtBirthday.Text = patientInfo.Birthday.ToShortDateString();
            this.txtBedNo.Text = patientInfo.PVisit.PatientLocation.Bed.ID;
            this.txtDateIn.Text = patientInfo.PVisit.InTime.ToShortDateString();
            this.txtPact.Text = patientInfo.Pact.Name;
        }

        /// <summary>
        /// 显示费用信息
        /// </summary>
        /// <returns></returns>
        protected virtual int DisplayDetail(string inpatientNO)
        {
            if (fpDrug_Sheet1.Rows.Count > 0)
            {
                this.fpDrug_Sheet1.RemoveRows(0, this.fpDrug_Sheet1.RowCount);
            }
            if (this.fpUndrug_Sheet1.Rows.Count > 0)
            {
                this.fpUndrug_Sheet1.RemoveRows(0, this.fpUndrug_Sheet1.RowCount);
            }
            //药品信息
            if (this.DisplayDrugDetail(inpatientNO) < 0)
            {
                return -1;
            }
            //非药品信息


            if (this.DisplayUndrugDetail(inpatientNO) < 0)
            {
                return -1;
            }

            
            return 1;
        }

        /// <summary>
        /// 显示费用品信息


        /// </summary>
        /// <returns></returns>
        protected virtual int DisplayDrugDetail(string inpatientNO)
        {
            DateTime fromDate = FS.FrameWork.Function.NConvert.ToDateTime("1900-01-01");
            DateTime endDate = FS.FrameWork.Function.NConvert.ToDateTime(this.InpatientManager.GetSysDateTime());
            ArrayList alDrung = new ArrayList();
            FS.HISFC.Models.Fee.MedItemList medicineList = new FS.HISFC.Models.Fee.MedItemList();
            //批费
            alDrung = this.InpatientManager.QueryMedItemListsCanQuit(inpatientNO, fromDate, endDate, "1,2", false);
            if (alDrung.Count > 0)
            {
                for (int i = 0; i < alDrung.Count; i++)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeItemList medcineList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                    medcineList = (FS.HISFC.Models.Fee.Inpatient.FeeItemList)alDrung[i];
                    this.fpDrug_Sheet1.Rows.Add(this.fpDrug_Sheet1.RowCount, 1);

                    this.fpDrug_Sheet1.Cells[i, 0].Value = medcineList.Item.Name;
                    this.fpDrug_Sheet1.Cells[i, 1].Value = medcineList.Item.Specs;
                    this.fpDrug_Sheet1.Cells[i, 2].Value = medcineList.Item.Price;
                    this.fpDrug_Sheet1.Cells[i, 3].Value = medcineList.Item.PriceUnit;
                    this.fpDrug_Sheet1.Cells[i, 4].Value = medcineList.Item.Qty;
                    this.fpDrug_Sheet1.Cells[i, 5].Value = medcineList.FT.TotCost;
                    this.fpDrug_Sheet1.Cells[i, 6].Value = medcineList.FT.OwnCost;
                    this.fpDrug_Sheet1.Cells[i, 7].Value = FS.FrameWork.Public.String.FormatNumberReturnString(medcineList.FT.OwnCost / medcineList.FT.TotCost, 2);
                    this.fpDrug_Sheet1.Cells[i, 8].Value = medcineList.FT.PayCost;
                    this.fpDrug_Sheet1.Cells[i, 9].Value = FS.FrameWork.Public.String.FormatNumberReturnString(medcineList.FT.PayCost / medcineList.FT.TotCost, 2);
                    this.fpDrug_Sheet1.Cells[i, 10].Value = medcineList.FT.PubCost;
                    this.fpDrug_Sheet1.Cells[i, 11].Value = medcineList.FeeOper.OperTime;
                    this.fpDrug_Sheet1.Rows[i].Tag = medcineList;

                }
            }
            
            return 1;
        }

        /// <summary>
        /// 非药品收费明细


        /// </summary>
        /// <returns></returns>
        protected virtual int DisplayUndrugDetail(string inpatientNO)
        {
            DateTime fromDate = FS.FrameWork.Function.NConvert.ToDateTime("1900-01-01");
            DateTime endDate = FS.FrameWork.Function.NConvert.ToDateTime(this.InpatientManager.GetSysDateTime());
            ArrayList alUnDrung = new ArrayList();
           
            alUnDrung = this.InpatientManager.QueryFeeItemListsCanQuit(inpatientNO, fromDate, endDate, false);
            if (alUnDrung.Count > 0)
            {
                for (int i = 0; i < alUnDrung.Count; i++)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeItemList undrugItem = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();

                    undrugItem = (FS.HISFC.Models.Fee.Inpatient.FeeItemList)alUnDrung[i];
                    this.fpUndrug_Sheet1.AddRows(this.fpUndrug_Sheet1.RowCount, 1);
                    this.fpUndrug_Sheet1.Cells[i, 0].Text = undrugItem.Item.Name;
                    this.fpUndrug_Sheet1.Cells[i, 1].Value = undrugItem.Item.Price;
                    this.fpUndrug_Sheet1.Cells[i, 3].Value = undrugItem.Item.PriceUnit;
                    this.fpUndrug_Sheet1.Cells[i, 2].Value = undrugItem.Item.Qty;
                    this.fpUndrug_Sheet1.Cells[i, 4].Value = undrugItem.FT.TotCost;
                    this.fpUndrug_Sheet1.Cells[i, 5].Value = undrugItem.FT.OwnCost;
                    this.fpUndrug_Sheet1.Cells[i, 6].Value = FS.FrameWork.Public.String.FormatNumberReturnString(undrugItem.FT.OwnCost / undrugItem.FT.TotCost, 2);
                    this.fpUndrug_Sheet1.Cells[i, 7].Value = undrugItem.FT.PayCost;
                    this.fpUndrug_Sheet1.Cells[i, 8].Value = FS.FrameWork.Public.String.FormatNumberReturnString(undrugItem.FT.PayCost / undrugItem.FT.TotCost, 2);
                    this.fpUndrug_Sheet1.Cells[i, 9].Value = undrugItem.FT.PubCost;
                    this.fpUndrug_Sheet1.Cells[i,10].Value = undrugItem.FeeOper.OperTime;
                    this.fpUndrug_Sheet1.Rows[i].Tag = undrugItem;

                }

            }
            return 1;
        }

        /// <summary>
        /// 清屏
        /// </summary>
        protected virtual void Clear()
        {
            this.patientInfo = null;
            if (fpDrug_Sheet1.Rows.Count >0 )
            {
                this.fpDrug_Sheet1.RemoveRows(0, this.fpDrug_Sheet1.RowCount);
            }
            if (this.fpUndrug_Sheet1.Rows.Count > 0)
            {
                this.fpUndrug_Sheet1.RemoveRows(0, this.fpUndrug_Sheet1.RowCount);
            }
            this.txtDept.Text = "";
            this.txtDoctor.Text = "";
            this.txtName.Text = "";
            this.txtNurseStation.Text = "";
            this.txtOldPact.Text = "";
            this.cmbNewPact.Text = "";
            this.txtBirthday.Text = "";
            this.txtDateIn.Text = "";
            this.txtBedNo.Text = "";
            this.txtPact.Text = "";
            this.ucQueryInpatientNo1.Focus();

        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <returns></returns>
        protected int IsValid()
        {
            if (this.patientInfo == null || string.IsNullOrEmpty(this.patientInfo.ID)) 
            {
                MessageBox.Show("没有患者基本信息，请正确输入住院号并回车确认!");

                return -1;
            }
            
            //判断合同单位
            if (this.cmbNewPact.SelectedIndex < 0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择合同单位"));
                return -1;
            }
            if (this.cmbNewPact.SelectedItem.ID == this.txtOldPact.Tag.ToString())
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("新合同单位与原合同单位相同，请重新选择"));
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 根据合同单位标示返回支付类别名称
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        private FS.FrameWork.Models.NeuObject GetPactUnitByID(string strID)
        {
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            FS.HISFC.Models.Base.PactInfo p = new FS.HISFC.Models.Base.PactInfo();
            p = this.myPactUnit.GetPactUnitInfoByPactCode(strID);
            if (p == null)
            {
                MessageBox.Show("检索合同单位出错" + this.myPactUnit.Err, "提示");
                return null;
            }
            if (p.PayKind.ID == "" || p.PayKind == null)
            {
                MessageBox.Show("该合同单位的结算类别没有维护", "提示");
                return null;
            }
            else
            {
                switch (p.PayKind.ID)
                {
                    case "01":
                        obj.Name = "自费"; obj.ID = "01";
                        break;
                    case "02":
                        obj.Name = "保险";
                        obj.ID = "02";
                        break;
                    case "03":
                        obj.Name = "公费在职";
                        obj.ID = "03";
                        break;
                    case "04":
                        obj.Name = "公费退休";
                        obj.ID = "04";
                        break;
                    case "05":
                        obj.Name = "公费高干";
                        obj.ID = "05";
                        break;
                    default:
                        break;
                }
            }
            return obj;
        }

        /// <summary>
        /// 身份变更确认操作
        /// </summary>
        /// <returns></returns>
        protected virtual int ChangePact()
        {
            if (this.IsValid() < 0)
            {
                return -1;
            }


            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在处理明细数据……");
            Application.DoEvents();

            FS.FrameWork.Models.NeuObject newPactObj = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.Models.NeuObject oldPactObj = new FS.FrameWork.Models.NeuObject();

            //合同单位信息
            FS.FrameWork.Models.NeuObject selectPactObj = new FS.FrameWork.Models.NeuObject();

            //备份收费药品信息
            ArrayList alDruglistBackUp = new ArrayList();
            //备份收费药品信息
            ArrayList alUndruglistBackUp = new ArrayList();

            selectPactObj = this.GetPactUnitByID(this.cmbNewPact.SelectedItem.ID);

            bool isChangeFeeDetail = true;
            if (this.needChangeFeeDetailPacts==null || this.needChangeFeeDetailPacts.Equals("|") || this.needChangeFeeDetailPacts.Equals("ALL") || this.needChangeFeeDetailPacts.Equals("ALL|"))
            {
                isChangeFeeDetail = true;
            }
            else if (this.needChangeFeeDetailPacts.Contains(this.cmbNewPact.SelectedItem.ID + "|")||this.needChangeFeeDetailPacts.Contains("|"+this.cmbNewPact.SelectedItem.ID)
                ||this.needChangeFeeDetailPacts.Equals(this.cmbNewPact.SelectedItem.ID))
            {
                isChangeFeeDetail = true;
            }
            else if (this.needChangeFeeDetailPacts.Contains(this.patientInfo.Pact.ID + "|")
                || this.needChangeFeeDetailPacts.Contains("|" + this.patientInfo.Pact.ID)
            ||this.needChangeFeeDetailPacts.Equals(this.patientInfo.Pact.ID))
            {
                isChangeFeeDetail = true;
            }
            else
            {
                isChangeFeeDetail = false;
            }


            this.medcareInterfaceProxy.SetPactCode(this.cmbNewPact.SelectedItem.ID);

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
            pharmacyIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            this.InpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.myPactUnit.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            feeIntegrate.MessageType = this.MessageType;

            newPactObj.ID = this.cmbNewPact.SelectedItem.ID;	//1 合同单位代码
            newPactObj.Name = this.cmbNewPact.SelectedItem.Name;		//2 合同单位名称
            newPactObj.User01 = selectPactObj.ID;		//3 结算类别代码
            newPactObj.User02 = selectPactObj.Name;		//4 结算类别名称
            newPactObj.User03 = this.patientInfo.SSN; //医疗证号

            oldPactObj.ID = this.txtOldPact.Tag.ToString();
            oldPactObj.Name = this.txtOldPact.Text;


            long returnValue = 0;
            FS.HISFC.Models.Fee.Inpatient.FeeItemList olbUndrugItem = null;
            FS.HISFC.Models.Fee.Inpatient.FeeItemList newUndrugItem = null;
            FS.HISFC.Models.Fee.Inpatient.FeeItemList oldMedicineItem = null;
            FS.HISFC.Models.Fee.Inpatient.FeeItemList newMedicineItem = null;
            if (isChangeFeeDetail)
            {
                #region 退费


                #region 处理非药品


                //处理非药品 
                if (this.fpUndrug_Sheet1.RowCount > 0)
                {
                    ArrayList alOldUndrugItem = new ArrayList();

                    for (int i = 0; i < fpUndrug_Sheet1.RowCount; i++)
                    {
                        //olbUndrugItem = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                        //newUndrugItem = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();

                        if (this.fpUndrug_Sheet1.Rows[i].Tag != null)
                        {
                            olbUndrugItem = (FS.HISFC.Models.Fee.Inpatient.FeeItemList)this.fpUndrug_Sheet1.Rows[i].Tag;

                            //将数量变成可退数量进行退费，然后重新收取相同数量（可退）的费用
                            olbUndrugItem.Item.Qty = olbUndrugItem.NoBackQty;
                            olbUndrugItem.FT.TotCost = olbUndrugItem.Item.Price * olbUndrugItem.Item.Qty / olbUndrugItem.Item.PackQty;
                            olbUndrugItem.FT.OwnCost = olbUndrugItem.FT.TotCost;

                            newUndrugItem = olbUndrugItem.Clone();

                            olbUndrugItem.ExtFlag2 = "3";//变更标志 
                            olbUndrugItem.FTSource.SourceType3 = "1";
                            olbUndrugItem.NoBackQty = 0;
                            olbUndrugItem.IsNeedUpdateNoBackQty = true;

                            alOldUndrugItem.Add(olbUndrugItem);

                            //if (this.feeIntegrate.QuitItem(this.patientInfo, olbUndrugItem) == -1)
                            //{
                            //    FS.FrameWork.Management.PublicTrans.RollBack();
                            //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            //    MessageBox.Show("调用中间层出错,请确认是否有未全退的费用" + this.feeIntegrate.Err);
                            //    this.fpUndrug.SetViewportTopRow(0, i);
                            //    this.fpUndrug_Sheet1.ActiveRowIndex = i;
                            //    return -1;
                            //}

                            //记录原处方号
                            newUndrugItem.Patient.User03 = newUndrugItem.RecipeNO;
                            newUndrugItem.Patient.User02 = newUndrugItem.SequenceNO.ToString();
                            //收费
                            newUndrugItem.RecipeNO = this.InpatientManager.GetUndrugRecipeNO();
                            newUndrugItem.Patient.Pact.ID = this.cmbNewPact.SelectedItem.ID;
                            newUndrugItem.FeeOper.ID = this.InpatientManager.Operator.ID;
                            newUndrugItem.FeeOper.OperTime = this.InpatientManager.GetDateTimeFromSysDateTime();
                            //清空打折信息，会自动重新计算
                            newUndrugItem.FT.RebateCost = 0m;

                            alUndruglistBackUp.Add(newUndrugItem.Clone());
                        }
                        else
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("从界面提取数据出错 " + this.feeIntegrate.Err);
                            this.Clear();
                            return -1;
                        }
                    }
                    if (alOldUndrugItem != null && alOldUndrugItem.Count > 0)
                    {
                        if (this.feeIntegrate.QuitItem(this.patientInfo, ref alOldUndrugItem) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("调用中间层出错,请确认是否有未全退的费用 " + this.feeIntegrate.Err);
                            //this.fpUndrug.SetViewportTopRow(0, i);
                            //this.fpUndrug_Sheet1.ActiveRowIndex = i;
                            return -1;
                        }
                    }
                }
                #endregion

                #region 处理药品


                //处理药品
                if (this.fpDrug_Sheet1.RowCount > 0)
                {
                    ArrayList alOldMedItem = new ArrayList();
                    for (int i = 0; i < fpDrug_Sheet1.RowCount; i++)
                    {
                        //oldMedicineItem = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                        //newMedicineItem = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();

                        if (this.fpDrug_Sheet1.Rows[i].Tag != null)
                        {
                            oldMedicineItem = (FS.HISFC.Models.Fee.Inpatient.FeeItemList)this.fpDrug_Sheet1.Rows[i].Tag;

                            oldMedicineItem.Item.Qty = oldMedicineItem.NoBackQty;
                            oldMedicineItem.FT.TotCost = oldMedicineItem.Item.Price * oldMedicineItem.Item.Qty / oldMedicineItem.Item.PackQty;
                            oldMedicineItem.FT.OwnCost = oldMedicineItem.FT.TotCost;

                            newMedicineItem = oldMedicineItem.Clone();
                            oldMedicineItem.ExtFlag2 = "3";//变更标志 
                            oldMedicineItem.FTSource.SourceType3 = "1";

                            oldMedicineItem.NoBackQty = 0;
                            oldMedicineItem.IsNeedUpdateNoBackQty = true;
                            //if (this.feeIntegrate.QuitItem(this.patientInfo, oldMedicineItem) == -1)
                            //{
                            //    FS.FrameWork.Management.PublicTrans.RollBack();
                            //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            //    MessageBox.Show("调用中间层出错,请确认是否有未全退的费用" + this.feeIntegrate.Err);
                            //    this.fpDrug.SetViewportTopRow(0, i);
                            //    this.fpDrug_Sheet1.ActiveRowIndex = i;
                            //    return -1;
                            //}

                            alOldMedItem.Add(oldMedicineItem);


                            //收费
                            //存储旧处方号
                            newMedicineItem.BalanceOper.User01 = newMedicineItem.RecipeNO;

                            newMedicineItem.RecipeNO = this.InpatientManager.GetDrugRecipeNO();
                            newMedicineItem.Patient.Pact.ID = this.cmbNewPact.SelectedItem.ID;
                            newMedicineItem.FeeOper.ID = this.InpatientManager.Operator.ID;
                            newMedicineItem.FeeOper.OperTime = this.InpatientManager.GetDateTimeFromSysDateTime();
                            //备份药品信息
                            //alDruglistBackUp.Add(newMedicineItem.Clone());
                            alDruglistBackUp.Add(newMedicineItem);
                        }
                        else
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("从界面提取数据出错 " + this.feeIntegrate.Err);
                            this.Clear();
                            return -1;
                        }
                    }

                    if (alOldMedItem != null && alOldMedItem.Count > 0)
                    {
                        if (this.feeIntegrate.QuitItem(this.patientInfo, ref alOldMedItem) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("调用中间层出错,请确认是否有未全退的费用" + this.feeIntegrate.Err);
                            //this.fpDrug.SetViewportTopRow(0, i);
                            //this.fpDrug_Sheet1.ActiveRowIndex = i;
                            return -1;
                        }
                    }


                }
                #endregion


                #endregion

                #region 医保接口处理
                FS.HISFC.Models.RADT.PatientInfo siPatientInfo = this.patientInfo.Clone();
                //更改合同单位，默认全部退费
                siPatientInfo.FT.TotCost = 0;
                this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                returnValue = this.medcareInterfaceProxy.Connect();
                if (returnValue < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();

                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("请确保待遇接口存在或正常初始化初始化失败" + this.medcareInterfaceProxy.ErrMsg);
                    this.Clear();
                    return -1;
                }

                returnValue = this.medcareInterfaceProxy.GetRegInfoInpatient(siPatientInfo);
                if (returnValue < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();

                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("待遇接口获得患者信息失败" + this.medcareInterfaceProxy.ErrMsg);
                    this.Clear();
                    return -1;
                }

                returnValue = this.medcareInterfaceProxy.LogoutInpatient(siPatientInfo);
                if (returnValue < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();

                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("待遇接口无费退院失败" + this.medcareInterfaceProxy.ErrMsg);
                    this.Clear();
                    return -1;
                }
                #endregion
            }
            //更新主表合同单位信息
            if (this.radtIntegrate.SetPactShiftData(this.patientInfo, newPactObj, oldPactObj) != 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("调用中间层出错");
                this.Clear();
                return -1;
            }

            //获得更改后得患者信息
            this.oENewPatientInfo = this.radtIntegrate.GetPatientInfomation(this.patientInfo.ID);

            if (this.oENewPatientInfo == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("获得变更后患者信息出错!!", "提示");
                this.Clear();
                return -1;
            }
            returnValue = 0;
            this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            returnValue = this.medcareInterfaceProxy.Connect();
            if (returnValue < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.medcareInterfaceProxy.Rollback();

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("请确保待遇接口存在或正常初始化初始化失败" + this.medcareInterfaceProxy.ErrMsg);
                this.Clear();
                return -1;
            }

            returnValue = this.medcareInterfaceProxy.GetRegInfoInpatient(oENewPatientInfo);
            if (returnValue < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.medcareInterfaceProxy.Rollback();
                //{0C35F3E3-2E72-4ae3-8809-FF3809DA2A16}
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("待遇接口获得患者信息失败" + this.medcareInterfaceProxy.ErrMsg);
                this.Clear();
                return -1;
            }
            returnValue = this.medcareInterfaceProxy.UploadRegInfoInpatient(oENewPatientInfo);
            if (returnValue < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.medcareInterfaceProxy.Rollback();
                //{0C35F3E3-2E72-4ae3-8809-FF3809DA2A16}
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("待遇接口上传住院登记信息失败" + this.medcareInterfaceProxy.ErrMsg);
                this.Clear();
                return -1;
            }

            if (isChangeFeeDetail)
            {
                #region 重新收费

                #region 处理非药品

                ArrayList alNewFeeItem = new ArrayList();

                //处理非药品 
                if (alUndruglistBackUp.Count > 0)
                {
                    alNewFeeItem.AddRange(alUndruglistBackUp);


                    ////newUndrugItem = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();

                    //for (int i = 0; i < alUndruglistBackUp.Count; i++)
                    //{
                    //    //newUndrugItem = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                    //    newUndrugItem = (FS.HISFC.Models.Fee.Inpatient.FeeItemList)alUndruglistBackUp[i];

                    //    //改为批量收费
                    //    alNewFeeItem.Add(newUndrugItem.Clone());
                    //    //if (this.feeIntegrate.FeeItem(this.oENewPatientInfo, newUndrugItem) == -1)
                    //    //{
                    //    //    FS.FrameWork.Management.PublicTrans.RollBack();
                    //    //    this.medcareInterfaceProxy.Rollback();
                    //    //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    //    //    MessageBox.Show("调用中间层出错" + this.feeIntegrate.Err);
                    //    //    this.Clear();
                    //    //    return -1;
                    //    //}
                    //}


                }
                #endregion

                #region 处理药品

                //处理药品
                if (alDruglistBackUp.Count > 0)
                {
                    oldMedicineItem = null;
                    newMedicineItem = null;
                    FS.HISFC.Models.Pharmacy.Item phaItemObj = null;

                    decimal price = 0;
                    decimal orgPrice = 0;

                    this.htPhaItem = new Hashtable();

                    for (int i = 0; i < alDruglistBackUp.Count; i++)
                    {
                        //oldMedicineItem = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                        oldMedicineItem = (FS.HISFC.Models.Fee.Inpatient.FeeItemList)alDruglistBackUp[i];

                        if (oldMedicineItem != null)
                        {
                            newMedicineItem = oldMedicineItem.Clone();

                            //重新根据合同单位计算价格
                            phaItemObj = new FS.HISFC.Models.Pharmacy.Item();
                            if (this.htPhaItem.Contains(newMedicineItem.Item.ID))
                            {
                                phaItemObj = htPhaItem[newMedicineItem.Item.ID] as FS.HISFC.Models.Pharmacy.Item;
                            }
                            else
                            {
                                phaItemObj = pharmacyIntegrate.GetItem(newMedicineItem.Item.ID);
                                this.htPhaItem.Add(newMedicineItem.Item.ID, phaItemObj);
                            }
                            if (phaItemObj == null)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("重新获取 【" + newMedicineItem.Item.Name + "】 价格出错！！");
                                return -1;
                            }


                            price = phaItemObj.RetailPrice2;
                            orgPrice = phaItemObj.Price;

                            if (this.feeIntegrate.GetPriceForInpatient(this.oENewPatientInfo, phaItemObj, ref price, ref orgPrice) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("重新获取【" + newMedicineItem.Item.Name + "】 价格出错！！");
                                return -1;
                            }

                            newMedicineItem.Item.Price = price;
                            newMedicineItem.Item.DefPrice = orgPrice;
                            newMedicineItem.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(price * newMedicineItem.Item.Qty / phaItemObj.PackQty, 2);
                            newMedicineItem.FT.OwnCost = newMedicineItem.FT.TotCost;
                            newMedicineItem.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber(orgPrice * newMedicineItem.Item.Qty / phaItemObj.PackQty, 2);

                            //此处收费改为批量收费
                            alNewFeeItem.Add(newMedicineItem);
                            //if (this.feeIntegrate.FeeItem(this.oENewPatientInfo, newMedicineItem) == -1)
                            //{
                            //    FS.FrameWork.Management.PublicTrans.RollBack();
                            //    this.medcareInterfaceProxy.Rollback();
                            //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            //    MessageBox.Show("调用中间层出错" + this.feeIntegrate.Err);
                            //    this.Clear();
                            //    return -1;
                            //}

                            //更新发药标记等，要放到费用收取之后

                            //更新费用明细表发药标记 addbyhuazb
                            //if (this.InpatientManager.UpdateMedItemExecInfo(oldMedicineItem.RecipeNO, oldMedicineItem.SequenceNO
                            //, oldMedicineItem.UpdateSequence, oldMedicineItem.SendSequence, (int)oldMedicineItem.PayType,
                            //oldMedicineItem.StockOper.Dept.ID, oldMedicineItem.ExecOper.ID, oldMedicineItem.ExecOper.OperTime) == -1)
                            //{
                            //    FS.FrameWork.Management.PublicTrans.RollBack();
                            //    this.medcareInterfaceProxy.Rollback();
                            //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            //    MessageBox.Show("调用中间层出错" + this.feeIntegrate.Err);
                            //    this.Clear();
                            //    return -1;
                            //}

                            ////更新出库申请表中旧的处方号等为最新的
                            //if (pharmacyIntegrate.UpdateApplyOutRecipe(oldMedicineItem.BalanceOper.User01, oldMedicineItem.SequenceNO, oldMedicineItem.RecipeNO, oldMedicineItem.SequenceNO) == -1)
                            //{
                            //    FS.FrameWork.Management.PublicTrans.RollBack();
                            //    this.medcareInterfaceProxy.Rollback();
                            //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            //    MessageBox.Show("更新药品申请表处方号信息发生错误！" + pharmacyIntegrate.Err);
                            //    this.Clear();
                            //    return -1;
                            //}
                        }
                    }
                }
                #endregion

                if (alNewFeeItem.Count > 0)
                {
                    //批量收费
                    if (this.feeIntegrate.FeeItem(this.oENewPatientInfo, ref alNewFeeItem) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.medcareInterfaceProxy.Rollback();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("调用中间层出错" + this.feeIntegrate.Err);
                        this.Clear();
                        return -1;
                    }
                }

                //待收费完成后，更新药品明细和发药申请表信息
                for (int i = 0; i < alDruglistBackUp.Count; i++)
                {
                    //oldMedicineItem = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                    oldMedicineItem = (FS.HISFC.Models.Fee.Inpatient.FeeItemList)alDruglistBackUp[i];

                    if (oldMedicineItem != null)
                    {
                        //更新费用明细表发药标记 addbyhuazb
                        if (this.InpatientManager.UpdateMedItemExecInfo(oldMedicineItem.RecipeNO, oldMedicineItem.SequenceNO
                        , oldMedicineItem.UpdateSequence, oldMedicineItem.SendSequence, (int)oldMedicineItem.PayType,
                        oldMedicineItem.StockOper.Dept.ID, oldMedicineItem.ExecOper.ID, oldMedicineItem.ExecOper.OperTime) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            this.medcareInterfaceProxy.Rollback();
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("调用中间层出错" + this.feeIntegrate.Err);
                            this.Clear();
                            return -1;
                        }

                        //更新出库申请表中旧的处方号等为最新的
                        if (pharmacyIntegrate.UpdateApplyOutRecipe(oldMedicineItem.BalanceOper.User01, oldMedicineItem.SequenceNO, oldMedicineItem.RecipeNO, oldMedicineItem.SequenceNO) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            this.medcareInterfaceProxy.Rollback();
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("更新药品申请表处方号信息发生错误！" + pharmacyIntegrate.Err);
                            this.Clear();
                            return -1;
                        }
                    }
                }

                #endregion
            }

            if (InterfaceManager.GetIADT() != null)
            {
                if (InterfaceManager.GetIADT().PatientInfo(this.patientInfo, this.patientInfo) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    MessageBox.Show(this, "身份变更失败，请向系统管理员报告错误信息：" + InterfaceManager.GetIADT().Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return -1;

                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            this.medcareInterfaceProxy.Commit();
            MessageBox.Show(FS.FrameWork.Management.Language.Msg("变更成功"));
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            //重新显示界面
            this.SetpatientInfo(this.oENewPatientInfo);

            //重新显示费用明细
            this.DisplayDetail(this.oENewPatientInfo.ID);

            return 1;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.ChangePact();
            return base.OnSave(sender, neuObject);
        }

        #endregion

        #region 事件
        private void ucQueryInpatientNo1_myEvent()
        {
            this.Clear();
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo.Trim() == "")
            {
                if (this.ucQueryInpatientNo1.Err == "")
                {
                    this.ucQueryInpatientNo1.Err = "此患者不在院";
                }
                FS.FrameWork.WinForms.Classes.Function.Msg(this.ucQueryInpatientNo1.Err, 211);

                this.ucQueryInpatientNo1.Focus();
                return;
            }
            this.patientInfo = this.radtIntegrate.GetPatientInfomation(this.ucQueryInpatientNo1.InpatientNo);
            if (this.patientInfo.PVisit.InState.ID.ToString() == FS.HISFC.Models.Base.EnumInState.N.ToString() || this.patientInfo.PVisit.InState.ID.ToString() == FS.HISFC.Models.Base.EnumInState.O.ToString())
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("该患者已经出院!", 111);

                this.patientInfo.ID = null;

                return;
            }
            //界面显示基本信息
            this.SetpatientInfo(this.patientInfo);
            //费用信息
            this.DisplayDetail(this.patientInfo.ID);
        }

        private void ucChangePact_Load(object sender, EventArgs e)
        {
           
            this.Init();
        }
        #endregion
    }
}