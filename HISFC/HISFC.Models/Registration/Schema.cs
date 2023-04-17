using System;

namespace FS.HISFC.Models.Registration
{
    /// <summary>
    /// <br>RegLevel</br>
    /// <br>[��������: �Ű�ʵ��]</br>
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
    public class Schema
    {
        /// <summary>
        /// 
        /// </summary>
        public Schema()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����

        /// <summary>
        /// �Ű�ģ��
        /// </summary>
        private SchemaTemplet templet;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime seeDate = DateTime.MaxValue;

        /// <summary>
        /// �ѹҺ���
        /// </summary>
        private decimal regedQTY = 0m;

        /// <summary>
        /// ԤԼ�绰�ѹ�
        /// </summary>
        private decimal telingQTY = 0m;

        /// <summary>
        /// ԤԼ�绰��ȷ����
        /// </summary>
        private decimal teledQTY = 0m;

        /// <summary>
        /// �����ѹ�
        /// </summary>
        private decimal spedQTY = 0m;

        /// <summary>
        /// �������
        /// </summary>
        private int seeNO = 0;

        /// <summary>
        /// ͣ����
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment stop;
        

        #endregion

        #region ����

        /// <summary>
        /// �Ű���Ϣ
        /// </summary>
        public SchemaTemplet Templet
        {
            get {
                if (this.templet == null)
                {
                    this.templet = new SchemaTemplet();
                }
                return this.templet; }
            set { templet = value; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime SeeDate
        {
            get { return this.seeDate; }
            set { this.seeDate = value; }
        }

        /// <summary>
        /// �ѹ�����
        /// </summary>
        public decimal RegedQTY
        {
            get { return this.regedQTY; }
            set { this.regedQTY = value; }
        }

        /// <summary>
        /// �绰��ԤԼ
        /// </summary>
        public decimal TelingQTY
        {
            get { return this.telingQTY; }
            set { this.telingQTY = value; }
        }

        /// <summary>
        /// ԤԼ�绰��ȡ
        /// </summary>
        public decimal TeledQTY
        {
            get { return this.teledQTY; }
            set { this.teledQTY = value; }
        }

        /// <summary>
        /// �����ѹ�
        /// </summary>
        public decimal SpedQTY
        {
            get { return this.spedQTY; }
            set { this.spedQTY = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public int SeeNO
        {
            get { return this.seeNO; }
            set { this.seeNO = value; }
        }
       
        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public Schema Clone()
        {
            Schema obj = base.MemberwiseClone() as Schema;

            obj.Templet = this.Templet.Clone();            

            return obj;
        }
        #endregion

    }
}
