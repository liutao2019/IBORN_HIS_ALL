using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.OutpatientFee.FoSan.Interface
{
    /// <summary>
    /// [用于本地化三院四院的发票列表双击显示界面]
    /// [创建时间： 2012-04-18]
    /// </summary>
    public interface IOutPatientInvoiceShow
    {
        bool SetInvoiceInfo(FS.HISFC.Models.Fee.Outpatient.Balance balace);
        DialogResult ShowDialog();
    }
}
