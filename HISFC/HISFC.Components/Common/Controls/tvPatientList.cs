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
    /// [功能描述: 患者列表树]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2004-10-12]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public class tvPatientList : FS.FrameWork.WinForms.Controls.NeuTreeView
    {

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public tvPatientList()
        {
            // Windows.Forms 类撰写设计器支持所必需的
            InitializeComponent();
            //初始化
            this.Init();
        }

        private System.Windows.Forms.ImageList imageList1;
        private System.ComponentModel.IContainer components;

        #region 组件设计器生成的代码


        ///// <summary>
        ///// 有参构造函数
        ///// </summary>
        ///// <param name="container">接口</param>
        //public tvPatientList(System.ComponentModel.IContainer container)
        //{
        //    // Windows.Forms 类撰写设计器支持所必需的
        //    container.Add(this);
        //    InitializeComponent();
        //    this.Init();//初始化
        //    //
        //    // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
        //    //
        //}

        /// <summary> 
        /// 清理所有正在使用的资源。
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
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
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
        
        #region 枚举
        /// <summary>
        /// 显示其他信息-住院号，科室，病床，在院状态
        /// </summary>
        public enum enuShowType
        {
            /// <summary>
            /// 不显示
            /// </summary>
            None = 0,

            /// <summary>
            /// 住院号
            /// </summary>
            InpatientNo = 1,

            /// <summary>
            /// 科室
            /// </summary>
            Dept = 3,

            /// <summary>
            /// 床号
            /// </summary>
            Bed = 5,

            /// <summary>
            /// 状态
            /// </summary>
            Status = 7
        }

        /// <summary>
        /// 显示信息方向，前面，后面(姓名放在相反的方向)
        /// </summary>
        public enum enuShowDirection
        {
            /// <summary>
            /// 放在姓名前
            /// </summary>
            Ahead,

            /// <summary>
            /// 放在姓名后
            /// </summary>
            Behind
        }

        /// <summary>
        /// 选择类型
        /// </summary>
        public enum enuChecked
        {
            /// <summary>
            /// 没有勾选框
            /// </summary>
            None,

            /// <summary>
            /// 单选框
            /// </summary>
            Radio,

            /// <summary>
            /// 复选框
            /// </summary>
            MultiSelect
        }
        #endregion

        #region 变量

        protected ArrayList myPatients = new ArrayList();

        /// <summary>
        /// 默认显示床号
        /// </summary>
        private enuShowType myShowType = enuShowType.Bed;

        /// <summary>
        /// 默认不显示CheckBox
        /// </summary>
        private enuChecked myChecked = enuChecked.None;

        /// <summary>
        /// 默认其他信息放在前面,姓名放在后面
        /// </summary>
        private enuShowDirection myDirection = enuShowDirection.Ahead;

        /// <summary>
        /// 默认如果是当天入院的患者,显示【新】
        /// </summary>
        private bool bIsShowNewPatient = true;

        private bool bControlChecked = false;

        /// <summary>
        /// 当日
        /// </summary>
        protected DateTime dtToday;

        /// <summary>
        /// 是否显示tooltip住院号
        /// </summary>
        protected bool bIsShowPatientNo = true;

        /// <summary>
        /// 是否显示节点人数
        /// </summary>
        protected bool bIsShowCount = true;

        /// <summary>
        /// 根节点显示图片
        /// </summary>
        public int RootImageIndex = 0;

        /// <summary>
        /// 根节点选中图片
        /// </summary>
        public int RootSelectedImageIndex = 1;

        /// <summary>
        /// 科室节点显示图片
        /// </summary>
        public int BranchImageIndex = 2;

        /// <summary>
        /// 科室节点选中图片
        /// </summary>
        public int BranchSelectedImageIndex = 3;

        /// <summary>
        /// 男患者图片
        /// </summary>
        public int MaleImageIndex = 4;

        /// <summary>
        /// 男患者选中图片
        /// </summary>
        public int MaleSelectedImageIndex = 5;

        /// <summary>
        /// 女患者图片
        /// </summary>
        public int FemaleImageIndex = 6;

        /// <summary>
        /// 女患者节点选中图片
        /// </summary>
        public int FemaleSelectedImageIndex = 7;

        /// <summary>
        /// 男孩显示图片
        /// </summary>
        public int BabyImageIndex = 8;

        /// <summary>
        /// 女孩显示图片
        /// </summary>
        public int GirlImageIndex = 10;


        public int LeaveImageIndex = 12;
        public int BlankImageIndex = 13;//{839D3A8A-49FA-4d47-A022-6196EB1A5715}

        #endregion

        #region 函数
        /// <summary>
        /// 是否显示新的患者
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
        /// 显示类型
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
        ///// 刷新列表
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
                //类型为叶
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
                            {	//请假
                                if (PatientInfo.PVisit.PatientLocation.Bed.Status.ID.ToString() == "R")
                                {
                                    obj.Name = obj.Name + "【请假】";
                                }
                            }
                            catch { }
                        }
                        catch
                        {//无病床信息
                        }
                        obj.User01 = PatientInfo.PVisit.PatientLocation.Dept.Name;
                        obj.User02 = PatientInfo.PVisit.InState.Name;
                        obj.User03 = PatientInfo.Sex.ID.ToString();
                        //入院不超过24小时,显示(新)
                        if (this.bIsShowNewPatient)
                        {
                            if (dtToday < PatientInfo.PVisit.InTime.Date.AddDays(1)) obj.Name = obj.Name + "(新)";
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
                {//为干
                    //分割字符串 text|tag 标识结点
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
                    {//结点
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
        ///// 刷新列表// {019BAA9F-7AF8-4249-8815-E5ED2DEC940E}
        ///// </summary>
        //protected virtual void RefreshList()
        //{
        //    this.Nodes.Clear();
        //    int Branch = 0;
        //   // System.Collections.Generic.Dictionary<string, TreeNode> areaIndexDic = new System.Collections.Generic.Dictionary<string, TreeNode>();//护士站对应节点
        //    System.Collections.Generic.Dictionary<int, System.Collections.Generic.Dictionary<string, TreeNode>> benchDic = new System.Collections.Generic.Dictionary<int, System.Collections.Generic.Dictionary<string, TreeNode>>();
        //    ArrayList listArea = new ArrayList();
        //    ArrayList listAreaNodes = new ArrayList();
        //    if (myPatients.Count == 0) this.AddRootNode();
        //    for (int i = 0; i < myPatients.Count; i++)
        //    {
        //        System.Windows.Forms.TreeNode newNode = new System.Windows.Forms.TreeNode();
        //        FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
        //        //类型为叶
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
        //                    {	//请假
        //                        if (PatientInfo.PVisit.PatientLocation.Bed.Status.ID.ToString() == "R")
        //                        {
        //                            obj.Name = obj.Name + "【请假】";
        //                        }
        //                    }
        //                    catch { }
        //                }
        //                catch
        //                {//无病床信息
        //                }
        //                obj.User01 = PatientInfo.PVisit.PatientLocation.Dept.Name;
        //                obj.User02 = PatientInfo.PVisit.InState.Name;
        //                obj.User03 = PatientInfo.Sex.ID.ToString();
        //                //入院不超过24小时,显示(新)
        //                if (this.bIsShowNewPatient)
        //                {
        //                    if (dtToday < PatientInfo.PVisit.InTime.Date.AddDays(1)) obj.Name = obj.Name + "(新)";
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
        //                    //生产要添加的节点
        //                    this.CreateNodeInfo(obj, PatientInfo, ref node);
        //                    newNode.Nodes.Add(node);
        //                }
        //                else
        //                {
        //                    // this.AddTreeNode(Branch, obj, PatientInfo.ID, PatientInfo);
        //                    System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode();
        //                    //生产要添加的节点
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
        //        {//为干
        //            //分割字符串 text|tag 标识结点
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
        //            {//结点
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
        /// 显示患者列表
        /// </summary>
        /// <param name="dicPatientList">《科室,《护理组,患者列表》》</param>
        public void SetPatient(System.Collections.Generic.Dictionary<string, object> dicPatientList)
        {
            if (dicPatientList == null)
            {
                return;
            }

            this.Nodes.Clear();

            #region 显示列表

            int count = 0;

            foreach (string key in dicPatientList.Keys)
            {
                TreeNode newNode=new TreeNode();
                //科室患者列表
                if (dicPatientList[key].GetType() == typeof(ArrayList))
                {
                    #region 加载科室节点

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

                    #region 加载患者节点

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
                            {	//请假
                                if (pInfo.PVisit.PatientLocation.Bed.Status.ID.ToString() == "R")
                                {
                                    obj.Name = obj.Name + "【请假】";
                                }
                            }
                            catch { }
                        }
                        catch
                        {
                            //无病床信息
                        }

                        obj.User01 = pInfo.PVisit.PatientLocation.Dept.Name;
                        obj.User02 = pInfo.PVisit.InState.Name;
                        obj.User03 = pInfo.Sex.ID.ToString();
                        //入院不超过24小时,显示(新)
                        if (this.bIsShowNewPatient)
                        {
                            if (dtToday < pInfo.PVisit.InTime.Date.AddDays(1)) obj.Name = obj.Name + "(新)";
                        }

                        this.CreateNodeInfo(obj, pInfo, ref pNode);
                        newNode.Nodes.Add(pNode);
                    }

                    count = newNode.GetNodeCount(false);
                    newNode.Text = newNode.Text + "(" + count.ToString() + ")";

                    #endregion
                }
                //护理组列表
                else if (dicPatientList[key].GetType() == typeof(System.Collections.Generic.Dictionary<string, ArrayList>))
                {
                    #region 加载科室节点

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
                        #region 加载护理组节点
                        TreeNode tendNode = new TreeNode();

                        if (string.IsNullOrEmpty(tendKey))
                        {
                            tendNode.Text = "未分组";
                            //tendNode.Tag = "未分组";
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

                        #region 加载患者节点

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
                                {	//请假
                                    if (pInfo.PVisit.PatientLocation.Bed.Status.ID.ToString() == "R")
                                    {
                                        obj.Name = obj.Name + "【请假】";
                                    }
                                }
                                catch { }
                            }
                            catch
                            {
                                //无病床信息
                            }

                            obj.User01 = pInfo.PVisit.PatientLocation.Dept.Name;
                            obj.User02 = pInfo.PVisit.InState.Name;
                            obj.User03 = pInfo.Sex.ID.ToString();
                            //入院不超过24小时,显示(新)
                            if (this.bIsShowNewPatient)
                            {
                                if (dtToday < pInfo.PVisit.InTime.Date.AddDays(1)) obj.Name = obj.Name + "(新)";
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
        /// 增加到患者列表显示
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
        /// 显示选择类型
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
        /// 显示其他信息位置
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
        /// 是否显示节点人数
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
        /// 是否显示tooltip住院号
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
        /// 是否弹出右键菜单，显示患者属性,默认值为 true 显示 
        /// </summary>
        private bool bIsShowContextMenu = true;

        /// <summary>
        /// 是否弹出右键菜单，显示患者属性,默认值为 true 显示 
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

        #region 修改未定，屏蔽

        /*

        /// <summary>
        /// 刷新列表
        /// </summary>
        protected virtual void RefreshList(ArrayList alPatients)
        {
            this.Nodes.Clear();
            int Branch = 0;
            if (alPatients.Count == 0)
            {
                this.AddRootNode();
            }

            //显示优先级
            //1、父节点
            //2、科室节点
            //3、护理组节点
            //4、患者节点

            for (int i = 0; i < alPatients.Count; i++)
            {
                System.Windows.Forms.TreeNode newNode = new System.Windows.Forms.TreeNode();
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                //类型为叶
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
                            {	//请假
                                if (PatientInfo.PVisit.PatientLocation.Bed.Status.ID.ToString() == "R")
                                {
                                    obj.Name = obj.Name + "【请假】";
                                }
                            }
                            catch { }
                        }
                        catch
                        {
                            //无病床信息
                        }

                        obj.User01 = PatientInfo.PVisit.PatientLocation.Dept.Name;
                        obj.User02 = PatientInfo.PVisit.InState.Name;
                        obj.User03 = PatientInfo.Sex.ID.ToString();
                        //入院不超过24小时,显示(新)
                        if (this.bIsShowNewPatient)
                        {
                            if (dtToday < PatientInfo.PVisit.InTime.Date.AddDays(1)) obj.Name = obj.Name + "(新)";
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
                    //为干
                    //分割字符串 text|tag 标识结点
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

            #region 显示人数
            if (this.bIsShowCount)
            {
                //可能存在2层或3层（护理组），所以递归显示人数
                foreach (System.Windows.Forms.TreeNode node in this.Nodes)
                {
                    if (node.Tag == null || node.Tag.GetType().ToString() == "System.String")
                    {
                        //结点
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
        /// 删除节点
        /// </summary>
        /// <param name="branch">父级节点索引</param>
        /// <param name="nodeIndex">要删除节点索引</param>
        public void DeleteNode(int branch, int nodeIndex)
        {
            //移除节点
            this.Nodes[branch].Nodes[nodeIndex].Remove();
        }


        /// <summary>
        /// 根据传入参数,修改指定的节点信息
        /// </summary>
        /// <param name="node">待修改的节点</param>
        /// <param name="nodeTextInfo">节点信息</param>
        /// <param name="nodeTag">节点的tag属性</param>
        public void ModifiyNode(System.Windows.Forms.TreeNode node, FS.FrameWork.Models.NeuObject nodeTextInfo, object nodeTag)
        {
            try
            {
                //生成节点信息
                this.CreateNodeInfo(nodeTextInfo, nodeTag, ref node);
            }
            catch { }
        }


        /// <summary>
        /// 根据传入参数,修改指定的节点信息
        /// </summary>
        /// <param name="node">待修改的节点</param>
        /// <param name="patientInfo">患者信息</param>
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
                {//无病床信息
                }

                nodeTextInfo.User01 = patientInfo.PVisit.PatientLocation.Dept.Name;
                nodeTextInfo.User02 = patientInfo.PVisit.InState.Name;
                nodeTextInfo.User03 = patientInfo.Sex.ID.ToString();
                if (this.bIsShowNewPatient)
                {
                    if (dtToday.Date == patientInfo.PVisit.InTime.Date)
                        nodeTextInfo.Name = nodeTextInfo.Name + "(新)";
                }

                //定义节点的引用,指向要修改的节点
                this.ModifiyNode(node, nodeTextInfo, patientInfo);
            }
            catch { }
        }


        /// <summary>
        /// 根据传入的信息,增加一个新节点
        /// </summary>
        /// <param name="branch">一级节点索引</param>
        /// <param name="nodeTextInfo">节点信息</param>
        /// <param name="nodeTag">节点Tag属性</param>
        public void AddTreeNode(int branch, FS.FrameWork.Models.NeuObject nodeTextInfo,string name, object nodeTag)
        {
            System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode();
            //生产要添加的节点
            this.CreateNodeInfo(nodeTextInfo, nodeTag, ref node);
            node.Name = name;
            //指定当前选中的节点
            try
            {
                //this.SelectedNode=this.Nodes[branch];
                //在父级节点下增加新节点
                this.Nodes[branch].Nodes.Add(node);
            }
            catch
            {
                this.Nodes.Add(new System.Windows.Forms.TreeNode("患者"));
                //this.SelectedNode=this.Nodes[0];
                //在选中的节点上增加新节点
                this.Nodes[0].Nodes.Add(node);
            }

            //在选中的节点上增加新节点
            //this.SelectedNode.Nodes.Add(node);
        }

        /// <summary>
        /// 根据传入参数,插入新节点
        /// </summary>
        /// <param name="branch">一级节点索引</param>
        /// <param name="patientInfo">患者信息</param>
        public void AddTreeNode(int branch, FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            try
            {
                
                //节点信息
                FS.FrameWork.Models.NeuObject nodeTextInfo = new FS.FrameWork.Models.NeuObject();
                nodeTextInfo.ID = patientInfo.PID.PatientNO;				//住院号
                nodeTextInfo.Name = patientInfo.Name;								//患者姓名
                nodeTextInfo.Memo = patientInfo.PVisit.PatientLocation.Bed.ID;		//床号
                nodeTextInfo.User01 = patientInfo.PVisit.PatientLocation.Dept.Name;	//科室名称
                nodeTextInfo.User02 = patientInfo.PVisit.InState.Name;				//在院状态
                nodeTextInfo.User03 = patientInfo.Sex.ID.ToString();		//性别
                //根据患者的入院日期,判断是否显示"(新)"
                if (this.bIsShowNewPatient)
                {
                    if (dtToday.Date == patientInfo.PVisit.InTime.Date)
                        nodeTextInfo.Name = nodeTextInfo.Name + "(新)";
                }

                //定义节点的引用,指向要修改的节点
                this.AddTreeNode(branch, nodeTextInfo,patientInfo.ID, patientInfo);
            }
            catch { }
        }


        /// <summary>
        /// 根据传入参数,创建节点信息
        /// </summary>
        /// <param name="neuObj">节点Text信息:obj.id ,name,memo=bed,user01=dept,user02=status user03=sex </param>
        /// <param name="obj">节点的Tag属性</param>
        /// <param name="node">返回参数:节点</param>
        private void CreateNodeInfo(FS.FrameWork.Models.NeuObject neuObj, object obj, ref System.Windows.Forms.TreeNode node)
        {
            //如果传入节点为空,则新建一个节点
            if (node == null)
                node = new System.Windows.Forms.TreeNode();

            #region 生成节点的Text
            string strText = neuObj.Name; //患者姓名
            string strMemo = "";
            switch (this.myShowType.GetHashCode())
            {
                case 1:
                    //住院号
                    strMemo = "【" + neuObj.ID + "】";
                    break;
                case 3:
                    //科室
                    if (neuObj.User01 != "" || neuObj.User01 != null) strMemo = "【" + neuObj.User01 + "】";
                    break;
                case 5:
                    //病床
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
                            strMemo = "【" + strMemo + "】";
                        }
                    }
                    break;
                case 7:
                    //状态
                    strMemo = "【" + neuObj.User02 + "】";
                    break;
                case 4:
                    //科室+住院号
                    strMemo = "【" + neuObj.User01 + "】" + "【" + neuObj.ID + "】";
                    break;
                case 6:
                    //病床+住院号
                    if (neuObj.Memo != "" || neuObj.Memo != null)
                        strMemo = "【" + neuObj.Memo.Substring(4) + "】" + "【" + neuObj.ID + "】";
                    else
                        strMemo = "【" + neuObj.ID + "】";
                    break;
                case 8:
                    //住院号+状态
                    strMemo = "【" + neuObj.ID + "】" + "【" + neuObj.User02 + "】";
                    break;
                case 10:
                    //科室+状态
                    strMemo = "【" + neuObj.User01 + "】" + "【" + neuObj.User02 + "】";
                    break;
                case 12:
                    //病床+状态
                    if (neuObj.Memo != "" || neuObj.Memo != null)
                        strMemo = "【" + neuObj.Memo.Substring(4) + "】" + "【" + neuObj.User02 + "】";
                    else
                        strMemo = "【" + neuObj.User02 + "】";
                    break;
                default:
                    strMemo = "";
                    break;
            }

            //根据显示位置,确定最终的名称
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

            //生产节点的ImageIndex
            switch (neuObj.User03)
            {
                case "F":
                    //男
                    if (((FS.FrameWork.Models.NeuObject)obj).ID.IndexOf("B") > 0)
                        node.ImageIndex = this.GirlImageIndex;	//婴儿女
                    else
                        node.ImageIndex = this.FemaleImageIndex;	//成年女
                    break;
                case "M":
                    if (((FS.FrameWork.Models.NeuObject)obj).ID.IndexOf("B") > 0)
                        node.ImageIndex = this.BabyImageIndex;	//婴儿男
                    else
                        node.ImageIndex = this.MaleImageIndex;	//成年男
                    break;
                default:
                    node.ImageIndex = this.MaleImageIndex;
                    break;
            }

            if (obj is FS.HISFC.Models.RADT.PatientInfo)
            {
                if (((FS.HISFC.Models.RADT.PatientInfo)obj).IsPtjtState)
                {
                    node.NodeFont = new Font("宋体 ", 9, FontStyle.Bold);

                }

            }

            //生产节点的SelectedImageIndex
            node.SelectedImageIndex = node.ImageIndex + 1;

            //生产节点的Tag属性
            node.Tag = obj;
        }


        /// <summary>
        /// 根据患者信息和一级节点,查找其子节点中患者所在的节点Index
        /// </summary>
        /// <param name="branch"></param>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public System.Windows.Forms.TreeNode FindNode(int branch, FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            FS.HISFC.Models.RADT.PatientInfo findPatient = null;
            foreach (System.Windows.Forms.TreeNode node in this.Nodes[branch].Nodes)
            {
                //取节点上的患者信息
                findPatient = node.Tag as FS.HISFC.Models.RADT.PatientInfo;
                //如果不能转换为患者信息,则继续查找下一个节点
                if (findPatient == null) continue;
                //如果找到,则返回此节点
                if (findPatient.ID == patientInfo.ID) return node;
            }

            //如果没有找到,则返回null
            return null;
        }

        /// <summary>
        /// 根据门诊患者信息和一级节点,查找其子节点中患者所在的节点Index
        /// </summary>
        /// <param name="branch"></param>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public System.Windows.Forms.TreeNode FindNode(int branch, FS.HISFC.Models.Registration.Register patientInfo)
        {
            FS.HISFC.Models.Registration.Register findPatient = null;
            foreach (System.Windows.Forms.TreeNode node in this.Nodes[branch].Nodes)
            {
                //取节点上的患者信息
                findPatient = node.Tag as FS.HISFC.Models.Registration.Register;
                //如果不能转换为患者信息,则继续查找下一个节点
                if (findPatient == null) continue;
                //如果找到,则返回此节点
                if (findPatient.ID == patientInfo.ID) return node;
            }

            //如果没有找到,则返回null
            return null;
        }

        /// <summary>
        /// 添加根节点
        /// </summary>
        protected virtual void AddRootNode()
        {
            this.Nodes.Add(new System.Windows.Forms.TreeNode("患者"));
        }


        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            this.ImageList = this.imageList1;
            this.HideSelection = false;

            try
            {
                if (this.IsShowContextMenu == true)//显示属性
                {
                    FS.FrameWork.WinForms.Controls.NeuContextMenuStrip contextMenu1 = new FS.FrameWork.WinForms.Controls.NeuContextMenuStrip();
                    contextMenu1.Items.Clear();
            
                    // 加入右键菜单  by zlw 2006-5-1
                    System.Windows.Forms.ContextMenu cmPatientPro = new System.Windows.Forms.ContextMenu();
                    System.Windows.Forms.MenuItem miPatientPro = new System.Windows.Forms.MenuItem();
                    System.Windows.Forms.MenuItem miPatientPro1 = new System.Windows.Forms.MenuItem();
                    System.Windows.Forms.MenuItem miPatientPro2 = new System.Windows.Forms.MenuItem();
                    System.Windows.Forms.MenuItem miPatientPro3 = new System.Windows.Forms.MenuItem();

                    cmPatientPro.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { miPatientPro, miPatientPro1, miPatientPro2, miPatientPro3 });// {1292AA34-6E62-4f01-9BE7-EAE667D34E5D}

                    miPatientPro.Text = "查看患者信息";

                    miPatientPro1.Text = "婴儿腕带(仅婴儿名字)";// {1292AA34-6E62-4f01-9BE7-EAE667D34E5D}

                    miPatientPro2.Text = "婴儿腕带(详细信息)";

                    miPatientPro3.Text = "母亲腕带";// {AB709343-3691-4776-B4DB-2F5BCD3D40F3}

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

        #region 事件
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
                    findPatient.User01 = "名字";
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
                    findPatient.User01 = "详细信息";
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
