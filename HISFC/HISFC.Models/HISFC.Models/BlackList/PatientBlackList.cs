using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.BlackList
{
    /// <summary>
    /// FS.HISFC.Models.BlackList.PatientBlackList<br></br>
    /// [��������: ����������ʵ��]<br></br>
    /// [�� �� ��: ·־��]<br></br>
    /// [����ʱ��: 2007-09-03]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    /// 
    [System.Serializable]
    public class PatientBlackList :NeuObject
    {
        #region ����
        /// <summary>
        /// ������
        /// </summary>
        private string cardNO = string.Empty;
        /// <summary>
        /// �Ƿ��ں�����
        /// </summary>
        private bool blackListValid = false;
        /// <summary>
        /// ��������ϸ
        /// </summary>
        private List<PatientBlackListDetail> blackListDetail = new List<PatientBlackListDetail>();
        #endregion

        #region ����
        /// <summary>
        /// ������
        /// </summary>
        public string CardNO
        {
            get
            {
                return cardNO;
            }
            set
            {
                cardNO = value;
            }
        }

        /// <summary>
        /// �Ƿ��ں�����
        /// </summary>
        public bool BlackListValid
        {
            get
            {
                return blackListValid;
            }
            set
            {
                blackListValid = value;
            }
        }

        /// <summary>
        /// ��������ϸ
        /// </summary>
        public List<PatientBlackListDetail> BlackListDetail
        {
            get
            {
                return blackListDetail;
            }
            set
            {
                blackListDetail = value;
            }
        }
        #endregion

        #region ����
        public new PatientBlackList Clone()
        {
            PatientBlackList obj = base.Clone() as PatientBlackList;
            List<PatientBlackListDetail> list = new List<PatientBlackListDetail>();
            foreach (PatientBlackListDetail detail in BlackListDetail)
            {
                list.Add(detail.Clone());
            }
            obj.BlackListDetail = list;
            return obj;
        }
        #endregion
    }
}
