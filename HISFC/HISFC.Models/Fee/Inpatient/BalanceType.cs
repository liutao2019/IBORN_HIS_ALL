using System;

namespace Neusoft.HISFC.Object.Fee.Inpatient
{
	/// <summary>
	/// BalanceType�������͡�
	///  ��Ժ���� I
	///  ת�ƽ��� R
	///  ��Ժ���� O
	///  �ؽ��� M
	///  ��ת���� S
	/// </summary>
	public class BalanceType:Neusoft.NFC.Object.NeuObject
	{
		public BalanceType()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public enum enuBalanceType
		{
			/// <summary>
			/// ��;���� I
			/// </summary>
			I,
			/// <summary>
			/// ��Ժ���� O
			/// </summary>
			O,
			/// <summary>
			/// ֱ�ӽ���
			/// </summary>
			D,
			/// <summary>
			/// ��ת����
			/// </summary>
			S
		};
		private enuBalanceType myID;
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
		public enuBalanceType GetIDFromName(string Name)
		{
			enuBalanceType c=new enuBalanceType();
			for(int i=0;i<100;i++)
			{
				c=(enuBalanceType)i;
				if(c.ToString()==Name) return c;
			}
			return (enuBalanceType)int.Parse(Name);
		}
		
		public new string Name
		{
			get
			{
				string strBalanceType;
				switch ((int)this.ID)
				{
					case 0:
						strBalanceType= "��;����";
						break;
					case 1:
						strBalanceType="��Ժ����";
						break;
					case 2:
						strBalanceType="ֱ�ӽ���";
						break;
					case 3:
						strBalanceType = "��ת����";
						break;
					default:
						strBalanceType="��;����";
						break;
				}
				base.Name=strBalanceType;
				return	strBalanceType;
			}
		}
		/// <summary>
		/// ���ȫ���б�
		/// </summary>
		/// <returns>ArrayList(BalanceType)</returns>
		public System.Collections.ArrayList List()
		{
			BalanceType aBalanceType;
			System.Collections.ArrayList alReturn=new System.Collections.ArrayList();
			int i;
			for(i=0;i<=4;i++)
			{
				aBalanceType=new BalanceType();
				aBalanceType.ID=(enuBalanceType)i;
				alReturn.Add(aBalanceType);
			}
			return alReturn;
		}
		public override string ToString()
		{
			return this.Name;
		}

		public new BalanceType Clone()
		{
			return base.Clone() as BalanceType;
		}

	}
}
