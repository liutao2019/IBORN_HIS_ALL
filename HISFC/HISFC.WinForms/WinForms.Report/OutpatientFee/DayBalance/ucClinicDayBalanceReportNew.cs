using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.WinForms.Report.OutpatientFee.DayBalance
{
    public partial class ucClinicDayBalanceReportNew : UserControl
    {
        public ucClinicDayBalanceReportNew()
        {
            InitializeComponent();
        }

        private bool isCollectData = false;
        /// <summary>
        /// �Ƿ��������
        /// </summary>
        public bool IsCollectData
        {
            set
            {
                isCollectData = value;
            }
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void InitUC(string title)
        {
            // ����ҽԺ����
            FS.HISFC.BizLogic.Manager.Constant constant = new FS.HISFC.BizLogic.Manager.Constant();
            this.lbltitle.Text = constant.GetHospitalName() + title;
        }

        #region ����Farpoint��ʽ
        /// <summary>
        /// ����Farpoint��Ʊ��ʽ
        /// </summary>
        /// <param name="sheet"></param>
        protected virtual void SetInvoiceFarpoint(FarPoint.Win.Spread.SheetView sheet)
        {
            int RowCount = sheet.Rows.Count;
            #region ��Ʊ��ʽ
            if (!isCollectData)
            {
                //��ֹƱ�ݺ�
                sheet.Rows.Count += 1;
                RowCount++;
                sheet.Cells[RowCount - 1, 0].Text = "ʹ��Ʊ�ݺţ�";//luoff
                sheet.Models.Span.Add(RowCount - 1, 1, 1, 5);//luoff
                sheet.Cells[RowCount - 1, 1].Tag = "A00101";
                                                                      //���޸�
                //sheet.Cells[RowCount - 1, 3].Text = "��ֹƱ�ݺ�";//luoff
                //sheet.Models.Span.Add(RowCount - 1, 4, 1, 2);
                //sheet.Cells[RowCount - 1, 4].Tag = "A00102";
            }
            //Ʊ������
            sheet.Rows.Count += 1;
            RowCount++;
            sheet.Cells[RowCount - 1, 0].Text = "Ʊ��������";
            sheet.Models.Span.Add(RowCount - 1, 1, 1, 5);
            sheet.Cells[RowCount - 1, 1].Tag = "A002";

            //��ЧƱ��
            sheet.Rows.Count += 1;
            RowCount++;
            sheet.Cells[RowCount - 1, 1].Tag = "A003";
            sheet.Cells[RowCount - 1, 0].Text = "��ЧƱ������";

            //�˷�Ʊ��
            sheet.Cells[RowCount - 1, 2].Text = "�˷�Ʊ������";
            sheet.Cells[RowCount - 1, 3].Tag = "A00401";
            //�˷�Ʊ�ݺ�: �����ս�Ͳ�ѯʱ��ʾ���ڻ���ʱ����ʾ
            if (!this.isCollectData)
            {
                //�˷�Ʊ�ݺ�
                sheet.Rows.Count += 1;
                sheet.Models.Span.Add(RowCount, 1, 1, 5);
                sheet.Cells[RowCount, 1].Tag = "A00402";
                sheet.Cells[RowCount, 0].Text = "�˷�Ʊ�ݺţ�";
                sheet.Rows[RowCount].Height = 50;
            }
            //����Ʊ��
            if (!this.isCollectData)
            {
                sheet.Cells[RowCount - 1, 4].Text = "����Ʊ������";
                sheet.Cells[RowCount - 1, 5].Tag = "A00501";

                //����Ʊ�ݺ�
                sheet.Rows.Count += 1;
                sheet.Models.Span.Add(RowCount + 1, 1, 1, 5);
                sheet.Rows[RowCount + 1].Height = 50;
                sheet.Cells[RowCount + 1, 1].Tag = "A00502";
                sheet.Cells[RowCount + 1, 0].Text = "����Ʊ�ݺţ�";
            }
            #endregion
        }

        /// <summary>
        /// ������ʾ���
        /// </summary>
        /// <param name="sheet">SheetView</param>
        protected virtual void SetMoneyFarpoint(FarPoint.Win.Spread.SheetView sheet)
        {
            int rowCount = sheet.Rows.Count;
            sheet.Rows.Count += 9;
            sheet.Cells[rowCount, 0].Text = "�˷ѽ��";
            sheet.Cells[rowCount, 1].Tag = "A006";
            sheet.Cells[rowCount, 2].Text = "���Ͻ��";
            sheet.Cells[rowCount, 3].Tag = "A007";
            sheet.Cells[rowCount, 4].Text = "Ѻ����";
            sheet.Cells[rowCount, 5].Tag = "A008";
            sheet.Cells[rowCount + 1, 0].Text = "��Ѻ���";
            sheet.Cells[rowCount + 1, 1].Tag = "A009";
            sheet.Cells[rowCount + 1, 2].Text = "������";
            sheet.Cells[rowCount + 1, 3].Tag = "A010";
            sheet.Cells[rowCount + 1, 4].Text = "��������";
            sheet.Cells[rowCount + 1, 5].Tag = "A011";

            sheet.Cells[rowCount + 2, 0].Text = "����ҽ��";
            sheet.Cells[rowCount + 2, 1].Tag = "A012";
            sheet.Cells[rowCount + 2, 2].Text = "�����Ը�";
            sheet.Cells[rowCount + 2, 3].Tag = "A013";
            sheet.Cells[rowCount + 2, 4].Text = "�����˻�";
            sheet.Cells[rowCount + 2, 5].Tag = "A026";

            sheet.Cells[rowCount + 3, 0].Text = "�б��Ը�";
            sheet.Cells[rowCount + 3, 1].Tag = "A014";
            sheet.Cells[rowCount + 3, 2].Text = "�б��˻�";
            sheet.Cells[rowCount + 3, 3].Tag = "A015";
            sheet.Cells[rowCount + 3, 4].Text = "�б�ͳ��";
            sheet.Cells[rowCount + 3, 5].Tag = "A016";
            sheet.Cells[rowCount + 4, 0].Text = "�б����";
            sheet.Cells[rowCount + 4, 1].Tag = "A017";

            sheet.Cells[rowCount + 5, 0].Text = "ʡ���Ը�";
            sheet.Cells[rowCount + 5, 1].Tag = "A018";
            sheet.Cells[rowCount + 5, 2].Text = "ʡ���˻�";
            sheet.Cells[rowCount + 5, 3].Tag = "A019";
            sheet.Cells[rowCount + 5, 4].Text = "ʡ��ͳ��";
            sheet.Cells[rowCount + 5, 5].Tag = "A020";
            sheet.Cells[rowCount + 6, 0].Text = "ʡ�����";
            sheet.Cells[rowCount + 6, 1].Tag = "A021";
            sheet.Cells[rowCount + 6, 2].Text = "ʡ����Ա";
            sheet.Cells[rowCount + 6, 3].Tag = "A022";


            sheet.Cells[rowCount + 7, 0].Text = "�Ͻ��ֽ��";
            sheet.Cells[rowCount + 7, 1].Tag = "A023";
            sheet.Cells[rowCount + 7, 2].Text = "�Ͻ�֧Ʊ��";
            sheet.Cells[rowCount + 7, 3].Tag = "A024";
            sheet.Cells[rowCount + 7, 4].Text = "�Ͻ�������";
            sheet.Cells[rowCount + 7, 5].Tag = "A025";

            sheet.Cells[rowCount + 8, 0].Text = "�Ͻ��ֽ���д����";
            sheet.Models.Span.Add(rowCount + 8, 1, 1, 5);
            sheet.Cells[rowCount + 8, 1].Tag = "A1000";
        }

        /// <summary>
        /// ����FarPoint��ʽ
        /// </summary>
        /// <param name="sheet"></param>
        public virtual void SetFarPoint()
        {
            FarPoint.Win.Spread.SheetView sheet = this.neuSpread1_Sheet1;
            SetInvoiceFarpoint(sheet);
            SetMoneyFarpoint(sheet);

            sheet.Rows.Count += 1;
            int count = sheet.Rows.Count;
            sheet.Cells[count - 1, 0].Text = "�Ʊ��ˣ�";
            sheet.Models.Span.Add(count - 1, 1, 1, 2);
            sheet.Cells[count - 1, 3].Text = "�տ�Աǩ����";
            sheet.Models.Span.Add(count - 1, 4, 1, 2);
            sheet.Rows.Count += 1;
            count = sheet.Rows.Count;
            sheet.Cells[count - 1, 0].Text = "����ˣ�";
            sheet.Models.Span.Add(count - 1, 1, 1, 2);
            sheet.Models.Span.Add(count - 1, 4, 1, 2);
        }

        #endregion

    }
}
