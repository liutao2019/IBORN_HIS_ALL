using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.DCP
{
    [System.Serializable]
    public class Common : ProcessBase, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        #region �ӿڱ���

        /// <summary>
        /// ��ѯ������Ϣ�ӿ�
        /// </summary>
        FS.SOC.HISFC.BizProcess.DCPInterface.IDepartment IQueryDeptmentInstance;

        /// <summary>
        /// ��ѯ��Ա��Ϣ�ӿ�
        /// </summary>
        FS.SOC.HISFC.BizProcess.DCPInterface.IEmployee IQueryEmployeeInstance;
        /// <summary>
        /// ������Ϣ�ӿ�
        /// </summary>
        FS.SOC.HISFC.BizProcess.DCPInterface.IConstant IConstantInstance;

        #endregion

        #region ˽�з���

        /// <summary>
        /// �����ȡʵ�ֿ��Ҳ�ѯ�ӿ�
        /// </summary>
        private void CreateIDeptment()
        {
            if (this.IQueryDeptmentInstance == null)
            {
                this.IQueryDeptmentInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(),
                    typeof(FS.SOC.HISFC.BizProcess.DCPInterface.IDepartment)) as FS.SOC.HISFC.BizProcess.DCPInterface.IDepartment;
            }

            if (this.IQueryDeptmentInstance == null)
            {
                throw new Exception(this.GetType().ToString() + "�еĽӿ�\n" + "FS.SOC.HISFC.BizProcess.DCPInterface.IDeptment" + "\nû��ά������,�������Ա��ϵ");
            }
        }

        /// <summary>
        /// �����ȡʵ����Ա��ѯ�ӿ�
        /// </summary>
        private void CreateIEmployee()
        {
            if (this.IQueryEmployeeInstance == null)
            {
                this.IQueryEmployeeInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(),
                    typeof(FS.SOC.HISFC.BizProcess.DCPInterface.IEmployee)) as FS.SOC.HISFC.BizProcess.DCPInterface.IEmployee;
            }

            if (this.IQueryEmployeeInstance == null)
            {
                throw new Exception(this.GetType().ToString() + "�еĽӿ�\n" + "FS.SOC.HISFC.BizProcess.DCPInterface.IEmployee" + "\nû��ά������,�������Ա��ϵ");
            }
        }

        /// <summary>
        /// �����ȡʵ�ֳ�����ѯ�ӿ�
        /// </summary>
        /// <param name="interfaceObj"></param>
        /// <returns></returns>
        private void CreateIConstant()
        {
            if (this.IConstantInstance == null)
            {
                this.IConstantInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(),
                    typeof(FS.SOC.HISFC.BizProcess.DCPInterface.IConstant)) as FS.SOC.HISFC.BizProcess.DCPInterface.IConstant;
            }

            if (this.IConstantInstance == null)
            {
                throw new Exception(this.GetType().ToString() + "�еĽӿ�\n" + "FS.SOC.HISFC.BizProcess.DCPInterface.IConstant" + "\nû��ά������,�������Ա��ϵ");
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ������п���,������Ч��Ч
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryDeptAllValidAndUnvalid()
        {
            this.CreateIDeptment();

            ArrayList alTemp = this.IQueryDeptmentInstance.QueryDeptAllValidAndUnvalid();

            if (alTemp == null)
            {
                this.ErrorMsg = this.IQueryDeptmentInstance.Msg;
            }

            return alTemp;
        }

        /// <summary>
        /// ���������Ч����
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryDeptAllValid()
        {
            this.CreateIDeptment();

            ArrayList alTemp = this.IQueryDeptmentInstance.QueryDeptAllValid();

            if (alTemp == null)
            {
                this.ErrorMsg = this.IQueryDeptmentInstance.Msg;
            }

            return alTemp;
        }

        /// <summary>
        /// ���ݿ������Ͳ�ѯ�����б�
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ArrayList QueryDeptByType(FS.HISFC.Models.Base.EnumDepartmentType type)
        {
            this.CreateIDeptment();

            ArrayList alTemp = this.IQueryDeptmentInstance.QueryDeptByType(type);

            if (alTemp == null)
            {
                this.ErrorMsg = this.IQueryDeptmentInstance.Msg;
            }

            return alTemp;
        }

        /// <summary>
        /// ���ݿ��ұ��ȡ������Ϣ
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.Department GetDeptByID(string deptID)
        {
            this.CreateIDeptment();

            FS.HISFC.Models.Base.Department tempDept = this.IQueryDeptmentInstance.GetDeptByID(deptID);

            if (tempDept == null)
            {
                this.ErrorMsg = this.IQueryDeptmentInstance.Msg;
            }

            return tempDept;
        }

        #endregion

        #region ��Ա

        /// <summary>
        /// ��ѯ����Ա��������Ч��Ч
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryEmployeeAllValidAndUnvalid()
        {
            this.CreateIEmployee();

            ArrayList alTemp = this.IQueryEmployeeInstance.QueryEmployeeAllValidAndUnvalid();

            if (alTemp == null)
            {
                this.ErrorMsg = this.IQueryEmployeeInstance.Msg;
            }

            return alTemp;
        }

        /// <summary>
        /// ��ѯ������ЧԱ��
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryEmployeeAllValid()
        {
            this.CreateIEmployee();

            ArrayList alTemp = this.IQueryEmployeeInstance.QueryEmployeeAllValid();

            if (alTemp == null)
            {
                this.ErrorMsg = this.IQueryEmployeeInstance.Msg;
            }

            return alTemp;
        }

        /// <summary>
        /// ������Ա����ȡ������Ա
        /// </summary>
        /// <param name="emplType">��Ա����</param>
        /// <returns></returns>
        public ArrayList QueryEmployeeByType(FS.HISFC.Models.Base.EnumEmployeeType emplType)
        {
            this.CreateIEmployee();

            ArrayList alTemp = this.IQueryEmployeeInstance.QueryEmployeeByType(emplType);

            if (alTemp == null)
            {
                this.ErrorMsg = this.IQueryEmployeeInstance.Msg;
            }

            return alTemp;
        }

        /// <summary>
        /// ��ѯĳ������������Ա
        /// </summary>
        /// <param name="deptID">���ұ���</param>
        /// <returns></returns>
        public ArrayList QueryEmployeeByDeptID(string deptID)
        {
            this.CreateIEmployee();

            ArrayList alTemp = this.IQueryEmployeeInstance.QueryEmployeeByDeptID(deptID);

            if (alTemp == null)
            {
                this.ErrorMsg = this.IQueryEmployeeInstance.Msg;
            }

            return alTemp;
        }

        /// <summary>
        /// ��ѯĳ������ĳ�����͵���Ա
        /// </summary>
        /// <param name="emplType">��Ա����</param>
        /// <param name="deptID">���ұ���</param>
        /// <returns></returns>
        public ArrayList QueryEmployeeByTypeAndDeptID(FS.HISFC.Models.Base.EnumEmployeeType emplType, string deptID)
        {
            this.CreateIEmployee();

            ArrayList alTemp = this.IQueryEmployeeInstance.QueryEmployeeByTypeAndDeptID(emplType, deptID);

            if (alTemp == null)
            {
                this.ErrorMsg = this.IQueryEmployeeInstance.Msg;
            }

            return alTemp;
        }

        /// <summary>
        /// ����Ա������ȡԱ��������Ϣ
        /// </summary>
        /// <param name="ID">��Ա����</param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.Employee GetEmployeeInfoByID(string ID)
        {
            this.CreateIEmployee();

            FS.HISFC.Models.Base.Employee tempEmployee = this.IQueryEmployeeInstance.GetEmployeeInfoByID(ID);

            if (tempEmployee == null)
            {
                this.ErrorMsg = this.IQueryEmployeeInstance.Msg;
            }

            return tempEmployee;
        }

        #endregion

        #region ����

        /// <summary>
        /// ���ݳ�������ȡ�����б�
        /// </summary>
        /// <param name="constType"></param>
        /// <returns></returns>
        public ArrayList QueryConstantList(string constType)
        {
            this.CreateIConstant();

            ArrayList alTemp = this.IConstantInstance.QueryConstantList(constType);

            if (alTemp == null)
            {
                this.ErrorMsg = this.IConstantInstance.Msg;
            }

            return alTemp;
        }

        /// <summary>
        /// ���ݳ�������ȡ�����б�
        /// </summary>
        /// <param name="constType"></param>
        /// <returns></returns>
        public ArrayList QueryConstantList(FS.HISFC.Models.Base.EnumConstant constType)
        {
            ArrayList alTemp = this.QueryConstantList(constType.ToString());

            if (alTemp == null)
            {
                this.ErrorMsg = this.IConstantInstance.Msg;
            }

            return alTemp;
        }

        /// <summary>
        /// ���ݳ������ͺ�IDȡ������Ϣ
        /// </summary>
        /// <param name="type">��������</param>
        /// <param name="ID">ID</param>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetConstantByTypeAndID(string type, string ID)
        {
            this.CreateIConstant();

            FS.FrameWork.Models.NeuObject temp = this.IConstantInstance.GetConstantByTypeAndID(type, ID);

            if (temp == null)
            {
                this.ErrorMsg = this.IConstantInstance.Msg;
            }

            return temp;
        }

        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] t = new Type[]{     
                    typeof(FS.SOC.HISFC.BizProcess.DCPInterface.IDepartment),
                    typeof(FS.SOC.HISFC.BizProcess.DCPInterface.IEmployee),
                    typeof(FS.SOC.HISFC.BizProcess.DCPInterface.IConstant)           
                                                };

                return t;
            }
        }

        #endregion
    }
}
