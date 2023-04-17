using System;

namespace neusoft.HISFC.Object.EMR
{
	/// <summary>
	/// QCData ��ժҪ˵����
	/// ������������ʵ��
	/// ���༰�ӿڣ�object
	/// </summary>
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
		protected neusoft.neuFC.Object.neuObject myCreater = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// ǩ����
		/// </summary>
		protected neusoft.neuFC.Object.neuObject mySaver = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// �����
		/// </summary>
		protected neusoft.neuFC.Object.neuObject mySealer = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// ɾ����
		/// </summary>
		protected neusoft.neuFC.Object.neuObject myDeleter = new neusoft.neuFC.Object.neuObject();
		
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
		public neusoft.neuFC.Object.neuObject Creater
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
		public neusoft.neuFC.Object.neuObject Saver
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
		public neusoft.neuFC.Object.neuObject Sealer
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
		public neusoft.neuFC.Object.neuObject Deleter
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
