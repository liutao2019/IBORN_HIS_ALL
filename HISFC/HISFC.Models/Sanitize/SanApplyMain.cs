using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.Sanitize
{
    /// <summary>
    /// [��������: ������û���]<br></br>
    /// [�� �� ��: shizj]<br></br>
    /// [����ʱ��: 2008-08]<br></br>
    /// </summary>
    public class SanApplyMain : Neusoft.NFC.Object.NeuObject, Neusoft.HISFC.Object.Base.IValidState
    {
        public SanApplyMain()
        {

        }

        #region ����
        /// <summary>
        /// ������ˮ��
        /// </summary>
        private string applyCode = string.Empty;

        /// <summary>
        /// �����¼˳���
        /// </summary>
        private decimal sortCode = 0;

        /// <summary>
        /// ���뵥�ݺ�
        /// </summary>
        private string billCode = string.Empty;

        /// <summary>
        /// ������1����2����3��������4��������5�黹
        /// </summary>
        private Neusoft.HISFC.Object.Sanitize.QCApplyState applyState = QCApplyState.APPLY;

        /// <summary>
        /// ��Ʒ��Ϣ
        /// </summary>
        private HISFC.Object.Material.StoreBase storeBase = new Neusoft.HISFC.Object.Material.StoreBase();

        /// <summary>
        /// ������
        /// </summary>
        private decimal useDays = 0;

        /// <summary>
        /// ���ż۸�
        /// </summary>
        private decimal outPrice = 0;

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment applyOper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        /// <summary>
        /// ��������
        /// </summary>
        private decimal applyNum = 0;

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment appoveOper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        /// <summary>
        /// ��������
        /// </summary>
        private decimal appoveNum = 0;

        /// <summary>
        /// �黹����Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment returnOper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        /// <summary>
        /// �黹��������Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment returnAPPOper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        /// <summary>
        /// �黹����
        /// </summary>
        private decimal returnNum = 0;

        /// <summary>
        /// �Ƿ���Ч1��Ч0ͣ��
        /// </summary>
        private Neusoft.HISFC.Object.Base.EnumValidState validState = Neusoft.HISFC.Object.Base.EnumValidState.Valid;

        /// <summary>
        /// ͣ������Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment stopOper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// �黹����
        /// </summary>
        public decimal ReturnNum
        {
            get
            {
                return this.returnNum;
            }
            set
            {
                this.returnNum = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public decimal AppoveNum
        {
            get
            {
                return this.appoveNum;
            }
            set
            {
                this.appoveNum = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public decimal ApplyNum
        {
            get
            {
                return this.applyNum;
            }
            set
            {
                this.applyNum = value;
            }
        }

        /// <summary>
        /// ������ˮ��
        /// </summary>
        public string ApplyCode
        {
            get
            {
                return this.applyCode;
            }
            set
            {
                this.applyCode = value;
            }
        }

        /// <summary>
        /// �����¼˳���
        /// </summary>
        public decimal SortCode
        {
            get
            {
                return this.sortCode;
            }
            set
            {
                this.sortCode = value;
            }
        }

        /// <summary>
        /// ���뵥�ݺ�
        /// </summary>
        public string BillCode
        {
            get
            {
                return this.billCode;
            }
            set
            {
                this.billCode = value;
            }
        }

        /// <summary>
        /// ������1����2����3��������4��������5�黹
        /// </summary>
        public Neusoft.HISFC.Object.Sanitize.QCApplyState ApplyState
        {
            get
            {
                return this.applyState;
            }
            set
            {
                this.applyState = value;
            }
        }

        /// <summary>
        /// ��Ʒ��Ϣ
        /// </summary>
        public HISFC.Object.Material.StoreBase StoreBase
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
        /// ������
        /// </summary>
        public decimal UseDays
        {
            get
            {
                return this.useDays;
            }
            set
            {
                this.useDays = value;
            }
        }

        /// <summary>
        /// ���ż۸�
        /// </summary>
        public decimal OutPrice
        {
            get
            {
                return this.outPrice;
            }
            set
            {
                this.outPrice = value;
            }
        }

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment ApplyOper
        {
            get
            {
                return this.applyOper;
            }
            set
            {
                this.applyOper = value;
            }
        }

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment AppoveOper
        {
            get
            {
                return this.appoveOper;
            }
            set
            {
                this.appoveOper = value;
            }
        }

        /// <summary>
        /// �黹����Ϣ
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment ReturnOper
        {
            get
            {
                return this.returnOper;
            }
            set
            {
                this.returnOper = value;
            }
        }


        /// <summary>
        /// ͣ������Ϣ
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment StopOper
        {
            get
            {
                return this.stopOper;
            }
            set
            {
                this.stopOper = value;
            }
        }

        /// <summary>
        /// �黹��������Ϣ
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment ReturnAPPOper
        {
            get
            {
                return this.returnAPPOper;
            }
            set
            {
                this.returnAPPOper = value;
            }
        }

        #region IValidState ��Ա
        /// <summary>
        /// �Ƿ���Ч1��Ч0ͣ��
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
        #endregion

        #region ����

        /// <summary>
        /// ��¡����ʵ��
        /// </summary>
        /// <returns></returns>
        public new SanApplyMain Clone()
        {
            SanApplyMain sanApplyMain = base.Clone() as SanApplyMain;

            sanApplyMain.StoreBase = StoreBase.Clone();

            sanApplyMain.ApplyOper = ApplyOper.Clone();

            sanApplyMain.AppoveOper = AppoveOper.Clone();

            sanApplyMain.ReturnOper = ReturnOper.Clone();

            sanApplyMain.ReturnAPPOper = ReturnAPPOper.Clone();

            sanApplyMain.StopOper = StopOper.Clone();

            return sanApplyMain;
        }

        #endregion
    }
}
