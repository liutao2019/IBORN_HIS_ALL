using System;


namespace FS.HISFC.Models.Admin {


	/// <summary>
	/// SysGroup ��ժҪ˵����
	/// ϵͳ��
	/// id ������룬name ��������
	/// </summary>
    /// 
    [System.Serializable]
	public class SysGroup:FS.FrameWork.Models.NeuObject,FS.HISFC.Models.Base.ISort
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
		public FS.FrameWork.Models.NeuObject ParentGroup = new FS.FrameWork.Models.NeuObject();

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
