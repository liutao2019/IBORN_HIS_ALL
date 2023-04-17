using System;
using System.Collections;
namespace neusoft.HISFC.Object.EMR
{
	/// <summary>
	/// QCCondition ��ժҪ˵����
	/// �ʿ����� ʵ��
	/// ID ���룬Name ָ������
	/// </summary>
	public class QCConditions:neusoft.neuFC.Object.neuObject
	{
		public QCConditions()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
	
		/// <summary>
		/// ������������
		/// </summary>
		protected string strConditions = "";
		/// <summary>
		/// ������������
		/// </summary>
		public string Conditions
		{
			get
			{
				return this.strConditions;
			}
			set
			{
				this.strConditions = value;
				//�ֽ��ַ���������QACondition����alCondition,��������
				string[] s = strConditions.Split('\n');
				this.alConditions.Clear();
				for(int i=0;i<s.Length;i++)
				{
					if(s[i].Trim()!="")
					{
						neusoft.HISFC.Object.EMR.QCCondition obj = new QCCondition();
						obj.Name = s[i].TrimStart();
						this.alConditions.Add(obj);
					}
				}
			}
		}
		protected ArrayList alConditions = new ArrayList();
		/// <summary>
		/// �����������
		/// </summary>
		public ArrayList AlConditions
		{
			get
			{
				return this.alConditions;
			}
			set
			{
				this.alConditions = value;
			}
		}
		protected QCAction action = new QCAction();
		/// <summary>
		/// ����
		/// </summary>
		public QCAction Acion
		{
			get
			{
				return action;
			}
			set
			{
				this.action = value;
			}
		}

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new QCConditions Clone()
		{
			QCConditions newObj = new QCConditions();
			newObj = base.Clone() as QCConditions;
			newObj.alConditions = this.alConditions.Clone() as ArrayList;
			newObj.action = this.action.Clone();
			return newObj;
		}
	}
}
