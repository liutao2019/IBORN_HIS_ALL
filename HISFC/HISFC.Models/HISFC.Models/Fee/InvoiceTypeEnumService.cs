using System;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Fee
{
	/// <summary>
	/// InvoiceTypeEnumService<br></br>
	/// [��������: �վ�(��Ʊ)����ö�ٷ�����]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2006-09-01]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
    ///  />
	/// </summary>
    /// {93E6443C-1FB5-45a7-B89D-F21A92200CF6}
    [Obsolete("����", true)]
	public class InvoiceTypeEnumService : EnumServiceBase
	{
		static InvoiceTypeEnumService( ) 
		{
			items[EnumInvoiceType.R] = "�Һ��վ�";
			items[EnumInvoiceType.C] = "�����վ�";
			items[EnumInvoiceType.I] = "סԺ�վ�";
			items[EnumInvoiceType.P] = "Ԥ���վ�";
            items[EnumInvoiceType.A] = "�����ʻ�";
		}

		#region ����

		/// <summary>
		/// ����ö������
		/// </summary>
		protected static Hashtable items = new Hashtable();
		
		/// <summary>
		/// �վ�(��Ʊ)����
		/// </summary>
		private EnumInvoiceType enumInvoiceType;

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
		/// �վ�(��Ʊ)����
		/// </summary>
		protected override Enum EnumItem
		{
			get
			{
				return this.enumInvoiceType;
			}
		}

		#endregion

		#region ����

		#region ��¡

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>��ǰ�����ʵ������</returns>
		public new InvoiceTypeEnumService Clone()
		{
			return base.Clone() as InvoiceTypeEnumService;
		}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(items)));
        }
		#endregion

		#endregion

	}
	
	#region ö��

	/// <summary>
	/// �վ�(��Ʊ)����
    /// </summary>{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
    [Obsolete("����",true)]
	public enum EnumInvoiceType
	{
		/// <summary>
		/// �Һ��վ�
		/// </summary>
		R=0,

		/// <summary>
		/// �����վ�
		/// </summary>
		C=1,

		/// <summary>
		/// סԺ�վ�
		/// </summary>
		I=2,

		/// <summary>
		/// Ԥ���վ�
		/// </summary>
		P=4,

        /// <summary>
        /// �����ʻ�
        /// </summary>
        A=5
	}

	#endregion

}
