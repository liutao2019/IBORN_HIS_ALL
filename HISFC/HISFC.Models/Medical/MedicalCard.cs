using System;
using Neusoft.HISFC.Models.Base;

namespace Neusoft.HISFC.Models.Medical
{
	/// <summary>
	/// [��������: ҽ��֤ʵ��]<br></br>
	/// [�� �� ��: ����ΰ]<br></br>
	/// [����ʱ��: 2006-09-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
	public class MedicalCard: Neusoft.FrameWork.Models.NeuObject
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
		public Neusoft.HISFC.Models.Base.Employee EmployeeInfo = new Employee();

		/// <summary>
		/// ��Ա����1 ���¿ƶ���Ա���еķ��࣬�����ŵĿ�ͷ��ĸ����ϵ��
		/// ����1�ֿ�ͷ��������������Ա��2�ֿ�ͷ����ҽ����Ա��
		/// </summary>
		public Neusoft.FrameWork.Models.NeuObject EmplKind1 = new Neusoft.FrameWork.Models.NeuObject();
	
		/// <summary>
		/// ��Ա����2 Ժ�ں�ͬ��Ա����ʿ���
		/// </summary>
		public Neusoft.FrameWork.Models.NeuObject EmplKind2 = new Neusoft.FrameWork.Models.NeuObject();

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
		public Neusoft.FrameWork.Models.NeuObject BlkReasonid = new Neusoft.FrameWork.Models.NeuObject();

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
