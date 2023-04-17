using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.DCPInterface
{
    /// <summary>
    /// ȡ������Ϣ�ӿ�
    /// </summary>
    public interface IConstant : IBase
    {
        /// <summary>
        /// ���ݳ�������ȡ�����б�
        /// </summary>
        /// <param name="constType"></param>
        /// <returns></returns>
        ArrayList QueryConstantList(string constType);

        /// <summary>
        /// ���ݳ�������ȡ�����б�
        /// </summary>
        /// <param name="constType"></param>
        /// <returns></returns>
        ArrayList QueryConstantList(FS.HISFC.Models.Base.EnumConstant constType);

        /// <summary>
        /// ���ݳ������ͺ�IDȡ������Ϣ
        /// </summary>
        /// <param name="type">��������</param>
        /// <param name="ID">ID</param>
        /// <returns></returns>
        FS.FrameWork.Models.NeuObject GetConstantByTypeAndID(string type, string ID);

    }
}
