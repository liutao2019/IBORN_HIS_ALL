using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.Sanitize
{
    /// <summary>
    /// [��������: ������ù�����]<br></br>
    /// [�� �� ��: shizj]<br></br>
    /// [����ʱ��: 2008-08]<br></br>
    /// </summary>
    public class SanApplyList : Neusoft.NFC.Object.NeuObject
    {
        public SanApplyList()
        {

        }

        #region ����
        /// <summary>
        /// ������ϸ��ˮ��SEQ_SAN_INPUT_CODE
        /// </summary>
        private string listCode = string.Empty;

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        private SanApplyMain sanApplyMain = new SanApplyMain();

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        private HISFC.Object.Material.StoreBase storeBase = new Neusoft.HISFC.Object.Material.StoreBase();

        #endregion

        #region ����
        /// <summary>
        ///  ������ϸ��ˮ��SEQ_SAN_INPUT_CODE
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
        /// ����������Ϣ
        /// </summary>
        public SanApplyMain SanApplyMain
        {
            get
            {
                return this.sanApplyMain;
            }
            set
            {
                this.sanApplyMain = value;
            }
        }

        /// <summary>
        /// ��������Ϣ
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

        #endregion

        #region ����
        /// <summary>
        /// ��¡����ʵ��
        /// </summary>
        /// <returns></returns>
        public new SanApplyList Clone()
        {
            SanApplyList sanApplyList = base.Clone() as SanApplyList;
            sanApplyList.SanApplyMain = SanApplyMain.Clone();
            sanApplyList.StoreBase = StoreBase.Clone();

            return sanApplyList;
        }

        #endregion

    }
}
