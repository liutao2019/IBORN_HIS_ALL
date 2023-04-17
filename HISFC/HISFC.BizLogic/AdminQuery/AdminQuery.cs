using System;
using System.Collections;
using System.Xml;
namespace FS.HISFC.BizLogic.AdminQuery
{
	/// <summary>
	/// Ժ����ѯ
	/// �̳���DataBase��
	/// [�޸ļ�¼]
	/// 2006-11-7
	/// ���Ժ����ѯ��Ա����Ϣ��
	/// </summary>
    public class AdminQuery : FS.FrameWork.Management.Database
	{
		/// <summary>
		/// ���ź���
		/// </summary>
		public AdminQuery()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		protected const string index = "AdministratorQuery";
		/// <summary>
		/// ArrayList 
		/// �������
		/// </summary>
		/// <returns></returns>
		public ArrayList GetListItems(string SqlID,string EmplID, string[] Params) 
		{
			//Filled By FS.HISFC.Object.Base.Department
			string sql = "";
			if(this.Sql.GetSql(SqlID,ref sql)== -1)
				return null;

			sql = sql.Replace("[��¼��Ա����]", EmplID);
			if(Params != null)
			{
				for(int i=0;i<Params.Length;i+=2)
				{
					sql = sql.Replace(Params[i],Params[i+1]);
				}
			}

			return this.myGetListItems(sql);
		}
		/// <summary>
		/// ����sql���ȡ�����б�
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		private ArrayList myGetListItems(string sql) 
		{
			if(this.ExecQuery(sql) == -1) return null;

			ArrayList result = new ArrayList();   
			while(this.Reader.Read()) 
			{
				FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
				obj.ID = (string)this.Reader[0];
				if(this.Reader[1] == null || this.Reader[1] == System.DBNull.Value)
				{
					obj.Name = "(�հ�)";
				}
				else
				{
					obj.Name = (string)this.Reader[1];
				}
				result.Add(obj);
			}
			this.Reader.Close();
			return result;
		}
		/// <summary>
		/// ������SQL
		/// ����NeuObject
		/// </summary>
		/// <returns></returns>
		public ArrayList GetSQL()
		{
			ArrayList al = new ArrayList();
			if(this.Sql == null) 
			{
				this.Err = "û������sql";
				return null;
			}
			foreach(FS.FrameWork.Models.NeuObject  info in this.Sql.alSql)
			{
				//				if(info.ID.IndexOf(index)>=0)
				if(info.ID.ToLower().StartsWith(index.ToLower()))
					al.Add(info);
			}
			return al;
		}
		
		/// <summary>
		/// ���sql����Ĳ�����Ϣ����
		/// [������]
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public object[] GetParams(string sql)
		{
			int start = 0 ,end =0;
			ArrayList al =new ArrayList();
			string s = "";
			al.Add("��");
			while(1 == 1)
			{
				start = sql.IndexOf("[",end);
				end = sql.IndexOf("]",end);
				if(start<0 || end < 0) break;
				s = sql.Substring(start+1,end - start-1);
				s = "["+s+"]";
				al.Add(s);
				end++;
			}
			return al.ToArray();	
		}
		
