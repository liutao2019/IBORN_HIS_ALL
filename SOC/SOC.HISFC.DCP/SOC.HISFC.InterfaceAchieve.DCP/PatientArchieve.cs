using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.SOC.HISFC.BizProcess.DCPInterfaceAchieve
{
    public class PatientArchieve : FS.SOC.HISFC.BizProcess.DCPInterface.IInpatient,FS.SOC.HISFC.BizProcess.DCPInterface.IOutpatient
    {
        private FS.HISFC.BizProcess.Integrate.RADT radtMgr = new FS.HISFC.BizProcess.Integrate.RADT();
        /// <summary>
        /// סԺ������Ϣ������
        /// </summary>
        private FS.HISFC.BizLogic.RADT.InPatient inpatientManagment = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// ���ﻼ�߹�����
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regManagment = new FS.HISFC.BizLogic.Registration.Register();

        #region IInpatient ��Ա

        public System.Collections.ArrayList GetPatientInfoByPatientNOAll(string patientNO)
        {
            System.Collections.ArrayList al = this.inpatientManagment.QueryInpatientNOByPatientNO( patientNO );
            if (al == null)
            {
                this.SetMsg( this.inpatientManagment );
                return null;
            }

            ArrayList alInpatient = null;
            this.msg = "δ�ҵ���Ӧ��סԺ���������ŵĻ���סԺ��¼";

            foreach (FS.FrameWork.Models.NeuObject info in al)
            {
                //if (info.Memo == "I")           //��Ժ����
                {
                    FS.HISFC.Models.RADT.PatientInfo patient = this.inpatientManagment.QueryPatientInfoByInpatientNO( info.ID );

                    alInpatient = new ArrayList();
                    alInpatient.Add( patient );

                    break;
                }
            }

            return alInpatient;
        }

        //wss
        public System.Collections.ArrayList QueryPatientByDeptCode(string deptCode, FS.HISFC.Models.RADT.InStateEnumService instate)
        {
            return radtMgr.QueryPatientByDeptCode(deptCode, instate);

        }

        public System.Collections.ArrayList GetPatientInfoByPatientName(string patientName)
        {
            System.Collections.ArrayList al = this.inpatientManagment.QueryComPatientInfoByName(patientName);

            this.SetMsg(this.inpatientManagment);

            return al;
        }

        public FS.HISFC.Models.RADT.Patient GetBasePatientInfomation(string patientCardNO)
        {
            //FS.HISFC.Models.RADT.PatientInfo info = this.inpatientManagment.GetPatientInfoByCardNO(patientCardNO);

            //this.SetMsg(this.inpatientManagment);

            //return info;

            return null;
        }

        public System.Collections.ArrayList QueryPatientInfoBySqlWhere(string strWhere)
        {
            System.Collections.ArrayList al = this.GetPatientInfoByWhere(strWhere);

            this.SetMsg(this.inpatientManagment);

            return al;
        }

        /// <summary>
        /// ����sqlwhere������ѯ������Ϣ����
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public System.Collections.ArrayList GetPatientInfoByWhere(string sqlWhere)
        {
            FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();
            string strSql = "";
            if (dataManager.Sql.GetSql("RADT.InPatient.QueryBaseInfo", ref strSql) == -1)
            {
                return null;
            }
            if (strSql == null)
            {
                return null;
            }
            if (sqlWhere == null)
            {
                return null;
            }

            ArrayList al = new ArrayList();
            FS.HISFC.Models.RADT.PatientInfo PatientInfo;

            if (dataManager.ExecQuery(strSql + sqlWhere) == -1)
            {
                return null;
            }

            try
            {
                while (dataManager.Reader.Read())
                {
                    FS.HISFC.Models.RADT.PatientInfo info = new FS.HISFC.Models.RADT.PatientInfo();

                    info.ID = dataManager.Reader[0].ToString();

                    info.PID.ID = info.ID;

                    info.PID.PatientNO = dataManager.Reader[1].ToString();
                    info.Name = dataManager.Reader[2].ToString();
                    info.Name = info.Name;

                    info.Sex.ID = dataManager.Reader[3].ToString();

                    info.Birthday = NConvert.ToDateTime(dataManager.Reader[4].ToString());

                    info.PVisit.InTime = NConvert.ToDateTime(dataManager.Reader[5].ToString());

                    info.PVisit.PatientLocation.Dept.ID = dataManager.Reader[6].ToString();

                    info.PVisit.PatientLocation.Dept.Name = dataManager.Reader[7].ToString();

                    info.PVisit.PatientLocation.Bed.ID = dataManager.Reader[8].ToString();

                    info.PVisit.PatientLocation.Bed.Status.ID = dataManager.Reader[9].ToString();

                    info.PVisit.PatientLocation.NurseCell.ID = dataManager.Reader[10].ToString();

                    info.PVisit.PatientLocation.NurseCell.Name = dataManager.Reader[11].ToString();

                    info.PVisit.AdmittingDoctor.ID = dataManager.Reader[12].ToString(); //ҽʦ����(סԺ)

                    info.PVisit.AdmittingDoctor.Name = dataManager.Reader[13].ToString(); //ҽʦ����(סԺ)

                    info.PVisit.AttendingDoctor.ID = dataManager.Reader[14].ToString(); //ҽʦ����(����)

                    info.PVisit.AttendingDoctor.Name = dataManager.Reader[15].ToString(); //ҽʦ����(����)

                    info.PVisit.ConsultingDoctor.ID = dataManager.Reader[16].ToString(); //ҽʦ����(����)

                    info.PVisit.ConsultingDoctor.Name = dataManager.Reader[17].ToString(); //ҽʦ����(����)

                    info.PVisit.TempDoctor.ID = dataManager.Reader[18].ToString(); //ҽʦ����(ʵϰ)

                    info.PVisit.TempDoctor.Name = dataManager.Reader[19].ToString(); //ҽʦ����(ʵϰ)

                    info.PVisit.AdmittingNurse.ID = dataManager.Reader[20].ToString(); // ��ʿ����(����)

                    info.PVisit.AdmittingNurse.Name = dataManager.Reader[21].ToString(); // ��ʿ����(����)

                    info.Disease.Tend.Name = dataManager.Reader[22].ToString();

                    info.Disease.Memo = dataManager.Reader[23].ToString(); //��ʳ

                    info.Diagnoses.Add(dataManager.Reader[24].ToString()); //���

                    info.FT.TotCost = NConvert.ToDecimal(dataManager.Reader[25]);

                    info.FT.LeftCost = NConvert.ToDecimal(dataManager.Reader[26]);

                    info.PVisit.MoneyAlert = NConvert.ToDecimal(dataManager.Reader[27]);

                    info.FT.PrepayCost = NConvert.ToDecimal(dataManager.Reader[28]);//Ԥ����

                    info.PID.CardNO = dataManager.Reader[29].ToString();

                    info.Pact.ID = dataManager.Reader[30].ToString();

                    info.Pact.Name = dataManager.Reader[31].ToString();

                    al.Add(info);
                }
            }
            catch (Exception ex)
            {
                this.Msg = "��û��߻�����Ϣ����" + ex.Message;
            }
            finally
            {
                dataManager.Reader.Close();
            }

            return al;
        }

        #endregion

        #region IBase ��Ա

        private string msg;

        private int msgCode;

        public string Msg
        {
            get
            {
                return this.msg;
            }
            set
            {
                this.msg = value;
            }
        }

        public int MsgCode
        {
            get
            {
                return this.msgCode;
            }
            set
            {
                this.msgCode = value;
            }
        }

        #endregion

        #region IOutpatient ��Ա

        public FS.HISFC.Models.Registration.Register GetByClinic(string clinicNO)
        {
            FS.HISFC.Models.Registration.Register info = this.regManagment.GetByClinic(clinicNO);

            this.SetMsg(this.regManagment);

            return info;
        }

        /// <summary>
        /// ������Ϣ����
        /// </summary>
        /// <param name="patientNO">����ID</param>
        /// <param name="limitDate">��С����</param>
        /// <returns></returns>
        public System.Collections.ArrayList Query(string patientNO, DateTime limitDate)
        {
            System.Collections.ArrayList al = this.regManagment.Query(patientNO, limitDate);

            this.SetMsg(this.regManagment);

            return al;
        }

        public System.Collections.ArrayList QueryValidPatientsByName(string patientName)
        {
            System.Collections.ArrayList al = this.regManagment.QueryByName(patientName);

            this.SetMsg(this.regManagment);

            return al;
        }
        /// <summary>
        /// ���ݿ���ҽ����ѯ���ﲡ��
        /// </summary>
        /// <param name="docID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isSee"></param>
        /// <returns></returns>
        public ArrayList QueryBySeeDoc(string docID, DateTime beginDate, DateTime endDate, bool isSee)
        {
            return this.regManagment.QueryBySeeDoc(docID, beginDate, endDate, isSee);
        }
        /// <summary>
        /// ���ݿ���ҽ����ѯ���ﲡ��
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public ArrayList QueryRegister(string sql)
        {
            return this.regManagment.QueryRegister(sql);
        }
        #endregion

        /// <summary>
        /// ������ʾ��Ϣ
        /// </summary>
        /// <param name="dataManagment"></param>
        private void SetMsg(FS.FrameWork.Management.Database dataManagment)
        {
            this.Msg = dataManagment.Err;
            this.MsgCode = dataManagment.DBErrCode;
        }

        #region IInpatient ��Ա


        public FS.HISFC.Models.RADT.PatientInfo GetPatientInfomation(string inPatientNO)
        {
            FS.HISFC.Models.RADT.PatientInfo patient = this.inpatientManagment.GetPatientInfoByPatientNO(inPatientNO);

            this.SetMsg(this.inpatientManagment);

            return patient;
        }

        #endregion
    }
}
