using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FS.DefultInterfacesAchieve.Material
{
    public class MaterialAchieve : FS.HISFC.BizProcess.Integrate.Material.MaterialInterface
    {
        #region MaterialInterface 成员

        public DataSet MaterialBaseInfo()
        {
            Material.MaterialFunction fun = new MaterialFunction();
            DataSet dt = fun.GetMaterialBaseInfo();
            return dt;
        }

        #endregion
    }
}
