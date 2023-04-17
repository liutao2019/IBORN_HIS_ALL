using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.Registration
{
    /// <summary>
    /// 预约挂号打印{C9ABAAAF-18E3-4553-B5A0-822E527BE685}
    /// </summary>
    public interface IBookingRegisterBill
    {

        int Print(FS.HISFC.Models.Registration.Booking booking);
    }
}
