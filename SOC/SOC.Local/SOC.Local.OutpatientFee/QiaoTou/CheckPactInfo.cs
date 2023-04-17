using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;

namespace FS.SOC.Local.OutpatientFee.QiaoTou
{
    /// <summary>
    /// 校验患者合同单位信息
    /// </summary>
    public class CheckPactInfo : FS.HISFC.BizProcess.Interface.Common.ICheckPactInfo
    {
        #region ICheckPactInfo 成员

        /// <summary>
        /// 错误信息
        /// </summary>
        string err;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Err
        {
            get
            {
                return err;
            }
            set
            {
                err = value;
            }
        }

        /// <summary>
        /// 患者基本信息
        /// </summary>
        private FS.HISFC.Models.RADT.Patient patientInfo = null;

        /// <summary>
        /// 患者基本信息
        /// </summary>
        public FS.HISFC.Models.RADT.Patient PatientInfo
        {
            get
            {
                return patientInfo;
            }
            set
            {
                patientInfo = value;
            }
        }

        /// <summary>
        /// 医疗待遇接口
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        /// <summary>
        /// 校验的合同单位
        /// </summary>
        string CheckEmployeePactList = "-1";

        /// <summary>
        /// 校验合同单位的有效性
        /// </summary>
        /// <returns></returns>
        public int CheckIsValid()
        {
            if (patientInfo == null)
            {
                err = "患者信息为空！";
                return -1;
            }

            if (CheckEmployeePactList == "-1")
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                CheckEmployeePactList = controlMgr.GetControlParam<string>("GFSI01", true, "");
            }

            if (CheckEmployeePactList.Contains(patientInfo.Pact.ID))
            {

                //this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                //开始待遇事务
                //this.medcareInterfaceProxy.BeginTranscation();

                //设置待遇的合同单位参数
                this.medcareInterfaceProxy.SetPactCode(patientInfo.Pact.ID);

                this.medcareInterfaceProxy.IsLocalProcess = false;

                //连接待遇接口
                long returnValue = this.medcareInterfaceProxy.Connect();

                if (returnValue == -1)
                {
                    //FS.FrameWork.Management.PublicTrans.RollBack();
                    //医保回滚可能出错，此处提示
                    //if (this.medcareInterfaceProxy.Rollback() == -1)
                    //{
                    //    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                    //    return;
                    //}
                    //MessageBox.Show(Language.Msg("医疗待遇接口连接失败!") + this.medcareInterfaceProxy.ErrMsg);
                    this.err = Language.Msg("医疗待遇接口连接失败!") + this.medcareInterfaceProxy.ErrMsg;

                    return -1;
                }

                //黑名单判断

                if (patientInfo.GetType() == typeof(FS.HISFC.Models.Registration.Register))
                {
                    if (this.medcareInterfaceProxy.IsInBlackList((FS.HISFC.Models.Registration.Register)patientInfo))
                    {
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                        // 医保回滚可能出错，此处提示
                        //if (this.medcareInterfaceProxy.Rollback() == -1)
                        //{
                        //    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                        //    return;
                        //}
                        this.medcareInterfaceProxy.Disconnect();
                        //MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                        this.err = this.medcareInterfaceProxy.ErrMsg;
                        return -1;
                    }
                }
                else
                {
                    if (this.medcareInterfaceProxy.IsInBlackList((FS.HISFC.Models.RADT.PatientInfo)patientInfo))
                    {
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                        // 医保回滚可能出错，此处提示
                        //if (this.medcareInterfaceProxy.Rollback() == -1)
                        //{
                        //    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                        //    return;
                        //}
                        this.medcareInterfaceProxy.Disconnect();
                        //MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                        this.err = this.medcareInterfaceProxy.ErrMsg;
                        return -1;
                    }
                }
            }
            return 1;
        }

        #endregion
    }
}
