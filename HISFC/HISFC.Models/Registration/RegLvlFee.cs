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
        private RegLevel regLevel;

        /// <summary>
        /// ��ͬ��λ
        /// </summary>
        private FS.HISFC.Models.Base.Pact pact;
        
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
        private OperEnvironment oper;
        #endregion

        #region ����

        /// <summary>
        /// �Һż���
        /// </summary>
        public RegLevel RegLevel
        {
            get {
                if (this.regLevel == null)
                {
                    this.regLevel = new RegLevel();
                }
                return regLevel; }
            set { regLevel = value; }
        }

        /// <summary>
        /// ��ͬ��λ
        /// </summary>
        public Pact Pact
        {
            get {
                if (this.pact == null)
                {
                    this.pact = new Pact();
                }
                return this.pact; }
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
            get {
                if (this.oper == null)
                {
                    this.oper = new OperEnvironment();
                }
                return oper; }
            set { oper = value; }
        }

        #endregion

        #region ����
        public new RegLvlFee Clone ()
		{
			RegLvlFee regLvlFee = base.Clone() as RegLvlFee;
            regLvlFee.RegLevel = this.RegLevel.Clone();
            regLvlFee.Pact = this.Pact.Clone();

            return regLvlFee;
		}
        #endregion
    }
}
