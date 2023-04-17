using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.HISFC.Fee.Models
{
    /// <summary>
    /// MatBase<br></br>
    /// <Font color='#FF1111'>[功能描述: 物资字典实体类]</Font><br></br>
    /// [创 建 者: 耿晓雷]<br></br>
    /// [创建时间: 2009-11-10]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///		/>
    /// </summary>
    [Serializable]
    public class MatBase : FS.HISFC.Models.Base.Item
    {
        #region 构造函数

        public MatBase()
        {
            base.ItemType = FS.HISFC.Models.Base.EnumItemType.MatItem;
            base.IsValid = true;
            base.PackQty = 1;
        }

        #endregion 构造函数

        #region 变量

        #region 私有变量

        ///// <summary>
        ///// 物资分类
        ///// </summary>
        //private MatKind kind = new MatKind();

        /// <summary>
        /// 仓库
        /// </summary>
        private FS.FrameWork.Models.NeuObject storage = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 国家编码--基类
        /// </summary>
        //private string gbCode = "";

        /// <summary>
        /// 别名
        /// </summary>
        private FS.HISFC.Models.Base.Spell otherName = new FS.HISFC.Models.Base.Spell();

        ///// <summary>
        ///// 有效范围
        ///// </summary>
        //private BizLogic.EnumEffectArea effectArea = FS.HISFC.BizLogic.Material.BizLogic.EnumEffectArea.本科室;

        /// <summary>
        /// 有效科室-当有效范围为指定科室时有效
        /// </summary>
        private string effectDept = "";

        /// <summary>
        /// 规格-基类中
        /// </summary>
        //private string specs = "";

        /// <summary>
        /// 最小单位
        /// </summary>
        private string minUnit = "";

        /// <summary>
        /// 最新入库价-大包装
        /// </summary>
        private decimal inPrice = 0;

        /// <summary>
        /// 零售价格
        /// </summary>
        private decimal salePrice = 0;

        /// <summary>
        /// 大包装单位
        /// </summary>
        private string packUnit = "";

        /// <summary>
        /// 大包装数量-基类中
        /// </summary>
        //private decimal packQty = 1;

        /// <summary>
        /// 大包装价格
        /// </summary>
        private decimal packPrice = 0;


        /// <summary>
        /// 最小费用代码
        /// </summary>
        private string feeCode = "";

        /// <summary>
        /// 财务收费标志
        /// </summary>
        private bool isFee = false;

        /// <summary>
        /// 是否有效-基类
        /// </summary>
        //private bool isValid = true;

        /// <summary>
        /// 是否是特殊物资
        /// </summary>
        private bool isSpecial = false;

        /// <summary>
        /// 是否是高值耗材
        /// </summary>
        private bool isHighValue = false;

        ///// <summary>
        ///// 生产厂家
        ///// </summary>
        //private MatCompany factory = new MatCompany();

        ///// <summary>
        ///// 供货公司
        ///// </summary>
        //private MatCompany company = new MatCompany();

        /// <summary>
        /// 来源
        /// </summary>
        private string inSource = "";

        /// <summary>
        /// 用途
        /// </summary>
        private string usage = "";

        /// <summary>
        /// 批文信息
        /// </summary>
        private string approveInfo = "";

        /// <summary>
        /// 生产者
        /// </summary>
        private string mader = "";

        /// <summary>
        /// 注册号
        /// </summary>
        private string registerCode = "";

        /// <summary>
        /// 特殊类别
        /// </summary>
        private string specialType = "";

        /// <summary>
        /// 注册时间
        /// </summary>
        private DateTime registerDate;

        /// <summary>
        /// 到期时间
        /// </summary>
        private DateTime overDate;

        /// <summary>
        /// 是否打包-供应室用(1是0否)
        /// </summary>
        private bool isPack = false;

        /// <summary>
        /// 财务是否已审核
        /// </summary>
        private bool isExamine = false;

        /// <summary>
        /// 是否为一次性耗材标志
        /// </summary>
        private bool isNoRecycle = false;

        /// <summary>
        /// 是否按批次管理
        /// </summary>
        /// {5E811F39-FCA7-4bbf-B2E0-62AD5D499D35}
        private bool isNeedBatchNo = false;

        /// <summary>
        /// 操作环境
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        ///// <summary>
        ///// 对应的生产厂家认证信息列表
        ///// </summary>
        //private List<MatBaseReg> baseRegList = new List<MatBaseReg>();

        ///// <summary>
        ///// 附件
        ///// </summary>
        //private List<MatFile> files = new List<MatFile>();

        /// <summary>
        /// 是否按月计划入库
        /// </summary>
        /// {7A8734DD-78FB-40ec-817E-8964CE065D90}
        private bool isPlan = false;

        private bool isB = false;

        private string extend1;
        

        #endregion 私有变量

        #region 保护变量
        #endregion 保护变量

        #region 公开变量
        #endregion 公开变量

        #endregion 变量

        #region 属性
        /// <summary>
        /// 是否按月计划入库
        /// </summary>
        /// {7A8734DD-78FB-40ec-817E-8964CE065D90}
        public bool IsPlan
        {
            get { return isPlan; }
            set { isPlan = value; }
        }

        ///// <summary>
        ///// 物资分类
        ///// </summary>
        //public MatKind Kind
        //{
        //    get
        //    {
        //        return kind;
        //    }
        //    set
        //    {
        //        kind = value;
        //    }
        //}

        /// <summary>
        /// 仓库
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

        /// <summary>
        /// 国家编码--基类
        /// </summary>
        //public string GbCode
        //{
        //    get
        //    {
        //        return gbCode;
        //    }
        //    set
        //    {
        //        gbCode = value;
        //    }
        //}

        /// <summary>
        /// 别名
        /// </summary>
        public FS.HISFC.Models.Base.Spell OtherName
        {
            get
            {
                return otherName;
            }
            set
            {
                otherName = value;
            }
        }

        ///// <summary>
        ///// 有效范围
        ///// </summary>
        //public BizLogic.EnumEffectArea EffectArea
        //{
        //    get
        //    {
        //        return effectArea;
        //    }
        //    set
        //    {
        //        effectArea = value;
        //    }
        //}

        /// <summary>
        /// 有效科室-当有效范围为指定科室时有效
        /// </summary>
        public string EffectDept
        {
            get
            {
                return effectDept;
            }
            set
            {
                effectDept = value;
            }
        }

        /// <summary>
        /// 最小单位
        /// </summary>
        public string MinUnit
        {
            get
            {
                return minUnit;
            }
            set
            {
                minUnit = value;
            }
        }

        /// <summary>
        /// 最新入库价-大包装
        /// </summary>
        public decimal InPrice
        {
            get
            {
                return inPrice;
            }
            set
            {
                inPrice = value;
            }
        }

        /// <summary>
        /// 零售价格
        /// </summary>
        public decimal SalePrice
        {
            get
            {
                return salePrice;
            }
            set
            {
                salePrice = value;
            }
        }

        /// <summary>
        /// 大包装单位
        /// </summary>
        public string PackUnit
        {
            get
            {
                return packUnit;
            }
            set
            {
                packUnit = value;
            }
        }

        /// <summary>
        /// 大包装价格
        /// </summary>
        public decimal PackPrice
        {
            get
            {
                return packPrice;
            }
            set
            {
                packPrice = value;
            }
        }

        ///// <summary>
        ///// 加价方式
        ///// </summary>
        //public BizLogic.EnumAddRateType AddRateType
        //{
        //    get
        //    {
        //        return addRateType;
        //    }
        //    set
        //    {
        //        addRateType = value;
        //    }
        //}

        /// <summary>
        /// 最小费用代码
        /// </summary>
        public string FeeCode
        {
            get
            {
                return feeCode;
            }
            set
            {
                feeCode = value;
            }
        }

        /// <summary>
        /// 财务收费标志
        /// </summary>
        public bool IsFee
        {
            get
            {
                return isFee;
            }
            set
            {
                isFee = value;
            }
        }

        /// <summary>
        /// 是否是特殊物资
        /// </summary>
        public bool IsSpecial
        {
            get
            {
                return isSpecial;
            }
            set
            {
                isSpecial = value;
            }
        }

        /// <summary>
        /// 是否是高值耗材
        /// </summary>
        public bool IsHighValue
        {
            get
            {
                return isHighValue;
            }
            set
            {
                isHighValue = value;
            }
        }

        ///// <summary>
        ///// 生产厂家
        ///// </summary>
        //public MatCompany Factory
        //{
        //    get
        //    {
        //        return factory;
        //    }
        //    set
        //    {
        //        factory = value;
        //    }
        //}

        ///// <summary>
        ///// 供货公司
        ///// </summary>
        //public MatCompany Company
        //{
        //    get
        //    {
        //        return company;
        //    }
        //    set
        //    {
        //        company = value;
        //    }
        //}

        /// <summary>
        /// 来源
        /// </summary>
        public string InSource
        {
            get
            {
                return inSource;
            }
            set
            {
                inSource = value;
            }
        }

        /// <summary>
        /// 用途
        /// </summary>
        public string Usage
        {
            get
            {
                return usage;
            }
            set
            {
                usage = value;
            }
        }

        /// <summary>
        /// 批文信息
        /// </summary>
        public string ApproveInfo
        {
            get
            {
                return approveInfo;
            }
            set
            {
                approveInfo = value;
            }
        }

        /// <summary>
        /// 生产者
        /// </summary>
        public string Mader
        {
            get
            {
                return mader;
            }
            set
            {
                mader = value;
            }
        }

        /// <summary>
        /// 注册号
        /// </summary>
        public string RegisterCode
        {
            get
            {
                return registerCode;
            }
            set
            {
                registerCode = value;
            }
        }

        /// <summary>
        /// 特殊类别
        /// </summary>
        public string SpecialType
        {
            get
            {
                return specialType;
            }
            set
            {
                specialType = value;
            }
        }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegisterDate
        {
            get
            {
                return registerDate;
            }
            set
            {
                registerDate = value;
            }
        }

        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime OverDate
        {
            get
            {
                return overDate;
            }
            set
            {
                overDate = value;
            }
        }

        /// <summary>
        /// 是否打包-供应室用(1是0否)
        /// </summary>
        public bool IsPack
        {
            get
            {
                return isPack;
            }
            set
            {
                isPack = value;
            }
        }

        /// <summary>
        /// 财务是否已审核
        /// </summary>
        public bool IsExamine
        {
            get
            {
                return isExamine;
            }
            set
            {
                isExamine = value;
            }
        }

        /// <summary>
        /// 是否为一次性耗材标志
        /// </summary>
        public bool IsNoRecycle
        {
            get
            {
                return isNoRecycle;
            }
            set
            {
                isNoRecycle = value;
            }
        }

        /// <summary>
        /// 是否按批次号管理
        /// </summary>
        /// {5E811F39-FCA7-4bbf-B2E0-62AD5D499D35}
        public bool IsNeedBatchNo
        {
            get
            {
                return isNeedBatchNo;
            }
            set
            {
                isNeedBatchNo = value;
            }
        }

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

 
      
        public string Extend1
        {
            set
            {
                this.extend1 = value;
            }
            get
            {
                return this.extend1;
            }
        }

        public bool IsB
        {
            set
            {
                this.isB = value;
            }
            get
            {
                return this.isB;
            }
        }

        /// <summary>
        /// 物品注册号
        /// </summary>
        private string registerNo = "";
        /// <summary>
        /// 物品注册号
        /// </summary>
        public string RegisterNo
        {
            set
            {
                this.registerNo = value;
            }
            get
            {
                return this.registerNo;
            }
        }
        #endregion 属性

        #region 方法

        #region 资源释放
        #endregion 资源释放

        #region 克隆

        /// <summary>
        /// 克隆函数
        /// </summary>
        /// <returns>返回克隆后的实体</returns>
        public new MatBase Clone()
        {
            MatBase matBase = base.Clone() as MatBase;

            matBase.storage = this.storage.Clone();
            matBase.otherName = this.otherName.Clone();
         

            return matBase;
        }

        #endregion 克隆

        #region 私有方法
        #endregion 私有方法

        #region 保护方法
        #endregion 保护方法

        #region 公开方法

        #endregion 公开方法

        #endregion 方法

        #region 事件
        #endregion 事件

        #region 接口实现
        #endregion 接口实现
    }

}
