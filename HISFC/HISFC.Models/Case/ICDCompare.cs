using System;
using neusoft.neuFC.Object;
namespace neusoft.HISFC.Object.Case
{
	/// <summary>
	/// ICDCompare ��ժҪ˵����ICD9,ICD10����ά�� 
	/// </summary>
	public class ICDCompare : neuObject 
	{
		public ICDCompare()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region  ˽�б���
		private  ICD icd10  = new ICD(); //����ICD10��Ϣ
		private  ICD icd9 = new ICD();  //����ICD9��Ϣ
		private  neuObject operInfo = new neuObject();  //����Ա��Ϣ, ID ���� Name ����
		private bool isValid ; //��Ч�Ա�ʶ
		private  DateTime operDate  ;                   //¼��ʱ��
		#endregion

		#region  ��������
		public  ICD ICD10  
		{
			//����ICD10��Ϣ
			get
			{
				return icd10;
			}
			set
			{
				icd10 = value;
			}
		}
		public ICD ICD9
		{
			//����ICD9��Ϣ
			get
			{
				return icd9 ;
			}
			set
			{
				icd9 = value; 
			}
		}
		public neuObject OperInfo 
		{
			//������ ��Ϣ
			get
			{
				return operInfo;
			}
			set
			{
				operInfo = value;
			}
		}
		public DateTime OperDate
		{
			//����ʱ��
			get
			{
				return operDate;
			}
			set
			{
				operDate = value;
			}
		}
		public bool IsValid
		{
			//��Ч�Ա�ʶ
			get
			{
				return isValid;
			}
			set
			{
				isValid = value; 
			}
		}
		#endregion
 
		#region  
		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns></returns>
		public new ICDCompare Clone()
		{
			//��¡����
			ICDCompare  icdCompare = base.Clone() as ICDCompare ; // ��¡����
			icdCompare.operInfo = operInfo.Clone(); //��¡
			icdCompare.icd10 = icd10.Clone(); //��¡CD10��Ϣ ����Ա��Ϣ
			icdCompare.icd9 = icd9.Clone(); //��¡CD9��Ϣ
			return icdCompare; 
		}
		#endregion 
	}
}
