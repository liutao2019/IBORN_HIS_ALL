using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using FS.SOC.HISFC.CallQueue.Interface;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.CallQueue.BizProcess
{
    public class NurseAssign
    {
        private static NurseAssign factory;

        private static object lockHelper = new object();

        public static NurseAssign CreateInstance()
        {
            if (factory == null)
            {
                lock (lockHelper)
                {
                    if (factory == null)
                    {
                        factory = factory ?? new NurseAssign();
                    }
                }
            }
            return factory;
        }

        private NurseAssign()
        {
            callSpeak = InterfaceManager.GetICallSpeak();
        }

        private System.Collections.Queue queue = System.Collections.Queue.Synchronized(new System.Collections.Queue());
        private FS.SOC.HISFC.CallQueue.BizLogic.NurseAssign nurseAssignMgr = new FS.SOC.HISFC.CallQueue.BizLogic.NurseAssign();
        private ICallSpeak callSpeak = null;

        /// <summary>
        /// 是否正在运行
        /// </summary>
        private bool isStart = false;

        /// <summary>
        /// 叫分诊
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <param name="noon"></param>
        public void CallAssign(string nurseCode, FS.FrameWork.Models.NeuObject noon)
        {
            //往队列里面加入次数
            object[] o = new object[] { nurseCode, noon };
            //进入队列
            queue.Enqueue(o);

            //启动异步调用
            if (!isStart)
            {
                Thread t = new Thread(new ThreadStart(this.AssignCallback));
                t.Start();
            }
        }

        /// <summary>
        /// 异步调用叫号
        /// </summary>
        /// <param name="ar"></param>
        private void AssignCallback()
        {
            try
            {
                isStart = true;

                //只要队列大于零，则开始叫号
                while (queue.Count > 0)
                {
                    Object[] o = queue.Dequeue() as Object[];
                    string nurseCode = o[0].ToString();
                    FS.FrameWork.Models.NeuObject noon = o[1] as FS.FrameWork.Models.NeuObject;
                    //根据护士站和午别查找所有的叫号申请信息
                    List<FS.SOC.HISFC.CallQueue.Models.NurseAssign> list = nurseAssignMgr.Query(nurseCode, noon);
                    if (list != null && list.Count > 0)
                    {
                        foreach (FS.SOC.HISFC.CallQueue.Models.NurseAssign nurseAssign in list)
                        {
                            //开始逐一叫号
                            //callSpeak.Speech(nurseAssign);
                            ////删除叫号信息
                            //this.nurseAssignMgr.Delete(nurseAssign.ID);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                nurseAssignMgr.Err=e.Message;
                nurseAssignMgr.WriteErr();
            }
            finally
            {
                isStart = false;
            }
        }
    }
}
