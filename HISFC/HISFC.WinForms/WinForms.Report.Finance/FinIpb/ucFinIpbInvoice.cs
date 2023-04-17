using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.Report.Finance.FinIpb
{
    /// <summary>
    /// סԺ��Ʊ��ѯ
    /// </summary>
    public partial class ucFinIpbInvoice : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinIpbInvoice()
        {
            InitializeComponent();
        }
        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }


            return base.OnRetrieve(base.beginTime, base.endTime);
        }

        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            try
            {
                //�������������DataWindow�ؼ�ʱ��Ҫ��ӡ����ʱ�����ݽ����жϴ�ӡ�ĸ�DataWindow�ؼ�

                if (this.dwMain.Focused)
                {
                    this.dwMain.Print();
                }
                else if (this.dwDetail.Focused) //����DataWindow�ؼ���ӡ���������
                {
                    this.dwDetail.Print();
                }
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("��ӡ����", "��ʾ");
                return -1;
            }

        }
        /// <summary>
        /// ��ѯ��ϸ��Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dwMain_RowFocusChanged(object sender, Sybase.DataWindow.RowFocusChangedEventArgs e)
        {
            int currRow = e.RowNumber;
            if (currRow == 0)
            {
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ�����ϸ�����Ժ�...");
            string sInvoiceNo;
            double dbBalanceNo;
            string sInpatientNo;
            sInvoiceNo = dwMain.GetItemString(currRow, "fin_ipb_balancehead_invoice_no");
            dbBalanceNo = dwMain.GetItemDouble(currRow, "fin_ipb_balancehead_balance_no");
            sInpatientNo = dwMain.GetItemString(currRow, "fin_ipb_balancehead_inpatient_no");

            dwDetail.Retrieve(sInvoiceNo, dbBalanceNo, sInpatientNo);

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        protected override int OnExport()
        {
            //������ڶ��DataWindowʱ����������Ҫ��д��������Ҫ��д�÷��������ݽ����жϵ��������ĸ�DataWindow
            try
            {
                //����Excel��ʽ�ļ�
                SaveFileDialog saveDial = new SaveFileDialog();
                saveDial.Filter = "Excel�ļ���*.xls��|*.xls";
                //�ļ���
                string fileName = string.Empty;
                if (saveDial.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveDial.FileName;
                }
                else
                {
                    return 1;
                }

                //���ݽ����жϵ����ĸ�DataWindow
                if (this.dwMain.Focused)
                {
                    this.dwMain.SaveAs(fileName, Sybase.DataWindow.FileSaveAsType.Excel);
                }
                else if (this.dwDetail.Focused) //����Դ�����
                {
                    this.dwDetail.SaveAs(fileName, Sybase.DataWindow.FileSaveAsType.Excel);
                }
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("��������", "��ʾ");
                return -1;
            }
        }

    }
}

