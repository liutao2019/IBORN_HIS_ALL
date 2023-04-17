using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Equipment
{
    [System.Serializable]
    public class PayList : FS.FrameWork.Models.NeuObject
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public PayList()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region �ֶ�

        /// <summary>
        /// �����̽����ϸ��ˮ��
        /// </summary>
        private string payListNo;

        /// <summary>
        /// �����̽����ˮ��
        /// </summary>
        private string payNo;

        /// <summary>
        /// �豸������Ϣ
        /// </summary>
        private FS.FrameWork.Models.NeuObject deptInfo = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �Ѹ�����
        /// </summary>
        private decimal payCost;

        /// <summary>
        /// ��Ʊ����
        /// </summary>
        private string invoiceNo;

        /// <summary>
        /// ƾ֤����
        /// </summary>
        private string voucherNo;

        /// <summary>
        /// �����������
        /// </summary>
        private string moneyType;

        /// <summary>
        /// ������
        /// </summary>
        private string payOper;

        /// <summary>
        /// ������
        /// </summary>
        private string dealOper;

        /// <summary>
        /// ��������
        /// </summary>
        private DateTime payDate;

        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        private OperEnvironment operInfo = new OperEnvironment();

        /// <summary>
        /// ��ע
        /// </summary>
        private string remark;

        /// <summary>
        /// �������1 ����(ͨ���ֽ�)��2 ����(ͨ��Ԥ����)��3 Ԥ����
        /// </summary>
        private string payType;

        /// <summary>
        /// ������˾
        /// </summary>
        private string companyID;

        #endregion

        #region ����

        /// <summary>
        /// �����̽����ϸ��ˮ��
        /// </summary>
        public string PayListNo
        {
            get { return payListNo; }
            set { payListNo = value; }
        }

        /// <summary>
        /// �����̽����ˮ��
        /// </summary>
        public string PayNo
        {
            get { return payNo; }
            set { payNo = value; }
        }

        /// <summary>
        /// �豸������Ϣ
        /// </summary>
        public FS.FrameWork.Models.NeuObject DeptInfo
        {
            get { return deptInfo; }
            set { deptInfo = value; }
        }

        /// <summary>
        /// �Ѹ�����
        /// </summary>
        public decimal PayCost
        {
            get { return payCost; }
            set { payCost = value; }
        }

        /// <summary>
        /// ��Ʊ����
        /// </summary>
        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
        }

        /// <summary>
        /// ƾ֤����
        /// </summary>
        public string VoucherNo
        {
            get { return voucherNo; }
            set { voucherNo = value; }
        }

        /// <summary>
        /// �����������
        /// </summary>
        public string MoneyType
        {
            get { return moneyType; }
            set { moneyType = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string PayOper
        {
            get { return payOper; }
            set { payOper = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string DealOper
        {
            get { return dealOper; }
            set { dealOper = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public DateTime PayDate
        {
            get { return payDate; }
            set { payDate = value; }
        }

        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        public OperEnvironment OperInfo
        {
            get { return operInfo; }
            set { operInfo = value; }
        }

        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        /// <summary>
        /// �������1 ����(ͨ���ֽ�)��2 ����(ͨ��Ԥ����)��3 Ԥ����
        /// </summary>
        public string PayType
        {
            get { return payType; }
            set { payType = value; }
        }

        /// <summary>
        /// ������˾
        /// </summary>
        public string CompanyID
        {
            get { return companyID; }
            set { companyID = value; }
        }

        #endregion

        #region ����

        public new PayList Clone()
        {
            PayList PayList = base.Clone() as PayList;

            PayList.deptInfo = this.deptInfo.Clone();
            PayList.operInfo = this.operInfo.Clone();

            return PayList;
        }

        #endregion
    }
}
