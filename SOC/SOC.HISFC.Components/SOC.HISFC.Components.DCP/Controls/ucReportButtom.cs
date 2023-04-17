using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.DCP.Controls
{
    /// <summary>
    /// ucReportButtom<br></br>
    /// [功能描述: 报卡底部uc]<br></br>
    /// [创 建 者: zj]<br></br>
    /// [创建时间: 2008-09-17]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucReportButtom : ucBaseMainReport
    {
        public ucReportButtom()
        {
            InitializeComponent();
        }

        #region 域变量

        /// <summary>
        /// 综合业务管理类
        /// </summary>
        private FS.SOC.HISFC.BizProcess.DCP.Common commonProcess = new FS.SOC.HISFC.BizProcess.DCP.Common();

        #endregion

        public bool SetReportTime
        {
            set
            {
                this.dtpReportTime.Enabled = value;
            }
        }

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sysdate"></param>
        public override int Init(DateTime sysdate)
        {
            base.Init(sysdate);//先初始化基类的方法。
            ArrayList alPerson = this.commonProcess.QueryEmployeeAllValidAndUnvalid();
            if(alPerson==null||alPerson.Count==0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("获取人员信息失败！"));
                return -1;
            }
            this.cmbReportDoctor.AddItems(alPerson);

            ArrayList alDept = this.commonProcess.QueryDeptAllValid();
            if (alDept == null || alDept.Count == 0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("获取科室信息失败！"));
                return -1;
            }
            this.cmbDoctorDept.AddItems(alDept);

            this.dtpReportTime.Value = this.sysdate;
            this.cmbDoctorDept.Tag = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
            this.cmbReportDoctor.Tag = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).ID;

            return 1;
        }

        /// <summary>
        /// 取值
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public override int GetValue(ref FS.HISFC.DCP.Object.CommonReport report)
        {
            return this.GetValue(ref report, OperType.新增);
        }

        public override int GetValue(ref FS.HISFC.DCP.Object.CommonReport report, OperType opertyp)
        {
            try
            {
                if (this.cmbDoctorDept.Tag == null || this.cmbDoctorDept.Tag.ToString() == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择报卡科室！"));
                    this.cmbDoctorDept.Select();
                    this.cmbDoctorDept.Focus();
                    return -1;
                }
                report.DoctorDept = this.cmbDoctorDept.SelectedItem;
                if (this.cmbReportDoctor.Tag == null || this.cmbReportDoctor.Tag.ToString() == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择报卡人！"));
                    this.cmbReportDoctor.Select();
                    this.cmbReportDoctor.Focus();
                    return -1;
                }
                report.ReportDoctor = this.cmbReportDoctor.SelectedItem;
                if (opertyp == OperType.不合格)
                {
                    if (string.IsNullOrEmpty(this.txtCase.Text))
                    {
                        this.MyMessageBox("请填写不合格原因，再设置为不合格", "提示>>");
                        this.txtCase.Select();
                        this.txtCase.Focus();
                        return -1;
                    }
                    report.OperCase = this.txtCase.Text;
                }
                report.ReportTime = this.dtpReportTime.Value;
                report.Memo = this.rtxtMemo.Text.Trim();//备注
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg(e.Message));
                return -1;
            }
        }

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public override int SetValue(FS.HISFC.DCP.Object.CommonReport report)
        {
            try
            {
                this.cmbDoctorDept.Tag = report.DoctorDept.ID;
                this.cmbReportDoctor.Tag = report.ReportDoctor.ID;
                this.txtCase.Text = report.OperCase;
                this.rtxtMemo.Text = report.Memo;
                this.dtpReportTime.Value = report.ReportTime;
                    
                return 1;
            }
            catch(Exception e)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg(e.Message));
                return -1;
            }
        }

        public override int SetControlEnable(bool enable)
        {
            this.cmbDoctorDept.Enabled = enable;
            this.txtCase.Enabled = enable;
            return base.SetControlEnable(enable);
        }

        public override void Clear()
        {
            this.cmbDoctorDept.Tag = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
            this.cmbReportDoctor.Tag = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).ID;
            this.txtCase.Text = "";
            this.rtxtMemo.Text = "";
            this.dtpReportTime.Value = this.sysdate;
            base.Clear();
        }

        public override void PrePrint()
        {
            this.gbReportButtom.BackColor = Color.White;
            this.BackColor = Color.White;
            this.rtxtMemo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            base.PrePrint();
        }

        public override void Printed()
        {
            this.gbReportButtom.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.BackColor = System.Drawing.Color.FromArgb(158, 177, 201);
            this.rtxtMemo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            base.Printed();
        }

        /// <summary>
        /// MessageBox
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="type">err错误 其它作标题</param>
        private void MyMessageBox(string message, string type)
        {
            switch (type)
            {
                case "err":
                    MessageBox.Show(message, "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    break;
                default:
                    MessageBox.Show(message, type, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    break;
            }
        }

        #endregion
    }
}
