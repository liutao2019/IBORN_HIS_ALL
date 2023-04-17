using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.Science
{
    /// <summary>
    /// Neusoft.HISFC.Models.Science.SubjectMembers<br></br>
    /// [��������:�������Ա��Ϣ ]<br></br>
    /// [�� �� ��: ţ��Ԫ]<br></br>
    /// [����ʱ��: 2008-05-07]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class SubjectMembers:Neusoft.FrameWork.Models.NeuObject,Neusoft.HISFC.Models.Base.IValid
    {
        #region ����
        /// <summary>
        /// ��Ч��ʶ 1:��Ч 0 ��Ч
        /// </summary>
        private bool isValid = true;

        /// <summary>
        /// ���»�����Ϣ
        /// </summary>
        private HRM.HRMEmployee hRMEmployee = new Neusoft.HISFC.Models.HRM.HRMEmployee();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private SubjectInfo subjectInfo = new SubjectInfo();

        #endregion
        #region ����
        #region IValid ��Ա
        /// <summary>
        /// ��Ч��ʶ
        /// </summary>
        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
            set
            {
                this.isValid  = value;
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public HRM.HRMEmployee HRMEmployee
        {
            set
            {
                this.hRMEmployee = value;
            }
            get
            {
                return this.hRMEmployee;
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public SubjectInfo SubjectInfo
        {
            set
            {
                this.subjectInfo = value;
            }
            get
            {
                return this.subjectInfo;
            }
        }

        #endregion
        #endregion

        #region ����
        public new SubjectMembers Clone()
        {
            SubjectMembers subjectMembers = base.Clone() as SubjectMembers;

            //������Ϣ
            subjectMembers.HRMEmployee = this.HRMEmployee.Clone();

            //������Ϣ
            subjectMembers.SubjectInfo = this.SubjectInfo.Clone();

            return subjectMembers;
        }
        #endregion
    }
}
