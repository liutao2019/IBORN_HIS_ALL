using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Order
{   
    /// <summary>
    /// ������뵥��Ϣ
    /// </summary>
    [Serializable]
    public class CheckSlip : FS.FrameWork.Models.NeuObject
    {
        public CheckSlip()
        {

        }

        #region ����

        /// <summary>
        /// ����
        /// </summary>
        private string checkSlipNo;

        /// <summary>
        /// סԺ��
        /// </summary>
        private string inpatientNO;

        /// <summary>
        /// �����
        /// </summary>
        private string cardNo;

        /// <summary>
        /// ��������
        /// </summary>
        private string doct_dept;

        /// <summary>
        /// ����
        /// </summary>
        private string zsInfo;

        /// <summary>
        /// ��������
        /// </summary>
        private string yxtzInfo;

        /// <summary>
        /// ����ʵ������
        /// </summary>
        private string yxsyInfo;

        /// <summary>
        /// �����
        /// </summary>
        private string diagName;

        /// <summary>
        /// ��鲿λ
        /// </summary>
        private string itemNote;

        /// <summary>
        /// �Ƿ�Ӽ�(0��ͨ/1�Ӽ�)
        /// </summary>
        private string emcFlag;

        /// <summary>
        /// ��ע
        /// </summary>
        private string moNote;

        /// <summary>
        /// ��չ���1
        /// </summary>
        private string extFlag1;

        /// <summary>
        /// ��չ���2
        /// </summary>
        private string extFlag2;

        /// <summary>
        /// ��չ���3
        /// </summary>
        private string extFlag3;

        /// <summary>
        /// ��չ���4
        /// </summary>
        private string extFlag4;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime applyDate;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime operDate;

        #endregion 

        #region ����

        /// <summary>
        /// ����
        /// </summary>
        public string CheckSlipNo
        {
            get { return checkSlipNo; }
            set { checkSlipNo = value; }
        }

        /// <summary>
        /// סԺ��
        /// </summary>
        public string InpatientNO
        {
            get { return inpatientNO; }
            set { inpatientNO = value; }
        }

        /// <summary>
        /// �����
        /// </summary>
        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string Doct_dept
        {
            get { return doct_dept; }
            set { doct_dept = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string ZsInfo
        {
            get { return zsInfo; }
            set { zsInfo = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string YxtzInfo
        {
            get { return yxtzInfo; }
            set { yxtzInfo = value; }
        }

        /// <summary>
        /// ����ʵ������
        /// </summary>
        public string YxsyInfo
        {
            get { return yxsyInfo; }
            set { yxsyInfo = value; }
        }

        /// <summary>
        /// �����
        /// </summary>
        public string DiagName
        {
            get { return diagName; }
            set { diagName = value; }
        }

        /// <summary>
        /// ��鲿λ
        /// </summary>
        public string ItemNote
        {
            get { return itemNote; }
            set { itemNote = value; }
        }

        /// <summary>
        /// �Ƿ�Ӽ�(0��ͨ/1�Ӽ�)
        /// </summary>
        public string EmcFlag
        {
            get { return emcFlag; }
            set { emcFlag = value; }
        }

        /// <summary>
        /// ��ע
        /// </summary>
        public string MoNote
        {
            get { return moNote; }
            set { moNote = value; }
        }

        /// <summary>
        /// ��չ���1
        /// </summary>
        public string ExtFlag1
        {
            get { return extFlag1; }
            set { extFlag1 = value; }
        }

        /// <summary>
        /// ��չ���2
        /// </summary>
        public string ExtFlag2
        {
            get { return extFlag2; }
            set { extFlag2 = value; }
        }

        /// <summary>
        /// ��չ���3
        /// </summary>
        public string ExtFlag3
        {
            get { return extFlag3; }
            set { extFlag3 = value; }
        }

        /// <summary>
        /// ��չ���4
        /// </summary>
        public string ExtFlag4
        {
            get { return extFlag4; }
            set { extFlag4 = value; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime ApplyDate
        {
            get { return applyDate; }
            set { applyDate = value; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime OperDate
        {
            get { return operDate; }
            set { operDate = value; }
        }
        #endregion 

        #region ����
        #endregion 

    }
}
