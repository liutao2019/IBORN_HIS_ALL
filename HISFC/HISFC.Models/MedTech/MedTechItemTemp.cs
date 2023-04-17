using System;

namespace neusoft.HISFC.Object.MedTech
{
	/// <summary>
	/// MedTechItemTemp ��ժҪ˵����
	/// ҽ��ԤԼ�Ű���Ϣ
	/// by sunxh
	/// 2005-3-3
	/// 
	/// ��Ŀ��     ��Ŀ ��ĿԤԼģ��
	///item_code,   --��Ŀ����
	///item_name,   --��Ŀ����
	///unit_flag,   --��λ��ʶ
	///dept_code,   --���Һ�
	///dept_name,   --��������
	///week,        --����
	///noon_code,   --���
	///book_lmt,   --ԤԼ�޶�
	///specialbook_lmt,   --����ԤԼ�޶�
	///valid_flag,   --1��Ч/0��Ч
	///remark,   --ע������
	///oper_code,   --����Ա
	///oper_date    --��������
	/// </summary>
	public class MedTechItemTemp : neusoft.neuFC.Object.neuObject
	{
		public MedTechItemTemp()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ������
		string deptName;		//��������
		string week;            //����
		string noonCode;        //���
		decimal bookLmt;         //ԤԼ�޶�
		decimal specialBookLmt;  //����ԤԼ�޶�
		private int conformNum ; //�ն�ȷ����
		#endregion

		/// <summary>
		/// ҽ��ԤԼ��Ŀ��Ϣ
		/// </summary>
		public neusoft.HISFC.Object.MedTech.MedTechItem MedTechItem = new MedTechItem();
		/// <summary>
		/// �ն�ȷ����
		/// </summary>
		public int  ConformNum
		{
			get
			{
				return conformNum;
			}
			set
			{
				conformNum = value;
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string DeptName
		{
			get 
			{
				return deptName;
			}
			set
			{
				deptName = value;
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Week
		{
			get 
			{
				return week;
			}
			set
			{
				week = value;
			}
		}
		/// <summary>
		/// ���
		/// </summary>
		public string NoonCode
		{
			get 
			{
				return noonCode;
			}
			set
			{
				noonCode = value;
			}
		}
		/// <summary>
		/// ԤԼ�޶�
		/// </summary>
		public decimal BookLmt
		{
			get 
			{
				return bookLmt;
			}
			set
			{
				bookLmt = value;
			}
		}
		/// <summary>
		/// ����ԤԼ�޶�
		/// </summary>
		public decimal SpecialBookLmt
		{
			get 
			{
				return specialBookLmt;
			}
			set
			{
				specialBookLmt = value;
			}
		}

		public new MedTechItemTemp Clone()
		{
			MedTechItemTemp obj = (MedTechItemTemp)base.Clone() ;
			return obj;
		}

	}
}
