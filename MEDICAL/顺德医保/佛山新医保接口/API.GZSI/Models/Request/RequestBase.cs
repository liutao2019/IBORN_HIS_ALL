using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Reflection;
using Newtonsoft.Json;

namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 请求实体基类
    /// [目前暂未使用此基类]
    /// </summary>
    public abstract class RequestBase
    {
        #region 公共入参
        public string infno { get; set; }//交易编号
        public string msgid { get; set; }//发送方报文ID
        public string insuplc_admdvs { get; set; }//参保地医保区划
        public string mdtrtarea_admvs { get; set; }//就医地医保区划
        public string recer_admdvs { get; set; }//接收方医保区划代码
        public string dev_no { get; set; }//设备编号
        public string dev_safe_info { get; set; }//设备安全信息
        public string signtype { get; set; }//签名类型
        public string cainfo { get; set; }//数字签名信息
        public string infver { get; set; }//接口版本号
        public string opter_type { get; set; }//经办人类别
        public string opter { get; set; }//经办人
        public string opter_name { get; set; }//经办人姓名
        public string inf_time { get; set; }//交易时间
        public string fixmedins_code { get; set; }//定点医疗机构编号
        public string sign_no { get; set; }//签到流水号
        public string recer_sys_code { get; set; }//接收方系统代码
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public RequestBase()
        {
            // 设置属性默认值
            PropertyInfo[] propertys = this.GetType().GetProperties();
            foreach (var property in propertys)
            {
                if (property.PropertyType == typeof(string))
                {
                    property.SetValue(this, string.Empty, null);
                }
            }
            //this.infno = "1101";
            this.msgid = "123";
            this.insuplc_admdvs = "";
            this.mdtrtarea_admvs = "";
            this.recer_admdvs = "440110";
            this.dev_no = "";
            this.dev_safe_info = "";
            this.signtype = "SM3";
            this.cainfo = "";
            this.infver = "1.0.0";
            this.opter_type = "1";
            this.opter = "01";
            this.opter_name = "张三";
            this.sign_no = "";
            this.inf_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.fixmedins_code = "011134";
            this.recer_sys_code = "123";
        }
    }
}
