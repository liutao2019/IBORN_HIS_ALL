using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.Models.Pharmacy
{
    /// <summary>
    /// CheckSpecial<br></br>
    /// <Font color='#FF1111'>[功能描述: 特殊盘点药品维护{98F0BF7A-5F41-4de3-884F-B38E71B41A8C}]</Font><br></br>
    /// [创 建 者: 飞斯]<br></br>
    /// [创建时间: 2010-08-05]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///		/>
    /// </summary>
    public class CheckSpecial : FS.FrameWork.Models.NeuObject
    {
        #region 变量、属性

        //库房
        private FS.FrameWork.Models.NeuObject storage = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// 库房
        /// </summary>
        public FS.FrameWork.Models.NeuObject Storage
        {
            get
            {
                return storage;
            }
            set
            {
                storage = value;
            }
        }
        //药品性质
        private FS.FrameWork.Models.NeuObject drugQuality = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// 药品性质
        /// </summary>
        public FS.FrameWork.Models.NeuObject DrugQuality
        {
            get
            {
                return drugQuality;
            }
            set
            {
                drugQuality = value;
            }
        }

        //盘点方式(1.正常，2.按批次)
        private string checkType = "";
        /// <summary>
        /// 盘点方式(1.正常，2.按批次)
        /// </summary>
        public string CheckType
        {
            get
            {
                return checkType;
            }
            set
            {
                checkType = value;
            }
        }
        //操作环境
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();
        /// <summary>
        /// 操作环境
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get
            {
                return oper;
            }
            set
            {
                oper = value;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new CheckSpecial Clone()
        {
            CheckSpecial checkSpecial = base.Clone() as CheckSpecial;

            checkSpecial.storage = this.storage.Clone();
            checkSpecial.drugQuality = this.drugQuality.Clone();
            checkSpecial.oper = this.oper.Clone();

            return checkSpecial;
        }
        #endregion
    }
}
