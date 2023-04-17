using System;

namespace FS.HISFC.Models.HealthRecord
{
    /// <summary>
    /// PatientInfo <br></br>
    /// [��������: סԺ�ձ�ʵ��]<br></br>
    /// [�� �� ��: sunm]<br></br>
    /// [����ʱ��: 2007-07]<br></br>
    /// 
    /// <�޸ļ�¼
    /// 
    ///		�޸���=��ǿ
    ///		�޸�ʱ��=2007-7-23
    ///		�޸�Ŀ��=��Ӧҵ����Ҫ
    ///		�޸�����=����Ƿ��Ѿ��м�¼������
    ///  />
    /// </summary>
    [Serializable]
    public class DayReport : FS.FrameWork.Models.NeuObject
    {
        public DayReport()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����
        private string hasRecord;       
        //�ձ�����
        private DateTime date_stat;
        //����
        private FS.FrameWork.Models.NeuObject objDept = new FS.FrameWork.Models.NeuObject();
        //�����ڴ�λ��
        private int bed_stand;
        //�����ⲡ����
        private int bed_nonst;
        //�Ӵ���
        private int bed_add;
        //�մ���
        private int bed_free;
        //ԭ�в�����
        private int remain_yesterday;
        //������Ժ��
        private int in_normal;
        //������Ժ��
        private int in_emc;
        //ת����
        private int in_change;
        //�����Ժ��
        private int out_normal;
        //24Сʱ��������
        private int dead_in24;
        //24Сʱ��������
        private int dead_out24;
        //��������������
        private int dead_ezs;
        //ת����
        private int out_change;
        //��Ժ����
        private int withdrawal;
        //����һ�㻼��
        private int patient_normal;
        //���ػ�����
        private int patient_serious;
        //��Σ������
        private int patient_terminally;
        //����ʹ����
        private decimal bed_userate;
        //�㻤��
        private int tend_num;
        //�����ͼ���
        private int pa_num;
        //�ٴ����������
        private int clpa_num;
        //��¼״̬
        private string rec_flag;
        //������
        private FS.FrameWork.Models.NeuObject modi_usercd;
        //��������
        private DateTime modi_date;
        //ʵ��ռ����
        private int bed_guding;
        //��Ժ�ٻ��������ã�
        private int todayin_outchange;
        //δ֪�ֶ�
        private int todayin_inchange;
        //ҽ������
        private int yb_num;
        //�㻤����
        private int acc_num;
        //��ĩʵ������
        private int ban_pnum;
        //��ʿվ
        private FS.FrameWork.Models.NeuObject nurse_station;
        //�㴲��
        private int accom_num;
        //�촯��
        private int bedstore_num;
        //Ժ�ڸ�Ⱦ��
        private int infect_num;
        //��Һ����
        private int trans_num;
        //��Һ��Ӧ����
        private int fecttrans_num;
        //��Ѫ����
        private int blood_num;
        //��Ѫ��Ӧ����
        private int fectblood_num;
        //��Ժ�ٻ���
        private int in_back;
        #endregion

