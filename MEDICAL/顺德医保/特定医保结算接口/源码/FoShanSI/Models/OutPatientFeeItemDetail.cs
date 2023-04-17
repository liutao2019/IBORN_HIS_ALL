using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoShanSI.Models
{
    /// <summary>
    /// 门诊项目明细表
    /// </summary>
    [System.Serializable]
    public class OutPatientFeeItemDetail:FS.FrameWork.Models.NeuObject
    {
        #region 字段

        /// <summary>
        /// 医院代码
        /// </summary>
        private string hospitalCode = string.Empty;


        /// <summary>
        /// 门诊标识号
        /// </summary>
        private string outPatientClinicCode = string.Empty;


        /// <summary>
        /// 医院项目代码
        /// </summary>
        private string hospitalItemCode = string.Empty;

        /// <summary>
        /// 医院辅助标识号
        /// </summary>
        private string hospitalUserCode = string.Empty;

        /// <summary>
        /// 项目说明
        /// </summary>
        private string itemDesc = string.Empty;

        /// <summary>
        /// 单位
        /// </summary>
        private string itemUnit = string.Empty;

        /// <summary>
        /// 数量
        /// </summary>
        private int itemQty = 0;

        /// <summary>
        /// 单价
        /// </summary>
        private decimal itemPrice = 0m;

        /// <summary>
        /// 合计金额
        /// </summary>
        private decimal itemTotCost = 0m;


        /// <summary>
        /// 处理日期
        /// </summary>
        private DateTime dealDate = System.DateTime.MinValue;

        /// <summary>
        /// 处理状态
        /// </summary>
        private string dealState = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        private string memo = string.Empty;

        /// <summary>
        /// USYS系统字段
        /// </summary>
        private string sysUse = string.Empty;


        #endregion

        #region 属性

        /// <summary>
        /// 医院代码
        /// </summary>
        public string HospitalCode
        {
            get { return hospitalCode; }
            set { hospitalCode = value; }
        }

        /// <summary>
        /// 门诊标识号
        /// </summary>
        public string OutPatientClinicCode
        {
            get { return outPatientClinicCode; }
            set { outPatientClinicCode = value; }
        }

        /// <summary>
        /// 医院项目代码
        /// </summary>
        public string HospitalItemCode
        {
            get { return hospitalItemCode; }
            set { hospitalItemCode = value; }
        }

        /// <summary>
        /// 医院辅助标识号
        /// </summary>
        public string HospitalUserCode
        {
            get { return hospitalUserCode; }
            set { hospitalUserCode = value; }
        }

        /// <summary>
        /// 项目说明
        /// </summary>
        public string ItemDesc
        {
            get { return itemDesc; }
            set { itemDesc = value; }
        }

        /// <summary>
        /// 单位
        /// </summary>
        public string ItemUnit
        {
            get { return itemUnit; }
            set { itemUnit = value; }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public int ItemQty
        {
            get { return itemQty; }
            set { itemQty = value; }
        }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal ItemPrice
        {
            get { return itemPrice; }
            set { itemPrice = value; }
        }

        /// <summary>
        /// 合计金额
        /// </summary>
        public decimal ItemTotCost
        {
            get { return itemTotCost; }
            set { itemTotCost = value; }
        }

        /// <summary>
        /// 处理日期
        /// </summary>
        public DateTime DealDate
        {
            get { return dealDate; }
            set { dealDate = value; }
        }

        /// <summary>
        /// 处理状态
        /// </summary>
        public string DealState
        {
            get { return dealState; }
            set { dealState = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }

        /// <summary>
        /// USYS系统字段
        /// </summary>
        public string SysUse
        {
            get { return sysUse; }
            set { sysUse = value; }
        }
        #endregion

        #region 方法



        #endregion
    
    }
}
