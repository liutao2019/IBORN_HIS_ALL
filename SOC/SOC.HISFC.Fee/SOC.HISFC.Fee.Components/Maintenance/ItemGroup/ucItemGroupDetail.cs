using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;
using FS.SOC.HISFC.Fee.Components.Maintenance.Item;

namespace FS.SOC.HISFC.Fee.Components.Maintenance.ItemGroup
{
    /// <summary>
    /// [功能描述: 物价组套基本信息明细界面]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// 说明：
    /// </summary>
     partial class ucItemGroupDetail : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.SOC.HISFC.Fee.Interface.Components.IItemGroupDetail
    {
         public ucItemGroupDetail()
         {
             InitializeComponent();
         }

        #region 域变量

        /// <summary>
        /// Fp设置文件
        /// </summary>
        private string settingFile = "";
        /// <summary>
        /// 过滤字符串
        /// </summary>
        private string filter = "1=1";

        /// <summary>
        /// 药品基本信息的数据集
        /// </summary>
        private System.Data.DataTable dtItems = new DataTable();
        /// <summary>
        /// 存储变量
        /// </summary>
        private System.Collections.Hashtable hsItem = new System.Collections.Hashtable();

        /// <summary>
        /// 非药品套餐维护
        /// </summary>
        private FS.HISFC.BizLogic.Manager.UndrugztManager ztManager = new FS.HISFC.BizLogic.Manager.UndrugztManager();

        private FS.SOC.HISFC.Fee.BizLogic.UndrugGroup itemGroupManager = new FS.SOC.HISFC.Fee.BizLogic.UndrugGroup();
         /// <summary>
         /// 非药品管理
         /// </summary>
        private FS.SOC.HISFC.Fee.BizLogic.Undrug undrugManager = new FS.SOC.HISFC.Fee.BizLogic.Undrug();

        private FS.SOC.HISFC.Fee.Components.Maintenance.Item.ucItem ucItem = null;
        private Form frmItem = null;
        #endregion

        #region 过滤

        private void filterItem()
        {
            this.filter = Function.GetFilterStr(this.dtItems.DefaultView, "%" + this.nTxtCustomCode.Text.Trim() + "%");

            this.dtItems.DefaultView.RowFilter = filter;
            this.neuSpread.ReadSchema(this.settingFile);
        }

        #endregion

        #region 初始化

        private void initFarPoint()
        {
            try
            {
                this.neuSpread.ReadSchema(this.settingFile);
            }
            catch (Exception ex)
            {
                this.fpItemGroup.ColumnHeader.Rows[0].Height = 34f;
                FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "错误", "读取配置文件错误，系统将采用默认显示！\r\n\r\n" + ex.Message, ToolTipIcon.Warning);
            }
            //if (!this.neuSpread.ReadSchema(this.settingFile))
            //{
            //    this.fpItemGroup.ColumnHeader.Rows[0].Height = 34f;
            //}

            this.fpItemGroup.LockForeColor = Color.Red;
            this.fpItemGroup.DataAutoSizeColumns = false;
        }

        private void initBaseData()
        {

           
        }

