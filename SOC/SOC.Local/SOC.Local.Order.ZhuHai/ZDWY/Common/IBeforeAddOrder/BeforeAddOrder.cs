using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.Common.IBeforeAddOrder
{
    /// <summary>
    /// 开立医嘱（处方）前操作接口
    /// 1、不同合同单位的给出提示
    /// </summary>
    public class BeforeAddOrder : FS.HISFC.BizProcess.Interface.Order.IBeforeAddOrder
    {
        #region IBeforeAddOrder 成员

        /// <summary>
        /// 错误信息
        /// </summary>
        string errInfo = "";

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrInfo
        {
            get
            {
                return errInfo;
            }
            set
            {
                errInfo = value;
            }
        }

        /// <summary>
        /// 住院
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int OnBeforeAddOrderForInPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct)
        {

            #region 警戒线判断

            //费用控制不严格控制医生，只是提示：
            //这里的50用警戒线值来处理代替
            //1）、如果患者余额<50，则提示医生开立的医嘱不能够审核。
            //2）、如果患者余额>50，但<500；则提示医生患者余额不足，请患者及时补交预交金，否则可能影响医嘱的正常执行。

            //用于提醒开立的金额限制
            decimal warningFee = 500;

            //实时获取最新的警戒线、余额等信息
            FS.HISFC.BizLogic.RADT.InPatient inpatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
            FS.HISFC.BizProcess.Integrate.Fee feeIntergrate = new FS.HISFC.BizProcess.Integrate.Fee();

            FS.HISFC.Models.RADT.PatientInfo pInfo = inpatientMgr.QueryPatientInfoByInpatientNO(patientInfo.ID);

            if (feeIntergrate.IsPatientLackFee(patientInfo, 0))
            {
                MessageBox.Show(pInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 患者【" + pInfo.Name + "】已经欠费，\r\n\r\n余额： " + pInfo.FT.LeftCost.ToString() + "小于警戒线： " + pInfo.PVisit.MoneyAlert.ToString() + "\r\n\r\n请提醒患者及时缴纳押金，否则开立的医嘱不能够正常核对执行！\r\n\r\n如有疑问请联系医务科、财务科！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 1;
            }
            else
            {
                if (patientInfo.PVisit.AlertFlag
                    && patientInfo.PVisit.AlertType.ID.ToString() == "M")
                {
                    if (patientInfo.FT.LeftCost < warningFee)
                    {
                        MessageBox.Show(pInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 患者【" + pInfo.Name + "】余额不足，\r\n\r\n余额： " + pInfo.FT.LeftCost.ToString() + "\r\n警戒线： " + pInfo.PVisit.MoneyAlert.ToString() + "\r\n\r\n请提醒患者及时缴纳押金，否则可能导致开立的医嘱不能够正常核对执行！\r\n\r\n如有疑问请联系医务科、财务科！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return 1;
                    }
                }
            }

            #endregion

            #region 处方权判断

            if (!isCanAddOrder())
            {
                errInfo = LocalMgr.Operator.Name+"医生您好：您没有处方权，不能开立医嘱！\r\n\r\n如有疑问请联系医务科！";
                return -1;
            }

            #endregion

            return 1;
        }

        /// <summary>
        /// 中五要求没有处方权的医生不能开立医嘱（含检验检查等），不能管床
        /// 2014-10-20 陈熙文
        /// </summary>
        /// <returns></returns>
        private bool isCanAddOrder()
        {
            string strSQL = @"select count(*) from met_com_authority f
                            where f.empl_code='{0}'
                            and f.popedom_type='1'";

            strSQL = string.Format(strSQL, LocalMgr.Operator.ID);

            string rev = LocalMgr.ExecSqlReturnOne(strSQL, "0");
            if (rev == "0")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 患者信息
        /// </summary>
        Hashtable hsPatientInfo = new Hashtable();

        /// <summary>
        /// 操作员
        /// </summary>
        FS.HISFC.Models.Base.Employee oper = null;

        FS.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        Classes.LocalManager localMgr = null;

        public Classes.LocalManager LocalMgr
        {
            get
            {
                if (localMgr == null)
                {
                    localMgr = new FS.SOC.Local.Order.ZhuHai.Classes.LocalManager();
                }
                return localMgr;
            }
        }

        FS.HISFC.BizLogic.Manager.DataBase dbMgr = new FS.HISFC.BizLogic.Manager.DataBase();

        /// <summary>
        /// 门诊
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int OnBeforeAddOrderForOutPatient(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct)
        {
            /* 处方管理办法：
             * 医师取得麻醉药品和第一类精神药品处方权后，方可在本机构开具麻醉药品和第一类精神药品处方，
             * 但不得为自己开具该类药品处方。
             * */

            //按照张槎的要求，所有药都不能给自己开立
            try
            {
                DateTime dtNow = dbMgr.GetDateTimeFromSysDateTime();

                #region 开立前提示是否在其他科室看诊过

                DateTime dtBegin = dtNow.Date;
                if (regObj.DoctorInfo.Templet.RegLevel.IsEmergency)
                {
                    dtBegin = dtNow.AddHours(-24);
                }

                ArrayList alReg = LocalMgr.GetRegSeeList(regObj.PID.CardNO, regObj.ID, dtBegin);
                if (alReg == null)
                {
                    FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "错误", "BeforeAddOrder 处出现异常错误！\r\n" + LocalMgr.Err, ToolTipIcon.Error);
                }

                if (alReg.Count > 0)
                {
                    string Msg = "患者["+regObj.Name+"]已经有以下看诊记录，请注意！\r\n\r\n";
                    foreach (FS.HISFC.Models.Registration.Register regTemp in alReg)
                    {
                        Msg += "于" + regTemp.DoctorInfo.SeeDate.ToString() + "在" + regTemp.SeeDoct.Dept.Name + "\r\n";
                    }

                    if (regObj.IsSee)
                    {
                        MessageBox.Show(Msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "提示", Msg, ToolTipIcon.Error);
                    }
                }

                #endregion


                if (!regObj.IsSee)
                {
                    #region 判断挂号级别不同，提醒是否看诊

                    string reglevlCode = "";
                    string regDiagItemCode = "";

                    FS.HISFC.Models.Base.Employee doct = FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(reciptDoct.ID);


                    FS.HISFC.Models.Registration.Schema schema = regIntegrate.GetSchema(reciptDoct.ID, dtNow);

                    if (schema == null)
                    {
                        if (regIntegrate.GetSupplyRegInfo(reciptDoct.ID, doct.Level.ID, reciptDept.ID, ref reglevlCode, ref regDiagItemCode) == -1)
                        {
                            errInfo = regIntegrate.Err;
                            return -1;
                        }

                    }
                    else
                    {
                        reglevlCode = schema.Templet.RegLevel.ID;
                    }

                    FS.HISFC.Models.Registration.RegLevel doctRegLevl = FS.SOC.HISFC.BizProcess.Cache.Fee.GetRegLevl(reglevlCode);
                    //{EF6FF76F-13B3-4de7-8071-0D3D33B447A1}
                    //if (regObj.DoctorInfo.Templet.RegLevel.ID != doctRegLevl.ID)
                    if (doctRegLevl != null && regObj.DoctorInfo.Templet.RegLevel.ID != doctRegLevl.ID)
                    {
                        if (MessageBox.Show("您看诊的挂号级别为:【" + doctRegLevl.Name + "】，\r\n而患者的挂号级别为：【" + regObj.DoctorInfo.Templet.RegLevel.Name + "】\r\n\r\n是否继续看诊？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                        {
                            errInfo = "您已经取消操作！";
                            return -1;
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "错误", "BeforeAddOrder 处出现异常错误！\r\n" + ex.Message, ToolTipIcon.Error);
            }

            return 1;
        }

        #endregion
    }
}
