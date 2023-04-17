using System;

namespace FS.HISFC.Models.Nurse
{

	#region ����ʵ��
	/// <summary>
	/// Room<br></br>
	/// [��������: ����ʵ��]<br></br>
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
	public class Room:FS.FrameWork.Models.NeuObject
	{

		#region ����
		/// <summary>
		/// �������
		/// </summary>
		private FS.FrameWork.Models.NeuObject nurse = new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// ������
		/// </summary>
		private string inputCode = "";
		/// <summary>
		/// ��Ч��ʶ
		/// </summary>
		private string isValid = "0";
		/// <summary>
		/// ��ʾ˳��
		/// </summary>
		private int sort = 0;
		#endregion

		#region ����
		/// <summary>
		/// �������
		/// </summary>
		public FS.FrameWork.Models.NeuObject Nurse
		{
			get
			{
				return this.nurse;
			}
			set
			{
				this.nurse = value;
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		public string InputCode
		{
			get
			{
				return this.inputCode;
			}
			set
			{
				this.inputCode = value;
			}
		}

		/// <summary>
		/// ��Ч��ʶ
		/// </summary>
		public string IsValid
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

		/// <summary>
		/// ��ʾ˳��
		/// </summary>
		public int Sort
		{
			get
			{
				return this.sort;
			}
			set
			{
				this.sort = value;
			}
		}

		#endregion

		#region ����
		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns></returns>
		public new Room Clone()
		{
            Room seat = base.Clone() as Room;
			seat.nurse = this.nurse.Clone();
			return seat;
		}
		#endregion

	}

	#endregion

	#region ��̨ʵ��

	/// <summary>
	/// Seat<br></br>
	/// [��������: ��̨ʵ��]<br></br>
	/// [�� �� ��: ������]<br></br>
	/// [����ʱ��: 2006-09-01]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��ΰ��'
	///		�޸�ʱ��='2007-02-07'
	///		�޸�Ŀ��='�޸�'
	///		�޸�����=''
	///  />
	/// </summary>
    /// 
    [System.Serializable]
	public class Seat:FS.FrameWork.Models.NeuObject
    {
        #region ����

        /// <summary>
        /// ����Ա
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

		/// <summary>
		/// �����к�������
		/// </summary>
		private int currentCount = 0;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.Models.Nurse.Room room = new Room();

		#endregion

		#region ����
        
        /// <summary>
        /// ����Ա
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get
            {
                return this.oper;
            }
            set
            {
                this.oper = value;
            }
        }

		/// <summary>
		/// �����к�������
		/// </summary>
		public int CurrentCount
		{
			get
			{
				return this.currentCount;
			}
			set
			{
				this.currentCount = value;
			}
		}

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public Room PRoom
        {
            get
            {
                return this.room;
            }
            set
            {
                this.room = value;
            }
        }

		#endregion

		#region ����
        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public new Seat Clone()
        {
            Seat seat = base.Clone() as Seat;
            seat.room = this.room.Clone();
            seat.oper = this.oper.Clone();
            return seat;
        }

        #endregion

        #region ����

        /// <summary>
        /// ����Ա����
        /// </summary>
        private string operCode = "";

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime operDate = DateTime.MinValue;

        /// <summary>
        /// ����Ա����
        /// </summary>
        [Obsolete("ʹ��Oper.ID", true)]
        public string OperCode
        {
            get
            {
                return this.operCode;
            }
            set
            {
                this.operCode = value;
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        [Obsolete("ʹ��Oper.OperTime", true)]
        public DateTime OperDate
        {
            get
            {
                return this.operDate;
            }
            set
            {
                this.operDate = value;
            }
        }

        #endregion

	}
	#endregion

}
