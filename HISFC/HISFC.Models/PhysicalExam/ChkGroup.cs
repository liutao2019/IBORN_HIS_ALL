namespace Neusoft.HISFC.Object.PhysicalExam {


	/// <summary>
	/// Bank<br></br>
	/// [��������: ����ʵ��]<br></br>
	/// [�� �� ��: ����ȫ]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class ChkGroup : Neusoft.HISFC.Object.Fee.ComGroup
	{
		public ChkGroup()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}                       
//		COMB_NAME    VARCHAR2(40) Y                ��������                       
//		OWN_RATE     NUMBER(10,2) Y                �Żݱ���          
//		PAY_RATE     NUMBER(10,2) Y                �Ը����          
//		PUB_RATE     NUMBER(10,2) Y                ���ѽ��          
//		ECO_RATE     NUMBER(10,2) Y                �Żݽ��          
//		VALID_STATE  VARCHAR2(1)  Y                ͣ�ñ�־   
		/// <summary>
		/// �Ը�����
		/// </summary>
		public decimal OwnRate ; //
		/// <summary>
		/// �Ը�����
		/// </summary>
		public decimal PayRate ; //
		/// <summary>
		/// ���ѱ���
		/// </summary>
		public decimal PubRate ;//
		/// <summary>
		///�Żݱ��� 
		/// </summary>
		public decimal EcoRate ;//
		/// <summary>
		/// �Ƿ��� 
		/// </summary>
		public string ISShare ;

		public new ChkGroup Clone()
		{
			ChkGroup obj = base.Clone() as ChkGroup;
			return obj;
		}
	}
}
