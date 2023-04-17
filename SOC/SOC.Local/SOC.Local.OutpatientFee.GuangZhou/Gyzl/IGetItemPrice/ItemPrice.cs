using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.OutpatientFee.GuangZhou.Gyzl.IGetItemPrice
{
    public class ItemPrice:FS.FrameWork.Management.Database, FS.HISFC.BizProcess.Interface.Fee.IGetItemPrice
    {
        FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();
        string sql = @"select t.item_code,t.unit_price,t.unit_price1,t.unit_price2 from fin_com_undruginfo t where t.item_code='{0}'";
        #region IGetItemPrice 成员

        public decimal GetPrice(string itemCode, FS.HISFC.Models.Registration.Register register, decimal UnitPrice, decimal ChildPrice, decimal SPPrice, decimal PurchasePrice, ref decimal orgPrice)
        {
            orgPrice = UnitPrice;
            return UnitPrice;
        }

        public decimal GetPriceForInpatient(string itemCode, FS.HISFC.Models.RADT.PatientInfo patientInfo, decimal UnitPrice, decimal ChildPrice, decimal SPPrice, decimal PurchasePrice, ref decimal orgPrice)
        {
            orgPrice = UnitPrice;
            return UnitPrice;
        }

        private int getPrice(string itemCode, ref decimal UnitPrice,ref decimal ChildPrice,ref decimal SPPrice)
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
                            UnitPrice = FS.FrameWork.Function.NConvert.ToDecimal(itemManager.Reader[1]);
                            SPPrice = FS.FrameWork.Function.NConvert.ToDecimal(itemManager.Reader[3]);
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
