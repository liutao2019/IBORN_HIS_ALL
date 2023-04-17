using System;
using System.Collections;

namespace FS.SOC.HISFC.BizLogic.DCP
{
    /// <summary>
    /// CancerExt 的摘要说明。
    /// 肿瘤报卡扩充表业务
    /// </summary>
    public class CancerExt : FS.FrameWork.Management.Database
    {
        public CancerExt()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }



        private FS.HISFC.DCP.Object.CancerAddExt cancerAddExt;


        /// <summary>
        /// 获取肿瘤报告卡的参数
        /// </summary>
        /// <param name="cancerAddress">实体</param>
        /// <returns>参数数组</returns>
        private string[] myGetCancerAddExtReportParm(FS.HISFC.DCP.Object.CancerAddExt cancerAddExt)
        {
            string[] strParm = {   cancerAddExt.Report_No,
								   cancerAddExt.Class_Code,
								   cancerAddExt.Class_Name,
								   cancerAddExt.Item_Code,
								   cancerAddExt.Item_Name,
								   cancerAddExt.Item_Demo  								   
							   };
            return strParm;
        }
        /// <summary>
        /// 插入 肿瘤报卡
        /// </summary>
        public int InsertCancerExt(FS.HISFC.DCP.Object.CancerAddExt cancerAddExt)
        {
            string strSQL = "";
            if (this.Sql.GetSql("DCP.CancerExt.Insert", ref strSQL) == -1)
            {
                this.Err = "没有找到DCP.CancerExt.Insert字段";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetCancerAddExtReportParm(cancerAddExt);  //取参数列表
                strSQL = string.Format(strSQL, strParm);    //替换SQL语句中的参数
            }
            catch (Exception ex)
            {
                this.Err = "格式化sql时候出错" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);


        }



        /// <summary>
        /// 更新报告卡
        /// </summary>
        /// <param name="cancerAdd">报卡实体</param>
        /// <returns></returns>
        public int UpdateConcerAddExtReport(FS.HISFC.DCP.Object.CancerAddExt cancerAddExt)
        {
            string strSQL = "";
            if (this.Sql.GetSql("DCP.CancerExt.Update", ref strSQL) == -1)
            {
                this.Err = "没有找到DCP.CancerExt.Update字段";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetCancerAddExtReportParm(cancerAddExt);//取参数列表
                strSQL = string.Format(strSQL, strParm);    //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化sql时候出错" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 肿瘤报告卡删除
        /// </summary>
        /// <param name="ReportNO">编号</param>
        /// <returns></returns>
        public int DeleteCancerAddExtReport(string ReportNO)
        {
            string strSQL = "";
            if (this.Sql.GetSql("DCP.CancerExt.DeleteByNo", ref strSQL) == -1)
            {
                this.Err = "没有找到DCP.CancerExt.DeleteByNo字段";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, ReportNO);    //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化sql时候出错" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }



        /// <summary>
        /// 获取报卡
        /// </summary>
        /// <param name="strSQL">sql语句</param>
        /// <returns>报卡实体数组</returns>
        public ArrayList myGetCancerAddExtReport(string ReportNO)
        {
            ArrayList al = new ArrayList();
            //FS.HISFC.DCP.Object.CancerAddExt  cancerAddExt;
            string strSQL = "";
            if (this.Sql.GetSql("DCP.CancerExt.Query", ref strSQL) == -1)
            {
                this.Err = "没有找到DCP.CancerExt.Query字段";
                return null;
            }
            string strSQLWhere = "";
            if (this.Sql.GetSql("DCP.CancerExt.Where", ref strSQLWhere) == -1)
            {
                this.Err = "没有找到DCP.CancerExt.Where字段";
                return null;
            }


            try
            {
                strSQLWhere = string.Format(strSQLWhere, ReportNO);    //替换SQL语句中的参数
            }
            catch (Exception ex)
            {
                this.Err = "格式化strSQLWhere时候出错" + ex.Message;
                this.WriteErr();
                return null;
            }


            this.ProgressBarText = "正在检索肿瘤报卡扩展报告卡信息...";
            this.ProgressBarValue = 0;
            this.ExecQuery(strSQL + strSQLWhere);

            try
            {
                while (this.Reader.Read())
                {
                    cancerAddExt = new FS.HISFC.DCP.Object.CancerAddExt();
                    cancerAddExt.Report_No = this.Reader[0].ToString();
                    cancerAddExt.Class_Code = this.Reader[1].ToString();
                    cancerAddExt.Class_Name = this.Reader[2].ToString();
                    cancerAddExt.Item_Code = this.Reader[3].ToString();
                    cancerAddExt.Item_Name = this.Reader[4].ToString();
                    cancerAddExt.Item_Demo = this.Reader[5].ToString();
                    al.Add(cancerAddExt);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获取肿瘤扩展报告卡信息时取值赋值到实体对象时出错" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }


            this.ProgressBarValue = -1;
            return al;
        }

    }
}
