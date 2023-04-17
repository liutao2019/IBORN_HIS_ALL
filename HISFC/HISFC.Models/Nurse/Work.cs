using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Nurse
{
    /// <summary>
    /// <br>RegLevel</br>
    /// <br>[��������: ��ʿ�Ű�ʵ��]</br>
    /// <br>[�� �� ��: ����]</br>
    /// <br>[����ʱ��: 2007-9-9]</br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    /// 
    [System.Serializable]
    public class Work
    {
         public Work()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����

        /// <summary>
        /// �Ű�ģ��
        /// </summary>
        private WorkTemplet templet = new WorkTemplet();

        /// <summary>
        /// �Ű���Ϣ
        /// </summary>
        public WorkTemplet Templet
        {
            get { return templet; }
            set { templet = value; }
        }

        /// <summary>
        /// �Ű�ʱ��
        /// </summary>
        private DateTime workDate = DateTime.MaxValue;

        public DateTime WorkDate
        {
            get { return workDate; }
            set { workDate = value; }
        }
       
        /// <summary>
        /// �Ű����
        /// </summary>
        private int workNO = 0;

        public int WorkNO
        {
            get { return workNO; }
            set { workNO = value; }
        }

        #endregion
   
        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public Work Clone()
        {
            Work obj = base.MemberwiseClone() as Work;

            obj.Templet = this.Templet.Clone();            

            return obj;
        }
        #endregion

    }
}
