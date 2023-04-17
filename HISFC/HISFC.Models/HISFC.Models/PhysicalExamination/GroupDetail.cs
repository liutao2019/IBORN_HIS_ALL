using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.PhysicalExamination
{
    /// <summary>
    /// GroupDetail<br></br>
    /// [��������: ������׹�����]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-03-2]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class GroupDetail : FS.HISFC.Models.Fee.ComGroupTail
    {
        #region ����
        /// <summary>
        /// ��Ч��־
        /// </summary>
        private string validState = string.Empty;
        /// <summary>
        /// ������
        /// </summary>
        private int chkTime ;
        /// <summary>
        /// ���
        /// </summary>
        private string spacs = string.Empty;
        /// <summary>
        /// ִ�п���
        /// </summary>
        private FS.FrameWork.Models.NeuObject execDept = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// ʵ�ʼ۸�
        /// </summary>
        private decimal realPrice;
        #endregion 

        #region ����
        /// <summary>
        /// ��Ч 0 ��Ч 1��Ч 
        /// </summary>
        public string ValidState
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
        /// ������
        /// </summary>
        public int ChkTime
        {
            get
            {
                return chkTime;
            }
            set
            {
                chkTime = value;
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public string Spacs
        {
            get
            {
                return spacs;
            }
            set
            {
                spacs = value;
            }
        }
        /// <summary>
        /// ִ�п���
        /// </summary>
        public FS.FrameWork.Models.NeuObject ExecDept
        {
            get
            {
                return execDept;
            }
            set
            {
                execDept = value;
            }
        }
        /// <summary>
        /// ʵ�ʼ۸�
        /// </summary>
        public decimal RealPrice
        {
            get
            {
                return realPrice;
            }
            set
            {
                realPrice = value;
            }
        }
        #endregion 
        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public new GroupDetail Clone()
        {
            GroupDetail obj = base.Clone() as GroupDetail;
            obj.execDept = this.execDept.Clone();
            return obj;
        }
    }
}
