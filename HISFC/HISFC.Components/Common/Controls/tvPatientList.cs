using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
namespace FS.HISFC.Components.Common.Controls
{
   
    /// <summary>
    /// [��������: �����б���]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class tvPatientList : FS.FrameWork.WinForms.Controls.NeuTreeView
    {

        /// <summary>
        /// �޲ι��캯��
        /// </summary>
        public tvPatientList()
        {
            // Windows.Forms ��׫д�����֧���������
            InitializeComponent();
            //��ʼ��
            this.Init();
        }

        private System.Windows.Forms.ImageList imageList1;
        private System.ComponentModel.IContainer components;

        #region �����������ɵĴ���


        ///// <summary>
        ///// �вι��캯��
        ///// </summary>
        ///// <param name="container">�ӿ�</param>
        //public tvPatientList(System.ComponentModel.IContainer container)
        //{
        //    // Windows.Forms ��׫д�����֧���������
        //    container.Add(this);
        //    InitializeComponent();
        //    this.Init();//��ʼ��
        //    //
        //    // TODO: �� InitializeComponent ���ú�����κι��캯������
        //    //
        //}

        /// <summary> 
        /// ������������ʹ�õ���Դ��
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tvPatientList));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "dir_close.bmp");
            this.imageList1.Images.SetKeyName(1, "dir_open.bmp");
            this.imageList1.Images.SetKeyName(2, "hourse.bmp");
            this.imageList1.Images.SetKeyName(3, "hourse1.bmp");
            this.imageList1.Images.SetKeyName(4, "36-2.bmp");
            this.imageList1.Images.SetKeyName(5, "36-3.bmp");
            this.imageList1.Images.SetKeyName(6, "47-2.gif");
            this.imageList1.Images.SetKeyName(7, "47-1.gif");
            this.imageList1.Images.SetKeyName(8, "82-2.bmp");
            this.imageList1.Images.SetKeyName(9, "82.bmp");
            this.imageList1.Images.SetKeyName(10, "40-2.bmp");
            this.imageList1.Images.SetKeyName(11, "40.bmp");
            this.imageList1.Images.SetKeyName(12, "097.GIF");
            this.imageList1.Images.SetKeyName(13, "blank.JPG");//{839D3A8A-49FA-4d47-A022-6196EB1A5715}
            // 
            // tvPatientList
            // 
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.LineColor = System.Drawing.Color.Black;
            this.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(tvPatientList_AfterSelect);
            this.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvPatientList_AfterCheck);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tvPatientList_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvPatientList_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion
        
        #region ö��
        /// <summary>
        /// ��ʾ������Ϣ-סԺ�ţ����ң���������Ժ״̬
        /// </summary>
        public enum enuShowType
        {
            /// <summary>
            /// ����ʾ
            /// </summary>
            None = 0,

            /// <summary>
            /// סԺ��
            /// </summary>
            InpatientNo = 1,

            /// <summary>
            /// ����
            /// </summary>
            Dept = 3,

            /// <summary>
            /// ����
            /// </summary>
            Bed = 5,

            /// <summary>
            /// ״̬
            /// </summary>
            Status = 7
        }

        /// <summary>
        /// ��ʾ��Ϣ����ǰ�棬����(���������෴�ķ���)
        /// </summary>
        public enum enuShowDirection
        {
            /// <summary>
            /// ��������ǰ
            /// </summary>
            Ahead,

            /// <summary>
            /// ����������
            /// </summary>
            Behind
        }

        /// <summary>
        /// ѡ������
        /// </summary>
        public enum enuChecked
        {
            /// <summary>
            /// û�й�ѡ��
            /// </summary>
            None,

            /// <summary>
            /// ��ѡ��
            /// </summary>
            Radio,

            /// <summary>
            /// ��ѡ��
            /// </summary>
            MultiSelect
        }
        #endregion

        #region ����

        protected ArrayList myPatients = new ArrayList();

        /// <summary>
        /// Ĭ����ʾ����
        /// </summary>
        private enuShowType myShowType = enuShowType.Bed;

        /// <summary>
        /// Ĭ�ϲ���ʾCheckBox
        /// </summary>
        private enuChecked myChecked = enuChecked.None;

        /// <summary>
        /// Ĭ��������Ϣ����ǰ��,�������ں���
        /// </summary>
        private enuShowDirection myDirection = enuShowDirection.Ahead;

        /// <summary>
        /// Ĭ������ǵ�����Ժ�Ļ���,��ʾ���¡�
        /// </summary>
        private bool bIsShowNewPatient = true;

        private bool bControlChecked = false;

        /// <summary>
        /// ����
        /// </summary>
        protected DateTime dtToday;

        /// <summary>
        /// �Ƿ���ʾtooltipסԺ��
        /// </summary>
        protected bool bIsShowPatientNo = true;

        /// <summary>
        /// �Ƿ���ʾ�ڵ�����
        /// </summary>
        protected bool bIsShowCount = true;

        /// <summary>
        /// ���ڵ���ʾͼƬ
        /// </summary>
        public int RootImageIndex = 0;

        /// <summary>
        /// ���ڵ�ѡ��ͼƬ
        /// </summary>
        public int RootSelectedImageIndex = 1;

        /// <summary>
        /// ���ҽڵ���ʾͼƬ
        /// </summary>
        public int BranchImageIndex = 2;

        /// <summary>
        /// ���ҽڵ�ѡ��ͼƬ
        /// </summary>
        public int BranchSelectedImageIndex = 3;

        /// <summary>
        /// �л���ͼƬ
        /// </summary>
        public int MaleImageIndex = 4;

        /// <summary>
        /// �л���ѡ��ͼƬ
        /// </summary>
        public int MaleSelectedImageIndex = 5;

        /// <summary>
        /// Ů����ͼƬ
        /// </summary>
        public int FemaleImageIndex = 6;

        /// <summary>
        /// Ů���߽ڵ�ѡ��ͼƬ
        /// </summary>
        public int FemaleSelectedImageIndex = 7;

        /// <summary>
        /// �к���ʾͼƬ
        /// </summary>
        public int BabyImageIndex = 8;

        /// <summary>
        /// Ů����ʾͼƬ
        /// </summary>
        public int GirlImageIndex = 10;


        public int LeaveImageIndex = 12;
        public int BlankImageIndex = 13;//{839D3A8A-49FA-4d47-A022-6196EB1A5715}

        #endregion

        #region ����
        /// <summary>
        /// �Ƿ���ʾ�µĻ���
        /// </summary>
        public bool IsShowNewPatient
        {
            get
            {
                return this.bIsShowNewPatient;
            }
            set
            {
                this.bIsShowNewPatient = value;
            }
        }

        /// <summary>
        /// ��ʾ����
        /// </summary>
        public enuShowType ShowType
        {
            get
            {
                return this.myShowType;
            }
            set
            {
                this.myShowType = value;
            }
        }

        /// <summary>
        ///// ˢ���б�
        ///// </summary>
        protected virtual void RefreshList()
        {
            this.Nodes.Clear();
            int Branch = 0;
            if (myPatients.Count == 0) this.AddRootNode();
            for (int i = 0; i < myPatients.Count; i++)
            {
                System.Windows.Forms.TreeNode newNode = new System.Windows.Forms.TreeNode();
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                //����ΪҶ
                if (myPatients[i].GetType().ToString() == "FS.HISFC.Models.RADT.PatientInfo")
                {
                    try
                    {
                        FS.HISFC.Models.RADT.PatientInfo PatientInfo = (FS.HISFC.Models.RADT.PatientInfo)myPatients[i];
                        obj.ID = PatientInfo.PID.PatientNO;
                        obj.Name = PatientInfo.Name;
                        try
                        {
                            obj.Memo = PatientInfo.PVisit.PatientLocation.Bed.ID;
                            try
                            {	//���
                                if (PatientInfo.PVisit.PatientLocation.Bed.Status.ID.ToString() == "R")
                                {
                                    obj.Name = obj.Name + "����١�";
                                }
                            }
                            catch { }
                        }
                        catch
                        {//�޲�����Ϣ
                        }
                        obj.User01 = PatientInfo.PVisit.PatientLocation.Dept.Name;
                        obj.User02 = PatientInfo.PVisit.InState.Name;
                        obj.User03 = PatientInfo.Sex.ID.ToString();
                        //��Ժ������24Сʱ,��ʾ(��)
                        if (this.bIsShowNewPatient)
                        {
                            if (dtToday < PatientInfo.PVisit.InTime.Date.AddDays(1)) obj.Name = obj.Name + "(��)";
                        }
                        this.AddTreeNode(Branch, obj, PatientInfo.ID, PatientInfo);
                    }
                    catch { }
                }
                else if (myPatients[i].GetType().ToString() == "FS.HISFC.Models.RADT.Patient")
                {
                    FS.HISFC.Models.RADT.Patient PatientInfo = (FS.HISFC.Models.RADT.Patient)myPatients[i];
                    obj.ID = PatientInfo.PID.PatientNO;
                    obj.Name = PatientInfo.Name;
                    obj.Memo = "";
                    obj.User01 = "";
                    obj.User02 = "";
                    obj.User03 = PatientInfo.Sex.ID.ToString();
                    this.AddTreeNode(Branch, obj, PatientInfo.ID, PatientInfo);

                }
                else if (myPatients[i].GetType().ToString() == "FS.FrameWork.Models.NeuObject")
                {
                    obj = (FS.FrameWork.Models.NeuObject)myPatients[i];
                    this.AddTreeNode(Branch, obj, obj.ID, obj);
                }
                else
                {//Ϊ��
                    //�ָ��ַ��� text|tag ��ʶ���
                    string all = myPatients[i].ToString();
                    string[] s = all.Split('|');

                    newNode.Text = s[0];

                    try
                    {
                        newNode.Name = s[1];
                        newNode.Tag = s[1];
                    }
                    catch { newNode.Tag = ""; }
                    try
                    {
                        newNode.ImageIndex = this.BranchImageIndex;
                        newNode.SelectedImageIndex = this.BranchSelectedImageIndex;
                    }
                    catch { }
                    Branch = this.Nodes.Add(newNode);
                }
            }
            if (this.bIsShowCount)
            {
                foreach (System.Windows.Forms.TreeNode node in this.Nodes)
                {

                    if (node.Tag == null || node.Tag.GetType().ToString() == "System.String")
                    {//���
                        int count = 0;
                        count = node.GetNodeCount(false);
                        node.Text = node.Text + "(" + count.ToString() + ")";
                    }
                }
            }
            this.ExpandAll();
            try//wolf added ensure node visible 
            {
                if (this.SelectedNode == null)
                {
                    try
                    {
                        this.SelectedNode = this.Nodes[0];
                    }
                    catch { }
                }
                this.SelectedNode.EnsureVisible();
            }
            catch { }

        }



        ///// <summary>
        ///// ˢ���б�// {019BAA9F-7AF8-4249-8815-E5ED2DEC940E}
        ///// </summary>
        //protected virtual void RefreshList()
        //{
        //    this.Nodes.Clear();
        //    int Branch = 0;
        //   // System.Collections.Generic.Dictionary<string, TreeNode> areaIndexDic = new System.Collections.Generic.Dictionary<string, TreeNode>();//��ʿվ��Ӧ�ڵ�
        //    System.Collections.Generic.Dictionary<int, System.Collections.Generic.Dictionary<string, TreeNode>> benchDic = new System.Collections.Generic.Dictionary<int, System.Collections.Generic.Dictionary<string, TreeNode>>();
        //    ArrayList listArea = new ArrayList();
        //    ArrayList listAreaNodes = new ArrayList();
        //    if (myPatients.Count == 0) this.AddRootNode();
        //    for (int i = 0; i < myPatients.Count; i++)
        //    {
        //        System.Windows.Forms.TreeNode newNode = new System.Windows.Forms.TreeNode();
        //        FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
        //        //����ΪҶ
        //        if (myPatients[i].GetType().ToString() == "FS.HISFC.Models.RADT.PatientInfo")
        //        {
        //            try
        //            {
        //                FS.HISFC.Models.RADT.PatientInfo PatientInfo = (FS.HISFC.Models.RADT.PatientInfo)myPatients[i];
        //                obj.ID = PatientInfo.PID.PatientNO;
        //                obj.Name = PatientInfo.Name;
        //                try
        //                {
        //                    obj.Memo = PatientInfo.PVisit.PatientLocation.Bed.ID;
        //                    try
        //                    {	//���
        //                        if (PatientInfo.PVisit.PatientLocation.Bed.Status.ID.ToString() == "R")
        //                        {
        //                            obj.Name = obj.Name + "����١�";
        //                        }
        //                    }
        //                    catch { }
        //                }
        //                catch
        //                {//�޲�����Ϣ
        //                }
        //                obj.User01 = PatientInfo.PVisit.PatientLocation.Dept.Name;
        //                obj.User02 = PatientInfo.PVisit.InState.Name;
        //                obj.User03 = PatientInfo.Sex.ID.ToString();
        //                //��Ժ������24Сʱ,��ʾ(��)
        //                if (this.bIsShowNewPatient)
        //                {
        //                    if (dtToday < PatientInfo.PVisit.InTime.Date.AddDays(1)) obj.Name = obj.Name + "(��)";
        //                }
        //                //this.AddTreeNode(Branch, obj, PatientInfo.ID, PatientInfo);
        //                string area = PatientInfo.PVisit.PatientLocation.NurseCell.Name;

        //                if (benchDic[Branch].ContainsKey(area) == false)
        //                {
        //                    // areaIndexDic.Add(area);

        //                    newNode.Text = area;
        //                    newNode.Name = "DeptPatient";
        //                    newNode.Tag = "";
        //                    newNode.ImageIndex = this.BranchImageIndex;
        //                    newNode.SelectedImageIndex = this.BranchSelectedImageIndex;
        //                    this.Nodes[Branch].Nodes.Add(newNode);
        //                    benchDic[Branch].Add(area, newNode);

        //                    System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode();
        //                    //����Ҫ��ӵĽڵ�
        //                    this.CreateNodeInfo(obj, PatientInfo, ref node);
        //                    newNode.Nodes.Add(node);
        //                }
        //                else
        //                {
        //                    // this.AddTreeNode(Branch, obj, PatientInfo.ID, PatientInfo);
        //                    System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode();
        //                    //����Ҫ��ӵĽڵ�
        //                    this.CreateNodeInfo(obj, PatientInfo, ref node);
        //                    benchDic[Branch][area].Nodes.Add(node);
        //                }

        //            }
        //            catch { }
        //        }
        //        else if (myPatients[i].GetType().ToString() == "FS.HISFC.Models.RADT.Patient")
        //        {
        //            FS.HISFC.Models.RADT.Patient PatientInfo = (FS.HISFC.Models.RADT.Patient)myPatients[i];
        //            obj.ID = PatientInfo.PID.PatientNO;
        //            obj.Name = PatientInfo.Name;
        //            obj.Memo = "";
        //            obj.User01 = "";
        //            obj.User02 = "";
        //            obj.User03 = PatientInfo.Sex.ID.ToString();
        //            this.AddTreeNode(Branch, obj, PatientInfo.ID, PatientInfo);
        //            //string area = PatientInfo.PVisit.PatientLocation.NurseCell.Name;
        //            //if (listArea.Contains(area) == false)
        //            //{
        //            //    listArea.Add(area);
        //            //    newNode.Text = area;
        //            //    newNode.Name = "DeptPatient";
        //            //    newNode.Tag = "DeptPatient";
        //            //    newNode.ImageIndex = this.BranchImageIndex;
        //            //    newNode.SelectedImageIndex = this.BranchSelectedImageIndex;
        //            //    this.Nodes[Branch].Nodes.Add(area);
        //            //}

        //        }
        //        else if (myPatients[i].GetType().ToString() == "FS.FrameWork.Models.NeuObject")
        //        {
        //            obj = (FS.FrameWork.Models.NeuObject)myPatients[i];
        //            this.AddTreeNode(Branch, obj, obj.ID, obj);
        //        }
        //        else
        //        {//Ϊ��
        //            //�ָ��ַ��� text|tag ��ʶ���
        //            string all = myPatients[i].ToString();
        //            string[] s = all.Split('|');

        //            newNode.Text = s[0];

        //            try
        //            {
        //                newNode.Name = s[1];
        //                newNode.Tag = s[1];
        //            }
        //            catch { newNode.Tag = ""; }
        //            try
        //            {
        //                newNode.ImageIndex = this.BranchImageIndex;
        //                newNode.SelectedImageIndex = this.BranchSelectedImageIndex;
        //            }
        //            catch { }
        //            Branch = this.Nodes.Add(newNode);
        //            benchDic.Add(Branch,new System.Collections.Generic.Dictionary<string,TreeNode>());
        //        }
        //    }
        //    if (this.bIsShowCount)
        //    {
        //        foreach (System.Windows.Forms.TreeNode node in this.Nodes)
        //        {

        //            if (node.Tag == null || node.Tag.GetType().ToString() == "System.String")
        //            {//���
        //                int count = 0;
        //                foreach (TreeNode  no in node.Nodes)
        //                {
        //                    foreach (TreeNode no2 in no.Nodes)
        //                    {
        //                        count++;
        //                    }
                            
        //                }
        //                //count = node.GetNodeCount(false);
        //                node.Text = node.Text + "(" + count.ToString() + ")";
        //            }
        //        }
        //    }
        //    this.ExpandAll();
        //    try//wolf added ensure node visible 
        //    {
        //        if (this.SelectedNode == null)
        //        {
        //            try
        //            {
        //                this.SelectedNode = this.Nodes[0];
        //            }
        //            catch { }
        //        }
        //        this.SelectedNode.EnsureVisible();
        //    }
        //    catch { }

        //}


        /// <summary>
        /// ��ʾ�����б�
        /// </summary>
        /// <param name="dicPatientList">������,��������,�����б���</param>
        public void SetPatient(System.Collections.Generic.Dictionary<string, object> dicPatientList)
        {
            if (dicPatientList == null)
            {
                return;
            }

            this.Nodes.Clear();

            #region ��ʾ�б�

            int count = 0;

            foreach (string key in dicPatientList.Keys)
            {
                TreeNode newNode=new TreeNode();
                //���һ����б�
                if (dicPatientList[key].GetType() == typeof(ArrayList))
                {
                    #region ���ؿ��ҽڵ�

                    string[] s = key.Split('|');

                    newNode.Text = s[0];

                    try
                    {
                        newNode.Name = s[1];
                        newNode.Tag = s[1];
                    }
                    catch
                    {
                        newNode.Tag = "";
                    }
                    newNode.ImageIndex = this.BranchImageIndex;
                    newNode.SelectedImageIndex = this.BranchSelectedImageIndex;
                    this.Nodes.Add(newNode);
                    #endregion

                    #region ���ػ��߽ڵ�

                    foreach (FS.HISFC.Models.RADT.PatientInfo pInfo in dicPatientList[key] as ArrayList)
                    {
                        FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                        TreeNode pNode = new TreeNode();

                        obj.ID = pInfo.PID.PatientNO;
                        obj.Name = pInfo.Name;
                        try
                        {
                            obj.Memo = pInfo.PVisit.PatientLocation.Bed.ID;
                            try
                            {	//���
                                if (pInfo.PVisit.PatientLocation.Bed.Status.ID.ToString() == "R")
                                {
                                    obj.Name = obj.Name + "����١�";
                                }
                            }
                            catch { }
                        }
                        catch
                        {
                            //�޲�����Ϣ
                        }

                        obj.User01 = pInfo.PVisit.PatientLocation.Dept.Name;
                        obj.User02 = pInfo.PVisit.InState.Name;
                        obj.User03 = pInfo.Sex.ID.ToString();
                        //��Ժ������24Сʱ,��ʾ(��)
                        if (this.bIsShowNewPatient)
                        {
                            if (dtToday < pInfo.PVisit.InTime.Date.AddDays(1)) obj.Name = obj.Name + "(��)";
                        }

                        this.CreateNodeInfo(obj, pInfo, ref pNode);
                        newNode.Nodes.Add(pNode);
                    }

                    count = newNode.GetNodeCount(false);
                    newNode.Text = newNode.Text + "(" + count.ToString() + ")";

                    #endregion
                }
                //�������б�
                else if (dicPatientList[key].GetType() == typeof(System.Collections.Generic.Dictionary<string, ArrayList>))
                {
                    #region ���ؿ��ҽڵ�

                    string[] s = key.Split('|');

                    newNode.Text = s[0];

                    try
                    {
                        newNode.Name = s[1];
                        newNode.Tag = s[1];
                    }
                    catch
                    {
                        newNode.Tag = "";
                    }
                    newNode.ImageIndex = this.BranchImageIndex;
                    newNode.SelectedImageIndex = this.BranchSelectedImageIndex;
                    this.Nodes.Add(newNode);
                    #endregion

                    System.Collections.Generic.Dictionary<string, ArrayList> dicTendPaient = dicPatientList[key] as System.Collections.Generic.Dictionary<string, ArrayList>;

                    int deptCount = 0;

                    foreach (string tendKey in dicTendPaient.Keys)
                    {
                        #region ���ػ�����ڵ�
                        TreeNode tendNode = new TreeNode();

                        if (string.IsNullOrEmpty(tendKey))
                        {
                            tendNode.Text = "δ����";
                            //tendNode.Tag = "δ����";
                        }
                        else
                        {
                            string[] s1 = tendKey.Split('|');

                            tendNode.Text = s1[0];

                            try
                            {
                                tendNode.Name = s1[1];
                                tendNode.Tag = s1[1];
                            }
                            catch
                            {
                                tendNode.Tag = "";
                            }
                        }
                        tendNode.ImageIndex = this.BranchImageIndex;
                        tendNode.SelectedImageIndex = this.BranchSelectedImageIndex;
                        newNode.Nodes.Add(tendNode);
                        #endregion

                        #region ���ػ��߽ڵ�

                        foreach (FS.HISFC.Models.RADT.PatientInfo pInfo in dicTendPaient[tendKey] as ArrayList)
                        {
                            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                            TreeNode pNode = new TreeNode();

                            obj.ID = pInfo.PID.PatientNO;
                            obj.Name = pInfo.Name;
                            try
                            {
                                obj.Memo = pInfo.PVisit.PatientLocation.Bed.ID;
                                try
                                {	//���
                                    if (pInfo.PVisit.PatientLocation.Bed.Status.ID.ToString() == "R")
                                    {
                                        obj.Name = obj.Name + "����١�";
                                    }
                                }
                                catch { }
                            }
                            catch
                            {
                                //�޲�����Ϣ
                            }

                            obj.User01 = pInfo.PVisit.PatientLocation.Dept.Name;
                            obj.User02 = pInfo.PVisit.InState.Name;
                            obj.User03 = pInfo.Sex.ID.ToString();
                            //��Ժ������24Сʱ,��ʾ(��)
                            if (this.bIsShowNewPatient)
                            {
                                if (dtToday < pInfo.PVisit.InTime.Date.AddDays(1)) obj.Name = obj.Name + "(��)";
                            }

                            this.CreateNodeInfo(obj, pInfo, ref pNode);
                            tendNode.Nodes.Add(pNode);
                        }

                        count = tendNode.GetNodeCount(false);
                        tendNode.Text = tendNode.Text + "(" + count.ToString() + ")";
                        deptCount += count;

                        #endregion
                    }

                    newNode.Text = newNode.Text + "(" + deptCount.ToString() + ")";
                }
            }

            #endregion

            this.ExpandAll();
            try//wolf added ensure node visible 
            {
                if (this.SelectedNode == null)
                {
                    try
                    {
                        this.SelectedNode = this.Nodes[0];
                    }
                    catch { }
                }
                this.SelectedNode.EnsureVisible();
            }
            catch { }
        }

        /// <summary>
        /// ���ӵ������б���ʾ
        /// </summary>
        public void SetPatient(ArrayList alPatients)
        {
            if (alPatients == null)
            {
                return;
            }
            this.myPatients = alPatients;
            this.RefreshList();        
        }

        /// <summary>
        /// ��ʾѡ������
        /// </summary>
        public enuChecked Checked
        {
            get
            {
                return this.myChecked;
            }
            set
            {
                this.myChecked = value;
                if (this.myChecked == enuChecked.MultiSelect)
                {
                    this.CheckBoxes = true;
                }
                else
                {
                    this.CheckBoxes = false;
                }
            }
        }

        /// <summary>
        /// ��ʾ������Ϣλ��
        /// </summary>
        public enuShowDirection Direction
        {
            get
            {
                return this.myDirection;
            }
            set
            {
                this.myDirection = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ�ڵ�����
        /// </summary>
        public bool IsShowCount
        {
            get
            {
                return this.bIsShowCount;
            }
            set
            {
                this.bIsShowCount = value;
            }
        }
        /// <summary>
        /// �Ƿ���ʾtooltipסԺ��
        /// </summary>
        public bool IsShowPatientNo
        {
            get
            {
                return this.bIsShowPatientNo;
            }
            set
            {
                this.bIsShowPatientNo = value;
            }
        }
        // by zlw 2006-5-1
        /// <summary>
        /// �Ƿ񵯳��Ҽ��˵�����ʾ��������,Ĭ��ֵΪ true ��ʾ 
        /// </summary>
        private bool bIsShowContextMenu = true;

        /// <summary>
        /// �Ƿ񵯳��Ҽ��˵�����ʾ��������,Ĭ��ֵΪ true ��ʾ 
        /// </summary>
        public bool IsShowContextMenu
        {
            get
            {
                return this.bIsShowContextMenu;
            }
            set
            {
                this.bIsShowContextMenu = value;
            }
        }

        #region �޸�δ��������

        /*

        /// <summary>
        /// ˢ���б�
        /// </summary>
        protected virtual void RefreshList(ArrayList alPatients)
        {
            this.Nodes.Clear();
            int Branch = 0;
            if (alPatients.Count == 0)
            {
                this.AddRootNode();
            }

            //��ʾ���ȼ�
            //1�����ڵ�
            //2�����ҽڵ�
            //3��������ڵ�
            //4�����߽ڵ�

            for (int i = 0; i < alPatients.Count; i++)
            {
                System.Windows.Forms.TreeNode newNode = new System.Windows.Forms.TreeNode();
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                //����ΪҶ
                if (alPatients[i].GetType().ToString() == "FS.HISFC.Models.RADT.PatientInfo")
                {
                    try
                    {
                        FS.HISFC.Models.RADT.PatientInfo PatientInfo = (FS.HISFC.Models.RADT.PatientInfo)alPatients[i];
                        obj.ID = PatientInfo.PID.PatientNO;
                        obj.Name = PatientInfo.Name;
                        try
                        {
                            obj.Memo = PatientInfo.PVisit.PatientLocation.Bed.ID;
                            try
                            {	//���
                                if (PatientInfo.PVisit.PatientLocation.Bed.Status.ID.ToString() == "R")
                                {
                                    obj.Name = obj.Name + "����١�";
                                }
                            }
                            catch { }
                        }
                        catch
                        {
                            //�޲�����Ϣ
                        }

                        obj.User01 = PatientInfo.PVisit.PatientLocation.Dept.Name;
                        obj.User02 = PatientInfo.PVisit.InState.Name;
                        obj.User03 = PatientInfo.Sex.ID.ToString();
                        //��Ժ������24Сʱ,��ʾ(��)
                        if (this.bIsShowNewPatient)
                        {
                            if (dtToday < PatientInfo.PVisit.InTime.Date.AddDays(1)) obj.Name = obj.Name + "(��)";
                        }
                        this.AddTreeNode(Branch, obj,PatientInfo.ID, PatientInfo);
                    }
                    catch { }
                }
                else if (alPatients[i].GetType().ToString() == "FS.HISFC.Models.RADT.Patient")
                {
                    FS.HISFC.Models.RADT.Patient PatientInfo = (FS.HISFC.Models.RADT.Patient)alPatients[i];
                    obj.ID = PatientInfo.PID.PatientNO;
                    obj.Name = PatientInfo.Name;
                    obj.Memo = "";
                    obj.User01 = "";
                    obj.User02 = "";
                    obj.User03 = PatientInfo.Sex.ID.ToString();
                    this.AddTreeNode(Branch, obj, PatientInfo.ID, PatientInfo);

                }
                else if (alPatients[i].GetType().ToString() == "FS.FrameWork.Models.NeuObject")
                {
                    obj = (FS.FrameWork.Models.NeuObject)alPatients[i];
                    this.AddTreeNode(Branch, obj, obj.ID,obj);
                }
                else
                {
                    //Ϊ��
                    //�ָ��ַ��� text|tag ��ʶ���
                    string all = alPatients[i].ToString();
                    string[] s = all.Split('|');

                    newNode.Text = s[0];

                    try
                    {
                        newNode.Name = s[1];
                        newNode.Tag = s[1];
                    }
                    catch
                    {
                        newNode.Tag = "";
                    }
                    try
                    {
                        newNode.ImageIndex = this.BranchImageIndex;
                        newNode.SelectedImageIndex = this.BranchSelectedImageIndex;
                    }
                    catch { }
                    Branch = this.Nodes.Add(newNode);
                }
            }

            #region ��ʾ����
            if (this.bIsShowCount)
            {
                //���ܴ���2���3�㣨�����飩�����Եݹ���ʾ����
                foreach (System.Windows.Forms.TreeNode node in this.Nodes)
                {
                    if (node.Tag == null || node.Tag.GetType().ToString() == "System.String")
                    {
                        //���
                        int count = 0;
                        count = node.GetNodeCount(false);
                        node.Text = node.Text + "(" + count.ToString() + ")";
                    }
                }
            }
            #endregion

            this.ExpandAll();
            try//wolf added ensure node visible 
            {
                if (this.SelectedNode == null)
                {
                    try
                    {
                        this.SelectedNode = this.Nodes[0];
                    }
                    catch { }
                }
                this.SelectedNode.EnsureVisible();
            }
            catch { }
        }

         * */

        #endregion

        /// <summary>
        /// ɾ���ڵ�
        /// </summary>
        /// <param name="branch">�����ڵ�����</param>
        /// <param name="nodeIndex">Ҫɾ���ڵ�����</param>
        public void DeleteNode(int branch, int nodeIndex)
        {
            //�Ƴ��ڵ�
            this.Nodes[branch].Nodes[nodeIndex].Remove();
        }


        /// <summary>
        /// ���ݴ������,�޸�ָ���Ľڵ���Ϣ
        /// </summary>
        /// <param name="node">���޸ĵĽڵ�</param>
        /// <param name="nodeTextInfo">�ڵ���Ϣ</param>
        /// <param name="nodeTag">�ڵ��tag����</param>
        public void ModifiyNode(System.Windows.Forms.TreeNode node, FS.FrameWork.Models.NeuObject nodeTextInfo, object nodeTag)
        {
            try
            {
                //���ɽڵ���Ϣ
                this.CreateNodeInfo(nodeTextInfo, nodeTag, ref node);
            }
            catch { }
        }


        /// <summary>
        /// ���ݴ������,�޸�ָ���Ľڵ���Ϣ
        /// </summary>
        /// <param name="node">���޸ĵĽڵ�</param>
        /// <param name="patientInfo">������Ϣ</param>
        public void ModifiyNode(System.Windows.Forms.TreeNode node, FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            try
            {
                FS.FrameWork.Models.NeuObject nodeTextInfo = new FS.FrameWork.Models.NeuObject();
                nodeTextInfo.ID = patientInfo.PID.PatientNO;
                nodeTextInfo.Name = patientInfo.Name;
                try
                {
                    nodeTextInfo.Memo = patientInfo.PVisit.PatientLocation.Bed.ID;
                }
                catch
                {//�޲�����Ϣ
                }

                nodeTextInfo.User01 = patientInfo.PVisit.PatientLocation.Dept.Name;
                nodeTextInfo.User02 = patientInfo.PVisit.InState.Name;
                nodeTextInfo.User03 = patientInfo.Sex.ID.ToString();
                if (this.bIsShowNewPatient)
                {
                    if (dtToday.Date == patientInfo.PVisit.InTime.Date)
                        nodeTextInfo.Name = nodeTextInfo.Name + "(��)";
                }

                //����ڵ������,ָ��Ҫ�޸ĵĽڵ�
                this.ModifiyNode(node, nodeTextInfo, patientInfo);
            }
            catch { }
        }


        /// <summary>
        /// ���ݴ������Ϣ,����һ���½ڵ�
        /// </summary>
        /// <param name="branch">һ���ڵ�����</param>
        /// <param name="nodeTextInfo">�ڵ���Ϣ</param>
        /// <param name="nodeTag">�ڵ�Tag����</param>
        public void AddTreeNode(int branch, FS.FrameWork.Models.NeuObject nodeTextInfo,string name, object nodeTag)
        {
            System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode();
            //����Ҫ��ӵĽڵ�
            this.CreateNodeInfo(nodeTextInfo, nodeTag, ref node);
            node.Name = name;
            //ָ����ǰѡ�еĽڵ�
            try
            {
                //this.SelectedNode=this.Nodes[branch];
                //�ڸ����ڵ��������½ڵ�
                this.Nodes[branch].Nodes.Add(node);
            }
            catch
            {
                this.Nodes.Add(new System.Windows.Forms.TreeNode("����"));
                //this.SelectedNode=this.Nodes[0];
                //��ѡ�еĽڵ��������½ڵ�
                this.Nodes[0].Nodes.Add(node);
            }

            //��ѡ�еĽڵ��������½ڵ�
            //this.SelectedNode.Nodes.Add(node);
        }

        /// <summary>
        /// ���ݴ������,�����½ڵ�
        /// </summary>
        /// <param name="branch">һ���ڵ�����</param>
        /// <param name="patientInfo">������Ϣ</param>
        public void AddTreeNode(int branch, FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            try
            {
                
                //�ڵ���Ϣ
                FS.FrameWork.Models.NeuObject nodeTextInfo = new FS.FrameWork.Models.NeuObject();
                nodeTextInfo.ID = patientInfo.PID.PatientNO;				//סԺ��
                nodeTextInfo.Name = patientInfo.Name;								//��������
                nodeTextInfo.Memo = patientInfo.PVisit.PatientLocation.Bed.ID;		//����
                nodeTextInfo.User01 = patientInfo.PVisit.PatientLocation.Dept.Name;	//��������
                nodeTextInfo.User02 = patientInfo.PVisit.InState.Name;				//��Ժ״̬
                nodeTextInfo.User03 = patientInfo.Sex.ID.ToString();		//�Ա�
                //���ݻ��ߵ���Ժ����,�ж��Ƿ���ʾ"(��)"
                if (this.bIsShowNewPatient)
                {
                    if (dtToday.Date == patientInfo.PVisit.InTime.Date)
                        nodeTextInfo.Name = nodeTextInfo.Name + "(��)";
                }

                //����ڵ������,ָ��Ҫ�޸ĵĽڵ�
                this.AddTreeNode(branch, nodeTextInfo,patientInfo.ID, patientInfo);
            }
            catch { }
        }


        /// <summary>
        /// ���ݴ������,�����ڵ���Ϣ
        /// </summary>
        /// <param name="neuObj">�ڵ�Text��Ϣ:obj.id ,name,memo=bed,user01=dept,user02=status user03=sex </param>
        /// <param name="obj">�ڵ��Tag����</param>
        /// <param name="node">���ز���:�ڵ�</param>
        private void CreateNodeInfo(FS.FrameWork.Models.NeuObject neuObj, object obj, ref System.Windows.Forms.TreeNode node)
        {
            //�������ڵ�Ϊ��,���½�һ���ڵ�
            if (node == null)
                node = new System.Windows.Forms.TreeNode();

            #region ���ɽڵ��Text
            string strText = neuObj.Name; //��������
            string strMemo = "";
            switch (this.myShowType.GetHashCode())
            {
                case 1:
                    //סԺ��
                    strMemo = "��" + neuObj.ID + "��";
                    break;
                case 3:
                    //����
                    if (neuObj.User01 != "" || neuObj.User01 != null) strMemo = "��" + neuObj.User01 + "��";
                    break;
                case 5:
                    //����
                    if (neuObj.Memo != "" || neuObj.Memo != null)
                    {
                        strMemo = neuObj.Memo;

                        if (strMemo.Length > 4)
                        {
                            strMemo = strMemo.Substring(4);
                        }
                        #region

                        #endregion
                        if (!string.IsNullOrEmpty(strMemo))
                        {
                            strMemo = "��" + strMemo + "��";
                        }
                    }
                    break;
                case 7:
                    //״̬
                    strMemo = "��" + neuObj.User02 + "��";
                    break;
                case 4:
                    //����+סԺ��
                    strMemo = "��" + neuObj.User01 + "��" + "��" + neuObj.ID + "��";
                    break;
                case 6:
                    //����+סԺ��
                    if (neuObj.Memo != "" || neuObj.Memo != null)
                        strMemo = "��" + neuObj.Memo.Substring(4) + "��" + "��" + neuObj.ID + "��";
                    else
                        strMemo = "��" + neuObj.ID + "��";
                    break;
                case 8:
                    //סԺ��+״̬
                    strMemo = "��" + neuObj.ID + "��" + "��" + neuObj.User02 + "��";
                    break;
                case 10:
                    //����+״̬
                    strMemo = "��" + neuObj.User01 + "��" + "��" + neuObj.User02 + "��";
                    break;
                case 12:
                    //����+״̬
                    if (neuObj.Memo != "" || neuObj.Memo != null)
                        strMemo = "��" + neuObj.Memo.Substring(4) + "��" + "��" + neuObj.User02 + "��";
                    else
                        strMemo = "��" + neuObj.User02 + "��";
                    break;
                default:
                    strMemo = "";
                    break;
            }

            //������ʾλ��,ȷ�����յ�����
            if (this.myDirection == enuShowDirection.Behind)
            {
                strText = strText + strMemo;
            }
            else
            {
                strText = strMemo + strText;
            }
            node.Text = strText;
            #endregion

            //�����ڵ��ImageIndex
            switch (neuObj.User03)
            {
                case "F":
                    //��
                    if (((FS.FrameWork.Models.NeuObject)obj).ID.IndexOf("B") > 0)
                        node.ImageIndex = this.GirlImageIndex;	//Ӥ��Ů
                    else
                        node.ImageIndex = this.FemaleImageIndex;	//����Ů
                    break;
                case "M":
                    if (((FS.FrameWork.Models.NeuObject)obj).ID.IndexOf("B") > 0)
                        node.ImageIndex = this.BabyImageIndex;	//Ӥ����
                    else
                        node.ImageIndex = this.MaleImageIndex;	//������
                    break;
                default:
                    node.ImageIndex = this.MaleImageIndex;
                    break;
            }

            if (obj is FS.HISFC.Models.RADT.PatientInfo)
            {
                if (((FS.HISFC.Models.RADT.PatientInfo)obj).IsPtjtState)
                {
                    node.NodeFont = new Font("���� ", 9, FontStyle.Bold);

                }

            }

            //�����ڵ��SelectedImageIndex
            node.SelectedImageIndex = node.ImageIndex + 1;

            //�����ڵ��Tag����
            node.Tag = obj;
        }


        /// <summary>
        /// ���ݻ�����Ϣ��һ���ڵ�,�������ӽڵ��л������ڵĽڵ�Index
        /// </summary>
        /// <param name="branch"></param>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public System.Windows.Forms.TreeNode FindNode(int branch, FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            FS.HISFC.Models.RADT.PatientInfo findPatient = null;
            foreach (System.Windows.Forms.TreeNode node in this.Nodes[branch].Nodes)
            {
                //ȡ�ڵ��ϵĻ�����Ϣ
                findPatient = node.Tag as FS.HISFC.Models.RADT.PatientInfo;
                //�������ת��Ϊ������Ϣ,�����������һ���ڵ�
                if (findPatient == null) continue;
                //����ҵ�,�򷵻ش˽ڵ�
                if (findPatient.ID == patientInfo.ID) return node;
            }

            //���û���ҵ�,�򷵻�null
            return null;
        }

        /// <summary>
        /// �������ﻼ����Ϣ��һ���ڵ�,�������ӽڵ��л������ڵĽڵ�Index
        /// </summary>
        /// <param name="branch"></param>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public System.Windows.Forms.TreeNode FindNode(int branch, FS.HISFC.Models.Registration.Register patientInfo)
        {
            FS.HISFC.Models.Registration.Register findPatient = null;
            foreach (System.Windows.Forms.TreeNode node in this.Nodes[branch].Nodes)
            {
                //ȡ�ڵ��ϵĻ�����Ϣ
                findPatient = node.Tag as FS.HISFC.Models.Registration.Register;
                //�������ת��Ϊ������Ϣ,�����������һ���ڵ�
                if (findPatient == null) continue;
                //����ҵ�,�򷵻ش˽ڵ�
                if (findPatient.ID == patientInfo.ID) return node;
            }

            //���û���ҵ�,�򷵻�null
            return null;
        }

        /// <summary>
        /// ��Ӹ��ڵ�
        /// </summary>
        protected virtual void AddRootNode()
        {
            this.Nodes.Add(new System.Windows.Forms.TreeNode("����"));
        }


        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
            this.ImageList = this.imageList1;
            this.HideSelection = false;

            try
            {
                if (this.IsShowContextMenu == true)//��ʾ����
                {
                    FS.FrameWork.WinForms.Controls.NeuContextMenuStrip contextMenu1 = new FS.FrameWork.WinForms.Controls.NeuContextMenuStrip();
                    contextMenu1.Items.Clear();
            
                    // �����Ҽ��˵�  by zlw 2006-5-1
                    System.Windows.Forms.ContextMenu cmPatientPro = new System.Windows.Forms.ContextMenu();
                    System.Windows.Forms.MenuItem miPatientPro = new System.Windows.Forms.MenuItem();
                    System.Windows.Forms.MenuItem miPatientPro1 = new System.Windows.Forms.MenuItem();
                    System.Windows.Forms.MenuItem miPatientPro2 = new System.Windows.Forms.MenuItem();
                    System.Windows.Forms.MenuItem miPatientPro3 = new System.Windows.Forms.MenuItem();

                    cmPatientPro.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { miPatientPro, miPatientPro1, miPatientPro2, miPatientPro3 });// {1292AA34-6E62-4f01-9BE7-EAE667D34E5D}

                    miPatientPro.Text = "�鿴������Ϣ";

                    miPatientPro1.Text = "Ӥ�����(��Ӥ������)";// {1292AA34-6E62-4f01-9BE7-EAE667D34E5D}

                    miPatientPro2.Text = "Ӥ�����(��ϸ��Ϣ)";

                    miPatientPro3.Text = "ĸ�����";// {AB709343-3691-4776-B4DB-2F5BCD3D40F3}

                    this.ContextMenu = cmPatientPro;

                    miPatientPro1.Click += new System.EventHandler(this.miPatientPro1_Click);// {1292AA34-6E62-4f01-9BE7-EAE667D34E5D}


                    miPatientPro2.Click += new System.EventHandler(this.miPatientPro2_Click);// {D5390E6B-48F5-45d3-B956-3BC952BDE1EA}

                    miPatientPro.Click += new System.EventHandler(this.miPatientPro_Click);

                    miPatientPro3.Click += new System.EventHandler(this.miPatientPro3_Click);// {AB709343-3691-4776-B4DB-2F5BCD3D40F3}


                }

                FS.HISFC.BizLogic.Manager.Spell dataBase = new FS.HISFC.BizLogic.Manager.Spell();
                this.dtToday = dataBase.GetDateTimeFromSysDateTime();
            }
            catch
            {
                this.dtToday = DateTime.Today;
            }
        }
        #endregion

        #region �¼�
        private void miPatientPro_Click(object sender, System.EventArgs e)
        {
            FS.HISFC.Models.RADT.PatientInfo findPatient = null;
            System.Windows.Forms.TreeNode node = this.SelectedNode;
            #region {93F17D80-F559-45f6-B380-23A8CC8A936D}
            if (node == null) return;
            #endregion
            findPatient = node.Tag as FS.HISFC.Models.RADT.PatientInfo;
            if (findPatient == null)
            {
                return;
            }
            else
            {
                ucPatientProperty ucPatientpro = new ucPatientProperty();
                ucPatientpro.Patient = findPatient;
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucPatientpro);
            }
        }
        private void miPatientPro1_Click(object sender, System.EventArgs e)
        {
            FS.HISFC.Models.RADT.PatientInfo findPatient = null;
            System.Windows.Forms.TreeNode node = this.SelectedNode;
            #region {93F17D80-F559-45f6-B380-23A8CC8A936D}
            if (node == null) return;
            #endregion
            findPatient = node.Tag as FS.HISFC.Models.RADT.PatientInfo;
            if (findPatient == null)
            {
                return;
            }
            else
            {
                // {A54386E1-DEDB-4fe2-A6D0-292BEBF1FBEE}
                FS.HISFC.BizProcess.Interface.Order.Inpatient.IBraceletPrint o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Common.Controls.tvPatientList), typeof(FS.HISFC.BizProcess.Interface.Order.Inpatient.IBraceletPrint),1) as FS.HISFC.BizProcess.Interface.Order.Inpatient.IBraceletPrint;
                if (o == null)
                {
                    return;
                }
                else
                {
                    findPatient.User01 = "����";
                    o.myPatientInfo = findPatient;
                    o.Print();
                }
            }
        }
        // {D5390E6B-48F5-45d3-B956-3BC952BDE1EA}
        private void miPatientPro2_Click(object sender, System.EventArgs e)
        {
            FS.HISFC.Models.RADT.PatientInfo findPatient = null;
            System.Windows.Forms.TreeNode node = this.SelectedNode;
            #region {93F17D80-F559-45f6-B380-23A8CC8A936D}
            if (node == null) return;
            #endregion
            findPatient = node.Tag as FS.HISFC.Models.RADT.PatientInfo;
            if (findPatient == null)
            {
                return;
            }
            else
            {
                // {A54386E1-DEDB-4fe2-A6D0-292BEBF1FBEE}
                FS.HISFC.BizProcess.Interface.Order.Inpatient.IBraceletPrint o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Common.Controls.tvPatientList), typeof(FS.HISFC.BizProcess.Interface.Order.Inpatient.IBraceletPrint), 1) as FS.HISFC.BizProcess.Interface.Order.Inpatient.IBraceletPrint;
                if (o == null)
                {
                    return;
                }
                else
                {
                    findPatient.User01 = "��ϸ��Ϣ";
                    o.myPatientInfo = findPatient;
                    o.Print();
                }
            }
        }
        private void miPatientPro3_Click(object sender, System.EventArgs e)// {AB709343-3691-4776-B4DB-2F5BCD3D40F3}
        {
            FS.HISFC.Models.RADT.PatientInfo findPatient = null;
            System.Windows.Forms.TreeNode node = this.SelectedNode;
            #region {93F17D80-F559-45f6-B380-23A8CC8A936D}
            if (node == null) return;
            #endregion
            findPatient = node.Tag as FS.HISFC.Models.RADT.PatientInfo;
            if (findPatient == null)
            {
                return;
            }
            else
            {// {A54386E1-DEDB-4fe2-A6D0-292BEBF1FBEE}
                FS.HISFC.BizProcess.Interface.Order.Inpatient.IBraceletPrint o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Common.Controls.tvPatientList), typeof(FS.HISFC.BizProcess.Interface.Order.Inpatient.IBraceletPrint), 2) as FS.HISFC.BizProcess.Interface.Order.Inpatient.IBraceletPrint;
                if (o == null)
                {
                    return;
                }
                else
                {
                    o.myPatientInfo = findPatient;
                    o.Print();
                }
            }
        }
        private void tvPatientList_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (this.CheckBoxes && this.bControlChecked == false)
            {
                e.Node.Checked = !e.Node.Checked;
            }
        }
        private void tvPatientList_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (this.CheckBoxes && this.bControlChecked == false)
            {
                foreach (System.Windows.Forms.TreeNode node in e.Node.Nodes)
                {
                    node.Checked = e.Node.Checked;
                }
            }
        }
        System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip();
        private void tvPatientList_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (this.bIsShowPatientNo)
            {
                System.Windows.Forms.TreeNode node = null;
                FS.HISFC.Models.RADT.PatientInfo info = null;
                System.Drawing.Point p = new System.Drawing.Point(e.X, e.Y);
                node = this.GetNodeAt(p);
                if (node == null) return;
                info = node.Tag as FS.HISFC.Models.RADT.PatientInfo;
                if (info == null) return;
                if(this.toolTip1.GetToolTip(this)!= info.PID.ID)
                    this.toolTip1.SetToolTip(this, info.PID.ID);
            }
        }

        private void tvPatientList_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (this.bIsShowContextMenu == false && this.ContextMenu!=null)
                {
                    this.ContextMenu = null;
                    return;
                }

                System.Windows.Forms.TreeNode node = this.GetNodeAt(e.X, e.Y);
                this.SelectedNode = node;
            }

        }

        public override void Refresh()
        {
            this.RefreshList();
        }
        #endregion      
    }
}
