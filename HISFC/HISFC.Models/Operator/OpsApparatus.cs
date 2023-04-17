using System;

namespace neusoft.HISFC.Object.Operator
{
	/// <summary>
	/// OpsApparatus ��ժҪ˵����
	/// ���������豸ʵ����
	/// </summary>
	public class OpsApparatus : neusoft.neuFC.Object.neuObject,neusoft.HISFC.Object.Base.ISpellCode
	{
		public OpsApparatus()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ƴ����
		/// </summary>
		private string mySpellCode = "";
		/// <summary>
		/// ������
		/// </summary>
		private string myUserCode = "";
		/// <summary>
		/// Ʒ��
		/// </summary>
		public string TradeMark = "";
		/// <summary>
		/// ����
		/// </summary>
		public string AppaSource = "";
		/// <summary>
		/// �ͺ�
		/// </summary>
		public string AppaModel = "";
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime BuyDate = DateTime.MinValue;
		/// <summary>
		/// �۸�
		/// </summary>
		public decimal AppaPrice = 0;
		/// <summary>
		/// ��λ
		/// </summary>
		public string AppaUnit = "";
		/// <summary>
		/// 1����/0δ��
		/// </summary>
		private string Status = "1";
		public bool bStatus
		{
			get
			{
				if(Status == "1")
					return true;
				else
					return false;
			}
			set
			{
				if(value == true)
					Status = "1";
				else
					Status = "0";
			}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string Saler = "";
		/// <summary>
		/// ��������
		/// </summary>
		public string Producer = "";
		/// <summary>
		/// 1����2��ͨ
		/// </summary>
		public string AppaKind = "";
		/// <summary>
		/// ��ע
		/// </summary>
		public string Remark = "";
		/// <summary>
		/// ����Ա
		/// </summary>
		public neusoft.HISFC.Object.RADT.Person User = new neusoft.HISFC.Object.RADT.Person();
		#region ISpellCode ��Ա
		/// <summary>
		/// ƴ����
		/// </summary>
		public string Spell_Code
		{
			get
			{
				// TODO:  ��� OpsApparatus.Spell_Code getter ʵ��
				return this.mySpellCode;
			}
			set
			{
				this.mySpellCode = value;
			}
		}

		public string WB_Code
		{
			get
			{
				// TODO:  ��� OpsApparatus.WB_Code getter ʵ��
				return null;
			}
			set
			{
				// TODO:  ��� OpsApparatus.WB_Code setter ʵ��
			}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string User_Code
		{
			get
			{
				// TODO:  ��� OpsApparatus.User_Code getter ʵ��
				return this.myUserCode;
			}
			set
			{
				this.myUserCode = value;
			}
		}

		#endregion

		public new OpsApparatus Clone()
		{
			OpsApparatus myApparatus = new OpsApparatus();
			myApparatus.ID = this.ID.ToString();
			myApparatus.Name = this.Name;
			myApparatus.mySpellCode = this.mySpellCode;
			myApparatus.myUserCode = this.myUserCode;
			myApparatus.TradeMark = this.TradeMark;
			myApparatus.AppaSource = this.AppaSource;
			myApparatus.AppaModel = this.AppaModel;
			myApparatus.BuyDate = this.BuyDate;
			myApparatus.AppaPrice = this.AppaPrice;
			myApparatus.AppaUnit = this.AppaUnit;
			myApparatus.Status = this.Status;
			myApparatus.Saler = this.Saler;
			myApparatus.Producer = this.Producer;
			myApparatus.AppaKind = this.AppaKind;
			myApparatus.Remark = this.Remark;
			myApparatus.User = this.User.Clone();
			return myApparatus;
		}
	}
}
