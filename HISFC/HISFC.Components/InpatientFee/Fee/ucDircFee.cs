using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using FS.FrameWork.Function;
using FS.HISFC.Models.Fee.Inpatient;
using FS.HISFC.Models.RADT;
using System.Collections;
using FS.FrameWork.WinForms.Forms;

namespace FS.HISFC.Components.InpatientFee.Fee
{
    /// <summary>
    /// ucDircFee<br></br>
    /// [��������: סԺֱ���շ�UC]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2006-11-06]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucDircFee : FS.FrameWork.WinForms.Controls.ucBaseControl,  FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        /// <summary>
        /// 
        /// </summary>
        public ucDircFee()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ���תҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        protected FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        protected ControlParam controlManager = new ControlParam();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// ���ù���ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ��������ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ҩƷҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyInterate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// ��ͬ��λҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        /// <summary>
        /// ��Ʊ������Ϣ
        /// </summary>
        protected DataSet dsInvoice = null;

        /// <summary>
        /// �Ƿ���Ը��ĺ�ͬ��λ
        /// ��������Ը���,Ĭ��Ϊ�Է�
        /// </summary>
        protected bool isCanChoosePact = false;

        /// <summary>
        /// �Ƿ�ͨ���������ɷ�����Ϣ
        /// </summary>
        protected bool isInputName = false;

        /// <summary>
        /// ֧����ʽѡ��ؼ�
        /// </summary>
        FS.HISFC.Components.Common.Controls.ucBalancePay balancePayControl = new FS.HISFC.Components.Common.Controls.ucBalancePay();

        /// <summary>
        /// toolBarService
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// �Ƿ��ж�Ƿ��,Y���ж�Ƿ�ѣ�����������շ�,M���ж�Ƿ�ѣ���ʾ�Ƿ�����շ�,N�����ж�Ƿ��
        /// </summary>
        FS.HISFC.Models.Base.MessType messtype = FS.HISFC.Models.Base.MessType.Y;

        #endregion
        #region IInterfaceContainer ��Ա

