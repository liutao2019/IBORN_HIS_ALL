using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.Sanitize
{
    /// <summary>
    /// [��������: �������Base]<br></br>
    /// [�� �� ��: shizj]<br></br>
    /// [����ʱ��: 2008-08]<br></br>
    /// </summary>
    public class SanPackMain : Neusoft.NFC.Object.NeuObject
    {
        public SanPackMain()
        {

        }

        #region ����

        /// <summary>
        /// ��������ˮ��
        /// </summary>
        private string packCode = string.Empty;

        /// <summary>
        /// �������(������+4λ����)
        /// </summary>
        private string batchNo = string.Empty;

        /// <summary>
        /// ˳���
        /// </summary>
        private int sortNum = 0;

        /// <summary>
        /// ���ݺ�(Ĭ��������+4λ����)
        /// </summary>
        private string billCode = string.Empty;

        /// <summary>
        /// ��Ʒ��Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.Material.StoreBase storeBase = new Neusoft.HISFC.Object.Material.StoreBase();

        /// <summary>
        /// �������
        /// </summary>
        private decimal inNum = 0;

        /// <summary>
        /// ������
        /// </summary>
        private string inDept = string.Empty;

        /// <summary>
        /// ��Ч����(��ֹ����)
        /// </summary>
        private System.DateTime validDate;

        /// <summary>
        /// �Ƿ��������1��0��
        /// </summary>
        private bool sterFlag = false;

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment oper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        /// <summary>
        /// �Ƿ���
        /// </summary>
        private bool unPackFlag = false;

        /// <summary>
        /// ��������
        /// </summary>
        private string inNo = string.Empty;

        /// <summary>
        /// �������Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment unPackOper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// ��������ˮ��
        /// </summary>
        public string PackCode
        {
            get
            {
                return this.packCode;
            }
            set
            {
                this.packCode = value;
            }
        }

        /// <summary>
        /// �������(������+4λ����)
        /// </summary>
        public string BatchNo
        {
            get
            {
                return this.batchNo;
            }
            set
            {
                this.batchNo = value;
            }
        }

        /// <summary>
        /// ˳���
        /// </summary>
        public int SortNum
        {
            get
            {
                return this.sortNum;
            }
            set
            {
                this.sortNum = value;
            }
        }

        /// <summary>
        /// ���ݺ�(Ĭ��������+4λ����)
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
        /// ��Ʒ��Ϣ
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
        /// �������
        /// </summary>
        public decimal InNum
        {
            get
            {
                return this.inNum;
            }
            set
            {
                this.inNum = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string InDept
        {
            get
            {
                return this.inDept;
            }
            set
            {
                this.inDept = value;
            }
        }

        /// <summary>
        /// ��Ч����(��ֹ����)
        /// </summary>
        public System.DateTime ValidDate
        {
            get
            {
                return this.validDate;
            }
            set
            {
                this.validDate = value;
            }
        }

        /// <summary>
        /// �Ƿ��������1��0��
        /// </summary>
        public bool SterFlag
        {
            get
            {
                return this.sterFlag;
            }
            set
            {
                this.sterFlag = value;
            }
        }

        /// <summary>
        /// ��������Ϣ
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

        /// <summary>
        /// �Ƿ���
        /// </summary>
        public bool UnPackFlag
        {
            get
            {
                return this.unPackFlag;
            }
            set
            {
                this.unPackFlag = value;
            }
        }

        /// <summary>
        /// �������Ϣ
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment UnPackOper
        {
            get
            {
                return this.unPackOper;
            }
            set
            {
                this.unPackOper = value;
            }
        }


        /// <summary>
        /// ��������
        /// </summary>
        public string InNo
        {
            get
            {
                return this.inNo;
            }
            set
            {
                this.inNo = value;
            }
        }

        #endregion

        #region ����
        #region ��¡
        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public new SanPackMain Clone()
        {
            SanPackMain sanPackMain = base.Clone() as SanPackMain;

            sanPackMain.StoreBase = StoreBase.Clone();
            sanPackMain.Oper = Oper.Clone();

            return sanPackMain;
        }
        #endregion
        #endregion

    }
}
