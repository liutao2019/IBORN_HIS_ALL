using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// <br>ȫ����</br>
    /// <br>[��������: ����ģ��ʵ��]</br>
    /// <br>[�� �� ��: ����]</br>
    /// <br>[����ʱ��: 2008-09-22]</br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class RFSample : Neusoft.FrameWork.Models.NeuObject
    {
        #region �ֶ�

        /// <summary>
        /// �����Ա
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject person = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ���ͷ���
        /// </summary>
        private string rfType = string.Empty;

        /// <summary>
        /// ģ������
        /// </summary>
        private string sampleContent = string.Empty;

        /// <summary>
        /// ��������
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment oper = new Neusoft.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// �����Ա
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject Person
        {
            get
            {
                return person;
            }
            set
            {
                person = value;
            }
        }

        /// <summary>
        /// ���ͷ���
        /// </summary>
        public string RfType
        {
            get
            {
                return rfType;
            }
            set
            {
                rfType = value;
            }
        }

        /// <summary>
        /// ģ������
        /// </summary>
        public string SampleContent
        {
            get
            {
                return sampleContent;
            }
            set
            {
                sampleContent = value;
            }
        }

        /// <summary>
        /// ��������
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

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new RFSample Clone()
        {
            RFSample rfSample = base.Clone() as Neusoft.HISFC.Models.HR.RFSample;

            rfSample.Person = this.Person.Clone();
            rfSample.Oper = this.Oper.Clone();

            return rfSample;
        }

        #endregion
    }
}
