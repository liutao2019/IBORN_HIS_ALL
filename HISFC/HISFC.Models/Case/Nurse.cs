using System;

namespace neusoft.HISFC.Object.Case
{
	/// <summary>
	/// Nurse ��ժҪ˵��:����ȼ���Ϣ	ID ����Ա��� Name ����Ա����
	/// </summary>
	public class Nurse : neusoft.neuFC.Object.neuObject
	{
		public Nurse()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
	#region ˽�б���
		
		private neusoft.HISFC.Object.RADT.Patient myPatientInfo = new neusoft.HISFC.Object.RADT.Patient();
		private neusoft.neuFC.Object.neuObject myNurseInfo = new neusoft.neuFC.Object.neuObject();
		private int exeNumber;
		private string exeUnit;
		private DateTime operDate;
		
	#endregion

	#region ����
		/// <summary>
		/// ���߻�����Ϣ
		/// </summary>
		public neusoft.HISFC.Object.RADT.Patient PatientInfo
		{
			get{ return myPatientInfo; }
			set{ myPatientInfo = value; }
		}
		/// <summary>
		/// ����ȼ���Ϣ ID �ȼ����� Name �ȼ�����
		/// </summary>
		public neusoft.neuFC.Object.neuObject NurseInfo
		{
			get{ return myNurseInfo; }
			set{ myNurseInfo = value; }
		}
		/// <summary>
		/// ִ������
		/// </summary>
		public int ExeNumber
		{
			get{ return exeNumber; }
			set{ exeNumber = value; }
		}
		/// <summary>
		/// ִ�е�λ
		/// </summary>
		public string ExeUnit
		{
			get{ return exeUnit; }
			set{ exeUnit = value; }
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OperDate
		{
			get{ return operDate; }
			set{ operDate = value; }
		}

	#endregion 

	#region ���к���
		public new Nurse Clone()
		{
			Nurse NurseClone = base.MemberwiseClone() as Nurse;

			NurseClone.PatientInfo = this.PatientInfo.Clone();
			NurseClone.NurseInfo = this.NurseInfo.Clone();

			return NurseClone;
		}
	#endregion
	}
}
