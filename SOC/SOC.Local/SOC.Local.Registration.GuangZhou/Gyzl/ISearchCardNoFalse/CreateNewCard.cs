using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Registration.GuangZhou.Gyzl.ISearchCardNoFalse
{
    public class CreateNewCard : FS.HISFC.BizProcess.Interface.Registration.ISearchCardNoFalse
    {
        #region ISearchCardNoFalse成员
        public int PostMarkNo(ref string markNo)
        {
            System.Runtime.Remoting.ObjectHandle obj = System.Activator.CreateInstance("HISFC.Components.Account", "FS.HISFC.Components.Account.Controls.ucCardManagerGyzl");
            if (obj == null)
            {
                return -1;
            }
            FS.HISFC.Components.Account.Controls.ucCardManagerGyzl f = obj.Unwrap() as FS.HISFC.Components.Account.Controls.ucCardManagerGyzl;
            f.Tag = markNo;
            f.IsAutoMatchCardType = true;
            f.IsCanChangeCardType = true;
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(f);
            if (f.FindForm().DialogResult == DialogResult.OK)
            {
                markNo = f.Tag.ToString();
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
