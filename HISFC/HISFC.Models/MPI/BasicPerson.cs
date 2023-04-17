using System;
using System.Collections.Generic;
using System.Text;
using Neusoft.HISFC.Object.Base;
using Neusoft.NFC.Object;

namespace Neusoft.HISFC.Object.MPI
{
    /// <summary>
    /// Patient <br></br>
    /// [��������: ���߻���ʵ����Ϣ]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2008]<br></br>
    /// <˵��>
    ///     EMPI �е��� ��Ϊ��Ա������Ϣ
    /// </˵��>    
    /// </summary>
    [Serializable]
    public class BasicPerson : Neusoft.NFC.Object.NeuObject, Neusoft.HISFC.Object.Base.IValid
    {
        public BasicPerson()
        {
 
        }

        #region ����

        /// <summary>
        /// ��ᱣ�պ�
        /// </summary>
        private string ssn = string.Empty;

        /// <summary>
        /// ��������
        /// </summary>
        private System.DateTime birthday;

        /// <summary>
        /// �Ա�
        /// </summary>
        private SexEnumService sex = new SexEnumService();

        /// <summary>
        /// ���� 
        /// </summary>
        private NeuObject country = new NeuObject();

        /// <summary>
        /// ���֤
        /// </summary>
        private string idCard = string.Empty;

        /// <summary>
        /// ����
        /// </summary>
        private NeuObject nationality = new NeuObject();

        /// <summary>
        /// ����
        /// </summary>
        private string dist;

        /// <summary>
        /// Ѫ��
        /// </summary>
        private EnumBloodTypeByABO bloodType = new EnumBloodTypeByABO();

        /// <summary>
        /// ��������
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment oper = new OperEnvironment();
        

        /// <summary>
        /// �Ƿ���Ч��1Ϊ��Ч��0Ϊ��Ч
        /// </summary>
        private bool isValid;

        #endregion

        #region ����

        /// <summary>
        /// ��ᱣ�պ�
        /// </summary>
        public string SSN
        {
            get
            {
                return this.ssn;
            }
            set
            {
                this.ssn = value;
            }
        }

        [System.ComponentModel.DisplayName("��������")]
        [System.ComponentModel.Description("���߳�������")]
        /// <summary>
        /// ��������
        /// </summary>
        public System.DateTime Birthday
        {
            get
            {
                return this.birthday;
            }
            set
            {
                this.birthday = value;
            }
        }

        [System.ComponentModel.DisplayName("�Ա�")]
        [System.ComponentModel.Description("�����Ա�")]
        /// <summary>
        /// �Ա�
        /// </summary>
        public SexEnumService Sex
        {
            get
            {
                return this.sex;
            }
            set
            {
                this.sex = value;
            }
        }      
        
        [System.ComponentModel.DisplayName("����")]
        [System.ComponentModel.Description("���߹���")]
        /// <summary>
        /// ����
        /// </summary>
        public NeuObject Country
        {
            get
            {
                return this.country;
            }
            set
            {
                this.country = value;
            }
        }    

        [System.ComponentModel.DisplayName("���֤��")]
        [System.ComponentModel.Description("�������֤��")]
        /// <summary>
        /// ֤����
        /// </summary>
        public string IDCard
        {
            get
            {
                return this.idCard;
            }
            set
            {
                this.idCard = value;
            }
        }

        [System.ComponentModel.DisplayName("����")]
        [System.ComponentModel.Description("��������")]
        /// <summary>
        /// ����
        /// </summary>
        public NeuObject Nationality
        {
            get
            {
                return this.nationality;
            }
            set
            {
                this.nationality = value;
            }
        }     

        [System.ComponentModel.DisplayName("����")]
        [System.ComponentModel.Description("���߼���")]
        /// <summary>
        /// ����
        /// </summary>
        public string DIST
        {
            get
            {
                return this.dist;
            }
            set
            {
                this.dist = value;
            }
        }      
     
        /// <summary>
        /// Ѫ��
        /// </summary>
        public EnumBloodTypeByABO BloodType
        {
            get
            {
                return this.bloodType;
            }
            set
            {
                this.bloodType = value;
            }
        }     
      
        [System.ComponentModel.DisplayName("����")]
        [System.ComponentModel.Description("��������")]
        /// <summary>
        /// ��������
        /// </summary>
        public new string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment Oper
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

        #endregion

        #region ����

        #region ��¡
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new BasicPerson Clone()
        {
            BasicPerson patient = base.Clone() as BasicPerson;

            patient.Country = this.Country.Clone();
            patient.Nationality = this.Nationality.Clone();
            patient.Sex = this.Sex.Clone();
            patient.Oper = this.Oper.Clone();
            return patient;
        }
        #endregion

        #endregion


        #region IValid ��Ա

        public bool IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                isValid = value;
            }
        }

        #endregion
    }
}
