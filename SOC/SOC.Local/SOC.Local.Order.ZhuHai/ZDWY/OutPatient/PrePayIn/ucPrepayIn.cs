using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Classes;
using FS.HISFC.Models.Base;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.PrePayIn
{
    public partial class ucPrepayIn : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer,FS.HISFC.BizProcess.Interface.Fee.IPrePayIn
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ucPrepayIn()
        {
            InitializeComponent();
        }

        #region 变量

        DataTable _dtPrepayIn = new DataTable();
        DataView _dvPrepayIn;

        protected FS.FrameWork.WinForms.Forms.ToolBarService _toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        //FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        /// <summary>
        /// 患者信息
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo _myPatientInfo = null;

        /// <summary>
        /// adt接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.IHE.IADT _adt = null;

        /// <summary>
        /// 入院通知单打印接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.IPrintInHosNotice _iPrintInHosNotice = null;

        /// <summary>
        /// 是否新增
        /// </summary>
        bool isNew = false;
        #endregion

        #region 属性
        /// <summary>
        /// 患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get
            {
                return _myPatientInfo;
            }
            set
            {
                if (value == null)
                {
                    _myPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                }
                else
                {
                    _myPatientInfo = value;

                    if (_myPatientInfo!= null)
                    {
                        this.SetPrepatient(_myPatientInfo, true);// {F6204EF5-F295-4d91-B81A-736A268DD394}
                    }
                }
            }
        }
        public void ShowDialog()
        {            
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(this);
        }


        private bool isShowButton = false;
        /// <summary>
        /// 是否显示保存控件
        /// </summary>
        public bool IsShowButton
        {
            get
            {
                return isShowButton;
            }
            set
            {
                isShowButton = value;
                this.btSave.Visible = isShowButton;
                this.btPrint.Visible = isShowButton;
                this.btQuery.Visible = isShowButton;

            }
        }
        #endregion

        #region 业务层变量

        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizProcess.Integrate.Fee _feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        FS.HISFC.BizLogic.Manager.Bed bedMgr = new FS.HISFC.BizLogic.Manager.Bed();
        FS.HISFC.Models.HealthRecord.ICD icdMgr = null;
        FS.HISFC.BizLogic.Fee.PactUnitInfo PactUnit = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
        
        #endregion

        #region 初始化

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            _toolBarService.AddToolButton("清屏", "清除录入的信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            _toolBarService.AddToolButton("取消预约", "取消预约", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            _toolBarService.AddToolButton("入院通知单", "打印入院通知单", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印执行单, true, false, null);
            _toolBarService.AddToolButton("刷卡", "刷卡", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B报警, true, false, null);

            return _toolBarService;
        }


        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "清屏":
                    this.Clear();
                    break;

                case "取消预约":
                    this.CancelPre();
                    break;

                case "入院通知单":
                    this.PrintNotice();
                    break;

                case "刷卡":// {DC68D3DF-1CB9-4d58-93E0-4F85B58E1647}
                    string mCardNO = "";
                    string error = "";
                    if (FS.HISFC.Components.Common.Classes.Function.OperMCard(ref mCardNO, ref error) < 0)
                    {
                        MessageBox.Show("读卡失败，请确认是否正确放置诊疗卡！\n" + error);
                        return;
                    }

                    mCardNO = ";" + mCardNO;
                    this.txtCardNo.Focus();
                    this.txtCardNo.SelectAll();
                    this.txtCardNo.Text = mCardNO;
                    this.txtCardNo_KeyDown(null, new KeyEventArgs(Keys.Enter));
                    break;

                default: break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryData();

            return base.OnQuery(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();

            return base.OnPrint(sender, neuObject);
        }

        /// <summary>
        /// 初始化控件,等信息
        /// </summary>
        /// <returns>成功 1 失败: -1</returns>
        protected virtual int Init()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在初始化窗口，请稍候^^");
            Application.DoEvents();
            try
            {
                //初始化住院科室列表
                this.cmbDept.AddItems(this.managerIntegrate.GetDepartment(EnumDepartmentType.I));   //this.myDept.GetInHosDepartment());
                //护士站
                this.cmbNurseCell.AddItems(this.managerIntegrate.GetDepartment(EnumDepartmentType.N)); //this.myDept.GetDeptment(EnumDepartmentType.N));

                //婚姻状况
                this.cmbMarriage.AddItems(FS.HISFC.Models.RADT.MaritalStatusEnumService.List());

                //合同单位{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
                //this.cmbPactUnit.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.PACTUNIT));
                this.cmbPactUnit.AddItems(SOC.HISFC.BizProcess.Cache.Fee.GetAllPactUnit());

                //职业信息
                this.cmbPos.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.PROFESSION));

                //国籍
                this.cmbCountry.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.COUNTRY));

                //出生地信息
                this.cmbHomePlace.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.DIST));

                //联系人地址信息
                //this.cmbLinkManAddr.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.AREA));

                //家庭住址信息
                //this.txtHomeAddr.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.AREA));

                //预约医生
                this.cmbPreDoc.AddItems(this.managerIntegrate.QueryEmployee(EnumEmployeeType.D));

                //联系人关系
                this.cmbLinkManRel.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.RELATIVE));

                //民族
                this.cmbNationality.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.NATION));

                //性别			
                this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());

                //生日
                this.dtBirthday.Value = this.bedMgr.GetDateTimeFromSysDateTime(); //this.myInpatient.GetDateTimeFromSysDateTime();
                //操作员
                //this.txtOper.Text =this.managerIntegrate. this.myInpatient.Operator.Name;

                //预约时间
                this.dtPreDate.Value = this.bedMgr.GetDateTimeFromSysDateTime();
                //患者类型信息// {67256EED-3DE6-4179-9C49-9A4D115DE309}
                this.cmbPatientType.AddItems(this.managerIntegrate.GetConstantList("PatientType"));
                this.cmbPatientType.SelectedIndex = 0;
                //结算类别
                this.cmbPayKind.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.PAYKIND));
                this.cmbPayKind.SelectedIndex = 0;
                this.dtBegin.Value = this.bedMgr.GetDateTimeFromSysDateTime().AddDays(-1);
                this.dtEnd.Value = this.bedMgr.GetDateTimeFromSysDateTime().AddDays(1);
                this.cmbPreDoc.Tag = this.bedMgr.Operator.ID;

                //诊断
                this.ucDiagnose1.Init();
                this.ucDiagnose1.SelectItem += new FS.HISFC.Components.Common.Controls.ucDiagnose.MyDelegate(ucDiagnose1_SelectItem);
                this.ActiveControl = this.txtCardNo;

                this.cmbCircs.AddItems(managerIntegrate.GetConstantList(EnumConstant.INCIRCS));
                this.cmbCircs.SelectedIndex = 0;

                this.InitInterface();


                cmbDept.SelectedIndex = -1;
                cmbDept.Tag = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(this.bedMgr.Operator.ID).Dept.ID;


                if (_myPatientInfo != null)// {D59C1D74-CE65-424a-9CB3-7F9174664504}
                {
                    this.SetPrepatient(_myPatientInfo, true);// {F6204EF5-F295-4d91-B81A-736A268DD394}
                }
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }

            return 1;
        }
        #endregion

        #region  方法

        public void Close()
        {
            this.FindForm().Close();
        }

        /// <summary>
        /// 清屏
        /// </summary>
        public void Clear()
        {
            foreach (Control c in this.tabPage1.Controls)
            {
                if (c.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuGroupBox)) continue;

                foreach (Control ctr in c.Controls)
                {
                    if (ctr.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuTextBox)
                        || ctr.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuComboBox))
                    {
                        if (ctr.Name != "txtOper" || ctr.Name != "cmbPreDoc")
                        {
                            ctr.Text = "";
                            ctr.Tag = "";
                        }
                    }

                }
            }

            this.dtPreDate.Value = this.bedMgr.GetDateTimeFromSysDateTime();
            this.dtBirthday.Value = this.bedMgr.GetDateTimeFromSysDateTime();
            this.cmbCircs.SelectedIndex = 0;
            if (this.cmbPreDoc.Items.Count > 0)
            {
                cmbPreDoc.Tag=this.bedMgr.Operator.ID;
            }
            this.cmbPatientType.Tag = "P";
            this.cmbPayKind.Tag = "01";
            this.cmbPactUnit.Tag = "1";
            this.txtCardNo.Focus();
            cmbDept.SelectedIndex = -1;
            cmbDept.Tag = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(this.bedMgr.Operator.ID).Dept.ID;
        }

        /// <summary>
        /// 设置患者信息
        /// </summary>
        private void SetPrepatient(FS.HISFC.Models.RADT.PatientInfo pInfo, bool isNew)
        {
            this.txtCardNo.Text = pInfo.PID.CardNO;//病例号
            txtCardNo.Tag = pInfo.User01;

            this.cmbDept.Tag = pInfo.PVisit.PatientLocation.Dept.ID;//住院科室
            this.cmbPatientType.Tag = pInfo.PatientType.ID;// {67256EED-3DE6-4179-9C49-9A4D115DE309}
            if (this.cmbPatientType.Items.Count > 0)
            {
                this.cmbPatientType.SelectedIndex = 0;
            }
            this.cmbDept.Text = pInfo.PVisit.PatientLocation.Dept.Name;
            this.cmbNurseCell.Tag = pInfo.PVisit.PatientLocation.NurseCell.ID;
            // {D59C1D74-CE65-424a-9CB3-7F9174664504}
            if (cmbDept.Tag == null
                || string.IsNullOrEmpty(cmbDept.Tag.ToString()))
            {
                cmbDept.SelectedIndex = -1;
                cmbDept.Tag = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(this.bedMgr.Operator.ID).Dept.ID;
            }

            if (pInfo.PVisit.InTime >= DateTime.MaxValue || pInfo.PVisit.InTime <= DateTime.MinValue || pInfo.PVisit.InTime == null)
            {

                this.dtPreDate.Value = this.bedMgr.GetDateTimeFromSysDateTime();
            }
            else
            {
                this.dtPreDate.Value = pInfo.PVisit.InTime;//预约日期----------
            }

            this.txtName.Text = pInfo.Name;//姓名
            if (this.cmbSex.Items.Count == 0)
            {                
                this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());
            }
            this.cmbSex.Tag = pInfo.Sex.ID;//性别

            if (string.IsNullOrEmpty(this.cmbSex.Tag.ToString()) || string.IsNullOrEmpty(cmbSex.Text))
            {
                this.cmbSex.SelectedIndex = 1;
            }
            this.cmbPactUnit.Tag = pInfo.Pact.ID;//合同单位 
            //this.cmbPactUnit.Text = pInfo.Pact.Name;//合同单位

            if (this.cmbPayKind.Items.Count == 0)
            {
                this.cmbPayKind.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.PAYKIND));
            }
            this.cmbPayKind.Tag = pInfo.Pact.PayKind.ID;//结算类别
            if (string.IsNullOrEmpty(this.cmbPayKind.Tag.ToString()))// {D5390E6B-48F5-45d3-B956-3BC952BDE1EA}
            {
                FS.HISFC.Models.Base.PactInfo pact = new FS.HISFC.Models.Base.PactInfo();
                pact = this.PactUnit.GetPactUnitInfoByPactCode(pInfo.Pact.ID);
                this.cmbPayKind.Tag = pact.PayKind.ID;

            }

            if (DateTime.MaxValue > pInfo.Birthday && pInfo.Birthday > DateTime.MinValue)
            {
                this.dtBirthday.Value = pInfo.Birthday;//出生日期// {F6204EF5-F295-4d91-B81A-736A268DD394}
            }
            else
            {
                this.dtBirthday.Value = this.bedMgr.GetDateTimeFromSysDateTime();
            }
            this.cmbMarriage.Tag = pInfo.MaritalStatus.ID;//婚姻状况
            this.txtIdentity.Text = pInfo.IDCard;//身份证号
            this.cmbPos.Tag = pInfo.Profession.ID;//职业
            this.cmbHomePlace.Tag = pInfo.AreaCode;//出生地
            this.cmbCountry.Tag = pInfo.Country.ID;//国籍

            this.txtHomeAddr.Text = pInfo.AddressHome;//家庭住址
            this.txtHomeTel.Text = pInfo.PhoneHome;//家庭电话
            this.txtWorkUnit.Text = pInfo.CompanyName;//工作单位
            this.txtLinkMan.Text = pInfo.Kin.Name;//联系人
            this.txtLinkManAddr.Text = pInfo.Kin.RelationAddress;//联系人住址
            this.txtLinkTel.Text = pInfo.Kin.RelationPhone;//联系人电话
            this.txtWorkTel.Text = pInfo.PhoneBusiness;//工作单位电话
            this.cmbNationality.Tag = pInfo.Nationality.ID;//民族
            this.cmbLinkManRel.Tag = pInfo.Kin.Relation.ID;//联系人关系
            this.cmbBedNo.Tag = pInfo.PVisit.PatientLocation.Bed.ID;//病床号

            this.cmbPreDoc.Tag = pInfo.PVisit.AdmittingDoctor.ID;//预约医生

            #region 带出门诊诊断

            if (isNew)
            {
                //if (pInfo.Diagnoses.Count > 0)
                //{
                //    this.txtInDiagnose.Tag = (pInfo.Diagnoses[0] as FS.FrameWork.Models.NeuObject).ID;//门诊诊断编码
                //}

                //this.txtInDiagnose.Text = pInfo.ClinicDiagnose;//门诊诊断名称

                string sql = @"
                        select * from 
                        (select f.diag_name
                        from met_cas_diagnose f,fin_opr_register r
                        where /*f.diag_kind='1'
                        and*/ f.inpatient_no=r.clinic_code
                        and r.card_no='{0}'
                        order by r.reg_date,f.diag_kind desc
                        )
                        where rownum=1";

                string strDiag = "";
                sql = string.Format(sql, pInfo.ID);
                strDiag = bedMgr.ExecSqlReturnOne(sql, "");
                if (strDiag == "-1")
                {
                    strDiag = "";
                }

                if (!string.IsNullOrEmpty(strDiag))
                {
                    txtInDiagnose.Text = strDiag.TrimEnd('、');
                }

                //ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(pInfo.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
                //if (al == null)
                //{
                //    MessageBox.Show("查询诊断信息错误！\r\n" + diagManager.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return; 
                //}

                //string strDiag = "";
                //foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
                //{
                //    if (diag != null && diag.IsValid)
                //    {
                //        strDiag += diag.DiagInfo.ICD10.Name + "、";
                //    }
                //}
                //txtInDiagnose.Text = strDiag.TrimEnd('、');
            }
            else
            {
                txtInDiagnose.Text = pInfo.ClinicDiagnose;
            }

            #endregion

            this.txtSSN.Text = pInfo.SSN;//医疗证号

            //押金
            this.numericUpDownForegift.Value = pInfo.FT.PrepayCost;

            //指征
            this.txtMemo.Text = pInfo.Memo;

            //入院情况
            if (pInfo.PVisit.Circs.ID == "")
            {
                if (this.cmbCircs.Items.Count > 0)
                {
                    this.cmbCircs.SelectedIndex = 0;
                }
            }
            else
            {
                this.cmbCircs.Tag = pInfo.PVisit.Circs.ID;
            }

            this.isNew = isNew;
        }

        protected override int OnSave(object sender, object neuObject)
        {
            try
            {
                return this.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show("预约登记失败：" + "\r\n" + ex.Message + "\r\n" + ex.TargetSite + "\r\n" + ex.StackTrace, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return 0;
        }

        /// <summary>
        /// 保存预约患者登记信息
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            //有效性判断
            if (!this.CheckValid())
            {
                return -1;
            }

            //得到PatientInfo实体
            this.GetPrePatient();

            //事务处理
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            managerIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (isNew)
            {
                if (this.managerIntegrate.InsertPreInPatient(this.PatientInfo) < 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();

                    if (this.managerIntegrate.DBErrCode == 1)
                    {
                        MessageBox.Show("此人已预约登记!");
                        this.txtCardNo.Focus();
                        this.txtCardNo.SelectAll();
                    }
                    else
                    {
                        MessageBox.Show("预约登记失败！\r\n" + managerIntegrate.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    return -1;
                }
            }
            else
            {
                if (this.managerIntegrate.UpdatePreInPatientByHappenNo(this.PatientInfo) < 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();

                    if (this.managerIntegrate.DBErrCode == 1)
                    {
                        MessageBox.Show("该患者更新预约登记失败!\r\n" + managerIntegrate.Err);
                        this.txtCardNo.Focus();
                        this.txtCardNo.SelectAll();
                    }
                    else
                    {
                        MessageBox.Show("更新预约登记失败！\r\n" + managerIntegrate.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return -1;
                }
            }

            if (this.cmbPreDoc.SelectedIndex >= 0)
            {
                this._myPatientInfo.PVisit.ReferringDoctor.ID = this.cmbPreDoc.Tag.ToString();
                this._myPatientInfo.PVisit.ReferringDoctor.Name = this.cmbPreDoc.Text;

                this._myPatientInfo.PVisit.AdmittingDoctor.ID = string.Empty;
            }

            if (this._adt == null)
            {
                this._adt = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IHE.IADT)) as FS.HISFC.BizProcess.Interface.IHE.IADT;
            }
            if (this._adt != null)
            {
                this._adt.PreRegInpatient(_myPatientInfo);
            }

            //提交
            FS.FrameWork.Management.PublicTrans.Commit();
            if (isNew)
            {
                MessageBox.Show("预约登记成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("更新预约信息成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.Clear();

            this.neuTabControl1.SelectedIndex = 1;
            this.QueryData();

            if (null == _iPrintInHosNotice)
            {
                MessageBox.Show("无法打印入院通知单", "提示", MessageBoxButtons.OK);
            }
            else
            {

                DialogResult dr = MessageBox.Show("是否打印入院通知单？", "提示", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    _iPrintInHosNotice.SetValue(this.PatientInfo);
                    _iPrintInHosNotice.Print();
                }
            }

            if (isShowButton)
            {
                this.Close();
            }

            return 0;
        }

        /// <summary>
        /// 判断输入合法性
        /// </summary>
        /// <returns></returns>
        private bool CheckValid()
        {
            //判断诊断号
            if (this.txtCardNo.Text == null || this.txtCardNo.Text.Trim() == "")
            {
                MessageBox.Show("请输入病历号!", "提示");
                this.txtCardNo.Focus();
                return false;
            }

            //判断姓名
            if (this.txtName.Text == "")
            {
                MessageBox.Show("请填写姓名！");
                this.txtName.Focus();
                return false;
            }
            //判断科室
            if (this.cmbDept.Text == "" || this.cmbDept.Tag == null)
            {
                MessageBox.Show("请输入科室！");
                this.cmbDept.Focus();
                return false;
            }


            //判断患者类型// {67256EED-3DE6-4179-9C49-9A4D115DE309}
            if (this.cmbPatientType.Text == "" || this.cmbPatientType.Tag == null)
            {
                MessageBox.Show("请选择患者类型！");
                this.cmbPatientType.Focus();
                return false;
            }
            //科室长度
            //if (!FS.FrameWork.Public.String.ValidMaxLengh(this.cmbDept.Text, 16))
            //{
            //    MessageBox.Show("科室输入过长,请重新输入!");
            //    return false;
            //}
            if (this.cmbDept.Tag == null)
            {
                MessageBox.Show("请输入科室！");
                return false;
            }
            //判断合同单位
            //if (this.cmbPactUnit.Text == "" || this.cmbPactUnit.Tag == null)
            //{
            //    MessageBox.Show("请输入合同单位！");
            //    return false;
            //}
            //出生日期 
            if (this.dtBirthday.Value > this.bedMgr.GetDateTimeFromSysDateTime())
            {
                MessageBox.Show("出生日期大于当前日期,请重新输入!");
                this.dtBirthday.Focus();
                return false;
            }

            //判断性别
            if (this.cmbSex.Text == "" || this.cmbSex.Tag == null)
            {
                MessageBox.Show("请输入性别！");
                this.cmbSex.Focus();
                return false;
            }
            //判断字符超长联系人电话
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtLinkTel.Text, 20))
            {
                MessageBox.Show("联系人电话录入超长！");
                this.txtLinkTel.Focus();
                return false;
            }
            //家庭电话
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtHomeTel.Text, 20))
            {
                MessageBox.Show("家庭电话录入超长！");
                this.txtHomeTel.Focus();
                return false;
            }
            //身份证
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtIdentity.Text, 18))
            {
                MessageBox.Show("身份证号码录入超过18位！");
                this.txtIdentity.Focus();
                return false;
            }
            if (this.cmbPactUnit.Text == "" || this.cmbPactUnit.Tag == null)
            {
                MessageBox.Show("结算类别不能为空，" + "\n" + "请选择计算类别");
                this.cmbPactUnit.Focus();
                return false;
            }

            //婚姻状况
            if (this.cmbMarriage.Text == "" || this.cmbMarriage.Tag == null) 
            {
                MessageBox.Show("婚姻状况不能为空，" + "\n" + "请选择婚姻状况");
                this.cmbMarriage.Focus();
                return false;
            }

            //入院主要诊断
            if (this.txtInDiagnose.Text == "")
            {
                MessageBox.Show("入院主要诊断不能为空，" + "\n" + "请选择入院主要诊断");
                this.txtInDiagnose.Focus();
                return false;
            }


            return true;
        }

        /// <summary>
        /// 诊断控件事件
        /// </summary>
        private void LoadEvents()
        {
            //回车跳转焦点
            foreach (Control c in this.neuGroupBox1.Controls)
            {
                c.KeyDown += new KeyEventHandler(c_KeyDown);
            }
            foreach (Control c in this.neuGroupBox2.Controls)
            {
                c.KeyDown += new KeyEventHandler(c_KeyDown);
            }
            foreach (Control c in this.neuGroupBox3.Controls)
            {
                c.KeyDown += new KeyEventHandler(c_KeyDown);
            }

        }

        /// <summary>
        /// 从ui获得患者信息
        /// </summary>
        private void GetPrePatient()
        {
            if (this._myPatientInfo == null)
            {
                _myPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            }

            this._myPatientInfo.PID.CardNO = txtCardNo.Text;//病例号
            if (this.cmbDept.Tag != null)
            {
                this._myPatientInfo.PVisit.PatientLocation.Dept.ID = cmbDept.Tag.ToString();//住院科室
            }

            if (txtCardNo.Tag != null
                && !string.IsNullOrEmpty(txtCardNo.Tag.ToString()))
            {
                _myPatientInfo.User01 = txtCardNo.Tag.ToString();
            }

            if (this.cmbNurseCell.Tag != null)
            {
                //护士站
                this._myPatientInfo.PVisit.PatientLocation.NurseCell.ID = cmbNurseCell.Tag.ToString();
            }

            this._myPatientInfo.PVisit.PatientLocation.Dept.Name = this.cmbDept.Text;
            this._myPatientInfo.PVisit.InTime = this.dtPreDate.Value;//预约日期----------
            //患者类型
            this._myPatientInfo.PatientType.ID = this.cmbPatientType.Tag.ToString();
            this._myPatientInfo.PatientType.Name = this.cmbPatientType.Text;

            this._myPatientInfo.Name = this.txtName.Text;//姓名
            if (this.cmbSex.Tag != null)
            {
                this._myPatientInfo.Sex.ID = this.cmbSex.Tag.ToString();//性别
            }
            //if (this.cmbPactUnit.Tag != null)
            //    this._myPatientInfo.Pact.ID = this.cmbPactUnit.Tag.ToString();//合同单位

            // {D5390E6B-48F5-45d3-B956-3BC952BDE1EA}
            if (this.cmbPactUnit.Tag != null)
            {
                //this._myPatientInfo.Pact.PayKind.ID = this.cmbPayKind.Tag.ToString();//结算类别
                this._myPatientInfo.Pact.ID = this.cmbPactUnit.Tag.ToString();

                FS.HISFC.Models.Base.PactInfo p = new FS.HISFC.Models.Base.PactInfo();
                p = this.PactUnit.GetPactUnitInfoByPactCode(this.cmbPactUnit.Tag.ToString());
                this._myPatientInfo.Pact.PayKind.ID = p.PayKind.ID;
            }


            this._myPatientInfo.Birthday = this.dtBirthday.Value;//出生日期
            if (this.cmbMarriage.Tag != null)
                this._myPatientInfo.MaritalStatus.ID = this.cmbMarriage.Tag.ToString();//婚姻状况
            this._myPatientInfo.IDCard = this.txtIdentity.Text;//身份证号
            if (this.cmbPos.Tag != null)
                this._myPatientInfo.Profession.ID = this.cmbPos.Tag.ToString();//职业
            if (this.cmbHomePlace.Tag != null)
                this._myPatientInfo.AreaCode = this.cmbHomePlace.Tag.ToString();//出生地
            if (this.cmbCountry.Tag != null)
                this._myPatientInfo.Country.ID = this.cmbCountry.Tag.ToString();//国籍

            this._myPatientInfo.AddressHome = this.txtHomeAddr.Text;//家庭住址
            this._myPatientInfo.PhoneHome = this.txtHomeTel.Text;//家庭电话
            this._myPatientInfo.CompanyName = this.txtWorkUnit.Text;//工作单位
            this._myPatientInfo.Kin.Name = this.txtLinkMan.Text;//联系人
            this._myPatientInfo.Kin.RelationAddress = this.txtLinkManAddr.Text;//联系人住址
            this._myPatientInfo.Kin.RelationPhone = this.txtLinkTel.Text;//联系人电话
            this._myPatientInfo.PhoneBusiness = this.txtWorkTel.Text;//工作单位电话
            if (this.cmbNationality.Tag != null)
                this._myPatientInfo.Nationality.ID = this.cmbNationality.Tag.ToString();//民族
            if (this.cmbLinkManRel.Tag != null)
                this._myPatientInfo.Kin.Relation.ID = this.cmbLinkManRel.Tag.ToString();//联系人关系
            this._myPatientInfo.PVisit.PatientLocation.Bed.ID = string.Empty;//this.cmbBedNo.Tag.ToString();//病床号
            if (this.cmbPreDoc.Tag != null)
                this._myPatientInfo.PVisit.AdmittingDoctor.ID = this.cmbPreDoc.Tag.ToString();//预约医生
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            
            // {D5390E6B-48F5-45d3-B956-3BC952BDE1EA}
            if (this.txtInDiagnose.Tag != null)
            {
                obj.ID = this.txtInDiagnose.Tag.ToString();
                obj.Name = this.txtInDiagnose.Text;
            }
            else
            {
                obj.ID = "MS999";
                obj.Name = this.txtInDiagnose.Text;
            }
            this._myPatientInfo.Diagnoses.Add(obj);//入院诊断
            this._myPatientInfo.MSDiagnoses = this.neuTextBox1.Text;//描述诊断
            this._myPatientInfo.ClinicDiagnose = this.txtInDiagnose.Text; ;//门诊诊断名称
            this._myPatientInfo.SSN = this.txtSSN.Text;//医疗证号

            _myPatientInfo.FT.PrepayCost = this.numericUpDownForegift.Value;
            _myPatientInfo.Memo = this.txtMemo.Text;

            _myPatientInfo.PVisit.Circs.ID = this.cmbCircs.Tag.ToString();//入院情况
            _myPatientInfo.PVisit.Circs.Name = this.cmbCircs.Text;//入院情况
        }

        /// <summary>
        /// 取消预约登记
        /// </summary>
        /// <returns></returns>
        public int CancelPre()
        {
            if (this.neuTabControl1.SelectedIndex == 0)
            {
                MessageBox.Show("切换到查询页面!!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
            }

            if (this.fpMainInfo_Sheet1.Rows.Count == 0) return -1;
            string CarNo = this.fpMainInfo_Sheet1.Cells[this.fpMainInfo_Sheet1.ActiveRowIndex, 1].Text;
            string HappenNo = this.fpMainInfo_Sheet1.Cells[this.fpMainInfo_Sheet1.ActiveRowIndex, 0].Text;
            if (MessageBox.Show("确认要取消预约登记" + CarNo + "号？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                return -1;

            FS.HISFC.Models.RADT.PatientInfo patient = this.managerIntegrate.QueryPreInPatientInfoByCardNO(HappenNo, CarNo);
            if (patient.User02 == "2")
            {
                MessageBox.Show("该患者已办理住院登记，无法取消预约！");
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.managerIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            int iRet = this.managerIntegrate.UpdatePreInPatientState(CarNo, "1", HappenNo);
            if (iRet < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.Msg("取消预约登记失败" + this.managerIntegrate.Err, 211);
                iRet = -1;
            }
            if (iRet == 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("该条记录已被取消!");
                this.QueryData();
                iRet = -1;
            }
            if (iRet > 0)
            {

                #region addby xuewj 2010-3-15
                if (this._adt == null)
                {
                    this._adt = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IHE.IADT)) as FS.HISFC.BizProcess.Interface.IHE.IADT;
                }
                if (this._adt != null && patient != null)
                {
                    this._adt.CancelPreRegInpatient(patient);
                }
                #endregion

                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("取消成功!");
                this.QueryData();
                iRet = 1;
            }

            return iRet;
        }

        /// <summary>
        /// 根据合同单位标示返回支付类别名称
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        private FS.FrameWork.Models.NeuObject GetPactUnitByID(string strID)
        {
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

            PactInfo p = managerIntegrate.GetPactUnitInfoByPactCode(strID);
            if (p == null)
            {
                MessageBox.Show("检索合同单位出错" + this.managerIntegrate.Err, "提示");
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
        /// 打印
        /// </summary>
        public void Print()
        {
            if (this.neuTabControl1.SelectedTab != this.tabPage2) return;

            FS.FrameWork.WinForms.Classes.Print p = new Print();

            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                p.PrintPreview(this.Panel1);
            }
            else
            {
                p.PrintPage(0, 0, Panel1);
                p.PrintPage(0, 0, Panel1);
            }
        }

        /// <summary>
        /// 设置诊断控件显示
        /// </summary>
        void SetLocation()
        {
            if (!this.ucDiagnose1.Visible)
            {
                return;
            }
            this.ucDiagnose1.Top = this.neuGroupBox2.Top;
            this.ucDiagnose1.Left = this.txtInDiagnose.Left;
            this.ucDiagnose1.Visible = true;
        }

        /// <summary>
        /// 设置诊断value
        /// </summary>
        void SetValue()
        {
            this.txtInDiagnose.Text = this.icdMgr.Name;
            this.txtInDiagnose.Tag = this.icdMgr.ID;
            this.ucDiagnose1.Visible = false;

            cmbNurseCell.Focus();
        }

        #endregion

        #region 事件
        void ucPrepayIn_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;

            Init();
            LoadEvents();
            InitQuery();
        }

        /// <summary>
        /// 焦点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void c_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                System.Windows.Forms.SendKeys.Send("{tab}");
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                e.Handled = false;
            }

        }

        /// <summary>
        /// 键盘事件处理
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    {
                        if (this.ucDiagnose1.Visible)
                            this.ucDiagnose1.PriorRow();
                        break;
                    }
                case Keys.Down:
                    {
                        if (this.ucDiagnose1.Visible)
                            this.ucDiagnose1.NextRow();
                        break;
                    }
                case Keys.Escape:
                    {
                        if (this.ucDiagnose1.Visible)
                            this.ucDiagnose1.Visible = false;
                        break;
                    }
                case Keys.Space:
                    {
                        if (this.txtInDiagnose.ContainsFocus)
                        {
                            this.SetLocation();
                        }
                        break;
                    }

                case Keys.Enter:
                    {
                        if (this.txtInDiagnose.ContainsFocus)
                        {
                            if (this.ucDiagnose1.Visible)
                            {
                                if (ucDiagnose1_SelectItem(Keys.Enter) == 0)
                                {
                                    SetValue();
                                }
                            }
                        }
                        break;
                    }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (this.txtCardNo.Text.Trim() == "")
                {
                    return;
                }
                string txtCardNo = this.txtCardNo.Text.Trim();

                this.Clear();

                FS.HISFC.Models.Account.AccountCard accountCardObj = new FS.HISFC.Models.Account.AccountCard();
                if (this._feeIntegrate.ValidMarkNO(txtCardNo, ref accountCardObj) <= 0)
                {
                    MessageBox.Show(this._feeIntegrate.Err);
                    return;
                }
                ArrayList arrPrein = this.managerIntegrate.GetPrepayInByCardNoAndDate(accountCardObj.Patient.PID.CardNO);

                if (arrPrein.Count != null && arrPrein.Count > 0)// {6BF1F99D-7307-4d05-B747-274D24174895}
                {
                    string strMessTip = "该患者已经存在预约信息，如下：\r\n";
                    int i = 1;
                    foreach (FS.HISFC.Models.RADT.PatientInfo PatientInfo in arrPrein)
                    {
                         string doctName =  SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(PatientInfo.PVisit.AdmittingDoctor.ID);
                         string nurseDeptName = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(PatientInfo.PVisit.PatientLocation.NurseCell.ID);
                         strMessTip += i + ".预约日期：" + PatientInfo.PVisit.InTime.ToShortDateString() + ";   预约科室：" + PatientInfo.PVisit.PatientLocation.Dept.Name + ";   预约病区：" + nurseDeptName + ";   预约医生：" + doctName +"\r\n";
                         i++;
                    }

                    DialogResult dr = MessageBox.Show(strMessTip + "\r\n是否重新办理预约入院？", "提示", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.No)
                    {
                        this.neuTabControl1.SelectedTab = this.tabPage2;
                        for (int index = 0; index < this.fpMainInfo_Sheet1.RowCount; index ++)
                        {
                            string cardNo = this.fpMainInfo_Sheet1.Cells[index, 1].Text;
                            if (cardNo == accountCardObj.Patient.PID.CardNO)
                            {
                                this.fpMainInfo.ActiveSheet.ActiveColumnIndex = index;
                                break;
                            }
                        }
                        return;
                    }
                }
                //this.txtCardNo.Text = accountCardObj.Patient.PID.CardNO;
                //this.txtName.Text = accountCardObj.Patient.Name;
                //this.cmbSex.Tag = accountCardObj.Patient.Sex.ID;
                //this.dtBirthday.Value = accountCardObj.Patient.Birthday;
                //this.txtSSN.Text = accountCardObj.Patient.SSN;
                //this.txtHomeAddr.Text = accountCardObj.Patient.AddressHome;

                this.SetPrepatient(accountCardObj.Patient, true);
            }
        }

        private void cmbPactUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strID = this.cmbPactUnit.Tag.ToString();
            FS.HISFC.Models.Base.PactInfo pact = new FS.HISFC.Models.Base.PactInfo();// {D5390E6B-48F5-45d3-B956-3BC952BDE1EA}
            pact = this.PactUnit.GetPactUnitInfoByPactCode(strID);
            this.cmbPayKind.Tag = pact.PayKind.ID;
            //this.cmbPayKind.Text = pact.PayKind.Name;
        }

        /// <summary>
        /// 通过护士站过滤床位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbNurseCell_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArrayList arrBed = new ArrayList();
            string strID = this.cmbNurseCell.Tag.ToString();
            arrBed = bedMgr.GetUnoccupiedBed(this.cmbNurseCell.Tag.ToString());
            if (arrBed == null)
            {
                MessageBox.Show("查询护士站床位信息失败！\r\n\r\n" + bedMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (arrBed.Count == 0)
            {
                lblNoBed.Visible = true;
            }
            else
            {
                lblNoBed.Visible = false;
            }

            this.cmbBedNo.AddItems(arrBed);
        }

        private void fpMainInfo_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //当前行索引
            int iRow = e.Row;
            //获取发生序号
            string strNo = this.fpMainInfo_Sheet1.Cells[iRow, 0].Text.Trim();
            string strCardNo = this.fpMainInfo_Sheet1.Cells[iRow, 1].Text.Trim();
            //获得预约登记实体返回给属性
            this._myPatientInfo = this.managerIntegrate.QueryPreInPatientInfoByCardNO(strNo, strCardNo);
            this.SetPrepatient(_myPatientInfo, false);

            this.neuTabControl1.SelectedIndex = 0;
        }

        private void txtInDiagnose_TextChanged(object sender, EventArgs e)
        {
            if (this.ckShow.Checked)// {9B75C463-0167-41d4-B42E-DB869D5EFC11}
            {
                ucDiagnose1.Visible = false;
            }
            else
            {
                ucDiagnose1.Visible = true;
            }
            if (string.IsNullOrEmpty(this.txtInDiagnose.Text))
            {
                ucDiagnose1.Visible = false;
            }
            this.SetLocation();
            if (ucDiagnose1.Visible)
            {
                this.ucDiagnose1.Filter(this.txtInDiagnose.Text.Trim());
            }
        }

        private int ucDiagnose1_SelectItem(Keys key)
        {
            int result = this.ucDiagnose1.GetItem(ref icdMgr);
            if (result < 0) return -1;
            SetValue();
            return 1;
        }

        private void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedIndex == 0)
            {
                this.txtCardNo.Focus();
                this.txtCardNo.SelectAll();
            }
        }

        #endregion

        #region 查询
        /// <summary>
        /// 初始化DataTable
        /// </summary>
        private void SetDataTable()
        {
            this.fpMainInfo_Sheet1.RowCount = 0;

            Type str = typeof(String);
            Type date = typeof(DateTime);

            Type dec = typeof(Decimal);
            Type bo = typeof(bool);

            _dtPrepayIn.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("发生序号", str),
                new DataColumn("病历号", str),
                new DataColumn("患者姓名", str),
                new DataColumn("性别", str),
                new DataColumn("合同单位", str),
			    new DataColumn("住院科室", str),
			    new DataColumn("预约日期", str),
			    new DataColumn("当前状态", str),															
			    new DataColumn("家庭地址", str),
			    new DataColumn("家庭电话", str),
			    new DataColumn("联系人", str),
			    new DataColumn("联系人电话", str),
			    new DataColumn("联系人地址", str),
			    new DataColumn("操作员", str),
			    new DataColumn("操作时间", str),
            });
        }

        private void InitQuery()
        {
            SetDataTable();
            QueryData();
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        public void QueryData()
        {
            string PrepayinState = this.GetState();
            string Begin = this.dtBegin.Value.ToShortDateString() + " 00:00:00";
            string End = this.dtEnd.Value.ToShortDateString() + " 23:59:59";

            this.QueryData(PrepayinState, Begin, End);
        }

        /// <summary>
        /// 根据预约状态和时间查找数据
        /// </summary>
        /// <param name="PrepayinState"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        private void QueryData(string PrepayinState, string begin, string end)
        {
            this._dtPrepayIn.Clear();

            ArrayList arrPrein = this.managerIntegrate.QueryPreInPatientInfoByDateAndState(PrepayinState, begin, end);
            if (arrPrein == null) return;

            string strName = "", strStateName = "";
            foreach (FS.HISFC.Models.RADT.PatientInfo obj in arrPrein)
            {
                #region 取性别名称
                switch (obj.Sex.ID.ToString())
                {
                    case "U":
                        strName = "未知";
                        break;
                    case "M":
                        strName = "男";
                        break;
                    case "F":
                        strName = "女";
                        break;
                    case "O":
                        strName = "其它";
                        break;
                    default:
                        break;
                }
                #endregion

                #region 登记状态
                switch (obj.User02.ToString())
                {
                    case "0":
                        strStateName = "预约登记";
                        break;
                    case "1":
                        strStateName = "取消预约登记";
                        break;
                    case "2":
                        strStateName = "预约转住院";
                        break;
                    default:
                        break;
                }
                #endregion

                #region 取合同单位、操作员名称

                if (!string.IsNullOrEmpty(obj.Pact.ID))
                {
                    PactInfo pactInfo = SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(obj.Pact.ID);
                    if (null != pactInfo) obj.Pact.Name = pactInfo.Name;
                }
                string strOperID = obj.User03.Substring(0, 6);
                string strOperName = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(strOperID);

                #endregion

                #region 向DataTable插入数据
                DataRow row = this._dtPrepayIn.NewRow();
                row["发生序号"] = obj.User01;
                row["病历号"] = obj.PID.CardNO;
                row["患者姓名"] = obj.Name;
                row["性别"] = strName;
                row["合同单位"] = obj.Pact.Name;//需转换
                row["住院科室"] = obj.PVisit.PatientLocation.Dept.Name;
                row["预约日期"] = obj.PVisit.InTime;//.Date_In;
                row["当前状态"] = strStateName;//需转换
                row["家庭地址"] = obj.AddressHome;
                row["家庭电话"] = obj.PhoneHome;
                row["联系人"] = obj.Kin.ID;
                row["联系人电话"] = obj.Kin.Memo;
                row["联系人地址"] = obj.Kin.User01;
                row["操作员"] = strOperName;
                row["操作时间"] = obj.User03.Substring(6, 10);

                this._dtPrepayIn.Rows.Add(row);
                #endregion
            }

            _dvPrepayIn = new DataView(this._dtPrepayIn);
            this.fpMainInfo_Sheet1.DataSource = this._dvPrepayIn;
            this.initFp();
        }

        /// <summary>
        /// 控制fp宽度
        /// </summary>
        private void initFp()
        {
            int im = 3;
            this.fpMainInfo_Sheet1.OperationMode = (FarPoint.Win.Spread.OperationMode)im;
            this.fpMainInfo_Sheet1.Columns.Get(0).Width = 0F;
            this.fpMainInfo_Sheet1.Columns.Get(1).Width = 100F;
            this.fpMainInfo_Sheet1.Columns.Get(2).Width = 72F;
            this.fpMainInfo_Sheet1.Columns.Get(3).Width = 48F;
            this.fpMainInfo_Sheet1.Columns.Get(5).Width = 88F;
            this.fpMainInfo_Sheet1.Columns.Get(6).Width = 100F;
            this.fpMainInfo_Sheet1.Columns.Get(9).Width = 95F;
            this.fpMainInfo_Sheet1.Columns.Get(10).Width = 102F;
            this.fpMainInfo_Sheet1.Columns.Get(11).Width = 127F;
            this.fpMainInfo_Sheet1.Columns.Get(12).Width = 85F;

            this.fpMainInfo_Sheet1.Columns.Get(14).Width = 85F;
        }

        /// <summary>
        /// 查看当前查询的状态
        /// </summary>
        /// <returns></returns>
        private string GetState()
        {
            string state = string.Empty;
            if (this.RbtPrePatient.Checked)
            {
                state = "0";
            }
            if (this.RbtCancelPre.Checked)
            {
                state = "1";
            }
            if (this.RbtChange.Checked)
            {
                state = "2";
            }
            return state;
        }

        /// <summary>
        /// 更改预约状态查询数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbtCheckChange(object sender, EventArgs e)
        {
            string PrepayinState = this.GetState();

            string Begin = this.dtBegin.Value.ToShortDateString() + " 00:00:00";
            string End = this.dtEnd.Value.ToShortDateString() + " 23:59:59";

            this.QueryData(PrepayinState, Begin, End);
        }

        #endregion


        /// <summary>
        /// 预览入院通知单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int PrintPreview(object sender, object neuObject)
        {
            string strNO = this.fpMainInfo_Sheet1.Cells[this.fpMainInfo_Sheet1.ActiveRowIndex, 0].Text;
            string cardNO = this.fpMainInfo_Sheet1.Cells[this.fpMainInfo_Sheet1.ActiveRowIndex, 1].Text;
            FS.HISFC.Models.RADT.PatientInfo p = this.managerIntegrate.QueryPreInPatientInfoByCardNO(strNO, cardNO);

            if (p == null)
            {
                MessageBox.Show("打印入院通知单时，查询患者预约信息失败。\n" + this.managerIntegrate.Err);
                return -1;
            }

            this._iPrintInHosNotice.SetValue(p);
            this._iPrintInHosNotice.PrintView();

            return base.PrintPreview(sender, neuObject);
        }

        public int InitInterface()
        {
            //_iPrintInHosNotice = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IPrintInHosNotice)) as FS.HISFC.BizProcess.Interface.IPrintInHosNotice;
            _iPrintInHosNotice = new ucOutPatientNoticeIBORN();
            return 1;
        }

        private int PrintNotice()
        {
            string strNO = this.fpMainInfo_Sheet1.Cells[this.fpMainInfo_Sheet1.ActiveRowIndex, 0].Text;
            string cardNO = this.fpMainInfo_Sheet1.Cells[this.fpMainInfo_Sheet1.ActiveRowIndex, 1].Text;
            FS.HISFC.Models.RADT.PatientInfo p = this.managerIntegrate.QueryPreInPatientInfoByCardNO(strNO, cardNO);

            if (p == null)
            {
                MessageBox.Show("打印入院通知单时，查询患者预约信息失败。\n" + this.managerIntegrate.Err);
                return -1;
            }

            this._iPrintInHosNotice.SetValue(p);
            this._iPrintInHosNotice.Print();

            return 1;
        }

        #region IInterfaceContainer 成员
        public Type[] InterfaceTypes
        {
            get
            {
                Type[] type = new Type[1];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.IPrintInHosNotice);

                return type;
            }
        }

        #endregion
        /// <summary>
        /// 根据科室找对应的病区
        /// </summary>
        /// <param name="deptStatCode"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public static ArrayList QueryNurseByDept(string deptCode)
        {
            FS.HISFC.BizLogic.Manager.DepartmentStatManager deptStatMgr = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();

            ArrayList alNurse = new ArrayList();
            ArrayList al = deptStatMgr.LoadByParent("01", deptCode);
            if (al == null || al.Count == 0)
            {
                al = deptStatMgr.LoadByChildren("01", deptCode);
                if (al != null)
                {
                    foreach (FS.HISFC.Models.Base.DepartmentStat deptStat in al)
                    {
                        alNurse.Add(new FS.FrameWork.Models.NeuObject(deptStat.PardepCode, deptStat.PardepName, ""));
                    }
                }
            }
            else
            {
                foreach (FS.HISFC.Models.Base.DepartmentStat deptStat in al)
                {
                    alNurse.Add(new FS.FrameWork.Models.NeuObject(deptStat.DeptCode, deptStat.DeptName, ""));
                }
            }
            return alNurse;
        }
        /// <summary>
        ///  护士站列表随科室变化 {E9EC275C-F044-40f1-BDDA-0F17410983EB}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbNurseCell.ClearItems();

            ArrayList alNurseCell = new ArrayList();

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = this.cmbDept.Tag.ToString();
            //alNurseCell = this.managerIntegrate.QueryNurseStationByDept(obj);

            alNurseCell = QueryNurseByDept(obj.ID);

            if (alNurseCell != null
                && alNurseCell.Count != 0)
            {
                this.cmbNurseCell.AddItems(alNurseCell);
                cmbNurseCell.SelectedIndex = 0;
            }
            else
            {
                cmbNurseCell.Text = "";

                cmbNurseCell.Tag = null;
            }
        }

        private void btSave_Click(object sender, EventArgs e)// {F6204EF5-F295-4d91-B81A-736A268DD394}
        {
            this.Save();
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否打印入院通知单？", "提示", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                this.PrintNotice();
            }

        }

        private void btQuery_Click(object sender, EventArgs e)
        {
            this.QueryData();
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)// {9B75C463-0167-41d4-B42E-DB869D5EFC11}
        {

            if (e.KeyData == Keys.Enter)
            {

                FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions frmQuery = new FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions();

                string QueryStr = this.txtName.Text;

                if (string.IsNullOrEmpty(QueryStr))
                {
                    return;
                }

                frmQuery.QueryByName(QueryStr);
                frmQuery.ShowDialog();

                if (frmQuery.DialogResult == DialogResult.OK)
                {
                    this.txtCardNo.Text = frmQuery.PatientInfo.PID.CardNO;
                    txtCardNo_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                }
            }
        }

        private void ckShow_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckShow.Checked)
            {
                this.ucDiagnose1.Visible = false;
            }
        }

        private void txtInDiagnose_TextChanged_1(object sender, EventArgs e)
        {

        }





    }
}
