using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Nurse
{
    /// <summary>
    /// [功能描述: 注射过程记录]<br></br>
    /// [创 建 者: 徐伟哲]<br></br>
    /// [创建时间: 2007/08/31]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// ID注射单号,MEMO备注     
    /// </summary>
    /// 
    [System.Serializable]
    public class InjectRecord : FS.FrameWork.Models.NeuObject
    {
        public InjectRecord()
        {
        }

        /// <summary>
        /// 操作员记录

        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment operEnv = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// 不良反应记录
        /// </summary>
        private FS.HISFC.Models.Nurse.Kickback kBack = new Kickback();

        /// <summary>
        /// 操作类型：0预约,1配药,2注射,3巡视,4拔针
        /// </summary>
        private int operType;

        /// <summary>
        /// 操作员记录

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

        /// <summary>
        /// 不良反应记录
        /// </summary>
        public FS.HISFC.Models.Nurse.Kickback KBack
        {
            get
            {
                return this.kBack;
            }
            set
            {
                this.kBack = value;
            }
        }

        /// <summary>
        /// 操作类型：0预约,1配药,2注射,3巡视,4拔针
        /// </summary>
        public int OperType
        {
            get
            {
                return this.operType;
            }
            set
            {
                this.operType = value;
            }
        }

        /// <summary>
        /// 复制新对象

        /// </summary>
        /// <returns>InjectRecord</returns>
        public new InjectRecord Clone()
        {
            InjectRecord ir = base.Clone() as InjectRecord;
            ir.operEnv = this.operEnv.Clone();
            ir.kBack = this.kBack.Clone();
            return ir;
        }
    }
}
