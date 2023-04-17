using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Integrate.Operation
{
    /// <summary>
    /// [��������: ���������]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-01-15]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class OperationReport : FS.HISFC.BizLogic.Operation.OpsReport
    {
        FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        private FS.HISFC.BizProcess.Integrate.Manager manager = new Manager();

        protected override FS.HISFC.Models.Base.Department GetDeptmentById(string id)
        {
            return this.deptManager.GetDeptmentById(id);    
        }

        protected override FS.HISFC.Models.Base.Employee GetEmployee(string id)
        {
            return this.manager.GetEmployeeInfo(id);
        }
    }
}
