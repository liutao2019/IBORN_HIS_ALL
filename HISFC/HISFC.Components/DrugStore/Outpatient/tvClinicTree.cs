using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;

namespace FS.HISFC.Components.DrugStore.Outpatient
{
    /// <summary>
    /// [��������: �����䷢ҩ�������]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11]<br></br>
    /// <�޸ļ�¼ 
    ///		
    ///  />
    /// </summary>
    public partial class tvClinicTree : FS.HISFC.Components.Common.Controls.baseTreeView
    {
        public tvClinicTree()
        {
            InitializeComponent();

            this.ImageList = this.groupImageList;
        }

        public tvClinicTree(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            this.ImageList = this.groupImageList;
        }

        /// <summary>
        /// ����������ݵ����ڵ�״̬
        /// </summary>
        private string state = "0";

        /// <summary>
        /// ���ڵ����ڵ�TabPage
        /// </summary>
        private System.Windows.Forms.TabPage parentTab = null;

        /// <summary>
        /// ����������ݵ����ڵ�״̬
        /// </summary>
        public string State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }

        /// <summary>
        /// ���ڵ����ڵ�TabPage
        /// </summary>
        public System.Windows.Forms.TabPage ParentTab
        {
            get
            {
                if (this.parentTab == null)
                    this.parentTab = new System.Windows.Forms.TabPage();
                return this.parentTab;
            }
            set
            {
                this.parentTab = value;
            }
        }

        /// <summary>
        /// ��ʾ�����б� ��AddTree����������
        /// </summary>
        /// <param name="alDrugRecipe">�б�����</param>
        /// <param name="isSupplemental">�Ƿ����б���׷����ʾ</param>
        /// <param name="isAutoShow">�Ƿ��Զ�ѡ�������ڵ�</param>
        public void ShowList(System.Windows.Forms.TreeNode rootNode,ArrayList alDrugRecipe, bool isSupplemental, bool isAutoShow)
        {
            if (!isSupplemental)
            {
                this.Nodes.Clear();
            }

            if (rootNode != null)
            {
                this.Nodes.Add(rootNode);
            }

            foreach (FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe in alDrugRecipe)
            {
                System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode();

                //{DF70D8FF-A1DD-421b-8E4A-4637745F1927}
                //�����ڵ���Ӽ�ֵ
                node.Name = drugRecipe.RecipeNO;

                node.Text = drugRecipe.PatientName;
                node.ImageIndex = 2;
                node.SelectedImageIndex = 4;
                node.Tag = drugRecipe;
                if (rootNode != null)
                {
                    rootNode.Nodes.Add(node);
                }
                else
                {
                    this.Nodes.Add(node);
                }
            }

            if (isAutoShow)
            {
                if (this.Nodes.Count > 0)
                {
                    this.SelectedNode = this.Nodes[this.Nodes.Count - 1];
                }
            }
        }

        /// <summary>
        /// ��ʾ�����б� ��AddTree����������
        /// </summary>
        /// <param name="alDrugRecipe">�б�����</param>
        /// <param name="isSupplemental">�Ƿ����б���׷����ʾ</param>
        /// <param name="isAutoShow">�Ƿ��Զ�ѡ�������ڵ�</param>
        public void ShowList(ArrayList alDrugRecipe, bool isSupplemental,bool isAutoShow)
        {
            if (!isSupplemental)
            {
                this.Nodes.Clear();
            }

            foreach (FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe in alDrugRecipe)
            {
                System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode();

                //{DF70D8FF-A1DD-421b-8E4A-4637745F1927}
                //�����ڵ���Ӽ�ֵ
                node.Name = drugRecipe.RecipeNO;

                node.Text = drugRecipe.PatientName;
                node.ImageIndex = 2;
                node.SelectedImageIndex = 4;
                node.Tag = drugRecipe;
                this.Nodes.Add(node);
            }

            if (isAutoShow)
            {
                if (this.Nodes.Count > 0)
                {
                    this.SelectedNode = this.Nodes[this.Nodes.Count - 1];
                }
            }
        }

         /// <summary>
        /// ��ʾ�����б� ��AddTree����������
        /// </summary>
        /// <param name="alDrugRecipe">�б�����</param>
        /// <param name="isSupplemental">�Ƿ����б���׷����ʾ</param>
        public void ShowList(ArrayList alDrugRecipe, bool isSupplemental)
        {
            this.ShowList(alDrugRecipe, isSupplemental, false);
        }

        /// <summary>
        /// �ڵ�ѡ���ƶ�
        /// </summary>
        /// <param name="isDown">�Ƿ������ƶ�</param>
        public void SelectNext(bool isDown)
        {
            if (this.Nodes.Count <= 0)
                return;
            if (this.SelectedNode == null)
            {
                this.SelectedNode = this.Nodes[0];
                return;
            }
            int iIndex = this.SelectedNode.Index;
            if (isDown)
            {
                if (iIndex == this.Nodes.Count - 1)
                {
                    this.SelectedNode = this.Nodes[0];
                }
                else
                {
                    this.SelectedNode = this.Nodes[iIndex + 1];
                }
            }
            else
            {
                if (iIndex == 0)
                {
                    this.SelectedNode = this.Nodes[this.Nodes.Count - 1];
                }
                else
                {
                    this.SelectedNode = this.Nodes[iIndex - 1];
                }
            }
        }
    }
}
