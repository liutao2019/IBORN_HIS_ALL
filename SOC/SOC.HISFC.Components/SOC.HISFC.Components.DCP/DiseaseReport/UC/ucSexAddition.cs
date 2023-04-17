using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.UFC.DCP.DiseaseReport.UC
{
    /// <summary>
    /// [���������� �Բ�����uc]
    /// [�� �� �ߣ� ZJ]
    /// [����ʱ�䣺 2008-08-25]
    /// </summary>
    public partial class ucSexAddition : ucBaseAddition
    {
        public ucSexAddition()
        {
            InitializeComponent();
            this.Init();
        }

        #region ��ʼ��

        /// <summary>
        /// ��ʼ����Ϣ
        /// </summary>
        public void Init()
        {
            //������
            InitCmb();
            InitCmbStyle();
        }

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        public void InitCmb()
        {
            Neusoft.HISFC.DCP.BizProcess.Common commonProcess = new Neusoft.HISFC.DCP.BizProcess.Common();
            //����״��
            ArrayList alMarry = commonProcess.QueryConstantList("MARRY_STATE");
            this.cmbMarry.AddItems(alMarry);

            //����
            ArrayList alNation = commonProcess.QueryConstantList(Neusoft.HISFC.Models.Base.EnumConstant.NATION);
            this.cmbNation.AddItems(alNation);

            //�Ļ��̶�
            ArrayList alEducation = commonProcess.QueryConstantList(Neusoft.HISFC.Models.Base.EnumConstant.EDUCATION);
            this.cmbEducation.AddItems(alEducation);

            //�Ӵ�ʷ
            ArrayList alTatch = commonProcess.QueryConstantList("Tatch");
            if (alTatch == null)
            {
                alTatch = new ArrayList();
            }
            this.cmbTatch.AddItems(alTatch);

            //��Ⱦ;��
            ArrayList alChannel = commonProcess.QueryConstantList("InfectChannel");
            if (alChannel == null)
            {
                alChannel = new ArrayList();
            }
            this.cmbChannel.AddItems(alChannel);

            //������Դ
            ArrayList alSampleSource = commonProcess.QueryConstantList("SampleSource");
            if (alSampleSource == null)
            {
                alSampleSource = new ArrayList();
            }
            this.cmbSampleSource.AddItems(alSampleSource);
        }

        /// <summary>
        /// ��ʼ��CMB�ĸ�ʽ
        /// </summary>
        public void InitCmbStyle()
        {
            foreach (Control c in this.gbSexAddition.Controls)
            {
                if (c.GetType() == typeof(Neusoft.FrameWork.WinForms.Controls.NeuComboBox))
                {
                    ((Neusoft.FrameWork.WinForms.Controls.NeuComboBox)c).DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }
        }
        
        #endregion

        #region ����

        /// <summary>
        /// ��֤������Ϣ�Ƿ�����
        /// </summary>
        /// <param name="msg">��ʾ��Ϣ</param>
        /// <returns>-1 ��������1 ����</returns>
        public new int IsValid(ref string msg)
        {
            int ret=1;
            if (this.cmbMarry.SelectedValue == null &&this.cmbMarry.Tag.ToString() =="")
            {
                msg += "����||";
                ret =-1;
            }
            if (this.cmbNation.SelectedValue == null && this.cmbNation.Tag.ToString() == "")
            {
                msg += "����||";
                ret = -1;
            }
            if (this.cmbEducation.SelectedValue == null && this.cmbEducation.Tag.ToString() == "")
            {
                msg += "�Ļ��̶�||";
                ret = -1;
            }
            if (this.cmbTatch.SelectedValue == null && this.cmbTatch.Tag.ToString() == "")
            {
                msg += "�Ӵ�ʷ||";
                ret = -1;
            }
            if (this.cmbChannel.SelectedValue == null && this.cmbChannel.Tag.ToString() == "")
            {
                msg += "��Ⱦ;��||";
                ret = -1;
            }
            if (this.cmbSampleSource.SelectedValue == null && this.cmbSampleSource.Tag.ToString() == "")
            {
                msg += "������Դ||";
                ret = -1;
            }
            if (this.txtTestUnit.Text == "" || this.txtTestUnit.Text == null)
            {
                msg += "ȷ�ϣ����ԣ���ⵥλ||";
                ret = -1;
            }
            return ret;
        }

        #endregion
    }
}
