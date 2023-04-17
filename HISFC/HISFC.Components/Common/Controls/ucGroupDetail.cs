using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Fee.Inpatient;
using FS.FrameWork.Function;
namespace FS.HISFC.Components.Common.Controls
{
    /// <summary>
    /// [��������: ����ά��]<br></br>
    /// [�� �� ��: Ѧռ��]<br></br>
    /// [����ʱ��: 2006��12��12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����='' 
    ///  />
    /// </summary>
    public partial class ucGroupDetail : FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucGroupDetail()
        {
            InitializeComponent();

            this.fpSpread1.ComboSelChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpSpread1_ComboSelChange);
        }

        #region ����
        /// <summary>
        /// �Ƿ�ֻ��һ�����ҽ������ײ���
        /// </summary>
        private bool isDeptOnly = true;

        /// <summary>
        /// �Ƿ�Ϊ��ҩƷ
        /// </summary>
        private bool isUndrug = false;

        /// <summary>
        /// ��ǰ��½����
        /// </summary>
        private FS.FrameWork.Models.NeuObject deptInfo;

        private decimal gpCost = decimal.Zero;
        #endregion

        #region ö��
        #region cols
        /// <summary>
        /// ��ö��
        /// </summary>
        private enum Cols
        {
            /// <summary>
            /// ��Ŀ����
            /// </summary>
            ItemCode,
            ItemName,//��Ŀ����
            Price,//�۸�
            Qty,//����
            Unit,//��λ
            Dept,//ִ�п���
            Combo,//��Ϻ�
            Memo,//��ע
            OperCode,//����Ա
            OperDate,//��������
            SortId//�����
        }
        #endregion

        #endregion

        #region ҵ������
        /// <summary>
        /// ���׹�����
        /// </summary>
        FS.HISFC.BizLogic.Manager.ComGroup grpMgr = new FS.HISFC.BizLogic.Manager.ComGroup();
        /// <summary>
        /// ִ�п����б�
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbDept = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        #endregion

        /// <summary>
        /// ��Ŀ�б�
        /// </summary>        
        private FS.HISFC.Components.Common.Controls.ucItemList ItemList;
        //�洢��������
        private ArrayList iGroups = new ArrayList();

        #region ����
        /// <summary>
        /// �Ƿ�ֻ�Ե�ǰ����Ա��½���ҽ������ײ���
        /// </summary>
        public bool IsDeptOnly
        {
            get
            {
                return this.isDeptOnly;
            }
            set
            {
                this.isDeptOnly = value;
            }
        }
        /// <summary>
        /// �Ƿ�Ϊ��ҩƷ
        /// </summary>
        public bool IsUndrug
        {
            set
            {
                this.isUndrug = value;
            }
        }

        /// <summary>
        /// ��ǰ��½����
        /// </summary>
        public FS.FrameWork.Models.NeuObject DeptInfo
        {
            get
            {
                if (this.deptInfo != null)
                    this.deptInfo = new FS.FrameWork.Models.NeuObject();
                return this.deptInfo;
            }
            set
            {
                this.deptInfo = value;
            }
        }
        private FS.HISFC.Components.Common.Controls.EnumShowItemType enumShowItemType = EnumShowItemType.All;
        // {407F4A63-CC38-4842-BFDA-995E1C3FC664}
        [Category("�ؼ�����"), Description("���øÿؼ����ص���Ŀ��� ҩƷ:drug ��ҩƷ undrug ����: all")]
        public FS.HISFC.Components.Common.Controls.EnumShowItemType ������Ŀ���
        {
            get
            {
                return enumShowItemType;
            }
            set
            {
                enumShowItemType = value;
            }
        }
        #endregion

        #region ������
        /// <summary>
        /// ���幤����
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("�������", "�������", 0, true, false, null);
            toolBarService.AddToolButton("ɾ������", "ɾ������", 1, true, false, null);
            toolBarService.AddToolButton("�����ϸ", "�����ϸ", 2, true, false, null);
            toolBarService.AddToolButton("ɾ����ϸ", "ɾ����ϸ", 3, true, false, null);
            toolBarService.AddToolButton("���", "���", 6, true, false, null);

            return toolBarService;
        }
        /// <summary>
        /// �˳�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Exit(object sender, object neuObject)
        {
            return base.Exit(sender, neuObject);
        }
        /// <summary>
        /// ���淽��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Save(object sender, object neuObject)
        {
            //���淽��
            Save();
            return base.Save(sender, neuObject);
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "�������":
                    AddGroup();
                    break;
                case "ɾ������":
                    DelGroup();
                    break;
                case "�����ϸ":
                    AddGroupDetail();
                    break;
                case "ɾ����ϸ":
                    DelGroupDetail();
                    break;
                case "���":
                    SaveAs();
                    break;
                default:
                    break;

            }
        }
        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        private int ucInit()
        {
            this.FindForm().KeyPreview = true;
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("������������,���Ժ�...");
                Application.DoEvents();

                //��ʼ�������б� ��ȡִ�п���ʱ����
                this.InitDeptList();

                //����Ƕ�һ�����Ҳ���
                if (this.isDeptOnly)
                {
                    this.deptInfo = (grpMgr.Operator as FS.HISFC.Models.Base.Employee).Dept;
                    //ֻ��һ�����ҽ������ײ��� ֻ��ȡһ�����ҵ������б�
                    this.neuTreeView1.Nodes.Clear();
                    TreeNode node = new TreeNode();
                    node.Text = this.deptInfo.Name;
                    node.Tag = this.deptInfo;//��ǰ��½����
                    node.ImageIndex = 3;
                    node.SelectedImageIndex = 3;

                    this.neuTreeView1.Nodes.Add(node);
                    //��ǰ��½����node��Ϊ�����
                    //���ݿ���ID��ӿ�������
                    this.AddDeptGroup(node, this.deptInfo.ID);

                    //���õ�ǰִ�п�����ʾ��Ϣ
                    this.cmbDept.Tag = this.deptInfo.ID;
                    this.cmbDept.Text = this.deptInfo.Name;
                    this.cmbDept.Enabled = false;
                    this.neuTreeView1.ExpandAll();
                }
                else
                {	//�ɲ鿴ȫԺ�������� �������Ϊ
                    this.GetAllGroups();
                    this.InitTree();
                    this.cmbDept.IsListOnly = true;
                }

                // ����farpoint��Ӧ����¼��
                this.InitFP();

                #region ��ӿ����б�ؼ�
                Controls.Add(lbDept);
                lbDept.Hide();
                lbDept.BorderStyle = BorderStyle.FixedSingle;
                lbDept.BringToFront();
                // lbDept.SelectedIndexChanged += new FS.FrameWork.WinForms.Controls.NeuListBoxPopup.SelectedIndexCollection(lbDept_SelectItem);
                lbDept.ItemSelected += new EventHandler(lbDept_ItemSelected);
                #endregion

                #region �����Ŀ�б�ؼ�
                //{44141277-5235-434b-8E0D-F70635D04B73}
                ItemList = new ucItemList();
                Controls.Add(ItemList);
                //����Ƿ�ҩƷ
                //if (this.isUndrug)
                //{
                //    ItemList.enuShowItemType = FS.HISFC.Components.Common.Controls.EnumShowItemType.Undrug;//��ҩƷ
                //}
                //else
                //{
                //    ItemList.enuShowItemType = FS.HISFC.Components.Common.Controls.EnumShowItemType.All;//ȫ��
                //}
                // {407F4A63-CC38-4842-BFDA-995E1C3FC664}
                ItemList.enuShowItemType = this.enumShowItemType;
                ItemList.Init(string.Empty);
                ItemList.Hide();
                ItemList.BringToFront();

                /* [2007/01/27]
                 * ԭ����ע�͵���,��֪��Ϊʲô.
                 */
                //ItemList.Init("");

                ItemList.SelectItem += new FS.HISFC.Components.Common.Controls.ucItemList.MyDelegate(ItemList_SelectItem);
                #endregion

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception a)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(a.Message);
            }

            return 0;
        }

        /// <summary>
        /// ѡ��ִ�п���
        /// </summary>
        /// <returns></returns>
        private int SelectDept()
        {
            int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
            if (CurrentRow < 0) return 0;

            fpSpread1.StopCellEditing();
            FS.FrameWork.Models.NeuObject item = null;
            item = lbDept.GetSelectedItem();
            if (item == null) return -1;

            fpSpread1_Sheet1.SetTag(CurrentRow, (int)Cols.Dept, item.ID);
            fpSpread1_Sheet1.SetValue(CurrentRow, (int)Cols.Dept, item.Name, false);

            lbDept.Visible = false;

            return 0;
        }

        /// <summary>
        /// ���ɿ��������б�
        /// </summary>
        /// <returns></returns>
        private int InitTree()
        {
            this.neuTreeView1.Nodes.Clear();
            //��������
            ArrayList deptTypes = FS.HISFC.Models.Base.DepartmentTypeEnumService.List();

            //һ��Ϊ��������
            foreach (FS.FrameWork.Models.NeuObject obj in deptTypes)
            {
                TreeNode node = new TreeNode();
                node.Text = obj.Name;//������������
                node.Tag = obj;
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;

                this.neuTreeView1.Nodes.Add(node);
                //��ӿ������Ͱ����Ŀ���     
                if (AddDepts(obj, node) == -1) return -1;
            }
            return 0;
        }

        /// <summary>
        /// ���ɿ����б�
        /// </summary>
        /// <returns></returns>
        private int InitDeptList()
        {
            try
            {
                FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
                ArrayList depts = deptManager.GetDeptmentAll();
                if (depts == null || depts.Count == 0) return -1;

                //��������ComboBox��ӿ���
                cmbDept.AddItems(depts);
                //��ִ�п���NeuListBoxPopup����ӿ���
                lbDept.AddItems(depts);

            }
            catch (Exception e)
            {
                MessageBox.Show("��ӿ����б�ʱ����!" + e.Message, "��ʾ");
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// ��ӿ������Ͱ����Ŀ���
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private int AddDepts(FS.FrameWork.Models.NeuObject type, TreeNode parent)
        {
            try
            {
                FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
                //��ȡtype���Ϳ���
                ArrayList depts = deptManager.GetDeptmentByType(type.ID);
                if (depts == null) return 0;
                //��ӿ���
                foreach (FS.HISFC.Models.Base.Department dept in depts)
                {
                    TreeNode child = new TreeNode();
                    child.Text = dept.Name;
                    child.Tag = dept;
                    child.ImageIndex = 3;
                    child.SelectedImageIndex = 3;

                    parent.Nodes.Add(child);
                    AddGroups(child, dept.ID);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("��ȡ�����б����!" + e.Message, "��ʾ");
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        private int ShowGroup(FS.HISFC.Models.Fee.ComGroup group)
        {
            txtName.Text = group.Name;
            txtName.Tag = group;
            txtInput.Text = group.inputCode;
            txtSpell.Text = group.spellCode;
            txtMemo.Text = group.reMark;

            if (group.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
                chkValid.Checked = true;
            else
                chkValid.Checked = false;

            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            FS.HISFC.Models.Base.Department dept = deptManager.GetDeptmentById(group.deptCode);
            if (dept == null)
                cmbDept.Text = group.deptCode;
            else
                cmbDept.Text = dept.Name;

            return 0;
        }

        /// <summary>
        /// ��ʾ������ϸ
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        private int ShowDetail(string groupID)
        {
            FS.HISFC.BizLogic.Manager.ComGroupTail detailManager = new FS.HISFC.BizLogic.Manager.ComGroupTail();
            //������ϸ
            gpCost = decimal.Zero;
            ArrayList details = detailManager.GetComGroupTailByGroupID(groupID);
            if (details == null)
            {
                MessageBox.Show("��ȡ������ϸʧ�� " + detailManager.Err);
                return -1;
            }

            if (fpSpread1_Sheet1.Rows.Count > 0)
                fpSpread1_Sheet1.Rows.Remove(0, fpSpread1_Sheet1.Rows.Count);
            foreach (FS.HISFC.Models.Fee.ComGroupTail detail in details)
            {
                AddDetailToFP(detail);
            }
            this.tbTotCost.Text = gpCost.ToString("F2");
            return 0;
        }

        /// <summary>
        /// �����ʾ������Ϣ
        /// </summary>
        /// <returns></returns>
        private int Clear()
        {
            txtName.Text = "";
            txtName.Tag = null;
            txtInput.Text = "";
            txtSpell.Text = "";
            chkValid.Checked = true;
            txtMemo.Text = "";
            this.tbTotCost.Text = "0.00";
            gpCost = decimal.Zero;
            if (!this.isDeptOnly)
                cmbDept.Text = "";

            if (fpSpread1_Sheet1.Rows.Count > 0)
                fpSpread1_Sheet1.Rows.Remove(0, fpSpread1_Sheet1.Rows.Count);

            return 0;
        }

        #region ��ȡ����
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns></returns>
        private int GetAllGroups()
        {
            FS.HISFC.BizLogic.Manager.ComGroup groupManager = new FS.HISFC.BizLogic.Manager.ComGroup();
            try
            {
                //iGroups = groupManager.GetAllGroups();
                iGroups = groupManager.GetAllGroupsByRoot("1");
            }
            catch (Exception e)
            {
                if (iGroups == null) iGroups = new ArrayList();
                MessageBox.Show("����HISFC.Manager.ComGroup.GetAllGroups()����!" + e.Message);
                return -1;
            }
            if (iGroups == null) iGroups = new ArrayList();

            return 0;
        }

        /// <summary>
        /// ��ӿ����������б�
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="deptID"></param>						
        private int AddGroups(TreeNode parent, string deptID)
        {
            foreach (FS.HISFC.Models.Fee.ComGroup group in iGroups)
            {
                if (group.deptCode == deptID)
                {
                    //AddGroup(parent, group);
                    this.AddGroupsRecursion(parent, group);
                }
            }

            //AddGroupsRecursion(parent, deptID, "ROOT");
            return 0;
        }

        private int AddGroupsRecursion(TreeNode parent, FS.HISFC.Models.Fee.ComGroup group)
        {

            ArrayList al = this.grpMgr.GetGroupsByDeptParent("1", group.deptCode, group.ID);
            if (al.Count == 0)
            {
                TreeNode newNode = new TreeNode();
                newNode.Tag = group;
                newNode.Text = "[" + group.inputCode + "]" + group.Name;// +"[" + group.ID + "]";
                parent.Nodes.Add(newNode);

                return -1;
            }
            else
            {
                #region donggq.--2010.09.28--{5461FEE6-FDEC-49c6-BE84-6015D1965878}--�޸������׵ݹ�

                TreeNode newNode = new TreeNode();
                newNode.Tag = group;
                newNode.Text = group.Name;// +"[" + group.ID + "]";
                parent.Nodes.Add(newNode);

                #endregion

                foreach (FS.HISFC.Models.Fee.ComGroup item in al)
                {
                    #region ԭ����
                    //TreeNode newNode = new TreeNode ();
                    //newNode.Tag = group;
                    //newNode.Text = group.Name;// +"[" + group.ID + "]";
                    //parent.Nodes.Add(newNode);
                    //return this.AddGroupsRecursion(newNode, item); 
                    #endregion

                    #region donggq.--2010.09.28--{5461FEE6-FDEC-49c6-BE84-6015D1965878}--�޸������׵ݹ�
                    this.AddGroupsRecursion(newNode, item);
                    #endregion
                }
            }

            return 1;
        }

        /// <summary>
        /// ���ݿ���ID ��ӿ�������
        /// </summary>
        /// <param name="parent">���ڵ�</param>
        /// <param name="deptID">���ұ���</param>
        /// <returns>�ɹ�����1 �����أ�1 �����ݷ���0</returns>
        private int AddDeptGroup(TreeNode parent, string deptID)
        {
            FS.HISFC.BizLogic.Manager.ComGroup groupManager = new FS.HISFC.BizLogic.Manager.ComGroup();
            //{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
            //ArrayList al = new ArrayList();
            ////���ݿ��һ����������
            //al = groupManager.GetAllGroupList(deptID);
            //if (al == null || al.Count == 0)
            //    return 0;
            //foreach (FS.HISFC.Models.Fee.ComGroup group in al)
            //{
            //    //�������
            //    this.AddGroup(parent, group);
            //}
            ArrayList al = new ArrayList();
            //���ݿ��һ����������
            al = groupManager.GetAllGroupsByRoot("1");

            ArrayList alDeptList = new ArrayList();

            foreach (FS.HISFC.Models.Fee.ComGroup item in al)
            {
                if (item.deptCode == deptID)
                {
                    alDeptList.Add(item);
                }
            }


            if (alDeptList == null || alDeptList.Count == 0)
                return 0;
            foreach (FS.HISFC.Models.Fee.ComGroup group in alDeptList)
            {
                this.AddGroupsRecursion(parent, group);
            }
            return 1;
        }

        /// <summary>
        ///������� 
        /// </summary>
        /// <param name="parent">TreeNode</param>
        /// <param name="group">����ʵ��</param>
        /// <returns>0</returns>
        private int AddGroup(TreeNode parent, FS.HISFC.Models.Fee.ComGroup group)
        {
            TreeNode node = new TreeNode();
            node.Tag = group;
            node.Text = "[" + group.inputCode + "]" + group.Name;
            node.ImageIndex = 1;
            node.SelectedImageIndex = 2;

            //if (this.isDeptOnly)
            //    this.neuTreeView1.Nodes[0].Nodes.Add(node);
            //else
            parent.Nodes.Add(node);
            //{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
            //this.AddGroupsRecursion(parent, group);
            this.neuTreeView1.SelectedNode = node;

            return 0;
        }

        #endregion

        #region ��ʾ����
        /// <summary>
        /// �����ϸ��farpoint
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        private int AddDetailToFP(FS.HISFC.Models.Fee.ComGroupTail detail)
        {
            fpSpread1_Sheet1.Rows.Add(fpSpread1_Sheet1.Rows.Count, 1);
            int row = fpSpread1_Sheet1.Rows.Count - 1;
            this.fpSpread1_Sheet1.Rows[row].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            decimal curPrice = decimal.Zero;
            if (detail.drugFlag == "1")
            {
                //ҩƷ�����������
                FS.HISFC.BizLogic.Pharmacy.Item drugManager = new FS.HISFC.BizLogic.Pharmacy.Item();
                //����ҩƷ������ĳһҩƷ��Ϣ
                FS.HISFC.Models.Pharmacy.Item drug = drugManager.GetItem(detail.itemCode);
                if (drug == null)//û�ҵ�
                {
                    drug = new FS.HISFC.Models.Pharmacy.Item();
                    drug.Name = "��Ŀ�����޸���Ŀ";
                }
                //������Ϊ��
                if (drug.Specs != null && drug.Specs != "")
                {
                    drug.Name = drug.Name + "{" + drug.Specs + "}";
                }

                fpSpread1_Sheet1.Cells[row, (int)Cols.ItemCode].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                fpSpread1_Sheet1.SetValue(row, (int)Cols.ItemCode, drug.UserCode, false);//��Ŀ����
                fpSpread1_Sheet1.SetValue(row, (int)Cols.ItemName, drug.Name, false);//��Ŀ����

                fpSpread1_Sheet1.SetValue(row, (int)Cols.Price, drug.Price, false);//�۸�
                //fpSpread1_Sheet1.SetValue(row, (int)Cols.Unit, drug.PriceUnit, false);//��λ

                decimal price;
                FarPoint.Win.Spread.CellType.ComboBoxCellType comboType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                comboType.Editable = true;
                comboType.Items = (new string[]{(drug as FS.HISFC.Models.Pharmacy.Item).MinUnit,
                                                (drug as FS.HISFC.Models.Pharmacy.Item).PackUnit});
                this.fpSpread1_Sheet1.Cells[row, (int)Cols.Unit].CellType = comboType;
                this.fpSpread1_Sheet1.Cells[row, (int)Cols.Unit].Locked = false;

                if (string.IsNullOrEmpty(detail.unitFlag) || FS.FrameWork.Public.String.IsNumeric(detail.unitFlag) == false)
                {
                    detail.unitFlag = "1";
                }

                if (detail.unitFlag == "2")
                {
                    this.fpSpread1_Sheet1.SetValue(row, (int)Cols.Unit, ((FS.HISFC.Models.Pharmacy.Item)drug).PackUnit, false);
                    drug.PriceUnit = ((FS.HISFC.Models.Pharmacy.Item)drug).PackUnit;
                    price = FS.FrameWork.Public.String.FormatNumber(drug.Price, 4);
                    this.fpSpread1_Sheet1.SetValue(row, (int)Cols.Price, price, false);
                }
                else
                {
                    price = FS.FrameWork.Public.String.FormatNumber(drug.Price / drug.PackQty, 4);
                    this.fpSpread1_Sheet1.SetValue(row, (int)Cols.Price, price, false);
                    this.fpSpread1_Sheet1.SetValue(row, (int)Cols.Unit, ((FS.HISFC.Models.Pharmacy.Item)drug).MinUnit, false);
                    drug.PriceUnit = ((FS.HISFC.Models.Pharmacy.Item)drug).MinUnit;

                }
                curPrice = price;
                this.fpSpread1_Sheet1.SetTag(row, (int)Cols.Unit, drug);
            }
            else
            {
                FS.HISFC.BizLogic.Fee.Item undrugManager = new FS.HISFC.BizLogic.Fee.Item();

                FS.HISFC.Models.Fee.Item.Undrug undrug = undrugManager.GetValidItemByUndrugCode(detail.itemCode);
                if (undrug == null)
                {//û�ҵ�
                    undrug = new FS.HISFC.Models.Fee.Item.Undrug();
                    undrug.Name = "��Ŀ�����޸���Ŀ";
                }
                fpSpread1_Sheet1.Cells[row, (int)Cols.ItemCode].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                fpSpread1_Sheet1.SetValue(row, (int)Cols.ItemCode, undrug.UserCode, false);//��Ŀ����
                fpSpread1_Sheet1.SetValue(row, (int)Cols.ItemName, undrug.Name, false);//��Ŀ����
                fpSpread1_Sheet1.SetValue(row, (int)Cols.Price, undrug.Price, false);//�۸�
                fpSpread1_Sheet1.SetValue(row, (int)Cols.Unit, undrug.PriceUnit, false);//��λ
                curPrice = undrug.Price;
                FarPoint.Win.Spread.CellType.TextCellType textType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.fpSpread1_Sheet1.Cells[row, (int)Cols.Unit].CellType = textType;
                this.fpSpread1_Sheet1.Cells[row, (int)Cols.Unit].Locked = true;
                this.fpSpread1_Sheet1.SetValue(row, (int)Cols.Unit, undrug.PriceUnit, false);
            }

            //��Ŀ����
            fpSpread1_Sheet1.SetTag(row, (int)Cols.ItemName, detail.itemCode);

            fpSpread1_Sheet1.SetValue(row, (int)Cols.Qty, detail.qty, false);//����
            fpSpread1_Sheet1.SetValue(row, (int)Cols.Dept, detail.deptName, false);//ִ�п���
            fpSpread1_Sheet1.SetTag(row, (int)Cols.Dept, detail.deptCode);//ִ�п��Ҵ���

            fpSpread1_Sheet1.SetValue(row, (int)Cols.Combo, detail.combNo, false);//��Ϻ�
            fpSpread1_Sheet1.SetValue(row, (int)Cols.Memo, detail.reMark, false);//��ע
            fpSpread1_Sheet1.SetValue(row, (int)Cols.OperCode, detail.operCode, false);//����Ա
            fpSpread1_Sheet1.SetValue(row, (int)Cols.OperDate, detail.OperDate.ToString(), false);//����ʱ��
            fpSpread1_Sheet1.SetValue(row, (int)Cols.SortId, (decimal)detail.SortNum, false); //���
            gpCost += Math.Round(detail.qty * curPrice, 2);
            fpSpread1_Sheet1.Rows[row].Tag = detail.drugFlag;
            return 0;
        }

        #endregion

        /// <summary>
        /// ����farpoint��Ӧ����¼��
        /// </summary>
        private void InitFP()
        {
            FarPoint.Win.Spread.InputMap im;
            im = this.fpSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.F2, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.F3, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.F4, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.PageUp, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.PageDown, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
        }

        #region FarPoint����

        /// <summary>
        /// ������Ŀ�б������б�λ��
        /// </summary>
        /// <returns></returns>
        private int SetLocation()
        {
            Control _cell = fpSpread1.EditingControl;
            if (_cell == null) return 0;

            if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.ItemName)
            {
                int y = this.splitContainer2.Location.Y + this.splitContainer2.Panel1.Height + _cell.Location.Y + _cell.Height + ItemList.Height + 7;
                if (y <= this.Height)
                    ItemList.Location = new Point(this.splitContainer1.Panel1.Width + _cell.Location.X + 10, y - ItemList.Height);
                else
                    ItemList.Location = new Point(this.splitContainer1.Panel1.Width + _cell.Location.X + 10, this.splitContainer2.Panel1.Height + _cell.Location.Y - ItemList.Height - 7);
            }
            else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.Dept)
            {
                lbDept.Location = new Point(this.splitContainer1.Panel1.Width + _cell.Location.X + 10,
                    this.splitContainer2.Location.Y + this.splitContainer2.Panel1.Height + _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                lbDept.Size = new Size(_cell.Width + SystemInformation.Border3DSize.Width * 2, 150);
            }
            return 0;
        }

        /// <summary>
        /// ѡ����
        /// </summary>
        /// <returns></returns>
        private int SelectItem()
        {
            if (ItemList.Visible == false)
            {
                ItemList.Visible = true;
                return 0;
            }
            try
            {
                fpSpread1.StopCellEditing();
                FS.HISFC.Models.Base.Item item = new FS.HISFC.Models.Base.Item();

                int rtn = ItemList.GetSelectItem(out item);
                if (rtn == -1 || rtn == 0) return -1;
                int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
                if (CurrentRow < 0) return -1;
                //����������FarPoint
                AddDetailToFP(item, CurrentRow);
                fpSpread1.Focus();
                
                fpSpread1_Sheet1.SetActiveCell(CurrentRow, (int)Cols.Qty, false);

                ItemList.Hide();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "��ʾ");
                fpSpread1.Focus();
                fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.ItemName, true);
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// ѡ������뵱ǰ��
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private int SelectItem(int row)
        {
            ItemList.SetCurrentRow(row);
            if (SelectItem() == -1) return -1;
            return 0;
        }
        /// <summary>
        /// ���ݴ���������FarPoint
        /// �����ϸ��FarPoint
        /// </summary>
        /// <param name="item"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private int AddDetailToFP(FS.HISFC.Models.Base.Item item, int row)
        {
            //��Ŀ�۸�
            fpSpread1_Sheet1.SetValue(row, (int)Cols.Price, item.Price, false);
            //����
            fpSpread1_Sheet1.SetText(row, (int)Cols.Qty, "1");
            //��λ
            fpSpread1_Sheet1.SetValue(row, (int)Cols.Unit, item.PriceUnit, false);

            decimal price;
            //ҩƷ��ѡ��ҩƷ�շѵ�λ,Ĭ��Ϊ��С��λ
            //if (item.IsPharmacy)
            if (item.ItemType == EnumItemType.Drug)
            {
                FarPoint.Win.Spread.CellType.ComboBoxCellType comboType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                comboType.Editable = true;
                comboType.Items = (new string[]{(item as FS.HISFC.Models.Pharmacy.Item).MinUnit,
                                                (item as FS.HISFC.Models.Pharmacy.Item).PackUnit});

                this.fpSpread1_Sheet1.Cells[row, (int)Cols.Unit].Text = comboType.Items[0];
                this.fpSpread1_Sheet1.Cells[row, (int)Cols.Unit].CellType = comboType;
                this.fpSpread1_Sheet1.Cells[row, (int)Cols.Unit].Locked = false;
                if (item.MinFee.User03 == "2")
                {
                    this.fpSpread1_Sheet1.SetValue(row, (int)Cols.Unit, ((FS.HISFC.Models.Pharmacy.Item)item).PackUnit, false);
                    item.PriceUnit = ((FS.HISFC.Models.Pharmacy.Item)item).PackUnit;
                    price = FS.FrameWork.Public.String.FormatNumber(item.Price, 4);
                    this.fpSpread1_Sheet1.SetValue(row, (int)Cols.Price, price, false);
                }
                else
                {
                    price = FS.FrameWork.Public.String.FormatNumber(item.Price / item.PackQty, 4);
                    this.fpSpread1_Sheet1.SetValue(row, (int)Cols.Price, price, false);
                    this.fpSpread1_Sheet1.SetValue(row, (int)Cols.Unit, ((FS.HISFC.Models.Pharmacy.Item)item).MinUnit, false);
                    item.PriceUnit = ((FS.HISFC.Models.Pharmacy.Item)item).MinUnit;
                }
                this.fpSpread1_Sheet1.SetTag(row, (int)Cols.Unit, item);


            }
            else//��ҩƷ
            {
                FarPoint.Win.Spread.CellType.TextCellType textType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.fpSpread1_Sheet1.Cells[row, (int)Cols.Unit].CellType = textType;
                this.fpSpread1_Sheet1.Cells[row, (int)Cols.Unit].Locked = true;
                this.fpSpread1_Sheet1.SetValue(row, (int)Cols.Unit, item.PriceUnit, false);
            }

            //ִ�п��ң�Ϊ��
            fpSpread1_Sheet1.SetTag(row, (int)Cols.Dept, this.deptInfo.ID);
            fpSpread1_Sheet1.SetValue(row, (int)Cols.Dept, this.deptInfo.Name, false);
            //��Ϻ�
            fpSpread1_Sheet1.SetValue(row, (int)Cols.Combo, "", false);
            //��ע
            fpSpread1_Sheet1.SetValue(row, (int)Cols.Memo, "", false);
            //��Ŀ����
            //if (item.IsPharmacy)
            if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                fpSpread1_Sheet1.Cells[row, (int)Cols.ItemCode].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                fpSpread1_Sheet1.SetValue(row, (int)Cols.ItemCode, item.UserCode, false);//��Ŀ����
                if (item.Specs != null && item.Specs != "")
                    fpSpread1_Sheet1.SetValue(row, (int)Cols.ItemName, item.Name + "{" + item.Specs + "}", false);
                else
                    fpSpread1_Sheet1.SetValue(row, (int)Cols.ItemName, item.Name, false);
            }
            else
            {
                fpSpread1_Sheet1.Cells[row, (int)Cols.ItemCode].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                fpSpread1_Sheet1.SetValue(row, (int)Cols.ItemCode, item.UserCode, false);//��Ŀ����
                fpSpread1_Sheet1.SetValue(row, (int)Cols.ItemName, item.Name, false);
            }
            //��Ŀ����
            fpSpread1_Sheet1.SetTag(row, (int)Cols.ItemName, item.ID);

            //if (item.IsPharmacy)
            if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                fpSpread1_Sheet1.Rows[row].Tag = "1";
            else
                fpSpread1_Sheet1.Rows[row].Tag = "2";

            return 0;
        }


        #endregion

        #region ��ӡ�ɾ��������ϸ
        /// <summary>
        /// ���һ��������ϸ
        /// </summary>
        /// <returns></returns>
        public int AddGroupDetail()
        {
            this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.Rows.Count, 1);
            this.fpSpread1.Focus();
            this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.Rows.Count - 1, 1);
            this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.Rows.Count - 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;


            return 0;
        }
        /// <summary>
        /// ɾ��һ��������ϸ
        /// </summary>
        /// <returns></returns>
        public int DelGroupDetail()
        {
            int row = fpSpread1_Sheet1.ActiveRowIndex;
            if (row < 0 || fpSpread1_Sheet1.RowCount == 0) return 0;

            string name = fpSpread1_Sheet1.GetText(row, (int)Cols.ItemName);
            if (MessageBox.Show("�Ƿ�ɾ��" + name + "?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.Yes)
            {
                this.fpSpread1.StopCellEditing();
                fpSpread1_Sheet1.Rows.Remove(row, 1);
            }
            return 0;
        }

        #endregion

        #region ��ӡ�ɾ������
        /// <summary>
        /// ɾ��ѡ�е�����
        /// </summary>
        /// <returns></returns>
        public int DelGroup()
        {
            TreeNode currNode = this.neuTreeView1.SelectedNode;

            //{C1B7688D-CFB1-4d7e-A4DA-E909B64B66D3}
            if (currNode.Nodes.Count > 0)
            {
                MessageBox.Show("�ýڵ��»������׽ڵ㣬�����ײ�ڵ㿪ʼɾ��!");
                return -1;
            }

            //��ǰû��ѡ�������
            if (currNode == null) return 0;
            //ѡ�еĲ������ף�����
            if (currNode.Tag.GetType() != typeof(FS.HISFC.Models.Fee.ComGroup)) return 0;

            FS.HISFC.Models.Fee.ComGroup group = currNode.Tag as FS.HISFC.Models.Fee.ComGroup;
            //ȷ��ɾ��?
            if (MessageBox.Show("�Ƿ�ɾ������:" + group.Name + "?", "��ʾ", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No) return 0;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            FS.HISFC.BizLogic.Manager.ComGroup grpManager = new FS.HISFC.BizLogic.Manager.ComGroup();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(grpManager.Connection);
            //t.BeginTransaction();//��ʼ����           

            //grpManager.SetTrans(t.Trans);
            try
            {
                //ɾ��������Ϣ
                if (grpManager.DeleteComGroup(group) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("ɾ��������Ϣ��ʱ����!" + grpManager.Err, "��ʾ");
                    return -1;
                }
                //ɾ��������ϸ
                if (grpManager.DelGroupDetails(group.ID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("ɾ��������ϸ��ʱ����!" + grpManager.Err, "��ʾ");
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("ɾ��������Ϣ��ʱ����!" + e.Message, "��ʾ");
                return -1;
            }
            this.neuTreeView1.Nodes.Remove(currNode);
            MessageBox.Show("����ɾ���ɹ�!", "��ʾ");

            return 0;
        }


        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        public int AddGroup()
        {
            Clear();
            AddGroupDetail();
            txtName.Focus();
            return 0;
        }
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="details">ArrayList����</param>
        /// <param name="deptID">����ID</param>
        /// <returns></returns>
        public int AddGroup(ArrayList details, string deptID)
        {
            this.splitContainer1.Panel1.Hide();
            Clear();
            this.cmbDept.Tag = deptID;

            foreach (FS.HISFC.Models.Fee.ComGroupTail detail in details)
            {
                this.AddDetailToFP(detail);
            }
            this.tbTotCost.Text = gpCost.ToString("F2");
            this.txtName.Focus();

            return 0;
        }
        #endregion


        #region ����
        /// <summary>
        /// ���淽��
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            this.fpSpread1.StopCellEditing();

            if (Valid() == -1) return -1;

            //���׹�����
            //FS.HISFC.BizLogic.Manager.ComGroup grpMgr = new FS.HISFC.BizLogic.Manager.ComGroup();
            //����
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(grpMgr.Connection);
            //t.BeginTransaction();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //������ϸ������
            FS.HISFC.BizLogic.Manager.ComGroupTail detailMgr = new FS.HISFC.BizLogic.Manager.ComGroupTail();
            grpMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            detailMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //��ȡ���״���
            object obj = txtName.Tag;
            string groupID = "";
            if (obj == null)//������
                groupID = detailMgr.getGroupID();
            else
                groupID = (obj as FS.HISFC.Models.Fee.ComGroup).ID;

            //��������ʵ��
            // FS.HISFC.Models.Fee.ComGroup group = AddGroupInstance(groupID, FS.FrameWork.Management.Connection.Operator.ID);
            FS.HISFC.Models.Fee.ComGroup group = AddGroupInstance(groupID, FS.FrameWork.Management.Connection.Operator.ID, obj);
            int count = 0;

            try
            {
                if (obj != null)
                {
                    //ɾ��ԭ������
                    #region ɾ��ԭ������
                    //ɾ��������Ϣ
                    if (grpMgr.DeleteComGroup(group) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("ɾ��������Ϣ��ʱ����!" + grpMgr.Err, "��ʾ");
                        return -1;
                    }
                    //ɾ��������ϸ
                    if (grpMgr.DelGroupDetails(group.ID) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("ɾ��������ϸ��ʱ����!" + grpMgr.Err, "��ʾ");
                        return -1;
                    }
                    #endregion
                }
                //��������
                if (grpMgr.InsertInToComGroup(group) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����������Ϣ�����!" + grpMgr.Err, "��ʾ");
                    return -1;
                }
                //ѭ��������ϸ
                for (int i = 0; i < fpSpread1_Sheet1.RowCount; i++)
                {
                    if (fpSpread1_Sheet1.GetTag(i, (int)Cols.ItemName) == null || fpSpread1_Sheet1.GetTag(i, (int)Cols.ItemName).ToString()
                        == "") continue;
                    //��ֵ������ϸʵ��
                    FS.HISFC.Models.Fee.ComGroupTail detail = AddGrpDetailInstance(i, groupID, FS.FrameWork.Management.Connection.Operator.ID);
                    if (detail == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                    //����������ϸ
                    if (detailMgr.InsertDataIntoComGroupTail(detail) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����������ϸ��ʱ����!" + detailMgr.Err, "��ʾ");
                        return -1;
                    }
                    count++;
                }
                //{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
                //if (count == 0)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show("��¼��������ϸ!", "��ʾ");
                //    return -1;
                //}
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��������ʱ����!" + e.Message, "��ʾ");
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            //ˢ�������б�
            if (obj == null)
            {
                //������������ף�������Ӧ�����������һ���½ڵ�
                //{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
                //TreeNode node = FindNode(cmbDept.Tag.ToString());
                TreeNode node = this.neuTreeView1.SelectedNode;//FindNode(cmbDept.Tag.ToString());
                if (this.isDeptOnly)
                {
                    this.AddGroup(node, group);
                }
                else
                {

                    if (node != null)
                        AddGroup(node, group);
                }
            }
            else
            {
                //������޸ĵ����ף����޸Ľڵ������ 
                this.neuTreeView1.SelectedNode.Tag = group;
                this.neuTreeView1.SelectedNode.Text = "[" + group.inputCode + "]" + group.Name;
            }

            MessageBox.Show("����ɹ�!", "��ʾ");
            txtName.Focus();

            return 0;
        }

        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        /// <returns></returns>
        private int Valid()
        {
            if (txtName.Text == null || txtName.Text == "")
            {
                MessageBox.Show("�������Ʋ���Ϊ��!", "��ʾ");
                txtName.Focus();
                return -1;
            }
            if (cmbDept.Tag == null || cmbDept.Tag.ToString() == "")
            {
                MessageBox.Show("�����������Ҳ���Ϊ��!", "��ʾ");
                cmbDept.Focus();
                return -1;
            }
            string groupID = "";
            if (txtName.Tag != null)
            {
                FS.HISFC.Models.Fee.ComGroup oldGroup = (FS.HISFC.Models.Fee.ComGroup)txtName.Tag;
                groupID = oldGroup.ID;
            }

            if (!FS.FrameWork.Public.String.ValidMaxLengh(txtName.Text, 50))
            {
                MessageBox.Show("�������ƹ���!", "��ʾ");
                txtName.Focus();
                return -1;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(txtInput.Text, 8))
            {
                MessageBox.Show("���������!", "��ʾ");
                txtInput.Focus();
                return -1;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(txtSpell.Text, 8))
            {
                MessageBox.Show("ƴ�������!", "��ʾ");
                txtInput.Focus();
                return -1;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(txtMemo.Text, 150))//{AC9872BD-F749-43a1-A31C-511352A198D6}
            {
                MessageBox.Show("��ע����!", "��ʾ");
                txtInput.Focus();
                return -1;
            }
            ArrayList list = this.grpMgr.QueryGroupsByName(this.txtName.Text, cmbDept.Tag.ToString());
            foreach (FS.HISFC.Models.Fee.ComGroup obj in list)
            {
                if (groupID != "")
                {
                    if (obj.ID != groupID)
                    {
                        MessageBox.Show(txtName.Text + "�Ѿ�����,�����");
                        return -1;
                    }
                }
                else
                {
                    MessageBox.Show(txtName.Text + "�Ѿ�����,�����");
                    return -1;
                }
            }
            return 0;
        }

        /// <summary>
        /// ���Ҵ���ΪdeptID�Ŀ��ҽڵ�
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        private TreeNode FindNode(string deptID)
        {
            foreach (TreeNode parent in this.neuTreeView1.Nodes)
            {
                foreach (TreeNode dept in parent.Nodes)
                {
                    if (dept.Tag.GetType() == typeof(FS.HISFC.Models.Base.Department))
                    {
                        if ((dept.Tag as FS.HISFC.Models.Base.Department).ID == deptID)
                        {
                            return dept;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// ���Ҵ���ΪdeptID�Ŀ��ҽڵ�
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        private TreeNode FindNodeByGroupID(string groupID)
        {
            foreach (TreeNode parent in this.neuTreeView1.Nodes)
            {
                TreeNode node = FindNodeResu(parent, groupID);
                if (node == null) continue;
                return node;
            }

            return null;
        }

        //{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
        private TreeNode FindNodeResu(TreeNode parentNode, string groupID)
        {
            if (parentNode.Tag.GetType() == typeof(FS.FrameWork.Models.NeuObject)) //��������һ��
            {
                foreach (TreeNode item in parentNode.Nodes)
                {
                    if (item.Nodes.Count > 0)
                    {
                        TreeNode tn = this.FindNodeResu(item, groupID);

                        if (tn == null)
                        {
                            continue;
                        }
                        else
                        {
                            return tn;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            else if (parentNode.Tag.GetType() == typeof(FS.HISFC.Models.Base.Department)) //����һ��
            {
                foreach (TreeNode node in parentNode.Nodes)
                {
                    if ((node.Tag as FS.HISFC.Models.Fee.ComGroup).ID == groupID)
                    {
                        return node;
                    }
                    else
                    {
                        if (node.Nodes.Count > 0)
                        {
                            TreeNode tNode = this.FindNodeResu(node, groupID);
                            if (tNode == null)
                            {
                                continue;
                            }
                            else
                            {

                                return tNode;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

            }
            else
            {
                foreach (TreeNode node in parentNode.Nodes)//���׼�
                {
                    if ((node.Tag as FS.HISFC.Models.Fee.ComGroup).ID == groupID)
                    {
                        return node;
                    }
                    else
                    {
                        if (node.Nodes.Count > 0)
                        {
                            TreeNode tN = this.FindNodeResu(node, groupID);
                            if (tN == null)
                            {
                                continue;
                            }
                            else
                            {
                                return tN;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }


            }
            return null;

        }


        /// <summary>
        ///��������ʵ��
        /// </summary>
        /// <param name="newID"></param>
        /// <param name="OperID"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Fee.ComGroup AddGroupInstance(string newID, string OperID)
        {
            FS.HISFC.Models.Fee.ComGroup group = new FS.HISFC.Models.Fee.ComGroup();
            group.ID = newID;//����ID
            group.Name = txtName.Text;//��������
            group.inputCode = txtInput.Text;//������
            group.spellCode = txtSpell.Text;//ƴ����
            group.groupKind = "1";//��������
            group.deptCode = cmbDept.Tag.ToString();//���׿���
            group.deptName = cmbDept.Tag.ToString();
            group.reMark = txtMemo.Text;//��ע
            group.operCode = OperID;//����ԱID
            //��Ч
            if (chkValid.Checked)
                group.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;//��Ч��־1��Ч2��Ч
            else
                group.ValidState = FS.HISFC.Models.Base.EnumValidState.Invalid;
            //{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
            if (this.neuTreeView1.SelectedNode.Tag.GetType() != typeof(FS.HISFC.Models.Fee.ComGroup))
            {
                group.ParentGroupID = "ROOT";
            }
            else
            {
                FS.HISFC.Models.Fee.ComGroup comGroup = this.neuTreeView1.SelectedNode.Tag as FS.HISFC.Models.Fee.ComGroup;
                group.ParentGroupID = comGroup.ID;
            }
            return group;
        }

        private FS.HISFC.Models.Fee.ComGroup AddGroupInstance(string newID, string OperID, object obj)
        {
            FS.HISFC.Models.Fee.ComGroup group = new FS.HISFC.Models.Fee.ComGroup();
            group.ID = newID;//����ID
            group.Name = txtName.Text;//��������
            group.inputCode = txtInput.Text;//������
            group.spellCode = txtSpell.Text;//ƴ����
            group.groupKind = "1";//��������
            group.deptCode = cmbDept.Tag.ToString();//���׿���
            group.deptName = cmbDept.Tag.ToString();
            group.reMark = txtMemo.Text;//��ע
            group.operCode = OperID;//����ԱID
            //��Ч
            if (chkValid.Checked)
                group.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;//��Ч��־1��Ч2��Ч
            else
                group.ValidState = FS.HISFC.Models.Base.EnumValidState.Invalid;
            //{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
            if (obj == null)
            {
                if (this.neuTreeView1.SelectedNode.Tag.GetType() != typeof(FS.HISFC.Models.Fee.ComGroup))
                {
                    group.ParentGroupID = "ROOT";
                }
                else
                {
                    FS.HISFC.Models.Fee.ComGroup comGroup = this.neuTreeView1.SelectedNode.Tag as FS.HISFC.Models.Fee.ComGroup;
                    group.ParentGroupID = comGroup.ID;
                }
            }
            else
            {
                if (this.neuTreeView1.SelectedNode.Parent.Tag.GetType() != typeof(FS.HISFC.Models.Fee.ComGroup))
                {
                    group.ParentGroupID = "ROOT";
                }
                else
                {
                    FS.HISFC.Models.Fee.ComGroup comGroup = this.neuTreeView1.SelectedNode.Parent.Tag as FS.HISFC.Models.Fee.ComGroup;
                    group.ParentGroupID = comGroup.ID;
                }
            }
            return group;
        }

        //����������ϸʵ��
        private FS.HISFC.Models.Fee.ComGroupTail AddGrpDetailInstance(int row, string newID, string OperID)
        {
            FS.HISFC.Models.Fee.ComGroupTail detail = new FS.HISFC.Models.Fee.ComGroupTail();
            detail.ID = newID;
            detail.sequenceNo = row;
            detail.itemCode = fpSpread1_Sheet1.GetTag(row, (int)Cols.ItemName).ToString();//����

            #region �ж�����
            decimal amount = 0;
            string qty = fpSpread1_Sheet1.GetText(row, (int)Cols.Qty);
            if (qty == null || qty == "") qty = "0";
            try
            {
                amount = Convert.ToDecimal(qty);
            }
            catch
            {
                MessageBox.Show("�����������Ϸ�!", "��ʾ");
                return null;
            }
            if (amount < 0)
            {
                MessageBox.Show("������������С����!", "��ʾ");
                return null;
            }
            #endregion
            detail.qty = amount;
            object obj = fpSpread1_Sheet1.GetTag(row, (int)Cols.Dept);
            if (obj != null)
            {
                detail.deptCode = obj.ToString();//ִ�п���
                detail.deptName = obj.ToString();
            }

            detail.drugFlag = fpSpread1_Sheet1.Rows[row].Tag.ToString();
            if (fpSpread1_Sheet1.Cells[row, (int)Cols.Unit].CellType is FarPoint.Win.Spread.CellType.ComboBoxCellType)
            {
                FarPoint.Win.Spread.CellType.ComboBoxCellType cell = fpSpread1_Sheet1.Cells[row, (int)Cols.Unit].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType;
                for (int i = 0; i < cell.Items.Length; i++)
                {
                    if (cell.Items[i] == fpSpread1_Sheet1.Cells[row, (int)Cols.Unit].Text)
                    {
                        detail.unitFlag = (i + 1).ToString();
                        break;
                    }
                }
            }
            else
            {
                detail.unitFlag = "2";
            }
            detail.combNo = fpSpread1_Sheet1.GetText(row, (int)Cols.Combo);
            detail.reMark = fpSpread1_Sheet1.GetText(row, (int)Cols.Memo);
            detail.SortNum = FS.FrameWork.Function.NConvert.ToInt32(fpSpread1_Sheet1.GetText(row, (int)Cols.SortId)); //���
            detail.operCode = OperID;

            return detail;
        }

        #endregion

        private void GetTreeNode(TreeNode node, string txt)
        {
            foreach (System.Windows.Forms.TreeNode tr in node.Nodes)
            {
                if (tr.Text.IndexOf(txt) >= 0)
                {
                    tr.BackColor = System.Drawing.Color.Pink;
                    if (tr.Parent != null)
                    {

                    }
                }
                else
                {
                    tr.BackColor = System.Drawing.Color.White;
                }
                this.GetTreeNode(tr, txt);
            }
        }
        public int SaveAs()
        {
            this.fpSpread1.StopCellEditing();
            txtName.Tag = null;
            //{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
            //if (Valid() == -1) return -1;

            //���׹�����
            //FS.HISFC.BizLogic.Manager.ComGroup grpMgr = new FS.HISFC.BizLogic.Manager.ComGroup();
            //����
            //FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(grpMgr.Connection);
            //t.BeginTransaction();//��ʼ����            
            //������ϸ������
            FS.HISFC.BizLogic.Manager.ComGroupTail detailMgr = new FS.HISFC.BizLogic.Manager.ComGroupTail();

            //grpMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //detailMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //�����µ����״���
            string groupID = detailMgr.getGroupID();
            //��������ʵ��
            FS.HISFC.Models.Fee.ComGroup group = AddGroupInstance(groupID, FS.FrameWork.Management.Connection.Operator.ID);

            Forms.frmChooseSelectNode frm = new FS.HISFC.Components.Common.Forms.frmChooseSelectNode();
            //{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
            //frm.DeptObj = this.cmbDept.SelectedItem;
            DialogResult dr = frm.ShowDialog();

            if (dr == DialogResult.Cancel)
            {
                return -1;
            }



            TreeNode selectedNode = frm.SelectedNode;

            frm.Dispose();

            //���¿���ֵ

            if (selectedNode.Tag.GetType() == typeof(FS.HISFC.Models.Base.Department))
            {
                group.deptCode = (selectedNode.Tag as FS.HISFC.Models.Base.Department).ID;
                group.deptName = (selectedNode.Tag as FS.HISFC.Models.Base.Department).ID;
                group.ParentGroupID = "ROOT";

            }
            else
            {
                group.deptCode = (selectedNode.Tag as FS.HISFC.Models.Fee.ComGroup).deptCode;
                group.deptName = (selectedNode.Tag as FS.HISFC.Models.Fee.ComGroup).deptCode;
                group.ParentGroupID = (selectedNode.Tag as FS.HISFC.Models.Fee.ComGroup).ID;
            }


            int count = 0;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                //��������
                if (grpMgr.InsertInToComGroup(group) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����������Ϣ�����!" + grpMgr.Err, "��ʾ");
                    return -1;
                }

                //ѭ��������ϸ
                for (int i = 0; i < fpSpread1_Sheet1.RowCount; i++)
                {
                    if (fpSpread1_Sheet1.GetTag(i, (int)Cols.ItemName) == null || fpSpread1_Sheet1.GetTag(i, (int)Cols.ItemName).ToString()
                        == "") continue;
                    //��ֵ������ϸʵ��
                    FS.HISFC.Models.Fee.ComGroupTail detail = AddGrpDetailInstance(i, groupID, FS.FrameWork.Management.Connection.Operator.ID);
                    if (detail == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                    //����������ϸ
                    if (detailMgr.InsertDataIntoComGroupTail(detail) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����������ϸ��ʱ����!" + detailMgr.Err, "��ʾ");
                        return -1;
                    }
                    count++;
                }
                if (count == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("��¼��������ϸ!", "��ʾ");
                    return -1;
                }
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��������ʱ����!" + e.Message, "��ʾ");
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            //ˢ�������б�
            TreeNode node = new TreeNode();

            if (selectedNode.Tag.GetType() == typeof(FS.HISFC.Models.Base.Department))
            {
                //{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
                //node = FindNode(cmbDept.Tag.ToString());
                node = FindNode((selectedNode.Tag as FS.HISFC.Models.Base.Department).ID);
            }
            else
            {
                node = FindNodeByGroupID((selectedNode.Tag as FS.HISFC.Models.Fee.ComGroup).ID);
            }
            if (node != null)//{9317CF49-CD92-459c-A90A-845D543F11E6}
            {
                //TreeNode node = this.neuTreeView1.SelectedNode;
                //{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
                if (this.isDeptOnly)
                    this.AddGroup(node, group);

                else
                {
                    if (node != null)
                    {
                        this.AddGroup(node, group);
                    }
                }

                node.ExpandAll();
            }
            //this.neuTreeView1.SelectedNode = node;


            MessageBox.Show("���ɹ�!", "��ʾ");

            txtName.Focus();

            return 0;
        }

        #endregion

        #region �¼�

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                #region Up
                //�жϸÿؼ��Ƿ��ý���
                if (fpSpread1.ContainsFocus)
                {
                    //�����Ŀ�б���ʾ
                    if (ItemList.Visible)
                        ItemList.PriorRow();
                    //���ִ�п�����ʾ
                    else if (lbDept.Visible)
                        lbDept.PriorRow();
                    else
                    {
                        int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
                        if (CurrentRow > 0)
                        {
                            fpSpread1_Sheet1.ActiveRowIndex = CurrentRow - 1;
                            fpSpread1_Sheet1.AddSelection(CurrentRow - 1, 0, 1, 1);
                        }
                    }
                }
                #endregion
            }

            if (keyData == Keys.Down)
            {
                #region Down
                //�жϸÿؼ��Ƿ��ý���
                if (fpSpread1.ContainsFocus)
                {
                    //�����Ŀ�б���ʾ
                    if (ItemList.Visible)
                        ItemList.NextRow();
                    //���ִ�п�����ʾ
                    else if (lbDept.Visible)
                        lbDept.NextRow();
                    else
                    {
                        int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
                        if (CurrentRow < fpSpread1_Sheet1.RowCount - 1)
                        {
                            fpSpread1_Sheet1.ActiveRowIndex = CurrentRow + 1;
                            fpSpread1_Sheet1.AddSelection(CurrentRow + 1, 0, 1, 1);
                        }
                        else
                        {   //���һ��������ϸ
                            AddGroupDetail();
                        }
                    }
                }
                #endregion
            }
            if (keyData == Keys.F4)
            {
                FS.FrameWork.WinForms.Controls.NeuTextBox txt = new FS.FrameWork.WinForms.Controls.NeuTextBox();
                txt.Size = new Size(200, 40);
                txt.KeyDown += new KeyEventHandler(txt_KeyDown);
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(txt);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                foreach (System.Windows.Forms.TreeNode node in this.neuTreeView1.Nodes)
                {
                    this.GetTreeNode(node, ((System.Windows.Forms.Control)sender).Text);
                }
            }
            //throw new NotImplementedException();
        }

        /// <summary>
        /// ����ProcessDialogKey��������Ի����
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {

            switch (keyData)
            {
                case Keys.Enter:
                    #region Enter
                    //�жϸÿؼ��Ƿ��ý���
                    if (fpSpread1.ContainsFocus)
                    {
                        //��ǰ����������ΪItemName(��Ŀ����)
                        if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.ItemName)
                        {
                            if (fpSpread1_Sheet1.Rows[fpSpread1_Sheet1.ActiveRowIndex].Tag == null)
                            {
                                if (SelectItem() == -1) return true;
                            }
                            else
                            {
                                fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.Qty, false);
                            }
                        }
                        //��ǰ����������ΪQty(����)
                        else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.Qty)
                        {   //���ü���Cell
                            fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.Dept, false);
                        }
                        //��ǰ����������ΪDept(ִ�п���)
                        else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.Dept)
                        {   //ִ�п����б���ʾ
                            if (lbDept.Visible)
                            {
                                SelectDept();
                            }
                            fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.Combo, false);
                        }
                        //��ǰ����������ΪCombo(��Ϻ�)
                        else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.Combo)
                        {
                            fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.Memo, false);
                        }
                        //��ǰ����������ΪMemo(��ע)
                        else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.Memo)
                        {
                            if (fpSpread1_Sheet1.RowCount == fpSpread1_Sheet1.ActiveRowIndex + 1)
                            {
                                //���һ��������ϸ
                                AddGroupDetail();
                            }
                            else
                            {
                                fpSpread1_Sheet1.ActiveRowIndex++;
                                fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.ItemName, true);
                            }
                        }
                    }
                    #endregion
                    break;
                case Keys.Escape:
                    //�����Ŀ������ʾ
                    if (ItemList.Visible)
                        ItemList.Visible = false;
                    //���ִ�п�����ʾ
                    if (lbDept.Visible)
                        lbDept.Visible = false;
                    break;
                #region 2007-4-28 ע�͵Ĵ��� ·־��
                /* ��up down�ڴ��¼��в����������Ƶ�ProcessCmdKey�¼���
                case Keys.Up:
                    #region Up
                    //�жϸÿؼ��Ƿ��ý���
                    if (fpSpread1.ContainsFocus)
                    {
                        //�����Ŀ�б���ʾ
                        if (ItemList.Visible)
                            ItemList.PriorRow();
                        //���ִ�п�����ʾ
                        else if (lbDept.Visible)
                            lbDept.PriorRow();
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
                    #endregion
                    break;
                case Keys.Down:
                    #region Down
                    //�жϸÿؼ��Ƿ��ý���
                    if (fpSpread1.ContainsFocus)
                    {
                        //�����Ŀ�б���ʾ
                        if (ItemList.Visible)
                            ItemList.NextRow();
                        //���ִ�п�����ʾ
                        else if (lbDept.Visible)
                            lbDept.NextRow();
                        else
                        {
                            int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
                            if (CurrentRow < fpSpread1_Sheet1.RowCount - 1)
                            {
                                fpSpread1_Sheet1.ActiveRowIndex = CurrentRow + 1;
                                fpSpread1_Sheet1.AddSelection(CurrentRow + 1, 0, 1, 0);
                            }
                            else
                            {   //���һ��������ϸ
                                AddGroupDetail();
                            }
                        }
                    }
                    #endregion
                    break;
                 */
                #endregion

                #region F2,F3,F4
                case Keys.F2:
                    //���FarPoint��ý��㲢����Ŀ������ʾ
                    if (fpSpread1.ContainsFocus && ItemList.Visible)
                    {
                        int row = int.Parse(keyData.ToString().Substring(1)) - 1;
                        SelectItem(row);
                    }
                    break;
                case Keys.F3:
                    if (fpSpread1.ContainsFocus && ItemList.Visible)
                    {
                        int row = int.Parse(keyData.ToString().Substring(1)) - 1;
                        SelectItem(row);
                    }
                    break;
                case Keys.F4:
                    if (fpSpread1.ContainsFocus && ItemList.Visible)
                    {
                        int row = int.Parse(keyData.ToString().Substring(1)) - 1;
                        SelectItem(row);
                    }
                    break;
                #endregion
                default:
                    break;
            }

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// FarPoint�ڱ༭ģʽ�ϴ����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_EditModeOn(object sender, EventArgs e)
        {
            fpSpread1.EditingControl.KeyDown += new KeyEventHandler(EditingControl_KeyDown);

            SetLocation();

            if (fpSpread1_Sheet1.ActiveColumnIndex != (int)Cols.Dept)
                lbDept.Visible = false;

            if (fpSpread1_Sheet1.ActiveColumnIndex != (int)Cols.ItemName)
                ItemList.Visible = false;
        }

        /// <summary>
        /// EditingControl.KeyDownί���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditingControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.ItemName)
            {
                if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F5 ||
                    e.KeyCode == Keys.F6 || e.KeyCode == Keys.F7 ||
                    e.KeyCode == Keys.F8 || e.KeyCode == Keys.F9 ||
                    e.KeyCode == Keys.F10)
                {
                    if (ItemList.Visible)
                    {
                        int row = int.Parse(e.KeyCode.ToString().Substring(1)) - 1;
                        SelectItem(row);
                    }
                }
                else if (e.KeyCode == Keys.F11)
                {//�л����뷨
                    if (ItemList.Visible)
                    {
                        ItemList.ChangeQueryType();
                    }
                }
                else if (e.KeyCode == Keys.PageDown)
                {
                    if (ItemList.Visible)
                        ItemList.NextPage();
                }
                else if (e.KeyCode == Keys.PageUp)
                {
                    if (ItemList.Visible)
                        ItemList.PriorPage();
                }
            }
        }

        /// <summary>
        /// FarPoint�༭�ı��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            string _Text;
            switch (e.Column)
            {
                case (int)Cols.ItemName://��Ŀ����
                    _Text = fpSpread1_Sheet1.ActiveCell.Text;
                    ItemList.Filter(_Text);
                    if (ItemList.Visible == false) ItemList.Visible = true;
                    //��յ�ǰ�б���
                    fpSpread1_Sheet1.SetValue(e.Row, (int)Cols.Price, "", false);
                    fpSpread1_Sheet1.SetValue(e.Row, (int)Cols.Qty, "", false);
                    fpSpread1_Sheet1.SetValue(e.Row, (int)Cols.Unit, "", false);
                    fpSpread1_Sheet1.SetValue(e.Row, (int)Cols.Dept, "", false);
                    fpSpread1_Sheet1.SetTag(e.Row, (int)Cols.ItemName, null);
                    fpSpread1_Sheet1.SetValue(e.Row, (int)Cols.ItemCode, "", false);
                    break;
                case (int)Cols.Dept://����ִ�п���			
                    _Text = fpSpread1_Sheet1.ActiveCell.Text;
                    lbDept.Filter(_Text);

                    if (lbDept.Visible == false)
                    {
                        lbDept.Visible = true;
                    }
                    fpSpread1_Sheet1.SetTag(e.Row, (int)Cols.Dept, null);

                    break;
            }
        }

        /// <summary>
        /// TreeViewѡ������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //ѡ������
            if (e.Node.Tag.GetType() == typeof(FS.HISFC.Models.Fee.ComGroup))
            {
                FS.HISFC.Models.Fee.ComGroup group = e.Node.Tag as FS.HISFC.Models.Fee.ComGroup;
                //��ʾ������Ϣ
                ShowGroup(group);
                ShowDetail(group.ID);
                this.cmbDept.Enabled = false;
            }
            else
            {
                Clear();
                if (e.Node.Tag.GetType() == typeof(FS.HISFC.Models.Base.Department))
                {
                    this.cmbDept.Tag = (e.Node.Tag as FS.HISFC.Models.Base.Department).ID;
                    this.cmbDept.Enabled = false;
                }
                else
                {
                    this.cmbDept.Enabled = true;
                }

            }
        }

        /// <summary>
        /// �ؼ������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucGroupDetail_Load(object sender, EventArgs e)
        {
            if (this.Tag != null && this.Tag.ToString() != "")
            {
                this.IsDeptOnly = true;
                this.toolBarService.SetToolButtonEnabled("���", true);
                //�������Ϊ��ʿվ
                if (this.Tag.ToString() == "2")
                    this.IsUndrug = true;

                try
                {
                    FS.FrameWork.Management.DataBaseManger data = new FS.FrameWork.Management.DataBaseManger();
                    //��õ�ǰ��½����
                    this.DeptInfo = ((FS.HISFC.Models.Base.Employee)data.Operator).Dept;
                }
                catch
                { }
            }
            //��ʼ��
            ucInit();


        }

        /// <summary>
        /// ѡ���װ��λ����С��λʱ�򴥷�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_ComboSelChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == (int)Cols.Unit)
            {
                FarPoint.Win.Spread.CellType.ComboBoxCellType comboType = (FarPoint.Win.Spread.CellType.ComboBoxCellType)this.fpSpread1_Sheet1.Cells[e.Row, e.Column].CellType;

                string text = e.EditingControl.Text;
                if (((FarPoint.Win.FpCombo)e.EditingControl).SelectedIndex == 0)
                {
                    //����С��λ�շ�
                    Object item = this.fpSpread1_Sheet1.GetTag(e.Row, (int)Cols.Unit);
                    if (item == null)
                    {
                        return;
                    }
                    decimal price = FS.FrameWork.Public.String.FormatNumber(
                        (item as FS.HISFC.Models.Base.Item).Price /
                        (item as FS.HISFC.Models.Base.Item).PackQty, 4);

                    this.fpSpread1_Sheet1.SetValue(e.Row, (int)Cols.Price, price, false);
                }
                else if (((FarPoint.Win.FpCombo)e.EditingControl).SelectedIndex == 1)
                {
                    //����װ��λ�շ�
                    Object item = this.fpSpread1_Sheet1.GetTag(e.Row, (int)Cols.Unit);
                    if (item == null)
                    {
                        return;
                    }

                    decimal price = (item as FS.HISFC.Models.Base.Item).Price;
                    this.fpSpread1_Sheet1.SetValue(e.Row, (int)Cols.Price, price, false);

                }
            }
        }

        #region �Զ����¼�
        /// <summary>
        /// lbDept������ʵ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lbDept_ItemSelected(object sender, EventArgs e)
        {
            SelectDept();
        }

        /// <summary>
        /// ucItemList������ʵ��
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int ItemList_SelectItem(Keys key)
        {
            //FarPointѡ����
            SelectItem();
            return 0;
        }

        #endregion

        #region ����ؼ�Enter�¼�
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtName.Text == null || txtName.Text == "")
                {
                    MessageBox.Show("�������Ʋ���Ϊ��!", "��ʾ");
                    txtName.Focus();
                    return;
                }
                //����ƴ����
                object obj = txtName.Tag;
                if (obj == null || (obj as FS.HISFC.Models.Fee.ComGroup).Name != txtName.Text)
                {
                    FS.HISFC.Models.Base.Spell spell = new FS.HISFC.Models.Base.Spell();
                    FS.HISFC.BizLogic.Manager.Spell spellMgr = new FS.HISFC.BizLogic.Manager.Spell();
                    try
                    {
                        spell = (FS.HISFC.Models.Base.Spell)spellMgr.Get(txtName.Text);
                    }
                    catch { }

                    // [2007/02/08] ԭ���Ĵ���
                    //  if (spell != null && spell.SpellCode.Trim() != "" && spell.SpellCode.Length > 9)
                    //  {
                    //     txtSpell.Text = spell.SpellCode.Substring(8);
                    //  }


                    if (spell.SpellCode.Trim() != "")
                    {
                        if (spell.SpellCode.Length > 9)
                        {
                            txtSpell.Text = spell.SpellCode.Substring(8);
                        }
                        else
                        {
                            txtSpell.Text = spell.SpellCode;
                        }
                    }
                }
                chkValid.Focus();
            }
        }

        private void chkValid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.txtInput.Focus();
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.txtSpell.Focus();
        }

        private void txtSpell_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (this.cmbDept.Enabled == true)
                {
                    this.cmbDept.Focus();
                }
                else
                {
                    this.txtMemo.Focus();
                }
        }

        private void cmbDept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.txtMemo.Focus();
        }

        private void txtMemo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.fpSpread1_Sheet1.RowCount == 0) AddGroupDetail();//���һ��������ϸ
                this.fpSpread1.Focus();
            }
        }
        #endregion

        #endregion

    }
}
