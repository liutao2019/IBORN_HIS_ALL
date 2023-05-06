using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
//using System.ServiceProcess;
using Newtonsoft.Json;//先引入这两个命名空间
using Newtonsoft.Json.Converters;
using System.Text;
using OADAL;
using System.Collections.Specialized;
using System.Net;
using System.IO;

namespace OADeal
{
    public partial class OAService
    {

        private static System.IO.TextWriter output;

        Dictionary<string, string> cache = new Dictionary<string, string>();

        public OAService()
        {
            //InitializeComponent();
        }

        public void GetOADataAndInsert(int dateBefore,int version)
        {
            //LogHelper.Write("开始抽取数据");
            List<FormInfo> listFeePaid = new List<FormInfo>();
            List<FormInfo> listFeeLend = new List<FormInfo>();
            List<FormInfo> listFeePay = new List<FormInfo>();
            List<FormInfo> listFeeTransfer = new List<FormInfo>();
            LoadID(listFeePaid, listFeeLend, listFeePay, listFeeTransfer,version);
            //string startDate = DateTime.Now.AddDays(-10).Date.ToString("yyyy-MM-dd HH:mm:ss");
            //string endDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59";
            string startDate = DateTime.Now.AddDays(dateBefore*(-1)).Date.ToString("yyyy-MM-dd HH:mm:ss");
            string endDate = DateTime.Now.AddDays(dateBefore * (-1)).ToString("yyyy-MM-dd") + " 23:59:59";
            List<Dictionary<string, List<string>>> listFee = new List<Dictionary<string, List<string>>>();

            output = System.IO.File.AppendText(System.Environment.CurrentDirectory + "\\oa.log");

            output.WriteLine("\n" + DateTime.Now.ToString() + "开始获取费用报销申请");
            output.Close();

            OADataDeal oaDAL = new OADataDeal();
            
            //费用报销申请
            foreach (FormInfo model in listFeePaid)
            {
                List<Dictionary<string, List<string>>> list = GetFormData(startDate, endDate, model);
                listFee = listFee.Concat(list).ToList();
            }
            //OADataDeal oaDAL = new OADataDeal();
            oaDAL.InsertExpenseFee(listFee);
            listFee.Clear();

            //费用借支申请
            foreach (FormInfo model in listFeeLend)
            {
                List<Dictionary<string, List<string>>> list = GetFormData(startDate, endDate, model);
                listFee = listFee.Concat(list).ToList();
            }
            oaDAL.InsertLoanFee(listFee);
            listFee.Clear();
            
            //费用付款申请
            foreach (FormInfo model in listFeePay)
            {
                List<Dictionary<string, List<string>>> list = GetFormData(startDate, endDate, model);
                listFee = listFee.Concat(list).ToList();
            }
            oaDAL.InsertPaymentFee(listFee);
            

            //资金调拨申请，资金调拨属于付款申请，所以先用付款申请同一个数据库
            foreach (FormInfo model in listFeeTransfer)
            {
                List<Dictionary<string, List<string>>> list = GetFormData(startDate, endDate, model);
                listFee = listFee.Concat(list).ToList();
            }
            oaDAL.InsertPaymentFee(listFee);
        }

