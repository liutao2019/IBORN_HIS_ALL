using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Integrate.Operation
{
    /// <summary>
    /// [��������: ������ҵ���]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-12-31]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class AnaeRecord : FS.HISFC.BizLogic.Operation.AnaeRecord
    {
        #region �ֶ�
        private Operation operation = new Operation();
        #endregion

        #region ����
        protected override FS.HISFC.BizLogic.Operation.Operation operationManager
        {
            get
            {
                return this.operation;
            }
        }
        #endregion
    }
}
