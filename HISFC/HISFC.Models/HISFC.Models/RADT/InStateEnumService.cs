using System;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.RADT
{
	/// <summary>
	/// InStateEnumService <br></br>
	/// [��������: סԺ������Ժ״̬ö�ٷ���ʵ��]<br></br>
	/// [����ʱ��: 2006-09-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���='����ΰ'
	///		�޸�ʱ��='2009-9-13'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
	public class InStateEnumService : EnumServiceBase
	{
        //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
		public InStateEnumService()
		{
			this.Items[EnumInState.R] = "סԺ�Ǽ����,�ȴ�����";
			this.Items[EnumInState.I] = "�����������,��Ժ״̬";
			this.Items[EnumInState.B] = "��Ժ�Ǽ����,����״̬";
			this.Items[EnumInState.O] = "��Ժ�������";
			this.Items[EnumInState.P] = "ԤԼ��Ժ";
			this.Items[EnumInState.N] = "�޷���Ժ";
			this.Items[EnumInState.C] = "����״̬";
            this.Items[EnumInState.E] = "תסԺ";
		}

		#region ����

		/// <summary>
		/// �������
		/// </summary>
		EnumInState enumInState;

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
				return this.enumInState;
			}
		}

		#endregion
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(items)));
        }
	}

	
}
