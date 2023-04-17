using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.HealthRecord.CaseHistory
{
    /// <summary>
    /// ��������ʵ��
    /// �����ڶ�ݸ������ʵ����һ�ֳ������������¼7�쳬��  ����ԭ����10�쳬�� 2011-8-2 by chengym
    /// </summary>
    public class CallBack : FS.FrameWork.Models.NeuObject
    {
        #region variable
        private  FS.HISFC.Models.RADT.PatientInfo patient=new FS.HISFC.Models.RADT.PatientInfo();
        private string isCallback;
        private FS.HISFC.Models.Base.OperEnvironment callbackOper=new FS.HISFC.Models.Base.OperEnvironment();
        private string isDocument;
        private FS.HISFC.Models.Base.OperEnvironment documentOper=new FS.HISFC.Models.Base.OperEnvironment();
        private int sevenDaysTimeout;
        private int fourteenDaysTimeout;
        #endregion

        #region attribute
        /// <summary>
        /// 7�쳬��
        /// </summary>
        public int SevenDaysTimeout
        {
            get { return sevenDaysTimeout; }
            set { sevenDaysTimeout = value; }
        }
        /// <summary>
        /// ʮ�쳬��
        /// </summary>
        public int FourteenDaysTimeout
        {
            get { return fourteenDaysTimeout; }
            set { fourteenDaysTimeout = value; }
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get { return this.patient; }
            set { this.patient = value; }
        }
        /// <summary>
        /// ���ձ�־ 0δ���� 1 ����
        /// </summary>
        public string IsCallback
        {
            get { return isCallback; }
            set { isCallback = value; }
        }

        /// <summary>
        /// ������Ա����
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment CallbackOper
        {
            get { return this.callbackOper; }
            set { this.callbackOper = value; }
        }
        /// <summary>
        /// �Ƿ�鵵
        /// </summary>
        public string IsDocument
        {
            get { return isDocument; }
            set { isDocument = value; }
        }

        /// <summary>
        /// �鵵��
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment DocumentOper
        {
            get { return this.documentOper; }
            set { this.documentOper = value; }
        }
      
        #endregion

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public new CallBack Clone()
        {
            CallBack cb = base.Clone() as CallBack;
            cb.patient = this.patient.Clone();
            cb.callbackOper = this.callbackOper.Clone();
            cb.documentOper = this.documentOper.Clone();
            return cb;
        }


    }
}
