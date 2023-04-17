using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.Models.Ticker
{
    public class CardTicketNew
    {
        /// <summary>
        /// 简介广告{6ABA909B-8693-46d5-B636-8C30797BAE8E}
        /// </summary>
        private string advert;

        public string Advert
        {
            get { return advert; }
            set { advert = value; }
        }

        /// <summary>
        /// 券码
        /// </summary>
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// 卡券业务
        /// </summary>
        private string couponBusiness;

        public string CouponBusiness
        {
            get { return couponBusiness; }
            set { couponBusiness = value; }
        }


        /// <summary>
        /// 卡券标签
        /// </summary>
        private string couponLable;

        public string CouponLable
        {
            get { return couponLable; }
            set { couponLable = value; }
        }

        /// <summary>
        /// 卡券类型
        /// </summary>
        private string couponType;

        public string CouponType
        {
            get { return couponType; }
            set { couponType = value; }
        }


        /// <summary>
        /// 卡券有效结束时间
        /// </summary>
        private string gmtExpiry;

        public string GmtExpiry
        {
            get { return gmtExpiry; }
            set { gmtExpiry = value; }
        }


        /// <summary>
        /// 卡券有效开始时间
        /// </summary>
        private string gmtStart;

        public string GmtStart
        {
            get { return gmtStart; }
            set { gmtStart = value; }
        }


        /// <summary>
        /// 核销时间
        /// </summary>
        private string gmtWriteOff;

        public string GmtWriteOff
        {
            get { return gmtWriteOff; }
            set { gmtWriteOff = value; }
        }


        /// <summary>
        /// 可用区域
        /// </summary>
        private string limitArea;

        public string LimitArea
        {
            get { return limitArea; }
            set { limitArea = value; }
        }

        /// <summary>
        /// 客户名称
        /// </summary>
        private string patientName;

        public string PatientName
        {
            get { return patientName; }
            set { patientName = value; }
        }


        /// <summary>
        /// 客户手机号
        /// </summary>
        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }


        /// <summary>
        /// 响应结果编码
        /// </summary>
        private string resultCode;

        public string ResultCode
        {
            get { return resultCode; }
            set { resultCode = value; }
        }


        /// <summary>
        /// 响应结果描述
        /// </summary>
        private string resultDesc;

        public string ResultDesc
        {
            get { return resultDesc; }
            set { resultDesc = value; }
        }


        /// <summary>
        /// 标题
        /// </summary>
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }



        /// <summary>
        /// 使用限制
        /// </summary>
        private string useLimits;

        public string UseLimits
        {
            get { return useLimits; }
            set { useLimits = value; }
        }


        /// <summary>
        /// 核销人名称
        /// </summary>
        private string writeOffMsgName;

        public string WriteOffMsgName
        {
            get { return writeOffMsgName; }
            set { writeOffMsgName = value; }
        }



        /// <summary>
        /// 核销人手机号
        /// </summary>
        private string writeOffMsgPhone;

        public string WriteOffMsgPhone
        {
            get { return writeOffMsgPhone; }
            set { writeOffMsgPhone = value; }


        }
    }
}
