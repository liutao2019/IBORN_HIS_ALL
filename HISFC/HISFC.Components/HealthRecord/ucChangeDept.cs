using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace UFC.HealthRecord
{
    public partial class ucChangeDept : Neusoft.NFC.Interface.Controls.ucBaseControl
    {
        public ucChangeDept()
        {
            InitializeComponent();
        }

        #region  ȫ�ֱ���
        //�����ļ�·��
        private System.Data.DataTable dtTable = new DataTable("����");
        private Neusoft.HISFC.Object.RADT.PatientInfo patient = new Neusoft.HISFC.Object.RADT.PatientInfo();
        private Neusoft.HISFC.Management.HealthRecord.DeptShift deptShift = new Neusoft.HISFC.Management.HealthRecord.DeptShift();
        private string inPatientNO = "";
        #endregion

        #region ����
        #endregion

        #region ����

        #region ����һ�� 
        //���һ����Ŀ
        private int AddRow()
        {
            try
            {
                if (fpEnter1_Sheet1.Rows.Count < 1)
                {
                    //����һ�п�ֵ
                    DataRow row = dtTable.NewRow();
                    row["���"] = "1";
                    row["ת������"] = System.DateTime.Now;
                    dtTable.Rows.Add(row);
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
        #endregion 

        #region   ɾ��һ��
        /// <summary>
        /// ɾ����ǰ�� 
        /// </summary>
        /// <returns></returns>
        private int DeleteActiveRow()
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
        public override int Save(object sender, object neuObject)
        {
            Neusoft.NFC.Management.Transaction trans = new Neusoft.NFC.Management.Transaction(deptShift.Connection);
            #region ת����Ϣ

            //ɾ����
            ArrayList deptDel = new ArrayList();
            //���ӵ� 
            ArrayList deptAdd = new ArrayList();
            //�޸Ĺ��� 
            ArrayList deptMod = new ArrayList();

            #region ������Ϣ�����ϵ�ת����Ϣ
            //#region ��һ��ת����Ϣ

            //if (firstDept.Tag != null) //��������� 1��tag��Ϊ�� ��firDeptΪ�� ���Ų�������û��ά���� �������ӵ� 2��tag��Ϊ�� firDept��Ϊ�� firDept.Dept.ID ������tag ���޸ĵ�
            //{						  //3 ��firDept��Ϊ��  tag Ϊ�� ��ɾ���� ��
            //    if (firDept == null || CaseBase.PatientInfo.CaseState == "1")//�����ӵ�
            //    {
            //        firDept = new Neusoft.HISFC.Object.RADT.Location();
            //        firDept.User02 = CaseBase.PatientInfo.ID;
            //        firDept.Dept.Name = firstDept.Text;
            //        firDept.Dept.ID = firstDept.Tag.ToString();
            //        firDept.User01 = this.dateTimePicker3.Value.ToString();

            //        deptAdd.Add(firDept);
            //    }
            //    else //�޸Ĺ���
            //    {
            //        firDept.User02 = CaseBase.PatientInfo.ID;
            //        firDept.Dept.Name = firstDept.Text;
            //        firDept.Dept.ID = firstDept.Tag.ToString();
            //        firDept.User01 = this.dateTimePicker3.Value.ToString();

            //        deptMod.Add(firDept);
            //    }

            //}
            //else if (firDept != null && firstDept.Tag == null) //ɾ����
            //{
            //    deptDel.Add(firDept);
            //}
            //#endregion
            //#region  �ڶ���ת����Ϣ
            //if (deptSecond.Tag != null)
            //{
            //    if (secDept == null || CaseBase.PatientInfo.CaseState == "1") //���ӵ� 
            //    {
            //        secDept = new Neusoft.HISFC.Object.RADT.Location();
            //        secDept.User02 = CaseBase.PatientInfo.ID;
            //        this.secDept.Dept.Name = deptSecond.Text;
            //        this.secDept.Dept.ID = deptSecond.Tag.ToString();
            //        secDept.User01 = this.dateTimePicker3.Value.ToString();

            //        deptAdd.Add(secDept);
            //    }
            //    else //�޸Ĺ��� 
            //    {
            //        secDept.User02 = CaseBase.PatientInfo.ID;
            //        this.secDept.Dept.Name = deptSecond.Text;
            //        this.secDept.Dept.ID = deptSecond.Tag.ToString();
            //        secDept.User01 = this.dateTimePicker3.Value.ToString();

            //        deptMod.Add(secDept);
            //    }
            //}
            //else if (secDept != null && deptSecond.Tag == null)
            //{
            //    deptDel.Add(secDept);
            //}
            //#endregion
            //#region ������ת��
            //if (deptThird.Tag != null)
            //{
            //    if (thirDept == null || CaseBase.PatientInfo.CaseState == "1")
            //    {
            //        thirDept = new Neusoft.HISFC.Object.RADT.Location();
            //        thirDept.User02 = CaseBase.PatientInfo.ID;
            //        this.thirDept.Dept.Name = deptThird.Text;
            //        this.thirDept.Dept.ID = deptThird.Tag.ToString();
            //        thirDept.User01 = this.dateTimePicker3.Value.ToString();

            //        deptAdd.Add(thirDept);
            //    }
            //    else
            //    {
            //        thirDept.User02 = CaseBase.PatientInfo.ID;
            //        this.thirDept.Dept.Name = deptThird.Text;
            //        this.thirDept.Dept.ID = deptThird.Tag.ToString();
            //        thirDept.User01 = this.dateTimePicker3.Value.ToString();

            //        deptMod.Add(thirDept);
            //    }
            //}
            //else if (thirDept != null && deptThird.Tag == null) //ɾ����
            //{
            //    deptDel.Add(thirDept);
            //}
            //#endregion
            #endregion

            //ɾ���հ���
            deleteRow();
            GetList("D", deptDel);
            GetList("A", deptAdd);
            GetList("M", deptMod);

            if (ValueState(deptDel) == -1 || ValueState(deptAdd) == -1 || ValueState(deptMod) == -1)
            {
                trans.RollBack();
                return -3;
            }
            if (deptDel != null)
            {
                foreach (Neusoft.HISFC.Object.RADT.Location obj in deptDel)
                {
                    if (deptShift.DeleteChangeDept(obj) < 1)
                    {
                        trans.RollBack();
                        MessageBox.Show("ת����Ϣ����ʧ��" + deptShift.Err);
                        return -1;
                    }
                }
            }
            if (deptAdd != null)
            {
                foreach (Neusoft.HISFC.Object.RADT.Location obj in deptAdd)
                {
                    if (deptShift.InsertChangeDept(obj) < 1)
                    {
                        trans.RollBack();
                        MessageBox.Show("ת����Ϣ����ʧ��" + deptShift.Err);
                        return -1;
                    }
                }
            }
            if (deptMod != null)
            {
                foreach (Neusoft.HISFC.Object.RADT.Location obj in deptMod)
                {
                    if (deptShift.UpdateChangeDept(obj) < 1)
                    {
                        trans.RollBack();
                        MessageBox.Show("ת����Ϣ����ʧ��" + deptShift.Err);
                        return -1;
                    }
                }
            }
            //��ѯ���������Ϣ
            ArrayList tempChangeDept = deptShift.QueryChangeDeptFromShiftApply(inPatientNO, "2");
            #endregion 

            trans.Commit();
            MessageBox.Show("����ɹ�");
            return 1;
        }
        #endregion 

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
        /// �޶���Ŀ�Ⱥܿɼ��� 
        /// </summary>
        private void LockFpEnter()
        {
            this.fpEnter1_Sheet1.Columns[0].Width = 220; //���ұ���
            this.fpEnter1_Sheet1.Columns[1].Width = 129;//��������
            this.fpEnter1_Sheet1.Columns[1].Locked = true;
            this.fpEnter1_Sheet1.Columns[2].Width = 119;//ת������
            this.fpEnter1_Sheet1.Columns[3].Visible = false; //���
        }
        /// <summary>
        /// ���ԭ�е�����
        /// </summary>
        /// <returns></returns>
        public int ClearInfo()
        {
            if (this.dtTable != null)
            {
                this.dtTable.Clear();
                LockFpEnter();
            }
            else
            {
                MessageBox.Show("ת�Ʊ�Ϊnull");
            }
            return 1;
        }
        public int SetReadOnly(bool type)
        {
            if (type)
            {
                this.fpEnter1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            }
            else
            {
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
            foreach (Neusoft.HISFC.Object.RADT.Location obj in list)
            {
                if (obj.User02 == "" || obj.User02 == null)
                {
                    MessageBox.Show("ת����ϢסԺ��ˮ�Ų���Ϊ��");
                    return -1;
                }
                if (obj.User02.Length > 14)
                {
                    MessageBox.Show("ת����ϢסԺ��ˮ�Ź���");
                    return -1;
                }
                if (obj.Dept.ID == "" || obj.Dept.ID == null)
                {
                    MessageBox.Show("ת����Ϣ���ұ��벻��Ϊ��");
                    return -1;
                }
                if (obj.Dept.ID.Length > 4)
                {
                    MessageBox.Show("ת����Ϣ ���ұ������");
                    return -1;
                }
                //				if(obj.Dept.Name == "" ||obj.Dept.Name == null)
                //				{
                //					MessageBox.Show("ת����Ϣ�������Ʋ���Ϊ��");
                //					return -1;
                //				}
                if (obj.Dept.Name.Length > 16)
                {
                    MessageBox.Show("ת����Ϣ �������ƹ���");
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
                this.dtTable.AcceptChanges();
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
        /// creator:zhangjunyi@Neusoft.com
        /// </summary>
        /// <param name="reset"></param>
        /// <returns></returns>
        public bool Reset(bool reset)
        {
            if (reset)
            {
                //������� ������� 
                if (dtTable != null)
                {
                    dtTable.Clear();
                    dtTable.AcceptChanges();
                }
            }
            else
            {
                //�������
                dtTable.AcceptChanges();
            }
            return true;
        }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void InitInfo()
        {
            try
            {
                //��ʼ����
                InitDatedtTable();
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
        public bool GetList(string strType, ArrayList list)
        {
            try
            {
                this.fpEnter1.StopCellEditing();
                switch (strType)
                {
                    case "A":
                        //���ӵ�����
                        DataTable AddTable = this.dtTable.GetChanges(DataRowState.Added);
                        GetChangeInfo(AddTable, list);
                        break;
                    case "M":
                        DataTable ModTable = this.dtTable.GetChanges(DataRowState.Modified);
                        GetChangeInfo(ModTable, list);
                        break;
                    case "D":
                        DataTable DelTable = this.dtTable.GetChanges(DataRowState.Deleted);
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
        /// ɾ���հ׵���
        /// </summary>
        /// <returns></returns>
        public int deleteRow()
        {
            if (fpEnter1_Sheet1.Rows.Count == 1)
            {
                if (fpEnter1_Sheet1.Cells[0, 0].Text == "" || fpEnter1_Sheet1.Cells[0, 1].Text == "")
                {
                    fpEnter1_Sheet1.Rows.Remove(0, 1);
                }
            }
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
                Neusoft.HISFC.Object.RADT.Location info = null;
                foreach (DataRow row in tempTable.Rows)
                {
                    info = new Neusoft.HISFC.Object.RADT.Location();
                    info.User02 = this.patient.ID;
                    info.Dept.ID = row["���ұ���"].ToString(); //0
                    info.Dept.Name = row["��������"].ToString();//1
                    info.User01 = row["ת������"].ToString();//2
                    info.User03 = row["���"].ToString(); //3
                    info.Floor = "0"; //�ڿ�����
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
        /// �������� 
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="deptList"></param>
        /// <returns></returns>
        public int LoadInfo(Neusoft.HISFC.Object.RADT.PatientInfo patientInfo, ArrayList deptList)
        {
            if (deptList == null || patientInfo == null)
            {
                return -1;
            }
            patient = patientInfo;
            if (deptList.Count <= 3)
            {
                return 0;
            }
            AddInfoToTable(deptList);
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
            dtTable.AcceptChanges();
            return 0;
        }
        /// <summary>
        /// ��ѯ�����Ϣ�������ı���
        /// </summary>
        private void AddInfoToTable(ArrayList alReturn)
        {
            if (this.dtTable != null)
            {
                this.dtTable.Clear();
                this.dtTable.AcceptChanges();
            }
            //ѭ��������Ϣ
            int i = 0;
            foreach (Neusoft.HISFC.Object.RADT.Location obj in alReturn)
            {
                if (i > 2) //��ͷ�����ڻ�����Ϣ�������Ѿ���ʾ�� ����洢����������ʾ��֮���
                {
                    DataRow row = dtTable.NewRow();
                    SetRow(obj, row);
                    dtTable.Rows.Add(row);
                }
                else
                {
                    i++;
                }
            }
            if ((this.patient.CaseState == "2") || (this.patient.CaseState == "3"))
            {
                //��ձ�ı�־λ
                dtTable.AcceptChanges();
            }
            LockFpEnter();
        }
        /// <summary>
        /// ��ֵ
        /// </summary>
        /// <param name="row"></param>
        /// <param name="info"></param>
        private void SetRow(Neusoft.HISFC.Object.RADT.Location info, DataRow row)
        {
            row["���ұ���"] = info.Dept.ID;//0
            row["��������"] = info.Dept.Name;//1
            if (info.User01 == "")
            {
                row["ת������"] = System.DateTime.Now; //2
            }
            else
            {
                row["ת������"] = info.User01;
            }
            row["���"] = info.User03;
        }
        private void InitDatedtTable()
        {
            try
            {
                Type strType = typeof(System.String);
                Type intType = typeof(System.Int32);
                Type dtType = typeof(System.DateTime);
                Type boolType = typeof(System.Boolean);
                Type floatType = typeof(System.Single);

                dtTable.Columns.AddRange(new DataColumn[]{
														   new DataColumn("���ұ���", strType),	//0
														   new DataColumn("��������", strType),	 //1
														   new DataColumn("ת������", dtType),//2
														   new DataColumn("���", strType)//3
														 });//14
                //������Դ
                this.fpEnter1_Sheet1.DataSource = dtTable;
                LockFpEnter();
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
                Neusoft.HISFC.Management.Manager.Department dept = new Neusoft.HISFC.Management.Manager.Department();
                this.fpEnter1.SelectNone = true;
                //��ȡ����
                ArrayList al = dept.GetDeptmentAll();
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, 0, al);
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, 1, al);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ucChangeDept_Load(object sender, System.EventArgs e)
        {
            //������Ӧ�����¼�
            fpEnter1.KeyEnter += new Neusoft.NFC.Interface.Controls.NeuFpEnter.keyDown(fpEnter1_KeyEnter);
            fpEnter1.SetItem += new Neusoft.NFC.Interface.Controls.NeuFpEnter.setItem(fpEnter1_SetItem);
            fpEnter1.ShowListWhenOfFocus = true;
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
                    if (i == 0 || i == 1)
                    {
                        ProcessDept();
                    }
                    else if (i == 2)
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
            return 0;
        }
        private int fpEnter1_SetItem(Neusoft.NFC.Object.NeuObject obj)
        {
            this.ProcessDept();
            return 0;
        }
        /// <summary>
        /// ����س����� ������ȡ������
        /// </summary>
        /// <returns></returns>
        private int ProcessDept()
        {
            int CurrentRow = fpEnter1_Sheet1.ActiveRowIndex;
            if (CurrentRow < 0) return 0;

            if (fpEnter1_Sheet1.ActiveColumnIndex == 0)
            {
                Neusoft.NFC.Interface.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, 0);
                //��ȡѡ�е���Ϣ
                Neusoft.NFC.Object.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                //���ұ���
                fpEnter1_Sheet1.ActiveCell.Text = item.ID;
                //��������
                fpEnter1_Sheet1.Cells[CurrentRow, 1].Text = item.Name;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, 2);
                return 0;
            }

            else if (fpEnter1_Sheet1.ActiveColumnIndex == 1)
            {
                Neusoft.NFC.Interface.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, 1);
                //��ȡѡ�е���Ϣ
                Neusoft.NFC.Object.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                //�������� 
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                //���ұ��� 
                fpEnter1_Sheet1.Cells[CurrentRow, 0].Text = item.ID;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, 2);
                return 0;
            }
            return 0;
        }


        private void fpEnter1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            ////����fpSpread1 ������
            //if (System.IO.File.Exists(filePath))
            //{
            //    Neusoft.NFC.Interface.Classes.CustomerFp.SaveColumnProperty(this.fpEnter1_Sheet1, filePath);
            //}
        }

        private void menuItem1_Click(object sender, System.EventArgs e)
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
            //			LoadInfo(inpatientNo,operType); //���¼�������

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
            this.fpEnter1_Sheet1.Rows.Remove(fpEnter1_Sheet1.ActiveRowIndex, 1);
            return 1;
        }
        #endregion 
    }
}
