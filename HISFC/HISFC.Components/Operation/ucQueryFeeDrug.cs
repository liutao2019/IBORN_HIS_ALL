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
    /// [��������: ���߷���ҩƷ��ѯ]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-01-08]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucQueryFeeDrug : UserControl
    {
        public ucQueryFeeDrug()
        {
            InitializeComponent();
        }

#region �ֶ�
        private FS.HISFC.BizProcess.Integrate.Fee feeManager = new FS.HISFC.BizProcess.Integrate.Fee();
        
#endregion

#region ����
        /// <summary>
        /// ���ҩƷ��ϸ
        /// </summary>
        /// <param name="patient"></param>
        public void AddItems(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            
            ArrayList drugs = this.feeManager.GetMedItemsForInpatient(patient.ID, patient.PVisit.InTime, Environment.AnaeManager.GetDateTimeFromSysDateTime());
            decimal totCost = 0;
            this.fpSpread2_Sheet1.RowCount = 0;
            if (drugs != null)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList drug in drugs)
                {
                    fpSpread2_Sheet1.Rows.Add(fpSpread2_Sheet1.RowCount, 1);
                    int row = fpSpread2_Sheet1.RowCount - 1;
                    //�����Ŀ����
                    if (drug.Item.Specs == "")
                        fpSpread2_Sheet1.SetValue(row, 0, drug.Item.Name, false);
                    else
                        fpSpread2_Sheet1.SetValue(row, 0, drug.Item.Name + "[" + drug.Item.Specs + "]", false);
                    //�۸�
                    fpSpread2_Sheet1.SetValue(row, 1, drug.Item.Price, false);
                    //����
                    fpSpread2_Sheet1.SetValue(row, 2, drug.Item.Qty, false);
                    //����
                    fpSpread2_Sheet1.SetValue(row, 3, drug.FTRate.ItemRate, false);
                    //��λ
                    fpSpread2_Sheet1.SetValue(row, 4, drug.Item.PriceUnit, false);
                    //�ܶ�
                    fpSpread2_Sheet1.SetValue(row, 5, drug.FT.TotCost, false);
                    //�շ���
                    fpSpread2_Sheet1.SetValue(row, 6, drug.FeeOper.ID, false);
                    //�շ�ʱ��
                    fpSpread2_Sheet1.SetValue(row, 7, drug.FeeOper.OperTime.ToString(), false);
                    totCost = totCost + drug.FT.TotCost;
                }
            }
            if (totCost > 0)
            {
                fpSpread2_Sheet1.Rows.Add(fpSpread2_Sheet1.RowCount, 1);
                int row = fpSpread2_Sheet1.RowCount - 1;
                fpSpread2_Sheet1.SetValue(row, 4, "�ϼ�", false);
                fpSpread2_Sheet1.SetValue(row, 5, totCost, false);
            }
        }
        /// <summary>
        /// {CBF49C01-5D42-407a-9F85-4E81081D562E}
        /// ��������ִ�п���
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="execDeptCode"></param>
        public void AddItems(FS.HISFC.Models.RADT.PatientInfo patient,string execDeptCode)
        {

            //ArrayList drugs = this.feeManager.GetMedItemsForInpatient(patient.ID, patient.PVisit.InTime, Environment.AnaeManager.GetDateTimeFromSysDateTime());
            ArrayList drugs = this.feeManager.GetMedItemsForInpatientByExecDeptCode(patient.ID, patient.PVisit.InTime, Environment.AnaeManager.GetDateTimeFromSysDateTime(),execDeptCode );
            decimal totCost = 0;
            this.fpSpread2_Sheet1.RowCount = 0;
            if (drugs != null)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList drug in drugs)
                {
                    fpSpread2_Sheet1.Rows.Add(fpSpread2_Sheet1.RowCount, 1);
                    int row = fpSpread2_Sheet1.RowCount - 1;
                    //�����Ŀ����
                    if (drug.Item.Specs == "")
                        fpSpread2_Sheet1.SetValue(row, 0, drug.Item.Name, false);
                    else
                        fpSpread2_Sheet1.SetValue(row, 0, drug.Item.Name + "[" + drug.Item.Specs + "]", false);
                    //�۸�
                    fpSpread2_Sheet1.SetValue(row, 1, drug.Item.Price, false);
                    //����
                    fpSpread2_Sheet1.SetValue(row, 2, drug.Item.Qty, false);
                    //����
                    fpSpread2_Sheet1.SetValue(row, 3, drug.FTRate.ItemRate, false);
                    //��λ
                    fpSpread2_Sheet1.SetValue(row, 4, drug.Item.PriceUnit, false);
                    //�ܶ�
                    fpSpread2_Sheet1.SetValue(row, 5, drug.FT.TotCost, false);
                    //�շ���
                    fpSpread2_Sheet1.SetValue(row, 6, drug.FeeOper.ID, false);
                    //�շ�ʱ��
                    fpSpread2_Sheet1.SetValue(row, 7, drug.FeeOper.OperTime.ToString(), false);
                    totCost = totCost + drug.FT.TotCost;
                }
            }
            if (totCost > 0)
            {
                fpSpread2_Sheet1.Rows.Add(fpSpread2_Sheet1.RowCount, 1);
                int row = fpSpread2_Sheet1.RowCount - 1;
                fpSpread2_Sheet1.SetValue(row, 4, "�ϼ�", false);
                fpSpread2_Sheet1.SetValue(row, 5, totCost, false);
            }
        }
        public void Reset()
        {
            this.fpSpread2_Sheet1.RowCount = 0;
        }

        public int Print()
        {
            return Environment.Print.PrintPreview(this);
        }
#endregion

    }
}
