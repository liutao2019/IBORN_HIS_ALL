using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Preparation
{
    /// <summary>
    /// Prescription<br></br>
    /// [��������: �Ƽ���ҩƷ��Ʒ�䷽]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2006-09-14]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class DisinfectPrescription : PrescriptionBase
    {
        /// <summary>
		/// ���캯��
		/// </summary>
        public DisinfectPrescription()
		{
		}

		#region  ����

		/// <summary>
		/// ��Ʒ
		/// </summary>
        private Fee.Item.Undrug drug = new Fee.Item.Undrug();

		#endregion

		#region  ����

		/// <summary>
		/// �Ƽ���Ʒ
		/// </summary>
        public Fee.Item.Undrug Drug
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
        public new DisinfectPrescription Clone()
		{
            DisinfectPrescription prescription = base.Clone() as DisinfectPrescription;

			prescription.drug = this.drug.Clone();

			return prescription;
		}
		#endregion
    }
}
