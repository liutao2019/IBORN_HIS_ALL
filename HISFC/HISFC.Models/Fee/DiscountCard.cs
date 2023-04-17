using FS.FrameWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.Models.Fee
{
    /// <summary>
    /// Invoice<br></br>
    /// [功能描述: 优惠卡]<br></br>
    /// [创 建 者: ]<br></br>
    /// [创建时间: 2023-03-30]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    /// 
    [System.Serializable]
    public class DiscountCard : NeuObject
    {
        /// <summary>
        /// 领取人
        /// </summary>
        private string getPersonCode;

        /// <summary>
        /// 领取人
        /// </summary>
        public string GetPersonCode
        {
            get { return getPersonCode; }
            set { getPersonCode = value; }
        }

        /// <summary>
        /// 领取时间
        /// </summary>
        private DateTime getDate;

        /// <summary>
        /// 领取时间
        /// </summary>
        public DateTime GetDate
        {
            get { return getDate; }
            set { getDate = value; }
        }

        /// <summary>
        /// 卡类型
        /// </summary>
        private string cardKind;

        /// <summary>
        /// 卡类型
        /// </summary>
        public string CardKind
        {
            get { return cardKind; }
            set { cardKind = value; }
        }


        /// <summary>
        /// 卡名称
        /// </summary>
        private string cardName;

        /// <summary>
        /// 卡名称
        /// </summary>
        public string CardName
        {
            get { return cardName; }
            set { cardName = value; }
        }

        /// <summary>
        /// 开始卡号
        /// </summary>
        private string startNo;

        /// <summary>
        /// 开始卡号
        /// </summary>
        public string StartNo
        {
            get { return startNo; }
            set { startNo = value; }
        }
        /// <summary>
        /// 结束卡号
        /// </summary>
        private string endNo;

        /// <summary>
        /// 结束卡号
        /// </summary>
        public string EndNo
        {
            get { return endNo; }
            set { endNo = value; }
        }

        /// <summary>
        /// 卡数量
        /// </summary>
        private string qty;

        /// <summary>
        /// 卡数量
        /// </summary>
        public string QTY
        {
            get { return qty; }
            set { qty = value; }
        }

        /// <summary>
        /// 已用卡号
        /// </summary>
        private string usedNo;

        /// <summary>
        /// 已用卡号
        /// </summary>
        public string UsedNo
        {
            get { return usedNo; }
            set { usedNo = value; }
        }
        /// <summary>
        /// 使用状态
        /// </summary>
        private string usedState;

        /// <summary>
        /// 使用状态
        /// </summary>
        public string UsedState
        {
            get { return usedState; }
            set { usedState = value; }
        }

        /// <summary>
        /// 公用标识
        /// </summary>
        private string isPub;

        /// <summary>
        /// 公用标识
        /// </summary>
        public string IsPub
        {
            get { return isPub; }
            set { isPub = value; }
        }

        /// <summary>
        /// 操作员
        /// </summary>
        private string operCode;

        /// <summary>
        /// 操作员
        /// </summary>
        public string OperCode
        {
            get { return operCode; }
            set { operCode = value; }
        }

        /// <summary>
        /// 操作时间
        /// </summary>
        private DateTime operDate;

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperDate
        {
            get { return operDate; }
            set { operDate = value; }
        }
    }
}
