using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.Report.Logistics.Pharmacy
{
    /// <summary>
    /// ���ⵥ����
    /// </summary>
    public partial class ucPhaOutputRepair : FSDataWindow.Controls.ucQueryBaseForDataWindow//FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucPhaOutputRepair()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected override void OnLoad()
        {
            
            base.OnLoad(); 
            this.InitCmb();
        }
        
        /// <summary>
        /// �ۺ�ҵ�����
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ����ҩ����ҩ���б�
        /// </summary>
        /// <returns></returns>
        protected override int OnDrawTree()
        {
            
            if (tvLeft == null)
            {
                return -1;
            }

            ArrayList deptList = this.managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.P);

            if (deptList == null)
            {
                return -1;
            }
            if (this.tvLeft.Nodes.Count > 0)
            {
                this.tvLeft.Nodes.Clear();
            }

            TreeNode parentTreeNode = new TreeNode("���в���");
            tvLeft.Nodes.Add(parentTreeNode);
            foreach (FS.HISFC.Models.Base.Department dept in deptList)
            {
                TreeNode deptNode = new TreeNode();
                deptNode.Tag = dept.ID;
                deptNode.Text = dept.Name;
                parentTreeNode.Nodes.Add(deptNode);
            }

            parentTreeNode.ExpandAll();

            return base.OnDrawTree();
        }
        
        /// <summary>
        /// ��ʼ����������б�
        /// </summary>
        private void InitCmb()
        {
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager userPower = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            List<FS.FrameWork.Models.NeuObject> list = userPower.QueryUserPrivCollection(this.employee.ID, "0320", this.employee.Dept.ID);
            if (list != null)
            {
                this.cmbOutType.AddItems(new ArrayList(list));
                if (list.Count > 0)
                {
                    this.cmbOutType.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        protected override int OnRetrieve(params object[] objects)
        {
            if (this.cmbOutType.Tag == null)
            {
                MessageBox.Show("��ѡ��������ͣ�", "��ʾ");
                return 1;
            }

            if (this.GetQueryTime() == -1)
            {
                return -1;
            }

            TreeNode selectNode = tvLeft.SelectedNode;

            if (selectNode.Level == 0)
            {
                return -1;
            }
            string deptCode = selectNode.Tag.ToString();

            return base.OnRetrieve(this.beginTime, this.endTime, this.cmbOutType.Tag.ToString(), deptCode);
        }
    }
}
