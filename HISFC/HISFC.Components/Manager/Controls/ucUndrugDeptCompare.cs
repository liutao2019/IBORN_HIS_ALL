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

namespace FS.HISFC.Components.Manager.Controls
{
    /// <summary>
    /// 非药品多科室维护
    /// </summary>
    public partial class ucUndrugDeptCompare : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucUndrugDeptCompare()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 非药品业务类 
        /// </summary>
        FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// 科室业务类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();

        /// <summary>
        /// 非药品集合
        /// </summary>
        ArrayList itemList = null;

        /// <summary>
        /// 科室集合
        /// </summary>
        ArrayList deptList = null;

        /// <summary>
        /// 适用范围集合
        /// </summary>
        ArrayList areaList = new ArrayList();

        /// <summary>
        /// 数据集合
        /// </summary>
        DataTable dt = new DataTable();

        /// <summary>
        /// 
        /// </summary>
        DataView dtview = new DataView();

        /// <summary>
        /// 只用于更新校验
        /// </summary>
        Dictionary<string, string> dictUpdate = new Dictionary<string, string>();

        /// <summary>
        /// 只用于插入校验
        /// </summary>
        Dictionary<string, string> dictInsert = new Dictionary<string, string>();

        /// <summary>
        /// 用于更新和插入校验
        /// </summary>
        Dictionary<string, string> dictALL = new Dictionary<string, string>();

        DateTime date = new DateTime();

        string text = string.Empty;

        #endregion

        #region 属性

        

        #endregion

        #region 工具栏

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        public override FS.FrameWork.WinForms.Forms.ToolBarService Init(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("删除", "删除", FS.FrameWork.WinForms.Classes.EnumImageList.M模版删除, true, false, null);
            return toolBarService;
        }

