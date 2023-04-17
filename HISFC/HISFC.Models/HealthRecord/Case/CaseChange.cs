using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.HealthRecord.Case
{
    /// <summary>
    /// [��������: ��������ʵ��]<br></br>
    /// [�� �� ��: ��ΰ��]<br></br>
    /// [����ʱ��: 2007/09/14]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// ID:����������ʶ, MEMO:��������˵�� 
    /// </summary>
    [Serializable]
    public class CaseChange :FS.FrameWork.Models.NeuObject , FS.HISFC.Models.Base.IValid
    {
        private string oldCardNO;
        private string newCardNO;

        private FS.HISFC.Models.Base.OperEnvironment doctorEnv = new FS.HISFC.Models.Base.OperEnvironment();
        private FS.HISFC.Models.Base.OperEnvironment operEnv = new FS.HISFC.Models.Base.OperEnvironment();

        private bool isValid;
        private decimal chargeCost;

        /// <summary>
        /// ���ѽ��
        /// </summary>
        public decimal ChargeCost
        {
            get
            {
                return this.chargeCost;
            }
            set
            {
                this.chargeCost = value;
            }
        }

        /// <summary>
        /// �ɲ�����
        /// </summary>
        public string OldCardNO
        {
            get
            {
                return this.oldCardNO;
            }
            set
            {
                this.oldCardNO = value;
            }
        }

        /// <summary>
        /// �²�����
        /// </summary>
        public string NewCardNO
        {
            get
            {
                return this.newCardNO;
            }
            set
            {
                this.newCardNO = value;
            }
        }

        /// <summary>
        /// ������������ҽ����Ϣ
        /// ID:ҽ�����, OperTime:ʱ��
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment DoctorEnv
        {
            get
            {
                return this.doctorEnv;
            }
            set
            {
                this.doctorEnv = value;
            }
        }

        /// <summary>
        /// ������������Ա
        /// ID:����Ա���, OperTime:ʱ��
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment OperEnv
        {
            get
            {
                return this.operEnv;
            }
            set
            {
                this.operEnv = value;
            }
        }

        #region IValid ��Ա

        /// <summary>
        /// �Ƿ��շ�
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


        public new CaseChange Clone()
        {
            CaseChange cc = base.Clone() as CaseChange;

            cc.doctorEnv = this.doctorEnv.Clone();
            cc.operEnv = this.operEnv.Clone();

            return cc;
        }
    }
}