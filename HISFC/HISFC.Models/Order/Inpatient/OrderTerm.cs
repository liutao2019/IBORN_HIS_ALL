using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.Order.Inpatient
{
    /// <summary>
    /// Neusoft.HISFC.Object.Order.InPatient.OrderTerm<br></br>
    /// [��������: סԺҽ������ʵ��(ҽ������)]<br></br>
    /// [�� �� ��: Sunm]<br></br>
    /// [����ʱ��: 2008-06-25]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class OrderTerm : Neusoft.HISFC.Object.Order.Inpatient.Order
    {
        public OrderTerm()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            this.Item.ItemType = Neusoft.HISFC.Object.Base.EnumItemType.Term;
        }

        #region ����

        

        #endregion

        #region ����

        

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new OrderTerm Clone()
        {
            // TODO:  ��� Order.Clone ʵ��
            OrderTerm obj = base.Clone() as OrderTerm;
            
            return obj;
        }

        #endregion
    }
}