        private void LoadID(List<FormInfo> listFeePaid, List<FormInfo> listFeeLend, List<FormInfo> listFeePay, List<FormInfo> listFeeTransfer,int version)
        {
            FormInfo formFee = new FormInfo();
            formFee.FormType = "费用报销申请";
            formFee.FormTypeID = "1";
            formFee.HospitalID = "IBORN";//defn5193225f8209476fbc8caa21ed7642f9
            formFee.FormID = getDefinitionIdByFormId("b2b0d97d-cd5b-461a-8503-09b425829b1b", version);
//            formFee.FormID = "defn5193225f8209476fbc8caa21ed7642f9";// "defn5193225f8209476fbc8caa21ed7642f9";//"defn58410a4d40f341d18fc9e1bf2274328a"
            listFeePaid.Add(formFee);

            FormInfo formFee1 = new FormInfo();
            formFee1.FormType = "费用报销申请";
            formFee1.FormTypeID = "1";
            formFee1.HospitalID = "BELLAIRE";//defn7d5c60c4da594450bd29dfc8c9818cf0
            formFee1.FormID = getDefinitionIdByFormId("form2d4d8561608448bcb3330999c8e4a241", version);
//            formFee1.FormID = "defn7d5c60c4da594450bd29dfc8c9818cf0";// "defnbc7c157e6bb54ddb95b4111d42c4a406";//defnbc7c157e6bb54ddb95b4111d42c4a406
            listFeePaid.Add(formFee1);

            FormInfo formFee2 = new FormInfo();
            formFee2.FormType = "费用报销申请";
            formFee2.FormTypeID = "1";
            formFee2.HospitalID = "SDIBORN";//defnb7b76c868efc450fa7a44460e4f3e5ed
            formFee2.FormID = getDefinitionIdByFormId("formcdcc34b8e2644d208de1ac3bfcab8a93", version);
//            formFee2.FormID = "defnb7b76c868efc450fa7a44460e4f3e5ed";// "defn49f4479475c144a5821b413b529c7332"; //"defn5164b07267af4e64903d1a18a635fb90";//defn49f4479475c144a5821b413b529c7332
            listFeePaid.Add(formFee2);

            FormInfo formFee3 = new FormInfo();
            formFee3.FormType = "费用报销申请";
            formFee3.FormTypeID = "1";
            formFee3.HospitalID = "SDIBORNCLINIC";
            formFee3.FormID = getDefinitionIdByFormId("form8b70901f8abb436894862a927f089f29", version);
            listFeePaid.Add(formFee3);

            ///借支申请
            FormInfo formLendFee = new FormInfo();
            formLendFee.FormType = "费用借支申请";
            formLendFee.FormTypeID = "2";
            formLendFee.HospitalID = "IBORN";
            formLendFee.FormID = getDefinitionIdByFormId("6f0d21b2-5766-4118-8059-bccf1a70cd73", version);
//            formLendFee.FormID = "defn302292a3e86544f28b7c5487efd7c189"; //"defna2c1f8213e8548bf93685545ecb9526e";
            listFeeLend.Add(formLendFee);

            FormInfo formLendFee1 = new FormInfo();
            formLendFee1.FormType = "费用借支申请";
            formLendFee1.FormTypeID = "2";
            formLendFee1.HospitalID = "BELLAIRE";
            formLendFee1.FormID = getDefinitionIdByFormId("form01c70c6a4bbf4078b43d0dd7892c66fa", version);
//            formLendFee1.FormID = "defnedb9e749540743c5bf16b40afe2907cd"; //"defnc4d1e7655a054da18c5b5557c1f033d1";
            listFeeLend.Add(formLendFee1);


            FormInfo formLendFee2 = new FormInfo();
            formLendFee2.FormType = "费用借支申请";
            formLendFee2.FormTypeID = "2";
            formLendFee2.HospitalID = "SDIBORN";
            formLendFee2.FormID = getDefinitionIdByFormId("formf8cc5b284a404e71b70b0bd9c1a7f723", version);
//            formLendFee2.FormID = "defn066ab9ed2a2e44dc8aa66181a73295e6"; //"defn5aea6dbe6c9c4a5fb2270fca39655acd"; //"088ff860-10a2-41e8-afee-2975e26a0946";
            listFeeLend.Add(formLendFee2);

            FormInfo formLendFee3 = new FormInfo();
            formLendFee3.FormType = "费用借支申请";
            formLendFee3.FormTypeID = "2";
            formLendFee3.HospitalID = "SDIBORNCLINIC";
            formLendFee3.FormID = getDefinitionIdByFormId("form6664b0cfd2bc47a8b59c45ec6ac67763", version);
            listFeeLend.Add(formLendFee3);

            /////工程类付款申请，目前不用工程类付款申请
            //FormInfo formProjectPayFee = new FormInfo();
            //formProjectPayFee.FormType = "工程费用付款申请";
            //formProjectPayFee.FormTypeID = "5";
            //formProjectPayFee.HospitalID = "IBORN";//defn5abce6e2e586469786aca3b1bbce023d
            //formProjectPayFee.FormID = "defnef51abc909ac4175a64b62c50e013644";// "defnd5b6df3c3d9a4c89b01b43163e8aee16"; //"defn58948e36a0cb4aa291c6f12902bc955e";
            //formProjectPayFee.FormID = getDefinitionIdByFormId("d56b0b3b-238a-468a-abcc-0a17c47a8fd8", version);
            //listFeePay.Add(formProjectPayFee);

            //FormType formProjectPayFee1 = new FormType();
            //formProjectPayFee1.FormType = "工程费用付款申请";
            //formProjectPayFee1.HospitalID = "BELLAIRE";
            //formProjectPayFee1.FormID = "defn58948e36a0cb4aa291c6f12902bc955e";
            //listFeePay.Add(formProjectPayFee1);

            //FormInfo formProjectPayFee2 = new FormInfo();
            //formProjectPayFee2.FormType = "工程费用付款申请";
            //formProjectPayFee2.FormTypeID = "5";
            //formProjectPayFee2.HospitalID = "SDIBORN";
            //formProjectPayFee2.FormID = "aabe71b4-56b1-45ac-ba91-54172de3edad";
            //listFeePay.Add(formProjectPayFee2);


            /////付款申请
            FormInfo formPayFee = new FormInfo();
            formPayFee.FormType = "费用付款申请";
            formPayFee.FormTypeID = "3";
            formPayFee.HospitalID = "IBORN";//defne6567efacf524aff9c7884a22a888038
            formPayFee.FormID = getDefinitionIdByFormId("eb98fcdd-3a03-436e-b41a-0016d6cb665a", version);
//            formPayFee.FormID = "defn50460a96051c4cde9529d288747cdcee";// "defnc518c63ff57a474198ec225f4d88b30c"; //"4cdd2769-bced-4c2d-8665-cd6a53b1520f";
            listFeePay.Add(formPayFee);

            FormInfo formPayFee1 = new FormInfo();
            formPayFee1.FormType = "费用付款申请";
            formPayFee1.FormTypeID = "3";
            formPayFee1.HospitalID = "BELLAIRE";
            formPayFee1.FormID = getDefinitionIdByFormId("form9e1bed8151a64846bc7e8f1701c38310", version);
//            formPayFee1.FormID = "defnc606f23a5ff34ace9e6184dfc8695829";// "defn7800bb4c8191424cae8683d25b794b81";
            listFeePay.Add(formPayFee1);

            FormInfo formPayFee2 = new FormInfo();
            formPayFee2.FormType = "费用付款申请";
            formPayFee2.FormTypeID = "3";
            formPayFee2.HospitalID = "SDIBORN";//defn760a1ae39ed349c3a1a553c0da896673
            formPayFee2.FormID = getDefinitionIdByFormId("form30e4c0794c27405eab9f8e2cc9e1ea16", version);
            listFeePay.Add(formPayFee2);

            FormInfo formPayFee3 = new FormInfo();
            formPayFee3.FormType = "费用付款申请";
            formPayFee3.FormTypeID = "3";
            formPayFee3.HospitalID = "SDIBORNCLINIC";//defn760a1ae39ed349c3a1a553c0da896673
            formPayFee3.FormID = getDefinitionIdByFormId("forme898b75017a247a2b94fa5949a54d8ca", version);
            listFeePay.Add(formPayFee3);

            /////付款申请
            FormInfo formBuyPayFee = new FormInfo();
            formBuyPayFee.FormType = "采购费用付款申请";
            formBuyPayFee.FormTypeID = "4";
            formBuyPayFee.HospitalID = "IBORN";//defnb1d5244357e44d68be9200cd2e4ae657
            formBuyPayFee.FormID = getDefinitionIdByFormId("formb50a28ec67944b57a487aa827b53002f", version); //"defn1e1d842b81d54c0db30ab0b78fda30ab"; //"defn2f2eeba4541e48e9a7aa4c3954a89030";
            listFeePay.Add(formBuyPayFee);

            /////资金调拨申请，资金划拨有5个版本，最近两个是defn34fa4987615347508c2494e582b613d5，defne2eca4300f58437a8406feda7821cb6f
            //资金划拨的院区不在这里进行拆分，而是通过后期插入数据库的逻辑层进行判断
            FormInfo formTransferFee = new FormInfo();
            formTransferFee.FormType = "资金调拨申请";
            formTransferFee.FormTypeID = "5";
            formTransferFee.HospitalID = "IBORN";//
            formTransferFee.FormID = getDefinitionIdByFormId("7eb0a69a-2167-4e44-a2b8-7fcc8f01cd62", version);
            listFeeTransfer.Add(formTransferFee);
        }

