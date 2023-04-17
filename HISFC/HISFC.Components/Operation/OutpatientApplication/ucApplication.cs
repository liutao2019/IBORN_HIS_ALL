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

namespace FS.HISFC.Components.Operation.OutpatientApplication
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
            this.Init();
            this.isSaveClose = false;
        }

        public ucApplication(FS.HISFC.Models.Registration.Register regObj, NeuObject item)
            : this()
        {
            PatientInfo patientInfo = new PatientInfo();
            patientInfo.ID = regObj.ID;//��ˮ��
            patientInfo.PID.PatientNO = regObj.PID.CardNO;//����
            patientInfo.PID.CardNO = regObj.PID.CardNO;//����
            patientInfo.Name = regObj.Name;//����
            patientInfo.Birthday = regObj.Birthday;
            patientInfo.Sex.ID = regObj.Sex.ID;
            if (regObj.SeeDoct.Dept.ID == null || regObj.SeeDoct.Dept.ID == "")
            {
                patientInfo.PVisit.PatientLocation.Dept.ID = regObj.DoctorInfo.Templet.Dept.ID;
                patientInfo.PVisit.PatientLocation.Dept.Name = regObj.DoctorInfo.Templet.Dept.Name;
            }
            else
            {
                patientInfo.PVisit.PatientLocation.Dept.ID = regObj.SeeDoct.Dept.ID;
            }
            patientInfo.Pact.PayKind.ID = regObj.Pact.PayKind.ID;
            this.txtCard.Text = patientInfo.PID.PatientNO;
            this.ucApplicationForm1.PatientInfo = patientInfo;
            this.ucApplicationForm1.MainOperation = item.Clone();

            this.isSaveClose = true;
        }

        #region �ֶ�
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        ArrayList alText = new ArrayList();//TEXT����
        ArrayList alChoose = new ArrayList();//ת���������


        private FS.HISFC.BizProcess.Integrate.Manager integrateManager = new FS.HISFC.BizProcess.Integrate.Manager();
        private Dictionary<string, Department> depts = new Dictionary<string, Department>();

        private bool isSaveClose;   //�Ƿ񱣴���Զ��ر�

        private TreeNode currentNode;
        #endregion

        #region ����
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
        // {84F6ADEA-7781-4560-8D59-AE7D95754D5D}
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

            begin = this.neuDateTimePicker1.Value;
            end = this.neuDateTimePicker2.Value.AddDays(1);
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
            //{2D90BEDC-96B2-4a4f-8197-21DF10F5EE17}
            end = end.AddYears(1);
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
            //{2D90BEDC-96B2-4a4f-8197-21DF10F5EE17}
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
                FS.HISFC.BizProcess.Integrate.Registration.Registration manager = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
                string emplId = Environment.OperatorID;// FS.FrameWork.Management.Connection.Operator.ID;
                DateTime dtBegin=this.neuDateTimePicker1.Value.Date;
                //{2D90BEDC-96B2-4a4f-8197-21DF10F5EE17}
                //DateTime dtEnd = this.neuDateTimePicker2.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                DateTime dtEnd = this.neuDateTimePicker2.Value.Date.AddDays(1);
                ArrayList alReg = new ArrayList();
                if (neuchops.Checked == true)
                {
                    //����������ʱ�����Ϣ{A448C42B-AEA2-4a36-889C-C5AB97C38A6B}
                alReg=manager.QueryBySeeDocAndSeeDate2(emplId, dtBegin, dtEnd, true);
                }
                else
                {
                     alReg = manager.QueryBySeeDocAndSeeDate(emplId, dtBegin, dtEnd, true);

                } ArrayList alPatients = new ArrayList();
                this.tvApply.Nodes.Clear();
                TreeNode root = new TreeNode();
                root.Text = "���ƻ���";
                this.tvApply.Nodes.Add(root);
                PatientInfo patientInfo = null;
                foreach (FS.HISFC.Models.Registration.Register regObj in alReg)
                {
                    patientInfo = new PatientInfo();
                    patientInfo.ID = regObj.ID;//��ˮ��
                    patientInfo.PID.PatientNO = regObj.PID.CardNO;//����
                    patientInfo.PID.CardNO = regObj.PID.CardNO;//����
                    patientInfo.Name = regObj.Name;//����
                    patientInfo.Birthday = regObj.Birthday;
                    patientInfo.Sex.ID = regObj.Sex.ID;
                    if (regObj.SeeDoct.Dept.ID == null || regObj.SeeDoct.Dept.ID == "")
                    {
                        patientInfo.PVisit.PatientLocation.Dept.ID = regObj.DoctorInfo.Templet.Dept.ID;
                        patientInfo.PVisit.PatientLocation.Dept.Name = regObj.DoctorInfo.Templet.Dept.Name;
                    }
                    else
                    {
                        //{2D90BEDC-96B2-4a4f-8197-21DF10F5EE17}
                        //patientInfo.PVisit.PatientLocation.Dept.ID = regObj.SeeDoct.Dept.ID;
                        patientInfo.PVisit.PatientLocation.Dept = Environment.IntegrateManager.GetDepartment(regObj.SeeDoct.Dept.ID);
                    }
                    patientInfo.PVisit.InTime = regObj.SeeDoct.OperTime;
                    patientInfo.Pact.PayKind.ID = regObj.Pact.PayKind.ID;

                    alPatients.Add(patientInfo);
                }

                foreach (FS.HISFC.Models.RADT.PatientInfo patient in alPatients)
                {

                    TreeNode node = new TreeNode();
                    

                    string seeDate = "["+patient.PVisit.InTime.ToString() + "]";
                    string patientNO = "[" + patient.PID.CardNO + "]";

                    node.Text = patientNO + patient.Name + "-" + seeDate;

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

            if (e.Node.Tag.GetType() == typeof(PatientInfo))
            {
                PatientInfo patient = e.Node.Tag as PatientInfo;
                if (patient.PVisit.InState.ID.ToString() != "N" && patient.PVisit.InState.ID.ToString() != "O")
                {
                    this.ucApplicationForm1.PatientInfo = patient;
                    this.txtCard.Text = patient.PID.PatientNO;
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

                this.txtCard.Text = application.PatientInfo.PID.PatientNO;
                this.ucApplicationForm1.OperationApplication = application;
                this.currentNode = e.Node;
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
                if (this.ucApplicationForm1.Cancel() >= 0)
                {
                    this.OnLoad(null);
                }
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        #endregion

        private void neuDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime begin = DateTime.MinValue, end = DateTime.MinValue;

            begin = this.neuDateTimePicker1.Value;
            end = this.neuDateTimePicker2.Value.AddDays(1);
            if (begin >= end)
            {
                MessageBox.Show("��ʼʱ�䲻�ܴ��ڽ���ʱ��!", "��ʾ");

            }
        }

        private void txtCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                //�Ȳ��ҹҺű�
                string cardNo = this.txtCard.Text.PadLeft(10, '0');
                FS.HISFC.BizProcess.Integrate.Registration.Registration regMana = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
                ArrayList alReg = regMana.Query(cardNo, System.DateTime.Now.AddYears(-1));
                if (alReg == null || alReg.Count == 0)
                {
                    MessageBox.Show("û�иû��ߵĹҺ���Ϣ");
                    return;
                }
                FS.HISFC.Models.Registration.Register regObj = new FS.HISFC.Models.Registration.Register();
                regObj = alReg[0] as FS.HISFC.Models.Registration.Register;
                PatientInfo patientInfo = new PatientInfo();
                patientInfo.ID = regObj.ID;//��ˮ��
                patientInfo.PID.PatientNO = regObj.PID.CardNO;//����
                patientInfo.PID.CardNO = regObj.PID.CardNO;//����
                patientInfo.Name = regObj.Name;//����
                patientInfo.Birthday = regObj.Birthday;
                patientInfo.Sex.ID = regObj.Sex.ID;
                patientInfo.PhoneHome = regObj.PhoneHome; //�绰 //{0a73b038-1b02-4881-b4e3-31728e3e8c4a}
                if (regObj.SeeDoct.Dept.ID == null || regObj.SeeDoct.Dept.ID == "")
                {
                    patientInfo.PVisit.PatientLocation.Dept.ID = regObj.DoctorInfo.Templet.Dept.ID;
                    patientInfo.PVisit.PatientLocation.Dept.Name = regObj.DoctorInfo.Templet.Dept.Name;
                }
                else
                {
                    patientInfo.PVisit.PatientLocation.Dept.ID = regObj.SeeDoct.Dept.ID;
                }
                patientInfo.Pact.PayKind.ID = regObj.Pact.PayKind.ID;

                this.ucApplicationForm1.PatientInfo = patientInfo;
            }
        }
    }
}
