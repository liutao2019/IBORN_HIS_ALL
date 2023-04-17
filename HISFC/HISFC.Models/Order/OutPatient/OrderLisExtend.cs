using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.Models.Order.OutPatient
{
    /// <summary>
    /// [功能描述: 门诊医嘱扩展表]<br></br>
    /// [创 建 者: ]<br></br>
    /// [创建时间: ]<br></br>{97B9173B-834D-49a1-936D-E4D04F98E4BA}
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    [Serializable]
    public class OrderLisExtend : FS.FrameWork.Models.NeuObject
    {
        public OrderLisExtend()
        {
        }

        #region 变量

        /// <summary>
        /// 门诊流水号
        /// </summary>
        private string clinicCode;

        /// <summary>
        /// 门诊流水号
        /// </summary>
        public string ClinicCode
        {
            get { return clinicCode; }
            set { clinicCode = value; }
        }

        /// <summary>
        /// 医嘱流水号
        /// </summary>
        private string moOrder;

        /// <summary>
        /// 医嘱流水号
        /// </summary>
        public string MoOrder
        {
            get { return moOrder; }
            set { moOrder = value; }
        }

        /// <summary>
        /// 适应症标记
        /// </summary>
        private string indications;

        /// <summary>
        /// 适应症标记
        /// </summary>
        public string Indications
        {
            get { return indications; }
            set { indications = value; }
        }

        /// <summary>
        /// 扩展1
        /// </summary>
        private string extend1 = string.Empty;

        /// <summary>
        /// 扩展1
        /// </summary>
        public string Extend1
        {
            get { return extend1; }
            set { extend1 = value; }
        }

        /// <summary>
        /// 扩展2
        /// </summary>
        private string extend2 = string.Empty;

        /// <summary>
        /// 扩展2
        /// </summary>
        public string Extend2
        {
            get { return extend2; }
            set { extend2 = value; }
        }

        /// <summary>
        /// 扩展3
        /// </summary>
        private string extend3 = string.Empty;

        /// <summary>
        /// 扩展3
        /// </summary>
        public string Extend3
        {
            get { return extend3; }
            set { extend3 = value; }
        }

        /// <summary>
        /// 扩展4
        /// </summary>
        private string extend4 = string.Empty;

        /// <summary>
        /// 扩展4
        /// </summary>
        public string Extend4
        {
            get { return extend4; }
            set { extend4 = value; }
        }

        /// <summary>
        /// 扩展5
        /// </summary>
        private string extend5 = string.Empty;

        /// <summary>
        /// 扩展4
        /// </summary>
        public string Extend5
        {
            get { return extend5; }
            set { extend5 = value; }
        }


        /// <summary>
        /// 操作人员
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// 操作人员
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get { return oper; }
            set { oper = value; }
        }

        #endregion

        public new OrderLisExtend Clone()
        {
            OrderLisExtend orderExt = base.Clone() as OrderLisExtend;
            orderExt.oper = this.oper.Clone();
            return orderExt;
        }
    }
}
