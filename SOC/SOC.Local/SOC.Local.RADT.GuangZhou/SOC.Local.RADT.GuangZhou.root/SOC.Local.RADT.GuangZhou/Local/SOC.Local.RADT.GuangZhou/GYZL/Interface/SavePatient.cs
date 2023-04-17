using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Neusoft.SOC.HISFC.BizProcess.CommonInterface;
using Neusoft.FrameWork.Management;
using System.Windows.Forms;
using Neusoft.SOC.HISFC.BizProcess.CommonInterface.Common;

namespace Neusoft.SOC.Local.RADT.GuangZhou.GYZL.Interface
{
    public class SavePatient : Neusoft.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<Neusoft.HISFC.Models.RADT.PatientInfo>
    {
        #region ISave 成员

        /// <summary>
        /// 保存住院登记后调用
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int Saved(EnumSaveType saveType, Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            if (saveType == EnumSaveType.Insert)
            {
                //调用病案打印接口
                Neusoft.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface IHealthRecord = ControllerFactroy.CreateFactory().CreateInferface<Neusoft.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface>(typeof(SavePatient), null);

                if (IHealthRecord != null)
                {
                    DialogResult rs = CommonController.CreateInstance().MessageBox("是否打印病案?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rs == DialogResult.Yes)
                    {
                        Neusoft.HISFC.Models.HealthRecord.Base parmPatientinfo = new Neusoft.HISFC.Models.HealthRecord.Base();
                        parmPatientinfo.PatientInfo = patient;
                        IHealthRecord.ControlValue(parmPatientinfo);
                        IHealthRecord.Print();
                    }
                }
                else
                {
                    //this.err = "打印接口：Neusoft.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface的实现未正确配置!\n请配置打印病案接口实现：" + this.GetType();
                    //return -1;
                }

                //调用预交金接口
                if (patient.FT.PrepayCost > 0)
                {
                    Neusoft.HISFC.BizProcess.Interface.FeeInterface.IPrepayPrint IPrepayPrint = ControllerFactroy.CreateFactory().CreateInferface<Neusoft.HISFC.BizProcess.Interface.FeeInterface.IPrepayPrint>(typeof(SavePatient), null);
                    if (IPrepayPrint == null)
                    {
                        this.err = "打印接口：Neusoft.HISFC.BizProcess.Interface.FeeInterface.IPrepayPrint的实现未正确配置!\n请配置预交金打印接口实现：" + this.GetType();
                        return -1;
                    }
                    else
                    {
                        if (patient.IsEncrypt)
                        {
                            patient.Name = Neusoft.FrameWork.WinForms.Classes.Function.Decrypt3DES(patient.NormalName);
                        }
                        IPrepayPrint.Clear();
                        //获取预交金
                        Neusoft.HISFC.BizLogic.Fee.InPatient inPatient = new Neusoft.HISFC.BizLogic.Fee.InPatient();
                        Neusoft.HISFC.Models.Fee.Inpatient.Prepay prepay = inPatient.QueryPrePay(patient.ID, "1");
                        if (prepay != null)
                        {
                            IPrepayPrint.SetValue(patient, prepay);
                            IPrepayPrint.Print();
                        }
                    }
                }

                //调用担保金接口
                if (patient.Surety.SuretyCost > 0)
                {
                    Neusoft.HISFC.BizProcess.Interface.FeeInterface.IPrintSurety ISuretyPrint = ControllerFactroy.CreateFactory().CreateInferface<Neusoft.HISFC.BizProcess.Interface.FeeInterface.IPrintSurety>(typeof(SavePatient), null);
                    if (ISuretyPrint != null)
                    {
                        ISuretyPrint.SetValue(patient);
                        ISuretyPrint.Print();
                    }
                    else
                    {
                        //this.err = "打印接口：Neusoft.HISFC.BizProcess.Interface.FeeInterface.IPrintSurety的实现未正确配置!\n请配置担保金打印接口实现：" + this.GetType();
                        //return -1;
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// 保存前调用方法
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int Saving(EnumSaveType saveType, Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            return 1;
        }

        /// <summary>
        /// 提交事务前调用接口
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int SaveCommitting(EnumSaveType saveType, Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            //调用HL7接口
            Neusoft.SOC.HISFC.BizProcess.MessagePatternInterface.IADT IInPatientADT = InterfaceManager.GetIADT();
            if (IInPatientADT != null)
            {
                if (saveType == EnumSaveType.Insert)
                {
                    if (IInPatientADT.Register(patientInfo, true) == -1)
                    {
                        this.err = "住院登记失败，请向系统管理员报告错误信息：" + IInPatientADT.Err;
                        return -1;
                    }
                }
                else if (saveType == EnumSaveType.Update)
                {
                    if (IInPatientADT.PatientInfo(patientInfo,patientInfo) == -1)
                    {
                        this.err = "修改患者信息失败，请向系统管理员报告错误信息：" + IInPatientADT.Err;
                        return -1;
                    }
                }
            }

            return 1;
        }

        #endregion

        #region IErr 成员

        private string err = "";
        public string Err
        {
            get
            {
                return this.err;
            }
            set
            {
                this.err = value;
            }
        }

        #endregion
    }
}
