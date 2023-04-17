using FS.FrameWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.Models.Fee
{
    /// <summary>
    /// Invoice<br></br>
    /// [功能描述: 患者优惠卡]<br></br>
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
    public class PatientDiscountCard : NeuObject
    {
        /// <summary>
        /// 卡号
        /// </summary>
        private string cardNo;

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
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
        /// 领取客户
        /// </summary>
        private string getName;

        /// <summary>
        /// 领取客户
        /// </summary>
        public string GetName
        {
            get { return getName; }
            set { getName = value; }
        }

        /// <summary>
        /// 领取客户卡号
        /// </summary>
        private string getCardNo;

        /// <summary>
        /// 领取客户卡号
        /// </summary>
        public string GetCardNo
        {
            get { return getCardNo; }
            set { getCardNo = value; }
        }

        /// <summary>
        /// 领取时间
        /// </summary>
        private DateTime getTime;

        /// <summary>
        /// 领取时间
        /// </summary>
        public DateTime GetTime
        {
            get { return getTime; }
            set { getTime = value; }
        }


        /// <summary>
        /// 领取客户联系号码
        /// </summary>
        private string getPhone;

        /// <summary>
        /// 领取客户联系号码
        /// </summary>
        public string GetPhone
        {
            get { return getPhone; }
            set { getPhone = value; }
        }

        /// <summary>
        /// 领取操作员
        /// </summary>
        private string getOper;

        /// <summary>
        /// 领取操作员
        /// </summary>
        public string GetOper
        {
            get { return getOper; }
            set { getOper = value; }
        }

        /// <summary>
        /// 使用客户
        /// </summary>
        private string usedName;

        /// <summary>
        /// 使用客户
        /// </summary>
        public string UsedName
        {
            get { return usedName; }
            set { usedName = value; }
        }

        /// <summary>
        /// 使用客户卡号
        /// </summary>
        private string usedCardNo;

        /// <summary>
        /// 使用客户卡号
        /// </summary>
        public string UsedCardNo
        {
            get { return usedCardNo; }
            set { usedCardNo = value; }
        }

        /// <summary>
        /// 使用客户联系号码
        /// </summary>
        private string usedPhone;

        /// <summary>
        /// 使用客户联系号码
        /// </summary>
        public string UsedPhone
        {
            get { return usedPhone; }
            set { usedPhone = value; }
        }

        /// <summary>
        /// 使用时间
        /// </summary>
        private DateTime usedTime;

        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime UsedTime
        {
            get { return usedTime; }
            set { usedTime = value; }
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
        /// 使用操作员
        /// </summary>
        private string usedOper;

        /// <summary>
        /// 使用操作员
        /// </summary>
        public string UsedOper
        {
            get { return usedOper; }
            set { usedOper = value; }
        }
    }
}
