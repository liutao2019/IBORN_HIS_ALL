using System;

namespace neusoft.HISFC.Object.Check
{
	/// <summary>
	/// DeptUse ��ժҪ˵����
	/// </summary>
	public class DeptUse : neusoft.neuFC.Object.neuObject
	{
		public DeptUse()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		private neusoft.neuFC.Object.neuObject execDeptInfo = null;
		private neusoft.neuFC.Object.neuObject deptInfo = null;
		/// <summary>
		/// �������� 
		/// </summary>
		public string ParentCode ;
		/// <summary>
		/// ��������
		/// </summary>
		public string CurrentCode; 
		/// <summary>
		/// ִ�п���
		/// </summary>
		public neusoft.neuFC.Object.neuObject ExecDeptInfo
		{
			get
			{
				if(execDeptInfo == null)
				{
					execDeptInfo = new neusoft.neuFC.Object.neuObject();
				}
				return execDeptInfo;
			}
			set
			{
				execDeptInfo = value;
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public neusoft.neuFC.Object.neuObject DeptInfo 
		{
			get
			{
				if(deptInfo == null)
				{
					deptInfo = new neusoft.neuFC.Object.neuObject();
				}
				return deptInfo;
			}
			set
			{
				deptInfo = value;
			}
		}
		/// <summary>
		/// ��Ŀ
		/// </summary>
		public neusoft.HISFC.Object.Base.Item  item = new neusoft.HISFC.Object.Base.Item();
		/// <summary>
		/// ��ʾ��ϸ��������
		/// </summary>
		public string UnitFlag ;
		public new DeptUse Clone()
		{
			DeptUse obj = base.Clone() as DeptUse;
			obj.ExecDeptInfo= this.ExecDeptInfo.Clone();
			obj.DeptInfo=this.DeptInfo.Clone();//(neusoft.HISFC.Object.Fee.Invoice)Invoice.Clone();

			obj.item=this.item.Clone();
			return obj;
		}
	}
}
