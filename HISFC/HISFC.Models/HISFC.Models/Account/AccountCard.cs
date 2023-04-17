using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Account
{
    /// <summary>
    /// FS.HISFC.Models.Account.AccountCard<br></br>
    /// [��������: �����ʻ���ʵ��]<br></br>
    /// [�� �� ��: ·־��]<br></br>
    /// [����ʱ��: 2007-05-03]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class AccountCard :FS.FrameWork.Models.NeuObject, Base.IValid
    {
       
        /// <summary>
        /// ���캯��
        /// </summary>
        public AccountCard()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����
        ///// <summary>
        ///// ���ﲡ��
        ///// </summary>
        //private string cardNO = string.Empty;
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// ��ݱ�ʶ����
        /// </summary>
        private string markNO = string.Empty;
        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.Models.NeuObject markType = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        private bool isValid =true;

        /// <summary>
        /// �ʻ�������ʵ��
        /// </summary>
        private List< AccountCardRecord> accountCardRecord = new List<AccountCardRecord>();
        
        #endregion 

        #region ����
        ///// <summary>
        ///// ���ﲡ��
        ///// </summary>
        //public string CardNO
        //{
        //    get
        //    {
        //        return this.cardNO;
        //    }
        //    set
        //    {
        //        this.cardNO = value;
        //    }
        //}
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            set
            {
                patient = value;
            }
            get
            {
                return patient;
            }
        }

        /// <summary>
        /// ��ݱ�ʶ����
        /// </summary>
        public string MarkNO
        {
            get
            {
                return this.markNO;
            }
            set
            {
                this.markNO = value;
            }
        }
        /// <summary>
        /// ��ݱ�ʶ����� 1�ſ� 2IC�� 3���Ͽ�
        /// </summary>
        public FS.FrameWork.Models.NeuObject MarkType
        {
            get
            {
                return this.markType;
            }
            set
            {
                this.markType = value;
            }
        }

        /// <summary>
        /// �����ʻ�������ʵ��
        /// </summary>
        public List<AccountCardRecord> AccountCardRecord
        {
            get
            {
                return this.accountCardRecord;
            }
            set
            {
                this.accountCardRecord = value;
            }
        }
       
        #endregion 

        #region ����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new AccountCard Clone()
        {
            AccountCard accountCard = base.Clone() as AccountCard;
            accountCard.MarkType = this.MarkType.Clone() as FS.FrameWork.Models.NeuObject;
            foreach (AccountCardRecord cardRecord in this.AccountCardRecord)
            {
                accountCard.AccountCardRecord.Add(cardRecord);
            }
            return accountCard;
        }
        #endregion

        #region IValid ��Ա
        /// <summary>
        /// �Ƿ���Ч true��Ч false����
        /// </summary>
        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
            set
            {
                this.isValid = value;
            }
        }

        #endregion

               

    }
}
