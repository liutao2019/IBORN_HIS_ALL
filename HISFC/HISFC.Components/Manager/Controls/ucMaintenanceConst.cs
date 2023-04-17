using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Forms;
using System.Collections;
using System.Reflection;

namespace FS.FrameWork.WinForms.Controls
{
    /// <summary>
    /// [��������: ����ά���ؼ�]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-11-10]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucMaintenanceConst : UserControl, IMaintenanceControlable
    {
        public ucMaintenanceConst()
        {
            InitializeComponent();
        }

        #region �ֶ�
        private IMaintenanceControlable maintenanceControl;
        private IMaintenanceForm maintenanceForm;
        /// <summary>
        /// �Ѿ���ʼ������ά���ؼ�
        /// </summary>
        protected Dictionary<TreeNode, IMaintenanceControlable> initedMaintenanceControl = new Dictionary<TreeNode, IMaintenanceControlable>();
        #endregion

        #region ����

        private IMaintenanceControlable CurrentMaintenanceControl
        {
            get
            {
                if (this.tvList.SelectedNode != null && this.tvList.SelectedNode.Parent != null)
                {

                    if (this.initedMaintenanceControl.ContainsKey(this.tvList.SelectedNode))
                    {
                        this.maintenanceControl = this.initedMaintenanceControl[this.tvList.SelectedNode];


                    }
                    else
                    {
                        if (this.tvList.SelectedNode.Tag != null)
                        {
                            FS.HISFC.Models.Admin.SysModelFunction info = this.tvList.SelectedNode.Tag as FS.HISFC.Models.Admin.SysModelFunction;

                            if (info.WinName == typeof(FS.FrameWork.WinForms.Controls.ucMaintenance).FullName)  //��̬ά������
                            {
                                object[] arg = new object[1];
                                FS.HISFC.Models.Base.Employee user = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                                arg[0] = user.CurrentGroup.ID + "_" + info.ID;

                                this.maintenanceControl = Assembly.LoadFrom(Application.StartupPath + "\\FrameWork.WinForms.dll").CreateInstance("FS.FrameWork.WinForms.Controls.ucMaintenance",
                                    true, BindingFlags.CreateInstance, null, arg, null, null) as IMaintenanceControlable;
                            }
                            else
                            {
                                this.maintenanceControl = Assembly.LoadFrom(Application.StartupPath + "\\" + info.DllName + ".dll").CreateInstance(info.WinName) as IMaintenanceControlable;
                                (this.maintenanceControl as Control).Text = info.Param;
                            }

                            this.maintenanceControl.Init();
                            this.ShowMaintenanceControl(this.maintenanceControl as Control);

                            this.initedMaintenanceControl.Add(this.tvList.SelectedNode, this.maintenanceControl);
                        }
                    }

                    return this.maintenanceControl;
                }
                else
                    return null;

            }

        }
        #endregion

        #region ����
        protected override void OnLoad(EventArgs e)
        {
            this.ShowList();
            this.maintenanceForm.ShowExportButton = false;
            this.maintenanceForm.ShowImportButton = false;
            this.maintenanceForm.ShowPrintButton = false;
            this.maintenanceForm.ShowPrintPreviewButton = false;
            this.maintenanceForm.ShowPrintConfigButton = false;
            (this.maintenanceForm as FS.FrameWork.WinForms.Forms.frmQuery).ShowQueryButton = false;
            base.OnLoad(e);
        }

        private void ShowMaintenanceControl(Control control)
        {
            this.splitContainer1.Panel2.Controls.Clear();
            if (control != null)
            {
                control.Dock = DockStyle.Fill;
                this.splitContainer1.Panel2.Controls.Add(control);
            }
        }
        /// <summary>
        /// ��ʾ�б�
        /// </summary>
        public void ShowList()
        {
            //����б�
            this.tvList.Nodes.Clear();
            this.tvList.ImageList = this.tvList.groupImageList;
            //��ʾ���ڵ�
            FS.HISFC.Models.Admin.SysModelFunction obj = new FS.HISFC.Models.Admin.SysModelFunction();
            obj.ID = ""; //��ʾ��������
            TreeNode node = new TreeNode();
            node.Text = "����ά��";
            node.ImageIndex = 0;
            node.SelectedImageIndex = node.ImageIndex;
            //node.Tag = obj;
            this.tvList.Nodes.Add(node);

            //ȡ��ǰ�û����п���ά���ĳ���
            FS.HISFC.BizLogic.Manager.SysGroup group = new FS.HISFC.BizLogic.Manager.SysGroup();
            //
            //  I don't think this is a good idea to save the info in this class
            //  Robin   2006-11-14
            FS.HISFC.Models.Base.Employee user = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

            ArrayList al = group.GetConstByGroup(user.CurrentGroup.ID);

            if (al == null)
            {
                MessageBox.Show("û�п���ά���ĳ���������ϵͳ���������ó���ά����");
                return;
            }

            //��ʾ����ά������
            foreach (FS.HISFC.Models.Admin.SysModelFunction info in al)
            {
                node = new TreeNode();
                node.Text = info.Name;
                node.ImageIndex = 3;
                node.SelectedImageIndex = 4;
                node.Tag = info;
                this.tvList.Nodes[0].Nodes.Add(node);
            }

            this.tvList.Nodes[0].ExpandAll();
        }

        /// <summary>
        /// ˢ�¿����ϸ����
        /// </summary>
        public void RefreshData()
        {
            //���ýӿڵ�Query��������ʾ����
            if (this.CurrentMaintenanceControl != null)
            {
                this.ShowMaintenanceControl(this.maintenanceControl as Control);
                this.maintenanceControl.Query();
            }
            else
            {

            }
        }

        #endregion



        #region �¼�

        private void tvList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //ˢ������
            this.RefreshData();
        }
        private void tvList_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (this.CurrentMaintenanceControl != null)
            {
                //this.CurrentMaintenanceControl.Save();
            }
        }
        #endregion
        #region IMaintenanceControlable ��Ա

        public IMaintenanceForm QueryForm
        {
            get
            {
                return this.maintenanceForm;
            }
            set
            {
                this.maintenanceForm = value;
            }
        }

        public int Query()
        {
            if (this.maintenanceControl != null)
                return maintenanceControl.Query();

            return -1;
        }

        public int Add()
        {
            if (this.maintenanceControl != null)
                return this.maintenanceControl.Add();

            return -1;
        }

        public int Delete()
        {
            if (this.CurrentMaintenanceControl != null)
            {
                return this.CurrentMaintenanceControl.Delete();
            }
            else
            {
                return -1;
            }
        }

        public int Save()
        {
            if (this.CurrentMaintenanceControl != null)
            {
                return this.CurrentMaintenanceControl.Save();
            }
            else
            {
                return -1;
            }
        }

        public int Export()
        {
            if (this.CurrentMaintenanceControl != null)
            {
                return this.CurrentMaintenanceControl.Export();
            }
            else
            {
                return -1;
            }
        }

        public int Print()
        {
            if (this.CurrentMaintenanceControl != null)
            {
                return this.CurrentMaintenanceControl.Print();
            }
            else
            {
                return -1;
            }
        }

        public bool IsDirty
        {
            get
            {
                if (this.CurrentMaintenanceControl != null)
                    return this.CurrentMaintenanceControl.IsDirty;
                return false;
            }
            set
            {
                if (this.CurrentMaintenanceControl != null)
                    this.CurrentMaintenanceControl.IsDirty = value;
            }
        }


        public int Copy()
        {
            return 0;
        }

        public int Cut()
        {
            return 0;
        }

        public int Import()
        {
            return 0;
        }

        public int Init()
        {
            return 0;
        }

        public int Modify()
        {
            return 0;
        }

        public int NextRow()
        {
            return 0;
        }

        public int Paste()
        {
            return 0;
        }

        public int PreRow()
        {
            return 0;
        }

        public int PrintConfig()
        {
            return 0;
        }

        public int PrintPreview()
        {
            return 0;
        }

        #endregion


    }

}
