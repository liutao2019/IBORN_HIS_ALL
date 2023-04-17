using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.PrescriptionComment
{
    /// <summary>
    /// [功能描述: 门诊配发药用的处方树]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 说明：
    /// 1、本控件只做显示数据，获取用户操作数据，所有违反此原则的一律不允许
    /// 2、因为处方查询的复杂性试图添加一个树的想法是比较冲动的，不要轻易尝试
    /// </summary>
    public partial class ucPrescriptionCommentTree : UserControl
    {
        #region 构造函数
        public ucPrescriptionCommentTree()
        {
            InitializeComponent();
            this.InitDoct();

        }

        public ucPrescriptionCommentTree(string tabPage1Name, string tabPage2Name)
        {
            InitializeComponent();
            this.TabPage1Name = tabPage1Name;
        }
        #endregion

        /// <summary>
        /// 初始化医生列表
        /// </summary>
        /// <returns></returns>
        public int InitDoct()
        {
           ArrayList allDoct = this.personMgr.GetEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (allDoct == null || allDoct.Count == 0)
            {
                this.ShowMessage("初始化医生列表出错,请联系信息科！", MessageBoxIcon.Error);
                    return -1;
            }
            this.nDoct.AddItems(allDoct);
            return 1;
        }

        #region 变量属性

        #region tabpage的标题

        public string tabPage1Name;
        public string TabPage1Name
        {
            get { return this.tabPage1.Text; }
            set { this.tabPage1.Text = value; }
        }

        //开始时间
        public DateTime beginTime;
        public DateTime BeginTime
        {
            get { return this.neuDateTimePicker1.Value; }
            set { this.neuDateTimePicker1.Value = value; }
        }

        //结束时间
        public DateTime endTime;
        public DateTime EndTime
        {
            get { return this.neuDateTimePicker2.Value; }
            set { this.neuDateTimePicker2.Value = value; }
        }

        //医生工号
        public string doctCode;
        public String DoctCode
        {
            get { return this.nDoct.Text;}
            set
            {
                if (string.IsNullOrEmpty(this.nDoct.Text))
                {
                  this.doctCode = "ALL";
                }
                else
                {
                    this.doctCode = this.nDoct.Text;
                }
              }
        }
        #endregion

        /// <summary>



        #region 查询类型
        /// <summary>
        /// 查找处方的查询类型
        /// </summary>
        enum EnumQueryType
        {
            发票号,
            就诊卡号,
            处方号,
            流水号,
            //健康卡,
            /// <summary>
            /// 在End之前添加类别，End标识结束
            /// </summary>
            End
        }

        /// <summary>
        /// 查询类型
        /// </summary>
        EnumQueryType queryType = EnumQueryType.发票号;
        #endregion

        #region 是否按发票显示处方
        public bool isShowInvoice = true;
        public Boolean IsShowInvoice
        {
            get
            {
                return this.isShowInvoice;
            }
            set
            {
                this.isShowInvoice = value;
            }
        }
        #endregion

        #region 当前操作处方
        FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe = null;

        /// <summary>
        /// 当前操作处方
        /// </summary>
        public FS.HISFC.Models.Pharmacy.DrugRecipe DrugRecipe
        {
            get { return drugRecipe; }
            set { drugRecipe = value; }
        }

        List <FS.HISFC.Models.Pharmacy.DrugRecipe> allDrugRecipe = null;
        public List<FS.HISFC.Models.Pharmacy.DrugRecipe> AllDrugRecipe
        {
            get { return allDrugRecipe; }
            set { allDrugRecipe = value; }
        }

        #endregion


        /// <summary>
        /// 人员管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();



        #region 当前操作的科室
        private FS.FrameWork.Models.NeuObject priveDept = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// 当前操作的科室
        /// </summary>
        [Browsable(false)]
        public FS.FrameWork.Models.NeuObject PriveDept
        {
            get { return priveDept; }
            set { priveDept = value; }
        }
        #endregion

        #region 原子化各操作
        /// <summary>
        /// 原子化各操作
        /// </summary>
        bool isAtomizationOnOper = true;

        /// <summary>
        /// 原子化各操作
        /// </summary>
        [Description("原子化各操作"), Category("设置"), Browsable(false)]
        public bool IsAtomizationOnOper
        {
            get { return isAtomizationOnOper; }
            set { isAtomizationOnOper = value; }
        }
        #endregion

        #endregion

        #region 代理
        /// <summary>
        /// 处方树已选择节点通知其他控件显示处方信息
        /// 
        /// </summary>
        public delegate void RecipeAfterSelectHandler();
        /// <summary>
        /// 处方树已选择节点通知其他控件显示处方信息
        /// </summary>
        public RecipeAfterSelectHandler RecipeAfterSelectEven;

        /// <summary>
        /// 向其他控件发送处方树的消息，该消息希望以气泡形式显示
        /// </summary>
        /// <param name="text">消息文本</param>
        /// <param name="timemout">延迟时间</param>
        /// <param name="toolTipIcon">气泡图标</param>
        public delegate void ShowBalloonTipHandler(string text, int timemout, ToolTipIcon toolTipIcon);
        /// <summary>
        /// 向其他控件发送处方树的消息，该消息希望以气泡形式显示
        /// </summary>
        public ShowBalloonTipHandler ShowBalloonTipEven;

        /// <summary>
        /// 发生在查询处方
        /// </summary>
        public delegate void RecipeQueryAfterHandler(int param);
        /// <summary>
        /// 发生在查询处方
        /// </summary>
        public RecipeQueryAfterHandler RecipeQueryAfterEven;
        #endregion

        #region 事件
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                try
                {
                    string queryType = ((int)this.queryType).ToString();
                    queryType = FS.SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientPrescriptionComment.xml", "ReipeQueryType", "Dept" + this.PriveDept.ID + "Type", queryType);
                    this.queryType = (EnumQueryType)(FS.FrameWork.Function.NConvert.ToInt32(queryType));
                    this.nlbQueryType.Text = this.queryType.ToString() + "：";
                }
                catch
                {
                    this.queryType = EnumQueryType.发票号;
                }
            }

            this.nlbQueryType.Click += new EventHandler(nlbQueryType_Click);
            this.tvBaseTree1.AfterSelect += new TreeViewEventHandler(tvBaseTree_AfterSelect);
            this.ntxtBillNO.KeyPress += new KeyPressEventHandler(ntxtBillNO_KeyPress);
            this.ntxtBillNO.KeyDown += new KeyEventHandler(ntxtBillNO_KeyDown);
        }

        void ntxtBillNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                this.SelectNode(true);
                this.ntxtBillNO.SelectAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                this.SelectNode(false);
                this.ntxtBillNO.SelectAll();
            }
        }

        void ntxtBillNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            //回车键
            if (e.KeyChar == (char)13)
            {
                int param = QueryRecipe();
                if (this.RecipeQueryAfterEven != null)
                {
                    RecipeQueryAfterEven(param);
                }

                this.ntxtBillNO.SelectAll();

            }
        }


        void tvBaseTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.ShowRecipeInfo(e.Node,this.isShowInvoice);
        }

        void nlbQueryType_Click(object sender, EventArgs e)
        {
            this.ChangeQueryType();
        }
        #endregion

        #region 方法

        /// <summary>
        /// 清除显示的信息
        /// </summary>
        public void Clear()
        {
            this.tvBaseTree1.Nodes.Clear();
        }

        /// <summary>
        /// 清除显示的信息
        /// </summary>
        /// <param name="state">0第一树 1第二树 其它值不清</param>
        public void Clear(int state)
        {
            if (state == 0)
            {
                this.tvBaseTree1.Nodes.Clear();
            }
        }

        /// <summary>
        /// 显示处方列表 向AddTree内增加数据
        /// </summary>
        /// <param name="alDrugRecipe">列表数组</param>
        /// <param name="isSupplemental">是否在列表内追加显示</param>
        /// <param name="state">处方状态</param>
        public int ShowRecipeList(ArrayList alDrugRecipe, bool isSupplemental, string state,bool isShowInvoice)
        {
            if (state == "0")
            {
                this.tvBaseTree1.ShowList(alDrugRecipe, isSupplemental, isShowInvoice);
            }
            else
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取处方树的处方调剂信息
        /// </summary>
        /// <param name="treeState">0第一个树 1第二个树</param>
        /// <param name="recipeState">处方状态</param>
        /// <returns>处方调剂信息数组</returns>
        public ArrayList GetRecipeList(string treeState, string recipeState)
        {
            ArrayList alRecipe = new ArrayList();
            TreeView tv = new TreeView();
            if (treeState == "0")
            {
                tv = this.tvBaseTree1;
            }
            foreach (TreeNode node in tv.Nodes)
            {
                if (node.Tag == null)
                {
                    continue;
                }
                FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe = node.Tag as FS.HISFC.Models.Pharmacy.DrugRecipe;
                if (drugRecipe.RecipeState == recipeState)
                {
                    alRecipe.Add(drugRecipe);
                }
            }
            return alRecipe;
        }

        /// <summary>
        /// 获取树中的处方列表
        /// </summary>
        /// <param name="state">0第一个树 1第二个树</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Pharmacy.DrugRecipe> GetRecipeList(int state)
        {
            List<FS.HISFC.Models.Pharmacy.DrugRecipe> listRecipe = new List<FS.HISFC.Models.Pharmacy.DrugRecipe>();
            SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Base.tvRecipeBaseTree tv = new  SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Base.tvRecipeBaseTree();
            if (state == 0)
            {
                tv = this.tvBaseTree1;
            }
            else
            {
                return listRecipe;
            }
            foreach (TreeNode node in tv.Nodes)
            {
                if (node.Tag is FS.HISFC.Models.Pharmacy.DrugRecipe)
                {
                    listRecipe.Add(node.Tag as FS.HISFC.Models.Pharmacy.DrugRecipe);
                }
            }
            return listRecipe;
        }

        /// <summary>
        /// 将焦点设置到处方查询处
        /// </summary>
        public void FocusBillNO()
        {

            this.ntxtBillNO.Select();
            this.ntxtBillNO.SelectAll();
            this.ntxtBillNO.Focus();
        }

        /// <summary>
        /// 转换查询录入的发票号等
        /// </summary>
        public string ConvertQueryNO()
        {
            string queryNO = this.ntxtBillNO.Text.Trim();

                switch (this.queryType)
                {
                    case EnumQueryType.就诊卡号:

                        FS.HISFC.Models.Account.AccountCard accountCardObj = new FS.HISFC.Models.Account.AccountCard();

                        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
                        if (feeIntegrate.ValidMarkNO(queryNO, ref accountCardObj) <= 0)
                        {
                            MessageBox.Show(feeIntegrate.Err);
                        }
                        queryNO = accountCardObj.Patient.PID.CardNO;

                        this.ntxtBillNO.Text = queryNO;//queryNO.PadLeft(10, '0');

                        //queryNO = this.ntxtBillNO.Text;
                        break;
                    case EnumQueryType.处方号:
                        break;
                    case EnumQueryType.发票号:
                        this.ntxtBillNO.Text = queryNO.PadLeft(12, '0');
                        queryNO = this.ntxtBillNO.Text;
                        break;
                    case EnumQueryType.流水号:
                        break;
                }
            

            return queryNO;
        }

        /// <summary>
        /// 设置处方树的处方状态
        /// </summary>
        /// <param name="state1">第一个处方树的状态</param>
        /// <param name="state2">第二个处方树的状态</param>
        public void SetTreeState(string state1)
        {
            this.tvBaseTree1.State = state1;
        }

        /// <summary>
        /// 选择下一个节点
        /// </summary>
        /// <param name="isUp">是否向上选择</param>
        public void SelectNode(bool isUp)
        {
            if (isUp)
            {
                if (this.neuTabControl1.SelectedTab == tabPage1)
                {
                    if (tvBaseTree1.SelectedNode != null && tvBaseTree1.SelectedNode.PrevVisibleNode != null)
                    {
                        tvBaseTree1.SelectedNode = tvBaseTree1.SelectedNode.PrevVisibleNode;
                    }
                    else if (tvBaseTree1.SelectedNode == null && tvBaseTree1.Nodes.Count > 0)
                    {
                        tvBaseTree1.SelectedNode = tvBaseTree1.Nodes[tvBaseTree1.Nodes.Count - 1];
                    }
                }
            }
            else
            {
                if (this.neuTabControl1.SelectedTab == tabPage1)
                {
                    if (tvBaseTree1.SelectedNode != null && tvBaseTree1.SelectedNode.NextVisibleNode != null)
                    {
                        tvBaseTree1.SelectedNode = tvBaseTree1.SelectedNode.NextVisibleNode;
                    }
                    else if (tvBaseTree1.SelectedNode == null && tvBaseTree1.Nodes.Count > 0)
                    {
                        tvBaseTree1.SelectedNode = tvBaseTree1.Nodes[0];
                    }
                }
            }
        }


        /// <summary>
        /// 更改查询方式
        /// </summary>
        public void ChangeQueryType()
        {
            if ((int)this.queryType + 1 == (int)EnumQueryType.End)
            {
                this.queryType = (EnumQueryType)0;
            }
            else
            {
                this.queryType = (EnumQueryType)((int)this.queryType + 1);
            }
            this.nlbQueryType.Text = this.queryType.ToString() + "：";

            try
            {
                FS.SOC.Public.XML.SettingFile.SaveSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientDrugStore.xml", "ReipeQueryType", "Dept" + this.PriveDept.ID + "Type", ((int)this.queryType).ToString());
            }
            catch
            { }
        }

        /// <summary>
        /// 选择处方树
        /// </summary>
        /// <param name="index">索引0第一个树 1第二树</param>
        public void SelectTree(int index)
        {
            if (index == 0)
            {
                if (this.neuTabControl1.SelectedTab != tabPage1)
                {
                    this.neuTabControl1.SelectedTab = tabPage1;
                }
                else if (this.tvBaseTree1.Nodes.Count > 0)
                {
                    if (this.tvBaseTree1.SelectedNode == null)
                    {
                        this.tvBaseTree1.SelectedNode = this.tvBaseTree1.Nodes[0];
                    }
                    else
                    {
                        this.ShowRecipeInfo(this.tvBaseTree1.SelectedNode,this.isShowInvoice);
                    }
                }
                this.tvBaseTree1.Select();
                this.tvBaseTree1.Focus();
            }
        }

        /// <summary>
        /// 处方查询
        /// </summary>
        private int QueryRecipe()
        {
            if (string.IsNullOrEmpty(this.ntxtBillNO.Text.Trim()))
            {
                return -1;
            }

            string queryNO = "";

            queryNO = this.ConvertQueryNO();
            int findedType1 = -1;


            findedType1 = this.FindRecipeAtTree(this.tvBaseTree1, queryNO);

            int findedType2 = -1;


            //两个树的搜索结果有6种情况：参考上面的说明
            if (findedType1 == -1)
            {
                findedType2 = this.FindRecipeAtDataBase();
                if (findedType1 == -1 && findedType2 == -1)
                {
                    this.ShowMessage("没有找到处方信息，请检查输入条件是否正确！",MessageBoxIcon.Information);
                }
                //在数据库查找
                return findedType2;
            }
            return -1;
        }

        /// <summary>
        /// 在树中查找处方
        /// 返回值，不要轻易改动，否则影响自动保存
        /// </summary>
        /// <param name="tv">处方树</param>
        /// <returns>0当前选择处方就是要查找的处方 1找到处方 -1没有找到处方</returns>
        private int FindRecipeAtTree(SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Base.tvRecipeBaseTree tv, string queryNO)
        {
            if (!isShowInvoice)
            {
                if (tv.Nodes.Count == 0)
                {
                    return -1;
                }

                //非处方号查找其实也包括处方号查找，用户以处方号方式查找时单独处理处方号查找是为了快速返回结果

                #region 处方号查找
                if (this.queryType == EnumQueryType.处方号)
                {
                    if (this.drugRecipe != null && string.Equals(this.drugRecipe.RecipeNO, queryNO))
                    {
                        return 0;
                    }
                    foreach (TreeNode node in tv.Nodes)
                    {
                        if (node.Tag != null)
                        {
                            FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe = node.Tag as FS.HISFC.Models.Pharmacy.DrugRecipe;

                            if (drugRecipe != null && drugRecipe.RecipeNO == queryNO)
                            {
                                tv.SelectedNode = node;
                                return 1;
                            }
                        }
                    }

                }
                #endregion

                #region 非处方号查找

                bool finded = false;
                int findedType = -1;

                //一张发票多个处方情况下查找，为了实现在几个处方中来回切换的功能，说明如下：
                //第一个循环在当前选中的节点之后查找，但是树必须有选中的节点
                //第二个循环在当前选中的节点之前查找，如果没有选中节点则是全树查找
                //两个循环都找不到的时候如果返回0，即表示当前节点即为要找的处方

                //一般会在当前操作树中查找，然后转到没操作的那个树，而没有操作的树一般是没有选中节点的，如此则直接跳到第二个循环中查找
                if (tv.SelectedNode != null)
                {
                    foreach (TreeNode node in tv.Nodes)
                    {
                        if (node.Tag != null)
                        {
                            FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe = node.Tag as FS.HISFC.Models.Pharmacy.DrugRecipe;

                            //首先找到当前选择处方在树中的位置，找到后findedType赋值为0，然后继续往后查找
                            if (this.drugRecipe != null && drugRecipe.RecipeNO != this.drugRecipe.RecipeNO)
                            {
                                //findedType=0指示到达了当前选中节点的位置
                                if (findedType == -1)
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                //找到当前选中的节点，findedType=0指示到达了当前选中节点的位置
                                findedType = 0;

                                //当前选中的处方也忽略，继续往后查找
                                continue;
                            }

                            finded = this.QueryNOEquals(drugRecipe, queryNO);

                            if (finded)
                            {
                                tv.SelectedNode = node;
                                findedType = 1;
                                break;
                            }
                        }
                    }
                }
                if (finded)
                {
                    return findedType;
                }

                //第二个循环在当前选中的节点之前查找，如果没有选中节点则是全树查找
                foreach (TreeNode node in tv.Nodes)
                {
                    if (node.Tag != null)
                    {
                        FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe = node.Tag as FS.HISFC.Models.Pharmacy.DrugRecipe;

                        //不查找当前已经选中的处方，回到原位时停止查找
                        if (this.drugRecipe != null && drugRecipe.RecipeNO == this.drugRecipe.RecipeNO)
                        {
                            findedType = 0;
                            break;
                        }

                        finded = this.QueryNOEquals(drugRecipe, queryNO);

                        if (finded)
                        {
                            tv.SelectedNode = node;
                            findedType = 1;
                            break;
                        }
                    }
                }
                if (finded)
                {
                    //这个表示在当前选择的处方前后找到符合条件的处方
                    return findedType;
                }
                else
                {
                    //当前选择的处方符合查询条件
                    finded = this.QueryNOEquals(this.drugRecipe, queryNO);
                    if (finded)
                    {
                        return 0;
                    }
                }
                #endregion

                return -1;
            }

            else
            {
                if (tv.Nodes.Count == 0)
                {
                    return -1;
                }

                //非处方号查找其实也包括处方号查找，用户以处方号方式查找时单独处理处方号查找是为了快速返回结果
                #region 处方号查找
                if (this.queryType == EnumQueryType.处方号)
                {
                    Hashtable hsRecipNo = new Hashtable();
                    if (tv.SelectedNode != null)
                    {         
                        List<FS.HISFC.Models.Pharmacy.DrugRecipe> selectedData = tv.SelectedNode.Tag as List<FS.HISFC.Models.Pharmacy.DrugRecipe>;
                        foreach (FS.HISFC.Models.Pharmacy.DrugRecipe dr in selectedData)
                        {
                            hsRecipNo.Add(dr.RecipeNO, dr);
                        }
                        if (hsRecipNo != null && hsRecipNo.Contains(queryNO))
                        {
                            return 0;
                        }
                    }
                    foreach (TreeNode node in tv.Nodes)
                    {
                        if (node.Tag != null)
                        {
                            List<FS.HISFC.Models.Pharmacy.DrugRecipe> alDrugRecipe = node.Tag as List<FS.HISFC.Models.Pharmacy.DrugRecipe>;
                            hsRecipNo = new Hashtable();
                            foreach (FS.HISFC.Models.Pharmacy.DrugRecipe dr in alDrugRecipe)
                            {
                                hsRecipNo.Add(dr.RecipeNO, dr);
                            }
                            if (hsRecipNo != null && hsRecipNo.Contains(queryNO))
                            {
                                tv.SelectedNode = node;
                                return 1;
                            }
                        }
                    }

                }
                #endregion

                #region 非处方号查找

                bool finded = false;
                int findedType = -1;

                //一张发票多个处方情况下查找，为了实现在几个处方中来回切换的功能，说明如下：
                //第一个循环在当前选中的节点之后查找，但是树必须有选中的节点
                //第二个循环在当前选中的节点之前查找，如果没有选中节点则是全树查找
                //两个循环都找不到的时候如果返回0，即表示当前节点即为要找的处方

                //一般会在当前操作树中查找，然后转到没操作的那个树，而没有操作的树一般是没有选中节点的，如此则直接跳到第二个循环中查找
                if (tv.SelectedNode != null)
                {
                    foreach (TreeNode node in tv.Nodes)
                    {
                        if (node.Tag != null)
                        {
                            List<FS.HISFC.Models.Pharmacy.DrugRecipe> allData = node.Tag as List<FS.HISFC.Models.Pharmacy.DrugRecipe>;
                            foreach (FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe in allData)
                            {

                                //首先找到当前选择处方在树中的位置，找到后findedType赋值为0，然后继续往后查找
                                if (this.drugRecipe != null && drugRecipe.RecipeNO != this.drugRecipe.RecipeNO)
                                {
                                    //findedType=0指示到达了当前选中节点的位置
                                    if (findedType == -1)
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    //找到当前选中的节点，findedType=0指示到达了当前选中节点的位置
                                    findedType = 0;

                                    //当前选中的处方也忽略，继续往后查找
                                    continue;
                                }

                                finded = this.QueryNOEquals(drugRecipe, queryNO);

                                if (finded)
                                {
                                    tv.SelectedNode = node;
                                    findedType = 1;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (finded)
                {
                    return findedType;
                }

                //第二个循环在当前选中的节点之前查找，如果没有选中节点则是全树查找
                foreach (TreeNode node in tv.Nodes)
                {
                    if (node.Tag != null)
                    {
                        List<FS.HISFC.Models.Pharmacy.DrugRecipe> allData = node.Tag as List<FS.HISFC.Models.Pharmacy.DrugRecipe>;
                        foreach (FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe in allData)
                        {

                            //不查找当前已经选中的处方，回到原位时停止查找
                            if (this.drugRecipe != null && drugRecipe.RecipeNO == this.drugRecipe.RecipeNO)
                            {
                                findedType = 0;
                                break;
                            }

                            finded = this.QueryNOEquals(drugRecipe, queryNO);

                            if (finded)
                            {
                                tv.SelectedNode = node;
                                findedType = 1;
                                break;
                            }
                        }
                    }
                }
                if (finded)
                {
                    //这个表示在当前选择的处方前后找到符合条件的处方
                    return findedType;
                }
                else
                {
                    //当前选择的处方符合查询条件
                    finded = this.QueryNOEquals(this.drugRecipe, queryNO);
                    if (finded)
                    {
                        return 0;
                    }
                }
                #endregion

                return -1;
            }
        }

        /// <summary>
        /// 在数据库中查找处方
        /// </summary>
        /// <returns></returns>
        private int FindRecipeAtDataBase()
        {
            #region 在数据库中查找
            int billType = -1;//单据类型 0 处方号 1 发票号 2 病历卡号
            switch (this.queryType)
            {
                case EnumQueryType.就诊卡号:
                    billType = 2;
                    break;
                case EnumQueryType.处方号:
                    billType = 0;
                    break;
                case EnumQueryType.发票号:
                    billType = 1;
                    break;
                case EnumQueryType.流水号:
                    billType = 3;
                    break;
            }

            bool finded1 = false;

            FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();


            #region 查询属于第一树的数据
            //直接查询所有正记录，包括发药未发药的
            ArrayList al = drugStoreMgr.QueryDrugRecipe("A", "M1", "A", billType, this.ntxtBillNO.Text.Trim());
            if (al == null)
            {
                this.ShowMessage("查询处方调剂信息出错：" + drugStoreMgr.Err, MessageBoxIcon.Error);
                return -1;
            }
            if (al.Count > 0)
            {
                finded1 = true;
                this.tvBaseTree1.ShowList(al, true, true);
            }

            if (finded1)
            {
                this.neuTabControl1.SelectedTab = this.tabPage1;

                //处方号查询结束，同张处方在同一药房只有一个状态，既然已经找到数据就不需要再查询
                if (billType == 0)
                {
                    return 1;
                }
            }
            #endregion

            if (!(finded1))
            {
                this.ShowMessage("没有找到处方信息，请检查输入的信息是否正确", MessageBoxIcon.Question);
                return -1;
            }
            return 1;
            #endregion
        }

        /// <summary>
        /// 当前查询方式下比较处方对应的编号和查询号是否相等
        /// </summary>
        /// <param name="drugRecipe">处方实体</param>
        /// <param name="queryNO">查询号</param>
        /// <returns>true 相等 false不相等</returns>
        private bool QueryNOEquals(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string queryNO)
        {
            if (drugRecipe == null)
            {
                return false;
            }
            bool finded = false;
            switch (this.queryType)
            {
                case EnumQueryType.就诊卡号:
                    finded = string.Equals(drugRecipe.CardNO, queryNO);
                    break;
                case EnumQueryType.处方号:
                    finded = string.Equals(drugRecipe.RecipeNO, queryNO);
                    break;
                case EnumQueryType.发票号:
                    finded = string.Equals(drugRecipe.InvoiceNO, queryNO);
                    break;
                case EnumQueryType.流水号:
                    finded = string.Equals(drugRecipe.ClinicNO, queryNO);
                    break;
                //case EnumQueryType.健康卡:
                //    finded = string.Equals(drugRecipe.CardNO, queryNO);
                //    break;
            }

            return finded;
        }

        /// <summary>
        /// 显示处方树节点的编号：处方号、发票号、病例号
        /// </summary>
        /// <param name="node">处方树节点</param>
        private void ShowRecipeInfo(TreeNode node,bool isShowInvoice)
        {
            if(!isShowInvoice)
            {
                if (node.Tag != null)
                {
                    this.drugRecipe = node.Tag as FS.HISFC.Models.Pharmacy.DrugRecipe;
                    switch (this.queryType)
                    {
                        case EnumQueryType.就诊卡号:
                            this.ntxtBillNO.Text = this.drugRecipe.CardNO;
                            break;
                        case EnumQueryType.处方号:
                            this.ntxtBillNO.Text = this.drugRecipe.RecipeNO;
                            break;
                        case EnumQueryType.发票号:
                            this.ntxtBillNO.Text = this.drugRecipe.InvoiceNO;
                            break;
                        case EnumQueryType.流水号:
                            this.ntxtBillNO.Text = this.drugRecipe.ClinicNO;
                            break;
                    }
                }
            }
                else
                {
                    if (node.Tag != null)
                    {
                        List<FS.HISFC.Models.Pharmacy.DrugRecipe> allData = (List<FS.HISFC.Models.Pharmacy.DrugRecipe>)node.Tag;
                        this.drugRecipe = allData[0] as FS.HISFC.Models.Pharmacy.DrugRecipe;
                        this.allDrugRecipe = new List<FS.HISFC.Models.Pharmacy.DrugRecipe>();
                        this.allDrugRecipe = allData;
                        switch (this.queryType)
                        {
                            case EnumQueryType.就诊卡号:
                                this.ntxtBillNO.Text = this.drugRecipe.CardNO;
                                break;
                            case EnumQueryType.处方号:
                                this.ntxtBillNO.Text = this.drugRecipe.RecipeNO;
                                break;
                            case EnumQueryType.发票号:
                                this.ntxtBillNO.Text = this.drugRecipe.InvoiceNO;
                                break;
                            case EnumQueryType.流水号:
                                this.ntxtBillNO.Text = this.drugRecipe.ClinicNO;
                                break;
                        }
                    }
                }
            

            if (this.RecipeAfterSelectEven != null)
            {
                this.RecipeAfterSelectEven();
            }
        }

        /// <summary>
        /// 以提示形式显示MessageBox
        /// </summary>
        /// <param name="text">提示文本</param>
        private void ShowMessage(string text)
        {
            ShowMessage(text, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 根据图标显示自动处理标题的MessageBox
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <param name="messageBoxIcon">图标</param>
        private void ShowMessage(string text, MessageBoxIcon messageBoxIcon)
        {
            string caption = "";
            switch (messageBoxIcon)
            {
                case MessageBoxIcon.Warning:
                    caption = "警告>>";
                    break;
                case MessageBoxIcon.Error:
                    caption = "错误>>";
                    break;
                default:
                    caption = "提示>>";
                    break;
            }

            MessageBox.Show(text, caption, MessageBoxButtons.OK, messageBoxIcon);
        }

        /// <summary>
        /// 气泡提示信息
        /// </summary>
        /// <param name="text">提示信息文本</param>
        /// <param name="timemout">延迟时间</param>
        private void ShowBalloonTip(string text, int timemout)
        {
            if (this.ShowBalloonTipEven != null)
            {
                this.ShowBalloonTipEven(text, timemout, ToolTipIcon.Info);
            }
            else
            {
                this.ShowMessage(text, MessageBoxIcon.None);
            }
        }

        #endregion
    }
}
