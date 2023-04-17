using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.PhysicalExamination
{
    /// <summary>
    /// SuggestionRule<br></br>
    /// [��������: ��콨�����]<br></br>
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
    public class SuggestionRule : FS.FrameWork.Models.NeuObject, FS.HISFC.Models.Base.IValid,FS.HISFC.Models.Base.ISpell
    {
        #region ˽�б���
        //ID �������
        //Name ��������
        private bool isValid = true; //��Ч��־
        private string spellCode = string.Empty; //ƴ����
        private string wbCode = string.Empty;//�����
        private string userCode = string.Empty;//�Զ�����
        private string suggestionValue = string.Empty;//����
        private FS.HISFC.Models.Base.OperEnvironment operInfo = new FS.HISFC.Models.Base.OperEnvironment();
        #endregion

        #region ��������

        /// <summary>
        /// ����Ա��Ϣ
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
        /// ����
        /// </summary>
        public string SuggestionValue
        {
            get
            {
                return suggestionValue;
            }
            set
            {
                suggestionValue = value;
            }
        }

        #region IValid ��Ա
        /// <summary>
        /// ��Ч��־
        /// </summary>
        public bool IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                isValid = value;
            }
        }
        #endregion

        #region ISpell ��Ա
        /// <summary>
        /// ƴ����
        /// </summary>
        public string SpellCode
        {
            get
            {
                return spellCode;
            }
            set
            {
                spellCode = value;
            }
        }
        /// <summary>
        /// �����
        /// </summary>
        public string WBCode
        {
            get
            {
                return wbCode;
            }
            set
            {
                wbCode = value;
            }
        }
        /// <summary>
        /// �Զ�����
        /// </summary>
        public string UserCode
        {
            get
            {
                return userCode;
            }
            set
            {
                userCode = value;
            }
        }

        #endregion

        #endregion 

        #region ��¡����
        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public new SuggestionRule Clone()
        {
            SuggestionRule obj = base.Clone() as SuggestionRule;
            obj.operInfo = this.operInfo.Clone();
            return obj;
        }
        #endregion
    }
}