using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Components.Pharmacy.Base
{
    public interface IPharmacyBizManager
    {
        FS.SOC.HISFC.Components.Pharmacy.Base.IBaseBiz GetBizImplement(FS.FrameWork.Models.NeuObject inputType);

        SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataChooseList GetDataChooseListImplement(FS.FrameWork.Models.NeuObject inputType);

        SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataDetail GetDataDetailImplement(FS.FrameWork.Models.NeuObject inputType);
    }
}
