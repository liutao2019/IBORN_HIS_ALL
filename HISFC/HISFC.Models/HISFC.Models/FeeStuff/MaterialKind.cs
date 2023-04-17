using System;

namespace FS.HISFC.Models.FeeStuff
{
    /// <summary>
    /// [��������: ���ʿ�Ŀ]
    /// [�� �� ��: �]
    /// [����ʱ��: 2007-03-10]
    /// </summary>
    [Serializable]
    public class MaterialKind : FS.HISFC.Models.Base.Item
    {
        public MaterialKind()
        {
        }

        #region ��

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private string kgrade;

        /// <summary>
        /// �ϼ�����
        /// </summary>
        private string superKind;

        /// <summary>
        /// ��ĩ����ʶ
        /// </summary>
        private bool endGrade;

        /// <summary>
        /// ��Ҫ��Ƭ
        /// </summary>
        private bool isCardNeed;

        /// <summary>
        /// ���ι���
        /// </summary>
        private bool isBatch;

        /// <summary>
        /// ��Ч�ڹ���
        /// </summary>
        private bool isValidcon;

        /// <summary>
        /// �����Ŀ����
        /// </summary>
        private FS.FrameWork.Models.NeuObject accountCode = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �����Ŀ����
        /// </summary>
        private FS.FrameWork.Models.NeuObject accountName = new FS.FrameWork.Models.NeuObject();

        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// Ԥ�Ʋ�ֵ��
        /// </summary>
        private decimal leftRate;

        /// <summary>
        /// �Ƿ�̶��ʲ�
        /// </summary>
        private bool isFixedAssets;

        /// <summary>
        /// �������
        /// </summary>
        private System.Int32 orderNo;

        /// <summary>
        /// ��Ӧ�ɱ�������Ŀ���
        /// </summary>
        private string statCode;

        /// <summary>
        /// �Ƿ�Ӽ�����
        /// </summary>
        private bool isAddFlag;
        #endregion

        #region ����

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string Kgrade
        {
            get
            {
                return this.kgrade;
            }
            set
            {
                this.kgrade = value;
            }
        }

        /// <summary>
        /// �ϼ�����
        /// </summary>
        public string SuperKind
        {
            get
            {
                return this.superKind;
            }
            set
            {
                this.superKind = value;
            }
        }

        /// <summary>
        /// ��ĩ����ʶ
        /// </summary>
        public bool EndGrade
        {
            get
            {
                return this.endGrade;
            }
            set
            {
                this.endGrade = value;
            }
        }

        /// <summary>
        /// ��Ҫ��Ƭ
        /// </summary>
        public bool IsCardNeed
        {
            get
            {
                return this.isCardNeed;
            }
            set
            {
                this.isCardNeed = value;
            }
        }

        /// <summary>
        /// ���ι���
        /// </summary>
        public bool IsBatch
        {
            get
            {
                return this.isBatch;
            }
            set
            {
                this.isBatch = value;
            }
        }

        /// <summary>
        /// ��Ч�ڹ���
        /// </summary>
        public bool IsValidcon
        {
            get
            {
                return this.isValidcon;
            }
            set
            {
                this.isValidcon = value;
            }
        }

        /// <summary>
        /// �����Ŀ����
        /// </summary>
        public FS.FrameWork.Models.NeuObject AccountCode
        {
            get
            {
                return this.accountCode;
            }
            set
            {
                this.accountCode = value;
            }
        }

        /// <summary>
        /// �����Ŀ����
        /// </summary>
        public FS.FrameWork.Models.NeuObject AccountName
        {
            get
            {
                return this.accountName;
            }
            set
            {
                this.accountName = value;
            }
        }

        /// <summary>
        /// ����Ա
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get
            {
                return this.oper;
            }
            set
            {
                this.oper = value;
            }
        }

        /// <summary>
        /// Ԥ�Ʋ�ֵ��
        /// </summary>
        public decimal LeftRate
        {
            get
            {
                return this.leftRate;
            }
            set
            {
                this.leftRate = value;
            }
        }

        /// <summary>
        /// �Ƿ�̶��ʲ�
        /// </summary>
        public bool IsFixedAssets
        {
            get
            {
                return this.isFixedAssets;
            }
            set
            {
                this.isFixedAssets = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public System.Int32 OrderNo
        {
            get
            {
                return this.orderNo;
            }
            set
            {
                this.orderNo = value;
            }
        }

        /// <summary>
        /// ��Ӧ�ɱ�������Ŀ���
        /// </summary>
        public string StatCode
        {
            get
            {
                return this.statCode;
            }
            set
            {
                this.statCode = value;
            }
        }

        /// <summary>
        /// �Ƿ�Ӽ�����
        /// </summary>
        public bool IsAddFlag
        {
            get
            {
                return this.isAddFlag;
            }
            set
            {
                this.isAddFlag = value;
            }
        }
        #endregion

        #region ����

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns>�ɹ����ؿ�¡���MaterialKindʵ�� ʧ�ܷ���null</returns>
        public new MaterialKind Clone()
        {
            MaterialKind materialKind = base.Clone() as MaterialKind;

            materialKind.AccountCode = this.AccountCode.Clone();

            materialKind.AccountName = this.AccountName.Clone();

            materialKind.Oper = this.Oper.Clone();

            return materialKind;
        }
        #endregion

    }
}
