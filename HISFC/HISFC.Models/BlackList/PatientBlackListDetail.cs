using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.BlackList
{
    /// <summary>
    /// FS.HISFC.Models.BlackList.PatientBlackListDetail<br></br>
    /// [��������: ������������ϸʵ��]<br></br>
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
    public class PatientBlackListDetail :NeuObject
    {
        #region ����
        /// <summary>
        /// ��ˮ��
        /// </summary>
        private string seqNo = string.Empty;
        /// <summary>
        /// �Ƿ��ں�����
        /// </summary>
        private bool blackListValid = true;
        /// <summary>
        /// ��������
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();
        #endregion

        #region ����
        /// <summary>
        /// ��ˮ��
        /// </summary>
        public string SeqNO
        {
            get
            {
                return seqNo;
            }
            set
            {
                seqNo = value;
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
        /// ��������
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
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
        public new PatientBlackListDetail Clone()
        {
            PatientBlackListDetail obj = base.Clone() as PatientBlackListDetail;
            obj.Oper = this.Oper.Clone();
            return obj;
        }
        #endregion
    }
}
