using System;
using System.Collections.Generic;
using FS.FrameWork.Models;
using System.Linq;
using System.Text;

namespace FS.HISFC.Models.Label
{
    /// <summary>
    /// [功能描述: his连接crm的标签实体]<br></br>
    /// [创 建 者: 胡云贵]<br></br>
    /// [创建时间: 2019-12-23]<br></br>
    /// {929E868E-3D45-4a06-AB38-722E309BD3C2}
    /// </summary> 
    [System.Serializable]
    public class PatientLabel : NeuObject
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PatientLabel()
        {

        }

        #region 变量
        /// <summary>
        /// 患者id
        /// </summary>
        private string patientId;

        /// <summary>
        /// 标签编码
        /// </summary>
        private string labelId;

        /// <summary>
        /// 标签id
        /// </summary>
        private string patientLabelId;
        
        /// <summary>
        /// 标签内容
        /// </summary>
        private string labelContent;

        /// <summary>
        /// 标签memo
        /// </summary>
        private string labelMemo;

        /// <summary>
        /// 标签创建时间
        /// </summary>
        private string labelCreateTime;

        /// <summary>
        /// 创建人姓名
        /// </summary>
        private string createrName;

        /// <summary>
        /// 创建人工号
        /// </summary>
        private string createrCode;

        /// <summary>
        /// 一级标签类型
        /// </summary>
        private string labelType1;

        /// <summary>
        /// 二级标签类型
        /// </summary>
        private string labelType2;

        /// <summary>
        /// 三级标签类型
        /// </summary>
        private string labelType3;

        /// <summary>
        /// 状态可用性
        /// </summary>
        private string labelStatus;
        #endregion

        #region 属性
        public string PatientId
        {
            get
            {
                return this.patientId;
            }
            set
            {
                this.patientId = value;
            }
        }

        public string LabelId
        {
            get
            {
                return this.labelId;
            }
            set
            {
                this.labelId = value;
            }
        }

        public string LabelContent
        {
            get
            {
                return this.labelContent;
            }
            set
            {
                this.labelContent = value;
            }
        }

        public string PatientLabelId
        {
            get
            {
                return this.patientLabelId;
            }
            set
            {
                this.patientLabelId = value;
            }
        }

        public string LabelMemo
        {
            get
            {
                return this.labelMemo;
            }
            set
            {
                this.labelMemo = value;
            }
        }

        public string LabelCreatetime
        {
            get
            {
                return this.labelCreateTime;
            }
            set
            {
                this.labelCreateTime = value;
            }
        }

        public string CreaterName
        {
            get
            {
                return this.createrName;
            }
            set
            {
                this.createrName = value;
            }
        }

        public string CreaterCode
        {
            get
            {
                return this.createrCode;
            }
            set
            {
                this.createrCode = value;
            }
        }

        public string LabelType1
        {
            get
            {
                return this.labelType1;
            }
            set
            {
                this.labelType1 = value;
            }
        }

        public string LabelType2
        {
            get
            {
                return this.labelType2;
            }
            set
            {
                this.labelType2 = value;
            }
        }

        public string LabelType3
        {
            get
            {
                return this.labelType3;
            }
            set
            {
                this.labelType3 = value;
            }
        }

        public string LabelStatus
        {
            get
            {
                return this.labelStatus;
            }
            set
            {
                this.labelStatus = value;
            }
        }


        #endregion

        #region 方法

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new PatientLabel Clone()
        {
            PatientLabel patientLabel = base.Clone() as PatientLabel;
            return patientLabel;
        }

        #endregion
    }
}
