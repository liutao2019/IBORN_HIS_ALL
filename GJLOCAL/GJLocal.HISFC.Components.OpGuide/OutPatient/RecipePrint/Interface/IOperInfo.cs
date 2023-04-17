using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJLocal.HISFC.Components.OpGuide.RecipePrint.Interface
{
    public interface IOperInfo
    {
        int SetOperInfo(Neusoft.FrameWork.Models.NeuObject doct, decimal cost);
    }
}
