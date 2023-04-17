using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.SOC.Local.Order.OutPatientOrder.GYZL.PrePayIn
{
    public partial class ucPrepayInQuery : UserControl
    {
        public ucPrepayInQuery()
        {
            InitializeComponent();
        }


        #region ����
        //ԤԼ״̬
        private string prepayinState;
        DataTable dtPrepayIn = new DataTable();
        DataView dvPrepayIn;
        private FS.HISFC.Models.RADT.PatientInfo myPatientInfo;
        #endregion

        #region ҵ���ʵ��
        /// <summary>
        /// ���Һ�ͬ��λ����ʵ��
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper myObjHelper = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// ������Ա����ʵ��
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper operObjHelper = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// ����ʵ��
        /// </summary>
        //private FS.HISFC.BizLogic.Manager.Constant Constant = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// ��Աҵ���
        /// </summary>
        //private FS.HISFC.BizLogic.Manager.Person myPerson = new FS.HISFC.BizLogic.Manager.Person();

        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        FS.HISFC.BizLogic.Fee.InPatient inPatient = new FS.HISFC.BizLogic.Fee.InPatient();
        /// <summary>
        /// ��ͬ��λҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.PactUnitInfo pactUnitInfo = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        //private FS.HISFC.BizLogic.RADT.InPatient myInpatient = new FS.HISFC.BizLogic.RADT.InPatient();
        #endregion

        #region ����
        /// <summary>
        /// ԤԼ״̬ 0ԤԼ��1���� 2ԤԼתסԺ
        /// </summary>
        public string PrepayinState
        {
            get { return prepayinState; }
            set { prepayinState = value; }
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get
            {
                return myPatientInfo;
            }
            set
            {
                if (value == null)
                    myPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                else
                    myPatientInfo = value;
            }
        }
        #endregion    

        #region ��ʼ��
        private void InitQuery()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ������ݣ����Ժ�^^");
            Application.DoEvents();
            //��ʼ����ͬ��λ��Ϣ
            //��ͬ��λ{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
            //this.myObjHelper.ArrayObject = this.managerIntegrate.GetConstantList(EnumConstant.PACTUNIT);// Constant.GetList(EnumConstant.PACTUNIT);
            this.myObjHelper.ArrayObject = this.pactUnitInfo.QueryPactUnitAll();
            //��ʼ����Ա��Ϣ
            this.operObjHelper.ArrayObject = this.managerIntegrate.QueryEmployeeAll();// myPerson.GetEmployeeAll();
            //��ֹʱ��
            this.dtEnd.Value = inPatient.GetDateTimeFromSysDateTime();
            this.dtBegin.Value = this.dtEnd.Value.AddDays(-1);
            //��ʼ��DataTable
            SetDataTable();
            //��ѯ����
            QueryData();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        #endregion 

        #region ����
        /// <summary>
        /// ��ʼ��DataTable
        /// </summary>
        private void SetDataTable()
        {
            this.fpMainInfo_Sheet1.RowCount = 0;

            Type str = typeof(String);
            Type date = typeof(DateTime);

            Type dec = typeof(Decimal);
            Type bo = typeof(bool);
            #region ԤԼ�Ǽ��б�

            dtPrepayIn.Columns.AddRange(new DataColumn[]{new DataColumn("�������", str),
															new DataColumn("������", str),
															new DataColumn("��������", str),
															new DataColumn("�Ա�", str),
															new DataColumn("��ͬ��λ", str),
															new DataColumn("סԺ����", str),
															new DataColumn("ԤԼ����", str),
															new DataColumn("��ǰ״̬", str),															
															new DataColumn("��ͥ��ַ", str),
															new DataColumn("��ͥ�绰", str),
															new DataColumn("��ϵ��", str),
															new DataColumn("��ϵ�˵绰", str),
															new DataColumn("��ϵ�˵�ַ", str),
															new DataColumn("����Ա", str),
															new DataColumn("����ʱ��", str)});



            #endregion
        }

        /// <summary>
        /// ��ѯ����
        /// </summary>
        public void QueryData()
        {
            string Begin = this.dtBegin.Value.ToShortDateString() + " 00:00:00";
            string End = this.dtEnd.Value.ToShortDateString() + " 23:59:59";
            this.QueryData( Begin, End);
        }

        /// <summary>
        /// ����ԤԼ״̬��ʱ���������
        /// </summary>
        /// <param name="PrepayinState"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        private void QueryData( string begin, string end)
        {
            this.dtPrepayIn.Clear();
            try
            {
                ArrayList arrPrein = new ArrayList();
                
                //arrPrein = this.myInpatient.GetPreInPatientInfoByDateAndState(this.PrepayinState, begin, end);
                arrPrein = this.managerIntegrate.QueryPreInPatientInfoByDateAndState(this.PrepayinState, begin, end);
                string strName = "", strStateName = "";
                if (arrPrein == null)
                    return;
                foreach (FS.HISFC.Models.RADT.PatientInfo obj in arrPrein)
                {
                    #region��ȡ�Ա�����
                    switch (obj.Sex.ID.ToString())
                    {
                        case "U":
                            strName = "δ֪";
                            break;
                        case "M":
                            strName = "��";
                            break;
                        case "F":
                            strName = "Ů";
                            break;
                        case "O":
                            strName = "����";
                            break;
                        default:
                            break;
                    }
                    #endregion

                    #region �Ǽ�״̬
                    switch (obj.User02.ToString())
                    {
                        case "0":
                            strStateName = "ԤԼ�Ǽ�";
                            break;
                        case "1":
                            strStateName = "ȡ��ԤԼ�Ǽ�";
                            break;
                        case "2":
                            strStateName = "ԤԼתסԺ";
                            break;
                        default:
                            break;
                    }
                    #endregion

                    #region ȡ��ͬ��λ������Ա����
                    obj.Pact.Name = this.myObjHelper.GetName(obj.Pact.ID);
                    string strOperID = obj.User03.Substring(0, 6);
                    string strOperName = this.operObjHelper.GetName(strOperID);
                    #endregion

                    #region ��DataTable��������
                    DataRow row = this.dtPrepayIn.NewRow();
                    row["�������"] = obj.User01;
                    row["������"] = obj.PID.CardNO;
                    row["��������"] = obj.Name;
                    row["�Ա�"] = strName;
                    row["��ͬ��λ"] = obj.Pact.Name;//��ת��
                    row["סԺ����"] = obj.PVisit.PatientLocation.Dept.Name;
                    row["ԤԼ����"] = obj.PVisit.InTime;//.Date_In;
                    row["��ǰ״̬"] = strStateName;//��ת��
                    row["��ͥ��ַ"] = obj.AddressHome;
                    row["��ͥ�绰"] = obj.PhoneHome;
                    row["��ϵ��"] = obj.Kin.ID;
                    row["��ϵ�˵绰"] = obj.Kin.Memo;
                    row["��ϵ�˵�ַ"] = obj.Kin.User01;
                    row["����Ա"] = strOperName;
                    row["����ʱ��"] = obj.User03.Substring(6, 10);

                    this.dtPrepayIn.Rows.Add(row);
                    #endregion

                    
                }

                dvPrepayIn = new DataView(this.dtPrepayIn);
                this.fpMainInfo_Sheet1.DataSource = this.dvPrepayIn;
                this.initFp();

            }
            catch { }
        }

        /// <summary>
        /// ����fp���
        /// </summary>
        private void initFp()
        {
            try
            {
                int im = 3;
                this.fpMainInfo_Sheet1.OperationMode = (FarPoint.Win.Spread.OperationMode)im;
                this.fpMainInfo_Sheet1.Columns.Get(0).Width = 0F;
                this.fpMainInfo_Sheet1.Columns.Get(1).Width = 80F;
                this.fpMainInfo_Sheet1.Columns.Get(2).Width = 72F;
                this.fpMainInfo_Sheet1.Columns.Get(3).Width = 48F;
                this.fpMainInfo_Sheet1.Columns.Get(5).Width = 88F;
                this.fpMainInfo_Sheet1.Columns.Get(6).Width = 100F;
                this.fpMainInfo_Sheet1.Columns.Get(9).Width = 95F;
                this.fpMainInfo_Sheet1.Columns.Get(10).Width = 102F;
                this.fpMainInfo_Sheet1.Columns.Get(11).Width = 127F;
                this.fpMainInfo_Sheet1.Columns.Get(12).Width = 85F;
                //			this.fpMainInfo_Sheet1.Columns.Get(13).Width = 80F;
                this.fpMainInfo_Sheet1.Columns.Get(14).Width = 85F;
            }
            catch { }
        }

        /// <summary>
        /// ���ݷ�����Ż��ʵ��
        /// </summary>
        /// <param name="strNo"></param>
        /// <param name="strCardNo"></param>
        private void setPatient(string strNo, string strCardNo)
        {
            this.myPatientInfo = this.managerIntegrate.QueryPreInPatientInfoByCardNO(strNo, strCardNo);// this.myInpatient.GetPreInPatientInfoByCardNO(strNo, strCardNo);
            //this.myPatientInfo.InTimes = this.PatientInfo.InTimes + 1;
        }

        private void GetPrePayData()
        {
            try
            {
                if (this.fpMainInfo_Sheet1.Rows.Count == 0) return;
                int iRow = this.fpMainInfo_Sheet1.ActiveRowIndex;
                //��ȡ�������
                string strNo = this.fpMainInfo_Sheet1.Cells[iRow, 0].Text.Trim();
                string strCardNo = this.fpMainInfo_Sheet1.Cells[iRow, 1].Text.Trim();
                //���ԤԼ�Ǽ�ʵ�巵�ظ�����
                this.setPatient(strNo, strCardNo);
                this.FindForm().DialogResult = DialogResult.OK;
            }
            catch
            {
                MessageBox.Show("ѡȡԤԼ����ʧ�ܣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        #endregion 

        #region �¼�
        private void ucPrepayInQuery_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                this.InitQuery();
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.QueryData();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.GetPrePayData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = DialogResult.Cancel;
        }

        private void fpMainInfo_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.GetPrePayData();
        }

        #endregion

    }
}
