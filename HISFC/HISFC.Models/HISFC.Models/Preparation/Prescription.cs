using System;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Preparation
{
	/// <summary>
	/// Prescription<br></br>
	/// [��������: �Ƽ���Ʒ�䷽]<br></br>
	/// [�� �� ��: ������]<br></br>
	/// [����ʱ��: 2006-09-14]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class Prescription : PrescriptionBase
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Prescription()
		{
		}

		#region  ����

		/// <summary>
		/// ��Ʒ
		/// </summary>
        private Pharmacy.Item drug = new Pharmacy.Item();

		#endregion

		#region  ����

		/// <summary>
		/// �Ƽ���Ʒ
		/// </summary>
        public Pharmacy.Item Drug
		{
			get
			{
				return this.drug;
			}
			set
			{
				this.drug = value;
                base.ID = value.ID;
                base.Name = value.Name;
			}
		}

        public override string ID
        {
            get
            {
                return base.ID;
            }
        }

        public override string Name
        {
            get
            {
                return base.Name;
            }
        }

		#endregion

		#region ����

		/// <summary>
		/// ���ƶ���
		/// </summary>
		/// <returns>Prescription</returns>
		public new Prescription Clone()
		{
			Prescription prescription = base.Clone() as Prescription;

			prescription.drug = this.drug.Clone();

			return prescription;
		}
		#endregion
	}
}
