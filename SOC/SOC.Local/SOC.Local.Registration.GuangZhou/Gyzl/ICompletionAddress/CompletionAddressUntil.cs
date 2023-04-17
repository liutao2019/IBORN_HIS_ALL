using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Registration.GuangZhou.Gyzl.ICompletionAddress
{
    public class CompletionAddressUntil:FS.HISFC.BizProcess.Interface.Registration.ICompletionAddress
    {

        #region ICompletionAddress 成员

        public string CompletionAddress(string address)
        {
            string result = address;
            frmSelectAddress f = new frmSelectAddress();
            int resultCount = f.QueryAddress(address);
            if (resultCount == 1)
            {
                result = f.Address;
            }
            else if (resultCount > 1)
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    result = f.Address;
                }
            }
            f.Dispose();
            return result;
        }

        #endregion


    }
}
