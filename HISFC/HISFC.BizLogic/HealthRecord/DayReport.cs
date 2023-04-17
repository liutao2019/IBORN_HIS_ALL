using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace FS.HISFC.BizLogic.HealthRecord
{
    /// <summary>
    /// [��������: סԺ�ձ�������]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-07-10]<br></br>
    /// 
    /// <�޸ļ�¼
    /// 
    ///		�޸���=��ǿ
    ///		�޸�ʱ��=2007-7-20
    ///		�޸�Ŀ��=���ƹ���
    ///		�޸�����=���ƹ���
    ///  />
    /// </summary>
    public class DayReport : FS.FrameWork.Management.Database
    {
        public DayReport()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        /// <summary>
        /// �޸Ĵ�λ��
        /// </summary>
        /// <param name="al"></param>
        /// <returns></returns>
        public int EditBedNum(ArrayList al)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.SetTrans( FS.FrameWork.Management.PublicTrans.Trans );

            foreach (FS.HISFC.Models.HealthRecord.DayReport dayReport in al)
            {
                if (dayReport.HasRecord == "0")
                #region �¼ӵ���
                {
                    string sql = "";

                    if (this.Sql.GetSql("HealthRecord.DayReport.EditBedNumInsert", ref sql) == -1)
                        return -1;

                    try
                    {
                        sql = string.Format(sql,
                            dayReport.DateStat.Date,
                            dayReport.Dept.ID,
                            dayReport.BedStandNum
                            );
                    }
                    catch (Exception e)
                    {
                        this.Err = "[HealthRecord.DayReport.EditBedNumInsert]��ʽ��ƥ��!" + e.Message;
                        this.ErrCode = e.Message;
                        return -1;
                    }
                    if (this.ExecQuery(sql) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                }
                #endregion
                else
                #region �޸ĵ���
                {
                    string sql = "";

                    if (this.Sql.GetSql("HealthRecord.DayReport.EditBedNumUpdate", ref sql) == -1)
                        return -1;

                    try
                    {
                        sql = string.Format(sql,
                            dayReport.BedStandNum,
                            dayReport.Dept.ID,
                            dayReport.DateStat.Date
                            );
                    }
                    catch (Exception e)
                    {
                        this.Err = "[HealthRecord.DayReport.EditBedNumUpdate]��ʽ��ƥ��!" + e.Message;
                        this.ErrCode = e.Message;
                        return -1;
                    }
                    if (this.ExecQuery(sql) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                }
                #endregion
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return 0;
        }
        /// <summary>
        /// ����ĳ�յ�סԺ�ձ�
        /// </summary>
        /// <param name="templet"></param>
        /// <returns></returns>
        public int Save(ArrayList al)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.SetTrans( FS.FrameWork.Management.PublicTrans.Trans );

            try
            {
                foreach (FS.HISFC.Models.HealthRecord.DayReport dayReport in al)
                {

                    if (dayReport.HasRecord == "0")
                    #region �¼ӵ���
                    {
                        string sql = "";

                        if (this.Sql.GetSql("HealthRecord.DayReport.SaveInsert", ref sql) == -1)
                            return -1;

                        try
                        {
                            sql = string.Format(sql,
                                dayReport.DateStat.Date,
                                dayReport.Dept.ID,
                                dayReport.RemainYesterdayNum,
                                dayReport.InNormalNum,
                                dayReport.InChangeNum,
                                dayReport.OutNormalNum,
                                dayReport.OutChangeNum,
                                dayReport.AccNum,
                                dayReport.BanpNum);
                        }
                        catch (Exception e)
                        {
                            this.Err = "[HealthRecord.DayReport.SaveInsert]��ʽ��ƥ��!" + e.Message;
                            this.ErrCode = e.Message;
                            return -1;
                        }
                        if (this.ExecQuery(sql) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            return -1;
                        }
                    }
                    #endregion
                    else
                    #region �޸ĵ���
                    {
                        string sql = "";

                        if (this.Sql.GetSql("HealthRecord.DayReport.SaveUpdate", ref sql) == -1)
                            return -1;

                        try
                        {
                            sql = string.Format(sql,
                                dayReport.RemainYesterdayNum,
                                dayReport.InNormalNum,
                                dayReport.InChangeNum,
                                dayReport.OutNormalNum,
                                dayReport.OutChangeNum,
                                dayReport.AccNum,
                                dayReport.BanpNum,
                                dayReport.Dept.ID,
                                dayReport.DateStat.Date
                                );
                        }
                        catch (Exception e)
                        {
                            this.Err = "[HealthRecord.DayReport.SaveUpdate]��ʽ��ƥ��!" + e.Message;
                            this.ErrCode = e.Message;
                            return -1;
                        }
                        if (this.ExecQuery(sql) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            return -1;
                        }
                    }
                    #endregion
                }
                FS.FrameWork.Management.PublicTrans.Commit();

            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// ��ѯĳ�յ�סԺ�ձ�
        /// </summary>
        /// <param name="statTime"></param>
        public ArrayList QueryByStatTime(DateTime statTime)
        {
            string strSql = "";

            if (this.Sql.GetSql("HealthRecord.DayReport.QueryByStatTime", ref strSql) == -1)
            {
                return null;
            }
            try
            {
                strSql = string.Format(strSql, statTime.ToString());
            }
            catch (Exception e)
            {
                this.Err = "[HealthRecord.DayReport.QueryByStatTime]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }
            ArrayList al = new ArrayList();
            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.HealthRecord.DayReport dayReport;
                    dayReport = new FS.HISFC.Models.HealthRecord.DayReport();
                    dayReport.DateStat = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader["DateStat"].ToString());
                    dayReport.Dept.ID = this.Reader["DeptID"].ToString();
                    dayReport.Dept.Name = this.Reader["DeptName"].ToString();

                    if (!String.IsNullOrEmpty(this.Reader["BedStandNum"].ToString()))
                    {
                        dayReport.BedStandNum = FS.FrameWork.Function.NConvert.ToInt32(this.Reader["BedStandNum"].ToString());
                    }
                    if (!String.IsNullOrEmpty(this.Reader["RemainYesterdayNum"].ToString()))
                    {
                        dayReport.RemainYesterdayNum = FS.FrameWork.Function.NConvert.ToInt32(this.Reader["RemainYesterdayNum"].ToString());
                    }
                    if (!String.IsNullOrEmpty(this.Reader["InNormalNum"].ToString()))
                    {
                        dayReport.InNormalNum = FS.FrameWork.Function.NConvert.ToInt32(this.Reader["InNormalNum"].ToString());
                    }
                    if (!String.IsNullOrEmpty(this.Reader["InChangeNum"].ToString()))
                    {
                        dayReport.InChangeNum = FS.FrameWork.Function.NConvert.ToInt32(this.Reader["InChangeNum"].ToString());
                    }
                    if (!String.IsNullOrEmpty(this.Reader["OutNormalNum"].ToString()))
                    {
                        dayReport.OutNormalNum = FS.FrameWork.Function.NConvert.ToInt32(this.Reader["OutNormalNum"].ToString());
                    }
                    if (!String.IsNullOrEmpty(this.Reader["OutChangeNum"].ToString()))
                    {
                        dayReport.OutChangeNum = FS.FrameWork.Function.NConvert.ToInt32(this.Reader["OutChangeNum"].ToString());
                    }
                    if (!String.IsNullOrEmpty(this.Reader["AccNum"].ToString()))
                    {
                        dayReport.AccNum = FS.FrameWork.Function.NConvert.ToInt32(this.Reader["AccNum"].ToString());
                    }
                    if (!String.IsNullOrEmpty(this.Reader["BanpNum"].ToString()))
                    {
                        dayReport.BanpNum = FS.FrameWork.Function.NConvert.ToInt32(this.Reader["BanpNum"].ToString());
                    }
                    dayReport.HasRecord = this.Reader["HasRecord"].ToString();
                    al.Add(dayReport);
                }

                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = "��ѯĳ�յ�סԺ�ձ�!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            return al;
        }
        #region ˽�з���

        #endregion

    }
}
