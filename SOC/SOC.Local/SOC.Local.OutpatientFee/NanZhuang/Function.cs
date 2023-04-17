using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.OutpatientFee.NanZhuang
{
    public class Function : FS.FrameWork.Management.Database
    {
        #region 根据姓名和身份证号合作医疗名单(1：成功/-1：失败)
        /// <summary>
        /// 根据姓名和身份证号合作医疗名单(1：成功/-1：失败)
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="idenNO">身份证号</param>
        /// <returns>1：成功/-1：失败</returns>
        public int GetCooperatePatientByNameAndIdenNO(string name, string idenNO, string department, string stateFlag, ref System.Data.DataSet dsResult)
        {
            int intReturn = 0;
            string sql="";
            intReturn = this.Sql.GetSql("FSLOCAL.GetCooperatePatientByNameAndIdenNO", ref sql);
            try
            {
                sql = string.Format(sql, name, idenNO, department, stateFlag);
            }
            catch (Exception e)
            {
                this.Err = "格式化SQL语句失败(" + this.Err + ")(" + e.Message + ")";
                return -1;
            }

            //
            // 执行SQL语句
            //
            intReturn = this.ExecQuery(sql, ref dsResult);
            if (intReturn == -1)
            {
                this.Err = "执行SQL语句失败" + this.Err;
                return -1;
            }

            return 1;
        }
        #endregion

        #region 根据姓名和身份证号合作医疗名单(1：成功/-1：失败)
        /// <summary>
        /// 根据姓名和身份证号合作医疗名单(1：成功/-1：失败)
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="idenNO">身份证号</param>
        /// <returns>1：成功/-1：失败</returns>
        public int InsertCooperatePatientByNameAndIDenNO(string name,string idenNO,string sex,string address, string department,string beginDate,string endDate,string stateFlag,string birthday,string siNo)
        {
            int intReturn = 0;
            string sql = "";
            intReturn = this.Sql.GetSql("FSLOCAL.InsertCooperatePatientByNameAndIDenNO", ref sql);
            try
            {
                sql = string.Format(sql, name, idenNO, siNo, birthday, sex, beginDate, endDate, stateFlag, address, department);
            }
            catch (Exception e)
            {
                this.Err = "格式化SQL语句失败(" + this.Err + ")(" + e.Message + ")";
                return -1;
            }

            //
            // 执行SQL语句
            //
            intReturn = this.ExecNoQuery(sql);
            if (intReturn == -1)
            {
                this.Err = "执行SQL语句失败" + this.Err;
                return -1;
            }

            return 1;
        }
        #endregion

        #region 根据姓名和身份证号合作医疗名单(1：成功/-1：失败)
        /// <summary>
        /// 根据姓名和身份证号合作医疗名单(1：成功/-1：失败)
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="idenNO">身份证号</param>
        /// <returns>1：成功/-1：失败</returns>
        public int UpdateCooperatePatientByNameAndIDenNO(string name, string idenNO, string sex, string address, string department, string beginDate, string endDate, string stateFlag, string birthday, string siNo, string seqNo)
        {
            int intReturn = 0;
            string sql = "";
            intReturn = this.Sql.GetSql("FSLOCAL.UpdateCooperatePatientByNameAndIDenNO", ref sql);
            try
            {
                sql = string.Format(sql, name, idenNO, siNo, birthday, sex, beginDate, endDate, stateFlag, address, department, seqNo);
            }
            catch (Exception e)
            {
                this.Err = "格式化SQL语句失败(" + this.Err + ")(" + e.Message + ")";
                return -1;
            }

            //
            // 执行SQL语句
            //
            intReturn = this.ExecNoQuery(sql);
            if (intReturn == -1)
            {
                this.Err = "执行SQL语句失败" + this.Err;
                return -1;
            }

            return 1;
        }
        #endregion

    }
}
