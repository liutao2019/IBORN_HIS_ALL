using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using FS.HISFC.Models.Base;
using System.Collections;

namespace FS.Report.MET.MetCas
{
    public partial class ucMetOpbList_wh : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucMetOpbList_wh()
        {
            InitializeComponent();
            base.LeftControl = QueryControls.Tree;
        }

        private System.Collections.Hashtable hashTable = new System.Collections.Hashtable();

        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizLogic.Manager.DepartmentStatManager myDepartmentStatManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();

        protected override int OnDrawTree()
        {
            if (tvLeft == null)
            {
                return -1;
            }
            ArrayList deptList = this.myDepartmentStatManager.LoadDepartmentStatAndByNodeKind("71", "0");

            ArrayList deptList1 = this.myDepartmentStatManager.LoadDepartmentStatAndByNodeKind("71", "1");


            TreeNode parentTreeNode = new TreeNode("ȫ��");
            parentTreeNode.Tag = "All";
            parentTreeNode.Text = "ȫԺ";
            tvLeft.Nodes.Add(parentTreeNode);
            foreach (FS.HISFC.Models.Base.DepartmentStat dept in deptList1)
            {
                TreeNode deptNode = new TreeNode();
                deptNode.Tag = dept.ID;
                deptNode.Text = dept.Name;
                parentTreeNode.Nodes.Add(deptNode);
            }
            parentTreeNode.ExpandAll();

            //FS.HISFC.BizLogic.Manager.DepartmentStatManager statMgr = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            ////�������ҷ���ȼ���������һ���ڵ��б�
            //ArrayList depts = statMgr.LoadLevelViewDepartemt("71");
            //foreach (FS.HISFC.Models.Base.DepartmentStat info in depts)
            //{
            //    hashTable.Add(info.PkID, info);
            //}

            ////��TreeView����ʾ������Ϣ
            //AddView("71");

            return base.OnDrawTree();
                        
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            TreeNode selectNode = tvLeft.SelectedNode;
            
            string deptCode = selectNode.Tag.ToString();
            string deptName = selectNode.Text.ToString();

            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, deptCode, deptName);
        }

        public void update()
        {
            Sybase.DataWindow.Transaction trans = new Sybase.DataWindow.Transaction();
            System.Data.OracleClient.OracleConnectionStringBuilder ocs =
            new System.Data.OracleClient.OracleConnectionStringBuilder(FS.FrameWork.Management.Connection.Instance.ConnectionString);
            trans.Password = ocs.Password;
            trans.ServerName = ocs.DataSource;
            trans.UserId = ocs.UserID;

            trans.Dbms = Sybase.DataWindow.DbmsType.Oracle8i;


            trans.AutoCommit = false;
            trans.DbParameter = "PBCatalogOwner='lchis19'";

            trans.Connect();
            this.dwMain.SetTransaction(trans);
            //this.dwMain.UpdateData(true);
            //trans.Commit();
            try
            {
                this.dwMain.UpdateData(true);
            }
            catch (Exception e)
            {
                trans.Rollback();
                MessageBox.Show("����ʧ�ܣ�" + e.Message);
                return;
            }
            trans.Commit();
            MessageBox.Show("����ɹ���");
        }
        protected override int OnSave(object sender, object neuObject)
        {
            this.update();
            return base.OnSave(sender, neuObject);
        }

        private void ucMetOpbList_wh_Load(object sender, EventArgs e)
        {
            DateTime sysTime = this.myDepartmentStatManager.GetDateTimeFromSysDateTime();
            this.dtpBeginTime.Text = sysTime.AddDays(-1).ToShortDateString() + " 00:00:00";
            this.dtpEndTime.Text = sysTime.ToShortDateString() + " 00:00:00";
        }


        
    }
}
