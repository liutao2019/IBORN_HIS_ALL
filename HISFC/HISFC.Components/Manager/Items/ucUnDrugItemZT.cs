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
    public partial class ucUnDrugItemZT : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private TreeNode node = new TreeNode();
        private DataView dvItems = new DataView();
        private FS.HISFC.BizLogic.Manager.UndrugztManager ztManager = new FS.HISFC.BizLogic.Manager.UndrugztManager();
        /// <summary>
        /// {21267B78-C198-43ed-8C52-5364C6F70FDA}
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();

        //{933F5263-3408-4ccd-A2A6-4C3693A9D10C}
        private bool isDeptFilter = false;      

        [Category("项目设置"), Description("是否按照登陆科室过滤项目")]
        public bool IsDeptFilter
        {
            get { return isDeptFilter; }
            set { isDeptFilter = value; }
        }

        public ucUnDrugItemZT()
        {
            InitializeComponent();
            this.SpreadDetails.EditModeOff += new System.EventHandler(this.SpreadDetails_EditModeOff);
        }

        private void ucUnDrugItemZT_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.FillTree() != 1)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("非药品项目组套树初始化失败"));
                    return;
                }
                
                if (this.FillFpItems() != 1)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("非药品项目列表初始化失败"));
                    return;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveButtonHandler();
            this.node.Tag = null;
            return 1;
        }

        private bool Valid()
        {
            for (int i = 0; i < this.fpDetails.Rows.Count; i++)
            {
                if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpDetails.GetText(i, 11)) <= 0)
                {
                    MessageBox.Show("第" + (i + 1).ToString() + "数量必须是大于零的数，请重新输入！");
                    return false;
                }
            }
            return true;
        }
        private void SaveButtonHandler()
        {
            if (this.fpDetails.Rows.Count == 0)
            {
                return;
            }
            if (!Valid()) return;
            decimal ChildPrice=0;
            decimal Price=0;　
            decimal SpecialPrice = 0; //特诊
            //{67BF98D0-3FF2-4ff6-ACF9-F06C4A601C6A}
            this.SpreadDetails.StopCellEditing();//更新cell值

            List<FS.HISFC.Models.Fee.Item.UndrugComb> lstUndrug = new List<FS.HISFC.Models.Fee.Item.UndrugComb>();
            for (int i = 0, j = this.fpDetails.Rows.Count; i < j; i++)
            {
                FS.HISFC.Models.Fee.Item.UndrugComb zt = new FS.HISFC.Models.Fee.Item.UndrugComb();
                zt.Package.ID = this.fpDetails.GetText(i, 0).Trim();
                zt.Package.Name = this.fpDetails.GetText(i, 1).Trim();
                zt.ID = this.fpDetails.GetText(i, 2).Trim();
                zt.Name = this.fpDetails.GetText(i, 3).Trim();
                zt.SortID = Convert.ToInt32(this.fpDetails.GetText(i, 4));
                zt.Qty = Convert.ToDecimal(this.fpDetails.GetText(i, 5));
                if (this.fpDetails.GetText(i, 6).Trim() == "")
                {
                    zt.ValidState = "1";
                }
                else
                {
                    zt.ValidState = this.fpDetails.GetText(i, 6).Trim() == "有效" ? "1" : "0";
                }
                zt.SpellCode = this.fpDetails.GetText(i, 7);
                zt.WBCode = this.fpDetails.GetText(i, 8);
                zt.UserCode = this.fpDetails.GetText(i, 9);
                zt.Memo = this.fpDetails.GetText(i, 10);
                //{67BF98D0-3FF2-4ff6-ACF9-F06C4A601C6A}
                if (zt.ValidState == "1")//增加判断,如果该条项目有效,才累加单价
                {
                    Price += zt.Qty * FS.FrameWork.Function.NConvert.ToDecimal(this.fpDetails.GetText(i, 11));
                    ChildPrice += zt.Qty * FS.FrameWork.Function.NConvert.ToDecimal(this.fpDetails.GetText(i, 12));
                    SpecialPrice += zt.Qty * FS.FrameWork.Function.NConvert.ToDecimal(this.fpDetails.GetText(i, 13));
                }
                lstUndrug.Add(zt);
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(ztManager.Connection);
            //trans.BeginTransaction();

            ztManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                if (this.ztManager.SaveUndrugzt(lstUndrug) != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存数据失败"));
                    return;
                }
                //string itemCode = ((FS.HISFC.Models.Fee.Item.Undrug)this.node.Tag).ID;
                string itemCode = ((FS.HISFC.Models.Fee.Item.Undrug)this.tvUndrugzt.SelectedNode.Tag).ID;
                if (this.ztManager.UpdateUndrugztPrice(itemCode, Price, ChildPrice, SpecialPrice) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存数据失败"));
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存数据成功"));
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();;
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存数据失败"));
            }
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            try
            {
                //{930860AF-89F2-4503-831E-FEAF7CF21B52}
                List<FS.HISFC.Models.Fee.Item.UndrugComb> printList = new List<FS.HISFC.Models.Fee.Item.UndrugComb>();
                List<FS.HISFC.Models.Fee.Item.UndrugComb> itemList = new List<FS.HISFC.Models.Fee.Item.UndrugComb>();
                if (this.node.Tag == null)//选择了根节点
                {
                    foreach (TreeNode e in this.tvUndrugzt.Nodes[0].Nodes)
                    {                        
                        FS.HISFC.Models.Fee.Item.Undrug itemObj = e.Tag as FS.HISFC.Models.Fee.Item.Undrug;
                            this.ztManager.QueryUnDrugztDetail(itemObj.ID, ref itemList);
                            if (itemList.Count != 0)
                            {
                                printList.AddRange(itemList);
                            }
                    }
                }
                else//选择某一个项目
                {
                    FS.HISFC.Models.Fee.Item.Undrug itemObj = this.node.Tag as FS.HISFC.Models.Fee.Item.Undrug;
                    this.ztManager.QueryUnDrugztDetail(itemObj.ID, ref itemList);
                    if (itemList.Count != 0)
                    {
                        printList.AddRange(itemList);
                    }
                }
                ucUnDrugItemZTPrint ucPrint = new ucUnDrugItemZTPrint();
                ucPrint.SetValue(printList);
                ucPrint.Print();
                //FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                //p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
                //p.PrintPreview(this.neuPanel3);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            return base.OnPrint(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            this.ExportInfo();
            return base.Export(sender, neuObject);
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        /// <returns>1,成功</returns>
        private int FillTree()
        {
            List<FS.HISFC.Models.Fee.Item.Undrug> lstUndrug = new List<FS.HISFC.Models.Fee.Item.Undrug>();

            if (this.IsDeptFilter)//{933F5263-3408-4ccd-A2A6-4C3693A9D10C}
            {
                #region 按照科室过滤的模式
                if (this.itemManager.QueryAllValidItemztAllDepts(ref lstUndrug) == -1)
                {
                    return -1;
                }
                if (lstUndrug.Count == 0)
                {
                    return -1;
                }
                Dictionary<string, string> dict = new Dictionary<string, string>();
                FS.HISFC.Models.Base.Employee e = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                for (int j = 0; j < lstUndrug.Count; j++)
                {

                    if (dict.ContainsKey(lstUndrug[j].ID))
                    {
                        dict[lstUndrug[j].ID] += "|" + lstUndrug[j].SpecialDept.ID;
                        lstUndrug.Remove(lstUndrug[j]);
                        j--;
                    }
                    else
                    {
                        if (lstUndrug[j].SpecialDept.ID != "")
                        {
                            dict.Add(lstUndrug[j].ID, lstUndrug[j].ExecDept + "|" + lstUndrug[j].SpecialDept.ID);
                        }
                        else
                        {
                            dict.Add(lstUndrug[j].ID, lstUndrug[j].ExecDept);
                        }
                    }
                }
                for (int j = 0; j < lstUndrug.Count; j++)
                {
                    string deptID = dict[lstUndrug[j].ID];
                    if (deptID != "")//非药品多科室维护了||执行科室维护了
                    {
                        if (!deptID.Contains(e.Dept.ID))
                        {
                            lstUndrug.Remove(lstUndrug[j]);
                            j--;
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region 原来的模式
                if (this.ztManager.QueryAllValidItemzt(ref lstUndrug) == -1)
                {
                    return -1;
                }
                if (lstUndrug.Count == 0)
                {
                    return -1;
                }
                #endregion
            }

            TreeNode root = new TreeNode("非药品组套列表");
            this.tvUndrugzt.Nodes.Add(root);

            for (int i = 0, j = lstUndrug.Count; i < j; i++)
            {
                TreeNode one = new TreeNode("("+lstUndrug[i].UserCode+")"+lstUndrug[i].Name);//节点名称为项目名称
                one.Tag = lstUndrug[i];
                root.Nodes.Add(one);
            }
            this.tvUndrugzt.ExpandAll();
            return 1;
        }

        private int FillFpItems()
        {
            if (this.ztManager.QueryAllValidItems(ref dvItems) == -1)
            {
                //MessageBox.Show(FS.FrameWork.Management.Language.Msg("获取数据失败"));
                return -1;
            }

            this.fpItems.DataSource = dvItems;
            //this.SetItemFarpoint();
            return 1;
        }        

        private int FillFpDetails(string pcode, string pname)
        {
            if (this.fpDetails.Rows.Count > 0)
            {
                this.fpDetails.Rows.Remove(0, this.fpDetails.Rows.Count);
            }

            List<FS.HISFC.Models.Fee.Item.UndrugComb> lstzt = new List<FS.HISFC.Models.Fee.Item.UndrugComb>();
            if (this.ztManager.QueryUnDrugztDetail(pcode, ref lstzt) == -1)
            {
                return -1;
            }

            if (lstzt.Count == 0)
            {
                return -1;
            }

            

            for (int i = 0, j = lstzt.Count; i < j; i++)
            {
               

               

                this.fpDetails.Rows.Add(i, 1);
                //{21267B78-C198-43ed-8C52-5364C6F70FDA}
                FS.HISFC.Models.Fee.Item.Undrug item = this.itemManager.GetUndrugByCode(lstzt[i].ID);
                if (item.ValidState == "0")
                {
                    this.fpDetails.RowHeader.Cells[i, 0].Text = "帐页停用";
                    this.fpDetails.RowHeader.Cells[i, 0].ForeColor = Color.Red;
                }

                if (item.ValidState == "2")
                {
                    this.fpDetails.RowHeader.Cells[i, 0].Text = "帐页废弃";
                    this.fpDetails.RowHeader.Cells[i, 0].ForeColor = Color.Red;
                }

                if (item.ValidState == "1")
                {
                    this.fpDetails.RowHeader.Cells[i, 0].Text = "";
                    this.fpDetails.RowHeader.Cells[i, 0].ForeColor = this.fpDetails.ColumnHeader.Cells[0, 0].ForeColor;
                }

                this.fpDetails.SetText(i, 0, lstzt[i].Package.ID);
                this.fpDetails.SetText(i, 1, lstzt[i].Package.Name);
                this.fpDetails.SetText(i, 2, lstzt[i].ID);
                this.fpDetails.SetText(i, 3, lstzt[i].Name);
                this.fpDetails.SetText(i, 4, lstzt[i].SortID.ToString());
                this.fpDetails.SetText(i, 5, lstzt[i].Qty.ToString());
                this.fpDetails.SetText(i, 6, lstzt[i].ValidState);
                this.fpDetails.SetText(i, 7, lstzt[i].SpellCode);
                this.fpDetails.SetText(i, 8, lstzt[i].WBCode);
                this.fpDetails.SetText(i, 9, lstzt[i].UserCode);

                this.fpDetails.SetText(i, 10, lstzt[i].Memo);//标志位,如果有值则为更新,否则为插入
                this.fpDetails.SetText(i, 11, lstzt[i].Price.ToString());
                this.fpDetails.SetText(i, 12, lstzt[i].ChildPrice.ToString());
                this.fpDetails.SetText(i, 13, lstzt[i].SpecialPrice.ToString());
                //{C030AC2E-6161-44a8-B82C-BF152B4FF426}
                this.fpDetails.SetText(i, 14, lstzt[i].Oper.Name);
                this.fpDetails.SetText(i, 15, lstzt[i].Oper.OperTime.ToString());
            }
            return 1;            
        }

        private void tvUndrugzt_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
        }

        private void SpreadItems_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
            {
                return;
            }
            if (this.node.Tag == null)
            {
                MessageBox.Show("请选择所要维护的项目！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string pcode = ((FS.HISFC.Models.Fee.Item.Undrug)this.node.Tag).ID;
            string pname = ((FS.HISFC.Models.Fee.Item.Undrug)this.node.Tag).Name;

            FS.HISFC.Models.Fee.Item.UndrugComb undrugzt = new FS.HISFC.Models.Fee.Item.UndrugComb();
            undrugzt.Package.ID = pcode;
            undrugzt.Package.Name = pname;
            undrugzt.ID = this.fpItems.GetText(e.Row, 0);//以后把0改成名称
            undrugzt.Name = this.fpItems.GetText(e.Row, 1);//也一样
            undrugzt.SortID = 0;// Convert.ToInt32(this.fpItems.GetText(e.Row, 4));//也一样
            undrugzt.Qty = 1;// Convert.ToDecimal(this.fpItems.GetText(e.Row, 5));//也一样
            undrugzt.ValidState = "有效";// this.fpItems.GetText(e.Row, 6);//有效性
            undrugzt.SpellCode = this.fpItems.GetText(e.Row, 5);
            undrugzt.WBCode = this.fpItems.GetText(e.Row, 6);
            undrugzt.UserCode = this.fpItems.GetText(e.Row, 4);
            undrugzt.ChildPrice =FS.FrameWork.Function.NConvert.ToDecimal(this.fpItems.GetText(e.Row, 26)); //儿童
            undrugzt.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.fpItems.GetText(e.Row, 9));//三甲
            undrugzt.SpecialPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.fpItems.GetText(e.Row, 27));//特诊价
            undrugzt.Memo = "";// this.fpItems.GetText(e.Row, 10);//标志位
            if (this.IsNewLineExists(undrugzt.Package.ID, undrugzt.ID))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("组套明细已经存在"));
                return;
            }
            CreateNewLineInFpDetails(undrugzt);
            SumCost();
        }

        private void CreateNewLineInFpDetails(FS.HISFC.Models.Fee.Item.UndrugComb zt)
        {
            this.fpDetails.Rows.Add(0, 1);

            this.fpDetails.SetText(0, 0, zt.Package.ID);
            this.fpDetails.SetText(0, 1, zt.Package.Name);
            this.fpDetails.SetText(0, 2, zt.ID);
            this.fpDetails.SetText(0, 3, zt.Name);
            this.fpDetails.SetText(0, 4, zt.SortID.ToString());
            this.fpDetails.SetText(0, 5, zt.Qty.ToString());
            this.fpDetails.SetText(0, 6, zt.ValidState);
            this.fpDetails.SetText(0, 7, zt.SpellCode);
            this.fpDetails.SetText(0, 8, zt.WBCode);
            this.fpDetails.SetText(0, 9, zt.UserCode);
            this.fpDetails.SetText(0, 10, zt.Memo);//标志位,如果有值则为更新,否则为插入
            //this.fpDetails.SetText(0, 11, "1");//数量
            this.fpDetails.SetText(0, 11, zt.Price.ToString());//三甲
            this.fpDetails.SetText(0, 12, zt.ChildPrice.ToString());//儿童
            this.fpDetails.SetText(0, 13, zt.SpecialPrice.ToString());//特诊价
        }

        private bool IsNewLineExists(string packageid, string itemid)
        {
            for (int i = 0, j = this.fpDetails.Rows.Count; i < j; i++)
            {
                if( this.fpDetails.GetText(i, 0).Trim().Equals(packageid) && 
                    this.fpDetails.GetText(i, 2).Trim().Equals(itemid))
                {
                    return true;                    
                }
            }
            return false;
        }

        private void GenerateRowFilter(string whereValue)
        {
            this.dvItems.AllowDelete = true;
            this.dvItems.AllowEdit = true;
            this.dvItems.AllowNew = true;
            //if(whereValue == "")
            //{
            //    throw new Exception("查询条件不能为空!");
            //}
            StringBuilder builder = new StringBuilder();
            builder.Append("拼音码 like '%");
            builder.Append(whereValue);
            builder.Append("%' ");
            builder.Append(" or ");
            builder.Append("五笔码 like '%");
            builder.Append(whereValue);
            builder.Append("%' ");
            builder.Append(" or ");
            builder.Append("输入码 like '%");
            builder.Append(whereValue);
            builder.Append("%' ");
            builder.Append(" or ");
            builder.Append("国家编码 like '%");
            builder.Append(whereValue);
            builder.Append("%' ");
            builder.Append(" or ");
            builder.Append("国际编码 like '%");
            builder.Append(whereValue);
            builder.Append("%' ");
            builder.Append(" or ");
            builder.Append("名称 like '%");
            builder.Append(whereValue);
            builder.Append("%' ");
            this.dvItems.RowFilter = builder.ToString();
            this.dvItems.RowStateFilter = DataViewRowState.CurrentRows;
        }

        private void tbQueryCondition_TextChanged(object sender, EventArgs e)
        {
            GenerateRowFilter(this.tbQueryCondition.Text);
        }

        private void SetItemFarpoint()
        {
            this.fpItems.Columns["编码"].Visible = false;
            this.fpItems.Columns["系统类别"].Visible = false;
            this.fpItems.Columns["最小费用代码"].Visible = false;
            this.fpItems.Columns["默认价"].Visible = false;
            this.fpItems.Columns["急诊加成比例"].Visible = false;
            this.fpItems.Columns["计划生育标记"].Visible = false;
            this.fpItems.Columns["确认标志"].Visible = false;
            this.fpItems.Columns["规格"].Visible = false;
            this.fpItems.Columns["设备编号"].Visible = false;
            this.fpItems.Columns["默认检查部位或标本"].Visible = false;
            this.fpItems.Columns["手术编码"].Visible = false;
            this.fpItems.Columns["手术分类"].Visible = false;
            this.fpItems.Columns["手术规模"].Visible = false;
            this.fpItems.Columns["儿童价"].Visible = false;
            this.fpItems.Columns["特诊价"].Visible = false;
            this.fpItems.Columns["省限制"].Visible = false;
            this.fpItems.Columns["市限制"].Visible = false;
            this.fpItems.Columns["自费项目"].Visible = false;
            this.fpItems.Columns["特殊标识1"].Visible = false;
            this.fpItems.Columns["特殊标识2"].Visible = false;
            this.fpItems.Columns["单价2"].Visible = false;
            this.fpItems.Columns["单价3"].Visible = false;
            this.fpItems.Columns["疾病分类"].Visible = false;
            this.fpItems.Columns["专科名称"].Visible = false;
            this.fpItems.Columns["病史及检查"].Visible = false;
            this.fpItems.Columns["检查要求"].Visible = false;
            this.fpItems.Columns["注意事项"].Visible = false;
            this.fpItems.Columns["CONSENT_FLAG"].Visible = false;
            this.fpItems.Columns["检查申请单名称"].Visible = false;
            this.fpItems.Columns["是否预约"].Visible = false;
            this.fpItems.Columns["ITEM_AREA"].Visible = false;
            this.fpItems.Columns["ITEM_NOAREA"].Visible = false;
            this.fpItems.Columns["单位标识"].Visible = false;
        }

        /// <summary>
        /// 原来想实现修改哪行,保存哪行了,以后再写吧
        /// </summary>
        /// <param name="rowIndex">被修改行的索引</param>
        /// <returns>1,成功; -1,失败</returns>
        private int SaveChangedRows(int rowIndex)
        {
            return 1;
        }

        /// <summary>
        /// 导出
        /// </summary>
        private void ExportInfo()
        {
            bool tr = false;
            string fileName = "";
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "excel|*.xls";
            saveFile.Title = "导出到Excel";

            //saveFile.FileName = "复合项目" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString().Replace(':', '-');

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                if (saveFile.FileName.Trim() != "")
                {
                    fileName = saveFile.FileName;
                    tr = this.SpreadDetails.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                }
                else
                {
                    MessageBox.Show("文件名不能为空!");
                    return;
                }

                if (tr)
                {
                    MessageBox.Show("导出成功!");
                }
                else
                {
                    MessageBox.Show("导出失败!");
                }
            }
        }

        /// <summary>
        /// 没处理,是为和SaveChangedRows一起使用的
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDetails_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
        {
        }
        //复合项目查询的回车事件
        //{461CB381-B0F3-44ba-A006-D9050CE2C4F5}
        private void txtUndrugComb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                string nodeCode = this.txtUndrugComb.Text.Trim().ToString();
                TreeNode nodeZt = this.tvUndrugzt.Nodes[0];
                foreach (TreeNode node in nodeZt.Nodes)
                {
                    string undrugId = ((FS.HISFC.Models.Fee.Item.Undrug)node.Tag).ID;
                    string undrugName = ((FS.HISFC.Models.Fee.Item.Undrug)node.Tag).Name;
                    string undrugWbCode = ((FS.HISFC.Models.Fee.Item.Undrug)node.Tag).WBCode;
                    string undrugSpellCode = ((FS.HISFC.Models.Fee.Item.Undrug)node.Tag).SpellCode;
                    string undrugUserCode = ((FS.HISFC.Models.Fee.Item.Undrug)node.Tag).UserCode;
                    string undrugNaCode = ((FS.HISFC.Models.Fee.Item.Undrug)node.Tag).NationCode;
                    if (undrugSpellCode.Contains(nodeCode.ToUpper()) || undrugWbCode.Contains(nodeCode.ToUpper()) 
                        || undrugNaCode.Contains(nodeCode) || undrugUserCode.Contains(nodeCode) 
                        || (undrugName).Contains(nodeCode))
                    {
                        this.tvUndrugzt.SelectedNode = node;
                        break;
                    }
                    else
                    {
                        this.tvUndrugzt.SelectedNode = nodeZt;
                    }
                }
            }
        }

        private void tvUndrugzt_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null)
            {
                this.node.Tag = null;
                this.fpDetails.RowCount = 0;

                this.lblUserCode.Visible = false;
                this.lblName.Visible = false;
                this.lblUnDrugZTInfo.Text = "";
                this.lblUndrugZTUserCode.Text = "";

                return;
            }
            node.Tag = e.Node.Tag;

            string pcode = ((FS.HISFC.Models.Fee.Item.Undrug)e.Node.Tag).ID;
            string pname = ((FS.HISFC.Models.Fee.Item.Undrug)e.Node.Tag).Name;

            this.lblUserCode.Visible = true;
            this.lblName.Visible = true;
            this.lblUnDrugZTInfo.Text = ((FS.HISFC.Models.Fee.Item.Undrug)e.Node.Tag).Name;
            this.lblUndrugZTUserCode.Text = ((FS.HISFC.Models.Fee.Item.Undrug)e.Node.Tag).UserCode;

            this.FillFpDetails(pcode, pname);

            this.SumCost();
        }

        private void SpreadDetails_EditModeOff(object sender, EventArgs e)
        {
            //增加组套名称和费用显示，停用项目红色提示{87E9CA9A-5C81-47c0-9926-FD5CBF4B11A6}
            if (this.fpDetails.ActiveColumnIndex == 5 || this.fpDetails.ActiveColumnIndex == 6)
            {
                SumCost();
            }
        }

        private void SumCost()
        {
            decimal price = 0m;
            decimal count = 0m;
            decimal cost = 0m;
            decimal rate = 0m;
            for (int row = 0; row < fpDetails.Rows.Count; row++)
            {
                //增加组套名称和费用显示，停用项目红色提示{87E9CA9A-5C81-47c0-9926-FD5CBF4B11A6}
                if (this.fpDetails.Cells[row, 6].Text == "有效")
                {
                    count = FS.FrameWork.Function.NConvert.ToDecimal(fpDetails.Cells[row, 5].Text);
                    price = FS.FrameWork.Function.NConvert.ToDecimal(fpDetails.Cells[row, 11].Text);
                    //优惠比例
                    rate = 1;
                    //FS.FrameWork.Function.NConvert.ToDecimal(fpDetails.Cells[row, 14].Text);
                    //cost += price * count;
                    cost += price * count * rate;
                }
                else
                {
                    this.fpDetails.Rows[row].ForeColor = Color.Red;
                }
            }
            //增加组套名称和费用显示，停用项目红色提示{87E9CA9A-5C81-47c0-9926-FD5CBF4B11A6}
            //this.lblCost.Text = "合计金额：" + FS.NFC.Public.String.FormatNumber(cost, 3);
            this.lblTotCost.Text = "金额：" + FS.FrameWork.Public.String.FormatNumber(cost, 2);
        }

        //private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        //protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        //{
        //    this.toolBarService.AddToolButton("保存", "保存组套明细", 0, true, false, null);
        //    return this.toolBarService;
        //    //return base.OnInit(sender, neuObject, param);
        //}
        //public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        //{
        //    switch (e.ClickedItem.Text)
        //    {
        //        case "保存":
        //            this.SaveButtonHandler();
        //            break;
        //        default: break;
        //    }
        //    //base.ToolStrip_ItemClicked(sender, e);
        //}
        //private void SetDetailFarpoint()
        //{
        //    this.fpDetails.Columns["项目名称"].Width = 150;
        //    this.fpDetails.Columns["组套名称"].Width = 150;
        //    this.fpDetails.Columns["拼音码"].Width = 80;
        //    this.fpDetails.Columns["五笔码"].Width = 80;
        //    this.fpDetails.Columns["输入码"].Width = 80;

        //    this.fpDetails.Columns["组套编号"].Visible = false;
        //    //this.fpDetails.Columns["组套名称"].Visible = false;
        //    this.fpDetails.Columns["项目编号"].Visible = false;
        //    //this.fpDetails.Columns["项目名称"].Visible = false;
        //    this.fpDetails.Columns["顺序号"].Visible = false;
        //    this.fpDetails.Columns["数量"].Visible = false;
        //    this.fpDetails.ColumnHeader.Cells.Get(0, 6).Value = "有效性";
        //    this.fpDetails.Columns["标志位"].Visible = false;
        //}
    }
}
