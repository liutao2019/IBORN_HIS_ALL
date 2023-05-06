using System;
using Neusoft.FrameWork.Models;

namespace HISTIMEJOB.XNH.Models
{
	/// <summary>
	/// PYNBMainInfo 的摘要说明。
	/// Id inpatientNo, name 患者姓名
	/// </summary>
    public class PYNBMainInfo : Neusoft.FrameWork.Models.NeuObject
	{
		public PYNBMainInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑   
			//
		}
		private int appl_year;
		/// <summary>
		/// 投保年度
		/// </summary>
		public int Appl_year
		{
			set
			{
				appl_year = value;
			}
			get
			{
				return appl_year;
			}
		}
		private string branch_no;
		/// <summary>
		/// 投保镇代码
		/// </summary>
		public string Branch_no
		{
			get
			{
				return branch_no;
			}
			set
			{
				branch_no = value;
			}
		}

		private string country_no;
		/// <summary>
		/// 投保村代码
		/// </summary>
		public string Country_no
		{
			set
			{
				country_no = value;
			}
			get
			{
				return country_no;
			}
		}

		private string poli_no;
		/// <summary>
		/// 投保交费清单编号
		/// </summary>
		public string Poli_no
		{
			set{poli_no = value;}
			get{return poli_no;}
		}

		private string hu_no;
		/// <summary>
		///  户口号  只可为9位
		/// </summary>
		public string Hu_no
		{
			get
			{
				
				return hu_no;
			}
			set{hu_no = value;}
		}

		private string man_no;
		/// <summary>
		/// 被保人编号
		/// </summary>
		public string Man_no
		{
			get{return man_no;}
			set{man_no = value;}
		}
		
		private string card_no;
		/// <summary>
		/// 被保人农合卡
		/// </summary>
		public string Card_no
		{
			get{return card_no;}
			set{card_no = value;}
		}

		private string man_type;
		/// <summary>
		/// -被保人类别
		/// </summary>
		public string Man_type
		{
			get{return man_type;}
			set{man_type = value;}
		}

		private string man_name;
		/// <summary>
		/// 姓名
		/// </summary>
		public string Man_name
		{
			get{return man_name;}
			set{man_name = value;}
		}

		private string man_id;
		/// <summary>
		/// 身份证号
		/// </summary>
		public string Man_id
		{
			get{return man_id;}
			set{man_id = value;}
		}

		private string man_sex;
		/// <summary>
		/// 性    别 M.男  F.女
		/// </summary>
		public string Man_sex
		{
			get{return man_sex;}
			set{man_sex = value;}
		}

		private DateTime man_birth;
		/// <summary>
		/// 出生日期
		/// </summary>
		public DateTime Man_birth
		{
			get{return man_birth;}
			set{man_birth = value;}
		}		

		private decimal man_age;
		/// <summary>
		/// 承保年龄
		/// </summary>
		public decimal Man_age
		{
			get{return man_age;}
			set{man_age = value;}
		}

		private DateTime work_date;
		/// <summary>
		/// 录入日期
		/// </summary>
		public DateTime Work_date
		{
			get{return work_date;}
			set{work_date = value;}
		}

		private string bank_type;
		/// <summary>
		/// 划帐方式:0无账号 A付现金
		///--B划个人帐 C划公司帐(或组帐）
		/// </summary>
		public string Bank_type
		{
			get{return bank_type;}
			set{bank_type = value;}
		}

		private string bank_fg;
		/// <summary>
		/// --银行代号
		/// --现金帐:A-付支票 B-付现金
		/// --个人帐:中行(1-6) 其它银行(A-Z);
		/// --公司帐:公司帐户的编号(1-9);

		/// </summary>
		public string Bank_fg
		{
			get{return bank_fg;}
			set{bank_fg = value;}
		}

		private string acct_name;
		/// <summary>
		/// -账号开户者名称
		/// </summary>
		public string Acct_name
		{
			get{return acct_name;}
			set{acct_name = value;}
		}

		private string bank_acct;
		/// <summary>
		/// 银行账号
		/// </summary>
		public string Bank_acct
		{
			get{return bank_acct;}
			set{bank_acct = value;}
		}

		private string bank_name;
		/// <summary>
		/// 开户银行名称
		/// </summary>
		public string Bank_name
		{
			get{return bank_name;}
			set{bank_name = value;}
		}

