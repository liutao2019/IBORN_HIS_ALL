using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.RADT;
using FS.HISFC.Models.Order;
using FS.FrameWork.Function;
using FS.HISFC.Models.Fee.Inpatient;

namespace FS.HISFC.BizProcess.Integrate.FeeInterface
{
    /// <summary>
    /// [��������: ��̨�����շ� ]<br></br>
    /// [�� �� ��: �����]<br></br>
    /// [������:maokb]<br></br>
    /// 
    /// <˵��>
    /// </˵��>
    /// </summary>
    public class InpatientRuleFee : IntegrateBase
    {
        #region ˵��

        /*�������
         * 1����ѯ������ִ��δ�շѵ�ҽ�������۴���0�������շ��š�Ӧִ�����ڷ�������
         * 2��ѭ��ҽ����Ϣ�ŵ�hsAllExecOrder�У�����Ŀ����+Ӧִ��������Ϊ��ֵ��֮ǰ����Ŀ������Ϊ��ֵ��ֻ�������ж�һ���շѣ�
         * 3���ų���Ժ���첻�շ���Ŀ�������жϰ�Сʱ�շѣ����������Сʱ����һ�� 
         * 4�����չ�����Ӧ������
         * 5���շѺ���������жϵ�ҽ����Ϣ�������շѵĺ��жϲ��շѵģ�
         * 
         * 
         * ע�⣺
         * 1��ÿ���շ��жϴӻ�����Ժ������ʱ����δ�շѵ���Ŀ����ֹ�ֽⲻ��ʱ����©�շ� 
         * 2���շѲ�ȡ�������շѡ���ȡ�߷��õ�ԭ��
         * 3������ֻ��ҽ�������жϣ���ѯ���е�ִ�е�  
         * 4����ȡ�շѹ�����FEE_TYPE�����������򣬺����շ��ܴ�Ӱ��
         *      02��ÿ�첻�����޶�����
         *      03��
         * 
         * */

        #endregion

        #region  ����
        /// <summary>
        /// �շѹ���
        /// </summary>
        DataSet dsFeeRule = null;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        ArrayList alFee = new ArrayList();

        /// <summary>
        /// �˷�
        /// </summary>
        ArrayList alQuitFee = new ArrayList();

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        DateTime beginTime;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        DateTime endTime;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        PatientInfo patientInfo = null;

        /// <summary>
        /// ���е�ҽ����Ϣ
        /// </summary>
        Dictionary<string, ArrayList> hsAllExecOrder = new Dictionary<string, ArrayList>();

        /// <summary>
        /// ���е�ҽ����Ϣ������ҽ���շѱ��ר�ã�
        /// </summary>
        Dictionary<string, ArrayList> hsAllExecOrderNew = new Dictionary<string, ArrayList>();

        /// <summary>
        /// ��Ų��շѺ�û��ҽ������Ŀ���룺���޶����
        /// </summary>
        List<string> NoFeeItemCodeList = new List<string>();

        /// <summary>
        /// ����δ�շ�ҽ����Ŀ����
        /// </summary>
        List<string> allListCode = new List<string>();

        /// <summary>
        /// �����շѵ��������㹻������ȡ����ҽ���ķ���
        /// </summary>
        //Dictionary<string, decimal> NoFeeOrderItem = new Dictionary<string, decimal>();

        /// <summary>
        /// ����շ�ҽ��
        /// </summary>
        Dictionary<string, ArrayList> hsFeeExecOrder = new Dictionary<string, ArrayList>();

        /// <summary>
        /// ����Сʱ�շ���Ŀ
        /// </summary>
        List<string> listHourItemCode = new List<string>();

        /// <summary>
        /// ��Ժ���첻�շ���Ŀ�б�ArrayList��
        /// </summary>
        ArrayList alNoFee = new ArrayList();

        /// <summary>
        /// ��Ժ���첻�շ���Ŀ��string��
        /// </summary>
        string outNoFeeItem = "";

        /// <summary>
        /// ����1Сʱ�Ĺ����ٷ�����һ��
        /// </summary>
        private const int Q1HMinute = 0;

        #endregion

        #region ҵ������
        FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
        FS.HISFC.BizLogic.Fee.UndrugFeeRegularManager feeruleManager = new FS.HISFC.BizLogic.Fee.UndrugFeeRegularManager();
        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new Fee();
        FS.HISFC.BizLogic.RADT.InPatient radtInpatient = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();
        FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
        FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// ��������
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();
               
        #endregion

        #region �շ�������

