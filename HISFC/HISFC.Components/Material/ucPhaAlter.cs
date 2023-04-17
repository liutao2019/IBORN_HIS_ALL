using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Material
{
    /// <summary>
    /// [��������: ҩƷ������]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// </summary>
    public partial class ucPhaAlter : UserControl
    {
        public ucPhaAlter()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// �ⷿ����
        /// </summary>
        private string deptCode;

        /// <summary>
        /// ���������ļ��� �������Ϣ
        /// </summary>
        private ArrayList alInfo;

        #endregion

        #region ����

        /// <summary>
        /// �ⷿ����
        /// </summary>
        public string DeptCode
        {
            set
            {
                this.deptCode = value;
            }
        }

        /// <summary>
        /// ���������ļ��� �������Ϣ
        /// </summary>
        public ArrayList ApplyInfo
        {
            get
            {
                if (this.alInfo == null)
                    this.alInfo = new ArrayList();
                return this.alInfo;
            }
        }

        /// <summary>
        /// ͳ����ʼʱ��
        /// </summary>
        private DateTime DtBegin
        {
            get
            {
                return NConvert.ToDateTime(this.dtpBegin.Text);
            }
        }

        /// <summary>
        /// ͳ�ƽ�ֹʱ��
        /// </summary>
        private DateTime DtEnd
        {
            get
            {
                return NConvert.ToDateTime(this.dtpEnd.Text);
            }
        }

        /// <summary>
        /// ��߿������
        /// </summary>
        private int MaxAlterDays
        {
            get
            {
                return NConvert.ToInt32(this.txtMaxDays.Text);
            }
        }

        /// <summary>
        /// ��Ϳ������
        /// </summary>
        private int MinAlterDays
        {
            get
            {
                return NConvert.ToInt32(this.txtMinDays.Text);
            }
        }

        #endregion


        /// <summary>
        /// ��������ֵ�Ŀⷿ���� ������Ϣ��ʾ
        /// </summary>
        public void SetData()
        {
            FS.HISFC.BizLogic.Material.Baseset baseManager = new FS.HISFC.BizLogic.Material.Baseset();
            FS.HISFC.Models.Material.MaterialStorage deptCons = baseManager.QueryStorageInfo(this.deptCode);
            if (deptCons == null)
            {
                MessageBox.Show("��ȡ���ҳ�����������! \n" + baseManager.Err);
                return;
            }
            this.dtpEnd.Value = baseManager.GetDateTimeFromSysDateTime().Date;
            this.dtpBegin.Value = this.dtpEnd.Value.AddDays(-deptCons.ReferenceDays);
            this.txtMaxDays.Text = deptCons.MaxDays.ToString();
            this.txtMinDays.Text = deptCons.MinDays.ToString();
            this.lbIntervalDays.Text = deptCons.ReferenceDays.ToString() + "��";
        }

        /// <summary>
        /// �ж��Ƿ�������
        /// </summary>
        /// <returns>�ɹ�����True  ���򷵻�False</returns>
        protected bool SaveValid()
        {
            if (this.MaxAlterDays == 0 || this.MinAlterDays == 0)
            {
                MessageBox.Show("������������߲���Ϊ��");
                return false;
            }
            if (this.MaxAlterDays < this.MinAlterDays)
            {
                MessageBox.Show("��߿����������С����Ϳ������");
                return false;
            }
            return true;
        }

        /// <summary>
        /// ȷ�ϲ������� �����Ĵ���
        /// </summary>
        public int Save()
        {
            if (this.SaveValid())
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڰ��������Ľ��м��� ���Ժ�...");
                Application.DoEvents();
                FS.HISFC.BizLogic.Material.Store storeManager = new FS.HISFC.BizLogic.Material.Store();
                this.alInfo = storeManager.FindByAlter("1", this.deptCode, this.DtBegin, this.DtEnd, this.MaxAlterDays, this.MinAlterDays);
                if (this.alInfo == null)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("�������Ĳ�����Ŀʧ�ܣ�");
                    return -1;
                }
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            else
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ���ڹر�
        /// </summary>
        public void Close()
        {
            if (this.ParentForm != null)
                this.ParentForm.Close();
        }


        /// <summary>
        /// ���ý���
        /// </summary>
        public new void Focus()
        {
            this.dtpBegin.Focus();
        }


        private void dtp_ValueChanged(object sender, EventArgs e)
        {
            if (this.DtEnd < this.DtBegin)
            {
                this.dtpEnd.Text = this.DtBegin.ToString();
            }
            this.lbIntervalDays.Text = (this.DtEnd - this.DtBegin).Days + "��";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.Save() == 1)
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