		private DateTime poli_date;
		/// <summary>
		/// 医疗险起保日期
		/// </summary>
		public DateTime Poli_date
		{
			set{poli_date = value;}
			get{return poli_date;}
		}

		private DateTime end_date;
		/// <summary>
		/// 医疗险到期日期
		/// </summary>
		public DateTime End_date
		{
			get{return end_date;}
			set{end_date = value;}
		}
		
		private string medi_mark;
		/// <summary>
		/// 保险类别 0停保 A-Z类别
		/// </summary>
		public string Medi_mark
		{
			set
			{
				medi_mark = value;
			}
			get
			{
				return medi_mark;
			}
		}
		
		private decimal poli_sum;
		/// <summary>
		/// 意外死亡保额
		/// </summary>
		public decimal Poli_sum
		{
			get{return poli_sum;}
			set{poli_sum = value;}
		}

		private decimal medi_sum;
		/// <summary>
		/// 医疗保险保额
		/// </summary>
		public decimal Medi_sum
		{
			get{return medi_sum;}
			set{medi_sum = value;}
		}

		private decimal pay_sum;
		/// <summary>
		/// 合计保险保费
		/// </summary>
		public decimal Pay_sum
		{
			get{return pay_sum;}
			set{pay_sum = value;}
		}

		private string remarks;
		/// <summary>
		/// 备    注
		/// </summary>
		public string Remarks
		{
			get
			{
				return remarks;
			}
			set
			{
				remarks = value;
			}
		}

		private string stuff_no;
		/// <summary>
		/// 操 作 员
		/// </summary>
		public string Stuff_no
		{
			set{stuff_no = value;}
			get{return stuff_no;}
		}
		
		private DateTime sys_date;
		/// <summary>
		/// 系统日期
		/// </summary>
		public DateTime Sys_date
		{
			set
			{
				sys_date = value;
			}
			get
			{
				return sys_date;
			}
		}

		private string n_hosp_code;
		/// <summary>
		/// 当前住院代码
		/// </summary>
		public string N_hosp_code
		{
			get
			{
				return n_hosp_code;
			}
			set
			{
				n_hosp_code = value;
			}
		}

		private decimal year_insur_sum;
		/// <summary>
		/// 本年累计费用
		/// </summary>
		public decimal Year_insur_sum
		{
			get
			{
				return year_insur_sum;
			}
			set
			{
				year_insur_sum = value;
			}
		}

		private decimal yu_insur_sum;
		/// <summary>
		/// 本次预结费用
		/// </summary>
		public decimal Yu_insur_sum
		{
			get
			{
				return yu_insur_sum;
			}
			set
			{
				yu_insur_sum = value;
			}
		}

		private DateTime stock_date;
		/// <summary>
		/// 入院日期
		/// </summary>
		public DateTime Stock_date
		{
			get
			{
				return stock_date;
			}
			set
			{
				stock_date = value;
			}
		}

		private DateTime out_date;
		/// <summary>
		/// 出院日期
		/// </summary>
		public DateTime Out_date
		{
			get
			{
				return out_date;
			}
			set
			{
				out_date = value;
			}

		}
		private string medi_no;
		/// <summary>
		/// 就医登记号
		/// </summary>
		public string Medi_no
		{
			get
			{
				return medi_no;
			}
			set
			{
				medi_no = value;
			}
		}

		private string medi_nn;
		/// <summary>
		/// 住院号
		/// </summary>
		public string Medi_nn
		{
			get
			{
				return medi_nn;
			}
			set
			{
				medi_nn = value;
			}
		}

		private string medi_sn;
		/// <summary>
		/// 流水号
		/// </summary>
		public string Medi_sn
		{
			get
			{
				return medi_sn;
			}
			set
			{
				medi_sn = value;
			}
		}
		private string hosp_code;
		/// <summary>
		/// 医院代码
		/// </summary>
		public string Hosp_code
		{
			get
			{
				return hosp_code;
			}
			set
			{
				hosp_code = value;
			}
		}
		
		private string zykb;
		/// <summary>
		/// 入住科别
		/// </summary>
		public string Zykb
		{
			get
			{
				return zykb;
			}
			set
			{
				zykb = value;
			}
		}

		private string jyzd;
		/// <summary>
		/// 入院诊断
		/// </summary>
		public string Jyzd
		{
			get
			{
				return jyzd;
			}
			set
			{
				jyzd = value;
			}
		}

