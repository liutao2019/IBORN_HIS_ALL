using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using FS.HISFC.Models.MedicalPackage;

namespace FS.HISFC.BizLogic.MedicalPackage.Fee
{
    public class Package : Base.DataBase
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Insert(FS.HISFC.Models.MedicalPackage.Fee.Package model)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.Insert", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.Insert";
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
        public int Update(FS.HISFC.Models.MedicalPackage.Fee.Package model)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.Update", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.Update";
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
        public int Delete(FS.HISFC.Models.MedicalPackage.Fee.Package model)
        {
            return this.DeleteByID(model.ID, model.Trans_Type);
        }

        /// <summary>
        /// 根据ID删除记录
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        public int DeleteByID(string ClinicCode, string Trans_Type)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.Delete", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.Delete";
                return -1;
            }

            try
            {
                Sql = string.Format(Sql, ClinicCode, Trans_Type);
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
        /// 根据ID删除记录
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        public int DeleteByRecipe(string RecipeNO)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.DeleteByRecipeNO", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.DeleteByRecipeNO";
                return -1;
            }

            try
            {
                Sql = string.Format(Sql, RecipeNO);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(Sql);
        }


        public int UpdatePackageCostFlag(string clinincIDs)
        {
            string sql = "     update exp_package xp set xp.cost_flag='{0}' where xp.clinic_code in ({1}) ";
            sql = string.Format(sql, "1", clinincIDs);
            int i = -1;
            try
            {
                i=this.ExecNoQuery(sql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return i;
        }
        //{EC52C67F-A234-4ef6-824E-34DF69778A6A}
        public int UpdatePackageCostFlag2(string clinincIDs,string cost_flag)
        {
            string sql = "     update exp_package xp set xp.cost_flag='{0}' where xp.clinic_code in ({1}) ";
            sql = string.Format(sql, cost_flag, clinincIDs);
            int i = -1;
            try
            {
                i = this.ExecNoQuery(sql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return i;
        }

        /// <summary>
        /// 修改一条数据{3599D82C-0E7E-4628-8FC6-DDAAA1CC6335}
        /// </summary>
        public int ShiftPackageOwner(FS.HISFC.Models.RADT.PatientInfo PatientInfo, FS.HISFC.Models.RADT.PatientInfo PatientInfoNew, FS.HISFC.Models.MedicalPackage.Fee.Invoice invoiceInfo,string operCode)
        {
            #region sql
            /**
             * update EXP_PACKAGE set patient_name ='{2}',card_no='{3}',oper_code='{4}',oper_date=sysdate 
                where INVOICE_NO='{0}' and card_no='{1}'
             * */
	        #endregion
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.ShiftPackageOwner", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.ShiftPackageOwner";
                return -1;
            }

            try
            {
                Sql = string.Format(Sql, invoiceInfo.ID, PatientInfo.PID.CardNO, PatientInfoNew.Name, PatientInfoNew.PID.CardNO);
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
        /// 更新操作人{0F7B065C-F9A2-48e7-A331-F9DC0EC64D3A}
        /// </summary>
        public int ShiftPackageOwnerOper(FS.HISFC.Models.RADT.PatientInfo PatientInfo, FS.HISFC.Models.RADT.PatientInfo PatientInfoNew, FS.HISFC.Models.MedicalPackage.Fee.Invoice invoiceInfo, string operCode,string operName)
        {
            #region sql
            /**
             * update exp_package_shift set oper_id='{2}',oper_name='{3}' where  INVOICE_NO='{0}' and card_no='{1}' and oper_id is null
             * */
            #endregion
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.ShiftPackageOwner.oper", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.ShiftPackageOwner.oper";
                return -1;
            }

            try
            {
                Sql = string.Format(Sql, invoiceInfo.ID, PatientInfo.PID.CardNO, operCode, operName);
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
        public string GetNewClinicNO()
        {
            return this.GetSequence("Package.Fee.GetNewClinicNO");
        }

        /// <summary>
        /// 获取新的划价单号
        /// </summary>
        /// <returns></returns>
        public string GetNewRecipeNO()
        {
            return this.GetSequence("Package.Fee.GetRecipeNO");
        }

        /// <summary>
        /// 通过流水号查询
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        public FS.HISFC.Models.MedicalPackage.Fee.Package QueryByID(string ClinicCode)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.Select.Where2", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.Select.Where2";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, ClinicCode);
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
                return al[0] as FS.HISFC.Models.MedicalPackage.Fee.Package;
            }

            return null;
        }

        /// <summary>
        /// 通过消费发票号查询未退费的套餐{351D714B-0153-483e-B1AB-697C5A9A9BAD}
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        public ArrayList QueryByCostInvoiceNoNoRtn(string CostInvoieNo)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.Select.Where6", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.Select.Where6";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, CostInvoieNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }

            ArrayList al = this.GetObjets(Sql);

            return al;
        }


        /// <summary>
        /// 通过划价单查询费用记录
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        public ArrayList QueryByRecipeNO(string RecipeNO,string Trans_Type)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.Select.Where1", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.Select.Where1";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, RecipeNO, Trans_Type);
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
        /// 通过划价单查询费用记录
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        public ArrayList QueryByInvoiceNO(string Invoice, string CancelFlag)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.Select.Where3", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.Select.Where3";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, Invoice, CancelFlag);
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
        /// {84D474E6-6590-472b-9D6C-8DC90000A16C} 
        /// 通过病历号单询费用记录  
        /// 更改挂号套餐查询只能查询到未使用的套餐
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        public ArrayList QueryByCardNO(string CardNO, string Pay_flag, string CancelFlag)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.Select.Where4", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.Select.Where4";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, CardNO, Pay_flag, CancelFlag);
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
        /// 只取未使用完的套餐{e87b8fc3-e03c-43eb-be1a-97473bc93ebb}
        /// </summary>
        /// <param name="CardNO"></param>
        /// <returns></returns>
        public ArrayList QueryUnusedByCardNO(string CardNO)
        {
            string Sql = "";


            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.SelectUnused", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.SelectUnused";
                return null;
            }

            try
            {
                Sql = string.Format(Sql, CardNO);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }

            return this.GetObjets(Sql);

        }


        public ArrayList QueryByCardNO2(string CardNO, string Pay_flag, string CancelFlag)
        {
            string Sql = "";
            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.Select3", ref Sql) == -1)
            {
                //{01C202AF-5D8A-4d89-9BA5-1910B5AA7607} 
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.Select3";
                return null;
            }

            try
            {
               
                Sql = string.Format(Sql, CardNO);
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
        /// 通过病历号单询费用记录  727B4075-258A-4F11-A757-D8F0AEF08380
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        public ArrayList QueryByCardNO1(string CardNO, string Pay_flag, string CancelFlag)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.Select2", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.Select2";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.Select.Where5", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.Select.Where5";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, CardNO, Pay_flag, CancelFlag);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }

            return this.GetObjets(Sql);
        }

        //{2E41B9BF-6B67-4b56-BD54-A836CE09F52B}
        public int GetPackageRegisterCount(string CardNO)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.AvaliableRegFee", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.AvaliableRegFee";
                return -1;
            }

            try
            {
                Sql = string.Format(Sql, CardNO);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            int rtn = 0;

            try
            {
                rtn = Int32.Parse(this.ExecSqlReturnOne(Sql));
            }
            catch
            {
                rtn = -1;
            }

            return rtn;
        }

        /// <summary>
        /// 通过病人信息查找划价单列表
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="Trans_Type"></param>
        /// <returns></returns>
        public ArrayList GetRecipesByPatient(FS.HISFC.Models.RADT.PatientInfo patient,string Trans_Type)
        {
            return this.QueryRecipesByCardNO(patient.PID.CardNO,Trans_Type);
        }

        /// <summary>
        /// 通过病历号查找划价单列表
        /// </summary>
        /// <param name="CardNO"></param>
        /// <returns></returns>
        public ArrayList QueryRecipesByCardNO(string CardNO, string Trans_Type)
        {
            ArrayList Recipes = new ArrayList();
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.SelectRecipes", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.SelectRecipes";
                return null;
            }

            try
            {
                Sql = string.Format(Sql, CardNO, Trans_Type);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }

            this.ExecQuery(Sql);

            try
            {
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject model = new FS.FrameWork.Models.NeuObject();
                    model.ID = this.Reader[0].ToString();
                    model.Name = this.Reader[1].ToString();
                    model.User01 = this.Reader[2].ToString();
                    model.User02 = this.Reader[3].ToString();
                    model.User03 = this.Reader[4].ToString();
                    model.Memo = this.Reader[5].ToString();
                    Recipes.Add(model);
                }

                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Reader.Close();
                return null;
            }

            return Recipes;
        }

        /// <summary>
        /// 根据划价单更新付款标志
        /// </summary>
        /// <param name="RecipeNO"></param>
        /// <param name="PayFlag"></param>
        /// <returns></returns>
        public int UpdatePayFlagByRecipeNO(string RecipeNO, bool PayFlag)
        {
            return 1;
        }

        /// <summary>
        /// {7C8E0BBA-04CB-4457-9399-39A25804EA12}
        /// {56809DCA-CD5A-435e-86F0-93DE99227DF4}
        /// 根据套餐ID更改特殊折扣标记
        /// </summary>
        /// <param name="RecipeNO"></param>
        /// <param name="PayFlag"></param>
        /// <returns></returns>
        public int UpdateSpecialFlagByID(string clinicCode,string specialFlag)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Update.Paykind", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.Select";
                return -1;
            }

            Sql = string.Format(Sql, clinicCode, specialFlag);

            try
            {
                return this.ExecNoQuery(Sql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
        }

        /// <summary>
        ///查询,  获取实体,注意MedicalPackage.Package.Fee.Package.Select2查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        
        private ArrayList GetObjets(string sql)
        {
            if (this.ExecQuery(sql) < 0) return null;

            ArrayList packageArray = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.MedicalPackage.Fee.Package model = new FS.HISFC.Models.MedicalPackage.Fee.Package();
                    model.ID = this.Reader[0].ToString();
                    model.Trans_Type = this.Reader[1].ToString();

                    if (model.PackageInfo == null)
                    {
                        model.PackageInfo = new FS.HISFC.Models.MedicalPackage.Package();
                    }
                    model.PackageInfo.ID = this.Reader[2].ToString();
                    model.Pay_Flag = this.Reader[3].ToString();
                    if (model.Patient == null)
                    {
                        model.Patient = new FS.HISFC.Models.RADT.Patient();
                    }
                    model.Patient.PID.CardNO = this.Reader[4].ToString();
                    model.Patient.Name = this.Reader[5].ToString();
                    model.Patient.Sex.ID = this.Reader[6].ToString();
                    model.Patient.Birthday = DateTime.Parse(this.Reader[7].ToString());
                    model.Card_Level = model.Patient.Pact.User01 = this.Reader[8].ToString();
                    //model.Patient.Pact.ID = this.Reader[9].ToString();
                    //{A777B7DF-AB62-4603-A0F6-B3643AD442F0}
                    //{56809DCA-CD5A-435e-86F0-93DE99227DF4}
                    //model.PayKind_Code = this.Reader[9].ToString();
                    model.SpecialFlag = this.Reader[9].ToString();
                    model.Package_Dept = this.Reader[10].ToString();
                    model.Consultant = this.Reader[11].ToString();
                    model.DelimitOper = this.Reader[12].ToString();
                    model.DelimitTime = DateTime.Parse(this.Reader[13].ToString());
                    model.Package_Cost = Decimal.Parse(this.Reader[14].ToString());
                    model.Real_Cost = Decimal.Parse(this.Reader[15].ToString());
                    model.Gift_cost = Decimal.Parse(this.Reader[16].ToString());
                    model.Etc_cost = Decimal.Parse(this.Reader[17].ToString());
                    model.Invoiceseq = this.Reader[18].ToString();
                    model.InvoiceNO = this.Reader[19].ToString();
                    model.Oper = this.Reader[20].ToString();
                    model.OperTime = DateTime.Parse(this.Reader[21].ToString());
                    model.Cancel_Flag = this.Reader[22].ToString();
                    model.CancelOper = this.Reader[23].ToString();
                    model.CancelTime = DateTime.Parse(this.Reader[24].ToString());
                    model.Original_Code = this.Reader[25].ToString();
                    model.Memo = this.Reader[26].ToString();
                    model.RecipeNO = this.Reader[27].ToString();
                    model.SequenceNO = this.Reader[28].ToString();
                    //{2FB3E463-C00D-4ff4-974A-5D7AA0DB9CA0}
                    model.Cost_Flag = this.Reader[29].ToString();
                    model.Cost_Invoice = this.Reader[30].ToString();
                    model.PackageInfo.Name = this.Reader[31].ToString();          //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}
                    model.PackageSequenceNO = this.Reader[32].ToString();         //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}
                    model.ParentPackageInfo.ID = this.Reader[33].ToString();      //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}
                    model.ParentPackageInfo.Name = this.Reader[34].ToString();    //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}

                    try      //旧套餐购买记录中 数量为空 会触发异常
                    {
                        model.PackageNum = Int32.Parse(this.Reader[35].ToString());    //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}

                    }
                    catch
                    {
                        model.PackageNum = 1;
                    }
                    // {01C202AF-5D8A-4d89-9BA5-1910B5AA7607} 
                    model.HospitalID = this.Reader[36].ToString();
                    model.HospitalName = this.Reader[37].ToString();
                    //{C4AD0122-ACE7-40EC-8759-529345575AF1}添加列数量判断
                    if (Reader.FieldCount > 38)
                    {
                        if (!Reader.IsDBNull(38)) model.PackageInfo.PackageType = this.Reader[38].ToString();

                    }



                    // model.PackageInfo.PackageType

                   
               
                    packageArray.Add(model);
                }

                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Reader.Close();
            }

            return packageArray;
        }
        /// <summary>
        /// 通过实体获取参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string[] GetParameters(FS.HISFC.Models.MedicalPackage.Fee.Package model)
        {
            return new string[]{
                model.ID,
                model.Trans_Type,
                model.PackageInfo.ID,
                model.Pay_Flag,
                model.Patient.PID.CardNO,
                model.Patient.Name,
                model.Patient.Sex.ID.ToString(),
                model.Patient.Birthday.ToString(),
                model.Patient.Pact.User01,  //会员等级
                //model.PayKind_Code, //合同单位 //{A777B7DF-AB62-4603-A0F6-B3643AD442F0}
                model.SpecialFlag, //特殊折扣标记 //{56809DCA-CD5A-435e-86F0-93DE99227DF4}
                model.Patient.Pact.User03,  //套餐科室
                model.Patient.Pact.User02,  //跟进人员
                model.DelimitOper,
                model.DelimitTime.ToString(),
                model.Package_Cost.ToString(),
                model.Real_Cost.ToString(),
                model.Gift_cost.ToString(),
                model.Etc_cost.ToString(),
                model.Invoiceseq,
                model.InvoiceNO,
                model.Oper,
                model.OperTime.ToString(),
                model.Cancel_Flag,
                model.CancelOper,
                model.CancelTime.ToString(),
                model.Original_Code,
                model.Memo,
                model.RecipeNO,
                model.SequenceNO,
                model.Cost_Flag,
                model.Cost_Invoice,
                model.PackageInfo.Name,          //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}
                model.PackageSequenceNO,         //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}
                model.ParentPackageInfo.ID,      //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}
                model.ParentPackageInfo.Name,    //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}
                model.PackageNum.ToString(),
                model.HospitalID,//{01C202AF-5D8A-4d89-9BA5-1910B5AA7607} 
                model.HospitalName//{01C202AF-5D8A-4d89-9BA5-1910B5AA7607} 
            };
        }


        /// <summary>
        /// 查询可用的套餐
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="serviceType">C：门诊；I：住院</param>
        /// <returns></returns>
        public ArrayList QueryAvailablePackage(string cardNO, string serviceType)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.Where.Available", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.Where.Available";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, cardNO, serviceType);
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
        /// 查询家庭套餐可用的套餐{DD31280F-7321-42BB-B150-4C63018ED85F}
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="serviceType">C：门诊；I：住院</param>
        /// <returns></returns>
        public ArrayList QueryAvailablePackage1(string cardNO, string serviceType)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.Where.Available1", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.Where.Available1";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, cardNO, serviceType);
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
        /// 查询套餐标记{74958B4A-AD55-4775-BE30-E030DDC47A64}
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public PackageTag QueryPakcage(string cardNO)
        {
            PackageTag model = null;
            string Sql = "";
            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.Select1", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.Select1";
                return null;
            }

            try
            {
                Sql = string.Format(Sql, cardNO);
                this.ExecQuery(Sql);

               
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    model = new PackageTag();
                    model.CardNO = this.Reader[0].ToString();
                    model.PackageNames = this.Reader[1].ToString();
                    model.PackageIDs = this.Reader[2].ToString();
                }

                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Reader.Close();
                return null;
            }
            return model;
        }


        /// <summary>
        /// 查询家庭成员DD31280F-7321-42BB-B150-4C63018ED85F
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ArrayList QueryFamlilyMember(string cardNO,string name)
        {
            string sql = @"   select p.card_no,p.name,p.familyname,p.familyrolename 
                                from com_patientinfo p where p.familyid = (select p.familyid from com_patientinfo p where p.card_no='{0}')";
            sql = string.Format(sql, cardNO);
            if (this.ExecQuery(sql) == -1) return null;
            //FS.FrameWork.Models.NeuObject NeuObject;
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Base.Const cons;
            while (this.Reader.Read())
            {
                //NeuObject=new NeuObject();
                cons = new FS.HISFC.Models.Base.Const();
                //cons.Type = (Const.enuConstant)(Reader[0].ToString());
                cons.ID = this.Reader[0].ToString();
                cons.Name = this.Reader[1].ToString();
                cons.Memo = this.Reader[2].ToString();
                cons.SpellCode = this.Reader[3].ToString();
               

                al.Add(cons);
            }
            this.Reader.Close();
            if (al.Count <= 0)
            {
                cons = new FS.HISFC.Models.Base.Const();
                cons.ID = cardNO;
                cons.Name = name;
                cons.Memo = name;
                cons.SpellCode = "本人";
                al.Add(cons);
            }
            return al;
           
        }

        /// <summary>
        /// 未执行的项目{048e65b9-0b30-4049-9d66-e74fbe28c2fa}
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public ArrayList QueryUnExeccutedItem(string cardNO)
        {
            /* string sql = @"select d.card_no 卡号,info.name 姓名,fun_get_dept_name(d.REG_DPCD) 开单科室,fun_get_employee_name(d.DOCT_CODE) 开单人,d.ITEM_CODE 编码,d.item_name 项目,'未执行' 状态 ,d.REG_DATE 开单时间 
                             from  fin_opb_feedetail d 
                                   left join com_patientinfo info  on d.card_no=info.card_no
                             where  exec_dpcd  in ('6003','6001')  and  confirm_flag='0' and DOCT_DEPT in ('5011','5012') and d.cancel_flag='1' and REG_DATE>to_date('2021-01-01 00:00:00','yyyy-mm-dd hh24:mi:ss') 
                                    and  d.card_no='{0}' ";*/

            string Sql = "";
            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Package.UnExeccutedItemByCardNO", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Package.UnExeccutedItemByCardNO";
                return null;
            }

            Sql = string.Format(Sql, cardNO);

            if (this.ExecQuery(Sql) == -1) return null;

            ArrayList al = new ArrayList();
            FS.HISFC.Models.MedicalPackage.Fee.PackageDetail del;

            while (this.Reader.Read())
            {
                del = new FS.HISFC.Models.MedicalPackage.Fee.PackageDetail();   //借用实体

                del.CardNO = this.Reader[0].ToString();
                del.Name = this.Reader[1].ToString();
                del.Cancel_Flag = this.Reader[2].ToString();
                del.CancelOper = this.Reader[3].ToString();
                del.Memo = this.Reader[4].ToString();
                del.Oper = this.Reader[5].ToString();
                del.PackageName = this.Reader[6].ToString();
                del.PayFlag = this.Reader[7].ToString();
                al.Add(del);
            }
            this.Reader.Close();

            return al;
        
        }



        //{A777B7DF-AB62-4603-A0F6-B3643AD442F0}
        /*public int QueryIsExistSIPackage(string cardNO)
        {
            string sql = @"
                  select count(*) from exp_package t, exp_packagedetail t2
                   where t.clinic_code = t2.clinic_code
                     and t.trans_type = t2.trans_type
                     and t.card_no = '{0}'
                     and t.cost_flag <> '1'
                     and t2.rtn_qty > 0
                     and t.cancel_flag = '0'
                     and t.pay_flag = '1'
                     and t.paykind_code = '2'";

            sql = string.Format(sql, cardNO);
            if (this.ExecQuery(sql) == -1) return -1;

            try
            {

                while (this.Reader.Read())
                {
                    int count = int.Parse(this.Reader[0].ToString());
                    return count;
                }

                return -1;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }*/

    }
}
