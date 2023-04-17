using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.Models
{
    /// <summary>
    /// [功能描述: 黑名单管理补充实体]<br></br>
    /// [创 建 者: zhaorong]<br></br>
    /// [创建时间: 2013-7]<br></br>
    /// </summary>
    [System.ComponentModel.DisplayName("黑名单管理信息")]
    [System.Serializable]
    public class BlackList:FS.HISFC.Models.BlackList.PatientBlackListDetail
    {
        #region 变量
        /// <summary>
        /// 序号
        /// </summary>
        private string blackId = String.Empty;
        /// <summary>
        /// 名称
        /// </summary>
        private string name;
        /// <summary>
        /// 合同单位编码
        /// </summary>
        private string pactCode;
        /// <summary>
        /// 合同单位名称
        /// </summary>
        private string pactName;
        /// <summary>
        /// 医疗证号
        /// </summary>
        private string mcordNo;
        /// <summary>
        /// 种类 0 单位 1 个人
        /// </summary>
        private string kind;
        /// <summary>
        /// 种类 0 单位 1 个人
        /// </summary>
        private string kindName;
        /// <summary>
        /// 身份证号
        /// </summary>
        private string idDno;
        /// <summary>
        /// 性别
        /// </summary>
        private string sexCode;
        /// <summary>
        /// 性别
        /// </summary>
        private string sexName;
        /// <summary>
        /// 出生日期
        /// </summary>
        private DateTime birthday;
        /// <summary>
        /// 定点医院1
        /// </summary>
        private string ddyy1;
        /// <summary>
        /// 定点医院1名称
        /// </summary>
        private string d1Name;
        /// <summary>
        /// 定点医院2
        /// </summary>
        private string ddyy2;
        /// <summary>
        /// 定点医院2名称
        /// </summary>
        private string d2Name;
        /// <summary>
        /// 定点医院3
        /// </summary>
        private string ddyy3;
        /// <summary>
        /// 定点医院3名称
        /// </summary>
        private string d3Name;
        /// <summary>
        /// 门诊比例
        /// </summary>
        private decimal clinicRate;
        /// <summary>
        /// 住院比例
        /// </summary>
        private decimal inpatientRate;
        /// <summary>
        /// 起始日期（有效期）
        /// </summary>
        private DateTime beginDate;
        /// <summary>
        /// 结束日期（有效期）
        /// </summary>
        private DateTime endDate;
        /// <summary>
        /// 日限额
        /// </summary>
        private decimal dayLimit;
        /// <summary>
        /// 月限额
        /// </summary>
        private decimal monthLimit;
        /// <summary>
        /// 年限额
        /// </summary>
        private decimal yearLimit;
        /// <summary>
        /// 一次限额
        /// </summary>
        private decimal onceLimit;
        /// <summary>
        /// 床位上限
        /// </summary>
        private decimal bedLimit;
        /// <summary>
        /// 空调上限
        /// </summary>
        private decimal airLimit;
        /// <summary>
        /// 操作员
        /// </summary>
        private string operCode;
        /// <summary>
        /// 操作员名称
        /// </summary>
        private string operName;
        /// <summary>
        /// 操作时间
        /// </summary>
        private DateTime operDate;
        /// <summary>
        /// 门诊启用标记
        /// </summary>
        private string clinicFlag;
        /// <summary>
        /// 住院启用标记
        /// </summary>
        private string inpatientFlag;
        /// <summary>
        /// 单位编码
        /// </summary>
        private string unitCode;
        #endregion

        #region 属性
        /// <summary>
        /// 序号
        /// </summary>
        public string BlackId
        {
            get
            {
                return blackId;
            }
            set
            {
                blackId = value;
            }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        /// <summary>
        /// 合同单位编码
        /// </summary>
        public string PactCode
        {
            get
            {
                return pactCode;
            }
            set
            {
                pactCode = value;
            }
        }
        /// <summary>
        /// 合同单位名称
        /// </summary>
        public string PactName
        {
            get
            {
                return pactName;
            }
            set
            {
                pactName = value;
            }
        }
        /// <summary>
        /// 医疗证号
        /// </summary>
        public string McordNo
        {
            get
            {
                return mcordNo;
            }
            set
            {
                mcordNo = value;
            }
        }
        /// <summary>
        /// 种类 0 单位 1 个人
        /// </summary>
        public string Kind
        {
            get
            {
                return kind;
            }
            set
            {
                kind = value;
            }
        }
        /// <summary>
        /// 种类 0 单位 1 个人
        /// </summary>
        public string KindName
        {
            get
            {
                return kindName;
            }
            set
            {
                kindName = value;
            }
        }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdDno
        {
            get
            {
                return idDno;
            }
            set
            {
                idDno = value;
            }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string SexCode
        {
            get
            {
                return sexCode;
            }
            set
            {
                sexCode = value;
            }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string SexName
        {
            get
            {
                return sexName;
            }
            set
            {
                sexName = value;
            }
        }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday
        {
            get
            {
                return birthday;
            }
            set
            {
                birthday = value;
            }
        }
        /// <summary>
        /// 定点医院1
        /// </summary>
        public string Ddyy1
        {
            get
            {
                return ddyy1;
            }
            set
            {
                ddyy1 = value;
            }
        }
        /// <summary>
        /// 定点医院1名称
        /// </summary>
        public string D1Name
        {
            get
            {
                return d1Name;
            }
            set
            {
                d1Name = value;
            }
        }
        /// <summary>
        /// 定点医院2
        /// </summary>
        public string Ddyy2
        {
            get
            {
                return ddyy2;
            }
            set
            {
                ddyy2 = value;
            }
        }
        /// <summary>
        /// 定点医院2名称
        /// </summary>
        public string D2Name
        {
            get
            {
                return d2Name;
            }
            set
            {
                d2Name = value;
            }
        }
        /// <summary>
        /// 定点医院3
        /// </summary>
        public string Ddyy3
        {
            get
            {
                return ddyy3;
            }
            set
            {
                ddyy3 = value;
            }
        }
        /// <summary>
        /// 定点医院3名称
        /// </summary>
        public string D3Name
        {
            get
            {
                return d3Name;
            }
            set
            {
                d3Name = value;
            }
        }
        /// <summary>
        /// 门诊比例
        /// </summary>
        public decimal ClinicRate
        {
            get
            {
                return clinicRate;
            }
            set
            {
                clinicRate = value;
            }
        }
        /// <summary>
        /// 住院比例
        /// </summary>
        public decimal InpatientRate
        {
            get
            {
                return inpatientRate;
            }
            set
            {
                inpatientRate = value;
            }
        }
        /// <summary>
        /// 起始日期（有效期）
        /// </summary>
        public DateTime BeginDate
        {
            get
            {
                return beginDate;
            }
            set
            {
                beginDate = value;
            }
        }
        /// <summary>
        /// 结束日期（有效期）
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
            }
        }
        /// <summary>
        /// 日限额
        /// </summary>
        public decimal DayLimit
        {
            get
            {
                return dayLimit;
            }
            set
            {
                dayLimit = value;
            }
        }
        /// <summary>
        /// 月限额
        /// </summary>
        public decimal MonthLimit
        {
            get
            {
                return monthLimit;
            }
            set
            {
                monthLimit = value;
            }
        }
        /// <summary>
        /// 年限额
        /// </summary>
        public decimal YearLimit
        {
            get
            {
                return yearLimit;
            }
            set
            {
                yearLimit = value;
            }
        }
        /// <summary>
        /// 一次限额
        /// </summary>
        public decimal OnceLimit
        {
            get
            {
                return onceLimit;
            }
            set
            {
                onceLimit = value;
            }
        }
        /// <summary>
        /// 床位上限
        /// </summary>
        public decimal BedLimit
        {
            get
            {
                return bedLimit;
            }
            set
            {
                bedLimit = value;
            }
        }
        /// <summary>
        /// 空调上限
        /// </summary>
        public decimal AirLimit
        {
            get
            {
                return airLimit;
            }
            set
            {
                airLimit = value;
            }
        }
        /// <summary>
        /// 操作员
        /// </summary>
        public string OperCode
        {
            get
            {
                return operCode;
            }
            set
            {
                operCode = value;
            }
        }
        /// <summary>
        /// 操作员名称
        /// </summary>
        public string OperName
        {
            get
            {
                return operName;
            }
            set
            {
                operName = value;
            }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperDate
        {
            get
            {
                return operDate;
            }
            set
            {
                operDate = value;
            }
        }
        /// <summary>
        /// 门诊启用标记
        /// </summary>
        public string ClinicFlag
        {
            get
            {
                return clinicFlag;
            }
            set
            {
                clinicFlag = value;
            }
        }
        /// <summary>
        /// 住院启用标记
        /// </summary>
        public string InpatientFlag
        {
            get
            {
                return inpatientFlag;
            }
            set
            {
                inpatientFlag = value;
            }
        }
        /// <summary>
        /// 单位编码
        /// </summary>
        public string UnitCode
        {
            get
            {
                return unitCode;
            }
            set
            {
                unitCode = value;
            }
        }
        #endregion

        #region 方法
        public new BlackList Clone()
        {
            BlackList blackList = base.Clone() as BlackList;
            return blackList;
        }
        #endregion
    }
}
