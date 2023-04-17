using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Fee
{
	/// <summary>
	/// TransferFee<br></br>
	/// [��������: ת�������]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-01]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    /// 
    [System.Serializable]
	public class TransferFee : NeuObject
	{
   
		#region ����
		
		/// <summary>
		/// ת��ҽ�ƻ�����Ϣ
		/// </summary>
		private NeuObject source = new NeuObject();
		
		/// <summary>
		/// ��С������Ϣ, ���minFee.ID = "all"��ô�������з���
		/// </summary>
		private NeuObject minFee = new NeuObject();
		
		/// <summary>
		/// ת������ 1 ����ת�룬2 סԺת�� 3 ��Ժת��   
		/// </summary>
		private NeuObject transferType = new NeuObject();
		
		/// <summary>
		/// ��ͬ��λ��Ϣ
		/// </summary>
		private PactInfo pact = new PactInfo();
		
		/// <summary>
		/// ������Ϣ
		/// </summary>
		private FT ft = new FT();
		
		/// <summary>
		/// �������
		/// </summary>
		private string balanceNO;
		
		/// <summary>
		/// ����״̬
		/// </summary>
		private string balanceState;
		
		/// <summary>
		/// ������Ϣ(����Ա,����ʱ��,��������)
		/// </summary>
		private OperEnvironment oper = new OperEnvironment();

		#endregion
		
		#region ����
		
		/// <summary>
		/// ת��ҽ�ƻ�����Ϣ
		/// </summary>
		public NeuObject Source
		{
			get
			{
				return this.source;
			}
			set
			{
				this.source = value;
			}
		}
		
		/// <summary>
		/// ��С������Ϣ, ���minFee.ID = "all"��ô�������з���
		/// </summary>
		public NeuObject MinFee
		{
			get
			{
				return this.minFee;
			}
			set
			{
				this.minFee = value;
			}
		}

		/// <summary>
		/// ת������ 1 ����ת�룬2 סԺת�� 3 ��Ժת��   
		/// </summary>
		public NeuObject Type
		{
			get
			{
				return this.transferType;
			}
			set
			{
				this.transferType = value;
			}
		}
		
		/// <summary>
		/// ��ͬ��λ��Ϣ
		/// </summary>
		public PactInfo Pact
		{
			get
			{
				return this.pact;
			}
			set
			{
				this.pact = value;
			}
		}
		
		/// <summary>
		/// ������Ϣ
		/// </summary>
		public FT FT
		{
			get
			{
				return this.ft;
			}
			set
			{
				this.ft = value;
			}
		}
		
		/// <summary>
		/// �������
		/// </summary>
		public string BalanceNO
		{
			get
			{
				return this.balanceNO;
			}
			set
			{
				this.balanceNO = value;
			}
		}
		
		/// <summary>
		/// ����״̬
		/// </summary>
		public string BalanceState
		{
			get
			{
				return this.balanceState;
			}
			set
			{
				this.balanceState = value;
			}
		}
		
		/// <summary>
		/// ������Ϣ(����Ա,����ʱ��,��������)
		/// </summary>
		public OperEnvironment Oper
		{
			get
			{
				return this.oper;
			}
			set
			{
				this.oper = value;
			}
		}
		
		#endregion

		#region ����
		
		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ����ʵ������</returns>
		public new TransferFee Clone()
		{
			TransferFee transferFee = base.Clone() as TransferFee;

			transferFee.FT = this.FT.Clone();
			transferFee.MinFee = this.MinFee.Clone();
			transferFee.Oper = this.Oper.Clone();
			transferFee.Pact = this.Pact.Clone();
			transferFee.Source = this.Source.Clone();
			transferFee.Type = this.Type.Clone();

			return transferFee;
		}

		#endregion

		#endregion

		#region ��������,����
		/// <summary>
		/// ת��ҽ�ƻ�������
		/// </summary>
		[Obsolete("����,������Source.ID����", true)]
		public string  ChangeCode ;
		/// <summary>
		/// ��С����
		/// </summary>
		[Obsolete("����,������MinFee����", true)]
		public FS.FrameWork.Models.NeuObject Fee = new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// ת������
		/// </summary>
		[Obsolete("����,������Type����", true)]
		public FS.FrameWork.Models.NeuObject ChangeType = new FS.FrameWork.Models.NeuObject();
		
		/// <summary>
		/// ҽ����ˮ��
		/// </summary>
		[Obsolete("����,������base.ID����", true)]
		public string ClinicNo ;
		/// <summary>
		/// ��������
		/// </summary>
		[Obsolete("����,������Pact.PayKind����", true)]
		public FS.FrameWork.Models.NeuObject PayKind = new FS.FrameWork.Models.NeuObject();
		

		/// <summary>
		/// ���ý��
		/// </summary>
		[Obsolete("����,������FT.TotCost����", true)]
		public decimal TotCost ;
		/// <summary>
		/// �Էѽ��
		/// </summary>
		[Obsolete("����,������FT.OwnCost����", true)]
		public decimal OwnCost;
		/// <summary>
		/// �Ը����
		/// </summary>
		[Obsolete("����,������FT.PayCost����", true)]
	    public decimal PayCost;
		/// <summary>
		/// ���ѽ��
		/// </summary>
		[Obsolete("����,������FT.PubCost����", true)]
		public decimal pubCost;
		/// <summary>
		/// �Żݽ��
		/// </summary>
		[Obsolete("����,������FT.EcoCost����", true)]
		public decimal EcoCost;
		[Obsolete("����,������BalanceNO����", true)]
		public string BalanceNo;

		#endregion
	}
}
