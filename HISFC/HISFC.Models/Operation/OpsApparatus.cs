using System;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Operation 
{
	/// <summary>
	/// OpsApparatus ��ժҪ˵����
	/// ���������豸ʵ����
	/// </summary>
    [Serializable]
    public class OpsApparatus : FS.HISFC.Models.Base.Spell 
	{

		public OpsApparatus()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		
		private string tradeMark = string.Empty;
		/// <summary>
		/// Ʒ��
		/// </summary>
		public string TradeMark
		{
			get
			{
				return this.tradeMark;
			}
			set
			{
				this.tradeMark = value;
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		public string AppaSource = "";

		/// <summary>
		/// �ͺ�
		/// </summary>
		public string AppaModel = "";
		private string model = string.Empty;
		public string Model
		{
			get
			{
				return this.model;
			}
			set
			{
				this.model = value;
			}
		}


		/// <summary>
		/// ��������
		/// </summary>
		public DateTime BuyDate = DateTime.MinValue;

		[Obsolete("Price",true)]
		public decimal AppaPrice = 0;
		private decimal price = 0;
		/// <summary>
		/// �۸�
		/// </summary>
		public decimal Price
		{
			get
			{
				return this.price;
			}
			set
			{
				this.price = value;
			}
		}
		

		[Obsolete("Unit",true)]
		public string AppaUnit = "";
		private string unit = string.Empty;
		/// <summary>
		/// ��λ
		/// </summary>
		public string Unit
		{
			get
			{
				return this.unit;
			}
			set
			{
				this.unit = value;
			}
		}


		/// <summary>
		/// 1����/0δ��
		/// </summary>
		private bool isValid = true;
		[Obsolete("IsValid",true)]
		public bool bStatus
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value;
			}
		}
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value;
			}
		}


		private string saler = string.Empty;
		/// <summary>
		/// ������
		/// </summary>
		public string Saler
		{
			get
			{
				return this.saler;
			}
			set
			{
				this.saler = value;
			}
		}


		/// <summary>
		/// ��������
		/// </summary>
		public string Producer = "";

		[Obsolete("Level",true)]
		public string AppaKind = "";
		private string level;
		/// <summary>
		/// ���� 1����,2��ͨ
		/// </summary>
		public string Level
		{
			get
			{
				return this.level;
			}
			set
			{
				this.level = value;
			}
		}


		private string remark = string.Empty;
		/// <summary>
		/// ��ע
		/// </summary>
		public string Remark
		{
			get
			{
				return this.remark;
			}
			set
			{
				this.remark = value;
			}
		}


		/// <summary>
		/// ����Ա
		/// </summary>
		public FS.HISFC.Models.Base.Employee User = new Employee();
		


		public new OpsApparatus Clone()
		{
			OpsApparatus myApparatus = base.Clone() as OpsApparatus;
			myApparatus.User = this.User.Clone();
			return myApparatus;
		}
	}
}
