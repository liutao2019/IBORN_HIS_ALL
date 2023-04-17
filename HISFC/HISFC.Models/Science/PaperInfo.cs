using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.Science
{
    /// <summary>
    /// Neusoft.HISFC.Models.Science.PaperInfo<br></br>
    /// [��������:������Ϣʵ�� ]<br></br>
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
    public class PaperInfo : Neusoft.FrameWork.Models.NeuObject, Neusoft.HISFC.Models.Base.IValid
    {

        #region ����

        /// <summary>
        /// ��Ч�Ա��
        /// </summary>
        private bool isValid = false;

        /// <summary>
        /// ������
        /// </summary>
        private string publiction = string.Empty;

        /// <summary>
        /// ��
        /// </summary>
        private string volume = string.Empty;

        /// <summary>
        /// ��
        /// </summary>
        private string period = string.Empty;

        /// <summary>
        /// ҳ
        /// </summary>
        private string page = string.Empty;

        /// <summary>
        /// ��������
        /// </summary>
        private DateTime publicDate;

        /// <summary>
        /// ����
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject author = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject dept = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ר��
        /// </summary>
        private string specialDept = string.Empty;

        /// <summary>
        /// ���ļ���
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject level = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// �������
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject kind = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ������Դ
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject source = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        private Base.OperEnvironment operInfo = new Neusoft.HISFC.Models.Base.OperEnvironment();

        


        #endregion

        #region ����

        /// <summary>
        /// ������
        /// </summary>
        public string Publiction
        {
            get
            {
                return this.publiction;
            }
            set
            {
                this.publiction = value;
            }
        }

        /// <summary>
        /// ��
        /// </summary>
        public string Volume
        {
            get
            {
                return this.volume;
            }
            set
            {
                this.volume = value;
            }
        }

        /// <summary>
        /// ��
        /// </summary>
        public string Period
        {
            get
            {
                return this.period;
            }
            set
            {
                this.period = value;
            }
        }

        /// <summary>
        /// ҳ
        /// </summary>
        public string Page
        {
            get
            {
                return this.page;
            }
            set
            {
                this.page = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public DateTime PublicDate
        {
            get
            {
                return this.publicDate;
            }
            set
            {
                this.publicDate = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject Author
        {
            get
            {
                return this.author;
            }
            set
            {
                this.author = value;
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
        /// ���ļ���
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
        /// �������
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject Kind
        {
            get
            {
                return this.kind;
            }
            set
            {
                this.kind = value;
            }
        }

        /// <summary>
        /// ������Դ
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

        #endregion

        #region ����

        public new PaperInfo Clone()
        {
            PaperInfo paperInfo = base.Clone() as PaperInfo;

            paperInfo.Author = this.Author.Clone();
            paperInfo.Dept = this.Dept.Clone();
            paperInfo.Kind = this.Kind.Clone();
            paperInfo.Level = this.Level.Clone();
            paperInfo.OperInfo = this.OperInfo.Clone();
            paperInfo.Source = this.Source.Clone();

            return paperInfo;
        }

        #endregion

    }
}
