using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder
{
    /// <summary>
    /// IOutPateintTree<br></br>
    /// [功能描述: 门诊医生站患者列表]<br></br>
    /// [创 建 者: houwb]<br></br>
    /// [创建时间: 2012-10-12]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public interface IOutPateintTree
    {
        string Err
        {
            set;
            get;
        }

        /// <summary>
        /// 添加到未诊列表树节点之前
        /// </summary>
        /// <param name="regObj"></param>
        /// <returns></returns>
        int BeforeAddToTree(FS.HISFC.Models.Registration.Register regObj);

        /// <summary>
        /// 添加到未诊列表树节点之后
        /// </summary>
        /// <param name="regObj"></param>
        /// <returns></returns>
        int AfterAddToTree(FS.HISFC.Models.Registration.Register regObj);

        ///// <summary>
        ///// 叫号
        ///// </summary>
        ///// <returns></returns>
        //int Call();

        ///// <summary>
        ///// 延迟叫号
        ///// </summary>
        ///// <returns></returns>
        //int DelayCall();

        ///// <summary>
        ///// 初始化
        ///// </summary>
        ///// <returns></returns>
        //int Init();

        ///// <summary>
        ///// 刷新列表
        ///// </summary>
        ///// <returns></returns>
        //int Refresh();

        ///// <summary>
        ///// 挂号患者实体
        ///// </summary>
        //FS.HISFC.Models.Registration.Register Register
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// 进诊
        ///// </summary>
        ///// <returns></returns>
        //int Triage();

        ///// <summary>
        ///// 取消进诊
        ///// </summary>
        ///// <returns></returns>
        //int CancelTriage();
    }
}
