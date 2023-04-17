using System;
using System.Collections;
namespace FS.HISFC.BizLogic.Operation
{
	/// <summary>
	/// OpsTableAlloc ��ժҪ˵����
	/// ������̨���������
	/// </summary>
	public class OpsTableAlloc : FS.FrameWork.Management.Database 
	{
		public OpsTableAlloc()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ��Ա����	
		///<summary>
		///������������̨������Ϣ
		///</summary>
		///<param name="AllotInfoAl">��������������Ϣ�����飬Ԫ��ΪFS.HISFC.Models.Operation.OpsTableAllot����</param>
		///<returns>0�ɹ� -1ʧ��</returns>
		public int AddAllotInfo(ArrayList AllotInfoAl)
		{
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsTableAlloc.AddAllotInfo.1",ref strSql) == -1) 
			{
				return -1;
			}

			foreach(FS.HISFC.Models.Operation.OpsTableAllot thisAllot in AllotInfoAl)
			{
				try
				{				
					//������̨����������Ӽ�¼
					strSql = string.Format(strSql,thisAllot.OpsRoom.ID.ToString(),thisAllot.Dept.ID.ToString(),
						thisAllot.Week.ID.ToString(),thisAllot.Qty,thisAllot.User.ID.ToString(),this.GetSysDateTime());
				}
				catch(Exception ex)
				{
					this.Err = ex.Message;
					this.ErrCode = ex.Message;
					return -1;            
				}
				if (strSql == null) return -1;	
			
				if(this.ExecNoQuery(strSql) == -1) return -1;
			}
			return 0;
		}
		///<summary>
		///������������̨������Ϣ
		///</summary>
		///<param name="AllotInfoAl">��������������Ϣ�����飬Ԫ��ΪFS.HISFC.Models.Operation.OpsTableAllot����</param>
		///<returns>0�ɹ� -1ʧ��</returns>
		public int UpdateAllotInfo(ArrayList AllotInfoAl)
		{
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsTableAlloc.UpdateAllotInfo.1",ref strSql) == -1) 
			{
				return -1;
			}

			foreach(FS.HISFC.Models.Operation.OpsTableAllot thisAllot in AllotInfoAl)
			{
				try
				{				
					//������̨������и��ļ�¼
					strSql = string.Format(strSql,thisAllot.OpsRoom.ID.ToString(),thisAllot.Dept.ID.ToString(),
						thisAllot.Week.ID.ToString(),thisAllot.Qty,thisAllot.User.ID.ToString(),this.GetSysDateTime());
				}
				catch(Exception ex)
				{
					this.Err = ex.Message;
					this.ErrCode = ex.Message;
					return -1;            
				}
				if (strSql == null) return -1;	
			
				if(this.ExecNoQuery(strSql) == -1) return -1;
			}
			return 0;
		}
		///<summary>
		///ɾ����������̨������Ϣ
		///</summary>
		///<param name="thisAllot">����������Ϣ����(FS.HISFC.Models.Operation.OpsTableAllot����)</param>
		///<returns>0�ɹ� -1ʧ��</returns>
		public int DelAllotInfo(FS.HISFC.Models.Operation.OpsTableAllot thisAllot)
		{
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsTableAlloc.DelAllotInfo.1",ref strSql) == -1) return -1;
			
			try
			{				
				//������̨�������ɾ����¼
				strSql = string.Format(strSql,thisAllot.OpsRoom.ID.ToString(),
					thisAllot.Dept.ID.ToString(),thisAllot.Week.ID.ToString());
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return -1;            
			}
			if (strSql == null) return -1;			
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// ��������ҵ���̨������Ϣ
		/// </summary>
		/// <param name="AllotInfoAl">
		/// �����ã���������������Ϣ�����飬
		/// Ԫ��ΪFS.HISFC.Models.Operation.OpsTableAllot����,����ʱû��Ԫ�أ�</param>
		/// <param name="OpsRoom">��������ʵ�壩</param>
		/// <returns>0�ɹ� -1ʧ�� </returns>
		public int GetAllotInfo(ref ArrayList AllotInfoAl, FS.FrameWork.Models.NeuObject OpsRoom)
		{
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsTableAlloc.GetAllotInfo.1",ref strSql) == -1) return -1;
			try
			{
				//��ȡ��������̨������Ϣ
				strSql = string.Format(strSql,OpsRoom.ID.ToString());
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return -1;            
			}
			if (strSql == null) return -1;
			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{
					FS.HISFC.Models.Operation.OpsTableAllot thisAllot = new FS.HISFC.Models.Operation.OpsTableAllot();
					try
					{
						thisAllot.OpsRoom.ID = Reader[0].ToString();//�������Ҵ���
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						thisAllot.Dept.ID = Reader[1].ToString();//�ٴ����Ҵ���
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						thisAllot.Dept.Name = Reader[4].ToString();//�ٴ���������
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						thisAllot.Week.ID = Reader[2].ToString();//����
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						thisAllot.Qty = System.Convert.ToInt16(Reader[3]);//��̨��
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}

					AllotInfoAl.Add(thisAllot);
				}
			}
			catch(Exception ex)
			{
				this.Err="���������̨������Ϣ����"+ex.Message;
				this.ErrCode="-1";
				this.WriteErr();
				return -1;
			}
			this.Reader.Close();
			return 0;
		}
		/// <summary>
		/// ��������ҵ���̨������Ϣ �����أ�
		/// </summary>
		/// <param name="AllotInfoAl">
		/// �����ã���������������Ϣ�����飬
		/// Ԫ��ΪFS.HISFC.Models.Operation.OpsTableAllot����,����ʱû��Ԫ�أ�</param>
		/// <param name="OpsRoom">��������ʵ�壩</param>
		/// <param name="Dept">���ٴ�����ʵ�壩</param>
		/// <returns>0�ɹ� -1ʧ�� </returns>
		public int GetAllotInfo(ref ArrayList AllotInfoAl, FS.FrameWork.Models.NeuObject OpsRoom,FS.FrameWork.Models.NeuObject Dept)
		{
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsTableAlloc.GetAllotInfo.2",ref strSql) == -1) return -1;
			try
			{
				//��ȡ��������̨������Ϣ
				strSql = string.Format(strSql,OpsRoom.ID.ToString(),Dept.ID.ToString());
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return -1;            
			}
			if (strSql == null) return -1;
			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{
					FS.HISFC.Models.Operation.OpsTableAllot thisAllot = new FS.HISFC.Models.Operation.OpsTableAllot();
					try
					{
						thisAllot.OpsRoom.ID = Reader[0].ToString();//�������Ҵ���
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						thisAllot.Dept.ID = Reader[1].ToString();//�ٴ����Ҵ���
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						thisAllot.Dept.Name = Reader[4].ToString();//�ٴ���������
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						thisAllot.Week.ID = Reader[2].ToString();//����
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						thisAllot.Qty = System.Convert.ToInt16(Reader[3]);//��̨��
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}

					AllotInfoAl.Add(thisAllot);
				}
			}
			catch(Exception ex)
			{
				this.Err="���������̨������Ϣ����"+ex.Message;
				this.ErrCode="-1";
				this.WriteErr();
				return -1;
			}
			this.Reader.Close();
			return 0;
		}
		#endregion
	}
}
