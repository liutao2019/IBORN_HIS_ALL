using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Order;

namespace FS.HISFC.Components.Order.Subtbl
{
    /// <summary>
    /// ���ϸ����㷨ά��
    /// </summary>
    public partial class ucSubtblManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucSubtblManager()
        {
            InitializeComponent();
        }

        #region ϵͳ�������
        FS.HISFC.BizLogic.Order.SubtblManager subtblMgr = new FS.HISFC.BizLogic.Order.SubtblManager();

        #endregion

        #region ����

        ArrayList al = new ArrayList();

        /// <summary>
        /// ��ҩƷ��Ŀ
        /// </summary>
        private DataTable dtUndrug = null;

        /// <summary>
        /// �Ƿ�����༭�޸� 0 ����������Ա����);1 ȫ������2 ���ݵ�½�����жϣ�Ŀǰֻ�е�½סԺ���Ҳ���ά����
        /// </summary>
        private int isAllowEdit = 1;

        /// <summary>
        /// �Ƿ�����༭�޸� 0 ����������Ա����);1 ȫ������2 ���ݵ�½�����жϣ�Ŀǰֻ�е�½סԺ���Ҳ���ά����
        /// </summary>
        [Category("����"), Description("�Ƿ�����༭�޸� 0 ����������Ա����);1 ȫ������2 ���ݵ�½�����жϣ�������Ӧ��ҽ��վ�����ԣ�")]
        public int IsAllowEdit
        {
            get
            {
                return isAllowEdit;
            }
            set
            {
                isAllowEdit = value;
            }
        }

        /// <summary>
        /// ҩƷ�б�
        /// </summary>
        private DataTable dtDrug = null;

        FS.FrameWork.WinForms.Forms.ToolBarService toolbar = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 
        /// </summary>
        ArrayList alItem = new ArrayList();

        /// <summary>
        /// ��ǰά�����÷�����Ŀ
        /// </summary>
        private FS.FrameWork.Models.NeuObject currentUsage = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �����б�
        /// </summary>
        //ArrayList alDept = new ArrayList();

        /// <summary>
        /// 
        /// </summary>
        IList<string> IDepartmentList = new List<string>();
        /// <summary>
        /// ���Ұ�����
        /// </summary>
        FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��ҩƷ��Ŀ�б�
        /// </summary>
        ArrayList alItems = new ArrayList();

        FS.FrameWork.Public.ObjectHelper itemHelper = new FS.FrameWork.Public.ObjectHelper();

