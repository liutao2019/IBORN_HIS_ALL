using System;
using System.Collections;
namespace neusoft.HISFC.Object.EMR
{
	/// <summary>
	/// QCAction ��ժҪ˵����
	/// </summary>
	public class QCAction
	{
		public QCAction()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		
		protected ArrayList alMessage = new ArrayList();
		/// <summary>
		/// ��Ϣ
		/// </summary>
		public ArrayList AlMessage
		{
			get
			{
				return this.alMessage;
			}
		}
		protected string strName ="";
		/// <summary>
		/// ����
		/// </summary>
		public string Name
		{
			get
			{
				return this.strName ;
			}
			set
			{
				this.strName = value;
				this.alMessage = new ArrayList();
				string[] s = value.Split('\n');
				for(int i=0;i<s.Length ;i++)
				{
					//�ж϶������ͼ���Ϣ
					if(s[i].Trim()!="")
					{
						int iStart,iEnd;
						neusoft.neuFC.Object.neuObject obj = new neusoft.neuFC.Object.neuObject();
						string sTmp = s[i].TrimStart();
						if(sTmp.Substring(0,4) == "ʹ��ʾ ")
						{
							obj.ID = "0";
							iStart =sTmp.IndexOf("'");
							iEnd = sTmp.IndexOf("'",iStart+1);
							obj.Name  =sTmp.Substring(iStart +1,iEnd - iStart -1);
						}
						else if(sTmp.Substring(0,4) == "ʹģ�� ")
						{
							obj.ID  = "1";
							iStart = sTmp.IndexOf("'");
							iEnd = sTmp.IndexOf("'",iStart+1);
							obj.Name = sTmp.Substring(iStart +1,iEnd - iStart -1);
						}
						else
						{
							return;
						}
						this.alMessage.Add(obj);
					}
				}
			}
		}
		/// <summary>
		/// ����дToString
		/// </summary>
		/// <returns></returns>
		public new string ToString()
		{
			return this.strName;
		}
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new QCAction Clone()
		{
			return this.MemberwiseClone() as QCAction;
		}
	}
}
