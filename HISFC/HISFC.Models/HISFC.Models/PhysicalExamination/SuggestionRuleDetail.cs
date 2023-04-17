using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.PhysicalExamination
{
    /// <summary>
    /// SuggestionRuleDetail<br></br>
    /// [��������: ��콨�������ϸ]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-06-10]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class SuggestionRuleDetail :FS.FrameWork.Models.NeuObject
    {
        #region ˽�б���  
        // ID ������ϸ����
        //Name ������ϸ����
        private SuggestionRule rule = new SuggestionRule(); //������ 
        private FS.FrameWork.Models.NeuObject itemType = new FS.FrameWork.Models.NeuObject();//��������
        private FS.FrameWork.Models.NeuObject firstOperation = new FS.FrameWork.Models.NeuObject();//��������1
        private FS.FrameWork.Models.NeuObject firstDetailValue = new FS.FrameWork.Models.NeuObject();//ֵ1
        private FS.FrameWork.Models.NeuObject secondOperation = new FS.FrameWork.Models.NeuObject();//��������2
        private FS.FrameWork.Models.NeuObject secondDetailValue = new FS.FrameWork.Models.NeuObject();//ֵ2
        //����Ա��Ϣ
        private FS.HISFC.Models.Base.OperEnvironment operInfo = new FS.HISFC.Models.Base.OperEnvironment();
        #endregion 

        #region  �������� 
        /// <summary>
        /// ���� 
        /// </summary>
        public SuggestionRule Rule
        {
            get
            {
                return rule;
            }
            set
            {
                rule = value;
            }
        } 
        /// <summary>
        /// ��������
        /// </summary>
        public FS.FrameWork.Models.NeuObject FirstOperation
        {
            get
            {
                return firstOperation;
            }
            set
            {
                firstOperation = value;
            }
        }
        /// <summary>
        /// ֵ1
        /// </summary>
        public FS.FrameWork.Models.NeuObject FirstDetailValue
        {
            get
            {
                return firstDetailValue;
            }
            set
            {
                firstDetailValue = value;
            }
        }
        /// <summary>
        ///�������� 
        /// </summary>
        public FS.FrameWork.Models.NeuObject ItemType
        {
            get
            {
                return itemType;
            }
            set
            {
                itemType = value;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public FS.FrameWork.Models.NeuObject SecondOperation
        {
            get
            {
                return secondOperation;
            }
            set
            {
                secondOperation = value;
            }
        }
        /// <summary>
        /// ֵ
        /// </summary>
        public FS.FrameWork.Models.NeuObject SecondDetailValue
        {
            get
            {
                return secondDetailValue;
            }
            set
            {
                secondDetailValue = value;
            }
        }
        #endregion 

        #region ��¡����
        #endregion 
    }
}
