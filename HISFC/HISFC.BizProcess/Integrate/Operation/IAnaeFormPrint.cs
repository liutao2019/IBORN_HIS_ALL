using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.BizProcess.Integrate.Operation
{
    /// <summary>
    /// [��������: ����ǼǴ�ӡ�ӿ�]<br></br>
    /// [�� �� ��: ���]<br></br>
    /// [����ʱ��: 2007-10-23]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public interface IAnaeFormPrint : Neusoft.FrameWork.WinForms.Forms.IReportPrinter
    {
        /// <summary>
        /// ����Ǽǵ�����
        /// </summary>
        Neusoft.HISFC.Models.Operation.AnaeRecord AnaeRecord
        {
            set;
        }
    }
}
