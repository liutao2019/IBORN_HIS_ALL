using System;
using FS.FrameWork.Models;


namespace FS.HISFC.Models.HealthRecord
{
    /// <summary>
    /// ICDTemplate���ģ����<br></br>
    /// [��������: ICDTemplate]<br></br>
    /// [�� �� ��: ���S]<br></br>
    /// [����ʱ��: 2009-04-17]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class ICDCategory : NeuObject, FS.HISFC.Models.Base.ISpell
    {
        public ICDCategory()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����

        /// <summary>
        /// �ϼ�����
        /// </summary>
        private string parentID = "";

        /// <summary>
        /// �ϼ�����
        /// </summary>
        public string ParentID
        {
            get
            {
                return parentID;
            }
            set
            {
                parentID = value;
            }
        }

        /// <summary>
        /// ���
        /// </summary>
        private string seqNO = "";

        /// <summary>
        /// ���
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
        /// ƴ����
        /// </summary>
        private string spellCode = "";

        /// <summary>
        /// �����
        /// </summary>
        private string wbCode = "";

        /// <summary>
        /// �Զ�����
        /// </summary>
        private string userCode = "";

        /// <summary>
        /// ����
        /// </summary>
        private string sortID = "";

        /// <summary>
        /// ����
        /// </summary>
        public string SortID
        {
            get
            {
                return sortID;
            }
            set
            {
                sortID = value;
            }
        }

        /// <summary>
        /// ICD���뷶Χ
        /// </summary>
        private string range = "";

        /// <summary>
        /// ICD���뷶Χ
        /// </summary>
        public string Range
        {
            get
            {
                return range;
            }
            set
            {
                range = value;
            }
        }

        /// <summary>
        /// ��֪����ɶ��
        /// </summary>
        private string sfbr = "";


        /// <summary>
        /// ��֪����ɶ��
        /// </summary>
        public string Sfbr
        {
            get
            {
                return sfbr;
            }
            set
            {
                sfbr = value;
            }
        }

        #endregion

        #region ��¡

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public new ICDCategory Clone()
        {
            ICDCategory ICDTemplate = base.Clone() as ICDCategory;
            return ICDTemplate;
        }

        #endregion

        #region ISpell ��Ա

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
    }
}
