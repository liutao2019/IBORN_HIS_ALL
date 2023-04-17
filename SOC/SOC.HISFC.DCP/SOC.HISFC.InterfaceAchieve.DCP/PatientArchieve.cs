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
        /// 住院患者信息管理类
        /// </summary>
        private FS.HISFC.BizLogic.RADT.InPatient inpatientManagment = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 门诊患者管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regManagment = new FS.HISFC.BizLogic.Registration.Register();

        #region IInpatient 成员

        public System.Collections.ArrayList GetPatientInfoByPatientNOAll(string patientNO)
        {
            System.Collections.ArrayList al = this.inpatientManagment.QueryInpatientNOByPatientNO( patientNO );
            if (al == null)
            {
                this.SetMsg( this.inpatientManagment );
                return null;
            }

            ArrayList alInpatient = null;
            this.msg = "未找到对应该住院（病历）号的患者住院记录";

            foreach (FS.FrameWork.Models.NeuObject info in al)
            {
                //if (info.Memo == "I")           //在院患者
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
        /// 根据sqlwhere条件查询患者信息集合
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

                    info.PVisit.AdmittingDoctor.ID = dataManager.Reader[12].ToString(); //医师代码(住院)

                    info.PVisit.AdmittingDoctor.Name = dataManager.Reader[13].ToString(); //医师姓名(住院)

                    info.PVisit.AttendingDoctor.ID = dataManager.Reader[14].ToString(); //医师代码(主治)

                    info.PVisit.AttendingDoctor.Name = dataManager.Reader[15].ToString(); //医师姓名(主治)

                    info.PVisit.ConsultingDoctor.ID = dataManager.Reader[16].ToString(); //医师代码(主任)

                    info.PVisit.ConsultingDoctor.Name = dataManager.Reader[17].ToString(); //医师姓名(主任)

                    info.PVisit.TempDoctor.ID = dataManager.Reader[18].ToString(); //医师代码(实习)

                    info.PVisit.TempDoctor.Name = dataManager.Reader[19].ToString(); //医师姓名(实习)

                    info.PVisit.AdmittingNurse.ID = dataManager.Reader[20].ToString(); // 护士代码(责任)

                    info.PVisit.AdmittingNurse.Name = dataManager.Reader[21].ToString(); // 护士姓名(责任)

                    info.Disease.Tend.Name = dataManager.Reader[22].ToString();

                    info.Disease.Memo = dataManager.Reader[23].ToString(); //饮食

                    info.Diagnoses.Add(dataManager.Reader[24].ToString()); //诊断

                    info.FT.TotCost = NConvert.ToDecimal(dataManager.Reader[25]);

                    info.FT.LeftCost = NConvert.ToDecimal(dataManager.Reader[26]);

                    info.PVisit.MoneyAlert = NConvert.ToDecimal(dataManager.Reader[27]);

                    info.FT.PrepayCost = NConvert.ToDecimal(dataManager.Reader[28]);//预交金

                    info.PID.CardNO = dataManager.Reader[29].ToString();

                    info.Pact.ID = dataManager.Reader[30].ToString();

                    info.Pact.Name = dataManager.Reader[31].ToString();

                    al.Add(info);
                }
            }
            catch (Exception ex)
            {
                this.Msg = "获得患者基本信息出错！" + ex.Message;
            }
            finally
            {
                dataManager.Reader.Close();
            }

            return al;
        }

        #endregion

        #region IBase 成员

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

        #region IOutpatient 成员

        public FS.HISFC.Models.Registration.Register GetByClinic(string clinicNO)
        {
            FS.HISFC.Models.Registration.Register info = this.regManagment.GetByClinic(clinicNO);

            this.SetMsg(this.regManagment);

            return info;
        }

        /// <summary>
        /// 患者信息检索
        /// </summary>
        /// <param name="patientNO">患者ID</param>
        /// <param name="limitDate">最小期限</param>
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
        /// 根据看诊医生查询门诊病人
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
        /// 根据看诊医生查询门诊病人
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public ArrayList QueryRegister(string sql)
        {
            return this.regManagment.QueryRegister(sql);
        }
        #endregion

        /// <summary>
        /// 设置提示信息
        /// </summary>
        /// <param name="dataManagment"></param>
        private void SetMsg(FS.FrameWork.Management.Database dataManagment)
        {
            this.Msg = dataManagment.Err;
            this.MsgCode = dataManagment.DBErrCode;
        }

        #region IInpatient 成员


        public FS.HISFC.Models.RADT.PatientInfo GetPatientInfomation(string inPatientNO)
        {
            FS.HISFC.Models.RADT.PatientInfo patient = this.inpatientManagment.GetPatientInfoByPatientNO(inPatientNO);

            this.SetMsg(this.inpatientManagment);

            return patient;
        }

        #endregion
    }
}
