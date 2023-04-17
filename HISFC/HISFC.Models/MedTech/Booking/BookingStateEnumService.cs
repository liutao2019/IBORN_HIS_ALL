using FS.HISFC.Models.Base;
using System.Collections;
using FS.FrameWork.Models;
namespace FS.HISFC.Models.MedTech.Booking
{
    /// <summary>
    /// [��������: ÿ���������ն�������״̬ö��]<br></br>
    /// [�� �� ��: ��ѩ��]<br></br>
    /// [����ʱ��: 2006-12-03]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// 
    /// </summary>
	public class BookingStateEnumService: EnumServiceBase
    
    {
        public BookingStateEnumService()

        {
            this.Items[EnumBookingState.Apply] = "Applyҽ��ԤԼ����״̬";
            this.Items[EnumBookingState.Booking] = "Bookingҽ��ԤԼ�Ǽ�״̬";
            this.Items[EnumBookingState.CancelBooking] = "CancelBookinngҽ��ԤԼȡ��״̬";
            this.Items[EnumBookingState.Execute] = "Executeҽ��ԤԼִ��״̬";
            this.Items[EnumBookingState.Invalid] = "Invalidҽ��ԤԼȡ��״̬";			
        }

        #region ����

		/// <summary>
		/// ԤԼ״̬
		/// </summary>
        EnumBookingState enumState;

		/// <summary>
		/// �洢ö�ٶ���
		/// </summary>
        protected static System.Collections.Hashtable items = new System.Collections.Hashtable();

		#endregion

		#region ����

		/// <summary>
		/// ����ö������
		/// </summary>
        protected override System.Collections.Hashtable Items
		{
			get
			{
				return items;
			}
		}
		
		/// <summary>
		/// ö����Ŀ
		/// </summary>
		protected override System.Enum EnumItem
		{
			get
			{
				return this.enumState;
			}
		}

		#endregion
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList(items.Values));
        }
        
		
	}
}
