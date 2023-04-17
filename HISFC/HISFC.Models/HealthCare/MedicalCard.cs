using System;

namespace neusoft.HISFC.Object.HealthCare
{
	/// <summary>
	/// ҽ��֤ʵ�� 
	/// ��������ͨ����ҽѧ����صı�����ҵ�ṩ�ķ���Ԥ�������ƺʹ�����������������������ϵĽ���
	/// </summary>
	public class MedicalCard: neusoft.neuFC.Object.neuObject
	{
		private System.String myId ;
		private System.String myMcardNo ;
		private System.String myRegisterId ;
		private System.DateTime myRegisterDate ;
		private System.String myAutditingId ;
		private System.DateTime myAutditingDate ;
		private StatusTypeENUM myStatus ;
		private System.String myBlockId ;
		private System.DateTime myBlockDate ;

		public MedicalCard() 
		{
			// TODO: �ڴ˴���ӹ��캯���߼�
		}
	

		/// <summary>
		/// ҵ�����
		/// </summary>
		public System.String Id
		{
			get{ return this.myId; }
			set{ this.myId = value; }
		}


        /// <summary>
        /// Ա��������Ϣ
        /// </summary>
		public Object.RADT.Person EmployeeInfo=new Object.RADT.Person();

		/// <summary>
		/// ��Ա����1 ���¿ƶ���Ա���еķ��࣬�����ŵĿ�ͷ��ĸ����ϵ��
		/// ����1�ֿ�ͷ��������������Ա��2�ֿ�ͷ����ҽ����Ա��
		/// </summary>
		public neusoft.neuFC.Object.neuObject EmplKind1 = new neusoft.neuFC.Object.neuObject();
	
		/// <summary>
		/// ��Ա����2 Ժ�ں�ͬ��Ա����ʿ���
		/// </summary>
		public neusoft.neuFC.Object.neuObject EmplKind2 = new neusoft.neuFC.Object.neuObject();

		/// <summary>
		/// ҽ��֤��
		/// </summary>
		public System.String McardNo
		{
			get{ return this.myMcardNo; }
			set{ this.myMcardNo = value; }
		}

		/// <summary>
		/// �Ǽ���
		/// </summary>
		public System.String RegisterId
		{
			get{ return this.myRegisterId; }
			set{ this.myRegisterId = value; }
		}


		/// <summary>
		/// �Ǽ�ʱ��
		/// </summary>
		public System.DateTime RegisterDate
		{
			get{ return this.myRegisterDate; }
			set{ this.myRegisterDate = value; }
		}


		/// <summary>
		/// ���һ��������
		/// </summary>
		public System.String AutditingId
		{
			get{ return this.myAutditingId; }
			set{ this.myAutditingId = value; }
		}


		/// <summary>
		/// ���һ������ʱ��
		/// </summary>
		public System.DateTime AutditingDate
		{
			get{ return this.myAutditingDate; }
			set{ this.myAutditingDate = value; }
		}

		/// <summary>
		/// ҽ��֤״̬, 0����,1��Ч,2����
		/// </summary>
		public StatusTypeENUM Status
		{
			get{ return this.myStatus; }
			set{ this.myStatus = value; }
		}


		/// <summary>
		/// ����/������
		/// </summary>
		public System.String BlockId
		{
			get{ return this.myBlockId; }
			set{ this.myBlockId = value; }
		}


		/// <summary>
		/// ����/����ʱ��
		/// </summary>
		public System.DateTime BlockDate
		{
			get{ return this.myBlockDate; }
			set{ this.myBlockDate = value; }
		}


		/// <summary>
		/// ����ԭ��
		/// </summary>
		public neusoft.neuFC.Object.neuObject BlkReasonid = new neusoft.neuFC.Object.neuObject();

	}
		/// <summary>
		/// ��״̬
		/// </summary>
		public enum StatusTypeENUM
		{
			/// <summary>
			/// ����
			/// </summary>
			Cancel,
			/// <summary>
			/// ��Ч
			/// </summary>
			Valid,
			/// <summary>
			/// ����
			/// </summary>
			Block
		}
	
}
