using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.Models.Account
{
    [Serializable]
    public class EnumMarkOperateTypesService : FS.HISFC.Models.Base.EnumServiceBase
    {

        public EnumMarkOperateTypesService()
        {
            this.Items[MarkOperateTypes.Begin] = "��ʼʹ��";
            this.Items[MarkOperateTypes.Stop] = "ֹͣʹ��";
            this.Items[MarkOperateTypes.Cancel] = "ȡ��ʹ��";
        }

        #region ����
        /// <summary>
        /// �����
        /// </summary>
        MarkOperateTypes markOperateTypes;
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
                return markOperateTypes; 
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
    /// ������ö��
    /// </summary>
    public enum MarkOperateTypes
    {
        /// <summary>
        /// ��ʼʹ��
        /// </summary>
        Begin=0,
        /// <summary>
        /// ֹͣʹ��
        /// </summary>
        Stop = 1,
        /// <summary>
        /// ȡ��ʹ��
        /// </summary>
        Cancel=2

    };
    #endregion
}
