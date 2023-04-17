using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.NMS
{
    /// <summary>
    /// [��������: Ȩ�޷�Χ����ʵ����]<br></br>
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
    public class Grant : Neusoft.FrameWork.Models.NeuObject
    {
        #region ���캯��

	    /// <summary>
        /// ���캯�� (ID:��ˮ��)
	    /// </summary>
        public Grant()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

	    #endregion ���캯��

        #region ����

        /// <summary>
        /// ������(��ԱID)
        /// </summary>
        private string grantOper;

        /// <summary>
        /// �������ͣ�1һ��ʱ���2�����ʱ���3���˿��ˣ�4�������ˣ�5���ҿ���
        /// </summary>
        private string grantType;

        /// <summary>
        /// ��������(��ԱID)
        /// </summary>
        private string inGrantOper;

        /// <summary>
        /// �����䲡��
        /// </summary>
        private string inGrantWard;

        /// <summary>
        /// ���������
        /// </summary>
        private string inGrantDept;

        /// <summary>
        /// ����Ȩ��:1��,0��
        /// </summary>
        private string writeFlag;

        /// <summary>
        /// �Ķ�Ȩ��:1��,0��
        /// </summary>
        private string readFlag;

        /// <summary>
        /// ����������Ϣ������Ա���롢����ʱ�䣩
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment operInfo = new Neusoft.HISFC.Models.Base.OperEnvironment();
        
        #endregion 

        #region ����

        /// <summary>
        /// �����ˣ���ԱID��
        /// </summary>
        public string GrantOper
        {
            get
            {
                return grantOper;
            }
            set
            {
                grantOper = value;
            }
        }

        /// <summary>
        /// �������ͣ�1һ��ʱ���2�����ʱ���3���˿��ˣ�4�������ˣ�5���ҿ���
        /// </summary>
        public string GrantType
        {
            get
            {
                return grantType;
            }
            set
            {
                grantType = value;
            }
        }

        /// <summary>
        /// �������ˣ���ԱID��
        /// </summary>
        public string InGrantOper
        {
            get
            {
                return inGrantOper;
            }
            set
            {
                inGrantOper = value;
            }
        }

        /// <summary>
        /// �����䲡��
        /// </summary>
        public string InGrantWard
        {
            get
            {
                return inGrantWard;
            }
            set
            {
                inGrantWard = value;
            }
        }

        /// <summary>
        /// ���������
        /// </summary>
        public string InGrantDept
        {
            get
            {
                return inGrantDept;
            }
            set
            {
                inGrantDept = value;
            }
        }

        /// <summary>
        /// ����Ȩ��:1��,0��
        /// </summary>
        public string WriteFlag
        {
            get
            {
                return writeFlag;
            }
            set
            {
                writeFlag = value;
            }
        }

        /// <summary>
        /// �Ķ�Ȩ��:1��,0��
        /// </summary>
        public string ReadFlag
        {
            get
            {
                return readFlag;
            }
            set
            {
                readFlag = value;
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
        public new Grant Clone()
        {
            Grant grant = base.Clone() as Grant;
            grant.OperInfo = this.operInfo.Clone();

            return grant;
        }

        #endregion
    }
}