		/// <summary>
		/// ��ò�ѯ���ֶ�
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public object[] GetFields(string sql)
		{
			ArrayList al =new ArrayList();
			object[] param = this.GetParams(sql);
			for(int j=0;j<param.Length;j++)
			{
				sql = sql.Replace(param[j].ToString(),System.DateTime.Now.ToString());
			}

			System.Data.OracleClient.OracleDataAdapter adapter = new System.Data.OracleClient.OracleDataAdapter(sql,this.con.ConnectionString);
			System.Data.DataSet ds = new System.Data.DataSet();
			adapter.Fill(ds,0,0,"table");
			if(ds==null) return al.ToArray();
			for(int i=0;i<ds.Tables[0].Columns.Count;i++)
			{
				al.Add(ds.Tables[0].Columns[i].Caption);
			}
			return al.ToArray();
		}
        static string strConn;
		/// <summary>
		/// ִ��sql
		/// </summary>
		/// <param name="strSql"></param>
		/// <param name="strDataSet"></param>
		/// <param name="strXSLFileName"></param>
		/// <param name="SettingXml"></param>
		/// <returns></returns>
        public int ExecQuery(string strSql, ref string strDataSet, string strXSLFileName, string SettingXml)
        {
            System.Data.OracleClient.OracleConnection con = new System.Data.OracleClient.OracleConnection(strConn);
            System.Data.OracleClient.OracleCommand command = new System.Data.OracleClient.OracleCommand();
            command.Connection = con;
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Clear();
            command.CommandText = strSql + "";
            try
            {
                System.Data.OracleClient.OracleDataReader TempReader1;
                TempReader1 = command.ExecuteReader();
                XmlDocument doc = new XmlDocument();
                XmlNode root;
                XmlElement node, row;
                doc.AppendChild(doc.CreateXmlDeclaration("1.0", "GB2312", ""));
                if (strXSLFileName != null && strXSLFileName != "")
                {
                    string PI = "type='text/xsl' href='" + strXSLFileName + "'";
                    System.Xml.XmlProcessingInstruction xmlProcessingInstruction = doc.CreateProcessingInstruction("xml-stylesheet", PI);
                    doc.AppendChild(xmlProcessingInstruction);
                }
                string Header = doc.OuterXml + "\n<DataSet>\n" + SettingXml;
                doc = new XmlDocument();
                root = doc.CreateElement("Table");
                doc.AppendChild(root);
                while (TempReader1.Read())
                {
                    row = doc.CreateElement("Row");
                    for (int i = 0; i < TempReader1.FieldCount; i++)
                    {
                        node = doc.CreateElement("Column");
                        node.SetAttribute("Name", TempReader1.GetName(i).ToString());
                        node.InnerText = TempReader1[i].ToString() + "";
                        row.AppendChild(node);
                    }
                    root.AppendChild(row);
                }
                strDataSet = Header + "\n" + doc.OuterXml + "\n</DataSet>";
                TempReader1.Close();
            }
            catch (Exception ex)
            {
                this.Err = "ִ������������!" + ex.Message;
                this.ErrorException = ex.InnerException + "+ " + ex.Source;
                this.ErrCode = strSql;
                this.WriteErr();
                return -1;
            }

            WriteDebug("ִ�в�ѯsql��䣡" + strSql);
            return 0;
        }

		#region �����Һ�״̬��ѯ������Ϣ�б�
		/// <summary>
		/// ���߲�ѯ-����������Ժ״̬������ѯȫԺ���л���
		/// </summary>
		/// <param name="State">סԺ״̬</param>
		/// <returns></returns>
		public ArrayList PatientQueryByState(FS.HISFC.Models.RADT.InStateEnumService State) 
		{
			#region �ӿ�˵��
			//RADT.Inpatient.PatientQuery.where.6
			//���룺���ұ��룬סԺ״̬
			//������������Ϣ
			#endregion
			ArrayList al=new ArrayList();
			string sql1="",sql2="";
			sql1 = PatientQuerySelect();
			if (sql1==null ) return null;
			
			if(this.Sql.GetSql("RADT.Inpatient.PatientQuery.WebQuery.ByState",ref sql2)==-1) 
			{
				this.Err="û���ҵ�RADT.Inpatient.PatientQuery.WebQuery.ByState�ֶ�!";
				this.ErrCode="-1";
				this.WriteErr();
				return null;
			}
			sql2=" "+string.Format(sql2,State.ID.ToString());
			return this.myPatientQuery(sql1+sql2);
		}

