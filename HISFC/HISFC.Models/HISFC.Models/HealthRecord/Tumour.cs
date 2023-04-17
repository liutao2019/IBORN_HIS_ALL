using System;


namespace FS.HISFC.Models.HealthRecord
{


    /// <summary>
    /// CTumour ��ժҪ˵����
    /// </summary>
    [Serializable]
    public class Tumour : FS.FrameWork.Models.NeuObject
    {
        public Tumour()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ˽�б���
        /// <summary>
        /// סԺ��ˮ��
        /// </summary>
        private string inpatientNo = "";//   --
        /// <summary>
        /// ���Ʒ�ʽ
        /// </summary>
        private string rmodeid = ""; //  --
        /// <summary>
        /// ���Ƴ�ʽ
        /// </summary>
        private string rprocessid = "";//   --
        /// <summary>
        /// ����װ��
        /// </summary>
        private string rdeviceid = "";//   --
        /// <summary>
        /// ���Ʒ�ʽ
        /// </summary>
        private string cmodeid = "";//   --
        /// <summary>
        /// ���Ʒ���
        /// </summary>
        private string cmethod = "";//   --
        /// <summary>
        /// ԭ����gy����
        /// </summary>
        private decimal gy1;//   --
        /// <summary>
        /// --ԭ�������
        /// </summary>
        private decimal time1;   //
        /// <summary>
        /// ԭ��������
        /// </summary>
        private decimal day1;//   --
        /// <summary>
        /// ԭ���ʼʱ��
        /// </summary>
        private System.DateTime begin_date1;//   --
        /// <summary>
        /// ԭ�������ʱ��
        /// </summary>
        private System.DateTime end_date1;//   --
        /// <summary>
        /// �����ܰͽ�gy����
        /// </summary>
        private decimal gy2;//   --
        /// <summary>
        /// �����ܰͽ����
        /// </summary>
        private decimal time2; //  --
        /// <summary>
        /// �����ܰͽ�����
        /// </summary>
        private decimal day2; //  --
        /// <summary>
        /// -�����ܰͽῪʼʱ��
        /// </summary>
        private System.DateTime begin_date2;//   -
        /// <summary>
        /// �����ܰͽ����ʱ��
        /// </summary>
        private System.DateTime end_date2;//   --
        /// <summary>
        /// ת����gy����
        /// </summary>
        private decimal gy3;//   --
        /// <summary>
        /// ת�������
        /// </summary>
        private decimal time3;//   --
        /// <summary>
        /// ת��������
        /// </summary>
        private decimal day3; //  --
        /// <summary>
        /// ת���ʼʱ��
        /// </summary>
        private System.DateTime begin_date3;//   --
        /// <summary>
        /// ת�������ʱ��
        /// </summary>
        private System.DateTime end_date3;//   --
        //--����Ա����
        private FS.HISFC.Models.Base.OperEnvironment operInfo = new FS.HISFC.Models.Base.OperEnvironment();
        #endregion

        #region ����
        public string Cmethod
        {
            get
            {
                return cmethod;
            }
            set
            {
                cmethod = value;
            }
        }
        /// <summary>
        /// ����Ա����
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
        /// ת�������ʱ��
        /// </summary>
        public System.DateTime EndDate3
        {
            get
            {
                return end_date3;
            }
            set
            {
                end_date3 = value;
            }
        }
        /// <summary>
        /// ת���ʼʱ��
        /// </summary>
        public System.DateTime BeginDate3
        {
            get
            {
                return begin_date3;
            }
            set
            {
                begin_date3 = value;
            }
        }
        /// <summary>
        /// ת�������
        /// </summary>
        public decimal Time3
        {
            get
            {
                return time3;
            }
            set
            {
                time3 = value;
            }
        }
        /// <summary>
        /// ת����gy����
        /// </summary>
        public decimal Gy3
        {
            get
            {
                return gy3;
            }
            set
            {
                gy3 = value;
            }
        }
        /// <summary>
        /// ת��������
        /// </summary>
        public decimal Day3
        {
            get
            {
                return day3;
            }
            set
            {
                day3 = value;
            }
        }
        /// <summary>
        /// �����ܰͽ����ʱ��
        /// </summary>
        public System.DateTime EndDate2
        {
            get
            {
                return end_date2;
            }
            set
            {
                end_date2 = value;
            }
        }
        /// <summary>
        /// �����ܰͽῪʼʱ��
        /// </summary>
        public System.DateTime BeginDate2
        {
            get
            {
                return begin_date2;
            }
            set
            {
                begin_date2 = value;
            }
        }
        /// <summary>
        /// �����ܰͽ�����
        /// </summary>
        public decimal Day2
        {
            get
            {
                return day2;
            }
            set
            {
                day2 = value;
            }
        }
        /// <summary>
        /// �����ܰͽ����
        /// </summary>
        public decimal Time2
        {
            get
            {
                return time2;
            }
            set
            {
                time2 = value;
            }
        }
        /// <summary>
        /// �����ܰͽ�gy����
        /// </summary>
        public decimal Gy2
        {
            get
            {
                return gy2;
            }
            set
            {
                gy2 = value;
            }
        }
        /// <summary>
        /// ԭ�������ʱ��
        /// </summary>
        public System.DateTime EndDate1
        {
            get
            {
                return end_date1;
            }
            set
            {
                end_date1 = value;
            }
        }
        /// <summary>
        /// ԭ���ʼʱ��
        /// </summary>
        public System.DateTime BeginDate1
        {
            get
            {
                return begin_date1;
            }
            set
            {
                begin_date1 = value;
            }
        }
        /// <summary>
        /// ԭ��������
        /// </summary>
        public decimal Day1
        {
            get
            {
                return day1;
            }
            set
            {
                day1 = value;
            }
        }
        /// <summary>
        /// ԭ�������
        /// </summary>
        public decimal Time1
        {
            get
            {
                return time1;
            }
            set
            {
                time1 = value;
            }
        }
        /// <summary>
        /// ԭ����gy����
        /// </summary>
        public decimal Gy1
        {
            get
            {
                return gy1;
            }
            set
            {
                gy1 = value;
            }
        }
        /// <summary>
        /// ���Ʒ�ʽ
        /// </summary>
        public string Cmodeid
        {
            get
            {
                return cmodeid;
            }
            set
            {
                cmodeid = value;
            }
        }
        /// <summary>
        /// ����װ��
        /// </summary>
        public string Rdeviceid
        {
            get
            {
                return rdeviceid;
            }
            set
            {
                rdeviceid = value;
            }
        }
        /// <summary>
        /// ���Ƴ�ʽ
        /// </summary>
        public string Rprocessid
        {
            get
            {
                return rprocessid;
            }
            set
            {
                rprocessid = value;
            }
        }
        /// <summary>
        /// ���Ʒ�ʽ
        /// </summary>
        public string Rmodeid
        {
            get
            {
                return rmodeid;
            }
            set
            {
                rmodeid = value;
            }
        }
        /// <summary>
        /// סԺ��ˮ��
        /// </summary>
        public string InpatientNo
        {
            get
            {
                return inpatientNo;
            }
            set
            {
                inpatientNo = value;
            }
        }
        #endregion

        #region ����
        public new Tumour Clone()
        {
            Tumour ct = (Tumour)base.Clone();
            ct.operInfo = this.operInfo.Clone();
            return ct;
        }
        #endregion

    }
}
