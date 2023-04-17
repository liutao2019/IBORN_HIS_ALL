using System;
using System.Data;
using System.Collections;
using System.Xml;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using FoShanDiseasePay.DataBase;
using FS.FrameWork.Function;

namespace FoShanDiseasePay.Jobs
{
    /// <summary>
    /// 医生信息上传  刚开始上传所有医生
    /// 之后根据com_employee表的操作时间来进行上传
    /// </summary>
    public class DoctInfoJob : BaseJob
    {
        public DoctInfoJob()
        {
        }

        /// <summary>
        /// 门诊业务类
        /// </summary>
        private BizLogic.OutManager outMgr = new FoShanDiseasePay.BizLogic.OutManager();

        /// <summary>
        /// 住院业务类
        /// </summary>
        private BizLogic.InManager inMgr = new FoShanDiseasePay.BizLogic.InManager();

        public override void Start()
        {
            string error = string.Empty;

            DateTime dtBegin = NConvert.ToDateTime(this.startTime);
            DateTime dtEnd = NConvert.ToDateTime(this.endTime);
                
            try
            {
                ArrayList al = this.inMgr.QueryDoctorInfo(dtBegin, dtEnd);
                if (al == null)
                {
                    LogManager.WriteLog(this.inMgr.Err);
                    return;
                }

                int result = 0;
                foreach (FS.HISFC.Models.Base.Employee emp in al)
                {
                    result = this.UploadDoctInfo(emp, ref error);

                    if (result <= 0)
                    {
                        LogManager.WriteLog(emp.ID + " " + emp.Name + "上传出错@" + error);
                    }
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message);
            }
            finally
            {

            }
        }

        //5.4	上传医师信息(HK001)
        private int UploadDoctInfo(FS.HISFC.Models.Base.Employee emp, ref string error)
        {
            string jsonTmp = @"'ysjgbm':'{0}','ysjgmc':'{1}','ysbm':'{2}','xm':'{3}','xb':'{4}','gmsfhm':'{5}','lxdh':'{6}',
                        'lxsj':'{7}','szks':'{8}','gh':'{9}','gwzt':'{10}','zyjszg':'{11}','zyjszgsj':'{12}',
                        'zyzglb':'{13}','zyzgsj':'{14}','kscj':'{15}','zgzs':'{16}','zgzssj':'{17}','zyzsh':'{18}',
                        'zyzssj':'{19}','zylb':'{20}','zyjszc':'{21}','sfld':'{22}','zyfw':'{23}','zydd':'{24}',
                        'cxdakf':'{25}','zjlnwg':'{26}','cxdaklb':'{27}','yycsyj':'{28}','yycssj':'{29}',
                        'jbjgshyj':'{30}','jbjgshsj':'{31}','shzt':'{32}','bz':'{33}'";

            string json = string.Empty;
            if (emp.Sex.Name == "男")
            {
                emp.Sex.Memo = "1";
            }
            else
            {
                emp.Sex.Memo = "2";
            }
            string dutyName = "";
            if (!string.IsNullOrEmpty(emp.Duty.Name))
            {
                switch (emp.Duty.Name)
                {
                    case "09":
                        dutyName = "1";
                        break;
                    case "10":
                        dutyName = "2";
                        break;
                    case "11":
                        dutyName = "3";
                        break;
                    case "13":
                        dutyName = "4";
                        break;
                    default:
                        dutyName = "5";

                        break;
                }
                //cmbMaritalStatus.Tag = info.PatientInfo.MaritalStatus.ID;
            }

            json = string.Format(jsonTmp, Manager.setObj.HospitalID, Manager.setObj.HospitalName, emp.ID, emp.Name,
                emp.Sex.Memo, "", "", "", emp.Dept.Name, emp.ID.TrimStart('0'), "1", dutyName, "", "", "", "", "", "", "1", "", "2",
                dutyName, "", "", "", "", "", "", "", "", "", "", "1", "").Replace("'", "\"");

            return base.UploadInfo("HK001", json, ref error);
        }

