using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.HISFC.Components.OutpatientFee
{
    public class Function
    {
        /// <summary>
        /// 返回执行科室
        /// </summary>
        /// <param name="recipeDept"></param>
        /// <param name="item"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public static ArrayList GetExecDept(FS.FrameWork.Models.NeuObject recipeDept, FS.HISFC.Models.Fee.Item.Undrug item, ref string errorInfo)
        {
            FS.HISFC.BizProcess.Interface.Fee.IExecDept IExecDept=InterfaceManager.GetIExecDept();

            if (IExecDept != null)
            {
                return IExecDept.GetExecDept(recipeDept, item,ref errorInfo);
            }

            return null;
        }

        /// <summary>
        /// 门诊收费二级权限
        /// </summary>
        public const string PrivQuit = "0820";
        /// <summary>
        /// 门诊退其他操作员的三级权限
        /// </summary>
        public const string PrivQuitOtherOperFee = "24";
        /// <summary>
        /// 门诊隔日退费权限
        /// </summary>
        public const string PrivQuitLastDayFee = "25";
        /// <summary>
        /// 门诊未看诊是否能继续收费的三级权限
        /// </summary>
        public const string PrivFeeWhenNoSeeDoc = "26";

        private static Dictionary<string, string> dictionaryYKDept = new Dictionary<string, string>();

        /// <summary>
        ///  判断是否是宜康科室
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public static bool IsContainYKDept(string dept)
        {
            if (dictionaryYKDept == null || dictionaryYKDept.Count == 0)
            {
                ArrayList al = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.Instance.QueryConstant("YkDept");
                if (al != null)
                {
                    foreach (FS.FrameWork.Models.NeuObject obj in al)
                    {
                        dictionaryYKDept[obj.ID] = obj.Name;
                    }
                }
            }

            return dictionaryYKDept.ContainsKey(dept);
        }


        /// <summary>
        /// 读卡接口
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int OperCard(ref string cardNO, ref string errInfo)
        {
            if (InterfaceManager.GetIOperCard() == null)
            {
                errInfo = "没有维护读卡接口！";
                return -1;
            }

            int result = InterfaceManager.GetIOperCard().ReadCardNO(ref cardNO, ref  errInfo);
            if (result == -1)
            {
                errInfo = "读卡失败，请确认是否正确放置诊疗卡！";
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 读取物理卡号
        /// {119F302E-69D9-445c-BF56-4109D975AD98}
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int OperMCard(ref string McardNO, ref string errInfo)
        {
            if (InterfaceManager.GetIOperCard() == null)
            {
                errInfo = "没有维护读卡接口！";
                return -1;
            }

            int result = InterfaceManager.GetIOperCard().ReadMCardNO(ref McardNO, ref  errInfo);
            if (result == -1)
            {
                errInfo = "读卡失败，请确认是否正确放置诊疗卡！";
                return -1;
            }

            return 1;
        }


        public static bool CheckAtmFee(string invoiceno)
        {
            string sql = @"
                            select * from (
                            select * from fin_opb_invoiceinfo  i 
                            start with  i.invoice_no='{0}' and i.trans_type='1'
                            connect  by prior cancel_invoice=invoice_no
                            )
                            where invoice_no like 'T%' and trans_type='1'

                   ";
            sql = string.Format(sql, invoiceno);

            FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();
            System.Data.DataSet ds=new System.Data.DataSet();
            outpatientManager.ExecQuery(sql, ref ds);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


 
        }

    }
}
