using System;
using System.Collections;
namespace Neusoft.HISFC.Object.RADT
{
	/// <summary>
	/// [��������: ѪҺ����ʵ��]<br></br>
	/// [�� �� ��: ����ΰ]<br></br>
	/// [����ʱ��: 2006-09-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary> 
	[System.Obsolete("�Ѿ����� ��Ϊ EnumBloodType",true)]
	public class BloodType:Neusoft.NFC.Object.NeuObject
	{
		public BloodType()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
/*
		public enum enuBloodType {

			/// <summary>
			/// δ֪
			/// </summary>
			U = 0,
			/// <summary>
			///A 
			/// </summary>
			A=1,
			/// <summary>
			/// B
			/// </summary>
			B=2,
			/// <summary>
			/// AB
			/// </summary>
			AB=3,
			/// <summary>
			/// O
			/// </summary>
			O=4
		};
		
		/// <summary>
		/// ����ID
		/// </summary>
		private EnumBloodType myID;
		private bool bIsRH=false;
		/// <summary>
		/// �Ƿ�RHѪ��
		/// </summary>
		public bool RH
		{
			get
			{
				return this.bIsRH;
			}
			set
			{
				this.bIsRH=value;
			}
		}
		/// <summary>
		/// ABOѪ��
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
					this.myID=(this.GetIDFromName (value.ToString())); 
				}
				catch
				{}
				base.ID=this.myID.ToString();
				string s=this.Name;
			}
		}

		public EnumBloodType GetIDFromName(string Name)
		{
			EnumBloodType c=new EnumBloodType();
			for(int i=0;i<100;i++)
			{
				c=(EnumBloodType)i;
				if(c.ToString()==Name) return c;
			}
			return (Neusoft.HISFC.Object.RADT.EnumBloodType)int.Parse(Name);
		}
		/// <summary>
		/// ��������
		/// </summary>
		public new string Name
		{
			get
			{
				string strBloodType;
				string strRH="RH����";
				
				if(this.bIsRH)
					strRH="RH����";
				else
					strRH="RH����";

				switch ((int)this.ID)
				{
					case 1:
						strBloodType= "A��"+strRH;
						break;
					case 2:
						strBloodType="B��"+strRH;
						break;
					case 3:
						strBloodType="AB��"+strRH;
						break;
					case 4:
						strBloodType="O��"+strRH;
						break;
					 default:
						strBloodType="δ֪";
						break;

				}
				base.Name=strBloodType;
				return	strBloodType;
			}
		}
		/// <summary>
		/// ���ȫ���б�
		/// </summary>
		/// <returns>ArrayList(BloodType)</returns>
		public static ArrayList List()
		{
			BloodType aBloodType;
			EnumBloodType e=new EnumBloodType();
			ArrayList alReturn=new ArrayList();
			int i;
			for(i=0;i<=System.Enum.GetValues(e.GetType()).GetUpperBound(0);i++)
			{
				aBloodType=new BloodType();
				aBloodType.ID=(EnumBloodType)i;
				aBloodType.Memo=i.ToString();
				alReturn.Add(aBloodType);
			}
			return alReturn;
		}
		public new BloodType Clone()
		{
			return base.Clone() as BloodType;
		}
		*/
	}
}
