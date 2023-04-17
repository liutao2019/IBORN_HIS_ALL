using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Models.Pharmacy.Constant
{
    /// <summary>
    /// [功能描述: 药品零售价调价价格生成公式实体]<br></br>
    /// [创 建 者: Cube]<br></br>
    /// [创建时间: 2012-12]<br></br>
    /// </summary>
    public class ControlAttribute : FS.FrameWork.Models.NeuObject
    {
        private string curObjectCode = "";

        public string ObjectCode
        {
            get 
            {
                return curObjectCode;
            }
            set
            {
                this.curObjectCode = value;
            }
            
        }

        private string curObjectName = "";

        public string ObjectName
        {
            get
            {
                return curObjectName;
            }
            set
            {
                this.curObjectName = value;
            }

        }


        private string curObjectType = "";

        public string ObjectType
        {
            get
            {
                return curObjectType;
            }
            set
            {
                this.curObjectType = value;
            }

        }

        private string curAttributeCode = "";

        public string AttributeCode
        {
            get
            {
                return curAttributeCode;
            }
            set
            {
                this.curAttributeCode = value;
            }

        }

        private string curAttributeName = "";

        public string AttributeName
        {
            get
            {
                return curAttributeName;
            }
            set
            {
                this.curAttributeName = value;
            }

        }

        private string curAttributeType = "";

        public string AttributeType
        {
            get
            {
                return curAttributeType;
            }
            set
            {
                this.curAttributeType = value;
            }

        }

        private string curValidState = "";

        public string ValidState
        {
            get
            {
                return this.curValidState;
            }
            set
            {
                this.curValidState = value;
            }
        }
        private string operID = "";

        /// <summary>
        /// 操作员
        /// </summary>
        public string OperID
        {
            get { return operID; }
            set { operID = value; }
        }

        private DateTime operTime = new DateTime();

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperTime
        {
            get { return operTime; }
            set { operTime = value; }
        }
    }
}
