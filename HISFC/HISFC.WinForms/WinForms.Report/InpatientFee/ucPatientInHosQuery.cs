using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using System.Collections;

namespace FS.WinForms.Report.InpatientFee
{      
    /// <summary>
    /// [��������: ��Ժ���������ѯ]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-9-13]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucPatientInHosQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region ����
        /// <summary>
        /// ����ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        /// <summary>
        /// סԺ����תҵ���
        /// </summary>
        FS.HISFC.BizLogic.RADT.InPatient patientQuery = new FS.HISFC.BizLogic.RADT.InPatient();
        /// <summary>
        /// ��Ա��Ϣҵ���
        /// </summary>
        FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Fee.InPatient feeManager = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// ҽ������ʵ��
        /// </summary>
        FS.HISFC.BizLogic.Order.Order orderManagement = new FS.HISFC.BizLogic.Order.Order();

        FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��ǰ����
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo currentPatient = new FS.HISFC.Models.RADT.PatientInfo();
        /// <summary>
        /// ��ѯʱ��
        /// </summary>
        string queryTime = string.Empty;
        /// <summary>
        /// ��ѯ����
        /// </summary>
        string deptCode = string.Empty;
        /// <summary>
        /// ��ѯ����
        /// </summary>
        string patientNo = string.Empty;
        /// <summary>
        /// DataTable ���
        /// </summary>
        DataTable dtPatientInfo=new DataTable();
        DataTable dtPatientOrder = new DataTable();
        DataTable dtFeeTotal = new DataTable();
        DataTable dtFeeDetail = new DataTable();
        DataTable dtPayInfo = new DataTable();
        DataTable dtShiftDept = new DataTable();
        /// <summary>
        /// ����
        /// </summary>
        Type str = typeof(String);
        Type date = typeof(DateTime);
        Type dec = typeof(Decimal);
        Type bo = typeof(bool);
        ArrayList alDepts = new ArrayList();
        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public ucPatientInHosQuery()
        {
            InitializeComponent();
        }

        private void ucPatientInHosQuery_Load(object sender, EventArgs e)
        {
            try//������п���
            {

                this.alDepts = this.deptManager.GetDeptmentAll();
            }
            catch { }

            this.InitSpreadParm();

            this.Text = "��Ժ���������ѯ";
            //��ʼ�������б�
            this.InitDept();
            this.InitTree();
            //����Ĭ�ϲ�ѯʱ�� 
            this.InitPatientInfo();

            this.InitFeeTotal();

            this.InitOrder();

            this.InitFeeDetail();

            this.InitPayInfo();

            this.InitRADTInfo();
        }
        #endregion

        #region ��ʼ����غ��� 

        /// <summary>
        /// fp��ʼ��
        /// </summary>
        private void InitSpreadParm()
        {
            this.spdPatient_Sheet1.DefaultStyle.Locked = true;
            this.spdFeeTotal_Sheet1.DefaultStyle.Locked = true;
            this.spdOrder_Sheet1.DefaultStyle.Locked = true;
        }

        /// <summary>
        /// ��ʼ�������б�
        /// </summary>
        /// <returns></returns>
        private int InitDept()
        {
            int findAll = 0;
            FS.FrameWork.Models.NeuObject objAll = new FS.FrameWork.Models.NeuObject();

            objAll.ID = "ALL";
            objAll.Name = "ȫ��";

            ArrayList deptList = this.deptManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.I);
            if (deptList == null)
            {
                MessageBox.Show(Language.Msg("���ؿ����б����!") + this.deptManager.Err);
                return -1;
            }

            deptList.Add(objAll);
            findAll = deptList.IndexOf(objAll);
            this.cmbDept.AddItems(deptList);
            if (findAll >= 0)
            {
                this.cmbDept.SelectedIndex = findAll;
            }
            return 1;
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <returns></returns>
        private int InitPatientInfo()
        {
            dtPatientInfo.Columns.AddRange(new DataColumn[]{new DataColumn("סԺ��ˮ��", str),
																new DataColumn("סԺ��", str),
																new DataColumn("����", str),
																new DataColumn("סԺ����", str),
																new DataColumn("����", str),
																new DataColumn("�������", str),
																new DataColumn("Ԥ����(δ��)", dec),
																new DataColumn("���úϼ�(δ��)", dec),
																new DataColumn("���", dec),
																new DataColumn("�Է�", dec),
																new DataColumn("�Ը�", dec),
																new DataColumn("����", dec),
																new DataColumn("��Ժ����", date),
																new DataColumn("��Ժ״̬", str),
																new DataColumn("��Ժ����", str),
																new DataColumn("Ԥ����(�ѽ�)", dec),
																new DataColumn("���úϼ�(�ѽ�)", dec),
																new DataColumn("��������", date)});

            dtPatientInfo.PrimaryKey = new DataColumn[] { dtPatientInfo.Columns["סԺ��ˮ��"] };
                this.spdPatient_Sheet1.DataSource = dtPatientInfo;
            return 1;
        }

