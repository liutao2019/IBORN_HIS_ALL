using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOC.Fee.Report.InpatientReport.Report
{
    /// <summary>
    /// 贵重材料审核报表
    /// </summary>
    public partial class ucPreciousMaterialReport : FS.SOC.Fee.Report.Base.ucReportBase
    {
        #region 属性
        /// <summary>
        /// 贵重材料最小费用
        /// </summary>`
        string strPreciousMaterialFeeCode = string.Empty;
        /// <summary>
        /// 贵重材料最小费用
        /// </summary>
        [Category("报表设置"), Description("贵重材料最小费用，多个以 | 分开")]
        public string StrPreciousMaterialFeeCode
        {
            get
            {
                if (string.IsNullOrEmpty(strPreciousMaterialFeeCode))
                {
                    return string.Empty;
                }
                else
                {
                    string strTemp = strPreciousMaterialFeeCode.Replace("'", "").Replace(",", "|").Replace(" ", "");
                    return strTemp;
                }
            }
            set
            {
                strPreciousMaterialFeeCode = value;
                if (!string.IsNullOrEmpty(strPreciousMaterialFeeCode))
                {
                    string[] strArr = strPreciousMaterialFeeCode.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    strPreciousMaterialFeeCode = "";
                    foreach (string str in strArr)
                    {
                        if (string.IsNullOrEmpty(str))
                        {
                            continue;
                        }
                        strPreciousMaterialFeeCode += "'" + str + "', ";
                    }
                    strPreciousMaterialFeeCode = strPreciousMaterialFeeCode.Trim().Trim(new char[] { ',' });
                }
            }
        }


        #endregion

        public ucPreciousMaterialReport()
        {
            InitializeComponent();
        }

        protected override object[] GetParams()
        {
            List<object> lstParams = new List<object>();
            lstParams.AddRange(base.GetParams());
            lstParams.Add(strPreciousMaterialFeeCode);

            return lstParams.ToArray();
        }
    }
}
