using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FarPoint.Win.Spread;
using FS.HISFC.Models.HealthRecord.EnumServer;
namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    /// <summary>
    /// ucOperation<br></br>
    /// [��������: ����������Ϣ¼��]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-04-20]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucOperation : UserControl
    {
        public ucOperation()
        {
            InitializeComponent();
        }

        #region �ؼ����˼��
        //����LoadInfo ����ѯ������Ϣ��������� 
        //����GetList ��ȡ���� ���ⲿ����
        //�ṩ Reset���������ⲿ������Ϻ� ������е�����  
        #endregion

        #region   ȫ�ֱ���
        //�����ļ�·�� 
        private string filePath = Application.StartupPath + "\\profile\\OperationCard.xml";
        //����� "DOC" ��ѯ����ҽ��վ¼���������Ϣ ���������ǡ�CAS�������ѯ����ʦ¼���������Ϣ
        private FS.HISFC.Models.HealthRecord.EnumServer.frmTypes operType;
        //���Ʒ���Ӥ����¼�� 
        private DataTable dtOperation;
        private DataView dvOperation;
        /// <summary>
        ///ICD �����Ϣ �б�
        /// </summary>
        private FS.FrameWork.WinForms.Controls.PopUpListBox ICDType = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        //�п�����
        private FS.FrameWork.WinForms.Controls.PopUpListBox NickType = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        private FS.FrameWork.Public.ObjectHelper NickTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        //��������
        private FS.FrameWork.WinForms.Controls.PopUpListBox CicaType = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        private FS.FrameWork.Public.ObjectHelper CicaTypeHelper = new FS.FrameWork.Public.ObjectHelper();
        //����ʽ�б�
        private FS.FrameWork.WinForms.Controls.PopUpListBox NarcType = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        private FS.FrameWork.Public.ObjectHelper NarcTypeHelper = new FS.FrameWork.Public.ObjectHelper();
        //����/����ҽ���б�
        private FS.FrameWork.WinForms.Controls.PopUpListBox DoctorType = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        private FS.FrameWork.Public.ObjectHelper DoctorHelper = new FS.FrameWork.Public.ObjectHelper();

        private FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();

        private int operationType = 0;

        //����ICD��
        static ArrayList icdList = new ArrayList();
        #endregion

        #region ����
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
        #endregion

        #region ����
        private void SetAllListUnVisiable()
        {
            NickType.Visible = false;
            NarcType.Visible = false;
            CicaType.Visible = false;
            DoctorType.Visible = false;
            ICDType.Visible = false;
        }
        /// <summary>
        /// ���û��Ԫ��
        /// </summary>
        public void SetActiveCells()
        {
            try
            {
                this.fpSpread1_Sheet1.SetActiveCell(0, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// �޶���Ŀ�Ⱥܿɼ��� 
        /// </summary>
        private void LockFpEnter()
        {
            this.fpSpread1_Sheet1.Columns[0].Width = 76; //��������
            //this.fpSpread1_Sheet1.Columns[0].Locked = true;//��������
            this.fpSpread1_Sheet1.Columns[1].Width = 85; //ICD-9-CM-3���
            //this.fpSpread1_Sheet1.Columns[1].Locked = true; //ICD-9-CM-3���
            //this.fpSpread1_Sheet1.Columns[1].Visible = false;

            this.fpSpread1_Sheet1.Columns[2].Width = 200;//��������
            //this.fpSpread1_Sheet1.Columns[1].Locked = true;//��������
            this.fpSpread1_Sheet1.Columns[3].Width = 40;//���� A
            //this.fpSpread1_Sheet1.Columns[2].Locked = true;//���� A
            this.fpSpread1_Sheet1.Columns[4].Width = 40; //���� B
            //this.fpSpread1_Sheet1.Columns[3].Locked = true;//���� B
            this.fpSpread1_Sheet1.Columns[4].Visible = false;
            this.fpSpread1_Sheet1.Columns[5].Width = 40; //I ��
            //this.fpSpread1_Sheet1.Columns[4].Locked = true;//I ��
            this.fpSpread1_Sheet1.Columns[6].Width = 45; //II ��
            //this.fpSpread1_Sheet1.Columns[5].Locked = true;//II ��
            this.fpSpread1_Sheet1.Columns[7].Width = 40; //����ʽ
            this.fpSpread1_Sheet1.Columns[7].Locked = true;//����ʽ
            this.fpSpread1_Sheet1.Columns[7].Visible = false;
            this.fpSpread1_Sheet1.Columns[8].Width = 60; //�п����ϵȼ�
            this.fpSpread1_Sheet1.Columns[8].Locked = true;//�п����ϵȼ�
            this.fpSpread1_Sheet1.Columns[8].Visible = false;
            this.fpSpread1_Sheet1.Columns[9].Width = 55; //����ҽʦ
            //this.fpSpread1_Sheet1.Columns[8].Locked = true;//����ҽʦ
            this.fpSpread1_Sheet1.Columns[10].Width = 50; //�п�
            //this.fpSpread1_Sheet1.Columns[10].Locked = true;//�п�
            this.fpSpread1_Sheet1.Columns[11].Width = 50; //����
            this.fpSpread1_Sheet1.Columns[12].Width = 40; //ͳ��
            this.fpSpread1_Sheet1.Columns[12].Locked = true;//ͳ��
            this.fpSpread1_Sheet1.Columns[12].Visible = false;
            this.fpSpread1_Sheet1.Columns[13].Visible = false; //ҽʦ����1
            this.fpSpread1_Sheet1.Columns[14].Visible = false; //ҽʦ����2
            this.fpSpread1_Sheet1.Columns[15].Visible = false; //���ֱ���1
            this.fpSpread1_Sheet1.Columns[16].Visible = false; //���ֱ���2
            this.fpSpread1_Sheet1.Columns[17].Visible = false; //����ҽʦ����
            this.fpSpread1_Sheet1.Columns[18].Visible = false; //�������
            this.fpSpread1_Sheet1.Columns[19].Visible = false; //�������к�
            this.fpSpread1_Sheet1.Columns[20].Width = 60;//����ʽ1
            this.fpSpread1_Sheet1.Columns[21].Width = 60;//����ʽ2
        }
        /// <summary>
        /// ���ԭ�е�����
        /// </summary>
        /// <returns></returns>
        public int ClearInfo()
        {
            if (this.dtOperation != null)
            {
                this.dtOperation.Clear();
                LockFpEnter();
            }
            else
            {
                MessageBox.Show("������Ϊnull");
            }
            return 1;
        }
        public int SetReadOnly(bool type)
        {
            if (type)
            {
                this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            }
            else
            {
                this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.Normal;
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
            foreach (FS.HISFC.Models.HealthRecord.OperationDetail obj in list)
            {
                if (obj.InpatientNO == "" || obj.InpatientNO == null)
                {
                    MessageBox.Show("סԺ��ˮ�Ų���Ϊ��");
                    return -1;
                }
                if (obj.OperationInfo.ID == "" || obj.OperationInfo.Name == "")
                {
                    MessageBox.Show("������Ϣ����Ϊ��");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.InpatientNO , 14))
                {
                    MessageBox.Show("סԺ��ˮ�Ź���");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.HappenNO , 2))
                {
                    MessageBox.Show("������Ź���");
                    return -1;
                }
                if (obj.OperType == "" || obj.OperType == null)
                {
                    MessageBox.Show("�����Ϊ��");
                    return -1;
                }
                if (obj.OperType.Length > 1)
                {
                    MessageBox.Show("���������");
                    return -1;
                }
                if (obj.OperationDate.Date < this.patient.PVisit.InTime.Date)
                {
                    MessageBox.Show("�������ڲ���С����Ժ���ڣ�");
                    return -1;
                }
                if (this.patient.PVisit.InState.ID.ToString()!="I" && obj.OperationDate.Date > this.patient.PVisit.OutTime.Date)
                {
                    MessageBox.Show("�������ڲ��ܴ��ڳ�Ժ���ڣ�");
                    return -1;
                }
                //�����ƴ��ڣ��幬���ȣ�û���пڣ�û�������������
                //if (obj.OperationInfo.Name != "" && (obj.NickKind == null || obj.NickKind==""))
                //{
                //    MessageBox.Show("����д�������п���𡱣�");
                //    return -1;
                //}
                //if (obj.OperationInfo.Name != "" && (obj.CicaKind == null || obj.CicaKind == ""))
                //{
                //    MessageBox.Show("����д���������ϵȼ�����");
                //    return -1;
                //}
                //if (obj.OperationInfo.Name != "" && (obj.MarcKind == null || obj.MarcKind == ""))
                //{
                //    MessageBox.Show("����д����������ʽ1����");
                //    return -1;
                //}
                //if ((obj.MarcKind != null || obj.MarcKind != "") && (obj.NarcDoctInfo.Name == null || obj.NarcDoctInfo.Name == ""))
                //{
                //    MessageBox.Show("����д����������ҽʦ����");
                //    return -1;
                //}
            }
            return 0;
        }
        /// <summary>
        /// ɾ����ǰ�� 
        /// </summary>
        /// <returns></returns>
        public int DeleteActiveRow()
        {
            if (this.operationType == 1) //�Զ�������Ŀ����ɾ��
            {
                return -1;
            }
            SetAllListUnVisiable();
            this.fpSpread1.EditModePermanent = false;
            this.fpSpread1.EditModeReplace = false;
            if (fpSpread1_Sheet1.Rows.Count > 0)
            {
                this.fpSpread1_Sheet1.Rows.Remove(fpSpread1_Sheet1.ActiveRowIndex, 1);
            }
            if (fpSpread1_Sheet1.Rows.Count == 0)
            {
                ICDType.Visible = false;
                NickType.Visible = false;
                CicaType.Visible = false;
                NarcType.Visible = false;
                DoctorType.Visible = false;
            }
            this.fpSpread1.EditModePermanent = true;
            this.fpSpread1.EditModeReplace = true;
            return 1;
        }
        /// <summary>
        /// ɾ���հ׵���
        /// </summary>
        /// <returns></returns>
        public int deleteRow()
        {
            SetAllListUnVisiable();
            this.fpSpread1.EditModePermanent = false;
            this.fpSpread1.EditModeReplace = false;
            if (fpSpread1_Sheet1.Rows.Count == 1)
            {
                //��һ����������������Ϊ�� 
                if (fpSpread1_Sheet1.Cells[0, 2].Text == "")
                {
                    fpSpread1_Sheet1.Rows.Remove(0, 1);
                }
            }
            this.fpSpread1.EditModePermanent = true;
            this.fpSpread1.EditModeReplace = true;
            return 1;
        }
        /// <summary>
        /// ����Ա����������޸�
        /// </summary>
        /// <returns></returns>
        public int fpEnterSaveChanges()
        {
            try
            {
                this.dtOperation.AcceptChanges();
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
        /// ������������ݻ�д������
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int fpEnterSaveChanges(ArrayList list)
        {
            AddInfoToTable(list);
            dtOperation.AcceptChanges();
            LockFpEnter();
            return 0;
        }
        public int AddInfoToTable(ArrayList list)
        {
            if (this.dtOperation != null)
            {
                this.dtOperation.Clear();
                this.dtOperation.AcceptChanges();
            }
            if (list != null)
            {
                //ѭ����������
                foreach (FS.HISFC.Models.HealthRecord.OperationDetail info in list)
                {
                    DataRow row = dtOperation.NewRow();
                    SetRow(row, info);
                    dtOperation.Rows.Add(row);
                }
            }
            else
            {
                return -1;
            }
            //���ı�־
            if ((this.operType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC && this.patient.CaseState == "2") || (this.operType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS && this.patient.CaseState == "3"))
            {
                //��ձ�ı�־λ
                dtOperation.AcceptChanges();
            }

            //			if(System.IO.File.Exists(filePath))
            //			{
            //				FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1,filePath);
            //			}
            LockFpEnter();
            return 0;
        }
        /// <summary>
        /// ���ص�ǰ������
        /// </summary>
        /// <returns></returns>
        public int GetfpSpread1RowCount()
        {
            return this.fpSpread1_Sheet1.Rows.Count;
        }
        //���һ����Ŀ
        public int AddRow()
        {
            //			DialogResult result = MessageBox.Show("�Ƿ�Ҫ����һ��","��ʾ",MessageBoxButtons.YesNo);
            //			if(result == DialogResult.No)
            //			{
            //				return 0 ;
            //			}
            if (fpSpread1_Sheet1.Rows.Count < 1)
            {
                //����һ�п�ֵ
                DataRow row = dtOperation.NewRow();
                dtOperation.Rows.Add(row);
                //�п����ϵǼ�
                fpSpread1_Sheet1.Cells[0, 8].Text = "/";
                fpSpread1_Sheet1.Cells[0, 0].Value = System.DateTime.Now;
            }
            else if (fpSpread1_Sheet1.ActiveRowIndex == fpSpread1_Sheet1.Rows.Count - 1)
            {
                //����һ��
                int row = fpSpread1_Sheet1.ActiveRowIndex;
                int col = fpSpread1_Sheet1.Columns.Count;
                fpSpread1_Sheet1.Rows.Add(fpSpread1_Sheet1.Rows.Count, 1);
                for (int i = 0; i < col; i++)
                {
                    fpSpread1_Sheet1.Cells[row + 1, i].Value = fpSpread1_Sheet1.Cells[row, i].Value;
                }
            }
            fpSpread1.Focus();
            fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.Rows.Count, 0);
            return 0;
        }
        /// <summary>
        /// ��ʼ�� ����
        /// </summary>
        public void InitInfo()
        {
            try
            {
                InputMap im;
                im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                fpSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;



                InitDataTable();
                //�п�����
                IniNickType();
                //��������
                IniCicaType();
                //����ʽ
                InitNarcList();
                //ҽ���б�
                InitDoctorList();

                //InitControlParam(); //�Զ���ȡ������Ϣ���Ʋ��� 

                if (this.operationType == 1)
                {
                    this.menuItem2.Visible = false;
                }
                fpSpread1.EditModePermanent = true;
                fpSpread1.EditModeReplace = true;
                fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.Normal;
                LockFpEnter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ��ʼ�����Ʋ���
        /// </summary>
        private void InitControlParam()
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            operationType = ctrlParamIntegrate.GetControlParam<int>("CASE02", true, 1);
        }

        /// <summary>
        /// ��������
        /// </summary>
        public void LoadInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes Type)
        {
            if (dtOperation == null)
            {
                return;
            }
            if (patientInfo == null)
            {
                return;
            }
            //���没����Ϣ
            patient = patientInfo;
            //��ֵ��������
            operType = Type;
            FS.HISFC.BizLogic.HealthRecord.Operation op = new FS.HISFC.BizLogic.HealthRecord.Operation();
            if (patient.ID == "")
            {
                return;
            }
            //��ѯ��������������

            ArrayList list = op.QueryOperation(operType, patient.ID);

            if (list == null)
            {
                MessageBox.Show("��ѯ������Ϣ����!");
                return;
            }
            if (list.Count == 0)
            {
                if (operType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
                {
                    list = op.QueryOperation(FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, patient.ID);
                }
            }
            this.AddInfoToTable(list);

            //������ϵͳ�м���������Ϣ
            list = op.QueryOperation(patient.ID); 

            if (list != null)
            {
                //ѭ����������
                foreach (FS.HISFC.Models.HealthRecord.OperationDetail info in list)
                {
                    DataRow row = dtOperation.NewRow();
                    SetRow(row, info);
                    dtOperation.Rows.Add(row);
                }
            }

            LockFpEnter();
        }

        /// <summary>
        /// ��ȡ��ص�����
        /// creator:zhangjunyi@FS.com
        /// </summary>
        /// <param name="str"> ��A������ ��M�� �޸� ��D��ɾ��</param>
        /// <returns>ʧ�ܷ��� false </returns>
        public bool GetList(string str, ArrayList list)
        {
            try
            {
                if (dtOperation == null)
                {
                    list = null;
                    return false;
                }
                this.fpSpread1.StopCellEditing();
                foreach (DataRow dr in this.dtOperation.Rows)
                {
                    dr.EndEdit();
                }
                switch (str)
                {
                    case "A":
                        //��ȡ�����ӵ�����
                        DataTable AddTable = dtOperation.GetChanges(DataRowState.Added);
                        //��ȡ����
                        GetChange(AddTable, list);
                        break;
                    case "M":
                        //��ȡ�޸Ĺ�������
                        DataTable ModTable = dtOperation.GetChanges(DataRowState.Modified);
                        //��ȡ����
                        GetChange(ModTable, list);
                        break;
                    case "D":
                        //��ȡ�޸Ĺ�������
                        DataTable DelTable = dtOperation.GetChanges(DataRowState.Deleted);
                        if (DelTable != null)
                        {
                            DelTable.RejectChanges();
                        }
                        //��ȡ����
                        GetChange(DelTable, list);
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                list = null;
                return false;
            }
        }

        /// <summary>
        /// ��ȡ�޸Ĺ������� 
        /// </summary>
        /// <param name="table">Ҫ��ȡ���ݵ�Table</param>
        /// <param name="list"> ���������</param>
        /// <returns>ʧ�ܷ���false ,�����鷵��null �ɹ����� null</returns>
        private bool GetChange(DataTable table, ArrayList list)
        {
            try
            {
                if (table == null)
                {
                    return false;
                }
                FS.HISFC.Models.HealthRecord.OperationDetail bb;
                foreach (DataRow row in table.Rows)
                {
                    bb = new FS.HISFC.Models.HealthRecord.OperationDetail();
                    //�������� ҽ��վ¼��Ļ򲡰���¼���
                    if (operType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
                    {
                        bb.OperType = "1";
                    }
                    else if (operType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
                    {
                        bb.OperType = "2";
                    }
                    bb.InpatientNO = patient.ID;
                    bb.OperationDate = FS.FrameWork.Function.NConvert.ToDateTime(row["��������"]);
                    bb.OperationInfo.ID = row["ICD-9-CM-3���"].ToString();
                    bb.OperationInfo.Name = row["��������"].ToString();
                    bb.FirDoctInfo.Name = row["���� A"].ToString();
                    bb.FourDoctInfo.Name = row["���� B"].ToString();
                    bb.SecDoctInfo.Name = row["I ��"].ToString();
                    bb.ThrDoctInfo.Name = row["II ��"].ToString();
                    if (row["����ʽ"].ToString() != "")
                    {
                        bb.MarcKind = NarcTypeHelper.GetID(row["����ʽ"].ToString());
                    }
                    bb.NarcDoctInfo.Name = row["����ҽʦ"].ToString();

                    if (row["�п�"].ToString() != "")
                    {
                        bb.NickKind = NickTypeHelper.GetID(row["�п�"].ToString());
                    }
                    if (row["����"].ToString() != "")
                    {
                        bb.CicaKind = CicaTypeHelper.GetID(row["����"].ToString());
                    }
                    if (row["ͳ��"] != DBNull.Value)
                    {
                        if (Convert.ToBoolean(row["ͳ��"]))
                        {
                            bb.StatFlag = "0";
                        }
                        else
                        {
                            bb.StatFlag = "1";
                        }
                    }
                    else
                    {
                        bb.StatFlag = "1";
                    }
                    bb.FirDoctInfo.ID = row["ҽʦ����1"].ToString();
                    bb.FourDoctInfo.ID = row["ҽʦ����2"].ToString();
                    bb.SecDoctInfo.ID = row["���ֱ���1"].ToString();
                    bb.ThrDoctInfo.ID = row["���ֱ���2"].ToString();
                    bb.NarcDoctInfo.ID = row["����ҽʦ����"].ToString();
                    bb.HappenNO = row["�������"].ToString();
                    bb.OperationNO = row["�������к�"].ToString();
                    bb.OutDate = this.patient.PVisit.OutTime;//��Ժ����
                    bb.InDate = patient.PVisit.InTime; //��Ժ���� 
                    bb.DeatDate = patient.DeathTime; //����ʱ�� 
                    bb.OperationDeptInfo.ID = ""; //��������
                    bb.OutDeptInfo.ID = patient.PVisit.PatientLocation.ID; //��Ժ����
                    //					bb.OperDate = dateTime;
                    list.Add(bb);
                }
                return true;
            }
            catch (Exception ex)
            {
                list = null;
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// ��ʼ��table 
        /// </summary>
        /// <returns></returns>
        private bool InitDataTable()
        {
            try
            {
                dtOperation = new DataTable("������Ϣ��¼��");
                dvOperation = new DataView(dtOperation);
                Type strType = typeof(System.String);
                Type intType = typeof(System.Int32);
                Type dtType = typeof(System.DateTime);
                Type boolType = typeof(System.Boolean);
                Type floatType = typeof(System.Single);

                dtOperation.Columns.AddRange(new DataColumn[]{
																 new DataColumn("��������", dtType),  //0
                                                                 new DataColumn("ICD-9-CM-3���", strType),//9
																 new DataColumn("��������", strType), //1
																 new DataColumn("���� A", strType),//2
																 new DataColumn("���� B", strType),//3
																 new DataColumn("I ��", strType),//4
																 new DataColumn("II ��", strType),//5
																 new DataColumn("����ʽ", strType), //6
																 new DataColumn("�п����ϵȼ�", strType),//7
																 new DataColumn("����ҽʦ", strType),//8

																 new DataColumn("�п�", strType),//10
																 new DataColumn("����", strType),//11
																 new DataColumn("ͳ��", boolType),//12
																 new DataColumn("ҽʦ����1", strType),//13
																 new DataColumn("ҽʦ����2", strType),//14
																 new DataColumn("���ֱ���1", strType),//15
																 new DataColumn("���ֱ���2", strType),//16
																 new DataColumn("����ҽʦ����", strType),//17
																 new DataColumn("�������", strType),//18
                                                                 new DataColumn("�������к�", strType),//19
                                                                 new DataColumn("����ʽ1",strType),//20
                                                                 new DataColumn("����ʽ2",strType)});//21

                //				//��������Ϊ�����
                //				CreateKeys(dtOperation);

                this.fpSpread1.DataSource = dvOperation;
                fpSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
                //����fpSpread1 ������
                //				if(System.IO.File.Exists(filePath))
                //				{
                //					FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1,filePath);
                //				}
                //				else
                //				{
                //					FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1,filePath);
                //				}
                return true;
            }
            catch { return false; }
        }
        /// <summary>
        /// ��ʵ���е�ֵ��ֵ��row��
        /// </summary>
        /// <param name="row">�����row</param>
        /// <param name="info">�����ʵ��</param>
        private void SetRow(DataRow row, FS.HISFC.Models.HealthRecord.OperationDetail info)
        {
            if (info.OperationDate.Date <= FS.FrameWork.Function.NConvert.ToDateTime("2011-10-09 23:00:00"))//�����˳���
            {
                FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
                //�ӳ������л�ȡ��������
                ArrayList list =  con.GetList("ANESTYPE");
                if (list != null)
                {
                    //���������
                    NarcType.AddItems(list);
                    NarcTypeHelper.ArrayObject = list;
                }
                NarcType.SelectItem -= new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(diagType_SelectItem);
                NarcType.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(diagType_SelectItem);
            }
            
            row["��������"] = info.OperationDate;
            if (info.OperationInfo.ID != "MS999")
            {
                row["ICD-9-CM-3���"] = info.OperationInfo.ID;
            }
            row["��������"] = info.OperationInfo.Name;
            row["���� A"] = info.FirDoctInfo.Name;
            row["���� B"] = info.FourDoctInfo.Name;
            row["I ��"] = info.SecDoctInfo.Name;
            row["II ��"] = info.ThrDoctInfo.Name;
            if (info.MarcKind != "")
            {
                row["����ʽ"] = NarcTypeHelper.GetName(info.MarcKind);
            }
            row["�п����ϵȼ�"] = NickTypeHelper.GetName(info.NickKind) + "/" + CicaTypeHelper.GetName(info.CicaKind);
            row["����ҽʦ"] = info.NarcDoctInfo.Name;
            if (info.NickKind != "")
            {
                row["�п�"] = NickTypeHelper.GetName(info.NickKind);
            }
            if (info.CicaKind != "")
            {
                row["����"] = CicaTypeHelper.GetName(info.CicaKind);
            }
            if (info.StatFlag == "0")
            {
                row["ͳ��"] = true;
            }
            else
            {
                row["ͳ��"] = false;
            }
            row["ҽʦ����1"] = info.FirDoctInfo.ID;
            row["ҽʦ����2"] = info.FourDoctInfo.ID;
            row["���ֱ���1"] = info.SecDoctInfo.ID;
            row["���ֱ���2"] = info.ThrDoctInfo.ID;
            row["�������"] = info.HappenNO;
            row["�������к�"] = info.OperationNO;
            //add chengym
            if (info.MarcKind != "")
            {
                row["����ʽ1"] = NarcTypeHelper.GetName(info.MarcKind);
            }
            if (info.OpbOpa != "")
            {
                row["����ʽ2"] = NarcTypeHelper.GetName(info.OpbOpa);
            }
        }


        private void ucOperation_Load(object sender, System.EventArgs e)
        {

        }

        /// <summary>
        /// ��ʼ�� ҽ���б������ҽʦ�б�
        /// </summary>
        private void InitDoctorList()
        {
            FS.HISFC.BizLogic.Manager.Person per = new FS.HISFC.BizLogic.Manager.Person();
            //��ȡ����/����ҽ���б�
            ArrayList list = per.GetEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
            empl.ID = "-";
            empl.Name = "-";
            empl.SpellCode = "A0";
            empl.WBCode = "-";
            list.Add(empl);
            if (list != null)
            {
                DoctorType.AddItems(list);
                DoctorHelper.ArrayObject = list;
            }
            //���� listBox
            Controls.Add(DoctorType);
            //����
            DoctorType.Hide();
            //���ñ߿�
            DoctorType.BorderStyle = BorderStyle.Fixed3D;
            DoctorType.BringToFront();
            DoctorType.SelectNone = true;
            //				lbDept.Font=new System.Drawing.Font("����", 12F);
            //����listBox�ĵ����¼�
            DoctorType.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(diagType_SelectItem);
        }

        /// <summary>
        /// ��ʼ��ICD�����б�
        /// </summary>
        public void InitICDList()
        {
            //ICD�������
            FS.HISFC.BizLogic.HealthRecord.ICD icd = new FS.HISFC.BizLogic.HealthRecord.ICD();
            if (icdList == null || icdList.Count == 0)
            {
                icdList = icd.Query(ICDTypes.ICDOperation, QueryTypes.Valid);
            }
            //			FS.HISFC.BizProcess.Fee.Item item = new FS.HISFC.BizProcess.Fee.Item();
            //			ArrayList  icdList = item.GetOperationItemList();
            //�����б�
            if (icdList != null)
            {

                ICDType.AddItems(icdList);
            }
            //���� listBox
            Controls.Add(ICDType);
            //����
            ICDType.Hide();
            //���ñ߿�
            ICDType.BorderStyle = BorderStyle.Fixed3D;
            ICDType.BringToFront();
            ICDType.SelectNone = true;
            //����listBox�ĵ����¼�
            ICDType.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(diagType_SelectItem);
        }
        /// <summary>
        /// ����ѡ���¼� 
        /// </summary>
        /// <param name="key"></param>
        /// <returns> �ɹ����� 0</returns>
        private int diagType_SelectItem(Keys key)
        {
            ProcessDept();
            return 0;
        }
        /// <summary>
        /// ����ʽ�б�
        /// </summary>
        private void InitNarcList()
        {
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //�ӳ������л�ȡ��������
            ArrayList list = con.GetList("CASEANESTYPE");// con.GetList("ANESTYPE");
            if (list != null)
            {
                //���������
                NarcType.AddItems(list);
                NarcTypeHelper.ArrayObject = list;
            }
            //���� listBox
            Controls.Add(NarcType);
            //����
            NarcType.Hide();
            //���ñ߿�
            NarcType.BorderStyle = BorderStyle.Fixed3D;
            NarcType.BringToFront();
            NarcType.SelectNone = true;
            //				lbDept.Font=new System.Drawing.Font("����", 12F);
            //����listBox�ĵ����¼�
            NarcType.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(diagType_SelectItem);
        }
        /// <summary>
        /// ��ʼ���п��б�
        /// </summary>
        private void IniNickType()
        {
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //�ӳ������л�ȡ�п�����
            ArrayList list = con.GetList("INCITYPE");
            if (list != null)
            {
                NickType.AddItems(list);
                NickTypeHelper.ArrayObject = list;
            }
            //���� listBox
            Controls.Add(NickType);
            //����
            NickType.Hide();
            //���ñ߿�
            NickType.BorderStyle = BorderStyle.Fixed3D;
            NickType.BringToFront();
            NickType.SelectNone = true;
            //				lbDept.Font=new System.Drawing.Font("����", 12F);
            //����listBox�ĵ����¼�
            NickType.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(diagType_SelectItem);
        }
        /// <summary>
        /// ��ʼ�������б�
        /// </summary>
        private void IniCicaType()
        {
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //�ӳ������л�ȡ�п�����
            ArrayList list = con.GetList("CICATYPE");
            if (list != null)
            {
                CicaType.AddItems(list);
                CicaTypeHelper.ArrayObject = list;
            }
            //���� listBox
            Controls.Add(CicaType);
            //����
            CicaType.Hide();
            //���ñ߿�
            CicaType.BorderStyle = BorderStyle.Fixed3D;
            CicaType.BringToFront();
            CicaType.SelectNone = true;
            //				lbDept.Font=new System.Drawing.Font("����", 12F);
            //����listBox�ĵ����¼�
            CicaType.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(diagType_SelectItem);
        }
        /// <summary>
        /// ����fpSpread1,ִ�п��ҵĻس�
        /// </summary>
        /// <returns></returns>
        private int ProcessDept()
        {
            try
            {
                int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
                if (CurrentRow < 0) return 0;

                if (fpSpread1_Sheet1.ActiveColumnIndex == 2)
                {
                    ////��ȡѡ�е���Ϣ
                    //FS.FrameWork.Models.NeuObject item = null;
                    //int rtn = ICDType.GetSelectedItem(out item);
                    ////					if(rtn==-1)return -1;
                    //if (item == null) return -1;
                    ////ICD�������
                    //fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    ////ICD��ϱ���
                    //fpSpread1_Sheet1.Cells[CurrentRow, 1].Text = item.ID;
                    //ICDType.Visible = false;
                    //fpSpread1.Focus();
                    //fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 3);
                    //return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 3)
                {
                    //��ȡѡ�е���Ϣ
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = DoctorType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //����A
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    //����A����
                    fpSpread1_Sheet1.Cells[CurrentRow, 13].Text = item.ID;
                    DoctorType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 5, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 4)
                {
                    //��ȡѡ�е���Ϣ
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = DoctorType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //����B
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    //����B����
                    fpSpread1_Sheet1.Cells[CurrentRow, 14].Text = item.ID;
                    DoctorType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 5, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 5)
                {
                    //��ȡѡ�е���Ϣ
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = DoctorType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //һ��A
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    //һ��A����
                    fpSpread1_Sheet1.Cells[CurrentRow, 15].Text = item.ID;
                    DoctorType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 6, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 6)
                {
                    //��ȡѡ�е���Ϣ
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = DoctorType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //����
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    //��������
                    fpSpread1_Sheet1.Cells[CurrentRow, 16].Text = item.ID;
                    DoctorType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 9, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 7)
                {
                    //��ȡѡ�е���Ϣ
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = NarcType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //����ʽ
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    NarcType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 9, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 9)
                {
                    //��ȡѡ�е���Ϣ
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = DoctorType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //����ҽʦ
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    //����ҽʦ����
                    fpSpread1_Sheet1.Cells[CurrentRow, 17].Text = item.ID;
                    DoctorType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 10, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 1)
                {
                    //��ȡѡ�е���Ϣ
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = ICDType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //ICD��ϱ���
                    fpSpread1_Sheet1.ActiveCell.Text = item.ID;
                    //ICD������� 
                    fpSpread1_Sheet1.Cells[CurrentRow, 2].Text = item.Name;
                    ICDType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 2, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 10)
                {
                    //��ȡѡ�е���Ϣ
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = NickType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //�п�����
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    NickType.Visible = false;

                    //�п����ϵȼ�
                    string strText = fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 8].Text;
                    //��ȡ/֮����ַ��� �滻/֮ǰ���ַ���
                    strText = item.Name + strText.Substring(GetStrPosition(strText, "/"));
                    fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 8].Text = strText;

                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 11, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 11)
                {
                    //��ȡѡ�е���Ϣ
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = CicaType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //��������
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    CicaType.Visible = false;

                    //�п����ϵȼ�
                    string strText = fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 8].Text;
                    //��ȡ /֮ǰ���ַ���  �滻 /֮����ַ���
                    strText = strText.Substring(0, GetStrPosition(strText, "/") + 1) + item.Name;
                    fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 8].Text = strText;

                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 20);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 20)
                {
                    //��ȡѡ�е���Ϣ
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = NarcType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //����ʽ
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    NarcType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 21, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 21)
                {
                    //��ȡѡ�е���Ϣ
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = NarcType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //����ʽ
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    NarcType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 12, true);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }
        private int GetStrPosition(string strStr, string subStr)
        {
            return strStr.LastIndexOf(subStr);
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {
                switch (keyData)
                {
                    case Keys.Enter:
                        #region �س��¼�
                        if (fpSpread1.ContainsFocus)
                        {
                            int i = fpSpread1_Sheet1.ActiveColumnIndex;
                            if (i == 1 || i == 2 || i == 3 || i == 4 || i == 5 || i == 6 || i == 8 || i == 9 || i == 10 || i == 11||i==20||i==21)
                            {
                                ProcessDept();
                            }
                            if (i == 0)
                            {
                                //��������
                                fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 1);
                            }
                            if (i == 8)
                            {
                                //�п���������
                                fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 9);
                            }
                            if (i == 12)
                            {
                                if (fpSpread1_Sheet1.ActiveRowIndex < fpSpread1_Sheet1.Rows.Count - 1)
                                {
                                    //����������һ�� ���������һ�е�һ��
                                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex + 1, 0);
                                }
                                else
                                {
                                    //									//��������һ�� �������е�һ��
                                    //									fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex,0);
                                    //����һ��
                                    this.AddRow();
                                }
                            }
                        }
                        break;
                        #endregion
                    case Keys.Up:
                        #region �ϼ�
                        if (fpSpread1.ContainsFocus)
                        {
                            //�������
                            if (ICDType.Visible)
                            {
                                ICDType.PriorRow();
                            }
                            //�п�����
                            else if (NickType.Visible)
                            {
                                NickType.PriorRow();
                            }
                            //��������
                            else if (CicaType.Visible)
                            {
                                CicaType.PriorRow();
                            }
                            //��������
                            else if (NarcType.Visible)
                            {
                                NarcType.PriorRow();
                            }
                            //ҽ���б�
                            else if (DoctorType.Visible)
                            {
                                DoctorType.PriorRow();
                            }
                            else
                            {
                                int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
                                if (CurrentRow > 0)
                                {
                                    fpSpread1_Sheet1.ActiveRowIndex = CurrentRow - 1;
                                    fpSpread1_Sheet1.AddSelection(CurrentRow - 1, 0, 1, 0);
                                }
                            }
                        }
                        break;
                        #endregion
                    case Keys.Down:
                        #region  �¼�

                        if (fpSpread1.ContainsFocus)
                        {
                            //�������
                            if (ICDType.Visible)
                            {
                                ICDType.NextRow();
                            }
                            //�п�����
                            else if (NickType.Visible)
                            {
                                NickType.NextRow();
                            }
                            //��������
                            else if (CicaType.Visible)
                            {
                                CicaType.NextRow();
                            }
                            //��������
                            else if (NarcType.Visible)
                            {
                                NarcType.NextRow();
                            }
                            //ҽ���б�
                            else if (DoctorType.Visible)
                            {
                                DoctorType.NextRow();
                            }
                            else
                            {
                                int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;

                                if (CurrentRow < fpSpread1_Sheet1.RowCount - 1)
                                {
                                    fpSpread1_Sheet1.ActiveRowIndex = CurrentRow + 1;
                                    fpSpread1_Sheet1.AddSelection(CurrentRow + 1, 0, 1, 0);
                                }
                                else
                                {
                                    //									AddRow();							
                                }
                            }
                        }
                        break;

                        #endregion
                    case Keys.NumPad1:
                        #region ���ּ� 1
                        //ͳ�Ʊ�־
                        if (fpSpread1_Sheet1.ActiveColumnIndex == 12)
                        {
                            //ͳ�Ʊ�־ȡ��
                            if (fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 12].Value == null)
                            {
                                fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 12].Value = true;
                            }
                            else if (fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 12].Value.ToString() == "False")
                            {
                                fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 12].Value = true;
                            }
                            else
                            {
                                fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 12].Value = false;
                            }
                            //							//��ת
                            //							if(fpSpread1_Sheet1.ActiveRowIndex < fpSpread1_Sheet1.Rows.Count -1)
                            //							{
                            //								//����������һ�� ��������һ�е�һ��
                            //								fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex+1,0);
                            //							}
                            //							else
                            //							{
                            //								//��������һ�� �������е�һ��
                            //								fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex,0);
                            //							}
                        }
                        #endregion
                        break;
                    case Keys.Escape:
                        ICDType.Visible = false;
                        NickType.Visible = false;
                        CicaType.Visible = false;
                        NarcType.Visible = false;
                        DoctorType.Visible = false;
                        break;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return base.ProcessDialogKey(keyData);
        }
        /// <summary>
        /// ��Ԫ���ڱ༭״̬ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            try
            {
                switch (e.Column)
                {

                    case 2:
                        ////����������
                        ////��ȡ��ǰ���ֵ
                        //ICDType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        ////ɸѡ����
                        break;
                    case 1:
                        //����ICD�������
                        //��ȡ��ǰ���ֵ
                        ICDType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //ɸѡ����
                        break;

                    #region  ҽ��

                    case 3:
                        //��������A
                        //��ȡ��ǰ���ֵ
                        DoctorType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //ɸѡ����
                        break;
                    case 4:
                        //��������B
                        //��ȡ��ǰ���ֵ
                        DoctorType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //ɸѡ����
                        break;
                    case 5:
                        //����һ��
                        //��ȡ��ǰ���ֵ
                        DoctorType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //ɸѡ����
                        break;
                    case 6:
                        //���˶���
                        //��ȡ��ǰ���ֵ
                        DoctorType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //ɸѡ����
                        break;
                    case 9:
                        //��������ҽʦ����
                        //��ȡ��ǰ���ֵ
                        DoctorType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //ɸѡ����
                        break;

                    #endregion

                    case 7:
                        //��������ʽ����
                        //��ȡ��ǰ���ֵ
                        NarcType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //ɸѡ����
                        break;
                    case 10:
                        //�����п�����
                        //��ȡ��ǰ���ֵ
                        NickType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //ɸѡ����
                        break;
                    case 11:
                        //������������
                        //��ȡ��ǰ���ֵ
                        CicaType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //ɸѡ����
                        break;
                    case 20:
                        //��������ʽ����
                        //��ȡ��ǰ���ֵ
                        NarcType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //ɸѡ����
                        break;
                    case 21:
                        //��������ʽ����
                        //��ȡ��ǰ���ֵ
                        NarcType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //ɸѡ����
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// ���������˵�����ʾλ��
        /// </summary>
        /// <returns></returns>

        private int SetLocation()
        {
            Control _cell = fpSpread1.EditingControl;
            //��ǰ���
            int intCol = fpSpread1_Sheet1.ActiveColumnIndex;
            //���� ICD��� �������λ�� 
            if (intCol == 1)// || intCol == 2
            {
                ICDType.Location = new Point(panel4.Location.X + _cell.Location.X,
                    panel4.Location.Y + panel4.Location.Y + _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                //				ICDType.Size=new Size(_cell.Width+SystemInformation.Border3DSize.Width*2,150);
                ICDType.Size = new Size(350, 200);
            }
            //���� ҽ���������λ�� 
            else if (intCol == 3 || intCol == 4 || intCol == 5 || intCol == 6 || intCol == 9)
            {
                DoctorType.Location = new Point(panel4.Location.X + _cell.Location.X,
                    panel4.Location.Y + panel4.Location.Y + _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                //				DoctorType.Size=new Size(_cell.Width+SystemInformation.Border3DSize.Width*2,150);
                DoctorType.Size = new Size(200, 150);
            }
            //���� ����ʽ �������λ�� 
            else if (intCol == 7 ||intCol==20 || intCol==21)
            {
                NarcType.Location = new Point(panel4.Location.X + _cell.Location.X,
                    panel4.Location.Y + panel4.Location.Y + _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                //				NarcType.Size=new Size(_cell.Width+SystemInformation.Border3DSize.Width*2,150);
                NarcType.Size = new Size(150, 100);
            }
            //���� �п� �������λ�� 
            else if (intCol == 10)
            {
                NickType.Location = new Point(panel4.Location.X + _cell.Location.X,
                    panel4.Location.Y + panel4.Location.Y + _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                //				NickType.Size=new Size(_cell.Width+SystemInformation.Border3DSize.Width*2,150);
                NickType.Size = new Size(150, 100);
            }
            //���� ���� �������λ�� 
            else if (intCol == 11)
            {
                CicaType.Location = new Point(panel4.Location.X + _cell.Location.X,
                    panel4.Location.Y + panel4.Location.Y + _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                //				CicaType.Size=new Size(_cell.Width+SystemInformation.Border3DSize.Width*2,150);
                CicaType.Size = new Size(150, 100);
            }

            return 0;
        }

        private void fpSpread1_EditModeOn(object sender, System.EventArgs e)
        {
            try
            {
                //			fpSpread1.EditingControl.KeyDown+=new KeyEventHandler(EditingControl_KeyDown);
                SetLocation();
                int intCol = fpSpread1_Sheet1.ActiveColumnIndex;
                //���� ICD��� ������Ŀɼ���
                if (intCol != 1)// && intCol != 2
                {
                    ICDType.Visible = false;
                }
                //���� ҽ��������Ŀɼ���
                if (intCol != 3 || intCol != 4 || intCol != 5 || intCol != 6 || intCol != 9)
                {
                    DoctorType.Visible = false;
                }
                //���� ��������������Ŀɼ���
                if (intCol != 7|| intCol!=20 ||intCol!=21)
                {
                    NarcType.Visible = false;
                }
                //���� �п�������Ŀɼ���
                if (intCol != 10)
                {
                    NickType.Visible = false;
                }
                //���� ����������Ŀɼ���
                if (intCol != 11)
                {
                    CicaType.Visible = false;
                }

                //�������
                if (intCol == 1)//|| intCol == 2
                {
                    ICDType.Filter(fpSpread1_Sheet1.ActiveCell.Text.Trim());
                    ICDType.Visible = true;
                }
                //ҽ��
                else if (intCol == 3 || intCol == 4 || intCol == 5 || intCol == 6 || intCol == 9)
                {
                    DoctorType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                    DoctorType.Visible = true;
                }
                //����ʽ
                else if (intCol == 7)
                {
                    NarcType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                    NarcType.Visible = true;
                }
                //�п�
                else if (intCol == 10)
                {
                    NickType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                    NickType.Visible = true;
                }
                //����
                else if (intCol == 11)
                {
                    CicaType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                    CicaType.Visible = true;
                }
                //����ʽ1
                else if (intCol == 20)
                {
                    NarcType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                    NarcType.Visible = true;
                }
                //����ʽ2
                else if (intCol == 21)
                {
                    NarcType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                    NarcType.Visible = true;
                }
            }
            catch { }
        }
        /// <summary>
        /// ��ȡ�к�
        /// </summary>
        /// <param name="view"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public int ColumnIndex(FarPoint.Win.Spread.SheetView view, string str)
        {
            try
            {
                foreach (FarPoint.Win.Spread.Column col in view.Columns)
                {
                    if (col.Label == str)
                    {
                        return col.Index;
                    }
                }
                MessageBox.Show("û���ҵ�" + str + "��");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            //			//����fpSpread1 ������
            //			if(System.IO.File.Exists(filePath))
            //			{
            //				FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1,filePath);
            //			}
        }
        //����
        private void menuItem1_Click(object sender, System.EventArgs e)
        {
            SetUp();
        }
        /// <summary>
        ///����fpSpread1_Sheet1 ������
        /// </summary>
        public void SetUp()
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
            LoadInfo(patient, operType); //���¼�������

        }

        /// <summary>
        /// ɾ�� 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem2_Click(object sender, System.EventArgs e)
        {
            DeleteRow();
        }
        /// <summary>
        /// ɾ�� 
        /// </summary>
        /// <returns></returns>
        public int DeleteRow()
        {
            SetAllListUnVisiable();
            this.fpSpread1.EditModePermanent = false;
            this.fpSpread1.EditModeReplace = false;
            this.fpSpread1_Sheet1.Rows.Remove(fpSpread1_Sheet1.ActiveRowIndex, 1);
            this.fpSpread1.EditModePermanent = true;
            this.fpSpread1.EditModeReplace = true;
            return 1;
        }
        #endregion

        private void btAdd_Click(object sender, EventArgs e)
        {
            this.AddRow();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            this.DeleteActiveRow();
        }

        /// <summary>
        /// ��ȡ�޸Ĺ������� 
        /// </summary>
        /// <param name="table">Ҫ��ȡ���ݵ�Table</param>
        /// <param name="list"> ���������</param>
        /// <returns>ʧ�ܷ���false ,�����鷵��null �ɹ����� null</returns>
        public bool GetOperationList(ArrayList list)
        {
            try
            {
                FS.HISFC.Models.HealthRecord.OperationDetail bb;
                for (int row = 0; row < this.fpSpread1_Sheet1.RowCount; row++)
                {
                    bb = new FS.HISFC.Models.HealthRecord.OperationDetail();

                    bb.OperType = "1";
                    bb.InpatientNO = patient.ID;
                    bb.OperationDate = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.Cells[row, 0].Text);
                    bb.OperationInfo.ID = this.fpSpread1_Sheet1.Cells[row, 1].Text;//ICD-9-CM-3���
                    if (bb.OperationInfo.ID == "")
                    {
                        bb.OperationInfo.ID = "MS999";
                    }

                    bb.OperationInfo.Name = this.fpSpread1_Sheet1.Cells[row, 2].Text;//��������
                    bb.FirDoctInfo.Name = this.fpSpread1_Sheet1.Cells[row, 3].Text; //���� A
                    //bb.FourDoctInfo.Name = row["���� B"].ToString();
                    bb.SecDoctInfo.Name = this.fpSpread1_Sheet1.Cells[row, 5].Text; //I ��
                    bb.ThrDoctInfo.Name = this.fpSpread1_Sheet1.Cells[row, 6].Text;// II ��
                    if (this.fpSpread1_Sheet1.Cells[row, 20].Text != "")//����ʽ1
                    {
                        bb.MarcKind = NarcTypeHelper.GetID(this.fpSpread1_Sheet1.Cells[row, 20].Text);
                    }
                    if (this.fpSpread1_Sheet1.Cells[row, 21].Text != "")//����ʽ2
                    {
                        bb.OpbOpa = NarcTypeHelper.GetID(this.fpSpread1_Sheet1.Cells[row, 21].Text);
                    }

                    bb.NarcDoctInfo.Name = this.fpSpread1_Sheet1.Cells[row, 9].Text;//����ҽʦ

                    if (this.fpSpread1_Sheet1.Cells[row, 10].Text != "")//�п�
                    {
                        bb.NickKind = NickTypeHelper.GetID(this.fpSpread1_Sheet1.Cells[row, 10].Text);
                    }
                    if (this.fpSpread1_Sheet1.Cells[row, 11].Text != "")//����
                    {
                        bb.CicaKind = CicaTypeHelper.GetID(this.fpSpread1_Sheet1.Cells[row, 11].Text);
                    }
                    if (this.fpSpread1_Sheet1.Cells[row, 12].Text != "")
                    {
                        if (Convert.ToBoolean(this.fpSpread1_Sheet1.Cells[row, 12].Text))
                        {
                            bb.StatFlag = "0";
                        }
                        else
                        {
                            bb.StatFlag = "1";
                        }
                    }
                    else
                    {
                        bb.StatFlag = "1";
                    }
                    bb.FirDoctInfo.ID = DoctorHelper.GetID(bb.FirDoctInfo.Name);//this.fpSpread1_Sheet1.Cells[row, 13].Text;// row["ҽʦ����1"].ToString();
                    bb.FourDoctInfo.ID = "";// row["ҽʦ����2"].ToString();
                    bb.SecDoctInfo.ID = DoctorHelper.GetID(bb.SecDoctInfo.Name);//this.fpSpread1_Sheet1.Cells[row, 15].Text;// row["���ֱ���1"].ToString();
                    bb.ThrDoctInfo.ID = DoctorHelper.GetID(bb.ThrDoctInfo.Name);//this.fpSpread1_Sheet1.Cells[row, 16].Text;// row["���ֱ���2"].ToString();
                    bb.NarcDoctInfo.ID = DoctorHelper.GetID(bb.NarcDoctInfo.Name);//this.fpSpread1_Sheet1.Cells[row, 17].Text;// row["����ҽʦ����"].ToString();
                    bb.HappenNO = row.ToString();// row["�������"].ToString();
                    bb.OperationNO = row.ToString();// row["�������к�"].ToString();
                    bb.OutDate = this.patient.PVisit.OutTime;//��Ժ����
                    bb.InDate = patient.PVisit.InTime; //��Ժ���� 
                    bb.DeatDate = patient.DeathTime; //����ʱ�� 
                    bb.OperationDeptInfo.ID = ""; //��������
                    bb.OutDeptInfo.ID = patient.PVisit.PatientLocation.ID; //��Ժ����
                    TimeSpan tt = bb.OperationDate- this.patient.PVisit.InTime;
                    if (tt.Days == 0)
                    {
                        bb.BeforeOperDays = 1;
                    }
                    else
                    {
                        bb.BeforeOperDays = tt.Days;
                    }
                    list.Add(bb);
                }
                return true;
            }
            catch (Exception ex)
            {
                list = null;
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
