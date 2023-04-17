using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.DCPInterface
{
    /// <summary>
    /// 取常数信息接口
    /// </summary>
    public interface IConstant : IBase
    {
        /// <summary>
        /// 根据常数类型取常数列表
        /// </summary>
        /// <param name="constType"></param>
        /// <returns></returns>
        ArrayList QueryConstantList(string constType);

        /// <summary>
        /// 根据常数类型取常数列表
        /// </summary>
        /// <param name="constType"></param>
        /// <returns></returns>
        ArrayList QueryConstantList(FS.HISFC.Models.Base.EnumConstant constType);

        /// <summary>
        /// 根据常数类型和ID取常数信息
        /// </summary>
        /// <param name="type">常数类型</param>
        /// <param name="ID">ID</param>
        /// <returns></returns>
        FS.FrameWork.Models.NeuObject GetConstantByTypeAndID(string type, string ID);

    }
}
