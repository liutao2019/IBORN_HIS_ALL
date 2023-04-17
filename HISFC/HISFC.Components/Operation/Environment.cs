using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.RADT;
using FS.FrameWork.Models;
using FS.HISFC.BizLogic.Operation;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [��������: ������]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-12-01]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public static class Environment
    {
        #region �ֶ�

        private static FS.HISFC.BizProcess.Integrate.Manager integrateManager = new FS.HISFC.BizProcess.Integrate.Manager();
        private static FS.HISFC.BizProcess.Integrate.RADT radtManager = new FS.HISFC.BizProcess.Integrate.RADT();
        private static FS.HISFC.BizProcess.Integrate.Operation.Operation operationManager = new FS.HISFC.BizProcess.Integrate.Operation.Operation();
        private static FS.HISFC.BizLogic.Operation.OpsTableManage tableManager = new FS.HISFC.BizLogic.Operation.OpsTableManage();
        private static FS.HISFC.BizProcess.Integrate.Operation.OpsRecord recordManager = new FS.HISFC.BizProcess.Integrate.Operation.OpsRecord();
        private static FS.HISFC.BizProcess.Integrate.Operation.AnaeRecord anaeManager = new FS.HISFC.BizProcess.Integrate.Operation.AnaeRecord();
        private static System.Collections.ArrayList alAnes;     //��������
        private static System.Collections.ArrayList alPayKind;  //�������
        private static System.Collections.ArrayList alDept;     //�����б�
        //private static FS.FrameWork.Public.ObjectHelper payKindHelper = new FS.FrameWork.Public.ObjectHelper();
        private static FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
        private static FS.HISFC.BizProcess.Integrate.Operation.OperationReport reportManager = new FS.HISFC.BizProcess.Integrate.Operation.OperationReport();
        #endregion

        #region ����
        public static string OperatorID
        {
            get
            {
                return FS.FrameWork.Management.Connection.Operator.ID;
            }
        }

        public static string OperatorDeptID
        {
            get
            {
                return (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).Dept.ID;
            }
        }

        /// <summary>
        /// ���������ڿ���
        /// </summary>
        public static NeuObject OperatorDept
        {
            get
            {
                return (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).Dept;
            }
        }

        public static FS.HISFC.BizProcess.Integrate.Manager IntegrateManager
        {
            get
            {
                return integrateManager;
            }
        }

        public static FS.HISFC.BizProcess.Integrate.RADT RadtManager
        {
            get
            {
                return radtManager;
            }
        }

        public static FS.HISFC.BizProcess.Integrate.Operation.Operation OperationManager
        {
            get
            {
                return operationManager;
            }
        }

        public static FS.HISFC.BizLogic.Operation.OpsTableManage TableManager
        {
            get
            {
                return tableManager;
            }
        }

        public static OpsRecord RecordManager
        {
            get
            {
                return recordManager;
            }
        }

        public static AnaeRecord AnaeManager
        {
            get
            {
                return anaeManager;
            }
        }

        public static bool DesignMode
        {
            get
            {
                return (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv");


            }
        }

        public static FS.FrameWork.WinForms.Classes.Print Print
        {
            get
            {
                return print;
            }
        }

        public static FS.HISFC.BizProcess.Integrate.Operation.OperationReport ReportManager
        {
            get
            {
                return reportManager;
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// �õ�����
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public static NeuObject GetAnes(string id)
        {
            if (alAnes == null)
            {
                alAnes = IntegrateManager.GetConstantList("CASEANESTYPE");//(FS.HISFC.Models.Base.EnumConstant.ANESTYPE);
            }

            foreach (NeuObject obj in alAnes)
            {
                if (obj.ID == id)
                    return obj;
            }

            return null;
        }

        /// <summary>
        /// �õ��������
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Robin   2006-12-12
        public static NeuObject GetPayKind(string id)
        {
            if (alPayKind == null)
            {
                alPayKind = IntegrateManager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYKIND);
            }

            foreach (NeuObject obj in alPayKind)
            {
                if (obj.ID == id)
                    return obj;
            }

            return null;
        }

        /// <summary>
        /// �õ�����
        /// </summary>
        /// <param name="id">����ID</param>
        /// <returns>����</returns>
        /// Robin   2006-12-13
        public static NeuObject GetDept(string id)
        {
            if (alDept == null)
            {
                alDept = IntegrateManager.GetDepartment();
            }

            foreach (NeuObject obj in alDept)
            {
                if (obj.ID == id)
                    return obj;
            }

            return null;
        }

        public static int GetPatientInfomation(FS.HISFC.Models.Operation.OperationAppllication operationAppllication)
        {
            operationAppllication.PatientInfo = RadtManager.GetPatientInfomation(operationAppllication.PatientInfo.ID);

            return 0;
        }

        #endregion
    }
}
