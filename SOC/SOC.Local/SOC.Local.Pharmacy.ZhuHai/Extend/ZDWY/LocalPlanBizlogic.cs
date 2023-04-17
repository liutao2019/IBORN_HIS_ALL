using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Extend.ZDWY
{
    public class LocalPlanBizlogic:FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 获取所有公式列表
        /// </summary>
        /// <returns></returns>
        public System.Collections.ArrayList GetAllPlanFomula()
        {
            ArrayList allPlanFomula = new ArrayList();
            string strSql = string.Empty;
            if (this.Sql.GetSql("SOC.Pharmacy.PLAN.GETPLANFOMULA",ref strSql) == -1)
            {
                this.Err = "没有找到sql语句：SOC.Pharmacy.PLAN.GETPLANFOMULA";
                return null;
            }
            try
            {
                this.ExecQuery(strSql);
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject neuPlanFomula = new FS.FrameWork.Models.NeuObject();
                    neuPlanFomula.ID = this.Reader[0].ToString();//ID
                    neuPlanFomula.User01 = this.Reader[1].ToString();//平均消耗日期
                    neuPlanFomula.User02 = this.Reader[2].ToString();//比率
                    neuPlanFomula.User03 = this.Reader[3].ToString();//计划日期
                    neuPlanFomula.Name = this.Reader[4].ToString();//公式
                    allPlanFomula.Add(neuPlanFomula);
                }
            }
            catch (Exception ex)
            {
                this.Reader.Close();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return allPlanFomula;
        }

        /// <summary>
        /// 删除公式
        /// </summary>
        /// <returns></returns>
        public int DeleteOnePlanFomula(string fomulaID)
        {
            string strSql = string.Empty;
            if (this.Sql.GetSql("SOC.Pharmacy.PLAN.DELETEONEFOMULA", ref strSql) == -1)
            {
                this.Err = "没有找到sql语句：SOC.Pharmacy.PLAN.DELETEONEFOMULA";
                return -1;
            }
            strSql = string.Format(strSql, FS.FrameWork.Function.NConvert.ToInt32(fomulaID));
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 插入一条计划公式
        /// </summary>
        /// <param name="neuPlanFomula"></param>
        /// <returns></returns>
        public int InsertOnePlanFomula(FS.FrameWork.Models.NeuObject neuPlanFomula)
        {
            string strSql = string.Empty;
            if (this.Sql.GetSql("SOC.Pharmacy.PLAN.INSERTONEFOMULA", ref strSql) == -1)
            {
                this.Err = "没有找到sql语句：SOC.Pharmacy.PLAN.INSERTONEFOMULA";
                return -1;
            }

            string seqSql = string.Empty;
            if (this.Sql.GetSql("SOC.Pharmacy.PLAN.GETSEQUENCENO", ref seqSql) == -1)
            {
                this.Err = "没有找到sql语句：SOC.Pharmacy.PLAN.GETSEQUENCENO";
                return -1;
            }
            string seqString = this.ExecSqlReturnOne(seqSql);
            strSql = string.Format(strSql, seqString, neuPlanFomula.User01, neuPlanFomula.User02, neuPlanFomula.User03, neuPlanFomula.Name);
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 插入入库扩展表
        /// </summary>
        /// <param name="inputInfo"></param>
        /// <returns></returns>
        public int InsertInputExtend(FS.HISFC.Models.Pharmacy.Input inputInfo)
        {
            string strSql = string.Empty;
            if (this.Sql.GetSql("SOC.Pharmacy.Input.InsertInputExtend", ref strSql) == -1)
            {
                this.Err = "没有找到sql语句：SOC.Pharmacy.Input.InsertInputExtend";
                return -1;
            }
            strSql = string.Format(strSql, inputInfo.Item.ID, inputInfo.Item.Name, inputInfo.BatchNO, inputInfo.GroupNO, inputInfo.Company.ID, inputInfo.Producer.ID,inputInfo.InListNO,inputInfo.Operation.Oper.ID);
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 根据药品编码、批次号、批号获取药品扩展信息
        /// </summary>
        /// <param name="drugCode"></param>
        /// <param name="batchNO"></param>
        /// <param name="groupNO"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Pharmacy.Input GetInputExtendInfo(string drugCode, string batchNO, string groupNO)
        {
            FS.HISFC.Models.Pharmacy.Input inputTmp = null;
            string strSql = string.Empty;
            if (this.Sql.GetSql("SOC.Pharmacy.Input.SelectInputExtend", ref strSql) == -1)
            {
                this.Err = "没有找到sql语句：SOC.Pharmacy.Input.SelectInputExtend";
                return inputTmp;
            }
            strSql = string.Format(strSql, drugCode, batchNO, groupNO);
            try
            {
                this.ExecQuery(strSql);
                while (this.Reader.Read())
                {
                    inputTmp = new FS.HISFC.Models.Pharmacy.Input();
                    inputTmp.Item.ID = this.Reader[0].ToString();
                    inputTmp.Item.Name = this.Reader[1].ToString();
                    inputTmp.BatchNO = this.Reader[2].ToString();
                    inputTmp.GroupNO = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3].ToString());
                    inputTmp.Producer.ID = this.Reader[4].ToString();
                    inputTmp.Company.ID = this.Reader[5].ToString();
                    inputTmp.InListNO = this.Reader[6].ToString();
                    inputTmp.Operation.Oper.ID = this.Reader[7].ToString();
                    inputTmp.Operation.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8]);
                }
            }
            catch (Exception ex)
            {
                this.Reader.Close();
                return inputTmp;
            }
            return inputTmp;
        }
    }
}
