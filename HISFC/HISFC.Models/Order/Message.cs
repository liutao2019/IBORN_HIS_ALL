using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.Models.Order
{

    /// <summary>
    /// FS.HISFC.Models.Order.Order<br></br>
    /// [功能描述: 消息实体]<br></br>
    /// [创 建 者: 梁正东]<br></br>{2E47C0BD-CD18-4ce8-B244-02DCE3B30DB6}
    /// [创建时间: 2020-07-30]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    [Serializable]
    public class Message: FS.FrameWork.Models.NeuObject
    {
         /// <summary>
        /// 消息实体
        /// ID 医嘱流水号
        /// </summary>
        public Message()
        {

        }



        #region 主键

        /// <summary>
        /// 主键
        /// </summary>
        private string message_id = "";

        #endregion

        #region 主键

        /// <summary>
        /// 主键
        /// </summary>
        public string Message_id
        {
            get
            {
                return this.message_id;
            }
            set
            {
                this.message_id = value;
            }
        }

        /// <summary>
        /// 卡号
        /// </summary>
        private string cardno = "";

        #endregion

        #region 卡号

        /// <summary>
        /// 卡号
        /// </summary>
        public string Card_NO
        {
            get
            {
                return this.cardno;
            }
            set
            {
                this.cardno = value;
            }
        }

        /// <summary>
        /// 电话
        /// </summary>
        private string phone = "";

        #endregion

        #region 电话

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone
        {
            get
            {
                return this.phone;
            }
            set
            {
                this.phone = value;
            }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        private string name = "";

        #endregion

        #region 姓名

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// 内容
        /// </summary>
        private string content = "";

        #endregion

        #region 内容

        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get
            {
                return this.content;
            }
            set
            {
                this.content = value;
            }
        }


        /// <summary>
        /// 发送人id
        /// </summary>
        private string opercode = "";

        #endregion

        #region 发送人id

        /// <summary>
        /// 发送人id
        /// </summary>
        public string OperCode
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
        /// 发送人
        /// </summary>
        private string opername = "";

        #endregion

        #region 发送人

        /// <summary>
        /// 发送人
        /// </summary>
        public string OperName
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
        /// 操作时间
        /// </summary>
        private DateTime opertime;

        #endregion

        #region 操作时间

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperTime
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

        /// <summary>
        /// 发送类型
        /// </summary>
        private string message_type = "";

        #endregion

        #region 发送类型

        /// <summary>
        /// 发送类型
        /// </summary>
        public string MessageType
        {
            get
            {
                return this.message_type;
            }
            set
            {
                this.message_type = value;
            }
        }
        /// <summary>
        /// 发送渠道
        /// </summary>
        private string message_channel = "";

        #endregion

        #region 发送渠道

        /// <summary>
        /// 发送渠道
        /// </summary>
        public string MessageChannel
        {
            get
            {
                return this.message_channel;
            }
            set
            {
                this.message_channel = value;
            }
        }
        /// <summary>
        /// 消息主题
        /// </summary>
        private string message_title = "";

        #endregion

        #region 消息主题

        /// <summary>
        /// 消息主题
        /// </summary>
        public string MessageTitle
        {
            get
            {
                return this.message_title;
            }
            set
            {
                this.message_title = value;
            }
        }

        /// <summary>
        /// 发送结果
        /// </summary>
        private string sendresult = "";

        #endregion

        #region 发送结果

        /// <summary>
        /// 发送结果
        /// </summary>
        public string SendResult
        {
            get
            {
                return this.sendresult;
            }
            set
            {
                this.sendresult = value;
            }
        }
        /// <summary>
        /// 扩展1
        /// </summary>
        private string extfield01 = "";

        #endregion

        #region 扩展1

        /// <summary>
        /// 扩展1
        /// </summary>
        public string ExtField01
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
        /// <summary>
        /// 扩展3
        /// </summary>
        private string extfield03 = "";

        #endregion

        #region 扩展3

        /// <summary>
        /// 扩展3
        /// </summary>
        public string ExtField03
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
        /// <summary>
        /// 扩展2
        /// </summary>
        private string extfield02 = "";

        #endregion

        #region 扩展2

        /// <summary>
        /// 扩展2
        /// </summary>
        public string ExtField02
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
        #region 方法

        #region 克隆

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new Message Clone()
        {
            return base.Clone() as Message;
        }

        #endregion

        #endregion

    }
}
