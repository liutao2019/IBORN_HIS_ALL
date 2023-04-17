using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
using FS.FrameWork.Function;

namespace FS.HISFC.Components.InpatientFee.Maintenance
{
    /// <summary>
    /// ucFeeCodeStatModify<br></br>
    /// [��������: ͳ�ƴ���ά��UC]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2006-11-26]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucFeeCodeStatModify : UserControl
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ucFeeCodeStatModify()
        {
            InitializeComponent();
        }

        #region ��������

        /// <summary>
        ///���Ĵ�ӡ���
        /// </summary>
        private string maxPrintOrder = string.Empty;

        /// <summary>
        /// ͳ�ƴ���ʵ��
        /// </summary>
        private FS.HISFC.Models.Fee.FeeCodeStat feeCodeStat = new FS.HISFC.Models.Fee.FeeCodeStat();

        /// <summary>
        /// ����ͳ�ƴ���
        /// </summary>
        private FS.HISFC.BizLogic.Fee.FeeCodeStat feeCodeStatManager = new FS.HISFC.BizLogic.Fee.FeeCodeStat();

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="feeCodeStat"></param>
        public delegate void ClickSave(FS.HISFC.Models.Fee.FeeCodeStat feeCodeStat);

        /// <summary>
        /// �����¼�
        /// </summary>
        public event ClickSave Save;

        /// <summary>
        /// ����ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// ��ǰUc�Ǳ��滹������
        /// </summary>
        private EnumSaveTypes saveType;

        /// <summary>
        /// ��С�����б�
        /// </summary>
        private ArrayList minFeeList = new ArrayList();

        #endregion

        #region ����

        /// <summary>
        /// ��С�����б�
        /// </summary>
        public ArrayList MinFeeList 
        {
            get 
            {
                return this.minFeeList;
            }
            set 
            {
                this.minFeeList = value;
                if (this.minFeeList != null) 
                {
                    this.cmbMinFee.ClearItems();

                    this.cmbMinFee.AddItems(this.minFeeList);
                }
            }
        }

        /// <summary>
        /// �Ǳ��滹������
        /// </summary>
        public EnumSaveTypes SaveType 
        {
            get 
            {
                return this.saveType;
            }
            set 
            {
                this.saveType = value;
                if (this.saveType == EnumSaveTypes.Add) 
                {
                    
                }
            }
        }

        /// <summary>
        /// ���Ĵ�ӡ���
        /// </summary>
        public string MaxPrintOrder 
        {
            get 
            {
                return this.maxPrintOrder;
            }
            set 
            {
                this.maxPrintOrder = value;
            }
        }
        
        /// <summary>
        /// ͳ�ƴ���ʵ��
        /// </summary>
        public FS.HISFC.Models.Fee.FeeCodeStat FeeCodeStat 
        {
            get 
            {
                return this.feeCodeStat;
            }
            set 
            {
                this.feeCodeStat = value;
            }
        }

        #endregion

        #region ˽�з���

        /// <summary>
        /// �������á�ͣ�á�����������
        /// </summary>
        protected virtual void InitValidState()
        {
            ArrayList validStates = new ArrayList();
            
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "1";
            obj.Name = "����";
            validStates.Add(obj);

            FS.FrameWork.Models.NeuObject obj1 = new FS.FrameWork.Models.NeuObject();
            obj1.ID = "0";
            obj1.Name = "ͣ��";
            validStates.Add(obj1);

            FS.FrameWork.Models.NeuObject obj2 = new FS.FrameWork.Models.NeuObject();
            obj2.ID = "2";
            obj2.Name = "����";
            validStates.Add(obj2);

            this.cmbValidState.AddItems(validStates);
        }

        /// <summary>
        /// ���ӱ������������
        /// </summary>
        protected virtual void InitReportType()
        {
            ArrayList reportTypes = new ArrayList();

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "FP";
            obj.Name = "��Ʊ";
            reportTypes.Add(obj);
            
            FS.FrameWork.Models.NeuObject obj1 = new FS.FrameWork.Models.NeuObject();
            obj1.ID = "TJ";
            obj1.Name = "ͳ��";
            reportTypes.Add(obj1);
            
            FS.FrameWork.Models.NeuObject obj2 = new FS.FrameWork.Models.NeuObject();
            obj2.ID = "BA";
            obj2.Name = "����";
            reportTypes.Add(obj2);
            
            FS.FrameWork.Models.NeuObject obj3 = new FS.FrameWork.Models.NeuObject();
            obj3.ID = "ZQ";
            obj3.Name = "֪��Ȩ";
            reportTypes.Add(obj3);
            
            this.cmbReportType.AddItems(reportTypes);
        }

        /// <summary>
        /// ��ʼ�������б�
        /// </summary>
        /// <returns></returns>
        protected virtual int InitCmb()
        {
            try
            {
                this.cmbExecDept.AddItems(this.managerIntegrate.QueryDeptmentsInHos(false));
                this.cmbCenterStatCode.AddItems(this.managerIntegrate.GetConstantList("CENTERFEECODE"));
                this.InitValidState();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return -1;
            }

            InitReportType();

            return 1;
        }

