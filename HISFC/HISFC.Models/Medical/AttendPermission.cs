using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.Medical
{
    /// <summary>
    /// ҽ���Ű�Ȩ����
    /// </summary>
    [Serializable]
    public class AttendPermission:Neusoft.FrameWork.Models.NeuObject
    {
        #region ����

        /// <summary>
        /// �Ű���Ա
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject permissionOper = null;

        /// <summary>
        /// �Ű����
        /// </summary>
        private string attendType="";

        /// <summary>
        /// Ȩ���Ű����
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject attendDept = null;

        /// <summary>
        /// Ȩ���Ű���Աְ��
        /// </summary>
        private Base.EmployeeTypeEnumService attendPersonType = null;

        /// <summary>
        /// ��������
        /// </summary>
        private Base.OperEnvironment oper = null;

        #endregion

        #region ����

        /// <summary>
        /// �Ű���Ա
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject PermissionOper
        {
            get
            {
                if(this.permissionOper==null)
                {
                    this.permissionOper = new Neusoft.FrameWork.Models.NeuObject();
                }
                return this.permissionOper;
            }
            set
            {
                this.permissionOper = value;
            }
        }

        /// <summary>
        /// �Ű����
        /// </summary>
        public string AttendType
        {
            get
            {
                return this.attendType;
            }
            set
            {
                this.attendType = value;
            }
        }

        /// <summary>
        /// Ȩ���Ű����
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject AttendDept
        {
            get
            {
                if (this.attendDept == null)
                {
                    this.attendDept = new Neusoft.FrameWork.Models.NeuObject();
                }
                return this.attendDept;
            }
            set
            {
                this.attendDept = value;
            }
        }

        /// <summary>
        /// Ȩ���Ű���Աְ��
        /// </summary>
        public Base.EmployeeTypeEnumService AttendPersonType
        {
            get
            {
                if (this.attendPersonType == null)
                {
                    this.attendPersonType = new Neusoft.HISFC.Models.Base.EmployeeTypeEnumService();
                }
                return this.attendPersonType;
            }
            set
            {
                this.attendPersonType = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public Base.OperEnvironment Oper
        {
            get
            {
                if (this.oper == null)
                {
                    this.oper = new Neusoft.HISFC.Models.Base.OperEnvironment();
                }
                return this.oper;
            }
            set
            {
                this.oper = value;
            }
        }


        #endregion

        #region ����

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public new AttendPermission Clone()
        {
            AttendPermission attend = base.Clone() as AttendPermission;

            this.oper = this.Oper.Clone();
            //this.attendPersonType = this.AttendPersonType.Clone();
            this.attendDept = this.attendDept.Clone();
            this.permissionOper = this.PermissionOper.Clone();

            return attend;
        }

        #endregion
    }
}
