using System;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.RADT
{
	/// <summary>
	/// [��������: ����״̬ʵ��]<br></br>
	/// [�� �� ��: ���Ʒ�]<br></br>
	/// [����ʱ��: 2006-09-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���='����ΰ'
	///		�޸�ʱ��='2006-9-12'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary> 
    [Serializable]
    public class MaritalStatusEnumService :EnumServiceBase
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public MaritalStatusEnumService()
		{
			items[EnumMaritalStatus.S] = "δ��";
			items[EnumMaritalStatus.M] = "�ѻ�";
			items[EnumMaritalStatus.D] = "ʧ��";
			items[EnumMaritalStatus.R] = "�ٻ�";
			items[EnumMaritalStatus.A] = "�־�";
			items[EnumMaritalStatus.W] = "ɥż";
		}
		
		
		#region ����

		private EnumMaritalStatus enumMaritalStatus;
		
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
				return this.enumMaritalStatus;
			}
		}

		#endregion  	
		
		#region ����
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(items)));
        }

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new MaritalStatusEnumService Clone()
        {
            return base.Clone() as MaritalStatusEnumService;
        }

		#endregion

	}
}
