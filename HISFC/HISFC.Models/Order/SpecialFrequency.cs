using System;

namespace FS.HISFC.Models.Order
{
	/// <summary>
	/// FS.HISFC.Models.Order.SpecialFrequency<br></br>
	/// [功能描述: 特殊频次实体]<br></br>
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
    public class SpecialFrequency : FS.FrameWork.Models.NeuObject
    {
        public SpecialFrequency()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 变量

        /// <summary>
        ///  //医嘱流水号
        /// </summary>
        private string orderID;

        /// <summary>
        /// //医嘱组合号
        /// </summary>
        private FS.HISFC.Models.Order.Combo combo;

        /// <summary>
        ///  //频次点
        /// </summary>
        private string point;

        /// <summary>
        /// //  频次点用量
        /// </summary>
        private string dose;

        /// <summary>
        /// 操作员
        /// </summary>
        private Base.OperEnvironment oper;//操作环境

        #endregion

        #region 属性
        /// <summary>
        /// 医嘱号
        /// </summary>
        public string OrderID
        {
            get
            {
                if (orderID == null)
                {
                    orderID = string.Empty;
                }
                return this.orderID;
            }
            set
            {
                this.orderID = value;
            }
        }

        /// <summary>
        /// 组合
        /// </summary>
        public Combo Combo
        {
            set
            {
                if (combo == null)
                {
                    combo = new Combo();
                }
                this.combo = value;
            }
            get
            {
                return this.combo;
            }
        }

        /// <summary>
        /// 时间点
        /// </summary>
        public string Point
        {
            get
            {
                if (point == null)
                {
                    point = string.Empty;
                }
                return this.point;
            }
            set
            {
                this.point = value;
            }
        }
        /// <summary>
        /// 剂量
        /// </summary>
        public string Dose
        {
            get
            {
                if (dose == null)
                {
                    dose = string.Empty;
                }
                return this.dose;
            }
            set
            {
                this.dose = value;
            }
        }
        /// <summary>
        /// 操作者
        /// </summary>
        public Base.OperEnvironment Oper
        {
            get
            {
                if (oper == null)
                {
                    oper = new FS.HISFC.Models.Base.OperEnvironment();
                }
                return this.oper;
            }
            set
            {
                this.oper = value;
            }
        }
        #endregion

        #region 作废的

        [Obsolete("用OrderID代替", true)]
        public string moOrder; //医嘱流水号
        [Obsolete("用Combo.ID代替", true)]
        public string combNo;  //医嘱组合号
        [Obsolete("用ID代替", true)]
        public string drqFreqtype; //频次类型
        [Obsolete("用Name代替", true)]
        public string drefreqName; //频次名称
        [Obsolete("用Point代替", true)]
        public string drqPoint; //频次点
        [Obsolete("用Dose代替", true)]
        public string dosePoint; //  频次点用量
        [Obsolete("用Oper.ID代替", true)]
        public string OperID; // 操作员
        [Obsolete("用Oper.OperTime代替", true)]
        public System.DateTime operDate; //操作时间

        #endregion

        #region 方法

        #region 克隆

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new SpecialFrequency Clone()
        {
            SpecialFrequency obj = base.Clone() as SpecialFrequency;
            obj.combo = this.Combo.Clone();
            obj.oper = this.Oper.Clone();
            return obj;
        }

        #endregion

        #endregion

    }
}
