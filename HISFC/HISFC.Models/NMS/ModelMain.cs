using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.NMS
{
    /// <summary>
    /// [��������: ����ģ��ʵ����]<br></br>
    /// [�� �� ��: ��ΰ��]<br></br>
    /// [����ʱ��: 2008-10-07]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    /// 
    [System.Serializable]
    public class ModelMain : Neusoft.FrameWork.Models.NeuObject
    {
        #region ���캯��

	    /// <summary>
        /// ���캯�� (ID:ģ��������ˮ�ţ�Name:ģ������)
	    /// </summary>
        public ModelMain()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

	    #endregion ���캯��

        #region ����

        /// <summary>
        /// ģ�帱����
        /// </summary>
        private string modelSubName;
        
        /// <summary>
        /// �Ƿ���Ч1��Ч0ͣ��
        /// </summary>
        private bool validFlag;
        
        /// <summary>
        /// ģ���ѯ��
        /// </summary>
        private string modelSpell;
        
        /// <summary>
        /// ģ������:1һ��ʱ�,2�����ʱ�,3���˿���,4��������,5���ҿ���
        /// </summary>
        private string modelType;
        
        /// <summary>
        /// ��ͷ˵��
        /// </summary>
        private string headerMark;
        
        /// <summary>
        /// ҳ��˵��
        /// </summary>
        private string footerMark;
        
        /// <summary>
        /// ����������Ϣ������Ա���롢����ʱ�䣩
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment operInfo = new Neusoft.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// ģ�帱����
        /// </summary>
        public string ModelSubName
        {
            get 
            {
                return modelSubName; 
            }
            set 
            {
                modelSubName = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ч1��Ч0ͣ��
        /// </summary>
        public bool ValidFlag
        {
            get 
            {
                return validFlag; 
            }
            set 
            {
                validFlag = value; 
            }
        }

        /// <summary>
        /// ģ���ѯ��
        /// </summary>
        public string ModelSpell
        {
            get
            {
                return modelSpell;
            }
            set
            {
                modelSpell = value;
            }
        }

        /// <summary>
        /// ģ������:1һ��ʱ�,2�����ʱ�,3���˿���,4��������,5���ҿ���
        /// </summary>
        public string ModelType
        {
            get
            {
                return modelType;
            }
            set
            {
                modelType = value;
            }
        }

        /// <summary>
        /// ��ͷ˵��
        /// </summary>
        public string HeaderMark
        {
            get
            {
                return headerMark;
            }
            set
            {
                headerMark = value;
            }
        }

        /// <summary>
        /// ҳ��˵��
        /// </summary>
        public string FooterMark
        {
            get
            {
                return footerMark;
            }
            set
            {
                footerMark = value;
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
        public new ModelMain Clone()
        {
            ModelMain modelMain = base.Clone() as ModelMain;
            modelMain.OperInfo = this.operInfo.Clone();
            return modelMain;
        }

        #endregion
    }
}
