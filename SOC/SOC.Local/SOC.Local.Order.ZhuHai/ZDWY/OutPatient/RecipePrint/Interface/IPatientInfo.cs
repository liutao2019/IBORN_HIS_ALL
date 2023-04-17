using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.RecipePrint.Interface
{
    public interface IPatientInfo
    {
        int SetPatientInfo(FS.HISFC.Models.Registration.Register regObj);
        int SetRecipeDept(string deptName);
    }
}
