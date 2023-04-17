using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Fee
{
	/// <summary>
	/// TransferPrepay<br></br>
	/// [��������: ת��Ԥ������]<br></br>
	/// [�� �� ��: ����]<br></br>
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
	public class TransferPrepay : NeuObject 
	{
		#region ����
		
		/// <summary>
		/// ת��ҽ�ƻ�����Ϣ
		/// </summary>
		private NeuObject source = new NeuObject();
		
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
		public new TransferPrepay Clone()
		{
			TransferPrepay transferPrepay = base.Clone() as TransferPrepay;

			transferPrepay.FT = this.FT.Clone();
			transferPrepay.Oper = this.Oper.Clone();
			transferPrepay.Pact = this.Pact.Clone();
			transferPrepay.Source = this.Source.Clone();
			transferPrepay.Type = this.Type.Clone();

			return transferPrepay;
		}

		#endregion

		#endregion
		
		#region ��������,����
		/// <summary>
		/// ת������
		/// </summary>
		[Obsolete("����,������Type.ID����", true)]
		public string ChangeType;
		/// <summary>
		/// ҽ����ˮ��
		/// </summary>
		[Obsolete("����,������base.ID����", true)]
		public string ClinicNo;
	
		/// <summary>
		/// �������
		/// </summary>
		[Obsolete("����,������Pact.PayKind����", true)]
		public FS.HISFC.Models.Base.PayKind payKind = new FS.HISFC.Models.Base.PayKind();
		
		/// <summary>
		/// ת��Ԥ�����
		/// </summary>
		[Obsolete("����,������FT.TransPrepay����", true)]
		public decimal ChangePrepayCost = 0m;
		/// <summary>
		/// ת�����Ա
		/// </summary>
		[Obsolete("����,������Oper����", true)]
		public FS.FrameWork.Models.NeuObject ChangeOper =new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// ת��ʱ��
		/// </summary>
		[Obsolete("����,������Oper.OperTime����", true)]
		public DateTime ChangeOperDate;

		#endregion
	}
}
