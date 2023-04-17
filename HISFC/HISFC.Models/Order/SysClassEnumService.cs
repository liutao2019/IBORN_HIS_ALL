using System;
using System.Collections;


namespace Neusoft.HISFC.Object.Order
{

	/// <summary>
	/// Neusoft.HISFC.Object.Order.SysClassEnumService<br></br>
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
	public class SysClassEnumService:Neusoft.HISFC.Object.Base.EnumServiceBase {

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
			items[EnumSysClass.UJ] = "����";
			items[EnumSysClass.UN] = "������";
			items[EnumSysClass.UO] = "����";
			items[EnumSysClass.UT] = "����";
			items[EnumSysClass.UZ] = "����";
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
		protected EnumMutex myMutex= EnumMutex.None;
		#endregion

		


		/// <summary>
		/// ����
		/// </summary>
		public EnumMutex Mutex
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
		/// ����ҽ��
		/// </summary>
		M = 3,
		/// <summary>
		/// ������
		/// </summary>
		UN = 4,
		/// <summary>
		/// ��ʳ
		/// </summary>
		MF = 5,
		/// <summary>
		///ת�� 
		/// </summary>
		MRD = 6,
		/// <summary>
		/// ת��
		/// </summary>
		MRB = 7,
		/// <summary>
		/// ԤԼ��Ժ
		/// </summary>
		MRH = 8,
		/// <summary>
		/// ����
		/// </summary>
		MC = 14,
		/// <summary>
		/// ��ҩƷ
		/// </summary>
		U = 9,
		/// <summary>
		/// ����
		/// </summary>
		UL = 10,
		/// <summary>
		/// ���
		/// </summary>
		UC = 11,
		/// <summary>
		/// ����
		/// </summary>
		UO = 12,
		/// <summary>
		/// ����
		/// </summary>
		UZ = 13,
		/// <summary>
		/// ����
		/// </summary>
		UJ = 15,
		/// <summary>
		/// ����
		/// </summary>
		UT = 16
			
	}	

		
	#endregion

}