		/// <summary>
		/// ���߲�ѯ-�������뻼��סԺ�Ų�����ѯȫԺ���л���
		/// </summary>
		/// <param name="ID">סԺ��</param>
		/// <returns></returns>
		public ArrayList PatientQueryByID(string ID) 
		{
			#region �ӿ�˵��
			//RADT.Inpatient.PatientQuery.where.6
			//���룺���ұ��룬סԺ״̬
			//������������Ϣ
			#endregion
			ArrayList al=new ArrayList();
			string sql1="",sql2="";
			sql1 = PatientQuerySelect();
			if (sql1==null ) return null;
			
			if(this.Sql.GetSql("RADT.Inpatient.PatientQuery.WebQuery.ByID",ref sql2)==-1) 
			{
				this.Err="û���ҵ�RADT.Inpatient.PatientQuery.WebQuery.ByID�ֶ�!";
				this.ErrCode="-1";
				this.WriteErr();
				return null;
			}
			sql2=" "+string.Format(sql2,ID);
			return this.myPatientQuery(sql1+sql2);
		}

		/// <summary>
		/// ���߲�ѯ-�������뻼������������ѯȫԺ���л���
		/// </summary>
		/// <param name="Name">��������</param>
		/// <returns></returns>
		public ArrayList PatientQueryByName(string Name) 
		{
			#region �ӿ�˵��
			//RADT.Inpatient.PatientQuery.where.6
			//���룺���ұ��룬סԺ״̬
			//������������Ϣ
			#endregion
			ArrayList al=new ArrayList();
			string sql1="",sql2="";
			sql1 = PatientQuerySelect();
			if (sql1==null ) return null;
			
			if(this.Sql.GetSql("RADT.Inpatient.PatientQuery.WebQuery.ByName",ref sql2)==-1) 
			{
				this.Err="û���ҵ�RADT.Inpatient.PatientQuery.WebQuery.ByName�ֶ�!";
				this.ErrCode="-1";
				this.WriteErr();
				return null;
			}
			sql2=" "+string.Format(sql2,Name);
			return this.myPatientQuery(sql1+sql2);
		}

		/// <summary>
		/// ���߲�ѯ-����������Ժʱ�������ѯȫԺ���л���
		/// </summary>
		/// <param name="BeginDate">��ѯ��ʼʱ��</param>
		/// <param name="EndDate">��ѯ����ʱ��</param>
		/// <returns></returns>
		public ArrayList PatientQueryByInDate(string BeginDate, string EndDate) 
		{
			#region �ӿ�˵��
			//RADT.Inpatient.PatientQuery.where.6
			//���룺���ұ��룬סԺ״̬
			//������������Ϣ
			#endregion
			ArrayList al=new ArrayList();
			string sql1="",sql2="";
			sql1 = PatientQuerySelect();
			if (sql1==null ) return null;
			
			if(this.Sql.GetSql("RADT.Inpatient.PatientQuery.WebQuery.ByInDate",ref sql2)==-1) 
			{
				this.Err="û���ҵ�RADT.Inpatient.PatientQuery.WebQuery.ByInDate�ֶ�!";
				this.ErrCode="-1";
				this.WriteErr();
				return null;
			}
			sql2=" "+string.Format(sql2,BeginDate, EndDate);
			return this.myPatientQuery(sql1+sql2);
		}

		/// <summary>
		/// ���߲�ѯ-����������Ҳ�����ѯȫԺ���л���
		/// </summary>
		/// <param name="Dept">���ұ���</param>
		/// <returns></returns>
		public ArrayList PatientQueryByDept(string Dept) 
		{
			#region �ӿ�˵��
			//RADT.Inpatient.PatientQuery.where.6
			//���룺���ұ��룬סԺ״̬
			//������������Ϣ
			#endregion
			ArrayList al=new ArrayList();
			string sql1="",sql2="";
			sql1 = PatientQuerySelect();
			if (sql1==null ) return null;
			
			if(this.Sql.GetSql("RADT.Inpatient.PatientQuery.WebQuery.ByDept",ref sql2)==-1) 
			{
				this.Err="û���ҵ�RADT.Inpatient.PatientQuery.WebQuery.ByDept�ֶ�!";
				this.ErrCode="-1";
				this.WriteErr();
				return null;
			}
			sql2=" "+string.Format(sql2,Dept);
			return this.myPatientQuery(sql1+sql2);
		}

//		/// <summary>
//		/// ���߲�ѯ-��ѯ���Ҳ�ͬ״̬�Ļ���
//		/// </summary>
//		/// <param name="dept_code">���ұ���</param>
//		/// <param name="State">סԺ״̬</param>
//		/// <returns></returns>
//		public ArrayList PatientQuery(string dept_code,Object.RADT.VisitStatus State) 
//		{
//			#region �ӿ�˵��
//			//RADT.Inpatient.PatientQuery.where.6
//			//���룺���ұ��룬סԺ״̬
//			//������������Ϣ
//			#endregion
//			ArrayList al=new ArrayList();
//			string sql1="",sql2="";
//			sql1 = PatientQuerySelect();
//			if (sql1==null ) return null;
//			
//			if(this.Sql.GetSql("RADT.Inpatient.PatientQuery.Where.9",ref sql2)==-1) 
//			{
//				this.Err="û���ҵ�RADT.Inpatient.PatientQuery.Where.9�ֶ�!";
//				this.ErrCode="-1";
//				this.WriteErr();
//				return null;
//			}
//			sql2=" "+string.Format(sql2,State.ID.ToString());
//			return this.myPatientQuery(sql1+sql2);
//		}
		#endregion

