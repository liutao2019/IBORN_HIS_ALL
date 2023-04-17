using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SOC.Fee.DayBalance.Manager;

namespace SOC.Fee.DayBalance.Inpatient
{
    /// <summary>
    /// סԺ�շ��սᱨ���ӡ����
    /// </summary>
    public partial class ucInpatientDayBalanceReport : UserControl
    {
        public ucInpatientDayBalanceReport()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// �Ƿ��������
        /// </summary>
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

        InpatientDayBalanceManage inpatientDayBalanceManage = new InpatientDayBalanceManage();

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void InitUC(string title)
        {
            this.lbltitle.Text = title;
        }

        /// <summary>
        /// �����ʾ
        /// </summary>
        public void Clear(string beginDate, string endDate)
        {
            //��ʾ�����ս�ʱ����Ʊ�Ա
            string strSpace = "               ";
            string strInfo = "�Ʊ�Ա��" + inpatientDayBalanceManage.Operator.Name + strSpace + "��ʼʱ�䣺" +
                            beginDate + strSpace + "��ֹʱ�䣺" + endDate;
            this.lblReportInfo.Text = strInfo;
            this.lblinvoiceInfo.Text = "";
        }

        #region ����Farpoint��ʽ

        /// <summary>
        /// ������ʾ��ʽ
        /// </summary>
        /// <param name="sheet">SheetView</param>
        protected virtual void SetFpStyle(FarPoint.Win.Spread.SheetView sheet)
        {      

            try
            {
                if (sheet.Rows.Count > 4)
                {
                    sheet.Rows.Count = 4;
                }

                //��ЧƱ��
                sheet.Rows.Count += 1;
                sheet.Cells[sheet.RowCount - 1, 0].Text = "��Ч����";
                sheet.Cells[sheet.RowCount - 1, 1].Tag = EnumCellName.A002;
                //������
                sheet.Cells[sheet.RowCount - 1, 2].Text = "��������";
                sheet.Cells[sheet.RowCount - 1, 3].Tag = EnumCellName.A003;
                //���Ͻ��
                sheet.Cells[sheet.RowCount - 1, 4].Text = "���Ͻ�";
                sheet.Cells[sheet.RowCount - 1, 5].Tag = EnumCellName.A004;

                sheet.RowCount += 1;
                //��������
                sheet.Cells[sheet.RowCount - 1, 0].Text = "ְ��ҽ����";
                sheet.Cells[sheet.RowCount - 1, 1].Tag = EnumCellName.A005;
                //�������
                sheet.Cells[sheet.RowCount - 1, 2].Text = "������⣺";
                sheet.Cells[sheet.RowCount - 1, 3].Tag = EnumCellName.A006;
                //����ҽ��
                sheet.Cells[sheet.RowCount - 1, 4].Text = "����ҽ�ƣ�";
                sheet.Cells[sheet.RowCount - 1, 5].Tag = EnumCellName.A007;
                //����Ա
                sheet.Cells[sheet.RowCount - 1, 6].Text = "����Ա��";
                sheet.Cells[sheet.RowCount - 1, 7].Tag = EnumCellName.A008;

                sheet.RowCount += 1;
               
                //�Ż��Ŵ�
                sheet.Cells[sheet.RowCount - 1, 0].Text = "�Ż��Ŵ���";
                sheet.Cells[sheet.RowCount - 1, 1].Tag = EnumCellName.A009;
                //����ҽ��
                sheet.Cells[sheet.RowCount - 1, 2].Text = "����ҽ����";
                sheet.Cells[sheet.RowCount - 1, 3].Tag = EnumCellName.A011;
                //��Ժְ������
                sheet.Cells[sheet.RowCount - 1, 4].Text = "��Ժ���⣺";
                sheet.Cells[sheet.RowCount - 1, 5].Tag = EnumCellName.A012;

                //�Ͻɽ��
                sheet.RowCount += 1;
                sheet.Cells[sheet.RowCount - 1, 0].Text = "�Ͻɽ��";
                sheet.Cells[sheet.RowCount - 1, 1].Tag = EnumCellName.A001;
                sheet.Cells[sheet.RowCount - 1, 2].Text = "��д";
                sheet.Models.Span.Add(sheet.RowCount - 1, 3, 1, 5);
                

                sheet.RowCount += 1;
                //�˻�����
                sheet.Cells[sheet.RowCount - 1, 0].Text = "Ԥ����";
                sheet.Cells[sheet.RowCount - 1, 1].Tag = EnumCellName.A010;               
                //�ֽ����
                sheet.Cells[sheet.RowCount - 1, 2].Text = "�ֽ��";
                sheet.Cells[sheet.RowCount - 1, 3].Tag = EnumCellName.A013;

                #region �Ʊ��˵�
                sheet.RowCount += 1;
                sheet.Cells[sheet.RowCount - 1, 0].Text = "�Ʊ��ˣ�";
                //sheet.Cells[sheet.RowCount - 1, 2].Text = "�տ�Աǩ����";
                sheet.Cells[sheet.RowCount - 1, 4].Text = "����ˣ�";

                #endregion

                return;

                #region ������

                #region ��Ʊ��ʽ

                if (!isCollectData)
                {
                    //��ֹƱ�ݺ�
                    sheet.Rows.Count += 1;
                    sheet.Cells[sheet.RowCount - 1, 0].Text = "ʹ��Ʊ�ݺţ�";//luoff
                    sheet.Models.Span.Add(sheet.RowCount - 1, 1, 1, 5);//luoff
                    sheet.Cells[sheet.RowCount - 1, 1].Tag = "A00101";
                }
                //Ʊ������
                sheet.Rows.Count += 1;
                sheet.Cells[sheet.RowCount - 1, 0].Text = "Ʊ��������";
                sheet.Models.Span.Add(sheet.RowCount - 1, 1, 1, 5);
                sheet.Cells[sheet.RowCount - 1, 1].Tag = "A002";

                //�˷�Ʊ��
                sheet.Cells[sheet.RowCount - 1, 2].Text = "�˷�Ʊ������";
                sheet.Cells[sheet.RowCount - 1, 3].Tag = "A00401";
                //�˷�Ʊ�ݺ�: �����ս�Ͳ�ѯʱ��ʾ���ڻ���ʱ����ʾ
                if (!this.isCollectData)
                {
                    //�˷�Ʊ�ݺ�
                    sheet.Rows.Count += 1;
                    sheet.Models.Span.Add(sheet.RowCount - 1, 1, 1, 5);
                    sheet.Cells[sheet.RowCount - 1, 1].Tag = "A00402";
                    sheet.Cells[sheet.RowCount - 1, 0].Text = "�˷�Ʊ�ݺţ�";
                    sheet.Rows[sheet.RowCount - 1].Height = 50;
                }
                //����Ʊ��
                if (!this.isCollectData)
                {
                    //����Ʊ�ݺ�
                    sheet.Rows.Count += 1;

                    sheet.Cells[sheet.RowCount - 1, 4].Text = "����Ʊ������";
                    sheet.Cells[sheet.RowCount - 1, 5].Tag = "A00501";
                    sheet.Models.Span.Add(sheet.RowCount - 1 + 1, 1, 1, 5);
                    sheet.Rows[sheet.RowCount - 1].Height = 50;
                    sheet.Cells[sheet.RowCount - 1, 1].Tag = "A00502";
                    sheet.Cells[sheet.RowCount - 1, 0].Text = "����Ʊ�ݺţ�";
                }
                #endregion

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
                sheet.Cells[rowCount + 7, 4].Text = "�Ͻ��˻���";
                sheet.Cells[rowCount + 7, 5].Tag = "A025";
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ����FarPoint��ʾ
        /// </summary>
        /// <param name="sheet"></param>
        public virtual void SetFarPoint()
        {
            FarPoint.Win.Spread.SheetView sheet = this.neuSpread1_Sheet1;

            SetFpStyle(sheet);
        }

        public void SetDetailName()
        {
           
            this.neuSpread1_Sheet1.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 0).Value = "��ҩ��";
            this.neuSpread1_Sheet1.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(0, 1).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 2).Value = "����";
            this.neuSpread1_Sheet1.Cells.Get(0, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(0, 3).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(0, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 4).Value = "������";
            this.neuSpread1_Sheet1.Cells.Get(0, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(0, 5).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(0, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 6).Value = "��λ��";
            this.neuSpread1_Sheet1.Cells.Get(0, 6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(0, 7).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(0, 7).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(0, 7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 0).Value = "�г�ҩ";
            this.neuSpread1_Sheet1.Cells.Get(1, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(1, 1).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(1, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 2).Value = "�����";
            this.neuSpread1_Sheet1.Cells.Get(1, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(1, 3).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(1, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 4).Value = "���Ʒ�";
            this.neuSpread1_Sheet1.Cells.Get(1, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(1, 5).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(1, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 6).Value = "�����";
            this.neuSpread1_Sheet1.Cells.Get(1, 6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(1, 7).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(1, 7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(2, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(2, 0).Value = "�в�ҩ";
            this.neuSpread1_Sheet1.Cells.Get(2, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(2, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(2, 1).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(2, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(2, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(2, 2).Value = "����";
            this.neuSpread1_Sheet1.Cells.Get(2, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(2, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(2, 3).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(2, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(2, 4).Value = "���Ϸ�";
            this.neuSpread1_Sheet1.Cells.Get(2, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(2, 5).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(2, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(2, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(2, 6).Value = "������";
            this.neuSpread1_Sheet1.Cells.Get(2, 6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(2, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(2, 7).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(2, 7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(3, 0).Value = "�ϼƣ�";
            this.neuSpread1_Sheet1.Cells.Get(3, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(3, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(3, 1).Value = "0.00";
            this.neuSpread1_Sheet1.Cells.Get(3, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(3, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(3, 2).TabStop = true;
            this.neuSpread1_Sheet1.Cells.Get(3, 2).Value = "��д:";
            this.neuSpread1_Sheet1.Cells.Get(3, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(3, 3).ColumnSpan = 5;
            this.neuSpread1_Sheet1.Cells.Get(3, 3).Value = "";
        }

        #endregion

        #endregion

    }

    /// <summary>
    /// ��ʾ��������
    /// </summary>
    public enum EnumCellName
    {
        /// <summary>
        /// �Ͻ��ֽ����֣�
        /// </summary>
        A001,

        /// <summary>
        /// ��Ч��Ʊ��
        /// </summary>
        A002,

        /// <summary>
        /// ���Ϸ�Ʊ��
        /// </summary>
        A003,

        /// <summary>
        /// ���Ͻ��
        /// </summary>
        A004,

        /// <summary>
        /// ����������
        /// </summary>
        A005,

        /// <summary>
        /// ���������
        /// </summary>
        A006,

        /// <summary>
        /// ����ҽ�ƽ��
        /// </summary>
        A007,

        /// <summary>
        /// ����Ա���
        /// </summary>
        A008,

        /// <summary>
        /// �Ż��Ŵ����
        /// </summary>
        A009,

        /// <summary>
        /// �˻�������
        /// </summary>
        A010,

        /// <summary>
        /// ����ҽ���������
        /// </summary>
        A011,

        /// <summary>
        /// ��Ժְ��������
        /// </summary>
        A012,

        /// <summary>
        /// �ֽ������
        /// </summary>
        A013
    }
}
