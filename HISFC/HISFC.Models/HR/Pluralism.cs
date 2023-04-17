using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// Neusoft.HISFC.Models.HR.Pluralism<br></br>
    /// [��������: ����ְʵ��]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2008-07-10]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class Pluralism : Neusoft.FrameWork.Models.NeuObject, Neusoft.HISFC.Models.Base.IValidState
    {
        #region �ֶ�

        /// <summary>
        /// �������
        /// </summary>
        private string happenNo = "";

        /// <summary>
        /// Ա��ʵ��
        /// </summary>
        private Neusoft.HISFC.Models.Base.Employee employee = new Neusoft.HISFC.Models.Base.Employee();


        /// <summary>
        /// ��ְʱ��
        /// </summary>
        private DateTime startTime;

        /// <summary>
        /// ��ֹʱ��
        /// </summary>
        private DateTime endTime;

        /// <summary>
        /// ��ְ����
        /// </summary>
        private string postKind = "";

        /// <summary>
        /// ѧ������
        /// </summary>
        private string organizationName = "";

        /// <summary>
        /// ְ��
        /// </summary>
        private string postCode = "";

        /// <summary>
        /// ����Ա
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment oper = new Neusoft.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ��Ч��
        /// </summary>
        private Neusoft.HISFC.Models.Base.EnumValidState validState = Neusoft.HISFC.Models.Base.EnumValidState.Valid;

        #endregion

        #region ����

        /// <summary>
        /// �������
        /// </summary>
        public string HappenNo
        {
            get
            {
                return happenNo;
            }
            set
            {
                happenNo = value;
            }
        }

        /// <summary>
        /// Ա��ʵ��
        /// </summary>
        public Neusoft.HISFC.Models.Base.Employee Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
            }
        }


        /// <summary>
        /// ��ְʱ��
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
            }
        }

        /// <summary>
        /// ��ֹʱ��
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
            }
        }

        /// <summary>
        /// ��ְ����
        /// </summary>
        public string PostKind
        {
            get
            {
                return postKind;
            }
            set
            {
                postKind = value;
            }
        }

        /// <summary>
        /// ѧ������
        /// </summary>
        public string OrganizationName
        {
            get
            {
                return organizationName;
            }
            set
            {
                organizationName = value;
            }
        }

        /// <summary>
        /// ְ��
        /// </summary>
        public string PostCode
        {
            get
            {
                return postCode;
            }
            set
            {
                postCode = value;
            }
        }

        /// <summary>
        /// ����Ա
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment Oper
        {
            get
            {
                return oper;
            }
            set
            {
                oper = value;
            }
        }

        /// <summary>
        /// ��Ч��
        /// </summary>
        public Neusoft.HISFC.Models.Base.EnumValidState ValidState
        {
            get
            {
                return validState;
            }
            set
            {
                validState = value;
            }
        }

        #endregion

        #region ����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new Pluralism Clone()
        {
            Pluralism pluralism = base.Clone() as Pluralism;

            pluralism.Employee = this.Employee.Clone();
            pluralism.Oper = this.Oper.Clone();

            return pluralism;
        }
        #endregion
    }
}
