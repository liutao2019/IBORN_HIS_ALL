using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [��������: ����ؼ�]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-01-15]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucReport : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucReport()
        {
            InitializeComponent();
            if (!Environment.DesignMode)
            {
                this.Init();
            }
        }

        #region �ֶ�
        private XmlDocument xmlDoc = new XmlDocument();
        #endregion

        private class ControlData
        {
            public string AssemblyName;
            public string CotnrolName;
            public Control Contorl;
        };

        #region ����
        private FS.FrameWork.WinForms.Forms.IReport Report
        {
            get
            {
                return ((ControlData)this.tvwControls.SelectedNode.Tag).Contorl as FS.FrameWork.WinForms.Forms.IReport;
            }
        }
        #endregion
        #region ����
        private void Init()
        {
            this.imageList1.Images.Add(FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.J��������));
            //��XML�ļ��ж������ṹ
            xmlDoc.Load("OperationReport.xml");
            XmlNodeList items = xmlDoc.SelectNodes("/Config/Item");
            foreach (XmlNode node in items)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Text = node.Attributes[0].Value;
                treeNode.ImageIndex = 0;
                ControlData data = new ControlData();
                data.AssemblyName = node.Attributes[1].Value;
                data.CotnrolName = node.Attributes[2].Value;

                treeNode.Tag = data;
                this.tvwControls.Nodes.Add(treeNode);
            }
        }
        #endregion

        #region �¼�
        private void neuTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ControlData data = e.Node.Tag as ControlData;

            if (data.Contorl == null)
            {
                data.Contorl = Assembly.LoadFrom(data.AssemblyName).CreateInstance(data.CotnrolName) as Control;
                data.Contorl.Dock = DockStyle.Fill;
            }
            this.pnlControls.Controls.Clear();
            this.pnlControls.Controls.Add(data.Contorl);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.Report != null)
                this.Report.Query();

            return base.OnQuery(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            if (this.Report != null)
                this.Report.Print();

            return base.OnPrint(sender, neuObject);
        }
        #endregion
    }
}
��