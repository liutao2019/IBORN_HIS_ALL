using System;


namespace FS.HISFC.Models.SIInterface {


	/// <summary>
	/// Compare 的摘要说明。
	/// </summary>
    [Serializable]
    public class Compare : FS.FrameWork.Models.NeuObject 
	{
		public Compare()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		//医保中心信息
		private Item centerItem = new Item();
		//本地项目代码
		private string hisCode;
		//本地项目检索信息
		private FS.HISFC.Models.Base.Spell spellCode  = new FS.HISFC.Models.Base.Spell(); 
		//本地项目规格
		private string specs;
		//本地项目价格
		private Decimal price;
		//本地项目剂型
		private string doseCode;

		private string regularName;
        #region 增加适应症
        //{8FE289B0-3034-442b-A9C3-CDBF7EBDB7B2}
        /// <summary>
        /// 是否是适应症
        /// </summary>
        private bool ispracticablesymptom = false;

        /// <summary>
        /// 药品适应等级（ID为代码,NAME为名称）
        /// </summary>
        private FS.FrameWork.Models.NeuObject practicablesymptom = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 适应症描述
        /// </summary>
        private string practicablesymptomdepiction;
        //{8FE289B0-3034-442b-A9C3-CDBF7EBDB7B2} 
        #endregion 

		public string RegularName
		{
			set
			{
				regularName = value;
			}
			get
			{
				return regularName;
			}
		}

		/// <summary>
		/// 医保中心信息
		/// </summary>
		public Item CenterItem {
			get
			{
				return centerItem;
			}
			set
			{
				centerItem = value;
			}
		}
		/// <summary>
		/// 本地项目代码
		/// </summary>
		public string HisCode
		{
			get
			{
				return hisCode;
			}
			set
			{
				hisCode = value;
			}
		}

		/// <summary>
		/// 检索信息
		/// </summary>
		public FS.HISFC.Models.Base.Spell SpellCode {
			get
			{
				return spellCode;
			}
			set
			{
				spellCode = value;
			}
		}
		/// <summary>
		/// 规格
		/// </summary>
		public string Specs
		{
			get
			{
				return specs;
			}
			set
			{
				specs = value;
			}
		}
		/// <summary>
		/// 自负比例
		/// </summary>
		public Decimal Price
		{
			get
			{
				return price;
			}
			set
			{
				price = value;
			}
		}
		/// <summary>
		/// 剂型编码
		/// </summary>
		public string DoseCode
		{
			get
			{
				return doseCode;
			}
			set
			{
				doseCode = value;
			}
        }

        /// <summary>
        /// 医保标识 1：医保内；0：医保外
        /// </summary>
        private string centerFlag;

        /// <summary>
        /// 医保标识 1：医保内；0：医保外
        /// </summary>
        public string CenterFlag
        {
            set
            {
                this.centerFlag = value;
            }
            get
            {
                return centerFlag;
            }
        }
        /// <summary>
        /// 包装单位
        /// </summary>
        private string packUnit;

        public string PackUnit
        {
            set
            {
                this.packUnit = value;
            }
            get
            {
                return packUnit;
            }
        }
        #region 增加适应症
        //{8FE289B0-3034-442b-A9C3-CDBF7EBDB7B2}
        /// <summary>
        /// 是否是适应症
        /// </summary>
        public bool Ispracticablesymptom
        {
            get
            {
                //{8DF3D566-FA34-44cb-A2D5-919FE05D1702}
                //if (this.practicablesymptomdepiction == "" || this.practicablesymptomdepiction == null)
                if (this.practicablesymptom.ID == "" || this.practicablesymptom.ID == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            set
            {
                ispracticablesymptom = value;
            }
        }

        /// <summary>
        /// 药品适应等级（ID为代码,NAME为名称）
        /// </summary>
        public FS.FrameWork.Models.NeuObject Practicablesymptom
        {
            get
            {
                return practicablesymptom;
            }
            set
            {
                practicablesymptom = value;
            }
        }

        /// <summary>
        /// 适应症描述
        /// </summary>
        public string Practicablesymptomdepiction
        {
            get
            {
                return practicablesymptomdepiction;
            }
            set
            {
                practicablesymptomdepiction = value;
            }
        }
        //{8FE289B0-3034-442b-A9C3-CDBF7EBDB7B2} 
        #endregion

        #region (珠海医保增加) 

        private FS.FrameWork.Models.NeuObject feeProperty = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 费用属性(珠海医保增加) 
        /// ID为代码,NAME为名称
        /// </summary>
        public FS.FrameWork.Models.NeuObject FeeProperty
        {
            get
            {
                return this.feeProperty;
            }
            set
            {
                this.feeProperty = value;
            }
        }

        private string fdaDrugCode;

        /// <summary>
        /// 药监局药品编码(珠海医保增加) 
        /// </summary>
        public string FdaDrguCode
        {
            get
            {
                return this.fdaDrugCode;
            }
            set
            {
                this.fdaDrugCode = value;
            }
        }

        #endregion
        public new Compare Clone()
		{
			Compare obj = base.Clone() as Compare;
			obj.centerItem = this.CenterItem.Clone();
			obj.SpellCode = this.SpellCode.Clone();
            obj.practicablesymptom = this.Practicablesymptom.Clone();
            obj.feeProperty = this.FeeProperty.Clone();
			return obj;
		}
	}
}
