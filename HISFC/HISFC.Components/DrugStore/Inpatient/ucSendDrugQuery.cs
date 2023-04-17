using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.DrugStore.Inpatient
{
    public partial class ucSendDrugQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucSendDrugQuery()
        {
            InitializeComponent();
 

            //Ĭ�ϲ���ʾ�˷���Ϣ/��������Ϣ
            this.IsShowQuitBill = false;
            this.IsShowOutBill = false;
        }

        /// <summary>
        /// �б�ڵ��������
        /// </summary>
        public enum NodeType
        {
            /// <summary>
            /// ����
            /// </summary>
            Patient,
            /// <summary>
            /// ȡҩ����
            /// </summary>
            Dept
        }

        /// <summary>
        /// ȡҩ�������� 
        /// </summary>
        public enum ReciveDrugType
        {
            /// <summary>
            /// ����
            /// </summary>
            Dept,
            /// <summary>
            /// ����վ
            /// </summary>
            NurseCell
        }


        #region �����

        /// <summary>
        /// ҩ��������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

        /// <summary>
        /// ҩ�������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ������Ϣ������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager departmentManager = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ���ҹ������û�ֻ�� ѡ�����ڴ���µ����� С���б�
        /// </summary>
        FS.HISFC.BizLogic.Manager.DepartmentStatManager manager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();


        /// <summary>
        /// ���ڵ����� 0 ����վ Patient ��ʾ���� 1 ���� Dept ȡҩ����
        /// </summary>
        protected NodeType treeType = NodeType.Patient;

        /// <summary>
        /// ȡҩ��������
        /// </summary>
        protected ReciveDrugType reciveType = ReciveDrugType.Dept;

        /// <summary>
        /// סԺ�Ų�ѯ
        /// </summary>
        protected string InPatientNo = "";

        /// <summary>
        /// ҩƷ����
        /// </summary>
        protected DataSet QualityDataSet = new DataSet();

        /// <summary>
        /// ��������ѯ
        /// </summary>
        protected DataSet DeptDataSet = new DataSet();

        /// <summary>
        /// ����վ��ѯ��DataSet
        /// </summary>
        protected DataSet NurseDtaSet = new DataSet();

        /// <summary>
        /// ��ѯ���� �ɲ�ѯ������վ�����п���
        /// </summary>
        private ArrayList deptInfo = null;

        /// <summary>
        /// ������
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��Ա������
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper personHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��ǰ����Ա��Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.Employee operVar = null;

        /// <summary>
        /// ��ǰ��ѯ����������
        /// </summary>
        private object nowObj = null;

        /// <summary>
        /// ��ǰѡ���ѯ�Ĳ���
        /// </summary>
        private int showLevel = 0;

        #endregion

        #region ����

        /// <summary>
        /// ���ڵ����� 0 ����վ Patient ��ʾ���� 1 ���� Dept ȡҩ����
        /// </summary>
        [Description("������ڵ������������"), Category("����"), DefaultValue(ucSendDrugQuery.NodeType.Patient)]
        public NodeType TreeType
        {
            get
            {
                return this.treeType;
            }
            set
            {
                this.treeType = value;

                if (value == NodeType.Patient)			//����վʹ�� ��ʾ�����б�/�˷���Ϣ/��������Ϣ
                {              
                    if (this.SpreadDrug.Sheets.Contains(this.sheetViewDetail))
                        this.SpreadDrug.Sheets.Remove(this.sheetViewDetail);
                    //����ʾҩƷ�����б�
                    if (this.neuTabControl1.TabPages.Contains(this.tpQuality))
                        this.neuTabControl1.TabPages.Remove(this.tpQuality);

                    this.IsShowCheck = false;
                    //this.ShowNurse();

                    this.lbTime.Text = "����ʱ�䣺";
                }
                else					//ҩ����ҩ��ѯ ��ʾȡҩ����/ҩƷ�����б�
                {
                    //����ʾ�˷���Ϣ/��������Ϣ�б�
                    if (this.neuTabControl2.TabPages.Contains(this.tpQuitFee))
                        this.neuTabControl2.TabPages.Remove(this.tpQuitFee);
                    if (this.neuTabControl2.TabPages.Contains(this.tpOutBill))
                        this.neuTabControl2.TabPages.Remove(this.tpOutBill);

                    //this.ShowDept();
                    //this.ShowDrugQuality();

                    this.lbTime.Text = "��ҩʱ�䣺";
                }
            }
        }

        /// <summary>
        /// ȡҩ��������
        /// </summary>
        [Description("ȡҩ�������� ���һ���վ"), Category("����"), DefaultValue(ucSendDrugQuery.ReciveDrugType.Dept)]
        public ReciveDrugType ReciveType
        {
            get
            {
                return this.reciveType;
            }
            set
            {
                this.reciveType = value;                
            }
        }

        /// <summary>
        /// �Ƿ���ʾ��������ѯTab
        /// </summary>
        [Description("�Ƿ���ʾ��������ѯTab"), Category("����"), DefaultValue(false),Browsable(false)]
        public bool IsShowOutBill
        {
            get
            {
                return this.neuTabControl2.TabPages.Contains(this.tpOutBill);
            }
            set
            {
                if (value && !this.neuTabControl2.TabPages.Contains(this.tpOutBill))
                    this.neuTabControl2.TabPages.Add(this.tpOutBill);
                if (!value && this.neuTabControl2.TabPages.Contains(this.tpOutBill))
                    this.neuTabControl2.TabPages.Remove(this.tpOutBill);
            }
        }

        /// <summary>
        /// �Ƿ���ʾ�˷ѵ���ѯTab
        /// </summary>
        [Description("�Ƿ���ʾ�˷ѵ���ѯTab"), Category("����"), DefaultValue(false),Browsable(false)]
        public bool IsShowQuitBill
        {
            get
            {
                return this.neuTabControl2.TabPages.Contains(this.tpQuitFee);
            }
            set
            {
                if (value && !this.neuTabControl2.TabPages.Contains(this.tpQuitFee))
                    this.neuTabControl2.TabPages.Add(this.tpQuitFee);
                if (!value && this.neuTabControl2.TabPages.Contains(this.tpQuitFee))
                    this.neuTabControl2.TabPages.Remove(this.tpQuitFee);
            }
        }

        /// <summary>
        /// �Ƿ���ʾ�Ѱ�/δ�ڹ���ѡ���
        /// </summary>
        [Description("�Ƿ���ʾ�Ѱ�/δ�ڹ���ѡ���"), Category("����"), DefaultValue(true),Browsable(false)]
        public bool IsShowCheck
        {
            get
            {
                return this.rbSended.Visible;
            }
            set
            {
                this.rbSended.Visible = value;
                this.rbSending.Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ���˿�
        /// </summary>
        [Description("�Ƿ���ʾ���˿�"), Category("����"), DefaultValue(true)]
        public bool IsShowFilter
        {
            set
            {
                this.lbFilter.Visible = value;
                this.txtFilter.Visible = value;
            }
        }

        /// <summary>
        ///  �Ƿ��в�ѯȨ�� ���в�ѯȨ�޿��ԶԲ�ѯʱ������޸�
        /// </summary>
        [Description("�Ƿ���Ҫ��ѯȨ�� ����ҪȨ�� ���޲�ѯȨ��ʱ���ܶԲ�ѯʱ������޸�"), Category("����"), 
        DefaultValue(true),Browsable(false)]
        public bool IsPrivQuery
        {
            set
            {
                this.dtpBegin.Enabled = value;
                this.dtpEnd.Enabled = value;
            }
        }

        /// <summary>
        /// ��ѯ���� �ɲ�ѯ������վ�����п���
        /// </summary>
        [Description("��ѯ����"), Category("����"), DefaultValue(true),Browsable(false)]
        public ArrayList DeptInfo
        {
            get
            {
                if (this.deptInfo == null)
                    this.deptInfo = new ArrayList();
                return this.deptInfo;
            }
            set
            {
                this.deptInfo = value;
            }
        } 

        #endregion

        #region ��������Ϣ

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();

            return 1;
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();

            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            object obj = this.hashTableFp[this.neuTabControl2.SelectedTab];

            FarPoint.Win.Spread.FpSpread fp = obj as FarPoint.Win.Spread.FpSpread;

            SaveFileDialog op = new SaveFileDialog();

            op.Title = "��ѡ�񱣴��·��������";
            op.CheckFileExists = false;
            op.CheckPathExists = true;
            op.DefaultExt = "*.xls";
            op.Filter = "(*.xls)|*.xls";

            DialogResult result = op.ShowDialog();

            if (result == DialogResult.Cancel || op.FileName == string.Empty)
            {
                return -1;
            }

            string filePath = op.FileName;

            bool returnValue = fp.SaveExcel(filePath, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);


            return base.Export(sender, neuObject);
        
        }

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��Fp����
        /// </summary>
        protected void FpInit()
        {
            //�Ի���������ʾʱ �Ե�һ�� �ڶ��н�����ͬ��ֵ�ϲ�
            this.sheetViewTot.SetColumnMerge(0, FarPoint.Win.Spread.Model.MergePolicy.Always);
            this.sheetViewTot.SetColumnMerge(1, FarPoint.Win.Spread.Model.MergePolicy.Always);
            //����ϸ������ʾʱ �Ե�һ�н�����ͬ��ֵ�ϲ�
            this.sheetViewDetail.SetColumnMerge(0, FarPoint.Win.Spread.Model.MergePolicy.Always);
            //���ö�����ʾ
            this.sheetViewTot.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetViewTot.Columns[0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetViewTot.Columns[1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetViewDetail.Columns[0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
        }

        /// <summary>
        /// ��ʼ����Ա��Ϣ
        /// </summary>
        protected void OperInit()
        {
            if (this.operVar == null)
                this.operVar = ((FS.HISFC.Models.Base.Employee)this.itemManager.Operator);
            System.Collections.ArrayList depts = new ArrayList();
            depts = manager.GetMultiSubDept(operVar.Dept.ID);
            this.cmbDrugDept.AddItems(depts);
            if (depts.Count > 0)
            {
                cmbDrugDept.Text = operVar.Dept.Name;
                cmbDrugDept.Tag = operVar.Dept.ID;
            }
        }

        /// <summary>
        /// ���ݳ�ʼ��
        /// </summary>
        protected void DataInit()
        {
            this.dtpEnd.Value = this.itemManager.GetDateTimeFromSysDateTime();
            this.dtpBegin.Value = this.dtpEnd.Value.AddDays(-1);

            //ȡȫԺ������Ϣ
            deptHelper.ArrayObject = this.departmentManager.GetDepartment();

            if (this.reciveType == ReciveDrugType.Dept)
            {
                this.deptInfo = departmentManager.QueryDepartment(this.operVar.Nurse.ID);
                if (this.deptInfo == null)
                {
                    FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
                    info.ID = this.operVar.Dept.ID;
                    info.Name = this.operVar.Dept.Name;
                    this.deptInfo.Add(info);
                }
            }
            else
            {
                //this.deptInfo = new ArrayList();
//{723DB273-E312-4bc6-9879-C7B311D157D9}
                //FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
                //info.ID = this.operVar.Nurse.ID;
                //info.Name = this.operVar.Nurse.Name;
                //this.deptInfo.Add(info);
                //{6D1A37FE-771B-460f-B6D5-E578D996A942}
                //this.deptInfo = departmentManager.QueryDepartment(this.operVar.Nurse.ID);
                //{A4ED7668-8B4C-441f-9A0D-7029EA040B14}
                this.deptInfo = new ArrayList();

                FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
                info.ID = this.operVar.Nurse.ID;
                info.Name = this.operVar.Nurse.Name;
                this.deptInfo.Add(info);
            }

            //ȡ��Ա��Ϣ
            FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
            this.personHelper.ArrayObject = managerIntegrate.QueryEmployeeAll();
        }
        /// <summary>
        /// ��������
        /// </summary>
        protected Hashtable hashTableFp = new Hashtable();
        private void InitHashTable()
        {
            foreach (TabPage t in this.neuTabControl2.TabPages)
            {
                foreach (Control c in t.Controls)
                {
                    if (c is FarPoint.Win.Spread.FpSpread)
                    {
                        this.hashTableFp.Add(t, c);
                    }
                }
            }
        }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Init()
        {
            this.FpInit();

            this.OperInit();

            this.DataInit();

            this.tvDept.ImageList = this.tvDept.deptImageList;

            this.ucQueryInpatientNo1.InputType = 0;			//�������� סԺ��
            this.InitHashTable();
        }

        /// <summary>
        /// ���ݼ�����ʾ
        /// </summary>
        public void ShowData()
        {
            if (this.treeType == NodeType.Patient)			//����վʹ�� ��ʾ�����б�/�˷���Ϣ/��������Ϣ
            {
                this.ShowNurse();
            }
            else					//ҩ����ҩ��ѯ ��ʾȡҩ����/ҩƷ�����б�
            {         
                this.ShowDept();
                this.ShowDrugQuality();
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʾ����վ�б�
        /// </summary>
        public void ShowNurse()
        {
            FS.HISFC.BizProcess.Integrate.RADT radtManager = new FS.HISFC.BizProcess.Integrate.RADT();

            this.OperInit();

            TreeNode deptNode = new TreeNode();
            //deptNode.Text = this.operVar.Nurse.Name;
            deptNode.Text = this.cmbDrugDept.Text;
            deptNode.ImageIndex = 0;// (int)FS.FrameWork.WinForms.Classes.EnumImageList.T����;
            deptNode.SelectedImageIndex = 0;

            ArrayList al = new ArrayList();
            //{723DB273-E312-4bc6-9879-C7B311D157D9}
            //al = radtManager.QueryPatient(this.operVar.Dept.ID, FS.HISFC.Models.Base.EnumInState.I);
            //al = radtManager.QueryPatientByNurseCellAndState (this.operVar.Dept.ID, FS.HISFC.Models.Base.EnumInState.I);
            //{A4ED7668-8B4C-441f-9A0D-7029EA040B14}
            al = radtManager.QueryPatient(this.cmbDrugDept.Tag.ToString(), FS.HISFC.Models.Base.EnumInState.I);

            //al = radtManager.QueryPatient(this.operVar.Dept.ID, FS.HISFC.Models.Base.EnumInState.I);
            if (al == null)
            {
                MessageBox.Show(Language.Msg("��ѯ���������б����!"));
                return;
            }

            TreeNode patientNode;

            foreach (FS.HISFC.Models.RADT.PatientInfo patientInfo in al)
            {
                patientNode = new TreeNode();
                patientNode.Text = "��" + patientInfo.PVisit.PatientLocation.Bed.Name + "��" + patientInfo.Name;
                patientNode.SelectedImageIndex = 1;
                patientNode.ImageIndex = 5;// (int)FS.FrameWork.WinForms.Classes.EnumImageList.A��Ա;
                patientNode.Tag = patientInfo.ID;
                deptNode.Nodes.Add(patientNode);
            }
            this.tvDept.Nodes.Add(deptNode);
            this.tvDept.ExpandAll();

            //FS.HISFC.Object.RADT.Location loc = new FS.HISFC.Object.RADT.Location();
            //loc.NurseCell = this.operVar.User.Nurse.Clone();
            //loc.Dept = this.operVar.User.Dept.Clone();
            //FS.HISFC.Object.RADT.VisitStatus state = new FS.HISFC.Object.RADT.VisitStatus();
            //state.ID = FS.HISFC.Object.RADT.VisitStatus.enuVisitStatus.I;
            //al = inPatient.PatientQuery(loc, state);                       
        }

        /// <summary>
        /// ��ʾ�����б�
        /// </summary>
        public void ShowDept()
        {
            this.OperInit();

            FS.HISFC.BizLogic.Pharmacy.Constant phaConstant = new FS.HISFC.BizLogic.Pharmacy.Constant();
            ArrayList al = phaConstant.QueryReciveDrugDept(this.operVar.Dept.ID);
            if (al == null)
            {
                MessageBox.Show(Language.Msg("��ȡȡҩ�����б����" + phaConstant.Err));
                return;
            }

            TreeNode deptNode;
            TreeNode rootNode = new TreeNode("ȡҩ����");
            rootNode.ImageIndex = 0;
            rootNode.SelectedImageIndex = 0;
            rootNode.Tag = "AAAA";
            foreach (FS.FrameWork.Models.NeuObject info in al)
            {
                if (info == null) 
                    continue;
                deptNode = new TreeNode();
                deptNode.Text = info.Name;
                deptNode.ImageIndex = 4;
                deptNode.SelectedImageIndex = 5;
                deptNode.Tag = info.ID;
                rootNode.Nodes.Add(deptNode);
            }
            this.tvDept.Nodes.Add(rootNode);
            this.tvDept.ExpandAll();
        }

        /// <summary>
        /// ��ʾҩƷ�����б�
        /// </summary>
        public void ShowDrugQuality()
        {

        }

        /// <summary>
        /// �������� ��������
        /// </summary>
        public void Query()
        {
            try
            {
                if (this.treeType == NodeType.Dept && this.neuTabControl1.SelectedTab == this.tpQuality && this.showLevel == 1)
                {
                    this.tvDrugType1.SelectedNode = this.tvDrugType1.Nodes[0];
                }

                this.Query(this.nowObj, this.showLevel);

                this.QueryNoExamData();

                this.SetFpColor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("��ѯִ�г���" + ex.Message));
                return;
            }
        }

        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// <param name="obj">������Ϣ</param>
        /// <param name="level">0 ������ڵ� 1 ����ӽڵ� 2 ͨ��סԺ�Ų�ѯ</param>
        protected void Query(object obj, int level)
        {
            if (this.treeType == NodeType.Dept)             //�б���ʾȡҩ���� ��ҩ�����а�ҩ��ѯ
            {
                this.SpreadDrug.ActiveSheet = this.sheetViewTot;

                if (this.SpreadDrug.Sheets.Contains(this.sheetViewDetail))
                    this.SpreadDrug.Sheets.Remove(this.sheetViewDetail);

                this.neuTabControl2.SelectedTab = this.tpDruged;
            }

            if (level == 1 && obj == null) 
                return;

            DataSet ds = new DataSet();
            if (this.treeType == NodeType.Patient)			//�б���ʾ�����黼�� ����������ȡҩ��ѯ
            {
                #region ����վ

                //�Ըû���վ��Ӧ�Ŀ��ҽ��в�ѯ
                string dept = "";
                foreach (FS.FrameWork.Models.NeuObject info in this.deptInfo)
                {
                    if (dept == "")
                        dept = info.ID;
                    else
                        dept = dept + "','" + info.ID;
                }
                if (level == 0)                             //���ڵ��ѯ ��ѯ���������л��ߵĻ��ܰ�ҩ���
                {
                    this.NurseDtaSet = new DataSet();
                    string[] strIndex = new string[1] { "Pharmacy.Item.GetApplyOutTot.ByTime" };
                    this.itemManager.ExecQuery(strIndex, ref NurseDtaSet, dept, this.dtpBegin.Value.ToString(), this.dtpEnd.Value.ToString());
                }
                else                                      //�ӽڵ��ѯ ��ѯָ�����ߵİ�ҩ���
                {
                    this.NurseDtaSet = new DataSet();
                    this.ucQueryInpatientNo1.Text = obj as string;
                    this.ucQueryInpatientNo1.Text = this.ucQueryInpatientNo1.Text.Substring(4);
                    string[] strIndex = new string[1] { "Pharmacy.Item.GetApplyOutTot.ByPatient" };
                    this.itemManager.ExecQuery(strIndex, ref NurseDtaSet, obj as string, this.dtpBegin.Value.ToString(), this.dtpEnd.Value.ToString());
                    if (NurseDtaSet != null && NurseDtaSet.Tables.Count > 0)
                    {
                        try
                        {
                            for (int i = 0; i < NurseDtaSet.Tables[0].Rows.Count; i++)
                            {
                                if (NurseDtaSet.Tables[0].Rows[i]["�������"] != null)
                                {
                                    NurseDtaSet.Tables[0].Rows[i]["�������"] = deptHelper.GetName(NurseDtaSet.Tables[0].Rows[i]["�������"].ToString());
                                }
                            }
                        }
                        catch { }
                    }
                }
                if (NurseDtaSet != null && NurseDtaSet.Tables.Count > 0)
                {
                    DataView dv = new DataView(this.NurseDtaSet.Tables[0]);
                    if (this.rbSended.Checked == true)
                    {
                        dv.RowFilter = string.Format("�Ƿ��ҩ = '{0}'", "�Ѱ�");
                    }
                    if (this.rbSending.Checked == true)
                    {
                        dv.RowFilter = string.Format("�Ƿ��ҩ = '{0}'", "δ��");
                    }
                    this.sheetViewTot.DataSource = dv;
                }
                    
                this.SetFormat();

                #endregion

                this.SetFpColor();
            }
            else
            {
                if (level == 2)                           //���ݻ���סԺ�Ų�ѯ
                {
                    string[] strIndex = new string[1] { "Pharmacy.Item.GetApplyOutTot.ByPatient" };
                    this.itemManager.ExecQuery(strIndex, ref ds, obj as string, this.dtpBegin.Value.ToString(), this.dtpEnd.Value.ToString());
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (ds.Tables[0].Columns.Contains("�������"))
                            {
                                dr["�������"] = this.deptHelper.GetName(dr["�������"].ToString());
                            }
                            if (ds.Tables[0].Columns.Contains("������"))
                            {
                                dr["������"] = this.personHelper.GetName(dr["������"].ToString());
                            }
                            if (ds.Tables[0].Columns.Contains("��ҩ��"))
                            {
                                dr["��ҩ��"] = this.personHelper.GetName(dr["��ҩ��"].ToString());
                            }
                        }
                        this.sheetViewTot.DataSource = ds;
                    }
                    this.SetNurseFormat();
                    return;
                }

                #region ����
                if (this.neuTabControl1.SelectedTab == this.tpQuality)
                {
                    #region ����ҩƷ���ʼ���
                    string qualityCode = "";
                    if (level == 0)
                    {

                        string applyState = "0','2','1";
                        if (this.rbSended.Checked)
                            applyState = "2','1";
                        if (this.rbSending.Checked)
                            applyState = "0";
                        this.QualityDataSet = new DataSet();
                        string[] strIndex = new string[1] { "Pharmacy.Item.GetApplyOut.ByDrugQuality" };
                        this.itemManager.ExecQuery(strIndex, ref QualityDataSet, this.operVar.Dept.ID, this.dtpBegin.Value.ToString(), this.dtpEnd.Value.ToString(), applyState);
                        if (QualityDataSet != null && QualityDataSet.Tables.Count > 0)
                            this.sheetViewTot.DataSource = QualityDataSet;
                        else
                            return;
                        if (this.QualityDataSet.Tables[0].Rows.Count > 0)
                        {
                            if (this.QualityDataSet.Tables[0].Rows[this.QualityDataSet.Tables[0].Rows.Count - 1][1].ToString() == "�ϼƣ�")
                            {
                                this.QualityDataSet.Tables[0].Rows.RemoveAt(this.QualityDataSet.Tables[0].Rows.Count - 1);
                            }
                        }
                        DataRow row = this.QualityDataSet.Tables[0].NewRow();
                        row[1] = "�ϼƣ�";
                        row["ƴ����"] = "%";
                        row[6] = this.QualityDataSet.Tables[0].Compute("sum(���)", "");
                        this.QualityDataSet.Tables[0].Rows.Add(row);
                    }
                    if (level == 1)
                    {
                        if (QualityDataSet == null || QualityDataSet.Tables.Count <= 0)
                            return;
                        try
                        {
                            if (obj != null) qualityCode = obj as string;
                            DataView dv = new DataView(QualityDataSet.Tables[0]);
                            dv.RowFilter = "ҩƷ���� = " + "'" + qualityCode + "'";
                            this.sheetViewTot.DataSource = dv;
                            if (dv.Table.Rows.Count > 0)
                            {
                                if (dv.Table.Rows[dv.Table.Rows.Count - 1][1].ToString() == "�ϼƣ�")
                                {
                                    dv.Table.Rows.RemoveAt(dv.Table.Rows.Count - 1);
                                }
                            }

                            DataRow row = dv.Table.NewRow();
                            row[1] = "�ϼƣ�";
                            row["ƴ����"] = "%";
                            row["ҩƷ����"] = qualityCode;
                            row[6] = dv.Table.Compute("sum(���)", "ҩƷ���� = " + "'" + qualityCode + "'");
                            dv.Table.Rows.Add(row);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Language.Msg("����ҩƷ���ʳ���!" + ex.Message));
                            return;
                        }
                    }
                    this.SetQualityFormat();
                    #endregion
                }
                else
                {
                    #region ����
                    if (level == 0)
                    {
                        string[] strIndex = new string[1] { "Pharmacy.Item.GetApplyOutTot.ByMedDept" };
                        this.itemManager.ExecQuery(strIndex, ref ds, this.operVar.Dept.ID, this.dtpBegin.Value.ToString(), this.dtpEnd.Value.ToString());
                        if (ds != null && ds.Tables.Count > 0)
                            this.sheetViewTot.DataSource = ds;
                        else
                            return;

                        DataRow row = ds.Tables[0].NewRow();
                        row[0] = "�ϼƣ�";
                        row[1] = ds.Tables[0].Compute("sum(ȡҩ���)", "");
                        ds.Tables[0].Rows.Add(row);
                        this.SetMedDeptFormat();
                    }
                    if (level == 1)
                    {
                        string dept = obj as string;
                        DeptDataSet = new DataSet();
                        string[] strIndex = new string[1] { "Pharmacy.Item.GetApplyOutTot.ByDept" };
                        this.itemManager.ExecQuery(strIndex, ref DeptDataSet, this.operVar.Dept.ID, dept, this.dtpBegin.Value.ToString(), this.dtpEnd.Value.ToString());
                        if (DeptDataSet != null && DeptDataSet.Tables.Count > 0)
                            this.sheetViewTot.DataSource = DeptDataSet;

                        DataRow row = this.DeptDataSet.Tables[0].NewRow();
                        row[1] = "�ϼƣ�";
                        row["ƴ����"] = "%";
                        row[7] = this.DeptDataSet.Tables[0].Compute("sum(���)", "");
                        this.DeptDataSet.Tables[0].Rows.Add(row);
                        this.SetTotFormat();
                    }
                    #endregion
                }
                #endregion
            }
        }

        /// <summary>
        /// ��ȡδȷ�ϵ��˷������ҽ��������Ϣ
        /// </summary>
        public void QueryNoExamData()
        {
            //���ζ��˷�����Ĳ�ѯ���� {42E9350D-EDFD-43fa-9DF8-0AEEECDBB9EA}
            return;

            if (this.treeType == NodeType.Dept) return;
            if (this.nowObj == null) return;
            string inPatientNo = this.nowObj.ToString();
            DataSet ds = new DataSet();
            DataSet dsOutput = new DataSet();
            string[] strIndex = new string[1] { "Pharmacy.Item.GetFeeOrderAffirmInfo.ByPatient" };
            int parm = this.itemManager.ExecQuery(strIndex, ref ds, inPatientNo, this.dtpBegin.Value.ToString(), this.dtpEnd.Value.ToString());
            if (parm == -1)
            {
                MessageBox.Show(Language.Msg(this.itemManager.Err));
            }
            if (ds != null && ds.Tables.Count > 0)
            {
                this.SpreadQuitFee_Sheet1.DataSource = ds;
            }
            strIndex = new string[1] { "Pharmacy.Item.GetOutputAffirm.ByPatient" };
            parm = this.itemManager.ExecQuery(strIndex, ref dsOutput, inPatientNo, this.dtpBegin.Value.ToString(), this.dtpEnd.Value.ToString());
            if (parm == -1)
            {
                MessageBox.Show(Language.Msg(this.itemManager.Err));
            }
            if (ds != null && ds.Tables.Count > 0)
            {
                this.SpreadOut_Sheet1.DataSource = dsOutput;
                this.SetOutputFormat();
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Filter()
        {
            if (this.treeType == NodeType.Dept)
            {
                if (this.neuTabControl1.SelectedTab == this.tpDept && this.tvDept.SelectedNode == this.tvDept.Nodes[0])
                    return;
                DataView dv;
                if (this.neuTabControl1.SelectedTab == this.tpQuality)
                {
                    dv = new DataView(this.QualityDataSet.Tables[0]);
                }
                else
                {
                    dv = new DataView(this.DeptDataSet.Tables[0]);
                }
                dv.RowFilter = "(ƴ���� LIKE '" + this.txtFilter.Text + "%') OR " +
                    "(����� LIKE '" + this.txtFilter.Text + "%') OR " +
                    "(�Զ����� LIKE '" + this.txtFilter.Text + "%') OR " +
                    "(ҩƷ���� LIKE '" + this.txtFilter.Text + "%') ";
                this.sheetViewTot.DataSource = dv;
                this.SetFormat();
            }
            else
            {
                if (this.NurseDtaSet == null || this.NurseDtaSet.Tables.Count <= 0)
                    return;
                DataView dv;
                if (this.neuTabControl2.SelectedTab == this.tpDruged)
                {
                    dv = new DataView(this.NurseDtaSet.Tables[0]);
                    dv.RowFilter = "(ƴ���� LIKE '%" + this.txtFilter.Text + "%') OR " +
                        "(����� LIKE '%" + this.txtFilter.Text + "%') OR (ҩƷ���� LIKE '%" + this.txtFilter.Text + "%')";
                    this.sheetViewTot.DataSource = dv;
                    this.SetNurseFormat();
                }
            }


            this.SetFpColor();
        }

        /// <summary>
        /// ͳ�ƻ���
        /// </summary>
        public void Sum()
        {
            if (this.treeType == NodeType.Patient)	//�Ի���վ��ѯ�����л���ͳ��
                return;
            int iIndex = this.sheetViewTot.Rows.Count;
            if (iIndex == 0)
                return;
            int iSumIndex = 0;
            if (this.neuTabControl1.SelectedTab == this.tpDept)
            {
                if (this.tvDept.SelectedNode != null && this.tvDept.Nodes != null && this.tvDept.SelectedNode == this.tvDept.Nodes[0])
                    iSumIndex = 1;
                else
                    iSumIndex = 6;
            }
            else
            {
                iSumIndex = 4;
            }
            try
            {
                this.sheetViewTot.Rows.Add(iIndex, 1);
                this.sheetViewTot.Cells[iIndex, 1].Text = "�ϼƣ�";
                this.sheetViewTot.Cells[iIndex, iSumIndex].Formula = "SUM(" + (char)(65 + iSumIndex) + "1:" + (char)(65 + iSumIndex) + iIndex.ToString() + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show("����ͳ�Ƴ���!" + ex.Message);
                return;
            }
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = true;//p.ShowPageSetup();
            FS.HISFC.Models.Base.PageSize page = new FS.HISFC.Models.Base.PageSize();
            page.Height = 1060;
            page.Width = 800;
            page.Left = 100;
            page.Name = "Letter";
            p.SetPageSize(page);
            System.Windows.Forms.Panel panel = new Panel();
            panel.BackColor = System.Drawing.Color.White;
            FarPoint.Win.Spread.FpSpread fp = new FarPoint.Win.Spread.FpSpread();
            FarPoint.Win.Spread.SheetView fpView = new FarPoint.Win.Spread.SheetView();
            fp.Sheets.Add(fpView);
            fpView.Columns.Count = this.sheetViewTot.Columns.Count;
            for (int i = 0; i < this.sheetViewTot.Columns.Count; i++)
            {
                fpView.Columns[i].Visible = this.sheetViewTot.Columns[i].Visible;
                fpView.Columns[i].Width = this.sheetViewTot.Columns[i].Width;
                fpView.Columns[i].Label = this.sheetViewTot.Columns[i].Label;
                fpView.Columns[i].BackColor = this.sheetViewTot.Columns[i].BackColor;
            }
            for (int i = 0; i < this.sheetViewTot.Rows.Count; i++)
            {
                fpView.Rows[i].Visible = this.sheetViewTot.Rows[i].Visible;
                fpView.Rows[i].Height = this.sheetViewTot.Rows[i].Height;
                fpView.Rows[i].BackColor = this.sheetViewTot.Rows[i].BackColor;
                for (int j = 0; j < this.sheetViewTot.Columns.Count; j++)
                {
                    fpView.Cells[i, j].Value = this.sheetViewTot.Cells[i, j].Value;
                }
            }
            panel.Controls.Add(fp);
            fp.Dock = System.Windows.Forms.DockStyle.Fill;
            p.PrintPreview(100, 30, panel);
        }

        #region Fp��ʾ���� ����Ч��¼��ʾ��ɫ

        /// <summary>
        /// ����Fp��ʾ����
        /// </summary>
        private void SetFpColor()
        {
            if (this.sheetViewTot.Columns.Count > 16)
            {
                for (int i = 0; i < this.sheetViewTot.Rows.Count; i++)
                {
                    this.sheetViewTot.SetRowLabel(i, 0, " ");
                    if (this.sheetViewTot.Cells[i, 18].Text == "��Ч")
                    {
                        this.sheetViewTot.Rows[i].ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        this.sheetViewTot.Rows[i].ForeColor = System.Drawing.Color.Black;
                    }
                    //if (this.sheetViewTot.Cells[i, 17].Text != null &&
                    //    this.sheetViewTot.Cells[i, 17].Text != "")
                    //{
                    //    this.sheetViewTot.SetRowLabel(i, 0, "��");
                    //    this.sheetViewTot.RowHeader.Cells[i, 0].BackColor = System.Drawing.Color.White;
                    //}
                }
            }
            if (this.sheetViewDetail.Columns.Count > 15)
            {
                for (int i = 0; i < this.sheetViewDetail.Rows.Count; i++)
                {
                    this.sheetViewDetail.SetRowLabel(i, 0, " ");
                    if (this.sheetViewDetail.Cells[i, 16].Text == "��Ч")
                    {
                        this.sheetViewDetail.Rows[i].ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        this.sheetViewDetail.Rows[i].ForeColor = System.Drawing.Color.Black;
                    }

                    if (this.sheetViewDetail.Cells[i, 16].Text != null &&
                        this.sheetViewDetail.Cells[i, 16].Text != "")
                    {
                        this.sheetViewDetail.SetRowLabel(i, 0, "��");
                        this.sheetViewDetail.RowHeader.Cells[i, 0].BackColor = System.Drawing.Color.White;
                    }
                }
            }
        }

        #endregion

        #region FarPoint��ʽ��

        public void SetFormat()
        {
            if (this.treeType == NodeType.Patient)		//����վ��ѯ��
            {
                this.SetNurseFormat();
                return;
            }
            if (this.treeType == NodeType.Dept)
            {
                if (this.neuTabControl1.SelectedTab == this.tpDept)
                {
                    if (this.tvDept.SelectedNode == this.tvDept.Nodes[0])
                    {
                        this.SetMedDeptFormat();
                    }
                    else
                    {
                        this.SetTotFormat();
                    }
                }
                else
                {
                    this.SetQualityFormat();
                }
            }
        }
        /// <summary>
        /// ��ʽ��
        /// </summary>
        private void SetNurseFormat()
        {

            try
            {
                this.sheetViewTot.DefaultStyle.Locked = true;
                this.sheetViewTot.GrayAreaBackColor = System.Drawing.Color.Honeydew;
                this.sheetViewTot.SelectionBackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(225)), ((System.Byte)(243)));

                this.sheetViewTot.Columns.Get(0).Width = 0F;
                this.sheetViewTot.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.sheetViewTot.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                this.sheetViewTot.Columns.Get(1).Width = 50F;
                this.sheetViewTot.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                this.sheetViewTot.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.sheetViewTot.Columns.Get(2).Width = 160F;
                this.sheetViewTot.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.sheetViewTot.Columns.Get(3).Width = 100F;
                this.sheetViewTot.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.sheetViewTot.Columns.Get(4).Width = 0F;		//ÿ����
                this.sheetViewTot.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.sheetViewTot.Columns.Get(5).Width = 0F;		//��λ
                this.sheetViewTot.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.sheetViewTot.Columns.Get(6).Width = 45F;		//Ƶ��
                this.sheetViewTot.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.sheetViewTot.Columns.Get(7).Width = 60F;		//�÷�
                this.sheetViewTot.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.sheetViewTot.Columns.Get(8).Width = 40F;
                this.sheetViewTot.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.sheetViewTot.Columns.Get(9).Width = 40F;
                this.sheetViewTot.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.sheetViewTot.Columns.Get(10).Width = 80F;

                this.sheetViewTot.Columns.Get(11).Width = 100F;
                this.sheetViewTot.Columns.Get(12).Width = 80F;
                this.sheetViewTot.Columns.Get(13).Width = 70F;
                this.sheetViewTot.Columns.Get(14).Width = 120F;
                this.sheetViewTot.Columns.Get(15).Width = 60F;
                try
                {
                    this.sheetViewTot.Columns.Get(16).Width = 80F;
                }
                catch { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void SetTotFormat()
        {
            this.sheetViewTot.DefaultStyle.Locked = true;
            this.sheetViewTot.Columns.Get(0).Width = 0F;		//ҩƷ����
            this.sheetViewTot.Columns.Get(1).Width = 80F;			//��ҩ״̬
            this.sheetViewTot.Columns.Get(2).Width = 160F;			//����
            this.sheetViewTot.Columns.Get(3).Width = 100F;			//���
            this.sheetViewTot.Columns.Get(4).Width = 75F;			//���ۼ�
            this.sheetViewTot.Columns.Get(5).Width = 75F;			//����
            this.sheetViewTot.Columns.Get(6).Width = 50F;			//��λ
            this.sheetViewTot.Columns.Get(7).Width = 80F;			//���            
            this.sheetViewTot.Columns.Get(8).Width = 0F;			//ƴ����
            this.sheetViewTot.Columns.Get(9).Width = 0F;			//�����
            this.sheetViewTot.Columns.Get(10).Width = 0F;			//�Զ�����
        }
        private void SetDetailFormat()
        {
            this.sheetViewDetail.DefaultStyle.Locked = true;
            this.sheetViewDetail.Columns.Get(0).Width = 40F;		//����
            this.sheetViewDetail.Columns.Get(1).Width = 80F;		//����
            this.sheetViewDetail.Columns.Get(2).Width = 160F;		//ҩƷ����
            this.sheetViewDetail.Columns.Get(3).Width = 85F;		//���
            this.sheetViewDetail.Columns.Get(4).Width = 75F;		//���ۼ�
            this.sheetViewDetail.Columns.Get(5).Width = 75F;		//ÿ����
            this.sheetViewDetail.Columns.Get(6).Width = 50F;		//��λ
            this.sheetViewDetail.Columns.Get(7).Width = 50F;		//Ƶ��
            this.sheetViewDetail.Columns.Get(8).Width = 50F;		//�÷�
            this.sheetViewDetail.Columns.Get(9).Width = 75F;		//����
            this.sheetViewDetail.Columns.Get(10).Width = 50F;		//��λ  
            this.sheetViewDetail.Columns.Get(11).Width = 90F;		//��ҩ��
            this.sheetViewDetail.Columns.Get(12).Width = 80F;		//��ҩ��
            this.sheetViewDetail.Columns.Get(13).Width = 100F;		//��ҩʱ��
        }

        private void SetAffirmFormat()
        {
            this.SpreadQuitFee_Sheet1.DefaultStyle.Locked = true;
        }
        private void SetOutputFormat()
        {
            this.SpreadOut_Sheet1.DefaultStyle.Locked = true;
            this.SpreadOut_Sheet1.Columns[0].Width = 70F;	//����
            this.SpreadOut_Sheet1.Columns[1].Width = 70F;	//������
            this.SpreadOut_Sheet1.Columns[2].Width = 150F;	//ҩƷ����
            this.SpreadOut_Sheet1.Columns[3].Width = 80F;
            this.SpreadOut_Sheet1.Columns[4].Width = 55F;	//����
            this.SpreadOut_Sheet1.Columns[5].Width = 100F;	//ȡҩҩ��
            this.SpreadOut_Sheet1.Columns[6].Width = 80F;
            this.SpreadOut_Sheet1.Columns[7].Width = 70F;
            this.SpreadOut_Sheet1.Columns[8].Width = 80F;
        }
        private void SetMedDeptFormat()
        {
            this.sheetViewTot.DefaultStyle.Locked = true;
            this.sheetViewTot.Columns[0].Visible = true;
            this.sheetViewTot.Columns[0].Width = 150F;
            this.sheetViewTot.Columns[1].Width = 100F;
            this.sheetViewTot.Columns[2].Width = 100F;
        }
        private void SetQualityFormat()
        {
            this.sheetViewTot.DefaultStyle.Locked = true;
            this.sheetViewTot.Columns[0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetViewTot.Columns[0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetViewTot.Columns[1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.sheetViewTot.Columns[0].Visible = false;

            this.sheetViewTot.Columns[1].Visible = true;
            this.sheetViewTot.Columns[1].Width = 120F;          //���
            this.sheetViewTot.Columns[2].Width = 140F;          //ҩƷ����
            this.sheetViewTot.Columns[3].Width = 80F;           //���
            this.sheetViewTot.Columns[4].Width = 80F;           //��ҩ��
            this.sheetViewTot.Columns[5].Width = 60F;           //��λ
            this.sheetViewTot.Columns[6].Width = 80F;           //���

            this.sheetViewTot.Columns[7].Visible = false;       //ҩƷ����
            this.sheetViewTot.Columns[7].Width = 60F;
            this.sheetViewTot.Columns[8].Width = 0F;
            this.sheetViewTot.Columns[9].Width = 0F;
            this.sheetViewTot.Columns[10].Width = 0F;
        }

        #endregion

        #endregion

        private void ucSendDrugQuery_Load(object sender, EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower() != "devenv")
            {

                this.IsShowCheck = false;

                this.Init();

                this.ShowData();
                this.neuTabControl1.TabPages.Remove(this.tpQuality);
            }
        }

        private void rbSend_CheckedChanged(object sender, EventArgs e)
        {
            this.Query();
        }

        private void tvDept_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node.Parent == null)          //���ڵ�
                {
                    this.nowObj = null;
                    this.showLevel = 0;
                }
                else
                {
                    if (this.treeType == NodeType.Dept && this.neuTabControl1.SelectedTab == this.tpQuality)
                        this.nowObj = (e.Node.Tag as FS.FrameWork.Models.NeuObject).ID;
                    else
                        this.nowObj = e.Node.Tag;
                    this.showLevel = 1;
                }

                this.Query(this.nowObj, this.showLevel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("��ѯͳ��ִ�г���" + ex.Message));
                return;
            }
        }

        private void nlbFilter_TextChanged(object sender, EventArgs e)
        {
            this.Filter();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F2)
            {
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void ucQueryInpatientNo1_myEvent_1()
        {
            try
            {
                this.InPatientNo = this.ucQueryInpatientNo1.InpatientNo;
                this.Query(this.InPatientNo, 2);
                this.nowObj = this.InPatientNo;
                this.showLevel = 1;
                this.QueryNoExamData();
                this.ucQueryInpatientNo1.Text = this.InPatientNo.Substring(4);
            }
            catch
            {
                MessageBox.Show(Language.Msg("ͨ��סԺ�Ž���ȡҩ/ȷ����Ϣ��ѯ����!"));
                return;
            }
        }

        private void SpreadDrug_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.treeType == NodeType.Dept)//ֻ�������б���ʾΪ����ʱ����Ч
            {              
                //��ʾ��ϸ��Ϣʱ ����˫�����д���
                if (this.SpreadDrug.ActiveSheet == this.sheetViewDetail)
                {
                    return;
                }

                string drugCode = this.sheetViewTot.Cells[e.Row, 0].Text;
                string class3Meaning = this.sheetViewTot.Cells[e.Row, 1].Text;

                DataSet ds = new DataSet();
                if (this.neuTabControl1.SelectedTab == this.tpQuality)		//��ҩƷ����Tabҳ
                {
                    #region ҩƷ����Tabҳ��ʾ��ϸ

                    string[] strIndex = new string[1] { "Pharmacy.Item.GetAppplyOutTot.ByQuality.Drug" };
                    this.itemManager.ExecQuery(strIndex, ref ds, this.operVar.Dept.ID, drugCode, this.dtpBegin.Value.ToString(), this.dtpEnd.Value.ToString(),class3Meaning);
                    if (ds != null && ds.Tables.Count > 0)
                        this.sheetViewDetail.DataSource = ds;
                    if (!this.SpreadDrug.Sheets.Contains(this.sheetViewDetail))
                        this.SpreadDrug.Sheets.Add(this.sheetViewDetail);
                    this.SpreadDrug.ActiveSheet = this.sheetViewDetail;

                    #region ��ʽ��
                    try
                    {
                        this.sheetViewDetail.DefaultStyle.Locked = true;
                        this.sheetViewDetail.Columns[0].Width = 100F;       //���
                        this.sheetViewDetail.Columns[1].Width = 100F;       //ȡҩ����
                        this.sheetViewDetail.Columns[2].Width = 160F;		//ҩƷ����
                        this.sheetViewDetail.Columns[3].Width = 80F;		//���
                        this.sheetViewDetail.Columns[4].Width = 80F;		//��ҩ��
                        this.sheetViewDetail.Columns[5].Width = 50F;		//��λ
                    }
                    catch { }
                    #endregion

                    #endregion
                }
                else															//��ȡҩ����Tabҳ
                {
                    #region ȡҩ����Tabҳ��ʾ��ϸ

                    if (this.tvDept.SelectedNode != null && this.tvDept.Nodes != null && this.tvDept.SelectedNode == this.tvDept.Nodes[0])
                        return;                    

                    string[] strIndex = new string[1] { "Pharmacy.Item.GetApplyOutTot.ByMedDept.Drug" };

                    if (this.nowObj != null)
                    {
                        this.itemManager.ExecQuery(strIndex, ref ds, this.operVar.Dept.ID, this.nowObj as string,drugCode, this.dtpBegin.Value.ToString(), this.dtpEnd.Value.ToString(),class3Meaning);
                    }

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        this.sheetViewDetail.DataSource = ds;
                    }

                    if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    {
                        return;
                    }

                    if (!this.SpreadDrug.Sheets.Contains(this.sheetViewDetail))
                        this.SpreadDrug.Sheets.Add(this.sheetViewDetail);
                    this.SpreadDrug.ActiveSheet = this.sheetViewDetail;
                    this.SetDetailFormat();

                    #endregion
                }

                this.SetFpColor();
            }
        }

        private void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedTab == this.tpQuality)
            {
                if (this.tvDrugType1.Nodes.Count > 0)
                {
                    this.tvDrugType1.SelectedNode = this.tvDrugType1.Nodes[0];
                    //���ζ��Ѱڡ�δ�ڵ���ʾ ����ʾ����ʹ��
                    //this.IsShowCheck = true;
                }
            }
            else
            {
                if (this.tvDept.Nodes.Count > 0)
                {
                    this.tvDept.SelectedNode = this.tvDept.Nodes[0];
                    this.IsShowCheck = false;
                }
            }
        }

        private void neuTabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.neuTabControl2.SelectedTab == this.tpQuitFee || this.neuTabControl2.SelectedTab == this.tpOutBill)
            {
                this.QueryNoExamData();
            }
        }

        private void neuTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.Filter();
        }
        //��ӡ
        //protected override void OnPrint(PaintEventArgs e)
        //{
        //    FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
        //    print.PrintPage(0,0,this.neuTabControl2.SelectedTab);
        //    base.OnPrint(e);
        //}
        //��ӡ��ť
        protected override int OnPrintPreview(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.PrintPreview(0,0,this.neuTabControl2.SelectedTab);
            return base.OnPrintPreview(sender, neuObject);
        }

        private void cmbDrugDept_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
 
       
    }
}
