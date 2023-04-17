using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizLogic.MedicalPackage.Fee
{
    public class Invoice : Base.DataBase
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Insert(FS.HISFC.Models.MedicalPackage.Fee.Invoice model)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Invoice.Insert", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Invoice.Insert";
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
        public int Update(FS.HISFC.Models.MedicalPackage.Fee.Invoice model)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Invoice.Update", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Invoice.Update";
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
        /// 删除一条数据
        /// </summary>
        public int Delete(FS.HISFC.Models.MedicalPackage.Fee.Invoice model)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Invoice.Delete", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Invoice.Delete";
                return -1;
            }

            try
            {
                Sql = string.Format(Sql, model.ID,model.Trans_Type);
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
        /// 获取一个新的流水号
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        public string GetNewInvoiceSeq()
        {
            return this.GetSequence("Package.Fee.GetNewInvoiceSEQ");
        }

        /// <summary>
        /// 根据发票号获取发票信息
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        public FS.HISFC.Models.MedicalPackage.Fee.Invoice QueryByInvoiceNO(string InvoiceNO,string Transtype)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Invoice.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Invoice.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Invoice.Select.Where2", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Invoice.Select.Where2";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, InvoiceNO, Transtype);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }

            ArrayList al =  this.GetObjets(Sql);

            if (al != null && al.Count > 0)
            {
                return al[0] as FS.HISFC.Models.MedicalPackage.Fee.Invoice;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 根据卡号日期有效标识查询发票
        /// </summary>
        /// <param name="CardNO"></param>
        /// <param name="Begin"></param>
        /// <param name="End"></param>
        /// <param name="CancelFlag"></param>
        /// <returns></returns>
        public ArrayList QueryInvoiceByCardNoDate(string CardNO, DateTime Begin, DateTime End, string CancelFlag)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Invoice.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Invoice.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Invoice.Select.Where1", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Invoice.Select.Where1";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, CardNO, Begin.ToString(), End.ToString(), CancelFlag);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }

            return this.GetObjets(Sql);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private ArrayList GetObjets(string sql)
        {
            if (this.ExecQuery(sql) < 0) return null;

            ArrayList InvoiceArray = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.MedicalPackage.Fee.Invoice model = new FS.HISFC.Models.MedicalPackage.Fee.Invoice();
                    model.ID = this.Reader[0].ToString();
                    model.InvoiceNO = this.Reader[0].ToString();
                    model.Trans_Type = this.Reader[1].ToString();
                    model.Paykindcode = this.Reader[2].ToString();
                    model.Card_Level = this.Reader[3].ToString();
                    model.Package_Cost = Decimal.Parse(this.Reader[4].ToString());
                    model.Real_Cost = Decimal.Parse(this.Reader[5].ToString());
                    model.Gift_cost = Decimal.Parse(this.Reader[6].ToString());
                    model.Etc_cost = Decimal.Parse(this.Reader[7].ToString());
                    model.Oper = this.Reader[8].ToString();
                    model.OperTime = DateTime.Parse(this.Reader[9].ToString());
                    model.CheckFlag = this.Reader[10].ToString();
                    model.CheckOper = this.Reader[11].ToString();
                    model.CheckTime = DateTime.Parse(this.Reader[12].ToString());
                    model.BalanceFlag = this.Reader[13].ToString();
                    model.BalanceNO = this.Reader[14].ToString();
                    model.BalanceOper = this.Reader[15].ToString();
                    if (!string.IsNullOrEmpty(this.Reader[16].ToString()))
                    {
                        model.BalanceTime = DateTime.Parse(this.Reader[16].ToString());
                    }
                    model.CorrectFlag = this.Reader[17].ToString();
                    model.CorrectOper = this.Reader[18].ToString();
                    model.CorrectTime = DateTime.Parse(this.Reader[19].ToString());
                    model.Cancel_Flag = this.Reader[20].ToString();
                    model.CancelOper = this.Reader[21].ToString();
                    model.CancelTime = DateTime.Parse(this.Reader[22].ToString());
                    model.Memo = this.Reader[23].ToString(); ;
                    model.InvoiceSeq = this.Reader[24].ToString(); ;
                    model.PrintInvoiceNO = this.Reader[25].ToString();
                    //  {D59EF243-868D-41a0-9827-5E2E608522CA}
                    model.Hospital_ID = this.Reader[26].ToString();
                    model.HospitalName = this.Reader[27].ToString();

                    InvoiceArray.Add(model);
                }

                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Reader.Close();
            }

            return InvoiceArray;
        }

        /// <summary>
        /// 通过实体获取参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string[] GetParameters(FS.HISFC.Models.MedicalPackage.Fee.Invoice model)
        {
            return new string[] { 
                model.InvoiceNO,
                model.Trans_Type,
                model.Paykindcode,
                model.Card_Level,
                model.Package_Cost.ToString(),
                model.Real_Cost.ToString(),
                model.Gift_cost.ToString(),
                model.Etc_cost.ToString(),
                model.Oper,
                model.OperTime.ToString(),
                model.CheckFlag,
                model.CheckOper,
                model.CheckTime.ToString(),
                model.BalanceFlag,
                model.BalanceNO,
                model.BalanceOper,
                model.BalanceTime.ToString(),
                model.CorrectFlag,
                model.CorrectOper,
                model.CorrectTime.ToString(),
                model.Cancel_Flag,
                model.CancelOper,
                model.CancelTime.ToString(),
                model.Memo,
                model.InvoiceSeq,
                model.PrintInvoiceNO,
                model.Hospital_ID,  //  {D59EF243-868D-41a0-9827-5E2E608522CA}
                model.HospitalName  //  {D59EF243-868D-41a0-9827-5E2E608522CA}
            };
        }

    }
}
