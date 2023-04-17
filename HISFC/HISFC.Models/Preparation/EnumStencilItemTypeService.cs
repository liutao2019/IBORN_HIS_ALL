using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.Models.Preparation
{
    /// <summary>
    /// EnumBloodKindEnumService<br></br>
    /// [��������: ģ����Ŀ����ö����]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2008-02]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary> 
    [Serializable]
    public class EnumStencilItemTypeService : Base.EnumServiceBase
    {

        static EnumStencilItemTypeService()
        {
            items[EnumStencilItemType.Person] = "��Ա";
            items[EnumStencilItemType.Dept] = "����";
            items[EnumStencilItemType.Number] = "��ֵ";
            items[EnumStencilItemType.Date] = "ʱ��";
            items[EnumStencilItemType.String] = "�Զ�����Ϣ";
            items[EnumStencilItemType.Extend] = "��չ";
        }

        EnumStencilItemType enumBloodKind;

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
                return this.enumBloodKind;
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

    #region ģ����Ŀ����ö��


    /// <summary>
    /// ģ����Ŀ����ö��
    /// </summary>
    public enum EnumStencilItemType
    {
        /// <summary>
        /// ��Ա
        /// </summary>
        Person = 0,
        /// <summary>
        /// ����
        /// </summary>
        Dept = 1,
        /// <summary>
        /// ��ֵ
        /// </summary>
        Number = 2,
        /// <summary>
        /// ʱ��
        /// </summary>
        Date = 3,
        /// <summary>
        /// �Զ�����Ϣ
        /// </summary>
        String = 4,
        /// <summary>
        /// ��չ
        /// </summary>
        Extend = 5
    }

    #endregion
}