        private FS.HISFC.Models.Base.Employee oper = (FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator;

        /// <summary>
        /// ����˵���ĵ�
        /// </summary>
        private string explainNote = "���Ĵ���ά������˵��.doc";

        /// <summary>
        /// ����˵���ĵ�
        /// </summary>
        [Category("����"), Description("���Ĵ���ά������˵����Ӧ���ĵ����ƣ�Ĭ�ϡ����Ĵ���ά������˵��.doc��")]
        public string ExplainNote
        {
            get
            {
                return explainNote;
            }
            set
            {
                explainNote = value;
            }
        }

        /// <summary>
        /// �Ƿ����п��Ҹ��Ķ���ʾ
        /// </summary>
        private bool isAllDeptShow = true;

        /// <summary>
        /// �Ƿ����п��Ҹ��Ķ���ʾ
        /// </summary>
        [Category("����"), Description("�Ƿ����п��Ҹ��Ķ���ʾ")]
        public bool IsAllDeptShow
        {
            get
            {
                return isAllDeptShow;
            }
            set
            {
                isAllDeptShow = value;
            }
        }

        /// <summary>
        /// ��ʾ����ά�������ͣ����סԺ��
        /// </summary>
        private EnumType showType = EnumType.ȫ��;

        /// <summary>
        /// ��ʾ����ά�������ͣ����סԺ��
        /// </summary>
        [Category("����"), Description("��ʾ����ά�������ͣ����סԺ��")]
        public EnumType ShowType
        {
            get
            {
                return showType;
            }
            set
            {
                showType = value;
            }
        }

        /// <summary>
        /// ���ﷶΧĬ�Ͽ���ȫ��
        /// </summary>
        private bool clinicAll = false;

        /// <summary>
        /// ��ʾ����ά�������ͣ����סԺ��
        /// </summary>
        [Category("����"), Description("���ﷶΧ���Ŀ����Ƿ�Ĭ��Ϊȫ�� false��true��")]
        public bool ClinicAll
        {
            get
            {
                return clinicAll;
            }
            set
            {
                clinicAll = value;
            }
        }

        /// <summary>
        /// ����˵��
        /// </summary>
        private string useExplain = "";

        /// <summary>
        /// ����˵�� ���ڽ�����ʾ
        /// </summary>
        [Category("����"), Description("����˵�� ���ڽ�����ʾ")]
        public string UseExplain
        {
            get
            {
                return useExplain;
            }
            set
            {
                useExplain = value;
            }
        }

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ����������
        /// </summary>
        private void Init()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ����������Ժ�....");
            Application.DoEvents();
            this.InitTree();
            this.ucInputItem1.Init();
            this.InitItemFp();

            ArrayList alDept = new ArrayList();
            //alDept = CacheManager.InterMgr.GetDepartment();
            alDept = SOC.HISFC.BizProcess.Cache.Common.GetDept();
            if (alDept == null)
            {
                MessageBox.Show(CacheManager.InterMgr.Err);
            }
            else
            {
                FS.HISFC.Models.Base.Const allObj = new FS.HISFC.Models.Base.Const();
                allObj.Name = "ȫ��";
                allObj.ID = "ROOT";
                allObj.UserCode = "QB";
                this.deptHelper.ArrayObject.Add(allObj);
                deptHelper.ArrayObject.AddRange(alDept);
                //alDept.Add(allObj);
                //this.deptHelper.ArrayObject = alDept;
            }

            alItems = new ArrayList(CacheManager.FeeIntegrate.QueryAllItemsList());
            if (alItems == null)
            {
                MessageBox.Show(CacheManager.FeeIntegrate.Err);
            }
            else
            {
                this.itemHelper.ArrayObject = alItems;
            }

            #region �����б�
            FarPoint.Win.Spread.CellType.ComboBoxCellType comCellType1 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();

            //���÷�Χ��0 ���1 סԺ��2 ȫ��
            string[] arrayTemp = new string[3] { "����", "סԺ", "ȫ��" };
            comCellType1.Items = arrayTemp;
            this.fpSpread1_Sheet1.Columns[(int)SubtblColumns.Area].CellType = comCellType1;

            //ҽ�����0 ȫ����1 ������2 ����
            arrayTemp = new string[3] { "ȫ��", "����", "����" };
            FarPoint.Win.Spread.CellType.ComboBoxCellType comCellType5 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            comCellType5.Items = arrayTemp;
            this.fpSpread1_Sheet1.Columns[(int)SubtblColumns.OrderType].CellType = comCellType5;

            //�鷶Χ��0 ÿ����ȡ��1 ��һ����ȡ��2 �ڶ��������
            arrayTemp = new string[3] { "ÿ����ȡ", "��һ����ȡ", "�ڶ��������" };
            FarPoint.Win.Spread.CellType.ComboBoxCellType comCellType2 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            comCellType2.Items = arrayTemp;
            this.fpSpread1_Sheet1.Columns[(int)SubtblColumns.CombArea].CellType = comCellType2;

            //�շѹ��򣺹̶�������*���Ժע��*����Ʒ������*���ҽ��������*Ƶ��
            arrayTemp = new string[8] { "���̶�����", "�����Ժע����", "������Ʒ����", "��Ӧִ�д���", "��Ƶ����", "��ҽ������", "������", "��Ժע����" };
            FarPoint.Win.Spread.CellType.ComboBoxCellType comCellType3 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            comCellType3.Items = arrayTemp;
            this.fpSpread1_Sheet1.Columns[(int)SubtblColumns.FeeRule].CellType = comCellType3;

            //�������0 ������ 1 Ӥ��ʹ�� 2 ��Ӥ��ʹ��
            arrayTemp = new string[3] { "������", "��ͯ", "����" };
            FarPoint.Win.Spread.CellType.ComboBoxCellType comCellType4 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            comCellType4.Items = arrayTemp;
            this.fpSpread1_Sheet1.Columns[(int)SubtblColumns.LimitType].CellType = comCellType4;

            #endregion

            #region �����б�

            #endregion

            this.lblUseExplain.Text = this.useExplain;
            this.pnDesign.Visible = true;
            if (string.IsNullOrEmpty(lblUseExplain.Text))
            {
                this.pnDesign.Visible = false;
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            #region ��ʼ�������б�

            ArrayList al = CacheManager.InterMgr.QueryDepartment(oper.Nurse.ID);
            if (al == null || al.Count == 0)
            {
                this.neuListBox1.Items.Add(oper.Dept);
                this.IDepartmentList.Add(oper.Dept.ID);
            }
            else
            {
                for (int i = 0; i < al.Count; i++)
                {
                    try
                    {
                        FS.FrameWork.Models.NeuObject o = al[i] as FS.FrameWork.Models.NeuObject;
                        this.neuListBox1.Items.Add(o);
                        this.IDepartmentList.Add(o.ID);
                    }
                    catch { }
                }
            }

            if (this.neuListBox1.Items.Count > 0)
            {
                this.neuListBox1.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("û��ά����ؿ��һ���վ��ά�������ʹ�øù���");
                return;
            }
            FS.FrameWork.Models.NeuObject objdept = this.neuListBox1.SelectedItem as FS.FrameWork.Models.NeuObject;

            #endregion
        }

        void fpSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == (int)SubtblColumns.Area)
            {
                if (this.fpSpread1_Sheet1.Cells[e.Row, (int)SubtblColumns.Area].Text == "����")
                {
                    this.fpSpread1_Sheet1.Cells[e.Row, (int)SubtblColumns.OrderType].Text = "ȫ��";
                    this.fpSpread1_Sheet1.Cells[e.Row, (int)SubtblColumns.OrderType].Locked = true;
                }
                else if (this.fpSpread1_Sheet1.Cells[e.Row, (int)SubtblColumns.Area].Text == "ȫ��")
                {
                    this.fpSpread1_Sheet1.Cells[e.Row, (int)SubtblColumns.OrderType].Text = "ȫ��";
                    this.fpSpread1_Sheet1.Cells[e.Row, (int)SubtblColumns.OrderType].Locked = true;
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[e.Row, (int)SubtblColumns.OrderType].Locked = false;
                }
            }
        }

        /// <summary>
        /// ��ʼ��������Ŀ�б�
        /// </summary>
        private void InitItemFp()
        {
            #region ��ҩƷ
            ArrayList alUndrug = new ArrayList(CacheManager.FeeIntegrate.QueryValidItems());
            if (alUndrug == null)
            {
                MessageBox.Show(CacheManager.FeeIntegrate.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //��������
            System.Type dtStr = System.Type.GetType("System.String");

            this.dtUndrug = new DataTable();

            //��myDataTable�ж�����
            this.dtUndrug.Columns.AddRange(new DataColumn[] {													
                                                    new DataColumn("����",      dtStr),
                                                    new DataColumn("����",      dtStr),
                                                    new DataColumn("���",     dtStr),
													new DataColumn("���",     dtStr),
													new DataColumn("����",   dtStr),
													new DataColumn("��λ",   dtStr),
													new DataColumn("ƴ����",   dtStr),
													new DataColumn("�����",   dtStr),
													new DataColumn("��Ŀ����",   dtStr)
											        });

            this.dtUndrug.PrimaryKey = new DataColumn[] { this.dtUndrug.Columns["��Ŀ����"] };

            DataRow dRow;

            foreach (FS.HISFC.Models.Fee.Item.Undrug undrugObj in alUndrug)
            {
                dRow = dtUndrug.NewRow();
                dRow["����"] = undrugObj.Name;
                dRow["����"] = undrugObj.UserCode;
                dRow["���"] = undrugObj.Specs;
                dRow["���"] = undrugObj.SysClass.Name;
                dRow["����"] = undrugObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ";
                dRow["��λ"] = undrugObj.PriceUnit;
                dRow["ƴ����"] = undrugObj.SpellCode;
                dRow["�����"] = undrugObj.WBCode;
                dRow["��Ŀ����"] = undrugObj.ID;
                dtUndrug.Rows.Add(dRow);
            }

            this.dtUndrug.DefaultView.Sort = "����";
            this.fpUndrug_Sheet1.DataSource = this.dtUndrug.DefaultView;

            this.fpUndrug_Sheet1.Columns[6].Visible = false;
            this.fpUndrug_Sheet1.Columns[7].Visible = false;
            this.fpUndrug_Sheet1.Columns[8].Visible = false;
            #endregion

            #region ҩƷ�б�

            ArrayList alDrug = new ArrayList(CacheManager.PhaIntegrate.QueryItemList(true));
            if (alDrug == null)
            {
                MessageBox.Show(CacheManager.PhaIntegrate.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.dtDrug = new DataTable();

            //��myDataTable�ж�����
            this.dtDrug.Columns.AddRange(new DataColumn[] {													
                                                    new DataColumn("����",      dtStr),
                                                    new DataColumn("����",      dtStr),
                                                    new DataColumn("���",     dtStr),
													new DataColumn("���",     dtStr),
													new DataColumn("����",   dtStr),
													new DataColumn("��λ",   dtStr),
													new DataColumn("ƴ����",   dtStr),
													new DataColumn("�����",   dtStr),
													new DataColumn("��Ŀ����",   dtStr)
											        });

            this.dtDrug.PrimaryKey = new DataColumn[] { this.dtDrug.Columns["��Ŀ����"] };


            foreach (FS.HISFC.Models.Pharmacy.Item drugObj in alDrug)
            {
                dRow = dtDrug.NewRow();
                dRow["����"] = drugObj.Name;
                dRow["����"] = drugObj.UserCode;
                dRow["���"] = drugObj.Specs;
                dRow["���"] = drugObj.SysClass.Name;
                dRow["����"] = drugObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ";
                dRow["��λ"] = drugObj.PriceUnit;
                dRow["ƴ����"] = drugObj.SpellCode;
                dRow["�����"] = drugObj.WBCode;
                dRow["��Ŀ����"] = drugObj.ID;
                dtDrug.Rows.Add(dRow);
            }

            this.dtDrug.DefaultView.Sort = "����";
            this.fpDrug_Sheet1.DataSource = this.dtDrug.DefaultView;

            this.fpDrug_Sheet1.Columns[6].Visible = false;
            this.fpDrug_Sheet1.Columns[7].Visible = false;
            this.fpDrug_Sheet1.Columns[8].Visible = false;

            #endregion
        }

        /// <summary>
        /// ��ʼ���÷�TreeView
        /// </summary>
        private void InitTree()
        {
            this.tvUsage.Nodes.Clear();
            TreeNode root = new TreeNode("�÷�");
            root.ImageIndex = 40;
            this.tvUsage.Nodes.Add(root);
            //����÷��б�
            if (al != null)
            {
                al = CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.USAGE);
            }

            if (al != null)
            {
                foreach (FS.FrameWork.Models.NeuObject obj in al)
                {
                    TreeNode node = new TreeNode(obj.Name);
                    node.Tag = obj;
                    node.ImageIndex = 41;
                    root.Nodes.Add(node);
                }
                root.Expand();
            }
        }

        #endregion

        #region �¼�

        /// <summary>
        /// ��ʼ��ToolBar
        /// </summary>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolbar.AddToolButton("ɾ��", "ɾ������", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            this.toolbar.AddToolButton("����˵��", "�鿴����˵��", FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ, true, false, null);
            return this.toolbar;
        }

        private void ucSubtblManager_Load(object sender, EventArgs e)
        {
            try
            {
                this.Init();

                this.neuListBox1.SelectedIndexChanged += new EventHandler(neuListBox1_SelectedIndexChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveData();
            return 1;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ɾ��")
            {
                if (this.fpSpread1_Sheet1.Rows.Count <= 0)
                    return;

                int row = this.fpSpread1_Sheet1.ActiveRowIndex;
                if (row < 0)
                    return;
                if (Delete(row) != -1)
                {
                    this.fpSpread1_Sheet1.Rows.Remove(row, 1);
                }
            }
            else if (e.ClickedItem.Text == "����˵��")
            {
                try
                {
                    System.Diagnostics.Process.Start(Application.StartupPath + "\\" + explainNote);
                }
                catch
                {
                    MessageBox.Show("�Ҳ���˵���ĵ�������ϵ��Ϣ�ƣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        private int Delete(int row)
        {
            if (fpSpread1_Sheet1.Rows[row].Locked)
            {
                MessageBox.Show("��û��ɾ��������Ŀ��Ȩ�ޣ�\r\n������������ϵ��Ϣ�ƣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return -1;
            }

            DialogResult Result = MessageBox.Show("ȷ��ɾ�������ݣ�", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (Result != DialogResult.OK)
            {
                return -1;
            }

            OrderSubtblNew objSubtbl = new OrderSubtblNew();

            //���÷�Χ��0 ���1 סԺ��3 ȫ��
            objSubtbl.Area = this.GetSelectData(row, (int)SubtblColumns.Area);


            //ҽ�����ȫ��������������
            objSubtbl.OrderType = this.GetSelectData(row, (int)SubtblColumns.OrderType);

            //�÷����࣬0 ҩƷ���÷���1 ��ҩƷ����Ŀ����
            objSubtbl.TypeCode = ((FS.FrameWork.Models.NeuObject)this.fpSpread1_Sheet1.Tag).ID;
            //���Ҵ��룬ȫԺͳһ����'ROOT'
            objSubtbl.Dept_code = this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Tag.ToString();
            //��Ŀ����
            objSubtbl.Item.ID = (this.fpSpread1_Sheet1.Rows[row].Tag as FS.FrameWork.Models.NeuObject).ID;
            //����
            objSubtbl.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Qty].Text);
            //�鷶Χ��0 ÿ����ȡ��1 ��һ����ȡ��2 �ڶ��������
            objSubtbl.CombArea = this.GetSelectData(row, (int)SubtblColumns.CombArea);
            //�շѹ��򣺹̶�������*���Ժע��*����Ʒ������*���ҽ��������*Ƶ�Ρ�*����ҽ��������*������*Ժע������Ժע����/Ƶ�� ��ȡ����
            objSubtbl.FeeRule = this.GetSelectData(row, (int)SubtblColumns.FeeRule);
            //�������0 ������ 1 Ӥ��ʹ�� 2 ��Ӥ��ʹ��
            objSubtbl.LimitType = this.GetSelectData(row, (int)SubtblColumns.LimitType);
            //�״���ȡ��Ŀ
            objSubtbl.FirstFeeFlag = FS.FrameWork.Function.NConvert.ToInt32(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.FirstFeeFlag].Text).ToString();
            //����Ա					
            objSubtbl.Oper.ID = (CacheManager.OutOrderMgr.Operator as FS.HISFC.Models.Base.Employee).ID;
            //����ʱ��
            objSubtbl.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OperTime].Text);

            //�Ƿ��ظ���ȡ
            objSubtbl.IsAllowReFee = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.IsAllowReFee].Text);
            //�Ƿ񵯳�ѡ��
            objSubtbl.IsAllowPopChose = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.IsAllowPopChose].Text);
            //�Ƿ�ÿ��������
            objSubtbl.IsCalculateByOnceDose = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.IsCalculateByOnceDose].Text);
            //ÿ������λ
            objSubtbl.DoseUnit = this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.DoseUnit].Text;
            //ÿ������ʼֵ
            objSubtbl.OnceDoseFrom = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OnceDoseFrom].Text);
            //ÿ��������ֵ
            objSubtbl.OnceDoseTo = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OnceDoseTo].Text);

            //��չ1
            objSubtbl.Extend1 = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Extend1].Text) ? "1" : "0";
            //��չ2
            objSubtbl.Extend2 = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Extend2].Text) ? "1" : "0";
            //��չ3
            objSubtbl.Extend3 = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Extend3].Text) ? "1" : "0";

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //��ɾ�� �����
            if (this.subtblMgr.DelSubtblInfo(objSubtbl) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.fpSpread1.ShowRow(0, row, FarPoint.Win.Spread.VerticalPosition.Center);
                MessageBox.Show(this.subtblMgr.Err, "��ʾ");
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

        private void tvPatientList1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode current = this.tvUsage.SelectedNode;

            if (current == null || current.Parent == null)
            {
                if (this.fpSpread1_Sheet1.RowCount > 0)
                    this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);

                this.fpSpread1_Sheet1.Tag = null;
            }
            else
            {
                FS.FrameWork.Models.NeuObject usage = current.Tag as FS.FrameWork.Models.NeuObject;

                this.Refresh(usage);
            }
        }

        private void ucInputItem1_SelectedItem(FS.FrameWork.Models.NeuObject sender)
        {
            if (this.ucInputItem1.FeeItem == null)
            {
                return;
            }

            if (this.fpSpread1_Sheet1.Tag == null)
            {
                MessageBox.Show("����ѡ���÷�!", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            //if (!oper.IsManager)
            //{
            //    if (isAllowEdit == 0 ||
            //        (isAllowEdit == 2 &&
            //        SOC.HISFC.BizProcess.Cache.Common.GetDept(oper.Dept.ID).DeptType.ID.ToString() != "I"))
            //    {
            //        MessageBox.Show("û��ά����Ȩ�ޣ�");
            //        return;
            //    }
            //}

            //if (isCheckRepeatItem)
            //{
            //    for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)//����Ƿ��ظ�
            //    {
            //        FS.FrameWork.Models.NeuObject obj = this.fpSpread1.ActiveSheet.Rows[i].Tag as FS.FrameWork.Models.NeuObject;
            //        if (obj.Memo == this.ucInputItem1.FeeItem.ID)
            //        {
            //            MessageBox.Show("�Ѵ�����Ŀ" + this.ucInputItem1.FeeItem.Name + "������ѡ��");
            //            return;//����ظ� ����
            //        }
            //    }
            //}

            this.AddItemToFp(this.ucInputItem1.FeeItem, 0);
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.fpSpread1.ContainsFocus)
            {
                if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)SubtblColumns.Dept)
                {
                    this.PopItem(this.deptHelper.ArrayObject, (int)SubtblColumns.Dept);
                }
                else if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)SubtblColumns.Item)
                {
                    this.PopItem(this.alItems, (int)SubtblColumns.Item);
                }
            }
        }

        public override int Export(object sender, object neuObject)
        {
            ExportToExcel();
            return base.Export(sender, neuObject);
        }

        #endregion

        #region ����

        private FS.FrameWork.Models.NeuObject GetDept()
        {
            if (neuListBox1.SelectedItem == null)
            {
                return oper.Dept;
            }
            return this.neuListBox1.SelectedItem as FS.FrameWork.Models.NeuObject;
        }

        /// <summary>
        /// �����Ŀ��farpoint
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="row"></param>
        private void AddItemToFp(FS.FrameWork.Models.NeuObject Item, int row)
        {
            if (this.fpSpread1_Sheet1.Tag == null)
            {
                MessageBox.Show("����ѡ���÷�!", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            FS.HISFC.Models.Base.Item myItem = null;

            if (Item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug) ||
                Item.GetType() == typeof(FS.HISFC.Models.Base.Item))
            {
                if (itemHelper != null && itemHelper.ArrayObject.Count > 0)
                {
                    myItem = itemHelper.GetObjectFromID(Item.ID) as FS.HISFC.Models.Base.Item;
                }
                if (myItem == null)
                {
                    myItem = CacheManager.FeeIntegrate.GetItem(Item.ID);
                    if (myItem == null)
                    {
                        MessageBox.Show(CacheManager.FeeIntegrate.Err);
                        return;
                    }
                }

                if (!myItem.IsValid)
                {
                    MessageBox.Show(myItem.Name + "�Ѿ�ͣ�ã�������ѡ��");
                    return;
                }

                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                //�жϸ���Ŀ�Ƿ����
                if (!myItem.IsValid)
                {
                    MessageBox.Show("��Ŀ" + myItem.Name + "�Ѿ�ͣ�ã�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //��fpSpread1_Sheet1�м�������
                obj.ID = myItem.ID;
                obj.Name = myItem.Name;
                this.fpSpread1.ActiveSheet.Rows.Add(row, 1);

                this.fpSpread1_Sheet1.Rows[row].Tag = myItem;

                //���÷�Χ��0 ���1 סԺ��3 ȫ��
                if (showType == EnumType.ȫ��
                    || oper.IsManager)
                {
                    this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Area].Text = "ȫ��";
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Area].Text = showType.ToString();
                }

                //ҽ�����ȫ��������������
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OrderType].Text = "ȫ��";

                if (!oper.IsManager)
                {
                    if (ClinicAll && showType == EnumType.����)
                    {
                        //���Ҵ��룬ȫԺͳһ����'ROOT'
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Text = "ȫ��";
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Tag = "ROOT";
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Text = this.GetDept().Name;
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Tag = GetDept().ID;
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Locked = true;
                    }
                }
                else
                {
                    //���Ҵ��룬ȫԺͳһ����'ROOT'
                    this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Text = "ȫ��";
                    this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Tag = "ROOT";
                }

                //������Ŀ����
                if (!string.IsNullOrEmpty(myItem.Specs))
                {
                    this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Item].Text = "��" + myItem.Specs + "��" + myItem.Name + "��" + myItem.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ��";
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Item].Text = myItem.Name + "��" + myItem.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ��";
                }
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Item].Tag = myItem.ID;
                //����
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Qty].Text = "1";
                //�鷶Χ��0 ÿ����ȡ��1 ��һ����ȡ��2 �ڶ��������
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.CombArea].Text = "ÿ����ȡ";
                //�շѹ��򣺹̶�������*���Ժע��*����Ʒ������*���ҽ��������*Ƶ�Ρ�*����ҽ��������*������*Ժע������Ժע����/Ƶ�� ��ȡ����
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.FeeRule].Text = "���̶�����";
                //�������0 ������ 1 Ӥ��ʹ�� 2 ��Ӥ��ʹ��
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.LimitType].Text = "������";
                //�״���ȡ��Ŀ
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.FirstFeeFlag].Value = FS.FrameWork.Function.NConvert.ToInt32(0);
                //����Ա
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Oper].Text = (CacheManager.OutOrderMgr.Operator as FS.HISFC.Models.Base.Employee).Name;
                //����ʱ��
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OperTime].Text = CacheManager.OutOrderMgr.GetSysDateTime().ToString();

                //�Ƿ��ظ���ȡ
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.IsAllowReFee].Value = true;
                //�Ƿ񵯳�ѡ��
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.IsAllowPopChose].Value = false;
                //�Ƿ�ÿ��������
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.IsCalculateByOnceDose].Value = false;
                //ÿ������λ
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.DoseUnit].Text = "";
                //ÿ������ʼֵ
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OnceDoseFrom].Text = "";
                //ÿ��������ֵ
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OnceDoseTo].Text = "";


                //��չ1
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Extend1].Value = false;
                //��չ2
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Extend2].Value = false;
                //��չ3
                this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Extend3].Value = false;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.fpSpread1.ContainsFocus)
            {
                if (keyData == Keys.Space)
                {
                    if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)SubtblColumns.Dept)
                    {
                        this.PopItem(this.deptHelper.ArrayObject, (int)SubtblColumns.Dept);
                    }
                    else if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)SubtblColumns.Item)
                    {
                        this.PopItem(this.alItems, (int)SubtblColumns.Item);
                    }
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// ��������ѡ��
        /// </summary>
        private void PopItem(ArrayList al, int index)
        {
            if (fpSpread1_Sheet1.Rows[fpSpread1_Sheet1.ActiveRowIndex].Locked)
            {
                return;
            }

            if (fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, (int)SubtblColumns.Dept].Locked
                && index == (int)SubtblColumns.Dept)
            {
                return;
            }
            if (fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, (int)SubtblColumns.Item].Locked
                && index == (int)SubtblColumns.Item)
            {
                return;
            }

            FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(al, ref info) == 0)
            {
                return;
            }
            else
            {
                //����
                if (index == (int)SubtblColumns.Dept)
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)SubtblColumns.Dept].Tag = info.ID;
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)SubtblColumns.Dept].Value = info.Name;
                }
                //��Ŀ
                else if (index == (int)SubtblColumns.Item)
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)SubtblColumns.Item].Tag = info.ID;
                    FS.HISFC.Models.Base.Item itemObj = info as FS.HISFC.Models.Base.Item;

                    //������Ŀ����
                    if (!string.IsNullOrEmpty(itemObj.Specs))
                    {
                        this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)SubtblColumns.Item].Value = "��" + itemObj.Specs + "��" + itemObj.Name + "��" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ��";
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)SubtblColumns.Item].Value = itemObj.Name + "��" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ��";
                    }
                    this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.ActiveRowIndex].Tag = info;
                }
            }
        }

        /// <summary>
        /// ˢ���б�
        /// </summary>
        /// <param name="person"></param>
        private void Refresh(FS.FrameWork.Models.NeuObject usage)
        {
            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ�����Ժ󡭡�");
            Application.DoEvents();

            try
            {
                //this.tabPage1.Text = usage.Name;
                this.lblDisplay.Text = usage.Name;
                this.currentUsage = usage;

                FS.HISFC.Models.Base.Item itemObj = new FS.HISFC.Models.Base.Item();

                alItem.Clear();
                alItem = this.subtblMgr.GetSubtblInfo("2", usage.ID, "ROOT");
                if (alItem == null)
                {
                    MessageBox.Show(this.subtblMgr.Err);
                    return;
                }

                this.fpSpread1_Sheet1.Tag = usage;
                if (alItem != null && alItem.Count > 0)
                {
                    foreach (OrderSubtblNew obj in alItem)
                    {
                        itemObj = this.itemHelper.GetObjectFromID(obj.Item.ID) as FS.HISFC.Models.Base.Item;
                        if (itemObj == null)
                        {
                            MessageBox.Show("������Ŀʧ�ܣ�" + obj.Item.Name + obj.Item.ID);
                            break;
                        }

                        this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);
                        int row = this.fpSpread1_Sheet1.RowCount - 1;
                        this.fpSpread1_Sheet1.Rows[row].Tag = obj.Item;

                        //��Χ
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Area].Text = (this.fpSpread1_Sheet1.Columns[(int)SubtblColumns.Area].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[FS.FrameWork.Function.NConvert.ToInt32(obj.Area)];
                        //ҽ�����ȫ��������������
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OrderType].Text = (this.fpSpread1_Sheet1.Columns[(int)SubtblColumns.OrderType].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[FS.FrameWork.Function.NConvert.ToInt32(obj.OrderType)];
                        //����
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Text = this.deptHelper.GetName(obj.Dept_code);
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Tag = obj.Dept_code;

                        if (SOC.HISFC.BizProcess.Cache.Common.GetDept(obj.Dept_code) != null
                            && SOC.HISFC.BizProcess.Cache.Common.GetDept(obj.Dept_code).ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
                        {
                            fpSpread1_Sheet1.Rows[row].BackColor = Color.OrangeRed;
                        }

                        //��Ŀ
                        //this.fpSpread1_Sheet1.Cells[row, 2].Text = itemObj.Name + "��" + itemObj.Specs + "��";
                        if (!string.IsNullOrEmpty(itemObj.Specs))
                        {
                            this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Item].Text = "��" + itemObj.Specs + "��" + itemObj.Name + "��" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ��";
                        }
                        else
                        {
                            this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Item].Text = itemObj.Name + "��" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ��";
                        }

                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Item].Tag = obj.Item.ID;
                        //����
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Qty].Text = obj.Qty.ToString();

                        //�����鷶Χ
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.CombArea].Text = (this.fpSpread1_Sheet1.Columns[(int)SubtblColumns.CombArea].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[FS.FrameWork.Function.NConvert.ToInt32(obj.CombArea)];
                        //������ȡ����
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.FeeRule].Text = (this.fpSpread1_Sheet1.Columns[(int)SubtblColumns.FeeRule].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[FS.FrameWork.Function.NConvert.ToInt32(obj.FeeRule)];
                        //�������0 ������ 1 Ӥ��ʹ�� 2 ��Ӥ��ʹ��
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.LimitType].Text = (this.fpSpread1_Sheet1.Columns[(int)SubtblColumns.LimitType].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[FS.FrameWork.Function.NConvert.ToInt32(obj.LimitType)];

                        //�״���ȡ��Ŀ
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.FirstFeeFlag].Value = FS.FrameWork.Function.NConvert.ToBoolean(obj.FirstFeeFlag);


                        //����Ա
                        this.fpSpread1_Sheet1.SetValue(row, (int)SubtblColumns.Oper, CacheManager.InterMgr.GetEmployeeInfo(obj.Oper.ID).Name, false);
                        //����ʱ��
                        this.fpSpread1_Sheet1.SetValue(row, (int)SubtblColumns.OperTime, obj.OperDate);

                        //�Ƿ��ظ���ȡ
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.IsAllowReFee].Value = FS.FrameWork.Function.NConvert.ToBoolean(obj.IsAllowReFee);
                        //�Ƿ񵯳�ѡ��
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.IsAllowPopChose].Value = FS.FrameWork.Function.NConvert.ToBoolean(obj.IsAllowPopChose);
                        //�Ƿ�ÿ��������
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.IsCalculateByOnceDose].Value = FS.FrameWork.Function.NConvert.ToBoolean(obj.IsCalculateByOnceDose);
                        //ÿ������λ
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.DoseUnit].Text = obj.DoseUnit;
                        //ÿ������ʼֵ
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OnceDoseFrom].Text = obj.OnceDoseFrom.ToString();
                        //ÿ��������ֵ
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.OnceDoseTo].Text = obj.OnceDoseTo.ToString();


                        //��չ1
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Extend1].Value = FS.FrameWork.Function.NConvert.ToBoolean(obj.Extend1);
                        //��չ2
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Extend2].Value = FS.FrameWork.Function.NConvert.ToBoolean(obj.Extend2);
                        //��չ3
                        this.fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Extend3].Value = FS.FrameWork.Function.NConvert.ToBoolean(obj.Extend3);

                        if (!oper.IsManager)
                        {
                            fpSpread1_Sheet1.Columns[(int)SubtblColumns.Area].Locked = true;

                            if (!isAllDeptShow)
                            {
                                if (obj.Dept_code != this.GetDept().ID
                                    && obj.Dept_code != "ROOT")
                                {
                                    fpSpread1_Sheet1.Rows[row].Visible = false;
                                }
                            }

                            if (isAllowEdit == 0 ||
                                (isAllowEdit == 2 && !IDepartmentList.Contains(obj.Dept_code)))
                            {
                                fpSpread1_Sheet1.Rows[row].Locked = true;
                                fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Item].Locked = true;
                                fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Locked = true;
                            }
                            else
                            {
                                fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Dept].Locked = false;
                            }

                            if (showType != EnumType.ȫ��)
                            {
                                if (fpSpread1_Sheet1.Cells[row, (int)SubtblColumns.Area].Text != showType.ToString())
                                {
                                    fpSpread1_Sheet1.Rows[row].Visible = false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(ex.Message + this.subtblMgr.Err);
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void SaveData()
        {
            this.fpSpread1.StopCellEditing();

            if (this.fpSpread1_Sheet1.Tag == null)
            {
                MessageBox.Show("����ѡ����Ŀ!", "��ʾ");
                return;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //��ʼ����
            try
            {
                this.subtblMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                //��ȫ��ɾ��
                if (alItem.Count > 0)
                {
                    if (this.subtblMgr.DelSubtblInfo((this.fpSpread1_Sheet1.Tag as FS.FrameWork.Models.NeuObject).ID) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.subtblMgr.Err, "��ʾ");
                        return;
                    }
                }

                OrderSubtblNew objSubtbl = null;
                //��ȫ��ѭ������
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    objSubtbl = new OrderSubtblNew();

                    //���÷�Χ��0 ���1 סԺ��3 ȫ��
                    objSubtbl.Area = this.GetSelectData(i, (int)SubtblColumns.Area);


                    //ҽ�����ȫ��������������
                    objSubtbl.OrderType = this.GetSelectData(i, (int)SubtblColumns.OrderType);

                    //�÷����࣬0 ҩƷ���÷���1 ��ҩƷ����Ŀ����
                    objSubtbl.TypeCode = ((FS.FrameWork.Models.NeuObject)this.fpSpread1_Sheet1.Tag).ID;
                    //���Ҵ��룬ȫԺͳһ����'ROOT'
                    objSubtbl.Dept_code = this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.Dept].Tag.ToString();
                    //��Ŀ����
                    objSubtbl.Item.ID = (this.fpSpread1_Sheet1.Rows[i].Tag as FS.FrameWork.Models.NeuObject).ID;
                    //����
                    objSubtbl.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.Qty].Text);
                    //�鷶Χ��0 ÿ����ȡ��1 ��һ����ȡ��2 �ڶ��������
                    objSubtbl.CombArea = this.GetSelectData(i, (int)SubtblColumns.CombArea);
                    //�շѹ��򣺹̶�������*���Ժע��*����Ʒ������*���ҽ��������*Ƶ�Ρ�*����ҽ��������*������*Ժע������Ժע����/Ƶ�� ��ȡ����
                    objSubtbl.FeeRule = this.GetSelectData(i, (int)SubtblColumns.FeeRule);
                    //�������0 ������ 1 Ӥ��ʹ�� 2 ��Ӥ��ʹ��
                    objSubtbl.LimitType = this.GetSelectData(i, (int)SubtblColumns.LimitType);
                    //�״���ȡ��Ŀ
                    objSubtbl.FirstFeeFlag = FS.FrameWork.Function.NConvert.ToInt32(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.FirstFeeFlag].Text).ToString();
                    //����Ա					
                    objSubtbl.Oper.ID = (CacheManager.OutOrderMgr.Operator as FS.HISFC.Models.Base.Employee).ID;
                    //����ʱ��
                    objSubtbl.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.OperTime].Text);

                    //�Ƿ��ظ���ȡ
                    objSubtbl.IsAllowReFee = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.IsAllowReFee].Text);
                    //�Ƿ񵯳�ѡ��
                    objSubtbl.IsAllowPopChose = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.IsAllowPopChose].Text);
                    //�Ƿ�ÿ��������
                    objSubtbl.IsCalculateByOnceDose = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.IsCalculateByOnceDose].Text);
                    //ÿ������λ
                    objSubtbl.DoseUnit = this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.DoseUnit].Text;
                    //ÿ������ʼֵ
                    objSubtbl.OnceDoseFrom = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.OnceDoseFrom].Text);
                    //ÿ��������ֵ
                    objSubtbl.OnceDoseTo = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.OnceDoseTo].Text);

                    //��չ1
                    objSubtbl.Extend1 = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.Extend1].Text) ? "1" : "0";
                    //��չ2
                    objSubtbl.Extend2 = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.Extend2].Text) ? "1" : "0";
                    //��չ3
                    objSubtbl.Extend3 = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[i, (int)SubtblColumns.Extend3].Text) ? "1" : "0";


                    #region ��֤����

                    if (objSubtbl.Area != "0" && (objSubtbl.FeeRule == "1" || objSubtbl.FeeRule == "6"))
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.fpSpread1.ShowRow(0, i, FarPoint.Win.Spread.VerticalPosition.Center);
                        MessageBox.Show("Ժע����������ֻ���������", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    if (objSubtbl.Area != "1" && objSubtbl.OrderType != "0")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.fpSpread1.ShowRow(0, i, FarPoint.Win.Spread.VerticalPosition.Center);
                        MessageBox.Show("��סԺ���ҽ�������ѡ��Ϊ��ȫ������", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    #endregion

                    //��ɾ�� �����
                    //if (this.subtblMgr.DelSubtblInfo(objSubtbl) == -1)
                    //{
                    //    FS.FrameWork.Management.PublicTrans.RollBack();
                    //    this.fpSpread1.ShowRow(0, i, FarPoint.Win.Spread.VerticalPosition.Center);
                    //    MessageBox.Show(this.subtblMgr.Err, "��ʾ");
                    //    return;
                    //}

                    if (this.subtblMgr.InsertSubtblInfo(objSubtbl) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.fpSpread1.ShowRow(0, i, FarPoint.Win.Spread.VerticalPosition.Center);
                        MessageBox.Show(this.subtblMgr.Err, "��ʾ");
                        return;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();

                FS.FrameWork.Models.NeuObject usage = this.currentUsage as FS.FrameWork.Models.NeuObject;
                alItem.Clear();
                alItem = this.subtblMgr.GetSubtblInfo("2", usage.ID, "ROOT");
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return;
            }
            MessageBox.Show("����ɹ�!", "��ʾ");
        }

        /// <summary>
        /// ��ȡ���ڵ�ID
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private string GetSelectData(int row, int column)
        {
            for (int j = 0; j < (this.fpSpread1_Sheet1.Columns[column].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items.Length; j++)
            {
                string item = (this.fpSpread1_Sheet1.Columns[column].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[j];

                if (item == this.fpSpread1_Sheet1.Cells[row, column].Text)
                {
                    return j.ToString();
                }
            }
            return "0";
        }


        /// <summary>
        /// ������:"����"��ť�������
        /// </summary>
        private void ExportToExcel()
        {
            if (this.fpSpread1_Sheet1.Rows.Count == 0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("û��Ҫ����������!"), "��Ϣ");

                return;
            }

            try
            {
                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel ������ (*.xls)|*.xls";
                DialogResult result = dlg.ShowDialog();

                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;
                    this.fpSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.None);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #region ����

        /// <summary>
        /// ���Ĺ���
        /// </summary>
        private void SubFilter(object sender, EventArgs e)
        {
            string area = this.cmbFilterArea.Text;
            string orderType = this.cmbFilterOrderType.Text;
            string dept = this.cmbFilterDept.Text;
            string item = this.txtFilterItem.Text.Trim();

            //DataView dView = this.fpSpread1_Sheet1.DataSource;
        }

        #endregion

        #endregion

        /// <summary>
        /// ��ҩƷ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            this.dtUndrug.DefaultView.RowFilter = "���� like '%" + this.txtFilter_Undrug.Text.Trim() + "%' or ���� like '%" +
                this.txtFilter_Undrug.Text.Trim() + "%' or ��Ŀ���� like '%" + this.txtFilter_Undrug.Text.Trim() + "%' or ƴ���� like '%"
                + this.txtFilter_Undrug.Text.Trim() + "%' or ����� like '%" + this.txtFilter_Undrug.Text.Trim() + "%'";
        }

        private void fpUndrug_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FS.FrameWork.Models.NeuObject undrugObj = new FS.FrameWork.Models.NeuObject();
            undrugObj.ID = this.fpUndrug_Sheet1.GetText(e.Row, 8);
            undrugObj.Name = this.fpUndrug_Sheet1.GetText(e.Row, 0);
            this.Refresh(undrugObj);
        }

        private void fpDrug_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FS.FrameWork.Models.NeuObject drugObj = new FS.FrameWork.Models.NeuObject();
            drugObj.ID = this.fpDrug_Sheet1.GetText(e.Row, 8);
            drugObj.Name = this.fpDrug_Sheet1.GetText(e.Row, 0);
            this.Refresh(drugObj);
        }

        private void txtFilter_Drug_TextChanged(object sender, EventArgs e)
        {
            this.dtDrug.DefaultView.RowFilter = "���� like '%" + this.txtFilter_Drug.Text.Trim() + "%' or ���� like '%" +
                this.txtFilter_Drug.Text.Trim() + "%' or ��Ŀ���� like '%" + this.txtFilter_Drug.Text.Trim() + "%' or ƴ���� like '%"
                + this.txtFilter_Drug.Text.Trim() + "%' or ����� like '%" + this.txtFilter_Drug.Text.Trim() + "%'";
        }

        private void neuListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Refresh(this.currentUsage);
        }
    }

    /// <summary>
    /// ������
    /// </summary>
    public enum SubtblColumns
    {
        /// <summary>
        /// ��Χ�����סԺ
        /// </summary>
        Area = 0,

        /// <summary>
        /// ҽ�����ȫ��������������
        /// </summary>
        OrderType,

        /// <summary>
        /// ���Ҵ��룬ȫԺͳһ����'ROOT'
        /// </summary>
        Dept,

        /// <summary>
        /// ������Ŀ����
        /// </summary>
        Item,

        /// <summary>
        /// ����
        /// </summary>
        Qty,

        /// <summary>
        /// �鷶Χ��0 ÿ����ȡ��1 ��һ����ȡ��2 �ڶ��������
        /// </summary>
        CombArea,

        /// <summary>
        /// �շѹ��򣺹̶�������*���Ժע��*����Ʒ������*���ҽ��������*Ƶ�Ρ�*����ҽ��������*������*Ժע������Ժע����/Ƶ�� ��ȡ����
        /// </summary>
        FeeRule,

        /// <summary>
        /// �������0 ������ 1 Ӥ��ʹ�� 2 ��Ӥ��ʹ��
        /// </summary>
        LimitType,

        /// <summary>
        /// �״���ȡ��Ŀ
        /// </summary>
        FirstFeeFlag,

        /// <summary>
        /// �Ƿ������ظ���ȡ
        /// </summary>
        IsAllowReFee,

        /// <summary>
        /// �Ƿ�������ѡ��
        /// </summary>
        IsAllowPopChose,

        /// <summary>
        /// �Ƿ����ÿ�������ƴ���
        /// </summary>
        IsCalculateByOnceDose,

        /// <summary>
        /// ÿ������λ
        /// </summary>
        DoseUnit,

        /// <summary>
        /// ÿ������ʼֵ
        /// </summary>
        OnceDoseFrom,

        /// <summary>
        /// ÿ��������ֵ
        /// </summary>
        OnceDoseTo,

        /// <summary>
        /// ����Ա
        /// </summary>
        Oper,

        /// <summary>
        /// ����ʱ��
        /// </summary>
        OperTime,

        /// <summary>
        /// ��չ1 ��������ҩ����ӡ
        /// </summary>
        Extend1,

        /// <summary>
        /// ��չ2
        /// </summary>
        Extend2,

        /// <summary>
        /// ��չ3
        /// </summary>
        Extend3
    }

    public enum EnumType
    {
        ȫ��,
        ����,
        סԺ
    }
}
