using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.BizLogic
{
    public  class Healthcheckup : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 获取PACS检查结果
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="pacsReulst"></param>
        /// <returns></returns>
        public int GetPacsResultInfo(FS.HISFC.HealthCheckup.Object.PacsReportItem pacsReulst)
        {
            string strSql = @"PROC_PEIS_GetPacsResultInfo,Sequenceno,22,1,{0},CheckDescription,22,1,{1},CheckConclusion,22,1,{2}," +
                             "Checkdoccode,22,1,{3},Checkdocname,22,1,{4},Checkdate,6,1,{5},Picture_Qty,13,2,1,Err_code,13,2,1,Err_text,22,2,1";
            try
            {
                int  PictureQty = 0;
                string strReturn = "";
                pacsReulst.Name = pacsReulst.Name.Replace(',', '，');
                pacsReulst.DIAGNAME1 = pacsReulst.DIAGNAME1.Replace(',', '，');
                pacsReulst.DIAGNAME2 = pacsReulst.DIAGNAME2.Replace(',', '，');
                strSql = string.Format(strSql, pacsReulst.SEQUENCENO, pacsReulst.DIAGNAME1, pacsReulst.DIAGNAME2, pacsReulst.OPERCODE, pacsReulst.OPERCODE, GetSysDateTime());
                if (this.ExecEvent(strSql, ref strReturn) == -1)
                {
                    this.Err = "执行存储过程出错！" + this.Err;
                    return -1;
                }
                string[] str = strReturn.Split(',');
                if (FS.FrameWork.Function.NConvert.ToInt32(str[1]) == -1)
                {
                    this.Err = str[0];
                    return -1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                this.Err = this.Err + ex.Message;
                return -1;
            }

            return 1;


        }

        //获取体检划价项目的组套编码
        public string GetTjUndrugGroup(string packageCode)
        {
            string strSql = @"select fun_get_hisitemcodebyzhcode('{0}') from dual";
            string packageItemcode ="";

            try
            {
                strSql = string.Format(strSql, packageCode);

                this.ExecQuery(strSql);
                if (Reader.Read())
                {
                    packageItemcode = this.Reader[0].ToString();
                
                }

            }
            catch (Exception ex)
            {
                this.Err = this.Err + ex.Message;
                return null;
            
            }

            return packageItemcode;
        
        
        }



       // public int 
    }
}
