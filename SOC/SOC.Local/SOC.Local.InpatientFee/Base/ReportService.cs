using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.Base
{
    class ReportService : FS.FrameWork.Management.Database
    {

        public ArrayList QueryInpatientDayFee(string patientNo, DateTime dtBeginDate, DateTime dtEndDate)
        {
            ArrayList al = new ArrayList();
            string strSql = string.Empty;

            if (this.Sql.GetSql("SDLocal.Fee.QueryPatientDayFee", ref strSql) == -1)
            {
                this.Err = "没有找到索引为: SDLocal.Fee.QueryPatientDayFee 的SQL语句";
                return null;
            }

            strSql = string.Format(strSql, patientNo, dtBeginDate.ToString(), dtEndDate.ToString());
            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "查询患者一日清单失败！";
                return null;
            }

            FS.HISFC.Models.Fee.Inpatient.FeeItemList ft;

            try
            {
                //循环读取数据
                while (this.Reader.Read())
                {
                    ft = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();

                    ft.Memo = this.Reader[0].ToString();
                    ft.Patient.PID.PatientNO = this.Reader[1].ToString();
                    ft.Patient.Name = this.Reader[2].ToString();
                    ft.Order.InDept.Name = this.Reader[3].ToString();
                    ft.Patient.Pact.Name = this.Reader[4].ToString();
                    ft.Patient.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[5].ToString());
                    ft.Patient.Sex.Name = this.Reader[6].ToString();
                    ft.Order.Patient.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7].ToString());
                    ft.Order.Patient.PVisit.PatientLocation.Bed.ID = this.Reader[8].ToString();
                    ft.FT.PrepayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9].ToString());
                    ft.Item.Name = this.Reader[10].ToString();
                    ft.Item.Specs = this.Reader[11].ToString();
                    ft.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[12].ToString());
                    ft.Item.PriceUnit = this.Reader[13].ToString();
                    ft.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[14].ToString());
                    ft.Patient.InTimes = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[15].ToString());
                    ft.Item.NationCode = this.Reader[16].ToString();

                    al.Add(ft);
                }

                this.Reader.Close();

                return al;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                return null;
            }
        }
    }
}
