using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// <br>Neusoft.HISFC.Models.HR.SIRefer</br>
    /// <br>[功能描述: 社会保险对照实体类]</br>
    /// <br>[创 建 者: 赵阳]</br>
    /// <br>[创建时间: 2008-07-17]</br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class SIRefer : Neusoft.FrameWork.Models.NeuObject
    {
        #region 字段

        /// <summary>
        /// 年度
        /// </summary>
        private string yearStr = "";

        /// <summary>
        /// 合同种类
        /// </summary>
        private string contractType = "";

        /// <summary>
        /// 保险种类(失业、工伤、养老、医疗等)
        /// </summary>
        private string insuranceType = "";

        /// <summary>
        /// 操作员
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment oper = new Neusoft.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region 属性

        /// <summary>
        /// 年度
        /// </summary>
        public string YearStr
        {
            get
            {
                return yearStr;
            }
            set
            {
                yearStr = value;
            }
        }

        /// <summary>
        /// 合同种类
        /// </summary>
        public string ContractType
        {
            get
            {
                return contractType;
            }
            set
            {
                contractType = value;
            }
        }

        /// <summary>
        /// 保险种类(失业、工伤、养老、医疗等)
        /// </summary>
        public string InsuranceType
        {
            get
            {
                return insuranceType;
            }
            set
            {
                insuranceType = value;
            }
        }

        /// <summary>
        /// 操作员
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment Oper
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
        public new SIRefer Clone()
        {
            SIRefer siRefer = base.Clone() as SIRefer;

            siRefer.Oper = this.Oper.Clone();

            return siRefer;
        }
        #endregion
    }
}
