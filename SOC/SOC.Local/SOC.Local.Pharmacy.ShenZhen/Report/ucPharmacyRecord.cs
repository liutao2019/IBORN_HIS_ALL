using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Pharmacy.ShenZhen.Report
{
    public partial class ucPharmacyRecord : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        public ucPharmacyRecord()
        {
            InitializeComponent();

            this.PriveClassTwos = "0310,0320";
            this.MainTitle = "药品台账";
            this.RightAdditionTitle = "";
            this.SQLIndexs = "SOC.Pharmacy.Report.Record.Static.SD";

        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitData()
        {
            FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();
            try
            {
                List<FS.HISFC.Models.Pharmacy.Item> alDrug = itemMgr.QueryItemList(true);
                System.Collections.ArrayList alObject = new System.Collections.ArrayList();
                foreach (FS.HISFC.Models.Pharmacy.Item item in alDrug)
                {
                    FS.HISFC.Models.Base.Spell neuObject = item as FS.HISFC.Models.Base.Spell;
                    neuObject.ID = item.ID;
                    neuObject.Name = item.Name; ;
                    neuObject.Memo = item.Specs;
                    alObject.Add(neuObject);
                }
                this.ncmbDrug.AddItems(alObject);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns></returns>
        private new string[] GetQueryConditions()
        {
            if (this.IsDeptAsCondition && this.IsTimeAsCondition)
            {
                string[] parm = { "", "", "", "", "", "", "", "" };
                if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                {
                    parm[0] = this.cmbDept.Tag.ToString();
                }
                parm[1] = this.dtStart.Value.ToString();
                parm[2] = this.dtEnd.Value.ToString();
                parm[3] = this.GetParm()[0];

                return parm;
            }
            if (!this.IsDeptAsCondition && this.IsTimeAsCondition)
            {
                string[] parm = { "", "", "", "", "", "", "" };

                parm[0] = this.dtStart.Value.ToString();
                parm[1] = this.dtEnd.Value.ToString();
                parm[2] = this.GetParm()[0];
                return parm;
            }
            if (this.IsDeptAsCondition && !this.IsTimeAsCondition)
            {
                string[] parm = { "", "", "", "", "", "" };
                if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                {
                    parm[0] = this.cmbDept.Tag.ToString();
                }
                parm[1] = this.GetParm()[0];
                return parm;
            }

            string[] parmNull = { "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null" };
            return parmNull;
        }

        /// <summary>
        /// 获取不定查询条件
        /// </summary>
        /// <returns></returns>
        private string[] GetParm()
        {

            string drugNO = "AAAA";
            if (this.ncmbDrug.Tag != null && !string.IsNullOrEmpty(this.ncmbDrug.Tag.ToString()) && !string.IsNullOrEmpty(this.ncmbDrug.Text.Trim()))
            {
                drugNO = this.ncmbDrug.Tag.ToString();
            }

            return new string[] { drugNO };
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryAdditionConditions = this.GetQueryConditions();
            this.IsNeedAdditionConditions = true;
            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// 加载窗口
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            
            //界面打开时不查询
            if (DesignMode)
            {
                return;
            }
            this.QueryDataWhenInit = false;
            this.QueryEndHandler = new DelegateQueryEnd(QueryEnd);
            this.InitData();
            base.OnLoad(e);
            
        }

        /// <summary>
        /// 设置列头
        /// </summary>
        private void QueryEnd()
        {
            this.fpSpread1_Sheet1.ColumnHeader.RowCount = 2;
            this.fpSpread1_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Default;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).RowSpan = 3;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "日期";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).RowSpan = 2;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "凭证号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).RowSpan = 2;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "摘要";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).ColumnSpan = 3;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "借方";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).ColumnSpan = 3;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "贷方";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).ColumnSpan = 3;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "结存";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 3).Value = "数量";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 4).Value = "单价";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 5).Value = "金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 6).Value = "数量";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 7).Value = "单价";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 8).Value = "金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 9).Value = "数量";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 10).Value = "单价";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 11).Value = "金额";
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine();
            this.fpSpread1_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine();


            FS.HISFC.Models.Pharmacy.Item item = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(this.ncmbDrug.Tag.ToString());
            if (item != null && !string.IsNullOrEmpty(item.ID))
            {

                this.lbAdditionTitleMid.Text +=  "    药品名称:" + item.Name + "    规格" + item.Specs;
            }
        }
        
    }
}
