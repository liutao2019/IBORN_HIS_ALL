using System;

namespace neusoft.HISFC.Object.Case {
	/// <summary>
	/// סԺ�ձ�ʵ��
	/// ID   ���ұ���
	/// Name ��������
	/// </summary>
	public class InpatientDayReport: neusoft.neuFC.Object.neuObject {
		private System.DateTime myDateStat ;
		private neusoft.neuFC.Object.neuObject nurseStation = new neusoft.neuFC.Object.neuObject();
		private System.Int32 myBedStand ;
		private System.Int32 myBedAdd ;
		private System.Int32 myBedFree ;
		private System.Int32 myBeginningNum ;
		private System.Int32 myInNormal ;
		private System.Int32 myInEmergency ;
		private System.Int32 myInTransfer ;
		private System.Int32 myInTransferInner ;
		private System.Int32 myInReturn ;
		private System.Int32 myOutNormal ;
		private System.Int32 myOutTransfer ;
		private System.Int32 myOutTransferInner  ;
		private System.Int32 myOutWithdrawal ;
		private System.Int32 myEndNum ;
		private System.Int32 myDeadIn24 ;
		private System.Int32 myDeadOut24 ;
		private System.Decimal myBedRate ;
		private System.Int32 myOther1Num ;
		private System.Int32 myOther2Num ;
		private System.String myOperCode ;
		private System.DateTime myOperDate ;

		/// <summary>
		/// סԺ�ձ�ʵ��
		/// ID   ���ұ���
		/// Name ��������
		/// </summary>
		public InpatientDayReport() {
			// TODO: �ڴ˴���ӹ��캯���߼�
		}


		/// <summary>
		/// ͳ������
		/// </summary>
		public System.DateTime DateStat {
			get{ return this.myDateStat; }
			set{ this.myDateStat = value; }
		}

		
		/// <summary>
		/// ��ʿվ
		/// </summary>
		public neusoft.neuFC.Object.neuObject  NurseStation {
			get{ return this.nurseStation; }
			set{ this.nurseStation = value; }
		}


		/// <summary>
		/// �����ڲ�����
		/// </summary>
		public System.Int32 BedStand {
			get{ return this.myBedStand; }
			set{ this.myBedStand = value; }
		}


		/// <summary>
		/// �Ӵ���
		/// </summary>
		public System.Int32 BedAdd {
			get{ return this.myBedAdd; }
			set{ this.myBedAdd = value; }
		}


		/// <summary>
		/// �մ���
		/// </summary>
		public System.Int32 BedFree {
			get{ return this.myBedFree; }
			set{ this.myBedFree = value; }
		}


		/// <summary>
		/// �ڳ�������
		/// </summary>
		public System.Int32 BeginningNum {
			get{ return this.myBeginningNum; }
			set{ this.myBeginningNum = value; }
		}


		/// <summary>
		/// ������Ժ��
		/// </summary>
		public System.Int32 InNormal {
			get{ return this.myInNormal; }
			set{ this.myInNormal = value; }
		}


		/// <summary>
		/// ������Ժ��
		/// </summary>
		public System.Int32 InEmergency {
			get{ return this.myInEmergency; }
			set{ this.myInEmergency = value; }
		}


		/// <summary>
		/// ������ת����
		/// </summary>
		public System.Int32 InTransfer {
			get{ return this.myInTransfer; }
			set{ this.myInTransfer = value; }
		}


		/// <summary>
		/// ������ת����(�ڲ�ת��,��ɽһ����)
		/// </summary>
		public System.Int32 InTransferInner {
			get{ return this.myInTransferInner; }
			set{ this.myInTransferInner = value; }
		}


		/// <summary>
		/// �л���Ժ����
		/// </summary>
		public System.Int32 InReturn {
			get{ return this.myInReturn; }
			set{ this.myInReturn = value; }
		}


		/// <summary>
		/// �����Ժ��
		/// </summary>
		public System.Int32 OutNormal {
			get{ return this.myOutNormal; }
			set{ this.myOutNormal = value; }
		}


		/// <summary>
		/// ת����������
		/// </summary>
		public System.Int32 OutTransfer {
			get{ return this.myOutTransfer; }
			set{ this.myOutTransfer = value; }
		}


		/// <summary>
		/// ת����������(�ڲ�ת��,��ɽһ����)
		/// </summary>
		public System.Int32 OutTransferInner {
			get{ return this.myOutTransferInner; }
			set{ this.myOutTransferInner = value; }
		}


		/// <summary>
		/// ��Ժ����
		/// </summary>
		public System.Int32 OutWithdrawal {
			get{ return this.myOutWithdrawal; }
			set{ this.myOutWithdrawal = value; }
		}


		/// <summary>
		/// ��ĩ������
		/// </summary>
		public System.Int32 EndNum {
			get{ return this.myEndNum; }
			set{ this.myEndNum = value; }
		}


		/// <summary>
		/// 24Сʱ��������
		/// </summary>
		public System.Int32 DeadIn24 {
			get{ return this.myDeadIn24; }
			set{ this.myDeadIn24 = value; }
		}


		/// <summary>
		/// 24Сʱ��������
		/// </summary>
		public System.Int32 DeadOut24 {
			get{ return this.myDeadOut24; }
			set{ this.myDeadOut24 = value; }
		}


		/// <summary>
		/// ��λʹ����
		/// </summary>
		public System.Decimal BedRate {
			get{ return this.myBedRate; }
			set{ this.myBedRate = value; }
		}


		/// <summary>
		/// ����1����
		/// </summary>
		public System.Int32 Other1Num {
			get{ return this.myOther1Num; }
			set{ this.myOther1Num = value; }
		}


		/// <summary>
		/// ����2����
		/// </summary>
		public System.Int32 Other2Num {
			get{ return this.myOther2Num; }
			set{ this.myOther2Num = value; }
		}


		/// <summary>
		/// ������
		/// </summary>
		public System.String OperCode {
			get{ return this.myOperCode; }
			set{ this.myOperCode = value; }
		}


		/// <summary>
		/// ��������
		/// </summary>
		public System.DateTime OperDate {
			get{ return this.myOperDate; }
			set{ this.myOperDate = value; }
		}

	}
}