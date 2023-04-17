using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Pharmacy
{
    /// <summary>
    /// [��������: ҩƷģ����]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// </summary>
    [Serializable]
    public class DrugStencil : FS.FrameWork.Models.NeuObject
    {
        public DrugStencil()
        {

        }

        #region ����

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ģ������
        /// </summary>
        private Pharmacy.DrugStencilEnumService openType = new DrugStencilEnumService();

        /// <summary>
        /// ģ����Ϣ
        /// </summary>
        private FS.FrameWork.Models.NeuObject stencil = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ҩƷ��Ϣ
        /// </summary>
        private FS.HISFC.Models.Pharmacy.Item item = new Item();

        /// <summary>
        /// ˳���
        /// </summary>
        private int sortNO;

        /// <summary>
        /// ����Ա
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ��չ��Ϣ
        /// </summary>
        private string extend;

        #endregion

        #region ����

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public FS.FrameWork.Models.NeuObject Dept
        {
            get
            {
                return this.dept;
            }
            set
            {
                this.dept = value;
            }
        }

        /// <summary>
        /// ģ������
        /// </summary>
        public DrugStencilEnumService OpenType
        {
            get
            {
                return this.openType;
            }
            set
            {
                this.openType = value;
                if (value.ID != null)
                    base.Memo = value.ID.ToString();
            }
        }

        /// <summary>
        /// ģ����Ϣ
        /// </summary>
        public FS.FrameWork.Models.NeuObject Stencil
        {
            get
            {
                return this.stencil;
            }
            set
            {
                this.stencil = value;
                base.ID = value.ID;
                base.Name = value.Name;
            }
        }
        /// <summary>
        /// ҩƷ��Ϣ
        /// </summary>
        public FS.HISFC.Models.Pharmacy.Item Item
        {
            get
            {
                return this.item;
            }
            set
            {
                this.item = value;
            }
        }

        /// <summary>
        /// ˳���
        /// </summary>
        public int SortNO
        {
            get
            {
                return this.sortNO;
            }
            set
            {
                this.sortNO = value;
            }
        }

        /// <summary>
        /// ����Ա
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

        /// <summary>
        /// ��չ�ֶ�
        /// </summary>
        public string Extend
        {
            get
            {
                return this.extend;
            }
            set
            {
                this.extend = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns>�ɹ����ص�ǰʵ���Ŀ�¡��Ϣ</returns>
        public new DrugStencil Clone()
        {
            DrugStencil drugStencil = base.Clone() as DrugStencil;
            drugStencil.dept = this.dept.Clone();
            drugStencil.stencil = this.stencil.Clone();
            drugStencil.item = this.item.Clone();
            drugStencil.oper = this.oper.Clone();

            return drugStencil;
        }

        #endregion
    }
}
