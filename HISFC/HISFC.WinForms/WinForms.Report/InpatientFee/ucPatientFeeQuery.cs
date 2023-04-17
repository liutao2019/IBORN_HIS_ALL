using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using System.Collections;

namespace FS.WinForms.Report.InpatientFee
{
    public partial class ucPatientFeeQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPatientFeeQuery()
        {
            InitializeComponent();
            this.fpDrugList.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpDrugList_ColumnWidthChanged);
            this.fpUndrugList.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpUndrugList_ColumnWidthChanged);
        }


        #region 变量
               
        /// <summary>
        /// 住院如出转业务层
        /// </summary>
        FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();

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

        /// <summary>
        /// 合同单位业务层
        /// </summary>
        FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
        ///// <summary>
        ///// 科室帮助类
        ///// </summary>
        //FS.FrameWork.Public.ObjectHelper departmentHelper = new FS.FrameWork.Public.ObjectHelper();
        ///// <summary>
        ///// 人员帮助类
        ///// </summary>
        //FS.FrameWork.Public.ObjectHelper employeeHelper = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// 最小费用帮助类
        /// </summary>
        FS.FrameWork.Public.ObjectHelper constantHelper = new FS.FrameWork.Public.ObjectHelper();

        #endregion

        /// <summary>
        /// 员工
        /// </summary>
        protected Hashtable hashTablePeople = new Hashtable();
        /// <summary>
        /// 科室
        /// </summary>
        protected Hashtable hashTableDept = new Hashtable();


        /// <summary>
        /// Tab
        /// </summary>
        protected Hashtable hashTableFp = new Hashtable();

        #region DataTalbe相关变量

        string pathNameMainInfo = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\OldQueryPatientMainInfo.xml";
        string pathNamePrepay = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\OldQueryPatientPrepay.xml";
        string pathNameFee = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\OldQueryPatientFee.xml";
        string pathNameDrugList = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\OldQueryPatientDrugList.xml";
        string pathNameUndrugList = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\OldQueryPatientUndrugList.xml";
        string pathNameBalance = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\OldQueryPatientBalance.xml";
        string pathNameDiagnose = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\OldQueryPatientDiagnose.xml";
        string pathNameChange = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\OldQueryPatientChange.xml";

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
        DataTable dtChange = new DataTable( );
        /// <summary>
        /// 变更信息视图
        /// </summary>
        DataView dvChange = new DataView( );

        #endregion

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
                        break;
                    }
                }
            }
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

            //初始化医生
            if (this.InitDoct() == -1)
            {
                return -1;
            }

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

            //departmentHelper.ArrayObject = m.GetDepartment();
            //employeeHelper.ArrayObject = m.QueryEmployeeAll();
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

            //{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
            //ArrayList pactList = this.consManager.GetList(FS.HISFC.Models.Base.EnumConstant.PACTUNIT);
            ArrayList pactList = this.pactManager.QueryPactUnitAll();
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

            if (findAll >= 0)
            {
                this.cmbDept.SelectedIndex = findAll;
            }

            return 1;
        }

        /// <summary>
        /// 初始化科室
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        private int InitDoct()
        {
            int findAll = 0;
            FS.FrameWork.Models.NeuObject objAll = new FS.FrameWork.Models.NeuObject();

            objAll.ID = "%";
            objAll.Name = "全部";

            ArrayList doctList = this.personManager.GetEmployee( FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (doctList == null)
            {
                MessageBox.Show(Language.Msg("加载医生列表出错!") + this.deptManager.Err);

                return -1;
            }

            doctList.Add(objAll);

            findAll = doctList.IndexOf(objAll);

            this.cmbDoct.AddItems(doctList);

            if (findAll >= 0)
            {
                this.cmbDoct.SelectedIndex = findAll;
            }

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
                //this.ucQueryInpatientNo1.Text = this.currentPatient.ID.Substring(4);
                //设置查询时间
                //设置查询时间
                DateTime beginTime = this.currentPatient.PVisit.InTime;
                DateTime endTime = this.radtManager.GetDateTimeFromSysDateTime();

                this.QueryAllInfomaition(beginTime, endTime);
                if (dtBalance.Rows.Count > 0)
                {
                    this.lblInState.Text = dtBalance.Rows[0]["结算类型"].ToString();
                }
            }
            this.fpMainInfo_Sheet1.Columns[13].Width = 180;
            return 1;
        }

        /// <summary>
        /// 显示患者基本信息
        /// </summary>
        /// <param name="patient">成功 1 失败 -1</param>
        private void SetPatientToFpMain(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            DataRow row = this.dtMainInfo.NewRow();

            try
            {

                row["住院流水号"] = patient.ID;
                row["住院号"] = patient.PID.PatientNO;
                row["姓名"] = patient.Name;
                row["住院科室"] = patient.PVisit.PatientLocation.Dept.Name;
                row["床号"] = patient.PVisit.PatientLocation.Bed.ID;
                row["患者类别"] = patient.Pact.Name;
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
                //row["医疗类别"] = patient.PVisit.MedicalType.Name;
                row["结算日期"] = patient.BalanceDate == new DateTime(1, 1, 1).Date ? string.Empty : patient.BalanceDate.ToString();

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
            ArrayList drugList = this.feeManager.GetMedItemsForInpatient(this.currentPatient.ID, beginTime, endTime);
            if (drugList == null)
            {
                MessageBox.Show(Language.Msg("获得患者药品明细出错!") + this.feeManager.Err);
                
                return;
            }

            string strTemp = null;
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
                ////row["执行科室"] = this.deptManager.GetDeptmentById(obj.ExecOper.Dept.ID).Name;
                ////row["患者科室"] = this.deptManager.GetDeptmentById(((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID).Name; 
                //row["执行科室"] = this.departmentHelper.GetName (obj.ExecOper.Dept.ID);
                //row["患者科室"] = this.departmentHelper.GetName (((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID); 
                #endregion

                strTemp = obj.ExecOper.Dept.ID;
                row["执行科室"] = this.hashTableDept.Contains(strTemp) ? this.hashTableDept[strTemp] : strTemp;

                strTemp = ((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID;
                row["患者科室"] = this.hashTableDept.Contains(strTemp) ? this.hashTableDept[strTemp] : strTemp;


                row["收费时间"] = obj.FeeOper.OperTime;

                //FS.HISFC.BizProcess.Integrate.Manager managerIntergrate = new FS.HISFC.BizProcess.Integrate.Manager();
                //FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
                //#region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                ////empl = managerIntergrate.GetEmployeeInfo(obj.FeeOper.ID); 
                //empl = this.employeeHelper.GetObjectFromID(obj.FeeOper.ID) as  FS.HISFC.Models.Base.Employee; 
                ////变态有时候居然会出现查不出的人
                //if (empl == null || empl.Name == string.Empty)
                //#endregion
                //{
                //    row["收费员"] = obj.FeeOper.ID;
                //}
                //else
                //{
                //    row["收费员"] = empl.Name;
                //}

                strTemp = obj.FeeOper.ID;
                row["收费员"] = this.hashTablePeople.Contains(strTemp) ? this.hashTablePeople[strTemp] : strTemp;


                row["发药时间"] = obj.ExecOper.OperTime.Date == new DateTime(1, 1, 1).Date ? string.Empty : obj.ExecOper.OperTime.ToString();

                //FS.HISFC.Models.Base.Employee confirmOper = new FS.HISFC.Models.Base.Employee();
                //#region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                ////confirmOper = managerIntergrate.GetEmployeeInfo(obj.ExecOper.ID);
                //confirmOper = employeeHelper.GetObjectFromID(obj.ExecOper.ID) as FS.HISFC.Models.Base.Employee;
                //#endregion
                //if (confirmOper == null || confirmOper.Name == string.Empty)
                //{
                //    row["发药员"] = obj.ExecOper.ID;
                //}
                //else
                //{
                //    row["发药员"] = confirmOper.Name;
                //}

                strTemp = obj.ExecOper.ID;
                row["发药员"] = this.hashTablePeople.Contains(strTemp) ? this.hashTablePeople[strTemp] : strTemp;
                
                //row["来源"] = obj.FTSource;
                row["费用分类"] = obj.Item.MinFee.ID;
                dtDrugList.Rows.Add(row);
            }

            this.AddSumInfo(dtDrugList, "药品名称", "合计:", "金额", "自费", "公费", "自负", "优惠");
        }

        /// <summary>
        /// 查询患者非药品明细
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        private void QueryPatientUndrugList(DateTime beginTime, DateTime endTime)
        {
            ArrayList undrugList = this.feeManager.QueryFeeItemLists(this.currentPatient.ID, beginTime, endTime);
            if (undrugList == null)
            {
                MessageBox.Show(Language.Msg("获得患者非药品明细出错!") + this.feeManager.Err);

                return;
            }

            string strTemp = string.Empty;
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
                //FS.HISFC.BizProcess.Integrate.Manager managerIntergrate = new FS.HISFC.BizProcess.Integrate.Manager();
                //FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
                //#region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                ////empl = managerIntergrate.GetEmployeeInfo(obj.FeeOper.ID); 
                //empl = this.employeeHelper.GetObjectFromID(obj.FeeOper.ID) as FS.HISFC.Models.Base.Employee; 
                //#endregion

                //if (empl == null || empl.Name == string.Empty)
                //{
                //    row["收费员"] = obj.FeeOper.ID;
                //}
                //else
                //{
                //    row["收费员"] = empl.Name;
                //}

                strTemp = obj.FeeOper.ID;
                row["收费员"] = this.hashTablePeople.Contains(strTemp) ? this.hashTablePeople[strTemp] : strTemp;


                //#region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                ////row["执行科室"] = this.deptManager.GetDeptmentById(obj.ExecOper.Dept.ID).Name;
                ////row["患者科室"] = this.deptManager.GetDeptmentById(((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID).Name; 
                //row["执行科室"] = this.departmentHelper.GetName(obj.ExecOper.Dept.ID);
                //row["患者科室"] = this.departmentHelper.GetName(((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID); 
                //#endregion

                strTemp = obj.ExecOper.Dept.ID;
                row["执行科室"] = this.hashTableDept.Contains(strTemp) ? this.hashTableDept[strTemp] : strTemp;

                strTemp = ((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID;
                row["患者科室"] = this.hashTableDept.Contains(strTemp) ? this.hashTableDept[strTemp] : strTemp;

                //row["来源"] = obj.FTSource;
                row["费用分类"] = obj.Item.MinFee.ID;
                dtUndrugList.Rows.Add(row);
            }

            this.AddSumInfo(dtUndrugList, "项目名称", "合计:", "金额", "自费", "公费", "自负", "优惠");
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

            string strTemp = null;
            foreach (FS.HISFC.Models.Fee.Inpatient.Prepay prepay in prepayList)
            {
                //FS.HISFC.Models.Base.Employee employeeObj = new FS.HISFC.Models.Base.Employee();
                //FS.HISFC.Models.Base.Department deptObj = new FS.HISFC.Models.Base.Department();
                DataRow row = dtPrepay.NewRow();

                row["票据号"] = prepay.RecipeNO;
                row["预交金额"] = prepay.FT.PrepayCost;
                row["支付方式"] = prepay.PayType.Name;

                //#region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                ////   employeeObj = this.personManager.GetPersonByID(prepay.PrepayOper.ID);
                //employeeObj = this.employeeHelper.GetObjectFromID(prepay.PrepayOper.ID) as FS.HISFC.Models.Base.Employee;
                //#endregion
                //row["操作员"] = employeeObj.Name;

                strTemp = prepay.PrepayOper.ID;
                row["操作员"] = this.hashTablePeople.Contains(strTemp) ? this.hashTablePeople[strTemp] : strTemp;

                row["操作日期"] = prepay.PrepayOper.OperTime;

                //#region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                ////deptObj = this.deptManager.GetDeptmentById(((FS.HISFC.Models.RADT.PatientInfo)prepay.Patient).PVisit.PatientLocation.Dept.ID);
                //deptObj = this.departmentHelper.GetObjectFromID(((FS.HISFC.Models.RADT.PatientInfo)prepay.Patient).PVisit.PatientLocation.Dept.ID) 
                //    as FS.HISFC.Models.Base.Department; 
                //#endregion
                //row["所在科室"] = deptObj.Name;

                strTemp = ((FS.HISFC.Models.RADT.PatientInfo)prepay.Patient).PVisit.PatientLocation.Dept.ID;
                row["所在科室"] = this.hashTablePeople.Contains(strTemp) ? this.hashTablePeople[strTemp] : strTemp;


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
                row["费用类别"] = obj.Item.MinFee.ID;



                //row["来源"] = obj.FTSource;

                dtFeeclass.Rows.Add(row);
            }

            this.AddSumInfo(dtFeeclass, "项目名称", "合计:", "金额", "自费", "公费", "自负", "优惠");

            //不知道为什么要去掉0的行
            //deleteqltisnull();
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



            //feeInfoList.AddRange(feeInfoListBalanced);

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo in feeInfoList)
            {

                DataRow row = dtFee.NewRow();
                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //row["费用名称"] = this.feeManager.GetComDictionaryNameByID("MINFEE", feeInfo.Item.MinFee.ID);
                
                row["费用名称"] = this.constantHelper.GetName ( feeInfo.Item.MinFee.ID);
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
                row["费用分类"] = feeInfo.Item.MinFee.ID;
                dtFee.Rows.Add(row);
            }

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

                dtFee.Rows.Add(row);
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

            string strTemp = null;

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

                //#region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                ////employeeObj = this.personManager.GetPersonByID(balance.BalanceOper.ID); 
                //employeeObj = this.employeeHelper.GetObjectFromID(balance.BalanceOper.ID) as FS.HISFC.Models.Base.Employee; 
                //#endregion

                //row["操作员"] = employeeObj.Name;

                strTemp = balance.BalanceOper.ID;
                row["操作员"] = this.hashTablePeople.Contains(strTemp) ? this.hashTablePeople[strTemp] : strTemp;


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
        private void QueryPatientChange( DateTime beginTime, DateTime endTime )
        {
            ArrayList changeList = this.radtManager.QueryPatientShiftInfoNew(this.currentPatient.ID);
            if (changeList == null) 
            {
                MessageBox.Show(Language.Msg("获得患者变更信息出错!") + this.radtManager.Err);

                return;
            }

            string strTemp = null;

            foreach (FS.HISFC.Models.Invalid.CShiftData change in changeList)
            {
                //FS.HISFC.Models.Base.Employee employeeObj = new FS.HISFC.Models.Base.Employee( );
                DataRow row = this.dtChange.NewRow( );
                row["序号"] = change.HappenNo.ToString();
                row["变更原因"] = change.ShitCause;
                row["原编码"] = change.OldDataCode;
                row["原名称"] = change.OldDataName;
                row["现编码"] = change.NewDataCode;
                row["现名称"] = change.NewDataName;
                row["备注"] = change.Mark;
                row["操作人编码"] = change.OperCode;
                //#region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                ////employeeObj = this.personManager.GetPersonByID(change.OperCode);
                //employeeObj = this.employeeHelper.GetObjectFromID(change.OperCode) as FS.HISFC.Models.Base.Employee; 
                //#endregion
                //row["操作人姓名"] = employeeObj.Name;

                strTemp = change.OperCode;
                row["操作人姓名"] = this.hashTablePeople.Contains(strTemp) ? this.hashTablePeople[strTemp] : strTemp;

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

            //使用第一列作为合计的内容
            //sumRow[totName] = disName;
            sumRow[table.Columns[0]] = disName;
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
            inputName = FS.FrameWork.Public.String.TakeOffSpecialChar(inputName,"'");
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
																new DataColumn("结算日期", str)/*,
																new DataColumn("医疗类别", str)*/});

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
				                                                new DataColumn("来源",str),
                                                                new DataColumn("费用分类", str)});

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
				                                                  new DataColumn("来源", str),
                                                                  new DataColumn("费用分类", str)});

                dvUndrugList = new DataView(dtUndrugList);

                this.fpUndrugList_Sheet1.DataSource = dvUndrugList;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpUndrugList_Sheet1, pathNameUndrugList);
            }
      
            #endregion

            #region 收费项目汇总

            //if (System.IO.File.Exists(pathNameFeeclass))
            //{
            //    FS.NFC.Interface.Classes.CustomerFp.CreatColumnByXML(pathNameFeeclass, dtFeeclass, ref dvFeeclass, this.fpFeeclass_Sheet1);

            //    FS.NFC.Interface.Classes.CustomerFp.ReadColumnProperty(this.fpFeeclass_Sheet1, pathNameFeeclass);
            //}
            //else
            //{
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
                                                                new DataColumn("费用类别", str),
                                                                new DataColumn("执行科室",str),
                                                                new DataColumn("患者科室",str),
                                                                new DataColumn("收费员", str),
                                                                new DataColumn("来源",str)});

            dvFeeclass = new DataView(dtFeeclass);

            this.fpFeeclass_Sheet1.DataSource = dvFeeclass;

            //    FS.NFC.Interface.Classes.CustomerFp.SaveColumnProperty(this.fpFeeclass_Sheet1, pathNameFeeclass);
            //}

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
														   new DataColumn("结算状态", str)});

                dvFee = new DataView(dtFee);

                this.fpFee_Sheet1.DataSource = dvFee;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpFee_Sheet1, pathNameFee);
            }
            dtFee.Columns.Add(new DataColumn("费用分类", str));
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
            if (this.currentPatient.PVisit.OutTime != DateTime.MinValue)
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
            this.lblfreecost.Text = this.currentPatient.FT.LeftCost.ToString( );
            if (this.currentPatient.CompanyName != null)
            {
                this.lblwork.Text = this.currentPatient.CompanyName.ToString( );
            }
            else
            {
                this.lblwork.Text = "";
            }
            if (this.currentPatient.AddressHome != null)
            {
                this.lblhome.Text = this.currentPatient.AddressHome.ToString( );
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

            neuSpread1.Visible = false;
        }

        /// <summary>
        /// 返回"_"
        /// </summary>
        /// <param name="langth">长度</param>
        /// <returns>成功 "---" 失败 null</returns>
        private string RetrunSplit(int langth) 
        {
            string s = string.Empty ;

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
            dtChange.Rows.Clear( );
        }
        private void InitContr()
        {
            this.neuLabel21.Visible = false;
            this.lblOwnCost.Visible = false;
            this.neuLabel23.Visible = false;
            this.lblPubCost.Visible = false;
        }


        #endregion

        private void ucQueryInpatientNo1_myEvent()
        {
            this.QueryPatientByInpatientNO();
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            print.PrintPage(0, 0, this.neuTabControl1.SelectedTab);
            
            return base.OnPrint(sender, neuObject);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.neuButton1_Click(null, null);
            return base.OnQuery(sender, neuObject);
        }
        //打印预览
        public override int  PrintPreview(object sender, object neuObject)
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

        private void neuButton1_Click(object sender, EventArgs e)
        {
            string inState = string.Empty;//入院状态
            string deptCode = string.Empty;//住院科室编码
            string pactCode = string.Empty;//合同单位编码
            string doctCode = string.Empty;//管床医生
            DateTime beginTime = this.dtpBeginTime.Value.Date;
            DateTime endTime = this.dtpEndTime.Value;

            if (this.cmbDept.Text == string.Empty || this.cmbDept.Text == "全部")
            {
                deptCode = "%";
               
            }
            else
            {
                deptCode = this.cmbDept.Tag.ToString();
            }

            if (this.cmbDoct.Text == string.Empty || this.cmbDoct.Text == "全部")
            {
                doctCode = "%";

            }
            else
            {
                doctCode = this.cmbDoct.Tag.ToString();
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
          
            ArrayList patientListTemp = radtManager.QueryPatientByConditons(pactCode, deptCode, inState, beginTime, endTime,doctCode);
            if (patientListTemp == null)
            {
                MessageBox.Show(Language.Msg("或者患者基本信息出错!") + this.radtManager.Err);
                Cursor.Current = Cursors.Arrow;
                return;
            }
            Cursor.Current = Cursors.Arrow;
            this.QueryPatient(patientListTemp);
        }

        private void ucPatientFeeQuery_Load(object sender, EventArgs e)
        {
            this.Init();

            this.hashTableDept.Clear();
            this.hashTablePeople.Clear();

            ArrayList tdlist = deptManager.GetDeptAllUserStopDisuse();
            foreach (FS.HISFC.Models.Base.Department tempinfo in tdlist)
            {
                hashTableDept.Add(tempinfo.ID, tempinfo.Name);
            }

            ArrayList tplist = personManager.GetEmployeeAll();
            foreach (FS.HISFC.Models.Base.Employee tempinfo in tplist)
            {
                hashTablePeople.Add(tempinfo.ID, tempinfo.Name);
            } 
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
            dtChange.Rows.Clear( );

            dtFeeclass.Rows.Clear();
            //设置查询时间
            DateTime beginTime = this.currentPatient.PVisit.InTime;
            DateTime endTime = this.radtManager.GetDateTimeFromSysDateTime();

            this.QueryAllInfomaition(beginTime, endTime);
            if (dtBalance.Rows.Count > 0)
            {
                this.lblInState.Text = dtBalance.Rows[0]["结算类型"].ToString();
            }

        }

        private void txtNameKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.QueryPatientByName();
            }
           
        }

        private void fpFee_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            neuSpread1_Sheet1.RowCount = 0;
            neuSpread1_Sheet2.RowCount = 0;
            int rowindex = 0;
            if (!neuSpread1.Visible)
            {
                neuSpread1.Visible = true;
            }

            string s = fpFee_Sheet1.Cells[e.Row, 7].Text;
            DataView dv2 = new DataView(dtDrugList, "费用分类='" + s + "'", "药品名称,收费时间", DataViewRowState.CurrentRows);
            DataView dv3 = new DataView(dtUndrugList, "费用分类='" + s + "'", "项目名称,收费时间", DataViewRowState.CurrentRows);
            //判断类别是药品还是非药品
            if (dv2.Count > 0)
            {
                DataView dv = new DataView(dtFeeclass, "费用类别='" + s + "'", "项目名称", DataViewRowState.CurrentRows);
                neuSpread1_Sheet1.DataSource = dv;
            }
            else if (dv3.Count > 0)
            {
                DataView dv1 = new DataView(dtFeeclass, "费用类别='" + s + "'", "项目名称", DataViewRowState.CurrentRows);
                neuSpread1_Sheet2.DataSource = dv1;
            }
            if (neuSpread1_Sheet1.RowCount == 0)
            {
                neuSpread1.ActiveSheetIndex = 1;
            }
            else
            {
                neuSpread1.ActiveSheetIndex = 0;
            }
        }

        private void fpFeeclass_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }

        private void fpDrugList_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpUndrugList_Sheet1, pathNameDrugList);
        }

        private void fpUndrugList_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpUndrugList_Sheet1, pathNameUndrugList);
        }
 
        //private void fpMainInfo_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        //{

        //}
    }
}
