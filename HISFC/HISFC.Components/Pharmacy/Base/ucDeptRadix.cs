using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
using FS.FrameWork.Function;

namespace FS.HISFC.Components.Pharmacy.Base
{
    /// <summary>
    /// [��������: ��������ҩ����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-04]<br></br>
    /// </summary>
    public partial class ucDeptRadix : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDeptRadix()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Constant phaConsManager = new FS.HISFC.BizLogic.Pharmacy.Constant();

        /// <summary>
        /// ���ϵ�ҵ����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager interManager = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// Ȩ�޿���
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// Ȩ����Ա
        /// </summary>
        private FS.FrameWork.Models.NeuObject privOper = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ҩƷ��Ϣ
        /// </summary>
        private ArrayList alItem = new ArrayList();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private ArrayList alDept = new ArrayList();

        /// <summary>
        /// ���ڵ�
        /// </summary>
        private TreeNode parentNode = null;

        /// <summary>
        /// ��ǰ�����Ļ�������
        /// </summary>
        private FS.FrameWork.Models.NeuObject radixDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �Ƿ��޸Ĺ�
        /// </summary>
        private bool isNew = false;

        /// <summary>
        /// �����ʼʱ��
        /// </summary>
        private DateTime maxBeginDate = System.DateTime.MinValue;

        #endregion

        #region ����

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        internal DateTime BeginDate        
        {
            get
            {
                return FS.FrameWork.Function.NConvert.ToDateTime(this.dtBegin.Text);
            }
        }

        /// <summary>
        /// ��ֹʱ��
        /// </summary>
        internal DateTime EndDate
        {
            get
            {
                return FS.FrameWork.Function.NConvert.ToDateTime(this.dtEnd.Text);
            }
        }

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        private int Init()
        {
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
            List<FS.HISFC.Models.Pharmacy.Item> alList  = itemManager.QueryItemList(true);
            if (alList == null)
            {
                MessageBox.Show(Language.Msg("��ȡҩƷ�б�������") + itemManager.Err);
                return -1;
            }
            foreach (FS.HISFC.Models.Pharmacy.Item item in alList)
            {
                item.Memo = item.Specs;
            }
            this.alItem = new ArrayList(alList.ToArray());

            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            this.alDept = deptManager.GetDeptmentAll();
            if (this.alDept == null)
            {
                MessageBox.Show(Language.Msg("��ȡ�����б�������") + deptManager.Err);
                return -1;
            }

            this.tvDeptList.ImageList = this.tvDeptList.groupImageList;

            this.parentNode = new TreeNode("����ҩ����",0,0);

            this.tvDeptList.Nodes.Add(this.parentNode);

            #region ����Fp�س�/���м�

            FarPoint.Win.Spread.InputMap im;

            im = this.neuSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.neuSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Space, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            #endregion

            this.neuSpread1.EditModeReplace = true;

            return 1;
        }

        /// <summary>
        /// ��ȡ����ҩ����ʱ���б�
        /// </summary>
        /// <returns></returns>
        protected int InitList()
        {
            List<FS.FrameWork.Models.NeuObject> list = this.phaConsManager.QueryDeptRadixDateList(this.privDept.ID);
            if (list == null)
            {
                MessageBox.Show("��ȡ�����б������� " + this.phaConsManager.Err);
                return -1;
            }

            if (list.Count > 0)
            {
                FS.FrameWork.Models.NeuObject max = list[0] as FS.FrameWork.Models.NeuObject;
                this.maxBeginDate = FS.FrameWork.Function.NConvert.ToDateTime(max.Name);
            }

            foreach (FS.FrameWork.Models.NeuObject info in list)
            {
                info.Name = info.ID + "��" + info.Name;
            }

            this.cmbStatList.AddItems(new ArrayList(list.ToArray()));
         
            return 1;
        }

        #endregion

        #region ������

