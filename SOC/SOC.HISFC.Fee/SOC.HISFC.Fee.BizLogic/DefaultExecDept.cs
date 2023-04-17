using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FS.SOC.HISFC.Fee.BizLogic
{
    /// <summary>
    /// [功能描述:默认执行科室业务类]<br></br>
    /// [创 建 者: zhaoj]<br></br>
    /// [创建时间: 2012-4]<br></br>
    /// </summary>
    public class DefaultExecDept:FS.FrameWork.Management.Database
    {
        private string[] myGetParams(FS.SOC.HISFC.Fee.Models.DefaultExecDept execDept)
        {
            return new string[] { 
                        execDept.ID,
                        execDept.Compare.ID,
                        execDept.Compare.Name,
                        execDept.Original.ID,
                        execDept.Original.UserCode,
                        execDept.Original.Name,
                        execDept.Target.ID,
                        execDept.Target.UserCode,
                        execDept.Target.Name,
                        execDept.Target.SpellCode,
                        execDept.Target.WBCode,
                        execDept.User01,
                        execDept.User02,
                        execDept.User03,
                        execDept.Target2.ID,
                        execDept.Target2.UserCode,
                        execDept.Target2.Name,
                        execDept.Target2.SpellCode,
                        execDept.Target2.WBCode,
                        execDept.Target2.User01,
                        execDept.Target2.User02,
                        execDept.Target2.User03
            };
        }

        /// <summary>
        /// 插入方法
        /// </summary>
        /// <param name="execDept"></param>
        /// <returns></returns>
        public int Insert(FS.SOC.HISFC.Fee.Models.DefaultExecDept execDept)
        {
            string sql = FS.SOC.HISFC.Fee.Data.AbstractExecDept.Current.Insert;

            return this.ExecNoQuery(string.Format(sql, this.myGetParams(execDept)));
        }

        /// <summary>
        /// 更新方法
        /// </summary>
        /// <param name="execDept"></param>
        /// <returns></returns>
        public int Update(FS.SOC.HISFC.Fee.Models.DefaultExecDept execDept)
        {
            string sql = FS.SOC.HISFC.Fee.Data.AbstractExecDept.Current.Update;

            return this.ExecNoQuery(string.Format(sql, this.myGetParams(execDept)));
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(string id)
        {
            string sql = FS.SOC.HISFC.Fee.Data.AbstractExecDept.Current.Delete;

            return this.ExecNoQuery(string.Format(sql,id));
        }

        /// <summary>
        /// 获取ID
        /// </summary>
        /// <returns></returns>
        public string GetID()
        {
            return this.ExecSqlReturnOne(FS.SOC.HISFC.Fee.Data.AbstractExecDept.Current.AutoID);
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public List<FS.SOC.HISFC.Fee.Models.DefaultExecDept> QueryAll(string compareID)
        {
            if (this.ExecQuery(string.Format(FS.SOC.HISFC.Fee.Data.AbstractExecDept.Current.SelectAll+" "+FS.SOC.HISFC.Fee.Data.AbstractExecDept.Current.WhereByCompareID,compareID)) == -1)
            {
                return null;
            }

            List<FS.SOC.HISFC.Fee.Models.DefaultExecDept> pactUnitList = new List<FS.SOC.HISFC.Fee.Models.DefaultExecDept>();//费用明细数组
            FS.SOC.HISFC.Fee.Models.DefaultExecDept pactUnit = null;

            try
            {
                while (this.Reader.Read())
                {
                    pactUnit = new FS.SOC.HISFC.Fee.Models.DefaultExecDept();
                    pactUnit.ID = this.Reader[0].ToString();         
                    pactUnit.Compare.ID = this.Reader[1].ToString();
                    pactUnit.Compare.ID = this.Reader[1].ToString();
                    pactUnit.Compare.Name = this.Reader[2].ToString();
                    pactUnit.Original.ID = this.Reader[3].ToString();
                    pactUnit.Original.UserCode = this.Reader[4].ToString();
                    pactUnit.Original.Name = this.Reader[5].ToString();
                    pactUnit.Target.ID = this.Reader[6].ToString();
                    pactUnit.Target.UserCode = this.Reader[7].ToString();
                    pactUnit.Target.Name = this.Reader[8].ToString();
                    pactUnit.Target.SpellCode = this.Reader[9].ToString();
                    pactUnit.Target.WBCode = this.Reader[10].ToString();
                    pactUnit.Target.User01 = this.Reader[11].ToString();
                    pactUnit.Target.User02 = this.Reader[12].ToString();
                    pactUnit.Target.User03 = this.Reader[13].ToString();
                    pactUnit.Target2.ID = this.Reader[14].ToString();
                    pactUnit.Target2.UserCode = this.Reader[15].ToString();
                    pactUnit.Target2.Name = this.Reader[16].ToString();
                    pactUnit.Target2.SpellCode = this.Reader[17].ToString();
                    pactUnit.Target2.WBCode = this.Reader[18].ToString();
                    pactUnit.Target2.User01 = this.Reader[19].ToString();
                    pactUnit.Target2.User02 = this.Reader[20].ToString();
                    pactUnit.Target2.User03 = this.Reader[21].ToString();   
                    pactUnitList.Add(pactUnit);
                }
                return pactUnitList;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// 根据原编码获取对照的信息
        /// </summary>
        /// <param name="compareID"></param>
        /// <param name="originalID"></param>
        /// <returns></returns>
        public List<FS.FrameWork.Models.NeuObject> QueryExecDept(string compareID, string originalID)
        {
            if (this.ExecQuery(string.Format(FS.SOC.HISFC.Fee.Data.AbstractExecDept.Current.SelectAll + " " + FS.SOC.HISFC.Fee.Data.AbstractExecDept.Current.WhereByCompareIDAndOriginalID, compareID,originalID)) == -1)
            {
                return null;
            }

            List<FS.FrameWork.Models.NeuObject> pactUnitList = new List<FS.FrameWork.Models.NeuObject>();//费用明细数组
            FS.FrameWork.Models.NeuObject pactUnit = null;
            try
            {
                while (this.Reader.Read())
                {
                    pactUnit = new FS.FrameWork.Models.NeuObject();
                    pactUnit.ID = this.Reader[6].ToString();
                    pactUnit.Name = this.Reader[8].ToString();
                    pactUnitList.Add(pactUnit);
                }
                return pactUnitList;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// 根据A+B获取对照的信息
        /// </summary>
        /// <param name="compareID"></param>
        /// <param name="originalID"></param>
        /// <returns></returns>
        public List<FS.FrameWork.Models.NeuObject> QueryExecDept(string compareID, string originalID,string targetID)
        {
            if (this.ExecQuery(string.Format(FS.SOC.HISFC.Fee.Data.AbstractExecDept.Current.SelectAll + " " + FS.SOC.HISFC.Fee.Data.AbstractExecDept.Current.WhereByCompareIDAndOrigianlIDAndTargetID, compareID, originalID, targetID)) == -1)
            {
                return null;
            }

            List<FS.FrameWork.Models.NeuObject> pactUnitList = new List<FS.FrameWork.Models.NeuObject>();//费用明细数组
            FS.FrameWork.Models.NeuObject pactUnit = null;
            try
            {
                while (this.Reader.Read())
                {
                    pactUnit = new FS.FrameWork.Models.NeuObject();
                    pactUnit.ID = this.Reader[14].ToString();
                    pactUnit.Name = this.Reader[16].ToString();
                    pactUnitList.Add(pactUnit);
                }
                return pactUnitList;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        public DataTable QueryForDataSet(string compareID)
        {
            DataSet ds = new DataSet();

            if (this.ExecQuery(string.Format(FS.SOC.HISFC.Fee.Data.AbstractExecDept.Current.SelectAll + " " + FS.SOC.HISFC.Fee.Data.AbstractExecDept.Current.WhereByCompareID, compareID), ref ds) < 0 || ds == null)
            {
                return null;
            }

            if (ds.Tables.Count == 0)
            {
                return new DataTable();
            }

            return ds.Tables[0];
        }
    }
}
