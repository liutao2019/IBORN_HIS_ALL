using System;
using System.Collections;
using FS.FrameWork.Function;
using FS.HISFC.BizLogic.Manager;


namespace FS.HISFC.BizLogic.RADT 
{
	/// <summary>
	/// סԺ��λ�ձ�������
	/// writed by cuipeng
	/// 2005-3
	/// </summary>
	public class InpatientDayReport : FS.FrameWork.Management.Database 
	{
		public InpatientDayReport() 
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

	
		#region סԺ�ձ����ܱ�
		/// <summary>
		/// ȡȫԺĳһ���סԺ�ձ�����
		/// </summary>
		/// <param name="dateBegin">��ʼ����</param>
		/// <param name="dateEnd">��ֹ����</param>
		/// <returns>סԺ�ձ����飬������null</returns>
		public ArrayList GetInpatientDayReportList(DateTime dateBegin, DateTime dateEnd) 
		{
			string strSQL = "";
			//string strWhere = "";
			//ȡSELECT���
			if (this.Sql.GetCommonSql("Case.InpatientDayReport.GetInpatientDayReportList",ref strSQL) == -1) 
			{
				this.Err="û���ҵ�Case.InpatientDayReport.GetInpatientDayReportList�ֶ�!";
				return null;
			}

			//��ʽ��SQL���
			try 
			{
				strSQL = string.Format(strSQL, dateBegin.ToString(), dateEnd.ToString(),"AAAA");
			}
			catch (Exception ex) 
			{
				this.Err = "��ʽ��SQL���ʱ����Case.InpatientDayReport.GetInpatientDayReportList:" + ex.Message;
				return null;
			}

			//ȡסԺ�ձ�����
			return this.myGetInpatientDayReport(strSQL);
		}


		
		/// <summary>
		/// ȡȫԺĳһ���סԺ�ձ�����
		/// </summary>
		/// <param name="dateStat">�ձ�������</param>
		/// <returns>סԺ�ձ����飬������null</returns>
		public ArrayList GetInpatientDayReportList(DateTime dateStat) 
		{
            //return this.GetInpatientDayReportList(Convert.ToDateTime(dateStat.ToShortDateString() + " 00:00:00"), Convert.ToDateTime(dateStat.ToShortDateString() + " 23:59:59"));
            return this.GetInpatientDayReportList(Convert.ToDateTime(dateStat.ToShortDateString() + " 00:00:00"), Convert.ToDateTime(dateStat.AddDays(1).ToShortDateString() + " 00:00:00"));

		}


		/// <summary>
		/// ȡĳһ����ĳһ���סԺ�ձ�����
		/// </summary>
		/// <param name="dateStat">ͳ�ƴ���</param>
		/// <param name="deptCode">���ұ���</param>
		/// <param name="nurseStation">����վ����</param>
		/// <returns>סԺ�ձ�ʵ��</returns>
		public FS.HISFC.Models.HealthRecord.InpatientDayReport GetInpatientDayReport(DateTime dateStat, string deptCode, string nurseStation) 
		{
			string strSQL = "";
			//ȡSELECT���
			if (this.Sql.GetCommonSql("Case.InpatientDayReport.GetInpatientDayReport",ref strSQL) == -1) 
			{
				this.Err="û���ҵ�Case.InpatientDayReport.GetInpatientDayReport�ֶ�!";
				return null;
			}

			string strWhere= "";
			//ȡWhere���
			if (this.Sql.GetCommonSql("Case.InpatientDayReport.GetInpatientDayReport.ByDept",ref strWhere) == -1) 
			{
				this.Err="û���ҵ�Case.InpatientDayReport.GetInpatientDayReport.ByDept�ֶ�!";
				return null;
			}

			//��ʽ��SQL���
			try 
			{
				strSQL = string.Format(strSQL + strWhere, Convert.ToDateTime(dateStat.ToShortDateString() + " 00:00:00"), Convert.ToDateTime(dateStat.AddDays(1).ToShortDateString()+ " 00:00:00"), deptCode, nurseStation);
			}
			catch (Exception ex) 
			{
				this.Err = "��ʽ��SQL���ʱ����Case.InpatientDayReport.GetdayReportBill:" + ex.Message;
				return null;
			}

			//ִ��SQL��䣬ȡסԺ�ձ�������
			ArrayList al = this.myGetInpatientDayReport(strSQL);
			if (al == null) 
			{
				this.Err = "ȡסԺ�ձ�����ʱ����" + this.Err;
				return null;
			}

			//���û���ҵ����ݣ��򷵻��½��Ķ��󣬷��򷵻���������
			if (al.Count == 0) 
				return new FS.HISFC.Models.HealthRecord.InpatientDayReport();
			else
				return al[0] as FS.HISFC.Models.HealthRecord.InpatientDayReport;
		}


