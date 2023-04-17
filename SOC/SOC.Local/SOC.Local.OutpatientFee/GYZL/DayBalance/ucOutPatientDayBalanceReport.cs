using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.OutpatientFee.GYZL.DayBalance
{
    /// <summary>
    /// �����շ��սᱨ���ӡ����
    /// </summary>
    public partial class ucOutPatientDayBalanceReport : UserControl
    {
        public ucOutPatientDayBalanceReport()
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
        /// <summary>
        /// ��ʾƱ����Ϣ
        /// </summary>
        protected bool blnShowBillInfo = false;
        /// <summary>
        /// ��ʾʹ��Ʊ����Ϣ
        /// </summary>
        protected bool blnShowUsedBill = false;
        /// <summary>
        /// ��ʾ����Ʊ����Ϣ
        /// </summary>
        protected bool blnShowValiBill = false;

        public List<FS.HISFC.Models.Fee.FeeCodeStat> lstFeecodeStat = null;
        public event FarpiontClick enentFarpiontClick;

        public delegate void FarpiontClick();
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
            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

            //��ʾ�����ս�ʱ����Ʊ�Ա
            //string strSpace = "               ";
            //string strInfo = "�Ʊ�Ա��" + deptMgr.Operator.Name + strSpace + "��ʼʱ�䣺" +
            //                beginDate + strSpace + "��ֹʱ�䣺" + endDate;
            string strInfo =   "ͳ��ʱ�䣺" + beginDate + " - " + endDate + "       �շ�Ա��" + deptMgr.Operator.Name;
            this.lblReportInfo.Text = strInfo;
            if (neuSpread1_Sheet1.Rows.Count > 0)
            {
                neuSpread1_Sheet1.Rows.Remove(0, neuSpread1_Sheet1.Rows.Count);
            }
        }


        /// <summary>
        /// �����ʾ
        /// </summary>
        public void Clear()
        {
            if (neuSpread1_Sheet1.Rows.Count > 0)
            {
                neuSpread1_Sheet1.Rows.Remove(0, neuSpread1_Sheet1.Rows.Count);
            }
            Clear("", "");
        }

        #region ����Farpoint��ʽ
        /// <summary>
        /// ������ʾ��ʽ
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="lstTitle"></param>
        protected void SetFpStyle(FarPoint.Win.Spread.SheetView sheet)
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
                sheet.Cells[sheet.RowCount - 1, 1].Text = "";
                sheet.Cells[sheet.RowCount - 1, 1].Tag = EnumCellName.A002;

                #region �Ʊ��˵�
                sheet.RowCount += 1;
                sheet.Cells[sheet.RowCount - 1, 0].Text = "�ɿ��ˣ�";
                //sheet.Cells[sheet.RowCount - 1, 2].Text = "�տ�Աǩ����";
                sheet.Cells[sheet.RowCount - 1, 3].Text = "�����ˣ�";
                sheet.Cells[sheet.RowCount - 1, 6].Text = "Ʊ������ˣ�";

                #endregion

                return;
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
        public void SetFarPoint(List<string> lstTitle)
        {
            FarPoint.Win.Spread.SheetView sheet = this.neuSpread1_Sheet1;

            if (lstTitle == null)
            {
                lstTitle = new List<string>();
            }
            SetFpStyle(sheet);
        }

        public void SetDetailName()
        {
            if (lstFeecodeStat != null && lstFeecodeStat.Count > 0)
            {
                int sortID = 0;
                FS.HISFC.Models.Fee.FeeCodeStat feecodeStat = null;
                for (int i = 0; i < lstFeecodeStat.Count; i++)
                {
                    feecodeStat = lstFeecodeStat[i];
                    sortID = feecodeStat.SortID - 1;

                    int rowIndex = Convert.ToInt32(sortID % 3);
                    int colIndex = Convert.ToInt32(sortID / 3) * 2;

                    this.neuSpread1_Sheet1.Cells.Get(rowIndex, colIndex).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    this.neuSpread1_Sheet1.Cells.Get(rowIndex, colIndex).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    this.neuSpread1_Sheet1.Cells.Get(rowIndex, colIndex).Value = feecodeStat.FeeStat.Name;

                    this.neuSpread1_Sheet1.Cells.Get(rowIndex, colIndex + 1).Value = "";
                    this.neuSpread1_Sheet1.Cells.Get(rowIndex, colIndex + 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    this.neuSpread1_Sheet1.Cells.Get(rowIndex, colIndex + 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

                }
            }
            else
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
            }

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

        /// <summary>
        /// ������ʾ��ʽ
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="lstTitle"></param>
        protected void SetFpStyleNew(FarPoint.Win.Spread.SheetView sheet, List<string> lstTitle)
        {
            try
            {
                if (sheet.Rows.Count > 4)
                {
                    sheet.Rows.Count = 4;
                }
                //�Ͻ��ֽ�
                sheet.RowCount += 1;
                sheet.Cells[sheet.RowCount - 1, 0].Text = "�Ͻ��ֽ�";

                sheet.Cells[sheet.RowCount - 1, 1].Text = "";
                sheet.Cells[sheet.RowCount - 1, 1].Tag = EnumCellName.A007;

                sheet.Cells[sheet.RowCount - 1, 2].Text = "��д:";
                sheet.Cells[sheet.RowCount - 1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                sheet.Cells[sheet.RowCount - 1, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                sheet.Cells[sheet.RowCount - 1, 3].Text = "";
                sheet.Models.Span.Add(sheet.RowCount - 1, 3, 1, 5);
                sheet.Cells[sheet.RowCount - 1, 3].Tag = EnumCellName.A001;
                sheet.Cells[sheet.RowCount - 1, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

                //��ЧƱ��
                sheet.Rows.Count += 1;

                sheet.Cells[sheet.RowCount - 1, 0].Text = "��Ч����";
                sheet.Cells[sheet.RowCount - 1, 1].Text = "";
                sheet.Cells[sheet.RowCount - 1, 1].Tag = EnumCellName.A002;
                //������
                sheet.Cells[sheet.RowCount - 1, 2].Text = "��������";
                sheet.Cells[sheet.RowCount - 1, 3].Text = "";
                sheet.Cells[sheet.RowCount - 1, 3].Tag = EnumCellName.A003;
                //���Ͻ��
                sheet.Cells[sheet.RowCount - 1, 4].Text = "���Ͻ�";
                sheet.Cells[sheet.RowCount - 1, 5].Text = "";
                sheet.Cells[sheet.RowCount - 1, 5].Tag = EnumCellName.A004;
                sheet.Rows[sheet.RowCount - 1].Visible = blnShowBillInfo;

                // ʹ��Ʊ�ݺ�
                sheet.Rows.Count += 1;
                sheet.Cells[sheet.RowCount - 1, 0].Text = "ʹ��Ʊ�ݺţ�";
                sheet.Cells[sheet.RowCount - 1, 1].Text = "";
                sheet.Models.Span.Add(sheet.RowCount - 1, 1, 1, 7);
                sheet.Cells[sheet.RowCount - 1, 1].Tag = EnumCellName.A005;
                sheet.Cells[sheet.RowCount - 1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
                sheet.Cells[sheet.RowCount - 1, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
                sheet.Rows[sheet.RowCount - 1].Height = 40;
                sheet.Rows[sheet.RowCount - 1].Visible = blnShowUsedBill;

                // ����Ʊ�ݺ�
                sheet.Rows.Count += 1;
                sheet.Cells[sheet.RowCount - 1, 0].Text = "����Ʊ�ݺţ�";
                sheet.Cells[sheet.RowCount - 1, 1].Text = "";
                sheet.Models.Span.Add(sheet.RowCount - 1, 1, 1, 7);
                sheet.Cells[sheet.RowCount - 1, 1].Tag = EnumCellName.A006;
                sheet.Cells[sheet.RowCount - 1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
                sheet.Cells[sheet.RowCount - 1, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;

                sheet.Rows[sheet.RowCount - 1].Height = 40;
                sheet.Rows[sheet.RowCount - 1].Visible = blnShowValiBill;

                sheet.RowCount += 1;

                int iCurrentRow = sheet.RowCount - 1;
                int colCount = 4;
                int iRowCount = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(lstTitle.Count * 1.0 / colCount).ToString());

                sheet.RowCount += iRowCount - 1;

                int idx = 0;
                for (int iRow = 0; iRow < iRowCount; iRow++)
                {
                    for (int iCol = 0; iCol < colCount; iCol++)
                    {
                        if (idx >= lstTitle.Count)
                        {
                            break;
                        }

                        sheet.Cells[iCurrentRow + iRow, iCol * 2].Text = lstTitle[idx];
                        sheet.Cells[iCurrentRow + iRow, iCol * 2 + 1].Text = "";
                        sheet.Cells[iCurrentRow + iRow, iCol * 2 + 1].Tag = lstTitle[idx];

                        idx++;
                    }
                }


                #region �Ʊ��˵�
                sheet.RowCount += 1;
                sheet.Cells[sheet.RowCount - 1, 0].Text = "�ɿ��ˣ�";
                //sheet.Cells[sheet.RowCount - 1, 2].Text = "�տ�Աǩ����";
                sheet.Cells[sheet.RowCount - 1, 3].Text = "�����ˣ�";
                sheet.Cells[sheet.RowCount - 1, 6].Text = "Ʊ������ˣ�";

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

        public void SetDetailNameNew()
        {
            if (lstFeecodeStat != null && lstFeecodeStat.Count > 0)
            {
                int sortID = 0;
                FS.HISFC.Models.Fee.FeeCodeStat feecodeStat = null;
                for (int i = 0; i < lstFeecodeStat.Count; i++)
                {
                    feecodeStat = lstFeecodeStat[i];
                    sortID = feecodeStat.SortID - 1;

                    int rowIndex = Convert.ToInt32(sortID % 3);
                    int colIndex = Convert.ToInt32(sortID / 3) * 2;

                    this.neuSpread1_Sheet1.Cells.Get(rowIndex, colIndex).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    this.neuSpread1_Sheet1.Cells.Get(rowIndex, colIndex).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    this.neuSpread1_Sheet1.Cells.Get(rowIndex, colIndex).Value = feecodeStat.FeeStat.Name;

                    this.neuSpread1_Sheet1.Cells.Get(rowIndex, colIndex + 1).Value = "";
                    this.neuSpread1_Sheet1.Cells.Get(rowIndex, colIndex + 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    this.neuSpread1_Sheet1.Cells.Get(rowIndex, colIndex + 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

                }
            }
            else
            {
               
            }
        }

        #endregion

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.neuSpread1.ActiveSheetIndex == 0)
            {
                if (this.enentFarpiontClick != null)
                {
                    this.enentFarpiontClick();
                }
            }
        }

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
        /// ʹ��Ʊ�ݺ�
        /// </summary>
        A005,

        /// <summary>
        /// ����Ʊ�ݺ�
        /// </summary>
        A006,
        /// <summary>
        /// �Ͻ��ֽ�(Сд)
        /// </summary>
        A007
    }
}