		#region �������Sql����ѯ������Ϣ�б�--˽��
		private ArrayList myPatientQuery(string SqlOperator) 
		{
			ArrayList al=new ArrayList();
			FS.FrameWork.Models.NeuObject PatientInfo = new FS.FrameWork.Models.NeuObject();
			this.ProgressBarText="���ڲ�ѯ����...";
			this.ProgressBarValue=0;

			if(this.ExecQuery(SqlOperator) == -1) return null;
			//ȡϵͳʱ��,�����õ������ַ���
			DateTime sysDate = this.GetDateTimeFromSysDateTime();

			try 
			{
				while (this.Reader.Read()) 
				{
					PatientInfo=new FS.FrameWork.Models.NeuObject();
					#region "�ӿ�˵��"
					//<!-- 0  סԺ��ˮ��,1 ���� ,2   סԺ��   ,3 ���￨��  ,4  ������, 5  ҽ��֤��
					//,6    ҽ�����,   7   �Ա�   ,8   ���֤��  ,9   ƴ��     ,10  ����
					//,11   ְҵ����     ,12 ְҵ����,13   ������λ    ,14   ������λ�绰      ,15   ��λ�ʱ�
					//,16   ���ڻ��ͥ��ַ     ,17   ��ͥ�绰   ,18   ���ڻ��ͥ�ʱ�   , 19  ����id,20  ����name
					//,21   �����ش���    , 22 ����������   ,23   ����id    ,24  ����name    ,25   ��ϵ��id
					//,26   ��ϵ������    ,27   ��ϵ�˵绰       ,28   ��ϵ�˵�ַ     ,29   ��ϵ�˹�ϵid , 30   ��ϵ�˹�ϵname
					//,31   ����״��id    ,32  ����״��name  ,33   ����id    , 34 ��������
					//,35   ���           ,36   ����         ,37   Ѫѹ      ,38   ABOѪ��
					//,39   �ش󼲲���־    ,40   ������־            
					//,41   ��Ժ����      ,42   ���Ҵ���   , 43  ��������  , 44  �������id 1-�Է�  2-���� 3-������ְ 4-�������� 5-���Ѹ߸�
					//,45   �����������   , 46 ��ͬ����   , 47  ��ͬ��λ����  , 48  ����
					//, 49 ����Ԫ����  , 50  ����Ԫ����, 51 ҽʦ����(סԺ), 52 ҽʦ����(סԺ)
					//, 53 ҽʦ����(����) , 54 ҽʦ����(����) , 55 ҽʦ����(����) , 56 ҽʦ����(����)
					//, 57 ҽʦ����(ʵϰ) , 58 ҽʦ����(ʵϰ), 59  ��ʿ����(����), 60  ��ʿ����(����)
					//, 61  ��Ժ���id  , 62  ��Ժ���name   , 63  ��Ժ;��id    , 64  ��Ժ;��name      
					//, 65  ��Ժ��Դid 1 -���� 2 -���� 3 -ת�� 4 -תԺ    , 66  ��Ժ��Դname
					//, 67  ��Ժ״̬ סԺ�Ǽ�  i-�������� -��Ժ�Ǽ� o-��Ժ���� p-ԤԼ��Ժ n-�޷���Ժ
					//,  68  ��Ժ����(ԤԼ)  , 69  ��Ժ���� , 70  �Ƿ���ICU 0 no 1 yes,71 icu code,72 icu name
					//,73 ¥ ,74 ��,75 �� 
					//,76 �ܹ����tot_cost ,77 �Էѽ�� own_cost,	78 �Ը���� Pay_Cost,79 ���ѽ�� Pub_Cost
					//,80 ʣ���� Left_Cost,81 �Żݽ��
					//,82  Ԥ����� ��83    ���ý��(�ѽ�)��84    Ԥ�����(�ѽ�) �� 85 ��������(�ϴ�)     
					//��86 ������, 87 ת�����,88 ChangePrepay ת��Ԥ����δ��)  ,89 ת����ý��(δ��),90 ����״̬91�������޶�겿��
					//,92 ��ע93�������޶�94Ѫ���ɽ�95סԺ����96��λ����97�յ�����98�������99��סҽʦ100�������յ��Ժ�
					//-->
					#endregion
					try 
					{
						if(!this.Reader.IsDBNull(0)) PatientInfo.ID = this.Reader[0].ToString();// סԺ��ˮ��
						if(!this.Reader.IsDBNull(1)) PatientInfo.Name =this.Reader[1].ToString();//����
						if(!this.Reader.IsDBNull(2)) PatientInfo.Memo =this.Reader[2].ToString();//  סԺ��						
						if(!this.Reader.IsDBNull(3)) PatientInfo.User01 =FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[3]).ToLongDateString();//  ��Ժ����
						if(!this.Reader.IsDBNull(4)) PatientInfo.User02 =this.Reader[4].ToString();//  ���Ҵ���
						if(!this.Reader.IsDBNull(5)) PatientInfo.User03 =this.Reader[5].ToString();// ��������
					}
					catch(Exception ex) 
					{ 
						this.Err=ex.Message;
						this.WriteErr();
						if(!Reader.IsClosed)
						{
							Reader.Close();
						}
						return null;
					}
					//��ñ����Ϣ
					#region "��ñ����Ϣ"
					//deleted by cuipeng 2005-5 ��֪���˹���Ϊɶ��,����������
					//this.myGetTempLocation(PatientInfo);
					#endregion
					this.ProgressBarValue++;
					al.Add(PatientInfo);
				}
			}//�׳�����
			catch(Exception ex) 
			{
				this.Err="��û��߻�����Ϣ����"+ex.Message;
				this.ErrCode="-1";
				this.WriteErr();
				if(!Reader.IsClosed)
				{
					Reader.Close();
				}
				return al;
			}
			this.Reader.Close();
			
