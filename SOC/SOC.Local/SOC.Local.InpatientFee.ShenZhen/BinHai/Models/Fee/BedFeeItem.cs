using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.SOC.Local.InpatientFee.ShenZhen.BinHai.Models.Fee
{
    [System.Serializable]
    [System.ComponentModel.DisplayName("��λ�̶�����")]
	public class BedFeeItem	: FS.HISFC.Models.Fee.BedFeeItem,ISort,IValidState
	{
		#region ����

        /// <summary>
        /// ʹ������
        /// </summary>
        private string useLimit;
	
		#endregion

		#region ����

        /// <summary>
        /// ʹ������
        /// </summary>
        public string UseLimit
        {
            get
            {
                return this.useLimit;
            }
            set
            {
                this.useLimit = value;
            }
        }

		#endregion

        #region ��¡

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns>���ص�ǰ���ʵ������</returns>
        public new BedFeeItem Clone()
        {
            return base.Clone() as BedFeeItem;
        }

        #endregion
	}
}
