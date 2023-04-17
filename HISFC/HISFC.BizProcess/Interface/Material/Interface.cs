using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.Material
{
    /// <summary>
    /// �������/���ⵥ�ݴ�ӡ
    /// </summary>
    public interface IBillPrint
    {

        /// <summary>
        /// ���ʹ�ӡ����
        /// </summary>
        /// <param name="alInData">����ӡ����</param>
        /// <returns></returns>
        int SetData(List<FS.HISFC.Models.FeeStuff.Input> alInData);

        /// <summary>
        /// ���ͳ������ӡ����
        /// </summary>
        /// <param name="alOutData"></param>
        /// <returns></returns>
        int SetData(List<FS.HISFC.Models.FeeStuff.Output> alOutData);

        /// <summary>
        /// �������ƻ�����ӡ����
        /// </summary>
        /// <param name="alPlan"></param>
        /// <returns></returns>
        int SetData(List<FS.HISFC.Models.FeeStuff.InputPlan> alPlan);

        /// <summary>
        /// �����̵����ӡ����
        /// </summary>
        /// <param name="alCheck"></param>
        /// <returns></returns>
        int SetData(List<FS.HISFC.Models.FeeStuff.Check> alCheck);

        int Print();

        int Prieview();
    }
}
