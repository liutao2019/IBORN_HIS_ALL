using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment
{
    public partial class ucDiagToPos : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private string sql;
        private DisTypeManage disTypeManage;
        private Dictionary<int, string> dicDisType;
        FarPoint.Win.Spread.CellType.ComboBoxCellType cmbDisType;
        private string type;
        private string permisstion;
        private FS.HISFC.Models.Base.Employee emp;

        private List<int> sourceList;
        private List<int> deleteList;
        private List<int> insertList;
        private List<int> updateList;

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService;

        /// <summary>
        /// 是否已经加载完毕，如果加载完毕将执行Sheet_cellChange事件
        /// </summary>
        private bool inited = false;
        /// <summary>
        /// 常数的类型
        /// </summary>
        public string Type
        {
            get
            {
                return type;
            }
            set 
            {
                type = value; ;
            }
        }

        /// <summary>
        /// 0: 标本库，1：护士站
        /// </summary>
        public string Permission
        {
            get
            {
                return permisstion;
            }
            set
            {
                permisstion = value;
            }
        }

        private enum Cols
        {
            type,              
            name, //如果标本使用则是诊断
            code,
            spell_code, 
            wb_code, 
            input_code, 
            sort_id,
            mark, //作为科室使用
            valid_state, 
            oper_code, 
            oper_date
        }

        public ucDiagToPos()
        {
            InitializeComponent();
            sql = "";
            disTypeManage = new DisTypeManage();
            dicDisType = new Dictionary<int, string>();
            cmbDisType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            type = "";
            emp = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

            sourceList = new List<int>();
            deleteList = new List<int>();
            insertList = new List<int>();
            updateList = new List<int>();
            toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        }

        private void InitDisType()
        {
            dicDisType = disTypeManage.GetAllDisType();
            
            if (dicDisType != null && dicDisType.Count > 0)
            {
                string[] itemData = new string[dicDisType.Count];
                string[] item = new string[dicDisType.Count];
                int i = 0;
                foreach (KeyValuePair<int, string> value in dicDisType)
                {
                    itemData[i] = value.Key.ToString();
                    item[i] = value.Value;
                    i++;                     
                }
                cmbDisType.ItemData = itemData;
                cmbDisType.Items = item;
            }
        }

        private void InitSpread()
        {
            try
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.type)].Label = "类型";
                neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.name)].Label = "名称";
                neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.code)].Label = "编号";
                neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.mark)].Label = "备注";
                neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.spell_code)].Label = "拼音码";
                neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.wb_code)].Label = "五笔码";
                neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.input_code)].Label = "输入码";
                //neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.sort_id)].Label = "排序ID";
                neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.valid_state)].Label = "有效性";
                neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.oper_code)].Label = "操作员";
                neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.oper_date)].Label = "操作时间";
                // neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.spell_code)].Label = "拼音码";

                neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.type)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.code)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.sort_id)].Visible = false;

                neuSpread1_Sheet1.Columns.Count = Convert.ToInt32(Cols.oper_date) + 1;
                if (type == "DiagnosebyNurse")
                {
                    neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.sort_id)].Visible = true;
                    neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.name)].Label = "诊断名称";
                    neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.name)].AllowAutoFilter = true;
                    neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.name)].AllowAutoSort = true;
                    neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.mark)].Label = "科室";
                    neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.sort_id)].Label = "需要采血";
                    neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.input_code)].Label = "部位";                   
                    neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.valid_state)].Visible = false;
                    //neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.code)].Visible = false;
                }
                if (type == "SpecPos")
                {
                    neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.spell_code)].Visible =false;// = "拼音码";
                    neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.wb_code)].Visible = false;
                    neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.code)].Visible = true;
                    neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.input_code)].Visible = false;
                    neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.mark)].Visible = false;
                }
                neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.sort_id)].Visible = false;
                neuSpread1_Sheet1.AutoGenerateColumns = false;
                neuSpread1_Sheet1.DataAutoSizeColumns = false;
                for (int i = 0; i < neuSpread1_Sheet1.Columns.Count; i++)
                {
                    neuSpread1_Sheet1.Columns[i].Width = 100;
                    neuSpread1_Sheet1.Columns[i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    neuSpread1_Sheet1.Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;// = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                }
            }
            catch
            { }
        }

        private void GetData()
        {
            deleteList.Clear();
            updateList.Clear();
            insertList.Clear();
            sourceList.Clear();

            neuSpread1_Sheet1.Rows.Count = 0;
            sql = "select * from com_dictionary where type = '{0}'";

            if (permisstion == "1")
            {
                sql += " and mark = '{1}'";  
            }
            else
            {
                if (type == "SpecPos")
                {
                    sql += " order by code";
                }
                else
                {
                    sql += " order by name";
                }
            }
            sql = string.Format(sql, type,emp.Dept.ID);
            DataSet ds = new DataSet();
            disTypeManage.ExecQuery(sql, ref ds);
            if (ds == null || ds.Tables.Count < 0)
            {
                return;
            }
            if (type == "DiagnosebyNurse")
            {
                if (permisstion == "0")
                {
                    InitDisType();
                    FarPoint.Win.Spread.CellType.ComboBoxCellType cmb = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                    cmb.ItemData = new string[] { "是", "否" };
                    cmb.Items = new string[] { "是", "否" };
                    //int colIndex = Convert.ToInt32(Cols.input_code);
                    neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.input_code)].CellType = cmbDisType;
                    neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.sort_id)].CellType = cmb;
                }
                else
                {
                    neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.mark)].Visible = false;
                    neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.sort_id)].Visible = false;
                }
            }
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int rowIndex = neuSpread1_Sheet1.Rows.Count;
                neuSpread1_Sheet1.Rows.Count= rowIndex +1;

                neuSpread1_Sheet1.Cells[rowIndex, Convert.ToInt32(Cols.type)].Text = dr["TYPE"] == null ? "" : dr["TYPE"].ToString();
                string sortId = dr["SORT_ID"] == null ? "" : dr["SORT_ID"].ToString();
                neuSpread1_Sheet1.Cells[rowIndex, Convert.ToInt32(Cols.sort_id)].Text = sortId == "1" ? "是" : "否";
                neuSpread1_Sheet1.Cells[rowIndex, Convert.ToInt32(Cols.spell_code)].Text = dr["SPELL_CODE"] == null ? "" : dr["SPELL_CODE"].ToString();
                neuSpread1_Sheet1.Cells[rowIndex, Convert.ToInt32(Cols.wb_code)].Text = dr["WB_CODE"] == null ? "" : dr["WB_CODE"].ToString();
                neuSpread1_Sheet1.Cells[rowIndex, Convert.ToInt32(Cols.oper_code)].Text = dr["OPER_CODE"] == null ? "" : dr["OPER_CODE"].ToString();
                neuSpread1_Sheet1.Cells[rowIndex, Convert.ToInt32(Cols.oper_date)].Text = dr["OPER_DATE"] == null ? "" : Convert.ToDateTime(dr["OPER_DATE"].ToString()).ToShortDateString();
                neuSpread1_Sheet1.Cells[rowIndex, Convert.ToInt32(Cols.name)].Text = dr["NAME"] == null ? "" : dr["NAME"].ToString();
                neuSpread1_Sheet1.Cells[rowIndex, Convert.ToInt32(Cols.mark)].Text = dr["MARK"] == null ? "" : dr["MARK"].ToString();
                neuSpread1_Sheet1.Cells[rowIndex, Convert.ToInt32(Cols.input_code)].Text = dr["INPUT_CODE"] == null ? "" : dr["INPUT_CODE"].ToString();
                neuSpread1_Sheet1.Cells[rowIndex, Convert.ToInt32(Cols.code)].Text = dr["CODE"] == null ? "" : dr["CODE"].ToString();
                neuSpread1_Sheet1.Cells[rowIndex, Convert.ToInt32(Cols.valid_state)].Text = dr["VALID_STATE"] == null ? "" : dr["VALID_STATE"].ToString();
                sourceList.Add(rowIndex);
            }
        }

        private void Add()
        {
            this.neuSpread1_Sheet1.RowCount += 1;
            int rowIndex = this.neuSpread1_Sheet1.RowCount - 1;
            insertList.Add(rowIndex);
            neuSpread1_Sheet1.ActiveRowIndex = rowIndex;
        }

        private void Delete()
        {

            int i = neuSpread1_Sheet1.ActiveRowIndex;
            if (neuSpread1_Sheet1.Cells[i, Convert.ToInt32(Cols.mark)].Text.Trim() != emp.Dept.ID)
            {
                MessageBox.Show("没有权限删除其他科室的诊断");
                return;
            }

              if(MessageBox.Show("确认要删除吗？","提示",MessageBoxButtons.YesNo)==DialogResult.No)
            {
                return;
            }

            if (insertList.Contains(i))
            {
                this.insertList.Remove(i);
                this.neuSpread1_Sheet1.ActiveRow.Visible = false;
                return;
            }
            else
            {
                this.deleteList.Add(i);
                this.neuSpread1_Sheet1.ActiveRow.Visible = false;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                InitSpread();
                GetData();
                inited = true;
            }
            catch
            {
 
            }
            base.OnLoad(e);
        }

        private void Save()
        {
            FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
            string specUpdateSql = "update com_dictionary set input_code='{0}', SORT_ID = {4} where name = '{1}' and CODE = '{2}' and type = '{3}'";
            foreach (int i in insertList)
            {
             //type,cons.ID,cons.Name,cons.Memo,cons.SpellCode,cons.WBCode,cons.UserCode,cons.SortID,FS.NFC.Function.NConvert.ToInt32(cons.IsValid),this.Operator.ID);

                FS.HISFC.Models.Base.Const c = new FS.HISFC.Models.Base.Const();
                if (type == "DiagnosebyNurse")
                {
                    c.ID = "";
                    string id = "";
                    disTypeManage.GetNextSequence(ref id);
                    c.ID = id;
                }
                else
                {
                    c.ID = neuSpread1_Sheet1.Cells[i, Convert.ToInt32(Cols.code)].Text;
                }
                c.Name = neuSpread1_Sheet1.Cells[i, Convert.ToInt32(Cols.name)].Text;
                c.Memo = emp.Dept.ID;
                c.SpellCode = neuSpread1_Sheet1.Cells[i, Convert.ToInt32(Cols.spell_code)].Text;
                c.WBCode = neuSpread1_Sheet1.Cells[i, Convert.ToInt32(Cols.wb_code)].Text;
                c.UserCode = neuSpread1_Sheet1.Cells[i, Convert.ToInt32(Cols.input_code)].Text;                
                c.SortID = 1;
                c.IsValid = true;
                int inRes = conMgr.InsertItem(type, c);
            }
            foreach (int d in deleteList)
            {
                string id = neuSpread1_Sheet1.Cells[d, Convert.ToInt32(Cols.code)].Text.Trim();
                int del = conMgr.DelConstant(type, id);
            }
            foreach (int u in updateList)
            {
                if (permisstion == "0")
                {
                    string tmpSql = specUpdateSql;
                    string input_code = neuSpread1_Sheet1.Cells[u, Convert.ToInt32(Cols.input_code)].Text;
                    string name = neuSpread1_Sheet1.Cells[u, Convert.ToInt32(Cols.name)].Text;
                    string code = neuSpread1_Sheet1.Cells[u, Convert.ToInt32(Cols.code)].Text;
                    string sortId = neuSpread1_Sheet1.Cells[u, Convert.ToInt32(Cols.sort_id)].Text.Trim() == "是" ? "1" : "0";
                    tmpSql = string.Format(tmpSql, input_code, name, code, type,sortId);
                    int res = disTypeManage.ExecNoQuery(tmpSql);
                }
                else
                {
                    FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();
                    con.ID = neuSpread1_Sheet1.Cells[u, Convert.ToInt32(Cols.code)].Text;         
                    con.Name = neuSpread1_Sheet1.Cells[u, Convert.ToInt32(Cols.name)].Text;
                    con.Memo = emp.Dept.ID;
                    con.SpellCode = neuSpread1_Sheet1.Cells[u, Convert.ToInt32(Cols.spell_code)].Text;
                    con.WBCode = neuSpread1_Sheet1.Cells[u, Convert.ToInt32(Cols.wb_code)].Text;
                    con.UserCode = neuSpread1_Sheet1.Cells[u, Convert.ToInt32(Cols.input_code)].Text;                    
                    con.IsValid = true;
                    conMgr.UpdateItem(type, con);
                }
            }
            MessageBox.Show("保存成功");
        //    string nurseUpdateSql = "UPDATE COM_DICTIONARY SET NAME = '{0}',MARK = '{1}',SPELL_CODE = '{2}'\n" +
        //        "  ,WB_CODE = '{3}',INPUT_CODE = '{4}',SORT_ID = {5},VALID_STATE = '{6}',OPER_CODE = '{7}',OPER_DATE = 'timestamp('{8}')'\n" +
        //        " where TYPE = '{9}' and CODE = '{10}'";
        //    string insertSql = "insert into COM_DICTIONARY (TYPE ,CODE  ,NAME  ,MARK  ,SPELL_CODE  ,WB_CODE  ,INPUT_CODE  ,SORT_ID\n"+
        //        "  ,VALID_STATE  ,OPER_CODE  ,OPER_DATE) VALUES ('{9}' TYPE,'' - CODE  ,'' -- NAME  ,'' -- MARK  ,'' -- SPELL_CODE\n" +
        //        "  ,'' -- WB_CODE  ,'' -- INPUT_CODE  ,0 -- SORT_ID  ,'' -- VALID_STATE  ,'' -- OPER_CODE  ,'' -- OPER_DATE)";
        //
        }

        public override int Save(object sender, object neuObject)
        {
            this.Save();
            GetData();
            return base.Save(sender, neuObject);
        }

        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (sourceList.Contains(e.Row) && !updateList.Contains(e.Row))
            {
                updateList.Add(e.Row);
            }
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("删除", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            this.toolBarService.AddToolButton("添加", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T添加, true, false, null);
            //this.toolBarService.AddToolButton("取消样本", "", (int)FS.NFC.Interface.Classes.EnumImageList.A取消, true, false, null);
            return this.toolBarService;
            //return base.OnInit(sender, neuObject, param);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "删除":
                    this.Delete();
                    break;
                case "添加":
                    this.Add();
                    break; 
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        private void neuSpread1_Sheet1_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
        {

            if (inited)
            {
                //如果改变了名称，则拼音码、五笔码自动发生变化
                if (e.Column == Convert.ToInt32(Cols.name))
                {
                    FarPoint.Win.Spread.Column column = neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.spell_code)];
                    if (column != null /*&& this.fpSpread1_Sheet1.Cells[e.Row,column.Index].Text.Length==0*/)
                    {
                        this.neuSpread1_Sheet1.Cells[e.Row, column.Index].Text = FS.FrameWork.Public.String.GetSpell(this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Text);
                    }

                    column = neuSpread1_Sheet1.Columns[Convert.ToInt32(Cols.wb_code)];
                    if (column != null)
                    {
                        FS.HISFC.BizLogic.Manager.Spell spellManager = new FS.HISFC.BizLogic.Manager.Spell();
                        FS.HISFC.Models.Base.ISpell spCode = spellManager.Get(this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Text);
                        if (spCode != null)
                            this.neuSpread1_Sheet1.Cells[e.Row, column.Index].Text = spCode.WBCode;
                    }
                }
            }
        }

        public override int Exit(object sender, object neuObject)
        {
            //if (permisstion == "1")
            //{
            //    FS.HISFC.Management.Manager.Constant conMgr = new FS.HISFC.Management.Manager.Constant();

            //    foreach (int u in sourceList)
            //    {
            //        FS.HISFC.Object.Base.Const con = new FS.HISFC.Object.Base.Const();
            //        con.ID = neuSpread1_Sheet1.Cells[u, Convert.ToInt32(Cols.code)].Text;
            //        con.Name = neuSpread1_Sheet1.Cells[u, Convert.ToInt32(Cols.name)].Text;
            //        con.Memo = emp.Dept.ID;
            //        con.SpellCode = neuSpread1_Sheet1.Cells[u, Convert.ToInt32(Cols.spell_code)].Text;
            //        con.WBCode = neuSpread1_Sheet1.Cells[u, Convert.ToInt32(Cols.wb_code)].Text;
            //        con.UserCode = neuSpread1_Sheet1.Cells[u, Convert.ToInt32(Cols.input_code)].Text;                 
            //        con.IsValid = true;
            //        conMgr.UpdateItem(type, con);
            //    }
            //}
            return base.Exit(sender, neuObject);
        }

    }
}
