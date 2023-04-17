using System;

namespace neusoft.HISFC.Object.Case
{
	/// <summary>
	/// Fee ��ժҪ˵����ID ����Ա���� Name ����Ա����
	/// </summary>
	public class Fee : neusoft.neuFC.Object.neuObject
	{
		public Fee()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		
		#region ˽�б���

		private string inpatientNO;
		private neusoft.neuFC.Object.neuObject myDeptInfo = new neusoft.neuFC.Object.neuObject();
		private neusoft.neuFC.Object.neuObject myMainOutICD = new neusoft.neuFC.Object.neuObject();
		private decimal totCost;
		private DateTime outDate;
		private DateTime operDate;
		private neusoft.neuFC.Object.neuObject myFeeInfo = new neusoft.neuFC.Object.neuObject();
	
		#endregion

		#region ����

		/// <summary>
		/// סԺ��ˮ��
		/// </summary>
		public string InpatientNO
		{
			get{ return inpatientNO; }
			set
			{
				if( CaseFunc.ExLength( value, 14, "סԺ��ˮ��" ) )
				{
					inpatientNO = value;
				}
			}
		}
		
		/// <summary>
		/// ������Ϣ(�Ƿ�Ҫ���ǻ���ת�����) ID ���ұ��� Name ��������
		/// </summary>
		public neusoft.neuFC.Object.neuObject DeptInfo
		{
			get{ return myDeptInfo; }
			set{ myDeptInfo = value; }
		}
		/// <summary>
		/// ��Ժ�������Ϣ ID �������Ϣ Name �������
		/// </summary>
		public neusoft.neuFC.Object.neuObject MainOutICD
		{
			get{ return myMainOutICD; }
			set{ myMainOutICD = value; }
		}
		/// <summary>
		/// ���
		/// </summary>
		public decimal TotCost
		{
			get{ return totCost; }
			set{ totCost = value; }
		}
		/// <summary>
		/// ��Ժ����
		/// </summary>
		public DateTime OutDate
		{
			get{ return outDate; }
			set{ outDate = value; }
		}
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime OperDate
		{
			get{ return operDate; }
			set{ operDate = value; }
		}
		/// <summary>
		/// ������Ϣ ID ���ô������ Name ���ô�������
		/// </summary>
		public neusoft.neuFC.Object.neuObject FeeInfo
		{
			get{ return myFeeInfo; }
			set{ myFeeInfo = value; }
		}

		#endregion

		#region ���ú���

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>Case.Fee</returns>
		public new Fee Clone()
		{
			Fee FeeClone = base.MemberwiseClone() as Fee;
	  
			FeeClone.FeeInfo = this.FeeInfo.Clone();
			FeeClone.DeptInfo = this.DeptInfo.Clone();
			FeeClone.MainOutICD = this.MainOutICD.Clone();
			
			return FeeClone;
		}

		#endregion
	}
}
