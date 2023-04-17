using System;
using System.Collections;

namespace Neusoft.HISFC.Object.Base
{
	/// <summary
	/// Week<br></br>
	/// [��������: ����ʵ��]<br></br>
	/// [�� �� ��: ����ΰ]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class Week:Neusoft.NFC.Object.NeuObject,Neusoft.HISFC.Object.Base.ISpell 
	{
		#region ö��
		public enum enuWeek
		{
			/// <summary>
			///һ 
			/// </summary>
			һ=1,
			/// <summary>
			/// ��
			/// </summary>
			��=2,
			/// <summary>
			/// ��
			/// </summary>
			��=3,
			/// <summary>
			/// ��
			/// </summary>
			��=4,
			/// <summary>
			/// ��
			/// </summary>
			��=5,
			/// <summary>
			/// ��
			/// </summary>
			��=6,
			/// <summary>
			/// ��
			/// </summary>
			��=7

		};
		
		/// <summary>
		/// ����ID
		/// </summary>
		private enuWeek weekID;
		#endregion

		#region ����
		/// <summary>
		/// ABOѪ��
		/// </summary>
		public new System.Object ID
		{
			get
			{
				return this.weekID;
			}
			set
			{
				try
				{
					this.weekID=(this.GetIDFromName (value.ToString())); 
				}
				catch
				{}
				base.ID=this.weekID.ToString();
				string s=this.Name;
			}
		}
		
	
		/// <summary>
		/// ��������
		/// </summary>
		public new string Name
		{
			get
			{				
				string strWeek = "";
				switch ((int)this.ID)
				{
					case 1:
						strWeek= "һ";
						break;
					case 2:
						strWeek="��";
						break;
					case 3:
						strWeek="��";
						break;
					case 4:
						strWeek="��";
						break;
					case 5:
						strWeek="��";
						break;
					case 6:
						strWeek="��";
						break;
					case 0:
						strWeek="��";
						break;
					default:
						strWeek="δ֪";
						break;

				}
				base.Name=strWeek;
				return	strWeek;
			}
		}
		#region ISpellCode ��Ա

		public string Spell_Code
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public string WB_Code
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public string User_Code
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		#endregion
		#endregion

	    #region ����
		public enuWeek GetIDFromName(string Name)
		{
			enuWeek c=new enuWeek();
			for(int i=0;i<100;i++)
			{
				c=(enuWeek)i;
				if(c.ToString()==Name) return c;
			}
			return (enuWeek)int.Parse(Name);
		}

		/// <summary>
		/// ���ȫ���б�
		/// </summary>
		/// <returns>ArrayList(BloodType)</returns>
		public static ArrayList List()
		{
			Week aWeek;
			enuWeek e=new enuWeek();
			ArrayList alReturn=new ArrayList();
			int i;
			for(i=0;i<=System.Enum.GetValues(e.GetType()).GetUpperBound(0);i++)
			{
				aWeek=new Week();
				aWeek.ID=(enuWeek)i;
				aWeek.Memo=i.ToString();
				alReturn.Add(aWeek);
			}
			return alReturn;
		}
	
        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
		public new Week Clone()
		{
			return base.Clone() as Week;
		}
		#endregion
	}
}
