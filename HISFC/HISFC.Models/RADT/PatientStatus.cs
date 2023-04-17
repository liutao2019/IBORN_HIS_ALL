
using System;
 

namespace Neusoft.HISFC.Object.RADT
{
	/// <summary>
	/// ������� written by wolf 
	/// 2004-6-9
	/// <br>Value	Description</br>
	///	<br>E	Emergency</br>
	///	<br>I	Inpatient</br>
	///	<br>O	Outpatient</br>
	///	<br>P	Preadmit	</br>				
	///	<br>R	Recurring Patient</br>
	///	<br>B	Obstetrics</br>
	///	<br>C	PhysicalExamination</br>
	/// </summary>
	[Obsolete("�Ѿ����ڣ�����ΪPatientType")]
	public class PatientStatus : Neusoft.NFC.Object.NeuObject
	{
		/// <summary>
		/// ���������
		/// </summary>
		public PatientStatus()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public enum enuPatientStatus
		{
			/// <summary>
			/// ����
			/// </summary>
			E,
			/// <summary>
			/// סԺ
			/// </summary>
			I,
			/// <summary>
			/// ����
			/// </summary>
			O,
			/// <summary>
			/// ԤԼ
			/// </summary>
			P,
			/// <summary>
			/// Recurring Patient--��ʱ����
			/// </summary>
			R,
			/// <summary>
			/// �и� --��ʱ����
			/// </summary>
			B,
			/// <summary>
			/// ���
			/// </summary>
			C
		};
		
		/// <summary>
		/// ����ID
		/// </summary>
		private enuPatientStatus myID;
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
		public enuPatientStatus GetIDFromName(string Name)
		{
			enuPatientStatus c=new enuPatientStatus();
			for(int i=0;i<100;i++)
			{
				c=(enuPatientStatus)i;
				if(c.ToString()==Name) return c;
			}
			return (enuPatientStatus)int.Parse(Name);
		}
		
		public new string Name
		{
			get
			{
				string strPatientStatus;
				switch ((int)this.ID)
				{
					case 0:
						strPatientStatus= "����";
						break;
					case 1:
						strPatientStatus="סԺ";
						break;
					case 2:
						strPatientStatus="����";
						break;
					case 3:
						strPatientStatus="ԤԼ";
						break;
					case 4:
						strPatientStatus="����";
						break;
					case 5:
						strPatientStatus="�и�";
						break;
					case 6:
						strPatientStatus="���";
						break;
					default:
						strPatientStatus="����";
						break;
				}
					base.Name=strPatientStatus;
				return	strPatientStatus;
			}
		}
		/// <summary>
		/// ���ȫ���б�
		/// </summary>
		/// <returns>ArrayList(PatientStatus)</returns>
		public System.Collections.ArrayList List()
		{
			PatientStatus aPatientStatus;
			System.Collections.ArrayList alReturn=new System.Collections.ArrayList();
			int i;
			for(i=0;i<=5;i++)
			{
				aPatientStatus=new PatientStatus();
				aPatientStatus.ID=(enuPatientStatus)i;
				aPatientStatus.Memo=i.ToString();
				alReturn.Add(aPatientStatus);
			}
			return alReturn;
		}
		public new PatientStatus Clone()
		{
			return this.MemberwiseClone() as PatientStatus;
		}
	}
}
