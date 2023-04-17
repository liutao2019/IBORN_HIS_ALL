using System;
using System.Collections;
namespace FS.HISFC.BizLogic.EPR
{
	/// <summary>
	/// Sign ��ժҪ˵����
	/// ��������
	/// </summary>
	public class Sign:FS.FrameWork.Management.Database 
	{
		/// <summary>
		/// ��������ҵ���
		/// </summary>
		public Sign()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		

		#region ǩ������
		/// <summary>
		/// ����һ���ļ���Ϣ
		/// </summary>
		/// <returns></returns>
		public int InsertSign(FS.FrameWork.Models.NeuObject obj)
		{
//			if(this.IsHaveSameSign (obj.ID) ==true)return 0;

			string strSql = "";
			if(this.Sql.GetSql("EPR.Sign.InsertSign",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,obj.ID ,obj.Name ,obj.Memo, obj.User01, obj.User02, obj.User03);
			}
			catch(Exception ex)
			{
				this.Err = "����Ĳ�����\n"+ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		
		/// <summary>
		/// ����һ���ļ���Ϣ
		/// </summary>
		/// <returns></returns>
		public int UpdateSignBackGround(string id, byte[] img)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.Sign.UpdateSignBackGround",ref strSql)==-1) return -1;
			strSql = string.Format(strSql, id);

			return this.InputBlob(strSql, img);
		}
		
		/// <summary>
		/// ����һ���ļ���Ϣ
		/// </summary>
		/// <returns></returns>
		public int DeleteSign(string id)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.Sign.DeleteSign",ref strSql)==-1) return -1;
			return this.ExecNoQuery(strSql,id);
		}
		
		/// <summary>
		/// ����һ���ļ���Ϣ
		/// </summary>
		/// <returns></returns>
		public int UpdateSign(FS.FrameWork.Models.NeuObject obj)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.Sign.UpdateSign",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,obj.ID ,obj.Name ,obj.Memo, obj.User01, obj.User02, obj.User03);
			}
			catch(Exception ex)
			{
				this.Err = "����Ĳ�����\n"+ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}

		

//		/// <summary>
//		/// �Ƿ�����ͬ�Ĳ����ļ�
//		/// </summary>
//		/// <param name="id"></param>
//		/// <returns></returns>
//		public bool IsHaveSameSign (string id)
//		{
//			string strSql = "";
//			if(this.Sql.GetSql("EPR.Sign.IsHaveSameSign",ref strSql)==-1) return false;
//			if(this.ExecQuery(strSql,id)==-1) return false;
//			if(this.Reader.HasRows) return true;
//			return false;
//		}

//		/// <summary>
//		/// ����ļ��ʿ�����
//		/// </summary>
//		/// <param name="ID"></param>
//		/// <returns></returns>
//		public FS.FrameWork.Models.neuObject  GetSign(string ID)
//		{
//			string strSql = "";
//			if(this.Sql.GetSql("EPR.Sign.GetSign.1",ref strSql)==-1) return null;
//			strSql = string.Format(strSql,ID);
//			ArrayList al = this.myGetSign(strSql);
//			if(al == null || al.Count<=0) return null;
//			return al[0] as FS.FrameWork.Models.neuObject ;
//		}

		/// <summary>
		/// �������������Ϣ-��ѯ���õĲ�����Ϣ
		/// </summary>
		/// <param name="inpatientNo"></param>
		/// <param name="ID"></param>
		/// <returns></returns>
		public FS.FrameWork.Models.NeuObject GetSign(string ID)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.Sign.GetSign",ref strSql)==-1) return null;
			strSql = string.Format(strSql,ID);
			ArrayList al =  this.myGetSign(strSql);
			if(al ==null || al.Count == 0) return null;
			return al[0] as FS.FrameWork.Models.NeuObject;
		}

		public byte[] GetSignBackGround(string ID)
		{
			string strSql = "";

			if(this.Sql.GetSql("EPR.Sign.GetSignBackGround", ref strSql) == -1) return null;
			strSql = string.Format(strSql, ID);
			return this.OutputBlob(strSql);
		} 
		
		/// <summary>
		/// �������������Ϣ-��ѯ���õĲ�����Ϣ
		/// </summary>
		/// <param name="inpatientNo"></param>
		/// <param name="ID"></param>
		/// <returns></returns>
		public ArrayList GetSignList()
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.Sign.GetSignList",ref strSql)==-1) return null;
			return this.myGetSign(strSql);
		}
		/// <summary>
		/// ���� ���� ��ѯ�ļ��б�
		/// </summary>
		/// <param name="strWhere"></param>
		/// <returns></returns>
		public ArrayList GetSignBySqlWhere(string strWhere)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.Sign.GetSign",ref strSql)==-1) return null;
			strSql = strSql +" "+strWhere;
			return this.myGetSign(strSql);
		}

		
		#region "˽��"
		private ArrayList myGetSign(string sql)
		{
			if(this.ExecQuery(sql)==-1) return null;
			ArrayList al = new ArrayList();
			while(this.Reader.Read())
			{
				FS.FrameWork.Models.NeuObject  sign = new FS.FrameWork.Models.NeuObject();
				sign.ID = this.Reader[0].ToString();
				sign.Name = this.Reader[1].ToString();
				sign.Memo = this.Reader[2].ToString();
				sign.User01 = this.Reader[3].ToString();
				sign.User02 = this.Reader[4].ToString();
				sign.User03 = this.Reader[5].ToString();
				al.Add(sign);
			}
			this.Reader.Close();
			return al;
		}

		/// <summary>
		/// ������Ա���Ա䶯���ݣ�����ִ�и��²��������û���ҵ����Ը��µ����ݣ������һ���¼�¼
		/// </summary>
		/// <param name="Permission">Ȩ��ʵ��</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int SetSign(FS.FrameWork.Models.NeuObject sign, byte[] img) 
		{
			int param;
			//ִ�и��²���
			param = UpdateSign(sign);

			//���û���ҵ����Ը��µ����ݣ������һ���¼�¼
			if (param == 0 || param == -1) 
			{
				param = InsertSign(sign);
			}
			if(param == -1) return -1;
			param = UpdateSignBackGround(sign.ID, img);
			return param;
		}
		#endregion
		#endregion

		
	}
}
