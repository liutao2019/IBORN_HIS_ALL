using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HISFC.BizLogic.MedicalPackage.Fee
{
    /// <summary>
    /// 套餐核销日志{727B4075-258A-4F11-A757-D8F0AEF08380} 套餐核销
    /// </summary>
    public class PackageWriteOffBLL : FS.HISFC.BizLogic.MedicalPackage.Base.DataBase
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Insert(FS.HISFC.Models.MedicalPackage.Fee.PackageWriteOff model)
        {
            string Sql = @"     insert into exp_packagewriteoff (CARD_NO, INVOICE_NO, CLINIC_CODE, PACKAGEID, OPER_CODE, OPER_DATE, REALCOST)
values ('{0}', '{1}', '{2}', '{3}', '{4}', to_date('{5}', 'yyyy-mm-dd hh24:mi:ss'), {6})";

            //if (this.Sql.GetCommonSql("MedicalPackage.Package.Fee.PackageCost.Insert", ref Sql) == -1)
            //{
            //    this.Err = "没有找到MedicalPackage.Package.Fee.PackageCost.Insert";
            //    return -1;
            //}
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
            int i =this.ExecNoQuery(Sql);
            string message = this.Err;
            return i;   
        }


        /// <summary>
        /// 通过实体获取参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string[] GetParameters(FS.HISFC.Models.MedicalPackage.Fee.PackageWriteOff model)
        {
            return new string[]{
                model.Card_NO,
                model.InvoiceNO,
                model.Clinic_Code,
                model.PackageID,
               
                //model.Oper_Code,
                this.Operator.ID,
                this.GetSysDateTime(),
                 model.RealCost.ToString (),
            
            };
        }
    }
}
