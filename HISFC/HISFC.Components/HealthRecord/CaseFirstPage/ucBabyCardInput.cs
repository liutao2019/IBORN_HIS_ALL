using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.HealthRecord.EnumServer;
namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    /// <summary>
    /// ucBabyCardInput<br></br>
    /// [��������: ������Ӥ��Ϣ¼��]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-04-20]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucBabyCardInput : UserControl
    {
        public ucBabyCardInput()
        {
            InitializeComponent();
        }

        #region ȫ�ֱ���
        public DataTable dtBaby = new DataTable("��Ӥ��");
        //		//ICD �����Ϣ �б�
        //		private FS.FrameWork.WinForms.Controls.PopUpListBox ICDType = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        //����
        //		private FS.FrameWork.WinForms.Controls.PopUpListBox BreathType = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        private FS.FrameWork.Public.ObjectHelper BreathTypeHelper = new FS.FrameWork.Public.ObjectHelper();
        //ת��
        //		private FS.FrameWork.WinForms.Controls.PopUpListBox BabyStateType = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        private FS.FrameWork.Public.ObjectHelper BabyStateTypeHelper = new FS.FrameWork.Public.ObjectHelper();
        //������
        //		private FS.FrameWork.WinForms.Controls.PopUpListBox BirthEndType = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        private FS.FrameWork.Public.ObjectHelper BirthEndTypeHelper = new FS.FrameWork.Public.ObjectHelper();
        //�Ա�
        //		private FS.FrameWork.WinForms.Controls.PopUpListBox SexType = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        private FS.FrameWork.Public.ObjectHelper SexTypeHelper = new FS.FrameWork.Public.ObjectHelper();
        //��ʶ��ҽ��վ ���� ������
        //		private string frmType = "";
        //�����ļ�·��
        private string filePath = Application.StartupPath + "\\profile\\ucBabyCardInput.xml";
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
        #endregion

        #region  ����
        /// <summary>
        /// ������Ϣ
        /// </summary>
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get
            {
                return patientInfo;
            }
            set
            {
                patientInfo = value;
            }
        }
        private string isHaveBaby = string.Empty;
        /// <summary>
        /// ȷ���Ƿ���ڸ�Ӥ����Ϣ
        /// </summary>
        public string IsHavedBaby
        {
            get
            {
                if (this.cmbIsHaveBaby.Tag == null)
                {
                    this.isHaveBaby = string.Empty;
                }
                else
                {
                    this.isHaveBaby = this.cmbIsHaveBaby.Tag.ToString();
                }
                return this.isHaveBaby;
            }
            set
            {
                this.isHaveBaby = value;
                this.cmbIsHaveBaby.Tag = this.isHaveBaby;
            }
        }
        #endregion

        #region  ����
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
        /// �޶���Ŀ��Ⱥܿɼ��� 
        /// </summary>
        private void LockFpEnter()
        {
            this.fpEnter1_Sheet1.Columns[0].Width = 38; //�Ա�
            this.fpEnter1_Sheet1.Columns[1].Width = 59;//������
            this.fpEnter1_Sheet1.Columns[2].Width = 100;//����
            this.fpEnter1_Sheet1.Columns[3].Width = 40; //ת��
            this.fpEnter1_Sheet1.Columns[4].Width = 80; //����
            this.fpEnter1_Sheet1.Columns[5].Width = 40; //Ժ�ڸ�Ⱦ����
            this.fpEnter1_Sheet1.Columns[5].Visible = false;
            this.fpEnter1_Sheet1.Columns[6].Width = 150; //ҽԺ��Ⱦ����
            this.fpEnter1_Sheet1.Columns[6].Visible = false;
            this.fpEnter1_Sheet1.Columns[7].Width = 80; //ICD-10����
            this.fpEnter1_Sheet1.Columns[7].Visible = false;
            this.fpEnter1_Sheet1.Columns[7].Locked = true; //ICD-10����
            this.fpEnter1_Sheet1.Columns[8].Width = 40; //���ȴ��� 
            this.fpEnter1_Sheet1.Columns[9].Width = 40; //���ȳɹ����� 
            this.fpEnter1_Sheet1.Columns[10].Visible = false; //���
        }
        /// <summary>
        /// ���ԭ�е�����
        /// </summary>
        /// <returns></returns>
        public int ClearInfo()
        {
            if (this.dtBaby != null)
            {
                this.dtBaby.Clear();
                LockFpEnter();
            }
            else
            {
                MessageBox.Show("��Ӥ��Ϊnull");
            }
            return 1;
        }
        /// <summary>
        /// ��farpoint ����Ϊֻ��
        /// </summary>
        /// <returns></returns>
        public int SetReadOnly(bool type)
        {
            if (type)
            {
                this.btAdd.Enabled = !type;
                this.btDelete.Enabled = !type;
                this.fpEnter1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
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
        public int ValueState(ArrayList list)
        {
            if (list == null)
            {
                return -2;
            }
            foreach (FS.HISFC.Models.HealthRecord.Baby obj in list)
            {
                if (obj.InpatientNo == "" || obj.InpatientNo == null)
                {
                    MessageBox.Show("��Ӥ��Ϣ סԺ�Ų���Ϊ��");
                    return -1;
                }
                if (obj.InpatientNo.Length > 14)
                {
                    MessageBox.Show("��Ӥ��Ϣ סԺ��ˮ�Ź���");
                    return -1;
                }
                if (obj.Weight <= 0)
                {
                    MessageBox.Show("��Ӥ��Ϣ ���ز���С�ڻ������");
                    return -1;
                }
                else if (obj.Weight > 99999.99)
                {
                    MessageBox.Show("��Ӥ��Ϣ �������ֹ���");
                    return -1;
                }

                if (obj.InfectNum < 0)
                {
                    MessageBox.Show("��Ӥ��Ϣ ��Ⱦ��������С����");
                    return -1;
                }
                else if (obj.InfectNum > 999)
                {
                    MessageBox.Show("��Ӥ��Ϣ ��Ⱦ��������");
                    return -1;
                }
                if (obj.SalvNum < 0)
                {
                    MessageBox.Show("��Ӥ��Ϣ ���ȴ�������С����");
                    return -1;
                }
                else if (obj.SalvNum > 99)
                {
                    MessageBox.Show("��Ӥ��Ϣ ���ȴ�������");
                    return -1;
                }

                if (obj.SuccNum < 0)
                {
                    MessageBox.Show("��Ӥ��Ϣ �ɹ���������С����");
                    return -1;
                }
                else if (obj.SuccNum > 99)
                {
                    MessageBox.Show("��Ӥ��Ϣ �ɹ���������");
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
        /// <summary>
        /// ɾ���հ׵���
        /// </summary>
        /// <returns></returns>
        public int deleteRow()
        {
            if (fpEnter1_Sheet1.Rows.Count == 1)
            {
                //��һ�б���Ϊ�� 
                if (fpEnter1_Sheet1.Cells[0, 0].Text == "" && fpEnter1_Sheet1.Cells[0, 1].Text == "")
                {
                    fpEnter1_Sheet1.Rows.Remove(0, 1);
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
                this.dtBaby.AcceptChanges();
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
            dtBaby.AcceptChanges();
            LockFpEnter();
            return 0;
        }
        /// <summary>
        /// ���ص�ǰ����
        /// </summary>
        /// <returns></returns>
        public int GetfpSpreadRowCount()
        {
            return fpEnter1_Sheet1.Rows.Count;
        }
        private int AddInfoToTable(ArrayList list)
        {
            if (this.dtBaby != null)
            {
                this.dtBaby.Clear();
                this.dtBaby.AcceptChanges();
            }
            if (list == null)
            {
                return -1;
            }
            //ѭ����������
            foreach (FS.HISFC.Models.HealthRecord.Baby info in list)
            {
                DataRow row = dtBaby.NewRow();
                SetRow(row, info);
                dtBaby.Rows.Add(row);
            }
            if ((this.patientInfo.CaseState == "2" || this.patientInfo.CaseState == "3"))
            {
                //���ı�־
                dtBaby.AcceptChanges();
            }
            LockFpEnter();
            return 0;
        }
        /// <summary>
        /// ��ѯ����ʾ����
        /// </summary>
        /// <returns>�������� ��1 ���� 0 �������в���1  </returns>
        public int LoadInfo(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            if (patient == null)
            {
                return -1;
            }
            patientInfo = patient;
            if (patientInfo.CaseState == "0")
            {
                //�������в���
                return 1;
            }
            FS.HISFC.BizLogic.HealthRecord.Baby ba = new FS.HISFC.BizLogic.HealthRecord.Baby();

            //��ѯ��������������
            ArrayList list = ba.QueryBabyByInpatientNo(patientInfo.ID);
            AddInfoToTable(list); 
            return 0;
        }
        /// <summary>
        /// ��ѯ���Ĺ����������� 
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool GetList(string strType, ArrayList list)
        {
            try
            {
                this.fpEnter1.StopCellEditing();
                foreach (DataRow dr in this.dtBaby.Rows)
                {
                    dr.EndEdit();
                }
                switch (strType)
                {
                    case "A":
                        //���ӵ�����
                        DataTable AddTable = this.dtBaby.GetChanges(DataRowState.Added);
                        GetChangeInfo(AddTable, list);
                        break;
                    case "M":
                        DataTable ModTable = this.dtBaby.GetChanges(DataRowState.Modified);
                        GetChangeInfo(ModTable, list);
                        break;
                    case "D":
                        DataTable DelTable = this.dtBaby.GetChanges(DataRowState.Deleted);
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
                FS.HISFC.Models.HealthRecord.Baby info = null;
                foreach (DataRow row in tempTable.Rows)
                {
                    info = new FS.HISFC.Models.HealthRecord.Baby();
                    //סԺ��ˮ��
                    info.InpatientNo = this.patientInfo.ID;
                    if (row["�Ա�"] != DBNull.Value)
                    {
                        info.SexCode = this.SexTypeHelper.GetID(row["�Ա�"].ToString());
                    }
                    if (row["������"] != DBNull.Value)
                    {
                        info.BirthEnd = this.BirthEndTypeHelper.GetID(row["������"].ToString());
                    }
                    if (row["����"] != DBNull.Value)
                    {
                        info.Weight = FS.FrameWork.Function.NConvert.ToInt32(row["����"].ToString());
                    }
                    if (row["ת��"] != DBNull.Value)
                    {
                        string sss = this.BabyStateTypeHelper.GetID(row["ת��"].ToString());
                        info.BabyState = this.BabyStateTypeHelper.GetID(row["ת��"].ToString());
                    }
                    if (row["����"] != DBNull.Value)
                    {
                        info.Breath = this.BreathTypeHelper.GetID(row["����"].ToString());
                    }
                    if (row["Ժ�ڸ�Ⱦ��"] != DBNull.Value)
                    {
                        info.InfectNum = FS.FrameWork.Function.NConvert.ToInt32(row["Ժ�ڸ�Ⱦ��"].ToString());
                    }
                    if (row["ҽԺ��Ⱦ����"] != DBNull.Value)
                    {
                        info.Infect.Name = row["ҽԺ��Ⱦ����"].ToString();
                    }
                    if (row["ICD-10����"] != DBNull.Value)
                    {
                        info.Infect.ID = row["ICD-10����"].ToString();
                    }
                    if (row["���ȴ���"] != DBNull.Value)
                    {
                        info.SalvNum = FS.FrameWork.Function.NConvert.ToInt32(row["���ȴ���"].ToString());
                    }
                    if (row["���ȳɹ�����"] != DBNull.Value)
                    {
                        info.SuccNum = FS.FrameWork.Function.NConvert.ToInt32(row["���ȳɹ�����"].ToString());
                    }
                    if (row["���"] != DBNull.Value)
                    {
                        info.HappenNum = FS.FrameWork.Function.NConvert.ToInt32(row["���"].ToString());
                    }
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
        /// ��ʵ���е�ֵ��ֵ��row��
        /// </summary>
        /// <param name="row">�����row</param>
        /// <param name="info">�����ʵ��</param>
        private void SetRow(DataRow row, FS.HISFC.Models.HealthRecord.Baby info)
        {
            row["�Ա�"] = this.SexTypeHelper.GetName(info.SexCode);
            row["������"] = this.BirthEndTypeHelper.GetName(info.BirthEnd);
            row["����"] = info.Weight;
            row["ת��"] = this.BabyStateTypeHelper.GetName(info.BabyState);
            row["����"] = this.BreathTypeHelper.GetName(info.Breath);
            row["Ժ�ڸ�Ⱦ��"] = info.InfectNum;
            row["ҽԺ��Ⱦ����"] = info.Infect.Name;
            row["ICD-10����"] = info.Infect.ID;
            row["���ȴ���"] = info.SalvNum;
            row["���ȳɹ�����"] = info.SuccNum;
            row["���"] = info.HappenNum;
        }
        /// <summary>
        /// ö��
        /// </summary>
        enum Cols
        {
            SexCode,//�Ա�
            BirthEnd,//������
            Weight,//����
            BabyState,//ת��
            Breath,//����
            InfectNum,//Ժ�ڸ�Ⱦ��
            InfectName,//ҽԺ��Ⱦ����
            Infect,//ICD-10����
            SalvNum,//���ȴ���
            SuccNum,//���ȳɹ�����
            HappenNum//���
        }
        /// <summary>
        /// ��ʼ�������б� ��ѯ���ݵ�
        /// </summary>
        /// <returns> -1 ����</returns>
        public int InitInfo()
        {
            //��ʼ��������������Դ
            InitDateTable();
            InitList();
            fpEnter1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White; 
            //FS.HISFC.BizLogic.HealthRecord.ICD icdMgr = new FS.HISFC.BizLogic.HealthRecord.ICD();
            //ArrayList icdList = icdMgr.Query(ICDTypes.ICD10, QueryTypes.Valid);
            //this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, 6, icdList);
            //this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, 7, icdList);
            return 0;
        }
        private int InitList()
        {
            try
            {
                FS.HISFC.BizLogic.HealthRecord.Baby cbaby = new FS.HISFC.BizLogic.HealthRecord.Baby();
                FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
                this.fpEnter1.SelectNone = true;
                //�Ա�
                ArrayList listSex = FS.HISFC.Models.Base.SexEnumService.List();// cbaby.GetSex();
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, 0, listSex);
                SexTypeHelper.ArrayObject = listSex;
                //������
                ArrayList listbaby = con.GetList(FS.HISFC.Models.Base.EnumConstant.CHILDBEARINGRESULT);//cbaby.GetBirthEnd();
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, 1, listbaby);
                BirthEndTypeHelper.ArrayObject = listbaby;

                //ת��
                ArrayList listBabyState = con.GetList("BABYZG"); //con.GetList(FS.HISFC.Models.Base.EnumConstant.ZG);// FS.HISFC.Managementcbaby.GetBabyState();
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, 3, listBabyState);
                BabyStateTypeHelper.ArrayObject = listBabyState;

                //����
                ArrayList listbreath = con.GetList(FS.HISFC.Models.Base.EnumConstant.BREATHSTATE); //cbaby.GetBreath();
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, 4, listbreath);
                BreathTypeHelper.ArrayObject = listbreath;
                //�ӳ������л�ȡ�Ƿ���Ҫ��Ӥ������ѡ��
                ArrayList listHavedBaby = con.GetList("CASEHAVEDBABY");
                if (listHavedBaby != null && listHavedBaby.Count > 0)
                {
                    this.label1.Visible = true;
                    this.cmbIsHaveBaby.Visible = true;
                }
                else
                {
                    this.label1.Visible = false;
                    this.cmbIsHaveBaby.Visible = false;
                }
                ArrayList Havedlist = con.GetList("CASENOTORHAVED");
                if (Havedlist != null)
                {
                    this.cmbIsHaveBaby.AddItems(Havedlist);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 0;

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

                dtBaby.Columns.AddRange(new DataColumn[]{
															new DataColumn("�Ա�", strType),	//0
															new DataColumn("������", strType),	 //1
															new DataColumn("����", floatType),//2
															new DataColumn("ת��", strType),//3
															new DataColumn("����", strType),//4
															new DataColumn("Ժ�ڸ�Ⱦ��", intType),//5
															new DataColumn("ҽԺ��Ⱦ����", strType),//6
															new DataColumn("ICD-10����", strType), //7
															new DataColumn("���ȴ���", intType),//8
															new DataColumn("���ȳɹ�����", intType),//9
															new DataColumn("���", intType)});//10
                //������Դ
                this.fpEnter1_Sheet1.DataSource = dtBaby;
                //				//����fpSpread1 ������
                //				if(System.IO.File.Exists(filePath))
                //				{
                //					FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpEnter1_Sheet1,filePath);
                //				}
                //				else
                //				{
                //					FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpEnter1_Sheet1,filePath);
                //				}
                LockFpEnter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ucBabyCardInput_Load(object sender, System.EventArgs e)
        {
            //������Ӧ�����¼�
            fpEnter1.KeyEnter += new FS.FrameWork.WinForms.Controls.NeuFpEnter.keyDown(fpEnter1_KeyEnter);
            fpEnter1.SetItem += new FS.FrameWork.WinForms.Controls.NeuFpEnter.setItem(fpEnter1_SetItem);
            fpEnter1.ShowListWhenOfFocus = true;
        }
        private int fpEnter1_SetItem(FS.FrameWork.Models.NeuObject obj)
        {
            this.ProcessDept();
            return 0;
        }
        /// <summary>
        /// �����س����� ������ȡ������
        /// </summary>
        /// <returns></returns>
        private int ProcessDept()
        {
            try
            {
                int CurrentRow = fpEnter1_Sheet1.ActiveRowIndex;
                if (CurrentRow < 0) return 0;

                if (fpEnter1_Sheet1.ActiveColumnIndex == 0)
                {
                    //��ȡѡ�е���Ϣ
                    FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, 0);
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = listBox.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //�Ա�
                    fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                    fpEnter1.Focus();
                    fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, 1);
                    return 0;
                }
                else if (fpEnter1_Sheet1.ActiveColumnIndex == 1)
                {
                    FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, 1);
                    //��ȡѡ�е���Ϣ
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = listBox.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    // ������
                    fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                    fpEnter1.Focus();
                    fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, 2);
                    return 0;
                }
                else if (fpEnter1_Sheet1.ActiveColumnIndex == 3)
                {
                    FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, 3);
                    //��ȡѡ�е���Ϣ
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = listBox.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //ת��
                    fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                    fpEnter1.Focus();
                    fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, 4);
                    return 0;
                }
                else if (fpEnter1_Sheet1.ActiveColumnIndex == 4)
                {
                    FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, 4);
                    //��ȡѡ�е���Ϣ
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = listBox.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //����
                    fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                    fpEnter1.Focus();
                    fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, 5);
                    return 0;
                }
                else if (fpEnter1_Sheet1.ActiveColumnIndex == 6)
                {
                    FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, 6);
                    //��ȡѡ�е���Ϣ
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = listBox.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //ICD������� 
                    fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                    //ICD��ϱ��� 
                    fpEnter1_Sheet1.Cells[CurrentRow, 7].Text = item.ID;
                    fpEnter1.Focus();
                    fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, 8);
                    return 0;
                }
                else if (fpEnter1_Sheet1.ActiveColumnIndex == 7)
                {
                    FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, 7);
                    //��ȡѡ�е���Ϣ
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = listBox.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //ICD����
                    fpEnter1_Sheet1.ActiveCell.Text = item.ID;
                    //ICD����
                    fpEnter1_Sheet1.Cells[CurrentRow, 6].Text = item.Name;
                    fpEnter1.Focus();
                    fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, 8);
                    return 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                //				MessageBox.Show("Enter,�����Լ����Ӵ����¼�������������һcell");
                //�س�
                if (this.fpEnter1.ContainsFocus)
                {
                    int i = this.fpEnter1_Sheet1.ActiveColumnIndex;
                    if (i == 0 || i == 1 || i == 3 || i == 4 || i == 6 || i == 7)
                    {
                        ProcessDept();
                    }
                    else if (i == 9)
                    {
                        if (fpEnter1_Sheet1.ActiveRowIndex < fpEnter1_Sheet1.Rows.Count - 1)
                        {
                            fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex + 1, 0);
                        }
                        else
                        {
                            //����һ��
                            this.AddRow();
                        }
                    }
                    else
                    {
                        if (i <= 8)
                        {
                            fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, i + 1);
                        }
                    }
                }
            }
            else if (key == Keys.Up)
            {
                //				MessageBox.Show("Up,�����Լ����Ӵ����¼��������������б�ʱ���������У���ʾ�����ؼ�ʱ���������ؼ������ƶ�");
            }
            else if (key == Keys.Down)
            {
                //				MessageBox.Show("Down�������Լ����Ӵ����¼��������������б�ʱ���������У���ʾ�����ؼ�ʱ���������ؼ������ƶ�");
            }
            else if (key == Keys.Escape)
            {
                if (fpEnter1_Sheet1.ActiveColumnIndex == 6 || fpEnter1_Sheet1.ActiveColumnIndex == 7)
                {
                    fpEnter1_Sheet1.Cells[fpEnter1_Sheet1.ActiveRowIndex, 6].Text = "";
                    fpEnter1_Sheet1.Cells[fpEnter1_Sheet1.ActiveRowIndex, 7].Text = "";
                }
            }
            return 0;
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            return base.ProcessDialogKey(keyData);
        }

        //����һ����Ŀ
        public int AddRow()
        {
            try
            {
                if (fpEnter1_Sheet1.Rows.Count < 1)
                {
                    //����һ�п�ֵ
                    DataRow row = dtBaby.NewRow();
                    row["����"] = 0;
                    row["���ȴ���"] = 0;
                    row["���ȳɹ�����"] = 0;
                    dtBaby.Rows.Add(row);
                }
                else
                {
                    //����һ��
                    int j = fpEnter1_Sheet1.Rows.Count;
                    this.fpEnter1_Sheet1.Rows.Add(j, 1);
                    for (int i = 0; i < fpEnter1_Sheet1.Columns.Count; i++)
                    {
                        fpEnter1_Sheet1.Cells[j, i].Value = fpEnter1_Sheet1.Cells[j - 1, i].Value;
                    }
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
        /// ����һ����Ŀ
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
        private void fpEnter1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            //����fpSpread1 ������
            if (System.IO.File.Exists(filePath))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpEnter1_Sheet1, filePath);
            }

        }
        /// <summary>
        /// ����fpSpread1_Sheet1�Ŀ��ȵ� ����󴥷����¼�
        /// </summary>
        private void uc_GoDisplay()
        {
            //			LoadInfo(inpatientNo,operType); //���¼�������

        }
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem1_Click(object sender, System.EventArgs e)
        {
            Common.Controls.ucSetColumn uc = new Common.Controls.ucSetColumn();
            uc.FilePath = this.filePath;
            //uc.DisplayEvent += new EventHandler(uc_GoDisplay);
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
        }

        private void menuItem2_Click(object sender, System.EventArgs e)
        {
            if (fpEnter1_Sheet1.Rows.Count > 0)
            {
                fpEnter1_Sheet1.Rows.Remove(fpEnter1_Sheet1.ActiveRowIndex, 1);
            }
        }
        #endregion 

        private void btAdd_Click(object sender, EventArgs e)
        {
            //this.AddRow();
            this.InsertRow();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            this.DeleteActiveRow();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.AddRow();
        }
    }
}