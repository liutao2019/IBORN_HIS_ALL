using System;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.RADT
{
	/// <summary>
	/// [��������: ѪҺ����ʵ��]<br></br>
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
    public class BloodTypeEnumService: EnumServiceBase	
	{
		/// <summary>
		/// ���캯�� ����ö����������
		/// </summary>
		public BloodTypeEnumService()
		{
            items[EnumBloodTypeByABO.U] = "δ֪";
            items[EnumBloodTypeByABO.A] = "A��";
            items[EnumBloodTypeByABO.B] = "B��";
            items[EnumBloodTypeByABO.AB] = "AB��";
            items[EnumBloodTypeByABO.O] = "O��";
		}

		
		#region ����

        private EnumBloodTypeByABO enumBloodType;

		/// <summary>
		/// ����ö������
		/// </summary>
		protected static Hashtable items = new Hashtable();

		/// <summary>
		/// �Ƿ� RH
		/// </summary>
		private bool bIsRH=false;

		#endregion
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(items)));
        }
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
				return this.enumBloodType;
			}
		}
		/// <summary>
		/// �Ƿ�RHѪ��
		/// </summary>
		public bool RH
		{
			get
			{
				return this.bIsRH;
			}
			set
			{
				this.bIsRH=value;
			}
		}
		
		#endregion
}
}
