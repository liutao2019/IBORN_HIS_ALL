using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Neusoft.HISFC.Object.Sanitize
{
    /// <summary>
    /// SanApplyStateEnumService<br></br>
    /// [��������: ����Ǽ�����ö�ٷ�����]<br></br>
    /// [�� �� ��: SHIZJ]<br></br>
    /// [����ʱ��: 2008-08]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class SanApplyStateEnumService:Neusoft.HISFC.Object.Base.EmployeeTypeEnumService
    {
        static SanApplyStateEnumService()
        {
            items[QCApplyState.APPLY] = "����";
            items[QCApplyState.APPROVE] = "����";
            items[QCApplyState.USEAPPLY] = "��������";
            items[QCApplyState.USEAPPROLY] = "��������";
            items[QCApplyState.RETURN] = "�黹";
        }

        QCApplyState qcApplyState;

        protected static Hashtable items = new Hashtable();

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
                return this.qcApplyState;
            }
        }

        public new static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(items)));
        }
    }

    /// <summary>
    /// ������1����2����3��������4��������5�黹
    /// </summary>
    public enum QCApplyState
    {
        /// <summary>
        /// 1����
        /// </summary>
        APPLY=1,

        /// <summary>
        /// 2����
        /// </summary>
        APPROVE=2,

        /// <summary>
        /// 3��������
        /// </summary>
        USEAPPLY=3,

        /// <summary>
        /// 4��������
        /// </summary>
        USEAPPROLY=4,

        /// <summary>
        /// 5�黹
        /// </summary>
        RETURN=5

    }
}
