using System.Collections;

namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// EmployeeTypeEnumService <br></br>
	/// [��������: ��Ա���ö�ٷ���ʵ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class EmployeeTypeEnumService : EnumServiceBase
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public EmployeeTypeEnumService()
		{
			this.Items[EnumEmployeeType.O] = "����";
			this.Items[EnumEmployeeType.D] = "ҽ��";
			this.Items[EnumEmployeeType.N] = "��ʿ";
			this.Items[EnumEmployeeType.F] = "�տ�Ա";
			this.Items[EnumEmployeeType.P] = "ҩʦ";
			this.Items[EnumEmployeeType.T] = "��ʦ";
			this.Items[EnumEmployeeType.C] = "��ʦ";
		}

		#region ����

		/// <summary>
		/// �������
		/// </summary>
		EnumEmployeeType enumEmployeeType;

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
		protected override System.Enum EnumItem
		{
			get
			{
				return this.enumEmployeeType;
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

	/// <summary>
	/// ��Ա���
	/// </summary>
	public enum EnumEmployeeType
	{
		/// <summary>
		///����:0
		/// </summary>
		O=0,
		/// <summary>
		///ҽ��:1
		/// </summary>
		D=1,
		/// <summary>
		///��ʿ:2
		/// </summary>
		N=2,
		/// <summary>
		///�տ�Ա:3
		/// </summary>
		F=3,
		/// <summary>
		///ҩʦ:4
		/// </summary>
		P=4,
		/// <summary>
		///��ʦ:5
		/// </summary>
		T=5,
		/// <summary>
		///��ʦ:6
		/// </summary>
		C=6
	};
}
