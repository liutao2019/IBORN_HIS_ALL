using System;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.RADT
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
    [Serializable]
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList(items.Values));
        }
		
	}

}
