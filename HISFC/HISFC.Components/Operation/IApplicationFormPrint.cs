using System;
using System.Collections.Generic;
using System.Text;

namespace UFC.Operation
{
    /// <summary>
    /// [��������: �������뵥��ӡ�ӿ�]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-01-04]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public interface IApplicationFormPrint : Neusoft.NFC.Interface.Forms.IReportPrinter
    {
        /// <summary>
        /// �������뵥����
        /// </summary>
        Neusoft.HISFC.Object.Operation.OperationAppllication OperationApplicationForm
        {
            set;
        }

    }
}
