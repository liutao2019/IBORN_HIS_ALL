using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.DCP.Controls
{
    /// <summary>
    /// [功能描述： 常数维护，在备注一栏中不让修改，只能选择项]
    /// [创 建 者 ： 赵景]
    /// [创建时间： 2008-09]
    /// <修改记录>
    /// </修改记录>
    /// </summary>
    public partial class ucInfectClassConst : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.FrameWork.WinForms.Forms.IMaintenanceControlable
    {
        public ucInfectClassConst()
        {
            InitializeComponent();
        }

        #region 域变量

        /// <summary>
        /// 常数列表
        /// </summary>
        private DataTable dtConst=new DataTable();

        /// <summary>
        /// 传染病附卡常数
        /// </summary>
        private ArrayList infectClassList = new ArrayList();

        /// <summary>
        /// 附卡常数帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper additionHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 人员帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper employeeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.SOC.HISFC.BizProcess.DCP.Common commonProcess = new FS.SOC.HISFC.BizProcess.DCP.Common();

        /// <summary>
        /// 附卡提示常数帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper AddtionReportMsgHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 拼音管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Spell spellLogicManagment = new FS.HISFC.BizLogic.Manager.Spell();

        private int columnCount;
        private bool isShowID = false;
        private string ConstType = "";
        private bool isSelectOne = false;
        #endregion

        #region 方法

        /// <summary>
        /// 设置列值
        /// </summary>
        private void SetFpValue()
        {
            this.neuSpread1_Sheet1.RowCount = 0;

            foreach (FS.HISFC.Models.Base.Const con in this.infectClassList)
            {
                this.neuSpread1_Sheet1.RowCount++;

                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)SetColName.ConNO].Value = con.ID;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)SetColName.ConName].Value = con.Name;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)SetColName.ConMemo].Value = con.Memo;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)SetColName.ConSpellCode].Value = con.SpellCode;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)SetColName.ConWBCode].Value = con.WBCode;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)SetColName.ConUserCode].Value = con.UserCode;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)SetColName.ConSortNO].Value = con.SortID;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)SetColName.ConValid].Value = con.IsValid;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)SetColName.ConOperName].Value = con.OperEnvironment.ID;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)SetColName.ConOperTime].Value = con.OperEnvironment.OperTime;
            }
        }

        /// <summary>
        /// 将数据添加到dtConsts
        /// </summary>
        /// <param name="con">常数实体</param>
        public void AddDataToTable(FS.HISFC.Models.Base.Const con)
        {
            if (con == null)
            {
                return;
            }

            this.dtConst.Rows.Add(new object[]{
                con.ID,
                con.Name,
                con.Memo,
                con.SpellCode,
                con.WBCode,
                con.UserCode,
                con.SortID,
                con.IsValid,
                con.OperEnvironment.ID,
                con.OperEnvironment.OperTime});
        }

        /// <summary>
        /// 将数据行添加到实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.Const AddData(DataRow dr)
        {
            if (dr == null)
            {
                return null;
            }
            
            FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();

            con.ID = dr[(int)SetColName.ConNO].ToString();
            con.Name = dr[(int)SetColName.ConName].ToString();
            con.Memo = dr[(int)SetColName.ConMemo].ToString();
            con.SpellCode = dr[(int)SetColName.ConSpellCode].ToString();
            con.WBCode = dr[(int)SetColName.ConWBCode].ToString();
            con.UserCode = dr[(int)SetColName.ConUserCode].ToString();
            con.SortID = FS.FrameWork.Function.NConvert.ToInt32(dr[(int)SetColName.ConSortNO]);
            con.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(dr[(int)SetColName.ConValid]);
            con.OperEnvironment.ID = this.employeeHelper.GetID(dr[(int)SetColName.ConOperName].ToString());
            con.OperEnvironment.OperTime =FS.FrameWork.Function.NConvert.ToDateTime( dr[(int)SetColName.ConOperTime]);

            return con;
        }

        /// <summary>
        /// 初始化dtConst
        /// </summary>
        public void InitDataTable()
        {
            this.neuSpread1_Sheet1.DataAutoSizeColumns = false;

            //定义类型
            System.Type typeStr = System.Type.GetType("System.String");
            System.Type typeInt= System.Type.GetType("System.Int");
            System.Type typeDateTime= System.Type.GetType("System.DateTime");
            System.Type typeBoolean = System.Type.GetType("System.Boolean");

            //初始化列
            this.dtConst.Columns.AddRange(new DataColumn[]{
                                                        new DataColumn("编码",typeStr),
                                                        new DataColumn("名称",typeStr),
                                                        new DataColumn("备注",typeStr),
                                                        new DataColumn("拼音码",typeStr),
                                                        new DataColumn("五笔码",typeStr),
                                                        new DataColumn("输入码",typeStr),
                                                        new DataColumn("顺序号",typeStr),
                                                        new DataColumn("是否有效",typeBoolean),
                                                        new DataColumn("操作员",typeStr),
                                                        new DataColumn("操作时间",typeDateTime)
                                                        });
            this.dtConst.DefaultView.AllowNew = true;
            this.dtConst.DefaultView.AllowEdit = true;
            this.dtConst.DefaultView.AllowDelete = true;

            this.neuSpread1_Sheet1.DataSource = this.dtConst.DefaultView;
        }

        /// <summary>
        /// 初始化Fp
        /// </summary>
        public void SetFormatFp()
        {
            this.neuSpread1_Sheet1.Columns[(int)SetColName.ConNO].Locked = true;
            this.neuSpread1_Sheet1.Columns[(int)SetColName.ConOperName].Locked = true;
            this.neuSpread1_Sheet1.Columns[(int)SetColName.ConOperTime].Locked = true;
        }

        /// <summary>
        /// 判断是否有效
        /// </summary>
        /// <param name="drAdd"></param>
        /// <returns></returns>
        public int IsValid()
        {
            DataTable dt = this.dtConst.GetChanges(DataRowState.Modified | DataRowState.Added);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (string.IsNullOrEmpty(dr["名称"].ToString()))
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("名称不能为空"));
                        return -1;
                    }
                }
            }

            dt = this.dtConst.GetChanges(DataRowState.Unchanged | DataRowState.Modified);
            DataTable dtAdd = this.dtConst.GetChanges(DataRowState.Added);
            if (dt != null && dtAdd != null)
            {
                foreach (DataRow drAdd in dtAdd.Rows)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["编码"].ToString() == drAdd["编码"].ToString())
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("已存在" + drAdd["编码"].ToString() + "编码"));
                            return -1;
                        }
                    }
                }
            }

            return 1;
        }

        #endregion

        #region 事件

        protected virtual void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            try
            {
                if (e.Column == columnCount)
                {
                    if (isSelectOne)
                    {
                        FS.FrameWork.WinForms.Forms.frmEasyChoose frmPop = new FS.FrameWork.WinForms.Forms.frmEasyChoose(this.AddtionReportMsgHelper.ArrayObject);
                        frmPop.Text = "请选择项目";
                        frmPop.StartPosition = FormStartPosition.CenterScreen;
                        frmPop.SelectedItem += new FS.FrameWork.WinForms.Forms.SelectedItemHandler(frmPop_SelectedItem);
                        frmPop.ShowDialog();
                    }
                    else
                    {
                        System.Collections.Hashtable hs = new System.Collections.Hashtable();
                        string memo = this.dtConst.Rows[e.Row][columnCount].ToString();
                        if (!string.IsNullOrEmpty(memo))
                        {
                            foreach (string s in memo.Split(','))
                            {
                                if (hs.ContainsValue(s))
                                {
                                    continue;
                                }
                                else
                                {
                                    if (!isShowID)
                                    {
                                        hs.Add(this.AddtionReportMsgHelper.GetID(s), s);
                                    }
                                    else
                                    {
                                        hs.Add(s, s);
                                    }
                                }
                            }
                        }
                        ucMyReport ucMyReport = new ucMyReport(hs);
                        int i = this.AddtionReportMsgHelper.ArrayObject.Count / 14;
                        if (i > 1)
                        {
                            ucMyReport.Size = new Size(150 * i, 300);
                        }
                        ucMyReport.ConstList = this.AddtionReportMsgHelper.ArrayObject;
                        ucMyReport.enterDataBinding += new ucMyReport.DataBinding(ucMyReport_enterDataBinding);

                        FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucMyReport, FormBorderStyle.Fixed3D, FormWindowState.Normal);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg(ex.Message));
                this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Value = "";
            }
        }

        private void frmPop_SelectedItem(FS.FrameWork.Models.NeuObject sender)
        {
            if (sender != null)
            {
                if (isShowID)
                {
                    this.neuSpread1_Sheet1.ActiveCell.Value = sender.ID;
                }
                else
                {
                    this.neuSpread1_Sheet1.ActiveCell.Value = sender.Name;
                }
            }
        }

        public void ucMyReport_enterDataBinding(System.Collections.Hashtable hsMemo)
        {
            string s="";
            foreach (System.Collections.DictionaryEntry de in hsMemo)
            {
                if (!isShowID)
                {
                    if (de.Value.ToString() != "")
                    {
                        s += "," + de.Value.ToString();
                    }
                }
                else
                {
                    if (de.Key.ToString() != "")
                    {
                        s += "," + de.Key.ToString();
                    }
                }
            }

            if(s.StartsWith(","))
            {
                s=s.Remove(0,1);
            }

            this.neuSpread1_Sheet1.ActiveCell.Value = s;
        }

        protected virtual void fpSpread1_Sheet1_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
        {           
            //如果改变了名称，则拼音码、五笔码自动发生变化
            if (e.Column==(int)SetColName.ConName)
            {
                FarPoint.Win.Spread.Column column = this.neuSpread1_Sheet1.Columns[(int)SetColName.ConSpellCode];
                if (column != null /*&& this.fpSpread1_Sheet1.Cells[e.Row,column.Index].Text.Length==0*/)
                {
                    FS.HISFC.Models.Base.ISpell spCode = this.spellLogicManagment.Get(this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Text);
                    if (spCode != null)
                        this.neuSpread1_Sheet1.Cells[e.Row, column.Index].Text = spCode.SpellCode;
                }

                column = this.neuSpread1_Sheet1.Columns[(int)SetColName.ConWBCode];
                if (column != null)
                {
                    FS.HISFC.Models.Base.ISpell spCode = this.spellLogicManagment.Get(this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Text);
                    if (spCode != null)
                        this.neuSpread1_Sheet1.Cells[e.Row, column.Index].Text = spCode.WBCode;
                }
            }
        }

        private void neuTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (dtConst != null)
            {
                this.dtConst.DefaultView.RowFilter = string.Format("编码 like '%{0}%' or 名称 like '%{0}%' or 备注 like '%{0}%'  or 拼音码 like '%{0}%' or 五笔码 like '%{0}%'", this.neuTextBox1.Text);
            }
        }

        #endregion

        #region IMaintenanceControlable 成员

        private bool isDirty = false;

        private FS.FrameWork.WinForms.Forms.IMaintenanceForm queryForm = null;

        /// <summary>
        /// 添加方法
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            if (this.dtConst == null)
            {
                return -1;
            }

            this.dtConst.Rows.InsertAt(this.dtConst.NewRow(), this.dtConst.Rows.Count);

            this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.RowCount - 1;
            this.neuSpread1_Sheet1.ActiveColumnIndex = 0;
            this.neuSpread1_Sheet1.ActiveRow.Locked = false;
            this.neuSpread1_Sheet1.Columns[(int)SetColName.ConMemo].Locked = true;

            return 1;
        }

        public int Copy()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Cut()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            if (this.dtConst==null||this.dtConst.Rows.Count==0)
            {
                return -1;
            }

            if (this.neuSpread1_Sheet1.ActiveRow == null)
            {
                return -1;
            }

            this.dtConst.Rows.Remove(this.dtConst.Rows[this.neuSpread1_Sheet1.ActiveRowIndex]);
            //this.dtConst.AcceptChanges();

            return 1;
        }

        public int Export()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Import()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Init()
        {
            this.InitDataTable();

            string[] s = this.FocusedControl.Text.Split('|');
            string constType = "";
            columnCount = (int)SetColName.ConMemo;
            if (s.Length >= 2)
            {
                this.ConstType = s[0];
                string[] sTemp = s[1].Split(',');
                if (sTemp.Length == 4)
                {
                    constType = sTemp[0];
                    columnCount = FS.FrameWork.Function.NConvert.ToInt32(sTemp[1]);
                    isShowID = FS.FrameWork.Function.NConvert.ToBoolean(sTemp[2]);
                    isSelectOne = FS.FrameWork.Function.NConvert.ToBoolean(sTemp[3]);
                }
            }

            this.neuSpread1_Sheet1.Columns[columnCount].Locked = true;
            this.neuSpread1_Sheet1.Columns[columnCount].BackColor = Color.FromArgb(220, 220, 220);

            //传染病的类型
            if (constType == "INFECTCLASS")
            {
                ArrayList alInfectClass = new ArrayList();

                alInfectClass.AddRange(commonProcess.QueryConstantList("INFECTCLASS"));
                if (alInfectClass == null)
                {
                    return -1;
                }

                ArrayList al = new ArrayList();
                foreach (FS.HISFC.Models.Base.Const infectclass in alInfectClass)
                {
                    ArrayList alItem = commonProcess.QueryConstantList(infectclass.ID);

                    al.AddRange(alItem);
                }
                this.AddtionReportMsgHelper.ArrayObject = al;
            }
            else
            {
                this.AddtionReportMsgHelper.ArrayObject = this.commonProcess.QueryConstantList(constType);
            }
            if (this.AddtionReportMsgHelper.ArrayObject == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("获取传染病附卡提示常数失败！"));
                return -1;
            }


            return 1;
        }

        public bool IsDirty
        {
            get
            {
                return this.isDirty;
            }
            set
            {
                this.isDirty = value;
            }
        }

        public int Modify()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int NextRow()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Paste()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int PreRow()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Print()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int PrintConfig()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int PrintPreview()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public int Query()
        {
            if (this.infectClassList == null)
            {
                return -1;
            }

            this.SetFormatFp();

            this.dtConst.Rows.Clear();
            this.additionHelper.ArrayObject = this.commonProcess.QueryConstantList(this.ConstType);
            if (this.additionHelper.ArrayObject == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("获取传染病附卡常数出错！"));
                return -1;
            }

            this.infectClassList = this.additionHelper.ArrayObject;

            foreach (FS.HISFC.Models.Base.Const con in this.infectClassList)
            {
                this.AddDataToTable(con);
            }

            this.dtConst.AcceptChanges();

            return 1;
        }

        public FS.FrameWork.WinForms.Forms.IMaintenanceForm QueryForm
        {
            get
            {
                return this.queryForm;
            }
            set
            {
                this.queryForm = value;
            }
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            this.neuSpread1.StopCellEditing();

            foreach (DataRow dr in this.dtConst.Rows)
            {
                dr.EndEdit();
            }

            //判断有效性
            if (this.IsValid() == -1)
            {
                return -1;
            }
            
            //定义事物
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.HISFC.BizLogic.Manager.Constant consManagment = new FS.HISFC.BizLogic.Manager.Constant();

            consManagment.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            DataTable dtChanges = this.dtConst.GetChanges( DataRowState.Modified | DataRowState.Added);
            
            if (dtChanges != null)
            {
                foreach (DataRow dr in dtChanges.Rows)
                {                    
                    FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();
                    con = this.AddData(dr);
                    int i = consManagment.UpdateItem(this.ConstType, con);
                    if ( i==0)
                    {
                        if (consManagment.InsertItem(this.ConstType, con) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("更新失败") + consManagment.Err);
                            return -1;
                        }
                    }
                    else if(i==-1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("更新失败") + consManagment.Err);
                        return -1;
                    }
                }
            }

            DataTable dtDelete = this.dtConst.GetChanges(DataRowState.Deleted);

            if (dtDelete != null)
            {
                dtDelete.RejectChanges();
                foreach (DataRow dr in dtDelete.Rows)
                {
                    if (consManagment.DelConstant(this.ConstType, dr[(int)SetColName.ConNO].ToString()) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("删除失败") + consManagment.Err);
                        return -1;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            this.SetFormatFp();
            this.dtConst.AcceptChanges();
            MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存成功"));
            return 1;
        }

        #endregion

        
               
    }

    /// <summary>
    /// 列枚举
    /// </summary>
    public enum SetColName
    {
        /// <summary>
        /// 常数代码
        /// </summary>
        ConNO,
        /// <summary>
        /// 常数名称
        /// </summary>
        ConName,
        /// <summary>
        /// 常数备注
        /// </summary>
        ConMemo,
        /// <summary>
        /// 拼音码
        /// </summary>
        ConSpellCode,
        /// <summary>
        /// 五笔码
        /// </summary>
        ConWBCode,
        /// <summary>
        /// 输入码
        /// </summary>
        ConUserCode,
        /// <summary>
        /// 顺序号
        /// </summary>
        ConSortNO,
        /// <summary>
        /// 是否有效
        /// </summary>
        ConValid,
        /// <summary>
        /// 操作员
        /// </summary>
        ConOperName,
        /// <summary>
        /// 操作时间
        /// </summary>
        ConOperTime,
    }
}
