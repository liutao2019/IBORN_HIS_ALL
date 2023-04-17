using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.OutpatientFee.GuangZhou.Gysy.ILisCalculateTube
{
    class LisCalculateTubeManager : FS.FrameWork.Management.Database
    {
        public int GetCuvetteItems(string CombinedStr, ref Dictionary<string, string> MapCuvetteItems, ref Dictionary<string, int> MapCuvetteNums)
        {
            string strSQL = "";
            string strReturn = "return";
            if (this.Sql.GetSql("FS.Procedure.prc_lis_PE_bloodinonetube", ref strSQL) == -1)
            {
                strSQL = "prc_lis_PE_bloodinonetube,moorderlist,22,1,{0}";
            }
            try
            {
                strSQL = string.Format(strSQL, CombinedStr);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            if (this.ExecEvent(strSQL, ref strReturn) == -1)
            {
                this.Err = strReturn + "执行存储过程出错!prc_lis_PE_bloodinonetube:" + this.Err;
                this.ErrCode = "prc_lis_PE_bloodinonetube";
                this.WriteErr();
                return -1;

            };
            strSQL = "";

            if (this.Sql.GetSql("Order.OutPatient.GetCuvetteItem", ref strSQL) == -1)
            {
                strSQL = "SELECT t.HISITEMCODE,t.SAMPLETUBE FROM hisitem_array_tumb_temp t";
            }
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "执行Order.OutPatient.GetCuvetteItem语句出错！";
                return -1;
            }
            while (Reader.Read())
            {
                FS.FrameWork.Models.NeuObject neuObj = new FS.FrameWork.Models.NeuObject();
                MapCuvetteItems.Add(this.Reader[0].ToString(), this.Reader[1].ToString());
            }
            this.Reader.Close();

            strSQL = "";

            if (this.Sql.GetSql("Order.OutPatient.GetCuvetteNum", ref strSQL) == -1)
            {
                strSQL = @"SELECT count(sampletube),sampletube FROM
                                        (
                                        SELECT GROUPCODE,MACHINECODE,SAMPLETYPE,sampletube FROM hisitem_array_tumb_temp
                                        GROUP BY GROUPCODE,MACHINECODE,SAMPLETYPE,SAMPLETUBE
                                        )
                                        GROUP BY SAMPLETUBE";
            }
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "执行Order.OutPatient.GetCuvetteNum语句出错！";
                return -1;
            }
            while (Reader.Read())
            {
                MapCuvetteNums.Add(this.Reader[1].ToString(), FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0].ToString()));
            }
            this.Reader.Close();

            return 1;
        }

    }
}
