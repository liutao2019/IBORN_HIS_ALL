using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.Components.Order
{
    public class Function
    {

        /// <summary>
        /// 读取病历号
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int OperCard(ref string cardNO, ref string errInfo)
        {
            if (CacheManager.GetIOperCard() == null)
            {
                errInfo = "没有维护读卡接口！";
                return -1;
            }

            int result = CacheManager.GetIOperCard().ReadCardNO(ref cardNO, ref  errInfo);
            if (result == -1)
            {
                errInfo = "读卡失败，请确认是否正确放置诊疗卡！";
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 读取物理卡号
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int OperMCard(ref string McardNO, ref string errInfo)
        {
            if (CacheManager.GetIOperCard() == null)
            {
                errInfo = "没有维护读卡接口！";
                return -1;
            }

            int result = CacheManager.GetIOperCard().ReadMCardNO(ref McardNO, ref  errInfo);
            if (result == -1)
            {
                errInfo = "读卡失败，请确认是否正确放置诊疗卡！";
                return -1;
            }

            return 1;
        }
        /// <summary>
        /// 写入病历号 // {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int WriterCardNo(string cardNO, ref string errInfo)
        {
            if (CacheManager.GetIOperCard() == null)
            {
                errInfo = "没有维护读卡接口！";
                return -1;
            }

            int result = CacheManager.GetIOperCard().WriterCardNO(3, cardNO, ref  errInfo);//病历号3扇区
            if (result == -1)
            {
                errInfo = "读卡失败，请确认是否正确放置诊疗卡！";
                return -1;
            }

            return 1;
        }
        /// <summary>
        /// 写入病历号
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int WriterMCard(string mCardNO, ref string errInfo)
        {
            if (CacheManager.GetIOperCard() == null)
            {
                errInfo = "没有维护读卡接口！";
                return -1;
            }

            int result = CacheManager.GetIOperCard().WriterCardNO(2, mCardNO, ref  errInfo);//卡面好2扇区
            if (result == -1)
            {
                errInfo = "读卡失败，请确认是否正确放置诊疗卡！";
                return -1;
            }

            return 1;
        }
    }
}
