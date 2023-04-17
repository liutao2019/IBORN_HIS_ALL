using System;
using System.Collections;
namespace FS.HISFC.Models.EPR
{
	/// <summary>
	/// QCCondition ��ժҪ˵����
	/// �ʿ����� ʵ��
	/// ID ���룬Name ָ������
	/// </summary>
    [Serializable]
	public class QCConditions:FS.FrameWork.Models.NeuObject
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
						FS.HISFC.Models.EPR.QCCondition obj = new QCCondition();
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
