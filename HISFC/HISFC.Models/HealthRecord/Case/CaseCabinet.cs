using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.HealthRecord.Case
{
    /// <summary>
    /// [功能描述: 病案柜实体]<br></br>
    /// [创 建 者: 徐伟哲]<br></br>
    /// [创建时间: 2007/08/16]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// ID病案柜编号，Name病案柜名
    /// </summary>
    [Serializable]
    public class CaseCabinet : FS.FrameWork.Models.NeuObject, FS.HISFC.Models.Base.IValid
    {
        public CaseCabinet()
        {
        }


        #region 变量

        /// <summary>
        /// 病案柜格数

        /// </summary>
        private int gridCount;

        /// <summary>
        /// 病案柜是否有效

        /// </summary>
        private bool isValid;

        /// <summary>
        /// 病案库信息  ID-病案库编码，NAME-病案库名称

        /// </summary>
        private FS.FrameWork.Models.NeuObject store = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 病案柜维护人信息，编码，姓名，时间

        /// </summary>
        //private FS.HISFC.Models.Base.OperEnvironment operEnv = new FS.HISFC.Models.Base.OperEnvironment();
        private FS.HISFC.Models.Base.OperEnvironment operEnv = new FS.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region IValid 成员

        /// <summary>
        /// 病案柜是否有效

        /// </summary>
        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
            set
            {
                this.isValid = value;
            }
        }

        #endregion

        #region 属性


        /// <summary>
        /// 病案柜格数

        /// </summary>
        public int GridCount
        {
            get
            {
                return this.gridCount;
            }
            set
            {
                this.gridCount = value;
            }
        }

        /// <summary>
        /// 病案库信息  ID- 病案库编号；NAME-病案库名称

        /// </summary>
        public FS.FrameWork.Models.NeuObject Store
        {
            get
            {
                return this.store;
            }
            set
            {
                this.store = value;
            }
        }

        /// <summary>
        /// 病案柜维护人信息，编码，姓名，时间

        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment OperEnv
        {
            get
            {
                return this.operEnv;
            }
            set
            {
                this.operEnv = value;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns>病案柜实体</returns>
        public new CaseCabinet Clone()
        {
            CaseCabinet cabinet = base.Clone() as CaseCabinet;

            cabinet.operEnv = this.operEnv.Clone();
            cabinet.store = this.store.Clone();

            return cabinet;
        }

        #endregion

    }
}

