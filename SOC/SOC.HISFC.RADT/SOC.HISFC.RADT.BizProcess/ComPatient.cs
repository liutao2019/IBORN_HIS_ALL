using System;
using System.Collections.Generic;
using System.Linq;
using FS.SOC.HISFC.BizProcess.CommonInterface.Common;
using System.Text;
using FS.SOC.HISFC.InpatientFee.BizProcess;

namespace FS.SOC.HISFC.RADT.BizProcess
{
    /// <summary>
    /// [功能描述: 患者基本信息相关的逻辑业务类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-03]<br></br>
    /// <修改记录>
    /// </修改记录>
    /// </summary>
    public class ComPatient:AbstractBizProcess
    {
        /// <summary>
        /// 保存患者基本信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        /// {DBB05E6E-156D-417f-AAC5-ABFB78DFBA31}
        public int SavePatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return this.SavePatient(patient, false);
        }

        /// <summary>
        /// 保存患者基本信息（住院用）
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="isInpatient"></param>
        /// <returns></returns>
        public int SavePatient(FS.HISFC.Models.RADT.PatientInfo patient, bool isInpatient)
        {
            FS.SOC.HISFC.RADT.BizLogic.ComPatient patientMgr = new FS.SOC.HISFC.RADT.BizLogic.ComPatient();
            this.BeginTransaction();
            this.SetDB(patientMgr);
            if (patientMgr.InsertPatient(patient) < 0)
            {
                //先查询
                if (isInpatient)
                {
                    if (patientMgr.UpdatePatientForInpatient(patient) <= 0)
                    {
                        this.RollBack();
                        this.err = "保存患者基本信息错误，" + patientMgr.Err;
                        return -1;
                    }
                }
                else
                {
                    if (patientMgr.UpdatePatient(patient) <= 0)
                    {
                        this.RollBack();
                        this.err = "保存患者基本信息错误，" + patientMgr.Err;
                        return -1;
                    }
                }
            }

            this.Commit();

            return 1;
        }

        //{11E484F5-B92A-4903-8C8A-4920908B4D0A}
        public static string postCreateCrm(FS.HISFC.Models.RADT.PatientInfo patientinfo)
        {
            FS.SOC.HISFC.RADT.BizLogic.ComPatient patientMgr = new FS.SOC.HISFC.RADT.BizLogic.ComPatient();


            FS.HISFC.Models.Base.Employee empl = FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.Models.Base.Department dept = empl.Dept as FS.HISFC.Models.Base.Department;

            string hospitalCode = string.Empty;

            if (dept.HospitalName.Contains("顺德"))
            {
                hospitalCode = "IBORNSD";
            }
            else
            {
                hospitalCode = "IBORNGZ";
            }

            string url = FS.HISFC.BizProcess.Integrate.WSHelper.GetUrl(hospitalCode, FS.HISFC.BizProcess.Integrate.URLTYPE.HIS);
                

            string reqFormat = @"<req>
                            <hiscardid>{0}</hiscardid>
                            <idCardNo>{1}</idCardNo>
                            <hospitalId>{2}</hospitalId>
                            <gender>{3}</gender>
                            <phone>{4}</phone>
                            <birthday>{5}</birthday>
                            <contractPerson>{6}</contractPerson>
                            <contractPersonPhone>{7}</contractPersonPhone>
                            <gender>{8}</gender>
                            <remark>{9}</remark>
                            <childflag>{10}</childflag>
                            <source>{11}</source>
                            <name>{12}</name>
                        </req>";

            string hosCode = string.Empty;

            //{D6F8B61E-4738-47ec-A840-DBDB3AD5EFE8}
            switch (hospitalCode)
            {
                case "IBORNSD":
                    hosCode = "1";
                    break;
                case "IBORNGZ":
                    hosCode = "2";
                    break;
                case "IBORNCLINIC":
                    hosCode = "3";
                    break;
                default:
                    hosCode = "2";
                    break;
            }

            string hiscardid = patientinfo.PID.CardNO;
            string idCardNo = "";
            //从数据库取id
            string hospitalId = hosCode;
            string gender = patientinfo.Sex.Name;
            string phone = patientinfo.PhoneHome;
            string birthday = patientinfo.Birthday.ToString("yyyy-MM-dd");
            string contractPerson = patientinfo.Kin.Name;
            string contractPersonPhone = patientinfo.Kin.RelationPhone;
            string remark = "";
            string childflag = "1";
            string source = "inPatient";
            string name = patientinfo.Name;
            string req = string.Format(reqFormat, hiscardid, idCardNo, hospitalId, gender, phone, birthday, contractPerson, contractPersonPhone,gender, remark, childflag, source, name);

            //wshelper公用方法升级了，测试库url为空，则不进行处理{1EC9ECE5-0E63-4073-B653-E668031E2DF1}
            if (url==null||url == "")
            {
                return "0";
            }

            //FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService();
            //string resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService("http://localhost:8080/IbornMobileService.asmx", "postInpatientCrm", new string[] { req }) as string;
            string resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService(url, "postInpatientCrm", new string[] { req }) as string;

            

            return "1";
        }
    }
}
