using System;
using System.Collections.Generic;
using System.Text;

namespace UFC.Operation
{
    /// <summary>
    /// [��������: �����ǼǴ�ӡ�ӿ�]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-01-08]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public interface IRecordFormPrint : Neusoft.NFC.Interface.Forms.IReportPrinter
    {
        /// <summary>
        /// �����Ǽǵ�����
        /// </summary>
        Neusoft.HISFC.Object.Operation.OperationRecord OperationRecord
        {
            set;
        }
    }
}
