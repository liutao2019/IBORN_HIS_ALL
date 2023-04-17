using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Pharmacy
{
    /// <summary>
    /// [��������: ����ҩƷ��]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-04]<br></br>
    /// </summary>
    [Serializable]
    public class DrugSpecial : FS.FrameWork.Models.NeuObject
    {
        public DrugSpecial()
        {

        }

        #region ����

        /// <summary>
        /// ������Ŀ����
        /// </summary>
        private EnumDrugSpecialType speType = EnumDrugSpecialType.Dept;
      
        /// <summary>
        /// ������Ŀ��Ϣ
        /// </summary>
        private FS.FrameWork.Models.NeuObject speItem = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ҩƷ��Ϣ
        /// </summary>
        private FS.HISFC.Models.Pharmacy.Item item = new Item();
      
        /// <summary>
        /// ������Ա
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();
       
        /// <summary>
        /// ��չ�ֶ�
        /// </summary>
        private string extend;
        #endregion

        #region ����

        /// <summary>
        /// ������Ŀ����
        /// </summary>
        public EnumDrugSpecialType SpeType
        {
            get
            {
                return speType;
            }
            set
            {
                speType = value;
            }
        }

        /// <summary>
        ///������Ŀ��Ϣ
        /// </summary>
        public FS.FrameWork.Models.NeuObject SpeItem
        {
            get
            {
                return this.speItem;
            }
            set
            {
                this.speItem = value;
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
                return item;
            }
            set
            {
                item = value;
            }
        }
        
        /// <summary>
        /// ������Ա
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get
            {
                return oper;
            }
            set
            {
                oper = value;
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
        /// ��¡����ʵ��
        /// </summary>
        /// <returns></returns>
        public new DrugSpecial Clone()
        {
            DrugSpecial drugSpe = base.Clone() as DrugSpecial;

            drugSpe.speItem = speItem.Clone();

            drugSpe.item = item.Clone();

            drugSpe.oper = oper.Clone();

            return drugSpe;
        }

        #endregion
    }

    /// <summary>
    /// ������Ŀ����
    /// </summary>
    public enum EnumDrugSpecialType
    {
        /// <summary>
        /// ����
        /// </summary>
        Dept = 0,
        /// <summary>
        /// ��Ա
        /// </summary>
        Doc = 1
    }
}
