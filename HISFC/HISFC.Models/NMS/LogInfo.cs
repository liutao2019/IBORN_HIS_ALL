using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.NMS
{
    /// <summary>
    /// [��������: ���������־ʵ����]<br></br>
    /// [�� �� ��: ��ΰ��]<br></br>
    /// [����ʱ��: 2008-10-07]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class LogInfo : Neusoft.FrameWork.Models.NeuObject
    {
        #region ���캯��

	    /// <summary>
        /// ���캯�� (ID:��־��ˮ��)
	    /// </summary>
        public LogInfo()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

	    #endregion ���캯��

        #region ����

        /// <summary>
        /// ��������:1���,2ɾ��,3�޸�,4��ѯ
        /// </summary>
        private string operType;

        /// <summary>
        /// ����
        /// </summary>
        private string tbName;

        /// <summary>
        /// �����ֶ���"|"�ָ�
        /// </summary>
        private string keyName;

        /// <summary>
        /// �����ֶ�����"|"�ָ�
        /// </summary>
        private string keyValue;

        /// <summary>
        /// �޸ĵ��ֶ���"|"�ָ�
        /// </summary>
        private string fieldName;

        /// <summary>
        /// �޸ĵ��ֶκ���"|"�ָ�
        /// </summary>
        private string fieldCont;

        /// <summary>
        /// ��ǰ�ֶ�����"|"�ָ�
        /// </summary>
        private string oldValue;

        /// <summary>
        /// �ĺ��ֶ�����"|"�ָ�
        /// </summary>
        private string newValue;

        /// <summary>
        /// ����������Ϣ������Ա���롢����ʱ�䣩
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment operInfo = new Neusoft.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// ��������:1���,2ɾ��,3�޸�,4��ѯ
        /// </summary>
        public string OperType
        {
            get
            {
                return operType;
            }
            set
            {
                operType = value;
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
        public string KeyName
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
        public string KeyValue
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
        public string FieldName
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
        /// �޸ĵ��ֶκ���"|"�ָ�
        /// </summary>
        public string FieldCont
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
        public string OldValue
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
        public string NewValue
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
        /// ����������Ϣ������Ա���롢����ʱ�䣩
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment OperInfo
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

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new LogInfo Clone()
        {
            LogInfo logInfo = base.Clone() as LogInfo;
            logInfo.OperInfo = this.operInfo.Clone();

            return logInfo;
        }

        #endregion
    }
}
