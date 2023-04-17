using System;
using System.Collections;
namespace FS.HISFC.BizLogic.Manager
{
	/// <summary>
	/// Report ��ժҪ˵����
	/// ������������
	/// </summary>
	public class Report:DataBase 
	{
		public Report()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// �������ʵ����ʵ��
		/// </summary>
		public FS.HISFC.Models.Base.Report m_Report = new FS.HISFC.Models.Base.Report();

		/// <summary>
		/// ���ݱ�������ȡ����ʵ��
		/// </summary>
		/// <param name="ParentGroupID">���������</param>
		/// <param name="GroupID">���������</param>
		/// <param name="ReportID">�������</param>
		/// <returns></returns>
		public FS.HISFC.Models.Base.Report GetReport(string ParentGroupID,string GroupID,string ReportID)
		{
			FS.HISFC.Models.Base.Report Report = new FS.HISFC.Models.Base.Report();
			string strSql = "";
			string strSqlwhere = "";
			if(this.GetSQL("Manager.Report.GetReport.Select.1",ref strSql) == -1) return null;
			if(this.GetSQL("Manager.Report.GetReport.Where.1",ref strSqlwhere) == -1) return null;
			try
			{
				//��ȡ��������̨������Ϣ
				strSqlwhere = string.Format(strSqlwhere,ParentGroupID,GroupID,ReportID);
			}
			catch{}
			if (strSql == null) return null;
			if(strSqlwhere == null) return null;
			strSql = strSql + " \n " + strSqlwhere;
			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{
					//Reader[0]����������
					//Reader[1]����������
					//Reader[2]�������
					Report.ID =Reader[2].ToString(); //�������
					Report.Name = Reader[3].ToString();//��������
					
					Report.CtrlName = Reader[4].ToString();//�ؼ�����
					
					Report.Parm = Reader[5].ToString();//�������

					Report.SpecialFlag = Reader[6].ToString();//������
					
					Report.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[7].ToString());//�Ƿ���Ч
					Report.SortID = FS.FrameWork.Function.NConvert.ToInt32(Reader[10].ToString()); //���
				}
			}
			catch(Exception ex)
			{
				this.Err="HisFC.Manager.Report.GetReport where1";
				this.ErrCode="��ñ�����Ϣ����"+ex.Message;
				this.WriteErr();
				return null;
			}
			this.Reader.Close();
			return Report;
		}


		/// <summary>
		/// �������ñ���-FS.HISFC.Models.Admin.SysModelFunction 
		/// </summary>
		/// <param name="GroupID">��ID</param>
		/// <returns>ģ���б�FS.HISFC.Models.Admin.SysModelFunction </returns>
		public ArrayList GetReportByGroup(string GroupID,string type) {
			ArrayList al = new ArrayList();
			string sql = "";
			if(this.GetSQL("Manager.Report.GetReportByGroup",ref sql) == -1) return null;
			try {
				sql = string.Format(sql,GroupID,type);
			}
			catch{this.Err ="����Manager.Report.GetConstByGroup����";this.WriteErr();}
			if(this.ExecQuery(sql)==-1) return null;
			
				while(this.Reader.Read()) {
					FS.HISFC.Models.Admin.SysModelFunction obj = new FS.HISFC.Models.Admin.SysModelFunction();
					obj.ID = this.Reader[0].ToString();
					obj.Name = this.Reader[1].ToString();
					obj.FunName = this.Reader[1].ToString();
					obj.WinName  = this.Reader[2].ToString();
					obj.Param = this.Reader[3].ToString();
					obj.DllName = this.Reader[4].ToString();
					obj.FormType = this.Reader[5].ToString();
					obj.FormShowType = this.Reader[6].ToString();
					obj.User01 = this.Reader[7].ToString();
					obj.User02 = this.Reader[8].ToString();
					al.Add(obj);
				}
			
			this.Reader.Close();
			return al;
		}
		/// <summary>
		/// �������ñ���-FS.HISFC.Models.Admin.SysModelFunction 
		/// </summary>
		/// <param name="GroupID">��ID</param>
		/// <returns>ģ���б�FS.HISFC.Models.Admin.SysModelFunction </returns>
		public ArrayList GetReportByGroup(string GroupID) 
		{
			ArrayList al = new ArrayList();
			string sql = "";
			if(this.GetSQL("Manager.Report.GetReportByGroup1",ref sql) == -1) return null;
			try 
			{
				sql = string.Format(sql,GroupID);
			}
			catch{this.Err ="����Manager.Report.GetConstByGroup����";this.WriteErr();}
			if(this.ExecQuery(sql)==-1) return null;
			
				while(this.Reader.Read()) 
				{
					FS.HISFC.Models.Admin.SysModelFunction obj = new FS.HISFC.Models.Admin.SysModelFunction();
					obj.ID = this.Reader[0].ToString();
					obj.Name = this.Reader[1].ToString();
					obj.FunName = this.Reader[1].ToString();
					obj.WinName  = this.Reader[2].ToString();
					obj.Param = this.Reader[3].ToString();
					obj.DllName = this.Reader[4].ToString();
					obj.FormType = this.Reader[5].ToString();
					obj.FormShowType = this.Reader[6].ToString();
					obj.User01 = this.Reader[7].ToString();
					obj.User02 = this.Reader[8].ToString();
					al.Add(obj);
				}
			
			this.Reader.Close();
			return al;
		}

		/// <summary>
		/// �����±���
		/// </summary>
		/// <returns>0 success -1 fail</returns>
		public int AddReport(FS.HISFC.Models.Base.Report info)
		{
			string strSql = "";
			if (this.GetSQL("Management.Manager.Report.AddReport",ref strSql)==-1)return -1;
			try
			{
				string IsValid="";
				if(info.IsValid)
				{
					IsValid = "1";
				}
				else
				{
					IsValid ="0";
				}
				string OperId =this.Operator.ID;
				strSql = string.Format(strSql,info.ParentCode,info.ID,info.Name,info.CtrlName,info.Parm,info.SpecialFlag,IsValid,OperId,info.SortID);
			}
			catch(Exception ee)
			{
				this.Err  = ee.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}


		/// <summary>
		/// ɾ������
		/// </summary>
		/// <returns>0 success -1 fail</returns>
		public int DelReport(FS.HISFC.Models.Base.Report info )
		{
			string strSql = "";
			if (this.GetSQL("Management.Manager.Report.DelReport",ref strSql)==-1)return -1;
			try
			{
				strSql = string.Format(strSql,info.ParentCode,info.ID);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}


		/// <summary>
		/// �޸ı���
		/// </summary>
		/// <returns>0 success -1 fail</returns>
		public int UpdateReport(FS.HISFC.Models.Base.Report info )
		{
			string strSql = "";
			if (this.GetSQL("Management.Manager.Report.UpdateReport",ref strSql)==-1)return -1;
			try
			{
				string OperId =this.Operator.ID;
			    string IsValid ="";
				if(info.IsValid)
				{
					IsValid = "1";
				}
				else
				{
					IsValid ="0";
				}
				strSql = string.Format(strSql,info.Name,info.CtrlName,info.Parm,info.SpecialFlag,IsValid,OperId,info.ID,info.SortID);
			}
			catch(Exception ee)
			{
				this.Err  = ee.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}


		/// <summary>
		/// ������ʾ����
		/// </summary>
		/// <param name="GroupID"></param>
		/// <returns></returns>
		public ArrayList GetReportAllByGroupID(string GroupID )
		{
			ArrayList List = null;
			string strSql = "";
			if (this.GetSQL("Management.Manager.Report.GetReportAllByGroupID",ref strSql)==-1) return null;

			strSql = string.Format(strSql,GroupID);
			this.ExecQuery(strSql);
			try
			{
				List = new ArrayList();
				FS.HISFC.Models.Base.Report Report = null; 
				while(this.Reader.Read())
				{
					Report =new FS.HISFC.Models.Base.Report();
					//Reader[0]����������
					//Reader[1]����������
					//Reader[2]�������
					Report.ID =Reader[2].ToString(); //�������
					Report.Name = Reader[3].ToString();//��������
					
					Report.CtrlName = Reader[4].ToString();//�ؼ�����
					
					Report.Parm = Reader[5].ToString();//�������

					Report.SpecialFlag = Reader[6].ToString();//������
					
					Report.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[7].ToString());//�Ƿ���Ч
					Report.SortID = FS.FrameWork.Function.NConvert.ToInt32(Reader[10].ToString()); //���
					List.Add(Report);
					Report=null;
				}
			}
			catch(Exception ex)
			{
				this.Err="HisFC.Manager.Report.GetReport where1";
				this.ErrCode="��ñ�����Ϣ����"+ex.Message;
				return null;
			}
			this.Reader.Close();
			return List ;
		}


		/// <summary>
		/// ȡĳһ�������¼
		/// </summary>
		/// <param name="type"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public ArrayList GetReportFromdictionary(string type,string name )
		{
			string strSql="";

			ArrayList al=new ArrayList();
			//�ӿ�˵��
			//����
			if(this.GetSQL("Manager.Report.GetReportFromdictionary",ref strSql)==-1) return null;
			try
			{
				strSql=string.Format(strSql,type,name);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err="�ӿڴ���"+ex.Message;
				this.WriteErr();
				return null;
			}

		
			if(this.ExecQuery(strSql)==-1) return null;
			//FS.FrameWork.Models.NeuObject NeuObject;
			FS.HISFC.Models.Base.Const cons ;
			while(this.Reader.Read())
			{
				//NeuObject=new NeuObject();
				cons = new FS.HISFC.Models.Base.Const();
				//cons.Type = (Const.enuConstant)(Reader[0].ToString());
				cons.ID=this.Reader[1].ToString();
				cons.Name=this.Reader[2].ToString();
				cons.Memo=this.Reader[3].ToString();
				cons.SpellCode=this.Reader[4].ToString();
				cons.WBCode = this.Reader[5].ToString();
				cons.UserCode = this.Reader[6].ToString();
				if(!Reader.IsDBNull(7)) 
					cons.SortID=Convert.ToInt32(this.Reader[7]);
				cons.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[8].ToString());
				cons.OperEnvironment.ID=this.Reader[9].ToString();
				if(!Reader.IsDBNull(10))
					cons.OperEnvironment.OperTime = Convert.ToDateTime(this.Reader[10].ToString());
				
				
				al.Add(cons);
			}
			this.Reader.Close();
			return al;
		}

		
		/// <summary>
		/// ����������
		/// �˷����ṩһ������sql.xml������������ѯ���,������DataSet�Ĺ���.
		/// �����̶�����Щ,���sql��䲻��Ҫ��Щ����,����sql������� OR ({n} = {n}) �������,�������
		/// </summary>
		/// <param name="myBeginTime">��ʼʱ��</param>
		/// <param name="myEndTime">��ֹʱ��</param>
		/// <param name="SqlIndex">��Ҫ��ѯ��SQL�������(��sql.xml�е�λ��)</param>
		/// <returns>������null���ɹ�����dataSet</returns>
		public System.Data.DataSet Query(string SqlIndex, DateTime myBeginTime, DateTime myEndTime) {
			string strSql = "";
			System.Data.DataSet dataSet = new System.Data.DataSet();

			//ȡSQL���
			if (this.GetSQL(SqlIndex, ref strSql) == -1 ) {
				this.Err = "û���ҵ�SQL���:" + SqlIndex ;
				return null;
			}
			try{
				strSql = string.Format(strSql, myBeginTime.ToString(), myEndTime.ToString());
			}
			catch (Exception ex) {
				this.Err = "����ֵʱ�����" + SqlIndex + ":"  + ex.Message;
				this.WriteErr();
				return null;
			}
			//����SQL���ȡ��ѯ���
			if (this.ExecQuery(strSql, ref dataSet) == -1) return null;

			return dataSet;
		}
		
		/// <summary>
		/// ����������
		/// �˷����ṩһ������sql.xml������������ѯ���,������DataSet�Ĺ���.
		/// �����̶�����Щ,���sql��䲻��Ҫ��Щ����,����sql������� OR ({n} = {n}) �������,�������
		/// </summary>
		/// <param name="myBeginTime">��ʼʱ��</param>
		/// <param name="myEndTime">��ֹʱ��</param>
		/// <param name="deptCode">���ұ���</param>
		/// <param name="SqlIndex">��Ҫ��ѯ��SQL�������(��sql.xml�е�λ��)</param>
		/// <returns>������null���ɹ�����dataSet</returns>
		public System.Data.DataSet Query(string SqlIndex, DateTime myBeginTime, DateTime myEndTime, string deptCode) {
			string strSql = "";
			System.Data.DataSet dataSet = new System.Data.DataSet();

			//ȡSQL���
			if (this.GetSQL(SqlIndex, ref strSql) == -1 ) {
				this.Err = "û���ҵ�SQL���:" + SqlIndex ;
				return null;
			}
			try{
				strSql = string.Format(strSql, myBeginTime.ToString(), myEndTime.ToString(), deptCode);
			}
			catch (Exception ex) {
				this.Err = "����ֵʱ�����" + SqlIndex + ":"  + ex.Message;
				this.WriteErr();
				return null;
			}
			//����SQL���ȡ��ѯ���
			if (this.ExecQuery(strSql, ref dataSet) == -1) return null;

			return dataSet;
		}
	}
}
