﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;

namespace FS.SOC.HISFC.Components.Pharmacy.Maintenance
{
    /// <summary>
    /// <br></br>
    /// [功能描述: 科室列表显示控件]<br></br>
    /// [创 建 者: 杨永刚 梁俊泽]<br></br>
    /// [创建时间: 2006-11]<br></br>
    /// 说明：库存初始化的作用不仅仅是增加库存，新药增加到各药房库存中便于生成计划，方便医生开发参考等
    /// </summary>
    public partial class ucStorageInit : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucStorageInit()
        {
            InitializeComponent();
        }

        #region 变量

        //数据存储
        private DataView dvDrugList;
        private DataSet dsDrug = new DataSet();
        //帮助类
        private FS.FrameWork.Public.ObjectHelper drugTypeHelper = new FS.FrameWork.Public.ObjectHelper();
        private FS.FrameWork.Public.ObjectHelper qualityHelper = new FS.FrameWork.Public.ObjectHelper();
        //药品管理类
        private FS.SOC.HISFC.BizLogic.Pharmacy.Storage storageMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Storage();
        //过虑串
        private string filter = "1=1";

        /// <summary>
        /// 是否拥有所有权限
        /// </summary>
        private bool isHavePriv = false;

        /// <summary>
        /// 是否按照包装单位增加库存，默认为false
        /// </summary>
        private bool isAddByPackQty = false;

        /// <summary>
        /// 权限科室
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 允许增加的最大库存量
        /// </summary>
        private uint maxStockQty = 0;

        [Description("是否允许按包装单位增加库存，默认为false")]
        public Boolean IsAddByPackQty
        {
            get
            {
                return isAddByPackQty;
            }
            set
            {
                isAddByPackQty = value;
            }
        }

        /// <summary>
        /// 允许增加的最大库存量
        /// </summary>
        [Description("允许增加的最大库存量，0则不允许数量录入")]
        public uint MaxStockQty
        {
            get 
            { 
                return maxStockQty;
            }
            set 
            { 
                maxStockQty = value;
                if (value == 0)
                {
                    txtSum.Enabled = false;
                }
            }
        }
        #endregion

        #region 方法

        /// <summary>
        /// 检索药品信息
        /// </summary>
        private void RetrieveData()
        {
            //显示等待信息
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("正在检索药品信息..."));
            Application.DoEvents();

            //取药品数据
            List<FS.HISFC.Models.Pharmacy.Item> al = storageMgr.QueryItemList(true);

            //对药品进行排序
            CompareApplyOut compare = new CompareApplyOut();
            al.Sort(compare);

            if (al == null)
            {
                MessageBox.Show(storageMgr.Err);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return;
            }