        /// <summary>
        /// ��������շ�
        /// </summary>
        /// <param name="patient">סԺ����ʵ��</param>
        /// <param name="isOutHos">�Ƿ��Ժʱ����</param>
        /// <param name="bTime">ҽ��ִ�п�ʼʱ��</param>
        /// <param name="eTime">����ʱ��</param>
        /// <returns></returns>
        public int DoRuleFee(PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FTSource ftSource,DateTime bTime, DateTime eTime)
        {
            //�����������
            this.ClearData();

            //���ڴ��������ע�ͣ�ÿ�������Զ��շѴ���Ĳ���һ����ǰ���23:59:50�������23��59:59
            //���Ǻ�����ڿ�ʼʱ��û���õ�

            //�շѿ�ʼʱ��ȡ������Ժʱ��(����1���£����Ǽ��ﻼ���Ǻ�����¼��Ժ��)����֤����ֽⲻ��ʱ��֮ǰ�ķ���Ҳ�ܼ���
            //beginTime = bTime;
            alFee = new ArrayList();
            beginTime = patient.PVisit.InTime.AddMonths(-1);
            endTime = eTime;
            //ȡ��������״̬��Ϣ
            patientInfo = this.radtInpatient.QueryPatientInfoByInpatientNO(patient.ID);

            //��ó�Ժʱ�䣬�����õ���
            patientInfo.PVisit.OutTime = patient.PVisit.OutTime;
            patientInfo.PVisit.PreOutTime = patient.PVisit.PreOutTime;

            #region Ӥ������
            if (patientInfo.IsBaby)
            {
                string motherID = this.radtInpatient.QueryBabyMotherInpatientNO(patient.ID);

                if (string.IsNullOrEmpty(motherID))
                {
                    this.Err = radtInpatient.Err;
                    return -1;
                }

                FS.HISFC.Models.RADT.PatientInfo motherPath = this.radtInpatient.QueryPatientInfoByInpatientNO(motherID);

                if (motherPath == null)
                {
                    this.Err = radtInpatient.Err;
                    return -1;
                }

                patientInfo.Pact = motherPath.Pact;
            }
            #endregion

            try
            {

                #region ��ȡ���з�ҩƷ�շѹ���

                //��֤����Ŀ�Ĺ�����FEE_TYPE�����������򣬺����շ��ܴ�Ӱ��

                if (dsFeeRule == null)
                {
                    dsFeeRule = feeruleManager.GetAlFeeRegular();
                    if (dsFeeRule == null)
                    {
                        this.Err = feeruleManager.Err;
                        return -1;
                    }

                    foreach (DataRow row in dsFeeRule.Tables[0].Rows)
                    {
                        if (!NConvert.ToBoolean(row["OUTFEE_FLAG"]))
                        {
                            outNoFeeItem += row["ITEM_CODE"] + "|";
                        }
                    }
                }
                #endregion

                #region  ��ȡ��Ժ���첻�շ���Ŀ

                //if (alNoFee == null || alNoFee.Count <= 0)
                //{
                //    alNoFee = constMgr.GetAllList("OutNoFee");

                //    if (alNoFee == null)
                //    {
                //        this.Err = "��ȡ��Ժ���첻�շ���Ŀ�б����" + constMgr.Err;
                //    }

                //    foreach (FS.FrameWork.Models.NeuObject constObj in alNoFee)
                //    {
                //         outNoFeeItem += row[""] + "|";
                //    }
                //}

                #endregion

                #region ��ȡ����Сʱ�շ���Ŀ

                //���ε��ˣ�����ҽ�����÷��Ƿ���Q1H�ж��Ƿ���Сʱ�շ���Ŀ���˴���û��Ҫ�ж���
                //�������Сʱ�շ���Ŀ��������ΪQ1HҲ����Сʱ�շ���Ŀ����
                //if (listHourItemCode == null || listHourItemCode.Count <= 0)
                //{
                //    listHourItemCode = this.GetHoureFeeItemCode();
                //}

                #endregion

                #region �����˻�ȡδ�շѵ�ҽ����Ŀ

                //��ȡ����������ִ��δ�շѵ���Ŀ
                //�˴���ȡ����ִ�е�ҽ�����ų���Сʱ�շ���С�ڰ��Сʱ��
                if (GetExecOrder() < 0)
                {
                    return -1;
                }

                //û��ҽ��
                if (hsAllExecOrder.Count == 0)
                {
                    return 1;
                }

                //��hsAllExecOrderNew�洢�����ҽ����Ϣ���������ҽ���շѱ���õ�
                //��������ķ�ʽ�½��Ĺ�ϣ���˳��Ϊԭ���ĵ���
                //foreach (string s in hsAllExecOrder.Keys)
                ////foreach(object obj in hsAllExecOrder.Values)
                //{
                //    hsAllExecOrderNew.Add(s, hsAllExecOrder[s]);
                //    //hsAllExecOrderNew.
                //}
                #endregion

                #region ���ջ���δ�շ�ҽ���е��շѹ��������

                if (GetExecOrderByRule() < 0)
                {
                    return -1;
                }

                //û����Ҫ�շѵ�ҽ��
                if (hsFeeExecOrder.Count <= 0)
                {
                    return 1;
                }

                #endregion

                #region �շ�

                //ת������ҽ����Ϣ������ʵ��
                if (this.GetAllFee(patientInfo,ftSource) < 0)
                {
                    return -1;
                }

                //��ȡ��Ŀ
                if (alFee.Count > 0)
                {
                    //�շ�
                    feeIntegrate.MessageType = MessType.N;
                    if (feeIntegrate.FeeItem(patientInfo, ref alFee) == -1)
                    {
                        this.Err = feeIntegrate.Err;
                        return -1;
                    }
                }

                #endregion

                #region �����շѱ��
                //����ҽ����ǣ�������û���շѶ����±�ǣ��˴����ղ�ѯ��ִ�е������£���֤�����ظ��շ�
                if (this.hsAllExecOrderNew.Count > 0)
                {
                    if (UpdateExec() < 0)
                    {
                        return -1;
                    }
                }

                ClearData();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return -1;
            }
            #endregion

            return 1;
        }

        #endregion

        #region ��ѯ��ȡδ�շ�ҽ��

