using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBorn.SI.GuangZhou
{
    /// <summary>
    /// 广州医保接口对接
    /// by 飞扬 2019-09-28
    /// </summary>
    public class MedicalProcess : IBorn.SI.BI.IMedical
    {
        #region 变量

        /// <summary>
        /// 医保对照接口
        /// </summary>
        private IBorn.SI.BI.ICompare iCompare;

        /// <summary>
        /// 医保登记接口
        /// </summary>
        private IBorn.SI.BI.IRADT iRADT;

        /// <summary>
        /// 医保结算接口
        /// </summary>
        private IBorn.SI.BI.IBalance iBalance;

        /// <summary>
        /// 费用上传接口
        /// </summary>
        private IBorn.SI.BI.IUpload iUpload;
        #endregion

        /// <summary>
        /// 医保对照接口
        /// </summary>
        /// <returns></returns>
        public IBorn.SI.BI.ICompare CreateCompare()
        {
            if (this.iCompare == null)
            {
                this.iCompare = new IBorn.SI.GuangZhou.MedicalCompareProcess();
            }
            return this.iCompare;
        }

        /// <summary>
        /// 医保登记接口
        /// </summary>
        /// <returns></returns>
        public IBorn.SI.BI.IRADT CreateRADT()
        {
            if (this.iRADT == null)
            {
                this.iRADT = new IBorn.SI.GuangZhou.MedicalRadtProcess();
            }
            return this.iRADT;
        }

        /// <summary>
        /// 医保结算接口
        /// </summary>
        /// <returns></returns>
        public IBorn.SI.BI.IBalance CreateBalance()
        {
            if (this.iBalance == null)
            {
                this.iBalance = new IBorn.SI.GuangZhou.MedicalBalanceProcess();
            }
            return this.iBalance;
        }


        /// <summary>
        /// 费用上传
        /// </summary>
        /// <returns></returns>
        public IBorn.SI.BI.IUpload CreateUpload()
        {
            if (this.iUpload == null)
            {
                this.iUpload = new IBorn.SI.GuangZhou.MedicalUploadProcess();
            }
            return this.iUpload;
        }
    }
}
