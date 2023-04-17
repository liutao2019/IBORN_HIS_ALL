using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Equipment
{
    /// <summary>
    /// Company<br></br>
    /// [��������: �豸�۾ɷ���ʵ��]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-10-30]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    /// 
    [System.Serializable]
    public class DeMethod : FS.HISFC.Models.Base.Spell
    {
        /// <summary>
	    /// ���캯��
	    /// </summary>
        public DeMethod()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

	    #region ����

        /// <summary>
        /// �۾ɷ�ʽ����
        /// </summary>
        private string deNote;

        /// <summary>
        /// �۾ɹ�ʽ����
        /// </summary>
        private string funNote;


        /// <summary>
        /// �¼������(С��)
        /// </summary>
        private decimal monthRate;

        /// <summary>
        /// ��������(С��)
        /// </summary>
        private decimal yearRate;

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        #endregion ����

        #region ����

        /// <summary>
        /// �۾ɷ�ʽ����
        /// </summary>
        public string DeNote
        {
            get { return deNote; }
            set { deNote = value; }
        }

        /// <summary>
        /// �۾ɹ�ʽ����
        /// </summary>
        public string FunNote
        {
            get { return funNote; }
            set { funNote = value; }
        }

        /// <summary>
        /// �¼������(С��)
        /// </summary>
        public decimal MonthRate
        {
            get { return monthRate; }
            set { monthRate = value; }
        }

        /// <summary>
        /// ��������(С��)
        /// </summary>
        public decimal YearRate
        {
            get { return yearRate; }
            set { yearRate = value; }
        }

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get { return oper; }
            set { oper = value; }
        }

        #endregion ����

        #region ����

        /// <summary>
        /// ������¡
        /// </summary>
        /// <returns>�ɹ����ؿ�¡���DeMethodʵ�� ʧ�ܷ���null</returns>
        public new DeMethod Clone()
        {
            DeMethod deMethod = base.Clone() as DeMethod;
            deMethod.oper = this.oper.Clone();
            return deMethod;
        }

        #endregion ����
    }
}
