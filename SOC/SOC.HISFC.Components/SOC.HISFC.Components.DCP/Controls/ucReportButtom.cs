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
    /// [��������: �����ײ�uc]<br></br>
    /// [�� �� ��: zj]<br></br>
    /// [����ʱ��: 2008-09-17]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucReportButtom : ucBaseMainReport
    {
        public ucReportButtom()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// �ۺ�ҵ�������
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

        #region ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="sysdate"></param>
        public override int Init(DateTime sysdate)
        {
            base.Init(sysdate);//�ȳ�ʼ������ķ�����
            ArrayList alPerson = this.commonProcess.QueryEmployeeAllValidAndUnvalid();
            if(alPerson==null||alPerson.Count==0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ��Ա��Ϣʧ�ܣ�"));
                return -1;
            }
            this.cmbReportDoctor.AddItems(alPerson);

            ArrayList alDept = this.commonProcess.QueryDeptAllValid();
            if (alDept == null || alDept.Count == 0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ������Ϣʧ�ܣ�"));
                return -1;
            }
            this.cmbDoctorDept.AddItems(alDept);

            this.dtpReportTime.Value = this.sysdate;
            this.cmbDoctorDept.Tag = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
            this.cmbReportDoctor.Tag = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).ID;

            return 1;
        }

        /// <summary>
        /// ȡֵ
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public override int GetValue(ref FS.HISFC.DCP.Object.CommonReport report)
        {
            return this.GetValue(ref report, OperType.����);
        }

        public override int GetValue(ref FS.HISFC.DCP.Object.CommonReport report, OperType opertyp)
        {
            try
            {
                if (this.cmbDoctorDept.Tag == null || this.cmbDoctorDept.Tag.ToString() == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѡ�񱨿����ң�"));
                    this.cmbDoctorDept.Select();
                    this.cmbDoctorDept.Focus();
                    return -1;
                }
                report.DoctorDept = this.cmbDoctorDept.SelectedItem;
                if (this.cmbReportDoctor.Tag == null || this.cmbReportDoctor.Tag.ToString() == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѡ�񱨿��ˣ�"));
                    this.cmbReportDoctor.Select();
                    this.cmbReportDoctor.Focus();
                    return -1;
                }
                report.ReportDoctor = this.cmbReportDoctor.SelectedItem;
                if (opertyp == OperType.���ϸ�)
                {
                    if (string.IsNullOrEmpty(this.txtCase.Text))
                    {
                        this.MyMessageBox("����д���ϸ�ԭ��������Ϊ���ϸ�", "��ʾ>>");
                        this.txtCase.Select();
                        this.txtCase.Focus();
                        return -1;
                    }
                    report.OperCase = this.txtCase.Text;
                }
                report.ReportTime = this.dtpReportTime.Value;
                report.Memo = this.rtxtMemo.Text.Trim();//��ע
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg(e.Message));
                return -1;
            }
        }

        /// <summary>
        /// ��ֵ
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
        /// <param name="message">��ʾ��Ϣ</param>
        /// <param name="type">err���� ����������</param>
        private void MyMessageBox(string message, string type)
        {
            switch (type)
            {
                case "err":
                    MessageBox.Show(message, "��ʾ", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    break;
                default:
                    MessageBox.Show(message, type, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    break;
            }
        }

        #endregion
    }
}
