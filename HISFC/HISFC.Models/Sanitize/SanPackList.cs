using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.Sanitize
{
    /// <summary>
    /// [��������: ���������ϸ]<br></br>
    /// [�� �� ��: shizj]<br></br>
    /// [����ʱ��: 2008-08]<br></br>
    /// </summary>
    public class SanPackList : Neusoft.NFC.Object.NeuObject
    {
        public SanPackList()
        {

        }

        #region ����

        /// <summary>
        /// �����ϸ����ˮ��SEQ_SAN_INPUT_CODE
        /// </summary>
        private string listCode = string.Empty;

        /// <summary>
        /// ��������������
        /// </summary>
        private Neusoft.HISFC.Object.Sanitize.SanPackMain sanPackMain = new SanPackMain();

        /// <summary>
        /// ��Ʒ��Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.Material.StoreBase storeBase = new Neusoft.HISFC.Object.Material.StoreBase();

        /// <summary>
        /// ���������
        /// </summary>
        private string outNo = string.Empty;

        #endregion

        #region ����

        /// <summary>
        /// �����ϸ����ˮ��SEQ_SAN_INPUT_CODE
        /// </summary>
        public string ListCode
        {
            get
            {
                return this.listCode;
            }
            set
            {
                this.listCode = value;
            }
        }

        /// <summary>
        /// ��������������
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
        /// ���������
        /// </summary>
        public string OutNo
        {
            get
            {
                return this.outNo;
            }
            set
            {
                this.outNo = value;
            }
        }

        #endregion

        #region ����
        #region Clone

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public new SanPackList Clone()
        {
            SanPackList sanPackList = base.Clone() as SanPackList;

            sanPackList.SanPackMain = SanPackMain.Clone();
            sanPackList.StoreBase = StoreBase.Clone();

            return sanPackList;
        }
        #endregion
        #endregion
    }
}
