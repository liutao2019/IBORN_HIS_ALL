using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Base;
using System.Collections;

namespace FS.HISFC.Models.Fee
{
    [System.Serializable]
    public class SuretyTypeEnumService : EnumServiceBase
    {
        public SuretyTypeEnumService()
        {
            this.Items[EnumSuretyType.E] = "��Ա����";
            this.Items[EnumSuretyType.U] = "��λ����";
            this.Items[EnumSuretyType.F] = "���񵣱�";
        }
        #region ����
        private EnumSuretyType enumSuretype;
        /// <summary>
        /// ����ö������
        /// </summary>
        protected static Hashtable items = new Hashtable();
        #endregion

        #region ����
        /// <summary>
        /// ����ö��
        /// </summary>
        protected override Hashtable Items
        {
            get
            {
                return items;
            }
        }
        /// <summary>
        /// ö����Ŀ
        /// </summary>
        protected override System.Enum EnumItem
        {
            get
            {
                return enumSuretype;
            }
        }

        #endregion

        #region ����
        /// <summary>
        /// �õ�ö�ٵ�NeuObject����
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(items)));
        }
        #endregion  
    }

    #region ��������
    /// <summary>
    /// ��������
    /// </summary>
    public enum EnumSuretyType
    {
        /// <summary>
        /// ��Ա����
        /// </summary>
        E = 0,
        /// <summary>
        /// ��λ����
        /// </summary>
        U = 1,
        /// <summary>
        /// ���񵣱�
        /// </summary>
        F = 2
    }
    #endregion
}
