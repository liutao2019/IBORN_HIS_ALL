using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.Models.Order
{
    /// <summary>
    /// FS.HISFC.Models.Order.Order<br></br>{D37652D3-1DB3-4f8c-AFE6-BE21625F082C}
    /// [功能描述: 消息实体]<br></br>
    /// [创 建 者: 梁正东]<br></br>
    /// [创建时间: 2020-08-11]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>

    [Serializable]
    public class MessageTemplate : FS.FrameWork.Models.NeuObject
    {
         /// <summary>
        /// 消息模板实体
        /// </summary>
        public MessageTemplate()
        {

        }

        #region 主键
        /// <summary>
        /// 主键
        /// </summary>
        private string messageTemplateId = "";
        /// <summary>
        /// 主键
        /// </summary>
        public string MessageTemplateId
        {
            get
            {
                return this.messageTemplateId;
            }
            set
            {
                this.messageTemplateId = value;
            }
        }
        #endregion

        #region 模板类型
        /// <summary>
        /// 模板类型
        /// </summary>
        private string msgTemplateType = "";
        /// <summary>
        /// 模板类型
        /// </summary>
        public string MsgTemplateType
        {
            get
            {
                return this.msgTemplateType;
            }
            set
            {
                this.msgTemplateType = value;
            }
        }
        #endregion

        #region 模板渠道
        /// <summary>
        /// 模板渠道
        /// </summary>
        private string msgTemplateChannel = "";
        /// <summary>
        /// 模板渠道
        /// </summary>
        public string MsgTemplateChannel
        {
            get
            {
                return this.msgTemplateChannel;
            }
            set
            {
                this.msgTemplateChannel = value;
            }
        }
        #endregion

        #region 模板主题
        /// <summary>
        /// 模板主题
        /// </summary>
        private string msgTemplateTitle = "";
        /// <summary>
        /// 模板主题
        /// </summary>
        public string MsgTemplateTitle
        {
            get
            {
                return this.msgTemplateTitle;
            }
            set
            {
                this.msgTemplateTitle = value;
            }
        }
        #endregion

        #region 模板内容
        /// <summary>
        /// 模板内容
        /// </summary>
        private string msgTemplateContent = "";
        /// <summary>
        /// 模板主题
        /// </模板内容>
        public string MsgTemplateContent
        {
            get
            {
                return this.msgTemplateContent;
            }
            set
            {
                this.msgTemplateContent = value;
            }
        }
        #endregion

        #region 排序号
        /// <summary>
        /// 排序号
        /// </summary>
        private string sortid = "";
        /// <summary>
        /// 排序号
        /// </模板内容>
        public string Sortid
        {
            get
            {
                return this.sortid;
            }
            set
            {
                this.sortid = value;
            }
        }
        #endregion

        #region 状态
        /// <summary>
        /// 状态
        /// </summary>
        private string state = "";
        /// <summary>
        /// 状态
        /// </模板内容>
        public string State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }
        #endregion



        #region 扩展1
        /// <summary>
        /// 扩展1
        /// </summary>
        private string extfield01 = "";
        /// <扩展1>
        /// 扩展1
        /// </模板内容>
        public string Extfield01
        {
            get
            {
                return this.extfield01;
            }
            set
            {
                this.extfield01 = value;
            }
        }
        #endregion


        #region 扩展2
        /// <summary>
        /// 扩展2
        /// </summary>
        private string extfield02 = "";
        /// <扩展1>
        /// 扩展2
        /// </扩展1>
        public string Extfield02
        {
            get
            {
                return this.extfield02;
            }
            set
            {
                this.extfield02 = value;
            }
        }
        #endregion

        #region 扩展3
        /// <summary>
        /// 扩展3
        /// </summary>
        private string extfield03 = "";
        /// <扩展3>
        /// 扩展3
        /// </扩展3>
        public string Extfield03
        {
            get
            {
                return this.extfield03;
            }
            set
            {
                this.extfield03 = value;
            }
        }
        #endregion

        #region 操作人代码
        /// <summary>
        /// 操作人代码
        /// </summary>
        private string opercode = "";
        /// <扩展3>
        /// 操作人代码
        /// </扩展3>
        public string Opercode
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
        #endregion

        #region 操作人
        /// <summary>
        /// 操作人
        /// </summary>
        private string opername = "";
        /// <扩展3>
        /// 操作人
        /// </扩展3>
        public string Opername
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
        #endregion

        #region 操作时间
        /// <summary>
        /// 操作时间
        /// </summary>
        private DateTime opertime;
        /// <扩展3>
        /// 操作时间
        /// </扩展3>
        public DateTime Opertime
        {
            get
            {
                return this.opertime;
            }
            set
            {
                this.opertime = value;
            }
        }
        #endregion

        

        #region 创建人代码
        /// <summary>
        /// 创建人代码
        /// </summary>
        private string createcode = "";
        /// <扩展3>
        /// 创建人代码
        /// </扩展3>
        public string Createcode
        {
            get
            {
                return this.createcode;
            }
            set
            {
                this.createcode = value;
            }
        }
        #endregion

        #region 创建人
        /// <summary>
        /// 创建人
        /// </summary>
        private string createname = "";
        /// <扩展3>
        /// 创建人
        /// </扩展3>
        public string Createname
        {
            get
            {
                return this.createname;
            }
            set
            {
                this.createname = value;
            }
        }
        #endregion


        #region 创建时间
        /// <summary>
        /// 创建人
        /// </summary>
        private DateTime createtime;
        /// <扩展3>
        /// 创建人
        /// </扩展3>
        public DateTime Createtime
        {
            get
            {
                return this.createtime;
            }
            set
            {
                this.createtime = value;
            }
        }
        #endregion

        #region 方法

        #region 克隆

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new MessageTemplate Clone()
        {
            return base.Clone() as MessageTemplate;
        }

        #endregion

        #endregion
    }

}
