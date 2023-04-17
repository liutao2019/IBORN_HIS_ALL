using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Pharmacy
{
    /// <summary>
    /// [��������: ҩƷ�ض�λ����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-11]<br></br>
    /// <˵��>
    ///     1��  ID ȡҩҩ�� Name ȡҩҩ������
    /// </˵��>
    /// </summary>
    [Serializable]
    public class DrugSpeLocation : FS.FrameWork.Models.NeuObject
    {
        public DrugSpeLocation()
        {

        }

        #region �����

        /// <summary>
        /// �ض�ҩƷ
        /// </summary>
        private FS.HISFC.Models.Pharmacy.Item item = new Item();

        /// <summary>
        /// ȡҩ����
        /// </summary>
        private FS.FrameWork.Models.NeuObject roomDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����Ա
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// �ض�ҩƷ
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
        /// ȡҩ����
        /// </summary>
        public FS.FrameWork.Models.NeuObject RoomDept
        {
            get
            {
                return this.roomDept;
            }
            set
            {
                this.roomDept = value;
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

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new DrugSpeLocation Clone()
        {
            DrugSpeLocation dr = base.Clone() as DrugSpeLocation;

            dr.item = this.item.Clone();
            dr.roomDept = this.roomDept.Clone();
            dr.oper = this.oper.Clone();

            return dr;
        }

        #endregion
    }
}