        /// <summary>
        /// ��ȡ����������ִ��δ�շѵ�ҽ��
        /// </summary>
        /// <returns></returns>
        private int GetExecOrder()
        {
            //��ȡ����δ�շ�ҽ��
            ArrayList alUnChargeOrder = this.feeruleManager.GetPatientNoFeeExecOrder(patientInfo, beginTime, endTime);
            if (alUnChargeOrder == null)
            {
                this.Err = "��ȡ����ҽ����Ϣʧ�ܣ�" + orderManager.Err;
                return -1;
            }
            if (alUnChargeOrder.Count == 0)
            {
                return 1;
            }

            ArrayList tempal = null;
            foreach (ExecOrder execOrd in alUnChargeOrder)
            {
                //�ų���Ժ���첻�շ���Ŀ
                if (patientInfo.PVisit.OutTime <= new DateTime(2000, 1, 1, 1, 0, 0, 1))
                {
                    patientInfo.PVisit.OutTime = patientInfo.PVisit.PreOutTime;
                }
                if (patientInfo.PVisit.OutTime.Date == this.feeruleManager.GetDateTimeFromSysDateTime().Date)
                {
                    if (execOrd.DateUse.Date == patientInfo.PVisit.OutTime.Date && outNoFeeItem.Contains(execOrd.Order.Item.ID))
                    {
                        continue;
                    }
                }

                //��Ϊ�ô����ź�Ӧִ��������Ϊ��ֵ
                //if (hsAllExecOrder.ContainsKey(execOrd.Order.Item.ID))
                string keys = execOrd.Order.Item.ID + execOrd.DateUse.ToString("yyyyMMdd");

                if (hsAllExecOrder.ContainsKey(keys))
                {
                    tempal = hsAllExecOrder[keys] as ArrayList;
                    tempal.Add(execOrd);

                    hsAllExecOrderNew[keys] = tempal;
                }
                else
                {
                    tempal = new ArrayList();
                    tempal.Add(execOrd);
                    hsAllExecOrder.Add(keys, tempal);
                    hsAllExecOrderNew.Add(keys, tempal);
                    allListCode.Add(keys);
                }
            }

            if (hsAllExecOrder.Count == 0)
            {
                return 1;
            }

            //�������Сʱ��Q1H���շ���Ŀ����������
            if (this.DealHourExecOrder() < 0)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ����Сʱ��Q1H���շ���Ŀҽ�� ����30������һ��
        /// </summary>
        /// <returns></returns>
        private int DealHourExecOrder()
        {
            //���ε��ˣ�����ҽ�����÷��Ƿ���Q1H�ж��Ƿ���Сʱ�շ���Ŀ���˴���û��Ҫ�ж���
            //�������Сʱ�շ���Ŀ��������ΪQ1HҲ����Сʱ�շ���Ŀ����
            //foreach (string code in listHourItemCode)
            //{
            //if (hsAllExecOrder.ContainsKey(code))
            //{

            foreach (string code in hsAllExecOrder.Keys)
            {
                ArrayList alExecOrder = hsAllExecOrder[code] as ArrayList;
                for (int i = 0; i < alExecOrder.Count; i++)
                {
                    ExecOrder execOrder = alExecOrder[i] as ExecOrder;
                    //��ѯҽ����Ϣ
                    FS.HISFC.Models.Order.Order order = orderManager.QueryOneOrder(execOrder.Order.ID);
                    if (order == null)
                    {
                        this.Err = "��ѯҽ��ʧ�ܣ�" + orderManager.Err;
                        return -1;
                    }

                    if (order.Frequency.ID.ToUpper() != "Q1H")
                    {
                        continue;
                    }

                    //��������
                    int tSpan = 0;
                    //�ֽ���ϸ�е�һ��Ϊ��һ������Ȼ������ÿ�����㶼�γ�һ�����ݡ�
                    //��˶����״������������>30����ʼС�ڰ�Сʱ���շ�,tSpan=tSpan-1���������<30��
                    //��ǰ���ڰ�СʱҪ��ȡ����,tSpan=tspan��

                    #region ���������շ� OK

                    //���������շ�
                    //�޸����maokb��ԭ���ý�ֹʱ�����ж��Ƿ����շѵ��죬��׼ȷ����Ϊ��ҽ���ڿ�������δ�ֽ⣬�ڶ����շ�ʱ
                    //û���շѣ����ǵ������շ�ʱ���յ�һ��ķ������޷��ж��Ƿ��ǵ��쿪��ҽ��
                    if (order.MOTime.Date == execOrder.DateUse.Date)
                    {
                        //�޸����maokb:û��Ҫ�ų�����ֹͣ��ҽ�����������µ��쿪��������ֹͣ��ҽ���޷��շ�
                        #region delete
                        //�ų�������ֹͣ����Ŀ
                        //if (order.EndTime > new DateTime(2009, 1, 1, 1, 1, 1) && order.EndTime.Date < this.orderManager.GetDateTimeFromSysDateTime().Date)
                        //{
                        //    alExecOrder.Clear();
                        //    continue;
                        //}
                        #endregion
                        //���쿪������ֹͣ
                        if (order.EndTime.Date == order.MOTime.Date)
                        {
                            //�޸����maokb�����ڵ��쿪������ֹͣ�ģ�ֱ����ֹͣʱ���ȥ����ʱ��Ȼ���жϰ�Сʱ��
                            #region delete
                            //tSpan -= order.BeginTime.Minute > 30 ? 1 : 0;  //��ʼʱ����Ӵ��ڰ�Сʱ������һ��
                            //tSpan += order.EndTime.Minute >= 30 ? 1 : 0; //ֹͣʱ����Ӵ��ڵ��ڰ�Сʱ�Ͷ���һ��

                            /*ֹͣ���죬��ʼʱ��ͽ�ֹʱ��һ������Сʱ�ж�{80E98AB3-5D27-4c75-BA93-50F44B283028}
                            // * �����ʼʱ�䲻����Сʱ����ֹʱ��Ҳ������Сʱ��Ҫ���������������ϼ��Ƿ�����Сʱ
                            // * */
                            //if (order.BeginTime.Minute > 30 && order.EndTime.Minute < 30)
                            //{
                            //    tSpan +=(60 - order.BeginTime.Minute) + order.EndTime.Minute >= 30 ? 1 : 0;
                            //}
                            #endregion
                            TimeSpan ts = order.EndTime - order.BeginTime;
                            if (ts.Minutes < Q1HMinute)
                            {
                                tSpan--;
                            }

                        }
                        //������������δֹͣ
                        //�޸����maokb����ΪҪ��0����һ���㵽0��ǰһ�죬����������ʱ��С��30min����Ҫ����һ��
                        else
                        {
                            //�����жϿ�ʼʱ�䲻�����㣬���㲻����
                            //if (order.BeginTime.Minute < 60 - Q1HMinute && order.BeginTime.Minute != 0)
                            //{
                            //    tSpan += 1;
                            //}
                        }

                        if (tSpan != 0)
                        {
                            //alExecOrder.Add(execOrder);
                            execOrder.Order.Item.Qty += tSpan ;
                        }

                        if (execOrder.Order.Item.Qty <= 0)
                        {
                            execOrder.Order.Item.Qty = 1 ;
                        }
                    }

                    #endregion 

                    #region ֹͣ��ҽ�����ж��Ƿ��а�Сʱ�ͳ�Ժ���첻�շ���Ŀ
                      //�޸����maokb:�ж�ֹͣ����Ҳ��ִ��ʱ���ҽ��ֹͣ�������ж�  
                    else if (order.EndTime.Date == execOrder.DateUse.Date && order.Status == 3)
                    {
                        if (order.Frequency.ID != "Q1H")
                        {
                            continue;
                        }
                        //�޸����maokb����Ϊ0����Ǵ�ҽ���Ѿ��������յ��ˣ����Խ���Ҫ��ȥ��һ��
                        //tSpan--;
                        //��ֹʱ�����������30���ӣ�����һ��
                        if (order.EndTime.Minute >= Q1HMinute && order.EndTime.Minute != 0)
                        {
                            tSpan += 1;
                        }
                        /*ֹͣ���죬��ʼʱ��ͽ�ֹʱ��һ������Сʱ�ж�{80E98AB3-5D27-4c75-BA93-50F44B283028}
                            * �����ʼʱ�䲻����Сʱ����ֹʱ��Ҳ������Сʱ��Ҫ���������������ϼ��Ƿ�����Сʱ
                            * */
                        //�޸����maokb:Ӧ���ǿ��������������ֹͣ��������ĺ�>30�����1��
                        else if (order.BeginTime.Minute > Q1HMinute && order.EndTime.Minute < Q1HMinute)
                        {
                            tSpan += (60 - order.BeginTime.Minute) + order.EndTime.Minute >= Q1HMinute ? 1 : 0;
                        }
                        //�޸����maokb:���ӿ���������յĺ�ֹͣ������յ�֮��ĺ�>30���ȥ1��
                        else if (order.BeginTime.Minute < Q1HMinute && order.EndTime.Minute > Q1HMinute)
                        {
                            tSpan -= order.BeginTime.Minute + (60 - order.EndTime.Minute) > Q1HMinute ? 1 : 0;
                        }                        

                        if (tSpan != 0)
                        {
                            execOrder.Order.Item.Qty += tSpan ;
                        }
                    }
                    #endregion
                }
            }
            //    }
            //}
            return 1;
        }

        #endregion

        #region ���չ�����ҽ������

        /// <summary>
        /// ���ջ���δ�շ�ҽ���е��շѹ��������
        /// </summary>
        /// <returns></returns>
        private int GetExecOrderByRule()
        {
            DataRow[] vdr = null;

            foreach (string itemCode in this.hsAllExecOrderNew.Keys)
            {
                ArrayList alExecOrder = hsAllExecOrder[itemCode] as ArrayList;
                if (alExecOrder == null)
                {
                    continue;
                }

                //for (int i = 0; i < alExecOrder.Count; i++)
                //�˴�����ѭ���ˣ�������ͬ��Ŀ�ڵ���Ŀ����SingleOrderItem������ѭ��
                if(alExecOrder.Count>0)
                {
                    FS.HISFC.Models.Order.ExecOrder execOrder = alExecOrder[0] as ExecOrder;

                    vdr = dsFeeRule.Tables[0].Select("ITEM_CODE = '" + execOrder.Order.Item.ID + "'");
                    if (vdr == null)
                        continue;
                    string feeType = string.Empty;

                    #region ��ȡ�����Ŀ���շ�����

                    //��ѯ������Ŀ����������
                    string relateItems = "";
                    if (feeruleManager.GetRelateItems(execOrder.Order.Item.ID, ref relateItems) <= 0)
                    {
                        this.Err = "��ѯ�����Ŀʧ�ܣ�" + feeruleManager.Err;
                        return -1;
                    }
                    relateItems = "'" + relateItems.Replace("|", "','") + "'";

                    //��ȡ������Ŀ���շ�������ʱ���޶�Ϊҽ��ִ��ʱ��ĵ���
                    decimal count = feeruleManager.GetFeeCountByExecOrder(patientInfo, execOrder.DateUse, execOrder.DateUse, relateItems);
                    if (count < 0)
                    {
                        this.Err = "��ѯ����ҽ����������ʧ�ܣ�" + orderManager.Err;
                        return -1;
                    }
                    #endregion

                    foreach (DataRow dr in vdr)
                    {
                        feeType = dr[4].ToString();  //�������
                        switch (feeType)
                        {
                            //�����޶�
                            case "02":
                                    SingleOrderItem(dr, itemCode, count);
                                    break;
                            //�����Ŀ�޶�
                            case "03":
                                    ComOrderItem(dr, itemCode, count);
                                    break;
                            //��Ŀ����
                            case "04":
                                    MutexOrder(dr, itemCode, count);
                                    break;
                            //��������Ŀ����
                            case "05":
                                    MutexOrderByCondition(dr, itemCode, count);
                                    break;
                            default:
                                break;
                        }
                    }
                }
            }
            return 1;
        }

        #region �շѹ���

        #region FeeType =02����Ŀ�޶�

        /// <summary>
        /// �Ե���Ŀ�޶�ĸ���������ȡִ��ҽ����Ϣ
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="itemCode">�շ���Ŀ����</param>
        /// <param name="count">��������</param>
        /// <returns></returns>
        private int SingleOrderItem(DataRow dr, string itemCode, decimal count)
        {
            /* �㷨��
             * �ж���Ŀitemcode�շ�������������count    
             * */

            if (NoFeeItemCodeList.Contains(itemCode))
                return 1;

            //�������� 1ʱ�� 2�Ǵ���
            string limitCondition = dr[3].ToString();
            //��������
            decimal limit = NConvert.ToDecimal(dr[5]);

            if (!hsFeeExecOrder.ContainsKey(itemCode) && !hsAllExecOrder.ContainsKey(itemCode))
            {
                NoFeeItemCodeList.Add(itemCode);
                return 1;
            }

            ArrayList alExecOrderTemp = new ArrayList();

            //�Ƿ����շ�ҽ��
            bool isFee = hsFeeExecOrder.ContainsKey(itemCode);
            if (isFee)
            {
                alExecOrderTemp = hsFeeExecOrder[itemCode] as ArrayList;
            }
            else
            {
                alExecOrderTemp = hsAllExecOrder[itemCode] as ArrayList;
            } 

            if (alExecOrderTemp == null || alExecOrderTemp.Count == 0)
            {
                if (!NoFeeItemCodeList.Contains(itemCode))
                {
                    NoFeeItemCodeList.Add(itemCode);
                    hsAllExecOrder.Remove(itemCode);
                }

                return 1;
            }

            //��ȡ��Ŀ�շѴ���
            //decimal count = 0m;

            #region ��ȡ��Ŀ����

            ArrayList alTemp = new ArrayList();
            foreach (ExecOrder execOrder in alExecOrderTemp)
            {
                if (count >= limit)
                {
                    if (!NoFeeItemCodeList.Contains(itemCode))
                    {
                        NoFeeItemCodeList.Add(itemCode);
                    }
                    break;
                }

                count += NConvert.ToDecimal(execOrder.Order.Item.Qty);
                if (count <= limit)
                {
                    alTemp.Add(execOrder);
                }
                //���޶���,�����յ� 
                else if (count > limit)
                {
                    execOrder.Order.Qty = limit - (count - execOrder.Order.Item.Qty);
                    alTemp.Add(execOrder);

                    if (!NoFeeItemCodeList.Contains(itemCode))
                    {
                        NoFeeItemCodeList.Add(itemCode);
                    }
                }
            }

            if (isFee)
            {
                hsFeeExecOrder[itemCode] = alTemp;
            }
            else
            {
                hsFeeExecOrder.Add(itemCode, alTemp);
                hsAllExecOrder.Remove(itemCode);
            }

            #endregion

            return 1;
        }

        #endregion

        #region FeeType =03 ����Ŀ����޶� �ж��˵���Ŀ���޶�

        /// <summary>
        /// ����Ŀ����޶�
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        private int ComOrderItem(DataRow dr, string itemCode, decimal count)
        {
            /* �㷨��
             * �ж������Ŀ���շ��б�hsFeeExecOrder�����е�����
             * ��֤�����Ŀ����+itemcode�������������޶�����
             * */

            /*����ҽ����Ϣ�б��ʱ���տ���ʱ���ʹ��ʱ������������ȡ��������ִ�е�
             * 
             * �����շ�ҽ��hsAllExecOrder
             * ȷ���շѵ�ҽ��hsFeeExecOrder
             * ���շѵ�ҽ��NoFeeItemCodeList
             * 
             **/
            //if (NoFeeItemCodeList.Contains(itemCode))
            //{
            //    return 1;
            //}

            //�������� 1ʱ�� 2�Ǵ���
            string limitCondition = dr[3].ToString();
            //��������
            decimal limit = NConvert.ToDecimal(dr[5]);
            //ͬ������Ŀ
            string vCom = dr[6].ToString();

            //����ͬ������Ŀ���뼯��
            List<string> allItemCodeList = new List<string>();

            #region ��ȡ�����շ���Ŀ����

            //ȡ������ͬ������Ŀ����
            if (!string.IsNullOrEmpty(vCom))
            {
                string[] vs = vCom.Split('|');
                foreach (string s in vs)
                {
                    if (string.IsNullOrEmpty(s))
                        continue;
                    if (allItemCodeList.Contains(s))
                        continue;
                    //if (NoFeeItemCodeList.Contains(s))
                    //    continue;
                    allItemCodeList.Add(s);
                }
            }

            if (allItemCodeList.Count == 0)
            {
                return 1;
            }

            #endregion

            #region ������Ϲ����ȡҽ����Ϣ

            ArrayList alExecOrder = new ArrayList();

            //decimal count = 0m;
            foreach (string code in allItemCodeList)
            {
                string orderCode = code + itemCode.Substring(itemCode.Length - 8);

                //�������ҽ���б������Ѿ����ڣ���������
                if (hsFeeExecOrder.ContainsKey(orderCode))
                {
                    ArrayList alTemp = hsFeeExecOrder[orderCode] as ArrayList;
                    if (alTemp != null)
                    {
                        foreach (ExecOrder execOrder in alTemp)
                        {
                            count += execOrder.Order.Item.Qty;
                        }
                    }
                }

                if (count >= limit)
                {
                    if (hsFeeExecOrder.ContainsKey(itemCode))
                    {
                        hsFeeExecOrder.Remove(itemCode);
                    }
                    else if (hsAllExecOrder.ContainsKey(itemCode))
                    {
                        hsAllExecOrder.Remove(itemCode);
                    }

                    return 1;
                }
            }

            if (hsAllExecOrder.ContainsKey(itemCode))
            {
                alExecOrder.AddRange(hsAllExecOrder[itemCode] as ArrayList);
                hsAllExecOrder.Remove(itemCode);
            }
            else
            {
                if (hsFeeExecOrder.ContainsKey(itemCode))
                {
                    alExecOrder.AddRange(hsFeeExecOrder[itemCode] as ArrayList);
                    hsFeeExecOrder.Remove(itemCode);
                }
            }

            this.GetSortComItem(alExecOrder, limit, count);

            #endregion

            return 1;
        }

        /// <summary>
        /// ����ִ��ʱ���ȡӦ������
        /// </summary>
        /// <param name="al">ҽ����Ŀ</param>
        /// <param name="limit">�޶�</param>
        /// <returns></returns>
        private int GetSortComItem(ArrayList al, decimal limit, decimal count)
        {
            //����Ӧʹ��ʱ������
            CompareExecOrderByExecTime compareByExecTime = new CompareExecOrderByExecTime();
            al.Sort(compareByExecTime);
            //��Ŀ����
            //decimal count = 0m;

            ArrayList alTemp = new ArrayList();
            foreach (ExecOrder order in al)
            {
                if (count >= limit)
                {
                    continue;
                }

                count += order.Order.Qty;
                if (count < limit)
                {
                    alTemp.Add(order);
                }
                else if (count > limit)
                {
                    decimal amod = count - limit;
                    order.Order.Qty -= amod;
                    alTemp.Add(order);
                }
                else
                {
                    alTemp.Add(order);
                }
            }

            ArrayList al1 = null;
            foreach (ExecOrder obj in alTemp)
            {
                string itemCode = obj.Order.Item.ID + obj.DateUse.ToString("yyyyMMdd");
                if (hsFeeExecOrder.ContainsKey(itemCode))
                {
                    al1 = hsFeeExecOrder[itemCode] as ArrayList;
                    al1.Add(obj);
                }
                else
                {
                    al1 = new ArrayList();
                    al1.Add(obj);
                    hsFeeExecOrder.Add(itemCode, al1);
                }
            }
            return 1;
        }

        #endregion

        #region FeeType = 04 ��Ŀ���� �ж��˵���Ŀ���޶�

        /// <summary>
        /// ������Ŀ
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        private int MutexOrder(DataRow dr, string itemCode, decimal count)
        {
            List<string> comCode = new List<string>();
            //decimal count = 0m;
            //�޶�����
            decimal limit = NConvert.ToDecimal(dr[5]);
            string[] limitItem = null;

            //���������Ŀ���շѣ����˳�
            foreach (string code in hsFeeExecOrder.Keys)
            {
                if (dr[6].ToString().Contains(code.Substring(0, code.Length - 8)))
                {
                    return 1;
                }
            }

            //������Ŀ
            if (!string.IsNullOrEmpty(dr[6].ToString()))
            {
                limitItem = dr[6].ToString().Split('|');
            }

            //���շѰ��������޶����
            if (NoFeeItemCodeList.Contains(itemCode))
            {
                return 1;
            }
            else
            {
                return this.SingleOrderItem(dr, itemCode, count);
            }
        }

        #endregion

        #region FeeType =05 ��������Ŀ���⣬����������ָ��ֵ�����ڴ���ָ��ֵ�򻥳�

        /// <summary>
        /// ������Ŀ
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        private int MutexOrderByCondition(DataRow dr, string itemCode, decimal count)
        {
            string limitCondition = dr[3].ToString();
            decimal limit = NConvert.ToDecimal(dr[5]);
            //������Ŀ
            string[] limitItem = null;
            if (!string.IsNullOrEmpty(dr[6].ToString()))
            {
                limitItem = dr[6].ToString().Split('|');
            }

            bool isLimit = false;
            if ("0".Equals(limitCondition))
            {
                #region ����ָ������

                 //�������ҽ���б������Ѿ����ڣ���������
                if (hsFeeExecOrder.ContainsKey(itemCode))
                {
                    ArrayList alTemp = hsFeeExecOrder[itemCode] as ArrayList;
                    if (alTemp != null)
                    {
                        foreach (ExecOrder execOrder in alTemp)
                        {
                            count += execOrder.Order.Item.Qty;
                        }
                    }
                }

                if (count >= limit)//�����������
                {
                    isLimit = true;
                }

                #endregion
            }
            else if ("1".Equals(limitCondition))
            {
                #region ����ָ������

                //��ȡ��ǰʱ��
                DateTime dtNow = this.feeruleManager.GetDateTimeFromSysDateTime();

                System.Windows.Forms.Day day=  FS.FrameWork.Public.EnumHelper.Current.GetEnum<System.Windows.Forms.Day>(dtNow.DayOfWeek.ToString());
                if ((int)day + 1 >= (int)limit)
                {
                    isLimit = true;
                }

                #endregion
            }

            if (isLimit)
            {
                //���ƻ�����Ŀ�շ�
                foreach (string limitCode in limitItem)
                {
                    string temp = limitCode + itemCode.Substring(itemCode.Length - 8);
                    if (hsFeeExecOrder.ContainsKey(temp))
                    {
                        hsFeeExecOrder.Remove(temp);
                    }
                    else if (hsAllExecOrder.ContainsKey(temp))
                    {
                        hsAllExecOrder.Remove(temp);
                    }

                    if (!NoFeeItemCodeList.Contains(limitCode))
                    {
                        NoFeeItemCodeList.Add(limitCode);
                    }
                }
            }
            else
            {
                //�����ƵĻ�����ȫ����ȡ
                if (hsFeeExecOrder.ContainsKey(itemCode) == false)
                {
                    if (hsAllExecOrder.ContainsKey(itemCode))
                    {
                        hsFeeExecOrder.Add(itemCode, hsAllExecOrder[itemCode]);
                        hsAllExecOrder.Remove(itemCode);
                    }
                }
            }

            return 1;
        }

        #endregion

        #endregion

        #endregion

        #region ����ִ�е��շѱ��

        /// <summary>
        /// �ӹ�ϣ���л��ҽ���б�
        /// </summary>
        /// <param name="hsOrder"></param>
        /// <returns></returns>
        private ArrayList GetAllFeeOrder(Dictionary<string, ArrayList> hsOrder)
        {
            ArrayList alTemp = new ArrayList();
            ArrayList al = new ArrayList();
            IDictionaryEnumerator id = hsOrder.GetEnumerator();
            while (id.MoveNext())
            {
                alTemp = id.Value as ArrayList;
                if (alTemp.Count > 0)
                {
                    al.AddRange(alTemp.ToArray());
                }
            }
            return al;
        }

        /// <summary>
        /// ����ҽ���շѱ��
        /// </summary>
        /// <returns></returns>
        private int UpdateExec()
        {
            ArrayList al = new ArrayList();

            al = GetAllFeeOrder(hsAllExecOrderNew);

            if (al.Count == 0) return 0;
            foreach (ExecOrder objOrder in al)
            {
                #region old����
                //����ִ�е�����������
                //if (orderManager.UpdateChargeExec(objOrder) == -1)//�����շѱ��
                //{
                //    this.Err = orderManager.Err;
                //    return -1;
                //}
                #endregion

                //��Ϊ��ѯִ�е�ʱ���Ѿ���ҽ�����������ˣ��˴����µ�ʱ��Ҳ��Ӧ���ջ��ܸ���
                //�˴�objOrder�Ѿ��ǻ��ܺ������
                if (this.feeruleManager.UpdateChargeExecNew(this.patientInfo, objOrder) == -1)//�����շѱ��
                {
                    this.Err = orderManager.Err;
                    return -1;
                }
            }
            return 1;
        }
        #endregion

        #region ת��ҽ����Ϣ������ʵ��

        /// <summary>
        /// ת������ҽ��������ʵ��
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hsOrder"></param>
        /// <returns></returns>
        private int GetAllFee(PatientInfo p, FS.HISFC.Models.Fee.Inpatient.FTSource ftSource)
        {
            FeeItemList f = null; 
            DateTime feeDate = new DateTime();
            ArrayList al = new ArrayList();
            IDictionaryEnumerator id = this.hsFeeExecOrder.GetEnumerator();
            while (id.MoveNext())
            {
                al = id.Value as ArrayList;
                foreach (ExecOrder execOrder in al)
                {
                    //��Ժ���죬�շ�ʱ��Ϊ��Ժʱ��
                    if (execOrder.DateUse.Date == p.PVisit.OutTime.Date)
                    {
                        feeDate = p.PVisit.OutTime;
                    }
                    //�����շ�ʱ��ΪӦʹ��ʱ��ĵ���
                    else
                    {
                        feeDate = new DateTime(execOrder.DateUse.Year, execOrder.DateUse.Month, execOrder.DateUse.Day, 23, 59, 59);
                    }

                    if (execOrder.Order.Item.Qty <= 0)
                    {
                        continue;
                    }

                    //�շ�ʱ��ȡҽ��Ӧִ�еĵ�������ʱ��
                    f = this.ConvertExecOrderToFeeItemList(p, execOrder, feeDate);

                    if (f == null)
                    {
                        this.Err = "ת��������Ϣʧ�ܣ�";
                        return -1;
                    }
                    f.FTSource = ftSource.Clone();//��¼������Դ
                    alFee.Add(f);
                }
            }
            return 1;
        }

        /// <summary>
        /// ��ִExecOrderת��FeeItemList
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Fee.Inpatient.FeeItemList ConvertExecOrderToFeeItemList(PatientInfo patient, ExecOrder eOrder, DateTime feeDate)
        {
            //����ҽ��ʵ��
            FS.HISFC.Models.Fee.Item.Undrug undrugItem = itemManager.GetValidItemByUndrugCode(eOrder.Order.Item.ID);
            if (undrugItem == null)
            {
                this.Err = "��ȡ��ҩƷ��Ŀ��Ϣʧ�ܣ�" + itemManager.Err;
                return null;
            }

            FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();

            feeItemList.Item = undrugItem;

            if (eOrder.Order.HerbalQty == 0)
            {
                eOrder.Order.HerbalQty = 1;
            }           
            decimal price = feeItemList.Item.Price;
            //�������۸�

            if (feeItemList.Item.ItemType == EnumItemType.UnDrug)
            {
                FS.HISFC.Models.Fee.Item.Undrug undrug = feeItemList.Item as FS.HISFC.Models.Fee.Item.Undrug;
                decimal orgPrice = 0;
                if(this.feeIntegrate.GetPriceForInpatient(patient,feeItemList.Item,ref price,ref orgPrice)==-1)
                {
                    this.Err = "ȡ��Ŀ:" + feeItemList.Item.Name + "�ļ۸����!" + pactManager.Err;

                    return null;
                }
            }
            feeItemList.Item.Price = price;
            feeItemList.Item.DefPrice = price;
            feeItemList.Item.Qty = eOrder.Order.Qty * eOrder.Order.HerbalQty;
            //���Ӹ����ĸ�ֵ {AE53ACB5-3684-42e8-BF28-88C2B4FF2360}
            feeItemList.Days = eOrder.Order.HerbalQty;

            feeItemList.Item.PriceUnit = eOrder.Order.Unit;//��λ���¸�
            feeItemList.RecipeOper.Dept = eOrder.Order.ReciptDept.Clone();
            feeItemList.RecipeOper.ID = eOrder.Order.ReciptDoctor.ID;
            feeItemList.RecipeOper.Name = eOrder.Order.ReciptDoctor.Name;
            feeItemList.ExecOper = eOrder.Order.ExecOper.Clone();
            feeItemList.ExecOper.Dept.ID = eOrder.Order.ExeDept.ID;
            feeItemList.StockOper.Dept = eOrder.Order.StockDept.Clone();
            if (feeItemList.Item.PackQty == 0)
            {
                feeItemList.Item.PackQty = 1;
            }
            feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty), 2);
            feeItemList.FT.OwnCost = feeItemList.FT.TotCost;
            feeItemList.IsBaby = eOrder.Order.IsBaby;
            feeItemList.IsEmergency = eOrder.Order.IsEmergency;
            feeItemList.Order = eOrder.Order.Clone();
            feeItemList.ExecOrder.ID = eOrder.Order.User03;
            feeItemList.NoBackQty = feeItemList.Item.Qty;
            feeItemList.FTRate.OwnRate = 1;
            feeItemList.BalanceState = "0";

