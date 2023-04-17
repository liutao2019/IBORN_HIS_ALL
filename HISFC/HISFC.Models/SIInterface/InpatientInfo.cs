using System;


namespace FS.HISFC.Models.SIInterface {


	/// <summary>
	/// ����ҽ����Ժ���߻�����Ϣ
	/// </summary>
    [Serializable]
    public class InpatientInfo:FS.FrameWork.Models.NeuObject
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
		private FS.FrameWork.Models.NeuObject emplType = new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// ��Ա��� 1����ְ2������3������4��1��4������5����ҵ7����ְ
		/// </summary>
		public FS.FrameWork.Models.NeuObject EmplType
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
		private FS.FrameWork.Models.NeuObject regType = new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// ������� 1-סԺ2-�����ض���Ŀ
		/// </summary>
		public FS.FrameWork.Models.NeuObject RegType
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
		private FS.FrameWork.Models.NeuObject inDiagnose = new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// ��Ժ��������
		/// </summary>
		public FS.FrameWork.Models.NeuObject InDiagnose
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
		private FS.FrameWork.Models.NeuObject inDept = new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// סԺ���� ҽҩ������ҽ���ͻ���ά���Ŀ��Ҵ���
		/// </summary>
		public FS.FrameWork.Models.NeuObject InDept
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
