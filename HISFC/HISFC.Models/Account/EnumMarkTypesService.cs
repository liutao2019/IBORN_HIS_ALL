using System.Collections;
using System;

namespace FS.HISFC.Models.Account
{
    [Serializable]
    public class EnumMarkTypesService : Base.EnumServiceBase
    {
        public EnumMarkTypesService()
        {
            this.Items[MarkTypes.NoneCard] = "�޿�";
            this.Items[MarkTypes.Magcard] = "�ſ�";
            this.Items[MarkTypes.IC] = "IC��";
        }


        #region ����
        /// <summary>
        /// �����
        /// </summary>
        MarkTypes markTypes;
        /// <summary>
        /// �洢ö��
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
                return markTypes; 
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
    #region ������ö��
    /// <summary>
    /// ��ݱ�ʶ����� 1�ſ� 2IC�� 3���Ͽ�
    /// </summary>
    public enum MarkTypes
    {
        /// <summary>
        /// �޿�
        /// </summary>
        NoneCard=0,
        /// <summary>
        /// �ſ�
        /// </summary>
        Magcard = 1,
        /// <summary>
        /// IC��
        /// </summary>
        IC=2

    };
    #endregion

}
