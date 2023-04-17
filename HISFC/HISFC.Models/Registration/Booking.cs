using System;

namespace FS.HISFC.Models.Registration
{
    /// <summary>
    /// <br>RegLevel</br>
    /// <br>[功能描述: 预约挂号实体]</br>
    /// <br>[创 建 者: 黄小卫]</br>
    /// <br>[创建时间: 2007-2-1]</br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    [Serializable]
    public class Booking:FS.HISFC.Models.RADT.Patient
	{
		/// <summary>
		/// 
		/// </summary>
		public Booking()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//          
        }

        #region 变量

        #region 看诊信息
        /// <summary>
        /// 看诊信息
        /// </summary>
        private Schema doctor;
		#endregion 		
		
        /// <summary>
        /// 操作环境
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper;

        /// <summary>
        /// 是否已看诊
        /// </summary>
        private bool isSee;

        /// <summary>
        /// 每日流水号
        /// </summary>
        private int orderNO = 0;

        /// <summary>
        /// 确认人
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment confirmOper;

        /// <summary>
        /// 预约类型ID
        /// </summary>
        private string bookingTypeId = string.Empty;

        /// <summary>
        /// 预约类型名称
        /// </summary>
        private string bookingTypeName = string.Empty;

        /// <summary>
        /// 挂号流水号
        /// </summary>
        private string regID = string.Empty;

        /// <summary>
        /// 挂号流水号
        /// </summary>
        public string RegID
        {
            get
            {
                return regID;
            }
            set
            {
                regID = value;
            }
        }

        #endregion

        #region 属性
        /// <summary>
        /// 预约类型ID
        /// </summary>
        public string BookingTypeId
        {
            get
            {
                return bookingTypeId;
            }
            set
            {
                bookingTypeId = value;
            }
        }

        /// <summary>
        /// 预约类型名称
        /// </summary>
        public string BookingTypeName
        {
            get
            {
                return bookingTypeName;
            }
            set
            {
                bookingTypeName = value;
            }
        }

        /// <summary>
        /// 看诊信息
        /// </summary>
        public Schema DoctorInfo
        {
            get {
                if (this.doctor == null)
                {
                    this.doctor = new Schema();
                }

                return this.doctor; }
            set { this.doctor = value; }
        }

        /// <summary>
        /// 每日流水号
        /// </summary>
        public int OrderNO
        {
            get { return orderNO; }
            set { orderNO = value; }
        }

        /// <summary>
        /// 是否已看诊
        /// </summary>
        public bool IsSee
        {
            get { return this.isSee; }
            set { this.isSee = value; }
        }

        /// <summary>
        /// 操作环境
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get {
                if (this.oper == null)
                {
                    this.oper = new FS.HISFC.Models.Base.OperEnvironment();
                }
                return this.oper; }
            set { this.oper = value; }
        }

        /// <summary>
        /// 确认人
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment ConfirmOper
        {
            get {
                if (this.confirmOper == null)
                {
                    this.confirmOper = new FS.HISFC.Models.Base.OperEnvironment();
                }
                return this.confirmOper; }
            set { this.confirmOper = value; }
        }

        /// <summary>
        /// 挂号费用信息
        /// </summary>
        private RegLvlFee regLvlFee;

        /// <summary>
        /// 挂号费用信息
        /// </summary>
        public RegLvlFee RegLvlFee
        {
            get
            {
                if (regLvlFee == null)
                {
                    regLvlFee = new RegLvlFee();
                }
                return regLvlFee;
            }
            set { regLvlFee = value; }
        }

        /// <summary>
        /// 身份标识卡号
        /// </summary>
        private string markNO = string.Empty;
        /// <summary>
        /// 卡类型
        /// </summary>
        private FS.FrameWork.Models.NeuObject markType;

        
        /// <summary>
        /// 身份标识卡号
        /// </summary>
        public string MarkNO
        {
            get
            {
                return this.markNO;
            }
            set
            {
                this.markNO = value;
            }
        }
        /// <summary>
        /// 身份标识卡类别 1磁卡 2IC卡 3保障卡
        /// </summary>
        public FS.FrameWork.Models.NeuObject MarkType
        {
            get
            {
                if (markType == null)
                {
                    markType = new FS.FrameWork.Models.NeuObject();
                }
                return this.markType;
            }
            set
            {
                this.markType = value;
            }
        }

        /// <summary>
        /// 交易流水号（外部用）
        /// </summary>
        private string businessNo;

        /// <summary>
        /// 交易流水号（外部用）
        /// </summary>
        public string BusinessNo
        {
            get { return businessNo; }
            set { businessNo = value; }
        }

        /// <summary>
        /// 交易信息
        /// </summary>
        private string businessInfo;

        /// <summary>
        /// 交易信息
        /// </summary>
        public string BusinessInfo
        {
            get
            {
                return businessInfo;
            }
            set { businessInfo = value; }
        }

        /// <summary>
        /// 有效性
        /// </summary>
        private bool isValid;

        /// <summary>
        /// 有效性
        /// </summary>
        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }

        #endregion

        #region 方法
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new Booking Clone()
		{
            Booking book = base.Clone() as Booking;

            book.DoctorInfo = this.DoctorInfo.Clone();
            book.Oper = this.Oper.Clone();
            book.MarkType = this.MarkType.Clone();
            book.ConfirmOper = this.ConfirmOper.Clone();
            book.RegLvlFee = this.RegLvlFee.Clone();


            return book;
        }
        #endregion

        #region 废弃
        /// <summary>
        /// 病历号
        /// </summary>
        [Obsolete("更改为：PID.CardNO", true)]
        public string CardNo = "";

        /// <summary>
        /// 身份证
        /// </summary>
        [Obsolete("更改为:IDCard", true)]
        public string IdenNo = "";

        /// <summary>
        /// 性别
        /// </summary>
        [Obsolete("更改为：Sex.ID", true)]
        public string SexID = "";

        /// <summary>
        /// 联系电话
        /// </summary>
        [Obsolete("更改为：PhoneHome", true)]
        public string Phone = "";

        /// <summary>
        /// 地址
        /// </summary>
        [Obsolete("更改为：AddressHome", true)]
        public string Address = "";
        #endregion
	}
}
