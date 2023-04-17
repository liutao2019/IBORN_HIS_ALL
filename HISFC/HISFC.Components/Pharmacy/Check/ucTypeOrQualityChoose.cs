using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Pharmacy.Check
{
    /// <summary>
    /// [��������: �����̵�����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// <�޸�>
    ///     <ʱ��>2007-07-16</ʱ��>
    ///     <�޸���>Liangjz</�޸���>
    ///     <�޸�����>
    ///             1 ����ȫ�̹���
    ///             2 ��������ʱ,���Ӷ�ͣ��ҩƷ/���Ϊ��ҩƷ�Ĵ���.
    ///     </�޸�����>
    /// </�޸�>
    /// </summary>
    public partial class ucTypeOrQualityChoose : UserControl
    {
        public ucTypeOrQualityChoose()
        {
            InitializeComponent();
        }

        public ucTypeOrQualityChoose(ArrayList alList) : this()
        {
 
        }

        public ucTypeOrQualityChoose(bool isTypeQuality) : this()
        {
            this.IsTypeQuality = isTypeQuality;

            this.ShowTypeQuality();
        }

        #region �����

        /// <summary>
        /// ѡ���ҩƷ���/ҩƷ����
        /// </summary>
        private List<FS.FrameWork.Models.NeuObject> drugTypeList = new List<FS.FrameWork.Models.NeuObject>();

        /// <summary>
        /// �Ƿ���ʾҩƷ���/ҩƷ����
        /// </summary>
        private bool IsTypeQuality = true;

        /// <summary>
        /// ������� 0 ȡ�� 1 ȷ�� 2 ȫ��ҩƷ
        /// </summary>
        private string resultFlag = "1";

        /// <summary>
        /// ѡ���ҩƷ���
        /// </summary>
        private string drugType = "";

        /// <summary>
        /// ѡ���ҩƷ����
        /// </summary>
        private string drugQuality = "";

        #endregion

        #region ����

        /// <summary>
        /// ѡ���ҩƷ���/ҩƷ����
        /// </summary>
        public List<FS.FrameWork.Models.NeuObject> DrugTypeList
        {
            get
            {
                return this.drugTypeList;
            }
            set
            {
                this.drugTypeList = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public string ResultFlag
        {
            get
            {
                return this.resultFlag;
            }
            set
            {
                this.resultFlag = value;
            }
        }

        /// <summary>
        /// ѡ���ҩƷ���
        /// </summary>
        public string DrugType
        {
            get
            {
                return this.drugType;
            }
            set
            {
                this.drugType = value;
            }
        }

        /// <summary>
        /// ѡ���ҩƷ����
        /// </summary>
        public string DrugQuality
        {
            get
            {
                return this.drugQuality;
            }
            set
            {
                this.drugQuality = value;
            }
        }

        /// <summary>
        /// �Ƿ�Կ��Ϊ���ҩƷ���з��ʴ���
        /// </summary>
        public bool IsCheckZeroStock
        {
            get
            {
                return this.ckZeroState.Checked;
            }
        }

        /// <summary>
        /// �Ƿ��ͣ��ҩƷ(���ⷿͣ��)���з��ʴ���
        /// </summary>
        public bool IsCheckStopDrug
        {
            get
            {
                return this.ckValidDrug.Checked;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʾҩƷ���ҩƷ�����б�
        /// </summary>
        public virtual void ShowTypeQuality()
        {
            this.tvObject.CheckBoxes = true;

            FS.HISFC.BizLogic.Manager.Constant constant = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList al;

            TreeNode typeParent = null;
            TreeNode typeNode = null;

            //����ҩƷ����б�
            typeParent = new TreeNode();
            typeParent.Text = "ҩƷ���";
            typeParent.Tag = "";
            typeParent.ImageIndex = 0;
            typeParent.SelectedImageIndex = 0;
            this.tvObject.Nodes.Add(typeParent);

            al = constant.GetList("ITEMTYPE");
            foreach (FS.HISFC.Models.Base.Const obj in al)
            {
                typeNode = new TreeNode();
                typeNode.Text = obj.Name;
                typeNode.Tag = obj.ID;
                typeNode.ImageIndex = 0;
                typeNode.SelectedImageIndex = 0;
                this.tvObject.Nodes[0].Nodes.Add(typeNode);
            }
            //����ҩƷ�����б�
            typeParent = new TreeNode();
            typeParent.Text = "ҩƷ����";
            typeParent.Tag = "";
            typeParent.ImageIndex = 0;
            typeParent.SelectedImageIndex = 0;
            this.tvObject.Nodes.Add(typeParent);

            al = constant.GetList("DRUGQUALITY");
            foreach (FS.HISFC.Models.Base.Const obj in al)
            {
                typeNode = new TreeNode();
                typeNode.Text = obj.Name;
                typeNode.Tag = obj.ID;
                typeNode.ImageIndex = 0;
                typeNode.SelectedImageIndex = 0;
                this.tvObject.Nodes[1].Nodes.Add(typeNode);
            }

            this.tvObject.ExpandAll();
        }

        /// <summary>
        /// ���ݴ�������飬��ʾ��tvObject��
        /// </summary>
        /// <param name="arrayObject">neuObject����</param>
        public virtual void ShowList(ArrayList alList)
        {
            //��Ӹ����ڵ�
            TreeNode nodeParent = new TreeNode();
            nodeParent.Text = "ȫ��";
            nodeParent.Tag = "";
            nodeParent.ImageIndex = 0;
            nodeParent.SelectedImageIndex = 0;
            this.tvObject.Nodes.Add(nodeParent);

            foreach (FS.FrameWork.Models.NeuObject obj in alList)
            {
                TreeNode node = new TreeNode();
                node.Text = obj.Name;
                node.Tag = obj;
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
                nodeParent.Nodes.Add(node);
            }
        }

        /// <summary>
        /// ��treeview��ѡ�е����ݱ��浽������
        /// </summary>
        public void Save()
        {
            //��������е����ݡ�
            this.drugTypeList.Clear();

            if (this.tvObject.Nodes.Count == 0)
                return;

            foreach (TreeNode node in this.tvObject.Nodes)
            {
                foreach (TreeNode tn in node.Nodes)
                {
                    //��ѡ�е���浽������
                    if (tn.Checked) this.drugTypeList.Add(tn.Tag as FS.FrameWork.Models.NeuObject);
                }
            }
        }

        /// <summary>
        /// ��ҩƷ������ҩƷ����ѡ�񷵻��ַ���
        /// </summary>
        public void SaveForTypeQuality()
        {
            //�������
            this.drugType = "AAAA";
            this.drugQuality = "AAAA";
            if (this.tvObject.Nodes.Count == 0) 
                return;

            foreach (TreeNode node in this.tvObject.Nodes[0].Nodes)
            {
                if (node.Checked)
                {
                    if (this.drugType == "AAAA")
                        this.drugType = "";
                    this.drugType += node.Tag.ToString() + "','";
                }
            }
            foreach (TreeNode node in this.tvObject.Nodes[1].Nodes)
            {
                if (node.Checked)
                {
                    if (this.drugQuality == "AAAA")
                        this.drugQuality = "";
                    this.drugQuality += node.Tag.ToString() + "','";
                }
            }
        }

        /// <summary>
        /// �ر�
        /// </summary>
        public void Close()
        {
            if (this.ParentForm != null)
                this.ParentForm.Close();
        }

        #endregion

        #region �¼�

        private void tvObject_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //���ѡ�е��Ǹ��ڵ㣬��ѡ���������ӽڵ�
            if (e.Node.Parent == null)
            {
                foreach (TreeNode node in e.Node.Nodes)
                {
                    if (node.Checked != e.Node.Checked) node.Checked = e.Node.Checked;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.IsTypeQuality)
            {
                this.SaveForTypeQuality();
                this.resultFlag = "1";
            }
            else
            {
                this.Save();
            }

            this.Close();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.resultFlag = "0";

            this.Close();
        }


        private void btnAll_Click(object sender, EventArgs e)
        {
            this.resultFlag = "2";

            this.Close();
        }


        #endregion         
    }
}
