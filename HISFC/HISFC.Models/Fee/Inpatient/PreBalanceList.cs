using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.FrameWork.Models;
namespace FS.HISFC.Models.Fee.Inpatient
{
    /// <summary>
    /// Balance<br></br>
    /// [功能描述: 住院预结算明细类]<br></br>
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
    public class PreBalanceList : NeuObject  //{C4231074-D350-4df9-AF7C-C37124B44B80}
    {
        #region 变量

        private string preblanceno;
        private string sequence_no;
        private string item_code;
        private string fee_code;
        private string item_name;
        private decimal unit_price;
        private decimal qty;
        private string current_unit;
        private string package_code;
        private string package_name;
        private string spec;
        private string priceunit;
        private string order_id;

        #endregion

        #region 属性
        /// <summary>
        /// 预结算号
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
        /// 流水号
        /// </summary>
        public string SEQUENCE_NO
        {
            get
            {
                return this.sequence_no;
            }
            set
            {
                this.sequence_no = value;
            }
        }

        /// <summary>
        /// 项目代码
        /// </summary>
        public string ITEM_CODE
        {
            get
            {
                return this.item_code;
            }
            set
            {
                this.item_code = value;
            }
        }


        /// <summary>
        /// 最小费用代码
        /// </summary>
        public string FEE_CODE
        {
            get
            {
                return this.fee_code;
            }
            set
            {
                this.fee_code = value;
            }
        }


        /// <summary>
        /// 项目名称
        /// </summary>
        public string ITEM_NAME
        {
            get
            {
                return this.item_name;
            }
            set
            {
                this.item_name = value;
            }
        }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal UNIT_PRICE
        {
            get
            {
                return this.unit_price;
            }
            set
            {
                this.unit_price = value;
            }
        }


        /// <summary>
        /// 预结数量
        /// </summary>
        public decimal QTY
        {
            get
            {
                return this.qty;
            }
            set
            {
                this.qty = value;
            }
        }



        /// <summary>
        /// 当前单位
        /// </summary>
        public string CURRENT_UNIT
        {
            get
            {
                return this.current_unit;
            }
            set
            {
                this.current_unit = value;
            }
        }

        /// <summary>
        /// 計價单位
        /// </summary>
        public string PRICEUNIT
        {
            get
            {
                return this.priceunit;
            }
            set
            {
                this.priceunit = value;
            }
        }



        /// <summary>
        /// 组套代码
        /// </summary>
        public string PACKAGE_CODE
        {
            get
            {
                return this.package_code;
            }
            set
            {
                this.package_code = value;
            }
        }

        /// <summary>
        /// 组套名
        /// </summary>
        public string PACKAGE_NAME
        {
            get
            {
                return this.package_name;
            }
            set
            {
                this.package_name = value;
            }
        }

        public string Spec
        {
            get
            {
                return this.spec;
            }
            set
            {
                this.spec = value;
            }
        }

        public string Order_ID
        {
            get
            {
                return this.order_id;
            }
            set
            {
                this.order_id = value;
            }
        }

        #endregion

        #region 方法
        #region 克隆

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns>当前对象的实例副本</returns>
        public new PreBalanceList Clone()
        {
            PreBalanceList balanceBase = base.Clone() as PreBalanceList;
            balanceBase.PREBLANCENO = this.PREBLANCENO;
            balanceBase.SEQUENCE_NO = this.SEQUENCE_NO;
            balanceBase.ITEM_CODE = this.ITEM_CODE;
            balanceBase.FEE_CODE = this.FEE_CODE;
            balanceBase.ITEM_NAME = this.ITEM_NAME;
            balanceBase.UNIT_PRICE = this.UNIT_PRICE;
            balanceBase.QTY = this.QTY;
            balanceBase.CURRENT_UNIT = this.CURRENT_UNIT;
            balanceBase.PACKAGE_CODE = this.PACKAGE_CODE;
            balanceBase.PACKAGE_NAME = this.PACKAGE_NAME;
            balanceBase.Order_ID = this.Order_ID;
            return balanceBase;
        }
        #endregion
        #endregion
    }
}
