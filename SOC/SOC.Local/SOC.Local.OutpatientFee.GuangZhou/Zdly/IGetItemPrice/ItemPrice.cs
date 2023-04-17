using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.OutpatientFee.GuangZhou.Zdly.IGetItemPrice
{
    public class ItemPrice : FS.FrameWork.Management.Database, FS.HISFC.BizProcess.Interface.Fee.IGetItemPrice
    {
        FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();
        string sql = @"select t.item_code,
                               decode(t.unitflag,
                                      '1',
                                      fun_get_packageprice(t.item_code),
                                      t.unit_price) unit_price,
                               decode(t.unitflag,
                                      '1',
                                      fun_get_packageprice1(t.item_code),
                                      t.unit_price1) unit_price1,
                               decode(t.unitflag,
                                      '1',
                                      fun_get_packageprice2(t.item_code),
                                      t.unit_price2) unit_price2,
                               t.Emerg_Scale
                          from fin_com_undruginfo t
                         where t.item_code = '{0}'
                        ";

        #region IGetItemPrice 成员

        public decimal GetPrice(string itemCode, FS.HISFC.Models.Registration.Register register, decimal UnitPrice, decimal ChildPrice, decimal SPPrice, decimal PurchasePrice, ref decimal orgPrice)
        {
            FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

            if (!object.Equals(register, null) 
                && Function.IsContainYKDept(register.DoctorInfo.Templet.Dept.ID))
            {
                this.GetPrice(itemCode, ref UnitPrice, ref ChildPrice, ref SPPrice);

                orgPrice = UnitPrice;
                return SPPrice;
            }
            else
            {
                this.GetPrice(itemCode, ref UnitPrice, ref ChildPrice, ref SPPrice);
                orgPrice = UnitPrice;
                return UnitPrice;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="patientInfo"></param>
        /// <param name="UnitPrice">默认价格</param>
        /// <param name="ChildPrice">儿童价</param>
        /// <param name="SPPrice">特诊价</param>
        /// <param name="PurchasePrice">购入价</param>
        /// <param name="orgPrice">原始价格（三甲价）</param>
        /// <returns></returns>
        public decimal GetPriceForInpatient(string itemCode, FS.HISFC.Models.RADT.PatientInfo patientInfo, decimal UnitPrice, decimal ChildPrice, decimal SPPrice, decimal PurchasePrice, ref decimal orgPrice)
        {
            FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            if (Function.IsContainYKDept(patientInfo.PVisit.PatientLocation.Dept.ID))
            {
                this.GetPrice(itemCode, ref UnitPrice, ref ChildPrice, ref SPPrice);
                orgPrice = UnitPrice;
                return SPPrice;
            }
            else
            {
                orgPrice = UnitPrice;
                return UnitPrice;
            }
        }

        /// <summary>
        /// 获取价格
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="UnitPrice">三甲价</param>
        /// <param name="ChildPrice">儿童价（目前无用）</param>
        /// <param name="SPPrice">特诊价</param>
        /// <returns></returns>
        private int GetPrice(string itemCode, ref decimal UnitPrice, ref decimal ChildPrice, ref decimal SPPrice)
        {
            int i = this.ExecQuery(string.Format(sql, itemCode));
            if (i > 0)
            {
                if (this.Reader != null)
                {
                    try
                    {
                        if (this.Reader.Read())
                        {
                            if (FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4]) > 0)
                            {
                                UnitPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[1]);
                                SPPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3]);
                                UnitPrice = UnitPrice * FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4]);
                                SPPrice = SPPrice * FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4]);
                            }
                            else
                            {
                                UnitPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[1]);
                                SPPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3]);
                            }
                        }
                    }
                    finally
                    {
                        if (this.Reader.IsClosed == false)
                        {
                            this.Reader.Close();
                        }
                    }
                }

                return 1;
            }
            else
            {
                return -1;
            }
        }

        #endregion
    }
}
