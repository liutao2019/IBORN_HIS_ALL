using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.DCP.Object
{
    /// <summary>
    /// AdditionReport<br></br>
    /// [功能描述: 附卡信息实体]<br></br>
    /// [创 建 者: zj]<br></br>
    /// [创建时间: 2008-8-25]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />    /// </summary>
    [System.Serializable]
    public class AdditionReport: FS.FrameWork.Models.NeuObject
    {
        #region 域变量

        /// <summary>
        /// 报卡实体
        /// </summary>
        private FS.HISFC.DCP.Object.CommonReport report;

        /// <summary>
        /// 患者编号
        /// </summary>
        private string patientNO;

        /// <summary>
        /// 患者姓名
        /// </summary>
        private string patientName;

        /// <summary>
        /// 附卡XML
        /// </summary>
        private string reportXML;

        #endregion

        #region 属性

        /// <summary>
        /// 报卡实体
        /// </summary>
        public FS.HISFC.DCP.Object.CommonReport Report
        {
            get
            {
                return report;
            }
            set
            {
                report = value;
            }
        }

        /// <summary>
        /// 患者编号
        /// </summary>
        public string PatientNO
        {
            get
            {
                return patientNO;
            }
            set
            {
                patientNO = value;
            }
        }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName
        {
            get
            {
                return patientName;
            }
            set
            {
                patientName = value;
            }
        }

        /// <summary>
        /// 附卡XML
        /// </summary>
        public string ReportXML
        {
            get
            {
                return reportXML;
            }
            set
            {
                reportXML = value;
            }
        }

        #endregion

        #region 方法

        public AdditionReport()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
        }

        public void Clone()
        {
            AdditionReport additionReport = base.Clone() as AdditionReport;

            additionReport.Report = this.Report.Clone();
        }

        #endregion
    }
}
