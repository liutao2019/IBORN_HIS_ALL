using System;
using System.Collections.Generic;
using System.Text;
using Neusoft.HISFC.Object.Base;
using Neusoft.NFC.Object;

namespace Neusoft.HISFC.Object.RADT
{
    /// <summary>
    /// [��������: ������Ϣ�ۺ�ʵ��]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-04-26]<br></br>
    /// <�޸ļ�¼/>
    /// </summary> 
    public class AllergyInfo:Allergy
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public AllergyInfo()
        {

        }

        #region  ����

        /// <summary>
        /// סԺ�Ż������ﲡ����
        /// </summary>
        private string patientNO;
        /// <summary>
        /// ��������
        /// </summary>
        private ServiceTypes patientType;
        /// <summary>
        /// ��Ч��
        /// </summary>
        private bool validState;
        /// <summary>
        /// ��������Ϣ
        /// </summary>
        private OperEnvironment oper = new OperEnvironment();
        /// <summary>
        /// ��������Ϣ
        /// </summary>
        private OperEnvironment cancelOper = new OperEnvironment();

        #endregion

        #region  ����

        /// <summary>
        /// סԺ�Ż������ﲡ����
        /// </summary>
        public string PatientNO
        {
            get
            {
                return this.patientNO;
            }
            set
            {
                this.patientNO = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public ServiceTypes PatientType
        {
            get
            {
                return this.patientType;
            }
            set 
            {
                this.patientType = value;
            }
        }

        /// <summary>
        /// ��Ч��
        /// </summary>
        public bool ValidState
        {
            get
            {
                return this.validState;
            }
            set
            {
                this.validState = value;
            }
        }

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        public OperEnvironment Oper
        {
            get 
            {
                return this.oper;
            }
            set 
            {
                this.oper = value;
            }
        }

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        public OperEnvironment CancelOper
        {
            get 
            {
                return this.cancelOper;
            }
            set 
            {
                this.cancelOper = value;
            }
        }

        #endregion

        #region  ����

        public new AllergyInfo clone()
        {
            AllergyInfo obj = base.Clone() as AllergyInfo;
            obj.oper = this.oper.Clone();
            obj.cancelOper = this.cancelOper.Clone();
            return obj;
        }

        #endregion
    }
}
