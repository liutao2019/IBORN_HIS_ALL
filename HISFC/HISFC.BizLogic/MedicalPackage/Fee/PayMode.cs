using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizLogic.MedicalPackage.Fee
{
    public class PayMode : Base.DataBase
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Insert(FS.HISFC.Models.MedicalPackage.Fee.PayMode model)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PayMode.Insert", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PayMode.Insert";
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
        public int Update(FS.HISFC.Models.MedicalPackage.Fee.PayMode model)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PayMode.Update", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PayMode.Update";
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
        public int Delete(FS.HISFC.Models.MedicalPackage.Fee.PayMode model)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PayMode.Delete", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PayMode.Delete";
                return -1;
            }

            try
            {
                Sql = string.Format(Sql, model.InvoiceNO, model.Trans_Type, model.SequenceNO);
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
        /// 通过发票号费用记录
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        public ArrayList QueryByInvoiceNO(string InvoiceNO, string CancelFlag)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PayMode.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PayMode.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PayMode.Select.Where1", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PayMode.Select.Where1";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, InvoiceNO, CancelFlag);
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
        /// 通过发票号以及序号查询
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        public FS.HISFC.Models.MedicalPackage.Fee.PayMode QueryByInvoiceSeq(string InvoiceNO, string Trans_type, string SequenceNO, string CancelFlag)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PayMode.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PayMode.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PayMode.Select.Where2", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PayMode.Select.Where2";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, InvoiceNO, Trans_type, SequenceNO, CancelFlag);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }

            ArrayList al = this.GetObjets(Sql);

            if (al != null && al.Count > 0)
            {
                return al[0] as FS.HISFC.Models.MedicalPackage.Fee.PayMode;
            }

            return null;
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
                    FS.HISFC.Models.MedicalPackage.Fee.PayMode model = new FS.HISFC.Models.MedicalPackage.Fee.PayMode();
                    model.InvoiceNO = this.Reader[0].ToString();
                    model.Trans_Type = this.Reader[1].ToString();
                    model.SequenceNO = this.Reader[2].ToString();
                    model.Mode_Code = this.Reader[3].ToString();
                    model.Tot_cost = Decimal.Parse(this.Reader[4].ToString());
                    model.Real_Cost = Decimal.Parse(this.Reader[5].ToString());
                    if (model.Bank == null)
                    {
                        model.Bank = new FS.FrameWork.Models.NeuObject();
                    }
                    model.Bank.ID = this.Reader[6].ToString();
                    model.Bank.Name = this.Reader[7].ToString();
                    model.Account = this.Reader[8].ToString();
                    model.AccountType = this.Reader[9].ToString();
                    model.AccountFlag = this.Reader[10].ToString();
                    model.PosNO = this.Reader[11].ToString();
                    model.CheckNO = this.Reader[12].ToString();
                    model.Oper = this.Reader[13].ToString();
                    model.OperTime = DateTime.Parse(this.Reader[14].ToString());
                    model.CheckFlag = this.Reader[15].ToString();
                    model.CheckOper = this.Reader[16].ToString();
                    model.CheckTime = DateTime.Parse(this.Reader[17].ToString());
                    model.BalanceFlag = this.Reader[18].ToString();
                    model.BalanceNO = this.Reader[19].ToString();
                    model.BalanceOper = this.Reader[20].ToString();
                    if (this.Reader[21] == null || String.IsNullOrEmpty(this.Reader[21].ToString()))
                    {
                        model.BalanceTime = DateTime.Parse("0001-01-01");
                    }
                    else
                    {
                        model.BalanceTime = DateTime.Parse(this.Reader[21].ToString());
                    }
                    //model.BalanceTime = this.Reader[21] == null ? DateTime.Parse("0001-01-01") : DateTime.Parse(this.Reader[21].ToString());//{036E68EF-9AE7-42c0-B9AD-FC6D716F1877}
                    model.CorrectFlag = this.Reader[22].ToString();
                    model.CorrectOper = this.Reader[23].ToString();
                    model.CorrectTime = DateTime.Parse(this.Reader[24].ToString());
                    model.Cancel_Flag = this.Reader[25].ToString(); ;
                    model.CancelOper = this.Reader[26].ToString(); ;
                    model.CancelTime = DateTime.Parse(this.Reader[27].ToString());
                    model.Memo = this.Reader[28].ToString();
                    model.Related_ID = this.Reader[29].ToString();
                    model.Related_ModeCode = this.Reader[30].ToString();
                    //  {D59EF243-868D-41a0-9827-5E2E608522CA} --套餐支付方式添加园区
                    model.Hospital_ID = this.Reader[31].ToString();
                    model.HospitalName = this.Reader[32].ToString();
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
        private string[] GetParameters(FS.HISFC.Models.MedicalPackage.Fee.PayMode model)
        {
            return new string[] { 
                model.InvoiceNO,
                model.Trans_Type,
                model.SequenceNO,
                model.Mode_Code,
                model.Tot_cost.ToString(),
                model.Real_Cost.ToString(),
                model.Bank.ID,
                model.Bank.Name,
                model.Account,
                model.AccountType,
                model.AccountFlag,
                model.PosNO,
                model.CheckNO,
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
                model.Related_ID,
                model.Related_ModeCode,
                // //  {D59EF243-868D-41a0-9827-5E2E608522CA}
                model.Hospital_ID,
                model.HospitalName

            };
        }
    }
}
