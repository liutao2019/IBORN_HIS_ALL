using System;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using FS.HISFC.BizLogic;
using FS.FrameWork.Management;
using System.Collections.Generic;
using FS.SOC.HISFC.BizProcess.CommonInterface;
namespace FS.SOC.Local.InpatientFee.GuangZhou
{
	/// <summary>
	/// 	/// Function ��ժҪ˵����
	/// </summary>
	///
    public class Function
    {
        //		private FarPoint.Win.Spread.FpSpread fpSpread1;
        //		private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        public Function()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����ǰ���
        /// <summary>
        /// �ж��Ƿ���δȷ�ϵ��˷�
        /// �Ƿ���δȷ�ϵĳ���
        /// </summary>
        /// <param name="ID">סԺ��ˮ��</param>
        /// <returns>-1����0ͨ��</returns>
        public int CheckBeforeBalance(string ID)
        {
            FS.HISFC.BizLogic.Order.ChargeBill billMgr = new FS.HISFC.BizLogic.Order.ChargeBill();
            FS.HISFC.BizLogic.Fee.ReturnApply apprMgr = new FS.HISFC.BizLogic.Fee.ReturnApply();
            FS.HISFC.BizLogic.Order.Order order = new FS.HISFC.BizLogic.Order.Order();
            //�ж��Ƿ���Ϊȷ�ϵĳ���
            //-��ʱ��ע�͵� xf
            #region �ж��Ƿ���Ϊȷ�ϵĳ���
            /*
            try
            {
                ArrayList allItems = billMgr.QueryChargeBillNotPrinted(ID, true);
                if (allItems == null) return -1;

                #region   Edit by xingz
                ArrayList validItems = new ArrayList();
                for (int i = 0; i < allItems.Count; i++)
                {
                    FS.HISFC.Models.Order.ChargeBill chargebill = (FS.HISFC.Models.Order.ChargeBill)allItems[i];
                    if (chargebill == null)
                    {
                        MessageBox.Show("��ȡδȷ�ϵ��շѵ�����");
                        return -1;
                    }
                    if (chargebill.IsPharmacy && chargebill.BillID != null && chargebill.BillID != "")
                    {
                        FS.HISFC.Models.Order.ExecOrder execorder = order.QueryExecOrder(chargebill.ExecID, "1");
                        if (execorder != null)
                        {
                            if (!execorder.IsValid)
                            {
                                validItems.Add(execorder);
                            }
                        }
                    }
                    if (!chargebill.IsPharmacy && chargebill.BillID != null && chargebill.BillID != "")
                    {
                        FS.HISFC.Models.Order.ExecOrder execorder = order.QueryExecOrder(chargebill.ExecID, "2");
                        if (execorder != null)
                        {
                            if (!execorder.IsValid)
                            {
                                validItems.Add(execorder);
                            }
                        }
                    }
                }
                #endregion

                if (validItems.Count > 0)
                {
                    DialogResult dr = MessageBox.Show("����δ��ȷ�ϵ��շѵ�,�Ƿ�������㣿", "��ʾ"
                        , MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dr == DialogResult.No)
                    {
                        return -1;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("��ȡ������Ŀʱ����!" + e.Message, "��ʾ");
                return -1;
            }
            */
            #endregion
            //�ж��Ƿ���δȷ�ϵ��˷�����
            try
            {
                ArrayList applys = apprMgr.QueryReturnApplys(ID, false);
                if (applys == null) return -1;
                if (applys.Count > 0)
                {
                    DialogResult dr = MessageBox.Show("����δȷ�ϵ��˷����룬�Ƿ�������㣿", "��ʾ"
                        , MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dr == DialogResult.No)
                    {
                        return -1;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("��ȡ�˷�������Ŀʱ����!" + ex.Message, "��ʾ");
                return -1;
            }
            //�ж��Ƿ����շѵ������ݴ���Ŀ
            try
            {
                FS.HISFC.BizLogic.Manager.Department myDept = new FS.HISFC.BizLogic.Manager.Department();
                FS.HISFC.BizLogic.Fee.TemporaryFee tempFee = new FS.HISFC.BizLogic.Fee.TemporaryFee();
                //��ȡ���������б�
                ArrayList alDept = myDept.GetDeptment("1");

                if (alDept != null && alDept.Count > 0)
                {
                    foreach (FS.HISFC.Models.Base.Department dept in alDept)
                    {
                        ArrayList alOpsFee = tempFee.Query(ID, dept.ID);

                        if (alOpsFee == null)
                        {
                            return -1;
                        }
                        if (alOpsFee.Count > 0)
                        {
                            DialogResult dr = MessageBox.Show(dept.Name + "�������ݴ浫δ�շѵ���Ŀ���Ƿ�������㣿", "��ʾ"
                                , MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (dr == DialogResult.No)
                            {
                                return -1;
                            }
                        }
                    }
                }
            }
            catch
            {
                return -1;
            }
            return 0;
        }
        #endregion

        #region ����ʱ��ȡδ��ȡ�Ĵ�λ��
        /// <summary>
        /// ����ʱ��ȡδ��ȡ�Ĵ�λ��
        /// </summary>
        /// <param name="info"></param>		
        /// <returns></returns>
        public int BedFeeForOutpatient(FS.HISFC.Models.RADT.PatientInfo info)
        {
            /*��ȡ��λ�Ѻ�����������ȡ��Ժ���߲��մ�λ�ѣ�
             * �в��ܽ�������⣺���ڻ����Ѿ���Ժ�Ǽǣ����ڻ��߰������ò�����ȡ��
             * �������δ�շ��ڼ�������������ֻ�ܰ��ջ�����ĵȼ���ȡ��λ��
             * ��λ�ȼ�ȡ�����洲��
             * */
            //�ж��Ƿ��з��䴲λ��δ���ﻼ��û�д�λ�ѣ�
            if (info.PVisit.PatientLocation.Bed.ID == "") return 0;
            //Ӥ�����մ�λ��
            if (info.Patient.IsBaby) return 0;
            //
            if (info.ExtendFlag1 == "1") return 0;
            //ϵͳ�л�ʱ�Ĵ�λ��
            if (info.PVisit.PatientLocation.Bed.ID == "SSSS") return 0;
            if (info.PVisit.OutTime == DateTime.MinValue) return 0;
            decimal Amount = 0m; //��λ������

            FS.HISFC.BizLogic.Manager.Bed bedFee = new FS.HISFC.BizLogic.Manager.Bed();
            DateTime OperDate = bedFee.GetDateTimeFromSysDateTime();
            DateTime chargeDate = OperDate;

            //������ȡ��λ������
            if (info.FT.PreFixFeeDateTime == DateTime.MinValue)
            {
                //���û�չ���λ��
                TimeSpan dt = new TimeSpan(info.PVisit.OutTime.Date.Ticks - info.PVisit.InTime.Date.Ticks);
                int days = dt.Days;
                if (days == 0)
                {
                    Amount = 1;
                }
                else
                {
                    Amount = (decimal)days;
                }
            }
            else
            {
                //����չ���λ��,��ɽһ��λ����ͷ����β,���һ�첻��
                TimeSpan dt = new TimeSpan(info.PVisit.OutTime.Date.Ticks - info.FT.PreFixFeeDateTime.Date.Ticks);
                int days = dt.Days;
                if (days <= 1) return 0;
                Amount = (decimal)dt.Days - 1;

            }

            if (Amount == 0) return 0;//û��Ҫ��ȡ�Ĵ�λ��
            if (Amount > 0)
            {
                DialogResult r = MessageBox.Show("�û�����δ��ȡ�Ĵ�λ����,�Ƿ���ȡ", "��ʾ", MessageBoxButtons.YesNo);
                if (r == DialogResult.No)
                    return 0;
            }


            //���ι̶�������ȡʱ��,��Ϊ��ͷ����β,�����շ��յ�����
            info.FT.PreFixFeeDateTime = FS.FrameWork.Function.NConvert.ToDateTime(info.PVisit.OutTime.AddDays(-1).ToShortDateString() + " 23:50:00");
            string OperId = "����";
            //��ȡ��λ�ȼ�
            FS.HISFC.BizLogic.Fee.BedFeeItem bedMgr = new FS.HISFC.BizLogic.Fee.BedFeeItem();
            FS.HISFC.Models.Base.Bed bed = bedFee.GetBedInfo(info.PVisit.PatientLocation.Bed.ID);
            if (bed == null)
            {
                MessageBox.Show("��ȡ��λ����" + bedFee.Err);
                return -1;
            }
            if (bed.BedGrade == null || bed.BedGrade.ID == "")
            {

                MessageBox.Show(bed.ID + "���Ĵ�λ�ȼ�û��ά��");
                return -1;
            }

            //��ȡ�̶������շ���Ŀ
            ArrayList bedItems;
            //bedItems = bedMgr.SelectByFeeCode(bed.BedGrade.ID);
            bedItems = bedMgr.QueryBedFeeItemByMinFeeCode(bed.BedGrade.ID);
            if (bedItems == null || bedItems.Count == 0)
            {
                MessageBox.Show("û��ά����λ�շ���Ŀ" + bedFee.Err);
                return 0;
            }
            #region ��ȡ��λ��

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.HISFC.BizLogic.Fee.InPatient feeMgr = new FS.HISFC.BizLogic.Fee.InPatient();
            FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

            feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (feeIntegrate.DoBedItemFee(bedItems, info, (int)Amount, OperDate,chargeDate, bed) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��ȡ��λ�ѳ���" + feeIntegrate.Err);
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return 0;
         
            #endregion
        }
        #endregion


        private static Dictionary<string, string> dictionaryYKDept = new Dictionary<string, string>();
        /// <summary>
        ///  �ж��Ƿ����˿�����
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public static bool IsContainYKDept()
        {

            string dept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;

            return IsContainYKDept(dept);
        }

        /// <summary>
        ///  �ж��Ƿ����˿�����
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public static bool IsContainYKDept(string dept)
        {
            if (dictionaryYKDept == null || dictionaryYKDept.Count == 0)
            {
                ArrayList al = CommonController.Instance.QueryConstant("YkDept");
                if (al != null)
                {
                    foreach (FS.FrameWork.Models.NeuObject obj in al)
                    {
                        dictionaryYKDept[obj.ID] = obj.Name;
                    }
                }
            }

            return dictionaryYKDept.ContainsKey(dept);
        }

    }
}
