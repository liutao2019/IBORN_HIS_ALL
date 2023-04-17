using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Operation.Report
{
    /// <summary>
    /// [��������: ��������ͳ��һ����]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-01-16]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucReportGeneral : ucReportBase
    {
        public ucReportGeneral()
        {
            InitializeComponent();
            this.Title = "��������ͳ��һ����";
            this.fpSpread1_Sheet1.GrayAreaBackColor = Color.White;
            this.fpSpread1_Sheet1.ColumnHeader.Rows[0].BackColor = Color.White;
            this.fpSpread1_Sheet1.Columns[-1].Width = 150;
            this.fpSpread1_Sheet1.Columns[-1].Locked = true;

            this.fpSpread1_Sheet1.Cells[0, 0].Value = "��ͨ����";
            this.fpSpread1_Sheet1.Cells[1, 0].Value = "��������";
            this.fpSpread1_Sheet1.Cells[2, 0].Value = "��Ⱦ����";
            this.fpSpread1_Sheet1.Cells[3, 0].Value = "��  �ƣ�";
        }

#region �¼�



        protected override int OnQuery()
        {
            this.fpSpread1_Sheet1.RowCount = 0;

            ArrayList DataAl = new ArrayList();


            DataAl = Environment.ReportManager.GetReport05(this.cmbDept.Tag.ToString(), this.dtpBegin.Value, this.dtpEnd.Value);
            if (DataAl == null || DataAl.Count == 0) return -1;

            this.fpSpread1_Sheet1.Rows.Add(0, 4);
            this.fpSpread1_Sheet1.Cells[0, 0].Value = "��ͨ����";
            this.fpSpread1_Sheet1.Cells[1, 0].Value = "��������";
            this.fpSpread1_Sheet1.Cells[2, 0].Value = "��Ⱦ����";
            this.fpSpread1_Sheet1.Cells[3, 0].Value = "��  �ƣ�";

            //��ǰ����Ӧ�����λ��
            int iRow = 0;
            int iCol = 0;
            string data = "";

            foreach (ArrayList thisData in DataAl)
            {
                if (thisData == null || thisData.Count < 3) continue;
                data = thisData[0].ToString();
                if (data == null || data == "") continue;
                iRow = FS.FrameWork.Function.NConvert.ToInt32(thisData[0].ToString()) - 1;
                data = thisData[1].ToString();
                if (data == null || data == "") continue;

                iCol = FS.FrameWork.Function.NConvert.ToInt32(thisData[1].ToString());
                if (thisData[2].ToString() != "")
                    this.fpSpread1_Sheet1.Cells[iRow, iCol].Value = thisData[2].ToString();
                else
                    this.fpSpread1_Sheet1.Cells[iRow, iCol].Value = "0";
            }

            //ÿ�еĺϼ���("�ϼ�"�в���)
            int iRowTotal = 0;
            for (int i = 0; i < this.fpSpread1_Sheet1.Rows.Count - 1; i++)
            {
                iRowTotal =
                    FS.FrameWork.Function.NConvert.ToInt32(this.fpSpread1_Sheet1.Cells[i, 1].Value) + //�ش�������
                    FS.FrameWork.Function.NConvert.ToInt32(this.fpSpread1_Sheet1.Cells[i, 2].Value) + //��������
                    FS.FrameWork.Function.NConvert.ToInt32(this.fpSpread1_Sheet1.Cells[i, 3].Value) + //��������
                    FS.FrameWork.Function.NConvert.ToInt32(this.fpSpread1_Sheet1.Cells[i, 4].Value);  //С������
                this.fpSpread1_Sheet1.Cells[i, 5].Value = iRowTotal.ToString();
                iRowTotal = 0;
            }
            //ÿ�еĺϼ���(�����в���)
            int iColTotal = 0;
            for (int j = 1; j < this.fpSpread1_Sheet1.Columns.Count; j++)
            {
                iColTotal =
                    FS.FrameWork.Function.NConvert.ToInt32(this.fpSpread1_Sheet1.Cells[0, j].Value) + //��ͨ������
                    FS.FrameWork.Function.NConvert.ToInt32(this.fpSpread1_Sheet1.Cells[1, j].Value) + //����������
                    FS.FrameWork.Function.NConvert.ToInt32(this.fpSpread1_Sheet1.Cells[2, j].Value);  //��Ⱦ������
                this.fpSpread1_Sheet1.Cells[3, j].Value = iColTotal.ToString();
                iColTotal = 0;
            }
            return 0;
        }
#endregion
    }
}
