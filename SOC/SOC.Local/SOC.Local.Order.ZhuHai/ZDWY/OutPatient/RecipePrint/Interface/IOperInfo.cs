using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.RecipePrint.Interface
{
    public interface IOperInfo
    {
        int SetOperInfo(FS.FrameWork.Models.NeuObject doct, decimal cost);
    }
}
