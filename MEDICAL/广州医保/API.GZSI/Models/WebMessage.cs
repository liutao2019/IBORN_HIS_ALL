using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace API.GZSI.Models
{
    /// <summary>
    /// 公共请求json
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WebMessage<T>
    {
        //交易编号
        private string infno = string.Empty;
        /// <summary>
        /// 交易编号
        /// </summary>
        [JsonProperty("infno")]
        public string Infno
        {
            get { return infno; }
            set { infno = value; }
        }
        //发送方报文ID
        private string msgid = string.Empty;
        /// <summary>
        /// 发送方报文ID
        /// </summary>
        [JsonProperty("msgid")]
        public string Msgid
        {
            get { return msgid; }
            set { msgid = value; }
        }
        //参保地医保区划
        private string insuplc_admdvs = string.Empty;
        /// <summary>
        /// 参保地医保区划
        /// </summary>
        [JsonProperty("insuplc_admdvs")]
        public string Insuplc_admdvs
        {
            get { return insuplc_admdvs; }
            set { insuplc_admdvs = value; }
        }
        //就医地医保区划
        private string mdtrtarea_admvs = string.Empty;
        /// <summary>
        /// 就医地医保区划
        /// </summary>
        [JsonProperty("mdtrtarea_admvs")]
        public string Mdtrtarea_admvs
        {
            get { return mdtrtarea_admvs; }
            set { mdtrtarea_admvs = value; }
        }
        //接收方医保区划代码
        private string recer_admdvs = string.Empty;
        /// <summary>
        /// 接收方医保区划代码
        /// </summary>
        [JsonProperty("recer_admdvs")]
        public string Recer_admdvs
        {
            get { return recer_admdvs; }
            set { recer_admdvs = value; }
        }
        private string dev_no = string.Empty;//设备编号
        /// <summary>
        /// 设备编号
        /// </summary>
        [JsonProperty("dev_no")]
        public string Dev_no
        {
            get { return dev_no; }
            set { dev_no = value; }
        }
        private string dev_safe_info = string.Empty;//设备安全信息
        /// <summary>
        /// 设备安全信息
        /// </summary>
        [JsonProperty("dev_safe_info")]
        public string Dev_safe_info
        {
            get { return dev_safe_info; }
            set { dev_safe_info = value; }
        }
        //签名类型
        private string signtype = string.Empty;
        /// <summary>
        /// 签名类型
        /// </summary>
        [JsonProperty("signtype")]
        public string Signtype
        {
            get { return signtype; }
            set { signtype = value; }
        }
        //数字签名信息
        private string cainfo = string.Empty;
        /// <summary>
        /// 数字签名信息
        /// </summary>
        [JsonProperty("cainfo")]
        public string Cainfo
        {
            get { return cainfo; }
            set { cainfo = value; }
        }
        //接口版本号
        private string infver = string.Empty;
        /// <summary>
        /// 接口版本号
        /// </summary>
        [JsonProperty("infver")]
        public string Infver
        {
            get { return infver; }
            set { infver = value; }
        }
        //经办人类别
        private string opter_type = string.Empty;
        /// <summary>
        /// 经办人类别
        /// </summary>
        [JsonProperty("opter_type")]
        public string Opter_type
        {
            get { return opter_type; }
            set { opter_type = value; }
        }
        //经办人
        private string opter = string.Empty;
        /// <summary>
        /// 经办人
        /// </summary>
        [JsonProperty("opter")]
        public string Opter
        {
            get { return opter; }
            set { opter = value; }
        }
        //经办人姓名
        private string opter_name = string.Empty;
        /// <summary>
        /// 经办人姓名
        /// </summary>
        [JsonProperty("opter_name")]
        public string Opter_name
        {
            get { return opter_name; }
            set { opter_name = value; }
        }
        //交易时间
        private string inf_time = string.Empty;
        /// <summary>
        /// 交易时间
        /// </summary>
        [JsonProperty("inf_time")]
        public string Inf_time
        {
            get { return inf_time; }
            set { inf_time = value; }
        }
        //定点医疗机构编号
        private string fixmedins_code = string.Empty;
        /// <summary>
        /// 定点医疗机构编号
        /// </summary>
        [JsonProperty("fixmedins_code")]
        public string Fixmedins_code
        {
            get { return fixmedins_code; }
            set { fixmedins_code = value; }
        }
        //定点医疗机构名称
        private string fixmedins_name = string.Empty;
        /// <summary>
        /// 定点医疗机构名称
        /// </summary>
        [JsonProperty("fixmedins_name")]
        public string Fixmedins_name
        {
            get { return fixmedins_name; }
            set { fixmedins_name = value; }
        }
        //签到流水号
        private string sign_no = string.Empty;
        /// <summary>
        /// 签到流水号
        /// </summary>
        [JsonProperty("sign_no")]
        public string Sign_no
        {
            get { return sign_no; }
            set { sign_no = value; }
        }//接收方系统代码
        private string recer_sys_code = string.Empty;
        /// <summary>
        /// 接收方系统代码
        /// </summary>
        [JsonProperty("recer_sys_code")]
        public string Recer_sys_code
        {
            get { return recer_sys_code; }
            set { recer_sys_code = value; }
        }
        private T input;
        [JsonProperty("input")]
        public T Input
        {
            get { return input; }
            set { input = value; }
        }
    }
}