        #endregion

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "删除")
            {
                DeleteRow();
            }
        }

        public void AddNewRow()
        {
            this.neuFpEnter1_Sheet1.Rows.Add(this.neuFpEnter1_Sheet1.Rows.Count, 1);
            if (this.neuFpEnter1_Sheet1.Rows.Count != 1)
            {
                //this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 0].Text = this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 2, 0].Text;
                //this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 4].Text = this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 2, 4].Text;
                //this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 5].Text = this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 2, 5].Text;
                this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 2].Text = "全部";
                //this.neuFpEnter1_Sheet1.SetActiveCell(this.neuFpEnter1_Sheet1.Rows.Count - 1, 1);

            }
            this.neuFpEnter1_Sheet1.SetActiveCell(this.neuFpEnter1_Sheet1.Rows.Count - 1, 0);
        }

        public void DeleteRow()
        {
            DialogResult rs = MessageBox.Show( "确认删除当前选择的信息吗？\n 请注意点击保存按钮以使改动生效", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information );
            if (rs == DialogResult.No)
            {
                return;
            }

            foreach (DataRow dr in this.dt.Rows)
            {
                dr.EndEdit();
            }

            this.neuFpEnter1_Sheet1.Rows.Remove(this.neuFpEnter1_Sheet1.ActiveRowIndex, 1);

            FS.FrameWork.Models.NeuObject item = new FS.FrameWork.Models.NeuObject();
            item.ID = this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.ActiveRowIndex, 4].Text;
            this.SetSelectItem( item );

            this.neuFpEnter1_Sheet1.SetActiveCell( this.neuFpEnter1_Sheet1.ActiveRowIndex, 0 );
        }

        #region 初始化
        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            InitDataSet();

            this.neuFpEnter1_Sheet1.Columns[4].Visible = false;
            this.neuFpEnter1_Sheet1.Columns[5].Visible = false;
            this.neuFpEnter1_Sheet1.Columns[8].Visible = false;
            this.neuFpEnter1_Sheet1.Columns[7].Visible = false;


            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "0";
            obj.Name = "全部";
            areaList.Add(obj);
            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "1";
            obj.Name = "门诊";
            areaList.Add(obj);
            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "2";
            obj.Name = "住院";
            areaList.Add(obj);
            //this.neuFpEnter1_Sheet1.RowCount = 1;
            itemList = itemManager.QueryValidItems();
            deptList = deptManager.GetDeptmentAll();
            this.neuFpEnter1.SetIDVisiable(this.neuFpEnter1_Sheet1, 0, false);
            this.neuFpEnter1.SetIDVisiable(this.neuFpEnter1_Sheet1, 1, false);
            this.neuFpEnter1.SetIDVisiable(this.neuFpEnter1_Sheet1, 2, false);
            this.neuFpEnter1.SetWidthAndHeight(240, 240);
            this.neuFpEnter1.SetColumnList(this.neuFpEnter1_Sheet1, 0, itemList);
            this.neuFpEnter1.SetColumnList(this.neuFpEnter1_Sheet1, 1, deptList);
            this.neuFpEnter1.SetColumnList(this.neuFpEnter1_Sheet1, 2, areaList);
            
            //this.neuFpEnter1.ShowListWhenOfFocus = true;            
            
            FarPoint.Win.Spread.InputMap im;

            im = this.neuFpEnter1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.neuFpEnter1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.neuFpEnter1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);            

            //string[] str=new string[3];
            //str[0] = "全部";
            //str[1] = "门诊";
            //str[2] = "住院";
            //FarPoint.Win.Spread.CellType.ComboBoxCellType cmbCellType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            //cmbCellType.Items = str;
            //this.neuFpEnter1_Sheet1.Columns[2].CellType = cmbCellType;

            this.neuFpEnter1.SetItem += new FS.FrameWork.WinForms.Controls.NeuFpEnter.setItem(neuFpEnter1_SetItem);
            this.neuFpEnter1.KeyEnter += new FS.FrameWork.WinForms.Controls.NeuFpEnter.keyDown(neuFpEnter1_KeyEnter);

        }

        private void InitDataSet()
        {
            this.neuFpEnter1_Sheet1.DataAutoSizeColumns = false;

            //定义类型
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Int32");
            System.Type dtBol = System.Type.GetType("System.Boolean");

            //在myDataTable中添加列
            this.dt.Columns.AddRange(new DataColumn[] {														
														new DataColumn("非药品名称",   dtStr),														
													    new DataColumn("科室名称",   dtStr),
                                                        new DataColumn("适用范围",   dtStr),
														new DataColumn("默认标记",   dtBol),
                                                        new DataColumn("非药品编码",     dtStr),														                                                        
														new DataColumn("科室编码",     dtStr),
														new DataColumn("顺序号",     dtDec),
                                                        new DataColumn("原科室编码",dtStr),
                                                        new DataColumn("原项目编码",dtStr)
											        });

            this.dt.DefaultView.AllowNew = true;
            dtview = new DataView(this.dt);
            this.neuFpEnter1_Sheet1.DataSource = this.dtview;
        }

        #endregion

        #region 查询
        protected override int OnQuery(object sender, object neuObject)
        {
            Query();
            return 1;
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            this.dt.Clear();
            this.dictUpdate.Clear();
            this.dictInsert.Clear();
            this.dictALL.Clear();
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("正在加载数据,请稍候..."));
            Application.DoEvents();
            ArrayList myList = this.itemManager.GetDeptByItemCode("ALL");
            //this.neuFpEnter1.ShowListWhenOfFocus = false;
            if (myList != null && myList.Count != 0)
            {
                SetQueryItem(myList);
            }
            AddNewRow();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            this.dt.AcceptChanges();
        }

        /// <summary>
        /// 查询科室维护信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected int SetQueryItem(ArrayList list)
        {            
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    FS.HISFC.Models.Fee.Item.Undrug itemObj = list[i] as FS.HISFC.Models.Fee.Item.Undrug;                    
                    SetValue(itemObj);
                    //主键 项目编码+科室编码+适用范围+默认标记+顺序号 [太多了~~先这么整吧，没想到别的办法]
                    //this.dictUpdate.Add(itemObj.ID + itemObj.ExecDept + text + itemObj.Memo, itemObj.Name + "|" + itemObj.User01);
                    //this.dictInsert.Add(itemObj.ID + itemObj.ExecDept, itemObj.Name + "|" + itemObj.User01);
                    //this.dictALL.Add(itemObj.ID + itemObj.ExecDept, itemObj.Name + "|" + itemObj.User01);
                }                
            }
            catch (Exception e)
            {
                MessageBox.Show("界面赋值出错");
                return -1;
            }
            return 1;
        }

        protected void SetValue(FS.HISFC.Models.Fee.Item.Undrug itemObj)
        {            
            if (itemObj.ItemArea == "0")
            {
                text = "全部";
            }
            else if (itemObj.ItemArea == "1")
            {
                text = "门诊";
            }
            else
            {
                text = "住院";
            }
            this.dt.Rows.Add(new object[] {
					                                itemObj.Name,					      //项目名称                          
				                                    itemObj.User01,//科室名称
                                                    text,//适用范围
                                                    FS.FrameWork.Function.NConvert.ToBoolean(itemObj.Memo),//默认标记
                                                    itemObj.ID, //原项目ID
                                                    itemObj.ExecDept,//原科室ID
                                                    FS.FrameWork.Function.NConvert.ToInt32( itemObj.UserCode),//顺序号     
                                                    itemObj.ExecDept,//原科室ID
                                                    itemObj.ID //原项目ID
                                                    
											        });
        }

        #endregion

        #region 选择项目
        private int neuFpEnter1_KeyEnter(Keys key)
        {
            if (key == Keys.Enter)
            {
                    if (this.GetSelectItem() == -1)
                    {
                        MessageBox.Show("由列表获取所选择项目出错");
                        return -1;
                    }
            }
            return 0;
        }

        /// <summary>
        /// 由列表获取所选择项目
        /// </summary>
        /// <returns>成功返回1 出错返回－1</returns>
        protected int GetSelectItem()
        {
            int currentRow = this.neuFpEnter1_Sheet1.ActiveRowIndex;
            if (currentRow < 0) return 0;
            if (this.neuFpEnter1_Sheet1.ActiveColumnIndex == 0)
            {
                //获取选中的信息
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.neuFpEnter1.getCurrentList(this.neuFpEnter1_Sheet1, 0);
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                if (item == null) return -1;
                this.SetSelectItem(item);               
                return 0;
            }
            if (this.neuFpEnter1_Sheet1.ActiveColumnIndex == 1)
            {
                //获取选中的信息
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.neuFpEnter1.getCurrentList(this.neuFpEnter1_Sheet1, 1);
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                if (item == null) return -1;
                this.SetSelectDept(item);
                return 0;
            }
            if (this.neuFpEnter1_Sheet1.ActiveColumnIndex == 2)
            {
                //获取选中的信息
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.neuFpEnter1.getCurrentList(this.neuFpEnter1_Sheet1, 2);
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                if (item == null) return -1;
                this.SetArea(item);
                return 0;
            }
            if (this.neuFpEnter1_Sheet1.ActiveColumnIndex == 6)
            {
                this.AddNewRow();
            }
            return 0;
        }

        /// <summary>
        /// 鼠标在项目列表中选择
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private int neuFpEnter1_SetItem(FS.FrameWork.Models.NeuObject obj)
        {
            switch (this.neuFpEnter1_Sheet1.ActiveColumnIndex)
            {
                case 0:
                    if (this.SetSelectItem(obj) == -1)
                    {
                        MessageBox.Show("处理所选择项目'项目名称'失败");
                        return -1;
                    }
                    break;
                case 1:
                    if (SetSelectDept(obj) == -1)
                    {
                        MessageBox.Show("处理所选择项目'科室名称'失败");
                        return -1;
                    }
                    break;
                case 2:
                    if (SetArea(obj) == -1)
                    {
                        MessageBox.Show("处理所选择项目'适用范围'失败");
                        return -1;
                    }
                    break;

            }
            return 0;
        }

        /// <summary>
        /// 处理由列表内所选择的项目
        /// </summary>
        /// <param name="obj">由弹出列表内所选择的项目</param>
        /// <returns>成功返回1 出错返回－1</returns>
        protected int SetSelectItem(FS.FrameWork.Models.NeuObject obj)
        {
            try
            {
                FS.HISFC.Models.Fee.Item.Undrug item = this.itemManager.GetValidItemByUndrugCode(obj.ID);
                if (item != null)
                {                    
                    this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.ActiveRowIndex, 0].Text = item.Name;
                    this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.ActiveRowIndex, 4].Text = item.ID;
                    this.neuFpEnter1_Sheet1.SetActiveCell(this.neuFpEnter1_Sheet1.ActiveRowIndex, 1);
                }
            }
            catch (Exception e)
            {
                return -1;
            }
                return 1;
        }

        /// <summary>
        /// 处理由列表内所选择的项目
        /// </summary>
        /// <param name="obj">由弹出列表内所选择的项目</param>
        /// <returns>成功返回1 出错返回－1</returns>
        protected int SetSelectDept(FS.FrameWork.Models.NeuObject obj)
        {
            try
            {
                FS.HISFC.Models.Base.Department dept = this.deptManager.GetDeptmentById(obj.ID);

                if (dept != null)
                {
                    this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.ActiveRowIndex, 1].Text = dept.Name;
                    this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.ActiveRowIndex, 5].Text = dept.ID;
                    this.neuFpEnter1_Sheet1.SetActiveCell(this.neuFpEnter1_Sheet1.ActiveRowIndex, 2);
                }
            }
            catch (Exception e)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 处理由列表内所选择的项目
        /// </summary>
        /// <param name="obj">由弹出列表内所选择的项目</param>
        /// <returns>成功返回1 出错返回－1</returns>
        protected int SetArea(FS.FrameWork.Models.NeuObject obj)
        {
            try
            {
                if (obj.ID == "0")
                {
                    this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.ActiveRowIndex, 2].Text = "全部";
                }
                else if (obj.ID == "1")
                {
                    this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.ActiveRowIndex, 2].Text = "门诊";
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.ActiveRowIndex, 2].Text = "住院";
                }
                this.neuFpEnter1_Sheet1.SetActiveCell(this.neuFpEnter1_Sheet1.ActiveRowIndex, 6);
            }
            catch (Exception e)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 校验是否存在重复项目
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        protected int Valid(DataTable tab)
        {
            Dictionary<string, string> validDict = new Dictionary<string, string>();//key是 非药品编码+"|"+科室编码+"|"+适用范围
            for(int i=0;i<tab.Rows.Count;i++)
            {
                DataRow row=tab.Rows[i] as DataRow;
                if (row.RowState != DataRowState.Deleted)//更新校验
                {
                    if (row["非药品编码"].ToString() != "" && row["科室编码"].ToString() != "")//非药品编码和科室编码不为空
                    {
                        string dtKey = row["非药品编码"].ToString() + "|" + row["科室编码"].ToString() + "|" + row["适用范围"].ToString();
                        if (validDict.ContainsKey(dtKey))
                        {
                            continue;
                        }
                        else
                        {
                            DataRow[] newrow = tab.Select("非药品编码 =" + "'" + row["非药品编码"].ToString() + "' and 科室编码 ='" + row["科室编码"].ToString() + "'");
                            if (newrow.Length > 1)
                            {
                                for (int j = 0; j < newrow.Length; j++)
                                {
                                    string key = newrow[j]["非药品编码"].ToString() + "|" + newrow[j]["科室编码"].ToString() + "|" + newrow[j]["适用范围"].ToString();
                                    if (newrow[j]["适用范围"].ToString() == "全部")
                                    {
                                        MessageBox.Show("存在重复项目  " + newrow[j]["非药品名称"].ToString() + "  " + newrow[j]["科室名称"].ToString());
                                        return -1;
                                    }
                                    else
                                    {
                                        if (validDict.ContainsKey(key))
                                        {
                                            MessageBox.Show("存在重复项目  " + newrow[j]["非药品名称"].ToString() + "  " + newrow[j]["科室名称"].ToString());
                                            return -1;
                                        }
                                        else
                                        {
                                            validDict.Add(key, newrow[j]["非药品名称"].ToString());
                                        }
                                    }
                                }
                            }
                            else//只有一条
                            {
                                validDict.Add(newrow[0]["非药品编码"].ToString() + "|" + newrow[0]["科室编码"].ToString() + "|" + newrow[0]["适用范围"].ToString(), newrow[0]["非药品名称"].ToString());

                            }
                        }
                    }
                }
            }
            
            return 1;
        }

        #endregion

        public override int Save(object sender, object neuObject)
        {
            this.neuFpEnter1.StopCellEditing();
            foreach (DataRow dr in this.dt.Rows)
            {
                dr.EndEdit();
            }
            DataTable validTab = this.dt.Copy();
            this.date = this.itemManager.GetDateTimeFromSysDateTime();
            FS.HISFC.Models.Fee.Item.Undrug item = null;
            //定义数据库处理事务
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            bool isUpdate = false;

            DataTable dataChanges = this.dt.GetChanges(DataRowState.Deleted);
            if (dataChanges != null)
            {
                dataChanges.RejectChanges();
                foreach (DataRow row in dataChanges.Rows)
                {
                    item = GetDataFromTable(row);
                    if (item.Name.ToString() != "" && item.User01.ToString() != "")
                    {
                        //执行更新操作，先更新，如果没有成功则插入新数据
                        if (this.itemManager.DeleteUnDrugCompare(item) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("删除信息发生错误!" + this.itemManager.Err));
                            return -1;
                        }
                    }
                }
                dataChanges.AcceptChanges();
                isUpdate = true;
            }
            dataChanges = this.dt.GetChanges(DataRowState.Modified | DataRowState.Added);
            if (dataChanges != null)
            {
                if (Valid(validTab) == -1)
                {
                    return -1;
                }
                foreach (DataRow row in dataChanges.Rows)
                {
                    item = GetDataFromTable(row);
                    if (item.ID == string.Empty || item.ExecDept == string.Empty)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("请从下拉列表中选择数据!"));
                        return -1;
                    }
                    if (item.Name.ToString() != "" && item.User01.ToString() != "")
                    {
                        //执行更新操作，先更新，如果没有成功则插入新数据
                        if (this.itemManager.SetUnDrugCompare(item) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("保存信息发生错误!" + this.itemManager.Err));
                            return -1;
                        }
                    }
                }
                dataChanges.AcceptChanges();
                isUpdate = true;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            if (isUpdate)
            {
                MessageBox.Show("保存成功");
            }
            this.Query();
            return 1;
        }

        protected FS.HISFC.Models.Fee.Item.Undrug GetDataFromTable(DataRow row)
        {
            FS.HISFC.Models.Fee.Item.Undrug item = new FS.HISFC.Models.Fee.Item.Undrug();
            item.ID = row["非药品编码"].ToString();//新项目编码
            item.Name = row["非药品名称"].ToString();
            item.ExecDept = row["科室编码"].ToString();//新科室编码
            item.User01 = row["科室名称"].ToString();
            item.UserCode = row["顺序号"].ToString();
            item.Memo = FS.FrameWork.Function.NConvert.ToInt32(row["默认标记"]).ToString();
            if (row["适用范围"].ToString() == "全部")
            {
                item.ItemArea = "0";
            }
            else if (row["适用范围"].ToString() == "门诊")
            {
                item.ItemArea = "1";
            }
            else
            {
                item.ItemArea = "2";
            }
            item.User02 = row["原科室编码"].ToString();//原科室编码
            item.User03 = row["原项目编码"].ToString();//原项目编码
            item.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;
            item.Oper.OperTime = date;
            return item;
        }


    }
}
