using System;

namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// Report ��ժҪ˵����
	/// </summary>
	public class Report:Neusoft.HISFC.Object.RADT.PatientInfo,Neusoft.HISFC.Object.Fee.IFeeItem
	{
		public Report()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public new Report Clone() {
			Neusoft.HISFC.Object.Fee.Report obj=base.MemberwiseClone() as Report;
			
			//			obj.SIMainInfo = this.SIMainInfo.Clone();
			return obj;
		}
		#region IFeeItem ��Ա
		private decimal feeCost1 = 0m;
		private decimal feeCost2 = 0m;
		private decimal feeCost3 = 0m;
		private decimal feeCost4 = 0m;
		private decimal feeCost5 = 0m;
		private decimal feeCost6 = 0m;
		private decimal feeCost7 = 0m;
		private decimal feeCost8 = 0m;
		private decimal feeCost9 = 0m;
		private decimal feeCost10 = 0m;

		private string feeItem1 = "";
		private string feeItem2 = "";
		private string feeItem3 = "";
		private string feeItem4 = "";
		private string feeItem5 = "";
		private string feeItem6 = "";
		private string feeItem7 = "";
		private string feeItem8 = "";
		private string feeItem9 = "";
		private string feeItem10 = "";
		private string feeItem11 = "";
		private string feeItem12 = "";
		private string feeItem13 = "";
		private string feeItem14 = "";
		private string feeItem15 = "";
		private string feeItem16 = "";
		private string feeItem17 = "";
		private string feeItem18 = "";
		private string feeItem19 = "";
		private string feeItem20 = "";
		private string feeItem21 = "";

		public string FeeItem1 {
			get {
				// TODO:  ��� Report.FeeItem1 getter ʵ��
				return this.feeItem1;
			}
			set {
				// TODO:  ��� Report.FeeItem1 setter ʵ��
				this.feeItem1 = value;
			}
		}

		public string FeeItem2 {
			get {
				// TODO:  ��� Report.FeeItem2 getter ʵ��
				return this.feeItem2;
			}
			set {
				// TODO:  ��� Report.FeeItem2 setter ʵ��
				this.feeItem2 = value;
			}
		}

		public string FeeItem3 {
			get {
				// TODO:  ��� Report.FeeItem3 getter ʵ��
				return this.feeItem3;
			}
			set {
				// TODO:  ��� Report.FeeItem3 setter ʵ��
				this.feeItem3 = value;
			}
		}

		public string FeeItem4 {
			get {
				// TODO:  ��� Report.FeeItem4 getter ʵ��
				return this.feeItem4;
			}
			set {
				// TODO:  ��� Report.FeeItem4 setter ʵ��
				this.feeItem4 = value;
			}
		}

		public string FeeItem5 {
			get {
				// TODO:  ��� Report.FeeItem5 getter ʵ��
				return this.feeItem5;
			}
			set {
				// TODO:  ��� Report.FeeItem5 setter ʵ��
				this.feeItem5 = value;
			}
		}

		public string FeeItem6 {
			get {
				// TODO:  ��� Report.FeeItem6 getter ʵ��
				return this.feeItem6;
			}
			set {
				// TODO:  ��� Report.FeeItem6 setter ʵ��
				this.feeItem6 = value;
			}
		}

		public string FeeItem7 {
			get {
				// TODO:  ��� Report.FeeItem7 getter ʵ��
				return this.feeItem7;
			}
			set {
				// TODO:  ��� Report.FeeItem7 setter ʵ��
				this.feeItem7 = value;
			}
		}

		public string FeeItem8 {
			get {
				// TODO:  ��� Report.FeeItem8 getter ʵ��
				return this.feeItem8;
			}
			set {
				// TODO:  ��� Report.FeeItem8 setter ʵ��
				this.feeItem8 = value;
			}
		}

		public string FeeItem9 {
			get {
				// TODO:  ��� Report.FeeItem9 getter ʵ��
				return this.feeItem9;
			}
			set {
				// TODO:  ��� Report.FeeItem9 setter ʵ��
				this.feeItem9 = value;
			}
		}

		public string FeeItem10 {
			get {
				// TODO:  ��� Report.FeeItem10 getter ʵ��
				return this.feeItem10;
			}
			set {
				// TODO:  ��� Report.FeeItem10 setter ʵ��
				this.feeItem10 = value;
			}
		}

		public string FeeItem11 {
			get {
				// TODO:  ��� Report.FeeItem11 getter ʵ��
				return this.feeItem11;
			}
			set {
				// TODO:  ��� Report.FeeItem11 setter ʵ��
				this.feeItem11 = value;
			}
		}

		public string FeeItem12 {
			get {
				// TODO:  ��� Report.FeeItem12 getter ʵ��
				return this.feeItem12;
			}
			set {
				// TODO:  ��� Report.FeeItem12 setter ʵ��
				this.feeItem12 = value;
			}
		}

		public string FeeItem13 {
			get {
				// TODO:  ��� Report.FeeItem13 getter ʵ��
				return this.feeItem13;
			}
			set {
				// TODO:  ��� Report.FeeItem13 setter ʵ��
				this.feeItem13 = value;
			}
		}

		public string FeeItem14 {
			get {
				// TODO:  ��� Report.FeeItem14 getter ʵ��
				return this.feeItem14;
			}
			set {
				// TODO:  ��� Report.FeeItem14 setter ʵ��
				this.feeItem14 = value;
			}
		}

		public string FeeItem15 {
			get {
				// TODO:  ��� Report.FeeItem15 getter ʵ��
				return this.feeItem15;
			}
			set {
				// TODO:  ��� Report.FeeItem15 setter ʵ��
				this.feeItem15 = value;
			}
		}

		public string FeeItem16 {
			get {
				// TODO:  ��� Report.FeeItem16 getter ʵ��
				return this.feeItem16;
			}
			set {
				// TODO:  ��� Report.FeeItem16 setter ʵ��
				this.feeItem16 = value;
			}
		}

		public string FeeItem17 {
			get {
				// TODO:  ��� Report.FeeItem17 getter ʵ��
				return this.feeItem17;
			}
			set {
				// TODO:  ��� Report.FeeItem17 setter ʵ��
				this.feeItem17 = value;
			}
		}

		public string FeeItem18 {
			get {
				// TODO:  ��� Report.FeeItem18 getter ʵ��
				return this.feeItem18;
			}
			set {
				// TODO:  ��� Report.FeeItem18 setter ʵ��
				this.feeItem18 = value;
			}
		}

		public string FeeItem19 {
			get {
				// TODO:  ��� Report.FeeItem19 getter ʵ��
				return this.feeItem19;
			}
			set {
				// TODO:  ��� Report.FeeItem19 setter ʵ��
				this.feeItem19 = value;
			}
		}

		public string FeeItem20 {
			get {
				// TODO:  ��� Report.FeeItem20 getter ʵ��
				return this.feeItem20;
			}
			set {
				// TODO:  ��� Report.FeeItem20 setter ʵ��
				this.feeItem20 = value;
			}
		}

		public string FeeItem21 {
			get {
				// TODO:  ��� Report.FeeItem21 getter ʵ��
				return this.feeItem21;
			}
			set {
				// TODO:  ��� Report.FeeItem21 setter ʵ��
				this.feeItem21 = value;
			}
		}

		public decimal FeeCost1 {
			get {
				// TODO:  ��� Report.FeeCost1 getter ʵ��
				return this.feeCost1;
			}
			set {
				// TODO:  ��� Report.FeeCost1 setter ʵ��
				this.feeCost1 = value;
			}
		}

		public decimal FeeCost2 {
			get {
				// TODO:  ��� Report.FeeCost2 getter ʵ��
				return this.feeCost2;
			}
			set {
				// TODO:  ��� Report.FeeCost2 setter ʵ��
				this.feeCost2 = value;
			}
		}

		public decimal FeeCost3 {
			get {
				// TODO:  ��� Report.FeeCost3 getter ʵ��
				return this.feeCost3;
			}
			set {
				// TODO:  ��� Report.FeeCost3 setter ʵ��
				this.feeCost3 = value;
			}
		}

		public decimal FeeCost4 {
			get {
				// TODO:  ��� Report.FeeCost4 getter ʵ��
				return this.feeCost4;
			}
			set {
				// TODO:  ��� Report.FeeCost4 setter ʵ��
				this.feeCost4 = value;
			}
		}

		public decimal FeeCost5 {
			get {
				// TODO:  ��� Report.FeeCost5 getter ʵ��
				return this.feeCost5;
			}
			set {
				// TODO:  ��� Report.FeeCost5 setter ʵ��
				this.feeCost5 = value;
			}
		}

		public decimal FeeCost6 {
			get {
				// TODO:  ��� Report.FeeCost6 getter ʵ��
				return this.feeCost6;
			}
			set {
				// TODO:  ��� Report.FeeCost6 setter ʵ��
				this.feeCost6 = value;
			}
		}

		public decimal FeeCost7 {
			get {
				// TODO:  ��� Report.FeeCost7 getter ʵ��
				return this.feeCost7;
			}
			set {
				// TODO:  ��� Report.FeeCost7 setter ʵ��
				this.feeCost7 = value;
			}
		}

		public decimal FeeCost8 {
			get {
				// TODO:  ��� Report.FeeCost8 getter ʵ��
				return this.feeCost8;
			}
			set {
				// TODO:  ��� Report.FeeCost8 setter ʵ��
				this.feeCost8 = value;
			}
		}

		public decimal FeeCost9 {
			get {
				// TODO:  ��� Report.FeeCost9 getter ʵ��
				return this.feeCost9;
			}
			set {
				// TODO:  ��� Report.FeeCost9 setter ʵ��
				this.feeCost9 = value;
			}
		}

		public decimal FeeCost10 {
			get {
				// TODO:  ��� Report.FeeCost10 getter ʵ��
				return this.feeCost10;
			}
			set {
				// TODO:  ��� Report.FeeCost10 setter ʵ��
				this.feeCost10 = value;
			}
		}

		#endregion
	}


	/// <summary>
	/// ������С����21��Ŀ
	/// </summary>
	public interface IFeeItem {
		string FeeItem1{get;set;}
		string FeeItem2{get;set;}
		string FeeItem3{get;set;}
		string FeeItem4{get;set;}
		string FeeItem5{get;set;}
		string FeeItem6{get;set;}
		string FeeItem7{get;set;}
		string FeeItem8{get;set;}
		string FeeItem9{get;set;}
		string FeeItem10{get;set;}
		string FeeItem11{get;set;}
		string FeeItem12{get;set;}
		string FeeItem13{get;set;}
		string FeeItem14{get;set;}
		string FeeItem15{get;set;}
		string FeeItem16{get;set;}
		string FeeItem17{get;set;}
		string FeeItem18{get;set;}
		string FeeItem19{get;set;}
		string FeeItem20{get;set;}
		string FeeItem21{get;set;}

		decimal FeeCost1{get;set;}
		decimal FeeCost2{get;set;}
		decimal FeeCost3{get;set;}
		decimal FeeCost4{get;set;}
		decimal FeeCost5{get;set;}
		decimal FeeCost6{get;set;}
		decimal FeeCost7{get;set;}
		decimal FeeCost8{get;set;}
		decimal FeeCost9{get;set;}
		decimal FeeCost10{get;set;}
	}
}
