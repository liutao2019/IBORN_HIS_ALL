using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Base;
namespace FS.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// [��������: �������⿪]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2006-11-30]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    public partial class ucBedChange : UserControl
    {
        public ucBedChange()
        {
            InitializeComponent();
        }

        #region ����
        protected BedType bedtype;
        private DataSet constantData = new DataSet();
        private string Err;
        private FS.HISFC.Models.RADT.PatientInfo mypatientinfo = null;
        private Bed myBed = new Bed();
        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();

        FS.HISFC.BizProcess.Integrate.RADT radtIntergrate = new FS.HISFC.BizProcess.Integrate.RADT();

        FS.FrameWork.Public.ObjectHelper helper = null;
        #endregion

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            set
            {
                this.mypatientinfo = value;
                this.cmbInpatientNo.Text = value.PID.PatientNO;
                this.lblShow.Text = value.Name;
            }
        }
        /// <summary>
        /// ��λ�������
        /// </summary>
        public BedType SetType
        {
            set
            {
                this.bedtype = value;
                this.cmbInpatientNo.Enabled = true;
                this.btnOK.Visible = true;
                this.btnCancel.Visible = false;
                if (bedtype.ToString() == "Wap")
                {
                    this.GroupBox1.Text = "��������";
                }
                else if (bedtype.ToString() == "Hang")
                {
                    this.GroupBox1.Text = "�Ҵ�����";
                }
                else if (bedtype.ToString() == "Unlock")
                {
                    this.GroupBox1.Text = "��Ҵ�����";
                    this.cmbInpatientNo.Enabled = false;
                    this.btnOK.Visible = false;
                    this.btnCancel.Visible = true;
                }
            }
        }
        #region ����
        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        private void InitControl()
        {
            constantData = InitDataSet();
            this.fpSpread1_Sheet1.DataSource = constantData.Tables[0];
            this.fpSpread1_Sheet1.Columns[0].Width = 40;
            this.fpSpread1_Sheet1.Columns[1].Width = 60;
            this.fpSpread1_Sheet1.Columns[2].Width = 80;
            this.fpSpread1_Sheet1.Columns[3].Width = 80;
            this.fpSpread1_Sheet1.Columns[4].Width = 40;
            this.fpSpread1_Sheet1.Columns[5].Width = 100;
            this.fpSpread1_Sheet1.Columns[6].Width = 80;
            this.fpSpread1_Sheet1.Columns[7].Width = 80;
            this.fpSpread1_Sheet1.Columns[8].Width = 40;
            this.fpSpread1_Sheet1.Columns[9].Width = 40;
            this.fpSpread1_Sheet1.Columns[10].Width = 75;

            helper = new FS.FrameWork.Public.ObjectHelper(manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.BEDGRADE));
        }


        /// <summary>
        /// ��ʼ�����
        /// </summary>
        private DataSet InitDataSet()
        {

            DataSet dataSet = new DataSet();
            DataTable table = new DataTable("Bedinfo");

            DataColumn column1 = new DataColumn("BED_NO");
            column1.DataType = typeof(System.String);
            table.Columns.Add(column1);


            DataColumn column2 = new DataColumn("WARD_NO");
            column2.DataType = typeof(System.String);
            table.Columns.Add(column2);

            DataColumn column3 = new DataColumn("FEE_GRADE_CODE");
            column3.DataType = typeof(System.String);
            table.Columns.Add(column3);

            DataColumn column4 = new DataColumn("BED_WEAVE");
            column4.DataType = typeof(System.String);
            table.Columns.Add(column4);
            //
            DataColumn column5 = new DataColumn("BED_STATE");
            column5.DataType = typeof(System.String);
            table.Columns.Add(column5);

            DataColumn column6 = new DataColumn("CLINIC_NO");
            column6.DataType = typeof(System.String);
            table.Columns.Add(column6);
            //
            DataColumn column7 = new DataColumn("BED_PHONECODE");
            column7.DataType = typeof(System.String);
            table.Columns.Add(column7);

            DataColumn column8 = new DataColumn("OWNER_PC");
            column8.DataType = typeof(System.String);
            table.Columns.Add(column8);

            DataColumn column9 = new DataColumn("VALID_STATE");
            column9.DataType = typeof(System.String);
            table.Columns.Add(column9);
            //
            DataColumn column10 = new DataColumn("PREPAY_FLAG");
            column10.DataType = typeof(System.String);
            table.Columns.Add(column10);

            DataColumn column11 = new DataColumn("PREPAY_OUTDATE");
            column11.DataType = typeof(System.String);
            table.Columns.Add(column11);



            dataSet.Tables.Add(table);

            return dataSet;
        }


        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="NurseID"></param>
        private void LoadData(string nurseID)
        {

            if (constantData == null)
                constantData = InitDataSet();
            DataTable table = constantData.Tables[0];
            if (table != null)
            {
                if (table.Rows.Count > 0)
                    table.Rows.Clear();
                ArrayList list = new ArrayList();
                try
                {
                    #region {8ADDE7FC-0CF5-4e86-9B0A-41DFD058A400}
                    if (this.bedtype.ToString() == "Wap" || this.bedtype.ToString() == "Unlock")
                    {
                        list = manager.QueryUnoccupiedBed(nurseID);
                    }
                    else if (this.bedtype.ToString() == "Hang" || this.bedtype.ToString() == "Change")
                    {
                        list = manager.QueryBedList(nurseID);
                    }
                    #endregion
                    if (list.Count > 0)
                    {

                        AddConstsToTable(list, table);
                        constantData.AcceptChanges();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
        }


        /// <summary>
        /// ������ʾ������Ϣ
        /// </summary>
        /// <param name="myNurse"></param>
        protected void RefreshList(string myNurse)
        {
            this.LoadData(myNurse);

        }


        private void AddConstsToTable(ArrayList list, DataTable table)
        {
            table.Clear();
            int i = 0;

            foreach (FS.HISFC.Models.Base.Bed myBed in list)
            {
                //string strTrue = "��", strFalse = "��";
                string strOutDate = "";

                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                try
                {
                    myBed.BedGrade.Name = helper.GetName(myBed.BedGrade.ID);
                }
                catch { }
                if (myBed.PrepayOutdate == DateTime.MinValue)
                {
                    strOutDate = "";
                }
                else
                {
                    strOutDate = myBed.PrepayOutdate.ToString();
                }
               
                table.Rows.Add(new Object[] { myBed.ID,
                    myBed.SickRoom,
                    myBed.BedGrade.Name, 
                    myBed.BedRankEnumService.Name,
                    myBed.Status.Name, 
                    myBed.InpatientNO, 
                    myBed.Phone, 
                    myBed.OwnerPc,
                    myBed.IsValid,
                    myBed.IsPrepay,
                    strOutDate });
                this.fpSpread1_Sheet1.Rows[i].Tag = myBed;
                i++;
            }
            for (int k = i; k <= 15; k++)
            {
                table.Rows.Add(new object[] { });
            }
        }


        private void ListPatient()
        {
          
            //this.cmbInpatientNo.AddItems(radtManager.QueryPatient.PatientQuery(myLocation, Status));
        }


        private Bed GetInfoFromRow(int row)
        {
            Bed myBed = new Bed();
            myBed = (Bed)this.fpSpread1_Sheet1.Rows[row].Tag;
            return myBed;
        }


        private void ShowInfo(Bed obj)
        {
            this.lblShow.Text = cmbInpatientNo.Text + " ѡ�� " + obj.ID + " ��";
        }


        protected FS.FrameWork.Models.NeuObject objNurse = null;
        private FS.FrameWork.Models.NeuObject nurseCell
        {
            get
            {

                if (objNurse == null)
                    objNurse = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.Clone();
                return objNurse;
            }
            set
            {
                objNurse = value;
            }
        }

        /// <summary>
        /// ��ʾ��λ�б�
        /// </summary>
        public void ShowBedList()
        {
            this.lblShow.Text = "";
            //ȡ��λ��Ϣ
            #region {8ADDE7FC-0CF5-4e86-9B0A-41DFD058A400}
            ArrayList al = new ArrayList();
            if (this.bedtype.ToString() == "Wap" || this.bedtype.ToString() == "Unlock")
            {
                al = this.manager.QueryUnoccupiedBed(this.nurseCell.ID);
            }
            else if (this.bedtype.ToString() == "Hang" || this.bedtype.ToString() == "Change")
            {
                al = this.manager.QueryBedList(this.nurseCell.ID);
            }
            #endregion
            if (al == null)
            {
                MessageBox.Show(this.manager.Err, "��ʾ");
                return;
            }

            //��ʾ����������
            this.fpSpread1_Sheet1.Rows.Count = 0;
            this.fpSpread1_Sheet1.Rows.Count = al.Count;

            //������ʾ����
            FS.HISFC.Models.Base.Bed bed = null;
            for (int i = 0; i < al.Count; i++)
            {
                bed = al[i] as FS.HISFC.Models.Base.Bed;
                if (bed == null) return;
                //��ֵ
                this.fpSpread1_Sheet1.Cells[i, 0].Value = bed.ID.Substring(4);	//����
                this.fpSpread1_Sheet1.Cells[i, 1].Value = bed.SickRoom;			//�����
                this.fpSpread1_Sheet1.Cells[i, 2].Value = bed.BedGrade.Name;		//��λ�ȼ�
                this.fpSpread1_Sheet1.Cells[i, 3].Value = bed.BedRankEnumService.Name;		//��λ����
                this.fpSpread1_Sheet1.Cells[i, 4].Value = bed.Status.Name;	//��λ״̬
                this.fpSpread1_Sheet1.Cells[i, 5].Value = bed.InpatientNO == "N" ? "" : bed.InpatientNO.Substring(4);		//סԺ��
                this.fpSpread1_Sheet1.Cells[i, 6].Value = bed.Phone;				//��λ�绰
                this.fpSpread1_Sheet1.Cells[i, 7].Value = bed.OwnerPc;			//����
                this.fpSpread1_Sheet1.Cells[i, 8].Value = bed.IsValid;			//�Ƿ���Ч
                this.fpSpread1_Sheet1.Cells[i, 9].Value = bed.IsPrepay;			//�Ƿ�ԤԼ
                this.fpSpread1_Sheet1.Rows[i].Tag = bed;
            }

            //ѡ�е�һ��
            if (this.fpSpread1_Sheet1.RowCount > 0)
                this.fpSpread1_Sheet1.ActiveRowIndex = 0;
            this.fpSpread1.Focus();
        }

        #endregion

        #region �¼�
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {

                //�г�����
                this.ListPatient();
                //��ʾ��λ
                this.ShowBedList();
                #region {8ADDE7FC-0CF5-4e86-9B0A-41DFD058A400}
                if (this.bedtype.ToString() == "Wap")
                {
                    this.GroupBox1.Text = "����";
                }
                else if (this.bedtype.ToString() == "Hang" || this.bedtype.ToString() == "Change")
                {
                    this.GroupBox1.Text = "����";
                }
                else
                {
                    this.GroupBox1.Text = "";
                }
                #endregion
            }
            catch { }

        }

        private void fpSpread1_Click(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.myBed = this.fpSpread1_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.Base.Bed;
            #region {8ADDE7FC-0CF5-4e86-9B0A-41DFD058A400}
            if (!string.IsNullOrEmpty(this.myBed.InpatientNO) && this.myBed.Status.ID.ToString() != "W")
            {
                this.selectPatient = this.radtManager.QueryPatientInfoByInpatientNO(this.myBed.InpatientNO);
            }
            #endregion
            if (this.myBed == null)
                this.lblShow.Text = "";
            else
                this.lblShow.Text = this.mypatientinfo.Name + " ѡ�� " + this.myBed.ID.Substring(4) + " ��";//{8ADDE7FC-0CF5-4e86-9B0A-41DFD058A400}
        }

        private void fpSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            this.myBed = this.fpSpread1_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Base.Bed;
            if (this.myBed == null)
                this.lblShow.Text = "";
            else
                this.lblShow.Text = this.mypatientinfo.Name + " ѡ�� " + this.myBed.ID.Substring(4) + " ��";//{8ADDE7FC-0CF5-4e86-9B0A-41DFD058A400}

        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            string kind = "";//1�Ҵ� 2  ���� 
            if (myBed.ID == "")
            {
                this.Err = "��ѡ��λ��";
                return;
            }
            if (this.bedtype.ToString() == "Hang")
            {
                kind = "1";
                //ѡ��Ĵ�λ�Ѿ���ռ��
                if (myBed.InpatientNO != "")
                {
                    if (this.mypatientinfo.ID == myBed.InpatientNO)
                    {
                        this.Err = "�����Ѿ�ѡ��ô�λ��";
                        return;
                    }
                    try
                    {
                        if (this.radtManager.ChangeBedInfo(this.mypatientinfo.ID, myBed.ID, kind) == 0)
                        {
                            this.Err = "����ɹ���";
                          
                        }
                        else
                        {
                            this.Err = "����ʧ�ܣ�" + this.radtManager.Err;
                        }
                    }
                    catch { }
                   
                    return;
                }
            }
            else if (this.bedtype.ToString() == "Wap")
            {
                kind = "2";
            }
            #region {8ADDE7FC-0CF5-4e86-9B0A-41DFD058A400}
            else if (this.bedtype.ToString() == "Change")
            {


                if (this.mypatientinfo.ID == myBed.InpatientNO)
                {
                    this.Err = "�����Ѿ�ѡ��ô�λ��";
                    return;
                }
                try
                {
                    if (this.ChangeItems() > 0)
                    {
                        this.Err = "����ɹ���";

                        this.FindForm().DialogResult = DialogResult.OK;
                        this.FindForm().Close();
                    }
                    else
                    {
                        this.Err = "����ʧ�ܣ�" + this.Err;
                    }
                }
                catch { }

                return;

            }
            #endregion
            //insert  or update
            try
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(this.radtManager.Connection);
                //SQLCA.BeginTransaction();

                this.radtManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                //if (this.radtManager.SwapPatientBed(this.mypatientinfo, myBed.ID, kind) == 0)
                if (radtIntergrate.WapBed(mypatientinfo, myBed, kind) != -1)
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                    this.Err = "����ɹ���";
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "����ʧ�ܣ�" + this.radtManager.Err;
                }
            }
            catch { }
            this.FindForm().DialogResult = DialogResult.OK;
            this.FindForm().Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.FindForm().Close();
        }

        
        #endregion

        #region {8ADDE7FC-0CF5-4e86-9B0A-41DFD058A400}

        private FS.HISFC.Models.RADT.PatientInfo selectPatient = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        private int ChangeItems()
        {
            int parm;
            FS.HISFC.Models.RADT.PatientInfo obj_a, obj_b;

            //�����ͬһ����,�򷵻�
            if (this.selectPatient != null)
            {
                if (!string.IsNullOrEmpty(this.selectPatient.ID) && this.mypatientinfo.ID == this.selectPatient.ID)
                {
                    return -1;
                }

                //���˶Ե���λ
                if (!string.IsNullOrEmpty(this.selectPatient.ID))
                {
                    obj_a = this.mypatientinfo;
                    obj_b = this.selectPatient;

                    if (obj_a.PVisit.PatientLocation.Bed.Status.ID.ToString() == "W" || obj_b.PVisit.PatientLocation.Bed.Status.ID.ToString() == "W")
                    {
                        MessageBox.Show("�������Ĵ�λ��һ״̬Ϊ���������ܵ�����", "��ʾ��");
                        return -1;
                    }
                    //
                    if (MessageBox.Show("�Ƿ�Ԥ����" + obj_a.Name + "���롰" + obj_b.Name + "������", "��ʾ��", System.Windows.Forms.MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Cancel) return -1;
                    //
                    try
                    {
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();

                        this.radtManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        //�����ߴ�λ�Ե�����
                        parm = this.radtManager.SwapPatientBed(obj_a, obj_b);
                        if (parm == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            this.Err = "����ʧ�ܣ�\n" + this.radtManager.Err;
                            return -1;
                        }
                        else if (parm == 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            this.Err = "����ʧ��! \n������Ϣ�б䶯,��ˢ�µ�ǰ����";
                            return -1;
                        }

                        FS.FrameWork.Management.PublicTrans.Commit();

                    }
                    catch (Exception ex)
                    {
                        this.Err = ex.Message;
                        return -1;
                    }

                    return 1;

                }
            }
            else
            {
                //����a����b��
                return (this.TransPatientToBed());

            }
            
            return 1;
        }


        /// <summary>
        /// ���˻�������
        /// </summary>
        /// <returns></returns>
        private int TransPatientToBed()
        {
            int parm = 0;
            FS.HISFC.Models.RADT.Location obj_location = new FS.HISFC.Models.RADT.Location();
            FS.HISFC.Models.RADT.PatientInfo obj_a;

            //ȡ��λ�ͻ�����Ϣ
            obj_a = this.mypatientinfo;
            obj_location.Bed = this.myBed;

            //���ų�ȥǰ��λ
            string bedNo = obj_location.Bed.ID.Length > 4 ? obj_location.Bed.ID.Substring(4) : obj_location.Bed.ID;

            if (obj_location.Bed.Status.ID.ToString() == "W")
            {
                MessageBox.Show("����Ϊ [" + bedNo + " ]�Ĵ�λ״̬Ϊ����������ռ�ã�", "��ʾ��");
                return -1;
            }
            else if (obj_location.Bed.Status.ID.ToString() == "C")
            {
                MessageBox.Show("����Ϊ [" + bedNo + " ]�Ĵ�λ״̬Ϊ�رգ�����ռ�ã�", "��ʾ��");
                return -1;
            }
            else if (obj_location.Bed.IsPrepay)
            {
                MessageBox.Show("����Ϊ [" + bedNo + " ]�Ĵ�λ�Ѿ�ԤԼ������ռ�ã�", "��ʾ��");
                return -1;
            }
            else if (!obj_location.Bed.IsValid)
            {
                MessageBox.Show("����Ϊ [" + bedNo + " ]�Ĵ�λ�Ѿ�ͣ�ã�����ռ�ã�", "��ʾ��");
                return -1;
            }
            else if (obj_location.Bed.Status.ID.ToString() == "I")
            {
                MessageBox.Show("����Ϊ [" + bedNo + " ]�Ĵ�λ״̬Ϊ���룬����ռ�ã�", "��ʾ��");
                return -1;
            }
            else if (obj_location.Bed.Status.ID.ToString() == "K")
            {
                MessageBox.Show("����Ϊ [" + bedNo + " ]�Ĵ�λ״̬Ϊ��Ⱦ������ռ�ã�", "��ʾ��");
                return -1;
            }
            
            if (MessageBox.Show("�Ƿ�Ԥ����" + obj_a.Name + "��ת�Ƶ�[" + bedNo + "]�Ŵ�", "��ʾ��", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No) return -1;
            
            try
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                this.radtManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //���˻�������
                parm = this.radtManager.TransferPatient(obj_a, obj_location);
                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "����ʧ�ܣ�\n" + this.radtManager.Err;
                    return -1;
                }
                else if (parm == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "����ʧ��! \n������Ϣ�б䶯�����Ѿ���Ժ,��ˢ�µ�ǰ����";
                    return -1;
                }
                //Ӥ������һ�𻻴�
                if (!obj_a.PID.PatientNO.Contains("B"))// {CDB01BF4-B40F-4cdc-9F0D-23F074290136}
                {
                    ArrayList alBabys = new ArrayList();
                    
                    alBabys = this.radtManager.QueryBabiesByMother(obj_a.ID);
                    {
                        foreach (FS.HISFC.Models.RADT.PatientInfo bInfo in alBabys)
                        {
                            parm = this.radtManager.TransferPatient(bInfo, obj_location);
                            if (parm == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                this.Err = "����ʧ�ܣ�\n" + this.radtManager.Err;
                                return -1;
                            }
                            else if (parm == 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                this.Err = "����ʧ��! \n������Ϣ�б䶯�����Ѿ���Ժ,��ˢ�µ�ǰ����";
                                return -1;
                            }
                        }
                    }
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return 1;
        }
        #endregion

    }
    	
    /// <summary>
    /// ��������
    /// </summary>
    public enum BedType {
	    /// <summary>
	    /// ����
	    /// </summary>
	    Wap,
	    /// <summary>
	    /// �Ҵ�
	    /// </summary>
	    Hang,
	    /// <summary>
	    /// ��Ҵ�
	    /// </summary>
	    Unlock,
        /// <summary>
        /// ����
        /// {8ADDE7FC-0CF5-4e86-9B0A-41DFD058A400}
        /// </summary>
        Change
    }
}
