using System;

namespace neusoft.HISFC.Object.Power
{
	/// <summary>
	/// SysGroup ��ժҪ˵����
	/// ϵͳ��
	/// id ������룬name ��������
	/// </summary>
	public class SysGroup:neusoft.neuFC.Object.neuObject,neusoft.HISFC.Object.Base.ISort
	{
		public SysGroup()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ������
		/// </summary>
		public neusoft.neuFC.Object.neuObject ParentGroup = new neusoft.neuFC.Object.neuObject();

		#region ISort ��Ա
		protected int sortid;
		public int SortID
		{
			get
			{
				// TODO:  ��� SysGroup.SortID getter ʵ��
				return this.sortid;
			}
			set
			{
				// TODO:  ��� SysGroup.SortID setter ʵ��
				this.sortid = value;
			}
		}

		#endregion
		public new SysGroup Clone()
		{
			SysGroup obj= base.Clone() as SysGroup;
			obj.ParentGroup = this.ParentGroup.Clone();
			return obj;
		}
	}
}
