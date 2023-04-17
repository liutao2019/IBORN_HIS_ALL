using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.Science
{
    /// <summary>
    /// Neusoft.HISFC.Models.Science.SubjectInfo<br></br>
    /// [��������:�������߳�Ա ]<br></br>
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
    public class PaperMember : Neusoft.FrameWork.Models.NeuObject, Neusoft.HISFC.Models.Base.IValid
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
        /// ������Ϣ
        /// </summary>
        private PaperInfo paperInfo = new PaperInfo();

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
        /// ������Ϣ
        /// </summary>
        public PaperInfo PaperInfo
        {
            get
            {
                return this.paperInfo;
            }
            set
            {
                this.paperInfo = value;
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

        public new PaperMember Clone()
        {
            PaperMember paperMember = base.Clone() as PaperMember;

            paperMember.HRMEmployee = this.HRMEmployee.Clone();
            paperMember.OperInfo = this.OperInfo.Clone();

            return paperMember;
        }

        #endregion
    }
}
