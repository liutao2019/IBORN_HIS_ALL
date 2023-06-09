using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace FS.HISFC.BizLogic.HealthRecord
{
    /// <summary>
    /// 病案转科业务
    /// </summary>
    public class DeptShift : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 转科记录  如果strType等于 1 从变更表里查询 如果 等于2 从病案变更表里查询 
        /// </summary>
        /// <param name="inpatienNo">住院流水号</param>
        /// <param name="strType">标识</param>
        /// <returns></returns>
        public ArrayList QueryChangeDeptFromShiftApply(string inpatienNo, string strType)
        {
            string strSql = "";
            ArrayList list = new ArrayList();
            if (strType == "1")
            {
                if (this.Sql.GetSql("Case.BaseDML.GetChangeDeptFromShiftApply1", ref strSql) == -1) return null;
            }
            if (strType == "2")
            {
                if (this.Sql.GetSql("Case.BaseDML.GetChangeDeptFromShiftApply2", ref strSql) == -1) return null;
            }
            try
            {
                strSql = string.Format(strSql, inpatienNo);
                //查询
                this.ExecQuery(strSql);
                FS.HISFC.Models.RADT.Location info = null;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.RADT.Location();
                    info.Dept.ID = Reader[0].ToString(); //科室编码
                    info.Dept.Name = Reader[1].ToString();//科室名称
                    info.Dept.Memo = Reader[2].ToString();// 转科时间
                    if (strType == "2")
                    {
                        info.User03 = Reader[3].ToString(); //发生序号
                        info.Floor = Reader[4].ToString();//在科天数
                    }
                    list.Add(info);
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                list = null;
            }
            return list;
        }

        /// <summary>
        /// 更新或增加一条信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertOrUpdate(FS.HISFC.Models.RADT.Location info)
        {
            int rowCount = UpdateChangeDept(info);
            if (rowCount <= 0)
            {
                return InsertChangeDept(info);
            }
            return rowCount;
        }

        /// <summary>
        /// 插入科室变更表 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertChangeDept(FS.HISFC.Models.RADT.Location info)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.BaseDML.InsertChangeDept", ref strSql) == -1) return -1;
            try
            {
                //获取索引值
                info.User03 = this.GetSequence("Case.BaseDML.InsertChangeDeptSequence");
                if (info.Building == "" || info.Building == null) //操作员
                {
                    info.Building = this.Operator.ID;
                }
                object[] mm = GetInfo(info);
                if (mm == null)
                {
                    this.Err = "业务层从实体中获取字符数组出错";
                    return -1;
                }
                strSql = string.Format(strSql, mm);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        public int UpdateChangeDept(FS.HISFC.Models.RADT.Location info)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.BaseDML.UpdateChangeDept", ref strSql) == -1) return -1;
            try
            {
                object[] mm = GetInfo(info);
                if (mm == null)
                {
                    this.Err = "业务层从实体中获取字符数组出错";
                    return -1;
                }
                strSql = string.Format(strSql, mm);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// 删除一行转科数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int DeleteChangeDept(FS.HISFC.Models.RADT.Location info)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.BaseDML.DeleteChangeDept", ref strSql) == -1) return -1;
            try
            {
                //格式化字符串
                strSql = string.Format(strSql, info.User02, info.User03); //user02 住院流水号 user03发生序号
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// 删除一行转科数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int DeleteChangeDept(string InpatientNO)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.BaseDML.DeleteChangeDeptByInpatientNO", ref strSql) == -1) return -1;
            try
            {
                //格式化字符串
                strSql = string.Format(strSql,InpatientNO); //user02 住院流水号 user03发生序号
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        private object[] GetInfo(FS.HISFC.Models.RADT.Location info)
        {
            try
            {
                object[] s = new object[10];
                s[0] = info.User02;		//住院流水号
                s[1] = info.User03; //发生序号      
                s[2] = info.Dept.ID; //科室编码
                s[3] = info.Dept.Name;//科室名称
                s[4] = info.Dept.Memo; //转科时间
                s[5] = "0"; //在科天数
                //s[5] = info.Floor; //在科天数
                s[6] = info.Building; //操作员 
                return s;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }
        #region  新电子病历转科信息
        /// <summary>
        /// 转科记录视图
        /// </summary>
        /// <param name="inpatienNo">住院流水号</param>
        /// <param name="strType">标识</param>
        /// <returns></returns>
        public ArrayList QueryChangeDeptFromView(string inpatienNo)
        {
            string strSql = @"select dept_code,dept_name,change_date,happen_no,days from view_met_cas_changedept where inpatient_no='{0}'";
            ArrayList list = new ArrayList();
            
            try
            {
                strSql = string.Format(strSql, inpatienNo);
                //查询
                this.ExecQuery(strSql);
                FS.HISFC.Models.RADT.Location info = null;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.RADT.Location();
                    info.Dept.ID = Reader[0].ToString(); //科室编码
                    info.Dept.Name = Reader[1].ToString();//科室名称
                    info.Dept.Memo = Reader[2].ToString();// 转科时间
                    info.User03 = Reader[3].ToString(); //发生序号
                    info.Floor = Reader[4].ToString();//在科天数
                    
                    list.Add(info);
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                list = null;
            }
            return list;
        }

        #endregion
        #region 废弃
        /// <summary>
        /// 转科记录  如果strType等于 1 从变更表里查询 如果 等于2 从病案变更表里查询 
        /// </summary>
        /// <param name="inpatienNo">住院流水号</param>
        /// <param name="strType">标识</param>
        /// <returns></returns>
        [Obsolete("废弃,用代替 QueryChangeDeptFromShiftApply",true)]
        public ArrayList GetChangeDeptFromShiftApply(string inpatienNo, string strType)
        {
            string strSql = "";
            ArrayList list = new ArrayList();
            if (strType == "1")
            {
                if (this.Sql.GetSql("Case.BaseDML.GetChangeDeptFromShiftApply1", ref strSql) == -1) return null;
            }
            if (strType == "2")
            {
                if (this.Sql.GetSql("Case.BaseDML.GetChangeDeptFromShiftApply2", ref strSql) == -1) return null;
            }
            try
            {
                strSql = string.Format(strSql, inpatienNo);
                //查询
                this.ExecQuery(strSql);
                FS.HISFC.Models.RADT.Location info = null;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.RADT.Location();
                    info.Dept.ID = Reader[0].ToString(); //科室编码
                    info.Dept.Name = Reader[1].ToString();//科室名称
                    info.Dept.Memo = Reader[2].ToString();// 转科时间
                    if (strType == "2")
                    {
                        info.User03 = Reader[3].ToString(); //发生序号
                        info.Floor = Reader[4].ToString();//在科天数
                    }
                    list.Add(info);
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                list = null;
            }
            return list;
        }
        /// <summary>
        /// 查询病室 
        /// </summary>
        /// <param name="inpatienNo">住院流水号</param>
        /// <param name="strType">1 查询是否有转床 2 查询病室信息</param>
        /// <returns></returns>
        public string QueryWardNoBedNOByInpatienNO(string inpatienNo, string strType)
        {
            string strSql = "";
            string returnStr = "";
            ArrayList list = new ArrayList();
            if (strType == "1")
            {
                if (this.Sql.GetSql("Case.BaseDML.ComShiftdate.RB", ref strSql) == -1) return null;
            }
            if (strType == "2")
            {
                if (this.Sql.GetSql("Case.BaseDML.WardNoBedNO", ref strSql) == -1) return null;
            }
            try
            {
                strSql = string.Format(strSql, inpatienNo);
                //查询
                this.ExecQuery(strSql);

                while (this.Reader.Read())
                {
                    returnStr = this.Reader[0].ToString();
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                returnStr = "";
            }
            return returnStr;
        }
        #endregion 
    }
}
