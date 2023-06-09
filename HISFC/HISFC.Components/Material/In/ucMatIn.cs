using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
using FS.HISFC.Components.Common.Controls;

namespace FS.HISFC.Components.Material.In
{
    /// <summary>
    /// 库存明细表触发器
    /// </summary>
    public partial class ucMatIn : ucIMAInOutBase, FS.FrameWork.WinForms.Forms.IInterfaceContainer,FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucMatIn()
        {
            InitializeComponent();
        }

        #region 域变量

        public IMatManager IManager = null;

        private System.Collections.Hashtable hsIManager = new Hashtable();

        /// <summary>
        /// 权限科室
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 工厂接口实例
        /// </summary>
        private FS.HISFC.Components.Material.IFactory matFactory = null;

        /// <summary>
        /// 物资入库单打印变量
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Material.IBillPrint iInPrint = null;

        /// <summary>
        /// 物资项目的所在科室 {AFE629CC-8493-4344-9792-8611C0BFA1BD}
        /// </summary>
        private string deptCode = string.Empty;

        #endregion

        #region 属性

        /// <summary>
        /// FpSheet
        /// </summary>
        [Browsable(false)]
        public FarPoint.Win.Spread.SheetView FpSheetView
        {
            get
            {
                return this.neuSpread1.Sheets[0];
            }
        }


        /// <summary>
        /// Fp
        /// </summary>
        [Browsable(false)]
        public FarPoint.Win.Spread.FpSpread Fp
        {
            get
            {
                return this.neuSpread1;
            }
        }

        public FS.HISFC.BizProcess.Interface.Material.IBillPrint IInPrint
        {
            get
            {
                if(this.iInPrint == null)
                {
                    this.InitPrintInterface();
                }

                return this.iInPrint;
            }
        }

        //{AFE629CC-8493-4344-9792-8611C0BFA1BD}
        public string DeptCode
        {
            get 
            { 
                return deptCode;
            }
            set 
            { 
                deptCode = value; 
            }
        }
        #endregion

