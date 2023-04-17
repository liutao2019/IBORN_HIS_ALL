using System;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Fee
{
	/// <summary>
	/// ReportTypeEnumService<br></br>
	/// [��������: ͳ�ƴ������ö��]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2006-09-14]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    /// 
    [System.Serializable]
	public class ReportTypeEnumService : EnumServiceBase
	{
		static ReportTypeEnumService() 
		{
			items[EnumReportType.FP] = "��Ʊ";
			items[EnumReportType.TJ] = "ͳ��";
			items[EnumReportType.BA] = "����";
			items[EnumReportType.ZQ] = "֪��Ȩ";
		}

		#region ����

		/// <summary>
		/// ����ö������
		/// </summary>
		protected static Hashtable items = new Hashtable();
		
		/// <summary>
		/// ��������
		/// </summary>
		private EnumReportType enumReportType;

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
		/// ͳ�ƴ�������
		/// </summary>
		protected override Enum EnumItem
		{
			get
			{
				return this.enumReportType;
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
	
	#region ö��

	/// <summary>
	/// ͳ�ƴ�������
	/// </summary>
	public enum EnumReportType
	{
		/// <summary>
		/// ��Ʊ
		/// </summary>
		FP = 0,

		/// <summary>
		/// ͳ��
		/// </summary>
		TJ = 1,

		/// <summary>
		/// ����
		/// </summary>
		BA = 2,

		/// <summary>
		/// ֪��Ȩ
		/// </summary>
		ZQ = 3
	}

	#endregion
}
