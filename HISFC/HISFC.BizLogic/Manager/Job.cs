using System;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.HISFC.BizLogic.Manager {
	/// <summary>
	/// Job������
	/// writed by cuipeng
	/// 2005-11
	/// </summary>
	public class Job : DataBase {

		public Job() {
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		/// <summary>
		/// ���ݿ��ұ���ȡһ��Job��Ϣ
		/// </summary>
		/// <param name="ID">Job����</param>
		/// <returns>Job</returns>
		public FS.HISFC.Models.Base.Job GetJob(string ID) {
			string strSQL = "";
			//ȡSELECT���
			if (this.GetSQL("Manager.Job.GetJob",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.Job.GetJob�ֶ�!";
				return null;
			}
			
			string strWhere = "";
			//ȡWHERE���
			if (this.GetSQL("Manager.Job.GetJob.Where",ref strWhere) == -1) {
				this.Err="û���ҵ�Manager.Job.GetJob.Where�ֶ�!";
				return null;
			}

			//��ʽ��SQL���
			try {
				strSQL += " " +strWhere;
				strSQL = string.Format(strSQL, ID);
			}
			catch (Exception ex) {
				this.Err = "��ʽ��SQL���ʱ����Manager.Job.GetJob.Where:" + ex.Message;
				return null;
			}

			//ȡJob
			ArrayList al = this.myGetJob(strSQL);
			if(al == null) 
				return null;

			if(al.Count == 0) 
				return new FS.HISFC.Models.Base.Job();

			return al[0] as FS.HISFC.Models.Base.Job;
		}


		/// <summary>
		/// ȡJob�б�
		/// </summary>
		/// <returns>Job���飬������null</returns>
		public ArrayList GetJobList() {
			string strSQL = "";
			//ȡSELECT���
			if (this.GetSQL("Manager.Job.GetJob",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.Job.GetJob�ֶ�!";
				return null;
			}

			//ȡJob����
			return this.myGetJob(strSQL);
		}


		/// <summary>
		/// ȡJob�б�
		/// </summary>
		/// <param name="jobType"></param>
		/// <returns>Job���飬������null</returns>
		public ArrayList GetJobList(string jobType) {
			string strSQL = "";
			//ȡSELECT���
			if (this.GetSQL("Manager.Job.GetJob",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.Job.GetJob�ֶ�!";
				return null;
			}

			string strWhere = "";
			//ȡWHERE���
			if (this.GetSQL("Manager.Job.GetJob.ByType",ref strWhere) == -1) {
				this.Err="û���ҵ�Manager.Job.GetJob.ByType�ֶ�!";
				return null;
			}

			try {
				strWhere = string.Format(strWhere, jobType);
			}
			catch(Exception ex) {
				this.Err = ex.Message;
				return null;
			}

			//ȡJob����
			return this.myGetJob(strSQL + " " + strWhere);
		}

		/// <summary>
		/// ��Job���в���һ����¼
		/// </summary>
		/// <param name="Job">������չ������</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int InsertJob(FS.HISFC.Models.Base.Job Job) {
			string strSQL="";
			//ȡ���������SQL���
			if(this.GetSQL("Manager.Job.InsertJob",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.Job.InsertJob�ֶ�!";
				return -1;
			}
			try {  
				string[] strParm = myGetParmJob( Job );     //ȡ�����б�
				strSQL=string.Format(strSQL, strParm);      //�滻SQL����еĲ�����
			}
			catch(Exception ex) {
				this.Err = "��ʽ��SQL���ʱ����Manager.Job.InsertJob:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		
		
		/// <summary>
		/// ����Job����һ����¼
		/// </summary>
		/// <param name="Job">������չ������</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int UpdateJob(FS.HISFC.Models.Base.Job Job) {
			string strSQL="";
			//ȡ���²�����SQL���
			if(this.GetSQL("Manager.Job.UpdateJob",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.Job.UpdateJob�ֶ�!";
				return -1;
			}
			try {  
				string[] strParm = myGetParmJob( Job );     //ȡ�����б�
				strSQL=string.Format(strSQL, strParm);      //�滻SQL����еĲ�����
			}
			catch(Exception ex) {
				this.Err = "��ʽ��SQL���ʱ����Manager.Job.UpdateJob:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		
		
		/// <summary>
		/// ɾ��Job����һ����¼
		/// </summary>
		/// <param name="ID">��ˮ��</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int DeleteJob(string ID) {
			string strSQL="";
			//ȡɾ��������SQL���
			if(this.GetSQL("Manager.Job.DeleteJob",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.Job.DeleteJob�ֶ�!";
				return -1;
			}
			try {  
				strSQL=string.Format(strSQL, ID);    //�滻SQL����еĲ�����
			}
			catch(Exception ex) {
				this.Err = "��ʽ��SQL���ʱ����Manager.Job.DeleteJob:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		

		/// <summary>
		/// ������Ա���Ա䶯���ݣ�����ִ�и��²��������û���ҵ����Ը��µ����ݣ������һ���¼�¼
		/// </summary>
		/// <param name="Job">Jobʵ��</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int SetJob(FS.HISFC.Models.Base.Job Job) {
			int parm;
			//ִ�и��²���
			parm = UpdateJob(Job);

			//���û���ҵ����Ը��µ����ݣ������һ���¼�¼
			if (parm == 0 ) {
				parm = InsertJob(Job);
			}
			return parm;
		}


		/// <summary>
		/// ȡJob�б�������һ�����߶���
		/// ˽�з����������������е���
		/// </summary>
		/// <param name="SQLString">SQL���</param>
		/// <returns>Job��Ϣ��������</returns>
		private ArrayList myGetJob(string SQLString) {
			ArrayList al=new ArrayList();                
			FS.HISFC.Models.Base.Job Job; //Jobʵ��
			this.ProgressBarText="���ڼ���Job...";
			this.ProgressBarValue=0;
			
			//ִ�в�ѯ���
			if (this.ExecQuery(SQLString)==-1) {
				this.Err="���Jobʱ��ִ��SQL������"+this.Err;
				this.ErrCode="-1";
				return null;
			}
			try {
				while (this.Reader.Read()) {
					//ȡ��ѯ����еļ�¼
					Job = new FS.HISFC.Models.Base.Job();
					Job.ID          = this.Reader[0].ToString();	//0 Job����
					Job.Name     = this.Reader[1].ToString();		//1 Job����
					Job.State.ID = this.Reader[2].ToString();		//2 ״̬N_��ͳ��, D_����ִ��,  M_����ִ�У�,Y_����ִ�� ,S_����ͳ��
					Job.LastTime = NConvert.ToDateTime(this.Reader[3].ToString());	//3 �ϴ�ִ��ʱ��
					Job.NextTime = NConvert.ToDateTime(this.Reader[4].ToString());	//4 �´�ִ��ʱ�� 
					Job.Type = this.Reader[5].ToString();			//5 ����: 0 ǰ̨Ӧ�ó�����, 1 ��̨job����
					Job.IntervalDays = NConvert.ToInt32(this.Reader[6].ToString()); //6 ����������
					Job.Department.ID = this.Reader[7].ToString();		//7 ��ǰ״̬��0���룬1ȷ�ϣ�2���ϣ�
					Job.User01   = this.Reader[8].ToString();		//8 �������Ա
					Job.User02   = this.Reader[9].ToString();		//9 ����ʱ��
					Job.Memo     = this.Reader[10].ToString();		//10 ��ע
                    if (this.Reader.FieldCount > 11)
                    {
                        Job.Implement.ID = this.Reader[11].ToString();//dllName
                        Job.Implement.Name = this.Reader[12].ToString();//className
                    }

					this.ProgressBarValue++;
					al.Add(Job);
				}
			}//�׳�����
			catch(Exception ex) {
				this.Err="���Job��Ϣʱ����"+ex.Message;
				this.ErrCode="-1";
				return null;
			}
			this.Reader.Close();

			this.ProgressBarValue=-1;
			return al;
		}


		/// <summary>
		/// ���update����insertJob��Ĵ����������
		/// </summary>
		/// <param name="Job">Jobʵ��</param>
		/// <returns>�ַ�������</returns>
		private string[] myGetParmJob(FS.HISFC.Models.Base.Job Job) {
			string[] strParm={   
								 Job.ID,					//0 Job����
								 Job.Name,					//1 Job����
								 Job.State.ID.ToString(),	//2 ״̬N_��ͳ��, D_����ִ��,  M_����ִ�У�,Y_����ִ�� ,S_����ͳ��
								 Job.LastTime.ToString(),	//3 �ϴ�ִ��ʱ��
								 Job.NextTime.ToString() ,	//4 �´�ִ��ʱ�� 
								 Job.Type,					//5 ����: 0 ǰ̨Ӧ�ó�����, 1 ��̨job����
								 Job.IntervalDays.ToString(),//6 ����������
								 Job.Department.ID,				//7 ����
								 this.Operator.ID,			//8 �������Ա
								 Job.Memo					//9 ��ע
							 };								 
			return strParm;
		}

		
	}

}

#region SQL
//<SQL id="Manager.Job.GetJob" Memo="ȡJob" input="none" output="3">
//<!--   --><![CDATA[  
//			SELECT  COM_EMPLOYEE_RECORD.RECORD_NO,                              --̨�ʼ�¼��ˮ��
//					COM_EMPLOYEE_RECORD.EMPL_CODE,                              --Ա������
//					COM_EMPLOYEE_RECORD.SHIFT_TYPE,                             --�䶯���ͣ�DEPT���ң�NURSE��ʿվ�ȣ�
//					COM_EMPLOYEE_RECORD.OLD_DATA_CODE,                          --ԭ���ϴ���
//					COM_EMPLOYEE_RECORD.OLD_DATA_NAME,                          --ԭ��������
//					COM_EMPLOYEE_RECORD.NEW_DATA_CODE,                          --�����ϴ���
//					COM_EMPLOYEE_RECORD.NEW_DATA_NAME,                          --����������
//					COM_EMPLOYEE_RECORD.STATE,                                  --��ǰ״̬��0���룬1ȷ�ϣ�2���ϣ�
//					COM_EMPLOYEE_RECORD.MARK,                                   --��ע
//					COM_EMPLOYEE_RECORD.APPLY_CODE,                             --�������Ա
//					COM_EMPLOYEE_RECORD.APPLY_DATE,                             --����ʱ��
//					COM_EMPLOYEE_RECORD.OPER_CODE,                              --����Ա����׼�����ϣ�
//					COM_EMPLOYEE_RECORD.OPER_DATE,                              --����ʱ�䣨��׼�����ϣ�
//					EMPL_NAME													--Ա������ 
//			FROM	COM_EMPLOYEE_RECORD,  
//					COM_EMPLOYEE 
//			WHERE	COM_EMPLOYEE_RECORD.PARENT_CODE  = COM_EMPLOYEE.PARENT_CODE 
//			AND		COM_EMPLOYEE_RECORD.CURRENT_CODE = COM_EMPLOYEE.CURRENT_CODE 
//			AND		COM_EMPLOYEE_RECORD.EMPL_CODE    = COM_EMPLOYEE.EMPL_CODE 
//			AND		COM_EMPLOYEE_RECORD.PARENT_CODE  = '[��������]' 
//			AND		COM_EMPLOYEE_RECORD.CURRENT_CODE = '[��������]' 
//]]></SQL>
//<SQL id="Manager.Job.GetJob.Where" Memo="ȡJob�б�" input="none" output="3">
//<!--   --><![CDATA[  
//			AND		COM_EMPLOYEE_RECORD.EMPL_CODE    = '{0}' 
//			AND		COM_EMPLOYEE_RECORD.SHIFT_TYPE   = '{1}' 
//			AND		COM_EMPLOYEE_RECORD.STATE        = '{2}' 
//			AND		ROWNUM = 1 
//]]></SQL>
//<SQL id="Manager.Job.GetJobList" Memo="ȡJob�б�" input="none" output="3">
//<!--   --><![CDATA[  
//			AND		COM_EMPLOYEE_RECORD.EMPL_CODE    = '{0}' 
//			AND		COM_EMPLOYEE_RECORD.OPER_DATE   >= '{1}' 
//			AND		COM_EMPLOYEE_RECORD.OPER_DATE   <= '{2}' 
//]]></SQL>
//<SQL id="Manager.Job.InsertJob" Memo="��Job���в���һ����¼ input="none" output="3">
//<!--   --><![CDATA[  
//			INSERT INTO COM_EMPLOYEE_RECORD (
//					PARENT_CODE ,                           --����ҽ�ƻ�������
//					CURRENT_CODE ,                          --����ҽԺ��������
//					RECORD_NO ,                             --̨�ʼ�¼��ˮ��
//					EMPL_CODE ,                             --Ա������
//					SHIFT_TYPE ,                            --�䶯���ͣ�DEPT���ң�NURSE��ʿվ�ȣ�
//					OLD_DATA_CODE ,                         --ԭ���ϴ���
//					OLD_DATA_NAME ,                         --ԭ��������
//					NEW_DATA_CODE ,                         --�����ϴ���
//					NEW_DATA_NAME ,                         --����������
//					STATE ,                                 --��ǰ״̬��0���룬1ȷ�ϣ�2���ϣ�
//					MARK ,                                  --��ע
//					APPLY_CODE ,                            --�������Ա
//					APPLY_DATE ,                            --����ʱ��
//					OPER_CODE ,                             --����Ա����׼�����ϣ�
//					OPER_DATE)                              --����ʱ�䣨��׼�����ϣ�
//			VALUES(
//					'[��������]',       --����ҽ�ƻ�������
//					'[��������]',       --����ҽԺ��������
//					'{0}' ,       --̨�ʼ�¼��ˮ��
//					'{1}' ,       --Ա������
//					'{2}' ,       --�䶯���ͣ�DEPT���ң�NURSE��ʿվ�ȣ�
//					'{3}' ,       --ԭ���ϴ���
//					'{4}' ,       --ԭ��������
//					'{5}' ,       --�����ϴ���
//					'{6}' ,       --����������
//					'{7}' ,       --��ǰ״̬��0���룬1ȷ�ϣ�2���ϣ�
//					'{8}' ,       --��ע
//					'{9}' ,       --�������Ա
//					to_date('{10}','yyyy-mm-dd HH24:mi:ss') ,       --����ʱ��
//					'{11}' ,      --����Ա����׼�����ϣ�
//					SYSDATE       --����ʱ�䣨��׼�����ϣ�
//					) 
//]]></SQL>
//<SQL id="Manager.Job.UpdateJob" Memo="����Job����һ����¼" input="none" output="3">
//<!--   --><![CDATA[         
//UPDATE	COM_EMPLOYEE_RECORD 
//SET 	EMPL_CODE = '{1}' ,                     --Ա������
//		SHIFT_TYPE = '{2}' ,                    --�䶯���ͣ�DEPT���ң�NURSE��ʿվ�ȣ�
//		OLD_DATA_CODE = '{3}' ,                 --ԭ���ϴ���
//		OLD_DATA_NAME = '{4}' ,                 --ԭ��������
//		NEW_DATA_CODE = '{5}' ,                 --�����ϴ���
//		NEW_DATA_NAME = '{6}' ,                 --����������
//		STATE = '{7}' ,                         --��ǰ״̬��0���룬1ȷ�ϣ�2���ϣ�
//		MARK = '{8}' ,                          --��ע
//		APPLY_CODE = '{9}' ,                    --�������Ա
//		APPLY_DATE = to_date('{10}','yyyy-mm-dd HH24:mi:ss') , --����ʱ��
//		OPER_CODE = '{11}' ,                    --����Ա����׼�����ϣ�
//		OPER_DATE = SYSDATE						--����ʱ�䣨��׼�����ϣ�
//WHERE	PARENT_CODE  = '[��������]' 
//AND		CURRENT_CODE = '[��������]' 
//AND		RECORD_NO = '{0}' 
//]]></SQL>
//<SQL id="Manager.Job.DeleteJob" Memo="ɾ��Job����һ����¼" input="none" output="3">
//<!--   --><![CDATA[ 
//			DELETE FROM COM_EMPLOYEE_RECORD 
//			WHERE	PARENT_CODE  = '[��������]'
//			AND		CURRENT_CODE = '[��������]'
//			AND		RECORD_NO = '{0}'        
//]]></SQL>
#endregion