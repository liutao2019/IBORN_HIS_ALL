using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.Models.Ticker
{
    public class CardTicker
    {
        private string id;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Code { set; get; }

        private string title;

        /// <summary>
        /// 卡卷标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string type;
        /// <summary>
        /// 卡卷类型
        /// </summary>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// 分享形式,0不可分享,1可裂变，2邀请
        /// </summary>
        public string ShareType { set; get; }

        private int preissuQuantity;
        /// <summary>
        /// 卡卷总数量
        /// </summary>
        public int PreissuQuantity
        {
            get { return preissuQuantity; }
            set { preissuQuantity = value; }
        }


        private string content;
        /// <summary>
        /// 卡卷使用详情
        /// </summary>
        public string Content
        {
            get { return content; }
            set { content = value; }
        }


        private Decimal rewardWeight;
        /// <summary>
        /// 奖励权重
        /// </summary>
        public Decimal RewardWeight
        {
            get { return rewardWeight; }
            set { rewardWeight = value; }
        }

        private DateTime startTime;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        private int limitReceiveNum;
        /// <summary>
        /// 限制每个用户领取数
        /// </summary>
        public int LimitReceiveNum
        {
            get { return limitReceiveNum; }
            set { limitReceiveNum = value; }
        }

        private string hospitalArea;
        /// <summary>
        /// 院区
        /// </summary>
        public string HospitalArea
        {
            get { return hospitalArea; }
            set { hospitalArea = value; }
        }

        private DateTime createTime;
        /// <summary>
        /// 卡卷创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        private string coverImgUrl;
        /// <summary>
        /// 卡卷封面图片
        /// </summary>
        public string CoverImgUrl
        {
            get { return coverImgUrl; }
            set { coverImgUrl = value; }
        }


        private string channelName;//展示用
        /// <summary>
        /// 卡卷归属渠道，多个情况下以英文逗号隔开
        /// </summary>
        public string ChannelName
        {
            get { return channelName; }
            set { channelName = value; }
        }

        private DateTime endTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        private string receiveCode;
        /// <summary>
        /// 接受人会员系统编码
        /// </summary>
        public string ReceiveCode
        {
            get { return receiveCode; }
            set { receiveCode = value; }
        }

        private string receiveId;
        /// <summary>
        /// 接收人mmwyID
        /// </summary>
        public string ReceiveId
        {
            get { return receiveId; }
            set { receiveId = value; }
        }

        /// <summary>
        /// 领取人姓名
        /// </summary>
        public string ReceivePersonName { set; get; }

        /// <summary>
        /// 领取人号码
        /// </summary>
        public string ReceiveMobile { set; get; }

        /// <summary>
        /// 领取时间
        /// </summary>
        public DateTime ReceiveTime { set; get; }

        /// <summary>
        /// 使用状态，0为未使用，1为已使用，2 为不可使用
        /// </summary>
        public int CouponState { set; get; }

        private decimal conditionMoney;
        /// <summary>
        /// 消费满多少元才可以使用
        /// </summary>
        public decimal ConditionMoney
        {
            get { return conditionMoney; }
            set { conditionMoney = value; }
        }


        private decimal cardMoney;
        /// <summary>
        /// 卡卷金额或者是礼品金额
        /// </summary>
        public decimal CardMoney
        {
            get { return cardMoney; }
            set { cardMoney = value; }
        }     

      
        private string regisFee;
        /// <summary>
        /// 是否包含挂号费0,不包含,1包含
        /// </summary>
        public string RegisFee
        {
            get { return regisFee; }
            set { regisFee = value; }
        }

        private string discount;
        /// <summary>
        /// 几折
        /// </summary>
        public string  Discount
        {
            get { return discount; }
            set { discount = value; }
        }

        private string professionName;
        /// <summary>
        /// 职称
        /// </summary>
        public string ProfessionName
        {
            get { return professionName; }
            set { professionName = value; }
        }

        private string giftName;
        /// <summary>
        /// 礼品名称
        /// </summary>
        public string GiftName
        {
            get { return giftName; }
            set { giftName = value; }
        }

        private string limitMealName;
        /// <summary>
        /// 限制套餐名称，多个情况下以英文逗号隔开
        /// </summary>
        public string LimitMealName
        {
            get { return limitMealName; }
            set { limitMealName = value; }
        }

        private string limitProjectName;
        /// <summary>
        /// 限制项目名称，多个情况下以英文逗号隔开
        /// </summary>
        public string LimitProjectName
        {
            get { return limitProjectName; }
            set { limitProjectName = value; }
        }

        private string limitDepartName;
        /// <summary>
        /// 限制科室名称，多个情况下以英文逗号隔开
        /// </summary>
        public string LimitDepartName
        {
            get { return limitDepartName; }
            set { limitDepartName = value; }
        }

        private string shortMessage;
        /// <summary>
        /// 短信内容
        /// </summary>
        public string ShortMessage
        {
            get { return shortMessage; }
            set { shortMessage = value; }
        }


    }

    public class ReturnData<T> where T : new()
    {
        public string Status { set; get; }

        public string Message { set; get; }

        public T Data { set; get; }
    }
}
