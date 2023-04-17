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
    public partial class ucApplication : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucApplication()
        {
            InitializeComponent();
            this.neuDateTimePicker2.Value = DateTime.Now.Date.AddDays(1);
            this.Init();
            this.isSaveClose = false;
        }

        public ucApplication(PatientInfo patient, NeuObject item)
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

        private bool isCancleApply = false;

        private int flag = 0;

        ArrayList alText = new ArrayList();//TEXT����
        ArrayList alChoose = new ArrayList();//ת���������
       // {0E140FEC-2F31-4414-8401-86E78FA3ADDC}
        public FS.HISFC.Models.Operation.OperationAppllication operationNewZyByMz; //������������ת��ΪסԺ��������

        #endregion

        #region ����

        [Category("�ؼ�����"), Description("������Ƿ���ʾ��ӡ")]
        public bool IsSavePrint
        {
            get
            {
                return ucApplicationForm1.IsSavePrint;
            }
            set
            {
                ucApplicationForm1.IsSavePrint = value;
            }
        }
        

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

        [Category("�ؼ�����"), Description("�Ѱ��������Ƿ���������������")]
        public bool IsCancleApply
        {
            get
            {
                return isCancleApply;
            }
            set
            {
                isCancleApply = value;
            }
        }

        // {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
        [Category("�ؼ�����"), Description("���������Ƿ񰴸���Ȩ�޻�ȡ������ҽ�������ȡ��������")]
        public bool IsOwnPrivilege
        {
            get
            {
                return ucApplicationForm1.IsOwnPrivilege;
            }
            set
            {
                ucApplicationForm1.IsOwnPrivilege = value;
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

            components.Add(this.ucApplicationForm1.rtbApplyNote);
            this.ucUserText1.SetControl(this.components);
            this.ucUserText1.OnChange += new EventHandler(ucUserText1_OnChange);
        }


        private void ucUserText1_OnChange(Object sender, EventArgs e)
        {
            SetUeserText();
        }

        /// <summary>
        /// ���ó�����
        /// </summary>
        private void SetUeserText()
        {
            try
            {

                string id = (this.integrateManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID;
                this.alText.Clear();
                this.alChoose.Clear();

                //���ݿ��һ�ó�����
                this.alText = FS.HISFC.Components.Order.CacheManager.InterMgr.GetList(id, 1);
                //���˳�����
                this.alText.AddRange(FS.HISFC.Components.Order.CacheManager.InterMgr.GetList((this.integrateManager.Operator as FS.HISFC.Models.Base.Employee).ID, 0));

                for (int i = 0; i < this.alText.Count; i++)
                {
                    FS.HISFC.Models.Base.UserText txt = alText[i] as FS.HISFC.Models.Base.UserText;
                    if (txt == null)
                        continue;
                    //ת�����е��ı���ƴ����
                    #region �޸�ȡ����ƴ���� {C8B64A7F-A732-40c6-9577-BDE3DD90D521}
                    txt.SpellCode = FS.HISFC.Components.Order.CacheManager.InterMgr.Get(txt.Name).SpellCode;
                    txt.User01 = txt.Text;
                    this.alChoose.Add(txt);
                    #endregion

                }
            }
            catch
            {
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
            TreeNode root = new TreeNode(dept.Name, 22, 22);
            root.Tag = "OPS";
            tvApply.Nodes.Add(root);

            DateTime begin = DateTime.MinValue, end = DateTime.MinValue;

            begin = this.neuDateTimePicker1.Value.Date;
            end = this.neuDateTimePicker2.Value.Date.AddDays(1);
            if (begin >= end)
            {
                MessageBox.Show("��ʼʱ�䲻�ܴ��ڽ���ʱ��!", "��ʾ");
                return -1;
            }
            //��������������δ�Ǽǵ���Ч�����뵥
            System.Collections.ArrayList al;
            al = Environment.OperationManager.GetOpsAppList(dept.ID, begin, end, "1");
            if (al != null)
            {
                foreach (FS.HISFC.Models.Operation.OperationAppllication apply in al)
                {
                    TreeNode node = new TreeNode();
                    node.Text = string.Concat("[", this.depts[apply.PatientInfo.PVisit.PatientLocation.Dept.ID].Name, "]  ",
                        apply.PatientInfo.Name);
                    if (apply.OperateKind == "2")
                        node.Text = node.Text + "������";

                    if (apply.OperationInfos.Count > 0)
                        node.Text = node.Text + "����" + (apply.OperationInfos[0] as FS.HISFC.Models.Operation.OperationInfo).OperationItem.Name;
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
                        node.Text = node.Text + "���Ѱ��š�";
                        

                    node.Tag = apply;
                    node.ImageIndex = 20;
                    node.SelectedImageIndex = 21;
                    root.Nodes.Add(node);
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
                            if (apply.ExecStatus == "3")
                            {
                                child.Text = "������";
                                child.ForeColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                child.Text = "������";
                                child.ForeColor = System.Drawing.Color.Red;
                            }                            
                        }
                        else
                        {
                            if (apply.ExecStatus == "3")
                            {
                                child.ForeColor = System.Drawing.Color.Blue;
                            }
                            else
                            {
                                child.ForeColor = System.Drawing.Color.Black;
                            }                            
                        }

                        if (apply.OperationInfos.Count > 0)
                            child.Text = child.Text + (apply.OperationInfos[0] as OperationInfo).OperationItem.Name;
                        if (apply.ExecStatus == "1") flag = 0;
                        if (apply.ExecStatus == "2") child.Text = child.Text + "������ˡ�"; flag = 1;
                        if (apply.ExecStatus == "3") child.Text = child.Text + "���Ѱ��š�"; flag = 1;
                        if (apply.ExecStatus == "4") child.Text = child.Text + "���ѵǼǡ�"; flag = 1;
                        if (apply.ExecStatus == "5") child.Text = child.Text + "����ȡ���Ǽǡ�"; flag = 0;
                        if (apply.ExecStatus == "6") child.Text = child.Text + "�������δ�շѡ�"; flag = 1;
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
            d1 = this.neuDateTimePicker1.Value.Date;
            d2 = this.neuDateTimePicker2.Value.Date.AddDays(1);

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
                //{0E140FEC-2F31-4414-8401-86E78FA3ADDC}
                PatientInfo patient = e.Node.Tag as PatientInfo;
                frmApplyMZList form = new frmApplyMZList(patient);
                if (form.applynum > 0)
                {
                    form.ShowDialog();
                }
                else
                {
                    form.Dispose();
                }
                
                if (form.operation != null)
                {
                    this.ucApplicationForm1.PatientInfo = patient;
                    this.ucApplicationForm1.applyoldMZ = form.operation;
                    this.ucApplicationForm1.SetOperationApplyBYMz();
                    #region
                    //this.operationNewZyByMz = form.operation; //������������
                    //this.ucApplicationForm1.applyoldMZ = form.operation.Clone();//���þɵ�������󵽴��壬׼����������������ɱ�־
                   
                    ////ת��Ϊ�µ�סԺ��������
                    //operationNewZyByMz.PatientSouce = "2";
                    //operationNewZyByMz.PatientInfo.ID = patient.ID;
                    //operationNewZyByMz.PatientInfo.PID.PatientNO = patient.PID.PatientNO;
                    //operationNewZyByMz.ID = "";

                    
                    //this.ucApplicationForm1.OperationApplication = operationNewZyByMz;//���þ�����������¼����סԺ������¼Ĭ��ֵ


                    // this.ucApplicationForm1.IsNew = true;//�µ�����
                    #endregion
                    this.ucQueryInpatientNo1.Text = patient.PID.PatientNO;
                    
                    return;
                }
                if (patient.PVisit.InState.ID.ToString() != "N" && patient.PVisit.InState.ID.ToString() != "O") 
                {
                    if (patient.PVisit.InState.ID.ToString() == "B")
                    {
                        MessageBox.Show("��Ժ�Ǽǻ��߲����������������룡", "��ʾ");
                    }
                    else
                    {
                        this.ucApplicationForm1.PatientInfo = patient;
                        this.ucQueryInpatientNo1.Text = patient.PID.PatientNO;
                    }
                    
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
                    if (application.PatientInfo.PVisit.InState.ID.ToString() == "B")
                    {
                        MessageBox.Show("��Ժ�Ǽǻ��߲����������������룡", "��ʾ");
                    }
                    else 
                    {
                        this.ucQueryInpatientNo1.Text = application.PatientInfo.PID.PatientNO;
                        this.ucApplicationForm1.OperationApplication = application;
                       // this.ucApplicationForm1.applyoldMZ = form.operation;
                        this.currentNode = e.Node;
                    }                   
                }
                else
                {
                    MessageBox.Show("���߲�����Ժ״̬��");
                }

                if (application.ExecStatus == "1")
                {
                    flag = 0;
                }
                else
                {
                    flag = 1;
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
            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {   
            if (e.ClickedItem.Text == "����")
            {
                FS.HISFC.Models.Operation.OperationAppllication application = Environment.OperationManager.GetOpsApp(this.OperationApplication.ID);
                if (application.ExecStatus == "1")
                {
                    flag = 0;
                }
                else
                {
                    flag = 1;
                }
                if (flag == 1)
                {
                    if (this.IsCancleApply)
                    {
                        if (this.ucApplicationForm1.CancelApply() == -1)
                        {
                            this.OnLoad(null);
                        }
                    }
                    else
                    {
                        MessageBox.Show("�����Ѱ��ţ�����ʧ�ܣ���Ҫȡ���뼰ʱ��ϵ�����ң�", "��ʾ");
                    }
                }
                else
                {
                    if (this.ucApplicationForm1.Cancel() >= 0)
                    {
                        this.OnLoad(null);
                    }
                }                
            }
            base.ToolStrip_ItemClicked(sender, e);
        }


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
                if (patient.PVisit.InState.ID.ToString() == "B")
                {
                    MessageBox.Show("��Ժ�Ǽǻ��߲��������������룡", "��ʾ");
                }
                else
                {
                    this.ucApplicationForm1.PatientInfo = patient;
                }                
            }
            else
            {
                MessageBox.Show("���߲�����Ժ״̬��");

            }
        }

        #endregion

        private void neuDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime begin = DateTime.MinValue, end = DateTime.MinValue;

            begin = this.neuDateTimePicker1.Value.Date;
            end = this.neuDateTimePicker2.Value.Date.AddDays(1);
            if (begin >= end)
            {
                MessageBox.Show("��ʼʱ�䲻�ܴ��ڽ���ʱ��!", "��ʾ");

            }
        }

       
    }
}
