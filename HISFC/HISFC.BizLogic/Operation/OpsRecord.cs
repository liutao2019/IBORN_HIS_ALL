using System;
using System.Collections;
namespace FS.HISFC.BizLogic.Operation
{
	/// <summary>
	/// OpsRecord ��ժҪ˵����
	/// �����Ǽǹ�����
	/// 2005-01-07 Written by liling 
	/// </summary>
	public abstract class OpsRecord : FS.FrameWork.Management.Database
	{
		public OpsRecord()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		private FS.HISFC.BizLogic.Operation.OpsTableManage tableManager = new FS.HISFC.BizLogic.Operation.OpsTableManage();
		//�������뵥������ʵ��
        protected abstract FS.HISFC.BizLogic.Operation.Operation operationManager
        {
            get;
        }
        //private FS.HISFC.Integrate.RADT radtManager = new FS.HISFC.Integrate.RADT();
		/// <summary>
		/// �����Ǽ�ʵ�� add by huangxw
		/// </summary>
		private FS.HISFC.Models.Operation.OperationRecord record = null;
		private ArrayList al=null;
		
		#region ��ѯ�����Ǽǵ�
		/// <summary>
		/// ��ѯָ��������ŵ������ǼǼ�¼
		/// </summary>
		/// <param name="OperatorNo"></param>
		/// <returns>�����Ǽǵ�����</returns>
		public FS.HISFC.Models.Operation.OperationRecord GetOperatorRecord( string operatorNO )
		{
			if(operatorNO == string.Empty)
			{
				return null;
			}
			
			string strSql = string.Empty;
			string strWhere = string.Empty;
			if(this.Sql.GetSql("Operator.OpsRecord.GetOperatorRecord.Select.1",ref strSql) == -1) 
			{
				return null;
			}

			if(this.Sql.GetSql("Operator.OpsRecord.GetOperatorRecord.Where.2",ref strWhere) == -1) 
			{
				return null;
			}

			strWhere = string.Format(strWhere,operatorNO);
			strSql = strSql + " \n" + strWhere;

            FS.HISFC.Models.Operation.OperationRecord thisOpsRec = new FS.HISFC.Models.Operation.OperationRecord();
            ArrayList list = QueryMyOperatorRecord(strSql);
            if (list == null)
            {
                return null;
            }
            if (list.Count > 0)
            {
                thisOpsRec = (FS.HISFC.Models.Operation.OperationRecord)list[0];
            }
			return thisOpsRec;
		}
        private ArrayList QueryMyOperatorRecord(string strSql)
        {
            al = new ArrayList();
            if (this.ExecQuery(strSql) == -1) return al;
            try
            {
                while (this.Reader.Read())
                {
                    record = new FS.HISFC.Models.Operation.OperationRecord();
                    record.OperationAppllication.ID = Reader[0].ToString();					//�������
                    //�Ȼ�ù������������뵥
                    //record.m_objOpsApp = m_objOperator.GetOpsApp(record.m_objOpsApp.OperationNo);
                    //����������뵥û��ʵ��ֵ���������ǲ��ǵ�������¼����������Ĺ���record.m_objOpsApp�ĸ�ֵ����������ġ�

                    record.OperationAppllication.PatientInfo.ID = Reader[1].ToString();//סԺ��ˮ��/�����(��'ZY010000000001')
                    record.OperationAppllication.PatientSouce = Reader[3].ToString();//1����/2סԺ
                    if (record.OperationAppllication.PatientSouce == "2")
                    {
                        record.OperationAppllication.PatientInfo = this.GetPatientInfo(record.OperationAppllication.PatientInfo.ID);
                    }
                    else
                    {
                        FS.HISFC.Models.Registration.Register regObj = this.GetRegInfo(record.OperationAppllication.PatientInfo.ID);
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
                        record.OperationAppllication.PatientInfo = patientInfo;
                    }
                    //record.OperationAppllication.PatientInfo = this.GetPatientInfo(record.OperationAppllication.PatientInfo.ID);
                    //----------------------------------------------------------------------------------------------------------
                    record.OperationAppllication.PatientInfo.PID.ID = Reader[2].ToString();//���￨��/������
                    record.OperationAppllication.PatientInfo.PID.PatientNO = Reader[2].ToString();//������(��'0000000001')
                    record.OperationAppllication.PatientInfo.PID.CardNO = Reader[2].ToString();//���￨��(��'0000000001')
                    //----------------------------------------------------------------------------------------------------------
                    //record.OperationAppllication.PatientSouce = Reader[3].ToString();//1����/2סԺ
                    record.OperationAppllication.PatientInfo.Name = Reader[4].ToString();//����
                    record.OperationAppllication.PatientInfo.PVisit.PatientLocation.Dept.ID = Reader[5].ToString();//סԺ����
                    record.OperationAppllication.PatientInfo.PVisit.PatientLocation.Bed.ID = Reader[6].ToString();//������
                    record.OperationAppllication.PatientInfo.Sex.ID = Reader[7].ToString();//�Ա�
                    record.OperationAppllication.PatientInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[8].ToString());//����
                    record.OperationAppllication.PatientInfo.BloodType.ID = Reader[9].ToString();//Ѫ��
                    record.OperationAppllication.OperateKind = Reader[10].ToString();					//1��ͨ2����3��Ⱦ					

                    if (Reader.IsDBNull(11) == false)
                    {
                        record.OperationAppllication.OperationDoctor.ID = Reader[11].ToString();//����ҽ��
                    }

                    if (Reader.IsDBNull(12) == false)
                    {
                        record.OperationAppllication.GuideDoctor.ID = Reader[12].ToString();//ָ��ҽ��	
                    }

                    record.OperationAppllication.PreDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[13].ToString());		//����ԤԼʱ��					

                    if (Reader.IsDBNull(14) == false)
                        record.OperationAppllication.Duration = System.Convert.ToDecimal(Reader[14].ToString());		//����Ԥ����ʱ					

                    record.OperationAppllication.AnesType.ID = Reader[15].ToString();					//��������					
                    //Reader[16]������
                    //Reader[17]ϴ�ֻ�ʿ��
                    //Reader[18]��̨��ʿ��
                    //Reader[19]Ѳ�ػ�ʿ��
                    record.OperationAppllication.TableType = Reader[20].ToString();					//0��̨1��̨2��̨					

                    if (Reader.IsDBNull(21) == false)
                    {
                        record.OperationAppllication.ExeDept = this.GetDeptmentById(Reader[21].ToString());//ִ�п���					
                        if (record.OperationAppllication.ExeDept == null)
                            record.OperationAppllication.ExeDept = new FS.HISFC.Models.Base.Department();
                        record.OperationAppllication.OperateRoom =
                            (FS.HISFC.Models.Base.Department)record.OperationAppllication.ExeDept;//������(������Ҫ�����뵥��������˵�������Ҽ�ִ�п���)
                    }

                    if (Reader.IsDBNull(22) == false)
                    {
                        record.OperationAppllication.OpsTable.ID = Reader[22].ToString();				//����̨
                        record.OperationAppllication.OpsTable.Name =
                            this.tableManager.GetTableNameFromID(record.OperationAppllication.OpsTable.ID.ToString());
                    }

                    if (Reader.IsDBNull(23) == false)
                    {
                        record.OperationAppllication.ApplyDoctor.ID = Reader[23].ToString();	//����ҽ��

                    }

                    record.OperationAppllication.ApplyDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[24].ToString());	//����ʱ��					
                    if (Reader.IsDBNull(25) == false)
                    {
                        record.OperationAppllication.ApproveDoctor.ID = Reader[25].ToString();//����ҽ��
                    }

