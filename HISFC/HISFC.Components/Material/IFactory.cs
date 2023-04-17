using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Components.Material
{
    public interface IFactory
    {
        FS.HISFC.Components.Material.IMatManager GetInInstance(FS.FrameWork.Models.NeuObject inPrivType, FS.HISFC.Components.Material.In.ucMatIn ucMatManager);


        FS.HISFC.Components.Material.IMatManager GetOutInstance(FS.FrameWork.Models.NeuObject outPrivType, FS.HISFC.Components.Material.Out.ucMatOut ucMatManager);      
    }
}
