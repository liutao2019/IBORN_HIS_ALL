using System;


namespace Neusoft.HISFC.Object.PhysicalExam {


	/// <summary>
	/// DeptUse ��ժҪ˵����
	/// </summary>
	public class DeptUse : Neusoft.NFC.Object.NeuObject
	{
		public DeptUse()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		private Neusoft.NFC.Object.NeuObject execDeptInfo = null;
		private Neusoft.NFC.Object.NeuObject deptInfo = null;
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
		public Neusoft.NFC.Object.NeuObject ExecDeptInfo
		{
			get
			{
				if(execDeptInfo == null)
				{
					execDeptInfo = new Neusoft.NFC.Object.NeuObject();
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
		public Neusoft.NFC.Object.NeuObject DeptInfo 
		{
			get
			{
				if(deptInfo == null)
				{
					deptInfo = new Neusoft.NFC.Object.NeuObject();
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
		public Neusoft.HISFC.Object.Base.Item  item = new Neusoft.HISFC.Object.Base.Item();
		/// <summary>
		/// ��ʾ��ϸ��������
		/// </summary>
		public string UnitFlag ;

		public new DeptUse Clone()
		{
			DeptUse obj = base.Clone() as DeptUse;
			obj.ExecDeptInfo= this.ExecDeptInfo.Clone();
			obj.DeptInfo=this.DeptInfo.Clone();//(Neusoft.HISFC.Object.Fee.Invoice)Invoice.Clone();

			obj.item=this.item.Clone();
			return obj;
		}
	}
}
