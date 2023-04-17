using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.EPR.QC
{
    /// <summary>
    /// �����ʿ������ؼ�
    /// ��������2008-4-1����
    /// </summary>
    public partial class ucQCSetting : UserControl
    {
        public ucQCSetting()
        {
            InitializeComponent();
        }
        /// <summary>
        /// /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="alQCConditions">�����ʿ�����</param>
        /// <param name="alPreSelectedCondition">��ǰѡ����ʿ�����</param>
        /// <param name="alSelectedCondition">����ѡ�����ʿ�����</param>
        public ucQCSetting(ArrayList alQCConditions,ArrayList alPreSelectedCondition,ref ArrayList alSelectedCondition)
        {
            InitializeComponent();
            this.alPreSelectedCondtion = alPreSelectedCondition;
            this.alQCConditions = alQCConditions;
            alSelectedCondition = this.alSelectedCondition;
            this.init();
        }
        /// <summary>
        /// �ʿ�����
        /// </summary>
        private ArrayList alQCConditions;
        /// <summary>
        /// �Ƿ��в�ѯ���Ľڵ�
        /// </summary>
        private bool hasNode = false;
        /// <summary>
        /// ��ǰ���ҵ�����
        /// </summary>
        private int currentIndex = 0;
        /// <summary>
        /// ��ʾF3�����ظ�ǰ��Ĳ���
        /// </summary>
        private ToolTip tt = new ToolTip();
        /// <summary>
        /// ѡ�������
        /// </summary>
        private ArrayList alSelectedCondition = new ArrayList();
        /// <summary>
        /// �˴�ѡ��ǰ��ѡ�������
        /// </summary>
        private ArrayList alPreSelectedCondtion;
        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void init()
        {
            this.tt.SetToolTip(this.neuTbSearch,"����F3�������ظ�ǰ��Ĳ���");
            ArrayList alSelectedConditionNames = new ArrayList();
            bool hasPreCondition = (this.alPreSelectedCondtion != null);
            if (hasPreCondition && this.alPreSelectedCondtion.Count>0)
            {
                for(int i=0;i<this.alPreSelectedCondtion.Count;i++){
                    alSelectedConditionNames.Add(this.alPreSelectedCondtion[i].ToString());
                }
            }
            if (this.alQCConditions != null)
            {
                for (int i = 0; i < this.alQCConditions.Count; i++)
                {
                    TreeNode node = new TreeNode(this.alQCConditions[i].ToString());
                    node.Text = this.alQCConditions[i].ToString();
                    node.Tag = this.alQCConditions[i];

                    node.Checked = alSelectedConditionNames.Contains(this.alQCConditions[i].ToString());
                    
                    this.neuTVSetting.Nodes.Add(node);
                }
            }
        }
        /// <summary>
        /// �س�ת������
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {

                if (this.neuTbSearch.Focused)
                {
                    if (!string.IsNullOrEmpty(this.neuTbSearch.Text))
                    {
                        this.currentIndex = 0;
                        this.hasNode = false;
                        if (this.neuTVSetting.Nodes.Count > 0)
                        {
                            this.LocateCondition();
                        }
                        return false;
                    }
                    else
                    {
                        MessageBox.Show("��ѯֵ����Ϊ�գ�");
                        this.neuTbSearch.Focus();
                        return false;
                    }
                }
                SendKeys.Send("{TAB}");
                return true;
            }
            if (keyData == Keys.F1)
            {
                this.neuTbSearch.Focus();
                return true;
            }
            if (keyData == Keys.F3)
            {
                if (string.IsNullOrEmpty(this.neuTbSearch.Text))
                {
                    MessageBox.Show("��ѯֵ����Ϊ�գ�");
                    this.neuTbSearch.Focus();
                    return false;
                }
                if (this.currentIndex + 1 >= this.neuTVSetting.Nodes.Count)
                {
                    this.currentIndex = 0;
                }
                this.currentIndex++;
                this.LocateCondition();
            }
            return base.ProcessDialogKey(keyData);

        }
        /// <summary>
        /// �������Ʋ�������
        /// </summary>
        /// <param name="ruleName"></param>
        private void LocateCondition()
        {
            if (this.neuTVSetting.Nodes.Count>0)
            {
                for (; this.currentIndex < this.neuTVSetting.Nodes.Count; this.currentIndex++)
                {
                    if (this.neuTVSetting.Nodes[this.currentIndex].Text.IndexOf(this.neuTbSearch.Text) != -1)
                    {
                        this.neuTVSetting.SelectedNode = this.neuTVSetting.Nodes[this.currentIndex];
                        this.hasNode = true;
                        break;
                    }
                    else
                    {
                        if (this.hasNode && this.currentIndex + 1 >= neuTVSetting.Nodes.Count)
                        {
                            this.currentIndex = 0;
                            continue;
                        }
                    }
                }
                if (!this.hasNode)
                {
                    MessageBox.Show("û���ҵ�Ҫ�����Ľڵ㣡");
                }
            }
        }
        /// <summary>
        /// ȡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuBtCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = DialogResult.Cancel;
            this.FindForm().Close();
        }

        /// <summary>
        /// ȷ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuBtOk_Click(object sender, EventArgs e)
        {
            this.alSelectedCondition.Clear();
            foreach (TreeNode node in this.neuTVSetting.Nodes)
            {
                if (node.Checked)
                {
                    this.alSelectedCondition.Add(node.Tag);
                }
            }
            this.FindForm().DialogResult = DialogResult.OK;
            this.FindForm().Close();
        }
        /// <summary>
        /// ȫ��ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuCbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool setChecked = this.neuCbSelectAll.Checked;
            foreach (TreeNode node in this.neuTVSetting.Nodes)
            {
                node.Checked = setChecked;
            }
        }

        private void neuBtRevSelect_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in this.neuTVSetting.Nodes)
            {
                node.Checked = !node.Checked;
            }
        }
    }
}
