using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.BizProcess.Interface.Fee
{
    /// <summary>
    /// 预约入院接口
    /// </summary>
    public interface IPrePayIn
    {
        /// <summary>
        /// 是否显示控件
        /// </summary>
        bool IsShowButton
        {
            get;
            set;
        }
        /// <summary>
        /// 患者信息
        /// </summary>
        /// <returns></returns>
        FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 显示控件
        /// </summary>
        void ShowDialog();

        
    }
}
