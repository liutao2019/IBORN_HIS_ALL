using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Report.Base
{
    /// <summary>
    /// [功能描述: FS SOC 封装 Microsoft ReportViewer]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-08]<br></br>
    /// </summary>
    public partial class ucReportView : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        
        public ucReportView()
        {
            InitializeComponent();

        }

      
        #region 属性

        private string reportEmbeddedResource = "";

        /// <summary>
        /// 报表资源rdlc文件
        /// </summary>
        [Description("报表资源rdlc文件"), Category("A.报表设置"), Browsable(true)]
        public string ReportEmbeddedResource
        {
            get
            {
                return this.reportEmbeddedResource;
            }
            set
            {
                this.reportEmbeddedResource = value;
            }
        }

        private string dataSetName = "";

        /// <summary>
        /// 报表资源数据集
        /// </summary>
        [Description("报表资源数据集"), Category("A.报表设置"), Browsable(true)]
        public string DataSetName
        {
            get { return dataSetName; }
            set { dataSetName = value; }
        }

        private string curDllName = "FS.SOC.HISFC.Components.Report.dll";

        /// <summary>
        /// 报表资源dll名称
        /// </summary>
        [Description("报表资源dll名称"), Category("A.报表设置"), Browsable(true)]
        public string DllName
        {
            get { return curDllName; }
            set { curDllName = value; }
        }

        private string curSQLIndex = "";

        /// <summary>
        /// 查询数据使用的sql
        /// </summary>
        [Description("查询数据使用的sql"), Category("A.报表设置"), Browsable(true)]
        public string SQLIndex
        {
            get { return curSQLIndex; }
            set { curSQLIndex = value; }
        }
        #endregion

        #region 方法
        public virtual void Query(Microsoft.Reporting.WinForms.ReportViewer reportViewer, params string[] param)
        {
            ReportView curReportView = new ReportView();
            curReportView.Query(reportViewer, this.ReportEmbeddedResource, this.DataSetName, this.DllName, this.SQLIndex, param);
        }
        #endregion
    }
}
