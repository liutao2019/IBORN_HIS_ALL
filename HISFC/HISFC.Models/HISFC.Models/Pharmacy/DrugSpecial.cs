using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Pharmacy
{
    /// <summary>
    /// [功能描述: 特限药品类]<br></br>
    /// [创 建 者: 梁俊泽]<br></br>
    /// [创建时间: 2007-04]<br></br>
    /// </summary>
    [Serializable]
    public class DrugSpecial : FS.FrameWork.Models.NeuObject
    {
        public DrugSpecial()
        {

        }

        #region 变量

        /// <summary>
        /// 特殊项目类型
        /// </summary>
        private EnumDrugSpecialType speType = EnumDrugSpecialType.Dept;
      
        /// <summary>
        /// 特殊项目信息
        /// </summary>
        private FS.FrameWork.Models.NeuObject speItem = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 药品信息
        /// </summary>
        private FS.HISFC.Models.Pharmacy.Item item = new Item();
      
        /// <summary>
        /// 操作人员
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();
       
        /// <summary>
        /// 扩展字段
        /// </summary>
        private string extend;
        #endregion

        #region 属性

        /// <summary>
        /// 特殊项目类型
        /// </summary>
        public EnumDrugSpecialType SpeType
        {
            get
            {
                return speType;
            }
            set
            {
                speType = value;
            }
        }

        /// <summary>
        ///特殊项目信息
        /// </summary>
        public FS.FrameWork.Models.NeuObject SpeItem
        {
            get
            {
                return this.speItem;
            }
            set
            {
                this.speItem = value;
                base.ID = value.ID;
                base.Name = value.Name;
            }
        }

        /// <summary>
        /// 药品信息
        /// </summary>
        public FS.HISFC.Models.Pharmacy.Item Item
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
            }
        }
        
        /// <summary>
        /// 操作人员
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

        /// <summary>
        /// 扩展字段
        /// </summary>
        public string Extend
        {
            get
            {
                return this.extend;
            }
            set
            {
                this.extend = value;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 克隆函数实现
        /// </summary>
        /// <returns></returns>
        public new DrugSpecial Clone()
        {
            DrugSpecial drugSpe = base.Clone() as DrugSpecial;

            drugSpe.speItem = speItem.Clone();

            drugSpe.item = item.Clone();

            drugSpe.oper = oper.Clone();

            return drugSpe;
        }

        #endregion
    }

    /// <summary>
    /// 特殊项目类型
    /// </summary>
    public enum EnumDrugSpecialType
    {
        /// <summary>
        /// 科室
        /// </summary>
        Dept = 0,
        /// <summary>
        /// 人员
        /// </summary>
        Doc = 1
    }
}
