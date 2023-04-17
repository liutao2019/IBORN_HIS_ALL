using System;

namespace Neusoft.HISFC.Object.Registration
{
	/// <summary>
	/// ҽ�������Ű�ʵ��
	/// </summary>
	public class DoctSchema
	{
		public DoctSchema()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ˽�г�Ա
		/// <summary>
		/// ��������
		/// </summary>
		protected DateTime seeDate;
		/// <summary>
		/// ����
		/// </summary>
		protected string week;
		/// <summary>
		/// ������
		/// </summary>
		protected string noonID;
		/// <summary>
		/// ����ҽ��
		/// </summary>
		protected Neusoft.NFC.Object.NeuObject doctor=new Neusoft.NFC.Object.NeuObject();
		/// <summary>
		/// ҽ��������
		/// </summary>
		protected string doctType;
		/// <summary>
		/// �������
		/// </summary>
		protected string dept;
		/// <summary>
		/// ��������
		/// </summary>
		protected Neusoft.NFC.Object.NeuObject room=new Neusoft.NFC.Object.NeuObject();
		/// <summary>
		/// ��̨
		/// </summary>
		protected string estrade;
		/// <summary>
		/// �Һż���
		/// </summary>
		protected string regLevel;
		/// <summary>
		/// �Һ��޶�
		/// </summary>
		protected int regLimit;
		/// <summary>
		/// ԤԼ�Һ��޶�
		/// </summary>
		protected int preRegLimit;
		/// <summary>
		/// �ѹ���
		/// </summary>
		protected int hasReg;
		/// <summary>
		/// �ѹ�ԤԼ��
		/// </summary>
		protected int hasPreReg;
		/// <summary>
		/// �Ƿ���Ч
		/// </summary>
		protected bool isValid;
		/// <summary>
		/// ͣ��ԭ��
		/// </summary>
		protected Neusoft.NFC.Object.NeuObject stopReason=new Neusoft.NFC.Object.NeuObject();
		/// <summary>
		/// ֹͣ��
		/// </summary>
		protected string stopCode;
		/// <summary>
		/// ֹͣʱ��
		/// </summary>
		protected DateTime stopDate;
		/// <summary>
		/// ��ע
		/// </summary>
		protected string memo;
		/// <summary>
		/// ����Աid
		/// </summary>
		protected string operCode;
		/// <summary>
		/// ����ʱ��
		/// </summary>
		protected DateTime operDate;
		#endregion

		#region ���г�Ա
		/// <summary>
		/// ���
		/// </summary>
		public string ID;
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime SeeDate
		{
			get{return this.seeDate;}
			set
			{
				this.seeDate=value;

				this.week=((int)this.seeDate.DayOfWeek).ToString();
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Week
		{
			get{return this.week;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string NoonID
		{
			get{return this.noonID;}
			set{this.noonID=value;}
		}
		/// <summary>
		/// ����ҽ��
		/// </summary>
		public Neusoft.NFC.Object.NeuObject Doctor
		{
			get{return this.doctor;}
			set{this.doctor=value;}
		}
		/// <summary>
		/// ҽ��������
		/// </summary>
		public string DoctType
		{
			get{return this.doctType;}
			set{this.doctType=value;}
		}
		/// <summary>
		/// �������
		/// </summary>
		public string Dept
		{
			get{return this.dept;}
			set{this.dept=value;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public Neusoft.NFC.Object.NeuObject Room
		{
			get{return this.room;}
			set{this.room=value;}
		}
		/// <summary>
		/// ��̨
		/// </summary>
		public string Estrade
		{
			get{return this.estrade;}
			set{this.estrade=value;}
		}
		/// <summary>
		/// �Һż���
		/// </summary>
		public string RegLevel
		{
			get{return this.regLevel;}
			set{this.regLevel=value;}
		}
		/// <summary>
		/// �Һ��޶�
		/// </summary>
		public int RegLimit
		{
			get{return this.regLimit;}
			set{this.regLimit=value;}
		}
		/// <summary>
		/// ԤԼ�Һ��޶�
		/// </summary>
		public int PreRegLimit
		{
			get{return this.preRegLimit;}
			set{this.preRegLimit=value;}
		}
		/// <summary>
		/// �ѹҺ���
		/// </summary>
		public int HasReg
		{
			get{return this.hasReg;}
			set{this.hasReg=value;}
		}
		/// <summary>
		/// �ѹ�ԤԼ����
		/// </summary>
		public int HasPreReg
		{
			get{return this.hasPreReg;}
			set{this.hasPreReg=value;}
		}
		/// <summary>
		/// �Ƿ���Ч
		/// </summary>
		public bool IsValid
		{
			get{return this.isValid;}
			set{this.isValid=value;}
		}
		/// <summary>
		/// ͣ��ԭ��
		/// </summary>
		public Neusoft.NFC.Object.NeuObject StopReason
		{
			get{return this.stopReason;}
			set{this.stopReason=value;}
		}
		/// <summary>
		/// ֹͣ��
		/// </summary>
		public string StopID
		{
			get{return this.stopCode;}
			set{this.stopCode=value;}
		}
		/// <summary>
		/// ֹͣʱ��
		/// </summary>
		public DateTime StopDate
		{
			get{return this.stopDate;}
			set{this.stopDate=value;}
		}
		/// <summary>
		/// ��ע
		/// </summary>
		public string Memo
		{
			get{return this.memo;}
			set{this.memo=value;}
		}
		/// <summary>
		/// ����Աid
		/// </summary>
		public string OperID
		{
			get{return this.operCode;}
			set{this.operCode=value;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OperDate
		{
			get{return this.operDate;}
			set{this.operDate=value;}
		}
		#endregion

		/// <summary>
		/// �����ѹҺ���
		/// </summary>
		/// <returns></returns>
		public int AddReg()
		{
			if(this.hasReg==this.regLimit)return -1;
			this.hasReg++;
			return 0;
		}

		/// <summary>
		/// ����ԤԼ�ѹҺ���
		/// </summary>
		/// <returns></returns>
		public int AddPreReg()
		{
			if(this.hasPreReg==this.preRegLimit)return -1;
			this.hasPreReg++;
			return 0;
		}


		public new DoctSchema Clone()
		{			
			return this.MemberwiseClone() as DoctSchema;			
		}
	}


	/// <summary>
	/// ���ʵ��
	/// </summary>
	public class Noon:Neusoft.HISFC.Object.Base.Spell {

		public Noon()
		{
		}
		#region ˽�б���
		//ID,Name
		/// <summary>
		/// ��ʼʱ��
		/// </summary>
		protected DateTime beginTime;
		/// <summary>
		/// ����ʱ��
		/// </summary>
		protected DateTime endTime;
		/// <summary>
		/// //�Ƿ���
		/// </summary>
		protected bool isUrg=false;
		#endregion

		#region ���б���
		/// <summary>
		/// ��ʼʱ��
		/// </summary>
		public DateTime BeginTime
		{
			get{return beginTime;}
			set{beginTime=value;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime EndTime
		{
			get{return endTime;}
			set{endTime=value;}
		}
		/// <summary>
		/// �Ƿ���
		/// </summary>
		public bool IsUrg
		{
			get{return isUrg;}
			set{isUrg=value;}
		}

		/// <summary>
		/// ����Աid
		/// </summary>
		public string OperID;
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OperDate;
		#endregion

		public new Noon Clone()
		{
			return base.MemberwiseClone() as Noon;
		}
		

	}
}
