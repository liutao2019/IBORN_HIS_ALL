using System;
using FS.HISFC.Models;
using System.Collections;
using FS.FrameWork.Models;

namespace FS.HISFC.BizLogic.Order
{
	/// <summary>
	/// TransFusion ��ժҪ˵����
	/// ��Һ������
	/// </summary>
	public class TransFusion:FS.FrameWork.Management.Database
	{
		public TransFusion()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// �����Һ��������Ϣ
		/// ������д��NURSE_CELL_CODE,USAGE_CODE��
		/// </summary>
		/// <param name="nurseCode"></param>
		/// <param name="usageCode"></param>
		/// <returns></returns>
		public int InsertTransFusion( string nurseCode, string usageCode )
		{
			#region "�ӿ�"
			//���룺0 �������� 1�÷����� 2 ����Ա
			//������0
			#endregion
			string strSql = "";
		
			if (this.Sql.GetCommonSql("Order.TransFusion.InsertItem.1",ref strSql)==-1) 
			{
				this.Err = "û���ҵ�Order.TransFusion.InsertItem.1";
				return -1;
			}
			try
			{
				strSql = string.Format(strSql,nurseCode,usageCode,this.Operator.ID);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		
		/// <summary>
		///  ɾ����Һ��������Ϣ
		///  ������д��NURSE_CELL_CODE,USAGE_CODE��
		/// </summary>
		/// <param name="nurseCode"></param>
		/// <param name="usageCode"></param>
		/// <returns>-1 ���� 0 û���ҵ���¼ >0 ��¼����</returns>
		public int DeleteTransFusion(string nurseCode, string usageCode)
		{
			string strSql = "";

			#region "�ӿ�"
			//���룺0 �������� 1�÷����� 2 ����Ա 3 ����ʱ��
			//������0
			#endregion
			if (this.Sql.GetCommonSql("Order.TransFusion.DeleteItem.1", ref strSql) == -1)
			{
				this.Err = "û���ҵ�Order.TransFusion.DeleteItem.1";
				
				return -1;
			}
			try
			{
				strSql = string.Format(strSql,nurseCode,usageCode);
			}
			catch (Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}

		
		/// <summary>
		///  �����Һ��������Ϣ
		/// </summary>
		/// <param name="nurseCode"></param>
		/// <returns></returns>
		public ArrayList QueryTransFusion( string nurseCode )
		{
			ArrayList al = new ArrayList();
			string strSql = "";
			//Order.TransFusion.Select.1
			//���룺0  NurseCode
			//����:���ڸ���Ŀ���÷��ĸ���
			if(this.Sql.GetCommonSql("Order.TransFusion.Select.1", ref strSql) == 0)
			{
				if(this.ExecQuery(strSql,nurseCode)==-1) return null;
				while(this.Reader.Read())
				{
					al.Add(this.Reader[0].ToString());
				}
				this.Reader.Close();
			}
			else
			{
				this.Err = "û���ҵ�Order.TransFusion.Select.1";
				if(!this.Reader.IsClosed)
					this.Reader.Close();
				return null;
			}
			return al;
		}

		#region ����
		[Obsolete("��QueryTransFusion������",true)]
		public ArrayList GetTransFusion( string nurseCode )
		{
			return this.QueryTransFusion(nurseCode);
		}
		#endregion
	}
}
