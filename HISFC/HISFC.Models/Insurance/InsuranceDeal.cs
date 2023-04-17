using System;
using System.Collections;
using System.Windows.Forms ;
using neusoft.neuFC.Object;
using neusoft.HISFC.Object;
namespace neusoft.HISFC.Object.Insurance
{
	/// <summary>
	/// InsuranceDeal ҽ������ά����
	/// </summary>
//	PACT_CODE	VARCHAR2(4)	N		��ͬ��λ
//	INPRO_STAUS	VARCHAR2(14)	N		��ְ״̬ 0 ��ְ 1 ������
//	PART_ID	NUMBER(1)	N		�ֶ����
//	STANDARD_COST	NUMBER(10)	N		����
//	SERVANT_SUBSIDY	NUMBER(10,2)	Y		����Ա����
//	SPECIAL_SUBSIDY	NUMBER(10,2)	Y		�Ϻ������
//	EXCEED_SUBSIDY	NUMBER(10,2)	Y		����
//	BEGIN_COST	NUMBER(10,2)	Y		���俪ʼ
//	END_COST	NUMBER(10,2)	Y		�������
//	OPER_CODE	VARCHAR2(6)	N		����Ա
//	OPER_DATE	DATE	N		��������
	public class InsuranceDeal:neusoft.neuFC.Object.neuObject
	{
		public InsuranceDeal()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ��ͬ����
		/// </summary>
		public neuObject PactCode = new neuObject();
		/// <summary>
		/// ��ְ״̬
		/// </summary>
		public neuObject InproStaus = new neuObject();
		/// <summary>
		/// �ֶ����
		/// </summary>
		public decimal PartID;
		/// <summary>
		/// ����
		/// </summary>
		public decimal StandardCost;
		/// <summary>
		/// ����Ա����
		/// </summary>
		public decimal ServantSubsidy;
		/// <summary>
		/// �Ϻ������
		/// </summary>
		public decimal SpecialSubsidy;
		/// <summary>
		/// ����
		/// </summary>
		public decimal ExceedSubsidy;
		/// <summary>
		/// ���俪ʼ
		/// </summary>
		public decimal BeginCost;
		/// <summary>
		/// �������
		/// </summary>
		public decimal EndCost;
	}
}
