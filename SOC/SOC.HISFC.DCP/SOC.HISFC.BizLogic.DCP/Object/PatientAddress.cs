using System;

namespace FS.HISFC.DCP.Object
{
	/// <summary>
	/// PatientAddress 的摘要说明。
	/// </summary>
	public class PatientAddress :FS.HISFC.Models.Base.Const
	{
		
		public PatientAddress()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
        private string addr_Code;                                      //地址编码
        private string addr_Name;                                        //地址名称
		private string  senior_Address;    //上级地址编码
		private string  lever; //级别

		/// <summary>
		/// 地址编码
		/// </summary>
        public string Addr_Code
		{
			get
			{
                return this.addr_Code;
			}
			set
			{
                this.addr_Code = value;
			}
		}

		/// <summary>
		/// 地址名称
		/// </summary>
        public string Addr_Name
		{
			get
			{
				return this.addr_Name;
			}
			set
			{
                this.addr_Name = value;
			}
		}


		/// <summary>
		/// 上级地址编码
		/// </summary>
		public string Senior_Address
		{
			get
			{
				return this.senior_Address;
			}
			set
			{
                this.senior_Address = value;
			}
		}
		/// <summary>
		/// 级别
		/// </summary>
		public string Lever
		{
			get
			{
				return this.lever ;

			}
			set
			{
				this.lever =value;
			}
		}
	}
}