        /// <summary>
        /// �޸Ĺ�������
        /// </summary>
        protected  virtual void Modify()
        {
            this.txtReportCode.Text = feeCodeStat.ID;
            this.txtReportCode.Enabled = false;
            this.txtReportName.Text = feeCodeStat.Name;
            this.txtReportName.Enabled = false;
            this.cmbReportType.Tag = feeCodeStat.ReportType.ID;
            this.cmbReportType.Enabled = false;
            //this.cmbMinFee.Tag = feeCodeStat.MinFee.ID;
            this.cmbMinFee.Tag = feeCodeStat.MinFee.ID.ToString();
            this.cmbMinFee.Enabled = false;
            this.txtFeeStatCode.Text = feeCodeStat.StatCate.ID;
            this.txtFeeStatName.Text = feeCodeStat.StatCate.Name;
            this.txtPrintOrder.Text = feeCodeStat.SortID.ToString();
            this.cmbExecDept.Tag = feeCodeStat.ExecDept.ID;
            this.cmbExecDept.Text = feeCodeStat.ExecDept.Name;
            this.cmbCenterStatCode.Tag = feeCodeStat.CenterStat;
            this.cmbValidState.Tag = ((int)feeCodeStat.ValidState).ToString();
            this.cmbValidState.Text = this.GetValidName(((int)feeCodeStat.ValidState).ToString());
            this.ckbContinue.Enabled = false;
            this.tbp_Main.Focus();
            this.txtFeeStatCode.Focus();
        }
        
        /// <summary>
        /// ���ӹ���
        /// </summary>
        protected virtual void Add()
        {
            this.txtReportCode.Text = feeCodeStat.ID;
            this.txtReportCode.Enabled = false;
            this.txtReportName.Text = feeCodeStat.Name;
            this.txtReportName.Enabled = false;
            //this.cmbReportType.Text = feeCodeStat.ReportType.Name;
            this.cmbReportType.Tag = feeCodeStat.ReportType.ID;
            this.cmbReportType.Enabled = false;
            this.cmbMinFee.Enabled = true;
            this.cmbMinFee.Text = string.Empty;
            this.cmbMinFee.Tag = string.Empty;
            this.txtFeeStatCode.Text = string.Empty;
            this.txtFeeStatName.Text = string.Empty;
            this.txtPrintOrder.Text = maxPrintOrder;
            this.cmbExecDept.Tag = string.Empty;
            this.cmbExecDept.Text = string.Empty;
            this.txtFeeStatName.Text = string.Empty;
            this.cmbCenterStatCode.Tag = string.Empty;
            this.cmbValidState.Tag = "1";
            this.cmbValidState.Text = "����";
            this.cmbValidState.Enabled = true;

            if (this.cmbMinFee.Items.Count == 0)
            {
                this.ckbContinue.Checked = false;
            }
            else
            {
                this.ckbContinue.Checked = true;
            }
        }

        /// <summary>
        /// ��Ч���ж�
        /// </summary>
        /// <returns>��Ч True ��Ч False</returns>
        protected virtual bool IsValid()
        {
            if (this.cmbMinFee.Text == string.Empty || this.cmbMinFee.Tag == null)
            {
                MessageBox.Show(Language.Msg("�������Ʋ���Ϊ��!"));
                this.cmbMinFee.Focus();

                return false;
            }
            if (this.txtFeeStatCode.Text == string.Empty || this.txtFeeStatCode.Text == null)
            {
                MessageBox.Show(Language.Msg("ͳ�ƴ��벻��!"));
                this.txtFeeStatCode.Focus();

                return false;
            }
            else
            {
                // [2007/02/07] �����ӵĴ���,����Ƿ�������
                //              ����ֱ��ͨ��MaxLength=2��������,��Ϊ���ݿ���ֶεĳ����������ֽ�
                //              �����MaxLength��Ϊ2,��ô����������������(4���ֽ�),�ͻ�������
                for (int i = 0, j = this.txtFeeStatCode.Text.Length; i < j; i++)
                {
                    if (!char.IsDigit(this.txtFeeStatCode.Text[i]))
                    {
                        MessageBox.Show("ͳ�ƴ��������С�ڵ���2������", "��ʾ", MessageBoxButtons.OK);
                        return false;
                    }
                }
            }
            if (this.txtFeeStatName.Text == string.Empty || this.txtFeeStatName.Text == null)
            {
                MessageBox.Show(Language.Msg("ͳ�����Ʋ���!"));
                this.txtFeeStatName.Focus();

                return false;
            }

            if (this.txtPrintOrder.Text == "0" || this.txtPrintOrder.Text == null || this.txtPrintOrder.Text == string.Empty)
            {
                MessageBox.Show(Language.Msg("��ӡ˳����Ϊ��!"));
                this.txtPrintOrder.Focus();
                
                return false;
            }
            for (int i = 0, j = this.txtPrintOrder.Text.Length; i < j; i++)
            {
                if (!char.IsDigit(this.txtPrintOrder.Text, i))
                {
                    MessageBox.Show("��ӡ˳��ֻ��Ϊ���֣����������룡", "��ʾ", MessageBoxButtons.OK);
                    txtPrintOrder.Focus();
                    txtPrintOrder.SelectAll();
                    return false;
                }
            }

            if (this.cmbValidState.Text == string.Empty || this.cmbValidState.Text == null)
            {
                MessageBox.Show("��Ч�Ա�ʶ����Ϊ��!");
                this.cmbValidState.Focus();

                return false;
            }

            return true;
        }

