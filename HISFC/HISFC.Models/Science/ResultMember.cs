using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.Science
{
    /// <summary>
    /// Neusoft.HISFC.Models.Science.SubjectInfo<br></br>
    /// [��������:���гɹ����Ա ]<br></br>
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
    public class ResultMember : Neusoft.FrameWork.Models.NeuObject, Neusoft.HISFC.Models.Base.IValid
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
        /// �о��ɹ���Ϣ
        /// </summary>
        private ResultInfo resultInfo = new ResultInfo();

        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        private Base.OperEnvironment operInfo = new Neusoft.HISFC.Models.Base.OperEnvironment();

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
        /// �о��ɹ���Ϣ
        /// </summary>
        public ResultInfo ResultInfo
        {
            get
            {
                return this.resultInfo;
            }
            set
            {
                this.resultInfo = value;
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

        public new ResultMember Clone()
        {
            ResultMember resultmember = base.Clone() as ResultMember;

            //������Ϣ
            resultmember.HRMEmployee = this.HRMEmployee.Clone();

            //�о��ɹ���Ϣ
            resultmember.ResultInfo = this.ResultInfo.Clone();

            return resultmember;
        }

        #endregion
    }
}
