using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Registration.ZhuHai.Zdwy.ISearchCardNoFalse
{
    public class CreateNewCard : FS.HISFC.BizProcess.Interface.Registration.ISearchCardNoFalse
    {
        #region ISearchCardNoFalse成员
        public int PostMarkNo(ref string markNo)
        {
            FS.HISFC.Components.Account.Controls.ucCardManager f = new FS.HISFC.Components.Account.Controls.ucCardManager();
            f.Tag = markNo;
            f.IsCanChangeCardType = true;
            
            f.IsJumpHomePhone = true;
            f.IsShowTipsWhenSaveSucess = false;

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
