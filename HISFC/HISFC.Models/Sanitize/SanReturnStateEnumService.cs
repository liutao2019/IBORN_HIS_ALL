using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Neusoft.HISFC.Object.Sanitize
{
    /// <summary>
    /// SanReturnStateEnumService<br></br>
    /// [��������: ����״̬����ö�ٷ�����]<br></br>
    /// [�� �� ��: SHIZJ]<br></br>
    /// [����ʱ��: 2008-08]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class SanReturnStateEnumService:Neusoft.HISFC.Object.Base.EmployeeTypeEnumService
    {
        static SanReturnStateEnumService()
        {
            items[QCReturnState.APPLY] = "����";
            items[QCReturnState.APPROVE] = "����ȷ��";
            items[QCReturnState.STERAPPROVE] = "���ȷ��";
            items[QCReturnState.PACKAPPROVE] = "���ȷ��";
            items[QCReturnState.GETAPPROVE] = "��ȡȷ��";
        }

        QCReturnState qcReturnState;

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
                return this.qcReturnState;
            }
        }

        public new static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(items)));
        }
    }

    /// <summary>
    /// ������1����2����ȷ��3���ȷ��4���ȷ��5��ȡȷ��
    /// </summary>
    public enum QCReturnState
    {
        /// <summary>
        /// 1����
        /// </summary>
        APPLY = 1,

        /// <summary>
        /// 2����ȷ��
        /// </summary>
        APPROVE = 2,

        /// <summary>
        /// 3���ȷ��
        /// </summary>
        STERAPPROVE = 3,

        /// <summary>
        /// 4���ȷ��
        /// </summary>
        PACKAPPROVE = 4,

        /// <summary>
        /// 5��ȡȷ��
        /// </summary>
        GETAPPROVE = 5

    }
}
