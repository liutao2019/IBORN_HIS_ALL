using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.RADT;
using FS.HISFC.Models.Base;
using FS.FrameWork.Models;
using FS.HISFC.Models.Operation;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [��������: ��������ؼ�]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-11-28]<br></br>
    /// <�޸ļ�¼
    ///		�޸���='�ſ���'
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��='������������'
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucApplicationzdly : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucApplicationzdly()
        {
            InitializeComponent();
            this.Init();
            this.isSaveClose = false;
        }

        public ucApplicationzdly(PatientInfo patient, NeuObject item)
            : this()
        {
            this.ucQueryInpatientNo1.Text = patient.PID.PatientNO;
            this.ucApplicationForm1.PatientInfo = patient;
            this.ucApplicationForm1.MainOperation = item.Clone();

            this.isSaveClose = true;
        }

        #region �ֶ�
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();


        private FS.HISFC.BizProcess.Integrate.Manager integrateManager = new FS.HISFC.BizProcess.Integrate.Manager();
        private Dictionary<string, Department> depts = new Dictionary<string, Department>();

        private bool isSaveClose;   //�Ƿ񱣴���Զ��ر�

        private TreeNode currentNode;
        #endregion

  
        [Category("�ؼ�����"), Description("��������ȵ�ǰʱ���������")]
        public bool CheckApplyTime
        {
            get
            {
                return ucApplicationForm1.CheckApplyTime;
            }
            set
            {
                ucApplicationForm1.CheckApplyTime = value;
            }
        }


        #region ����
        [Category("�ؼ�����"), Description("�Ƿ��ӡ���������뵥״̬Ϊ�Ѱ���")]
        public bool IsUpdateSate
        {
            get
            {
                return ucApplicationForm1.IsUpdateSate;
            }
            set
            {
                ucApplicationForm1.IsUpdateSate = value;
            }
        }


        [Category("�ؼ�����"), Description("����ʱ�䳬����ֹʱ�䣬�Ƿ���Ҫ��Ϊ����")]
        public bool CheckEmergency
        {
            get
            {
                return  ucApplicationForm1.CheckEmergency;
            }
            set
            {
                ucApplicationForm1.CheckEmergency = value;
            }
        }
        [Category("�ؼ�����"), Description("�������ղ���������һ������")]
        public bool CheckDate
        {
            get
            {
                return ucApplicationForm1.CheckDate;
            }
            set
            {
                ucApplicationForm1.CheckDate = value;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FS.HISFC.Models.Operation.OperationAppllication OperationApplication
        {
            get
            {
                return this.ucApplicationForm1.OperationApplication;
            }

        }

        [Category("�ؼ�����"), Description("�������뾯ʾ������")]
        public string Note
        {
            get
            {
                return ucApplicationForm1.Note;
            }
            set
            {
                ucApplicationForm1.Note = value;
            }
        }
        #endregion
        #region ����
        /// <summary>
        /// ��ʹ��
        /// </summary>
        private void Init()
        {
            this.imageList1.Images.Add(FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R��Ա��));
            this.imageList1.Images.Add(FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R��Ա));
            this.imageList1.Images.Add(FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡִ�е�));
            this.imageList1.Images.Add(FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.G�˿�));
            this.BackColor = System.Drawing.Color.Azure;
            neuPanel1.BackColor = System.Drawing.Color.Azure;
            ucApplicationForm1.BackColor = System.Drawing.Color.Azure;
            //�õ������б�
            System.Collections.ArrayList alDepts = this.integrateManager.GetDepartment();
            foreach (Department dept in alDepts)
            {
                this.depts.Add(dept.ID, dept);
            }
        }

        /// <summary>
        /// �������������뵥�б�
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        private int RefreshApply(NeuObject dept)
        {
            tvApply.Nodes.Clear();
            TreeNode root = new TreeNode("�������뵥", 22, 22);
            root.Tag = "OPS";
            tvApply.Nodes.Add(root);

            TreeNode root1 = new TreeNode("�Ѱ�������", 22, 22);
            root1.Tag = "3";
            tvApply.Nodes.Add(root1);

            //add by zhy 2013-11-12
            TreeNode root4 = new TreeNode("�����δ�շ�����", 22, 22);
            root4.Tag = "6";
            tvApply.Nodes.Add(root4);

            TreeNode root2 = new TreeNode("���շ�����", 22, 22);
            root2.Tag = "4";
            tvApply.Nodes.Add(root2);

            TreeNode root3 = new TreeNode("����������", 22, 22);
            root3.Tag = "5";
            tvApply.Nodes.Add(root3);

           

            DateTime begin = DateTime.MinValue, end = DateTime.MinValue;

            begin = this.neuDateTimePicker1.Value;
            end = this.neuDateTimePicker2.Value.AddDays(1);
            if (begin >= end)
            {
                MessageBox.Show("��ʼʱ�䲻�ܴ��ڽ���ʱ��!", "��ʾ");
                return -1;
            }
            //��������������δ�Ǽǵ���Ч�����뵥
            System.Collections.ArrayList al;
           // al = Environment.OperationManager.GetOpsAppList(dept.ID, begin, end, "1");

            al = Environment.OperationManager.GetALLOpsApp(begin.ToString(), end.ToString());
            if (al != null)
            {
                foreach (FS.HISFC.Models.Operation.OperationAppllication apply in al)

                {
                    TreeNode node = new TreeNode();
                    node.Tag = apply;
                    node.ImageIndex = 20;
                    node.SelectedImageIndex = 21;
                    node.Text = apply.PatientInfo.Name +" "+ apply.PatientInfo.PVisit.PatientLocation.Bed.Name + "��";
                    node.Text = string.Concat("[", this.depts[apply.PatientInfo.PVisit.PatientLocation.NurseCell.ID].Name, "]  ",
                           node.Text);
                  // node.Text = string.Concat("[", this.depts[apply.PatientInfo.PVisit.PatientLocation.Dept.ID].Name, "]  ",
                    //    apply.PatientInfo.Name);
                    if (apply.OperateKind == "2")
                        node.Text = node.Text + "������";

                    if (apply.OperationInfos.Count > 0)
                        node.Text = node.Text + "����" + (apply.OperationInfos[0] as FS.HISFC.Models.Operation.OperationInfo).OperationItem.Name;

                    if (apply.IsValid)
                    {

                        if (apply.ExecStatus == "2")
                            if (apply.ApproveNote == "1")
                            {
                                node.Text = node.Text + "�������ͨ����";
                            }
                            else
                            {
                                node.Text = node.Text + "�����δͨ����";
                            }
                        if (apply.ExecStatus == "3")
                        {
                            node.Text = node.Text + "���Ѱ��š�";
                            root1.Nodes.Add(node);
                            continue;
                        }

                        else if (apply.ExecStatus == "4")
                        {
                            node.Text = node.Text + "�����շѡ�";
                            root2.Nodes.Add(node);
                            continue;
                        }
                        else if (apply.ExecStatus == "5")
                        {
                            node.Text = node.Text + "����ȡ����";
                            root3.Nodes.Add(node);
                            continue;
                        }
                        else if (apply.ExecStatus == "6")
                        {
                            node.Text = node.Text + "�������δ�շѡ�";
                            root4.Nodes.Add(node);
                            continue;
                        }
                        else
                        {
                            root.Nodes.Add(node);
                        }
                    }
                    else
                    {
                        node.Text = node.Text + "�������ϡ�";
                        root3.Nodes.Add(node);
                    }
                }
            }
            tvApply.ExpandAll();

            return 0;
        }

        /// <summary>
        /// ��ӻ����µ����뵥�б�
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        private int AddApply(NeuObject dept)
        {
            DateTime begin = DateTime.MinValue, end = DateTime.MinValue;
            if (this.GetDateTime(ref begin, ref end) == -1) return -1;
            //���뵥�б�
            ArrayList al = Environment.OperationManager.GetOpsAppList(dept, begin, end);
            foreach (TreeNode node in this.tvApply.Nodes[0].Nodes)
            {
                PatientInfo patient = node.Tag as PatientInfo;

                for (int i = al.Count - 1; i >= 0; i--)
                {
                    OperationAppllication apply = (OperationAppllication)al[i];
                    if (apply.PatientInfo.ID == patient.ID)
                    {
                        TreeNode child = new TreeNode();
                        if (apply.OperateKind == "2")
                        {
                            child.Text = "������";
                            child.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            child.ForeColor = System.Drawing.Color.Black;
                        }

                        if (apply.OperationInfos.Count > 0)
                            child.Text = child.Text + (apply.OperationInfos[0] as OperationInfo).OperationItem.Name;
                        if (apply.ExecStatus == "2") child.Text = child.Text + "������ˡ�";
                        if (apply.ExecStatus == "3") child.Text = child.Text + "���Ѱ��š�";
                        if (apply.ExecStatus == "4") child.Text = child.Text + "���ѵǼǡ�";
                        if (apply.ExecStatus == "5") child.Text = child.Text + "����ȡ���Ǽǡ�";
                        if (apply.ExecStatus == "6") child.Text = child.Text + "�������δ�շѡ�";
                        if (apply.IsValid == false) child.Text = child.Text + "����ȡ�����롻";
                        if (child.Text == "") child.Text = apply.PreDate.ToString();

                        child.Tag = apply;
                        child.ImageIndex = 2;
                        child.SelectedImageIndex = 2;
                        node.Nodes.Add(child);
                    }
                }
                node.Expand();
            }

            return 0;
        }

        /// <summary>
        /// ��ȡ��ѯ��ʼ������ʱ��
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private int GetDateTime(ref DateTime begin, ref DateTime end)
        {
            DateTime d1, d2;
            d1 = this.neuDateTimePicker1.Value;
            d2 = this.neuDateTimePicker2.Value.AddDays(1);

            if (d1 > d2)
            {
                MessageBox.Show("��ʼʱ�䲻�ܴ��ڽ���ʱ��!", "��ʾ");
                return -1;
            }
            begin = d1;
            end = d2;

            return 0;
        }

        #endregion

        #region �¼�

        protected override void OnLoad(EventArgs e)
        {
            this.ucApplicationForm1.Clean();
            Department dept = this.integrateManager.GetDepartment(Environment.OperatorDeptID);

            if (dept.SpecialFlag == "1")    //�������ң����ɸ������ҵ�ȫ�����뵥
            {
                this.RefreshApply(Environment.OperatorDept);
            }
            else                           //���������ң����ɸÿ��������뵥
            {
                FS.HISFC.BizProcess.Integrate.RADT manager = new FS.HISFC.BizProcess.Integrate.RADT();
                string deptID = dept.ID;
                ArrayList alPatients = manager.QueryPatient(deptID, FS.HISFC.Models.Base.EnumInState.I);

                this.tvApply.Nodes.Clear();
                TreeNode root = new TreeNode();
                root.Text = "���ƻ���";
                this.tvApply.Nodes.Add(root);
                foreach (FS.HISFC.Models.RADT.PatientInfo patient in alPatients)
                {

                    TreeNode node = new TreeNode();
                    

                    string bedNO = "["+patient.PVisit.PatientLocation.Bed.ID.Substring(4,patient.PVisit.PatientLocation.Bed.ID.Length -4) + "]";
                    string patientNO = "[" + patient.PID.PatientNO + "]";

                    node.Text = bedNO + "-" + patientNO + patient.Name;

                    if (patient.Sex.ID.ToString() == "M")
                    {
                        node.ImageIndex = 1;
                        node.SelectedImageIndex = 1;
                    }
                    else
                    {
                        node.ImageIndex = 3;
                        node.SelectedImageIndex = 3;
                    }
                    node.Tag = patient;
                    root.Nodes.Add(node);
                }
                this.AddApply(Environment.OperatorDept);
                tvApply.ExpandAll();
            }

            base.OnLoad(e);
        }

        private void neuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void tvPatientList1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag == null)
                return;

            //if (e.Node.Tag.GetType() == typeof(PatientInfo))
            //{
            //    PatientInfo patient = e.Node.Tag as PatientInfo;
            //    this.ucQueryInpatientNo1.Text = patient.PID.PatientNO;
            //    this.ucApplicationForm1.PatientInfo = patient;
            //}
            //else if (e.Node.Tag.GetType() == typeof(OperationAppllication))
            //{
            //    OperationAppllication application = e.Node.Tag as OperationAppllication;
            //    this.ucQueryInpatientNo1.Text = application.PatientInfo.PID.PatientNO;
            //    this.ucApplicationForm1.OperationApplication = application;

            //    this.currentNode = e.Node;
            //}
            if (e.Node.Tag.GetType() == typeof(PatientInfo))
            {
                PatientInfo patient = e.Node.Tag as PatientInfo;
                if (patient.PVisit.InState.ID.ToString() != "N" && patient.PVisit.InState.ID.ToString() != "O")
                {
                    this.ucApplicationForm1.PatientInfo = patient;
                    this.ucQueryInpatientNo1.Text = patient.PID.PatientNO;
                }
                else
                {
                    MessageBox.Show("���߲�����Ժ״̬��");

                }
                //this.ucQueryInpatientNo1.Text = patient.PID.PatientNO;
                //this.ucApplicationForm1.PatientInfo = patient;
            }
            else if (e.Node.Tag.GetType() == typeof(OperationAppllication))
            {
                OperationAppllication application = e.Node.Tag as OperationAppllication;
                if (application.PatientInfo.PVisit.InState.ID.ToString() != "N" && application.PatientInfo.PVisit.InState.ID.ToString() != "O")
                {
                    this.ucQueryInpatientNo1.Text = application.PatientInfo.PID.PatientNO;
                    this.ucApplicationForm1.OperationApplication = application;
                    this.currentNode = e.Node;
                }
                else
                {
                    MessageBox.Show("���߲�����Ժ״̬��");
                }

            }//{757094AC-55CD-428c-8C81-BB1F3F76172D}
        }

        public override int Save(object sender, object neuObject)
        {
            if (this.ucApplicationForm1.Save() >= 0)
            {
                //this.ucApplicationForm1.OperationApplication = new OperationAppllication();
                this.Query(sender, neuObject);
            }

            if ((!this.ucApplicationForm1.IsNew) && this.currentNode != null)
            {
                //{924DB426-6487-4bde-9365-48E3C496DA19}
                //this.currentNode.Text = this.ucApplicationForm1.OperationApplication.MainOperationName;
                //this.currentNode = null;
            }

            if (this.isSaveClose)
            {
                this.ParentForm.DialogResult = DialogResult.OK;
                this.ParentForm.Close();
            }


            return base.Save(sender, neuObject);
        }

        public override int Query(object sender, object neuObject)
        {
            this.OnLoad(null);
            return base.Query(sender, neuObject);
        }
        public override int Print(object sender, object neuObject)
        {
            return this.ucApplicationForm1.Print();
        }
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("����", "����", 1, true, false, null);
            this.toolBarService.AddToolButton("����", "����״̬�Ѿ�����", (int)FS.FrameWork.WinForms.Classes.EnumImageList.A����, true, false, null);
            this.toolBarService.AddToolButton("���շ�", "���շѺ��߳�Ժ������ʾû���շ�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            this.toolBarService.AddToolButton("ȡ���շ�", "ȡ���շ�״̬", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B��ҩ̨ɾ��, true, false, null);


            return this.toolBarService;
        }

        //public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        //{
          
        //    base.ToolStrip_ItemClicked(sender, e);
        //}


        private void ucQueryInpatientNo1_myEvent()
        {
            if (ucQueryInpatientNo1.InpatientNo == "")
            {
                MessageBox.Show("û�иû�����Ϣ!", "��ʾ");
                ucQueryInpatientNo1.Focus();
                return;
            }

            PatientInfo patient = Environment.RadtManager.GetPatientInfomation(this.ucQueryInpatientNo1.InpatientNo);
            //{757094AC-55CD-428c-8C81-BB1F3F76172D}
            //this.ucApplicationForm1.PatientInfo = patient;
            if (patient.PVisit.InState.ID.ToString() != "N" && patient.PVisit.InState.ID.ToString() != "O")
            {
                this.ucApplicationForm1.PatientInfo = patient;
            }
            else
            {
                MessageBox.Show("���߲�����Ժ״̬��");

            }
        }

        #endregion

        private void neuDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //DateTime begin = DateTime.MinValue, end = DateTime.MinValue;

            //begin = this.neuDateTimePicker1.Value;
            //end = this.neuDateTimePicker2.Value.AddDays(1);
            //if (begin >= end)
            //{
            //    MessageBox.Show("��ʼʱ�䲻�ܴ��ڽ���ʱ��!", "��ʾ");

            //}
        }


       

        private int Update(string stateid)
        {
           return  this.ucApplicationForm1.Update(stateid);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    this.Update("3");
                    this.OnLoad(null);
                    break;
                case "���շ�":
                    this.Update("4");
                    this.OnLoad(null);
                    break;
                case "����":
                 
                    if(this.ucApplicationForm1.Cancel() >= 0)
                        {
                            this.OnLoad(null);
                        }
                    break;
                case "ȡ���շ�":
                    this.Update("3");
                    this.OnLoad(null);
                    break;

                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
    }
}
