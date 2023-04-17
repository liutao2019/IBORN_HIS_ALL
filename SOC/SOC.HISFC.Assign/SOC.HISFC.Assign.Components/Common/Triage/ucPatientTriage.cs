using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.SOC.HISFC.Assign.Components.Base;

namespace FS.SOC.HISFC.Assign.Components.Common.Triage
{    
    /// <summary>
    /// [功能描述: 患者分诊信息显示]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    internal partial class ucPatientTriage : ucAssignBase
    {
        public ucPatientTriage()
        {
            InitializeComponent();
        }

        #region 属性

        /// <summary>
        /// 患者挂号信息
        /// </summary>
        public FS.HISFC.Models.Registration.Register Register
        {
            get
            {
                return this.txtCard.Tag as FS.HISFC.Models.Registration.Register;
            }
            set
            {
                this.setItem(value);
            }
        }

        private FS.SOC.HISFC.Assign.Models.Assign assign;
        /// <summary>
        /// 患者分诊信息
        /// </summary>
        public FS.SOC.HISFC.Assign.Models.Assign Assign
        {
            get
            {
                return assign;
            }
        }

        private bool isOK = false;
        public bool IsOK
        {
            get
            {
                return isOK;
            }
        }

        #endregion

        #region 触发事件

        protected override void OnLoad(EventArgs e)
        {
            this.setFocus();
            this.isOK = false;
            base.OnLoad(e);
        }

        private void cmbQueue_SelectedIndexChanged(object sender, EventArgs e)
        {
            FS.FrameWork.Models.NeuObject obj = this.cmbQueue.SelectedItem;
            if (obj == null) return;
            if (obj is FS.SOC.HISFC.Assign.Models.Queue)
            {
                this.txtRoom.Text = (obj as FS.SOC.HISFC.Assign.Models.Queue).SRoom.Name;
            }
            else
            {
                this.txtRoom.Text = obj.User03;
            }
        }

        private void cmbQueue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string inputText = this.cmbQueue.Text.Trim();
                if (inputText.Length < 6)
                {
                    inputText = inputText.PadLeft(6, '0');
                }
                ArrayList al = this.cmbQueue.alItems;
                if (al != null)
                {
                    for (int i = 0; i < al.Count; i++)
                    {
                        FS.SOC.HISFC.Assign.Models.Queue queobj = al[i] as FS.SOC.HISFC.Assign.Models.Queue;
                        if (queobj.Doctor.ID == inputText)
                        {
                            this.cmbQueue.SelectedIndex = i;
                            break;
                        }
                    }
                }

                this.btnSave.Focus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (save() >0)
            {
                this.FindForm().Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        #endregion

        #region 方法

        private void setItem(FS.HISFC.Models.Registration.Register register)
        {
            //赋值
            this.txtCard.Text = register.PID.PatientNO;
            this.txtName.Text = register.Name;
            this.txtDept.Text = register.DoctorInfo.Templet.Dept.Name;
            this.txtRegDate.Text = register.DoctorInfo.SeeDate.ToString();
            this.txtCard.Tag = register;

            //判断是不是专家号
            FS.HISFC.Models.Registration.RegLevel regLevel =FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetRegLevel(register.DoctorInfo.Templet.RegLevel.ID);

            for (int i = 0; i < this.cmbQueue.alItems.Count; i++)
            {
                FS.SOC.HISFC.Assign.Models.Queue queue = this.cmbQueue.alItems[i] as FS.SOC.HISFC.Assign.Models.Queue;
                register.RegLvlFee.RegLevel.IsExpert = regLevel.IsExpert;
                //全都符合
                if (queue.IsExpert == register.RegLvlFee.RegLevel.IsExpert
                    &&
                    queue.Doctor.ID == register.DoctorInfo.Templet.Doct.ID
                    &&
                    queue.AssignDept.ID == register.DoctorInfo.Templet.Dept.ID)
                {
                    this.cmbQueue.Tag = queue.ID;
                    return;
                }
            }

            for (int i = 0; i < this.cmbQueue.alItems.Count; i++)
            {
                FS.SOC.HISFC.Assign.Models.Queue queue =  this.cmbQueue.alItems[i] as FS.SOC.HISFC.Assign.Models.Queue;

                register.RegLvlFee.RegLevel.IsExpert = regLevel.IsExpert;

                //号别相同（指是否专家）科室相同
                if (queue.IsExpert== register.RegLvlFee.RegLevel.IsExpert
                    &&
                    queue.AssignDept.ID == register.DoctorInfo.Templet.Dept.ID)
                {
                    this.cmbQueue.Tag = queue.ID;
                    return;
                }

            }

        }

        private void setFocus()
        {
            this.cmbQueue.Select();
            this.cmbQueue.Focus();
        }

        private int save()
        {
            if(this.Register==null)
            {
                return -1;
            }

            FS.FrameWork.Models.NeuObject obj = this.cmbQueue.SelectedItem;
            if (obj == null)
            {
                CommonController.CreateInstance().MessageBox(this, "请选择分诊队列!", MessageBoxIcon.Asterisk);
                this.setFocus();
                return -1;
            }

            FS.SOC.HISFC.Assign.Models.Queue queueinfo = (FS.SOC.HISFC.Assign.Models.Queue)this.cmbQueue.SelectedItem;
            assign = new FS.SOC.HISFC.Assign.Models.Assign();
            assign.Register = this.Register;
            assign.Queue = obj as FS.SOC.HISFC.Assign.Models.Queue;

            if (assign.Queue.IsExpert && assign.Register.DoctorInfo.Templet.RegLevel.IsExpert == false)
            {
                if (CommonController.CreateInstance().MessageBox(this, "普通号进诊专家队列，是否继续？", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return -1;
            }
            if (assign.Queue.IsExpert == assign.Register.DoctorInfo.Templet.RegLevel.IsExpert
                && string.IsNullOrEmpty(this.Register.DoctorInfo.Templet.Doct.ID) == false
                && assign.Queue.Doctor.ID != this.Register.DoctorInfo.Templet.Doct.ID)
            {
                if (CommonController.CreateInstance().MessageBox(this, "选择医师与挂号医师不一致，是否继续？", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return -1;
            }
            if (assign.Queue.IsExpert ==false && assign.Register.DoctorInfo.Templet.RegLevel.IsExpert == true)
            {
                if (CommonController.CreateInstance().MessageBox(this, "专家挂号进诊普通队列，是否继续？", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) 
                    return -1;
            }
            
            this.isOK = true;
            return 1;
        }

        #endregion

        #region 初始化

        public int Init(ArrayList alQueue)
        {
            this.cmbQueue.AddItems(alQueue);
            if (this.cmbQueue.Items.Count > 0)
            {
                this.cmbQueue.SelectedIndex = 0;
            }
            return 1;
        }

        public void Clear()
        {
            this.txtCard.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.txtDept.Text = string.Empty;
            this.txtRegDate.Text = string.Empty;
            this.txtCard.Tag = null;
            this.cmbQueue.ClearItems();
        }

        #endregion
    }
}
