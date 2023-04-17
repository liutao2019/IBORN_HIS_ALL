using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Order
{
    /// <summary>
    /// [��������: ��Һ��Ϣ�� ҽ���ۺ�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-04]<br></br>   
    /// </summary>
    [Serializable]
    public class Compound : FS.FrameWork.Models.NeuObject
    {
        public Compound()
        {

        }

        #region �����

        /// <summary>
        /// �Ƿ���Ҫ��Һ
        /// </summary>
        private bool isNeedCompound = false;

        /// <summary>
        /// �Ƿ���ִ��
        /// </summary>
        private bool isExec = false;

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment compoundOper = new FS.HISFC.Models.Base.OperEnvironment();
        
        #endregion

        /// <summary>
        /// �Ƿ���Ҫ��Һ
        /// </summary>
        public bool IsNeedCompound
        {
            get
            {
                return isNeedCompound;
            }
            set
            {
                isNeedCompound = value;
            }
        }

        /// <summary>
        /// �Ƿ���ִ��
        /// </summary>
        public bool IsExec
        {
            get
            {
                return isExec;
            }
            set
            {
                isExec = value;
            }
        }

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment CompoundOper
        {
            get
            {
                return compoundOper;
            }
            set
            {
                compoundOper = value;
            }
        }

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns>�������¡ʵ��</returns>
        public new Compound Clone()
        {
            Compound compound = base.Clone() as Compound;

            compound.compoundOper = this.compoundOper.Clone();

            return compound;
        }
      

    }
}