            eOrder.IsCharge = true;

            //��������
            eOrder.ChargeOper.Dept = eOrder.Order.NurseStation;
            eOrder.ChargeOper.ID = "000000";
            eOrder.ChargeOper.Name = "�ռƷ�";
            eOrder.ChargeOper.OperTime = feeDate; //�շ�ʱ��ȡ

            //�շѻ���
            feeItemList.ChargeOper.ID = eOrder.ChargeOper.ID;
            feeItemList.ChargeOper.Name = eOrder.ChargeOper.Name;
            feeItemList.ChargeOper.OperTime = feeDate;

            feeItemList.FeeOper.ID = eOrder.ChargeOper.ID;
            feeItemList.FeeOper.Name = eOrder.ChargeOper.Name;
            feeItemList.FeeOper.OperTime = this.feeruleManager.GetDateTimeFromSysDateTime(); //����ʱ��ȡ��ǰʱ��

            //��չ��Ϣ�� ����ʱ��εĽ�ֹʱ�� endTime ���ں������
            feeItemList.ExtOper.OperTime = endTime;

            feeItemList.TransType = TransTypes.Positive;
            feeItemList.UndrugComb.ID = eOrder.Order.Package.ID;
            feeItemList.UndrugComb.Name = eOrder.Order.Package.Name;

