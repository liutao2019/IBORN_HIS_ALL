using System;

namespace neusoft.HISFC.Object.HealthCare
{
	/// <summary>
	/// Infection ��ժҪ˵����
	/// </summary>
	public class Infection: neusoft.neuFC.Object.neuObject
	{
		private System.String myId ;
		private System.String myZg ;
		private System.DateTime myInfectDate ;
		private System.String myIsOp ;
		private System.String myIsUrgop ;
		private System.String myInciType ;
		private System.DateTime myOpsDate ;
		private System.String myEndotrachealAnae ;
		private System.String myInfectSymptom ;
		private System.String myInfectDie ;
		private System.String myPathogenyInspect ;
		private System.String myPathogenyName1 ;
		private System.String myIsSusceptivity1 ;
		private System.String myIsInaction1 ;
		private System.String myPathogenyName2 ;
		private System.String myIsSusceptivity2 ;
		private System.String myIsInaction2 ;
		private System.String myAntibioticName ;
		private System.Int32 myAntibioticNum ;
		private System.String myOperCode ;
		private System.DateTime myOperDate ;
        private System.String isValid ;
		public Infection() 
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
		/// �������߻�����Ϣ �� �����Ϣ ��������Ϣ��������Ϣ �� ID inpatientNO,name ����,Memo ��ע  
		/// </summary>
		public neusoft.HISFC.Object.RADT.PatientInfo PatientInfo=new neusoft.HISFC.Object.RADT.PatientInfo();


		/// <summary>
		/// ��Ժ���
		/// </summary>
		public neusoft.neuFC.Object.neuObject OutDiag= new neusoft.neuFC.Object.neuObject();


		/// <summary>
		/// ת��
		/// </summary>
		public System.String Zg
		{
			get{ return this.myZg; }
			set{ this.myZg = value; }
		}


		/// <summary>
		/// ��Ⱦ����
		/// </summary>
		public System.DateTime InfectDate
		{
			get{ return this.myInfectDate; }
			set{ this.myInfectDate = value; }
		}


		/// <summary>
		/// ��Ⱦ��ϴ���
		/// </summary>
		public neusoft.neuFC.Object.neuObject InfectDiag = new neusoft.neuFC.Object.neuObject();


		/// <summary>
		/// ��Ⱦ��λ
		/// </summary>
		public neusoft.neuFC.Object.neuObject InfectPart = new neusoft.neuFC.Object.neuObject();


		/// <summary>
		/// ��Ⱦԭ��
		/// </summary>
		public neusoft.neuFC.Object.neuObject InfectReason = new neusoft.neuFC.Object.neuObject();
//		{
//			get{ return this.myInfectReason; }
//			set{ this.myInfectReason = value; }
//		}


		/// <summary>
		/// �Ƿ�����
		/// </summary>
		public System.String IsOp
		{
			get{ return this.myIsOp; }
			set{ this.myIsOp = value; }
		}


		/// <summary>
		/// ��������
		/// </summary>
		public neusoft.neuFC.Object.neuObject OpsInfo = new neusoft.neuFC.Object.neuObject();
//		{
//			get{ return this.myOpsCode; }
//			set{ this.myOpsCode = value; }
//		}


//		/// <summary>
//		/// ��������
//		/// </summary>
//		public System.String OpsName
//		{
//			get{ return this.myOpsName; }
//			set{ this.myOpsName = value; }
//		}


		/// <summary>
		/// �Ƿ�������
		/// </summary>
		public System.String IsUrgop
		{
			get{ return this.myIsUrgop; }
			set{ this.myIsUrgop = value; }
		}


		/// <summary>
		/// �п�����
		/// </summary>
		public System.String InciType
		{
			get{ return this.myInciType; }
			set{ this.myInciType = value; }
		}


		/// <summary>
		/// ��������
		/// </summary>
		public System.DateTime OpsDate
		{
			get{ return this.myOpsDate; }
			set{ this.myOpsDate = value; }
		}


