using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Nurse.Controls.ZDWY.Array
{
    public class IAssignDisplay : FS.SOC.HISFC.Assign.Interface.Components.IAssignDisplay
    {
        frmArrayDisplay frmDisplay = null;

        #region IAssignDisplay 成员

        public void Close()
        {
            if (frmDisplay != null
                && !frmDisplay.IsDisposed)
            {
                frmDisplay.Hide();
            }
        }

        public void Show()
        {
            if (frmDisplay == null
                || frmDisplay.IsDisposed)
            {
                frmDisplay = new frmArrayDisplay();
            }

            frmDisplay.Show();

            //if (Screen.AllScreens.Length > 1)
            //{
            //    if (Screen.AllScreens[0].Primary)
            //    {
            //        MessageBox.Show("主显示器是0", "哈哈");
            //        frmDisplay.DesktopBounds = Screen.AllScreens[1].Bounds;
            //    }
            //    else
            //    {
            //        MessageBox.Show("主显示器是1", "哈哈");
            //        frmDisplay.DesktopBounds = Screen.AllScreens[0].Bounds;
            //    }
            //}
            //else
            //{
            //    if (FS.FrameWork.WinForms.Classes.Function.IsManager())
            //    {
            //        frmDisplay.DesktopBounds = Screen.AllScreens[0].Bounds;
            //    }
            //}
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            frmDisplay.Close();
        }

        #endregion
    }
}
