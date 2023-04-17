using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.MedicalPackage
{
    public class Package : FS.HISFC.Models.Base.Spell, FS.HISFC.Models.Base.IValid,FS.HISFC.Models.Base.ISort
    {
        /// <summary>
        /// 使用范围 1门诊/2住院/3体检/4全部
        /// </summary>
        private ServiceTypes userType;

        /// <summary>
        /// 套餐类型
        /// </summary>
        private string packageType = string.Empty;

        /// <summary>
        /// 创建人
        /// </summary>
        private OperEnvironment CreateInfo = new OperEnvironment();


        /// <summary>
        /// 修改人
        /// </summary>
        private OperEnvironment ModifyInfo = new OperEnvironment();

        /// <summary>
        /// 使用范围 1门诊/2住院/3体检/4全部
        /// </summary>
        public ServiceTypes UserType
        {
            get
            {
                return this.userType;
            }
            set
            {
                this.userType = value;
            }
        }

        /// <summary>
        /// 套餐类型
        /// </summary>
        public String PackageType
        {
            get { return this.packageType; }
            set { this.packageType = value; }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateOper
        {
            get
            {
                if (this.CreateInfo == null)
                {
                    this.CreateInfo = new OperEnvironment();
                }
                return this.CreateInfo.ID;
            }
            set
            {
                if (this.CreateInfo == null)
                {
                    this.CreateInfo = new OperEnvironment();
                }
                this.CreateInfo.ID = value;
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get 
            {
                if (this.CreateInfo == null)
                {
                    this.CreateInfo = new OperEnvironment();
                }
                return this.CreateInfo.OperTime;
            }
            set 
            {
                if (this.CreateInfo == null)
                {
                    this.CreateInfo = new OperEnvironment();
                }
                this.CreateInfo.OperTime = value;
            }
        }

        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifyOper
        {
            get
            {
                if (this.ModifyInfo == null)
                {
                    this.ModifyInfo = new OperEnvironment();
                }
                return this.ModifyInfo.ID;
            }
            set
            {
                if (this.ModifyInfo == null)
                {
                    this.ModifyInfo = new OperEnvironment();
                }
                this.ModifyInfo.ID = value;
            }
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime
        {
            get
            {
                if (this.ModifyInfo == null)
                {
                    this.ModifyInfo = new OperEnvironment();
                }
                return this.ModifyInfo.OperTime;
            }
            set
            {
                if (this.ModifyInfo == null)
                {
                    this.ModifyInfo = new OperEnvironment();
                }
                this.ModifyInfo.OperTime = value;
            }
        }

        //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}
        #region 新增字段

        /// <summary>
        /// 套餐级别 1-套餐，2-子套餐
        /// </summary>
        public string PackageClass
        {
            get;
            set;
        }

        /// <summary>
        /// 父级编码
        /// </summary>
        public string ParentCode
        {
            get;
            set;
        }

        /// <summary>
        /// 组合套餐标记
        /// </summary>
        public string ComboFlag
        {
            get;
            set;
        }

        /// <summary>
        /// 主套餐包标记
        /// </summary>
        public string MainFlag
        {
            get;
            set;
        }


        /// <summary>
        /// 特殊套餐标记
        /// </summary>
        public string SpecialFlag
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// 有效标记 0 false 无效，1 true 有效
        /// </summary>		
        public bool IsValid
        {
            get;
            set;
        }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortID
        {
            get;
            set;
        }


        public new Package Clone()
        {
            Package package = new Package();
            package.ID = this.ID;
            package.Name = this.Name;
            package.SpellCode = this.spellCode;
            package.UserCode = this.UserCode;
            package.PackageType = this.PackageType;
            package.UserType = this.userType;
            package.SortID = this.SortID;
            package.IsValid = this.IsValid;
            package.Memo = this.Memo;
            package.PackageClass = this.PackageClass;
            package.ParentCode = this.ParentCode;
            package.ComboFlag = this.ComboFlag;
            package.MainFlag = this.MainFlag;
            package.SpecialFlag = this.SpecialFlag;
            package.ModifyOper = this.ModifyOper;
            package.ModifyTime = this.ModifyTime;
            package.CreateOper = this.CreateOper;
            package.CreateTime = this.CreateTime;
            return package;
        }
    }

    /// <summary>
    /// 套餐标记{74958B4A-AD55-4775-BE30-E030DDC47A64}
    /// </summary>
    public class PackageTag : FS.FrameWork.Models.NeuObject
    {
        public string CardNO { set; get; }

        public string PackageNames { set; get; }

        public string PackageIDs { set; get; }
    }
}
