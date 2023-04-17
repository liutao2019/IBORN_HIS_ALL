using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.HealthRecord.EnumServer;
namespace FS.HISFC.Components.HealthRecord.ICD
{
    /// <summary>
    /// frmICDInfo<br></br>
    /// [��������: ����ICDά����Ϣ¼��]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-04-20]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class frmICDInfo : Form
    {
        public frmICDInfo()
        {
            InitializeComponent();
        }
        #region   ˽�б���

        //�������ͣ� ���ӣ��޸ģ���ͣ�� ����������趨�����пؼ������� 
        private FS.HISFC.Models.HealthRecord.EnumServer.EditTypes editType;
        //����ICD���� ���洢�޸�ǰICD��Ϣ
        private FS.HISFC.Models.HealthRecord.ICD orgICD = new FS.HISFC.Models.HealthRecord.ICD();
        //����ICD���� ���洢�޸ĺ�ICD��Ϣ
        private FS.HISFC.Models.HealthRecord.ICD newICD = new FS.HISFC.Models.HealthRecord.ICD();
        //����ҵ��� ����
        private FS.HISFC.BizLogic.HealthRecord.ICD myICD = new FS.HISFC.BizLogic.HealthRecord.ICD();
        //ICD���
        private ICDTypes myICDType;

        #endregion

        #region	��������

        /// <summary>
        /// �������ͣ� ���ӣ��޸ģ���ͣ�� 
        /// EditType.ADD ����
        /// EditType.Modify�޸�
        /// EditType.Cancel ���� 
        /// </summary>
        public EditTypes EditType
        {
            set
            {
                editType = value;
            }
        }
        /// <summary>
        /// �޸�ǰ��Ϣ
        /// </summary>
        public FS.HISFC.Models.HealthRecord.ICD OrgICD
        {
            set
            {
                orgICD = value;
            }
        }
        /// <summary>
        /// �޸ĺ���Ϣ
        /// </summary>
        public FS.HISFC.Models.HealthRecord.ICD NewICD
        {
            set
            {
                newICD = value;
            }
        }
        /// <summary>
        /// ����ICD��� ICD10 ��ICD9 ��ICDOperation 
        /// </summary>
        public ICDTypes ICDType
        {
            set
            {
                myICDType = value;
            }
        }
        #endregion

        #region  ���ڿؼ����¼�

        #region �����Load �¼�
        private void ucICDInfo_Load(object sender, System.EventArgs e)
        {
            FS.HISFC.BizLogic.HealthRecord.ICD icd = new FS.HISFC.BizLogic.HealthRecord.ICD();
            //�����Ա��б�
            this.SexComBox.AppendItems(FS.HISFC.Models.Base.SexEnumService.List());
            if (SexComBox.Items != null)
            {
                if (SexComBox.Items.Count > 0)
                {
                    SexComBox.SelectedIndex = 0;
                }
            }

            //�����ж������� ���޸� 
            if (editType == EditTypes.Add)
            {
                //����
                this.IsValid.Checked = true;  //���ӵĵ�ʱ�� ��Ч��
                this.IsValid.Enabled = false; //��Ч
                ContinueInput.Checked = true; //������������
                cbTraditional.Checked = false;
                Is30Illness.Checked = false;
                IsInfection.Checked = false;
                IsTumour.Checked = false;
                this.Text = "����";
            }
            else
            {
                //�޸�
                this.textSeqNO.Text = orgICD.User01.ToString();//���к�
                this.textICDid.Text = orgICD.ID; //ICD����
                this.textICDName.Text = orgICD.Name;//ICD����
                this.WBCode.Text = orgICD.WBCode;
                this.textSpellCode.Text = orgICD.SpellCode;//ƴ����
                this.textUserCode.Text = orgICD.UserCode;//�Զ������
                this.cbTraditional.Checked = orgICD.TraditionalDiag;
                //if (orgICD.Is30Illness == "��")
                //{
                    this.Is30Illness.Checked = FS.FrameWork.Function.NConvert.ToBoolean(orgICD.Is30Illness); //�Ƿ�30�ּ���
                //}
                //else
                //{
                //    this.Is30Illness.Checked = false; //�Ƿ�30�ּ���
                //}
                //if (orgICD.IsInfection == "��")
                //{
                    this.IsInfection.Checked = FS.FrameWork.Function.NConvert.ToBoolean(orgICD.IsInfection);//�Ƿ��Ǵ�Ⱦ��
                //}
                //else
                //{
                //    this.IsInfection.Checked = false;//�Ƿ��Ǵ�Ⱦ��
                //}
                //if (orgICD.IsTumour == "��")
                //{
                    this.IsTumour.Checked = FS.FrameWork.Function.NConvert.ToBoolean(orgICD.IsTumour);// //�Ƿ��Ƕ�������
                //}
                //else
                //{
                //    this.IsTumour.Checked = false; //�Ƿ��Ƕ�������
                //}
                this.IsValid.Checked = orgICD.IsValid;
                //if (orgICD.IsValid == "��")
                //{
                //    this.IsValid.Checked = true;//�Ƿ���Ч
                //}
                //else
                //{
                //    this.IsValid.Checked = false;//�Ƿ���Ч
                //}
                this.SexComBox.Tag = orgICD.SexType.ID; //�����Ա�
                ContinueInput.Enabled = false;  //�޸Ĳ�������������
                //�������޸�ID
                textICDid.ReadOnly = true;
                this.Text = "�޸�";
            }
        }
        #endregion

        #region  �رհ�ť
        private void button2_Click(object sender, System.EventArgs e)
        {
            //�رձ�����
            this.Close();
        }
        #endregion

        #region  ȷ����ť
        private void button1_Click(object sender, System.EventArgs e)
        {
            try
            {
                //�ж����ݵ���Ч��
                if (!ValidCheck())
                {
                    //�е�������Ч
                    return;
                }
                //��ȡ��Ϣ
                this.GetICDinfo();

                if (newICD == null) //��ȡ��Ϣʧ��
                {
                    return;
                }
                if (Save())
                {
                    //����ɹ� ��ˢ������������
                    SaveButtonClick(newICD);
                    if (!ContinueInput.Enabled)
                    {
                        //������޸����� �޸����ֱ�ӹرմ���
                        this.Close();
                    }
                    textICDid.Focus(); //��ϱ����ý���
                    //�ж��Ƿ�����������
                    if (ContinueInput.Checked)
                    {
                        //��մ��� ��������
                        this.textSpellCode.Text = "";
                        this.textICDid.Text = "";
                        this.textICDName.Text = "";
                        this.textSeqNO.Text = "";
                        this.textUserCode.Text = "";
                        this.Is30Illness.Checked = false;
                        this.IsInfection.Checked = false;
                        this.IsTumour.Checked = false;
                    }
                    else
                    {
                        this.Close(); //�رմ���
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region �س��¼�
        /// <summary>
        /// ICD id �Ļس��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textICDid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.textICDName.Focus();
            }
        }

        /// <summary>
        ///ICD���� �� �س��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textICDName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.textSpellCode.Focus();
            }

        }

        /// <summary>
        /// ƴ���� �� �س��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textSpellCode_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.WBCode.Focus();
            }

        }
        /// <summary>
        /// �Զ���  �� �س��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textUserCode_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.textSeqNO.Focus();
            }
        }
        /// <summary>
        /// 30 �м��� ��ť �� �س��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Is30Illness_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.IsInfection.Focus();
            }
        }
        /// <summary>
        /// ��Ⱦ ��ť �� �س��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IsInfection_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.IsTumour.Focus();
            }
        }
        /// <summary>
        /// ������ť �� �س��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IsTumour_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (this.IsValid.Enabled) //�����Ч
                {
                    this.IsValid.Focus(); //��ý���
                }
                else
                {
                    this.ContinueInput.Focus(); //���� �� ����һ���ؼ�
                }
            }
        }
        /// <summary>
        /// ��Ч�԰�ť �� �س��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IsValid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.ContinueInput.Focus();
            }
        }
        /// <summary>
        /// ��������Ļس��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContinueInput_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.button1.Focus();
            }

        }
        /// <summary>
        /// ��ŵĻس��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textSeqNO_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.Is30Illness.Focus();
            }
        }
        #endregion

        #endregion

        #region �Զ��庯��

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <returns>�ɹ����� true ʧ�ܷ��� false</returns>
        private bool Save()
        {
            //��������

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(myICD.Connection);
            ////��ʼ����
            //t.BeginTransaction();

            myICD.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //ִ�в������
            int iReturn = 0; //����ֵ 
            string errInfo = "";
            //��������ӵ� �����
            if (editType == EditTypes.Add)
            {
                //����ICD��Ϣ
                iReturn = myICD.Insert(newICD, myICDType);

                if (iReturn > 0)
                {
                    ArrayList alInfo = new ArrayList();
                    alInfo.Add(newICD);
                    int param = Function.SendBizMessage(alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.ICD10, ref errInfo);

                    if (param == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        Function.ShowMessage("ICD10���ʧ�ܣ�����ϵͳ�����������Ϣ��" + errInfo, MessageBoxIcon.Error);
                        return false;
                    }

                    //�ύ����
                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show("����ɹ�");
                    return true;
                }
                else
                {
                    //��������
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ʧ��" + myICD.Err);
                    return false;
                }
            }
            else if (editType == EditTypes.Modify) //������ִ�и��²�����
            {
                iReturn = myICD.Update(orgICD, newICD, myICDType);
                if (iReturn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ICD��Ϣʧ��!" + myICD.Err);
                    return false;
                }
                if (iReturn == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("û���ҵ��ɸ��µ�ICD��Ϣ,�����޸ĵ�ICD��Ϣ�Ѿ����������޸�!");
                    return false;
                }

                ArrayList alInfo = new ArrayList();
                alInfo.Add(newICD);
                int param = Function.SendBizMessage(alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Modify, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.ICD10, ref errInfo);

                if (param == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                    Function.ShowMessage("ICD10�޸�ʧ�ܣ�����ϵͳ�����������Ϣ��" + errInfo, MessageBoxIcon.Error);
                    return false;
                }


                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("����ɹ�");
                return true;
            }

            return true;
        }
        #endregion

        #region �Զ��庯��
        /// <summary>
        /// ��ȡ�޸Ļ���������Ϣ
        /// </summary>
        private void GetICDinfo()
        {

            try
            {
                if (editType == EditTypes.Add)
                {
                    newICD.User01 = textSeqNO.Text;    //�������
                    newICD.ID = this.textICDid.Text;					 //ICD����
                    newICD.Name = this.textICDName.Text;				 //ICD����
                    newICD.SpellCode = this.textSpellCode.Text; //ƴ����
                    newICD.WBCode = this.WBCode.Text; //����� 
                    newICD.UserCode = this.textUserCode.Text;    //ͳ����
                    newICD.Is30Illness = FS.FrameWork.Function.NConvert.ToInt32(Is30Illness.Checked).ToString();		 //�Ƿ���30�ּ���
                    newICD.IsInfection = FS.FrameWork.Function.NConvert.ToInt32(IsInfection.Checked).ToString();		 //�Ƿ��Ǵ�Ⱦ��
                    newICD.IsTumour = FS.FrameWork.Function.NConvert.ToInt32(IsTumour.Checked).ToString();//;            //�Ƿ�������
                    newICD.IsValid = true;
                    newICD.SexType.ID = this.SexComBox.Tag.ToString(); //�����Ա� 
                    newICD.SexType.Name = this.SexComBox.Text;
                    newICD.TraditionalDiag = cbTraditional.Checked;

                }
                if (editType == EditTypes.Modify)
                {
                    newICD = orgICD.Clone();
                    newICD.User01 = textSeqNO.Text;    //�������
                    newICD.Name = this.textICDName.Text;				 //ICD����
                    newICD.SpellCode = this.textSpellCode.Text; //ƴ����
                    newICD.WBCode = this.WBCode.Text; //����� 
                    newICD.UserCode = this.textUserCode.Text;    //ͳ����
                    newICD.Is30Illness = FS.FrameWork.Function.NConvert.ToInt32(Is30Illness.Checked).ToString();		 //�Ƿ���30�ּ���
                    newICD.IsInfection = FS.FrameWork.Function.NConvert.ToInt32(IsInfection.Checked).ToString();		 //�Ƿ��Ǵ�Ⱦ��
                    newICD.IsTumour = FS.FrameWork.Function.NConvert.ToInt32(IsTumour.Checked).ToString();//;            //�Ƿ�������
                    newICD.IsValid = this.IsValid.Checked; //��Ч��
                    newICD.SexType.ID = this.SexComBox.Tag.ToString(); //�����Ա�
                    newICD.SexType.Name = this.SexComBox.Text;
                    newICD.TraditionalDiag = cbTraditional.Checked;
                }
                #region ȡ����Ա �Ͳ���ʱ��
                //����ط����Ǻ�׼��ֻ����ʱ��ǰ̨��ʾ�á�
                FS.HISFC.BizLogic.HealthRecord.ICD icd = new FS.HISFC.BizLogic.HealthRecord.ICD();
                //����Ա ���� ����
                newICD.OperInfo.ID = icd.Operator.ID;
                newICD.OperInfo.Name = icd.Operator.Name;
                //����ʱ��
                newICD.OperInfo.OperTime = icd.GetDateTimeFromSysDateTime();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// �ж��������ݵ���Ч��
        /// </summary>
        /// <returns>���ݶ��Ϻ����� ����TRUE  ���򷵻�false </returns>
        private bool ValidCheck()
        {
            try
            {
                //�������� �洢 ICD 
                ArrayList alReturn = new ArrayList();

                if (textICDid.Text == "")
                {
                    textICDid.Focus();
                    MessageBox.Show("ICD ���벻��Ϊ��");//
                    return false;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(textICDid.Text, 20))
                {
                    textICDid.Focus();
                    MessageBox.Show("ICD�������"); //��ʾ����
                    return false;
                }
                //��������� �����жϱ����Ƿ���� ���޸Ĳ��ж�
                if (editType == EditTypes.Add)
                {
                    //�ж�ICD�����Ƿ����
                    alReturn = myICD.IsExistAndReturn(textICDid.Text, myICDType, true);

                    if (alReturn == null)
                    {
                        MessageBox.Show("���ICD��Ϣ����!" + myICD.Err);
                        return false;
                    }
                    if (alReturn.Count > 0)
                    {
                        textICDid.Focus();
                        MessageBox.Show("���� " + textICDid.Text + " �Ѿ�����");
                        return false;
                    }
                }
                if (textICDName.Text == null || textICDName.Text == "")
                {
                    textICDName.Focus();
                    MessageBox.Show("����������");
                    return false;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(textICDName.Text, 100))
                {
                    textICDName.Focus();
                    MessageBox.Show("ICD ���ƹ���");
                    return false;
                }

                if (!FS.FrameWork.Public.String.ValidMaxLengh(textSpellCode.Text, 8))
                {
                    textSpellCode.Focus();
                    MessageBox.Show("ƴ���� ����");
                    return false;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(WBCode.Text, 8))
                {
                    WBCode.Focus();
                    MessageBox.Show("����� ����");
                    return false;
                }


                if (!FS.FrameWork.Public.String.ValidMaxLengh(textUserCode.Text, 8))
                {
                    textUserCode.Focus();
                    MessageBox.Show("ͳ���� ����");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// ����ƴ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textICDName_Leave(object sender, System.EventArgs e)
        {
            try
            {
                //���� ƴ����
                //���� ʵ���� ҵ����
                FS.HISFC.BizLogic.Manager.Spell mySpell = new FS.HISFC.BizLogic.Manager.Spell();
                //���� ʵ����ʵ����
                FS.HISFC.Models.Base.Spell spCode = new FS.HISFC.Models.Base.Spell();
                //��ѯ
                spCode = (FS.HISFC.Models.Base.Spell)mySpell.Get(textICDName.Text);
                //������
                if (spCode.SpellCode == null)
                    return;
                //�ж��Ƿ񳬳�
                if (spCode.SpellCode.Length > 8)
                {
                    spCode.SpellCode = spCode.SpellCode.Substring(0, 8);
                }
                //�ж��Ƿ񳬳�
                if (spCode.WBCode.Length > 8)
                {
                    spCode.WBCode = spCode.WBCode.Substring(0, 8);
                }
                if (textSpellCode.Text == "")
                {
                    //��ֵ
                    textSpellCode.Text = spCode.SpellCode; //ƴ���� 
                }
                if (WBCode.Text == "")
                {
                    WBCode.Text = spCode.WBCode; //����� 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void WBCode_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.textUserCode.Focus();
            }
        }

    }
}