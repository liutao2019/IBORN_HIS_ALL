using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.Operation
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
    public interface IRecordFormPrint : FS.FrameWork.WinForms.Forms.IReportPrinter
    {
        /// <summary>
        /// �����Ǽǵ�����
        /// </summary>
        FS.HISFC.Models.Operation.OperationRecord OperationRecord
        {
            set;
        }
    }
}
