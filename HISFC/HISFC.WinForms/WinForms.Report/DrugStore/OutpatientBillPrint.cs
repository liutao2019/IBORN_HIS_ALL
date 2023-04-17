using System;
using System.Collections.Generic;
using System.Text;

namespace FS.WinForms.Report.DrugStore
{
    /// <summary>
    /// ������ҩ����ӡ����
    /// 
    /// <����˵��>
    ///     1�����ݲ�ͬ�Ĵ�ӡ���ͽ��д�ӡ ���ؽӿ�ʵ��
    ///     2����չ��ʽ Ŀǰ����������Ŀ ��ӡ������Һ��ǩ����Һ�嵥
    /// </����˵��>
    /// </summary>
    public class OutpatientBillPrint : FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory
    {
        public OutpatientBillPrint()
        {
 
        }

        #region IOutpatientPrintFactory ��Ա

        public FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint GetInstance(FS.HISFC.Models.Pharmacy.DrugTerminal terminal)
        {
            switch (terminal.TerimalPrintType)
            {
                case FS.HISFC.Models.Pharmacy.EnumClinicPrintType.��ǩ:

                   // return new FS.WinForms.Report.DrugStore.DrugLabelPrint();

                    return new FS.WinForms.Report.DrugStore.ucRecipeLabelFY();//{EB6E8006-7228-46ea-9C01-D0832563178D} sel ��ҩ�嵥��ӡ

                case FS.HISFC.Models.Pharmacy.EnumClinicPrintType.��չ:
                    
                    return new FS.WinForms.Report.DrugStore.ZLInjectPrintInstance();
                    
                case FS.HISFC.Models.Pharmacy.EnumClinicPrintType.�嵥:

                    //return new FS.WinForms.Report.DrugStore.ucZLHerbalBill();
                    return new FS.WinForms.Report.DrugStore.ucOutHerbalBill();
            }

            return null;
        }

        #endregion
    }
}
