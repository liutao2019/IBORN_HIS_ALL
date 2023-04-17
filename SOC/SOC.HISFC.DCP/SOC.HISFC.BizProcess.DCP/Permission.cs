using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;

namespace FS.SOC.HISFC.BizProcess.DCP
{
    /// <summary>
    /// [功能描述: 权限管理]<br></br>
    /// [创 建 者: ]<br></br>
    /// [创建时间: 2009-05]<br></br>
    /// <说明>
    ///     1、HIS4.5 系统重构 由原HISFC.Manager内挪过来
    /// </说明>
    /// </summary>
    public class Permission : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 根据人员编码，二级权限编码取人员拥有权限的部门
        /// </summary>
        /// <param name="userCode">操作员编码</param>
        /// <param name="class2Code">二级权限码</param>
        /// <returns>成功返回具有权限的科室集合 失败返回null</returns>        
        public List<FS.FrameWork.Models.NeuObject> QueryUserPriv(string userCode, string class2Code)
        {
            //取SQL语句
            string sqlstring = PrepareSQL("Manager.UserPowerDetailManager.LoadPrivDept", userCode, class2Code);
            if (sqlstring == null)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            //取数据
            List<FS.FrameWork.Models.NeuObject> al = new List<NeuObject>();
            try
            {
                FS.FrameWork.Models.NeuObject info;
                this.ExecQuery(sqlstring);
                while (this.Reader.Read())
                {
                    info = new NeuObject();
                    info.ID = this.Reader[0].ToString();  //科室编码
                    info.Name = this.Reader[1].ToString();  //科室名称
                    info.User01 = this.Reader[2].ToString();  //二级权限编码
                    info.User02 = this.Reader[3].ToString();  //二级权限名称
                    info.User03 = this.Reader[4].ToString();  //二级权限特殊标记：1判断窗口权限时，只要存在权限就允许进入，不需要用户选择科室
                    info.Memo = this.Reader[5].ToString();  //科室类型
                    al.Add(info);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            return al;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        private string PrepareSQL(string sqlName, params string[] values)
        {
            string strSql = string.Empty;
            if (this.Sql.GetSql(sqlName, ref  strSql) == -1)
            {
                this.Err = "找不到sql语句:" + sqlName;
                return null;
            }
            try
            {
                if (values != null)
                    strSql = string.Format(strSql, values);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                strSql = null;
            }
            return strSql;
        }
    }
}
