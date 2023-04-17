using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Pharmacy
{
    /// <summary>
    /// [功能描述: 药品模版类]<br></br>
    /// [创 建 者: 飞斯]<br></br>
    /// [创建时间: 2006-12]<br></br>
    /// </summary>
    [Serializable]
    public class DrugStencil : FS.FrameWork.Models.NeuObject
    {
        public DrugStencil()
        {

        }

        #region 变量

        /// <summary>
        /// 科室信息
        /// </summary>
        private FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 模版类型
        /// </summary>
        private Pharmacy.DrugStencilEnumService openType = new DrugStencilEnumService();

        /// <summary>
        /// 模版信息
        /// </summary>
        private FS.FrameWork.Models.NeuObject stencil = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 药品信息
        /// </summary>
        private FS.HISFC.Models.Pharmacy.Item item = new Item();

        /// <summary>
        /// 顺序号
        /// </summary>
        private int sortNO;

        /// <summary>
        /// 操作员
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// 扩展信息
        /// </summary>
        private string extend;

        #endregion

        #region 属性

        /// <summary>
        /// 科室信息
        /// </summary>
        public FS.FrameWork.Models.NeuObject Dept
        {
            get
            {
                return this.dept;
            }
            set
            {
                this.dept = value;
            }
        }

        /// <summary>
        /// 模版类型
        /// </summary>
        public DrugStencilEnumService OpenType
        {
            get
            {
                return this.openType;
            }
            set
            {
                this.openType = value;
                if (value.ID != null)
                    base.Memo = value.ID.ToString();
            }
        }

        /// <summary>
        /// 模版信息
        /// </summary>
        public FS.FrameWork.Models.NeuObject Stencil
        {
            get
            {
                return this.stencil;
            }
            set
            {
                this.stencil = value;
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
                return this.item;
            }
            set
            {
                this.item = value;
            }
        }

        /// <summary>
        /// 顺序号
        /// </summary>
        public int SortNO
        {
            get
            {
                return this.sortNO;
            }
            set
            {
                this.sortNO = value;
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
        /// 克隆函数
        /// </summary>
        /// <returns>成功返回当前实例的克隆信息</returns>
        public new DrugStencil Clone()
        {
            DrugStencil drugStencil = base.Clone() as DrugStencil;
            drugStencil.dept = this.dept.Clone();
            drugStencil.stencil = this.stencil.Clone();
            drugStencil.item = this.item.Clone();
            drugStencil.oper = this.oper.Clone();

            return drugStencil;
        }

        #endregion
    }
}
