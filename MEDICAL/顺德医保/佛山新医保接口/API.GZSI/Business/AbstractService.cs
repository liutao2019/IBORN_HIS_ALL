using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace API.GZSI.Business
{
    /// <summary>
    /// 公共接口调用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="E"></typeparam>
    public abstract class AbstractService<T, E> : IService where E : Models.Response.ResponseBase
    {
        /// <summary>
        /// 当前接口ID
        /// </summary>
        public abstract string InterfaceID
        {
            get;
        }

        /// <summary>
        /// 本地管理类
        /// </summary>
        protected LocalManager localManager = new LocalManager();

        /// <summary>
        /// 错误信息
        /// </summary>
        private string errorMsg = string.Empty;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg
        {
            get
            {
                return errorMsg;
            }
            set
            {
                errorMsg = value;
            }
        }

        /// <summary>
        /// 调用签到函数签到
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public virtual int ServiceSign(DateTime sNow)
        {
            int ret = -1;
            string Infno = "";
            Models.UserInfo.Instance.sign_no = "";

            //调用实体
            CommonService9001 commonService9001 = new CommonService9001();
            Models.Request.RequestGzsiModel9001 requestGzsiModel9001 = new Models.Request.RequestGzsiModel9001();
            Models.Response.ResponseGzsiModel9001 responseGzsiModel9001 = new Models.Response.ResponseGzsiModel9001();

            //参数赋值
            Models.Request.RequestGzsiModel9001.Data requestGzsiModel9001Data = new Models.Request.RequestGzsiModel9001.Data();
            requestGzsiModel9001Data.mac = API.GZSI.Common.SystemInfo.GetMacAddrLocal();
            requestGzsiModel9001Data.ip = API.GZSI.Common.SystemInfo.GetClientLocalIPv4Address();
            requestGzsiModel9001Data.opter_no = FS.FrameWork.Management.Connection.Operator.ID;
            requestGzsiModel9001.signIn = requestGzsiModel9001Data;

            //接口调用
            string requestStr = commonService9001.ConvertModelToSendMessage(requestGzsiModel9001, ref Infno);
            string responseStr = string.Empty;
            ret = Class.HttpHelper.RequestData(requestStr, Infno, ref responseStr);
            if (ret == -1)
            {
                this.errorMsg = "获取医保信息系统连接状态服务失败，原因：" + Class.HttpHelper.Err;
                return -1;
            }

            //返回结果转化
            ret = commonService9001.ConvertReceiverMessageToModel(responseStr, requestGzsiModel9001, ref responseGzsiModel9001);
            if (ret == -1)
            {
                this.errorMsg = commonService9001.ErrorMsg;
                return -1;
            }

            //结果处理
            Models.UserInfo.Instance.sign_no = responseGzsiModel9001.output.signinoutb.sign_no;
            Models.UserInfo.Instance.sign_time = DateTime.Parse(responseGzsiModel9001.output.signinoutb.sign_time);
            Models.UserInfo.Instance.sign_oper = FS.FrameWork.Management.Connection.Operator.ID;
            
            //保存签到信息至配置文件
            Models.UserInfo.Instance.SaveSign();

            return 1;
        }

        /// <summary>
        /// 调用接口服务
        /// 单对象返回
        /// </summary>
        /// <returns></returns>
        public int CallService(T sendObject, ref E receiveObject, params object[] appendParams)
        {
            int retInt;
            string Infno = "";

            //业务前签到
            DateTime sNow = FS.FrameWork.Function.NConvert.ToDateTime(this.localManager.GetSysDateTime());
            if (string.IsNullOrEmpty(Models.UserInfo.Instance.sign_no) ||
                Models.UserInfo.Instance.sign_time < sNow.AddHours(-24) || 
                Models.UserInfo.Instance.sign_oper != FS.FrameWork.Management.Connection.Operator.ID)
            {
                retInt = this.ServiceSign(sNow);
                if (retInt == -1)
                {
                    return -1;
                }
            }

            
            //具体业务处理
            string inputData = this.ConvertModelToSendMessage(sendObject,ref Infno, appendParams);
            string outData = string.Empty;


            // {ACB49EE2-E85B-4AA0-96CB-2EE7912AA5E8}
            if (Infno == "9102")//文件下载接口
            {
                byte[] filebyte = new byte[1];
                retInt = Class.HttpHelper.RequestDataByte(inputData, Infno, ref filebyte);
                if (Encoding.UTF8.GetString(filebyte).Contains("error"))
                {
                    this.errorMsg = Encoding.UTF8.GetString(filebyte);
                    return -1;
                }


                string filePath = string.Empty;
                if (appendParams != null && appendParams.Length > 0)
                {
                    filePath = appendParams[0].ToString();
                }

                //指定路径
                if (!string.IsNullOrEmpty(filePath))
                {
                    FileStream fsout = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                    fsout.Write(filebyte, 0, filebyte.Length);
                    fsout.Close();
                    return 1;
                }
                else
                {
                    //非指定路径
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    //saveFileDialog1.Filter = "Excel |.xls";
                    saveFileDialog1.Filter = "(*.zip)|*.zip";
                    saveFileDialog1.Title = "导出数据";
                    saveFileDialog1.FileName = "9102数据下载";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        if (saveFileDialog1.FileName != "")
                        {
                            FileStream fsout = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write);
                            fsout.Write(filebyte, 0, filebyte.Length);
                            fsout.Close();
                        }
                        return 1;
                    }
                }
            }

            retInt = Class.HttpHelper.RequestData(inputData, Infno, ref outData);

            if (retInt == -1)
            {
                this.errorMsg = "调用" + this.InterfaceID + "服务失败，原因：" + Class.HttpHelper.Err;
                return -1;
            }

            //返回业务处理结果
            return this.ConvertReceiverMessageToModel(outData, sendObject, ref receiveObject, appendParams);
        }

        /// <summary>
        /// 消息转换，将对象转换成发送消息
        /// </summary>
        /// <param name="t">原始对象</param>
        /// <param name="e">转换后消息</param>
        /// <param name="errInfo">转换过程中的错误信息</param>
        /// <param name="appendParams">附加参数</param>//SerialNumber交易流水号,strTransVersion交易版本号,strVerifyCode交易验证码
        /// <returns>-1 失败 1 成功</returns>
        protected virtual string ConvertModelToSendMessage(T t,ref string Infno, params object[] appendParams)
        {
            if (appendParams == null || t == null)
            {
                throw new Exception("传入的参数有问题");
            }

            Models.WebMessage<T> wm = new Models.WebMessage<T>();
            wm.Infno = this.InterfaceID;
            Infno = wm.Infno;
            wm.Msgid = DateTime.Now.ToString("yyyyMMddHHmmssff") + "0001";
            wm.Insuplc_admdvs = string.IsNullOrEmpty(Models.UserInfo.Instance.insuplc) ? "440606" : Models.UserInfo.Instance.insuplc;//异地需要改变参保地
            //wm.Insuplc_admdvs = "440606";
            wm.Mdtrtarea_admvs = "440606";
            wm.Recer_admdvs = "440606";
            wm.Dev_no = "";
            wm.Dev_safe_info = "";
            wm.Signtype = "SM3";
            wm.Cainfo = Common.SM3Helper.Encryption(t.ToString());
            wm.Infver = "1.0.0";
            wm.Opter_type = "1";
            wm.Opter = "01";
            wm.Opter_name = FS.FrameWork.Management.Connection.Operator.ID;
            wm.Inf_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            wm.Fixmedins_code = Models.UserInfo.Instance.userId;
            wm.Fixmedins_name = "顺德爱博恩妇产医院";
            wm.Recer_sys_code = "HIS";
            wm.Sign_no = Models.UserInfo.Instance.sign_no;
            wm.Input = t;

            string retWm = Class.JsonHelper.SerializeObject(wm);
            return retWm;
        }

        /// <summary>
        /// 消息转换，将结果消息转换成对象
        /// </summary>
        /// <param name="m">结果消息</param>
        /// <param name="n">结果对象</param>
        /// <param name="errInfo">转换过程中的错误信息</param>
        /// <param name="appendParams">附件参数</param>
        /// <returns>-1 失败 1 成功</returns>
        protected virtual int ConvertReceiverMessageToModel(string m, T t, ref E e, params object[] appendParams)
        {
            try
            {
                //新格式解析
                e = Class.JsonHelper.DeserializeJsonToObject<E>(m);
                if (e.infcode != "0")
                {
                    if (e.infcode == null)
                    {
                        //返回报文格式为老格式：{"code":"","type":"","message":"","data":""}
                        Models.ServiceResult<E> resultObj = Class.JsonHelper.DeserializeJsonToObject<Models.ServiceResult<E>>(m);
                        if (resultObj.code != "0")
                        {
                            this.errorMsg = "调用" + this.InterfaceID + "服务失败，原因：" + resultObj.code + ":" + resultObj.message;
                            return -1;
                        }

                        e = resultObj.data;
                        if (e.infcode != "0")
                        {
                            this.errorMsg = "调用" + this.InterfaceID + "服务失败，原因：" + e.infcode + ":" + e.err_msg;
                            return -1;
                        }
                    }
                    else
                    {
                        this.errorMsg = "调用" + this.InterfaceID + "服务失败，原因：" + e.infcode + ":" + e.err_msg;
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                this.errorMsg = "调用" + this.InterfaceID + "服务失败，原因：" + ex.Message;
                return -1;
            }

            return 1;
        }
    }
}
