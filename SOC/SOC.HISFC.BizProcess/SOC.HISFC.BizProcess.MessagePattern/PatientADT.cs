using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HL7Message;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.ADT
{
    public class PatientADT : IADT
    {
        #region IADT 成员

        /// <summary>
        /// 住院结算
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="positive"></param>
        /// <returns></returns>
        public int Balance(FS.HISFC.Models.RADT.PatientInfo patientInfo, bool positive)
        {
            Object o=null;
            if (positive)
            {
                return new MessageSender.ADT_A03_Sender().Send(patientInfo,ref o, ref this.err);
            }
            else
            {
                return new MessageSender.ADT_A13_Sender().Send(patientInfo, ref o, ref this.err);
            }
        }

     

        /// <summary>
        /// 分诊、取消分诊
        /// </summary>
        /// <param name="assign"></param>
        /// <param name="positive"></param>
        /// <param name="state">0 分诊 1 进诊</param>
        /// <returns></returns>
        public int AssignInfo(FS.HISFC.Models.Nurse.Assign assign, bool positive, int state)
        {
            Object o=null;
            if (state == 0)
            {
                if (positive)
                {
                    return new MessageSender.ADT_A02_Sender().Send(assign, ref o, ref this.err);
                }
                else
                {
                    return new MessageSender.ADT_A12_Sender().Send(assign, ref o, ref this.err);
                }
            }
            else if (state == 1)
            {
                if (positive)
                {
                    return 1;
                }
                else
                {
                    return new MessageSender.ADT_A32_Sender().Send(assign, ref o, ref this.err);
                }
            }
            return 1;
        }

 



        #endregion

        private string err = string.Empty;
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

        #region IADT 成员

        /// <summary>
        /// 修改病人信息
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int PatientInfo(FS.HISFC.Models.RADT.Patient patient, object patientInfo)
        {
            Object o = null;

            return new MessageSender.ADT_A08_Sender().Send(patientInfo, ref o, ref this.err, patient);

        }

        /// <summary>
        /// 登记信息
        /// </summary>
        /// <param name="register"></param>
        /// <param name="positive"></param>
        /// <returns></returns>
        public int Register(object register, bool positive)
        {
            Object o = null;

           if (register is FS.HISFC.Models.RADT.PatientInfo) //住院系统
            {
                if (positive)
                {
                    return new MessageSender.ADT_A01_Sender().Send(register , ref o, ref this.err);
                }
                else
                {
                    return new MessageSender.ADT_A11_Sender().Send(register , ref o, ref this.err);
                }
            }
            else  //其他系统
            {
               
                if (positive)
                {
                    return new MessageSender.ADT_A04_Sender().Send(register, ref o, ref this.err);
                }
                else
                {
                    return new MessageSender.ADT_A11_Sender().Send(register, ref o, ref this.err);
                }
            }
    


         

        }

   

        #endregion

        #region IADT 成员

        /// <summary>
        /// 预交金
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="alprepay"></param>
        /// <returns></returns>
        public int Prepay(FS.HISFC.Models.RADT.PatientInfo patient, ArrayList alprepay,string flag)
        {
            Object o = null;
            return new MessageSender.DFT_P03_Sender().Send(patient, ref o, ref this.err,alprepay,flag);
        }


        /// <summary>
        /// 查询预约挂号
        /// </summary>
        /// <param name="alSchema"></param>
        /// <returns></returns>
        public int QueryBookingNumber(ArrayList alSchema)
        {
            Object o = null;
            return new MessageSender.SQM_S25_Sender().Send(new FS.HISFC.Models.Registration.Schema(), ref o, ref this.err, alSchema);

        }
        #endregion





    }
}
