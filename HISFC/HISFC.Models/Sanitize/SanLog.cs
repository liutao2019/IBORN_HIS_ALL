using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.Sanitize
{
    /// <summary>
    /// [��������: ��Ʒ��־����]<br></br>
    /// [�� �� ��: shizj]<br></br>
    /// [����ʱ��: 2008-08]<br></br>
    /// </summary>
    public class SanLog : Neusoft.NFC.Object.NeuObject
    {
        public SanLog()
        {

        }
        #region ����

        /// <summary>
        /// ��־��ˮ��SEQ_SAN_OTHER_CODE
        /// </summary>
        private string logNo = string.Empty;

        /// <summary>
        /// ��������1���2ɾ��3�޸�4��ѯ
        /// </summary>
        private LogType logType = LogType.Add;

        /// <summary>
        /// ����
        /// </summary>
        private string tbName = string.Empty;

        /// <summary>
        /// �����ֶ���"|"�ָ�
        /// </summary>
        private StringBuilder keyName = new StringBuilder(100);

        /// <summary>
        /// �����ֶ�����"|"�ָ�
        /// </summary>
        private StringBuilder keyValue = new StringBuilder(100);

        /// <summary>
        /// �޸ĵ��ֶ���"|"�ָ�
        /// </summary>
        private StringBuilder fieldName = new StringBuilder(3000);

        /// <summary>
        ///�޸ĵ��ֶκ���"|"�ָ�
        /// </summary>
        private StringBuilder fieldCont = new StringBuilder(3000);

        /// <summary>
        /// ��ǰ�ֶ�����"|"�ָ�
        /// </summary>
        private StringBuilder oldValue = new StringBuilder(3000);

        /// <summary>
        /// �ĺ��ֶ�����"|"�ָ�
        /// </summary>
        private StringBuilder newValue = new StringBuilder(3000);

        /// <summary>
        /// ��Ա��Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment oper = new Neusoft.HISFC.Object.Base.OperEnvironment();


        #endregion

        #region ����
        /// <summary>
        /// ��־��ˮ��SEQ_SAN_OTHER_CODE
        /// </summary>
        public string LogNo
        {
            get
            {
                return logNo;
            }
            set
            {
                logNo = value;
            }
        }

        /// <summary>
        /// ��������1���2ɾ��3�޸�4��ѯ
        /// </summary>
        public LogType LogType
        {
            get
            {
                return logType;
            }
            set
            {
                logType = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string TbName
        {
            get
            {
                return tbName;
            }
            set
            {
                tbName = value;
            }
        }

        /// <summary>
        /// �����ֶ���"|"�ָ�
        /// </summary>
        public StringBuilder KeyName
        {
            get
            {
                return keyName;
            }
            set
            {
                keyName = value;
            }
        }

        /// <summary>
        /// �����ֶ�����"|"�ָ�
        /// </summary>
        public StringBuilder KeyValue
        {
            get
            {
                return keyValue;
            }
            set
            {
                keyValue = value;
            }
        }

        /// <summary>
        /// �޸ĵ��ֶ���"|"�ָ�
        /// </summary>
        public StringBuilder FieldName
        {
            get
            {
                return fieldName;
            }
            set
            {
                fieldName = value;
            }
        }

        /// <summary>
        ///�޸ĵ��ֶκ���"|"�ָ�
        /// </summary>
        public StringBuilder FieldCont
        {
            get
            {
                return fieldCont;
            }
            set
            {
                fieldCont = value;
            }
        }

        /// <summary>
        /// ��ǰ�ֶ�����"|"�ָ�
        /// </summary>
        public StringBuilder OldValue
        {
            get
            {
                return oldValue;
            }
            set
            {
                oldValue = value;
            }
        }

        /// <summary>
        /// �ĺ��ֶ�����"|"�ָ�
        /// </summary>
        public StringBuilder NewValue
        {
            get
            {
                return newValue;
            }
            set
            {
                newValue = value;
            }
        }

        /// <summary>
        /// ��Ա��Ϣ
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment Oper
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

        #endregion

        #region ����
        #region clone

        public new SanLog Clone()
        {
            SanLog sanlog = base.Clone() as SanLog;
            sanlog.Oper = this.Oper.Clone();

            return sanlog;
        }
        #endregion
        #endregion

    }
}
