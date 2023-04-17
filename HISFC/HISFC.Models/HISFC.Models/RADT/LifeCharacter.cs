using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.RADT
{
    /// <summary>
    /// [��������: ������������ʵ��]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-05-02]<br></br>
    /// <�޸ļ�¼/>
    /// </summary> 
    [Serializable]
    public class LifeCharacter : FS.FrameWork.Models.NeuObject
    {
        #region ����
        /// <summary>
        /// ��Ժʱ��
        /// </summary>
        private DateTime inDate;
        /// <summary>
        /// ����
        /// </summary>
        private string bedNO;
        /// <summary>
        /// ��������
        /// </summary>
        private DateTime measureDate;
        /// <summary>
        /// ����ʱ���
        /// </summary>
        private int time;
        /// <summary>
        /// ǿ������
        /// </summary>
        private int forceHypothermia;
        /// <summary>
        /// Ŀ������
        /// </summary>
        private decimal targetTemperature;
        /// <summary>
        /// ��������
        /// </summary>
        private string temperatureType;
        /// <summary>
        /// ����
        /// </summary>
        private int breath;
        /// <summary>
        /// ����
        /// </summary>
        private int pulse;
        /// <summary>
        /// ����
        /// </summary>
        private decimal temperature;
        /// <summary>
        /// Ѫѹ���ߣ�
        /// </summary>
        private int highBloodPressure;
        /// <summary>
        /// Ѫѹ���ͣ�
        /// </summary>
        private int lowBloodPressure;
        /// <summary>
        /// ��������Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();
        /// <summary>
        /// ����
        /// </summary>
        private FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// ����վ
        /// </summary>
        private FS.FrameWork.Models.NeuObject nurseStation = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ���߱��
        /// </summary>
        private PID pID = new PID();

        #endregion

        #region ����
        /// <summary>
        /// ��Ժ����
        /// </summary>
        public DateTime InDate
        {
            get 
            {
                return this.inDate;
            }
            set 
            {
                this.inDate = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string BedNO
        {
            get
            {
                return this.bedNO;
            }
            set
            {
                this.bedNO = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public DateTime MeasureDate
        {
            get { return this.measureDate; }
            set { this.measureDate = value; }
        }

        /// <summary>
        /// ����ʱ���
        /// </summary>
        public int Time
        {
            get { return this.time; }
            set { this.time = value; }
        }

        /// <summary>
        /// ǿ�н���
        /// </summary>
        public bool IsForceHypothermia
        {
            get { return this.forceHypothermia == 1 ? true : false; }
            set
            {
                if (value)
                    this.forceHypothermia = 1;
                else
                    this.forceHypothermia = 0;
            }
        }
        /// <summary>
        /// ǿ�н���Int
        /// </summary>
        public int ForceHypothermiaInt
        {
            get { return this.forceHypothermia; }
            set
            {
                this.forceHypothermia = value;
            }
        }
        /// <summary>
        /// Ŀ������
        /// </summary>
        public decimal TargetTemperature
        {
            get { return this.targetTemperature; }
            set { this.targetTemperature = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string TemperatureType
        {
            get
            {
                if (this.temperatureType == null)
                    return string.Empty;
                else
                    return this.temperatureType;
            }
            set { this.temperatureType = value; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public int Breath
        {
            get
            {
                return this.breath;
            }
            set
            {
                this.breath = value;
            }
        }


        /// <summary>
        /// ����
        /// </summary>
        public int Pulse
        {
            get
            {
                return this.pulse;
            }
            set
            {
                this.pulse = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public decimal Temperature
        {
            get
            {
                return this.temperature;
            }
            set
            {
                this.temperature = value;
            }
        }

        /// <summary>
        /// ��ѹ
        /// </summary>
        public int HighBloodPressure
        {
            get
            {
                return this.highBloodPressure;
            }
            set
            {
                this.highBloodPressure = value;
            }
        }

        /// <summary>
        /// ��ѹ
        /// </summary>
        public int LowBloodPressure
        {
            get
            {
                return this.lowBloodPressure;
            }
            set
            {
                this.lowBloodPressure = value;
            }
        }

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get 
            {
                return this.oper;
            }
            set 
            {
                this.oper = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public FS.FrameWork.Models.NeuObject Dept
        {
            get 
            {
                return this.dept;
            }
            set 
            {
                this.dept = value;
            }
        }

        /// <summary>
        /// ����վ
        /// </summary>
        public FS.FrameWork.Models.NeuObject NurseStation
        {
            get 
            {
                return this.nurseStation;
            }
            set
            {
                this.nurseStation = value;
            }
        }

        /// <summary>
        /// ���߱��
        /// </summary>
        public PID PID
        {
            get
            {
                return this.pID;
            }
            set
            {
                this.pID = value;
            }
        }

        #endregion

        #region ����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new LifeCharacter clone()
        {
            LifeCharacter obj = base.Clone() as LifeCharacter;
            obj.oper = this.oper.Clone();
            obj.dept = this.dept.Clone();
            obj.nurseStation = this.nurseStation.Clone();
            return obj;
        }

        #endregion
    }
}
