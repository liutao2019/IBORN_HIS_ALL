using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.FrameWork.Function;

namespace Neusoft.WinForms.Report.InpatientFee
{
    public partial class ucDayReportNew : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDayReportNew()
        {
            InitializeComponent();
        }

        #region ����
        Neusoft.FrameWork.WinForms.Forms.ToolBarService toolBarService = new Neusoft.FrameWork.WinForms.Forms.ToolBarService();
        /// <summary>
        /// 1ͳ�� 2��ѯ����
        /// </summary>
        protected string OperMode = "0";

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime dtBegin = DateTime.MinValue;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime dtEnd = DateTime.MinValue;

        /// <summary>
        /// �ս�ҵ���
        /// </summary>
        Functions.DayReport feeDayreport = new Report.InpatientFee.Functions.DayReport();
        private DateTime dtDefaultBeginDate = Neusoft.FrameWork.Function.NConvert.ToDateTime("2006-01-01");
        DataSet ds = null;
        private Class.DayReport dayReport = new Report.InpatientFee.Class.DayReport();
        private Class.Item item = null;
        private OperType operType=OperType.DayReport;
        private List<Class.DayReport> listEnviroment = null;
        #endregion

        #region ����
        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        public DateTime DtBegin
        {
            get
            {
                return this.dtBegin;
            }
            set
            {
                this.dtBegin = value;
                this.lblBeginDate.Text = this.dtBegin.ToString();
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime DtEnd
        {
            get
            {
                return this.dtEnd;
            }
            set
            {
                this.dtEnd = value;
                this.lblEndDate.Text = this.dtEnd.ToString();
            }
        }
        /// <summary>
        /// Ĭ�Ͽ�ʼʱ��
        /// </summary>
        public DateTime DefaultBeginDate
        {
            get
            {
                return this.dtDefaultBeginDate;
            }
            set
            {
                this.dtDefaultBeginDate = value;
            }
        }
        
        [Description("DayReport���սᡡCollectDayReport������"),Browsable(true)]
        public OperType OperationType
        {
            get
            {
                return operType;
            }
            set
            {
                operType = value;
            }
        }
        #endregion

        #region ö��
        /// <summary>
        /// Farpoint��Cell��tag
        /// </summary>
        private enum EnumColumn
        {
            /// <summary>
            /// ҽ��Ԥ�տ�(�跽���)
            /// </summary>
            DebitPrePay,
            /// <summary>
            /// ҽ��Ԥ�տ�(�������)
            /// </summary>
            LenderPrePay,
            /// <summary>
            /// ҽ��Ӧ�տ�(�������)
            /// </summary>
            LenderPay,
            /// <summary>
            /// ���д��(�跽���)
            /// </summary>
            DebitBank,
            /// <summary>
            /// ���д��(�������)
            /// </summary>
            LenderBank,
            /// <summary>
            /// �ֽ�(�跽���)
            /// </summary>
            DebitCACost,
            /// <summary>
            /// �ֽ�(�������)
            /// </summary>
            LenderCACost,

            /// <summary>
            /// ���п�(�跽���)
            /// </summary>
            DebitBankCard,
            /// <summary>
            /// ���п�(�������)
            /// </summary>
            LenderBankCard,
            /// <summary>
            /// Ժ���ʻ�(�跽���)
            /// </summary>
            DebitYSCost,
            /// <summary>
            /// Ժ���ʻ�(�������)
            /// </summary>
            LenderYSCost,
            /// <summary>
            /// ����Ԥ��(�跽���)
            /// </summary>
            DebitOtherPrePay,
            /// <summary>
            /// ����Ԥ��(�������)
            /// </summary>
            LenderOtherPrePay,
            /// <summary>
            /// ѪѺ��(�������)
            /// </summary>
            Blood,
            /// <summary>
            /// ����ҽ��(�跽���)
            /// </summary>
            Derate,
            /// <summary>
            /// ����ҽ�Ƽ���(�跽���)
            /// </summary>
            BusaryPubCost,
            /// <summary>
            /// ��ҽ���ʻ�(�跽���)
            /// </summary>
            CmedicarePayCost,
            /// <summary>
            /// �б�ͳ��(�跽���)
            /// </summary>
            CmedicarePubCost,
            /// <summary>
            /// �б����(�跽���)
            /// </summary>
            CmedicareOverCost,
            /// <summary>
            /// ʡ���ʻ�(�跽���)
            /// </summary>
            PmedicarePayCost,
            /// <summary>
            /// ʡҽ��ͳ��
            /// </summary>
            PmedicarePubCost,
            /// <summary>
            /// ʡ�����
            /// </summary>
            PmedicareOverCost,
            /// <summary>
            /// ʡ������Ա
            /// </summary>
            PmedicareOfficialCost,
            /// <summary>
            /// �跽�ϼ�
            /// </summary>
            DebitSummer,
            /// <summary>
            /// �����ϼ�
            /// </summary>
            LenderSummer,
            /// <summary>
            /// Ԥ����Ʊ�ݺŷ�Χ
            /// </summary>
            PrePayInvoiceBound,
            /// <summary>
            /// Ԥ��������
            /// </summary>
            PrePayInvoiceCount,
            /// <summary>
            /// Ԥ������Ч����
            /// </summary>
            PrePayUseInvoiceCount,//luoff
            /// <summary>
            /// Ԥ������������
            /// </summary>
            PrePayWasteInvoiceCount,
            /// <summary>
            /// Ԥ��������Ʊ�ݺ�
            /// </summary>
            PrePayWasteInvoiceNo ,
            /// <summary>
            /// ����Ʊ�ݺŷ�Χ
            /// </summary>
            BalanceInvoiceBound,
            /// <summary>
            /// ����Ʊ������
            /// </summary>
            BalanceInvoiceCount,
            /// <summary>
            /// ����Ʊ����Ч����
            /// </summary>
            BalanceUseInvoiceCount,//luoff
            /// <summary>
            /// ����Ʊ����������
            /// </summary>
            BalanceWasteInvoiceCount,
            /// <summary>
            /// ��������Ʊ�ݺ�
            /// </summary>
            BalanceWasteInvoiceNo,
            /// <summary>
            /// �Ͻ��ֽ�
            /// </summary>
            PayTotCost,
            /// <summary>
            /// ��д
            /// </summary>
            PayTotCostCapital,
            /// <summary>
            /// �Ͻ�����
            /// </summary>
            PayBankCardTotCost,
            /// <summary>
            /// �Ͻ�֧Ʊ��
            /// </summary>
            PayBankTotCost,
        }
        #endregion

        #region ����

        #region ��ʼ��
        /// <summary>
        /// ��ʼ������
        /// </summary>
        protected virtual void Init()
        {

            if (OperationType == OperType.DayReport)
            {
                //��ʼ��ʱ��Ͳ���Ա
                this.lblOperator.Text = this.feeDayreport.Operator.Name;
                this.DtEnd = this.feeDayreport.GetDateTimeFromSysDateTime();
                //��ȡ�ϴ��ս���Ϣʵ��
                Class.DayReport dayReport = new Report.InpatientFee.Class.DayReport();
                dayReport = this.feeDayreport.GetOperLastDayReport(this.feeDayreport.Operator.ID);
                if (dayReport == null)
                {
                    dayReport = new Report.InpatientFee.Class.DayReport();
                    dayReport.EndDate = this.DefaultBeginDate;
                }
                this.DtBegin = dayReport.EndDate;
            }
            else
            {
                this.panelDayReport.Visible = false;
            }
            this.SetFarPoint(this.neuSpread1_Sheet1);
        }
        #endregion

        #region �����ս�ʱ��
        private void SetDayReprotDate()
        {
            this.DtEnd = this.feeDayreport.GetDateTimeFromSysDateTime();
            //��ȡ�ϴ��ս���Ϣʵ��
            //�������ȫ�ֱ�������Ȼȫ�ֱ���û����� {D1C44D8F-2BFB-4ff3-A689-4E8F42E79251} wbo 2010-08-20
            //Class.DayReport dayReport = new Report.InpatientFee.Class.DayReport();
            dayReport = null;
            dayReport = this.feeDayreport.GetOperLastDayReport(this.feeDayreport.Operator.ID);
            if (dayReport == null)
            {
                dayReport = new Report.InpatientFee.Class.DayReport();
                dayReport.EndDate = this.DefaultBeginDate;
            }
            this.DtBegin = dayReport.EndDate;
        }
        #endregion

        #region ����FarPoint
        /// <summary>
        /// ����Farpoint
        /// </summary>
        /// <param name="sheet">SheetView</param>
        private void SetFarPoint(FarPoint.Win.Spread.SheetView sheet)
        {
            if (sheet.Rows.Count > 0)
            {
                sheet.Rows.Remove(0, sheet.Rows.Count);
            }
            int index = 0;
            //�ϲ���ֹ��
            int beginSpan = 0;
            int endSpan = 0;
            //��Ŀ�ϼ�
            index = AddFarpointRow(sheet);
            sheet.Cells[index, 0].Text = "�ϼƣ�";
            sheet.Models.Span.Add(index, 1, 1, 3);
            sheet.Cells[index, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            
            index = AddFarpointRow(sheet);
            sheet.Cells[index, 0].Text = "�跽���";
            sheet.Cells[index, 1].Text = "��ƿ�Ŀ";
            sheet.Cells[index, 2].Text = "�������";
            //�ϲ���ʼ��
            beginSpan = index;

            index = AddFarpointRow(sheet);
            sheet.Cells[index, 1].Text = "ҽ��Ԥ�տ�";
            sheet.Cells[index, 0].Tag = EnumColumn.DebitPrePay.ToString();
            sheet.Cells[index, 0].Note = "˫���õ�Ԫ����Բ쿴�ʹ�ӡ��ϸ";
            sheet.Cells[index, 0].NoteStyle = FarPoint.Win.Spread.NoteStyle.PopupNote;
            sheet.Cells[index, 2].Tag = EnumColumn.LenderPrePay.ToString();
            sheet.Cells[index, 2].Note = "˫���õ�Ԫ����Բ쿴�ʹ�ӡ��ϸ";
            sheet.Cells[index, 2].NoteStyle = FarPoint.Win.Spread.NoteStyle.PopupNote;
           
            index = AddFarpointRow(sheet);
            sheet.Cells[index, 1].Text = "ҽ��Ӧ�տ�";
            sheet.Cells[index, 2].Tag = EnumColumn.LenderPay.ToString();
            sheet.Cells[index, 2].Note = "˫���õ�Ԫ����Բ쿴�ʹ�ӡ��ϸ";
            sheet.Cells[index, 2].NoteStyle = FarPoint.Win.Spread.NoteStyle.PopupNote;

            index = AddFarpointRow(sheet);
            sheet.Cells[index, 1].Text = "���д��";
            sheet.Cells[index, 0].Tag = EnumColumn.DebitBank.ToString();
            sheet.Cells[index, 2].Tag = EnumColumn.LenderBank.ToString();

            index = AddFarpointRow(sheet);
            sheet.Cells[index, 1].Text = "�ֽ�";
            sheet.Cells[index, 0].Tag = EnumColumn.DebitCACost.ToString();
            sheet.Cells[index, 2].Tag = EnumColumn.LenderCACost.ToString();

            index = AddFarpointRow(sheet);
            sheet.Cells[index, 1].Text = "���п�";
            sheet.Cells[index, 0].Tag = EnumColumn.DebitBankCard.ToString();
            sheet.Cells[index, 2].Tag = EnumColumn.LenderBankCard.ToString();

            index = AddFarpointRow(sheet);
            sheet.Cells[index, 1].Text = "Ժ���ʻ�";
            sheet.Cells[index, 0].Tag = EnumColumn.DebitYSCost.ToString();
            sheet.Cells[index, 2].Tag = EnumColumn.LenderYSCost.ToString();

            index = AddFarpointRow(sheet);
            sheet.Cells[index, 1].Text = "����Ԥ��";
            sheet.Cells[index, 0].Tag = EnumColumn.DebitOtherPrePay.ToString();
            sheet.Cells[index, 2].Tag = EnumColumn.LenderOtherPrePay.ToString();


            //ѪѺ����ʱ��ȥ��
            index = AddFarpointRow(sheet);
            sheet.Cells[index, 1].Text = "ѪѺ��";
            sheet.Cells[index, 2].Tag = EnumColumn.Blood.ToString();
            sheet.Rows[index].Visible = false;

            index = AddFarpointRow(sheet);
            sheet.Cells[index, 1].Text = "ҽ�Ƽ���";
            sheet.Cells[index, 0].Tag = EnumColumn.Derate.ToString();

            index = AddFarpointRow(sheet);
            sheet.Cells[index, 1].Text = "����ҽ�Ƽ���";
            sheet.Cells[index, 0].Tag = EnumColumn.BusaryPubCost.ToString();

            index = AddFarpointRow(sheet);
            sheet.Cells[index, 1].Text = "�б��˻�";
            sheet.Cells[index, 0].Tag = EnumColumn.CmedicarePayCost.ToString();

            index = AddFarpointRow(sheet);
            sheet.Cells[index, 1].Text = "�б�ͳ��";
            sheet.Cells[index, 0].Tag = EnumColumn.CmedicarePubCost.ToString();

            index = AddFarpointRow(sheet);
            sheet.Cells[index, 1].Text = "�б����";
            sheet.Cells[index, 0].Tag = EnumColumn.CmedicareOverCost.ToString();

            index = AddFarpointRow(sheet);
            sheet.Cells[index, 1].Text = "ʡ���˻�";
            sheet.Cells[index, 0].Tag = EnumColumn.PmedicarePayCost.ToString();

            index = AddFarpointRow(sheet);
            sheet.Cells[index, 1].Text = "ʡ��ͳ��";
            sheet.Cells[index, 0].Tag = EnumColumn.PmedicarePubCost.ToString();

            index = AddFarpointRow(sheet);
            sheet.Cells[index, 1].Text = "ʡ�����";
            sheet.Cells[index, 0].Tag = EnumColumn.PmedicareOverCost.ToString();

            index = AddFarpointRow(sheet);
            sheet.Cells[index, 1].Text = "ʡ������Ա";
            sheet.Cells[index, 0].Tag = EnumColumn.PmedicareOfficialCost.ToString();

            index = AddFarpointRow(sheet);
            sheet.Cells[index, 1].Text = "�ϼ�";
            sheet.Cells[index, 0].Tag = EnumColumn.DebitSummer.ToString();
            sheet.Cells[index, 2].Tag = EnumColumn.LenderSummer.ToString();
            endSpan = index;

            for (int i = beginSpan; i < endSpan; i++)
            {
                sheet.Models.Span.Add(i, 2, 1, 2);
            }

            if (OperationType == OperType.DayReport)
            {
                index = AddFarpointRow(sheet);
                sheet.Cells[index, 0].Text = "Ԥ����Ʊ�ݺŷ�Χ";
                sheet.Models.Span.Add(index, 1, 1, 3);
                sheet.Cells[index, 1].Tag = EnumColumn.PrePayInvoiceBound.ToString();
                sheet.Rows[index].Height = 50;
                sheet.Cells[index, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }
            index = AddFarpointRow(sheet);
            sheet.Cells[index, 0].Text = "Ԥ��������";
            sheet.Cells[index, 1].Tag = EnumColumn.PrePayInvoiceCount.ToString();//luoff
            sheet.Models.Span.Add(index, 1, 1, 3);//luoff
            sheet.Cells[index, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            
            index = AddFarpointRow(sheet);
            sheet.Cells[index, 0].Text = "Ԥ������Ч����";
            sheet.Cells[index, 1].Tag = EnumColumn.PrePayUseInvoiceCount.ToString();
            sheet.Cells[index, 2].Text = "Ԥ������������";
            sheet.Cells[index, 3].Tag = EnumColumn.PrePayWasteInvoiceCount.ToString();
            if (OperationType == OperType.DayReport)
            {
                index = AddFarpointRow(sheet);
                sheet.Cells[index, 0].Text = "Ԥ��������Ʊ�ݺ�";
                sheet.Cells[index, 1].Tag = EnumColumn.PrePayWasteInvoiceNo.ToString();
                sheet.Rows[index].Height = 80;
                sheet.Models.Span.Add(index, 1, 1, 3);
                sheet.Cells[index, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }

            if (OperationType == OperType.DayReport)
            {
                index = AddFarpointRow(sheet);
                sheet.Cells[index, 0].Text = "����Ʊ�ݺŷ�Χ";
                sheet.Models.Span.Add(index, 1, 1, 3);
                sheet.Cells[index, 1].Tag = EnumColumn.BalanceInvoiceBound.ToString();
                sheet.Rows[index].Height = 50;
                sheet.Cells[index, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }

            index = AddFarpointRow(sheet);
            sheet.Cells[index, 0].Text = "����Ʊ������";
            sheet.Cells[index, 1].Tag = EnumColumn.BalanceInvoiceCount.ToString();
            sheet.Models.Span.Add(index, 1, 1, 3);

            index = AddFarpointRow(sheet);
            sheet.Cells[index, 0].Text = "����Ʊ����Ч����";
            sheet.Cells[index, 1].Tag = EnumColumn.BalanceUseInvoiceCount.ToString();

            sheet.Cells[index, 2].Text = "����Ʊ����������";
            sheet.Cells[index, 3].Tag = EnumColumn.BalanceWasteInvoiceCount.ToString();

            if (OperationType == OperType.DayReport)
            {
                index = AddFarpointRow(sheet);
                sheet.Cells[index, 0].Text = "��������Ʊ�ݺ�";
                sheet.Cells[index, 1].Tag = EnumColumn.BalanceWasteInvoiceNo.ToString();
                sheet.Rows[index].Height = 80;
                sheet.Models.Span.Add(index, 1, 1, 3);
                sheet.Cells[index, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }

            index = AddFarpointRow(sheet);
            sheet.Cells[index, 0].Text = "�Ͻ��ֽ�";
            sheet.Cells[index, 1].Tag = EnumColumn.PayTotCost.ToString();
            sheet.Cells[index, 2].Text = "����д����";
            sheet.Cells[index, 2].Tag = EnumColumn.PayTotCostCapital.ToString();
            sheet.Models.Span.Add(index, 2, 1, 2);

            index = AddFarpointRow(sheet);
            sheet.Cells[index, 0].Text = "�Ͻ�������";
            sheet.Cells[index, 1].Tag = EnumColumn.PayBankCardTotCost.ToString();
            sheet.Cells[index, 2].Text = "�Ͻ�֧Ʊ��";
            sheet.Cells[index, 3].Tag = EnumColumn.PayBankTotCost.ToString();

        }
        /// <summary>
        /// ����SheetView��
        /// </summary>
        /// <param name="sheet">sheetView</param>
        /// <returns>SheetView���һ�е�Index</returns>
        private int AddFarpointRow(FarPoint.Win.Spread.SheetView sheet)
        {
            int count = sheet.Rows.Count;
            sheet.Rows.Add(count, 1);
            int index = sheet.Rows.Count-1;
            return index;
        }

        #endregion

        #region ����ToolBar�ؼ���ť
        /// <summary>
        /// ����ToolBar�ؼ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("�ս�", "��ʼ�����ս����ͳ��", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.S�ֶ�¼��, true, false, null);

            toolBarService.AddToolButton("ʱ��", "ѡ���ս�ʱ�䷶Χ", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.S����, true, false, null);

            toolBarService.AddToolButton("����", "�򿪰����ļ�", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);

            toolBarService.AddToolButton("����", "�����ս����ͳ��", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.H�ϲ�, true, false, null);
            #region {05FE6DC0-EE61-4aba-A00D-E57B853B3793}�ս���ܲ���

            //toolBarService.AddToolButton("����", "�����ѻ��ܵ��ս����ͳ��", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.H�ϲ�, true, false, null);

            #endregion
            #region {B8DB7B0D-623A-4643-B3B0-F28FA720CF15}
            toolBarService.AddToolButton("���ܲ���", "�����ѻ��ܵ��ս����ͳ��", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.H�ϲ�, true, false, null);

            #endregion
            toolBarService.AddToolButton("ȷ��", "������ܼ�¼", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            return this.toolBarService;
        }
        #endregion

        #region ѡ��ʱ��
        /// <summary>
        /// ѡ��ʱ��
        /// </summary>
        /// <returns>1�ɹ� ��1ʧ��</returns>
        protected virtual int ChooseDateTime()
        {
            Neusoft.FrameWork.WinForms.Forms.BaseForm f;
            f = new Neusoft.FrameWork.WinForms.Forms.BaseForm();

            ucChooseTime ucTime = new ucChooseTime();
            ucTime.OnEndChooseDateTime += new ucChooseTime.myDelegateGetDateTime(ucTime_OnEndChooseDateTime);

            ucTime.BeginDate = this.DtBegin;
            ucTime.EndDate = this.DtEnd;
            ucTime.IsBeginDateEdit = false;
            ucTime.Dock = System.Windows.Forms.DockStyle.Fill;
            f.Controls.Add(ucTime);

            f.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            f.Size = new Size(295, 200);

            f.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            f.BackColor = this.Parent.BackColor;
            f.Text = "ѡ���ս�ʱ��";
            f.ShowDialog();
            return 1;
        }
        #endregion

        #region ����Tag����Cell,��Text��ֵ
        /// <summary>
        /// ����Tag����Cell,��Text��ֵ
        /// </summary>
        /// <param name="tagValue">Cell�ı�ʶ</param>
        /// <param name="textValue">CellҪ��ʾ��Text</param>
        private void SetCellValue(string tagValue, string textValue)
        {
            FarPoint.Win.Spread.Cell cell = this.neuSpread1_Sheet1.GetCellFromTag(null, tagValue);
            if (cell == null) return;
            cell.Text = textValue;
        }
        /// <summary>
        /// �õ�Cell��Text
        /// </summary>
        /// <param name="tagValue">cell��tag</param>
        /// <returns></returns>
        private string GetCellValue(string tagValue)
        {
            FarPoint.Win.Spread.Cell cell = this.neuSpread1_Sheet1.GetCellFromTag(null, tagValue);
            if (cell == null) return string.Empty;
            return cell.Text;
        }
        #endregion

        #region ִ���ս����
        /// <summary>
        /// ִ���ս����
        /// </summary>
        /// <returns>1�ɹ� ��1ʧ��</returns>
        protected virtual int ExecDayReport()
        {
            try
            {
                this.SetFarPoint(this.neuSpread1_Sheet1);
                SetDayReprotDate();
                string ResultValue = string.Empty;
                string resultAll = string.Empty;//luoff
                string resultWaste = string.Empty;//luoff
                //�跽�ϼ�
                decimal debitSummer = 0m;
                //�����ϼ�
                decimal lenderSummer = 0m;
                //��ʾ��Ŀ��ϸ
                SetDayReportItem();

                #region ҽ��Ԥ�տ�
                //ҽ��Ԥ�տ�跽
                ResultValue = this.feeDayreport.GetBalancedPrepayCostByOperIDAndTime(this.DtBegin, this.DtEnd, this.feeDayreport.Operator.ID);
                this.SetCellValue(EnumColumn.DebitPrePay.ToString(), ResultValue);
                debitSummer += NConvert.ToDecimal(ResultValue);
                //ҽ��Ԥ�տ����
                ResultValue = this.feeDayreport.GetPrepayCostByOperIDAndTime(this.DtBegin, this.DtEnd, this.feeDayreport.Operator.ID);
                this.SetCellValue(EnumColumn.LenderPrePay.ToString(), ResultValue);
                lenderSummer += NConvert.ToDecimal(ResultValue);
                #endregion

                #region ҽ��Ӧ�տ�
                ResultValue = this.feeDayreport.GetLenderPay(this.DtBegin, this.DtEnd, this.feeDayreport.Operator.ID);
                this.SetCellValue(EnumColumn.LenderPay.ToString(), ResultValue);
                lenderSummer += NConvert.ToDecimal(ResultValue);
                #endregion

                #region ���д��
                //�跽���д��
                decimal prepayCheckCost = 0m;
                prepayCheckCost = NConvert.ToDecimal(this.feeDayreport.GetPrepayCheckCostByOperIDAndTime(this.DtBegin, this.DtEnd, this.feeDayreport.Operator.ID));
                decimal supplyCheckCost = 0m;
                supplyCheckCost = NConvert.ToDecimal(this.feeDayreport.GetSupplyCheckCostByOperIDAndTime(this.DtEnd, this.DtEnd, this.feeDayreport.Operator.ID));
                this.SetCellValue(EnumColumn.DebitBank.ToString(), (prepayCheckCost + supplyCheckCost).ToString());
                debitSummer += prepayCheckCost + supplyCheckCost;

                //�������д��
                ResultValue = this.feeDayreport.GetReturnCheckCostByOperIDAndTime(this.DtBegin, this.DtEnd, this.feeDayreport.Operator.ID);
                this.SetCellValue(EnumColumn.LenderBank.ToString(), ResultValue);
                lenderSummer += NConvert.ToDecimal(ResultValue);
                #endregion

                #region �ֽ�
                //Ԥ���ֽ�
                decimal prepayCashCost = 0m;
                prepayCashCost = NConvert.ToDecimal(feeDayreport.GetPrepayCashCostByOperIDAndTime(this.DtBegin, this.DtEnd, this.feeDayreport.Operator.ID));
                //�����ֽ�
                decimal supplyCashCost = 0m;
                supplyCashCost = NConvert.ToDecimal(feeDayreport.GetSupplyCashCostByOperIDAndTime(this.DtBegin, this.DtEnd, this.feeDayreport.Operator.ID));
                //�跽�ֽ�
                this.SetCellValue(EnumColumn.DebitCACost.ToString(), (prepayCashCost + supplyCashCost).ToString());
                debitSummer += prepayCashCost + supplyCashCost;

                //�����ֽ�
                decimal returnCashCost = 0m;
                returnCashCost = NConvert.ToDecimal(feeDayreport.GetReturnCashCostByOperIDAndTime(this.DtBegin, this.DtEnd, this.feeDayreport.Operator.ID));
                this.SetCellValue(EnumColumn.LenderCACost.ToString(), returnCashCost.ToString());
                lenderSummer += returnCashCost;
                #endregion

                #region ���п�
                //�跽
                decimal prepayBankCost = 0m;
                prepayBankCost = NConvert.ToDecimal(feeDayreport.GetPrepayBankCardCost(DtBegin, DtEnd, feeDayreport.Operator.ID));

                decimal supplyBankCost = 0m;
                supplyBankCost = NConvert.ToDecimal(feeDayreport.GetSupplyBankCostByOperIDAndTime(DtBegin, DtEnd, feeDayreport.Operator.ID));

                this.SetCellValue(EnumColumn.DebitBankCard.ToString(), (prepayBankCost + supplyBankCost).ToString());
                debitSummer += prepayBankCost + supplyBankCost;

                //����
                ResultValue = this.feeDayreport.GetReturnBankCostByOperIDAndTime(DtBegin, DtEnd, feeDayreport.Operator.ID);
                this.SetCellValue(EnumColumn.LenderBankCard.ToString(), ResultValue);
                lenderSummer += NConvert.ToDecimal(ResultValue);
                #endregion

                #region Ժ���ʻ�
                decimal YsCost = 0m, SupplyYsCost = 0m, ReturnYsCost = 0m;
                //�跽
                YsCost = NConvert.ToDecimal(this.feeDayreport.GetYSCost(this.DtBegin, this.DtEnd, this.feeDayreport.Operator.ID));
                SupplyYsCost = NConvert.ToDecimal(this.feeDayreport.GetSupplyYSCost(this.DtBegin, this.DtEnd, this.feeDayreport.Operator.ID));
                this.SetCellValue(EnumColumn.DebitYSCost.ToString(), (YsCost + SupplyYsCost).ToString());
                debitSummer += YsCost + SupplyYsCost;

                //����
                ReturnYsCost = NConvert.ToDecimal(this.feeDayreport.GetReturnYSCost(this.DtBegin, this.DtEnd, this.feeDayreport.Operator.ID));
                this.SetCellValue(EnumColumn.LenderYSCost.ToString(),ReturnYsCost.ToString());
                lenderSummer += ReturnYsCost;
                #endregion

                #region ����Ԥ��
                //��
                decimal OtherPrePay = 0m, SupplyOtherPrePay = 0m, ReturnOtherPrePay = 0m;
                OtherPrePay = NConvert.ToDecimal(this.feeDayreport.GetOtherPrepayCost(
                                                this.DtBegin, this.DtEnd, this.feeDayreport.Operator.ID));
                SupplyOtherPrePay = NConvert.ToDecimal(this.feeDayreport.GetSupplyOtherPrePayCost(
                                                this.DtBegin, this.DtEnd, this.feeDayreport.Operator.ID));
                this.SetCellValue(EnumColumn.DebitOtherPrePay.ToString(), (OtherPrePay + SupplyOtherPrePay).ToString());
                debitSummer += OtherPrePay + SupplyOtherPrePay;

                //����
                ReturnOtherPrePay = NConvert.ToDecimal(this.feeDayreport.GetReturnOtherPrePayCost(
                                                 this.DtBegin, this.DtEnd, this.feeDayreport.Operator.ID));
                this.SetCellValue(EnumColumn.LenderOtherPrePay.ToString(), ReturnOtherPrePay.ToString());
                lenderSummer += ReturnOtherPrePay;
                #endregion

                #region ҽ�Ƽ���
                //ҽ�Ƽ���
                ResultValue = this.feeDayreport.GetDayReportDerCost(DtBegin, DtEnd, feeDayreport.Operator.ID);
                this.SetCellValue(EnumColumn.Derate.ToString(), ResultValue);
                debitSummer += NConvert.ToDecimal(ResultValue);
                #endregion

                #region ���Ѽ��ʽ��
                //���Ѽ��ʽ��
                ResultValue = this.feeDayreport.GetBursaryCostByOperIDAndTime(DtBegin, DtEnd, feeDayreport.Operator.ID);
                this.SetCellValue(EnumColumn.BusaryPubCost.ToString(), ResultValue);
                debitSummer += NConvert.ToDecimal(ResultValue);
                #endregion

                this.SetProtectPay(ref debitSummer);

                //�跽�ϼ�
                this.SetCellValue(EnumColumn.DebitSummer.ToString(), debitSummer.ToString());
                //�����ϼ�
                this.SetCellValue(EnumColumn.LenderSummer.ToString(), lenderSummer.ToString());

                //Ԥ��������
                ResultValue = this.feeDayreport.GetValidPrepayInvoiceQtyByOperIDAndTime(DtBegin, dtEnd, feeDayreport.Operator.ID);
                this.SetCellValue(EnumColumn.PrePayInvoiceCount.ToString(), ResultValue);
                resultAll = ResultValue;
                
                //Ԥ������������
                ResultValue = this.feeDayreport.GetWastePrepayInvoiceQtyByOperIDAndTime(DtBegin, dtEnd, feeDayreport.Operator.ID);
                this.SetCellValue(EnumColumn.PrePayWasteInvoiceCount.ToString(), ResultValue);
                resultWaste = ResultValue;

                //Ԥ������Ч����
                int result = Convert.ToInt32(resultAll) - Convert.ToInt32(resultWaste);
                ResultValue = Convert.ToString(result);
                this.SetCellValue(EnumColumn.PrePayUseInvoiceCount.ToString(), ResultValue);//luoff

                ////Ԥ����Ʊ������
                //Neusoft.FrameWork.Models.NeuObject invoiceZone = new Neusoft.FrameWork.Models.NeuObject();

                //invoiceZone = this.feeDayreport.GetPrepayInvoiceZoneByOperIDAndTime(DtBegin, dtEnd, feeDayreport.Operator.ID);

                //if (invoiceZone != null)
                //{
                //    ResultValue = invoiceZone.ID.ToString() + "----" + invoiceZone.Name;
                //    this.SetCellValue(EnumColumn.PrePayInvoiceBound.ToString(), ResultValue);
                //}

                //Ԥ����Ʊ������  luoff
                DataSet ds = new DataSet();
                int resultValue = feeDayreport.GetPrepayInvoiceZoneNew(this.dtBegin, this.dtEnd, this.feeDayreport.Operator.ID, ref ds);
                if (resultValue == -1) return -1;
                DataTable table = ds.Tables[0];

                //if (table.Rows.Count == 0) return -1;{89969B93-7B2A-427b-9363-78C1E16D87F3}
                if (table.Rows.Count != 0)
                {

                    this.SetCellValue(EnumColumn.PrePayInvoiceBound.ToString(), GetPrepayOrBalanceInvoiceZone(table));
                }
               

                //Ԥ��������Ʊ��
                ArrayList alWasteNO = new ArrayList();
                alWasteNO = this.feeDayreport.QueryWastePrepayInvNOByOperIDAndTime(DtBegin, dtEnd, feeDayreport.Operator.ID);
                string wasteInvNO = "";
                Neusoft.HISFC.Models.Fee.Inpatient.Prepay prepay = new Neusoft.HISFC.Models.Fee.Inpatient.Prepay();
                if (alWasteNO.Count == 0)
                {
                    this.SetCellValue(EnumColumn.PrePayWasteInvoiceNo.ToString(), string.Empty);
                }
                else
                {
                    for (int i = 0; i < alWasteNO.Count; i++)
                    {
                        prepay = (Neusoft.HISFC.Models.Fee.Inpatient.Prepay)alWasteNO[i];
                        wasteInvNO = wasteInvNO + prepay.RecipeNO + "|";
                    }
                    this.SetCellValue(EnumColumn.PrePayWasteInvoiceNo.ToString(), wasteInvNO.Substring(0, wasteInvNO.Length - 1));
                }


                //����Ʊ������
                ResultValue = this.feeDayreport.GetValidBalanceInvoiceQtyByOperIDAndTime(DtBegin, DtEnd, feeDayreport.Operator.ID);
                this.SetCellValue(EnumColumn.BalanceInvoiceCount.ToString(), ResultValue);
                resultAll = ResultValue;

                //������������
                ResultValue = this.feeDayreport.GetWasteBalanceInvoiceQtyByOperIDAndTime(DtBegin, DtEnd, feeDayreport.Operator.ID);
                this.SetCellValue(EnumColumn.BalanceWasteInvoiceCount.ToString(), ResultValue);
                resultWaste = ResultValue;

                //����Ʊ����Ч����
                result = Convert.ToInt32(resultAll) - Convert.ToInt32(resultWaste);
                ResultValue = Convert.ToString(result);
                this.SetCellValue(EnumColumn.BalanceUseInvoiceCount.ToString(), ResultValue);

                ////����Ʊ������
                //Neusoft.FrameWork.Models.NeuObject balanceInvoiceZone = new Neusoft.FrameWork.Models.NeuObject();
                //balanceInvoiceZone = this.feeDayreport.GetBalanceInvoiceZoneByOperIDAndTime(DtBegin, DtEnd, feeDayreport.Operator.ID);
                //if (balanceInvoiceZone != null)
                //{
                //    this.SetCellValue(EnumColumn.BalanceInvoiceBound.ToString(), balanceInvoiceZone.ID.ToString() + "----" + balanceInvoiceZone.Name);
                //}

                #region ����Ʊ������ luoff
                DataSet balanceDS = new DataSet();
                int balanceValue = feeDayreport.GetBalanceInvoiceZoneNew(this.DtBegin, this.DtEnd, this.feeDayreport.Operator.ID, ref balanceDS);
                if (balanceValue == -1)
                {
                    return -1;
                }
                DataTable balanceTable = balanceDS.Tables[0];
                //if (balanceTable.Rows.Count == 0){89969B93-7B2A-427b-9363-78C1E16D87F3}
                //{
                //    return -1;
                //}
                if (balanceTable.Rows.Count != 0)
                {
                    this.SetCellValue(EnumColumn.BalanceInvoiceBound.ToString(), GetPrepayOrBalanceInvoiceZone(balanceTable));
                }
                #endregion

                //��������Ʊ��
                ArrayList alWasteBalanceInvNO = new ArrayList();
                alWasteBalanceInvNO = this.feeDayreport.QueryWasteBalanceInvNOByOperIDAndTime(DtBegin, DtEnd, feeDayreport.Operator.ID);
                string balanceWasteNO = "";
                Neusoft.HISFC.Models.Fee.Inpatient.Balance balance = new Neusoft.HISFC.Models.Fee.Inpatient.Balance();

                if (alWasteBalanceInvNO.Count == 0)
                {
                    this.SetCellValue(EnumColumn.BalanceWasteInvoiceNo.ToString(), string.Empty);
                }
                else
                {
                    for (int j = 0; j < alWasteBalanceInvNO.Count; j++)
                    {
                        balance = (Neusoft.HISFC.Models.Fee.Inpatient.Balance)alWasteBalanceInvNO[j];
                        balanceWasteNO = balanceWasteNO + balance.Invoice.ID + "|";
                    }
                    this.SetCellValue(EnumColumn.BalanceWasteInvoiceNo.ToString(), balanceWasteNO.Substring(0, balanceWasteNO.Length - 1));
                }
                SetPayTotCostInfo();
                this.OperMode = "1";
                return 1;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        #region ���Ԥ����\����Ʊ������  luoff

        private string GetPrepayOrBalanceInvoiceZone(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            int count = dt.Rows.Count - 1;
            string minStr = dt.Rows[0][0].ToString();
            string maxStr = dt.Rows[0][0].ToString();
            for (int i = 0; i < count - 1; i++)
                for (int j = i + 1; j < count; j++)
                {
                    long froInt;
                    long nxtInt;
                    try
                    {
                        froInt = Convert.ToInt32(dt.Rows[i][0].ToString());
                        nxtInt = Convert.ToInt32(dt.Rows[j][0].ToString());
                    }
                    catch (Exception ex)
                    {

                        break;
                    }
                    long chaInt = nxtInt - froInt;
                    if (chaInt > 1)
                    {
                        maxStr = dt.Rows[i][0].ToString();
                        if (maxStr.Equals(minStr))
                        {
                            sb.Append(minStr + ",");
                        }
                        else
                        {
                            sb.Append(minStr + "��" + maxStr + ",");
                        }
                        minStr = dt.Rows[j][0].ToString();
                        break;
                    }
                    else
                    {
                        break;
                    }

                }
            maxStr = dt.Rows[count][0].ToString();
            sb.Append(minStr + "��" + maxStr);
            return sb.ToString();

        }

        #endregion

      


        /// <summary>
        /// ��ʾ��Ŀ��ϸ
        /// </summary>
        private void SetDayReportItem()
        {
           // ds = this.feeDayreport.GetDayReportItem(this.DtBegin, this.DtEnd, this.feeDayreport.Operator.ID);//ԭ��ȡͳ�ƴ�����Ŀ��ϸ
            ds = this.feeDayreport.GetDayReportItemZYRJ(this.DtBegin, this.DtEnd, this.feeDayreport.Operator.ID);
            if (ds == null)
            {
                return;
            }
            
            try
            {
                DataTable dt = ds.Tables[0];
                int Index = 0;
                int iMod = 0;
                decimal TotCost = 0m;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Index = Convert.ToInt32(i / 2);
                    iMod = i % 2;
                    if (iMod == 0)
                    {
                        this.neuSpread1_Sheet1.Rows.Add(Index, 1);
                        this.neuSpread1_Sheet1.Cells[Index, 0].Tag = dt.Rows[i][0].ToString();
                        this.neuSpread1_Sheet1.Cells[Index, 0].Text = dt.Rows[i][1].ToString();
                        this.neuSpread1_Sheet1.Cells[Index, 1].Text = dt.Rows[i][2].ToString();
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[Index, 2].Tag = dt.Rows[i][0].ToString();
                        this.neuSpread1_Sheet1.Cells[Index, 2].Text = dt.Rows[i][1].ToString();
                        this.neuSpread1_Sheet1.Cells[Index, 3].Text = dt.Rows[i][2].ToString();
                    }
                    TotCost += NConvert.ToDecimal(dt.Rows[i][2]);
                    #region ������Ŀʵ��
                    item = new Report.InpatientFee.Class.Item();
                    item.StateCode = dt.Rows[i][0].ToString();
                    item.TotCost = NConvert.ToDecimal(dt.Rows[i][2]);
                    item.Mark = dt.Rows[i][1].ToString();
                    dayReport.ItemList.Add(item);
                    #endregion
                }
                //�ϼ�
                this.neuSpread1_Sheet1.Cells[Index + 1, 1].Text = TotCost.ToString();
            }
            catch { }
            finally
            {
                ds.Dispose();
            }
        }
        /// <summary>
        /// ��ʾʡ����ҽ��֧������
        /// </summary>
        /// <param name="DebitSummer">�ϼ�����</param>
        private void SetProtectPay(ref decimal DebitSummer)
        {
            ds = this.feeDayreport.GetDayReportProtectPay(this.DtBegin, this.DtEnd, this.feeDayreport.Operator.ID);
            if (ds == null) return;
            DataTable dt = ds.Tables[0];
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    switch (dr["pact_code"].ToString())
                    {
                        case "2":
                            {
                                this.SetCellValue(EnumColumn.CmedicarePayCost.ToString(), dr["pay_cost"].ToString());
                                DebitSummer += NConvert.ToDecimal(dr["pay_cost"]);

                                this.SetCellValue(EnumColumn.CmedicarePubCost.ToString(), dr["pub_cost"].ToString());
                                DebitSummer += NConvert.ToDecimal(dr["pub_cost"]);

                                this.SetCellValue(EnumColumn.CmedicareOverCost.ToString(), dr["over_cost"].ToString());
                                DebitSummer += NConvert.ToDecimal(dr["over_cost"]);
                                break;
                            }
                        case "3":
                            {
                                this.SetCellValue(EnumColumn.PmedicarePayCost.ToString(), dr["pay_cost"].ToString());
                                DebitSummer += NConvert.ToDecimal(dr["pay_cost"]);

                                this.SetCellValue(EnumColumn.PmedicarePubCost.ToString(), dr["pub_cost"].ToString());
                                DebitSummer += NConvert.ToDecimal(dr["pub_cost"]);

                                this.SetCellValue(EnumColumn.PmedicareOverCost.ToString(), dr["over_cost"].ToString());
                                DebitSummer += NConvert.ToDecimal(dr["over_cost"]);

                                this.SetCellValue(EnumColumn.PmedicareOfficialCost.ToString(), dr["official_cost"].ToString());
                                DebitSummer += NConvert.ToDecimal(dr["official_cost"]);
                                break;
                            }
                    }
                }
            }
            catch { }
            finally
            {
                ds.Dispose();
            }

        }
        /// <summary>
        /// ��ʾ�Ͻ�����
        /// </summary>
        private void SetPayTotCostInfo()
        {
            decimal resultValue = 0m;
            //�Ͻ��ֽ�=�ֽ�跽������
            resultValue = NConvert.ToDecimal(this.GetCellValue(EnumColumn.DebitCACost.ToString()))
                        -NConvert.ToDecimal(this.GetCellValue(EnumColumn.LenderCACost.ToString()));
            this.SetCellValue(EnumColumn.PayTotCost.ToString(), resultValue.ToString());
            //��д
            this.SetCellValue(EnumColumn.PayTotCostCapital.ToString(),"��д��"+ NConvert.ToCapital(resultValue));
            //�Ͻ�������=���п��跽������
            resultValue = NConvert.ToDecimal(this.GetCellValue(EnumColumn.DebitBankCard.ToString()))
                        - NConvert.ToDecimal(this.GetCellValue(EnumColumn.LenderBankCard.ToString()));
            this.SetCellValue(EnumColumn.PayBankCardTotCost.ToString(),resultValue.ToString());
            //�Ͻ�֧Ʊ����д��跽������
            resultValue = NConvert.ToDecimal(this.GetCellValue(EnumColumn.DebitBank.ToString()))
                        - NConvert.ToDecimal(this.GetCellValue(EnumColumn.LenderBank.ToString()));
            this.SetCellValue(EnumColumn.PayBankTotCost.ToString(),resultValue.ToString());
        }
        /// <summary>
        /// ����ս�ʵ��
        /// </summary>
        private void GetDayReprot()
        {
            string resultValue=string.Empty;
            //ͳ�����
            dayReport.StatNO = this.feeDayreport.GetNewDayReportID();

            //��ʼʱ��

            dayReport.BeginDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.DtBegin);
            //��������
            dayReport.EndDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.DtEnd);

            //����Ա����

            dayReport.Oper.ID = this.feeDayreport.Operator.ID;
            //ͳ��ʱ��
            dayReport.Oper.OperTime = this.feeDayreport.GetDateTimeFromSysDateTime();

            #region ҽ��Ԥ�տ�
            //��
            resultValue=this.GetCellValue(EnumColumn.DebitPrePay.ToString());
            dayReport.BalancePrepayCost = NConvert.ToDecimal(resultValue);
            //��
            resultValue = this.GetCellValue(EnumColumn.LenderPrePay.ToString());
            dayReport.PrepayCost = NConvert.ToDecimal(resultValue);
            #endregion

            #region ҽ��Ӧ�տ�
            resultValue = this.GetCellValue(EnumColumn.LenderPay.ToString());
            dayReport.BalanceCost = NConvert.ToDecimal(resultValue);
            #endregion

            #region ���д��
            //��
            resultValue = this.GetCellValue(EnumColumn.DebitBank.ToString());
            dayReport.DebitCheckCost = NConvert.ToDecimal(resultValue);
            //��
            resultValue = this.GetCellValue(EnumColumn.LenderBank.ToString());
            dayReport.LenderCheckCost = NConvert.ToDecimal(resultValue);
            #endregion

            #region �ֽ�
            //��
            resultValue = this.GetCellValue(EnumColumn.DebitCACost.ToString());
            dayReport.DebitCash = NConvert.ToDecimal(resultValue);
            //��
            resultValue = this.GetCellValue(EnumColumn.LenderCACost.ToString());
            dayReport.LenderCash = NConvert.ToDecimal(resultValue);
            #endregion

            #region ���п�
            //��
            resultValue = this.GetCellValue(EnumColumn.DebitBankCard.ToString());
            dayReport.DebitBankCost = NConvert.ToDecimal(resultValue);
            //��
            resultValue = this.GetCellValue(EnumColumn.LenderBankCard.ToString());
            dayReport.LenderBankCost = NConvert.ToDecimal(resultValue);
            #endregion

            #region Ժ���ʻ�
            //��
            resultValue = this.GetCellValue(EnumColumn.DebitYSCost.ToString());
            dayReport.DebitHos = NConvert.ToDecimal(resultValue);
            //��
            resultValue = this.GetCellValue(EnumColumn.LenderYSCost.ToString());
            dayReport.LenderHos = NConvert.ToDecimal(resultValue);
            #endregion

            #region ����Ԥ��
            //��
            resultValue = this.GetCellValue(EnumColumn.DebitOtherPrePay.ToString());
            dayReport.DebitOther = NConvert.ToDecimal(resultValue);
            //��
            resultValue = this.GetCellValue(EnumColumn.LenderOtherPrePay.ToString());
            dayReport.LenderOther = NConvert.ToDecimal(resultValue);
            #endregion

            #region ҽ�Ƽ���
            resultValue = this.GetCellValue(EnumColumn.Derate.ToString());
            dayReport.DerateCost = NConvert.ToDecimal(resultValue);
            #endregion 

            #region ����ҽ�Ƽ���
            resultValue = this.GetCellValue(EnumColumn.BusaryPubCost.ToString());
            dayReport.BursaryPubCost = NConvert.ToDecimal(resultValue);
            #endregion

            #region �б��˻�
            resultValue = this.GetCellValue(EnumColumn.CmedicarePayCost.ToString());
            dayReport.CityMedicarePayCost = NConvert.ToDecimal(resultValue);
            #endregion

            #region �б�ͳ��
            resultValue = this.GetCellValue(EnumColumn.CmedicarePubCost.ToString());
            dayReport.CityMedicarePubCost = NConvert.ToDecimal(resultValue);
            #endregion

            #region �б����
            resultValue = this.GetCellValue(EnumColumn.CmedicareOverCost.ToString());
            dayReport.CityMedicareOverCost = NConvert.ToDecimal(resultValue);
            #endregion

            #region ʡ���˻�
            resultValue = this.GetCellValue(EnumColumn.PmedicarePayCost.ToString());
            dayReport.ProvinceMedicarePayCost = NConvert.ToDecimal(resultValue);
            #endregion

            #region ʡ��ͳ��
            resultValue = this.GetCellValue(EnumColumn.PmedicarePubCost.ToString());
            dayReport.ProvinceMedicarePubCost = NConvert.ToDecimal(resultValue);
            #endregion

            #region ʡ�����
            resultValue = this.GetCellValue(EnumColumn.PmedicareOverCost.ToString());
            dayReport.ProvinceMedicarePayCost = NConvert.ToDecimal(resultValue);
            #endregion

            #region ʡ������Ա
            resultValue = this.GetCellValue(EnumColumn.PmedicareOfficialCost.ToString());
            dayReport.ProvinceMedicareOfficeCost = NConvert.ToDecimal(resultValue);
            #endregion

            #region Ԥ����Ʊ�ݺŷ�Χ
            resultValue = this.GetCellValue(EnumColumn.PrePayInvoiceBound.ToString());
            dayReport.PrepayInvZone = resultValue;
            #endregion

            #region Ԥ��������
            resultValue = this.GetCellValue(EnumColumn.PrePayInvoiceCount.ToString());
            dayReport.PrepayInvCount = NConvert.ToInt32(resultValue);
            #endregion

            #region Ԥ������������
            resultValue = this.GetCellValue(EnumColumn.PrePayWasteInvoiceCount.ToString());
            dayReport.PrepayWasteInvCount = NConvert.ToInt32(resultValue);
            #endregion

            #region Ԥ��������Ʊ�ݺ�
            resultValue = this.GetCellValue(EnumColumn.PrePayWasteInvoiceNo.ToString());
            dayReport.PrepayWasteInvNO = resultValue;
            #endregion

            #region ����Ʊ�ݺŷ�Χ
            resultValue = this.GetCellValue(EnumColumn.BalanceInvoiceBound.ToString());
            dayReport.BalanceInvZone = resultValue;
            #endregion

            #region ����Ʊ������
            resultValue = this.GetCellValue(EnumColumn.BalanceInvoiceCount.ToString());
            dayReport.BalanceInvCount = NConvert.ToInt32(resultValue);
            #endregion

            #region ����Ʊ����������
            resultValue = this.GetCellValue(EnumColumn.BalanceWasteInvoiceCount.ToString());
            dayReport.BalanceWasteInvCount = NConvert.ToInt32(resultValue);
            #endregion

            #region ��������Ʊ�ݺ�
            resultValue = this.GetCellValue(EnumColumn.BalanceWasteInvoiceNo.ToString());
            dayReport.BalanceWasteInvNO = resultValue;
            #endregion
        }
        #endregion

        #region ��ʾ��ѯ�ս�����
        /// <summary>
        /// 
        /// </summary>
        private void SetDayReport()
        {
            this.DtBegin = this.dayReport.BeginDate;
            this.DtEnd = this.dayReport.EndDate;
            ShowDayReportDetail(dayReport.ItemList);
            ShwoDayReport();
            SetPayTotCostInfo();
            
        }
        private void ShowDayReportDetail(List<Class.Item> list)
        {
            int Index = 0;
            int iMod = 0;
            decimal TotCost = 0m;
            for (int i = 0; i < list.Count; i++)
            {
                Index = Convert.ToInt32(i / 2);
                iMod = i % 2;
                if (iMod == 0)
                {
                    this.neuSpread1_Sheet1.Rows.Add(Index, 1);
                    this.neuSpread1_Sheet1.Cells[Index, 0].Tag = list[i].StateCode;
                    this.neuSpread1_Sheet1.Cells[Index, 0].Text = list[i].Mark;
                    this.neuSpread1_Sheet1.Cells[Index, 1].Text = list[i].TotCost.ToString();
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[Index, 2].Tag = list[i].StateCode;
                    this.neuSpread1_Sheet1.Cells[Index, 2].Text = list[i].Mark;
                    this.neuSpread1_Sheet1.Cells[Index, 3].Text = list[i].TotCost.ToString();
                }
                TotCost += list[i].TotCost;
            }
            //�ϼ�
            this.neuSpread1_Sheet1.Cells[Index + 1, 1].Text = TotCost.ToString();
        }

        private void ShwoDayReport()
        {
            decimal debitSummer = 0m, lenderSummer = 0m;
            #region ҽ��Ԥ�տ�
            //��
            this.SetCellValue(EnumColumn.DebitPrePay.ToString(), dayReport.BalancePrepayCost.ToString());
            debitSummer += dayReport.BalancePrepayCost;
            //��
            this.SetCellValue(EnumColumn.LenderPrePay.ToString(), dayReport.PrepayCost.ToString());
            lenderSummer += dayReport.PrepayCost;
            #endregion

            #region ҽ��Ӧ�տ�
            this.SetCellValue(EnumColumn.LenderPay.ToString(), dayReport.BalanceCost.ToString());
            lenderSummer += dayReport.BalanceCost;
            #endregion

            #region ���д��
            //��
            this.SetCellValue(EnumColumn.DebitBank.ToString(), dayReport.DebitCheckCost.ToString());
            debitSummer += dayReport.DebitCheckCost;
            //��
            this.SetCellValue(EnumColumn.LenderBank.ToString(), dayReport.LenderCheckCost.ToString());
            lenderSummer += dayReport.LenderCheckCost;
            #endregion

            #region �ֽ�
            //��
            this.SetCellValue(EnumColumn.DebitCACost.ToString(), dayReport.DebitCash.ToString());
            debitSummer += dayReport.DebitCash;
            //��
            this.SetCellValue(EnumColumn.LenderCACost.ToString(), dayReport.LenderCash.ToString());
            lenderSummer += dayReport.LenderCash;
            #endregion

            #region ���п�
            //��
            this.SetCellValue(EnumColumn.DebitBankCard.ToString(), dayReport.DebitBankCost.ToString());
            debitSummer += dayReport.DebitBankCost;
            //��
            this.SetCellValue(EnumColumn.LenderBankCard.ToString(), dayReport.LenderBankCost.ToString());
            lenderSummer += dayReport.LenderBankCost;
            #endregion

            #region Ժ���ʻ�
            //��
            this.SetCellValue(EnumColumn.DebitYSCost.ToString(), dayReport.DebitHos.ToString());
            debitSummer += dayReport.DebitHos;
            //��
            this.SetCellValue(EnumColumn.LenderYSCost.ToString(), dayReport.LenderHos.ToString());
            lenderSummer += dayReport.LenderHos;
            #endregion

            #region ����Ԥ��
            //��
            this.SetCellValue(EnumColumn.DebitOtherPrePay.ToString(), dayReport.DebitOther.ToString());
            debitSummer += dayReport.DebitOther;
            //��
            this.SetCellValue(EnumColumn.LenderOtherPrePay.ToString(), dayReport.LenderOther.ToString());
            lenderSummer += dayReport.LenderOther;
            #endregion

            // ҽ�Ƽ���
            this.SetCellValue(EnumColumn.Derate.ToString(), dayReport.DerateCost.ToString());
            debitSummer += dayReport.DerateCost;

            // ����ҽ�Ƽ���
            this.SetCellValue(EnumColumn.BusaryPubCost.ToString(), dayReport.BursaryPubCost.ToString());
            debitSummer += dayReport.BursaryPubCost;

            // �б��˻�
            this.SetCellValue(EnumColumn.CmedicarePayCost.ToString(), dayReport.CityMedicarePayCost.ToString());
            debitSummer += dayReport.CityMedicarePayCost;

            // �б�ͳ��
            this.SetCellValue(EnumColumn.CmedicarePubCost.ToString(), dayReport.CityMedicarePubCost.ToString());
            debitSummer += dayReport.CityMedicarePubCost;

            // �б����
            this.SetCellValue(EnumColumn.CmedicareOverCost.ToString(), dayReport.CityMedicareOverCost.ToString());
            debitSummer += dayReport.CityMedicareOverCost;

            // ʡ���˻�
            this.SetCellValue(EnumColumn.PmedicarePayCost.ToString(), dayReport.ProvinceMedicarePayCost.ToString());
            debitSummer += dayReport.ProvinceMedicarePayCost;
            // ʡ��ͳ��
            this.SetCellValue(EnumColumn.PmedicarePubCost.ToString(), dayReport.ProvinceMedicarePubCost.ToString());
            debitSummer += dayReport.ProvinceMedicarePubCost;

            // ʡ�����
            this.SetCellValue(EnumColumn.PmedicareOverCost.ToString(), dayReport.ProvinceMedicarePayCost.ToString());
            debitSummer += dayReport.ProvinceMedicarePayCost;
            
            // ʡ������Ա
            this.SetCellValue(EnumColumn.PmedicareOfficialCost.ToString(), dayReport.ProvinceMedicareOfficeCost.ToString());
            debitSummer += dayReport.ProvinceMedicareOfficeCost;

            //�ϼ�
            //�跽�ϼ�
            this.SetCellValue(EnumColumn.DebitSummer.ToString(), debitSummer.ToString());
            //�����ϼ�
            this.SetCellValue(EnumColumn.LenderSummer.ToString(), lenderSummer.ToString());

            // Ԥ����Ʊ�ݺŷ�Χ
            this.SetCellValue(EnumColumn.PrePayInvoiceBound.ToString(), dayReport.PrepayInvZone);

            #region Ԥ��������
            this.SetCellValue(EnumColumn.PrePayInvoiceCount.ToString(), dayReport.PrepayInvCount.ToString());
            #endregion

            // Ԥ������������
            this.SetCellValue(EnumColumn.PrePayWasteInvoiceCount.ToString(), dayReport.PrepayWasteInvCount.ToString());

           // Ԥ��������Ʊ�ݺ�
            this.SetCellValue(EnumColumn.PrePayWasteInvoiceNo.ToString(), dayReport.PrepayWasteInvNO);

            // ����Ʊ�ݺŷ�Χ
            this.SetCellValue(EnumColumn.BalanceInvoiceBound.ToString(), dayReport.BalanceInvZone);

            // ����Ʊ������
            this.SetCellValue(EnumColumn.BalanceInvoiceCount.ToString(), dayReport.BalanceInvCount.ToString());

            // ����Ʊ����������
            this.SetCellValue(EnumColumn.BalanceWasteInvoiceCount.ToString(), dayReport.BalanceWasteInvCount.ToString());
           
            // ��������Ʊ�ݺ�
            this.SetCellValue(EnumColumn.BalanceWasteInvoiceNo.ToString(), dayReport.BalanceWasteInvNO);
           
        }
        
        #endregion

        #region �����ս�����
        protected virtual void Save()
        {
            if (this.OperMode != "1") return;
            //if (this.dayReport.ItemList.Count == 0)
            //{
            //    MessageBox.Show("��ʱ���û�з������ã������սᣡ");
            //    return;
            //}
            DialogResult diaResult = MessageBox.Show("�Ƿ�����ս�,�ս�����ݽ����ָܻ�?", "סԺ�տ�Ա�ɿ��ձ�", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (diaResult == DialogResult.No) return;
            //�����ս�ʵ��
            this.GetDayReprot();
            if (string.IsNullOrEmpty(dayReport.PrepayInvZone) && string.IsNullOrEmpty(dayReport.PrepayWasteInvNO) && string.IsNullOrEmpty(dayReport.BalanceInvZone) && string.IsNullOrEmpty(dayReport.BalanceWasteInvNO))
            {
                MessageBox.Show("�������ս����ݣ�");
                return;
            }
            //����
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            //Neusoft.FrameWork.Management.Transaction trans = new Neusoft.FrameWork.Management.Transaction(Neusoft.FrameWork.Management.Connection.Instance);
            //trans.BeginTransaction();

            feeDayreport.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            int result = 0;
            //������ϸ
            result = feeDayreport.InsetDayReportDetail(dayReport);
            if (result == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(feeDayreport.Err);
                return;
            }
            //�����սᱨ��
            result = feeDayreport.InsertDayReport(dayReport);
            if (result == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(feeDayreport.Err);
                return;
            }
            //{9B8D83F8-CB0F-48fb-8ECD-0BA4462A952A}
            //���½�����Ϣ
            result = feeDayreport.UpdateDayReport(dayReport.StatNO, dayReport.Oper.ID, dayReport.Oper.OperTime, dtBegin, dtEnd);
            if(result <0)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(feeDayreport.Err);
                return;
            }
            Neusoft.FrameWork.Management.PublicTrans.Commit();
            dayReport.ItemList.Clear();
            MessageBox.Show("����ɹ���");
            this.OperMode = "2";
        }
        #endregion

        #region ��ѯ����
        private void QueryOrCollectData()
        {
            ucPopDayReportQueryNew ucQuery = new ucPopDayReportQueryNew(OperationType);
            DialogResult result = Neusoft.FrameWork.WinForms.Classes.Function.PopShowControl(ucQuery);
            if (result != DialogResult.OK) return ;
            this.dayReport = ucQuery.DayReprot;
            SetDayReport();
            if (this.OperationType == OperType.CollectDayReport)
            {
                listEnviroment = ucQuery.CollectEnvironment;
            }
        }
        #endregion

        #region  {05FE6DC0-EE61-4aba-A00D-E57B853B3793}�ս���ܲ���
        private void QueryOrCollectDataCheck()
        {
            //#region {B8DB7B0D-623A-4643-B3B0-F28FA720CF15}
            //if (this.operType == OperType.DayReport)
            //{
            //    Neusoft.FrameWork.WinForms.Classes.Function.MessageBox("�ù��������ս���ܲ������ս�ģʽ��Ч");
            //    return;
            //} 
            //#endregion
            ucPopDayReportQueryNew ucQuery = new ucPopDayReportQueryNew(OperationType);
            ucQuery.ckRePrint.Checked = true;
            DialogResult result = Neusoft.FrameWork.WinForms.Classes.Function.PopShowControl(ucQuery);
            if (result != DialogResult.OK) return;
            this.dayReport = ucQuery.DayReprot;
            SetDayReport();
            if (this.OperationType == OperType.CollectDayReport)
            {
                listEnviroment = ucQuery.CollectEnvironment;
            }
        } 
        #endregion
        #region �����ս��������
        /// <summary>
        /// �����ս��������
        /// </summary>
        public void SaveCollectData()
        {
            if (listEnviroment == null || listEnviroment.Count == 0)
            {
                MessageBox.Show("���Ȼ�������Ȼ���ٱ������ݣ�");
                return;
            }
            DialogResult result = MessageBox.Show("ȷ��Ҫ����������ݣ�", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
            //����Ա����
            string operID = this.feeDayreport.Operator.ID;
            //����ʱ��
            DateTime OperDate = this.feeDayreport.GetDateTimeFromSysDateTime();

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            //Neusoft.FrameWork.Management.Transaction trans = new Neusoft.FrameWork.Management.Transaction(Neusoft.FrameWork.Management.Connection.Instance);
            //trans.BeginTransaction();

            this.feeDayreport.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            foreach (Class.DayReport obj in listEnviroment)
            {
                if (feeDayreport.SaveCollectData(operID, OperDate, obj.StatNO) == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�����������ʧ�ܣ�");
                }
            }

            Neusoft.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("����������ݳɹ���");
        }
        #endregion
        #endregion

        #region �¼�
        private void ucDayReportNew_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        void ucTime_OnEndChooseDateTime(DateTime beginTime, DateTime endTime)
        {
            this.DtBegin = beginTime;
            this.DtEnd = endTime;
            //ִ���ս�
            this.ExecDayReport();
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "�ս�":
                    {
                        this.ExecDayReport();
                        break;
                    }
                case "ʱ��":
                    {
                        ChooseDateTime();
                        break;
                    }
                case "����":
                    {
                        QueryOrCollectData();
                        break;
                    }
                #region {05FE6DC0-EE61-4aba-A00D-E57B853B3793}�ս���ܲ���
                //case "����":
                //    {
                //        QueryOrCollectDataCheck();
                //        break;
                //    }
                        #endregion
                #region {B8DB7B0D-623A-4643-B3B0-F28FA720CF15}
                case "���ܲ���":
                    {
                        QueryOrCollectDataCheck();
                        break;
                    }
                #endregion
        case "ȷ��":
                    {
                        this.SaveCollectData();
                        break;
                    }
                case "����":
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (this.OperationType != OperType.DayReport) return -1;
            this.Save();
            return base.OnSave(sender, neuObject);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            QueryOrCollectData();
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            print.PrintPage(0, 0, this);
            return base.OnPrint(sender, neuObject);
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            object tag = neuSpread1_Sheet1.ActiveCell.Tag;
            if (tag == null) return;
            if (neuSpread1_Sheet1.ActiveCell.Text.Trim() == string.Empty) return;
            if (operType == OperType.DayReport)
            {
                if (tag.ToString() == EnumColumn.LenderPrePay.ToString())
                {
                    ucDayReportDetail c = new ucDayReportDetail(this.DtBegin, this.DtEnd, operType);
                    c.FrmTitle = "����ҽ��Ԥ�տ���ϸ";
                    c.aMod = 1;
                    Neusoft.FrameWork.WinForms.Classes.Function.ShowControl(c);
                }
                if (tag.ToString() == EnumColumn.DebitPrePay.ToString())
                {
                    ucDayReportDetail c = new ucDayReportDetail(this.DtBegin, this.DtEnd, operType);
                    c.FrmTitle = "�跽ҽ��Ԥ�տ���ϸ";
                    c.aMod = 0;
                    Neusoft.FrameWork.WinForms.Classes.Function.ShowControl(c);
                }
                if (tag.ToString() == EnumColumn.LenderPay.ToString())
                {
                    ucDayReportDetail c = new ucDayReportDetail(this.DtBegin, this.DtEnd, operType);
                    c.FrmTitle = "ҽ��Ӧ�տ���ϸ";
                    c.aMod = 2;
                    Neusoft.FrameWork.WinForms.Classes.Function.ShowControl(c);
                }
            }
            //����
            else
            {
                if (tag.ToString() == EnumColumn.LenderPrePay.ToString())
                {
                    ucDayReportDetail c = new ucDayReportDetail(listEnviroment, operType);
                    c.FrmTitle = "����ҽ��Ԥ�տ���ϸ";
                    c.aMod = 1;
                    Neusoft.FrameWork.WinForms.Classes.Function.ShowControl(c);
                }
                if (tag.ToString() == EnumColumn.DebitPrePay.ToString())
                {
                    ucDayReportDetail c = new ucDayReportDetail(listEnviroment, operType);
                    c.FrmTitle = "�跽ҽ��Ԥ�տ���ϸ";
                    c.aMod = 0;
                    Neusoft.FrameWork.WinForms.Classes.Function.ShowControl(c);
                }
                if (tag.ToString() == EnumColumn.LenderPay.ToString())
                {
                    ucDayReportDetail c = new ucDayReportDetail(listEnviroment, operType);
                    c.FrmTitle = "ҽ��Ӧ�տ���ϸ";
                    c.aMod = 2;
                    Neusoft.FrameWork.WinForms.Classes.Function.ShowControl(c);
                }

            }
        }
        #endregion
        
    }
    /// <summary>
    /// �����������սᡢ��ѯ���ǻ���
    /// </summary>
    public enum OperType
    {
        /// <summary>
        /// �ս�Ͳ�ѯ
        /// </summary>
        DayReport = 0,
        /// <summary>
        /// ����
        /// </summary>
        CollectDayReport = 1,
    }
}
