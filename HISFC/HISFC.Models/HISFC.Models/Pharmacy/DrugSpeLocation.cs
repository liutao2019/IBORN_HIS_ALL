using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Pharmacy
{
    /// <summary>
    /// [功能描述: 药品特定位置类]<br></br>
    /// [创 建 者: 梁俊泽]<br></br>
    /// [创建时间: 2007-11]<br></br>
    /// <说明>
    ///     1、  ID 取药药房 Name 取药药房名称
    /// </说明>
    /// </summary>
    [Serializable]
    public class DrugSpeLocation : FS.FrameWork.Models.NeuObject
    {
        public DrugSpeLocation()
        {

        }

        #region 域变量

        /// <summary>
        /// 特定药品
        /// </summary>
        private FS.HISFC.Models.Pharmacy.Item item = new Item();

        /// <summary>
        /// 取药病区
        /// </summary>
        private FS.FrameWork.Models.NeuObject roomDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 操作员
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region 属性

        /// <summary>
        /// 特定药品
        /// </summary>
        public FS.HISFC.Models.Pharmacy.Item Item
        {
            get            
            {
                return this.item;
            }
            set
            {
                this.item = value;
            }
        }

        /// <summary>
        /// 取药病区
        /// </summary>
        public FS.FrameWork.Models.NeuObject RoomDept
        {
            get
            {
                return this.roomDept;
            }
            set
            {
                this.roomDept = value;
            }
        }

        /// <summary>
        /// 操作员
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get
            {
                return this.oper;
            }
            set
            {
                this.oper = value;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new DrugSpeLocation Clone()
        {
            DrugSpeLocation dr = base.Clone() as DrugSpeLocation;

            dr.item = this.item.Clone();
            dr.roomDept = this.roomDept.Clone();
            dr.oper = this.oper.Clone();

            return dr;
        }

        #endregion
    }
}
