using System;

namespace Neusoft.HISFC.Object.Base
{
	/// <summary>
	///������Ϣ�� 
	/// 
	/// </summary>
	public class FT : Neusoft.NFC.Object.NeuObject
	{
		/// <summary>
		/// ������Ϣ��
		/// </summary>
		public FT()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		//������δ�����
		/// <summary>
		/// �ܹ����
		/// </summary>
		public decimal Tot_Cost;
		/// <summary>
		/// �Էѽ��
		/// </summary>
		public decimal Own_Cost;
		/// <summary>
		/// �Ը����
		/// </summary>
		public decimal Pay_Cost;
		/// <summary>
		/// ���ѽ��
		/// </summary>
		public decimal Pub_Cost;
		/// <summary>
		/// ʣ����
		/// </summary>
		public decimal Left_Cost;
		/// <summary>
		/// ������
		/// </summary>
		public decimal Dereate_Cost;
		/// <summary>
		/// ���ս��
		/// </summary>
		public decimal Supply_Cost;
		/// <summary>
		/// �������
		/// </summary>
		public decimal Return_Cost;
		/// <summary>
		/// Ԥ����
		/// </summary>
		public decimal Prepay_Cost;
		/// <summary>
		/// �Żݽ��
		/// </summary>
		public decimal Rebate_Cost;
	
		/// <summary>
		/// �Ѿ�����ķ���
		/// </summary>
		public decimal Balance_Cost;
		/// <summary>
		/// �Ѿ������Ԥ����
		/// </summary>
		public decimal Balance_Prepay;
		/// <summary>
		/// ת���ܷ���
		/// </summary>
		public decimal ChangeTotCost;
		/// <summary>
		/// ת��Ԥ����
		/// </summary>
		public decimal ChangePrepay;
		/// <summary>
		/// תѺ��
		/// </summary>
		public decimal TransPrepay;
		/// <summary>
		/// �ϴι̶����üƷ�ʱ��
		/// </summary>
		/// <returns></returns>
		public DateTime PreFixFeeDateTime;
		/// <summary>
		/// �̶����üƷѼ��
		/// </summary>
		public int FixFeeInterval;
        /// <summary>
        /// Ѫ���ɽ�
        /// </summary>
		public decimal BloodLateFee=0m;
		/// <summary>
		/// ���ѻ���ҩƷ���޶�
		/// </summary>
		public decimal Day_Limit = 0m;
		/// <summary>
		/// ���ѻ���ҩƷ���޶��ۼ�
		/// </summary>
		public decimal LimitTot=0m;
		/// <summary>
		/// ���ѻ���ҩƷ������
		/// </summary>
		public decimal Limit_OverTop = 0m;
		/// <summary>
		/// ����ҩƷ�ۼ�
		/// </summary>
		public decimal BursaryTotMedFee = 0m;
		/// <summary>
		/// ��λ����
		/// </summary>
		public decimal BedLimit =0m;
		/// <summary>
		/// �յ�����
		/// </summary>
		public decimal AirLimit = 0m;
		/// <summary>
		/// ��������������޶����
		/// </summary>
		public decimal AdjustOverTop = 0m;
		/// <summary>
		/// ��λ���괦�� 0���겻��1��������2���겻��(���������ʱ����)
		/// </summary>
		public string  BedOverDeal = "";
		/// <summary>
		/// �Էѱ���
		/// </summary>
		public decimal OwnRate = 1;
		/// <summary>
		/// �Ը�����
		/// </summary>
		public decimal PayRate = 0;

		/// <summary>
		/// ��ʳ�ܽ��
		/// </summary>
		public decimal BoardCost;
		/// <summary>
		/// ��ʳԤ����
		/// </summary>
		public decimal BoardPrepay;

		public  new FT Clone()
		{
			return base.Clone() as FT;
		}
	}
}
