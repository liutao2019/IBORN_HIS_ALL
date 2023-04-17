using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.HISFC.Components.Manager
{
    /// <summary>
    /// 附材算法维护
    /// </summary>
    public partial class ucSubtblManager : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucSubtblManager()
        {
            InitializeComponent();
        }

        #region 系统层管理类

        Neusoft.HISFC.BizLogic.Order.Order orderManager = new Neusoft.HISFC.BizLogic.Order.Order();
        Classes.SubtblManager subtblMgr = new Neusoft.HISFC.Components.Manager.Classes.SubtblManager();
        Neusoft.HISFC.BizProcess.Integrate.Manager managerIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Manager();
        #endregion

        #region 变量

        ArrayList al = new ArrayList();

        Neusoft.FrameWork.WinForms.Forms.ToolBarService toolbar = new Neusoft.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 
        /// </summary>
        ArrayList alItem = new ArrayList();

        /// <summary>
        /// 科室列表
        /// </summary>
        ArrayList alDept = new ArrayList();

        /// <summary>
        /// 科室帮助类
        /// </summary>
        Neusoft.FrameWork.Public.ObjectHelper deptHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        ArrayList alItems = new ArrayList();
        Neusoft.FrameWork.Public.ObjectHelper itemHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        Neusoft.HISFC.BizProcess.Integrate.Fee feeMgr = new Neusoft.HISFC.BizProcess.Integrate.Fee();

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化窗体数据
        /// </summary>
        private void Init()
        {
            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载数据请稍后....");
            Application.DoEvents();
            this.initTree();
            this.ucInputItem1.Init();

            alDept = this.managerIntegrate.GetDepartment();
            if (alDept == null)
            {
                MessageBox.Show(managerIntegrate.Err);
            }
            else
            {
                Neusoft.FrameWork.Models.NeuObject allObj = new Neusoft.FrameWork.Models.NeuObject("ROOT", "全部", "");
                alDept.Add(allObj);
                this.deptHelper.ArrayObject = alDept;
            }

            alItems = new ArrayList(feeMgr.QueryAllItemsList());
            if (alItems == null)
            {
                MessageBox.Show(feeMgr.Err);
            }
            else
            {
                this.itemHelper.ArrayObject = alItems;
            }

            #region 下拉列表
            FarPoint.Win.Spread.CellType.ComboBoxCellType comCellType1 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();

            //适用范围：0 门诊；1 住院；2 全部
            string[] arrayTemp = new string[3]{"门诊","住院","全部"
            };
            comCellType1.Items = arrayTemp;
            this.fpSpread1_Sheet1.Columns[0].CellType = comCellType1;

            //组范围：0 每组收取、1 第一组收取、2 第二组起加收
            arrayTemp = new string[3] { "每组收取", "第一组收取", "第二组起加收" };
            FarPoint.Win.Spread.CellType.ComboBoxCellType comCellType2 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            comCellType2.Items = arrayTemp;
            this.fpSpread1_Sheet1.Columns[4].CellType = comCellType2;

            //收费规则：固定数量、*最大院注、*组内品种数、*最大医嘱数量、*频次
            arrayTemp = new string[5] { "×固定数量", "×最大院注", "×组内品种数", "×最大医嘱数量", "×频次" };
            FarPoint.Win.Spread.CellType.ComboBoxCellType comCellType3 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            comCellType3.Items = arrayTemp;
            this.fpSpread1_Sheet1.Columns[5].CellType = comCellType3;

            //限制类别：0 不限制 1 婴儿使用 2 非婴儿使用
            arrayTemp = new string[3] { "不限制", "婴儿", "非婴儿" };
            FarPoint.Win.Spread.CellType.ComboBoxCellType comCellType4 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            comCellType4.Items = arrayTemp;
            this.fpSpread1_Sheet1.Columns[6].CellType = comCellType4;

            #endregion

            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        /// 初始化用法TreeView
        /// </summary>
        private void initTree()
        {
            this.tvPatientList1.Nodes.Clear();
            TreeNode root = new TreeNode("用法");
            root.ImageIndex = 40;
            this.tvPatientList1.Nodes.Add(root);
            //获得用法列表
            if (al != null)
            {
                al = managerIntegrate.GetConstantList(Neusoft.HISFC.Models.Base.EnumConstant.USAGE);
            }

            if (al != null)
            {
                foreach (Neusoft.FrameWork.Models.NeuObject obj in al)
                {
                    TreeNode node = new TreeNode(obj.Name);
                    node.Tag = obj;
                    node.ImageIndex = 41;
                    root.Nodes.Add(node);
                }
                root.Expand();
            }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 初始化ToolBar
        /// </summary>
        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolbar.AddToolButton("删除", "删除数据", Neusoft.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            return this.toolbar;
        }

        private void ucSubtblManager_Load(object sender, EventArgs e)
        {
            try
            {
                this.Init();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveData();
            return 1;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "删除")
            {
                if (this.fpSpread1_Sheet1.Rows.Count <= 0)
                    return;
                int row = this.fpSpread1_Sheet1.ActiveRowIndex;
                if (row < 0)
                    return;
                DialogResult Result = MessageBox.Show("确认删除该数据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (Result == DialogResult.OK)
                {
                    this.fpSpread1_Sheet1.Rows.Remove(row, 1);
                }
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        private void tvPatientList1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode current = this.tvPatientList1.SelectedNode;

            if (current == null || current.Parent == null)
            {
                if (this.fpSpread1_Sheet1.RowCount > 0)
                    this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);

                this.fpSpread1_Sheet1.Tag = null;
            }
            else
            {
                Neusoft.FrameWork.Models.NeuObject usage = current.Tag as Neusoft.FrameWork.Models.NeuObject;

                this.Refresh(usage);
            }
        }

        private void ucInputItem1_SelectedItem(Neusoft.FrameWork.Models.NeuObject sender)
        {
            if (this.ucInputItem1.FeeItem == null)
                return;

            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)//检查是否重复
            {
                Neusoft.FrameWork.Models.NeuObject obj = this.fpSpread1.ActiveSheet.Rows[i].Tag as Neusoft.FrameWork.Models.NeuObject;
                if (obj.Memo == this.ucInputItem1.FeeItem.ID)
                {
                    MessageBox.Show("已存在项目" + this.ucInputItem1.FeeItem.Name + "请重新选择！");
                    return;//如果重复 返回
                }
            }
            this.AddItemToFp(this.ucInputItem1.FeeItem, 0);
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.fpSpread1.ContainsFocus)
            {
                if (this.fpSpread1_Sheet1.ActiveColumnIndex == 1)
                {
                    this.PopItem(this.alDept, 1);
                }
                else if (this.fpSpread1_Sheet1.ActiveColumnIndex == 2)
                {
                    this.PopItem(this.alItems, 2);
                }
            }
            //DialogResult Result = MessageBox.Show("确认删除该数据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            //if (Result == DialogResult.OK)
            //{
            //    this.fpSpread1_Sheet1.Rows.Remove(e.Row, 1);
            //}
        }

        #endregion

        #region 方法

        /// <summary>
        /// 添加项目到farpoint
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="row"></param>
        private void AddItemToFp(Neusoft.FrameWork.Models.NeuObject Item, int row)
        {
            if (this.fpSpread1_Sheet1.Tag == null)
            {
                MessageBox.Show("请先选择用法!", "提示");
                return;
            }

            if (Item.GetType() == typeof(Neusoft.HISFC.Models.Fee.Item.Undrug) ||
                Item.GetType() == typeof(Neusoft.HISFC.Models.Base.Item))
            {
                Neusoft.HISFC.Models.Base.Item myItem = this.feeMgr.GetItem(Item.ID);
                if (myItem == null)
                {
                    MessageBox.Show(feeMgr.Err);
                    return;
                }
                else if (!myItem.IsValid)
                {
                    MessageBox.Show(myItem.Name + "已经停用，请重新选择！");
                    return;
                }

                Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject();
                //判断该项目是否可用
                if (!myItem.IsValid)
                {
                    MessageBox.Show("项目" + myItem.Name + "已经停用！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //在fpSpread1_Sheet1中加入数据
                obj.ID = myItem.ID;
                obj.Name = myItem.Name;
                this.fpSpread1.ActiveSheet.Rows.Add(row, 1);

                this.fpSpread1_Sheet1.Rows[row].Tag = myItem;

                //适用范围：0 门诊；1 住院；3 全部
                this.fpSpread1_Sheet1.Cells[row, 0].Text = "全部";
                //科室代码，全院统一附材'ROOT'
                this.fpSpread1_Sheet1.Cells[row, 1].Text = "全部";
                this.fpSpread1_Sheet1.Cells[row, 1].Tag = "ROOT";
                //附加项目编码
                if (!string.IsNullOrEmpty(myItem.Specs))
                {
                    this.fpSpread1_Sheet1.Cells[row, 2].Text = "【" + myItem.Specs + "】" + myItem.Name + "【" + myItem.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】";
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[row, 2].Text = myItem.Name + "【" + myItem.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】";
                }
                this.fpSpread1_Sheet1.Cells[row, 2].Tag = myItem.ID;
                //数量
                this.fpSpread1_Sheet1.Cells[row, 3].Text = "1";
                //组范围：0 每组收取、1 第一组收取、2 第二组起加收
                this.fpSpread1_Sheet1.Cells[row, 4].Text = "每组收取";
                //收费规则：固定数量、*最大院注、*组内品种数、*最大医嘱数量、*频次
                this.fpSpread1_Sheet1.Cells[row, 5].Text = "×固定数量";
                //限制类别：0 不限制 1 婴儿使用 2 非婴儿使用
                this.fpSpread1_Sheet1.Cells[row, 6].Text = "不限制";
                //操作员
                this.fpSpread1_Sheet1.Cells[row, 7].Text = (this.orderManager.Operator as Neusoft.HISFC.Models.Base.Employee).Name;
                //操作时间
                this.fpSpread1_Sheet1.Cells[row, 8].Text = this.orderManager.GetSysDateTime().ToString();
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.fpSpread1.ContainsFocus)
            {
                if (keyData == Keys.Space)
                {
                    if (this.fpSpread1_Sheet1.ActiveColumnIndex == 1)
                    {
                        this.PopItem(this.alDept, 1);
                    }
                    else if (this.fpSpread1_Sheet1.ActiveColumnIndex == 2)
                    {
                        this.PopItem(this.alItems, 2);
                    }
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// 弹出常数选择
        /// </summary>
        private void PopItem(ArrayList al, int index)
        {
            Neusoft.FrameWork.Models.NeuObject info = new Neusoft.FrameWork.Models.NeuObject();
            if (Neusoft.FrameWork.WinForms.Classes.Function.ChooseItem(al, ref info) == 0)
            {
                return;
            }
            else
            {
                //科室
                if (index == 1)
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 1].Tag = info.ID;
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 1].Value = info.Name;
                }
                //项目
                else if (index == 2)
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 2].Tag = info.ID;
                    Neusoft.HISFC.Models.Base.Item itemObj = info as Neusoft.HISFC.Models.Base.Item;

                    //附加项目编码
                    if (!string.IsNullOrEmpty(itemObj.Specs))
                    {
                        this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 2].Value = "【" + itemObj.Specs + "】" + itemObj.Name + "【" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】";
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 2].Value = itemObj.Name + "【" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】";
                    }
                    this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.ActiveRowIndex].Tag = info;
                }
            }
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <param name="person"></param>
        private void Refresh(Neusoft.FrameWork.Models.NeuObject usage)
        {
            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);
            }

            try
            {
                this.tabPage1.Text = usage.Name;
                Neusoft.HISFC.Models.Base.Item itemObj = new Neusoft.HISFC.Models.Base.Item();

                alItem.Clear();
                alItem = this.subtblMgr.GetSubtblInfo("3", usage.ID, "ROOT");
                if (alItem == null)
                {
                    MessageBox.Show(this.subtblMgr.Err);
                    return;
                }

                this.fpSpread1_Sheet1.Tag = usage;
                if (alItem != null && alItem.Count > 0)
                {
                    foreach (Classes.OrderSubtbl obj in alItem)
                    {
                        itemObj = this.itemHelper.GetObjectFromID(obj.Item.ID) as Neusoft.HISFC.Models.Base.Item;
                        if (itemObj == null)
                        {
                            MessageBox.Show("查找项目失败：" + obj.Item.Name + obj.Item.ID);
                            break;
                        }

                        this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);
                        int row = this.fpSpread1_Sheet1.RowCount - 1;
                        this.fpSpread1_Sheet1.Rows[row].Tag = obj.Item;

                        //范围
                        this.fpSpread1_Sheet1.Cells[row, 0].Text = (this.fpSpread1_Sheet1.Columns[0].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[Neusoft.FrameWork.Function.NConvert.ToInt32(obj.Area)];
                        //科室
                        this.fpSpread1_Sheet1.Cells[row, 1].Text = this.deptHelper.GetName(obj.Dept_code);
                        this.fpSpread1_Sheet1.Cells[row, 1].Tag = obj.Dept_code;
                        //项目
                        //this.fpSpread1_Sheet1.Cells[row, 2].Text = itemObj.Name + "【" + itemObj.Specs + "】";
                        if (!string.IsNullOrEmpty(itemObj.Specs))
                        {
                            this.fpSpread1_Sheet1.Cells[row, 2].Text = "【" + itemObj.Specs + "】" + itemObj.Name + "【" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】";
                        }
                        else
                        {
                            this.fpSpread1_Sheet1.Cells[row, 2].Text = itemObj.Name + "【" + itemObj.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】";
                        }

                        this.fpSpread1_Sheet1.Cells[row, 2].Tag = obj.Item.ID;
                        //数量
                        this.fpSpread1_Sheet1.Cells[row, 3].Text = obj.Qty.ToString();

                        //规则：组范围
                        this.fpSpread1_Sheet1.Cells[row, 4].Text = (this.fpSpread1_Sheet1.Columns[4].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[Neusoft.FrameWork.Function.NConvert.ToInt32(obj.CombArea)];
                        //规则：收取规则
                        this.fpSpread1_Sheet1.Cells[row, 5].Text = (this.fpSpread1_Sheet1.Columns[5].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[Neusoft.FrameWork.Function.NConvert.ToInt32(obj.FeeRule)];
                        //限制类别：0 不限制 1 婴儿使用 2 非婴儿使用
                        this.fpSpread1_Sheet1.Cells[row, 6].Text = (this.fpSpread1_Sheet1.Columns[6].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[Neusoft.FrameWork.Function.NConvert.ToInt32(obj.LimitType)];
                        //操作员
                        this.fpSpread1_Sheet1.SetValue(row, 7, managerIntegrate.GetEmployeeInfo(obj.Oper.ID).Name, false);
                        //操作时间
                        this.fpSpread1_Sheet1.SetValue(row, 8, obj.OperDate);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + this.subtblMgr.Err);
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
            #region 验证数据放在选择项目中
            //验证数据
            //if (!this.Valid())
            //{
            //    this.fpSpread1.Focus();
            //    return;
            //}
            #endregion

            this.fpSpread1.StopCellEditing();

            if (this.fpSpread1_Sheet1.Tag == null)
            {
                MessageBox.Show("请先选择项目!", "提示");
                return;
            }
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            //开始事务
            try
            {
                this.subtblMgr.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

                //先全部删除
                if (alItem.Count > 0)
                {
                    if (this.subtblMgr.DelSubtblInfo((this.fpSpread1_Sheet1.Tag as Neusoft.FrameWork.Models.NeuObject).ID) == -1)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.subtblMgr.Err, "提示");
                        return;
                    }
                }

                Classes.OrderSubtbl objSubtbl = null;
                //在全部循环插入
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    objSubtbl = new Classes.OrderSubtbl();

                    //适用范围：0 门诊；1 住院；3 全部
                    objSubtbl.Area = this.GetSelectData(i, 0);
                    //用法分类，0 药品按用法，1 非药品按项目代码
                    objSubtbl.TypeCode = ((Neusoft.FrameWork.Models.NeuObject)this.fpSpread1_Sheet1.Tag).ID;
                    //科室代码，全院统一附材'ROOT'
                    objSubtbl.Dept_code = this.fpSpread1_Sheet1.Cells[i, 1].Tag.ToString();
                    //项目编码
                    objSubtbl.Item.ID = (this.fpSpread1_Sheet1.Rows[i].Tag as Neusoft.FrameWork.Models.NeuObject).ID;
                    //数量
                    objSubtbl.Qty = Neusoft.FrameWork.Function.NConvert.ToInt32(this.fpSpread1_Sheet1.Cells[i, 3].Text);
                    //组范围：0 每组收取、1 第一组收取、2 第二组起加收
                    objSubtbl.CombArea = this.GetSelectData(i, 4);
                    //收费规则：固定数量、*最大院注、*组内品种数、*最大医嘱数量、*频次
                    objSubtbl.FeeRule = this.GetSelectData(i, 5);
                    //限制类别：0 不限制 1 婴儿使用 2 非婴儿使用
                    objSubtbl.LimitType = this.GetSelectData(i, 6);
                    //操作员					
                    objSubtbl.Oper.ID = (this.orderManager.Operator as Neusoft.HISFC.Models.Base.Employee).ID;
                    //操作时间
                    objSubtbl.OperDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.Cells[i, 8].Text);

                    if (this.subtblMgr.InsertSubtblInfo(objSubtbl) == -1)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.subtblMgr.Err, "提示");
                        return;
                    }
                }
                Neusoft.FrameWork.Management.PublicTrans.Commit();

                TreeNode current = this.tvPatientList1.SelectedNode;
                Neusoft.FrameWork.Models.NeuObject usage = current.Tag as Neusoft.FrameWork.Models.NeuObject;
                alItem.Clear();
                alItem = this.subtblMgr.GetSubtblInfo("3", usage.ID, "ROOT");
            }
            catch (Exception e)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "提示");
                return;
            }
            MessageBox.Show("保存成功!", "提示");
        }

        /// <summary>
        /// 验证数据看是否有重复的数据()
        /// </summary>
        /// <param name="ItemID">项目ID</param>
        /// <returns></returns>
        private bool Valid(string Item_ID)
        {
            try
            {
                if (this.fpSpread1_Sheet1.Rows.Count > 0)
                {
                    this.fpSpread1.StopCellEditing();
                    for (int i = 0; i < this.fpSpread1_Sheet1.Rows.Count; i++)
                    {
                        string ItemID = this.fpSpread1_Sheet1.Cells[i, 0].Tag.ToString();

                        if (ItemID == Item_ID)
                        {
                            this.fpSpread1_Sheet1.Rows[i].BackColor = Color.Red;
                            MessageBox.Show("该项目已存在，项目不能重复！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.fpSpread1_Sheet1.Rows[i].BackColor = Color.White;
                            return false;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取现在的ID
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private string GetSelectData(int row, int column)
        {
            for (int j = 0; j < (this.fpSpread1_Sheet1.Columns[column].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items.Length; j++)
            {
                string item = (this.fpSpread1_Sheet1.Columns[column].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[j];

                if (item == this.fpSpread1_Sheet1.Cells[row, column].Text)
                {
                    return j.ToString();
                }
            }
            return "0";
        }

        #endregion
    }
}
