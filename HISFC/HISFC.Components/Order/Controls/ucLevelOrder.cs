using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace FS.HISFC.Components.Order.Controls
{
    //{1EB2DEC4-C309-441f-BCCE-516DB219FD0E} 层级形式开立医嘱 yangw 20101024
    public partial class ucLevelOrder : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucLevelOrder()
        {
            InitializeComponent();
        }

        #region 属性
        public int InOutType
        {
            set
            {
                this.tvItemLevel1.InOutType = value;
            }
            get
            {
                return this.tvItemLevel1.InOutType;
            }
        }

        private ArrayList alOrder;

        public ArrayList AlOrder
        {
            set
            {
                alOrder = value;
            }
            get
            {
                return this.alOrder;
            }
        }

        /// <summary>
        /// 患者信息
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patient = null;

        /// <summary>
        /// 患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            set
            {
                if (value == null)
                {
                    MessageBox.Show("患者信息赋值错误");
                    return;
                }
                this.patient = value;
            }
        }


        private ArrayList alOrderType;

        protected FS.FrameWork.Public.ObjectHelper hprOrderType = new FS.FrameWork.Public.ObjectHelper();

        protected FS.FrameWork.Public.ObjectHelper hprUndrug = new FS.FrameWork.Public.ObjectHelper();

        protected System.Collections.Generic.Dictionary<string, ArrayList> dctExecDepts = new Dictionary<string, ArrayList>();

        //protected FS.FrameWork.Public.ObjectHelper hprDepts = new FS.FrameWork.Public.ObjectHelper();

        //protected FS.FrameWork.Public.ObjectHelper hprFrequence = new FS.FrameWork.Public.ObjectHelper();

        //protected System.Collections.Generic.Dictionary<string,ArrayList> dctExecDepts = new Dictionary<string,ArrayList>();

        protected FS.HISFC.BizLogic.Manager.ItemLevel itemLevelManager = new FS.HISFC.BizLogic.Manager.ItemLevel();

        private DataTable dt = new DataTable();

        private DataView dv;
        #endregion

        #region 函数
        /// <summary>
        /// 初始化DataSet
        /// </summary>
        private void InitDataTable()
        {
            //定义类型
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtBol = System.Type.GetType("System.Boolean");

            this.dt.Columns.AddRange(new DataColumn[] {
                                                                    new DataColumn("选择",        dtBol),
                                                                    new DataColumn("项目编码",	  dtStr),
                                                                    new DataColumn("项目名称",    dtStr),
                                                                    new DataColumn("规格",		  dtStr),
                                                                    new DataColumn("价格",		  dtStr),
                                                                    new DataColumn("单位",		  dtStr),
                                                                    new DataColumn("排序号",	  dtStr),
                                                                    new DataColumn("拼音码",      dtStr),
                                                                    new DataColumn("五笔码",      dtStr)
            });

            dv = new DataView(dt);

            this.neuSpread1_Sheet1.DataSource = this.dv;
        }

        /// <summary>
        /// 医嘱保存
        /// </summary>
        protected int Save()
        {
            if (this.Valid() == -1)
                return -1;

            this.AlOrder = new ArrayList();
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (!FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)Col.ColCheck].Value))
                {
                    continue;
                }
                string comboID = "";
                try
                {
                    comboID = CacheManager.OutOrderMgr.GetNewOrderComboID();//添加组合号;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("获取医嘱组合号出错" + ex.Message);
                    return -1;
                }

                if (this.InOutType == 2)
                {
                    FS.HISFC.Models.Order.Inpatient.Order order;

                    order = new FS.HISFC.Models.Order.Inpatient.Order();
                    //order.Item = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Fee.Item.Undrug;
                    FS.HISFC.Models.Fee.Item.Undrug undrugTag = this.hprUndrug.GetObjectFromID(this.neuSpread1_Sheet1.Cells[i, (int)Col.ColCode].Text) as FS.HISFC.Models.Fee.Item.Undrug;
                    if (undrugTag == null)
                    {
                        continue;
                    }
                    order.Item = undrugTag;
                    if (order.Item == null)
                        continue;
                    //患者信息
                    order.Patient = this.patient;
                    //医嘱组合号
                    order.Combo.ID = comboID;
                    //医嘱类型
                    order.OrderType = this.hprOrderType.GetObjectFromID("LZ") as FS.HISFC.Models.Order.OrderType;

                    //单位  {AE53ACB5-3684-42e8-BF28-88C2B4FF2360}
                    order.Unit = order.Item.PriceUnit;

                    //煎药方式
                    order.Memo = this.txtMemo.Text;
                    
                    order.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.txtQty.Text);

                    order.ExeDept = this.cmbExecDept.SelectedItem;

                    if (this.cmbLabSample.SelectedItem != null)
                    {
                        order.CheckPartRecord = this.cmbLabSample.Text;
                        order.Sample = this.cmbLabSample.SelectedItem;
                    }
                    if (this.cmbCheckPart.SelectedItem != null)
                    {
                        order.CheckPartRecord = this.cmbCheckPart.Text;
                        order.Sample = this.cmbCheckPart.SelectedItem;
                    }

                    order.Frequency = SOC.HISFC.BizProcess.Cache.Order.GetFrequency("QD");
                    
                    //order.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.dtBegin.Text);

                    this.alOrder.Add(order);
                }
                else if (this.InOutType == 1)
                {
                    FS.HISFC.Models.Order.OutPatient.Order order;

                    order = new FS.HISFC.Models.Order.OutPatient.Order();
                    //order.Item = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Fee.Item.Undrug;
                    FS.HISFC.Models.Fee.Item.Undrug undrugTag = this.hprUndrug.GetObjectFromID(this.neuSpread1_Sheet1.Cells[i, (int)Col.ColCode].Text) as FS.HISFC.Models.Fee.Item.Undrug;
                    if (undrugTag == null)
                    {
                        continue;
                    }
                    order.Item = undrugTag;
                    if (order.Item == null)
                        continue;
                    //患者信息
                    order.Patient = this.patient;

                    //医嘱组合号
                    order.Combo.ID = comboID;

                    //医嘱类型
                    //order.OrderType = this.hprOrderType.GetObjectFromID("MZ") as FS.HISFC.Models.Order.OrderType;
                    

                    //单位 {AE53ACB5-3684-42e8-BF28-88C2B4FF2360}
                    order.Unit = order.Item.PriceUnit;

                    //煎药方式
                    order.Memo = this.txtMemo.Text;
                    
                    order.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.txtQty.Text);

                    order.ExeDept = this.cmbExecDept.SelectedItem;

                    if (this.cmbLabSample.SelectedItem != null)
                    {
                        order.CheckPartRecord = this.cmbLabSample.Text;
                        order.Sample = this.cmbLabSample.SelectedItem;
                    }
                    if (this.cmbCheckPart.SelectedItem != null)
                    {
                        order.CheckPartRecord = this.cmbCheckPart.Text;
                        order.Sample = this.cmbCheckPart.SelectedItem;
                    }

                    //order.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.dtBegin.Text);
                    //if (this.dtEnd.Checked)
                    //    order.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(this.dtEnd.Text);

                    this.alOrder.Add(order);
                }
            }
            return 1;
        }

        /// <summary>
        /// 根据非药品获取执行科室
        /// </summary>
        /// <param name="undrugTmp"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private ArrayList GetExecDept(FS.HISFC.Models.Fee.Item.Undrug undrugTmp, string type)
        {
            if (this.dctExecDepts.ContainsKey(undrugTmp.ID))
            {
                return this.dctExecDepts[undrugTmp.ID];
            }

            ArrayList alDepts = CacheManager.FeeIntegrate.QueryDeptList(undrugTmp.ID, this.InOutType.ToString());
            if (alDepts == null || alDepts.Count == 0)
            {
                if (string.IsNullOrEmpty(undrugTmp.ExecDept))
                {
                    this.dctExecDepts.Add(undrugTmp.ID, SOC.HISFC.BizProcess.Cache.Common.GetDept());
                    return SOC.HISFC.BizProcess.Cache.Common.GetDept();
                }
                else
                {
                    FS.FrameWork.Models.NeuObject neuObj = SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(undrugTmp.ExecDept);
                    if (neuObj != null)
                    {
                        ArrayList alExecDepts = new ArrayList();
                        alExecDepts.Add(neuObj);
                        this.dctExecDepts.Add(undrugTmp.ID, alExecDepts);
                        return alExecDepts;
                    }
                }
            }
            else
            {
                this.dctExecDepts.Add(undrugTmp.ID, alDepts);
                return alDepts;
            }

            return null;
        }

        // <summary>
        /// 医嘱有效性检查
        /// </summary>
        /// <returns>无错误返回1 出错返回－1</returns>
        protected int Valid()
        {
            if (this.cmbExecDept.Tag == null)
            {
                MessageBox.Show("请选择执行科室！");
                return -1;
            }
            if (this.txtQty.Text == "")
            {
                MessageBox.Show("请输入数量!");
                this.txtQty.Focus();
                return -1;
            }
            if (this.txtQty.Text.Contains(".") || this.txtQty.Text.Contains("-"))
            {
                MessageBox.Show("数量不能为小数或者负数!");
                this.txtQty.Focus();
                return -1;
            }
            
            return 1;
        }


        /// <summary>
        /// 保存条件到ＸＭＬ文件
        /// </summary>
        private void SaveDefaultLevelClassToXml()
        {
            if (this.cmbItemClass.Tag == null)
            {
                return;
            }
            string strErr = "";
            FS.FrameWork.WinForms.Classes.Function.DefaultValueFilePath = Application.StartupPath + "\\HISDefaultValue.xml";
            FS.FrameWork.WinForms.Classes.Function.SaveDefaultValue("LevelOrder", "DefaultLevelClass", out strErr, this.cmbItemClass.Tag.ToString());
            if (strErr != "")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg(strErr, 22);
            }
        }
        /// <summary>
        /// 从ＸＭＬ文件中读取
        /// </summary>
        private void LoadDefaultLevelClassFromXml()
        {
            string strErr = "";
            FS.FrameWork.WinForms.Classes.Function.DefaultValueFilePath = Application.StartupPath + "\\HISDefaultValue.xml";
            ArrayList a1 = FS.FrameWork.WinForms.Classes.Function.GetDefaultValue("LevelOrder", "DefaultLevelClass", out strErr);
            if (a1 != null && a1.Count > 0)
            {
                this.cmbItemClass.Tag = a1[0].ToString();
            }
        }
        #endregion

        #region  事件
        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                //分类
                ArrayList alLevelClass = CacheManager.GetConList("LEVELCLASS");
                this.cmbItemClass.AddItems(alLevelClass);
                //树
                this.tvItemLevel1.AfterSelect += new TreeViewEventHandler(tvItemLevel1_AfterSelect);
                this.tvItemLevel1.IsEdit = true;

                //样本
                ArrayList alLabSample = CacheManager.GetConList("LABSAMPLE");
                this.cmbLabSample.AddItems(alLabSample);
                //部位
                ArrayList alCheckPart = CacheManager.GetConList("CHECKPART");
                this.cmbCheckPart.AddItems(alCheckPart);
                //默认数量为1
                this.txtQty.Text = "1";

                alOrderType = CacheManager.InterMgr.QueryOrderTypeList();//医嘱类型
                this.hprOrderType.ArrayObject = alOrderType;

                InitDataTable();

                this.LoadDefaultLevelClassFromXml();
            }            
        }

        private void tvItemLevel1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.tvItemLevel1.SelectedNode.Tag == null)
            {
                return;
            }
            this.neuSpread1_Sheet1.RowCount = 0;
            this.dt.Clear();
            ArrayList alItemLevel = this.itemLevelManager.GetAllItemByFolderID((this.tvItemLevel1.SelectedNode.Tag as FS.HISFC.Models.Fee.Item.ItemLevel).ID);
            if (alItemLevel != null)
            {
                foreach (FS.HISFC.Models.Fee.Item.ItemLevel itemLevel in alItemLevel)
                {
                    FS.HISFC.Models.Fee.Item.Undrug undrug = new FS.HISFC.Models.Fee.Item.Undrug();
                    if (this.hprUndrug.GetObjectFromID(itemLevel.ID) == null)
                    {
                        undrug = CacheManager.FeeIntegrate.GetItem(itemLevel.ID);
                        this.hprUndrug.ArrayObject.Add(undrug);
                    }
                    else
                    {
                        undrug = this.hprUndrug.GetObjectFromID(itemLevel.ID) as FS.HISFC.Models.Fee.Item.Undrug;
                    }
                    if (undrug == null)
                    {
                        MessageBox.Show("没有查询到编码为" + itemLevel.ID + "的"  + itemLevel.Name + "项目");
                        continue;
                    }
                    if (undrug.ValidState != "1")
                    {
                        MessageBox.Show("编码为" + itemLevel.ID + "的" + itemLevel.Name + "项目已经失效！");
                        continue;
                    }
                    //this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
                    //int lastRowIndex = this.neuSpread1_Sheet1.RowCount - 1;
                    //this.neuSpread1_Sheet1.Rows[lastRowIndex].Tag = undrug;
                    //this.neuSpread1_Sheet1.Cells[lastRowIndex, (int)Col.ColCode].Text = undrug.ID;
                    //this.neuSpread1_Sheet1.Cells[lastRowIndex, (int)Col.ColName].Text = undrug.Name;
                    //this.neuSpread1_Sheet1.Cells[lastRowIndex, (int)Col.ColSpec].Text = undrug.Specs;
                    //this.neuSpread1_Sheet1.Cells[lastRowIndex, (int)Col.ColPrice].Text = undrug.Price.ToString();
                    //this.neuSpread1_Sheet1.Cells[lastRowIndex, (int)Col.ColUnit].Text = undrug.PriceUnit;
                    //this.neuSpread1_Sheet1.Cells[lastRowIndex, (int)Col.ColSortID].Text = itemLevel.SortID.ToString();
                    //this.neuSpread1_Sheet1.Cells[lastRowIndex, (int)Col.ColSpellCode].Text = undrug.SpellCode;
                    //this.neuSpread1_Sheet1.Cells[lastRowIndex, (int)Col.ColWBCode].Text = undrug.WBCode;
                    this.dt.Rows.Add(false, undrug.ID, undrug.Name, undrug.Specs, undrug.Price.ToString(), undrug.PriceUnit, itemLevel.SortID.ToString(), undrug.SpellCode, undrug.WBCode);

                }
            }
        }

        private void cmbItemClass_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.cmbItemClass.Tag != null && this.cmbItemClass.SelectedItem != null)
            {
                this.tvItemLevel1.LevelClass = this.cmbItemClass.SelectedItem as FS.FrameWork.Models.NeuObject;
                this.tvItemLevel1.RefreshGroupByClass();

                this.SaveDefaultLevelClassToXml();
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtFilter.Text.Trim()))
            {
                dv.RowFilter = "1=1";
            }
            else
            {
                dv.RowFilter = "(项目名称 like '%" + txtFilter.Text.Trim().ToUpper() + "%') or (拼音码 like '%" + txtFilter.Text.Trim().ToUpper() + "%') or (五笔码 like '%" + txtFilter.Text.Trim().ToUpper() + "%')";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.Save() > 0)
            {
                if (this.ParentForm != null)
                {
                    this.ParentForm.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.AlOrder = new ArrayList();
            if (this.ParentForm != null)
            {
                this.ParentForm.Close();
            }
        }
        #endregion

        #region 列枚举
        protected enum Col
        {
            ColCheck,
            ColCode,
            ColName,
            ColSpec,
            ColPrice,
            ColUnit,
            ColSortID,
            ColSpellCode,
            ColWBCode
        }
        #endregion

        private void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == (int)Col.ColCheck)
            {
                //FS.HISFC.Models.Fee.Item.Undrug undrugTmp = this.neuSpread1_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.Fee.Item.Undrug;
                FS.HISFC.Models.Fee.Item.Undrug undrugTmp = this.hprUndrug.GetObjectFromID(this.neuSpread1_Sheet1.Cells[e.Row, (int)Col.ColCode].Text) as FS.HISFC.Models.Fee.Item.Undrug;
                if (undrugTmp == null)
                {
                    return;
                }

                ArrayList alItemDepts = GetExecDept(undrugTmp, this.InOutType.ToString());

                if (FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Value))
                {
                    
                    if (cmbExecDept.alItems == null || cmbExecDept.alItems.Count == 0)
                    {
                        if (alItemDepts != null)
                        {
                            this.cmbExecDept.AddItems(alItemDepts);
                            this.cmbExecDept.SelectedIndex = -1;
                            this.cmbExecDept.Tag = null;
                        }
                    }
                    else
                    {
                        ArrayList alCurrDepts = this.cmbExecDept.alItems;
                        ArrayList alResultDepts = new ArrayList();
                        foreach (FS.FrameWork.Models.NeuObject currDept in alCurrDepts)
                        {
                            foreach (FS.FrameWork.Models.NeuObject execDept in alItemDepts)
                            {
                                if (currDept.ID == execDept.ID)
                                {
                                    alResultDepts.Add(currDept);
                                }
                            }
                        }

                        if (alResultDepts.Count == 0)
                        {
                            MessageBox.Show("所选项目与已选项目不在同一执行科室执行！请分开开立！");
                            this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Value = false;
                        }
                        else
                        {
                            this.cmbExecDept.AddItems(alResultDepts);
                        }
                    }
                }
                else
                {
                    if (alItemDepts == SOC.HISFC.BizProcess.Cache.Common.GetDept())
                    {
                        return;
                    }

                    this.cmbExecDept.ClearItems();
                    for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                    {
                        if (!FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)Col.ColCheck].Value))
                        {
                            return;
                        }

                        //FS.HISFC.Models.Fee.Item.Undrug undrugTag = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Fee.Item.Undrug;
                        FS.HISFC.Models.Fee.Item.Undrug undrugTag = this.hprUndrug.GetObjectFromID(this.neuSpread1_Sheet1.Cells[i, (int)Col.ColCode].Text) as FS.HISFC.Models.Fee.Item.Undrug;
                        if (undrugTag == null)
                        {
                            continue;
                        }
                        ArrayList alUndrugDept = GetExecDept(undrugTag, this.InOutType.ToString());

                        if (cmbExecDept.Items == null || cmbExecDept.Items.Count == 0)
                        {
                            this.cmbExecDept.AddItems(alUndrugDept);
                            continue;
                        }

                        ArrayList alCurrDepts = this.cmbExecDept.alItems;
                        ArrayList alResultDepts = new ArrayList();
                        foreach (FS.FrameWork.Models.NeuObject currDept in alCurrDepts)
                        {
                            foreach (FS.FrameWork.Models.NeuObject execDept in alUndrugDept)
                            {
                                if (currDept.ID == execDept.ID)
                                {
                                    alResultDepts.Add(currDept);
                                }
                            }
                        }
                        this.cmbExecDept.AddItems(alResultDepts);
                    }
                }

                if (this.cmbExecDept.alItems != null && this.cmbExecDept.alItems.Count == 1)
                {
                    this.cmbExecDept.Tag = (this.cmbExecDept.alItems[0] as FS.FrameWork.Models.NeuObject).ID;
                }
            }
        }
        
        
    }
}
