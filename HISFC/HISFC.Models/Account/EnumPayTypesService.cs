using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace FS.HISFC.Models.Account
{
    
    [Serializable]
    public class EnumPayTypesService : FS.HISFC.Models.Base.EnumServiceBase
    {
        public EnumPayTypesService() 
        {
            this.Items[PayWayTypes.P] = "P";
            this.Items[PayWayTypes.R] = "R";
            this.Items[PayWayTypes.C] = "C";
            this.Items[PayWayTypes.I] = "I";
            this.Items[PayWayTypes.M] = "M";
            this.Items[PayWayTypes.Y] = "Y";
        }

        #region ����
        /// <summary>
        /// ��������
        /// </summary>
        PayWayTypes payWayTypes;
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
                return payWayTypes; 
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
    /// �������ͣ�P�����ײͣ�R����Һţ�C�������ѣ�IסԺ���㣻M�ײ�Ѻ��
    /// </summary>
    public enum PayWayTypes
    {
        /// <summary>
        /// �����ײ�
        /// </summary>
        P = 0,
        /// <summary>
        /// ����Һ�
        /// </summary>
        R= 1,
        /// <summary>
        /// ��������
        /// </summary>
        C=2,
        /// <summary>
        /// סԺ����
        /// </summary>
        I = 3,
        
        /// <summary>
        /// �ײ�Ѻ��
        /// </summary>
        M = 4,

        /// <summary>
        /// ҽ������{1C42FA6C-C70A-4cd4-82C4-9FA1FCABD73B}
        /// </summary>
        Y = 5,

    };
    #endregion
}