		/// <summary>
		/// ��סԺ�ձ����ܱ��в���һ����¼
		/// </summary>
		/// <param name="dayReport">סԺ�ձ�ʵ��</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int InsertInpatientDayReport(FS.HISFC.Models.HealthRecord.InpatientDayReport dayReport) 
		{
			string strSQL="";
			if(this.Sql.GetCommonSql("Case.InpatientDayReport.InsertInpatientDayReport",ref strSQL) == -1) 
			{
				this.Err="û���ҵ�Case.InpatientDayReport.InsertInpatientDayReport�ֶ�!";
				return -1;
			}
			try 
			{  
				string[] strParm = myGetParmInpatientDayReport( dayReport );     //ȡ�����б�
				strSQL=string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
			}
			catch(Exception ex) 
			{
				this.Err = "��ʽ��SQL���ʱ����Case.InpatientDayReport.InsertInpatientDayReport:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		
		
		/// <summary>
		/// ����סԺ�ձ����ܱ���һ����¼
		/// </summary>
		/// <param name="dayReport">סԺ�ձ�ʵ��</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int UpdateInpatientDayReport(FS.HISFC.Models.HealthRecord.InpatientDayReport dayReport) 
		{
			string strSQL="";
			if(this.Sql.GetCommonSql("Case.InpatientDayReport.UpdateInpatientDayReport",ref strSQL) == -1) 
			{
				this.Err="û���ҵ�Case.InpatientDayReport.UpdateInpatientDayReport�ֶ�!";
				return -1;
			}
			try 
			{  
				string[] strParm = myGetParmInpatientDayReport( dayReport );     //ȡ�����б�
				strSQL=string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
			}
			catch(Exception ex) 
			{
				this.Err = "��ʽ��SQL���ʱ����Case.InpatientDayReport.UpdateInpatientDayReport:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		
		
		/// <summary>
		/// ɾ��סԺ�ձ����ܱ���һ����¼
		/// </summary>
		/// <param name="dayReportID">����¼��</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int DeleteInpatientDayReport(string dayReportID) 
		{
			string strSQL="";
			if(this.Sql.GetCommonSql("Case.InpatientDayReport.DeleteInpatientDayReport",ref strSQL) == -1) 
			{
				this.Err="û���ҵ�Case.InpatientDayReport.DeleteInpatientDayReport�ֶ�!";
				return -1;
			}
			try 
			{  
				//����������ӵ�סԺ�ձ�������ֱ�ӷ���
				strSQL=string.Format(strSQL, dayReportID);            //�滻SQL����еĲ�����
			}
			catch(Exception ex) 
			{
				this.Err = "��ʽ��SQL���ʱ����Case.InpatientDayReport.DeleteInpatientDayReport:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		

		/// <summary>
		/// �ȸ���סԺ�ձ������û���ҵ����������һ��������
		/// </summary>
		/// <returns></returns>
		public int SetInpatientDayReport(FS.HISFC.Models.HealthRecord.InpatientDayReport dayReport) 
		{
			int parm;
			//�ȸ���סԺ�ձ�
			parm = this.UpdateInpatientDayReport(dayReport);
			if (parm == 0) 
			{
				//���û���ҵ����������һ��������
				parm = this.InsertInpatientDayReport(dayReport);
			}
			return parm;
		}
		
		FS.FrameWork.Management.ExtendParam managerExtendParam = new FS.FrameWork.Management.ExtendParam();

		/// <summary>
		/// �ձ���̬���£���ÿ�η�����λ�䶯��ʱ�����
		/// �ȸ���סԺ�ձ������û���ҵ��������������ձ��е���ĩ���͹̶���λ��������һ��������
		/// </summary>
		/// <returns></returns>
		public int DynamicUpdate(FS.HISFC.Models.HealthRecord.InpatientDayReport dayReport) 
		{
			try 
			{
				int parm;
				//����סԺ�ձ�
				parm = this.UpdateInpatientDayReport(dayReport);
				//���û���ҵ��������ݣ�˵���Ǳ��յ�һ�θ��£���ȡ�����ձ�����ĩ����Ϊ�����ڳ����͹̶���λ�������ݲ������ݿ�
				if(parm == 0) 
				{		
					//ȡ����סԺ�ձ�����ĩ��
					FS.HISFC.Models.HealthRecord.InpatientDayReport lastReport = this.GetInpatientDayReport(dayReport.DateStat.AddDays(-1), dayReport.ID, dayReport.NurseStation.ID);
					if(lastReport == null) return -1;
					
					//���û���ҵ��������������Ϊ������ĩ��Ϊ0
					if (lastReport.ID == "")  lastReport.EndNum = GetDeptPatientNum(dayReport.ID, dayReport.NurseStation.ID);

					//�����ڳ�����������ĩ��
					dayReport.BeginningNum = lastReport.EndNum;
					//������ĩ����������ĩ�� �� ������ĩ���������ڳ�����
					dayReport.EndNum = dayReport.EndNum + lastReport.EndNum;
				
					//ȡ�̶���λ��
					FS.HISFC.Models.Base.ExtendInfo deptExt = managerExtendParam.GetComExtInfo(FS.HISFC.Models.Base.EnumExtendClass.DEPT,"CASE_BED_STAND",dayReport.ID);
					if(deptExt == null) return -1;
					dayReport.BedStand = Convert.ToInt32(deptExt.NumberProperty);

					//����סԺ�ձ�����һ���¼�¼
					parm = this.InsertDayReportDetail(dayReport);
				}

				
				if (parm == -1) return parm;
				 
				//�����ձ���ϸ���뺯��
				return this.SetDayReportDetail(dayReport);
				//return parm;
			}
			catch(Exception ex) 
			{
				this.Err = ex.Message;
				return -1;
			}
		}


		/// <summary>
		/// �ձ���̬���£���ÿ�η�����λ�䶯��ʱ�����
		/// �ȸ���סԺ�ձ������û���ҵ��������������ձ��е���ĩ���͹̶���λ��������һ��������
		/// </summary>
		/// <param name="patientInfo">����ʵ��</param>
		/// <param name="type">��λ�䶯����</param>
		/// <returns></returns>
		public int DynamicUpdate(FS.HISFC.Models.RADT.PatientInfo patientInfo, string type) 
		{
			
			DateTime sysDate = Convert.ToDateTime(this.GetSysDate() + " 00:00:00");

			//���¶�̬�ձ�
			//ȡ����סԺ�ձ����ݣ���Ϊ���µ�ʵ��
			FS.HISFC.Models.HealthRecord.InpatientDayReport dayReport = this.GetInpatientDayReport(sysDate, patientInfo.PVisit.PatientLocation.Dept.ID, patientInfo.PVisit.PatientLocation.NurseCell.ID);
			if(dayReport == null) return -1;

			
			//���ձ�ʵ�帳ֵ
			//��̬�ձ�������Ҫ��������,������ˮ��,����
			dayReport.User01 = type;
			dayReport.User02 = patientInfo.ID;
			dayReport.User03 = patientInfo.PVisit.PatientLocation.Bed.ID;
			
			//���û���ҵ����յ��ձ���¼��dayReport��û�п�������
			//�˴�ID���ܸ�ֵ����Ϊ��������ô�ID�ж��Ƿ���Ҫ�����¼�¼��dayReport.ID = patientInfo.PVisit.PatientLocation.Dept.ID;  //�ձ����ұ���
			dayReport.NurseStation.ID = patientInfo.PVisit.PatientLocation.NurseCell.ID; //�ձ���������
			dayReport.DateStat = sysDate; //�ձ�����
            switch (type.ToString())
            {
                case "K":
                    //���䴦��������Ժ��1����ĩ������1
                    //if (Type.ToString()=="K") 
                    {
                        dayReport.InNormal = dayReport.InNormal + 1;
                        dayReport.EndNum = dayReport.EndNum + 1;
                    }
                    break;

                case "RI":
                case "RO":
                    //ת�봦��RI������ת���1����ĩ������1
                    //ת������RO��ת�����Ƽ�1����ĩ������1
                    {
                        //ȡԭ���ҡ��ֿ�����Ϣ
                        FS.HISFC.BizLogic.Manager.Department department = new FS.HISFC.BizLogic.Manager.Department();
                        department.SetTrans(this.Trans);

                        //ȡԭ������Ϣ
                        FS.HISFC.Models.Base.Department oldDept = department.GetDeptmentById(patientInfo.PVisit.PatientLocation.Dept.ID);
                        if (oldDept == null)
                        {
                            this.Err = department.Err;
                            return -1;
                        }

                        //ȡ�¿�����Ϣ
                        FS.HISFC.Models.Base.Department newDept = department.GetDeptmentById(patientInfo.PVisit.PatientLocation.Dept.User03);
                        if (newDept == null)
                        {
                            this.Err = department.Err;
                            return -1;
                        }

                        //ת�봦������ת���1����ĩ������1
                        if (type.ToString() == "RI")
                        {

                            if (oldDept.SpecialFlag == "C" && newDept.SpecialFlag == "C")
                            {
                                //������������Ϊ���ƣ���ɽ���������ڲ�ת��
                                //�ڲ�ת������1
                                dayReport.InTransferInner = dayReport.InTransferInner + 1;
                            }
                            else
                            {
                                //����ת������1
                                dayReport.InTransfer = dayReport.InTransfer + 1;
                            }
                            //��ĩ������1
                            dayReport.EndNum = dayReport.EndNum + 1;

                            //[2011-5-23]by zhaozf �޸Ĵ�λ�ձ�
                            dayReport.RelationDeptID = patientInfo.PVisit.PatientLocation.Dept.User03;
                            dayReport.RelationNurseCellID = patientInfo.PVisit.PatientLocation.NurseCell.User03;
                        }
                        else
                        {
                            //ת������ת����1����ĩ������1
                            if (oldDept.SpecialFlag == "C" && newDept.SpecialFlag == "C")
                            {
                                //������������Ϊ���ƣ���ɽ���������ڲ�ת��
                                //�ڲ�ת������1
                                dayReport.OutTransferInner = dayReport.OutTransferInner + 1;
                            }
                            else
                            {
                                //ת����������1
                                dayReport.OutTransfer = dayReport.OutTransfer + 1;
                            }
                            dayReport.EndNum = dayReport.EndNum - 1;

                            //[2011-5-23]by zhaozf �޸Ĵ�λ�ձ�
                            dayReport.RelationDeptID = newDept.ID;
                            dayReport.RelationNurseCellID = patientInfo.PVisit.PatientLocation.NurseCell.User03;
                        }
                    }
                    break;
                case "C":
                    //סԺ�ٻأ�סԺ�ٻؼ�1����ĩ������1
                    {
                        if (patientInfo.PVisit.OutTime.Date >= sysDate.Date)
                        {
                            //dayReport.DateStat = Convert.ToDateTime(patientInfo.PVisit.OutTime.Date + " 00:00:00");			
                            dayReport.DateStat = patientInfo.PVisit.OutTime.Date;

                            //���Ҵ˻����Ƿ��д��ڵ��ڽ��յĳ�Ժ�ǼǼ�¼�������������������Ժ�Ǽ��ձ���ϸ��¼
                            #region {8997C648-0AE4-42f4-943A-4E34EC127B39}
                            int iMyReturn = this.CancelDayReportDetail(dayReport);
                            if (iMyReturn == 1)
                            #endregion
                            {
                                //���û��߳�Ժ���ڵ��ڽ��գ��򽫳�Ժ������1����ϸ�Ѿ����ϡ����ٲ�����ϸ��¼
                                if (patientInfo.PVisit.OutTime.Date == sysDate.Date)
                                {
                                    dayReport.OutNormal = dayReport.OutNormal - 1;
                                    //��NurseStation.User03 �� ��1������ʾ����Ҫ�����ձ���ϸ��¼
                                    dayReport.NurseStation.User03 = "N";
                                }
                                else
                                {
                                    //������߳�Ժ���ڴ��ڽ��죬�򲻴����ձ����ܱ���ϸ�Ѿ�����
                                    return 1;
                                }
                            }
                            else
                            {
                                //���û�п������ϵĳ�Ժ��¼��˵���ٻص�����ǰ�����ݣ����ٻ�������1
                                dayReport.InReturn = dayReport.InReturn + 1;
                            }
                        }

                        dayReport.EndNum = dayReport.EndNum + 1;
                        switch (patientInfo.PVisit.ZG.ID)
                        {
                            case "1":
                                dayReport.OutCure = dayReport.OutCure - 1;
                                break;
                            case "2":
                                dayReport.OutBetter = dayReport.OutBetter - 1;
                                break;
                            case "3":
                                dayReport.OutUnCure = dayReport.OutUnCure - 1;
                                break;
                            case "4":
                                dayReport.OutDeath = dayReport.OutDeath - 1;
                                break;
                            case "5":
                                dayReport.OutOther = dayReport.OutOther - 1;
                                break;                          
                        }
                    }
                    break;
                case "O":
                    {
                        //������߳�Ժ���ڴ��ڽ��죬�ձ�ͳ�����ڵ��ڻ��߳�Ժ���ڡ��������ձ����ܱ�ֻ����ϸ��
                        if (patientInfo.PVisit.OutTime >= sysDate.AddDays(1))
                        {
                            #region {BDB95DB2-E42B-4ee5-82F1-6DB5CDA072FA}
                            //dayReport.DateStat = Convert.ToDateTime(patientInfo.PVisit.OutTime + " 00:00:00");
                            dayReport.DateStat = patientInfo.PVisit.OutTime.Date;
                            #endregion
                            dayReport.ID = patientInfo.PVisit.PatientLocation.Dept.ID;  //�ձ����ұ���
                            //�����ձ���ϸ���뺯��
                            return this.SetDayReportDetail(dayReport);
                        }

                        //�����Ժ���ڴ��ڽ��գ��򲻸����ձ���ֻ����һ���ձ���ϸ��¼��ÿ���̨�ձ���������У�������ĳ�Ժ������1
                        //��Ժ�Ǽǣ������Ժ��1����ĩ������1
                        dayReport.OutNormal = dayReport.OutNormal + 1;
                        dayReport.EndNum = dayReport.EndNum - 1; //�˴��п�����-1

                        switch (patientInfo.PVisit.ZG.ID)
                        {
                            case "1":
                                dayReport.OutCure = dayReport.OutCure + 1;
                                break;
                            case "2":
                                dayReport.OutBetter = dayReport.OutBetter + 1;
                                break;
                            case "3":
                                dayReport.OutUnCure = dayReport.OutUnCure + 1;
                                break;
                            case "4":
                                dayReport.OutDeath = dayReport.OutDeath + 1;
                                break;
                            case "5":
                                dayReport.OutOther = dayReport.OutOther + 1;
                                break;
                        }

                    }
                    break;

                case "OF":
                    //�޷���Ժ���޷���Ժ��1����ĩ������1
                    //if (Type.ToString()=="OF") 
                    {
                        if (patientInfo.PVisit.PatientLocation.Bed.ID == "")
                        {
                            return 1; //�������û�з��䴲λ����дסԺ��־
                        }

                        //houwb 2012-3-8
                        //�����޷���Ժ ��Ժ����-1
                        //�����޷���Ժ ��Ժ����+1
                        if (patientInfo.PVisit.InTime.Date == this.GetDateTimeFromSysDateTime().Date)
                        {
                            dayReport.InNormal = dayReport.InNormal - 1;
                            dayReport.EndNum = dayReport.EndNum - 1;
                        }
                        else
                        {
                            dayReport.OutWithdrawal = dayReport.OutWithdrawal + 1;
                            dayReport.EndNum = dayReport.EndNum - 1; //�˴��п�����-1
                        }
                    }
                    break;

                //[2011-5-23]by zhaozf �޸Ĵ�λ�ձ�
                case "CNO":
                    {
                        dayReport.RelationDeptID = patientInfo.PVisit.PatientLocation.NurseCell.User03;
                        dayReport.RelationNurseCellID = patientInfo.PVisit.PatientLocation.Dept.User03;
                    }
                    break;
                case "CN":
                    {
                        dayReport.RelationDeptID = patientInfo.PVisit.PatientLocation.NurseCell.User03;
                        dayReport.RelationNurseCellID = patientInfo.PVisit.PatientLocation.Dept.User03;
                    }
                    break;
            }


			//��λ�ձ����������»��߲���
			//���û���ҵ��������ݣ�˵���Ǳ��յ�һ�θ��£�����ձ�ʵ�帳��ʼֵ��������һ����¼
			if(dayReport.ID == "") 
			{					
				//���ÿ���賿����̨�洢�����Զ�������һ����ձ����ݣ��Ͳ��������δ���

                //�����ұ��븳ֵ
                #region {275A3935-042C-4e62-8717-3B21ADB77D6E}
                dayReport.ID = patientInfo.PVisit.PatientLocation.Dept.ID;  //�ձ����ұ���
                //dayReport.ID = patientInfo.PVisit.PatientLocation.NurseCell.ID;  //�ձ����ұ���
                #endregion
                dayReport.NurseStation.ID = patientInfo.PVisit.PatientLocation.NurseCell.ID;

				//ȡ�̶���λ��
                //FS.HISFC.Models.Base.DepartmentExt deptExt = this.GetDepartmentExt("CASE_BED_STAND",dayReport.ID);
                //if(deptExt == null) return -1;
                //dayReport.BedStand = Convert.ToInt32(deptExt.NumberProperty);
                //------------------
                FS.FrameWork.Management.ExtendParam deptExtThing = new FS.FrameWork.Management.ExtendParam();
                deptExtThing.SetTrans(this.Trans);
                decimal decBedNumber = deptExtThing.GetComExtInfoNumber(FS.HISFC.Models.Base.EnumExtendClass.DEPT, "CASE_BED_STAND", dayReport.ID);
                dayReport.BedStand = Convert.ToInt32(decBedNumber);
                //------------------

				//ȡ����סԺ�ձ�����ĩ��
				FS.HISFC.Models.HealthRecord.InpatientDayReport lastReport = this.GetInpatientDayReport(dayReport.DateStat.AddDays(-1), dayReport.ID, dayReport.NurseStation.ID);
				if(lastReport == null) return -1;
					
				//���û���ҵ������������ȡ��ʱ���µĻ�����Ժ����Ϊ��ʼ����ĩ��
				//����û�г�ʼ�ڳ�����ֻ�ܸ��ݵ�ǰ�Ļ�������Ϊ��ĩ����Ȼ����㱾���ڳ�����
				if (lastReport.ID == "")  
				{
					//ȡ������Ժ������Ϊ��ʱ��ĩ��
					dayReport.EndNum = GetDeptPatientNum(dayReport.ID, dayReport.NurseStation.ID);
					//��������ڳ���
					this.ComputeBeginingNum(ref dayReport);
				}
				else 
				{
					//�����ڳ�����������ĩ��
					dayReport.BeginningNum = lastReport.EndNum;
					//���������ĩ��
					this.ComputeEndNum(ref dayReport);
				}
		

				//����סԺ�ձ�����һ���¼�¼
				if (this.InsertInpatientDayReport(dayReport) == -1) return -1;

			}
			else  
			{
				//����סԺ�ձ�
				if (this.UpdateInpatientDayReport(dayReport) != 1) return -1;				
			}
				 
			//���NurseStation.User03 == "N"����ʾ����Ҫ������ϸ����
			if (dayReport.NurseStation.User03 == "N") 
			{
				return 1;
			}

			//�����ձ���ϸ���뺯��
			return this.SetDayReportDetail(dayReport);
			return 1;
		}


		/// <summary>
		/// �����ձ�ʵ���еĸ��������ݼ�����ĩ����
		/// </summary>
		/// <param name="dayReport"></param>
		/// <returns></returns>
		public void ComputeEndNum(ref FS.HISFC.Models.HealthRecord.InpatientDayReport dayReport) 
		{
			if (dayReport == null) return;
			dayReport.EndNum = dayReport.BeginningNum  
				+ dayReport.InNormal  
				+ dayReport.InEmergency 
				+ dayReport.InReturn
				+ dayReport.InTransfer 
				- dayReport.OutNormal
				- dayReport.OutTransfer
				- dayReport.OutWithdrawal;
		}


		/// <summary>
		/// �����ձ�ʵ���еĸ��������ݼ����ڳ�����
		/// </summary>
		/// <param name="dayReport"></param>
		/// <returns></returns>
		public void ComputeBeginingNum(ref FS.HISFC.Models.HealthRecord.InpatientDayReport dayReport) 
		{
			if (dayReport == null) return;
			dayReport.BeginningNum = dayReport.EndNum  
				- dayReport.InNormal  
				- dayReport.InEmergency 
				- dayReport.InReturn
				- dayReport.InTransfer 
				+ dayReport.OutNormal
				+ dayReport.OutTransfer
				+ dayReport.OutWithdrawal;
		}


		/// <summary>
		/// ����ձ��еĸ�����ֵ(�����ڳ���,��ĩ�������ڳ���)
		/// </summary>
		/// <param name="dayReport"></param>
		public void Clear(ref FS.HISFC.Models.HealthRecord.InpatientDayReport dayReport)  
		{
			if (dayReport == null) return;
			
			dayReport.InNormal  = 0;
			dayReport.InEmergency = 0;
			dayReport.InReturn= 0;
			dayReport.InTransfer = 0;
			dayReport.OutNormal= 0;
			dayReport.OutTransfer= 0;
			dayReport.OutWithdrawal= 0;
			dayReport.EndNum = dayReport.BeginningNum;
		}


		/// <summary>
		/// ȡסԺ�ձ���Ϣ�б�������һ�����߶�������¼
		/// ˽�з����������������е���
		/// writed by cuipeng
		/// 2005-1
		/// </summary>
		/// <param name="SQLString">SQL���</param>
		/// <returns>סԺ�ձ���Ϣ��������</returns>
		private ArrayList myGetInpatientDayReport(string SQLString) 
		{
			ArrayList al=new ArrayList();                
			FS.HISFC.Models.HealthRecord.InpatientDayReport dayReport; //סԺ�ձ���Ϣʵ��
			this.ProgressBarText="���ڼ���סԺ�ձ�����Ϣ...";
			this.ProgressBarValue=0;
			
			//ִ�в�ѯ���
			if (this.ExecQuery(SQLString)==-1) 
			{
				this.Err="��ÿ����Ϣʱ��ִ��SQL������"+this.Err;
				this.ErrCode="-1";
				return null;
			}
			try 
			{
				while (this.Reader.Read()) 
				{
					//ȡ��ѯ����еļ�¼
					dayReport = new FS.HISFC.Models.HealthRecord.InpatientDayReport();
					dayReport.DateStat =    NConvert.ToDateTime(this.Reader[0].ToString()); //0 ͳ������
					dayReport.ID =          this.Reader[1].ToString();                      //1 סԺ�ձ�����
					dayReport.BedStand =    NConvert.ToInt32(this.Reader[2].ToString());    //2 �����ڲ�����
					dayReport.BedAdd =      NConvert.ToInt32(this.Reader[3].ToString());    //3 �Ӵ��� 
					dayReport.BedFree =     NConvert.ToInt32(this.Reader[4].ToString());    //4 �մ���
					dayReport.BeginningNum= NConvert.ToInt32(this.Reader[5].ToString());    //5 �ڳ�������
					dayReport.InNormal =    NConvert.ToInt32(this.Reader[6].ToString());    //6 ������Ժ��
					dayReport.InEmergency = NConvert.ToInt32(this.Reader[7].ToString());    //7 ������Ժ��
					dayReport.InTransfer =  NConvert.ToInt32(this.Reader[8].ToString());    //8 ������ת����
					dayReport.InReturn =    NConvert.ToInt32(this.Reader[9].ToString());    //9  �л���Ժ����
					dayReport.OutNormal =   NConvert.ToInt32(this.Reader[10].ToString());   //10 �����Ժ��
					dayReport.OutTransfer = NConvert.ToInt32(this.Reader[11].ToString());   //11 ת����������
					dayReport.OutWithdrawal=NConvert.ToInt32(this.Reader[12].ToString());   //12 ��Ժ����
					dayReport.EndNum =      NConvert.ToInt32(this.Reader[13].ToString());   //13 ��ĩ������
					dayReport.DeadIn24 =    NConvert.ToInt32(this.Reader[14].ToString());   //14 24Сʱ��������
					dayReport.DeadOut24 =   NConvert.ToInt32(this.Reader[15].ToString());   //15 24Сʱ��������
					dayReport.BedRate =     NConvert.ToDecimal(this.Reader[16].ToString()); //16 ��λʹ����
					dayReport.Other1Num =   NConvert.ToInt32(this.Reader[17].ToString());   //17 ����1����
					dayReport.Other2Num =   NConvert.ToInt32(this.Reader[18].ToString());   //18 ����2����
					dayReport.OperInfo.ID =    this.Reader[19].ToString();                     //19 ����Ա����
					dayReport.OperInfo.OperTime=     NConvert.ToDateTime(this.Reader[20].ToString());//20 ����ʱ��
					dayReport.Memo =        this.Reader[21].ToString();                     //21 ��ע
					dayReport.NurseStation.ID = this.Reader[22].ToString();                 //22 ��ʿվ����
					dayReport.Name =        this.Reader[23].ToString();                     //23 ��������
					dayReport.InTransferInner = NConvert.ToInt32(this.Reader[24].ToString());//24 �ڲ�ת����
					dayReport.OutTransferInner = NConvert.ToInt32(this.Reader[25].ToString());//25 �ڲ�ת����

                    if (this.Reader.FieldCount >= 27)
                    {
                        dayReport.OutCure = NConvert.ToInt32(this.Reader[26].ToString());// ��Ժ����
                        dayReport.OutUnCure = NConvert.ToInt32(this.Reader[27].ToString());//��Ժδ��
                        dayReport.OutBetter = NConvert.ToInt32(this.Reader[28].ToString());//��Ժ��ת
                        dayReport.OutDeath = NConvert.ToInt32(this.Reader[29].ToString());//��Ժ����
                        dayReport.OutOther = NConvert.ToInt32(this.Reader[30].ToString());//��Ժ����
                    }
				
                    this.ProgressBarValue++;
					al.Add(dayReport);
				}
			}//�׳�����
			catch(Exception ex) 
			{
				this.Err="���סԺ�ձ���Ϣʱ����"+ex.Message;
				this.ErrCode="-1";
				return null;
			}
			this.Reader.Close();

			this.ProgressBarValue=-1;
			return al;
		}


		/// <summary>
		/// ���update����insert����Ĵ����������
		/// </summary>
		/// <param name="dayReport">�����</param>
		/// <returns>�ַ�������</returns>
		private string[] myGetParmInpatientDayReport(FS.HISFC.Models.HealthRecord.InpatientDayReport dayReport) 
		{

			string[] strParm={   
								 dayReport.DateStat.ToString(),      //0 ͳ������
								 dayReport.ID,                       //1 ���ұ���
								 dayReport.NurseStation.ID,          //2 ��ʿվ����
								 dayReport.BedStand.ToString(),      //3 �����ڲ�����
								 dayReport.BedAdd.ToString(),        //4 �Ӵ��� 
								 dayReport.BedFree.ToString(),       //5 �մ���
								 dayReport.BeginningNum.ToString(),  //6 �ڳ�������
								 dayReport.InNormal.ToString(),      //7 ������Ժ��
								 dayReport.InEmergency.ToString(),   //8 ������Ժ��
								 dayReport.InTransfer.ToString(),    //9 ������ת����
								 dayReport.InReturn.ToString(),      //10 �л���Ժ����
								 dayReport.OutNormal.ToString(),     //11 �����Ժ��
								 dayReport.OutTransfer.ToString(),   //12 ת����������
								 dayReport.OutWithdrawal.ToString(), //13 ��Ժ����
								 dayReport.EndNum.ToString(),        //14 ��ĩ������
								 dayReport.DeadIn24.ToString(),      //15 24Сʱ��������
								 dayReport.DeadOut24.ToString(),     //16 24Сʱ��������
								 dayReport.BedRate.ToString(),       //17 ��λʹ����
								 dayReport.Other1Num.ToString(),     //18 ����1����
								 dayReport.Other2Num.ToString(),     //19 ����2����
								 dayReport.Memo.ToString(),          //20 ��ע
								 this.Operator.ID,                    //21 ����Ա����
								 dayReport.InTransferInner.ToString(),//22 �ڲ�ת���� ����ɽ����
								 dayReport.OutTransferInner.ToString(),//23 �ڲ�ת���� ����ɽ����
                                 dayReport.OutCure.ToString(),//��Ժ����
                                 dayReport.OutUnCure.ToString(),//��Ժδ��
                                 dayReport.OutBetter.ToString(),//��Ժ��ת
                                 dayReport.OutDeath.ToString(),//��Ժ����
                                 dayReport.OutOther.ToString()//��Ժ����
                             
							 };								 
			return strParm;
		}

		
//		/// <summary>
//		/// ȡĳһ���ң��ض��������չ����
//		/// �˷�����FS.HisFC.Management.Manager.DepartmentExt�е�GetDepartmentExt������ͬ
//		/// </summary>
//		/// <param name="PropertyCode">���Ա���</param>
//		/// <param name="DeptID">���ұ���</param>
//		/// <returns>��������</returns>
		private FS.HISFC.Models.Base.DepartmentExt GetDepartmentExt(string PropertyCode,string DeptID) 
		{
			string strSQL = "";
			string strWhere = "";
			//ȡSELECT���
			if (this.Sql.GetCommonSql("Manager.DepartmentExt.GetDepartmentExtList",ref strSQL) == -1) 
			{
				this.Err="û���ҵ�Manager.DepartmentExt.GetDepartmentExtList�ֶ�!";
				return null;
			}
			if (this.Sql.GetCommonSql("Manager.DepartmentExt.And.DeptID",ref strWhere) == -1) 
			{
				this.Err="û���ҵ�Manager.DepartmentExt.And.DeptID�ֶ�!";
				return null;
			}
			//��ʽ��SQL���
			try 
			{
				strSQL += " " +strWhere;
				strSQL = string.Format(strSQL, PropertyCode,DeptID);
			}
			catch (Exception ex) 
			{
				this.Err = "��ʽ��SQL���ʱ����Manager.DepartmentExt.And.DeptID:" + ex.Message;
				return null;
			}

			//ȡ������������
			FS.HISFC.Models.Base.DepartmentExt departmentExt = new FS.HISFC.Models.Base.DepartmentExt();
			
			//ִ�в�ѯ���
			if (this.ExecQuery(strSQL)==-1) 
			{
				this.Err="��ÿ���������Ϣʱ��ִ��SQL������"+this.Err;
				this.ErrCode="-1";
				return null;
			}
			try 
			{
				while (this.Reader.Read()) 
				{
					//ȡ��ѯ����еļ�¼
					departmentExt = new FS.HISFC.Models.Base.DepartmentExt();
					departmentExt.Dept.ID   = this.Reader[0].ToString();          //0 ���ұ���
					departmentExt.Dept.Name = this.Reader[1].ToString();          //1 ��������
					departmentExt.PropertyCode   = this.Reader[2].ToString();     //2 ���Ա���
					departmentExt.PropertyName   = this.Reader[3].ToString();     //3 ��������
					departmentExt.StringProperty = this.Reader[4].ToString();     //4 �ַ����� 
					departmentExt.NumberProperty = NConvert.ToDecimal(this.Reader[5].ToString()); //5 ��ֵ����
					departmentExt.DateProperty   = NConvert.ToDateTime(this.Reader[6].ToString());//6 ��������
					departmentExt.Memo      = this.Reader[7].ToString();          //7 ��ע
					departmentExt.OperEnvironment.ID  = this.Reader[8].ToString();          //8 ��������
					departmentExt.OperEnvironment.OperTime  = NConvert.ToDateTime(this.Reader[9].ToString());     //9 ����ʱ��
					departmentExt.User01    = this.Reader[10].ToString();         //��������
				}
				this.Reader.Close();
			}//�׳�����
			catch(Exception ex) 
			{
				this.Err="��ÿ���������Ϣʱ����"+ex.Message;
				this.ErrCode="-1";
				return null;
			}

			return departmentExt;
		}


		/// <summary>
		/// ��ȡĳ�������е�סԺ������
		/// </summary>
		/// <param name="DeptId"></param>
		/// <returns></returns>
		private int GetDeptPatientNum(string DeptId,string NurseID)
		{
			try 
			{ 
				string strSQL="";
				if(this.Sql.GetCommonSql("Case.InpatientDayReport.GetDeptPatientNum",ref strSQL) == -1) 
				{
					this.Err="û���ҵ�Case.InpatientDayReport.GetDeptPatientNum�ֶ�!";
					return -1;
				}
 
				//����������ӵ�סԺ�ձ�������ֱ�ӷ���
				strSQL=string.Format(strSQL, DeptId, NurseID);            //�滻SQL����еĲ�����
			
				return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strSQL));
			}
			catch(Exception ex) 
			{
				this.Err = "��ʽ��SQL���ʱ����Case.InpatientDayReport.GetDeptPatientNum:" + ex.Message;
				this.WriteErr();
				return -1;
			}
		}

		#endregion 

		#region �ձ���ϸ����
		/// <summary>
		/// ȡȫԺĳһ���סԺ�ձ�����
		/// </summary>
		/// <param name="dateBegin">��ʼ����</param>
		/// <param name="dateEnd">��ֹ����</param>
		/// <returns>סԺ�ձ����飬������null</returns>
		public ArrayList GetDayReportDetailList(DateTime dateBegin, DateTime dateEnd) 
		{
			return this.GetDayReportDetailList(dateBegin, dateEnd, "ALL", "ALL");
		}


		/// <summary>
		/// ȡȫԺĳһ���סԺ�ձ�����
		/// </summary>
		/// <param name="dateBegin">��ʼ����</param>
		/// <param name="dateEnd">��ֹ����</param>
		/// <returns>סԺ�ձ����飬������null</returns>
		public ArrayList GetDayReportDetailList(DateTime dateBegin, DateTime dateEnd, string deptCode, string nurseCellCode) 
		{
			string strSQL = "";
			//string strWhere = "";
			//ȡSELECT���
			if (this.Sql.GetCommonSql("Case.DayReport.GetDayReportDetailList",ref strSQL) == -1) 
			{
				this.Err="û���ҵ�Case.DayReport.GetDayReportDetailList�ֶ�!";
				return null;
			}

			//��ʽ��SQL���
			try 
			{
				strSQL = string.Format(strSQL, dateBegin.ToString(), dateEnd.ToString(), deptCode);
			}
			catch (Exception ex) 
			{
				this.Err = "��ʽ��SQL���ʱ����Case.DayReport.GetDayReportDetailList:" + ex.Message;
				return null;
			}

			//ȡסԺ�ձ�����
			return this.myGetDayReportDetail(strSQL);
		}


		/// <summary>
		/// ���ձ���ϸ���в���һ����¼
		/// </summary>
		/// <param name="reportDetail">סԺ�ձ�ʵ��</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int InsertDayReportDetail(FS.HISFC.Models.HealthRecord.InpatientDayReport reportDetail) 
		{
			string strSQL="";
			if(this.Sql.GetCommonSql("Case.DayReport.InsertDayReportDetail",ref strSQL) == -1) 
			{
				this.Err="û���ҵ�Case.DayReport.InsertDayReportDetail�ֶ�!";
				return -1;
			}
			try 
			{  
				string[] strParm = myGetParmDayReportDetail( reportDetail );     //ȡ�����б�
				strSQL=string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
			}
			catch(Exception ex) 
			{
				this.Err = "��ʽ��SQL���ʱ����Case.DayReport.InsertDayReportDetail:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		
		
		/// <summary>
		/// �����ձ���ϸ����һ����¼
		/// </summary>
		/// <param name="reportDetail">סԺ�ձ�ʵ��</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int UpdateDayReportDetail(FS.HISFC.Models.HealthRecord.InpatientDayReport reportDetail) 
		{
			string strSQL="";
			if(this.Sql.GetCommonSql("Case.DayReport.UpdateDayReportDetail",ref strSQL) == -1) 
			{
				this.Err="û���ҵ�Case.DayReport.UpdateDayReportDetail�ֶ�!";
				return -1;
			}
			try 
			{  
				string[] strParm = myGetParmDayReportDetail( reportDetail );     //ȡ�����б�
				strSQL=string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
			}
			catch(Exception ex) 
			{
				this.Err = "��ʽ��SQL���ʱ����Case.DayReport.UpdateDayReportDetail:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		
				
		/// <summary>
		/// �����ձ���ϸ����һ����¼
		/// </summary>
		/// <param name="reportDetail">סԺ�ձ�ʵ��</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int CancelDayReportDetail(FS.HISFC.Models.HealthRecord.InpatientDayReport reportDetail) 
		{
			string strSQL="";
			if(this.Sql.GetCommonSql("Case.DayReport.CancelDayReportDetail",ref strSQL) == -1) 
			{
				this.Err="û���ҵ�Case.DayReport.CancelDayReportDetail�ֶ�!";
				return -1;
			}
			try 
			{  
				//����˵����0����סԺ��ˮ�ţ�1�ձ�ͳ�����ͣ�Ŀǰֻ�г�Ժ�������ϣ���2ͳ������
				strSQL=string.Format(strSQL, reportDetail.User02, "O", reportDetail.DateStat.ToString());            //�滻SQL����еĲ�����
			}
			catch(Exception ex) 
			{
				this.Err = "��ʽ��SQL���ʱ����Case.DayReport.CancelDayReportDetail:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		

		/// <summary>
		/// �ȸ���סԺ�ձ������û���ҵ����������һ��������
		/// </summary>
		/// <returns></returns>
		public int SetDayReportDetail(FS.HISFC.Models.HealthRecord.InpatientDayReport dayReport) 
		{
			//����dayReport�е�user01�������Ǻ�������
			//1 K �����䣬
			//2 RB��ת�룬
			//3 RD��ת����
			//4 O ����Ժ�Ǽǣ�
			//5 C ���ٻأ�
			//6 OF���޷���Ժ
			//			switch (dayReport.User01) {
			//				case "K":	//����
			//					dayReport.User01 = "I_NORMAL";
			//					break;
			//				case "RI":	//ת��
			//					dayReport.User01 = "I_TRANSFER";
			//					break;
			//				case "C":	//�ٻ�
			//					dayReport.User01 = "I_RETURN";
			//					break;
			//				case "RO":	//ת��
			//					dayReport.User01 = "O_TRANSFER";
			//					break;
			//				case "O":	//��Ժ�Ǽ�
			//					dayReport.User01 = "O_NORMAL";
			//					break;
			//				case "OF":	//�޷���Ժ
			//					dayReport.User01 = "O_WITHDRAWAL";
			//					break;
			//			}
			//			int parm;
			//			//�ȸ���סԺ�ձ�
			//			parm = this.InsertInpatientDayReportDetail(dayReport);
			//			return parm;
			return this.InsertDayReportDetail(dayReport);
		}


		/// <summary>
		/// ȡ�ձ���ϸ��Ϣ�б�������һ�����߶�������¼
		/// ˽�з����������������е���
		/// writed by cuipeng
		/// 2006-3
		/// </summary>
		/// <param name="SQLString">SQL���</param>
		/// <returns>NeuObject��������</returns>
		private ArrayList myGetDayReportDetail(string SQLString) 
		{
			ArrayList al=new ArrayList();                
			FS.HISFC.Models.HealthRecord.InpatientDayReport reportDetail; //סԺ�ձ���Ϣʵ��
			this.ProgressBarText="���ڼ���סԺ�ձ�����Ϣ...";
			this.ProgressBarValue=0;
			
			//ִ�в�ѯ���
			if (this.ExecQuery(SQLString)==-1) 
			{
				this.Err="��ÿ����Ϣʱ��ִ��SQL������"+this.Err;
				this.ErrCode="-1";
				return null;
			}
			try 
			{
				while (this.Reader.Read()) 
				{
					//ȡ��ѯ����еļ�¼
					reportDetail = new FS.HISFC.Models.HealthRecord.InpatientDayReport();
					reportDetail.DateStat = NConvert.ToDateTime(this.Reader[0].ToString()); //0 ͳ������
					reportDetail.ID       = this.Reader[1].ToString();                      //1 ���ұ���
					reportDetail.NurseStation.ID = this.Reader[2].ToString();               //2 ����վ����
					reportDetail.User01   = this.Reader[3].ToString();                      //3 ͳ������
					reportDetail.User02   = this.Reader[4].ToString();                      //4 סԺ��ˮ��
					reportDetail.User03   = this.Reader[5].ToString();                      //5 ����
					reportDetail.Memo     = this.Reader[6].ToString();                      //6 ��ע
					reportDetail.OperInfo.ID = this.Reader[7].ToString();                      //7 ����Ա
					reportDetail.OperInfo.OperTime = NConvert.ToDateTime(this.Reader[8].ToString()); //8 ����ʱ��
					
					this.ProgressBarValue++;
					al.Add(reportDetail);
				}
			}//�׳�����
			catch(Exception ex) 
			{
				this.Err="���סԺ�ձ���Ϣʱ����"+ex.Message;
				this.ErrCode="-1";
				return null;
			}
			this.Reader.Close();

			this.ProgressBarValue=-1;
			return al;
		}


		/// <summary>
		/// ���update����insert����Ĵ����������
		/// </summary>
		/// <param name="reportDetail">�����</param>
		/// <returns>�ַ�������</returns>
		private string[] myGetParmDayReportDetail(FS.HISFC.Models.HealthRecord.InpatientDayReport reportDetail) 
		{
            string strSQL = "";
            string patientNo = reportDetail.User02;
            string zg = "";
            if (this.Sql.GetCommonSql("Case.DayReport.GetDayReportDetailZg", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Case.DayReport.GetDayReportDetailZg�ֶ�!";
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, patientNo);            //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Case.DayReport.GetDayReportDetailZg:" + ex.Message;
                this.WriteErr();
                this.ErrCode = "-1";
                return null;
            }
           
     
         
          
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "���ת�����ʱ����ִ��SQL������" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    //ȡ��ѯ����еļ�¼
                    zg = this.Reader[0].ToString();          //0 ת�����                 
                }
                this.Reader.Close();
            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "���ת�����ʱ����" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            string[] strParm={   
								 reportDetail.DateStat.ToString(),      //0 ͳ������
								 reportDetail.ID,                       //1 ���ұ���
								 reportDetail.NurseStation.ID,          //2 ��ʿվ����
								 reportDetail.User02,					//3 סԺ��ˮ��
								 reportDetail.User03,					//4 ����
								 reportDetail.User01,					//5 ͳ������
								 this.Operator.ID,						//6 ����Ա����
								 reportDetail.Memo,					//7 ��ע
                                 zg,                                 //8 ת�����\
                                 //[2011-5-23]by zhaozf �޸Ĵ�λ�ձ�
                                 reportDetail.RelationDeptID,
                                 reportDetail.RelationNurseCellID
                                
							 };								 
			return strParm;
		}

		#endregion 


        #region ���ձ����� 2012-8-5 �Ժ����ձ��Դ�Ϊ׼
        /// <summary>
        /// �ϴ��´���ʶ��1-�ϴ���0-�´�
        /// </summary>
        private string strType = string.Empty;
        /// <summary>
        /// ������Դ����
        /// </summary>
        private string strSource = string.Empty;
        /// <summary>
        /// ��λ����
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="type">����:��Ժ�Ǽǣ��������ʿվ����޷���Ժ����Ժ�Ǽǡ���Ժ�ٻ� </param>
        /// <returns></returns>
        public int ArriveBed(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Base.EnumShiftType shiftType)
        {
            if (patientInfo == null)
            {
                return -1;
            }
            this.strSource = shiftType.ToString();
            switch (shiftType)
            {
                case  FS.HISFC.Models.Base.EnumShiftType.B://��Ժ�Ǽ�
                    this.strType = "0";
                    break;
                case FS.HISFC.Models.Base.EnumShiftType.K://����
                    this.strType = "0";
                    break;
                case FS.HISFC.Models.Base.EnumShiftType.OF://�޷���Ժ
                    this.strType = "1";
                    break;
                case FS.HISFC.Models.Base.EnumShiftType.O://��Ժ
                    this.strType = "1";
                    break;
                case FS.HISFC.Models.Base.EnumShiftType.C://��Ժ�ٻ�
                    this.strType = "0";
                    break;
                default :
                    break;
            }
            //�����/�ⴲ��¼
            FS.HISFC.Models.RADT.InPatientBedTransReord objBedInfo = this.TransBedInfo(patientInfo);
            if (objBedInfo == null)
            {
                return -1;
            }
            return this.InSertInPatientBedTransReord(objBedInfo);
        }
      
        /// <summary>
        /// ��λ����:ת��
        /// </summary>
        /// <param name="patientInfoOld">ת��ǰ������Ϣ</param>
        /// <param name="patientInfoNew">ת��ǰ������Ϣ</param>
        /// <returns></returns>
        public int TransBed(FS.HISFC.Models.RADT.PatientInfo patientInfoOld,FS.HISFC.Models.RADT.PatientInfo patientInfoNew)
        {
            if (patientInfoOld == null || patientInfoNew == null)
            {
                return -1;
            }

            //����ԭ���ҵ��´���¼
            this.strSource = FS.HISFC.Models.Base.EnumShiftType.RO.ToString();
            this.strType = "1";
            FS.HISFC.Models.RADT.InPatientBedTransReord objBedInfo = this.TransBedInfoForChangeDept(patientInfoOld, patientInfoNew);
            if (objBedInfo == null)
            {
                return -1;
            }
            if (this.InSertInPatientBedTransReord(objBedInfo) == -1)
            {
                return -1;
            }
            //�����¿��ҵ��ϴ���¼
            this.strType = "0";
            this.strSource = FS.HISFC.Models.Base.EnumShiftType.RI.ToString();
            objBedInfo = this.TransBedInfoForChangeDept(patientInfoOld, patientInfoNew);
            if (objBedInfo == null)
            {
                return -1;
            }
            return this.InSertInPatientBedTransReord(objBedInfo);
        }

        /// <summary>
        /// ��λ����:����
        /// </summary>
        /// <param name="patientInfo">����������Ϣ</param>
        /// <param name="bedInfo">�����Ĵ�λ��Ϣ </param>
        /// <param name="isAdd">�Ƿ������true-������false-������� </param>
        /// <returns></returns>
        public int AddExtentBed(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Base.Bed bedInfo,bool isAdd)
        {
            if (patientInfo == null || bedInfo == null)
            {
                return -1;
            }
            if (isAdd)
            {
                this.strType = "0";
                this.strSource = FS.HISFC.Models.Base.EnumShiftType.ABD.ToString();
            }
            else
            {
                this.strType = "1";
                this.strSource = FS.HISFC.Models.Base.EnumShiftType.RBD.ToString();
            }
            FS.HISFC.Models.RADT.InPatientBedTransReord objBedInfo = this.TransBedInfoAddBed(patientInfo, bedInfo);
            if (objBedInfo == null)
            {
                return -1;
            }
            return this.InSertInPatientBedTransReord(objBedInfo);
        }

        /// <summary>
        /// ת��������Ϣ�Ǳ�׼�Ĵ�λ��¼������ʽ
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        private FS.HISFC.Models.RADT.InPatientBedTransReord TransBedInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            FS.HISFC.Models.RADT.InPatientBedTransReord objBedInfo = null;
            if (patientInfo == null)
            {
                return objBedInfo;
            }
            objBedInfo = new FS.HISFC.Models.RADT.InPatientBedTransReord();
            objBedInfo.INPATIENT_NO = patientInfo.ID;
            objBedInfo.PATIENT_NO = patientInfo.PID.PatientNO;
            objBedInfo.OLD_DEPT_ID =string.Empty;
            objBedInfo.OLD_DEPT_NAME = string.Empty;
            objBedInfo.TARGET_DEPT_ID = patientInfo.PVisit.PatientLocation.Dept.ID;
            objBedInfo.TARGET_DEPT_NAME = patientInfo.PVisit.PatientLocation.Dept.Name;
            objBedInfo.BED_NO = patientInfo.PVisit.PatientLocation.Bed.ID;
            objBedInfo.TRANS_TYPE = this.strType;
            objBedInfo.TRANS_CODE = this.strSource;
            objBedInfo.MEDICAL_GROUP_CODE = string.Empty;
            objBedInfo.CARE_GROUP_CODE = string.Empty;
            objBedInfo.IN_DOCT_CODE = patientInfo.PVisit.AdmittingDoctor.ID;
            objBedInfo.NURSE_STATION_CODE = patientInfo.PVisit.PatientLocation.NurseCell.ID;
            objBedInfo.ZG = patientInfo.PVisit.ZG.ID;
            objBedInfo.SEQUENCE_NO = string.Empty ;
            objBedInfo.OPER_CODE = "009999";
            //objBedInfo.OPER_DATE = this.GetDateTimeFromSysDateTime();
            objBedInfo.DEPT_CODE = patientInfo.PVisit.PatientLocation.Dept.ID;
            objBedInfo.OLD_NURSE_ID = string.Empty;
            objBedInfo.OLD_NURSE_NAME = string.Empty;
            objBedInfo.TARGET_NURSE_ID = patientInfo.PVisit.PatientLocation.NurseCell.ID;
            objBedInfo.TARGET_NURSE_NAME = patientInfo.PVisit.PatientLocation.NurseCell.Name;
            return objBedInfo;

        }
        /// <summary>
        /// ת��������Ϣ�Ǳ�׼�Ĵ�λ��¼������ʽ(ת�Ʋ���ר��)
        /// </summary>
        /// <param name="patientInfoOld"></param>
        /// <param name="patientInfoNew"></param>
        /// <returns></returns>
        private FS.HISFC.Models.RADT.InPatientBedTransReord TransBedInfoForChangeDept(FS.HISFC.Models.RADT.PatientInfo patientInfoOld,FS.HISFC.Models.RADT.PatientInfo patientInfoNew)
        {
            FS.HISFC.Models.RADT.InPatientBedTransReord objBedInfo = null;
            if (patientInfoOld == null || patientInfoNew==null)
            {
                return objBedInfo;
            }
            objBedInfo = new FS.HISFC.Models.RADT.InPatientBedTransReord();
            objBedInfo.INPATIENT_NO = patientInfoOld.ID;
            objBedInfo.PATIENT_NO = patientInfoOld.PID.PatientNO;
            //����
            objBedInfo.OLD_DEPT_ID = patientInfoOld.PVisit.PatientLocation.Dept.ID;
            objBedInfo.OLD_DEPT_NAME = patientInfoOld.PVisit.PatientLocation.Dept.Name;
            objBedInfo.TARGET_DEPT_ID = patientInfoNew.PVisit.PatientLocation.Dept.ID;
            objBedInfo.TARGET_DEPT_NAME = patientInfoNew.PVisit.PatientLocation.Dept.Name;
            //��ʿվ
            objBedInfo.OLD_NURSE_ID = patientInfoOld.PVisit.PatientLocation.NurseCell.ID;
            objBedInfo.OLD_NURSE_NAME = patientInfoOld.PVisit.PatientLocation.NurseCell.Name;
            objBedInfo.TARGET_NURSE_ID = patientInfoNew.PVisit.PatientLocation.NurseCell.ID;
            objBedInfo.TARGET_NURSE_NAME = patientInfoNew.PVisit.PatientLocation.NurseCell.Name;
            if (this.strType == "1")
            {
                objBedInfo.BED_NO = patientInfoOld.PVisit.PatientLocation.Bed.ID;
                objBedInfo.DEPT_CODE = patientInfoOld.PVisit.PatientLocation.Dept.ID;
                objBedInfo.IN_DOCT_CODE = patientInfoOld.PVisit.AdmittingDoctor.ID;
                objBedInfo.NURSE_STATION_CODE = patientInfoOld.PVisit.PatientLocation.NurseCell.ID;
            }
            else
            {

                objBedInfo.BED_NO = patientInfoNew.PVisit.PatientLocation.Bed.ID;
                objBedInfo.DEPT_CODE = patientInfoNew.PVisit.PatientLocation.Dept.ID;
                objBedInfo.IN_DOCT_CODE = patientInfoNew.PVisit.AdmittingDoctor.ID;
                objBedInfo.NURSE_STATION_CODE = patientInfoNew.PVisit.PatientLocation.NurseCell.ID;
            }
            objBedInfo.TRANS_TYPE = this.strType;
            objBedInfo.TRANS_CODE = this.strSource;
            objBedInfo.MEDICAL_GROUP_CODE = string.Empty;
            objBedInfo.CARE_GROUP_CODE = string.Empty;

            objBedInfo.ZG = string.Empty;
            objBedInfo.SEQUENCE_NO = string.Empty;
            objBedInfo.OPER_CODE = "009999";
            //objBedInfo.OPER_DATE = this.GetDateTimeFromSysDateTime();

            return objBedInfo;

        }


        /// <summary>
        /// ת��������Ϣ�Ǳ�׼�Ĵ�λ��¼������ʽ
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="bedInfo"></param>
        /// <returns></returns>
        private FS.HISFC.Models.RADT.InPatientBedTransReord TransBedInfoAddBed(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Base.Bed bedInfo)
        {
            FS.HISFC.Models.RADT.InPatientBedTransReord objBedInfo = null;
            if (patientInfo == null)
            {
                return objBedInfo;
            }
            objBedInfo = new FS.HISFC.Models.RADT.InPatientBedTransReord();
            objBedInfo.INPATIENT_NO = patientInfo.ID;
            objBedInfo.PATIENT_NO = patientInfo.PID.PatientNO;
            objBedInfo.OLD_DEPT_ID = string.Empty;
            objBedInfo.OLD_DEPT_NAME = string.Empty;
            objBedInfo.TARGET_DEPT_ID = patientInfo.PVisit.PatientLocation.Dept.ID;
            objBedInfo.TARGET_DEPT_NAME = patientInfo.PVisit.PatientLocation.Dept.Name;
            if (this.strType == "1")
            {
                objBedInfo.BED_NO = patientInfo.PVisit.PatientLocation.Bed.ID;
            }
            else
            {
                objBedInfo.BED_NO = bedInfo.ID;
            }
            objBedInfo.TRANS_TYPE = this.strType;
            objBedInfo.TRANS_CODE = this.strSource;
            objBedInfo.MEDICAL_GROUP_CODE =string.Empty;
            objBedInfo.CARE_GROUP_CODE = string.Empty;
            objBedInfo.IN_DOCT_CODE = patientInfo.PVisit.AdmittingDoctor.ID;
            objBedInfo.NURSE_STATION_CODE = patientInfo.PVisit.PatientLocation.NurseCell.ID;
            objBedInfo.ZG = patientInfo.PVisit.ZG.ID;
            objBedInfo.SEQUENCE_NO = string.Empty;
            objBedInfo.OPER_CODE = "009999";
            //objBedInfo.OPER_DATE = this.GetDateTimeFromSysDateTime();
            objBedInfo.DEPT_CODE = patientInfo.PVisit.PatientLocation.Dept.ID;

            objBedInfo.OLD_NURSE_ID = string.Empty;
            objBedInfo.OLD_NURSE_NAME = string.Empty;
            objBedInfo.TARGET_NURSE_ID = patientInfo.PVisit.PatientLocation.NurseCell.ID;
            objBedInfo.TARGET_NURSE_NAME = patientInfo.PVisit.PatientLocation.NurseCell.Name;
            return objBedInfo;

        }

      
        /// <summary>
        /// ���봲λ�����¼
        /// </summary>
        /// <param name="p_objBedInfo">��λ��¼��Ϣ</param>
        /// <returns></returns>
        private int InSertInPatientBedTransReord(FS.HISFC.Models.RADT.InPatientBedTransReord p_objBedInfo)
        {
            if (p_objBedInfo == null)
            {
                return -1;
            }
            string strSql = string.Empty;

            if (Sql.GetCommonSql("RADT.InPatient.InSertInPatientBedTransReord", ref strSql) == 0)
            {
                #region SQL
                /*insert into met_cas_bedinfodailyreport
                  (inpatient_no,
                   patient_no,
                   old_dept_id,
                   old_dept_name,
                   target_dept_id,
                   target_dept_name,
                   bed_no,
                   trans_type,
                   trans_code,
                   medical_group_code,
                   care_group_code,
                   in_doct_code,
                   nurse_station_code,
                   zg,
                   sequence_no,
                   oper_code,
                   oper_date,
                   OLD_NURSTATION_CODE,
                   OLD_NURSTATION_NAME,
                   TARGET_NURSTATION_CODE,
                   TARGET_NURSTATION_NAME)*/
                #endregion
                try
                {
                     strSql = string.Format(strSql,
                                            p_objBedInfo.INPATIENT_NO,
                                            p_objBedInfo.PATIENT_NO,
                                            p_objBedInfo.OLD_DEPT_ID,
                                            p_objBedInfo.OLD_DEPT_NAME,
                                            p_objBedInfo.TARGET_DEPT_ID,
                                            p_objBedInfo.TARGET_DEPT_NAME,
                                            p_objBedInfo.BED_NO,
                                            p_objBedInfo.TRANS_TYPE,
                                            p_objBedInfo.TRANS_CODE,
                                            p_objBedInfo.MEDICAL_GROUP_CODE,
                                            p_objBedInfo.CARE_GROUP_CODE,
                                            p_objBedInfo.IN_DOCT_CODE,
                                            p_objBedInfo.NURSE_STATION_CODE,
                                            p_objBedInfo.ZG,
                                            p_objBedInfo.SEQUENCE_NO,
                                            p_objBedInfo.OPER_CODE,
                                            p_objBedInfo.DEPT_CODE,
                                            p_objBedInfo.OLD_NURSE_ID,
                                            p_objBedInfo.OLD_NURSE_NAME,
                                            p_objBedInfo.TARGET_NURSE_ID,
                                            p_objBedInfo.TARGET_NURSE_NAME); //��ע
                }
                catch
                {
                    Err = "�����������RADT.InPatient.InSertInPatientBedTransReord!";
                    WriteErr();
                    return -1;
                }
            }
            return ExecNoQuery(strSql);

        }
        #endregion

    }

}
