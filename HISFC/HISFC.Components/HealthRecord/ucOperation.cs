using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FarPoint.Win.Spread;
using Neusoft.HISFC.Object.HealthRecord.EnumServer;
namespace UFC.HealthRecord
{
    public partial class ucOperation : UserControl
    {
        public ucOperation()
        {
            InitializeComponent();
        }

        #region   ȫ�ֱ���
        //�����ļ�·�� 
        //private string filePath = Application.StartupPath + "\\profile\\OperationCard.xml";
        //����� "DOC" ��ѯ����ҽ��վ¼���������Ϣ ���������ǡ�CAS�������ѯ����ʦ¼���������Ϣ
        private Neusoft.HISFC.Object.HealthRecord.EnumServer.frmTypes operType;
        //���Ʒ���Ӥ����¼�� 
        private DataTable dtOperation;
        private DataView dvOperation;
        /// <summary>
        ///ICD �����Ϣ �б�
        /// </summary>
        private Neusoft.NFC.Interface.Controls.PopUpListBox ICDType = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        //�п�����
        private Neusoft.NFC.Interface.Controls.PopUpListBox NickType = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper NickTypeHelper = new Neusoft.NFC.Public.ObjectHelper();

        //��������
        private Neusoft.NFC.Interface.Controls.PopUpListBox CicaType = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper CicaTypeHelper = new Neusoft.NFC.Public.ObjectHelper();
        //����ʽ�б�
        private Neusoft.NFC.Interface.Controls.PopUpListBox NarcType = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper NarcTypeHelper = new Neusoft.NFC.Public.ObjectHelper();
        //����/����ҽ���б�
        private Neusoft.NFC.Interface.Controls.PopUpListBox DoctorType = new Neusoft.NFC.Interface.Controls.PopUpListBox();

        private Neusoft.HISFC.Object.RADT.PatientInfo patient = new Neusoft.HISFC.Object.RADT.PatientInfo();
        #endregion

        #region ����
        //		/// <summary>
        //		///����� "DOC" ��ѯ����ҽ��վ¼���������Ϣ ���������ǡ�CAS�������ѯ����ʦ¼���������Ϣ
        //		/// </summary>
        //		public string OperType
        //		{
        //			set
        //			{
        //				operType =value;
        //			}
        //		}
        //		/// <summary>
        //		/// סԺ��ˮ��
        //		/// </summary>
        //		public string InpatientNo
        //		{
        //			set
        //			{
        //				if(value != null)
        //				{
        //					inpatientNo = value;
        //					try
        //					{
        //						LoadInfo();
        //					}
        //					catch
        //					{
        //					}
        //				}
        //			}
        //		}
        ////		/// <summary>
        ////		/// �����ӵ�����
        ////		/// </summary>
        ////		public ArrayList AddList
        ////		{
        ////			get
        ////			{
        ////				try
        ////				{
        ////					GetList("A");
        ////				}
        ////				catch{}
        ////				return addList;
        ////			}
        ////		}
        ////		/// <summary>
        ////		/// �޸Ĺ�������
        ////		/// </summary>
        ////		public ArrayList ModList
        ////		{
        ////			get
        ////			{
        ////				try
        ////				{
        ////					GetList("M");
        ////				}
        ////				catch{}
        ////				return modList;
        ////			}
        ////		}
        ////		/// <summary>
        ////		/// ɾ��������
        ////		/// </summary>
        ////		public ArrayList DelList
        ////		{
        ////			get
        ////			{
        ////				try
        ////				{
        ////					GetList("D");
        ////				}
        ////				catch{}
        ////				return delList;
        ////			}
        ////		}
        #endregion

        #region ����
       
