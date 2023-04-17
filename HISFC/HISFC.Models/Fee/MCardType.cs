using System;
using System.Data;
using System.Collections;

namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// ҽ��֤�� ��ժҪ˵����
	/// </summary>
	public class MCardType:Neusoft.NFC.Object.NeuObject
	{
		public MCardType()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}		


		#region ����ҽ�ƿ�����ϸ
		public enum enuMCardType {
			/// <summary>
			/// ����
			/// </summary>
			//("����"),Display("����")]
			TT=1,
			/// <summary>
			/// ��Ժְ��
			/// </summary>
			//("��ͨ"),Display("��Ժְ��")]
			BYZG=2,
			/// <summary>
			/// ��Ժ����(�ϰ���)
			/// </summary>
			//("��ͨ"),Display("��Ժ����")]
			BYJS1=3,
			/// <summary>
			///��Ժ����(�°���)
			/// </summary>
			//("��ͨ"),Display("��Ժ����")]
			BYJS2=4,
			/// <summary>
			/// ��Ժ����(ȫ��)
			/// </summary>
			//("��ͨ"),Display("��Ժ����")]
			BYJS=5,
			/// <summary>
			/// ��Ժ����
			/// </summary>
			//("��ͨ"),Display("��Ժ����")]
			BYTX=6,
			/// <summary>
			/// ��Ժ����
			/// </summary>
			//("��ͨ"),Display("��Ժ����")]
			BYLX=7,
			/// <summary>
			/// ʡ��ҽ�ɲ�
			/// </summary>
			//("ʡ��ҽ"),Display("ʡ��ҽ�ɲ�")]
			SGYXX_J80=8,
			/// <summary>
			/// ʡ��ҽ�ɲ�
			/// </summary>
			//("ʡ��ҽ"),Display("ʡ��ҽ�ɲ�")]
			SGYXX_J81=9,
			/// <summary>
			///ʡ��ҽ�ɲ�
			/// </summary>
			//("ʡ��ҽ"),Display("ʡ��ҽ�ɲ�")]
			SGYXX_J82=10,
			/// <summary>
			/// ʡ��ҽ�ɲ�
			/// </summary>
			//("ʡ��ҽ"),Display("ʡ��ҽ�ɲ�")]
			SGYXX_J83=11,
			/// <summary>
			/// ʡ��ҽ�ɲ�
			/// </summary>
			//("ʡ��ҽ"),Display("ʡ��ҽ�ɲ�")]
			SGYXX_J86=12,
			/// <summary>
			/// ʡ��ҽͳ��
			/// </summary>
			//("ʡ��ҽ"),Display("ʡ��ҽ�ɲ�")]
			SGYTC_90 = 13,
			/// <summary>
			/// ʡ��ҽ��������**��
			/// </summary>
			//("ʡ��ҽ"),Display("ʡ��ҽ�ɲ�")]
			SGYXX_J = 14,
			/// <summary>
			/// ʡ��ҽ�ɲ�
			/// </summary>
			//("ʡ��ҽ"),Display("ʡ��ҽ�ɲ�")]
			SGYGB_80 = 15,
			/// <summary>
			/// ʡ��ҽ�ɲ�
			/// </summary>
			//("ʡ��ҽ"),Display("ʡ��ҽ�ɲ�")]
			SGYGB_81 = 16,
			/// <summary>
			/// ʡ��ҽ�ɲ�
			/// </summary>
			//("ʡ��ҽ"),Display("ʡ��ҽ�ɲ�")]
			SGYGB_82 = 17,
			/// <summary>
			/// ʡ��ҽ�ɲ�
			/// </summary>
			//("ʡ��ҽ"),Display("ʡ��ҽ�ɲ�")]
			SGYGB_83 = 18,
			/// <summary>
			/// ʡ��ҽͳ��
			/// </summary>
			//("ʡ��ҽ"),Display("ʡ��ҽͳ��")]
			SGYTC_84 = 19,
			/// <summary>
			/// ��ֱ���ɲ�
			/// </summary>
			//("��ֱ��"),Display("��ֱ���ɲ�20")]
			SZSGB_00 = 20,
			/// <summary>
			/// ��ֱ���ɲ�
			/// </summary>
			//("��ֱ��"),Display("��ֱ���ɲ�21")]
			SZSGB_01 = 21,
			/// <summary>
			/// ��ֱ���ɲ�
			/// </summary>
			//("��ֱ��"),Display("��ֱ���ɲ�22")]
			SZSGB_70 = 22,
			/// <summary>
			/// ��ֱ���ɲ�
			/// </summary>
			//("��ֱ��"),Display("��ֱ���ɲ�23")]
			SZSGB_71 = 23,
			/// <summary>
			/// �и�֪
			/// </summary>
			//("��ֱ��"),Display("��ֱ���ɲ�24")]
			SZSGB_72 = 24,
			/// <summary>
			/// ��ֱ���ɲ�
			/// </summary>
			//("��ֱ��"),Display("��ֱ���ɲ�25")]
			SZSGB_02 = 25,
			/// <summary>
			/// ��ɽ�� �ɲ�
			/// </summary>
			//("��ɽ��"),Display("��ɽ��26")]
			DSQGB_10 = 26,
			/// <summary>
			/// ��ɽ�� �ɲ�
			/// </summary>
			//("��ɽ��"),Display("��ɽ��")]
			DSQGB_11 = 27,
			/// <summary>
			/// ��ɽ�� ͳ��
			/// </summary>
			//("��ɽ��"),Display("��ɽ��")]
			DSQTC_12 = 28,
			/// <summary>
			/// Խ����
			/// </summary>
			//("Խ����"),Display("Խ����")]
			YXQGB_20 = 29,
			/// <summary>
			/// Խ���� 
			/// </summary>
			//("Խ����"),Display("Խ����")]
			YXQGB_21 = 30,
			/// <summary>
			/// Խ����
			/// </summary>
			//("Խ����"),Display("Խ����")]
			YXQTC_22 = 31,
			/// <summary>
			/// ������
			/// </summary>
			//("������"),Display("������")]
			LWQGB_30 = 32,
			/// <summary>
			/// ������
			/// </summary>
			//("������"),Display("������")]
			LWQGB_31 = 33,
			/// <summary>
			/// ������
			/// </summary>
			//("������"),Display("������")]
			LWQTC_32 = 34,
			/// <summary>
			/// ������ �ɲ�
			/// </summary>
			//("������"),Display("������")]
			HZQGB_40 = 35,
			/// <summary>
			/// ������ �ɲ�
			/// </summary>
			//("������"),Display("������")]
			HZQGB_41 = 36,
            /// <summary>
            /// ������ ͳ��
            /// </summary>
            //("������"),Display("������")]
			HZQTC_42 = 37,
			/// <summary>
			/// ������ �ɲ�
			/// </summary>
			//("������"),Display("������")]
			BYQGB_50 = 38,
			/// <summary>
			/// ������
			/// </summary>
			//("������"),Display("������")]
			BYQGB_51 = 39,
			/// <summary>
			/// ������
			/// </summary>
			//("������"),Display("������")]
			BYQTC_52 = 40,
			/// <summary>
			/// ������
			/// </summary>
			//("������"),Display("������")]
			HPQGB_60 = 41,
			/// <summary>
			/// ������
			/// </summary>
			//("������"),Display("������")]
			HPQGB_61 = 42,
			/// <summary>
			/// ������
			/// </summary>
			//("������"),Display("������")]
			HPQTC_62 = 43,
			/// <summary>
			/// �����
			/// </summary>
			//("�����"),Display("�����")]
			THQGB_A0 = 44,
			/// <summary>
			/// �����
			/// </summary>
			//("�����"),Display("�����")]
			THQGB_A1 = 45,
			/// <summary>
			/// �����
			/// </summary>
			//("�����"),Display("�����")]
			THQTC_A2 = 46,
			/// <summary>
			/// ������ �ɲ�
			/// </summary>
			//("������"),Display("������")]
			FCQGB_B0 = 47,
			/// <summary>
			/// ������
			/// </summary>
			//("������"),Display("������")]
			FCQGB_B1 = 48,
			/// <summary>
			/// ������
			/// </summary>
			//("������"),Display("������")]
			FCQGB_B2 = 49,
			/// <summary>
			/// �������ɲ�
			/// </summary>
			//("������"),Display("������")]
			KFQGB_K1 = 50,
			/// <summary>
			/// ����������
			/// </summary>
			//("������"),Display("������")]
			KFQGB_K2 = 51,
			/// <summary>
			/// ����������
			/// </summary>
			//("������"),Display("������")]
			KFQGB_K3 = 52,
			/// <summary>
			/// ����������
			/// </summary>
			//("������"),Display("������")]
			KFQGB_K70 = 53,
			/// <summary>
			/// ����������
			/// </summary>
			//("������"),Display("������54")]
			KFQGB_K71 = 54,
			/// <summary>
			/// ����������
			/// </summary>
			//("������"),Display("������55")]
			KFQGB_K72 = 55,
			/// <summary>
			/// ������
			/// </summary>
			//("������"),Display("������56")]
            SQX_03 = 56,
			/// <summary>
			/// ������
			/// </summary>
			//("������"),Display("������57")]
			SQX_75 = 57,
			/// <summary>
			/// ������
			/// </summary>
			//("������"),Display("������58")]
			SQX_73 = 58,
			/// <summary>
			/// ����
			/// </summary>
			//("����"),Display("����")]
			QT_All = 59
		}
		
		/// <summary>
		/// ����ID
		/// </summary>
		private enuMCardType myID;
		//public new System.Object ID 
	
		public new System.Object ID {
			get {
				return this.myID;
			}
			set {
				try {
					this.myID=this.GetIDFromName (value.ToString()); 
				}
				catch {
					string err="�޷�ת��"+this.GetType().ToString()+"���룡";
				}
				base.ID=this.myID.ToString();
				string s=this.Name;
			}
		}

		public enuMCardType GetIDFromName(string Name) {
			//FIXME
			enuMCardType c=new enuMCardType();
			for(int i=0;i<100;i++) {
				c=(enuMCardType)i;
				if(c.ToString()==Name) return c;
			}
			return (Neusoft.HISFC.Object.Fee.MCardType.enuMCardType)int.Parse(Name);
		}
		/// <summary>
		/// ��������
		/// </summary>
		public new string Name {
			get {
				string str;
				string strUp;
				switch ((int)this.ID) {
					case 1:
						str= "����";
						strUp = "����";
						break;
					case 2:
						str="��Ժְ��";
						strUp = "��Ժְ��";
						break;
					case 3:
						str="��Ժ����(�ϰ���)";
						strUp = "��Ժ����(�ϰ���)";
						break;
					case 4:
						str="��Ժ����(�°���)";
						strUp = "��Ժ����(�°���)";
						break;
					case 5:
						str="��Ժ����(ȫ��)";
						strUp = "��Ժ����(ȫ��)";
						break;
					case 6:
						str="��Ժ����";
						strUp = "��Ժ����";
						break;
					case 7:
						str="��Ժ����";
						strUp = "��Ժ����";
						break;
					case 8:
						str="ʡ��ҽ�ɲ�(��80)";
						strUp = "ʡ��ҽ�ɲ�(��80)";
						break;
					case 9:
						str="ʡ��ҽ�ɲ�(��81)";
						strUp = "ʡ��ҽ�ɲ�(��81)";
						break;
					case 10:
						str="ʡ��ҽ�ɲ�(��82)";
						strUp = "ʡ��ҽ�ɲ�(��82)";
						break;
					case 11:
						str="ʡ��ҽ�ɲ�(��83)";
						strUp = "ʡ��ҽ�ɲ�(��83)";
						break;
					case 12:
						str="ʡ��ҽ�ɲ�(��86)";
						strUp = "ʡ��ҽ�ɲ�(��86)";
						break;
					case 13:
						str="ʡ��ҽͳ��(90)";
						strUp = "ʡ��ҽͳ��(90)";
						break;
					case 14:
						str="ʡ��ҽ����(��**)";
						strUp = "ʡ��ҽ����(��**)";
						break;
					case 15:
						str="ʡ��ҽ�ɲ�(80)";
						strUp = "ʡ��ҽ�ɲ�(80)";
						break;
					case 16:
						str="ʡ��ҽ�ɲ�(81)";
						strUp = "ʡ��ҽ�ɲ�(81)";
						break;
					case 17:
						str="ʡ��ҽ�ɲ�(82)";
						strUp = "ʡ��ҽ�ɲ�(82)";
						break;
					case 18:
						str="ʡ��ҽ�ɲ�(83)";
						strUp = "ʡ��ҽ�ɲ�(83)";
						break;
					case 19:
						str="ʡ��ҽͳ��(84)";
						strUp = "ʡ��ҽͳ��(84)";
						break;
					case 20:
						str="��ֱ���ɲ�(00)";
						strUp = "��ֱ��";
						break;
					case 21:
						str="��ֱ���ɲ�(01)";
						strUp = "��ֱ��";
						break;
					case 22:
						str="��ֱ���ɲ�(70)";
						strUp = "��ֱ��";
						break;
					case 23:
						str="��ֱ���ɲ�(71)";
						strUp = "��ֱ��";
						break;
					case 24:
						str="��ֱ���ɲ�(72)";
						strUp = "�и�֪";
						break;
					case 25:
						str="��ֱ��ͳ��(02)";
						strUp = "��ֱ��";
						break;
					case 26:
						str="��ɽ���ɲ�(10)";
						strUp = "��ɽ��";
						break;
					case 27:
						str="��ɽ���ɲ�(11)";
						strUp = "��ɽ��";
						break;
					case 28:
						str="��ɽ���ɲ�(12)";
						strUp = "��ɽ��";
						break;
					case 29:
						str="Խ�����ɲ�(20)";
						strUp = "��Ժְ��";
						break;
					case 30:
						str="Խ�����ɲ�(21)";
						strUp = "Խ����";
						break;
					case 31:
						str="Խ����ͳ��(22)";
						strUp = "Խ����";
						break;
					case 32:
						str="�������ɲ�(30)";
						strUp = "������";
						break;
					case 33:
						str="�������ɲ�(31)";
						strUp = "������";
						break;
					case 34:
						str="������ͳ��(32)";
						strUp = "������";
						break;
					case 35:
						str="�������ɲ�(40)";
						strUp = "������";
						break;
					case 36:
						str="�������ɲ�(41)";
						strUp = "������";
						break;
					case 37:
						str="������ͳ��(42)";
						strUp = "������";
						break;
					case 38:
						str="�������ɲ�(50)";
						strUp = "������";
						break;
					case 39:
						str="�������ɲ�(51)";
						strUp = "������";
						break;
					case 40:
						str="������ͳ��(52)";
						strUp = "������";
						break;
					case 41:
						str="�������ɲ�(60)";
						strUp = "������";
						break;
					case 42:
						str="�������ɲ�(61)";
						strUp = "������";
						break;
					case 43:
						str="������ͳ��(62)";
						strUp = "������";
						break;
					case 44:
						str="������ɲ�(A0)";
						strUp = "�����";
						break;
					case 45:
						str="������ɲ�(A1)";
						strUp = "�����";
						break;
					case 46:
						str="�����ͳ��(A2)";
						strUp = "�����";
						break;
					case 47:
						str="�������ɲ�(B0)";
						strUp = "������";
						break;
					case 48:
						str="�������ɲ�(B1)";
						strUp = "������";
						break;
					case 49:
						str="������ͳ��(B2)";
						strUp = "������";
						break;
					case 50:
						str="�������ɲ�(K1)";
						strUp = "������";
						break;
					case 51:
						str="����������(K2)";
						strUp = "������";
						break;
					case 52:
						str="����������(K3)";
						strUp = "������";
						break;
					case 53:
						str="����������(K70)";
						strUp = "������";
						break;
					case 54:
						str="����������(K71)";
						strUp = "������";
						break;
					case 55:
						str="����������(K72)";
						strUp = "������";
						break;
					case 56:
						str="������(03)";
						strUp = "������";
						break;
					case 57:
						str="������(75)";
						strUp = "������";
						break;
					case 58:
						str="������(73)";
						strUp = "������";
						break;
					default:
						str="����";
						strUp = "����";
						break;
				}
				base.Name=str;
				base.User01 = strUp;
				return	str;
			}
		}
		/// <summary>
		/// ���ȫ���б�
		/// </summary>
		/// <returns>ArrayList(DepartmentType)</returns>
		public static ArrayList List() {
			MCardType o;
			//enuDepartmentType e=new enuDepartmentType();
			ArrayList alReturn=new ArrayList();
			int i;		
			int iCount = System.Enum.GetValues(typeof(enuMCardType)).GetUpperBound(0);
			for(i=0;i <= iCount;i++) {
				o=new MCardType();
				o.ID=(enuMCardType)i;
				o.Memo=i.ToString();
				alReturn.Add(o);
			}
			return alReturn;
		}
#endregion
		public static ArrayList upList() {
			MCardType o;
			//enuDepartmentType e=new enuDepartmentType();
			ArrayList alReturn=new ArrayList();
			int i;
			int iCount = System.Enum.GetValues(typeof(enuMCardType)).GetUpperBound(0);
			for(i=0;i <= iCount;i++) {
				o=new MCardType();
				o.ID=(enuMCardType)i;
				o.Memo=i.ToString();
				alReturn.Add(o);
			}
			return alReturn;
		}
		public new MCardType Clone() {
			return this.MemberwiseClone() as MCardType;
		}
	}
}
