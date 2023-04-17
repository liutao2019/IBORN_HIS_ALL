using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace Neusoft.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// [��������: ��ٹ������]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2006-11-30]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucPatientLeave : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPatientLeave()
        {
            InitializeComponent();
        }

        private void ucPatientLeave_Load(object sender, EventArgs e)
        {

        }

        #region ����

        Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = null;
        Neusoft.HISFC.BizProcess.Integrate.Manager manager = new Neusoft.HISFC.BizProcess.Integrate.Manager();
        Neusoft.HISFC.BizProcess.Integrate.Order orderManager = new Neusoft.HISFC.BizProcess.Integrate.Order();
        Neusoft.HISFC.BizLogic.RADT.InPatient inpatient = new Neusoft.HISFC.BizLogic.RADT.InPatient();
        Neusoft.HISFC.Models.RADT.Leave myLeave = null;
        Neusoft.FrameWork.Models.NeuObject dept = null;
        protected bool IsLeave = false;
        #endregion
        /// <summary>

        /// ��ʼ���ؼ�
		/// </summary>
		private  void InitControl() {
			dept  = this.patientInfo.PVisit.PatientLocation.Dept.Clone();
			try {
				//��ʼ��ҽ���б�
				this.cmbDoc.AddItems(manager.QueryEmployee(Neusoft.HISFC.Models.Base.EnumEmployeeType.D,dept.ID ));
				this.cmbDoc.IsListOnly = true;
				
				//�����ʾ�ؼ��е�����
				ClearInfo();
			}
			catch{}
			
		}
		

		/// <summary>
		/// ��ʼ�������б�
		/// </summary>
		private void InitTree() {
			//��ձ���
			this.IsLeave = false;
			this.treeView1.Nodes.Clear();
			this.treeView1.BeginUpdate();
			System.Windows.Forms.TreeNode Node=new System.Windows.Forms.TreeNode();
			Node.Text ="��ٹ���";
			Node.Tag = "";
			this.treeView1.Nodes.Add(Node);

			//ȡ���������Ϣ
			ArrayList al=new ArrayList();
			al=this.inpatient.GetPatientLeaveAvailable(this.patientInfo.ID);
			if (al == null) {
                MessageBox.Show(this.inpatient.Err);
				return;
			}

			//��ӻ��������Ϣ
			foreach(Neusoft.HISFC.Models.RADT.Leave leave in al) {	
				this.AddTreeNode(leave);
			}
			this.treeView1.ExpandAll();
			this.treeView1.SelectedNode = this.treeView1.Nodes[0];
			this.treeView1.EndUpdate();

		}


		/// <summary>
		/// �������ͽڵ�
		/// </summary>
		/// <param name="leave"></param>
		private void AddTreeNode(Neusoft.HISFC.Models.RADT.Leave leave) {
			System.Windows.Forms.TreeNode Node=new System.Windows.Forms.TreeNode();
			try {
				Node.Text = leave.LeaveTime.ToString();
				Node.Tag  = leave;
				//�������״̬�Ĳ�ͬ,��ʾ��ͬ��ͼƬ
				if(leave.LeaveFlag == "0") {
					//������ټ�¼
					Node.ImageIndex = 1;
					//��ʶ���ߴ�����Ч����ټ�¼
					this.IsLeave = true;
				}
				else {
					//�������ټ�¼
					Node.ImageIndex = 2;
				}

				Node.SelectedImageIndex = Node.ImageIndex;
				
				this.treeView1.Nodes[0].Nodes.Add(Node);
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message,"������ʾ");
			}
		}


		/// <summary>
		/// ���û��������Ϣ���ؼ�
		/// </summary>
		/// <param name="leave"></param>
		private void ShowLeaveInfo(Neusoft.HISFC.Models.RADT.Leave leave) {
			//���û�л�����Ϣ,������
			if(this.myLeave == null) {
				this.ClearInfo();
				return;
			}

			//����ҽ��
			this.cmbDoc.Tag  =leave.DoctCode;
			if(leave.DoctCode == "") 
				this.cmbDoc.Text = null;
			//�������
			this.txtDays.Text= leave.LeaveDays.ToString();
			//�Ǽ�������
			this.txtRegisterPerson.Text = manager.GetEmployeeInfo(leave.Oper.ID).Name;
			//����������
			try {
                if(leave.CancelOper.ID !="")
				    this.txtCancelPerson.Text= manager.GetEmployeeInfo(leave.CancelOper.ID).Name;
			}
			catch {
				this.txtCancelPerson.Text = "";
			}
			//��ע
			this.txtRemark.Text = leave.Memo;
			//�������
			this.dtpRegisterDate.Text = leave.LeaveTime.ToString();
            try
            {
			    //ȡ������
			    if(this.myLeave.LeaveFlag != "0") 
                    this.dtpCancelDate.Text = leave.CancelOper.OperTime.ToString();
			    else
                    this.dtpCancelDate.Text = this.inpatient.GetDateTimeFromSysDateTime().ToString();
            }catch{}
			//��ʾ��ټ�¼
			if(leave.LeaveFlag == "0") {
				
				this.btnCancel.Enabled = true;
				this.btnSave.Enabled   = true;
			}
			else {
				//ȡ������ټ�¼��ʾ������Ϣ
			
				this.btnCancel.Enabled = false;
				this.btnSave.Enabled   = false;
			}
		
		}


		/// <summary>
		/// ��ȡ���������Ϣ
		/// </summary>
		private bool GetLeaveInfo() {
			bool isNew = true;
			if(this.myLeave == null)
			{
				this.myLeave = new Neusoft.HISFC.Models.RADT.Leave();
				isNew = true;
			}
			else
			{
				isNew = false;
			}

			//��������������Ϣ,��ȡ���������Ϣ
			if(this.myLeave.ID == "") {
				this.myLeave.ID = this.patientInfo.ID;
				this.myLeave.LeaveTime =this.dtpRegisterDate.Value;
			}
            
			//�������
			this.myLeave.LeaveDays = Neusoft.FrameWork.Function.NConvert.ToInt32(this.txtDays.Text);
			//����ҽ��
			this.myLeave.DoctCode = this.cmbDoc.Tag.ToString();
			//��ע
			this.myLeave.Memo = this.txtRemark.Text;
			return isNew;
		}


		/// <summary>
		/// ����
		/// </summary>
		private void ClearInfo() {
			//����б�ѡ�и��ڵ�
			this.treeView1.SelectedNode = this.treeView1.Nodes[0];
			this.txtName.Tag  = this.patientInfo.ID;
            this.txtName.Text = this.patientInfo.Name;
            this.txtBed.Text = this.patientInfo.PVisit.PatientLocation.Bed.ID;
			this.dtpRegisterDate.Value = this.inpatient.GetDateTimeFromSysDateTime();
			this.dtpCancelDate.Value =this.dtpRegisterDate.Value;
			this.cmbDoc.Tag  ="";
			this.cmbDoc.Text = null;
			this.txtDays.Text="";
			this.txtCancelPerson.Text = "";
			this.txtRegisterPerson.Text="";
			this.txtRemark.Text = "";
			//��ť����
			this.btnCancel.Enabled = true;
			this.btnSave.Enabled   = true;
		}


		/// <summary>
		/// ˢ��
		/// </summary>
		/// <param name="inPatientNo"></param>
		public void RefreshList(string inPatientNo) {
			//��ʼ���ؼ�
			InitControl();

			//��ʼ����
			InitTree();
		}


		/// <summary>
		/// ����
		/// </summary>
		/// <returns></returns>
		public int Save() {
			//������ߴ�����Ч����ټ�¼,�������������Ϣ
			if(this.IsLeave && this.myLeave == null) {
				MessageBox.Show("�˻����Ѿ�����һ��Ч�������Ϣ,����ͬʱ�Ǽ�������Ч�����. \n���ȶ��Ѵ��ڵ������Ϣ�������ٴ���.","��ʾ");
				this.txtDays.Focus();
				return -1;
			}

			//Ӥ�����������
			if(this.patientInfo.IsBaby ) {
				MessageBox.Show("Ӥ�����������.","��ʾ");
				return -1;
			}

			//ȡ�������������µ���Ϣ,�����жϲ���
            this.patientInfo = this.inpatient.QueryPatientInfoByInpatientNO(this.patientInfo.ID);
			if(this.patientInfo == null) {
				MessageBox.Show(this.inpatient.Err);
				return -1;
			}

            //�ж��Ƿ�Ƿ��

            if (this.patientInfo.FT.LeftCost < this.patientInfo.PVisit.MoneyAlert)
            {
                if(MessageBox.Show("�û����Ѿ�Ƿ�ѣ��Ƿ��������", "ע��", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return -1;
                }
            }

			//����¼������
			if (this.txtDays.Text == "") {
				MessageBox.Show("������д�������","��ʾ");
				this.txtDays.Focus();
				return -1;
			}

			//��������Ϊ0
			if (Neusoft.FrameWork.Function.NConvert.ToInt32(this.txtDays.Text) == 0) {
				MessageBox.Show("��¼����Ч���������","��ʾ");
				this.txtDays.Focus();
				return -1;
			}

			//ȡ���������Ϣ
			bool b =this.GetLeaveInfo();

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            //Neusoft.FrameWork.Management.Transaction SQLCA=new Neusoft.FrameWork.Management.Transaction(this.inpatient.Connection);
            //SQLCA.BeginTransaction();

            this.inpatient.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            orderManager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

			//���������Ϣ
            int parm = this.inpatient.SetPatientLeave(this.myLeave, this.patientInfo.PVisit.PatientLocation.Bed);
			if (parm == -1) {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
				MessageBox.Show(this.inpatient.Err);
				return -1;
			}
			else if(parm == 0){
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
				MessageBox.Show("������Ϣ�ѷ����䶯,��ˢ�µ�ǰ����","��ʾ");
				return 0;
			}

			if(b)//����ٵ�
			{
				//���ʱ,ֹͣҽ��ִ��
				//��٣�������ٺ�1000��
                if (orderManager.UpdateDecoTime(this.patientInfo.ID, 1000) == -1) 
				{
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
					MessageBox.Show(this.orderManager.Err);
					return 0;
				}
			}
            Neusoft.FrameWork.Management.PublicTrans.Commit();
			this.RefreshList(this.patientInfo.ID);
			MessageBox.Show("�Ǽǳɹ�!","��ʾ");

			return 1;
		}


		/// <summary>
		/// ���ٴ���
		/// </summary>
		public void Cancel() 
        {
			//ȡ�������������µ���Ϣ,�����жϲ���
            this.patientInfo = this.inpatient.QueryPatientInfoByInpatientNO(this.patientInfo.ID);
			if(this.patientInfo == null) {
				MessageBox.Show(this.inpatient.Err);
				return;
			}		

			if(this.myLeave == null) {
				MessageBox.Show("��ѡ��Ҫ���ٵļ�¼","��ʾ");
				return;
			}

			//ȡ���������Ϣ
			this.GetLeaveInfo();
			this.myLeave.LeaveFlag = "1";

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            //Neusoft.FrameWork.Management.Transaction SQLCA=new Neusoft.FrameWork.Management.Transaction(this.inpatient.Connection);
            //SQLCA.BeginTransaction();

            this.inpatient.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            this.orderManager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

			int parm = this.inpatient.DiscardPatientLeave(this.myLeave, this.patientInfo.PVisit.PatientLocation.Bed);
			if (parm == -1) {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
				MessageBox.Show(this.inpatient.Err);
			}
			else if(parm == 0){
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
				MessageBox.Show("������Ϣ�ѷ����䶯,��ˢ�µ�ǰ����","��ʾ");
				return;
			}

			//����ʱ,--%%%%��ҽ���´�ִ��ʱ���Ϊ�˿�ϵͳʱ��
			//�ȸ��»�ȥ���ٸ��µ���ǰʱ�䣨�����ǰʱ��>�´ηֽ�ʱ��Ÿ���)
            DateTime dtToday = this.inpatient.GetDateTimeFromSysDateTime();
		
			if(this.orderManager.UpdateDecoTime(this.patientInfo.ID,-1000) == -1) 
			{
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
				MessageBox.Show(this.orderManager.Err);
				return;
			}
			//���³ɵ�ǰ���ڣ�
			if(this.orderManager.UpdateDecoTime(this.patientInfo.ID,dtToday) == -1) 
			{
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
				MessageBox.Show(this.orderManager.Err);
				return;
			}

            Neusoft.FrameWork.Management.PublicTrans.Commit();
			this.RefreshList(this.patientInfo.ID);
			MessageBox.Show("���ٳɹ�!","��ʾ");
		}


		private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			//��ȡ��ǰѡ�������Ϣ
			this.myLeave = e.Node.Tag as Neusoft.HISFC.Models.RADT.Leave ;

			//��ѡ�е������Ϣ��ʾ�ڿؼ���
			this.ShowLeaveInfo(this.myLeave);
		}


		private void btAdd_Click(object sender, System.EventArgs e) {
			this.ClearInfo();
		}

		private void btCancel_Click(object sender, System.EventArgs e) {
			//���ٴ���
			this.Cancel();		
		}

		private void btSave_Click(object sender, System.EventArgs e) {	
			if(this.Save() == 1) {
                    //Report.ucPrintTempOutHosDrug u = new RADT.Report.ucPrintTempOutHosDrug();
                    //u.InpatientNo = this.PatientInfo.ID;
                    //u.PrintPreview();
				
			}
		}

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.patientInfo = neuObject as Neusoft.HISFC.Models.RADT.PatientInfo;
            if (this.patientInfo.ID != null || this.patientInfo.ID != "")
            {
                try
                {
                    RefreshList(patientInfo.ID);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
               
            }
            return 0;
        }
	
    }
}
