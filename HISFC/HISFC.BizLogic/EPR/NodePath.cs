using System;
using System.Collections;
namespace FS.HISFC.BizLogic.EPR
{
	/// <summary>
	/// NodePath ��ժҪ˵����
	/// </summary>
	public class NodePath:FS.FrameWork.Management.Database
	{
		public NodePath()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ����һ���½ڵ�
		/// </summary>
		/// <returns></returns>
		public int InsertNodePath(FS.FrameWork.Models.NeuObject obj)
		{
			string strSql = "";
			if(this.Sql.GetSql("Manager.NodePath.Insert",ref strSql)==-1) return -1;
			
			return this.ExecNoQuery(strSql,obj.ID,obj.Name,obj.Memo,obj.User01,obj.User02,obj.User03,"0");
		}
		
		/// <summary>
		///  ɾ���ڵ�
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public int DeleteNodePath(string id)
		{
			string strSql = "";
			if(this.Sql.GetSql("Manager.NodePath.Delete",ref strSql)==-1) return -1;
			return this.ExecNoQuery(strSql,id);
		}
		
	
		/// <summary>
		/// ��ѯ���нڵ�
		/// </summary>
		/// <returns></returns>
		public System.Data.DataSet GetNodePath()
		{
			string strSql="";
	
			if(this.Sql.GetSql("Manager.NodePath.Select",ref strSql)==-1) return null;
			System.Data.DataSet ds = new System.Data.DataSet();
			if(this.ExecQuery(strSql,ref ds) == -1) return null;
			return ds;
		}

		/// <summary>
		/// �������
		/// </summary>
		/// <returns></returns>
		public ArrayList GetNodePathList()
		{
			string strSql="";
	
			if(this.Sql.GetSql("Manager.NodePath.Select",ref strSql)==-1) return null;
			
			ArrayList al = new ArrayList();
			FS.FrameWork.Models.NeuObject obj = null;
			if(this.ExecQuery(strSql) == -1) return null;

			while(this.Reader.Read())
			{
				obj = new FS.FrameWork.Models.NeuObject();

				try
				{
					obj.ID =this.Reader[0].ToString();//fullpath
				}
				catch
				{}
				try
				{
					obj.Name =this.Reader[1].ToString();//node name
				}
				catch
				{}
				try
				{
					obj.Memo  =this.Reader[2].ToString();//memo
				}
				catch
				{}
				try
				{
					obj.User01  =this.Reader[3].ToString();
				}
				catch
				{}
				try
				{
					obj.User02  =this.Reader[4].ToString();
				}
				catch
				{}
				try
				{
					obj.User03  =this.Reader[5].ToString();
				}
				catch
				{}
				al.Add(obj);
			}
			this.Reader.Close();
			return al;
		}
	}
}
