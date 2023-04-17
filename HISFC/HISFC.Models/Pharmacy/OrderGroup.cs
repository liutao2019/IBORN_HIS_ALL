using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Pharmacy
{
    /// <summary>
    /// [��������: ҽ������������]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2007-08]<br></br>
    /// </summary>
    [Serializable]
    public class OrderGroup : FS.FrameWork.Models.NeuObject
    {
        public OrderGroup()
        {
 
        }

        /// <summary>
        /// ������ҽ����ʼʱ��ָҽ��ִ��ʱ��)
        /// </summary>
        private DateTime beginTime = System.DateTime.MinValue;

        /// <summary>
        /// ������ҽ������ʱ��(ָҽ��ִ��ʱ��)
        /// </summary>
        private DateTime endTime = System.DateTime.MaxValue;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = null;

        /// <summary>
        /// ������ҽ����ʼʱ��ָҽ��ִ��ʱ��)
        /// </summary>
        public DateTime BeginTime
        {
            get
            {
                return this.beginTime;
            }
            set
            {
                this.beginTime = value;
            }
        }

        /// <summary>
        /// ������ҽ������ʱ��(ָҽ��ִ��ʱ��)
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return this.endTime;
            }
            set
            {
                this.endTime = value;
            }
        }

        /// <summary>
        /// ����Ա
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get
            {
                if (this.oper == null)
                {
                    this.oper = new FS.HISFC.Models.Base.OperEnvironment();
                }
                return this.oper;
            }
            set
            {
                this.oper = value;
            }
        }

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public new OrderGroup Clone()
        {
            OrderGroup cloneOrderGroup = base.Clone() as OrderGroup;

            cloneOrderGroup.Oper = this.Oper.Clone();

            return cloneOrderGroup;
        }
    }
}
