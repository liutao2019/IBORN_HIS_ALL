using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IBorn.SI.GuangZhou
{
    /// <summary>
    /// 广州医保登记接口实现
    /// by 飞扬 2019-09-28
    /// </summary>
    public class MedicalRadtProcess : IBorn.SI.BI.IRADT
    {

        IBorn.SI.GuangZhou.SILocalManager myInterface = new IBorn.SI.GuangZhou.SILocalManager();

        #region IRADT 成员

        private string errorMsg;
        /// <summary>
        /// 错误提示信息
        /// </summary>
        public string ErrorMsg
        {
            get
            {
                return this.errorMsg;
            }
        }

        public int CancelLeaveRegister<T>(string registerType, T register)
        {
            throw new NotImplementedException();
        }

        public int CancelRegister<T>(string registerType, T register)
        {
            //广州医保登记，在医保端完成-此方法只是将社保端的医保登记信息关联到本地
            if (register == null || string.IsNullOrEmpty(registerType))
            {
                this.errorMsg = "传参错误！";
                return -1;
            }
            if (registerType == "O")
            {
                #region 门诊
                var reg = register as FS.HISFC.Models.Registration.Register;
                //广州医保登记，在医保端完成-此方法只是将社保端的医保登记信息关联到本地
                if (reg == null || string.IsNullOrEmpty(reg.ID))
                {
                    this.errorMsg = "没有获得患者基本信息,请赋值!";
                    return -1;
                }                
                return myInterface.UpdateSiMainInfoDisable(reg.ID, reg.SIMainInfo.RegNo);
                #endregion
            }
            else if (registerType == "I")
            {
                #region 住院
                var inPatient = register as FS.HISFC.Models.RADT.PatientInfo;
                ////广州医保取消登记，在医保端完成-此方法只是将本地的医保登记信息设置无效
                return myInterface.UpdateSiMainInfoDisable(inPatient.ID, inPatient.SIMainInfo.RegNo);
                #endregion
            }
            return 1;
        }

        public int ChangePatient<T>(string registerType, T register)
        {
            throw new NotImplementedException();
        }


        public T GetPatient<T>(string registerType, T register)
        {
            throw new NotImplementedException();
        }

        public int LeaveRegister<T>(string registerType, T register)
        {
            throw new NotImplementedException();
        }
        //只是中间表交换数据对接的话，改名为关联同步更好
        public int Register<T>(string registerType, T register)
        {
            if (register == null || string.IsNullOrEmpty(registerType))
            {
                this.errorMsg = "传参错误！";
                return -1;
            }
            if (registerType == "O")
            {
                var reg = register as FS.HISFC.Models.Registration.Register;
                //广州医保登记，在医保端完成-此方法只是将社保端的医保登记信息关联到本地
                if (reg == null || string.IsNullOrEmpty(reg.ID))
                {
                    this.errorMsg = "没有获得患者基本信息,请赋值!";
                    return -1;
                }
                IBorn.SI.GuangZhou.Controls.ucRegisterInfoOutPatient uc = new IBorn.SI.GuangZhou.Controls.ucRegisterInfoOutPatient();
                uc.Register = reg;
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                FS.FrameWork.WinForms.Classes.Function.ShowControl(uc);
                if (uc.IsOK)
                {
                    uc.Register.SIMainInfo.IsValid = true;
                    uc.Register.SIMainInfo.IsBalanced = false;                   
                    int iReturn = myInterface.SaveSIMainInfo(uc.Register);
                    if (iReturn <= 0)
                    {
                        MessageBox.Show("保存医保端的登记信息到本地失败!" + myInterface.Err);
                        return -1;
                    }
                    return 1;
                }
                else
                {
                    this.errorMsg = "未选择医保端的登记信息!";
                    return -1;
                }
            }
            else if (registerType == "I")
            {
                #region 住院
                //广州医保登记，在医保端完成-此方法只是将社保端的医保登记信息关联到本地                
                var inPatient = register as FS.HISFC.Models.RADT.PatientInfo;
                ////住院登记状态不关联医保信息                
                //if (inPatient.sta == "R")
                //{
                //    return 1;
                //}
                //List<WF.Expense.Models.InPatient.InsRegister> listInsReg = this.queryMgr.QueryInsRegister(register.ID);
                //if (listInsReg != null && listInsReg.Count > 0)
                //{
                //    //已经获取医保端的住院登记信息
                //    return 1;
                //}

                //WF.Expense.Models.InPatient.InsRegister insReg = new Models.InPatient.InsRegister();
                //insReg.Register = (register as WF.Expense.Models.InPatient.Register).Clone();

                //WF.Expense.BI.Impl.Medical.GuangZhouSI.Controls.ucRegisterInfo uc = new WF.Expense.BI.Impl.Medical.GuangZhouSI.Controls.ucRegisterInfo();
                //uc.Register = insReg;

                //WF.Common.UI.Classes.Function.ShowControl(uc);

                //if (uc.IsOK)
                //{
                //    return 1;
                //}
                //else
                //{
                //    this.errorMsg = "未选择医保端的登记信息!";
                //    //失败
                //    return -1;
                //}
                #endregion
            }

            throw new NotImplementedException();
        }

        public int Verification<T>(string registerType, T register)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