        private List<Dictionary<string, List<string>>> GetFormData(string startDate, string endDate, FormInfo fm)
        {
            string tocked = getTocked();
            string url = " https://qwif.do1.com.cn/qwcgi/api/apiFormExportAction!getFormDetails.action?token=" + tocked + "&corpId=ww5657a30e3f4e3ca3";
            NameValueCollection stringDict = new NameValueCollection();
            stringDict.Add("data", "{\"pageNO\":1,\"pageSize\":100,startDate:\"" + startDate + "\",endDate:\"" + endDate + "\",definitionId:\"" + fm.FormID + "\"}");
            string result = HttpPostForm(url, stringDict);
            List<Dictionary<string, List<string>>> listAll = new List<Dictionary<string, List<string>>>();
            if (string.IsNullOrEmpty(result)) return listAll;

            
            object obj = JsonConvert.DeserializeObject(result);
            Newtonsoft.Json.Linq.JObject js = obj as Newtonsoft.Json.Linq.JObject;//把上面的obj转换为 Jobject对象

           // Newtonsoft.Json.Linq.JToken jsonModel = js["data"];//取Jtoken对象     通过Jobject的索引获得到

            Newtonsoft.Json.Linq.JToken jsonModel = js;//取Jtoken对象     通过Jobject的索引获得到


            //dynamic jsonModel = Newtonsoft.Json.Linq.JToken.Parse(result) as dynamic;

            if (jsonModel["code"].ToString() == "22159" || jsonModel["code"].ToString() == "22160")
            {
                return listAll;
            }
            //((Newtonsoft.Json.Linq.JContainer)arr).Count;
            //jsonModel["data"]["pageData"]["Count"].Count;

            String ss =jsonModel["data"].ToString();
            if (!ss.Contains("pageData"))
            {
                return listAll;
            }
            Newtonsoft.Json.Linq.JArray jArr = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(jsonModel["data"]["pageData"].ToString());

            for (int i = 0; i < jArr.Count; i++)
            {

                string isover = jsonModel["data"]["pageData"][i]["isover"].ToString();
                if (isover != "1")
                {
                    continue;
                }
                string applyName = jsonModel["data"]["pageData"][i]["approvers"].ToString();
                Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
                dic.Add("Id", new List<string>());
                string id = jsonModel["data"]["pageData"][i]["id"].ToString();
                dic["Id"].Add(id);

                dic.Add("closePerson", new List<string>());
                string closePerson = jsonModel["data"]["pageData"][i]["closePerson"].ToString();
                dic["closePerson"].Add(closePerson);

                dic.Add("closeTime", new List<string>());
                string closeTime = jsonModel["data"]["pageData"][i]["closeTime"].ToString();
                dic["closeTime"].Add(closeTime);

                string deptname = jsonModel["data"]["pageData"][i]["departmentName"].ToString();

                dic.Add("instanceTitle", new List<string>());
                string instanceTitle = jsonModel["data"]["pageData"][i]["instanceTitle"].ToString();
                dic["instanceTitle"].Add(instanceTitle);
                // string personName = jsonModel.data.pageData[i].personName;
                List<string> listPerson = new List<string>();

                string personName = jsonModel["data"]["pageData"][i]["personName"].ToString();
                listPerson.Add(personName);
                dic.Add("personName", listPerson);

                string createTime = jsonModel["data"]["pageData"][i]["createaTime"].ToString();
                dic.Add("createTime", new List<string>());
                dic["createTime"].Add(createTime);
                //(Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(jsonModel["data"]["pageData"][i]["formItemDefinitionsVO"].ToString()).Count;
                Newtonsoft.Json.Linq.JArray jArrzicaidan =(Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(jsonModel["data"]["pageData"][i]["formItemDefinitionsVO"].ToString());
                if (jArrzicaidan.Count > 0)//有子菜单
                {
                    Newtonsoft.Json.Linq.JArray jArritemvo = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(jsonModel["data"]["pageData"][i]["formItemDefinitionsVO"].ToString());
                    for (int j = 0; j < jArritemvo.Count; j++)
                    {
                        //(Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(jsonModel["data"]["pageData"][i]["formItemDefinitionsVO"][j]["childItemDefinitions"].ToString()).Count
                        //if (Convert.ToInt32(jsonModel["data"]["pageData"][i]["formItemDefinitionsVO"][j]["childItemDefinitions"]["Count"]) > 0)
                        Newtonsoft.Json.Linq.JArray childItemDefinitions = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(jsonModel["data"]["pageData"][i]["formItemDefinitionsVO"][j]["childItemDefinitions"].ToString());
                        if (childItemDefinitions.Count > 0)
                        {
                            //Convert.ToInt32(jsonModel["data"]["pageData"][i]["formItemDefinitionsVO"][j]["childItemDefinitions"]["Count"])
                            //for (int k = 0; k < Convert.ToInt32(jsonModel["data"]["pageData"][i]["formItemDefinitionsVO"][j]["childItemDefinitions"]["Count"]); k++)           
                            for (int k = 0; k < childItemDefinitions.Count; k++)
                            {
                                List<string> listval = new List<string>();

                                string key = jsonModel["data"]["pageData"][i]["formItemDefinitionsVO"][j]["childItemDefinitions"][k]["itemName"].ToString();
                                Newtonsoft.Json.Linq.JArray jrrayValueStr = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(jsonModel["data"]["pageData"][i]["formItemDefinitionsVO"][j]["childItemDefinitions"][k]["valueStrs"].ToString());
                                for (int l = 0; l < jrrayValueStr.Count; l++)
                                {
                                    string value = jsonModel["data"]["pageData"][i]["formItemDefinitionsVO"][j]["childItemDefinitions"][k]["valueStrs"][l]["valueStr"].ToString();
                                    listval.Add(value);
                                }
                                if (dic.ContainsKey(key))
                                {
                                    key = "出纳核定" + key;
                                }
                                dic.Add(key, listval);
                            }
                        }
                        else
                        {
                            string itemname = jsonModel["data"]["pageData"][i]["formItemDefinitionsVO"][j]["itemName"].ToString();
                            string value = string.Empty;
                            Newtonsoft.Json.Linq.JArray jrrayformItemDefinitionsVO = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(jsonModel["data"]["pageData"][i]["formItemDefinitionsVO"][j]["valueStrs"].ToString());
                            if (jrrayformItemDefinitionsVO.Count > 0)
                                value = jsonModel["data"]["pageData"][i]["formItemDefinitionsVO"][j]["valueStrs"][0]["valueStr"].ToString();
                            List<string> listval = new List<string>();
                            listval.Add(value);
                            if (dic.Keys.Contains(itemname))
                            {
                                itemname = "总" + itemname;
                            }
                            dic.Add(itemname, listval);


                        }
                    }
                }
                dic.Add("FormType", new List<string>());
                dic["FormType"].Add(fm.FormType);

                dic.Add("FormTypeID", new List<string>());
                dic["FormTypeID"].Add(fm.FormTypeID);
                // FormTypeID
                dic.Add("HospitalID", new List<string>());
                dic["HospitalID"].Add(fm.HospitalID);
                listAll.Add(dic);
            }
            return listAll;
        }

        public string getTocked()
        {
            string TOKEN_KEY = "TOKEN_KEY";
            string TOKEN_EXCEEDTIME = "TOKEN_EXCEEDTIME";
            string cacheTokenValue="";

            if (cache.ContainsKey("TOKEN_EXCEEDTIME"))
            {
                string cacheExceedTimeString = "";
                DateTime cacheExceedTime;
                cache.TryGetValue("TOKEN_EXCEEDTIME", out cacheExceedTimeString);
                if (DateTime.TryParse(cacheExceedTimeString,out cacheExceedTime))
                {
                    if (DateTime.Now > cacheExceedTime.AddHours(1))
                    {
                        //已过期，重新获取
                    }
                    else
                    {
                        if (cache.ContainsKey("TOKEN_KEY"))
                        {
                             cache.TryGetValue("TOKEN_KEY",out cacheTokenValue);
                             return cacheTokenValue;
                        }
                    }
                }
            }
            output = System.IO.File.AppendText(System.Environment.CurrentDirectory + "\\oa.log");
            output.WriteLine("\n" + DateTime.Now.ToString() + "获取token：");
            output.Close();
            //return "OTNkYzlhZmUtNWFjMS00YjJjLThmYjgtMTA4ZjE0MzEzMWFk";
            string str = HttpApi("https://qwif.do1.com.cn/qwcgi/portal/api/qwsecurity!getToken.action?developerId=qw825073dcc1ec483997cfecd2740c75aa&developerKey=OThlMGM2Y2YtNzk0Zi00NTQ5LTk2ZjctYzVhMWQwNTM1YTAw", "", "get");
            //dynamic json = Newtonsoft.Json.Linq.JToken.Parse(str) as dynamic;

            object obj = JsonConvert.DeserializeObject(str);
            Newtonsoft.Json.Linq.JObject js = obj as Newtonsoft.Json.Linq.JObject;//把上面的obj转换为 Jobject对象

            Newtonsoft.Json.Linq.JToken jsonModel = js;//取Jtoken对象     通过Jobject的索引获得到


            string token = string.Empty;
            if (jsonModel["code"].ToString() == "0")
            {
                token = jsonModel["data"]["token"].ToString();
            }

            output = System.IO.File.AppendText(System.Environment.CurrentDirectory + "\\oa.log");
            output.WriteLine("\n" + DateTime.Now.ToString() + "返回报文为：" + str);
            output.WriteLine("\n" + DateTime.Now.ToString() + "获取token为："+token);
            output.Close();

            cache.Remove("TOKEN_KEY");
            cache.Remove("TOKEN_EXCEEDTIME");

            cache.Add("TOKEN_EXCEEDTIME", DateTime.Now.ToString());
            cache.Add("TOKEN_KEY", token);

            return token;
        }

        public static string HttpApi(string url, string jsonstr, string type)
        {
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);//webrequest请求api地址  
            request.Accept = "text/html,application/xhtml+xml,*/*";
            request.ContentType = "application/text";
            request.Method = type.ToUpper().ToString();//get或者post  
            if (!string.IsNullOrEmpty(jsonstr))
            {
                byte[] buffer = encoding.GetBytes(jsonstr);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        public string HttpPostForm(string url, NameValueCollection stringDict)
        {
            try
            {
                string responseContent;
                var memStream = new MemoryStream();
                var webRequest = (HttpWebRequest)WebRequest.Create(url);
                // 边界符 
                var boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
                // 边界符 
                var beginBoundary = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
                // 最后的结束符 
                var endBoundary = Encoding.ASCII.GetBytes("--" + boundary + "--\r\n");

                // 设置属性 
                //webRequest.CookieContainer = Cookie;
                webRequest.Method = "POST";
                webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
                // 写入字符串的Key 
                var stringKeyHeader = "\r\n--" + boundary +
                                       "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                                       "\r\n\r\n{1}\r\n";

                foreach (byte[] formitembytes in from string key in stringDict.Keys
                                                 select string.Format(stringKeyHeader, key, stringDict[key])
                                                     into formitem
                                                     select Encoding.UTF8.GetBytes(formitem))
                {
                    memStream.Write(formitembytes, 0, formitembytes.Length);
                }

                // 写入最后的结束边界符 
                memStream.Write(endBoundary, 0, endBoundary.Length);

                webRequest.ContentLength = memStream.Length;

                var requestStream = webRequest.GetRequestStream();

                memStream.Position = 0;
                var tempBuffer = new byte[memStream.Length];
                memStream.Read(tempBuffer, 0, tempBuffer.Length);
                memStream.Close();

                requestStream.Write(tempBuffer, 0, tempBuffer.Length);
                requestStream.Close();

                var httpWebResponse = (HttpWebResponse)webRequest.GetResponse();

                using (var httpStreamReader = new StreamReader(httpWebResponse.GetResponseStream(),
                                                                Encoding.GetEncoding("utf-8")))
                {
                    responseContent = httpStreamReader.ReadToEnd();
                }

                httpWebResponse.Close();
                webRequest.Abort();

                return responseContent;
            }
            catch (Exception ex)
            {
                // LoggerManagerSingle.Instance.Error("http form post 网站出错", ex);
            }

            return ""; ;
        }

        public string getLatestFormList(string page)
        {
            string result="";
            if (cache.ContainsKey(page)) 
            {
                 cache.TryGetValue(page, out result);
                 return result;
            }

            string tocked = getTocked();
            string url = "https://qwif.do1.com.cn/qwcgi/api/apiFormExportAction!getFormOrder.action?token=" + tocked + "&corpId=ww5657a30e3f4e3ca3&data={'pageNO':" + page + ",'pageSize':100,definitionId:'defne6567efacf524aff9c7884a22a888038'}";

            NameValueCollection stringDict = new NameValueCollection();
            result = HttpPostForm(url, stringDict);
            cache.Add(page, result);

            return result;
        }

        public int getMaxPageFormList()
        {
            string result = getLatestFormList(1+"");
            object obj = JsonConvert.DeserializeObject(result);
            Newtonsoft.Json.Linq.JObject js = obj as Newtonsoft.Json.Linq.JObject;//把上面的obj转换为 Jobject对象

            // Newtonsoft.Json.Linq.JToken jsonModel = js["data"];//取Jtoken对象     通过Jobject的索引获得到

            Newtonsoft.Json.Linq.JToken jsonModel = js;//取Jtoken对象     通过Jobject的索引获得到

            int maxPage = int.Parse(jsonModel["data"]["maxPage"].ToString());
            return maxPage;
        }

        public string getDefinitionIdByFormId(string formid,int version)
        {
            int maxPage = getMaxPageFormList();

            for (int curPage = 1; curPage <= maxPage; curPage++)
            {
                string result = getLatestFormList(curPage+"")+"";
                object obj = JsonConvert.DeserializeObject(result);
                Newtonsoft.Json.Linq.JObject js = obj as Newtonsoft.Json.Linq.JObject;//把上面的obj转换为 Jobject对象

                // Newtonsoft.Json.Linq.JToken jsonModel = js["data"];//取Jtoken对象     通过Jobject的索引获得到

                Newtonsoft.Json.Linq.JToken jsonModel = js;//取Jtoken对象     通过Jobject的索引获得到


                //dynamic jsonModel = Newtonsoft.Json.Linq.JToken.Parse(result) as dynamic;


                Newtonsoft.Json.Linq.JArray jArr = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(jsonModel["data"]["pageData"].ToString());
                for (int i = 0; i < jArr.Count; i++)
                {
                    string definitionVersionsId = jsonModel["data"]["pageData"][i]["definitionVersionsId"].ToString();
                    if (definitionVersionsId == formid)
                    {
                        
                        //string versionLatest = jsonModel["data"]["pageData"][i]["versions"].ToString();
                        //获取最新的版本号的definitionid并返回
                        //string definitionid = jsonModel["data"]["pageData"][i]["versionDefinition"]["versions"].ToString();

                        Newtonsoft.Json.Linq.JArray jArrVersions = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(jsonModel["data"]["pageData"][i]["versionDefinition"].ToString());

                        int maxversion = 1;
                        for (int k = 0; k < jArrVersions.Count; k++)
                        {
                            int currentVersion = int.Parse(jsonModel["data"]["pageData"][i]["versionDefinition"][k]["versions"].ToString());
                            if (currentVersion > maxversion)
                                maxversion = currentVersion;
                        }

                        string versionLatest = maxversion + "";
                        
                        for (int j = 0; j < jArrVersions.Count; j++)
                        {
                            if (versionLatest == jsonModel["data"]["pageData"][i]["versionDefinition"][j]["versions"].ToString())
                            {
                                if (version == 1 && j > 1) 
                                {
                                    //如果是version==1，则获取旧一个版本的审批流程
                                    return jsonModel["data"]["pageData"][i]["versionDefinition"][j-1]["id"].ToString();
                                }
                                return jsonModel["data"]["pageData"][i]["versionDefinition"][j]["id"].ToString();
                            }
                        }
                    }
                }
            }
            return "";
            
        }
    }
}
