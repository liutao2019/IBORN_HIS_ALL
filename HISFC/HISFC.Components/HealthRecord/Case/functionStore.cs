using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.Case
{
    class functionStore
    {

        /// <summary>
        /// 根据住院号--获得病案柜号
        /// </summary>
        /// <param name="PatientNo">住院号</param>
        public string GetCabinet(string PatientNo)
        {
            string Cabinet = "";
            int CaseNo = 0;
            try
            {
                CaseNo = FS.FrameWork.Function.NConvert.ToInt32(PatientNo);
            }
            catch
            {
                return "";
            }
            if (CaseNo == 0)
            {
                return "";
            }
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList  CabinetAl =  con.GetAllList("CaseCabinet");
            foreach (FS.FrameWork.Models.NeuObject obj in CabinetAl)
            {
                string[] str = obj.Memo.Split('-');
                int storeBegin = 0;
                int storeEnd = 0;
                if (str.Length > 0)
                {
                    storeBegin = FS.FrameWork.Function.NConvert.ToInt32(str[0].ToString());
                    storeEnd = FS.FrameWork.Function.NConvert.ToInt32(str[1].ToString());
                    if (CaseNo >= storeBegin && CaseNo <= storeEnd)
                    {
                        Cabinet = obj.ID.ToString();
                        break;
                    }
                }
            }
            return Cabinet;
        }
        /// <summary>
        /// 根据住院号-获得库房号
        /// </summary>
        /// <param name="PatientNo">住院号</param>
        public string GetCaseStore(string PatientNo)
        {
            string caseStore = "";
            int CaseNo = 0;
            try
            {
                CaseNo = FS.FrameWork.Function.NConvert.ToInt32(PatientNo);
            }
            catch
            {
                return "";
            }
            if (CaseNo == 0)
            {
                return "";
            }
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList  StoreAl = con.GetAllList("CaseStore");
            foreach (FS.FrameWork.Models.NeuObject obj in StoreAl)//存在多个段情况
            {
                string[] fd = obj.Memo.Split('|');
                if (fd.Length > 0)
                {
                    for (int i = 0; i < fd.Length; i++)
                    {
                        string[] fd1 = fd[i].ToString().Split('-');
                        int fd1Begin = 0;
                        int fd1End = 0;
                        if (fd1.Length > 0)
                        {
                            fd1Begin = FS.FrameWork.Function.NConvert.ToInt32(fd1[0].ToString());
                            fd1End = FS.FrameWork.Function.NConvert.ToInt32(fd1[1].ToString());
                            if (CaseNo >= fd1Begin && CaseNo <= fd1End)
                            {
                                caseStore = obj.ID.ToString();
                                break;
                            }
                        }
                    }
                }
            }
            return caseStore;
        }
    }
}
