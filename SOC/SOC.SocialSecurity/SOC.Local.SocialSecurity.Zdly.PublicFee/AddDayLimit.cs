using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.SocialSecurity.Zdly.PublicFee
{
    class AddDayLimit:FS.HISFC.BizProcess.Interface.Common.IJob
    {
        public AddDayLimit()
        {
        }

        private string myMessage = "";
        #region IJob 成员

        public string Message
        {
            get
            {
                return this.myMessage;
            }
        }

        public int Start()
        {
            try
            {
                //定义费用管理类,并执行	
                FS.HISFC.BizLogic.Fee.Item job = new FS.HISFC.BizLogic.Fee.Item();
                //公费日限额处理
                //if (job.ExecProcAddDayLimit() == -1)
                //{
                //    //WriteLogo(job.Err + DateTime.Now.ToString());
                //    return -1;
                //}

                //非主线程的方法在此处调用,更新job列表中的状态和执行时间
                //this.UpdateRow("FIN_AddDayLimit");
                return 1;
            }
            catch (Exception ex)
            {
                //this.WriteLogo(ex.Message + ex.Source);
                return -1;
            }
        }

        #endregion
    }
}
