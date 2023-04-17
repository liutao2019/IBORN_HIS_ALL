using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FS.SOC.Local.DrugStore.ZhuHai.Common
{
    /// <summary>
    /// [功能描述: 本地化公用函数]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-1]<br></br>
    /// 说明：
    /// </summary>
    public class Function
    {
        /// <summary>
        /// 转换剂量单位
        /// 逻辑太多，修改时认真思考
        /// </summary>
        /// <param name="applyOut">出库申请实体</param>
        /// <returns></returns>
        public static string GetOnceDose(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            if (applyOut.DoseOnce == 0)
            {
                return "";
            }

            //如果剂量单位空
            if (string.IsNullOrEmpty(applyOut.Item.DoseUnit))
            {
                return applyOut.DoseOnce.ToString("F2").TrimEnd('0').TrimEnd('.');
            }

            //applyout中没有基本剂量
            FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID);
            if (item == null)
            {
                return applyOut.DoseOnce.ToString("F2").TrimEnd('0').TrimEnd('.') + applyOut.Item.DoseUnit;
            }

            //剂量单位和最小单位相同
            if (applyOut.Item.MinUnit == applyOut.Item.DoseUnit)
            {
                //剂量单位和最小单位相同，但是基本剂量不为1则基本剂量维护错误，不转换
                //增加药品基本信息中的判断，因为applyOut表中的剂量单位也存在存最小单位的情况
                if (item.BaseDose != 1m && item.MinUnit == item.DoseUnit)
                {
                    return applyOut.DoseOnce.ToString("F2").TrimEnd('0').TrimEnd('.') + applyOut.Item.DoseUnit;
                }

                //第二基本剂量单位情况处理
                if (item.SecondBaseDose != 1m && item.MinUnit == item.SecondDoseUnit)
                {
                    return applyOut.DoseOnce.ToString("F2").TrimEnd('0').TrimEnd('.') + applyOut.Item.DoseUnit;
                }
                return ConvertDoseOnce(applyOut.DoseOnce, applyOut.Item.DoseUnit) + applyOut.Item.DoseUnit;
            }
            else
            {
                //剂量单位和最小单位不相同，但是基本剂量为1则基本剂量维护错误，不转换
                //第二基本剂量单位情况处理
                if ((item.BaseDose == 1m && applyOut.Item.DoseUnit == item.DoseUnit)
                    || (item.SecondBaseDose == 1m && applyOut.Item.DoseUnit == item.SecondDoseUnit))
                {
                    string labelPrintConvertAways = "-1";
                    string fileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientDrugStorePrintSetting.xml";
                    if (System.IO.File.Exists(fileName))
                    {
                        labelPrintConvertAways = SOC.Public.XML.SettingFile.ReadSetting(fileName, "Label", "OnceDoseConvertAways", "False");
                    }
                    else
                    {
                        SOC.Public.XML.SettingFile.SaveSetting(fileName, "Label", "OnceDoseConvertAways", "False");
                    }
                    if (labelPrintConvertAways == "False")
                    {
                        return applyOut.DoseOnce.ToString("F2").TrimEnd('0').TrimEnd('.') + applyOut.Item.DoseUnit;
                    }
                }
                //滴眼液默认不转换。
                else if (item.Name.Contains("滴眼液"))
                {
                    return applyOut.DoseOnce.ToString("F2").TrimEnd('0').TrimEnd('.') + applyOut.Item.DoseUnit;
                }
            }

            //如果剂量单位是中文
            if (!SOC.Public.Char.IsLetterOrNumber(applyOut.Item.DoseUnit[0]))
            {
                return ConvertDoseOnce(applyOut.DoseOnce, applyOut.Item.DoseUnit) + applyOut.Item.DoseUnit;
            }           

            decimal baseDose = 0;

            //新增第二基本剂量判断,处理当applyout表中存的剂量单位为第二基本剂量的情况
            if (item.SecondDoseUnit == applyOut.Item.DoseUnit)
            {
                baseDose = item.SecondBaseDose;
            }
            else
            {
                baseDose = item.BaseDose;
            }

            if (baseDose == 0)
            {
                return ConvertDoseOnce(applyOut.DoseOnce, applyOut.Item.DoseUnit) + applyOut.Item.DoseUnit;
            }
            //取整数部分
            decimal minQty = System.Math.Floor(applyOut.DoseOnce / baseDose);
            //取小数部分
            decimal doseQty = applyOut.DoseOnce / baseDose - minQty;

            if (doseQty == 0)//是整最小单位
            {
                return minQty.ToString("F2").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
            }

            //分数转换，先采用准确计算，然后近似计算
            string onceDose = "";
            if (doseQty == 0.5m)
            {
                onceDose = "半" + applyOut.Item.MinUnit;
            }
            else if (3 * (applyOut.DoseOnce - minQty * baseDose) == baseDose)
            {
                //分数转换，先采用准确计算，后面采用近似计算
                onceDose = "1/3" + applyOut.Item.MinUnit;
            }
            else if (3 * (applyOut.DoseOnce - minQty * baseDose) == 2 * baseDose)
            {
                //分数转换，先采用准确计算，后面采用近似计算
                onceDose = "2/3" + applyOut.Item.MinUnit;
            }
            else if (doseQty == 0.25m)
            {
                onceDose = "1/4" + applyOut.Item.MinUnit;
            }
            else if (doseQty == 0.75m)
            {
                onceDose = "3/4" + applyOut.Item.MinUnit;
            }
            else if (doseQty == 0.2m)
            {
                onceDose = "1/5" + applyOut.Item.MinUnit;
            }
            else if (doseQty == 0.4m)
            {
                onceDose = "2/5" + applyOut.Item.MinUnit;
            }
            else if (doseQty == 0.6m)
            {
                onceDose = "3/5" + applyOut.Item.MinUnit;
            }
            else if (doseQty == 0.8m)
            {
                onceDose = "4/5" + applyOut.Item.MinUnit;
            }

            //onceDose为空，则还没有转换为分数形式，不为空的转换成分数形式后还判断是否大于一个最小单位，大于的话也不转换
            if (string.IsNullOrEmpty(onceDose))
            {
                if (minQty == 0)//不够一个最小单位，采用近似计算
                {
                    if (Math.Round(doseQty, 1) == 0.3m)
                    {
                        return "1/3" + applyOut.Item.MinUnit;
                    }
                    else if (Math.Round(doseQty, 1) == 0.6m || Math.Round(doseQty, 1) == 0.7m)
                    {
                        return "2/3" + applyOut.Item.MinUnit;
                    }
                }
                return applyOut.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.DoseUnit;
            }
            else
            {
                if (doseQty == 0)//是整最小单位
                {
                    return minQty.ToString("F2").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
                }
                else if (minQty == 0)//不够一个最小单位
                {
                    return onceDose;
                }
            }

            return (applyOut.DoseOnce / baseDose).ToString("F2").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
        }


        /// <summary>
        /// 转换剂量单位
        /// 逻辑太多，修改时认真思考
        /// </summary>
        /// <param name="applyOut">出库申请实体</param>
        /// <returns></returns>
        public static string GetOnceDoseGYZL(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            //如果剂量单位空
            if (string.IsNullOrEmpty(applyOut.Item.DoseUnit))
            {
                return applyOut.DoseOnce.ToString("F2").TrimEnd('0').TrimEnd('.');
            }

            //applyout中没有基本剂量
            FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID);
            if (item == null)
            {
                return applyOut.DoseOnce.ToString("F2").TrimEnd('0').TrimEnd('.') + applyOut.Item.DoseUnit;
            }

            //剂量单位和最小单位相同
            if (applyOut.Item.MinUnit == applyOut.Item.DoseUnit)
            {
                //剂量单位和最小单位相同，但是基本剂量不为1则基本剂量维护错误，不转换
                //增加药品基本信息中的判断，因为applyOut表中的剂量单位也存在存最小单位的情况
                if (item.BaseDose != 1m && item.MinUnit == item.DoseUnit)
                {
                    return applyOut.DoseOnce.ToString("F2").TrimEnd('0').TrimEnd('.') + applyOut.Item.DoseUnit;
                }

                //第二基本剂量单位情况处理
                if (item.SecondBaseDose != 1m && item.MinUnit == item.SecondDoseUnit)
                {
                    return applyOut.DoseOnce.ToString("F2").TrimEnd('0').TrimEnd('.') + applyOut.Item.DoseUnit;
                }
                return ConvertDoseOnce(applyOut.DoseOnce, applyOut.Item.DoseUnit) + applyOut.Item.DoseUnit;
            }
            else
            {
                //剂量单位和最小单位不相同，但是基本剂量为1则基本剂量维护错误，不转换
                //第二基本剂量单位情况处理
                if ((item.BaseDose == 1m && applyOut.Item.DoseUnit == item.DoseUnit)
                    || (item.SecondBaseDose == 1m && applyOut.Item.DoseUnit == item.SecondDoseUnit))
                {
                    string labelPrintConvertAways = "-1";
                    string fileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientDrugStorePrintSetting.xml";
                    if (System.IO.File.Exists(fileName))
                    {
                        labelPrintConvertAways = SOC.Public.XML.SettingFile.ReadSetting(fileName, "Label", "OnceDoseConvertAways", "False");
                    }
                    else
                    {
                        SOC.Public.XML.SettingFile.SaveSetting(fileName, "Label", "OnceDoseConvertAways", "False");
                    }
                    if (labelPrintConvertAways == "False")
                    {
                        return applyOut.DoseOnce.ToString("F2").TrimEnd('0').TrimEnd('.') + applyOut.Item.DoseUnit;
                    }
                }
                //滴眼液默认不转换。
                else if (item.Name.Contains("滴眼液"))
                {
                    return applyOut.DoseOnce.ToString("F2").TrimEnd('0').TrimEnd('.') + applyOut.Item.DoseUnit;
                }
            }

            //如果剂量单位是中文
            //if (!SOC.Public.Char.IsLetterOrNumber(applyOut.Item.DoseUnit[0]))
            //{
            //    return ConvertDoseOnce(applyOut.DoseOnce, applyOut.Item.DoseUnit) + applyOut.Item.DoseUnit;
            //}

            decimal baseDose = 0;

            //新增第二基本剂量判断,处理当applyout表中存的剂量单位为第二基本剂量的情况
            if (item.SecondDoseUnit == applyOut.Item.DoseUnit)
            {
                baseDose = item.SecondBaseDose;
            }
            else
            {
                baseDose = item.BaseDose;
            }

            if (baseDose == 0)
            {
                return ConvertDoseOnce(applyOut.DoseOnce, applyOut.Item.DoseUnit) + applyOut.Item.DoseUnit;
            }
            //取整数部分
            decimal minQty = System.Math.Floor(applyOut.DoseOnce / baseDose);
            //取小数部分
            decimal doseQty = applyOut.DoseOnce / baseDose - minQty;

            if (doseQty == 0)//是整最小单位
            {
                return minQty.ToString("F2").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
            }

            //分数转换，先采用准确计算，然后近似计算
            string onceDose = "";
            if (doseQty == 0.5m)
            {
                onceDose = "半" + applyOut.Item.MinUnit;
            }
            else if (3 * (applyOut.DoseOnce - minQty * baseDose) == baseDose)
            {
                //分数转换，先采用准确计算，后面采用近似计算
                onceDose = "1/3" + applyOut.Item.MinUnit;
            }
            else if (3 * (applyOut.DoseOnce - minQty * baseDose) == 2 * baseDose)
            {
                //分数转换，先采用准确计算，后面采用近似计算
                onceDose = "2/3" + applyOut.Item.MinUnit;
            }
            else if (doseQty == 0.25m)
            {
                onceDose = "1/4" + applyOut.Item.MinUnit;
            }
            else if (doseQty == 0.75m)
            {
                onceDose = "3/4" + applyOut.Item.MinUnit;
            }
            else if (doseQty == 0.2m)
            {
                onceDose = "1/5" + applyOut.Item.MinUnit;
            }
            else if (doseQty == 0.4m)
            {
                onceDose = "2/5" + applyOut.Item.MinUnit;
            }
            else if (doseQty == 0.6m)
            {
                onceDose = "3/5" + applyOut.Item.MinUnit;
            }
            else if (doseQty == 0.8m)
            {
                onceDose = "4/5" + applyOut.Item.MinUnit;
            }

            //onceDose为空，则还没有转换为分数形式，不为空的转换成分数形式后还判断是否大于一个最小单位，大于的话也不转换
            if (string.IsNullOrEmpty(onceDose))
            {
                if (minQty == 0)//不够一个最小单位，采用近似计算
                {
                    if (Math.Round(doseQty, 1) == 0.3m)
                    {
                        return "1/3" + applyOut.Item.MinUnit;
                    }
                    else if (Math.Round(doseQty, 1) == 0.6m || Math.Round(doseQty, 1) == 0.7m)
                    {
                        return "2/3" + applyOut.Item.MinUnit;
                    }
                }
                return applyOut.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.DoseUnit;
            }
            else
            {
                if (doseQty == 0)//是整最小单位
                {
                    return minQty.ToString("F2").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
                }
                else if (minQty == 0)//不够一个最小单位
                {
                    return onceDose;
                }
            }

            return (applyOut.DoseOnce / baseDose).ToString("F2").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
        }

        /// <summary>
        /// 分数转换
        /// </summary>
        /// <param name="value">转换成分数的小数值</param>
        /// <returns></returns>
        private static string ConvertDoseOnce(decimal value,string doseUnit)
        {
            if (string.IsNullOrEmpty(doseUnit))
            {
                return value.ToString("F2").TrimEnd('0').TrimEnd('.');
            }
            //英文数字单位不转换
            if (SOC.Public.Char.IsLetterOrNumber(doseUnit[0]))
            {
                return value.ToString("F2").TrimEnd('0').TrimEnd('.');
            }

            if (value == 0.2m)
            {
                return "1/5";
            }
            else if (value == 0.3m || value == 0.33m || value == 0.333m || value == 0.3333m)
            {
                return "1/3";
            }
            else if (value == 0.4m)
            {
                return "2/5";
            }
            else if (value == 0.5m)
            {
                return "半";
            }
            else if (value == 0.6m|| value == 0.66m || value == 0.666m || value == 0.6666m)
            {
                return "2/3";
            }
            else if (value == 0.7m || value == 0.67m || value == 0.667m || value == 0.6667m)
            {
                return "2/3";
            }
            else if (value == 0.8m)
            {
                return "4/5";
            }
            return value.ToString("F2").TrimEnd('0').TrimEnd('.');
        }        

        /// <summary>
        /// 获取频次名称
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public static string GetFrequenceName(FS.HISFC.Models.Order.Frequency frequency)
        {
            string id = frequency.ID.ToLower().Replace(".","");
            if (id == "qd")//每天一次
            {
                return "每天一次";
            }
            else if (id == "bid")//每天两次
            {
                return "每天两次";
            }
            else if (id == "tid")//每天三次
            {
                return "每天三次";
            }
            else if (id == "hs")//睡前
            {
                return "睡前";
            }
            else if (id == "qn")//每晚一次
            {
                return "每晚一次";
            }
            else if (id == "qid")//每天四次
            {
                return "每天四次";
            }
            else if (id == "pcd")//晚餐后
            {
                return "晚餐后";
            }
            else if (id == "pcl")//午餐后
            {
                return "午餐后";
            }
            else if (id == "pcm")//早餐后
            {
                return "早餐后";
            }
            else if (id == "prn")//必要时服用
            {
                return "必要时用";
            }
            else if (id == "遵医嘱")
            {
                return "遵医嘱";
            }
            if (string.IsNullOrEmpty(frequency.Name))
            {
                FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
                string fName = dbMgr.ExecSqlReturnOne("SELECT FREQUENCY_NAME FROM MET_COM_DEPTFREQUENCY WHERE FREQUENCY_CODE ='" + frequency.ID + "'");
                if (fName != "-1")
                {
                    return fName;
                }
            }
            return frequency.Name;
        }

        /// <summary>
        /// 获取医嘱的备注天数
        /// </summary>
        /// <param name="OrderNO"></param>
        /// <returns></returns>
        public static FS.HISFC.Models.Order.OutPatient.Order GetOrder(string orderNO)
        {
            FS.HISFC.BizLogic.Order.OutPatient.Order outpatientOrderManager = new FS.HISFC.BizLogic.Order.OutPatient.Order();
            FS.HISFC.Models.Order.OutPatient.Order outpatientOrder = outpatientOrderManager.QueryOneOrder(orderNO);
            return outpatientOrder;
        }

        /// <summary>
        /// 获取摆药单是否打印药袋
        /// </summary>
        /// <param name="drugBillClassNO"></param>
        /// <returns></returns>
        public static bool IsPrintInpatientDrugBag(string drugBillClassNO)
        {
            return FS.FrameWork.Function.NConvert.ToBoolean(SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStorePrintSetting.xml", "BillNO" + drugBillClassNO, "PrintInpatientDrugBag", "0"));
        }
        /// <summary>
        /// 获取摆药单是否打印标签
        /// </summary>
        /// <param name="drugBillClassNO"></param>
        /// <returns></returns>
        public static bool IsPrintInpatientDrugLabel(string drugBillClassNO)
        {
            return FS.FrameWork.Function.NConvert.ToBoolean(SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStorePrintSetting.xml", "BillNO" + drugBillClassNO, "PrintInpatientDrugLabel", "0"));
        }
        /// <summary>
        /// 获取明细打印为主的摆药单是否打印汇总单
        /// </summary>
        /// <param name="drugBillClassNO"></param>
        /// <returns></returns>
        public static bool IsPrintInpatientDrugTot(string drugBillClassNO)
        {
            return FS.FrameWork.Function.NConvert.ToBoolean(SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStorePrintSetting.xml", "BillNO" + drugBillClassNO, "PrintInpatientDrugTot", "0"));
        }

        public static string GetAge(DateTime birthday)
        {
            if (birthday > DateTime.Now.AddYears(-200))
            {
                FS.HISFC.BizLogic.Manager.Person persomMgr = new FS.HISFC.BizLogic.Manager.Person();
                {
                    return persomMgr.GetAge(birthday);
                }
            }
            return "";
        }

        /// <summary>
        /// 获取住院主表诊断
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public static string GetInpatientDiagnose(string inpatientNO)
        {
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            string diagName = dbMgr.ExecSqlReturnOne("select i.diag_name from fin_ipr_inmaininfo i where i.inpatient_no = '" + inpatientNO + "'");
            return diagName;
        }

        /// <summary>
        /// 生成条形码方法
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Image CreateBarCode(string code, int width, int height)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;
            b.IncludeLabel = true;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, width, height);
        }
    }
}
