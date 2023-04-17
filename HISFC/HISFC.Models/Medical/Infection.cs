using System;
using System.Collections.Generic;

namespace Neusoft.HISFC.Models.Medical
{
    /// <summary>
    /// [��������: ��Ⱦʵ��]<br></br>
    /// [�� �� ��: ����ΰ]<br></br>
    /// [����ʱ��: 2006-09-05]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=' ����ܰ'
    ///		�޸�ʱ��=2008��04��03'
    ///		�޸�Ŀ��='ҽԺ��Ⱦ�ǼǱ�'
    ///		�޸�����='ʵ�������Ⱦ��λ.��Ⱦԭ��.��Դ��  ��Щ������infection�������Բ���ͬһ������'
    ///  />
    /// </summary>
    [Serializable]
    public class Infection : Neusoft.FrameWork.Models.NeuObject, Base.IValid
    {

        public Infection()
        {
            // TODO: �ڴ˴���ӹ��캯���߼�
        }

        #region ����

        /// <summary>
        /// ҵ�����
        /// </summary>
        private string infectionId;

        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        private Neusoft.HISFC.Models.RADT.Patient patient = new Neusoft.HISFC.Models.RADT.Patient();
        /// <summary>
        /// ��Ժ����
        /// </summary>
        private DateTime inDate;
        /// <summary>
        /// ��Ժ���
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject inIcd = new Neusoft.FrameWork.Models.NeuObject();
        /// <summary>
        /// ��Ժ����
        /// </summary>
        private DateTime outDate;
        /// <summary>
        /// ��Ժ��ϴ���
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject outIcd = new Neusoft.FrameWork.Models.NeuObject();
        /// <summary>
        /// סԺ����
        /// </summary>
        private string inDept;
        /// <summary>
        /// ����
        /// </summary>
        private string bedNo;
        /// <summary>
        /// ת��
        /// </summary>
        private string zg;


        /// <summary>
        /// ��Ⱦ����
        /// </summary>
        private DateTime infectDate;
        /// <summary>
        /// ��Ⱦ��ϴ���
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject infectionIcd = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        private bool isOp;
        /// <summary>
        /// ��������
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject opsCode = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        private bool isUrgop;
        /// <summary>
        /// �����п�
        /// </summary>
        private string inciType;
        /// <summary>
        /// ��������
        /// </summary>
        private DateTime opsDate;
        /// <summary>
        /// �Ƿ�����������
        /// </summary>
        private string endotrachealAnae;
        /// <summary>
        /// ��Ⱦ�ٴ�֢״
        /// </summary>
        private string infectSymptom;
        /// <summary>
        /// ��Ⱦ��������ϵ
        /// </summary>
        private string infectDie;
        /// <summary>
        /// ��ʹ�õÿ�����
        /// </summary>
        private List<string> antibioticId = new List<string>();
        /// <summary>
        /// ����������
        /// </summary>
        private string antibioticNum;
        /// <summary>
        /// �Ǽ��˱���
        /// </summary>
        private string operCode;
        /// <summary>
        ///�Ǽ�����
        /// </summary>
        private DateTime operDate;
        /// <summary>
        /// ��Ч״̬
        /// </summary>
        private bool isValid;
        /// <summary>
        /// ��Ⱦ��λ
        /// </summary>

        private List<string> infectPart = new List<string>();
        /// <summary>
        /// ��Ⱦԭ��
        /// </summary>
        private List<string> infectReason = new List<string>();

        #endregion

        #region ����

        /// <summary>
        /// ҵ�����
        /// </summary>
        public string InfectionId
        {
            get
            {
                return this.infectionId;
            }
            set
            {
                this.infectionId = value;
            }
        }
        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        public Neusoft.HISFC.Models.RADT.Patient Patient
        {
            get
            {
                return this.patient;
            }
            set
            {
                this.patient = value;
            }
        }

        /// <summary>
        /// ��Ժ����
        /// </summary>
        public DateTime InDate
        {
            get
            {
                return this.inDate;
            }
            set
            {
                this.inDate = value;
            }

        }
        /// <summary>
        /// ��Ժ��ϴ���
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject InIcd
        {
            get
            {
                return this.inIcd;
            }
            set
            {
                this.inIcd = value;
            }
        }

