using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Registration
{
    /// <summary>
    /// 挂号费维护
    /// </summary>
    public partial class ucRegFee : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucRegFee()
        {
            InitializeComponent();

            this.Load += new EventHandler(ucRegFee_Load);
            this.treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);
            this.treeView1.BeforeSelect += new TreeViewCancelEventHandler(treeView1_BeforeSelect);
        }
        #region 属性
        private bool isShowPubDiagFee = false;
        /// <summary>
        /// 是否默认显示记账诊金列
        /// </summary>
        [Category("控件设置"), Description("是否默认显示记账诊金列")]
        public bool IsShowPubDiagFee
        {
            set
            {
                this.isShowPubDiagFee = value;
            }
            get
            {
                return this.isShowPubDiagFee;
            }
        }
        #endregion

        #region 定义域
        /// <summary>
        /// 挂号费管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.RegLvlFee regFeeMgr = new FS.HISFC.BizLogic.Registration.RegLvlFee();
        private FS.HISFC.BizLogic.Registration.RegLevel regMgr = new FS.HISFC.BizLogic.Registration.RegLevel();

        /// <summary>
        /// 参数控制类{A53D57D8-E44D-4517-8B24-E13D686D6F1B}
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();

        private ArrayList al;
        private ArrayList alLevel;
        private DataTable dtRegFee = new DataTable();
        private string levelName;
        #endregion

        #region 初始化
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucRegFee_Load(object sender, EventArgs e)
        {
            this.InitTree();
            this.InitRegLevl();
            this.InitDataTable();
        }
        /// <summary>
        /// 生成挂号员列表
        /// </summary>
        private void InitTree()
        {
            this.treeView1.Nodes.Clear();

            TreeNode root = new TreeNode("合同单位", 22, 22);
            this.treeView1.Nodes.Add(root);

            FS.HISFC.BizProcess.Integrate.Manager pactMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            //获得合同单位列表
            this.al = pactMgr.QueryPactUnitOutPatient();
            if (al == null)
            {
                MessageBox.Show("获取合同单位信息时出错!" + pactMgr.Err, "提示");
                return;
            }

            foreach (FS.HISFC.Models.Base.PactInfo obj in al)
            {
                TreeNode node = new TreeNode(obj.Name, 11, 35);
                node.Tag = obj.ID;
                root.Nodes.Add(node);
            }
            root.Expand();
        }
        /// <summary>
        /// 获取挂号级别
        /// </summary>
        private void InitRegLevl()
        {
            FS.HISFC.BizLogic.Registration.RegLevel regMgr = new FS.HISFC.BizLogic.Registration.RegLevel();

            alLevel = regMgr.Query(true);
            if (alLevel == null)
            {
                MessageBox.Show("获取挂号级别时出错!" + regMgr.Err, "提示");
                return;
            }
        }
        /// <summary>
        /// 初始化数据窗格式
        /// </summary>
        private void InitDataTable()
        {
            this.dtRegFee.Columns.AddRange(new DataColumn[]{
															   new DataColumn("挂号级别",System.Type.GetType("System.String")),
															   new DataColumn("挂号费",System.Type.GetType("System.Decimal")),
															   new DataColumn("检查费",Type.GetType("System.Decimal")),
															   new DataColumn("自费诊疗费",Type.GetType("System.Decimal")),
															   new DataColumn("记帐诊疗费",Type.GetType("System.Decimal")),
															   new DataColumn("附加费",Type.GetType("System.Decimal")),
															   new DataColumn("流水号",Type.GetType("System.String")),
															   new DataColumn("级别代码",Type.GetType("System.String"))
														   });
        }
        #endregion

        #region 方法

        #region 验证是否需要插入
        private void MakeAll(string pactCode)
        {
            al = regFeeMgr.Query(pactCode,true);
            //			if(al.Count != alLevel.Count)
            //			{
            bool IsFound = false;
            foreach (FS.HISFC.Models.Registration.RegLevel level in alLevel)
            {
                IsFound = false;

                foreach (FS.HISFC.Models.Registration.RegLvlFee obj in al)
                {
                    if (level.ID == obj.RegLevel.ID)
                    {
                        IsFound = true;
                        break;
                    }
                }

                if (!IsFound)
                {
                    //级别代码中有，但是该合同单位没有维护。
                    FS.HISFC.Models.Registration.RegLvlFee regFee = this.Insert(pactCode, level.ID);
                    //直接添加到窗口中，防止再次检索数据库。
                    al.Add(regFee);
                }
            }
            //			}
        }
        #endregion

        #region 查询
        /// <summary>
        /// 按合同单位查询挂号级别
        /// </summary>
        private void Query(string PactID)
        {
            //			al = this.regFeeMgr.Query(PactID ) ;
            if (al == null)
            {
                MessageBox.Show("查询挂号费信息时出错!" + this.regFeeMgr.Err, "提示");
                return;
            }

            this.dtRegFee.Rows.Clear();

            foreach (FS.HISFC.Models.Registration.RegLvlFee info in al)
            {
                this.getNamebyId(info.RegLevel.ID);
                if (levelName == "" || levelName == null)
                {
                    MessageBox.Show("获取级别名称出错！");
                    return;
                }
                this.dtRegFee.Rows.Add(new object[]{
													   levelName,
													   info.RegFee,
													   info.ChkFee,
													   info.OwnDigFee,
													   info.PubDigFee,
													   info.OthFee,
													   info.ID,
													   info.RegLevel.ID
												   });
            }
            this.dtRegFee.AcceptChanges();
            this.fpSpread1_Sheet1.DataSource = this.dtRegFee;
          

            this.SetFpFormat();
        }
        #endregion
        /// <summary>
        /// 查询按钮用：其实没啥意义
        /// </summary>
        private void Query()
        {
            int i = 0;
            string pactCode = string.Empty ;
            TreeNode root = this.treeView1.Nodes[0];
            foreach(TreeNode node in root.Nodes )
            {
                if (node.Checked)
                {
                    i++;
                    pactCode = node.Tag.ToString();
                }
            }
            if (i > 1)
            {
                MessageBox.Show("请选择一条记录进行查询操作");
                pactCode = string.Empty;
                return;
            }
            if (!(pactCode == "" && pactCode ==string.Empty))
            {
                this.MakeAll(pactCode);
                this.Query(pactCode);
            }

            
        }
        #region 插入
        /// <summary>
        /// 插入一条新的记录
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="levelCode"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.RegLvlFee Insert(string pactCode, string levelCode)
        {
            FS.HISFC.Models.Registration.RegLvlFee info = new FS.HISFC.Models.Registration.RegLvlFee();

            info.ID = regFeeMgr.GetSequence("Registration.RegLevel.GetSeqNo");
            info.Pact.ID = pactCode;
            info.RegLevel.ID = levelCode;            
            info.RegFee = 0;
            info.ChkFee = 0;
            info.OwnDigFee = 0;
            info.PubDigFee = 0;
            info.OthFee = 0;
            info.Oper.ID = this.regFeeMgr.Operator.ID;
            info.Oper.OperTime = regFeeMgr.GetDateTimeFromSysDateTime();

            if (regFeeMgr.Insert(info) == -1)
            {
                MessageBox.Show("添加合同单位挂号费分配信息失败！[Registration.RegFee.Insert.1]" + regFeeMgr.Err);
            }

            return info;
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        private int Valid()
        {
            ///判断金额不能为空
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                for (int j = 0; j < this.fpSpread1_Sheet1.Columns.Count; j++)
                {
                    if (this.fpSpread1_Sheet1.Cells[i, j].Text == string.Empty)
                    {
                        MessageBox.Show("第" + (i + 1).ToString() + "行金额不能为空");
                        this.fpSpread1_Sheet1.Cells[i, j].Text = "0.00";
                        return -1;
                    }
            
                }
            }

            return 1;
        }

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            this.fpSpread1.StopCellEditing();

            if (this.Valid() == -1)
            {
                return;
            }
            if (this.treeView1.SelectedNode == null || this.treeView1.SelectedNode.Parent == null)
            {
                MessageBox.Show("无合同单位!", "提示");
                return;
            }

            string pactNames = "";
            foreach (TreeNode node in this.treeView1.Nodes[0].Nodes)
            {
                if (node.Checked)
                {
                    pactNames = pactNames + node.Text + "\n";
                }
            }
            if (pactNames.EndsWith("\n"))
            {
                pactNames.TrimEnd('n').TrimEnd('\\');
            }

            if (MessageBox.Show("是否保存以下合同单位维护？\n"+pactNames, "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }


            //当前合同单位代码
            string pactID = this.treeView1.SelectedNode.Tag.ToString();

            if(fpSpread1_Sheet1.RowCount>0)
                dtRegFee.Rows[fpSpread1_Sheet1.ActiveRowIndex].EndEdit();

            //修改            
            DataTable dtModify = dtRegFee.GetChanges(DataRowState.Modified);
            ArrayList alModify = this.GetChanges(dtModify);

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存,请稍后...");
            Application.DoEvents();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(regFeeMgr.con);
            //SQLCA.BeginTransaction();

            this.regFeeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            try
            {
                foreach (FS.HISFC.Models.Registration.RegLvlFee info in alModify)
                {
                    if (regFeeMgr.Update(info) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("更新挂号费分配方案出错！" + regFeeMgr.Err, "提示");
                        return;
                    }
                }

                /// 保存其他选中的节点
                /// 
                foreach (TreeNode pact in this.treeView1.Nodes[0].Nodes)
                {
                    if (pact.Checked && pact != this.treeView1.SelectedNode)
                    {
                        //删除原来的挂号费

                        if (this.regFeeMgr.DeleteByPact(pact.Tag.ToString()) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("删除挂号费出错！" + regFeeMgr.Err, "提示");
                            return;
                        }

                        //Copy 挂号费
                        if (this.regFeeMgr.CopyByPact(pact.Tag.ToString(), pactID) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("复制挂号费出错！" + regFeeMgr.Err, "提示");
                            return;
                        }
                    }
                    //取消选择
                    if (pact.Checked)
                    {
                        pact.Checked = false;
                    }
                }
                this.treeView1.Nodes[0].Checked = false;
               
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(e.Message, "提示");
                return;
            }
            //嵌入对其他系统或其他业务模块的信息传送桥接处理
            string errInfo = "";
            //ArrayList alInfo = new ArrayList();
            //alInfo.Add(regLevel);
            int param = Function.SendBizMessage(alModify, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Modify, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.RegLevel, ref errInfo);

            if (param == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                Function.ShowMessage("挂号级别维护失败，请向系统管理员报告错误信息：" + errInfo, MessageBoxIcon.Error);
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            MessageBox.Show("保存成功！");
            dtRegFee.AcceptChanges();
        }
        #endregion

        #region 公有设置
        /// <summary>
        /// 获取改变的信息，转化为实体
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private ArrayList GetChanges(DataTable dt)
        {
            this.al = new ArrayList();
            if (dt != null)
            {
                try
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        FS.HISFC.Models.Registration.RegLvlFee info = new FS.HISFC.Models.Registration.RegLvlFee();
                        info.ID = row["流水号"].ToString();
                        info.RegFee = FS.FrameWork.Function.NConvert.ToDecimal(row["挂号费"].ToString());
                        info.ChkFee = FS.FrameWork.Function.NConvert.ToDecimal(row["检查费"].ToString());
                        info.OwnDigFee = FS.FrameWork.Function.NConvert.ToDecimal(row["自费诊疗费"].ToString());
                        info.PubDigFee = FS.FrameWork.Function.NConvert.ToDecimal(row["记帐诊疗费"].ToString());
                        info.OthFee = FS.FrameWork.Function.NConvert.ToDecimal(row["附加费"].ToString());
                        info.RegLevel.ID = row["级别代码"].ToString();
                        info.RegLevel.Name = row["挂号级别"].ToString();
                        this.al.Add(info);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("生成实体集合时出错!" + e.Message, "提示");
                    return null;
                }
            }
            return al;

        }
        /// <summary>
        /// 设定显示格式
        /// </summary>
        private void SetFpFormat()
        {
            FarPoint.Win.Spread.CellType.NumberCellType numbCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numbCellType.MaximumValue = 2499.99;
            numbCellType.MinimumValue = 0;

            FarPoint.Win.Spread.CellType.TextCellType txtCellType = new FarPoint.Win.Spread.CellType.TextCellType();
            txtCellType.ReadOnly = true;

            this.fpSpread1_Sheet1.Columns[0].CellType = txtCellType;
            this.fpSpread1_Sheet1.Columns[0].Locked = true;
            this.fpSpread1_Sheet1.Columns[1].CellType = numbCellType;
            this.fpSpread1_Sheet1.Columns[2].CellType = numbCellType;
            this.fpSpread1_Sheet1.Columns[3].CellType = numbCellType;
            this.fpSpread1_Sheet1.Columns[4].CellType = numbCellType;
            this.fpSpread1_Sheet1.Columns[5].CellType = numbCellType;
            //{989D0388-3A0A-4664-A805-B797322BFAB6}
            this.fpSpread1_Sheet1.Columns[4].Visible = this.isShowPubDiagFee;
            this.fpSpread1_Sheet1.Columns[6].Visible = false;
            this.fpSpread1_Sheet1.Columns[7].Visible = false;

            //{A53D57D8-E44D-4517-8B24-E13D686D6F1B}
            #region 其他费处理
            ///其它费类型0：空调费1病历本费2：其他费
            string rtn = this.ctlMgr.QueryControlerInfo("400027");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";

            switch (rtn)
            {
                case "0":
                    {
                        //广州用
                        this.fpSpread1_Sheet1.Columns[5].Label = "床费";
                        break;
                    }
                case "1": //病历本费
                    {
                        this.fpSpread1_Sheet1.Columns[5].Label = "病历本费";
                        break;
                    }
                case "2": //其他费
                    {
                        
                        this.fpSpread1_Sheet1.Columns[5].Label = "其他费";
                        break;
 
                    }
                default:
                    break;
            }
            #endregion
            
       
        }

        private void getNamebyId(string levelCode)
        {
            foreach (FS.HISFC.Models.Registration.RegLevel level in alLevel)
            {
                if (levelCode == level.ID)
                {
                    levelName = level.Name.ToString();
                    break;
                }
            }
        }
        #endregion

        #endregion

        #region 事件
        
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent != null)//不是父节点
            {
                this.MakeAll(e.Node.Tag.ToString());
                this.Query(e.Node.Tag.ToString());
            }
            else
            {
                this.al = new ArrayList();
                this.Query("NULL");
            }
        }

        /// <summary>
        /// 是否修改数据？
        /// </summary>
        /// <returns></returns>
        public bool IsChange()
        {
            this.fpSpread1.StopCellEditing();

            DataTable dt = dtRegFee.GetChanges();

            if (dt == null || dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                if (MessageBox.Show("数据已经修改,是否保存变动?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    this.Save();
                    return true;
                }
            }
            return true;
        }


        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            this.IsChange();
        }

        protected override int OnSave(object sender, object neuObject)
        {
            Save();

            return base.OnSave(sender, neuObject);
        }
        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return base.OnQuery(sender, neuObject);
        }
        #endregion

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            bool selectedFlag = e.Node.Checked;

            if (e.Node.Parent == null)
            {
                foreach (TreeNode node in e.Node.Nodes)
                {
                    node.Checked = selectedFlag;
                }
            }
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
            TreeNode node = this.treeView1.SelectedNode;
            if (node.Parent == null)
                node.ExpandAll();           
        }

        private void treeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            e.Node.ExpandAll();
        }
    }
}
