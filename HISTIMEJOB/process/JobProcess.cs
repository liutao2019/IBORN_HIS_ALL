using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace OADAL
{
    class JobProcess
    {
        /// <summary>
        /// 同步用友供应商到his
        /// </summary>
        public void SynchronizeYongYouCompany()
        {
            try
            {
                //开始匹配his和用友的供应商列表
                DataTable yydt = getYYCompany();
                DataTable hisdt = getHisCompany();

                for (int i = 0; i < hisdt.Rows.Count; i++)
                {
                    string hisCompanyName = hisdt.Rows[i]["WZ_COMPANY_NAME"].ToString();
                    DataRow[] yyRows = yydt.Select("供应商名称 = '" + hisCompanyName + "'");

                    if (yyRows.Length > 0)
                    {
                        //检测到了his没有编码，但是在用友系统里面查到了有编码，所以同步过来
                        string yyCode = yyRows[0]["供应商编码"].ToString();
                        string yyName = yyRows[0]["供应商名称"].ToString();

                        updateHISYYCompanyCode(yyName, yyCode);
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.Write(e.Message);
            }
        }


        /// <summary>
        /// 同步用友科目到his
        /// </summary>
        public void SynchronizeYongYouSubject()
        {
            /***
             * 关于院区
             * his系统内部存储的三个分别是IBORN,BELLAIRE,SDIBORN
             * 用友要以编码对应账套，目前001账套对应广州，002对应广州综合门诊，003对应顺德
             * 
             **/
            try
            {
                synYYToHis("001", "IBORN");
                synYYToHis("002", "BELLAIRE");
                synYYToHis("003", "SDIBORN");
            }
            catch (Exception e)
            {
                LogHelper.Write(e.Message);
            }
        }

        #region 私有方法-同步供应商
        private DataTable getHisCompany()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "select WZ_COMPANY_NAME from yy_com_company where yy_code is null";
                dt = OracleHelper.ExecuteDataTable(sql);
            }
            catch (Exception e)
            {
                LogHelper.Write(e.Message);
            }
            return dt;
        }

        private DataTable getYYCompany()
        {
            DataTable dt = new DataTable();

            try
            {
                GZ.BizLogic.Delivery.Voucher.OAFeeBLL OAbll = new GZ.BizLogic.Delivery.Voucher.OAFeeBLL();

                dt = OAbll.ArrayOfBaseDocSup("001");
                //dt的内部包含了供应商列表，其中包含两个字段，一个是供应商名字一个是供应商编码
                //其中datatable可以用来解析
                /*
                 * dr["供应商编码"] = supCode;
                    dr["供应商名称"] = supName;
                    dt.Rows.Add(dr);
                 * */
            }
            catch (Exception e)
            {
                LogHelper.Write(e.Message);
            }
            return dt;
        }

        private int updateHISYYCompanyCode(string companyName, string yyCompanyCode)
        {
            string updateSql = "update yy_com_company set YY_CODE= '{0}' where WZ_COMPANY_NAME = '{1}'";
            string executeSql = string.Format(updateSql, yyCompanyCode, companyName);

            int i = OracleHelper.ExecuteNonQuery(executeSql);
            return i;
        }
        #endregion

        #region 私有方法-同步用友科目

        private DataTable getYYSubject(string accCode)
        {
            DataTable dt = new DataTable();

            try
            {
                GZ.BizLogic.Delivery.Voucher.OAFeeBLL OAbll = new GZ.BizLogic.Delivery.Voucher.OAFeeBLL();

                //dt = OAbll.getCodeData(accCode);
                dt = OAbll.GetSubject(accCode);
                //dt的内部包含了供应商列表，其中包含两个字段，一个是供应商名字一个是供应商编码
                //其中datatable可以用来解析
                /*
                 * dr["供应商编码"] = supCode;
                    dr["供应商名称"] = supName;
                    dt.Rows.Add(dr);
                 * */
            }
            catch (Exception e)
            {
                LogHelper.Write(e.Message);
            }
            return dt;
        }

        private DataTable getHisSubject(string hospitalId)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "select * from OA_COM_SUBJECT t";
                dt = OracleHelper.ExecuteDataTable(sql);
            }
            catch (Exception e)
            {
                LogHelper.Write(e.Message);
            }
            return dt;
        }

        private int insertHISYYSubject(string code, string name,string hospitalId)
        {
            string insertSql = @"insert into oa_com_subject
                                  (科目编码, 科目名称, hospital)
                                values
                                  ( '{0}', '{1}', '{2}')";
            string executeSql = string.Format(insertSql,code, name, hospitalId);

            int i = OracleHelper.ExecuteNonQuery(executeSql);
            return i;
        }

        private void synYYToHis(string accCode, string hospitalId)
        {
            try
            {
                //开始匹配his和用友的科目
                DataTable yydt = getYYSubject(accCode);
                DataTable hisdt = getHisSubject(hospitalId);

                for (int i = 0; i < hisdt.Rows.Count; i++)
                {
                    string yySubjectCode = yydt.Rows[i]["编码"].ToString();
                    string yySubjectName = yydt.Rows[i]["名称"].ToString();
                    DataRow[] hisRows = hisdt.Select("科目编码 = '" + yySubjectCode + "'");

                    if (hisRows.Length > 0)
                    {
                        //his已经存在这一个用友的编码，所以无需同步
                        //string yyCode = yyRows[0]["供应商编码"].ToString();
                        //string yyName = yyRows[0]["供应商名称"].ToString();

                        //updateHISYYCompanyCode(yyName, yyCode);
                    }
                    else
                    {
                        //his没有这一个科目编码，所以把这个科目同步过来
                        insertHISYYSubject(yySubjectCode, yySubjectName, hospitalId);
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.Write(e.Message);
            }
        }
        #endregion

    }
}