        #region ����
        public string HasRecord
        {
            get
            {
                return hasRecord;
            }
            set
            {
                hasRecord = value;
            }
        }
        /// <summary>
        /// �ձ�����
        /// </summary>
        public DateTime DateStat
        {
            get { return this.date_stat; }
            set { this.date_stat = value; }
        }
        /// <summary>
        /// �ձ�����
        /// </summary>
        public FS.FrameWork.Models.NeuObject Dept
        {
            get { return this.objDept; }
            set { this.objDept = value; }
        }
        /// <summary>
        /// �����ڴ�λ��
        /// </summary>
        public int BedStandNum 
        {
            get { return this.bed_stand; }
            set { this.bed_stand = value; }
        }
        /// <summary>
        /// �����ⲡ����
        /// </summary>
        public int BedNonstNum 
        {
            get { return this.bed_nonst; }
            set { this.bed_nonst = value; }
        }
        /// <summary>
        /// �Ӵ���
        /// </summary>
        public int BedAddNum 
        {
            get { return this.bed_add; }
            set { this.bed_add = value; }
        }
        /// <summary>
        /// �մ���
        /// </summary>
        public int BedFreeNum 
        {
            get { return this.bed_free; }
            set { this.bed_free = value; }
        }
        /// <summary>
        /// ԭ�в�����
        /// </summary>
        public int RemainYesterdayNum 
        {
            get { return this.remain_yesterday; }
            set { this.remain_yesterday = value; }
        }
        /// <summary>
        /// ������Ժ��
        /// </summary>
        public int InNormalNum 
        {
            get { return this.in_normal; }
            set { this.in_normal = value; }
        }
        /// <summary>
        /// ������Ժ��
        /// </summary>
        public int InEmcNum 
        {
            get { return this.in_emc; }
            set { this.in_emc = value; }
        }
        /// <summary>
        /// ת����
        /// </summary>
        public int InChangeNum 
        {
            get { return this.in_change; }
            set { this.in_change = value; }
        }
        /// <summary>
        /// �����Ժ��
        /// </summary>
        public int OutNormalNum 
        {
            get { return this.out_normal; }
            set { this.out_normal = value; }
        }
        /// <summary>
        /// 24Сʱ��������
        /// </summary>
        public int DeadIn24Num 
        {
            get { return this.dead_in24; }
            set { this.dead_in24 = value; }
        }
        /// <summary>
        /// 24Сʱ��������
        /// </summary>
        public int DeadOut24Num 
        {
            get { return this.dead_out24; }
            set { this.dead_out24 = value; }
        }
        /// <summary>
        /// ��������������
        /// </summary>
        public int DeadEzsNum 
        {
            get { return this.dead_ezs; }
            set { this.dead_ezs = value; }
        }
        /// <summary>
        /// ת����
        /// </summary>
        public int OutChangeNum 
        {
            get { return this.out_change; }
            set { this.out_change = value; }
        }
        /// <summary>
        /// ��Ժ����
        /// </summary>
        public int WithdrawalNum 
        {
            get { return this.withdrawal; }
            set { this.withdrawal = value; }
        }
        /// <summary>
        /// ����һ�㻼��
        /// </summary>
        public int PatientNormalNum 
        {
            get { return this.patient_normal; }
            set { this.patient_normal = value; }
        }
        /// <summary>
        /// ���ػ�����
        /// </summary>
        public int PatientSeriousNum 
        {
            get { return this.patient_serious; }
            set { this.patient_serious = value; }
        }
        /// <summary>
        /// ��Σ������
        /// </summary>
        public int PatientTerminallyNum 
        {
            get { return this.patient_terminally; }
            set { this.patient_terminally = value; }
        }
        /// <summary>
        /// ����ʹ����
        /// </summary>
        public decimal BedUseRate
        {
            get { return this.bed_userate; }
            set { this.bed_userate = value; }
        }
        /// <summary>
        /// �㻤��
        /// </summary>
        public int TendNum 
        {
            get { return this.tend_num; }
            set { this.tend_num = value; }
        }
        /// <summary>
        /// �����ͼ���
        /// </summary>
        public int PaNum 
        {
            get { return this.pa_num; }
            set { this.pa_num = value; }
        }
        /// <summary>
        /// �ٴ����������
        /// </summary>
        public int ClPaNum 
        {
            get { return this.clpa_num; }
            set { this.clpa_num = value; }
        }
        /// <summary>
        /// ��¼״̬
        /// </summary>
        public string RecFlag 
        {
            get { return this.rec_flag; }
            set { this.rec_flag = value; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public FS.FrameWork.Models.NeuObject ModiUser 
        {
            get { return this.modi_usercd; }
            set { this.modi_usercd = value; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public DateTime ModiDate 
        {
            get { return this.modi_date; }
            set { this.modi_date = value; }
        }
        /// <summary>
        /// ʵ��ռ����
        /// </summary>
        public int BedGuding 
        {
            get { return this.bed_guding; }
            set { this.bed_guding = value; }
        }
        /// <summary>
        /// ��Ժ�ٻ��������ã�
        /// </summary>
        public int TodayInoutChange 
        {
            get { return this.todayin_outchange; }
            set { this.todayin_outchange = value; }
        }
        /// <summary>
        /// δ֪�ֶ�,Ŀǰ���ݿ�û��ע�ͣ�����Ϊ�����ֶ�
        /// </summary>
        public int TodayIninChange 
        {
            get { return this.todayin_inchange; }
            set { this.todayin_inchange = value; }
        }
        /// <summary>
        /// ҽ������
        /// </summary>
        public int YbNum 
        {
            get { return this.yb_num; }
            set { this.yb_num = value; }
        }
        /// <summary>
        /// �㻤����
        /// </summary>
        public int AccNum 
        {
            get { return this.acc_num; }
            set { this.acc_num = value; }
        }
        /// <summary>
        /// ��ĩʵ������
        /// </summary>
        public int BanpNum
        {
            get { return this.ban_pnum; }
            set { this.ban_pnum = value; }
        }
        /// <summary>
        /// ��ʿվ
        /// </summary>
        public FS.FrameWork.Models.NeuObject NurseStation 
        {
            get { return this.nurse_station; }
            set { this.nurse_station = value; }
        }
        /// <summary>
        /// �㴲��
        /// </summary>
        public int AccomNum 
        {
            get { return this.accom_num; }
            set { this.accom_num = value; }
        }
        /// <summary>
        /// �촯��
        /// </summary>
        public int BedStoreNum 
        {
            get { return this.bedstore_num; }
            set { this.bedstore_num = value; }
        }
        /// <summary>
        /// Ժ�ڸ�Ⱦ��
        /// </summary>
        public int InfectNum 
        {
            get { return this.infect_num; }
            set { this.infect_num = value; }
        }
        /// <summary>
        /// ��Һ����
        /// </summary>
        public int TransNum 
        {
            get { return this.trans_num; }
            set { this.trans_num = value; }
        }
        /// <summary>
        /// ��Һ��Ӧ����
        /// </summary>
        public int FectTransNum 
        {
            get { return this.fecttrans_num; }
            set { this.fecttrans_num = value; }
        }
        /// <summary>
        /// ��Ѫ����
        /// </summary>
        public int BloodNum 
        {
            get { return this.blood_num; }
            set { this.blood_num = value; }
        }
        /// <summary>
        /// ��Ѫ��Ӧ����
        /// </summary>
        public int FectBloodNum 
        {
            get { return this.fectblood_num; }
            set { this.fectblood_num = value; }
        }
        /// <summary>
        /// ��Ժ�ٻ���
        /// </summary>
        public int InBackNum 
        {
            get { return this.in_back; }
            set { this.in_back = value; }
        }
        #endregion

        #region ����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new DayReport Clone()
        {
            DayReport myDayReport = base.Clone() as DayReport;
            myDayReport.Dept = this.Dept.Clone();
            myDayReport.ModiUser = this.ModiUser.Clone();
            myDayReport.NurseStation = this.NurseStation.Clone();

            return myDayReport;
        }

        #endregion
    }
}
