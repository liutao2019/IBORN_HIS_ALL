using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Fee.Item
{
	/// <summary>
	/// Item<br></br>
	/// [��������: �����Ŀ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-15]<br></br>
	/// <�޸ļ�¼ 
	///		�޸���='' 
	///		�޸�ʱ��='yyyy-mm-dd' 
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    /// 
    [System.Serializable]
	public class UndrugComb : Undrug
	{
		#region ����
		
		/// <summary>
		/// ���
		/// </summary>
		private int sortID;

        /// <summary>
        /// �����ĿID,Name
        /// </summary>
        private NeuObject package =null;// new NeuObject();

        /// <summary>
        /// �۸�ӳɣ��Żݣ�����
        /// </summary>
        private decimal itemRate;

        /// <summary>
        /// �۸�ӳɣ��Żݣ�����
        /// </summary>
        public decimal ItemRate
        {
            get
            {
                return itemRate;
            }
            set
            {
                itemRate = value;
            }
        }

		#endregion

        #region ����

        /// <summary>
        /// �����ĿID,Name
        /// </summary>
        public NeuObject Package 
        {
            get 
            {
                if (package == null)
                {
                    package = new NeuObject();
                }
                return this.package;
            }
            set 
            {
                this.package = value;
            }
        }

        #endregion

        #region ����

        #region ��¡

        /// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ����ʵ������</returns>
		public new UndrugComb Clone()
		{
            UndrugComb undrugComb = base.Clone() as UndrugComb;

            undrugComb.Package = this.Package.Clone();

            return undrugComb;
		}

		#endregion

		#endregion
		
		#region �ӿ�ʵ��

		#region ISort ��Ա
		
		/// <summary>
		/// ���
		/// </summary>
		public int SortID
		{
			get
			{
				return this.sortID;
			}
			set
			{
				this.sortID = value;
			}
		}

		#endregion

		#endregion

		#region ���ñ���

		/// <summary>
		/// ϵͳ���  
		/// </summary>
		[Obsolete("����,����SysClass����", true)]
		public string  sysClass; //ϵͳ���  
		
		/// <summary>
		/// ƴ����  
		/// </summary>
		[Obsolete("����,����SpellCode����", true)]
		public new string spellCode ; //  ƴ����  
		/// <summary>
		/// ���
		/// </summary>
		[Obsolete("����,����WBCode����", true)]
		public string wbCode  ; //���
		/// <summary>
		/// ������
		/// </summary>
		[Obsolete("����,����UserCode����", true)]
		public string inputCode  ;//������
		/// <summary>
		/// ִ�п��ұ���
		/// </summary>
		[Obsolete("����,ExecDept����", true)]
		public string deptCode ;//ִ�п��ұ���
		/// <summary>
		/// ˳���
		/// </summary>
		[Obsolete("����,SortID����", true)]
		public int  sortId  ; //˳���
		/// <summary>
		/// ȷ�ϱ�־ 
		/// </summary>
		[Obsolete("����,����IsNeedConfirm����", true)]
		public string confirmFlag ;//ȷ�ϱ�־ 
		/// <summary>
		/// ��չ��־
		/// </summary>
		[Obsolete("����,����SpecialFlag����", true)]
		public string  ExtFlag; //��չ��־
		/// <summary>
		/// ��չ��־1
		/// </summary>
		[Obsolete("����,����SpecialFlag1����", true)]
		public string Ext1Flag ;// ��չ��־1

		/// <summary>
		/// ��ʷ�����(����������뵥ʱʹ��) 
		/// </summary>
		public new string Mark1;//��ʷ�����(����������뵥ʱʹ��) 
		/// <summary>
		/// ���Ҫ��(����������뵥ʱʹ��)  
		/// </summary>
		public new string Mark2;//���Ҫ��(����������뵥ʱʹ��)  
		/// <summary>
		/// ע������(����������뵥ʱʹ��)
		/// </summary>
		public new string Mark3;//ע������(����������뵥ʱʹ��)
		/// <summary>
		/// //������뵥����   
		/// </summary>
		public new string Mark4;
		/// <summary>
		/// �Ƿ���ҪԤԼ
		/// </summary>
		public new string NeedBespeak;

		#endregion
		
	}
}
