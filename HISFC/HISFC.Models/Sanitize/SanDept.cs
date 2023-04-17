using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.Sanitize
{
    /// <summary>
    /// [��������: �ٴ�������Ʒ������]<br></br>
    /// [�� �� ��: shizj]<br></br>
    /// [����ʱ��: 2008-08]<br></br>
    /// </summary>
    public class SanDept : Neusoft.NFC.Object.NeuObject ,Neusoft.HISFC.Object.Base.IValidState
    {
        public SanDept()
        {

        }

        #region ����

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.Material.StoreBase storeBase = new Neusoft.HISFC.Object.Material.StoreBase();

        /// <summary>
        /// ����
        /// </summary>
        private decimal baseNum = 0;

        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        private Neusoft.HISFC.Object.Base.EnumValidState validState = Neusoft.HISFC.Object.Base.EnumValidState.Valid;

        /// <summary>
        /// ��;�����
        /// </summary>
        private decimal lowNum = 0;

        /// <summary>
        /// ������Ա
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment oper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        ///��Ʒ��Ϣ
        /// </summary>
        public Neusoft.HISFC.Object.Material.StoreBase StoreBase
        {
            get
            {
                return this.storeBase;
            }
            set
            {
                this.storeBase = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public decimal BaseNum
        {
            get
            {
                return this.baseNum;
            }
            set
            {
                this.baseNum = value;
            }
        }

        #region IValidState ��Ա

        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        public Neusoft.HISFC.Object.Base.EnumValidState ValidState
        {
            get
            {
                return this.validState;
            }
            set
            {
                this.validState = value;
            }
        }

        #endregion

        /// <summary>
        /// ��;�����
        /// </summary>
        public decimal LowNum
        {
            get
            {
                return this.lowNum;
            }
            set
            {
                this.lowNum = value;
            }
        }

        /// <summary>
        /// ������Ա
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
        /// ��¡����ʵ��
        /// </summary>
        /// <returns></returns>
        public new SanDept Clone()
        {
            SanDept sanDept = base.Clone() as SanDept;

            sanDept.StoreBase = StoreBase.Clone();

            sanDept.Oper = Oper.Clone();

            return sanDept;
        }

        #endregion

    }
}
