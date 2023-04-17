using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJLocal.HISFC.Components.OpGuide.RecipePrint.Interface
{
    public interface ITitle
    {
        int SetTitleInfo(Neusoft.HISFC.Models.Registration.Register regObj, string recipeNo, string speRecipeFlag);
    }
}
