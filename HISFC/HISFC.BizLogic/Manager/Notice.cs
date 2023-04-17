using System;
using System.Collections;

namespace FS.HISFC.BizLogic.Manager
{
	/// <summary>
	/// Notice ��ժҪ˵����
	/// </summary>
	public class Notice:DataBase
	{
		public Notice()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		/// <summary>
		/// ִ��Sql��� ���Noticeʵ������
		/// </summary>
		/// <param name="sql">��ִ��Sql���</param>
		/// <returns>�ɹ�����ArrayList ʧ�ܷ���null</returns>
		protected ArrayList myNoticeQuery(string sql)
		{
			ArrayList al = new ArrayList();
			
			if (this.ExecQuery(sql) == -1)
				return null;

			try
			{
				FS.HISFC.Models.Base.Notice notice;
				while (this.Reader.Read())
				{
					notice = new FS.HISFC.Models.Base.Notice();

					notice.ID = this.Reader[0].ToString();						//0 ��ˮ��
					notice.Dept.ID = this.Reader[1].ToString();				//1 ���ұ���
					notice.Group.ID = this.Reader[2].ToString();				//2 ���������
					notice.NoticeInfo = this.Reader[3].ToString();				//3 ������Ϣ
					notice.NoticeDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4].ToString());	//4 ��������
					notice.NoticeDept.ID = this.Reader[5].ToString();			//5 ��������
					notice.ExtFlag = this.Reader[6].ToString();					//6 ��չ��־
					notice.OperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7].ToString());	//7 ��������
					notice.OperEnvironment.ID = this.Reader[8].ToString();				//8 ������
					notice.NoticeTitle = this.Reader[9].ToString();				//9 ������Ϣ����

