using System;
using neusoft.HISFC.Object.RADT;
using System.Collections;
using neusoft.neuFC.Object;

namespace neusoft.HISFC.Management.Case
{
	/// <summary>
	/// Diagnose ��ժҪ˵����
	/// </summary>
	public class Diagnose:neusoft.neuFC.Management.Database
	{
		public Diagnose()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ���ҵ��
			#region ��������Ϸ������
			/// <summary>
			/// ��������Ϸ������
			/// </summary>
			/// <returns>long ���������� ����ʱ����-1</returns>
			public long GetNewDignoseNo()
			{
				long lNewNo = -1;
				string strSql = "";
				if(this.Sql.GetSql("RADT.Diagnose.GetNewDiagnoseNo.1",ref strSql) == -1) return -1; 
				if (strSql == null) return -1;
				this.ExecQuery(strSql);
				try
				{
					while(this.Reader.Read())
					{
						lNewNo = long.Parse(Reader[0].ToString());
					}
				}
				catch(Exception ex)
				{
					this.Err = ex.Message;
					this.WriteErr();;
					return -1;            
				}
				this.Reader.Close();
				return lNewNo;
			}
			#endregion
			#region �Ǽǻ��������Ϣ
			/// <summary>
			/// �Ǽ��µĻ������
			/// </summary>
			/// <param name="Diagnose"></param>
			/// <returns></returns>
			public int CreatePatientDiagnose(neusoft.HISFC.Object.RADT.Diagnose Diagnose)
			{
				#region "�ӿ�˵��"
				///�ӿ����� RADT.Diagnose.CreatePatientDiagnose.1
				// 0  --סԺ��ˮ��, 1 --�������      2   --������   ,     3   --������  ,4   --��ϱ��� 
				// 5  --�������,   6   --���ʱ��   ,7   --���ҽ������  ,8   --ҽ������ , 9   --�Ƿ���Ч
				// 10 --��Ͽ���ID 11   --�Ƿ������ 12   --��ע          13   --����Ա    14   --����ʱ��
				#endregion
				string strSql="";
				if(this.Sql.GetSql("RADT.Diagnose.CreatePatientDiagnose.1",ref strSql)==-1) return -1;
				
				try
				{
					string[] s=new string[15];
					try
					{
						s[0]=Diagnose.Patient.ID.ToString();// --����סԺ��ˮ��
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[1]=Diagnose.HappenNo.ToString();//  --�������
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[2]=Diagnose.Patient.PID.CardNo;// --���￨��
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[3]=Diagnose.DiagType.ID.ToString();//  --������
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[4]=Diagnose.ID.ToString();// --��ϱ���
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[5]=Diagnose.Name ;//--�������
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[6]=Diagnose.DiagDate.ToString();//  --���ʱ��
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[7]=Diagnose.Doctor.ID.ToString();//    --���ҽ��
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[8]=Diagnose.Doctor.Name;//    --���ҽ��
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[9]=(System.Convert.ToInt16(Diagnose.IsValid)).ToString();//    --�Ƿ���Ч
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[10]=Diagnose.Dept.ID.ToString();//  --��Ͽ���
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[11]=(System.Convert.ToInt16(Diagnose.IsMain)).ToString();//  --�Ƿ������
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					
					try
					{
						s[12]=Diagnose.Memo;//    --��ע
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[13]=this.Operator.ID.ToString();//    --������
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[14]=this.GetSysDateTime().ToString();//    --������
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					
					strSql=string.Format(strSql,s);
				}
				catch(Exception ex)
				{
					this.Err="��ֵʱ�����"+ex.Message;
					this.WriteErr();
					return -1;
				}	
				return this.ExecNoQuery(strSql);
			}
			#endregion
			#region ���»��������Ϣ
			/// <summary>
			/// ���»��������Ϣ
			/// </summary>
			/// <param name="Diagnose"></param>
			/// <returns></returns>
			public int UpdatePatientDiagnose(neusoft.HISFC.Object.RADT.Diagnose Diagnose)
			{
				#region "�ӿ�˵��"
				///�ӿ����� RADT.Diagnose.UpdatePatientDiagnose.1
				// 0  --סԺ��ˮ��, 1 --�������      2   --������   ,     3   --������  ,4   --��ϱ��� 
				// 5  --�������,   6   --���ʱ��   ,7   --���ҽ������  ,8   --ҽ������ , 9   --�Ƿ���Ч
				// 10 --��Ͽ���ID 11   --�Ƿ������ 12   --��ע          13   --����Ա    14   --����ʱ��
				#endregion
				string strSql="";
				if(this.Sql.GetSql("RADT.Diagnose.UpdatePatientDiagnose.1",ref strSql)==-1) return -1;
				
				try
				{
					string[] s=new string[15];
					try
					{
						s[0]=Diagnose.Patient.ID.ToString();// --��ϱ���
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[1]=Diagnose.HappenNo.ToString();//  --�������
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[2]=Diagnose.Patient.PID.CardNo;// --��ϱ���
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[3]=Diagnose.DiagType.ID.ToString();//  --������
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[4]=Diagnose.ID.ToString();// --��ϱ���
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[5]=Diagnose.Name ;//--�������
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[6]=Diagnose.DiagDate.ToString();//  --���ʱ��
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[7]=Diagnose.Doctor.ID.ToString();//    --���ҽ��
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[8]=Diagnose.Doctor.Name;//    --���ҽ��
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[9]=(System.Convert.ToInt16(Diagnose.IsValid)).ToString();//    --�Ƿ���Ч
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[10]=Diagnose.Dept.ID.ToString();//  --��Ͽ���
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[11]=(System.Convert.ToInt16(Diagnose.IsMain)).ToString();//  --�Ƿ������
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					
					try
					{
						s[12]=Diagnose.Memo;//    --��ע
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[13]=this.Operator.ID.ToString();//    --������
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						s[14]=this.GetSysDateTime().ToString();//    --������
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					strSql=string.Format(strSql,s);
				}
				catch(Exception ex)
				{
					this.Err="��ֵʱ�����"+ex.Message;
					this.WriteErr();
					return -1;
				}	
				return this.ExecNoQuery(strSql);
			}
			#endregion
			#region ���ϻ��������Ϣ
			/// <summary>
			/// ���ϻ��������Ϣ
			/// </summary>
			/// <param name="Diagnose"></param>
			/// <returns></returns>
			public int DcPatientDiagnose(neusoft.HISFC.Object.RADT.Diagnose Diagnose)
			{
				#region �ӿ�˵��
				///���ϻ��������Ϣ
				///RADT.Diagnose.DcPatientDiagnose.1
				///���룺0 InpatientNoסԺ��ˮ��,1 happenno �������
				///������0 
				#endregion
				string strSql="";
				if(this.Sql.GetSql("RADT.Diagnose.DcPatientDiagnose.1",ref strSql)==-1) return -1;
				try
				{
					strSql=string.Format(strSql,Diagnose.Patient.ID,Diagnose.HappenNo.ToString());
				}
				catch
				{
					this.Err="����������ԣ�RADT.Diagnose.DcPatientDiagnose.1";
					return -1;
				}
				return this.ExecNoQuery(strSql);
			}
			#endregion
		#endregion
		#region "��ѯ����"
        #region "��ѯ�������"
		/// <summary>
		/// ��ѯ�����������
		/// </summary>
		/// <param name="InPatientNo"></param>
		/// <returns></returns>
		public ArrayList PatientDiagnoseQuery(string InPatientNo)
		{
			#region �ӿ�˵��
			/////RADT.Diagnose.PatientDiagnoseQuery.1
			///���룺סԺ��ˮ��
			///���������������Ϣ
			#endregion
			ArrayList al=new ArrayList();
			string sql1="",sql2="";

			sql1 = PatientQuerySelect();
			if (sql1==null ) return null;

			if(this.Sql.GetSql("RADT.Diagnose.PatientDiagnoseQuery.1",ref sql2)==-1)
			{
				this.Err="û���ҵ�RADT.Diagnose.PatientDiagnoseQuery.1�ֶ�!";
				this.ErrCode="-1";
				return null;
			}
			sql1=sql1+" "+string.Format(sql2,InPatientNo);
			return this.myPatientQuery(sql1);
		}
		/// <summary>
		/// ��ѯ���߸��������
		/// </summary>
		/// <param name="InPatientNo"></param>
		/// <param name="DiagType"></param>
		/// <returns></returns>
		public ArrayList PatientDiagnoseQuery(string InPatientNo,string DiagType)
		{
			#region �ӿ�˵��
			/////RADT.Diagnose.PatientDiagnoseQuery.2
			///���룺סԺ��ˮ��
			///���������������Ϣ
			#endregion
			ArrayList al=new ArrayList();
			string sql1="",sql2="";

			sql1 = PatientQuerySelect();
			if (sql1==null ) return null;

			if(this.Sql.GetSql("RADT.Diagnose.PatientDiagnoseQuery.2",ref sql2)==-1)
			{
				this.Err="û���ҵ�RADT.Diagnose.PatientDiagnoseQuery.2�ֶ�!";
				this.ErrCode="-1";
				return null;
			}
			sql1=sql1+" "+string.Format(sql2,InPatientNo,DiagType);
			return this.myPatientQuery(sql1);
		}
		/// <summary>
		/// ��ѯ���߸�״̬���
		/// </summary>
		/// <param name="InPatientNo"></param>
		/// <param name="IsValid"></param>
		/// <returns></returns>
		public ArrayList PatientDiagnoseQuery(string InPatientNo,bool IsValid)
		{
			#region �ӿ�˵��
			/////RADT.Diagnose.PatientDiagnoseQuery.3
			///���룺סԺ��ˮ��
			///���������������Ϣ
			#endregion
			ArrayList al=new ArrayList();
			string sql1="",sql2="";

			sql1 = PatientQuerySelect();
			if (sql1==null ) return null;

			if(this.Sql.GetSql("RADT.Diagnose.PatientDiagnoseQuery.3",ref sql2)==-1)
			{
				this.Err="û���ҵ�RADT.Diagnose.PatientDiagnoseQuery.3�ֶ�!";
				this.ErrCode="-1";
				return null;
			}
			sql1=sql1+" "+string.Format(sql2,InPatientNo,neusoft.neuFC.Function.NConvert.ToInt32(IsValid).ToString());
			return this.myPatientQuery(sql1);
		}
		/// <summary>
		/// ��ѯ������/�������
		/// </summary>
		/// <param name="InPatientNo"></param>
		/// <param name="IsMain"></param>
		/// <returns></returns>
		public ArrayList MainDiagnoseQuery(string InPatientNo,bool IsMain)
		{
			#region �ӿ�˵��
			/////RADT.Diagnose.PatientDiagnoseQuery.4
			///���룺0סԺ��ˮ��1 �Ƿ������
			///���������������Ϣ
			#endregion
			ArrayList al=new ArrayList();
			string sql1="",sql2="";

			sql1 = PatientQuerySelect();
			if (sql1==null ) return null;

			if(this.Sql.GetSql("RADT.Diagnose.PatientDiagnoseQuery.4",ref sql2)==-1)
			{
				this.Err="û���ҵ�RADT.Diagnose.PatientDiagnoseQuery.4�ֶ�!";
				this.ErrCode="-1";
				return null;
			}
			sql1=sql1+" "+string.Format(sql2,InPatientNo,IsMain.ToString());
			return this.myPatientQuery(sql1);
		}
		/// ��ѯ���������Ϣ��select��䣨��where������
		private string PatientQuerySelect()
		{
			#region �ӿ�˵��
			///RADT.Diagnose.DiagnoseQuery.select.1
			///���룺0
			///������sql.select
			#endregion
			string sql="";
			if(this.Sql.GetSql("RADT.Diagnose.DiagnoseQuery.select.1",ref sql)==-1)
			{
				this.Err="û���ҵ�RADT.Diagnose.DiagnoseQuery.select.1�ֶ�!";
				this.ErrCode="-1";
				this.WriteErr();
				return null;
			}
			return sql;
		}
		//˽�к�������ѯ���߻�����Ϣ
		private ArrayList myPatientQuery(string SQLPatient)
		{
			ArrayList al=new ArrayList();
			neusoft.HISFC.Object.RADT.Diagnose Diagnose ;
			this.ProgressBarText="���ڲ�ѯ�������...";
			this.ProgressBarValue=0;
			
			this.ExecQuery(SQLPatient);
			try
			{
				while (this.Reader.Read())
				{
					Diagnose=new neusoft.HISFC.Object.RADT.Diagnose();
					#region "�ӿ�˵��"
					// 0  --סԺ��ˮ��, 1 --�������      2   --������   ,     3   --������  ,4   --��ϱ��� 
					// 5  --�������,   6   --���ʱ��   ,7   --���ҽ������  ,8   --ҽ������ , 9   --�Ƿ���Ч
					// 10 --��Ͽ���ID 11   --�Ƿ������ 12   --��ע          13   --����Ա    14   --����ʱ��
					#endregion
					try
					{
						Diagnose.Patient.ID = this.Reader[0].ToString();// סԺ��ˮ��
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						Diagnose.HappenNo = neusoft.neuFC.Function.NConvert.ToInt32(this.Reader[1].ToString());//  �������
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						Diagnose.Patient.PID.CardNo = this.Reader[2].ToString();//������
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						Diagnose.DiagType.ID = this.Reader[3].ToString();//������
						DiagnoseType diagnosetype =new DiagnoseType();
						diagnosetype.ID = Diagnose.DiagType.ID;
						Diagnose.DiagType.Name = diagnosetype.Name;//����������

					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						Diagnose.ID = this.Reader[4].ToString();		//��ϴ���
						Diagnose.ICD10.ID = this.Reader[4].ToString();
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						Diagnose.Name = this.Reader[5].ToString();		//�������
						Diagnose.ICD10.Name = this.Reader[5].ToString();
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
				
					try
					{
						Diagnose.DiagDate = neusoft.neuFC.Function.NConvert.ToDateTime(this.Reader[6].ToString());
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						Diagnose.Doctor.ID = this.Reader[7].ToString();
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						Diagnose.Doctor.Name = this.Reader[8].ToString();
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						Diagnose.IsValid = neusoft.neuFC.Function.NConvert.ToBoolean(this.Reader[9]);
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						Diagnose.Dept.ID = this.Reader[10].ToString();
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						Diagnose.IsMain = neusoft.neuFC.Function.NConvert.ToBoolean(this.Reader[11]);
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						Diagnose.Memo = this.Reader[12].ToString();
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						Diagnose.User01 = this.Reader[13].ToString();
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						Diagnose.User02 = this.Reader[14].ToString();
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					al.Add(Diagnose);
				}
			}
			catch(Exception ex)
			{
				this.Err="��û��������Ϣ����"+ex.Message;
				this.ErrCode="-1";
				this.WriteErr();
				return al;
			}
			this.Reader.Close();

			this.ProgressBarValue=-1;
			return al;
		}
		#endregion
		#region "ICD10"
		/// <summary>
		/// ��ѯ��������������ӦICD10
		/// </summary>
		/// <param name="DiseaseCode"></param>
		/// <returns></returns>
		public ArrayList ICD10Query(string  DiseaseCode)
		{
			#region �ӿ�˵��
			/////RADT.Diagnose.ICD10Query.1
			///���룺0����������
			///������ICD10�����Ϣ
			#endregion
			ArrayList al=new ArrayList();
			string sql1="",sql2="";

			sql1 = ICD10QuerySelect();
			if (sql1==null ) return null;

			if(this.Sql.GetSql("RADT.Diagnose.ICD10Query.1",ref sql2)==-1)
			{
				this.Err="û���ҵ�RADT.Diagnose.ICD10Query.1�ֶ�!";
				this.ErrCode="-1";
				return null;
			}
			sql1=sql1+" "+string.Format(sql2,DiseaseCode);
			return this.myICD10Query(sql1);
		}


		public ArrayList ICD10QueryAll()
		{
			ArrayList al = new ArrayList();
	
			string sqlBegin = "";

			sqlBegin = this.ICD10QuerySelect();

			return this.myICD10Query( sqlBegin );
		}

		/// <summary>
		/// ��ѯ������ICD10
		/// </summary>
		/// <param name="DiagnoseType"></param>
		/// <returns></returns>
		public ArrayList ICD10Query(DiagnoseType DiagnoseType)
		{
			#region �ӿ�˵��
			/////RADT.Diagnose.ICD10Query.2
			///���룺0��Ϸ���
			///������ICD10�����Ϣ
			#endregion
			ArrayList al=new ArrayList();
			string sql1="",sql2="";

			sql1 = ICD10QuerySelect();
			if (sql1==null ) return null;

			if(this.Sql.GetSql("RADT.Diagnose.ICD10Query.2",ref sql2)==-1)
			{
				this.Err="û���ҵ�RADT.Diagnose.ICD10Query.2�ֶ�!";
				this.ErrCode="-1";
				return null;
			}
			sql1=sql1+" "+string.Format(sql2,DiagnoseType.ID);
			return this.myICD10Query(sql1);
		}
		/// ��ѯ���������Ϣ��select��䣨��where������
		private string ICD10QuerySelect()
		{
			#region �ӿ�˵��
			///RADT.Diagnose.ICD10Query.select.1
			///���룺0
			///������sql.select
			#endregion
			string sql="";
			if(this.Sql.GetSql("RADT.Diagnose.ICD10Query.select.1",ref sql)==-1)
			{
				this.Err="û���ҵ�RADT.Diagnose.ICD10Query.select.1�ֶ�!";
				this.ErrCode="-1";
				this.WriteErr();
				return null;
			}
			return sql;
		}
		//˽�к�������ѯ���߻�����Ϣ
		private ArrayList myICD10Query(string SQLPatient)
		{
			ArrayList al=new ArrayList();
			neusoft.HISFC.Object.RADT.ICD10 objICD ;
			this.ProgressBarText="���ڲ�ѯICD10���...";
			this.ProgressBarValue=0;
			
			this.ExecQuery(SQLPatient);
			try
			{
				while (this.Reader.Read())
				{
					objICD=new ICD10();
					#region "�ӿ�˵��"
					///�ӿ����� RADT.Diagnose.CreatePatientDiagnose.1
					// 0  --ICD10��      1   --�������     2   --ƴ����         3   --�����  ,    4   --���ļ�������
					// 5  --��������1,   6   --��������2   ,7   --��������ԭ��  ,8   --���������� , 9   --��׼סԺ��
					// 10 --סԺ�ȼ�    11   --����Ա      12   --����ʱ��
					#endregion
					try
					{
						objICD.ID = this.Reader[0].ToString();
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						objICD.SICD10 = this.Reader[1].ToString();
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						objICD.SpellCode.Spell_Code = this.Reader[2].ToString();
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						objICD.SpellCode.WB_Code  = this.Reader[3].ToString();
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						objICD.Name = this.Reader[4].ToString();
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						objICD.User01 = this.Reader[5].ToString();
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						objICD.User02 = this.Reader[6].ToString();
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						objICD.DeadReason = this.Reader[7].ToString();
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						objICD.DiseaseCode = this.Reader[8].ToString();
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						objICD.InDays = int.Parse(this.Reader[9].ToString());
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						objICD.Memo = this.Reader[10].ToString();
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					try
					{
						objICD.User03 = this.Reader[11].ToString();
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					
					try
					{
						objICD.OperDate = neusoft.neuFC.Function.NConvert.ToDateTime(this.Reader[12]);
					}
					catch(Exception ex)
					{
						this.Err=ex.Message;
						this.WriteErr();
					}
					
					al.Add(objICD);
				}
			}
			catch(Exception ex)
			{
				this.Err="���ICD10�����Ϣ����"+ex.Message;
				this.ErrCode="-1";
				this.WriteErr();
				return al;
			}
			this.Reader.Close();

			this.ProgressBarValue=-1;
			return al;
		}
		/// <summary>
		/// ����ICD10��Ϣ��������ֲ��ܸ��������һ��
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int UpdateICD( neusoft.HISFC.Object.RADT.ICD10 info )
		{
			string strSql = "", strSql2;
			int i = 0;
			if (this.Sql.GetSql("RADT.Diagnose.myIcdManagerImpl.UpdatemyIcd",ref strSql)==-1) return -1;
			
			try
			{   				
				strSql = string.Format(strSql,info.ID, info.SICD10, info.SpellCode.Spell_Code, 
										info.SpellCode.WB_Code, info.Name, info.User01, info.User02, 
										info.DeadReason, info.DiseaseCode, info.InDays.ToString(), 
										info.Memo, info.User03, info.OperDate.ToString());

			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}      			

			try
			{
				i = this.ExecNoQuery(strSql);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}

			if( i == 0 ) //����
			{			
				if (this.Sql.GetSql("RADT.Diagnose.myIcdManagerImpl.InsertmyIcd",ref strSql)==-1) return -1;
				try
				{
					strSql2 = string.Format(strSql,strSql,info.ID, info.SICD10, info.SpellCode.Spell_Code, 
						info.SpellCode.WB_Code, info.Name, info.User01, info.User02, 
						info.DeadReason, info.DiseaseCode, info.InDays.ToString(), 
						info.Memo, info.User03, info.OperDate.ToString());
				}
				catch(Exception ex)
				{
					this.Err=ex.Message;
					this.ErrCode=ex.Message;
					return -1;
				}
				
				return this.ExecNoQuery(strSql2);
			}
			else if( i > 0 )
				return 0;
			else
			{
				return -1;
			}
		}
		/// <summary>
		/// ɾ��ICD10��Ϣ
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int DeleteICD( neusoft.HISFC.Object.RADT.ICD10 info )
		{
			string strSql = "";
			if (this.Sql.GetSql("RADT.Diagnose.myIcdManagerImpl.DeletemyIcd",ref strSql)==-1) return -1;
				
			try
			{   				
				strSql = string.Format(strSql, info.ID);

			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}      			

			try
			{
				return this.ExecNoQuery(strSql);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}

		}

		#endregion
		#endregion


	}
}
