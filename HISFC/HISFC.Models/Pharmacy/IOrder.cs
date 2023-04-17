using System;

namespace Neusoft.HISFC.Object.Pharmacy
{
	/// <summary>
	/// IOrder ��ժҪ˵����
	/// </summary>
	public interface IOrder1
	{
		/// <summary>
		/// ����ID�ţ����￨�Ż���סԺ�ţ�
		/// </summary>
		string PatientID
		{
			get;
			set;
		}

		/// <summary>
		/// �������ڲ�����������ҩ�Ĳ�����
		/// </summary>
		Neusoft.NFC.Object.NeuObject NurseStation
		{
			get;
			set;
		}

		/// <summary>
		/// ÿ�μ���
		/// </summary>
		decimal DoseOnce
		{
			get;
			set;
		}

		/// <summary>
		/// ������λ
		/// </summary>
		string DoseUnit
		{
			get;
			set;
		}

		/// <summary>
		/// �÷�
		/// </summary>
		Neusoft.NFC.Object.NeuObject Usage
		{
			get;
			set;
		}

		/// <summary>
		/// Ƶ��
		/// </summary>
		Neusoft.HISFC.Object.Order.Frequency Frequency
		{
			get;
			set;
		}

		/// <summary>
		/// �������ڲ�����������ҩ�Ĳ�����
		/// </summary>
		string ExecNo
		{
			get;
			set;
		}

		/// <summary>
		/// �������ڲ�����������ҩ�Ĳ�����
		/// </summary>
		string CombNo
		{
			get;
			set;
		}

		/// <summary>
		/// �������ڲ�����������ҩ�Ĳ�����
		/// </summary>
		string OrderNo
		{
			get;
			set;
		}
	}
}
