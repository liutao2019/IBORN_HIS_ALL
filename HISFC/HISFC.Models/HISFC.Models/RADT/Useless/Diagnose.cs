using System;

namespace FS.HISFC.Models.RADT
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
    [Serializable]
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
		private FS.HISFC.Models.RADT.Patient patient = new Patient();

		/// <summary>
		/// �������(10λ����)
		/// </summary>
		private long happenNO;
		
		/// <summary>
		/// ������
		/// </summary>
		private FS.FrameWork.Models.NeuObject type = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ICD10
		/// </summary>
		private FS.HISFC.Models.HealthRecord.ICD iCD10 = new FS.HISFC.Models.HealthRecord.ICD();

		/// <summary>
		/// ���ʱ��
		/// </summary>
		private DateTime diagTime;

		/// <summary>
		/// ���ҽ��
		/// </summary>
		private FS.FrameWork.Models.NeuObject doctor = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ��Ͽ���
		/// </summary>
		private FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();

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
		public FS.HISFC.Models.RADT.Patient Patient
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
		public FS.FrameWork.Models.NeuObject Type
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
		public FS.HISFC.Models.HealthRecord.ICD ICD10
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
		public FS.FrameWork.Models.NeuObject Doctor
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
		public FS.FrameWork.Models.NeuObject Dept
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
		bool FS.HISFC.Models.Base.IValid.IsValid
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
