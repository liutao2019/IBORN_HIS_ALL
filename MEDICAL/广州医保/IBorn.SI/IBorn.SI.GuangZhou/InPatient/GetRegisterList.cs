using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBorn.SI.GuangZhou.InPatient
{
    public class GetRegisterList : IBorn.SI.GuangZhou.AbstractService<FS.HISFC.Models.RADT.PatientInfo, List<FS.HISFC.Models.RADT.PatientInfo>>
    {
        public override bool Transcation
        {
            get { return false; }
        }

        protected override int Excute(FS.HISFC.Models.RADT.PatientInfo t, ref System.Data.DataTable dt, params object[] appendParams)
        {
            if (appendParams == null || appendParams.Length == 0)
            {
                return -1;
            }
            string sql = "select * from HIS_ZYDJ";
            string where = " where 1=1 ";
            if (appendParams.Length > 0 && appendParams[0].ToString().Length > 0)
            {
                where += string.Format(" and GMSFHM='{0}'", appendParams[0].ToString());
            }
            if (appendParams.Length > 1 && appendParams[1].ToString().Length > 0)
            {
                DateTime regDate = FS.FrameWork.Function.NConvert.ToDateTime(appendParams[1]);
                DateTime beginDate = new DateTime(regDate.Year, regDate.Month, regDate.Day, 0, 0, 0);
                //DateTime endDate = new DateTime(regDate.Year, regDate.Month, regDate.Day, 23, 59, 59);
                //where += string.Format(" and RYRQ >= '{0}'" + " and RYRQ <= '{1}'", beginDate.ToString("yyyy-MM-dd HH:mm:ss"), endDate.ToString("yyyy-MM-dd HH:mm:ss"));
                where += string.Format(" and RYRQ >= '{0}'", beginDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (appendParams.Length > 2 && appendParams[2].ToString().Length > 0)
            {
                where += string.Format(" and JZLB = '{0}'", appendParams[2].ToString());
            }
            if (appendParams.Length > 3 && appendParams[3].ToString().Length > 0)
            {
                where += string.Format(" and XM = '{0}'", appendParams[3].ToString());
            }
            dt = IBorn.SI.GuangZhou.Base.SIDataBase.ExecuteDataTable(sql + where);
            if (dt == null)
            {
                return -1;
            }
            else if (dt.Rows.Count == 0)
            {
                string where_new = " where 1=1 ";
                if (appendParams.Length > 0 && appendParams[0].ToString().Length > 0)
                {
                    string idNo = appendParams[0].ToString();
                    if (idNo.Length > 17)  //转成15位身份证 
                    {
                        idNo = idNo.Remove(6, 2);
                        idNo = idNo.Remove(idNo.Length - 1);
                    }
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
                if (appendParams.Length > 3 && appendParams[3].ToString().Length > 0)
                {
                    where_new += string.Format(" and XM = '{0}'", appendParams[3].ToString());
                }
                dt = IBorn.SI.GuangZhou.Base.SIDataBase.ExecuteDataTable(sql + where_new);
                if (dt == null)
                {
                    return -1;
                }
            }
            return 1;
        }

        protected override int GetResult(System.Data.DataTable dt, FS.HISFC.Models.RADT.PatientInfo t, ref List<FS.HISFC.Models.RADT.PatientInfo> regList, params object[] appendParams)
        {
            if (dt == null)
            {
                return base.GetResult(dt, t, ref regList, appendParams);
            }
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                FS.HISFC.Models.RADT.PatientInfo reg = t.Clone();
                reg.SIMainInfo.RegNo = dr["JYDJH"].ToString();        //就医登记号
                reg.SIMainInfo.HosNo = dr["YYBH"].ToString();    //医院编号
                reg.IDCard = dr["GMSFHM"].ToString();    //证件号
                reg.SIMainInfo.Name = reg.Name = dr["XM"].ToString();    //患者姓名
                reg.CompanyName = dr["DWMC"].ToString();          //参保单位名称
                //性别
                if (dr["XB"].ToString() == "1")
                {
                    reg.Sex.ID = "M";
                }
                else if (dr["XB"].ToString() == "2")
                {
                    reg.Sex.ID = "F";
                }
                else
                {
                    reg.Sex.ID = "U";
                }
                reg.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(dr["CSRQ"]);   //出生日期
                reg.SIMainInfo.EmplType = dr["RYLB"].ToString();  //人员类别
                //dr["ZYH"].ToString();  //挂号
                reg.SIMainInfo.MedicalType.ID = dr["JZLB"].ToString();  //就诊类别：1住院 2门诊特定项目3-门诊4-购药
                reg.SIMainInfo.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(dr["RYRQ"].ToString());   //医保登记时间
                reg.SIMainInfo.ClinicDiagNose = dr["RYZD"].ToString();   //登记诊断
                reg.SIMainInfo.InDiagnose.ID = dr["RYZDGJDM"].ToString();         //登记诊断编码
                //dr["BQDM"].ToString();      //登记科室
                //dr["CWDH"].ToString();         //入院床位
                reg.SIMainInfo.AppNo = FS.FrameWork.Function.NConvert.ToInt32(dr["TZDXSPH"].ToString());      //审批号
                reg.User01 = dr["BZ1"].ToString();   //备注1 存放记录产生时间，格式为YYYY-MM-DD hh:mm:ss
                reg.User02 = dr["BZ2"].ToString();           //18、备注2
                reg.User03 = dr["BZ3"].ToString();           //19、备注3
                reg.SIMainInfo.ReadFlag = FS.FrameWork.Function.NConvert.ToInt32(dr["DRBZ"].ToString());  //20、上传状态0 暂存 1 提交
                regList.Add(reg);
            }
            return 1;
        }
    }
}
