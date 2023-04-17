using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.Components.Nurse
{
    public class Function
    {
        public Function()
        {
        }

        /// <summary>
        /// ��ȡ���
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static string GetNoon(DateTime current)
        {
            FS.HISFC.BizProcess.Integrate.Registration.Registration schemaMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

            ArrayList alNoon = schemaMgr.Query();
            if (alNoon == null) return "";
            /*
             * ʵ�����Ϊҽ������ʱ���,�������Ϊ08~11:30������Ϊ14~17:30
             * ���ԹҺ�Ա����������ʱ��ιҺ�,���п�����ʾ���δά��
             * ���Ը�Ϊ���ݴ���ʱ�����ڵ�������磺9��30��06~12֮�䣬��ô���ж��Ƿ��������
             * 06~12֮�䣬ȫ������˵��9:30���Ǹ�������
             */

            int[,] zones = new int[,] { { 0, 120000 }, { 120000, 180000 }, { 180000, 235959 } };
            int time = int.Parse(current.ToString("HHmmss"));
            int begin = 0, end = 0;

            for (int i = 0; i < 3; i++)
            {
                if (zones[i, 0] <= time && zones[i, 1] > time)
                {
                    begin = zones[i, 0];
                    end = zones[i, 1];
                    break;
                }
            }

            foreach (FS.HISFC.Models.Registration.Noon obj in alNoon)
            {
                if (int.Parse(obj.BeginTime.ToString("HHmmss")) >= begin &&
                    int.Parse(obj.EndTime.ToString("HHmmss")) <= end)
                {
                    return obj.ID;
                }
            }

            return "";
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="assign"></param>
        /// <param name="TrigeWhereFlag">�����־ 1.�ֵ�����  2.�ֵ���̨</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static int Triage( FS.HISFC.Models.Nurse.Assign assign,
            string TrigeWhereFlag, ref string error)
        {

            FS.HISFC.BizLogic.Nurse.Assign assignMgr = new FS.HISFC.BizLogic.Nurse.Assign();
            
            FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

            try
            {
                //assignMgr.SetTrans(trans.Trans);
                //regMgr.SetTrans(trans.Trans);

                //1����ȡ������������
                assign.SeeNO = assignMgr.Query((assign.Queue as FS.FrameWork.Models.NeuObject));
                if (assign.SeeNO == -1)
                {
                    error = assignMgr.Err;
                    return -1;
                }

                assign.SeeNO = assign.SeeNO + 1;
                //ר�ҵ�ֱ��ȡ ʱ����ڵĿ������
                //				if(FS.HISFC.Components.Function.NConvert.ToInt32(assign.Register.IsPre) == 1)
                #region {DB856C47-4C49-4257-AA81-1DCBFF658B1E}
                //if (assign.Register.DoctorInfo.Templet.Doct.ID != null && assign.Register.DoctorInfo.Templet.Doct.ID != "")
                //{
                //    assign.SeeNO = assign.Register.DoctorInfo.SeeNO;
                //} 
                #endregion

                //2�����������Ϣ��
                if (assignMgr.Insert(assign) == -1)
                {
                    error = assignMgr.Err;
                    return -1;
                }

                //3�����¹Һ���Ϣ���÷����־
                FS.HISFC.BizLogic.Nurse.Assign a = new FS.HISFC.BizLogic.Nurse.Assign();
                //a.SetTrans(trans.Trans);
                if (regMgr.Update(assign.Register.ID, FS.FrameWork.Management.Connection.Operator.ID,
                    a.GetDateTimeFromSysDateTime()/*regMgr.GetDateTimeFromSysDateTime()*/) == -1)
                {
                    error = regMgr.Err;                    
                    return -1;
                }
                //4.������������1
                if (assignMgr.UpdateQueue(assign.Queue.ID, "1") == -1)
                {
                    error = assignMgr.Err;
                    return -1;
                }

            }
            catch (Exception e)
            {
                error = e.Message;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// ȡ������
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="assign"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static int CancelTriage(FS.HISFC.Models.Nurse.Assign assign, ref string error)
        {
            FS.HISFC.BizLogic.Nurse.Assign assignMgr = new FS.HISFC.BizLogic.Nurse.Assign();

            FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

            try
            {
                //assignMgr.SetTrans(trans.Trans);
                //regMgr.SetTrans(trans.Trans);

                //ɾ��������Ϣ
                int rtn = assignMgr.Delete(assign);
                if (rtn == -1)//����
                {
                    error = assignMgr.Err;
                    return -1;
                }

                if (rtn == 0)
                {
                    error = "�÷�����Ϣ״̬�Ѿ������ı�,��ˢ����Ļ!";
                    return -1;
                }
                //�ָ��Һ���Ϣ�ķ���״̬
                rtn = regMgr.CancelTriage(assign.Register.ID);
                if (rtn == -1)
                {
                    error = regMgr.Err;
                    return -1;
                }
                //4.��������-1
                if (assignMgr.UpdateQueue(assign.Queue.ID, "-1") == -1)
                {
                    error = assignMgr.Err;
                    return -1;
                }
            }
            catch (Exception e)
            {
                error = e.Message;
                return -1;
            }

            return 0;
        }
    }

    [System.Xml.Serialization.XmlRoot()]
    public struct RefreshFrequence
    {
        /// <summary>
        /// ���Ϊ:10�����ʮ��
        /// 
        /// Ĭ��Ϊ:"no"��ˢ��
        /// </summary>
        public string RefreshTime;

        /// <summary>
        /// �Ƿ������Զ�����
        /// </summary>
        public bool IsAutoTriage;
    }
}