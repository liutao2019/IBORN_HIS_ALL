using System;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// SexEnumService <br></br>
	/// [��������: �Ա�ö�ٷ���ʵ��]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-09-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class SexEnumService : EnumServiceBase
	{
		public SexEnumService()
		{
			this.Items[EnumSex.U] = "δ֪";
			this.Items[EnumSex.M] = "��";
			this.Items[EnumSex.F] = "Ů";
			this.Items[EnumSex.O] = "����";
            this.Items[EnumSex.A] = "ȫ��";
		}

		#region ����

		/// <summary>
		/// �������
		/// </summary>
		EnumSex enumSex;

		/// <summary>
		/// �洢ö�ٶ���
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
		
		/// <summary>
		/// ö����Ŀ
		/// </summary>
		protected override Enum EnumItem
		{
			get
			{
				return this.enumSex;
			}
		}

		#endregion

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new SexEnumService Clone()
		{
			SexEnumService sexEnumService = base.Clone() as SexEnumService;
            
			return sexEnumService;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList( GetObjectItems(items)));
        }
	}

	
}
