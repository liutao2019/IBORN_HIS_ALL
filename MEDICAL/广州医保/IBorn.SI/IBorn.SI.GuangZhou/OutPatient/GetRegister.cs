using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBorn.SI.GuangZhou.OutPatient
{
    public class GetRegister : IBorn.SI.GuangZhou.AbstractService<FS.HISFC.Models.Registration.Register, FS.HISFC.Models.Registration.Register>
    {
        public override bool Transcation
        {
            get { return false; }
        }

        protected override int Excute(FS.HISFC.Models.Registration.Register t, ref System.Data.DataTable dt, params object[] appendParams)
        {
            if (appendParams == null || appendParams.Length == 0)
            {
                return -1;
            }
            string sql = "select  JYDJH, YYBH, GMSFHM, XM, DWMC, XB, CSRQ, RYLB, " +
                         "GWYJB, ZYH, JZLB, RYRQ, RYZD, RYZDGJDM, BQDM, CWDH,TZDXSPH, BZ1, BZ2, BZ3, DRBZ from HIS_MZDJ";
            string where = " where 1=1 ";

            if (appendParams.Length > 0 && appendParams[0].ToString().Length > 0)
            {
                where += string.Format(" and GMSFHM='{0}'", appendParams[0].ToString());
            }

            if (appendParams.Length > 1 && appendParams[1].ToString().Length > 0)
            {
                DateTime regDate = FS.FrameWork.Function.NConvert.ToDateTime(appendParams[1]);
                DateTime beginDate = new DateTime(regDate.Year, regDate.Month, regDate.Day, 0, 0, 0);
                DateTime endDate = new DateTime(regDate.Year, regDate.Month, regDate.Day, 23, 59, 59);

                where += string.Format(" and RYRQ >= '{0}'" + " and RYRQ <= '{1}'", beginDate.ToString("yyyy-MM-dd HH:mm:ss"), endDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            if (appendParams.Length > 2 && appendParams[2].ToString().Length > 0)
            {
                where += string.Format(" and JZLB = '{0}'", appendParams[2].ToString());
            }

            if (appendParams.Length > 0 && appendParams[3].ToString().Length > 0)
            {
                where += string.Format(" and XM='{0}'", appendParams[3].ToString());
            }

            dt = IBorn.SI.GuangZhou.Base.SIDataBase.ExecuteDataTable(sql + where);

            if (dt == null)
            {
                return -1;
            }
            else if (dt.Rows.Count == 0)
            {
                string where_new = " where 1=1 ";
                if (appendParams.Length > 0 && appendParams[0].ToString().Length > 1)
                {
                    string idNo = appendParams[0].ToString();
                    //if (idNo.Length > 17)  //转成15位身份证 ,HIS登记时已经全部统一转成18位
                    //{
                    //    idNo = idNo.Remove(6, 2);
                    //    idNo = idNo.Remove(idNo.Length - 1);
                    //}
                    where_new += string.Format(" and GMSFHM='{0}'", idNo);
                }
                if (appendParams.Length > 1 && appendParams[1].ToString().Length > 0)
                {
                    DateTime regDate = FS.FrameWork.Function.NConvert.ToDateTime(appendParams[1]);
                    DateTime beginDate = new DateTime(regDate.Year, regDate.Month, regDate.Day, 0, 0, 0);
                    where_new += string.Format(" and RYRQ >= '{0}'", beginDate.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (appendParams.Length > 2 && appendParams[2].ToString().Length > 0)
                {
                    where_new += string.Format(" and JZLB = '{0}'", appendParams[2].ToString());
                }

                if (appendParams.Length > 0 && appendParams[3].ToString().Length > 0)
                {
                    where += string.Format(" and XM='{0}'", appendParams[3].ToString());
                }
                dt = IBorn.SI.GuangZhou.Base.SIDataBase.ExecuteDataTable(sql + where_new);
                if (dt == null)
                {
                    return -1;
                }
            }

            return 1;
        }

        protected override int GetResult(System.Data.DataTable dt, FS.HISFC.Models.Registration.Register t, ref FS.HISFC.Models.Registration.Register reg, params object[] appendParams)
        {
            if (dt == null)
            {
                return base.GetResult(dt, t, ref reg, appendParams);
            }
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            System.Data.DataRow dr = dt.Rows[0];

            //insReg.SerialNO = dr["JYDJH"].ToString();        //就医登记号
            //insReg.HosCode = dr["YYBH"].ToString();    //医院编号
            //insReg.Register.Patient.PID.Identifier.ID = dr["GMSFHM"].ToString();    //证件号
            //insReg.Register.Patient.Name = dr["XM"].ToString();    //患者姓名
            //insReg.MedicalUnitName = dr["DWMC"].ToString();          //参保单位名称
            //insReg.Register.Patient.Sex.ID = dr["XB"].ToString();  //性别
            //insReg.Register.Patient.Birthday = WConvert.ToDateTime(dr["CSRQ"]);   //出生日期
            //insReg.PersonType = dr["RYLB"].ToString();  //人员类别
            //insReg.Register.Patient.ID = dr["ZYH"].ToString();  //挂号
            //insReg.BizTtype = dr["JZLB"].ToString();  //就诊类别：1住院 2门诊特定项目3-门诊4-购药
            //insReg.RegisterTime = WConvert.ToDateTime(dr["RYRQ"].ToString());   //医保登记时间
            //insReg.Register.PatientAdmit.Diagnose.Name = dr["RYZD"].ToString();   //登记诊断
            //insReg.Register.PatientAdmit.Diagnose.NO = dr["RYZDGJDM"].ToString();         //登记诊断编码
            //insReg.Register.PatientAdmit.Dept.ID = dr["BQDM"].ToString();      //登记科室
            //insReg.InVist.Bed.ID = dr["CWDH"].ToString();         //入院床位
            //insReg.Approve.NO = dr["TZDXSPH"].ToString();               //审批号
            //insReg.Oper.Time = WConvert.ToDateTime(dr["BZ1"].ToString());   //备注1 存放记录产生时间，格式为YYYY-MM-DD hh:mm:ss
            //insReg.User02 =dr["BZ2"].ToString()           //18、备注2
            //insReg.User03 =dr["BZ3"].ToString()           //19、备注3
            //insReg.UploadState = dr["DRBZ"].ToString();                 //20、上传状态0 暂存 1 提交
            //insReg.IsValidFlag = true;

            reg.SIMainInfo.RegNo = dr[0].ToString();
            reg.SIMainInfo.HosNo = dr[1].ToString();
            reg.SIMainInfo.ID = dr[0].ToString();
            reg.IDCard = dr[2].ToString();
            reg.Name = dr[3].ToString();
            reg.SIMainInfo.Name = dr[3].ToString();
            reg.CompanyName = dr[4].ToString();
            if (dr[5].ToString() == "1")
            {
                reg.Sex.ID = "M";
            }
            else if (dr[5].ToString() == "2")
            {
                reg.Sex.ID = "F";
            }
            else
            {
                reg.Sex.ID = "U";
            }
            reg.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(dr[6].ToString());
            reg.SIMainInfo.EmplType = dr[7].ToString();
            reg.SIMainInfo.User01 = dr[8].ToString();
            reg.SIMainInfo.User02 = dr[9].ToString();
            reg.SIMainInfo.MedicalType.ID = dr[10].ToString();
            //reg.RegDate = FS.FrameWork.Function.NConvert.ToDateTime(dr[11].ToString());
            reg.SIMainInfo.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(dr[11].ToString());
            reg.SIMainInfo.ClinicDiagNose = dr[12].ToString();
            reg.SIMainInfo.InDiagnose.ID = dr[13].ToString();
            reg.SIMainInfo.AppNo = FS.FrameWork.Function.NConvert.ToInt32(dr[16].ToString());
            reg.User01 = dr[17].ToString();
            reg.User02 = dr[18].ToString();
            reg.User03 = dr[19].ToString();
            reg.SIMainInfo.ReadFlag = FS.FrameWork.Function.NConvert.ToInt32(dr[20].ToString());

            return 1;
        }
    }
}
