using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.HealthRecord.CaseLend
{
    /// <summary>
    /// frmCaseCard<br></br>
    /// [��������: �������Ŀ���Ϣ¼��]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-04-20]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class frmCaseCard : Form
    {
        public frmCaseCard()
        {
            InitializeComponent();
        }
        #region ȫ�ֱ���
        FS.HISFC.BizLogic.HealthRecord.CaseCard card = new FS.HISFC.BizLogic.HealthRecord.CaseCard();
        private FS.HISFC.Models.HealthRecord.EnumServer.EditTypes editType;
        #endregion
        /// <summary>
        /// ���ñ�־
        /// </summary>
        public FS.HISFC.Models.HealthRecord.EnumServer.EditTypes EditType
        {
            set
            {
                editType = value;
                if (editType == FS.HISFC.Models.HealthRecord.EnumServer.EditTypes.Add)
                {
                    this.ckValue.Checked = true;
                    this.ckContinue.Enabled = true;
                    this.txCardNo.ReadOnly = false;
                }
                else if (editType == FS.HISFC.Models.HealthRecord.EnumServer.EditTypes.Modify)
                {
                    this.txCardNo.ReadOnly = true;
                    this.ckContinue.Enabled = false;
                }
            }
            get
            {
                return editType;
            }
        }
        /// <summary>
        /// ���� 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, System.EventArgs e)
        {

            if (CheckValue() == -1)
            {
                return;
            }
            FS.HISFC.Models.HealthRecord.ReadCard obj = this.Getinfo();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(card.Connection);
            //trans.BeginTransaction();

            card.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            int iReturn = 0;
            #region ��������
            //��������ӵ� �����
            if (editType == FS.HISFC.Models.HealthRecord.EnumServer.EditTypes.Add)
            {
                //����I��Ϣ
                iReturn = card.Insert(obj);

                if (iReturn < 0)
                {
                    //��������
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ʧ�� " + card.Err);
                }
            }
            else if (editType == FS.HISFC.Models.HealthRecord.EnumServer.EditTypes.Modify) //������ִ�и��²�����
            {
                iReturn = card.Update(obj);
                if (iReturn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("������Ϣʧ��! " + card.Err);
                }
                if (iReturn == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("û���ҵ��ɸ��µ���Ϣ");
                }
            }
            #endregion
            //�ύ����
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("����ɹ�");
            if (this.ckContinue.Checked && this.ckContinue.Enabled)
            {
                this.ClearInfo();
            }
            else
            {
                this.Visible = false;
            }
            SaveHandle(obj);
        }
        private int CheckValue()

        {
            if (this.comperson.Tag == null || comperson.Tag.ToString() == "")
            {
                this.comperson.Focus();
                MessageBox.Show("��ѡ����Ա");
                return -1;
            }
            if (this.comDept.Tag == null || comDept.Tag.ToString() == "")
            {
                this.comDept.Focus();
                MessageBox.Show("��ѡ�����");
                return -1;
            }
            if (this.txCardNo.Text == "")
            {
                this.txCardNo.Focus();
                MessageBox.Show("��ѡ���������֤��");
                return -1;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(txCardNo.Text, 14))
            {
                this.txCardNo.Focus();
                MessageBox.Show("�����������");
                return -1;
            }
            if (this.editType == FS.HISFC.Models.HealthRecord.EnumServer.EditTypes.Add)
            {
                FS.HISFC.Models.HealthRecord.ReadCard obj1 = card.GetCardInfo(txCardNo.Text);
                if (obj1 == null)
                {
                    this.txCardNo.Focus();
                    MessageBox.Show("��ѯ����");
                    return -1;
                }
                if (obj1.CardID != "")
                {
                    this.txCardNo.Focus();
                    MessageBox.Show("����֤���Ѿ�����");
                    return -1;
                }
            }
            return 1;
        }
        /// <summary>
        /// ��ȡ�����ϵ���Ϣ
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.HealthRecord.ReadCard Getinfo()
        {
            FS.HISFC.Models.HealthRecord.ReadCard info = new FS.HISFC.Models.HealthRecord.ReadCard();
            info.CardID = this.txCardNo.Text;
            info.EmployeeInfo.Name = this.comperson.Text;
            if (this.comperson.Tag != null)
            {
                info.EmployeeInfo.ID = this.comperson.Tag.ToString();
            }
            if (this.comDept.Tag != null)
            {
                info.DeptInfo.ID = this.comDept.Tag.ToString();
            }
            info.DeptInfo.Name = this.comDept.Text;
            info.User01 = this.card.Operator.ID;
            info.EmployeeInfo.OperTime = System.DateTime.Now; //ftpÿ��ͬ���������ͱ��ص�ʱ��,��������дû�й�ϵ
            if (this.ckValue.Checked)
            {
                info.ValidFlag = "1";
            }
            else
            {
                info.ValidFlag = "2";
                info.CancelDate = System.DateTime.Now;//ftpÿ��ͬ���������ͱ��ص�ʱ��,��������дû�й�ϵ
                info.CancelOperInfo.ID = card.Operator.ID;
                info.CancelOperInfo.Name = card.Operator.Name;
            }
            return info;
        }
        private void frmCaseCard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsClose)
            {
                e.Cancel = true;
                this.Visible = false;
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public void ClearInfo()
        {
            this.comperson.Text = "";
            this.comDept.Text = "";
            this.txCardNo.Text = "";
            this.ckValue.Checked = false;
        }
        /// <summary>
        /// �������� 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int SetInfo(FS.HISFC.Models.HealthRecord.ReadCard obj)
        {
            this.txCardNo.Text = obj.CardID; //����
            this.comperson.Tag = obj.EmployeeInfo.ID; //Ա����
            this.comperson.Text = obj.EmployeeInfo.Name;//Ա������
            this.comDept.Tag = obj.DeptInfo.ID;//���Ҵ���
            this.comDept.Text = obj.DeptInfo.Name;//��������
            if (obj.ValidFlag == "1" || obj.ValidFlag == "��Ч")
            {
                this.ckValue.Checked = true;
            }
            else
            {
                this.ckValue.Checked = false;
            }
            return 1;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            this.Visible = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deptList"></param>
        /// <returns></returns>
        public void SetDeptList(ArrayList deptList)
        {
            this.comDept.AppendItems(deptList);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PersonList"></param>
        /// <returns></returns>
        public void SetPersonList(ArrayList PersonList)
        {

            this.comperson.AppendItems(PersonList);
        }

        private void comperson_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.comDept.Focus();
            }
        }

        private void comDept_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txCardNo.Focus();
            }
        }

        private void txCardNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.ckValue.Focus();
            }
        }

        private void ckValue_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void frmCaseCard_Load(object sender, System.EventArgs e)
        {
            this.comperson.Focus();
        }
    }
}