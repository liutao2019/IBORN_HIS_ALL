using System;
using System.Collections;
using System.Globalization;
using FS.HISFC.Models.Fee;
using FS.HISFC.Models.RADT;
using FS.FrameWork.Management;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.BizLogic.RADT
{
    /// <summary>
    /// �������۹���
    /// </summary>
	public class OutPatient : Database
	{
		/// <summary>
		/// write by lisy 2005-01-01
		/// </summary>
		///<history>
		/// adjust by zhouxs 2005-5-16
		///</history>
		public OutPatient()
		{
		}

		#region ��

		#endregion

		#region ����

		#endregion

		#region ����

		#region ���з���

		#endregion

		#region ע�Ỽ����Ϣ(ûд��)

		/// <summary>
		/// ע�Ỽ����Ϣ
		/// </summary>
		/// <param name="PatientInfo">�ǼǵĻ�����Ϣ</param>
		/// <returns>0�ɹ� -1ʧ��</returns>
		public int RegisterPatient(PatientInfo PatientInfo)
		{
			return 0;
		}

		#endregion

		#region ע��������Ϣ(ûд��)

		/// <summary>
		/// ע���û�
		/// </summary>
		/// <param name="PatientInfo">�ǼǵĻ�����Ϣ</param>
		/// <returns>0�ɹ� -1ʧ��</returns>
		public int DischargePatient(PatientInfo PatientInfo)
		{
			return 0;
		}

		#endregion

		#region ���»�����Ϣ(ûд��)

		/// <summary>
		/// ���»�����Ϣ ��סԺ��ˮ��Ϊ����
		/// </summary>
		/// <param name="PatientInfo"></param>
		/// <returns>0 �ɹ� -1ʧ��</returns>
		public int UpdatePatient(PatientInfo PatientInfo)
		{
			return 0;
		}

		#endregion

		#region	ɾ��������Ϣ(ûд��)

		/// <summary>
		/// ɾ��������Ϣ
		/// </summary>
		/// <param name="PatientInfo"></param>
		/// <returns>0 �ɹ� -1 ʧ��</returns>
		public int DeletePatient(PatientInfo PatientInfo)
		{
			return 0;
		}

		#endregion

		#region ���ı仯סԺ��(ûд��)

		/// <summary>
		/// �仯סԺ��
		/// </summary>
		/// <param name="PatientInfo"></param>
		/// <param name="newPatientNo"></param>
		/// <returns></returns>
		public int ChangePID(PatientInfo PatientInfo, string newPatientNo)
		{
			return 0;
		}

		#endregion

		#region �����µ������(ûд��)

		/// <summary>
		/// �����µ������
		/// </summary>
		/// <returns>�����µ������</returns>
		public string GetNewCardNo()
		{
			return "";
		}

		#endregion

		#region �����￨�Ų�ѯ������Ϣ

		//���߲�ѯ
		/// <summary>
		/// �����￨�Ų�ѯ������Ϣ
		/// </summary>
		/// <param name="strPatientNo"></param>
		/// <returns>������Ϣʵ��</returns>
		public PatientInfo PatientQuery(string strPatientNo)
		{
			string strSql = "";

			#region �ӿ�˵��

			//RADT.OutPatient.Get.2
			//����:�����
			//������������Ϣ

			#endregion

			if (Sql.GetCommonSql("RADT.OutPatient.Get.2", ref strSql) == -1) return null;
			try
			{
				strSql = string.Format(strSql, strSql);
			}
			catch
			{
				Err = "�������ԣ�RADT.OutPatient.Get.2";
				WriteErr();
				return null;
			}
			ArrayList al = myPatientQuery(strSql);
			if (al.Count > 0) return (PatientInfo) al[0];
			return null;
		}

		#endregion

		#region ����Ժʱ��κ�״̬��ѯ������Ϣ

		/// <summary>
		/// ���߲�ѯ-����Ժʱ��κ�״̬��ѯ������Ϣ
		/// </summary>
		/// <param name="beginDateTime"></param>
		/// <param name="endDateTime"></param>
		/// <param name="State"></param>
		/// <returns>ArrayList�б�</returns>
		public ArrayList PatientQuery(DateTime beginDateTime, DateTime endDateTime, InStateEnumService State)
		{
			#region �ӿ�˵��

			//RADT.OutPatient.1
			//����:ע��ʱ�俪ʼ��������״̬
			//������������Ϣ

			#endregion

			string strSql = "";
			string[] strArg = new string[3];
			strArg[0] = beginDateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
			strArg[1] = endDateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
			strArg[2] = State.ID.GetHashCode().ToString();
			if (Sql.GetCommonSql("RADT.OutPatient.1", ref strSql) == -1)
			{
				Err = "û���ҵ�RADT.OutPatient.1�ֶ�!";
				ErrCode = "-1";
				WriteErr();
				return null;
			}
			strSql = string.Format(strSql, strArg);
			return myPatientQuery(strSql);
		}

		#endregion

		#region ���µ��Կ���Ϣ	

		/// <summary>
		/// ���µ��Կ���Ϣ 
		/// </summary>
		/// <param name="card">���߾��￨��Ϣ</param>
		/// <returns>0 �ɹ� -1 ʧ��</returns>
		public int UpdateCardInfo(Card card)
		{
			#region �ӿ�˵��

			//����������Ϣ��RADT.OutPatient.UpdateCardInfo.1
			//update com_patientinfo
			//			set 
			//			IC_CARDNO='{1}',--���Կ���
			//			ACT_CODE ='{2}'--�˻����� 
			//			where 
			//			CARD_NO='{0}'--���� 

			#endregion

			string strSql = "";
			try
			{
				if (Sql.GetCommonSql("RADT.OutPatient.UpdateCard", ref strSql) == 0)
				{
					strSql = string.Format(strSql, card.ID, card.ICCard.ID);
				}
			}
			catch (Exception ex)
			{
				Err = ex.Message;
				ErrCode = ex.Message;
				return -1;
			}
			return ExecNoQuery(strSql);
		}

		#endregion

		public int UpdateCardState(string Card, string State)
		{
			#region �ӿ�˵��

			//����������Ϣ��RADT.OutPatient.UpdateCardInfo.1
			//update com_patientinfo
			//			set 
			//			IC_CARDNO='{1}',--���Կ���
			//			ACT_CODE ='{2}'--�˻����� 
			//			where 
			//			CARD_NO='{0}'--���� 

			#endregion

			string strSql = "";
			try
			{
				if (Sql.GetCommonSql("RADT.OutPatient.LoseCard", ref strSql) == 0)
				{
					strSql = string.Format(strSql, Card, State);
				}
			}
			catch (Exception ex)
			{
				Err = ex.Message;
				ErrCode = ex.Message;
				return -1;
			}
			return ExecNoQuery(strSql);
		}

		#region ���»���������Ϣ

		/// <summary>
		/// ��������¼
		/// </summary>
		/// <param name="card"></param>
		/// <returns>0 �ɹ� -1 ʧ��</returns>
		public int InsertPassWord(Card card)
		{
			#region

			//			insert into com_passwordrecord( parent_code,   --����ҽ�ƻ�������
			//		            current_code,   --����ҽ�ƻ�������		            
			//                CARD_NO,	--ҽ����ˮ�� �����ǹҺŵ���  סԺ��סԺ��ˮ��
			//                HAPPEN_NO,	--�������
			//                OLD_PASSWORD,	--	ԭ����
			//                NEW_PASSWORD,	--		������
			//                OPER_CODE,--	����Ա
			//                OPER_DATE	--		����ʱ��
			//                  VALUES 
			//		               ( '[��������]',   --����ҽ�ƻ�������
			//		            '[��������]',   --����ҽ�ƻ�������
			//		            '{0}',   --�����ˮ�?�����ǹҺŵ���  סԺ��סԺ��ˮ��
			//		            '{1}',   --�������
			//		            '{2}',   --ԭ����
			//		            '{3}',   --������
			//                '{4}',   --������		            
			//		            sysdate)  --����ʱ��		           

			#endregion

			string strSql = "";
			//			string strNo="";
			NeuObject obj = new NeuObject();

			if (Sql.GetCommonSql("RADT.OutPatient.InsertPassWord", ref strSql) == -1) return -1;
			try
			{
				strSql = string.Format(strSql, card.ID, card.NewPassword, card.OldPassword, Operator.ID);
			}
			catch (Exception ex)
			{
				Err = ex.Message;
				ErrCode = ex.Message;
				return -1;
			}
			return ExecNoQuery(strSql);
        }

        #region �������۵Ǽ�\����\����

        #region �������۵Ǽ�
        /// <summary>
        /// �������۵Ǽ�,ͬʱ������Ϣ���в���һ����¼
        /// </summary>
        /// <param name="PatientInfo">ԤԼ��λ������PatientInfo��bed ��</param>
        /// <returns>���� 0 �ɹ� С�� 0 ʧ��</returns>
        public int RegisterObservePatient(FS.HISFC.Models.Registration.Register OutPatient)
        {
            //�������۱�־
            if (UpdateEmergencyObserve(OutPatient.ID,FS.HISFC.Models.Base.EnumInState.N,FS.HISFC.Models.Base.EnumInState.R,GetSysDateTime(),DateTime.MinValue.ToString(),"") <= 0)
            {
                Err = "�������۱�־ʧ��";
                WriteErr();
                return -1;
            }

            InPatient InpatentManager = new InPatient();

            InpatentManager.SetTrans(this.Trans);

            //���±����¼����
            if (InpatentManager.SetShiftData(OutPatient.ID, FS.HISFC.Models.Base.EnumShiftType.EB, "���۵Ǽ�", OutPatient.DoctorInfo.Templet.Dept, OutPatient.DoctorInfo.Templet.Dept, false) >= 0)
            {
                return 0;
            }

            Err = "���±����¼��ʧ��";
            WriteErr();
            return -1;
        }
        #endregion

        #region �������۽���

        /// <summary>
        /// �������۽���
        /// </summary>
        /// <param name="register">�������ۻ���</param>
        /// <returns></returns>
        public int RecievePatient(FS.HISFC.Models.Registration.Register patientInfo, FS.HISFC.Models.Base.Bed bed, FS.HISFC.Models.Base.EnumShiftType type, string notes)
        {
            InPatient InpatentManager = new InPatient();

            InpatentManager.SetTrans(this.Trans);

            FS.HISFC.Models.Base.EnumInState status = FS.HISFC.Models.Base.EnumInState.R;
            int parm;

            //����ǰ�Ĵ�λ��Ϣ
            Bed oldBed = new Bed();
            oldBed.InpatientNO = "N"; //'N'����û�л���
            oldBed.Status.ID = "U"; //'U'����մ�

            //���º�Ĵ�λ��Ϣ
            bed.InpatientNO = patientInfo.ID;
            bed.Status.ID = "O";

            //���´�λ��Ϣ
            parm = this.UpdateBedStatus(bed, oldBed);
            if (parm != 1)
            {
                return parm;
            }

            patientInfo.PVisit.PatientLocation.Bed = bed;

            if (type.ToString() == "EK")
                //���ڱ�����͵���"����K"����Ժ״̬��"��Ժ�Ǽ�R"
                status = FS.HISFC.Models.Base.EnumInState.R;
            else if (type.ToString() == "EC")
                //���ڱ�����͵���"�ٻ�C"����Ժ״̬��"��Ժ�Ǽ�B"
                status = FS.HISFC.Models.Base.EnumInState.B;

            string strSql = "";
            //���䴦��
            //���»��߽����¼
            if (Sql.GetCommonSql("RADT.InPatient.ArrivePatient.2", ref strSql) == -1) return -1;
            try
            {
                string[] n = {
				             	patientInfo.ID,
				             	patientInfo.PVisit.PatientLocation.Dept.ID,
				             	patientInfo.PVisit.PatientLocation.Dept.Name,
				             	patientInfo.PVisit.PatientLocation.Bed.ID,
				             	patientInfo.PVisit.PatientLocation.Bed.Status.ID.ToString(),
				             	patientInfo.PVisit.AttendingDoctor.ID,
				             	patientInfo.PVisit.AttendingDoctor.Name,
				             	patientInfo.PVisit.ReferringDoctor.ID,
				             	patientInfo.PVisit.ReferringDoctor.Name,
				             	patientInfo.PVisit.ConsultingDoctor.ID,
				             	patientInfo.PVisit.ConsultingDoctor.Name,
				             	patientInfo.PVisit.AdmittingDoctor.ID,
				             	patientInfo.PVisit.AdmittingDoctor.Name,
				             	patientInfo.PVisit.AdmitSource.ID,
				             	patientInfo.PVisit.AdmitSource.Name,
				             	patientInfo.PVisit.AdmittingNurse.ID,
				             	patientInfo.PVisit.AdmittingNurse.Name,
				             	patientInfo.PVisit.InSource.ID,
				             	patientInfo.PVisit.InSource.Name,
				             	patientInfo.PVisit.Circs.ID,
				             	patientInfo.PVisit.Circs.Name,
				             	patientInfo.PVisit.PatientLocation.NurseCell.ID,
				             	patientInfo.PVisit.PatientLocation.NurseCell.Name,
				             	Operator.ID, //�����˱���
                                patientInfo.PVisit.AttendingDirector.ID,//�����α���
                                patientInfo.PVisit.AttendingDirector.Name//����������
				             };

                strSql = string.Format(strSql, n);
            }
            catch (Exception ex)
            {
                Err = "����ֵʱ�����" + ex.Message;
                WriteErr();
                return -1;
            }

            parm = ExecNoQuery(strSql);
            if (parm <= 0)
            {
                return parm;
            };
            
            //�����Ϣ
            if (InpatentManager.SetShiftData(patientInfo.ID, type, notes, patientInfo.PVisit.PatientLocation.NurseCell, patientInfo.PVisit.PatientLocation.Bed, patientInfo.IsBaby) < 0)
            {
                return -1;
            }
            if (type.ToString() == "EC") //��Ժ�ٻ�
            {
                //���²���״̬
                if (UpdateEmergencyObserve(patientInfo.ID, FS.HISFC.Models.Base.EnumInState.B, FS.HISFC.Models.Base.EnumInState.I,patientInfo.PVisit.InTime.ToString(), patientInfo.PVisit.OutTime.ToString(), patientInfo.PVisit.ZG.ID) <= 0)
                {
                    return -1;
                }
            }
            //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
            else if (type.ToString() == "IC") //����תסԺ�ٻ�
            {
                //���²���״̬
                if (UpdateEmergencyObserve(patientInfo.ID, FS.HISFC.Models.Base.EnumInState.C, FS.HISFC.Models.Base.EnumInState.I, patientInfo.PVisit.InTime.ToString(), patientInfo.PVisit.OutTime.ToString(), patientInfo.PVisit.ZG.ID) <= 0)
                {
                    return -1;
                }
            }
            else
            {
                //���²���״̬
                if (UpdateEmergencyObserve(patientInfo.ID, FS.HISFC.Models.Base.EnumInState.R, FS.HISFC.Models.Base.EnumInState.I, patientInfo.PVisit.InTime.ToString(), patientInfo.PVisit.OutTime.ToString(), patientInfo.PVisit.ZG.ID) <= 0)
                {
                    return -1;
                }
            }
            return 1;
        }

        #endregion

        //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
        #region ���ۻ��߳���
        /// <summary>
        /// ���ۻ��߳��غ���
        /// </summary>
        /// <param name="OutPatient">������Ϣ</param>
        /// <param name="type">EO���۳�Ժ�Ǽ� CPI����תסԺ CI����סԺ</param>
        /// <returns></returns>
        public int OutObservePatientManager(FS.HISFC.Models.Registration.Register OutPatient,FS.HISFC.Models.Base.EnumShiftType type,string note)
        {
            FS.HISFC.Models.RADT.ShiftTypeEnumService shiftService = new ShiftTypeEnumService();
            int resultValue = 0;
            
            switch (type)
            {
                case EnumShiftType.EO:
                    {
                        //���۳�Ժ�Ǽ�
                        resultValue = UpdateEmergencyObserve(OutPatient.ID, FS.HISFC.Models.Base.EnumInState.I, FS.HISFC.Models.Base.EnumInState.P, OutPatient.PVisit.InTime.ToString(), DateTime.MinValue.ToString(), "");
                        if (resultValue == 0)
                        {
                            resultValue = UpdateEmergencyObserve(OutPatient.ID, FS.HISFC.Models.Base.EnumInState.R, FS.HISFC.Models.Base.EnumInState.N, OutPatient.PVisit.InTime.ToString(), DateTime.MinValue.ToString(), "");
                        }
                        break;
                    }
                case EnumShiftType.CPI:
                    {
                        //����תסԺ
                        resultValue = UpdateEmergencyObserve(OutPatient.ID, FS.HISFC.Models.Base.EnumInState.I, FS.HISFC.Models.Base.EnumInState.E, OutPatient.PVisit.InTime.ToString(), GetSysDateTime(), "");
                        break;
                    }
                case EnumShiftType.CI:
                    {
                        //����סԺ
                        resultValue = UpdateEmergencyObserve(OutPatient.ID, FS.HISFC.Models.Base.EnumInState.E, FS.HISFC.Models.Base.EnumInState.C, OutPatient.PVisit.InTime.ToString(), GetSysDateTime(), "");
                        if (resultValue > 0)
                        {
                            //�ͷŴ�λ
                            Bed newBed = OutPatient.PVisit.PatientLocation.Bed.Clone();
                            OutPatient.PVisit.PatientLocation.Bed.InpatientNO = OutPatient.ID;
                            OutPatient.PVisit.PatientLocation.Bed.Status.ID = EnumBedStatus.O;
                            newBed.Status.ID = EnumBedStatus.U.ToString();
                            newBed.InpatientNO = "N";

                            //���´�λ״̬,���жϲ���

                            resultValue = this.UpdateBedStatus(newBed, OutPatient.PVisit.PatientLocation.Bed);
                            if (resultValue <= 0)
                            {
                                return -1;
                            }
                        }
                        break;
                    }
            }
            
            if(resultValue <=0)
            {
                Err = "�������۱�־ʧ��";
                return -1;
            }

            InPatient InpatentManager = new InPatient();

            InpatentManager.SetTrans(this.Trans);

            //���±����¼����
            if (InpatentManager.SetShiftData(OutPatient.ID, type, note, OutPatient.DoctorInfo.Templet.Dept, OutPatient.DoctorInfo.Templet.Dept, false) >= 0)
            {
                return 1;
            }

            Err = "���±����¼��ʧ��";
            return -1;
            
        }
        #endregion


        #region MyRegion

        /// <summary>
		/// ���߳�Ժ�Ǽ�
		/// </summary>
		/// <param name="patientInfo">���߻�����Ϣ</param>
		/// <returns></returns>
		public int RegisterOutHospital(FS.HISFC.Models.Registration.Register patientInfo)
		{
            InPatient InpatentManager = new InPatient();
            if (this.Trans != null)
            {
                InpatentManager.SetTrans(this.Trans);
            }
            Bed newBed = patientInfo.PVisit.PatientLocation.Bed.Clone();
            patientInfo.PVisit.PatientLocation.Bed.InpatientNO = patientInfo.ID;
            patientInfo.PVisit.PatientLocation.Bed.Status.ID = EnumBedStatus.O;
			newBed.Status.ID = EnumBedStatus.U.ToString();
			newBed.InpatientNO = "N";

			//���´�λ״̬,���жϲ���
           
			int parm = this.UpdateBedStatus(newBed, patientInfo.PVisit.PatientLocation.Bed);
			if (parm <= 0)
			{
				return parm;
			}
			 
			//�����Ϣ����
            if (InpatentManager.SetShiftData(patientInfo.ID, EnumShiftType.EO,
			                 "��Ժ�Ǽ�", patientInfo.DoctorInfo.Templet.Dept, patientInfo.DoctorInfo.Templet.Dept, patientInfo.IsBaby) < 0)
            {
				return -1;
            }

		    //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
            this.UpdateEmergencyObserve(patientInfo.ID,EnumInState.P,EnumInState.B,patientInfo.PVisit.InTime.ToString(),patientInfo.PVisit.OutTime.ToString(),patientInfo.PVisit.ZG.ID  );
            
            return 1; 
         
		}
        /// <summary>
        /// ��¼�������Ҵ���Ϣ BED_KIND 1 �Ҵ� 2 ����
        /// STATUS ״̬ 0 �Ҵ� 1 ���
        /// </summary>
        /// <param name="inpatientNO">סԺ��</param>
        /// <param name="bedNO">����</param>
        /// <param name="kind">���</param>
        /// <returns>���� 0 �ɹ���С�� 0 ʧ��</returns>
        public int ChangeBedInfo(string clinicNO, string bedNO, string kind)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.UpdateBedInfoRecord.1", ref strSql) == -1)
            #region SQL
            /*UPDATE fin_ipr_hangbedinfo   --�Ҵ���Ϣ��

					 SET  status='1',   --״̬ 0 �Ҵ� 1 ���
							  bed_kind = '{2}',
						  oper_code='{3}',   --����Ա

						  oper_date=sysdate    --��������
				   WHERE PARENT_CODE='[��������]'  and 
						 CURRENT_CODE='[��������]' and 
						 INPATIENT_NO='{0}' and
						 BED_NO = '{1}' and
						 STATUS='0'
						 */
            #endregion
            {
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, clinicNO, bedNO, kind, Operator.ID);
            }
            catch
            {
                Err = "����������ԣ�RADT.InPatient.UpdateBedInfoRecord.1";
                WriteErr();
                return -1;
            }
            if (ExecNoQuery(strSql) <= 0)
            {
                if (Sql.GetCommonSql("RADT.InPatient.InsertBedInfoRecord.1", ref strSql) == -1)
                {
                    return -1;
                }
                try
                {
                    strSql = string.Format(strSql, clinicNO, bedNO, kind, Operator.ID);
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                    return -1;
                }
                return ExecNoQuery(strSql);
            }
            return 0;
        }

        /// <summary>
        /// ��ѯ�������ⴲλռ����Ϣ���������Ҵ���
        /// </summary>
        /// <param name="InPatientNo"></param>
        /// <returns></returns>
        public ArrayList GetSpecialBedInfo(string clinicNO)
        {
            string sql = string.Empty;
            ArrayList al = new ArrayList();

            if (Sql.GetCommonSql("RADT.Inpatient.GetSpecialBedInfo.1", ref sql) == -1)
            #region SQL
            /*
				  SELECT bed_no,                    --����
				         bed_kind                   --1 �Ҵ� 2 ���� 
				         FROM fin_ipr_hangbedinfo   --�Ҵ���Ϣ��
				   where PARENT_CODE='[��������]' and 
							CURRENT_CODE='[��������]' and 
							inpatient_no='{0}'  and
							status = '0'            --�Ҵ�
				*/
            #endregion
            {
                return null;
            }
            sql = string.Format(sql, clinicNO);
            if (ExecQuery(sql) < 0) return null;

            #region "read"

            try
            {
                while (Reader.Read())
                {
                    Bed obj = new Bed();

                    try
                    {
                        obj.ID = Reader[0].ToString();
                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        WriteErr();
                    }
                    try
                    {
                        obj.Memo = Reader[1].ToString();
                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        WriteErr();
                    }
                    al.Add(obj);
                }
            }
            catch (Exception ex)
            {
                Err = "��ֵʱ�����" + ex.Message;
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
            Reader.Close();

            #endregion

            return al;
        }
        /// <summary>
        /// ����� ��ĳ�������ͷ�
        /// ��Ҵ��ô˺�������ȷ��Ŀ�괲λδռ��
        /// </summary>
        /// <param name="patientInfo">���߻�����Ϣ</param>
        /// <param name="bedNO">����id</param>
        /// <param name="type">1�Ҵ� 2  ����</param>
        /// <returns>0û�и��� 1�ɹ� -1 ʧ��</returns>
        public int UnWrapPatientBed(FS.HISFC.Models.Registration.Register patientInfo, string bedNO, string type)
        {
            #region �ӿ�˵��

            //���� ����� ��ĳ�������ͷ�
            //RADT.InPatient.PatientUnWapBed.2
            //���룺0 InpatientNoסԺ��ˮ��,1��������,2������
            //������0 

            #endregion

            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.PatientWapBed.2", ref strSql) == -1)
            {
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, patientInfo.ID, patientInfo.Name, bedNO);
            }
            catch
            {
                Err = "����������ԣ�RADT.InPatient.PatientWapBed.2";
                WriteErr();
                return -1;
            }
            if (ExecNoQuery(strSql) <= 0)
            {
                return -1;
            }
            if (ChangeBedInfo(patientInfo.ID, bedNO, type) < 0)
            {
                return -1;
            }
            return 0;
        }

        /// <summary>
		/// ���´�λ״̬��inpatientNo,����ԭ������Ϣ���в����ж�
		/// writed by cuipeng
		/// 2005-5
		/// </summary>
		/// <param name="newBed">�²�����Ϣ</param>
		/// <param name="oldBed">�ɲ�����Ϣ</param>
		/// <returns>0û���� >1 �ɹ� -1 ʧ��</returns>
		public int UpdateBedStatus(Bed newBed, Bed oldBed)
		{
			string strSql = string.Empty;
			if (Sql.GetCommonSql("RADT.InPatient.UpdateSickBedInfo.2", ref strSql) == -1)
				#region SQL
				/*
				UPDATE 	COM_BEDINFO 
			    SET   	CLINIC_NO= '{3}',        --���º�Ļ�����ˮ��
	            		BED_STATE = '{4}'        --���º�Ĵ�λ״̬
			    WHERE 	PARENT_CODE  = '[��������]'  
		        AND   	CURRENT_CODE = '[��������]' 
		        AND   	BED_NO       = '{0}'     --����ǰ�Ĵ���
		        AND   	CLINIC_NO    = '{1}'     --����ǰ�Ļ�����ˮ��
		        AND   	BED_STATE    = '{2}'     --����ǰ�Ĵ�λ״̬ 
				*/
				#endregion
			{
				return -1;
			}

			try
			{
				strSql = string.Format(strSql,
				                       newBed.ID, //0����
				                       oldBed.InpatientNO, //1����ǰ����ID
				                       oldBed.Status.ID, //2����ǰ��λ״̬
				                       newBed.InpatientNO, //3���º���ID
				                       newBed.Status.ID //4���º�λ״̬
					);
			}
			catch (Exception ex)
			{
				Err = ex.Message;
				ErrCode = ex.Message;
				WriteErr();
				return -1;
			}
			return ExecNoQuery(strSql);
		}

        /// <summary>
        /// ת�ƻ��� -�����������ң�������
        /// ��Ҫ�������(���²�����,���±����)
        /// </summary>
        /// <param name="PatientInfo">������Ϣ</param>
        /// <param name="newLocation">�µ�λ����Ϣ</param>
        /// <returns>0û�и���(��������),1�ɹ� -1ʧ��</returns>
        public int TransferPatient(FS.HISFC.Models.Registration.Register PatientInfo, Location newLocation)
        {
             InPatient InpatentManager = new InPatient();

            if (this.Trans != null)
            {
                InpatentManager.SetTrans(this.Trans);
            }
            //ת����
            if (newLocation.NurseCell.ID == string.Empty)
            {
                newLocation.NurseCell.ID = PatientInfo.PVisit.PatientLocation.NurseCell.ID;
                newLocation.NurseCell.Name = PatientInfo.PVisit.PatientLocation.NurseCell.Name;
            }
            //ת��
            if (newLocation.Dept.ID == string.Empty)
            {
                newLocation.Dept.ID = PatientInfo.PVisit.PatientLocation.Dept.ID;
                newLocation.Dept.Name = PatientInfo.PVisit.PatientLocation.Dept.Name;
            }
            //ת��
            if (newLocation.Bed.ID == string.Empty)
            {
                newLocation.Bed.ID = PatientInfo.PVisit.PatientLocation.Bed.ID;
            }


           

            //������ߴ�λ�����仯,����²�������Ϣ(�˴��Ĵ�����:ת��,ת��,ת����)
            if (newLocation.Bed.ID != PatientInfo.PVisit.PatientLocation.Bed.ID)
            {
                //���»������ڴ�λ����Ϣ
                //�����´�λ���ǰ����Ϣ,�����жϲ���
                Bed tempBed = newLocation.Bed.Clone();
                //oldBed.InpatientNo  = "N";
                //oldBed.BedStatus.ID = "U";

                //��������Ϣ
                if (InpatentManager.SetShiftData(PatientInfo.ID, EnumShiftType.RB,
                                 "ת��", PatientInfo.PVisit.PatientLocation.Bed, tempBed, false) < 0)
                {
                    return -1;
                }

                //�޸��µĴ�λ��Ϣ(����ID�ͻ���ԭ��λ��״̬)
                tempBed.InpatientNO = PatientInfo.ID;
                //������ǰ����ԭ��λΪ��,���޸�Ϊռ��
                if (PatientInfo.PVisit.PatientLocation.Bed.ID == string.Empty)
                    tempBed.Status.ID = "O";
                else
                    tempBed.Status.ID = PatientInfo.PVisit.PatientLocation.Bed.Status.ID;

                //�����´�λ:ԭ��λ�ϵĻ��߻����´�λ��
                int parm = UpdateBedStatus(tempBed, newLocation.Bed);
                if (parm <= 0) return parm;

                //����´�λ�ڱ��ǰ�ǿմ����һ����ڱ��ǰ�д�λ(˵�����β����ǻ���,���ǽ������),����ջ���ԭ��λ�Ĵ�λ��Ϣ
                if (newLocation.Bed.InpatientNO == "N" && PatientInfo.PVisit.PatientLocation.Bed.ID != string.Empty)
                {
                    //�޸Ļ��߱��ǰ�Ĵ�λ��Ϣ
                    tempBed = PatientInfo.PVisit.PatientLocation.Bed.Clone();
                    tempBed.InpatientNO = "N";
                    tempBed.Status.ID = "U";

                    //���»��߱��ǰ��λ:���
                    parm = UpdateBedStatus(tempBed, PatientInfo.PVisit.PatientLocation.Bed);
                    if (parm <= 0) return parm;
                }
            }

            return 1;
        }
        #region ת��

        /// <summary>
        /// ���������˵Ĵ�λ��Ϣ
        /// <br>���룺Դ����,Ŀ�껼��</br>
        /// </summary>
        /// <param name="sourcePatientInfo">ԭ���߻�����Ϣ</param>
        /// <param name="targetPatientInfo">Ŀ�껼�߻�����Ϣ</param>
        /// <returns>0�ɹ� -1ʧ��</returns>
        public int SwapPatientBed(FS.HISFC.Models.Registration.Register sourcePatientInfo, FS.HISFC.Models.Registration.Register targetPatientInfo)
        {
            #region �ӿ�˵��

            //���������˵Ĵ�λ��Ϣ
            //���»��ߵĴ�λ�����ҡ�������Ϣ��
            //RADT.InPatient.SwapPatient.1
            //���룺old (0 InpatientNoסԺ��ˮ��,1��������,(2����id,3����name,
            //		4����id,5����name,6����id)
            //		new(7 סԺ��ˮ�ţ�8����,9����id,10����name,
            //		11����id,12����name,13����id)
            //������0 

            #endregion

            //���²�����
            if (sourcePatientInfo.PVisit.PatientLocation.Bed.ID != targetPatientInfo.PVisit.PatientLocation.Bed.ID)
            {
                //ǰ�߻�������
                int parm = TransferPatient(sourcePatientInfo, targetPatientInfo.PVisit.PatientLocation);
                if (parm != 1)
                {
                    return parm;
                }

                //���߻�������
                parm = TransferPatient(targetPatientInfo, sourcePatientInfo.PVisit.PatientLocation);
                if (parm != 1)
                {
                    return parm;
                }
            }

            return 1;
        }

       
        #endregion

		#endregion
        #endregion


        /// <summary>
        /// �������۵Ǽ�\����\����
        /// </summary>
        /// <param name="clinicID">�����</param>
        /// <param name="enuInstate">״̬</param>
        /// <param name="dtBeginTime">��ʼʱ��</param>
        /// <param name="dtEndTime">����ʱ��</param>
        /// <param name="strZG">ת�����</param>
        /// <returns>-1ʧ��,�����ɹ�</returns>
        public int UpdateEmergencyObserve(string clinicID, FS.HISFC.Models.Base.EnumInState oldenuInstate, FS.HISFC.Models.Base.EnumInState newenuInstate, string dtBeginTime, string dtEndTime, string strZG)
        {
            string StrSql = "";
            if (this.Sql.GetCommonSql("RADT.OutPatient.UpdateEmergencyObserve.1", ref StrSql) == -1) return -1;
            StrSql = string.Format(StrSql, clinicID, oldenuInstate,newenuInstate,dtBeginTime, dtEndTime, strZG);
            return this.ExecNoQuery(StrSql);

        }
      

		#endregion

		#region ˽�з���

		#endregion

		#region ���ݴ���sql����ѯ������Ϣ

		/// <summary>
		/// ���ݴ���sql����ѯ������Ϣ
		/// </summary>
		/// <param name="strSql">����sql���</param>
		/// <returns>ArrayList������Ϣ�б�</returns>
		private ArrayList myPatientQuery(string strSql)
		{
			ArrayList al = new ArrayList();
			PatientInfo PatientInfo;
			ProgressBarText = "���ڲ�ѯ����...";
			ProgressBarValue = 0;

			try
			{
				ExecQuery(strSql);
				try
				{
					while (Reader.Read())
					{
						PatientInfo = new PatientInfo();
						//<!--0 id, 1 name, 2 סԺ��, 3 �����, 4 ������, 5 �籣��
						//	 6 ��������, 7 sex id or Name,8,address,9 country ,10 phone_home
						//	 11 phone_work,12 ����״̬ id,13 ���֤,14 ����,15 ����ʱ��
						//	 16 ����֤����,17 ְҵ id,18 ְҵname ,19 ����
						//	-->
						//���߷�����Ϣ	<!--0 id, 1 name, 20 �������id, 21 ����id, 22 ����name, 23 ����id
						//			  24 ����name, 25 ¥,26,��,27 �� ,28 ����
						//			  29 ����״̬,30 ����ҽʦ id,31 ����ҽʦname,32 ������ҽʦid,33 ������ҽʦ name
						//			  34 ����ҽʦid,35 ����ҽʦname,36 סԺҽʦid  ,37 סԺҽʦname,38,ת��/ת������ id
						//			  39 ת��/ת������name,40 ��ICU���� id ,41 ��ICU���� name,42 ��Ժ;�� id,43 ��Ժ;�� name
						//			  44 ���λ�ʿ id ,45 ���λ�ʿ name,46 ����״̬id,47 ����״̬ name,48 סԺ״̬id,
						//			  49 סԺ���� ,50 ��Ժ���� ,51 ԤԼ��Ժ���� ,52 ע������,53 ���id,
						//			  54 ��� name,55 ������� -->
						//	���߷�����Ϣ
						//<!--0 id,1 name,56  �ܹ����tot_cost,57 �Էѽ�� own_cost,
						//58  �Ը���� Pay_Cost,59 ���ѽ�� Pub_Cost,
						//60  ʣ���� Left_Cost,61 ������ Dereate_Cost
						//62  ���ս�� Supply_Cost;-->

						#region ��û��߻�����Ϣ

						try
						{
							PatientInfo.ID = Reader[0].ToString();
							PatientInfo.Name = Reader[1].ToString();
							PatientInfo.ID = PatientInfo.ID;
							PatientInfo.Name = PatientInfo.Name;
							PatientInfo.PID.PatientNO = Reader[2].ToString();
							PatientInfo.PID.CardNO = Reader[3].ToString();
							PatientInfo.PID.CaseNO = Reader[4].ToString();
							PatientInfo.SSN = Reader[5].ToString();
							try
							{
								PatientInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[6].ToString());
							}
							catch
							{
							}
							PatientInfo.Sex.ID = (Reader[7].ToString());

							PatientInfo.AddressHome = Reader[8].ToString();
							PatientInfo.Country.Name = Reader[9].ToString();
							PatientInfo.PhoneHome = Reader[10].ToString();
							PatientInfo.PhoneBusiness = Reader[11].ToString();
							PatientInfo.MaritalStatus.ID = Reader[12].ToString();
							PatientInfo.IDCard = Reader[13].ToString();
							PatientInfo.Nationality.ID = Reader[14].ToString();
							try
							{
								PatientInfo.DeathTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[15].ToString());
							}
							catch
							{
							}
							PatientInfo.DeathAttestor.Name = Reader[16].ToString();
							PatientInfo.Profession.ID = Reader[17].ToString();
							PatientInfo.Profession.Name = Reader[18].ToString();
							PatientInfo.DIST = Reader[19].ToString();
						}
						catch (Exception ex)
						{
							Err = "��ȡ���߻�����Ϣ����" + ex.Message;
							WriteErr();
							al = null;
						}

						#endregion

						#region ���߷�����Ϣ

						try
						{
							PatientInfo.PVisit.ID = PatientInfo.ID;
							PatientInfo.PVisit.Name = PatientInfo.Name;
							PatientInfo.PVisit.PatientType.ID = Reader[20].ToString();
							PatientInfo.PVisit.PatientLocation.Dept.ID = Reader[21].ToString();
							PatientInfo.PVisit.PatientLocation.Dept.Name = Reader[22].ToString();
							PatientInfo.PVisit.PatientLocation.NurseCell.ID = Reader[23].ToString();
							PatientInfo.PVisit.PatientLocation.NurseCell.Name = Reader[24].ToString();
							PatientInfo.PVisit.PatientLocation.Building = Reader[25].ToString();
							PatientInfo.PVisit.PatientLocation.Floor = Reader[26].ToString();
							PatientInfo.PVisit.PatientLocation.Room = Reader[27].ToString();
							PatientInfo.PVisit.PatientLocation.Bed.ID = Reader[28].ToString();
							PatientInfo.PVisit.PatientLocation.Bed.Status.ID = Reader[29].ToString();
						}
						catch (Exception ex)
						{
							Err = "��ȡ���߷��ʻ�����Ϣ����" + ex.Message;
							WriteErr();
							al = null;
						}

						#endregion

						#region ���߷�����Ϣ

						try
						{
							PatientInfo.FT.ID = PatientInfo.ID;
							PatientInfo.FT.Name = PatientInfo.Name;
							PatientInfo.FT.TotCost = decimal.Parse(Reader[56].ToString());
							PatientInfo.FT.OwnCost = decimal.Parse(Reader[57].ToString());
							PatientInfo.FT.PayCost = decimal.Parse(Reader[58].ToString());
							PatientInfo.FT.PubCost = decimal.Parse(Reader[59].ToString());
							PatientInfo.FT.LeftCost = decimal.Parse(Reader[60].ToString());
							PatientInfo.FT.DerateCost = decimal.Parse(Reader[61].ToString());
							PatientInfo.FT.SupplyCost = decimal.Parse(Reader[62].ToString());
						}
						catch (Exception ex)
						{
							Err = "��ȡ���߷��û�����Ϣ����" + ex.Message;
							WriteErr();
							al = null;
						}

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
					return al;
				}

				ProgressBarValue = -1;
				return al;
			}
			catch (Exception ex)
			{
				Err = "��û��߻�����Ϣ����" + ex.Message;
				ErrCode = "-1";
				WriteErr();
				al = null;
				return al;
			}
			finally
			{
				al = null;
			}
		}

		///��ˮ��
		///
		///
		private string GetNewPassWordID()
		{
			// SELECT PASSWORD.nextval	FROM dual
			string sql = "";
			if (Sql.GetCommonSql("RADT.OutPatient.GetNewPassWordID", ref sql) == -1) return null;
			string strReturn = ExecSqlReturnOne(sql);
			if (strReturn == "-1" || strReturn == "") return null;
			return strReturn;
		}


		public int SaveMoney(string strCardNo, decimal dMoney)
		{
			#region �ӿ�˵��

			//����������Ϣ��RADT.OutPatient.UpdateMoney
			//			update com_patientinfo set 
			//			lact_sum = act_amt,
			//			act_amt=act_amt+{1} 
			//			where 
			//			PARENT_CODE='[��������]'  
			//			and CURRENT_CODE='[��������]' 
			//			and card_no='{0}'

			#endregion

			int iRet = 1;
			string strSql = "";
			if (Sql.GetCommonSql("RADT.OutPatient.UpdateMoney", ref strSql) == 0)
			{
				try
				{
					strSql = string.Format(strSql, strCardNo, dMoney);
					if (ExecNoQuery(strSql) > 0)
					{
						Card obj = new Card();
						obj = GetMoney(strCardNo);
						if (obj != null)
						{
							if (InsertMoney(obj) > 0)
								return iRet = 1;
						}
						else
						{
							return iRet = -1;
						}
					}
					else
					{
						return iRet = -1;
					}
				}
				catch (Exception ex)
				{
					Err = ex.Message;
					ErrCode = ex.Message;
					return iRet = -1;
				}
			}
			else
			{
				return iRet = -1;
			}
			return iRet;
		}

		private int UpdateMoney()
		{
			return 0;
		}

		public Card GetMoney(string strCardNo)
		{
			Card obj = new Card();
			string sql1 = "";

			#region �ӿ�˵��

			//						select lact_sum,act_amt from com_patientinfo
			//			where
			//			PARENT_CODE='[��������]'  
			//			and CURRENT_CODE='[��������]' 
			//			and card_no = '{0}'
			//RADT.OutPatient.Get.2
			//����:����
			//�������������Ϣ

			#endregion

			if (Sql.GetCommonSql("RADT.OutPatient.GetMoney", ref sql1) == -1) return null;
			try
			{
				sql1 = string.Format(sql1, strCardNo);
			}
			catch
			{
				Err = "RADT.OutPatient.GetMoney";
				WriteErr();
				return null;
			}
			if (ExecQuery(sql1) == -1) return null;
			if (Reader.Read())
			{
				obj.ID = strCardNo;
				obj.OldAmount = Convert.ToDecimal(Reader[0].ToString());
				obj.NewAmount = Convert.ToDecimal(Reader[1].ToString());
				Reader.Close();
				return obj;
			}
			else
			{
				Reader.Close();
				return null;
			}
		}

		private int InsertMoney(Card oECard)
		{
			#region �ӿ�˵��

			//INSERT  INTO 
			//FIN_COM_ACCOUNTRECORD 
			//(parent_code,   --����ҽ�ƻ������� \n                   
			// current_code,   --����ҽ�ƻ������� \n      
			//CARD_NO ,--����\n
			//HAPPEN_NO,--�������\n
			//OLD_AMOUNT,--���ʻ����\n
			//NEW_AMOUNT,--���˻����\n
			//OPER_CODE,OPER_DATE)
			//VALUES
			//('[��������]',   --����ҽ�ƻ������� \n                   
			//'[��������]',   --����ҽ�ƻ������� \n   
			//'{0}',
			//(SELECT NVL(MAX(HAPPEN_NO),0)+1 FROM FIN_COM_ACCOUNTRECORD),
			//{1},{2},'{3}',sysdate)

			#endregion

			string strSql = "";
			//			FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
			if (Sql.GetCommonSql("RADT.OutPatient.InsertMoney", ref strSql) == -1) return -1;
			try
			{
				strSql = string.Format(strSql, oECard.ID, oECard.OldAmount, oECard.NewAmount, Operator.ID);
			}
			catch (Exception ex)
			{
				Err = ex.Message;
				ErrCode = ex.Message;
				return -1;
			}
			return ExecNoQuery(strSql);
		}

		/// <summary>
		/// ���µ��Կ���Ϣ 
		/// <param name="strID"></param>
		/// <param name="strPassWord"></param>
		/// <returns></returns>
		/// </summary>
		public int SetCardPassword(string strID, string strPassWord)
		{
			int iRet = 1;

			#region �ӿ�˵��

			//����������Ϣ��RADT.OutPatient.SetCardPassword
			//update com_patientinfo
			//			set 
			//			ACT_CODE ='{1}'--�˻����� 
			//			where 
			//PARENT_CODE='[��������]'  
			//and CURRENT_CODE='[��������]' 
			//and CARD_NO='{0}'--���� 

			#endregion

			string strOldPassword = "";
			if (GetPassWord(strID) == "") strOldPassword = strPassWord;
			strOldPassword = GetPassWord(strID);
			string strSql = "";
			if (Sql.GetCommonSql("RADT.OutPatient.SetCardPassword", ref strSql) == 0)
			{
				try
				{
					strSql = string.Format(strSql, strID, strPassWord);
				}
				catch (Exception ex)
				{
					Err = ex.Message;
					ErrCode = ex.Message;
					iRet = -1;
				}
			}
			else
			{
				iRet = -1;
			}
			Card obj = new Card();
			obj.ID = strID;
			obj.NewPassword = strPassWord;
			obj.OldPassword = strOldPassword;
			if (ExecNoQuery(strSql) > 0)
			{
				if (InsertPassWord(obj) != -1)
				{
					iRet = 1;
				}
			}
			else
			{
				iRet = -1;
			}
			return iRet;
		}

		private string GetPassWord(string strCardNo)
		{
			string strPassWord = "";
			string sql1 = "";

			#region �ӿ�˵��

			//			select ACT_CODE from com_patientinfo
			//			where
			//			PARENT_CODE='[��������]'  
			//			and CURRENT_CODE='[��������]' 
			//			and card_no = '{0}'
			//RADT.OutPatient.GetPassWord
			//����:����
			//�������������Ϣ

			#endregion

			if (Sql.GetCommonSql("RADT.OutPatient.GetPassWord", ref sql1) == -1) return null;
			try
			{
				sql1 = string.Format(sql1, strCardNo);
			}
			catch
			{
				Err = "RADT.OutPatient.GetPassWord";
				WriteErr();
				return null;
			}
			if (ExecQuery(sql1) == -1) return null;
			if (Reader.Read())
			{
				strPassWord = Reader[0].ToString();
				Reader.Close();
			}
			else
			{
				Reader.Close();
				strPassWord = "";
			}
			return strPassWord;
		}

		public int LogoutCard(string strCardID)
		{
			string strSql = "";

			#region "�ӿ�"

			//���룺0 1 
			//������0

			#endregion

			if (Sql.GetCommonSql("RADT.OutPatient.LogoutCard", ref strSql) == -1) return -1;
			try
			{
				strSql = string.Format(strSql, strCardID);
			}
			catch (Exception ex)
			{
				Err = ex.Message;
				ErrCode = ex.Message;
				return -1;
			}
			return ExecNoQuery(strSql);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="patientno">סԺ��</param>
		/// <returns></returns>
		public TransferFee leaveHospital(string patientno)
		{
			string strSql = "";
			TransferFee info = null;
			if (Sql.GetCommonSql("Radt.OutPatient.leaveHospital", ref strSql) == -1) return null;
			try
			{
				strSql = string.Format(strSql, patientno);
				ExecQuery(strSql);
				while (Reader.Read())
				{
					info = new TransferFee();
					if (Reader[0] != DBNull.Value)
					{
						info.FT.TotCost = Convert.ToDecimal(Reader[0]); // ȡ���ý�� δ��
					}
					if (Reader[1] != DBNull.Value)
					{
						info.FT.OwnCost = Convert.ToDecimal(Reader[1]); //ȡ���ý��  �ѽ�
					}
					if (Reader[2] != DBNull.Value)
					{
						info.FT.PayCost = Convert.ToDecimal(Reader[2]); //ȡԤ����  
					}
				}
				Reader.Close();
			}
			catch (Exception ee)
			{
				Err = ee.Message;
			}
			return info;
		}

		#endregion

		/// <summary>
		/// ���ݿ��Ż�ÿ���״̬
		/// </summary>
		/// <param name="CardNo"></param>
		/// <returns></returns>
		public string GetValidCardState(string CardNo)
		{
			string strSql = "";
			if (Sql.GetCommonSql("RADT.GetValidCardState.1", ref strSql) == -1) return null;
			try
			{
				strSql = string.Format(strSql, CardNo);
			}
			catch (Exception ex)
			{
				Err = ex.Message;
				ErrCode = ex.Message;
				return null;
			}
			return ExecSqlReturnOne(strSql);
		}

		/// <summary>
		/// ���ݿ��Ż������
		/// </summary>
		/// <param name="CardNo"></param>
		/// <returns></returns>
		public string GetOldPassword(string CardNo)
		{
			string strSql = "";
			if (Sql.GetCommonSql("RADT.GetOldPassword.1", ref strSql) == -1) return null;
			try
			{
				strSql = string.Format(strSql, CardNo);
			}
			catch (Exception ex)
			{
				Err = ex.Message;
				ErrCode = ex.Message;
				return null;
			}
			return ExecSqlReturnOne(strSql);
		}

		#endregion
	}
}