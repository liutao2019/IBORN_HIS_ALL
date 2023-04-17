using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A42
{
    /// <summary>
    /// 患者多人交换床位
    /// </summary>
    public class PatientSwapBed
    {
        FS.HISFC.BizProcess.Integrate.RADT RADTIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 多人交互床位
        /// </summary>
        /// <param name="adta39"></param>
        /// <returns></returns>
        public int ProcessMessage(NHapi.Model.V24.Message.ADT_A42 adtA42, ref NHapi.Base.Model.IMessage ackMessage,ref string errInfo)
        {
            if (adtA42.PATIENTRepetitionsUsed <= 0)
            {
                errInfo = "转床失败，不存在需要转床的患者";
                return -1;
            }
            //获取患者信息
            FS.HISFC.Models.RADT.PatientInfo patientInfo = null;
            FS.HISFC.Models.Base.Bed bedInfo = null;

            //记录操作人
            FS.FrameWork.Management.Connection.Operator.ID = adtA42.EVN.GetOperatorID(0).IDNumber.Value;
            if (string.IsNullOrEmpty(FS.FrameWork.Management.Connection.Operator.ID))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "出院登记失败，原因：操作员编码为空";
                return -1;
            }
            FS.FrameWork.Management.Connection.Operator = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetEmployee(FS.FrameWork.Management.Connection.Operator.ID);
            if (FS.FrameWork.Management.Connection.Operator == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "出院登记失败，原因：传入的操作员编码，系统中找不到" + adtA42.EVN.GetOperatorID(0).IDNumber.Value;
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.radtManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.RADTIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.managerIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            for (int i = 0; i < adtA42.PATIENTRepetitionsUsed; i++)
            {
                //按照顺序进行床位的交换
                string inpatientNO = adtA42.GetPATIENT(i).PV1.VisitNumber.ID.Value;
                //床号=病区+床号
                string bedNO = adtA42.GetPATIENT(i).PV1.AssignedPatientLocation.PointOfCare.Value + adtA42.GetPATIENT(i).PV1.AssignedPatientLocation.Bed.Value;

                //找到对应的患者信息
                if (string.IsNullOrEmpty(inpatientNO))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "转床确认失败，原因：患者流水号为空";
                    return -1;
                }

                patientInfo = RADTIntegrate.QueryPatientInfoByInpatientNO(inpatientNO);
                if (patientInfo == null || string.IsNullOrEmpty(patientInfo.ID))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "转床确认获取" + inpatientNO + "患者信息失败，原因：" + this.RADTIntegrate.Err;
                    return -1;
                }

                //先根据床号找到原来
                if (string.IsNullOrEmpty(bedNO))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "转床确认失败，原因：床位编号为空";
                    return -1;
                }

                bedInfo = this.managerIntegrate.GetBed(bedNO);
                if (bedInfo == null || string.IsNullOrEmpty(bedInfo.ID))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "转床确认获取" + bedNO + "床位信息失败，原因：" + this.RADTIntegrate.Err;
                    return -1;
                }

                //是否特殊处理（包床：B  取消包床：C  ） （挂床先不考虑）（空值为正常的换床）
                string bedKind = adtA42.GetPATIENT(i).PV1.BedStatus.Value;

                if (string.IsNullOrEmpty(bedKind))
                {
                    #region 换床
                    if (string.IsNullOrEmpty(bedInfo.InpatientNO) || bedInfo.InpatientNO.Equals("N"))
                    {
                        if (bedInfo.Status.ID.ToString() == "W")
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errInfo = "转床确认失败，原因：床号为 [" + bedNO + " ]的床位状态为包床";
                            return -1;
                        }
                        else if (bedInfo.Status.ID.ToString() == "C")
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errInfo = "转床确认失败，原因：床号为 [" + bedNO + " ]的床位状态为关闭";
                            return -1;
                        }
                        else if (bedInfo.IsPrepay)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errInfo = "转床确认失败，原因：床号为 [" + bedNO + " ]的床位已经预约";
                            return -1;
                        }
                        else if (!bedInfo.IsValid)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errInfo = "转床确认失败，原因：床号为 [" + bedNO + " ]的床位已经停用";
                            return -1;
                        }
                        else if (bedInfo.Status.ID.ToString() == "I")
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errInfo = "转床确认失败，原因：床号为 [" + bedNO + " ]的床位状态为隔离";
                            return -1;
                        }
                        else if (bedInfo.Status.ID.ToString() == "K")
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errInfo = "转床确认失败，原因：床号为 [" + bedNO + " ]的床位状态为污染";
                            return -1;
                        }

                        FS.HISFC.Models.RADT.Location obj_location = new FS.HISFC.Models.RADT.Location();
                        obj_location.Bed = bedInfo;
                        int parm = this.radtManager.TransferPatient(patientInfo, obj_location);
                        if (parm == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errInfo = "转床确认失败，原因：" + this.radtManager.Err;
                            return -1;
                        }
                        else if (parm == 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errInfo = "转床确认失败，原因：患者信息有变动或者已经出院";
                            return -1;
                        }

                    }
                    //如果当前患者的主要和床位的住院号一致，则直接返回
                    else if (bedNO.Equals(bedInfo.InpatientNO))
                    {
                        continue;
                    }
                    //交换床位
                    else
                    {
                        //查找交换的对象
                        FS.HISFC.Models.RADT.PatientInfo patientB = RADTIntegrate.QueryPatientInfoByInpatientNO(bedInfo.InpatientNO);
                        if (patientB == null || string.IsNullOrEmpty(patientB.ID))
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errInfo = "转床确认获取" + bedInfo.InpatientNO + "患者信息失败，原因：" + this.RADTIntegrate.Err;
                            return -1;
                        }

                        //两者交换床位
                        if (patientInfo.PVisit.PatientLocation.Bed.Status.ID.ToString() == "W" || patientB.PVisit.PatientLocation.Bed.Status.ID.ToString() == "W")
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errInfo = "转床确认失败，原因：被调换的床位其一状态为包床，不能调换！";
                            return -1;
                        }

                        int parm = this.radtManager.SwapPatientBed(patientB, patientInfo);
                        if (parm == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errInfo = "转床确认失败，原因：" + this.radtManager.Err;
                            return -1;
                        }
                        else if (parm == 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errInfo = "转床确认失败，原因：患者信息有变动,请刷新当前窗口";
                            return -1;
                        }

                    }
                    #endregion
                }
                else if (bedKind.Equals("B"))
                {
                    #region 包床

                    if (this.radtManager.SwapPatientBed(patientInfo, bedInfo.ID, "2") == -1)
                    {
                        errInfo = "换床失败[处理包床" + bedInfo.ID + "]，原因：" + this.radtManager.Err;
                        return -1;
                    }

                    #endregion
                }
                else if (bedKind.Equals("C")) //(bedInfo.Equals("C")) MD nieaj2012-05-14
                {
                    #region 解包床

                    //解包处理
                    if (this.radtManager.UnWrapPatientBed(patientInfo, bedInfo.ID, "2") == -1)
                    {
                        errInfo = "换床失败[处理解包床" + bedInfo.ID + "]，原因：" + this.radtManager.Err;
                        return -1;
                    }

                    #endregion
                }


            }
            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }
        
    }
}
