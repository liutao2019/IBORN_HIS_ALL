using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.OutpatientFee.ZhuHai.Zdwy.ITruncFee
{
    public class ITruncFeeImplement:FS.HISFC.BizProcess.Interface.Fee.ITruncFee
    {
        /// <summary>
        /// 金额位数
        /// </summary>
        private int median = 2;

        #region ITruncFee 成员

        public object[] TruncFee(object[] args)
        {
            object[] returnObj = null;
            #region 1.门诊收费界面
            if (args.Length >= 2 && (args[0] is FS.HISFC.Models.Base.FT) && ((args[1] is FS.HISFC.Models.Fee.Outpatient.FeeItemList)))
            {
                FS.HISFC.Models.Base.FT ft = args[0] as FS.HISFC.Models.Base.FT;
                FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemLit = args[1] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                ft.TotCost = FS.FrameWork.Public.String.TruncateNumber(feeItemLit.Item.Price * feeItemLit.Item.Qty / feeItemLit.Item.PackQty, median);
                ft.RebateCost = FS.FrameWork.Public.String.TruncateNumber(feeItemLit.FT.RebateCost * feeItemLit.Item.Qty / feeItemLit.Item.PackQty, median);
                returnObj = new object[] { ft };
            }
            #endregion
            return returnObj;
        }

        #endregion


        #region ITruncFee 成员

        public object TruncFee(object arg)
        {
            object returnObj = null;

            #region 2、门诊医嘱实体转化为费用实体
            if (arg is FS.HISFC.Models.Order.OutPatient.Order)
            {
                FS.HISFC.Models.Order.OutPatient.Order order = arg as FS.HISFC.Models.Order.OutPatient.Order;
                returnObj = new FS.HISFC.Models.Base.FT();
                //为NULL返回新实体
                if (order == null || order.FT == null)
                {
                    return returnObj;
                }
                
                ((FS.HISFC.Models.Base.FT)returnObj).AdjustOvertopCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.AdjustOvertopCost, median);
                ((FS.HISFC.Models.Base.FT)returnObj).AirLimitCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.AirLimitCost, median);
                ((FS.HISFC.Models.Base.FT)returnObj).BalancedCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.BalancedCost, median);
                ((FS.HISFC.Models.Base.FT)returnObj).BalancedPrepayCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.BalancedPrepayCost, median);
                ((FS.HISFC.Models.Base.FT)returnObj).BedLimitCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.BedLimitCost, median);
                ((FS.HISFC.Models.Base.FT)returnObj).BedOverDeal = order.FT.BedOverDeal;
                ((FS.HISFC.Models.Base.FT)returnObj).BloodLateFeeCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.BloodLateFeeCost, median);
                ((FS.HISFC.Models.Base.FT)returnObj).BoardCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.BoardCost, median);
                ((FS.HISFC.Models.Base.FT)returnObj).BoardPrepayCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.BoardPrepayCost, median);
                ((FS.HISFC.Models.Base.FT)returnObj).DrugFeeTotCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.DrugFeeTotCost, median);
                ((FS.HISFC.Models.Base.FT)returnObj).TransferPrepayCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.TransferPrepayCost, median);
                ((FS.HISFC.Models.Base.FT)returnObj).TransferTotCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.TransferTotCost, median);
                ((FS.HISFC.Models.Base.FT)returnObj).DayLimitCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.DayLimitCost, median);
                ((FS.HISFC.Models.Base.FT)returnObj).DerateCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.DerateCost, median);
                ((FS.HISFC.Models.Base.FT)returnObj).FixFeeInterval = order.FT.FixFeeInterval;
                ((FS.HISFC.Models.Base.FT)returnObj).ID = order.FT.ID;
                ((FS.HISFC.Models.Base.FT)returnObj).LeftCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.LeftCost, median);
                ((FS.HISFC.Models.Base.FT)returnObj).OvertopCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.OvertopCost, median);
                ((FS.HISFC.Models.Base.FT)returnObj).DayLimitTotCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.DayLimitTotCost, median);
                ((FS.HISFC.Models.Base.FT)returnObj).Memo = order.FT.Memo;
                ((FS.HISFC.Models.Base.FT)returnObj).Name = order.FT.Name;
                ((FS.HISFC.Models.Base.FT)returnObj).OwnCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.OwnCost, median);
                ((FS.HISFC.Models.Base.FT)returnObj).FTRate.OwnRate = order.FT.FTRate.OwnRate;
                ((FS.HISFC.Models.Base.FT)returnObj).PayCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.PayCost, median);
                ((FS.HISFC.Models.Base.FT)returnObj).FTRate.PayRate = order.FT.FTRate.PayRate;
                ((FS.HISFC.Models.Base.FT)returnObj).PreFixFeeDateTime = order.FT.PreFixFeeDateTime;
                ((FS.HISFC.Models.Base.FT)returnObj).PrepayCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.PrepayCost, median);
                ((FS.HISFC.Models.Base.FT)returnObj).PubCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.PubCost, median);
                ((FS.HISFC.Models.Base.FT)returnObj).RebateCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.RebateCost, median);
                ((FS.HISFC.Models.Base.FT)returnObj).ReturnCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.ReturnCost, median);
                ((FS.HISFC.Models.Base.FT)returnObj).SupplyCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.SupplyCost, median);
                ((FS.HISFC.Models.Base.FT)returnObj).TotCost = FS.FrameWork.Public.String.TruncateNumber(order.FT.TotCost, median);

                ((FS.HISFC.Models.Base.FT)returnObj).User01 = order.FT.User01;
                ((FS.HISFC.Models.Base.FT)returnObj).User02 = order.FT.User02;
                ((FS.HISFC.Models.Base.FT)returnObj).User03 = order.FT.User03;
            }
            #endregion

            #region 3、传入金额直接调用转换
            if (arg is decimal)
            {
                returnObj = (object)FS.FrameWork.Public.String.TruncateNumber(FS.FrameWork.Function.NConvert.ToDecimal(arg), median);
            }
            #endregion

            #region 4、传入出库申请实体
            if (arg is FS.HISFC.Models.Pharmacy.ApplyOut)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = arg as FS.HISFC.Models.Pharmacy.ApplyOut;
                returnObj = (object)FS.FrameWork.Public.String.TruncateNumber(applyOut.Item.PriceCollection.RetailPrice * (applyOut.Operation.ApplyQty / applyOut.Item.PackQty),median);
            }
            #endregion

            #region 5、传入出库实体
            if (arg is FS.HISFC.Models.Pharmacy.Output)
            {
                FS.HISFC.Models.Pharmacy.Output output = arg as FS.HISFC.Models.Pharmacy.Output;
                returnObj = (object)FS.FrameWork.Public.String.TruncateNumber(output.Item.PriceCollection.RetailPrice * (output.Quantity / output.Item.PackQty),median);
            }
            #endregion

            return returnObj;
        }

        #endregion
    }
}