            //user02Ϊ��չ����Ա
            feeItemList.User02 = eOrder.Order.Oper.ID;

            return feeItemList;
        }
        #endregion

        #region ������
        /// <summary>
        /// ����б�
        /// </summary>
        private void ClearData()
        {
            alFee.Clear();            
            alQuitFee.Clear();
            hsFeeExecOrder.Clear();
            hsAllExecOrder.Clear();
            hsAllExecOrderNew.Clear();
            NoFeeItemCodeList.Clear();          
            allListCode.Clear();         
        }
        #endregion

        //////////////////////////////////////////

        /// <summary>
        /// �����շѹ����ȡ��Ŀ����
        /// </summary>
        /// <returns></returns>
        public List<string> GetFeeRuleItemCode()
        {
            
            List<string> listItemCode = new List<string>();
            try
            {
                DataSet ds = feeruleManager.GetAlFeeRegular();
                if (ds == null)
                {
                    this.Err = feeruleManager.Err;
                    return null;
                }
                string itemCode = string.Empty;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    itemCode = dr[1].ToString();  //ȡ��Ŀ����
                    if (!listItemCode.Contains(itemCode))
                    {
                        listItemCode.Add(itemCode);
                    }
                    itemCode = dr[6].ToString();
                    if (!string.IsNullOrEmpty(itemCode))
                    {
                        string[] s = itemCode.Split('|');
                        foreach (string ss in s)
                        {
                            if (!listItemCode.Contains(ss))
                            {
                                listItemCode.Add(ss);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            return listItemCode;
        }
    }

    /// <summary>
    /// ��Ŀ������
    /// </summary>
    class CompareExecOrderByExecTime : IComparer
    {
        public int Compare(object x, object y)
        {
            ExecOrder o1 = (x as ExecOrder).Clone();
            ExecOrder o2 = (y as ExecOrder).Clone();

            DateTime oX = o1.DateUse;// ExecOper.OperTime;
            DateTime oY = o2.DateUse; //ExecOper.OperTime;

            return DateTime.Compare(oX, oY);
        }

    }
}