                    record.OperationAppllication.OperationType.ID = Reader[26].ToString();				//������ģ					
                    #region ʹ��{24517986-0535-44a3-A25F-F6FD4B362496}
                    try
                    {
                        string strGerm = Reader[27].ToString();						//1 �о� 0�޾�
                        record.OperationAppllication.IsGermCarrying = FS.FrameWork.Function.NConvert.ToBoolean(strGerm);
                    }
                    catch { }
                    #endregion

                    record.OperationAppllication.InciType.ID = Reader[28].ToString();					//�п�����
                    record.OperationAppllication.ScreenUp = Reader[29].ToString();					//1 Ļ�� 2 Ļ��
                    record.OpsDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[30].ToString());		//����ʱ��
                    record.AcceptDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[31].ToString());	//�ӻ���ʱ��
                    record.Memo = Reader[32].ToString();							//��ע
                    record.OperationAppllication.BloodType.ID = Reader[33].ToString();			//ѪҺ�ɷ֣�ȫѪ��Ѫ����Ѫ��ȣ�				
                    //					try
                    //					{
                    //						record.m_objOpsApp.BloodNum =  System.Convert.ToDecimal(Reader[34].ToString());	//Ѫ��
                    //					}
                    //					catch{}
                    record.OperationAppllication.BloodUnit = Reader[35].ToString();			//��Ѫ��λ
                    record.EnterDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[36].ToString());//��������ʱ��
                    record.OutDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[37].ToString());	//��������ʱ��					
                    if (Reader.IsDBNull(38) == false)
                    {
                        record.Duration = System.Convert.ToDecimal(Reader[38].ToString());		//����ʵ����ʱ
                    }
                    #region ��ʱ����
                    //					try
                    //					{
                    //						string strForeSober = Reader[39].ToString();						//��ǰ��ʶ��1����/0������       
                    //						record.bForeSober = FS.FrameWork.Function.NConvert.ToBoolean(strForeSober);
                    //					}
                    //					catch{}
                    //					try
                    //					{
                    //						string strStepSober = Reader[40].ToString();						//������ʶ��1����/0������       
                    //						record.bStepSober = FS.FrameWork.Function.NConvert.ToBoolean(strStepSober);
                    //					}
                    //					catch{}
                    //					record.ForePress = Reader[41].ToString();		//��ǰѪѹ
                    //					record.StepPress = Reader[42].ToString();		//����Ѫѹ					
                    //					try
                    //					{
                    //						record.ForePulse =  System.Convert.ToDecimal(Reader[43].ToString());		//��ǰ����
                    //					}
                    //					catch{}
                    //					try
                    //					{
                    //						record.StepPulse =  System.Convert.ToDecimal(Reader[44].ToString());		//��������
                    //					}
                    //					catch{}
                    //					record.ScarNum = FS.FrameWork.Function.NConvert.ToInt32(Reader[45].ToString());	//�촯����					
                    //					try
                    //					{
                    //						record.TransFusionQty =  System.Convert.ToDecimal(Reader[46].ToString());	//��Һ��
                    //					}
                    //					catch{}
                    //					record.SampleQty = FS.FrameWork.Function.NConvert.ToInt32(Reader[47].ToString());	//�걾����
                    //					record.GuidtubeNum = FS.FrameWork.Function.NConvert.ToInt32(Reader[48].ToString());	//�����ܸ���
                    //					record.BeforeReady.ID = Reader[49].ToString();	//��ǰ׼��					
                    //					try
                    //					{
                    //						string strToolExam = Reader[50].ToString();						//���ߺ˲�     
                    //						record.bToolExam = FS.FrameWork.Function.NConvert.ToBoolean(strToolExam);
                    //					}
                    //					catch{}
                    //					try
                    //					{
                    //						string strSeperate = Reader[51].ToString();						//�Ƿ���� 
                    //						record.bSeperate = FS.FrameWork.Function.NConvert.ToBoolean(strSeperate);
                    //					}
                    //					catch{}
                    //					try
                    //					{
                    //						string strDanger = Reader[52].ToString();						//�Ƿ�Σ��
                    //						record.bDanger = FS.FrameWork.Function.NConvert.ToBoolean(strDanger);
                    //					}
                    //					catch{}
                    //					record.LetBlood = FS.FrameWork.Function.NConvert.ToInt32(Reader[53].ToString());	//��Ѫ����
                    //					record.MainLine = FS.FrameWork.Function.NConvert.ToInt32(Reader[54].ToString());	//��ע����
                    //					record.MusleLine = FS.FrameWork.Function.NConvert.ToInt32(Reader[55].ToString());	//��ע����
                    //					record.TransFusion = FS.FrameWork.Function.NConvert.ToInt32(Reader[56].ToString());	//��Һ����
                    //					record.TransOxyen = FS.FrameWork.Function.NConvert.ToInt32(Reader[57].ToString());	//��������
                    //					record.Stale = FS.FrameWork.Function.NConvert.ToInt32(Reader[58].ToString());	//�������					
                    //					try
                    //					{
                    //						string strQues = Reader[59].ToString();						//�Ƿ���
                    //						record.bQuestion = FS.FrameWork.Function.NConvert.ToBoolean(strQues);
                    //					}
                    //					catch{}  
                    #endregion
                    string strI_Infec = Reader[60].ToString();						//I���пڸ�Ⱦ 1�� 2��
                    record.IsInfected = FS.FrameWork.Function.NConvert.ToBoolean(strI_Infec); 
                    if (Reader.IsDBNull(61) == false)
                    {
                        //�Ƿ�����
                        record.IsDead = FS.FrameWork.Function.NConvert.ToBoolean(Reader[61].ToString());
                    }

                    record.ExtraMemo = Reader[62].ToString();			//����˵��					

                    if (Reader.IsDBNull(63) == false)
                    {
                        //�Ƿ���Ч
                        record.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[63].ToString());
                    }

                    if (Reader.IsDBNull(64) == false)
                    {
                        //�Ƿ��շ�
                        record.IsCharged = FS.FrameWork.Function.NConvert.ToBoolean(Reader[64].ToString());
                    }

                    record.OperationAppllication.PatientInfo.Weight = Reader[65].ToString();//����	
                    record.OperationAppllication.RoomID = Reader[66].ToString();//����
                    record.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[67].ToString());
                    record.BloodPressureIn = Reader[68].ToString();//�������   add by huangxw	������ǰѪѹ����
                    record.OperationAppllication.OperationDoctor.Dept.ID = Reader[69].ToString();
                    
                    //{4F176F48-7AE6-4b93-9846-4F0EB7D3EDBF} �����Ǽ������Ƿ��������� 20100914
                    record.IsRescue = FS.FrameWork.Function.NConvert.ToBoolean(Reader[70]);
                    //{5DFF5830-8094-4ee0-A830-93731510284C}
                    record.IsOtherHosDoc = FS.FrameWork.Function.NConvert.ToBoolean(Reader[71]);
                    record.SpecialDevoice = Reader[72].ToString();
                    //{455F0D5D-89B7-4e06-974A-931534B52AC3}��������ʱ��
                    record.OpsEndDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[73].ToString());
                    al.Add(record);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Err = "��������Ǽǵ�������Ϣ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                al.Clear();
                return al;
            }

            try
            {
                foreach (FS.HISFC.Models.Operation.OperationRecord obj in al)
                {
                    obj.OperationAppllication.DiagnoseAl = this.operationManager.GetIcdFromApp(obj.OperationAppllication);	//����б�					
                    obj.OperationAppllication.OperationInfos = this.operationManager.GetOpsInfoFromApp(obj.OperationAppllication.ID);//������Ŀ��Ϣ�б�				
                    obj.OperationAppllication.RoleAl = this.operationManager.GetRoleFromApp(obj.OperationAppllication.ID);//��Ա��ɫ�б�
                    //�������Ը�ֵ��Ϊͻ�����ֲ����벿��ҵ����÷���
                    foreach (FS.HISFC.Models.Operation.ArrangeRole thisRole in obj.OperationAppllication.RoleAl)
                    {
                        if (thisRole.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper1.ToString()
                            || thisRole.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper2.ToString()
                            || thisRole.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper3.ToString())
                            //����ҽʦ�б�
                            obj.OperationAppllication.HelperAl.Add(thisRole.Clone());
                    }
                }
            }
            catch (Exception ex)
            {
                this.Err = "��������Ǽǵ�������Ϣ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                al.Clear();
                return al;
            }
            return al;
        }
		/// <summary>
		/// ��ѯָ��ʱ����ڵ������ǼǼ�¼�б�
		/// </summary>
		/// <param name="ExeDeptID">string ִ�п��Ҵ���</param>
		/// <param name="BeginDate">DateTime ��ʼʱ��</param>
		/// <param name="EndDate">DateTime ����ʱ��</param>
		/// <returns>�����ǼǼ�¼�б�Ԫ��ΪFS.HISFC.Models.Operation.OperatorRecord���ͣ�</returns>
		public ArrayList GetOperatorRecords(string ExeDeptID,DateTime BeginDate,DateTime EndDate)
		{
			al=new ArrayList();
			string strSql = string.Empty;
			string strWhere = string.Empty;

			if(this.Sql.GetSql("Operator.OpsRecord.GetOperatorRecord.Select.1",ref strSql) == -1) 
			{
				return al;
			}

			if(this.Sql.GetSql("Operator.OpsRecord.GetOperatorRecord.Where.1",ref strWhere) == -1) 
			{
				return al;
			}

			if(strSql == null || strWhere == null) 
			{
				return al;
			}

			strWhere = string.Format(strWhere,ExeDeptID,BeginDate.ToString(),EndDate.ToString());
			strSql = strSql + " \n" + strWhere;
            al = QueryMyOperatorRecord(strSql);
			return al;
		}
		#endregion

		#region �����Ǽǵ�����
		/// <summary>
		/// ���������Ǽ�
		/// </summary>
		/// <param name="OpsRecord">�����Ǽǵ�����</param>
		/// <returns>0 success -1 fail</returns>
		public int AddOperatorRecord(FS.HISFC.Models.Operation.OperationRecord OpsRecord)
		{
			#region �½������ǼǼ�¼
			///�½��ǼǼ�¼
			///Operation.Operation.CreateApplication.1
			///���룺67
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
			string ls_SickRoom = string.Empty;  //������
			FS.HISFC.Models.Operation.OperationAppllication OpsApp = new FS.HISFC.Models.Operation.OperationAppllication();
			OpsApp = OpsRecord.OperationAppllication;
			
			ls_ClinicCode = OpsApp.PatientInfo.ID;
			ls_PatientNo = OpsApp.PatientInfo.PID.ID;
			ls_Name =  OpsApp.PatientInfo.Name;
			ls_Sex =  OpsApp.PatientInfo.Sex.ID.ToString();
			ldt_Birthday =  OpsApp.PatientInfo.Birthday;
			ls_DeptCode =  OpsApp.PatientInfo.PVisit.PatientLocation.Dept.ID.ToString();
			ls_BedNo =  OpsApp.PatientInfo.PVisit.PatientLocation.Bed.ID.ToString();
			ls_BloodCode =  OpsApp.PatientInfo.BloodType.ID.ToString();
			ls_SickRoom =  OpsApp.PatientInfo.PVisit.PatientLocation.Room;
			//--------------------------------------------------------
			#endregion			
			//OpsApp.ExeDept =  OpsApp.OperateRoom;//ִ�п���(������Ҫ�����뵥��������˵�������Ҽ�ִ�п���)
			//bool��־ֵת��
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
			string strForeSober = FS.FrameWork.Function.NConvert.ToInt32(OpsRecord.IsSoberIn).ToString();
			string strStepSober = FS.FrameWork.Function.NConvert.ToInt32(OpsRecord.IsSoberOut).ToString();
			string strToolExam = FS.FrameWork.Function.NConvert.ToInt32(OpsRecord.bToolExam).ToString();
			string strSeperate = FS.FrameWork.Function.NConvert.ToInt32(OpsRecord.IsSeperated).ToString();
			if(OpsRecord.OperationAppllication.IsHeavy || OpsRecord.OperationAppllication.IsChange)
				OpsRecord.IsDangerous = true;
			else 
				OpsRecord.IsDangerous = false;
			string strDanger = FS.FrameWork.Function.NConvert.ToInt32(OpsRecord.IsDangerous).ToString();
			string strQues = FS.FrameWork.Function.NConvert.ToInt32(OpsRecord.IsMistaken).ToString();
			string strI_Infection = FS.FrameWork.Function.NConvert.ToInt32(OpsRecord.IsInfected).ToString();
			string strDie = FS.FrameWork.Function.NConvert.ToInt32(OpsRecord.IsDead).ToString();
			string strFee = FS.FrameWork.Function.NConvert.ToInt32(OpsRecord.IsCharged).ToString();
            //{5DFF5830-8094-4ee0-A830-93731510284C}
            string intIsOtherHosDoc = FS.FrameWork.Function.NConvert.ToInt32(OpsRecord.IsOtherHosDoc).ToString();
			if(OpsRecord.OperationAppllication.PatientInfo.Weight == "") 
				OpsRecord.OperationAppllication.PatientInfo.Weight = "0";
			string strOperDate = OpsRecord.OperDate.ToString();//Add By Maokb

	        string docDetp = OpsRecord.OperationAppllication.OperationDoctor.Dept.ID;//by zlw

			if(this.Sql.GetSql("Operator.OpsRecord.AddOperatorRecord.1",ref strSql)==-1) 
			{
				return -1;
			}

			try
			{				
				//�����ǼǱ������Ӽ�¼
				//ÿ��5������
				string []str = new string[]{
											   OpsApp.ID,//1
											   ls_ClinicCode,
											   ls_PatientNo,
											   OpsApp.PatientSouce,
											   ls_Name,
											   ls_DeptCode,
											   ls_BedNo,
											   ls_Sex,
											   ldt_Birthday.ToString(),
											   ls_BloodCode,//10
											   OpsApp.OperateKind,
											   OpsApp.OperationDoctor.ID.ToString(),
											   OpsApp.GuideDoctor.ID.ToString(),
											   OpsApp.PreDate.ToString(),
											   OpsApp.Duration.ToString(),
											   OpsApp.AnesType.ID.ToString(),
											   OpsApp.HelperAl.Count.ToString(),
                                               "0","0","0",//20
											   OpsApp.TableType,
											   OpsApp.ExeDept.ID.ToString(),
											   OpsApp.OpsTable.ID.ToString(),
											   OpsApp.ApplyDoctor.ID.ToString(),
											   OpsApp.ApplyDate.ToString(),//25
											   OpsApp.ApproveDoctor.ID.ToString(),
											   OpsApp.OperationType.ID.ToString(),
											   strGerm,OpsApp.InciType.ID.ToString(),
											   OpsApp.ScreenUp,
											   OpsRecord.OpsDate.ToString(),//30
											   OpsRecord.AcceptDate.ToString(),
											   OpsRecord.Memo,
											   OpsApp.BloodType.ID.ToString(),
											   OpsApp.BloodNum.ToString(),
											   OpsApp.BloodUnit,//35
											   OpsRecord.EnterDate.ToString(),
											   OpsRecord.OutDate.ToString(),
											   OpsRecord.Duration.ToString(),
											   strForeSober,
											   strStepSober,""/*OpsRecord.ForePress by huangxw*/,//41
											   OpsRecord.StepPress,
											   OpsRecord.ForePulse.ToString(),
											   OpsRecord.StepPulse.ToString(),
											   OpsRecord.BedsoreCount.ToString(),
											   OpsRecord.TransfusionQuantity.ToString(),
											   OpsRecord.SampleCount.ToString(),
											   OpsRecord.EduceFlowTubeCount.ToString(),
											   OpsRecord.BeforeReady.ID.ToString(),
											   strToolExam,strSeperate,strDanger,//50
											   OpsRecord.PhlebotmomizeTimes.ToString(),
											   OpsRecord.VeinInjectionTimes.ToString(),
											   OpsRecord.MuscleInjectionTimes.ToString(),
											   OpsRecord.TransfusionTimes.ToString(),
											   OpsRecord.TransoxygenTimes.ToString(),
											   OpsRecord.ExportUrineTimes.ToString(),
											   strQues,//57
											   strI_Infection,strDie,
											   OpsRecord.ExtraMemo,
                                               "1",strFee,//62
											   OpsRecord.OperationAppllication.PatientInfo.Weight,
											   this.Operator.ID.ToString(),
											   OpsRecord.OperationAppllication.RoomID,
											   strOperDate,
											   OpsRecord.BloodPressureIn,
											   docDetp,/*ҽ�����ڿ��� by zlw */
                                               OpsRecord.IsRescue ? "1" : "0" ,//{4F176F48-7AE6-4b93-9846-4F0EB7D3EDBF} �����Ǽ������Ƿ��������� 20100914
                                               intIsOtherHosDoc,//{5DFF5830-8094-4ee0-A830-93731510284C}�Ƿ���Ժר��
                                               OpsRecord.SpecialDevoice, //{5DFF5830-8094-4ee0-A830-93731510284C}�����豸����
                                               OpsRecord.OpsEndDate.ToString() //��������ʱ��2012-3-8
										   };
//				strSql = string.Format(strSql,OpsApp.OperationNo,ls_ClinicCode,ls_PatientNo,OpsApp.Pasource,ls_Name,
//					ls_DeptCode,ls_BedNo,ls_Sex,ldt_Birthday.ToString(),ls_BloodCode,
//					OpsApp.OperateKind,OpsApp.Ops_docd.ID.ToString(),OpsApp.Gui_docd.ID.ToString(),OpsApp.Pre_Date.ToString(),OpsApp.Duration.ToString(),
//					OpsApp.Anes_type.ID.ToString(),OpsApp.HelperAl.Count,0,0,0,
//					OpsApp.TableType,OpsApp.ExeDept.ID.ToString(),OpsApp.OpsTable.ID.ToString(),OpsApp.Apply_Doct.ID.ToString(),OpsApp.Apply_Date.ToString(),
//					OpsApp.ApprDocd.ID.ToString(),OpsApp.OperateType.ID.ToString(),strGerm,OpsApp.InciType.ID.ToString(),OpsApp.ScreenUp,
//					OpsRecord.OpsDate.ToString(),OpsRecord.ReceptDate.ToString(),OpsRecord.Remark,OpsApp.BloodType.ID.ToString(),OpsApp.BloodNum.ToString(),
//					OpsApp.BloodUnit,OpsRecord.EnterDate.ToString(),OpsRecord.OutDate.ToString(),OpsRecord.RealDuation.ToString(),strForeSober,
//					strStepSober,""/*OpsRecord.ForePress by huangxw*/,OpsRecord.StepPress,OpsRecord.ForePulse.ToString(),OpsRecord.StepPulse.ToString(),
//					OpsRecord.ScarNum.ToString(),OpsRecord.TransFusionQty.ToString(),OpsRecord.SampleQty.ToString(),OpsRecord.GuidtubeNum.ToString(),OpsRecord.BeforeReady.ID.ToString(),
//					strToolExam,strSeperate,strDanger,OpsRecord.LetBlood.ToString(),OpsRecord.MainLine.ToString(),
//					OpsRecord.MusleLine.ToString(),OpsRecord.TransFusion.ToString(),OpsRecord.TransOxyen.ToString(),OpsRecord.Stale.ToString(),strQues,
//					strI_Infection,strDie,OpsRecord.SpecialComment,"1",strFee,
//					OpsRecord.m_objOpsApp.PatientInfo.Weight,this.Operation.ID.ToString(),OpsRecord.m_objOpsApp.RoomID,strOperDate,OpsRecord.ForePress);
				if(this.ExecNoQuery(strSql,str) == -1) 
				{
					return -1;
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
		/// ���������Ǽ�
		/// </summary>
		/// <param name="OpsRecord">�����Ǽǵ�����</param>
		/// <returns>0 success -1 fail</returns>
		public int UpdateOperatorRecord(FS.HISFC.Models.Operation.OperationRecord OpsRecord)
		{				
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
			string ls_SickRoom = string.Empty;  //������
			FS.HISFC.Models.Operation.OperationAppllication OpsApp = new FS.HISFC.Models.Operation.OperationAppllication();
			OpsApp = OpsRecord.OperationAppllication;
			
			ls_ClinicCode = OpsApp.PatientInfo.ID;
			ls_PatientNo = OpsApp.PatientInfo.PID.ID;
			ls_Name =  OpsApp.PatientInfo.Name;
			ls_Sex =  OpsApp.PatientInfo.Sex.ID.ToString();
			ldt_Birthday =  OpsApp.PatientInfo.Birthday;
			ls_DeptCode =  OpsApp.PatientInfo.PVisit.PatientLocation.Dept.ID.ToString();
			ls_BedNo =  OpsApp.PatientInfo.PVisit.PatientLocation.Bed.ID.ToString();
			ls_BloodCode =  OpsApp.PatientInfo.BloodType.ID.ToString();
			ls_SickRoom =  OpsApp.PatientInfo.PVisit.PatientLocation.Room;
			//--------------------------------------------------------
			#endregion			
			//OpsApp.ExeDept =  OpsApp.OperateRoom;//ִ�п���(������Ҫ�����뵥��������˵�������Ҽ�ִ�п���)

			//bool��־ֵת��
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
			string strForeSober = FS.FrameWork.Function.NConvert.ToInt32(OpsRecord.IsSoberIn).ToString();
			string strStepSober = FS.FrameWork.Function.NConvert.ToInt32(OpsRecord.IsSoberOut).ToString();
			string strToolExam = FS.FrameWork.Function.NConvert.ToInt32(OpsRecord.bToolExam).ToString();
			string strSeperate = FS.FrameWork.Function.NConvert.ToInt32(OpsRecord.IsSeperated).ToString();
			if(OpsRecord.OperationAppllication.IsHeavy || OpsRecord.OperationAppllication.IsChange)
				OpsRecord.IsDangerous = true;
			else 
				OpsRecord.IsDangerous = false;
			string strDanger = FS.FrameWork.Function.NConvert.ToInt32(OpsRecord.IsDangerous).ToString();
			string strQues = FS.FrameWork.Function.NConvert.ToInt32(OpsRecord.IsMistaken).ToString();
			string strI_Infection = FS.FrameWork.Function.NConvert.ToInt32(OpsRecord.IsInfected).ToString();
			string strDie = FS.FrameWork.Function.NConvert.ToInt32(OpsRecord.IsDead).ToString();
			string strFee = FS.FrameWork.Function.NConvert.ToInt32(OpsRecord.IsCharged).ToString();
            //{5DFF5830-8094-4ee0-A830-93731510284C}
            string strIsOtherHosDoc = FS.FrameWork.Function.NConvert.ToInt32(OpsRecord.IsOtherHosDoc).ToString();
			if(OpsRecord.OperationAppllication.PatientInfo.Weight == "") 
				OpsRecord.OperationAppllication.PatientInfo.Weight = "0";
			//by zlw
	        string docDept = OpsRecord.OperationAppllication.OperationDoctor.Dept.ID;
			if(this.Sql.GetSql("Operator.OpsRecord.UpdateOperatorRecord.1",ref strSql)==-1) return -1;
			try
			{				
				//�����ǼǱ������Ӽ�¼
				//ÿ��5������
				string []str = new string[]{
											   OpsApp.ID,ls_ClinicCode,ls_PatientNo,OpsApp.PatientSouce,ls_Name,
											   ls_DeptCode,ls_BedNo,ls_Sex,ldt_Birthday.ToString(),ls_BloodCode,
											   OpsApp.OperateKind,OpsApp.OperationDoctor.ID.ToString(),OpsApp.GuideDoctor.ID.ToString(),OpsApp.PreDate.ToString(),OpsApp.Duration.ToString(),
											   OpsApp.AnesType.ID.ToString(),OpsApp.HelperAl.Count.ToString(),"0","0","0",
											   OpsApp.TableType,OpsApp.ExeDept.ID.ToString(),OpsApp.OpsTable.ID.ToString(),OpsApp.ApplyDoctor.ID.ToString(),OpsApp.ApplyDate.ToString(),
											   OpsApp.ApproveDoctor.ID.ToString(),OpsApp.OperationType.ID.ToString(),strGerm,OpsApp.InciType.ID.ToString(),OpsApp.ScreenUp,
											   OpsRecord.OpsDate.ToString(),OpsRecord.AcceptDate.ToString(),OpsRecord.Memo,OpsApp.BloodType.ID.ToString(),OpsApp.BloodNum.ToString(),
											   OpsApp.BloodUnit,OpsRecord.EnterDate.ToString(),OpsRecord.OutDate.ToString(),OpsRecord.Duration.ToString(),strForeSober,
											   strStepSober,""/*OpsRecord.ForePress add by huangxw*/,OpsRecord.StepPress,OpsRecord.ForePulse.ToString(),OpsRecord.StepPulse.ToString(),
											   OpsRecord.BedsoreCount.ToString(),OpsRecord.TransfusionQuantity.ToString(),OpsRecord.SampleCount.ToString(),OpsRecord.EduceFlowTubeCount.ToString(),OpsRecord.BeforeReady.ID.ToString(),
											   strToolExam,strSeperate,strDanger,OpsRecord.PhlebotmomizeTimes.ToString(),OpsRecord.VeinInjectionTimes.ToString(),
											   OpsRecord.MuscleInjectionTimes.ToString(),OpsRecord.TransfusionTimes.ToString(),OpsRecord.TransoxygenTimes.ToString(),OpsRecord.ExportUrineTimes.ToString(),strQues,
											   strI_Infection,strDie,OpsRecord.ExtraMemo,"1",strFee,
											   OpsRecord.OperationAppllication.PatientInfo.Weight,this.Operator.ID.ToString(),OpsRecord.OperationAppllication.RoomID,docDept,OpsRecord.BloodPressureIn, OpsRecord.IsRescue ? "1":"0", //{4F176F48-7AE6-4b93-9846-4F0EB7D3EDBF} �����Ǽ������Ƿ��������� 20100914
                                               strIsOtherHosDoc,//{5DFF5830-8094-4ee0-A830-93731510284C}��Ժר��
                                               OpsRecord.SpecialDevoice,//{5DFF5830-8094-4ee0-A830-93731510284C}�����豸����
                                               OpsRecord.OpsEndDate.ToString() //��������ʱ��2012-3-8 chengym
										   };
//				strSql = string.Format(strSql,OpsApp.OperationNo,ls_ClinicCode,ls_PatientNo,OpsApp.Pasource,ls_Name,
//					ls_DeptCode,ls_BedNo,ls_Sex,ldt_Birthday.ToString(),ls_BloodCode,
//					OpsApp.OperateKind,OpsApp.Ops_docd.ID.ToString(),OpsApp.Gui_docd.ID.ToString(),OpsApp.Pre_Date.ToString(),OpsApp.Duration.ToString(),
//					OpsApp.Anes_type.ID.ToString(),OpsApp.HelperAl.Count,0,0,0,
//					OpsApp.TableType,OpsApp.ExeDept.ID.ToString(),OpsApp.OpsTable.ID.ToString(),OpsApp.Apply_Doct.ID.ToString(),OpsApp.Apply_Date.ToString(),
//					OpsApp.ApprDocd.ID.ToString(),OpsApp.OperateType.ID.ToString(),strGerm,OpsApp.InciType.ID.ToString(),OpsApp.ScreenUp,
//					OpsRecord.OpsDate.ToString(),OpsRecord.ReceptDate.ToString(),OpsRecord.Remark,OpsApp.BloodType.ID.ToString(),OpsApp.BloodNum.ToString(),
//					OpsApp.BloodUnit,OpsRecord.EnterDate.ToString(),OpsRecord.OutDate.ToString(),OpsRecord.RealDuation.ToString(),strForeSober,
//					strStepSober,""/*OpsRecord.ForePress add by huangxw*/,OpsRecord.StepPress,OpsRecord.ForePulse.ToString(),OpsRecord.StepPulse.ToString(),
//					OpsRecord.ScarNum.ToString(),OpsRecord.TransFusionQty.ToString(),OpsRecord.SampleQty.ToString(),OpsRecord.GuidtubeNum.ToString(),OpsRecord.BeforeReady.ID.ToString(),
//					strToolExam,strSeperate,strDanger,OpsRecord.LetBlood.ToString(),OpsRecord.MainLine.ToString(),
//					OpsRecord.MusleLine.ToString(),OpsRecord.TransFusion.ToString(),OpsRecord.TransOxyen.ToString(),OpsRecord.Stale.ToString(),strQues,
//					strI_Infection,strDie,OpsRecord.SpecialComment,"1",strFee,
//					OpsRecord.m_objOpsApp.PatientInfo.Weight,this.Operation.ID.ToString(),OpsRecord.m_objOpsApp.RoomID);
				if(this.ExecNoQuery(strSql,str) == -1) return -1;
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return -1;            
			}
//			if (strSql == null) return -1;	
//			
//			if(this.ExecNoQuery(strSql) == -1) return -1;
			return 0;
		}
		/// <summary>
		/// ���������Ǽ���Ϣ
		/// </summary>
		/// <param name="operationNo"></param>
		/// <returns></returns>
		public int CancelRecord(string operationNo)
		{
			string sql=string.Empty;
			if(this.Sql.GetSql("Operator.OpsRecord.CancelOperatorRecord.1",ref sql)==-1)return -1;
            //{E526A9B6-48BC-4ffc-A1F8-069276E7E738}
			sql=string.Format(sql,operationNo,this.Operator.ID);
			return this.ExecNoQuery(sql);
		}

        /// <summary>
        /// ���������Ǽ���Ϣ��������뵥״̬(·־�� 2007-4-17)
        /// </summary>
        /// <param name="operationNo">�������к�</param>
        /// <returns></returns>
        public int CacelApply(string operationNo)
        {
            string sql = string.Empty;
            if (this.Sql.GetSql("Operator.OpsRecord.CancelOperatorRecord.2", ref sql) == -1) return -1;
            sql = string.Format(sql, operationNo);
            return this.ExecNoQuery(sql);
        }

		#endregion
		/// <summary>
		/// ��ȡ�Ƿ������޸������ǼǱ�־
		/// </summary>
		/// <returns>��־1�����޸� 0�����޸ģ���ΪError,��ϵͳ����δ����</returns>
		public string GetModifyEnabled()
		{
            FS.FrameWork.Management.ControlParam ctrlLogicInstance = new FS.FrameWork.Management.ControlParam();
            string ctrlValue = ctrlLogicInstance.QueryControlerInfo( "opreco" );

            if (ctrlValue == null)
            {
                return null;
            }

            if (ctrlValue == "") 
			{
				this.Err = "ϵͳδά���Ƿ������޸������ǼǼ�¼����������ϵϵͳ����Ա��";
				this.ErrCode = "ϵͳδά���Ƿ������޸������ǼǼ�¼����������ϵϵͳ����Ա��";	
				this.WriteErr();
				return "Error";
			}

            return ctrlValue;
		}

        protected abstract FS.HISFC.Models.RADT.PatientInfo GetPatientInfo(string id);
        protected abstract FS.HISFC.Models.Registration.Register GetRegInfo(string id);
        protected abstract FS.HISFC.Models.Base.Department GetDeptmentById(string id);

        ////{80D89813-7B64-4acf-A2CD-55BFD9F1E7C6}
        public int DeleteOpsRecord(string operationNO)
        {
            string strSql = string.Empty;

            int returnValue = this.Sql.GetSql("Operator.OpsRecord.DeleteOperatorRecord.1", ref strSql);

            if (returnValue < 0)
            {
                this.Err = "��ȡ[Operator.OpsRecord.DeleteOperatorRecord.1]��Ӧ��sql���ʧ��";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, operationNO);
            }
            catch (Exception ex)
            {

                this.Err = "��ʽ��sql���ʧ��!" + ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);

        }

        #region {5F37177C-DE87-4b3e-9041-07A786B55D81}

        /// <summary>
        /// ���������շ�
        /// </summary>
        /// <param name="operationNo"></param>
        /// <returns></returns>
        public int UpdateAnaeFee(string operationNo)
        {
            string sql = string.Empty;

            if (this.Sql.GetSql("Operator.OpsRecord.UpdateAnaeFee.1", ref sql) == -1)
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
                this.Err = "�������ǼǼ�¼�շѱ�־����[Operator.OpsRecord.UpdateAnaeFee.1]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// �������Ǽ��շѱ�־
        /// </summary>
        /// <param name="operationNo"></param>
        /// <returns></returns>
        public int UpdateOpsFee(string operationNo)
        {
            string sql = string.Empty;

            if (this.Sql.GetSql("Operator.OpsRecord.UpdateOpsRecordFee.1", ref sql) == -1)
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
                this.Err = "�������ǼǼ�¼�շѱ�־����[Operator.OpsRecord.UpdateOpsRecordFee.1]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        #endregion

	}
}
