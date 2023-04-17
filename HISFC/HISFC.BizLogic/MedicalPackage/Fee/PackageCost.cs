using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizLogic.MedicalPackage.Fee
{
    public class PackageCost : Base.DataBase
    {
        /// <summary>
        /// 增加一条数据{351D714B-0153-483e-B1AB-697C5A9A9BAD}
        /// </summary>
        public int Insert(FS.HISFC.Models.MedicalPackage.Fee.PackageCost model)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageCost.Insert", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PackageCost.Insert";
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

        ///// <summary>
        ///// 修改一条数据
        ///// </summary>
        //public int Update(FS.HISFC.Models.MedicalPackage.Fee.PackageCost model)
        //{
        //    string Sql = "";

        //    if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageCost.Update", ref Sql) == -1)
        //    {
        //        this.Err = "没有找到MedicalPackage.Package.Fee.PackageCost.Update";
        //        return -1;
        //    }

        //    try
        //    {
        //        Sql = string.Format(Sql, this.GetParameters(model));
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = ex.Message;
        //        this.ErrCode = ex.Message;
        //        return -1;
        //    }
        //    return this.ExecNoQuery(Sql);
        //}

        /// <summary>
        /// 修改一条数据{351D714B-0153-483e-B1AB-697C5A9A9BAD}
        /// </summary>
        public int UpdateByCostType(FS.HISFC.Models.MedicalPackage.Fee.PackageCost model)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageCost.UpdateByType", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PackageCost.UpdateByType";
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
        /// 修改一条数据{351D714B-0153-483e-B1AB-697C5A9A9BAD}
        /// {14B9C3EE-70B6-46df-B279-B8F3487519C4}
        /// </summary>
        public int UpdateByCostTypeForCrmCostId(FS.HISFC.Models.MedicalPackage.Fee.PackageCost model)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageCost.UpdateByTypeByCrmCost", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PackageCost.UpdateByTypeByCrmCost";
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
        /// 获取一个新的流水号{6974FE57-7E0F-4c8f-AFC8-675CA7536C61}{351D714B-0153-483e-B1AB-697C5A9A9BAD}
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        public string GetNewCostid()
        {
            return this.GetSequence("PackageCost.Fee.GetNewCostid");
        }


        /// <summary>
        /// 查询消费记录{351D714B-0153-483e-B1AB-697C5A9A9BAD}
        /// </summary>
        //public ArrayList QueryByInvoiceNO(string InvoiceNO,string CancelFlag)
        //{
        //    string Sql = "";
        //    string Where = "";

        //    if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageCost.Select", ref Sql) == -1)
        //    {
        //        this.Err = "没有找到MedicalPackage.Package.Fee.PackageCost.Select";
        //        return null;
        //    }

        //    if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageCost.Select.Where1", ref Where) == -1)
        //    {
        //        this.Err = "没有找到MedicalPackage.Package.Fee.PackageCost.Select.Where1";
        //        return null;
        //    }

        //    try
        //    {
        //        Sql += Where;
        //        Sql = string.Format(Sql, InvoiceNO, CancelFlag);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = ex.Message;
        //        this.ErrCode = ex.Message;
        //        return null;
        //    }

        //    return this.GetObjets(Sql);
        //}

        /// <summary>
        /// 查询消费记录按消费类型区分{351D714B-0153-483e-B1AB-697C5A9A9BAD}
        /// </summary>
        public ArrayList QueryByInvoiceNOByType(string InvoiceNO, string CancelFlag,string costtype)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageCost.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PackageCost.Select";
                return null;
            }
            if (costtype == "ZY")
            {
                if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageCost.Select.WhereZY", ref Where) == -1)
                {
                    this.Err = "没有找到MedicalPackage.Package.Fee.PackageCost.Select.Where1";
                    return null;
                }
            }
            else if (costtype == "MZ")
            {
                if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageCost.Select.WhereMZ", ref Where) == -1)
                {
                    this.Err = "没有找到MedicalPackage.Package.Fee.PackageCost.Select.Where1";
                    return null;
                }
            }
            else
            {
                if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageCost.Select.Where1", ref Where) == -1)
                {
                    this.Err = "没有找到MedicalPackage.Package.Fee.PackageCost.Select.Where1";
                    return null;
                }
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
        /// 查询消费记录主键
        /// </summary>
        public ArrayList QueryByID(string clincode, string seq)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageCost.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PackageCost.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageCost.Select.WhereByIdAndSeq", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PackageCost.Select.WhereByIdAndSeq";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, clincode, seq);
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
        /// 获取实体{351D714B-0153-483e-B1AB-697C5A9A9BAD}//{EC52C67F-A234-4ef6-824E-34DF69778A6A}
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private ArrayList GetObjets(string sql)
        {
            if (this.ExecQuery(sql) < 0) return null;

            ArrayList costArray = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.MedicalPackage.Fee.PackageCost model = new FS.HISFC.Models.MedicalPackage.Fee.PackageCost();
                    model.InvoiceNO = this.Reader[0].ToString();
                    model.SequenceNO = this.Reader[1].ToString();
                    model.Trans_Type = this.Reader[2].ToString();
                    model.PackageClinic = this.Reader[3].ToString();
                    model.DetailSeq = this.Reader[4].ToString();
                    model.Amount = Decimal.Parse(this.Reader[5].ToString());
                    model.Unit = this.Reader[6].ToString();
                    model.Tot_Cost = Decimal.Parse(this.Reader[7].ToString());
                    model.Real_Cost = Decimal.Parse(this.Reader[8].ToString());
                    model.Gift_cost = Decimal.Parse(this.Reader[9].ToString());
                    model.Etc_cost = Decimal.Parse(this.Reader[10].ToString());
                    model.Oper = this.Reader[11].ToString();
                    model.OperTime = DateTime.Parse(this.Reader[12].ToString());
                    model.Balance_flag = this.Reader[13].ToString();
                    model.Balance_no = this.Reader[14].ToString();
                    model.BalanceOper = this.Reader[15].ToString();
                    model.BalanceTime = DateTime.Parse(this.Reader[16].ToString());
                    model.Cancel_Flag = this.Reader[17].ToString();
                    model.CancelOper = this.Reader[18].ToString();
                    model.CancelTime = DateTime.Parse(this.Reader[19].ToString());
                    model.Memo = this.Reader[20].ToString();
                    model.COSTID = this.Reader[21].ToString();
                    model.COSTCLINIC = this.Reader[22].ToString();
                    model.CARDNO = this.Reader[23].ToString();
                    model.COST_TYPE = this.Reader[24].ToString();
                    model.ITEM_CODE = this.Reader[25].ToString();
                    model.HOSPITAL_ID = this.Reader[26].ToString();
                    model.HOSPITAL_NAME = this.Reader[27].ToString(); //{6974FE57-7E0F-4c8f-AFC8-675CA7536C61}
                    model.Has_Card_NO = this.Reader[28].ToString();
                    model.Use_Card_NO = this.Reader[29].ToString();
                    model.NewPackageClinic = this.Reader[30].ToString();
                    model.NewDetailSeq = this.Reader[31].ToString();
                    costArray.Add(model);
                }

                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Reader.Close();
            }

            return costArray;
        }

         /// <summary>
        /// 通过实体获取参数{351D714B-0153-483e-B1AB-697C5A9A9BAD}//{EC52C67F-A234-4ef6-824E-34DF69778A6A}
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string[] GetParameters(FS.HISFC.Models.MedicalPackage.Fee.PackageCost model)
        {
            return new string[]{
                model.InvoiceNO,
                model.SequenceNO,
                model.Trans_Type,
                model.PackageClinic,
                model.DetailSeq,
                model.Amount.ToString(),
                model.Unit,
                model.Tot_Cost.ToString(),
                model.Real_Cost.ToString(),
                model.Gift_cost.ToString(),
                model.Etc_cost.ToString(),
                model.Oper,
                model.OperTime.ToString(),
                model.Balance_flag,
                model.Balance_no,
                model.BalanceOper,
                model.BalanceTime.ToString(),
                model.Cancel_Flag,
                model.CancelOper,
                model.CancelTime.ToString(),
                model.Memo,
                //{DD31280F-7321-42BB-B150-4C63018ED85F}
                model .Has_Card_NO,
                model .Use_Card_NO,
                model.COSTID,
                model.COSTCLINIC,
                model.CARDNO,
                model.COST_TYPE,
                model.ITEM_CODE,
                model.HOSPITAL_ID,
                model.HOSPITAL_NAME,//{6974FE57-7E0F-4c8f-AFC8-675CA7536C61}
                model.NewPackageClinic,
                model.NewDetailSeq

            };
        }
    }
}
