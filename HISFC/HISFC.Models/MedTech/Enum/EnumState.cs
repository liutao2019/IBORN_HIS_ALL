namespace Neusoft.HISFC.Object.MedTech.Enum
{
    /// <summary>
    /// [��������: ÿ���������ն�������״̬ö��]<br></br>
    /// [�� �� ��: ��ΰ��]<br></br>
    /// [����ʱ��: 2006-12-03]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// 
    /// </summary>
	public enum EnumState {

		/// <summary>
		/// �ȴ�
		/// </summary>
		Wait = 0,
		/// <summary>
		/// ԤԼ
		/// </summary>
		Precontract = 1,
		/// <summary>
		/// ԤԼȷ��
		/// </summary>
		Confirm = 2,
		/// <summary>
		/// ȡ��ԤԼ
		/// </summary>
		CancelPrecontract = 3,
		/// <summary>
		/// ִ��
		/// </summary>
		Execute = 4,
		/// <summary>
		/// ��Ч
		/// </summary>
		Invalid = 5
	}
}
