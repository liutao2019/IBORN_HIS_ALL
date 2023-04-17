using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace Report.Finance.FinIpb
{
    public partial class ucFinIpbItemStat : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region ����

        FS.HISFC.BizProcess.Integrate.Manager inteManager = new FS.HISFC.BizProcess.Integrate.Manager();

        //FS.HISFC.BizProcess.Integrate.RADT patientRadt = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizProcess.Integrate.RADT patientRadt = new FS.HISFC.BizProcess.Integrate.RADT();

        FS.HISFC.Models.Base.Employee empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

        #endregion 

        public ucFinIpbItemStat()
        {
            InitializeComponent();
        }

        private void ucFinIpbItemStat_Load(object sender, EventArgs e)
        {
            this.InitDeptTree();
            this.InitPatientTree();
        }

        #region ����

        /// <summary>
        /// ��ʼ���ն˿�����
        /// </summary>
        private void InitDeptTree()
        {
            TreeNode parentNode = new TreeNode("�ն˿���");
            this.tvDept.Nodes.Add(parentNode);
            ArrayList al = new ArrayList();
            al = inteManager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.T);
            if(al == null|| al.Count == 0)
            {
                return;
            }
            foreach(FS.HISFC.Models.Base.Department dept in al)
            {
                TreeNode node = new TreeNode();
                node.Tag = dept.ID;
                node.Text = dept.Name;
                parentNode.Nodes.Add(node);
            }
            this.tvDept.ExpandAll();
        }

        /// <summary>
        /// ��ʼ������������
        /// </summary>
        private void InitPatientTree()
        {
            string currentDeptID = empl.Dept.ID;

            TreeNode patientNode = new TreeNode("��������");
            this.tvPatient.Nodes.Add(patientNode);
            ArrayList al = new ArrayList();
            al = patientRadt.QueryPatient(currentDeptID,FS.HISFC.Models.Base.EnumInState.I);
            if (al == null || al.Count == 0)
            {
                return;
            }
            foreach(FS.HISFC.Models.RADT.PatientInfo patientInfo in al)
            {
                TreeNode node = new TreeNode();
                node.Tag = patientInfo.PID.CaseNO;
                node.Text = patientInfo.Name;
                patientNode.Nodes.Add(node);
            }
            this.tvPatient.ExpandAll();
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            System.Type dtStr = System.Type.GetType("System.String");
            String[] condition;
            ArrayList tempAL = new ArrayList();

            if (this.neuTabControl1.SelectedTab == this.tabPage1)//�ն˿����б�
            {
                foreach (TreeNode tn in tvDept.GetNodeAt(0, 0).Nodes)
                {
                    if (tn.Checked)
                    {
                        tempAL.Add(tn.Tag.ToString());
                    }
                }
                condition = tempAL.ToArray(dtStr) as string[];
                if (condition != null && condition.Length == 0)
                {
                    MessageBox.Show("��ѡ�����");
                    return -1;
                }
                //for (int i = 0; i < tempAL.Count; i++)
                //{
                //    if (i == tempAL.Count - 1)
                //    {
                //        condition += tempAL[i].ToString();
                //        condition += ")";
                //    }
                //    else
                //    {
                //        condition += tempAL[i].ToString();
                //        condition += ",";
                //    }
                //}
                this.neuTabControl2.SelectedTab = this.tabPage3;
                return dwDeptPatient.Retrieve(this.dtpTime.Value.Date, this.dtpTime.Value.Date.AddDays(1).AddMilliseconds(-1), empl.Dept.ID, empl.Dept.Name,condition);
            }
            else
            {
                foreach (TreeNode tn in tvPatient.GetNodeAt(0, 0).Nodes)
                {
                    if (tn.Checked)
                    {
                        tempAL.Add(tn.Tag.ToString());
                    }
                }
                condition = tempAL.ToArray(dtStr) as string[];
                if (condition != null && condition.Length == 0)
                {
                    MessageBox.Show("��ѡ����");
                    return -1;
                }
                //for (int j = 0; j < tempAL.Count; j++)
                //{
                //    if (j == tempAL.Count - 1)
                //    {
                //        condition += tempAL[j].ToString();
                //        condition += ")";
                //    }
                //    else
                //    {
                //        condition += tempAL[j].ToString();
                //        condition += ",";
                //    }
                //}
                this.neuTabControl2.SelectedTab = this.tabPage4;
                return dwPatientDept.Retrieve(this.dtpTime.Value.Date, this.dtpTime.Value.Date.AddDays(1).AddMilliseconds(-1), empl.Dept.ID, empl.Dept.Name,condition);
                
            }
        }
        #endregion

        #region �¼�

        private void tvDept_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e == null)
            {
                return;
            }
            if(e.Node.Text.Equals("�ն˿���"))
            {
                foreach (TreeNode tn in e.Node.Nodes)
                {
                    if(e.Node.Checked)
                    {
                        tn.Checked = true;
                    }
                    else
                    {
                        tn.Checked = false;
                    }     
                }
                return;
            }
            else 
            {
                if (e.Node.Checked)
                {
                    e.Node.Checked = true;
                }
                else
                {
                    e.Node.Checked = false;  
                }      
            }
        }

        private void tvPatient_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e == null)
            {
                return;
            }
            if (e.Node.Text.Equals("��������"))
            {
                foreach (TreeNode tn in e.Node.Nodes)
                {
                    if (e.Node.Checked)
                    {
                        tn.Checked = true;
                    }
                    else
                    {
                        tn.Checked = false;
                    }
                }
                return;
            }
            else
            {
                if (e.Node.Checked)
                {
                    e.Node.Checked = true;
                }
                else
                {
                    e.Node.Checked = false;
                }
            }
        }

        #endregion

        
    }
}
