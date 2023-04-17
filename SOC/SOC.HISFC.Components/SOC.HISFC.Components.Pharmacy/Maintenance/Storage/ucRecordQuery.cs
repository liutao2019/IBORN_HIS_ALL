using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Pharmacy.Maintenance
{
    public partial class ucRecordQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        
         public ucRecordQuery()
        {
            InitializeComponent();
        }


        public ucRecordQuery(string deptCode, string drugCode)
        {
            InitializeComponent();
            DateTime dtTmp = this.inOutMgr.GetDateTimeFromSysDateTime();
            this.ndtBegin.Value = dtTmp.Date.AddDays(-30);
            this.ndtEnd.Value = dtTmp;
            curDeptCode = deptCode;
            curDrugCode = drugCode;
            this.ShowDetail(deptCode, drugCode,this.ndtBegin.Value,this.ndtEnd.Value);
        }

        #region 属性和变量
        /// <summary>
        /// 当前操作的科室
        /// </summary>
        private string curDeptCode = string.Empty;

        /// <summary>
        /// 当前操作的项目
        /// </summary>
        private string curDrugCode = string.Empty;

        /// <summary>
        /// 入出库操作管理类
        /// </summary>
        private FS.SOC.HISFC.BizLogic.Pharmacy.InOut inOutMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();

        string strSql = @"Pharmacy.Report.RecordInStockInfo";

        DataSet ds = new DataSet();

        DataTable dt = new DataTable();
        
        #endregion

        /// <summary>
        /// 显示台账信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="drugCode"></param>
        private void ShowDetail(string deptCode, string drugCode,DateTime dtBegin,DateTime dtEnd)
        {
            this.inOutMgr.ExecQuery(strSql,ref ds,deptCode,drugCode,dtBegin.ToString(),dtEnd.ToString());

            dt = ds.Tables[0] as DataTable;

            this.SetFarpoint(dt);

            return;
        }

        private void SetFarpoint(DataTable dt)
        {
            this.SetFarpointColumns();
            this.SetFarpointDetail(dt);
            this.SetFarpontDataType();
        }

        private void SetFarpontDataType()
        {
            for(int index = 0;index < this.neuSpreadDetail_Sheet1.Columns.Count;index++)
            {
                this.neuSpreadDetail_Sheet1.Columns[index].Locked = true;
            }
        }

        private void SetFarpointDetail(DataTable dt)
        {
            int index = 1;
            foreach(DataRow dr in this.dt.Rows)
            {
                this.neuSpreadDetail_Sheet1.Rows.Add(this.neuSpreadDetail_Sheet1.Rows.Count, 1);

                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet1.操作日期].Text = dr["oper_date"].ToString();
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet1.操作员].Text = dr["oper_code"].ToString();
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet1.厂家].Text = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(dr["producer"].ToString());
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet1.供货公司].Text = dr["company"].ToString();
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet1.药品去向].Text = dr["dept"].ToString();
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet1.单位].Text = dr["pack_unit"].ToString();
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet1.购入金额].Text = dr["purchase_cost"].ToString();
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet1.规格].Text = dr["specs"].ToString();
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet1.库存量].Text = dr["store_num"].ToString();
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet1.类型].Text = dr["class3_name"].ToString();
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet1.库存金额].Text = dr["store_cost"].ToString();
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet1.批号].Text = dr["batch_no"].ToString();
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet1.入出库数量].Text = dr["in_num"].ToString();
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet1.效期].Text = FS.FrameWork.Function.NConvert.ToDateTime(dr["valid_date"].ToString()).ToShortDateString();
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet1.药品名称].Text = dr["trade_name"].ToString();
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet1.自定义码].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(dr["drug_code"].ToString());
                index++;
            }
        }

        private void SetFarpointColumns()
        {
            this.neuSpreadDetail_Sheet1.Columns.Count = (int)EnumColumnSet1.操作日期 + 1;
            this.neuSpreadDetail_Sheet1.Rows.Add(0, 1);
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet1.操作日期].Width = 115F;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet1.操作日期].Text = "操作日期";
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet1.操作员].Width = 60F;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet1.操作员].Text = "操作员";
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet1.供货公司].Width = 120F;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet1.供货公司].Text = "供货公司";
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet1.药品去向].Width = 80F;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet1.药品去向].Text = "药品去向";
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet1.厂家].Width = 120F;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet1.厂家].Text = "厂家";
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet1.单位].Width = 30F;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet1.单位].Text = "单位";
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet1.购入金额].Width = 60F;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet1.购入金额].Text = "购入金额";
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet1.规格].Width = 75F;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet1.规格].Text = "规格";
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet1.库存量].Width = 50F;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet1.库存量].Text = "库存量";
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet1.类型].Width = 120F;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet1.类型].Text = "类型";
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet1.库存金额].Width = 60F;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet1.库存金额].Text = "库存金额";
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet1.批号].Width = 60F;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet1.批号].Text = "批号";
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet1.入出库数量].Width = 50F;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet1.入出库数量].Text = "入出库数量";
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet1.效期].Width = 70F;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet1.效期].Text = "效期";
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet1.药品名称].Width = 200F;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet1.药品名称].Text = "药品名称";
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet1.自定义码].Width = 60F;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet1.自定义码].Text = "自定义码";
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbtQuery_Click(object sender, EventArgs e)
        {
            this.Clear();
            this.ShowDetail(curDeptCode,curDrugCode,this.ndtBegin.Value,this.ndtEnd.Value);
        }

        private void Clear()
        {
            this.neuSpreadDetail_Sheet1.RowCount = 0;
            ds = new DataSet();
            dt = new DataTable();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbtExport_Click(object sender, EventArgs e)
        {
            this.neuSpreadDetail.Export();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbtExit_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
    }

    enum EnumColumnSet1
    { 
        类型,
        自定义码,
        药品名称,
        规格,
        单位,
        入出库数量,
        购入金额,
        库存量,
        库存金额,
        批号,
        效期,
        供货公司,
        药品去向,
        厂家,
        操作员,
        操作日期
    }
}