        /// <summary>
        /// ��Ժ����
        /// </summary>
        public DateTime OutDate
        {
            get
            {
                return this.outDate;
            }
            set
            {
                this.outDate = value;
            }
        }
        /// <summary>
        /// ��Ժ��ϴ���
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject OutIcd
        {
            get
            {
                return this.outIcd;
            }
            set
            {
                this.outIcd = value;
            }
        }
        /// <summary>
        /// סԺ����
        /// </summary>
        public string InDept
        {
            get
            {
                return this.inDept;
            }
            set
            {
                this.inDept = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string BedNo
        {
            get
            {
                return this.bedNo;
            }
            set
            {
                this.bedNo = value;
            }
        }
        /// <summary>
        /// <summary>
        /// ת��
        public string ZG
        {
            get
            {
                return this.zg;
            }
            set
            {
                this.zg = value;
            }
        }

        /// </summary>
        /// ��Ⱦ����
        /// </summary>
        public DateTime InfectDate
        {
            get
            {
                return this.infectDate;
            }
            set
            {
                this.infectDate = value;
            }
        }
        /// <summary>
        /// ��Ⱦ��ϴ���
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject InfectionIcd
        {
            get
            {
                return this.infectionIcd;
            }
            set
            {
                this.infectionIcd = value;
            }
        }

        /// <summary>
        /// �Ƿ�����
        /// </summary>

        public bool IsOp
        {
            get
            {
                return this.isOp;
            }
            set
            {
                this.isOp = value;
            }

        }
        /// <summary>
        /// ��������
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject OpsCode
        {
            get
            {
                return this.opsCode;
            }
            set
            {
                this.opsCode = value;
            }
        }
        /// <summary>
        /// �Ƿ�������
        /// </summary>
        public bool IsUrgop
        {
            get
            {
                return this.isUrgop;
            }
            set
            {
                this.isUrgop = value;
            }

        }
        /// <summary>
        /// �п�����
        /// </summary>
        public string InciType
        {
            get
            {
                return this.inciType;
            }
            set
            {
                this.inciType = value;
            }
        }
        public DateTime OpsDate
        {
            get
            {
                return this.opsDate;
            }
            set
            {
                this.opsDate = value;
            }
        }
        /// <summary>
        /// �Ƿ�����������
        /// </summary>
        public string EndotrachealAnae
        {
            get
            {
                return this.endotrachealAnae;
            }
            set
            {
                this.endotrachealAnae = value;
            }
        }
        /// <summary>
        /// ��Ⱦ�ٴ�֢״
        /// </summary>
        public string InfectSymptom
        {
            get
            {
                return this.infectSymptom;
            }
            set
            {
                this.infectSymptom = value;
            }

        }
        /// <summary>
        /// ��Ⱦ��������ϵ
        /// </summary>
        public string InfectDie
        {
            get
            {
                return this.infectDie;
            }
            set
            {
                this.infectDie = value;
            }
        }

        /// <summary>
        /// ��ʹ�õÿ�����
        /// </summary>
        public List<string> AntibioticId
        {
            get
            {
                return this.antibioticId;
            }
            set
            {
                this.antibioticId = value;
            }
        }
        /// <summary>
        /// ����������
        /// </summary>
        public string AntibioticNum
        {
            get
            {
                return this.antibioticNum;
            }
            set
            {
                this.antibioticNum = value;
            }
        }

        /// <summary>
        /// �Ǽ��˱���
        /// </summary>
        public string OperCode
        {
            get
            {
                return this.operCode;
            }
            set
            {
                this.operCode = value;
            }

        }
        /// <summary>
        ///�Ǽ�����
        /// </summary>
        public DateTime OperDate
        {
            get
            {
                return this.operDate;
            }
            set
            {
                this.operDate = value;
            }

        }

        /// <summary>
        ///��Ⱦ��λ
        /// </summary>
        public List<string> InfectPart
        {
            get
            {
                return this.infectPart;
            }
            set
            {
                this.infectPart = value;
            }
        }

        /// <summary>
        ///��Ⱦԭ��
        /// </summary>
        public List<string> InfectReason
        {
            get
            {
                return this.infectReason;
            }
            set
            {
                this.infectReason = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns>���ص�ǰ�����ʵ������</returns>
        public new Infection Clone()
        {
            Infection infection = base.Clone() as Infection;
            infection.opsCode = this.opsCode.Clone();
            infection.infectionIcd = this.infectionIcd.Clone();
            infection.outIcd = this.outIcd.Clone();
            infection.inIcd = this.inIcd.Clone();
            infection.patient = this.patient.Clone();
            return infection;

        }

        #endregion

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
    }
}