        Type[] FS.FrameWork.WinForms.Forms.IInterfaceContainer.InterfaceTypes
        {
            get
            {
                Type[] type = new Type[1];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy);

                return type;
            }
        }

        #endregion
        #region ����
        /// <summary>
        /// �Ƿ���Ը��ĺ�ͬ��λ
        /// ��������Ը���,Ĭ��Ϊ�Է�
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ���Ը��ĺ�ͬ��λ,��������Ը���,Ĭ��Ϊ�Է�")]
        public bool IsCanChoosePact 
        {
            get 
            {
                return this.isCanChoosePact;
            }
            set 
            {
                this.isCanChoosePact = value;

                this.cmbPact.Enabled = this.isCanChoosePact;
            }
        }

        /// <summary>
        /// ��ǰ�����Ƿ����ģʽ
        /// </summary>
        protected new bool DesignMode
        {
            get
            {
                return (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv");


            }
        }

        [Category("�ؼ�����"), Description("�Ƿ��ж�Ƿ��,Y���ж�Ƿ�ѣ�����������շ�,M���ж�Ƿ�ѣ���ʾ�Ƿ�����շ�,N�����ж�Ƿ��")]
        public FS.HISFC.Models.Base.MessType MessageType
        {
            get
            {
                return this.messtype;
            }
            set
            {
                this.messtype = value;
            }
        }

        #endregion

        #region ˽�з���

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int Init() 
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڳ�ʼ��,��ȴ�...");
            Application.DoEvents();

            //��ʼ��סԺ����
            if (InitDept() == -1) 
            {
                return -1;
            }

            //��ʼ����ͬ��λ,
            if (this.InitPact() == -1) 
            {
                return -1;
            }

            //��ʼ��ҽ��
            if (this.InitDoct() == -1) 
            {
                return -1;
            }

            //��ʼ���շѿؼ�
            this.ucInpatientCharge1.Init(string.Empty);
            this.feeIntegrate.MessageType = this.MessageType;
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return 1;
        }

        /// <summary>
        /// ��ʼ��ҽ����Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int InitDoct()
        {
            ArrayList doctList = this.managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (doctList == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(Language.Msg("��ʼ��ҽ���б����!") + this.managerIntegrate.Err);

                return -1;
            }
            this.cmbDoct.AddItems(doctList);

            return 1;
        }

        /// <summary>
        /// ��ʼ����ͬ��λ��Ϣ
        /// </summary>
        /// <returns>�ɹ�1 ʧ�� -1</returns>
        private int InitPact()
        {
            //�������ѡ���ͬ��λ,��ô�Ժ�ͬ��λ��Ϣ���л�ȡ���к�ͬ��λ
            //��䵽��ͬ��λѡ���combox��,���������ѡ��,Ĭ�ϳ�ʼһ���Էѵĺ�ͬ��λ.
            if (this.isCanChoosePact)
            {
                ArrayList pactList = this.pactManager.QueryPactUnitInPatient();
                if (pactList == null)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show(Language.Msg("��ʼ����ͬ��λ�б����!") + this.pactManager.Err);

                    return -1;
                }
                this.cmbPact.Enabled = true;
            }
            else
            {
                FS.FrameWork.Models.NeuObject tempObj = new FS.FrameWork.Models.NeuObject();
                tempObj.ID = "1";
                tempObj.Name = "�Է�";

                ArrayList pactList = new ArrayList();
                pactList.Add(tempObj);

                this.cmbPact.AddItems(pactList);

                this.cmbPact.SelectedIndex = 0;

                this.cmbPact.Enabled = false;
            }

            return 1;
        }

        /// <summary>
        /// ��ʼ��סԺ����
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int InitDept()
        {
            ArrayList deptList = this.managerIntegrate.QueryDeptmentsInHos(true);
            if (deptList == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(Language.Msg("��ʼ�������б����!") + this.managerIntegrate.Err);

                return -1;
            }
            this.cmbDept.AddItems(deptList);

            return 1;
        }

        /// <summary>
        /// ��ʾ���߻�����Ϣ
        /// </summary>
        protected virtual void SetInfomaition() 
        {
            this.txtName.Text = this.patientInfo.Name;
            this.cmbDept.Tag = this.patientInfo.PVisit.PatientLocation.Dept.ID;
            this.cmbDoct.Tag = this.patientInfo.PVisit.AdmittingDoctor.ID;
            this.txtInDate.Text = this.patientInfo.PVisit.InTime.ToShortDateString();
            this.txtBedNO.Text = this.patientInfo.PVisit.PatientLocation.Bed.ID;
            this.txtNurseStation.Text = this.patientInfo.PVisit.PatientLocation.NurseCell.Name;
            this.txtLeftCost.Text = this.patientInfo.FT.LeftCost.ToString();
            this.txtDoct.Text = this.patientInfo.PVisit.AdmittingDoctor.Name;

        }

        /// <summary>
        /// ��֤��Ժ���ߵ���Ժ״̬�Ƿ����ֱ���շ�
        /// ��Ժ����,�����޷���Ժ���� ��δ���ﻼ�� ������ֱ���շ�
        /// </summary>
        /// <returns>�����շ� True ������ false</returns>
        protected virtual bool IsPatientStateValid() 
        {
            //�ж��Ƿ��Ժ
            if (this.patientInfo.PVisit.InState.ID.ToString() == "N" || this.patientInfo.PVisit.InState.ID.ToString() == "O")
            {
                MessageBox.Show(Language.Msg("�û����Ѿ���Ժ!"));
                this.ucQueryInpatientNO.Focus();
                this.ucQueryInpatientNO.TextBox.SelectAll();

                this.patientInfo.ID = null;

                return false;
            }

            //�ж�û�н���
            if (this.patientInfo.PVisit.InState.ID.ToString() == "R")
            {
                MessageBox.Show(Language.Msg("�û��߻�û�н���,��ȥ����������շ�"));

                this.ucQueryInpatientNO.Focus();
                this.ucQueryInpatientNO.TextBox.SelectAll();

                this.patientInfo.ID = null;

                return false;
            }

            return true;
        }

        /// <summary>
        /// ͨ������������û��߻�����Ϣ
        /// </summary>
        /// <returns>�ɹ� ���߻�����Ϣʵ�� ʧ��: null</returns>
        protected virtual FS.HISFC.Models.RADT.PatientInfo GetPatientInfoFromInputName() 
        {
            string temp = this.controlManager.QueryControlerInfo("100024");

            string pNO = this.inpatientManager.GetTempPatientNO(temp);
            if (pNO == null || pNO == string.Empty) 
            {
                pNO = temp + "000000";
                MessageBox.Show(Language.Msg("��ȡ��ʱסԺ�������!ϵͳ����ʼ��סԺ��"));
            }

            pNO = (long.Parse(pNO) + 1).ToString();

            pNO = pNO.PadLeft(10, '0');

            MessageBox.Show("������ʱסԺ����Ϊ" + pNO, "��ʾ");

            this.ucQueryInpatientNO.TextBox.Text = pNO;

            //this.patientInfo.ID = "ZY01" + pNO;

            this.patientInfo.ID = radtIntegrate.GetNewInpatientNO();

            this.patientInfo.PID.PatientNO = pNO;
            this.patientInfo.PID.CardNO = pNO;
            this.patientInfo.Name = this.txtName.Text;//����
            this.patientInfo.PVisit.PatientLocation.Dept.Name = this.cmbDept.Text; //����
            this.patientInfo.PVisit.PatientLocation.Dept.ID = this.cmbDept.Tag.ToString();//���Ҵ���
            this.patientInfo.PVisit.PatientLocation.NurseCell.ID = this.cmbDept.Tag.ToString(); //��ʿվ��ʱ�ÿ��Ҵ���
            this.patientInfo.Sex.ID = "U";//�Ա�
            this.patientInfo.PVisit.InTime = this.inpatientManager.GetDateTimeFromSysDateTime();//��Ժ����
            this.patientInfo.Pact.ID = this.cmbPact.Tag.ToString();
            this.patientInfo.Pact.Name = this.cmbPact.Text;
            this.patientInfo.Pact.PayKind.ID = "01";//�������
            this.patientInfo.Pact.Name = "�Է�";//��ͬ��λ����
            this.patientInfo.PVisit.AdmitSource.ID = "0";//��Ժ��Դ
            this.patientInfo.PVisit.InSource.ID = "0";//��Ժ;��
            this.patientInfo.PVisit.Circs.ID = "0"; //��Ժ���
            this.patientInfo.BalanceNO = 0; //�������

            this.txtInDate.Text = this.patientInfo.PVisit.InTime.ToShortDateString();

            return this.patientInfo;
        }

        /// <summary>
        /// �жϺϷ�����
        /// </summary>
        /// <returns>����true ������ false</returns>
        protected virtual bool IsValid() 
        {
            if (this.cmbDoct.Tag == null || this.cmbDoct.Tag.ToString() == string.Empty || this.cmbDoct.Text == string.Empty)
            {
                MessageBox.Show(Language.Msg("�����뿪��ҽ��"));
                this.cmbDoct.Focus();

                return false;
            }

            if (this.ucQueryInpatientNO.TextBox.Text.Trim() == string.Empty || this.ucQueryInpatientNO.TextBox.Text == null)
            {
                MessageBox.Show(Language.Msg("��ѡ��һ������!"));

                return false;
            }

            if (this.patientInfo == null)
            {
                MessageBox.Show(Language.Msg("�����뻼�߻�����Ϣ"));

                return false;
            }

            if (this.patientInfo.ID == null || this.patientInfo.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("��ѡ��һ������!"));
                
                return false ;
            }
            if (this.cmbDept.Tag == null || this.cmbDept.Tag.ToString() == string.Empty)
            {
                MessageBox.Show(Language.Msg("������סԺ���ң�"));
                this.cmbDept.Focus();
                return false;

            }

            try
            {
                patientInfo.PVisit.PatientLocation.Dept.Name = ((FS.HISFC.Models.Base.Department)this.cmbDept.SelectedItem).Memo;//��������
            }
            catch 
            {
                MessageBox.Show(Language.Msg("סԺ�������벻��ȷ�����������룡"));
                return false;
            }
            return this.ucInpatientCharge1.IsValid();
        }

        /// <summary>
        /// ���ɽ�����ϸ��Ϣ����
        /// </summary>
        /// <param name="invoiceNO">��Ʊ��</param>
        /// <param name="operTime">����ʱ��</param>
        /// <param name="balanceNO">�������</param>
        /// <param name="list">��ϸ���</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>�ɹ�: ����BalanceList�ķ��ͼ��� ʧ�� null</returns>
        protected ArrayList MakeBalanceListFromFeeItemList(string invoiceNO, DateTime operTime, int balanceNO, ArrayList list, ref string errText) 
        {
            ArrayList balanceLists = new ArrayList();

            dsInvoice = new DataSet();

            if (this.feeIntegrate.GetInvoiceClass("ZY01", ref dsInvoice) == -1)
            {
                errText = "��÷�Ʊ��Ϣ����!" + this.feeIntegrate.Err;

                return null;
            }

            dsInvoice.Tables[0].PrimaryKey = new DataColumn[] { dsInvoice.Tables[0].Columns["FEE_CODE"] };
           
            foreach (FeeItemList f in list)
            {
                DataRow rowFind = dsInvoice.Tables[0].Rows.Find(new object[] { f.Item.MinFee.ID });
                if (rowFind == null)
                {
                    errText = "��ʼ����Ʊʧ��,��ά����Ʊ��������С����Ϊ" + f.Item.MinFee.Name + "�ķ�Ʊ��Ŀ";
                        //"��С����Ϊ" + f.Item.MinFee.ID + "����С����û����MZ01�ķ�Ʊ������ά��";

                    return null;
                }
          
                rowFind["TOT_COST"] = NConvert.ToDecimal(rowFind["TOT_COST"].ToString()) + f.FT.OwnCost;
                rowFind["OWN_COST"] = NConvert.ToDecimal(rowFind["OWN_COST"].ToString()) + f.FT.OwnCost;
                rowFind["PAY_COST"] = NConvert.ToDecimal(rowFind["PAY_COST"].ToString()) + 0;
                rowFind["PUB_COST"] = NConvert.ToDecimal(rowFind["PUB_COST"].ToString()) + 0;
            }

            BalanceList balanceList = null;

            for (int i = 1; i < 100; i++)
            {
                //�ҵ���ͬ��ӡ���,��ͬһͳ�����ķ��ü���
                DataRow[] rowFind = dsInvoice.Tables[0].Select("SEQ = " + i.ToString(), "SEQ ASC");
                //���û���ҵ�˵���Ѿ��ҹ������Ĵ�ӡ���,���з����Ѿ��ۼ����.
                if (rowFind.Length == 0)
                {

                }
                else
                {
                    balanceList = new BalanceList();

                    foreach (DataRow row in rowFind)
                    {
                        balanceList.BalanceBase.FT.PubCost += NConvert.ToDecimal(row["PUB_COST"].ToString());
                        balanceList.BalanceBase.FT.OwnCost += NConvert.ToDecimal(row["OWN_COST"].ToString());
                        balanceList.BalanceBase.FT.PayCost += NConvert.ToDecimal(row["PAY_COST"].ToString());
                    }

                    balanceList.BalanceBase.FT.TotCost = balanceList.BalanceBase.FT.OwnCost + balanceList.BalanceBase.FT.PayCost + balanceList.BalanceBase.FT.PubCost;

                    if (balanceList.BalanceBase.FT.TotCost <= 0)
                    {
                        continue;
                    }

                    balanceList.BalanceBase.Invoice.ID = invoiceNO;
                    balanceList.BalanceBase.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                    balanceList.FeeCodeStat.StatCate.ID = rowFind[0]["FEE_STAT_CATE"].ToString();
                    balanceList.FeeCodeStat.StatCate.Name= rowFind[0]["FEE_STAT_NAME"].ToString();
                    balanceList.FeeCodeStat.SortID = i;
                    balanceList.BalanceBase.BalanceOper.ID = this.inpatientManager.Operator.ID;
                    balanceList.BalanceBase.BalanceOper.OperTime = operTime;
                    balanceList.BalanceBase.BalanceType.ID = FS.HISFC.Models.Fee.EnumBalanceType.D;
                    balanceList.BalanceBase.BalanceOper.Dept.ID = ((FS.HISFC.Models.Base.Employee)this.inpatientManager.Operator).Dept.ID;
                    balanceList.BalanceBase.ID = balanceNO.ToString();

                    balanceLists.Add(balanceList);
                }
            }

            return balanceLists;
        }

        /// <summary>
        /// ���ɽ�������Ϣ
        /// </summary>
        /// <param name="invoiceNO">��Ʊ��</param>
        /// <param name="operTime">����ʱ��</param>
        /// <param name="balanceNO">�������</param>
        /// <param name="balanceLists">����BalanceList�ķ��ͼ���</param>
        /// <returns>�ɹ�:��Ʊס����Ϣʵ�� ʧ��: null</returns>
        protected FS.HISFC.Models.Fee.Inpatient.Balance MakeBalanceFromBalanceList(string invoiceNO, DateTime operTime, int balanceNO, ArrayList balanceLists) 
        {
            FS.HISFC.Models.Fee.Inpatient.Balance balance = new FS.HISFC.Models.Fee.Inpatient.Balance();

            balance = ((FS.HISFC.Models.Fee.Inpatient.Balance)(balanceLists[0] as BalanceList).BalanceBase).Clone();
            balance.FT = new FS.HISFC.Models.Base.FT();

            foreach (BalanceList balanceList in balanceLists) 
            {
                balance.FT.TotCost += balanceList.BalanceBase.FT.TotCost;
                balance.FT.OwnCost += balanceList.BalanceBase.FT.OwnCost;
                balance.FT.PayCost += balanceList.BalanceBase.FT.PayCost ;
                balance.FT.PubCost += balanceList.BalanceBase.FT.PubCost;
            }

            balance.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
            balance.FT.SupplyCost = balance.FT.TotCost;
			balance.BeginTime = operTime;
            balance.EndTime = operTime;
            balance.PrintTimes = 1;
			balance.IsMainInvoice = true;
            balance.IsLastBalance = false;
            balance.ID = balanceNO.ToString();

            return balance;
        }

        /// <summary>
        /// ��ý������
        /// </summary>
        /// <returns>�ɹ� ������� ʧ�� -1</returns>
        private int GetBalanceNO() 
        {
            //��ҵ����ȡ�������
            string balanceNO = string.Empty;

            balanceNO = this.inpatientManager.GetNewBalanceNO(this.patientInfo.ID);
            if (balanceNO == null)
            {
                return -1;
            }
            if (balanceNO == "-1") 
            {
                balanceNO = "1";
            }

            return NConvert.ToInt32(balanceNO);
        }

        /// <summary>
        /// ���뻼�߻�����Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int InsertPatientInfo() 
        {
            int iReturn = 0;

        SetNewNo:

            string pNO = string.Empty;

            string parm = this.controlManager.QueryControlerInfo("100024");

            pNO = this.inpatientManager.GetTempPatientNO(parm);
            if (pNO == null || pNO == string.Empty)
            {
                pNO = parm + "000000";
            }

            pNO = ((long.Parse(pNO) + 1).ToString()).PadLeft(10, '0');

            this.ucQueryInpatientNO.txtInputCode.Text = pNO;

            //this.patientInfo.ID = "ZY01" + pNO;

            this.patientInfo.ID = radtIntegrate.GetNewInpatientNO();

            this.patientInfo.PID.PatientNO = pNO;
            this.patientInfo.PID.CardNO = pNO;

            //״̬������Ϊ��Ժ״̬�ſ��������շ�
            //this.patientInfo.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.O;
            this.patientInfo.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.I;

            //���뻼������
            iReturn = this.radtIntegrate.RegisterPatient(this.patientInfo);
            if (iReturn <= 0)
            {

                if (this.radtIntegrate.DBErrCode == 1)
                {
                    goto SetNewNo;
                }
                else
                {
                    MessageBox.Show(this.patientInfo.PID.PatientNO + Language.Msg("�Ѿ����ã�������������������סԺ��") + this.radtIntegrate.Err);
                    
                    return -1;                    
                }

            }
            //����û�в�����ǣ�����ģ�鲻��ȡ���ֻ���
            iReturn = this.radtIntegrate.UpdatePatientCaseFlag(this.patientInfo.ID, "0");

            if (iReturn <= 0)
            {
                MessageBox.Show("���²�����ǳ���" + this.radtIntegrate.Err);
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ���!
        /// </summary>
        protected void Clear() 
        {
            this.txtName.Text = string.Empty;
            this.cmbDept.Tag = string.Empty;
            this.cmbDoct.Tag = string.Empty;
            this.txtInDate.Text = string.Empty;
            this.txtBedNO.Text = string.Empty;
            this.txtNurseStation.Text = string.Empty;
            this.txtLeftCost.Text = string.Empty;
            this.txtDoct.Text = string.Empty;

            this.patientInfo = null;

            this.ucInpatientCharge1.Clear();

            this.rbName.Checked = true;
            this.isInputName = true;
            this.txtName.Focus();
            this.ucQueryInpatientNO.Text = string.Empty;
        }

        /// <summary>
        /// ��Ʊ��ӡ
        /// </summary>
        /// <param name="alBalanceList">�����������</param>
        /// <param name="balanceForInvoice">������ʵ��</param>
        protected virtual void PrintInvoice(ArrayList alBalanceList,FS.HISFC.Models.Fee.Inpatient.Balance balanceForInvoice)
        {
            //Balance.ucBalanceInvoice balanceInvoice = new UFC.InpatientFee.Balance.ucBalanceInvoice();
            FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy balanceInvoice = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy)) as FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy;

            if (balanceInvoice == null)
            {
                return;
            }
            balanceInvoice.PatientInfo = this.patientInfo;
            //{D0D3A300-FD19-4fef-B763-FD5697274BBD}
            if (balanceInvoice.SetValueForPrint(this.patientInfo, balanceForInvoice, alBalanceList,null) == -1)
            {
                
                return ;
            }
            //����ӡ��
            balanceInvoice.Print();
        }

        #endregion

        #region ���з���

        /// <summary>
        /// �����շ���Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int Save() 
        {   
            if (!this.IsValid()) 
            {
                return -1;
            }

            //��ʼ���ݿ�����
            //Transaction t = new Transaction(this.inpatientManager.Connection);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.controlManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.pharmacyInterate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            

            //��������
            ArrayList feeItemlists = new ArrayList();//��ǰҪ�շ���Ŀ�б�
            string invoiceNO = string.Empty;//��Ʊ��
            DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();//��ǰϵͳʱ��
            int balanceNO = 0;//�������
            string recipeDoctCode = string.Empty;//����ҽ������
            int returnValue = 0;//����ֵ
            string errText = string.Empty;//������Ϣ
            recipeDoctCode = this.ucInpatientCharge1.RecipeDoctCode;

            //��÷�Ʊ��
            //{C3C5304F-2034-4fbd-A42C-EFE4F6EA6E8E}
            //invoiceNO = this.feeIntegrate.GetNewInvoiceNO(FS.HISFC.Models.Fee.EnumInvoiceType.I);
            invoiceNO = this.feeIntegrate.GetNewInvoiceNO("I");
            if (invoiceNO == null || invoiceNO.Trim() == string.Empty)
            {
                this.feeIntegrate.Rollback();
                MessageBox.Show(Language.Msg("����ȡסԺ���㷢Ʊ!") );
                
                return -1;
            }

            //��ý������
            balanceNO = this.GetBalanceNO();
            if (balanceNO == -1) 
            {
                this.feeIntegrate.Rollback();
                MessageBox.Show(Language.Msg("��ý�����ų���!") + this.inpatientManager.Err);

                return -1;
            }

            //������ֹ����������,�������µǼǻ��ߴ���
            if (this.isInputName) 
            {
                this.patientInfo.BalanceNO = balanceNO;

                if (this.InsertPatientInfo() == -1) 
                {
                    this.feeIntegrate.Rollback();
                    MessageBox.Show(Language.Msg("���뻼�߻�����Ϣ!") + this.radtIntegrate.Err);

                    return -1;
                }
            }

            //��õ�ǰ����Ҫ�շѵ���Ŀ��Ϣ,���Ҹ�ֵҽ��,��ǰʱ�����Ϣ
            feeItemlists = this.ucInpatientCharge1.QueryFeeItemArrayList(recipeDoctCode, nowTime, "0");
            if (feeItemlists == null) 
            {
                this.feeIntegrate.Rollback();
                MessageBox.Show(Language.Msg("����շ���ϸ����!"));

                return -1;
            }
            if (feeItemlists.Count == 0) 
            {
                this.feeIntegrate.Rollback();
                MessageBox.Show(Language.Msg("��¼���շ���ϸ!"));

                return -1;
            }

            //ѭ���Է�����ϸ��ֵ
            foreach (FeeItemList f in feeItemlists) 
            {
                f.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                f.Patient = this.patientInfo.Clone();
                f.PayType = FS.HISFC.Models.Base.PayTypes.Balanced;
                //f.ExecOper.ID = this.inpatientManager.Operator.ID;
                //f.ExecOper.OperTime = nowTime;
                f.FeeOper.ID = this.inpatientManager.Operator.ID;
                f.FeeOper.OperTime = nowTime;
                f.BalanceNO = balanceNO;
                f.BalanceState = "1";
                f.NoBackQty = f.Item.Qty;
                f.Invoice.ID = invoiceNO;
                f.RecipeOper.ID = recipeDoctCode;
                f.RecipeOper.Dept.ID = this.patientInfo.PVisit.PatientLocation.Dept.ID;
                f.StockOper.Dept.ID = f.ExecOper.Dept.ID;
                
            }

            //�����շѺ���
            this.feeIntegrate.MessageType = FS.HISFC.Models.Base.MessType.N;
            returnValue = this.feeIntegrate.FeeItem(this.patientInfo, ref feeItemlists);
            if (returnValue == -1) 
            {
                this.feeIntegrate.Rollback();
                MessageBox.Show(Language.Msg("�����շѺ�������!") + this.feeIntegrate.Err);

                return -1;
            }
          
            //���뷢ҩ����
            foreach (FeeItemList f in feeItemlists) 
            {
                //if (f.Item.IsPharmacy)
                if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {

                    returnValue = this.pharmacyInterate.ApplyOut(this.patientInfo, f, nowTime, true);
                    if (returnValue == -1)
                    {
                        this.feeIntegrate.Rollback();
                        MessageBox.Show(Language.Msg("���÷�ҩ���뺯������!") + this.pharmacyInterate.Err);

                        return -1;
                    }
                }
            }

            //���ɽ�����ϸ
            ArrayList balanceList = this.MakeBalanceListFromFeeItemList(invoiceNO, nowTime, balanceNO, feeItemlists, ref errText);
            if (balanceList == null) 
            {
                this.feeIntegrate.Rollback();
                MessageBox.Show(Language.Msg("���ɽ�����ϸ����!") + errText);

                return -1;
            }

            //���������ϸ��Ϣ
            returnValue = this.feeIntegrate.InsertBalanceList(this.patientInfo, balanceList);
            if (returnValue == -1) 
            {
                this.feeIntegrate.Rollback();
                MessageBox.Show(Language.Msg("���������ϸ����!") + this.feeIntegrate.Err);

                return -1;
            }

            //���ɽ���ͷ��Ϣ
            FS.HISFC.Models.Fee.Inpatient.Balance balance = this.MakeBalanceFromBalanceList(invoiceNO, nowTime, balanceNO, balanceList);
            if (balance == null) 
            {
                this.feeIntegrate.Rollback();
                MessageBox.Show(Language.Msg("���ɽ���ͷ����!"));

                return -1;
            }

            //�������ͷ��Ϣ
            returnValue = this.inpatientManager.InsertBalance(this.patientInfo, balance);
            if (returnValue == -1)
            {
                this.feeIntegrate.Rollback();
                MessageBox.Show(Language.Msg("�������ͷ�����!") + this.inpatientManager.Err);

                return -1;
            }

            balancePayControl = new FS.HISFC.Components.Common.Controls.ucBalancePay();

            balancePayControl.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            balancePayControl.IsShowButton = true;
            balancePayControl.Init();
            balancePayControl.ServiceType = FS.HISFC.Models.Base.ServiceTypes.I;
            balancePayControl.TotOwnCost = balance.FT.OwnCost;
            balancePayControl.RealCost = balance.FT.OwnCost;

            FS.FrameWork.WinForms.Classes.Function.PopShowControl(balancePayControl);

            if (!balancePayControl.IsCurrentChoose) 
            {
                this.feeIntegrate.Rollback();
                MessageBox.Show(Language.Msg("����ȷѡ��֧����ʽ!"));

                return -1;
            }

            ArrayList balancePayList = this.balancePayControl.QueryBalancePayList();
            if (balancePayList == null || balancePayList.Count == 0) 
            {
                this.feeIntegrate.Rollback();
                MessageBox.Show(Language.Msg("������ѡ��֧����ʽ!"));

                return -1;
            }

            foreach (FS.HISFC.Models.Fee.Inpatient.BalancePay balancePay in balancePayList)
            {
                balancePay.Invoice.ID = invoiceNO;
                balancePay.BalanceNO = balanceNO;
                balancePay.TransKind.ID = "1";
                balancePay.BalanceOper.ID = this.inpatientManager.Operator.ID;
                balancePay.BalanceOper.OperTime = nowTime;
                returnValue = this.inpatientManager.InsertBalancePay(balancePay);
                if (returnValue == -1)
                {
                    this.feeIntegrate.Rollback();
                    MessageBox.Show(Language.Msg("����֧����ʽʧ��!") + this.inpatientManager.Err);

                    return -1;
                }
            }

            returnValue = this.inpatientManager.UpdateInMainInfoBalanced(this.patientInfo,nowTime,balanceNO,balance.FT);
            if (returnValue != 1)
            {
                this.feeIntegrate.Rollback();
                MessageBox.Show(Language.Msg("����סԺ����ʧ��!") + this.inpatientManager.Err);

                return -1;
            }

            //������ֹ����������,�������µǼǻ��ߴ���
            if (this.isInputName)
            {
                FS.HISFC.Models.RADT.InStateEnumService status = new InStateEnumService();
                status.ID = FS.HISFC.Models.Base.EnumInState.O;
                //���»���״̬Ϊ��Ժ����״̬
                if (this.radtIntegrate.UpdatePatientState(this.patientInfo, status) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("���»���״̬��������!") + this.feeIntegrate.Err);

                    return -1;
                }
            }

            this.feeIntegrate.Commit();

            MessageBox.Show(Language.Msg("�շѳɹ�!"));

            this.PrintInvoice(balanceList, balance);

            this.Clear();
            
            return 1;   
        }

        #endregion

        #region �¼�

        /// <summary>
        /// סԺ�Żس�
        /// </summary>
        private void ucQueryInpatientNO_myEvent()
        {
            //{FF539371-A89F-4a21-911A-3F2FAE388EF0}
            this.ucInpatientCharge1.Clear();
            if ( this.ucQueryInpatientNO.InpatientNo == null || this.ucQueryInpatientNO.InpatientNo.Trim() == string.Empty)
            {
                MessageBox.Show(Language.Msg("û�и�סԺ��,����֤������") + this.ucQueryInpatientNO.Err);
                this.ucQueryInpatientNO.Focus();
                this.ucQueryInpatientNO.TextBox.SelectAll();

                return;
            }

            //��û��߻�����Ϣ
            this.patientInfo = this.radtIntegrate.GetPatientInfomation(this.ucQueryInpatientNO.InpatientNo);
            if (this.patientInfo == null) 
            {
                MessageBox.Show(Language.Msg("��û��߻�����Ϣ����!") + this.radtIntegrate.Err);
                this.ucQueryInpatientNO.Focus();
                this.ucQueryInpatientNO.TextBox.SelectAll();

                return;
            }

            //�ж���Ժ״̬�Ƿ����
            if (!this.IsPatientStateValid()) 
            {
                return;
            }

            //��ʾ���߻�����Ϣ
            this.SetInfomaition();

            this.ucInpatientCharge1.PatientInfo = this.patientInfo;
            
            this.cmbDoct.Focus();
        }

        /// <summary>
        /// ����ToolBar�ؼ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "���¼�����Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            toolBarService.AddToolButton("����", "�򿪰����ļ�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            toolBarService.AddToolButton("����", "����һ����Ŀ¼����", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            toolBarService.AddToolButton("ɾ��", "ɾ��һ��¼�����Ŀ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);

            return this.toolBarService;
        }

        /// <summary>
        /// �Զ��尴ť���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    this.Clear();
                    break;
                case "����":
                    this.ucInpatientCharge1.AddRow();
                    break;
                case "ɾ��":
                    this.ucInpatientCharge1.DelRow();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                //{FF539371-A89F-4a21-911A-3F2FAE388EF0}
                this.ucInpatientCharge1.Clear();
                this.patientInfo = new PatientInfo();
                
                this.GetPatientInfoFromInputName();

                this.ucInpatientCharge1.PatientInfo = this.patientInfo;
                this.cmbDept.Focus();
            }
        }

        private void ucDircFee_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode) 
            {
                this.rbName.Checked = true;
                this.txtName.Focus();
                
                this.Init();
            }
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.patientInfo != null)
            {
                if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != string.Empty)
                {
                    this.patientInfo.PVisit.PatientLocation.Dept.ID = this.cmbDept.Tag.ToString();
                    this.patientInfo.PVisit.PatientLocation.Dept.Name = this.cmbDept.Text.Trim();

                    this.patientInfo.PVisit.PatientLocation.NurseCell.ID = this.cmbDept.Tag.ToString();
                    this.patientInfo.PVisit.PatientLocation.NurseCell.Name = this.cmbDept.Text.ToString();
                }
            }
        }

        private void cmbDept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                this.cmbDoct.Focus();
            }
        }

        private void cmbDoct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.patientInfo != null)
            {
                if (this.cmbDoct.Tag != null && this.cmbDoct.Tag.ToString() != string.Empty)
                {
                    this.patientInfo.PVisit.AdmittingDoctor.ID = this.cmbDoct.Tag.ToString();
                    this.patientInfo.PVisit.AdmittingDoctor.Name = this.cmbDoct.Text.Trim();
                    this.ucInpatientCharge1.RecipeDoctCode = this.patientInfo.PVisit.AdmittingDoctor.ID;
                }
            }
        }

        private void cmbDoct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                this.ucInpatientCharge1.Focus();
            }
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            
            return base.OnSave(sender, neuObject);
        }

        private void rbInpatientNO_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbInpatientNO.Checked)
            {
                this.txtName.Enabled = false;
                this.isInputName = false;
                this.ucQueryInpatientNO.txtInputCode.Enabled = true;
                this.ucQueryInpatientNO.Focus();
                this.ucQueryInpatientNO.txtInputCode.Focus();
            }
        }

        private void rbName_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbName.Checked) 
            {
                this.Clear();
                this.txtName.Enabled = true;
                this.ucQueryInpatientNO.txtInputCode.Enabled = false;
                this.isInputName = true;
                this.txtName.Focus();
            }
        }
    }
}
