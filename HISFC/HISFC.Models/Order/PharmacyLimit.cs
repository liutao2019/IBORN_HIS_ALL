using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Order
{
    /// <summary>
    /// FS.HISFC.Models.Order.PharmacyLimit<br></br>
    /// [��������: ҽ��ҩƷ����ʵ��]<br></br>
    /// [�� �� ��: Sunm]<br></br>
    /// [����ʱ��: 2007-08-28]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class PharmacyLimit : FS.FrameWork.Models.NeuObject
    {
        public PharmacyLimit()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����

        /// <summary>
        /// �Ƿ���Ҫ�ϼ�ҽ�����
        /// </summary>
        private bool isLeaderCheck;

        /// <summary>
        /// �Ƿ���Ҫ�ֹ�����
        /// </summary>
        private bool isNeedRecipe;

        /// <summary>
        /// ��Ч��
        /// </summary>
        private bool isValid;

        /// <summary>
        /// ��ע
        /// </summary>
        private string remark;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ���Ҫ�ϼ�ҽ�����
        /// </summary>
        public bool IsLeaderCheck
        {
            get { return this.isLeaderCheck; }
            set { this.isLeaderCheck = value; }
        }

        /// <summary>
        /// �Ƿ���Ҫ�ֹ�����
        /// </summary>
        public bool IsNeedRecipe
        {
            get { return this.isNeedRecipe; }
            set { this.isNeedRecipe = value; }
        }

        /// <summary>
        /// ��Ч��
        /// </summary>
        public bool IsValid
        {
            get { return this.isValid; }
            set { this.isValid = value; }
        }

        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get { return this.oper; }
            set { this.oper = value; }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new PharmacyLimit Clone()
        {
            // TODO:  ��� CurePhase.Clone ʵ��
            PharmacyLimit obj = base.Clone() as PharmacyLimit;

            obj.oper = this.oper.Clone();

            return obj;
        }

        #endregion

    }
}
