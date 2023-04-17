using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Local.Material.Base
{
    public class DataBaseLogic:FS.FrameWork.Management.Database
    {
      
        /// <summary>
        /// 查询信息
        /// </summary>
        /// <param name="dv">返回的数据视图</param>
        /// <returns>1,成功; -1,失败</returns>
        public int QueryDataBySql(string sql,ref System.Data.DataTable  dt,params string[] p )
        {
            string strsql = sql;
            //if (this.Sql.GetSql("Manager.Bed.QueryBedInfo", ref strsql) == -1)
            //{
            //    return -1;
            //}
            try
            {
                strsql = string.Format(strsql,p);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            System.Data.DataSet ds = new System.Data.DataSet();
            if (this.ExecQuery(strsql, ref ds) == -1)
            {
                return -1;
            }
            dt = ds.Tables[0];
            return 1;
        }
        /// <summary>
        /// 查询信息
        /// </summary>
        /// <param name="dv">返回的数据视图</param>
        /// <returns>1,成功; -1,失败</returns>
        public int QueryDataBySqlId(string sqlId, ref System.Data.DataTable dt, params string[] p)
        {
            string strsql = string.Empty;
            if (this.Sql.GetSql(sqlId, ref strsql) == -1)
            {
                return -1;
            }
            try
            {
                strsql = string.Format(strsql, p);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            System.Data.DataSet ds = new System.Data.DataSet();
            if (this.ExecQuery(strsql, ref ds) == -1)
            {
                return -1;
            }
            dt = ds.Tables[0];
            return 1;
        }
        /// <summary>
        /// 查询信息
        /// </summary>
        /// <param name="dv">返回的数据视图</param>
        /// <returns>1,成功; -1,失败</returns>
        public int QueryDataBySql(string sql, ref System.Data.DataTable dt, System.Collections.Generic.List<FS.FrameWork.Models.NeuObject> p)
        {
            string pStr = string.Empty;

            foreach (FS.FrameWork.Models.NeuObject  no in p)
            {
                pStr = pStr + no.Name.ToString() + ",";
            }
            return this.QueryDataBySql(sql,ref dt, pStr.Split(','));
          
        }
        /// <summary>
        /// 查询信息
        /// </summary>
        /// <param name="dv">返回的数据视图</param>
        /// <returns>1,成功; -1,失败</returns>
        public int QueryDataBySqlId(string sqlId, ref System.Data.DataTable dt, System.Collections.Generic.List<FS.FrameWork.Models .NeuObject> p)
        {
            string pStr = string.Empty;

            foreach (FS.FrameWork.Models.NeuObject no in p)
            {
                pStr = pStr + no.Name.ToString() + "$";
            }
            return this.QueryDataBySqlId(sqlId, ref dt, pStr.Split('$'));
        }

        #region   物资使用

        public ArrayList GetAllMonthly(string storage)
        {
            string str = "select t.ms_code,t.oper_date from mat_com_msdept t" +
            " where t.storage_code='{0}' order by t.oper_date desc";
            ArrayList montyly = new ArrayList();
            str = string.Format(str, storage);
            this.ExecQuery(str);
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = this.Reader[0].ToString();
                obj.Name = this.Reader[1].ToString();
                montyly.Add(obj);
            }
            return montyly;
        }

        //获取所有分类

        //public ArrayList GetAllKind(string storageCode)
        //{
        //    ArrayList kindList = new ArrayList();
        //    string strSql = "select t.kind_code,t.kind_name from mat_com_kindinfo t" +
        //    " where (t.storage_code='{0}' or '{0}'='ALL')  order by t.kind_code";
        //    strSql = string.Format(strSql, storageCode);
        //    this.ExecQuery(strSql);
        //    while (this.Reader.Read())
        //    {
        //        FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
        //        obj.ID = this.Reader[0].ToString();
        //        obj.Name = this.Reader[1].ToString();
        //        kindList.Add(obj);
        //    }
        //    return kindList;
        //}

        ////查找仓库
        //public ArrayList GetStoreDept()
        //{
        //    ArrayList deptList = new ArrayList();
        //    string strSql = "select t.dept_code,t.dept_name,t.spell_code,t.wb_code" +
        //    " from COM_DEPTSTAT t" + " where t.pardep_code='9006' and t.stat_code='55'";
        //    this.ExecQuery(strSql);
        //    while (this.Reader.Read())
        //    {
        //        FS.HISFC.Models.Base.Department obj = new FS.HISFC.Models.Base.Department();
        //        obj.ID = this.Reader[0].ToString();
        //        obj.Name = this.Reader[1].ToString();
        //        obj.SpellCode = this.Reader[2].ToString();
        //        obj.WBCode = this.Reader[3].ToString();
        //        deptList.Add(obj);
        //    }
        //    return deptList;

        //}
        ////查找供货商
        //public ArrayList GetCompany()
        //{
        //    string strSql = "select t.company_code,t.company_name,t.spell_code,t.wb_code,t.custom_code" +
        //    " from mat_com_company t" + " where t.valid_flag='1'";
        //    ArrayList companyList = new ArrayList();
        //    this.ExecQuery(strSql);
        //    while (this.Reader.Read())
        //    {
        //        //FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
        //        MatCompany obj = new MatCompany();
        //        obj.ID = this.Reader[0].ToString();
        //        obj.Name = this.Reader[1].ToString();
        //        obj.SpellCode = this.Reader[2].ToString();
        //        obj.WBCode = this.Reader[3].ToString();
        //        obj.UserCode = this.Reader[4].ToString();
        //        companyList.Add(obj);
        //    }
        //    return companyList;
        //}

        ////查找所有科室
        //public ArrayList GetAllDept()
        //{
        //    ArrayList deptList = new ArrayList();
        //    string strSql = "select t.dept_code,t.dept_name,t.spell_code,t.wb_code" +
        //     " from com_department t order by t.dept_code";
        //    this.ExecQuery(strSql);
        //    while (this.Reader.Read())
        //    {
        //        FS.HISFC.Models.Base.Department obj = new FS.HISFC.Models.Base.Department();
        //        obj.ID = this.Reader[0].ToString();
        //        obj.Name = this.Reader[1].ToString();
        //        obj.SpellCode = this.Reader[2].ToString();
        //        obj.WBCode = this.Reader[3].ToString();
        //        deptList.Add(obj);
        //    }
        //    return deptList;
        //}

        ////获取入库记录
        //public ArrayList GetInput(string fromDate, string endDate, string kindCode, string storageCode)
        //{
        //    string strSql = "select p.company_name,sum(t.in_num),sum(t.in_cost)" +
        //    " from mat_com_input t,mat_com_company p" +
        //    " where p.company_code=t.company_code" +
        //    " and t.in_date>=to_date('{0}','yyyy-mm-dd hh24:mi:ss')" +
        //    " and t.in_date<=to_date('{1}','yyyy-mm-dd hh24:mi:ss')" +
        //    " and t.kind_code='{2}'" +
        //    " and (t.storage_code='{3}' or 'ALL'='{3}')" +
        //    " group by p.company_name" +
        //    " order by p.company_name";
        //    ArrayList inputList = new ArrayList();
        //    strSql = string.Format(strSql, fromDate, endDate, kindCode, storageCode);
        //    this.ExecQuery(strSql);
        //    while (this.Reader.Read())
        //    {
        //        string[] param = new string[3];
        //        param[0] = this.Reader[0].ToString();
        //        param[1] = this.Reader[1].ToString();
        //        param[2] = this.Reader[2].ToString();
        //        inputList.Add(param);
        //    }
        //    return inputList;
        //}

        ////获取对应出库记录

        //public ArrayList GetOut(string fromDate, string endDate, string kindCode, string storageCode)
        //{
        //    string strSql = "select c.dept_name,sum(m.out_num),sum(m.out_cost)" +
        //    " from mat_com_output m,com_department c" +
        //    " where m.stock_code in(" +
        //    " select p.stock_code from mat_com_stockdetail p" +
        //    " where p.in_no in(select t.in_no" +
        //    " from mat_com_input t" +
        //    " where (t.storage_code='{3}' or 'ALL'='{3}')" +
        //    " and t.in_date>=to_date('{0}','yyyy-mm-dd hh24:mi:ss')" +
        //    " and t.in_date<=to_date('{1}','yyyy-mm-dd hh24:mi:ss')" +
        //    " and t.kind_code='{2}'))" +
        //    " and m.out_date>=to_date('{0}','yyyy-mm-dd hh24:mi:ss')" +
        //    " and m.out_date<=to_date('{1}','yyyy-mm-dd hh24:mi:ss')" +
        //    " and (m.storage_code='{3}' or 'ALL'='{3}')" +
        //    " and m.target_dept=c.dept_code" +
        //    " group by c.dept_name" +
        //    " order by c.dept_name";

        //    strSql = string.Format(strSql, fromDate, endDate, kindCode, storageCode);
        //    this.ExecQuery(strSql);

        //    ArrayList outPutList = new ArrayList();
        //    while (this.Reader.Read())
        //    {
        //        string[] param = new string[3];
        //        param[0] = this.Reader[0].ToString();
        //        param[1] = this.Reader[1].ToString();
        //        param[2] = this.Reader[2].ToString();
        //        outPutList.Add(param);
        //    }
        //    return outPutList;
        //}

        #endregion
    }
}
