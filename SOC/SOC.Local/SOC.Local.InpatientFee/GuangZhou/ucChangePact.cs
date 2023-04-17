using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.GuangZhou
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

        bool isSetAlertMoney = true;
        [Category("控件设置"), Description("身份变更后是否自动设置默认警戒线 true:是 false:否")]
        public bool IsSetAlertMoney 
        {
            get { return isSetAlertMoney; }
            set { isSetAlertMoney = value; }
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
            //if (this.DisplayDrugDetail(inpatientNO) < 0)
            //{
            //    return -1;
            //}
            ////非药品信息


            //if (this.DisplayUndrugDetail(inpatientNO) < 0)
            //{
            //    return -1;
            //}

            
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

            //#region 判断未确认的退费申请

            //ArrayList applys = this.feeIntegrate.QueryReturnApplys(this.patientInfo.ID, false);
            //if (applys == null)
            //{
            //    MessageBox.Show(FS.FrameWork.Management.Language.Msg(feeIntegrate.Err));
            //    return -1;
            //}
            //if (applys.Count > 0) //存在退费申请提示是否需要做院登记
            //{
            //    string itemInfo = "项目:\r\n";
            //    foreach (FS.HISFC.Models.Fee.ReturnApply returnApply in applys)
            //    {
            //        itemInfo += returnApply.Item.Name + "--(" + managerIntegrate.GetDepartment(returnApply.ExecOper.Dept.ID).Name + ")" + "\r\n";
            //    }

            //   MessageBox.Show("还有未确认的退费申请，请先确认退费申请再进行身份变更？" + itemInfo, "警告"
            //            , MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            //    return -1;
            //}

            //#endregion           

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

        bool isDoing = false;
        /// <summary>
        /// 身份变更确认操作
        /// </summary>
        /// <returns></returns>
        protected virtual int ChangePact()
        {
            if (isDoing)
            {
                //正在身份变更
                return 1;
            }
            isDoing = true;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在处理明细数据……");
            Application.DoEvents();

            if (this.IsValid() < 0)
            {
                isDoing = false;
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return -1;
            }



            FS.FrameWork.Models.NeuObject newPactObj = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.Models.NeuObject oldPactObj = new FS.FrameWork.Models.NeuObject();

            //合同单位信息
            FS.FrameWork.Models.NeuObject selectPactObj = new FS.FrameWork.Models.NeuObject();

            //备份收费药品信息
            ArrayList alDruglistBackUp = new ArrayList();
            //备份收费药品信息
            ArrayList alUndruglistBackUp = new ArrayList();
            //是否原合同单位是公费
            bool IsChangeFromPub = false;
            selectPactObj = this.GetPactUnitByID(this.cmbNewPact.SelectedItem.ID);
            if (selectPactObj.ID == "03")
            {
                if (string.IsNullOrEmpty(this.patientInfo.SSN))
                {
                    MessageBox.Show("公费患者" + this.patientInfo.Name + "没有输入医疗证号，请到修改患者基本信息处录入该患者的医疗证号!");
                    isDoing = false;
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return -1;
                }
            }
            long returnValue = 0;
            //取消原合同单位得住院登记
            if (this.patientInfo.Pact.PayKind.ID == "02")
            {
                this.medcareInterfaceProxy.SetPactCode(this.patientInfo.Pact.ID);

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                returnValue = this.medcareInterfaceProxy.CancelRegInfoInpatient(this.patientInfo);
                if (returnValue < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    MessageBox.Show("取消医保登记失败，" + this.medcareInterfaceProxy.ErrMsg);
                    this.Clear();
                    isDoing = false;
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();

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

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在处理明细数据……");
            Application.DoEvents();

            FS.HISFC.Models.RADT.PatientInfo pubPatientInfo = this.patientInfo.Clone();
            pubPatientInfo.Pact.ID = newPactObj.ID;
            pubPatientInfo.Pact.Name = newPactObj.Name;
            pubPatientInfo.Pact.PayKind.ID = newPactObj.User01;
            pubPatientInfo.Pact.PayKind.Name = newPactObj.User02;

            this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            returnValue = this.medcareInterfaceProxy.Connect();
            if (returnValue < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.medcareInterfaceProxy.Rollback();

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("请确保待遇接口存在或正常初始化初始化失败" + this.medcareInterfaceProxy.ErrMsg);
                this.Clear();
                isDoing = false;
                return -1;
            }

            returnValue = this.medcareInterfaceProxy.GetRegInfoInpatient(pubPatientInfo);
            if (returnValue < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.medcareInterfaceProxy.Rollback();

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("待遇接口获得患者信息失败" + this.medcareInterfaceProxy.ErrMsg);
                this.Clear();
                isDoing = false;
                return -1;
            }

            returnValue = this.medcareInterfaceProxy.UploadRegInfoInpatient(pubPatientInfo);
            if (returnValue < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.medcareInterfaceProxy.Rollback();
                //{0C35F3E3-2E72-4ae3-8809-FF3809DA2A16}
                MessageBox.Show("待遇接口上传住院登记信息失败" + this.medcareInterfaceProxy.ErrMsg);
                this.Clear();
                isDoing = false;
                return -1;
            }
            //更新主表合同单位信息
            if (this.radtIntegrate.SetPactShiftData(pubPatientInfo, newPactObj, oldPactObj) != 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("调用中间层出错");
                this.Clear();
                isDoing = false;
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
                isDoing = false;
                return -1;
            }

            SOC.HISFC.InpatientFee.BizProcess.Fee socFeeManager = new FS.SOC.HISFC.InpatientFee.BizProcess.Fee();
            socFeeManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (socFeeManager.ProcessChangePact(this.oENewPatientInfo,this.patientInfo.Pact, this.oENewPatientInfo.Pact) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.medcareInterfaceProxy.Rollback();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("身份变更失败，" + this.medcareInterfaceProxy.ErrMsg);
                this.Clear();
                isDoing = false;
                return -1;
            }


            //调整警戒线，不放在同一事物中
            //if (this.IsSetAlertMoney)
            {
                FS.HISFC.BizLogic.Manager.Constant conStant = new FS.HISFC.BizLogic.Manager.Constant();
                FS.FrameWork.Models.NeuObject conStantObj = null;
                if (Function.IsContainYKDept())
                {
                    conStantObj = conStant.GetConstant("YKMONEYALERT", this.oENewPatientInfo.Pact.ID);

                    if (string.IsNullOrEmpty(conStantObj.ID))
                    {
                        conStantObj = conStant.GetConstant("YKMONEYALERT", this.oENewPatientInfo.Pact.PayKind.ID);
                    }
                }
                else
                {
                    conStantObj = conStant.GetConstant("MONEYALERT", this.oENewPatientInfo.Pact.ID);

                    if (string.IsNullOrEmpty(conStantObj.ID))
                    {
                        conStantObj = conStant.GetConstant("MONEYALERT", this.oENewPatientInfo.Pact.PayKind.ID);
                    }
                }
                if (conStantObj == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("默认警戒线没有维护，请先维护！");
                    return -1;
                }
                if (FS.FrameWork.Public.String.IsNumeric(conStantObj.Memo))
                {
                    this.oENewPatientInfo.PVisit.MoneyAlert = FS.FrameWork.Function.NConvert.ToDecimal(conStantObj.Memo);
                }
                else
                {
                    if (Function.IsContainYKDept())
                    {
                        this.oENewPatientInfo.PVisit.MoneyAlert = -5000m;
                    }
                    else
                    {
                        this.oENewPatientInfo.PVisit.MoneyAlert = 0m;
                    }
                }
                if (this.radtIntegrate.UpdatePatientAlert(this.patientInfo.ID, oENewPatientInfo.PVisit.MoneyAlert, FS.HISFC.Models.Base.EnumAlertType.M.ToString(), DateTime.MinValue, DateTime.MinValue) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新警戒线失败！" + radtIntegrate.Err);
                    return -1;
                }
            }

            if (InterfaceManager.GetIADT() != null)
            {
                if (InterfaceManager.GetIADT().PatientInfo(this.patientInfo, this.patientInfo) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    MessageBox.Show(this, "身份变更失败，请向系统管理员报告错误信息：" + InterfaceManager.GetIADT().Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    isDoing = false;
                    return -1;

                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            this.medcareInterfaceProxy.Commit();

            MessageBox.Show(FS.FrameWork.Management.Language.Msg("变更成功"));



            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            isDoing = false;

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

        private void cmbNewPact_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string str = this.cmbNewPact.SelectedItem.ID;
            //if (!string.IsNullOrEmpty(str))
            //{
            //    string strPayKind = this.GetPactUnitByID(str).ID;
            //    if (strPayKind != "01" && strPayKind != "02")
            //    {
            //        ucPatientPubInfo uc = new ucPatientPubInfo();

            //        if (this.patientInfo.ID != null && this.patientInfo.ID.ToString() != "")
            //        {
            //            uc.PatientInfo = this.patientInfo;
            //            uc.Name = "公费信息";
            //        }
            //        else
            //        {
            //            MessageBox.Show("请输入住院号确认患者!", "提示!");
            //            this.cmbNewPact.Text = "";
            //            this.cmbNewPact.Tag = null;
            //            return;
            //        }
            //        FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
            //    }
            //}
    
        }
    }
}