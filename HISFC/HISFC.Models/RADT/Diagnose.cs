using System;

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
	public class Diagnose:Base.Spell,Base.IValid
	{

		/// <summary>
		/// ���캯��
		/// </summary>
		public Diagnose()
		{
		}


		#region ����

		/// <summary>
		/// ������Ϣ
		/// </summary>
		private Neusoft.HISFC.Object.RADT.Patient patient = new Patient();

		/// <summary>
		/// �������(10λ����)
		/// </summary>
		private long happenNO;
		
		/// <summary>
		/// ������
		/// </summary>
		private Neusoft.NFC.Object.NeuObject type = new Neusoft.NFC.Object.NeuObject();

		/// <summary>
		/// ICD10
		/// </summary>
		private Neusoft.HISFC.Object.HealthRecord.ICD iCD10 = new Neusoft.HISFC.Object.HealthRecord.ICD();

		/// <summary>
		/// ���ʱ��
		/// </summary>
		private DateTime diagTime;

		/// <summary>
		/// ���ҽ��
		/// </summary>
		private Neusoft.NFC.Object.NeuObject doctor = new Neusoft.NFC.Object.NeuObject();

		/// <summary>
		/// ��Ͽ���
		/// </summary>
		private NFC.Object.NeuObject dept = new NFC.Object.NeuObject();

		/// <summary>
		/// �Ƿ���Ч
		/// </summary>
		private bool isValid;

		/// <summary>
		/// �Ƿ������
		/// </summary>
		public bool isMain;

		#endregion

		#region ����
		/// <summary>
		/// ������Ϣ
		/// </summary>
		public Neusoft.HISFC.Object.RADT.Patient Patient
		{
			get
			{
				return this.patient ;
			}
			set
			{
				this.patient = value ;
			}
		}

		/// <summary>
		/// �������(10λ����)
		/// </summary>
		public long HappenNO
		{
			get
			{
				return this.happenNO;
			}
			set
			{
				this.happenNO = value ;
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		public Neusoft.NFC.Object.NeuObject Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value ;
			}
		}

		/// <summary>
		/// ICD10
		/// </summary>
		public Neusoft.HISFC.Object.HealthRecord.ICD ICD10
		{
			get
			{
				return this.iCD10 ;
			}
			set
			{
				this.iCD10 = value ;
			}
		}

		/// <summary>
		/// ���ʱ��
		/// </summary>
		public DateTime DiagTime
		{
			get
			{
				return this.diagTime ;
			}
			set
			{
				this.diagTime = value ;
			}
		}

		/// <summary>
		/// ���ҽ��
		/// </summary>
		public Neusoft.NFC.Object.NeuObject Doctor
		{
			get
			{
				return this.doctor ;
			}
			set
			{
				this.doctor = value ;
			}
		}

		/// <summary>
		/// ��Ͽ���
		/// </summary>
		public NFC.Object.NeuObject Dept
		{
			get
			{
				return this.dept;
			}
			set
			{
				this.dept = value ;
			}
		}

		/// <summary>
		/// �Ƿ������
		/// </summary>
		public bool IsMain
		{
			get
			{
				return this.isMain ;
			}
			set
			{
				this.isMain = value ;
			}
		}

		#endregion

		#region ����

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new Diagnose Clone()
		{
			Diagnose diagnose= base.Clone() as Diagnose;
			diagnose.Type = Type.Clone();
			diagnose.Dept = Dept.Clone();
			diagnose.Doctor = Doctor.Clone();
			diagnose.Patient = Patient.Clone();
			diagnose.ICD10 = ICD10.Clone();
			return diagnose;
		}

		#endregion

		#region IValid ��Ա

		/// <summary>
		/// ��Ч��־
		/// </summary>
		bool Neusoft.HISFC.Object.Base.IValid.IsValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value ;
			}
		}

		#endregion
		
		#region
		
		/// <summary>
		/// ���ʱ��
		/// </summary>
		[System.Obsolete("��Ϊ���� DiagTime",true)]
		public DateTime DiagDate;
		
		[System.Obsolete("��Ϊ���� HappenNO",true)]
		public long HappenNo;
		#endregion
	}
}
