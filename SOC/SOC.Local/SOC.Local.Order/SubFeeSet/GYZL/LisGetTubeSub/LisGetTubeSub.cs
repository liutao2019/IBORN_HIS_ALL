using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using Neusoft.SOC.Local.Order.SubFeeSet.GYZL.LisGetTubeSub.EasiLab.DataAccess.Models;
using Neusoft.SOC.Local.Order.SubFeeSet.GYZL.LisGetTubeSub.EasiLab.Models.LabManagementing;
using Neusoft.SOC.Local.Order.SubFeeSet.GYZL.LisGetTubeSub.EasiLab.DataAccess.Models.HisInterface;
using Neusoft.SOC.Local.Order.SubFeeSet.GYZL.LisGetTubeSub.EasiLab;

namespace Neusoft.SOC.Local.Order.SubFeeSet.GYZL.LisGetTubeSub
{
    public class LisGetTubeSub
    {
        Neusoft.HISFC.Models.Registration.Register currentPatientInfo = new Neusoft.HISFC.Models.Registration.Register();
        public Neusoft.HISFC.Models.Registration.Register Patient
        {
            get
            {
                return this.currentPatientInfo;
            }
            set
            {
                this.currentPatientInfo = value;
            }
        }

        private string errMsg = "";

        private SeparationTubeTestInfo hisData;
        public SeparationTubeTestInfo HisData
        {
            get
            {
                return this.hisData;
            }
            set
            {
                this.hisData = value;
            }
        }

        public TubeGroupInfo InvokeWebservice(SeparationTubeTestInfo[] sepTube)
        {
            try
            {
                HisInterfaceClient his = new HisInterfaceClient();
                return his.GetTubeGroups(sepTube);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lis计算试管失败，原因：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// 获取试管附材信息并插入费用表
        /// </summary>
        /// <param name="LisInfo"></param>
        public void GetHisTubeSub(TubeGroupInfo LisInfo, Neusoft.HISFC.Models.Order.OutPatient.Order order)
        {
            ArrayList alSubItems = new ArrayList();
            Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList tmpFeeItemList = null;
            Neusoft.HISFC.Models.Fee.Item.Undrug undrugInfo = new Neusoft.HISFC.Models.Fee.Item.Undrug();
            Neusoft.HISFC.BizProcess.Integrate.Fee feeManagement = new Neusoft.HISFC.BizProcess.Integrate.Fee();


            if (LisInfo != null)
            {
                ContainerInfo[] lisCt = LisInfo.Containersk__BackingField;
                if (lisCt != null)
                {
                    foreach (ContainerInfo hisTest in lisCt)
                    {
                        if (hisTest != null)
                        {
                            TestInfo[] hisItems = hisTest.TestItems;
                            foreach (TestInfo hisItem in hisItems)
                            {
                                tmpFeeItemList = new Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList();
                                undrugInfo = feeManagement.GetItem(hisItem.TestId);
                                tmpFeeItemList.Item.ID = undrugInfo.ID;
                                tmpFeeItemList.Item.Name = undrugInfo.Name;
                                tmpFeeItemList.Item.Qty = 1;
                                tmpFeeItemList.CancelType = Neusoft.HISFC.Models.Base.CancelTypes.Valid;
                                tmpFeeItemList.Patient.ID = this.Patient.ID;//门诊流水号
                                tmpFeeItemList.Patient.PID.CardNO = this.Patient.PID.CardNO;//门诊卡号
                                tmpFeeItemList.Order.Patient.Pact.ID = this.Patient.Pact.ID;
                                tmpFeeItemList.Order.Patient.Pact.PayKind.ID = this.Patient.Pact.PayKind.ID;
                                tmpFeeItemList.ChargeOper.ID = Neusoft.FrameWork.Management.Connection.Operator.ID;
                                tmpFeeItemList.FTSource = "0";
                                tmpFeeItemList.Item.IsMaterial = true;//是附材

                                tmpFeeItemList.IsUrgent = order.IsEmergency;//是否加急
                                tmpFeeItemList.Order.Sample = order.Sample;//样本信息
                                tmpFeeItemList.Memo = order.Memo;//备注
                                tmpFeeItemList.Item.MinFee = order.Item.MinFee;//最小费用
                                tmpFeeItemList.PayType = Neusoft.HISFC.Models.Base.PayTypes.Charged;//划价状态
                                tmpFeeItemList.Item.Price = undrugInfo.Price;//价格

                                tmpFeeItemList.Item.PriceUnit = undrugInfo.PriceUnit;//价格单位
                                tmpFeeItemList.FT.TotCost = undrugInfo.Price * tmpFeeItemList.Item.Qty;

                                tmpFeeItemList.Item.SysClass = undrugInfo.SysClass;//系统类别
                                tmpFeeItemList.TransType = Neusoft.HISFC.Models.Base.TransTypes.Positive;//交易类型
                                tmpFeeItemList.RecipeSequence = order.ReciptSequence;//收费序列
                                tmpFeeItemList.RecipeNO = order.ReciptNO;//处方号
                                tmpFeeItemList.SequenceNO = -1;//处方流水号
                                alSubItems.Add(tmpFeeItemList);
                            }
                        }
                    }
                }
            }
            if (alSubItems != null)
            {
                if (alSubItems.Count > 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

                    feeManagement.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

                    bool iReturn = feeManagement.SetChargeInfo(this.Patient, alSubItems, System.DateTime.Now, ref errMsg);
                    if (iReturn == false)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }
                    Neusoft.FrameWork.Management.PublicTrans.Commit();
                }
            }
        }
    }
}
 