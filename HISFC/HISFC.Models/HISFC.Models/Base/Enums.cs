namespace FS.HISFC.Models.Base
{
	#region ��������
	/// <summary>
	/// ��������
	/// </summary>
	public enum ServiceTypes 
	{

		/// <summary>
		/// ����
		/// </summary>
		C = 1,
		/// <summary>
		/// סԺ
		/// </summary>
		I = 2
	}
	#endregion
	
	#region ģ����ѯ��Ŀ��ʽ
	/// <summary>
	/// ģ����ѯ��Ŀ��ʽ
	/// </summary>
	public enum InputTypes
	{
		/// <summary>
		/// ƴ����
		/// </summary>
		Spell = 1,
		/// <summary>
		/// �����
		/// </summary>
		WB = 2, 
		/// <summary>
		/// �Զ�����
		/// </summary>
		UserCode = 3,
		/// <summary>
		/// ����
		/// </summary>
		Name = 4
	}
	#endregion
	
	#region ��Ŀ���
	/// <summary>
	/// ��Ŀ���
	/// </summary>
	public enum ItemTypes
	{
		/// <summary>
		/// ������Ŀ,����ҩƷ,������Ŀ
		/// </summary>
		All = 1, 
		/// <summary>
		///����ҩƷ 
		/// </summary>
		AllMedicine = 2,
		/// <summary>
		/// ��ҩ
		/// </summary>
		WesternMedicine = 3,
		/// <summary>
		/// �г�ҩ
		/// </summary>
		ChineseMedicine = 4,
		/// <summary>
		/// �в�ҩ
		/// </summary>
		HerbalMedicine = 5,
		/// <summary>
		/// ��ҩƷ
		/// </summary>
		Undrug = 6
	}
	
	#endregion
	
	#region ��������
	/// <summary>
	/// ��������
	/// </summary>
	public enum TransTypes
	{
		/// <summary>
		/// ������
		/// </summary>
		Positive = 1, 
		/// <summary>
		/// ������
		/// </summary>
		Negative = 2
	}
	#endregion
	
	#region �շ�����
	/// <summary>
	/// �շ�����
	/// </summary>
	public enum PayTypes
	{
		/// <summary>
		/// ����δ�շ�
		/// </summary>
		Charged = 0,
		/// <summary>
		/// �Ѿ��շ�
		/// </summary>
		Balanced = 1,
        /// <summary>
        /// �Ѿ���ҩ
        /// </summary>
        SendDruged = 2,
		/// <summary>
		/// �������
		/// </summary>
		EXAMINE = 3,
		/// <summary>
		/// ҩƷԤ���
		/// </summary>
		PhaConfim = 4
	}
	#endregion
	
	#region ������Ϣ
	/// <summary>
	/// ������Ϣ
	/// </summary>
	public enum CancelTypes
	{
		/// <summary>
		/// ��Ч
		/// </summary>
		Valid = 1,
		/// <summary>
		/// �Ѿ������˷�
		/// </summary>
		Canceled = 0,
		/// <summary>
		/// �ش�
		/// </summary>
		Reprint = 2,
		/// <summary>
		/// ע��
		/// </summary>
		LogOut = 3
	}
	#endregion
	
	#region �շѲ���
	/// <summary>
	/// �շѲ���
	/// </summary>
	public enum ChargeTypes
	{
		/// <summary>
		/// ���۱���
		/// </summary>
		Save = 1,
		/// <summary>
		/// �շ�
		/// </summary>
		Fee = 2
	}
	#endregion

    #region ��Ч��״̬

    /// <summary>
    /// ��Ч��״̬
    /// </summary>
    public enum EnumValidState
    {
        /// <summary>
        /// ��Ч
        /// </summary>
        Valid = 1,
        /// <summary>
        /// ��Ч
        /// </summary>
        Invalid = 0,
        /// <summary>
        /// ����/����
        /// </summary>
        Ignore = 2,
        /// <summary>
        /// ��չ
        /// </summary>
        Extend = 3
    }

    #endregion

    #region �������������ѯ
    /// <summary>
	/// �������������ѯ
	/// </summary>
	public enum QueryValidTypes
	{
		/// <summary>
		/// ��Ч
		/// </summary>
		Valid = 1,
		/// <summary>
		/// ����
		/// </summary>
		Cancel = 0,
		/// <summary>
		/// ����
		/// </summary>
		All = 2
	}
#endregion
	
	#region ����״̬
	/// <summary>
	/// ����״̬
	/// </summary>
	public enum EnumBedStatus
	{
		/// <summary>
		/// Closed
		/// </summary>
		C,
		/// <summary>
		/// Unoccupied
		/// </summary>
		U,
		/// <summary>
		/// Contaminated��Ⱦ��
		/// </summary>
		K,
		/// <summary>
		/// �����
		/// </summary>
		I,
		/// <summary>
		/// Occupied
		/// </summary>
		O,
		/// <summary>
		/// �ٴ�  user define
		/// </summary>
		R,
		/// <summary>
		/// ���� user define
		/// </summary>
		W,
		/// <summary>
		/// �Ҵ�
		/// </summary>
		H
	}	
	#endregion
     
    #region Rhd��Ϣ
    /// <summary>
    /// Rhd��Ϣ
    /// </summary>
    public enum RhDs
    {
        /// <summary>
        /// ����
        /// </summary>
        Positive = 1,
        /// <summary>
        /// ����
        /// </summary>
        Negative = 2
    }

    #endregion

    #region ѪҺ����������Ϣ
    /// <summary>
    /// Rhd��Ϣ
    /// </summary>
    public enum EnumBloodTypeByRh
    {
        /// <summary>
        /// ����
        /// </summary>
        P = 1,
        /// <summary>
        /// ����
        /// </summary>
        N = 2,
    }

    #endregion

	#region ѪҺ����
	/// <summary>
	/// ѪҺ����ö��
	/// </summary>
	public enum EnumBloodTypeByABO
	{

		/// <summary>
		/// δ֪
		/// </summary>
		U = 0,
		/// <summary>
		///A 
		/// </summary>
		A=1,
		/// <summary>
		/// B
		/// </summary>
		B=2,
		/// <summary>
		/// AB
		/// </summary>
		AB=3,
		/// <summary>
		/// O
		/// </summary>
		O=4
	};
	#endregion
	
	#region �������
	/// <summary>
	/// �������ö��
	/// </summary>
	public enum EnumDiagnoseType
	{	
		/// <summary>
		/// ��Ժ���
		/// </summary>
		IN = 1,
		/// <summary>
		/// ת�����
		/// </summary>
		TURNIN = 2,
		/// <summary>
		/// ��Ժ���
		/// </summary>
		OUT = 3,
		/// <summary>
		/// ת�����
		/// </summary>
		TURNOUT = 4,
		/// <summary>
		/// ȷ�����
		/// </summary>
		SURE = 5,
		/// <summary>
		/// �������
		/// </summary>
		DEAD = 6,
		/// <summary>
		/// ��ǰ���
		/// </summary>
		OPSFRONT = 7,
		/// <summary>
		/// �������
		/// </summary>
		OPSAFTER = 8,
		/// <summary>
		/// ��Ⱦ���
		/// </summary>
		INFECT = 9,
		/// <summary>
		/// �����ж����
		/// </summary>
		DAMNIFY = 10,
		/// <summary>
		/// ����֢���
		/// </summary>
		COMPLICATION = 11,
		/// <summary>
		/// �������
		/// </summary>
		PATHOLOGY = 12,
		/// <summary>
		/// �������
		/// </summary>
		SAVE = 13,
		/// <summary>
		/// ��Σ���
		/// </summary>
		FAIL = 14,
		/// <summary>
		/// �������
		/// </summary>
		CLINIC = 15,
		/// <summary>
		/// �������
		/// </summary>
		OTHER = 16,
		/// <summary>
		/// �������
		/// </summary>
		BALANCE = 17

	};
	
	#endregion
	
	#region ����״̬
	/// <summary>
	/// ����״̬
	/// </summary>
	public enum EnumMaritalStatus 
	{
		/// <summary>
		/// Single
		/// </summary>
		S=1,
		/// <summary>
		/// Married
		/// </summary>
		M,
		/// <summary>
		/// Divorced
		/// </summary>
		D,
		/// <summary>
		/// remarriage
		/// </summary>
		R,
		/// <summary>
		/// Separated
		/// </summary>
		A,
		/// <summary>
		/// Widowed
		/// </summary>
		W
	};
	
	#endregion
	
	#region �Ա�
	/// <summary>
	/// �Ա�
	/// </summary>
	public enum EnumSex
	{
		/// <summary>
		/// ��
		/// </summary>
		M=1,
		/// <summary>
		/// Ů
		/// </summary>
		F=2,
		/// <summary>
		/// ����
		/// </summary>
		O=3,
		/// <summary>
		/// δ֪
		/// </summary>
		U=0,
        /// <summary>
		/// ȫ��
		/// </summary>
		A=4
	};
	
	#endregion

    //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
	#region סԺ������Ժ״̬
	/// <summary>
	/// סԺ������Ժ״̬
	/// </summary>
	public enum EnumInState
	{
		/// <summary>
		/// Registration סԺ�Ǽ���� �ȴ�����:0
		/// </summary>
		R =0,
		/// <summary>
		/// after Receiption,in ����������� ��Ժ״̬:1
		/// </summary>
		I =1,
		/// <summary>
		/// Balance  ��Ժ�Ǽ���� ����״̬:2
		/// </summary>
		B =2,
		/// <summary>
		/// out Balance��Ժ�������:3
		/// </summary>
		O =3,
		/// <summary>
		///PreOutԤԼ��Ժ:4
		/// </summary>
		P =4,
		/// <summary>
		/// NoFee�޷���Ժ:5
		/// </summary>
		N =5,
		/// <summary>
		/// Close ����״̬:6
		/// </summary>
		C =6,
        /// <summary>
        /// תסԺ
        /// </summary>
        E
	};
	#endregion
	
	#region	�������
    //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
	/// <summary>
	/// �������
	/// </summary>
	public enum EnumShiftType 
	{
					
		/// <summary>
		/// ת��
		/// </summary>
		RD,
		/// <summary>
		/// ת��1
		/// </summary>
		RB,
		/// <summary>
		/// ת��
		/// </summary>
		RI,
		/// <summary>
		/// ת��
		/// </summary>
		RO,
		/// <summary>
		/// ����2
		/// </summary>
		K,
		/// <summary>
		/// סԺ�Ǽ�3
		/// </summary>
		B,
		/// <summary>
		/// �ٻ�4
		/// </summary>
		C,
		/// <summary>
		/// ��Ժ�Ǽ�5
		/// </summary>
		O,
		/// <summary>
		/// �޷���Ժ
		/// </summary>
		OF,
		/// <summary>
		/// ����
		/// </summary>
		BA,
		/// <summary>
		/// �����ٻ�
		/// </summary>
		BB,
		/// <summary>
		///��;���� 
		/// </summary>
		MB,
		/// <summary>
		/// ������Ϣ�޸�
		/// </summary>
		F,
		/// <summary>
		/// ���괲�ͳ���յ��޸�
		/// </summary>
		LB,
		/// <summary>
		/// �������޶���
		/// </summary>
		DL,
		/// <summary>
		/// �������޶��ۼ�
		/// </summary>
		BT,
		/// <summary>
		/// �����嵥��ӡ
		/// </summary>
		BP,
		/// <summary>
		/// ��ݱ��
		/// </summary>
		CP,
		/// <summary>
		/// ��ҽ���շ�֤����
		/// </summary>
		ZM,
        /// <summary>
        /// �������סԺ����
        /// </summary>
        CD,
        /// <summary>
        /// ���۵Ǽ�
        /// </summary>
        EB,
        /// <summary>
        /// סԺ�Ǽ�3
        /// </summary>
        EK,
        /// <summary>
        /// �ٻ�4
        /// </summary>
        EC,
        /// <summary>
        /// ��Ժ�Ǽ�5
        /// </summary>
        EO,
        /// <summary>
        /// ת�벡��{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
        /// </summary>
        CN,
        /// <summary>
        /// ת������{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
        /// </summary>
        CNO,
        /// <summary>
        /// ����תסԺ
        /// </summary>
        CPI,
        /// <summary>
        /// ����סԺ
        /// </summary>
        CI,
        /// <summary>
        /// ����תסԺ�ٻ�
        /// </summary>
        IC,
        //{D97A6AA0-5AFB-443f-B74D-1AD1604B1567} ���ӿ�������־ yangw 20100907
        /// <summary>
        /// ����
        /// </summary>
        AC,
        /// <summary>
        /// ����
        /// </summary>
        AO


	}
	#endregion

    #region ԤԼ״̬
    /// <summary>
    /// ԤԼ״̬
    /// </summary>
    public enum EnumBookingState
    {
        /// <summary>
        /// ԤԼ����
        /// </summary>
        Apply = 0,
        /// <summary>
        /// ԤԼ�Ǽ�
        /// </summary>
        Booking = 1,
        /// <summary>
        /// ȡ���Ǽ�
        /// </summary>
        CancelBooking = 2,
        /// <summary>
        /// ִ��
        /// </summary>
        Execute = 3,
        /// <summary>
        /// ��Ч
        /// </summary>
        Invalid = 4
    }
    #endregion

    #region �Һ�״̬
    
    /// <summary>
    /// �Һ�״̬
    /// </summary>
    public enum EnumRegisterStatus
    {
        /// <summary>
        /// �˷�
        /// </summary>
        Back,
        /// <summary>
        /// ��Ч
        /// </summary>
        Valid,
        /// <summary>
        /// ����
        /// </summary>
        Cancel
    }
    #endregion

    #region �Һ�����
    /// <summary>
    /// �Һ�����
    /// </summary>
    public enum EnumRegType
    {
        /// <summary>
        /// �ֳ��Һ�
        /// </summary>
        Reg,
        /// <summary>
        /// ԤԼ�Һ�
        /// </summary>
        Pre,
        /// <summary>
        /// ����Һ�
        /// </summary>
        Spe
    }
    #endregion

    #region �Һ��Ű�����
    /// <summary>
    /// �Ű�����
    /// </summary>
    public enum EnumSchemaType
    {
        /// <summary>
        /// �����Ű�
        /// </summary>
        Dept,
        /// <summary>
        /// ҽ���Ű�
        /// </summary>
        Doct
    }
    #endregion

    #region �շѼ����б�����
    /// <summary>
    /// ������Ŀ���
    /// </summary>
    public enum ItemKind
    {
        /// <summary>
        /// ҩƷ
        /// </summary>
        Pharmacy,
        /// <summary>
        /// ��ҩƷ
        /// </summary>
        Undrug,
        /// <summary>
        /// ȫ��
        /// </summary>
        All
    }
    #endregion

    public enum EnumItemType
    {
        /// <summary>
        /// ��ҩƷ
        /// </summary>
        UnDrug=0,
        /// <summary>
        /// ҩƷ
        /// </summary>
        Drug,
        /// <summary>
        /// ������Ŀ
        /// </summary>
        MatItem
    }

    /// <summary>
    /// Ƿ���ж���ʾ����Y��ֻ��ʾǷ��,�������շ� M����ʾǷ�ѣ��������շ�
    /// N�����ж��Ƿ�Ƿ��
    /// </summary>
    public enum MessType
    {
        /// <summary>
        /// ��ʾǷ��,�������շ�
        /// </summary>
        Y,
        /// <summary>
        /// ��ʾǷ�ѣ��������շ�
        /// </summary>
        M,
        /// <summary>
        /// ���ж��Ƿ�Ƿ��
        /// </summary>
        N,

    }

    /// <summary>
    /// ��������
    /// </summary>
    public enum EBlanceType
    {
        /// <summary>
        /// ��Ժ����
        /// </summary>
        Out = 0,
        /// <summary>
        /// ��;����
        /// </summary>
        Mid = 1,
        /// <summary>
        /// Ƿ�ѽ���
        /// </summary>
        Owe = 2
    }

    /// <summary>
    /// ��������
    /// </summary>
    public enum EnumPatientShiftValid
    {
        /// <summary>
        /// ��Ժ�Ǽ�
        /// </summary>
        O,
        /// <summary>
        /// ��Ժ�ٻ�
        /// </summary>
        C,
        /// <summary>
        /// ת��
        /// </summary>
        R,
    }

    //{A45EE85D-B1E3-4af0-ACAD-9DAF65610611}
    /// <summary>
    /// ��������������
    /// </summary>
    public enum EnumAlertType
    {
        /// <summary>
        /// ����Ǯ����
        /// </summary>
        M = 0,
        /// <summary>
        /// ��ʱ�������
        /// </summary>
        D=1
    }
}