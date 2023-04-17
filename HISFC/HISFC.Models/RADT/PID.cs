using System;
 
namespace FS.HISFC.Models.RADT
{
	/// <summary>
	/// PID <br></br>
	/// [��������: ���߸��ֺ���,ID - סԺ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2004-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��˹'
	///		�޸�ʱ��='2006-09-11'
	///		�޸�Ŀ��='�汾����'
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class PID : FS.FrameWork.Models.NeuObject
	{
		/// <summary>
		/// ����������
		/// </summary>
		public PID()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		
		}

		#region ����

		/// <summary>
		/// ���￨��
		/// </summary>
		private string cardNO;

		/// <summary>
		/// ������
		/// </summary>
		private string caseNO;

		/// <summary>
		/// ����������(����)
		/// </summary>
		private string healthNO;

		/// <summary>
		/// ĸ��סԺ��ˮ��.���������Ӥ��,��ô���ڶ���ĸ�׵�סԺ��ˮ��
		/// </summary>
		private string motherInpatientNO;

		#endregion

		#region ����

		/// <summary>
		/// ���￨��
		/// </summary>
		public string CardNO
		{
			get
			{
				return this.cardNO;
			}
			set
			{
				this.cardNO = value;
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		public string CaseNO
		{
			get
			{
				return this.caseNO;
			}
			set
			{
				this.caseNO = value;
			}
		}

		/// <summary>
		/// סԺ��
		/// </summary>
		public string PatientNO
		{
			get
			{
				return this.ID;
			}
			set
			{
				this.ID=value;
			}
		}

		/// <summary>
		/// ����������(����)
		/// </summary>
		public string HealthNO
		{
			get
			{
				return this.healthNO;
			}
			set
			{
				this.healthNO = value;
			}
		}

		/// <summary>
		/// ĸ��סԺ��ˮ��.���������Ӥ��,���ڶ���ĸ�׵�סԺ��ˮ��
		/// </summary>
		public string MotherInpatientNO
		{
			get
			{
                if (this.motherInpatientNO == null)
                {
                    this.motherInpatientNO = string.Empty;
                }
				return this.motherInpatientNO;
			}
			set
			{
				this.motherInpatientNO = value;
			}
		}

		//		private string babyInpatientNo;
		//		/// <summary>
		//		/// Ӥ��סԺ��ˮ��, רΪӤ��������,���ڹ���סԺ�����Ӥ����Ϣ.
		//		/// </summary>
		//		public string BabyInpatientNo
		//		{
		//			get
		//			{
		//				return babyInpatientNo;
		//			}
		//			set
		//			{
		//				babyInpatientNo = value;
		//			}
		//		}

		#endregion

		#region ����

		/// <summary>
		/// ���￨��
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪCardNO", true)]
		public string CardNo;

		/// <summary>
		/// ������
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪCaseNO", true)]
		public string RecordNo;

		/// <summary>
		/// ĸ��סԺ��ˮ��.���������Ӥ��,���ֶ����ڶ���ĸ�׵�סԺ��ˮ��
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪMotherInpatientNO", true)]
		public string MotherInpatientNo 
		{
			get 
			{
				return this.motherInpatientNO;
			}
			set 
			{
				motherInpatientNO = value;
			}
		}



		#endregion

		#region ����

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new PID Clone()
		{
			return this.MemberwiseClone() as PID;
		}

		#endregion
	}
}
