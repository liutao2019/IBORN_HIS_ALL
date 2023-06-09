using System;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Registration
{   
    /// <summary>
    /// <br>RegLvlFee</br>
    /// <br>[功能描述: 挂号费实体]</br>
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
    public class RegLvlFee:FS.FrameWork.Models.NeuObject
	{        
        /// <summary>
        /// 挂号费实体
        /// </summary>
		public RegLvlFee()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//            
        }

        #region 变量
        /// <summary>
        ///挂号级别 
        /// </summary>
        private RegLevel regLevel;

        /// <summary>
        /// 合同单位
        /// </summary>
        private FS.HISFC.Models.Base.Pact pact;
        
        /// <summary>
        /// 挂号费
        /// </summary>
        private decimal regFee = 0m;

        /// <summary>
        /// 检查费
        /// </summary>
        private decimal chkFee = 0m;
        /// <summary>
        /// 自费诊查费
        /// </summary>
        private decimal ownDigFee = 0m;

        /// <summary>
        /// 记帐诊查费
        /// </summary>
        private decimal pubDigFee = 0m;
        /// <summary>
        /// 其它费
        /// </summary>
        private decimal othFee = 0m;

        /// <summary>
        /// 操作环境
        /// </summary>
        private OperEnvironment oper;
        #endregion

        #region 属性

        /// <summary>
        /// 挂号级别
        /// </summary>
        public RegLevel RegLevel
        {
            get {
                if (this.regLevel == null)
                {
                    this.regLevel = new RegLevel();
                }
                return regLevel; }
            set { regLevel = value; }
        }

        /// <summary>
        /// 合同单位
        /// </summary>
        public Pact Pact
        {
            get {
                if (this.pact == null)
                {
                    this.pact = new Pact();
                }
                return this.pact; }
            set { this.pact = value; }
        }

        /// <summary>
        /// 挂号费
        /// </summary>
        public decimal RegFee
        {
            get { return this.regFee; }
            set { this.regFee = value; }
        }

        /// <summary>
        /// 检查费
        /// </summary>
        public decimal ChkFee
        {
            get { return this.chkFee; }
            set { this.chkFee = value; }
        }

        /// <summary>
        /// 自费诊查费
        /// </summary>
        public decimal OwnDigFee
        {
            get { return this.ownDigFee; }
            set { this.ownDigFee = value; }
        }
        /// <summary>
        /// 记帐诊查费
        /// </summary>
        public decimal PubDigFee
        {
            get { return this.pubDigFee; }
            set { this.pubDigFee = value; }
        }
        /// <summary>
        /// 其它费
        /// </summary>
        public decimal OthFee
        {
            get { return this.othFee; }
            set { this.othFee = value; }
        }
         
        /// <summary>
        /// 操作环境
        /// </summary>
        public OperEnvironment Oper
        {
            get {
                if (this.oper == null)
                {
                    this.oper = new OperEnvironment();
                }
                return oper; }
            set { oper = value; }
        }

        #endregion

        #region 方法
        public new RegLvlFee Clone ()
		{
			RegLvlFee regLvlFee = base.Clone() as RegLvlFee;
            regLvlFee.RegLevel = this.RegLevel.Clone();
            regLvlFee.Pact = this.Pact.Clone();

            return regLvlFee;
		}
        #endregion
    }
}
