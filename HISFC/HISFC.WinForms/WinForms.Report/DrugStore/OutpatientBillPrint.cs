using System;
using System.Collections.Generic;
using System.Text;

namespace FS.WinForms.Report.DrugStore
{
    /// <summary>
    /// 门诊配药单打印工厂
    /// 
    /// <功能说明>
    ///     1、根据不同的打印类型进行打印 返回接口实例
    ///     2、扩展方式 目前按照肿瘤项目 打印门诊配液标签、配液清单
    /// </功能说明>
    /// </summary>
    public class OutpatientBillPrint : FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory
    {
        public OutpatientBillPrint()
        {
 
        }

        #region IOutpatientPrintFactory 成员

        public FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint GetInstance(FS.HISFC.Models.Pharmacy.DrugTerminal terminal)
        {
            switch (terminal.TerimalPrintType)
            {
                case FS.HISFC.Models.Pharmacy.EnumClinicPrintType.标签:

                   // return new FS.WinForms.Report.DrugStore.DrugLabelPrint();

                    return new FS.WinForms.Report.DrugStore.ucRecipeLabelFY();//{EB6E8006-7228-46ea-9C01-D0832563178D} sel 配药清单打印

                case FS.HISFC.Models.Pharmacy.EnumClinicPrintType.扩展:
                    
                    return new FS.WinForms.Report.DrugStore.ZLInjectPrintInstance();
                    
                case FS.HISFC.Models.Pharmacy.EnumClinicPrintType.清单:

                    //return new FS.WinForms.Report.DrugStore.ucZLHerbalBill();
                    return new FS.WinForms.Report.DrugStore.ucOutHerbalBill();
            }

            return null;
        }

        #endregion
    }
}
