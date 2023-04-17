using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.Report.Finance.FinIpb
{
    public partial class ucFinIpbDoctFee : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        /// <summary>
        /// סԺҽ��������ͳ��
        /// </summary>
        public ucFinIpbDoctFee()
        {
            InitializeComponent();
        }
        //FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        private string emplCode = string.Empty;
        protected override void OnLoad()
        {

            this.isAcross = true;
            this.isSort = false;
            //this.Init();

            base.OnLoad();
           
            //������ݡ�ҽ��
            FS.HISFC.Models.Base.Employee allDoct = new FS.HISFC.Models.Base.Employee();
            System.Collections.ArrayList alDoctList = managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            allDoct.ID = "%%";
            allDoct.Name = "ȫ��";
            allDoct.SpellCode = "QB";
            alDoctList.Insert(0, allDoct);
            this.cboDoctCode.AddItems(alDoctList);
            if (cboDoctCode.Items.Count > 0)
            {
                cboDoctCode.SelectedIndex = 0;
                emplCode = this.cboDoctCode.Tag.ToString();
            }

        }

        /// <summary>
        /// ��д������
        /// </summary>
        /// <returns></returns>
        //protected override int OnDrawTree()
        //{
        //    if (tvLeft == null)
        //    {
        //        return -1;
        //    }
        //    ArrayList emplList = managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);

        //    TreeNode parentTreeNode = new TreeNode("����ҽ��");
        //    tvLeft.Nodes.Add(parentTreeNode);
        //    foreach (FS.HISFC.Models.Base.Employee empl in emplList)
        //    {
        //        TreeNode emplNode = new TreeNode();
        //        emplNode.Tag = empl.ID;
        //        emplNode.Text = empl.Name;
        //        parentTreeNode.Nodes.Add(emplNode);
        //    }

        //    parentTreeNode.ExpandAll();

        //    return base.OnDrawTree();
        //}
        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }

            //TreeNode selectNode = tvLeft.SelectedNode;

            //if (selectNode.Level == 0)
            //{
            //    return -1;
            //}
            //string emplCode = selectNode.Tag.ToString();

            return base.OnRetrieve(base.beginTime, base.endTime, emplCode);
        }

        private void cboDoctCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDoctCode.SelectedIndex >= 0)
            {
                emplCode = this.cboDoctCode.Tag.ToString();
            }
        }

       
    }
}