            //取药品类型数组
            FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
            this.drugTypeHelper.ArrayObject = constantMgr.GetAllList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE);
            this.qualityHelper.ArrayObject = constantMgr.GetAllList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY);

            //显示药品数据
            FS.HISFC.Models.Pharmacy.Item info;
            for (int i = 0; i < al.Count; i++)
            {
                info = al[i] as FS.HISFC.Models.Pharmacy.Item;
                this.dsDrug.Tables[0].Rows.Add(new object[] {
																	false,             //是否添加
																	info.Name,        //药品名称
																	info.Specs,       //药品规格
																	info.PriceCollection.RetailPrice, //零售价
																	qualityHelper.GetName(info.Quality.ID),//药品性质
																	info.PackUnit,    //包装单位
																	info.PackQty,     //包装数量
																	info.MinUnit,     //最小单位
																	info.ID,          //药品编码
																	drugTypeHelper.GetName(info.Type.ID), //药品类型
																	info.NameCollection.SpellCode,  //拼音码		
																	info.NameCollection.WBCode,     //五笔码		
																	info.NameCollection.UserCode,   //自定义码		
																	info.NameCollection.RegularName, //通用名		
																	info.NameCollection.SpellCode,//通用名拼音码		
																	info.NameCollection.WBCode,   //通用名五笔码	
				});
                //设置格式
                this.SetFormat();
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(i, al.Count);
                Application.DoEvents();
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

        }

        /// <summary>
        /// 初始化视图
        /// </summary>
        private void InitDataView()
        {
            //绑定数据源
            this.dsDrug.Tables.Clear();
            this.dsDrug.Tables.Add();
            this.dvDrugList = new DataView(this.dsDrug.Tables[0]);
            this.neuSpread1_Sheet1.DataSource = this.dvDrugList;
            this.dvDrugList.AllowEdit = true;

            //定义类型
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtDTime = System.Type.GetType("System.DateTime");
            System.Type dtBool = System.Type.GetType("System.Boolean");

            //在myDataTable中添加列
            this.dsDrug.Tables[0].Columns.AddRange(new DataColumn[] {
																			new DataColumn("添加",        dtBool),
																			new DataColumn("商品名称",    dtStr),
																			new DataColumn("规格",        dtStr),
																			new DataColumn("零售价",      dtDec),
																			new DataColumn("药品性质",    dtStr),
																			new DataColumn("包装单位",    dtStr),
																			new DataColumn("包装数量",    dtDec),
																			new DataColumn("最小单位",    dtStr),
																			new DataColumn("药品编码",    dtStr),
																			new DataColumn("药品类别",    dtStr),
																			new DataColumn("拼音码",      dtStr),
																			new DataColumn("五笔码",      dtStr),
																			new DataColumn("自定义码",    dtStr),
																			new DataColumn("通用名",      dtStr),
																			new DataColumn("通用名拼音码",dtStr),
																			new DataColumn("通用名五笔码",dtStr)
																		});
        }

        /// <summary>
        /// 设置farpoint格式
        /// </summary>
        private void SetFormat()
        {
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.neuSpread1_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "添加";
            this.neuSpread1_Sheet1.Columns.Get(0).Locked = false;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 38F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "商品名称";
            this.neuSpread1_Sheet1.Columns.Get(1).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 129F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "规格";
            this.neuSpread1_Sheet1.Columns.Get(2).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "零售价";
            this.neuSpread1_Sheet1.Columns.Get(3).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "药品性质";
            this.neuSpread1_Sheet1.Columns.Get(4).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 72F;
            this.neuSpread1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "包装单位";
            this.neuSpread1_Sheet1.Columns.Get(5).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 39F;
            this.neuSpread1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "包装数量";
            this.neuSpread1_Sheet1.Columns.Get(6).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 42F;
            this.neuSpread1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "最小单位";
            this.neuSpread1_Sheet1.Columns.Get(7).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 41F;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "药品编码";
            this.neuSpread1_Sheet1.Columns.Get(8).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "药品类别";
            this.neuSpread1_Sheet1.Columns.Get(9).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(10).Label = "拼音码";
            this.neuSpread1_Sheet1.Columns.Get(10).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(11).Label = "五笔码";
            this.neuSpread1_Sheet1.Columns.Get(11).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(12).Label = "自定义码";
            this.neuSpread1_Sheet1.Columns.Get(12).Visible = true;
            this.neuSpread1_Sheet1.Columns.Get(13).Label = "通用名";
            this.neuSpread1_Sheet1.Columns.Get(13).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(14).Label = "通用名拼音码";
            this.neuSpread1_Sheet1.Columns.Get(14).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(15).Label = "通用名五笔码";
            this.neuSpread1_Sheet1.Columns.Get(15).Visible = false;
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;

        }

        /// <summary>
        /// 选择事件
        /// </summary>
        /// <param name="isSelectAll"></param>
        private void SelectDrug(bool isSelectAll)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                this.neuSpread1_Sheet1.Cells[i, 0].Value = isSelectAll;
            }
        }

        /// <summary>
        /// 合法性检查
        /// </summary>
        private int ValidCheck()
        {
            //判断数量录入是否正确
            if (this.txtSum.Text == string.Empty || this.txtSum.Text.Trim() == "")
            {
                this.txtSum.Text = "0";
            }

            //判断录入数量
            if (FS.FrameWork.Function.NConvert.ToDecimal(this.txtSum.Text) > this.maxStockQty)
            {
                MessageBox.Show(Language.Msg("数量必须小于系统设置最大值：" + this.maxStockQty.ToString()), Language.Msg("数量录入错误"));
                this.txtSum.Focus();
                return -1;
            }

            if (this.maxStockQty > 0)
            {
                DialogResult dr = MessageBox.Show(Language.Msg("新增加的药品购入价、零售价都是从药品基本信息的中获取的，请注意维护准确\n\n是否继续？"), Language.Msg("提示"),MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                {
                    return -1;
                }
            }

            //停止数据编辑状态
            for (int i = 0; i < this.dvDrugList.Count; i++)
            {
                this.dvDrugList[i].EndEdit();
            }
            //设置过滤条件
            this.dvDrugList.RowFilter = this.filter + " and 添加 = true";
            //设置格式
            this.SetFormat();

            //判断是否存在药品数据
            if (this.neuSpread1_Sheet1.Rows.Count == 0)
            {
                MessageBox.Show(Language.Msg("请选择要添加的药品"), Language.Msg("提示"));
                return -1;
            }

            if (MessageBox.Show(Language.Msg("确定要增加您选中的“") + this.neuSpread1_Sheet1.Rows.Count.ToString() + Language.Msg("”条药品库存吗？"), Language.Msg("确认增加库存"), MessageBoxButtons.YesNo) == DialogResult.No) return -1;

            return 0;
        }

        /// <summary>
        /// 初始化库存
        /// </summary>
        private void SetStorage()
        {
            if (this.ValidCheck() < 0)
            {
                return;
            }
            List<FS.HISFC.Models.Base.Department> aldept = this.tvDeptTree1.SelectNodes;

            if (aldept.Count == 0)
            {
                MessageBox.Show(Language.Msg("请选择要添加的库房"), Language.Msg("提示"));
                return;
            }
            if (!this.isHavePriv)
            {
                bool isAllowEdit = false;
                foreach (FS.HISFC.Models.Base.Department dept in aldept)
                {
                    if (dept.ID == this.privDept.ID)
                    {
                        isAllowEdit = true;
                    }
                }
                if (!isAllowEdit)
                {
                    MessageBox.Show(Language.Msg("您的权限不能对其他库存科室进行库存初始化! 请添加药品基本信息维护权限"), Language.Msg("提示"));
                    return;
                }
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("正在添加库存数据..."));
            Application.DoEvents();

            //定义数据库处理事务
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.storageMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            try
            {
                string drugCode = "";
                decimal quantity = FS.FrameWork.Function.NConvert.ToDecimal(this.txtSum.Text);
                bool IsUpdate = false;
                bool check = false;
                FS.HISFC.Models.Pharmacy.StorageBase storageBase = new FS.HISFC.Models.Pharmacy.StorageBase();
                storageBase.GroupNO = 1;                   //批次号  
                storageBase.BatchNO = "1";                 //批号
                storageBase.ShowState = "0";               //显示的单位标记
                storageBase.ValidTime = this.storageMgr.GetDateTimeFromSysDateTime().AddMonths(6);        //有效期
                storageBase.PlaceNO = "0";               //货位号
                storageBase.ID = "0";                      //单据号
                storageBase.SerialNO = 0;                 //单内序号
                storageBase.SystemType = "01";            //库存操作类型M1门诊发药,M2门诊退药,Z1住院发药,Z2住院退药……
                storageBase.PrivType = "00";			   //class2_code
                storageBase.Class2Type = "0310";
                storageBase.Memo = "库存初始化";           //备注
                storageBase.Operation.Oper.OperTime = this.storageMgr.GetDateTimeFromSysDateTime();

                foreach (FS.HISFC.Models.Base.Department dept in aldept)
                {
                    for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                    {
                        FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(i, this.neuSpread1_Sheet1.RowCount);
                        Application.DoEvents();

                        //如果没有选中，则不处理此条数据
                        check = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, 0].Value);
                        if (!check) continue;

                        //取药品编码
                        drugCode = this.neuSpread1_Sheet1.Cells[i, 8].Text;
                        //库房编码
                        storageBase.StockDept.ID = dept.ID;
                        //目标科室
                        storageBase.TargetDept.ID = dept.ID;
                        //获得药品信息
                        storageBase.Item = this.storageMgr.GetItem(drugCode);

                        if (storageBase.Item == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show(Language.Msg("无法转换成storageBase.Item类型"), Language.Msg("提示"));
                            return;
                        }

                        //包装数量为0时，不允许增加库存
                        if (storageBase.Item.PackQty == 0) continue;

                        if (isAddByPackQty)
                        {
                            storageBase.Quantity = quantity * storageBase.Item.PackQty;          //库存更新数量
                        }
                        else
                        {
                            storageBase.Quantity = quantity;
                        }


                        storageBase.StoreCost = quantity * storageBase.Item.PriceCollection.RetailPrice / storageBase.Item.PackQty;      //库存金额

                        //插入库存表
                        if (this.storageMgr.SetStorage(storageBase) != 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show(this.storageMgr.Err, Language.Msg("保存错误提示"));
                            return;
                        }

                        IsUpdate = true;
                    }
                }
                if (IsUpdate)
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show(Language.Msg("保存成功！"));
                }
                else
                {
                    //如果没有更新的数据,则回滚事务.
                    FS.FrameWork.Management.PublicTrans.RollBack();
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(ex.Message);
                return;
            }


            //显示全部药品
            this.dvDrugList.RowFilter = "1=1";
            this.SetFormat();
            //取消选中
            this.SelectDrug(false);

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

        }

        #endregion

        #region 事件

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            //初始化dataview
            this.InitDataView();
            //初始化药品基本信息
            this.RetrieveData();
            //药品列表展开第一层
            this.tvDrugType1.Nodes[0].Expand();

            this.isHavePriv = Function.JugePrive("0301");
            this.privDept = ((FS.HISFC.Models.Base.Employee)(storageMgr.Operator)).Dept;

            base.OnLoad(e);
        }

        /// <summary>
        /// 过虑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQueryCode_TextChanged(object sender, EventArgs e)
        {
            if (this.dsDrug.Tables[0].Rows.Count == 0) return;

            try
            {
                string queryCode = "";
                queryCode = "%" + this.txtQueryCode.Text.Trim() + "%";

                string str = "((拼音码 LIKE '" + queryCode + "') OR " +
                    "(五笔码 LIKE '" + queryCode + "') OR " +
                    "(自定义码 LIKE '" + queryCode + "') OR " +
                    "(商品名称 LIKE '" + queryCode + "') OR" +
                    "(通用名拼音码 LIKE '" + queryCode + "') OR " +
                    "(通用名五笔码 LIKE '" + queryCode + "') OR " +
                    "(通用名 LIKE '" + queryCode + "') )";

                //设置过滤条件
                this.dvDrugList.RowFilter = this.filter + " AND " + str;
                //设置格式
                this.SetFormat();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 药品性质选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvDrugType1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //初始化
            this.filter = "1=1";
            this.txtQueryCode.Text = "";
            if (e.Node.Parent != null)
            {
                if (e.Node.Level == 1)
                {
                    this.filter = "( 药品类别 = '" + e.Node.Text + "') ";
                }
                else if (e.Node.Level == 2)
                {
                    this.filter = "( 药品类别 = '" + e.Node.Parent.Text + "')" + " and ( 药品性质 = '" + e.Node.Text + "')";
                }
            }
            else
            {
                this.filter = "1=1";
            }

            this.dvDrugList.RowFilter = this.filter;
            //设置格式
            this.SetFormat();
        }

        #endregion

        #region 工具栏信息

        /// <summary>
        /// 定义工具栏服务
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 初始化工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="NeuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object NeuObject, object param)
        {
            //增加工具栏
            this.toolBarService.AddToolButton("全选", "选中全部药品", 0, true, false, null);
            this.toolBarService.AddToolButton("全不选", "取消选中全部药品", 1, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
           
            this.SetStorage();
            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// 工具栏按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "全选":
                    this.SelectDrug(true);
                    break;
                case "全不选":
                    this.SelectDrug(false);
                    break;

            }

        }

        #endregion


        public class CompareApplyOut : IComparer<FS.HISFC.Models.Pharmacy.Item>
        {
            public int Compare(FS.HISFC.Models.Pharmacy.Item x, FS.HISFC.Models.Pharmacy.Item y)
            {
                FS.HISFC.Models.Pharmacy.Item o1 = (x as FS.HISFC.Models.Pharmacy.Item).Clone();
                FS.HISFC.Models.Pharmacy.Item o2 = (y as FS.HISFC.Models.Pharmacy.Item).Clone();


                string oX = o1.UserCode;
                string oY = o2.UserCode;

                int nComp;

                if (oX == null)
                {
                    nComp = (oY != null) ? -1 : 0;   //ox为null，oy不为null，返回-1，否则返回0；
                }
                else if (oY == null)
                {
                    nComp = 1;
                }
                else
                {
                    nComp = string.Compare(oX.ToString(), oY.ToString());
                }

                return nComp;
            }
        }
    }
}
