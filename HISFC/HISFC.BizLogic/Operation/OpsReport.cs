using System;
using System.Collections;
using System.Data;
namespace FS.HISFC.BizLogic.Operation
{
	/// <summary>
	/// OpsReport ��ժҪ˵����
	/// </summary>
	public abstract class OpsReport : FS.FrameWork.Management.Database 
	{
		public OpsReport()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ��������
		/// <summary>
		/// ����������Ϣ��ѯ
		/// </summary>
		/// <param name="BeginTime">��ʼʱ��</param>
		/// <param name="EndTime">��ֹʱ��</param>
		/// <returns>�������������������飨Ԫ��Ϊ��ӳ��������Ϣ�����飩</returns>
		public ArrayList GetReport03(DateTime BeginTime,DateTime EndTime)
		{
			ArrayList al = new ArrayList();
			ArrayList alDataRow = new ArrayList();
			//FS.HISFC.BizLogic.Manager.UserManager userManager = new FS.HISFC.BizLogic.Manager.UserManager();
			#region ��ȡ���ݵı���
			string strName = string.Empty;//����
			string strBedNo = string.Empty;//����
			string strPatientNo = string.Empty;//סԺ��/������
			FS.HISFC.Models.Base.SexEnumService sex = new FS.HISFC.Models.Base.SexEnumService();//�Ա�
			//��ȡ����
			int iBirthYear = 0;//������
			int iThisYear = 0;//����
			string strAge = string.Empty;//����
			string strDiagnose = string.Empty;//��ǰ���
			string strItem = string.Empty;//��������(������)
			string strEnterDate = string.Empty;//��ʼ����ʱ��
			string strOutDate = string.Empty;//��������ʱ��
			string strOpsDoct = string.Empty;//����ҽ��
			string strAnesType = string.Empty;//����ʽ
			FS.FrameWork.Models.NeuObject OpsKind = new FS.FrameWork.Models.NeuObject();//��������
			#endregion
			string strSql = string.Empty;
			
			if(this.Sql.GetSql("Operator.OpsReport.GetReport03",ref strSql) == -1) 
			{
				return null;
			}

			try
			{	
				strSql = string.Format(strSql,BeginTime.ToString(),EndTime.ToString());
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport03";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			
			if (strSql == null) 
				return null;
			
			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{					
					FS.HISFC.Models.Base.Employee employee = new FS.HISFC.Models.Base.Employee();
					alDataRow.Clear();
					strName = Reader[0].ToString();//����
					strBedNo = Reader[1].ToString();//����
					strPatientNo = Reader[2].ToString();//סԺ��/������
					sex.ID = Reader[3].ToString();//�Ա�
					iBirthYear = FS.FrameWork.Function.NConvert.ToDateTime(Reader[4].ToString()).Year;
					iThisYear = this.GetDateTimeFromSysDateTime().Year;
					strAge = System.Convert.ToString(iThisYear - iBirthYear);//����
					strDiagnose = Reader[5].ToString();//��ǰ���
					strItem = Reader[6].ToString();//��������
					strEnterDate = Reader[7].ToString();//������ʼʱ��
					strOutDate = Reader[8].ToString();//��������ʱ��
					employee.ID = Reader[9].ToString();
					strOpsDoct = employee.Name;//����ҽ��
					strAnesType = Reader[10].ToString();//����ʽ
					OpsKind.ID = Reader[11].ToString();//��������
					switch(OpsKind.ID.ToString())
					{
						case "1"://��ͨ
							OpsKind.Name = "��ͨ";
							break;
						case "2"://����
							OpsKind.Name = "����";
							break;
						case "3"://��Ⱦ
							OpsKind.Name = "��Ⱦ";
							break;
						default:
							OpsKind.Name = string.Empty;
							break;
					}
					//����ǰ�����ݼ���
					alDataRow.Add(strName);
					alDataRow.Add(strBedNo);
					alDataRow.Add(strPatientNo);
					alDataRow.Add(sex.Name);
					alDataRow.Add(strAge);
					alDataRow.Add(strDiagnose);
					alDataRow.Add(strItem);
					alDataRow.Add(strEnterDate);
					alDataRow.Add(strOutDate);
					alDataRow.Add(strOpsDoct);
					alDataRow.Add(strAnesType);
					alDataRow.Add(OpsKind.Name);
					al.Add(alDataRow.Clone());
				}
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport03";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			this.Reader.Close();
			return al;
		}
		/// <summary>
		/// ��������ͳ��һ����
		/// </summary>
		/// <param name="DeptID">���ұ���</param>
		/// <param name="BeginTime">��ʼʱ��</param>
		/// <param name="EndTime">��ֹʱ��</param>
		/// <returns>�������������������飨Ԫ��Ϊ��ӳ��������Ϣ�����飩</returns>
		public ArrayList GetReport05(string DeptID,DateTime BeginTime,DateTime EndTime)
		{
			ArrayList al = new ArrayList();
			ArrayList alDataRow = new ArrayList();
			
			string strOpsKind = string.Empty;
			string strDegree = string.Empty;
			string strCount = string.Empty;

			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsReport.GetReport05",ref strSql) == -1) return null;
			try
			{	
				strSql = string.Format(strSql,DeptID,BeginTime.ToString(),EndTime.ToString());
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport05";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			if (strSql == null) return null;
			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{					
					alDataRow.Clear();
					strOpsKind = Reader[0].ToString();//�������
					strDegree = Reader[1].ToString();//������ģ
					strCount = Reader[2].ToString();//����					
					
					//����ǰ�����ݼ���
					alDataRow.Add(strOpsKind);
					alDataRow.Add(strDegree);
					alDataRow.Add(strCount);
					al.Add(alDataRow.Clone());
				}
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport05";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			this.Reader.Close();
			return al;
		}
		/// <summary>
		/// �������ͳ����������(������ҽ�����ڲ���)
		/// </summary>
		/// <param name="BeginTime">��ʼʱ��</param>
		/// <param name="EndTime">��ֹʱ��</param>
		/// <returns>�������������������飨Ԫ��Ϊ��ӳ��������Ϣ�����飩</returns>
		[Obsolete("������������⣬������",true)]
		public ArrayList GetReport06(DateTime BeginTime,DateTime EndTime)
		{
			ArrayList al = new ArrayList();
			ArrayList alDataRow = new ArrayList();
			
			string strDeptName = string.Empty;
			string strDegree = string.Empty;
			string strCount = string.Empty;
			FS.HISFC.Models.Base.Employee employee =new FS.HISFC.Models.Base.Employee();
			employee.ID= this.Operator.ID;
			string strSql = string.Empty;

			if(this.Sql.GetSql("Operator.OpsReport.GetReport06",ref strSql) == -1) 
			{
				return null;
			}

			try
			{	
				strSql = string.Format(strSql,BeginTime.ToString(),EndTime.ToString(),employee.Dept.ID);
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport06";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			if (strSql == null) return null;
			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{					
					alDataRow.Clear();
					strDeptName = Reader[0].ToString();//����ҽ�����ڿ���
					strDegree = Reader[1].ToString();//������ģ
					strCount = Reader[2].ToString();//����					
					
					//����ǰ�����ݼ���
					alDataRow.Add(strDeptName);
					alDataRow.Add(strDegree);
					alDataRow.Add(strCount);
					al.Add(alDataRow.Clone());
				}
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport06";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			this.Reader.Close();
			return al;
		}
		/// <summary>
		/// �������ͳ����������(������ҽ�����ڲ���)  by  zlw 2006-5-17
		/// </summary>
		/// <param name="DeptID">ִ�п���</param>
		/// <param name="BeginTime">��ʼʱ��</param>
		/// <param name="EndTime">��ֹʱ��</param>
		/// <returns>�������������������飨Ԫ��Ϊ��ӳ��������Ϣ�����飩</returns>
		public ArrayList GetReport06(string DeptID,DateTime BeginTime,DateTime EndTime)
		{
			ArrayList al = new ArrayList();
			ArrayList alDataRow = new ArrayList();
			
			string strDeptName = string.Empty;
			string strDegree = string.Empty;
			string strCount = string.Empty;
//			FS.HISFC.Models.RADT.Person ps = (FS.HISFC.Models.RADT.Person)this.Operation;
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsReport.GetReport06",ref strSql) == -1) return null;
			try
			{	
				strSql = string.Format(strSql,BeginTime.ToString(),EndTime.ToString(),DeptID);
				
				//				strSql = string.Format(strSql,BeginTime.ToString(),EndTime.ToString(),ps.Dept.ID);
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport06";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			if (strSql == null) return null;
			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{					
					alDataRow.Clear();
					strDeptName = Reader[0].ToString();//����ҽ�����ڿ���
					strDegree = Reader[1].ToString();//������ģ
					strCount = Reader[2].ToString();//����					
					
					//����ǰ�����ݼ���
					alDataRow.Add(strDeptName);
					alDataRow.Add(strDegree);
					alDataRow.Add(strCount);
					al.Add(alDataRow.Clone());
				}
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport06";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			this.Reader.Close();
			return al;
		}

		
		/// <summary>
		/// �������ͳ����������(���������ڲ���)  by  cuip 2006-8
		/// </summary>
		/// <param name="DeptID">ִ�п���</param>
		/// <param name="BeginTime">��ʼʱ��</param>
		/// <param name="EndTime">��ֹʱ��</param>
		/// <returns>�������������������飨Ԫ��Ϊ��ӳ��������Ϣ�����飩</returns>
		public ArrayList GetReport06_1(string DeptID,DateTime BeginTime,DateTime EndTime)
		{
			ArrayList al = new ArrayList();
			ArrayList alDataRow = new ArrayList();
			
			string strDeptName = string.Empty;
			string strDegree = string.Empty;
			string strCount = string.Empty;
			//			FS.HISFC.Models.RADT.Person ps = (FS.HISFC.Models.RADT.Person)this.Operation;
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsReport.GetReport06_1",ref strSql) == -1) return null;
			try
			{	
				strSql = string.Format(strSql,BeginTime.ToString(),EndTime.ToString(),DeptID);
				
				//				strSql = string.Format(strSql,BeginTime.ToString(),EndTime.ToString(),ps.Dept.ID);
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport06";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			if (strSql == null) return null;
			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{					
					alDataRow.Clear();
					strDeptName = Reader[0].ToString();//����ҽ�����ڿ���
					strDegree = Reader[1].ToString();//������ģ
					strCount = Reader[2].ToString();//����					
					
					//����ǰ�����ݼ���
					alDataRow.Add(strDeptName);
					alDataRow.Add(strDegree);
					alDataRow.Add(strCount);
					al.Add(alDataRow.Clone());
				}
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport06";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			this.Reader.Close();
			return al;
		}

		/// <summary>
		/// �����������ͳ��(���Ա�)
		/// </summary>
		/// <param name="DeptID">���ұ���</param>
		/// <param name="BeginTime">��ʼʱ��</param>
		/// <param name="EndTime">��ֹʱ��</param>
		/// <returns>�������������������飨Ԫ��Ϊ��ӳ��������Ϣ�����飩</returns>
		public ArrayList GetReport07(string DeptID,DateTime BeginTime,DateTime EndTime)
		{
			ArrayList al = new ArrayList();
			ArrayList alDataRow = new ArrayList();
			FS.HISFC.Models.Base.SexEnumService sex = new FS.HISFC.Models.Base.SexEnumService();;
			string strCount = string.Empty;
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsReport.GetReport07",ref strSql) == -1) return null;
			try
			{	
				strSql = string.Format(strSql,DeptID,BeginTime.ToString(),EndTime.ToString());
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport07";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			if (strSql == null) return null;
			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{					
					alDataRow.Clear();
					sex.ID = Reader[0].ToString();
					strCount = Reader[1].ToString();
					//����ǰ�����ݼ���
					alDataRow.Add(sex.Name);
					alDataRow.Add(strCount);
					al.Add(alDataRow.Clone());
				}
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport07";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			this.Reader.Close();
			return al;
		}
		/// <summary>
		/// �����������ͳ��(����ǰ���)
		/// </summary>
		/// <param name="DeptID">���ұ���</param>
		/// <param name="BeginTime">��ʼʱ��</param>
		/// <param name="EndTime">��ֹʱ��</param>
		/// <returns>�������������������飨Ԫ��Ϊ��ӳ��������Ϣ�����飩</returns>
		public ArrayList GetReport08(string DeptID,DateTime BeginTime,DateTime EndTime)
		{
			ArrayList al = new ArrayList();
			ArrayList alDataRow = new ArrayList();
			string strDiagnose = string.Empty;
			string strCount = string.Empty;
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsReport.GetReport08",ref strSql) == -1) return null;
			try
			{	
				strSql = string.Format(strSql,DeptID,BeginTime.ToString(),EndTime.ToString(),"1");
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport08";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			if (strSql == null) return null;
			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{					
					alDataRow.Clear();
					strDiagnose = Reader[0].ToString();
					strCount = Reader[1].ToString();
					//����ǰ�����ݼ���
					alDataRow.Add(strDiagnose);
					alDataRow.Add(strCount);
					al.Add(alDataRow.Clone());
				}
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport08";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			this.Reader.Close();
			return al;
		}
		/// <summary>
		/// �����������ͳ��(����������)
		/// </summary>
		/// <param name="DeptID">���ұ���</param>
		/// <param name="BeginTime">��ʼʱ��</param>
		/// <param name="EndTime">��ֹʱ��</param>
		/// <returns>�������������������飨Ԫ��Ϊ��ӳ��������Ϣ�����飩</returns>
		public ArrayList GetReport09(string DeptID,DateTime BeginTime,DateTime EndTime)
		{
			ArrayList al = new ArrayList();
			ArrayList alDataRow = new ArrayList();
			string strItem = string.Empty;
			string strCount = string.Empty;
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsReport.GetReport09",ref strSql) == -1) return null;
			try
			{	
				strSql = string.Format(strSql,DeptID,BeginTime.ToString(),EndTime.ToString(),"1");
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport09";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			if (strSql == null) return null;
			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{					
					alDataRow.Clear();
					strItem = Reader[0].ToString();
					strCount = Reader[1].ToString();
					//����ǰ�����ݼ���
					alDataRow.Add(strItem);
					alDataRow.Add(strCount);
					al.Add(alDataRow.Clone());
				}
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport09";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			this.Reader.Close();
			return al;
		}
		/// <summary>
		/// �����������ͳ��(������ʽ)
		/// </summary>
		/// <param name="DeptID">���ұ���</param>
		/// <param name="BeginTime">��ʼʱ��</param>
		/// <param name="EndTime">��ֹʱ��</param>
		/// <returns>�������������������飨Ԫ��Ϊ��ӳ��������Ϣ�����飩</returns>
		public ArrayList GetReport10(string DeptID,DateTime BeginTime,DateTime EndTime)
		{
			ArrayList al = new ArrayList();
			ArrayList alDataRow = new ArrayList();
			string strAnaeType = string.Empty;
			string strCount = string.Empty;
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsReport.GetReport10",ref strSql) == -1) return null;
			try
			{	
				strSql = string.Format(strSql,DeptID,BeginTime.ToString(),EndTime.ToString(),"1");
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport10";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			if (strSql == null) return null;
			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{					
					alDataRow.Clear();
					strAnaeType = Reader[0].ToString();
					strCount = Reader[1].ToString();
					//����ǰ�����ݼ���
					alDataRow.Add(strAnaeType);
					alDataRow.Add(strCount);
					al.Add(alDataRow.Clone());
				}
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport10";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			this.Reader.Close();
			return al;
		}
		/// <summary>
		/// ��ҽ���������ͳ����������
		/// Robin �޸ģ����÷����ѱ�
		/// </summary>
		/// <param name="BeginTime">��ʼʱ��</param>
		/// <param name="EndTime">��ֹʱ��</param>
		/// <returns>�������������������飨Ԫ��Ϊ��ӳ��������Ϣ�����飩</returns>
		public ArrayList GetReport11(string DeptID,DateTime BeginTime,DateTime EndTime)
		{
			ArrayList al = new ArrayList();
			ArrayList alDataRow = new ArrayList();			 
			string strCount = string.Empty;
			string personID = string.Empty;
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsReport.GetReport11",ref strSql) == -1) 
				return null;

			try
			{	
				strSql = string.Format(strSql,DeptID,BeginTime.ToString(),EndTime.ToString());
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport11";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			
			if (strSql == null) 
				return null;

			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{
                    alDataRow = new ArrayList();
                    personID = Reader[0].ToString();
                    string deptName = Reader[1].ToString();
                    strCount = Reader[2].ToString(); 
                    alDataRow.Add(personID); 
                    alDataRow.Add(deptName);
                    alDataRow.Add(strCount);
					al.Add(alDataRow);
				}
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport11";
				this.ErrCode = ex.Message;
				this.WriteErr();
				this.Reader.Close();
				return null;            
			}
			this.Reader.Close();
			return al;
		}
		/// <summary>
		/// ����PACU�Ĳ���ͳ��
		/// </summary>
		/// <param name="DeptID">���ұ���</param>
		/// <param name="BeginTime">��ʼʱ��</param>
		/// <param name="EndTime">��ֹʱ��</param>
		/// <returns>�������������������飨Ԫ��Ϊ��ӳ��������Ϣ�����飩</returns>
		public ArrayList GetReport12(string DeptID,DateTime BeginTime,DateTime EndTime)
		{
			ArrayList al = new ArrayList();
			ArrayList alDataRow = new ArrayList();
			FS.HISFC.Models.Base.SexEnumService sex = new FS.HISFC.Models.Base.SexEnumService();
			string strCount = string.Empty;
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsReport.GetReport12",ref strSql) == -1) return null;
			try
			{	
				strSql = string.Format(strSql,DeptID,BeginTime.ToString(),EndTime.ToString());
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport12";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}

			if (strSql == null) 
				return null;

			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{					
					alDataRow.Clear();
					sex.ID = Reader[0].ToString();
					strCount = Reader[1].ToString();
					//����ǰ�����ݼ���
					alDataRow.Add(sex.Name);
					alDataRow.Add(strCount);
					al.Add(alDataRow.Clone());
				}
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport12";
				this.ErrCode = ex.Message;
				this.WriteErr();
				this.Reader.Close();
				return null;
			}
			this.Reader.Close();
			return al;
		}
		/// <summary>
		/// ������״���������ͳ�ƽ���PACU����������
		/// </summary>
		/// <param name="DeptID">���ұ���</param>
		/// <param name="BeginTime">��ʼʱ��</param>
		/// <param name="EndTime">��ֹʱ��</param>
		/// <returns>�������������������飨Ԫ��Ϊ��ӳ��������Ϣ�����飩</returns>
		public ArrayList GetReport13(string DeptID,DateTime BeginTime,DateTime EndTime)
		{
			ArrayList al = new ArrayList();
			ArrayList alDataRow = new ArrayList();
			string strStatus = string.Empty;
			string strCount = string.Empty;
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsReport.GetReport13",ref strSql) == -1) return null;
			try
			{	
				strSql = string.Format(strSql,DeptID,BeginTime.ToString(),EndTime.ToString());
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport13";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			if (strSql == null) return null;
			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{					
					alDataRow.Clear();
					strStatus = Reader[0].ToString();
					strCount = Reader[1].ToString();
					//����ǰ�����ݼ���
					alDataRow.Add(strStatus);
					alDataRow.Add(strCount);
					al.Add(alDataRow.Clone());
				}
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport13";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			this.Reader.Close();
			return al;
		}
		/// <summary>
		/// ������״���������ͳ�ƽ���PACU����������
		/// </summary>
		/// <param name="DeptID">���ұ���</param>
		/// <param name="BeginTime">��ʼʱ��</param>
		/// <param name="EndTime">��ֹʱ��</param>
		/// <returns>�������������������飨Ԫ��Ϊ��ӳ��������Ϣ�����飩</returns>
		public ArrayList GetReport14(string DeptID,DateTime BeginTime,DateTime EndTime)
		{
			ArrayList al = new ArrayList();
			ArrayList alDataRow = new ArrayList();
			string strStatus = string.Empty;
			string strCount = string.Empty;
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsReport.GetReport14",ref strSql) == -1) return null;
			try
			{	
				strSql = string.Format(strSql,DeptID,BeginTime.ToString(),EndTime.ToString());
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport14";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			if (strSql == null) return null;
			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{					
					alDataRow.Clear();
					strStatus = Reader[0].ToString();
					strCount = Reader[1].ToString();
					//����ǰ�����ݼ���
					alDataRow.Add(strStatus);
					alDataRow.Add(strCount);
					al.Add(alDataRow.Clone());
				}
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport14";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			this.Reader.Close();
			return al;
		}
		/// <summary>
		/// ����̨����ͳ�Ʊ�
		/// </summary>
		/// <param name="DeptID"></param>
		/// <param name="BeginTime"></param>
		/// <param name="EndTime"></param>
		/// <param name="opsQty1"></param>
		/// <param name="opsQty2"></param>
		/// <returns></returns>
		public int GetReport20(string DeptID,DateTime BeginTime,DateTime EndTime,ref int opsQty1,ref int opsQty2)
		{
			string strSql=string.Empty;
			int Qty1=0,Qty2=0;

			if(Sql.GetSql("Operator.OpsReport.GetReport20",ref strSql)==-1)return -1;
			try
			{
				strSql=string.Format(strSql,BeginTime.ToString(),EndTime.ToString(),DeptID);
				if(ExecQuery(strSql)==-1)return -1;
				while(Reader.Read())
				{
					decimal cost=FS.FrameWork.Function.NConvert.ToDecimal(Reader[0].ToString());
					if(cost>=1000)
						Qty1++;
					else
						Qty2++;
				}
				Reader.Close();
			}
			catch(Exception e)
			{
				this.Err="Operator.OpsReport.GetReport20!"+e.Message;
				this.ErrCode=e.Message;
				WriteErr();
				return -1;
			}
			
			opsQty1=Qty1;
			opsQty2=Qty2;
			return 0;
		}
		/// <summary>
		/// �����������������ȡ��������
		/// </summary>
		/// <param name="BeginTime">��ʼʱ��</param>
		/// <param name="EndTime">��ֹʱ��</param>
		/// <returns>�������������������飨Ԫ��Ϊ��ӳ��������Ϣ�����飩</returns>
		public ArrayList GetReport21(string DeptID,DateTime BeginTime,DateTime EndTime)
		{
			ArrayList al = new ArrayList();
			
			//FS.HISFC.BizLogic.Manager.Department objdept = new FS.HISFC.BizLogic.Manager.Department();
			
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsReport.GetReport21",ref strSql) == -1) return null;
			try
			{	
				strSql = string.Format(strSql,BeginTime.ToString(),EndTime.ToString(),DeptID);
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport21";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			if (strSql == null) return null;
			if(this.ExecQuery(strSql)==-1)return null;
			try
			{
				while(this.Reader.Read())
				{					
					FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
					
					obj.ID=Reader[0].ToString();
					obj.Memo=Reader[1].ToString();
					FS.HISFC.Models.Base.Department dept=this.GetDeptmentById(obj.ID);
					if(dept==null)
						obj.Name=obj.ID+"(δ֪)";
					else
						obj.Name=dept.Name;
					//����ǰ�����ݼ���
					al.Add(obj);
				}
			}
			catch(Exception ex)
			{
				this.Err = "Operator.OpsReport.GetReport21";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return null;            
			}
			this.Reader.Close();
			return al;
		}
		/// <summary>
		/// ������Ϣһ����
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
        public DataSet GetPersonOperation(string DeptID, DateTime Begin, DateTime End)
		{
			//������Ϣһ����
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = string.Empty;
				if(this.Sql.GetSql("Operator.OpsReport.GetPersonOperation",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,DeptID,Begin.ToString(),End.ToString());
				}

				this.ExecQuery(strSql,ref ds);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
			return ds;
		}
		
		/// <summary>
		/// ���� ��Ⱦ ������Ϣһ����
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
		public DataSet GetEmergencyOperation (string Begin,string End)
		{
			//������Ϣһ����
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = string.Empty;
                FS.HISFC.Models.Base.Employee ee = (FS.HISFC.Models.Base.Employee)this.Operator;
				if(this.Sql.GetSql("Operator.OpsReport.GetEmergencyOperation",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
                    strSql = string.Format(strSql, Begin, End, ee.Dept.ID);
				}

				this.ExecQuery(strSql,ref ds);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
			return ds;
		}
		
		/// <summary>
		/// ����������������ѯ
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
		public DataSet GetSpecalOperationReport (string DeptID,DateTime Begin,DateTime End)
		{
			//FS.HISFC.Models.RADT.Person ps = new FS.HISFC.Models.RADT.Person();
			//FS.HISFC.BizLogic.Manager.Person p = new FS.HISFC.BizLogic.Manager.Person();
			//ps = this.Operation as FS.HISFC.Models.RADT.Person; 
//			if(ps == null)
//			{
//				this.Err = "��ѯ��Ա��Ϣ����";
//				return null;
//			}
			//������Ϣһ����
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = string.Empty;
				if(this.Sql.GetSql("Operator.OpsReport.GetReportSpecal",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End,DeptID);
				}

				this.ExecQuery(strSql,ref ds);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
			return ds;
		}
		
		/// <summary>
		/// ����������������ѯ
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
//		public DataSet GetSpecalOperationReport (DateTime Begin,DateTime End)
//		{
//			FS.HISFC.Models.RADT.Person ps = new FS.HISFC.Models.RADT.Person();
//			//FS.HISFC.BizLogic.Manager.Person p = new FS.HISFC.BizLogic.Manager.Person();
//			ps = this.Operator as FS.HISFC.Models.RADT.Person; 
//			if(ps == null)
//			{
//				this.Err = "��ѯ��Ա��Ϣ����";
//				return null;
//			}
//			//������Ϣһ����
//			System.Data.DataSet  ds = new DataSet();
//			try
//			{
//				string strSql = string.Empty;
//				if(this.Sql.GetSql("Operator.OpsReport.GetReportSpecal",ref strSql) ==-1)
//				{
//					this.Err = this.Sql.Err;
//					return null;
//				}
//				else
//				{
//					strSql = string.Format(strSql,Begin,End,ps.Dept.ID);
//				}
//
//				this.ExecQuery(strSql,ref ds);
//			}
//			catch(Exception ee)
//			{
//				this.Err = ee.Message;
//				return null;
//			}
//			return ds;
//		}
//		
		/// <summary>
		/// ��ѯĳ��ʱ�����Ա���Ű���Ϣ���
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
//		public DataSet GetEmployReport (string Begin,string End,string EmployCode)
//		{
//			FS.HISFC.Models.RADT.Person ps = new FS.HISFC.Models.RADT.Person();
//			FS.HISFC.BizLogic.Manager.Person p = new FS.HISFC.BizLogic.Manager.Person();
//			ps = p.GetPersonByID(EmployCode); //
//			if(ps == null)
//			{
//				this.Err = "��ѯ��Ա��Ϣ����";
//				return null;
//			}
//			//������Ϣһ����
//			System.Data.DataSet  ds = new DataSet();
//			try
//			{
//				string strSql = string.Empty;
//				if(this.Sql.GetSql("Operator.OpsReport.GetEmployReport",ref strSql) ==-1)
//				{
//					this.Err = this.Sql.Err;
//					return null;
//				}
//				else
//				{
//					strSql = string.Format(strSql,Begin,End,ps.Dept.ID,EmployCode);
//				}
//
//				this.ExecQuery(strSql,ref ds);
//			}
//			catch(Exception ee)
//			{
//				this.Err = ee.Message;
//				return null;
//			}
//			return ds;
//		}

		#endregion

        protected abstract FS.HISFC.Models.Base.Department GetDeptmentById(string id);
        protected abstract FS.HISFC.Models.Base.Employee GetEmployee(string id);
	}
}
