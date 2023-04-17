using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.Models.MedicalPackage.Fee
{
    /// <summary>
    /// {727B4075-258A-4F11-A757-D8F0AEF08380}套餐核销
    /// </summary>
    public  class PackageWriteOff
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string Card_NO { set; get; }

        /// <summary>
        /// 核销收费发票号
        /// </summary>
        public string InvoiceNO { set; get; }

        /// <summary>
        /// 购买套餐流水哈
        /// </summary>
        public string Clinic_Code { set; get; }

        /// <summary>
        /// 套餐编号
        /// </summary>
        public string PackageID { set; get; }

        /// <summary>
        /// 核销人工号
        /// </summary>
        public string Oper_Code { set; get; }

        /// <summary>
        /// 核销日期
        /// </summary>
        public DateTime Oper_Date { set; get; }

        /// <summary>
        /// 核销套餐金额
        /// </summary>
        public decimal RealCost { set; get; }
    }
}
