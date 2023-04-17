using System;

namespace neusoft.HISFC.Object.Case
{
	/// <summary>
	/// CasOperationDetail ��ժҪ˵����
	/// </summary>
	public class OperationDetail
	{
		public OperationDetail()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ˽�б���
		//˽���ֶ�
		private string myInpatientNO;
		private string myHappenNO;
		private DateTime myOperationDate;
		private neusoft.neuFC.Object.neuObject myOperationInfo = new neusoft.neuFC.Object.neuObject();
		private string myOperationKind;
		private string myMarcKind;
		private string myNickKind;
		private string myCicaKind;
		private neusoft.neuFC.Object.neuObject myFirDoctInfo = new neusoft.neuFC.Object.neuObject();
		private neusoft.neuFC.Object.neuObject mySecDoctInfo = new neusoft.neuFC.Object.neuObject();
		private neusoft.neuFC.Object.neuObject myThrDoctInfo = new neusoft.neuFC.Object.neuObject();
		private neusoft.neuFC.Object.neuObject myNarcDoctInfo = new neusoft.neuFC.Object.neuObject();
		private neusoft.neuFC.Object.neuObject myFourDoctInfo = new neusoft.neuFC.Object.neuObject();
		private string myOpbOpa;
		private int myBeforeOperDays;
		private string myStatFlag;
		private DateTime myInDate;
		private DateTime myOutDate;
		private DateTime myDeatDate;
		private neusoft.neuFC.Object.neuObject myOperationDeptInfo = new neusoft.neuFC.Object.neuObject();
		private neusoft.neuFC.Object.neuObject myOutDeptInfo = new neusoft.neuFC.Object.neuObject();
		private neusoft.neuFC.Object.neuObject myOutICDInfo = new neusoft.neuFC.Object.neuObject();
		private string mySYNDFlag;
		private DateTime myOperDate;
		private string operType;
		#endregion

		#region ����

		/// <summary>
		/// סԺ��ˮ��
		/// </summary>
		public string InpatientNO 
		{
			get	{ return  myInpatientNO;}
			set	{  myInpatientNO = value; }
		}

		/// <summary>
		/// �������
		/// </summary>
		public string HappenNO 
		{
			get	{ return  myHappenNO;}
			set	{  myHappenNO = value; }
		}

		/// <summary>
		/// ��������
		/// </summary>
		public DateTime OperationDate 
		{
			get	{ return  myOperationDate;}
			set	{  myOperationDate = value; }
		}

		/// <summary>
		/// ������ϢID �������� Name��������
		/// </summary>
		public neusoft.neuFC.Object.neuObject OperationInfo 
		{
			get	{ return  myOperationInfo;}
			set	{  myOperationInfo = value; }
		}

		/// <summary>
		/// ��������
		/// </summary>
		public string OperationKind 
		{
			get	{ return  myOperationKind;}
			set	{  myOperationKind = value; }
		}

		/// <summary>
		/// ����ʽ
		/// </summary>
		public string MarcKind 
		{
			get	{ return  myMarcKind;}
			set	{  myMarcKind = value; }
		}

		/// <summary>
		/// �п�����
		/// </summary>
		public string NickKind 
		{
			get	{ return  myNickKind;}
			set	{  myNickKind = value; }
		}

		/// <summary>
		/// ��������
		/// </summary>
		public string CicaKind 
		{
			get	{ return  myCicaKind;}
			set	{  myCicaKind = value; }
		}

		/// <summary>
		/// ����ҽʦ��Ϣ1 ID ҽ������ Name ҽ������
		/// </summary>
		public neusoft.neuFC.Object.neuObject FirDoctInfo 
		{
			get	{ return  myFirDoctInfo;}
			set	{  myFirDoctInfo = value; }
		}

		/// <summary>
		/// ����ҽʦ��Ϣ2 ID ҽ������ Name ҽ������
		/// </summary>
		public neusoft.neuFC.Object.neuObject FourDoctInfo 
		{
			get	{ return  myFourDoctInfo;}
			set	{  myFourDoctInfo = value; }
		}

