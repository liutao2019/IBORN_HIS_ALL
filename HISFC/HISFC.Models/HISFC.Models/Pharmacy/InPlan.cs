using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Pharmacy
{
    /// <summary>
    /// [��������: ���ƻ���]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-02-03]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    ///  ID ���ƻ���ˮ��
    /// </summary>
    [Serializable]
    public class InPlan : Base.PlanBase
    {
        public InPlan() 
		{

		}

		#region ����

		/// <summary>
		/// �ɹ�����
		/// </summary>
		private System.String myPlanType ;

        /// <summary>
        /// �ɹ�����ˮ��
        /// </summary>
        private string stockNO;

        /// <summary>
        ///  �ɹ�����
        /// </summary>
        private string stockType;

        /// <summary>
        /// ���ϡ�����ƻ�����ˮ�� ����ʱ�� '|' �ָ� �����ϼƻ��� �洢�ºϲ��ƻ�����ˮ�� ���ºϲ��ƻ��� �洢ԭ�ƻ�����ˮ��
        /// </summary>
        private string replacePlanNO;

		#endregion

        /// <summary>
        /// �Ƿ���������ɹ���¼
        /// </summary>
        public bool IsMultiStockRecord
        {
            get
            {
                if (this.stockNO.IndexOf("|") != -1)
                    return true;
                else
                    return false;
            }
        }

		/// <summary>
		/// �ɹ�����0�ֹ��ƻ���1�����ߣ�2���ģ�3ʱ�䣬4������
		/// </summary>
		public System.String PlanType 
		{
			get
			{
				return this.myPlanType; 
			}
			set
			{
				this.myPlanType = value; 
			}
		}

        /// <summary>
        /// �ɹ����� 0 ���� 1 ���� 2 ��
        /// </summary>
        public string StockType
        {
            get
            {
                return this.stockType;
            }
            set
            {
                this.stockType = value;
            }
        }

        /// <summary>
        /// �ɹ���ˮ�� �����ɹ���¼ʱ�� '|' �ָ�
        /// </summary>
        public string StockNO
        {
            get
            {
                return this.stockNO;
            }
            set
            {
                this.stockNO = value;
            }
        }

        /// <summary>
        /// ���ϡ�����ƻ�����ˮ�� ����ʱ�� '|' �ָ� �����ϼƻ��� �洢�ºϲ��ƻ�����ˮ�� ���ºϲ��ƻ��� �洢ԭ�ƻ�����ˮ��
        /// </summary>
        public string ReplacePlanNO
        {
            get
            {
                return this.replacePlanNO;
            }
            set
            {
                this.replacePlanNO = value;
            }
        }

		#region ����

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>�ɹ����ص�ǰʵ���Ŀ�¡ʵ��</returns>
		public new InPlan Clone()
		{
            InPlan inPlan = base.Clone() as InPlan;

			return inPlan;
		}


		#endregion
    }
}
