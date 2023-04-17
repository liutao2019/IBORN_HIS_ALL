using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// <br>ȫ����</br>
    /// <br>[��������: ���ɳ���ʵ����]</br>
    /// <br>[�� �� ��: ����]</br>
    /// <br>[����ʱ��: 2008-10-16]</br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class PublicAbroad : Neusoft.FrameWork.Models.NeuObject
    {
        #region �ֶ�

        /// <summary>
        /// ������Ա
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject personOut = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��������
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject dept = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ְ��
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject post = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ������
        /// </summary>
        private string approveNo = string.Empty;

        /// <summary>
        /// �����
        /// </summary>
        private string polityAuditNo = string.Empty;

        /// <summary>
        /// ��Ч��
        /// </summary>
        private decimal effectDuring = 0;

        /// <summary>
        /// ��������
        /// </summary>
        private string outDuty = string.Empty;

        /// <summary>
        /// ������ʼ����
        /// </summary>
        private DateTime outStartDate;

        /// <summary>
        /// ������������
        /// </summary>
        private DateTime outEndDate;

        /// <summary>
        /// ��������
        /// </summary>
        private decimal outDays = 0;

        /// <summary>
        /// ��������
        /// </summary>
        private DateTime birthdayDate;

        /// <summary>
        /// ����Ա      
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment oper = new Neusoft.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����
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
        /// ��������
        /// </summary>
        public DateTime BirthdayDate
        {
            get
            {
                return birthdayDate;
            }
            set
            {
                birthdayDate = value;
            }
        }


        /// <summary>
        /// ��������
        /// </summary>
        public decimal OutDays
        {
            get
            {
                return outDays;
            }
            set
            {
                outDays = value;
            }
        }

        /// <summary>
        /// ������������
        /// </summary>
        public DateTime OutEndDate
        {
            get
            {
                return outEndDate;
            }
            set
            {
                outEndDate = value;
            }
        }


        /// <summary>
        /// ������ʼ����
        /// </summary>
        public DateTime OutStartDate
        {
            get
            {
                return outStartDate;
            }
            set
            {
                outStartDate = value;
            }
        }


        /// <summary>
        /// ��������
        /// </summary>
        public string OutDuty
        {
            get
            {
                return outDuty;
            }
            set
            {
                outDuty = value;
            }
        }


        /// <summary>
        /// ��Ч��
        /// </summary>
        public decimal EffectDuring
        {
            get
            {
                return effectDuring;
            }
            set
            {
                effectDuring = value;
            }
        }


        /// <summary>
        /// �����
        /// </summary>
        public string PolityAuditNo
        {
            get
            {
                return polityAuditNo;
            }
            set
            {
                polityAuditNo = value;
            }
        }


        /// <summary>
        /// ������
        /// </summary>
        public string ApproveNo
        {
            get
            {
                return approveNo;
            }
            set
            {
                approveNo = value;
            }
        }


        /// <summary>
        /// ְ��
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject Post
        {
            get
            {
                return post;
            }
            set
            {
                post = value;
            }
        }


        /// <summary>
        /// ��������
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject Dept
        {
            get
            {
                return dept;
            }
            set
            {
                dept = value;
            }
        }


        /// <summary>
        /// ������Ա
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject PersonOut
        {
            get
            {
                return personOut;
            }
            set
            {
                personOut = value;
            }
        }
        #endregion

        #region ����

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new PublicAbroad Clone()
        {
            PublicAbroad publicAbr = base.Clone() as PublicAbroad;

            publicAbr.PersonOut = this.PersonOut.Clone();//������Ա
            publicAbr.Dept = this.Dept.Clone();//����
            publicAbr.Post = this.Post.Clone();//ְ��
            publicAbr.Oper = this.Oper.Clone();//����Ա

            return publicAbr;
        }
        #endregion
    }
}
