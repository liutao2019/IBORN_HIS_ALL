using System;
using System.Collections;
namespace Neusoft.HISFC.Object.Base
{
	/// <summary>
	/// Sequence<br></br>
	/// [��������: ϵͳ���ʵ��]<br></br>
	/// [�� �� ��: ����ΰ]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class SysClass:Neusoft.NFC.Object.NeuObject 
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public SysClass()
		{
		}

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
			/// ����
			/// </summary>
			MN = 4,
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
			UC = 11
			
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
				switch ((int)this.ID)
				{
					case 0:
						str=" ��ҩ";
						break;
					case 1:
						str="�г�ҩ";
						break;
					case 2:
						str="�в�ҩ";
						break;
					case 3:
						str="����ҽ��";
						break;
					case 4:
						str="����";
						break;
					case 5:
						str="��ʳ";
						break;
					case 6:
						str="ת�� ";
						break;
					case 7:
						str="ת��";
						break;
					case 8:
						str="ԤԼ��Ժ";
						break;
					case 9:
						str="��ҩƷ";
						break;
					case 10:
						str="����";
						break;
					case 11:
						str="���";
						break;
					default:
						str="����";
						break;
				}
				base.Name=str;
				return	str;
			}
		}

		/// <summary>
		/// ����ID
		/// </summary>
		private enuSysClass myID;
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
					this.myID=(this.GetIDFromName (value.ToString())); 
				}
				catch
				{
					string err="�޷�ת��"+this.GetType().ToString()+"���룡";
				}
				base.ID=this.myID.ToString();
				string s=this.Name;
			}
		}
		#endregion

		#region ����

		#region ��¡
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>SpellCode��ʵ��</returns>
		public new SysClass Clone()
		{
			return this.MemberwiseClone() as SysClass;
		}

		#endregion

		#region ��������
		/// <summary>
		/// ��������
		/// </summary>
		public enuSysClass GetIDFromName(string Name)
		{
			enuSysClass c=new enuSysClass();
			for(int i=0;i<100;i++)
			{
				c=(enuSysClass)i;
				if(c.ToString()==Name) return c;
			}
			return (Neusoft.HISFC.Object.Base.SysClass.enuSysClass)int.Parse(Name);
		}
		#endregion

		#region ��ȡȫ���б�
		/// <summary>
		/// ���ȫ���б�
		/// </summary>
		/// <returns>ArrayList(SysClass)</returns>
		public static ArrayList List()
		{
			SysClass o;
			enuSysClass e=new enuSysClass();
			ArrayList alReturn=new ArrayList();
			int i;
			for(i=0;i<=System.Enum.GetValues(e.GetType()).GetUpperBound(0);i++)
			{
				o=new SysClass();
				o.ID=(enuSysClass)i;
				o.Memo=i.ToString();
				alReturn.Add(o);
			}
			return alReturn;
		}
		#endregion

		#endregion

		
	}
	
}
