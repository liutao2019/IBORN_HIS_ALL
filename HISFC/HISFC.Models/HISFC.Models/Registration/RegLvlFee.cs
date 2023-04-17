using System;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Registration
{   
    /// <summary>
    /// <br>RegLvlFee</br>
    /// <br>[��������: �Һŷ�ʵ��]</br>
    /// <br>[�� �� ��: ��С��]</br>
    /// <br>[����ʱ��: 2007-2-1]</br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class RegLvlFee:FS.FrameWork.Models.NeuObject
	{        
        /// <summary>
        /// �Һŷ�ʵ��
        /// </summary>
		public RegLvlFee()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//            
        }

        #region ����
        /// <summary>
        ///�Һż��� 
        /// </summary>
        private RegLevel regLevel = new RegLevel();

        /// <summary>
        /// ��ͬ��λ
        /// </summary>
        private FS.HISFC.Models.Base.Pact pact = new Pact();
        
        /// <summary>
        /// �Һŷ�
        /// </summary>
        private decimal regFee = 0m;

        /// <summary>
        /// ����
        /// </summary>
        private decimal chkFee = 0m;
        /// <summary>
        /// �Է�����
        /// </summary>
        private decimal ownDigFee = 0m;

        /// <summary>
        /// ��������
        /// </summary>
        private decimal pubDigFee = 0m;
        /// <summary>
        /// ������
        /// </summary>
        private decimal othFee = 0m;

        /// <summary>
        /// ��������
        /// </summary>
        private OperEnvironment oper = new OperEnvironment();
        #endregion

        #region ����

        /// <summary>
        /// �Һż���
        /// </summary>
        public RegLevel RegLevel
        {
            get { return regLevel; }
            set { regLevel = value; }
        }

        /// <summary>
        /// ��ͬ��λ
        /// </summary>
        public Pact Pact
        {
            get { return this.pact; }
            set { this.pact = value; }
        }

        /// <summary>
        /// �Һŷ�
        /// </summary>
        public decimal RegFee
        {
            get { return this.regFee; }
            set { this.regFee = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public decimal ChkFee
        {
            get { return this.chkFee; }
            set { this.chkFee = value; }
        }

        /// <summary>
        /// �Է�����
        /// </summary>
        public decimal OwnDigFee
        {
            get { return this.ownDigFee; }
            set { this.ownDigFee = value; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public decimal PubDigFee
        {
            get { return this.pubDigFee; }
            set { this.pubDigFee = value; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public decimal OthFee
        {
            get { return this.othFee; }
            set { this.othFee = value; }
        }
         
        /// <summary>
        /// ��������
        /// </summary>
        public OperEnvironment Oper
        {
            get { return oper; }
            set { oper = value; }
        }

        #endregion

        #region ����
        public new RegLvlFee Clone ()
		{
			RegLvlFee regLvlFee = base.Clone() as RegLvlFee;
            regLvlFee.RegLevel = this.regLevel.Clone();
            regLvlFee.Pact = this.pact.Clone();

            return regLvlFee;
		}
        #endregion
    }
}