        /// <summary>
        /// ���û���
        /// </summary>
        /// <returns></returns>
        private int InitFeeTotal()
        {
            this.dtFeeTotal.Columns.AddRange(new DataColumn[]{ new DataColumn("��������",str),
                                                               new DataColumn("���",dec),
                                                               new DataColumn("�Է�",dec),
                                                               new DataColumn("����",dec),
                                                               new DataColumn("�Ը�",dec),
                                                               new DataColumn("�Żݽ��",dec),
                                                               new DataColumn("����״̬",str)});
            //this.dtFeeTotal.PrimaryKey = new DataColumn[] { this.dtFeeTotal.Columns["��������"] };
            this.spdFeeTotal_Sheet1.DataSource = this.dtFeeTotal;
            return 1;
        }

        /// <summary>
        /// ҽ��
        /// </summary>
        /// <returns></returns>
        private int InitOrder()
        {

            this.dtPatientOrder.Columns.AddRange(new DataColumn[]
			{
				new DataColumn("ҽ������",str),				//2
				new DataColumn("ҽ����ˮ��",str),				//3
				new DataColumn("ҽ��״̬",str),				//4 �¿�������ˣ�ִ��
				new DataColumn("��Ϻ�",str),					//5
				new DataColumn("��ҩ",str),					//6
				new DataColumn("ҽ������",str),				//8
				new DataColumn("��",str),					    //9
				new DataColumn("��ע",str),					//20
				new DataColumn("����",dec),					//9
				new DataColumn("������λ",str),				//10
				new DataColumn("ÿ����",str),				//11
				new DataColumn("��λ",str),					//12
				new DataColumn("����",str),					//13
				new DataColumn("Ƶ�α���",str),				//14
				new DataColumn("Ƶ��",str),				//15
				new DataColumn("�÷�",str),				//17
				new DataColumn("����",str),
				new DataColumn("��ʼʱ��",str),				//18
				new DataColumn("ֹͣʱ��",str),				//19
				new DataColumn("����ҽ��",str),				//21
				new DataColumn("ִ�п���",str),				//23
				new DataColumn("�Ӽ�",str),					//24
				new DataColumn("��鲿λ",str),				//25
				new DataColumn("��������",str),				//26
				new DataColumn("�ۿ����",str),				//28
				new DataColumn("¼����",str),					//30
				new DataColumn("��������",str),				//31
				new DataColumn("����ʱ��",str),				//32
				new DataColumn("ֹͣ��",str),					//34
				new DataColumn("Ƥ�Ա�־",str),				//36
				new DataColumn("���ı�־",bo),
				
			});

            this.spdOrder_Sheet1.DataSource = this.dtPatientOrder;
            return 1;
        }

        /// <summary>
        /// ������ϸ
        /// </summary>
        /// <returns></returns>
        private int InitFeeDetail()
        {
            this.dtFeeDetail.Columns.AddRange(new DataColumn[]{ new DataColumn("����", str),
																new DataColumn("���", str),
																new DataColumn("����", dec),
																new DataColumn("����", dec),
																new DataColumn("����", dec),
																new DataColumn("��λ", str),
																new DataColumn("���", dec),
																new DataColumn("�Է�", dec),
																new DataColumn("����", dec),
																new DataColumn("�Ը�", dec),
																new DataColumn("�Ż�", dec),
																new DataColumn("ִ�п���",str),
																new DataColumn("���߿���",str),
																new DataColumn("�շ�ʱ��", str),
																new DataColumn("�շ�Ա", str),
																new DataColumn("��ҩʱ��", str),   
																new DataColumn("��ҩԱ", str)});
            this.spdFeeInfo_Sheet1.DataSource = this.dtFeeDetail;
            return 1;

        }

