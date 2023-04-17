using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Pharmacy
{
    /// <summary>
    /// [��������: Э��������Ϣ]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2008-06]<br></br>
    /// <˵��>
    ///     ID PackageID  Name PackageName
    /// </˵��>
    /// </summary>
    [System.Serializable]
    public class Nostrum : FS.FrameWork.Models.NeuObject, FS.HISFC.Models.Base.IValid
    {
        public Nostrum()
        {

        }

        #region �����

        /// <summary>
        /// Э����������Ŀ��Ϣ
        /// </summary>
        private FS.HISFC.Models.Pharmacy.Item item = new Item();

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private decimal qty;

        /// <summary>
        /// ˳���
        /// </summary>
        private int sortNO;

        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        private bool isValid = true;

        /// <summary>
        /// ��������
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// Э����������Ŀ��Ϣ
        /// </summary>
        public FS.HISFC.Models.Pharmacy.Item Item
        {
            get
            {
                return this.item;
            }
            set
            {
                this.item = value;
            }
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public decimal Qty
        {
            get
            {
                return this.qty;
            }
            set
            {
                this.qty = value;
            }
        }

        /// <summary>
        /// ˳���
        /// </summary>
        public int SortNO
        {
            get
            {
                return this.sortNO;
            }
            set
            {
                this.sortNO = value;
            }
        }

        /// <summary>
        /// ��������
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

        #endregion

        #region IValid ��Ա

        /// <summary>
        /// ��Ч��
        /// </summary>
        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
            set
            {
                this.isValid = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new Nostrum Clone()
        {
            Nostrum info = base.Clone() as Nostrum;

            info.item = this.item.Clone();
            info.oper = this.oper.Clone();

            return info;
        }

        #endregion
    }
}
