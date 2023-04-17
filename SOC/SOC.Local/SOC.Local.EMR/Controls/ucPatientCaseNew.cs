using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.EMR.Contrl
{
    public partial class ucPatientCaseNew : FS.FrameWork.WinForms.Controls.ucBaseControl//,FS.HISFC.BizProcess.Interface.Terminal.IOutpatientCase
    {
        public ucPatientCaseNew()
        {
            InitializeComponent();
            InitPatientCaseType();
        }

        private void InitPatientCaseType()
        {
            this.cmbPatientCaseType.Items.Clear();
            this.cmbPatientCaseType.Items.Add("急诊");
            this.cmbPatientCaseType.Items.Add("首诊");
            this.cmbPatientCaseType.Items.Add("复诊");
            this.cmbPatientCaseType.SelectedIndexChanged += new EventHandler(cmbPatientCaseType_SelectedIndexChanged);
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (e.Tag == null)
            {
                this.pnlPatientCase.Controls.Clear();
                this.cmbPatientCaseType.Text = string.Empty;
                MessageBox.Show("请选择患者！");
                return -1;
            }

            this.Reg = e.Tag as FS.HISFC.Models.Registration.Register;

            if (this.cmbPatientCaseType.Text == "急诊")
            {
                this.SetOutpatientCase(this.Reg, CaseType.Out_Emergency_Record);
            }
            else if (this.cmbPatientCaseType.Text == "首诊")
            {
                this.SetOutpatientCase(this.Reg, CaseType.Out_First);
            }
            else if (this.cmbPatientCaseType.Text == "复诊")
            {
                this.SetOutpatientCase(this.Reg, CaseType.Out_Second_Record);
            }
            return 1;
        }

        void cmbPatientCaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbPatientCaseType.Text == "急诊")
            {
                this.SetOutpatientCase(this.Reg, CaseType.Out_Emergency_Record);
            }
            else if (this.cmbPatientCaseType.Text == "首诊")
            {
                this.SetOutpatientCase(this.Reg, CaseType.Out_First);
            }
            else if (this.cmbPatientCaseType.Text == "复诊")
            {
                this.SetOutpatientCase(this.Reg, CaseType.Out_Second_Record);
            }
        }

        //路志鹏

        #region 变量
        //南庄修改
        public ArrayList diagnoses = null;
        ArrayList alText = new ArrayList();//TEXT数组
        ArrayList alChoose = new ArrayList();//转换后的数组
        private string Mod_No = null;//模板NO
        private string Mod_Name = null;//模板名称
        FS.FrameWork.Models.NeuObject obj = null;//返回的实体
        /// <summary>
        ///Update、Insert操作后时间(用来修改的时候进行判断)
        /// </summary>
        private string newOperTime = string.Empty;
        /// <summary>
        /// Update操作前的时间
        /// </summary>
        private string oldOperTime = string.Empty;

        /// <summary>
        /// 用来判断是修改还是新增加病历
        /// </summary>
        private bool isNew = true;

        #endregion

        #region 属性
        /// <summary>
        /// 模板信息
        /// </summary>
        private string Module_No
        {
            get
            {
                return Mod_No;
            }
            set
            {
                this.Mod_No = value;
            }

        }

        /// <summary>
        /// 模板Name
        /// </summary>
        private string Module_Name
        {
            get
            {
                return Mod_Name;
            }
            set
            {
                Mod_Name = value;
            }
        }
        /// <summary>
        /// 用来判断是修改还是新增加病历
        /// </summary>
        private bool IsNew
        {
            get
            {
                return isNew;
            }
            set
            {
                isNew = value;
            }

        }
        /// <summary>
        /// 操作后的时间
        /// </summary>
        private string NewOperTime
        {
            get
            {
                return newOperTime;
            }
            set
            {
                newOperTime = value;
            }
        }
        /// <summary>
        /// 操作前的时间
        /// </summary>
        private string OldOperTime
        {
            get
            {
                return oldOperTime;
            }
            set
            {
                oldOperTime = value;
            }
        }

        #region {FAEDC7CD-81B3-4fe2-BFF0-65D4ACE52CF7}

        private bool isUseFilter = false;

        /// <summary>
        /// 常用语是否分组过滤
        /// </summary>
        [Category("控件设置"), Description("常用语是否分组过滤")]
        public bool IsUseFilter
        {
            set
            {
                this.isUseFilter = value;
            }
            get
            {
                return this.isUseFilter;
            }
        }

        #endregion

        #endregion

        #region 系统层管理类

        FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory tempCaseModule = null;
        FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory caseHistory = null;
        //患者信息
        private FS.HISFC.Models.Registration.Register myReg = null;
        #endregion

        #region 患者信息

        public FS.HISFC.Models.Registration.Register Reg
        {
            get
            {
                return this.myReg;
            }
            set
            {
                this.myReg = value;
            }
        }

        #endregion


        #region EMR门诊病历接口  {4F2B8C3A-A728-4668-9879-37BF75DBE6E2}


        private FS.SOC.HISFC.BizLogic.EmrNew.EMROutpatientLogic emrOutPatientLogic = null; 

        public void SetOutpatientCase(FS.HISFC.Models.Registration.Register reg, CaseType type)
        {
            if (reg == null || string.IsNullOrEmpty(reg.ID))
            {
                MessageBox.Show("请选择患者信息");
                return;
            }

            long emrID = 0;

            if (emrOutPatientLogic == null)
            {
                emrOutPatientLogic = new FS.SOC.HISFC.BizLogic.EmrNew.EMROutpatientLogic();
            }

            if (emrOutPatientLogic.GetEmrRegId(reg.ID, ref emrID) < 0)
            {
                MessageBox.Show("获取EMR门诊流水号失败");
                return;
            }
            //FS.Emr.HisInterface.Bll.Application.Facade.RecordInterface emrInterface = new FS.Emr.HisInterface.Bll.Application.Facade.RecordInterface();
            FS.HISFC.Models.Base.Employee emp = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            string queryEmp = @"select e.ID from vemr_emp e where e.EMPL_CODE='{0}'";
            string querydept = @"select d.id from vemr_department d where d.dept_code='{0}'";

            queryEmp = string.Format(queryEmp, emp.ID);
            querydept = string.Format(querydept, emp.Dept.ID);
            string empid = this.emrOutPatientLogic.ExecSqlReturnOne(queryEmp);
            string deptID = this.emrOutPatientLogic.ExecSqlReturnOne(querydept);
            if (empid == "-1" || deptID == "-1")
            {
                MessageBox.Show("操作员编号或者科室在EMR系统不存在");
                return;
            }

            //emrInterface.CreatNewOutSetByPatient(emrID, Convert.ToInt64(empid), emrOutPatientLogic.GetDateTimeFromSysDateTime());
            //FS.Emr.HisInterface.UI.Internal.Facade.RecordUIFacde emrUIInterface = new FS.Emr.HisInterface.UI.Internal.Facade.RecordUIFacde();
            //System.Windows.Forms.Control con = emrUIInterface.CreatNewOutRecord(emrID, Convert.ToInt64(deptID), Convert.ToInt64(empid), type.ToString());
            //FS.FrameWork.WinForms.Classes.Function.PopShowControl(con);\
            this.pnlPatientCase.Controls.Clear();
            //this.pnlPatientCase.Controls.Add(con);
            //con.Dock = DockStyle.Fill;
            this.pnlPatientCase.AutoScroll = true;
        }

        public enum CaseType
        {
            Out_Emergency_Record,   //急诊
            Out_First,  //首诊
            Out_Second_Record   //复诊
        }
        #endregion

        #region IOutpatientCase 成员 {967CA656-AB9D-4841-8BFE-9A2EC7E8F886}

        private bool isBrowse = false;

        public int InitUC()
        {
            return 1;
        }

        public FS.HISFC.Models.Registration.Register Register
        {
            set
            {
                this.Reg = value;
            }
        }

        public bool IsBrowse
        {
            set
            {
                this.isBrowse = value;

                //this.Width = this.neuPanel3.Width;
                //this.neuPanel1.Visible = !value;
                //this.neuPanel2.Visible = !value;
            }
        }

        /// <summary>
        /// 数据显示
        /// </summary>
        public void Show()
        {
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(this);
        }

        #endregion
    }
}
