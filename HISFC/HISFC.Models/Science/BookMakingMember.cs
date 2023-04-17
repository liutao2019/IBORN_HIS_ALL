using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.Science
{
    /// <summary>
    /// Neusoft.HISFC.Models.Science.BookMakingMember<br></br>
    /// [��������:������Ա��Ϣʵ�� ]<br></br>
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
    public class BookMakingMember : Neusoft.FrameWork.Models.NeuObject, Neusoft.HISFC.Models.Base.IValid
    {
        
        #region ����

        /// <summary>
        /// ְ��,��,����,������,��ί
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject responsibility = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��Ч��
        /// </summary>
        private bool isValid = true;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private HRM.HRMEmployee hRMEmployee = new Neusoft.HISFC.Models.HRM.HRMEmployee();

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        private Base.OperEnvironment operInfo = new Neusoft.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private BookMaking bookMaking = new BookMaking();

        #endregion 

        #region ����

        /// <summary>
        /// ְ��,��,����,������,��ί
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject Responsibility
        {
            get
            {
                return this.responsibility;
            }
            set
            {
                this.responsibility = value;
            }
        }

        /// <summary>
        /// ��Ч��
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
        /// ����������Ϣ
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
        /// ������Ϣ
        /// </summary>
        public BookMaking BookMaking
        {
            get
            {
                return this.bookMaking;
            }
            set
            {
                this.bookMaking = value;
            }
        }

        #endregion

        #region ����

        public new BookMakingMember Clone()
        {
            BookMakingMember member = base.Clone() as BookMakingMember;

            member.HRMEmployee = this.HRMEmployee.Clone();
            member.OperInfo = this.OperInfo.Clone();
            member.BookMaking = this.BookMaking.Clone();
            member.Responsibility = this.Responsibility.Clone();

            return member;
        }

        #endregion 
    }
}
