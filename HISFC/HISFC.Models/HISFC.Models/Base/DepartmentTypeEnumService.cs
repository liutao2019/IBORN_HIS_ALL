using System.Collections;


namespace FS.HISFC.Models.Base
{
    /// <summary>
    /// DepartmentTypeEnumService<br></br>
    /// [��������: ��������ö�ٷ���]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-08-28]<br></br>
    /// <�޸ļ�¼
    ///		�޸���='����ΰ'
    ///		�޸�ʱ��='2006��9��1'
    ///		�޸�Ŀ��='ʹ���µ������ʾ����'
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class DepartmentTypeEnumService : EnumServiceBase
    {
        static DepartmentTypeEnumService()
        {
            items[EnumDepartmentType.C] = "����";
            items[EnumDepartmentType.I] = "סԺ";
            items[EnumDepartmentType.F] = "����";
            items[EnumDepartmentType.L] = "����";
            items[EnumDepartmentType.PI] = "ҩ��";
            items[EnumDepartmentType.T] = "�ն�";
            items[EnumDepartmentType.O] = "����";
            items[EnumDepartmentType.D] = "����";
            items[EnumDepartmentType.P] = "ҩ��";
            items[EnumDepartmentType.N] = "��ʿվ";
            items[EnumDepartmentType.OP] = "����";
            items[EnumDepartmentType.U] = "�Զ������";
        }
        EnumDepartmentType enumDepartmentType;
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
                return this.enumDepartmentType;
            }
        }
        protected override System.Enum DefaultItem
        {
            get
            {
                return EnumDepartmentType.U;
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


    /// <summary>
    /// �����������ö��
    /// </summary>
    public enum EnumDepartmentType
    {

        /// <summary>
        /// clinic����
        /// </summary>
        //ParentText("��������"),Display("����")]
        C = 1,
        /// <summary>
        /// InhospitalסԺ
        /// </summary>
        //ParentText("��������"),Display("סԺ")] 
        I = 2,
        /// <summary>
        /// finance����
        /// </summary>
        //ParentText("��������"),Display("����")]
        F = 3,
        /// <summary>
        /// ����(logistics) 
        /// </summary>
        //ParentText("��������"),Display("����")]
        L = 4,
        /// <summary>
        /// pharamacy inventory  ҩ�� 
        /// </summary>
        //ParentText("��������"),Display("ҩ��")]
        PI = 5,
        /// <summary>
        /// terminal�ն�
        /// </summary>
        //ParentText("��������"),Display("�ն�")] 
        T = 6,
        /// <summary>
        /// other����
        /// </summary>
        //ParentText("��������"),Display("����")]
        O = 7,
        /// <summary>
        /// ����department
        /// </summary>
        //ParentText("��������"),Display("����")] 
        D = 8,
        /// <summary>
        /// ҩ��pharmacy
        /// </summary>
        //ParentText("��������"),Display("ҩ��")]
        P = 9,
        /// <summary>
        /// ��ʿվnurse
        /// </summary>
        //ParentText("��������"),Display("��ʿվ")] 
        N = 10,
        /// <summary>
        /// ����operate
        /// </summary>
        //ParentText("��������"),Display("����")]
        OP = 11,
        /// <summary>
        /// �Զ������User Define
        /// </summary>
        U = 12
    }
}
