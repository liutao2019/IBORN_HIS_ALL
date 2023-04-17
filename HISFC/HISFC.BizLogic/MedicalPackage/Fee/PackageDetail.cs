using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizLogic.MedicalPackage.Fee
{
    public class PackageDetail : Base.DataBase
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Insert(FS.HISFC.Models.MedicalPackage.Fee.PackageDetail model)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageDetail.Insert", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PackageDetail.Insert";
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
        public int Update(FS.HISFC.Models.MedicalPackage.Fee.PackageDetail model)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageDetail.Update", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PackageDetail.Update";
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
        public int Delete(FS.HISFC.Models.MedicalPackage.Fee.PackageDetail model)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageDetail.Delete", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PackageDetail.Delete";
                return -1;
            }

            try
            {
                Sql = string.Format(Sql, model.ID, model.Trans_Type, model.SequenceNO);
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
        /// 修改一条数据{3599D82C-0E7E-4628-8FC6-DDAAA1CC6335}
        /// </summary>
        public int ShiftPackageDetailOwner(FS.HISFC.Models.RADT.PatientInfo PatientInfo, FS.HISFC.Models.RADT.PatientInfo PatientInfoNew, FS.HISFC.Models.MedicalPackage.Fee.Invoice invoiceInfo, string operCode)
        {
            #region sql
            /**
             * update EXP_PACKAGEDETAIL set card_no='{2}',oper_code='{3}',oper_date=sysdate 
                where INVOICE_NO='{0}' and card_no='{1}'
             * */
            #endregion
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageDetail.ShiftPackageOwner", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PackageDetail.ShiftPackageOwner";
                return -1;
            }

            try
            {
                Sql = string.Format(Sql, invoiceInfo.ID, PatientInfo.PID.CardNO, PatientInfoNew.PID.CardNO);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(Sql);
        }

        //更新操作人信息{0F7B065C-F9A2-48e7-A331-F9DC0EC64D3A}
        public int ShiftPackageDetailOwnerOper(FS.HISFC.Models.RADT.PatientInfo PatientInfo, FS.HISFC.Models.RADT.PatientInfo PatientInfoNew, FS.HISFC.Models.MedicalPackage.Fee.Invoice invoiceInfo, string operCode, string operName)
        {
            #region sql
            /**
             * update exp_packagedetail_shift set oper_id='{2}',oper_name='{3}' where  INVOICE_NO='{0}' and card_no='{1}' and oper_id is null
             * */
            #endregion
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageDetail.ShiftPackageOwner.oper", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PackageDetail.ShiftPackageOwner.oper";
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
        /// 根据流水号查询套餐明细
        /// </summary>
        /// <param name="ClinicNO"></param>
        /// <returns></returns>
        public ArrayList QueryDetailByClinicNO(string ClinicNO, string CancelFlag)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageDetail.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PackageDetail.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageDetail.Select.Where1", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PackageDetail.Select.Where1";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, ClinicNO, CancelFlag);
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
        /// 根据多个流水号查询套餐明细//{727B4075-258A-4F11-A757-D8F0AEF08380} 套餐核销
        /// </summary>
        /// <param name="ClinicNO"></param>
        /// <returns></returns>
        public ArrayList QueryDetailByClinicNOs(string ClinicNOs, string CancelFlag)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageDetail.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PackageDetail.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageDetail.Select.Where3", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PackageDetail.Select.Where3";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, ClinicNOs, CancelFlag);
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
        /// 根据多个流水号查询套餐明细//{727B4075-258A-4F11-A757-D8F0AEF08380} 套餐核销
        /// </summary>
        /// <param name="ClinicNO"></param>
        /// <returns></returns>
        public ArrayList QueryDetailByClinicNOsNoCost(string ClinicNOs, string CancelFlag)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageDetail.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PackageDetail.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageDetail.Select.Where4", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PackageDetail.Select.Where4";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, ClinicNOs, CancelFlag);
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
        /// {D1E043F1-B598-4B8E-BEFB-8B4C48E56AB3} 
        /// 套餐核销
        /// 根据多个流水号查询套餐明细
        /// </summary>
        /// <param name="ClinicNO"></param>
        /// <returns></returns>
        public ArrayList QueryDetailByClinicNOs1(string ClinicNOs, string CancelFlag)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageDetail.Select1", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PackageDetail.Select1";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageDetail.Select.Where3", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PackageDetail.Select.Where3";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, ClinicNOs, CancelFlag);
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
        /// 根据交易流水号和顺序号查询某一条套餐记录
        /// </summary>
        /// <param name="ClinicNO"></param>
        /// <param name="Sequence"></param>
        /// <returns></returns>
        public FS.HISFC.Models.MedicalPackage.Fee.PackageDetail QueryDetailByClinicSeq(string ClinicNO, string TransType, string Sequence)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageDetail.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PackageDetail.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageDetail.Select.Where2", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.PackageDetail.Select.Where2";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, ClinicNO, TransType, Sequence);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }

            ArrayList al = this.GetObjets(Sql);

            if (al == null || al.Count == 0)
            {
                return null;
            }

            return al[0] as FS.HISFC.Models.MedicalPackage.Fee.PackageDetail;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private ArrayList GetObjets(string sql)
        {
            if (this.ExecQuery(sql) < 0) return null;

            ArrayList packagedetailArray = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.MedicalPackage.Fee.PackageDetail model = new FS.HISFC.Models.MedicalPackage.Fee.PackageDetail();
                    model.ID = this.Reader[0].ToString();
                    model.SequenceNO = this.Reader[1].ToString();
                    model.Trans_Type = this.Reader[2].ToString();
                    model.PayFlag = this.Reader[3].ToString();
                    model.CardNO = this.Reader[4].ToString();
                    if (model.Item == null)
                    {
                        model.Item = new FS.HISFC.Models.Base.Item();
                    }
                    model.Item.ID = this.Reader[5].ToString();
                    model.Item.Name = this.Reader[6].ToString();
                    if (model.ExecDept == null)
                    {
                        model.ExecDept = new FS.FrameWork.Models.NeuObject();
                    }
                    model.ExecDept.ID = this.Reader[7].ToString();
                    model.Item.Price = Decimal.Parse(this.Reader[8].ToString());
                    model.Item.Qty = Decimal.Parse(this.Reader[9].ToString());
                    model.Unit = this.Reader[10].ToString();
                    model.UnitFlag = this.Reader[11].ToString();
                    model.Detail_Cost = Decimal.Parse(this.Reader[12].ToString());
                    model.Real_Cost = Decimal.Parse(this.Reader[13].ToString());
                    model.Gift_cost = Decimal.Parse(this.Reader[14].ToString());
                    model.Etc_cost = Decimal.Parse(this.Reader[15].ToString());
                    model.RtnQTY = Decimal.Parse(this.Reader[16].ToString());
                    model.ConfirmQTY = Decimal.Parse(this.Reader[17].ToString());
                    model.Cancel_Flag = this.Reader[18].ToString();
                    model.InvoiceNO = this.Reader[19].ToString();
                    model.Memo = this.Reader[20].ToString();
                    model.Oper = this.Reader[21].ToString();
                    model.OperTime = DateTime.Parse(this.Reader[22].ToString());
                    model.CancelOper = this.Reader[23].ToString();
                    model.CancelTime = DateTime.Parse(this.Reader[24].ToString());

                    try
                    {
                        //{98A19959-7CD4-4f69-839D-D7020687D3A3}
                        model.Item.Specs = this.Reader[25].ToString();

                        // {01C202AF-5D8A-4d89-9BA5-1910B5AA7607} 
                        model.HospitalID = this.Reader[26].ToString();
                        model.HospitalName = this.Reader[27].ToString();

                        // {01C202AF-5D8A-4d89-9BA5-1910B5AA7607} 
                        if (this.Reader.FieldCount > 28)
                        {
                            model.PackageName = this.Reader[28].ToString();
                        }
                    }
                    catch
                    {
                    }

                    packagedetailArray.Add(model);
                }

                this.Reader.Close();
            }
            catch (Exception ex)
            {
                packagedetailArray = null;
                this.Reader.Close();
            }

            return packagedetailArray;
        }

        /// <summary>
        /// 根据就诊卡号查询套餐明细//{727B4075-258A-4F11-A757-D8F0AEF08380} 套餐核销
        /// </summary>
        /// <param name="ClinicNO"></param>
        /// <returns></returns>
        public ArrayList QueryDetailByCardNO(string cardNO)
        {
            string Sql = @"select xpd.* from exp_package xp inner join exp_packagedetail xpd on xp.clinic_code=xpd.clinic_code and xpd.rtn_qty>0 
inner join bd_com_package bcp on bcp.package_id=xp.package_id and  bcp.service_range!='I' 
 where xp.card_no='{0}' and xp.cancel_flag='0' and xp.cost_flag='0'";
            //string Where = "";

            //if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageDetail.Select", ref Sql) == -1)
            //{
            //    this.Err = "没有找到MedicalPackage.Package.Fee.PackageDetail.Select";
            //    return null;
            //}

            //if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageDetail.Select.Where3", ref Where) == -1)
            //{
            //    this.Err = "没有找到MedicalPackage.Package.Fee.PackageDetail.Select.Where3";
            //    return null;
            //}

            try
            {
                // Sql += Where;
                Sql = string.Format(Sql, cardNO);
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
        /// 获取购买2023产检套餐升级次数为0的项目
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>

        public string QueryPackageDetailItemZeroByCardNO(string cardNO)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Fee.PackageDetail.QueryPackageDetailItemZeroByCardNO", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Fee.PackageDetail.QueryPackageDetailItemZeroByCardNO";
                return null;
            }
            try
            {
                Sql = string.Format(Sql, cardNO);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }

            if (this.ExecQuery(Sql) < 0) return null;

            StringBuilder bulider = new StringBuilder();

            try
            {
                while (this.Reader.Read())
                {
                    bulider.Append(this.Reader[0].ToString());
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {

                this.Reader.Close();
            }

            return bulider.ToString();

        }


        /// <summary>
        /// 根据就诊卡号查询家庭套餐明细//{DD31280F-7321-42BB-B150-4C63018ED85F}  查询家庭套餐明细
        /// </summary>
        /// <param name="ClinicNO"></param>
        /// <returns></returns>
        public ArrayList QueryFamilyPackageDetailByCardNO(string cardNO)
        {
            //
            string Sql = "";
            //            string Sql = @"select xpd.* from exp_package xp inner join exp_packagedetail xpd on xp.clinic_code=xpd.clinic_code and xpd.rtn_qty>0 
            //inner join bd_com_package bcp on bcp.package_id=xp.package_id and  bcp.service_range!='I' and bcp.valid_flag='1'
            // where xp.card_no='{0}' and xp.cancel_flag='0' and xp.cost_flag='0'";


            if (this.Sql.GetCommonSql("MedicalPackage.Fee.PackageDetail.QueryFamilyPackageDetailByCardNO", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Fee.PackageDetail.QueryFamilyPackageDetailByCardNO";
                return null;
            }
            try
            {
                // Sql += Where;
                Sql = string.Format(Sql, cardNO);
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
        /// 通过实体获取参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string[] GetParameters(FS.HISFC.Models.MedicalPackage.Fee.PackageDetail model)
        {
            return new string[] { 
                model.ID,
                model.SequenceNO,
                model.Trans_Type,
                model.PayFlag,
                model.CardNO,
                model.Item.ID,
                model.Item.Name,
                model.ExecDept.ID,
                model.Item.Price.ToString(),
                model.Item.Qty.ToString(),
                model.Unit,
                model.UnitFlag,
                model.Detail_Cost.ToString(),
                model.Real_Cost.ToString(),
                model.Gift_cost.ToString(),
                model.Etc_cost.ToString(),
                model.RtnQTY.ToString(),
                model.ConfirmQTY.ToString(),
                model.Cancel_Flag,
                model.InvoiceNO,
                model.Memo,
                model.Oper,
                model.OperTime.ToString(),
                model.CancelOper,
                model.CancelTime.ToString(),
                //{01C202AF-5D8A-4d89-9BA5-1910B5AA7607} 
                model.HospitalID.ToString(),
                model.HospitalName.ToString()

            };
        }
    }
}