        private int InitPayInfo()
        {
            this.dtPayInfo.Columns.AddRange(new DataColumn[]{ new DataColumn("Ʊ�ݺ�", str),
															  new DataColumn("Ԥ�����", dec),
															  new DataColumn("֧����ʽ", str),
															  new DataColumn("����Ա", str),
															  new DataColumn("��������", date),
															  new DataColumn("���ڿ���", str),
															  new DataColumn("����״̬", str),
															  new DataColumn("��Դ", str)});
            this.spdPayInfo_Sheet1.DataSource = this.dtPayInfo;
            return 1;
        }

        private int InitRADTInfo()
        {
            this.dtShiftDept.Columns.AddRange(new DataColumn[] { new DataColumn("ԭ����",str),
                                                                 new DataColumn("ԭ��ʿվ",str),
                                                                 new DataColumn("�¿���",str),
                                                                 new DataColumn("�»�ʿվ",str),
                                                                 new DataColumn("ȷ��ʱ��",str)});
            this.spdShiftInfo_Sheet1.DataSource = this.dtShiftDept;
            return 1;
        }

        #endregion

        #region �¼�

        private void btQuery_Click(object sender, EventArgs e)
        {
            //���ݲ�ѯ������û�����Ϣ�б�
            //��ʼ����
            this.InitTree();
            this.ClearDT();
            //��ѯ����
            ArrayList patientList = new ArrayList();
            ArrayList deptList = new ArrayList(); //�����б�
            this.queryTime = this.dtTime.Value.ToString();
            deptList = this.deptManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.I);
            if (this.cmbDept.Text == "ȫ��")
            {
                foreach (FS.HISFC.Models.Base.Department dept in deptList)
                {
                    deptCode = (dept.ID).ToString();
                    patientList.AddRange(this.patientQuery.PatientInHosQueryByTime(deptCode, queryTime));
                }
            }
            else
            {
                if (this.cmbDept.Text != "")
                {
                    deptCode = (this.cmbDept.Tag).ToString();
                }
                patientList = this.patientQuery.PatientInHosQueryByTime(deptCode, queryTime);
            }
            if (patientList == null)
            {
                MessageBox.Show(Language.Msg("��û�����Ϣ����!") + this.patientQuery.Err);
                return;
            }
            this.GetArraryPatientInfo(patientList);
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void InitTree()
        {
            ArrayList deptList = new ArrayList(); //�����б�
            ArrayList patientList = new ArrayList();//�����б�
            this.queryTime = this.dtTime.Value.ToString();
            this.tvPatientList.ImageList = this.tvPatientList.deptImageList;

            this.tvPatientList.Nodes.Clear();

            if (this.cmbDept.Text == "ȫ��" || this.cmbDept.Text == string.Empty)//Ĭ��Ϊȫ������ ȫ�������б�
            {
                //this.cmbDept.Tag = "ALL";
                //this.cmbDept.Text = "ȫ��";
                deptList = this.deptManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.I);

                TreeNode parentTreeNode = new TreeNode("סԺ����");
                tvPatientList.Nodes.Add(parentTreeNode);

                foreach (FS.HISFC.Models.Base.Department dept in deptList)
                {
                    TreeNode parentNode = new TreeNode();
                    parentNode.Tag = dept.ID;
                    parentNode.Text = dept.Name;
                    parentNode.ImageIndex = 5;
                    parentNode.SelectedImageIndex = 5;
                    //this.tvPatientList.Nodes.Add(parentNode);
                    parentTreeNode.Nodes.Add(parentNode);

                    //����ӽڵ� Ϊ��Ա�б�
                    deptCode = dept.ID;
                    patientList = new ArrayList();
                    patientList = this.patientQuery.PatientInHosQueryByTime(deptCode, queryTime);

                    if (patientList != null)
                    {
                        //��̬������Ա�б�
                        foreach (FS.HISFC.Models.RADT.PatientInfo patientInfo in patientList)
                        {
                            TreeNode patientNode = new TreeNode();
                            patientNode.Tag = patientInfo.ID;
                            patientNode.Text = patientInfo.Name;
                            patientNode.ImageIndex = 0;
                            patientNode.SelectedImageIndex = 1;
                            parentNode.Nodes.Add(patientNode);
                        }
                    }
                }

            }
            else //�г���ǰ����
            {
                TreeNode parentTreeNode = new TreeNode("סԺ����");
                tvPatientList.Nodes.Add(parentTreeNode);

                TreeNode parentNode = new TreeNode();
                parentNode.Tag = this.cmbDept.Tag.ToString();
                parentNode.Text = this.cmbDept.Text;
                parentNode.ImageIndex = 5;
                parentNode.SelectedImageIndex = 5;
                parentTreeNode.Nodes.Add(parentNode);

                //����ӽڵ� Ϊ��Ա�б�
                deptCode = this.cmbDept.Tag.ToString();
                patientList = this.patientQuery.PatientInHosQueryByTime(deptCode, queryTime);
                if (patientList != null)
                {
                    //��̬������Ա�б�
                    foreach (FS.HISFC.Models.RADT.PatientInfo patientInfo in patientList)
                    {
                        TreeNode patientNode = new TreeNode();
                        patientNode.Tag = patientInfo.ID;
                        patientNode.Text = patientInfo.Name;
                        patientNode.ImageIndex = 0;
                        patientNode.SelectedImageIndex = 1;
                        parentNode.Nodes.Add(patientNode);
                    }
                }
            }
        }

        /// <summary>
        /// ��ո���dt
        /// </summary>
        private void ClearDT()
        {
            this.dtFeeDetail.Clear();
            this.dtFeeTotal.Clear();
            this.dtPatientInfo.Clear();
            this.dtPatientOrder.Clear();
            this.dtPayInfo.Clear();
            this.dtShiftDept.Clear();
        }
        #endregion

        #region ��ȡ���߸�����Ϣ��tabҳ��

        /// <summary>
        /// ��û�����Ϣ
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private void GetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            DataRow row = this.dtPatientInfo.NewRow();
            try
            {

                row["סԺ��ˮ��"] = patient.ID;
                row["סԺ��"] = patient.PID.PatientNO;
                row["����"] = patient.Name;
                row["סԺ����"] = patient.PVisit.PatientLocation.Dept.Name;
                row["����"] = patient.PVisit.PatientLocation.Bed.ID;
                row["�������"] = patient.Pact.Name;
                row["Ԥ����(δ��)"] = patient.FT.PrepayCost;
                row["���úϼ�(δ��)"] = patient.FT.TotCost;
                row["���"] = patient.FT.LeftCost;
                row["�Է�"] = patient.FT.OwnCost;
                row["�Ը�"] = patient.FT.PayCost;
                row["����"] = patient.FT.PubCost;
                row["��Ժ����"] = patient.PVisit.InTime;
                row["��Ժ״̬"] = patient.PVisit.InState.Name;

                row["��Ժ����"] = patient.PVisit.OutTime.Date == new DateTime(1, 1, 1).Date ? string.Empty : patient.PVisit.OutTime.ToString();

                row["Ԥ����(�ѽ�)"] = patient.FT.BalancedPrepayCost;
                row["���úϼ�(�ѽ�)"] = patient.FT.BalancedCost;
                row["��������"] = patient.BalanceDate;

                this.dtPatientInfo.Rows.Add(row);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
        }

        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        private void GetArraryPatientInfo(ArrayList patientInfo)
        {
            this.dtPatientInfo.Clear();
            foreach (FS.HISFC.Models.RADT.PatientInfo patient in patientInfo)
            {
                this.GetPatientInfo(patient);
            }
        }

        /// <summary>
        /// ��ʾ���߷��û�����Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        private void GetArraryFeeTotal(ArrayList patientInfo)
        {
            this.dtFeeTotal.Clear();
            foreach (FS.HISFC.Models.RADT.PatientInfo patient in patientInfo)
            {
                this.GetPatientFeeTotal(patient);
            }
        }

        /// <summary>
        /// ��ʾ����ҽ����Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        private void GetArrayPatientOrder(ArrayList patientInfo)
        {
            this.dtPatientOrder.Clear();
            foreach (FS.HISFC.Models.RADT.PatientInfo patient in patientInfo)
            {
                this.GetPatientOrder(patient);
            }
        }

        /// <summary>
        /// ���߷�����ϸ��Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        private void GetArrayFeedetail(ArrayList patientInfo)
        {
            this.dtFeeDetail.Clear();
            foreach (FS.HISFC.Models.RADT.PatientInfo patient in patientInfo)
            {
                this.GetPatientDrugList(patient);
                this.GetPatientUndrugList(patient);
            }
        }

        /// <summary>
        /// ����Ԥ������Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        private void GetArrayPayInfo(ArrayList patientInfo)
        {
            this.dtPayInfo.Clear();
            foreach (FS.HISFC.Models.RADT.PatientInfo patient in patientInfo)
            {
                this.GetPatientPayInfo(patient);
            }
        }

        /// <summary>
        /// ����ת�����
        /// </summary>
        /// <param name="patientInfo"></param>
        private void GetArrayRADTInfo(ArrayList patientInfo)
        {
            this.dtShiftDept.Clear();
            foreach (FS.HISFC.Models.RADT.PatientInfo patient in patientInfo)
            {
                this.GetPatientShiftDept(patient);
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ÿ�������
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        private string GetDeptName(FS.FrameWork.Models.NeuObject dept)
        {
            for (int i = 0; i < this.alDepts.Count; i++)
            {
                FS.FrameWork.Models.NeuObject obj = (FS.FrameWork.Models.NeuObject)this.alDepts[i];
                if (obj.ID == dept.ID)
                {
                    dept.Name = obj.Name;
                    return dept.Name;
                }
            }
            return "";
        }

        /// <summary>
        /// �����Ľڵ���ʾ������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvPatientList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ArrayList deptList = new ArrayList(); //�����б�
            ArrayList patientList = new ArrayList();//�����б�
            this.queryTime = this.dtTime.Value.ToString();
            this.ClearDT();
            if (e.Node.Level == 0)
            {
                deptList = this.deptManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.I);
                foreach (FS.HISFC.Models.Base.Department dept in deptList)
                {
                    deptCode = (dept.ID).ToString();
                    patientList.AddRange(this.patientQuery.PatientInHosQueryByTime(deptCode, queryTime));
                }
            }
            else if (e.Node.Level == 1)
            {
                deptCode = e.Node.Tag.ToString();
                patientList = this.patientQuery.PatientInHosQueryByTime(deptCode, queryTime);
            }
            else
            {
                patientNo = e.Node.Tag.ToString();
                patientList.Add(this.patientQuery.QueryPatientInfoByInpatientNO(patientNo));
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ����,��ȴ�....");
                Application.DoEvents();
                this.GetArraryFeeTotal(patientList);
                this.GetArrayPatientOrder(patientList);
                this.GetArrayFeedetail(patientList);
                this.GetArrayPayInfo(patientList);
                this.GetArrayRADTInfo(patientList);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            this.GetArraryPatientInfo(patientList);
        }

        #endregion

        #region ����ҩƷ��ϸ����ҩƷ��ϸ

        /// <summary>
        /// ��û���ҩƷ��ϸ
        /// </summary>
        /// <param name="patient"></param>
        private void GetPatientDrugList(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            DateTime beginTime = patient.PVisit.InTime;
            DateTime endTime = this.feeManager.GetDateTimeFromSysDateTime();

            ArrayList drugList = this.feeManager.GetMedItemsForInpatient(patient.ID, beginTime, endTime);
            if (drugList == null)
            {
                MessageBox.Show(Language.Msg("��û���ҩƷ��ϸ����!") + this.feeManager.Err);

                return;
            }
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList obj in drugList)
            {
                DataRow row = this.dtFeeDetail.NewRow();

                row["����"] = obj.Item.Name;
                FS.HISFC.Models.Pharmacy.Item medItem = (FS.HISFC.Models.Pharmacy.Item)obj.Item;
                row["���"] = medItem.Specs;
                row["����"] = obj.Item.Price;
                row["����"] = obj.Item.Qty;
                row["����"] = obj.Days;
                row["��λ"] = obj.Item.PriceUnit;
                row["���"] = obj.FT.TotCost;
                row["�Է�"] = obj.FT.OwnCost;
                row["����"] = obj.FT.PubCost;
                row["�Ը�"] = obj.FT.PayCost;
                row["�Ż�"] = obj.FT.RebateCost;
                row["ִ�п���"] = this.deptManager.GetDeptmentById(obj.ExecOper.Dept.ID).Name;
                row["���߿���"] = this.deptManager.GetDeptmentById(((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID).Name;
                row["�շ�ʱ��"] = obj.FeeOper.OperTime;

                FS.HISFC.BizProcess.Integrate.Manager managerIntergrate = new FS.HISFC.BizProcess.Integrate.Manager();
                FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
                empl = managerIntergrate.GetEmployeeInfo(obj.FeeOper.ID);
                if (empl.Name == string.Empty)
                {
                    row["�շ�Ա"] = obj.FeeOper.ID;
                }
                else
                {
                    row["�շ�Ա"] = empl.Name;
                }


                row["��ҩʱ��"] = obj.ExecOper.OperTime.Date == new DateTime(1, 1, 1).Date ? string.Empty : obj.ExecOper.OperTime.ToString();

                FS.HISFC.Models.Base.Employee confirmOper = new FS.HISFC.Models.Base.Employee();
                confirmOper = managerIntergrate.GetEmployeeInfo(obj.ExecOper.ID);

                if (confirmOper.Name == string.Empty)
                {
                    row["��ҩԱ"] = obj.ExecOper.ID;
                }
                else
                {
                    row["��ҩԱ"] = confirmOper.Name;
                }

                this.dtFeeDetail.Rows.Add(row);
            }
        }

        /// <summary>
        /// ��ѯ���߷�ҩƷ��ϸ
        /// </summary>
        /// <param name="patient"></param>
        private void GetPatientUndrugList(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            DateTime beginTime = patient.PVisit.InTime;
            DateTime endTime = this.feeManager.GetDateTimeFromSysDateTime();

            ArrayList undrugList = this.feeManager.QueryFeeItemLists(patient.ID, beginTime, endTime);
            if (undrugList == null)
            {
                MessageBox.Show(Language.Msg("��û��߷�ҩƷ��ϸ����!") + this.feeManager.Err);

                return;
            }

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList obj in undrugList)
            {
                DataRow row = this.dtFeeDetail.NewRow();

                row["����"] = obj.Item.Name;
                row["����"] = obj.Item.Price;
                row["����"] = obj.Item.Qty;
                row["��λ"] = obj.Item.PriceUnit;
                row["���"] = obj.FT.TotCost;
                row["�Է�"] = obj.FT.OwnCost;
                row["����"] = obj.FT.PubCost;
                row["�Ը�"] = obj.FT.PayCost;
                row["�Ż�"] = obj.FT.RebateCost;
                row["�շ�ʱ��"] = obj.FeeOper.OperTime;

                //�տ�Ա����
                FS.HISFC.BizProcess.Integrate.Manager managerIntergrate = new FS.HISFC.BizProcess.Integrate.Manager();
                FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
                empl = managerIntergrate.GetEmployeeInfo(obj.FeeOper.ID);

                if (empl.Name == string.Empty)
                {
                    row["�շ�Ա"] = obj.FeeOper.ID;
                }
                else
                {
                    row["�շ�Ա"] = empl.Name;
                }

                row["ִ�п���"] = this.deptManager.GetDeptmentById(obj.ExecOper.Dept.ID).Name;
                row["���߿���"] = this.deptManager.GetDeptmentById(((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID).Name;

                this.dtFeeDetail.Rows.Add(row);
            }
        }
        #endregion

        #region ����ҽ����ϸ

        #region ��ҽ���õ�

        private DataRow AddObjectToRow(object obj, DataTable table)
        {

            DataRow row = table.NewRow();

            string strTemp = "";
            FS.HISFC.Models.Order.Inpatient.Order order = null;
            order = obj as FS.HISFC.Models.Order.Inpatient.Order;

            if (order.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
            {
                FS.HISFC.Models.Pharmacy.Item objItem = order.Item as FS.HISFC.Models.Pharmacy.Item;
                row["��ҩ"] = System.Convert.ToInt16(order.Combo.IsMainDrug);	//6
                row["ÿ����"] = order.DoseOnce.ToString();					//10
                row["��λ"] = objItem.DoseUnit;								//0415 2307096 wang renyi
                row["����"] = order.HerbalQty;								//11
            }
            else if (order.Item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug))
            {
                //FS.HISFC.Models.Fee.Item objItem = order.Item as FS.HISFC.Models.Fee.Item;
            }

            row["ҽ������"] = order.OrderType.Name;								//2
            row["ҽ����ˮ��"] = order.ID;										//3
            row["ҽ��״̬"] = order.Status;										//12 �¿�������ˣ�ִ��
            row["��Ϻ�"] = order.Combo.ID;	//5

            if (order.Item.Specs == null || order.Item.Specs.Trim() == "")
            {
                row["ҽ������"] = order.Item.Name;
            }
            else
            {
                row["ҽ������"] = order.Item.Name + "[" + order.Item.Specs + "]";
            }

            //ҽ����ҩ
            if (order.IsPermission) row["ҽ������"] = "��" + row["ҽ������"];

            row["����"] = order.Qty;
            row["������λ"] = order.Unit;
            row["Ƶ�α���"] = order.Frequency.ID;
            row["Ƶ��"] = order.Frequency.Name;
            row["�÷�"] = order.Usage.Name;
            row["����"] = order.Item.SysClass.Name;
            row["��ʼʱ��"] = order.BeginTime;
            if (order.ExeDept.Name == "" && order.ExeDept.ID != "") order.ExeDept.Name = this.GetDeptName(order.ExeDept);
            row["ִ�п���"] = order.ExeDept.Name;
            if (order.IsEmergency)
            {
                strTemp = "��";
            }
            else
            {
                strTemp = "��";
            }
            row["�Ӽ�"] = strTemp;
            row["��鲿λ"] = order.CheckPartRecord;
            row["��������"] = order.Sample;
            row["�ۿ����"] = deptHelper.GetName(order.StockDept.ID);

            row["��ע"] = order.Memo;
            row["¼����"] = order.Oper.Name;
            if (order.ReciptDept.Name == "" && order.ReciptDept.ID != "") order.ReciptDept.Name = this.GetDeptName(order.ReciptDept);
            row["����ҽ��"] = order.ReciptDoctor.Name;
            row["��������"] = order.ReciptDept.Name;
            row["����ʱ��"] = order.MOTime.ToString();
            row["ֹͣʱ��"] = order.EndTime;
            row["ֹͣ��"] = order.DCOper.Name;
            row["Ƥ�Ա�־"] = order.HypoTest;
            row["���ı�־"] = order.IsSubtbl;
            return row;
        }

               #endregion

        /// <summary>
        /// ��û���ҽ����Ϣ
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private void GetPatientOrder(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            ArrayList alAllOrder = orderManagement.QueryOrder(patient.ID);
            if (alAllOrder == null) return;
            foreach (FS.HISFC.Models.Order.Inpatient.Order orderObj in alAllOrder)
            {
                if (orderObj == null) continue;
                this.dtPatientOrder.Rows.Add(AddObjectToRow(orderObj, dtPatientOrder));
            }
        }
        #endregion

        #region ���߷��û���
        /// <summary>
        /// ���߷��úϼ�
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private void GetPatientFeeTotal(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            DateTime beginTime = patient.PVisit.InTime;
            DateTime endTime = this.feeManager.GetDateTimeFromSysDateTime();

            ArrayList feeInfoList = this.feeManager.QueryFeeInfosGroupByMinFeeByInpatientNO(patient.ID, beginTime, endTime, "0");
            if (feeInfoList == null)
            {
                MessageBox.Show(Language.Msg("��û��߷��û�����ϸ����!") + this.feeManager.Err);

                return;
            }

            this.dtFeeTotal.Rows.Clear();

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo in feeInfoList)
            {

                DataRow row = dtFeeTotal.NewRow();

                row["��������"] = this.feeManager.GetComDictionaryNameByID("MINFEE", feeInfo.Item.MinFee.ID);
                row["���"]     = feeInfo.FT.TotCost;
                row["�Է�"]     = feeInfo.FT.OwnCost;
                row["����"]     = feeInfo.FT.PubCost;
                row["�Ը�"]     = feeInfo.FT.PayCost;
                row["�Żݽ��"] = feeInfo.FT.RebateCost;
                row["����״̬"] = "δ����";

                dtFeeTotal.Rows.Add(row);
            }

            ArrayList feeInfoListBalanced = this.feeManager.QueryFeeInfosGroupByMinFeeByInpatientNO(patient.ID, beginTime, endTime, "1");
            if (feeInfoListBalanced == null)
            {
                MessageBox.Show(Language.Msg("��û��߷��û�����ϸ����!") + this.feeManager.Err);

                return;
            }

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo in feeInfoListBalanced)
            {

                DataRow row = dtFeeTotal.NewRow();

                row["��������"] = this.feeManager.GetComDictionaryNameByID("MINFEE", feeInfo.Item.MinFee.ID);
                row["���"]     = feeInfo.FT.TotCost;
                row["�Է�"]     = feeInfo.FT.OwnCost;
                row["����"]     = feeInfo.FT.PubCost;
                row["�Ը�"]     = feeInfo.FT.PayCost;
                row["�Żݽ��"] = feeInfo.FT.RebateCost;
                row["����״̬"] = "�ѽ���";

                dtFeeTotal.Rows.Add(row);
            }
        }
        #endregion

        #region ����Ԥ����
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private void GetPatientPayInfo(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            DateTime beginTime = patient.PVisit.InTime;
            DateTime endTime = this.feeManager.GetDateTimeFromSysDateTime();

            ArrayList prepayList = this.feeManager.QueryPrepays(patient.ID);
            if (prepayList == null)
            {
                MessageBox.Show(Language.Msg("��û���Ԥ������ϸ����!") + this.feeManager.Err);

                return;
            }

            foreach (FS.HISFC.Models.Fee.Inpatient.Prepay prepay in prepayList)
            {
                FS.HISFC.Models.Base.Employee employeeObj = new FS.HISFC.Models.Base.Employee();
                FS.HISFC.Models.Base.Department deptObj = new FS.HISFC.Models.Base.Department();
                DataRow row = this.dtPayInfo.NewRow();

                row["Ʊ�ݺ�"] = prepay.RecipeNO;
                row["Ԥ�����"] = prepay.FT.PrepayCost;
                row["֧����ʽ"] = prepay.PayType.Name;
                employeeObj = this.personManager.GetPersonByID(prepay.PrepayOper.ID);
                row["����Ա"] = employeeObj.Name;
                row["��������"] = prepay.PrepayOper.OperTime;
                deptObj = this.deptManager.GetDeptmentById(((FS.HISFC.Models.RADT.PatientInfo)prepay.Patient).PVisit.PatientLocation.Dept.ID);
                row["���ڿ���"] = deptObj.Name;
                string tempBalanceStatusName = string.Empty;
                switch (prepay.BalanceState)
                {
                    case "0":
                        tempBalanceStatusName = "δ����";
                        break;
                    case "1":
                        tempBalanceStatusName = "�ѽ���";
                        break;
                    case "2":
                        tempBalanceStatusName = "�ѽ�ת";
                        break;
                }
                row["����״̬"] = tempBalanceStatusName;
                string tempPrepayStateName = string.Empty;
                switch (prepay.PrepayState)
                {
                    case "0":
                        tempPrepayStateName = "��ȡ";
                        break;
                    case "1":
                        tempPrepayStateName = "����";
                        break;
                    case "2":
                        tempPrepayStateName = "����";
                        break;
                }
                this.dtPayInfo.Rows.Add(row);
            }
        }
        #endregion

        #region ����ת����Ϣ
        /// <summary>
        /// ת����Ϣ
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private void GetPatientShiftDept(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            ArrayList radtList = this.patientQuery.GetPatientRADTInfo(patient.ID);
            if (radtList == null)
            {
                MessageBox.Show(Language.Msg("��û���ת����ϸ����!") + this.patientQuery.Err);

                return;
            }

            foreach (FS.HISFC.Models.Invalid.CShiftData csdata in radtList)
            {
                DataRow row = this.dtShiftDept.NewRow();

                row["ԭ����"] = this.deptManager.GetDeptmentById(csdata.OldDataCode).Name;
                row["ԭ��ʿվ"] = this.deptManager.GetDeptmentById(csdata.OldDataName).Name;
                row["�¿���"] = this.deptManager.GetDeptmentById(csdata.NewDataCode).Name;
                row["�»�ʿվ"] = this.deptManager.GetDeptmentById(csdata.NewDataName).Name;
                row["ȷ��ʱ��"] = csdata.User03;
                this.dtShiftDept.Rows.Add(row);
            }
        }
        #endregion

        private void spdPatient_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            ArrayList patientList = new ArrayList();
            patientNo = this.spdPatient_Sheet1.Cells[e.Row, 0].Text.ToString();
            patientList.Add(this.patientQuery.QueryPatientInfoByInpatientNO(patientNo));

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ����,��ȴ�....");
            Application.DoEvents();
            this.GetArraryFeeTotal(patientList);
            this.GetArrayPatientOrder(patientList);
            this.GetArrayFeedetail(patientList);
            this.GetArrayPayInfo(patientList);
            this.GetArrayRADTInfo(patientList);
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

        }

    }
}
