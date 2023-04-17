using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJLocal.HISFC.Components.OpGuide.RecipePrint.Interface
{
    public interface IPatientInfo
    {
        int SetPatientInfo(Neusoft.HISFC.Models.Registration.Register regObj);
        int SetRecipeDept(string deptName);
    }
}
