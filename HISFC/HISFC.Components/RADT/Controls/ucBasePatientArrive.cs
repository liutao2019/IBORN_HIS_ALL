using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.RADT;
namespace FS.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// 接诊，转入，召回等基础控件
    /// </summary>
    public partial class ucBasePatientArrive : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucBasePatientArrive()
        {
            InitializeComponent();
            if (IPrintInHosNotice == null)
            {
                IPrintInHosNotice = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IPrintInHosNotice)) as FS.HISFC.BizProcess.Interface.IPrintInHosNotice;
            }

            this.AutoScroll = true;
        }

        /// <summary>
        /// 出院通知单打印接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.IPrintInHosNotice IPrintInHosNotice = null;

        #region 变量

        /// <summary>
        /// 综合管理业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        //protected FS.HISFC.BizLogic.Manager.Constant managerConstant = new FS.HISFC.BizLogic.Manager.Constant();

        FS.HISFC.BizLogic.RADT.InPatient Inpatient = new FS.HISFC.BizLogic.RADT.InPatient();

        FS.HISFC.BizProcess.Integrate.RADT managerRADT = new FS.HISFC.BizProcess.Integrate.RADT();
       
        /// <summary>
        /// 床位日报处理
        /// </summary>
        FS.HISFC.BizLogic.RADT.InpatientDayReport dayReportMgr = new FS.HISFC.BizLogic.RADT.InpatientDayReport();

        /// <summary>
        /// 当前患者
        /// </summary>
        protected FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// 是否存在科室病区对应关系
        /// </summary>
        bool isNurseDept = false;//{F044FCF3-6736-4aaa-AA04-4088BB194C20}
        /// <summary>
        /// 住院安排床位时，是否允许选择全院医生：0-本科室医生，1-全院医生，2-有登录权限医生
        /// {35CAF42E-C189-4f52-A9FC-EBA030CFBCCE}
        /// </summary>
        string strAllDoc = "0";

        /// <summary>
        /// 待分配的床号
        /// </summary>
        protected string arrangeBedNO;

        /// <summary>
        /// 接诊类型
        /// </summary>
        public EnumArriveType Arrivetype
        {
            set
            {
                arrivetype = value;
            }
            get
            {
                return arrivetype;
            }
        }

        /// <summary>
        /// 接诊类型
        /// </summary>
        private EnumArriveType arrivetype;
        
        /// <summary>
        /// 护士站
        /// </summary>
        protected FS.FrameWork.Models.NeuObject NurseCell = null;

        /// <summary>
        /// 科室人员列表
        /// </summary>
        protected Hashtable hsDeptEmplList = new Hashtable();

        /// <summary>
        /// 当前操作员
        /// </summary>
        FS.HISFC.Models.Base.Employee empl;

        /// <summary>
        /// 是否允许隔日召回
        /// </summary>
        private bool isAllowCallBackOtherDay = false;

        /// <summary>
        /// 是否允许隔日召回
        /// </summary>
        [Category("出院召回"), Description("是否允许隔日召回")]
        public bool IsAllowCallBackOtherDay
        {
            get
            {
                return isAllowCallBackOtherDay;
            }
            set
            {
                isAllowCallBackOtherDay = value;
            }
        }

        /// <summary>
        /// 是否允许修改床位等级
        /// </summary>
        private bool isAllowModifyBedGrad = false;

        /// <summary>
        /// 是否允许修改床位等级
        /// </summary>
        public bool IsAllowModifyBedGrad
        {
            get
            {
                return isAllowModifyBedGrad;
            }
            set
            {
                isAllowModifyBedGrad = value;

                this.cmbBedLevl.Enabled = isAllowModifyBedGrad;
            }
        }

        /// <summary>
        /// 住院接诊、召回、转入、换医师接口
        /// </summary>
        private FS.SOC.HISFC.BizProcess.NurseInterface.IArriveBase IArriveBase = null;

        /// <summary>
        /// 床位列表帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper bedListHelper = null;

        #endregion

        #region 属性

        /// <summary>
        /// 待分配的床号
        /// </summary>
        public string ArrangeBedNO
        {
            get
            {
                return arrangeBedNO;
            }
            set
            {
                arrangeBedNO = value;
            }
        }

        /// <summary>
        /// 住院医师是否必填
        /// </summary>
        bool isAdmittingDoctorNeed = false;

        /// <summary>
        /// 住院医师是否必填
        /// </summary>
        public bool IsAdmittingDoctorNeed
        {
            get
            {
                return isAdmittingDoctorNeed;
            }
            set
            {
                isAdmittingDoctorNeed = value;
            }
        }

        /// <summary>
        /// 主治医师是否必填
        /// </summary>
        bool isAttendingDoctorNeed = false;

        /// <summary>
        /// 主治医师是否必填
        /// </summary>
        public bool IsAttendingDoctorNeed
        {
            get
            {
                return isAttendingDoctorNeed;
            }
            set
            {
                isAttendingDoctorNeed = value;
            }
        }

        /// <summary>
        /// 主任医师是否必填
        /// </summary>
        bool isConsultingDoctorNeed = false;

        /// <summary>
        /// 主任医师是否必填
        /// </summary>        
        public bool IsConsultingDoctorNeed
        {
            get
            {
                return isConsultingDoctorNeed;
            }
            set
            {
                isConsultingDoctorNeed = value;
            }
        }

        /// <summary>
        /// 科主任是否必填
        /// </summary>
        bool isDirectorDoctorNeed = false;

        /// <summary>
        /// 科主任是否必填
        /// </summary>        
        public bool IsDirectorDoctorNeed
        {
            get
            {
                return isDirectorDoctorNeed;
            }
            set
            {
                isDirectorDoctorNeed = value;
            }
        }

        /// <summary>
        /// 责任护士是否必填
        /// </summary>
        bool isAdmittingNurseNeed = false;

        /// <summary>
        /// 责任护士是否必填
        /// </summary>        
        public bool IsAdmittingNurseNeed
        {
            get
            {
                return isAdmittingNurseNeed;
            }
            set
            {
                isAdmittingNurseNeed = value;
            }
        }

        /// <summary>
        /// 全部医生人员列表
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper allDoctHelper = null;

        /// <summary>
        /// 全部护士人员列表
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper allNurseHelper = null;

        /// <summary>
        /// 本科室医生人员列表
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper privDoctHelper = null;

        /// <summary>
        /// 本病区护士人员列表
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper privNurseHelper = null;

        /// <summary>
        /// 接诊时是否自动加载收治医师或转科前医师
        /// </summary>
        private bool isAddDoctWhenArrive = true;

        /// <summary>
        /// 接诊时是否自动加载收治医师或转科前医师
        /// </summary>
        public bool IsAddDoctWhenArrive
        {
            get
            {
                return isAddDoctWhenArrive;
            }
            set
            {
                isAddDoctWhenArrive = value;
            }
        }

        /// <summary>
        /// 是否默认加载全院医生
        /// </summary>
        private bool isAddAllDoct = true;

        /// <summary>
        /// 是否默认加载全院医生
        /// </summary>
        public bool IsAddAllDoct
        {
            get
            {
                return isAddAllDoct;
            }
            set
            {
                isAddAllDoct = value;
                this.cbxAddAllDoct.Checked = isAddAllDoct;
            }
        }
        /// <summary>
        /// 是否允许修改接诊时间// {3B757263-9BE5-4e5a-9AD2-0815E9A210C7}
        /// </summary>
        private bool isCanEditArriveInTime = true;

        /// <summary>
        /// 是否允许修改接诊时间// {3B757263-9BE5-4e5a-9AD2-0815E9A210C7}
        /// </summary>
        public bool IsCanEditArriveInTime
        {
            get
            {
                return isCanEditArriveInTime;
            }
            set
            {
                isCanEditArriveInTime = value;
            }
        }

        /// <summary>
        /// 是否默认加载全院护士
        /// </summary>
        private bool isAddAllNurse = true;

        /// <summary>
        /// 是否默认加载全院护士
        /// </summary>
        public bool IsAddAllNurse
        {
            get
            {
                return isAddAllNurse;
            }
            set
            {
                isAddAllNurse = value;
                this.cbxAddAllNurse.Checked = isAddAllNurse;
            }
        }

        #endregion

        #region 方法

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            NurseCell = empl.Nurse.Clone();

            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            //是否存在科室病区对应关系{F044FCF3-6736-4aaa-AA04-4088BB194C20}
            //isNurseDept = controlIntegrate.GetControlParam<bool>("900010", true, false);
            //strAllDoc = controlIntegrate.GetControlParam<string>("ZY0003", false, "0");

            ArrayList alDepts = new ArrayList();
            ArrayList alTemp = new ArrayList();

            alDepts = managerIntegrate.QueryDepartment(empl.Nurse.ID);
            if (alDepts == null)
            {
                MessageBox.Show("获取病区对应的科室列表错误：\r\n" + managerIntegrate.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }

            allDoctHelper = new FS.FrameWork.Public.ObjectHelper();
            alTemp = managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (alTemp == null)
            {
                MessageBox.Show("查询人员列表错误：\r\n" + managerIntegrate.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }
            allDoctHelper.ArrayObject = alTemp;

            allNurseHelper = new FS.FrameWork.Public.ObjectHelper();
            alTemp = managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.N);
            if (alTemp == null)
            {
                MessageBox.Show("查询人员列表错误：\r\n" + managerIntegrate.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }
            allNurseHelper.ArrayObject = alTemp;

            privDoctHelper = new FS.FrameWork.Public.ObjectHelper();
            foreach (FS.FrameWork.Models.NeuObject dept in alDepts)
            {
                alTemp = managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D, dept.ID);
                if (alTemp == null)
                {
                    MessageBox.Show("查询人员列表错误：\r\n" + managerIntegrate.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //return;
                }
                privDoctHelper.ArrayObject.AddRange(alTemp);
            }

            privNurseHelper = new FS.FrameWork.Public.ObjectHelper();
            foreach (FS.FrameWork.Models.NeuObject dept in alDepts)
            {
                alTemp = managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.N, dept.ID);
                if (alTemp == null)
                {
                    MessageBox.Show("查询人员列表错误：\r\n" + managerIntegrate.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //return;
                }
                privNurseHelper.ArrayObject.AddRange(alTemp);
            }

            //加载医师时读取复选框属性
            if (this.cbxAddAllDoct.Checked == true)
            {
                //加载住院医生列表
                this.cmbDoct.AddItems(allDoctHelper.ArrayObject);
                //加载主治医生列表
                this.cmbAttendingDoc.AddItems(allDoctHelper.ArrayObject);
                //加载主任医生列表
                this.cmbConsultingDoc.AddItems(allDoctHelper.ArrayObject);
                //加载科主任列表
                this.cmbDirector.AddItems(allDoctHelper.ArrayObject);
                //加载责任医师列表
                this.cmbResponsibleDoc.AddItems(allDoctHelper.ArrayObject);
            }
            else
            {
                //加载住院医生列表
                this.cmbDoct.AddItems(privDoctHelper.ArrayObject);
                //加载主治医生列表
                this.cmbAttendingDoc.AddItems(privDoctHelper.ArrayObject);
                //加载主任医生列表
                this.cmbConsultingDoc.AddItems(privDoctHelper.ArrayObject);
                //加载科主任列表
                this.cmbDirector.AddItems(privDoctHelper.ArrayObject);
                //加载责任医师列表
                this.cmbResponsibleDoc.AddItems(privDoctHelper.ArrayObject);
            }

            //加载护士时读取复选框属性
            if (this.cbxAddAllNurse.Checked == true)
            {

                this.cmbAdmittingNur.AddItems(allNurseHelper.ArrayObject);
                this.cmbAdmittingNur.AppendItems(managerIntegrate.GetDeliverEmployee());  //{0a849cd8-db12-48e0-97ff-0b34f287c0a0}
              
            }
            else
            {
                this.cmbAdmittingNur.AddItems(privNurseHelper.ArrayObject);
            }

            if (bedListHelper == null)
            {
                bedListHelper = new FS.FrameWork.Public.ObjectHelper();
                bedListHelper.ArrayObject = managerIntegrate.QueryBedList(empl.Nurse.ID);
            }

            //加载床位列表
            if (this.arrivetype == EnumArriveType.ChangeDoct)
            {
                //换医生时,显示全部床位
                this.cmbBedNo.AddItems(bedListHelper.ArrayObject);
            }
            else
            {
                //接珍时,只显示空床
                this.cmbBedNo.AddItems(managerIntegrate.QueryUnoccupiedBed(empl.Nurse.ID));
            }

            //血型
            //this.cmbBloodType.AddItems(this.managerConstant.GetAllList("BloodKindCode"));
            this.cmbBloodType.AddItems(FS.HISFC.Models.RADT.BloodTypeEnumService.List());	//取血型列表

            this.cmbBedLevl.AddItems(managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.BEDGRADE));

            this.cmbDoct.IsListOnly = true;
            this.cmbBedNo.IsListOnly = true;
            this.cmbAdmittingNur.IsListOnly = true;
            this.cmbAttendingDoc.IsListOnly = true;
            this.cmbConsultingDoc.IsListOnly = true;
            this.cmbDirector.IsListOnly = true;
            this.cmbBedLevl.IsListOnly = true;
            this.cmbResponsibleDoc.IsListOnly = true;//责任医师

            if (this.IArriveBase == null)
            {
                IArriveBase = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.BizProcess.NurseInterface.IArriveBase)) as FS.SOC.HISFC.BizProcess.NurseInterface.IArriveBase;
            }

            this.SetTendCombox();

            if (this.arrivetype == EnumArriveType.CallBack)
            {
                this.btOutBillPrint.Visible = true;
            }
            else
            {
                this.btOutBillPrint.Visible = false;
            }

            this.cbxAddAllDoct.CheckedChanged += new EventHandler(cbxAddAllDoct_CheckedChanged);
            this.cbxAddAllNurse.CheckedChanged += new EventHandler(cbxAddAllNurse_CheckedChanged);
            
            this.cmbBedNo.SelectedIndexChanged += new EventHandler(cmbBedNo_SelectedIndexChanged);

            this.cmbBedLevl.SelectedIndexChanged += new EventHandler(cmbBedLevl_SelectedIndexChanged);


            this.lblDoct.ForeColor = isAdmittingDoctorNeed ? Color.Blue : Color.Black;
            this.lblAttendingDoc.ForeColor = isAttendingDoctorNeed ? Color.Blue : Color.Black;
            this.lblConsultingDoc.ForeColor = isConsultingDoctorNeed ? Color.Blue : Color.Black;
            this.lblDirector.ForeColor = isDirectorDoctorNeed ? Color.Blue : Color.Black;
            this.lblAdmittingNur.ForeColor = isAdmittingNurseNeed ? Color.Blue : Color.Black;
            this.dtpInTime.Value = this.managerIntegrate.GetDateTimeFromSysDateTime();
            if (this.isCanEditArriveInTime)// {3B757263-9BE5-4e5a-9AD2-0815E9A210C7}
            {
                this.dtpInTime.Enabled = true;
            }
            else
            {
                this.dtpInTime.Enabled = false;
            }
            return null;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.cmbBedNo.Focused)
                {
                    this.cmbDoct.Focus();
                    return true;
                }
                else if (this.cmbDoct.Focused)
                {
                    this.cmbAttendingDoc.Focus();
                    return true;
                }
                else if (this.cmbAttendingDoc.Focused)
                {
                    this.cmbConsultingDoc.Focus();
                    return true;
                }
                else if (this.cmbConsultingDoc.Focused)
                {
                    cmbAdmittingNur.Focus();
                    return true;
                }
                else if (this.cmbAdmittingNur.Focused)
                {
                    this.cmbResponsibleDoc.Focus();//责任医师
                    return true;
                }
                else if (this.cmbResponsibleDoc.Focused)
                {
                    cmbTend.Focus();
                    return true;
                }
                else if (cmbTend.Focused)
                {
                    cmbDiet.Focus();
                    return true;
                }
                else if (cmbDiet.Focused)
                {
                    if (cmbDirector.Visible)
                    {
                        cmbDirector.Focus();
                        return true;
                    }
                    else
                    {
                        this.btOK.Focus();
                        return true;
                    }
                }
                else if (cmbDirector.Focused)
                {
                    this.btOK.Focus();
                    return true;
                }
                else if (btOK.Focused)
                {
                    this.neuButton1_Click(new object(), null);
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        void cbxAddAllNurse_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbxAddAllNurse.Checked)
            {
                this.cmbAdmittingNur.AddItems(allNurseHelper.ArrayObject);
                this.cmbAdmittingNur.AppendItems(managerIntegrate.GetDeliverEmployee());  ////{0a849cd8-db12-48e0-97ff-0b34f287c0a0}
            }
            else
            {
                this.cmbAdmittingNur.AddItems(privNurseHelper.ArrayObject);
            }
            this.SetPatientInfo(this.patientInfo);
        }

        void cbxAddAllDoct_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbxAddAllDoct.Checked)
            {
                //加载住院医生列表
                this.cmbDoct.AddItems(allDoctHelper.ArrayObject);
                //加载主治医生列表
                this.cmbAttendingDoc.AddItems(allDoctHelper.ArrayObject);
                //加载主任医生列表
                this.cmbConsultingDoc.AddItems(allDoctHelper.ArrayObject);
                //加载科主任列表
                this.cmbDirector.AddItems(allDoctHelper.ArrayObject);
                //加载责任医师列表
                this.cmbResponsibleDoc.AddItems(allDoctHelper.ArrayObject);
            }
            else
            {
                //加载住院医生列表
                this.cmbDoct.AddItems(privDoctHelper.ArrayObject);
                //加载主治医生列表
                this.cmbAttendingDoc.AddItems(privDoctHelper.ArrayObject);
                //加载主任医生列表
                this.cmbConsultingDoc.AddItems(privDoctHelper.ArrayObject);
                //加载科主任列表
                this.cmbDirector.AddItems(privDoctHelper.ArrayObject);
                //加载责任医师列表
                this.cmbResponsibleDoc.AddItems(privDoctHelper.ArrayObject);
            }
            this.SetPatientInfo(this.patientInfo);
        }

        /// <summary>
        /// 护理级别和饮食
        /// </summary>
        private void SetTendCombox()
        {
           ArrayList alTend=  managerIntegrate.GetConstantList("TENDLEVEL");
           this.cmbTend.AddItems(alTend);

           ArrayList alDiet = managerIntegrate.GetConstantList("DIET");
           this.cmbDiet.AddItems(alDiet);
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.RefreshList(((FS.HISFC.Models.RADT.PatientInfo)neuObject).ID);
            return base.OnSetValue(neuObject, e);
        }
       
        /// <summary>
        /// 将患者信息显示到控件上
        /// </summary>
        /// <param name="PatientInfo"></param>
        private void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo PatientInfo)
        {
            ClearPatintInfo();

            this.txtPatientNo.Text = PatientInfo.PID.PatientNO;
            this.txtPatientNo.Tag = PatientInfo.ID;
            this.txtName.Text = PatientInfo.Name;
            this.txtSex.Text = PatientInfo.Sex.Name;
            //添加科室与病区 add by zhy
            this.txtPatientDept.Text = PatientInfo.PVisit.PatientLocation.Dept.Name;
            this.txtPatientNueseCell.Text = PatientInfo.PVisit.PatientLocation.NurseCell.Name;

            this.cmbBloodType.Tag = PatientInfo.BloodType.ID;

            this.cmbDoct.Tag = PatientInfo.PVisit.AdmittingDoctor.ID;

            this.cmbAttendingDoc.Tag = PatientInfo.PVisit.AttendingDoctor.ID;

            this.cmbConsultingDoc.Tag = PatientInfo.PVisit.ConsultingDoctor.ID;

            this.cmbAdmittingNur.Tag = PatientInfo.PVisit.AdmittingNurse.ID;

            this.cmbDirector.Tag = PatientInfo.PVisit.AttendingDirector.ID;

            this.cmbResponsibleDoc.Tag = PatientInfo.PVisit.ResponsibleDoctor.ID;//责任医师

            PatientInfo.Disease.Memo = this.Inpatient.GetFoodName(PatientInfo.ID);

            this.cmbDiet.Text = PatientInfo.Disease.Memo;
            this.cmbTend.Tag = PatientInfo.Disease.Tend.ID;
            this.cmbTend.Text = PatientInfo.Disease.Tend.Name;

            if (this.arrivetype == EnumArriveType.ShiftIn)
            {
                this.cmbBedNo.Text = "";
                this.cmbBedNo.Tag = "";
            }
            else
            {
                this.cmbBedNo.Text = PatientInfo.PVisit.PatientLocation.Bed.ID.Length > 4 ? PatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) : "";
                this.cmbBedNo.Tag = PatientInfo.PVisit.PatientLocation.Bed.ID;
            }

            //{C29C2D37-D519-49af-AFEA-4981B1A013AE}
            if (this.arrangeBedNO != null)
            {
                this.cmbBedNo.Tag = this.arrangeBedNO;
            }

            //换医生或者婴儿召回不用选择床位,跟妈妈相同
            //if (PatientInfo.IsBaby || this.arrivetype == EnumArriveType.ChangeDoct)
            //{
            //    this.cmbBedNo.Enabled = false;
            //}
            //{DF72A3CF-38E6-4616-8287-DC989A4155F9} 婴儿转科
            if (this.arrivetype == EnumArriveType.ChangeDoct)
            {
                this.cmbBedNo.Enabled = false;
            }
            else
            {
                if (PatientInfo.IsBaby)// {7FFE7A7E-239D-4019-97B4-D3F80BB79713}
                {
                    this.cmbBedNo.Enabled = false;
                }
                else
                {
                    this.cmbBedNo.Enabled = true;
                }
            }

            //baby才可以编辑责任医师选择框
            if (PatientInfo.IsBaby)
            {
                this.cmbResponsibleDoc.Enabled = true;
                this.lblResponsibeDoc.ForeColor = Color.Blue;
            }
            else
            {
                this.cmbResponsibleDoc.Enabled = false;
                this.lblResponsibeDoc.ForeColor = Color.Black;
            }

            if (cmbBedNo.Tag != null && !string.IsNullOrEmpty(cmbBedNo.Tag.ToString()))
            {
                this.cmbBedLevl.Tag = (bedListHelper.GetObjectFromID(cmbBedNo.Tag.ToString()) as FS.HISFC.Models.Base.Bed).BedGrade.ID;
                ShowBedFee();
            }

            if (this.arrivetype == EnumArriveType.Accepts)
            {
                if (!this.isAddDoctWhenArrive)
                {
                    this.cmbDoct.Tag = null;

                    this.cmbAttendingDoc.Tag = null;

                    this.cmbConsultingDoc.Tag = null;

                    this.cmbAdmittingNur.Tag = null;

                    this.cmbDirector.Tag = null;

                    this.cmbResponsibleDoc.Tag = null;//责任医师
                }
            }
        }

        /// <summary>
        /// 获得患者基本信息从控件到PatientInfo
        /// </summary>
        /// <param name="PatientInfo"></param>
        private void GetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            //取控件中住院医生
            if (this.cmbDoct.Tag != null && !string.IsNullOrEmpty(this.cmbDoct.Tag.ToString()))
            {
                patientInfo.PVisit.AdmittingDoctor.ID = this.cmbDoct.Tag.ToString();
                patientInfo.PVisit.AdmittingDoctor.Name = this.cmbDoct.Text;
            }
            else
            {
                patientInfo.PVisit.AdmittingDoctor.ID = "";
                patientInfo.PVisit.AdmittingDoctor.Name = "";
            }

            //取控件中主治医生
            if (this.cmbAttendingDoc.Tag != null && !string.IsNullOrEmpty(this.cmbAttendingDoc.Tag.ToString()))
            {
                patientInfo.PVisit.AttendingDoctor.ID = this.cmbAttendingDoc.Tag.ToString();
                patientInfo.PVisit.AttendingDoctor.Name = this.cmbAttendingDoc.Text;
            }
            else
            {
                patientInfo.PVisit.AttendingDoctor.ID = "";
                patientInfo.PVisit.AttendingDoctor.Name = "";
            }


            //取控件中主任医生
            if (this.cmbConsultingDoc.Tag != null && !string.IsNullOrEmpty(this.cmbConsultingDoc.Tag.ToString()))
            {
                patientInfo.PVisit.ConsultingDoctor.ID = this.cmbConsultingDoc.Tag.ToString();
                patientInfo.PVisit.ConsultingDoctor.Name = this.cmbConsultingDoc.Text;
            }
            else
            {
                patientInfo.PVisit.ConsultingDoctor.ID = "";
                patientInfo.PVisit.ConsultingDoctor.Name = "";
            }

            //取控件中责任医师
            if (this.cmbResponsibleDoc.Tag != null && !string.IsNullOrEmpty(this.cmbResponsibleDoc.Tag.ToString()))
            {
                patientInfo.PVisit.ResponsibleDoctor.ID = this.cmbResponsibleDoc.Tag.ToString();
                patientInfo.PVisit.ResponsibleDoctor.Name = this.cmbResponsibleDoc.Text;
            }
            else
            {
                patientInfo.PVisit.ResponsibleDoctor.ID = "";
                patientInfo.PVisit.ResponsibleDoctor.Name = "";
            }

            //取控件中责任护士
            if (this.cmbAdmittingNur.Tag != null && !string.IsNullOrEmpty(this.cmbAdmittingNur.Tag.ToString()))
            {
                patientInfo.PVisit.AdmittingNurse.ID = this.cmbAdmittingNur.Tag.ToString();
                patientInfo.PVisit.AdmittingNurse.Name = this.cmbAdmittingNur.Text;
            }
            else
            {
                patientInfo.PVisit.AdmittingNurse.ID = "";
                patientInfo.PVisit.AdmittingNurse.Name = "";
            }

            //取控件中科主任
            if (this.cmbDirector.Tag != null && !string.IsNullOrEmpty(this.cmbDirector.Tag.ToString()))
            {
                patientInfo.PVisit.AttendingDirector.ID = this.cmbDirector.Tag.ToString();
                patientInfo.PVisit.AttendingDirector.Name = this.cmbDirector.Text;
            }
            else
            {
                patientInfo.PVisit.AttendingDirector.ID = "";
                patientInfo.PVisit.AttendingDirector.Name = "";
            }

            //取护理级别
            if (this.cmbTend.Tag != null && !string.IsNullOrEmpty(this.cmbTend.Tag.ToString()))
            {
                patientInfo.Disease.Tend.ID = this.cmbTend.Tag.ToString();
            }
            patientInfo.Disease.Tend.Name = this.cmbTend.Text;
            //取饮食
            if (!string.IsNullOrEmpty(this.cmbDiet.Text))
            {
                patientInfo.Disease.Memo = this.cmbDiet.Text;
            }

            //患者住院状态为入院登记
            patientInfo.PVisit.InState.ID = "I";
            patientInfo.PVisit.RegistTime = this.dtpInTime.Value;// {3B757263-9BE5-4e5a-9AD2-0815E9A210C7}
        }

        /// <summary>
        /// 清屏
        /// </summary>
        public virtual void ClearPatintInfo()
        {
            this.cmbDoct.Text = "";
            this.cmbDoct.Tag = "";
            this.cmbAttendingDoc.Text = "";
            this.cmbAttendingDoc.Tag = "";
            this.cmbConsultingDoc.Text = "";
            this.cmbConsultingDoc.Tag = "";
            this.cmbAdmittingNur.Text = "";
            this.cmbAdmittingNur.Tag = "";
            this.cmbDirector.Text = "";
            this.cmbDirector.Tag = "";
            this.cmbDiet.Tag = "";
            this.cmbDiet.Text = "";
            this.cmbTend.Tag = "";
            this.cmbTend.Text = "";

            this.cmbResponsibleDoc.Tag = "";//责任医师
            this.cmbResponsibleDoc.Text = "";

            lblBedLevlFee.Text = "";
        }

        /// <summary>
        /// 刷新患者信息
        /// </summary>
        /// <param name="inPatientNo"></param>
        public virtual void RefreshList(string inPatientNo)
        {
            //加载床位列表
            if (this.arrivetype == EnumArriveType.ChangeDoct)
            {
                //换医生时,显示全部床位
                if (bedListHelper == null)
                {
                    bedListHelper = new FS.FrameWork.Public.ObjectHelper();
                    bedListHelper.ArrayObject = managerIntegrate.QueryBedList(empl.Nurse.ID);
                }
                this.cmbBedNo.AddItems(bedListHelper.ArrayObject);
            }
            else
            {
                //接珍时,只显示空床
                this.cmbBedNo.AddItems(managerIntegrate.QueryUnoccupiedBed(empl.Nurse.ID));
            }

            try
            {
                this.patientInfo = this.Inpatient.QueryPatientInfoByInpatientNO(inPatientNo);
                if (this.patientInfo == null)
                {
                    MessageBox.Show(this.Inpatient.Err);
                    this.patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                }

                this.cbxAddAllDoct.Checked = this.isAddAllDoct;
                this.cbxAddAllNurse.Checked = this.isAddAllNurse;


                //2012-12-10 直接读取复选框判断加载的列表
                if (this.cbxAddAllDoct.Checked == true)
                {
                    //加载住院医生列表
                    this.cmbDoct.AddItems(allDoctHelper.ArrayObject);
                    //加载主治医生列表
                    this.cmbAttendingDoc.AddItems(allDoctHelper.ArrayObject);
                    //加载主任医生列表
                    this.cmbConsultingDoc.AddItems(allDoctHelper.ArrayObject);
                    //加载科主任列表
                    this.cmbDirector.AddItems(allDoctHelper.ArrayObject);
                    //加载责任医师列表
                    this.cmbResponsibleDoc.AddItems(allDoctHelper.ArrayObject);
                }
                else
                {
                    //医生都是本科室医生时，只加载本科室医生列表，否则加载全院医生列表
                    if (!string.IsNullOrEmpty(patientInfo.PVisit.AdmittingDoctor.ID) &&
                        privDoctHelper.GetObjectFromID(patientInfo.PVisit.AdmittingDoctor.ID) == null)
                    {
                        //加载住院医生列表
                        this.cmbDoct.AddItems(allDoctHelper.ArrayObject);
                    }
                    else
                    {
                        //加载住院医生列表
                        this.cmbDoct.AddItems(privDoctHelper.ArrayObject);
                    }
                    if (!string.IsNullOrEmpty(patientInfo.PVisit.AttendingDoctor.ID) &&
                        privDoctHelper.GetObjectFromID(patientInfo.PVisit.AttendingDoctor.ID) == null)
                    {
                        //加载主治医生列表
                        this.cmbAttendingDoc.AddItems(allDoctHelper.ArrayObject);
                    }
                    else
                    {
                        //加载主治医生列表
                        this.cmbAttendingDoc.AddItems(privDoctHelper.ArrayObject);
                    }
                    if (!string.IsNullOrEmpty(patientInfo.PVisit.ConsultingDoctor.ID) &&
                        privDoctHelper.GetObjectFromID(patientInfo.PVisit.ConsultingDoctor.ID) == null)
                    {
                        //加载主任医生列表
                        this.cmbConsultingDoc.AddItems(allDoctHelper.ArrayObject);
                    }
                    else
                    {
                        //加载主任医生列表
                        this.cmbConsultingDoc.AddItems(privDoctHelper.ArrayObject);
                    }
                    if (!string.IsNullOrEmpty(patientInfo.PVisit.AttendingDirector.ID) &&
                        privDoctHelper.GetObjectFromID(patientInfo.PVisit.AttendingDirector.ID) == null)
                    {
                        //加载科主任列表
                        this.cmbDirector.AddItems(allDoctHelper.ArrayObject);
                    }
                    else
                    {
                        //加载科主任列表
                        this.cmbDirector.AddItems(privDoctHelper.ArrayObject);
                    }
                    if (!string.IsNullOrEmpty(patientInfo.PVisit.ResponsibleDoctor.ID) &&
                        privDoctHelper.GetObjectFromID(patientInfo.PVisit.ResponsibleDoctor.ID) == null)
                    {
                        //加载责任医师列表
                        this.cmbResponsibleDoc.AddItems(allDoctHelper.ArrayObject);
                    }
                    else
                    {
                        //加载责任医师列表
                        this.cmbResponsibleDoc.AddItems(privDoctHelper.ArrayObject);
                    }
                }

                //2012-12-10 直接读取复选框判断加载的列表
                if (this.cbxAddAllNurse.Checked == true)
                {
                    this.cmbAdmittingNur.AddItems(allNurseHelper.ArrayObject);
                    this.cmbAdmittingNur.AppendItems(managerIntegrate.GetDeliverEmployee());  //{0a849cd8-db12-48e0-97ff-0b34f287c0a0}
                }
                else
                {
                    //护士都是本科室护士时，只加载本科室护士列表，否则加载全院护士列表
                    if (!string.IsNullOrEmpty(patientInfo.PVisit.AdmittingNurse.ID) &&
                        privDoctHelper.GetObjectFromID(patientInfo.PVisit.AdmittingNurse.ID) == null)
                    {
                        this.cmbAdmittingNur.AddItems(allNurseHelper.ArrayObject);
                        this.cmbAdmittingNur.AppendItems(managerIntegrate.GetDeliverEmployee()); //{0a849cd8-db12-48e0-97ff-0b34f287c0a0}
                    }
                    else
                    {
                        this.cmbAdmittingNur.AddItems(privNurseHelper.ArrayObject);
                    }
                }

                this.SetPatientInfo(this.patientInfo);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// 保存设置
        /// </summary>
        public virtual int Save()
        {
            //取婴儿接珍时的信息信息
            FS.HISFC.Models.RADT.PatientInfo PatientInfo = null;
            //取患者最新的住院主表信息
            PatientInfo = this.Inpatient.QueryPatientInfoByInpatientNO(this.patientInfo.ID);
            if (this.patientInfo == null)
            {
                MessageBox.Show(this.Inpatient.Err);
                return -1;
            }
            if (PatientInfo.IsBaby)// {7FFE7A7E-239D-4019-97B4-D3F80BB79713}
            {
                string motherID = this.Inpatient.QueryBabyMotherInpatientNO(this.patientInfo.ID);

                FS.HISFC.Models.RADT.PatientInfo motherPatientInfo = null;
                motherPatientInfo = this.Inpatient.QueryPatientInfoByInpatientNO(motherID);
                if (motherPatientInfo == null)
                {
                    MessageBox.Show(this.Inpatient.Err);
                    return -1;
                }

                if (this.NurseCell.ID == motherPatientInfo.PVisit.PatientLocation.NurseCell.ID)
                {
                    PatientInfo.PVisit.PatientLocation.Bed.ID = motherPatientInfo.PVisit.PatientLocation.Bed.ID;
                    
                }
            }
            else
            {
                this.cmbBedNo.Enabled = true;
            }

            //换医生时,如果患者已不在本科,则清空数据---当患者转科后,如果本窗口没有关闭,会出现此种情况
            if (PatientInfo.PVisit.PatientLocation.NurseCell.ID != this.NurseCell.ID
                && this.arrivetype == EnumArriveType.ChangeDoct)
            {
                MessageBox.Show("患者已不在本病区,请刷新当前窗口");
                return -1;
            }

            //如果患者已不是在院状态,则不允许操作
            if (PatientInfo.PVisit.InState.ID.ToString() != this.patientInfo.PVisit.InState.ID.ToString())
            {
                MessageBox.Show("患者信息已发生变化,请刷新当前窗口");
                return -1;
            }

            //保存旧的患者基本信息 用于各种变更记录及相关操作
            FS.HISFC.Models.RADT.PatientInfo oldPatientInfo = PatientInfo.Clone();

            //取变动信息:取医生、护士、科室等信息
            this.GetPatientInfo(PatientInfo);

            //判断是否医生、护士等是否必填
            if (this.JudgeClinicWorkersIsNeed(PatientInfo) == -1)
            {
                return -1;
            }

            //判断是否已选择床位
            if ((this.cmbBedNo.Tag == null || string.IsNullOrEmpty(this.cmbBedNo.Tag.ToString())) && patientInfo.IsBaby == false)
            {
                MessageBox.Show("未选择床位！");
                this.cmbBedNo.Focus();
                return -1;
            }

            FS.HISFC.BizProcess.Interface.IPatientShiftValid obj = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IPatientShiftValid)) as FS.HISFC.BizProcess.Interface.IPatientShiftValid;
            if (obj != null)
            {
                string err = string.Empty;
                bool bl = obj.IsPatientShiftValid(PatientInfo, FS.HISFC.Models.Base.EnumPatientShiftValid.C, ref err);
                if (!bl)
                {
                    MessageBox.Show(err);
                    return -1;
                }
            }

            //取处理时的床位信息
            FS.HISFC.Models.Base.Bed bed = new FS.HISFC.Models.Base.Bed();
            bed.ID = this.cmbBedNo.Tag.ToString();	//床号
            bed.InpatientNO = patientInfo.ID;		//床位上患者住院流水号

            FS.HISFC.Models.Base.Bed currBed = cmbBedNo.SelectedItem as FS.HISFC.Models.Base.Bed;
            if (currBed != null)
            {
                //O:占床 U:空床(可使用床位) C:关闭  H:挂床 W:包床 K:污染  I: 隔离,R请假
                if (currBed.Status.ID.ToString() == "H")
                {
                    MessageBox.Show(currBed.ID.Substring(4) + "床状态为挂床，不允许分配患者！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
                else if (currBed.Status.ID.ToString() == "K")
                {
                    MessageBox.Show(currBed.ID.Substring(4) + "床状态为污染，不允许分配患者！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
                else if (currBed.Status.ID.ToString() == "C")
                {
                    MessageBox.Show(currBed.ID.Substring(4) + "床已关闭，不允许分配患者！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
                else if (currBed.Status.ID.ToString() == "I")
                {
                    if (MessageBox.Show(currBed.ID.Substring(4) + "床状态为隔离，是否分配患者？", "询问", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return -1;
                    }
                }
            }

            #region 业务处理

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            managerRADT.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);


            #region 处理修改床位等级信息

            if (cmbBedNo.Tag != null && !string.IsNullOrEmpty(cmbBedNo.Tag.ToString()))
            {
                FS.HISFC.Models.Base.Bed bedOld = bedListHelper.GetObjectFromID(cmbBedNo.Tag.ToString()) as FS.HISFC.Models.Base.Bed;

                FS.HISFC.Models.Base.Bed bedNew = bedOld.Clone();
                bedNew.BedGrade.ID = cmbBedLevl.Tag.ToString();
                bedNew.BedGrade.Name = cmbBedLevl.Text;

                if (bedOld.BedGrade.ID != cmbBedLevl.Tag.ToString())
                {
                    string sql = @"UPDATE  com_bedinfo     --病床信息表
                                   SET  fee_grade_code = '{1}' --床位等级编码
                                   WHERE  BED_NO = '{0}'   --床号";
                    if (this.Inpatient.ExecNoQuery(sql, bedOld.ID, bedNew.BedGrade.ID) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.Msg("更新床位等级信息出错!" + this.managerIntegrate.Err, 211);

                        return -1;
                    }

                    //插入变更信息
                    if (this.managerRADT.InsertShiftData(this.patientInfo.ID, FS.HISFC.Models.Base.EnumShiftType.F, "修改床位等级", bedOld.BedGrade, new FS.FrameWork.Models.NeuObject(cmbBedLevl.Tag.ToString(), cmbBedLevl.Text, "")) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.Msg("插入变更信息出错!" + this.managerRADT.Err, 211);

                        return -1;
                    }
                }
            }

            #endregion

            //转科
            if (this.arrivetype == EnumArriveType.ShiftIn)
            {
                if (this.IArriveBase != null)
                {
                    if (IArriveBase.PatientArrive(EnumArriveType.ShiftIn, oldPatientInfo, PatientInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(IArriveBase.ErrInfo);
                        return -1;
                    }
                }

                if (managerRADT.ShiftIn(PatientInfo, this.NurseCell, this.cmbBedNo.Tag.ToString()) == -1)//调用转科业务
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(managerRADT.Err);
                    return -1;
                }
                else
                {
                }

                //床位日报处理
                //if (dayReportMgr.ArriveBed(oldPatientInfo, patientInfo) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show("处理动态日报出错！\r\n" + dayReportMgr.Err);
                //    return -1;
                //}
            }

            //接珍护理站为当前护理站(转入操作时,需要保留原护理站信息,所以在此处处理)
            PatientInfo.PVisit.PatientLocation.NurseCell = this.NurseCell;
            PatientInfo.PVisit.PatientLocation.Bed = bed;

            //接诊
            if (this.arrivetype == EnumArriveType.Accepts)
            {
                //PatientInfo.BloodType.Name = this.cmbBloodType.Text;//存ID不存Name
                PatientInfo.BloodType.ID = this.cmbBloodType.Tag;
                 
                if (managerRADT.ArrivePatient(PatientInfo, bed) == -1)//调用接诊业务
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(managerRADT.Err);
                    return -1;
                }
                else
                {
                }

                if (this.IArriveBase != null)
                {
                    if (IArriveBase.PatientArrive(EnumArriveType.Accepts, oldPatientInfo, PatientInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(IArriveBase.ErrInfo);
                        return -1;
                    }
                }

                //床位日报处理
                //if (dayReportMgr.ArriveBed(patientInfo, FS.HISFC.Models.Base.EnumShiftType.K) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show("处理动态日报出错！\r\n" + dayReportMgr.Err);
                //    return -1;
                //}
            }

            //召回
            if (this.arrivetype == EnumArriveType.CallBack)
            {
                if (!this.isAllowCallBackOtherDay & patientInfo.PVisit.OutTime.Date < this.Inpatient.GetDateTimeFromSysDateTime().Date)
                {
                    MessageBox.Show("不允许隔日召回！如有疑问请联系信息科！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                PatientInfo.BloodType.ID = this.cmbBloodType.Tag;
                if (managerRADT.CallBack(PatientInfo, bed) == -1)//调用召回业务
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(managerRADT.Err);
                    return -1;
                }
                else
                {
                    //对于出院登记时补收的费用进行退费
                    FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
                    feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    if (feeIntegrate.QuitSupplementFee(PatientInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("退费失败！" + feeIntegrate.Err);
                        return -1;
                    }
                    if (this.IArriveBase != null)
                    {
                        if (IArriveBase.PatientArrive(EnumArriveType.CallBack, oldPatientInfo, PatientInfo) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(IArriveBase.ErrInfo);
                            return -1;
                        }
                    }
                }

                //床位日报处理
                //if (dayReportMgr.ArriveBed(patientInfo, FS.HISFC.Models.Base.EnumShiftType.C) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show("处理动态日报出错！\r\n" + dayReportMgr.Err);
                //    return -1;
                //}
            }

            //换医师
            if (this.arrivetype == EnumArriveType.ChangeDoct)
            {
                PatientInfo.BloodType.ID = this.cmbBloodType.Tag;

                if (managerRADT.ChangeDoc(PatientInfo) == -1)//调用换医生业务
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(managerRADT.Err);
                    return -1;
                }
                else
                {
                }

                if (this.IArriveBase != null)
                {
                    if (IArriveBase.PatientArrive(EnumArriveType.ChangeDoct, oldPatientInfo, PatientInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(IArriveBase.ErrInfo);
                        return -1;
                    }
                }
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();
            this.arrangeBedNO = null; //{C29C2D37-D519-49af-AFEA-4981B1A013AE}
            MessageBox.Show(managerRADT.Err);

            base.OnRefreshTree();//刷新树
            return 1;
        }

        /// <summary>
        /// 判断必填项
        /// </summary>
        /// <returns></returns>
        private int JudgeClinicWorkersIsNeed(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            if (this.isAdmittingDoctorNeed)
            {
                if (string.IsNullOrEmpty(patientInfo.PVisit.AdmittingDoctor.ID))
                {
                    MessageBox.Show("请选择产科责任医师！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.cmbDoct.Focus();
                    return -1;
                }
            }
            if (isAttendingDoctorNeed)
            {
                if (string.IsNullOrEmpty(patientInfo.PVisit.AttendingDoctor.ID))
                {
                    MessageBox.Show("请选择主治医师！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.cmbAttendingDoc.Focus();
                    return -1;
                }
            }
            if (isAdmittingNurseNeed)
            {
                if (string.IsNullOrEmpty(patientInfo.PVisit.AdmittingNurse.ID))
                {
                    MessageBox.Show("请选择责任护士！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.cmbAdmittingNur.Focus();
                    return -1;
                }
            }
            if (isConsultingDoctorNeed)
            {
                if (string.IsNullOrEmpty(patientInfo.PVisit.ConsultingDoctor.ID))
                {
                    MessageBox.Show("请选择主任医师！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.cmbConsultingDoc.Focus();
                    return -1;
                }
            }
            if (isDirectorDoctorNeed && this.cmbDirector.Visible)
            {
                if (string.IsNullOrEmpty(patientInfo.PVisit.AttendingDirector.ID))
                {
                    MessageBox.Show("请选择科主任医师！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.cmbDirector.Focus();
                    return -1;
                }
            }

            if (this.cmbResponsibleDoc.Enabled)//若责任医师可选择数据（只有baby才是必填的）
            {
                if (string.IsNullOrEmpty(patientInfo.PVisit.ResponsibleDoctor.ID))
                {
                    MessageBox.Show("请选择责任医师！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.cmbResponsibleDoc.Focus();
                    return -1;
                }
            }

            if (!isCanAddOrder(patientInfo.PVisit.AdmittingDoctor.ID))
            {
                string doctName = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(patientInfo.PVisit.AdmittingDoctor.ID);
                MessageBox.Show("医生【" + doctName + "】没有处方权，不能作为住院医师！\r\n\r\n如有疑问请联系医务科！", "禁止", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.cmbDoct.Focus();
                return -1;
            }
            if (this.patientInfo.PVisit.InTime > patientInfo.PVisit.RegistTime)
            {
                MessageBox.Show("接诊时间不能小于入院时间！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.dtpInTime.Focus();
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 中五要求没有处方权的不让作为管床医生
        /// </summary>
        /// <returns></returns>
        private bool isCanAddOrder(string emplID)
        {
            string strSQL = @"select count(*) from met_com_authority f
                            where f.empl_code='{0}'
                            and f.popedom_type='1'";

            strSQL = string.Format(strSQL, emplID);

            string rev = Inpatient.ExecSqlReturnOne(strSQL, "0");
            if (rev == "0")
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 事件 

        private void cmbBedNo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.cmbBedNo.Tag == null || this.cmbBedNo.Tag.ToString() == "") 
                return;

            this.cmbBedLevl.Tag = (bedListHelper.GetObjectFromID(cmbBedNo.Tag.ToString()) as FS.HISFC.Models.Base.Bed).BedGrade.ID;
            ShowBedFee();
        }

        void cmbBedLevl_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// 显示床位费
        /// </summary>
        /// <returns></returns>
        private int ShowBedFee()
        {
            FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
            if (cmbBedLevl.Tag == null || string.IsNullOrEmpty(cmbBedLevl.Tag.ToString()))
            {
                return -1;
            }
            ArrayList alBedItem = feeIntegrate.QueryBedFeeItemByMinFeeCode(cmbBedLevl.Tag.ToString());
            if (alBedItem == null)
            {
                MessageBox.Show("查询床位费出错！床位等级：" + cmbBedLevl.Name + "\r\n" + feeIntegrate.Err, "错误");
                return -1;
            }

            decimal bedFee = 0;
            DateTime dtNow = feeIntegrate.GetDateTimeFromSysDateTime();
            foreach (FS.HISFC.Models.Fee.BedFeeItem bedItem in alBedItem)
            {
                if (bedItem.IsTimeRelation)
                {
                    DateTime begin = new DateTime(dtNow.Year, bedItem.BeginTime.Month, bedItem.BeginTime.Day);
                    DateTime end = new DateTime(dtNow.Year, bedItem.EndTime.Month, bedItem.EndTime.Day);

                    //开始时间>结束时间 认为是从上年开始的时间
                    if (begin > end)
                    {
                        begin = begin.AddYears(-1);
                    }

                    if (begin > dtNow || end < dtNow)
                    {
                        continue;
                    }
                }

                FS.HISFC.Models.Fee.Item.Undrug undrug = feeIntegrate.GetItem(bedItem.ID);


                if (undrug != null && undrug.IsValid)
                {
                    if (SOC.HISFC.BizProcess.Cache.Common.IsContainYKDept(((FS.HISFC.Models.Base.Employee)this.Inpatient.Operator).Dept.Clone().ID))
                    {
                        bedFee += bedItem.Qty * undrug.SpecialPrice;

                    }
                    else
                    {
                        bedFee += bedItem.Qty * undrug.Price;

                    }
                }
            }
            lblBedLevlFee.Text = "床位费：" + bedFee.ToString("F6").TrimEnd('0').TrimEnd('.') + "元";
            return 1;
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        #endregion

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { typeof(FS.HISFC.BizProcess.Interface.IPatientShiftValid) };
            }
        }

        #endregion

        private void btOutBillPrint_Click(object sender, EventArgs e)
        {
            if (this.patientInfo == null)
            {
                MessageBox.Show("未选中患者！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IPrintInHosNotice != null)
            {
                IPrintInHosNotice.SetValue(this.patientInfo);
                if (((FS.HISFC.Models.Base.Employee)this.managerIntegrate.Operator).IsManager)
                {
                    IPrintInHosNotice.PrintView();
                }
                else
                {
                    IPrintInHosNotice.Print();
                }
            }
            else
            {
                ucOutPrint print = new ucOutPrint();
                print.SetPatientInfo(this.patientInfo);
                //print.NameFlag = this.cmbOutpatientAim.Tag.ToString().Trim();
                //print.PrintPreview();
                print.Print();
            }
           
            MessageBox.Show("补打出院通知单成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


    }
}