        private void initDataTable()
        {
            if (this.dtItems == null)
            {
                this.dtItems = new DataTable();
            }

            this.fpItemGroup.DataAutoSizeColumns = false;
            //定义类型
            this.dtItems.Columns.AddRange(new DataColumn[] { 
                                                   new DataColumn("自定义码", typeof(string)),
                                                   new DataColumn("组套名称", typeof(string)),
                                                   new DataColumn("项目编号", typeof(string)),
                                                   new DataColumn("项目名称", typeof(string)),
                                                   new DataColumn("数量", typeof(decimal)),
                                                   new DataColumn("系统类别", typeof(string)),
                                                   new DataColumn("费用代码", typeof(string)),
                                                   new DataColumn("默认价", typeof(string)),
                                                   new DataColumn("儿童价", typeof(string)),
                                                   new DataColumn("特诊价", typeof(string)),
                                                   new DataColumn("加成（优惠）比例", typeof(string)),
                                                   new DataColumn("有效性", typeof(bool)), 
                                                   new DataColumn("顺序号", typeof(int)), 
                                                   new DataColumn("备注", typeof(string)), 
                                                   new DataColumn("拼音码", typeof(string)),
                                                   new DataColumn("五笔码", typeof(string)),
                                                   new DataColumn("组套编号", typeof(string))

            });


            this.setReadOnly(true);

            DataColumn[] keys = new DataColumn[1];

            keys[0] = this.dtItems.Columns["项目编号"];

            this.dtItems.PrimaryKey = keys;

            this.fpItemGroup.DataSource = this.dtItems.DefaultView;

            try
            {
                this.neuSpread.ReadSchema(this.settingFile);
            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "错误", "读取配置文件错误，系统将采用默认显示！\r\n\r\n" + ex.Message, ToolTipIcon.Warning);
            }
        }

        private void initEvents()
        {
            this.nTxtCustomCode.TextChanged -= new EventHandler(nTxtCustomCode_TextChanged);
            this.nTxtCustomCode.TextChanged += new EventHandler(nTxtCustomCode_TextChanged);

            this.neuSpread.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread_ColumnWidthChanged);
            this.neuSpread.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread_ColumnWidthChanged);

            this.neuSpread.ButtonClicked -= new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread_ButtonClicked);
            this.neuSpread.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread_ButtonClicked);

            this.neuSpread.Change -= new FarPoint.Win.Spread.ChangeEventHandler(neuSpread_Change);
            this.neuSpread.Change += new FarPoint.Win.Spread.ChangeEventHandler(neuSpread_Change);

            this.btnLookUndrug.Click -= new EventHandler(btnLookUndrug_Click);
            this.btnLookUndrug.Click += new EventHandler(btnLookUndrug_Click);
        }

        /// <summary>
        /// 设置药品维护窗口
        /// </summary>
        private void InitMaintenanceForm()
        {
            if (this.ucItem == null)
            {
                this.ucItem = new FS.SOC.HISFC.Fee.Components.Maintenance.Item.ucItem();
                this.ucItem.Dock = DockStyle.Fill;
                this.ucItem.Init();
                this.ucItem.EndSave -= new ucItem.SaveItemHandler(ucItem_EndSave);
                this.ucItem.EndSave += new ucItem.SaveItemHandler(ucItem_EndSave);
            }
            if (this.frmItem == null)
            {
                this.frmItem = new Form();
                this.frmItem.Width = this.ucItem.Width + 10;
                this.frmItem.Height = this.ucItem.Height + 25;
                this.frmItem.Text = "物价详细信息维护";
                this.frmItem.StartPosition = FormStartPosition.CenterScreen;
                this.frmItem.ShowInTaskbar = false;
                this.frmItem.HelpButton = false;
                this.frmItem.MaximizeBox = false;
                this.frmItem.MinimizeBox = false;
                this.frmItem.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.frmItem.Controls.Add(this.ucItem);
            }

        }
        #endregion

        #region 数据显示

        private int addItemObjectToDataTable(FS.HISFC.Models.Fee.Item.UndrugComb item)
        {
            if (item == null)
            {
                CommonController.CreateInstance().MessageBox("向DataTable中添加物价复合项目基本信息失败：物价基本信息为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
            if (this.dtItems == null)
            {
                CommonController.CreateInstance().MessageBox("向DataTable中添加物价复合项目基本信息失败：DataTable为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }

            DataRow row = this.dtItems.NewRow();

            this.setDataRowValue(item, row);

            this.dtItems.Rows.Add(row);

            if (this.hsItem.Contains(item.ID))
            {
                CommonController.CreateInstance().MessageBox("编码：" + item.ID + " 名称：" + item.Name + " 已经重复！", System.Windows.Forms.MessageBoxIcon.Information);
                return -1;
            }
            else
            {
                this.hsItem.Add(item.ID, item);
            }

            

            return 1;
        }

        private int setDataRowValue(FS.HISFC.Models.Fee.Item.UndrugComb item, DataRow row)
        {
            row["组套编号"] = item.Package.ID;
            row["组套名称"] = item.Package.Name;
            row["项目编号"] = item.ID;
            row["项目名称"] = item.Name;
            row["系统类别"] = item.SysClass.Name;
            row["数量"] = item.Qty.ToString("F2");
            row["费用代码"] = CommonController.CreateInstance().GetConstantName(FS.HISFC.Models.Base.EnumConstant.MINFEE, item.MinFee.ID);//
            row["自定义码"] = item.UserCode;
            row["拼音码"] = item.SpellCode;
            row["五笔码"] = item.WBCode;
            row["顺序号"] = item.SortID;
            row["备注"] = item.Memo;
            row["默认价"] = item.Price.ToString("F4");
            row["儿童价"] = item.ChildPrice.ToString("F4");
            row["特诊价"] = item.SpecialPrice.ToString("F4");
            row["有效性"] = FS.FrameWork.Function.NConvert.ToBoolean(item.ValidState);
            //若优惠比例为0,则赋值为1
            if (item.ItemRate == 0)
            {
                item.ItemRate = 1;
            }
            row["加成（优惠）比例"] = item.ItemRate.ToString("F2");

            return 1;
        }

        /// <summary>
        /// 从数据表内获取数据
        /// </summary>
        /// <param name="row">需获取数据的数据表行</param>
        /// <returns></returns>
        private FS.HISFC.Models.Fee.Item.UndrugComb getDataFromTable(DataRow row, FS.SOC.HISFC.Fee.Models.Undrug itemGroup)
        {
            FS.HISFC.Models.Fee.Item.UndrugComb undrugComb = new FS.HISFC.Models.Fee.Item.UndrugComb();
            undrugComb.Package.ID = itemGroup.ID;
            undrugComb.Package.Name = itemGroup.Name;
            undrugComb.ID = row["项目编号"].ToString();
            FS.HISFC.Models.Fee.Item.UndrugComb undrug = this.hsItem[undrugComb.ID] as FS.HISFC.Models.Fee.Item.UndrugComb;
            undrugComb = undrug.Clone();
            undrugComb.SortID = Convert.ToInt32(row["顺序号"].ToString());
            undrugComb.Qty = Convert.ToDecimal(row["数量"].ToString());
            undrugComb.ValidState = FS.FrameWork.Function.NConvert.ToInt32(row["有效性"].ToString()).ToString();
            undrugComb.Memo = row["备注"].ToString();
            undrugComb.ItemRate = Convert.ToDecimal(row["加成（优惠）比例"]);
            
            undrugComb.SysClass = itemGroup.SysClass;

            return undrugComb;
        }

        private void computePrice()
        {
            FS.SOC.HISFC.Fee.Models.Undrug itemGroup = this.lbItemGroupCustom.Tag as FS.SOC.HISFC.Fee.Models.Undrug;
            if (itemGroup == null)
            {
                return;
            }

            //计算金额
            decimal Price = 0;
            decimal ChildPrice = 0;
            decimal SpecialPrice = 0;
            for (int i = 0; i < this.fpItemGroup.RowCount; i++)
            {
                FS.HISFC.Models.Fee.Item.UndrugComb undrugComb = new FS.HISFC.Models.Fee.Item.UndrugComb();
                undrugComb.ID = this.neuSpread.GetCellText(0, i, "项目编号");
                undrugComb = this.hsItem[undrugComb.ID] as FS.HISFC.Models.Fee.Item.UndrugComb;
                undrugComb.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread.GetCellText(0, i, "数量"));
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread.GetCellText(0, i, "有效性")))
                {
                    Price += FS.FrameWork.Public.String.FormatNumber(undrugComb.Qty * undrugComb.Price * undrugComb.ItemRate, 2);
                    ChildPrice += FS.FrameWork.Public.String.FormatNumber(undrugComb.Qty * undrugComb.ChildPrice * undrugComb.ItemRate, 2);
                    SpecialPrice += FS.FrameWork.Public.String.FormatNumber(undrugComb.Qty * undrugComb.SpecialPrice * undrugComb.ItemRate, 2);

                    this.fpItemGroup.Rows[i].ForeColor = Color.Black;
                }
                else
                {
                    this.fpItemGroup.Rows[i].ForeColor = Color.Red;
                }
            }

            this.lbPrice.Text = Price.ToString("F4");
            this.lbChildPrice.Text = ChildPrice.ToString("F4");
            this.lbSpecialPrice.Text = SpecialPrice.ToString("F4");

        }

        private void setReadOnly(bool isReadOnly)
        {
            foreach (DataColumn dc in this.dtItems.Columns)
            {
                if (dc.ColumnName.Equals("数量")
                    || dc.ColumnName.Equals("有效性")
                    || dc.ColumnName.Equals("顺序号")
                    || dc.ColumnName.Equals("备注")
                    || dc.ColumnName.Equals("加成（优惠）比例")
                    )
                {
                    dc.ReadOnly = false;
                }
                else
                {
                    dc.ReadOnly = isReadOnly;
                }
            }
        }

        #endregion

        #region 事件触发

        void nTxtCustomCode_TextChanged(object sender, EventArgs e)
        {
            this.filterItem();
        }

        void neuSpread_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.neuSpread.SaveSchema(this.settingFile);
        }

        void neuSpread_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == this.neuSpread.GetColumnIndex(0, "有效性"))
            {
                this.computePrice();
            }
        }

        void neuSpread_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            if (e.Column == this.neuSpread.GetColumnIndex(0, "数量"))
            {
                this.computePrice();
            }
        }

        void btnLookUndrug_Click(object sender, EventArgs e)
        {
            //查看属性项目编号
            if (!Function.JugePrive("0801", "01"))
            {
                CommonController.CreateInstance().MessageBox("因为您没有物价基本信息维护权限，该操作已取消！", MessageBoxIcon.Information);
                return;
            }

            string undrugNO = this.neuSpread.GetCellText(0, this.fpItemGroup.ActiveRowIndex, "项目编号");
            //查找
            FS.SOC.HISFC.Fee.BizLogic.Undrug undrugMgr = new FS.SOC.HISFC.Fee.BizLogic.Undrug();
            FS.SOC.HISFC.Fee.Models.Undrug undrug = undrugMgr.GetUndrug(undrugNO);
            if (undrug != null)
            {
                this.InitMaintenanceForm();
                this.ucItem.Init();
                this.ucItem.EndSave -= new ucItem.SaveItemHandler(ucItem_EndSave);
                this.ucItem.EndSave += new ucItem.SaveItemHandler(ucItem_EndSave);
                this.ucItem.SetItem(undrug, false);
                this.frmItem.ShowDialog();

            }

        }

        void ucItem_EndSave(FS.SOC.HISFC.Fee.Models.Undrug item)
        {
            DataRow row = this.dtItems.Rows.Find(new string[] { item.ID });
            if (row != null)
            {
                this.setReadOnly(false);
                row["项目编号"] = item.ID;
                row["项目名称"] = item.Name;
                row["系统类别"] = item.SysClass.Name;
                row["费用代码"] = CommonController.CreateInstance().GetConstantName(FS.HISFC.Models.Base.EnumConstant.MINFEE, item.MinFee.ID);//
                row["自定义码"] = item.UserCode;
                row["拼音码"] = item.SpellCode;
                row["五笔码"] = item.WBCode;
                row["备注"] = item.Memo;
                row["默认价"] = item.Price.ToString("F4");
                row["有效性"] = FS.FrameWork.Function.NConvert.ToBoolean(item.ValidState);


                this.setReadOnly(true);
            }
        }

        #endregion

        #region IItemGroupDetail 成员

         /// <summary>
         /// 添加组套项目
         /// </summary>
         /// <param name="itemGroup"></param>
         /// <returns></returns>
        public int AddItemGroup(FS.SOC.HISFC.Fee.Models.Undrug itemGroup)
        {
            if (itemGroup == null)
            {
                return -1;
            }

            this.lbItemGroupCustom.Tag = itemGroup;

            this.lbItemGroupCustom.Text = itemGroup.UserCode;
            this.lbItemGroupName.Text = itemGroup.Name;
            this.lbPrice.Text = itemGroup.Price.ToString("F4");

            //复合项目
            ArrayList lstzt = this.itemGroupManager.QueryUndrugGroupDetail(itemGroup.ID);

            if (lstzt == null)
            {
                throw new Exception(this.ztManager.Err);
            }

            //FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载数据...");
            //Application.DoEvents();
            //int progress = 1;
            foreach (FS.HISFC.Models.Fee.Item.UndrugComb item in lstzt)
            {
                //FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(progress++, lstzt.Count);
                if (this.addItemObjectToDataTable(item) != 1)
                {
                    continue;
                }
            }
            //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            this.dtItems.AcceptChanges();
            //无权限，则锁定
            if (!Function.JugePrive("0801", "02"))
            {
                this.fpItemGroup.Rows[0, this.fpItemGroup.RowCount - 1].Locked = true;
            }
            else
            {
                this.fpItemGroup.Rows[0, this.fpItemGroup.RowCount - 1].Locked = false;
            }

            //更新价格
            this.computePrice();

            return 1;
        }

         /// <summary>
         /// 添加项目
         /// </summary>
         /// <param name="item"></param>
         /// <returns></returns>
        public int AddItem(FS.SOC.HISFC.Fee.Models.Undrug item)
        {
            FS.SOC.HISFC.Fee.Models.Undrug itemGroup = this.lbItemGroupCustom.Tag as FS.SOC.HISFC.Fee.Models.Undrug;
            if (itemGroup == null)
            {
                return -1;
            }
            FS.HISFC.Models.Fee.Item.UndrugComb undrugzt = new FS.HISFC.Models.Fee.Item.UndrugComb();
            undrugzt.Package.ID = itemGroup.ID;
            undrugzt.Package.Name = itemGroup.Name;

            undrugzt.ID = item.ID;
            undrugzt.Name = item.Name;
            undrugzt.SortID = 0;
            undrugzt.Qty = 1;
            undrugzt.ValidState = "1";
            undrugzt.SpellCode = item.SpellCode;
            undrugzt.WBCode = item.WBCode;
            undrugzt.UserCode = item.UserCode;
            undrugzt.ChildPrice = item.ChildPrice; //儿童
            undrugzt.Price = item.Price;//三甲
            undrugzt.SpecialPrice = item.SpecialPrice;//特诊价
            undrugzt.Memo = "";
            
            this.addItemObjectToDataTable(undrugzt);

            //更新价格
            this.computePrice();
            return 1;
        }

        public int AddNewRow()
        {
            if (this.dtItems == null)
            {
                this.initDataTable();
            }
            this.dtItems.DefaultView.RowFilter += " 1=1 or 项目编号='F001'";
            DataRow row = this.dtItems.NewRow();
            row["项目编号"] = "F001";
            this.dtItems.Rows.Add(row);
            this.neuSpread.Focus();

            return 1;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int Save()
        {

            this.neuSpread.StopCellEditing();

            foreach (DataRow dr in this.dtItems.Rows)
            {
                dr.EndEdit();
            }

            //有效性判断
            if (!Valid())
            {
                return -1;
            };

            FS.SOC.HISFC.Fee.Models.Undrug itemGroup = this.lbItemGroupCustom.Tag as FS.SOC.HISFC.Fee.Models.Undrug;
            if (itemGroup == null)
            {
                return -1;
            }


            decimal ChildPrice = 0;
            decimal Price = 0;
            decimal SpecialPrice = 0; //特诊

            foreach (DataRow row in this.dtItems.Rows)
            {
                FS.HISFC.Models.Fee.Item.UndrugComb undrugComb = this.getDataFromTable(row, itemGroup);
                if (FS.FrameWork.Function.NConvert.ToBoolean(row["有效性"]))
                {
                    Price += FS.FrameWork.Public.String.FormatNumber(undrugComb.Qty * undrugComb.Price * undrugComb.ItemRate, 2);
                    ChildPrice += FS.FrameWork.Public.String.FormatNumber(undrugComb.Qty * undrugComb.ChildPrice * undrugComb.ItemRate, 2);
                    SpecialPrice += FS.FrameWork.Public.String.FormatNumber(undrugComb.Qty * undrugComb.SpecialPrice * undrugComb.ItemRate, 2);
                }
            }

            //取修改和增加的数据
            List<FS.HISFC.Models.Fee.Item.UndrugComb> lstUndrug = new List<FS.HISFC.Models.Fee.Item.UndrugComb>();
            DataTable dataChanges = this.dtItems.GetChanges(DataRowState.Modified|DataRowState.Added);

            if (dataChanges != null)
            {
                foreach (DataRow row in dataChanges.Rows)
                {
                    FS.HISFC.Models.Fee.Item.UndrugComb undrugComb = this.getDataFromTable(row, itemGroup);
                    undrugComb.Oper.Memo =row.RowState== DataRowState.Added?"MAD": "MUP";
                    lstUndrug.Add(undrugComb);
                }
            }


            if (lstUndrug.Count > 0)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存...");
                Application.DoEvents();
                //定义数据库处理事务
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                ztManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                if (this.ztManager.SaveUndrugzt(lstUndrug) != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存数据失败！\r\n" + ztManager.Err));
                    return -1;
                }

                if (InterfaceManager.GetISaveItemGroupDetail().SaveCommitting(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Insert, new ArrayList(lstUndrug)) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    CommonController.CreateInstance().MessageBox(this, "保存失败，请与系统管理员联系并报告错误：" + InterfaceManager.GetISaveItem().Err, MessageBoxIcon.Error);
                    return -1;
                }
            }


            //更新价格
            if (this.ztManager.UpdateUndrugztPrice(itemGroup.ID, Price, ChildPrice, SpecialPrice) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                CommonController.CreateInstance().MessageBox(this, "保存数据失败" + this.ztManager.Err, MessageBoxIcon.Information);
                return -1;
            }
            itemGroup = undrugManager.GetUndrug(itemGroup.ID);
            if (itemGroup == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                CommonController.CreateInstance().MessageBox(this, "保存失败，请与系统管理员联系并报告错误：" + undrugManager.Err, MessageBoxIcon.Error);
                return -1;
            }
            if (InterfaceManager.GetISaveItem().SaveCommitting(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Update, itemGroup) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                CommonController.CreateInstance().MessageBox(this, "保存失败，请与系统管理员联系并报告错误：" + InterfaceManager.GetISaveItem().Err, MessageBoxIcon.Error);
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            if (dataChanges != null)
            {
                dataChanges.AcceptChanges();
            }

            itemGroup.Price = Price;
            itemGroup.ChildPrice = ChildPrice;
            itemGroup.SpecialPrice = SpecialPrice;
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            CommonController.CreateInstance().MessageBox(this, "保存数据成功", MessageBoxIcon.Information);

            this.computePrice();

            return 1;
        }

        /// <summary>
        /// 有效性验证
        /// </summary>
        /// <returns></returns>
        public bool Valid()
        {
            return true;
        }

        /// <summary>
        /// 删除新加项
        /// </summary>
        /// <returns></returns>
        public int DeleteItem()
        {
            if (this.fpItemGroup.RowCount <= 0||this.fpItemGroup.ActiveRowIndex<0)
            {
                return -1;
            }

            string itemCode= this.neuSpread.GetCellText(0, this.fpItemGroup.ActiveRowIndex, "项目编号");

            //从DataTable里面查找
            if (this.dtItems != null)
            {
                DataRow dr = this.dtItems.Rows.Find(itemCode);
                if (dr != null )
                {
                    if (dr.RowState == DataRowState.Added)
                    {
                        this.dtItems.Rows.Remove(dr);
                        if (this.hsItem.ContainsKey(itemCode))
                        {
                            this.hsItem.Remove(itemCode);
                        }
                        this.computePrice();
                    }
                    else
                    {
                        CommonController.CreateInstance().MessageBox(this, "已经保存的项目不允许删除，请作废！", MessageBoxIcon.Information);
                        return -1;
                    }

                }
            }

            return 1;

        }

        #endregion

        #region IDataDetail 成员

        public int Clear()
        {
            this.lbItemGroupCustom.Text ="";
            this.lbItemGroupCustom.Tag = null;
            this.lbItemGroupName.Text = "";
            this.lbPrice.Text = "";
            this.nTxtCustomCode.Text = "";
            this.hsItem.Clear();
            this.dtItems.Clear();

            return 1;
        }

        public string Filter
        {
            set {  }
        }

        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.FilterTextChangeHander FilterTextChanged
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public FS.SOC.Windows.Forms.FpSpread FpSpread
        {
            get { return this.neuSpread; }
        }

        public string Info
        {
            set {  }
        }

        public int Init()
        {
            try
            {
                this.initEvents();
                this.initDataTable();
                this.initBaseData();
                this.initFarPoint();
            }
            catch (Exception ex)
            {
                CommonController.CreateInstance().MessageBox("初始化失败，请系统管理员报告错误：" + ex.Message, MessageBoxIcon.Information);
                return -1;
            }

            return 1;
        }

        public bool IsContainsFocus
        {
            get { return this.Focused; }
        }

        public int SetFocus()
        {
            if (this.fpItemGroup.RowCount > 0)
            {
                this.fpItemGroup.ActiveRowIndex = 0;
                this.fpItemGroup.AddSelection(0, 1, 1, 1);
            }
            return 1;
        }

        public int SetFocusToFilter()
        {
            this.nTxtCustomCode.Select();
            this.nTxtCustomCode.Focus();
            return 1;
        }

        public string SettingFileName
        {
            set { this.settingFile = value; ; }
        }

        #endregion

        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            ztManager.Dispose();
            hsItem.Clear();
            dtItems.Dispose();
            this.neuSpread.Dispose();
        }

        #endregion
    }
}
