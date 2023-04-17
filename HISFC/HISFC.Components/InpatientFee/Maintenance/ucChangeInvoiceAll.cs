using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.InpatientFee.Maintenance
{
    public partial class ucChangeInvoiceAll : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region ����
        FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum invoiceManager = new FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum();

        /// <summary>
        /// �Ƿ���ʾ��Ʊ������
        /// </summary>
        private bool isShow = true;

        /// <summary>
        /// �Ƿ���ʾ��Ʊ������
        /// </summary>
        [Category("��ʾ����"), Description("�Ƿ���ʾ��Ʊ������")]
        public bool IsShow
        {
            get
            {
                return this.isShow;
            }
            set
            {
                this.isShow = value;
            }
        }

        #endregion

        public ucChangeInvoiceAll()
        {
            InitializeComponent();
            this.ucInvoiceChangeNOInitInvoiceType1.QueryAfterChange += this.Query;
        }

        private void Query()
        {
            this.OnQuery(new object(), new object());
        }

        /// <summary>
        /// ��ȡ����Ա���ú�δ�õķ�Ʊ����
        /// </summary>
        /// <param name="operCode"></param>
        /// <returns></returns>
        public int SetTreeView(string operCode)
        {
            if (this.neuTreeView1.Nodes.Count > 0)
            {
                this.neuTreeView1.Nodes.Clear();
            }

            ArrayList alInvoice = invoiceManager.GetInvoiceTypeByOperIDORFinGroupID(operCode, "");

            if (alInvoice == null)
            {
                MessageBox.Show("��ѯ��Ʊ���ͳ���");
                return -1;
            }
            TreeNode rootNode = new TreeNode();
            rootNode.Text = FS.FrameWork.Management.Connection.Operator.Name;
            rootNode.Tag = FS.FrameWork.Management.Connection.Operator.ID;
            rootNode.SelectedImageIndex = (int)FS.FrameWork.WinForms.Classes.EnumImageList.R��Ա;
            rootNode.ImageIndex = (int)FS.FrameWork.WinForms.Classes.EnumImageList.R��Ա;



            for (int i = 0; i < alInvoice.Count; i++)
            {
                FS.FrameWork.Models.NeuObject obj = alInvoice[i] as FS.FrameWork.Models.NeuObject;

                TreeNode node = new TreeNode();
                node.Text = obj.Name;
                node.Tag = obj;
                node.ImageIndex = (int)FS.FrameWork.WinForms.Classes.EnumImageList.M��ϸ;
                node.SelectedImageIndex = (int)FS.FrameWork.WinForms.Classes.EnumImageList.M��ϸ;
                rootNode.Nodes.Add(node);
            }
            rootNode.ExpandAll();
            this.neuTreeView1.Nodes.Add(rootNode);

            return 1;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.neuTreeView1.SelectedNode.Level == 0)
            {
                this.ucInvoiceChangeNOInitInvoiceType1.InvoiceTypeName = "";
                this.ucInvoiceChangeNOInitInvoiceType1.InvoiceTypeID = "";
                this.ucInvoiceChangeNOInitInvoiceType1.QueryInvoices("", "");
                return -1;
            }
            this.QueryInvoice(FS.FrameWork.Management.Connection.Operator.ID, (FS.FrameWork.Models.NeuObject)this.neuTreeView1.SelectedNode.Tag);

            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        ///��ѯ�վ���Ϣ
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected int QueryInvoice(string operCode, FS.FrameWork.Models.NeuObject obj)
        {
            if (this.neuTreeView1.SelectedNode.Level == 0)
            {
                this.ucInvoiceChangeNOInitInvoiceType1.InvoiceTypeName = "";
                this.ucInvoiceChangeNOInitInvoiceType1.InvoiceTypeID = "";
                this.ucInvoiceChangeNOInitInvoiceType1.QueryInvoices("", "");
                return -1;
            }
            this.ucInvoiceChangeNOInitInvoiceType1.InvoiceTypeID = obj.ID;
            this.ucInvoiceChangeNOInitInvoiceType1.InvoiceTypeName = obj.Name;
            int returnValue = this.ucInvoiceChangeNOInitInvoiceType1.QueryInvoices(operCode, obj.ID);
            if (returnValue < 0)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {

            this.SetTreeView(FS.FrameWork.Management.Connection.Operator.ID);

            this.ucInvoiceChangeNOInitInvoiceType1.InvoiceTypeName = "";

            //�Ƿ���ʾ"����"��
            this.ucInvoiceChangeNOInitInvoiceType1.IsShowButtonColumn = this.IsShow;

            base.OnLoad(e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.ucInvoiceChangeNOInitInvoiceType1.SaveResult();
            return base.OnSave(sender, neuObject);
        }

        private void neuTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.neuTreeView1.SelectedNode.Level == 0)
            {
                this.ucInvoiceChangeNOInitInvoiceType1.InvoiceTypeName = "";
                this.ucInvoiceChangeNOInitInvoiceType1.InvoiceTypeID = "";
                this.ucInvoiceChangeNOInitInvoiceType1.QueryInvoices("", "");
                return;
            }
            this.QueryInvoice(FS.FrameWork.Management.Connection.Operator.ID, (FS.FrameWork.Models.NeuObject)e.Node.Tag);
        }




    }
}
