using System;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using FoShanDiseasePay.DataBase;
using FoShanDiseasePay.Common;

namespace FoShanDiseasePay.Jobs
{
    /// <summary>
    /// Job����
    /// </summary>
    public class BaseJob
    {

        protected string errMsg = string.Empty;

        //���ݿ����sql
        protected string sql = @"INSERT INTO COM_FoShanSI_DETAIL
                                  (BUSINESS_TYPE,   VISITINGSERIALNUMBER,   STARTDATE,
                                   ENDDATE,   OPER_FLAG,   OPER_DATE)
                                VALUES
                                  ('{0}',   '{1}',   TO_DATE('{2}', 'yyyy-MM-dd'),
                                   TO_DATE('{3}', 'yyyy-MM-dd'),   '{4}',   SYSDATE)";

        public string startTime = string.Empty;

        public string endTime = string.Empty;

        public BaseJob()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        public virtual void Start()
        {

        }

        public virtual void Test(string id)
        {

        }

        protected string GetConvertValue(string type, string str)
        {
            foreach (Neusoft.FrameWork.Models.NeuObject obj in Manager.setObj.ContentList)
            {
                if (obj.ID.Trim().ToUpper() == type.ToUpper().Trim())
                {
                    if (obj.Name.ToUpper() == str.Trim().ToUpper())
                    {
                        return obj.Memo.Trim();
                    }
                }
            }
            return str;
        }

        protected object GetConvertDate(object obj)
        {
            if (obj == null || Convert.IsDBNull(obj))
            {
                return obj;
            }
            if (Convert.ToDateTime(obj) == DateTime.MinValue)
            {
                return Convert.DBNull;
            }
            return obj;
        }

        /// <summary>
        /// �ϴ����ѯ
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="inxml"></param>
        /// <returns>1�ɹ�0ʧ��</returns>
        public int UploadInfo(string funCode, string cdata, ref string error)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                sb.Append("\"hApiCode\":\"").Append(funCode).Append("\",");
                sb.Append("\"hPlatCode\":\"hApi\",");
                sb.Append("\"hUserCode\":\"").Append(Manager.setObj.UserCode).Append("\",");
                sb.Append("\"hUserOrgCode\":\"").Append(Manager.setObj.OrgCode).Append("\",");
                sb.Append("\"hUserIP\":\"").Append(Manager.setObj.IPAddress).Append("\",");
                if (funCode == "HD002" || funCode == "HH002")
                {
                    sb.Append("\"hData\":").Append(cdata.Replace("'", "\"")).Append(",");
                }
                else
                {
                    sb.Append("\"hData\":").Append("{").Append(cdata.Replace("'", "\"")).Append("},");
                }
                sb.Append("\"hSignData\":\"").Append(Manager.setObj.Sign).Append("\"}");

                string json = sb.ToString();//.Replace("{[", "[").Replace("]}", "]");
                sb = null;

                LogManager.WriteLog("���÷�����" + funCode + "�������Ϊ��" + json);

                #region ����

                //string[] data = {
                //                 "\"hApiCode\":\"" + funCode + "\"",
                //                 "\"hPlatCode\":\"hApi\"",
                //                 "\"hUserCode\":\"" + Manager.setObj.UserCode + "\"",
                //                 "\"hUserOrgCode\":\"" + Manager.setObj.OrgCode + "\"",
                //                 "\"hUserIP\":\"" + Manager.setObj.IPAddress + "\"",
                //                 "\"hData\":" + cdata,
                //                 "\"hSignData\":\"" + Manager.setObj.Sign + "\""
                //                };
                //string postData = string.Empty;
                //StringBuilder sb = new StringBuilder();
                //sb.Append('{');
                //foreach (string item in data)
                //{
                //    sb.Append(item).Append(",");
                //    //postData += item + ",";
                //}
                //postData = sb.ToString().TrimEnd(',');
                //postData += "}";
                //sb = null;

                #endregion

                //1��ֱ�ӵ���ҽ���ӿ�
                //Object obj = Common.WebAPIHelper.InvokeService(Manager.setObj.WebServiceAddress, json, ref error);

                //2������ҽԺǰ�û�����
                string[] args = { json };
                Object obj = WebServiceHelper.InvokeWebService(Manager.setObj.WebServiceAddress, "SendReveForHospital", args, ref error);

                if (obj == null)
                {
                    return 0;
                }

                LogManager.WriteLog("���÷�����" + funCode + "���ص���ϢΪ��" + obj.ToString());

                Newtonsoft.Json.Linq.JObject jo = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(obj.ToString());

                if (jo["Code"].ToString() == "EMI_R_00")
                {
                    return 1;
                }

                if (string.IsNullOrEmpty(error))
                {
                    error = obj.ToString();   //������Ϣ
                }
                return -1;
            }
            catch (Exception e)
            {
                error = e.Message;
                return -1;
            }

            return 1;
        }

    }
}
