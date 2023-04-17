using System;
using System.Collections;


namespace FS.HISFC.Models.Base
{

	/// <summary>
	/// FS.HISFC.Models.Order.SysClassEnumService<br></br>
	/// [��������: ҽ��������Ŀ���]<br></br>
	/// [�� �� ��: ������]<br></br>
	/// [����ʱ��: 2006-09-01]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class SysClassEnumService: EnumServiceBase 
    {
		static SysClassEnumService()
		{
			items[EnumSysClass.M] = "����ҽ��";
			items[EnumSysClass.MC] = "����";
			items[EnumSysClass.MF] = "��ʳ";
			items[EnumSysClass.MRB] = "ת��";
			items[EnumSysClass.MRD] = "ת��";
			items[EnumSysClass.MRH] = "ԤԼ��Ժ";
			items[EnumSysClass.P] = "��ҩ";
			items[EnumSysClass.PCC] = "�в�ҩ";
			items[EnumSysClass.PCZ] = "�г�ҩ";
			items[EnumSysClass.U] = "��ҩƷ";
			items[EnumSysClass.UC] = "���";
            items[EnumSysClass.UL] = "����";
			items[EnumSysClass.UJ] = "����";
			items[EnumSysClass.UN] = "������";
            //items[EnumSysClass.UO] = FS.FrameWork.Management.Language.Msg("����");
            items[EnumSysClass.UO] = "����";
			items[EnumSysClass.UT] = "����";
			items[EnumSysClass.UZ] = "����";
            items[EnumSysClass.UF] = "����";
            items[EnumSysClass.N] = "����";
		}

		#region ����
			
		/// <summary>
		/// ����ö������
		/// </summary>
		protected static Hashtable items = new Hashtable();

		EnumSysClass enumSysClass;
		
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
		
		protected override System.Enum EnumItem
		{
			get
			{
				return this.enumSysClass;
			}
		}

		#endregion
		
		#region ����

		/// <summary>
		/// ����
		/// </summary>
		protected FS.HISFC.Models.Order.EnumMutex myMutex= FS.HISFC.Models.Order.EnumMutex.None;
		#endregion

		/// <summary>
		/// ����
		/// </summary>
		public FS.HISFC.Models.Order.EnumMutex Mutex
		{
			get
			{
				return this.myMutex;
			}
			set
			{
				this.myMutex = value;
			}
		}

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
		/// ��¡����
		/// </summary>
		/// <returns></returns>
		public new SysClassEnumService Clone()
		{
			return base.Clone() as SysClassEnumService;
		}

		#endregion
	}
	#region ö��

	/// <summary>
	/// 
	/// </summary>
	public enum EnumSysClass
	{
		/// <summary>
		/// ��ҩ
		/// </summary>
		P = 0,
		/// <summary>
		/// �г�ҩ
		/// </summary>
		PCZ = 1,
		/// <summary>
		/// �в�ҩ
		/// </summary>
		PCC = 2,
		/// <summary>
		/// ����
		/// </summary>
		UL = 3,
		/// <summary>
		/// ���
		/// </summary>
		UC = 4,
        /// <summary>
        /// ����
        /// </summary>
        UZ = 5,
		/// <summary>
		/// ����
		/// </summary>
		UO = 6,
        /// <summary>
        /// ����
        /// </summary>
        MC = 7,
        /// <summary>
        /// ��ҩƷ
        /// </summary>
        U = 8,
		/// <summary>
		/// ����
		/// </summary>
		UT = 9,
        /// <summary>
        /// ����
        /// </summary>
        UF= 10,
        /// <summary>
        /// ������
        /// </summary>
        UN = 11,
        /// <summary>
        /// ����ҽ��
        /// </summary>
        M = 12,
        /// <summary>
        /// ����
        /// </summary>
        UJ = 13,
        /// <summary>
        /// ��ʳ
        /// </summary>
        MF = 14,
        /// <summary>
        ///ת�� 
        /// </summary>
        MRD = 15,
        /// <summary>
        /// ת��
        /// </summary>
        MRB = 16,
        /// <summary>
        /// ԤԼ��Ժ
        /// </summary>
        MRH = 17,

        /// <summary>
        /// ����
        /// </summary>
        N=18
    }
    #endregion
}
