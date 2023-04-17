using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.RADT.Interface.Common
{
    /// <summary>
    /// [功能描述: 患者基本信息，登记信息控件属性相关控制]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public interface IInputControl
    {
        /// <summary>
        /// 是否必须输入
        /// </summary>
        bool IsTextInput
        {
            get;
            set;
        }

        /// <summary>
        /// Tag是否判断
        /// </summary>
        bool IsTagInput
        {
            get;
            set;
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        string InputMsg
        {
            get;
            set;
        }

        /// <summary>
        /// 是否有效录入
        /// </summary>
        /// <returns></returns>
        bool IsValidInput();

        /// <summary>
        /// 是否默认中文输入法
        /// </summary>
        bool IsDefaultCHInput
        {
            get;
            set;
        }

        /// <summary>
        /// 读取信息
        /// </summary>
        /// <param name="path"></param>
        void ReadConfig(System.Xml.Linq.XElement doc);

        /// <summary>
        /// 保存信息
        /// </summary>
        /// <param name="path"></param>
        void SaveConfig(System.Xml.Linq.XElement doc);
    }
}
