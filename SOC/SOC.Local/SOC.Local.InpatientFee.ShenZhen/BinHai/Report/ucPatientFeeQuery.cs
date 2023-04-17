using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.ShenZhen.BinHai.Report
{
    public partial class ucPatientFeeQuery : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucPatientFeeQuery()
        {
            InitializeComponent();

            //明细信息显示窗口
            //this.frmPatientFeeByMinFee.Controls.Add(this.ucPatientFeeByMinFee);
            //this.frmPatientFeeByMinFee.Size = this.ucPatientFeeByMinFee.Size;
            //this.ucPatientFeeByMinFee.Dock = DockStyle.Fill;
            //this.frmPatientFeeByMinFee.StartPosition = FormStartPosition.CenterParent;
            //this.frmPatientFeeByMinFee.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            //this.frmPatientFeeByMinFee.Visible = false;
        }

        #region 变量

        /// <summary>
        /// 起始时间
        /// </summary>
        private DateTime BeginTime = System.DateTime.MaxValue;

        /// <summary>
        /// 终止时间
        /// </summary>
        private DateTime EndTime = System.DateTime.MaxValue;

        /// <summary>
        /// 明细信息控件
        /// </summary>
        //FS.HISFC.Fee.ucPatientFeeByMinFee ucPatientFeeByMinFee = new FS.UFC.InpatientFee.Fee.ucPatientFeeByMinFee();

        /// <summary>
        /// 明细信息窗口
        /// </summary>
        Form frmPatientFeeByMinFee = new Form();

        private bool Timefalg = true;

        private bool isAll = false;
        private bool isInvoiceNo = false;
        private string invoiceNo = string.Empty;
        
        /// <summary>
        /// 住院如出转业务层
        /// </summary>
        FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();

        private FS.HISFC.BizLogic.Fee.FeeCodeStat feeCodeStat = new FS.HISFC.BizLogic.Fee.FeeCodeStat();
        /// <summary>
        /// 诊断业务层
        /// </summary>
        FS.HISFC.BizLogic.HealthRecord.Diagnose diagnoseManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        /// <summary>
        /// 科室业务层
        /// </summary>
        FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        /// <summary>
        /// 人员信息业务层
        /// </summary>
        FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();

        /// <summary>
        /// 常数业务层
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 费用业务层
        /// </summary>
        FS.HISFC.BizLogic.Fee.InPatient feeManager = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// 当前患者
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo currentPatient = new FS.HISFC.Models.RADT.PatientInfo();

        #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
        /// <summary>
        /// 科室帮助类
        /// </summary>
        FS.FrameWork.Public.ObjectHelper departmentHelper = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// 人员帮助类
        /// </summary>
        FS.FrameWork.Public.ObjectHelper employeeHelper = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// 最小费用帮助类
        /// </summary>
        FS.FrameWork.Public.ObjectHelper constantHelper = new FS.FrameWork.Public.ObjectHelper();

        #endregion

        /// <summary>
        /// Tab
        /// </summary>
        protected Hashtable hashTableFp = new Hashtable();

        #region DataTalbe相关变量

        string pathNameMainInfo = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientMainInfo.xml";
        string pathNamePrepay = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientPrepay.xml";
        string pathNameFee = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientFee.xml";
        string pathNameDrugList = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientDrugList.xml";
        string pathNameUndrugList = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientUndrugList.xml";
        string pathNameBalance = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientBalance.xml";
        string pathNameDiagnose = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientDiagnose.xml";
        string pathNameChange = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientChange.xml";
        string pathNameFeeclass = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientFeeclass.xml";
        string pathNameDiagnoseList = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientDiagnoseList.xml";
        /// <summary>
        /// 患者主信息
        /// </summary>
        DataTable dtMainInfo = new DataTable();

        /// <summary>
        /// 患者主信息视图
        /// </summary>
        DataView dvMainInfo = new DataView();

        /// <summary>
        /// 药品明细
        /// </summary>
        DataTable dtDrugList = new DataTable();

        /// <summary>
        /// 药品明细视图
        /// </summary>
        DataView dvDrugList = new DataView();

        /// <summary>
        /// 非药品信息
        /// </summary>
        DataTable dtUndrugList = new DataTable();

        /// <summary>
        /// 非药品信息视图
        /// </summary>
        DataView dvUndrugList = new DataView();

        /// <summary>
        /// 预交金信息
        /// </summary>
        DataTable dtPrepay = new DataTable();

        /// <summary>
        /// 预交金视图
        /// </summary>
        DataView dvPrepay = new DataView();

        /// <summary>
        /// 费用汇总信息
        /// </summary>
        DataTable dtFee = new DataTable();

        /// <summary>
        /// 费用汇总信息视图
        /// </summary>
        DataView dvFee = new DataView();

        /// <summary>
        /// 收费项目汇总
        /// </summary>
        DataTable dtFeeclass = new DataTable();

        /// <summary>
        /// 收费项目汇总视图
        /// </summary>
        DataView dvFeeclass = new DataView();

        /// <summary>
        /// 入出院诊断信息
        /// </summary>
        DataTable dtDiagnoseList = new DataTable();

        /// <summary>
        /// 入出院诊断信息视图
        /// </summary>
        DataView dvDiagnoseList = new DataView();

        /// <summary>
        /// 费用结算信息
        /// </summary>
        DataTable dtBalance = new DataTable();

        /// <summary>
        /// 费用结算信息视图
        /// </summary>
        DataView dvBalance = new DataView();

        /// <summary>
        /// 变更信息
        /// </summary>
        DataTable dtChange = new DataTable();
        /// <summary>
        /// 变更信息视图
        /// </summary>
        DataView dvChange = new DataView();

        /// <summary>
        /// 药品临时信息视图
        /// </summary>
        DataView dvDrugListTemp = new DataView();

        /// <summary>
        /// 非药品临时信息视图
        /// </summary>
        DataView dvUndrugListTemp = new DataView();

        string filterInput = "1=1";	//输入码过滤条件
        #endregion

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        //{BEE371A5-5E62-41ab-B116-AB3840C70A9C}
        /// <summary>
        /// 本次结算费用明细
        /// </summary>
        ArrayList alFeeItemLists = new ArrayList();

        /// <summary>
        /// 存储主打印发票
        /// </summary>
        ArrayList alBalanceListHead = new ArrayList();

        //{B7A6B07C-165A-4a7b-B53C-A959F765D94D}
        ArrayList alBalancePay = new ArrayList();

        /// <summary>
        /// 事务
        /// </summary>
        //protected FS.FrameWork.Management.Transaction t;
        /// <summary>
        /// 医疗待遇接口
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
       
        #endregion

        #region 私有方法

        private void InitHashTable()
        {
            foreach (TabPage t in this.neuTabControl1.TabPages)
            {
                foreach (Control c in t.Controls)
                {
                    if (c is FarPoint.Win.Spread.FpSpread)
                    {
                        this.hashTableFp.Add(t, c);
                    }
                }
            }
            this.hashTableFp.Add(tabPage2, (Control)this.fpDrugList);
            this.hashTableFp.Add(tabPage3, (Control)this.fpUndrugList);
        }

        /// <summary>
        /// 初始化 
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int Init()
        {
            //初始化科室
            if (this.InitDept() == -1)
            {
                return -1;
            }

            this.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(System.DateTime.Now.ToShortDateString() + " 00:00:00");
            this.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(System.DateTime.Now.ToShortDateString() + " 23:59:59"); ;

            //初始化合同单位
            if (this.InitPact() == -1)
            {
                return -1;
            }

            //初始化入院状态
            if (this.InitInState() == -1)
            {
                return -1;
            }

            if (this.InitDataTable() == -1)
            {
                return -1;
            }
            #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
            //初始化帮助类
            if (this.InitHelper() == -1)
            {
                return -1;
            }
            #endregion
            this.InitHashTable();
            this.InitContr();

            //FS.Report.InpatientFee.ucFinIpbPatientDayFee uc1 = new FS.Report.InpatientFee.ucFinIpbPatientDayFee();
            //uc1.Dock = DockStyle.Fill;
            //uc1.Visible = true;
            //this.tpDayFee.Controls.Add(uc1);

            return 1;
        }

        /// <summary>
        /// 初始化入院状态
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        private int InitInState()
        {
            FS.FrameWork.Models.NeuObject objAll = new FS.FrameWork.Models.NeuObject();

            objAll.ID = "%";
            objAll.Name = "全部";

            ArrayList inStateList = FS.HISFC.Models.RADT.InStateEnumService.List();

            inStateList.Add(objAll);

            this.cmbPatientState.AddItems(inStateList);

            return 1;
        }

        #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
        /// <summary>
        /// 初始化帮助类
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int InitHelper()
        {
            int rtnValue = 0;
            FS.HISFC.BizProcess.Integrate.Manager m = new FS.HISFC.BizProcess.Integrate.Manager();

            departmentHelper.ArrayObject = m.GetDepartment();
            employeeHelper.ArrayObject = m.QueryEmployeeAll();
            constantHelper.ArrayObject = m.QueryConstantList("MINFEE");

            return rtnValue;
        }
        #endregion

        /// <summary>
        /// 初始化合同单位
        /// </summary>
        /// <returns>成功1 失败 -1</returns>
        private int InitPact()
        {
            int findAll = 0;
            FS.FrameWork.Models.NeuObject objAll = new FS.FrameWork.Models.NeuObject();

            objAll.ID = "%";
            objAll.Name = "全部";

            ArrayList pactList = this.consManager.GetList(FS.HISFC.Models.Base.EnumConstant.PACKUNIT);
            if (pactList == null)
            {
                MessageBox.Show(Language.Msg("加载合同单位列表出错!") + this.consManager.Err);

                return -1;
            }

            pactList.Add(objAll);

            findAll = pactList.IndexOf(objAll);

            this.cmbPact.AddItems(pactList);

            if (findAll >= 0)
            {
                this.cmbPact.SelectedIndex = findAll;
            }

            return 1;
        }

        /// <summary>
        /// 初始化科室
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        private int InitDept()
        {
            int findAll = 0;
            FS.FrameWork.Models.NeuObject objAll = new FS.FrameWork.Models.NeuObject();

            objAll.ID = "%";
            objAll.Name = "全部";

            ArrayList deptList = this.deptManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.I);
            if (deptList == null)
            {
                MessageBox.Show(Language.Msg("加载科室列表出错!") + this.deptManager.Err);

                return -1;
            }

            deptList.Add(objAll);

            findAll = deptList.IndexOf(objAll);

            this.cmbDept.AddItems(deptList);
            this.cmbHosDept1.AddItems(deptList);
            this.cmbHosDept2.AddItems(deptList);

            if (findAll >= 0)
            {
                this.cmbDept.SelectedIndex = findAll;
                this.cmbHosDept1.SelectedIndex = findAll;
                this.cmbHosDept2.SelectedIndex = findAll;
            }

            dtpBeginTime.Text = System.DateTime.Now.ToShortDateString() + " 00:00:00";
            dtpEndTime.Text = System.DateTime.Now.ToShortDateString() + " 23:59:59";
            return 1;
        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <param name="patients">患者信息列表</param>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int QueryPatient(ArrayList patients)
        {
            this.Clear();

            this.dtMainInfo.Rows.Clear();

            Cursor.Current = Cursors.WaitCursor;
            //住院主表信息
            foreach (FS.HISFC.Models.RADT.PatientInfo patient in patients)
            {
                this.SetPatientToFpMain(patient);
            }

            Cursor.Current = Cursors.Arrow;

            if (patients.Count == 1)
            {
                this.currentPatient = (FS.HISFC.Models.RADT.PatientInfo)patients[0];
                this.SetPatientInfo();
                if (this.currentPatient.PVisit.InState.ID.Equals("O"))
                {
                    this.toolBarService.SetToolButtonEnabled("取消医保结算", false);
                }
                else
                {
                    this.toolBarService.SetToolButtonEnabled("取消医保结算", true);
                }

                this.ucQueryInpatientNo1.Text = this.currentPatient.ID.Substring(4);
                //设置查询时间
                //设置查询时间
                DateTime beginTime = this.currentPatient.PVisit.InTime;
                DateTime endTime = this.radtManager.GetDateTimeFromSysDateTime();

                this.QueryAllInfomaition(beginTime, endTime);
            }
            fpMainInfo_Sheet1.DataSource = dtMainInfo;
            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpMainInfo_Sheet1, pathNameMainInfo);

            return 1;
        }

        /// <summary>
        /// 显示患者基本信息
        /// </summary>
        /// <param name="patient">成功 1 失败 -1</param>
        private void SetPatientToFpMain(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            //常数管理类
            FS.HISFC.BizLogic.Manager.Constant conMger = new FS.HISFC.BizLogic.Manager.Constant();
            FS.FrameWork.Models.NeuObject neuObj = new FS.FrameWork.Models.NeuObject();
            neuObj = conMger.GetConstant("SZPERSONTYPE", patient.PVisit.MedicalType.ID);
           

            DataRow row = this.dtMainInfo.NewRow();

            try
            {

                row["住院流水号"] = patient.ID;
                row["住院号"] = patient.PID.PatientNO;
                row["姓名"] = patient.Name;
                row["住院科室"] = patient.PVisit.PatientLocation.Dept.Name;
                row["床号"] = patient.PVisit.PatientLocation.Bed.ID;
                if (patient.Pact.ID == "1")
                    row["患者类别"] = patient.Pact.Name;
                else
                    row["患者类别"] = neuObj.Name;
                row["预交金(未结)"] = patient.FT.PrepayCost;
                row["费用合计(未结)"] = patient.FT.TotCost;
                row["余额"] = patient.FT.LeftCost;
                row["自费"] = patient.FT.OwnCost;
                row["自负"] = patient.FT.PayCost;
                row["公费"] = patient.FT.PubCost;
                row["入院日期"] = patient.PVisit.InTime;
                row["在院状态"] = patient.PVisit.InState.Name;

                row["出院日期"] = patient.PVisit.OutTime.Date == new DateTime(1, 1, 1).Date ? string.Empty : patient.PVisit.OutTime.ToString();

                row["预交金(已结)"] = patient.FT.BalancedPrepayCost;
                row["费用合计(已结)"] = patient.FT.BalancedCost;
                if (patient.Pact.ID == "2" && patient.ExtendFlag2 == "3")
                    row["住院类别"] = "病种";
                else row["住院类别"] = "普通";
                row["结算日期"] = patient.BalanceDate;
                row["门诊诊断"] = patient.ClinicDiagnose;
                row["备注"] ="" ;//patient.MemoInfo;

                this.dtMainInfo.Rows.Add(row);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return;
            }
        }

        /// <summary>
        /// 根据输入的住院号查询患者基本信息
        /// </summary>
        private void QueryPatientByInpatientNO()
        {
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo == string.Empty)
            {
                MessageBox.Show(Language.Msg("您输入的住院号错误,请重新输入!"));
                this.Clear();

                return;
            }

            FS.HISFC.Models.RADT.PatientInfo patientInfo = this.radtManager.QueryPatientInfoByInpatientNO(this.ucQueryInpatientNo1.InpatientNo);
            if (patientInfo == null || patientInfo.ID == null || patientInfo.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("或者患者基本信息出错!") + this.radtManager.Err);
                this.Clear();
                return;
            }

            this.Clear();

            this.txtName.Text = patientInfo.Name;
            this.btnQuery.Focus();

            ArrayList patientListTemp = new ArrayList();

            patientListTemp.Add(patientInfo);

            this.QueryPatient(patientListTemp);
        }

        /// <summary>
        /// 获得患者药品明细
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        private void QueryPatientDrugList(DateTime beginTime, DateTime endTime)
        {
            ArrayList drugList = new ArrayList();
            if (this.cmbHosDept1.Text == "全部")
            {
                drugList = this.feeManager.GetMedItemsForInpatient(this.currentPatient.ID, beginTime, endTime);
            }
            else
            {
              //  drugList = this.feeManager.GetMedItemsForInpatientByDept(this.currentPatient.ID, beginTime, endTime, this.cmbHosDept1.Tag.ToString());
            }
            if (drugList == null)
            {
                MessageBox.Show(Language.Msg("获得患者药品明细出错!") + this.feeManager.Err);

                return;
            }
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList obj in drugList)
            {
                DataRow row = dtDrugList.NewRow();

                row["药品名称"] = obj.Item.Name;
                FS.HISFC.Models.Pharmacy.Item medItem = (FS.HISFC.Models.Pharmacy.Item)obj.Item;
                row["规格"] = medItem.Specs;
                row["单价"] = obj.Item.Price;
                row["数量"] = obj.Item.Qty;
                row["付数"] = obj.Days;
                row["单位"] = obj.Item.PriceUnit;
                row["金额"] = obj.FT.TotCost;
                row["自费"] = obj.FT.OwnCost;
                row["公费"] = obj.FT.PubCost;
                row["自负"] = obj.FT.PayCost;
                row["优惠"] = obj.FT.RebateCost;

                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //row["执行科室"] = this.deptManager.GetDeptmentById(obj.ExecOper.Dept.ID).Name;
                //row["患者科室"] = this.deptManager.GetDeptmentById(((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID).Name; 
                row["执行科室"] = this.departmentHelper.GetName(obj.ExecOper.Dept.ID);
                row["患者科室"] = this.departmentHelper.GetName(((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID);
                #endregion
                row["收费时间"] = obj.FeeOper.OperTime;

                FS.HISFC.BizProcess.Integrate.Manager managerIntergrate = new FS.HISFC.BizProcess.Integrate.Manager();
                FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //empl = managerIntergrate.GetEmployeeInfo(obj.FeeOper.ID); 
                empl = this.employeeHelper.GetObjectFromID(obj.FeeOper.ID) as FS.HISFC.Models.Base.Employee;
                //变态有时候居然会出现查不出的人
                if (empl == null || empl.Name == string.Empty)
                #endregion
                {
                    row["收费员"] = obj.FeeOper.ID;
                }
                else
                {
                    row["收费员"] = empl.Name;
                }


                row["发药时间"] = obj.ExecOper.OperTime.Date == new DateTime(1, 1, 1).Date ? string.Empty : obj.ExecOper.OperTime.ToString();

                FS.HISFC.Models.Base.Employee confirmOper = new FS.HISFC.Models.Base.Employee();
                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //confirmOper = managerIntergrate.GetEmployeeInfo(obj.ExecOper.ID);
                confirmOper = employeeHelper.GetObjectFromID(obj.ExecOper.ID) as FS.HISFC.Models.Base.Employee;
                #endregion
                if (confirmOper == null || confirmOper.Name == string.Empty)
                {
                    row["发药员"] = obj.ExecOper.ID;
                }
                else
                {
                    row["发药员"] = confirmOper.Name;
                }

                //row["来源"] = obj.FTSource;

                dtDrugList.Rows.Add(row);
            }
            dvDrugListTemp = new DataView(dtDrugList);
            this.AddSumInfo(dtDrugList, "药品名称", "合计:", "金额", "自费", "公费", "自负", "优惠");
            DrugListred();
        }

        /// <summary>
        /// 查询患者非药品明细
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        private void QueryPatientUndrugList(DateTime beginTime, DateTime endTime)
        {
            ArrayList undrugList =new ArrayList();
            if (this.cmbHosDept2.Text == "全部")
            {
                undrugList = this.feeManager.QueryFeeItemLists(this.currentPatient.ID, beginTime, endTime);
            }
            else
            {
              //  undrugList = this.feeManager.QueryFeeItemListsByDept(this.currentPatient.ID, beginTime, endTime, this.cmbHosDept2.Tag.ToString());
            }
            if (undrugList == null)
            {
                MessageBox.Show(Language.Msg("获得患者非药品明细出错!") + this.feeManager.Err);

                return;
            }

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList obj in undrugList)
            {
                DataRow row = dtUndrugList.NewRow();

                row["项目名称"] = obj.Item.Name;
                row["单价"] = obj.Item.Price;
                row["数量"] = obj.Item.Qty;
                row["单位"] = obj.Item.PriceUnit;
                row["金额"] = obj.FT.TotCost;
                row["自费"] = obj.FT.OwnCost;
                row["公费"] = obj.FT.PubCost;
                row["自负"] = obj.FT.PayCost;
                row["优惠"] = obj.FT.RebateCost;
                row["收费时间"] = obj.FeeOper.OperTime;

                //收款员姓名
                FS.HISFC.BizProcess.Integrate.Manager managerIntergrate = new FS.HISFC.BizProcess.Integrate.Manager();
                FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //empl = managerIntergrate.GetEmployeeInfo(obj.FeeOper.ID); 
                empl = this.employeeHelper.GetObjectFromID(obj.FeeOper.ID) as FS.HISFC.Models.Base.Employee;
                #endregion

                if (empl == null || empl.Name == string.Empty)
                {
                    row["收费员"] = obj.FeeOper.ID;
                }
                else
                {
                    row["收费员"] = empl.Name;
                }

                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //row["执行科室"] = this.deptManager.GetDeptmentById(obj.ExecOper.Dept.ID).Name;
                //row["患者科室"] = this.deptManager.GetDeptmentById(((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID).Name; 
                row["执行科室"] = this.departmentHelper.GetName(obj.ExecOper.Dept.ID);
                row["患者科室"] = this.departmentHelper.GetName(((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID);
                #endregion
                //row["来源"] = obj.FTSource;

                dtUndrugList.Rows.Add(row);
            }
            dvUndrugListTemp = new DataView(dtUndrugList);
            this.AddSumInfo(dtUndrugList, "项目名称", "合计:", "金额", "自费", "公费", "自负", "优惠");
            UndrugListred();
        }

        /// <summary>
        /// 查询患者预交金信息
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        private void QueryPatientPrepayList(DateTime beginTime, DateTime endTime)
        {
            ArrayList prepayList = this.feeManager.QueryPrepays(this.currentPatient.ID);
            if (prepayList == null)
            {
                MessageBox.Show(Language.Msg("获得患者预交金明细出错!") + this.feeManager.Err);

                return;
            }

            foreach (FS.HISFC.Models.Fee.Inpatient.Prepay prepay in prepayList)
            {
                FS.FrameWork.Models.NeuObject Objprepay = new FS.FrameWork.Models.NeuObject();
                FS.HISFC.Models.Base.Employee employeeObj = new FS.HISFC.Models.Base.Employee();
                FS.HISFC.Models.Base.Department deptObj = new FS.HISFC.Models.Base.Department();
                DataRow row = dtPrepay.NewRow();

                row["票据号"] = prepay.RecipeNO;
                row["预交金额"] = prepay.FT.PrepayCost;
                row["支付方式"] = consManager.GetConstant("PAYMODES",prepay.PayType.ID).Name;
                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //   employeeObj = this.personManager.GetPersonByID(prepay.PrepayOper.ID);
                employeeObj = this.employeeHelper.GetObjectFromID(prepay.PrepayOper.ID) as FS.HISFC.Models.Base.Employee;
                #endregion
                row["操作员"] = employeeObj.Name;
                row["操作日期"] = prepay.PrepayOper.OperTime;
                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //deptObj = this.deptManager.GetDeptmentById(((FS.HISFC.Models.RADT.PatientInfo)prepay.Patient).PVisit.PatientLocation.Dept.ID);
                deptObj = this.departmentHelper.GetObjectFromID(((FS.HISFC.Models.RADT.PatientInfo)prepay.Patient).PVisit.PatientLocation.Dept.ID)
                    as FS.HISFC.Models.Base.Department;
                #endregion
                row["所在科室"] = deptObj.Name;
                string tempBalanceStatusName = string.Empty;
                switch (prepay.BalanceState)
                {
                    case "0":
                        tempBalanceStatusName = "未结算";
                        break;
                    case "1":
                        tempBalanceStatusName = "已结算";
                        break;
                    case "2":
                        tempBalanceStatusName = "已结转";
                        break;
                }
                row["结算状态"] = tempBalanceStatusName;
                string tempPrepayStateName = string.Empty;
                switch (prepay.PrepayState)
                {
                    case "0":
                        tempPrepayStateName = "收取";
                        break;
                    case "1":
                        tempPrepayStateName = "作废";
                        break;
                    case "2":
                        tempPrepayStateName = "补打";
                        break;
                }

                //row["来源"] = tempPrepayStateName;

                dtPrepay.Rows.Add(row);
            }

            this.AddSumInfo(dtPrepay, "票据号", "合计:", "预交金额");

            dvPrepay.Sort = "票据号 ASC";
        }

        /// <summary>
        /// 获得收费项目汇总
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        private void QueryPatientFeeclass(DateTime beginTime, DateTime endTime)
        {
            ArrayList feeclass = this.feeManager.QueryFeeItemListSum(this.currentPatient.ID, beginTime, endTime, "");
            if (feeclass == null)
            {
                MessageBox.Show(Language.Msg("获得患者收费项目出错!") + this.feeManager.Err);

                return;
            }
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList obj in feeclass)
            {
                DataRow row = dtFeeclass.NewRow();

                row["项目名称"] = obj.Item.Name;
                row["规格"] = obj.Item.Specs;
                row["单价"] = obj.Item.Price;
                row["数量"] = obj.Item.Qty;
                row["单位"] = obj.Item.PriceUnit;
                row["金额"] = obj.FT.TotCost;
                row["自费"] = obj.FT.OwnCost;
                row["公费"] = obj.FT.PubCost;
                row["自负"] = obj.FT.PayCost;
                row["优惠"] = obj.FT.RebateCost;




                //row["来源"] = obj.FTSource;

                dtFeeclass.Rows.Add(row);
            }

            this.AddSumInfo(dtFeeclass, "项目名称", "合计:", "金额", "自费", "公费", "自负", "优惠");
            deleteqltisnull();
        }



        /// <summary>
        /// 入出院诊断信息
        /// </summary>
        private void QueryPatientDiagnoseList()
        {
            ArrayList diagnoselist = this.diagnoseManager.QueryDiagnoseNoByPatientNo(this.currentPatient.ID);
            if (diagnoseManager == null)
            {
                MessageBox.Show(Language.Msg("获得患者收费项目出错!") + this.feeManager.Err);

                return;
            }
            foreach (FS.HISFC.Models.HealthRecord.Diagnose dg in diagnoselist)
            {
                DataRow row = dtDiagnoseList.NewRow();
                row["住院流水号"] = dg.DiagInfo.Patient.ID;
                row["发生序号"] = dg.DiagInfo.HappenNo;
                row["诊断ICD码"] = dg.DiagInfo.ICD10.ID;
                row["诊断名称"] = dg.DiagInfo.ICD10.Name;
                row["诊断日期"] = dg.DiagInfo.DiagDate;
                row["医师姓名"] = dg.DiagInfo.Doctor.Name;
                row["入院日期"] = dg.Pvisit.InTime;
                row["出院日期"] = dg.Pvisit.OutTime;
                row["操作员"] = dg.ID;
                row["操作时间"] = dg.OperInfo.OperTime;



                dtDiagnoseList.Rows.Add(row);
            }
        }

        /// <summary>
        /// 获得患者指定时间段内的最小费用汇总信息
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        private void QueryPatientFeeInfo(DateTime beginTime, DateTime endTime)
        {
            ArrayList feeInfoList = this.feeManager.QueryFeeInfosGroupByMinFeeByInpatientNO(this.currentPatient.ID, beginTime, endTime, "0");
            if (feeInfoList == null)
            {
                MessageBox.Show(Language.Msg("获得患者费用汇总明细出错!") + this.feeManager.Err);

                return;
            }
            this.alBalanceListHead.Clear();
            ArrayList alFeeState = new ArrayList();
            alFeeState = feeCodeStat.QueryFeeCodeStatByReportCode("ZY01");
            //feeInfoList.AddRange(feeInfoListBalanced);
            SortedList alBlance = new SortedList();
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo in feeInfoList)
            {

                DataRow row = dtFee.NewRow();
                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //row["费用名称"] = this.feeManager.GetComDictionaryNameByID("MINFEE", feeInfo.Item.MinFee.ID);

                row["费用名称"] = this.constantHelper.GetName(feeInfo.Item.MinFee.ID);
                #endregion
                row["金额"] = feeInfo.FT.TotCost;
                row["自费"] = feeInfo.FT.OwnCost;
                row["公费"] = feeInfo.FT.PubCost;
                row["自负"] = feeInfo.FT.PayCost;
                row["优惠金额"] = feeInfo.FT.RebateCost;
                string temp = string.Empty;

                //if (feeInfo.BalanceState == "0")
                //{
                //    temp = "未结算";
                //}
                //else
                //{
                //    temp = "已结算";
                //}
                row["结算状态"] = "未结算";
                row["费用代码"] = feeInfo.Item.MinFee.ID.ToString();

                dtFee.Rows.Add(row);
                #region 清单打印所需数据
                FS.HISFC.Models.Fee.Inpatient.BalanceList balanceListAdd = new FS.HISFC.Models.Fee.Inpatient.BalanceList();
                //实体赋值

                FS.FrameWork.Models.NeuObject objFeeStat = new FS.FrameWork.Models.NeuObject();
                objFeeStat = this.GetFeeStatByFeeCode(feeInfo.Item.MinFee.ID, alFeeState);
                if (objFeeStat == null)
                {
                    string feeName = "";
                    feeName = this.feeManager.GetMinFeeNameByCode(feeInfo.Item.MinFee.ID);
                    this.feeManager.Err = "请维护发票对照中最小费用为" + feeName + "的发票项目";
                    return;
                }

                balanceListAdd.FeeCodeStat.StatCate.ID = objFeeStat.ID;
                balanceListAdd.FeeCodeStat.StatCate.Name = objFeeStat.Name;

                //{467854CA-FAE4-4adf-9003-1E7B00F8456B} By JinHe
                balanceListAdd.FeeCodeStat.MinFee.ID = feeInfo.Item.MinFee.ID;

                balanceListAdd.FeeCodeStat.SortID = int.Parse(objFeeStat.Memo);


                balanceListAdd.BalanceBase.FT.TotCost = feeInfo.FT.TotCost;
                balanceListAdd.BalanceBase.FT.OwnCost = feeInfo.FT.OwnCost;
                balanceListAdd.BalanceBase.FT.PayCost = feeInfo.FT.PayCost;
                balanceListAdd.BalanceBase.FT.PubCost = feeInfo.FT.PubCost;
                //balanceListAdd.BalanceBase.Invoice.ID = invoiceNo;
                balanceListAdd.BalanceBase.Patient.IsBaby = this.currentPatient.IsBaby;
                //balanceListAdd.ID = balNo.ToString();
                //balanceListAdd.BalanceBase.ID = balNo.ToString();
                balanceListAdd.BalanceBase.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                balanceListAdd.BalanceBase.BalanceOper.ID = this.feeManager.Operator.ID;
                balanceListAdd.BalanceBase.BalanceOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.feeManager.GetSysDate());
                balanceListAdd.BalanceBase.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                //if (feeInfo.FT.TotCost != 0)
                //{
                //    alBlance.Add(objFeeStat.ID, balanceListAdd);
                //}
                this.alBalanceListHead.Add(balanceListAdd);
                #endregion
            }
            //if (alBlance.Count > 0)
            //{
            //    this.alBalanceListHead.Clear();
            //    foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList al in alBlance.Values)
            //    {
            //        this.alBalanceListHead.Add(al);
            //    }
            //    alBlance.Clear();
            //}
            ArrayList feeInfoListBalanced = this.feeManager.QueryFeeInfosGroupByMinFeeByInpatientNO(this.currentPatient.ID, beginTime, endTime, "1");
            if (feeInfoListBalanced == null)
            {
                MessageBox.Show(Language.Msg("获得患者费用汇总明细出错!") + this.feeManager.Err);

                return;
            }
    

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo in feeInfoListBalanced)
            {

                DataRow row = dtFee.NewRow();

                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //row["费用名称"] = this.feeManager.GetComDictionaryNameByID("MINFEE", feeInfo.Item.MinFee.ID);
                row["费用名称"] = this.constantHelper.GetName(feeInfo.Item.MinFee.ID);
                #endregion
                row["金额"] = feeInfo.FT.TotCost;
                row["自费"] = feeInfo.FT.OwnCost;
                row["公费"] = feeInfo.FT.PubCost;
                row["自负"] = feeInfo.FT.PayCost;
                row["优惠金额"] = feeInfo.FT.RebateCost;
                string temp = string.Empty;

                //if (feeInfo.BalanceState == "0")
                //{
                //    temp = "未结算";
                //}
                //else
                //{
                //    temp = "已结算";
                //}
                row["结算状态"] = "已结算";
                row["费用代码"] = feeInfo.Item.MinFee.ID.ToString();

                dtFee.Rows.Add(row);
                #region 清单打印所需数据
                FS.HISFC.Models.Fee.Inpatient.BalanceList balanceListAdd = new FS.HISFC.Models.Fee.Inpatient.BalanceList();
                //实体赋值

                FS.FrameWork.Models.NeuObject objFeeStat = new FS.FrameWork.Models.NeuObject();
                objFeeStat = this.GetFeeStatByFeeCode(feeInfo.Item.MinFee.ID, alFeeState);
                if (objFeeStat == null)
                {
                    string feeName = "";
                    feeName = this.feeManager.GetMinFeeNameByCode(feeInfo.Item.MinFee.ID);
                    this.feeManager.Err = "请维护发票对照中最小费用为" + feeName + "的发票项目";
                    return;
                }

                balanceListAdd.FeeCodeStat.StatCate.ID = objFeeStat.ID;
                balanceListAdd.FeeCodeStat.StatCate.Name = objFeeStat.Name;

                //{467854CA-FAE4-4adf-9003-1E7B00F8456B} By JinHe
                balanceListAdd.FeeCodeStat.MinFee.ID = feeInfo.Item.MinFee.ID;

                balanceListAdd.FeeCodeStat.SortID = int.Parse(objFeeStat.Memo);


                balanceListAdd.BalanceBase.FT.TotCost = feeInfo.FT.TotCost;
                balanceListAdd.BalanceBase.FT.OwnCost = feeInfo.FT.OwnCost;
                balanceListAdd.BalanceBase.FT.PayCost = feeInfo.FT.PayCost;
                balanceListAdd.BalanceBase.FT.PubCost = feeInfo.FT.PubCost;
                //balanceListAdd.BalanceBase.Invoice.ID = invoiceNo;
                balanceListAdd.BalanceBase.Patient.IsBaby = this.currentPatient.IsBaby;
                //balanceListAdd.ID = balNo.ToString();
                //balanceListAdd.BalanceBase.ID = balNo.ToString();
                balanceListAdd.BalanceBase.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                balanceListAdd.BalanceBase.BalanceOper.ID = this.feeManager.Operator.ID;
                balanceListAdd.BalanceBase.BalanceOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.feeManager.GetSysDate());
                balanceListAdd.BalanceBase.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                //if (feeInfo.FT.TotCost != 0)
                //{
                //    alBlance.Add(objFeeStat.ID, balanceListAdd);
                //}
                this.alBalanceListHead.Add(balanceListAdd);
                #endregion

            }
            this.AddSumInfo(dtFee, "费用名称", "合计:", "金额", "自费", "公费", "自负", "优惠金额");
        }

        /// <summary>
        /// 获得患者结算信息
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        private void QueryPatientBalance(DateTime beginTime, DateTime endTime)
        {
            ArrayList balanceList = this.feeManager.QueryBalancesByInpatientNO(this.currentPatient.ID);
            if (balanceList == null)
            {
                MessageBox.Show(Language.Msg("获得患者费用结算出错!") + this.feeManager.Err);

                return;
            }
            foreach (FS.HISFC.Models.Fee.Inpatient.Balance balance in balanceList)
            {
                FS.HISFC.Models.Base.Employee employeeObj = new FS.HISFC.Models.Base.Employee();
                string temp = "";
                DataRow row = dtBalance.NewRow();

                row["发票号码"] = balance.Invoice.ID;
                row["预交金额"] = balance.FT.PrepayCost;
                row["总金额"] = balance.FT.TotCost;
                row["自费"] = balance.FT.OwnCost;
                row["公费"] = balance.FT.PubCost;
                row["自负"] = balance.FT.PayCost;
                row["优惠"] = balance.FT.RebateCost;
                row["返还金额"] = balance.FT.ReturnCost;
                row["补收金额"] = balance.FT.SupplyCost;
                row["结算时间"] = balance.BalanceOper.OperTime;
                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //employeeObj = this.personManager.GetPersonByID(balance.BalanceOper.ID); 
                employeeObj = this.employeeHelper.GetObjectFromID(balance.BalanceOper.ID) as FS.HISFC.Models.Base.Employee;
                #endregion

                row["操作员"] = employeeObj.Name;
                row["结算类型"] = balance.BalanceType.Name;

                switch (balance.CancelType)
                {
                    case FS.HISFC.Models.Base.CancelTypes.Valid:
                        temp = "正常结算";
                        break;
                    case FS.HISFC.Models.Base.CancelTypes.LogOut:
                        temp = "结算召回";
                        break;
                    case FS.HISFC.Models.Base.CancelTypes.Reprint:
                        temp = "发票重打";
                        break;

                }
                row["结算状态"] = temp;

                dtBalance.Rows.Add(row);
            }

            AddSumInfo(dtBalance, "发票号码", "合计:", "总金额", "自费", "公费", "自负", "优惠", "返还金额", "补收金额");
        }

        /// <summary>
        /// 获得患者变更信息
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        private void QueryPatientChange(DateTime beginTime, DateTime endTime)
        {
            ArrayList changeList = this.radtManager.QueryPatientShiftInfoNew(this.currentPatient.ID);
            if (changeList == null)
            {
                MessageBox.Show(Language.Msg("获得患者变更信息出错!") + this.radtManager.Err);

                return;
            }
            foreach (FS.HISFC.Models.Invalid.CShiftData change in changeList)
            {
                FS.HISFC.Models.Base.Employee employeeObj = new FS.HISFC.Models.Base.Employee();
                DataRow row = this.dtChange.NewRow();
                row["序号"] = change.HappenNo.ToString();
                row["变更原因"] = change.ShitCause;
                row["原编码"] = change.OldDataCode;
                row["原名称"] = change.OldDataName;
                row["现编码"] = change.NewDataCode;
                row["现名称"] = change.NewDataName;
                row["备注"] = change.Mark;
                row["操作人编码"] = change.OperCode;
                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //employeeObj = this.personManager.GetPersonByID(change.OperCode);
                employeeObj = this.employeeHelper.GetObjectFromID(change.OperCode) as FS.HISFC.Models.Base.Employee;
                #endregion
                if (employeeObj == null)
                {//{9205BD4C-36B9-4b64-A875-E1969D52BFBE}
                    //找不到对应的医生名了
                    row["操作人姓名"] = change.OperCode;
                }
                else
                {
                    row["操作人姓名"] = employeeObj.Name;
                }
                row["操作时间"] = change.Memo;

                this.dtChange.Rows.Add(row);
            }
        }

        /// <summary>
        /// 添加合计
        /// </summary>
        /// <param name="table">当前DataTalbe</param>
        /// <param name="totName">合计的名称位置</param>
        /// <param name="disName">合剂的名称</param>
        /// <param name="sumColName">统计列的数组</param>
        public void AddSumInfo(DataTable table, string totName, string disName, params string[] sumColName)
        {
            DataRow sumRow = table.NewRow();

            sumRow[totName] = disName;

            foreach (string s in sumColName)
            {
                object sum = table.Compute("SUM(" + s + ")", "");
                sumRow[s] = sum;
            }

            table.Rows.Add(sumRow);
        }

        /// <summary>
        /// 根据输入的患者姓名查询患者基本信息
        /// </summary>
        private void QueryPatientByName()
        {
            if (this.txtName.Text.Trim() == string.Empty)
            {
                MessageBox.Show(Language.Msg("输入姓名不能为空!"));
                this.txtName.Focus();

                return;
            }

            string inputName = "%" + this.txtName.Text.Trim() + "%";
            //去掉特殊字符
            inputName = FS.FrameWork.Public.String.TakeOffSpecialChar(inputName, "'");
            //按照姓名直接查询患者想细信息
            string name = this.txtName.Text;
            ArrayList patientListTemp = this.radtManager.QueryPatientInfoByName(inputName);
            if (patientListTemp == null || patientListTemp.Count == 0)
            {
                MessageBox.Show(Language.Msg("无此患者信息!") + this.radtManager.Err);

                this.Clear();
                this.txtName.Text = name;
                return;
            }

            if (patientListTemp.Count > 0)
            {
                this.Clear();
                this.txtName.Text = name;
                this.QueryPatient(patientListTemp);
            }

            return;
        }

        /// <summary>
        /// 初始化DataTable
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        private int InitDataTable()
        {
            Type str = typeof(String);
            Type date = typeof(DateTime);
            Type dec = typeof(Decimal);
            Type bo = typeof(bool);

            #region 住院主表信息

            if (System.IO.File.Exists(pathNameMainInfo))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(pathNameMainInfo, dtMainInfo, ref dvMainInfo, this.fpMainInfo_Sheet1);

                dtMainInfo.PrimaryKey = new DataColumn[] { dtMainInfo.Columns["住院流水号"] };

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpMainInfo_Sheet1, pathNameMainInfo);

            }
            else
            {

                dtMainInfo.Columns.AddRange(new DataColumn[]{new DataColumn("住院流水号", str),
																new DataColumn("住院号", str),
																new DataColumn("姓名", str),
																new DataColumn("住院科室", str),
																new DataColumn("床号", str),
																new DataColumn("患者类别", str),
																new DataColumn("预交金(未结)", dec),
																new DataColumn("费用合计(未结)", dec),
																new DataColumn("余额", dec),
																new DataColumn("自费", dec),
																new DataColumn("自负", dec),
																new DataColumn("公费", dec),
																new DataColumn("入院日期", date),
																new DataColumn("在院状态", str),
																new DataColumn("出院日期", str),
																new DataColumn("预交金(已结)", dec),
																new DataColumn("费用合计(已结)", dec),
                                                                new DataColumn("住院类别", str),
																new DataColumn("结算日期", date),
                                                                new DataColumn("门诊诊断",str),
                                                                new DataColumn("备注",str)
																});

                dtMainInfo.PrimaryKey = new DataColumn[] { dtMainInfo.Columns["住院流水号"] };

                dvMainInfo = new DataView(dtMainInfo);

                this.fpMainInfo_Sheet1.DataSource = dvMainInfo;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpMainInfo_Sheet1, pathNameMainInfo);
            }

            #endregion

            #region 药品明细信息

            if (System.IO.File.Exists(pathNameDrugList))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(pathNameDrugList, dtDrugList, ref dvDrugList, this.fpDrugList_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpDrugList_Sheet1, pathNameDrugList);
            }
            else
            {
                dtDrugList.Columns.AddRange(new DataColumn[]{new DataColumn("药品名称", str),
																new DataColumn("规格", str),
																new DataColumn("单价", dec),
																new DataColumn("数量", dec),
																new DataColumn("付数", dec),
																new DataColumn("单位", str),
																new DataColumn("金额", dec),
																new DataColumn("自费", dec),
																new DataColumn("公费", dec),
																new DataColumn("自负", dec),
																new DataColumn("优惠", dec),
																new DataColumn("执行科室",str),
																new DataColumn("患者科室",str),
																new DataColumn("收费时间", str),
																new DataColumn("收费员", str),
																new DataColumn("发药时间", str),   
																new DataColumn("发药员", str),
				                                                new DataColumn("来源",str)});

                dvDrugList = new DataView(dtDrugList);

                this.fpDrugList_Sheet1.DataSource = dvDrugList;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpDrugList_Sheet1, pathNameDrugList);
            }

            #endregion

            #region 非药品明细信息
            if (System.IO.File.Exists(pathNameUndrugList))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(pathNameUndrugList, dtUndrugList, ref dvUndrugList, this.fpUndrugList_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpUndrugList_Sheet1, pathNameUndrugList);
            }
            else
            {
                dtUndrugList.Columns.AddRange(new DataColumn[]{new DataColumn("项目名称", str),
																  new DataColumn("单价", dec),
																  new DataColumn("数量", dec),
																  new DataColumn("单位", str),
																  new DataColumn("金额", dec),
																  new DataColumn("自费", dec),
																  new DataColumn("公费", dec),
																  new DataColumn("自负", dec),
																  new DataColumn("优惠", dec),
																  new DataColumn("执行科室", str),
																  new DataColumn("患者科室",str),
																  new DataColumn("收费时间", str),
																  new DataColumn("收费员", str),
				                                                  new DataColumn("来源", str)});

                dvUndrugList = new DataView(dtUndrugList);

                this.fpUndrugList_Sheet1.DataSource = dvUndrugList;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpUndrugList_Sheet1, pathNameUndrugList);
            }

            #endregion

            #region 收费项目汇总

            if (System.IO.File.Exists(pathNameFeeclass))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(pathNameFeeclass, dtFeeclass, ref dvFeeclass, this.fpFeeclass_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpFeeclass_Sheet1, pathNameFeeclass);
            }
            else
            {
                dtFeeclass.Columns.AddRange(new DataColumn[]{new DataColumn("项目名称", str),
                                                                new DataColumn("规格", str),
															    new DataColumn("单价", dec),
																new DataColumn("数量", dec),
																new DataColumn("单位", str),
																new DataColumn("金额", dec),
																new DataColumn("自费", dec),
																new DataColumn("公费", dec),
																new DataColumn("自负", dec),
																new DataColumn("优惠", dec),
																new DataColumn("执行科室",str),
																new DataColumn("患者科室",str),
																new DataColumn("收费员", str),
				                                                new DataColumn("来源",str)});

                dvFeeclass = new DataView(dtFeeclass);

                this.fpFeeclass_Sheet1.DataSource = dvFeeclass;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpFeeclass_Sheet1, pathNameFeeclass);
            }

            #endregion

            #region 入出院诊断信息
            //if (System.IO.File.Exists(pathNameDiagnoseList))
            //{
            //    FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(pathNameDiagnoseList, dtDiagnoseList, ref dvDiagnoseList, this.fpDiagnoseList_Sheet1);
            //    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.fpDiagnoseList_Sheet1, pathNameDiagnoseList);
            //}
            //else
            {
                dtDiagnoseList.Columns.AddRange(new DataColumn[]{new DataColumn("住院流水号",str),
                                                                new DataColumn("发生序号",str),
                                                                new DataColumn("诊断ICD码",str),
                                                                new DataColumn("诊断名称",str),
                                                                new DataColumn("诊断日期",date),
                                                                new DataColumn("医师姓名",str),
                                                                new DataColumn("入院日期",date),
                                                                new DataColumn("出院日期",date),
                                                                new DataColumn("操作员",str),
                                                                new DataColumn("操作时间",str)});
                dvDiagnoseList = new DataView(dtDiagnoseList);
                this.fpDiagnoseList_Sheet1.DataSource = dvDiagnoseList;
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.fpDiagnoseList_Sheet1, pathNameDiagnoseList);
            }
            #endregion

            #region 预交金信息

            if (System.IO.File.Exists(pathNamePrepay))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(pathNamePrepay, dtPrepay, ref dvPrepay, this.fpPrepay_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpPrepay_Sheet1, pathNamePrepay);
            }
            else
            {
                dtPrepay.Columns.AddRange(new DataColumn[]{new DataColumn("票据号", str),
															  new DataColumn("预交金额", dec),
															  new DataColumn("支付方式", str),
															  new DataColumn("操作员", str),
															  new DataColumn("操作日期", date),
															  new DataColumn("所在科室", str),
															  new DataColumn("结算状态", str),
															  new DataColumn("来源", str)});

                dvPrepay = new DataView(dtPrepay);

                this.fpPrepay_Sheet1.DataSource = dvPrepay;
                dvPrepay.Sort = "票据号 ASC";

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpPrepay_Sheet1, pathNamePrepay);
            }

            #endregion

            #region 最小费用信息

            if (System.IO.File.Exists(pathNameFee))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(pathNameFee, dtFee, ref dvFee, this.fpFee_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpFee_Sheet1, pathNameFee);
            }
            else
            {
                dtFee.Columns.AddRange(new DataColumn[]{new DataColumn("费用名称", str),
														   new DataColumn("金额", dec),
														   new DataColumn("自费", dec),
														   new DataColumn("公费", dec),
														   new DataColumn("自负", dec),
														   new DataColumn("优惠金额", dec),
														   new DataColumn("结算状态", str),
                                                           new DataColumn("费用代码", str)});

                dvFee = new DataView(dtFee);

                this.fpFee_Sheet1.DataSource = dvFee;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpFee_Sheet1, pathNameFee);
            }

            #endregion

            #region 结算信息
            if (System.IO.File.Exists(pathNameBalance))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(pathNameBalance, dtBalance, ref dvBalance, this.fpBalance_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpBalance_Sheet1, pathNameBalance);
            }
            else
            {
                dtBalance.Columns.AddRange(new DataColumn[]{new DataColumn("发票号码", str),
															   new DataColumn("结算类型", str),
															   new DataColumn("结算状态", str),
															   new DataColumn("预交金额", dec),
															   new DataColumn("总金额", dec),
															   new DataColumn("自费", dec),
															   new DataColumn("公费", dec),
															   new DataColumn("自负", dec),
															   new DataColumn("优惠", dec),
															   new DataColumn("返还金额", dec),
															   new DataColumn("补收金额", dec),
															   new DataColumn("结算时间", date),
															   new DataColumn("操作员", str)});

                dvBalance = new DataView(dtBalance);

                this.fpBalance_Sheet1.DataSource = dvBalance;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpBalance_Sheet1, pathNameBalance);
            }
            #endregion

            #region 变更信息
            if (System.IO.File.Exists(this.pathNameChange))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(this.pathNameChange, dtChange, ref dvChange, this.fpChange_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpChange_Sheet1, pathNameChange);
            }
            else
            {
                this.dtChange.Columns.AddRange(new DataColumn[]{new DataColumn("序号", str),
															   new DataColumn("变更原因", str),
															   new DataColumn("原编码", str),
															   new DataColumn("原名称", str),
															   new DataColumn("现编码", str),
															   new DataColumn("现名称", str),
															   new DataColumn("备注", str),
															   new DataColumn("操作人编码", str),
															   new DataColumn("操作人姓名", str),
															   new DataColumn("操作时间", str)});

                this.dvChange = new DataView(dtChange);

                this.fpChange_Sheet1.DataSource = dvChange;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpChange_Sheet1, pathNameChange);
            }
            #endregion

            return 1;
        }

        private void deleteqltisnull()
        {
            for (int i = 0; fpFeeclass.Sheets[0].Cells[i, 3].Text.Length != 0; i++)
            {
                string str = string.Empty;
                str = fpFeeclass.ActiveSheet.Cells[i, 3].Text;
                double num = double.Parse(str);
                if (num <= 0)
                {
                    this.fpFeeclass.ActiveSheet.Rows.Remove(i, 1);
                    i--;
                }
            }
        }

        /// <summary>
        /// 药品退费用红色字体标记
        /// </summary>
        private void DrugListred()
        {


            //for (int i = 0; fpDrugList.Sheets[0].Cells[i, 4].Text.Length != 0; i++)
            //{
            //    string str = string.Empty;
            //    str = fpDrugList.ActiveSheet.Cells[i, 4].Text;
            //    double num = double.Parse(str);
            //    if (num <= 0)
            //    {
            //        fpDrugList.Sheets[0].Rows[i].ForeColor = Color.Red;
            //    }
            //}
        }
        /// <summary>
        /// 非药品退费红色字体标记
        /// </summary>
        private void UndrugListred()
        {

            //for (int i = 0; fpUndrugList.Sheets[0].Cells[i, 3].Text.Length != 0; i++)
            //{
            //    string str = string.Empty;
            //    str = fpUndrugList.ActiveSheet.Cells[i, 3].Text;
            //    double num = double.Parse(str);
            //    if (num <= 0)
            //    {
            //        fpUndrugList.Sheets[0].Rows[i].ForeColor = Color.Red;
            //    }
            //}
        }


        /// <summary>
        /// 显示患者基本信息
        /// </summary>
        protected void SetPatientInfo()
        {
            if (this.currentPatient == null || this.currentPatient.ID == null || this.currentPatient.ID == string.Empty)
            {
                return;
            }

            this.lblID.Text = this.currentPatient.PID.PatientNO;//住院号
            this.lblName.Text = this.currentPatient.Name;//姓名;
            this.lblSex.Text = this.currentPatient.Sex.Name;
            this.lblAge.Text = this.currentPatient.Age;
            this.lblBed.Text = this.currentPatient.PVisit.PatientLocation.Bed.ID;
            this.lblDept.Text = this.currentPatient.PVisit.PatientLocation.Dept.Name;
            this.lblPact.Text = this.currentPatient.Pact.Name;//合同单位
            this.lblDateIn.Text = this.currentPatient.PVisit.InTime.ToShortDateString();//住院日期
            if (this.currentPatient.PVisit.OutTime != DateTime.MinValue&&this.currentPatient.PVisit.OutTime!=new DateTime(0002,1,1))
            {
                this.lblOutDate.Text = this.currentPatient.PVisit.OutTime.ToShortDateString();
            }
            this.lblInState.Text = this.currentPatient.PVisit.InState.Name;//在院状态
            decimal TotCost = this.currentPatient.FT.TotCost + this.currentPatient.FT.BalancedCost;
            //this.lblTotCost.Text = this.currentPatient.FT.TotCost.ToString();
            this.lblTotCost.Text = TotCost.ToString();

            this.lblOwnCost.Text = this.currentPatient.FT.OwnCost.ToString();
            this.lblPubCost.Text = this.currentPatient.FT.PubCost.ToString();
            this.lblPrepayCost.Text = this.currentPatient.FT.PrepayCost.ToString();
            this.lblUnBalanceCost.Text = this.currentPatient.FT.TotCost.ToString();
            this.lblBalancedCost.Text = this.currentPatient.FT.BalancedCost.ToString();
            this.lblDiagnose.Text = this.currentPatient.MainDiagnose;
            this.lblMemo.Text = this.currentPatient.Memo;
            this.lblfreecost.Text = this.currentPatient.FT.LeftCost.ToString();
            if (this.currentPatient.CompanyName != null)
            {
                this.lblwork.Text = this.currentPatient.CompanyName.ToString();
            }
            else
            {
                this.lblwork.Text = "";
            }
            if (this.currentPatient.AddressHome != null)
            {
                this.lblhome.Text = this.currentPatient.AddressHome.ToString();
            }
            else
            {
                this.lblhome.Text = "";
            }
            if (this.currentPatient.PhoneHome != null)
            {
                this.lblTel.Text = this.currentPatient.PhoneHome.ToString();
            }
            else if (this.currentPatient.PhoneBusiness != null)
            {
                this.lblTel.Text = this.currentPatient.PhoneBusiness.ToString();
            }
            else
            {
                this.lblTel.Text = "";
            }

        }

        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        protected void QueryAllInfomaition(DateTime beginTime, DateTime endTime)
        {
            this.QueryPatientDrugList(beginTime, endTime);

            this.QueryPatientUndrugList(beginTime, endTime);

            this.QueryPatientPrepayList(beginTime, endTime);

            this.QueryPatientFeeInfo(beginTime, endTime);

            this.QueryPatientBalance(beginTime, endTime);

            this.QueryPatientChange(beginTime, endTime);

            this.QueryPatientFeeclass(beginTime, endTime);
            this.QueryPatientDiagnoseList();
            string err = string.Empty;
            this.QueryFeeItemlist(this.currentPatient, beginTime, endTime, ref err);
        }

        /// <summary>
        /// 返回"_"
        /// </summary>
        /// <param name="langth">长度</param>
        /// <returns>成功 "---" 失败 null</returns>
        private string RetrunSplit(int langth)
        {
            string s = string.Empty;

            for (int i = 0; i < langth; i++)
            {
                s += "_";
            }

            return s;
        }

        /// <summary>
        /// 清空
        /// </summary>
        private void Clear()
        {
            this.lblID.Text = this.RetrunSplit(10);
            this.lblName.Text = this.RetrunSplit(5);
            this.lblSex.Text = this.RetrunSplit(4);
            this.lblAge.Text = this.RetrunSplit(4);
            this.lblBed.Text = this.RetrunSplit(10);
            this.lblDept.Text = this.RetrunSplit(10);
            this.lblPact.Text = this.RetrunSplit(10);
            this.lblDateIn.Text = this.RetrunSplit(10);
            this.lblOutDate.Text = this.RetrunSplit(10);

            this.lblInState.Text = this.RetrunSplit(6);
            this.lblTotCost.Text = this.RetrunSplit(10);
            this.lblOwnCost.Text = this.RetrunSplit(10);
            this.lblPubCost.Text = this.RetrunSplit(10);
            this.lblPrepayCost.Text = this.RetrunSplit(10);
            this.lblUnBalanceCost.Text = this.RetrunSplit(10);
            this.lblBalancedCost.Text = this.RetrunSplit(10);
            this.lblDiagnose.Text = this.RetrunSplit(20);
            this.lblMemo.Text = this.RetrunSplit(30);
            this.txtName.Text = string.Empty;
            this.lblfreecost.Text = string.Empty;
            this.lblwork.Text = string.Empty;
            this.lblTel.Text = string.Empty;
            this.lblhome.Text = string.Empty;
            dtMainInfo.Rows.Clear();
            dtDrugList.Rows.Clear();
            dtUndrugList.Rows.Clear();
            dtPrepay.Rows.Clear();
            dtFee.Rows.Clear();
            dtBalance.Rows.Clear();
            dtChange.Rows.Clear();
            dtFeeclass.Rows.Clear();
            dtDiagnoseList.Rows.Clear();
        }
        private void InitContr()
        {
            this.neuLabel21.Visible = true;
            this.lblOwnCost.Visible = true;
            this.neuLabel23.Visible = true;
            this.lblPubCost.Visible = true;
        }

        /// <summary>
        /// 出院费用清单打印
        /// </summary>
        /// <param name="interfaceIndex">接口序号，默认0明细清单，1汇总清单，2医嘱清单,4医保结算单</param>
        /// <returns></returns>
        protected int BlanceListPrint(string interfaceIndex)
        {
           // FS.HISFC.BizProcess.Integrate.FeeInterface..FeeInterface.IBalanceListPrint p = null;
            FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy pSi = null;
            DateTime beginTime ;
            DateTime endTime ;
            if (Timefalg == true || this.isAll)
            {
                 beginTime = this.currentPatient.PVisit.InTime;
                 endTime = this.radtManager.GetDateTimeFromSysDateTime();
            }
            else
            {
                 currentPatient.User01 = "T";
                 currentPatient.User02 = BeginTime.ToShortDateString()+"  至  " + EndTime.ToShortDateString();
                 beginTime = this.BeginTime;
                 endTime = this.EndTime;
            }            
            string err = "";
            this.QueryFeeItemlist(this.currentPatient, beginTime, endTime, ref err);


            long returnValue = 0;
            if (interfaceIndex == "4"&& currentPatient.Pact.ID =="2" && !currentPatient.PVisit.InState.ID.Equals("O"))
            {

                returnValue = this.medcareInterfaceProxy.SetPactCode(this.currentPatient.Pact.ID);
                if (returnValue != 1)
                {
                    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                returnValue = this.medcareInterfaceProxy.Connect();
                if (returnValue != 1)
                {
                    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }

                //{BF6500FD-71FE-4cce-B328-D10CB7CBF22B}添加读卡 注意：主要为了读取医保串，PreBalanceInpatient预结方法PreBalanceInpatient中应判断patient.SIMainInfo.Memo是否为空
                //如果为空从本地取
                //returnValue = this.medcareInterfaceProxy.GetRegInfoInpatient(this.currentPatient);
                //if (returnValue != 1)
                //{
                //    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                //    return -1;
                //}

                this.currentPatient.SIMainInfo.SiEmployeeCode = "H0510";

                returnValue = this.medcareInterfaceProxy.PreBalanceInpatient(this.currentPatient, ref alFeeItemLists);
                if (returnValue != 1)
                {
                    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }

                returnValue = this.medcareInterfaceProxy.Disconnect();
                if (returnValue != 1)
                {
                    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }
            }
            else if (interfaceIndex == "4" && currentPatient.Pact.ID == "1")
            {
                MessageBox.Show("不是医保病人,不能补打医保结算单");
                return -1;
            
            }


            if (this.alFeeItemLists.Count > 0)
            {
                int balanceNO = 0;
                string invoiceNO = "";
                DateTime dtSys = DateTime.MinValue;
                dtSys = this.feeManager.GetDateTimeFromSysDateTime();
                //初始化Balancelist数组以便形成票面信息
                if (interfaceIndex == "4")
                {
                    pSi = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy), FS.FrameWork.Function.NConvert.ToInt32(interfaceIndex)) as FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy;
                    pSi.PatientInfo = this.currentPatient;
                    if (this.alBalanceListHead.Count <= 0)
                        return 0;


                    if (pSi.SetValueForPrint(this.currentPatient, null, this.alBalanceListHead, this.alBalancePay) == -1)
                    {
                        this.alBalanceListHead = new ArrayList();
                        this.alBalancePay = new ArrayList();
                        return -1;
                    }

                    //调打印类
                    pSi.PrintPreview();

                }
                else
                {
                    //p = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceListPrint), FS.FrameWork.Function.NConvert.ToInt32(interfaceIndex)) as FS.HISFC.Integrate.FeeInterface.IBalanceListPrint;

                    //p.PatientInfo = this.currentPatient;
                    //p.BloodMinFeeCode = this.bloodMinFeeCode;

                    ////无费用信息
                    //if (this.alBalanceListHead.Count <= 0)
                    //    return 0;

                    //if (p.SetValueForPreview(this.currentPatient, null, this.alBalanceListHead, alFeeItemLists, null) == -1)
                    //{
                    //    this.alBalanceListHead = new ArrayList();
                    //    return -1;
                    //}
                    ////调打印


                    //p.PrintPreview();
                }
               

                return 1;

            }
            return -1;
        }

        /// <summary>
        /// 获取患者本次结算信息
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="fromDate"></param>
        /// <param name="ToDate"></param>
        /// <returns></returns>
        protected virtual int QueryFeeItemlist(FS.HISFC.Models.RADT.PatientInfo currentPatient, DateTime fromDate, DateTime ToDate, ref string errText)
        {
            //非药品明细
            ArrayList alItemList = new ArrayList();

            //药品明细
            ArrayList alMedicineList = new ArrayList();

            if (currentPatient.PVisit.InState.ID.Equals("O"))//有结束日期
            {
                //查询
               // alItemList = this.feeManager.QueryItemListstoBalance(currentPatient.ID, fromDate, ToDate);
                if (alItemList == null)
                {
                    errText = "查询患者非药品信息出错" + this.feeManager.Err;
                    return -1;
                }

              //  alMedicineList = this.feeManager.QueryMedicineListstoBalance(currentPatient.ID, fromDate, ToDate);
                if (alMedicineList == null)
                {
                    errText = "查询患者药品信息出错" + this.feeManager.Err;
                    return -1;
                }

            }
            else//其他结算
            {

                if (Timefalg == false)
                {
                    
                    //查询
                    //if (this.isAll)
                    ////    alItemList = this.feeManager.QueryItemListsForBalanceByInvoiceNo(currentPatient.ID, "", "ALL");
                    //else if (this.isInvoiceNo)
                    ////    alItemList = this.feeManager.QueryItemListsForBalanceByInvoiceNo(currentPatient.ID, this.invoiceNo, "NO");
                    //else
                    //    alItemList = this.feeManager.QueryItemListsForBalance(currentPatient.ID, fromDate, ToDate);
                    if (alItemList == null)
                    {
                        errText = "查询患者非药品信息出错" + this.feeManager.Err;
                        return -1;
                    }

                    //if (this.isAll)
                    //    alMedicineList = this.feeManager.QueryMedicineListsForBalance(currentPatient.ID, "", "ALL");
                    //else if (this.isInvoiceNo)
                    //    alMedicineList = this.feeManager.QueryMedicineListsForBalance(currentPatient.ID, this.invoiceNo, "NO");
                    //else
                        alMedicineList = this.feeManager.QueryMedicineListsForBalance(currentPatient.ID, fromDate, ToDate);
                    if (alMedicineList == null)
                    {
                        errText = "查询患者药品信息出错" + this.feeManager.Err;
                        return -1;
                    }
                }
                else
                {
                    //查询
                    alItemList = this.feeManager.QueryItemListsForBalance(currentPatient.ID);
                    if (alItemList == null)
                    {
                        errText = "查询患者非药品信息出错" + this.feeManager.Err;
                        return -1;
                    }

                    alMedicineList = this.feeManager.QueryMedicineListsForBalance(currentPatient.ID);
                    if (alMedicineList == null)
                    {
                        errText = "查询患者药品信息出错" + this.feeManager.Err;
                        return -1;
                    }
                }
            }
            this.alFeeItemLists = new ArrayList();
            alFeeItemLists.Clear();
            //添加汇总信息
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList item in alItemList)
            {
                this.alFeeItemLists.Add(item);
            }

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList medicineList in alMedicineList)
            {
                this.alFeeItemLists.Add(medicineList);
            }

            return 1;

        }

        /// <summary>
        /// 通过最小费用获取统计大类memo存打印顺序
        /// </summary>
        /// <param name="feeCode"></param>
        /// <param name="alInvoice"></param>
        /// <returns></returns>
        protected FS.FrameWork.Models.NeuObject GetFeeStatByFeeCode(string feeCode, ArrayList al)
        {
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            FS.HISFC.Models.Fee.FeeCodeStat feeStat;

            for (int i = 0; i < al.Count; i++)
            {
                feeStat = (FS.HISFC.Models.Fee.FeeCodeStat)al[i];
                if (feeStat.MinFee.ID == feeCode)
                {
                    obj.ID = feeStat.StatCate.ID;

                    obj.Name = feeStat.StatCate.Name;
                    obj.Memo = feeStat.SortID.ToString().PadLeft(2,'0');
                    return obj;
                }
            }
            return null;
        }
        #endregion

        private void ucQueryInpatientNo1_myEvent()
        {
            this.QueryPatientByInpatientNO();
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            if (this.neuTabControl1.SelectedIndex == 9)
            {
                //FS.Report.InpatientFee.ucFinIpbPatientDayFee ucPatientDF = this.tpDayFee.Controls[0] as FS.Report.InpatientFee.ucFinIpbPatientDayFee;
                //ucPatientDF.PrintDW();
            }
            else
            {
                print.PrintPage(0, 0, this.neuTabControl1.SelectedTab);
            }

            return base.OnPrint(sender, neuObject);
        }
        //打印预览
        public override int PrintPreview(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print printview = new FS.FrameWork.WinForms.Classes.Print();

            printview.PrintPreview(0, 0, this.neuTabControl1.SelectedTab);
            return base.OnPrintPreview(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            object obj = this.hashTableFp[this.neuTabControl1.SelectedTab];

            FarPoint.Win.Spread.FpSpread fp = obj as FarPoint.Win.Spread.FpSpread;

            SaveFileDialog op = new SaveFileDialog();

            op.Title = "请选择保存的路径和名称";
            op.CheckFileExists = false;
            op.CheckPathExists = true;
            op.DefaultExt = "*.xls";
            op.Filter = "(*.xls)|*.xls";

            DialogResult result = op.ShowDialog();

            if (result == DialogResult.Cancel || op.FileName == string.Empty)
            {
                return -1;
            }

            string filePath = op.FileName;

            bool returnValue = fp.SaveExcel(filePath, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);


            return base.Export(sender, neuObject);
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("打印日期清单", "设置检索日期", FS.FrameWork.WinForms.Classes.EnumImageList.C查询历史, true, false, null);
            this.toolBarService.AddToolButton("补打汇总清单", "汇总清单", FS.FrameWork.WinForms.Classes.EnumImageList.X信息, true, false, null);
            this.toolBarService.AddToolButton("补打明细清单", "明细清单", FS.FrameWork.WinForms.Classes.EnumImageList.M明细, true, false, null);
            this.toolBarService.AddToolButton("医嘱清单", "医嘱清单", FS.FrameWork.WinForms.Classes.EnumImageList.Y医嘱, true, false, null);
            this.toolBarService.AddToolButton("补打医保清单", "医保结算清单", FS.FrameWork.WinForms.Classes.EnumImageList.Y医保, true, false, null);
            this.toolBarService.AddToolButton("取消上传", "取消医保上传", FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);
            this.toolBarService.AddToolButton("取消预结算", "取消预结算", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            this.toolBarService.AddToolButton("取消医保结算", "取消医保结算", FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "打印日期清单")
            {
                //选择时间段，如果没有选择就返回
                //if (FS.FrameWork.WinForms.Classes.Function.ChooseDate(ref this.BeginTime, ref this.EndTime) == 0)
                //    return;

                if (this.currentPatient == null || string.IsNullOrEmpty(this.currentPatient.ID)) return;

                FS.FrameWork.WinForms.Forms.frmChooseDate frmDate = new FS.FrameWork.WinForms.Forms.frmChooseDate();
                if (frmDate.ShowDialog() == DialogResult.OK)
                {
                    this.BeginTime = frmDate.DateBegin;
                    this.EndTime = frmDate.DateEnd;
                    //this.isAll = frmDate;
                    //this.isInvoiceNo = frmDate.IsInvoiceNo;
                    //this.invoiceNo = frmDate.InvoiceNo;
                    Timefalg = false;
                    this.BlanceListPrint("0");
                }
                if (!frmDate.IsDisposed)
                    frmDate.Dispose();

                this.isAll = false;
                this.isInvoiceNo = false;
                this.invoiceNo = "";
            }

            if (e.ClickedItem.Text == "补打汇总清单")
            {
                Timefalg = true;
                this.BlanceListPrint("1");
            }
            else if (e.ClickedItem.Text == "补打明细清单")
            {
                Timefalg = true;
                this.BlanceListPrint("0");
            }
            else if (e.ClickedItem.Text == "医嘱清单")
            {
                Timefalg = true;
                this.BlanceListPrint("2");
            }
            else if (e.ClickedItem.Text == "补打医保清单")
            {
                Timefalg = true;
                this.BlanceListPrint("4");
            }
            if (e.ClickedItem.Text == "取消上传")
            {
                CancelUploadFee();
            }
            if (e.ClickedItem.Text == "取消预结算")
            {
                CancelPreBalance();
            
            }
            if (e.ClickedItem.Text == "取消医保结算")
            {
                CancelBalance(); 
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            //认为输入住院号查询
            if (this.ucQueryInpatientNo1.Text.Length > 0)
            {
                this.ucQueryInpatientNo1_myEvent();
            }
            else//认为输入姓名查询
            {
                this.QueryPatientByName();
            }
        }

        /// <summary>
        /// 取消结算
        /// </summary>
        /// <returns></returns>
        private int CancelBalance()
        {
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo.Trim() == "")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("住院号错误，没有找到该患者", 111);
                this.ucQueryInpatientNo1.Focus();
                return -1;
            }

            #region 医保结算



            long returnValue = 0;

            returnValue = this.medcareInterfaceProxy.SetPactCode(this.currentPatient.Pact.ID);
            if (returnValue != 1)
            {

                // errText = this.medcareInterfaceProxy.ErrMsg;
                return -1;

            }
            this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            returnValue = this.medcareInterfaceProxy.Connect();
            if (returnValue != 1)
            {
                return -1;
            }

            currentPatient.SIMainInfo.User03 = "T"; //取消预结算
            //取消医保上传费用
            returnValue = this.medcareInterfaceProxy.CancelBalanceInpatient(currentPatient, ref alFeeItemLists);
            if (returnValue != 1)
            {
                return -1;
            }

            returnValue = this.medcareInterfaceProxy.Disconnect();
            if (returnValue != 1)
            {
                return -1;
            }
            #endregion

            MessageBox.Show("取消上预结算成功,请重新上传费用!");

            return 1;


        }

        private int CancelPreBalance()
        {
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo.Trim() == "")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("住院号错误，没有找到该患者", 111);
                this.ucQueryInpatientNo1.Focus();
                return -1;
            }


            #region 医保结算



            long returnValue = 0;

            returnValue = this.medcareInterfaceProxy.SetPactCode(this.currentPatient.Pact.ID);
            if (returnValue != 1)
            {

                // errText = this.medcareInterfaceProxy.ErrMsg;
                return -1;

            }
            this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            returnValue = this.medcareInterfaceProxy.Connect();
            if (returnValue != 1)
            {
                return -1;
            }

            currentPatient.SIMainInfo.User03 = "T"; //取消预结算
            //取消医保上传费用
            returnValue = this.medcareInterfaceProxy.PreBalanceInpatient(currentPatient,ref alFeeItemLists);
            if (returnValue != 1)
            {
                return -1;
            }

            returnValue = this.medcareInterfaceProxy.Disconnect();
            if (returnValue != 1)
            {
                return -1;
            }
            #endregion

            MessageBox.Show("取消上预结算成功,请重新上传费用!");

            return 1;
        
        }
        /// <summary>
        /// 重新上传医保费用
        /// </summary>
        /// <returns></returns>
        protected int CancelUploadFee()
        {

            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo.Trim() == "")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("住院号错误，没有找到该患者", 111);
                this.ucQueryInpatientNo1.Focus();
                return -1;
            }

            #region 医保结算



            long returnValue = 0;

            returnValue = this.medcareInterfaceProxy.SetPactCode(this.currentPatient.Pact.ID);
            if (returnValue != 1)
            {

                // errText = this.medcareInterfaceProxy.ErrMsg;
                return -1;

            }
            this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            returnValue = this.medcareInterfaceProxy.Connect();
            if (returnValue != 1)
            {
                return -1;
            }

            //取消医保上传费用
            returnValue = this.medcareInterfaceProxy.DeleteUploadedFeeDetailsAllInpatient(currentPatient);
            if (returnValue != 1)
            {
                return -1;
            }

            returnValue = this.medcareInterfaceProxy.Disconnect();
            if (returnValue != 1)
            {
                return -1;
            }
            #endregion

            MessageBox.Show("取消上传成功,请重新上传费用!");

            return 1;



        }


        private void neuButton1_Click(object sender, EventArgs e)
        {
            string inState = string.Empty;//入院状态
            string deptCode = string.Empty;//住院科室编码
            string pactCode = string.Empty;//合同单位编码
            DateTime beginTime = this.dtpBeginTime.Value;
            DateTime endTime = this.dtpEndTime.Value;

            if (this.cmbDept.Text == string.Empty || this.cmbDept.Text == "全部")
            {
                deptCode = "%";

            }
            else
            {
                deptCode = this.cmbDept.Tag.ToString();
            }
            if (this.cmbPact.Text == string.Empty || this.cmbPact.Text == "全部")
            {
                pactCode = "%";
            }
            else
            {
                pactCode = this.cmbPact.Tag.ToString();
            }
            if (this.cmbPatientState.Text == string.Empty || this.cmbPatientState.Text == "全部")
            {
                inState = "%";
            }
            else
            {
                inState = this.cmbPatientState.Tag.ToString();
            }
            Cursor.Current = Cursors.WaitCursor;

            if (inState.Equals("O"))  //住院已结算病人
            {
                //ArrayList patientListTemp = radtManager.QueryPatientByConditonsOutpatient(pactCode, deptCode, inState, beginTime, endTime);
                //if (patientListTemp == null)
                //{
                //    MessageBox.Show(Language.Msg("或者患者基本信息出错!") + this.radtManager.Err);
                //    Cursor.Current = Cursors.Arrow;
                //    return;
                //}
              
                //Cursor.Current = Cursors.Arrow;
                //this.QueryPatient(patientListTemp);
         
            }

            else
            {
                ArrayList patientListTemp = radtManager.QueryPatientByConditons(pactCode, deptCode, inState, beginTime, endTime);
                if (patientListTemp == null)
                {
                    MessageBox.Show(Language.Msg("或者患者基本信息出错!") + this.radtManager.Err);
                    Cursor.Current = Cursors.Arrow;
                    return;
                }
                Cursor.Current = Cursors.Arrow;
                this.QueryPatient(patientListTemp);
             }
        }

        private void ucPatientFeeQuery_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void fpMainInfo_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.fpMainInfo_Sheet1.RowCount <= 0)
            {
                return;
            }
            string inpatientNO = this.fpMainInfo_Sheet1.Cells[e.Row, 0].Text;

            if (inpatientNO == null)
            {
                return;
            }

            this.currentPatient = this.radtManager.QueryPatientInfoByInpatientNO(inpatientNO);
            if (this.currentPatient == null || this.currentPatient.ID == null || this.currentPatient.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("查询患者基本信息出错!") + this.radtManager.Err);

                return;
            }

            this.SetPatientInfo();

            dtDrugList.Rows.Clear();
            dtUndrugList.Rows.Clear();
            dtPrepay.Rows.Clear();
            dtFee.Rows.Clear();
            dtBalance.Rows.Clear();
            dtChange.Rows.Clear();
            dtFeeclass.Rows.Clear();
            dtDiagnoseList.Rows.Clear();
            //设置查询时间
            DateTime beginTime = this.currentPatient.PVisit.InTime;
            DateTime endTime = this.radtManager.GetDateTimeFromSysDateTime();

            this.QueryAllInfomaition(beginTime, endTime);

        }

        private void txtNameKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.QueryPatientByName();
            }

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.queryInfo();
            return 1;
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void queryInfo()
        {
            if (this.currentPatient != null)
            {
                if (this.currentPatient.ID != "")
                {
                    if (this.neuTabControl1.SelectedIndex == 2)
                    {
                        DateTime detailbeginTime = new DateTime(this.neuDateTimePicker1.Value.Year, this.neuDateTimePicker1.Value.Month, this.neuDateTimePicker1.Value.Day, 0, 0, 0);
                        DateTime detailendTime = new DateTime(this.neuDateTimePicker2.Value.Year, this.neuDateTimePicker2.Value.Month, this.neuDateTimePicker2.Value.Day, 23, 59, 59);
                        if (detailbeginTime > detailendTime)
                        {
                            MessageBox.Show("开始日期不能大于结束日期,请修改!", "提示");
                            return;
                        }
                        dtDrugList.Rows.Clear();
                        this.QueryPatientDrugList(detailbeginTime, detailendTime);
                        this.fpDrugList_Sheet1.DataSource = dvDrugListTemp;
                    }
                    else if (this.neuTabControl1.SelectedIndex == 3)
                    {
                        DateTime detailbeginTime = new DateTime(this.neuDateTimePicker4.Value.Year, this.neuDateTimePicker4.Value.Month, this.neuDateTimePicker4.Value.Day, 0, 0, 0);
                        DateTime detailendTime = new DateTime(this.neuDateTimePicker3.Value.Year, this.neuDateTimePicker3.Value.Month, this.neuDateTimePicker3.Value.Day, 23, 59, 59);
                        if (detailbeginTime > detailendTime)
                        {
                            MessageBox.Show("开始日期不能大于结束日期,请修改!", "提示");
                            return;
                        }
                        dtUndrugList.Rows.Clear();
                        this.QueryPatientUndrugList(detailbeginTime, detailendTime);
                        this.fpUndrugList_Sheet1.DataSource = dvUndrugListTemp;
                    }
                    else if (this.neuTabControl1.SelectedIndex == 9)
                    {
                        FS.FrameWork.WinForms.Forms.IControlable ic1 = null;
                        TreeNode node = new TreeNode();
                        ic1 = this.tpDayFee.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                        if (ic1 != null)
                        {
                            ic1.SetValue(this.currentPatient, node);
                        }
                    }
                }
            }
        }

        //private void fpMainInfo_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        //{

        //}

        #region IInterfaceContainer 成员

        Type[] FS.FrameWork.WinForms.Forms.IInterfaceContainer.InterfaceTypes
        {
            get
            {
                Type[] type = new Type[2];

                //type[0] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceListPrint);
                type[1] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy);
                return type;
            }
        }

        #endregion

        private void fpFee_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Row < 0 || e.Row == this.fpFee_Sheet1.Rows.Count - 1) return;
            string balanceState = "0";
            string feecode = "";
            DateTime beginTime = this.currentPatient.PVisit.InTime;
            DateTime endTime = this.radtManager.GetDateTimeFromSysDateTime();
            if (this.fpFee_Sheet1.Cells[e.Row, 6].Text == "未结算")
                balanceState = "0";
            else if (this.fpFee_Sheet1.Cells[e.Row, 6].Text == "已结算")
                balanceState = "1";
            feecode = this.fpFee_Sheet1.Cells[e.Row, 7].Text;
            this.frmPatientFeeByMinFee.Text = this.fpFee_Sheet1.Cells[e.Row, 0].Text + this.fpFee_Sheet1.Cells[e.Row, 1].Text;
            //if (this.ucPatientFeeByMinFee.QueryDetailByMinFee(this.currentPatient.ID.ToString(), beginTime, endTime, balanceState, feecode))
            //{
            //    this.frmPatientFeeByMinFee.ShowDialog();
            //    return;
            //}
        }

        /// <summary>
        /// 过滤数据1
        /// </summary>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //取输入码
            string queryCode = this.textBox1.Text;
            queryCode = "%" + queryCode + "%";

            //设置过滤条件
            this.filterInput = "((药品名称 LIKE '" + queryCode + "') ) " ;//+"(五笔码 LIKE '" + queryCode + "') OR " + "(费用名称 LIKE '" + queryCode + "') )"

            //过滤药品数据
            this.SetFilter();
        }

        /// <summary>
        /// 设置过滤条件,过滤数据
        /// </summary>
        private void SetFilter()
        {
            //过滤数据
            if (this.neuTabControl1.SelectedIndex == 2)
            {
                if (this.dvDrugListTemp.Table != null && this.dvDrugListTemp.Table.Rows.Count > 0)
                {
                    this.fpDrugList_Sheet1.RowCount = 0;
                    this.fpDrugList_Sheet1.DataSource = dvDrugListTemp;
                    this.dvDrugListTemp.RowFilter = this.filterInput;
                }
            }
            else if (this.neuTabControl1.SelectedIndex == 3)
            {
                if (this.dvUndrugListTemp.Table != null && this.dvUndrugListTemp.Table.Rows.Count > 0)
                {
                    this.fpUndrugList_Sheet1.RowCount = 0;
                    this.fpUndrugList_Sheet1.DataSource = dvUndrugListTemp;
                    this.dvUndrugListTemp.RowFilter = this.filterInput;
                }
            }
        }

        /// <summary>
        /// 过滤数据2
        /// </summary>
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //取输入码
            string queryCode = this.textBox2.Text;
            queryCode = "%" + queryCode + "%";

            //设置过滤条件
            this.filterInput = "((项目名称 LIKE '" + queryCode + "') ) ";//+"(五笔码 LIKE '" + queryCode + "') OR " + "(费用名称 LIKE '" + queryCode + "') )"

            //过滤药品数据
            this.SetFilter();

        }
    }
}
