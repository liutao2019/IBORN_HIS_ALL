using System;

namespace FS.HISFC.Models.Order.Inpatient
{
	/// <summary>
	/// FS.HISFC.Models.Order.InPatient.Order<br></br>
	/// [��������: סԺҽ������ʵ��]<br></br>
	/// [�� �� ��: ���Ʒ�]<br></br>
	/// [����ʱ��: 2006-09-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class Order:FS.HISFC.Models.Order.Order
	{
		public Order()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����

		#region ˽��
		/// <summary>
		/// ҽ�����ͣ����̡��Ƿ��շѡ��Ƿ���Ҫȷ�ϡ�ҩ���Ƿ���ҩ���Ƿ���Ҫ��ӡִ�е���
		/// </summary>
        private Models.Order.OrderType orderType = new OrderType();

        /// <summary>
        /// ֹͣҽ�������
        /// {16EE9720-A7C1-4f07-8262-2F0A1567C78F}
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment dcNurse = new FS.HISFC.Models.Base.OperEnvironment();

		#endregion

		#endregion

		#region ����

		/// <summary>
		/// ҽ�����ͣ����̡��Ƿ��շѡ��Ƿ���Ҫȷ�ϡ�ҩ���Ƿ���ҩ���Ƿ���Ҫ��ӡִ�е���
		/// </summary>
        public Models.Order.OrderType OrderType
		{
			get
			{
				return this.orderType;
			}
			set
			{
				this.orderType = value;
			}
		}

        /// <summary>
        /// ֹͣҽ�������
        /// {16EE9720-A7C1-4f07-8262-2F0A1567C78F}
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment DCNurse
        {
            get
            {
                return this.dcNurse;
            }
            set
            {
                this.dcNurse = value;
            }
        }

		#endregion

		#region ����

		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new Order Clone()
		{
			// TODO:  ��� Order.Clone ʵ��
			Order obj = base.Clone() as Order;
			obj.orderType = this.orderType.Clone();
            #region {16EE9720-A7C1-4f07-8262-2F0A1567C78F}
            obj.DCNurse = this.dcNurse.Clone();

            #endregion

            return obj;
		}

		#endregion

		#endregion
	}
}
