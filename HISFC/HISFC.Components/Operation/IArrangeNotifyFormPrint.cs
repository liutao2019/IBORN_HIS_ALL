using System;
using System.Collections.Generic;
using System.Text;

namespace UFC.Operation
{
    /// <summary>
    /// [��������: ��������֪ͨ����ӡ�ӿ�]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-01-04]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public interface IArrangeNotifyFormPrint : IApplicationFormPrint
    {
        /// <summary>
        /// �Ƿ��ӡ��̨���뵥
        /// </summary>
        bool IsPrintExtendTable
        {
            set;
        }
    }
}
