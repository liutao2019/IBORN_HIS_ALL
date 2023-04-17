using System;


namespace FS.HISFC.Models.HealthRecord
{


    /// <summary>
    /// QCTest ��ժҪ˵���������ʿ���Ϣ�Ǽ� ���ʵ����
    /// </summary>
    [Serializable]
    public class QCTest : FS.FrameWork.Models.NeuObject
    {
        public QCTest()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        //		PARENT_CODE  VARCHAR2(14)                  ����ҽ�ƻ�������        
        //		CURRENT_CODE VARCHAR2(14)                  ����ҽ�ƻ�������        
        //		SEQUENCE_NO  VARCHAR2(10)                  ������ˮ��              
        //		TEST_DATE    DATE         Y                ��������                
        //		EMPL_CODE    VARCHAR2(6)  Y                ��Ա���                
        //		EMPL_NAME    VARCHAR2(10) Y                ��Ա����                
        //		MARK         NUMBER(4,2)  Y                ����                    
        //		LEVL_CODE    VARCHAR2(2)  Y                ҽʦ�������            
        //		LEVL_NAME    VARCHAR2(50) Y                ҽʦ��������            
        //		VALID_STATE  VARCHAR2(1)  Y                �Ƿ����� 0 ��Ч 1  ���� 
        //		OPER_CODE    VARCHAR2(6)  Y                ¼�����Ա              
        //		OPER_DATE    DATE         Y                ¼��ʱ�� 
        #region  ˽�б���

        //ID ������ˮ��
        //����ʱ��
        private string testDate;
        //��Ա ID����  name ����
        private FS.FrameWork.Models.NeuObject emplInfo = new FS.FrameWork.Models.NeuObject();
        //����
        private float mark;
        //ҽʦ ID ������� NAME ��������
        private FS.FrameWork.Models.NeuObject levelInfo = new FS.FrameWork.Models.NeuObject();
        //��Ч�Ա�ʶ
        private bool validState;
        //����Ա
        private FS.FrameWork.Models.NeuObject operInfo = new FS.FrameWork.Models.NeuObject();
        //¼��ʱ��
        private DateTime operDate;
        #endregion

        #region ��������
        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        public FS.FrameWork.Models.NeuObject OperInfo
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
        /// <summary>
        /// ��Ч
        /// </summary>
        public bool ValidState
        {
            get
            {
                return validState;
            }
            set
            {
                validState = value;
            }
        }
        /// <summary>
        /// ҽʦ���� id ������� name ��������
        /// </summary>
        public FS.FrameWork.Models.NeuObject LevelInfo
        {
            get
            {
                return levelInfo;
            }
            set
            {
                levelInfo = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public float Mark
        {
            get
            {
                return mark;
            }
            set
            {
                mark = value;
            }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public string TestDate
        {
            get
            {
                return testDate;
            }
            set
            {
                testDate = value;
            }
        }
        /// <summary>
        /// �μӿ�����Ա��Ϣ id ��Ա���� name ��Ա���� 
        /// </summary>
        public FS.FrameWork.Models.NeuObject EmplInfo
        {
            get
            {
                return emplInfo;
            }
            set
            {
                emplInfo = value;
            }
        }
        #endregion


        public new QCTest Clone()
        {
            QCTest qct = (QCTest)base.Clone();
            qct.emplInfo = this.emplInfo.Clone();
            qct.levelInfo = this.levelInfo.Clone();
            qct.operInfo = this.operInfo.Clone();
            return qct;

        }

    }
}
