using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Business
{
    public interface IService
    {
        string InterfaceID
        {
            get;
        }

        string ErrorMsg
        {
            get;
            set;
        }
    }
}