        /// <summary>
        /// �޶���Ŀ�Ⱥܿɼ��� 
        /// </summary>
        private void LockFpEnter()
        {
            this.fpSpread1_Sheet1.Columns[0].Width = 76; //��������
            this.fpSpread1_Sheet1.Columns[1].Width = 93;//��������
            this.fpSpread1_Sheet1.Columns[2].Width = 40;//���� A
            this.fpSpread1_Sheet1.Columns[3].Width = 40; //���� B
            this.fpSpread1_Sheet1.Columns[4].Width = 40; //I ��
            this.fpSpread1_Sheet1.Columns[5].Width = 40; //II ��
            this.fpSpread1_Sheet1.Columns[6].Width = 40; //����ʽ
            this.fpSpread1_Sheet1.Columns[7].Width = 80; //�п����ϵȼ�
            this.fpSpread1_Sheet1.Columns[7].Locked = true;//�п����ϵȼ�
            this.fpSpread1_Sheet1.Columns[8].Width = 50; //����ҽʦ
            this.fpSpread1_Sheet1.Columns[9].Width = 100; //ICD-9-CM-3���
            this.fpSpread1_Sheet1.Columns[9].Locked = true; //ICD-9-CM-3���
            this.fpSpread1_Sheet1.Columns[10].Width = 50; //�п�
            this.fpSpread1_Sheet1.Columns[11].Width = 50; //����
            this.fpSpread1_Sheet1.Columns[12].Width = 40; //ͳ��
            this.fpSpread1_Sheet1.Columns[13].Visible = false; //ҽʦ����1
            this.fpSpread1_Sheet1.Columns[14].Visible = false; //ҽʦ����2
            this.fpSpread1_Sheet1.Columns[15].Visible = false; //���ֱ���1
            this.fpSpread1_Sheet1.Columns[16].Visible = false; //���ֱ���2
            this.fpSpread1_Sheet1.Columns[17].Visible = false; //����ҽʦ����
            this.fpSpread1_Sheet1.Columns[18].Visible = false; //�������
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
            foreach (Neusoft.HISFC.Object.HealthRecord.OperationDetail obj in list)
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
                if (obj.InpatientNO.Length > 14)
                {
                    MessageBox.Show("סԺ��ˮ�Ź���");
                    return -1;
                }
                if (obj.HappenNO.Length > 2)
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
            }
            return 0;
        }
        /// <summary>
        /// ɾ����ǰ�� 
        /// </summary>
        /// <returns></returns>
        public int DeleteActiveRow()
        {
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
            return 1;
        }
        /// <summary>
        /// ɾ���հ׵���
        /// </summary>
        /// <returns></returns>
        public int deleteRow()
        {
            if (fpSpread1_Sheet1.Rows.Count == 1)
            {
                //��һ�б���Ϊ�� 
                if (fpSpread1_Sheet1.Cells[0, 1].Text == "" && fpSpread1_Sheet1.Cells[0, 2].Text == "")
                {
                    fpSpread1_Sheet1.Rows.Remove(0, 1);
                }
            }
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
                foreach (Neusoft.HISFC.Object.HealthRecord.OperationDetail info in list)
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
            if ((this.operType == Neusoft.HISFC.Object.HealthRecord.EnumServer.frmTypes.DOC && this.patient.CaseState == "2") || (this.operType == Neusoft.HISFC.Object.HealthRecord.EnumServer.frmTypes.CAS && this.patient.CaseState == "3"))
            {
                //��ձ�ı�־λ
                dtOperation.AcceptChanges();
            }

            //			if(System.IO.File.Exists(filePath))
            //			{
            //				Neusoft.NFC.Interface.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1,filePath);
            //			}
            LockFpEnter();
            return 0;
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
                fpSpread1_Sheet1.Cells[0, 7].Text = "/";
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
        /// ��������
        /// </summary>
        public void LoadInfo(Neusoft.HISFC.Object.RADT.PatientInfo patientInfo, Neusoft.HISFC.Object.HealthRecord.EnumServer.frmTypes Type)
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
            Neusoft.HISFC.Management.HealthRecord.Operation op = new Neusoft.HISFC.Management.HealthRecord.Operation();
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
                if (operType == Neusoft.HISFC.Object.HealthRecord.EnumServer.frmTypes.CAS)
                {
                    list = op.QueryOperation(Neusoft.HISFC.Object.HealthRecord.EnumServer.frmTypes.DOC, patient.ID);
                }
            }
            this.AddInfoToTable(list);
        }

        /// <summary>
        /// ��ȡ��ص�����
        /// creator:zhangjunyi@Neusoft.com
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
                Neusoft.HISFC.Object.HealthRecord.OperationDetail bb;
                foreach (DataRow row in table.Rows)
                {
                    bb = new Neusoft.HISFC.Object.HealthRecord.OperationDetail();
                    //�������� ҽ��վ¼��Ļ򲡰���¼���
                    if (operType == Neusoft.HISFC.Object.HealthRecord.EnumServer.frmTypes.DOC)
                    {
                        bb.OperType = "1";
                    }
                    else if (operType == Neusoft.HISFC.Object.HealthRecord.EnumServer.frmTypes.CAS)
                    {
                        bb.OperType = "2";
                    }
                    bb.InpatientNO = patient.ID;
                    bb.OperationDate = Neusoft.NFC.Function.NConvert.ToDateTime(row["��������"]);
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
                    bb.OperationInfo.ID = row["ICD-9-CM-3���"].ToString();
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
																 new DataColumn("��������", strType), //1
																 new DataColumn("���� A", strType),//2
																 new DataColumn("���� B", strType),//3
																 new DataColumn("I ��", strType),//4
																 new DataColumn("II ��", strType),//5
																 new DataColumn("����ʽ", strType), //6
																 new DataColumn("�п����ϵȼ�", strType),//7
																 new DataColumn("����ҽʦ", strType),//8
																 new DataColumn("ICD-9-CM-3���", strType),//9
																 new DataColumn("�п�", strType),//10
																 new DataColumn("����", strType),//11
																 new DataColumn("ͳ��", boolType),//12
																 new DataColumn("ҽʦ����1", strType),//13
																 new DataColumn("ҽʦ����2", strType),//14
																 new DataColumn("���ֱ���1", strType),//15
																 new DataColumn("���ֱ���2", strType),//16
																 new DataColumn("����ҽʦ����", strType),//17
																 new DataColumn("�������", strType)});//18

                //				//��������Ϊ�����
                //				CreateKeys(dtOperation);

                this.fpSpread1.DataSource = dvOperation;
                fpSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
                //����fpSpread1 ������
                //				if(System.IO.File.Exists(filePath))
                //				{
                //					Neusoft.NFC.Interface.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1,filePath);
                //				}
                //				else
                //				{
                //					Neusoft.NFC.Interface.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1,filePath);
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
        private void SetRow(DataRow row, Neusoft.HISFC.Object.HealthRecord.OperationDetail info)
        {
            row["��������"] = info.OperationDate;
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
            row["ICD-9-CM-3���"] = info.OperationInfo.ID;
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
        }

        /// <summary>
        /// ��ʼ�� ҽ���б������ҽʦ�б�
        /// </summary>
        private void InitDoctorList()
        {
            Neusoft.HISFC.Management.Manager.Person per = new Neusoft.HISFC.Management.Manager.Person();
            //��ȡ����/����ҽ���б�
            ArrayList list = per.GetEmployee(Neusoft.HISFC.Object.Base.EnumEmployeeType.D);
            if (list != null)
            {
                DoctorType.AddItems(list);
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
            DoctorType.SelectItem += new Neusoft.NFC.Interface.Controls.PopUpListBox.MyDelegate(diagType_SelectItem);
        }

        /// <summary>
        /// ��ʼ��ICD�����б�
        /// </summary>
        public void InitICDList()
        {
            //ICD�������
            Neusoft.HISFC.Management.HealthRecord.ICD icd = new Neusoft.HISFC.Management.HealthRecord.ICD();
            ArrayList icdList = icd.Query(ICDTypes.ICDOperation, QueryTypes.Valid);
            //			Neusoft.HISFC.Management.Fee.Item item = new Neusoft.HISFC.Management.Fee.Item();
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
            ICDType.SelectItem += new Neusoft.NFC.Interface.Controls.PopUpListBox.MyDelegate(diagType_SelectItem);
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
            Neusoft.HISFC.Management.Manager.Constant con = new Neusoft.HISFC.Management.Manager.Constant();
            //�ӳ������л�ȡ��������
            ArrayList list = con.GetList("ANESTYPE");
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
            NarcType.SelectItem += new Neusoft.NFC.Interface.Controls.PopUpListBox.MyDelegate(diagType_SelectItem);
        }
        /// <summary>
        /// ��ʼ���п��б�
        /// </summary>
        private void IniNickType()
        {
            Neusoft.HISFC.Management.Manager.Constant con = new Neusoft.HISFC.Management.Manager.Constant();
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
            NickType.SelectItem += new Neusoft.NFC.Interface.Controls.PopUpListBox.MyDelegate(diagType_SelectItem);
        }
        /// <summary>
        /// ��ʼ�������б�
        /// </summary>
        private void IniCicaType()
        {
            Neusoft.HISFC.Management.Manager.Constant con = new Neusoft.HISFC.Management.Manager.Constant();
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
            CicaType.SelectItem += new Neusoft.NFC.Interface.Controls.PopUpListBox.MyDelegate(diagType_SelectItem);
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

                if (fpSpread1_Sheet1.ActiveColumnIndex == 1)
                {
                    //��ȡѡ�е���Ϣ
                    Neusoft.NFC.Object.NeuObject item = null;
                    int rtn = ICDType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //ICD�������
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    //ICD��ϱ���
                    fpSpread1_Sheet1.Cells[CurrentRow, 9].Text = item.ID;
                    ICDType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 2);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 2)
                {
                    //��ȡѡ�е���Ϣ
                    Neusoft.NFC.Object.NeuObject item = null;
                    int rtn = DoctorType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //����A
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    //����A����
                    fpSpread1_Sheet1.Cells[CurrentRow, 13].Text = item.ID;
                    DoctorType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 3, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 3)
                {
                    //��ȡѡ�е���Ϣ
                    Neusoft.NFC.Object.NeuObject item = null;
                    int rtn = DoctorType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //����B
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    //����B����
                    fpSpread1_Sheet1.Cells[CurrentRow, 14].Text = item.ID;
                    DoctorType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 4, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 4)
                {
                    //��ȡѡ�е���Ϣ
                    Neusoft.NFC.Object.NeuObject item = null;
                    int rtn = DoctorType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //һ��A
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    //һ��A����
                    fpSpread1_Sheet1.Cells[CurrentRow, 15].Text = item.ID;
                    DoctorType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 5, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 5)
                {
                    //��ȡѡ�е���Ϣ
                    Neusoft.NFC.Object.NeuObject item = null;
                    int rtn = DoctorType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //����
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    //��������
                    fpSpread1_Sheet1.Cells[CurrentRow, 16].Text = item.ID;
                    DoctorType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 6, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 6)
                {
                    //��ȡѡ�е���Ϣ
                    Neusoft.NFC.Object.NeuObject item = null;
                    int rtn = NarcType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //����ʽ
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    NarcType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 8, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 8)
                {
                    //��ȡѡ�е���Ϣ
                    Neusoft.NFC.Object.NeuObject item = null;
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

                else if (fpSpread1_Sheet1.ActiveColumnIndex == 9)
                {
                    //��ȡѡ�е���Ϣ
                    Neusoft.NFC.Object.NeuObject item = null;
                    int rtn = ICDType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //ICD��ϱ���
                    fpSpread1_Sheet1.ActiveCell.Text = item.ID;
                    //ICD������� 
                    fpSpread1_Sheet1.Cells[CurrentRow, 1].Text = item.Name;
                    ICDType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 10, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 10)
                {
                    //��ȡѡ�е���Ϣ
                    Neusoft.NFC.Object.NeuObject item = null;
                    int rtn = NickType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //�п�����
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    NickType.Visible = false;

                    //�п����ϵȼ�
                    string strText = fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 7].Text;
                    //��ȡ/֮����ַ��� �滻/֮ǰ���ַ���
                    strText = item.Name + strText.Substring(GetStrPosition(strText, "/"));
                    fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 7].Text = strText;

                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 11, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 11)
                {
                    //��ȡѡ�е���Ϣ
                    Neusoft.NFC.Object.NeuObject item = null;
                    int rtn = CicaType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //��������
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    CicaType.Visible = false;

                    //�п����ϵȼ�
                    string strText = fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 7].Text;
                    //��ȡ /֮ǰ���ַ���  �滻 /֮����ַ���
                    strText = strText.Substring(0, GetStrPosition(strText, "/") + 1) + item.Name;
                    fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 7].Text = strText;

                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 12);
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
                            if (i == 1 || i == 2 || i == 3 || i == 4 || i == 5 || i == 6 || i == 8 || i == 9 || i == 10 || i == 11)
                            {
                                ProcessDept();
                            }
                            if (i == 0)
                            {
                                //��������
                                fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 1);
                            }
                            if (i == 7)
                            {
                                //�п���������
                                fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 8);
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

                    case 1:
                        //����������
                        //��ȡ��ǰ���ֵ
                        ICDType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //ɸѡ����
                        break;
                    case 9:
                        //����ICD�������
                        //��ȡ��ǰ���ֵ
                        ICDType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //ɸѡ����
                        break;

                    #region  ҽ��

                    case 2:
                        //��������A
                        //��ȡ��ǰ���ֵ
                        DoctorType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //ɸѡ����
                        break;
                    case 3:
                        //��������B
                        //��ȡ��ǰ���ֵ
                        DoctorType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //ɸѡ����
                        break;
                    case 4:
                        //����һ��
                        //��ȡ��ǰ���ֵ
                        DoctorType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //ɸѡ����
                        break;
                    case 5:
                        //���˶���
                        //��ȡ��ǰ���ֵ
                        DoctorType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //ɸѡ����
                        break;
                    case 8:
                        //��������ҽʦ����
                        //��ȡ��ǰ���ֵ
                        DoctorType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //ɸѡ����
                        break;

                    #endregion

                    case 6:
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
            if (intCol == 1 || intCol == 9)
            {
                ICDType.Location = new Point(panel1.Location.X + _cell.Location.X,
                    panel1.Location.Y + panel1.Location.Y + _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                //				ICDType.Size=new Size(_cell.Width+SystemInformation.Border3DSize.Width*2,150);
                ICDType.Size = new Size(200, 200);
            }
            //���� ҽ���������λ�� 
            else if (intCol == 2 || intCol == 3 || intCol == 4 || intCol == 5 || intCol == 8)
            {
                DoctorType.Location = new Point(panel1.Location.X + _cell.Location.X,
                    panel1.Location.Y + panel1.Location.Y + _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                //				DoctorType.Size=new Size(_cell.Width+SystemInformation.Border3DSize.Width*2,150);
                DoctorType.Size = new Size(200, 150);
            }
            //���� ����ʽ �������λ�� 
            else if (intCol == 6)
            {
                NarcType.Location = new Point(panel1.Location.X + _cell.Location.X,
                    panel1.Location.Y + panel1.Location.Y + _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                //				NarcType.Size=new Size(_cell.Width+SystemInformation.Border3DSize.Width*2,150);
                NarcType.Size = new Size(150, 100);
            }
            //���� �п� �������λ�� 
            else if (intCol == 10)
            {
                NickType.Location = new Point(panel1.Location.X + _cell.Location.X,
                    panel1.Location.Y + panel1.Location.Y + _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                //				NickType.Size=new Size(_cell.Width+SystemInformation.Border3DSize.Width*2,150);
                NickType.Size = new Size(150, 100);
            }
            //���� ���� �������λ�� 
            else if (intCol == 11)
            {
                CicaType.Location = new Point(panel1.Location.X + _cell.Location.X,
                    panel1.Location.Y + panel1.Location.Y + _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
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
                if (intCol != 1 && intCol != 9)
                {
                    ICDType.Visible = false;
                }
                //���� ҽ��������Ŀɼ���
                if (intCol != 2 || intCol != 3 || intCol != 4 || intCol != 5 || intCol != 8)
                {
                    DoctorType.Visible = false;
                }
                //���� ��������������Ŀɼ���
                if (intCol != 6)
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
                if (intCol == 1 || intCol == 9)
                {
                    ICDType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                    ICDType.Visible = true;
                }
                //ҽ��
                else if (intCol == 2 || intCol == 3 || intCol == 4 || intCol == 5 || intCol == 8)
                {
                    DoctorType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                    DoctorType.Visible = true;
                }
                //����ʽ
                else if (intCol == 6)
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
            //				Neusoft.NFC.Interface.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1,filePath);
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
            //Neusoft.UFC.Common.Controls.ucSetColumn uc = new Neusoft.UFC.Common.Controls.ucSetColumn();
            //uc.FilePath = this.filePath;
            //uc.GoDisplay += new Neusoft.UFC.Common.Controls.ucSetColumn.DisplayNow(uc_GoDisplay);
            //Neusoft.NFC.Interface.Classes.Function.PopShowControl(uc);
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
            this.fpSpread1_Sheet1.Rows.Remove(fpSpread1_Sheet1.ActiveRowIndex, 1);
            return 1;
        }
        #endregion

        #region ����
        /// <summary>
        /// ���ص�ǰ������
        /// </summary>
        /// <returns></returns>
        [Obsolete("����", true)]
        public int GetfpSpread1RowCount()
        {
            return this.fpSpread1_Sheet1.Rows.Count;
        }
        /// <summary>
        /// ���û��Ԫ��
        /// </summary>
        [Obsolete("����", true)]
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
        #endregion 
    }
}
