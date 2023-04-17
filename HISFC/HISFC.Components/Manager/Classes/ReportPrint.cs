using System;
using System.Collections.Generic;
using System.Text;

namespace UFC.Manager.Classes
{
    /// <summary>
    /// [��������: �����ӡ����ʵ����]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-11-27]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class ReportPrint
    {
        public string ContainerDllName;
        public string ContainerContorl;
        public List<ReportPrintControl> ReportPrintControls=new List<ReportPrintControl>();

        public void Add(string dllName, string controlName,short index,string memo)
        {
            ReportPrintControl reportPrintControl = new ReportPrintControl();
            reportPrintControl.DllName = dllName;
            reportPrintControl.ControlName = controlName;
            reportPrintControl.Index = index;
            reportPrintControl.Memo = memo;

            this.ReportPrintControls.Add(reportPrintControl);
        }
    }

    /// <summary>
    /// [��������: �����ӡ�ؼ�ʵ����]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-11-27]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class ReportPrintControl
    {
        public string DllName;
        public string ControlName;
        public string InterfaceName;
        public short Index;
        public string Memo;
    };
}
