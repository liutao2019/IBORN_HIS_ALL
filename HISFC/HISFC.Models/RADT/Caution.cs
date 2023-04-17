using System;
using FS.HISFC.Models.Base;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.RADT
{
	/// <summary>
	/// [��������: ����ʵ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���='����ΰ'
	///		�޸�ʱ��='2006-9-12'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary> 
    [Serializable]
	public class Caution :NeuObject 
	{

		/// <summary>
		/// ���캯��
		/// </summary>
		public Caution()
		{
		}

        #region ����

		/// <summary>
		/// �������
		/// </summary>
		private decimal money = 0M;

		
		
		/// <summary>
		/// ������
		/// </summary>
        private OperEnvironment auditingOper;
		
		/// <summary>
		/// ��������
		/// </summary>
		private string type;

		#endregion

        #region ����

		/// <summary>
		/// �������
		/// </summary>
		public decimal Money
		{
			get
			{
				return this.money;
			}
			set
			{
				this.money = value ;
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		public OperEnvironment  AuditingOper
		{
			get
			{
                if (auditingOper == null)
                {
                    auditingOper = new OperEnvironment();
                }
				return this.auditingOper;
			}
			set
			{
				this.auditingOper = value ;
			}
		}
		
		/// <summary>
		/// ��������
		/// </summary>
		public string Type
		{
			get
			{
				return this.type ;
			}
			set
			{
				this.type = value ;
			}
		}
		#endregion

		#region ����

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new Caution Clone()
		{
			Caution caution= base.Clone() as Caution;
			caution.AuditingOper = this.AuditingOper.Clone();
			return caution;
		}

		#endregion
		
		#region ����
		
		/// <summary>
		/// ������ ������
		/// </summary>
        [Obsolete("����Ϊ ���� AuditingOper")]
        private OperEnvironment applyPerson;

		#endregion
	}
}