        /// <summary>
        /// �����Ч�Ե�����
        /// </summary>
        /// <param name="id">����</param>
        /// <returns>�ɹ� ��Ч�Ե����� ʧ�� null</returns>
        protected virtual string GetValidName(string id)
        {
            string name = string.Empty;
            
            switch (id)
            {
                case "1":
                    name = "����";
                    break;
                case "0":
                    name = "ͣ��";
                    break;
                case "2":
                    name = "����";
                    break;
            }

            return name;
        }

        /// <summary>
        /// ȷ���¼�
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int Confirm() 
        {              
            this.feeCodeStat.MinFee.ID = this.cmbMinFee.Tag.ToString();//��С����
            this.feeCodeStat.MinFee.Name = this.cmbMinFee.Text.ToString();//��С��������

            this.feeCodeStat.StatCate.ID = this.txtFeeStatCode.Text;//ͳ�ƴ���
            this.feeCodeStat.StatCate.Name = this.txtFeeStatName.Text;//ͳ�ƴ�������
            this.feeCodeStat.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.txtPrintOrder.Text);//��ӡ˳��
            this.feeCodeStat.ExecDept.ID = this.cmbExecDept.Tag.ToString();//ִ�п���ID
            this.feeCodeStat.ExecDept.Name = this.cmbExecDept.Text.ToString();//ִ�ƿ�������
            this.feeCodeStat.ReportType.ID = this.cmbReportType.Tag.ToString();//������.
            this.feeCodeStat.CenterStat = this.cmbCenterStatCode.Tag.ToString();//���Ĵ���
            this.feeCodeStat.ValidState = (FS.HISFC.Models.Base.EnumValidState) NConvert.ToInt32(this.cmbValidState.Tag);//��Ч�Ա�ʶ

            try
            {
                //FS.FrameWork.Management.Transaction t = new Transaction(this.feeCodeStatManager.Connection);
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                //t.BeginTransaction();

                this.feeCodeStatManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                int returnValue = 0;

                if (this.saveType == EnumSaveTypes.Add)
                {
                    returnValue = this.feeCodeStatManager.InsertFeeCodeStat(this.feeCodeStat);
                }
                else 
                {
                    returnValue = this.feeCodeStatManager.UpdateFeeCodeStat(this.feeCodeStat);
                }

                if (returnValue <= 0) 
                {
                    //{27950423-3D3C-4ca6-882E-254D455EA2E3}
                    if (this.feeCodeStatManager.DBErrCode == 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("��������ͳ�ƴ�����Ϣ����!��Ϣ�Ѵ���"));
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();

                        MessageBox.Show(Language.Msg("��������ͳ�ƴ�����Ϣ����!") + this.feeCodeStatManager.Err);

                    }

                   
                    return -1;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                
                this.Save(feeCodeStat);

                if (!this.ckbContinue.Checked)
                {
                    this.ParentForm.Close();
                }
            }
            catch 
            {
                return -1;
            }
           
            return 1;
        }

        #endregion

        #region ���з���

        /// <summary>
        /// ���ݴ��������ʼ��
        /// </summary>
        public void Init()
        {
            this.InitCmb();
            
            if (this.saveType == EnumSaveTypes.Add)
            {
                this.Add();
            }
            if (this.saveType == EnumSaveTypes.Modify)
            {
                this.Modify();
            }

        }

        #endregion

        #region ö��

        /// <summary>
        /// ���������
        /// </summary>
        public enum EnumSaveTypes 
        {
            /// <summary>
            /// ����
            /// </summary>
            Add = 0,

            /// <summary>
            /// �޸�
            /// </summary>
            Modify
        }

        #endregion

        #region �¼�

        /// <summary>
        /// ȷ����ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click_1(object sender, EventArgs e)
        {
            //�ж���Ч��
            if (!this.IsValid())
            {
                return;
            }
            if (this.Confirm() == 1)
            {
                MessageBox.Show("�������ݳɹ�", "��ʾ", MessageBoxButtons.OK);
                this.FindForm().Close();
            }
            else
            {
                //{27950423-3D3C-4ca6-882E-254D455EA2E3}
               // MessageBox.Show("��������ʧ��", "��ʾ", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// ���ڹرհ�ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.FindForm().Close();
            }
            catch { }
        }

        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucFeeCodeStatModify_Load(object sender, EventArgs e)
        {
            this.Init();

            try
            {
                this.FindForm().Text = "���ù���";
                this.FindForm().MinimizeBox = false;
                this.FindForm().MaximizeBox = false;
            }
            catch { }
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter) 
            {
                SendKeys.Send("{TAB}");

                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        #endregion
    }
}
