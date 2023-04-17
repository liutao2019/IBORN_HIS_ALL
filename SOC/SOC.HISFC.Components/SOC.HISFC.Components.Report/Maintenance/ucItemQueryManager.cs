using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Report.Maintenance
{
    public partial class ucItemQueryManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucItemQueryManager()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ���ҹ�����
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();

        /// <summary>
        /// ��Ŀ������
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item phaManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ����������
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constmanager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// ��Ŀ��ѯ����ά��������
        /// </summary>
        private FS.HISFC.BizLogic.Manager.ItemInfoQuery queryManager = new FS.HISFC.BizLogic.Manager.ItemInfoQuery();

        /// <summary>
        /// Ȩ����
        /// </summary>
        private FS.HISFC.BizLogic.Manager.PowerLevelManager myPriv = new FS.HISFC.BizLogic.Manager.PowerLevelManager();

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��ѯ���Ͱ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper queryHelper = new FS.FrameWork.Public.ObjectHelper();
   
        /// <summary>
        /// ��Ŀ������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper itemHelper = new FS.FrameWork.Public.ObjectHelper();
        
        /// <summary>
        /// ��ҩƷ��Ŀ����
        /// </summary>
        List<FS.HISFC.Models.Fee.Item.Undrug> undrugList = new List<FS.HISFC.Models.Fee.Item.Undrug>();

        /// <summary>
        /// ҩƷ��Ŀ����
        /// </summary>
        List<FS.HISFC.Models.Pharmacy.Item> drugList = new List<FS.HISFC.Models.Pharmacy.Item>();

        /// <summary>
        /// �����б�
        /// </summary>
        ArrayList deptList = new ArrayList();

        /// <summary>
        /// Ȩ��
        /// </summary>
        ArrayList alPrivs = new ArrayList();

        /// <summary>
        /// ��ҩƷ��Ŀ�б�
        /// </summary>
        DataSet dsItem = new DataSet();

        /// <summary>
        /// ��ҩƷ��Ŀ�б���ͼ
        /// </summary>
        DataView dvItem = new DataView();
        #endregion 

        #region ����

        bool isAllowAllDept = true;
        [Description("�Ƿ�����ά��ȫԺ���п���"),Category("����")]
        public bool IsAllowAllDept
        {
            get
            {
                return this.isAllowAllDept;
            }
            set
            {
                this.isAllowAllDept = value;
            }
        }
        #endregion 

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void InitPan()
        {
            InitData();
        }

        private void InitFpSheet()
        {
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "���";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "��ѯ����";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "���ұ���";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "����";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "��Ŀ����";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "��Ŀ����";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "��Ŀ����";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "˳���";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Ȩ��";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Ȩ������";
            this.neuSpread2_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Azure;
            this.neuSpread2_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread2_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread2_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread2_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread2_Sheet1.Columns.Get(0).Label = "���";
            this.neuSpread2_Sheet1.Columns.Get(0).Width = 42F;
            this.neuSpread2_Sheet1.Columns.Get(1).AllowAutoSort = true;
            this.neuSpread2_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread2_Sheet1.Columns.Get(1).Label = "��ѯ����";
            this.neuSpread2_Sheet1.Columns.Get(1).Width = 86F;
            this.neuSpread2_Sheet1.Columns.Get(2).AllowAutoSort = true;
            this.neuSpread2_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread2_Sheet1.Columns.Get(2).Label = "���ұ���";
            this.neuSpread2_Sheet1.Columns.Get(2).Width = 70F;
            this.neuSpread2_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread2_Sheet1.Columns.Get(3).Label = "����";
            this.neuSpread2_Sheet1.Columns.Get(3).Width = 69F;
            this.neuSpread2_Sheet1.Columns.Get(4).AllowAutoSort = true;
            this.neuSpread2_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread2_Sheet1.Columns.Get(4).Label = "��Ŀ����";
            this.neuSpread2_Sheet1.Columns.Get(4).Width = 78F;
            this.neuSpread2_Sheet1.Columns.Get(5).AllowAutoSort = true;
            this.neuSpread2_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread2_Sheet1.Columns.Get(5).Label = "��Ŀ����";
            this.neuSpread2_Sheet1.Columns.Get(5).Width = 173F;
            this.neuSpread2_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread2_Sheet1.Columns.Get(6).Label = "��Ŀ����";
            this.neuSpread2_Sheet1.Columns.Get(6).Width = 77F;
            this.neuSpread2_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread2_Sheet1.Columns.Get(7).Label = "˳���";
            this.neuSpread2_Sheet1.Columns.Get(7).Width = 46F;
            this.neuSpread2_Sheet1.Columns.Get(8).Label = "Ȩ��";
            this.neuSpread2_Sheet1.Columns.Get(8).Width = 46F;
            this.neuSpread2_Sheet1.Columns.Get(9).Label = "Ȩ������";
            this.neuSpread2_Sheet1.Columns.Get(9).Width = 65F;
            this.neuSpread2_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread2_Sheet1.RowHeader.Columns.Get(0).Width = 23F;
            this.neuSpread2_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Azure;
            this.neuSpread2_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread2_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuSpread2_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread2_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Azure;
            this.neuSpread2_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread2_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread2_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread2_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
        }

        /// <summary>
        /// ���ݳ�ʼ��
        /// </summary>
        private void InitData()
        {        
            ///���ؿ����б�
            deptList = this.deptManager.GetDeptmentAll();
            if (deptList == null || deptList.Count <= 0)
            {
                MessageBox.Show("���ؿ����б�ʧ�ܣ�" + this.deptManager.Err, "��ʾ");
                return;
            }
            else
            {
                FS.HISFC.Models.Base.Department all = new FS.HISFC.Models.Base.Department();
                all.ID = "ALL";
                all.Name = "ȫ������";
                all.UserCode = "ALL";

                deptList.Add(all);
            }

            deptHelper.ArrayObject = deptList;

            ///���ط�ҩƷ��Ŀ�б�
            undrugList = this.itemManager.QueryAllItemList();
            if (undrugList == null)
            {
                MessageBox.Show(this.queryManager.Err);
                return;
            }

            drugList = this.phaManager.QueryItemList();
            if (drugList == null)
            {
                MessageBox.Show(this.queryManager.Err);
                return;
            }

            itemHelper.ArrayObject = new ArrayList(undrugList);
            itemHelper.ArrayObject.AddRange(new ArrayList(drugList));

            ///��ʼ�������
            InitTree();

            ///������ά������Ŀ��ѯ����
            dsItem.Clear();
            if (this.queryManager.QueryItemQueryMend_Const("ALL", "ALL", ref dsItem) == -1)
            {
                MessageBox.Show(queryManager.Err);
                return;
            }
            if (dsItem == null || dsItem.Tables.Count <= 0)
            {
                MessageBox.Show(this.queryManager.Err);
                return;
            }

            dvItem = new DataView(dsItem.Tables[0]);

            SetAmentValueToSheet();

            ArrayList alTemp = this.myPriv.LoadLevel3ByLevel1("81");

            FS.HISFC.Models.Admin.PowerLevelClass3 priv = new FS.HISFC.Models.Admin.PowerLevelClass3();

            priv.Class3Code = "";
            priv.Class3Name = "����";

            alTemp.Add(priv);

            foreach (FS.HISFC.Models.Admin.PowerLevelClass3 pri in alTemp)
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

                obj.ID = pri.ID;
                obj.Name = pri.Name;

                alPrivs.Add(obj);
            }
        }

        /// <summary>
        /// ��ʼ����ѯ����
        /// </summary>
        private void InitTree()
        {
            ///���ز�ѯ�����
            ArrayList queryTypeArray = new ArrayList();

            queryTypeArray = this.constmanager.GetList("ITEMQUERYTYPE");

            if (queryTypeArray == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("���س���[ITEMQUERYTYPE]�б�������"));
                return;
            }

            this.queryHelper.ArrayObject = queryTypeArray;

            System.Windows.Forms.TreeNode root = new TreeNode();
            root.Tag = "All";
            root.Text = "���в�ѯ����";
            this.tvItemQuery.Nodes.Add(root);
            foreach (FS.HISFC.Models.Base.Const cons in queryTypeArray)
            {
                System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode();
                node.Tag = cons;
                node.Text = cons.Name;
                root.Nodes.Add(node);
            }

            ArrayList al = null;
            foreach (System.Windows.Forms.TreeNode node in root.Nodes)
            {
                al = new ArrayList();
                FS.HISFC.Models.Base.Const cons = node.Tag as FS.HISFC.Models.Base.Const;
                al = this.queryManager.GetDeptByItemQueryType(cons.ID);
                if (al == null)
                {
                    MessageBox.Show(this.queryManager.Err);
                    return;
                }

                foreach (FS.FrameWork.Models.NeuObject obj in al)
                {
                    System.Windows.Forms.TreeNode childNode = new System.Windows.Forms.TreeNode();
                    childNode.Tag = obj.ID;
                    childNode.Text = obj.Name;

                    if ((this.itemManager.Operator as FS.HISFC.Models.Base.Employee).IsManager)
                    {
                        node.Nodes.Add(childNode);
                    }
                    else
                    {
                        if ((this.itemManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID == obj.ID)
                        {
                            node.Nodes.Add(childNode);
                        }
                    }
                }
            }
            root.Expand();
        }

        DataSet dsUndrug = new DataSet();
        DataView dvUndrug = new DataView();

        /// <summary>
        /// ��Ŀ�б���˿�
        /// </summary>
        private void ItemListRowsFilter()
        {
            if (string.IsNullOrEmpty(this.txtResultFilter.Text.Trim()))
            {
                //return;
            }

            string _filter = this.txtResultFilter.Text.TrimStart().TrimEnd();   //ȡ����β���ַ�

            try
            {
                if (dvItem != null)
                {
                    dvItem.RowFilter = "��Ŀ���� like '%" + _filter + "%' OR ��Ŀ���� like '%" + _filter + "%' OR ��Ŀ���� like '%" + _filter
                        + "%' OR ���Ҵ��� like '%" + _filter + "%' OR �������� like '%" + _filter + "%'";
                }
                //FarPoint.Win.Spread.SheetView dvSheet = this.neuSpread1_Sheet1.DataSource;

                //dvSheet.row = "��Ŀ���� like '%" + _filter + "%' OR ��Ŀ���� like '%" + _filter + "%' OR ƴ���� like '%" + _filter + "%' OR ����� like '%" + _filter + "%' OR ���ұ��� like '%" + _filter + "%'";
            }
            catch (Exception ex)
            {
                MessageBox.Show("���˳�������" + ex.Message);
                return;
            }

            this.SetAmentValueToSheet();
        }
        #endregion 

        /// <summary>
        /// �������� -- ���в����ٸ���
        /// </summary>
        /// <returns></returns>
        private int Save()
        {
            if (this.neuSpread2_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("û������");
                return 0;
            }

            this.neuSpread2.StopCellEditing();
            this.dsItem.AcceptChanges();

            #region ����
            //Hashtable hsPriv = new Hashtable();

            //for (int i = 0; i < this.neuSpread2_Sheet1.RowCount; i++)
            //{
            //    if (!hsPriv.Contains(this.neuSpread2_Sheet1.Cells[i, 3].Text))
            //    {
            //        hsPriv.Add(this.neuSpread2_Sheet1.Cells[i, 3].Text, this.neuSpread2_Sheet1.Cells[i, 9].Text);
            //    }
            //    else
            //    {
            //        string priv = hsPriv[this.neuSpread2_Sheet1.Cells[i, 3].Text].ToString();

            //        if (this.neuSpread2_Sheet1.Cells[i, 9].Text != priv)
            //        {
            //            MessageBox.Show("ͬһ������ͬһ���ҵ�Ȩ��ֻ����һ�֣����޸ģ�");
            //            SetAmentValueToSheet();
            //            return -1;
            //        }
            //    }
            //}
            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            queryManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            for (int i = 0; i < this.neuSpread2_Sheet1.RowCount; i++)
            {
                FS.HISFC.Models.Base.Const amentObj = new FS.HISFC.Models.Base.Const();
                amentObj = GetRowObj(i);

                if (this.isValidate(amentObj, i) == -1)
                {
                    return -1;
                }

                if (string.IsNullOrEmpty(amentObj.ID) || FS.FrameWork.Function.NConvert.ToInt32(amentObj.ID) == 0)
                {
                    amentObj.ID = GetNewRowNumber();
                    if (this.queryManager.InsertItemInfoAment(amentObj) <= 0)
                    {
                        FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(queryManager.Err);
                        return -1;
                    }
                }
                else
                {
                    if (this.queryManager.UpdateItemInfoAment(amentObj) <= 0)
                    {
                        FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(queryManager.Err);
                        return -1;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            ///ˢ�����б�
            this.tvItemQuery.Nodes.Clear();
            this.InitTree();

            ///���¼�����ά������Ŀ��ѯ����
            dsItem.Clear();
            if (this.queryManager.QueryItemQueryMend_Const("ALL", "ALL", ref dsItem) == -1)
            {
                MessageBox.Show(queryManager.Err);
                return -1;
            }

            dvItem = new DataView(dsItem.Tables[0]);

            SetAmentValueToSheet();

            MessageBox.Show("����ɹ�!");
            return 1;
        }

        #region ����

        /// <summary>
        /// ��ʼ����ά���Ĳ�ѯ��Ϣ
        /// </summary>
        private void SetAmentValueToSheet()
        {
            this.neuSpread2_Sheet1.DataSource = dvItem;

            this.neuSpread2_Sheet1.Columns.Get(3).Locked = true;
            this.neuSpread2_Sheet1.Columns.Get(5).Locked = true;
            SetAmentSheetWidth();

            this.InitFpSheet();
        }

        /// <summary>
        /// ����ά���б��п�
        /// </summary>
        private void SetAmentSheetWidth()
        {
            this.neuSpread2_Sheet1.Columns.Get(0).Width = 70F;
            this.neuSpread2_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread2_Sheet1.Columns.Get(0).Visible = false;

            this.neuSpread2_Sheet1.Columns.Get(1).Width = 200F;
            this.neuSpread2_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread2_Sheet1.Columns.Get(1).Locked = true;
            this.neuSpread2_Sheet1.Columns.Get(2).Width = 80F;
            this.neuSpread2_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            //this.neuSpread2_Sheet1.Columns.Get(2).Visible = false;

            this.neuSpread2_Sheet1.Columns.Get(3).Width = 120F;    //����
            this.neuSpread2_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread2_Sheet1.Columns.Get(3).Locked = true;

            this.neuSpread2_Sheet1.Columns.Get(4).Width = 110F;
            this.neuSpread2_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread2_Sheet1.Columns.Get(5).Width = 160F;
            this.neuSpread2_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            //this.neuSpread2_Sheet1.Columns.Get(5).Locked = true;

            this.neuSpread2_Sheet1.Columns.Get(6).Width = 80F;
            this.neuSpread2_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.neuSpread2_Sheet1.Columns.Get(6).Visible = false;
            this.neuSpread2_Sheet1.Columns.Get(6).Locked = true;
            this.neuSpread2_Sheet1.Columns.Get(7).Width = 70F;
            this.neuSpread2_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.InitFpSheet();
            
        }

        /// <summary>
        /// ������
        /// </summary>
        private void SetSheetColumnLocked()
        {
            this.neuSpread2_Sheet1.Columns.Get(0).Locked = false;
            this.neuSpread2_Sheet1.Columns.Get(1).Locked = false;
            this.neuSpread2_Sheet1.Columns.Get(2).Locked = false;
            this.neuSpread2_Sheet1.Columns.Get(3).Locked = false;
            this.neuSpread2_Sheet1.Columns.Get(4).Locked = false;
            this.neuSpread2_Sheet1.Columns.Get(5).Locked = false;
            this.neuSpread2_Sheet1.Columns.Get(6).Locked = false;
            this.neuSpread2_Sheet1.Columns.Get(7).Locked = false;
        }
        /// <summary>
        /// ��Ч���ж�
        /// </summary>
        /// <returns></returns>
        private int isValidate(FS.FrameWork.Models.NeuObject obj, int i)
        {
            if (obj == null)
            {
                MessageBox.Show("δ�������Ŀ�����豣�棡","��ʾ");
                return -1;
            }
            if (obj.User01 == "")
            {
                MessageBox.Show("��" + (i+1) + "��" + "���ұ��벻��Ϊ�գ���ѡ����ң�","��ʾ");
                return -1;
            }
            if (obj.User02 == "")
            {
                MessageBox.Show("��" + (i+1) + "��" + "��Ŀ���벻��Ϊ�գ���ѡ����Ŀ��", "��ʾ");
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ��ȡ���еı��
        /// </summary>
        /// <returns></returns>
        private string GetNewRowNumber()
        {
            string ruleSeq = this.queryManager.GetSequence();
            return ruleSeq;
        }

        /// <summary>
        /// �����Ŀ
        /// </summary>
        /// <returns></returns>
        private int AddItem()
        {
            if (this.tvItemQuery.SelectedNode == null)
            {
                MessageBox.Show("��ѡ�񱨱�ڵ���ӣ�", "��ʾ");
                return -1;
            }

            if (this.tvItemQuery.SelectedNode.Level == 1)
            {
                int rowcount = this.dsItem.Tables[0].Rows.Count;

                DataRow drv = dsItem.Tables[0].NewRow();

                drv["���"] = 0;
                drv["��ѯ����"] = this.tvItemQuery.SelectedNode.Text;
                drv["���Ҵ���"] = "";
                drv["��������"] = "";
                drv["��Ŀ����"] = "";
                drv["��Ŀ����"] = "";
                drv["��Ŀ����"] = "";
                drv["˳���"] = rowcount;

                dsItem.Tables[0].Rows.Add(drv);

                this.SetSheetColumnLocked();
                this.neuSpread2_Sheet1.Columns.Get(3).Locked = true;
                this.neuSpread2_Sheet1.Columns.Get(5).Locked = true;
            }
            else if (this.tvItemQuery.SelectedNode.Level == 2)
            {
                int rowcount = this.dsItem.Tables[0].Rows.Count;

                DataRow drv = dsItem.Tables[0].NewRow();

                drv["���"] = 0;
                drv["��ѯ����"] = this.tvItemQuery.SelectedNode.Parent.Text;
                drv["���Ҵ���"] = this.tvItemQuery.SelectedNode.Tag.ToString();
                drv["��������"] = this.tvItemQuery.SelectedNode.Text;
                drv["��Ŀ����"] = "";
                drv["��Ŀ����"] = "";
                drv["��Ŀ����"] = "";
                drv["˳���"] = rowcount;

                dsItem.Tables[0].Rows.Add(drv);

                this.SetSheetColumnLocked();
                this.neuSpread2_Sheet1.Columns.Get(2).Locked = true;
                this.neuSpread2_Sheet1.Columns.Get(3).Locked = true;
                this.neuSpread2_Sheet1.Columns.Get(5).Locked = true;
            }
            else
            {
                MessageBox.Show("��ѡ���ӽڵ���ӣ�", "��ʾ");
                return -1;
            }

            SetAmentSheetWidth();
            return 1;
        }

        /// <summary>
        /// ɾ����
        /// </summary>
        /// <returns></returns>
        private int DeleteRow()
        {
            if (this.neuSpread2_Sheet1.ActiveRowIndex < 0)
            {
                MessageBox.Show("��ѡ����!");
                return 0;
            }

            if (MessageBox.Show("�Ƿ�ɾ������Ŀ", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return 0;
            }

            int activeRow = this.neuSpread2_Sheet1.ActiveRowIndex;

            if (!string.IsNullOrEmpty(this.neuSpread2_Sheet1.Cells[activeRow, 0].Text) && !string.Equals(this.neuSpread2_Sheet1.Cells[activeRow,0].Text,"0"))
            {
                FS.FrameWork.Models.NeuObject tempObj = new FS.FrameWork.Models.NeuObject();

                tempObj = GetRowObj(activeRow);

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                queryManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (this.queryManager.DeleteItemInfoAment(tempObj) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("ɾ��ʧ��" + queryManager.Err);
                    return -1;
                }
                this.neuSpread2_Sheet1.RemoveRows(activeRow, 1);

                FS.FrameWork.Management.PublicTrans.Commit();
                return 1;
            }
            else
            {
                this.neuSpread2_Sheet1.RemoveRows(activeRow, 1);
            }

            return 0;
        }

        /// <summary>
        /// ��ȡֵ
        /// </summary>
        /// <param name="rowindex"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Base.Const GetRowObj(int rowindex)
        {
            FS.HISFC.Models.Base.Const amentObj = new FS.HISFC.Models.Base.Const();

            if (rowindex < 0)
            {
                return null;
            }
            amentObj.ID = this.neuSpread2_Sheet1.Cells[rowindex, 0].Text.TrimEnd();
            amentObj.Name = this.queryHelper.GetObjectFromName(this.neuSpread2_Sheet1.Cells[rowindex, 1].Value.ToString()).ID.ToString();
            amentObj.User01 = this.neuSpread2_Sheet1.Cells[rowindex, 2].Text.TrimEnd();
            amentObj.User02 = this.neuSpread2_Sheet1.Cells[rowindex, 4].Text.TrimEnd();
            amentObj.User03 = this.neuSpread2_Sheet1.Cells[rowindex, 5].Text.TrimEnd();
            amentObj.Memo = this.neuSpread2_Sheet1.Cells[rowindex, 7].Text.TrimEnd();
            amentObj.SpellCode = this.neuSpread2_Sheet1.Cells[rowindex, 8].Text;

            return amentObj;
        }

        #endregion 

        #region �¼�
        /// <summary>
        /// ���ش����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucItemQueryManagerNew_Load(object sender, EventArgs e)
        {
            InitPan();
        }

        /// <summary>
        /// ��ά����Ŀ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtResultFilter_TextChanged(object sender, EventArgs e)
        {
            ItemListRowsFilter();
        }

        /// <summary>
        /// ��Ӿ�����Ŀ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread2_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            FS.FrameWork.Models.NeuObject obj;

            if (e.Column == 2)
            {
                if (deptList != null && deptList.Count > 0)
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    FS.FrameWork.WinForms.Classes.Function.ChooseItem(deptList, ref obj);
                    if (obj.ID == "")
                    {
                        this.neuSpread2_Sheet1.SetValue(e.Row, 2, obj.ID);
                        this.neuSpread2_Sheet1.SetValue(e.Row, 3, obj.Name);
                        return;
                    }
                    else
                    {
                        if ((this.deptManager.Operator as FS.HISFC.Models.Base.Employee).IsManager)
                        {
                            this.neuSpread2_Sheet1.SetValue(e.Row, 2, obj.ID);
                            this.neuSpread2_Sheet1.SetValue(e.Row, 3, obj.Name);
                        }
                        else
                        {
                            if ((this.deptManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID != obj.ID)
                            {
                                MessageBox.Show("��ѡ���½���ң�" + (this.deptManager.Operator as FS.HISFC.Models.Base.Employee).Dept.Name);
                                return;
                            }
                            else
                            {
                                this.neuSpread2_Sheet1.SetValue(e.Row, 2, obj.ID);
                                this.neuSpread2_Sheet1.SetValue(e.Row, 3, obj.Name);
                            }
                        }
                    }
                }
            }
            if (e.Column == 4)
            {
                ArrayList al = new ArrayList(undrugList);
                al.AddRange(new ArrayList(drugList));

                if (al != null && al.Count > 0)
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    FS.FrameWork.WinForms.Classes.Function.ChooseItem(al, ref obj);
                    if (obj.ID == "")
                    {
                        this.neuSpread2_Sheet1.SetValue(e.Row, 4, obj.ID);
                        this.neuSpread2_Sheet1.SetValue(e.Row, 5, obj.Name);
                        this.neuSpread2_Sheet1.SetValue(e.Row, 6, "");
                        return;
                    }
                    FS.HISFC.Models.Base.ISpell spell = obj as FS.HISFC.Models.Base.ISpell;
                    DataRow[] drCol = dsItem.Tables[0].Select("���Ҵ��� = '" + this.neuSpread2_Sheet1.GetValue(e.Row, 2).ToString()
                        + "' and ��Ŀ���� = '" + spell.UserCode + "' and ��ѯ���� = '" + this.neuSpread2_Sheet1.GetValue(e.Row, 1).ToString() + "'");
                    if (drCol.Length > 0)
                    {
                        MessageBox.Show("�Ѿ���ӵ���");
                        return;
                    }
                    this.neuSpread2_Sheet1.SetValue(e.Row, 4, obj.ID);
                    this.neuSpread2_Sheet1.SetValue(e.Row, 5, obj.Name);
                    this.neuSpread2_Sheet1.SetValue(e.Row, 6, spell.UserCode);
                }
            }

            if (e.Column == 8)
            {
                if (this.alPrivs != null && alPrivs.Count > 0)
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    FS.FrameWork.WinForms.Classes.Function.ChooseItem(alPrivs, ref obj);

                    if (obj != null)
                    {
                        this.neuSpread2_Sheet1.SetValue(e.Row, 8, obj.ID);
                        this.neuSpread2_Sheet1.SetValue(e.Row, 9, obj.Name);
                    }
                }
            }
        }

        /// <summary>
        /// �������ڵ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvItemQuery_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                ///������ά������Ŀ��ѯ����
                dsItem.Clear();
                if (this.queryManager.QueryItemQueryMend_Const("ALL", "ALL", ref dsItem) == -1)
                {
                    MessageBox.Show(queryManager.Err);
                    return;
                }

                dvItem = new DataView(dsItem.Tables[0]);

                SetAmentValueToSheet();
            }
            else if (e.Node.Level == 1)
            {
                dvItem.RowFilter = "��ѯ���� = '" + e.Node.Text + "'";

                SetAmentValueToSheet(); 
            }
            else if (e.Node.Level == 2)
            {
                dvItem.RowFilter = "��ѯ���� = '" + e.Node.Parent.Text + "' and �������� = '" + e.Node.Text + "'";

                SetAmentValueToSheet();
            }
        }

        #endregion

        private void tbAdd_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        private void tbDel_Click(object sender, EventArgs e)
        {
            DeleteRow();
        }

        private void tbSave_Click(object sender, EventArgs e)
        {
            Save();
        }
    }      
}
