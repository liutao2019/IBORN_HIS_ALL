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
        private Schema doctor = new Schema();
		#endregion 		
		
        /// <summary>
        /// ��������
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// �Ƿ��ѿ���
        /// </summary>
        private bool isSee = false;

        /// <summary>
        /// ȷ����
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment confirmOper = new FS.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public Schema DoctorInfo
        {
            get { return this.doctor; }
            set { this.doctor = value; }
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
            get { return this.oper; }
            set { this.oper = value; }
        }

        /// <summary>
        /// ȷ����
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment ConfirmOper
        {
            get { return this.confirmOper; }
            set { this.confirmOper = value; }
        }
        #endregion

        #region ����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new Booking Clone()
		{
            Booking obj = base.Clone() as Booking;

            obj.DoctorInfo = this.doctor.Clone();
            obj.Oper = this.oper.Clone();

            return obj;
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
