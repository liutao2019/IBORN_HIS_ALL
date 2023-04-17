using System;

namespace FS.HISFC.Models.Fee.Outpatient
{
	/// <summary>
	/// BalancePay<br></br>
	/// [功能描述: 门诊支付方式类]<br></br>
	/// [创 建 者: 飞斯]<br></br>
	/// [创建时间: 2006-09-13]<br></br>
	/// <修改记录 
	///		修改人='' 
	///		修改时间='yyyy-mm-dd' 
	///		修改目的=''
	///		修改描述=''
	///  />
	/// </summary>
    /// 
    [System.Serializable]
	public class BalancePay : BalancePayBase
	{
		#region 变量

        /// <summary>
        /// {3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
        /// </summary>
        private string hospital_id;

        /// <summary>
        /// 院区名{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
        /// </summary>
        private string hospital_name;
        

		/// <summary>
		/// 交易流水号
		/// </summary>
		private string squence;

		/// <summary>
		/// 发票序号，一次结算产生多张发票的combNO
		/// </summary>
		private string invoiceCombNO;

        /// <summary>
        /// 是否代付
        /// </summary>
        private bool isEmpPay = false;

        /// <summary>
        /// 代付人账号
        /// </summary>
        private string accountNo = string.Empty;

        /// <summary>
        /// 代付账户类型编码
        /// </summary>
        private string accountTypeCode = string.Empty;

        /// <summary>
        /// 代付人信息
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();

		#endregion

		#region 属性

        /// <summary>
        /// 代付人信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo EmpowerPatient
        {

            get
            {
                return this.patient;
            }
            set
            {
                this.patient = value;
            }
        }
        /// <summary>
        ///是否代付
        /// </summary>
        public bool IsEmpPay
        {
            get
            {
                return this.isEmpPay;
            }
            set
            {
                this.isEmpPay = value;
            }
        }

        /// <summary>
        ///代付人账号
        /// </summary>
        public string AccountNo
        {
            get
            {
                return this.accountNo;
            }
            set
            {
                this.accountNo = value;
            }
        }

        /// <summary>
        ///代付账户类型编码
        /// </summary>
        public string AccountTypeCode
        {
            get
            {
                return this.accountTypeCode;
            }
            set
            {
                this.accountTypeCode = value;
            }
        }
		/// <summary>
		/// 交易流水号
		/// </summary>
		public string Squence
		{
			get
			{
				return this.squence;
			}
			set
			{
				this.squence = value;
			}
		}

		/// <summary>
		/// 发票序号，一次结算产生多张发票的combNO
		/// </summary>
		public string InvoiceCombNO
		{
			get
			{
				return this.invoiceCombNO;
			}
			set
			{
				this.invoiceCombNO = value;
			}
		}
        /// <summary>
        ///院区id// {31E733E1-8329-4e47-A024-47BCD4C6976D} {3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
        /// </summary>
        public string Hospital_id
        {
            get
            {
                return this.hospital_id;
            }
            set
            {
                this.hospital_id = value;
            }
        }

        // {31E733E1-8329-4e47-A024-47BCD4C6976D}{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
        /// <summary>
        /// 院区名
        /// </summary>
        public string Hospital_name
        {
            get
            {
                return this.hospital_name;
            }
            set
            {
                this.hospital_name = value;
            }
        }
		#endregion

		#region 方法
		
		#region 克隆
		
		/// <summary>
		/// 克隆
		/// </summary>
		/// <returns>返回当前对象实例</returns>
		public new BalancePay Clone()
		{
			return base.Clone() as BalancePay;
		} 

		#endregion

		#endregion
	}
}
