using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.OutpatientFee.Interface.Confirm
{
    public interface IPatientInfo
    {
        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="register"></param>
        void ShowInfo(FS.HISFC.Models.Registration.Register register);

        /// <summary>
        /// 清空数据
        /// </summary>
        /// <returns></returns>
        int Clear();
    }
}
