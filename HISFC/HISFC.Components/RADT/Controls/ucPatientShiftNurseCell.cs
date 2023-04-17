using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// {9A2D53D3-25BE-4630-A547-A121C71FB1C5}
    /// [功能描述: 转病区申请，取消控件]<br></br>
    /// [创 建 者: Sunm]<br></br>
    /// [创建时间: 2009-07-09]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    /// </summary>
    public partial class ucPatientShiftNurseCell : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ucPatientShiftNurseCell()
        {
            InitializeComponent();
            cmbNewDept.SelectedIndexChanged += new EventHandler(cmbNewDept_SelectedIndexChanged);
        }

        #region 变量

        /// <summary>
        /// 科室病区业务类
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 患者业务类
        /// </summary>
        FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 患者信息
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        FS.HISFC.BizProcess.Integrate.Common.ControlParam ctlParamManage = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        //需要提示选择的东东
        string checkMessage = "";

        //提示禁止的东东
        string stopMessage = "";
        #region addby xuewj IADT接口

        FS.HISFC.BizProcess.Interface.IHE.IADT adt = null;

        #endregion


        #endregion

        #region 属性

        private bool isCancel = false;
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
            }
        }


        private CheckState isCanWhenUnConfirmOrder = CheckState.Check;
        /// <summary>
        /// 存在未审核的医嘱是否允许办理转病区
        /// </summary>
        public CheckState IsCanWhenUnConfirmOrder
        {
            get
            {
                return this.isCanWhenUnConfirmOrder;
            }
            set
            {
                this.isCanWhenUnConfirmOrder = value;
            }
        }
        #endregion

        /// <summary>
        /// 是否显示
        /// </summary>
        private bool isShowShiftNurse = false;
        public bool IsShowShiftNurse
        {
            get
            {
                return this.isShowShiftNurse;
            }
            set
            {
                this.isShowShiftNurse = value;
            }
        }
        #region 函数

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {

            try
            {
                //{4C8D1E46-9C15-47dc-89B2-2CE58B934E17}
                //ArrayList al = new ArrayList();
                //al = manager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.N);
                //this.cmbNewDept.AddItems(al);
            }
            catch { }

        }

        /// <summary>
        /// 将患者信息显示在控件中
        /// </summary>
        private void ShowPatientInfo()
        {
            this.txtPatientNo.Text = this.patientInfo.PID.PatientNO;		//住院号
            this.txtPatientNo.Tag = this.patientInfo.ID;							//住院流水号
            this.txtName.Text = this.patientInfo.Name;								//患者姓名
            this.txtSex.Text = this.patientInfo.Sex.Name;					//性别
            this.txtOldDept.Text = this.patientInfo.PVisit.PatientLocation.NurseCell.Name;//源科室名称
            this.cmbBedNo.Text = this.patientInfo.PVisit.PatientLocation.Bed.ID.Length > 4 ? this.patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) : "";	//床号
            //定义患者Location实体
            FS.HISFC.Models.RADT.Location newLocation = new FS.HISFC.Models.RADT.Location();
            //取患者转科申请信息
            newLocation = this.inpatientManager.QueryShiftNewLocation(this.patientInfo.ID, this.patientInfo.PVisit.PatientLocation.Dept.ID);
            this.patientInfo.User03 = newLocation.User03;
            if (this.patientInfo.User03 == null)
                this.patientInfo.User03 = "1";//申请

            if (this.isCanWhenUnConfirmOrder != CheckState.Yes)
            {
                string msg = FS.HISFC.Components.RADT.Classes.Function.CheckAllOrderUnConfirm(patientInfo.ID);
                if (!string.IsNullOrEmpty(msg))
                {
                    if (isCanWhenUnConfirmOrder == CheckState.Check)// {06A6BD92-97A6-4f01-8B6E-533B35CBF2DD}
                    {
                        checkMessage += "\r\n\r\n★存在未审核医嘱\r\n" + msg;
                    }
                    else if (isCanWhenUnConfirmOrder == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n★存在未审核医嘱\r\n" + msg;
                    }
                }

            }
            this.label1.Text = stopMessage;
            if (newLocation == null)
            {
                MessageBox.Show(this.inpatientManager.Err);
                return;
            }

            this.cmbNewDept.Tag = newLocation.NurseCell.ID;	//新科室名称
            this.txtNote.Text = newLocation.Memo;		//备注
            //如果没有转科申请,则清空新科室编码
            if (newLocation.NurseCell.ID == "")
            {
                this.cmbNewDept.Text = null;
            }
            if (this.patientInfo.User03 != null && this.patientInfo.User03 == "0")
                this.label8.Visible = true;
            else
                this.label8.Visible = false;

            //{62BB0773-5C87-4458-9EA9-A8196C5D3EF4}
            ArrayList alNurse = this.QueryNurseByDept(this.patientInfo.PVisit.PatientLocation.Dept.ID);
            this.cmbNewDept.Items.Clear();
            this.cmbNewDept.AddItems(alNurse);
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
        /// 清屏
        /// </summary>
        public void ClearPatintInfo()
        {
            this.cmbNewDept.Text = "";
            this.cmbNewDept.Tag = "";
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.patientInfo = neuObject as FS.HISFC.Models.RADT.PatientInfo;
            RefreshList(this.patientInfo);
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            //{4C8D1E46-9C15-47dc-89B2-2CE58B934E17}
            //this.InitControl();
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.cmbNewDept.Tag == null || this.cmbNewDept.Tag.ToString() == "")
            {
                MessageBox.Show("请选择要转入的病区!");
                return;
            }
            //{3D9A4F05-98F9-450b-B0EE-CCEBCED15C6F}
            if (this.patientInfo.IsBaby)
            {
                DialogResult result = MessageBox.Show("是否继续？转科室时禁止单独转移宝宝，若需要转科，请选择母亲转移!","请核对：",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {

                }
                else
                {
                    return;
                }
            }
            // {7FFE7A7E-239D-4019-97B4-D3F80BB79713}
            if (!this.patientInfo.IsBaby && this.patientInfo.IsHasBaby)
            {
                ArrayList alBabys = this.inpatientManager.QueryBabiesByMother(this.patientInfo.ID);
                if (alBabys.Count > 0)
                {
                    foreach (FS.HISFC.Models.RADT.PatientInfo obj in alBabys)
                    {
                        FS.HISFC.Models.RADT.PatientInfo babyInfo = new FS.HISFC.Models.RADT.PatientInfo();
                        babyInfo = this.inpatientManager.QueryPatientInfoByInpatientNO(obj.ID);
                        if (babyInfo.PVisit.PatientLocation.NurseCell.ID == this.patientInfo.PVisit.PatientLocation.NurseCell.ID)
                        {
                            DialogResult rev = MessageBox.Show("该患者有婴儿在同一个病区，是否一起跟着转病区？", "询问", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (rev == DialogResult.Cancel)
                            {
                                return;
                            }
                            else if (rev == DialogResult.Yes)
                            {
                                this.patientInfo.User01 = "1";

                            }
                            else
                            {
                                this.patientInfo.User01 = "0";
                            }

                        }
                    }
                }
            }
            FS.HISFC.Components.RADT.Forms.frmMessageShow frmMessage = new FS.HISFC.Components.RADT.Forms.frmMessageShow();
            frmMessage.Clear();
            if (!string.IsNullOrEmpty(checkMessage))// {06A6BD92-97A6-4f01-8B6E-533B35CBF2DD}
            {
                frmMessage.SetPatientInfo(patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 患者【" + patientInfo.Name + "】");
                frmMessage.SetTipMessage("存在以下问题未处理,是否继续办理转科？");
                frmMessage.SetMessage(checkMessage);
                frmMessage.SetPerfectWidth();
                if (frmMessage.ShowDialog() == DialogResult.No)
                {
                    return;
                }
            }

            if (!string.IsNullOrEmpty(stopMessage))
            {
                frmMessage.SetPatientInfo(patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 患者【" + patientInfo.Name + "】");
                frmMessage.SetTipMessage("存在以下问题未处理,不能继续办理转科！");
                frmMessage.SetMessage(stopMessage);
                frmMessage.SetPerfectWidth();
                frmMessage.HideNoButton();
                frmMessage.ShowDialog();
                return;
            }
            FS.FrameWork.Models.NeuObject nurseCell = new FS.FrameWork.Models.NeuObject();

            nurseCell.ID = this.cmbNewDept.Tag.ToString();
            nurseCell.Name = this.cmbNewDept.Text;
            nurseCell.Memo = this.txtNote.Text;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        
            FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();

            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            if (radt.ShiftOut(this.patientInfo, this.patientInfo.PVisit.PatientLocation.Dept,nurseCell, this.patientInfo.User03, this.isCancel) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
            }
            else
            {
                #region addby xuewj 
                if (this.adt == null)
                {
                    this.adt = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IHE.IADT)) as FS.HISFC.BizProcess.Interface.IHE.IADT;
                }
                if (this.adt != null && patientInfo != null)
                {
                    this.adt.CancelTransferPatient(patientInfo);
                }
                #endregion
                FS.FrameWork.Management.PublicTrans.Commit();
                
            }
            MessageBox.Show(radt.Err);

            base.OnRefreshTree();//刷新树
        }

        /// <summary>
        /// 根据科室找到病区{D2B432AC-723A-4a54-88CA-690507CEC1B9}
        /// 字典是{F41B3E83-2136-4939-921F-8339C96C181E}
        /// select t.rowid,t.* from com_controlargument t where t.control_code  like '%NOCHAR%'; 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbNewDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(this.cmbNewDept.Tag.ToString()))
            {
                return;
            }
            #region 判断限制转科的病区和其他病区之间不能互转
            string noShiftDept = this.ctlParamManage.GetControlParam<string>("NOCHAR");


            //如果选中的和现住的病区都是限制性的，则可以互转
            if (noShiftDept.Contains(this.cmbNewDept.Tag.ToString())
                && noShiftDept.Contains(this.patientInfo.PVisit.PatientLocation.NurseCell.ID))
            {
                return;
            }

            //如果都不是限制性的部门，则可以互转
            if (!noShiftDept.Contains(this.cmbNewDept.Tag.ToString())
                && !noShiftDept.Contains(this.patientInfo.PVisit.PatientLocation.NurseCell.ID))
            {
                return;
            }

            MessageBox.Show("不允许从【" + this.patientInfo.PVisit.PatientLocation.NurseCell.Name + "】转病区到【" + this.cmbNewDept.Text + "】");
            this.cmbNewDept.Tag = this.patientInfo.PVisit.PatientLocation.NurseCell.ID;
            return;
            /*

            //判断选中科室，如果是限制转科的，就不让转，除非当前病区也是限制病区，限制病区之间可以互转
            if (!string.IsNullOrEmpty(this.cmbNewDept.Tag.ToString())
                && noShiftDept.Contains(this.cmbNewDept.Tag.ToString())
                && this.patientInfo.PVisit.PatientLocation.NurseCell.ID != this.cmbNewDept.Tag.ToString()
                && !noShiftDept.Contains(this.patientInfo.PVisit.PatientLocation.NurseCell.ID))
            {
                MessageBox.Show("不允许从【" + this.patientInfo.PVisit.PatientLocation.NurseCell.Name + "】转病区到【" + this.cmbNewDept.Text + "】");
                this.cmbNewDept.Tag = this.patientInfo.PVisit.PatientLocation.NurseCell.ID;
                return;
            }

            //本科室是限制转科的，就不让转其他科室
            if (!string.IsNullOrEmpty(this.cmbNewDept.Tag.ToString())
                && !noShiftDept.Contains(this.patientInfo.PVisit.PatientLocation.NurseCell.ID)
                //&& this.patientInfo.PVisit.PatientLocation.NurseCell.ID != this.cmbNewDept.Tag.ToString()
                )
            {
                MessageBox.Show("不允许从【" + this.patientInfo.PVisit.PatientLocation.NurseCell.Name + "】转病区到【" + this.cmbNewDept.Text + "】");
                this.cmbNewDept.Tag = this.patientInfo.PVisit.PatientLocation.NurseCell.ID;
                return;
            }*/

            #endregion

        }
        #endregion

        #region IInterfaceContainer 成员

        /// <summary>
        /// 接口容器
        /// </summary>
        public Type[] InterfaceTypes
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        #endregion
    }
}
