using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizLogic.MedicalPackage.Fee
{
    public class Deposit : Base.DataBase
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Insert(FS.HISFC.Models.MedicalPackage.Fee.Deposit model)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Deposit.Insert", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Deposit.Insert";
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
        public int Update(FS.HISFC.Models.MedicalPackage.Fee.Deposit model)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Deposit.Update", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Deposit.Update";
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
        public int Delete(FS.HISFC.Models.MedicalPackage.Fee.Deposit model)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Deposit.Delete", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Deposit.Delete";
                return -1;
            }

            try
            {
                Sql = string.Format(Sql, model.ID, model.Trans_Type);
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
        /// 根据卡号查询押金记录
        /// </summary>
        public ArrayList QueryByCardNO(string CardNO, string CancelFlag)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Deposit.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Deposit.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Deposit.Select.Where1", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Deposit.Select.Where1";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, CardNO, CancelFlag);
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
        /// 根据卡号查询押金记录
        /// </summary>
        public ArrayList QueryByCardNOAndDate(string CardNO, string CancelFlag, DateTime begin, DateTime end)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Deposit.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Deposit.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Deposit.Select.Where3", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Deposit.Select.Where3";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, CardNO, CancelFlag, begin.ToString(), end.ToString());
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
        /// 根据卡号查询押金记录
        /// </summary>
        public FS.HISFC.Models.MedicalPackage.Fee.Deposit QueryByDepositNO(string DepositNO)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Deposit.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Deposit.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.Deposit.Select.Where2", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Fee.Deposit.Select.Where2";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, DepositNO);
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
                return al[0] as FS.HISFC.Models.MedicalPackage.Fee.Deposit;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取一个新的流水号
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        public string GetNewDepositNO()
        {
            return this.GetSequence("Package.Fee.GetNewDepositNO");
        }

        /// <summary>
        /// 消费指定的押金记录
        /// </summary>
        /// <param name="deposit">被消费的押金记录</param>{AAEAAEF2-7573-4c3a-AFB8-EB6F6BF79C53}
        /// <param name="cost">消费金额</param>
        /// <returns></returns>
        public int DepositCost(FS.HISFC.Models.MedicalPackage.Fee.Deposit deposit, decimal cost)
        {
            FS.HISFC.Models.MedicalPackage.Fee.Deposit dep = this.QueryByDepositNO(deposit.ID);
            HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
            HISFC.Models.Base.Department dept = employee.Dept as HISFC.Models.Base.Department;
            string hospitalid = dept.HospitalID;
            string hospitalname = dept.HospitalName;
            if (dep.Cancel_Flag != "0")
            {
                this.Err = "押金信息已发生变化，请重试！";
                return -1;
            }

            if (cost > deposit.Amount)
            {
                this.Err = "消费金额大于最大金额！";
                return -1;
            }

            FS.HISFC.Models.MedicalPackage.Fee.Deposit depositOld = deposit;
            FS.HISFC.Models.MedicalPackage.Fee.Deposit depositCos = deposit.Clone(false);
            FS.HISFC.Models.MedicalPackage.Fee.Deposit rest = deposit.Clone(false);

            depositOld.Cancel_Flag = "1";
            depositOld.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
            depositOld.CancelTime = this.GetDateTimeFromSysDateTime();

            if (this.Update(depositOld) < 0)
            {
                return -1;
            }

            depositCos.Trans_Type = "2";
            depositCos.Amount = -depositCos.Amount;
            depositCos.Cancel_Flag = "1";
            //{F4EC0C51-BFAD-4e34-BF17-E2749E58CAE8}
            depositCos.Oper = FS.FrameWork.Management.Connection.Operator.ID;
            depositCos.OperTime = this.GetDateTimeFromSysDateTime();
            depositCos.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
            depositCos.CancelTime = this.GetDateTimeFromSysDateTime();
            depositCos.DepositType = FS.HISFC.Models.MedicalPackage.Fee.DepositType.TCXF;
            depositCos.HospitalID = hospitalid;
            depositCos.HospitalName = hospitalname;//{AAEAAEF2-7573-4c3a-AFB8-EB6F6BF79C53}
            if (this.Insert(depositCos) < 0)
            {
                return -1;
            }

            //如果消费金额不足此条押金金额，则重新把剩余部分插回数据库
            if (deposit.Amount > cost)
            {
                rest.ID = this.GetNewDepositNO();
                rest.Amount = deposit.Amount - cost;
                rest.OriginalClinic = depositOld.ID;
                rest.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                rest.OperTime = this.GetDateTimeFromSysDateTime();
                //{F4EC0C51-BFAD-4e34-BF17-E2749E58CAE8}
                rest.DepositType = FS.HISFC.Models.MedicalPackage.Fee.DepositType.XFHH;
                rest.HospitalID = hospitalid;//{AAEAAEF2-7573-4c3a-AFB8-EB6F6BF79C53}
                rest.HospitalName = hospitalname;
                if (this.Insert(rest) < 0)
                {
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// 退押金记录
        /// </summary>
        /// <param name="deposit">退费记录</param>
        /// <param name="cost">退费金额</param>
        /// <returns></returns>
        public int DepositCancel(FS.HISFC.Models.MedicalPackage.Fee.Deposit deposit, decimal cost, string CancelPayCode)
        {
            FS.HISFC.Models.MedicalPackage.Fee.Deposit dep = this.QueryByDepositNO(deposit.ID);
            if (dep.Cancel_Flag != "0")
            {
                this.Err = "押金信息已发生变化，请重试！";
                return -1;
            }

            if (cost > deposit.Amount)
            {
                this.Err = "退费金额大于押金金额！";
                return -1;
            }

            FS.HISFC.Models.MedicalPackage.Fee.Deposit depositOld = deposit;
            FS.HISFC.Models.MedicalPackage.Fee.Deposit depositCos = deposit.Clone(false);
            FS.HISFC.Models.MedicalPackage.Fee.Deposit rest = deposit.Clone(false);

            depositOld.Cancel_Flag = "1";
            depositOld.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
            depositOld.CancelTime = this.GetDateTimeFromSysDateTime();


            //退费方式
            if (string.IsNullOrEmpty(CancelPayCode))
            {
                CancelPayCode = depositOld.Mode_Code;
            }

            if (this.Update(depositOld) < 0)
            {
                return -1;
            }

            depositCos.Trans_Type = "2";
            depositCos.Amount = -depositCos.Amount;
            depositCos.Mode_Code = CancelPayCode;
            depositCos.Cancel_Flag = "1";
            depositCos.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
            depositCos.CancelTime = this.GetDateTimeFromSysDateTime();
            //{F4EC0C51-BFAD-4e34-BF17-E2749E58CAE8}
            depositCos.Oper = FS.FrameWork.Management.Connection.Operator.ID;
            depositCos.OperTime = this.GetDateTimeFromSysDateTime();
            depositCos.DepositType = FS.HISFC.Models.MedicalPackage.Fee.DepositType.TYJ;

            //{E5C47B78-AB42-4423-8B0F-1658CEB5AC7C}
            FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.Models.Base.Department dept = employee.Dept as FS.HISFC.Models.Base.Department;
            //{E5C47B78-AB42-4423-8B0F-1658CEB5AC7C}
            depositCos.HospitalID = dept.HospitalID;
            depositCos.HospitalName = dept.HospitalName;


            if (this.Insert(depositCos) < 0)
            {
                return -1;
            }

            //如果消费金额不足此条押金金额，则重新把剩余部分插回数据库
            if (deposit.Amount > cost)
            {
                rest.ID = this.GetNewDepositNO();
                rest.Amount = deposit.Amount - cost;
                rest.OriginalClinic = depositOld.ID;
                rest.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                rest.OperTime = this.GetDateTimeFromSysDateTime();
                //{F4EC0C51-BFAD-4e34-BF17-E2749E58CAE8}
                rest.DepositType = FS.HISFC.Models.MedicalPackage.Fee.DepositType.JYJ;

                if (this.Insert(rest) < 0)
                {
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private ArrayList GetObjets(string sql)
        {
            if (this.ExecQuery(sql) < 0) return null;

            ArrayList depositArray = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.MedicalPackage.Fee.Deposit model = new FS.HISFC.Models.MedicalPackage.Fee.Deposit();
                    model.ID = this.Reader[0].ToString();
                    model.Trans_Type = this.Reader[1].ToString();
                    model.RecipeNO = this.Reader[2].ToString();
                    model.Mode_Code = this.Reader[3].ToString();
                    model.Amount = Decimal.Parse(this.Reader[4].ToString());
                    if (model.Bank == null)
                    {
                        model.Bank = new FS.FrameWork.Models.NeuObject();
                    }
                    model.Bank.ID = this.Reader[5].ToString();
                    model.Bank.Name = this.Reader[6].ToString();
                    model.Account = this.Reader[7].ToString();
                    model.AccountType = this.Reader[8].ToString();
                    model.AccountFlag = this.Reader[9].ToString();
                    model.PosNO = this.Reader[10].ToString();
                    model.CheckNO = this.Reader[11].ToString();
                    model.Oper = this.Reader[12].ToString();
                    model.OperTime = DateTime.Parse(this.Reader[13].ToString());
                    model.CheckFlag = this.Reader[14].ToString();
                    model.CheckOper = this.Reader[15].ToString();
                    model.CheckTime = DateTime.Parse(this.Reader[16].ToString());
                    model.BalanceFlag = this.Reader[17].ToString();
                    model.BalanceNO = this.Reader[18].ToString();
                    model.BalanceOper = this.Reader[19].ToString();
                    if (string.IsNullOrEmpty(this.Reader[20].ToString()))
                    {
                        model.BalanceTime = DateTime.Parse("0001-01-01");
                    }
                    else
                    {
                        model.BalanceTime = DateTime.Parse(this.Reader[20].ToString());
                    }
                   
                    model.CorrectFlag = this.Reader[21].ToString();
                    model.CorrectOper = this.Reader[22].ToString();
                    model.CorrectTime = DateTime.Parse(this.Reader[23].ToString());
                    model.Cancel_Flag = this.Reader[24].ToString();
                    model.OriginalClinic = this.Reader[25].ToString(); ;
                    model.CancelOper = this.Reader[26].ToString(); ;
                    model.CancelTime = DateTime.Parse(this.Reader[27].ToString());
                    model.Memo = this.Reader[28].ToString();
                    model.CardNO = this.Reader[29].ToString();
                    model.DepositType = (FS.HISFC.Models.MedicalPackage.Fee.DepositType)Enum.Parse(typeof(FS.HISFC.Models.MedicalPackage.Fee.DepositType), this.Reader[30].ToString());
                    //{E5C47B78-AB42-4423-8B0F-1658CEB5AC7C}
                    model.HospitalID = this.Reader[31].ToString();
                    model.HospitalName = this.Reader[32].ToString();

                    depositArray.Add(model);
                }

                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Reader.Close();
            }

            return depositArray;
        }

        /// <summary>
        /// 通过实体获取参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string[] GetParameters(FS.HISFC.Models.MedicalPackage.Fee.Deposit model)
        {
            return new string[] {
                model.ID,
                model.Trans_Type,
                model.RecipeNO,
                model.Mode_Code,
                model.Amount.ToString(),
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
                model.OriginalClinic,
                model.CancelOper,
                model.CancelTime.ToString(),
                model.Memo,
                model.CardNO,
                model.DepositType.ToString(),
                //{E5C47B78-AB42-4423-8B0F-1658CEB5AC7C}
                model.HospitalID.ToString(),
                model.HospitalName.ToString()
            };
        }

    }
}
