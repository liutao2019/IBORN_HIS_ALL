using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neusoft.SOC.Local.OutpatientFee.ShenZhen.BinHai.IOutpatientPopupFee
{
    public class Function
    {
        #region 静态变量

        /// <summary>
        /// 控制参数帮助类
        /// </summary>
        public static Neusoft.FrameWork.Public.ObjectHelper controlerHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        #endregion

        #region 分发票

        /// <summary>
        /// 分票处理, 不在事务内
        /// </summary>
        /// <param name="cost">当前金额</param>
        /// <returns>处理后得金额</returns>
        public static decimal DealCent(decimal cost)
        {
            return DealCent(cost, null);
        }
        /// <summary>
        /// 分票处理 在事务内
        /// </summary>
        /// <param name="cost">当前金额</param>
        /// <param name="t">数据库连接</param>
        /// <returns>处理后得金额</returns>
        public static decimal DealCent(decimal cost, Neusoft.FrameWork.Management.Transaction t)
        {
            Neusoft.FrameWork.Management.ControlParam myCtrl = new Neusoft.FrameWork.Management.ControlParam();
            if (t != null)
            {
                myCtrl.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            }
            string conValue = "0";//myCtrl.QueryControlerInfo(Neusoft.HISFC.BizProcess.Integrate.Const.CENTRULE);
            if (controlerHelper == null || controlerHelper.ArrayObject == null || controlerHelper.ArrayObject.Count <= 0)
            {
                conValue = myCtrl.QueryControlerInfo(Neusoft.HISFC.BizProcess.Integrate.Const.CENTRULE);
            }
            else
            {
                Neusoft.FrameWork.Models.NeuObject obj = controlerHelper.GetObjectFromID(Neusoft.HISFC.BizProcess.Integrate.Const.CENTRULE);

                if (obj == null)
                {
                    conValue = myCtrl.QueryControlerInfo(Neusoft.HISFC.BizProcess.Integrate.Const.CENTRULE);
                }
                else
                {
                    conValue = ((Neusoft.HISFC.Models.Base.ControlParam)obj).ControlerValue;
                }
            }
            if (conValue == null || conValue == "" || conValue == "-1")
            {
                conValue = "0";//默认不处理
            }
            decimal dealedCost = 0;

            switch (conValue)
            {
                case "0": //不处理
                    dealedCost = cost;
                    break;
                case "1": //四舍五入
                    dealedCost = Neusoft.FrameWork.Public.String.FormatNumber(cost, 1);
                    break;
                case "2": //上取整
                    dealedCost = cost * 10;
                    if (dealedCost != Decimal.Truncate(dealedCost))
                    {
                        dealedCost = Decimal.Truncate(dealedCost) + 1;
                    }
                    dealedCost = dealedCost / 10;
                    break;
                case "3": //下取整
                    dealedCost = cost * 10;
                    dealedCost = Decimal.Truncate(dealedCost) / 10;
                    break;

            }
            return dealedCost;
        }

        #endregion
    }
}
