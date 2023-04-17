using System;
using System.Collections;
using System.Data;

namespace neusoft.HISFC.Management.Fee
{
	/// <summary>
	///�������صģ���Ҫ�����ݿ����ӵ�һЩ������(Create By Maokb)
	///1.��ѯ��С�������ƺʹ��룻
	/// </summary>
	public class FeeManage:neusoft.neuFC.Management.Database 
	{
		/// <summary>
		/// ������1.��ѯ��С�������ƺʹ��룻
		/// </summary>
		public FeeManage()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ���к���
		/// <summary>
		/// ��ѯ��С���ô��룬����
		/// </summary>
		/// <returns>������С���ô�������Ƶ�����</returns>
		public ArrayList GetMinFee()
		{
			string sql = "";
			if(this.Sql.GetSql("Fee.FeeManage.GetMinFee",ref sql)==-1)
			{
				this.Err="û���ҵ�Fee.FeeManage.GetMinFee�ֶ�!";
				return null;
			}
			//��ѯ���Item
			return this.addItem2(sql);
		}
		#endregion

		#region ˽�к���
		/// <summary>
		/// ����������Ӱ��������Item
		/// </summary>
		/// <param name="excSql">ִ�е�SQL���</param>
		/// <returns></returns>
		private ArrayList addItem2(string excSql)
		{
			ArrayList al = new ArrayList();

			if(this.ExecQuery(excSql)==-1)
				return null;
			
			try
			{
				while(this.Reader.Read())
				{
					neusoft.neuFC.Object.neuObject obj = new neusoft.neuFC.Object.neuObject();
					obj.ID = this.Reader[0].ToString();//ID
					obj.Name = this.Reader[1].ToString();//Name
					al.Add(obj);
					obj=null;
				}
				this.Reader.Close();
				return al;
			}
			catch(Exception ex)
			{
				this.Err="�����Ŀ����"+ex.Message;
				this.ErrCode="-1";
				this.WriteErr();
				if(this.Reader.IsClosed==false)this.Reader.Close();
				al=null;
				return al;
			}
		}
		#endregion
	}
}
