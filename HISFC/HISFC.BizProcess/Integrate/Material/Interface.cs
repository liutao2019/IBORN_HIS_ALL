using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.BizProcess.Integrate.Material
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
        int SetData(List<Neusoft.HISFC.Models.Material.Input> alInData);

        /// <summary>
        /// ���ͳ������ӡ����
        /// </summary>
        /// <param name="alOutData"></param>
        /// <returns></returns>
        int SetData(List<Neusoft.HISFC.Models.Material.Output> alOutData);

        /// <summary>
        /// �������ƻ�����ӡ����
        /// </summary>
        /// <param name="alPlan"></param>
        /// <returns></returns>
        int SetData(List<Neusoft.HISFC.Models.Material.InputPlan> alPlan);

        /// <summary>
        /// �����̵����ӡ����
        /// </summary>
        /// <param name="alCheck"></param>
        /// <returns></returns>
        int SetData(List<Neusoft.HISFC.Models.Material.Check> alCheck);

        int Print();

        int Prieview();
    }
}
