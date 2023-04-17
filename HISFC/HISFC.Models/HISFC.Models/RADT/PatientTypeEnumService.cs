using System.Collections;

namespace FS.HISFC.Models.RADT
{
	/// <summary>
	/// PatientTypeEnumService <br></br>
	/// [��������: �������ö�ٷ���ʵ��]<br></br>
	/// [���� - E]<br></br>
	/// [סԺ - I]<br></br>
	/// [���� - O]<br></br>
	/// [ԤԼ - P]<br></br>
	/// [Recurring - R]<br></br>
	/// [�и� - B]<br></br>
	/// [��� - C]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-09-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class PatientTypeEnumService : FS.HISFC.Models.Base.EnumServiceBase
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public PatientTypeEnumService()
		{
			this.Items[EnumPatientType.E] = "����";
			this.Items[EnumPatientType.I] = "סԺ";
			this.Items[EnumPatientType.O] = "����";
			this.Items[EnumPatientType.P] = "ԤԼ";
			this.Items[EnumPatientType.R] = "Recurring";
			this.Items[EnumPatientType.B] = "�и�";
			this.Items[EnumPatientType.C] = "���";
		}

		#region ����

		/// <summary>
		/// �������
		/// </summary>
		FS.HISFC.Models.RADT.EnumPatientType enumPatientType;

		/// <summary>
		/// �洢ö�ٶ���
		/// </summary>
		protected static Hashtable items = new Hashtable();

		#endregion

		#region ����

		/// <summary>
		/// ����ö������
		/// </summary>
		protected override Hashtable Items
		{
			get
			{
				return items;
			}
		}
		
		/// <summary>
		/// ö����Ŀ
		/// </summary>
		protected override System.Enum EnumItem
		{
			get
			{
				return this.enumPatientType;
			}
		}

		#endregion

		#region ����

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new PatientTypeEnumService Clone()
		{
			PatientTypeEnumService patientTypeEnumService = base.Clone() as PatientTypeEnumService;

			return patientTypeEnumService;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList(items.Values));
        }
		#endregion
	}

	/// <summary>
	/// �������ö��
	/// </summary>
	public enum EnumPatientType
	{
		/// <summary>
		/// ����:0
		/// </summary>
		E,
		/// <summary>
		/// סԺ:1
		/// </summary>
		I,
		/// <summary>
		/// ����:2
		/// </summary>
		O,
		/// <summary>
		/// ԤԼ:3
		/// </summary>
		P,
		/// <summary>
		/// Recurring Patient--��ʱ����:4
		/// </summary>
		R,
		/// <summary>
		/// �и� --��ʱ����:5
		/// </summary>
		B,
		/// <summary>
		/// ���:6
		/// </summary>
		C
	};
}
