using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.Operation
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
    public interface IAnaeFormPrint : FS.FrameWork.WinForms.Forms.IReportPrinter
    {
        /// <summary>
        /// ����Ǽǵ�����
        /// </summary>
        FS.HISFC.Models.Operation.AnaeRecord AnaeRecord
        {
            set;
        }
    }
}
