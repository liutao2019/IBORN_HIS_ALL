using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.DrugStore.GuangZhou.ZDLY.Outpatient.IRON
{
    public class IronBizlogic:FS.FrameWork.Management.Database
    {   
        #region 内部使用
        private string[] myGetOutmedtablehis(Outmedtablehis outmedtablehis)
        {
            try
            {
                string[] parm = {
                                    outmedtablehis.PrescriptionNO,//0处方号码
                                    outmedtablehis.MedID.ToString(),//1处方流水号
								    outmedtablehis.MedOnlyCode,//2药品唯一编码
                                    outmedtablehis.MedAMT.ToString(),//3药品出库数量
                                    outmedtablehis.MedName,//4药品名称
                                    outmedtablehis.MedUnit,//5药品规格
                                    outmedtablehis.MedPack,//6药品包装
                                    outmedtablehis.MedConvercof.ToString(),//7拆零系数
                                    outmedtablehis.MedFactory,//8厂商
                                    outmedtablehis.MedOutTime.ToString(),//9处方时间
                                    outmedtablehis.WindowNO.ToString(),//10窗口号
                                    outmedtablehis.PatientName,//11病人姓名
                                    outmedtablehis.PatientSex,//12病人性别
                                    outmedtablehis.PatientAge.ToString(),//13病人年龄
                                    outmedtablehis.PatientAgeUnit,//14病人年龄单位
                                    outmedtablehis.SendFlag.ToString(),//15发送标记 默认为0
                                    outmedtablehis.Dateofbirth.ToString(),//16出生日期
                                    outmedtablehis.WardNO,//17科室代码
                                    outmedtablehis.MedUsage,//18频次
                                    outmedtablehis.Diagnosis,//19诊断
                                    outmedtablehis.MedPerday,//20用量
                                    outmedtablehis.MedPerdos,//21用法
                                    outmedtablehis.DoctorName,//22医生姓名
                                    outmedtablehis.WardName,//23科室名称
                                    outmedtablehis.PatientID,//24病人ID
                                    outmedtablehis.FPNO1,//25发票号
                                    outmedtablehis.MedUnitprice.ToString(),//26单价	
	                                outmedtablehis.Remark.ToString(),      //27备注
								};
                return parm;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
     
        }
        #endregion

        #region 外部使用
        /// <summary>
        /// 插入发药机处方信息表
        /// </summary>
        /// <param name="outmedtablehis"></param>
        /// <returns></returns>
        public int Insert(Outmedtablehis outmedtablehis)
        {
            string strSql = "";
            if (this.Sql.GetSql("Pharmacy.DrugStore.Insert.ZDLYFYJ", ref strSql) == -1)
            {
                this.Err = "没有找到sql语句Pharmacy.DrugStore.Insert.ZDLYFYJ，请联系信息科";
                this.WriteErr();
                return -1;
            }
            try
            {
                string[] strParm = this.myGetOutmedtablehis(outmedtablehis);     //取参数列表
                strSql = string.Format(strSql, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.DrugStore.Insert.ZDLYFYJ:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 插入闪框
        /// </summary>
        /// <param name="drugRecipt"></param>
        /// <param name="drugTerminal"></param>
        /// <returns></returns>
        public int InsertCompress(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipt,FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            string strSql = "";
            if (this.Sql.GetSql("Pharmacy.DrugStore.Insert.ZDLYFYJCompress", ref strSql) == -1)
            {
                this.Err = "没有找到sql语句Pharmacy.DrugStore.Insert.ZDLYFYJCompress，请联系信息科";
                this.WriteErr();
                return -1;
            }
            try
            {
                //sstrSql = string.Format(strSql, drugRecipt.RecipeNO, drugRecipt.SendTerminal.ID, "0", drugRecipt.InvoiceNO);
                int windowNO = 0;
                if (drugRecipt.SendTerminal.ID == "4312")
                {
                    windowNO = 1;
                }
                else if (drugRecipt.SendTerminal.ID == "4320")
                {
                    windowNO = 2;
                }
                else if (drugRecipt.SendTerminal.ID == "4321")
                {
                    windowNO = 3;
                }
                else if (drugRecipt.SendTerminal.ID == "4322")
                {
                    windowNO = 4;
                }
                else if (drugRecipt.SendTerminal.ID == "4323")
                {
                    windowNO = 5;
                }
                else if (drugRecipt.SendTerminal.ID == "4324")
                {
                    windowNO = 6;
                }
                else if (drugRecipt.SendTerminal.ID == "4325")
                {
                    windowNO = 7;
                }
                else if (drugRecipt.SendTerminal.ID == "4326")
                {
                    windowNO = 8;
                }



                strSql = string.Format(strSql, drugRecipt.InvoiceNO, windowNO, "0", drugRecipt.RecipeNO,drugRecipt.FeeOper.OperTime);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.DrugStore.Insert.ZDLYFYJCompress:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        #endregion
    }
}
