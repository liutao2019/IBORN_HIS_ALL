using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePatternInterface
{
    /// <summary>
    /// [功能描述: 业务消息模式：收发管理]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public interface ISender
    {
        /// <summary>
        /// 信息发送
        /// </summary>
        /// <param name="alInfo">所有信息</param>
        /// <param name="operType">操作类别</param>
        /// <param name="infoType">数据类别</param>
        /// <param name="errInfo">错误信息</param>
        /// <returns>-1发送失败</returns>
        int Send(ArrayList alInfo, EnumOperType operType, EnumInfoType infoType, ref string errInfo);

        /// <summary>
        ///  消息发送
        /// </summary>
        /// <param name="singleInfo"></param>
        /// <param name="resultSingleInfo"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        int Send(object singleInfo, ref object resultSingleInfo, ref string errInfo,params object[] appendParams);
    }
}
