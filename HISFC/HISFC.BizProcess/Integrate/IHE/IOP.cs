using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.BizProcess.Integrate.IHE
{
    /// <summary>
    /// [����������OP�ӿ�]
    /// [�� �� �ߣ�Ѧ�Ľ�]
    /// [����ʱ�䣺2010-03-08]
    /// </summary>
    public interface IOP
    {
        /// <summary>
        /// ����ҽ��
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        int PlaceOrder(Neusoft.HISFC.Models.Order.Order order);

        /// <summary>
        /// ����ҽ��
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        int PlaceOrder(List<Neusoft.HISFC.Models.Order.Inpatient.Order> items);

        /// <summary>
        /// ����ҽ��
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        int PlaceOrder(System.Collections.ArrayList items);
    }
}
