using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FarPoint.Win.Spread;
namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    /// <summary>
    /// ucDiagNoseInput<br></br>
    /// [��������: �������¼��]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-04-20]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucMetCasDiagnose : UserControl
    {
        public ucMetCasDiagnose()
        {
            InitializeComponent();
        }

        #region  ȫ�ֱ���
        //������
        private ArrayList diagnoseType = new ArrayList();
        private FS.FrameWork.Public.ObjectHelper diagnoseTypeHelper = new FS.FrameWork.Public.ObjectHelper();
        //�����б�
        private ArrayList PeriorList = new ArrayList();
        private FS.FrameWork.Public.ObjectHelper PeriorListHelper = new FS.FrameWork.Public.ObjectHelper();
        //�ּ��б�
        private ArrayList LeveList = new ArrayList();
        private FS.FrameWork.Public.ObjectHelper LeveListHelper = new FS.FrameWork.Public.ObjectHelper();
        //��Ժ���� �б�
        private ArrayList diagOutStateList = new ArrayList();
        private FS.FrameWork.Public.ObjectHelper diagOutStateListHelper = new FS.FrameWork.Public.ObjectHelper();
        //�����ļ���·�� 
        private string filePath = Application.StartupPath + "\\profile\\ucDiagNoseInput.xml";
        private DataTable dtDiagnose = new DataTable("�����Ϣ��");
        private FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();
        //���� �������� 
        private ArrayList OperList = new ArrayList();
        private FS.FrameWork.Public.ObjectHelper OperListHelper = new FS.FrameWork.Public.ObjectHelper();
        //�����Ϣ
        public ArrayList diagList = null;
        //��ʶ��ҽ��վ ���� ������
        private FS.HISFC.Models.HealthRecord.EnumServer.frmTypes frmType;

        #region  �е�ȫ�ֱ���
        private enum Col
        {
            DiagKind = 0, //������
            Icd10Code = 1, //ICD10 ���� 
            Icd10Name = 2,//ICD10 ����
            OutState = 3, //��Ժ����
            OperationFlag = 4, //����
            Disease = 5, //30�ּ���
            CLPa = 6,//�������
            Perior = 7,//����
            Level = 8,//�ּ�
            DubDiag = 9,//�Ƿ�����
            MainDiag = 10,//�����
            //{74A9AA46-74B3-49e8-910A-54A998E428AF} �������﹦��
            IsDraftExamine = 17, //�Ƿ�����
            ICD=18,//ICD����
            IcdName=19, //ICD����
            //ICDFCode=20,//ICD��������
            //ICDFName=21 //ICD������������

        }

        #endregion
        #endregion

        #region ����
        //{8BC09475-C1D9-4765-918B-299E21E04C74} ���¼������ҽ��վ������ҽ��վ������������
        /// <summary>
        /// �����һ���ҽ��վʹ��  true ҽ��վ  false ������ 
        /// </summary>
        private bool isCas = true;
        /// <summary>
        /// �����һ���ҽ��վʹ��  true ҽ��վ  false ������
        /// </summary>
        public bool IsCas
        {
            get
            {
                return isCas;
            }
            set
            {
                isCas = value;
            }
        }
        /// <summary>
        /// �Ƿ�����  true ���� false ������ chengym
        /// �÷������� ֱ��¼�����������  �����ٱ༭ icd10  ������ ֱ��¼�� ICD10
        /// </summary>
        private bool isDoubt = true;
        /// <summary>
        /// �������� true ���� false ������
        /// </summary>
        public bool IsDoubt
        {
            get { return this.isDoubt; }
            set { this.isDoubt = value; }
        }
        #region {6EF7D73B-4350-4790-B98C-C0BD0098516E}
        /// <summary>
        /// ���ҳ�����ϱ�־
        /// </summary>
        private bool isUseDeptICD = false;

        /// <summary>
        /// ���ҳ�����ϱ�־
        /// </summary>
        [Category("���ҳ������"), Description("�Ƿ���ʹ�ÿ��ҳ������")]
        public bool IsUseDeptICD
        {
            get
            {
                return isUseDeptICD;
            }
            set
            {
                isUseDeptICD = value;
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// ������Ϣ
        /// </summary>
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
            }
        }

        /// <summary>
        /// ����һ���հ��� 
        /// </summary>
        /// <returns></returns>
        public void AddBlankRow()
        {
            this.fpEnter1_Sheet1.Rows.Add(this.fpEnter1_Sheet1.RowCount, 1);
            this.fpEnter1.Focus();
            this.fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.RowCount-1, 0);

        }
        /// <summary>
        /// Ժ�ڸ�Ⱦ���� 
        /// </summary>
        /// <returns></returns>
        public int GetInfectionNum()
        {
            int j = 0;
            if (fpEnter1_Sheet1.RowCount == 0)
            {
                return 0;
            }
            string strName = diagnoseTypeHelper.GetName("4");
            for (int i = 0; i < fpEnter1_Sheet1.RowCount; i++)
            {
                if (fpEnter1_Sheet1.Cells[i, 0].Text == strName)
                {
                    j++;
                }
            }
            return j;
        }
        /// <summary>
        /// �Ƿ��в���֢
        /// </summary>
        /// <returns></returns>
        public string GetSyndromeFlag()
        {
            string str = "0";
            if (fpEnter1_Sheet1.RowCount == 0)
            {
                return "0";
            }
            for (int i = 0; i < fpEnter1_Sheet1.RowCount; i++)
            {
                //if (fpEnter1_Sheet1.Cells[i, 0].Text == str)
                if(fpEnter1_Sheet1.Cells[i,0].Text=="����֢���")
                {
                    str = "1";
                    break;
                }
            }
            return str;
        }
        /// <summary>
        /// ���û��Ԫ��
        /// </summary>
        public void SetActiveCells()
        {
            try
            {
                this.fpEnter1_Sheet1.SetActiveCell(0, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// ���ԭ�е�����
        /// </summary>
        /// <returns></returns>
        public int ClearInfo()
        {
            if (this.dtDiagnose != null)
            {
                this.dtDiagnose.Clear();
                LockFpEnter();
            }
            else
            {
                MessageBox.Show("��ϱ�Ϊnull");
            }
            return 1;
        }
        /// <summary>
        /// ��ȡ���е������Ϣ
        /// </summary>
        /// <returns></returns>
        public int GetAllDiagnose(ArrayList list)
        {
            //{691E10E6-4AB5-4252-82AD-4552DB079F2F}
            this.fpEnter1.StopCellEditing();
            foreach (DataRow dr in dtDiagnose.Rows)
            {
                dr.EndEdit();
            }
            DataTable tempdt = dtDiagnose.Copy();
            tempdt.AcceptChanges();
            GetChangeInfo(tempdt, list);
            return 1;
        }
        /// <summary>
        /// ���ñ༭ģʽ
        /// </summary>
        /// <param name="type">����boolֵ</param>
        /// <param name="editType">�༭���ڵ�����</param>
        /// <returns></returns>
        public int SetReadOnly(bool type ,FS.HISFC.Models.HealthRecord.EnumServer.frmTypes editType)
        {
            if (type)
            {
                this.btAdd.Enabled = !type;
                this.btDelete.Enabled = !type;
                if (editType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
                {
                    this.fpEnter1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.Normal;
                    this.fpEnter1_Sheet1.Columns[0].Locked = true; //������
                    this.fpEnter1_Sheet1.Columns[1].Locked = true;
                    this.fpEnter1_Sheet1.Columns[2].Locked = true;
                    this.fpEnter1_Sheet1.Columns[3].Locked = true; //��Ժ����
                    this.fpEnter1_Sheet1.Columns[4].Locked = true; //��������
                    this.fpEnter1_Sheet1.Columns[5].Locked = true; //30�ּ���
                    this.fpEnter1_Sheet1.Columns[6].Locked = true; //�������
                    this.fpEnter1_Sheet1.Columns[7].Locked = true; //����
                    this.fpEnter1_Sheet1.Columns[8].Locked = true; //�ּ�
                    this.fpEnter1_Sheet1.Columns[9].Locked = true; //�Ƿ�����
                }
                else
                {
                    this.fpEnter1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
                }
            }
            else
            {
                this.btAdd.Enabled = !type;
                this.btDelete.Enabled = !type;
                this.fpEnter1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.Normal;
            }
            return 0;
        }
        /// <summary>
        /// У�����ݵĺϷ��ԡ�
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int ValueStateNew(List<FS.HISFC.Models.HealthRecord.Diagnose> list)
        {
            if (list == null)
            {
                return -2;
            }
            foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in list)
            {
                if (obj.DiagInfo.Patient.ID == "" || obj.DiagInfo.Patient.ID == null)
                {
                    MessageBox.Show("�����Ϣ��סԺ��ˮ�Ų���Ϊ��");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.Patient.ID,14))
                {
                    MessageBox.Show("�����Ϣ��סԺ��ˮ�Ź���");
                    return -1;
                }
                if (obj.DiagInfo.HappenNo > 999999999)
                {
                    MessageBox.Show("�����Ϣ�ķ�����Ź���");
                    return -1;
                }
                if (obj.DiagInfo.DiagType.ID == "" || obj.DiagInfo.DiagType.ID == null)
                {
                    MessageBox.Show("�����Ϣ��������Ͳ���Ϊ��");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.DiagType.ID, 2))
                {
                    MessageBox.Show("�����Ϣ��������ͱ������");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.LevelCode, 20))
                {
                    MessageBox.Show("�����Ϣ����ϼ���������");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.PeriorCode,20))
                {
                    MessageBox.Show("�����Ϣ����Ϸ��ڱ������");
                    return -1;
                }
                //{74A9AA46-74B3-49e8-910A-54A998E428AF} �������﹦��
                if (obj.Pvisit.Memo == "0")
                {
                    if (obj.DiagInfo.ICD10.ID.Trim() == "" || obj.DiagInfo.ICD10.ID.Trim() == null)
                    {
                        MessageBox.Show("�����Ϣ��ICD��ϲ���Ϊ��");
                        return -1;
                    }
                    if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.ICD10.ID, 50))
                    {
                        MessageBox.Show("�����Ϣ����ϱ������");
                        return -1;
                    }
                }
                if (obj.DiagInfo.ICD10.Name == "" || obj.DiagInfo.ICD10.Name == null)
                {
                    MessageBox.Show("�����Ϣ��ICD��ϲ���Ϊ��");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.ICD10.Name, 100))
                {
                    MessageBox.Show("�����Ϣ��������ƹ���");
                    return -1;
                }
                if (obj.DiagInfo.Doctor.ID == "" || obj.DiagInfo.Doctor.ID == null)
                {
                    MessageBox.Show("�����Ϣ�����ҽ�����벻��Ϊ��");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.Doctor.ID, 6))
                {
                    MessageBox.Show("�����Ϣ��ҽ���������");
                    return -1;
                }
                if (obj.DiagInfo.Doctor.Name == "" || obj.DiagInfo.Doctor.Name == null)
                {
                    MessageBox.Show("�����Ϣ�����ҽ������Ϊ��");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.Doctor.Name,10))
                {
                    MessageBox.Show("�����Ϣ��ҽ�����ƹ���");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagOutState, 20))
                {
                    MessageBox.Show("�����Ϣ����Ժ����������");
                    return -1;
                }
                if (obj.DiagInfo.DiagType.ID == "1" && obj.DiagOutState == "")
                {
                    MessageBox.Show("��ѡ����Ҫ��ϵ���Ժ���飡");
                    return -1;
                }
                if (obj.OperType.Length > 1)
                {
                    MessageBox.Show("�����Ϣ�����������");
                    return -1;
                }
            }
            return 0;
        }

        /// <summary>
        /// У�����ݵĺϷ��ԡ�
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int ValueState(ArrayList list)
        {
            if (list == null)
            {
                return -2;
            }
            foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in list)
            {
                if (obj.DiagInfo.Patient.ID == "" || obj.DiagInfo.Patient.ID == null)
                {
                    MessageBox.Show("�����Ϣ��סԺ��ˮ�Ų���Ϊ��");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.Patient.ID, 14))
                {
                    MessageBox.Show("�����Ϣ��סԺ��ˮ�Ź���");
                    return -1;
                }
                if (obj.DiagInfo.HappenNo > 999999999)
                {
                    MessageBox.Show("�����Ϣ�ķ�����Ź���");
                    return -1;
                }
                if (obj.DiagInfo.DiagType.ID == "" || obj.DiagInfo.DiagType.ID == null)
                {
                    MessageBox.Show("�����Ϣ��������Ͳ���Ϊ��");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.DiagType.ID, 2))
                {
                    MessageBox.Show("�����Ϣ��������ͱ������");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.LevelCode, 20))
                {
                    MessageBox.Show("�����Ϣ����ϼ���������");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.PeriorCode, 20))
                {
                    MessageBox.Show("�����Ϣ����Ϸ��ڱ������");
                    return -1;
                }
                //{74A9AA46-74B3-49e8-910A-54A998E428AF} �������﹦��
                if (obj.Pvisit.Memo == "0")
                {
                    if (obj.DiagInfo.ICD10.ID.Trim() == "" || obj.DiagInfo.ICD10.ID.Trim() == null)
                    {
                        MessageBox.Show("�����Ϣ��ICD��ϲ���Ϊ��");
                        return -1;
                    }
                    if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.ICD10.ID, 50))
                    {
                        MessageBox.Show("�����Ϣ����ϱ������");
                        return -1;
                    }
                }
                if (obj.DiagInfo.ICD10.Name == "" || obj.DiagInfo.ICD10.Name == null)
                {
                    MessageBox.Show("�����Ϣ��ICD��ϲ���Ϊ��");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.ICD10.Name, 100))
                {
                    MessageBox.Show("�����Ϣ��������ƹ���");
                    return -1;
                }
                if (obj.DiagInfo.Doctor.ID == "" || obj.DiagInfo.Doctor.ID == null)
                {
                    MessageBox.Show("�����Ϣ�����ҽ�����벻��Ϊ��");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.Doctor.ID, 6))
                {
                    MessageBox.Show("�����Ϣ��ҽ���������");
                    return -1;
                }
                if (obj.DiagInfo.Doctor.Name == "" || obj.DiagInfo.Doctor.Name == null)
                {
                    MessageBox.Show("�����Ϣ�����ҽ������Ϊ��");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.Doctor.Name, 10))
                {
                    MessageBox.Show("�����Ϣ��ҽ�����ƹ���");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagOutState, 20))
                {
                    MessageBox.Show("�����Ϣ����Ժ����������");
                    return -1;
                }
                if (obj.DiagInfo.DiagType.ID == "1" && obj.DiagOutState == "")
                {
                    MessageBox.Show("��ѡ����Ҫ��ϵ���Ժ���飡");
                    return -1;
                }
                if (obj.OperType.Length > 1)
                {
                    MessageBox.Show("�����Ϣ�����������");
                    return -1;
                }
            }
            return 0;
        }

        /// <summary>
        /// ����Ա����������޸�
        /// </summary>
        /// <returns></returns>
        public int fpEnterSaveChanges()
        {
            try
            {
                this.dtDiagnose.AcceptChanges();
                LockFpEnter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// ���ص�ǰ����
        /// </summary>
        /// <returns></returns>
        public int GetfpSpreadRowCount()
        {
            return fpEnter1_Sheet1.Rows.Count;
        }
        /// <summary>
        /// ���reset Ϊ�� ������������� ���������  Ϊ�� ֻ�Ǳ��浱ǰ����
        /// creator:zhangjunyi@FS.com
        /// </summary>
        /// <param name="reset"></param>
        /// <returns></returns>
        public bool Reset(bool reset)
        {
            if (reset)
            {
                //������� ������� 
                if (dtDiagnose != null)
                {
                    dtDiagnose.Clear();
                    dtDiagnose.AcceptChanges();
                }
            }
            else
            {
                //�������
                dtDiagnose.AcceptChanges();
            }
            LockFpEnter();
            return true;
        }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void InitInfo()
        {
            try
            {
                #region {6EF7D73B-4350-4790-B98C-C0BD0098516E}
                if (!this.DesignMode)
                {
                    ////this.ucDiagnose1.IsUseDeptICD = this.isUseDeptICD;
                    //this.ucDiagnose1.Init();
                    this.ucDiagnose1.InitCase();
                }
                #endregion
                //��ʼ����
                InitDateTable();
                //���������б�
                this.initList();
                //InputMap im;
                //im = fpEnter1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                //im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                //im = fpEnter1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                //im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                //im = fpEnter1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                //im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
 
                fpEnter1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
                LockFpEnter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// ���������סԺ������Ϣ ��type������ѯ�����Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="Type"></param>
        /// <returns>-1 ���� 0 ����Ĳ�����ϢΪ��,��������1 �������в�����2�����Ѿ���棬������ҽ���޸ĺͲ��� 3 ��ѯ������ 4��ѯû������  </returns>
        public int LoadInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes Type)
        {
            try
            {
                this.rtxtDiag.Text = "";//add chengym
                frmType = Type;
                if (patientInfo == null)
                {
                    //û�иò��˵���Ϣ
                    return 0;
                }

                patient = patientInfo;
                if (patientInfo.CaseState == "0")
                {
                    //�������в���
                    return 1;
                }
                //����ҵ������
                FS.HISFC.BizLogic.HealthRecord.Diagnose diag = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
                diagList = new ArrayList();

                if (Type == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC) // ҽ��վ¼�벡��
                {
                    #region  ҽ��վ¼�벡��

                    //add by chengym 2011-9-27 
                    this.DiagnoseLoadInfo(patientInfo.ID, Type);

                    //Ŀǰ�����в��� ����Ŀǰû��¼�벡��  ���߱�־λλ�գ�Ĭ��������¼�벡���� 
                    // ҽ��վ¼�벡��
                    if (patientInfo.CaseState == "1" || patientInfo.CaseState == "2")
                    {
                        //��ҽ��վ¼�����Ϣ�в�ѯ
                        diagList = diag.QueryCaseDiagnose(patientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC,FS.HISFC.Models.Base.ServiceTypes.I);
                    }
                    else
                    {
                        // �����Ѿ�����Ѿ�������ҽ���޸ĺͲ���
                        return 2;
                    }

                    #endregion
                }
                else if (Type == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)//������¼�벡��
                {
                    #region ������¼�벡��

                    ArrayList alTempDocDiagnose = new ArrayList();
                    ArrayList alTempCasDiagnose = new ArrayList();
                    //Ŀǰ�����в��� ����Ŀǰû��¼�벡��  ���߱�־λλ�գ�Ĭ��������¼�벡���� 
                    if (patientInfo.CaseState == "1" || patientInfo.CaseState == "2" || patientInfo.CaseState == "3")
                    {
                        //ҽ��վ�Ѿ�¼�벡��
                        alTempDocDiagnose = diag.QueryCaseDiagnose( patientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC,FS.HISFC.Models.Base.ServiceTypes.I );

                        //�������Ѿ�¼�벡��
                        alTempCasDiagnose = diag.QueryCaseDiagnose( patientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS,FS.HISFC.Models.Base.ServiceTypes.I );

                        if (alTempDocDiagnose != null && alTempDocDiagnose.Count > 0)
                        {
                            diagList.AddRange( alTempDocDiagnose );
                        }
                        if (alTempCasDiagnose != null && alTempCasDiagnose.Count > 0)
                        {
                            diagList.AddRange( alTempCasDiagnose );
                        }
                    }
                    else
                    {
                        //�����Ѿ���� �������޸ġ�
                        diagList = diag.QueryCaseDiagnose( patientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS,FS.HISFC.Models.Base.ServiceTypes.I );
                        this.fpEnter1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
                    }                 

                    #endregion
                }
                else
                {
                    //û�д������ �����κδ���
                }

                if (diagList != null)
                {
                    //��ѯ������
                    AddInfoToTable(diagList);
                    return 3;
                }
                else
                {//��ѯû������
                    return 4;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        //{8BC09475-C1D9-4765-918B-299E21E04C74} ���¼������ҽ��վ������ҽ��վ������������
        /// <summary>
        /// ���������סԺ������Ϣ ��type������ѯ�����Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="Type"></param>
        /// <returns>-1 ���� 0 ����Ĳ�����ϢΪ��,��������1 �������в�����2�����Ѿ���棬������ҽ���޸ĺͲ��� 3 ��ѯ������ 4��ѯû������  </returns>
        public int LoadInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes Type, string diagInputType)
        {
           return this.LoadInfo(patientInfo, Type);
            #region  ����ԭ��û�б�Ҫ����ֿ����� 2012-1-10 chengym
            //try
            //{
            //    frmType = Type;
            //    if (patientInfo == null)
            //    {
            //        //û�иò��˵���Ϣ
            //        return 0;
            //    }

            //    patient = patientInfo;
            //    if (patientInfo.CaseState == "0")
            //    {
            //        //�������в���
            //        return 1;
            //    }
            //    //����ҵ������
            //    FS.HISFC.BizLogic.HealthRecord.Diagnose diag = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            //    diagList = new ArrayList();

            //    if (Type == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC) // ҽ��վ¼�벡��
            //    {
            //        if (diagInputType == "cas")
            //        {
            //            #region  ҽ��վ¼�벡��

            //            //Ŀǰ�����в��� ����Ŀǰû��¼�벡��  ���߱�־λλ�գ�Ĭ��������¼�벡���� 
            //            // ҽ��վ¼�벡��
            //            if (patientInfo.CaseState == "1" || patientInfo.CaseState == "2")
            //            {
            //                //��ҽ��վ¼�����Ϣ�в�ѯ
            //                diagList = diag.QueryCaseDiagnose(patientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC,FS.HISFC.Models.Base.ServiceTypes.I);
            //            }
            //            else
            //            {
            //                // �����Ѿ�����Ѿ�������ҽ���޸ĺͲ���
            //                return 2;
            //            }

            //            #endregion
            //        }
            //        else
            //        {
            //            diagList = diag.QueryDiagnoseNoOps(patientInfo.ID);

            //        }
            //    }
            //    else if (Type == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)//������¼�벡��
            //    {
            //        #region ������¼�벡��
            //        //Ŀǰ�����в��� ����Ŀǰû��¼�벡��  ���߱�־λλ�գ�Ĭ��������¼�벡���� 
            //        if (patientInfo.CaseState == "1" || patientInfo.CaseState == "2")
            //        {
            //            //ҽ��վ�Ѿ�¼�벡��
            //            diagList = diag.QueryCaseDiagnose(patientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC,FS.HISFC.Models.Base.ServiceTypes.I);
            //        }
            //        else if (patientInfo.CaseState == "3")
            //        {
            //            //�������Ѿ�¼�벡��
            //            diagList = diag.QueryCaseDiagnose(patientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS,FS.HISFC.Models.Base.ServiceTypes.I);
            //        }
            //        else
            //        {
            //            //�����Ѿ���� �������޸ġ�
            //            diagList = diag.QueryCaseDiagnose(patientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS,FS.HISFC.Models.Base.ServiceTypes.I);
            //            this.fpEnter1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            //        }

            //        #endregion
            //    }
            //    else
            //    {
            //        //û�д������ �����κδ���
            //    }

            //    if (diagList != null)
            //    {
            //        //��ѯ������
            //        AddInfoToTable(diagList);

            //        //for (int i = 0; i < diagList.Count; i++)
            //        //{
            //        //    FS.HISFC.Models.HealthRecord.Diagnose diagInfo = diagList[i] as FS.HISFC.Models.HealthRecord.Diagnose;
            //        //    if (diagInfo.IsDraftExamine == "1") //�ж��Ƿ�����
            //        //    {
            //        //        this.fpEnter1_Sheet1.Cells[i, (int)Col.Icd10Code].Locked = true;
            //        //        this.fpEnter1_Sheet1.Cells[i, (int)Col.Icd10Name].Locked = false;

            //        //    }
            //        //    else
            //        //    {
            //        //        this.fpEnter1_Sheet1.Cells[i, (int)Col.Icd10Code].Locked = false;
            //        //        this.fpEnter1_Sheet1.Cells[i, (int)Col.Icd10Name].Locked = true;

            //        //    }
            //        //}
            //        this.dtDiagnose.AcceptChanges();

            //        return 3;
            //    }
            //    else
            //    {//��ѯû������
            //        return 4;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    return -1;
            //}
            #endregion 
        }
        public bool GetList(string strType, ArrayList list)
        {
            try
            {
                this.fpEnter1.StopCellEditing();
                //this.fpEnter1.EditModePermanent = false;
                //this.fpEnter1.EditModeReplace = false;
                foreach (DataRow dr in this.dtDiagnose.Rows)
                {
                    dr.EndEdit();
                }
                switch (strType)
                {
                    case "A":
                        //���ӵ�����
                        DataTable AddTable = this.dtDiagnose.GetChanges(DataRowState.Added);
                        GetChangeInfo(AddTable, list);
                        break;
                    case "M":
                        DataTable ModTable = this.dtDiagnose.GetChanges(DataRowState.Modified);
                        GetChangeInfo(ModTable, list);
                        break;
                    case "D":
                        DataTable DelTable = this.dtDiagnose.GetChanges(DataRowState.Deleted);
                        if (DelTable != null)
                        {
                            DelTable.RejectChanges();
                        }
                        GetChangeInfo(DelTable, list);
                        break;
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
        /// ɾ����ǰ�� 
        /// </summary>
        /// <returns></returns>
        public int DeleteActiveRow()
        {
            this.fpEnter1.SetAllListBoxUnvisible();
            this.fpEnter1.EditModePermanent = false;
            this.fpEnter1.EditModeReplace = false;
            if (fpEnter1_Sheet1.Rows.Count > 0)
            {
                this.fpEnter1_Sheet1.Rows.Remove(fpEnter1_Sheet1.ActiveRowIndex, 1);

                //{3C71EDBD-8179-41e6-98DB-B70CC6E01D61}
                if (this.ucDiagnose1.Visible) 
                {
                    this.ucDiagnose1.Visible = false;
                }
            }
            if (fpEnter1_Sheet1.Rows.Count == 0)
            {
                this.fpEnter1.SetAllListBoxUnvisible();
                #region {3C71EDBD-8179-41e6-98DB-B70CC6E01D61}
                this.ucDiagnose1.Visible = false;
                #endregion
            } 
            this.fpEnter1.EditModePermanent = true;
            this.fpEnter1.EditModeReplace = true;
            return 1;
        }
        /// <summary>
        /// ɾ���հ׵���
        /// </summary>
        /// <returns></returns>
        public int deleteRow()
        {
            this.fpEnter1.SetAllListBoxUnvisible();
            this.fpEnter1.EditModePermanent = false;
            this.fpEnter1.EditModeReplace = false;
            if (fpEnter1_Sheet1.Rows.Count == 1)
            {
                //��һ�г�Ժ�������Ϊ�� 
                if (fpEnter1_Sheet1.Cells[0, 2].Text == "")
                {
                    fpEnter1_Sheet1.Rows.Remove(0, 1);
                }
            }
            #region {3C71EDBD-8179-41e6-98DB-B70CC6E01D61}
            if (fpEnter1_Sheet1.Rows.Count == 0)
            {
                this.fpEnter1.SetAllListBoxUnvisible();
                
                this.ucDiagnose1.Visible = false;
            }
            #endregion
            this.fpEnter1.EditModePermanent = true;
            this.fpEnter1.EditModeReplace = true;
            return 1;
        }
        /// <summary>
        /// ��ȡ�޸Ĺ�����Ϣ
        /// </summary>
        /// <returns></returns>
        private bool GetChangeInfo(DataTable tempTable, ArrayList list)
        {
            if (tempTable == null)
            {
                return true;
            }
            try
            {
                FS.HISFC.Models.HealthRecord.Diagnose info = null;
                foreach (DataRow row in tempTable.Rows)
                {
                    info = new FS.HISFC.Models.HealthRecord.Diagnose();
                    info.DiagInfo.Patient.ID = this.patient.ID;
                    //{8BC09475-C1D9-4765-918B-299E21E04C74} ���¼������ҽ��վ������ҽ��վ������������
                    info.DiagInfo.Patient.PID.CardNO = this.patient.PID.CardNO;
                    //������
                    info.DiagInfo.DiagType.ID = diagnoseTypeHelper.GetID(row["������"].ToString());
                    //��Ժ��ϵ���ϸ���
                    //					info.MainFlag = diagnoseTypeHelper.GetID(row["������"].ToString());
                    info.DiagInfo.ICD10.ID = row["ICD10"].ToString();//2
                    if (info.DiagInfo.DiagType.ID == "1") //����������ó� 
                    {
                        info.DiagInfo.IsMain = true;
                    }
                    else
                    {
                        info.DiagInfo.IsMain = false;
                    }
                    info.DiagInfo.ICD10.Name = row["�������"].ToString();
                    if (row["��Ժ����"] != DBNull.Value)
                    {
                        info.DiagOutState = diagOutStateListHelper.GetID(row["��Ժ����"].ToString()); //3
                    }
                    if (row["��������"] != DBNull.Value)
                    {
                        info.OperationFlag = OperListHelper.GetID(row["��������"].ToString());
                    }

                    if (ConvertBool(row["30�ּ���"]))//5
                    {
                        info.Is30Disease = "1";
                    }
                    else
                    {
                        info.Is30Disease = "0";
                    }
                    if (ConvertBool(row["�������"]))//6
                    {
                        info.CLPA = "1";
                    }
                    else
                    {
                        info.CLPA = "0";
                    }
                    if (row["�ּ�"] != DBNull.Value)
                    {
                        info.LevelCode = LeveListHelper.GetID(row["�ּ�"].ToString()); //7
                    }
                    if (row["����"] != DBNull.Value)
                    {
                        info.PeriorCode = PeriorListHelper.GetID(row["����"].ToString());//8
                    }
                    if (ConvertBool(row["�Ƿ�����"]))//9
                    {
                        info.DubDiagFlag = "1";
                    }
                    else
                    {
                        info.DubDiagFlag = "0";
                    }
                    info.DiagInfo.HappenNo = FS.FrameWork.Function.NConvert.ToInt32(row["���"]);//10
                    info.DiagInfo.DiagDate = FS.FrameWork.Function.NConvert.ToDateTime(row["�������"]);//11
                    //{8BC09475-C1D9-4765-918B-299E21E04C74} ���¼������ҽ��վ������ҽ��վ������������
                    if (info.DiagInfo.DiagDate == System.DateTime.MinValue)
                    {
                        info.DiagInfo.DiagDate = (new FrameWork.Management.DataBaseManger()).GetDateTimeFromSysDateTime();
                    }
                    info.Pvisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(row["��Ժ����"]);//12
                    info.Pvisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(row["��Ժ����"]);//13
                    if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
                    {
                        info.OperType = "1";
                    }
                    else if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
                    {
                        info.OperType = "2";
                    }
                    else
                    {
                    }
                    FS.HISFC.BizLogic.HealthRecord.Diagnose dia = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
                    if (row["���ҽʦ����"] != DBNull.Value)
                    {
                        info.DiagInfo.Doctor.ID = row["���ҽʦ����"].ToString();
                    }
                    else
                    {
                        info.DiagInfo.Doctor.ID = dia.Operator.ID;
                        info.DiagInfo.Doctor.Name = dia.Operator.Name;
                    }
                    if (row["���ҽʦ"] != DBNull.Value)
                    {
                        info.DiagInfo.Doctor.Name = row["���ҽʦ"].ToString();
                    }
                    else
                    {
                        info.DiagInfo.Doctor.ID = dia.Operator.ID;
                        info.DiagInfo.Doctor.Name = dia.Operator.Name;
                    }
                    //{8BC09475-C1D9-4765-918B-299E21E04C74} ���¼������ҽ��վ������ҽ��վ������������
                    info.DiagInfo.Dept.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                    info.DiagInfo.IsValid = true;
                    //{74A9AA46-74B3-49e8-910A-54A998E428AF} �������﹦��
                    if (ConvertBool(row["�Ƿ�����"]))
                    {
                        info.User01 = "1";
                    }
                    else
                    {
                        info.User01 = "0";
                    }
                    //info.DiagInfo.ICDF10.ID = row["ICD������"].ToString();
                    //info.DiagInfo.ICDF10.Name = row["ICD����������"].ToString();

                    list.Add(info);
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
        /// ��ʵ��ת����BOOL����
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool ConvertBool(object obj)
        {
            if (obj == DBNull.Value)
            {
                return false;
            }
            return Convert.ToBoolean(obj);
        }
        /// <summary>
        /// ������������ݻ�д������
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int fpEnterSaveChanges(ArrayList list)
        {
            AddInfoToTable(list);
            dtDiagnose.AcceptChanges();
            this.LockFpEnter();
            return 0;
        }
        /// <summary>
        /// ��ѯ�����Ϣ�������ı���
        /// </summary>
        private void AddInfoToTable(ArrayList alReturn)
        {
            bool Result = false;
            if ((this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC && this.patient.CaseState == "2") || (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS && this.patient.CaseState == "3"))
            {
                Result = true;
            }
            //�����ǰ������
            if (this.dtDiagnose != null)
            {
                this.dtDiagnose.Clear();
                this.dtDiagnose.AcceptChanges();
            }
            //ѭ��������Ϣ
            foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in alReturn)
            {
                //����ֻ�����������Ϻ���Ժ���֮������
                //				if(obj.DiagInfo.DiagType.ID != "1"&&obj.DiagInfo.DiagType.ID != "14")
                //				{
                DataRow row = dtDiagnose.NewRow();

                SetRow(obj, row, Result);
                dtDiagnose.Rows.Add(row);
                //				}

            }
            //			if(System.IO.File.Exists(filePath))
            //			{
            //				FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpEnter1_Sheet1,filePath);
            //			}
            if ((this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC && this.patient.CaseState == "2") || (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS && this.patient.CaseState == "3"))
            {
                //��ձ�ı�־λ
                dtDiagnose.AcceptChanges();
            }
            LockFpEnter();
        }

        /// <summary>
        /// ��ֵ
        /// </summary>
        /// <param name="row"></param>
        /// <param name="info"></param>
        private void SetRow(FS.HISFC.Models.HealthRecord.Diagnose info, DataRow row, bool tempBool)
        {
            row["������"] = diagnoseTypeHelper.GetName(info.DiagInfo.DiagType.ID); //0
            row["�������"] = FS.HISFC.Components.HealthRecord.CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(info.DiagInfo.ICD10.Name, false); ;//1
            if (info.DiagInfo.ICD10.ID == "MS999")
            {
                row["ICD10"] = "";
            }
            else
            {
                row["ICD10"] = info.DiagInfo.ICD10.ID;//2
            }
            row["��Ժ����"] = diagOutStateListHelper.GetName(info.DiagOutState); //3
            row["��������"] = OperListHelper.GetName(info.OperationFlag);
            if (info.Is30Disease == "0")//5
            {
                row["30�ּ���"] = false;
            }
            else if (info.Is30Disease == "1")
            {
                row["30�ּ���"] = true;
            }

            if (info.CLPA == "0")//6
            {
                row["�������"] = false;
            }
            else if (info.CLPA == "1")
            {
                row["�������"] = true;
            }
            row["�ּ�"] = LeveListHelper.GetName(info.LevelCode); //7
            row["����"] = PeriorListHelper.GetName(info.PeriorCode);//8


            if (info.DubDiagFlag == "0") //9
            {
                row["�Ƿ�����"] = false;
            }
            else if (info.DubDiagFlag == "1")
            {
                row["�Ƿ�����"] = true;
            }
            //�����
            row["�����"] = info.DiagInfo.IsMain;
            row["���"] = info.DiagInfo.HappenNo;//10
            if (info.DiagInfo.DiagDate == System.DateTime.MinValue)
            {
                row["�������"] = System.DateTime.Now;
            }
            else
            {
                row["�������"] = info.DiagInfo.DiagDate;//11
            }
            row["��Ժ����"] = patient.PVisit.InTime;//12
            row["��Ժ����"] = patient.PVisit.OutTime;//13
            row["���ҽʦ����"] = info.DiagInfo.Doctor.ID;
            row["���ҽʦ"] = info.DiagInfo.Doctor.Name;
            //if (info.DiagInfo.ICDF10.ID == "MS999")
            //{
            //    row["ICD������"] = "";
            //}
            //else
            //{
            //    row["ICD������"] = info.DiagInfo.ICDF10.ID;//2
            //}
            //row["ICD����������"] = FS.HISFC.Components.HealthRecord.CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(info.DiagInfo.ICDF10.Name, false); 
            //{74A9AA46-74B3-49e8-910A-54A998E428AF} �������﹦��
            if (string.IsNullOrEmpty(info.DiagInfo.ICD10.ID.Trim()) && !string.IsNullOrEmpty(info.DiagInfo.ICD10.Name.Trim()))
            {
                row["�Ƿ�����"] = true;
            }
            else
            {
                row["�Ƿ�����"] = false;
            }
            if (info.DiagInfo.ICD10.ID != "MS999")
            {
                row["ICD"] = info.DiagInfo.ICD10.ID;//2
            }
            else
            {
                row["ICD"] = "";//2
            }
            row["ICD����"] = FS.HISFC.Components.HealthRecord.CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(info.Memo,false);
            //����fpSpread1 ������
        }
        private void ucDiagNoseInput_Load(object sender, System.EventArgs e)
        {
            InputMap im;
            im = fpEnter1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpEnter1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpEnter1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            //������Ӧ�����¼�
            fpEnter1.KeyEnter += new FS.FrameWork.WinForms.Controls.NeuFpEnter.keyDown(fpEnter1_KeyEnter);
            fpEnter1.SetItem += new FS.FrameWork.WinForms.Controls.NeuFpEnter.setItem(fpEnter1_SetItem);
            fpEnter1.KeyUp += new KeyEventHandler(fpEnter1_KeyUp);
            fpEnter1.ShowListWhenOfFocus = true;
            #region {6EF7D73B-4350-4790-B98C-C0BD0098516E}
            //if (!this.DesignMode)
            //{
            //    this.ucDiagnose1.Init();
            //}
            #endregion
            //this.ucDiagnose1.SelectItem +=new Common.Controls.ucDiagnose.MyDelegate(ucDiagnose1_SelectItem);
            this.ucDiagnose1.SelectItem+=new ucDiagnose.MyDelegate(ucDiagnose1_SelectItem);
            this.ucDiagnose1.Visible = false;

            LoadTipMessage();
        }

        private void LoadTipMessage()
        {
            try
            {
                if (!System.IO.File.Exists(Application.StartupPath + "\\Setting\\FirstPageSetting.xml"))
                {
                    return;
                }
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                System.Xml.XmlNode node = null;
                doc.Load(Application.StartupPath + "\\Setting\\FirstPageSetting.xml");
                node = doc.SelectSingleNode("/����/��ʾ1");

                if (node != null)
                {
                    this.lblTipMessage.Text = node.InnerXml;
                }

                node = doc.SelectSingleNode("/����/��ʾ2");

                if (node != null)
                {
                    this.lblTipMessage2.Text = node.InnerXml;
                }
            }
            catch
            {
                ;
            }
        }

        private void InitDateTable()
        {
            try
            {
                Type strType = typeof(System.String);
                Type intType = typeof(System.Int32);
                Type dtType = typeof(System.DateTime);
                Type boolType = typeof(System.Boolean);
                Type floatType = typeof(System.Single);

                dtDiagnose.Columns.AddRange(new DataColumn[]{
														   new DataColumn("������", strType),	//0
														   new DataColumn("ICD10", strType),	 //1
														   new DataColumn("�������", strType),//2
														   new DataColumn("��Ժ����", strType),//3
														   new DataColumn("��������", strType),//4
														   new DataColumn("30�ּ���", boolType),//5
														   new DataColumn("�������", boolType),//6
														   new DataColumn("����", strType), //7
														   new DataColumn("�ּ�", strType),//8
														   new DataColumn("�Ƿ�����", boolType),//9
														   new DataColumn("�����", boolType),//9
														   new DataColumn("���", intType),//10
														   new DataColumn("�������", dtType),//11
														   new DataColumn("��Ժ����", dtType),//12
														   new DataColumn("��Ժ����", dtType),//13
														   new DataColumn("���ҽʦ����", strType),//14 
														   new DataColumn("���ҽʦ", strType),//15
                                                           //{74A9AA46-74B3-49e8-910A-54A998E428AF} �������﹦��
                                                           new DataColumn("�Ƿ�����", boolType),//16
                                                           new DataColumn("ICD",strType),//17
                                                           new DataColumn("ICD����",strType),//18
                                                           //new DataColumn("ICD������",strType),//19
                                                           //new DataColumn("ICD����������",strType)//20
                                                           });
                //������Դ
                this.fpEnter1_Sheet1.DataSource = dtDiagnose;
                //����fpSpread1 ������
                //				if(System.IO.File.Exists(filePath))
                //				{
                //					FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpEnter1_Sheet1,filePath);
                //				}
                //				else
                //				{
                //					FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpEnter1_Sheet1,filePath);
                //				}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// �����������б�
        /// </summary>
        private void initList()
        {
            try
            {
                FS.HISFC.BizLogic.HealthRecord.Diagnose da = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
                FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
                this.fpEnter1.SelectNone = true;
                //��ȡ��Ժ���������
                //				diagnoseType = da.GetDiagnoseList();
                diagnoseType = FS.HISFC.Models.HealthRecord.DiagnoseType.SpellList();
                diagnoseTypeHelper.ArrayObject = diagnoseType;
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, 0, diagnoseType);

                //�����б�
                PeriorList = con.GetList(FS.HISFC.Models.Base.EnumConstant.DIAGPERIOD);
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, 7, PeriorList);
                PeriorListHelper.ArrayObject = PeriorList;
                //������������
                OperList = con.GetList(FS.HISFC.Models.Base.EnumConstant.OPERATIONTYPE);
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, 4, OperList);
                OperListHelper.ArrayObject = OperList;

                //�ּ��б� 
                LeveList = con.GetList(FS.HISFC.Models.Base.EnumConstant.DIAGLEVEL);
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, 8, LeveList);
                LeveListHelper.ArrayObject = LeveList;

                //��Ժ����
                diagOutStateList = con.GetList("CASECOMEINSTATE");
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, 3, diagOutStateList);
                diagOutStateListHelper.ArrayObject = diagOutStateList;
                this.fpEnter1.SetSpecalCol((int)Col.Icd10Code);

                this.fpEnter1.SetWidthAndHeight(200, 200);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ����س����� ������ȡ������
        /// </summary>
        /// <returns></returns>
        private int ProcessDept()
        {
            int CurrentRow = fpEnter1_Sheet1.ActiveRowIndex;
            if (CurrentRow < 0) return 0;

            if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.DiagKind) //������� 
            {
                //��ȡѡ�е���Ϣ
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.DiagKind);
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                //2011-10-10 chengym ������д�����ϴ�ӡ�������������Ӧ������
                if (this.fpEnter1_Sheet1.ActiveRowIndex != 0)
                {
                    if (item.ID.ToString() == "1")
                    {
                        MessageBox.Show("ֻ�����һ����д��Ҫ���", "��ʾ��");
                        fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.DiagKind);
                        return -1;
                    }
                }
                
                //������
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Icd10Name);
                //fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICDFName);
                return 0;
            }
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.OutState)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.OutState);
                //��ȡѡ�е���Ϣ
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                // ��Ժ��Ϣ
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                fpEnter1.Focus();
                //fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.OperationFlag);
                //add by chengym
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICD);
                //fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICDFCode);
                return 0;
            }
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.OperationFlag)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.OperationFlag);
                //��ȡѡ�е���Ϣ
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                // ��Ժ��Ϣ
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Disease);
                return 0;
            }
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.Perior)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.Perior);
                //��ȡѡ�е���Ϣ
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                //����
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Level);
                return 0;
            }
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.Level)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.Level);
                //��ȡѡ�е���Ϣ
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                //����
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.DubDiag);
                return 0;
            }
            //{8BC09475-C1D9-4765-918B-299E21E04C74} ���¼������ҽ��վ������ҽ��վ������������
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.Icd10Code)
            {
                #region {9FFEAAA8-2387-4b90-B3BD-D2FBFDC48E95}
                //if (!this.isCas)
                //{
                #region {9F550E5B-669F-4856-BAED-94F69B729CAE}
                //���ӻس�ѡ�������Ϣ
                this.GetInfo();
                #endregion
                if (this.isCas)
                {
                    fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.OutState);
                }
                else
                {
                    fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.IsDraftExamine);
                }
                //fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.IsDraftExamine);
                return 0;
                //}
                #endregion
            }
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.ICD)
            {

                try
                {
                    FS.HISFC.Models.HealthRecord.ICD item = null;
                    //if (this.ucDiagnose1.GetItem(ref item) == -1)
                    //{
                    //    return -1;
                    //}
                    if (this.ucDiagnose1.GetItemCase(ref item) == -1)
                    {
                        return -1;
                    }
                    if (item == null) return -1;

                    string itemCode = string.Empty;

                    for (int i = 0; i < this.fpEnter1_Sheet1.Rows.Count; i++)
                    {
                        if (i == this.fpEnter1_Sheet1.ActiveRowIndex) continue;
                        itemCode = this.fpEnter1_Sheet1.Cells[i, (int)Col.ICD].Text.Trim();
                        if (!string.IsNullOrEmpty(itemCode) && itemCode == item.ID)
                        {
                            MessageBox.Show("������Ѵ��ڣ�");
                            return -1;
                        }
                    }

                    //ICD�������
                    fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICD].Text = item.ID;
                    
                    //ICD��ϱ���
                    fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.IcdName].Text = item.Name;
                    
                    if (this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Icd10Name].Text.Trim() == "")
                    {
                        this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Icd10Name].Text = item.Name;
                    }
                  
                    ucDiagnose1.Visible = false;
                    fpEnter1.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return 0;
            }


            //else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.ICDFCode)
            //{

            //    try
            //    {
            //        FS.HISFC.Models.HealthRecord.ICD item = null;
            //        //if (this.ucDiagnose1.GetItem(ref item) == -1)
            //        //{
            //        //    return -1;
            //        //}
            //        if (this.ucDiagnose1.GetItemCase(ref item) == -1)
            //        {
            //            return -1;
            //        }
            //        if (item == null) return -1;

            //        string itemCode = string.Empty;

            //        for (int i = 0; i < this.fpEnter1_Sheet1.Rows.Count; i++)
            //        {
            //            if (i == this.fpEnter1_Sheet1.ActiveRowIndex) continue;
            //            itemCode = this.fpEnter1_Sheet1.Cells[i, (int)Col.ICDFCode].Text.Trim();
            //            if (!string.IsNullOrEmpty(itemCode) && itemCode == item.ID)
            //            {
            //                MessageBox.Show("������Ѵ��ڣ�");
            //                return -1;
            //            }
            //        }

            //        //ICD�����������
            //        fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICDFCode].Text = item.ID;
            //        //ICD������ϱ���

            //        if (this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICDFName].Text.Trim() == "")
            //        {
            //            this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICDFName].Text = item.Name;
            //        }

            //        ucDiagnose1.Visible = false;
            //        fpEnter1.Focus();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //    return 0;
            //}

            return 0;
        }
        /// <summary>
        /// ������Ӧ����
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int fpEnter1_KeyEnter(Keys key)
        {
            if (key == Keys.Enter)
            {
                //				MessageBox.Show("Enter,�����Լ���Ӵ����¼�������������һcell");
                //�س�
                if (this.fpEnter1.ContainsFocus)
                {
                    int i = this.fpEnter1_Sheet1.ActiveColumnIndex;
                    //{8BC09475-C1D9-4765-918B-299E21E04C74} ���¼������ҽ��վ������ҽ��վ������������
                    if (i == (int)Col.DiagKind || i == (int)Col.OutState || i == (int)Col.OperationFlag || i == (int)Col.Perior || i == (int)Col.Level || i == (int)Col.Icd10Code)
                    {
                        ProcessDept();
                    }
                    else if (i == (int)Col.DubDiag)
                    {
                        fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.IsDraftExamine);
                    }
                    else if (i == (int)Col.IsDraftExamine)
                    {
                        if (fpEnter1_Sheet1.ActiveRowIndex < fpEnter1_Sheet1.Rows.Count - 1)
                        {
                            fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex + 1, 0);
                        }
                        else
                        {
                            if (this.Tag != null)
                            {
                                this.AddBlankRow(); //����һ���հ��� 
                            }
                            else
                            {
                                //����һ��
                                this.AddRow();
                            }
                        }
                    }
                    else
                    {
                        if (i < (int)Col.DubDiag)
                        {
                            fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, i + 1);
                        }
                    }
                }
            }
            else if (key == Keys.Up)
            {
                //				MessageBox.Show("Up,�����Լ���Ӵ����¼��������������б�ʱ���������У���ʾ�����ؼ�ʱ���������ؼ������ƶ�");
            }
            else if (key == Keys.Down)
            {
                //				MessageBox.Show("Down�������Լ���Ӵ����¼��������������б�ʱ���������У���ʾ�����ؼ�ʱ���������ؼ������ƶ�");
            }
            else if (key == Keys.Escape)
            {
                //				MessageBox.Show("Escape,ȡ���б�ɼ�");
            }
            else if (key == Keys.Add)
            {
                if (fpEnter1_Sheet1.Rows.Count == 0 || fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.DubDiag)
                {
                    AddRow();
                }
            }
            return 0;
        }
        /// <summary>
        /// ���һ����Ŀ
        /// </summary>
        /// <returns></returns>
        public int AddRow()
        {
            try
            {
                if (fpEnter1_Sheet1.Rows.Count < 1)
                {
                    //����һ�п�ֵ
                    DataRow row = dtDiagnose.NewRow();
                    row["���"] = 1;
                    dtDiagnose.Rows.Add(row);
                    for (int i = 0; i < fpEnter1_Sheet1.RowCount; i++)
                    {
                        fpEnter1_Sheet1.Cells[i, (int)Col.IsDraftExamine].Value = "True";
                    }
                }
                else
                {
                    //����һ��
                    int j = fpEnter1_Sheet1.Rows.Count;
                    this.fpEnter1_Sheet1.Rows.Add(j, 1);

                    fpEnter1_Sheet1.Cells[fpEnter1_Sheet1.Rows.Count - 1, (int)Col.DiagKind].Value = "�������";
                    fpEnter1_Sheet1.Cells[fpEnter1_Sheet1.Rows.Count-1, (int)Col.IsDraftExamine].Value = "True";
                    //for (int i = 0; i < fpEnter1_Sheet1.Columns.Count; i++)
                    //{
                    //    if (i == 0)
                    //    {
                    //        fpEnter1_Sheet1.Cells[j, i].Value = "�������";
                    //    }
                    //    else
                    //    {
                    //        fpEnter1_Sheet1.Cells[j, i].Value = fpEnter1_Sheet1.Cells[j - 1, i].Value;
                    //    }
                    //}
                }
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.Rows.Count, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }
        /// <summary>
        /// ���һ����Ŀ
        /// </summary>
        /// <returns></returns>
        public int InsertRow()
        {
            try
            {
                if (this.fpEnter1_Sheet1.RowCount == 0)
                {
                    this.AddRow();
                }
                else
                {
                    //����һ��
                    int actRow = fpEnter1_Sheet1.ActiveRowIndex + 1;
                    this.fpEnter1_Sheet1.Rows.Add(actRow, 1);
                    fpEnter1_Sheet1.Cells[actRow, (int)Col.DiagKind].Value = "�������";
                    fpEnter1_Sheet1.Cells[actRow, (int)Col.IsDraftExamine].Value = "True";
                    //for (int i = 0; i < fpEnter1_Sheet1.Columns.Count; i++)
                    //{
                    //    if (i == 0)
                    //    {
                    //        fpEnter1_Sheet1.Cells[actRow, i].Value = "�������";
                    //    }
                    //    else
                    //    {
                    //        fpEnter1_Sheet1.Cells[actRow, i].Value = fpEnter1_Sheet1.Cells[actRow - 1, i].Value;
                    //    }
                    //}
                    fpEnter1.Focus();
                    fpEnter1_Sheet1.SetActiveCell(actRow, 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }
        /// <summary>
        /// ��������Ŀ�� ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem1_Click(object sender, System.EventArgs e)
        {
            Common.Controls.ucSetColumn uc = new Common.Controls.ucSetColumn();
            uc.FilePath = this.filePath;
            //uc.GoDisplay += new Common.Controls.ucSetColumn.DisplayNow(uc_GoDisplay);
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
        }
        /// <summary>
        /// ����fpSpread1_Sheet1�Ŀ�ȵ� ����󴥷����¼�
        /// </summary>
        private void uc_GoDisplay()
        {
            //			LoadInfo(inpatientNo,operType); //���¼�������

        }

        /// <summary>
        /// ɾ����ǰ��¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem2_Click(object sender, System.EventArgs e)
        {
            if (this.fpEnter1_Sheet1.Rows.Count > 0)
            {
                //ɾ����ǰ��
                this.fpEnter1_Sheet1.Rows.Remove(this.fpEnter1_Sheet1.ActiveRowIndex, 1);
            }
        }

        /// <summary>
        /// �޶���Ŀ�Ⱥܿɼ��� 
        /// </summary>
        private void LockFpEnter()
        {
            this.fpEnter1_Sheet1.Columns[0].Width = 59; //������
            this.fpEnter1_Sheet1.Columns[1].Width = 124;//ICD10
            this.fpEnter1_Sheet1.Columns[2].Locked = true;
            this.fpEnter1_Sheet1.Columns[2].Width = 150;//�������
            this.fpEnter1_Sheet1.Columns[3].Width = 65; //��Ժ����
            this.fpEnter1_Sheet1.Columns[4].Width = 40; //��������
            this.fpEnter1_Sheet1.Columns[5].Width = 40; //30�ּ���
            this.fpEnter1_Sheet1.Columns[6].Width = 40; //�������
            this.fpEnter1_Sheet1.Columns[7].Width = 51; //����
            this.fpEnter1_Sheet1.Columns[8].Width = 50; //�ּ�
            this.fpEnter1_Sheet1.Columns[9].Width = 40; //�Ƿ�����
            this.fpEnter1_Sheet1.Columns[10].Width = 40; //�����
            this.fpEnter1_Sheet1.Columns[10].Visible = false; //�����
            this.fpEnter1_Sheet1.Columns[11].Visible = false; //���
            this.fpEnter1_Sheet1.Columns[12].Visible = false; //�������
            this.fpEnter1_Sheet1.Columns[13].Visible = false; //��Ժ����
            this.fpEnter1_Sheet1.Columns[14].Visible = false; //��Ժ����
            this.fpEnter1_Sheet1.Columns[15].Visible = false; //���ҽʦ����
            this.fpEnter1_Sheet1.Columns[16].Visible = false; //���ҽʦ

            //{8BC09475-C1D9-4765-918B-299E21E04C74} ���¼������ҽ��վ������ҽ��վ������������
            if (!isCas) 
            {

                fpEnter1_Sheet1.Columns[(int)Col.OutState].Visible = false;//��Ժ����
                fpEnter1_Sheet1.Columns[(int)Col.OperationFlag].Visible = false;//��������
                fpEnter1_Sheet1.Columns[(int)Col.Disease].Visible = false;//30�ּ���
                fpEnter1_Sheet1.Columns[(int)Col.CLPa].Visible = false;//�������
                fpEnter1_Sheet1.Columns[(int)Col.Perior].Visible = false;//����
                fpEnter1_Sheet1.Columns[(int)Col.Level].Visible = false;//�ּ�
                fpEnter1_Sheet1.Columns[(int)Col.DubDiag].Visible = false;//�Ƿ�����
                //fpEnter1_Sheet1.Columns[(int)Col.MainDiag].Visible = true;//�����
            }
            else
            {
                fpEnter1_Sheet1.Columns[(int)Col.OutState].Visible = true;//��Ժ����
                fpEnter1_Sheet1.Columns[(int)Col.OperationFlag].Visible = true;//��������
                fpEnter1_Sheet1.Columns[(int)Col.Disease].Visible = true;//30�ּ���
                fpEnter1_Sheet1.Columns[(int)Col.CLPa].Visible = true;//�������
                fpEnter1_Sheet1.Columns[(int)Col.Perior].Visible = true;//����
                fpEnter1_Sheet1.Columns[(int)Col.Level].Visible = true;//�ּ�
                fpEnter1_Sheet1.Columns[(int)Col.DubDiag].Visible = true;//�Ƿ�����
                //fpEnter1_Sheet1.Columns[(int)Col.MainDiag].Visible = false;//�����

            }



            //{74A9AA46-74B3-49e8-910A-54A998E428AF} �������﹦��
            fpEnter1_Sheet1.Columns[(int)Col.IsDraftExamine].Visible = true;//�Ƿ�����
            for (int i = 0; i < fpEnter1_Sheet1.Rows.Count; i++)
            {
                if (this.fpEnter1_Sheet1.Cells[i, (int)Col.IsDraftExamine].Value == null || this.fpEnter1_Sheet1.Cells[i, (int)Col.IsDraftExamine].Value.ToString() == "False")
                {
                    this.fpEnter1_Sheet1.Cells[i, (int)Col.Icd10Code].Locked = false;
                    this.fpEnter1_Sheet1.Cells[i, (int)Col.Icd10Name].Locked = true;
                    //this.fpEnter1_Sheet1.Cells[i, (int)Col.ICDFCode].Locked = true;
                    //this.fpEnter1_Sheet1.Cells[i, (int)Col.ICDFName].Locked = true;
                }
                else
                {
                    this.fpEnter1_Sheet1.Cells[i, (int)Col.Icd10Code].Locked = true;
                    this.fpEnter1_Sheet1.Cells[i, (int)Col.Icd10Name].Locked = false;
                    this.fpEnter1_Sheet1.Cells[i, (int)Col.Icd10Code].Text = " ";
                    //this.fpEnter1_Sheet1.Cells[i, (int)Col.ICDFCode].Locked = false;
                    //this.fpEnter1_Sheet1.Cells[i, (int)Col.ICDFName].Locked = false;
                }
            }
            //add by chengym һ��ҽ��¼����������� ����ѡ��ICD
            if (isCas)
            {
                fpEnter1_Sheet1.Columns[(int)Col.Icd10Code].Visible = false;//icd10����
                fpEnter1_Sheet1.Columns[(int)Col.OperationFlag].Visible = false;//��������
                fpEnter1_Sheet1.Columns[(int)Col.Disease].Visible = false;//30�ּ���
                fpEnter1_Sheet1.Columns[(int)Col.CLPa].Visible = false;//�������
                fpEnter1_Sheet1.Columns[(int)Col.Perior].Visible = false;//����
                fpEnter1_Sheet1.Columns[(int)Col.Level].Visible = false;//�ּ�
                fpEnter1_Sheet1.Columns[(int)Col.DubDiag].Visible = false;//�Ƿ�����
                fpEnter1_Sheet1.Columns[(int)Col.IsDraftExamine].Visible = false;//�Ƿ�����

                this.fpEnter1_Sheet1.Columns[2].Width = 320;//�������
                this.fpEnter1_Sheet1.Columns[18].Width = 59;
                this.fpEnter1_Sheet1.Columns[19].Width = 220;
                this.fpEnter1_Sheet1.Columns[(int)Col.IcdName].Visible = false;

                for (int i = 0; i < fpEnter1_Sheet1.Rows.Count; i++)
                {
                    this.fpEnter1_Sheet1.Cells[i, (int)Col.IsDraftExamine].Value = "True";
                }
            }
          
        }
        private int fpEnter1_SetItem(FS.FrameWork.Models.NeuObject obj)
        {
            this.ProcessDept();
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        { 
            if (keyData == Keys.NumPad1)
            {
                //r�����ǰ����checkbox���͵� �� ����1 ѡ��״̬
                int i = fpEnter1_Sheet1.ActiveColumnIndex;
                if (i == (int)Col.Disease || i == (int)Col.CLPa || i == (int)Col.DubDiag || i == (int)Col.MainDiag)
                {
                    //ͳ�Ʊ�־ȡ��
                    if (fpEnter1_Sheet1.Cells[fpEnter1_Sheet1.ActiveRowIndex, i].Value == null)
                    {
                        fpEnter1_Sheet1.Cells[fpEnter1_Sheet1.ActiveRowIndex, i].Value = true;
                    }
                    else if (fpEnter1_Sheet1.Cells[fpEnter1_Sheet1.ActiveRowIndex, i].Value.ToString() == "False")
                    {
                        fpEnter1_Sheet1.Cells[fpEnter1_Sheet1.ActiveRowIndex, i].Value = true;
                    }
                    else
                    {
                        fpEnter1_Sheet1.Cells[fpEnter1_Sheet1.ActiveRowIndex, i].Value = false;
                    }
                }
            }
            else if (keyData.GetHashCode() == Keys.Escape.GetHashCode())
            {
                this.ucDiagnose1.Visible = false;
            }
            else if (keyData.GetHashCode() == Keys.Up.GetHashCode())
            {
                if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.Icd10Code)
                {
                    if (this.ucDiagnose1.Visible == true)
                    {
                        this.ucDiagnose1.PriorRow();
                    }
                }
                else if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.ICD)
                {
                    if (this.ucDiagnose1.Visible == true)
                    {
                        this.ucDiagnose1.PriorRow();
                    }
                }
            }
            else if (keyData.GetHashCode() == Keys.Down.GetHashCode())
            {
                if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.Icd10Code)
                {
                    if (this.ucDiagnose1.Visible == true)
                    {
                        this.ucDiagnose1.NextRow();
                    }
                }
                else if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.ICD)
                {
                    if (this.ucDiagnose1.Visible == true)
                    {
                        this.ucDiagnose1.NextRow();
                    }
                }
            }
            else if (keyData.GetHashCode() == Keys.Enter.GetHashCode())
            {
                if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.Icd10Code)
                {
                    if (this.ucDiagnose1.Visible == true)
                    {
                        this.ucDiagnose1_SelectItem(keyData);
                    }
                }
                else if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.ICD)
                {
                    //if (this.ucDiagnose1.Visible == true)
                    //{
                    //    this.ucDiagnose1_SelectItem(keyData);
                    //}
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        private void fpEnter1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            //����fpSpread1 ������
            if (System.IO.File.Exists(filePath))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpEnter1_Sheet1, filePath);
            }
        }
        /// <summary>
        /// ����Ԫ���е����ݱ仯ʱ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpEnter1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            //ɸѡ����

            try
            {
                if (e.Column == 1)
                {
                    if (this.ucDiagnose1.Visible == false)
                    {
                        this.ucDiagnose1.Visible = true;
                    }
                    string str = fpEnter1_Sheet1.ActiveCell.Text;
                    //this.ucDiagnose1.Filter(str);
                    this.ucDiagnose1.FilterCase(str);
                }
                else if (e.Column == 18)//add by chengym
                {
                    if (this.ucDiagnose1.Visible == false)
                    {
                        this.ucDiagnose1.Visible = true;
                    }
                    string str = fpEnter1_Sheet1.ActiveCell.Text;
                    //this.ucDiagnose1.Filter(str);
                    this.ucDiagnose1.FilterCase(str);
                }
                else if (e.Column == 20)//add by ren.jch
                {
                    if (this.ucDiagnose1.Visible == false)
                    {
                        this.ucDiagnose1.Visible = true;
                    }
                    string str = fpEnter1_Sheet1.ActiveCell.Text;
                    //this.ucDiagnose1.Filter(str);
                    this.ucDiagnose1.FilterCase(str);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private int GetInfo()
        {
            try
            {
                if (this.fpEnter1_Sheet1.ActiveColumnIndex == 1)
                {
                    FS.HISFC.Models.HealthRecord.ICD item = null;
                    if (this.ucDiagnose1.GetItem(ref item) == -1)
                    {
                        //MessageBox.Show("��ȡ��Ŀ����!","��ʾ");
                        return -1;
                    }
                    //			this.contralActive.Text=(item as FS.HISFC.Models.HealthRecord.ICD).Name;
                    //			this.contralActive.Tag=item;
                    //			this.ucDiag1.Visible=false;
                    if (item == null) return -1;

                    string itemCode = string.Empty;

                    for (int i = 0; i < this.fpEnter1_Sheet1.Rows.Count; i++)
                    {
                        if (i == this.fpEnter1_Sheet1.ActiveRowIndex) continue;
                        itemCode = this.fpEnter1_Sheet1.Cells[i, (int)Col.Icd10Code].Text.Trim();
                        if (!string.IsNullOrEmpty(itemCode) && itemCode == item.ID)
                        {
                            MessageBox.Show("������Ѵ��ڣ�");
                            return -1;
                        }
                    }

                    //ICD�������
                    fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Icd10Code].Text = item.ID;
                    //ICD��ϱ���
                    fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Icd10Name].Text = item.Name;
                    ucDiagnose1.Visible = false;
                    fpEnter1.Focus();
                    //{8BC09475-C1D9-4765-918B-299E21E04C74} ���¼������ҽ��վ������ҽ��վ������������
                    if (this.isCas)
                    {
                        fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.OutState);
                    }
                    else
                    {
                        fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.IsDraftExamine);
                    }
                }
                else if(this.fpEnter1_Sheet1.ActiveColumnIndex==18)//add chengym 2011-9-27
                {
                    FS.HISFC.Models.HealthRecord.ICD item = null;
                    if (this.ucDiagnose1.GetItemCase(ref item) == -1)
                    {
                        return -1;
                    }
                    if (item == null) return -1;

                    string itemCode = string.Empty;

                    for (int i = 0; i < this.fpEnter1_Sheet1.Rows.Count; i++)
                    {
                        if (i == this.fpEnter1_Sheet1.ActiveRowIndex) continue;
                        itemCode = this.fpEnter1_Sheet1.Cells[i, (int)Col.ICD].Text.Trim();
                        if (!string.IsNullOrEmpty(itemCode) && itemCode == item.ID)
                        {
                            MessageBox.Show("������Ѵ��ڣ�");
                            return -1;
                        }
                    }

                    //ICD�������
                    fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICD].Text = item.ID;
                    //ICD��ϱ���
                    fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.IcdName].Text = item.Name;
                    if (fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Icd10Name].Text.Trim() == "")
                    {
                        fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Icd10Name].Text = item.Name;
                    }
                    ucDiagnose1.Visible = false;
                    fpEnter1.Focus();
                    fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.IcdName);
                }
                else if (this.fpEnter1_Sheet1.ActiveColumnIndex == 20)//add 
                {
                    FS.HISFC.Models.HealthRecord.ICD item = null;
                    if (this.ucDiagnose1.GetItemCase(ref item) == -1)
                    {
                        return -1;
                    }
                    if (item == null) return -1;

                    string itemCode = string.Empty;

                    for (int i = 0; i < this.fpEnter1_Sheet1.Rows.Count; i++)
                    {
                        if (i == this.fpEnter1_Sheet1.ActiveRowIndex) continue;
                        //itemCode = this.fpEnter1_Sheet1.Cells[i, (int)Col.ICDFCode].Text.Trim();
                        if (!string.IsNullOrEmpty(itemCode) && itemCode == item.ID)
                        {
                            MessageBox.Show("������Ѵ��ڣ�");
                            return -1;
                        }
                    }

                    ////ICD�������
                    //fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICDFCode].Text = item.ID;
                    ////ICD��ϱ���
                    ////fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICDFName].Text = item.Name;
                    //if (fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICDFName].Text.Trim() == "")
                    //{
                    //    fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICDFName].Text = item.Name;
                    //}
                    ucDiagnose1.Visible = false;
                    fpEnter1.Focus();
                    fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.IcdName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }
        /// <summary>
        /// ���㵽��Ԫ�� 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpEnter1_EditModeOn(object sender, System.EventArgs e)
        {
            if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.Icd10Code)
            {
                Control _cell = fpEnter1.EditingControl;
                //����λ��
                this.ucDiagnose1.Location = new System.Drawing.Point(_cell.Location.X, _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                ucDiagnose1.BringToFront();
                //this.ucDiagnose1.Filter(fpEnter1_Sheet1.ActiveCell.Text);
                this.ucDiagnose1.FilterCase(fpEnter1_Sheet1.ActiveCell.Text.Trim());

                this.ucDiagnose1.Visible = true;
            }
            else if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.ICD)//add by chengym
            {
                Control _cell = fpEnter1.EditingControl;
                //����λ��
                this.ucDiagnose1.Location = new System.Drawing.Point(_cell.Location.X, _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                ucDiagnose1.BringToFront();
                this.ucDiagnose1.FilterCase(fpEnter1_Sheet1.ActiveCell.Text.Trim());
                //this.ucDiagnose1.Filter(fpEnter1_Sheet1.ActiveCell.Text);
                this.ucDiagnose1.Visible = true;
            }
            //else if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.ICDFCode)//add by 
            //{
            //    Control _cell = fpEnter1.EditingControl;
            //    //����λ��
            //    this.ucDiagnose1.Location = new System.Drawing.Point(_cell.Location.X, _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
            //    ucDiagnose1.BringToFront();
            //    this.ucDiagnose1.FilterCase(fpEnter1_Sheet1.ActiveCell.Text.Trim());
            //    //this.ucDiagnose1.Filter(fpEnter1_Sheet1.ActiveCell.Text);
            //    this.ucDiagnose1.Visible = true;
            //}
            else
            {
                this.ucDiagnose1.Visible = false;
            }
            if (this.ucDiagnose1.Visible == true)
            {
                this.panel5.Width = 0;
            }
            else
            {
                this.panel5.Width = 186;
            }
            //��ʾ
        }

        private int ucDiagnose1_SelectItem(Keys key)
        {
            GetInfo();
            return 0;
        }

        private void fpEnter1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.fpEnter1_Sheet1.ActiveColumnIndex == 2)//(int)Col.Icd10Code)
                {
                    GetInfo();
                }
            }
        }

        //{74A9AA46-74B3-49e8-910A-54A998E428AF} �������﹦��
        private void fpEnter1_Sheet1_CellChanged(object sender, SheetViewEventArgs e)
        {
            if (e.Column == (int)Col.IsDraftExamine)
            {
                if (this.fpEnter1_Sheet1.Cells[e.Row, e.Column].Value.ToString() == "False")
                {
                    this.fpEnter1_Sheet1.Cells[e.Row, (int)Col.Icd10Code].Locked = false;
                    this.fpEnter1_Sheet1.Cells[e.Row, (int)Col.Icd10Name].Locked = true;
                    //this.fpEnter1_Sheet1.Cells[e.Row, (int)Col.ICDFCode].Locked = true;
                    //this.fpEnter1_Sheet1.Cells[e.Row, (int)Col.ICDFName].Locked = true;
                }
                else
                {
                    this.fpEnter1_Sheet1.Cells[e.Row, (int)Col.Icd10Code].Locked = true;
                    this.fpEnter1_Sheet1.Cells[e.Row, (int)Col.Icd10Name].Locked = false;
                    this.fpEnter1_Sheet1.Cells[e.Row, (int)Col.Icd10Code].Text = " ";
                    //this.fpEnter1_Sheet1.Cells[e.Row, (int)Col.ICDFName].Locked = false;
                    //this.fpEnter1_Sheet1.Cells[e.Row, (int)Col.ICDFCode].Locked = false;
                }
            }
        }

        #region ���Ӳ����е���ϻ�ȡ 2011-9-27 chengym
        private void DiagnoseLoadInfo(string inPatient, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes frmType)
        {
            FS.HISFC.BizLogic.HealthRecord.Diagnose diagInfo = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            List<FS.FrameWork.Models.NeuObject> diagList = new List<FS.FrameWork.Models.NeuObject>();

            if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
            {
                diagList = diagInfo.QueryDiagnoseFromOutCaseByInpatient(inPatient);
                if (diagList != null)
                {
                    if (diagList.Count == 0)
                    {
                        this.rtxtDiag.Text = string.Empty;
                    }
                    else
                    {
                        foreach (FS.FrameWork.Models.NeuObject info in diagList)
                        {
                            this.rtxtDiag.Text = this.rtxtDiag.Text + "\n" + info.Memo.ToString();
                        }
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="RowID"></param>
        private void SetDoubtFarmart(int RowID)
        {
            if (!this.isDoubt)
            {
                this.fpEnter1_Sheet1.Cells[RowID, (int)Col.IsDraftExamine].Value = "False";
            }
            else
            {
                this.fpEnter1_Sheet1.Cells[RowID, (int)Col.IsDraftExamine].Value= "True";
            }
        }
        /// <summary>
        /// ����һ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAdd_Click(object sender, EventArgs e)
        {
            this.AddRow();
        }
        /// <summary>
        /// ɾ����ǰ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDelete_Click(object sender, EventArgs e)
        {
            this.DeleteActiveRow();
        }

        /// <summary>
        /// ��ȡ��Ͻ�������
        /// chengym 2011-9-27 
        /// ԭ��ʹ��DataTable��GetChanges�ķ�ʽʵ������дĪ����������鲻��ԭ�򣨹������޸Ķ��ˣ�ĳ���ط�Ӱ���ˣ�
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool GetDiagnosInfo(List<FS.HISFC.Models.HealthRecord.Diagnose> list)
        {
            try
            {
                FS.HISFC.Models.HealthRecord.Diagnose info = null;
                for (int row = 0; row < this.fpEnter1_Sheet1.RowCount; row++)
                {
                    info = new FS.HISFC.Models.HealthRecord.Diagnose();
                    info.DiagInfo.Patient.ID = this.patient.ID;//סԺ��ˮ��
                    info.DiagInfo.Patient.PID.CardNO = this.patient.PID.CardNO;//����
                    //������
                    info.DiagInfo.DiagType.ID = diagnoseTypeHelper.GetID(this.fpEnter1_Sheet1.Cells[row, (int)Col.DiagKind].Text);
                    //��Ժ��ϵ���ϸ���
                    if (info.DiagInfo.DiagType.ID == "1") //����������ó� 
                    {
                        info.DiagInfo.IsMain = true;
                    }
                    else
                    {
                        info.DiagInfo.IsMain = false;
                    }
                    if (this.fpEnter1_Sheet1.Cells[row, (int)Col.ICD].Text != "")//ICD����
                    {
                        info.DiagInfo.ICD10.ID = this.fpEnter1_Sheet1.Cells[row, (int)Col.ICD].Text;
                    }
                    else
                    {
                        info.DiagInfo.ICD10.ID = "MS999";
                    }
                    info.DiagInfo.ICD10.Name =FS.HISFC.Components.HealthRecord.CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(this.fpEnter1_Sheet1.Cells[row, (int)Col.Icd10Name].Text,true);//ICD����
                    //if (this.fpEnter1_Sheet1.Cells[row, (int)Col.ICDFCode].Text != "")//����ICD����
                    //{
                    //    info.DiagInfo.ICDF10.ID = this.fpEnter1_Sheet1.Cells[row, (int)Col.ICDFCode].Text;
                    //}
                    //else
                    //{
                    //    info.DiagInfo.ICDF10.ID = "MS999";
                    //}
                    //info.DiagInfo.ICDF10.Name = FS.HISFC.Components.HealthRecord.CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(this.fpEnter1_Sheet1.Cells[row, (int)Col.ICDFName].Text, true);//����ICD����
                    
                    if (this.fpEnter1_Sheet1.Cells[row, (int)Col.OutState].Text != "")//��Ժת�����
                    {
                        info.DiagOutState = diagOutStateListHelper.GetID(this.fpEnter1_Sheet1.Cells[row, (int)Col.OutState].Text); //3
                    }

                    info.OperationFlag = "";//��������
                    info.Is30Disease = "0";//30�ּ���
                    info.CLPA = "0";//�������
                    info.LevelCode = ""; //�ּ�
                    info.PeriorCode = "";//����
                    info.DubDiagFlag = "0";//�Ƿ�����
                    info.DiagInfo.HappenNo = row;//10
                    info.DiagInfo.DiagDate = patient.PVisit.InTime;//11�������  
                    info.Pvisit.InTime = patient.PVisit.InTime;//12��Ժ����
                    info.Pvisit.OutTime = patient.PVisit.OutTime;//13��Ժ����
                    info.OperType = "1";//ҽ��վ¼��

                    info.PerssonType = FS.HISFC.Models.Base.ServiceTypes.I;
                    info.IsValid = true;

                    FS.HISFC.BizLogic.HealthRecord.Diagnose dia = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

                    info.DiagInfo.Doctor.ID = dia.Operator.ID;//���ҽʦ����
                    info.DiagInfo.Doctor.Name = dia.Operator.Name;//���ҽʦ
                    info.DiagInfo.Dept.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                    info.DiagInfo.IsValid = true;
                    info.Pvisit.Memo = "0";//�Ƿ�����
                    info.Memo = FS.HISFC.Components.HealthRecord.CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(this.fpEnter1_Sheet1.Cells[row, (int)Col.IcdName].Text,true); ;//ICD�������
                    info.PerssonType = FS.HISFC.Models.Base.ServiceTypes.I;
                    info.IsValid = true;
                    list.Add(info);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void btInsert_Click(object sender, EventArgs e)
        {
            this.InsertRow();
        }
    }
}
