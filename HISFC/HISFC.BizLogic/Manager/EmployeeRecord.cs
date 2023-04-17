using System;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.HISFC.BizLogic.Manager {
	/// <summary>
	/// ������չȨ�޹�����
	/// writed by cuipeng
	/// 2005-3
	/// </summary>
	public class EmployeeRecord : DataBase {

		public EmployeeRecord() {
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		/// <summary>
		/// ȡ��Ա���롢�䶯���͡�״̬ȡһ����Ա���Ա䶯��Ϣ
		/// </summary>
		/// <param name="emplCode">��Ա����</param>
		/// <param name="shiftType">�䶯����</param>
		/// <param name="state">״̬</param>
		/// <returns>��Ա���Ա䶯��Ϣ</returns>
		public FS.HISFC.Models.Base.EmployeeRecord GetEmployeeRecord(string emplCode, string shiftType, string state) {
			string strSQL = "";
			//ȡSELECT���
			if (this.GetSQL("Manager.EmployeeRecord.GetEmployeeRecord",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.EmployeeRecord.GetEmployeeRecord�ֶ�!";
				return null;
			}
			
			string strWhere = "";
			//ȡWHERE���
			if (this.GetSQL("Manager.EmployeeRecord.GetEmployeeRecord.Where",ref strWhere) == -1) {
				this.Err="û���ҵ�Manager.EmployeeRecord.GetEmployeeRecord.Where�ֶ�!";
				return null;
			}

			//��ʽ��SQL���
			try {
				strSQL += " " +strWhere;
				strSQL = string.Format(strSQL, emplCode, shiftType, state);
			}
			catch (Exception ex) {
				this.Err = "��ʽ��SQL���ʱ����Manager.EmployeeRecord.GetEmployeeRecord.Where:" + ex.Message;
				return null;
			}

			//ȡ��Ա���Ա䶯��Ϣ
			ArrayList al = this.myGetEmployeeRecord(strSQL);
			if(al == null || al.Count <=0) return null;
			return al[0] as FS.HISFC.Models.Base.EmployeeRecord;
		}


		/// <summary>
		/// ȡĳһ��Ա��ĳһ��ʱ���ڵ����б䶯��Ϣ
		/// </summary>
		/// <param name="emplCode">��Ա����</param>
		/// <param name="beginDate">��ʼ����</param>
		/// <param name="endDate">��������</param>
		/// <returns>��Ա���Ա䶯��Ϣ���飬������null</returns>
		public ArrayList GetEmployeeRecordList(string emplCode, DateTime beginDate, DateTime endDate) {
			string strSQL = "";
			//ȡSELECT���
			if (this.GetSQL("Manager.EmployeeRecord.GetEmployeeRecord",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.EmployeeRecord.GetEmployeeRecord�ֶ�!";
				return null;
			}

			string strWhere = "";
			//ȡWHERE���
			if (this.GetSQL("Manager.EmployeeRecord.GetEmployeeRecordList",ref strWhere) == -1) {
				this.Err="û���ҵ�Manager.EmployeeRecord.GetEmployeeRecordList�ֶ�!";
				return null;
			}

			//��ʽ��SQL���
			try {
				strSQL += " " +strWhere;
				strSQL = string.Format(strSQL, emplCode, beginDate.ToString(), endDate.ToString());
			}
			catch (Exception ex) {
				this.Err = "��ʽ��SQL���ʱ����Manager.EmployeeRecord.GetEmployeeRecordList:" + ex.Message;
				return null;
			}

			//ȡ��Ա���Ա䶯��Ϣ����
			return this.myGetEmployeeRecord(strSQL);
		}


		/// <summary>
		/// ȡĳһ���ҡ�ĳһ״̬�ġ�ĳһ���Ա䶯��Ϣ
		/// </summary>
		/// <param name="NewDataCode">�䶯������ݱ���</param>
		/// <param name="shiftType">�䶯����</param>
		/// <param name="state">״̬��0���룬1��׼��</param>
		/// <returns>��Ա���Ա䶯��Ϣ���飬������null</returns>
		public ArrayList GetEmployeeRecordList(string NewDataCode, string shiftType, string state) {
			string strSQL = "";
			//ȡSELECT���
			if (this.GetSQL("Manager.EmployeeRecord.GetEmployeeRecord",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.EmployeeRecord.GetEmployeeRecord�ֶ�!";
				return null;
			}

			string strWhere = "";
			//ȡWHERE���
			if (this.GetSQL("Manager.EmployeeRecord.GetEmployeeRecordList.1",ref strWhere) == -1) {
				this.Err="û���ҵ�Manager.EmployeeRecord.GetEmployeeRecordList.1�ֶ�!";
				return null;
			}

			//��ʽ��SQL���
			try {
				strSQL += " " +strWhere;
				strSQL = string.Format(strSQL, NewDataCode, shiftType, state);
			}
			catch (Exception ex) {
				this.Err = "��ʽ��SQL���ʱ����Manager.EmployeeRecord.GetEmployeeRecordList.1:" + ex.Message;
				return null;
			}

			//ȡ��Ա���Ա䶯��Ϣ����
			return this.myGetEmployeeRecord(strSQL);
		}

				
		/// <summary>
		/// ȡĳһ��Ա�Ŀ��һ��߲������Ա䶯��Ϣ
		/// </summary>
		/// <param name="emplCode">��Ա����</param>
		/// <param name="state">����״̬</param>
		/// <returns>��Ա���Ա䶯��Ϣ���飬������null</returns>
		public ArrayList GetEmployeeRecordListByEmpl(string emplCode, string state) {
			string strSQL = "";
			//ȡSELECT���
			if (this.GetSQL("Manager.EmployeeRecord.GetEmployeeRecord",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.EmployeeRecord.GetEmployeeRecord�ֶ�!";
				return null;
			}

			string strWhere = "";
			//ȡWHERE���
			if (this.GetSQL("Manager.EmployeeRecord.GetEmployeeRecordListByEmpl",ref strWhere) == -1) {
				this.Err="û���ҵ�Manager.EmployeeRecord.GetEmployeeRecordListByEmpl�ֶ�!";
				return null;
			}

			//��ʽ��SQL���
			try {
				strSQL += " " +strWhere;
				strSQL = string.Format(strSQL, emplCode, state);
			}
			catch (Exception ex) {
				this.Err = "��ʽ��SQL���ʱ����Manager.EmployeeRecord.GetEmployeeRecordListByEmpl:" + ex.Message;
				return null;
			}

			//ȡ��Ա���Ա䶯��Ϣ����
			return this.myGetEmployeeRecord(strSQL);
		}


		/// <summary>
		/// ȡĳһ��Ա�Ŀ��һ��߲������Ա䶯��Ϣ
		/// </summary>
		/// <param name="emplCode">��Ա����</param>
		/// <returns>��Ա���Ա䶯��Ϣ���飬������null</returns>
		public ArrayList GetEmployeeRecordListByEmpl(string emplCode) {
			return this.GetEmployeeRecordListByEmpl(emplCode, "A");
		}


		/// <summary>
		/// ����Ա���Ա䶯��Ϣ���в���һ����¼
		/// </summary>
		/// <param name="employeeRecord">������չ������</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int InsertEmployeeRecord(FS.HISFC.Models.Base.EmployeeRecord employeeRecord) {
			string strSQL="";
			//ȡ���������SQL���
			if(this.GetSQL("Manager.EmployeeRecord.InsertEmployeeRecord",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.EmployeeRecord.InsertEmployeeRecord�ֶ�!";
				return -1;
			}
			try {  
				//ȡ��ˮ��
				employeeRecord.ID = this.GetSequence("Manager.GetConstantID");
				if (employeeRecord.ID == "") return -1;

				string[] strParm = myGetParmEmployeeRecord( employeeRecord );     //ȡ�����б�
				strSQL=string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
			}
			catch(Exception ex) {
				this.Err = "��ʽ��SQL���ʱ����Manager.EmployeeRecord.InsertEmployeeRecord:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		
		
		/// <summary>
		/// ������Ա���Ա䶯��Ϣ����һ����¼
		/// </summary>
		/// <param name="employeeRecord">������չ������</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int UpdateEmployeeRecord(FS.HISFC.Models.Base.EmployeeRecord employeeRecord) {
			string strSQL="";
			//ȡ���²�����SQL���
			if(this.GetSQL("Manager.EmployeeRecord.UpdateEmployeeRecord",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.EmployeeRecord.UpdateEmployeeRecord�ֶ�!";
				return -1;
			}
			try {  
				string[] strParm = myGetParmEmployeeRecord( employeeRecord );     //ȡ�����б�
				strSQL=string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
			}
			catch(Exception ex) {
				this.Err = "��ʽ��SQL���ʱ����Manager.EmployeeRecord.UpdateEmployeeRecord:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		
		
		/// <summary>
		/// ɾ����Ա���Ա䶯��Ϣ����һ����¼
		/// </summary>
		/// <param name="ID">��ˮ��</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int DeleteEmployeeRecord(string ID) {
			string strSQL="";
			//ȡɾ��������SQL���
			if(this.GetSQL("Manager.EmployeeRecord.DeleteEmployeeRecord",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.EmployeeRecord.DeleteEmployeeRecord�ֶ�!";
				return -1;
			}
			try {  
				strSQL=string.Format(strSQL, ID);    //�滻SQL����еĲ�����
			}
			catch(Exception ex) {
				this.Err = "��ʽ��SQL���ʱ����Manager.EmployeeRecord.DeleteEmployeeRecord:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		

		/// <summary>
		/// ������Ա���Ա䶯���ݣ�����ִ�и��²��������û���ҵ����Ը��µ����ݣ������һ���¼�¼
		/// </summary>
		/// <param name="employeeRecord">��Ա���Ա䶯��Ϣʵ��</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int SetEmployeeRecord(FS.HISFC.Models.Base.EmployeeRecord employeeRecord) {
//			int parm;
//			//ִ�и��²���
//			parm = UpdateEmployeeRecord(employeeRecord);
//
//			//���û���ҵ����Ը��µ����ݣ������һ���¼�¼
//			if (parm == 0 ) {
//				parm = InsertEmployeeRecord(employeeRecord);
//			}
//			if (parm == -1 ) {
//				return -1;
//			}
//			
//			//����䶯���ݱ���׼����ͬʱ������Ա��Ϣ���ж�Ӧ�����
//			if (employeeRecord.State == "1") {
//				//��Աʵ��
//				FS.HISFC.Models.RADT.Person person = new FS.HISFC.Models.RADT.Person(); 
//				//��Ա������
//				FS.HISFC.Management.Manager.Person personManager = new Person(); 
				//����trans
//				personManager.SetTrans(this.command.Transaction);
       
				//ȡ��Աȫ����Ϣ
//				person = personManager.GetPersonByID(employeeRecord.Empl.ID);
//				if (person == null) {
//					this.Err = personManager.Err;
//					return -1;
//				}
//
//				//������ұ䶯
//				if (employeeRecord.ShiftType.ID == "DEPT") 
//				{
//					person.Dept.ID   = employeeRecord.NewData.ID ;  //���ұ���
//					person.Dept.Name = employeeRecord.NewData.Name ;  //��������
//					//�ڽ��п��ұ䶯��ͬʱҪ���л���վ�䶯
//					//if (person.PersonType.ID.ToString() == "N") {
//					FS.HISFC.Management.Manager.Department departMent = new Department();
//					//����trans
//					departMent.SetTrans(this.command.Transaction);
//					//����ʿվ�䶯
//					try
//					{
//						person.Nurse =  departMent.GetNurseStationFromDept(person.Dept)[0] as FS.FrameWork.Models.NeuObject;
//					}
//					catch{}
//					//}
//				}
//
//				//����ʿվ�䶯
//				if (employeeRecord.ShiftType.ID == "NURSE") {
//					person.Nurse.ID   = employeeRecord.NewData.ID;  //���ұ���
//					person.Nurse.Name = employeeRecord.NewData.Name;  //��������
//				}
//
//				//�ñ䶯������ݸ�����Ա��Ϣ
//				parm = personManager.Update(person);
//				this.Err = personManager.Err;
//			}
			//����
			//return parm;
			return 1;
		}

        /// <summary>
        /// ��Աת�ƺ������Ա�µĿ��Һͻ���վ
        /// </summary>
        /// <param name="record">��Աת����Ϣ</param>
        /// <returns>1:�ɹ�   -1:ʧ��</returns>
        public int Update(FS.HISFC.Models.Base.EmployeeRecord record)
        {
            string sql = "";
            if (this.GetSQL("Person.UpdateDept", ref sql) == -1)
                return -1;
            /*
             *  UPDATE com_employee   --Ա�������
				SET  					
					dept_code='{0}',   --�������Һ�
					nurse_cell_code = (select t.pardep_code from com_deptstat t where t.dept_code='{0}' and t.stat_code='01' and rownum=1),   --��������վ
					oper_code = '{1}',
					oper_date = sysdate
			    WHERE   empl_code='{2}'    --Ա������ 
                        and                
                        dept_code='{3}'      
             */
            try
            {
                sql = string.Format(sql, record.NewData.ID, this.Operator.ID, record.Employee.ID, record.OldData.ID);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "�ӿڴ���" + ex.Message;
                this.WriteErr();
                return -1;
            }

            if (this.ExecNoQuery(sql) <= 0) return -1;


            return 1;
        }


		/// <summary>
		/// ȡ��Ա���Ա䶯��Ϣ�б�������һ�����߶���
		/// ˽�з����������������е���
		/// </summary>
		/// <param name="SQLString">SQL���</param>
		/// <returns>��Ա���Ա䶯��Ϣ��Ϣ��������</returns>
		private ArrayList myGetEmployeeRecord(string SQLString) {
			ArrayList al=new ArrayList();                
			FS.HISFC.Models.Base.EmployeeRecord employeeRecord; //��Ա���Ա䶯��Ϣʵ��
			this.ProgressBarText="���ڼ�����Ա���Ա䶯��Ϣ...";
			this.ProgressBarValue=0;
			
			//ִ�в�ѯ���
			if (this.ExecQuery(SQLString)==-1) {
				this.Err="�����Ա���Ա䶯��Ϣʱ��ִ��SQL������"+this.Err;
				this.ErrCode="-1";
				return null;
			}
			try {
				while (this.Reader.Read()) {
					//ȡ��ѯ����еļ�¼
					employeeRecord = new FS.HISFC.Models.Base.EmployeeRecord();
					employeeRecord.ID          = this.Reader[0].ToString(); //0 ̨�ʼ�¼��ˮ��
					employeeRecord.Employee.ID     = this.Reader[1].ToString(); //1 Ա������
					employeeRecord.ShiftType.ID= this.Reader[2].ToString(); //2 �䶯���ͣ�DEPT���ң�NURSE��ʿվ�ȣ�
					employeeRecord.OldData.ID  = this.Reader[3].ToString(); //3 ԭ���ϴ���
					employeeRecord.OldData.Name = this.Reader[4].ToString(); //4 ԭ�������� 
					employeeRecord.NewData.ID = this.Reader[5].ToString(); //5 �����ϴ���
					employeeRecord.NewData.Name = this.Reader[6].ToString(); //6 ����������
					employeeRecord.State       = this.Reader[7].ToString(); //7 ��ǰ״̬��0���룬1ȷ�ϣ�2���ϣ�
					employeeRecord.Memo        = this.Reader[8].ToString(); //8 ��ע
					employeeRecord.ApplyOperator.ID   = this.Reader[9].ToString(); //9 �������Ա
					employeeRecord.ApplyTime   = NConvert.ToDateTime(this.Reader[10].ToString()); //10����ʱ��
					employeeRecord.OperEnvironment.ID    = this.Reader[11].ToString();//11 ����Ա����׼�����ϣ�
					employeeRecord.OperDate    = NConvert.ToDateTime(this.Reader[12].ToString()); //12 ����ʱ�䣨��׼�����ϣ�
					employeeRecord.Employee.Name   = this.Reader[13].ToString();//13 Ա������
					this.ProgressBarValue++;
					al.Add(employeeRecord);
				}
			}//�׳�����
			catch(Exception ex) {
				this.Err="�����Ա���Ա䶯��Ϣ��Ϣʱ����"+ex.Message;
				this.ErrCode="-1";
				return null;
			}
			this.Reader.Close();

			this.ProgressBarValue=-1;
			return al;
		}


		/// <summary>
		/// ���update����insert��Ա���Ա䶯��Ϣ��Ĵ����������
		/// </summary>
		/// <param name="employeeRecord">��Ա���Ա䶯��Ϣʵ��</param>
		/// <returns>�ַ�������</returns>
		private string[] myGetParmEmployeeRecord(FS.HISFC.Models.Base.EmployeeRecord employeeRecord) {
			if(employeeRecord.State == "0") {
				//���������״̬�����������ǲ�����
				employeeRecord.ApplyOperator.ID = this.Operator.ID;
				employeeRecord.ApplyTime = this.GetDateTimeFromSysDateTime();
			}
			string[] strParm={   
								employeeRecord.ID,           //̨�ʼ�¼��ˮ��
								employeeRecord.Employee.ID,      //Ա������
								employeeRecord.ShiftType.ID, //�䶯���ͣ�DEPT���ң�NURSE��ʿվ�ȣ�
								employeeRecord.OldData.ID , //ԭ���ϴ���
								employeeRecord.OldData.Name , //ԭ��������
								employeeRecord.NewData.ID,  //�����ϴ���
								employeeRecord.NewData.Name,  //����������
								employeeRecord.State ,       //��ǰ״̬��0���룬1ȷ�ϣ�2���ϣ�
								employeeRecord.Memo ,        //��ע
								employeeRecord.ApplyOperator.ID ,   //�������Ա
								employeeRecord.ApplyTime.ToString(),//����ʱ��
								this.Operator.ID             //����Ա����׼�����ϣ�
			};								 
			return strParm;
		}

		
	}

}

#region SQL
//<SQL id="Manager.EmployeeRecord.GetEmployeeRecord" Memo="ȡ��Ա���Ա䶯��Ϣ" input="none" output="3">
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
//<SQL id="Manager.EmployeeRecord.GetEmployeeRecord.Where" Memo="ȡ��Ա���Ա䶯��Ϣ�б�" input="none" output="3">
//<!--   --><![CDATA[  
//			AND		COM_EMPLOYEE_RECORD.EMPL_CODE    = '{0}' 
//			AND		COM_EMPLOYEE_RECORD.SHIFT_TYPE   = '{1}' 
//			AND		COM_EMPLOYEE_RECORD.STATE        = '{2}' 
//			AND		ROWNUM = 1 
//]]></SQL>
//<SQL id="Manager.EmployeeRecord.GetEmployeeRecordList" Memo="ȡ��Ա���Ա䶯��Ϣ�б�" input="none" output="3">
//<!--   --><![CDATA[  
//			AND		COM_EMPLOYEE_RECORD.EMPL_CODE    = '{0}' 
//			AND		COM_EMPLOYEE_RECORD.OPER_DATE   >= '{1}' 
//			AND		COM_EMPLOYEE_RECORD.OPER_DATE   <= '{2}' 
//]]></SQL>
//<SQL id="Manager.EmployeeRecord.InsertEmployeeRecord" Memo="����Ա���Ա䶯��Ϣ���в���һ����¼ input="none" output="3">
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
//<SQL id="Manager.EmployeeRecord.UpdateEmployeeRecord" Memo="������Ա���Ա䶯��Ϣ����һ����¼" input="none" output="3">
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
//<SQL id="Manager.EmployeeRecord.DeleteEmployeeRecord" Memo="ɾ����Ա���Ա䶯��Ϣ����һ����¼" input="none" output="3">
//<!--   --><![CDATA[ 
//			DELETE FROM COM_EMPLOYEE_RECORD 
//			WHERE	PARENT_CODE  = '[��������]'
//			AND		CURRENT_CODE = '[��������]'
//			AND		RECORD_NO = '{0}'        
//]]></SQL>
#endregion