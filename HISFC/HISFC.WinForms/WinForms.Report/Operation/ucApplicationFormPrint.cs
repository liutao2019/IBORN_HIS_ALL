using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.Operation
{
    /// <summary>
    /// [��������: ���뵥��ӡ���]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-01-03]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucApplicationFormPrint : UserControl, FS.HISFC.BizProcess.Interface.Operation.IApplicationFormPrint
    {
        public ucApplicationFormPrint()
        {
            InitializeComponent();
        }

        #region IApplicationForm ��Ա

        public FS.HISFC.Models.Operation.OperationAppllication OperationApplicationForm
        {
            set
            { 
                

            }
        }

        #endregion

        #region IReportPrinter ��Ա

        public int Export()
        {
            return 0;
        }

        public int Print()
        {
            return 0;
        }

        public int PrintPreview()
        {
            return 0;
        }

        #endregion

    }
}
