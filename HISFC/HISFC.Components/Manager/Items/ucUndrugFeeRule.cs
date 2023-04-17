using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Manager.Items
{
    public partial class ucUndrugFeeRule : FS.HISFC.Components.Manager.Items.ucUnDrugItems
    {
        public ucUndrugFeeRule()
        {
            InitializeComponent();
        }

        #region 域变量

        private HISFC.BizLogic.Fee.UndrugFeeRegularManager feeRegularManager = new FS.HISFC.BizLogic.Fee.UndrugFeeRegularManager();

        private FS.HISFC.BizLogic.Manager.Constant conManager = new FS.HISFC.BizLogic.Manager.Constant();

        private FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();

        private Hashtable feeTypeHash = new Hashtable();

        private Hashtable itemHash = new Hashtable();

        private FS.HISFC.Components.Manager.Items.ucHandleFeeRule ucHandleFeeRule = null;

        private DataTable dtFeeRegular = null;

        private string FilterAll = "";

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #endregion

        #region 方法

        /// <summary>
        /// 填充 收费规则
        /// </summary>
        private void FillFeeRule()
        {
            ArrayList feeRule = new ArrayList();

            //feeRule = this.conManager.GetAllList("FEEREGULAR");
            feeRule.AddRange(new FS.FrameWork.Models.NeuObject[] { new FS.FrameWork.Models.NeuObject("02", "按数收费,每天不超过即定次数", "按数收费,每天不超过即定次数"), new FS.FrameWork.Models.NeuObject("03", "每天多种同类项目不能超过即定数量", "每天多种同类项目不能超过即定数量"), new FS.FrameWork.Models.NeuObject("04", "项目互斥", "项目互斥"),new FS.FrameWork.Models.NeuObject("05","按条件项目互斥","当数量大于指定值或星期大于指定值则互斥") });

            foreach (FS.FrameWork.Models.NeuObject tempObj in feeRule)
            {
                //初始化收费规则哈希表
                this.feeTypeHash.Add(tempObj.ID, tempObj);
            }

            if (this.itemHash.Count == 0)
            {
                foreach (FS.HISFC.Models.Fee.Item.Undrug undrug in base.undrugList)
                {
                    if (this.itemHash.ContainsKey(undrug.ID) == false)
                    {
                        this.itemHash.Add(undrug.ID, undrug);
                    }
                }
            }
        }

        private void InitFeeRule()
        {
            ArrayList al = this.feeRegularManager.QueryAllFeeRegular();
            if (al == null)
            {
                MessageBox.Show(this, "获取收费规则信息出错！" + this.feeRegularManager.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.AddToDataTable(al);
        }

        private void InitDataTable()
        {
            dtFeeRegular = new DataTable();

            dtFeeRegular.Columns.AddRange(new DataColumn[] { 
                    new DataColumn("规则编码",typeof(System.String)),
                    new DataColumn("项目编码",typeof(System.String)),
                    new DataColumn("自定义码",typeof(System.String)),
                    new DataColumn("项目名称",typeof(System.String)),
                    new DataColumn("限制条件",typeof(System.String)),
                    new DataColumn("限制类型",typeof(System.String)),
                    new DataColumn("限制数量",typeof(System.String)),
                    new DataColumn("限制项目",typeof(System.String)),
                    new DataColumn("操作员",typeof(System.String)),
                    new DataColumn("操作时间",typeof(System.DateTime)),
                    new DataColumn("备注",typeof(System.String))
                            });
            this.dtFeeRegular.DefaultView.Sort = "项目编码 DESC";
            this.neuSpread1_Sheet1.DataSource = dtFeeRegular.DefaultView;

        }

        private void AddToDataTable(ArrayList al)
        {
            if (al == null)
            {
                return;
            }

            this.InitDataTable();

            foreach (FS.HISFC.Models.Fee.Item.UndrugFeeRegular feeRegular in al)
            {
                DataRow dr = this.dtFeeRegular.NewRow();

                FS.HISFC.Models.Fee.Item.Undrug undrug = itemHash[feeRegular.Item.ID] as FS.HISFC.Models.Fee.Item.Undrug;
                if (string.IsNullOrEmpty(undrug.UserCode))
                {
                    MessageBox.Show("自定义码为空：项目名称：" + feeRegular.Item.Name);
                    //return;
                }
                dr["规则编码"] = feeRegular.ID;
                dr["项目编码"] = feeRegular.Item.ID;
                dr["自定义码"] = undrug.UserCode;
                dr["项目名称"] = feeRegular.Item.Name;
                if (FS.FrameWork.Function.NConvert.ToInt32(feeRegular.LimitCondition) == 1)
                {
                    dr["限制条件"] = "按时间";
                }
                else
                {
                    dr["限制条件"] = "按数量";
                }
                dr["限制类型"] = ((FS.FrameWork.Models.NeuObject)this.feeTypeHash[feeRegular.Regular.ID]).Name;
                dr["限制数量"] = feeRegular.DayLimit.ToString();
                string[] mutex = feeRegular.LimitItem.ID.Split('|');

                string itemListStr = "";

                if (mutex != null && mutex.Length > 0)
                {
                    for (int j = 0; j < mutex.Length; j++)
                    {
                        if (string.IsNullOrEmpty(mutex[j].ToString()))
                        {
                            continue;
                        }
                        //查询非药品信息的名称
                        if (itemHash.ContainsKey(mutex[j].ToString()))
                        {
                            itemListStr += itemHash[mutex[j].ToString()].ToString() + ",";
                        }
                        else
                        {
                            FS.HISFC.Models.Fee.Item.Undrug undrugTemp = this.itemManager.GetValidItemByUndrugCode(mutex[j].ToString());
                            if (undrugTemp == null)
                            {
                                MessageBox.Show(this, "获取非药品信息出错！" + this.itemManager.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            itemListStr += undrugTemp.Name + ",";
                            this.itemHash.Add(undrugTemp.ID, undrugTemp);
                        }

                    }

                    if (string.IsNullOrEmpty(itemListStr) == false)
                    {
                        itemListStr = itemListStr.Substring(0, itemListStr.Length - 1);
                    }
                }
                dr["限制项目"] = itemListStr;
                dr["操作员"] = feeRegular.Oper.ID;
                dr["操作时间"] = feeRegular.Oper.OperTime.ToString();
                dr["备注"] = feeRegular.Memo;

                this.dtFeeRegular.Rows.Add(dr);

            }

            this.neuSpread1_Sheet1.Columns[1].Visible = false;

            this.dtFeeRegular.AcceptChanges();
        }

        private void AddRegular()
        {
            if (base.neuSpreadItems.ActiveSheet.ActiveRowIndex < 0)
            {
                return;
            }

            if (ucHandleFeeRule == null)
            {
                ucHandleFeeRule = new ucHandleFeeRule();

                this.ucHandleFeeRule.feeTypeHash = this.feeTypeHash;
                this.ucHandleFeeRule.itemHash = this.itemHash;

                this.ucHandleFeeRule.InitFeeRegular(new ArrayList(base.undrugList));

            }
            this.ucHandleFeeRule.operMode = 0;
            FS.HISFC.Models.Fee.Item.UndrugFeeRegular undrugFeeRegular = new FS.HISFC.Models.Fee.Item.UndrugFeeRegular();
            undrugFeeRegular.Item.ID = base.neuSpreadItems.ActiveSheet.GetText(base.neuSpreadItems.ActiveSheet.ActiveRowIndex, base.GetCloumn("项目编号"));
            undrugFeeRegular.Item.Name = base.neuSpreadItems.ActiveSheet.GetText(base.neuSpreadItems.ActiveSheet.ActiveRowIndex, base.GetCloumn("项目名称"));
            this.ucHandleFeeRule.SetFeeRegular(undrugFeeRegular);
            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "收费规则明细";
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucHandleFeeRule, FormBorderStyle.FixedDialog);

            //刷新界面
            this.InitFeeRule();
            this.FilterRegular();
        }

        private void DeleteRegular()
        {
            if (this.neuSpread1.ActiveSheet.ActiveRowIndex < 0)
            {
                return;
            }
            if (string.IsNullOrEmpty(this.neuSpread1.ActiveSheet.Cells[this.neuSpread1.ActiveSheet.ActiveRowIndex, 0].Text) == false)
            {
                if (feeRegularManager.DeleteUndrugFeeRegular(this.neuSpread1.ActiveSheet.Cells[this.neuSpread1.ActiveSheet.ActiveRowIndex, 0].Text) <= 0)
                {
                    MessageBox.Show(this, "删除出错！" + this.feeRegularManager.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                MessageBox.Show(this, "删除成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.neuSpread1_Sheet1.RemoveRows(this.neuSpread1.ActiveSheet.ActiveRowIndex, 1);
                this.dtFeeRegular.AcceptChanges();
            }
        }

        private void FilterRegular()
        {

            DataTable dtTemp = base.dvUndrugItem.ToTable();

            if (dtTemp == null)
            {
                return;
            }

            string filter = "";
            if (dtTemp.Rows.Count == base.dvUndrugItem.Table.Rows.Count)
            {
                filter = this.FilterAll;
            }
            else
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    filter += "'" + dr["项目编号"].ToString() + "',";
                }

                if (string.IsNullOrEmpty(filter) == false)
                {
                    filter = filter.Substring(0, filter.Length - 1);
                }
                else
                {
                    filter = "''";
                }
            }

            this.dtFeeRegular.DefaultView.RowFilter = "项目编码 in (" + filter + ")";
        }

        #endregion

        #region 事件

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ////删除双击事件
            base.neuSpreadItems.CellDoubleClick -= new FarPoint.Win.Spread.CellClickEventHandler(base.neuSpreadItems_CellDoubleClick);

            ////增加单击事件
            //this.ucUnDrugItems1.neuSpreadItems.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(neuSpreadItems_CellClick);

            this.FillFeeRule();
            this.InitFeeRule();

            DataTable dtTemp = base.dvUndrugItem.Table;

            if (dtTemp == null)
            {
                return;
            }

            this.FilterAll = "";
            foreach (DataRow dr in dtTemp.Rows)
            {
                this.FilterAll += "'" + dr["项目编号"].ToString() + "',";
            }
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService = base.OnInit(sender, neuObject, param);

            this.toolBarService.AddToolButton("添加收费规则", "添加收费规则", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);
            this.toolBarService.AddToolButton("删除收费规则", "删除收费规则", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            base.ToolStrip_ItemClicked(sender, e);

            switch (e.ClickedItem.Text)
            {
                case "添加收费规则":
                    this.AddRegular();
                    break;
                case "删除收费规则":
                    this.DeleteRegular();
                    break;
                default:
                    break;
            }
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Row < 0)
            {
                return;
            }

            if (ucHandleFeeRule == null)
            {
                ucHandleFeeRule = new ucHandleFeeRule();
                this.ucHandleFeeRule.feeTypeHash = this.feeTypeHash;
                this.ucHandleFeeRule.itemHash = this.itemHash;

                this.ucHandleFeeRule.InitFeeRegular(new ArrayList(base.undrugList));
            }
            FS.HISFC.Models.Fee.Item.UndrugFeeRegular undrugFeeRegular = this.feeRegularManager.GetSingleFeeRegularById(this.neuSpread1_Sheet1.Cells[e.Row, 0].Text);
            if (undrugFeeRegular == null)
            {
                MessageBox.Show(this, "获取收费规则信息出错！" + this.feeRegularManager.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.ucHandleFeeRule.operMode = 1;
            this.ucHandleFeeRule.SetFeeRegular(undrugFeeRegular);
            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "收费规则明细";
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucHandleFeeRule, FormBorderStyle.FixedDialog);

            //刷新界面
            this.InitFeeRule();
            this.FilterRegular();
        }

        protected override void neuSpreadItems_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            base.neuSpreadItems_CellClick(sender, e);

            if (e.Row >= 0)
            {
                this.dtFeeRegular.DefaultView.RowFilter = "项目编码 in ('" + base.neuSpreadItems.ActiveSheet.GetText(e.Row, base.GetCloumn("项目编号")) + "')";
            }
        }

        protected override void GenerateRowFilter(string whereValue, string sct, string sft, string state, string tag, string itemType)
        {
            base.GenerateRowFilter(whereValue, sct, sft, state, tag, itemType);

            if (string.IsNullOrEmpty(whereValue) && string.IsNullOrEmpty(sct) && string.IsNullOrEmpty(sft))
            {
                this.dtFeeRegular.DefaultView.RowFilter = "项目编码 in (" + this.FilterAll + ")";
            }
            else
            {
                this.FilterRegular();
            }
        }

        #endregion
    }
}
