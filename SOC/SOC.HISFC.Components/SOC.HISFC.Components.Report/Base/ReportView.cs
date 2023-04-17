using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Report.Base
{
    /// <summary>
    /// [功能描述: FS SOC 封装 Microsoft ReportViewer]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-08]<br></br>
    /// </summary>
    public class ReportView
    {
        //数据库管理类：使用基类的数据库连接
        public FS.FrameWork.Management.DataBaseManger dataBaseManger = new FS.FrameWork.Management.DataBaseManger();

        #region 方法
        public virtual int Query(Microsoft.Reporting.WinForms.ReportViewer reportViewer, string reportEmbeddedResource, string dataSetName,string DllName,string SQLIndex, params string[] param)
        {
            if (reportViewer == null)
            {
                Function.ShowMessage("ReportViewer为空，请与系统管理员联系！", MessageBoxIcon.Information);
                return -1;
            }

            if (string.IsNullOrEmpty(reportEmbeddedResource))
            {
                Function.ShowMessage("报表资源为空，请与系统管理员联系！", MessageBoxIcon.Information);
                return -1;
            }
            else 
            {
                reportViewer.LocalReport.ReportEmbeddedResource = reportEmbeddedResource;
            }

            if (string.IsNullOrEmpty(dataSetName))
            {
                Function.ShowMessage("报表资源数据集为空，请与系统管理员联系！", MessageBoxIcon.Information);
                return -1;
            }
            if (string.IsNullOrEmpty(SQLIndex))
            {
                Function.ShowMessage("查询数据的sql索引为空，请与系统管理员联系！", MessageBoxIcon.Information);
                return -1;
            }

            //数据集，限定一个
            System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(DllName);
            if (assembly == null)
            {
                Function.ShowMessage("报表设置的DllName不正确，请与系统管理员联系！", MessageBoxIcon.Information);
                return -1;
            }
            object obDataSet = assembly.CreateInstance(dataSetName, true);
            if (!(obDataSet is DataSet))
            {
                Function.ShowMessage("报表设置的DataSetName不正确，请与系统管理员联系！", MessageBoxIcon.Information);
                return -1;
            }
            DataSet curDataSet = (DataSet)obDataSet;


            //报表的数据看连接（获取sql的是HIS的连接）
            //{014680EC-6381-408b-98FB-A549DAA49B82}
            //IDbConnection curConnection = FS.FrameWork.Server.ConnectionPool.GetConnection("FS.SOC.HISFC.Components.Report");
            IDbConnection curConnection = FS.FrameWork.Server.PoolingNew.CreateInstance().GetConnection("FS.SOC.HISFC.Components.Report");
            if (curConnection == null)
            {
                Function.ShowMessage("数据连接为空，请与系统管理员联系！", MessageBoxIcon.Information);
                return -1;
            }
            if (curConnection.State != ConnectionState.Open)
            {
                curConnection.Open();
            }

            if (curConnection is System.Data.OracleClient.OracleConnection)
            {
                //数据集中的表可以是多个，和sql对应

                int index = 0;
                foreach (string curSQLIndex in SQLIndex.Split(';', ' ', '|'))
                {
                    string curSQL = "";
                    if (dataBaseManger.Sql.GetSql(curSQLIndex, ref curSQL) == -1)
                    {
                        Function.ShowMessage("获取查询数据的sql出错，请与系统管理员联系并报告错误：" + dataBaseManger.Err, MessageBoxIcon.Information);
                        return -1;
                    }
                    curSQL = string.Format(curSQL, param);

                    System.Data.OracleClient.OracleCommand command = new System.Data.OracleClient.OracleCommand();
                    command.CommandType = CommandType.Text;
                    command.Connection = curConnection as System.Data.OracleClient.OracleConnection;
                    command.CommandText = curSQL;
                    command.Parameters.Clear();
                    System.Data.OracleClient.OracleDataReader oracleDataReader = command.ExecuteReader();
                    curDataSet.Tables[index].Load(oracleDataReader);

                    oracleDataReader.Close();

                    index++;
                }
                if (curConnection.State == ConnectionState.Open)
                {
                    curConnection.Close();
                }
            }
            else
            {
                if (curConnection.State == ConnectionState.Open)
                {
                    curConnection.Close();
                }
                Function.ShowMessage("系统不支持当前数据库类型，请与系统管理员联系！", MessageBoxIcon.Information);
                return -1;
            }

            for (int index = 0; index < curDataSet.Tables.Count; index++)
            {
                //报表数据源
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource = new Microsoft.Reporting.WinForms.ReportDataSource();
                //名称：string
                reportDataSource.Name = curDataSet.DataSetName + "_" + curDataSet.Tables[index].TableName;

                //数据：System.Data.DataTable
                reportDataSource.Value = curDataSet.Tables[index];
                reportViewer.LocalReport.DataSources.Add(reportDataSource);
            }

            //展示报表
            reportViewer.RefreshReport();

            return 1;
        }
        #endregion

    }
}
