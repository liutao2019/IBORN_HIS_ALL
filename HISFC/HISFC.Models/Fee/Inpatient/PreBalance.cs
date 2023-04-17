using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Fee.Inpatient
{
    /// <summary>
    /// Balance<br></br>
    /// [功能描述: 住院预结算类]<br></br>
    /// [创 建 者: lzd]<br></br>
    /// [创建时间: 2021-02-07]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    /// 
    [System.Serializable]
    public class PreBalance : NeuObject  //{C4231074-D350-4df9-AF7C-C37124B44B80}
    {
        #region 变量
        private string preblanceno = string.Empty;
        private string inpatientno;
        private string name2;
        decimal balanceprice;
        DateTime balancedate;
        private string opercode;
        private string opername;
        private string packageids;
        private string memo;
        private string isvalid;
        DateTime canceldate;
        private string cancelcode;
        private string cancelman;

        #endregion
        
        #region 属性
        /// <summary>
        /// 流水号
        /// </summary>
        public string PREBLANCENO
        {
            get
            {
                return this.preblanceno;
            }
            set
            {
                this.preblanceno = value;
            }
        }
        /// <summary>
        /// 住院号
        /// </summary>

        public string INPATIENTNO
        {
            get
            {
                return this.inpatientno;
            }
            set
            {
                this.inpatientno = value;
            }
        }
        /// <summary>
        /// 客户姓名
        /// </summary>
         public string NAME
         {
           get
            {
                return this.name2;
            }
            set
            {
                this.name2 = value;
            }
         }
        /// <summary>
        /// 预结算金额
        /// </summary>
         public decimal BALANCEPRICE
         {
           get
            {
                return this.balanceprice;
            }
            set
            {
                this.balanceprice = value;
            }
         }

        /// <summary>
        /// 操作人代码
        /// </summary>
        public string OPERCODE
         {
           get
            {
                return this.opercode;
            }
            set
            {
                this.opercode = value;
            }
         }
        /// <summary>
        /// 操作人
        /// </summary>
          public string OPERNAME
         {
           get
            {
                return this.opername;
            }
            set
            {
                this.opername = value;
            }
         }


          /// <summary>
          /// 预结套餐id
          /// </summary>
          public string PACKAGEIDS
         {
           get
            {
                return this.packageids;
            }
            set
            {
                this.packageids = value;
            }
         }


          /// <summary>
          /// 备注
          /// </summary>
           public string MEMO
         {
           get
            {
                return this.memo;
            }
            set
            {
                this.memo = value;
            }
         }


           /// <summary>
           /// 状态
           /// </summary>
          public string ISVALID
         {
           get
            {
                return this.isvalid;
            }
            set
            {
                this.isvalid = value;
            }
         }


          /// <summary>
          /// 取消人码
          /// </summary>
         public string CANCELCODE
         {
           get
            {
                return this.cancelcode;
            }
            set
            {
                this.cancelcode = value;
            }
         }


         /// <summary>
         /// 取消人
         /// </summary>
         public string CANCELMAN
         {
           get
            {
                return this.cancelman;
            }
            set
            {
                this.cancelman = value;
            }
         }
                      



        /// <summary>
        /// 预结时间
        /// </summary>
        public DateTime BALANCEDATE
        {
            get
            {
                return this.balancedate;
            }
            set
            {
                this.balancedate = value;
            }
        }

           /// <summary>
        /// 取消时间
        /// </summary>
        public DateTime CANCELDATE
        {
            get
            {
                return this.canceldate;
            }
            set
            {
                this.canceldate = value;


            }
        }
        #endregion

        #region 方法

        #region 克隆

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns>当前对象的实例副本</returns>
        public new PreBalance Clone()
        {
            PreBalance balanceBase = base.Clone() as PreBalance;
            balanceBase.PREBLANCENO = this.PREBLANCENO;
            balanceBase.INPATIENTNO = this.INPATIENTNO;
            balanceBase.NAME = this.NAME;
            balanceBase.BALANCEPRICE = this.BALANCEPRICE;
            balanceBase.BALANCEDATE = this.BALANCEDATE;
            balanceBase.OPERCODE = this.OPERCODE;
            balanceBase.OPERNAME = this.OPERNAME;
            balanceBase.PACKAGEIDS = this.PACKAGEIDS;
            balanceBase.MEMO = this.MEMO;
            balanceBase.ISVALID = this.ISVALID;
            balanceBase.CANCELDATE = this.CANCELDATE;
            balanceBase.CANCELCODE = this.CANCELCODE;
            balanceBase.CANCELMAN = this.CANCELMAN;
            return balanceBase;
        }
        #endregion

        #endregion
    }
}
