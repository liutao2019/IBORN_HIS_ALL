using System;

namespace FS.HISFC.Models.Nurse
{
	/// <summary>
	/// Inject<br></br>
	/// [��������: ע��ʵ��]<br></br>
	/// [�� �� ��: ������]<br></br>
	/// [����ʱ��: 2006-09-01]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��ΰ��'
	///		�޸�ʱ��='2007-02-07'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    /// 
    [System.Serializable]
	public class Inject : FS.FrameWork.Models.NeuObject
	{
		
		#region ����

		/// <summary>
		/// ע��˳��
		/// </summary>
		private System.String orderNO = "";

		/// <summary>
		/// �Ƿ�Ƥ��
		/// </summary>
		private System.String hypotest = "";

		/// <summary>
		/// ִ��ʱ��
		/// </summary>
		private System.DateTime execTime = System.DateTime.MinValue;

		/// <summary>
		/// ��ҩʱ��
		/// </summary>
		private System.DateTime mixTime = System.DateTime.MinValue;
		/// <summary>
		/// ע��ʱ��
		/// </summary>
		private System.DateTime injectTime = System.DateTime.MinValue;

		/// <summary>
		/// ����
		/// </summary>
		private System.Int32 injectSpeed = 0;
		/// <summary>
		/// ����ʱ��
		/// </summary>
		private System.DateTime endTime = System.DateTime.MinValue;
		/// <summary>
		/// ������ʱ��
		/// </summary>
		private System.DateTime sendemcTime = System.DateTime.MinValue;

		/// <summary>
		/// ע����˳��
		/// </summary>
		private System.String injectOrder = "";

        /// <summary>
        /// ���߷�����Ŀ��Ϣ
        /// </summary>
        private FS.HISFC.Models.Fee.Outpatient.FeeItemList item = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();
        
        /// <summary>
        /// ��ҩ����Ϣ36.37
        /// </summary>
        private FS.FrameWork.Models.NeuObject mixOperInfo = new FS.FrameWork.Models.NeuObject();
        
		/// <summary>
		/// ע������Ϣ39.40
		/// </summary>
		private  FS.FrameWork.Models.NeuObject injectOperInfo = new FS.FrameWork.Models.NeuObject();        

		/// <summary>
		/// ��������Ϣ47
		/// </summary>
		private FS.FrameWork.Models.NeuObject stopOper = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.Models.Registration.Register patient = new FS.HISFC.Models.Registration.Register();
        
        /// <summary>
        /// �Ǽǲ�������
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment booker = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ƿǩ��ӡ���
        /// {EB016FFE-0980-479c-879E-225462ECA6D0}
        /// </summary>
        private string printNo = "";

		#endregion

		#region ����

