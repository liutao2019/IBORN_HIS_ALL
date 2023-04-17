using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// 医嘱授权
    /// </summary>
    public partial class ucPermissionInput : UserControl
    {
        public ucPermissionInput()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                try
                {
                    this.cmbDept.AddItems(SOC.HISFC.BizProcess.Cache.Common.GetDept());
                }
                catch
                {
                }
                this.cmbDoctor.IsListOnly = true;
                this.cmbDept.IsListOnly = true;
            }
        }

        #region 变量

        /// <summary>
        /// 医嘱授权管理
        /// </summary>
        private FS.HISFC.BizLogic.Order.Permission permissionMgr = new FS.HISFC.BizLogic.Order.Permission();

        /// <summary>
        /// 会诊实体
        /// </summary>
        protected FS.HISFC.Models.Order.Consultation permission = new FS.HISFC.Models.Order.Consultation();

        /// <summary>
        /// 会诊实体
        /// </summary>
        public FS.HISFC.Models.Order.Consultation Permission
        {
            get
            {
                return this.permission;
            }
            set
            {
                this.permission = value;
                this.SetValue();
            }
        }

        #endregion

        /// <summary>
        /// 显示
        /// </summary>
        private void SetValue()
        {
            if (this.permission == null) return;
            try
            {
                this.cmbDept.Tag = this.permission.DeptConsultation.ID;
                this.cmbDoctor.Tag = this.permission.DoctorConsultation.ID;
                this.dtBegin.Value = this.permission.BeginTime;
                this.dtEnd.Value = this.permission.EndTime;
                this.txtMemo.Text = this.permission.Name;
            }
            catch { }
        }

        /// <summary>
        /// 获取授权信息
        /// </summary>
        /// <returns></returns>
        private int GetValue()
        {
            if (this.cmbDept.Tag == null || this.cmbDept.Text == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请输入授权科室"));
                return -1;
            }
            //if (this.cmbDoctor.Tag == null || this.cmbDoctor.Text == "")
            //{
            //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请输入授权医生"));
            //    return -1;
            //}
            System.Collections.ArrayList list = this.permissionMgr.QueryPermission(this.permission.InpatientNo);
            if (list == null)
            {
                MessageBox.Show("获取授权信息失败" + this.permissionMgr.Err);
                return -1;
            }
            FS.HISFC.Models.RADT.PatientInfo patientObj = CacheManager.RadtIntegrate.GetPatientInfomation(this.permission.InpatientNo);
            if (patientObj == null)
            {
                MessageBox.Show("获取病人信息失败" + CacheManager.RadtIntegrate.Err);
                return -1;
            }
            if (cmbDoctor.Tag.ToString() == patientObj.PVisit.AdmittingDoctor.ID)
            {
                MessageBox.Show(cmbDoctor.Text + " 是分管医生 ，不需要再分配");
                return -1;
            }
            if (list.Count > 0)
            {
                foreach (FS.HISFC.Models.Order.Consultation tem in list)
                {
                    if (tem.DoctorConsultation.ID == cmbDoctor.Tag.ToString() && tem.ID != permission.ID)
                    {
                        if (MessageBox.Show(cmbDoctor.Text + " 在" + tem.BeginTime.ToString() + " - " + tem.EndTime.ToString() + "期间已经有权限" + ",需要重新分配","警告",MessageBoxButtons.OK,MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
                        {
                            return -1;
                        }
                    }
                }
            }

            if (this.permission == null)
            {
                this.permission = new FS.HISFC.Models.Order.Consultation();
            }

            this.permission.DeptConsultation.ID = this.cmbDept.Tag.ToString();
            this.permission.DoctorConsultation.ID = this.cmbDoctor.Tag.ToString();
            this.permission.DoctorConsultation.Name = this.cmbDoctor.Text;
            this.permission.BeginTime = this.dtBegin.Value;
            this.permission.EndTime = DateTime.Parse(this.dtEnd.Value.ToShortDateString() + " 23:59:59");
            if (this.permission.BeginTime > this.permission.EndTime)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("处方授权开始时间不能大于结束时间"));
                return -1;
            }
            string memo = this.txtMemo.Text.Trim();
            if (FS.FrameWork.Public.String.ValidMaxLengh(memo, 20) == false)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("授权说明不能超过10个汉字!"));
                return -1;
            }
            this.permission.Name = memo;
            
            return 0;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            if (this.GetValue() == -1)
            {
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            permissionMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            if (this.permission.ID == "")
            {
                if (permissionMgr.InsertPermission(this.permission) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg(permissionMgr.Err));
                    return -1;
                }
            }
            else
            {
                if (permissionMgr.UpdatePermission(this.permission) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg(permissionMgr.Err));
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存成功"));
            return 0;
        }

        /// <summary>
        /// 验证时间
        /// </summary>
        /// <returns></returns>
        private bool valid()
        {
            TimeSpan s = new TimeSpan(dtEnd.Value.Ticks - dtBegin.Value.Ticks);
            if (s.Ticks < 0)
            {
                MessageBox.Show("开始时间小于最后时间!");
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.valid())
            {
                return;
            }
            if (this.Save() == 0)
            {
                this.FindForm().Close();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.cmbDoctor.alItems = null;
                this.cmbDoctor.Text = "";
                this.cmbDoctor.Tag = "";
                this.cmbDoctor.AddItems(CacheManager.InterMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D, this.cmbDept.Tag.ToString()));
            }
            catch
            {
                MessageBox.Show(this.cmbDept.Text + "没有医生！");
            }
        }
    }
}
