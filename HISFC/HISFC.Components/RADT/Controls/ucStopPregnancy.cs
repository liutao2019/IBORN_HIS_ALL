using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// [功能描述: 终止妊娠登记管理]<br></br>
    /// [创 建 者: zhaoj]<br></br>
    /// [创建时间: 2012-2-10]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucStopPregnancy : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucStopPregnancy()
        {
            InitializeComponent();
        }

        #region 成员变量

        private FS.HISFC.Models.RADT.PatientInfo patientInfo = null;
        private bool isAdd = true;

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public void Init()
        {
            this.btSave.Click -= new EventHandler(btSave_Click);
            this.btSave.Click += new EventHandler(btSave_Click);

            this.btCancel.Click -= new EventHandler(btCancel_Click);
            this.btCancel.Click += new EventHandler(btCancel_Click);

            this.ucQueryInpatientNo.myEvent -= new FS.HISFC.Components.Common.Controls.myEventDelegate(ucQueryInpatientNo_myEvent);
            this.ucQueryInpatientNo.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(ucQueryInpatientNo_myEvent);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            if (this.patientInfo == null)
            {
                MessageBox.Show("请选择或输入住院号查找患者", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
            }

            FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
            FS.HISFC.Models.RADT.PatientInfo patient = inpatientManager.QueryPatientInfoByInpatientNO(this.patientInfo.ID);
            if (patient == null)
            {
                MessageBox.Show(inpatientManager.Err);
                return -1;
            }
            //如果患者已不是在院状态,则不允许操作
            if (patient.PVisit.InState.ID.ToString() != this.patientInfo.PVisit.InState.ID.ToString())
            {
                MessageBox.Show("患者信息已发生变化,请刷新当前窗口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
            }

            //控制只有母亲才能做婴儿登记
            if (this.patientInfo.Sex.ID.ToString() != "F" || this.patientInfo.PID.PatientNO.StartsWith("B"))
            {
                MessageBox.Show("只能是妇女才能做终止妊娠登记！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
            }

            int stopPregnancyWeeks=0;
            if (FS.FrameWork.Public.String.IsNumeric(this.txtStopPregnancyWeeks.Text) == false || Int32.TryParse(this.txtStopPregnancyWeeks.Text, out stopPregnancyWeeks) == false || stopPregnancyWeeks <= 0)
            {
                MessageBox.Show("终止妊娠周数必须填写正整数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
            }

            FS.HISFC.BizLogic.RADT.StopPregnancy stopPregnancyMgr = new FS.HISFC.BizLogic.RADT.StopPregnancy();

            //保存终止妊娠登记记录
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            stopPregnancyMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //从界面获取妊娠登记信息
            FS.HISFC.Models.RADT.StopPregnancy stopPregnancy = new FS.HISFC.Models.RADT.StopPregnancy();
            stopPregnancy.ID = this.patientInfo.ID;
            stopPregnancy.Weeks = stopPregnancyWeeks;
            stopPregnancy.Area.ID = this.rBtnInProvince.Checked ? "1" : this.rBtnOutProvince.Checked ? "2" : "0";
            stopPregnancy.RegDate = stopPregnancyMgr.GetDateTimeFromSysDateTime();
            stopPregnancy.Oper.ID = stopPregnancyMgr.Operator.ID;

            if (this.isAdd)
            {
                if (stopPregnancyMgr.Insert(stopPregnancy) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("保存失败，原因：" + stopPregnancyMgr.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
            else
            {
                if (stopPregnancyMgr.Update(stopPregnancy) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("保存失败，原因：" + stopPregnancyMgr.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("登记成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return 1;
        }

        /// <summary>
        /// 取消登记
        /// </summary>
        /// <returns></returns>
        public int Cancel()
        {
            if (this.patientInfo == null)
            {
                MessageBox.Show("请选择或输入住院号查找患者", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
            }

            if (!this.isAdd)
            {
                //删除登记信息
                FS.HISFC.BizLogic.RADT.StopPregnancy stopPregnancyMgr = new FS.HISFC.BizLogic.RADT.StopPregnancy();
                if (stopPregnancyMgr.Delete(this.patientInfo.ID) < 0)
                {
                    MessageBox.Show("取消失败，原因：" + stopPregnancyMgr.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
                MessageBox.Show("取消登记成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return 1;
        }

        /// <summary>
        /// 清屏
        /// </summary>
        public void Clear()
        {
            this.isAdd = true;

            this.txtName.Text = string.Empty;
            this.txtSex.Text = string.Empty;
            this.txtStopPregnancyWeeks.Text = string.Empty;
            this.rBtnLocal.Checked = true;

            this.SetPatientInfo(this.patientInfo);

        }

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="patient"></param>
        public void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            if (patient!=null&&patient.ID != null && patient.ID != "")
            {
                this.ucQueryInpatientNo.Text = patient.ID;
                this.ucQueryInpatientNo.InputType = 0;

                this.txtName.Text = patient.Name;
                this.txtSex.Text = patient.Sex.Name;
                this.txtStopPregnancyWeeks.Text = "0";
                this.rBtnLocal.Checked = true;

                //查找终止妊娠登记记录
                FS.HISFC.BizLogic.RADT.StopPregnancy stopPregnancyMgr = new FS.HISFC.BizLogic.RADT.StopPregnancy();
                FS.HISFC.Models.RADT.StopPregnancy stopPregnancy = stopPregnancyMgr.Get(patient.ID);
                if (stopPregnancy != null)
                {
                    this.txtStopPregnancyWeeks.Text = stopPregnancy.Weeks.ToString();
                    switch (stopPregnancy.Area.ID)
                    {
                        case "1":
                            this.rBtnInProvince.Checked = true;
                            break;
                        case "2":
                            this.rBtnOutProvince.Checked = true;
                            break;
                        case "0":
                        default:
                            this.rBtnLocal.Checked = true;
                            break;
                    }
                    this.isAdd = false;
                    this.dtOperatedate.Value = stopPregnancy.RegDate;
                }
                this.txtStopPregnancyWeeks.Focus();
                this.txtStopPregnancyWeeks.SelectAll();

            }
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.patientInfo = neuObject as FS.HISFC.Models.RADT.PatientInfo;

            this.SetPatientInfo(this.patientInfo);

            return base.OnSetValue(neuObject, e);
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

        #endregion

        #region 事件

        void ucQueryInpatientNo_myEvent()
        {
            if (this.ucQueryInpatientNo.InpatientNo != null && this.ucQueryInpatientNo.InpatientNo.Length > 0)
            {
                FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
                this.patientInfo = inpatientManager.QueryPatientInfoByInpatientNO(this.ucQueryInpatientNo.InpatientNo);
                if (this.patientInfo == null)
                {
                    MessageBox.Show(inpatientManager.Err);
                    return;
                }

                this.SetPatientInfo(this.patientInfo);
            }
        }

        void btCancel_Click(object sender, EventArgs e)
        {
            if (this.Cancel() > 0)
            {
                this.Clear();
            }
        }

        void btSave_Click(object sender, EventArgs e)
        {
            if (this.Save() > 0)
            {
                this.Clear();
            }
        }

        #endregion
    }
}
