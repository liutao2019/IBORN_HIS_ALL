using System;
using System.Collections;
namespace Neusoft.HISFC.Object.RADT
{
	/// <summary>
	/// [��������: �������ʵ��]<br></br>
	/// [�� �� ��: ����ΰ]<br></br>
	/// [����ʱ��: 2006-09-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary> 
	public class DiagnoseType:Neusoft.NFC.Object.NeuObject
	{
		public DiagnoseType()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/*
		[System.Obsolete("��ö�ٸ�ΪEnumDiagnoseType",true)]
		public enum enuDiagnoseType
		{	
			/// <summary>
			/// ��Ժ���
			/// </summary>
			IN = 1,
			/// <summary>
			/// ת�����
			/// </summary>
			TURNIN = 2,
			/// <summary>
			/// ��Ժ���
			/// </summary>
			OUT = 3,
			/// <summary>
			/// ת�����
			/// </summary>
			TURNOUT = 4,
			/// <summary>
			/// ȷ�����
			/// </summary>
			SURE = 5,
			/// <summary>
			/// �������
			/// </summary>
			DEAD = 6,
			/// <summary>
			/// ��ǰ���
			/// </summary>
			OPSFRONT = 7,
			/// <summary>
			/// �������
			/// </summary>
			OPSAFTER = 8,
			/// <summary>
			/// ��Ⱦ���
			/// </summary>
			INFECT = 9,
			/// <summary>
			/// �����ж����
			/// </summary>
			DAMNIFY = 10,
			/// <summary>
			/// ����֢���
			/// </summary>
			COMPLICATION = 11,
			/// <summary>
			/// �������
			/// </summary>
			PATHOLOGY = 12,
			/// <summary>
			/// �������
			/// </summary>
			SAVE = 13,
			/// <summary>
			/// ��Σ���
			/// </summary>
			FAIL = 14,
			/// <summary>
			/// �������
			/// </summary>
			CLINIC = 15,
			/// <summary>
			/// �������
			/// </summary>
			OTHER = 16,
			/// <summary>
			/// �������
			/// </summary>
			BALANCE = 17

		};
		/// <summary>
		/// ����ID
		/// </summary>
		private EnumDiagnoseType myID;
	
	
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
		
		public EnumDiagnoseType GetIDFromName(string Name)
		{
			EnumDiagnoseType c=new EnumDiagnoseType();
			for(int i=0;i<100;i++)
			{
				c=(EnumDiagnoseType)i;
				if(c.ToString()==Name) return c;
			}
			return (Neusoft.HISFC.Object.RADT.EnumDiagnoseType)int.Parse(Name);
		}
		/// <summary>
		/// ��������
		/// </summary>
		public new string Name
		{
			get
			{
				string strDiagnoseType;

				switch ((int)this.ID)
				{
					case 1:
						strDiagnoseType= "��Ժ���";
						break;
					case 2:
						strDiagnoseType="ת�����";
						break;
					case 3:
						strDiagnoseType="��Ժ���";
						break;
					case 4:
						strDiagnoseType="ת�����";
						break;
					case 5:
						strDiagnoseType="ȷ�����";
						break;
					case 6:
						strDiagnoseType="�������";
						break;
					case 7:
						strDiagnoseType= "��ǰ���";
						break;
					case 8:
						strDiagnoseType="�������";
						break;
					case 9:
						strDiagnoseType="��Ⱦ���";
						break;
					case 10:
						strDiagnoseType="�����ж����";
						break;
					case 11:
						strDiagnoseType="����֢���";
						break;
					case 12:
						strDiagnoseType="�������";
						break;
					case 13:
						strDiagnoseType= "�������";
						break;
					case 14:
						strDiagnoseType="�������";
						break;
					case 15:
						strDiagnoseType="�������";
						break;
					case 16:
						strDiagnoseType="�������";
						break;
					default:
						strDiagnoseType="�������";
						break;
				}
				base.Name=strDiagnoseType;
				return	strDiagnoseType;
			}
		}
		/// <summary>
		/// ���ȫ���б�
		/// </summary>
		/// <returns>ArrayList(DiagnoseType)</returns>
		public static ArrayList List()
		{
			EnumDiagnoseType aDiagnoseType;
			EnumDiagnoseType e=new EnumDiagnoseType();
			ArrayList alReturn=new ArrayList();
			int i;
			for(i=0;i<=System.Enum.GetValues(e.GetType()).GetUpperBound(0);i++)
			{
				aDiagnoseType=new EnumDiagnoseType();
				aDiagnoseType.ID=(EnumDiagnoseType)i;
				aDiagnoseType.Memo=i.ToString();
				alReturn.Add(aDiagnoseType);
			}
			return alReturn;
		}
		*/
	}
}
