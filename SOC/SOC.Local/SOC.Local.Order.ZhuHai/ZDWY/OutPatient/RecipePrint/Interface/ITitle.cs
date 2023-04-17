using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.RecipePrint.Interface
{
    public interface ITitle
    {
        int SetTitleInfo(FS.HISFC.Models.Registration.Register regObj, string recipeNo, string speRecipeFlag);
    }
}