		/// <summary>
		/// I��ҽʦ��Ϣ ID ҽ������ Name ҽ������
		/// </summary>
		public neusoft.neuFC.Object.neuObject SecDoctInfo 
		{
			get	{ return  mySecDoctInfo;}
			set	{  mySecDoctInfo = value; }
		}

		/// <summary>
		/// II��ҽʦ��Ϣ ID ҽ������ Name ҽ������
		/// </summary>
		public neusoft.neuFC.Object.neuObject ThrDoctInfo 
		{
			get	{ return  myThrDoctInfo;}
			set	{  myThrDoctInfo = value; }
		}

		/// <summary>
		/// ����ҽʦ��Ϣ
		/// </summary>
		public neusoft.neuFC.Object.neuObject NarcDoctInfo 
		{
			get	{ return  myNarcDoctInfo;}
			set	{  myNarcDoctInfo = value; }
		}

		/// <summary>
		/// ��ǰ_�����
		/// </summary>
		public string OpbOpa 
		{
			get	{ return  myOpbOpa;}
			set	{  myOpbOpa = value; }
		}

		/// <summary>
		/// ��ǰסԺ����
		/// </summary>
		public int BeforeOperDays 
		{
			get	{ return  myBeforeOperDays;}
			set	{  myBeforeOperDays = value; }
		}

		/// <summary>
		/// ͳ�Ʊ�־
		/// </summary>
		public string StatFlag 
		{
			get	{ return  myStatFlag;}
			set	{  myStatFlag = value; }
		}

		/// <summary>
		/// �������
		/// </summary>
		public DateTime InDate 
		{
			get	{ return  myInDate;}
			set	{  myInDate = value; }
		}

		/// <summary>
		/// ��Ժ����
		/// </summary>
		public DateTime OutDate 
		{
			get	{ return  myOutDate;}
			set	{  myOutDate = value; }
		}

		/// <summary>
		/// ��������
		/// </summary>
		public DateTime DeatDate 
		{
			get	{ return  myDeatDate;}
			set	{  myDeatDate = value; }
		}

		/// <summary>
		/// ����������ϢID ���ұ��� Name ��������
		/// </summary>
		public neusoft.neuFC.Object.neuObject OperationDeptInfo 
		{
			get	{ return  myOperationDeptInfo;}
			set	{  myOperationDeptInfo = value; }
		}

		/// <summary>
		/// ��Ժ������Ϣ ID ���ұ��� Name ��������
		/// </summary>
		public neusoft.neuFC.Object.neuObject OutDeptInfo 
		{
			get	{ return  myOutDeptInfo;}
			set	{  myOutDeptInfo = value; }
		}

		/// <summary>
		/// ��Ժ�����ICD��Ϣ ID��Ϣ���� Name �������
		/// </summary>
		public neusoft.neuFC.Object.neuObject OutICDInfo 
		{
			get	{ return  myOutICDInfo;}
			set	{  myOutICDInfo = value; }
		}

		/// <summary>
		/// �Ƿ�ϲ�֢
		/// </summary>
		public string SYNDFlag 
		{
			get	{ return  mySYNDFlag;}
			set	{  mySYNDFlag = value; }
		}

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OperDate 
		{
			get	{ return  myOperDate;}
			set	{  myOperDate = value; }
		}

		/// <summary>
		/// ��������
		/// </summary>
		public string OperType
		{
			get
			{
				return operType;
			}
			set
			{
				operType = value;
			}
		}
		#endregion

		#region ���ú���

		public new OperationDetail Clone()
		{
			OperationDetail OperationDetailClone = base.MemberwiseClone() as OperationDetail;
			
			OperationDetailClone.myFirDoctInfo = this.myFirDoctInfo;
			OperationDetailClone.myNarcDoctInfo = this.myNarcDoctInfo;
			OperationDetailClone.myOperationDeptInfo = this.myOperationDeptInfo;
			OperationDetailClone.myOperationInfo = this.myOperationInfo;
			OperationDetailClone.myOutDeptInfo = this.myOutDeptInfo;
			OperationDetailClone.myOutICDInfo = this.myOutICDInfo;
			OperationDetailClone.mySecDoctInfo = this.mySecDoctInfo;
			OperationDetailClone.myThrDoctInfo = this.myThrDoctInfo;

			return OperationDetailClone;

		}

		#endregion
	}
}
