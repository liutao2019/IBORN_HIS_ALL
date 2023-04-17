using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizLogic.Registration
{
    public class RegDetail:FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 业务层统一的获取sql方法
        /// </summary>
        /// <param name="index">sql的ID</param>
        /// <param name="SQL">SQL</param>
        /// <returns>-1失败</returns>
        public int GetSQL(string index, ref string SQL)
        {
            return this.Sql.GetSql(index, "COM_SQL", ref SQL);
        }

        /// <summary>
        /// 将sql缓存
        /// </summary>
        /// <param name="index">sql的ID</param>
        /// <param name="SQL">SQL</param>
        /// <returns>-1失败</returns>
        public int CacheSQL(string index, string SQL)
        {
            return this.Sql.CacheSql(index, SQL);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Insert(HISFC.Models.Registration.RegisterFeeDetail model)
        {
            //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
            string Sql = "";
            HISFC.Models.Base.Employee employee = FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
            if (employee == null)
            {
                employee = new FS.HISFC.Models.Base.Employee();
            }
            HISFC.Models.Base.Department dept = employee.Dept as HISFC.Models.Base.Department;
            if (dept == null)
            {
                dept = new FS.HISFC.Models.Base.Department();
            }
            string hospital_id = dept.HospitalID;
            string hospital_name = dept.HospitalName;
            //{6C71C55A-6162-4bc3-B3E8-902640CCD869}
            if(string.IsNullOrEmpty(model.Hospital_id))
            {
            model.Hospital_id = hospital_id;
            model.Hospital_name = hospital_name;
            }
            if (this.Sql.GetCommonSql("Registration.RegisterFeeDetail.Insert", ref Sql) == -1)
            {
                this.Err = "没有找到Registration.RegisterFeeDetail.Insert";
                return -1;
            }
            try
            {
                Sql = string.Format(Sql, this.GetParameters(model));
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(Sql);
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        public int Update(HISFC.Models.Registration.RegisterFeeDetail model)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("Registration.RegisterFeeDetail.Update", ref Sql) == -1)
            {
                this.Err = "没有找到Registration.RegisterFeeDetail.Update";
                return -1;
            }

            try
            {
                Sql = string.Format(Sql, this.GetParameters(model));
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(Sql);
        }

        public List<FS.HISFC.Models.Registration.RegisterFeeDetail> QueryDetailByInvoiceNO(string InvoiceNO)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("Registration.RegisterFeeDetail.Select", ref Sql) == -1)
            {
                this.Err = "没有找到Registration.RegisterFeeDetail.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("Registration.RegisterFeeDetail.Select.Where1", ref Where) == -1)
            {
                this.Err = "没有找到Registration.RegisterFeeDetail.Select.Where1";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, InvoiceNO);
                return this.GetObjets(Sql);
            }
            catch 
            {
                return null;
            }
        }
            
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private List<FS.HISFC.Models.Registration.RegisterFeeDetail> GetObjets(string sql)
        {
            if (this.ExecQuery(sql) < 0) return null;

            List<FS.HISFC.Models.Registration.RegisterFeeDetail> detailList = new List<FS.HISFC.Models.Registration.RegisterFeeDetail>();

            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Registration.RegisterFeeDetail model = new FS.HISFC.Models.Registration.RegisterFeeDetail();
                    model.ClinicNO = this.Reader[0].ToString();
                    model.SequenceNO = this.Reader[1].ToString();
                    model.TransType = (this.Reader[2].ToString() == "1") ? FS.HISFC.Models.Base.TransTypes.Positive : FS.HISFC.Models.Base.TransTypes.Negative;
                    model.CardNo = this.Reader[3].ToString();
                    model.MarkType.ID = this.Reader[4].ToString();
                    model.MarkNO = this.Reader[5].ToString();
                    if (model.FeeItem == null)
                    {
                        model.FeeItem = new FS.HISFC.Models.Base.Item();
                    }
                    model.FeeItem.ID = this.Reader[6].ToString();
                    model.FeeItem.Name = this.Reader[7].ToString();
                    model.FeeItem.Qty = Decimal.Parse(this.Reader[8].ToString());
                    model.Tot_cost = Decimal.Parse(this.Reader[9].ToString());
                    model.Real_cost = Decimal.Parse(this.Reader[10].ToString());
                    model.Gift_cost = Decimal.Parse(this.Reader[11].ToString());
                    model.Etc_cost = Decimal.Parse(this.Reader[12].ToString());
                    model.Print_InvoiceNo = this.Reader[13].ToString();
                    model.InvoiceNo = this.Reader[14].ToString();
                    model.Memo = this.Reader[15].ToString();
                    model.IsBalance = this.Reader[16].ToString() == "1";
                    model.BalanceNo = this.Reader[17].ToString();
                    model.BalanceOper.ID = this.Reader[18].ToString();
                    model.BalanceOper.OperTime = DateTime.Parse(this.Reader[19].ToString());
                    model.Oper.ID = this.Reader[20].ToString();
                    model.Oper.OperTime = DateTime.Parse(this.Reader[21].ToString());
                    model.CancelFlag = Int32.Parse(this.Reader[22].ToString());
                    model.CancelOper.ID = this.Reader[23].ToString();
                    model.CancelOper.OperTime = DateTime.Parse(this.Reader[24].ToString());
                    model.Hospital_id = this.Reader[25].ToString();
                    model.Hospital_name = this.Reader[26].ToString(); // {3515892E-1541-47de-8E0B-E306798A358C}
                    detailList.Add(model);
                }

                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Reader.Close();
            }

            return detailList;
        }

        /// <summary>
        /// 通过实体获取参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string[] GetParameters(HISFC.Models.Registration.RegisterFeeDetail model)
        {
            return new string[]{
                model.ClinicNO,
                model.SequenceNO,
                model.TransType == FS.HISFC.Models.Base.TransTypes.Positive ? "1":"2",
                model.CardNo,
                model.MarkType.ID,
                model.MarkNO,
                model.FeeItem.ID,
                model.FeeItem.Name,
                model.Qty.ToString(),
                model.Tot_cost.ToString(),
                model.Real_cost.ToString(),
                model.Gift_cost.ToString(),
                model.Etc_cost.ToString(),
                model.Print_InvoiceNo,
                model.InvoiceNo,
                model.Memo,
                model.IsBalance?"1":"0",
                model.BalanceNo,
                model.BalanceOper.ID,
                model.BalanceOper.OperTime.ToString(),
                model.Oper.ID,
                model.Oper.OperTime.ToString(),
                model.CancelFlag.ToString(),
                model.CancelOper.ID,
                model.CancelOper.OperTime.ToString(),
                model.Hospital_id.ToString(),
                model.Hospital_name.ToString() //{3515892E-1541-47de-8E0B-E306798A358C}
            };
        }
   
    }
}
