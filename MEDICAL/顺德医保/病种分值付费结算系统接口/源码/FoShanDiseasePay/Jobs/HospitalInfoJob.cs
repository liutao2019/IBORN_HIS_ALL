using System;
using System.Data;
using System.Collections;
using System.Xml;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FoShanDiseasePay.Jobs
{
    /// <summary>
    /// OutpatientJob 的摘要说明。
    /// 医疗机构信息上传
    /// </summary>
    public class HospitalInfoJob : BaseJob
    {
        public HospitalInfoJob()
        {
        }

        /// <summary>
        /// 基本业务管理类
        /// </summary>
        private BizLogic.BaseManager baseMgr = new FoShanDiseasePay.BizLogic.BaseManager();

        public override void Start()
        {
            string error = string.Empty;
            try
            {
                Neusoft.HISFC.Models.Base.Hospital hospitalInfo = baseMgr.QueryHospitalInfo();

                int result = this.UploadHospitalInfo(hospitalInfo, ref error);
                if (result < 0)
                {
                    LogManager.WriteLog("上传医院基本信息出错!" + error);
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

        //4.3	上报医疗机构信息(HQ001)
        private int UploadHospitalInfo(Neusoft.HISFC.Models.Base.Hospital hospital, ref string error)
        {
            string data = string.Empty;

            string json = @"'ygHospitalID':'{0}','code':'{1}','buKind':'{2}','grade':'{3}','kind':'{4}','subjectFlag':'{5}',
                            'presider':'{6}','accountName':'{7}','bank':'{8}','account':'{9}','address':'{10}','postCode':'{11}',
                            'phone':'{12}','fax':'{13}','url':'{14}','provinceName':'{15}','cityName':'{16}','countyName':'{17}',
                            'orgDesc':'{18}','linkMan':'{19}','linkPhone':'{20}','linkEmail':'{21}','medicareFlag':'{22}',
                            'ownership':'{23}','featureClinic':'{24}','doctorNum':'{25}'";

            data = string.Format(json, hospital.SunProcureCode, hospital.HospitalCode, hospital.OperationNature,
                hospital.HospitalGrade, hospital.HospitalCategory, hospital.Affiliation, hospital.LegalRepresent,
                hospital.AccountName, hospital.AccountBank, hospital.Account, hospital.Address, hospital.ZipCode,
                hospital.Phone, hospital.Fax, hospital.NetAddress, hospital.ProvinceLocation, hospital.CityLocation,
                hospital.DistLocation, hospital.Remark, hospital.LinkMan, hospital.LinkTel, hospital.LinkMail,
                hospital.IsInSecurity ? "1" : "0", hospital.Ownership,  hospital.SpecialClinic,hospital.DoctNumber);

            return this.UploadInfo("HQ001", data, ref error);
        }

        //4.4	医疗机构信息查询(HQ001A)
        private int QueryHospitalInfo(string hospitalCode, ref string error)
        {
            string data = string.Empty;

            string json = @"'ygHospitalID':'{0}'";

            data = string.Format(json, hospitalCode).Replace("'", "\""); ;

            return this.UploadInfo("HQ001A", data, ref error);
        }
    }
}