        /// <summary>
        /// �Ǽǲ�������
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Booker
        {
            get
            {
                return this.booker;
            }
            set
            {
                this.booker = value;
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public FS.HISFC.Models.Registration.Register Patient
        {
            get
            {
                return this.patient;
            }
            set
            {
                this.patient = value;
            }
        }

		/// <summary>
		/// ���߷�����Ŀ��Ϣ
		/// </summary>
        public FS.HISFC.Models.Fee.Outpatient.FeeItemList Item
        {
            get
            {
                return this.item;
            }
            set
            {
                this.item = value;
            }
        }
        
		/// <summary>
		/// ��ҩ����Ϣ36.37
		/// </summary>
        public FS.FrameWork.Models.NeuObject MixOperInfo
        {
            get
            {
                return this.mixOperInfo;
            }
            set
            {
                this.mixOperInfo = value;
            }
        }

		/// <summary>
		/// ÿ��˳���2
		/// </summary>
		public System.String OrderNO
		{
			get
			{
                return this.orderNO;
			}
			set
			{
                this.orderNO = value; 
			}
		}

		/// <summary>
		/// �Ƿ�Ƥ��26
		/// </summary>
		public System.String Hypotest
		{
			get
			{
				return this.hypotest; 
			}
			set
			{
				this.hypotest = value; 
			}
		}

		/// <summary>
		/// ִ������33
		/// </summary>
		public System.DateTime ExecTime
		{
			get
			{
				return this.execTime; 
			}
			set
			{
				this.execTime = value;
			}
		}

		/// <summary>
		/// ��ҩʱ��38
		/// </summary>
		public System.DateTime MixTime
		{
			get
			{ 
				return this.mixTime;
			}
			set
			{
				this.mixTime = value;
			}
		}

		/// <summary>
		/// ע������Ϣ39.40
		/// </summary>
		public FS.FrameWork.Models.NeuObject InjectOperInfo
        {
            get
            {
                return this.injectOperInfo;
            }
            set
            {
                this.injectOperInfo = value;
            }
        }

        /// <summary>
        /// ע��ʱ��
        /// </summary>
        public DateTime InjectTime
        {
            get
            {
                return this.injectTime;
            }
            set
            {
                this.injectTime = value;
            }
        }

		/// <summary>
		/// ����42
		/// </summary>
		public System.Int32 InjectSpeed
		{
			get
			{
				return this.injectSpeed;
			}
			set
			{ 
				this.injectSpeed = value; 
			}
		}

		/// <summary>
		/// ����ʱ��43
		/// </summary>
		public System.DateTime EndTime
		{
			get
			{ 
				return this.endTime;
			}
			set
			{ 
				this.endTime = value;
			}
		}

		/// <summary>
		/// �ͼ���ʱ��44
		/// </summary>
		public System.DateTime SendemcTime
		{
			get
			{ 
				return this.sendemcTime;
			}
			set
			{ 
				this.sendemcTime = value; 
			}
		}


		/// <summary>
		/// ������ע��˳���46
		/// </summary>
		public System.String InjectOrder
		{
			get
			{
				return this.injectOrder; 
			}
			set
			{ 
				this.injectOrder = value;
			}
		}
        
		/// <summary>
		/// ��������Ϣ47
		/// </summary>
		public FS.FrameWork.Models.NeuObject StopOper
        {
            get
            {
                return this.stopOper;
            }
            set
            {
                this.stopOper = value;
            }
        }

        /// <summary>
        /// ƿǩ��ӡ���
        /// {EB016FFE-0980-479c-879E-225462ECA6D0}
        /// </summary>
        public string PrintNo
        {
            get
            {
                return printNo;
            }
            set
            {
                printNo = value;
            }
        }

		#endregion

		#region ����
		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns></returns>
		public new Inject Clone()
		{
			Inject inject = base.Clone() as Inject;
            inject.item = this.item.Clone();
            inject.mixOperInfo = this.mixOperInfo.Clone();
            inject.injectOperInfo = this.injectOperInfo.Clone();
            inject.stopOper = this.stopOper.Clone();
            inject.patient = this.patient.Clone();
            inject.booker = this.booker.Clone();
			return inject;
		}
		#endregion

        #region ���ڵ�
        
		/// <summary>
		/// ��ע
		/// </summary>
		//private System.String myRemark = "";
		/// <summary>
		/// ע��ΨһID��
		/// </summary>
        //private System.String myID = "";
		/// <summary>
		/// ��������
		/// </summary>
        //private System.String patientName = "";	
		/// <summary>
		/// �Ա�
		/// </summary>
        //private System.String mySexCode = "";
		/// <summary>
		/// ����
		/// </summary>
        //private System.DateTime myBirthday = System.DateTime.MinValue;	
		/// <summary>
		/// �Ǽ���
		/// </summary>
		//private System.String myBookerID = "";
		/// <summary>
		/// �Ǽ�ʱ��
		/// </summary>
		//private System.DateTime myRegisterDate = System.DateTime.MinValue;

        
		/// <summary>
		/// ��������5
		/// </summary>
        [Obsolete("����Register��ĳ�Ա", true)]
		public System.String PatientName
		{
			get
			{
				return null; 
			}
			set
			{
			}
		}

		/// <summary>
		/// �Ա�6
		/// </summary>
        [Obsolete("����Register��ĳ�Ա", true)]
		public System.String SexCode
		{
			get
			{
                return null;
			}
			set
			{
			}
		}

		/// <summary>
		/// ��������7
		/// </summary>
        [Obsolete("����Register��ĳ�Ա", true)]
		public System.DateTime Birthday
		{
			get
			{ 
				return DateTime.MinValue; 
			}
			set
			{
			}
		}
		/// <summary>
		/// ��ע45
		/// </summary>
        [Obsolete("�ñ����Memo�ֶ�", true)]
		public System.String Remark
		{
			get
			{
                return null;
			}
			set
			{ 
			}
		}
		/// <summary>
		/// ע��ʱ��41
		/// </summary>
        [Obsolete("����InjectTime",true)]
		public System.DateTime InjectDate
		{
			get
			{ 
				return this.injectTime;
			}
			set
			{ 
				this.injectTime = value; 
			}
		}
		/// <summary>
		/// �Ǽ���34
		/// </summary>
        [Obsolete("����OperEnvironment��Name��Ա", true)]
		public System.String BookerID
		{
			get
			{
				return null; 
			}
			set
			{
			}
		}
        
		/// <summary>
		/// �Ǽ�ʱ��35
		/// </summary>
        [Obsolete("����OperEnvironment��OperTime��Ա", true)]
		public System.DateTime RegisterDate
		{
			get
			{
				return DateTime.MinValue; 
			}
			set
			{ 
			}
		}
		/// <summary>
		/// ��ˮ��1
		/// </summary>
        [Obsolete("�ñ���ID����", true)]
		public System.String Id
		{
			get
			{
                return null;
			}
			set
			{ 
			}
		}
#endregion

	}
}
