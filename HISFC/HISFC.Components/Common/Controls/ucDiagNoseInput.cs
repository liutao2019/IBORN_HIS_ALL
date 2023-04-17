using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FarPoint.Win.Spread; 
namespace FS.HISFC.Components.Common.Controls
{ /// <summary>
    /// [��������: ���¼�����]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-7-11]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// <��ע�� ���ؼ�����ͨ���������ã���ʵ�ֱַ𱣴�ҽ��¼����ϺͲ�����¼�����
    /// />
    /// </summary>
    public partial class ucDiagNoseInput : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDiagNoseInput()
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
        //��Ժ��� �б�
        private ArrayList diagOutStateList = new ArrayList();
        private FS.FrameWork.Public.ObjectHelper diagOutStateListHelper = new FS.FrameWork.Public.ObjectHelper();
        private string inpatientNo;
        private FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();
        //���� �������� 
        private ArrayList OperList = new ArrayList();
        private FS.FrameWork.Public.ObjectHelper OperListHelper = new FS.FrameWork.Public.ObjectHelper();
        //�����Ϣ
        public ArrayList diagList = null;
        FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
        //��ʶ��ҽ��վ ���� ������

        private FS.HISFC.Models.HealthRecord.EnumServer.frmTypes frmType = FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC;
        /// <summary>
        /// ���幤��������

        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        private FarPoint.Win.Spread.CellType.CheckBoxCellType checkCellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
        #endregion

        #region ����


        [Description("����Ա����")]
        public FS.HISFC.Models.HealthRecord.EnumServer.frmTypes OperType
        {
            set
            {
                this.frmType = value;
            }
            get
            {
                return frmType;
            }
        }
        #endregion

        #region ���������Ӱ�ť�����¼�

        /// <summary>
        /// ���������Ӱ�ť�����¼�

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    this.AddRow();
                    break;
                case "ˢ��":
                    this.LoadInfo(inpatientNo);
                    break;
                case "ɾ��":
                    this.DeleteActiveRow();
                    break;
                case "��ӡ":

