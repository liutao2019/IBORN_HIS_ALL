using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
namespace FS.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// [功能描述: 转科申请，取消控件]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2006-11-30]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucPatientShiftOut : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucPatientShiftOut()
        {
            InitializeComponent();

            cmbNewDept.SelectedIndexChanged += new EventHandler(cmbNewDept_SelectedIndexChanged);

            this.btnBackDept.Click += new EventHandler(btnBackDept_Click);

            this.ncbBackDept.CheckedChanged += new EventHandler(ncbBackDept_CheckedChanged);

            this.ncbBackDept.Checked = false;

            this.btnBackDept.Visible = false;

            this.btnSave.Visible = true;

            this.cbxModefyNurse.CheckedChanged += new EventHandler(cbxModefyNurse_CheckedChanged);

        }


        /// <summary>
        /// 返回原科室特别处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ncbBackDept_CheckedChanged(object sender, EventArgs e)
        {
            this.cmbNewDept.Items.Clear();

            this.cmbNewNurse.Items.Clear();

            ArrayList allDept = new ArrayList();

            ArrayList allNurse = new ArrayList();

            ArrayList allShiftData = inpatient.QueryPatientShiftInfoNew(this.patientInfo.ID);

            if (this.ncbBackDept.Checked)
            {
                //过滤出加载列表,按照comshiftdata表的处理
                if (allShiftData == null || allShiftData.Count == 0)
                {
                }
                else
                {
                    foreach (FS.HISFC.Models.Invalid.CShiftData shiftInfo in allShiftData)
                    {
                        if (shiftInfo.ShitType == "RO")
                        {
                            //添加原病区和新病区
                            FS.FrameWork.Models.NeuObject deptInfoOld = (FS.FrameWork.Models.NeuObject)FS.SOC.HISFC.BizProcess.Cache.Common.GetDept(shiftInfo.OldDataCode);
                            FS.FrameWork.Models.NeuObject deptInfoNew = (FS.FrameWork.Models.NeuObject)FS.SOC.HISFC.BizProcess.Cache.Common.GetDept(shiftInfo.NewDataCode);
                            ArrayList nurseInfoOld = interMgr.QueryNurseStationByDept(deptInfoOld, "");
                            ArrayList nurseInfoNew = interMgr.QueryNurseStationByDept(deptInfoNew, "");
                            allDept.Add(deptInfoOld);
                            allDept.Add(deptInfoNew);
                            allNurse.AddRange(nurseInfoOld);
                            allNurse.AddRange(nurseInfoNew);
                        }
                    }
                }
            }
            else
            {
                allDept = manager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.I);
                allNurse = manager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.N);
            }
            this.cmbNewDept.AddItems(allDept);
            this.cmbNewNurse.AddItems(allNurse);
            //加载全部科室

            this.btnSave.Visible = !this.ncbBackDept.Checked;
            this.btnBackDept.Visible = this.ncbBackDept.Checked;
        }

        #region 变量
        FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
        FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizLogic.RADT.InPatient inpatient = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        //{62BB0773-5C87-4458-9EA9-A8196C5D3EF4}
        FS.HISFC.BizProcess.Integrate.Common.ControlParam ctlParamManage = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 是否是取消转科的功能
        /// </summary>
        private bool isCancel = false;

        /// <summary>
        /// 接口 转科、转科召回等地方的判断,是否可以执行下一步操作
        /// </summary>
        FS.HISFC.BizProcess.Interface.IPatientShiftValid IPatientShiftValid = null;

        /// <summary>
        /// 存在未收费的手术申请单是否允许办理
        /// </summary>
        private CheckState isCanShiftWhenUnFeeUOApply = CheckState.Check;

        /// <summary>
        /// 是否可以更改病区
        /// </summary>
        public bool IsAbleChangeInpatientArea
        {
            get
            {
                return this.cbxModefyNurse.Enabled;
            }
            set
            {
                this.cbxModefyNurse.Enabled = value;
            }
        }


        #endregion

        #region 属性
        /// <summary>
        /// 是否取消申请
        /// </summary>
        public bool IsCancel
        {
            get
            {
                return this.isCancel;
            }
            set
            {
                this.isCancel = value;

                this.panel1.Visible = !value;
                this.cbxModefyNurse.Visible = !value;
                this.cmbNewDept.Enabled = !value;
                this.cmbNewNurse.Enabled = !value;
            }
        }

        #region 新提示设置

        /// <summary>
        /// 是否使用新提示
        /// </summary>
        private bool isUseNewMessage = true;

        /// <summary>
        /// 是否使用新提示
        /// </summary>
        [Category("转科申请"), Description("是否使用新提示格式")]
        public bool IsUseNewMessage
        {
            set { this.isUseNewMessage = value; }
            get { return this.isUseNewMessage; }
        }

        #endregion

        /// <summary>
        /// 存在未收费的手术申请单是否允许办理
        /// </summary>
        [Category("转科"), Description("存在未收费的手术申请单是否允许办理，默认为校验。"), DefaultValue(CheckState.Check)]
        public CheckState IsCanShiftWhenUnFeeUOApply
        {
            get
            {
                return this.isCanShiftWhenUnFeeUOApply;
            }
            set
            {
                isCanShiftWhenUnFeeUOApply = value;
            }
        }
        #endregion

        #region 函数
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            try
            {
                ArrayList al = FS.HISFC.Models.Base.SexEnumService.List();

                //this.cmbNewDept.AddItems(manager.QueryDeptmentsInHos(true));
                this.cmbNewDept.AddItems(manager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.I));

                this.cmbNewNurse.AddItems(manager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.N));

                IPatientShiftValid = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IPatientShiftValid)) as FS.HISFC.BizProcess.Interface.IPatientShiftValid;
            }
            catch
            {
            }
        }

        /// <summary>
        /// 返回原科室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnBackDept_Click(object sender, EventArgs e)
        {
            if (this.cmbNewDept.Tag == null || this.cmbNewDept.Tag.ToString() == "")
            {
                MessageBox.Show("请选择要转入的科室!");
                return;
            }

            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            if (this.cmbNewNurse.Tag == null || this.cmbNewNurse.Tag.ToString() == "")
            {
                MessageBox.Show("请选择要转入的病区!");
                return;
            }

            FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();
            dept.ID = this.cmbNewDept.Tag.ToString();
            dept.Name = this.cmbNewDept.Text;
            dept.Memo = this.txtNote.Text;


            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            FS.FrameWork.Models.NeuObject nurseCell = new FS.FrameWork.Models.NeuObject();
            nurseCell.ID = this.cmbNewNurse.Tag.ToString();
            nurseCell.Name = this.cmbNewNurse.Text;

            //记录转入科室病区,方便消息发送
            FS.HISFC.Models.RADT.PatientInfo patientNew = this.patientInfo.Clone() as FS.HISFC.Models.RADT.PatientInfo;
            patientNew.PVisit.PatientLocation.NurseCell = nurseCell;
            patientNew.PVisit.PatientLocation.Dept = dept;

            if (!this.IsCancel)
            {
                //返回转科前各种信息检查
                if (this.CheckBackShiftOut(this.patientInfo, true) == -1)
                {
                    return;
                }
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();

            if (radt.ShiftOut(this.patientInfo, dept, nurseCell, this.patientInfo.User03, this.isCancel) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            if (bSaveAndClose)
            {
                dialogResult = DialogResult.OK;
                this.FindForm().Close();
                return;
            }

            MessageBox.Show(radt.Err);

            base.OnRefreshTree();//刷新树
        }

        /// <summary>
        /// 根据科室找到病区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbNewDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            //{62BB0773-5C87-4458-9EA9-A8196C5D3EF4}
            #region 判断限制转科的科室和其他科室之间不能互转
            string noShiftDept = this.ctlParamManage.GetControlParam<string>("NOCHDT");

            //判断选中科室，如果是限制转科的，就不让转
            if (!string.IsNullOrEmpty(this.cmbNewDept.Tag.ToString()) 
                && noShiftDept.Contains(this.cmbNewDept.Tag.ToString())
                && this.patientInfo.PVisit.PatientLocation.Dept.ID != this.cmbNewDept.Tag.ToString())
            {
                MessageBox.Show("不允许从【" + this.patientInfo.PVisit.PatientLocation.Dept.Name + "】转科到【" + this.cmbNewDept.Text + "】");
                this.cmbNewDept.Tag = this.patientInfo.PVisit.PatientLocation.Dept.ID;
                return;
            }

            //本科室是限制转科的，就不让转其他科室
            if (!string.IsNullOrEmpty(this.cmbNewDept.Tag.ToString())
                && noShiftDept.Contains(this.patientInfo.PVisit.PatientLocation.Dept.ID)
                && this.patientInfo.PVisit.PatientLocation.Dept.ID != this.cmbNewDept.Tag.ToString())
            {
                MessageBox.Show("不允许从【" + this.patientInfo.PVisit.PatientLocation.Dept.Name + "】转科到【" + this.cmbNewDept.Text + "】");
                this.cmbNewDept.Tag = this.patientInfo.PVisit.PatientLocation.Dept.ID;
                return;
            }

            #endregion

            //ArrayList alNurse = interMgr.QueryNurseStationByDept(new FS.FrameWork.Models.NeuObject(this.cmbNewDept.Tag.ToString(), "", ""));
            ArrayList alNurse = this.QueryNurseByDept(this.cmbNewDept.Tag.ToString());
            #region
            if (alNurse != null && alNurse.Count > 0)
            {
                this.cmbNewNurse.Items.Clear();
                this.cmbNewNurse.AddItems(alNurse);
                this.cmbNewNurse.Tag = (alNurse[0] as FS.FrameWork.Models.NeuObject).ID;
            }
            #endregion
        }


        /// <summary>
        /// 将患者信息显示在控件中
        /// </summary>
        private void ShowPatientInfo()
        {
            this.txtPatientNo.Text = this.patientInfo.PID.PatientNO;		//病人号
            this.txtPatientNo.Tag = this.patientInfo.ID;							//住院流水号
            this.txtName.Text = this.patientInfo.Name;								//患者姓名
            this.txtSex.Text = this.patientInfo.Sex.Name;					//性别
            this.txtOldDept.Text = this.patientInfo.PVisit.PatientLocation.Dept.Name;//源科室名称
            this.cmbBedNo.Text = this.patientInfo.PVisit.PatientLocation.Bed.ID.Length > 4 ? this.patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) : "";	//床号
            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            this.txtOldNurse.Text = this.patientInfo.PVisit.PatientLocation.NurseCell.Name;
            //定义患者Location实体
            FS.HISFC.Models.RADT.Location newLocation = new FS.HISFC.Models.RADT.Location();
            //取患者转科申请信息
            newLocation = this.inpatient.QueryShiftNewLocation(this.patientInfo.ID, this.patientInfo.PVisit.PatientLocation.Dept.ID);
            this.patientInfo.User03 = newLocation.User03;
            if (this.patientInfo.User03 == null)
                this.patientInfo.User03 = "1";//申请

            if (newLocation == null)
            {
                MessageBox.Show(this.inpatient.Err);
                return;
            }

            this.cmbNewDept.Tag = newLocation.Dept.ID;	//新科室名称
            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            this.cmbNewNurse.Tag = newLocation.NurseCell.ID;
            this.txtNote.Text = newLocation.Memo;		//备注
            //如果没有转科申请,则清空新科室编码
            if (newLocation.Dept.ID == "")
            {
                this.cmbNewDept.Text = null;
            }

            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            if (newLocation.NurseCell.ID == "")
            {
                this.cmbNewNurse.Text = null;
            }

            if (this.patientInfo.User03 != null && this.patientInfo.User03 == "0")
                this.label8.Visible = true;
            else
                this.label8.Visible = false;
        }


        /// <summary>
        /// 清屏
        /// </summary>
        public void ClearPatintInfo()
        {
            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            this.cmbNewNurse.Tag = "";
            this.cmbNewNurse.Text = "";
            this.cmbNewDept.Text = "";
            this.cmbNewDept.Tag = "";
        }

        /// <summary>
        /// {62BB0773-5C87-4458-9EA9-A8196C5D3EF4}
        /// 根据科室找对应的病区
        /// </summary>
        /// <param name="deptStatCode"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        private ArrayList QueryNurseByDept(string deptCode)
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
        /// 刷新
        /// </summary>
        /// <param name="patientInfo"></param>
        public void RefreshList(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            try
            {
                //将患者信息显示在控件中
                this.ShowPatientInfo();

                this.panel1.AutoScrollPosition = new Point(0, 0);
                this.CheckShiftOut(this.patientInfo, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.patientInfo = neuObject as FS.HISFC.Models.RADT.PatientInfo;
            RefreshList(this.patientInfo);
            return 0;
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.InitControl();
            return null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.cmbNewDept.Tag == null || this.cmbNewDept.Tag.ToString() == "")
            {
                MessageBox.Show("请选择要转入的科室!");
                return;
            }

            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            if (this.cmbNewNurse.Tag == null || this.cmbNewNurse.Tag.ToString() == "")
            {
                MessageBox.Show("请选择要转入的病区!");
                return;
            }

            FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();
            dept.ID = this.cmbNewDept.Tag.ToString();
            dept.Name = this.cmbNewDept.Text;
            dept.Memo = this.txtNote.Text;


            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            FS.FrameWork.Models.NeuObject nurseCell = new FS.FrameWork.Models.NeuObject();
            nurseCell.ID = this.cmbNewNurse.Tag.ToString();
            nurseCell.Name = this.cmbNewNurse.Text;

            //记录转入科室病区,方便消息发送
            FS.HISFC.Models.RADT.PatientInfo patientNew = this.patientInfo.Clone() as FS.HISFC.Models.RADT.PatientInfo;
            patientNew.PVisit.PatientLocation.NurseCell = nurseCell;
            patientNew.PVisit.PatientLocation.Dept = dept;

            if (!this.IsCancel)
            {
                //转科前对患者各种信息的检查
                if (this.CheckShiftOut(this.patientInfo, true) == -1)
                {
                    return;
                }
            }

            //是否选择的停止全部长嘱
            bool autoDC = false;
            if (!this.isAutoDcOrder && this.isUseShiftAutoDcOrder)
            {
                DialogResult rev = MessageBox.Show("是否停止全部长嘱？", "询问", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (rev == DialogResult.Cancel)
                {
                    return;
                }
                else if (rev == DialogResult.Yes)
                {
                    autoDC = true;
                }
            }

            //if (patientNew.IsBaby)// {7FFE7A7E-239D-4019-97B4-D3F80BB79713}
            //{
            //    DialogResult rev = MessageBox.Show("是否只转婴儿，否则请按妈妈转科？", "询问", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            //    if (rev == DialogResult.Cancel)
            //    {
            //        return;
            //    }
            //}

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();

            if (radt.ShiftOut(this.patientInfo, dept, nurseCell, this.patientInfo.User03, this.isCancel) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
            }

            if (this.isUseShiftAutoDcOrder)
            {
                string errInfo = "";
                if (this.isAutoDcOrder || autoDC)
                {
                    if (this.AutoDcOrder(ref errInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errInfo);
                        return;
                    }
                }
            }


            FS.FrameWork.Management.PublicTrans.Commit();

            if (bSaveAndClose)
            {
                dialogResult = DialogResult.OK;
                this.FindForm().Close();
                return;
            }

            MessageBox.Show(radt.Err);

            base.OnRefreshTree();//刷新树
        }



        #region 转科申请限制

        /// <summary>
        /// 存在退费申请是否允许做转科申请
        /// </summary>
        private CheckState isCanShiftWhenQuitFeeApplay = CheckState.Check;

        /// <summary>
        /// 有退费申请是否允许转科申请
        /// </summary>
        [Category("转科申请"), Description("存在退费申请是否允许做转科申请")]
        public CheckState IsCanShiftWhenQuitFeeApplay
        {
            get
            {
                return isCanShiftWhenQuitFeeApplay;
            }
            set
            {
                isCanShiftWhenQuitFeeApplay = value;
            }
        }

        /// <summary>
        /// 存在退药申请是否允许做转科申请
        /// </summary>
        private CheckState isCanShiftWhenQuitDrugApplay = CheckState.Check;

        /// <summary>
        /// 存在退药申请是否允许做转科申请
        /// </summary>
        [Category("转科申请"), Description("存在退药申请是否允许做转科申请")]
        public CheckState IsCanShiftWhenQuitDrugApplay
        {
            get
            {
                return isCanShiftWhenQuitDrugApplay;
            }
            set
            {
                isCanShiftWhenQuitDrugApplay = value;
            }
        }

        /// <summary>
        /// 存在发药申请是否允许做转科申请
        /// </summary>
        private CheckState isCanShiftWhenDrugApplay = CheckState.Check;

        /// <summary>
        /// 存在发药申请是否允许做转科申请
        /// </summary>
        [Category("转科申请"), Description("存在发药申请是否允许做转科申请")]
        public CheckState IsCanShiftWhenDrugApplay
        {
            get
            {
                return this.isCanShiftWhenDrugApplay;
            }
            set
            {
                this.isCanShiftWhenDrugApplay = value;
            }
        }

        /// <summary>
        /// 存在未确认项目是否允许做转科申请
        /// </summary>
        private CheckState isCanShiftWhenUnConfirm = CheckState.Check;

        /// <summary>
        /// 存在未确认项目是否允许做转科申请
        /// </summary>
        [Category("转科申请"), Description("存在未确认项目是否允许做转科申请")]
        public CheckState IsCanShiftWhenUnConfirm
        {
            get
            {
                return this.isCanShiftWhenUnConfirm;
            }
            set
            {
                this.isCanShiftWhenUnConfirm = value;
            }
        }

        /// <summary>
        /// 未开立转科医嘱是否允许做转科申请
        /// </summary>
        private CheckState isCanShiftWhenNoOutOrder = CheckState.No;

        /// <summary>
        /// 未开立转科医嘱是否允许做转科申请
        /// </summary>
        [Category("转科申请"), Description("未开立转科医嘱是否允许做转科申请")]
        public CheckState IsCanShiftWhenNoOutOrder
        {
            get
            {
                return this.isCanShiftWhenNoOutOrder;
            }
            set
            {
                this.isCanShiftWhenNoOutOrder = value;
            }
        }

        /// <summary>
        /// 未全部停止长嘱是否允许做转科申请
        /// </summary>
        private CheckState isCanShiftWhenNoDcOrder = CheckState.No;

        /// <summary>
        /// 未全部停止长嘱是否允许做转科申请
        /// </summary>
        [Category("转科申请"), Description("未全部停止长嘱是否允许做转科申请")]
        public CheckState IsCanShiftWhenNoDcOrder
        {
            get
            {
                return this.isCanShiftWhenNoDcOrder;
            }
            set
            {
                this.isCanShiftWhenNoDcOrder = value;
            }
        }

        /// <summary>
        /// 存在未审核医嘱是否允许做转科申请
        /// </summary>
        private CheckState isCanShiftWhenUnConfirmOrder = CheckState.No;

        /// <summary>
        /// 存在未审核医嘱是否允许做转科申请
        /// </summary>
        [Category("转科申请"), Description("存在未审核医嘱是否允许做转科申请")]
        public CheckState IsCanShiftWhenUnConfirmOrder
        {
            get
            {
                return this.isCanShiftWhenUnConfirmOrder;
            }
            set
            {
                this.isCanShiftWhenUnConfirmOrder = value;
            }
        }

        /// <summary>
        /// 存在未收费的非药品执行档是否允许做转科申请
        /// </summary>
        private CheckState isCanShiftWhenNoFeeExecUndrugOrder = CheckState.Check;

        /// <summary>
        /// 存在未收费的非药品执行档是否允许做转科申请
        /// </summary>
        [Category("转科申请"), Description("存在未收费的非药品执行档是否允许做转科申请")]
        public CheckState IsCanShiftWhenNoFeeExecUndrugOrder
        {
            get
            {
                return this.isCanShiftWhenNoFeeExecUndrugOrder;
            }
            set
            {
                isCanShiftWhenNoFeeExecUndrugOrder = value;
            }
        }

        /// <summary>
        /// 存在未完成的会诊申请是否允许办理转科申请
        /// </summary>
        private CheckState isCanShiftWhenUnConsultation = CheckState.Check;

        /// <summary>
        /// 存在未完成的会诊申请是否允许办理转科申请
        /// </summary>
        [Category("转科申请"), Description("存在未完成的会诊申请是否允许办理转科申请")]
        public CheckState IsCanShiftWhenUnConsultation
        {
            get
            {
                return this.isCanShiftWhenUnConsultation;
            }
            set
            {
                isCanShiftWhenUnConsultation = value;
            }
        }

        /// <summary>
        /// 欠费是否允许办理转科手续
        /// </summary>
        private CheckState isCanShiftWhenLackFee = CheckState.Check;

        /// <summary>
        /// 欠费是否允许办理转科手续
        /// </summary>
        [Category("转科申请"), Description("欠费是否允许办理转科手续")]
        public CheckState IsCanShiftWhenLackFee
        {
            get
            {
                return this.isCanShiftWhenLackFee;
            }
            set
            {
                isCanShiftWhenLackFee = value;
            }
        }

        #endregion

        /// <summary>
        /// 对患者进行转科判断 by huangchw 2012-11-20
        /// </summary>
        /// <returns></returns>
        private int CheckShiftOut(FS.HISFC.Models.RADT.PatientInfo patient, bool isSave)
        {
            //整理：把提示统一放到一起

            //需要提示选择的东东
            string checkMessage = "";

            //提示禁止的东东
            string stopMessage = "";

            Classes.Function funMgr = new FS.HISFC.Components.RADT.Classes.Function();
            if (IPatientShiftValid != null)
            {
                bool bl = IPatientShiftValid.IsPatientShiftValid(patient, FS.HISFC.Models.Base.EnumPatientShiftValid.O, ref stopMessage);
                if (!bl)
                {
                    if (!string.IsNullOrEmpty(stopMessage))
                    {
                        MessageBox.Show(stopMessage);
                    }
                    return -1;
                }
            }


            //注意不要在业务层弹出MessageBox！！！

            /*
             * 一、费用
             *  1、存在退费申请，不允许办理转科登记
             * 二、药品
             *  1、存在退药申请，不允许办理转科登记
             *  2、存在发药申请，提示是否办理转科登记
             * 三、终端确认
             *  1、存在未确认项目，不允许或提示是否允许办理转科登记
             * 
             * 对于其他情况，采用接口本地化实现
             * 1、是否长嘱全停
             * 2、是否开立转科医嘱
             * 3、是否有未审核医嘱
             * 4、判断床位数和护理费的收取是否正确
             * */


            #region 1、存在退费申请，不允许办理转科登记

            if (isCanShiftWhenQuitFeeApplay != CheckState.Yes)
            {
                string ReturnApplyItemInfo = FS.HISFC.Components.RADT.Classes.Function.CheckReturnApply(patient.ID);
                if (ReturnApplyItemInfo != null)
                {
                    string[] item = ReturnApplyItemInfo.Split('\r');
                    string tip = "";
                    for (int i = 0; i < item.Length; i++)
                    {
                        if (isUseNewMessage)
                        {
                            tip += item[i] + "\r";
                        }
                        else
                        {
                            if (i <= 2)
                            {
                                tip += item[i] + "\r";
                                if (i == item.Length - 1 || i == 2)
                                {
                                    tip += "   等";
                                }
                            }
                        }
                    }

                    if (isCanShiftWhenQuitFeeApplay == CheckState.Check)
                    {
                        checkMessage += "\r\n★存在未确认的退费申请！\r\n" + tip;
                    }
                    else if (isCanShiftWhenQuitFeeApplay == CheckState.No)
                    {
                        //存在退费申请不允许做转科申请
                        stopMessage += "\r\n存在未确认的退费申请！\r\n" + ReturnApplyItemInfo;
                    }
                }
            }
            #endregion

            #region 2、存在退药申请，提示是否继续

            if (isCanShiftWhenQuitDrugApplay != CheckState.Yes)
            {
                //增加查询患者是否有未审核的退药记录,为转科申请判断用
                if (isUseNewMessage)
                {
                    string msg = FS.HISFC.Components.RADT.Classes.Function.CheckQuitDrugApplay(patient.ID);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        checkMessage += "\r\n\r\n★存在未审核的退药申请信息！" + "\r" + msg;
                    }
                }
                else
                {
                    int returnValue = this.pharmacyIntegrate.QueryNoConfirmQuitApply(patient.ID);
                    if (returnValue == -1)
                    {
                        MessageBox.Show("查询患者退药申请信息出错!" + this.pharmacyIntegrate.Err);

                        return -1;
                    }
                    if (returnValue > 0) //有申请但是没有核准的退药信息
                    {
                        if (this.isCanShiftWhenQuitDrugApplay == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n★存在未审核的退药申请信息！";
                        }
                        else if (this.isCanShiftWhenQuitDrugApplay == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n存在未审核的退药申请信息！";
                        }
                    }
                }
            }

            #endregion

            #region 3、判断患者是存在未摆药的药品 提示是否继续

            if (isCanShiftWhenDrugApplay != CheckState.Yes)
            {
                if (isUseNewMessage)
                {
                    string msg = FS.HISFC.Components.RADT.Classes.Function.CheckDrugApplayWithOutQuit(patient.ID);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        checkMessage += "\r\n\r\n★存在未摆药的药品项目！\r\n" + msg;
                    }
                }
                else
                {
                    string msg = FS.HISFC.Components.RADT.Classes.Function.CheckDrugApplay(patient.ID);
                    if (msg != null)
                    {
                        string[] item = msg.Split('\r');
                        string tip = "";
                        for (int i = 0; i < item.Length; i++)
                        {
                            if (i <= 2)
                            {
                                tip += item[i] + "\r";
                                if (i == item.Length - 1 || i == 2)
                                {
                                    tip += "   等";
                                }
                            }
                        }

                        if (this.isCanShiftWhenDrugApplay == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n★存在未摆药的药品项目！\r\n" + tip;
                        }
                        else if (isCanShiftWhenDrugApplay == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n存在未摆药的药品项目！\r\n" + msg;
                        }
                    }
                }
            }
            #endregion

            #region 4、存在未终端确认项目，提示是否继续

            if (isCanShiftWhenUnConfirm != CheckState.Yes)
            {
                string UnConfirmItemInfo = FS.HISFC.Components.RADT.Classes.Function.CheckUnConfirm(patient.ID);
                if (UnConfirmItemInfo != null)
                {
                    string[] item = UnConfirmItemInfo.Split('\r');
                    string tip = "";
                    for (int i = 0; i < item.Length; i++)
                    {
                        if (isUseNewMessage)
                        {
                            tip += item[i] + "\r\n";
                        }
                        else
                        {
                            if (i <= 2)
                            {
                                tip += item[i] + "\r\n";

                                //{4660CE00-A79E-468a-8086-DE9C8D811779} 控制显示信息长度
                                if (tip.Length > 100)
                                {
                                    tip = tip.Substring(0, 100);
                                    tip += "   等";
                                    break;
                                }
                                //{4660CE00-A79E-468a-8086-DE9C8D811779} 控制显示信息长度

                                if (i == item.Length - 1 || i == 2)
                                {
                                    tip += "   等";
                                }
                            }
                        }
                    }

                    if (this.isCanShiftWhenUnConfirm == CheckState.Check)
                    {
                        //checkMessage += "\r\n\r\n★存在未确认收费的终端项目！\r\n" + tip;
                        checkMessage += "\r\n\r\n★存在未确认收费的终端项目！\r\n" + UnConfirmItemInfo;
                    }
                    else if (isCanShiftWhenUnConfirm == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n存在未确认收费的终端项目！\r\n" + UnConfirmItemInfo;
                    }
                }
            }

            #endregion

            #region 5、判断是否开立转科医嘱

            if (isCanShiftWhenNoOutOrder != CheckState.Yes)
            {
                FS.HISFC.Models.Order.Inpatient.Order inOrder = null;

                int rev = this.orderIntegrate.GetShiftOutOrderType(patientInfo.ID, ref inOrder);

                if (rev < 0)
                {
                    MessageBox.Show("查询转科医嘱出错!\r\n" + orderIntegrate.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    return -1;
                }
                else if (rev == 0)
                {
                    if (isCanShiftWhenNoOutOrder == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n★未开立转科医嘱！";
                    }
                    else if (isCanShiftWhenNoOutOrder == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n★未开立转科医嘱！";
                    }
                }
            }

            #endregion

            #region 6、判断长嘱是否全停

            if (isCanShiftWhenNoDcOrder != CheckState.Yes)
            {
                if (isUseNewMessage)
                {
                    string msg = FS.HISFC.Components.RADT.Classes.Function.CheckAllLongOrderUnStop(patient.ID);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        if (isCanShiftWhenNoDcOrder == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n★长期医嘱没有全部停止\r\n" + msg;
                        }
                        else if (isCanShiftWhenNoDcOrder == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n★长期医嘱没有全部停止\r\n" + msg;
                        }
                    }
                }
                else
                {
                    if (!funMgr.CheckIsAllLongOrderStop(patient.ID))
                    {
                        if (isCanShiftWhenNoDcOrder == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n★" + funMgr.Err;
                        }
                        else if (isCanShiftWhenNoDcOrder == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n" + funMgr.Err;
                        }
                    }
                }
            }

            #endregion

            #region 7、判断是否有未审核医嘱


            if (isCanShiftWhenUnConfirmOrder != CheckState.Yes)
            {
                if (isUseNewMessage)
                {
                    string msg = FS.HISFC.Components.RADT.Classes.Function.CheckAllOrderUnConfirm(patient.ID);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        if (isCanShiftWhenUnConfirmOrder == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n★存在未审核医嘱\r\n" + msg;
                        }
                        else if (isCanShiftWhenUnConfirmOrder == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n★存在未审核医嘱\r\n" + msg;
                        }
                    }
                }
                else
                {
                    if (!funMgr.CheckIsAllOrderConfirm(patient.ID))
                    {
                        if (isCanShiftWhenUnConfirmOrder == CheckState.Check)
                        {
                            stopMessage += "\r\n\r\n★" + funMgr.Err;
                        }
                        else if (isCanShiftWhenUnConfirmOrder == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n" + funMgr.Err;
                        }
                    }
                }
            }

            #endregion

            #region 8、判断是否有未收费的非药品医嘱执行档

            if (isCanShiftWhenNoFeeExecUndrugOrder != CheckState.Yes)
            {
                string returnStr = FS.HISFC.Components.RADT.Classes.Function.CheckExecOrderCharge(patient.ID);
                if (returnStr != null)
                {
                    string[] strArray = returnStr.Split(new char[3] { '|', '|', '|' });

                    if (Convert.ToInt32(strArray[3]) > 0)
                    {
                        if (this.isCanShiftWhenNoFeeExecUndrugOrder == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n★存在未收费项目！\r\n" + strArray[0];

                            if (!isUseNewMessage)
                            {
                                //{4660CE00-A79E-468a-8086-DE9C8D811779} 控制显示信息长度
                                if (checkMessage.Length > 250)
                                {
                                    checkMessage = checkMessage.Substring(0, 250);
                                    checkMessage += "   等";
                                }
                            }
                            //{4660CE00-A79E-468a-8086-DE9C8D811779} 控制显示信息长度

                        }
                        else if (this.isCanShiftWhenNoFeeExecUndrugOrder == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n存在未收费项目！\r\n" + strArray[0];
                        }
                    }
                }
            }

            #endregion

            #region 9、转科情况不允许为空

            //if (this.cmbZg.Tag == null || string.IsNullOrEmpty(cmbZg.Tag.ToString()))
            //{
            //    stopMessage += "\r\n\r\n病案室要求，转科情况不允许为空！\r\n";
            //}
            #endregion

            #region 10、存在未收费手术申请单不允许办理转科
            // {a50189cc-d017-4e36-97f1-e011f4603858} 转科加入判定手术申请单不允许转科。2013-8-29 by xuc.
            if (this.isCanShiftWhenUnFeeUOApply != CheckState.Yes)
            {
                string sql = @"select  f.apply_date,f.pre_date,a.item_name,f.apply_dpcd from met_ops_apply f,met_ops_operationitem a
                                                        where a.operationno=f.operationno
                                                        and f.clinic_code = a.clinic_code  
                                                        and f.clinic_code='{0}'
                                                        and f.ynvalid='1'
                                                        and a.main_flag = '1'
                                                        and f.execstatus!='4'
                                                        and f.execstatus!='5'
                            order by a.sort_no";
                try
                {
                    DataSet dsTemp = null;
                    string msg = null;
                    int queryRev = funMgr.ExecQuery(string.Format(sql, patient.ID), ref dsTemp);

                    if (queryRev > 0)
                    {
                        if (dsTemp != null && dsTemp.Tables.Count > 0)
                        {
                            DataTable dtTemp = dsTemp.Tables[0];

                            string strApplyDate = string.Empty;
                            string strItemName = string.Empty;
                            string strPreDate = string.Empty;
                            string applyDpcd = string.Empty;
                            foreach (DataRow drRow in dtTemp.Rows)
                            {
                                strApplyDate = drRow["apply_date"].ToString();
                                strItemName = drRow["item_name"].ToString();
                                strPreDate = drRow["pre_date"].ToString();
                                applyDpcd = drRow["apply_dpcd"].ToString();

                                msg = msg + "申请时间：" + strApplyDate + "    " + "预约时间：" + strPreDate + "\r\n" + "申请科室：" + SOC.HISFC.BizProcess.Cache.Common.GetDept(applyDpcd) + "    手术名称：" + strItemName + "\r\n";
                            }

                            if (this.isCanShiftWhenUnFeeUOApply == CheckState.No)
                            {
                                // 包含手术申请则不允许进行转科。
                                stopMessage += "\r\n\r\n★存在未完成的手术申请单！\r\n" + msg;
                            }
                            else if (this.isCanShiftWhenUnFeeUOApply == CheckState.Check)
                            {
                                // 包含手术申请则不允许进行转科。
                                checkMessage += "\r\n\r\n★存在未完成的手术申请单！\r\n" + msg;
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("存在未完成的手术申请单");
                }
            }

            #endregion


            #region 11、存在未完成会诊，不允许转科

            if (isCanShiftWhenUnConsultation != CheckState.Yes)
            {
                string strSQL = @"select --f.state,--0:开立未接收 1:已接收 2:已完成 3:已拒绝
                                   count(1)
                            from bhemr.CONS_CONSULTATION f
                            where f.state in('0','1')
                            and f.inpatient_id=(
                            select g.id from 
                            bhemr.pt_inpatient_cure g
                            where g.inpatient_code='{0}'
                            )";
                strSQL = string.Format(strSQL, this.patientInfo.ID);

                string errMsg = null;
                if (funMgr.ExecSqlReturnOne(strSQL) != "0")
                {
                    errMsg = "\r\n\r\n★存在未完成的会诊申请！\n";
                }

                if (isCanShiftWhenUnConsultation == CheckState.Check)
                {
                    checkMessage += errMsg;
                }
                else if (isCanShiftWhenUnConsultation == CheckState.No)
                {
                    stopMessage += errMsg;
                }
            }
            #endregion

            #region 欠费判断

            if (isCanShiftWhenLackFee != CheckState.Yes)
            {
                try
                {
                    if (patient.PVisit.MoneyAlert != 0 && patient.FT.LeftCost < this.patientInfo.PVisit.MoneyAlert)
                    {
                        if (isCanShiftWhenUnFeeUOApply == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n已经欠费，\r\n余额： " + patient.FT.LeftCost.ToString() + "\r\n警戒线： " + patient.PVisit.MoneyAlert.ToString();
                        }
                        else if (isCanShiftWhenUnFeeUOApply == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n已经欠费，\r\n余额： " + patient.FT.LeftCost.ToString() + "\r\n警戒线： " + patient.PVisit.MoneyAlert.ToString();
                        }
                    }
                }
                catch
                {
                }
            }
            #endregion

            if (!isSave)
            {
                this.label1.Text = stopMessage;
                return -1;
            }

            if (isUseNewMessage)
            {
                FS.HISFC.Components.RADT.Forms.frmMessageShow frmMessage = new FS.HISFC.Components.RADT.Forms.frmMessageShow();
                frmMessage.Clear();
                if (!string.IsNullOrEmpty(checkMessage))
                {
                    frmMessage.SetPatientInfo(patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 患者【" + patient.Name + "】");
                    frmMessage.SetTipMessage("存在以下问题未处理,是否继续办理转科？");
                    frmMessage.SetMessage(checkMessage);
                    frmMessage.SetPerfectWidth();
                    if (frmMessage.ShowDialog() == DialogResult.No)
                    {
                        return -1;
                    }
                }

                if (!string.IsNullOrEmpty(stopMessage))
                {
                    frmMessage.SetPatientInfo(patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 患者【" + patient.Name + "】");
                    frmMessage.SetTipMessage("存在以下问题未处理,不能继续办理转科！");
                    frmMessage.SetMessage(stopMessage);
                    frmMessage.SetPerfectWidth();
                    frmMessage.HideNoButton();
                    frmMessage.ShowDialog();
                    return -1;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(checkMessage))
                {
                    if (MessageBox.Show(patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 患者【" + patient.Name + "】\r\n存在以下问题未处理,是否继续办理转科？\r\n\r\n" + checkMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return -1;
                    }
                }

                if (!string.IsNullOrEmpty(stopMessage))
                {
                    MessageBox.Show(patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 患者【" + patient.Name + "】\r\n存在以下问题未处理,不能继续办理转科！\r\n\r\n" + stopMessage, "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// 患者转回原科室判断
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        private int CheckBackShiftOut(FS.HISFC.Models.RADT.PatientInfo patient, bool isSave)
        {
            //由于此种方式是判断转科错误的情况，所以只判断转科过来后是否开立过医嘱
            string stopMessage = "";

            #region 判断医嘱
            string strOrderSql = @"select count(*)
  from met_ipm_order a,(select b.clinic_no,b.new_data_code,b.oper_date
          from com_shiftdata b
         where b.shift_type = 'RO'
           and b.clinic_no = '{0}'
           and rownum = 1
           order by b.oper_date desc) bb  where
           a.inpatient_no = '{0}' 
           and a.inpatient_no = bb.clinic_no(+)
           and a.dept_code = bb.new_data_code(+)
           and a.mo_date > bb.oper_date ";

            strOrderSql = string.Format(strOrderSql, patient.ID);

            int orderCount = FS.FrameWork.Function.NConvert.ToInt32(inpatient.ExecSqlReturnOne(strOrderSql, "0"));

            if (orderCount > 0)
            {
                stopMessage += "患者：" + patient.Name + "在转科后已经开立" + orderCount + "条医嘱！\n" + "无法直接返回原科室，请联系医生开立转科医嘱进行正常转科操作！";
                MessageBox.Show(stopMessage);
                return -1;
            }
            #endregion

            #region 判断费用，如果发生床位费日计费等需要把患者费用退掉然后才能转回原科室
            string strFeeSql = @"select sum(c.pub_cost + c.pay_cost + c.own_cost) from fin_ipb_feeinfo c,(select b.clinic_no,b.new_data_code,b.oper_date
          from com_shiftdata b
         where b.shift_type = 'RO'
           and b.clinic_no = '{0}'
           and rownum = 1
           order by b.oper_date desc) bb  where
           c.inpatient_no = '{0}' 
           and c.recipe_deptcode = '{1}'
           and c.inpatient_no = bb.clinic_no(+)
           and c.recipe_deptcode = bb.new_data_code(+)
           and c.charge_date > bb.oper_date";

            strFeeSql = string.Format(strFeeSql, patient.ID, patient.PVisit.PatientLocation.Dept.ID);

            decimal feeCost = FS.FrameWork.Function.NConvert.ToDecimal(inpatient.ExecSqlReturnOne(strFeeSql, "0"));
            if (feeCost > 0)
            {
                stopMessage += "患者：" + patient.Name + "在转科后已经发生" + feeCost + "元费用！\n" + "直接返回科室前请先退掉本次入科产生的日计费等项目信息！";
                MessageBox.Show(stopMessage);
                return -1;
            }

            #endregion

            return 1;
        }

        private FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// 转科自动停止长嘱的停止医生
        /// </summary>
        private AotuDcDoct autoDcDoct = AotuDcDoct.ExecutOutDoct;

        /// <summary>
        /// 转科自动停止长嘱的停止医生
        /// </summary>
        [Category("控件设置"), Description("转科自动停止长嘱的停止医生")]
        public AotuDcDoct AutoDcDoct
        {
            get
            {
                return autoDcDoct;
            }
            set
            {
                autoDcDoct = value;
            }
        }

        /// <summary>
        /// 是否使用转科自动停止医嘱功能
        /// </summary>
        private bool isUseShiftAutoDcOrder = false;

        /// <summary>
        /// 是否使用转科自动停止医嘱功能
        /// </summary>
        [Category("转科设置"), Description("是否使用转科自动停止医嘱功能，使用后必须开立转科医嘱才能转科！")]
        public bool IsUseShiftAutoDcOrder
        {
            get
            {
                return isUseShiftAutoDcOrder;
            }
            set
            {
                isUseShiftAutoDcOrder = value;
            }
        }

        /// <summary>
        /// 是否转科自动停止医嘱
        /// </summary>
        private bool isAutoDcOrder = false;

        /// <summary>
        /// 是否转科自动停止医嘱
        /// </summary>
        public bool IsAutoDcOrder
        {
            get
            {
                return isAutoDcOrder;
            }
            set
            {
                isAutoDcOrder = value;
            }
        }

        /// <summary>
        /// 转科自动停止全部长嘱
        /// </summary>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private int AutoDcOrder(ref string errInfo)
        {
            //开立转科医嘱的医生
            if (autoDcDoct == AotuDcDoct.ExecutOutDoct)
            {
                FS.HISFC.Models.Order.Inpatient.Order orderObj = null;
                int rev = this.orderIntegrate.GetShiftOutOrderType(patientInfo.ID, ref orderObj);
                if (rev == -1)
                {
                    errInfo = orderIntegrate.Err;
                    return -1;
                }
                else if (rev == 0)
                {
                    errInfo = "患者【" + patientInfo.Name + "】还未开立转科医嘱！";
                    return -1;
                }

                if (orderObj == null || orderObj.ReciptDoctor == null || string.IsNullOrEmpty(orderObj.ReciptDoctor.ID))
                {
                    errInfo = "患者【" + patientInfo.Name + "】还未开立转科医嘱！";
                    return -1;
                }

                if (this.orderIntegrate.AutoDcOrder(patientInfo.ID, orderObj.ReciptDoctor, this.inpatient.Operator, "", "转科自动停止") == -1)
                {
                    errInfo = this.orderIntegrate.Err;
                    return -1;
                }
            }
            //主任医生
            else if (autoDcDoct == AotuDcDoct.ExecutOutDoct)
            {
                if (this.patientInfo.PVisit.AttendingDirector == null ||
                    string.IsNullOrEmpty(patientInfo.PVisit.AttendingDirector.ID))
                {
                    errInfo = "患者【" + patientInfo.Name + "】没有维护主任医师，不能自动停止医嘱！";
                    return -1;
                }

                if (this.orderIntegrate.AutoDcOrder(patientInfo.ID, patientInfo.PVisit.AttendingDirector, this.inpatient.Operator, "", "转科自动停止") == -1)
                {
                    errInfo = this.orderIntegrate.Err;
                    return -1;
                }
            }
            //管床医生
            else if (autoDcDoct == AotuDcDoct.ExecutOutDoct)
            {
                if (this.orderIntegrate.AutoDcOrder(patientInfo.ID, patientInfo.PVisit.AdmittingDoctor, this.inpatient.Operator, "", "转科自动停止") == -1)
                {
                    errInfo = this.orderIntegrate.Err;
                    return -1;
                }
            }

            return 1;
        }

        #endregion

        #region ITransferDeptApplyable 成员
        bool bSaveAndClose = false;
        DialogResult dialogResult = DialogResult.None;

        public FS.FrameWork.Models.NeuObject Dept
        {
            get
            {
                return this.cmbNewDept.SelectedItem;
            }
        }

        public void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.InitControl();
            this.patientInfo = patientInfo.Clone();
            RefreshList(this.patientInfo);
        }

        public DialogResult ShowDialog()
        {
            bSaveAndClose = true;
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(this);
            return dialogResult;

        }

        private void cbxModefyNurse_CheckedChanged(object sender, EventArgs e)
        {
            cmbNewNurse.Enabled = cbxModefyNurse.Checked;
        }


        #endregion

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { typeof(FS.HISFC.BizProcess.Interface.ITransferDeptApplyable), 
                                    typeof(FS.HISFC.BizProcess.Interface.IPatientShiftValid) };
            }
        }

        #endregion
    }
}
