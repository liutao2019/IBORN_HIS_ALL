using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Public
{
    /// <summary>
    /// [功能描述: 函数相关]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-9]<br></br>
    /// </summary>
    public class Function
    {
        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <param name="birthday">出生日期</param>
        /// <param name="curTime">当前时间</param>
        /// <returns>返回年月天，不足1天的返回1天</returns>
        public static string GetAge(DateTime birthday, DateTime curTime)
        {
            if ((birthday - curTime).Ticks >= 0)
            {
                return "";
            }
            if (EqualsDate(birthday, curTime))
            {
                return "1天";
            }

            int days = 0;
            int months = 0;
            int years = 0;

            string age = "";

            //减法，天相减
            if (curTime.Day >= birthday.Day)
            {
                days = curTime.Day - birthday.Day;
            }
            else
            {
                //借位，天数不够，借一个月
                months = months - 1;
                DateTime tmpTime = curTime.AddMonths(-1);

                //被借位的那个月总天数比出生日期大，天数=当前日期+被借位月份天数-初始日期               
                if (DateTime.DaysInMonth(tmpTime.Year, tmpTime.Month) > birthday.Day)
                {
                    days = curTime.Day + DateTime.DaysInMonth(tmpTime.Year, tmpTime.Month) - birthday.Day;
                }
                else
                {
                    //否则，天数=当前日期，为什么呢？ 
                  
                    /* 简单点说：
                     * 在1.31号出生的人，2.28或者2.29都不算满月，而在3.1时要算年龄应该是1月1天，期间不存在刚好满一月的情况
                     */
                    /* 复杂情况：
                     * 如果在大月31号出生，而当前日期小于31号并且被借位那个月刚好又是小月，只有30号，甚至是闰月只有28天时就会产生问题；
                     * 同样如果在29、30号出生，而当前日期小于29、30，被借位那个月刚好是闰月也会产生问题；
                     * 到被借位的那个月最后一天不能算整月，再过一天（就是当前月）又是整月多一天，所以不存在刚好整月的那一天
                     */
                    /*
                     * 那么，在1.30、31号出生的人到3.1号时是不是一样大呢？
                     */
                    days = curTime.Day;
                }
            }

            //减法，月相减
            if (curTime.AddMonths(months).Month >= birthday.Month)
            {
                //借月引起年变化
                if (curTime.AddMonths(months).Year < curTime.Year)
                {
                    years = years - 1;
                }
                months = curTime.AddMonths(months).Month - birthday.Month;
            }
            else
            {
                //借位，月份不够，借一年
                years = years - 1;
                months = curTime.AddMonths(months).Month - birthday.Month + 12;
            }
            years = curTime.Year + years - birthday.Year;

            if (years > 0)
            {
                age = years.ToString() + "岁";
            }
            if (months > 0)
            {
                age += months.ToString() + "月";
            }
            if (days > 0)
            {
                age += days.ToString() + "天";
            }

            return age;
        }

        /// <summary>
        /// 比较两个日期是否相等
        /// </summary>
        /// <param name="dt1">第一个日期</param>
        /// <param name="dt2">第二个日期</param>
        /// <returns>true相等 false不相等</returns>
        private static bool EqualsDate(DateTime dt1, DateTime dt2)
        {
            if (dt1.Year == dt2.Year && dt1.Month == dt2.Month && dt1.Day == dt2.Day)
            {
                return true;
            }

            return false;
        }


        #region 创建条码

        /// <summary>
        /// 创建条码信息（二维码）
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static System.Drawing.Image CreateQRCode(string text)
        {
            if (text.Trim() == System.String.Empty)
            {
                //errInfo = "内容为空！";
                return null;
            }
            ThoughtWorks.QRCode.Codec.QRCodeEncoder qrCodeEncoder = new ThoughtWorks.QRCode.Codec.QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 4;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.L;
            System.Drawing.Image image;
            image = qrCodeEncoder.Encode(text, Encoding.GetEncoding("gb2312"));
            return image;
        }

        /// <summary>
        /// 创建人员条码信息（二维码）
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static System.Drawing.Image CreateQRCode(FS.HISFC.Models.RADT.Patient patient)
        {
            string strPInfo = "";

            if (patient is FS.HISFC.Models.RADT.PatientInfo)
            {
                FS.HISFC.Models.RADT.PatientInfo pInfo = patient as FS.HISFC.Models.RADT.PatientInfo;

                strPInfo = "UPID:" + pInfo.PID.PatientNO + "||"
                    + "cis_id:" + pInfo.ID + "||"
                    + "pid:" + pInfo.PID.CardNO + "||"
                    + "in_patient_no:" + pInfo.PID.PatientNO + "||"    //{0573DB2C-535C-41a2-8B74-D35276CCB69E}
                    + "birth_time:" + pInfo.Birthday.ToString() + "||"
                    + "bed_no:" + (!string.IsNullOrEmpty(pInfo.PVisit.PatientLocation.Bed.ID) ? pInfo.PVisit.PatientLocation.Bed.ID.Substring(4) : "") + "||"
                    + "sex:" + pInfo.Sex.ID.ToString() + "||"
                    + "social_no:" + pInfo.IDCard;
            }
            else if (patient is FS.HISFC.Models.Registration.Register)
            {
                FS.HISFC.Models.Registration.Register regObj = patient as FS.HISFC.Models.Registration.Register;

                strPInfo = "UPID:" + regObj.PID.PatientNO + "||"
                    + "cis_id:" + regObj.ID + "||"
                    + "pid:" + regObj.PID.CardNO + "||"
                    + "in_patient_no:" + regObj.PID.PatientNO + "||"    //{0573DB2C-535C-41a2-8B74-D35276CCB69E}
                    + "birth_time:" + regObj.Birthday.ToString() + "||"
                    + "bed_no:" + "" + "||"
                    + "sex:" + regObj.Sex.ID.ToString() + "||"
                    + "social_no:" + regObj.IDCard;
            }
            else
            {
                //errInfo = "患者不能识别！";
                return null;
            }
            return CreateQRCode(strPInfo);
        }

        /// <summary>
        /// 创建条码信息（一维码）
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static System.Drawing.Image CreateBarCode(string text, int width, int height)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            b.IncludeLabel = true;
            return b.Encode(BarcodeLib.TYPE.CODE128, text, width, height);
        }

        /// <summary>
        /// 创建人员条码信息（一维码）
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static System.Drawing.Image CreateBarCode(FS.HISFC.Models.RADT.Patient patient, int width, int height)
        {
            try
            {
                return CreateBarCode(patient.PID.PatientNO, width, height);
            }
            catch (Exception ex)
            {
                //errInfo = ex.Message;
                return null;
            }
        }

        #endregion
    }
}
