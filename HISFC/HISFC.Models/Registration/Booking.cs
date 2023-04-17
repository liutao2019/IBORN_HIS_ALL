using System;

namespace FS.HISFC.Models.Registration
{
    /// <summary>
    /// <br>RegLevel</br>
    /// <br>[��������: ԤԼ�Һ�ʵ��]</br>
    /// <br>[�� �� ��: ��С��]</br>
    /// <br>[����ʱ��: 2007-2-1]</br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class Booking:FS.HISFC.Models.RADT.Patient
	{
		/// <summary>
		/// 
		/// </summary>
		public Booking()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//          
        }

        #region ����

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private Schema doctor;
		#endregion 		
		
        /// <summary>
        /// ��������
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper;

        /// <summary>
        /// �Ƿ��ѿ���
        /// </summary>
        private bool isSee;

        /// <summary>
        /// ÿ����ˮ��
        /// </summary>
        private int orderNO = 0;

        /// <summary>
        /// ȷ����
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment confirmOper;

        /// <summary>
        /// ԤԼ����ID
        /// </summary>
        private string bookingTypeId = string.Empty;

        /// <summary>
        /// ԤԼ��������
        /// </summary>
        private string bookingTypeName = string.Empty;

        /// <summary>
        /// �Һ���ˮ��
        /// </summary>
        private string regID = string.Empty;

        /// <summary>
        /// �Һ���ˮ��
        /// </summary>
        public string RegID
        {
            get
            {
                return regID;
            }
            set
            {
                regID = value;
            }
        }

        #endregion

        #region ����
        /// <summary>
        /// ԤԼ����ID
        /// </summary>
        public string BookingTypeId
        {
            get
            {
                return bookingTypeId;
            }
            set
            {
                bookingTypeId = value;
            }
        }

        /// <summary>
        /// ԤԼ��������
        /// </summary>
        public string BookingTypeName
        {
            get
            {
                return bookingTypeName;
            }
            set
            {
                bookingTypeName = value;
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public Schema DoctorInfo
        {
            get {
                if (this.doctor == null)
                {
                    this.doctor = new Schema();
                }

                return this.doctor; }
            set { this.doctor = value; }
        }

        /// <summary>
        /// ÿ����ˮ��
        /// </summary>
        public int OrderNO
        {
            get { return orderNO; }
            set { orderNO = value; }
        }

        /// <summary>
        /// �Ƿ��ѿ���
        /// </summary>
        public bool IsSee
        {
            get { return this.isSee; }
            set { this.isSee = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get {
                if (this.oper == null)
                {
                    this.oper = new FS.HISFC.Models.Base.OperEnvironment();
                }
                return this.oper; }
            set { this.oper = value; }
        }

        /// <summary>
        /// ȷ����
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment ConfirmOper
        {
            get {
                if (this.confirmOper == null)
                {
                    this.confirmOper = new FS.HISFC.Models.Base.OperEnvironment();
                }
                return this.confirmOper; }
            set { this.confirmOper = value; }
        }

        /// <summary>
        /// �Һŷ�����Ϣ
        /// </summary>
        private RegLvlFee regLvlFee;

        /// <summary>
        /// �Һŷ�����Ϣ
        /// </summary>
        public RegLvlFee RegLvlFee
        {
            get
            {
                if (regLvlFee == null)
                {
                    regLvlFee = new RegLvlFee();
                }
                return regLvlFee;
            }
            set { regLvlFee = value; }
        }

        /// <summary>
        /// ��ݱ�ʶ����
        /// </summary>
        private string markNO = string.Empty;
        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.Models.NeuObject markType;

        
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
                if (markType == null)
                {
                    markType = new FS.FrameWork.Models.NeuObject();
                }
                return this.markType;
            }
            set
            {
                this.markType = value;
            }
        }

        /// <summary>
        /// ������ˮ�ţ��ⲿ�ã�
        /// </summary>
        private string businessNo;

        /// <summary>
        /// ������ˮ�ţ��ⲿ�ã�
        /// </summary>
        public string BusinessNo
        {
            get { return businessNo; }
            set { businessNo = value; }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private string businessInfo;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string BusinessInfo
        {
            get
            {
                return businessInfo;
            }
            set { businessInfo = value; }
        }

        /// <summary>
        /// ��Ч��
        /// </summary>
        private bool isValid;

        /// <summary>
        /// ��Ч��
        /// </summary>
        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }

        #endregion

        #region ����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new Booking Clone()
		{
            Booking book = base.Clone() as Booking;

            book.DoctorInfo = this.DoctorInfo.Clone();
            book.Oper = this.Oper.Clone();
            book.MarkType = this.MarkType.Clone();
            book.ConfirmOper = this.ConfirmOper.Clone();
            book.RegLvlFee = this.RegLvlFee.Clone();


            return book;
        }
        #endregion

        #region ����
        /// <summary>
        /// ������
        /// </summary>
        [Obsolete("����Ϊ��PID.CardNO", true)]
        public string CardNo = "";

        /// <summary>
        /// ���֤
        /// </summary>
        [Obsolete("����Ϊ:IDCard", true)]
        public string IdenNo = "";

        /// <summary>
        /// �Ա�
        /// </summary>
        [Obsolete("����Ϊ��Sex.ID", true)]
        public string SexID = "";

        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        [Obsolete("����Ϊ��PhoneHome", true)]
        public string Phone = "";

        /// <summary>
        /// ��ַ
        /// </summary>
        [Obsolete("����Ϊ��AddressHome", true)]
        public string Address = "";
        #endregion
	}
}