		private string turn_flag;
		/// <summary>
		/// 是否转院
		/// </summary>
		public string Turn_flag
		{
			get
			{
				return turn_flag;
			}
			set
			{
				turn_flag = value;
			}
		}

		private string turn_hosp;
		/// <summary>
		/// 转院前的医院代码
		/// </summary>
		public string Turn_hosp
		{
			get
			{
				return turn_hosp;
			}
			set
			{
				turn_hosp = value;
			}
		}

		private string regstuff_no;
		/// <summary>
		/// 操作员代码
		/// </summary>
		public string RegStuff_no
		{
			get
			{
				return regstuff_no;
			}
			set
			{
				regstuff_no = value;
			}
		}

		private string stuff_name;
		/// <summary>
		/// 操作员姓名
		/// </summary>
		public string Stuff_name
		{
			get
			{
				return stuff_name;
			}
			set
			{
				stuff_name = value;
			}
		}

		private string state_flag;
		/// <summary>
		/// 状态
		/// </summary>
		public string State_flag
		{
			get
			{
				return state_flag;
			}
			set
			{
				state_flag = value;
			}
		}

		private string rec_state;
		/// <summary>
		/// 接收数据状态
		/// </summary>
		public string Rec_state
		{
			get
			{
				return rec_state;
			}
			set
			{
				rec_state = value;
			}
		}

		private int last_ph;
		/// <summary>
		/// 最后批号
		/// </summary>
		public int Last_ph
		{
			get
			{
				return last_ph;
			}
			set
			{
				last_ph = value;
			}
		}

		private string memo1;
		/// <summary>
		/// 备注1
		/// </summary>
		public string Memo1
		{
			set{memo1 = value;}
			get{return memo1;}
		}

		private string memo2;
		/// <summary>
		/// 备注2
		/// </summary>
		public string Memo2
		{
			set{memo2 = value;}
			get{return memo2;}
		}

		private string memo3;
		/// <summary>
		/// 备注3
		/// </summary>
		public string Memo3
		{
			set{memo3 = value;}
			get{return memo3;}
		}
		
        private string outDiagnosis;
		/// <summary>
		/// 出院诊断
		/// </summary>
		public string OutDiagnosis
		{
			set{outDiagnosis=value;}
			get{return outDiagnosis;}
		}

		private string outConditions;
		/// <summary>
		/// 出院情况
		/// </summary>
		public string OutConditions
		{
			set {outConditions=value;}
			get{return outConditions;}
		}

		public class Drawwork
		{
            /// <summary>
            /// 记录号(系统生成)  农保主键
            /// </summary>
            public string ddata_sn = string.Empty;//记录号
            /// <summary>
            /// 投保镇代码
            /// </summary>
			public string dbranch_no = string.Empty;
            /// <summary>
            /// 投保村代码
            /// </summary>
            public string dcountry_no = string.Empty;      //投保村代码
            /// <summary>
            /// 就医登记号
            /// </summary>
            public string dmedi_no;   //就医登记号
            /// <summary>
            /// 本次批号
            /// </summary>
            public int dPH ;     //本次批号

            /// <summary>
            ///  0-医疗 1-死亡
            /// </summary>
            public string dmedi_type = string.Empty;  // 0-医疗 1-死亡
            /// <summary>
            /// 生成日期
            /// </summary>
            public DateTime dwork_date ; //生成日期

            /// <summary>
            /// 医院代码
            /// </summary>
            public string dhosp_code= string.Empty;  //医院代码  
            /// <summary>
            /// 医院住院号
            /// </summary>
            public string dmedi_nn = string.Empty;//医院住院号 

            /// <summary>
            /// 被保险人
            /// </summary>
            public string dunit_name = string.Empty; // 被保险人
            /// <summary>
            /// 领款人
            /// </summary>
            public string ddraw_name = string.Empty; //领款人

            /// <summary>
            /// 领款人身份证号码
            /// </summary>
            public string ddraw_id = string.Empty; //领款人身份证号码

            /// <summary>
            /// 出院总费用
            /// </summary>
            public decimal dsum_medi_sum; //出院总费用 

            /// <summary>
            /// 费用发生起日期
            /// </summary>
            public DateTime ds_stock_date; //费用发生起日期

            /// <summary>
            /// 费用发生止日期
            /// </summary>
            public DateTime de_stock_date; //费用发生止日期

            /// <summary>
            /// 起付金额
            /// </summary>
            public decimal dstart_pay_sum; //起付金额 

