using System;

namespace Neusoft.HISFC.Object.Base
{
	/*----------------------------------------------------------------
	// Copyright (C) 2004 ����ɷ����޹�˾
	// ��Ȩ���С� 
	//
	// �ļ�����enuType.cs
	// �ļ�������������������ʵ��
	//
	// 
	// ������ʶ�������� 20050328
	//
	// �޸ı�ʶ����ѩ�� 20060412
	// �޸�����������һ�´���
	//
	// �޸ı�ʶ��
	// �޸�������
	//----------------------------------------------------------------*/
	public  enum enuType
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
	
	/// <summary>
	/// �շ�����
	/// </summary>
	public enum PayTypes
	{
		/// <summary>
		/// ����δ�շ�
		/// </summary>
		Charged = 1,
		/// <summary>
		/// �Ѿ��շ�
		/// </summary>
		Balanced = 2,
		/// <summary>
		/// �������
		/// </summary>
		EXAMINE = 3,
		/// <summary>
		/// ҩƷԤ���
		/// </summary>
		PhaConfim = 4
	}
	
	/// <summary>
	/// ������Ϣ
	/// </summary>
	public enum CancelTypes
	{
		/// <summary>
		/// ��Ч
		/// </summary>
		Valid = 0,
		/// <summary>
		/// �Ѿ������˷�
		/// </summary>
		Canceled = 1,
		/// <summary>
		/// �ش�
		/// </summary>
		RePrint = 2,
		/// <summary>
		/// ע��
		/// </summary>
		LogOut = 3
	}
	
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
	
}