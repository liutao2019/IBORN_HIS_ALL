using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using FS.SOC.Local.Order.OutPatientOrder.GYZL.Common.EasiLab.DataAccess.Models.HisInterface;
using FS.SOC.Local.Order.OutPatientOrder.GYZL.Common.EasiLab;
using FS.SOC.Local.Order.OutPatientOrder.GYZL.Common.EasiLab.Models.LabManagementing;
using FS.SOC.Local.Order.OutPatientOrder.GYZL.Common.EasiLab.DataAccess.Models;

namespace FS.SOC.Local.Order.OutPatientOrder.GYZL.Common
{
    public class LisGetTubeSub
    {
        FS.HISFC.Models.Registration.Register currentPatientInfo = new FS.HISFC.Models.Registration.Register();
        public FS.HISFC.Models.Registration.Register Patient
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
                return his.GetTubeGroupsForHisCode(sepTube);
            }
            catch (Exception ex)
            {
                //暂时屏蔽掉提示，没有划价成功，打印检验申请单据提示未收费。
                //MessageBox.Show("Lis计算试管失败，原因："+ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// 获取试管附材信息并插入费用表
        /// </summary>
        /// <param name="LisInfo"></param>
        public void GetHisTubeSub(TubeGroupInfo LisInfo, FS.HISFC.Models.Order.OutPatient.Order order)
        {
            ArrayList alSubItems = new ArrayList();
            FS.HISFC.Models.Fee.Outpatient.FeeItemList tmpFeeItemList = null;
            FS.HISFC.Models.Fee.Item.Undrug undrugInfo = new FS.HISFC.Models.Fee.Item.Undrug();
            FS.HISFC.BizProcess.Integrate.Fee feeManagement = new FS.HISFC.BizProcess.Integrate.Fee();

            
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
                                    tmpFeeItemList = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();
                                    undrugInfo = feeManagement.GetItem(hisItem.TestId);
                                    tmpFeeItemList.Item.ID = undrugInfo.ID;
                                    tmpFeeItemList.Item.Name = undrugInfo.Name;
                                    tmpFeeItemList.Item.Qty = 1;
                                    tmpFeeItemList.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                                    tmpFeeItemList.Patient.ID = this.Patient.ID;//门诊流水号
                                    tmpFeeItemList.Patient.PID.CardNO = this.Patient.PID.CardNO;//门诊卡号
                                    tmpFeeItemList.Order.Patient.Pact.ID = this.Patient.Pact.ID;
                                    tmpFeeItemList.Order.Patient.Pact.PayKind.ID = this.Patient.Pact.PayKind.ID;
                                    tmpFeeItemList.ChargeOper.ID = FS.FrameWork.Management.Connection.Operator.ID;
                                    tmpFeeItemList.FTSource = "0";
                                    tmpFeeItemList.Item.IsMaterial = true;//是附材

                                    tmpFeeItemList.IsUrgent = order.IsEmergency;//是否加急
                                    tmpFeeItemList.Order.Sample = order.Sample;//样本信息
                                    tmpFeeItemList.Memo = order.Memo;//备注
                                    tmpFeeItemList.Item.MinFee = order.Item.MinFee;//最小费用
                                    tmpFeeItemList.PayType = FS.HISFC.Models.Base.PayTypes.Charged;//划价状态
                                    tmpFeeItemList.Item.Price = undrugInfo.Price;//价格

                                    tmpFeeItemList.Item.PriceUnit = undrugInfo.PriceUnit;//价格单位
                                    tmpFeeItemList.FT.TotCost = undrugInfo.Price * tmpFeeItemList.Item.Qty;

                                    tmpFeeItemList.Item.SysClass = undrugInfo.SysClass;//系统类别
                                    tmpFeeItemList.TransType = FS.HISFC.Models.Base.TransTypes.Positive;//交易类型
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
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    feeManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    
                    bool iReturn = feeManagement.SetChargeInfo(this.Patient, alSubItems, System.DateTime.Now, ref errMsg);
                    if (iReturn == false)
                    {
                        //暂时屏蔽掉提示，没有划价成功，打印检验申请单据提示未收费。
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
            }
        }
    }
}
 