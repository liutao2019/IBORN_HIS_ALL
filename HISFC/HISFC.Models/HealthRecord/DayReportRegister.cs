using System;

namespace FS.HISFC.Models.HealthRecord
{
    /// <summary>
    /// [��������: �����ձ�ʵ��]<br></br>
    /// [�� �� ��: ��ȫ]<br></br>
    /// [����ʱ��: 2007-09-17]<br></br>
    /// 
    /// <�޸ļ�¼
    ///		�޸��� =
    ///		�޸�ʱ�� =
    ///		�޸�Ŀ�� =
    ///		�޸����� =
    ///  />
    /// </summary>
    [Serializable]
    public class DayReportRegister : FS.FrameWork.Models.NeuObject
    {
        public DayReportRegister()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����

        //�ձ�����
        private DateTime dateStat;

        //��������
        private FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();

        //�����˴���
        private int clinicNum;

        //�����˴���
        private int emcNum;

        //������������
        private int emcDeadNum;

        //�۲�����
        private int observeNum;
 
        //�۲�����
        private int observeDeadNum;

        //����
        private int reDiagnoseNum;

        //����
        private int clcDiagnoseNum;

        //ר��
        private int specialNum;

        //ҽ��
        private int hosInsuranceNum;

        //���/�������
        private int bdCheckNum;
        
        //����Ա����
        private FS.FrameWork.Models.NeuObject oper = new FS.FrameWork.Models.NeuObject();
        
        //����ʱ��
        private DateTime operDate;

        #endregion

        #region ����

        /// <summary>
        /// �ձ�����
        /// </summary>
        public DateTime DateStat
        {
            get
            {
                return dateStat;
            }
            set
            {
                dateStat = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public FS.FrameWork.Models.NeuObject Dept
        {
            get
            {
                return dept;
            }
            set
            {
                dept = value;
            }
        }

        /// <summary>
        /// �����˴���
        /// </summary>
        public int ClinicNum
        {
            get
            {
                return clinicNum;
            }
            set
            {
                clinicNum = value;
            }
        }

        /// <summary>
        /// �����˴���
        /// </summary>
        public int EmcNum
        {
            get
            {
                return emcNum;
            }
            set
            {
                emcNum = value;
            }
        }

        /// <summary>
        /// ���������˴���
        /// </summary>
        public int EmcDeadNum
        {
            get
            {
                return emcDeadNum;
            }
            set
            {
                emcDeadNum = value;
            }
        }

        /// <summary>
        /// �۲�����
        /// </summary>
        public int ObserveNum
        {
            get
            {
                return observeNum;
            }
            set
            {
                observeNum = value;
            }
        }

        /// <summary>
        /// �۲�����
        /// </summary>
        public int ObserveDeadNum
        {
            get
            {
                return observeDeadNum;
            }
            set
            {
                observeDeadNum = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public int ReDiagnoseNum
        {
            get
            {
                return reDiagnoseNum;
            }
            set
            {
                reDiagnoseNum = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public int ClcDiagnoseNum
        {
            get
            {
                return clcDiagnoseNum;
            }
            set
            {
                clcDiagnoseNum = value;
            }
        }

        /// <summary>
        /// ר��
        /// </summary>
        public int SpecialNum
        {
            get
            {
                return specialNum;
            }
            set
            {
                specialNum = value;
            }
        }

        /// <summary>
        /// ҽ��
        /// </summary>
        public int HosInsuranceNum
        {
            get
            {
                return hosInsuranceNum;
            }
            set
            {
                hosInsuranceNum = value;
            }
        }

        /// <summary>
        /// ���/�������
        /// </summary>
        public int BdCheckNum
        {
            get
            {
                return bdCheckNum;
            }
            set
            {
                bdCheckNum = value;
            }
        }

        /// <summary>
        /// ����Ա����
        /// </summary>
        public FS.FrameWork.Models.NeuObject Oper
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

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime OperDate
        {
            get
            {
                return operDate;
            }
            set
            {
                operDate = value;
            }
        }

        #endregion

        #region ����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new DayReportRegister Clone()
        {
            DayReportRegister myReport = base.Clone() as DayReportRegister;
            myReport.Dept = this.Dept.Clone();
            myReport.Oper = this.Dept.Clone();

            return myReport;
        }

        #endregion
    }
}
