using System;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// FT<br></br>
	/// [��������: ������Ϣ����]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2006-09-06]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class FT : NeuObject
	{
		#region ����
		
		/// <summary>
		/// �ܽ��
		/// </summary>
		private decimal totCost;
		
		/// <summary>
		/// �Էѽ��
		/// </summary>
		private decimal ownCost;
		
		/// <summary>
		/// �Ը����
		/// </summary>
		private decimal payCost;
		
		/// <summary>
		/// ���ѽ��,ҽ����Ч���
		/// </summary>
		private decimal pubCost;
		
		/// <summary>
		/// ʵ�����
		/// </summary>
		private decimal realCost;

		/// <summary>
		/// ʣ����
		/// </summary>
		private decimal leftCost;

		/// <summary>
		/// ������
		/// </summary>
		private decimal derateCost;

		/// <summary>
		/// ���ս��
		/// </summary>
		private decimal supplyCost;

		/// <summary>
		/// �������
		/// </summary>
		private decimal returnCost;

		/// <summary>
		/// Ԥ����
		/// </summary>
		private decimal prepayCost;

		/// <summary>
		/// �Żݽ��
		/// </summary>
		private decimal rebateCost;
		
		/// <summary>
		/// �Ѿ�����ķ���
		/// </summary>
		private decimal balancedCost;

		/// <summary>
		/// �Ѿ������Ԥ����
		/// </summary>
		private decimal balancedPrepayCost;

		/// <summary>
		/// ת���ܷ���
		/// </summary>
		private decimal transferTotCost;

		/// <summary>
		/// ת��Ԥ����(תѺ��)
		/// </summary>
		private decimal transferPrepayCost;

		/// <summary>
		/// Ѫ���ɽ�
		/// </summary>
		private decimal bloodLateFeeCost;

		/// <summary>
		/// ���ѻ���ҩƷ���޶�
		/// </summary>
		private decimal dayLimitCost;

		/// <summary>
		/// ���ѻ���ҩƷ���޶��ۼ�
		/// </summary>
		private decimal dayLimitTotCost;

		/// <summary>
		/// ���ѻ���ҩƷ������
		/// </summary>
		private decimal overtopCost;

		/// <summary>
		/// ����ҩƷ�ۼ�
		/// </summary>
		private decimal drugFeeTotCost;

		/// <summary>
		/// ��λ����
		/// </summary>
		private decimal bedLimitCost;

		/// <summary>
		/// �յ�����
		/// </summary>
		private decimal airLimitCost;

        /// <summary>
        ///  ��λ���괦�� 0���겻��1��������2���겻��(���������ʱ����)
        /// </summary>
        private string bedOverDeal;

		/// <summary>
		/// ��������������޶����
		/// </summary>
		private decimal adjustOvertopCost;

		/// <summary>
		/// ҩƷ������
		/// </summary>
		private decimal excessCost;

		/// <summary>
		/// �Է�ҩ���
		/// </summary>
		private decimal drugOwnCost;
		
		/// <summary>
		/// ��ʳ�ܽ��
		/// </summary>
		private decimal boardCost;

		/// <summary>
		/// ��ʳԤ����
		/// </summary>
		private decimal boardPrepayCost;

		/// <summary>
		/// �ϴι̶����üƷ�ʱ��
		/// </summary>
		/// <returns></returns>
		private DateTime preFixFeeDateTime;

		/// <summary>
		/// �̶����üƷѼ��
		/// </summary>
		private int fixFeeInterval;
		
		/// <summary>
		/// ������Ϣ
		/// </summary>
		private FTRate ftRate = new FTRate();

		#endregion

		#region ����

		/// <summary>
		/// �ܽ��
		/// </summary>
		public decimal TotCost
		{
			get
			{
				return this.totCost;
			}
			set
			{
				this.totCost = value;
			}
		}
		
		/// <summary>
		/// �Էѽ��
		/// </summary>
		public decimal OwnCost
		{
			get
			{
				return this.ownCost;
			}
			set
			{
				this.ownCost = value;
			}
		}
		
		/// <summary>
		/// �Ը����
		/// </summary>
		public decimal PayCost
		{
			get
			{
				return this.payCost;
			}
			set
			{
				this.payCost = value;
			}
		}
		
		/// <summary>
		/// ���ѽ��,ҽ����Ч���
		/// </summary>
		public decimal PubCost
		{
			get
			{
				return this.pubCost;
			}
			set
			{
				this.pubCost = value;
			}
		}

		/// <summary>
		/// ʵ�����
		/// </summary>
		public decimal RealCost
		{
			get
			{
				return this.realCost;
			}
			set
			{
				this.realCost = value;
			}
		}

		/// <summary>
		/// ʣ����
		/// </summary>
		public decimal LeftCost
		{
			get
			{
				return this.leftCost;
			}
			set
			{
				this.leftCost = value;
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		public decimal DerateCost
		{
			get
			{
				return this.derateCost;
			}
			set
			{
				this.derateCost = value;
			}
		}

		/// <summary>
		/// ���ս��
		/// </summary>
		public decimal SupplyCost
		{
			get
			{
				return this.supplyCost;
			}
			set
			{
				this.supplyCost = value;
			}
		}

		/// <summary>
		/// �������
		/// </summary>
		public decimal ReturnCost
		{
			get
			{
				return this.returnCost;
			}
			set
			{
				this.returnCost = value;
			}
		}

		/// <summary>
		/// Ԥ����
		/// </summary>
		public decimal PrepayCost
		{
			get
			{
				return this.prepayCost;
			}
			set
			{
				this.prepayCost = value;
			}
		}

		/// <summary>
		/// �Żݽ��
		/// </summary>
		public decimal RebateCost
		{
			get
			{
				return this.rebateCost;
			}
			set
			{
				this.rebateCost = value;
			}
		}
		
		/// <summary>
		/// �Ѿ�����ķ���
		/// </summary>
		public decimal BalancedCost
		{
			get
			{
				return this.balancedCost;
			}
			set
			{
				this.balancedCost = value;
			}
		}

		/// <summary>
		/// �Ѿ������Ԥ����
		/// </summary>
		public decimal BalancedPrepayCost
		{
			get
			{
				return this.balancedPrepayCost;
			}
			set
			{
				this.balancedPrepayCost = value;
			}
		}

		/// <summary>
		/// ת���ܷ���
		/// </summary>
		public decimal TransferTotCost
		{
			get
			{
				return this.transferTotCost;
			}
			set
			{
				this.transferTotCost = value;
			}
		}

		/// <summary>
		/// ת��Ԥ����(תѺ��)
		/// </summary>
		public decimal TransferPrepayCost
		{
			get
			{
				return this.transferPrepayCost;
			}
			set
			{
				this.transferPrepayCost = value;
			}
		}
		/// <summary>
		/// Ѫ���ɽ�
		/// </summary>
		public decimal BloodLateFeeCost
		{
			get
			{
				return this.bloodLateFeeCost;
			}
			set
			{
				this.bloodLateFeeCost = value;
			}
		}

		/// <summary>
		/// ���ѻ���ҩƷ���޶�
		/// </summary>
		public decimal DayLimitCost
		{
			get
			{
				return this.dayLimitCost;
			}
			set
			{
				this.dayLimitCost = value;
			}
		}

		/// <summary>
		/// ���ѻ���ҩƷ���޶��ۼ�
		/// </summary>
		public decimal DayLimitTotCost
		{
			get
			{
				return this.dayLimitTotCost;
			}
			set
			{
				this.dayLimitTotCost = value;
			}
		}

		/// <summary>
		/// ���ѻ���ҩƷ������
		/// </summary>
		public decimal OvertopCost
		{
			get
			{
				return this.overtopCost;
			}
			set
			{
				this.overtopCost = value;
			}
		}

		/// <summary>
		/// ����ҩƷ�ۼ�
		/// </summary>
		public decimal DrugFeeTotCost
		{
			get
			{
				return this.drugFeeTotCost;
			}
			set
			{
				this.drugFeeTotCost = value;
			}
		}

		/// <summary>
		/// ��λ����
		/// </summary>
		public decimal BedLimitCost
		{
			get
			{
				return this.bedLimitCost;
			}
			set
			{
				this.bedLimitCost = value;
			}
		}

		/// <summary>
		/// �յ�����
		/// </summary>
		public decimal AirLimitCost
		{
			get
			{
				return this.airLimitCost;
			}
			set
			{
				this.airLimitCost = value;
			}
		}

		/// <summary>
		/// ��������������޶����
		/// </summary>
		public decimal AdjustOvertopCost
		{
			get
			{
				return this.adjustOvertopCost;
			}
			set
			{
				this.adjustOvertopCost = value;
			}
		}

		/// <summary>
		/// ҩƷ������
		/// </summary>
		public decimal ExcessCost
		{
			get
			{
				return this.excessCost;
			}
			set
			{
				this.excessCost = value;
			}
		}

		/// <summary>
		/// �Է�ҩ���
		/// </summary>
		public decimal DrugOwnCost
		{
			get
			{
				return this.drugOwnCost;
			}
			set
			{
				this.drugOwnCost = value;
			}
		}
		
		/// <summary>
		/// ��ʳ�ܽ��
		/// </summary>
		public decimal BoardCost
		{
			get
			{
				return this.boardCost;
			}
			set
			{
				this.boardCost = value;
			}
		}

		/// <summary>
		/// ��ʳԤ����
		/// </summary>
		public decimal BoardPrepayCost
		{
			get
			{
				return this.boardPrepayCost;
			}
			set
			{
				this.boardPrepayCost = value;
			}
		}

		/// <summary>
		/// �ϴι̶����üƷ�ʱ��
		/// </summary>
		/// <returns></returns>
		public DateTime PreFixFeeDateTime
		{
			get
			{
				return this.preFixFeeDateTime;
			}
			set
			{
				this.preFixFeeDateTime = value;
			}
		}

		/// <summary>
		/// �̶����üƷѼ��
		/// </summary>
		public int FixFeeInterval
		{
			get
			{
				return this.fixFeeInterval;
			}
			set
			{
				this.fixFeeInterval = value;
			}
		}

		/// <summary>
		/// ��λ���괦�� 0���겻��1��������2���겻��(���������ʱ����)
		/// </summary>
		public string  BedOverDeal
		{
			get
			{
                return this.bedOverDeal;
			}
			set
			{
				this.bedOverDeal = value;
			}
		}

		/// <summary>
		/// ������Ϣ
		/// </summary>
		public FTRate FTRate
		{
			get
			{
				return this.ftRate;
			}
			set
			{
				this.ftRate = value;
			}
		}

		#endregion

		#region ����

		#region ��¡

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ����ʵ���ĸ���</returns>
		public  new FT Clone()
		{
			FT ft = base.Clone() as FT;

			ft.FTRate = this.FTRate.Clone();

			return ft;
		}

		#endregion

		#endregion

		#region ��������
		
		/// <summary>
		/// �ܹ����
		/// </summary>
		[Obsolete("����,TotCost", true)]
		public decimal Tot_Cost;
		/// <summary>
		/// �Էѽ��
		/// </summary>
		 [Obsolete("����,OwnCost", true)]
		public decimal Own_Cost;
		/// <summary>
		/// �Ը����
		/// </summary>
		[Obsolete("����,payCost", true)]
		public decimal Pay_Cost;
		/// <summary>
		/// ���ѽ��
		/// </summary>
		[Obsolete("����,pubCost", true)]
		public decimal Pub_Cost;
		/// <summary>
		/// ʣ����
		/// </summary>
		[Obsolete("����,LeftCost", true)]
		public decimal Left_Cost;
		/// <summary>
		/// ������
		/// </summary>
		[Obsolete("����,DerateCost", true)]
		public decimal Dereate_Cost;
		/// <summary>
		/// ���ս��
		/// </summary>
		[Obsolete("����,SupplyCost", true)]
		public decimal Supply_Cost;
		/// <summary>
		/// �������
		/// </summary>
		[Obsolete("����,ReturnCost", true)]
		public decimal Return_Cost;
		/// <summary>
		/// Ԥ����
		/// </summary>
		[Obsolete("����,PrepayCost", true)]
		public decimal Prepay_Cost;
		/// <summary>
		/// �Żݽ��
		/// </summary>
		[Obsolete("����,RebateCost", true)]
		public decimal Rebate_Cost;
	
		/// <summary>
		/// �Ѿ�����ķ���
		/// </summary>
		[Obsolete("����,BalancedCost", true)]
		public decimal Balance_Cost;
		/// <summary>
		/// �Ѿ������Ԥ����
		/// </summary>
		[Obsolete("����,BalancedPrepayCost", true)]
		public decimal Balance_Prepay;
		/// <summary>
		/// ת���ܷ���
		/// </summary>
		[Obsolete("����,TransferTotCost", true)]
		public decimal ChangeTotCost;
		/// <summary>
		/// ת��Ԥ����
		/// </summary>
		[Obsolete("����,TransferPrepayCost", true)]
		public decimal ChangePrepay;
		/// <summary>
		/// תѺ��
		/// </summary>
		[Obsolete("����", true)]
		public decimal TransPrepay;
		
        /// <summary>
        /// Ѫ���ɽ�
        /// </summary>
        [Obsolete("����,BloodLateFeeCost", true)]
		public decimal BloodLateFee=0m;
		/// <summary>
		/// ���ѻ���ҩƷ���޶�
		/// </summary>
		 [Obsolete("����,DayLimitCost", true)]
		public decimal Day_Limit = 0m;
		/// <summary>
		/// ���ѻ���ҩƷ���޶��ۼ�
		/// </summary>
		 [Obsolete("����,DayLimitTotCost", true)]
		public decimal LimitTot=0m;
		/// <summary>
		/// ���ѻ���ҩƷ������
		/// </summary>
		[Obsolete("����,overTopCost", true)]
		public decimal Limit_OverTop = 0m;
		/// <summary>
		/// ����ҩƷ�ۼ�
		/// </summary>
		[Obsolete("����,DrugFeeTotCost", true)]
		public decimal BursaryTotMedFee = 0m;
		/// <summary>
		/// ��λ����
		/// </summary>
		[Obsolete("����,BedLimitCost", true)]
		public decimal BedLimit =0m;
		/// <summary>
		/// �յ�����
		/// </summary>
		[Obsolete("����,AirLimitCost", true)]
		public decimal AirLimit = 0m;
		/// <summary>
		/// ��������������޶����
		/// </summary>
		[Obsolete("����,AdjustOvertopCost", true)]
		public decimal AdjustOverTop = 0m;
		
		/// <summary>
		/// �Էѱ���
		/// </summary>
		[Obsolete("���� FTRate", true)]
		public decimal OwnRate = 1;
		/// <summary>
		/// �Ը�����
		/// </summary>
		[Obsolete("���� FTRate", true)]
		public decimal PayRate = 0;

		
		/// <summary>
		/// ��ʳԤ����
		/// </summary>
		[Obsolete("���� BoardPrepayCost", true)]
		public decimal BoardPrepay;

		#endregion
	}
}
