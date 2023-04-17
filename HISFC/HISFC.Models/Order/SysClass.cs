using System;
using System.Collections;


namespace Neusoft.HISFC.Object.Order
{
	/// <summary>
	/// SysClass<br></br>
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
	public class SysClass:Neusoft.NFC.Object.NeuObject 
	{

		public SysClass()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		#region ����
	
		/// <summary>
		/// ����ID
		/// </summary>
		private enuSysClass myID;
		/// <summary>
		/// ����
		/// </summary>
		protected enuMutex myMutex= enuMutex.None;
		#endregion

		#region ö��

		/// <summary>
		/// 
		/// </summary>
		public enum enuSysClass
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

		#region ����

		/// <summary>
		/// ����
		/// </summary>
		public new string Name
		{
			get
			{
				string str;
				switch ( (int)this.ID )
				{
					case 0:
						str = "��ҩ";
						break;
					case 1:
						str = "�г�ҩ";
						break;
					case 2:
						str = "�в�ҩ";
						break;
					case 3:
						str = "����ҽ��";
						break;
					case 4:
						str = "����";
						break;
					case 5:
						str = "��ʳ";
						break;
					case 6:
						str = "ת��";
						break;
					case 7:
						str = "ת��";
						break;
					case 8:
						str = "ԤԼ��Ժ";
						break;
					case 9:
						str = "��ҩƷ";
						break;
					case 10:
						str = "����";
						break;
					case 11:
						str = "���";
						break;
					case 12:
						str = "����";
						break;
					case 13:
						str = "����";
						break;
					case 14:
						str = "����";
						break;
					case 15:
						str = "����";
						break;
					case 16:
						str = "����";
						break;
					default:
						str = "����";
						break;
				}
				return	str;
			}
		}


		/// <summary>
		/// ����ID
		/// </summary>
		public new System.Object ID
		{
			get
			{
				return this.myID;
			}
			set
			{
				try
				{
					this.myID = ( this.GetIDFromName( value.ToString() )); 
				}
				catch
				{
					string err = "�޷�ת��" + this.GetType().ToString() + "���룡";
				}
				base.ID = this.myID.ToString();
				base.Name = this.Name;
			}
		}


		/// <summary>
		/// ����
		/// </summary>
		public enuMutex Mutex
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


		#endregion

		#region ����

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns></returns>
		public new SysClass Clone()
		{
			return base.Clone() as SysClass;
		}


		/// <summary>
		/// �������ƻ�ȡ����
		/// </summary>
		/// <param name="Name">����</param>
		/// <returns>����</returns>
		public enuSysClass GetIDFromName( string Name )
		{
			enuSysClass c = new enuSysClass();
			for (int i = 0; i < 100; i++)
			{
				c = (enuSysClass)i;
				if (c.ToString() == Name) return c;
			}
			return (Neusoft.HISFC.Object.Order.SysClass.enuSysClass)int.Parse(Name);
		}


		/// <summary>
		/// ���ȫ���б�
		/// </summary>
		/// <returns>ArrayList(SysClass)</returns>
		public static ArrayList List()
		{
			SysClass o;
			enuSysClass e = new enuSysClass();
			ArrayList alReturn = new ArrayList();
			int i;
			for (i = 0; i <= System.Enum.GetValues( e.GetType() ).GetUpperBound(0); i++)
			{
				//���������ҽ��������ʾ
				if (i == 3)
				{
					continue;
				}

				o = new SysClass();
				o.ID = (enuSysClass)i;
				o.Memo = i.ToString();
				alReturn.Add( o );
			}
			return alReturn;
		}


		#endregion

	}

}
