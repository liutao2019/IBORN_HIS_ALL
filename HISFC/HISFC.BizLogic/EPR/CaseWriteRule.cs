using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.BizLogic.EPR
{   
    /// <summary>
    /// CaseWriteRule<br></br>
    /// [��������: ������д�淶]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-10]<br></br>
    /// <�޸ļ�¼
    /// 
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>   
    public class CaseWriteRule : FS.FrameWork.Management.Database
    {
        public CaseWriteRule()
        {

        }
        /// <summary>
        /// �����д�淶
        /// </summary>
        /// <returns>1 �ɹ���-1 ʧ��</returns>
        public int InsertRule(FS.HISFC.Models.EPR.CaseWriteRule rule)
        {
            string sqlIndex = "EPR.CaseWriteRule.InsertRule";
            string strSql = string.Empty;
            try
            {
                if (this.Sql.GetSql(sqlIndex, ref strSql) == -1)
                {
                    return -1;
                }
                strSql = string.Format(strSql,
                                       rule.RuleCode,
                                       rule.RuleName,
                                       rule.DeptName,
                                       rule.DeptCode,
                                       rule.Descript,
                                       rule.Sort,
                                       rule.Memo,
                                       rule.RuleLink,
                                       rule.ParentCode);
                if (this.ExecNoQuery(strSql) == -1)
                {
                    return -1;
                }

                if (!string.IsNullOrEmpty(rule.RuleData))
                {
                    sqlIndex = "EPR.CaseWriteRule.UpdateBlob";
                    strSql = string.Empty;
                    if (this.Sql.GetSql(sqlIndex, ref strSql) == -1)
                    {
                        return -1;
                    }
                    strSql = string.Format(strSql, rule.RuleCode);

                    if (this.InputBlob(strSql, System.Text.Encoding.GetEncoding("GB2312").GetBytes(rule.RuleData)) == -1)
                    {
                        DeleteRule(rule, false);
                        return -1;
                    }
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message + "\r\n" + e.StackTrace;
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// �޸���д�淶
        /// </summary>
        /// <returns>1 �ɹ���-1 ʧ��</returns>
        public int ModifyRule(FS.HISFC.Models.EPR.CaseWriteRule rule)
        {
            string sqlIndex = "EPR.CaseWriteRule.ModifyRule";
            string strSql = string.Empty;
            try
            {
                if (this.Sql.GetSql(sqlIndex, ref strSql) == -1)
                {
                    return -1;
                }
                strSql = string.Format(strSql,
                                       rule.RuleCode,
                                       rule.RuleName,
                                       rule.DeptName,
                                       rule.DeptCode,
                                       rule.Descript,
                                       rule.Sort,
                                       rule.Memo,
                                       rule.RuleLink,
                                       rule.ParentCode);
                if (this.ExecNoQuery(strSql) == -1)
                {
                    return -1;
                }
                if (!string.IsNullOrEmpty(rule.RuleData))
                {
                    sqlIndex = "EPR.CaseWriteRule.UpdateBlob";
                    strSql = string.Empty;
                    if (this.Sql.GetSql(sqlIndex, ref strSql) == -1)
                    {
                        return -1;
                    }
                    strSql = string.Format(strSql, rule.RuleCode);

                    if (this.InputBlob(strSql, System.Text.Encoding.GetEncoding("GB2312").GetBytes(rule.RuleData)) == -1)
                    {
                        return -1;
                    }
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message + "\r\n" + e.StackTrace;
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// ɾ����д�淶
        /// </summary>
        /// <returns>1 �ɹ���-1 ʧ��</returns>
        public int DeleteRule(FS.HISFC.Models.EPR.CaseWriteRule rule,bool deleteChildren)
        {
            string sqlIndex = "EPR.CaseWriteRule.DeleteRule";
            string strSql = string.Empty;
            try
            {
                if (this.Sql.GetSql(sqlIndex, ref strSql) == -1)
                {
                    return -1;
                }
                strSql = string.Format(strSql, (rule.RuleCode == "0000" ? "000" : rule.RuleCode) + (deleteChildren ? "%" : ""));

                if (this.ExecNoQuery(strSql) == -1)
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message + "\r\n" + e.StackTrace;
                return -1;
            }

            return 1;
        }
        /// <summary>
        /// ������ק���ӽڵ��rule_code��parent_code
        /// </summary>
        /// <param name="cwr"></param>
        /// <param name="newParentCode"></param>
        /// <returns></returns>
        public int ModifyForDrag(FS.HISFC.Models.EPR.CaseWriteRule cwr,string newParentCode)
        {

            string sqlIndex = "EPR.CaseWriteRule.UpdateForDrag";
            string strSql = string.Empty;
            try
            {
                if (this.Sql.GetSql(sqlIndex, ref strSql) == -1)
                {
                    return -1;
                }
                strSql = string.Format(strSql, cwr.RuleCode, newParentCode);
                if (this.ExecNoQuery(strSql) == -1)
                {
                    return -1;
                }
                if (this.DeleteRule(cwr, false) == -1)
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message + "\r\n" + e.StackTrace;
                return -1;
            }

            return 1;
        }
        /// <summary>
        /// ��ȡ�����淶����
        /// </summary>
        /// <returns></returns>
        public FS.HISFC.Models.EPR.CaseWriteRule GetRule(string ruleCode)
        {
            string sqlIndex = "EPR.CaseWriteRule.QueryRule";
            string sqlWhereIndex = "EPR.CaseWriteRule.QueryRule.Where";
            string strSql = string.Empty;
            string strSqlWhere = string.Empty;

            FS.HISFC.Models.EPR.CaseWriteRule rule = new FS.HISFC.Models.EPR.CaseWriteRule();
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {
                if (this.Sql.GetSql(sqlIndex, ref strSql) == -1)
                {
                    return null;
                }
                if (this.Sql.GetSql(sqlWhereIndex, ref strSqlWhere) == -1)
                {
                    return null;
                }
                strSql += string.Format(strSqlWhere, ruleCode);
                if (this.ExecQuery(strSql, ref ds) == -1)
                {
                    return null;
                }
                byte[] data;
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    rule.RuleCode = row["rule_code"].ToString();
                    rule.RuleName = row["rule_name"].ToString();
                    rule.DeptName = row["dept_name"].ToString();
                    rule.DeptCode = row["his_dept_code"].ToString();
                    rule.Descript = row["brifsign"].ToString();
                    rule.Sort = row["sort"].ToString();
                    rule.Memo = row["memo"].ToString();
                    rule.RuleLink = row["rule_link"].ToString();
                    rule.ParentCode = row["parent_code"].ToString();
                    //rule.RuleData = row["data"].ToString();
                    break;
                }

                strSql = string.Empty;
                sqlIndex = "EPR.CaseWriteRule.QueryBlob";
                if (this.Sql.GetSql(sqlIndex, ref strSql) == -1)
                {
                    return null;
                }
                strSql = string.Format(strSql, ruleCode);
                data = this.OutputBlob(strSql);
                rule.RuleData = (data == null ? (string.Empty) : System.Text.Encoding.GetEncoding("GB2312").GetString(data));
            }
            catch (Exception e)
            {
                this.Err = e.Message + "\r\n" + e.StackTrace;
                return null;
            }
            return rule;
        }
        /// <summary>
        /// �õ�����Ŀ¼�б�
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryAllCatalog()
        {
            ArrayList alCatalog = new ArrayList();
            string sqlIndex = "EPR.CaseWriteRule.QueryCatalog";
            string sqlOrder = string.Empty;

            try
            {
                if (this.Sql.GetSql("EPR.CaseWriteRule.QueryCatalog.OrderBy", ref sqlOrder) == -1)
                {
                    return null;
                }
                FS.HISFC.Models.EPR.CaseWriteRule rule;
                System.Data.DataSet ds = new System.Data.DataSet();
                string strSql = string.Empty;
                if (this.Sql.GetSql(sqlIndex, ref strSql) == -1)
                {
                    return null;
                }
                strSql += sqlOrder;
                if (this.ExecQuery(strSql, ref ds) == -1)
                {
                    return null;
                }
                strSql = string.Empty;
                sqlIndex = "EPR.CaseWriteRule.QueryBlob";
                if (this.Sql.GetSql(sqlIndex, ref strSql) == -1)
                {
                    return null;
                }
                byte[] data;
                string sql = strSql;
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    rule = new FS.HISFC.Models.EPR.CaseWriteRule();
                    rule.RuleCode = row["rule_code"].ToString();
                    rule.RuleName = row["rule_name"].ToString();
                    rule.DeptName = row["dept_name"].ToString();
                    rule.DeptCode = row["his_dept_code"].ToString();
                    rule.Descript = row["brifsign"].ToString();
                    rule.Sort = row["sort"].ToString();
                    rule.Memo = row["memo"].ToString();
                    rule.RuleLink = row["rule_link"].ToString();
                    rule.ParentCode = row["parent_code"].ToString();

                    strSql = string.Format(sql, rule.RuleCode);
                    data = this.OutputBlob(strSql);
                    rule.RuleData = (data == null ? (string.Empty) : System.Text.Encoding.GetEncoding("GB2312").GetString(data));

                    alCatalog.Add(rule);
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message + "\r\n" + e.StackTrace;
                return null;
            }
            return alCatalog;
        }
        /// <summary>
        /// �õ�����Ŀ¼�б�
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryCatalogByDeptCode(string deptCode)
        {            
            ArrayList alCatalog = new ArrayList();
            string strSqlWhere = string.Empty;
            string sqlOrder = string.Empty;
            string sqlIndex = "EPR.CaseWriteRule.QueryCatalog";
            string sqlWhereIndex = "EPR.CaseWriteRule.QueryCatalog.Where2";

            try
            {
                if (this.Sql.GetSql("EPR.CaseWriteRule.QueryCatalog.OrderBy", ref sqlOrder) == -1)
                {
                    return null;
                }
                
                FS.HISFC.Models.EPR.CaseWriteRule rule;
                System.Data.DataSet ds = new System.Data.DataSet();
                string strSql = string.Empty;
                if (this.Sql.GetSql(sqlIndex, ref strSql) == -1)
                {
                    return null;
                }
                
                if (this.Sql.GetSql(sqlWhereIndex, ref strSqlWhere) == -1)
                {
                    return null;
                }
                strSql += string.Format(strSqlWhere, deptCode);
                strSql += sqlOrder;
                if (this.ExecQuery(strSql, ref ds) == -1)
                {
                    return null;
                }
                strSql = string.Empty;
                sqlIndex = "EPR.CaseWriteRule.QueryBlob";
                if (this.Sql.GetSql(sqlIndex, ref strSql) == -1)
                {
                    return null;
                }
                byte[] data;
                string sql = strSql;
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    rule = new FS.HISFC.Models.EPR.CaseWriteRule();
                    rule.RuleCode = row["rule_code"].ToString();
                    rule.RuleName = row["rule_name"].ToString();
                    rule.DeptName = row["dept_name"].ToString();
                    rule.DeptCode = row["his_dept_code"].ToString();
                    rule.Descript = row["brifsign"].ToString();
                    rule.Sort = row["sort"].ToString();
                    rule.Memo = row["memo"].ToString();
                    rule.RuleLink = row["rule_link"].ToString();
                    rule.ParentCode = row["parent_code"].ToString();

                    strSql = string.Format(sql, rule.RuleCode);
                    data = this.OutputBlob(strSql);
                    rule.RuleData = (data == null ? (string.Empty) : System.Text.Encoding.GetEncoding("GB2312").GetString(data));

                    alCatalog.Add(rule);
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message + "\r\n" + e.StackTrace;
                return null;
            }
            //this.Reader.Close();
            return alCatalog;
        }
        /// <summary>
        /// ͨ������õ�ĳ��Ŀ¼
        /// </summary>
        /// <returns></returns>
        public FS.HISFC.Models.EPR.CaseWriteRule GetCatalogByID(string ruleCode)
        {
            string sqlIndex = "EPR.CaseWriteRule.QueryCatalog";
            string sqlWhereIndex = "EPR.CaseWriteRule.QueryCatalog.Where";
            FS.HISFC.Models.EPR.CaseWriteRule rule = new FS.HISFC.Models.EPR.CaseWriteRule();
            System.Data.DataSet ds = new System.Data.DataSet();
            string strSql = string.Empty;
            try
            {
                if (this.Sql.GetSql(sqlIndex, ref strSql) == -1)
                {
                    return null;
                }
                string strSqlWhere = string.Empty;
                if (this.Sql.GetSql(sqlWhereIndex, ref strSqlWhere) == -1)
                {
                    return null;
                }
                strSql += string.Format(strSqlWhere, ruleCode);
                if (this.ExecQuery(strSql, ref ds) == -1)
                {
                    return null;
                }
                strSql = string.Empty;
                sqlIndex = "EPR.CaseWriteRule.QueryBlob";
                if (this.Sql.GetSql(sqlIndex, ref strSql) == -1)
                {
                    return null;
                }
                byte[] data;
                string sql = strSql;
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    rule.RuleCode = row["rule_code"].ToString();
                    rule.RuleName = row["rule_name"].ToString();
                    rule.DeptName = row["dept_name"].ToString();
                    rule.DeptCode = row["his_dept_code"].ToString();
                    rule.Descript = row["brifsign"].ToString();
                    rule.Sort = row["sort"].ToString();
                    rule.Memo = row["memo"].ToString();
                    rule.RuleLink = row["rule_link"].ToString();
                    rule.ParentCode = row["parent_code"].ToString();

                    strSql = string.Format(sql, rule.RuleCode);
                    data = this.OutputBlob(strSql);
                    rule.RuleData = (data == null ? (string.Empty) : System.Text.Encoding.GetEncoding("GB2312").GetString(data));

                    break;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message + "\r\n" + e.StackTrace;
                return null;
            }
            return rule;
        }
        /// <summary>
        /// ͨ��Ŀ¼���Ƶõ�ĳ��Ŀ¼�������ص�һ��
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryCatalogByName(string ruleName)
        {
            string sqlIndex = "EPR.CaseWriteRule.QueryCatalog";
            string sqlWhereIndex = "EPR.CaseWriteRule.QueryCatalog.Where1";
            string strSql = string.Empty;
            string strSqlWhere = string.Empty;
            string sqlOrder = string.Empty;
            ArrayList alList = new ArrayList();
            FS.HISFC.Models.EPR.CaseWriteRule rule ;
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {
                if (this.Sql.GetSql("EPR.CaseWriteRule.QueryCatalog.OrderByForF3", ref sqlOrder) == -1)
                {
                    return null;
                }
                if (this.Sql.GetSql(sqlIndex, ref strSql) == -1)
                {
                    return null;
                }
                if (this.Sql.GetSql(sqlWhereIndex, ref strSqlWhere) == -1)
                {
                    return null;
                }
                strSql += string.Format(strSqlWhere, ruleName) + sqlOrder;
                if (this.ExecQuery(strSql, ref ds) == -1)
                {
                    return null;
                }
                strSql = string.Empty;
                sqlIndex = "EPR.CaseWriteRule.QueryBlob";
                if (this.Sql.GetSql(sqlIndex, ref strSql) == -1)
                {
                    return null;
                }
                byte[] data;
                string sql = strSql;
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    rule =  new FS.HISFC.Models.EPR.CaseWriteRule();
                    rule.RuleCode = row["rule_code"].ToString();
                    rule.RuleName = row["rule_name"].ToString();
                    rule.DeptName = row["dept_name"].ToString();
                    rule.DeptCode = row["his_dept_code"].ToString();
                    rule.Descript = row["brifsign"].ToString();
                    rule.Sort = row["sort"].ToString();
                    rule.Memo = row["memo"].ToString();
                    rule.RuleLink = row["rule_link"].ToString();
                    rule.ParentCode = row["parent_code"].ToString();

                    strSql = string.Format(sql, rule.RuleCode);
                    data = this.OutputBlob(strSql);
                    rule.RuleData = (data == null ? (string.Empty) : System.Text.Encoding.GetEncoding("GB2312").GetString(data));

                    alList.Add(rule);
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message + "\r\n" + e.StackTrace;
                return null;
            }
            return alList;
        }
    }
}
