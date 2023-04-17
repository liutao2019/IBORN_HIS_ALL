using System;

namespace Neusoft.HISFC.Object.Material
{
    /// <summary>
    /// [��������: ���ʴ洢�ֿ�]
    /// [�� �� ��: �]
    /// [����ʱ��: 2007-03-12]
    /// 
    ///	 [�� �� ��:��ά]
    /// [�޸�ʱ��:2007-11-27] 
    /// [�޸�Ŀ��:������ӷ��Ϲ淶]
    ///	 [�޸�����:ʵ�����ֶα��]
    /// </summary>
    public class MaterialStorage : Neusoft.HISFC.Object.Base.Spell
    {
        public MaterialStorage()
        {

        }


        #region ��

        /// <summary>
        /// ���ⵥ��ʼ��
        /// </summary>
        private System.Int32 outStartNO;

        /// <summary>
        /// ��ⵥ��ʼ��
        /// </summary>
        private System.Int32 inStartNO;

        /// <summary>
        /// ���뵥��ʼ��
        /// </summary>
        private System.Int32 planStartNO;

        /// <summary>
        /// ���޹̶��ʲ���־
        /// </summary>
        private bool isWithFix;

        /// <summary>
        /// �Ƿ��ǲֿ�
        /// </summary>
        private bool isStorage;

        /// <summary>
        /// �Ƿ������
        /// </summary>
        private bool isStoreManage;

        /// <summary>
        /// �Ƿ��������
        /// </summary>
        private bool isBatchManage;

        /// <summary>
        /// ��߿������
        /// </summary>
        private System.Int32 maxDays;

        /// <summary>
        /// ��Ϳ������
        /// </summary>
        private System.Int32 minDays;

        /// <summary>
        /// �ο�����
        /// </summary>
        private System.Int32 referenceDays;

        /// <summary>
        /// ����Ա
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment oper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// ���ⵥ��ʼ��
        /// </summary>
        public System.Int32 OutStartNO
        {
            get
            {
                return this.outStartNO;
            }
            set
            {
                this.outStartNO = value;
            }
        }

        /// <summary>
        /// ��ⵥ��ʼ��
        /// </summary>
        public System.Int32 InStartNO
        {
            get
            {
                return this.inStartNO;
            }
            set
            {
                this.inStartNO = value;
            }
        }

        /// <summary>
        /// ���뵥��ʼ��
        /// </summary>
        public System.Int32 PlanStartNO
        {
            get
            {
                return this.planStartNO;
            }
            set
            {
                this.planStartNO = value;
            }
        }

        /// <summary>
        /// ���޹̶��ʲ�
        /// </summary>
        public bool IsWithFix
        {
            get
            {
                return this.isWithFix;
            }
            set
            {
                this.isWithFix = value;
            }
        }

        /// <summary>
        /// �Ƿ��ǲֿ⣬1�ǣ�0����
        /// </summary>
        public bool IsStorage
        {
            get
            {
                return this.isStorage;
            }
            set
            {
                this.isStorage = value;
            }
        }

        /// <summary>
        /// �Ƿ�����棬1�������棬2����������
        /// </summary>
        public bool IsStoreManage
        {
            get
            {
                return this.isStoreManage;
            }
            set
            {
                this.isStoreManage = value;
            }
        }

        /// <summary>
        /// �Ƿ�������Σ�1���������Σ�2������������
        /// </summary>
        public bool IsBatchManage
        {
            get
            {
                return this.isBatchManage;
            }
            set
            {
                this.isBatchManage = value;
            }
        }

        /// <summary>
        /// ��߿������
        /// </summary>
        public System.Int32 MaxDays
        {
            get
            {
                return this.maxDays;
            }
            set
            {
                this.maxDays = value;
            }
        }

        /// <summary>
        /// ��Ϳ������
        /// </summary>
        public System.Int32 MinDays
        {
            get
            {
                return this.minDays;
            }
            set
            {
                this.minDays = value;
            }
        }

        /// <summary>
        /// �ο�����
        /// </summary>
        public System.Int32 ReferenceDays
        {
            get
            {
                return this.referenceDays;
            }
            set
            {
                this.referenceDays = value;
            }
        }

        /// <summary>
        /// ����Ա
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment Oper
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

        #endregion

        #region ����

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns>�ɹ����ؿ�¡���MaterialStorageʵ�� ʧ�ܷ���null</returns>
        public new MaterialStorage Clone()
        {
            MaterialStorage materialStorage = base.Clone() as MaterialStorage;

            materialStorage.oper = this.oper.Clone();

            return materialStorage;
        }

        #endregion
    }
}
