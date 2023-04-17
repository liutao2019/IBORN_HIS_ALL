﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Order.Inpatient;
using System.Collections;

namespace FS.HISFC.BizLogic.Order
{
    /// <summary>
    /// FS.HISFC.Models.Order.Inpatient.MedicalTeam<br></br>
    /// [功能描述: 医疗组维护业务层]<br></br>
    /// [创 建 者: 牛鑫元]<br></br>
    /// [创建时间: 2010-06-29]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public class MedicalTeam:FS.FrameWork.Management.Database
    {

        


        #region 查询
        /// <summary>
        /// 根据sql语句插叙医疗组维护信息
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        private List<FS.HISFC.Models.Order.Inpatient.MedicalTeam> ExcuteQueryBySql(string strSql)
        {
            int returnValue = this.ExecQuery(strSql);

            if (returnValue < 0)
            {
                return null;
            }
            List<FS.HISFC.Models.Order.Inpatient.MedicalTeam> medicalTeamList = new List<FS.HISFC.Models.Order.Inpatient.MedicalTeam>();
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Order.Inpatient.MedicalTeam medicalTeamObj = new FS.HISFC.Models.Order.Inpatient.MedicalTeam();
                medicalTeamObj.Dept.ID = this.Reader[0].ToString();
                medicalTeamObj.Dept.Name = this.Reader[1].ToString();
                medicalTeamObj.ID = this.Reader[2].ToString();
                medicalTeamObj.Name = this.Reader[3].ToString();
                medicalTeamObj.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[4].ToString());
                medicalTeamObj.Oper.ID = this.Reader[5].ToString();
                medicalTeamObj.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6].ToString());
                medicalTeamList.Add(medicalTeamObj);

            }
            this.Reader.Close();
            return medicalTeamList;
        }

        /// <summary>
        /// 根据索引查询
        /// </summary>
        /// <param name="sqlIndex"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private List<FS.HISFC.Models.Order.Inpatient.MedicalTeam> QueryMedicalTeamBySqlIndex(string whereIndex, params string[] args)
        {
            string strSql = string.Empty;
            string sqlIndex = "MedicalTeam.Select.Base";
            string strWhere = string.Empty;
            
            //查询基本sql
            int returnValue = this.Sql.GetCommonSql(sqlIndex, ref strSql);
            if (returnValue < 0)
            {
                this.Err = "查询" +sqlIndex+"对应的sql语句失败\n" + this.Sql.Err;
                return null;
            }

            //查询条件
            returnValue = this.Sql.GetCommonSql(whereIndex, ref strWhere);
            if (returnValue < 0)
            {
                this.Err = "查询" + sqlIndex + "对应的sql语句失败\n" + this.Sql.Err;
                return null;
            }

            try
            {
                strWhere = string.Format(strWhere, args);
            }
            catch (Exception ex)
            {

                this.Err = "格式化字符串出错!\n"  + ex.Message;
                return null;
            }

            return this.ExcuteQueryBySql(strSql + " " + strWhere);
        }

        /// <summary>
        /// 插入或删除信息
        /// </summary>
        /// <param name="sqlIndex"></param>
        /// <param name="medicalTeam"></param>
        /// <returns></returns>
        private int InsertOrUpdateTable(string sqlIndex, FS.HISFC.Models.Order.Inpatient.MedicalTeam medicalTeam)
        {
            string[] args = this.GetArgs(medicalTeam);

            string strSql = string.Empty;

            int returnValue = this.Sql.GetCommonSql(sqlIndex, ref strSql);

            if (returnValue < 0)
            {
                this.Err = "查询" + sqlIndex + "对应的sql语句失败！\n" + this.Sql.Err;
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, args);
            }
            catch (Exception ex)
            {

                this.Err = "格式化字符串出错!\n" + ex.Message;
                return -1;
            }



            return this.ExecNoQuery(strSql);
        }

        private string[] GetArgs(FS.HISFC.Models.Order.Inpatient.MedicalTeam medicalTeam)
        {
            string[] args = new string[]
            {
               medicalTeam.Dept.ID,
               medicalTeam.Dept.Name,
               medicalTeam.ID,
               medicalTeam.Name,
               FS.FrameWork.Function.NConvert.ToInt32(medicalTeam.IsValid).ToString(),
               medicalTeam.Oper.ID,
               medicalTeam.Oper.OperTime.ToString()
                 };
            return args;
        }



        
        #endregion
        #region 共有方法
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="medicalTeam"></param>
        /// <returns></returns>
        public int InsertMedicalTeam(FS.HISFC.Models.Order.Inpatient.MedicalTeam medicalTeam)
        {
            return this.InsertOrUpdateTable("MedicalTeam.Insert", medicalTeam);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="medicalTeam"></param>
        /// <returns></returns>
        public int UpdateMedicalTeam(FS.HISFC.Models.Order.Inpatient.MedicalTeam medicalTeam)
        {
            return this.InsertOrUpdateTable("MedicalTeam.Update", medicalTeam);
        }

        /// <summary>
        /// 根据科室医疗组编码删除医疗组信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="medicalTeamCode"></param>
        /// <returns></returns>
        public int DeleteMedicalTeam(string deptCode, string medicalTeamCode)
        {
            string strSql = string.Empty;

            int returnValue = this.Sql.GetCommonSql("MedicalTeam.Delete", ref strSql);

            if (returnValue < 0)
            {
                this.Err = "查询MedicalTeam.Delete对应的sql语句失败！\n" + this.Sql.Err;
                return -1;
            }

            try
            {
                strSql = string.Format(strSql,deptCode, medicalTeamCode);
            }
            catch (Exception ex)
            {

                this.Err = "格式化字符串出错!\n" + ex.Message;
                return -1;
            }



            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 根据科室查询医疗组信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Order.Inpatient.MedicalTeam> QueryMedicalTeamByDept(string deptCode)
        {
            return this.QueryMedicalTeamBySqlIndex("MedicalTeam.Select.Where1", deptCode);
        }


        /// <summary>
        /// 更新标记
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="medicalTeamCode"></param>
        /// <returns></returns>
        public int UpdateValidFlag(string isValid, string deptCode, string medicalTeamCode)
        {
            string strSql = string.Empty;

            int returnValue = this.Sql.GetCommonSql("MedicalTeam.Update.ValidFlag", ref strSql);

            if (returnValue < 0)
            {
                this.Err = "查询MedicalTeam.Delete对应的sql语句失败！\n" + this.Sql.Err;
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, isValid, deptCode, medicalTeamCode);
            }
            catch (Exception ex)
            {

                this.Err = "格式化字符串出错!\n" + ex.Message;
                return -1;
            }



            return this.ExecNoQuery(strSql);
        }
        #endregion
    }
}