		/// <summary>
		/// �Ƿ�����������
		/// </summary>
		public System.String EndotrachealAnae
		{
			get{ return this.myEndotrachealAnae; }
			set{ this.myEndotrachealAnae = value; }
		}


		/// <summary>
		/// ��Ⱦ�ٴ�֢״
		/// </summary>
		public System.String InfectSymptom
		{
			get{ return this.myInfectSymptom; }
			set{ this.myInfectSymptom = value; }
		}


		/// <summary>
		/// ��Ⱦ��������ϵ
		/// </summary>
		public System.String InfectDie
		{
			get{ return this.myInfectDie; }
			set{ this.myInfectDie = value; }
		}


		/// <summary>
		/// ��ԭѧ���
		/// </summary>
		public System.String PathogenyInspect
		{
			get{ return this.myPathogenyInspect; }
			set{ this.myPathogenyInspect = value; }
		}


		/// <summary>
		/// �걾1
		/// </summary>
		public neusoft.neuFC.Object.neuObject LabSample1 = new neusoft.neuFC.Object.neuObject();
		
		/// <summary>
		/// ��ԭ������
		/// </summary>
		public neusoft.neuFC.Object.neuObject PathogenyKind1 = new neusoft.neuFC.Object.neuObject();
		
		/// <summary>
		/// ��ԭ������
		/// </summary>
		public System.String PathogenyName1
		{
			get{ return this.myPathogenyName1; }
			set{ this.myPathogenyName1 = value; }
		}


		

		/// <summary>
		/// �Ƿ�����
		/// </summary>
		public System.String IsSusceptivity1
		{
			get{ return this.myIsSusceptivity1; }
			set{ this.myIsSusceptivity1 = value; }
		}


		/// <summary>
		/// �Ƿ���ҩ
		/// </summary>
		public System.String IsInaction1
		{
			get{ return this.myIsInaction1; }
			set{ this.myIsInaction1 = value; }
		}


		/// <summary>
		/// �걾2
		/// </summary>
		public neusoft.neuFC.Object.neuObject LabSample2 = new neusoft.neuFC.Object.neuObject();
		

		/// <summary>
		/// ��ԭ������2
		/// </summary>
		public neusoft.neuFC.Object.neuObject PathogenyKind2 = new neusoft.neuFC.Object.neuObject();

		/// <summary>
		/// ��ԭ������
		/// </summary>
		public System.String PathogenyName2
		{
			get{ return this.myPathogenyName2; }
			set{ this.myPathogenyName2 = value; }
		}

		/// <summary>
		/// �Ƿ�����
		/// </summary>
		public System.String IsSusceptivity2
		{
			get{ return this.myIsSusceptivity2; }
			set{ this.myIsSusceptivity2 = value; }
		}

		/// <summary>
		/// �Ƿ���ҩ
		/// </summary>
		public System.String IsInaction2
		{
			get{ return this.myIsInaction2; }
			set{ this.myIsInaction2 = value; }
		}


		/// <summary>
		/// ʹ�ÿ���������
		/// </summary>
		public System.String AntibioticName
		{
			get{ return this.myAntibioticName; }
			set{ this.myAntibioticName = value; }
		}


		/// <summary>
		/// ����������
		/// </summary>
		public System.Int32 AntibioticNum
		{
			get{ return this.myAntibioticNum; }
			set{ this.myAntibioticNum = value; }
		}


		/// <summary>
		/// �Ǽ���
		/// </summary>
		public System.String OperCode
		{
			get{ return this.myOperCode; }
			set{ this.myOperCode = value; }
		}


		/// <summary>
		/// �Ǽ�ʱ��
		/// </summary>
		public System.DateTime OperDate
		{
			get{ return this.myOperDate; }
			set{ this.myOperDate = value; }
		}


		/// <summary>
		/// �Ƿ���Ч
		/// </summary>
		public System.String IsValid
		{
			get{ return this.isValid; }
			set{ this.isValid = value; }
		}

	}
}
