using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.OutpatientFee.FuYou
{

    /// <summary>
    /// [功能描述: 门诊收费打印本地化实例]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-03]<br></br>
    /// 说明：
    /// 1、主要是实现接口函数逻辑
    /// </summary>>
    public class PrintInterfaceImplement : FS.HISFC.BizProcess.Interface.Fee.IOutpatientAfterFee
    {
        #region IOutpatientAfterFee 成员

        ucItemListBill ucItemListBill;

        public int AfterFee(ArrayList alFeeItem, string info)
        {
            if (ucItemListBill == null)
            {
                ucItemListBill = new ucItemListBill();
            }
            return ucItemListBill.PrintData(alFeeItem);
        }

        #endregion
    }
}
