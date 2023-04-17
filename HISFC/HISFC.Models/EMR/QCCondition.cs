using System;
using System.Collections;
namespace neusoft.HISFC.Object.EMR
{
	/// <summary>
	/// QCCondition ��ժҪ˵����
	/// ID Type =0,1,2,3,4,5
	/// 
	///	"������ \'��Ϣ\'������\'����\'", 0
	///	"��HIS\'��Ϣ\'������\'����\'",  1
	///	"������ \'����\'���Ѿ�\'����\'", 2
	///	"������-\'����\'���Ѿ�\'ǩ��\'", 3
	///	"������+\'����\'������ʱ��,����\'ʱ��\'��", 4
	///	"���ؼ� \'����\'������\'����\'" 5
	/// </summary>
	public class QCCondition:neusoft.neuFC.Object.neuObject
	{
		public QCCondition()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ����
		/// </summary>
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
				if(value.Length<4) return;
				string s = value.Substring(0,4);
				if(s =="������ ")
				{
					base.ID = "0";
				}
				else if(s =="��HIS")
				{
					base.ID = "1";
				}
				else if(s =="������ ")
				{
					base.ID = "2";
				}
				else if(s =="������-")
				{
					base.ID = "3";
				}
				else if(s =="������+")
				{
					base.ID = "4";
				}
				else if(s =="���ؼ� ")
				{
					base.ID = "5";
				}
				else//����ʶ��
				{
					return;
				}
				//ת��
				int iPosition_start =0,iPosition_end =0;
				try
				{
					iPosition_start = value.IndexOf("'",0);
					iPosition_end = value.IndexOf("'",iPosition_start + 1);
					this.strInfoName = value.Substring(iPosition_start + 1,iPosition_end - iPosition_start -1);
				
					iPosition_start = value.IndexOf("'",iPosition_end + 1);
					iPosition_end = value.IndexOf("'",iPosition_start + 1);
					this.strInfoCondition = value.Substring(iPosition_start + 1,iPosition_end - iPosition_start -1);
				
				}
				catch{}
			}
		}
		private string strInfoName ="";
		/// <summary>
		/// ������Ϣ
		/// </summary>
		public string InfoName
		{
			get
			{
				return this.strInfoName;
			}
			
		}
		private string  strInfoCondition ="";
		/// <summary>
		/// ��Ϣ����
		/// </summary>
		public string InfoCondition
		{
			get
			{
				return this.strInfoCondition;
			}
			
		}
		
	}
}
