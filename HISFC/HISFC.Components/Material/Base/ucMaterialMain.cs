using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.WinForms.Forms;

namespace FS.HISFC.Components.Material.Base
{
    public partial class ucMaterialMain : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucMaterialMain()
        {
            InitializeComponent();
        }

        #region ��������

        /// <summary>
        /// ����ʵ��
        /// </summary>
        private FS.HISFC.BizLogic.Material.MetItem Item = new FS.HISFC.BizLogic.Material.MetItem();

        /// <summary>
        /// �����ÿؼ�
        /// </summary>
        private FS.HISFC.Components.Common.Controls.ucSetColumn ucSetColumn = null;

        /// <summary>
        /// XML�ļ�·��
        /// </summary>
        private string filePath = FS.FrameWork.WinForms.Classes.Function.SettingPath + "\\MaterialItem.xml";
        
        //��������
        private string filterTree = "0"; //���ͽڵ�ѡ���������
        private string filterInput = " 1=1 "; //�������������
        private string filterValid = " 1=1 ";

        /// <summary>
        /// �ֿ����
        /// </summary>
        public string storageCode;

        /// <summary>
        /// Ȩ��
        /// </summary>
        private bool isEditExpediency;

        #endregion

        #region ��ʼ�����οؼ�

        /// <summary>
        /// �õ��¼���Ŀ��Ϣ
        /// </summary>
        /// <param name="parm">������Ŀ����</param>
        /// <returns>�¼���Ŀ��Ϣ����</returns>
        public ArrayList GetHasChildren(string parm)
        {
            FS.HISFC.BizLogic.Material.Baseset matBase = new FS.HISFC.BizLogic.Material.Baseset();

            return matBase.QueryKindAllByPreID(parm);
        }


        /// <summary>
        /// ���TreeView�Ľڵ���Ϣ
        /// </summary>
        /// <param name="preID">�ϼ���Ŀ����</param>
        /// <param name="curNode">�ϼ��ڵ�</param>
        public void InsertNode(System.Windows.Forms.TreeNode node, string preID, string storagecode)
        {
            ArrayList al = new ArrayList();

            try
            {
                //ȡ�ӽڵ���Ϣ
                al = this.GetHasChildren(preID);

                if (al.Count <= 0)
                {
                    return;
                }

                //����ӽڵ���Ϣ
                foreach (FS.HISFC.Models.Material.MaterialKind materialKind in al)
                {

                    TreeNode kindTree = new TreeNode(materialKind.Name, 2, 1);
                    kindTree.ImageIndex = 0;
                    kindTree.SelectedImageIndex = 0;

                    kindTree.Tag = materialKind.ID;

                    node.Nodes.Add(kindTree);

                    if (!materialKind.EndGrade)
                    {
                        this.InsertNode(kindTree, materialKind.ID, storagecode);
                    }

                }
            }
            catch { }
        }

        /// <summary>
        /// ��ʼ��TreeView
        /// </summary>
        public void InitTreeView()
        {
            this.neuTreeView1.ImageList = this.neuTreeView1.groupImageList;

            TreeNode title = new TreeNode("ȫ����Ŀ��Ϣ", 1, 2);
            title.ImageIndex = 4;

            title.Tag = "0";

            //��Ӹ��ڵ�
            this.neuTreeView1.Nodes.Add(title);

            ArrayList al = new ArrayList();

            try
            {
                //ȡĬ��һ����Ŀ
                al = this.GetHasChildren("0");

                if (al.Count > 0)
                {
                    this.InsertNode(title, "0", storageCode);
                }
            }
            catch { }

            this.neuTreeView1.ExpandAll();
        }

        #endregion

