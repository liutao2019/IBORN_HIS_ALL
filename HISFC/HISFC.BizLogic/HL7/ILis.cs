using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizLogic.HL7
{
    /// <summary>
    /// [功能描述: LIS医嘱接口]<br></br>
    /// [创 建 者: 王铁全]<br></br>
    /// [创建时间: 2007-05-11]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public interface ILis
    {
        int PlaceOrder(FS.HISFC.Models.Order.Order order);
        int PlaceOrder(ICollection<FS.HISFC.Models.Order.Order> orders);
        bool CheckOrder(FS.HISFC.Models.Order.Order order);
        int SetPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo);
        int Commit();
        int Rollback();

    }

}
