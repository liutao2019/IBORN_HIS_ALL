using System;
using FS.HISFC.Models;
using System.Collections;
using System.Collections.Generic;
using FS.FrameWork.Models;
using FS.HISFC.Models.Operation;
using System.Data;
namespace FS.HISFC.BizLogic.Operation
{
	#region ��������ҵ������
	//���������������ҵ��
	//������Ա�Űࡢ������̨���䡢�������롢�����Ҹ�������������
	//�����š��շ�ģ��ά���������������ѡ������˷ѡ������Ǽǡ�����Ǽǡ�����ȡ��
	//ҵ�������
	//�������롢�������š�����ȡ�� ҵ��ʵ���Ǵ����������뵥�����Ϣ
	//�����Ǽ� ҵ��ʵ���Ǵ��������ǼǼ�¼�����Ϣ
	//����Ǽ� ҵ��ʵʩ�Ǵ�������ǼǼ�¼�����Ϣ
	//�����������ѡ������˷� ҵ��ʵ���ǵ��÷��ýӿڴ������
	//�շ�ģ��ά�� ҵ��ʵ���ǵ���ͨ��ģ����ά��������ģ��
	//������Ա���� ҵ��ʵ���ǵ���ͨ���Ű���ά���������Ű���Ϣ
	//������̨���� ҵ��ʵ���Ǵ�������̨����Ϣ
	#endregion
	/// [��������: ����������]<br></br>
	/// [�� �� ��: ����ȫ]<br></br>
	/// [����ʱ��: 2006-09-28]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public abstract class Operation : FS.FrameWork.Management.Database
	{
		public Operation()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

#region �ֶ�

		private OperationRoleEnumService roleType = new OperationRoleEnumService();
		private FS.HISFC.BizLogic.Operation.OpsTableManage TableManage = new OpsTableManage();

#endregion		
        /// <summary>
		/// ������Ա�Ű�
		/// </summary>
		/// <param name=""></param>
		/// <returns>0 success -1 fail</returns>
		public int PlanOperator()
		{
			return 0;
		}
		
		#region ��ȡ�������뵥��Ϣ
        /// <summary>
        /// ��ȡ��ҪSQL 
        /// </summary>
        /// <returns></returns>
        private string GetOperationSql()
        {
            string strSql = string.Empty;
            if (this.Sql.GetSql("Operator.Operator.Select.2", ref strSql) == -1)
            {
                return null;
            }
            return strSql;
        }
        /// <summary>
        /// ��ȡ��ҪSQL (��������������������)
        /// </summary>
        /// <returns></returns>
        private string GetOperationSqlNew()
        {
            string strSql = string.Empty;
            if (this.Sql.GetSql("Operator.Operator.Select.New", ref strSql) == -1)
            {
                return null;
            }
            return strSql;
        }
        /// <summary>
        /// ��ȡ��ҪSQL (�������������������Ų˵��µ������δ�շ�����)
        /// </summary>
        /// <returns></returns>
        private string GetAlreadyOperationSql()
        {
            string strSql = string.Empty;
            if (this.Sql.GetSql("Operator.Operator.Select.AlreadyOps", ref strSql) == -1)
            {
                return null;
            }
            return strSql;
        }
		/// <summary>
		/// ����������Ż���������뵥
		/// </summary>
		/// <param name="OperatorNo">�������</param>
		/// <returns>�������뵥����</returns>
		public FS.HISFC.Models.Operation.OperationAppllication GetOpsApp(string operationNo)
		{
			FS.HISFC.Models.Operation.OperationAppllication opsApp = new FS.HISFC.Models.Operation.OperationAppllication();
			ArrayList myAl = new ArrayList();
			//ҵ�������ѡ������ʱ��С�ڸ���ʱ���������Ч���ѽ��й��������ŵ��������뵥��			
            string strSql = GetOperationSql();
            if (strSql == null)
            {
                return null;
			}

			string strSqlwhere = string.Empty;
			if(this.Sql.GetSql("Operator.Operator.GetApplication.0",ref strSqlwhere) == -1) 
			{
				return opsApp;
			}

			try
			{				
				strSqlwhere = string.Format(strSqlwhere,operationNo);
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operation.Operation.GetOpsApp";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return opsApp;            
			}			

			strSql = strSql + " \n" + strSqlwhere;
			myAl =  this.GetOpsAppListFromSql(strSql);
			//myAl��Ӧ��ֻ��һ��Ԫ��
			if(myAl.Count == 0)
			{
				return null;
			}
			
			opsApp = myAl[0] as FS.HISFC.Models.Operation.OperationAppllication;
			

			return opsApp;
		}

        /// <summary>
        /// ��ȡ�����Ѿ�������������������б�
        /// </summary>
        /// <param name="?"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ArrayList GetCancelAppList(string deptCode,DateTime beginTime,DateTime endTime)
        {
            string strSql = this.GetOperationSql();
            string strSql1 = string.Empty;
            if(this.Sql.GetSql("Operator.Operator.GetCancelApplicationApply",ref strSql1) == -1)
            {
                this.Err = "û���ҵ�SQL���Operator.Operator.GetCancelApplicationApply";
                return null;
            }
            strSql = strSql + "\n" + strSql1;
            strSql = string.Format(strSql, deptCode, beginTime.ToString(), endTime.ToString());
            ArrayList alCancelAppList =  this.GetOpsAppListFromSql(strSql);
            return alCancelAppList;
        }

		/// <summary>
		/// ��ȡ���ߵ��������뵥��Ϣ
		/// </summary>
		/// <param name="PatientInfo">���߶���</param>
		/// <param name="Pasource">��������1����2סԺ</param>
		/// <param name="endTime">��ѯ����ʱ��</param>
		/// <returns>���ߵ��������뵥��������</returns>
		public ArrayList GetOpsAppList(FS.HISFC.Models.RADT.PatientInfo PatientInfo,string Pasource,string endTime)
		{			
			if(PatientInfo == null)
			{
				return null;	//����Ļ��߶���Ϊ��
			}

			ArrayList myAl = new ArrayList();

			//ҵ�������ѡ���û�������ʱ��С�ڸ���ʱ���������Ч��δ�����������뵥��

			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.Operator.GetApplication.1",ref strSql) == -1) 
			{
				return myAl;
			}

			try
			{
				switch(Pasource)
				{
					case "1":
						//strSql = string.Format(strSql,PatientInfo.Patient.PID.CardNo,Pasource,endTime);
						strSql = string.Format(strSql,PatientInfo.ID.ToString(),Pasource,endTime);
						break;
					case "2":
						//����סԺ��ˮ�Ÿ�ֵ			
						//PatientInfo.Patient.ID = this.GetInPatientNo(PatientInfo.Patient.PID.ID.ToString());
						//strSql = string.Format(strSql,PatientInfo.Patient.PID.PatientNo,Pasource,endTime);
						strSql = string.Format(strSql,PatientInfo.ID.ToString(),Pasource,endTime);
						break;
				}
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operator.Operator.GetOpsAppList 1";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return myAl;            
			}

			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{
					FS.HISFC.Models.Operation.OperationAppllication opsApplication = new FS.HISFC.Models.Operation.OperationAppllication();
					
					opsApplication.ID = Reader[0].ToString();										//�������					
					try
					{
						opsApplication.OperationDoctor.ID = Reader[1].ToString();					//����ҽ��
						// by zlw 2006-5-24
						opsApplication.User01 = Reader[1].ToString();	//����ҽ�����ڿ���
					}
					catch{}
					
					opsApplication.GuideDoctor.ID = Reader[2].ToString();											//ָ��ҽ��	

					opsApplication.PreDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[3].ToString());		//����ԤԼʱ��					
					try
					{
						opsApplication.Duration = System.Convert.ToDecimal(Reader[4].ToString());					//����Ԥ����ʱ					
					}
					catch{}
					opsApplication.AnesType.ID = Reader[5].ToString();												//��������

					opsApplication.ExeDept.ID = Reader[6].ToString();												//ִ�п���

					opsApplication.OperateRoom = opsApplication.ExeDept as FS.HISFC.Models.Base.Department;	//������(������Ҫ�����뵥��������˵�������Ҽ�ִ�п���)

					opsApplication.TableType = Reader[7].ToString();					//0��̨1��̨2��̨					
					
					opsApplication.ApplyDoctor.ID = Reader[8].ToString();				//����ҽ��

					
					opsApplication.ApplyDoctor.Dept.ID = Reader[9].ToString();//�������
					
					opsApplication.ApplyDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());	//����ʱ��
					opsApplication.ApplyNote = Reader[11].ToString();					//���뱸ע					

					opsApplication.ApproveDoctor.ID = Reader[12].ToString();//����ҽ��					

					opsApplication.ApproveDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[13].ToString());	//����ʱ��
					opsApplication.ApproveNote = Reader[14].ToString();					//������ע
					opsApplication.OperationType.ID = Reader[15].ToString();				//������ģ
					opsApplication.InciType.ID = Reader[16].ToString();					//�п�����					
					
					string strGerm = Reader[17].ToString();						//1 �о� 0�޾�
					opsApplication.IsGermCarrying = FS.FrameWork.Function.NConvert.ToBoolean(strGerm);
					
					opsApplication.ScreenUp = Reader[18].ToString();					//1 Ļ�� 2 Ļ��					
					opsApplication.BloodType.ID = Reader[19].ToString();				//ѪҺ�ɷ�					
					try
					{
						opsApplication.BloodNum =  System.Convert.ToDecimal(Reader[20].ToString());		//Ѫ��
					}
					catch{}
					opsApplication.BloodUnit = Reader[21].ToString();					//��Ѫ��λ
					opsApplication.OpsNote = Reader[22].ToString();						//����ע������
					opsApplication.AneNote = Reader[23].ToString();						//����ע������				
					opsApplication.ExecStatus = Reader[24].ToString();					//1�������� 2 �������� 3�������� 4�������
					
					
					string strFinished = Reader[25].ToString();					//0δ������/1��������
					opsApplication.IsFinished = FS.FrameWork.Function.NConvert.ToBoolean(strFinished);
					
					
					string strAnesth = Reader[26].ToString();					//0δ����/1������
					opsApplication.IsAnesth = FS.FrameWork.Function.NConvert.ToBoolean(strAnesth);
					
					opsApplication.Folk = Reader[27].ToString();						//ǩ�ּ���
					opsApplication.RelaCode.ID = Reader[28].ToString();					//������ϵ
					opsApplication.FolkComment = Reader[29].ToString();					//�������
					
					try
					{
						string strUrgent = Reader[30].ToString();					//�Ӽ�����,1��/0��
						opsApplication.IsUrgent = FS.FrameWork.Function.NConvert.ToBoolean(strUrgent);
					}
					catch{}
					try
					{
						string strChange = Reader[31].ToString();					//1��Σ/0��
						opsApplication.IsChange = FS.FrameWork.Function.NConvert.ToBoolean(strChange);
					}
					catch{}
					try
					{
						string strHeavy = Reader[32].ToString();						//1��֢/0��
						opsApplication.IsHeavy = FS.FrameWork.Function.NConvert.ToBoolean(strHeavy);
					}
					catch{}
					try
					{
						opsApplication.IsSpecial = FS.FrameWork.Function.NConvert.ToBoolean(Reader[33].ToString());	//1��������/0��
					}
					catch{}

					opsApplication.User.ID = Reader[34].ToString();	//����Ա

					try
					{
						opsApplication.IsUnite = FS.FrameWork.Function.NConvert.ToBoolean(Reader[35].ToString());//1�ϲ�/0��	
					}
					catch{}
					opsApplication.OperateKind = Reader[37].ToString();					//1��ͨ2����3��Ⱦ				
					try
					{
						string strIsNeedAcco = Reader[38].ToString();					//�Ƿ���Ҫ��̨��ʿ
						opsApplication.IsAccoNurse = FS.FrameWork.Function.NConvert.ToBoolean(strIsNeedAcco);
					}
					catch{}
					try
					{
						string strIsNeedPrep = Reader[39].ToString();					//�Ƿ���ҪѲ�ػ�ʿ
						opsApplication.IsPrepNurse = FS.FrameWork.Function.NConvert.ToBoolean(strIsNeedPrep);
					}
					catch{}
					opsApplication.RoomID=Reader[40].ToString();
					opsApplication.PatientInfo = PatientInfo.Clone();					//���߻�����Ϣ					
					opsApplication.PatientSouce = Pasource;									//1����2סԺ
					opsApplication.IsValid = true;										//1��Ч0��Ч	
						
