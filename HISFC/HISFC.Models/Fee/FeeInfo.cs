using System;
using Neusoft.NFC.Object;
using Neusoft.HISFC.Object;
namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	///������Ϣ�� 
	///IDסԺ��ˮ��
	///name ��������
	///
	/// </summary>
	public class FeeInfo:Neusoft.NFC.Object.NeuObject,Neusoft.HISFC.Object.Base.IDept																						
	{
		/// <summary>
		/// ������Ŀ��Ϣ
		/// </summary>
		public FeeInfo()
		{
		}
		/// <summary>
		/// ������ˮ��
		/// </summary>
		public string NoteNO;
		/// <summary>
		/// סԺ����
		/// </summary>
		private NeuObject InpatientDept=new NeuObject();
		/// <summary>
		/// ��С����
		/// </summary>
		public NeuObject MinFee=new NeuObject();
		/// <summary>
		/// ����ҽ��
		/// </summary>
		private NeuObject Doctor=new NeuObject();
		/// <summary>
		/// ������Ϣ
		/// </summary>
		public FT Fee=new FT();
		/// <summary>
		/// �������
		/// </summary>
		public NeuObject PayKind=new NeuObject();
		/// <summary>
		/// ִ�п���
		/// </summary>
		protected NeuObject ExecDept=new NeuObject();
		/// <summary>
		/// ��ͬ��λ
		/// </summary>
		public Neusoft.NFC.Object.NeuObject  Pact=new NeuObject();
		/// <summary>
		/// �������
		/// </summary>
		public int BalanceSequance;
		/// <summary>
		/// ����״̬
		/// </summary>
		public string BalanceStatus;
		/// <summary>
		/// Ӥ�����
		/// </summary>
		public bool IsBaby;
		/// <summary>
		/// ��������
		/// </summary>
		private NeuObject RecDept = new NeuObject();
		/// <summary>
		/// �ۿ����
		/// </summary>
		private NeuObject StoDept = new NeuObject();
		/// <summary>
		/// �շ�ʱ��
		/// </summary>
		public DateTime DtFee = new DateTime();
		/// <summary>
		/// �������� 1 ������ 2 ������
		/// </summary>
		public string  TransType;
		/// <summary>
		/// ����Ա
		/// </summary>
		public NeuObject ChargeOper = new NeuObject();
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime  DtCharge;
		/// <summary>
		/// �շ�Ա
		/// </summary>
        public NeuObject FeeOper = new NeuObject();
		/// <summary>
		/// ����Ա
		/// </summary>
		public NeuObject BalanceOper = new NeuObject();
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime  DtBalance;
		/// <summary>
		/// ���㷢Ʊ��
		/// </summary>
		public string BalanceInvoice;
		/// <summary>
		/// ������
		/// </summary>
		public string CheckNo;
	    /// <summary>
	    /// ��ʿվ
	    /// </summary>
		private NeuObject NurseCell = new NeuObject();
		/// <summary>
		/// �շ�Ա����
		/// </summary>
		public NeuObject FeeOperDept = new NeuObject();

		/// <summary>
		/// ��չ��־:��ɽһ�������ô��ֶα�ʾ�Ƿ�Ϊ������ҩ(���Է���Ŀ) 0������,1����
		/// </summary>
		public string ExtFlag = "";
		/// <summary>
		/// ��չ��־1
		/// </summary>
		public string ExtFlag1 = "";
		/// <summary>
		/// ��չ��־2
		/// </summary>
		public string ExtFlag2 = "";
		/// <summary>
		/// ��չ����
		/// </summary>
		public string ExtCode = "";
		/// <summary>
		/// ��չ��Ա����
		/// </summary>
		public string ExtOperCode = "";
		/// <summary>
		/// ��չ����
		/// </summary>
		public DateTime ExtDate;

		#region IDept ��Ա

		public NeuObject InDept
		{
			get
			{
				// TODO:  ��� FeeInfo.Neusoft.HISFC.Object.Base.IDept.InDept getter ʵ��
				return this.InpatientDept;
			}
			set
			{
				InpatientDept = value;
				// TODO:  ��� FeeInfo.Neusoft.HISFC.Object.Base.IDept.InDept setter ʵ��
			}
		}

		public NeuObject ExeDept
		{
			get
			{
				// TODO:  ��� FeeInfo.ExeDept getter ʵ��
				return this.ExecDept;
			}
			set
			{
				// TODO:  ��� FeeInfo.ExeDept setter ʵ��
				ExecDept = value;
			}
		}

		public NeuObject ReciptDept
		{
			get
			{
				// TODO:  ��� FeeInfo.ReciptDept getter ʵ��
				return this.RecDept;
			}
			set
			{
				// TODO:  ��� FeeInfo.ReciptDept setter ʵ��
				this.RecDept = value;
			}
		}

		public NeuObject NurseStation
		{
			get
			{
				// TODO:  ��� FeeInfo.NurseStation getter ʵ��
				return this.NurseCell;
			}
			set
			{
				// TODO:  ��� FeeInfo.NurseStation setter ʵ��
				this.NurseCell =value;
			}
		}

		public NeuObject StockDept
		{
			get
			{
				// TODO:  ��� FeeInfo.StockDept getter ʵ��
				return this.StoDept;
			}
			set
			{
				// TODO:  ��� FeeInfo.StockDept setter ʵ��
				this.StoDept = value;
			}
		}

		public NeuObject ReciptDoct
		{
			get
			{
				// TODO:  ��� FeeInfo.ReciptDoc getter ʵ��
				return this.Doctor;
			}
			set
			{
				// TODO:  ��� FeeInfo.ReciptDoc setter ʵ��
				this.Doctor=value;
			}
		}

		#endregion
		public new FeeInfo Clone()
		{
			FeeInfo Obj = base.Clone() as FeeInfo;
			Obj.InpatientDept= this.InpatientDept.Clone();
			Obj.MinFee=this.MinFee.Clone();
			Obj.Doctor=this.Doctor.Clone();
			Obj.Fee=this.Fee.Clone();
			Obj.PayKind=this.PayKind.Clone();
			Obj.Pact=this.Pact.Clone();
			Obj.ExecDept=this.ExecDept.Clone();
			Obj.StoDept=this.StoDept.Clone();
			Obj.RecDept=this.RecDept.Clone();
			Obj.NurseCell=this.NurseCell.Clone();
			Obj.ChargeOper=this.ChargeOper.Clone();
			Obj.FeeOper=this.FeeOper.Clone();
			Obj.FeeOperDept=this.FeeOperDept.Clone();
			return Obj;
		}
	}
}
