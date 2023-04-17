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
    public partial class ucReportGeneral2 : ucReportBase
    {
        public ucReportGeneral2()
        {
            InitializeComponent();
            this.Title = "�������ͳ����������(������ҽ�����ڲ���)";
            this.fpSpread1_Sheet1.GrayAreaBackColor = Color.White;
            this.fpSpread1_Sheet1.ColumnHeader.Rows[0].BackColor = Color.White;
            this.fpSpread1_Sheet1.Columns[-1].Width = 150;
            this.fpSpread1_Sheet1.Columns[-1].Locked = true;


        }

#region �¼�



        protected override int OnQuery()
        {
            this.fpSpread1_Sheet1.RowCount = 0;

            #region ���տ��ҽ��в�ѯ  by zlw 2006-5-17


            ArrayList DataAl;

            DataAl = Environment.ReportManager.GetReport06(this.cmbDept.Tag.ToString(), this.dtpBegin.Value, this.dtpEnd.Value);
            #endregion

            if (DataAl == null || DataAl.Count == 0) return -1;

            string strPreDept = "";
            string strCurDept = "";
            string strDegree = "";
            int iCol = 0;
            foreach (ArrayList thisData in DataAl)
            {
                if (thisData == null || thisData.Count < 3) 
                    continue;

                strCurDept = thisData[0].ToString();//������
                
                if (strCurDept == "") 
                    strCurDept = "δ֪";
                
                strDegree = thisData[1].ToString();//������ģ
                //ʼ�ձ��ֵ�ǰ������ݵĿ����ǵ�0��(��ȡ�����������Ѿ����տ����������)
                //�����һ���¿��ң�������һ��
                if (strCurDept != strPreDept)
                {
                    this.fpSpread1_Sheet1.Rows.Add(0, 1);
                    this.fpSpread1_Sheet1.Cells[0, 0].Value = strCurDept;
                }
                strPreDept = strCurDept;
                if (strDegree == null || strDegree == "") continue;

                iCol = FS.FrameWork.Function.NConvert.ToInt32(strDegree);

                if (thisData[2].ToString() != "")
                    this.fpSpread1_Sheet1.Cells[0, iCol].Value = thisData[2].ToString();
                else
                    this.fpSpread1_Sheet1.Cells[0, iCol].Value = "0";
            }

            //���ӡ��ϼơ���
            this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.Rows.Count, 1);
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1, 0].Value = "��  �ƣ�";

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
                //�������е�ÿһ��
                for (int k = 0; k < this.fpSpread1_Sheet1.Rows.Count - 1; k++)
                {
                    iColTotal = iColTotal + FS.FrameWork.Function.NConvert.ToInt32(this.fpSpread1_Sheet1.Cells[k, j].Value);
                }
                //���ø��еĺϼ���	
                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1, j].Value = iColTotal.ToString();
                iColTotal = 0;
            }

            return 0;
        }
#endregion
    }
}
