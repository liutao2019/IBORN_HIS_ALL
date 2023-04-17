using System;


namespace FS.HISFC.Models.HealthRecord
{


    /// <summary>
    /// Fee ��ժҪ˵����ID ����Ա���� Name ����Ա����
    /// </summary>
    [Serializable]
    public class Fee : FS.FrameWork.Models.NeuObject
    {
        public Fee()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ˽�б���

        private string inpatientNO;
        private FS.FrameWork.Models.NeuObject myDeptInfo = new FS.FrameWork.Models.NeuObject();
        private FS.FrameWork.Models.NeuObject myMainOutICD = new FS.FrameWork.Models.NeuObject();
        private decimal totCost;
        private DateTime outDate;
        private DateTime operDate;
        private FS.FrameWork.Models.NeuObject myFeeInfo = new FS.FrameWork.Models.NeuObject();
        private FS.HISFC.Models.Base.OperEnvironment operInfo = new FS.HISFC.Models.Base.OperEnvironment();
        #endregion

        #region ����

        /// <summary>
        /// סԺ��ˮ��
        /// </summary>
        public string InpatientNO
        {
            get
            {
                return inpatientNO;
            }
            set
            {

                inpatientNO = value;
            }
        }

        /// <summary>
        /// ������Ϣ(�Ƿ�Ҫ���ǻ���ת�����) ID ���ұ��� Name ��������
        /// </summary>
        public FS.FrameWork.Models.NeuObject DeptInfo
        {
            get
            {
                return myDeptInfo;
            }
            set
            {
                myDeptInfo = value;
            }
        }
        /// <summary>
        /// ��Ժ�������Ϣ ID �������Ϣ Name �������
        /// </summary>
        public FS.FrameWork.Models.NeuObject MainOutICD
        {
            get
            {
                return myMainOutICD;
            }
            set
            {
                myMainOutICD = value;
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public decimal TotCost
        {
            get
            {
                return totCost;
            }
            set
            {
                totCost = value;
            }
        }
        /// <summary>
        /// ��Ժ����
        /// </summary>
        public DateTime OutDate
        {
            get
            {
                return outDate;
            }
            set
            {
                outDate = value;
            }
        }
        /// <summary>
        /// ����Ա��
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment OperInfo
        {
            get
            {
                return operInfo;
            }
            set
            {
                operInfo = value;
            }
        }
        /// <summary>
        /// ������Ϣ ID ���ô������ Name ���ô�������
        /// </summary>
        public FS.FrameWork.Models.NeuObject FeeInfo
        {
            get
            {
                return myFeeInfo;
            }
            set
            {
                myFeeInfo = value;
            }
        }

        #endregion

        #region ���ú���


        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns>Case.Fee</returns>
        public new Fee Clone()
        {
            Fee FeeClone = base.MemberwiseClone() as Fee;

            FeeClone.FeeInfo = this.FeeInfo.Clone();
            FeeClone.DeptInfo = this.DeptInfo.Clone();
            FeeClone.MainOutICD = this.MainOutICD.Clone();
            FeeClone.operInfo = operInfo.Clone();

            return FeeClone;
        }

        #endregion

        #region ����
        /// <summary>
        /// ��������
        /// </summary>
        [Obsolete("���� �� OperInfo.OperTime ����", true)]
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
    }
}
