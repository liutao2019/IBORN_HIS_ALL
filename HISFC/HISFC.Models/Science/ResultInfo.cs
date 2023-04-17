using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.Science
{
    /// <summary>
    /// Neusoft.HISFC.Models.Science.ResultInfo<br></br>
    /// [��������:���гɹ���Ϣʵ�� ]<br></br>
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
    public class ResultInfo : Neusoft.FrameWork.Models.NeuObject, Neusoft.HISFC.Models.Base.IValid
    {

        #region ����

        /// <summary>
        /// ��Ч��׼λ
        /// </summary>
        private bool isValid = true;

        /// <summary>
        /// �ɹ����
        /// </summary>
        private string resultID = string.Empty;

        /// <summary>
        /// �ɹ�����
        /// </summary>
        private string resultName = string.Empty;

        /// <summary>
        /// ����
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject dept = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ר��
        /// </summary>
        private string specialityDept = string.Empty;

        /// <summary>
        /// �ɹ�����
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject resultLevel = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// �ɹ��ȼ�
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject resultGrade = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ǩ������
        /// </summary>
        private DateTime confirmDate;

        /// <summary>
        /// ��������
        /// </summary>
        private DateTime encouragementDate;

        /// <summary>
        /// ������λ
        /// </summary>
        private string encouragementUnit = string.Empty;

        /// <summary>
        /// �������
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject encouragementKind = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ������
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject principal = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        private Base.OperEnvironment operInfo = new Neusoft.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        #region IValid ��Ա

        /// <summary>
        /// ��Ч��׼λ
        /// </summary>
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
        /// �ɹ����
        /// </summary>
        public string ResultID
        {
            get
            {
                return this.resultID;
            }
            set
            {
                this.resultID = value;
            }
        }

        /// <summary>
        /// �ɹ�����
        /// </summary>
        public string ResultName
        {
            get
            {
                return this.resultName;
            }
            set
            {
                this.resultName = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject Dept
        {
            get
            {
                return this.dept;
            }
            set
            {
                this.dept = value;
            }
        }

        /// <summary>
        /// ר��
        /// </summary>
        public string SpecialityDept
        {
            get
            {
                return this.specialityDept;
            }
            set
            {
                this.specialityDept = value;
            }
        }

        /// <summary>
        /// �ɹ�����
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject ResultLevel
        {
            get
            {
                return this.resultLevel;
            }
            set
            {
                this.resultLevel = value;
            }
        }

        /// <summary>
        /// �ɹ��ȼ�
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject ResultGrade
        {
            get
            {
                return this.resultGrade;
            }
            set
            {
                this.resultGrade = value;
            }
        }

        /// <summary>
        /// ǩ������
        /// </summary>
        public DateTime ConfirmDate
        {
            get
            {
                return this.confirmDate;
            }
            set
            {
                this.confirmDate = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public DateTime EncouragementDate
        {
            get
            {
                return this.encouragementDate;
            }
            set
            {
                this.encouragementDate = value;
            }
        }

        /// <summary>
        /// ������λ
        /// </summary>
        public string EncouragementUnit
        {
            get
            {
                return this.encouragementUnit;
            }
            set
            {
                this.encouragementUnit = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject EncouragementKind
        {
            get
            {
                return this.encouragementKind;
            }
            set
            {
                this.encouragementKind = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject Principal
        {
            get
            {
                return this.principal;
            }
            set
            {
                this.principal = value;
            }
        }

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

        public new ResultInfo Clone()
        {
            ResultInfo resultInfo = base.Clone() as ResultInfo;

            //����
            resultInfo.Dept = this.Dept.Clone();

            //�ɹ�����
            resultInfo.ResultLevel = this.ResultLevel.Clone();

            //�ɹ��ȼ�
            resultInfo.ResultGrade = this.ResultGrade.Clone();

            //�������
            resultInfo.EncouragementKind = this.EncouragementKind.Clone();

            //������
            resultInfo.Principal = this.Principal.Clone();

            //����Ա��Ϣ
            resultInfo.OperInfo = this.OperInfo.Clone();

            return resultInfo;
        }

        #endregion
 
    }
}
