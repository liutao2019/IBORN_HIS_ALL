using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Function;
using FS.FrameWork.Management;
namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// 创建者：冯超
    /// 创建时间：2010-11-1
    /// 新手术安排，动态设置检索列
    /// </summary>
    public partial class ucArrangmentQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucArrangmentQuery()
        {
            InitializeComponent();
        }
        #region 变量

        /// <summary>
        /// 管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager conManagerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 手术安排信息数据集
        /// </summary>
        DataSet ds = null;
        /// <summary>
        /// 手术安排信息数据视图
        /// </summary>
        DataView dv = null;
        /// <summary>
        /// 树节点数组
        /// </summary>
        ArrayList treeList = new ArrayList();
        #endregion

        #region 属性
        #endregion

        #region 方法
        /// <summary>
        /// 树加载
        /// </summary>
        /// <returns>sucessfull:1，fail:-1</returns>
        private int InitTreeView()
        {
            this.tvQueryList.Nodes.Clear();
            TreeNode root = new TreeNode("默认全部");
            root.SelectedImageIndex = 22;
            root.ImageIndex = 22;
            root.Tag = "ALL";
            this.tvQueryList.Nodes.Add(root);
            return 1;
        }
        /// <summary>
        /// 根据列表刷新树
        /// </summary>
        /// <returns>sucessfull:1，fail:-1</returns>
        private int RefreshTreeView()
        {

            this.tvQueryList.Nodes.Clear();
            TreeNode root = new TreeNode(this.cmbQuery.Text);
            root.SelectedImageIndex = 22;
            root.ImageIndex = 22;
            root.Tag = this.cmbQuery.Tag;
            this.tvQueryList.Nodes.Add(root);
            if (this.treeList.Count > 0)
            {
                foreach (string txt in this.treeList)
                {
                    this.tvQueryList.Nodes[0].Nodes.Add(txt);
                    this.tvQueryList.Nodes[0].ExpandAll();
                }
            }

            return 1;
        }
        /// <summary>
        /// 得到列索引
        /// </summary>
        /// <returns></returns>
        private int GetIndex(string tag)
        {
            if (this.neuSpread1_Sheet1.Columns.Count > 0)
            {
                for (int j = 0; j < this.neuSpread1_Sheet1.Columns.Count; j++)
                {
                    if (this.neuSpread1_Sheet1.Columns[j].Label == tag)
                    {
                        return j;
                    }
                }
            }

            return -1;
        }
        /// <summary>
        /// 初始化手术安排信息
        /// </summary>
        /// <returns>sucessfull:1，fail:-1</returns>
        private int InitFpData()
        {
            DateTime beginTime = this.dtBegin.Value;
            DateTime endTime = this.dtEnd.Value;
            this.ds = new DataSet();
            this.dv = new DataView();
            ds = Environment.OperationManager.GetOpsAppList(Environment.OperatorDeptID, beginTime, endTime, ref ds); ;
            if (ds == null)
            {
                MessageBox.Show(Language.Msg("初始化手术信息出错" + Environment.OperationManager.Err));
                return -1;
            }
            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount , 1);
                int rows = this.neuSpread1_Sheet1.RowCount - 1;
                this.neuSpread1_Sheet1.SetValue(rows,0,dr[0].ToString(),false);
                this.neuSpread1_Sheet1.SetValue(rows, 1, dr[1].ToString(), false);
                this.neuSpread1_Sheet1.SetValue(rows, 2, dr[2].ToString(), false);
                this.neuSpread1_Sheet1.SetValue(rows, 3, dr[3].ToString(), false);
                this.neuSpread1_Sheet1.SetValue(rows, 4, dr[4].ToString(), false);
                this.neuSpread1_Sheet1.SetValue(rows, 5, dr[5].ToString(), false);
                this.neuSpread1_Sheet1.SetValue(rows, 6, dr[6].ToString(), false);
                this.neuSpread1_Sheet1.SetValue(rows, 7, dr[7].ToString(), false);
                this.neuSpread1_Sheet1.SetValue(rows, 8, dr[8].ToString(), false);
                this.neuSpread1_Sheet1.SetValue(rows, 9, dr[9].ToString(), false);
                this.neuSpread1_Sheet1.SetValue(rows, 10, dr[10].ToString(), false);
                this.neuSpread1_Sheet1.SetValue(rows, 11, dr[11].ToString(), false);
                this.neuSpread1_Sheet1.SetValue(rows, 12, dr[12].ToString(), false);
                this.neuSpread1_Sheet1.SetValue(rows, 13, dr[13].ToString(), false);
                this.neuSpread1_Sheet1.SetValue(rows, 14, dr[14].ToString(), false);
                this.neuSpread1_Sheet1.SetValue(rows, 15, dr[15].ToString(), false);
                this.neuSpread1_Sheet1.SetValue(rows, 16, dr[16].ToString(), false);
                this.neuSpread1_Sheet1.SetValue(rows, 17, dr[17].ToString(), false);
            }
            //dv = ds.Tables[0].DefaultView;
            //this.neuSpread1_Sheet1.DataSource = dv;
            //根据状态设置界面显示
            SetFormatFp();
            return 1;
        }
        /// <summary>
        /// 根据状态设置界面显示
        /// </summary>
        private void SetFormatFp()
        {
            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    //手术安排
                    if (this.neuSpread1_Sheet1.Cells[i, 18].Text == "安排")
                    {
                        this.neuSpread1_Sheet1.RowHeader.Cells[i, 0].Text = "安排";
                        this.neuSpread1_Sheet1.Rows[i].BackColor = Color.White;
                    }
                    //手术完成登记
                    if (this.neuSpread1_Sheet1.Cells[i, 18].Text == "完成")
                    {
                        this.neuSpread1_Sheet1.RowHeader.Cells[i, 0].Text = "完成";
                        this.neuSpread1_Sheet1.Rows[i].BackColor = Color.LawnGreen;
                    }
                    //手术取消
                    if (this.neuSpread1_Sheet1.Cells[i, 18].Text == "取消")
                    {
                        this.neuSpread1_Sheet1.RowHeader.Cells[i, 0].Text = "取消";
                        this.neuSpread1_Sheet1.Rows[i].BackColor = Color.Red;
                    }
                }
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            //this.dtBegin.Value = Environment.OperationManager.GetDateTimeFromSysDateTime();
            //this.dtEnd.Value = Environment.OperationManager.GetDateTimeFromSysDateTime();
            this.neuSpread1_Sheet1.RowCount = 0;
            this.dtBegin.Value = this.dtEnd.Value.Date;
            this.dtEnd.Value =   this.dtEnd.Value.Date.AddDays(1);
            //if (this.InitFpData() == -1)
            //{
            //    return;
            //}
            if (this.InitTreeView() == -1)
            {
                return;
            }
            //根据常数初始化选择条件
            ArrayList list = new ArrayList();
            list = this.conManagerIntegrate.GetConstantList("QueryColumnSet");
            if (list == null)
            {
                MessageBox.Show(Language.Msg("根据常数获取检索条件出错!" ));
                return;
            }
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "ALL";
            obj.Name = "默认全部";
            list.Insert(0,obj);
            this.cmbQuery.AddItems(list);
            this.cmbQuery.SelectedIndex = 0;
            base.OnLoad(e);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.neuSpread1_Sheet1.RowCount = 0;
            if (this.InitFpData() == -1)
            {
                return -1;
            }
            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            PrintInfo();

            return base.OnPrint( sender,  neuObject);
        }

        #region 打印方法
        /// <summary>
        /// 打印方法
        /// </summary>
        private void PrintInfo()
        {
            FS.FrameWork.WinForms.Classes.Print pr = new FS.FrameWork.WinForms.Classes.Print();
            pr.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            pr.PrintPreview(30,30,this.neuPanel3);
        }
        #endregion
        /// <summary>
        ///切换下拉框列表时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbQuery_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.treeList = new ArrayList();
            this.InitFpData();
            if (this.cmbQuery.Tag == null || this.cmbQuery.Text == "" || this.cmbQuery.Tag.ToString() == "ALL")
            {
                this.InitTreeView();               
            }
            int j=this.GetIndex(this.cmbQuery.Text);
            if (j == -1)
            {
                return;
            }
            //根据查询列向树节点数组赋值
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                string str=this.neuSpread1_Sheet1.Cells[i,j].Text;
                //只ADD不同的数据形成节点
                if (this.treeList.Contains(str) || str=="")
                {
                    continue;
                }
                this.treeList.Add(str);
            }
            this.RefreshTreeView();
        }
       
        /// <summary>
        /// 选择树节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvQueryList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text == this.cmbQuery.Text)
                return;
            string filter = "";
            filter = this.cmbQuery.Text.Trim() + "= '" + e.Node.Text+"'";
            this.dv.RowFilter = filter;
            SetFormatFp();
        }
        #endregion


    }
}