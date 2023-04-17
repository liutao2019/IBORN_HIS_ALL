using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Preparation
{
    /// <summary>
    /// <br></br>
    /// [��������: ��������ѡ�����]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2008-03]<br></br>
    /// <˵��>
    /// </˵��>
    /// </summary>
    public partial class ucChooseData : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucChooseData()
        {
            InitializeComponent();
        }

        #region ��̬��������

        /// <summary>
        /// Ŀ����Ҽ���
        /// </summary>
        private static System.Collections.ArrayList alTargetDeptList = null;

        /// <summary>
        /// �����ҡ�������Ϣѡ��
        /// </summary>
        /// <param name="stockDept">������</param>
        /// <param name="inTargetDept">���Ŀ�����</param>
        /// <param name="isNeedApply">�Ƿ���Ҫ��������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        internal static int ChooseInputTargetData(FS.FrameWork.Models.NeuObject stockDept, ref FS.FrameWork.Models.NeuObject inTargetDept, out bool isNeedApply)
        {
            isNeedApply = false;

            if (alTargetDeptList == null)
            {
                FS.HISFC.BizProcess.Integrate.Manager privInOutManager = new FS.HISFC.BizProcess.Integrate.Manager();
                alTargetDeptList = privInOutManager.GetPrivInOutDeptList(stockDept.ID, "0320");
                if (alTargetDeptList == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg(privInOutManager.Err));
                    return -1;
                }
            }

            ArrayList alTarget = new ArrayList();
            foreach (FS.HISFC.Models.Base.PrivInOutDept privInOutDept in alTargetDeptList)
            {
                FS.FrameWork.Models.NeuObject offerInfo = new FS.FrameWork.Models.NeuObject();
                offerInfo.ID = privInOutDept.Dept.ID;			    //������λ����
                offerInfo.Name = privInOutDept.Dept.Name;		    //������λ����
                offerInfo.Memo = privInOutDept.Memo;		    //��ע

                alTarget.Add(offerInfo.Clone());
            }

            alTarget.Add(inTargetDept.Clone());

            using (ucChooseData uc = new ucChooseData())
            {
                uc.ComboxLabel = "���Ŀ�����";
                uc.ComboxData = alTarget;
                uc.ComboxSelectTag = inTargetDept.ID;

                uc.CheckLabel = "������������������ֱ�����";
                uc.CheckChooseChecked = isNeedApply;

                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "�����Ϣ����";
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);

                if (uc.Result == DialogResult.OK)
                {
                    inTargetDept.ID = uc.ComboxSelectTag.ToString();
                    inTargetDept.Name = uc.ComboxSelectText;

                    isNeedApply = uc.CheckChooseChecked;
                }
                else
                {
                    return -1;
                }
            }

            return 1;
        }

        #endregion

        /// <summary>
        /// ���ѡ��
        /// </summary>
        private DialogResult rs = DialogResult.Cancel;

        #region ����

        /// <summary>
        /// Combox��ӦLabel
        /// </summary>
        public string ComboxLabel
        {
            set
            {
                this.lbTarget.Text = value;
            }
        }

        /// <summary>
        /// CheckBox �� Label
        /// </summary>
        public string CheckLabel
        {
            set
            {
                this.ckChoose.Text = value;
            }
        }

        /// <summary>
        /// Combox ����Դ
        /// </summary>
        public System.Collections.ArrayList ComboxData
        {
            set
            {
                if (value != null)
                {
                    this.cmbData.AddItems(value);
                }
            }
        }

        /// <summary>
        /// Combox ��ǰѡ���� Tag
        /// </summary>
        public object ComboxSelectTag
        {
            get
            {
                return this.cmbData.Tag;
            }
            set
            {
                this.cmbData.Tag = value;
            }
        }

        /// <summary>
        /// Combox ��ǰѡ���� Text
        /// </summary>
        public string ComboxSelectText
        {
            get
            {
                return this.cmbData.Text;
            }
            set
            {
                this.cmbData.Text = value;
            }
        }

        /// <summary>
        /// CheckBox ��ǰ�Ƿ�ѡ��
        /// </summary>
        public bool CheckChooseChecked
        {
            get
            {
                return this.ckChoose.Checked;
            }
            set
            {
                this.ckChoose.Checked = value;
            }
        }

        /// <summary>
        /// ���ֵ
        /// </summary>
        public DialogResult Result
        {
            get
            {
                return this.rs;
            }
        }

        #endregion

        /// <summary>
        /// �ر�
        /// </summary>
        protected void Close()
        {
            if (this.ParentForm != null)
            {
                this.ParentForm.Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.ComboxSelectTag == null || this.ComboxSelectTag.ToString() == "")
            {
                MessageBox.Show("��ѡ�����Ŀ�����");
            }

            this.rs = DialogResult.OK;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.rs = DialogResult.Cancel;

            this.Close();
        }
    }
}
