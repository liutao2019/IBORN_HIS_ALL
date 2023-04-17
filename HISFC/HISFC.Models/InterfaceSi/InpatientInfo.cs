using System;

namespace neusoft.HISFC.Object.InterfaceSi
{
	/// <summary>
	/// ����ҽ����Ժ���߻�����Ϣ
	/// </summary>
	public class InpatientInfo:neusoft.neuFC.Object.neuObject
	{
		public InpatientInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		private string regNo;
		/// <summary>
		/// ��ҽ�ǼǺ�
		/// </summary>
		public string RegNo
		{
			get
			{
				return regNo;
			}
			set
			{
				regNo = value;
			}
		}
		private string hosNo;
		/// <summary>
		/// ҽԺ���
		/// </summary>
		public string HosNo
		{
			get
			{
				return hosNo;
			}
			set
			{
				hosNo = value;
			}
		}
		private string personId;
		/// <summary>
		/// ������ݺ���
		/// </summary>
		public string PersonId
		{
			get
			{
				return personId;
			}
			set
			{
				personId = value;
			}
		}
		private string personName;
		/// <summary>
		/// ����
		/// </summary>
		public string PersonName
		{
			get
			{
				return personName;
			}
			set
			{
				personName = value;
			}
		}
		private string companyName;
		/// <summary>
		/// ��λ����
		/// </summary>
		public string CompanyName
		{
			get
			{
				return companyName;
			}
			set
			{
				companyName = value;
			}
		}
		private DateTime birthday;
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime Birthday
		{
			get
			{
				return birthday;
			}
			set
			{
				birthday = value;
			}
		}
		private neusoft.neuFC.Object.neuObject emplType = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// ��Ա��� 1����ְ2������3������4��1��4������5����ҵ7����ְ
		/// </summary>
		public neusoft.neuFC.Object.neuObject EmplType
		{
			get
			{
				return emplType;
			}
			set
			{
				emplType = value;
			}
		}
		private string patientNo;
		/// <summary>
		/// סԺ��
		/// </summary>
		public string PatientNo
		{
			get
			{
				return patientNo;
			}
			set
			{
				patientNo = value;
			}
		}
		private neusoft.neuFC.Object.neuObject regType = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// ������� 1-סԺ2-�����ض���Ŀ
		/// </summary>
		public neusoft.neuFC.Object.neuObject RegType
		{
			get
			{
				return regType;
			}
			set
			{
				regType = value;
			}
		}
		private DateTime inDate;
		/// <summary>
		/// ��Ժ���������
		/// </summary>
		public DateTime InDate
		{
			get
			{
				return inDate;
			}
			set
			{
				inDate = value;
			}
		}
		private neusoft.neuFC.Object.neuObject inDiagnose = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// ��Ժ��������
		/// </summary>
		public neusoft.neuFC.Object.neuObject InDiagnose
		{
			get
			{
				return inDiagnose;
			}
			set
			{
				inDiagnose = value;
			}
		}
		private neusoft.neuFC.Object.neuObject inDept = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// סԺ���� ҽҩ������ҽ���ͻ���ά���Ŀ��Ҵ���
		/// </summary>
		public neusoft.neuFC.Object.neuObject InDept
		{
			get
			{
				return inDept;
			}
			set
			{
				inDept = value;
			}
		}
		private string bedNo;
		/// <summary>
		/// ��λ����
		/// </summary>
		public string BedNo
		{
			get
			{
				return bedNo;
			}
			set
			{
				bedNo = value;
			}
		}
		private int applyNo;
		/// <summary>
		/// �������������
		/// </summary>
		public int ApplyNo
		{
			get
			{
				return applyNo;
			}
			set
			{
				applyNo = value;
			}
		}
		private int readFlag;
		/// <summary>
		/// �����־
		/// </summary>
		public int ReadFlag
		{
			get
			{
				return readFlag;
			}
			set
			{
				readFlag = value;
			}
		}

		public new InpatientInfo Clone()
		{
			InpatientInfo obj = base.MemberwiseClone() as InpatientInfo;

			obj.InDiagnose = this.InDiagnose.Clone();
			obj.InDept = this.InDept.Clone();
			obj.EmplType = this.EmplType.Clone();
			obj.RegType = this.RegType.Clone();

			return obj;
		}

	}
}
