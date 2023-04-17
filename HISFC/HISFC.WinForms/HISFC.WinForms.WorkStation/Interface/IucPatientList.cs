using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.WinForms.WorkStation.Interface
{
    public interface IucPatientList
    {
        int SetValue(FS.HISFC.Models.RADT.PatientInfo patient);
        event FS.FrameWork.WinForms.Forms.OKHandler OkEvent;
        System.Windows.Forms.DialogResult Show();
        System.Windows.Forms.DialogResult Show(System.Windows.Forms.Control panel);
    }
}
