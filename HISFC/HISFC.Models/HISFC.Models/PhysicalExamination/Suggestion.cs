using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.PhysicalExamination
{
    /// <summary>
    /// Suggestion<br></br>
    /// [��������: ��콨�鱣��]<br></br>
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
    public class Suggestion : FS.FrameWork.Models.NeuObject
    { 
        #region ˽�б���
        //���к�
        private string seqNO = string.Empty;
        //�����ˮ��
        private string chkClinicNo = string.Empty; 
        //�����
        private string itemValue = string.Empty;
        //��콨��
        private SuggestionRule suggestions = new SuggestionRule();
        //����Ա��Ϣ
        private FS.HISFC.Models.Base.OperEnvironment operInfo = new FS.HISFC.Models.Base.OperEnvironment();

        #endregion 

        #region  ��������

        /// <summary>
        /// ��ˮ��
        /// </summary>
        public string SeqNO
        {
            get
            {
                return seqNO;
            }
            set
            {
                seqNO = value;
            }
        }

        /// <summary>
        /// �����ˮ��
        /// </summary>
        public string ChkClinicNo
        {
            get
            {
                return chkClinicNo;
            }
            set
            {
                chkClinicNo = value;
            }
        }

        /// <summary>
        /// �����
        /// </summary>
        public string ItemValue
        {
            get
            {
                return itemValue;
            }
            set
            {
                itemValue = value;
            }
        }
        /// <summary>
        /// ��콨��
        /// </summary>
        public SuggestionRule Suggestions
        {
            get
            {
                return suggestions;
            }
            set
            {
                suggestions = value;
            }
        }

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
        #endregion 
    }
}
