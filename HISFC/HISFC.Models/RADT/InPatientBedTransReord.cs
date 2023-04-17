using System;
 
namespace FS.HISFC.Models.RADT
{
	/// <summary>
    /// InPatientBedTransReord <br></br>
	/// [��������: סԺ���˴�λ�����¼]<br></br>
	/// [�� �� ��: xiaohf]<br></br>
	/// [����ʱ��: 2012��8��5��14:14:52]<br></br>
	/// </summary>
    [Serializable]
    public class InPatientBedTransReord : FS.FrameWork.Models.NeuObject
	{
		/// <summary>
		/// ����������
		/// </summary>
        public InPatientBedTransReord()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		
		}

		#region ����
        /// <summary>
        /// סԺ��ˮ��
        /// </summary>
        private string inpatient_no = string.Empty;
        /// <summary>
        /// סԺ�ţ�����Ӥ�������
        /// </summary>
        private string patient_no = string.Empty;
        /// <summary>
        /// ԭ���ұ���
        /// </summary>
        private string old_dept_id = string.Empty;
        /// <summary>
        /// ԭ��������
        /// </summary>
        private string old_dept_name = string.Empty;
        /// <summary>
        /// Ŀ����ұ���
        /// </summary>
        private string target_dept_id = string.Empty;
        /// <summary>
        /// Ŀ���������
        /// </summary>
        private string target_dept_name = string.Empty;
        /// <summary>
        /// ��λ����
        /// </summary>
        private string bed_no = string.Empty;
        /// <summary>
        /// ��λ�������ͣ�0-�ϴ���1-�´�
        /// </summary>
        private string trans_type = string.Empty;
        /// <summary>
        /// ��λ������Դ����,��¼������������Ժ�Ǽǣ���ʿվ�����
        /// </summary>
        private string trans_code = string.Empty;
        /// <summary>
        /// ҽ�������
        /// </summary>
        private string medical_group_code = string.Empty;
        /// <summary>
        /// ���������
        /// </summary>
        private string care_group_code = string.Empty;
        /// <summary>
        /// ��Ժ�ܴ�ҽ��
        /// </summary>
        private string in_doct_code = string.Empty;
        /// <summary>
        /// ��ʿվ����
        /// </summary>
        private string nurse_station_code = string.Empty;
        /// <summary>
        /// ת����루ͬסԺ����ZG��
        /// </summary>
        private string zg = string.Empty;
        /// <summary>
        /// ������ˮ�ţ����ں���ϸ�����
        /// </summary>
        private string sequence_no = string.Empty;
        /// <summary>
        /// ������
        /// </summary>
        private string oper_code = string.Empty;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime oper_date = DateTime.MinValue;
        /// <summary>
        /// ���˵�ǰ���ڿ��ұ���
        /// </summary>
        private string dept_code = string.Empty;
        /// <summary>
        /// ԭ��ʿվ������������
        /// </summary>
        private string old_nurse_id = string.Empty;
        /// <summary>
        /// ԭ��ʿվ������������
        /// </summary>
        private string old_nurse_name = string.Empty;
        /// <summary>
        /// Ŀ�껤ʿվ������������
        /// </summary>
        private string target_nurse_id = string.Empty;
        /// <summary>
        /// Ŀ�껤ʿվ������������
        /// </summary>
        private string target_nurse_name = string.Empty;

		#endregion

		#region ����

