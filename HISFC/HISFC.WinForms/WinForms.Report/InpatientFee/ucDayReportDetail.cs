using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Neusoft.WinForms.Report.InpatientFee
{
    public partial class ucDayReportDetail : UserControl
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="dtbegin">��ʼʱ��</param>
        /// <param name="dtend">��ֹʱ��</param>
        ///<param name="operType">��������</param>
        public ucDayReportDetail(DateTime dtbegin,DateTime dtend,OperType operType)
        {
            InitializeComponent();
            dtBegin = dtbegin;
            dtEnd = dtend;
            operationType = operType;
        }
        /// <summary>
        /// ���캯��
        /// </summary>
       ///<param name="list">�����ս�ʱ��κͲ���Ա</param>
        ///<param name="operType">��������</param>
        public ucDayReportDetail(List<Class.DayReport> arrylist, OperType operType)
        {
            InitializeComponent();
            list = arrylist;
            operationType = operType;
            this.panelDayReport.Visible = false;
        }

        #region ����
        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime dtBegin = DateTime.MinValue;
        /// <summary>
        /// ��ֹʱ��
        /// </summary>
        private DateTime dtEnd = DateTime.MinValue;
        /// <summary>
        /// �������
        /// </summary>
        private string frmTitle = string.Empty;
        private List<Class.DayReport> list = null; 
        /// <summary>
        /// ��ϸ���
        /// </summary>
        private int amod=0;
        /// <summary>
        /// ��������
        /// </summary>
        private OperType operationType;
        /// <summary>
        /// �ս�ҵ���
        /// </summary>
        Functions.DayReport feeDayReport = new Report.InpatientFee.Functions.DayReport();
        #endregion

        #region ����
        /// <summary>
        /// �������
        /// </summary>
        public string FrmTitle
        {
            set
            {
                this.frmTitle = value;
            }
        }
        /// <summary>
        /// ��ѯ���
        /// </summary>
        public int aMod
        {
            set
            {
                amod = value;
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ������ϸ����
        /// </summary>
        private void GetLenderPrePayDetail()
        {
            DataSet ds = null;
            string sumCol = string.Empty;
            if (operationType == OperType.DayReport)
            {
                //0���跽ҽ��Ԥ�տ���ϸ 1������ҽ��Ԥ�տ���ϸ 2������ҽ��Ӧ�տ�
                switch (amod)
                {
                    case 0:
                        {
                            ds = feeDayReport.GetLenderPrePayDetail(this.dtBegin, this.dtEnd, feeDayReport.Operator.ID, 0);
                            sumCol = "Ԥ�����";
                            break;
                        }
                    case 1:
                        {
                            ds = feeDayReport.GetLenderPrePayDetail(this.dtBegin, this.dtEnd, feeDayReport.Operator.ID, 1);
                            sumCol = "���";
                            break;
                        }
                    case 2:
                        {
                            ds = feeDayReport.GetLenderPayDetail(this.dtBegin, this.dtEnd, feeDayReport.Operator.ID);
                            sumCol = "���ý��";
                            break;
                        }
                }
                if (ds == null)
                {
                    MessageBox.Show(feeDayReport.Err);
                    return;
                }
            }
            else
            {
                //0���跽ҽ��Ԥ�տ���ϸ 1������ҽ��Ԥ�տ���ϸ 2������ҽ��Ӧ�տ�
                switch (amod)
                {
                    case 0:
                        {

                            ds = feeDayReport.GetLenderPrePayDetail(list, 0);
                            sumCol = "Ԥ�����";
                            break;
                        }
                    case 1:
                        {
                            ds = feeDayReport.GetLenderPrePayDetail(list, 1);
                            sumCol = "���";
                            break;
                        }
                    case 2:
                        {
                            ds = feeDayReport.GetLenderPayDetail(list);
                            sumCol = "���ý��";
                            break;
                        }
                }
                if (ds == null)
                {
                    MessageBox.Show(feeDayReport.Err);
                    return;
                }
            }
            this.neuSpread1.DataSource = ds;

            GetSummer(ds,sumCol);
        }

        private void GetSummer(DataSet ds,string sumCol)
        {
            decimal summer = 0m;
            
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                summer += Neusoft.FrameWork.Function.NConvert.ToDecimal(dr[sumCol]);
            }
            int rowCount = this.neuSpread1_Sheet1.Rows.Count;
            this.neuSpread1_Sheet1.Rows.Add(rowCount, 1);
            this.neuSpread1_Sheet1.Cells[rowCount, 0].Text = "�ϼƣ�";
            this.neuSpread1_Sheet1.Models.Span.Add(rowCount, 1, 1, this.neuSpread1_Sheet1.Columns.Count - 1);
            this.neuSpread1_Sheet1.Cells[rowCount, 1].Text = summer.ToString();
        }
        #endregion

        #region �¼�
        private void btClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void ucDayReportDetail_Load(object sender, EventArgs e)
        {
            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ������ݣ���ȴ�^^");
            Application.DoEvents();
            this.FindForm().Text = frmTitle;
            this.lblTitle.Text = frmTitle;
            this.lblBeginDate.Text = this.dtBegin.ToString();
            this.lblEndDate.Text = this.dtEnd.ToString();
            this.lblOperater.Text = feeDayReport.Operator.Name;
            GetLenderPrePayDetail();
            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            print.PrintPage(0, 0, this.panelPrint);
        }
        #endregion
    }
}
