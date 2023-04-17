using System;
 
using System.Collections;
namespace Neusoft.HISFC.Object.RADT
{
	/// <summary>
	/// �Ա��� written by wolf 
	/// 2004-5 
	/// </summary>
	/// <example> ���ʹ��list����
	/// <code>
	///     Dim c As New Neusoft.HISFC.Object.RADT.Patient
	///		c.Sex.ID = RADT.Sex.enuSex.F
	///		c.Sex.ID = 0
	///
	///		MsgBox(c.Sex.Name)
	///
	///		Dim i As Int16
	///		Dim a As New Neusoft.HISFC.Object.RADT.Sex
	///		Dim b As New ArrayList
	///		b = a.List
	///		For i = 0 To b.Count - 1
	///			MsgBox(CType(b(i), RADT.Sex).Name.ToString)
	///		Next
	/// </code>
	/// </example>
	[Obsolete("�Ѿ����ڣ�����ΪEnumSex")]
	public class Sex:Neusoft.NFC.Object.NeuObject
	{
		/// <summary>
		/// �Ա���
		/// </summary>
		public Sex()
		{
		}
		public enum enuSex
		{
			/// <summary>
			/// Male
			/// </summary>
			M=1,
			/// <summary>
			/// female
			/// </summary>
			F=2,
			/// <summary>
			/// other
			/// </summary>
			O=3,
			/// <summary>
			/// unknow
			/// </summary>
			U=0
		};
		
		/// <summary>
		/// ����ID
		/// </summary>
		private enuSex myID;
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
					{}
					base.ID=this.myID.ToString();
					string s=this.Name;
			}
		}
		public enuSex GetIDFromName(string Name)
		{
			enuSex c=new enuSex();
			for(int i=0;i<100;i++)
			{
				c=(enuSex)i;
				if(c.ToString()==Name) return c;
			}
			return (Neusoft.HISFC.Object.RADT.Sex.enuSex)int.Parse(Name);
		}
		/// <summary>
		/// �����Ա�����
		/// </summary>
		public new string Name
		{
			get
			{
				string strSex;
				switch ((int)this.ID)
				{
					case 1:
						strSex= "��";
						break;
					case 2:
						strSex="Ů";
						break;
					case 3:
						strSex="����";
						break;
					default:
						strSex="δ֪";
						break;

				}
				base.Name=strSex;
				return	strSex;
			}
		}
		/// <summary>
		/// ����Ա�ȫ���б�
		/// </summary>
		/// <returns>ArrayList(Sex)</returns>
		public static ArrayList List()
		{
			Sex aSex;
			enuSex e=new enuSex();
			ArrayList alReturn=new ArrayList();
			int i;
			for(i=0;i<=System.Enum.GetValues(e.GetType()).GetUpperBound(0);i++)
			{
				aSex=new Sex();
				aSex.ID=(enuSex)i;
				aSex.Memo=i.ToString();
				alReturn.Add(aSex);
			}
			return alReturn;
		}
		public new Sex Clone()
		{
			return this.MemberwiseClone() as Sex;
		}
	}
}
