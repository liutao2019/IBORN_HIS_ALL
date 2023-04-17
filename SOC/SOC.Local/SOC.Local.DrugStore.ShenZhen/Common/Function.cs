using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
using System.Collections.Specialized;

namespace FS.SOC.Local.DrugStore.ShenZhen.Common
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
            }

            //如果剂量单位是中文
            //if (!SOC.Public.Char.IsLetterOrNumber(applyOut.Item.DoseUnit[0]))
            Regex r = new Regex(@"[\u4e00-\u9fa5]+");
            Match mc = r.Match(applyOut.Item.DoseUnit);
            if (mc.Length != 0)
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
                if (IsSpecialUsage(applyOut.Usage.ID))//判断特殊用法
                {
                    return applyOut.DoseOnce + applyOut.Item.DoseUnit;
                }
                else
                {
                    return minQty.ToString("F2").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
                }
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
                    if (Math.Round(doseQty, 2) == 0.33m)
                    {
                        return "1/3" + applyOut.Item.MinUnit;
                    }
                    if (Math.Round(doseQty, 2) == 0.25m)
                    {
                        return "1/4" + applyOut.Item.MinUnit;
                    }
                    else if (Math.Round(doseQty, 1) == 0.6m || Math.Round(doseQty, 1) == 0.7m)
                    {
                        return "2/3" + applyOut.Item.MinUnit;
                    }
                    if (Math.Round(doseQty, 1) == 0.5m)
                    {
                        return "1/2" + applyOut.Item.MinUnit;
                    }
                    if (Math.Round(doseQty, 2) == 0.17m || Math.Round(doseQty, 2) == 0.16m)
                    { 
                      return "1/6" + applyOut.Item.MinUnit;
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
        private static string ConvertDoseOnce(decimal value, string doseUnit)
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
            else if (value == 0.6m || value == 0.66m || value == 0.666m || value == 0.6666m)
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
        /// 获取医嘱的备注天数
        /// </summary>
        /// <param name="OrderNO"></param>
        /// <returns></returns>
        public static FS.HISFC.Models.Order.OutPatient.Order GetOrder(string orderNO)
        {
            FS.HISFC.BizLogic.Order.OutPatient.Order outpatientOrderManager = new FS.HISFC.BizLogic.Order.OutPatient.Order();
            FS.HISFC.Models.Order.OutPatient.Order outpatientOrder = outpatientOrderManager.QueryOneOrder(orderNO);
            if (outpatientOrder == null)
            {
                outpatientOrder = new FS.HISFC.Models.Order.OutPatient.Order();
            }
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

        /// <summary>
        /// 根据出生日期获取年龄
        /// </summary>
        /// <param name="birthday"></param>
        /// <returns></returns>
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

        public static System.Collections.Hashtable hsPlaceNO = new System.Collections.Hashtable();

        /// <summary>
        /// 获取库存货位号
        /// </summary>
        /// <param name="applyOut">出库申请实体</param>
        /// <returns></returns>
        public static string GetStockPlaceNO(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            if (string.IsNullOrEmpty(applyOut.PlaceNO))
            {
                string key = applyOut.StockDept.ID + applyOut.Item.ID;
                if (hsPlaceNO.Contains(key))
                {
                    return hsPlaceNO[key].ToString();
                }

                FS.SOC.HISFC.BizLogic.Pharmacy.Storage storageMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Storage();

                string placeNO = storageMgr.GetPlaceNO(applyOut.StockDept.ID, applyOut.Item.ID);
                hsPlaceNO.Add(key, placeNO);
                return placeNO;
            }

            return applyOut.PlaceNO;
        }

        public static System.Collections.Hashtable hsPactUnit = new System.Collections.Hashtable();

        /// <summary>
        /// 获取合同单位名称
        /// </summary>
        /// <param name="pactUnitNO">合同单位编码</param>
        /// <returns></returns>
        public static string GetPactUnitName(string pactUnitNO)
        {
            if (hsPlaceNO.Contains(pactUnitNO))
            {
                return hsPlaceNO[pactUnitNO].ToString();
            }
            try
            {
                FS.HISFC.BizProcess.Integrate.Manager inteMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                string pactUnitName = inteMgr.GetPactUnitInfoByPactCode(pactUnitNO).Name;
                hsPlaceNO.Add(pactUnitNO, pactUnitName);
                return pactUnitName;
            }
            catch { }

            return "";
        }

        /// <summary>
        /// 获取二维条形码图片，注：没有异常处理
        /// </summary>
        /// <param name="code">需要编码的字符串</param>
        /// <returns>二维码图片，null第三方dll返回值不是图片格式</returns>
        public static System.Drawing.Image Create2DBarcode(string code)
        {
            FS.SOC.Public.Assembly.Models.SOCPropert propertInfo = new FS.SOC.Public.Assembly.Models.SOCPropert();
            propertInfo.Name = "QRCodeErrorCorrect";
            propertInfo.Value = "H";

            List<FS.SOC.Public.Assembly.Models.SOCPropert> listPropert = new List<FS.SOC.Public.Assembly.Models.SOCPropert>();
            listPropert.Add(propertInfo);

            object value = FS.SOC.Public.Assembly.Method.Invoke
                (
                "ThoughtWorks.QRCode.dll",//dll名称
                "ThoughtWorks.QRCode.Codec.QRCodeEncoder",//类名称
                "Encode",//方法名称
                new Type[] { typeof(string) },//参数类型，必须保证顺序、个数一致
                new object[] { code },//参数，必须保证顺序类型、正确
                listPropert//属性值，指定属性名称和属性的值
                );

            if (value is System.Drawing.Image)
            {
                return value as System.Drawing.Image;
            }
            return null;
        }

        /// <summary>
        /// 生成条形码方法
        /// </summary>
        /// <param name="code">条形码编码</param>
        /// <param name="with">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>null 提供条形码的dll中返回值不是imge类型</returns>
        public static System.Drawing.Image CreateBarCode(string code, int with, int height)
        {
            List<FS.SOC.Public.Assembly.Models.SOCPropert> listPropert = new List<FS.SOC.Public.Assembly.Models.SOCPropert>();
            FS.SOC.Public.Assembly.Models.SOCPropert propertInfo = new FS.SOC.Public.Assembly.Models.SOCPropert();
            propertInfo.Name = "IncludeLabel";
            propertInfo.Value = "True";
            listPropert.Add(propertInfo);

            propertInfo = new FS.SOC.Public.Assembly.Models.SOCPropert();
            propertInfo.Name = "Alignment";
            propertInfo.Value = "CENTER";
            listPropert.Add(propertInfo);

            //反射dll
            System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom("BarcodeLib.dll");

            //获取dll中的class
            Type tType = assembly.GetType("BarcodeLib.TYPE");

            //参数类型，必须保证顺序、个数一致
            Type[] parametersType = new Type[] { tType, typeof(string), typeof(System.Drawing.Color), typeof(System.Drawing.Color), typeof(int), typeof(int) };
            //参数，必须保证顺序类型、正确
            object[] parametersValue = new object[] { System.Enum.Parse(tType, "CODE128"), code, System.Drawing.Color.Black, System.Drawing.Color.White, with, height };

            object value = FS.SOC.Public.Assembly.Method.Invoke
                (
                "BarcodeLib.dll",//dll名称
                "BarcodeLib.Barcode",//类名称
                "Encode",//方法名称
                parametersType,
                parametersValue,
                listPropert//属性值，指定属性名称和属性的值
                );

            if (value is System.Drawing.Image)
            {
                return value as System.Drawing.Image;
            }
            return null;
        }

        /// 获取频次名称
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public static string GetFrequenceName(FS.HISFC.Models.Order.Frequency frequency)
        {
            string id = frequency.ID.ToLower().Replace(".", "");
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
        /// 根据终端编码获取终端名称
        /// </summary>
        /// <param name="terminalId"></param>
        /// <returns></returns>
        public static string GetTerminalNameById(string terminalId)
        {
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            string terminalName = dbMgr.ExecSqlReturnOne("select t_name from pha_sto_terminal  where t_code='" + terminalId + "'");
            if (terminalName != "-1")
            {
                return terminalName;
            }
            return "";
        }

        /// <summary>
        /// 根据门诊号获取挂号级别
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public static string GetRegCodeByClinicCode(string clinicCode)
        {
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            string regCode = dbMgr.ExecSqlReturnOne("select reglevl_code from fin_opr_register  where clinic_code='" + clinicCode + "'");
            if (regCode != "-1")
            {
                return regCode;
            }
            return "";
        }

        /// <summary>
        /// 根据挂号级别获取级别名称
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public static string GetRegNameByClinicCode(string clinicCode)
        {
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            string regName = dbMgr.ExecSqlReturnOne("select reglevl_name from fin_opr_register  where clinic_code='" + clinicCode + "'");
            if (regName != "-1")
            {
                return regName;
            }
            return "";
        }

        /// <summary>
        /// 根据处方流水号获取全科标记
        /// </summary>
        /// <param name="sequenceN"></param>
        /// <returns></returns>
        public static string GetAccountFlagBySequenceNo(string recipeNO,string sequenceNO)
        {
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            string accountFlag = dbMgr.ExecSqlReturnOne("select ext_flag from fin_opb_feedetail where recipe_no='" + recipeNO + "'" + " and sequence_no='" + sequenceNO + "'");
            if(string.IsNullOrEmpty(accountFlag))
            {
                return "1";
            }
            else
            {
                return accountFlag;
            }
        }

        /// <summary>
        /// 判断是不是特殊用法
        /// </summary>
        /// <param name="USAGEID"></param>
        /// <returns></returns>
        public static bool IsSpecialUsage(string usageId)
        {
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            string usageCode = dbMgr.ExecSqlReturnOne("select name from com_dictionary a where a.type ='SPECIALUSAGE' and  a.valid_state='1'  and a.code ='" + usageId + "'");
            if (usageCode == "-1")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 判断是不是特殊剂型
        /// </summary>
        /// <param name="USAGEID"></param>
        /// <returns></returns>
        public static bool IsSpecialDoseModel(string DoseModelId)
        {
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            string DoseModelName = dbMgr.ExecSqlReturnOne("select name from com_dictionary a where a.type ='SPECIALDOSEMODELID' and  a.valid_state='1' and a.code ='" + DoseModelId + "'");
            if (DoseModelName == "-1")
            {
                return false;
            }
            return true;
        }

        #region 门诊配药保存
        /// <summary>
        /// 门诊配药保存
        /// </summary>
        /// <param name="applyOutCollection">摆药申请信息</param>
        /// <param name="terminal">配药终端</param>
        /// <param name="drugedDept">配药科室信息</param>
        /// <param name="drugedOper">配药人员信息</param>
        /// <param name="isUpdateAdjustParam">是否更新处方调剂参数</param>
        /// <returns>配药确认成功返回1 失败返回-1</returns>
        internal static int OutpatientDrug(List<FS.HISFC.Models.Pharmacy.ApplyOut> applyOutCollection, FS.FrameWork.Models.NeuObject terminal, FS.FrameWork.Models.NeuObject drugedDept, FS.FrameWork.Models.NeuObject drugedOper, bool isUpdateAdjustParam)
        {
            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
            if (pharmacyIntegrate.OutpatientDrug(applyOutCollection, terminal, drugedDept, drugedOper, isUpdateAdjustParam) != 1)
            {
                System.Windows.Forms.MessageBox.Show(pharmacyIntegrate.Err);
                return -1;
            }
            return 1;
        }
        #endregion


        public static string[] GetLocalIpv4()
        {
        //事先不知道ip的个数，数组长度未知，因此用StringCollection储存
          try
           {
            IPAddress[] localIPs;
            localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            StringCollection IpCollection = new StringCollection();
            foreach (IPAddress ip in localIPs)
            {
                //根据AddressFamily判断是否为ipv4,如果是InterNetWork则为ipv6
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                IpCollection.Add(ip.ToString());
            }
                string[] IpArray = new string[IpCollection.Count];
                IpCollection.CopyTo(IpArray, 0);
                return IpArray;
            }

            catch (Exception ex)
            {
                return null;
            }
            return null;
            }
         

    }
}
