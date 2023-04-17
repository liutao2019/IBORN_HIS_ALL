using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Order.Medical.Controls
{
    public partial class ucPopedomManagement : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPopedomManagement()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 资质业务类
        /// </summary>
        private FS.HISFC.BizLogic.Order.Medical.Ability abyMgr = new FS.HISFC.BizLogic.Order.Medical.Ability();

        /// <summary>
        /// 
        /// </summary>
        private List<FS.HISFC.Models.Order.Medical.Popedom> popAdd = new List<FS.HISFC.Models.Order.Medical.Popedom>();

        /// <summary>
        /// 专业帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper specialityHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 职级帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper levelHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 手术规模帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper operateHelper = new FS.FrameWork.Public.ObjectHelper();
        //private FS.FrameWork.Public.ObjectHelper ehDrugLevel = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 药品性质
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper drugTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 特殊检查帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper checkInfoHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 组套权限维护帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper groupTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 工具条
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// Fp数据源DataSet
        /// </summary>
        private DataTable dtCheck = null;

        private ArrayList alEmp = new ArrayList();

        #region 工具条处理


        /// <summary>
        /// 注册工具条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("删除", "删除权限", 2, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// 重写保存按钮实现方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Save(object sender, object neuObject)
        {
            if (fpPopedom.Rows.Count > 0)
            {
                SavePopedom();
            }

            return base.Save(sender, neuObject);
        }

        /// <summary>
        /// 处理按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "删除":
                    DeletePopedom();
                    break;
                default: break;
            }

        }

        #endregion

        private bool isLevelWithCard = false;
        /// <summary>
        /// 是否与资质关联
        /// </summary>
        public bool IsLevelWithCard
        {
            get
            {
                return this.isLevelWithCard;
            }
            set
            {
                this.isLevelWithCard = value;
            }
        }

        #region 树形控件相关方法

        /// <summary>
        /// 在一个节点下加载专业节点
        /// </summary>
        /// <param name="Anode">父节点</param>
        /// <returns>子节点个数</returns>
        private int AddSpecNodes(TreeNode Anode)
        {
            if (this.isLevelWithCard)
            {
                foreach (FS.HISFC.Models.Base.Const cons in specialityHelper.ArrayObject)
                {
                    TreeNode pNode = new TreeNode();

                    pNode.Tag = cons.ID;
                    pNode.Text = cons.Name;
                    pNode.ToolTipText = "专业";
                    pNode.ImageIndex = 3;

                    int childCount = AddDoctorNodes(pNode, Anode);

                    if (childCount > 0)
                    {
                        Anode.Nodes.Add(pNode);
                    }
                }
            }
            else
            {
                foreach (FS.HISFC.Models.Base.Employee emp in alEmp)
                {
                    if (emp.Level.ID == Anode.Tag.ToString())
                    {
                        TreeNode pNode = new TreeNode();

                        pNode.Tag = emp.ID;
                        pNode.Text = emp.Name;
                        pNode.ToolTipText = "医生";
                        pNode.ImageIndex = 3;

                        Anode.Nodes.Add(pNode);
                    }
                }
            }

            return Anode.Nodes.Count;
        }

        /// <summary>
        /// 在一个节点下加载医生节点
        /// </summary>
        /// <param name="Anode">父节点</param>
        /// <param name="parentNode">父节点的父节点</param>
        /// <returns>子节点个数</returns>
        private int AddDoctorNodes(TreeNode Anode, TreeNode parentNode)
        {
            List<FS.HISFC.Models.Order.Medical.Ability> aList = abyMgr.QueryDoctorListBySpecLevl(Anode.Tag.ToString(), parentNode.Tag.ToString());
            foreach (FS.HISFC.Models.Order.Medical.Ability ability in aList)
            {
                TreeNode pNode = new TreeNode();

                pNode.Tag = ability.Employee.ID;
                pNode.Text = ability.Employee.Name;
                pNode.ToolTipText = "医生";
                pNode.ImageIndex = 3;

                Anode.Nodes.Add(pNode);
            }


            return Anode.Nodes.Count;
        }

        /// <summary>
        /// 查找节点
        /// </summary>
        /// <param name="findInfo"></param>
        private void FindNodes(string findInfo)
        {
            if (!this.isLevelWithCard)
            {
                //职级
                foreach (TreeNode node in tvDoctor.Nodes[0].Nodes)
                {
                    foreach (TreeNode doctNode in node.Nodes)
                    {
                        if (doctNode.Text.Contains(findInfo.Trim()) || 
                            doctNode.Tag.ToString().Contains(findInfo.Trim()))
                        {
                            this.tvDoctor.SelectedNode = doctNode;
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 初始化左树节点
        /// </summary>
        private void InitTreeViewNodesLeft()
        {
            tvDoctor.Nodes[0].Tag = "全部医生";
            tvDoctor.Nodes[0].Text = "全部医生";

            //加载医生职级节点
            foreach (FS.HISFC.Models.Base.Const cons in levelHelper.ArrayObject)
            {
                TreeNode pNode = new TreeNode();

                pNode.Tag = cons.ID;
                pNode.Text = cons.Name;
                pNode.ToolTipText = "医师职级";
                pNode.ImageIndex = 3;

                int childCount = AddSpecNodes(pNode);

                if (childCount > 0)
                {
                    tvDoctor.Nodes[0].Nodes.Add(pNode);
                }
            }
        }

        /// <summary>
        /// 初始化右树节点
        /// </summary>
        private void InitTreeViewNodesRight()
        {
            //加载手术规模节点
            foreach (FS.HISFC.Models.Base.Const cons in operateHelper.ArrayObject)
            {
                TreeNode pNode = new TreeNode();
                pNode.Tag = cons.ID;
                pNode.Text = cons.Name;
                pNode.ImageIndex = 4;
                pNode.ToolTipText = "权限";
                tvPopedom.Nodes[0].Nodes[0].Nodes.Add(pNode);
            }

            //加载药品性质节点
            foreach (FS.HISFC.Models.Base.Const cons in drugTypeHelper.ArrayObject)
            {
                TreeNode pNode = new TreeNode();
                pNode.Tag = cons.ID;
                pNode.Text = cons.Name;
                pNode.ImageIndex = 4;
                pNode.ToolTipText = "权限";
                tvPopedom.Nodes[0].Nodes[1].Nodes.Add(pNode);
            }

            //加载组套权限
            foreach (FS.HISFC.Models.Base.Const cons in this.groupTypeHelper.ArrayObject)
            {
                TreeNode pNode = new TreeNode();
                pNode.Tag = cons.ID;
                pNode.Text = cons.Name;
                pNode.ImageIndex = 4;
                pNode.ToolTipText = "权限";
                tvPopedom.Nodes[0].Nodes[2].Nodes.Add(pNode);
            }

            tvPopedom.ExpandAll();
        }
        #endregion

        #region 方法与事件

        /// <summary>
        /// 初始化Helper类型
        /// </summary>
        private void IniHelper()
        {
            specialityHelper.ArrayObject = GetConstant(FS.HISFC.Models.Base.EnumConstant.SPECIALITY);//专业
            levelHelper.ArrayObject = GetConstant(FS.HISFC.Models.Base.EnumConstant.LEVEL);//职级
            operateHelper.ArrayObject = GetConstant(FS.HISFC.Models.Base.EnumConstant.OPERATETYPE);//手术规模
            drugTypeHelper.ArrayObject = GetConstant(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY);//药品性质

            //增加组套权限维护
            groupTypeHelper.ArrayObject = CacheManager.GetConList("GROUPMANAGER");

            FS.HISFC.BizLogic.Manager.Person myPer = new FS.HISFC.BizLogic.Manager.Person();
            alEmp = myPer.GetEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);

        }

        /// <summary>
        /// 根据参数类型获得ArrayList
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private ArrayList GetConstant(FS.HISFC.Models.Base.EnumConstant type)
        {
            //常数管理类
            ArrayList constList = CacheManager.GetConList(type);
            if (constList == null)
                throw new FS.FrameWork.Exceptions.ReturnNullValueException();

            return constList;
        }

        private void ucPopedomManagement_Load(object sender, EventArgs e)
        {
            Application.DoEvents();
            IniHelper();
            InitTreeViewNodesLeft();
            InitTreeViewNodesRight();

            this.fpPopedom.RowCount = 0;
            this.InitCheckInfoDataSet();
            this.QueryCheckInfo();
            this.tvDoctor.Nodes[0].Expand();
        }

        private void tvPopedom_DoubleClick(object sender, EventArgs e)
        {
            if (CheckDoctTreeValid() == false)
            {
                return;
            }

            if (tvPopedom.SelectedNode.ToolTipText == "权限")
            {
                //填充数组
                FS.HISFC.Models.Order.Medical.Popedom popedomOne = new FS.HISFC.Models.Order.Medical.Popedom();

                popedomOne.EmplCode = tvDoctor.SelectedNode.Tag.ToString();
                popedomOne.EmplName = tvDoctor.SelectedNode.Text;
                popedomOne.Popedoms.Name = tvPopedom.SelectedNode.Text;
                if (tvPopedom.SelectedNode.Tag == null)
                {
                    popedomOne.PopedomType.Name = tvPopedom.SelectedNode.Text;
                    popedomOne.Popedoms.ID = tvPopedom.SelectedNode.Index.ToString();
                    popedomOne.PopedomType.ID = tvPopedom.SelectedNode.Index.ToString();
                }
                else
                {
                    popedomOne.PopedomType.Name = tvPopedom.SelectedNode.Parent.Text;
                    popedomOne.PopedomType.ID = tvPopedom.SelectedNode.Parent.Index.ToString();
                    popedomOne.Popedoms.ID = tvPopedom.SelectedNode.Tag.ToString();
                }

                if (this.tvDoctor.SelectedNode.ToolTipText == "医生")
                {
                    for (int i = 0; i < fpPopedom.RowCount; i++)
                    {
                        if ((popedomOne.PopedomType.ID == fpPopedom.Cells[i, 7].Text.Trim()) && (popedomOne.Popedoms.ID == fpPopedom.Cells[i, 6].Text.Trim()))
                        {
                            MessageBox.Show("该权限已经添加");
                            return;
                        }
                    }
                }

                fpPopedom.Rows[fpPopedom.RowCount - 1].Tag = popedomOne;
                //popAdd.Add(popedomOne);

                //填充farPoint控件
                fpPopedom.RowCount = fpPopedom.RowCount + 1;
                fpPopedom.Cells[fpPopedom.RowCount - 1, 0].Text = tvDoctor.SelectedNode.Tag.ToString();
                fpPopedom.Cells[fpPopedom.RowCount - 1, 1].Text = tvDoctor.SelectedNode.Text;
                if (tvPopedom.SelectedNode.Tag == null)
                {
                    fpPopedom.Cells[fpPopedom.RowCount - 1, 2].Text = tvPopedom.SelectedNode.Text;
                    fpPopedom.Cells[fpPopedom.RowCount - 1, 6].Text = tvPopedom.SelectedNode.Index.ToString();
                    fpPopedom.Cells[fpPopedom.RowCount - 1, 7].Text = tvPopedom.SelectedNode.Index.ToString();
                }
                else
                {
                    fpPopedom.Cells[fpPopedom.RowCount - 1, 2].Text = tvPopedom.SelectedNode.Parent.Text;
                    fpPopedom.Cells[fpPopedom.RowCount - 1, 6].Text = tvPopedom.SelectedNode.Tag.ToString();
                    fpPopedom.Cells[fpPopedom.RowCount - 1, 7].Text = tvPopedom.SelectedNode.Parent.Index.ToString();
                }
                fpPopedom.Cells[fpPopedom.RowCount - 1, 3].Text = tvPopedom.SelectedNode.Text;
                fpPopedom.Cells[fpPopedom.RowCount - 1, 4].Text = "否";
                fpPopedom.Cells[fpPopedom.RowCount - 1, 8].Text = "1";
            }
            else if (tvPopedom.SelectedNode.ToolTipText == "RECIPE")
            {
                foreach (TreeNode node in tvPopedom.SelectedNode.Nodes)
                {
                    //填充数组
                    FS.HISFC.Models.Order.Medical.Popedom popedomOne = new FS.HISFC.Models.Order.Medical.Popedom();

                    popedomOne.EmplCode = tvDoctor.SelectedNode.Tag.ToString();
                    popedomOne.EmplName = tvDoctor.SelectedNode.Text;
                    popedomOne.Popedoms.Name = node.Text;
                    if (node.Tag == null)
                    {
                        popedomOne.PopedomType.Name = node.Text;
                        popedomOne.Popedoms.ID = node.Index.ToString();
                        popedomOne.PopedomType.ID = node.Index.ToString();
                    }
                    else
                    {
                        popedomOne.PopedomType.Name = node.Parent.Text;
                        popedomOne.PopedomType.ID = node.Parent.Index.ToString();
                        popedomOne.Popedoms.ID = node.Tag.ToString();
                    }

                    if (tvDoctor.SelectedNode.ToolTipText == "医生")
                    {
                        for (int i = 0; i < fpPopedom.RowCount; i++)
                        {
                            if ((popedomOne.PopedomType.ID == fpPopedom.Cells[i, 7].Text.Trim()) && (popedomOne.Popedoms.ID == fpPopedom.Cells[i, 6].Text.Trim()))
                            {
                                MessageBox.Show("该权限已经添加");
                                return;
                            }
                        }
                    }

                    fpPopedom.Rows[fpPopedom.RowCount - 1].Tag = popedomOne;
                    //popAdd.Add(popedomOne);

                    //填充farPoint控件
                    fpPopedom.RowCount = fpPopedom.RowCount + 1;
                    fpPopedom.Cells[fpPopedom.RowCount - 1, 0].Text = tvDoctor.SelectedNode.Tag.ToString();
                    fpPopedom.Cells[fpPopedom.RowCount - 1, 1].Text = tvDoctor.SelectedNode.Text;
                    if (node.Tag == null)
                    {
                        fpPopedom.Cells[fpPopedom.RowCount - 1, 2].Text = node.Text;
                        fpPopedom.Cells[fpPopedom.RowCount - 1, 6].Text = node.Index.ToString();
                        fpPopedom.Cells[fpPopedom.RowCount - 1, 7].Text = node.Index.ToString();
                    }
                    else
                    {
                        fpPopedom.Cells[fpPopedom.RowCount - 1, 2].Text = node.Parent.Text;
                        fpPopedom.Cells[fpPopedom.RowCount - 1, 6].Text = node.Tag.ToString();
                        fpPopedom.Cells[fpPopedom.RowCount - 1, 7].Text = node.Parent.Index.ToString();
                    }
                    fpPopedom.Cells[fpPopedom.RowCount - 1, 3].Text = node.Text;
                    fpPopedom.Cells[fpPopedom.RowCount - 1, 4].Text = "否";
                    fpPopedom.Cells[fpPopedom.RowCount - 1, 8].Text = "1";
                }
            }
        }

        /// <summary>
        /// 左树选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvDoctor_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Query();
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        private void Query()
        {
            popAdd.Clear();
            List<FS.HISFC.Models.Order.Medical.Popedom> PopedomShow = null;
            if (tvDoctor.SelectedNode.ToolTipText == "医生")
            {
                PopedomShow = abyMgr.QueryPopedomByEmplID(tvDoctor.SelectedNode.Tag.ToString());
                FillFp(PopedomShow);
            }
            else if (tvDoctor.SelectedNode.ToolTipText == "医师职级")
            {
                PopedomShow = new List<FS.HISFC.Models.Order.Medical.Popedom>();
                foreach (TreeNode node in tvDoctor.SelectedNode.Nodes)
                {
                    PopedomShow.AddRange(abyMgr.QueryPopedomByEmplID(node.Tag.ToString()));
                }
                FillFp(PopedomShow);
            }
            else
            {
                this.fpPopedom.RowCount = 0;
            }
        }

        /// <summary>
        /// 实现保存的方法
        /// </summary>
        private void SavePopedom()
        {
            //{EC320C77-250E-4f44-863D-2E47B9F2FA22}
            if (tvDoctor.SelectedNode == null)
            {
                return;
            }

            popAdd.Clear();
            //{EC320C77-250E-4f44-863D-2E47B9F2FA22}
            AddDataOfChildDoctToSaveList(this.tvDoctor.SelectedNode);

            try
            {
                int rev = 0;
                //事务开始
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                foreach (FS.HISFC.Models.Order.Medical.Popedom pdm in popAdd)
                {
                    //先检查是否已经维护过权限了
                    //{EC320C77-250E-4f44-863D-2E47B9F2FA22}
                    rev = abyMgr.CheckByEmplRight(pdm.EmplCode, pdm.PopedomType.ID, pdm.Popedoms.ID);
                    if (rev < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("查找维护信息失败：" + abyMgr.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (rev > 0)
                    {
                        //已存在此权限不做处理了
                        //if (abyMgr.UpdatePopedom(pdm) == -1)
                        //{
                        //    FS.FrameWork.Management.PublicTrans.RollBack();
                        //    MessageBox.Show("保存失败：" + abyMgr.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //return;
                        //}
                    }
                    else
                    {
                        if (abyMgr.InsertPopedom(pdm) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存失败：" + abyMgr.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("保存成功");
                Query();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// 把选择好的权限数据加入到准备保存的数据列表中
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private int AddDataOfChildDoctToSaveList(TreeNode node)
        {
            if (node.ToolTipText == "医生")
            {
                for (int i = 0; i < fpPopedom.RowCount; i++)
                {
                    FS.HISFC.Models.Order.Medical.Popedom spo = new FS.HISFC.Models.Order.Medical.Popedom();
                    spo.EmplCode = node.Tag.ToString();
                    spo.EmplName = node.Text;
                    spo.PopedomType.Name = fpPopedom.Cells[i, 2].Text;
                    spo.Popedoms.Name = fpPopedom.Cells[i, 3].Text;
                    if (fpPopedom.Cells[i, 4].Text == "是")
                    {
                        spo.CheckFlag = "1";
                    }
                    else
                    {
                        spo.CheckFlag = "0";
                    }

                    spo.ID = fpPopedom.Cells[i, 5].Text;
                    spo.Popedoms.ID = fpPopedom.Cells[i, 6].Text;
                    spo.PopedomType.ID = fpPopedom.Cells[i, 7].Text;
                    spo.User03 = fpPopedom.Cells[i, 8].Text;
                    popAdd.Add(spo);
                }
            }
            else
            {
                foreach (TreeNode childNode in node.Nodes)
                {
                    AddDataOfChildDoctToSaveList(childNode);
                }
            }
            return 1;
        }

        /// <summary>
        /// 删除一条权限
        /// </summary>
        private void DeletePopedom()
        {
            if (fpPopedom.Rows.Count > 0)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                for (int i = 0; i < fpPopedom.RowCount; i++)
                {
                    if (fpPopedom.IsSelected(i, 0))
                    {
                        if (abyMgr.DeletePopedom(fpPopedom.Cells[i, 5].Text) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("删除失败:" + abyMgr.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("删除成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.fpPopedom.ClearSelection();
                this.Query();

                //if (abyMgr.DeletePopedom(fpPopedom.Cells[fpPopedom.ActiveRow.Index, 5].Text) == -1)
                //{
                //    MessageBox.Show("删除失败");
                //}
                //else
                //{
                //    fpPopedom.ActiveRow.Remove();
                //    MessageBox.Show("删除成功");
                //}
            }
        }

        /// <summary>
        /// 填充FP控件
        /// </summary>
        /// <param name="addFp"></param>
        private void FillFp(List<FS.HISFC.Models.Order.Medical.Popedom> addFp)
        {
            if (addFp == null)
            {
                this.fpPopedom.RowCount = 0;

                return;
            }
            this.fpPopedom.RowCount = 0;
            for (int rowCount = 0; rowCount < addFp.Count; rowCount++)
            {
                this.fpPopedom.Rows.Add(rowCount, 1);

                //赋值查询结果到FarPoint对应的单元格中
                this.fpPopedom.Cells[rowCount, 0].Text = addFp[rowCount].EmplCode;

                this.fpPopedom.Cells[rowCount, 1].Text = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(addFp[rowCount].EmplCode);

                switch (addFp[rowCount].PopedomType.ID)
                {
                    case "0":
                        this.fpPopedom.Cells[rowCount, 2].Text = "手术权限";
                        this.fpPopedom.Cells[rowCount, 3].Text = operateHelper.GetName(addFp[rowCount].Popedoms.ID);
                        break;
                    case "1":
                        this.fpPopedom.Cells[rowCount, 2].Text = "处方权限";
                        this.fpPopedom.Cells[rowCount, 3].Text = drugTypeHelper.GetName(addFp[rowCount].Popedoms.ID);
                        break;
                    case "2"://{C0C116F2-E66F-41e7-AA16-8C410196C606}
                        this.fpPopedom.Cells[rowCount, 2].Text = "组套权限";
                        this.fpPopedom.Cells[rowCount, 3].Text = this.groupTypeHelper.GetName(addFp[rowCount].Popedoms.ID);
                        break;
                    case "3":
                        this.fpPopedom.Cells[rowCount, 2].Text = "会诊权限";
                        this.fpPopedom.Cells[rowCount, 3].Text = "会诊权限";
                        break;
                    case "4":
                        this.fpPopedom.Cells[rowCount, 2].Text = "大型仪器";
                        this.fpPopedom.Cells[rowCount, 3].Text = "大型仪器";
                        break;
                    case "9":
                        this.fpPopedom.Cells[rowCount, 2].Text = "特殊检查";
                        this.fpPopedom.Cells[rowCount, 3].Text = checkInfoHelper.GetName(addFp[rowCount].Popedoms.ID);
                        break;
                }
                if (addFp[rowCount].CheckFlag == "1")
                {
                    this.fpPopedom.Cells[rowCount, 4].Text = "是";
                }
                else
                {
                    this.fpPopedom.Cells[rowCount, 4].Text = "否";
                }
                this.fpPopedom.Cells[rowCount, 5].Text = addFp[rowCount].ID;
                this.fpPopedom.Cells[rowCount, 6].Text = addFp[rowCount].Popedoms.ID;
                this.fpPopedom.Cells[rowCount, 7].Text = addFp[rowCount].PopedomType.ID;
                this.fpPopedom.Cells[rowCount, 8].Text = "0";
            }
        }

        #endregion

        private void neuContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "item0")
            {
                FS.HISFC.Components.Common.Forms.frmTreeNodeSearch frm = new FS.HISFC.Components.Common.Forms.frmTreeNodeSearch();
                frm.Init(this.tvDoctor);
                frm.ShowDialog();
            }
        }

        #region 于洋的添加的方法

        /// <summary>
        /// 查询特殊检查信息
        /// </summary>
        /// <returns>1：成功，-1：失败</returns>
        private int QueryCheckInfo()
        {
            List<FS.HISFC.Models.Fee.Item.Undrug> lCheckInfo = CacheManager.FeeItemMgr.QueryAllItemList();

            if (lCheckInfo == null)
            {
                MessageBox.Show(Language.Msg("加载特殊检查列表数据发生错误" + CacheManager.FeeItemMgr.Err));
                return -1;
            }

            ArrayList alCheckInfo = new ArrayList();

            //清空数据
            this.dtCheck.Rows.Clear();
            DataRow newRow;

            foreach (FS.HISFC.Models.Fee.Item.Undrug medicalTerm in lCheckInfo)
            {
                newRow = this.dtCheck.NewRow();

                newRow["特殊检查ID"] = medicalTerm.ID;
                newRow["特殊检查"] = medicalTerm.Name;
                newRow["拼音码"] = medicalTerm.SpellCode;
                newRow["五笔码"] = medicalTerm.WBCode;
                newRow["自定义码"] = medicalTerm.UserCode;

                this.dtCheck.Rows.Add(newRow);

                alCheckInfo.Add(medicalTerm);
            }

            //提交DataTable中的变化。

            this.dtCheck.AcceptChanges();

            this.SpreadCheck_Sheet1.Visible = true;

            checkInfoHelper.ArrayObject = alCheckInfo;

            return 1;
        }
        
        /// <summary>
        /// 初始化特殊检查数据集
        /// </summary>
        private void InitCheckInfoDataSet()
        {
            this.SpreadCheck_Sheet1.DataAutoSizeColumns = false;

            //定义类型
            System.Type dtStr = System.Type.GetType("System.String");

            this.dtCheck = new DataTable();

            //在myDataTable中定义列
            this.dtCheck.Columns.AddRange(new DataColumn[] {													
                                                    new DataColumn("特殊检查ID",      dtStr),
                                                    new DataColumn("特殊检查",      dtStr),
                                                    new DataColumn("拼音码",     dtStr),
													new DataColumn("五笔码",     dtStr),
													new DataColumn("自定义码",   dtStr),
											        });

            this.dtCheck.Columns["特殊检查"].ReadOnly = true;

            //隐藏不显示的信息
            this.SpreadCheck_Sheet1.Visible = false;
            this.SpreadCheck_Sheet1.DataSource = this.dtCheck.DefaultView;

            this.SpreadCheck_Sheet1.Columns[0].Visible = false;
            this.SpreadCheck_Sheet1.Columns[2].Visible = false;
            this.SpreadCheck_Sheet1.Columns[3].Visible = false;
            this.SpreadCheck_Sheet1.Columns[4].Visible = false;

            this.SpreadCheck_Sheet1.Columns[1].Width = 160;
        }

        /// <summary>
        /// 通过输入的查询码，过滤数据列表
        /// </summary>
        private void FilterCheck()
        {
            if (this.dtCheck.Rows.Count == 0)
            {
                return;
            }

            try
            {
                string queryCode = "";

                string filterData = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtCheck.Text);

                queryCode = "%" + filterData + "%";

                string filter = "(特殊检查 LIKE '" + queryCode + "') OR " +
                    "(拼音码 LIKE '" + queryCode + "') OR " +
                    "(五笔码 LIKE '" + queryCode + "') OR " +
                    "(自定义码 LIKE '" + queryCode + "') ";

                //设置过滤条件
                this.dtCheck.DefaultView.RowFilter = filter;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg(ex.Message));
            }
        }

        /// <summary>
        /// 特殊检查过滤框输入值变化事件的处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCheck_TextChanged(object sender, EventArgs e)
        {
            //过滤信息
            this.FilterCheck();
        }

        /// <summary>
        /// 特殊检查列表中双击事件的处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpreadCheck_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (CheckDoctTreeValid() == false)
            {
                return;
            }

            //填充数组
            FS.HISFC.Models.Order.Medical.Popedom popedomOne = new FS.HISFC.Models.Order.Medical.Popedom();

            popedomOne.EmplCode = tvDoctor.SelectedNode.Tag.ToString();
            popedomOne.EmplName = tvDoctor.SelectedNode.Text;

            popedomOne.PopedomType.ID = "9";
            popedomOne.PopedomType.Name = "特殊检查";
            popedomOne.Popedoms.ID = this.SpreadCheck_Sheet1.Cells[e.Row, 0].Text;
            popedomOne.Popedoms.Name = this.SpreadCheck_Sheet1.Cells[e.Row, 1].Text;

            if (tvDoctor.SelectedNode.ToolTipText == "医生")
            {
                for (int i = 0; i < fpPopedom.RowCount; i++)
                {
                    if ((popedomOne.PopedomType.ID == fpPopedom.Cells[i, 7].Text.Trim()) && (popedomOne.Popedoms.ID == fpPopedom.Cells[i, 6].Text.Trim()))
                    {
                        MessageBox.Show("该权限已经添加");
                        return;
                    }
                }
            }

            fpPopedom.Rows[fpPopedom.RowCount - 1].Tag = popedomOne;
            //popAdd.Add(popedomOne);

            //填充farPoint控件
            fpPopedom.RowCount = fpPopedom.RowCount + 1;

            fpPopedom.Cells[fpPopedom.RowCount - 1, 0].Text = tvDoctor.SelectedNode.Tag.ToString();
            fpPopedom.Cells[fpPopedom.RowCount - 1, 1].Text = tvDoctor.SelectedNode.Text;
            fpPopedom.Cells[fpPopedom.RowCount - 1, 2].Text = "特殊检查";
            fpPopedom.Cells[fpPopedom.RowCount - 1, 3].Text = this.SpreadCheck_Sheet1.Cells[e.Row, 1].Text;
            fpPopedom.Cells[fpPopedom.RowCount - 1, 4].Text = "否";

            fpPopedom.Cells[fpPopedom.RowCount - 1, 6].Text = this.SpreadCheck_Sheet1.Cells[e.Row, 0].Text;
            fpPopedom.Cells[fpPopedom.RowCount - 1, 7].Text = "9";
            fpPopedom.Cells[fpPopedom.RowCount - 1, 8].Text = "1";

        }

        /// <summary>
        /// 检查医生树控件是否合法
        /// </summary>
        /// <returns></returns>
        protected bool CheckDoctTreeValid()
        {
            if (tvDoctor.Nodes[0].Nodes.Count <= 0)
            {
                MessageBox.Show("没有任何的医生信息！");
                return false;
            }

            //{EC320C77-250E-4f44-863D-2E47B9F2FA22}
            if (tvDoctor.SelectedNode == null)
            {
                MessageBox.Show("没有选择医生或者专业！");
                return false;
            }
            return true;
        }
        #endregion

        private void txtFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.FindNodes(this.txtFind.Text);
            }
        }
    }
}
