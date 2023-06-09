using System;

namespace neusoft.HISFC.Object.Check
{
	/// <summary>
	/// ChkItemList 的摘要说明。
	/// </summary>
	public class ChkItemList: neusoft.neuFC.Object.neuObject
	{
		public ChkItemList()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		
		private string unitFlag ;//单位标识 1 药品 2 非药品 3明细 4组套
		private string clinicNo ;//体检序号
		private string cardNo;//就诊卡号
//		private int amount;//数量
		private decimal ecoRate;//优惠比率
		private string comNO; //组合号
		private string sequenceNo; //序列号
		private neusoft.neuFC.Object.neuObject conformOper = null; //确认人
		private neusoft.neuFC.Object.neuObject oper   = null;//操作员
		private  neusoft.neuFC.Object.neuObject execDept = null; //执行科室
		private neusoft.HISFC.Object.Fee.Item item = null; //项目
		private string extFlag;//  扩展标志                                
		private decimal extNumber;// 扩展标志                                
		private string extChar;//  扩展字符字段                            
		private string extFlag1;// 扩展标志                                
		private decimal extNumber1;//扩展标志                                
		private string extChar1;//   扩展字符字段 
		private System.DateTime conformDate ;
		private string confirmFlag;
		/// <summary>
		/// 确认标志
		/// </summary>
		public string ConfirmFlag
		{
			get
			{
				return  confirmFlag;
			}
			set
			{
				confirmFlag = value;
			}
		}
		/// <summary>
		/// 确认日期
		/// </summary>
		public System.DateTime ConformDate
		{
			get
			{
				return conformDate;
			}
			set
			{
				conformDate = value;
			}
		}
		/// <summary>
		/// 扩展标志
		/// </summary>
		public string ExtChar1
		{
			get
			{
				return extChar1;
			}
			set
			{
				extChar1 = value;
			}
		}
		/// <summary>
		/// 扩展标志
		/// </summary>
		public string ExtChar
		{
			get
			{
				return extChar;
			}
			set
			{
				extChar = value;
			}
		}
		/// <summary>
		/// 扩展标志
		/// </summary>
		public decimal ExtNumber1
		{
			get
			{
				return extNumber1;
			}
			set
			{
				extNumber1 = value;
			}
		}
		/// <summary>
		/// 扩展标志
		/// </summary>
		public decimal ExtNumber
		{
			get
			{
				return extNumber;
			}
			set
			{
				extNumber = value;
			}
		}
		/// <summary>
		/// 扩展标志
		/// </summary>
		public string ExtFlag1
		{
			get
			{
				return extFlag1;
			}
			set
			{
				extFlag1 = value;
			}
		}
		/// <summary>
		/// 扩展标志
		/// </summary>
		public string ExtFlag
		{
			get
			{
				return extFlag;
			}
			set
			{
				extFlag = value;
			}
		}
		/// <summary>
		/// 序列号
		/// </summary>
		public string SequenceNo
		{
			get
			{
				return sequenceNo;
			}
			set
			{
				sequenceNo = value;
			}
		}

		/// <summary>
		/// 组合号
		/// </summary>
		public string ComNo
		{
			get
			{
				return comNO;
			}
			set
			{
				comNO = value;
			}
		}
		/// <summary>
		/// 项目
		/// </summary>
		public neusoft.HISFC.Object.Fee.Item Item
		{
			get
			{
				if(item == null)
				{
					item  = new neusoft.HISFC.Object.Fee.Item();
				}
				return item;
			}
			set
			{
				item = value;
			}
		}
		/// <summary>
		/// 执行科室
		/// </summary>
		public neusoft.neuFC.Object.neuObject ExecDept
		{
			get
			{
				if(execDept == null)
				{
					execDept = new neusoft.neuFC.Object.neuObject();
				}
				return execDept;
			}
			set
			{
				execDept = value;
			}
		}
		/// <summary>
		/// 操作员
		/// </summary>
		public  neusoft.neuFC.Object.neuObject Oper
		{
			get
			{
				if(oper == null)
				{
					oper = new neusoft.neuFC.Object.neuObject();
				}
				return oper;
			}
			set
			{
				oper = value;
			}
		}
		/// <summary>
		/// 优惠比率
		/// </summary>
		public decimal EcoRate
		{
			get
			{
				return ecoRate;
			}
			set
			{
				ecoRate = value;
			}
		}
		//		/// <summary>
		//		/// 数量
		//		/// </summary>
		//		public int Amount
		//		{
		//			get
		//			{
		//				return amount;
		//			
		//			}
		//			set
		//			{
		//				amount  = value;
		//			}
		//
		//		}
				/// <summary>
				/// 确认人
				/// </summary>
		public neusoft.neuFC.Object.neuObject ConformOper
		{
			get
			{
				if(conformOper == null)
				{
					conformOper = new neusoft.neuFC.Object.neuObject();
				}
				return conformOper;
			}
			set
			{
				conformOper =  value;
			}
		}
		/// <summary>
		/// 就诊卡号
		/// </summary>
		public string CardNo
		{
			get
			{
				return cardNo;
			}
			set
			{
				cardNo = value;
			}
		}
		/// <summary>
		/// 体检序号
		/// </summary>
		public string ClinicNo
		{
			get
			{
				return clinicNo;
			}
			set
			{
				clinicNo = value;
			}
		}
		/// <summary>
		/// 单位标识 0药品/1 非药品/2组套/3复合项目
		/// </summary>
		public string UnitFlag
		{
			get
			{
				return unitFlag;
			}
			set
			{
				unitFlag = value;
			}
		}
		public new ChkItemList Clone()
		{
			ChkItemList obj = base.Clone() as ChkItemList;
			obj.item= this.item.Clone();
			obj.ExecDept=this.ExecDept.Clone();//(neusoft.HISFC.Object.Fee.Invoice)Invoice.Clone();
			obj.Oper=this.Oper.Clone();
			obj.ConformOper = this.ConformOper.Clone();
			return obj;
		}
		
	}
}