			this.ProgressBarValue=-1;
			return al;
		}
		#endregion		

		#region ��������ѯ������Ϣ�б�
		/// ��ѯ������Ϣ��select��䣨��where������
		private string PatientQuerySelect() 
		{
			#region �ӿ�˵��
			//RADT.Inpatient.PatientQuery.select.1
			//���룺0
			//������sql.select
			#endregion
			string sql="";
			if(this.Sql.GetSql("RADT.Inpatient.PatientQuery.WebQuery.Select",ref sql)==-1) 
			{
				this.Err="û���ҵ�RADT.Inpatient.PatientQuery.WebQuery.Select�ֶ�!";
				this.ErrCode="-1";
				this.WriteErr();
				return null;
			}
			return sql;
		}
		#endregion

		/// <summary>
		/// ����û�TreeList
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public ArrayList GetUserTreeList(string userID)
		{
			return null;
		}

        //public string GetDate(string sid)
        //{
        //    QueryResult result = new QueryResult();
        //    return result.GetDate(sid);
        //}

		#region Ժ����ѯ��Ա��
		/// <summary>
		/// ɾ��Ժ����ѯ��Ա��Ϣ
		/// </summary>
		/// <param name="Empl_Code"></param>
		/// <returns></returns>
		public int DeleteAQOperator(string Empl_Code) 
		{
			string sql = "";
			if(this.Sql.GetSql("AdminQuery.AQPermission.Delete",ref sql)== -1)
				return -1;

		 
			try 
			{
				sql=string.Format(sql, Empl_Code);
			}
			catch(Exception ex) 
			{
				this.ErrCode=ex.Message;
				this.Err="�ӿڴ���"+ex.Message;
				this.WriteErr();
				return -1;
			}

			if(this.ExecNoQuery(sql) == -1) return -1;

			return 1;
		}

