using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.BizProcess.Integrate.Operation
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
    public interface IApplicationFormPrint : Neusoft.FrameWork.WinForms.Forms.IReportPrinter
    {
        /// <summary>
        /// �������뵥����
        /// </summary>
        Neusoft.HISFC.Models.Operation.OperationAppllication OperationApplicationForm
        {
            set;
        }

    }
}
