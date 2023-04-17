
namespace Neusoft.HISFC.Models.Base
{
	/// <summary>
	/// Controler<br></br>
	/// [��������: ������ʵ��]<br></br>
	/// [�� �� ��: ����ȫ]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class Controler : Neusoft.FrameWork.Models.NeuObject
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Controler()
		{
		}


		#region ����

		/// <summary>
		/// ���Ʋ���ֵ
		/// </summary>
        private string controlerValue;

		/// <summary>
		/// �Բ���ʾ
		/// </summary>
        private bool isVisible; 

		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ
		/// </summary>
		private bool alreadyDisposed = false;

		#endregion

		#region ����

		/// <summary>
		/// ���Ʋ�����ֵ
		/// </summary>
        public string ControlerValue
        {
            get
            {
                return this.controlerValue;
            }
            set
            {
                this.controlerValue = value;
            }
        }

		/// <summary>
		/// �Ƿ���ʾ 0 ��ʾ 1 ����ʾ
		/// </summary>
        public bool VisibleFlag
        {
            get
            {
                return this.isVisible;
            }
            set
            {
                this.isVisible = value;
            }
        }
		#endregion

		#region ����

		#region �ͷ�
		/// <summary>
		/// �ͷ���Դ
		/// </summary>
		/// <param name="isDisposing"></param>
		protected override void Dispose(bool isDisposing)
		{
			if (this.alreadyDisposed)
			{
				return;
			}

			base.Dispose (isDisposing);

			this.alreadyDisposed = true;
		}

		#endregion

		#region ��¡
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns>Controler���ʵ��</returns>
        public new Controler Clone()
        {
            return base.Clone() as Controler;
        }
		#endregion

		#endregion

		

	}
}