		/// <summary>
		/// ����Ժ����ѯ��Ա��Ϣ
		/// </summary>
		/// <param name="info">����ʵ��</param>
		/// <returns></returns>
		public int InsertAQPermission(FS.FrameWork.Models.NeuObject info) 
		{
			
			string sql = "";
			if(this.Sql.GetSql("AdminQuery.AQPermission.Insert",ref sql)== -1)
				return -1;
//			insert into his_role_aqOperator
//			(PARENT_CODE,    --VARCHAR2(6)                   ���������   
//			GROUP_CODE ,    --VARCHAR2(6)                   ���������   
//			EMPL_CODE,      --VARCHAR2(6)                   Ա������     
//			LOGIN_PASSWORD, --VARCHAR2(8)  Y                ��¼��Ա���� 
//			LOGIN_NAME,     --VARCHAR2(20)                  Ա����¼���� 
//			MANAGER_FLAG,   --VARCHAR2(1)  Y                ����Ա��־   
//			GROUP_NAME,     --VARCHAR2(30) Y                ����������   
//			PARENT_NAME ,   --VARCHAR2(30) Y                ����������   
//			EMPL_NAME,      --VARCHAR2(10) Y                Ա������     
//			OWNER_FLAG)     --VARCHAR2(1)  Y        0        
//			values('{0}','{0}', '{1}', '{2}','{3}','0','{4}','{4}','{5}','0')
			try 
			{
				sql=string.Format(sql, info.User02, info.ID, info.User01, info.Memo, info.User03, info.Name);
				
			}
			catch(Exception ex) 
			{
				this.ErrCode=ex.Message;
				this.Err="�ӿڴ���"+ex.Message;
				this.WriteErr();
				return -1;
			}

			if(this.ExecNoQuery(sql) == -1) return -1;


			return 1;
		}

		/// <summary>
		/// ��ò�ѯ��Ա�б�
		/// </summary>
		/// <returns></returns>
		public ArrayList GetAQOperator() 
		{
			#region �ӿ�˵��
			//��ø�������Ա�б�
			//Manager.Person.GetEmployee.1
			//���룺0 type ��Ա���� 
			//��������Ա��Ϣ
			#endregion
			string strSql="";
			if (this.Sql.GetSql("AdminQuery.AQPermission.GetOperatorList",ref  strSql) == 0 ) 
			{
				try 
				{
					strSql= string.Format(strSql);
				}
				catch(Exception ex) 
				{
					this.Err=ex.Message;
					this.ErrCode=ex.Message;
					return null;
				}
			}
			else 
			{
				return null;
			}
			return this.myOperatorQuery(strSql);
		}

		/// <summary>
		/// ��õ�����ѯ��ԱȨ���б�
		/// </summary>
		/// <param name="Operator_ID"></param>
		/// <returns></returns>
		public ArrayList GetAQOperatorPermission(string Operator_ID) 
		{
			string strSql="";
			if (this.Sql.GetSql("AdminQuery.AQPermission.GetOPeratorPermissionList",ref  strSql) == 0 ) 
			{
				try 
				{
					strSql= string.Format(strSql, Operator_ID);
				}
				catch(Exception ex) 
				{
					this.Err=ex.Message;
					this.ErrCode=ex.Message;
					return null;
				}
			}
			else 
			{
				return null;
			}
			return this.myOperatorPermissionQuery(strSql);
		}

