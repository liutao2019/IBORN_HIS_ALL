using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace FoShanSporadicUpload
{
    /// <summary>
    /// 调用佛山是零星报销代理服务器的WebService发布的方法
    /// </summary>
    public class SIBizProcess
    {
        #region 变量

        /// <summary>
        /// 服务
        /// </summary>
        private SporadicService.FacadeServiceProxyService facadeService = new FoShanSporadicUpload.SporadicService.FacadeServiceProxyService();

        #endregion

        #region 方法

        /// <summary>
        /// 医院登录[100]
        /// </summary>
        /// <param name="transNO"></param>
        /// <param name="inXML"></param>
        /// <returns></returns>
        public Model.ResultHead Login(string transNO, string inXML)
        {
            string errMsg = "";
            //入参
            Model.ResultHead result = InvokeWebServiceByIn("医院登录[100]", "login", transNO, inXML, ref errMsg);


            return result;
        }
        /// <summary>
        /// 接口服务
        /// </summary>
        /// <param name="transNO"></param>
        /// <param name="inXML"></param>
        /// <returns></returns>
        private Model.ResultHead InvokeWebServiceByIn(string transName, string methodName, string transNO, string inXML, ref string errMsg)
        {
            //入参
            LogManager.WriteLog(transName + inXML);

            string[] args = { transNO, inXML };
            Object obj = Common.WebServiceHelper.InvokeWebService(Function.WebServiceAddress, methodName, args, ref errMsg);
            if (obj == null)
            {
                return null;
            }
            string outXML = obj.ToString();
            Model.ResultHead result = Function.GetResultHead(transNO, outXML);

            //出参
            LogManager.WriteLog(transName + outXML);

            return result;
        }

        /// <summary>
        /// 医院口令修改[101]
        /// </summary>
        /// <param name="transNO"></param>
        /// <param name="inXML"></param>
        /// <returns></returns>
        public Model.ResultHead ChangePw(string transNO, string inXML)
        {
            string errMsg = "";
            Model.ResultHead result = InvokeWebServiceByIn("医院口令修改[101]:", "login", transNO, inXML, ref errMsg);


            return result;

        }

        /// <summary>
        /// 待上传处方查询[31]
        /// </summary>
        /// <param name="transNO"></param>
        /// <param name="inXML"></param>
        /// <param name="listRecipe"></param>
        /// <returns></returns>
        public Model.ResultHead QueryNeedUploadRecipe(string transNO, string inXML, ref List<Model.NeedUploadRecipe> listRecipe)
        {
            listRecipe = new List<FoShanSporadicUpload.Model.NeedUploadRecipe>(); //初始化

            string errMsg = "";
            Model.ResultHead result = InvokeWebServiceByIn("待上传处方查询[31]:", "process", transNO, inXML, ref errMsg);
            if (result.Code == "1" && !string.IsNullOrEmpty(result.OutPutXML))
            {
                #region 解释返回的明细

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result.OutPutXML);

                XmlNodeList nodeList = doc.SelectNodes("output/sqldata/row");

                foreach (XmlNode node in nodeList)
                {
                    string rowXML = node.OuterXml;
                    if (!string.IsNullOrEmpty(rowXML))
                    {
                        doc = new XmlDocument();
                        doc.LoadXml(rowXML);

                        //节点-值
                        Dictionary<string, string> dicNodeValue = new Dictionary<string, string>();
                        //节点
                        List<string> listNode = new List<string>();
                        listNode.Add("aac002");
                        listNode.Add("aac001");
                        listNode.Add("aac003");
                        listNode.Add("akc331");
                        listNode.Add("akc332");
                        listNode.Add("akc333");

                        string errorMsg = string.Empty; //错误信息
                        int rev = Function.GetNodeValue(doc, listNode, "row", ref dicNodeValue, out errorMsg);
                        if (rev > 0)
                        {
                            Model.NeedUploadRecipe recipe = new FoShanSporadicUpload.Model.NeedUploadRecipe();
                            if (dicNodeValue.ContainsKey("aac002"))
                            {
                                recipe.IdNO = dicNodeValue["aac002"];  //证件号码
                            }
                            if (dicNodeValue.ContainsKey("aac001"))
                            {
                                recipe.MedicalCardNO = dicNodeValue["aac001"]; //医保编号
                            }
                            if (dicNodeValue.ContainsKey("aac003"))
                            {
                                recipe.PatientName = dicNodeValue["aac003"];   //姓名 
                            }
                            if (dicNodeValue.ContainsKey("akc331"))
                            {
                                recipe.ServiceType = dicNodeValue["akc331"];   //门诊住院标志 1-门诊、2-住院
                            }
                            if (dicNodeValue.ContainsKey("akc332"))
                            {
                                recipe.InvoiceNO = dicNodeValue["akc332"];    //发票号
                            }
                            if (dicNodeValue.ContainsKey("akc333"))
                            {
                                recipe.GSType = dicNodeValue["akc333"];    //工伤标志
                            }
                            listRecipe.Add(recipe);
                        }
                        else
                        {
                            result.Message += "\r\n" + errorMsg;
                        }
                    }
                }


                #endregion
            }


            return result;
        }

        /// <summary>
        /// 零报处方项目上传[33]
        /// </summary>
        /// <param name="transNO"></param>
        /// <param name="inXML"></param>
        /// <returns></returns>
        public Model.ResultHead UploadRecipe(string transNO, string inXML)
        {
            string errMsg = "";

            Model.ResultHead result = InvokeWebServiceByIn("零报处方项目上传[33]:", "process", transNO, inXML, ref errMsg);


            return result;

        }

        /// <summary>
        /// 已上传处方明细汇总信息查询[32]
        /// </summary>
        /// <param name="transNO"></param>
        /// <param name="inXML"></param>
        /// <param name="listHaveUploadedRecipe"></param>
        /// <returns></returns>
        public Model.ResultHead QueryHaveUploadedRecipe(string transNO, string inXML, ref List<Model.HaveUploadedRecipe> listHaveUploadedRecipe)
        {

            listHaveUploadedRecipe = new List<FoShanSporadicUpload.Model.HaveUploadedRecipe>(); //初始化

            string errMsg = "";
            Model.ResultHead result = InvokeWebServiceByIn("已上传处方明细汇总信息查询[32]:", "process", transNO, inXML, ref errMsg);
            if (result.Code == "1" && !string.IsNullOrEmpty(result.OutPutXML))
            {
                #region 解释返回的明细

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result.OutPutXML);

                XmlNodeList nodeList = doc.SelectNodes("output/sqldata/row");

                foreach (XmlNode node in nodeList)
                {
                    string rowXML = node.OuterXml;
                    if (!string.IsNullOrEmpty(rowXML))
                    {
                        doc = new XmlDocument();
                        doc.LoadXml(rowXML);

                        //节点-值
                        Dictionary<string, string> dicNodeValue = new Dictionary<string, string>();
                        //节点
                        List<string> listNode = new List<string>();
                        listNode.Add("aac002");
                        listNode.Add("aac001");
                        listNode.Add("akc331");
                        listNode.Add("akc220");
                        listNode.Add("akc330");
                        listNode.Add("ykc610");
                        listNode.Add("aka111");
                        listNode.Add("aka112");
                        listNode.Add("akc222y");
                        listNode.Add("akc223y");
                        listNode.Add("akc224");
                        listNode.Add("akc229");
                        listNode.Add("akc230");
                        listNode.Add("akc231");
                        listNode.Add("akc226");
                        listNode.Add("akc225");
                        listNode.Add("akc227");
                        listNode.Add("ykc611");
                        listNode.Add("aka074");
                        listNode.Add("aka067");
                        listNode.Add("aka070");
                        listNode.Add("cke522");
                        listNode.Add("aae030");
                        listNode.Add("aae031");
                        listNode.Add("ykc613");
                        listNode.Add("ykc011");
                        listNode.Add("akc221");
                        listNode.Add("akc332");

                        string errorMsg = string.Empty; //错误信息
                        int rev = Function.GetNodeValue(doc, listNode, "row", ref dicNodeValue, out errorMsg);
                        if (rev > 0)
                        {
                            Model.HaveUploadedRecipe recipe = new FoShanSporadicUpload.Model.HaveUploadedRecipe();

                            #region 组装实体

                            if (dicNodeValue.ContainsKey("aac002"))
                            {
                                recipe.IdNO = dicNodeValue["aac002"];  //证件号码
                            }
                            if (dicNodeValue.ContainsKey("aac001"))
                            {
                                recipe.MedicalCardNO = dicNodeValue["aac001"]; //医保编号
                            }
                            if (dicNodeValue.ContainsKey("akc331"))
                            {
                                recipe.ServiceType = dicNodeValue["akc331"];  //门诊住院标志
                            }
                            if (dicNodeValue.ContainsKey("akc220"))
                            {
                                recipe.PatientID = dicNodeValue["akc220"];  //处方号
                            }
                            if (dicNodeValue.ContainsKey("akc330"))
                            {
                                recipe.InTimes = dicNodeValue["akc330"];  //住院次数
                            }
                            if (dicNodeValue.ContainsKey("ykc610"))
                            {
                                recipe.SequenceNO = dicNodeValue["ykc610"];  //项目序号
                            }
                            if (dicNodeValue.ContainsKey("aka111"))
                            {
                                recipe.StatCode = dicNodeValue["aka111"];  //大类代码
                            }
                            if (dicNodeValue.ContainsKey("aka112"))
                            {
                                recipe.StatName = dicNodeValue["aka112"];  //大类名称
                            }
                            if (dicNodeValue.ContainsKey("akc222y"))
                            {
                                recipe.CenterItemCode = dicNodeValue["akc222y"];  //项目代码
                            }
                            if (dicNodeValue.ContainsKey("akc223y"))
                            {
                                recipe.CenterItemName = dicNodeValue["akc223y"];  //项目名称
                            }
                            if (dicNodeValue.ContainsKey("akc224"))
                            {
                                recipe.GovermentItemCode = dicNodeValue["akc224"];  //药监局药品编码
                            }
                            if (dicNodeValue.ContainsKey("akc229"))
                            {
                                recipe.SpecialDrugFlag = dicNodeValue["akc229"];  //限制用药标记
                            }
                            if (dicNodeValue.ContainsKey("akc230"))
                            {
                                recipe.RegisterName = dicNodeValue["akc230"];  //医用材料的注册证产品名称
                            }
                            if (dicNodeValue.ContainsKey("akc231"))
                            {
                                recipe.RegisterNO = dicNodeValue["akc231"];  //医用材料的食药监注册号
                            }
                            if (dicNodeValue.ContainsKey("akc226"))
                            {
                                recipe.Qty = FS.FrameWork.Function.NConvert.ToDecimal(dicNodeValue["akc226"]);  //数量
                            }
                            if (dicNodeValue.ContainsKey("akc225"))
                            {
                                recipe.ItemPrice = FS.FrameWork.Function.NConvert.ToDecimal(dicNodeValue["akc225"]);  //单价
                            }
                            if (dicNodeValue.ContainsKey("akc227"))
                            {
                                recipe.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(dicNodeValue["akc227"]);  //费用总额
                            }
                            if (dicNodeValue.ContainsKey("ykc611"))
                            {
                                recipe.ProductAddress = dicNodeValue["ykc611"];  //产地
                            }
                            if (dicNodeValue.ContainsKey("aka074"))
                            {
                                recipe.Spces = dicNodeValue["aka074"];  //规格型号
                            }
                            if (dicNodeValue.ContainsKey("aka067"))
                            {
                                recipe.ItemUnit = dicNodeValue["aka067"];  //计价单位
                            }
                            if (dicNodeValue.ContainsKey("aka070"))
                            {
                                recipe.DoseForm = dicNodeValue["aka070"];  //剂型
                            }
                            if (dicNodeValue.ContainsKey("cke522"))
                            {
                                recipe.UseInfo = dicNodeValue["cke522"];  //使用情况
                            }
                            if (dicNodeValue.ContainsKey("aae030"))
                            {
                                recipe.StartTime = dicNodeValue["aae030"];  //费用开始日期
                            }
                            if (dicNodeValue.ContainsKey("aae031"))
                            {
                                recipe.EndTime = dicNodeValue["aae031"];  //费用终止日期
                            }
                            if (dicNodeValue.ContainsKey("ykc613"))
                            {
                                recipe.DoctName = dicNodeValue["ykc613"];  //处方医生姓名
                            }
                            if (dicNodeValue.ContainsKey("ykc011"))
                            {
                                recipe.DeptName = dicNodeValue["ykc011"];  //科室名称
                            }
                            if (dicNodeValue.ContainsKey("akc221"))
                            {
                                recipe.FeeDate = dicNodeValue["akc221"];  //收费时间
                            }
                            if (dicNodeValue.ContainsKey("akc332"))
                            {
                                recipe.InvoiceNO = dicNodeValue["akc332"];    //发票号
                            }

                            #endregion

                            listHaveUploadedRecipe.Add(recipe);
                        }
                        else
                        {
                            result.Message += "\r\n" + errorMsg;
                        }
                    }
                }


                #endregion
            }
            return result;
        }

        /// <summary>
        /// 零报处方项目回退[34]
        /// </summary>
        /// <param name="transNO"></param>
        /// <param name="inXML"></param>
        /// <returns></returns>
        public Model.ResultHead CancelUploadRecipe(string transNO, string inXML)
        {
            string errMsg = "";
            Model.ResultHead result = InvokeWebServiceByIn("零报处方项目回退[34]:", "process", transNO, inXML, ref errMsg);

            return result;
        }

        #endregion


    }
}
