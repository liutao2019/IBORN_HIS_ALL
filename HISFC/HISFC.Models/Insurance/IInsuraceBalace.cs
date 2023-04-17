using System;

namespace neusoft.HISFC.Object.Insurance
{
	/// <summary>
	/// IInsuraceBalace ��ժҪ˵����
	/// </summary>
	public interface IInsuraceBalace
	{
		/// <summary>
		/// �ʻ�֧��
		/// </summary>
		decimal AcountPay{get;set;}
		/// <summary>
		/// ����Ա����
		/// </summary>
		decimal OfficePay{get;set;}
		/// <summary>
		/// ����
		/// </summary>
		decimal LargePay{get;set;}
		/// <summary>
		/// �Ϻ��
		/// </summary>
		decimal MiltrayPay{get;set;}
		/// <summary>
		/// �����ֽ�֧��
		/// </summary>
		decimal CashPay{get;set;}
	}
}
