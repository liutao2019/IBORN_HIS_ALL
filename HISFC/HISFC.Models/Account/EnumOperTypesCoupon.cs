using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace FS.HISFC.Models.Account
{
    
    [Serializable]
    public class EnumOperTypesCoupon : FS.HISFC.Models.Base.EnumServiceBase
    {
        public EnumOperTypesCoupon() 
        {
            this.Items[CounponOperTypes.Pay] = "����";
            this.Items[CounponOperTypes.Quit] = "�˷�";
            this.Items[CounponOperTypes.Exc] = "�һ�";
        }

        #region ����
        /// <summary>
        /// �������
        /// </summary>
        CounponOperTypes operTypes;
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
                return operTypes; 
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
    #region ��������ö��
    /// <summary>
    /// ��������1����2�һ�
    /// </summary>
    public enum CounponOperTypes
    {

        /// <summary>
        /// �˷�
        /// </summary>
        Quit = 1,
        /// <summary>
        /// ����
        /// </summary>
        Pay=2,
        /// <summary>
        /// �һ�
        /// </summary>
        Exc=3
    };
    #endregion
}
