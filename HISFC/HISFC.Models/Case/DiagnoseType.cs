using System;
using System.Collections;
namespace neusoft.HISFC.Object.Case
{
	/// <summary>
	/// DiagnoseType ��ժҪ˵����
	/// </summary>
	public class DiagnoseType:neusoft.neuFC.Object.neuObject
	{
		public DiagnoseType()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public enum enuDiagnoseType
		{	
			/// <summary>
			/// ��Ҫ���
			/// </summary>
			OUT = 1,
			/// <summary>
			/// �������
			/// </summary>
			OTHER = 2,
			/// <summary>
			/// ����֢���
			/// </summary>
			COMPLICATION = 3,
			/// <summary>
			/// ��Ⱦ���
			/// </summary>
			INFECT = 4,
			/// <summary>
			/// �����ж����
			/// </summary>
			DAMNIFY = 5,
			/// <summary>
			/// �������
			/// </summary>
			PATHOLOGY = 6,
			/// <summary>
			/// �������
			/// </summary>
			SENSITIVE = 7,
			/// <summary>
			/// ����������
			/// </summary>
			BABYDISEASE = 8,
			/// <summary>
			/// ������Ժ��
			/// </summary>
			BABYINFECT = 9,
			/// <summary>
			/// �������
			/// </summary>
			CLINIC = 10,
			/// <summary>
			/// ��Ժ���
			/// </summary>
			IN = 11
			#region 
//			/// <summary>
//			/// ��Ժ���
//			/// </summary>
//			IN = 1,
//			/// <summary>
//			/// ת�����
//			/// </summary>
//			TURNIN = 2,
//			/// <summary>
//			/// ��Ժ���
//			/// </summary>
//			OUT = 3,
//			/// <summary>
//			/// ת�����
//			/// </summary>
//			TURNOUT = 4,
//			/// <summary>
//			/// ȷ�����
//			/// </summary>
//			SURE = 5,
//			/// <summary>
//			/// �������
//			/// </summary>
//			DEAD = 6,
//			/// <summary>
//			/// ��ǰ���
//			/// </summary>
//			OPSFRONT = 7,
//			/// <summary>
//			/// �������
//			/// </summary>
//			OPSAFTER = 8,
//			/// <summary>
//			/// ��Ⱦ���
//			/// </summary>
//			INFECT = 9,
//			/// <summary>
//			/// �����ж����
//			/// </summary>
//			DAMNIFY = 10,
//			/// <summary>
//			/// ����֢���
//			/// </summary>
//			COMPLICATION = 11,
//			/// <summary>
//			/// �������
//			/// </summary>
//			PATHOLOGY = 12,
//			/// <summary>
//			/// �������
//			/// </summary>
//			SAVE = 13,
//			/// <summary>
//			/// ��Σ���
//			/// </summary>
//			FAIL = 14,
//			/// <summary>
//			/// �������
//			/// </summary>
//			CLINIC = 15,
//			/// <summary>
//			/// �������
//			/// </summary>
//			OTHER = 16,
//			/// <summary>
//			/// �������
//			/// </summary>
//			BALANCE = 17
			#endregion 

		};
		/// <summary>
		/// ����ID
		/// </summary>
		private enuDiagnoseType myID;
	
	
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
		
		public enuDiagnoseType GetIDFromName(string Name)
		{
			enuDiagnoseType c=new enuDiagnoseType();
			for(int i=0;i<100;i++)
			{
				c=(enuDiagnoseType)i;
				if(c.ToString()==Name) return c;
			}
			return (neusoft.HISFC.Object.Case.DiagnoseType.enuDiagnoseType)int.Parse(Name);
		}
		/// <summary>
		/// ��������
		/// </summary>
		public new string Name
		{
			get
			{
				string strDiagnoseType = "";

				switch ((int)this.ID)
				{
						#region ��ǰ�� 
//					case 1:
//						strDiagnoseType= "��Ժ���";
//						break;
//					case 2:
//						strDiagnoseType="ת�����";
//						break;
//					case 3:
//						strDiagnoseType="��Ժ���";
//						break;
//					case 4:
//						strDiagnoseType="ת�����";
//						break;
//					case 5:
//						strDiagnoseType="ȷ�����";
//						break;
//					case 6:
//						strDiagnoseType="�������";
//						break;
//					case 7:
//						strDiagnoseType= "��ǰ���";
//						break;
//					case 8:
//						strDiagnoseType="�������";
//						break;
//					case 9:
//						strDiagnoseType="��Ⱦ���";
//						break;
//					case 10:
//						strDiagnoseType="�����ж����";
//						break;
//					case 11:
//						strDiagnoseType="����֢���";
//						break;
//					case 12:
//						strDiagnoseType="�������";
//						break;
//					case 13:
//						strDiagnoseType= "�������";
//						break;
//					case 14:
//						strDiagnoseType="�������";
//						break;
//					case 15:
//						strDiagnoseType="�������";
//						break;
//					case 16:
//						strDiagnoseType="�������";
//						break;
//					default:
//						strDiagnoseType="�������";
//						break;
						#endregion 
					case 1:
						strDiagnoseType="��Ҫ���";
						break;
					case 2:
						strDiagnoseType="�������";
						break;
					case 3:
						strDiagnoseType="����֢���";
						break;
					case 4:
						strDiagnoseType="��Ⱦ���";
						break;
					case 5:
						strDiagnoseType="�����ж����";
						break;
					case 6:
						strDiagnoseType="�������";
						break;
					case 7:
						strDiagnoseType="�������";
						break;
					case 8:
						strDiagnoseType="����������";
						break;
					case 9:
						strDiagnoseType="������Ժ��";
						break;
					case 10:
						strDiagnoseType="�������";
						break;
					case 11:
						strDiagnoseType="��Ժ���";
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
			DiagnoseType aDiagnoseType;
			enuDiagnoseType e=new enuDiagnoseType();
			ArrayList alReturn=new ArrayList();
			int i;
			for(i=1;i<=System.Enum.GetValues(e.GetType()).GetUpperBound(0);i++)
			{
				aDiagnoseType=new DiagnoseType();
				aDiagnoseType.ID=(enuDiagnoseType)i;
				aDiagnoseType.Memo=i.ToString();
				alReturn.Add(aDiagnoseType);
			}
			return alReturn;
		}

		/// <summary>
		/// ���ȫ���б� ���ص�ʵ�� �̳���ISpellCode�ӿ�
		/// </summary>
		/// <returns>ArrayList(DiagnoseType)</returns>
		public static ArrayList SpellList()
		{
			neusoft.HISFC.Object.Base.SpellCode info = null;
			DiagnoseType aDiagnoseType;
			enuDiagnoseType e=new enuDiagnoseType();
			ArrayList alReturn=new ArrayList();
			int i;
			for(i=1;i<=System.Enum.GetValues(e.GetType()).GetUpperBound(0) +1;i++)
			{
				info = new neusoft.HISFC.Object.Base.SpellCode();
				aDiagnoseType=new DiagnoseType();
				aDiagnoseType.ID=i;
				aDiagnoseType.Memo=i.ToString();
				info.ID = i.ToString();
				info.Name = aDiagnoseType.Name; 
				alReturn.Add(info);
			}
			return alReturn;
		}
	}
}
