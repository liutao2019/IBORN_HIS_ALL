using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [功能描述: 患者费用非药品查询]<br></br>
    /// [创 建 者: 王铁全]<br></br>
    /// [创建时间: 2007-01-08]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucQueryFeeUndrug : UserControl
    {
        public ucQueryFeeUndrug()
        {
            InitializeComponent();
        }

        #region 字段
        private FS.HISFC.BizProcess.Integrate.Fee feeManager = new FS.HISFC.BizProcess.Integrate.Fee();

        #endregion

#region 方法
        /// <summary>
        /// 添加非药品明细
        /// </summary>
        /// <param name="patient"></param>
        public void AddItems(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            ArrayList undrugs = feeManager.QueryFeeItemLists(patientInfo.ID, patientInfo.PVisit.InTime, Environment.AnaeManager.GetDateTimeFromSysDateTime());
            decimal totCost = 0;
            this.fpSpread3_Sheet1.RowCount = 0;
            if (undrugs != null)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList undrug in undrugs)
                {
                    fpSpread3_Sheet1.Rows.Add(fpSpread3_Sheet1.RowCount, 1);
                    int row = fpSpread3_Sheet1.RowCount - 1;
                    //添加项目名称
                    fpSpread3_Sheet1.SetValue(row, 0, undrug.Item.Name, false);
                    //价格
                    fpSpread3_Sheet1.SetValue(row, 1, undrug.Item.Price, false);
                    //数量
                    fpSpread3_Sheet1.SetValue(row, 2, undrug.Item.Qty, false);
                    //比率
                    fpSpread3_Sheet1.SetValue(row, 3, undrug.FTRate.ItemRate, false);
                    //单位
                    fpSpread3_Sheet1.SetValue(row, 4, undrug.Item.PriceUnit, false);
                    //总额
                    fpSpread3_Sheet1.SetValue(row, 5, undrug.FT.TotCost, false);
                    //收费人
                    fpSpread3_Sheet1.SetValue(row, 6, undrug.FeeOper.ID, false);
                    //收费时间
                    fpSpread3_Sheet1.SetValue(row, 7, undrug.FeeOper.OperTime.ToString(), false);
                    totCost = totCost + undrug.FT.TotCost;
                }
            }
            if (totCost > 0)
            {
                fpSpread3_Sheet1.Rows.Add(fpSpread3_Sheet1.RowCount, 1);
                int row = fpSpread3_Sheet1.RowCount - 1;
                fpSpread3_Sheet1.SetValue(row, 4, "合计", false);
                fpSpread3_Sheet1.SetValue(row, 5, totCost, false);
            }
        }
        //{CBF49C01-5D42-407a-9F85-4E81081D562E}
        //按执行科室
        public void AddItems(FS.HISFC.Models.RADT.PatientInfo patientInfo,string execDeptCode)
        {
            //ArrayList undrugs = feeManager.QueryFeeItemLists(patientInfo.ID, patientInfo.PVisit.InTime, Environment.AnaeManager.GetDateTimeFromSysDateTime());
            ArrayList undrugs = feeManager.QueryFeeItemListsByExecDeptCode(patientInfo.ID, patientInfo.PVisit.InTime, Environment.AnaeManager.GetDateTimeFromSysDateTime(), execDeptCode);
            decimal totCost = 0;
            this.fpSpread3_Sheet1.RowCount = 0;
            if (undrugs != null)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList undrug in undrugs)
                {
                    fpSpread3_Sheet1.Rows.Add(fpSpread3_Sheet1.RowCount, 1);
                    int row = fpSpread3_Sheet1.RowCount - 1;
                    //添加项目名称
                    fpSpread3_Sheet1.SetValue(row, 0, undrug.Item.Name, false);
                    //价格
                    fpSpread3_Sheet1.SetValue(row, 1, undrug.Item.Price, false);
                    //数量
                    fpSpread3_Sheet1.SetValue(row, 2, undrug.Item.Qty, false);
                    //比率
                    fpSpread3_Sheet1.SetValue(row, 3, undrug.FTRate.ItemRate, false);
                    //单位
                    fpSpread3_Sheet1.SetValue(row, 4, undrug.Item.PriceUnit, false);
                    //总额
                    fpSpread3_Sheet1.SetValue(row, 5, undrug.FT.TotCost, false);
                    //收费人
                    fpSpread3_Sheet1.SetValue(row, 6, undrug.FeeOper.ID, false);
                    //收费时间
                    fpSpread3_Sheet1.SetValue(row, 7, undrug.FeeOper.OperTime.ToString(), false);
                    totCost = totCost + undrug.FT.TotCost;
                }
            }
            if (totCost > 0)
            {
                fpSpread3_Sheet1.Rows.Add(fpSpread3_Sheet1.RowCount, 1);
                int row = fpSpread3_Sheet1.RowCount - 1;
                fpSpread3_Sheet1.SetValue(row, 4, "合计", false);
                fpSpread3_Sheet1.SetValue(row, 5, totCost, false);
            }
        }

        public int Print()
        {
            return Environment.Print.PrintPreview(this);
        }

        public void Reset()
        {
            this.fpSpread3_Sheet1.RowCount = 0;
        }
#endregion
    }
}
