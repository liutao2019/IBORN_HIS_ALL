using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.Sanitize
{
    /// <summary>
    /// [��������: ���ά����]<br></br>
    /// [�� �� ��: shizj]<br></br>
    /// [����ʱ��: 2008-08]<br></br>
    /// </summary>
    /// 
    public class SanPackBase : Neusoft.NFC.Object.NeuObject
    {
        public SanPackBase()
        {

        }

        #region ����
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        private Neusoft.HISFC.Object.Material.MaterialItem thingInfo = new Neusoft.HISFC.Object.Material.MaterialItem();

        /// <summary>
        /// ��Ʒ��ϸ��
        /// </summary>
        private Neusoft.HISFC.Object.Material.MaterialItem itemCode = new Neusoft.HISFC.Object.Material.MaterialItem();

        /// <summary>
        /// ����
        /// </summary>
        private decimal itemNum = 0;

        /// <summary>
        ///������Ա
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment oper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        #endregion

        #region ����
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public Neusoft.HISFC.Object.Material.MaterialItem ThingInfo
        {
            get
            {
                return this.thingInfo;
            }
            set
            {
                this.thingInfo = value;
            }
        }

        /// <summary>
        /// ��Ʒ��ϸ��
        /// </summary>
        public Neusoft.HISFC.Object.Material.MaterialItem ItemCode
        {
            get
            {
                return this.itemCode;
            }
            set
            {
                this.itemCode = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public decimal ItemNum
        {
            get
            {
                return this.itemNum;
            }
            set
            {
                this.itemNum = value;
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

        #region ��¡

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public new SanPackBase Clone()
        {
            SanPackBase sanPackBase = base.Clone() as SanPackBase;

            sanPackBase.ThingInfo = this.ThingInfo.Clone();
            sanPackBase.ItemCode = this.ItemCode.Clone();
            sanPackBase.Oper = this.Oper.Clone();

            return sanPackBase;
        }

        #endregion

        #endregion
    }
}