            /// <summary>
            /// 应付金额
            /// </summary>
            public decimal dinsur_sum; //应付金额

            /// <summary>
            /// 自付金额
            /// </summary>
            public decimal dmy_pay_sum; //自付金额

            /// <summary>
            /// 本年还可报余额
            /// </summary>
            public decimal dye_insur_sum; //本年还可报余额

            /// <summary>
            /// 业务操作员
            /// </summary>
            public string dop_stuff = string.Empty; //业务操作员

            /// <summary>
            /// 状态
            /// </summary>
            public string dstate_flag = string.Empty; //状态 

            /// <summary>
            /// 接收数据状态
            /// </summary>
            public string drec_state = string.Empty; //接收数据状态
		}

        /// <summary>
        /// 补偿结算过程类
        /// </summary>
		public class Medilist_data
		{
            /// <summary>
            /// 投保年度
            /// </summary>
			public string mappl_year = string.Empty; // 投保年度
            /// <summary>
            /// 投保镇代码
            /// </summary>
            public string mbranch_no = string.Empty; //投保镇代码
            /// <summary>
            /// 投保村代码
            /// </summary>
            public string mcountry_no= string.Empty; //投保村代码
            /// <summary>
            /// 就医登记号
            /// </summary>
            public string mmedi_no; //就医登记号
            /// <summary>
            /// 本次批号
            /// </summary>
            public int mPH ;//本次批号
            /// <summary>
            /// 序号
            /// </summary>
            public int mno;//序号
            /// <summary>
            /// 补偿比例%限额下限
            /// </summary>
            public decimal msum1; //补偿比例%限额下限
            /// <summary>
            /// 补偿比例%限额上限
            /// </summary>
            public decimal msum2; //补偿比例%限额上限


            /// <summary>
            /// 补偿比例%
            /// </summary>
            public decimal mrate; //补偿比例%

            /// <summary>
            /// 范围段内可付金额
            /// </summary>
            public decimal mm_sum1; //范围段内可付金额

            /// <summary>
            /// 实际补偿金额
            /// </summary>
            public decimal mm_sum2; //实际补偿金额

            /// <summary>
            /// 状态
            /// </summary>
            public string mstate_flage = string.Empty; //状态 
            /// <summary>
            /// 状态
            /// </summary>
            public string mstate_flag = string.Empty; //状态 
            /// <summary>
            /// 接收数据状态
            /// </summary>
            public string mrec_state = string.Empty;//接收数据状态
		}

		public class Hosp_pay
		{
            /// <summary>
            /// 医院代码
            /// </summary>
			public string hhosp_code = string.Empty; //医院代码   
            /// <summary>
            /// 医院住院号
            /// </summary>
            public string hmedi_nn = string.Empty; //医院住院号 
            /// <summary>
            /// 就医登记
            /// </summary>
            public string hmedi_no ;//就医登记
            
            /// <summary>
            /// 本次批号
            /// </summary>
            public int hPH; //本次批号
            /// <summary>
            /// 姓名
            /// </summary>
            public string hman_name = string.Empty; //姓名
            /// <summary>
            /// 收据号码
            /// </summary>
            public string hpay_no = string.Empty;//收据号码
            /// <summary>
            /// 出院总费用
            /// </summary>
            public decimal hsum_medi_sum;//出院总费用
            /// <summary>
            /// 付款时间
            /// </summary>
            public DateTime hh_pay_date; //付款时间

            /// <summary>
            /// 医院出纳姓名
            /// </summary>
            public string hh_pay_stuff = string.Empty; //医院出纳姓名

            /// <summary>
            /// 医院出纳代码
            /// </summary>
            public string hh_pay_stuff_code = string.Empty; //医院出纳代码

            /// <summary>
            /// 实付金额
            /// </summary>
            public decimal hh_pay_sum; //实付金额

            /// <summary>
            /// 状态
            /// </summary>
            public string hstate_flag = string.Empty; //状态 

            /// <summary>
            /// 接收数据状态
            /// </summary>
            public string hrec_state= string.Empty; //接收数据状态 
            /// <summary>
            /// 农保划账标志 0未划/1已划
            /// </summary>
            public string hlb_pay_flag= string.Empty; //农保划账标志 0未划/1已划
		}

		public new PYNBMainInfo Clone()
		{
			PYNBMainInfo obj = base.Clone() as PYNBMainInfo;			
			return obj;
		}
	
	}
}
