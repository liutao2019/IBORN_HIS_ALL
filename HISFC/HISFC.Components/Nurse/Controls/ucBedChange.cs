using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Base;
namespace FS.HISFC.Components.Nurse.Controls
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

        FS.HISFC.BizLogic.RADT.InPatient radtInpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();

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
                    if (this.bedtype.ToString() == "Wap" || this.bedtype.ToString() == "Unlock")
                    {
                        list = manager.QueryUnoccupiedBed(nurseID);
                    }
                    else if (this.bedtype.ToString() == "Hang")
                    {
                        list = manager.QueryBedList(nurseID);
                    }

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
            ArrayList al = this.manager.QueryUnoccupiedBed(this.nurseCell.ID);
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

            }
            catch { }

        }

        private void fpSpread1_Click(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.myBed = this.fpSpread1_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.Base.Bed;
            if (this.myBed == null)
                this.lblShow.Text = "";
            else
                this.lblShow.Text = cmbInpatientNo.Text + " ѡ�� " + this.myBed.ID + " ��";
        }

        private void fpSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            this.myBed = this.fpSpread1_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Base.Bed;
            if (this.myBed == null)
                this.lblShow.Text = "";
            else
                this.lblShow.Text = cmbInpatientNo.Text + " ѡ�� " + this.myBed.ID + " ��";

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
                        if (this.radtInpatientManager.ChangeBedInfo( this.mypatientinfo.ID, myBed.ID, kind ) == 0)
                        {
                            this.Err = "����ɹ���";
                          
                        }
                        else
                        {
                            this.Err = "����ʧ�ܣ�" + this.radtInpatientManager.Err;
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
            //insert  or update
            try
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(this.radtManager.Connection);
                //SQLCA.BeginTransaction();

                this.radtInpatientManager.SetTrans( FS.FrameWork.Management.PublicTrans.Trans );

                if (this.radtInpatientManager.SwapPatientBed( this.mypatientinfo, myBed.ID, kind ) == 0)
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                    this.Err = "����ɹ���";
                   

                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "����ʧ�ܣ�" + this.radtInpatientManager.Err;
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

        private void button1_Click(object sender, System.EventArgs e)
        {
            ////��Ҵ�
            //if (this.bedtype.ToString() == "Unlock")
            //{
            //    int Err = 0;

            //    try
            //    {
            //        FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(this.Patient.Connection);
            //        SQLCA.BeginTransaction();
            //        this.Patient.SetTrans(SQLCA.Trans);
            //        Err = this.Patient.PatientUnWapBed(mypatientinfo, mypatientinfo.PVisit.PatientLocation.Bed.ID, "1");
            //        if (Err == 0)
            //        {
            //            FS.FrameWork.Management.PublicTrans.RollBack();
            //            this.Err = "�û����޹Ҵ���Ϣ��";
            //            MessageBox.Show(this.Err, "��ʾ��");
            //        }
            //        else if (Err < 0)
            //        {
            //            FS.FrameWork.Management.PublicTrans.RollBack();
            //            this.Err = "��Ҵ�ʧ�ܣ�";
            //            MessageBox.Show(this.Err, "��ʾ��");
            //        }
            //        myBed.InpatientNo = mypatientinfo.ID;
            //        myBed.BedStatus.ID = FS.HISFC.Models.RADT.BedStatus.enuBedStatus.O;

            //        if (this.Patient.UpdateBedStatus(myBed) <= 0)
            //        {
            //            FS.FrameWork.Management.PublicTrans.RollBack();
            //            this.Err = "��Ҵ�ʧ�ܣ�";
            //            MessageBox.Show(this.Err, "��ʾ��");
            //        }
            //        mypatientinfo.PVisit.PatientLocation.Bed = myBed;

            //        if (this.Patient.ArrivePatient(mypatientinfo, "I") <= 0)
            //        {
            //            FS.FrameWork.Management.PublicTrans.RollBack();
            //            this.Err = "��Ҵ�ʧ�ܣ�";
            //            MessageBox.Show(this.Err, "��ʾ��");
            //        }
            //        SQLCA.Commit();
            //        this.Err = "��Ҵ��ɹ���";

            //        this.FindForm().DialogResult = DialogResult.OK;

            //    }
            //    catch { }
            //}
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
	    Unlock
    }
}
