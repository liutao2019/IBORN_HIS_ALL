using System;
using System.Collections.Generic;
using System.Text;


namespace FS.HISFC.Models.HealthRecord.Case
{
    /// <summary>
    /// [��������: �������Ĳ�ѯ�Ǽ�]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007/08/24]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class CaseLend : CaseInfo
    {
        public CaseLend()
        {
        }

        #region ����

        /// <summary>
        /// ҵ������
        /// </summary>
        private EnumLendType lendType;

        /// <summary>
        /// ���Ĳ�����
        /// </summary>
        private FS.HISFC.Models.Base.Employee lendEmpl = new FS.HISFC.Models.Base.Employee();

        /// <summary>
        /// ������ʼʱ��  
        /// </summary>
        private DateTime startingTime;

        /// <summary>
        /// ���Ľ�ֹʱ��  
        /// </summary>
        private DateTime endTime;

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        private bool isAuditing;

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment auditingOper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// �Ƿ��Ѿ��黹
        /// </summary>
        private bool isReturn;

        /// <summary>
        /// ���Ĳ����˹黹��������
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment returnOper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ���Ĺ黹ȷ��
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment returnConfirmOper = new FS.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// ҵ������
        /// </summary>   
        public EnumLendType LendType
        {
            get
            {
                return lendType;
            }
            set
            {
                lendType = value;
            }
        }

        /// <summary>
        /// ���Ĳ�����
        /// </summary>
        public FS.HISFC.Models.Base.Employee LendEmpl
        {
            get
            {
                return lendEmpl;
            }
            set
            {
                lendEmpl = value;
            }
        }

        /// <summary>
        /// ������ʼʱ��  
        /// </summary>     
        public DateTime StartingTime
        {
            get
            {
                return startingTime;
            }
            set
            {
                startingTime = value;
            }
        }

        /// <summary>
        /// ���Ľ�ֹʱ��  
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
            }
        }

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        public bool IsAuditing
        {
            get
            {
                return isAuditing;
            }
            set
            {
                isAuditing = value;
            }
        }

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment AuditingOper
        {
            get
            {
                return auditingOper;
            }
            set
            {
                auditingOper = value;
            }
        }
             
        /// <summary>
        /// �Ƿ��Ѿ��黹
        /// </summary>
        public bool IsReturn
        {
            get
            {
                return isReturn;
            }
            set
            {
                isReturn = value;
            }
        }

        /// <summary>
        /// ���Ĳ����˹黹��������
        /// </summary>   
        public FS.HISFC.Models.Base.OperEnvironment ReturnOper
        {
            get
            {
                return returnOper;
            }
            set
            {
                returnOper = value;
            }
        }

        /// <summary>
        /// ���Ĺ黹ȷ�ϲ�������
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment ReturnConfirmOper
        {
            get
            {
                return returnConfirmOper;
            }
            set
            {
                returnConfirmOper = value;
            }
        }

        #endregion

        #region ����

        #region ��¡

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns>������ѯ��������¼</returns>
        public new CaseLend Clone()
        {
            CaseLend caseLend = base.Clone() as CaseLend;

            caseLend.AuditingOper = this.AuditingOper.Clone();
            caseLend.LendEmpl = this.LendEmpl.Clone();
            caseLend.ReturnConfirmOper = this.ReturnConfirmOper.Clone();
            caseLend.ReturnOper = this.ReturnOper.Clone();

            return caseLend;
        }

        #endregion

        #endregion
    }



}
