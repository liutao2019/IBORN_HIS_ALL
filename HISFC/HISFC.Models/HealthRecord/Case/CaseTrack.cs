using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.HealthRecord.Case
{
    /// <summary>
    /// [��������: ��������]<br></br>
    /// [�� �� ��: ��ΰ��]<br></br>
    /// [����ʱ��: 2007/09/13]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// ID�����ټ�¼ID,  User01������ʹ�����ͱ���
    /// </summary>
    [Serializable]
    public class CaseTrack : FS.FrameWork.Models.NeuObject
    {
        public CaseTrack()
        {
        }

        /// <summary>
        /// ���߲���
        /// </summary>
        private CaseInfo patientCase = new CaseInfo();

        /// <summary>
        /// ����ʹ��ʱ��, ����ʹ���˹���, ����ʹ�ÿ��ұ���, ����ʹ�����ͱ��룬��Ӧ������CASE05
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment useCaseEnv = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ���߲�����Ϣ
        /// </summary>
        public CaseInfo PatientCase
        {
            get
            {
                return this.patientCase;
            }
            set
            {
                this.patientCase = value;
            }
        }

        /// <summary>
        /// ����ʹ����Ϣ��¼
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment UseCaseEnv
        {
            get
            {
                return this.useCaseEnv;
            }
            set
            {
                this.useCaseEnv = value;
            }
        }

        /// <summary>
        /// �����¶�ʵ��
        /// </summary>
        /// <returns>nullʧ��</returns>
        public new CaseTrack Clone()
        {
            CaseTrack ct = base.Clone() as CaseTrack;

            if (ct == null)
            {
                return null;
            }

            ct.useCaseEnv = this.useCaseEnv.Clone();
            ct.patientCase = this.patientCase.Clone();

            return ct;
        }

    }
}
