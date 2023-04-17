using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.Sanitize
{
    /// <summary>
    /// [��������: ������¼]<br></br>
    /// [�� �� ��: shizj]<br></br>
    /// [����ʱ��: 2008-08]<br></br>
    /// </summary>
    public class SanSter :Neusoft.NFC.Object.NeuObject
    {
        public SanSter()
        {

        }

        #region ����

        /// <summary>
        /// ������¼��ˮ��
        /// </summary>
        private string sterCode = string.Empty;

        /// <summary>
        /// �������ݺ�
        /// </summary>
        private string billCode = string.Empty;

        /// <summary>
        /// ����˳���
        /// </summary>
        private string sortCode = string.Empty;

        /// <summary>
        /// ��Ʒ������
        /// </summary>
        private Neusoft.HISFC.Object.Sanitize.SanReturnMain sanReturnMain = new SanReturnMain();

        /// <summary>
        /// ���
        /// </summary>
        private Neusoft.HISFC.Object.Sanitize.SanPackMain sanPackMain = new SanPackMain();

        /// <summary>
        /// ����
        /// </summary>
        private Neusoft.HISFC.Object.Base.Department dept = new Neusoft.HISFC.Object.Base.Department();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.Material.MaterialItem matItem = new Neusoft.HISFC.Object.Material.MaterialItem();

        /// <summary>
        /// �Ƿ���(1��0��)
        /// </summary>
        private bool packFlag = false;

        /// <summary>
        /// ������
        /// </summary>
        private decimal sterCost = 0;

        /// <summary>
        ///  ����
        /// </summary>
        private decimal sterNum = 0;

        /// <summary>
        /// ����
        /// </summary>
        private decimal inPrice = 0;

        /// <summary>
        /// ���ż۸�
        /// </summary>
        private decimal outPrice = 0;

        /// <summary>
        /// ����Ա
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment oper = new Neusoft.HISFC.Object.Base.OperEnvironment(); 
        #endregion

        #region ����

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

        /// <summary>
        /// ����
        /// </summary>
        public decimal SterNum
        {
            get
            {
                return this.sterNum;
            }
            set
            {
                this.sterNum = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public decimal SterCost
        {
            get
            {
                return this.sterCost;
            }
            set
            {
                this.sterCost = value;
            }
        }

        /// <summary>
        /// �Ƿ���(1��0��)
        /// </summary>
        public bool PackFlag
        {
            get
            {
                return this.packFlag;
            }
            set
            {
                this.packFlag = value;
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public Neusoft.HISFC.Object.Material.MaterialItem MatItem
        {
            get
            {
                return this.matItem;
            }
            set
            {
                this.matItem = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public Neusoft.HISFC.Object.Base.Department Dept
        {
            get
            {
                return this.dept;
            }
            set
            {
                this.dept = value;
            }
        }

        /// <summary>
        /// ���
        /// </summary>
        public Neusoft.HISFC.Object.Sanitize.SanPackMain SanPackMain
        {
            get
            {
                return this.sanPackMain;
            }
            set
            {
                this.sanPackMain = value;
            }
        }

        /// <summary>
        /// ��Ʒ������
        /// </summary>
        public Neusoft.HISFC.Object.Sanitize.SanReturnMain SanReturnMain
        {
            get
            {
                return this.sanReturnMain;
            }
            set
            {
                this.sanReturnMain = value;
            }
        }

        /// <summary>
        /// ����˳���
        /// </summary>
        public string SortCode
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
        /// �������ݺ�
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
        /// ������¼��ˮ��
        /// </summary>
        public string SterCode
        {
            get
            {
                return this.sterCode;
            }
            set
            {
                this.sterCode = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public decimal InPrice
        {
            get
            {
                return this.inPrice;
            }
            set
            {
                this.inPrice = value;
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

        #endregion

        #region ����

        #region ��¡

        public new SanSter Colne()
        {
            SanSter sanSter = base.Clone() as SanSter;
            sanSter.SanReturnMain = new SanReturnMain();
            sanSter.SanPackMain = new SanPackMain();
            sanSter.Dept = new Neusoft.HISFC.Object.Base.Department();
            sanSter.MatItem = new Neusoft.HISFC.Object.Material.MaterialItem();
            sanSter.Oper = new Neusoft.HISFC.Object.Base.OperEnvironment();
            return sanSter;
        }

        #endregion 
        #endregion
    }
}
