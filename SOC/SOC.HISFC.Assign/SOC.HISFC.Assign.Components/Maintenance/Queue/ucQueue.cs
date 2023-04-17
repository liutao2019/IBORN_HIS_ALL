using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.Assign.Interface.Components;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.FrameWork.Public;
using FS.SOC.HISFC.Assign.Models;

namespace FS.SOC.HISFC.Assign.Components.Maintenance.Queue
{
    /// <summary>
    /// [功能描述: 队列界面]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public partial class ucQueue : FS.FrameWork.WinForms.Controls.ucBaseControl, IMaintenance<KeyValuePair<FS.SOC.HISFC.Assign.Models.Queue, Day>>
    {
        public ucQueue()
        {
            InitializeComponent();
        }

        #region 事件变量

        FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<KeyValuePair<FS.SOC.HISFC.Assign.Models.Queue, Day>> saveInfo = null;

        FS.SOC.HISFC.Assign.BizLogic.Queue queueMgr = new FS.SOC.HISFC.Assign.BizLogic.Queue();
        FS.SOC.HISFC.Assign.BizLogic.QueueTemplate queueTemplateMgr = new FS.SOC.HISFC.Assign.BizLogic.QueueTemplate();

        #endregion

        #region 变量

        private Day weekday = Day.Monday;
        private FS.SOC.HISFC.Assign.BizProcess.Queue queueBiz = new FS.SOC.HISFC.Assign.BizProcess.Queue();
        private FS.SOC.HISFC.Assign.BizProcess.QueueTemplate queueTemplateBiz = new FS.SOC.HISFC.Assign.BizProcess.QueueTemplate();
        private FS.SOC.HISFC.Assign.BizProcess.Room roomBiz = new FS.SOC.HISFC.Assign.BizProcess.Room();

        #endregion

        #region 初始化

        private void initEvents()
        {
            this.btnOK.Click -= new EventHandler(btnOK_Click);
            this.btnOK.Click += new EventHandler(btnOK_Click);

            this.btnExit.Click -= new EventHandler(btnExit_Click);
            this.btnExit.Click += new EventHandler(btnExit_Click);

            this.cmbNurseStation.SelectedIndexChanged -= new EventHandler(cmbNurseStation_SelectedIndexChanged);
            this.cmbNurseStation.SelectedIndexChanged += new EventHandler(cmbNurseStation_SelectedIndexChanged);

            this.cmbDept.SelectedIndexChanged -= new EventHandler(cmbDept_SelectedIndexChanged);
            this.cmbDept.SelectedIndexChanged += new EventHandler(cmbDept_SelectedIndexChanged);

            this.cmbRoom.SelectedIndexChanged -= new EventHandler(cmbRoom_SelectedIndexChanged);
            this.cmbRoom.SelectedIndexChanged += new EventHandler(cmbRoom_SelectedIndexChanged);

            this.cmbDoct.SelectedIndexChanged -= new EventHandler(cmb_SelectedIndexChanged);
            this.cmbDoct.SelectedIndexChanged += new EventHandler(cmb_SelectedIndexChanged);

            this.cmbQueueType.SelectedIndexChanged -= new EventHandler(cmb_SelectedIndexChanged);
            this.cmbQueueType.SelectedIndexChanged += new EventHandler(cmb_SelectedIndexChanged);

            this.cmbRegLevel.SelectedIndexChanged -= new EventHandler(cmb_SelectedIndexChanged);
            this.cmbRegLevel.SelectedIndexChanged += new EventHandler(cmb_SelectedIndexChanged);

            this.cmbRoom.SelectedIndexChanged -= new EventHandler(cmb_SelectedIndexChanged);
            this.cmbRoom.SelectedIndexChanged += new EventHandler(cmb_SelectedIndexChanged);

            this.cmbConsole.SelectedIndexChanged -= new EventHandler(cmb_SelectedIndexChanged);
            this.cmbConsole.SelectedIndexChanged += new EventHandler(cmb_SelectedIndexChanged);

            this.cmbPatientConditionType.SelectedIndexChanged -= new EventHandler(cmbPatientConditionType_SelectedIndexChanged);
            this.cmbPatientConditionType.SelectedIndexChanged += new EventHandler(cmbPatientConditionType_SelectedIndexChanged);
        }

        void cmbPatientConditionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbQueueType.SelectedItem == null || string.IsNullOrEmpty(this.cmbQueueType.SelectedItem.ID))
            {
                this.setQueueName(EnumQueueType.Custom);
            }
            else
            {
                this.setQueueName(EnumHelper.Current.GetEnum<EnumQueueType>(this.cmbQueueType.SelectedItem.ID));
            }
        }

        private void initDatas()
        {
            this.dtQueue.Value = CommonController.CreateInstance().GetSystemTime();
            this.cmbNoon.AddItems(CommonController.CreateInstance().QueryNoon());
            this.cmbQueueType.AddItems(EnumHelper.Current.EnumArrayList<EnumQueueType>());

            this.cmbDoct.AddItems(CommonController.CreateInstance().QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D));
            this.cmbRegLevel.AddItems(FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().QueryRegLevel());

            this.cmbPatientConditionType.AddItems(CommonController.CreateInstance().QueryConstant("PatientConditionType"));
            if (cmbPatientConditionType.Items.Count > 0)
            {
                cmbPatientConditionType.SelectedIndex = 0;
            }
        }

        #endregion

        #region 方法

        private bool isValid()
        {
            if (this.cmbNurseStation.Tag == null || this.cmbNurseStation.Text.Trim().Length == 0)
            {
                CommonController.CreateInstance().MessageBox(this, "护士站不能为空", MessageBoxIcon.Error);
                this.cmbNurseStation.Select();
                this.cmbNurseStation.Focus();
                return false;
            }

            if (this.cmbDept.Tag == null || this.cmbDept.Text.Trim().Length == 0)
            {
                CommonController.CreateInstance().MessageBox(this, "科室不能为空", MessageBoxIcon.Error);
                this.cmbDept.Select();
                this.cmbDept.Focus();
                return false;
            }

            if (weekday == Day.Default)
            {
                FS.SOC.HISFC.Assign.Models.Queue queue = null;
                if (this.Tag is FS.SOC.HISFC.Assign.Models.Queue)
                {
                    queue = this.Tag as FS.SOC.HISFC.Assign.Models.Queue;
                }

                if (queue != null && this.dtQueue.Value.Date != queue.QueueDate.Date)
                {
                    if (CommonController.CreateInstance().MessageBox(this, "队列日期不是当前选择的日期，是否继续", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
                    {
                        this.dtQueue.Select();
                        this.dtQueue.Focus();
                        return false;
                    }
                }
            }
            if (this.dtQueue.Value.Date < CommonController.CreateInstance().GetSystemTime().Date)
            {
                CommonController.CreateInstance().MessageBox(this, "已超过当前日期，不能增加或修改", MessageBoxIcon.Error);
                this.dtQueue.Select();
                this.dtQueue.Focus();
                return false;
            }

            if (this.cmbNoon.Tag == null || this.cmbNoon.Text.Trim().Length == 0)
            {
                CommonController.CreateInstance().MessageBox(this, "午别不能为空", MessageBoxIcon.Error);
                this.cmbNoon.Select();
                this.cmbNoon.Focus();
                return false;
            }

            if (this.cmbQueueType.Tag == null || this.cmbQueueType.Text.Trim().Length == 0)
            {
                CommonController.CreateInstance().MessageBox(this, "队列类型不能为空", MessageBoxIcon.Error);
                this.cmbQueueType.Select();
                this.cmbQueueType.Focus();
                return false;
            }

            if (EnumHelper.Current.GetEnum<EnumQueueType>(this.cmbQueueType.SelectedItem.ID) == EnumQueueType.Doctor)
            {
                if (this.cmbDoct.SelectedItem == null)
                {
                    CommonController.CreateInstance().MessageBox(this, "医生队列必须选择医生", MessageBoxIcon.Error);
                    this.cmbDoct.Select();
                    this.cmbDoct.Focus();
                    return false;
                }
                if (this.cmbRoom.SelectedItem == null)
                {
                    CommonController.CreateInstance().MessageBox(this, "医生队列必须选择诊室，否则叫号无法定位到诊室", MessageBoxIcon.Error);
                    this.cmbRoom.Select();
                    this.cmbRoom.Focus();
                    return false;
                }
                else if (this.cmbConsole.SelectedItem == null)
                {
                    CommonController.CreateInstance().MessageBox(this, "医生队列必须选择诊台，否则叫号无法定位到诊室", MessageBoxIcon.Error);
                    this.cmbConsole.Select();
                    this.cmbConsole.Focus();
                    return false;
                }
            }

            if (EnumHelper.Current.GetEnum<EnumQueueType>(this.cmbQueueType.SelectedItem.ID) == EnumQueueType.RegLevel)
            {
                if (this.cmbRegLevel.SelectedItem == null)
                {
                    CommonController.CreateInstance().MessageBox(this, "级别队列必须选择挂号级别", MessageBoxIcon.Error);
                    this.cmbRegLevel.Select();
                    this.cmbRegLevel.Focus();
                    return false;
                }

                if (this.cmbRoom.SelectedItem != null)
                {
                    CommonController.CreateInstance().MessageBox(this, "级别队列不允许选择诊室和诊台，否则会影响正常的诊室或医生队列！", MessageBoxIcon.Error);
                    this.cmbRoom.Select();
                    this.cmbRoom.Focus();
                    return false;
                }
                else if (this.cmbConsole.SelectedItem != null)
                {
                    CommonController.CreateInstance().MessageBox(this, "级别队列不允许选择诊室和诊台，否则会影响正常的诊室或医生队列！", MessageBoxIcon.Error);
                    this.cmbConsole.Select();
                    this.cmbConsole.Focus();
                    return false;
                }
                else if (this.cmbDoct.SelectedItem != null)
                {
                    CommonController.CreateInstance().MessageBox(this, "级别队列不允许选择选择医生，否则会影响正常的诊室或医生队列！", MessageBoxIcon.Error);
                    this.cmbDoct.Select();
                    this.cmbDoct.Focus();
                    return false;
                }
            }

            if (EnumHelper.Current.GetEnum<EnumQueueType>(this.cmbQueueType.SelectedItem.ID) == EnumQueueType.Custom)
            {
                if (this.cmbRoom.SelectedItem == null)
                {
                    CommonController.CreateInstance().MessageBox(this, "自定义队列必须选择诊室", MessageBoxIcon.Error);
                    this.cmbRoom.Select();
                    this.cmbRoom.Focus();
                    return false;
                }
                else if (this.cmbConsole.SelectedItem == null)
                {
                    CommonController.CreateInstance().MessageBox(this, "自定义队列必须选择诊台", MessageBoxIcon.Error);
                    this.cmbConsole.Select();
                    this.cmbConsole.Focus();
                    return false;
                }
            }

            if (this.tbQueueName.Text.Trim().Length == 0)
            {
                CommonController.CreateInstance().MessageBox(this, "队列名称不能为空", MessageBoxIcon.Error);
                this.tbQueueName.Select();
                this.tbQueueName.Focus();
                return false;
            }

            if (this.cmbPatientConditionType.SelectedItem == null)
            {
                CommonController.CreateInstance().MessageBox(this, "患者病情类别不允许为空", MessageBoxIcon.Error);
                this.cmbPatientConditionType.Select();
                this.cmbPatientConditionType.Focus();
                return false;
            }

            return true;
        }

        private FS.SOC.HISFC.Assign.Models.Queue getQueue()
        {
            FS.SOC.HISFC.Assign.Models.Queue queue = null;
            if (this.Tag is FS.SOC.HISFC.Assign.Models.Queue)
            {
                queue = this.Tag as FS.SOC.HISFC.Assign.Models.Queue;
            }
            else
            {
                queue = new FS.SOC.HISFC.Assign.Models.Queue();
            }

            if (this.cmbNurseStation.SelectedItem != null)
            {
                queue.AssignNurse = this.cmbNurseStation.SelectedItem;
            }
            else
            {
                queue.AssignNurse = new FS.FrameWork.Models.NeuObject();
            }

            if (this.cmbDept.SelectedItem != null)
            {
                queue.AssignDept = this.cmbDept.SelectedItem;
            }
            else
            {
                queue.AssignDept = new FS.FrameWork.Models.NeuObject();
            }

            //看诊医生
            if (this.cmbDoct.SelectedItem != null)
            {
                queue.Doctor.ID = this.cmbDoct.SelectedItem.ID;
            }
            else
            {
                queue.Doctor = new FS.FrameWork.Models.NeuObject();
            }

            //队列类别
            if (this.cmbQueueType.SelectedItem != null)
            {
                queue.QueueType = EnumHelper.Current.GetEnum<FS.SOC.HISFC.Assign.Models.EnumQueueType>(this.cmbQueueType.SelectedItem.ID);
            }
            //午别
            if (this.cmbNoon.SelectedItem != null)
            {
                queue.Noon.ID = this.cmbNoon.SelectedItem.ID;
            }
            else
            {
                queue.Noon = new FS.FrameWork.Models.NeuObject();
            }

            if (this.cmbRoom.SelectedItem != null)
            {
                queue.SRoom = this.cmbRoom.SelectedItem;
            }
            else
            {
                queue.SRoom = new FS.FrameWork.Models.NeuObject();
            }
            //诊台
            if (this.cmbConsole.SelectedItem != null)
            {
                queue.Console = this.cmbConsole.SelectedItem;
            }
            else
            {
                queue.Console = new FS.FrameWork.Models.NeuObject();
            }
            //挂号级别
            if (this.cmbRegLevel.SelectedItem != null)
            {
                queue.RegLevel = this.cmbRegLevel.SelectedItem;
            }
            else
            {
                queue.RegLevel = new FS.FrameWork.Models.NeuObject();
            }

            queue.Name = this.tbQueueName.Text;
            queue.Memo = this.tbMemo.Text;
            queue.QueueDate = this.dtQueue.Value;
            //显示顺序
            queue.Order = FS.FrameWork.Function.NConvert.ToInt32(this.tbSort.Text);
            //是否有效
            queue.IsValid = this.ckValid.Checked;
            //操作员
            queue.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;
            //是否专家
            queue.IsExpert = this.ckExpert.Checked;
            //患者病情类别
            queue.PatientConditionType = this.cmbPatientConditionType.SelectedItem.ID;

            return queue;
        }

        private int Save()
        {
            FS.SOC.HISFC.Assign.Models.Queue queue = this.getQueue();

            if (weekday == Day.Default)//队列维护
            {
                string error = "";
                if (queueBiz.SaveQueue(queue, string.IsNullOrEmpty(queue.ID) ? FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Insert : FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Update, ref error) <= 0)
                {
                    CommonController.CreateInstance().MessageBox(this, error, MessageBoxIcon.Error);
                    return -1;
                }

            }
            else//队列模板维护
            {
                FS.SOC.HISFC.Assign.Models.QueueTemplate queueTemplate = Function.Convert(queue);
                queueTemplate.WeekDay = EnumHelper.Current.GetEnum<DayOfWeek>(weekday.ToString());
                string error = "";
                if (queueTemplateBiz.SaveQueueTemplate(queueTemplate, string.IsNullOrEmpty(queueTemplate.ID) ? FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Insert : FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Update, ref error) <= 0)
                {
                    CommonController.CreateInstance().MessageBox(this, error, MessageBoxIcon.Error);
                    return -1;
                }
                queue.ID = queueTemplate.ID;
            }

            if (this.SaveInfo != null)
            {
                this.SaveInfo(new KeyValuePair<FS.SOC.HISFC.Assign.Models.Queue, Day>(queue, weekday));
            }
            return 1;
        }

        /// <summary>
        /// 设置队列的名称
        /// </summary>
        /// <param name="queueType"></param>
        private void setQueueName(FS.SOC.HISFC.Assign.Models.EnumQueueType queueType)
        {
            switch (queueType)
            {
                case EnumQueueType.Custom:
                    this.tbQueueName.Text = this.cmbRoom.Text + "  " + this.cmbConsole.Text + " " + this.cmbPatientConditionType.Text;
                    this.cmbDoct.Visible = true;
                    this.cmbRoom.Visible = true;
                    this.cmbConsole.Visible = true;
                    break;
                case EnumQueueType.Doctor:
                    this.tbQueueName.Text = this.cmbDoct.Text + " " + this.cmbRoom.Text + " " + this.cmbPatientConditionType.Text;
                    this.cmbDoct.Visible = true;
                    this.cmbRoom.Visible = true;
                    this.cmbConsole.Visible = true;
                    break;
                case EnumQueueType.RegLevel:
                    this.tbQueueName.Text = this.cmbRegLevel.Text + " " + this.cmbPatientConditionType.Text;
                    //this.cmbDoct.Visible = false;
                    //this.cmbRoom.Visible = false;
                    //this.cmbConsole.Visible = false;
                    break;
                default:
                    this.tbQueueName.Text = this.cmbRoom.Text + "  " + this.cmbConsole.Text + " " + this.cmbPatientConditionType.Text;
                    break;
            }
        }

        #endregion

        #region 触发事件

        void btnExit_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            if (!this.isValid())
            {
                return;
            }

            if (this.Save() <= 0)
            {
                return;
            }

            if (this.ckContinue.Checked == false)
            {
                this.ParentForm.Close();
            }
        }

        void cmbNurseStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbNurseStation.SelectedItem != null)
            {
                string error = "";
                this.cmbDept.ClearItems();
                this.cmbDept.Text = "";
                this.cmbDept.Tag = null;
                this.cmbDept.AddItems(Function.GetNurseDept(this.cmbNurseStation.SelectedItem.ID, ref error));
                this.cmbDept.Tag = null;
            }
        }

        void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbDept.SelectedItem != null && this.cmbNurseStation.SelectedItem != null)
            {
                string error = "";
                this.cmbRoom.ClearItems();
                this.cmbRoom.Text = "";
                this.cmbRoom.Tag = null;
                this.cmbRoom.AddItems(roomBiz.GetRooms(this.cmbNurseStation.SelectedItem.ID, this.cmbDept.SelectedItem.ID, ref error));
            }
        }

        void cmbRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbRoom.SelectedItem != null)
            {
                FS.SOC.HISFC.Assign.BizLogic.Console consoleMgr = new FS.SOC.HISFC.Assign.BizLogic.Console();
                ArrayList al = consoleMgr.Query(this.cmbRoom.SelectedItem.ID);
                this.cmbConsole.ClearItems();
                this.cmbConsole.Text = "";
                this.cmbConsole.Tag = null;
                this.cmbConsole.AddItems(al);
            }
        }

        void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbQueueType.SelectedItem == null || string.IsNullOrEmpty(this.cmbQueueType.SelectedItem.ID))
            {
                this.setQueueName(EnumQueueType.Custom);
            }
            else
            {
                this.setQueueName(EnumHelper.Current.GetEnum<EnumQueueType>(this.cmbQueueType.SelectedItem.ID));
            }

            if (sender == this.cmbRegLevel)
            {
                this.ckExpert.Checked = this.cmbRegLevel.Tag != null && FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetRegLevel(this.cmbRegLevel.Tag.ToString()) != null && FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetRegLevel(this.cmbRegLevel.Tag.ToString()).IsExpert;
            }

        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                System.Windows.Forms.SendKeys.Send("{TAB}");
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        #endregion

        #region IMaintenance<KeyValuePair<Queue,Day>> 成员

        public int Init(System.Collections.ArrayList al)
        {
            if (al == null)
            {
                return -1;
            }

            for (int i = 0; i < al.Count; i++)
            {
                if (al[i] is FS.FrameWork.Models.NeuObject)
                {
                    this.cmbNurseStation.AddItems(al);
                    break;
                }
            }


            return 1;
        }

        public int Add(KeyValuePair<FS.SOC.HISFC.Assign.Models.Queue, Day> t)
        {
            this.Clear();

            weekday = t.Value;

            if (t.Key != null)
            {
                this.cmbNurseStation.Tag = t.Key.AssignNurse.ID;
                this.cmbDept.Tag = t.Key.AssignDept.ID;
                this.dtQueue.Value = t.Key.QueueDate;
            }
            return 1;
        }

        public int Modify(KeyValuePair<FS.SOC.HISFC.Assign.Models.Queue, Day> t)
        {
            this.Clear();
            FS.SOC.HISFC.Assign.Models.Queue queue = new FS.SOC.HISFC.Assign.Models.Queue();

            if (t.Value == Day.Default)
            {
                queue = queueMgr.GetQueue(t.Key.ID); //重新获取队列信息
            }
            else
            {
                queue = queueTemplateMgr.GetQueueTemplate(t.Key.ID);
            }
            weekday = t.Value;
            if (t.Key != null)
            {
                ////午别
                //this.cmbNoon.Tag = t.Key.Noon.ID;
                ////看诊科室
                //this.cmbNurseStation.Tag = t.Key.AssignNurse.ID;
                //this.cmbDept.Tag = t.Key.AssignDept.ID;
                ////看诊医生
                //this.cmbDoct.Tag = t.Key.Doctor.ID;
                ////队列日期
                //this.dtQueue.Value = t.Key.QueueDate;
                ////队列类型
                //this.cmbQueueType.Tag = t.Key.QueueType.ToString();
                //this.cmbRoom.Tag = t.Key.SRoom.ID;
                //this.cmbConsole.Tag = t.Key.Console.ID;
                ////挂号级别
                //this.cmbRegLevel.Tag = t.Key.RegLevel.ID;

                ////对列名称
                //this.tbQueueName.Text = t.Key.Name;
                //this.ckValid.Checked = t.Key.IsValid;
                //this.ckExpert.Checked = t.Key.IsExpert;
                ////显示顺序
                //this.tbSort.Text = t.Key.Order.ToString();
                ////备注
                //this.tbMemo.Text = t.Key.Memo;


                //午别
                this.cmbNoon.Tag = queue.Noon.ID;
                //看诊科室
                this.cmbNurseStation.Tag = queue.AssignNurse.ID;
                this.cmbDept.Tag = queue.AssignDept.ID;
                //看诊医生
                this.cmbDoct.Tag = queue.Doctor.ID;

                if (queue.QueueDate == DateTime.MinValue)
                {
                    this.dtQueue.Value = new DateTime(1900, 1, 1);
                }
                else
                {
                    //队列日期
                    this.dtQueue.Value = queue.QueueDate;
                }
                //队列类型
                this.cmbQueueType.Tag = queue.QueueType.ToString();
                this.cmbRoom.Tag = queue.SRoom.ID;
                this.cmbConsole.Tag = queue.Console.ID;
                //挂号级别
                this.cmbRegLevel.Tag = queue.RegLevel.ID;

                //对列名称
                this.tbQueueName.Text = queue.Name;
                this.ckValid.Checked = queue.IsValid;
                this.ckExpert.Checked = queue.IsExpert;
                //显示顺序
                this.tbSort.Text = queue.Order.ToString();
                //备注
                this.tbMemo.Text = queue.Memo;

                this.cmbPatientConditionType.Tag = queue.PatientConditionType;

                this.Tag = t.Key;
            }
            return 1;
        }

        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<KeyValuePair<FS.SOC.HISFC.Assign.Models.Queue, Day>> SaveInfo
        {
            get
            {
                return saveInfo;
            }
            set
            {
                saveInfo = value;
            }
        }

        #endregion

        #region IInitialisable 成员

        public int Init()
        {
            this.initDatas();
            this.initEvents();

            return 1;
        }

        #endregion

        #region IClearable 成员

        public int Clear()
        {
            this.cmbDept.Tag = null;
            //午别
            this.cmbNoon.Tag = null;
            //看诊科室
            this.cmbNurseStation.Tag = null;
            //看诊医生
            this.cmbDoct.Tag = null;
            //队列日期
            this.dtQueue.Value = DateTime.Now;
            //队列类型
            this.cmbQueueType.Tag = null;
            this.cmbRoom.Tag = null;
            this.cmbConsole.Tag = null;
            this.cmbRegLevel.Tag = null;
            //对列名称
            this.tbQueueName.Text = "";
            this.ckValid.Checked = true;
            this.ckExpert.Checked = false;
            //显示顺序
            this.tbSort.Text = "0";
            //备注
            this.tbMemo.Text = "";
            weekday = Day.Monday;
            if (cmbPatientConditionType.Items.Count > 0)
            {
                this.cmbPatientConditionType.SelectedIndex = 0;
            }

            this.Tag = null;
            return 1;
        }

        #endregion

        #region ILoadable 成员

        public new int Load()
        {
            return 1;
        }

        #endregion

        private void cmbRoom_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (this.cmbQueueType.SelectedItem == null || string.IsNullOrEmpty(this.cmbQueueType.SelectedItem.ID))
            {
                this.setQueueName(EnumQueueType.Custom);
            }
            else
            {
                this.setQueueName(EnumHelper.Current.GetEnum<EnumQueueType>(this.cmbQueueType.SelectedItem.ID));
            }
            FS.HISFC.Models.Nurse.Room obj = this.cmbRoom.SelectedItem as FS.HISFC.Models.Nurse.Room;
            this.tbSort.Text = obj.Sort.ToString();
        }

        private void cmbDept_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //this.cmbDoct.ClearItems();//多科室没维护，暂时屏蔽
            //this.cmbDoct.AddItems((new FS.HISFC.BizLogic.Manager.Person()).GetEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D, this.cmbDept.SelectedItem.ID));
        }
    }
}