        /// <summary>
        /// 初始化打印变量
        /// </summary>
        internal virtual void InitPrintInterface()
        {
            this.iInPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Material.IBillPrint)) as FS.HISFC.BizProcess.Interface.Material.IBillPrint;
            if(this.iInPrint == null)
            {
                MessageBox.Show("请设置打印接口!");
                return;
            }
        }

        /// <summary>
        /// 设置待选择数据
        /// </summary>
        /// <param name="dataType">数据类别 0 物品列表 1 目标单位科室库存列表 2 本科室库存列表 3 自定义列表</param>
        /// <param name="sqlIndex">Sql索引 类别为3时该参数才有意义</param>
        /// <param name="filterField">过滤字段 类别为3时该参数才有意义</param>
        /// <param name="formatStr">Sql参数 类别为3时该参数才有意义</param>
        /// <returns></returns>
        public int SetSelectData(string dataType, bool isBatch, string[] sqlIndex, string[] filterField, params string[] formatStr)
        {
            this.ucMaterialItemList1.ShowFpRowHeader = false;

            switch (dataType)
            {
                case "0":
                    //by yuyun 08-8-11{AFE629CC-8493-4344-9792-8611C0BFA1BD}
                    this.ucMaterialItemList1.ShowMaterialList(deptCode);
                    break;
                case "1":
                    if (this.TargetDept.ID == "")
                    {
                        MessageBox.Show("请选择供货单位");
                        return -1;
                    }
                    this.ucMaterialItemList1.ShowDeptStorage(this.TargetDept.ID, isBatch);
                    break;
                case "2":
                    this.ucMaterialItemList1.ShowDeptStorage(this.DeptInfo.ID, isBatch);
                    break;
                case "3":
                    this.ucMaterialItemList1.ShowInfoList(sqlIndex, filterField, formatStr);
                    break;
            }

            return 1;
        }

        /// <summary>
        /// 设置待选择数据显示
        /// </summary>
        /// <param name="label"></param>
        /// <param name="width"></param>
        /// <param name="visible"></param>
        /// <returns></returns>
        public void SetSelectFormat(string[] label, int[] width, bool[] visible)
        {
            this.ucMaterialItemList1.SetFormat(label, width, visible);
        }

        /// <summary>
        /// 获取显示数据的第一列到指定列宽度
        /// </summary>
        /// <param name="columnNum">需计算的列数量</param>
        /// <param name="width">返回的宽度</param>
        protected void GetColumnWidth(int iColumn, ref int iWidth)
        {
            this.ucMaterialItemList1.GetColumnWidth(iColumn, ref iWidth);
        }

        /// <summary>
        /// 设置列表数据宽度 显示指定列
        /// </summary>
        /// <param name="showColumnCount">显示指定列个数 不计算隐藏列</param>
        public void SetItemListWidth(int showColumnCount)
        {
            int iWidth = this.panelItemSelect.Width;

            this.ucMaterialItemList1.GetColumnWidth(showColumnCount, ref iWidth);

            this.panelItemSelect.Width = iWidth + 5;
        }

        /// <summary>
        /// 过滤 
        /// </summary>
        /// <param name="filterData"></param>
        protected override void Filter(string filterData)
        {
            this.IManager.Filter(filterData);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            FS.FrameWork.Management.DataBaseManger dataManager = new DataBaseManger();

            FS.FrameWork.Models.NeuObject class2Priv = new FS.FrameWork.Models.NeuObject();
            class2Priv.ID = "0510";
            class2Priv.Name = "入库";
            this.Class2Priv = class2Priv;       //入库

            //由权限科室获取
            //this.DeptInfo = ((FS.HISFC.Models.Base.Employee)dataManager.Operator).Dept;
            this.OperInfo = dataManager.Operator;
            this.OperInfo.Memo = "in";

            FS.HISFC.BizLogic.Manager.Department managerIntegrate = new FS.HISFC.BizLogic.Manager.Department();
            FS.HISFC.Models.Base.Department dept = managerIntegrate.GetDeptmentById(this.DeptInfo.ID);
            if (dept != null)
                this.DeptInfo.Memo = dept.DeptType.ID.ToString();

            if (this.FilePath == "")
            {
                this.FilePath = @"\Setting\PhaInSetting.xml";
            }

            if (this.SetPrivType(true) == -1)
            {
                return;
            }
            this.SetCancelVisible(false);
            this.GetInterface();
        }

        private void SetCancelVisible(bool p)
        {
            
        }

        protected override void FilterPriv(ref List<FS.FrameWork.Models.NeuObject> privList)
        {
            for (int i = privList.Count - 1; i >= 0; i--)
            {
                FS.FrameWork.Models.NeuObject priv = privList[i] as FS.FrameWork.Models.NeuObject;

                ////后勤库房	屏蔽内部入库申请 内部入库退库申请  物资申购

                if (this.DeptInfo.Memo == "L")
                {
                    if (priv.Memo == "13" || priv.Memo == "18")
                    {
                        privList.Remove(priv);
                    }
                }

                //非后勤库房 屏蔽一般入库、特殊入库、发票入库、入库退库
                if (this.DeptInfo.Memo != "L")
                {
                    if (priv.Memo == "11" || priv.Memo == "1C" || priv.Memo == "1A" || priv.Memo == "19")
                    {
                        privList.Remove(priv);
                    }
                }
            }
        }

        /// <summary>
        /// 初始化Fp信息
        /// </summary>
        public void InitFp()
        {
            FarPoint.Win.Spread.InputMap im;

            im = this.neuSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
        }

        /// <summary>
        /// 设置初始焦点
        /// </summary>
        public void SetFocus()
        {
            if (this.IsShowItemSelectpanel)
            {
                this.ucMaterialItemList1.SetFocusSelect();
            }
            else
            {
                if (this.FpSheetView.Rows.Count > 0)
                {
                    this.IManager.SetFocusSelect();
                }
            }
        }

        /// <summary>
        /// 激活Fp
        /// </summary>
        public void SetFpFocus()
        {
            this.neuSpread1.Select();
        }

        protected override void Clear()
        {
            base.Clear();

            if (this.ucMaterialItemList1 != null)
            {
                this.ucMaterialItemList1.Clear();
            }

            this.FpSheetView.Reset();

            this.FpSheetView.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.FpSheetView.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin3", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);

            this.Fp.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.Fp.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;

            this.InitFp();
        }

        /// <summary>
        /// 设置接口实例
        /// </summary>
        private void GetInterface()
        {
            this.Clear();

            //通过反射方式读取Factory文件 这样 可以将 Factory与其他相关的类型文件全部挪出UFC 实现自定义            
            //.
            

            ////入库界面 对于申请权限的会报错...
            //if (this.PrivType.Memo == "13" || this.PrivType.Memo == "18")
            //{
            //    this.PrivType.Memo = "11";
            //    this.PrivType.Name = "一般入库";
            //}
            //..


            //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
            //先释放掉事件资源
            if (this.IManager != null)
            {
                this.IManager.Dispose();
            }

            if (this.matFactory == null)
            {
                this.matFactory = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.Components.Material.IFactory)) as FS.HISFC.Components.Material.IFactory;
            }

            if (this.matFactory == null)
            {
                MatFactory factory = new MatFactory();
                this.matFactory = factory as FS.HISFC.Components.Material.IFactory;
            }
            this.IManager = this.matFactory.GetInInstance(this.PrivType, this);

            if (this.IManager == null)
            {
                System.Windows.Forms.MessageBox.Show("根据入库类别获取对应接口实例失败");
                return;
            }

            this.neuSpread1_Sheet1.DataAutoSizeColumns = false;

            this.neuSpread1_Sheet1.DataSource = null;
            //为了实现过滤 赋值DefaultView
            DataTable dtTemp = this.IManager.InitDataTable();
            if (dtTemp != null)
                this.neuSpread1_Sheet1.DataSource = dtTemp.DefaultView;
            else
                this.neuSpread1_Sheet1.DataSource = dtTemp;

            this.IManager.SetFormat();				//格式化

            this.IManager.SetFocusSelect();			//焦点设置

            this.neuPanel1.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            this.neuPanel4.SuspendLayout();
            this.panelItemSelect.SuspendLayout();
            this.SuspendLayout();

            this.AddItemInputUC(this.IManager.InputModualUC);

            this.neuPanel1.ResumeLayout();
            this.neuPanel3.ResumeLayout();
            this.neuPanel4.ResumeLayout();
            this.panelItemSelect.ResumeLayout();
            this.ResumeLayout();
        }


        #region 工具栏
        /*
        protected override int OnSave()
        {
            if (this.IManager != null)
            {
                this.IManager.Save();
            }

            return base.OnSave();
        }

        protected override int OnShowApplyList()
        {
            if (this.IManager != null)
            {
                this.IManager.ShowApplyList(false);
                AddNote();
            }

            return base.OnShowApplyList();
        }

        protected override int OnShowInList()
        {
            if (this.IManager != null)
            {
                this.IManager.ShowInList();
                AddNote();
            }

            return base.OnShowInList();
        }

        protected override int OnShowOutList()
        {
            if (this.IManager != null)
            {
                this.IManager.ShowOutList();
            }

            return base.OnShowOutList();
        }

        protected override int OnDel()
        {
            if (this.IManager != null)
            {
                if (this.neuSpread1_Sheet1.Rows.Count > 0)
                {
                    this.IManager.Delete(this.neuSpread1_Sheet1, this.neuSpread1_Sheet1.ActiveRowIndex);
                }
            }

            return base.OnDel();
        }

        protected override int OnShowStockList()
        {
            if (this.IManager != null)
            {
                this.IManager.ShowStockList();
                AddNote();
            }
            return base.OnShowStockList();
        }
        */
        protected override int OnSave(object sender, object neuObject)
        {
            this.neuSpread1.StopCellEditing();

            this.IManager.Save();

            this.neuSpread1.StartCellEditing(null, false);

            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.IManager.Print();
            return 1;
        }

        public override void OnApplyList()
        {
            this.IManager.ShowApplyList();
        }

        public override void OnInList()
        {
            this.IManager.ShowInList();
        }

        public override void OnStockList()
        {
            this.IManager.ShowStockList();
        }

        public override void OnOutList()
        {
            this.IManager.ShowOutList();
        }

        public override void OnDelete()
        {
            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                this.IManager.Delete(this.neuSpread1_Sheet1, this.neuSpread1_Sheet1.ActiveRowIndex);
            }
        }

        public override void OnImport()
        {
            if (this.IManager != null)
            {
                this.IManager.ImportData();
            }
        }

        #endregion

        public void AddNote(int codeIndex,int noteIndex)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                string itemCode = this.neuSpread1_Sheet1.Cells[i, codeIndex].Text.Trim();

                string note = Function.GetNote(itemCode);

                if (note.Length > 0)
                {
                    this.neuSpread1_Sheet1.Cells[i, noteIndex].BackColor = Color.LightCoral;
                    this.neuSpread1_Sheet1.SetNote(i, noteIndex, note);
                }
            }
        }

        private void ucMatIn_Load(object sender, EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                //FS.FrameWork.Models.NeuObject testPrivDept = new FS.FrameWork.Models.NeuObject();
                //int parma = FS.HISFC.Components.Common.Classes.Function.ChoosePivDept("0510", ref testPrivDept);

                //if (parma == -1)            //无权限
                //{
                //    MessageBox.Show(Language.Msg("您无此窗口操作权限"));
                //    return;
                //}
                //else if (parma == 0)       //用户选择取消
                //{
                //    return;
                //}

                //this.DeptInfo = testPrivDept;

                //base.OnStatusBarInfo(null, "操作科室： " + testPrivDept.Name);

                //需要在此处设置 操作科室                

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在进行数据初始化...请稍候");
                Application.DoEvents();

                this.Init();

                this.InitFp();

                if (this.IManager != null)
                {
                    this.IManager.SetFocusSelect();
                }

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }

            //this.rbnPRe.Visible = true;
            //this.rbnAfter.Visible = true;
            return;
        }

        protected override void OnEndPrivChanged(FS.FrameWork.Models.NeuObject changeData, object param)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在根据入库类别加载界面 请稍候..");
            Application.DoEvents();

            this.GetInterface();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            base.OnEndPrivChanged(changeData, param);

            if (changeData.Memo == "11")
            {
                //this.rbnPRe.Visible = true;
                //this.rbnAfter.Visible = true;
            }
            else
            {
            //    this.rbnPRe.Visible = false;
            //    this.rbnAfter.Visible = false;
            }
        }

        protected override void OnEndTargetChanged(FS.FrameWork.Models.NeuObject changeData, object param)
        {
            if (this.IManager != null)
            {
                this.IManager.SetFocusSelect();
            }

            base.OnEndTargetChanged(changeData, param);
        }

        private void ucMaterialItemList1_ChooseDataEvent(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            if (sv != null && activeRow >= 0)
            {
                if (this.IManager != null)
                {
                    if (this.IManager.AddItem(sv, activeRow) == -1)
                    {
                        this.ucMaterialItemList1.SetFocusSelect();
                    }
                }
            }
        }

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { 
                    typeof(FS.HISFC.Components.Material.IFactory),
                    typeof(FS.HISFC.BizProcess.Interface.Material.IBillPrint)
                };
            }
        }

        #endregion

        #region IPreArrange 成员

        public int PreArrange()
        {
            FS.FrameWork.Models.NeuObject testPrivDept = new FS.FrameWork.Models.NeuObject();
            int parma = FS.HISFC.Components.Common.Classes.Function.ChoosePivDept("0510", ref testPrivDept);

            if (parma == -1)            //无权限
            {
                MessageBox.Show(Language.Msg("您无此窗口操作权限"));
                return -1;
            }
            else if (parma == 0)       //用户选择取消
            {
                return -1;
            }

            this.DeptInfo = testPrivDept;

            base.OnStatusBarInfo(null, "操作科室： " + testPrivDept.Name);

            return 1;
        }

        #endregion
    }
}
