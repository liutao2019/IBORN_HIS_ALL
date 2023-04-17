using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Material.Base
{
    public partial class ucMatAddRate : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucMatAddRate()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// toolbarservice
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 加价率业务层
        /// </summary>
        private FS.HISFC.BizLogic.Material.Baseset addRateManager = new FS.HISFC.BizLogic.Material.Baseset();

        private FS.FrameWork.Public.ObjectHelper helper = new FS.FrameWork.Public.ObjectHelper();

        ArrayList alRateKindInfo = new ArrayList();

        private FS.FrameWork.Models.NeuObject oper = FS.FrameWork.Management.Connection.Operator;

        #endregion

        #region 私有方法

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {
            this.InitTreeView();
            //this.ucMaterialKindTree1.GetLak += new ucMaterialKindTree.GetLevelAndKind(ucMaterialKindTree1_GetLak);
            this.SetColumnType();
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        protected virtual void InitTreeView()
        {
            this.tvRateKind.ImageList = this.tvRateKind.groupImageList;
            this.tvRateKind.Nodes.Clear();
            TreeNode title = new TreeNode();
            title.Text = "加价方式";
            title.ImageIndex = 1;
            this.tvRateKind.Nodes.Add(title);
            FS.HISFC.Models.Material.MaterialAddRateEnumService matAddRateEnum = new FS.HISFC.Models.Material.MaterialAddRateEnumService();
            
            ArrayList al = FS.HISFC.Models.Material.MaterialAddRateEnumService.List();
            this.alRateKindInfo = al;
            this.helper.ArrayObject = al;

            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                TreeNode rateKindNode = new TreeNode();

                rateKindNode.ImageIndex = 3;
                rateKindNode.SelectedImageIndex = 4;
                rateKindNode.Text = obj.Name;
                rateKindNode.Tag = obj.ID;

                title.Nodes.Add(rateKindNode);
            }
            
            this.tvRateKind.ExpandAll();
        }

        /// <summary>
        /// 表格列设置
        /// </summary>
        protected virtual void SetColumnType()
        {
            FS.HISFC.Models.Material.MaterialAddRate addRate = new FS.HISFC.Models.Material.MaterialAddRate();
            FarPoint.Win.Spread.CellType.ComboBoxCellType cmbRateKindCellType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numType = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType dtType = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            
            cmbRateKindCellType.Items = addRate.RateKind.StringItems;

            this.FpAddRate_Sheet1.Columns[0].Visible = false;
            this.FpAddRate_Sheet1.Columns[1].Visible = false;
            this.FpAddRate_Sheet1.Columns[2].CellType = cmbRateKindCellType;
            this.FpAddRate_Sheet1.Columns[2].Locked = true;
            this.FpAddRate_Sheet1.Columns[4].CellType = numType;
            this.FpAddRate_Sheet1.Columns[5].CellType = numType;
            this.FpAddRate_Sheet1.Columns[6].CellType = numType;
            this.FpAddRate_Sheet1.Columns[7].CellType = numType;
            this.FpAddRate_Sheet1.Columns[8].Visible = false;
            this.FpAddRate_Sheet1.Columns[9].CellType = dtType;
            this.FpAddRate_Sheet1.Columns[10].Visible = false;
        }

        /// <summary>
        /// 添加新行
        /// </summary>
        protected virtual void AddNewRow()
        {
            if (this.tvRateKind.SelectedNode.Tag != null)
            {
                int row = this.FpAddRate_Sheet1.RowCount;
                //{6C1CAA95-490C-4e40-B351-6CE306472BB6}
                if (this.tvRateKind.SelectedNode.Tag.ToString() == "R" && row >= 1)
                {
                    MessageBox.Show("固定加价率只能有一条，如要添加，请先删除以前的加价率数据。");

                    return;
                }
                this.FpAddRate_Sheet1.Rows.Add(row, 1);
                this.FpAddRate_Sheet1.Cells[row, 0].Text = "ADD";
                this.FpAddRate_Sheet1.Cells[row, 8].Text = oper.ID;
                this.FpAddRate_Sheet1.Cells[row, 2].Text = this.helper.GetName(this.tvRateKind.SelectedNode.Tag.ToString());
            }
        }

        /// <summary>
        /// 取得表格中的加价率实体
        /// </summary>
        /// <param name="rowIndex">行号</param>
        /// <returns></returns>
        protected virtual FS.HISFC.Models.Material.MaterialAddRate GetAddRateFromFp(int rowIndex)
        {
            if (rowIndex >= 0)
            {
                FS.HISFC.Models.Material.MaterialAddRate obj = new FS.HISFC.Models.Material.MaterialAddRate();
                FS.FrameWork.Public.ObjectHelper myHelper = new FS.FrameWork.Public.ObjectHelper();


                obj.ID = this.FpAddRate_Sheet1.Cells[rowIndex, 1].Text.Trim();

                obj.RateKind.ID = this.helper.GetID(this.FpAddRate_Sheet1.Cells[rowIndex, 2].Text.Trim());
                obj.Specs = this.FpAddRate_Sheet1.Cells[rowIndex, 3].Text.Trim();
                obj.PriceLow = FS.FrameWork.Function.NConvert.ToDecimal(this.FpAddRate_Sheet1.Cells[rowIndex, 4].Text.Trim());
                obj.PriceHigh = FS.FrameWork.Function.NConvert.ToDecimal(this.FpAddRate_Sheet1.Cells[rowIndex, 5].Text.Trim());
                obj.AddRate = FS.FrameWork.Function.NConvert.ToDecimal(this.FpAddRate_Sheet1.Cells[rowIndex, 6].Text.Trim());
                obj.AppendFee = FS.FrameWork.Function.NConvert.ToDecimal(this.FpAddRate_Sheet1.Cells[rowIndex, 7].Text.Trim());
                obj.Oper.ID = this.FpAddRate_Sheet1.Cells[rowIndex, 8].Text.Trim();
                obj.MaterialKind.ID = this.FpAddRate_Sheet1.Cells[rowIndex, 10].Text.Trim();
                return obj;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 向表格添加数据
        /// </summary>
        /// <param name="alAddRate"></param>
        protected virtual void AddDataToFp(ArrayList alAddRate)
        {
            if (alAddRate != null && alAddRate.Count > 0)
            {
                foreach (FS.HISFC.Models.Material.MaterialAddRate obj in alAddRate)
                {
                    ArrayList alMatKind = new ArrayList();
                    FS.HISFC.Models.Material.MaterialKind matKind = new FS.HISFC.Models.Material.MaterialKind();
                    alMatKind = this.addRateManager.QueryKindAllByID(obj.MaterialKind.ID);
                    if (alMatKind.Count > 0)
                    {
                        matKind = alMatKind[0] as FS.HISFC.Models.Material.MaterialKind;
                    }
                    int row = this.FpAddRate_Sheet1.RowCount;
                    this.FpAddRate_Sheet1.Rows.Add(row, 1);
                    this.FpAddRate_Sheet1.Cells[row, 1].Text = obj.ID;
                    this.FpAddRate_Sheet1.Cells[row, 2].Text = obj.RateKind.Name;
                    this.FpAddRate_Sheet1.Cells[row, 3].Text = obj.Specs;
                    this.FpAddRate_Sheet1.Cells[row, 4].Text = obj.PriceLow.ToString();
                    this.FpAddRate_Sheet1.Cells[row, 5].Text = obj.PriceHigh.ToString();
                    this.FpAddRate_Sheet1.Cells[row, 6].Text = obj.AddRate.ToString();
                    this.FpAddRate_Sheet1.Cells[row, 7].Text = obj.AppendFee.ToString();
                    this.FpAddRate_Sheet1.Cells[row, 8].Text = obj.Oper.ID;
                    this.FpAddRate_Sheet1.Cells[row, 9].Value = obj.OperDate;
                    this.FpAddRate_Sheet1.Cells[row, 10].Text = matKind.Name;
                    this.FpAddRate_Sheet1.Cells[row, 10].Tag = obj.MaterialKind.ID;

                    this.FpAddRate_Sheet1.Rows[row].Tag = obj;
                }
            }
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <returns></returns>
        protected virtual int DeleteAddRate()
        {
            if (this.FpAddRate_Sheet1.RowCount > 0)
            {
                int iSelect = this.FpAddRate_Sheet1.ActiveRowIndex;

                string addRateID = this.FpAddRate_Sheet1.Cells[iSelect, 1].Text.Trim();

                DialogResult r = MessageBox.Show("是否要删除选择的加价率信息？此操作不可回退！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r == DialogResult.No)
                    return 0;

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.addRateManager.Connection);
                //t.BeginTransaction();

                this.addRateManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                int iReturn = this.addRateManager.DeleteAddRate(addRateID);
                if (iReturn < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("删除加价率信息失败！" + this.addRateManager.Err);
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                this.FpAddRate_Sheet1.Rows.Remove(iSelect, 1);
            }
            else
            {
                MessageBox.Show("没有选择的数据！");
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        protected virtual int SaveAddRate()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.addRateManager.Connection);
            //t.BeginTransaction();

            this.addRateManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            for (int i = 0; i < this.FpAddRate_Sheet1.RowCount; i++)
            {
                if (this.FpAddRate_Sheet1.Cells[i, 0].Text == "ADD")
                {
                    FS.HISFC.Models.Material.MaterialAddRate obj = this.GetAddRateFromFp(i);
                    obj.ID = this.addRateManager.GetNewAddRateID();
                    obj.Oper.ID = this.oper.ID;
                    obj.OperDate = this.addRateManager.GetDateTimeFromSysDateTime();

                    if (this.addRateManager.InsertAddRate(obj) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("插入加价率信息失败！" + this.addRateManager.Err);
                        return -1;
                    }
                }
                else if (this.FpAddRate_Sheet1.Cells[i, 0].Text == "MODIFY")
                {
                    FS.HISFC.Models.Material.MaterialAddRate obj = this.GetAddRateFromFp(i);
                    
                    obj.Oper.ID = this.oper.ID;
                    obj.OperDate = this.addRateManager.GetDateTimeFromSysDateTime();

                    if (this.addRateManager.UpdateAddRate(obj) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新加价率信息失败！" + this.addRateManager.Err);
                        return -1;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("保存成功！");

            ArrayList al = this.addRateManager.QueryAddRateByRateKind(this.tvRateKind.SelectedNode.Tag.ToString());
            this.FpAddRate_Sheet1.RowCount = 0;
            this.AddDataToFp(al);

            return 1;
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveAddRate();
            return base.OnSave(sender, neuObject);
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("添加", "在表格中添加一条", FS.FrameWork.WinForms.Classes.EnumImageList.T添加, true, false, null);
            toolBarService.AddToolButton("删除", "删除选中明细信息", FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            
            return this.toolBarService;
        }

        #endregion

        #region 共有方法

        #endregion

        #region 事件


        //private void ucMaterialKindTree1_GetLak(object sender, TreeViewEventArgs e)
        //{
        //    string matKindID = e.Node.Tag.ToString();
        //    this.FpAddRate_Sheet1.RowCount = 0;
        //    ArrayList alAddRate = new ArrayList();
        //    alAddRate = this.addRateManager.QueryAddRateByMatKind(matKindID);
        //    this.AddDataToFp(alAddRate);
        //}

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "添加":
                    this.AddNewRow();
                    break;
                case "删除":
                    this.DeleteAddRate();
                    break;
            }
            
            base.ToolStrip_ItemClicked(sender, e);
        }

        private void ucMatAddRate_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void tvRateKind_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                ArrayList al = this.addRateManager.QueryAddRateByRateKind(e.Node.Tag.ToString());
                this.FpAddRate_Sheet1.RowCount = 0;
                this.AddDataToFp(al);
            }
        }

        private void FpAddRate_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (this.FpAddRate_Sheet1.Cells[e.Row, 0].Text == "")
            {
                this.FpAddRate_Sheet1.Cells[e.Row,0].Text = "MODIFY";
            }
        }

        #endregion
    }
}

