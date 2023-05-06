using System;
using System.Collections.Generic;
using System.Text;

namespace HISTIMEJOB
{
    /// <summary>
    /// IJob 的摘要说明。
    /// </summary>
    public partial interface IJob
    {

        string Message
        {
            get;
        }


        int Start();

    }
}