					myAl.Add(opsApplication);
				}
			}
			catch(Exception ex)
			{
				this.Err="��û����������뵥��Ϣ����"+ex.Message;
				this.ErrCode="-1";
				this.WriteErr();
				return myAl;
			}
			this.Reader.Close();
			try
			{
				foreach(FS.HISFC.Models.Operation.OperationAppllication opsApplication in myAl)
				{
					opsApplication.DiagnoseAl = this.GetIcdFromApp(opsApplication);							//����б�					
					opsApplication.OperationInfos = this.GetOpsInfoFromApp(opsApplication.ID);				//������Ŀ��Ϣ�б�				
					opsApplication.RoleAl = this.GetRoleFromApp(opsApplication.ID);							//��Ա��ɫ�б�
					//�������Ը�ֵ��Ϊͻ�����ֲ����벿��ҵ����÷���
					foreach(FS.HISFC.Models.Operation.ArrangeRole thisRole in opsApplication.RoleAl)
					{
						if(thisRole.RoleType.ID.ToString()== FS.HISFC.Models.Operation.EnumOperationRole.Helper1.ToString()
							|| thisRole.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper2.ToString()
							||thisRole.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper3.ToString())
							//����ҽʦ�б�
							opsApplication.HelperAl.Add(thisRole.Clone());
					}
					opsApplication.AppaRecAl = GetAppaRecFromApp(opsApplication.ID);//�������ϰ����б�
				}
			}
			catch(Exception ex)
			{
				this.Err="��û��������б���Ϣ����"+ex.Message;
				this.ErrCode="-1";
				this.WriteErr();
				return myAl;
			}
			return myAl;
		}		
		/// <summary>
		/// ��ȡ���������������뵥��Ϣ (����)
		/// </summary>
		/// <param name="OpsRoomID">�������ұ���</param>
		/// <param name="endTime">��ѯ����ʱ��</param>
		/// <returns>�������뵥��������</returns>
		public ArrayList GetOpsAppList( string opsRoomID, string endTime )
		{
			ArrayList myAl = new ArrayList();
			//ҵ�������ѡ������������ʱ��С�ڸ���ʱ���������Ч��δ�����������뵥��			
            string strSql = GetOperationSql();
            if (strSql == null) 
			{
				return myAl;
			}
			string strSqlwhere = string.Empty;
			if(this.Sql.GetSql("Operator.Operator.GetApplication.2",ref strSqlwhere) == -1) 
			{
				return myAl;
			}

			try
			{				
				strSqlwhere = string.Format(strSqlwhere,opsRoomID,endTime);			
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operator.Operator.GetOpsAppList 2";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return myAl;            
			}
			strSql = strSql + " \n" + strSqlwhere;
			return GetOpsAppListFromSql(strSql);
		}		
		/// <summary>
		/// ��ȡ���������������뵥��Ϣ (����)
		/// </summary>
		/// <param name="OpsRoomID">�������ұ���</param>
		/// <param name="beginTime">��ʼʱ��</param>
		/// <param name="endTime">��ѯ����ʱ��</param>
        /// <param name="isNeedApprove">�Ƿ���Ҫ��� true �� false ��</param>
		/// <returns>�������뵥��������</returns>
        public ArrayList GetOpsAppList(string OpsRoomID, string beginTime, string endTime, bool isNeedApprove)
		{
			ArrayList myAl = new ArrayList();

            string strSql = GetOperationSqlNew();
            if (strSql == null)
            {
                return null;
            }
			
			string strSqlwhere = string.Empty;
            if (isNeedApprove == false)
            {
                if (this.Sql.GetSql("Operator.Operator.GetApplication.6", ref strSqlwhere) == -1)
                {
                    return myAl;
                }
            }
            else
            {
                if (this.Sql.GetSql("Operator.Operator.GetApplication.NeedApprove", ref strSqlwhere) == -1)
                {
                    return myAl;
                }
            }

			try
			{				
				strSqlwhere = string.Format(strSqlwhere,OpsRoomID,beginTime,endTime);			
			}
			catch(Exception ex)
			{
                if (isNeedApprove == false)
                {
                    this.Err = "HISFC.Operation.Operation.GetOpsAppList.6";
                }
                else
                {
                    this.Err = "HISFC.Operation.Operation.GetOpsAppList.NeedApprove";
                }
				this.ErrCode = ex.Message;
				this.WriteErr();
				return myAl;            
			}
			strSql = strSql + " \n" + strSqlwhere;
			return GetOpsAppListFromSqlNew(strSql);
		}

        /// <summary>
        /// ��ȡ�����������뵥��Ϣ 
        /// </summary>{66E970CB-3342-4c38-9B14-0E514DDC82A3}
        /// <param name="cardno">���￨��</param>
        /// <returns>�������뵥��������</returns>
        public ArrayList GetOpsAppListByCardNo(string cardno)
        {
            ArrayList myAl = new ArrayList();

            string strSql = GetOperationSqlNew();
            if (strSql == null)
            {
                return null;
            }

            string strSqlwhere = string.Empty;

            if (this.Sql.GetSql("Operator.Operator.GetApplication.ByCardNoMZ", ref strSqlwhere) == -1)
            {
                return myAl;
            }
           
            try
            {
                strSqlwhere = string.Format(strSqlwhere,cardno);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.WriteErr();
                return myAl;
            }
            strSql = strSql + " \n" + strSqlwhere;
            return GetOpsAppListFromSqlNew(strSql);
        }

        /// <summary>
		/// ��ȡ�������������δ�շѵ�������Ϣ�����������������Ų˵��µ������δ�շ����� add by zhy
		/// </summary>
		/// <param name="OpsRoomID">�������ұ���</param>
		/// <param name="beginTime">��ʼʱ��</param>
		/// <param name="endTime">��ѯ����ʱ��</param>
        /// <param name="isNeedApprove">�Ƿ���Ҫ��� true �� false ��</param>
		/// <returns>�������뵥��������</returns>
        public ArrayList GetAlreadyOpsList(string OpsRoomID, string beginTime, string endTime)
        {
            ArrayList myAl = new ArrayList();
            string strSql = GetAlreadyOperationSql();
            if (strSql == null)
            {
                return null;
            }
            try
            {
                strSql = string.Format(strSql, OpsRoomID, beginTime, endTime);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.WriteErr();
                return myAl;
            }
           
            //return GetAlreadyOpsListFromSql(strSql);
            return GetOpsAppListFromSqlNew(strSql);
        }
		/// <summary>
		/// ��ȡĳ��ʱ����ڵ����е���Ч���������룬�����շ�ʱ������
		/// </summary>
		/// <param name="beginTime"></param>
		/// <param name="endTime"></param>
		/// <returns></returns>
		public ArrayList GetAnaesthAppList(string beginTime,string endTime)
		{
			ArrayList myAl = new ArrayList();
			
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.Operation.Select.2",ref strSql) == -1) 
			{
				return myAl;
			}

			string strSqlwhere = string.Empty;
			if(this.Sql.GetSql("Operator.Operation.GetApplication.zjy",ref strSqlwhere) == -1) 
			{
				return myAl;
			}

			try
			{				
				strSqlwhere = string.Format(strSqlwhere,beginTime,endTime);			
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operator.Operator.GetOpsAppList zjy";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return myAl;            
			}
			strSql = strSql + " \n" + strSqlwhere;
			return GetOpsAppListFromSql(strSql);
		}
		/// <summary>
		/// ��ȡĳ��������ĳ��ʱ�������е���Ч���������뵥��Ϣ
		/// </summary>
		/// <param name="beginTime"></param>
		/// <param name="endTime"></param>
		/// <param name="DeptId"></param>
		/// <returns></returns>
		public ArrayList GetApplistByDeptIdandTime(string beginTime,string endTime,string DeptId)
		{
			ArrayList myAl = new ArrayList();

            string strSql = GetOperationSql();
            if (strSql == null)
            {
                return null;
            }

			string strSqlwhere = string.Empty;
			if(this.Sql.GetSql("Operator.Operator.GetApplication.GetApplistByDeptIdandTime",ref strSqlwhere) == -1) 
			{
				return myAl;
			}

			try
			{				
				strSqlwhere = string.Format(strSqlwhere,beginTime,endTime,DeptId);			
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operator.Operator.GetOpsAppList GetApplistByDeptIdandTime";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return myAl;            
			}
			strSql = strSql + " \n" + strSqlwhere;
			return GetOpsAppListFromSql(strSql);
		}
		/// <summary>
		/// ��ȡ�����Ѱ��Ź����������뵥��Ϣ (����)
		/// </summary>
		/// <param name="endTime">��ѯ����ʱ��</param>
		/// <returns>�������뵥��������</returns>
		public ArrayList GetOpsAppList(string endTime)
		{
			ArrayList myAl = new ArrayList();
			//ҵ�������ѡ������ʱ��С�ڸ���ʱ���������Ч���ѽ��й��������ŵ��������뵥��			
            string strSql = GetOperationSql();
            if (strSql == null)
            {
                return null;
            }

			string strSqlwhere = string.Empty;
			if(this.Sql.GetSql("Operator.Operator.GetApplication.3",ref strSqlwhere) == -1) 
			{
				return myAl;
			}

			try
			{				
				strSqlwhere = string.Format(strSqlwhere,endTime);			
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operator.Operator.GetOpsAppList 3";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return myAl;            
			}			
			strSql = strSql + " \n" + strSqlwhere;
			myAl =  GetOpsAppListFromSql(strSql);
			return myAl;
		}		
		/// <summary>
		/// ��ȡ�����Ѱ��Ź����������뵥��Ϣ (����)hxw
		/// </summary>
		/// <param name="beginDate">��ѯ��ʼʱ��</param>
		/// <param name="endTime">��ѯ����ʱ��</param>
		/// <returns>�������뵥��������</returns>
		public ArrayList GetOpsAppList(DateTime beginDate,DateTime endTime)
		{
			ArrayList myAl = new ArrayList();

            string strSql = GetOperationSql();
            if (strSql == null)
            {
                return null;
            }

			string strSqlwhere = string.Empty;
			if(this.Sql.GetSql("Operator.Operator.GetApplication.7",ref strSqlwhere) == -1) 
			{
				return myAl;
			}

			try
			{				
				strSqlwhere = string.Format(strSqlwhere,beginDate.ToString(),endTime.ToString());			
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operator.Operator.GetOpsAppList 7";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return myAl;
			}			
			strSql = strSql + " \n" + strSqlwhere;
			myAl =  GetOpsAppListFromSql(strSql);
			return myAl;
		}
        //{F86F131E-E35D-47ba-A090-38199D6C7584}feng.ch
        /// <summary>
        /// ��ѯ�ض������ҵ������Ѿ����������ŵ���Ϣ
        /// </summary>
        /// <param name="deptCode">�����ұ���</param>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="ds">���ݼ�</param>
        /// <returns>�ɹ��������ݼ�</returns>
        public DataSet GetOpsAppList(string deptCode,DateTime beginDate, DateTime endTime,ref DataSet ds)
        {
            ds=new DataSet();

            string strSql = "";
            if (this.Sql.GetSql("Operator.Operator.GetApplication.ds", ref strSql) == -1)
            {
                this.Err = "Operator.Operator.GetApplication.ds";
                this.WriteErr();
                return null;
            }
            strSql = string.Format(strSql,deptCode,beginDate,endTime);
            if (this.ExecQuery(strSql, ref ds) == -1) return null;
            return ds;
        }	
		/// <summary>
		/// ����סԺ��ˮ�Ż�ȡ�û�������������
		/// </summary>
		/// <param name="inpatientno"></param>
		/// <returns></returns>
		public string GetMaxByPatient(string inpatientno)
		{
			string strSql = string.Empty;
			#region sql
//			  select max(operationno)
//				from met_ops_apply
//			where inpatient_no='{0}'
//				and ynvalid='1'
	   #endregion
			if(this.Sql.GetSql("Operator.Operator.GetApplication.8",ref strSql)==-1)
			{
				return string.Empty;
			}

			try
			{
				strSql=string.Format(strSql,inpatientno);
			}
			catch(Exception e)
			{
				this.Err="HISFC.Operator.Operator.GetMaxByPatient"+e.Message;
				this.ErrCode=e.Message;
				WriteErr();

				return string.Empty;
			}

			return this.ExecSqlReturnOne(strSql);			
		}
		/// <summary>
		/// ��ȡ����δ�Ǽǹ����������뵥��Ϣ (����)
		/// </summary>
		/// <param name="OpsRoomID">�������ұ���</param>
		/// <param name="beginTime">��ѯ��ʼʱ��</param>
		/// <param name="endTime">��ѯ����ʱ��</param>
		/// <param name="Valid">0��Ч 1 ��Ч</param>
		/// <returns>�������뵥��������</returns>
		public ArrayList GetOpsAppList(string OpsRoomID,DateTime beginTime,DateTime endTime,string Valid)
		{
			ArrayList myAl = new ArrayList();
			//ҵ�������ѡ������ʱ��С�ڸ���ʱ���������Ч���ѽ��й��������ŵ��������뵥��			
            string strSql = GetOperationSql();
            if (strSql == null)
            {
                return null;
            }

			string strSqlwhere = string.Empty;
			if(this.Sql.GetSql("Operator.Operator.GetApplication.4",ref strSqlwhere) == -1) 
			{
				return myAl;
			}

			try
			{				
				strSqlwhere = string.Format(strSqlwhere,OpsRoomID,beginTime.ToString(),endTime.ToString(),Valid);			
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operator.Operator.GetOpsList 4";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return myAl;            
			}			
			strSql = strSql + " \n" + strSqlwhere;
			myAl = GetOpsAppListFromSql(strSql);
			return myAl;
		}
		/// <summary>
		/// ��ȡ�Ѱ���δ�Ǽ��������Լ������ӵ�������δ�շ�״̬�Ļ���(by zhy)
		/// </summary>
		/// <param name="OpsRoomID"></param>
		/// <param name="begin"></param>
		/// <param name="end"></param>
		/// <param name="Isvalid"></param>
		/// <returns></returns>
		public ArrayList GetOpsAppList(string OpsRoomID,DateTime begin,DateTime end,bool Isvalid)
		{
			ArrayList myAl = new ArrayList();
			//ҵ�������ѡ������ʱ��С�ڸ���ʱ���������Ч���ѽ��й��������ŵ��������뵥��			
            string strSql = GetOperationSql();
            if (strSql == null)
            {
                return null;
            }
			string strSqlwhere = string.Empty;
			if(this.Sql.GetSql("Operator.Operator.GetApplication.9",ref strSqlwhere) == -1) 
			{
				return myAl;
			}

			try
			{				
				strSqlwhere = string.Format(strSqlwhere,OpsRoomID,begin.ToString(),end.ToString(),
					FS.FrameWork.Function.NConvert.ToInt32(Isvalid));			
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operator.Operator.GetOpsList 9";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return myAl;
			}			
			strSql = strSql + " \n" + strSqlwhere;
			myAl = GetOpsAppListFromSql(strSql);
			return myAl;
		}

        //==============================================================================================
        //==============================================================================================
        #region �����Ӵ���
        //�޸��ˣ�·־�� ʱ�䣺2007-4-12 
        //Ŀ�ģ��������Ǽ������Ӱ���סԺ�Ų�ѯ����סԺ���԰��ŵ���������
        public ArrayList GetOpsAppListByPatient(string OpsRoomID, string Patient_code)
        {
            ArrayList myAl = new ArrayList();
            string strSql = GetOperationSql();
            if (strSql == null)
            {
                return null;
            }
            string strSqlwhere = string.Empty;
            if (this.Sql.GetSql("Operator.Operator.GetApplication.11", ref strSqlwhere) == -1)
            {
                return myAl;
            }
            strSqlwhere = string.Format(strSqlwhere, OpsRoomID, Patient_code);
            strSql = strSql + "\n" + strSqlwhere;
            myAl = GetOpsAppListFromSql(strSql);
            return myAl;
        }

        //Ŀ�ģ��������ǼǴ�������ʾ���ϵ�������
        public ArrayList GetOpsCancelRecord(string ExeDeptID, DateTime begin, DateTime end)
        {
            ArrayList myAl = new ArrayList();
            string strSql = string.Empty;
            if (this.Sql.GetSql("Operator.Operator.Select.3", ref strSql) == -1)
            {
                return myAl;
            }
            strSql = string.Format(strSql, ExeDeptID, begin.ToString(), end.ToString());
            myAl = GetOpsAppListFromSql(strSql);
            return myAl;
        }
       
       
        #endregion

        //===========================================================================================
        //===========================================================================================

		/// <summary>
		/// ��ȡ������ͬһ��������������� 
		/// </summary>
		/// <param name="dept">�����</param>
		/// <param name="begin"></param>
		/// <param name="end"></param>
		/// <returns></returns>
		public ArrayList GetOpsApplistInSameRoom(FS.FrameWork.Models.NeuObject dept,DateTime begin,DateTime end)
		{
			ArrayList myAl = new ArrayList();

            string strSql = GetOperationSql();
            if (strSql == null)
            {
                return null;
            }

			string strSqlwhere = string.Empty;
			if(this.Sql.GetSql("Operator.Operator.GetApplicationInSameRoom",ref strSqlwhere) == -1) 
			{
				return myAl;
			}

			try
			{				
				strSqlwhere = string.Format(strSqlwhere,dept.ID,begin.ToString(),end.ToString());			
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operator.Operator.GetOpsList 10";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return myAl;
			}			
			strSql = strSql + " \n" + strSqlwhere;
			myAl = GetOpsAppListFromSql(strSql);
			return myAl;
		}
		/// <summary>
		/// ��ȡ����һ��ʱ�䷶Χ���������뵥
		/// </summary>
		/// <param name="dept">����</param>
		/// <param name="begin">ԤԼ��ʼʱ��</param>
		/// <param name="end">ԤԼ����ʱ��</param>
		/// <returns></returns>
        public ArrayList GetOpsAppList(FS.FrameWork.Models.NeuObject dept, DateTime begin, DateTime end)
        {
            ArrayList myAl = new ArrayList();

            string strSql = GetOperationSql();
            if (strSql == null)
            {
                return null;
            }
            string strSqlwhere = string.Empty;
            if (this.Sql.GetSql("Operator.Operator.GetApplication.10", ref strSqlwhere) == -1)
            {
                return myAl;
            }
            try
            {
                strSqlwhere = string.Format(strSqlwhere, dept.ID, begin.ToString(), end.ToString());
            }
            catch (Exception ex)
            {
                this.Err = "HISFC.Operator.Operator.GetOpsList 10";
                this.ErrCode = ex.Message;
                this.WriteErr();
                return myAl;
            }
            strSql = strSql + " \n" + strSqlwhere;
            myAl = GetOpsAppListFromSql(strSql);
            return myAl;
        }
		/// <summary>
		/// ��ȡ����δ�������������������뵥��Ϣ (����)
		/// ����Ǽ���
		/// </summary>
		/// <param name="beginTime">��ѯ��ʼʱ��</param>
		/// <param name="endTime">��ѯ����ʱ��</param>
		/// <param name="Valid">0��Ч 1 ��Ч</param>
		/// <returns>�������뵥��������</returns>
		public ArrayList GetOpsAppList(DateTime beginTime,DateTime endTime,string Valid)
		{
			ArrayList myAl = new ArrayList();
			//ҵ�������ѡ������ʱ��С�ڸ���ʱ���������Ч���ѽ��й��������ŵ��������뵥��			
            string strSql = GetOperationSql();
            if (strSql == null)
            {
                return null;
            }

			string strSqlwhere = string.Empty;
			if(this.Sql.GetSql("Operator.Operator.GetApplication.5",ref strSqlwhere) == -1) 
			{
				return myAl;
			}

			try
			{				
				strSqlwhere = string.Format(strSqlwhere,beginTime.ToString(),endTime.ToString(),Valid);			
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operator.Operator.GetOpsAppList 5";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return myAl;            
			}			
			strSql = strSql + " \n" + strSqlwhere;
			myAl = GetOpsAppListFromSql(strSql);
			return myAl;
		}		
		/// <summary>
		/// ��ø���SQL����ѯ�������뵥��������
		/// </summary>
		/// <param name="strSql">ָ���Ĳ�ѯ���</param>
		/// <returns>�������뵥��������</returns>
		private ArrayList GetOpsAppListFromSql(string strSql)
		{
			ArrayList myAl = new ArrayList();

//			FS.HISFC.BizLogic.Manager.Person Person = new FS.HISFC.BizLogic.Manager.Person();
//			FS.HISFC.BizLogic.Manager.Department Department = new FS.HISFC.BizLogic.Manager.Department();
			
			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{
                    FS.HISFC.Models.Operation.OperationAppllication opsApplication = new FS.HISFC.Models.Operation.OperationAppllication();
					opsApplication.ID = Reader[0].ToString();					//�������					
					
					opsApplication.OperationDoctor.ID = Reader[1].ToString();	//����ҽ��				
                    opsApplication.OperationDoctor.Name = this.GetEmployeeName(opsApplication.OperationDoctor.ID);
                        
					opsApplication.GuideDoctor.ID = Reader[2].ToString();		//ָ��ҽ��	
					
					opsApplication.PreDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[3].ToString());		//����ԤԼʱ��					
					
					if(Reader.IsDBNull(4))
						opsApplication.Duration=0m;
					else
                        opsApplication.Duration =  System.Convert.ToDecimal(Reader[4].ToString());		//����Ԥ����ʱ					
					
					opsApplication.AnesType.ID = Reader[5].ToString();					//��������					
					
					opsApplication.ExeDept.ID = Reader[6].ToString();//ִ�п���					

					opsApplication.OperateRoom = 
						opsApplication.ExeDept as FS.HISFC.Models.Base.Department;	//������(������Ҫ�����뵥��������˵�������Ҽ�ִ�п���)
					
					opsApplication.TableType = Reader[7].ToString();					//0��̨1��̨2��̨					
					
					opsApplication.ApplyDoctor.ID = Reader[8].ToString();				//����ҽ��
                    opsApplication.ApplyDoctor.Name = this.GetEmployeeName(opsApplication.ApplyDoctor.ID);
                        
					
					opsApplication.ApplyDoctor.Dept.ID = Reader[9].ToString();//�������
					
					opsApplication.ApplyDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());	//����ʱ��
					opsApplication.ApplyNote = Reader[11].ToString();					//���뱸ע					
					
					opsApplication.ApproveDoctor.ID = Reader[12].ToString();//����ҽ��
					
					opsApplication.ApproveDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[13].ToString());	//����ʱ��
					opsApplication.ApproveNote = Reader[14].ToString();					//������ע					
					opsApplication.OperationType.ID = Reader[15].ToString();				//������ģ
					opsApplication.InciType.ID = Reader[16].ToString();					//�п�����					
					
					string strGerm = Reader[17].ToString();						//1 �о� 0�޾�
					opsApplication.IsGermCarrying = FS.FrameWork.Function.NConvert.ToBoolean(strGerm);
				
					opsApplication.ScreenUp = Reader[18].ToString();					//1 Ļ�� 2 Ļ��					
					opsApplication.BloodType.ID = Reader[19].ToString();				//ѪҺ�ɷ�					
					if(Reader.IsDBNull(20))
						opsApplication.BloodNum=0m;
					else
                        opsApplication.BloodNum =  System.Convert.ToDecimal(Reader[20].ToString());		//Ѫ��
					
					opsApplication.BloodUnit = Reader[21].ToString();					//��Ѫ��λ
					opsApplication.OpsNote = Reader[22].ToString();						//����ע������
					opsApplication.AneNote = Reader[23].ToString();						//����ע������					
					opsApplication.ExecStatus = Reader[24].ToString();					//1�������� 2 �������� 3�������� 4�������
					
					string strFinished = Reader[25].ToString();						//0δ������/1��������
					opsApplication.IsFinished = FS.FrameWork.Function.NConvert.ToBoolean(strFinished);
					
					string strAnesth = Reader[26].ToString();					//0δ����/1������
					opsApplication.IsAnesth = FS.FrameWork.Function.NConvert.ToBoolean(strAnesth);
					
					opsApplication.Folk = Reader[27].ToString();						//ǩ�ּ���
					opsApplication.RelaCode.ID = Reader[28].ToString();					//������ϵ
					opsApplication.FolkComment = Reader[29].ToString();					//�������					
					
					string strUrgent = Reader[30].ToString();					//�Ӽ�����,1��/0��
					opsApplication.IsUrgent = FS.FrameWork.Function.NConvert.ToBoolean(strUrgent);
					
					string strChange = Reader[31].ToString();					//1��Σ/0��
					opsApplication.IsChange = FS.FrameWork.Function.NConvert.ToBoolean(strChange);
				
					string strHeavy = Reader[32].ToString();						//1��֢/0��
					opsApplication.IsHeavy = FS.FrameWork.Function.NConvert.ToBoolean(strHeavy);
					
					string strSpecial = Reader[33].ToString();					//1��������/0��
					opsApplication.IsSpecial = FS.FrameWork.Function.NConvert.ToBoolean(strSpecial);
					
					opsApplication.User.ID = Reader[34].ToString();	//����Ա
				
					opsApplication.IsUnite = FS.FrameWork.Function.NConvert.ToBoolean(Reader[35].ToString());//1�ϲ�/0��
					
					opsApplication.OperateKind = Reader[37].ToString();					//1��ͨ2����3��Ⱦ
					opsApplication.PatientSouce = Reader[38].ToString();					//1����/2סԺ					
					//try
					//{
						//thisOpsApp.PatientInfo.Patient.PID.PatientNo = Reader[39].ToString();//סԺ��
						//thisOpsApp.PatientInfo.Patient.ID  = this.GetInPatientNo(thisOpsApp.PatientInfo.Patient.PID.PatientNo);//סԺ��ˮ��
					//}
					//catch{}
                    
					opsApplication.PatientInfo.ID = Reader[39].ToString();//�����/סԺ��ˮ��
                    if (opsApplication.PatientSouce == "2")
                    {
                        opsApplication.PatientInfo = this.GetPatientInfo(opsApplication.PatientInfo.ID);
                    }
                    else
                    {
                        FS.HISFC.Models.Registration.Register regObj = this.GetRegInfo(opsApplication.PatientInfo.ID);
                        FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                        patientInfo.ID = regObj.ID;//��ˮ��
                        patientInfo.PID.PatientNO = regObj.PID.CardNO;//����
                        patientInfo.PID.CardNO = regObj.PID.CardNO;//����
                        patientInfo.Name = regObj.Name;//����
                        patientInfo.Birthday = regObj.Birthday;
                        patientInfo.Sex.ID = regObj.Sex.ID;
                        if (regObj.SeeDoct.Dept.ID == null || regObj.SeeDoct.Dept.ID == "")
                        {
                            patientInfo.PVisit.PatientLocation.Dept.ID = regObj.DoctorInfo.Templet.Dept.ID;
                            patientInfo.PVisit.PatientLocation.Dept.Name = regObj.DoctorInfo.Templet.Dept.Name;
                        }
                        else
                        {
                            patientInfo.PVisit.PatientLocation.Dept.ID = regObj.SeeDoct.Dept.ID;
                        }
                        patientInfo.Pact.PayKind.ID = regObj.Pact.PayKind.ID;
                        opsApplication.PatientInfo = patientInfo;
                    }
					//-----------------------------------------------------------------------------------
					opsApplication.PatientInfo.PID.ID = Reader[40].ToString();//���￨��/סԺ��
					opsApplication.PatientInfo.PID.CardNO = Reader[40].ToString();//���￨��
					opsApplication.PatientInfo.PID.PatientNO = Reader[40].ToString();//סԺ��
					//-----------------------------------------------------------------------------------
					opsApplication.PatientInfo.Name = Reader[41].ToString();	//����
					opsApplication.PatientInfo.Sex.ID = Reader[42].ToString();	//�Ա�
					opsApplication.PatientInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[43].ToString());//����					
					
					if(Reader.IsDBNull(44))
						opsApplication.PatientInfo.FT.PrepayCost=0m;
					else
						opsApplication.PatientInfo.FT.PrepayCost =  System.Convert.ToDecimal(Reader[44].ToString());//Ԥ����
					
					opsApplication.PatientInfo.PVisit.PatientLocation.Dept.ID = Reader[45].ToString();//סԺ����
					
					opsApplication.PatientInfo.PVisit.PatientLocation.Bed.ID = Reader[46].ToString();//������
					opsApplication.PatientInfo.BloodType.ID = Reader[47].ToString();//Ѫ��					
					try
					{
						opsApplication.OpsTable.ID = Reader[48].ToString();				//����̨
						opsApplication.OpsTable.Name = 
							this.TableManage.GetTableNameFromID(opsApplication.OpsTable.ID.ToString());
					}
					catch{}
					
					string strIsNeedAcco = Reader[49].ToString();					//�Ƿ���Ҫ��̨��ʿ
					opsApplication.IsAccoNurse = FS.FrameWork.Function.NConvert.ToBoolean(strIsNeedAcco);
					
					string strIsNeedPrep = Reader[50].ToString();					//�Ƿ���ҪѲ�ػ�ʿ
					opsApplication.IsPrepNurse = FS.FrameWork.Function.NConvert.ToBoolean(strIsNeedPrep);
					
					opsApplication.RoomID=Reader[51].ToString();
					opsApplication.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[52].ToString());//1��Ч0��Ч	
                    opsApplication.OperationDoctor.Dept.ID = Reader[54].ToString();
                    ////{B9DDCC10-3380-4212-99E5-BB909643F11B}
                    opsApplication.AnesWay = Reader[55].ToString();
                    //{F0B32D1F-99B6-4b1a-8393-C1F89B98543B}
                    opsApplication.Position = Reader[56].ToString();
                    opsApplication.Eneity = Reader[57].ToString();
                    opsApplication.LastTime = Reader[58].ToString();
                    opsApplication.IsOlation = Reader[59].ToString();
					myAl.Add(opsApplication);
                   
				}
			}
			catch(Exception ex)
			{
				this.Err="��û����������뵥��Ϣ����"+ex.Message;
				this.ErrCode="-1";
				this.WriteErr();
				return myAl;
			}
			this.Reader.Close();
			try
			{
				foreach(FS.HISFC.Models.Operation.OperationAppllication opsApp in myAl)
				{
					opsApp.DiagnoseAl = this.GetIcdFromApp(opsApp);	//����б�					
					opsApp.OperationInfos = GetOpsInfoFromApp(opsApp.ID);//������Ŀ��Ϣ�б�				
					opsApp.RoleAl = GetRoleFromApp(opsApp.ID);//��Ա��ɫ�б�
					//�������Ը�ֵ��Ϊͻ�����ֲ����벿��ҵ����÷���
					foreach(FS.HISFC.Models.Operation.ArrangeRole thisRole in opsApp.RoleAl)
					{
						if(thisRole.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper1.ToString()
							|| thisRole.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper2.ToString()
							||thisRole.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper3.ToString())
							//����ҽʦ�б�
							opsApp.HelperAl.Add(thisRole.Clone());
					}
					//thisOpsApp.AppaRecAl = GetAppaRecFromApp(thisOpsApp.OperationNo);//�������ϰ����б�
				}
			}
			catch(Exception ex)
			{
				this.Err="��û��������б���Ϣ����"+ex.Message;
				this.ErrCode="-1";
				this.WriteErr();
				return myAl;
			}
			return myAl;
		}


        /// <summary>
        /// ��ø���SQL����ѯ�������뵥��������,��������meno�������������ŵ�ԭ��add by zhy
        /// </summary>
        /// <param name="strSql">ָ���Ĳ�ѯ���</param>
        /// <returns>�������뵥��������</returns>
        private ArrayList GetOpsAppListFromSqlNew(string strSql)
        {
            ArrayList myAl = new ArrayList();

            //			FS.HISFC.BizLogic.Manager.Person Person = new FS.HISFC.BizLogic.Manager.Person();
            //			FS.HISFC.BizLogic.Manager.Department Department = new FS.HISFC.BizLogic.Manager.Department();

            this.ExecQuery(strSql);
            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Operation.OperationAppllication opsApplication = new FS.HISFC.Models.Operation.OperationAppllication();
                    opsApplication.ID = Reader[0].ToString();					//�������					

                    opsApplication.OperationDoctor.ID = Reader[1].ToString();	//����ҽ��				
                    opsApplication.OperationDoctor.Name = this.GetEmployeeName(opsApplication.OperationDoctor.ID);

                    opsApplication.GuideDoctor.ID = Reader[2].ToString();		//ָ��ҽ��	

                    opsApplication.PreDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[3].ToString());		//����ԤԼʱ��					

                    if (Reader.IsDBNull(4))
                        opsApplication.Duration = 0m;
                    else
                        opsApplication.Duration = System.Convert.ToDecimal(Reader[4].ToString());		//����Ԥ����ʱ					

                    opsApplication.AnesType.ID = Reader[5].ToString();					//��������					

                    opsApplication.ExeDept.ID = Reader[6].ToString();//ִ�п���					

                    opsApplication.OperateRoom =
                        opsApplication.ExeDept as FS.HISFC.Models.Base.Department;	//������(������Ҫ�����뵥��������˵�������Ҽ�ִ�п���)

                    opsApplication.TableType = Reader[7].ToString();					//0��̨1��̨2��̨					

                    opsApplication.ApplyDoctor.ID = Reader[8].ToString();				//����ҽ��
                    opsApplication.ApplyDoctor.Name = this.GetEmployeeName(opsApplication.ApplyDoctor.ID);


                    opsApplication.ApplyDoctor.Dept.ID = Reader[9].ToString();//�������

                    opsApplication.ApplyDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());	//����ʱ��
                    opsApplication.ApplyNote = Reader[11].ToString();					//���뱸ע					

                    opsApplication.ApproveDoctor.ID = Reader[12].ToString();//����ҽ��

                    opsApplication.ApproveDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[13].ToString());	//����ʱ��
                    opsApplication.ApproveNote = Reader[14].ToString();					//������ע					
                    opsApplication.OperationType.ID = Reader[15].ToString();				//������ģ
                    opsApplication.InciType.ID = Reader[16].ToString();					//�п�����					

                    string strGerm = Reader[17].ToString();						//1 �о� 0�޾�
                    opsApplication.IsGermCarrying = FS.FrameWork.Function.NConvert.ToBoolean(strGerm);

                    opsApplication.ScreenUp = Reader[18].ToString();					//1 Ļ�� 2 Ļ��					
                    opsApplication.BloodType.ID = Reader[19].ToString();				//ѪҺ�ɷ�					
                    if (Reader.IsDBNull(20))
                        opsApplication.BloodNum = 0m;
                    else
                        opsApplication.BloodNum = System.Convert.ToDecimal(Reader[20].ToString());		//Ѫ��

                    opsApplication.BloodUnit = Reader[21].ToString();					//��Ѫ��λ
                    opsApplication.OpsNote = Reader[22].ToString();						//����ע������
                    opsApplication.AneNote = Reader[23].ToString();						//����ע������					
                    opsApplication.ExecStatus = Reader[24].ToString();					//1�������� 2 �������� 3�������� 4�������

                    string strFinished = Reader[25].ToString();						//0δ������/1��������
                    opsApplication.IsFinished = FS.FrameWork.Function.NConvert.ToBoolean(strFinished);

                    string strAnesth = Reader[26].ToString();					//0δ����/1������
                    opsApplication.IsAnesth = FS.FrameWork.Function.NConvert.ToBoolean(strAnesth);

                    opsApplication.Folk = Reader[27].ToString();						//ǩ�ּ���
                    opsApplication.RelaCode.ID = Reader[28].ToString();					//������ϵ
                    opsApplication.FolkComment = Reader[29].ToString();					//�������					

                    string strUrgent = Reader[30].ToString();					//�Ӽ�����,1��/0��
                    opsApplication.IsUrgent = FS.FrameWork.Function.NConvert.ToBoolean(strUrgent);

                    string strChange = Reader[31].ToString();					//1��Σ/0��
                    opsApplication.IsChange = FS.FrameWork.Function.NConvert.ToBoolean(strChange);

                    string strHeavy = Reader[32].ToString();						//1��֢/0��
                    opsApplication.IsHeavy = FS.FrameWork.Function.NConvert.ToBoolean(strHeavy);

                    string strSpecial = Reader[33].ToString();					//1��������/0��
                    opsApplication.IsSpecial = FS.FrameWork.Function.NConvert.ToBoolean(strSpecial);

                    opsApplication.User.ID = Reader[34].ToString();	//����Ա

                    opsApplication.IsUnite = FS.FrameWork.Function.NConvert.ToBoolean(Reader[35].ToString());//1�ϲ�/0��

                    opsApplication.OperateKind = Reader[37].ToString();					//1��ͨ2����3��Ⱦ
                    opsApplication.PatientSouce = Reader[38].ToString();					//1����/2סԺ					
                    //try
                    //{
                    //thisOpsApp.PatientInfo.Patient.PID.PatientNo = Reader[39].ToString();//סԺ��
                    //thisOpsApp.PatientInfo.Patient.ID  = this.GetInPatientNo(thisOpsApp.PatientInfo.Patient.PID.PatientNo);//סԺ��ˮ��
                    //}
                    //catch{}

                    opsApplication.PatientInfo.ID = Reader[39].ToString();//�����/סԺ��ˮ��
                    if (opsApplication.PatientSouce == "2")
                    {
                        opsApplication.PatientInfo = this.GetPatientInfo(opsApplication.PatientInfo.ID);
                    }
                    else
                    {
                        FS.HISFC.Models.Registration.Register regObj = this.GetRegInfo(opsApplication.PatientInfo.ID);
                        FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                        patientInfo.ID = regObj.ID;//��ˮ��
                        patientInfo.PID.PatientNO = regObj.PID.CardNO;//����
                        patientInfo.PID.CardNO = regObj.PID.CardNO;//����
                        patientInfo.Name = regObj.Name;//����
                        patientInfo.Birthday = regObj.Birthday;
                        patientInfo.Sex.ID = regObj.Sex.ID;
                        if (regObj.SeeDoct.Dept.ID == null || regObj.SeeDoct.Dept.ID == "")
                        {
                            patientInfo.PVisit.PatientLocation.Dept.ID = regObj.DoctorInfo.Templet.Dept.ID;
                            patientInfo.PVisit.PatientLocation.Dept.Name = regObj.DoctorInfo.Templet.Dept.Name;
                        }
                        else
                        {
                            patientInfo.PVisit.PatientLocation.Dept.ID = regObj.SeeDoct.Dept.ID;
                        }
                        patientInfo.Pact.PayKind.ID = regObj.Pact.PayKind.ID;
                        opsApplication.PatientInfo = patientInfo;
                    }
                    //-----------------------------------------------------------------------------------
                    opsApplication.PatientInfo.PID.ID = Reader[40].ToString();//���￨��/סԺ��
                    opsApplication.PatientInfo.PID.CardNO = Reader[40].ToString();//���￨��
                    opsApplication.PatientInfo.PID.PatientNO = Reader[40].ToString();//סԺ��
                    //-----------------------------------------------------------------------------------
                    opsApplication.PatientInfo.Name = Reader[41].ToString();	//����
                    opsApplication.PatientInfo.Sex.ID = Reader[42].ToString();	//�Ա�
                    opsApplication.PatientInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[43].ToString());//����					

                    if (Reader.IsDBNull(44))
                        opsApplication.PatientInfo.FT.PrepayCost = 0m;
                    else
                        opsApplication.PatientInfo.FT.PrepayCost = System.Convert.ToDecimal(Reader[44].ToString());//Ԥ����

                    opsApplication.PatientInfo.PVisit.PatientLocation.Dept.ID = Reader[45].ToString();//סԺ����

                    opsApplication.PatientInfo.PVisit.PatientLocation.Bed.ID = Reader[46].ToString();//������
                    opsApplication.PatientInfo.BloodType.ID = Reader[47].ToString();//Ѫ��					
                    try
                    {
                        opsApplication.OpsTable.ID = Reader[48].ToString();				//����̨
                        opsApplication.OpsTable.Name =
                            this.TableManage.GetTableNameFromID(opsApplication.OpsTable.ID.ToString());
                    }
                    catch { }

                    string strIsNeedAcco = Reader[49].ToString();					//�Ƿ���Ҫ��̨��ʿ
                    opsApplication.IsAccoNurse = FS.FrameWork.Function.NConvert.ToBoolean(strIsNeedAcco);

                    string strIsNeedPrep = Reader[50].ToString();					//�Ƿ���ҪѲ�ػ�ʿ
                    opsApplication.IsPrepNurse = FS.FrameWork.Function.NConvert.ToBoolean(strIsNeedPrep);

                    opsApplication.RoomID = Reader[51].ToString();
                    opsApplication.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[52].ToString());//1��Ч0��Ч	
                    opsApplication.OperationDoctor.Dept.ID = Reader[54].ToString();
                    ////{B9DDCC10-3380-4212-99E5-BB909643F11B}
                    opsApplication.AnesWay = Reader[55].ToString();
                    //{F0B32D1F-99B6-4b1a-8393-C1F89B98543B}
                    opsApplication.Position = Reader[56].ToString();
                    opsApplication.Eneity = Reader[57].ToString();
                    opsApplication.LastTime = Reader[58].ToString();
                    opsApplication.IsOlation = Reader[59].ToString();
                    opsApplication.Memo = Reader[60].ToString();
                    myAl.Add(opsApplication);

                }
            }
            catch (Exception ex)
            {
                this.Err = "��û����������뵥��Ϣ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return myAl;
            }
            this.Reader.Close();
            try
            {
                foreach (FS.HISFC.Models.Operation.OperationAppllication opsApp in myAl)
                {
                    opsApp.DiagnoseAl = this.GetIcdFromApp(opsApp);	//����б�					
                    opsApp.OperationInfos = GetOpsInfoFromApp(opsApp.ID);//������Ŀ��Ϣ�б�				
                    opsApp.RoleAl = GetRoleFromApp(opsApp.ID);//��Ա��ɫ�б�
                    //�������Ը�ֵ��Ϊͻ�����ֲ����벿��ҵ����÷���
                    foreach (FS.HISFC.Models.Operation.ArrangeRole thisRole in opsApp.RoleAl)
                    {
                        if (thisRole.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper1.ToString()
                            || thisRole.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper2.ToString()
                            || thisRole.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper3.ToString())
                            //����ҽʦ�б�
                            opsApp.HelperAl.Add(thisRole.Clone());
                    }
                    //thisOpsApp.AppaRecAl = GetAppaRecFromApp(thisOpsApp.OperationNo);//�������ϰ����б�
                }
            }
            catch (Exception ex)
            {
                this.Err = "��û��������б���Ϣ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return myAl;
            }
            return myAl;
        }
	
		/// <summary>
		///����������Ż��������Ŀ��Ϣ�б�
		/// </summary>
		/// <param name="OperatorNo">�������</param>
		/// <returns>���ߵ���Ŀ��Ϣ��������</returns>
        public List<OperationInfo> GetOpsInfoFromApp(string operationNO)
		{
            List<OperationInfo> InfoAl = new List<OperationInfo>();
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.Operator.GetOpsInfoFromApp.1",ref strSql) == -1) 
			{
				return InfoAl;//������
			}

			try
			{
				strSql = string.Format(strSql,operationNO);
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operator.Operator.GetOpsInfoFromApp";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return InfoAl;            
			}

			if (strSql == null) 
			{
				return InfoAl;
			}

			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{
					FS.HISFC.Models.Operation.OperationInfo thisOperateInfo = new FS.HISFC.Models.Operation.OperationInfo();
					thisOperateInfo.OperationItem.ID = Reader[0].ToString();//��Ŀ����
                    thisOperateInfo.OperationItem.Name = Reader[1].ToString();//��Ŀ����
					if(Reader.IsDBNull(2)==false)
                        thisOperateInfo.OperationItem.Price = System.Convert.ToDecimal(Reader[2].ToString());//����
					if(Reader.IsDBNull(3)==false)
						thisOperateInfo.FeeRate =  System.Convert.ToDecimal(Reader[3].ToString());//�շѱ���
					if(Reader.IsDBNull(4)==false)
						thisOperateInfo.Qty = System.Convert.ToInt16(Reader[4]);//����
					
					thisOperateInfo.StockUnit = Reader[5].ToString();//��λ
					thisOperateInfo.OperateType.ID = Reader[6].ToString();//������ģ
					thisOperateInfo.InciType.ID = Reader[7].ToString();//�п�����
					thisOperateInfo.OpePos.ID = Reader[8].ToString();//������λ
					thisOperateInfo.IsMainFlag = FS.FrameWork.Function.NConvert.ToBoolean(Reader[9].ToString());//��������־ 1��/0��
					
					thisOperateInfo.IsValid = true;
					InfoAl.Add(thisOperateInfo);
				}
			}
			catch(Exception ex)
			{
				this.Err="���������Ŀ��Ϣ����"+ex.Message;
				this.ErrCode="-1";
				this.WriteErr();
				return InfoAl;
			}
			this.Reader.Close();
			return InfoAl;
		}
		/// <summary>
		/// ����������Ż����Ա��ɫ�����б�
		/// </summary>
		/// <param name="OperatorNo">�������뵥���</param>
		/// <returns>ָ����������Ա�������������</returns>
		public ArrayList GetRoleFromApp(string OperatorNo)
		{
			ArrayList RoleAl = new ArrayList();
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.Operator.GetRoleFromApp.1",ref strSql) == -1) 
			{
				return RoleAl;//������
			}

			try
			{
				strSql = string.Format(strSql,OperatorNo);
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operator.Operator.GetRoleFromApp";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return RoleAl;            
			}
			if (strSql == null) 
			{
				return RoleAl;
			}

			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{
					FS.HISFC.Models.Operation.ArrangeRole thisRole = new FS.HISFC.Models.Operation.ArrangeRole();
					thisRole.RoleType.ID = Reader[0].ToString();		//��ɫ����
					thisRole.ID = Reader[1].ToString();			//��Ա����
					thisRole.Name = Reader[2].ToString();		//��Ա����
					thisRole.ForeFlag = Reader[3].ToString();			//0��ǰ����1�����¼
					if (thisRole.ForeFlag == string.Empty || thisRole.ForeFlag == null)
						thisRole.ForeFlag = "0";
					thisRole.RoleOperKind.ID = Reader[4].ToString();//��Ա״̬
                    //{69F783B4-65EB-4cc3-B489-2A7D5B5A5F00}����ʱ��
                    thisRole.SupersedeDATE = FS.FrameWork.Function.NConvert.ToDateTime(Reader[5].ToString());
					RoleAl.Add(thisRole);
				}
			}
			catch(Exception ex)
			{
				this.Err="���������Ա��ɫ��Ϣ����"+ex.Message;
				this.ErrCode="-1";
				this.WriteErr();
				return RoleAl;
			}
			this.Reader.Close();
			return RoleAl;
		}
		/// <summary>
		/// ����������Ż���������ϰ����б�
		/// </summary>
		/// <param name="OperatorNo">�������뵥���</param>
		/// <returns>ָ����������Ա�������������</returns>
		public ArrayList GetAppaRecFromApp(string OperatorNo)
		{
			ArrayList AppaRecAl = new ArrayList();
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.Operator.GetAppaRec.1",ref strSql) == -1) 
			{
				return AppaRecAl;//������
			}

			try
			{
				strSql = string.Format(strSql,OperatorNo);
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operator.Operator.GetAppaRecFromApp";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return AppaRecAl;            
			}
			if (strSql == null) 
			{
				return AppaRecAl;
			}

			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{
					FS.HISFC.Models.Operation.OpsApparatusRec thisRec = new FS.HISFC.Models.Operation.OpsApparatusRec();
					
					thisRec.OperationNo = OperatorNo;//�������
					thisRec.OpsAppa.ID = Reader[0].ToString();//�豸����					
					thisRec.OpsAppa.Name = Reader[1].ToString();//�豸����
					thisRec.foreflag = FS.FrameWork.Function.NConvert.ToInt32(Reader[2].ToString());//1.��ǰ����/2.�����¼						
					if (thisRec.foreflag == 0)
						thisRec.foreflag = 1;
					thisRec.Qty = FS.FrameWork.Function.NConvert.ToInt32(Reader[3].ToString());//����
					thisRec.AppaUnit = Reader[4].ToString();//��λ
					
					AppaRecAl.Add(thisRec);
				}
			}
			catch(Exception ex)
			{
				this.Err="����������ϰ�����Ϣ����"+ex.Message;
				this.ErrCode="-1";
				this.WriteErr();
				return AppaRecAl;
			}
			this.Reader.Close();
			return AppaRecAl;
		}

		#endregion
		#region ����������뵥�Ĳ���
		/// <summary>
		/// ��ȡ���������뵥���
		/// </summary>
		/// <returns>�����뵥���</returns>
		public string GetNewOperationNo()
		{
			string strNewNo = string.Empty;
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.Operator.GetNewOperationNo.1",ref strSql) == -1) 
			{
				return strNewNo; //���ַ���
			}
			if (strSql == null) return strNewNo;
			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{
					strNewNo = Reader[0].ToString();
				}
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operator.Operator.GetNewOperationNo";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return strNewNo;
			}
			this.Reader.Close();
			return strNewNo;
		}
		/// <summary>
		/// ��ȡ�������뵥״̬
		/// </summary>
		/// <param name="OperatorNo">�������뵥���</param>
		/// <returns>���뵥״̬ 1������ 2������ 3�Ѱ��� 4�����</returns>
		public string GetApplicationStatus( string operatorNO )
		{
			string strStatus = string.Empty;
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.Operator.GetApplicationStatus.1",ref strSql) == -1) 
			{
				return strStatus;//������
			
			}
			
			try
			{
				strSql = string.Format(strSql,operatorNO);
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operator.Operator.GetApplicationStatus";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return strStatus;            
			}

			if (strSql == null) 
				return strStatus;
			
			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{
					strStatus = Reader[0].ToString();
				}
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operation.Operation.GetApplicationStatus";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return strStatus;            
			}
			this.Reader.Close();

			return strStatus;
		}

		/// <summary>
		/// ��������(�½��������뵥)
		/// </summary>
		/// <param name="OpsApp">�������뵥ʵ��</param>
		/// <returns>0 success -1 fail</returns>
		public int CreateApplication(FS.HISFC.Models.Operation.OperationAppllication OpsApp)
		{
			#region �½��������뵥
			///�½��������뵥
			///Operation.Operation.CreateApplication.1
			///���룺58
			///������0 
			#endregion			
			string strSql = string.Empty;		
			#region ��ȡ���߻�����Ϣ
			//--------------------------------------------------------		
			//�ֲ���������
			string ls_ClinicCode = string.Empty;		//סԺ��ˮ��/�����
			string ls_PatientNo = string.Empty;			//������/������
			string ls_Name = string.Empty;				//��������
			string ls_Sex = string.Empty;				//�Ա�
			DateTime ldt_Birthday = DateTime.MinValue;	//����
			string ls_DeptCode = string.Empty;			//סԺ����
			string ls_BedNo = string.Empty;				//��λ��
			string ls_BloodCode = string.Empty;			//����Ѫ��
			decimal ld_PrePay = 0;						//Ԥ����
			string ls_SickRoom = string.Empty;			//������		
	
			#region �����ж���������������סԺ������ȷ��Ӧ������Ļ��ߺ�			
			switch (OpsApp.PatientSouce)
			{
				case "1":  //��������
					ls_PatientNo = OpsApp.PatientInfo.PID.CardNO;
					break;
				case "2":  //סԺ����
					ls_PatientNo = OpsApp.PatientInfo.PID.PatientNO;
					break;
				default:
					break;
			}			
			#endregion

			ls_ClinicCode = OpsApp.PatientInfo.ID.ToString();
			//ls_PatientNo = OpsApp.PatientInfo.Patient.PID.RecordNo;
			ls_Name = OpsApp.PatientInfo.Name;
			ls_Sex = OpsApp.PatientInfo.Sex.ID.ToString();
			ldt_Birthday = OpsApp.PatientInfo.Birthday;
			ls_DeptCode = OpsApp.PatientInfo.PVisit.PatientLocation.Dept.ID.ToString();
			ls_BedNo = OpsApp.PatientInfo.PVisit.PatientLocation.Bed.ID.ToString();
			ls_BloodCode = OpsApp.PatientInfo.BloodType.ID.ToString();
			ld_PrePay = OpsApp.PatientInfo.FT.PrepayCost;
			ls_SickRoom = OpsApp.PatientInfo.PVisit.PatientLocation.Room;
			//--------------------------------------------------------
			#endregion
			DateTime ldt_ReceptDate = DateTime.MinValue;//�ӻ���ʱ��
			//���������뵥������ʱ�伴Ϊ��ǰϵͳʱ��
			OpsApp.ApplyDate = FS.FrameWork.Function.NConvert.ToDateTime(this.GetSysDateTime());
			OpsApp.ExeDept = OpsApp.OperateRoom;//ִ�п���(������Ҫ�����뵥��������˵�������Ҽ�ִ�п���)
			string strGerm = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsGermCarrying).ToString();
			string strFinished = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsFinished).ToString();
			string strAnesth = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsAnesth).ToString();
			string strUrgent = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsUrgent).ToString();
			string strChange = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsChange).ToString();
			string strHeavy = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsHeavy).ToString();
			string strSpecial = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsSpecial).ToString();
            string strValid = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsValid).ToString();
			string strUnite = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsUnite).ToString();
            string strNeedAcco = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsAccoNurse).ToString();
            string strNeedPrep = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsPrepNurse).ToString();
			//Ĭ��ȡ��һ�����Ϊͳ����ǰ���
			string strDiagnose = string.Empty;
			if(OpsApp.DiagnoseAl.Count > 0)
			{
				foreach(FS.HISFC.Models.HealthRecord.DiagnoseBase MainDiagnose in OpsApp.DiagnoseAl)
				{
					if(MainDiagnose.IsValid)
					{
						strDiagnose = MainDiagnose.Name + "(" + MainDiagnose.ID.ToString() + ")";
						break;
					}
				}
			}

			if(this.Sql.GetSql("Operator.Operator.CreateApplication.1",ref strSql)==-1) 
			{
				return -1;
			}

			try
            {  //{0E140FEC-2F31-4414-8401-86E78FA3ADDC}��˱�ע��Ϊ��Դ����������������
					string []str = new string[]{	OpsApp.ID,ls_ClinicCode,ls_PatientNo,OpsApp.PatientSouce,ls_Name,
												   ls_Sex,ldt_Birthday.ToString(),ld_PrePay.ToString(),ls_DeptCode,ls_BedNo,
												  ls_BloodCode,strDiagnose,OpsApp.OperateKind,OpsApp.OperationDoctor.ID.ToString(),OpsApp.GuideDoctor.ID.ToString(),
						ls_SickRoom,OpsApp.PreDate.ToString(),OpsApp.Duration.ToString(),OpsApp.AnesType.ID.ToString(),OpsApp.HelperAl.Count.ToString(),
						"0","0","0",OpsApp.ExeDept.ID.ToString(),OpsApp.TableType,
						OpsApp.ApplyDoctor.ID.ToString(),OpsApp.ApplyDoctor.Dept.ID.ToString(),OpsApp.ApplyDate.ToString(),OpsApp.ApplyNote,OpsApp.ApproveDoctor.ID.ToString(),
						OpsApp.ApproveDate.ToString(),OpsApp.Appsourceid,"",OpsApp.OperationType.ID.ToString(),OpsApp.InciType.ID.ToString(),
						strGerm,OpsApp.ScreenUp,OpsApp.OpsTable.ID.ToString(),ldt_ReceptDate.ToString(),OpsApp.BloodType.ID.ToString(),
						OpsApp.BloodNum.ToString(),OpsApp.BloodUnit,OpsApp.OpsNote,OpsApp.AneNote,OpsApp.ExecStatus,
						strFinished,strAnesth,OpsApp.Folk,OpsApp.RelaCode.ID.ToString(),OpsApp.FolkComment,
						strUrgent,strChange,strHeavy,strSpecial,OpsApp.User.ID.ToString(),
						System.DateTime.Now.ToString(),strValid,strUnite,"",strNeedAcco,strNeedPrep,OpsApp.OperationDoctor.Dept.ID,/*{B9DDCC10-3380-4212-99E5-BB909643F11B}*/OpsApp.AnesWay,
                         //{F0B32D1F-99B6-4b1a-8393-C1F89B98543B}
                         OpsApp.Position,OpsApp.Eneity,OpsApp.LastTime,OpsApp.IsOlation
											   }	;	
				//�������뵥�������Ӽ�¼
//				//ÿ��5������
//				strSql = string.Format(strSql,OpsApp.OperationNo,ls_ClinicCode,ls_PatientNo,OpsApp.Pasource,ls_Name,
//					ls_Sex,ldt_Birthday.ToString(),ld_PrePay.ToString(),ls_DeptCode,ls_BedNo,
//					ls_BloodCode,strDiagnose,OpsApp.OperateKind,OpsApp.Ops_docd.ID.ToString(),OpsApp.Gui_docd.ID.ToString(),
//					ls_SickRoom,OpsApp.Pre_Date.ToString(),OpsApp.Duration.ToString(),OpsApp.Anes_type.ID.ToString(),OpsApp.HelperAl.Count,
//					0,0,0,OpsApp.ExeDept.ID.ToString(),OpsApp.TableType,
//					OpsApp.Apply_Doct.ID.ToString(),OpsApp.Apply_Doct.Dept.ID.ToString(),OpsApp.Apply_Date.ToString(),OpsApp.ApplyNote,OpsApp.ApprDocd.ID.ToString(),
//					OpsApp.ApprDate.ToString(),OpsApp.ApprNote,"",OpsApp.OperateType.ID.ToString(),OpsApp.InciType.ID.ToString(),
//					strGerm,OpsApp.ScreenUp,OpsApp.OpsTable.ID.ToString(),ldt_ReceptDate.ToString(),OpsApp.BloodType.ID.ToString(),
//					OpsApp.BloodNum.ToString(),OpsApp.BloodUnit,OpsApp.OpsNote,OpsApp.AneNote,OpsApp.ExecStatus,
//					strFinished,strAnesth,OpsApp.Folk,OpsApp.RelaCode.ID.ToString(),OpsApp.FolkComment,
//					strUrgent,strChange,strHeavy,strSpecial,OpsApp.User.ID.ToString(),
//					this.GetSysDateTime(),strValid,strUnite,"",strNeedAcco,strNeedPrep);
				if(this.ExecNoQuery(strSql,str) == -1) 
				{
					return -1;
				}
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operation.Operation.CreateApplication";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return -1;            
			}
			#region ����������Ŀ������Ϣ
			//--------------------------------------------------------			
			//��Ա����뵥���漰�����������������Ϣ
			foreach (FS.HISFC.Models.Operation.OperationInfo OperateInfo in OpsApp.OperationInfos)
			{
				//���������Ϣ
				try
				{
					if(AddOperationInfo(OpsApp,OperateInfo) == -1) 
					{
						return -1;
					}
				}
				catch(Exception ex)
				{
					this.Err = "HISFC.Operation.Operation.CreateApplication OperateInfo";
					this.ErrCode = ex.Message;
					this.WriteErr();
					return -1;            
				}
			}
			//--------------------------------------------------------
			#endregion
			#region �������������Ϣ
			//��û������е�������ǰ���
			ArrayList al = this.GetIcdFromApp(OpsApp);
			//�ж��Ƿ���ڼ�¼�ı�־
			bool bIsExist = false;
			//����Ҫ����������Ϣ�б�(OpsApp.DiagnoseAl)
			foreach(FS.HISFC.Models.HealthRecord.DiagnoseBase willAddDiagnose in OpsApp.DiagnoseAl)
			{
				bIsExist = false;				
				//�����������е�����������ϣ����willAddDiagnose�Ѿ����ڣ�������״̬��
				//���willAddDiagnose�в����ڣ��������ü�¼�����ݿ���
				foreach(FS.HISFC.Models.HealthRecord.DiagnoseBase thisDiagnose in al)
				{
					if(thisDiagnose.HappenNo == willAddDiagnose.HappenNo && thisDiagnose.Patient.ID.ToString() == willAddDiagnose.Patient.ID.ToString())
					{
						//�Ѿ�����	����				
                        //TODO:����ҵ���
						//if(this.DiagnoseManager.UpdatePatientDiagnose(willAddDiagnose) == -1) return -1;
						//bIsExist = true;
					}
				}
				//������Ϻ��ֲ����� ����
				if(bIsExist == false)
				{
                    //TODO:����ҵ���
					//if(this.DiagnoseManager.CreatePatientDiagnose(willAddDiagnose) == -1) return -1;
				}
			}
			#endregion
			#region ����������Ա��ɫ��Ϣ
			try
			{
				if(this.ProcessRoleForApply(OpsApp) == -1) return -1;
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operation.Operation.CreateApplication Role";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return -1;            
			}
			#endregion
			return 0;
		}

        /// <summary>
        /// �޸��������ߵ�״̬����ExecStatus=3���Ѱ���״̬��ΪExecStatus=6��������δ�շ�״̬����״̬ΪҽԺҪ�����
        /// </summary>
        /// <param name="OpsApp">�������뵥ʵ��</param>
        /// <returns>0 success -1 fail</returns>
        public int UpdateAlreadyOperation(FS.HISFC.Models.Operation.OperationAppllication AlreadyOpsApp)
        {
           // AlreadyOpsApp.ExecStatus = '6';
            string strSql = string.Empty;
            if (this.Sql.GetSql("Operator.Operator.UpdateAlreadyOperation.1", ref strSql) == -1) return -1;
            try
            {
                string[] str = new string[] { AlreadyOpsApp.ID,"6"};
                if (this.ExecNoQuery(strSql, str) == -1) return -1;
            }
            catch (Exception ex)
            {
                this.Err = "HISFC.Operation.Operation.UpdateAlreadyOperation";
                this.ErrCode = ex.Message;
                this.WriteErr();
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// �޸��������ߵ�״̬����ExecStatus=6���Ѱ���״̬��ΪExecStatus=3��������δ�շ�״̬����״̬ΪҽԺҪ�����
        /// </summary>
        /// <param name="OpsApp">�������뵥ʵ��</param>
        /// <returns>0 success -1 fail</returns>
        public int UpdateCancelAlreadyOperation(FS.HISFC.Models.Operation.OperationAppllication CancelAlreadyOps)
        {
            // AlreadyOpsApp.ExecStatus = '6';
            string strSql = string.Empty;
            if (this.Sql.GetSql("Operator.Operator.UpdateCancelAlreadyOperation.1", ref strSql) == -1) return -1;
            try
            {
                string[] str = new string[] { CancelAlreadyOps.ID, "3" };
                if (this.ExecNoQuery(strSql, str) == -1) return -1;
            }
            catch (Exception ex)
            {
                this.Err = "HISFC.Operation.Operation.UpdateCancelAlreadyOperation";
                this.ErrCode = ex.Message;
                this.WriteErr();
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// ��ȡ�Ѿ���������Ϊ�շѵĻ�����Ϣ
        /// </summary>
        /// <param name="OpsRoomID">�������ұ���</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">��ѯ����ʱ��</param>
        /// <param name="isNeedApprove">�Ƿ���Ҫ��� true �� false ��</param>
        /// <returns>�������뵥��������</returns>
        public ArrayList GetAlreadyOpsAppList(string OpsRoomID, string beginTime, string endTime, bool isNeedApprove)
        {
            ArrayList myAl = new ArrayList();
            string sqlStr = string.Empty;
            sqlStr = @"select a.operationno,a.clinic_code,a.name,a.execstatus  
                       from met_ops_apply a
                       where pre_date >= to_date('{1}','yyyy-mm-dd HH24:mi:ss') 
                       and pre_date <= to_date('{2}','yyyy-mm-dd HH24:mi:ss')
                       --and ynanesth = '1'
                       and a.ynvalid='1'
                       and a.execstatus='3'";
            try
            {
                sqlStr = string.Format(sqlStr, OpsRoomID, beginTime, endTime, isNeedApprove);
                if (this.ExecQuery(sqlStr) == -1)
                {
                    this.Err = "����������������Ϣʧ��";
                    return myAl;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return myAl;
            }
            return GetAlreadyOpsAppListFromSqlNew(sqlStr);
        }

        /// <summary>
        /// ��ø���SQL����ѯ����������δ�շѶ�������,������add by zhy
        /// </summary>
        /// <param name="strSql">ָ���Ĳ�ѯ���</param>
        /// <returns>������δ�շѻ��߶�������</returns>
        private ArrayList GetAlreadyOpsAppListFromSqlNew(string strSql)
        {
            ArrayList myAl = new ArrayList();
            this.ExecQuery(strSql);
            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Operation.OperationAppllication opsApplication = new FS.HISFC.Models.Operation.OperationAppllication();
                    opsApplication.ID = Reader[0].ToString();					//�������	
                    opsApplication.PatientInfo.ID = Reader[1].ToString();//�����/סԺ��ˮ��
                    opsApplication.PatientInfo.Name = Reader[2].ToString();	//����
                    opsApplication.ExecStatus = Reader[3].ToString();	//����״̬
                    myAl.Add(opsApplication);
                }
            }
            catch (Exception ex)
            {
                this.Err = "���������������Ϣ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return myAl;
            }
            this.Reader.Close();

            return myAl;
        }

        /// <summary>
        /// ��������(�޸��������뵥)
        /// </summary>
        /// <param name="OpsApp">�������뵥ʵ��</param>
        /// <returns>0 success -1 fail</returns>
        public int UpdateApplicationByFee(FS.HISFC.Models.Operation.OperationAppllication OpsApp)
        {
            #region �޸��������뵥
            ///�޸��������뵥
            ///Operation.Operation.UpdateApplication.1
            ///���룺58
            ///������0 
            #endregion

            string strSql = string.Empty;
            #region ��ȡ���߻�����Ϣ
            //--------------------------------------------------------		
            //�ֲ���������
            string ls_ClinicCode = string.Empty;//סԺ��ˮ��/�����
            string ls_PatientNo = string.Empty; //������/������
            string ls_Name = string.Empty;	  //��������
            string ls_Sex = string.Empty;		  //�Ա�
            DateTime ldt_Birthday = DateTime.MinValue; //����
            string ls_DeptCode = string.Empty;  //סԺ����
            string ls_BedNo = string.Empty;	  //��λ��
            string ls_BloodCode = string.Empty; //����Ѫ��
            decimal ld_PrePay = 0;	  //Ԥ����
            string ls_SickRoom = string.Empty;  //������

            #region �����ж���������������סԺ������ȷ��Ӧ������Ļ��ߺ�
            switch (OpsApp.PatientSouce)
            {
                case "1":  //��������
                    ls_PatientNo = OpsApp.PatientInfo.PID.CardNO;
                    break;
                case "2":  //סԺ����
                    ls_PatientNo = OpsApp.PatientInfo.PID.PatientNO;
                    break;
            }
            #endregion
            ls_ClinicCode = OpsApp.PatientInfo.ID.ToString();
            //ls_PatientNo = OpsApp.PatientInfo.Patient.PID.RecordNo;
            ls_Name = OpsApp.PatientInfo.Name;
            ls_Sex = OpsApp.PatientInfo.Sex.ID.ToString();
            ldt_Birthday = OpsApp.PatientInfo.Birthday;
            ls_DeptCode = OpsApp.PatientInfo.PVisit.PatientLocation.Dept.ID.ToString();
            ls_BedNo = OpsApp.PatientInfo.PVisit.PatientLocation.Bed.ID.ToString();
            ls_BloodCode = OpsApp.PatientInfo.BloodType.ID.ToString();
            ld_PrePay = OpsApp.PatientInfo.FT.PrepayCost;
            ls_SickRoom = OpsApp.PatientInfo.PVisit.PatientLocation.Room;
            //--------------------------------------------------------
            #endregion
            DateTime ldt_ReceptDate = DateTime.MinValue;//�ӻ���ʱ��
            OpsApp.ExeDept = OpsApp.OperateRoom;//ִ�п���(������Ҫ�����뵥��������˵�������Ҽ�ִ�п���)
            string strGerm = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsGermCarrying).ToString();
            string strFinished = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsFinished).ToString();
            string strAnesth = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsAnesth).ToString();
            string strUrgent = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsUrgent).ToString();
            string strChange = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsChange).ToString();
            string strHeavy = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsHeavy).ToString();
            string strSpecial = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsSpecial).ToString();
            string strValid = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsValid).ToString();
            string strUnite = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsUnite).ToString();
            string strNeedAcco = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsAccoNurse).ToString();
            string strNeedPrep = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsPrepNurse).ToString();
            //Ĭ��ȡ��һ�����Ϊͳ����ǰ���
            string strDiagnose = string.Empty;
            if (OpsApp.DiagnoseAl.Count > 0)
            {
                foreach (FS.HISFC.Models.HealthRecord.DiagnoseBase MainDiagnose in OpsApp.DiagnoseAl)
                {
                    if (MainDiagnose.IsValid)
                    {
                        strDiagnose = MainDiagnose.Name + "(" + MainDiagnose.ID.ToString() + ")";
                        break;
                    }
                }
            }
            if (this.Sql.GetSql("Operator.Operator.UpdateApplication.1", ref strSql) == -1) return -1;
            try
            {
                string[] str = new string[]{
											   OpsApp.ID,ls_ClinicCode,ls_PatientNo,OpsApp.PatientSouce,ls_Name,
											   ls_Sex,ldt_Birthday.ToString(),ld_PrePay.ToString(),ls_DeptCode,ls_BedNo,
											   ls_BloodCode,strDiagnose,OpsApp.OperationDoctor.ID.ToString(),OpsApp.GuideDoctor.ID.ToString(),ls_SickRoom,
											   OpsApp.PreDate.ToString(),OpsApp.Duration.ToString(),OpsApp.AnesType.ID.ToString(),OpsApp.HelperAl.Count.ToString(),"0",
											   "0","0",OpsApp.ExeDept.ID.ToString(),OpsApp.TableType,OpsApp.ApplyDoctor.ID.ToString(),
											   OpsApp.ApplyDoctor.Dept.ID.ToString(),OpsApp.ApplyDate.ToString(),OpsApp.ApplyNote,OpsApp.ApproveDoctor.ID,OpsApp.ApproveDate.ToString(),
											   OpsApp.ApproveNote,"",OpsApp.OperationType.ID.ToString(),OpsApp.InciType.ID.ToString(),strGerm,
											   OpsApp.ScreenUp,OpsApp.OpsTable.ID.ToString(),ldt_ReceptDate.ToString(),OpsApp.BloodType.ID.ToString(),OpsApp.BloodNum.ToString(),
											   OpsApp.BloodUnit,OpsApp.OpsNote,OpsApp.AneNote,OpsApp.ExecStatus,strFinished,
											   strAnesth,OpsApp.Folk,OpsApp.RelaCode.ID.ToString(),OpsApp.FolkComment,strUrgent,
											   strChange,strHeavy,strSpecial,OpsApp.User.ID.ToString(),this.GetSysDateTime(),
											   strValid,strUnite,"",OpsApp.OperateKind,strNeedAcco,strNeedPrep,OpsApp.RoomID,OpsApp.OperationDoctor.Dept.ID,
                                               /*{B9DDCC10-3380-4212-99E5-BB909643F11B}*/OpsApp.AnesWay,
                                               //{F0B32D1F-99B6-4b1a-8393-C1F89B98543B}
                                               OpsApp.Position,OpsApp.Eneity,OpsApp.LastTime,OpsApp.IsOlation,
                                               OpsApp.Memo //add by zhy
										   };
                //ÿ��5������
                //				strSql = string.Format(strSql,OpsApp.OperationNo,ls_ClinicCode,ls_PatientNo,OpsApp.Pasource,ls_Name,
                //					ls_Sex,ldt_Birthday.ToString(),ld_PrePay.ToString(),ls_DeptCode,ls_BedNo,
                //					ls_BloodCode,strDiagnose,OpsApp.Ops_docd.ID.ToString(),OpsApp.Gui_docd.ID.ToString(),ls_SickRoom,
                //					OpsApp.Pre_Date.ToString(),OpsApp.Duration.ToString(),OpsApp.Anes_type.ID.ToString(),OpsApp.HelperAl.Count,0,
                //					0,0,OpsApp.ExeDept.ID.ToString(),OpsApp.TableType,OpsApp.Apply_Doct.ID.ToString(),
                //					OpsApp.Apply_Doct.Dept.ID.ToString(),OpsApp.Apply_Date.ToString(),OpsApp.ApplyNote,OpsApp.ApprDocd.ID.ToString(),OpsApp.ApprDate.ToString(),
                //					OpsApp.ApprNote,"",OpsApp.OperateType.ID.ToString(),OpsApp.InciType.ID.ToString(),strGerm,
                //					OpsApp.ScreenUp,OpsApp.OpsTable.ID.ToString(),ldt_ReceptDate.ToString(),OpsApp.BloodType.ID.ToString(),OpsApp.BloodNum.ToString(),
                //					OpsApp.BloodUnit,OpsApp.OpsNote,OpsApp.AneNote,OpsApp.ExecStatus,strFinished,
                //					strAnesth,OpsApp.Folk,OpsApp.RelaCode.ID.ToString(),OpsApp.FolkComment,strUrgent,
                //					strChange,strHeavy,strSpecial,OpsApp.User.ID.ToString(),this.GetSysDateTime(),
                //					strValid,strUnite,"",OpsApp.OperateKind,strNeedAcco,strNeedPrep,OpsApp.RoomID);	

                if (this.ExecNoQuery(strSql, str) == -1) return -1;
            }
            catch (Exception ex)
            {
                this.Err = "HISFC.Operation.Operation.UpdateApplication";
                this.ErrCode = ex.Message;
                this.WriteErr();
                return -1;
            }


            #region ����������Ŀ������Ϣ
            if (DelOperationInfo(OpsApp) == -1) return -1;
            //��Ա����뵥���漰�����������������Ŀ��Ϣ
            foreach (FS.HISFC.Models.Operation.OperationInfo OperateInfo in OpsApp.OperationInfos)
            {
                //���������Ŀ��Ϣ
                if (AddOperationInfo(OpsApp, OperateInfo) == -1) return -1;
            }
            #endregion
            #region �������������Ϣ
            //��û������е�������ǰ���
            ArrayList al = this.GetIcdFromApp(OpsApp);
            //�ж��Ƿ���ڼ�¼�ı�־
            bool bIsExist = false;
            //����Ҫ����������Ϣ�б�(OpsApp.DiagnoseAl)
            foreach (FS.HISFC.Models.HealthRecord.DiagnoseBase willAddDiagnose in OpsApp.DiagnoseAl)
            {
                bIsExist = false;
                //�����������е�����������ϣ����willAddDiagnose�Ѿ����ڣ�������״̬��
                //���willAddDiagnose�в����ڣ��������ü�¼�����ݿ���
                foreach (FS.HISFC.Models.HealthRecord.DiagnoseBase thisDiagnose in al)
                {
                    if (thisDiagnose.HappenNo == willAddDiagnose.HappenNo && thisDiagnose.Patient.ID.ToString() == willAddDiagnose.Patient.ID.ToString())
                    {
                        //�Ѿ�����	����	
                        //TODO:����ҵ���
                        //if(this.DiagnoseManager.UpdatePatientDiagnose(willAddDiagnose) == -1) return -1;
                        bIsExist = true;
                    }
                }
                //������Ϻ��ֲ����� ����
                if (bIsExist == false)
                {
                    //TODO:����ҵ���
                    //if(this.DiagnoseManager.CreatePatientDiagnose(willAddDiagnose) == -1) return -1;
                }
            }
            #endregion
            #region ����������Ա��ɫ��Ϣ
            try
            {
                if (this.ProcessRoleForApply(OpsApp) == -1) return -1;
            }
            catch (Exception ex)
            {
                this.Err = "HISFC.Operation.Operation.UpdateApplication Role";
                this.ErrCode = ex.Message;
                this.WriteErr();
                return -1;
            }
            #endregion
            return 1;
        }		

		/// <summary>
		/// ��������(�޸��������뵥)
		/// </summary>
		/// <param name="OpsApp">�������뵥ʵ��</param>
		/// <returns>0 success -1 fail</returns>
        public int UpdateApplication(FS.HISFC.Models.Operation.OperationAppllication OpsApp)
		{
			#region �޸��������뵥
			///�޸��������뵥
			///Operation.Operation.UpdateApplication.1
			///���룺58
			///������0 
			#endregion
			
			string strSql = string.Empty;		
			#region ��ȡ���߻�����Ϣ
			//--------------------------------------------------------		
			//�ֲ���������
			string ls_ClinicCode = string.Empty;//סԺ��ˮ��/�����
			string ls_PatientNo = string.Empty; //������/������
			string ls_Name = string.Empty;	  //��������
			string ls_Sex = string.Empty;		  //�Ա�
			DateTime ldt_Birthday = DateTime.MinValue; //����
			string ls_DeptCode = string.Empty;  //סԺ����
			string ls_BedNo = string.Empty;	  //��λ��
			string ls_BloodCode = string.Empty; //����Ѫ��
			decimal ld_PrePay = 0;	  //Ԥ����
			string ls_SickRoom = string.Empty;  //������
			
			#region �����ж���������������סԺ������ȷ��Ӧ������Ļ��ߺ�			
			switch (OpsApp.PatientSouce)
			{
				case "1":  //��������
					ls_PatientNo = OpsApp.PatientInfo.PID.CardNO;
					break;
				case "2":  //סԺ����
					ls_PatientNo = OpsApp.PatientInfo.PID.PatientNO;
					break;
			}			
			#endregion
			ls_ClinicCode = OpsApp.PatientInfo.ID.ToString();
			//ls_PatientNo = OpsApp.PatientInfo.Patient.PID.RecordNo;
			ls_Name = OpsApp.PatientInfo.Name;
			ls_Sex = OpsApp.PatientInfo.Sex.ID.ToString();
			ldt_Birthday = OpsApp.PatientInfo.Birthday;
			ls_DeptCode = OpsApp.PatientInfo.PVisit.PatientLocation.Dept.ID.ToString();
			ls_BedNo = OpsApp.PatientInfo.PVisit.PatientLocation.Bed.ID.ToString();
			ls_BloodCode = OpsApp.PatientInfo.BloodType.ID.ToString();
			ld_PrePay = OpsApp.PatientInfo.FT.PrepayCost;
			ls_SickRoom = OpsApp.PatientInfo.PVisit.PatientLocation.Room;
			//--------------------------------------------------------
			#endregion
			DateTime ldt_ReceptDate = DateTime.MinValue;//�ӻ���ʱ��
			OpsApp.ExeDept = OpsApp.OperateRoom;//ִ�п���(������Ҫ�����뵥��������˵�������Ҽ�ִ�п���)
			string strGerm = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsGermCarrying).ToString();
			string strFinished = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsFinished).ToString();
			string strAnesth = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsAnesth).ToString();
			string strUrgent = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsUrgent).ToString();
			string strChange = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsChange).ToString();
			string strHeavy = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsHeavy).ToString();
			string strSpecial = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsSpecial).ToString();
            string strValid = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsValid).ToString();
			string strUnite = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsUnite).ToString();
            string strNeedAcco = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsAccoNurse).ToString();
            string strNeedPrep = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsPrepNurse).ToString();
			//Ĭ��ȡ��һ�����Ϊͳ����ǰ���
			string strDiagnose = string.Empty;
			if(OpsApp.DiagnoseAl.Count > 0)
			{
				foreach(FS.HISFC.Models.HealthRecord.DiagnoseBase MainDiagnose in OpsApp.DiagnoseAl)
				{
					if(MainDiagnose.IsValid)
					{
						strDiagnose = MainDiagnose.Name + "(" + MainDiagnose.ID.ToString() + ")";
						break;
					}
				}
			}
			if(this.Sql.GetSql("Operator.Operator.UpdateApplication.1",ref strSql)==-1) return -1;
			try
			{
				string []str = new string[]{
											   OpsApp.ID,ls_ClinicCode,ls_PatientNo,OpsApp.PatientSouce,ls_Name,
											   ls_Sex,ldt_Birthday.ToString(),ld_PrePay.ToString(),ls_DeptCode,ls_BedNo,
											   ls_BloodCode,strDiagnose,OpsApp.OperationDoctor.ID.ToString(),OpsApp.GuideDoctor.ID.ToString(),ls_SickRoom,
											   OpsApp.PreDate.ToString(),OpsApp.Duration.ToString(),OpsApp.AnesType.ID.ToString(),OpsApp.HelperAl.Count.ToString(),"0",
											   "0","0",OpsApp.ExeDept.ID.ToString(),OpsApp.TableType,OpsApp.ApplyDoctor.ID.ToString(),
											   OpsApp.ApplyDoctor.Dept.ID.ToString(),OpsApp.ApplyDate.ToString(),OpsApp.ApplyNote,OpsApp.ApproveDoctor.ID,OpsApp.ApproveDate.ToString(),
											   OpsApp.ApproveNote,"",OpsApp.OperationType.ID.ToString(),OpsApp.InciType.ID.ToString(),strGerm,
											   OpsApp.ScreenUp,OpsApp.OpsTable.ID.ToString(),ldt_ReceptDate.ToString(),OpsApp.BloodType.ID.ToString(),OpsApp.BloodNum.ToString(),
											   OpsApp.BloodUnit,OpsApp.OpsNote,OpsApp.AneNote,OpsApp.ExecStatus,strFinished,
											   strAnesth,OpsApp.Folk,OpsApp.RelaCode.ID.ToString(),OpsApp.FolkComment,strUrgent,
											   strChange,strHeavy,strSpecial,OpsApp.User.ID.ToString(),this.GetSysDateTime(),
											   strValid,strUnite,"",OpsApp.OperateKind,strNeedAcco,strNeedPrep,OpsApp.RoomID,OpsApp.OperationDoctor.Dept.ID,
                                               /*{B9DDCC10-3380-4212-99E5-BB909643F11B}*/OpsApp.AnesWay,
                                               //{F0B32D1F-99B6-4b1a-8393-C1F89B98543B}
                                               OpsApp.Position,OpsApp.Eneity,OpsApp.LastTime,OpsApp.IsOlation,
                                               OpsApp.Memo //add by zhy
										   };
				//ÿ��5������
//				strSql = string.Format(strSql,OpsApp.OperationNo,ls_ClinicCode,ls_PatientNo,OpsApp.Pasource,ls_Name,
//					ls_Sex,ldt_Birthday.ToString(),ld_PrePay.ToString(),ls_DeptCode,ls_BedNo,
//					ls_BloodCode,strDiagnose,OpsApp.Ops_docd.ID.ToString(),OpsApp.Gui_docd.ID.ToString(),ls_SickRoom,
//					OpsApp.Pre_Date.ToString(),OpsApp.Duration.ToString(),OpsApp.Anes_type.ID.ToString(),OpsApp.HelperAl.Count,0,
//					0,0,OpsApp.ExeDept.ID.ToString(),OpsApp.TableType,OpsApp.Apply_Doct.ID.ToString(),
//					OpsApp.Apply_Doct.Dept.ID.ToString(),OpsApp.Apply_Date.ToString(),OpsApp.ApplyNote,OpsApp.ApprDocd.ID.ToString(),OpsApp.ApprDate.ToString(),
//					OpsApp.ApprNote,"",OpsApp.OperateType.ID.ToString(),OpsApp.InciType.ID.ToString(),strGerm,
//					OpsApp.ScreenUp,OpsApp.OpsTable.ID.ToString(),ldt_ReceptDate.ToString(),OpsApp.BloodType.ID.ToString(),OpsApp.BloodNum.ToString(),
//					OpsApp.BloodUnit,OpsApp.OpsNote,OpsApp.AneNote,OpsApp.ExecStatus,strFinished,
//					strAnesth,OpsApp.Folk,OpsApp.RelaCode.ID.ToString(),OpsApp.FolkComment,strUrgent,
//					strChange,strHeavy,strSpecial,OpsApp.User.ID.ToString(),this.GetSysDateTime(),
//					strValid,strUnite,"",OpsApp.OperateKind,strNeedAcco,strNeedPrep,OpsApp.RoomID);	
			
				if(this.ExecNoQuery(strSql,str) == -1) return -1;
			}
			catch(Exception ex)
			{
				this.Err ="HISFC.Operation.Operation.UpdateApplication";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return -1;            
			}
	

			#region ����������Ŀ������Ϣ
			if (DelOperationInfo(OpsApp) == -1) return -1;
			//��Ա����뵥���漰�����������������Ŀ��Ϣ
			foreach (FS.HISFC.Models.Operation.OperationInfo OperateInfo in OpsApp.OperationInfos)
			{
				//���������Ŀ��Ϣ
				if(AddOperationInfo(OpsApp,OperateInfo) == -1) return -1;
			}
			#endregion
			#region �������������Ϣ
			//��û������е�������ǰ���
			ArrayList al = this.GetIcdFromApp(OpsApp);
			//�ж��Ƿ���ڼ�¼�ı�־
			bool bIsExist = false;
			//����Ҫ����������Ϣ�б�(OpsApp.DiagnoseAl)
			foreach(FS.HISFC.Models.HealthRecord.DiagnoseBase willAddDiagnose in OpsApp.DiagnoseAl)
			{
				bIsExist = false;				
				//�����������е�����������ϣ����willAddDiagnose�Ѿ����ڣ�������״̬��
				//���willAddDiagnose�в����ڣ��������ü�¼�����ݿ���
				foreach(FS.HISFC.Models.HealthRecord.DiagnoseBase thisDiagnose in al)
				{
					if(thisDiagnose.HappenNo == willAddDiagnose.HappenNo && thisDiagnose.Patient.ID.ToString() == willAddDiagnose.Patient.ID.ToString())
					{
						//�Ѿ�����	����	
                        //TODO:����ҵ���
                        //if(this.DiagnoseManager.UpdatePatientDiagnose(willAddDiagnose) == -1) return -1;
						bIsExist = true;
					}
				}
				//������Ϻ��ֲ����� ����
				if(bIsExist == false)
				{
                    //TODO:����ҵ���
					//if(this.DiagnoseManager.CreatePatientDiagnose(willAddDiagnose) == -1) return -1;
				}
			}
			#endregion
			#region ����������Ա��ɫ��Ϣ
			try
			{
				if(this.ProcessRoleForApply(OpsApp) == -1) return -1;
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operation.Operation.UpdateApplication Role";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return -1;            
			}
			#endregion
			#region ��������������Ϣ
			try
			{
				if(this.ProcessAppaRecForApply(OpsApp) == -1) return -1;
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operation.Operation.UpdateApplication Apparatus";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return -1;            
			}
			#endregion
			return 0;
		}		
		/// <summary>
		/// ����ȡ��
		/// </summary>
		/// <param name="OpsApplication">�������뵥ʵ��</param>
		/// <returns>0 success -1 fail</returns>
        public int CancelApplication(FS.HISFC.Models.Operation.OperationAppllication OpsApp)
		{
			#region ȡ���������뵥
			///ȡ���������뵥
			///Operation.Operation.CancelApplication.1
			///���룺4
			///������0 
			#endregion
			string strSql=string.Empty;
			if(this.Sql.GetSql("Operator.Operator.UpdateApplication.2",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,OpsApp.ID,OpsApp.User.ID.ToString(),this.GetSysDateTime(),"0");
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operator.Operator.CancelApplication";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return -1;            
			}
			if (strSql == null) return -1;	
			return this.ExecNoQuery(strSql);
		}

        /// <summary>
        /// ����ȡ������
        /// </summary>
        /// <param name="OpsApp"></param>
        /// <param name="cancelReason"></param>
        /// <returns></returns>
        public int InsertCancelApply(FS.HISFC.Models.Operation.OperationAppllication OpsApp, string cancelReason)
        {
            //����ȡ������
            string strSql = string.Empty;
            if (this.Sql.GetSql("Operator.Operator.InsertCancelApply", ref strSql) == -1)
            {
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, OpsApp.ID, cancelReason,this.Operator.ID, this.GetDateTimeFromSysDateTime());
            }
            catch (Exception ex)
            {
                this.Err = "Operator.Operator.InsertCancelApply";
                this.WriteErr();
                return -1;
            }
            if (strSql == null) { return -1; }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ������������Ų������������Ƿ�����
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Operation.OperationAppllication QueryCancelApplyByAppID(string appId)
        {
            string strSql = string.Empty;
            if (this.Sql.GetSql("Operator.Operator.QueryCancelApply", ref strSql) == -1)
            {
                this.Err = "Operator.Operator.QueryCancelApply";
                return null;
            }
            FS.HISFC.Models.Operation.OperationAppllication application = new OperationAppllication();
             try
             {
                 strSql = string.Format(strSql, appId);
                 this.ExecQuery(strSql);
                 while (this.Reader.Read())
                 {
                     application = new OperationAppllication();
                     application.ID = this.Reader[0].ToString();
                     application.Memo = this.Reader[1].ToString();
                     application.OperationDoctor.ID = this.Reader[2].ToString();
                     application.PreDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[3].ToString());
                 }
             }
             catch (Exception ex)
             {
                 this.Reader.Close();
                 return null;
             }
             finally
             {
                 this.Reader.Close();
             }
             return application;
        }

        /// <summary>
        /// ����������Һ����������ڻ�ȡ����̨�����
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public string GetOrderByApplyDeptAndTime(string deptCode, DateTime dateTime)
        {
            string strSql = string.Empty;
            if (this.Sql.GetSql("Operator.Operator.GetOrderByApplyDeptAndTime", ref strSql) == -1)
            {
                return string.Empty;
            }
            strSql = string.Format(strSql, deptCode, dateTime);
            return this.ExecSqlReturnOne(strSql, string.Empty);
        }
    
	
		/// <summary>
		/// �����ǼǺ�������뵥״̬
		/// </summary>
		/// <param name="OperatorNo">�������뵥���</param>
		/// <returns>0 success -1 fail</returns>
		public int DoOperatorRecord(string OperatorNo)
		{			
			//����������Ϊ1������״̬��Ϊ4
			string strSql=string.Empty;

			if(this.Sql.GetSql("Operator.Operator.UpdateApplication.3",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,OperatorNo);
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operator.Operator.DoOperatorRecord";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return -1;            
			}
			if (strSql == null) return -1;			
			return this.ExecNoQuery(strSql);
		}


		/// <summary>
		/// ����ǼǺ�������뵥״̬
		/// </summary>
		/// <param name="OperatorNo"></param>
		/// <returns></returns>
		public int DoAnaeRecord(string OperatorNo)
		{			
			string strSql=string.Empty;

			if(this.Sql.GetSql("Operator.Operator.UpdateApplication.4",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,OperatorNo);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				this.WriteErr();
				return -1;            
			}
			if (strSql == null) return -1;			
			return this.ExecNoQuery(strSql);
		}

        /// <summary>//{80D89813-7B64-4acf-A2CD-55BFD9F1E7C6}
        /// ����ǼǺ�������뵥״̬
        /// </summary>
        /// <param name="OperatorNo"></param>
        /// <returns></returns>
        public int DoAnaeRecord(string OperatorNo,string status)
        {
            string strSql = string.Empty;

            if (this.Sql.GetSql("Operator.Operator.UpdateApplication.Status", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, OperatorNo,status);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                this.WriteErr();
                return -1;
            }
            if (strSql == null) return -1;
            return this.ExecNoQuery(strSql);
        }		
		#endregion
		#region ��������ҵ������ж�
		/// <summary>
		/// ���ָ�����ڿ�����ָ��������ʣ����������̨��
		/// </summary>
		/// <param name="OpsRoom">�����Ҷ���</param>
		/// <param name="DeptID">����������ұ��</param>
		/// <param name="PreDate">����ԤԼ����</param>
		/// <returns> >=0 ���������̨���� -1 fail </returns>
		public int GetEnableTableNum(FS.HISFC.Models.Base.Department operationRoom,string deptID,DateTime preDate)
		{
			int iTotalNum = 0;	//����
			int iAlreadyNum = 0;//��������
			int iEnableNum = 0;	//�п�������
			
			string strWeekDay = string.Empty;//���ڼ�
			switch(preDate.DayOfWeek)
			{
				case System.DayOfWeek.Monday:
					strWeekDay = "1";
					break;
				case System.DayOfWeek.Tuesday:
					strWeekDay = "2";
					break;
				case System.DayOfWeek.Wednesday:
					strWeekDay = "3";
					break;
				case System.DayOfWeek.Thursday:
					strWeekDay = "4";
					break;
				case System.DayOfWeek.Friday:
					strWeekDay = "5";
					break;
				case System.DayOfWeek.Saturday:
					strWeekDay = "6";
					break;
				case System.DayOfWeek.Sunday:
					strWeekDay = "7";
					break;
			}
			//���ݿ��Һ��������Լ����ڼ������ԭʼ�������̨��
			string strSql1 = string.Empty;
			if(this.Sql.GetSql("Operator.OpsTableAlloc.GetAllotInfo.3",ref strSql1) == -1) return -1;
			try
			{
				strSql1 = string.Format(strSql1,operationRoom.ID.ToString(),deptID,strWeekDay);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return -1;            
			}
			if (strSql1 == null) return -1;
			this.ExecQuery(strSql1);
			try
			{
				while(this.Reader.Read())
				{
					iTotalNum = FS.FrameWork.Function.NConvert.ToInt32(Reader[0].ToString());//���ԭ���������
				}
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return -1;            
			}
			this.Reader.Close();
			//�ٻ�ȡ�Ѿ��������̨��
			string strSql2 = string.Empty;
			if(this.Sql.GetSql("Operator.Operator.GetAlreadyNum.1",ref strSql2) == -1) return -1;
			try
			{
				string strBegin,strEnd;
				strBegin = preDate.Date.ToString();
				strEnd = preDate.Date.AddDays(1).ToString();
				strSql2 = string.Format(strSql2,strBegin,strEnd,operationRoom.ID.ToString(),deptID);

			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return -1;            
			}
			if (strSql2 == null) return -1;
			this.ExecQuery(strSql2);
			try
			{
				while(this.Reader.Read())
				{
					iAlreadyNum = FS.FrameWork.Function.NConvert.ToInt32(Reader[0].ToString());//����Ѿ����������̨��
				}
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return -1;            
			}
			this.Reader.Close();
			//�п��������̨��
			iEnableNum = iTotalNum - iAlreadyNum;
			return iEnableNum;
		}
		/// <summary>
		/// ����ԤԼ����ʱЧ���ж�
		/// </summary>
		/// <param name="PreDate">����ԤԼ����</param>
		/// <returns>Error:ϵͳֵδά�����ʽ�Ƿ���Before:ԤԼʱ��С�����ڣ�Over:����������յ���̨��OK:��������</returns>
		public string PreDateValidity(DateTime PreDate)
		{
			//�������ԤԼ���������죬���ж�����ʱ���Ƿ���������������ʱ�䣬������Ӧֵ
			//���С������ʱ�䣬������Ӧֵ
			DateTime dtNow = FS.FrameWork.Function.NConvert.ToDateTime(this.GetSysDateTime());
			DateTime dtTomorrow = dtNow.AddDays(1).Date;
			DateTime dtLimited = DateTime.MinValue;
			if(PreDate < dtNow)//С������
			{return "Before";}
			else if(PreDate.Date.Date == dtNow.Date)//����ԤԼ
			{
				//��������ʱ���Ƿ�������������
				string allowApply=GetControlArgument("100031");
				if(allowApply=="Error")return "Error";
				
				if(allowApply.Trim()=="1")//����
				{
					return "OK";
				}
				else//������
				{
					return "Over";
				}
			}
			else if(PreDate.Date == dtTomorrow) //ԤԼ����������
			{
				//�������������ʱ��
				string strTimeLimited = GetControlArgument("optime");
				if(strTimeLimited == "Error"||strTimeLimited==string.Empty) return "Error";
				//�ж�strTimeLimited�Ƿ�����Ч��ʱ���ʽ
				try
				{
					string Today = dtNow.Year.ToString() + "-" +dtNow.Month.ToString() +"-" +dtNow.Day.ToString();
					string TodayTime = Today + " " + strTimeLimited;	
					dtLimited = FS.FrameWork.Function.NConvert.ToDateTime(TodayTime);
				}
				catch
				{
					this.Err = "ϵͳ�����������ʱ�������ʽ�Ƿ�������ϵϵͳ����Ա��";
					this.ErrCode = "ϵͳ�����������ʱ�������ʽ�Ƿ�������ϵϵͳ����Ա��";
					this.WriteErr();
					return "Error"; 
				}
				if(dtNow > dtLimited)
				{
					string allowApply=GetControlArgument("100031");
					if(allowApply=="Error")return "Error";
					if(allowApply.Trim()=="1")
					{
						return "OK";
					}
					else
					{
						return "Over";
					}					
				}
			}
			return "OK";
		}
		/// <summary>
		/// ��ȡϵͳ����
		/// </summary>
		/// <returns>����ʱ���ַ�������ΪError,��ϵͳ����δ����</returns>
		private string GetControlArgument(string ctlID)
		{
			string strSql = string.Empty;
			string ctlValue=string.Empty;

			if(this.Sql.GetSql("QueryControlerInfo.2",ref strSql) == -1) return string.Empty;				
			if (strSql == null) return string.Empty;						
			try
			{
                //{8892F821-0519-47a0-A4DF-B1C96EE8E679}ȡҽԺ����
				strSql=string.Format(strSql,ctlID,this.Hospital.ID);
				this.ExecQuery(strSql);
				while(this.Reader.Read())
				{
					ctlValue=this.Reader[0].ToString();
				}
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				this.WriteErr();
				if(Reader.IsClosed==false)Reader.Close();
				return "Error";
			}
			this.Reader.Close();
		
			if(ctlValue==string.Empty) 
			{
				this.Err = "ϵͳδά���������ã���������:"+ctlID+"����ϵϵͳ����Ա��";
				this.ErrCode ="ϵͳδά���������ã���������:"+ctlID+"����ϵϵͳ����Ա��";	
				this.WriteErr();
				return "Error";
			}
			return ctlValue;
		}
		#endregion
		#region ������Ŀ��¼��Ϣ����
		/// <summary>
		/// ���������¼��Ϣ
		/// </summary>
		/// <param name="OpsApp">�������뵥ʵ��</param>
		/// <param name="OperateInfo">������Ϣ��ʵ��</param>
		/// <returns>0 success -1 fail</returns>
        public int AddOperationInfo(FS.HISFC.Models.Operation.OperationAppllication OpsApp, FS.HISFC.Models.Operation.OperationInfo OperateInfo)
		{
			#region ���������¼��Ϣ
			///���������¼��Ϣ
			///Operation.Operation.AddOperationInfo.1
			///���룺23
			///������0 
			#endregion
			#region ��ȡ������Ŀ������Ϣ
			//----------------------------------------------------
			//�ֲ��������� �ͷ����йص���Ϣ
			string ls_ItemCode = string.Empty;	//������Ŀ����
			string ls_ItemName = string.Empty;	//������Ŀ����
			decimal ld_UnitPrice = 0;	//����
			decimal ld_FeeRate = 1;		//�շѱ���
			int	   li_Qty = 0;			//����
			string ls_StockUnit = string.Empty;	//��λ

            ls_ItemCode = OperateInfo.OperationItem.ID.ToString();
            ls_ItemName = OperateInfo.OperationItem.Name;
            ld_UnitPrice = OperateInfo.OperationItem.Price;
			ld_FeeRate = OperateInfo.FeeRate;
			li_Qty = OperateInfo.Qty;
			ls_StockUnit = OperateInfo.StockUnit;

			//�ֲ��������� �������йص���Ϣ
			string ls_OperateType = string.Empty;	//������ģ
			string ls_InciType = string.Empty;		//�п�����
			string ls_OpePos = string.Empty;		//������λ

			//ls_OperateType = OpsApp.OperateType.ID.ToString();
			//ls_InciType = OpsApp.InciType.ID.ToString();
			//ls_OpePos = OpsApp.OpePos.ID.ToString();
			ls_OperateType = OperateInfo.OperateType.ID.ToString();
			ls_InciType = OperateInfo.InciType.ID.ToString();
			ls_OpePos = OperateInfo.OpePos.ID.ToString();
			//----------------------------------------------------
			#endregion
			#region ��ȡ���߻�����Ϣ
			//--------------------------------------------------------
			//�ֲ��������� ���߻�����Ϣ
			string ls_ClinicCode = string.Empty;//סԺ��ˮ��/�����
			string ls_DeptCode = string.Empty;  //סԺ����
			ls_ClinicCode = OpsApp.PatientInfo.ID.ToString();
			/*
			#region �����ж���������������סԺ������ȷ��Ӧ������Ļ��ߺ�			
			switch (OpsApp.Pasource)
			{
				case "1":  //��������
					ls_ClinicCode = OpsApp.PatientInfo.Patient.PID.CardNo;
					break;
				case "2":  //סԺ����
					ls_ClinicCode = OpsApp.PatientInfo.Patient.PID.PatientNo;
					break;
				default:
					break;
			}			
			#endregion
			*/
			ls_DeptCode = OpsApp.PatientInfo.PVisit.PatientLocation.Dept.ID;
			//--------------------------------------------------------
			#endregion
			string strSql = string.Empty;
			string strGerm = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsGermCarrying).ToString();
			string strUrgent = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsUrgent).ToString();
			string strChange = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsChange).ToString();
			string strHeavy = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsHeavy).ToString();
			string strSpecial = FS.FrameWork.Function.NConvert.ToInt32(OpsApp.IsSpecial).ToString();
			string strValid = FS.FrameWork.Function.NConvert.ToInt32(OperateInfo.IsValid).ToString();
			string strMainFlag = FS.FrameWork.Function.NConvert.ToInt32(OperateInfo.IsMainFlag).ToString();
			if(this.Sql.GetSql("Operator.Operator.AddOperationInfo.1",ref strSql) == -1) return -1;
			try
			{
				string []str2 = new string[]{
												OpsApp.ID,ls_ClinicCode,ls_DeptCode,ls_ItemCode,ls_ItemName,
												ld_UnitPrice.ToString(),ld_FeeRate.ToString(),li_Qty.ToString(),ls_StockUnit,ls_OperateType,
												ls_InciType,OpsApp.ScreenUp,strGerm,ls_OpePos,strUrgent,
												strChange,strHeavy,strSpecial,strMainFlag,OperateInfo.Remark,
												strValid,OpsApp.User.ID.ToString(),System.DateTime.Now.ToString()
											};
				//��������¼�������Ӽ�¼
				//ÿ��5������
//				strSql = string.Format(strSql,OpsApp.OperationNo,ls_ClinicCode,ls_DeptCode,ls_ItemCode,ls_ItemName,
//					ld_UnitPrice.ToString(),ld_FeeRate.ToString(),li_Qty.ToString(),ls_StockUnit,ls_OperateType,
//					ls_InciType,OpsApp.ScreenUp,strGerm,ls_OpePos,strUrgent,
//					strChange,strHeavy,strSpecial,strMainFlag,OperateInfo.Remark,
//					strValid,OpsApp.User.ID.ToString(),System.DateTime.Now.ToString());
				return this.ExecNoQuery(strSql,str2);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return -1;            
			}			
			
		}
		/// <summary>
		/// ɾ���������뵥��������¼��Ϣ
		/// </summary>
		/// <param name="OpsApp">�������뵥ʵ��</param>
		/// <returns>0 success -1 fail</returns>
        public int DelOperationInfo(FS.HISFC.Models.Operation.OperationAppllication OpsApp)
		{
			#region ɾ��������¼��Ϣ
			///ɾ��������¼��Ϣ
			///Operation.Operation.DelOperationInfo.1
			///���룺23
			///������0 
			#endregion		
			
			string strSql = string.Empty;

			if(this.Sql.GetSql("Operator.Operator.DelOperationInfo.1",ref strSql) == -1) return -1;
			try
			{				
				strSql = string.Format(strSql,OpsApp.ID);
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
		#endregion
		#region ������Ա��ɫ����
		/// <summary>
		/// �������뵥�漰����Ա��ɫ����
		/// </summary>
		/// <param name="OpsApp">�������뵥����</param>
		/// <returns>0 success,-1 fail</returns>
        public int ProcessRoleForApply(FS.HISFC.Models.Operation.OperationAppllication OpsApp)
		{	
			//�Ȳ������߶�ʮһ��������������뵥��ԭ�е����н�ɫ����
			try
			{
				if(DelArrangeRoleAll(OpsApp) == -1) return -1;
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return -1;            
			}

			//���н�ɫ(����������ҽ����ָ��ҽ�������֡�������ʿ������ҽ���Ƚ�ɫ)
			try
			{
				foreach(FS.HISFC.Models.Operation.ArrangeRole thisRole in OpsApp.RoleAl)
				{
                    //{69F783B4-65EB-4cc3-B489-2A7D5B5A5F00}
					//if(AddArrangeRole(OpsApp,thisRole,thisRole.RoleType.ID.ToString(),thisRole.RoleOperKind.ID.ToString(),thisRole.ForeFlag) == -1) return -1;
                    if (AddArrangeRole(OpsApp, thisRole, thisRole.RoleType.ID.ToString(), thisRole.RoleOperKind.ID.ToString(), thisRole.ForeFlag,thisRole.SupersedeDATE) == -1) return -1;
				}
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return -1;            
			}
			return 0;
		}
		/// <summary>
		///  ���������Ա��ɫ
		/// </summary>
		/// <param name="OpsApp">�������뵥����</param>
		/// <param name="Person">��ɫ��Ա����</param>
		/// <param name="strRole">��ɫ����</param>
		/// <param name="strOperKind">��ɫ״̬ ����/�Ӱ�/ֱ���</param>
		/// <param name="strForeFlag">��ɫ����</param>
		/// <returns>0 success, -1 fail</returns>
        /// {69F783B4-65EB-4cc3-B489-2A7D5B5A5F00}
        public int AddArrangeRole(FS.HISFC.Models.Operation.OperationAppllication OpsApp, FS.HISFC.Models.Operation.ArrangeRole employee, string strRole, string strOperKind, string strForeFlag, DateTime supersedeDATE)
		{
			string strSql=string.Empty;
			if(this.Sql.GetSql("Operator.Operator.AddArrangeRole.1",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,OpsApp.ID,strRole,employee.ID.ToString(),
                    employee.Name, strForeFlag, OpsApp.User.ID.ToString(), strOperKind, supersedeDATE.ToString());
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
		/// ɾ���������е�������Ա��ɫ����
		/// </summary>
		/// <param name="OpsApp">�������뵥����ʵ��</param>
		/// <returns>0 success -1 fail</returns>
        public int DelArrangeRoleAll(FS.HISFC.Models.Operation.OperationAppllication OpsApp)
		{
			string strSql=string.Empty;
			if(this.Sql.GetSql("Operator.Operator.DelArrangeRole.1",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,OpsApp.ID);
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
		/// ɾ����������ĳһ��ɫ����Ա����
		/// </summary>
		/// <param name="OpsApp">�������뵥����</param>
		/// <param name="strRole">��ɫ����</param>
		/// <returns>0 success -1 fail</returns>
        public int DelArrangeRole(FS.HISFC.Models.Operation.OperationAppllication OpsApp, string strRole)
		{
			string strSql=string.Empty;
			if(this.Sql.GetSql("Operator.Operator.DelArrangeRole.2",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,OpsApp.ID,strRole);
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

		#endregion
		#region �������ϴ��������豸��
		/// <summary>
		/// �������뵥�漰���������ϴ���
		/// </summary>
		/// <param name="OpsApp">�������뵥����</param>
		/// <returns>0 success,-1 fail</returns>
        public int ProcessAppaRecForApply(FS.HISFC.Models.Operation.OperationAppllication OpsApp)
		{	
			//����������������뵥��ԭ�е������������ϰ�����Ϣ
			try
			{
				if(DelAppaRecAll(OpsApp) == -1) return -1;
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return -1;            
			}
			
			//�����������ϰ�����Ϣ
			try
			{
				foreach(FS.HISFC.Models.Operation.OpsApparatusRec thisRec in OpsApp.AppaRecAl)
				{
					if(AddAppaRec(thisRec) == -1) return -1;
				}
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return -1;            
			}
			return 0;
		}
		/// <summary>
		/// �����������ϼ�¼��Ϣ
		/// </summary>
		/// <param name="OpsAppaRec">�������ϼ�¼����</param>
		/// <returns> 0 success -1 fail</returns>
		public int AddAppaRec(FS.HISFC.Models.Operation.OpsApparatusRec OpsAppaRec)
		{
			string strSql=string.Empty;
			if(this.Sql.GetSql("Operator.Operator.AddAppaRec.1",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,OpsAppaRec.OperationNo,OpsAppaRec.OpsAppa.ID.ToString(),
					OpsAppaRec.OpsAppa.Name,OpsAppaRec.foreflag.ToString(),OpsAppaRec.Qty.ToString(),
					OpsAppaRec.AppaUnit,OpsAppaRec.User.ID.ToString());
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
		/// ɾ��ĳһ�������뵥�����������ϼ�¼��Ϣ
		/// </summary>
		/// <param name="OpsApp">�������뵥����</param>
		/// <param name="strRole">��ɫ����</param>
		/// <returns>0 success -1 fail</returns>
        public int DelAppaRecAll(FS.HISFC.Models.Operation.OperationAppllication OpsApp)
		{
			string strSql=string.Empty;
			if(this.Sql.GetSql("Operator.Operator.DelAppaRec.1",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,OpsApp.ID);
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
		#endregion
		/// <summary>
		/// �����Ҹ���
		/// </summary>
		/// <param name="OpsApplication">�������뵥ʵ��</param>
		/// <returns>0 success -1 fail</returns>
        public int ChangeOperatorRoom(FS.HISFC.Models.Operation.OperationAppllication OpsApp)
		{
			#region �����Ҹ���
			///����������
			///Operation.Operation.ChangeOperatorRoom.1
			///���룺60
			///������0 
			#endregion
			string strSql=string.Empty;

			if(this.Sql.GetSql("Operator.Operator.UpdateApplication.5",ref strSql)==-1) return -1;
			try
			{			
				//����������Ÿ����������ڡ�������(ִ�п���)��������
				strSql = string.Format(strSql,OpsApp.ID,OpsApp.PreDate.ToString(),OpsApp.OperateRoom.ID.ToString(),
					OpsApp.TableType,this.Operator.ID.ToString());
			}
			catch(Exception ex)
			{
				this.Err = "HISFC.Operator.Operator.ChangeOperatorRoom";
				this.ErrCode = ex.Message;
				this.WriteErr();
				return -1;            
			}

			if (strSql == null) 
				return -1;
			
			return this.ExecNoQuery(strSql);
		}	

		/// <summary>
		/// �������շѱ�־
		/// </summary>
		/// <param name="operationNo"></param>
		/// <returns></returns>
		public int UpdateOpsFee(string operationNo)
		{
			string sql=string.Empty;

			if(this.Sql.GetSql("Operator.Operator.UpdateFee",ref sql)==-1)
			{
				return -1;
			}

			try
			{
				sql=string.Format(sql,operationNo);

				return this.ExecNoQuery(sql);
			}
			catch(Exception e)
			{
				this.Err="�������շѱ�־����[Operator.Operator.UpdateFee]"+e.Message;
				this.ErrCode=e.Message;
				return -1;
			}
		}
		/// <summary>
		/// �жϻ��������Ƿ����շ�: 1���շѡ�0δ�շѡ�2û��������-1����
		/// </summary>
		/// <param name="inpatientNo"></param>
		/// <returns></returns>
		public int IsFee(string inpatientNo)
		{
			string sql = string.Empty;

			if(this.Sql.GetSql("Operator.Operator.QueryIsFee",ref sql)==-1)
			{
				return -1;
			}

			try
			{
				sql=string.Format(sql,inpatientNo);

				if(this.ExecQuery(sql)==-1)
				{
					return -1;
				}

				//û�н�������
				while(this.Reader.Read())
				{
					if(this.Reader[0].ToString()=="0")
					{
						this.Reader.Close();
						return 0;
					}
				}
				this.Reader.Close();

				return 1;
			}
			catch(Exception e)
			{
				this.Err="��ѯ������������ʱ����!"+e.Message;
				this.ErrCode=e.Message;
				return -1;
			}
		}
		/// <summary>
		/// �ж�ʱ�����������Ƿ��Ѿ����� -1 ����1 �������ظ����룬2 �����ظ�����
		/// </summary>
		/// <param name="InaptientNo"></param>
		/// <param name="Diagnose"></param>
		/// <param name="PreDate"></param>
		/// <returns></returns>
		public int IsExistSameApplication(string InaptientNo,string Diagnose,string PreDate)
		{
			string sql=string.Empty;
			if(this.Sql.GetSql("Operator.Operator.IsExistSameApplication",ref sql)==-1)return -1;
			try
			{
                string[] str = new string[] { InaptientNo, Diagnose, PreDate };
                //sql=string.Format(sql,InaptientNo,Diagnose,PreDate);
                if (this.ExecQuery(sql, str) == -1)
				{
					return -1;
				}

                //�����ظ����������� ��Ҫ��ʾҽ��(��SQL���)
				if(this.Reader.Read())
				{
                    decimal dQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0].ToString());
                    if (dQty > 0)
                    {
                        this.Reader.Close();
                        return 2;  //��ѯ������ͬ�����������¼�������ظ�����
                    }
                    else
                    {
                        this.Reader.Close();
                        return 1;
                    }
				}
				else
                { //û�н�������
					this.Reader.Close();
					return 1;
				}
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				return -1;
			}
		}
		/// <summary>
		/// ĳ������ĳ������������̨���Ƿ�����ظ� ��1 �������ظ����룬2 �����ظ�����
		/// </summary>
		/// <param name="InaptientNo"></param>
		/// <param name="Diagnose"></param>
		/// <param name="PreDate"></param>
		/// <returns></returns>
		public int IsExistSameApplicationTableSeq(string strBegin,string strEnd,string ExeDept,string TableType)
		{
			string sql=string.Empty;
			if(this.Sql.GetSql("Operator.Operator.IsExistSameApplicationTableSeq",ref sql)==-1)
			{
				return -1;
			}

			try
			{
				sql=string.Format(sql,ExeDept,TableType,strBegin,strEnd);
				if(this.ExecQuery(sql)==-1)
				{
					return -1;
				}

				//û�н�������
				if(this.Reader.Read())
				{
					this.Reader.Close();
					return 1;
				}
				else
				{ //�����ظ����������� ��Ҫ��ʾҽ��
					this.Reader.Close();
					return 2;
				}
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				return -1;
			}
		}
		/// <summary>
		/// �ж�ʱ�����������Ƿ�Ӧ������̨  1 �Ѿ�����������̨ 2 û����������̨   -1 ����
		/// </summary>
		/// <param name="BeginDate">��ʼʱ��</param>
		/// <param name="EndDate">����ʱ��</param>
		/// <param name="ExeDept">ִ�п���</param>
		/// <param name="ExeDept">�������</param>
		/// <param name="Type">����</param>
		/// <returns></returns>
		public int SameDeptApplication(string BeginDate,string EndDate,string ExeDept,string AppDept ,string Type )
		{
			string sql=string.Empty;
			if(this.Sql.GetSql("Operator.Operator.SameDeptApplication",ref sql)==-1)
			{
				return -1;
			}

			try
			{
				sql=string.Format(sql,BeginDate,EndDate,ExeDept,AppDept,Type);
				if(this.ExecQuery(sql)==-1)
				{
					return -1;
				}

				//û�н������� ��Ҫ��ʾҽ��
				if(this.Reader.Read())
				{
					this.Reader.Close();
					return 1;
				}
				else
				{ //������������ 
					this.Reader.Close();
					return 2;
				}
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				return -1;
			}
		}

      
        #region �������Sql����ѯ������Ϣ�б�--˽��
        //���߲�ѯ
        /// <summary>
        /// ���߲�ѯ-��סԺ�Ų�
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <returns>������Ϣ PatientInfo</returns>
        public ArrayList QueryPatientInfoBypatientNO(string PatientNO)
        {
            string strSql1 = string.Empty;
            string strSql2 = string.Empty;

            #region �ӿ�˵��

            /////RADT.Inpatient.PatientQuery.where.7
            //����:סԺ��ˮ��
            //������������Ϣ

            #endregion

            //strSql1 = PatientQuerySelect();
            if (Sql.GetSql("RADT.Inpatient.PatientQuery.Select.1", ref strSql1) == -1)
            {
                return null;
            }
            if (strSql1 == null) return null;

            //if (Sql.GetSql("RADT.Inpatient.PatientQuery.Where.7", ref strSql2) == -1)
            //{
            //    return null;
            //}
            strSql2 = " where   patient_no = '{0}' ";
            try
            {
                strSql1 = strSql1 + " " + string.Format(strSql2, PatientNO);
            }
            catch
            {
                Err = " where   patient_no = '{0}'��ֵ��ƥ�䣡";
                ErrCode = " where   patient_no = '{0}'��ֵ��ƥ�䣡";
                return null;
            }
            ArrayList al = new ArrayList();
            try
            {
                al = myPatientQuery(strSql1);
            }
            catch (Exception ee)
            {
                Err = "û���ҵ�������Ϣ!" + ee.Message;
                WriteErr();
                return null;
            }
            return al;
        }

        private ArrayList myPatientQuery(string SQLPatient)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.RADT.PatientInfo PatientInfo;
            ProgressBarText = "���ڲ�ѯ����...";
            ProgressBarValue = 0;

            if (ExecQuery(SQLPatient) == -1)
            {
                return null;
            }
            //ȡϵͳʱ��,�����õ������ַ���
            DateTime sysDate = GetDateTimeFromSysDateTime();

            try
            {
                while (Reader.Read())
                {
                    PatientInfo = new  FS.HISFC.Models.RADT.PatientInfo();

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
                    //,76 �ܹ����TotCost ,77 �Էѽ�� OwnCost,	78 �Ը���� PayCost,79 ���ѽ�� PubCost
                    //,80 ʣ���� LeftCost,81 �Żݽ��
                    //,82  Ԥ����� ��83    ���ý��(�ѽ�)��84    Ԥ�����(�ѽ�) �� 85 ��������(�ϴ�)     
                    //��86 ������, 87 ת�����,88 TransferPrepayCost ת��Ԥ����δ��)  ,89 ת����ý��(δ��),90 ����״̬91�������޶�겿��
                    //,92 ��ע93�������޶�94Ѫ���ɽ�95סԺ����96��λ����97�յ�����98�������99��סҽʦ100�������յ��Ժ�
                    //-->

                    #endregion

                    try
                    {
                        if (!Reader.IsDBNull(0)) PatientInfo.ID = Reader[0].ToString(); // סԺ��ˮ��
                        if (!Reader.IsDBNull(0)) PatientInfo.ID = Reader[0].ToString(); // סԺ��ˮ��
                        if (!Reader.IsDBNull(1)) PatientInfo.Name = Reader[1].ToString(); //����
                        if (!Reader.IsDBNull(1)) PatientInfo.Name = Reader[1].ToString(); //����
                        if (!Reader.IsDBNull(2)) PatientInfo.PID.PatientNO = Reader[2].ToString(); //  סԺ��						
                        if (!Reader.IsDBNull(3)) PatientInfo.PID.CardNO = Reader[3].ToString(); //���￨��
                        if (!Reader.IsDBNull(4)) PatientInfo.PID.CaseNO = Reader[4].ToString(); // ������
                        if (!Reader.IsDBNull(5)) PatientInfo.SSN = Reader[5].ToString(); // ҽ��֤��
                        if (!Reader.IsDBNull(6)) PatientInfo.PVisit.MedicalType.ID = Reader[6].ToString(); //   ҽ�����id
                        if (!Reader.IsDBNull(7)) PatientInfo.Sex.ID = Reader[7].ToString(); //  �Ա�
                        if (!Reader.IsDBNull(8)) PatientInfo.IDCard = Reader[8].ToString(); //  ���֤��
                        if (!Reader.IsDBNull(9)) PatientInfo.Memo = Reader[9].ToString(); //  ƴ��
                        if (!Reader.IsDBNull(10)) PatientInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[10]); // ����
                        if (!Reader.IsDBNull(11)) PatientInfo.Profession.ID = Reader[11].ToString(); //  ְҵ����
                        if (!Reader.IsDBNull(12)) PatientInfo.Profession.Name = Reader[12].ToString(); //ְҵ����
                        if (!Reader.IsDBNull(13)) PatientInfo.CompanyName = Reader[13].ToString(); //  ������λ
                        if (!Reader.IsDBNull(14)) PatientInfo.PhoneBusiness = Reader[14].ToString(); //  ������λ�绰
                        if (!Reader.IsDBNull(15)) PatientInfo.User01 = Reader[15].ToString(); //  ��λ�ʱ�
                        if (!Reader.IsDBNull(16)) PatientInfo.AddressHome = Reader[16].ToString(); //  ���ڻ��ͥ��ַ
                        if (!Reader.IsDBNull(17)) PatientInfo.PhoneHome = Reader[17].ToString(); //  ��ͥ�绰
                        if (!Reader.IsDBNull(18)) PatientInfo.User02 = Reader[18].ToString(); //  ���ڻ��ͥ�ʱ�
                        if (!Reader.IsDBNull(20)) PatientInfo.DIST = Reader[20].ToString(); // ����name
                        if (!Reader.IsDBNull(21)) PatientInfo.AreaCode = Reader[21].ToString(); //  �����ش���
                        if (!Reader.IsDBNull(23)) PatientInfo.Nationality.ID = Reader[23].ToString(); //  ����id
                        if (!Reader.IsDBNull(24)) PatientInfo.Nationality.Name = Reader[24].ToString(); // ����name
                        if (!Reader.IsDBNull(25)) PatientInfo.Kin.ID = Reader[25].ToString(); //  ��ϵ��id
                        if (!Reader.IsDBNull(26)) PatientInfo.Kin.Name = Reader[26].ToString(); //  ��ϵ������
                        if (!Reader.IsDBNull(27)) PatientInfo.Kin.RelationPhone = Reader[27].ToString(); //  ��ϵ�˵绰
                        if (!Reader.IsDBNull(28)) PatientInfo.Kin.RelationAddress = Reader[28].ToString(); //  ��ϵ�˵�ַ
                        if (!Reader.IsDBNull(29)) PatientInfo.Kin.Relation.ID = Reader[29].ToString(); //  ��ϵ�˹�ϵid
                        if (!Reader.IsDBNull(30)) PatientInfo.Kin.Relation.Name = Reader[30].ToString(); //  ��ϵ�˹�ϵname
                        if (!Reader.IsDBNull(31)) PatientInfo.MaritalStatus.ID = Reader[31].ToString(); //  ����״��id
                        if (!Reader.IsDBNull(33)) PatientInfo.Country.ID = Reader[33].ToString(); //  ����id
                        if (!Reader.IsDBNull(34)) PatientInfo.Country.Name = Reader[34].ToString(); //��������
                        if (!Reader.IsDBNull(35)) PatientInfo.Height = Reader[35].ToString(); //  ���
                        if (!Reader.IsDBNull(36)) PatientInfo.Weight = Reader[36].ToString(); //  ����
                        if (!Reader.IsDBNull(38)) PatientInfo.BloodType.ID = Reader[38].ToString(); //  ABOѪ��
                        if (!Reader.IsDBNull(39)) PatientInfo.Disease.IsMainDisease = FS.FrameWork.Function.NConvert.ToBoolean(Reader[39]); //  �ش󼲲���־
                        if (!Reader.IsDBNull(40)) PatientInfo.Disease.IsAlleray = FS.FrameWork.Function.NConvert.ToBoolean(Reader[40]); //  ������־
                        if (!Reader.IsDBNull(41)) PatientInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[41]); //  ��Ժ����
                        if (!Reader.IsDBNull(42)) PatientInfo.PVisit.PatientLocation.Dept.ID = Reader[42].ToString(); //  ���Ҵ���
                        if (!Reader.IsDBNull(43)) PatientInfo.PVisit.PatientLocation.Dept.Name = Reader[43].ToString(); // ��������
                        if (!Reader.IsDBNull(44)) PatientInfo.Pact.PayKind.ID = Reader[44].ToString();
                        // �������id 1-�Է�  2-���� 3-������ְ 4-�������� 5-���Ѹ߸�
                        if (!Reader.IsDBNull(45)) PatientInfo.Pact.PayKind.Name = Reader[45].ToString(); //  �����������
                        if (!Reader.IsDBNull(46)) PatientInfo.Pact.ID = Reader[46].ToString(); //��ͬ����
                        if (!Reader.IsDBNull(47)) PatientInfo.Pact.Name = Reader[47].ToString(); // ��ͬ��λ����
                        if (!Reader.IsDBNull(48)) PatientInfo.PVisit.PatientLocation.Bed.ID = Reader[48].ToString(); // ����
                        if (!Reader.IsDBNull(48))
                            PatientInfo.PVisit.PatientLocation.Bed.Name = PatientInfo.PVisit.PatientLocation.Bed.ID.Length > 4
                                                                            ? PatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4)
                                                                            : PatientInfo.PVisit.PatientLocation.Bed.ID; // ����
                        if (!Reader.IsDBNull(90)) PatientInfo.PVisit.PatientLocation.Bed.Status.ID = Reader[90].ToString(); //��λ״̬
                        if (!Reader.IsDBNull(90)) PatientInfo.PVisit.PatientLocation.Bed.InpatientNO = Reader[0].ToString(); // סԺ��ˮ��
                        if (!Reader.IsDBNull(49)) PatientInfo.PVisit.PatientLocation.NurseCell.ID = Reader[49].ToString(); //����Ԫ����
                        if (!Reader.IsDBNull(50)) PatientInfo.PVisit.PatientLocation.NurseCell.Name = Reader[50].ToString(); // ����Ԫ����
                        if (!Reader.IsDBNull(51)) PatientInfo.PVisit.AdmittingDoctor.ID = Reader[51].ToString(); //ҽʦ����(סԺ)
                        if (!Reader.IsDBNull(52)) PatientInfo.PVisit.AdmittingDoctor.Name = Reader[52].ToString(); //ҽʦ����(סԺ)
                        if (!Reader.IsDBNull(53)) PatientInfo.PVisit.AttendingDoctor.ID = Reader[53].ToString(); //ҽʦ����(����)
                        if (!Reader.IsDBNull(54)) PatientInfo.PVisit.AttendingDoctor.Name = Reader[54].ToString(); //ҽʦ����(����)
                        if (!Reader.IsDBNull(55)) PatientInfo.PVisit.ConsultingDoctor.ID = Reader[55].ToString(); //ҽʦ����(����)
                        if (!Reader.IsDBNull(56)) PatientInfo.PVisit.ConsultingDoctor.Name = Reader[56].ToString(); //ҽʦ����(����)
                        if (!Reader.IsDBNull(57)) PatientInfo.PVisit.TempDoctor.ID = Reader[57].ToString(); //ҽʦ����(ʵϰ)
                        if (!Reader.IsDBNull(58)) PatientInfo.PVisit.TempDoctor.Name = Reader[58].ToString(); //ҽʦ����(ʵϰ)
                        if (!Reader.IsDBNull(59)) PatientInfo.PVisit.AdmittingNurse.ID = Reader[59].ToString(); // ��ʿ����(����)
                        if (!Reader.IsDBNull(60)) PatientInfo.PVisit.AdmittingNurse.Name = Reader[60].ToString(); // ��ʿ����(����)
                        if (!Reader.IsDBNull(61)) PatientInfo.PVisit.Circs.ID = Reader[61].ToString(); // ��Ժ���id
                        if (!Reader.IsDBNull(62)) PatientInfo.PVisit.Circs.Name = Reader[62].ToString(); // ��Ժ���name
                        if (!Reader.IsDBNull(63)) PatientInfo.PVisit.AdmitSource.ID = Reader[63].ToString(); // ��Ժ;��id
                        if (!Reader.IsDBNull(64)) PatientInfo.PVisit.AdmitSource.Name = Reader[64].ToString(); // ��Ժ;��name
                        if (!Reader.IsDBNull(65)) PatientInfo.PVisit.InSource.ID = Reader[65].ToString();
                        // ��Ժ��Դid 1 -���� 2 -���� 3 -ת�� 4 -תԺ
                        if (!Reader.IsDBNull(66)) PatientInfo.PVisit.InSource.Name = Reader[66].ToString(); // ��Ժ��Դname
                        if (!Reader.IsDBNull(67)) PatientInfo.PVisit.InState.ID = Reader[67].ToString();
                        // ��Ժ״̬ סԺ�Ǽ�  i-�������� -��Ժ�Ǽ� o-��Ժ���� p-ԤԼ��Ժ n-�޷���Ժ
                        if (!Reader.IsDBNull(68)) PatientInfo.PVisit.PreOutTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[68]); // ��Ժ����(ԤԼ)
                        #region {8D72F2C7-624C-41e4-9922-7A5556B9D82E}
                        if (!Reader.IsDBNull(69))
                        {
                            if (FS.FrameWork.Function.NConvert.ToDateTime(Reader[69]) < FS.FrameWork.Function.NConvert.ToDateTime("1000-01-01"))
                            {
                                PatientInfo.PVisit.OutTime = DateTime.MinValue;
                            }
                            else//{3D0766DE-A5AA-409f-8A04-C56F4C9D53DA}
                            {
                                PatientInfo.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[69]);
                            }

                        }
                        #endregion
                        if (!Reader.IsDBNull(71)) PatientInfo.PVisit.ICULocation.ID = Reader[71].ToString(); //icu code
                        if (!Reader.IsDBNull(72)) PatientInfo.PVisit.ICULocation.Name = Reader[72].ToString(); //icu name
                        if (!Reader.IsDBNull(73)) PatientInfo.PVisit.PatientLocation.Building = Reader[73].ToString(); //¥
                        if (!Reader.IsDBNull(74)) PatientInfo.PVisit.PatientLocation.Floor = Reader[74].ToString(); //��
                        if (!Reader.IsDBNull(75)) PatientInfo.PVisit.PatientLocation.Room = Reader[75].ToString(); //��
                        if (!Reader.IsDBNull(76)) PatientInfo.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[76]); //�ܹ����TotCost
                        if (!Reader.IsDBNull(77)) PatientInfo.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[77]); //�Էѽ�� OwnCost
                        if (!Reader.IsDBNull(78)) PatientInfo.FT.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[78]); //�Ը���� PayCost
                        if (!Reader.IsDBNull(79)) PatientInfo.FT.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[79]); //���ѽ�� PubCost
                        if (!Reader.IsDBNull(80)) PatientInfo.FT.LeftCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[80]); //ʣ���� LeftCost
                        if (!Reader.IsDBNull(81)) PatientInfo.FT.RebateCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[81]); //�Żݽ��
                        if (!Reader.IsDBNull(82)) PatientInfo.FT.PrepayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[82]); // Ԥ�����
                        if (!Reader.IsDBNull(83)) PatientInfo.FT.BalancedCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[83]); //   ���ý��(�ѽ�)
                        if (!Reader.IsDBNull(84)) PatientInfo.FT.BalancedPrepayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[84]); //   Ԥ�����(�ѽ�)
                        if (!Reader.IsDBNull(85))
                        {
                            try
                            {
                                PatientInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[85]); //����ʱ��
                            }
                            catch { }
                        }
                        if (!Reader.IsDBNull(86)) PatientInfo.PVisit.MoneyAlert = FS.FrameWork.Function.NConvert.ToDecimal(Reader[86]); //������
                        if (!Reader.IsDBNull(87)) PatientInfo.PVisit.ZG.ID = Reader[87].ToString(); //  ת�����
                        if (!Reader.IsDBNull(88)) PatientInfo.FT.TransferPrepayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[88]); // ת��Ԥ����δ��) 
                        if (!Reader.IsDBNull(89)) PatientInfo.FT.TransferTotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[89]); //  ת����ý�δ��) 
                        if (!Reader.IsDBNull(91)) PatientInfo.FT.OvertopCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[91]); //   ���ѳ�����
                        if (!Reader.IsDBNull(92)) PatientInfo.Memo = Reader[92].ToString(); //��ע
                        if (!Reader.IsDBNull(93)) PatientInfo.FT.DayLimitCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[93]); //   �������޶�
                        if (!Reader.IsDBNull(94)) PatientInfo.FT.BloodLateFeeCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[94]); //  Ѫ���ɽ�
                        if (!Reader.IsDBNull(95)) PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(Reader[95]); //סԺ����
                        if (!Reader.IsDBNull(96)) PatientInfo.FT.BedLimitCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[96].ToString()); //��λ����
                        if (!Reader.IsDBNull(97)) PatientInfo.FT.AirLimitCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[97].ToString()); //�յ�����
                        if (!Reader.IsDBNull(99)) PatientInfo.DoctorReceiver.ID = Reader[99].ToString();
                        if (!Reader.IsDBNull(98)) PatientInfo.ClinicDiagnose = Reader[98].ToString();
                        if (!Reader.IsDBNull(100)) PatientInfo.ProCreateNO = Reader[100].ToString(); //�������յ��Ժ�
                        PatientInfo.IsHasBaby = FS.FrameWork.Function.NConvert.ToBoolean(Reader[101].ToString()); //�Ƿ���Ӥ��
                        PatientInfo.Disease.Tend.Name = Reader[102].ToString(); //������
                        //������ȡ���
                        PatientInfo.FT.FixFeeInterval = FS.FrameWork.Function.NConvert.ToInt32(Reader[103].ToString());
                        //�ϴ���ȡʱ��
                        PatientInfo.FT.PreFixFeeDateTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[104].ToString());
                        //���ѳ��괦�� 0���겻��1��������
                        PatientInfo.FT.BedOverDeal = Reader[105].ToString();
                        //���޶��ۼ�
                        PatientInfo.FT.DayLimitTotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[106].ToString());
                        //����״̬: 0 ���財�� 1 ��Ҫ���� 2 ҽ��վ�γɲ��� 3 �������γɲ��� 4�������
                        PatientInfo.CaseState = Reader[107].ToString();

                        PatientInfo.ExtendFlag = Reader[108].ToString(); //�Ƿ��������޶�� 0 ��ͬ�� 1 ͬ�� ��ɽһԺ����
                        PatientInfo.ExtendFlag1 = Reader[109].ToString(); //��չ���1
                        PatientInfo.ExtendFlag2 = Reader[110].ToString(); //��չ���2
                        PatientInfo.FT.BoardCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[111]); //��ʳ�����ܶ�
                        PatientInfo.FT.BoardPrepayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[112]); //��ʳԤ�����
                        PatientInfo.PVisit.BoardState = Reader[113].ToString(); //��ʳ����״̬��0��Ժ 1��Ժ
                        PatientInfo.FT.FTRate.OwnRate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[114].ToString()); //�Էѱ���
                        PatientInfo.FT.FTRate.PayRate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[115].ToString()); //�Ը�����
                        PatientInfo.FT.DrugFeeTotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[116].ToString()); //���ѻ��߹���ҩƷ�ۼ�(���޶�)
                        PatientInfo.MainDiagnose = Reader[117].ToString(); //����סԺ�����

                        PatientInfo.Age = GetAge(PatientInfo.Birthday, sysDate); //���ݳ�������ȡ��������
                        PatientInfo.IsEncrypt = FS.FrameWork.Function.NConvert.ToBoolean(Reader[118].ToString());
                        PatientInfo.NormalName = Reader[119].ToString();
                        if (!Reader.IsDBNull(120)) PatientInfo.BalanceNO = FS.FrameWork.Function.NConvert.ToInt32(Reader[120].ToString());//�������
                        //{F0C48258-8EFB-4356-B730-E852EE4888A0}
                        PatientInfo.ExtendFlag1 = Reader[121].ToString();//����


                        if (PatientInfo.PID.PatientNO.StartsWith("B"))
                        {
                            //�Ƿ�Ӥ��
                            PatientInfo.IsBaby = true;
                        }
                        else
                        {
                            PatientInfo.IsBaby = false;
                        }

                        //{2FA0D4CE-E2EB-4bc7-975A-3693B71C62CF}
                        if (!Reader.IsDBNull(122))
                        {
                            PatientInfo.IsStopAcount = FS.FrameWork.Function.NConvert.ToBoolean(Reader[122].ToString());
                        }
                        PatientInfo.PVisit.AlertType.ID = this.Reader[123].ToString();
                        PatientInfo.PVisit.BeginDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[124]);
                        PatientInfo.PVisit.EndDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[125]);
                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        WriteErr();
                        if (!Reader.IsClosed)
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

                    ProgressBarValue++;
                    al.Add(PatientInfo);
                }
            } //�׳�����
            catch (Exception ex)
            {
                Err = "��û��߻�����Ϣ����" + ex.Message;
                ErrCode = "-1";
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return al;
            }
            Reader.Close();

            ProgressBarValue = -1;
            return al;
        }


        public ArrayList GetALLOpsApp(string beginTime,string endTime)
        {
            ArrayList myAl = new ArrayList();
            //ҵ����򣺻�ȡ���е���������Ϣ		
            string strSql = GetOperationSql();
            if (strSql == null)
            {
                return null;
            }

            string strSqlwhere = string.Empty;
            if (this.Sql.GetSql("Operator.Operator.GetApplication.33", ref strSqlwhere) == -1)
            {
                return myAl;
            }

            try
            {
                strSqlwhere = string.Format(strSqlwhere,beginTime, endTime);
            }
            catch (Exception ex)
            {
                this.Err = "Operator.Operator.GetApplication.33";
                this.ErrCode = ex.Message;
                this.WriteErr();
                return myAl;
            }
            strSql = strSql + " \n" + strSqlwhere;
            myAl = GetOpsAppListFromSql(strSql);
            return myAl;
        }





        public ArrayList GetOpsAppListforzdly(FS.HISFC.Models.RADT.PatientInfo PatientInfo, string Pasource, string endTime)
        {
            if (PatientInfo == null)
            {
                return null;	//����Ļ��߶���Ϊ��
            }

            ArrayList myAl = new ArrayList();

            //ҵ�������ѡ���û�������ʱ��С�ڸ���ʱ���������Ч��δ�����������뵥��

            string strSql = string.Empty;
            if (this.Sql.GetSql("Operator.Operator.GetApplication.99", ref strSql) == -1)
            {
                return myAl;
            }

            try
            {
                switch (Pasource)
                {
                    case "1":
                        //strSql = string.Format(strSql,PatientInfo.Patient.PID.CardNo,Pasource,endTime);
                        strSql = string.Format(strSql, PatientInfo.ID.ToString(), Pasource, endTime);
                        break;
                    case "2":
                        //����סԺ��ˮ�Ÿ�ֵ			
                        //PatientInfo.Patient.ID = this.GetInPatientNo(PatientInfo.Patient.PID.ID.ToString());
                        //strSql = string.Format(strSql,PatientInfo.Patient.PID.PatientNo,Pasource,endTime);
                        strSql = string.Format(strSql, PatientInfo.ID.ToString(), Pasource, endTime);
                        break;
                }
            }
            catch (Exception ex)
            {
                this.Err = "HISFC.Operator.Operator.GetOpsAppList 99";
                this.ErrCode = ex.Message;
                this.WriteErr();
                return myAl;
            }

            this.ExecQuery(strSql);
            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Operation.OperationAppllication opsApplication = new FS.HISFC.Models.Operation.OperationAppllication();

                    opsApplication.ID = Reader[0].ToString();										//�������					
                    try
                    {
                        opsApplication.OperationDoctor.ID = Reader[1].ToString();					//����ҽ��
                        // by zlw 2006-5-24
                        opsApplication.User01 = Reader[1].ToString();	//����ҽ�����ڿ���
                    }
                    catch { }

                    opsApplication.GuideDoctor.ID = Reader[2].ToString();											//ָ��ҽ��	

                    opsApplication.PreDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[3].ToString());		//����ԤԼʱ��					
                    try
                    {
                        opsApplication.Duration = System.Convert.ToDecimal(Reader[4].ToString());					//����Ԥ����ʱ					
                    }
                    catch { }
                    opsApplication.AnesType.ID = Reader[5].ToString();												//��������

                    opsApplication.ExeDept.ID = Reader[6].ToString();												//ִ�п���

                    opsApplication.OperateRoom = opsApplication.ExeDept as FS.HISFC.Models.Base.Department;	//������(������Ҫ�����뵥��������˵�������Ҽ�ִ�п���)

                    opsApplication.TableType = Reader[7].ToString();					//0��̨1��̨2��̨					

                    opsApplication.ApplyDoctor.ID = Reader[8].ToString();				//����ҽ��


                    opsApplication.ApplyDoctor.Dept.ID = Reader[9].ToString();//�������

                    opsApplication.ApplyDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());	//����ʱ��
                    opsApplication.ApplyNote = Reader[11].ToString();					//���뱸ע					

                    opsApplication.ApproveDoctor.ID = Reader[12].ToString();//����ҽ��					

                    opsApplication.ApproveDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[13].ToString());	//����ʱ��
                    opsApplication.ApproveNote = Reader[14].ToString();					//������ע
                    opsApplication.OperationType.ID = Reader[15].ToString();				//������ģ
                    opsApplication.InciType.ID = Reader[16].ToString();					//�п�����					

                    string strGerm = Reader[17].ToString();						//1 �о� 0�޾�
                    opsApplication.IsGermCarrying = FS.FrameWork.Function.NConvert.ToBoolean(strGerm);

                    opsApplication.ScreenUp = Reader[18].ToString();					//1 Ļ�� 2 Ļ��					
                    opsApplication.BloodType.ID = Reader[19].ToString();				//ѪҺ�ɷ�					
                    try
                    {
                        opsApplication.BloodNum = System.Convert.ToDecimal(Reader[20].ToString());		//Ѫ��
                    }
                    catch { }
                    opsApplication.BloodUnit = Reader[21].ToString();					//��Ѫ��λ
                    opsApplication.OpsNote = Reader[22].ToString();						//����ע������
                    opsApplication.AneNote = Reader[23].ToString();						//����ע������				
                    opsApplication.ExecStatus = Reader[24].ToString();					//1�������� 2 �������� 3�������� 4�������


                    string strFinished = Reader[25].ToString();					//0δ������/1��������
                    opsApplication.IsFinished = FS.FrameWork.Function.NConvert.ToBoolean(strFinished);


                    string strAnesth = Reader[26].ToString();					//0δ����/1������
                    opsApplication.IsAnesth = FS.FrameWork.Function.NConvert.ToBoolean(strAnesth);

                    opsApplication.Folk = Reader[27].ToString();						//ǩ�ּ���
                    opsApplication.RelaCode.ID = Reader[28].ToString();					//������ϵ
                    opsApplication.FolkComment = Reader[29].ToString();					//�������

                    try
                    {
                        string strUrgent = Reader[30].ToString();					//�Ӽ�����,1��/0��
                        opsApplication.IsUrgent = FS.FrameWork.Function.NConvert.ToBoolean(strUrgent);
                    }
                    catch { }
                    try
                    {
                        string strChange = Reader[31].ToString();					//1��Σ/0��
                        opsApplication.IsChange = FS.FrameWork.Function.NConvert.ToBoolean(strChange);
                    }
                    catch { }
                    try
                    {
                        string strHeavy = Reader[32].ToString();						//1��֢/0��
                        opsApplication.IsHeavy = FS.FrameWork.Function.NConvert.ToBoolean(strHeavy);
                    }
                    catch { }
                    try
                    {
                        opsApplication.IsSpecial = FS.FrameWork.Function.NConvert.ToBoolean(Reader[33].ToString());	//1��������/0��
                    }
                    catch { }

                    opsApplication.User.ID = Reader[34].ToString();	//����Ա

                    try
                    {
                        opsApplication.IsUnite = FS.FrameWork.Function.NConvert.ToBoolean(Reader[35].ToString());//1�ϲ�/0��	
                    }
                    catch { }
                    opsApplication.OperateKind = Reader[37].ToString();					//1��ͨ2����3��Ⱦ				
                    try
                    {
                        string strIsNeedAcco = Reader[38].ToString();					//�Ƿ���Ҫ��̨��ʿ
                        opsApplication.IsAccoNurse = FS.FrameWork.Function.NConvert.ToBoolean(strIsNeedAcco);
                    }
                    catch { }
                    try
                    {
                        string strIsNeedPrep = Reader[39].ToString();					//�Ƿ���ҪѲ�ػ�ʿ
                        opsApplication.IsPrepNurse = FS.FrameWork.Function.NConvert.ToBoolean(strIsNeedPrep);
                    }
                    catch { }
                    opsApplication.RoomID = Reader[40].ToString();
                    opsApplication.PatientInfo = PatientInfo.Clone();					//���߻�����Ϣ					
                    opsApplication.PatientSouce = Pasource;									//1����2סԺ
                    opsApplication.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[41].ToString());//1��Ч0��Ч					//1��Ч0��Ч	

                    myAl.Add(opsApplication);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��û����������뵥��Ϣ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return myAl;
            }
            this.Reader.Close();
            try
            {
                foreach (FS.HISFC.Models.Operation.OperationAppllication opsApplication in myAl)
                {
                    opsApplication.DiagnoseAl = this.GetIcdFromApp(opsApplication);							//����б�					
                    opsApplication.OperationInfos = this.GetOpsInfoFromApp(opsApplication.ID);				//������Ŀ��Ϣ�б�				
                    opsApplication.RoleAl = this.GetRoleFromApp(opsApplication.ID);							//��Ա��ɫ�б�
                    //�������Ը�ֵ��Ϊͻ�����ֲ����벿��ҵ����÷���
                    foreach (FS.HISFC.Models.Operation.ArrangeRole thisRole in opsApplication.RoleAl)
                    {
                        if (thisRole.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper1.ToString()
                            || thisRole.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper2.ToString()
                            || thisRole.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper3.ToString())
                            //����ҽʦ�б�
                            opsApplication.HelperAl.Add(thisRole.Clone());
                    }
                    opsApplication.AppaRecAl = GetAppaRecFromApp(opsApplication.ID);//�������ϰ����б�
                }
            }
            catch (Exception ex)
            {
                this.Err = "��û��������б���Ϣ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return myAl;
            }
            return myAl;
        }		
        
        #endregion
        protected abstract FS.HISFC.Models.RADT.PatientInfo GetPatientInfo(string id);
        protected abstract FS.HISFC.Models.Registration.Register GetRegInfo(string id);
        protected abstract string GetEmployeeName(string id);
        public abstract ArrayList GetIcdFromApp(FS.HISFC.Models.Operation.OperationAppllication opsApp);
	}
}
