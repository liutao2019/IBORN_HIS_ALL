using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.UFC.DrugStore.Outpatient
{
    class ClinicTreeManager
    {
        /// <summary>
        /// ���������ʼ��
        /// </summary>
        /// <param name="addTree">��������� ���������ӽڵ�</param>
        /// <param name="operTree">���ݲ����� �Ը��������ݽ��в���</param>
        /// <param name="nextTree">���ݺ���� ���ݲ������ƶ�Ŀ����</param>
        /// <param name="funMode">����ģ������</param>
        /// <param name="operDateState">����������ڵ�����״̬</param>
        internal ClinicTreeManager(TabPage addTab,TreeView addTree,TabPage operTab,TreeView operTree,TabPage nextTab,TreeView nextTree)
        {
            this.AddTab = addTab;
            this.AddTree = addTree;

            this.OperTab = operTab;
            this.OperTree = operTree;

            this.NextTab = nextTab;
            this.NextTree = nextTree;

            Neusoft.NFC.Management.DataBaseManger dataBaseManager = new Neusoft.NFC.Management.DataBaseManger();
            this.minQueryDate = dataBaseManager.GetDateTimeFromSysDateTime().Date;
        }

        #region ����

        /// <summary>
        /// ���TabPageҳ
        /// </summary>
        private TabPage AddTab = null;

        /// <summary>
        /// ���������
        /// </summary>
        private TreeView AddTree = null;

        /// <summary>
        /// ����TabPage
        /// </summary>
        private TabPage OperTab = null;        

        /// <summary>
        /// ���ݲ�����
        /// </summary>
        private TreeView OperTree = null;

        /// <summary>
        /// ���TabPage
        /// </summary>
        private TabPage NextTab = null;

        /// <summary>
        /// ���ݺ����
        /// </summary>
        private TreeView NextTree = null;

        /// <summary>
        /// ����ģ������
        /// </summary>
        private OutpatientFun funMode = OutpatientFun.Drug;

        /// <summary>
        /// ����������ڵ�����״̬
        /// </summary>
        private string operDataState = "0";

        /// <summary>
        /// ��ѯʱ������ʱ������
        /// </summary>
        private DateTime minQueryDate = System.DateTime.MinValue;

        #endregion

        #region ����

        /// <summary>
        /// �б�ڵ�
        /// </summary>
        public int NodeCount
        {
            get
            {
                return this.AddTree.Nodes.Count;
            }
        }

        /// <summary>
        /// ����������ڵ�����
        /// </summary>
        public string OperDataState
        {
            get
            {
                return this.operDataState;
            }
        }

        /// <summary>
        /// ��ѯʱ������ʱ������
        /// </summary>
        public DateTime MinQueryDate
        {
            get
            {
                if (this.funMode != null && this.funMode == OutpatientFun.Send)
                    return this.minQueryDate.AddSeconds(1);
                else
                    return this.minQueryDate;
            }
        }

        #endregion

        /// <summary>
        /// ��ȡ����Ĵ������������������ҩʱ��
        /// </summary>
        /// <param name="drugRecipeAl">������������</param>
        private void GetMinDrugedDate(ArrayList drugRecipeAl)
        {
            if (this.funMode == OutpatientFun.Drug || this.funMode == OutpatientFun.Back)
                return;

            if (drugRecipeAl.Count <= 0)
                return;

            this.minQueryDate = System.DateTime.MinValue;
            foreach (Neusoft.HISFC.Object.Pharmacy.DrugRecipe info in drugRecipeAl)
            {
                if (info.DrugedOper.OperTime > this.minQueryDate)
                {
                    this.minQueryDate = info.DrugedOper.OperTime;
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
            if (this.AddTree == null)
                return;

            if (!isSupplemental)
                this.AddTree.Nodes.Clear();

            this.GetMinDrugedDate(alDrugRecipe);

            foreach (Neusoft.HISFC.Object.Pharmacy.DrugRecipe drugRecipe in alDrugRecipe)
            {
                TreeNode node = new TreeNode();
                node.Text = drugRecipe.PatientName;
                node.ImageIndex = 3;
                node.SelectedImageIndex = 4;
                node.Tag = drugRecipe;
                this.AddTree.Nodes.Add(node);
            }
        }

        /// <summary>
        /// ���ڵ�λ��ת�� ��AddTree�ڽڵ�ת�Ƶ�NextTree
        /// </summary>
        public void ChangeNode()
        {
            if (this.AddTree == null)
                return;

            TreeNode tempNode = this.AddTree.SelectedNode;
            if (tempNode != null)
            {
                this.AddTree.Nodes.Remove(tempNode);
                if (this.NextTree != null)
                    this.NextTree.Nodes.Add(tempNode);
            }
        }

        /// <summary>
        /// ɾ�� ��OperTree��ɾ���ڵ�
        /// </summary>
        public void DelNode()
        {
            if (this.OperTree == null)
                return;

            TreeNode tempNode = this.OperTree.SelectedNode;
            if (tempNode != null)
            {
                this.OperTree.Nodes.Remove(tempNode);              
            }
        }

        /// <summary>
        /// ���
        /// </summary>
        public void Clear()
        {
            this.AddTree.Nodes.Clear();
            this.NextTree.Nodes.Clear();
            this.OperTree.Nodes.Clear();
        }

        /// <summary>
        /// �ڵ�ѡ���ƶ�
        /// </summary>
        /// <param name="isDown">�Ƿ������ƶ�</param>
        public void SelectNextNode(bool isDown)
        {
           
        }

    }
}
