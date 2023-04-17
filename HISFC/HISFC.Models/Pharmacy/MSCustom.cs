using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Pharmacy
{
    /// <summary>
    /// [��������: ҩƷ�Զ����½�ά��]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2007-07]<br></br>
    /// <˵��>
    ///     1��ID�洢��ˮ��
    /// </˵��>
    /// </summary>
    [Serializable]
    public class MSCustom : FS.FrameWork.Models.NeuObject
    {
        #region �����

        /// <summary>
        /// ��������
        /// </summary>
        private FS.HISFC.Models.Base.EnumDepartmentType deptType = FS.HISFC.Models.Base.EnumDepartmentType.P;

        /// <summary>
        /// ��Ŀ��Ϣ
        /// </summary>
        private FS.FrameWork.Models.NeuObject customItem = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��Ŀ���
        /// </summary>
        private int itemOrder;

        /// <summary>
        /// ��Ŀ��������
        /// </summary>
        private FS.HISFC.Models.Base.EnumMSCustomType customType = FS.HISFC.Models.Base.EnumMSCustomType.���;

        /// <summary>
        /// ��������
        /// </summary>
        private string typeItem;

        /// <summary>
        /// ��֧���� 1 ���� 2 ֧��
        /// </summary>
        private FS.HISFC.Models.Base.TransTypes trans = FS.HISFC.Models.Base.TransTypes.Positive;

        /// <summary>
        /// �۸�����
        /// </summary>
        private string priceType;

        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();
        #endregion

        #region ����

        /// <summary>
        /// ��������
        /// </summary>
        public FS.HISFC.Models.Base.EnumDepartmentType DeptType
        {
            get
            {
                return this.deptType;
            }
            set
            {
                this.deptType = value;
            }
        }

        /// <summary>
        /// ��Ŀ��Ϣ
        /// </summary>
        public FS.FrameWork.Models.NeuObject CustomItem
        {
            get
            {
                return this.customItem;
            }
            set
            {
                this.customItem = value;
            }
        }

        /// <summary>
        /// ��Ŀ���
        /// </summary>
        public int ItemOrder
        {
            get
            {
                return this.itemOrder;
            }
            set
            {
                this.itemOrder = value;
            }
        }

        /// <summary>
        /// ��Ŀ��������
        /// </summary>
        public FS.HISFC.Models.Base.EnumMSCustomType CustomType
        {
            get
            {
                return this.customType;
            }
            set
            {
                this.customType = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string TypeItem
        {
            get
            {
                return this.typeItem;
            }
            set
            {
                this.typeItem = value;
            }
        }

        /// <summary>
        /// ��֧���� 1 ���� 2 ֧��
        /// </summary>
        public FS.HISFC.Models.Base.TransTypes Trans
        {
            get
            {
                return this.trans;
            }
            set
            {
                this.trans = value;
            }
        }

        /// <summary>
        /// �۸�����
        /// </summary>
        public string PriceType
        {
            get
            {
                return this.priceType;
            }
            set
            {
                this.priceType = value;
            }
        }

        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get            
            {
                return this.oper;
            }
            set
            {
                this.oper = value;
            }
        }
        #endregion


        #region ����

        public new MSCustom Clone()
        {
            MSCustom ms = base.Clone() as MSCustom;

            ms.customItem = this.customItem.Clone();

            ms.oper = this.oper.Clone();

            return ms;
        }

        #endregion
    }
}
