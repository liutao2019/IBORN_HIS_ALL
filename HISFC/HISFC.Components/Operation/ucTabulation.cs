using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.Operation
{
    public partial class ucTabulation : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucTabulation()
        {
            InitializeComponent();
            Init();
        }
        private FS.HISFC.BizProcess.Integrate.Registration.Tabulation tabMgr = new FS.HISFC.BizProcess.Integrate.Registration.Tabulation();
       
        private FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();

        private ArrayList al = null;
        private ucTabular tabular = null;
        private FS.HISFC.Models.Base.Employee var = null;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int Init()
        {
            var = (FS.HISFC.Models.Base.Employee)this.dataManager.Operator; 
            InitDept();
            this.ucTabular1.Init(var);
            this.ucTabular1.Clear();

            this.dateTimePicker1.Value = this.dataManager.GetDateTimeFromSysDateTime();

            return 0;
        }
        /// <summary>
        /// ���ɲ���Ա��������б�
        /// </summary>
        /// <returns></returns>
        private int InitDept()
        {
            this.tvDept.Nodes.Clear();

            TreeNode root = new TreeNode("�ֹܿ���");
            root.SelectedImageIndex = 22;
            root.ImageIndex = 22;
            this.tvDept.Nodes.Add(root);
            //List al <FS.FrameWork.Models.NeuObject>
            //����Ȩ�޻�ò���Ա���ڿ���
            al= new ArrayList(FS.HISFC.Components.Common.Classes.Function.QueryPrivList("0601",true).ToArray()); 
            if (al == null) return -1;
            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                TreeNode node = new TreeNode();
                node.Text = obj.Name;
                node.SelectedImageIndex = 41;
                node.ImageIndex = 40;
                node.Tag = obj;
                root.Nodes.Add(node);
            }

            this.tvDept.ExpandAll();

            return 0;
        }

        #region ��������Ϣ

        /// <summary>
        /// ���幤��������
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #region ��ʼ��������
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "����", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S��һ��, true, false, null);
            toolBarService.AddToolButton("����", "����", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X��һ��, true, false, null);
            toolBarService.AddToolButton( "����", "����", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S��һ��, true, false, null );
            toolBarService.AddToolButton( "����", "����", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X��һ��, true, false, null ); 
            return toolBarService;
        }
        #endregion

        #region ���������Ӱ�ť�����¼�
        /// <summary>
        /// ���������Ӱ�ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    this.ucTabular1.PriorWeek();
                    break;
                case "����":
                    this.ucTabular1.NextWeek();
                    break; 
                case "����":
                    this.ucTabular1.Up();
                    break;
                case "����":
                    this.ucTabular1.Down();
                    break; 
                default:
                    break;
            }
        }
        #endregion

        #endregion 

        #region ����
        protected override int OnSave(object sender, object neuObject)
        {
            this.ucTabular1.Save();
            return base.OnSave(sender, neuObject);
        }
        #endregion 

        private void tvDept_DoubleClick(object sender, System.EventArgs e)
        {
            TreeNode node = this.tvDept.SelectedNode;
            if (node == null || node.Tag == null) return;

            this.ucTabular1.QueryCurrent((node.Tag as FS.FrameWork.Models.NeuObject).ID);
            //��ӱ����Ű��¼
            QueryTabular((node.Tag as FS.FrameWork.Models.NeuObject).ID);
            //��¼��ǰ����
            this.treeView1.Tag = (node.Tag as FS.FrameWork.Models.NeuObject).ID;
        }
        /// <summary>
        /// ��ѯ���Ű����
        /// </summary>
        /// <param name="deptID"></param>
        private void QueryTabular(string deptID)
        {
            string order = "";

            DateTime begin = DateTime.Parse(this.dateTimePicker1.Value.Year.ToString() + "-" +
                this.dateTimePicker1.Value.Month.ToString() + "-1");
            al = tabMgr.Query(begin, deptID);

            this.treeView1.Nodes[0].Nodes.Clear();
            if (al != null)
            {
                for (int i = 0; i < al.Count; i++)
                {
                    order = al[i].ToString();//�Ű����
                    TreeNode node = new TreeNode();
                    node.Text = order.Substring(4, 2) + "��" + order.Substring(6, 2) + "�ա�" + order.Substring(12, 2) + "��" + order.Substring(14, 2) + "��";
                    node.SelectedImageIndex = 21;
                    node.ImageIndex = 20;
                    node.Tag = order;
                    this.treeView1.Nodes[0].Nodes.Add(node);
                }
            }
            this.treeView1.Nodes[0].Expand();
        }

        /// <summary>
        /// ˢ���Ű���Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePicker1_ValueChanged(object sender, System.EventArgs e)
        {
            if (this.treeView1.Tag == null) return;
            this.QueryTabular(this.treeView1.Tag.ToString());
        }
        //�����Ű�ģ��
        private void treeView1_DoubleClick(object sender, System.EventArgs e)
        {
            TreeNode select = this.treeView1.SelectedNode;
            if (select == null) return;
            if (select.Parent == null) return;
            if (this.treeView1.Tag == null || this.treeView1.Tag.ToString() == "") return;

            #region ���ɴ���
            System.Windows.Forms.Form f = new Form();
            f.MinimizeBox = false;
            f.MaximizeBox = false;
            f.Text = "�Ű�";
            f.StartPosition = FormStartPosition.CenterParent;
            f.Size = new Size(600, 450);

            Panel p1 = new Panel();
            p1.TabIndex = 0;


            Panel p2 = new Panel();
            p2.TabIndex = 1;
            p2.Height = 50;
            p2.Dock = DockStyle.Bottom;
            f.Controls.Add(p1);
            f.Controls.Add(p2);

            tabular = new ucTabular();
            tabular.Init(var);
            p1.Controls.Add(tabular);
            p1.Dock = DockStyle.Fill;
            tabular.Dock = DockStyle.Fill;

            Button bOK = new Button();
            Button bExit = new Button();
            p2.Controls.Add(bOK);
            p2.Controls.Add(bExit);
            bOK.Text = "����";
            bOK.DialogResult = DialogResult.OK;
            bExit.Text = "ȡ��";
            bOK.Location = new Point(p2.Width - 220, 13);
            bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            bExit.Location = new Point(p2.Width - 120, 13);
            bExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            f.AcceptButton = bOK;
            f.CancelButton = bExit;
            tabular.Query(select.Tag.ToString(), this.treeView1.Tag.ToString());

            bOK.Click += new EventHandler(bOK_Click);
            f.ShowDialog();
            f.Dispose();
            #endregion

        }

        //����ģ��
        private void bOK_Click(object sender, EventArgs e)
        {
            ArrayList al = this.tabular.getTabular();
            if (al == null) return;
            this.ucTabular1.LoadTemplate(al);
        }
    }
}