		/// <summary>
		/// ˽�к���������Ժ����ѯ��Ա������Ϣ
		/// </summary>
		/// <param name="SqlOperator"></param>
		/// <returns></returns>
		private ArrayList myOperatorQuery(string SqlOperator) 
		{
			ArrayList al=new ArrayList();
			
			if (this.ExecQuery(SqlOperator) == -1) return null;
			try 
			{
		
				while(this.Reader.Read()) 
				{
					FS.FrameWork.Models.NeuObject obj= new FS.FrameWork.Models.NeuObject();
					try 
					{
//						SELECT   distinct a.empl_code,   --Ա������
//						a.empl_name,   --Ա������
//						a.login_password,   --��¼��Ա����
//						a.login_name,   --Ա����¼����
//						a.manager_flag,   --����Ա��־
//						a.owner_flag,   --�����־
//						'',--�����
//						''   --ƴ����
//						FROM his_role_Aqoperator a ,r_employee b  --Ȩ�޹�����Ա��ϸ
//						where a.empl_code = b.empl_code 
    
						obj.ID = this.Reader[0].ToString();
						obj.Name = this.Reader[1].ToString();
						obj.User01 = this.Reader[2].ToString();
						obj.Memo = this.Reader[3].ToString();				
						obj.User02 = "0";
						obj.User03 = "0";
					}	
					catch(Exception ex) 
					{
						this.Err="���Ժ����ѯ��Ա��Ϣ����"+ex.Message;
						this.WriteErr();
						return null;
					}
					al.Add(obj);
				}
			}//�׳�����
			catch(Exception ex) 
			{
				this.Err="���Ժ����ѯ��Ա��Ϣ����"+ex.Message;
				this.ErrCode="-1";
				this.WriteErr();
				return null;
			}
			this.Reader.Close();
			return al;
		}

		/// <summary>
		/// ˽�к�����������ѯ��ԱȨ����Ϣ
		/// </summary>
		/// <param name="SqlOperatorPermission"></param>
		/// <returns></returns>
		private ArrayList myOperatorPermissionQuery(string SqlOperatorPermission) 
		{
			ArrayList al=new ArrayList();
			
			if (this.ExecQuery(SqlOperatorPermission) == -1) return null;
			try 
			{
		
				while(this.Reader.Read()) 
				{
					FS.FrameWork.Models.NeuObject obj= new FS.FrameWork.Models.NeuObject();
					try 
					{
//						select 
//						PARENT_CODE,    --VARCHAR2(6)                   ���������   
//						GROUP_CODE,     --VARCHAR2(6)                   ���������   
//						EMPL_CODE,      --VARCHAR2(6)                   Ա������     
//						LOGIN_PASSWORD, --VARCHAR2(8)  Y                ��¼��Ա���� 
//						LOGIN_NAME,     --VARCHAR2(20)                  Ա����¼���� 
//						MANAGER_FLAG,   --VARCHAR2(1)  Y                ����Ա��־   
//						GROUP_NAME,     --VARCHAR2(30) Y                ����������   
//						PARENT_NAME,    --VARCHAR2(30) Y                ����������   
//						EMPL_NAME,      --VARCHAR2(10) Y                Ա������     
//						OWNER_FLAG     --VARCHAR2(1)  Y        0                 
//						from his_role_aqoperator --Ժ����ѯȨ�ޱ�
//						where empl_code = '{0}'    
						obj.ID = this.Reader[2].ToString();
						obj.Name = this.Reader[8].ToString();
						obj.User01 = this.Reader[3].ToString();
						obj.Memo = this.Reader[4].ToString();				
						obj.User02 = this.Reader[1].ToString();
						obj.User03 = this.Reader[6].ToString();
					}	
					catch(Exception ex) 
					{
						this.Err="���Ժ����ѯ��Ա��Ϣ����"+ex.Message;
						this.WriteErr();
						return null;
					}
					al.Add(obj);
				}
			}//�׳�����
			catch(Exception ex) 
			{
				this.Err="���Ժ����ѯ��Ա��Ϣ����"+ex.Message;
				this.ErrCode="-1";
				this.WriteErr();
				return null;
			}
			this.Reader.Close();
			return al;
		}
		#endregion Ժ����ѯ��Ա��

	}
}
