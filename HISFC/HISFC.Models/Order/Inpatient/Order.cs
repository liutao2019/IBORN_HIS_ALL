using System;

namespace FS.HISFC.Models.Order.Inpatient
{
	/// <summary>
	/// FS.HISFC.Models.Order.InPatient.Order<br></br>
	/// [功能描述: 住院医嘱资料实体]<br></br>
	/// [创 建 者: 飞斯]<br></br>
	/// [创建时间: 2006-09-10]<br></br>
	/// <修改记录
	///		修改人=''
	///		修改时间='yyyy-mm-dd'
	///		修改目的=''
	///		修改描述=''
	///  />
	/// </summary>
    [Serializable]
    public class Order:FS.HISFC.Models.Order.Order
	{
		public Order()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region 变量

		#region 私有
		/// <summary>
		/// 医嘱类型（长短、是否收费、是否需要确认、药房是否配药、是否需要打印执行单）
		/// </summary>
        private Models.Order.OrderType orderType;

        /// <summary>
        /// 停止医嘱审核人
        /// {16EE9720-A7C1-4f07-8262-2F0A1567C78F}
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment dcNurse;

        /// <summary>
        /// 医嘱打印提取标志
        /// </summary>
        private string getFlag;

        /// <summary>
        /// 医嘱打印提取标志
        /// </summary>
        public string GetFlag
        {
            get
            {
                if (getFlag == null)
                {
                    getFlag = "0";
                }
                return getFlag;
            }
            set
            {
                getFlag = value;
            }
        }

        /// <summary>
        /// 特殊医嘱标示:手术医嘱等
        /// </summary>
        private string speOrderType;
        /// <summary>
        /// 特殊医嘱标示:手术医嘱等
        /// </summary>
        public string SpeOrderType
        {
            get
            {
                if (speOrderType == null)
                {
                    speOrderType = string.Empty;
                }

                return speOrderType;
            }
            set
            {
                speOrderType = value;
            }
        }

		#endregion

		#endregion

		#region 属性

		/// <summary>
		/// 医嘱类型（长短、是否收费、是否需要确认、药房是否配药、是否需要打印执行单）
		/// </summary>
        public Models.Order.OrderType OrderType
		{
			get
			{
                if (orderType == null)
                {
                    orderType = new OrderType();
                }
				return this.orderType;
			}
			set
			{
				this.orderType = value;
			}
		}

        /// <summary>
        /// 停止医嘱审核人
        /// {16EE9720-A7C1-4f07-8262-2F0A1567C78F}
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment DCNurse
        {
            get
            {
                if (dcNurse == null)
                {
                    dcNurse = new FS.HISFC.Models.Base.OperEnvironment();
                }
                return this.dcNurse;
            }
            set
            {
                this.dcNurse = value;
            }
        }

		#endregion

		#region 方法

		#region 克隆
		
		/// <summary>
		/// 克隆
		/// </summary>
		/// <returns></returns>
		public new Order Clone()
		{
			// TODO:  添加 Order.Clone 实现
			Order obj = base.Clone() as Order;
			obj.OrderType = this.OrderType.Clone();
            obj.DCNurse = this.DCNurse.Clone();

            return obj;
		}

		#endregion

		#endregion
	}
}
