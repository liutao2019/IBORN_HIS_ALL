using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.FuYou
{
    /// <summary>
    /// ������˵������
    /// �������ˣ���
    /// ������ʱ�䣺��
    /// ���޸ļ�¼��
    ///   2009-8-24 xuc ���ӿ���ѡ�����Ҷ໼�߲�ѯ���� {EA2A7657-6A55-4582-8052-DC7F8A5A4795}
    /// 
    /// 
    /// </summary>
    public partial class ucFinIpbPatientDayFee : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinIpbPatientDayFee()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// ҵ���
        /// </summary>
        FS.HISFC.BizLogic.RADT.InPatient managerIntegrate = new FS.HISFC.BizLogic.RADT.InPatient();

        FuYou.ucFinIpbPatientDayFeeDouble ucFinIpbPatientDayFeeDouble = new ucFinIpbPatientDayFeeDouble();
        /// <summary>
        /// �Ƿ���ʾȫԺ����
        /// </summary>
        bool isShowAllInDeptPatient = false;
        /// <summary>
        /// �Ƿ���ʾȫԺ����
        /// </summary>
        public bool IsShowAllInDeptPatient
        {
            get
            {
                return isShowAllInDeptPatient;
            }
            set
            {
                isShowAllInDeptPatient = value;
            }
        }

        string inpatientLine = string.Empty;
        #endregion

        /// <summary>
        ///��ʼ����
        /// </summary>
        /// <returns></returns>
        protected override int OnDrawTree()
        {
            if (tvLeft == null)
            {
                return -1;
            }
            base.OnDrawTree();

            this.tvLeft.Nodes.Clear();

            //����ѡ
            this.tvLeft.CheckBoxes = true;

            if (isShowAllInDeptPatient == false)
            {
                //��Ժ����
                FS.HISFC.Models.RADT.InStateEnumService inState = new FS.HISFC.Models.RADT.InStateEnumService();
                inState.ID = FS.HISFC.Models.Base.EnumInState.I.ToString();

                //ArrayList emplList = managerIntegrate.QueryPatientBasic(base.employee.Dept.ID, inState);

                ArrayList emplList = managerIntegrate.QueryPatientBasicByNurseCell(base.employee.Nurse.ID, inState);

                TreeNode parentTreeNode = new TreeNode("��������");
                parentTreeNode.Checked = false;
                parentTreeNode.Tag = "ROOT";
                tvLeft.Nodes.Add(parentTreeNode);
                foreach (FS.HISFC.Models.RADT.PatientInfo empl in emplList)
                {
                    TreeNode emplNode = new TreeNode();
                    emplNode.Tag = empl;
                    emplNode.Text = "��" + empl.PVisit.PatientLocation.Bed.ID.Substring(4) + "��" + empl.Name;
                    parentTreeNode.Nodes.Add(emplNode);
                }

                parentTreeNode.ExpandAll();
                parentTreeNode.Checked = false;
            }
            else
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ���ȫԺ�����б����Ե�......");
                Application.DoEvents();

                //ȫԺ�����б�
                //��Ժ����
                ArrayList emplList = managerIntegrate.QueryPatient(FS.HISFC.Models.Base.EnumInState.I);

                //�������б�
                Dictionary<string, TreeNode> deptDic = new Dictionary<string, TreeNode>();

                TreeNode parentTreeNode = new TreeNode("ȫԺ����");
                
                parentTreeNode.Tag = "ROOT";
                tvLeft.Nodes.Add(parentTreeNode);
                int index = 0;
                foreach (FS.HISFC.Models.RADT.PatientInfo empl in emplList)
                {
                    if (deptDic.ContainsKey(empl.PVisit.PatientLocation.Dept.ID))
                    {
                        TreeNode patient = new TreeNode();
                        patient.Tag = empl;
                        patient.Text = empl.Name + "��" + empl.PID.PatientNO.ToString() + "��";

                        patient.Checked = false;
                        deptDic[empl.PVisit.PatientLocation.Dept.ID].Nodes.Add(patient);
                    }
                    else
                    {
                        TreeNode dept = new TreeNode();
                        dept.ForeColor = Color.Blue;
                        dept.Tag = empl.PVisit.PatientLocation.Dept;
                        dept.Text = empl.PVisit.PatientLocation.Dept.Name + "��" + empl.PVisit.PatientLocation.Dept.ID.ToString() + "��";
                        
                        TreeNode patient = new TreeNode();
                        patient.Tag = empl;
                        patient.Text = empl.Name + "��" + empl.PID.PatientNO.ToString()+"��";
                        patient.Checked = false;
                        dept.Nodes.Add(patient);
                        deptDic.Add(empl.PVisit.PatientLocation.Dept.ID, dept);

                        dept.Checked = false;
                        parentTreeNode.Nodes.Add(dept);
                    }
                    index++;
                }
                parentTreeNode.ExpandAll();
                parentTreeNode.Checked = false;

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            
            this.tvLeft.AfterSelect -= new TreeViewEventHandler(tvLeft_AfterSelect);
            this.tvLeft.AfterSelect += new TreeViewEventHandler(tvLeft_AfterSelect);
            this.tvLeft.AfterCheck -= new TreeViewEventHandler(tvLeft_AfterCheck);
            this.tvLeft.AfterCheck += new TreeViewEventHandler(tvLeft_AfterCheck);

            return 1;
        }

        /// <summary>
        /// ѡ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tvLeft_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                e.Node.Checked = !e.Node.Checked;
            }
        }

        /// <summary>
        /// ��ѡ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tvLeft_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                bool isCheck = e.Node.Checked;
                this.SelectPatient(e.Node, isCheck);
            }
        }

        /// <summary>
        /// ��ѡ����
        /// </summary>
        /// <param name="treeNode"></param>
        private void SelectPatient(TreeNode treeNode, bool isCheck)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = isCheck;
                SelectPatient(node, isCheck);
            }
        }

        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        protected override int OnRetrieve(params object[] objects)
        {
            if (this.ucQueryInpatientNo1.InpatientNo != null && this.ucQueryInpatientNo1.InpatientNo != "")
            {
                ucQueryInpatientNo1_myEvent();
                return 1;
            }
            this.inpatientLine = "";
            List<string> inpatientLine = new List<string>();
            GetPatients(this.tvLeft.Nodes[0], inpatientLine);
            if (inpatientLine.Count <= 0)
            {
                MessageBox.Show("��ѡ����");
                return -1;
            }
            string[] inpatient = inpatientLine.ToArray();

            DateTime dtBeginTime = dtpBeginTime.Value.Date;
            DateTime dtEndTime = dtBeginTime.AddDays(1).AddSeconds(-1);
            base.OnRetrieve(inpatient, dtBeginTime, dtEndTime, "ALL");
            for (int i = 1; i <= this.dwMain.RowCount; i++)
            {
                //����������ʾ
                try
                {
                    string birthday = this.dwMain.GetItemString(i, "birthday");
                    this.dwMain.SetItemString(i, "txt_age", this.managerIntegrate.GetAge(FS.FrameWork.Function.NConvert.ToDateTime(birthday)));
                }
                catch
                {
                }
            }
            return 1;
        }

        /// <summary>
        /// �ݹ��ȡѡ��Ļ���
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="inpatientLine"></param>
        void GetPatients(TreeNode nodes, List<string> inpatientLine)
        {
            foreach (TreeNode node in nodes.Nodes)
            {
                if (node.Checked && node.Tag is FS.HISFC.Models.RADT.PatientInfo)
                {
                    FS.HISFC.Models.RADT.PatientInfo patient = node.Tag as FS.HISFC.Models.RADT.PatientInfo;
                    inpatientLine.Add(patient.ID);
                    this.inpatientLine += "'" + patient.ID + "',";
                }
                GetPatients(node, inpatientLine);
            }
        }

        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            //if (this.dwMain != null)
            //{
            //    FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            //    FS.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("patientDayFee");
            //    if (pSize != null)
            //    {
            //        dwMain.Modify("DataWindow.Print.Paper.Size=256");
            //        //�˴�����letterֽΪ��216*279 ����Ϊ850*1100
            //        //dwMain.Modify("DataWindow.Print.CustomPage.Length=1100");
            //        //dwMain.Modify("DataWindow.Print.CustomPage.Width=425");
            //        //�˴�����letterֽΪ��216*279
            //        dwMain.Modify("DataWindow.Print.CustomPage.Length=" + pSize.Width);
            //        dwMain.Modify("DataWindow.Print.CustomPage.Width=" + pSize.Height);
            //    }
            //    this.dwMain.Print();
            //}
            this.ucFinIpbPatientDayFeeDouble.inpatientLine = this.inpatientLine;
            return this.ucFinIpbPatientDayFeeDouble.SetPrintData(this.dtpBeginTime.Value);
        }

        private void ucFinIpbPatientDayFee_Load(object sender, EventArgs e)
        {
            //Ĭ�ϴ�ӡ�����嵥
            this.dtpBeginTime.Value = managerIntegrate.GetDateTimeFromSysDateTime().Date;
        }

        private void ucQueryInpatientNo1_myEvent()
        {
            if (this.ucQueryInpatientNo1.Text == null || this.ucQueryInpatientNo1.Text.Trim() == "")
            {
                MessageBox.Show("������סԺ��");

                return;
            }

            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo.Trim() == "")
            {
                if (this.ucQueryInpatientNo1.Err == "")
                {
                    ucQueryInpatientNo1.Err = "�˻��߲���Ժ!";
                }
                FS.FrameWork.WinForms.Classes.Function.Msg(this.ucQueryInpatientNo1.Err, 211);

                this.ucQueryInpatientNo1.Focus();
                return;
            }
            else
            {
                if (base.GetQueryTime() == -1)
                {
                    return;
                }
                FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();
                //ȡ����ʵ��
                FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                patientInfo = radt.QueryPatientInfoByInpatientNO(this.ucQueryInpatientNo1.InpatientNo);
                List<string> inpatientLine = new List<string>();
                inpatientLine.Add(patientInfo.ID);
                if (inpatientLine.Count <= 0)
                {
                    MessageBox.Show("��ѡ����");
                    return;
                }
                string[] inpatient = inpatientLine.ToArray();

                DateTime dtBeginTime = dtpBeginTime.Value.Date;
                DateTime dtEndTime = dtBeginTime.AddDays(1).AddSeconds(-1);
                base.OnRetrieve(inpatient, dtBeginTime, dtEndTime, "ALL");
                for (int i = 1; i <= this.dwMain.RowCount; i++)
                {
                    //����������ʾ
                    try
                    {
                        string birthday = this.dwMain.GetItemString(i, "birthday");
                        this.dwMain.SetItemString(i, "txt_age", this.managerIntegrate.GetAge(FS.FrameWork.Function.NConvert.ToDateTime(birthday)));
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}

