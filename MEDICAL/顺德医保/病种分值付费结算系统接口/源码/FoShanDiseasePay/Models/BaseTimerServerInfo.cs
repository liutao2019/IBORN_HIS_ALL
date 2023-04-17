using System;
using System.Threading;
using System.Collections;
using FoShanDiseasePay.Jobs;
using FoShanDiseasePay.DataBase;

namespace FoShanDiseasePay.Base
{
    /// <summary>
    /// baseTimerServerInfo 的摘要说明。
    /// </summary>
    public class BaseTimerServerInfo
    {
        public BaseTimerServerInfo()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private Timer m_IntervalTimer;
        private BaseJob m_Ijob;
        private JobInfo m_obinfo;
        private DataBaseHelp dbHelp = new DataBaseHelp();

        /// <summary>
        /// 暴露IntervalTimer，
        /// </summary>
        public Timer IntervalTimer
        {
            get { return m_IntervalTimer; }
            set { m_IntervalTimer = value; }
        }

        /// <summary>
        /// 要执行的job
        /// </summary>
        public BaseJob IJob
        {
            get { return m_Ijob; }
            set { m_Ijob = value; }
        }

        /// <summary>
        /// job对象
        /// </summary>
        public JobInfo JobObject
        {
            get { return m_obinfo; }
            set { m_obinfo = value; }
        }

        /// <summary>
        /// 供回调用
        /// </summary>
        /// <param name="source"></param>
        public void OnTimer(object source)
        {
            if (m_obinfo == null)
            {
                return;
            }
            Manager manager = new Manager();
            ArrayList joblist = manager.GetJobList();
            foreach (JobInfo jobInfo in joblist)
            {
                if (m_obinfo.JOBCODE == jobInfo.JOBCODE)
                {
                    m_obinfo = jobInfo;
                    break;
                }
            }

            long m_Interval = 1000 * 60 * 10;

            if (m_obinfo.INTERVAL != 0)
            {
                m_Interval = m_obinfo.INTERVAL;
            }

            try
            {
                m_IntervalTimer.Change(System.Threading.Timeout.Infinite, m_Interval);
                if (ShouldRun())
                {
                    if (m_obinfo.JOBSTATE == "D" || m_obinfo.JOBSTATE == "M")
                    {
                        m_Ijob.startTime = DateTime.Now.ToString("yyyy-MM-dd");
                        m_Ijob.endTime = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        m_Ijob.startTime = m_obinfo.LASTDTIME.ToString("yyyy-MM-dd HH:mm:ss");
                        m_Ijob.endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    m_Ijob.Start();

                    if (m_obinfo.JOBSTATE == "D")
                    {
                        string sql = @"update COM_FoShanSI_JOB set last_dtime = to_date('{0}','yyyy-MM-dd hh24:mi:ss'),
                                      next_dtime = sysdate + 1,job_endtime = to_char(sysdate,'HH24:mi') where job_code = '{0}'";

                        sql = string.Format(sql, m_Ijob.endTime);

                        dbHelp.ExecuteNonQuery(sql, m_obinfo.JOBCODE);
                    }
                }
            }
            catch
            {

            }
            finally
            {
                m_IntervalTimer.Change(m_Interval, m_Interval);
            }
        }

        private bool ShouldRun()
        {
            if (m_obinfo == null)
            {
                return false;
            }
            if (m_obinfo.JOBSTATE == "N") //不执行
            {
                return false;
            }
            else if (m_obinfo.JOBSTATE == "D") //每天执行一次
            {
                //每天一次 就看执行时间到了没  是否已经执行过
                int hour = 1;
                int minute = 0;
                if (m_obinfo.JOBSTARTTIME != null && m_obinfo.JOBSTARTTIME != "")
                {
                    string[] times = m_obinfo.JOBSTARTTIME.Split(':');
                    try
                    {
                        hour = int.Parse(times[0]);
                    }
                    catch
                    {
                        hour = 1;
                    }

                    try
                    {
                        minute = int.Parse(times[1]);
                    }
                    catch
                    {
                        minute = 0;
                    }
                }
                string startTime = DateTime.Now.ToString("yyyy-MM-dd") + " " + hour.ToString("00") + ":" + minute.ToString("00");
                DateTime dtStart = DateTime.ParseExact(startTime, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                long m_Interval = 1000 * 60 * 10;
                if (m_obinfo.INTERVAL != 0)
                {
                    m_Interval = m_obinfo.INTERVAL;
                }
                if (DateTime.Now >= dtStart && DateTime.Now < dtStart.AddMilliseconds(m_Interval)) //判断是否到执行时间
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (m_obinfo.JOBSTATE == "M") //隔几分钟执行一次
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
