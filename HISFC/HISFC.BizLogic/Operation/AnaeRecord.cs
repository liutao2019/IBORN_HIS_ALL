using System;
using System.Collections;
namespace FS.HISFC.BizLogic.Operation
{
	/// <summary>
	/// [��������: ����Ǽǿ�����]<br></br>
	/// [�� �� ��: ����ȫ]<br></br>
	/// [����ʱ��: 2006-09-27]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public abstract class AnaeRecord : FS.FrameWork.Management.Database
	{
        /// <summary>
        /// 
        /// </summary>
		public AnaeRecord()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
        /// �������뵥������ʵ��
		/// </summary>
        protected abstract FS.HISFC.BizLogic.Operation.Operation operationManager
        {
            get;
        }
		/// <summary>
		/// ���ָ����ŵ�����ǼǼ�¼
		/// </summary>
        /// <param name="operatorNo">�������</param>
		/// <returns>����ǼǼ�¼����</returns>
		public FS.HISFC.Models.Operation.AnaeRecord GetAnaeRecord( string operatorNo )
		{
			if(operatorNo.Length == 0)
			{
				return null;
			}
			
			string strSql = string.Empty;
			string strWhere = string.Empty;

			if(this.Sql.GetSql("Operator.AnaeRecord.GetAnaeRecord.Select.1",ref strSql) == -1) 
			{
				return null;
			}

			if(this.Sql.GetSql("Operator.AnaeRecord.GetAnaeRecord.Where.2",ref strWhere) == -1) 
			{
				return null;
			}

			strWhere = string.Format(strWhere,operatorNo);
			strSql = strSql + " \n" + strWhere;
			FS.HISFC.Models.Operation.AnaeRecord anaeRecord = new FS.HISFC.Models.Operation.AnaeRecord();
			//�Ȼ�ù������������뵥
			anaeRecord.OperationApplication = operationManager.GetOpsApp(operatorNo);
			//����������뵥û��ʵ��ֵ���������ǲ��ǵ������¼����������Ĺ���thisOpsRec.m_objOpsApp�ĸ�ֵ����������ġ�

			//��ѯSQL����Ѿ���ã���ʼ��ѯ
			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{
					anaeRecord.OperationApplication.ID = Reader[0].ToString();					//�������
					anaeRecord.OperationApplication.PatientInfo.ID  = Reader[1].ToString();//סԺ��ˮ��/�����(��'ZY010000000001')
					//----------------------------------------------------------------------------------------------------------
					anaeRecord.OperationApplication.PatientInfo.PID.ID = Reader[2].ToString();//���￨��/������
					anaeRecord.OperationApplication.PatientInfo.PID.PatientNO = Reader[2].ToString();//������(��'0000000001')
					anaeRecord.OperationApplication.PatientInfo.PID.CardNO = Reader[2].ToString();//���￨��(��'0000000001')
					//----------------------------------------------------------------------------------------------------------
					anaeRecord.OperationApplication.PatientInfo.Name = Reader[3].ToString();//����
					anaeRecord.OperationApplication.PatientInfo.Sex.ID = Reader[4].ToString();//�Ա�
					anaeRecord.OperationApplication.PatientSouce = Reader[5].ToString();//1����/2סԺ
					anaeRecord.OperationApplication.AnesType.ID = Reader[6].ToString();//����ʽ
					anaeRecord.AnaeDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[7].ToString());//����ʱ��
					//����ҽʦ���������ֵ���Ϣ�Ѿ�������thisAnaeRec.m_objOpsApp.RoleAl��
					//Reader[8] ����ҽʦ
					//Reader[9] ��������
					anaeRecord.AnaeResult.ID = Reader[10].ToString();//����Ч��
					try
					{
						anaeRecord.IsPACU = FS.FrameWork.Function.NConvert.ToBoolean(Reader[11].ToString());//�Ƿ���PACU,1�� 0�� 
					}
					catch{}
					anaeRecord.InPacuDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[12].ToString());//��(PACU)��ʱ��
					anaeRecord.InPacuStatus.ID = Reader[13].ToString();//��(PACU)��״̬
					anaeRecord.OutPacuDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[14].ToString());//��(PACU)��ʱ��
					anaeRecord.OutPacuStatus.ID = Reader[15].ToString();//��(PACU)��״̬
					anaeRecord.Memo = Reader[16].ToString();//��ע
					anaeRecord.IsDemulcent = FS.FrameWork.Function.NConvert.ToBoolean(Reader[17].ToString());//������ʹ��1��0��
					anaeRecord.DemulcentType.ID = Reader[18].ToString();//��ʹ��ʽ
					anaeRecord.DemulcentModel.ID = Reader[19].ToString();//����
					anaeRecord.DemulcentDays = FS.FrameWork.Function.NConvert.ToInt32(Reader[20].ToString());//��ʹ����
					anaeRecord.PullOutDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[21].ToString());//�ι�ʱ��
					anaeRecord.PullOutOperator.ID = Reader[22].ToString();//�ι���
					anaeRecord.DemulcentEffect.ID = Reader[23].ToString();//��ʹЧ��
					anaeRecord.IsCharged = FS.FrameWork.Function.NConvert.ToBoolean(Reader[24].ToString());//0δ����/1�Ѽ���
					anaeRecord.ExecDept.ID = Reader[25].ToString();//ִ�п���
                    //{C7BDDFBF-BD3A-43c7-8057-432EC8B59338}
                    anaeRecord.Direction = Reader[26].ToString();//����ȥ��
                    //{26E31402-7D3C-4798-B2BE-C34F06C4FCC7}
                    anaeRecord.DemuDrug = Reader[27].ToString(); //��ʹ��ҩ
				}
			}
			catch(Exception ex)
			{
				this.Err="�������Ǽǵ���Ϣ����"+ex.Message;
				this.ErrCode="-1";
				this.WriteErr();
				return null;
			}
			this.Reader.Close();	
			return anaeRecord;
		}
		/// <summary>
		/// ��ѯָ��ʱ����ڵ�����ǼǼ�¼�б�
		/// </summary>
		/// <param name="ExeDeptID">string ִ�п��Ҵ���</param>
		/// <param name="BeginDate">DateTime ��ʼʱ��</param>
		/// <param name="EndDate">DateTime ����ʱ��</param>
		/// <returns>����ǼǼ�¼�б�Ԫ��ΪFS.HISFC.Models.Operation.AnaeRecord���ͣ�</returns>
		public ArrayList GetAnaeRecords(string ExeDeptID,DateTime BeginDate,DateTime EndDate)
		{
			ArrayList AnaeRecordAl = new ArrayList();
			string strSql = string.Empty;
			string strWhere = string.Empty;
			if(this.Sql.GetSql("Operator.AnaeRecord.GetAnaeRecord.Select.1",ref strSql) == -1) 
			{
				return AnaeRecordAl;
			}

			if(this.Sql.GetSql("Operator.AnaeRecord.GetAnaeRecord.Where.1",ref strWhere) == -1) 
			{
				return AnaeRecordAl;
			}

			strWhere = string.Format(strWhere,ExeDeptID,BeginDate.ToString(),EndDate.ToString());
			strSql = strSql + " \n" + strWhere;
			//��ѯSQL����Ѿ���ã���ʼ��ѯ�������ע������
			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{
					FS.HISFC.Models.Operation.AnaeRecord thisAnaeRec = new FS.HISFC.Models.Operation.AnaeRecord();
					
					thisAnaeRec.OperationApplication.ID = Reader[0].ToString();					//�������
					//�Ȼ�ù������������뵥
					thisAnaeRec.OperationApplication = operationManager.GetOpsApp(thisAnaeRec.OperationApplication.ID);
					//����������뵥û��ʵ��ֵ���������ǲ��ǵ������¼����������Ĺ���thisOpsRec.m_objOpsApp�ĸ�ֵ����������ġ�

					thisAnaeRec.OperationApplication.PatientInfo.ID  = Reader[1].ToString();//סԺ��ˮ��/�����(��'ZY010000000001')
					//----------------------------------------------------------------------------------------------------------
					thisAnaeRec.OperationApplication.PatientInfo.PID.ID = Reader[2].ToString();//���￨��/������
					thisAnaeRec.OperationApplication.PatientInfo.PID.PatientNO = Reader[2].ToString();//������(��'0000000001')
					thisAnaeRec.OperationApplication.PatientInfo.PID.CardNO = Reader[2].ToString();//���￨��(��'0000000001')
					//----------------------------------------------------------------------------------------------------------
					thisAnaeRec.OperationApplication.PatientInfo.Name = Reader[3].ToString();//����
					thisAnaeRec.OperationApplication.PatientInfo.Sex.ID = Reader[4].ToString();//�Ա�
					thisAnaeRec.OperationApplication.PatientSouce = Reader[5].ToString();//1����/2סԺ
					thisAnaeRec.OperationApplication.AnesType.ID = Reader[6].ToString();//����ʽ
					thisAnaeRec.AnaeDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[7].ToString());//����ʱ��
					//����ҽʦ���������ֵ���Ϣ�Ѿ�������thisAnaeRec.m_objOpsApp.RoleAl��
					//Reader[8] ����ҽʦ
					//Reader[9] ��������
					thisAnaeRec.AnaeResult.ID = Reader[10].ToString();//����Ч��
					try
					{
						thisAnaeRec.IsPACU = FS.FrameWork.Function.NConvert.ToBoolean(Reader[11].ToString());//�Ƿ���PACU,1�� 0�� 
					}
					catch{}
					thisAnaeRec.InPacuDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[12].ToString());//��(PACU)��ʱ��
					thisAnaeRec.InPacuStatus.ID = Reader[13].ToString();//��(PACU)��״̬
					thisAnaeRec.OutPacuDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[14].ToString());//��(PACU)��ʱ��
					thisAnaeRec.OutPacuStatus.ID = Reader[15].ToString();//��(PACU)��״̬
					thisAnaeRec.Memo = Reader[16].ToString();//��ע
					thisAnaeRec.IsDemulcent = FS.FrameWork.Function.NConvert.ToBoolean(Reader[17].ToString());//������ʹ��1��0��
					thisAnaeRec.DemulcentType.ID = Reader[18].ToString();//��ʹ��ʽ
					thisAnaeRec.DemulcentModel.ID = Reader[19].ToString();//����
					thisAnaeRec.DemulcentDays = FS.FrameWork.Function.NConvert.ToInt32(Reader[20].ToString());//��ʹ����
					thisAnaeRec.PullOutDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[21].ToString());//�ι�ʱ��
					thisAnaeRec.PullOutOperator.ID = Reader[22].ToString();//�ι���
					thisAnaeRec.DemulcentEffect.ID = Reader[23].ToString();//��ʹЧ��
					thisAnaeRec.IsCharged = FS.FrameWork.Function.NConvert.ToBoolean(Reader[24].ToString());//0δ����/1�Ѽ���
					thisAnaeRec.ExecDept.ID = Reader[25].ToString();//ִ�п���
                    //{C7BDDFBF-BD3A-43c7-8057-432EC8B59338}
                    thisAnaeRec.Direction = Reader[26].ToString();//����ȥ��
                    //{26E31402-7D3C-4798-B2BE-C34F06C4FCC7}
                    thisAnaeRec.DemuDrug = Reader[27].ToString(); //��ʹ��ҩ
					AnaeRecordAl.Add(thisAnaeRec);
				}
			}
			catch(Exception ex)
			{
				this.Err="�������Ǽǵ���Ϣ����"+ex.Message;
				this.ErrCode="-1";
				this.WriteErr();
				AnaeRecordAl.Clear();
				return AnaeRecordAl;
			}
			this.Reader.Close();	
			return AnaeRecordAl;
		}
		#region ����Ǽǵ�����
		/// <summary>
		/// ��������Ǽ�
		/// </summary>
		/// <param name="AnaeRecord">����Ǽǵ�����</param>
		/// <returns>0 success -1 fail</returns>
		public int AddAnaeRecord(FS.HISFC.Models.Operation.AnaeRecord AnaeRecord)
		{
			string strSql = string.Empty;	
			#region ��ȡ���߻�����Ϣ
			//--------------------------------------------------------		
			//�ֲ���������
			string ls_ClinicCode = string.Empty;//סԺ��ˮ��/�����
			string ls_PatientNo = string.Empty; //������/������
			string ls_Name = string.Empty;	  //��������
			string ls_Sex = string.Empty;		  //�Ա�
			FS.HISFC.Models.Operation.OperationAppllication OpsApp;
			OpsApp = AnaeRecord.OperationApplication;
			
			ls_ClinicCode = OpsApp.PatientInfo.ID;
			ls_PatientNo = OpsApp.PatientInfo.PID.ID;
			ls_Name =  OpsApp.PatientInfo.Name;
			ls_Sex =  OpsApp.PatientInfo.Sex.ID.ToString();			
			//--------------------------------------------------------
			#endregion			
			//bool��־ֵת��
			string strIsPACU = FS.FrameWork.Function.NConvert.ToInt32(AnaeRecord.IsPACU).ToString();
			string strDemulcent = FS.FrameWork.Function.NConvert.ToInt32(AnaeRecord.IsDemulcent).ToString();
			string strChargeFlag = FS.FrameWork.Function.NConvert.ToInt32(AnaeRecord.IsCharged).ToString();
			if(this.Sql.GetSql("Operator.AnaeRecord.AddAnaeRecord.1",ref strSql)==-1) 
			{
				return -1;
			}

			try
			{				
				//�����ǼǱ������Ӽ�¼
				//ÿ��5������
				strSql = string.Format(strSql,OpsApp.ID,ls_ClinicCode,ls_PatientNo,ls_Name,ls_Sex,OpsApp.PatientSouce,
					OpsApp.AnesType.ID.ToString(),AnaeRecord.AnaeDate.ToString(),"","",AnaeRecord.AnaeResult.ID.ToString(),
					strIsPACU,AnaeRecord.InPacuDate.ToString(),AnaeRecord.InPacuStatus.ID.ToString(),AnaeRecord.OutPacuDate.ToString(),AnaeRecord.OutPacuStatus.ID.ToString(),
					AnaeRecord.Memo,strDemulcent,AnaeRecord.DemulcentType.ID.ToString(),AnaeRecord.DemulcentModel.ID.ToString(),AnaeRecord.DemulcentDays.ToString(),
					AnaeRecord.PullOutDate.ToString(),AnaeRecord.PullOutOperator.ID.ToString(),AnaeRecord.DemulcentEffect.ID.ToString(),strChargeFlag,this.Operator.ID.ToString(),
					AnaeRecord.ExecDept.ID.ToString(),
                    //{C7BDDFBF-BD3A-43c7-8057-432EC8B59338}
                    AnaeRecord.Direction,
                    //{26E31402-7D3C-4798-B2BE-C34F06C4FCC7}
                    AnaeRecord.DemuDrug);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return -1;            
			}
			if (strSql == null) return -1;	
			
			if(this.ExecNoQuery(strSql) == -1) return -1;
			return 0;
		}
		/// <summary>
		/// ��������Ǽ���Ϣ
		/// </summary>
		/// <param name="AnaeRecord">����Ǽ�ʵ�����</param>
		/// <returns>0 success -1 fail</returns>
		public int UpdateAnaeRecord(FS.HISFC.Models.Operation.AnaeRecord AnaeRecord)
		{
			string strSql = string.Empty;	
			#region ��ȡ���߻�����Ϣ
			//--------------------------------------------------------		
			//�ֲ���������
			string ls_ClinicCode = string.Empty;//סԺ��ˮ��/�����
			string ls_PatientNo = string.Empty; //������/������
			string ls_Name = string.Empty;	  //��������
			string ls_Sex = string.Empty;		  //�Ա�
			FS.HISFC.Models.Operation.OperationAppllication OpsApp = new FS.HISFC.Models.Operation.OperationAppllication();
			OpsApp = AnaeRecord.OperationApplication;
			
			ls_ClinicCode = OpsApp.PatientInfo.ID;
			ls_PatientNo = OpsApp.PatientInfo.PID.ID;
			ls_Name =  OpsApp.PatientInfo.Name;
			ls_Sex =  OpsApp.PatientInfo.Sex.ID.ToString();			
			//--------------------------------------------------------
			#endregion			
			//bool��־ֵת��
			string strIsPACU = FS.FrameWork.Function.NConvert.ToInt32(AnaeRecord.IsPACU).ToString();
			string strDemulcent = FS.FrameWork.Function.NConvert.ToInt32(AnaeRecord.IsDemulcent).ToString();
			string strChargeFlag = FS.FrameWork.Function.NConvert.ToInt32(AnaeRecord.IsCharged).ToString();
			if(this.Sql.GetSql("Operator.AnaeRecord.UpdateAnaeRecord.1",ref strSql)==-1) 
			{
				return -1;
			}

			try
			{				
				//�����ǼǱ������Ӽ�¼
				//ÿ��5������
				strSql = string.Format(strSql,OpsApp.ID,ls_ClinicCode,ls_PatientNo,ls_Name,ls_Sex,OpsApp.PatientSouce,
					OpsApp.AnesType.ID.ToString(),AnaeRecord.AnaeDate.ToString(),"","",AnaeRecord.AnaeResult.ID.ToString(),
					strIsPACU,AnaeRecord.InPacuDate.ToString(),AnaeRecord.InPacuStatus.ID.ToString(),AnaeRecord.OutPacuDate.ToString(),AnaeRecord.OutPacuStatus.ID.ToString(),
					AnaeRecord.Memo,strDemulcent,AnaeRecord.DemulcentType.ID.ToString(),AnaeRecord.DemulcentModel.ID.ToString(),AnaeRecord.DemulcentDays.ToString(),
					AnaeRecord.PullOutDate.ToString(),AnaeRecord.PullOutOperator.ID.ToString(),AnaeRecord.DemulcentEffect.ID.ToString(),strChargeFlag,this.Operator.ID.ToString(),
                    AnaeRecord.ExecDept.ID.ToString(), 
                    //{C7BDDFBF-BD3A-43c7-8057-432EC8B59338}
                    AnaeRecord.Direction,
                    //{26E31402-7D3C-4798-B2BE-C34F06C4FCC7}
                    AnaeRecord.DemuDrug);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return -1;            
			}
			if (strSql == null) return -1;	
			
			if(this.ExecNoQuery(strSql) == -1) return -1;
			return 0;
		}
		#endregion
		/// <summary>
		/// ��ȡ�Ƿ������޸������ǼǱ�־
		/// </summary>
		/// <returns>��־1�����޸� 0�����޸ģ���ΪError,��ϵͳ����δ����</returns>
		public string GetModifyEnabled()
		{
			string strSql = string.Empty;
			string strFlag = string.Empty;
			if(this.Sql.GetSql("Operator.OpsRecord.GetRecordModifyFlag.1",ref strSql) == -1) 
			{
				return strFlag;				
			}

			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{
					strFlag = this.Reader[0].ToString();
				}
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				this.WriteErr();
				return "Error";            
			}
			this.Reader.Close();		
			if(strFlag == "") 
			{
				this.Err = "ϵͳδά���Ƿ������޸�����ǼǼ�¼����������ϵϵͳ����Ա��";
				this.ErrCode = "ϵͳδά���Ƿ������޸�����ǼǼ�¼����������ϵϵͳ����Ա��";	
				this.WriteErr();
				return "Error";
			}
			return strFlag;
		}

        #region {5F37177C-DE87-4b3e-9041-07A786B55D81}

        /// <summary>
        /// ������Ǽ��շѱ�־
        /// </summary>
        /// <param name="operationNo"></param>
        /// <returns></returns>
        public int UpdateAnaeFee(string operationNo)
        {
            string sql = string.Empty;

            if (this.Sql.GetSql("Operator.AnaeRecord.UpdateAnaeRecordFee.1", ref sql) == -1)
            {
                return -1;
            }

            try
            {
                sql = string.Format(sql, operationNo);

                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "������ǼǼ�¼�շѱ�־����[Operator.AnaeRecord.UpdateAnaeRecordFee.1]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        #endregion

	}
}
