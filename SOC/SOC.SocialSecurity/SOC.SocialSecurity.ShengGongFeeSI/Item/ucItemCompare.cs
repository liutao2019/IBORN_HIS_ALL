using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.SocialSecurity.ShengGongFeeSI.Item
{
    public partial class ucItemCompare : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucItemCompare()
        {
            InitializeComponent();
        }

        #region 枚举

        /// <summary>
        /// 中心码列表枚举
        /// </summary>
        public enum CompareEnum
        {
            选择,
            编码,
            上传码,
            名称,
            规格,
            价格,
            中心码,
            中心名称,
            剂型,
            报销等级,
            比例,
            类别,
            费用类别,
            审批,
            匹配者,
            匹配时间,
            中心拼音码,
            中心五笔码,
            拼音码,
            五笔码
        }

        /// <summary>
        /// 中心码列表枚举
        /// </summary>
        public enum CenterEnum
        {
            中心码,
            中心名称,
            规格,
            剂型,
            报销等级,
            比例,
            类别,
            费用类别,
            拼音码,
            五笔码,
            备注
        }

        /// <summary>
        /// 本地列表枚举
        /// </summary>
        public enum LocalEnum
        {
            上传码,
            编码,
            名称,
            规格,
            价格,
            剂型,
            费用类别,
            拼音码,
            五笔码,
            生产单位
        }

        #endregion

        #region 变量

        private DataTable dtCenterItem = new DataTable();
        private DataTable dtLocalNoCompareItem = new DataTable();
        private DataTable dtCompareItem = new DataTable();
        FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 合同单位类型
        /// </summary>
        private string pactType = "shgy";

        #endregion

        #region 方法

        /// <summary>
        /// 合同单位类型
        /// </summary>
        public string PactType
        {
            get
            {
                return pactType;
            }
            set
            {
                pactType = value;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public void InitData()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载数据，请稍后...");
            Application.DoEvents();
            DataSet dsCenterItem = this.getCenterItem(this.pactType);
            DataSet dsLocalNoCompareItem = this.getLocalNoCompareItem(this.pactType);
            DataSet dsCompareItem = this.getCompareItem(this.pactType);

            this.spreadCenterItem_Sheet1.DataAutoSizeColumns = false;
            this.spreadCenterItem_Sheet1.DataAutoCellTypes = false;
            dtCenterItem = dsCenterItem.Tables[0].Copy();
            dtCenterItem.DefaultView.Sort = CenterEnum.中心码.ToString() + "," + CenterEnum.中心名称.ToString();
            this.spreadCenterItem_Sheet1.DataSource = dtCenterItem.DefaultView;

            this.spreadHISItem_Sheet1.DataAutoSizeColumns = false;
            this.spreadHISItem_Sheet1.DataAutoCellTypes = false;
            dtLocalNoCompareItem = dsLocalNoCompareItem.Tables[0].Copy();
            dtLocalNoCompareItem.DefaultView.Sort = LocalEnum.上传码.ToString() + "," + LocalEnum.名称.ToString();
            this.spreadHISItem_Sheet1.DataSource = dtLocalNoCompareItem.DefaultView;

            this.spreadCompareItem_Sheet1.DataAutoSizeColumns = false;
            this.spreadCompareItem_Sheet1.DataAutoCellTypes = false;
            dtCompareItem = dsCompareItem.Tables[0].Copy();
            dtCompareItem.DefaultView.Sort = CompareEnum.匹配时间.ToString() + " desc";
            this.spreadCompareItem_Sheet1.DataSource = dtCompareItem.DefaultView;


            #region cmb

            //西药（01）、中成药（02）、中药饮片（03）、医院制剂（04）、诊疗项目和医疗服务设施（05）、医用材料（06）
            ArrayList alTemp = new FS.HISFC.BizLogic.Manager.Constant().GetAllList("ShgyCenterSysClass");
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "ALL";
            obj.Name = "全部";
            alTemp.Insert(0, obj);
            this.cmbCenterSysClass.AddItems(alTemp);
            this.cmbCenterSysClass.SelectedIndex = 0;

             alTemp = new FS.HISFC.BizLogic.Manager.Constant().GetAllList("MINFEE");
            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "ALL";
            obj.Name = "全部";
            alTemp.Insert(0, obj);
            this.cmbLocalSysClass.AddItems(alTemp);
            this.cmbLocalSysClass.SelectedIndex = 0;

            #endregion
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        /// 去掉Tab键和Enter键
        /// </summary>
        public void InitSpread()
        {

            FarPoint.Win.Spread.InputMap im = null;
            im = this.spreadCenterItem.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused, FarPoint.Win.Spread.OperationMode.RowMode);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Tab, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Shift&Keys.Tab, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.spreadHISItem.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused, FarPoint.Win.Spread.OperationMode.RowMode);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Tab, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Shift & Keys.Tab, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

           
        }

        /// <summary>
        /// 加载事件
        /// </summary>
        public void InitEvent()
        {
            this.spreadHISItem.MouseDown += new MouseEventHandler(spreadHISItem_MouseDown);
            this.spreadCenterItem.DragEnter += new DragEventHandler(spreadCenterItem_DragEnter);
            this.spreadCenterItem.DragDrop += new DragEventHandler(spreadCenterItem_DragDrop);
            this.spreadCenterItem.DragOver += new DragEventHandler(spreadCenterItem_DragOver);

            this.txtCenter.TextChanged += new EventHandler(txtCenter_TextChanged);
            this.txtLocal.TextChanged += new EventHandler(txtLocal_TextChanged);
            this.txtCompare.TextChanged += new EventHandler(txtCompare_TextChanged);
            this.cmbCenterSysClass.SelectedIndexChanged += new EventHandler(cmbCenterSysClass_SelectedIndexChanged);
            this.cmbLocalSysClass.SelectedIndexChanged += new EventHandler(cmbLocalSysClass_SelectedIndexChanged);

            this.spreadCompareItem.MouseDown += new MouseEventHandler(spreadCompareItem_MouseDown);
            this.spreadHISItem.DragEnter += new DragEventHandler(spreadHISItem_DragEnter);
            this.spreadHISItem.DragDrop += new DragEventHandler(spreadHISItem_DragDrop);
            this.spreadHISItem.DragOver += new DragEventHandler(spreadHISItem_DragOver);

            this.spreadCenterItem.KeyUp += new KeyEventHandler(spreadCenterItem_KeyUp);
            this.spreadHISItem.KeyUp += new KeyEventHandler(spreadHISItem_KeyUp);

            this.txtCenter.KeyDown += new KeyEventHandler(txtCenter_KeyDown);
            this.txtLocal.KeyDown += new KeyEventHandler(txtLocal_KeyDown);
            this.txtCompare.KeyDown += new KeyEventHandler(txtCompare_KeyDown);
        }

        /// <summary>
        /// 删除对照信息
        /// </summary>
        public void Delete()
        {
            int row = this.spreadCompareItem_Sheet1.RowCount;
            bool deleteYes = false;
            for (int i = row - 1; i >= 0; i--)
            {
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.spreadCompareItem_Sheet1.Cells[i, (int)CompareEnum.选择].Value))
                {
                    if (deleteYes == false)
                    {
                        if (MessageBox.Show(this, "确定要删除这些对照信息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            deleteYes = true;
                        }
                    }
                    
                    if(deleteYes)
                    {
                        if (this.deleteCompareItem(this.pactType, this.spreadCompareItem_Sheet1.Cells[i, (int)CompareEnum.中心码].Text, this.spreadCompareItem_Sheet1.Cells[i, (int)CompareEnum.编码].Text) <= 0)
                        {
                            MessageBox.Show(this, "删除对照信息失败，" + dbMgr.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        } 

                        //插入已对照的信息
                        DataSet ds = this.getLocalNoCompareItem(this.pactType,  this.spreadCompareItem_Sheet1.Cells[i, (int)CompareEnum.编码].Text);
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow drv = dtLocalNoCompareItem.NewRow();
                            foreach (int s in Enum.GetValues(typeof(LocalEnum)))
                            {
                                drv[s] = ds.Tables[0].Rows[0][s];
                            }
                            dtLocalNoCompareItem.Rows.InsertAt(drv, 0);
                        }

                        
                        this.spreadCompareItem_Sheet1.Cells[i, (int)CompareEnum.选择].Value = false;
                        this.spreadCompareItem_Sheet1.RemoveRows(i, 1);

                    }

                }
            }
        }

        /// <summary>
        /// 删除对照信息
        /// </summary>
        public void Delete(int row)
        {
            string hisName = this.spreadCompareItem.ActiveSheet.Cells[row, (int)CompareEnum.名称].Text;
            //获取CenterCode
            string centerName = this.spreadCompareItem.ActiveSheet.Cells[row, (int)CompareEnum.中心名称].Text;

            if (MessageBox.Show(this, string.Format("确定要删除【{0}】与【{1}】的对照信息？",centerName,hisName), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            string hisCode = this.spreadCompareItem.ActiveSheet.Cells[row, (int)CompareEnum.编码].Text;
            //获取CenterCode
            string centerCode = this.spreadCompareItem.ActiveSheet.Cells[row, (int)CompareEnum.中心码].Text;

            if (this.deleteCompareItem(this.pactType, centerCode,hisCode) <= 0)
            {
                MessageBox.Show(this, "删除对照信息失败，" + dbMgr.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            //插入已对照的信息
            DataSet ds = this.getLocalNoCompareItem(this.pactType,hisCode);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow drv = dtLocalNoCompareItem.NewRow();
                foreach (int s in Enum.GetValues(typeof(LocalEnum)))
                {
                    drv[s] = ds.Tables[0].Rows[0][s];
                }
                dtLocalNoCompareItem.Rows.InsertAt(drv, 0);
            }


            this.spreadCompareItem_Sheet1.Cells[row, (int)CompareEnum.选择].Value = false;
            this.spreadCompareItem_Sheet1.RemoveRows(row, 1);

        }

        /// <summary>
        /// 保存对照信息
        /// </summary>
        public int Save()
        {
            int hisRow = this.spreadHISItem.ActiveSheet.ActiveRowIndex;
            if (hisRow < 0)
            {
                return 0;
            }

            int centerRow = this.spreadCenterItem.ActiveSheet.ActiveRowIndex;
            if (centerRow < 0)
            {
                return 0;
            }

            string hisName = this.spreadHISItem.ActiveSheet.Cells[hisRow, (int)LocalEnum.名称].Text;
            //获取CenterCode
            string centerName = this.spreadCenterItem.ActiveSheet.Cells[centerRow, (int)CenterEnum.中心名称].Text;

            if (MessageBox.Show(this, string.Format("确定要将【{0}】与【{1}】对照吗？", centerName, hisName), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return -1;
            }

            string hisCode = this.spreadHISItem.ActiveSheet.Cells[hisRow, (int)LocalEnum.编码].Text;
            //获取CenterCode
            string centerCode = this.spreadCenterItem.ActiveSheet.Cells[centerRow, (int)CenterEnum.中心码].Text;

            if (this.insertCompareItem(this.pactType, centerCode, hisCode) > 0)
            {
                //插入已对照的信息
                DataSet ds = this.getCompareItem(this.pactType, centerCode, hisCode);
                DataRow drv = dtCompareItem.NewRow();

                foreach (int s in Enum.GetValues(typeof(CompareEnum)))
                {
                    drv[s] = ds.Tables[0].Rows[0][s];
                }

                dtCompareItem.Rows.InsertAt(drv, 0);

                //删除待对照的信息
                this.spreadHISItem_Sheet1.RemoveRows(this.spreadHISItem_Sheet1.ActiveRowIndex, 1);
            }

            return 1;
        }

        #endregion

        #region 事件

        #region 重写

        /// <summary>
        /// 窗口加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            this.InitData();
            this.InitSpread();
            this.InitEvent();
            base.OnLoad(e);
        }

        /// <summary>
        /// 点查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.InitData();

            return base.OnQuery(sender, neuObject);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            spreadHISItem_KeyUp(sender, new KeyEventArgs(Keys.Enter));
            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// 工具栏加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("删除", "删除对照信息", FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            this.toolBarService.AddToolButton("审批", "审批对照信息", FS.FrameWork.WinForms.Classes.EnumImageList.Q全选, true, false, null);
            this.toolBarService.AddToolButton("取消审批", "取消审批对照信息", FS.FrameWork.WinForms.Classes.EnumImageList.Q全选取消, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// 自定义工具栏事件触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "删除":
                    this.Delete();
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// 窗口键盘按键事件
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                return true;
            }
           
            return base.ProcessDialogKey(keyData);
        }

        #endregion

        #region 拖拽

        void spreadHISItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left&&e.Clicks==1)
            {
                FarPoint.Win.Spread.Model.CellRange cr = this.spreadHISItem.GetCellFromPixel(0, 0, this.spreadHISItem.PointToClient(MousePosition).X, this.spreadHISItem.PointToClient(MousePosition).Y);
                if (cr != null)
                {
                    if (cr.Row == this.spreadHISItem.ActiveSheet.ActiveRowIndex)
                    {
                        FS.HISFC.Models.Base.Item item = new FS.HISFC.Models.Base.Item();
                        item.ID=this.spreadHISItem.ActiveSheet.Cells[cr.Row,(int)LocalEnum.编码].Text;
                        spreadHISItem.DoDragDrop(item, DragDropEffects.Copy | DragDropEffects.Move);
                        this.spreadHISItem.ActiveSheet.ActiveColumnIndex = cr.Column;
                        this.spreadHISItem.Focus();
                    }
                }
            }
            else if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                spreadHISItem_KeyUp(sender, new KeyEventArgs(Keys.Enter));
            }
        }

        void spreadCenterItem_DragEnter(object sender, DragEventArgs e)
        {
            FarPoint.Win.Spread.Model.CellRange cr = this.spreadCenterItem.GetCellFromPixel(0, 0, this.spreadCenterItem.PointToClient(MousePosition).X, this.spreadCenterItem.PointToClient(MousePosition).Y);
            if (cr.Column != -1 && cr.Row != -1)
            { 
                if (e.Data.GetData(typeof(FS.HISFC.Models.Base.Item)) !=null)
                {
                    e.Effect = DragDropEffects.Move | DragDropEffects.Scroll;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        void spreadCenterItem_DragDrop(object sender, DragEventArgs e)
        {

              FarPoint.Win.Spread.Model.CellRange cr = this.spreadCenterItem.GetCellFromPixel(0, 0, this.spreadCenterItem.PointToClient(MousePosition).X, this.spreadCenterItem.PointToClient(MousePosition).Y);
              if (cr.Column != -1 && cr.Row != -1)
              {
                  //获取数据
                  object o = e.Data.GetData(typeof(FS.HISFC.Models.Base.Item));
                  if (o != null)
                  {
                      //获取HISCode
                      FS.HISFC.Models.Base.Item item = o as FS.HISFC.Models.Base.Item;
                      string hisCode = item.ID;
                      //获取CenterCode
                      string centerCode = this.spreadCenterItem.ActiveSheet.Cells[cr.Row, (int)CenterEnum.中心码].Text;

                      if (this.insertCompareItem(this.pactType, centerCode, hisCode) > 0)
                      {
                          //插入已对照的信息
                          DataSet ds = this.getCompareItem(this.pactType, centerCode, hisCode);
                          DataRow drv = dtCompareItem.NewRow();

                          foreach (int s in Enum.GetValues(typeof(CompareEnum)))
                          {
                              drv[s] = ds.Tables[0].Rows[0][s];
                          }

                          dtCompareItem.Rows.InsertAt(drv, 0);

                          //删除待对照的信息
                          this.spreadHISItem_Sheet1.RemoveRows(this.spreadHISItem_Sheet1.ActiveRowIndex, 1);
                      }
                      else
                      {
                          MessageBox.Show(this, "插入对照信息失败，" + dbMgr.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                          return;
                      }
                  }
              }
        }

        void spreadCenterItem_DragOver(object sender, DragEventArgs e)
        {
            FarPoint.Win.Spread.Model.CellRange cr = this.spreadCenterItem.GetCellFromPixel(0, 0, this.spreadCenterItem.PointToClient(MousePosition).X, this.spreadCenterItem.PointToClient(MousePosition).Y);
            if (cr.Column != -1 && cr.Row != -1)
            {
                if (e.Data.GetData(typeof(FS.HISFC.Models.Base.Item)) != null)
                {
                    this.spreadCenterItem_Sheet1.AddSelection(cr.Row, cr.Column, 1, 1);
                    this.spreadCenterItem.ActiveSheet.ActiveRowIndex = cr.Row;
                    this.spreadCenterItem.ActiveSheet.ActiveColumnIndex = cr.Column;

                    e.Effect = DragDropEffects.Move | DragDropEffects.Scroll;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        void spreadCompareItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                FarPoint.Win.Spread.Model.CellRange cr = this.spreadCompareItem.GetCellFromPixel(0, 0, this.spreadCompareItem.PointToClient(MousePosition).X, this.spreadCompareItem.PointToClient(MousePosition).Y);
                if (cr != null)
                {
                    if (cr.Row == this.spreadCompareItem.ActiveSheet.ActiveRowIndex&&cr.Column!=(int)CompareEnum.选择)
                    {
                        FS.HISFC.Models.SIInterface.Compare compare = new FS.HISFC.Models.SIInterface.Compare();
                        compare.CenterItem.ID = this.spreadCompareItem.ActiveSheet.Cells[cr.Row, (int)CompareEnum.中心码].Text;
                        compare.HisCode = this.spreadCompareItem.ActiveSheet.Cells[cr.Row, (int)CompareEnum.编码].Text;
                        spreadCompareItem.DoDragDrop(compare, DragDropEffects.Copy | DragDropEffects.Move);
                        this.spreadCompareItem.ActiveSheet.ActiveColumnIndex = cr.Column;
                        this.spreadCompareItem.Focus();
                    }
                }
            }
            else if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                if (this.spreadCompareItem.ActiveSheet.ActiveRowIndex >= 0)
                {
                    this.Delete(this.spreadCompareItem.ActiveSheet.ActiveRowIndex);
                }
            }
        }

        void spreadHISItem_DragEnter(object sender, DragEventArgs e)
        {
            FarPoint.Win.Spread.Model.CellRange cr = this.spreadHISItem.GetCellFromPixel(0, 0, this.spreadHISItem.PointToClient(MousePosition).X, this.spreadHISItem.PointToClient(MousePosition).Y);

            if (e.Data.GetData(typeof(FS.HISFC.Models.SIInterface.Compare)) != null)
            {
                e.Effect = DragDropEffects.Move | DragDropEffects.Scroll;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        void spreadHISItem_DragDrop(object sender, DragEventArgs e)
        {
            FarPoint.Win.Spread.Model.CellRange cr = this.spreadHISItem.GetCellFromPixel(0, 0, this.spreadHISItem.PointToClient(MousePosition).X, this.spreadHISItem.PointToClient(MousePosition).Y);

            //获取数据
            object o = e.Data.GetData(typeof(FS.HISFC.Models.SIInterface.Compare));
            if (o != null)
            {
                //获取HISCode
                FS.HISFC.Models.SIInterface.Compare compare = o as FS.HISFC.Models.SIInterface.Compare;

                if (this.deleteCompareItem(this.pactType, compare.CenterItem.ID, compare.HisCode) > 0)
                {
                    //插入已对照的信息
                    DataSet ds = this.getLocalNoCompareItem(this.pactType, compare.HisCode);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow drv = dtLocalNoCompareItem.NewRow();
                        foreach (int s in Enum.GetValues(typeof(LocalEnum)))
                        {
                            drv[s] = ds.Tables[0].Rows[0][s];
                        }
                        dtLocalNoCompareItem.Rows.InsertAt(drv, 0);
                        //删除已对照的信息
                        this.spreadCompareItem_Sheet1.RemoveRows(this.spreadCompareItem_Sheet1.ActiveRowIndex, 1);
                    }


                }
                else
                {
                    MessageBox.Show(this, "删除对照信息失败，" + dbMgr.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        void spreadHISItem_DragOver(object sender, DragEventArgs e)
        {
            FarPoint.Win.Spread.Model.CellRange cr = this.spreadHISItem.GetCellFromPixel(0, 0, this.spreadHISItem.PointToClient(MousePosition).X, this.spreadHISItem.PointToClient(MousePosition).Y);

            if (e.Data.GetData(typeof(FS.HISFC.Models.SIInterface.Compare)) != null)
            {
                this.spreadHISItem_Sheet1.AddSelection(cr.Row, cr.Column, 1, 1);
                this.spreadHISItem.ActiveSheet.ActiveRowIndex = cr.Row;
                this.spreadHISItem.ActiveSheet.ActiveColumnIndex = cr.Column;

                e.Effect = DragDropEffects.Move | DragDropEffects.Scroll;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }
        #endregion

        #region 过滤
        void txtCenter_TextChanged(object sender, EventArgs e)
        {
            if (this.cmbCenterSysClass.Text == "全部")
            {
                this.dtCenterItem.DefaultView.RowFilter = CenterEnum.类别.ToString() + " like '%%'";
            }
            else
            {
                this.dtCenterItem.DefaultView.RowFilter = CenterEnum.类别.ToString() + " like '%" + this.cmbCenterSysClass.Text + "%'";
            }

            this.dtCenterItem.DefaultView.RowFilter += string.Format(" and (中心码 like '{0}%' or 中心名称 like '%{0}%' or 拼音码 like '%{0}%' or 五笔码 like '%{0}%')", this.txtCenter.Text.Trim());

            if (this.spreadCenterItem.ActiveSheet.RowCount > 0)
            {
                this.spreadCenterItem.ActiveSheet.ActiveRowIndex = 0;
            }
        }

        void txtLocal_TextChanged(object sender, EventArgs e)
        {
            if (this.cmbLocalSysClass.Text == "全部")
            {
                this.dtLocalNoCompareItem.DefaultView.RowFilter = LocalEnum.费用类别.ToString() + " like '%%'";
            }
            else
            {
                this.dtLocalNoCompareItem.DefaultView.RowFilter = LocalEnum.费用类别.ToString() + " like '%" + this.cmbLocalSysClass.Text + "%'";
            }

            this.dtLocalNoCompareItem.DefaultView.RowFilter += string.Format(" and (编码 like '%{0}%' or 上传码 like '{0}%' or 名称 like '%{0}%' or 拼音码 like '%{0}%' or 五笔码 like '%{0}%')", this.txtLocal.Text.Trim());
            if (this.spreadHISItem.ActiveSheet.RowCount > 0)
            {
                this.spreadHISItem.ActiveSheet.ActiveRowIndex = 0;
            }
        }

        void txtCompare_TextChanged(object sender, EventArgs e)
        {
            if (this.cmbCenterSysClass.Text == "全部")
            {
                this.dtCompareItem.DefaultView.RowFilter = CenterEnum.类别.ToString() + " like '%%'";
            }
            else
            {
                this.dtCompareItem.DefaultView.RowFilter = CenterEnum.类别.ToString() + " like '%" + this.cmbCenterSysClass.Text + "%'";
            }

            this.dtCompareItem.DefaultView.RowFilter += string.Format(" and (编码 like '%{0}%' or 上传码 like '{0}%' or 名称 like '%{0}%'  or 中心码 like '{0}%' or 中心名称 like '%{0}%' or 中心拼音码 like '%{0}%' or 中心五笔码 like '%{0}%' or 拼音码 like '%{0}%' or 五笔码  like '%{0}%')", this.txtCompare.Text.Trim());

        }

        void cmbLocalSysClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtLocal_TextChanged(sender, e);
        }

        void cmbCenterSysClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCenter_TextChanged(sender, e);
            txtCompare_TextChanged(sender, e);
        }

        #endregion

        #region 上下键

        void txtCenter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up)
            {
                if (this.spreadCenterItem_Sheet1.ActiveRowIndex > 0)
                {
                    this.spreadCenterItem_Sheet1.ActiveRowIndex--;
                    this.spreadCenterItem.ShowRow(0, this.spreadCenterItem_Sheet1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Nearest);
                }
            }
            else if (e.KeyData == Keys.Down)
            {
                if (this.spreadCenterItem_Sheet1.ActiveRowIndex < this.spreadCenterItem_Sheet1.RowCount - 1)
                {
                    this.spreadCenterItem_Sheet1.ActiveRowIndex++;
                    this.spreadCenterItem.ShowRow(0, this.spreadCenterItem_Sheet1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Nearest);
                }
            }
        }

        void txtLocal_KeyDown(object sender, KeyEventArgs e)
        { 
            if (e.KeyData == Keys.Up)
            {
                if (this.spreadHISItem_Sheet1.ActiveRowIndex > 0)
                {
                    this.spreadHISItem_Sheet1.ActiveRowIndex--;
                    this.spreadHISItem.ShowRow(0, this.spreadHISItem_Sheet1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Nearest);
                }
            }
            else if (e.KeyData == Keys.Down)
            {
                if (this.spreadHISItem_Sheet1.ActiveRowIndex < this.spreadHISItem_Sheet1.RowCount - 1)
                {
                    this.spreadHISItem_Sheet1.ActiveRowIndex++;
                    this.spreadHISItem.ShowRow(0, this.spreadHISItem_Sheet1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Nearest);
                }
            }
        }

        void txtCompare_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up)
            {
                if (this.spreadCompareItem_Sheet1.ActiveRowIndex > 0)
                {
                    this.spreadCompareItem_Sheet1.ActiveRowIndex--;
                    this.spreadCompareItem.ShowRow(0, this.spreadCompareItem_Sheet1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Nearest);
                }
            }
            else if (e.KeyData == Keys.Down)
            {
                if (this.spreadCompareItem_Sheet1.ActiveRowIndex < this.spreadCompareItem_Sheet1.RowCount - 1)
                {
                    this.spreadCompareItem_Sheet1.ActiveRowIndex++;
                    this.spreadCompareItem.ShowRow(0, this.spreadCompareItem_Sheet1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Nearest);
                }
            }
        }
        #endregion

        #region Spread

        void spreadHISItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                //提示保存
                if (this.Save() > 0)
                {
                    this.txtCenter.Focus();
                    this.txtCenter.Select();
                    this.txtCenter.SelectAll();
                }
                else
                {
                    this.txtLocal.Focus();
                    this.txtLocal.Select();
                    this.txtLocal.SelectAll();
                }
            }
        }

        void spreadCenterItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }
        #endregion

        #endregion

        #region 数据操作

        /// <summary>
        /// 查找中心项目
        /// </summary>
        /// <param name="pactType"></param>
        /// <returns></returns>
        private DataSet getCenterItem(string pactType)
        {
            DataSet ds=new DataSet();
            string sql = @"select item_code 中心码,
                                            name 中心名称,
                                            a.specs 规格,
                                            a.dose_name 剂型,
                                            decode(a.item_grade,'1','公费','自费') 报销等级,
                                            a.rate 比例,
                                            fun_get_dictionary_name('ShgyCenterSysClass',a.sys_class) 类别,
                                            a.fee_name 费用类别,
                                            a.spell_code 拼音码,
                                            a.wb_code 五笔码,
                                            a.memo 备注
                                from fin_com_siitem a
                                where pact_code='{0}'";

            dbMgr.ExecQuery(string.Format(sql, pactType),ref ds);

            return ds;
        }

        /// <summary>
        /// 查找本地未对照的项目
        /// </summary>
        /// <returns></returns>
        private DataSet getLocalNoCompareItem(string pactType)
        {
            DataSet ds = new DataSet();
            string sql = @"select 
                                                a.custom_code 上传码,
                                                a.drug_code 编码,
                                                a.trade_name 名称,
                                                a.specs 规格,
                                                a.retail_price 价格,
                                                fun_get_dictionary_name('DOSAGEFORM',a.dose_model_code) 剂型,
                                                fun_get_dictionary_name('MINFEE',a.fee_code) 费用类别,
                                                a.spell_code 拼音码,
                                                a.wb_code 五笔码,
                                               fun_get_company_name(a.producer_code) 生成单位
                                    from pha_com_baseinfo a
                                    where a.valid_state=fun_get_valid()
                                    and not exists (select 1 from fin_com_compare c where c.pact_code='{0}' and c.his_code=a.drug_code)
                                    and a.custom_code not like 'B%'
                                    union all
                                    select 
                                                b.input_code 上传码,
                                                b.item_code 编码,
                                                b.item_name 名称,
                                                b.specs 规格,
                                                b.unit_price 价格,
                                                '' 剂型,
                                                fun_get_dictionary_name('MINFEE',b.fee_code) 费用类别,
                                                b.spell_code 拼音码,
                                                b.wb_code 五笔码,
                                                b.producer_info 生成单位
                                    from fin_com_undruginfo b
                                    where b.valid_state =fun_get_valid()
                                    and b.unitflag='0'
                                    and not exists (select 1 from fin_com_compare c where c.pact_code='{0}' and c.his_code=b.item_code)
                                    order by 上传码";

            dbMgr.ExecQuery(string.Format(sql, pactType),ref ds);

            return ds;
        }

        /// <summary>
        /// 查找本地未对照的项目
        /// </summary>
        /// <returns></returns>
        private DataSet getLocalNoCompareItem(string pactType,string itemCode)
        {
            DataSet ds = new DataSet();
            string sql = @"select 
                                                a.custom_code 上传码,
                                                a.drug_code 编码,
                                                a.trade_name 名称,
                                                a.specs 规格,
                                                a.retail_price 价格,
                                                fun_get_dictionary_name('DOSAGEFORM',a.dose_model_code) 剂型,
                                                fun_get_dictionary_name('MINFEE',a.fee_code) 费用类别,
                                                a.spell_code 拼音码,
                                                a.wb_code 五笔码,
                                                fun_get_company_name(a.producer_code) 生成单位
                                    from pha_com_baseinfo a
                                    where a.valid_state=fun_get_valid()
                                    and a.drug_code='{1}'
                                    and not exists (select 1 from fin_com_compare c where c.pact_code='{0}' and c.his_code=a.drug_code)
                                    and a.custom_code not like 'B%'
                                    union all
                                    select 
                                                b.input_code 上传码,
                                                b.item_code 编码,
                                                b.item_name 名称,
                                                b.specs 规格,
                                                b.unit_price 价格,
                                                '' 剂型,
                                                fun_get_dictionary_name('MINFEE',b.fee_code) 费用类别,
                                                b.spell_code 拼音码,
                                                b.wb_code 五笔码,
                                                b.producer_info 生成单位
                                    from fin_com_undruginfo b
                                    where b.valid_state =fun_get_valid()
                                    and b.unitflag='0'
                                    and b.item_code='{1}'
                                    and not exists (select 1 from fin_com_compare c where c.pact_code='{0}' and c.his_code=b.item_code)
                                    order by 上传码";

            dbMgr.ExecQuery(string.Format(sql, pactType,itemCode), ref ds);

            return ds;
        }

        /// <summary>
        /// 查找已对照项目
        /// </summary>
        /// <param name="pactType"></param>
        /// <returns></returns>
        private DataSet getCompareItem(string pactType)
        {
            DataSet ds = new DataSet();
            string sql = @"select 
                                                'false' 选择,
                                                a.his_code 编码,
                                                a.his_user_code 上传码,
                                                a.his_name 名称,
                                                a.his_specs 规格,
                                                a.his_price 单价,
                                                a.center_code 中心码,
                                                a.center_name 中心名称,
                                                a.center_dose 剂型,
                                                decode(a.center_item_grade,'1','公费','自费') 报销等级,
                                                a.center_rate 比例,
                                                fun_get_dictionary_name('ShgyCenterSysClass',a.center_sys_class) 类别,
                                                fun_get_dictionary_name('MINFEE',a.center_fee_code) 费用类别,
                                                decode(a.approvalstate,'1','已审批','未审批') 审批,
                                                fun_get_employee_name(a.oper_code) 匹配者,
                                                a.oper_date 匹配时间,
                                                a.center_spell 中心拼音码,
                                                a.center_wb_code 中心五笔码,
                                                a.his_spell  拼音码,
                                                a.his_wb_code 五笔码
                                    from fin_com_compare a
                                    where a.pact_code='{0}'
                                    order by a.oper_date desc";

            dbMgr.ExecQuery(string.Format(sql, pactType), ref ds);

            return ds;
        }

        /// <summary>
        /// 查找中心项目
        /// </summary>
        /// <param name="pactType"></param>
        /// <returns></returns>
        private DataSet getCompareItem(string pactType, string centerCode, string itemCode)
        {
            DataSet ds = new DataSet();
            string sql = @"select 
                                                'false' 选择,
                                                a.his_code 编码,
                                                a.his_user_code 上传码,
                                                a.his_name 名称,
                                                a.his_specs 规格,
                                                a.his_price 单价,
                                                a.center_code 中心码,
                                                a.center_name 中心名称,
                                                a.center_dose 剂型,
                                                decode(a.center_item_grade,'1','公费','自费') 报销等级,
                                                a.center_rate 比例,
                                                fun_get_dictionary_name('ShgyCenterSysClass',a.center_sys_class) 类别,
                                                fun_get_dictionary_name('MINFEE',a.center_fee_code) 费用类别,
                                                decode(a.approvalstate,'1','已审批','未审批') 审批,
                                                fun_get_employee_name(a.oper_code) 匹配者,
                                                a.oper_date 匹配时间,
                                                a.center_spell 中心拼音码,
                                                a.center_wb_code 中心五笔码,
                                                a.his_spell  拼音码,
                                                a.his_wb_code 五笔码
                                    from fin_com_compare a
                                where a.pact_code='{0}'
                                and a.center_code='{1}'
                                and a.his_code='{2}'";

            dbMgr.ExecQuery(string.Format(sql, pactType, centerCode, itemCode), ref ds);

            return ds;
        }

        /// <summary>
        /// 插入对照信息
        /// </summary>
        /// <param name="pactType"></param>
        /// <param name="centerCode"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        private int insertCompareItem(string pactType, string centerCode, string itemCode)
        {
            string insertSql = @"insert into fin_com_compare a
                                                        (PACT_CODE,
	                                                    HIS_CODE,
	                                                    HIS_NAME,
	                                                    REGULARNAME,
	                                                    CENTER_CODE,
	                                                    CENTER_SYS_CLASS,
	                                                    CENTER_NAME,
	                                                    CENTER_ENAME,
	                                                    CENTER_SPECS,
	                                                    CENTER_DOSE,
	                                                    CENTER_SPELL,
	                                                    CENTER_FEE_CODE,
	                                                    CENTER_ITEM_TYPE,
	                                                    CENTER_ITEM_GRADE,
	                                                    CENTER_RATE,
	                                                    CENTER_PRICE,
	                                                    CENTER_MEMO,
	                                                    HIS_SPELL,
	                                                    HIS_WB_CODE,
	                                                    HIS_USER_CODE,
	                                                    HIS_SPECS,
	                                                    HIS_PRICE,
	                                                    HIS_DOSE,
	                                                    OPER_CODE,
	                                                    OPER_DATE,
	                                                    SPECIAL_LIMIT_FLAG,
	                                                    SPECIAL_LIMIT_CONTENT,
	                                                    APPROVALDATE,
	                                                    APPROVALSTATE,
	                                                    CENTER_FEE_NAME,
	                                                    CENTER_WB_CODE)
                                                    select s.pact_code,
                                                                f.item_code,
                                                                f.item_name,
                                                                f.other_name,
                                                                s.item_code,
                                                                s.sys_class,
                                                                s.name,
                                                                s.ename,
                                                                s.specs,
                                                                s.dose_name,
                                                                s.spell_code,
                                                                s.fee_code,
                                                                s.item_type,
                                                                s.item_grade,
                                                                s.rate,
                                                                s.price,
                                                                s.memo,
                                                                f.spell_code,
                                                                f.wb_code,
                                                                f.input_code,
                                                                f.specs,
                                                                f.unit_price,
                                                                '' dose,
                                                                '{3}' oper_code,
                                                                sysdate oper_date,
                                                                s.aka302,
                                                                s.aka303,
                                                                null APPROVALDATE,
                                                                '0' APPROVALSTATE,
                                                                s.fee_name,
                                                                s.wb_code
                                                    from fin_com_siitem s,fin_com_undruginfo f
                                                    where s.pact_code='{0}' and s.item_code='{1}'
                                                    and f.item_code='{2}'
                                                    union all
                                                    select s.pact_code,
                                                                b.drug_code,
                                                                b.trade_name,
                                                                b.regular_name,
                                                                s.item_code,
                                                                s.sys_class,
                                                                s.name,
                                                                s.ename,
                                                                s.specs,
                                                                s.dose_name,
                                                                s.spell_code,
                                                                s.fee_code,
                                                                s.item_type,
                                                                s.item_grade,
                                                                s.rate,
                                                                s.price,
                                                                s.memo,
                                                                b.spell_code,
                                                                b.wb_code,
                                                                b.custom_code,
                                                                b.specs,
                                                                b.retail_price,
                                                                '' dose,
                                                                '{3}' oper_code,
                                                                sysdate oper_date,
                                                                s.aka302,
                                                                s.aka303,
                                                                null APPROVALDATE,
                                                                '0' APPROVALSTATE,
                                                                s.fee_name,
                                                                s.wb_code
                                                    from fin_com_siitem s,pha_com_baseinfo b
                                                    where s.pact_code='{0}' and s.item_code='{1}'
                                                    and b.drug_code='{2}'";


            return dbMgr.ExecNoQuery(string.Format(insertSql, pactType, centerCode, itemCode, dbMgr.Operator.ID));
        }

        /// <summary>
        /// 删除对照信息
        /// </summary>
        /// <param name="pactType"></param>
        /// <param name="centerCode"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        private int deleteCompareItem(string pactType, string centerCode, string itemCode)
        {
            string insertSql = @"delete  from fin_com_compare a 
                               where a.pact_code='{0}'
                                and a.center_code='{1}'
                                and a.his_code='{2}'";

            return dbMgr.ExecNoQuery(string.Format(insertSql, pactType, centerCode, itemCode));
        }

        #endregion

    }
}
