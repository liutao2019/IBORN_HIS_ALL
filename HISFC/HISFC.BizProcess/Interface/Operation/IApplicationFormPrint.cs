using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.Operation
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
    public interface IApplicationFormPrint : FS.FrameWork.WinForms.Forms.IReportPrinter
    {
        /// <summary>
        /// �������뵥����
        /// </summary>
        FS.HISFC.Models.Operation.OperationAppllication OperationApplicationForm
        {
            set;
        }

    }
}
