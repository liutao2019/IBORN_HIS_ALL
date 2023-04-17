using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.Science
{    
    /// <summary>
    /// Neusoft.HISFC.Models.Science.BookMaking<br></br>
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
    public class BookMaking : Neusoft.FrameWork.Models.NeuObject, Neusoft.HISFC.Models.Base.IValid
    {

        #region ���� 

        private bool isValid = true;

        /// <summary>
        /// ������
        /// </summary>
        private string publishingCompany = string.Empty;

        /// <summary>
        /// ��������
        /// </summary>
        private DateTime publishingDate;

        /// <summary>
        /// ����
        /// </summary>
        private int wordNumber = 0;

        /// <summary>
        /// ����
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject editerChief = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject dept = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        private Base.OperEnvironment operInfo = new Neusoft.HISFC.Models.Base.OperEnvironment();

        private string extFile00 = string.Empty;

        private string extFile01 = string.Empty;

        private string extFile02 = string.Empty;

        private string extFile03 = string.Empty;

        #endregion

        #region ����

        #region IValid ��Ա

        /// <summary>
        /// ��Ч�Ա��
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
        /// ������
        /// </summary>
        public string PublishingCompany
        {
            get
            {
                return this.publishingCompany;
            }
            set
            {
                this.publishingCompany = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public DateTime PublishingDate
        {
            get
            {
                return this.publishingDate;
            }
            set
            {
                this.publishingDate = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public int WordNumber
        {
            get
            {
                return this.wordNumber;
            }
            set
            {
                this.wordNumber = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject EditerChief
        {
            get
            {
                return this.editerChief;
            }
            set
            {
                this.editerChief = value;
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

        /// <summary>
        /// ��չ���
        /// </summary>
        public string ExtFile00
        {
            get
            {
                return this.extFile00;
            }
            set
            {
                this.extFile00 = value;
            }
        }

        /// <summary>
        /// ��չ���
        /// </summary>
        public string ExtFile01
        {
            get
            {
                return this.extFile01;
            }
            set
            {
                this.extFile01 = value;
            }
        }

        /// <summary>
        /// ��չ���
        /// </summary>
        public string ExtFile02
        {
            get
            {
                return this.extFile02;
            }
            set
            {
                this.extFile02 = value;
            }
        }

        /// <summary>
        /// ��չ���
        /// </summary>
        public string ExtFile03
        {
            get
            {
                return this.extFile03;
            }
            set
            {
                this.extFile03 = value;
            }
        }

        #endregion

        #region ����

        public new BookMaking Clone()
        {
            BookMaking bookmaking = base.Clone() as BookMaking;

            bookmaking.EditerChief = this.EditerChief.Clone();
            bookmaking.Dept = this.Dept.Clone();
            bookmaking.OperInfo = this.OperInfo.Clone();
            
            return bookmaking;

        }

        #endregion
    }
}