        //5.5	医师信息作废(HK002)
        private int CancelDoctInfo(string doctCode)
        {
            string json = @"{'ysjgbm':'{0}','ysbm':'{1}'}";

            string error = string.Empty;

            json = string.Format(json, Manager.setObj.HospitalID, doctCode).Replace("'", "\"");

            int result = base.UploadInfo("HK002", json, ref error);

            return result;
        }

        //5.6	医师信息查询(HK003)
        private int QueryDoctInfo(string doctCode)
        {
            string json = @"{'ysjgbm':'{0}',ysbm:'{1}'}";

            string error = string.Empty;

            json = string.Format(json, Manager.setObj.HospitalID, doctCode).Replace("'", "\"");

            int result = base.UploadInfo("HK003", json, ref error);

            return result;
        }

        #region 新接口

        public void StartNew()
        {
            string error = string.Empty;

            DateTime dtBegin = NConvert.ToDateTime(this.startTime);
            DateTime dtEnd = NConvert.ToDateTime(this.endTime);

            try
            {
                ArrayList al = this.inMgr.QueryDoctorInfoNew(dtBegin, dtEnd);
                if (al == null)
                {
                    LogManager.WriteLog(this.inMgr.Err);
                    return;
                }

                int result = 0;
                foreach (FS.HISFC.Models.Base.Employee emp in al)
                {
                    result = this.UploadDoctInfoNew(emp, ref error);

                    if (result <= 0)
                    {
                        LogManager.WriteLog(emp.ID + " " + emp.Name + "上传出错@" + error);
                    }
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message);
            }
            finally
            {

            }
        }

        //5.4	上传医师信息(HK001)
        private int UploadDoctInfoNew(FS.HISFC.Models.Base.Employee emp, ref string error)
        {
            string jsonTmp = @"'ysjgbm':'{0}','ysjgmc':'{1}','ysbm':'{2}','xm':'{3}','xb':'{4}','gmsfhm':'{5}','lxdh':'{6}',
                        'lxsj':'{7}','szks':'{8}','gh':'{9}','gwzt':'{10}','zyjszg':'{11}','zyjszgsj':'{12}',
                        'zyzglb':'{13}','zyzgsj':'{14}','kscj':'{15}','zgzs':'{16}','zgzssj':'{17}','zyzsh':'{18}',
                        'zyzssj':'{19}','zylb':'{20}','zyjszc':'{21}','sfld':'{22}','zyfw':'{23}','zydd':'{24}',
                        'cxdakf':'{25}','zjlnwg':'{26}','cxdaklb':'{27}','yycsyj':'{28}','yycssj':'{29}',
                        'jbjgshyj':'{30}','jbjgshsj':'{31}','shzt':'{32}','bz':'{33}'";

            StringBuilder json = new StringBuilder();
            if (emp.Sex.Name == "男")
            {
                emp.Sex.Memo = "1";
            }
            else
            {
                emp.Sex.Memo = "2";
            }
            string dutyName = "";
            if (!string.IsNullOrEmpty(emp.Duty.Name))
            {
                switch (emp.Duty.Name)
                {
                    case "09":
                        dutyName = "1";
                        break;
                    case "10":
                        dutyName = "2";
                        break;
                    case "11":
                        dutyName = "3";
                        break;
                    case "13":
                        dutyName = "4";
                        break;
                    default:
                        dutyName = "5";

                        break;
                }
                //cmbMaritalStatus.Tag = info.PatientInfo.MaritalStatus.ID;
            }
            if (emp.EmployeeType.ID.ToString() == "D")
            {

            }
            json.Append(string.Format(jsonTmp, Manager.setObj.HospitalID, Manager.setObj.HospitalName, emp.ID, emp.Name,//3
                emp.Sex.Memo, "", "", "", emp.Dept.ID, emp.ID.TrimStart('0'), "1", //10
                dutyName, "", "1", "", "", "", "", "1", "", "2",//20
                dutyName, "0", "", "", "", "", "", "", "", "",//30
                "", "1", "").Replace("'", "\""));

            return base.UploadInfoNew("HK001", json,false, ref error);
        }
        #endregion
    }
}
