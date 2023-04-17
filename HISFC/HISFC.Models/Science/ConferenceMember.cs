using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.Science
{
    /// <summary>
    /// Neusoft.HISFC.Models.Science.ConferenceMember<br></br>
    /// [��������: ѧ�������Աʵ�� ]<br></br>
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
    public class ConferenceMember : Neusoft.FrameWork.Models.NeuObject, Neusoft.HISFC.Models.Base.IValid
    {
        #region ����

        /// <summary>
        /// ��Ч�Ա��
        /// </summary>
        private bool isValid = true;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private HRM.HRMEmployee hRMEmployee = new Neusoft.HISFC.Models.HRM.HRMEmployee();

        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        private Base.OperEnvironment operInfo = new Neusoft.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ѧ��������Ϣ
        /// </summary>
        private ConferenceInfo conferenceInfo = new ConferenceInfo();

        #endregion

        #region ����

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public HRM.HRMEmployee HRMEmployee
        {
            get
            {
                return this.hRMEmployee;
            }
            set
            {
                this.hRMEmployee = value;
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
        /// ѧ��������Ϣ
        /// </summary>
        public ConferenceInfo ConferenceInfo
        {
            get
            {
                return this.conferenceInfo;
            }
            set
            {
                this.conferenceInfo = value;
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

        public new ConferenceMember Clone()
        {
            ConferenceMember conferenceMember = base.Clone() as ConferenceMember;
            conferenceMember.ConferenceInfo = this.ConferenceInfo.Clone();
            conferenceMember.HRMEmployee = this.HRMEmployee.Clone();
            conferenceMember.OperInfo = this.OperInfo.Clone();

            return conferenceMember;
        }

        #endregion
    }
}