        protected override int OnSave(object sender, object neuObject)
        {
           this.SaveDeptRadix();

           return 1;
        }

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("��������", "��������ҩ����ʱ������", FS.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null);
            toolBarService.AddToolButton("���ӿ���", "��������ҩ����", FS.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null);
            toolBarService.AddToolButton("������ϸ", "��������ҩ��Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            toolBarService.AddToolButton("ɾ������", "ɾ������������Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            toolBarService.AddToolButton("ɾ����ϸ", "ɾ������ҩ��Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            toolBarService.AddToolButton("����ͳ��", "ͳ��ʱ����ڵ�������", FS.FrameWork.WinForms.Classes.EnumImageList.Z�ݴ�, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "��������")
            {
                this.NewStat();
            }
            if (e.ClickedItem.Text == "���ӿ���")
            {
                this.NewDept();
            }
            if (e.ClickedItem.Text == "������ϸ")
            {
                this.NewDeptRadix();
            }
            if (e.ClickedItem.Text == "ɾ������")
            {
                this.DelDeptRadixDept();
            }
            if (e.ClickedItem.Text == "ɾ����ϸ")
            {
                this.DelDeptRadixDetail();
            }
            if (e.ClickedItem.Text == "����ͳ��")
            {
                this.ExpandStat();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        public override int Export(object sender, object neuObject)
        {
            if (this.neuSpread1.Export() == 1)
            {
                MessageBox.Show(Language.Msg("�����ɹ�"));
            }
            return base.Export(sender, neuObject);
        }

        #endregion

        #region ����

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="isClearTree">�Ƿ������</param>
        protected void Clear(bool isClearTree)
        {
            if (isClearTree)
            {
                this.parentNode.Nodes.Clear();
            }

            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// �½�����
        /// </summary>
        protected void NewStat()
        {
            DateTime sysTime = this.phaConsManager.GetDateTimeFromSysDateTime();

            if (this.maxBeginDate != System.DateTime.MinValue)
            {
                this.dtBegin.Value = this.maxBeginDate;
                this.dtEnd.Value = this.dtBegin.Value.AddMonths(1);
            }
            else
            {
                this.dtBegin.Value = sysTime.AddMonths(-1);
                this.dtEnd.Value = sysTime;
            }

            this.dtEnd.Enabled = true;

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                //������Ĭ���ϴλ�����
                //this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColRadixQty].Text = "0";
                this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColSurplusQty].Text = "0";
                this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColSurplusQty].Text = "0";
                this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColExpendQty].Text = "0";
                this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColMemo].Text = "0";
            }
        }

        /// <summary>
        /// ��������ҩ�б�
        /// </summary>
        /// <returns></returns>
        protected int ShowRadixDept()
        {
            List<FS.FrameWork.Models.NeuObject> alList = this.phaConsManager.QueryDeptRadixDeptList(this.privDept.ID,this.BeginDate);
            if (alList == null)
            {
                MessageBox.Show(Language.Msg("��ȡ����ҩ�����б�������") + this.phaConsManager.Err);
                return -1;
            }

            this.Clear(true);

            foreach (FS.FrameWork.Models.NeuObject info in alList)
            {
                TreeNode node = new TreeNode(info.Name);

                node.ImageIndex = 4;
                node.SelectedImageIndex = 5;

                node.Tag = info;

                this.parentNode.Nodes.Add(node);
            }

            this.parentNode.ExpandAll();

            return 1;
        }

        /// <summary>
        /// �������ҩ��Ϣ
        /// </summary>
        /// <param name="deptRadix">����ҩ��Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int AddDeptRadixToFp(FS.HISFC.Models.Pharmacy.Common.DeptRadix deptRadix)
        {
            this.neuSpread1_Sheet1.Rows.Add(0, 1);

            this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColItemName].Text = deptRadix.Item.Name;
            this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColItemSpecs].Text = deptRadix.Item.Specs;
            this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColUnit].Text = deptRadix.Item.PackUnit;
            this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColRadixQty].Value = deptRadix.RadixQty;
            this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColSurplusQty].Value = deptRadix.SurplusQty;
            this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColSupllyQty].Value = deptRadix.SurplusQty;
            this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColExpendQty].Value = deptRadix.ExpendQty;
            this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColMemo].Text = deptRadix.Memo;
            this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColDrugID].Text = deptRadix.Item.ID;

            this.neuSpread1_Sheet1.Rows[0].Tag = deptRadix;

            return 1;
        }

        /// <summary>
        /// ���ݿ��ұ����ȡ����ҩ��ϸ��Ϣ
        /// </summary>
        /// <param name="deptCode">����/����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int ShowRadixDetail(string deptCode)
        {
            this.Clear(false);

            List<FS.HISFC.Models.Pharmacy.Common.DeptRadix> alDetail = this.phaConsManager.QueryDeptRadix(this.privDept.ID, deptCode,this.BeginDate);
            if (alDetail == null)
            {
                MessageBox.Show(Language.Msg("��ȡ����ҩƷ��ϸ��Ϣ��������") + this.phaConsManager.Err);
                return -1;
            }

            foreach (FS.HISFC.Models.Pharmacy.Common.DeptRadix deptRadix in alDetail)
            {
                this.AddDeptRadixToFp(deptRadix);
            }

            return 1;
        }

        /// <summary>
        /// ����ҩƷѡ��
        /// </summary>
        /// <param name="iRowIndex"></param>
        protected void PopDrug(int iRowIndex)
        {
            string[] label = { "����","��Ʒ����", "���" };
            float[] width = { 60F,140F, 100F };
            bool[] visible = { false,true, true };
            FS.FrameWork.Models.NeuObject speObj = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alItem, ref speObj) == 1)
            {
                FS.HISFC.Models.Pharmacy.Common.DeptRadix deptRadix = this.neuSpread1_Sheet1.Rows[iRowIndex].Tag as FS.HISFC.Models.Pharmacy.Common.DeptRadix;

                deptRadix.Item = speObj as FS.HISFC.Models.Pharmacy.Item;

                this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColItemName].Text = speObj.Name;
                this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColItemSpecs].Text = speObj.Memo;
                this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColDrugID].Text = speObj.ID;
                this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColUnit].Text = deptRadix.Item.PackUnit;

                this.SetFocus();
            }
        }

        /// <summary>
        /// ɾ����������
        /// </summary>
        protected void DelDeptRadixDept()
        {
            if (this.tvDeptList.SelectedNode == this.parentNode)
            {
                return;
            }
            if (this.parentNode.Nodes.Count == 0)
            {
                return;
            }

            DialogResult rs = MessageBox.Show(Language.Msg("�Ƿ�ɾ���ÿ������л���ҩ��Ϣ?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                return;
            }

            if (this.tvDeptList.SelectedNode.Parent != null)
            {
                if (this.phaConsManager.DelDeptRadix(this.privDept.ID, this.radixDept.ID,this.BeginDate) == -1)
                {
                    MessageBox.Show(Language.Msg("ִ��ɾ������ʧ��"));
                    return;
                }

                MessageBox.Show(Language.Msg("ɾ���ɹ�"));                

                this.parentNode.Nodes.Remove(this.tvDeptList.SelectedNode);

                if (this.parentNode.Nodes.Count == 0)
                {
                    this.InitList();
                }
            }
        }

        /// <summary>
        /// ɾ��������ϸ
        /// </summary>
        protected void DelDeptRadixDetail()
        {
            if (this.parentNode.Nodes.Count == 0 || this.neuSpread1_Sheet1.Rows.Count == 0)
            {
                return;
            }

            DialogResult rs = MessageBox.Show(Language.Msg("�Ƿ�ɾ���û���ҩ��Ϣ?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                return;
            }

            int iRow = this.neuSpread1_Sheet1.ActiveRowIndex;

            FS.HISFC.Models.Pharmacy.Common.DeptRadix deptRadix = this.neuSpread1_Sheet1.Rows[iRow].Tag as FS.HISFC.Models.Pharmacy.Common.DeptRadix;

            if (this.phaConsManager.DelDeptRadix(this.privDept.ID, deptRadix.Dept.ID, deptRadix.Item.ID,this.BeginDate) == -1)
            {
                MessageBox.Show(Language.Msg("ִ��ɾ������ʧ��"));
                return;
            }

            MessageBox.Show(Language.Msg("ɾ���ɹ�"));

            this.neuSpread1_Sheet1.Rows.Remove(iRow, 1);

            if (this.neuSpread1_Sheet1.Rows.Count == 0)
            {
                this.parentNode.Nodes.Remove(this.tvDeptList.SelectedNode);
            }

        }

        /// <summary>
        /// ������������
        /// </summary>
        protected void NewDept()
        {
            string[] label = { "����","����"};
            float[] width = { 80F, 100F };
            bool[] visible = { true, true };
            FS.FrameWork.Models.NeuObject deptObj = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alDept, ref deptObj) == 1)
            {
                TreeNode node = new TreeNode(deptObj.Name);
                node.Tag = deptObj;

                this.parentNode.Nodes.Add(node);

                this.radixDept = deptObj;

                this.tvDeptList.SelectedNode = node;

                //this.isNew = true;

                this.neuSpread1_Sheet1.ActiveColumnIndex = (int)ColumnSet.ColItemName;
            }
        }

        /// <summary>
        /// ��������ҩƷ��Ϣ
        /// </summary>
        protected void NewDeptRadix()
        {
            if (this.tvDeptList.SelectedNode.Parent == null)
            {
                return;
            }

            int rowCount = this.neuSpread1_Sheet1.Rows.Count;

            this.neuSpread1_Sheet1.Rows.Add(rowCount, 1);
           
            FS.HISFC.Models.Pharmacy.Common.DeptRadix deptRadix = new FS.HISFC.Models.Pharmacy.Common.DeptRadix();
            deptRadix.StockDept = this.privDept;            

            deptRadix.Dept = this.radixDept;

            this.neuSpread1_Sheet1.Rows[rowCount].Tag = deptRadix;

            this.neuSpread1.Select();

            this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.Rows.Count;
            this.neuSpread1_Sheet1.ActiveColumnIndex = (int)ColumnSet.ColItemName;

            this.neuSpread1.StartCellEditing(null, false);

            this.isNew = true;
        }

        /// <summary>
        /// ��Ч�Լ��
        /// </summary>
        /// <returns></returns>
        protected bool IsValid()
        {
            if (this.neuSpread1_Sheet1.Rows.Count == 0)
            {
                MessageBox.Show(Language.Msg("����ӻ���ҩ��ϸ��Ϣ"));
                return false;
            }

            for(int i = 0;i < this.neuSpread1_Sheet1.Rows.Count;i++)
            {
                string memo = this.neuSpread1_Sheet1.Cells[i,(int)ColumnSet.ColMemo].Text;
               
                if (!FS.FrameWork.Public.String.ValidMaxLengh(memo,100))
                {
                    MessageBox.Show(Language.Msg("��ע�������ݹ��� ���ʵ�����"));
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// ����
        /// </summary>
        protected void SaveDeptRadix()
        {
            if (!this.IsValid())
            {
                return;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.phaConsManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (this.phaConsManager.DelDeptRadix(this.privDept.ID, this.radixDept.ID,this.BeginDate) == -1)            
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("����ǰ ɾ��ԭ���һ���ҩ��Ϣ��������") + this.phaConsManager.Err);
                return;
            }

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.Pharmacy.Common.DeptRadix deptRadix = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.Common.DeptRadix;

                if (deptRadix == null || deptRadix.Item.ID == "")
                {
                    continue;
                }
                deptRadix.BeginDate = this.BeginDate;
                deptRadix.EndDate = this.EndDate;

                deptRadix.RadixQty = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColRadixQty].Text);
                deptRadix.SurplusQty = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i,(int)ColumnSet.ColSurplusQty].Text);
                deptRadix.SupplyQty = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i,(int)ColumnSet.ColSurplusQty].Text);
                deptRadix.ExpendQty = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColExpendQty].Text);
                deptRadix.Memo = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColMemo].Text;

                FS.HISFC.Models.Base.Department dept = interManager.GetDepartment(this.privDept.ID);
                switch (dept.DeptType.ID.ToString())
                {
                    case "T":
                        {
                            deptRadix.DeptType = DrugType.Terminal.ToString();
                        }
                        break;
                    case "P":
                        {
                            deptRadix.DeptType = DrugType.State.ToString();
                        }
                        break;
                    case "N":
                        {
                            deptRadix.DeptType = DrugType.Nurse.ToString();
                        }
                        break;
                    default:
                        {
                            deptRadix.DeptType = DrugType.Nurse.ToString();
                            break;
                        }

                }

                if (this.phaConsManager.InsertDeptRadix(deptRadix) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    if (this.phaConsManager.DBErrCode == 1)
                    {
                        MessageBox.Show(Language.Msg(deptRadix.Item.Name + "������Ϣά���ظ� ��ɾ��һ��"));
                    }
                    else
                    {
                        MessageBox.Show(Language.Msg("�����»���ҩ��Ϣ��������") + this.phaConsManager.Err);
                    }
                    return;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show(Language.Msg("����ɹ�"));

            this.isNew = false;

            this.InitList();

            this.ShowRadixDept();
        }

        /// <summary>
        /// ������ͳ��
        /// </summary>
        protected void ExpandStat()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
            {
                return;
            }

            DateTime dtEnd = this.phaConsManager.GetDateTimeFromSysDateTime();
            DateTime dtBegin = dtEnd.AddDays(-29);
            if (FS.FrameWork.WinForms.Classes.Function.ChooseDate(ref dtBegin, ref dtEnd) == 1)
            {
                string drugCollection = "";
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    drugCollection = drugCollection + "','" + this.neuSpread1_Sheet1.Cells[i,(int)ColumnSet.ColDrugID].Text;
                }

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڽ���ҩƷ������ͳ�� ���Ժ�..."));
                Application.DoEvents();

                DataSet ds = new DataSet();
                if (this.phaConsManager.ExecQuery("Pharmacy.DeptRadix.Expand", ref ds, this.privDept.ID, this.radixDept.ID, dtBegin.ToString(), dtEnd.ToString(), drugCollection) == -1)
                {
                    MessageBox.Show(Language.Msg("ִ��Sql��ѯ��������Ϣʧ��"));
                    return;
                }

                if (ds != null && ds.Tables.Count > 0)
                {
                    System.Collections.Hashtable hsExpand = new Hashtable();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        hsExpand.Add(dr[0].ToString(), dr[1]);
                    }

                    for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                    {
                        if (hsExpand.ContainsKey(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColDrugID].Text))
                        {
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColExpendQty].Value = hsExpand[this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColDrugID].Text];
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColExpendQty].Value = 0;
                        }
                    }
                }

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        protected void SetFocus()
        {
            this.neuSpread1_Sheet1.ActiveColumnIndex = (int)ColumnSet.ColRadixQty;

            this.neuSpread1.StartCellEditing(null, false);
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.privOper = this.phaConsManager.Operator;

                this.privDept = ((FS.HISFC.Models.Base.Employee)this.phaConsManager.Operator).Dept;

                this.Init();

                this.InitList();
            }

            base.OnLoad(e);
        }

        private void tvDeptList_AfterSelect(object sender, TreeViewEventArgs e)
        {            
            if (e.Node.Parent == null)
            {
                this.Clear(false);

                return;
            }
            else
            {
                this.radixDept = e.Node.Tag as FS.FrameWork.Models.NeuObject;

                this.ShowRadixDetail(this.radixDept.ID);
            }
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColItemName)
            {
                this.PopDrug(e.Row);
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Space)
            {
                if (this.neuSpread1.ContainsFocus && this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColItemName)
                {
                    this.PopDrug(this.neuSpread1_Sheet1.ActiveRowIndex);

                    return base.ProcessDialogKey(keyData);
                }
                if (this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColRadixQty)
                {
                    this.neuSpread1_Sheet1.ActiveColumnIndex++;
                }
                else if (this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColSurplusQty)
                {
                    this.neuSpread1_Sheet1.ActiveColumnIndex = (int)ColumnSet.ColSupllyQty;
                }
                else if (this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColSupllyQty)
                {
                    this.neuSpread1_Sheet1.ActiveColumnIndex = (int)ColumnSet.ColMemo;
                }
                else if (this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColMemo)
                {
                    if (this.neuSpread1_Sheet1.Rows.Count - 1 == this.neuSpread1_Sheet1.ActiveRowIndex)
                    {
                        this.NewDeptRadix();
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.ActiveRowIndex++;
                    }
                }
            }
            if (keyData == Keys.F8)
            {
                this.NewDeptRadix();
            }
            return base.ProcessDialogKey(keyData);
        }

        private void tvDeptList_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (this.isNew)
            {
                DialogResult rs = MessageBox.Show(Language.Msg("�ÿ��һ���ҩ�����ѷ����仯 �Ƿ񱣴�?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (rs == DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        private void neuSpread1_EditModeOff(object sender, EventArgs e)
        {
            decimal radixQty = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColRadixQty].Text);
            decimal surplusQty = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColSurplusQty].Text);
            decimal expendQty = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColExpendQty].Text);

            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColSupllyQty].Text = (radixQty + expendQty - surplusQty).ToString();

        }

        private void cmbStatList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbStatList.Tag != null)
            {
                DateTime dtBegin = FS.FrameWork.Function.NConvert.ToDateTime(this.cmbStatList.Tag.ToString());
                this.dtBegin.Value = dtBegin;
                this.dtBegin.Enabled = false;

                string statStr = this.cmbStatList.Text;
                string endDateStr = statStr.Substring(statStr.IndexOf('��') + 1);
                this.dtEnd.Value = FS.FrameWork.Function.NConvert.ToDateTime(endDateStr);
                this.dtEnd.Enabled = false;

                this.ShowRadixDept();
            }
        }      

        /// <summary>
        /// ������
        /// </summary>
        private enum ColumnSet
        {
            ColItemName,
            ColItemSpecs,
            ColUnit,
            ColRadixQty,
            ColSurplusQty,
            ColExpendQty,
            ColSupllyQty,
            ColMemo,
            ColDrugID
        }

        /// <summary>
        /// ��������ö��
        /// </summary>
        private enum DrugType
        {
            Nurse,
            Terminal,
            State
        }

    }
}