        #region ��ʼ��������

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override ToolBarService OnInit(object sender, object NeuObject, object param)
        {
            toolBarService.AddToolButton("����", "������Ʒ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            toolBarService.AddToolButton("�޸�", "�޸ĵ�ǰ��Ʒ��Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            toolBarService.AddToolButton("ɾ��", "ɾ����ǰ��Ʒ��Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            toolBarService.AddToolButton("����", "��������ʾ��ʽ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S����, true, false, null);
 
            return this.toolBarService;
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    if (this.isEditExpediency)
                    {
                        //����
                        this.ucMaterialQuery1.MatKind = this.filterTree;
                        this.ucMaterialQuery1.storageCode = this.storageCode;
                        this.ucMaterialQuery1.Add();
                    }
                    else
                    {
                        MessageBox.Show("��������Ȩ��");
                    }
                    break;
                case "�޸�":
                    if (this.isEditExpediency)
                    {
                        this.ucMaterialQuery1.Modify();
                    }
                    else
                    {
                        MessageBox.Show("�����޸�Ȩ��");
                    }
                    break;
                case "ɾ��":
                    if (this.isEditExpediency)
                    {
                        this.ucMaterialQuery1.Delete();
                    }
                    else
                    {
                        MessageBox.Show("����ɾ��Ȩ��");
                    }
                    break;
                case "����":

                    SetupColumn();
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        public override int Export(object sender, object NeuObject)
        {
            this.ExportInfo();
            return 1;
        }

        protected override int OnPrint(object sender, object NeuObject)
        {
            return 1;
        }

        #endregion

        #region ����

        /// <summary>
        /// ������ʾ��
        /// </summary>
        public void SetupColumn()
        {
            this.ucSetColumn = new FS.HISFC.Components.Common.Controls.ucSetColumn();
            this.ucSetColumn.FilePath = filePath;
            this.ucSetColumn.DisplayEvent += new EventHandler(this.uc_GoDisplay);
            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��ʾ����";
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucSetColumn);
        }


        /// <summary>
        /// ���ù�������,��������
        /// </summary>
        private void SetFilter()
        {
            //��Ϲ�������
            string filter;

            if (this.filterTree == "0")
            {
                filter = this.filterInput;
            }
            else
            {
                filter = "��Ŀ���� like '" + this.filterTree + "%'and " + this.filterInput;
            }

            //��������
            this.ucMaterialQuery1.SetFilter(filter);
        }


        /// <summary>
        /// ��������ΪExcel��ʽ
        /// </summary>
        private void ExportInfo()
        {
            try
            {
                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel (*.xls)|*.*";
                DialogResult result = dlg.ShowDialog();

                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;
                    this.ucMaterialQuery1.fpMaterialQuery.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region �¼�

        private void ucMaterialMain_Load(object sender, EventArgs e)
        {
            //this.isEditExpediency = false;
            //this.ucMaterialQuery1.EditExpediency = false;

            //FS.FrameWork.Models.NeuObject testPrivDept = new FS.FrameWork.Models.NeuObject();
            //int parma = FS.HISFC.Components.Common.Classes.Function.ChoosePivDept("0501", ref testPrivDept);
            //if (parma == -1)            //��Ȩ��
            //{
            //    MessageBox.Show("���޴˴��ڲ���Ȩ��");
            //    return;
            //}
            //else if (parma == 0)       //�û�ѡ��ȡ��
            //{
            //    return;
            //}

            //this.isEditExpediency = true;
            //this.ucMaterialQuery1.EditExpediency = true;

            //this.storageCode = testPrivDept.ID;

            //base.OnStatusBarInfo(null, "�������ң� " + testPrivDept.Name);

            this.InitTreeView();

            List<FS.HISFC.Models.Material.MaterialItem> al = new List<FS.HISFC.Models.Material.MaterialItem>();
            al = Item.QueryMetItemAll();			
            if (al == null)
            {
                MessageBox.Show(this.Item.Err, "������ʾ");
                return;
            }

            if (this.ucMaterialQuery1.InitDataSet(al) != 1)
            {
                return;
            }
        }

        private void uc_GoDisplay(object sender, EventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.ucMaterialQuery1.fpMaterialQuery_Sheet1, filePath);
            List<FS.HISFC.Models.Material.MaterialItem> al = new List<FS.HISFC.Models.Material.MaterialItem>();
            al = this.Item.QueryMetItemAll();//.GetMetItemList();
            if (al == null)
            {
                MessageBox.Show(this.Item.Err, "������ʾ");
                return;
            }
            if (this.ucMaterialQuery1.InitDataSet(al) != 1) return;

        }

        private void neuTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //���ù�������
            this.filterTree = e.Node.Tag.ToString();

            //��������
            this.SetFilter();
        }

        private void txtInputCode_TextChanged(object sender, EventArgs e)
        {
            if (this.ucMaterialQuery1.DefaultDataView.Table.Rows.Count == 0) return;

            //ȡ������
            string queryCode = this.txtInputCode.Text;
            if (this.chbMisty.Checked)
            {
                queryCode = "%" + queryCode + "%";
            }
            else
            {
                queryCode = queryCode + "%";
            }

            //���ù�������
            this.filterInput = "((ƴ���� LIKE '" + queryCode + "') OR " +
                "(����� LIKE '" + queryCode + "') OR " +
                "(�Զ����� LIKE '" + queryCode + "') OR " +
                "(��Ʒ���� LIKE '" + queryCode + "'))";

            //��������
            this.SetFilter();
        }

        private void txtInputCode_KeyDown(object sender, KeyEventArgs e)
        {
            //�ϼ�ͷѡ����һ����¼
            if (e.KeyCode == Keys.Up)
            {
                if (this.ucMaterialQuery1.fpMaterialQuery_Sheet1.ActiveRowIndex > 0)
                {
                    this.ucMaterialQuery1.fpMaterialQuery_Sheet1.ActiveRowIndex--;
                    return;
                }
            }

            //�¼�ͷѡ����һ����¼
            if (e.KeyCode == Keys.Down)
            {
                if (this.ucMaterialQuery1.fpMaterialQuery_Sheet1.ActiveRowIndex < ucMaterialQuery1.fpMaterialQuery_Sheet1.RowCount)
                {
                    this.ucMaterialQuery1.fpMaterialQuery_Sheet1.ActiveRowIndex++;
                    return;
                }
            }
        }

        private void txtInputCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                //ѡ���ı�
                this.txtInputCode.SelectAll();
                //�޸���Ʒ��Ϣ
                this.ucMaterialQuery1.Modify();
            }
        }

        #endregion

        #region IPreArrange ��Ա

        public int PreArrange()
        {
            this.isEditExpediency = false;
            this.ucMaterialQuery1.EditExpediency = false;

            FS.FrameWork.Models.NeuObject testPrivDept = new FS.FrameWork.Models.NeuObject();
            int parma = FS.HISFC.Components.Common.Classes.Function.ChoosePivDept("0501", ref testPrivDept);
            if (parma == -1)            //��Ȩ��
            {
                MessageBox.Show("���޴˴��ڲ���Ȩ��");
                return -1;
            }
            else if (parma == 0)       //�û�ѡ��ȡ��
            {
                return -1;
            }

            this.isEditExpediency = true;
            this.ucMaterialQuery1.EditExpediency = true;

            this.storageCode = testPrivDept.ID;

            base.OnStatusBarInfo(null, "�������ң� " + testPrivDept.Name);
            return 1;
        }

        #endregion
    }
}
