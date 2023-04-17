using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.Science
{    
    /// <summary>
    /// Neusoft.HISFC.Models.Science.EmployeePost<br></br>
    /// [��������:ѧ����ְ��Ϣʵ�� ]<br></br>
    /// [�� �� ��: �·�]<br></br>
    /// [����ʱ��: 2008-05-21]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class EmployeePost : Neusoft.FrameWork.Models.NeuObject, Neusoft.HISFC.Models.Base.IValid
    {
        #region ����

        /// <summary>
        /// ��Ա������Ϣ
        /// </summary>
        private Neusoft.HISFC.Models.Base.Employee employeeBase = new Neusoft.HISFC.Models.Base.Employee();

        /// <summary>
        /// ר��
        /// </summary>
        private string specialDept = string.Empty;

        /// <summary>
        /// һ����ְ����
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject emplorageFirst = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ������ְ����
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject emplorageSecond = new Neusoft.FrameWork.Models.NeuObject();
        
        /// <summary>
        /// ѧ��ְ��
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject sCIPost = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime beginDate;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime endDate;

        /// <summary>
        /// ��Ч��
        /// </summary>
        private bool isValid = true;

        private Base.OperEnvironment operInfo = new Neusoft.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// ��Ա������Ϣ
        /// </summary>
        public Neusoft.HISFC.Models.Base.Employee EmployeeBase
        {
            get
            {
                return this.employeeBase;
            }
            set
            {
                this.employeeBase = value;
            }
        }

        /// <summary>
        /// ר��
        /// </summary>
        public string SpecialDept
        {
            get
            {
                return this.specialDept;
            }
            set
            {
                this.specialDept = value;
            }
        }

        /// <summary>
        /// һ����ְ����
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject EmplorageFirst
        {
            get
            {
                return this.emplorageFirst;
            }
            set
            {
                this.emplorageFirst = value;
            }
        }

        /// <summary>
        /// ������ְ����
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject EmplorageSecond
        {
            get
            {
                return this.emplorageSecond;
            }
            set
            {
                this.emplorageSecond = value;
            }
        }

        /// <summary>
        /// ѧ��ְ��
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject SCIPost
        {
            get
            {
                return this.sCIPost;
            }
            set
            {
                this.sCIPost = value;
            }
        }

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        public DateTime BeginDate
        {
            get
            {
                return this.beginDate;
            }
            set
            {
                this.beginDate = value;
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                return this.endDate;
            }
            set
            {
                this.endDate = value;
            }
        }

        #region IValid ��Ա

        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
            set
            {
                this.isValid = value;
            }
        }

        #endregion

        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        public Base.OperEnvironment OperInfo
        {
            get
            {
                return this.operInfo;
            }
            set
            {
                this.operInfo = value;
            }
        }

        #endregion

        #region ����

        public new EmployeePost Clone()
        {
            EmployeePost employeePost = base.Clone() as EmployeePost;
            employeePost.EmplorageFirst = this.EmplorageFirst.Clone();
            employeePost.EmplorageSecond = this.EmplorageSecond.Clone();
            employeePost.EmployeeBase = this.EmployeeBase.Clone();
            employeePost.SCIPost = this.SCIPost.Clone();

            return employeePost;
        }

        #endregion
    }
}
