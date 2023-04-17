using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.WinForms.Report.Pharmacy
{
    public partial class ucStoOutputWithRecCount : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public ucStoOutputWithRecCount()
        {
            InitializeComponent();
            string strAll = "ALL";
            string strName = "ȫ��";

            FS.FrameWork.Models.NeuObject neuO = new FS.FrameWork.Models.NeuObject();
            neuO.ID = strAll;
            neuO.Name = strName;
            ArrayList myDrugQuality = consManager.GetList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY);
            myDrugQuality.Add(neuO);
            this.cmbQuality.AddItems(myDrugQuality);
            this.cmbQuality.SelectedIndex = 0;

        }
        #region ����

        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();

        #endregion
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        protected override int OnDrawTree()
        {

            ArrayList deptList = this.managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.P);
            if (deptList == null)
            {
                return -1;
            }
            TreeNode parentTreeNode = new TreeNode("���п���");
            this.tvLeft.Nodes.Add(parentTreeNode);

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
        /// ��������
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }

            TreeNode selectNode = this.tvLeft.SelectedNode;

            if (selectNode.Level == 0)
            {
                return -1;
            }

            string deptCode = selectNode.Tag.ToString();

            //this.dwMain.Retrieve();
            
            string name=this.employee.Name;

            return base.OnRetrieve(base.beginTime, base.endTime, deptCode,this.cmbQuality.Tag.ToString(),name);
        }
    }
}

