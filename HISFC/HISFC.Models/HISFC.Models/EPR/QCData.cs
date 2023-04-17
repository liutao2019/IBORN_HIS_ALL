using System;

namespace FS.HISFC.Models.EPR
{
	/// <summary>
	/// QCData ��ժҪ˵����
	/// ������������ʵ��
	/// ���༰�ӿڣ�object
	/// </summary>
    [Serializable]
	public class QCData
	{
		/// <summary>
		/// 
		/// </summary>
		public QCData()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		
		/// <summary>
		/// �����ļ�״̬
		/// </summary>
		protected int myState = 0;
		/// <summary>
		/// ������
		/// </summary>
		protected FS.FrameWork.Models.NeuObject myCreater = new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// ǩ����
		/// </summary>
		protected FS.FrameWork.Models.NeuObject mySaver = new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// �����
		/// </summary>
		protected FS.FrameWork.Models.NeuObject mySealer = new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// ɾ����
		/// </summary>
		protected FS.FrameWork.Models.NeuObject myDeleter = new FS.FrameWork.Models.NeuObject();
		
		/// <summary>
		/// ��ǰ״̬
		/// </summary>
		public int State
		{
			get
			{
				return myState;
			}
			set
			{
				myState = value;
			}
		}
		/// <summary>
		/// ������ id code name 
		/// memo ����
		/// </summary>
		public FS.FrameWork.Models.NeuObject Creater
		{
			get
			{
				return this.myCreater;
			}
			set
			{
				this.myCreater = value;
			}
		}
		/// <summary>
		/// ǩ���� id code name 
		/// memo ����
		/// </summary>
		public FS.FrameWork.Models.NeuObject Saver
		{
			get
			{
				return this.mySaver;
			}
			set
			{
				this.mySaver = value;
			}
		}
		/// <summary>
		/// ����� id code name 
		/// memo ����
		/// </summary>
		public FS.FrameWork.Models.NeuObject Sealer
		{
			get
			{
				return this.mySealer;
			}
			set
			{
				this.mySealer = value;
			}
		}
		/// <summary>
		/// ɾ���� id code name 
		/// memo ����
		/// </summary>
		public FS.FrameWork.Models.NeuObject Deleter
		{
			get
			{
				return this.myDeleter;
			}
			set
			{
				this.myDeleter = value;
			}
		}

		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public  QCData Clone()
		{
			QCData newObj = new QCData();
			newObj.myCreater = this.myCreater.Clone();
			newObj.mySaver = this.mySaver.Clone();
			newObj.mySealer = this.mySealer.Clone();
			newObj.myDeleter = this.myDeleter.Clone();
			return newObj;
		}
	}
}
