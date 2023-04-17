using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Order
{
    /// <summary>
    /// FS.HISFC.Models.Order.CurePhase<br></br>
    /// [��������: ���ƽ׶���Ϣʵ��]<br></br>
    /// [�� �� ��: Sunm]<br></br>
    /// [����ʱ��: 2007-08-23]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class CurePhase : FS.FrameWork.Models.NeuObject
    {
        public CurePhase()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����

        /// <summary>
        /// סԺ��ˮ��
        /// </summary>
        private string patientID;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ���ƽ׶���Ϣ
        /// </summary>
        private FS.FrameWork.Models.NeuObject curePhaseInfo = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime startTime;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime endTime;

        /// <summary>
        /// ����ҽ��
        /// </summary>
        private FS.FrameWork.Models.NeuObject doctor = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��ע
        /// </summary>
        private string remark;

        /// <summary>
        /// ��Ч��
        /// </summary>
        private bool isVaild;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// סԺ��ˮ��
        /// </summary>
        public string PatientID
        {
            get { return this.patientID; }
            set { this.patientID = value; }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public FS.FrameWork.Models.NeuObject Dept
        {
            get { return this.dept; }
            set { this.dept = value; }
        }

        /// <summary>
        /// ���ƽ׶���Ϣ
        /// </summary>
        public FS.FrameWork.Models.NeuObject CurePhaseInfo
        {
            get { return this.curePhaseInfo; }
            set { this.curePhaseInfo = value; }
        }

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        public DateTime StartTime
        {
            get { return this.startTime; }
            set { this.startTime = value; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime EndTime
        {
            get { return this.endTime; }
            set { this.endTime = value; }
        }

        /// <summary>
        /// ����ҽ��
        /// </summary>
        public FS.FrameWork.Models.NeuObject Doctor
        {
            get { return this.doctor; }
            set { this.doctor = value; }
        }

        /// <summary>
        /// ��Ч��
        /// </summary>
        public bool IsVaild
        {
            get { return this.isVaild; }
            set { this.isVaild = value; }
        }

        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get { return this.oper; }
            set { this.oper = value; }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new CurePhase Clone()
        {
            // TODO:  ��� CurePhase.Clone ʵ��
            CurePhase obj = base.Clone() as CurePhase;
            
            obj.oper = this.oper.Clone();

            return obj;
        }

        #endregion

        
    }
}