        /// <summary>
        /// סԺ��ˮ��
        /// </summary>
        public string INPATIENT_NO
        {
            get
            {
                return this.inpatient_no;
            }
            set
            {
                this.inpatient_no = value;
            }
        }
        /// <summary>
        /// סԺ�ţ�����Ӥ�������
        /// </summary>
        public string PATIENT_NO
        {
            get
            {
                return this.patient_no;
            }
            set
            {
                this.patient_no = value;
            }
        }
        /// <summary>
        /// ԭ���ұ���
        /// </summary>
        public string OLD_DEPT_ID
        {
            get
            {
                return this.old_dept_id;
            }
            set
            {
                this.old_dept_id = value;
            }
        }
        /// <summary>
        /// ԭ��������
        /// </summary>
        public string OLD_DEPT_NAME
        {
            get
            {
                return this.old_dept_name;
            }
            set
            {
                this.old_dept_name = value;
            }
        }
        /// <summary>
        /// Ŀ����ұ���
        /// </summary>
        public string TARGET_DEPT_ID
        {
            get
            {
                return this.target_dept_id;
            }
            set
            {
                this.target_dept_id = value;
            }
        }
        /// <summary>
        /// Ŀ���������
        /// </summary>
        public string TARGET_DEPT_NAME
        {
            get
            {
                return this.target_dept_name;
            }
            set
            {
                this.target_dept_name = value;
            }
        }
        /// <summary>
        /// ��λ����
        /// </summary>
        public string BED_NO
        {
            get
            {
                return this.bed_no;
            }
            set
            {
                this.bed_no = value;
            }
        }
        /// <summary>
        /// ��λ�������ͣ�0-�ϴ���1-�´�
        /// </summary>
        public string TRANS_TYPE
        {
            get
            {
                return this.trans_type;
            }
            set
            {
                this.trans_type = value;
            }
        }
        /// <summary>
        /// ��λ������Դ����,��¼������������Ժ�Ǽǣ���ʿվ�����
        /// </summary>
        public string TRANS_CODE
        {
            get
            {
                return this.trans_code;
            }
            set
            {
                this.trans_code = value;
            }
        }
        /// <summary>
        /// ҽ�������
        /// </summary>
        public string MEDICAL_GROUP_CODE
        {
            get
            {
                return this.medical_group_code;
            }
            set
            {
                this.medical_group_code = value;
            }
        }
        /// <summary>
        /// ���������
        /// </summary>
        public string CARE_GROUP_CODE
        {
            get
            {
                return this.care_group_code;
            }
            set
            {
                this.care_group_code = value;
            }
        }
        /// <summary>
        /// ��Ժ�ܴ�ҽ��
        /// </summary>
        public string IN_DOCT_CODE
        {
            get
            {
                return this.in_doct_code;
            }
            set
            {
                this.in_doct_code = value;
            }
        }
        /// <summary>
        /// ��ʿվ����
        /// </summary>
        public string NURSE_STATION_CODE
        {
            get
            {
                return this.nurse_station_code;
            }
            set
            {
                this.nurse_station_code = value;
            }
        }
        /// <summary>
        /// ת����루ͬסԺ����ZG��
        /// </summary>
        public string ZG
        {
            get
            {
                return this.zg;
            }
            set
            {
                this.zg = value;
            }
        }
        /// <summary>
        /// ������ˮ�ţ����ں���ϸ�����
        /// </summary>
        public string SEQUENCE_NO
        {
            get
            {
                return this.sequence_no;
            }
            set
            {
                this.sequence_no = value;
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string OPER_CODE
        {
            get
            {
                return this.oper_code;
            }
            set
            {
                this.oper_code = value;
            }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime OPER_DATE
        {
            get
            {
                return this.oper_date;
            }
            set
            {
                this.oper_date = value;
            }
        }
           /// <summary>
        /// ���˵�ǰ���ڿ��ұ���
        /// </summary>
        public string DEPT_CODE
        {
            get
            {
                return this.dept_code;
            }
            set
            {
                this.dept_code = value;
            }
        }

        /// <summary>
        /// ԭ���ұ���
        /// </summary>
        public string OLD_NURSE_ID
        {
            get
            {
                return this.old_nurse_id;
            }
            set
            {
                this.old_nurse_id = value;
            }
        }
        /// <summary>
        /// ԭ��������
        /// </summary>
        public string OLD_NURSE_NAME
        {
            get
            {
                return this.old_nurse_name;
            }
            set
            {
                this.old_nurse_name = value;
            }
        }
        /// <summary>
        /// Ŀ����ұ���
        /// </summary>
        public string TARGET_NURSE_ID
        {
            get
            {
                return this.target_nurse_id;
            }
            set
            {
                this.target_nurse_id = value;
            }
        }
        /// <summary>
        /// Ŀ���������
        /// </summary>
        public string TARGET_NURSE_NAME
        {
            get
            {
                return this.target_nurse_name;
            }
            set
            {
                this.target_nurse_name = value;
            }
        }
		#endregion

		#region ����

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
        public new InPatientBedTransReord Clone()
		{
            return this.MemberwiseClone() as InPatientBedTransReord;
		}

		#endregion
	}
}
