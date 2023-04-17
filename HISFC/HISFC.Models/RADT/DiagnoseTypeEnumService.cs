using System;
using System.Collections;
using Neusoft.HISFC.Object.Base;

namespace Neusoft.HISFC.Object.RADT
{
	/// <summary>
	/// [��������: �������ʵ��]<br></br>
	/// [�� �� ��: ����ΰ]<br></br>
	/// [����ʱ��: 2006-09-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary> 
	public class DiagnoseTypeEnumService:EnumServiceBase	
	{
		public DiagnoseTypeEnumService()
		{
			items[EnumDiagnoseType.IN] = "��Ժ���";
			items[EnumDiagnoseType.TURNIN] = "ת�����";
			items[EnumDiagnoseType.OUT] = "��Ժ���";
			items[EnumDiagnoseType.TURNOUT] = "ת�����";
			items[EnumDiagnoseType.SURE] = "ȷ�����";
			items[EnumDiagnoseType.DEAD] = "�������";
			items[EnumDiagnoseType.OPSAFTER] = "��ǰ��";
			items[EnumDiagnoseType.INFECT] = "�������";
			items[EnumDiagnoseType.DAMNIFY] = "��Ⱦ���";
			items[EnumDiagnoseType.DAMNIFY] ="�����ж����";
			items[EnumDiagnoseType.COMPLICATION] = "����֢���";
			items[EnumDiagnoseType.PATHOLOGY] = "�������";
			items[EnumDiagnoseType.SAVE] = "�������";
			items[EnumDiagnoseType.FAIL] = "��Σ���";
			items[EnumDiagnoseType.CLINIC] = "�������";
			items[EnumDiagnoseType.OTHER] = "�������";
			items[EnumDiagnoseType.BALANCE] = "�������";
		}

		#region ����

        protected EnumDiagnoseType enuDimagnoseType;
		/// <summary>
		/// ����ö������
		/// </summary>
		protected static Hashtable items = new Hashtable();

		#endregion

		#region ����

		/// <summary>
		/// ����ö������
		/// </summary>
		protected override Hashtable Items
		{
			get
			{
				return items;
			}
		}
		
		protected override Enum EnumItem
		{
			get
			{
				return this.enuDimagnoseType;
			}
		}

		#endregion  	
		
	}
//	#region �������ö��
//
//	public enum EnumDiagnoseType
//	{	
//		/// <summary>
//		/// ��Ժ���
//		/// </summary>
//		IN = 1,
//		/// <summary>
//		/// ת�����
//		/// </summary>
//		TURNIN = 2,
//		/// <summary>
//		/// ��Ժ���
//		/// </summary>
//		OUT = 3,
//		/// <summary>
//		/// ת�����
//		/// </summary>
//		TURNOUT = 4,
//		/// <summary>
//		/// ȷ�����
//		/// </summary>
//		SURE = 5,
//		/// <summary>
//		/// �������
//		/// </summary>
//		DEAD = 6,
//		/// <summary>
//		/// ��ǰ���
//		/// </summary>
//		OPSFRONT = 7,
//		/// <summary>
//		/// �������
//		/// </summary>
//		OPSAFTER = 8,
//		/// <summary>
//		/// ��Ⱦ���
//		/// </summary>
//		INFECT = 9,
//		/// <summary>
//		/// �����ж����
//		/// </summary>
//		DAMNIFY = 10,
//		/// <summary>
//		/// ����֢���
//		/// </summary>
//		COMPLICATION = 11,
//		/// <summary>
//		/// �������
//		/// </summary>
//		PATHOLOGY = 12,
//		/// <summary>
//		/// �������
//		/// </summary>
//		SAVE = 13,
//		/// <summary>
//		/// ��Σ���
//		/// </summary>
//		FAIL = 14,
//		/// <summary>
//		/// �������
//		/// </summary>
//		CLINIC = 15,
//		/// <summary>
//		/// �������
//		/// </summary>
//		OTHER = 16,
//		/// <summary>
//		/// �������
//		/// </summary>
//		BALANCE = 17
//
//	};
//	#endregion
}
