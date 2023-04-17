using System.Collections;

namespace FS.HISFC.Models.RADT
{
    /// <summary>
    /// PatientNOTypeEnumService <br></br>
    /// [��������: ����סԺ�ŷ�������]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2006-11-7]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class PatientNOTypeEnumService : FS.HISFC.Models.Base.EnumServiceBase
    {
        /// <summary>
		/// ���캯��
		/// </summary>
        public PatientNOTypeEnumService()
		{
            this.Items[EnumPatientNOType.First] = "��һ����Ժ";
            this.Items[EnumPatientNOType.Second] = "�����Ժ";
            this.Items[EnumPatientNOType.Temp] = "��ʱ��";
		}

        #region ����

        /// <summary>
        /// ����סԺ�ŷ�������
        /// </summary>
        FS.HISFC.Models.RADT.EnumPatientNOType enumPatientNOType;

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
        ///����סԺ�ŷ�������
        /// </summary>
        protected override System.Enum EnumItem
        {
            get
            {
                return this.enumPatientNOType;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns>��ǰ�����ʵ������</returns>
        public new PatientNOTypeEnumService Clone()
        {
            PatientNOTypeEnumService patientNOTypeEnumService = base.Clone() as PatientNOTypeEnumService;

            return patientNOTypeEnumService;
        }

        /// <summary>
        /// ����ö���б�
        /// </summary>
        /// <returns>ö���б�</returns>
        public new static ArrayList List()
        {
            return (new ArrayList(items.Values));
        }
        #endregion
    }

    /// <summary>
    /// ����סԺ�ŷ�������
    /// </summary>
    public enum EnumPatientNOType 
    {
        /// <summary>
        /// ��һ����Ժ
        /// </summary>
        First = 1,

        /// <summary>
        /// �����Ժ
        /// </summary>
        Second = 2,

        /// <summary>
        /// ��ʱ��
        /// </summary>
        Temp = 3
    }
}
