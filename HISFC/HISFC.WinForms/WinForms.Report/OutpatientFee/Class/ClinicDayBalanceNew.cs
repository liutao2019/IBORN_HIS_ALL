using System;
using System.Collections.Generic;
using System.Text;

namespace FS.WinForms.Report.OutpatientFee.Class
{
    public class ClinicDayBalanceNew : FS.FrameWork.Models.NeuObject
    {
        #region ����
        /// <summary>
        /// �ս����
        /// </summary>
        private string blanceNo = string.Empty;
        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime beginTime = DateTime.MinValue;
        /// <summary>
        /// ��ֹʱ��
        /// </summary>
        private DateTime endTime = DateTime.MinValue;
        /// <summary>
        /// ������
        /// </summary>
        private decimal totCost = 0;
        /// <summary>
        /// ��������
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper=new FS.HISFC.Models.Base.OperEnvironment();
        /// <summary>
        /// ͳ�ƴ���
        /// </summary>
        private FS.FrameWork.Models.NeuObject invoiceNo = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// ��ʼ��Ʊ��
        /// </summary>
        private string begionInvoiceNo = string.Empty;
        /// <summary>
        /// ��ֹ��Ʊ��
        /// </summary>
        private string endInvoiceNo = string.Empty;
        /// <summary>
        /// ���Ϸ�Ʊ��
        /// </summary>
        private string falseInvoiceNo = string.Empty;
        /// <summary>
        /// �˷ѷ�Ʊ��
        /// </summary>
        private string cancelInvoiceNo = string.Empty;
        /// <summary>
        /// �������� 4����Ŀ��ϸ 5����Ʊ��Ϣ 6�������Ϣ
        /// </summary>
        private string typeStr = string.Empty;

        /// <summary>
        /// ������ʾ��λ��
        /// </summary>
        private string sortId = string.Empty;
        #endregion

        #region ����
        /// <summary>
        /// �ս����
        /// </summary>
        public string BlanceNO
        {
            get
            {
                return blanceNo;
            }
            set
            {
                blanceNo = value;
            }

        }

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        public DateTime BeginTime
        {
            get
            {
                return beginTime;
            }
            set
            {
                beginTime = value;
            }
        }

        /// <summary>
        /// ��ֹʱ��
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public decimal TotCost
        {
            get
            {
                return totCost;
            }
            set
            {
                totCost = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get
            {
                return oper;
            }
            set
            {
                oper = value;
            }
        }

        /// <summary>
        /// ͳ�ƴ���
        /// </summary>
        public FS.FrameWork.Models.NeuObject InvoiceNO
        {
            get
            {
                return invoiceNo;
            }
            set
            {
                invoiceNo = value;
            }
        }

        /// <summary>
        /// ��ʼ��Ʊ��
        /// </summary>
        public string BegionInvoiceNO
        {
            get
            {
                return begionInvoiceNo;
            }
            set
            {
                begionInvoiceNo = value;
            }
        }

        /// <summary>
        /// ��ֹ��Ʊ��
        /// </summary>
        public string EndInvoiceNo
        {
            get
            {
                return endInvoiceNo;
            }
            set
            {
                endInvoiceNo = value;
            }
        }
        /// <summary>
        /// ���Ϸ�Ʊ��
        /// </summary>
        public string FalseInvoiceNo
        {
            get
            {
                return falseInvoiceNo;
            }
            set
            {
                falseInvoiceNo = value;
            }
        }

        /// <summary>
        /// �˷ѷ�Ʊ��
        /// </summary>
        public string CancelInvoiceNo
        {
            get
            {
                return cancelInvoiceNo;
            }
            set
            {
                cancelInvoiceNo = value;
            }
        }

        /// <summary>
        /// �������� 4����Ŀ��ϸ 5����Ʊ��Ϣ 6�������Ϣ
        /// </summary>
        public string TypeStr
        {
            get
            {
                return typeStr;
            }
            set
            {
                typeStr = value;
            }
        }
        /// <summary>
        /// ������ʾλ��
        /// </summary>
        public string SortID
        {
            get
            {
                return sortId;
            }
            set
            {
                sortId = value;
            }
        }
        #endregion

        #region ��¡
        public new ClinicDayBalanceNew Clone()
        {
            ClinicDayBalanceNew obj = base.Clone() as ClinicDayBalanceNew;
            obj.Oper = this.Oper.Clone();
            obj.InvoiceNO = this.InvoiceNO.Clone();
            return obj;
        }
        #endregion
    }
}
