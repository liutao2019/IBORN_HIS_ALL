using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.Science
{
    /// <summary>
    /// Neusoft.HISFC.Models.Science.ConferenceInfo<br></br>
    /// [��������: ѧ������ʵ�� ]<br></br>
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
    public class ConferenceInfo : Neusoft.FrameWork.Models.NeuObject, Neusoft.HISFC.Models.Base.IValid
    {
        #region ����

        /// <summary>
        /// ���鿪ʼʱ��
        /// </summary>
        private DateTime start;

        /// <summary>
        /// �������ʱ��
        /// </summary>
        private DateTime end;

        /// <summary>
        /// ����
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject dept = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����ص�
        /// </summary>
        private string locate = string.Empty;

        /// <summary>
        /// ���鼶��
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject level = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ���Ľ�����ʽ
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject paperCommunion = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����ƪ��
        /// </summary>
        private int paperCopies = 0;

        /// <summary>
        /// �������Դ
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject source = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// �����
        /// </summary>
        private decimal cost = 0M;

        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        private Base.OperEnvironment operInfo = new Neusoft.HISFC.Models.Base.OperEnvironment();


        private bool isValid = true;

        #endregion

        #region ����

        /// <summary>
        /// ���鿪ʼʱ��
        /// </summary>
        public DateTime Start
        {
            get
            {
                return this.start;
            }
            set
            {
                this.start = value;
            }
        }

        /// <summary>
        /// �������ʱ��
        /// </summary>
        public DateTime End
        {
            get
            {
                return this.end;
            }
            set
            {
                this.end = value;
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
        /// ����ص�
        /// </summary>
        public string Locate
        {
            get
            {
                return this.locate;
            }
            set
            {
                this.locate = value;
            }
        }

        /// <summary>
        /// ���鼶��
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject Level
        {
            get
            {
                return this.level;
            }
            set
            {
                this.level = value;
            }
        }

        /// <summary>
        /// ���Ľ�����ʽ
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject PaperCommunion
        {
            get
            {
                return this.paperCommunion;
            }
            set
            {
                this.paperCommunion = value;
            }
        }

        /// <summary>
        /// ����ƪ��
        /// </summary>
        public int PaperCopies
        {
            get
            {
                return this.paperCopies;
            }
            set
            {
                this.paperCopies = value;
            }
        }

        /// <summary>
        /// �������Դ
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject Source
        {
            get
            {
                return this.source;
            }
            set
            {
                this.source = value;
            }
        }

        /// <summary>
        /// �����
        /// </summary>
        public decimal Cost
        {
            get
            {
                return this.cost;
            }
            set
            {
                this.cost = value;
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

        #region IValid ��Ա

        /// <summary>
        /// ��Ч��
        /// </summary>
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

        #endregion

        #region ����

        public new ConferenceInfo Clone()
        {
            ConferenceInfo conferenceInfo = base.Clone() as ConferenceInfo;
            conferenceInfo.Dept = this.Dept.Clone();
            conferenceInfo.Level = this.Level.Clone();
            conferenceInfo.OperInfo = this.OperInfo.Clone();
            conferenceInfo.PaperCommunion = this.Clone();

            return conferenceInfo;
        }

        #endregion
    }
}