					al.Add(notice);
				}
			}
			catch (Exception ex)
			{
				this.Err = "��ȡ�ѷ�����Ϣ��������" + ex.Message;
				this.WriteErr();
				return null;
			}
			finally
			{
				this.Reader.Close();
			}
			return al;
		}
		/// <summary>
		/// ����insert �� update ��������
		/// </summary>
		/// <param name="notice">noticeʵ��</param>
		/// <returns>�ɹ�����string����</returns>
		protected string[] myNoticeParm(FS.HISFC.Models.Base.Notice notice)
		{
			if (notice == null)
				return null;
			string[] strParm = {
								   notice.ID,						//0 ��ˮ��
									notice.Dept.ID,				//1 ���ұ���
									notice.Group.ID,				//2 ���������
									notice.NoticeInfo,				//3 ������Ϣ
									notice.NoticeDate.ToString(),	//4 ��������
									notice.NoticeDept.ID,			//5 ��������
									notice.ExtFlag,					//6 ��չ��־
									notice.OperEnvironment.OperTime.ToString(),		//7 ��������
									notice.OperEnvironment.ID,				//8 ����Ա
								    notice.NoticeTitle				//9 ������Ϣ����
							   };
			return strParm;
		}


		/// <summary>
		/// ���뷢����Ϣ
		/// </summary>
		/// <param name="notice">������Ϣʵ��</param>
		/// <returns>�ɹ����� 1 ʧ�ܷ���-1</returns>
		protected int InsertNotice(FS.HISFC.Models.Base.Notice notice)
		{
			string sql = "";
			if (this.GetSQL("Manager.Notice.Insert",ref sql) == -1)		
				return -1;

			try
			{
				string[] strParm = this.myNoticeParm(notice);
				sql = string.Format(sql,strParm);
			}
			catch(Exception ex)
			{
				this.Err = "��ʽ��Insert Sql������" + ex.Message;
				this.WriteErr();
				return -1;
			}

			return this.ExecNoQuery(sql);
		}
		/// <summary>
		/// ���·�����Ϣ
		/// </summary>
		/// <param name="notice">������Ϣʵ��</param>
		/// <returns>�ɹ����·���1 ʧ�ܷ���-1</returns>
		protected int UpdateNotice(FS.HISFC.Models.Base.Notice notice)
		{
			string sql = "";
			if (this.GetSQL("Manager.Notice.Update",ref sql) == -1)
				return -1;
			try
			{
				string[] strParm = this.myNoticeParm(notice);
				sql = string.Format(sql,strParm);
			}
			catch(Exception ex)
			{
				this.Err = "��ʽ��Update Sql������" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(sql);
		}

		
		/// <summary>
		/// ɾ��������Ϣ 
		/// </summary>
		/// <param name="noticeID">������Ϣ��ˮ��</param>
		/// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
		public int DeleteNotice(string noticeID)
		{
            //������Ϣ�ȶ���ɾ������¼ɾ��״̬
//            return this.ExecNoQuery(@"update com_notice f
//                                set f.oper_code='{1}',
//                                f.oper_date=to_date('{2}','yyyy-mm-dd hh24:mi:ss')
//                                where f.notice_id='{0}'", 
//                        noticeID, this.Operator.ID, this.GetDateTimeFromSysDateTime().ToString());

			string sql = "";
			if (this.GetSQL("Manager.Notice.Delete",ref sql) == -1)		
				return -1;

			try
			{
				sql = string.Format(sql,noticeID);
			}
			catch(Exception ex)
			{
				this.Err = "��ʽ��Delete Sql������" + ex.Message;
				this.WriteErr();
				return -1;
			}

			return this.ExecNoQuery(sql);
		}

		/// <summary>
		/// ���÷�����Ϣ ��ִ�и��²��� ��ִ�в������
		/// </summary>
		/// <param name="notice">������Ϣʵ��</param>
		/// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
		public int SetNotice(FS.HISFC.Models.Base.Notice notice)
		{
			int parm = this.UpdateNotice(notice);
			if(parm == -1)
				return parm;
			if (parm == 0)
				return this.InsertNotice(notice);
			return parm;
		}

		/// <summary>
		/// ���ݿ��ұ��롢��������� ��ȡ ��Ӧ�ķ�����Ϣ
		/// </summary>
		/// <param name="deptCode">���ұ���</param>
		/// <param name="groupCode">���������</param>
		/// <param name="beginNoticeDate">������ʼ����</param>
		/// <param name="endNoticeDate">������ֹ����</param>
		/// <returns>�ɹ�����notice���� ʧ�ܷ���null</returns>
		public ArrayList GetNotice(string deptCode,string groupCode,DateTime beginNoticeDate,DateTime endNoticeDate)
		{
			string sqlSelect = "",strWhere = "";
			if (this.GetSQL("Manager.Notice.GetNotice.Select",ref sqlSelect) == -1)		
				return null;
			if (this.GetSQL("Manager.Notice.GetNotice.Where.ByDept",ref strWhere) == -1)
				return null;

			try
			{
				sqlSelect = sqlSelect + strWhere;
				sqlSelect = string.Format(sqlSelect,deptCode,groupCode,beginNoticeDate.ToString(),endNoticeDate.ToString());
			}
			catch(Exception ex)
			{
				this.Err = "��ʽ��" + sqlSelect +"Sql������" + ex.Message;
				this.WriteErr();
				return null;
			}

			return this.myNoticeQuery(sqlSelect);
		}
		/// <summary>
		/// ���ݷ������� �������� ��ȡ ��Ӧ�� ������Ϣ
		/// </summary>
		/// <param name="noticeDept">��������</param>
		/// <param name="beginNoticeDate">������ʼ����</param>
		/// <param name="endNoticeDate">������ֹ����</param>
		/// <returns>�ɹ�����notice���� ʧ�ܷ���null</returns>
		public ArrayList GetNotice(string noticeDept,DateTime beginNoticeDate,DateTime endNoticeDate)
		{
			string sqlSelect = "",strWhere = "";
			if (this.GetSQL("Manager.Notice.GetNotice.Select",ref sqlSelect) == -1)		
				return null;
			if (this.GetSQL("Manager.Notice.GetNotice.Where.ByNoticeDept",ref strWhere) == -1)
				return null;

			try
			{
				sqlSelect = sqlSelect + strWhere;
				sqlSelect = string.Format(sqlSelect,noticeDept,beginNoticeDate.ToString(),endNoticeDate.ToString());
			}
			catch(Exception ex)
			{
				this.Err = "��ʽ��" + sqlSelect + "Sql������" + ex.Message;
				this.WriteErr();
				return null;
			}

			return this.myNoticeQuery(sqlSelect);
		}
	}
}
