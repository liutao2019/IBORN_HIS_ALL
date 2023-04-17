using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.IHE
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
        int PlaceOrder(FS.HISFC.Models.Order.Order order);

        /// <summary>
        /// ����ҽ��
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        int PlaceOrder(List<FS.HISFC.Models.Order.Inpatient.Order> items);

        /// <summary>
        /// ����ҽ��
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        int PlaceOrder(System.Collections.ArrayList items);
    }
}
