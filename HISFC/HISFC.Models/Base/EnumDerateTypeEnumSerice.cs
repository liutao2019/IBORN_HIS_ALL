using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
 

namespace Neusoft.HISFC.Object.Base
{
	/// <summary>
	/// EnumBloodKindEnumService<br></br>
	/// [��������: �������ͷ�����]<br></br>
	/// [�� �� ��: ������]<br></br>
	/// [����ʱ��: 2008-07-28]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
    /// </summary> 
    [System.Serializable]
    public class EnumDerateTypeEnumSerice : EnumServiceBase
    {

        static EnumDerateTypeEnumSerice()
        {
            items[EnumDerateTypeResult.Balance] = "�������";
            items[EnumDerateTypeResult.OWEDerate] = "Ƿ�Ѽ���";

        }
        EnumDerateTypeResult enumTestResult;
        #region ����

        /// <summary>
        /// ����ö������
        /// </summary>
        protected static Hashtable items = new Hashtable();



        #endregion

        #region ����


        /// <summary>
        /// ����ö������
        /// </summary>
        protected override Hashtable Items
        {
            get
            {
                return items;
            }
        }

        protected override System.Enum EnumItem
        {
            get
            {
                return this.enumTestResult;
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(items)));
        }
    }

    #region 


    /// <summary>
    /// ��������

    /// </summary>
    [System.Serializable]
    public enum EnumDerateTypeResult
    {
        /// <summary>
        /// �������

        /// </summary>
        Balance = 0,
        /// <summary>
        /// Ƿ�Ѽ���

        /// </summary>
        OWEDerate = 1,
    }

    #endregion
}