                    break;
                default:
                    break;
            }
        }
        #endregion

        #region ��ʼ��������
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "����", FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            toolBarService.AddToolButton("ˢ��", "ˢ��", FS.FrameWork.WinForms.Classes.EnumImageList.Sˢ��, true, false, null);
            toolBarService.AddToolButton("ɾ��", "ɾ��", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            toolBarService.AddToolButton("��ӡ", "��ӡ", FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            return toolBarService;
        }
        #endregion


        #region ö��
        enum Cols
        {
            diagType, //������
            ICDCode,//icd
            ICDName, //ICD����
            outState, //��Ժ���
            Operation,//��������
            disease, //30�ּ���

            clpa,//�������
            perionCode,//���� 
            levelCode,//�ּ�
            dubdiag,//�Ƿ�����
            mainDiag,//�����

            happenNo,//���
            diagTime,//�������
            inTime,//��Ժ����
            outTime,//��Ժ����
            diagDocCode,//���ҽ��
            diagDocName//���ҽ��

        }
        #endregion

        #region ��ʼ��


        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.fpEnter1_Sheet1.Rows.Remove(0, this.fpEnter1_Sheet1.RowCount);
            FS.FrameWork.Models.NeuObject obj = neuObject as FS.FrameWork.Models.NeuObject;
            if (obj == null) return -1;
            this.inpatientNo = obj.ID;
            this.LoadInfo(obj.ID);//�ּ�
            return 0;
        }
        #region �����ʼ��

        /// <summary>
        /// ��ʼ��

        /// </summary>
        private void InitInfo()
        {
            try
            {
                //���������б�
                this.initList();
                fpEnter1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
                LockFpEnter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region �����������б�

        /// <summary>
        /// �����������б�

        /// </summary>
        private void initList()
        {
            try
            {
                FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase da = new FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase();
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

                //��Ժ����б�
                diagOutStateList = con.GetList(FS.HISFC.Models.Base.EnumConstant.ZG);
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, 3, diagOutStateList);
                diagOutStateListHelper.ArrayObject = diagOutStateList;

                this.fpEnter1.SetWidthAndHeight(200, 200);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #endregion

        #region ����һ��

        //���һ����Ŀ

        public int AddRow()
        {
            try
            {
                if (this.inpatientNo == null || this.inpatientNo == "")
                {
                    MessageBox.Show("¼���ǰ����ѡ����");
                    return 0;
                }
                fpEnter1_Sheet1.Rows.Add(0, 1);
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.Rows.Count, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }
        #endregion

        #region ɾ��һ��

        /// <summary>
        /// ɾ����ǰ�� 
        /// </summary>
        /// <returns></returns>
        public int DeleteActiveRow()
        {
            if (fpEnter1_Sheet1.Rows.Count > 0)
            {
                this.fpEnter1_Sheet1.Rows.Remove(fpEnter1_Sheet1.ActiveRowIndex, 1);
            }
            if (fpEnter1_Sheet1.Rows.Count == 0)
            {
                this.fpEnter1.SetAllListBoxUnvisible();
            }
            return 1;
        }
        #endregion

        #region ����

        #region ��ȡ��Ҫ���������
        #region ��ȡ�޸Ĺ�����Ϣ
        /// <summary>
        /// ��ȡ�޸Ĺ�����Ϣ
        /// </summary>
        /// <returns></returns>
        private ArrayList GetChangeInfo()
        {
            ArrayList list = new ArrayList();
            try
            {
                FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase dia = new FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase();
                FS.HISFC.Models.HealthRecord.Diagnose info = null;
                for (int i = 0; i < this.fpEnter1_Sheet1.RowCount; i++)
                {
                    info = new FS.HISFC.Models.HealthRecord.Diagnose();
                    info.DiagInfo.Patient.ID = inpatientNo;
                    //������
                    info.DiagInfo.DiagType.ID = diagnoseTypeHelper.GetID(this.fpEnter1_Sheet1.Cells[i, (int)Cols.diagType].Text);
                    info.DiagInfo.ICD10.ID = fpEnter1_Sheet1.Cells[i, (int)Cols.ICDCode].Text;//2
                    //if (info.DiagInfo.DiagType.ID == "1") //����������ó� 
                    //{

                    //}
                    //else
                    //{
                    //    info.DiagInfo.IsMain = false;
                    //}
                    info.DiagInfo.ICD10.Name = fpEnter1_Sheet1.Cells[i, (int)Cols.ICDName].Text;
                    //if (row["��Ժ���"] != DBNull.Value)
                    //{
                    info.DiagOutState = diagOutStateListHelper.GetID(fpEnter1_Sheet1.Cells[i, (int)Cols.outState].Text); //3
                    //}
                    //if (row["��������"] != DBNull.Value)
                    //{
                    info.OperationFlag = OperListHelper.GetID(fpEnter1_Sheet1.Cells[i, (int)Cols.Operation].Text);
                    //}

                    //if ()//5
                    //{
                    info.Is30Disease = FS.FrameWork.Function.NConvert.ToInt32(ConvertBool(fpEnter1_Sheet1.Cells[i, (int)Cols.disease].Value)).ToString();
                    //}
                    //else
                    //{
                    //    info.Is30Disease = "0";
                    //}
                    //if (ConvertBool(row["�������"]))//6
                    //{
                    info.CLPA = FS.FrameWork.Function.NConvert.ToInt32(ConvertBool(fpEnter1_Sheet1.Cells[i, (int)Cols.clpa].Value)).ToString();
                    //}
                    //else
                    //{
                    //info.CLPA = "0";
                    //}
                    //if (row["�ּ�"] != DBNull.Value)
                    //{
                    info.LevelCode = LeveListHelper.GetID(fpEnter1_Sheet1.Cells[i, (int)Cols.levelCode].Text); //7
                    //}
                    //if (row["����"] != DBNull.Value)
                    //{
                    info.PeriorCode = PeriorListHelper.GetID(fpEnter1_Sheet1.Cells[i, (int)Cols.perionCode].Text);//8
                    //}
                    //if (ConvertBool(row["�Ƿ�����"]))//9
                    //{
                    info.DubDiagFlag = FS.FrameWork.Function.NConvert.ToInt32(ConvertBool(fpEnter1_Sheet1.Cells[i, (int)Cols.dubdiag].Value)).ToString(); ;
                    info.DiagInfo.IsMain = ConvertBool(fpEnter1_Sheet1.Cells[i, (int)Cols.mainDiag].Value);
                    //}
                    //else
                    //{
                    //    info.DubDiagFlag = "0";
                    //}
                    info.DiagInfo.HappenNo = i;
                    info.DiagInfo.DiagDate = FS.FrameWork.Function.NConvert.ToDateTime(fpEnter1_Sheet1.Cells[i, (int)Cols.diagTime].Text);//11
                    info.Pvisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(fpEnter1_Sheet1.Cells[i, (int)Cols.inTime].Text);//12
                    info.Pvisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(fpEnter1_Sheet1.Cells[i, (int)Cols.outTime].Text);//13
                    if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
                    {
                        info.OperType = "1";
                    }
                    else if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
                    {
                        info.OperType = "2";
                    }

                    if (fpEnter1_Sheet1.Cells[i, 0].Tag != null)
                    {
                        FS.HISFC.Models.HealthRecord.Diagnose obj = (FS.HISFC.Models.HealthRecord.Diagnose)fpEnter1_Sheet1.Cells[i, 0].Tag;
                        info.DiagInfo.Doctor = obj.DiagInfo.Doctor;
                    }
                    else
                    {
                        info.DiagInfo.Doctor.ID = deptMgr.Operator.ID;
                        info.DiagInfo.Doctor.Name = deptMgr.Operator.Name;
                    }
                    list.Add(info);
                }
                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        #endregion

        #region ��ʵ��ת����BOOL����
        /// <summary>
        /// ��ʵ��ת����BOOL����
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool ConvertBool(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            return FS.FrameWork.Function.NConvert.ToBoolean(obj);
        }
        #endregion

        #endregion

        public override int Save(object sender, object neuObject)
        {
            if (inpatientNo == null || inpatientNo == "")
            {
                MessageBox.Show("��ѡ����");
                return 0;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase diagNose = new FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase();
            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(deptMgr.Connection);
            //trans.BeginTransaction();
            //diagNose.SetTrans(trans.Trans);

            this.fpEnter1.StopCellEditing();
            ArrayList list = this.GetChangeInfo();
            if (list.Count == 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("û�пɱ������Ϣ");
                return 0;
            }
            if (ValueState(list) == -1)
            {

                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }


            #region ɾ��
            if (diagNose.DeleteDiagnoseAll(this.inpatientNo, frmType, FS.HISFC.Models.Base.ServiceTypes.I) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("���������Ϣʧ��" + diagNose.Err);
                return -1;
            }
            #endregion
            #region ���� ����
            foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in list)
            {
                if (diagNose.InsertDiagnose(obj) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ʧ�� " + diagNose.Err);
                }
            }
            #endregion
            this.fpEnterSaveChanges();
            FS.FrameWork.Management.PublicTrans.Commit();
            ClearInfo();
            LoadInfo(inpatientNo);
            MessageBox.Show("����ɹ�");

            return base.Save(sender, neuObject);
        }
        #endregion

        #region ���ԭ�е�����

        /// <summary>
        /// ���ԭ�е�����

        /// </summary>
        /// <returns></returns>
        private int ClearInfo()
        {
            if (this.fpEnter1_Sheet1.RowCount != 0)
            {
                this.fpEnter1_Sheet1.Rows.Remove(0, this.fpEnter1_Sheet1.RowCount);
                LockFpEnter();
            }
            else
            {
                MessageBox.Show("��ϱ�Ϊnull");
            }
            return 1;
        }
        #endregion

        #region ����ֻ��
        bool readOnly; //�Ƿ�ֻ��
        [Description("�Ƿ�ֻ��")]
        public bool SetReadOnly
        {
            get
            {
                return readOnly;
            }
            set
            {
                readOnly = value;
                if (readOnly)
                {
                    this.fpEnter1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
                }
                else
                {
                    this.fpEnter1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.Normal;
                }

            }
        }
        #endregion

        #region У�����ݵĺϷ���

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
                if (obj.DiagInfo.Patient.ID.Length > 14)
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
                if (obj.DiagInfo.DiagType.ID.Length > 2)
                {
                    MessageBox.Show("�����Ϣ��������ͱ������");
                    return -1;
                }
                if (obj.LevelCode.Length > 3)
                {
                    MessageBox.Show("�����Ϣ����ϼ���������");
                    return -1;
                }
                if (obj.PeriorCode.Length > 3)
                {
                    MessageBox.Show("�����Ϣ����Ϸ��ڱ������");
                    return -1;
                }
                if (obj.DiagInfo.ICD10.ID == "" || obj.DiagInfo.ICD10.ID == null)
                {
                    MessageBox.Show("�����Ϣ��ICD��ϲ���Ϊ��");
                    return -1;
                }
                if (obj.DiagInfo.ICD10.ID.Length > 30)
                {
                    MessageBox.Show("�����Ϣ����ϱ������");
                    return -1;
                }
                if (obj.DiagInfo.ICD10.Name == "" || obj.DiagInfo.ICD10.Name == null)
                {
                    MessageBox.Show("�����Ϣ��ICD��ϲ���Ϊ��");
                    return -1;
                }
                if (obj.DiagInfo.ICD10.Name.Length > 100)
                {
                    MessageBox.Show("�����Ϣ��������ƹ���");
                    return -1;
                }
                if (obj.DiagInfo.Doctor.ID == "" || obj.DiagInfo.Doctor.ID == null)
                {
                    MessageBox.Show("�����Ϣ�����ҽ�����벻��Ϊ��");
                    return -1;
                }
                if (obj.DiagInfo.Doctor.ID.Length > 6)
                {
                    MessageBox.Show("�����Ϣ��ҽ���������");
                    return -1;
                }
                if (obj.DiagInfo.Doctor.Name == "" || obj.DiagInfo.Doctor.Name == null)
                {
                    MessageBox.Show("�����Ϣ�����ҽ������Ϊ��");
                    return -1;
                }
                if (obj.DiagInfo.Doctor.Name.Length > 10)
                {
                    MessageBox.Show("�����Ϣ��ҽ�����ƹ���");
                    return -1;
                }
                if (obj.DiagOutState.Length > 2)
                {
                    MessageBox.Show("�����Ϣ����������������");
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
        #endregion

        #region ����Ա����������޸�

        /// <summary>
        /// ����Ա����������޸�

        /// </summary>
        /// <returns></returns>
        public int fpEnterSaveChanges()
        {
            try
            {
                LockFpEnter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;
        }
        #endregion

        #region ���������סԺ��/����� ��ѯ�����Ϣ
        /// <summary>
        /// ���������סԺ��/����� ��ѯ�����Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="Type"></param>
        /// <returns>-1 ���� 0 ����Ĳ�����ϢΪ��,��������1 �������в�����2�����Ѿ���棬������ҽ���޸ĺͲ��� 3 ��ѯ������ 4��ѯû������  </returns>
        public int LoadInfo(string InpatientNO) //FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes Type)
        {
            try
            {
                if (this.fpEnter1_Sheet1.RowCount > 0)
                {
                    this.fpEnter1_Sheet1.Rows.Remove(0, this.fpEnter1_Sheet1.RowCount);
                }
                if (InpatientNO == null || InpatientNO == "")
                {
                    //û�иò��˵���Ϣ
                    MessageBox.Show("��ѡ����");
                    return 0;
                }
                this.inpatientNo = InpatientNO;
                FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();
                FS.HISFC.BizProcess.Integrate.RADT pa = new FS.HISFC.BizProcess.Integrate.RADT();// FS.HISFC.BizLogic.RADT.InPatient();
                FS.HISFC.BizProcess.Integrate.Registration.Registration register = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
                //��סԺ�����в�Ѯ
                patient = pa.GetPatientInfoByPatientNO(InpatientNO);
                if (patient == null)
                {
                    FS.HISFC.Models.Registration.Register obj = register.GetByClinic(InpatientNO);
                    if (obj == null)
                    {
                        MessageBox.Show("��ѯ������Ϣ����");
                        return -1;
                    }
                    patient = new FS.HISFC.Models.RADT.PatientInfo();
                    patient.ID = obj.ID;
                    patient.CaseState = "1";
                }

                if (patient.CaseState == "0")
                {
                    //�������в���
                    return 1;
                }
                //����ҵ������

                FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase diag = new FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase();
                diagList = new ArrayList();

                if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC) // ҽ��վ¼�벡��
                {
                    #region  ҽ��վ¼�벡��


                    //Ŀǰ�����в��� ����Ŀǰû��¼�벡��  ���߱�־λλ�գ�Ĭ��������¼�벡���� 
                    // ҽ��վ¼�벡��

                    if (patient.CaseState == null || patient.CaseState == "1" || patient.CaseState == "2")
                    {
                        //��ҽ��վ¼�����Ϣ�в�ѯ
                        diagList = diag.QueryCaseDiagnose(patient.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, FS.HISFC.Models.Base.ServiceTypes.I);
                    }
                    else
                    {
                        // �����Ѿ�����Ѿ�������ҽ���޸ĺͲ���
                        return 2;
                    }

                    #endregion
                }
                else if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)//������¼�벡��
                {
                    #region ������¼�벡��

                    //Ŀǰ�����в��� ����Ŀǰû��¼�벡��  ���߱�־λλ�գ�Ĭ��������¼�벡���� 
                    if (patient.CaseState == null || patient.CaseState == "1" || patient.CaseState == "2")
                    {
                        //ҽ��վ�Ѿ�¼�벡��

                        diagList = diag.QueryCaseDiagnose(patient.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, FS.HISFC.Models.Base.ServiceTypes.I);
                    }
                    else if (patient.CaseState == "3")
                    {
                        //�������Ѿ�¼�벡��

                        diagList = diag.QueryCaseDiagnose(patient.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS, FS.HISFC.Models.Base.ServiceTypes.I);
                    }
                    else if (patient.CaseState == "4")
                    {
                        //�����Ѿ���� �������޸ġ�

                        diagList = diag.QueryCaseDiagnose(patient.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS, FS.HISFC.Models.Base.ServiceTypes.I);
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

                    AddInfoToFP(diagList);
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
        #endregion

        #region �������
        /// <summary>
        /// ��ѯ�����Ϣ�������ı���
        /// </summary>
        private void AddInfoToFP(ArrayList alReturn)
        {
            bool Result = false;
            if ((this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC && this.patient.CaseState == "2") || (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS && this.patient.CaseState == "3"))
            {
                Result = true;
            }
            //�����ǰ������

            if (this.fpEnter1_Sheet1.RowCount > 0)
            {
                this.fpEnter1_Sheet1.Rows.Remove(0, this.fpEnter1_Sheet1.Rows.Count);
            }
            //ѭ��������Ϣ
            foreach (FS.HISFC.Models.HealthRecord.Diagnose info in alReturn)
            {
                this.fpEnter1_Sheet1.Rows.Add(0, 1);
                this.fpEnter1_Sheet1.Cells[0, (int)Cols.diagType].Text = diagnoseTypeHelper.GetName(info.DiagInfo.DiagType.ID); //0
                this.fpEnter1_Sheet1.Cells[0, (int)Cols.ICDName].Text = info.DiagInfo.ICD10.Name;//1
                this.fpEnter1_Sheet1.Cells[0, (int)Cols.ICDCode].Text = info.DiagInfo.ICD10.ID;//2
                this.fpEnter1_Sheet1.Cells[0, (int)Cols.outState].Text = diagOutStateListHelper.GetName(info.DiagOutState); //3
                this.fpEnter1_Sheet1.Cells[0, (int)Cols.Operation].Text = OperListHelper.GetName(info.OperationFlag);
                this.fpEnter1_Sheet1.Cells[0, (int)Cols.disease].Value = FS.FrameWork.Function.NConvert.ToBoolean(info.Is30Disease);
                this.fpEnter1_Sheet1.Cells[0, (int)Cols.clpa].Value = FS.FrameWork.Function.NConvert.ToBoolean(info.CLPA);
                this.fpEnter1_Sheet1.Cells[0, (int)Cols.levelCode].Text = LeveListHelper.GetName(info.LevelCode); //7
                this.fpEnter1_Sheet1.Cells[0, (int)Cols.perionCode].Text = PeriorListHelper.GetName(info.PeriorCode);//8
                this.fpEnter1_Sheet1.Cells[0, (int)Cols.dubdiag].Value = FS.FrameWork.Function.NConvert.ToBoolean(info.DubDiagFlag);
                this.fpEnter1_Sheet1.Cells[0, (int)Cols.mainDiag].Value = info.DiagInfo.IsMain;
                this.fpEnter1_Sheet1.Cells[0, (int)Cols.happenNo].Text = info.DiagInfo.HappenNo.ToString();
                this.fpEnter1_Sheet1.Cells[0, (int)Cols.diagTime].Text = info.DiagInfo.DiagDate.ToString();
                this.fpEnter1_Sheet1.Cells[0, (int)Cols.inTime].Text = patient.PVisit.InTime.ToString();
                this.fpEnter1_Sheet1.Cells[0, (int)Cols.outTime].Text = patient.PVisit.OutTime.ToString();
                this.fpEnter1_Sheet1.Cells[0, (int)Cols.diagDocCode].Text = info.DiagInfo.Doctor.ID;
                this.fpEnter1_Sheet1.Cells[0, (int)Cols.diagDocName].Text = info.DiagInfo.Doctor.Name;
                this.fpEnter1_Sheet1.Cells[0, 0].Tag = info;
            }
            LockFpEnter();
        }
        #endregion

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
            InitInfo();
            this.ucDiagnose1.Init();
            this.ucDiagnose1.SelectItem += new FS.HISFC.Components.Common.Controls.ucDiagnose.MyDelegate(ucDiagnose1_SelectItem);
            this.ucDiagnose1.Visible = false;
        }

        #region  ��ѡ����Ŀ�Ĵ����¼�
        #region ����س����� ������ȡ������

        /// <summary>
        /// ����س����� ������ȡ������

        /// </summary>
        /// <returns></returns>
        private int ProcessDept()
        {
            int CurrentRow = fpEnter1_Sheet1.ActiveRowIndex;
            if (CurrentRow < 0) return 0;

            if (fpEnter1_Sheet1.ActiveColumnIndex == (int)EnumCol.DiagKind) //������� 
            {
                //��ȡѡ�е���Ϣ

                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)EnumCol.DiagKind);
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                //������
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)EnumCol.Icd10Code);
                return 0;
            }
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)EnumCol.OutState)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)EnumCol.OutState);
                //��ȡѡ�е���Ϣ

                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                // ��Ժ��Ϣ
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)EnumCol.OperationFlag);
                return 0;
            }
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)EnumCol.OperationFlag)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)EnumCol.OperationFlag);
                //��ȡѡ�е���Ϣ

                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                // ��Ժ��Ϣ
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)EnumCol.Disease);
                return 0;
            }
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)EnumCol.Perior)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)EnumCol.Perior);
                //��ȡѡ�е���Ϣ

                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                //����
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)EnumCol.Level);
                return 0;
            }
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)EnumCol.Level)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)EnumCol.Level);
                //��ȡѡ�е���Ϣ

                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                //����
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)EnumCol.DubDiag);
                return 0;
            }

            return 0;
        }

        private int fpEnter1_SetItem(FS.FrameWork.Models.NeuObject obj)
        {
            this.ProcessDept();
            return 0;
        }
        #endregion
        #endregion

        #region ������Ӧ����
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
                    if (i == (int)EnumCol.DiagKind || i == (int)EnumCol.OutState || i == (int)EnumCol.OperationFlag || i == (int)EnumCol.Perior || i == (int)EnumCol.Level)
                    {
                        ProcessDept();
                    }
                    else if (i == (int)EnumCol.DubDiag)
                    {
                        if (fpEnter1_Sheet1.ActiveRowIndex < fpEnter1_Sheet1.Rows.Count - 1)
                        {
                            fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex + 1, 0);
                        }
                        else
                        {
                            //if (this.Tag != null)
                            //{
                            //    this.AddBlankRow(); //����һ���հ��� 
                            //}
                            //else
                            //{
                            //����һ��

                            this.AddRow();
                            //}
                        }
                    }
                    else
                    {
                        if (i < (int)EnumCol.DubDiag)
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
                if (fpEnter1_Sheet1.Rows.Count == 0 || fpEnter1_Sheet1.ActiveColumnIndex == (int)EnumCol.DubDiag)
                {
                    AddRow();
                }
            }
            return 0;
        }
        #endregion

        #region �޶���Ŀ�ȺͿɼ���

        /// <summary>
        /// �޶���Ŀ�ȺͿɼ��� 
        /// </summary>
        private void LockFpEnter()
        {
            //this.fpEnter1_Sheet1.Columns[(int)Cols.diagType].Width = 59; //������
            //this.fpEnter1_Sheet1.Columns[(int)Cols.ICDCode].Width = 124;//ICD10
            this.fpEnter1_Sheet1.Columns[(int)Cols.ICDName].Locked = true;
            //this.fpEnter1_Sheet1.Columns[(int)Cols.ICDName].Width = 150;//�������
            //this.fpEnter1_Sheet1.Columns[(int)Cols.outState].Width = 65; //��Ժ���
            //this.fpEnter1_Sheet1.Columns[(int)Cols.Operation].Width = 40; //��������
            this.fpEnter1_Sheet1.Columns[(int)Cols.disease].CellType = checkCellType; //30�ּ���

            this.fpEnter1_Sheet1.Columns[(int)Cols.clpa].CellType = checkCellType; //�������
            //this.fpEnter1_Sheet1.Columns[(int)Cols.perionCode].Width = 51; //����
            //this.fpEnter1_Sheet1.Columns[(int)Cols.levelCode].Width = 50; //�ּ�
            this.fpEnter1_Sheet1.Columns[(int)Cols.dubdiag].CellType = checkCellType; //�Ƿ�����
            this.fpEnter1_Sheet1.Columns[(int)Cols.mainDiag].CellType = checkCellType; //�����

            //this.fpEnter1_Sheet1.Columns[(int)Cols.mainDiag].Visible = false; //�����

            this.fpEnter1_Sheet1.Columns[(int)Cols.happenNo].Visible = false; //���
            this.fpEnter1_Sheet1.Columns[(int)Cols.diagTime].Visible = false; //�������
            this.fpEnter1_Sheet1.Columns[(int)Cols.inTime].Visible = false; //��Ժ����
            this.fpEnter1_Sheet1.Columns[(int)Cols.outTime].Visible = false; //��Ժ����
            this.fpEnter1_Sheet1.Columns[(int)Cols.diagDocCode].Visible = false; //���ҽʦ����
            this.fpEnter1_Sheet1.Columns[(int)Cols.diagDocName].Visible = false; //���ҽʦ
        }
        #endregion

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.NumPad1)
            {
                int i = fpEnter1_Sheet1.ActiveColumnIndex;
                if (i == (int)EnumCol.Disease || i == (int)EnumCol.CLPa || i == (int)EnumCol.DubDiag || i == (int)EnumCol.MainDiag)
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
                if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)EnumCol.Icd10Code)
                {
                    this.ucDiagnose1.PriorRow();
                }
            }
            else if (keyData.GetHashCode() == Keys.Down.GetHashCode())
            {
                if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)EnumCol.Icd10Code)
                {
                    this.ucDiagnose1.NextRow();
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        #region ����Ԫ���е����ݱ仯ʱ����

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
                    this.ucDiagnose1.Filter(fpEnter1_Sheet1.ActiveCell.Text, false);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region ѡ�����

        #region ��ȡ���
        private int GetInfo()
        {
            try
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
                //ICD�������
                fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)EnumCol.Icd10Code].Text = item.ID;
                //ICD��ϱ���
                fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)EnumCol.Icd10Name].Text = item.Name;
                ucDiagnose1.Visible = false;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)EnumCol.OutState);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }
        #endregion

        #region ���㵽��Ԫ��

        /// <summary>
        /// ���㵽��Ԫ�� 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpEnter1_EditModeOn(object sender, System.EventArgs e)
        {
            if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)EnumCol.Icd10Code)
            {
                Control _cell = fpEnter1.EditingControl;
                //����λ��
                this.ucDiagnose1.Location = new System.Drawing.Point(_cell.Location.X, _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                ucDiagnose1.BringToFront();
                this.ucDiagnose1.Filter(fpEnter1_Sheet1.ActiveCell.Text, false);
                this.ucDiagnose1.Visible = true;
            }
            else
            {
                this.ucDiagnose1.Visible = false;
            }

            //��ʾ
        }

        private int ucDiagnose1_SelectItem(Keys key)
        {
            GetInfo();
            return 0;
        }
        #endregion

        #region �س��¼�
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
        #endregion

        #endregion

        #region  �е�ö��
        private enum EnumCol
        {
            DiagKind = 0, //������
            Icd10Code = 1, //ICD10 ���� 
            Icd10Name = 2,//ICD10 ����
            OutState = 3, //��Ժ���
            OperationFlag = 4, //����
            Disease = 5, //30�ּ���

            CLPa = 6,//�������
            Perior = 7,//����
            Level = 8,//�ּ�
            DubDiag = 9,//�Ƿ�����
            MainDiag = 10//�����

        }
        #endregion

        #region ����
        /// <summary>
        /// ����һ���հ��� 
        /// </summary>
        /// <returns></returns>
        [Obsolete("���� ", true)]
        public void AddBlankRow()
        {
            this.fpEnter1_Sheet1.Rows.Add(this.fpEnter1_Sheet1.RowCount, 1);
        }

        /// <summary>
        /// �Ƿ��в���֢
        /// </summary>
        /// <returns></returns>
        [Obsolete("����", true)]
        public string GetSyndromeFlag()
        {
            string str = "0";
            if (fpEnter1_Sheet1.RowCount == 0)
            {
                return "0";
            }
            for (int i = 0; i < fpEnter1_Sheet1.RowCount; i++)
            {
                if (fpEnter1_Sheet1.Cells[i, 0].Text == str)
                {
                    str = "1";
                    break;
                }
            }
            return str;
        }

        /// <summary>
        /// Ժ�ڸ�Ⱦ���� 
        /// </summary>
        /// <returns></returns>
        [Obsolete(" ���� ", true)]
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
        /// ɾ���հ׵���
        /// </summary>
        /// <returns></returns>
        public int deleteRow()
        {
            if (fpEnter1_Sheet1.Rows.Count == 1)
            {
                //��һ�б���Ϊ�� 
                if (fpEnter1_Sheet1.Cells[0, 1].Text == "")
                {
                    fpEnter1_Sheet1.Rows.Remove(0, 1);
                }
            }
            return 1;
        }

        /// <summary>
        /// ���ص�ǰ����
        /// </summary>
        /// <returns></returns>
        [Obsolete("����")]
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
        [Obsolete("����", true)]
        public bool Reset(bool reset)
        {
            if (reset)
            {
                //������� ������� 
                //if (dtDiagnose != null)
                //{
                //    dtDiagnose.Clear();
                //    dtDiagnose.AcceptChanges();
                //}
            }
            else
            {
                //�������
                //dtDiagnose.AcceptChanges();
            }
            LockFpEnter();
            return true;
        }
        /// <summary>
        /// ���û��Ԫ��

        /// </summary>
        [Obsolete("����", true)]
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
        /// ������������ݻ�д������
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [Obsolete("����", true)]
        public int fpEnterSaveChanges(ArrayList list)
        {
            AddInfoToFP(list);
            this.LockFpEnter();
            return 0;
        }

        #region ��ȡ���е������Ϣ
        /// <summary>
        /// ��ȡ���е������Ϣ
        /// </summary>
        /// <returns></returns>
        [Obsolete("����", true)]
        public int GetAllDiagnose(ArrayList list)
        {
            //GetChangeInfo(dtDiagnose, list);
            return 1;
        }
        #endregion

        
        #endregion
        private void btAdd_Click(object sender, EventArgs e)
        {
            this.AddRow();
        }
        private void btDelete_Click(object sender, EventArgs e)
        {
            DeleteActiveRow();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            Save(new object(), new object());
        }
    }
}
