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
    /// [��������: ���߷��÷�ҩƷ��ѯ]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-01-08]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucQueryFeeUndrug : UserControl
    {
        public ucQueryFeeUndrug()
        {
            InitializeComponent();
        }

        #region �ֶ�
        private FS.HISFC.BizProcess.Integrate.Fee feeManager = new FS.HISFC.BizProcess.Integrate.Fee();

        #endregion

#region ����
        /// <summary>
        /// ��ӷ�ҩƷ��ϸ
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
                    //�����Ŀ����
                    fpSpread3_Sheet1.SetValue(row, 0, undrug.Item.Name, false);
                    //�۸�
                    fpSpread3_Sheet1.SetValue(row, 1, undrug.Item.Price, false);
                    //����
                    fpSpread3_Sheet1.SetValue(row, 2, undrug.Item.Qty, false);
                    //����
                    fpSpread3_Sheet1.SetValue(row, 3, undrug.FTRate.ItemRate, false);
                    //��λ
                    fpSpread3_Sheet1.SetValue(row, 4, undrug.Item.PriceUnit, false);
                    //�ܶ�
                    fpSpread3_Sheet1.SetValue(row, 5, undrug.FT.TotCost, false);
                    //�շ���
                    fpSpread3_Sheet1.SetValue(row, 6, undrug.FeeOper.ID, false);
                    //�շ�ʱ��
                    fpSpread3_Sheet1.SetValue(row, 7, undrug.FeeOper.OperTime.ToString(), false);
                    totCost = totCost + undrug.FT.TotCost;
                }
            }
            if (totCost > 0)
            {
                fpSpread3_Sheet1.Rows.Add(fpSpread3_Sheet1.RowCount, 1);
                int row = fpSpread3_Sheet1.RowCount - 1;
                fpSpread3_Sheet1.SetValue(row, 4, "�ϼ�", false);
                fpSpread3_Sheet1.SetValue(row, 5, totCost, false);
            }
        }
        //{CBF49C01-5D42-407a-9F85-4E81081D562E}
        //��ִ�п���
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
                    //�����Ŀ����
                    fpSpread3_Sheet1.SetValue(row, 0, undrug.Item.Name, false);
                    //�۸�
                    fpSpread3_Sheet1.SetValue(row, 1, undrug.Item.Price, false);
                    //����
                    fpSpread3_Sheet1.SetValue(row, 2, undrug.Item.Qty, false);
                    //����
                    fpSpread3_Sheet1.SetValue(row, 3, undrug.FTRate.ItemRate, false);
                    //��λ
                    fpSpread3_Sheet1.SetValue(row, 4, undrug.Item.PriceUnit, false);
                    //�ܶ�
                    fpSpread3_Sheet1.SetValue(row, 5, undrug.FT.TotCost, false);
                    //�շ���
                    fpSpread3_Sheet1.SetValue(row, 6, undrug.FeeOper.ID, false);
                    //�շ�ʱ��
                    fpSpread3_Sheet1.SetValue(row, 7, undrug.FeeOper.OperTime.ToString(), false);
                    totCost = totCost + undrug.FT.TotCost;
                }
            }
            if (totCost > 0)
            {
                fpSpread3_Sheet1.Rows.Add(fpSpread3_Sheet1.RowCount, 1);
                int row = fpSpread3_Sheet1.RowCount - 1;
                fpSpread3_Sheet1.SetValue(row, 4, "�ϼ�", false);
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
