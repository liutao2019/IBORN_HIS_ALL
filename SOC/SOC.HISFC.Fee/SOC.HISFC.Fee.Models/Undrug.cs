using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.Models
{
    /// <summary>
    /// [功能描述: 非药品补充实体]<br></br>
    /// [创 建 者: zhaoj]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    [System.ComponentModel.DisplayName("物价字典信息")]
    [Serializable]
    public class Undrug : FS.HISFC.Models.Fee.Item.Undrug
    {
        /// <summary>
        /// 注册码
        /// </summary>
        private string registerCode;
        /// <summary>
        /// 注册时间
        /// </summary>
        private DateTime registerDate;
        /// <summary>
        /// 生产厂家
        /// </summary>
        private string producer;

        /// <summary>
        /// 项目名称信息
        /// </summary>
        private FS.HISFC.Models.IMA.NameService nameCollection = null;

        /// <summary>
        /// 项目名称信息
        /// </summary>
        [System.ComponentModel.DisplayName("名称集合")]
        [System.ComponentModel.Description("包括通用名，别名，英文名，国际编码，国家编码")]
        public FS.HISFC.Models.IMA.NameService NameCollection
        {
            get
            {
                if(this.nameCollection==null)
                {
                    this.nameCollection = new FS.HISFC.Models.IMA.NameService();
                }
                return this.nameCollection;
            }
            set
            {
                this.nameCollection = value;
            }
        }

        /// <summary>
        /// 医保等级
        /// </summary>
        private FS.FrameWork.Models.NeuObject grade = null;

        /// <summary>
        /// 医保等级
        /// </summary>
        [System.ComponentModel.DisplayName("医保等级")]
        [System.ComponentModel.Description("用来对照医保用")]
        public new FS.FrameWork.Models.NeuObject Grade
        {
            get
            {
                if (grade == null)
                {
                    grade = new FS.FrameWork.Models.NeuObject();
                }

                return grade;
            }
            set
            {
                grade = value;
            }
        }

        /// <summary>
        /// 国际编码（已作废，请使用NameCollection.InternationalCode）
        /// </summary>
        [System.ComponentModel.DisplayName("国际编码")]
        [System.ComponentModel.Description("国际编码，已作废，请使用NameCollection.InternationalCode")]
        [Obsolete("NationCode已作废，请使用NameCollection.InternationalCode")]
        public new string NationCode
        {
            get
            {
                return this.NameCollection.InternationalCode;
            }
            set
            {
                this.NameCollection.InternationalCode = value;
            }
        }
        /// <summary>
        /// 医技预约类型
        /// </summary>
        public FS.FrameWork.Models.NeuObject MTType
        {
            get { return mtType; }
            set { this.mtType = value; }
        }
        private FS.FrameWork.Models.NeuObject mtType = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// 国家编码（已作废，请使用NameCollection.GBCode）
        /// </summary>
        [System.ComponentModel.DisplayName("国家编码")]
        [System.ComponentModel.Description("国家编码，已作废，请使用NameCollection.GBCode")]
        [Obsolete("NationCode已作废，请使用NameCollection.GBCode")]
        public new string GBCode
        {
            get
            {
                return this.NameCollection.GbCode;
            }
            set
            {
                this.NameCollection.GbCode = value;
            }
        }

        /// <summary>
        /// 注册码
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName("注册码")]
        public string RegisterCode
        {
            get
            {
                return this.registerCode;
            }
            set
            {
                this.registerCode = value;
            }
        }

        /// <summary>
        /// 注册时间
        /// </summary>
        [System.ComponentModel.DisplayName("注册时间")]
        public DateTime RegisterDate
        {
            get
            {
                return this.registerDate;
            }
            set
            {
                this.registerDate = value;
            }
        }

        /// <summary>
        /// 生产厂家
        /// </summary>
        [System.ComponentModel.DisplayName("生产厂家")]
        public string Producer
        {
            get
            {
                return this.producer;
            }
            set
            {
                this.producer = value;
            }
        }

        /// <summary>
        /// 默认执行科室（门诊）
        /// </summary>
        private string defaultExecDeptForOut;

        /// <summary>
        /// 默认执行科室（门诊）
        /// </summary>
        [System.ComponentModel.DisplayName("默认执行科室（门诊）")]
        public string DefaultExecDeptForOut
        {
            get
            {
                return defaultExecDeptForOut;
            }
            set
            {
                defaultExecDeptForOut = value;
            }
        }

        /// <summary>
        /// 默认执行科室（住院）
        /// </summary>
        private string defaultExecDeptForIn;

        /// <summary>
        /// 默认执行科室（住院）
        /// </summary>
        [System.ComponentModel.DisplayName("默认执行科室（住院）")]
        public string DefaultExecDeptForIn
        {
            get
            {
                return defaultExecDeptForIn;
            }
            set
            {
                defaultExecDeptForIn = value;
            }
        }

        public new Undrug Clone()
        {
            Undrug u = base.Clone() as Undrug;
            u.grade = this.Grade.Clone();
            u.nameCollection = this.NameCollection.Clone();

            return u;
        }

    }
}
