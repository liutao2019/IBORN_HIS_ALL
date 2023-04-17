using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;
using System.Collections;
using FS.HL7Message;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry
{
    /// <summary>
    /// HIS端门诊划价收费业务处理
    /// 1、接收、作废医嘱信息（药物/治疗项目、检验、检查、手术)
    /// 2、发送门诊收费、退费确认（药物/治疗项目、检验、检查、手术)
    /// 3、确认执行信息（检验、检查）
    /// 4、接收、作废住院执行档信息
    /// 5、发送住院摆药、住院收费确认消息
    /// </summary>
    public class OrderControl : IOrder
    {
        public const char FilterStr = '#';

        #region IOrder 成员
        private string err = string.Empty;


        #endregion

        #region IOrder 成员
        /// <summary>
        /// 发送消息（费用，医嘱，发药申请）
        /// </summary>
        /// <param name="patientInfo">患者信息</param>
        /// <param name="alFeeInfo">费用或医嘱或发药申请信息</param>
        /// <param name="isPositive">正流程或负流程</param>
        /// <returns></returns>
        public int SendDrugInfo(object regInfo, ArrayList alObj, bool isPositive)
        {
            Object o = null;
            if (regInfo is FS.HISFC.Models.RADT.PatientInfo)
            {
                return new MessageSender.RGV_O15_Sender().Send(regInfo as FS.HISFC.Models.RADT.PatientInfo, ref o, ref this.err, alObj, isPositive);
            }
            if (regInfo is FS.HISFC.Models.Registration.Register)
            {
                return new MessageSender.RGV_O15_Sender().Send(regInfo as FS.HISFC.Models.Registration.Register, ref o, ref this.err, alObj, isPositive);
            }
            else
            {
                err = string.Format("未知的类型T={0}", typeof(object));
                return -1;
            }

        }

        /// <summary>
        /// 发送消息（费用，医嘱）
        /// </summary>
        /// <param name="regInfo">体检</param>
        /// <param name="alobj"></param>
        /// <param name="isPostive"></param>
        /// <returns></returns>
        public int SendFeeInfo(object regInfo, ArrayList alObj, bool isPositive)
        {

            #region 体检系统
            if (regInfo is FS.HISFC.HealthCheckup.Object.ChkRegister)
            {
                if (alObj == null || alObj.Count == 0)
                {
                    return 1;
                }

                //检查收费、退费确认消息
                ArrayList org020 = new ArrayList();
                //检验收费、退费确认消息
                ArrayList orl022 = new ArrayList();
                string[] sysClassNames = Enum.GetNames(typeof(FS.HISFC.Models.Base.EnumSysClass));

                foreach (FS.HISFC.HealthCheckup.Object.CHKFeeItem feeItemObj in alObj)
                {
                    object sysclass = feeItemObj.Item.SysClass.ID;
                    if (string.IsNullOrEmpty(feeItemObj.PackageCode) == false)
                    {
                        sysclass = feeItemObj.UndrugComb.SysClass.ID;
                    }
                    if (sysClassNames.Contains(sysclass.ToString()))
                    {
                        switch ((FS.HISFC.Models.Base.EnumSysClass)Enum.Parse(typeof(FS.HISFC.Models.Base.EnumSysClass), sysclass.ToString()))
                        {
                            //检验
                            case EnumSysClass.UL:
                                orl022.Add(feeItemObj);
                                break;

                            //检查
                            case EnumSysClass.UC:
                                org020.Add(feeItemObj);
                                break;
                        }
                    }

                }

                int ret = 1;
                Object o = null;

                if (org020.Count > 0 && ret > 0)//检查发送消息
                {
                    if (isPositive == true)
                    {
                        ret = new MessageSender.OMG_O19_Sender().Send(regInfo, ref o, ref this.err, org020, isPositive);
                        ret = new MessageSender.ORG_O20_Sender().Send(regInfo, ref o, ref this.err, org020, isPositive);
                    }
                    else
                    {
                        ret = new MessageSender.ORG_O20_Sender().Send(regInfo, ref o, ref this.err, org020, isPositive);
                        ret = new MessageSender.OMG_O19_Sender().Send(regInfo, ref o, ref this.err, org020, isPositive);
                    }
                }

                if (orl022.Count > 0 && ret > 0) //检验发送消息
                {
                    if (isPositive == true) //收费时
                    {
                        ret = new MessageSender.OML_O21_Sender().Send(regInfo, ref o, ref this.err, orl022, isPositive);
                        ret = new MessageSender.ORL_O22_Sender().Send(regInfo, ref o, ref this.err, orl022, isPositive);
                    }
                    else //退费时
                    {
                        ret = new MessageSender.ORL_O22_Sender().Send(regInfo, ref o, ref this.err, orl022, isPositive);
                        ret = new MessageSender.OML_O21_Sender().Send(regInfo, ref o, ref this.err, orl022, isPositive);
                    }
                }


                return ret;
            }
            #endregion

            #region 住院系统
            else if (regInfo is FS.HISFC.Models.RADT.PatientInfo)
            {
                Object o = null;
                //判断数据内容
                if (alObj == null || alObj.Count == 0)
                {
                    this.err = "发送数据为空！";
                    return -1;
                }
                else if (alObj[0] is FS.HISFC.Models.Fee.Inpatient.FeeItemList)
                {
                    return new MessageSender.RAS_O17_Sender().Send(regInfo, ref o, ref this.err, alObj, isPositive);
                }
                else if (alObj[0] is FS.HISFC.Models.Pharmacy.ApplyOut)
                {
                    return new MessageSender.RGV_O15_Sender().Send(regInfo, ref o, ref this.err, alObj, isPositive);
                }
                else
                {
                    return 1;
                }
            }
            #endregion

            #region 门诊系统
            else if (regInfo is FS.HISFC.Models.Registration.Register)
            {
                FS.HISFC.Models.Registration.Register register = regInfo as FS.HISFC.Models.Registration.Register;
                if (alObj == null || alObj.Count == 0)
                {
                    return 1;
                }

                //处方收费、退费确认消息
                ArrayList orp010 = new ArrayList();
                //手术收费、退费确认消息
                ArrayList orr002 = new ArrayList();
                //检查收费、退费确认消息
                ArrayList org020 = new ArrayList();
                //检验收费、退费确认消息
                ArrayList orl022 = new ArrayList();
                string[] sysClassNames = Enum.GetNames(typeof(FS.HISFC.Models.Base.EnumSysClass));
                ItemCodeMapManager mapMgr = new ItemCodeMapManager();

                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemObj in alObj)
                {
                    if ("F00000080760".Equals(feeItemObj.Item.ID) ||
                          "F00000080740".Equals(feeItemObj.Item.ID))//收取打包费用的，就将看诊状态修改为原始状态
                    {
                        //if (register.IsSee == false)
                        {
                            string sql = @"UPDATE fin_opr_register   SET see_date = null,ynsee = '0'  WHERE clinic_code='{0}'";
                            if (mapMgr.ExecNoQuery(string.Format(sql, register.ID)) <= 0)
                            {
                                //更新看诊标记失败
                                this.err = "更新看诊标记失败，原因：" + mapMgr.Err;
                                return -1;
                            }
                        }

                        //如果是全科的，那么就要更新护士站ID
                        //增加护士站排队处理
                        //找科室对应的护士站，如果没有就用科室
                        FS.HISFC.BizLogic.Manager.DepartmentStatManager departStat = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
                        ArrayList alNurse = departStat.LoadByChildren("14", register.DoctorInfo.Templet.Dept.ID);
                        if (alNurse == null)
                        {
                            this.err = "获取科室对应的护理站失败，原因：" + departStat.Err;
                            return -1;
                        }
                        string nurseID = string.Empty;
                        if (alNurse.Count == 0)
                        {
                            nurseID = register.DoctorInfo.Templet.Dept.ID;
                        }
                        else
                        {
                            nurseID = (alNurse[0] as FS.HISFC.Models.Base.DepartmentStat).PardepCode;
                        }

                        if (register.DoctorInfo.Templet.Dept.ID.Equals("3110") && register.DoctorInfo.Templet.RegLevel.ID.Equals("5"))
                        {
                            register.Memo = nurseID;
                        }

                        //更新挂号表
                        if (this.updateRegisterSeeNO(register, out this.err) < 0)
                        {
                            return -1;
                        }
                    }

                    FS.FrameWork.Models.NeuObject obj = mapMgr.GetHL7Code(EnumItemCodeMap.OutpatientOrder, new FS.FrameWork.Models.NeuObject(feeItemObj.Order.ID, feeItemObj.Order.ID, ""));
                    if (obj == null)
                    {
                        continue;
                    }
                    feeItemObj.Order.OrderType = obj.ID;
                    feeItemObj.Order.ApplyNo = obj.Name;

                    if (sysClassNames.Contains(feeItemObj.Order.OrderType))
                    {
                        switch ((FS.HISFC.Models.Base.EnumSysClass)Enum.Parse(typeof(FS.HISFC.Models.Base.EnumSysClass), feeItemObj.Order.OrderType))
                        {
                            //手术
                            case EnumSysClass.UO:
                                orr002.Add(feeItemObj);
                                break;
                            //检查
                            case EnumSysClass.UC:
                                org020.Add(feeItemObj);
                                break;
                            //检验
                            case EnumSysClass.UL:
                                orl022.Add(feeItemObj);
                                break;
                            case EnumSysClass.P:
                            case EnumSysClass.U:
                                orp010.Add(feeItemObj);
                                break;
                            case EnumSysClass.UZ:
                                orp010.Add(feeItemObj);
                                break;
                            //其他处方信息
                            default:
                                orp010.Add(feeItemObj);
                                break;
                        }
                    }

                }

                int ret = 1;
                Object o = null;
                //发送消息
                if (orr002.Count > 0 && ret > 0)
                {
                    ret = new MessageSender.ORR_O02_Sender().Send(regInfo, ref o, ref this.err, orr002, isPositive);
                }

                if (org020.Count > 0 && ret > 0)
                {
                    ret = new MessageSender.ORG_O20_Sender().Send(regInfo, ref o, ref this.err, org020, isPositive);
                }

                if (orl022.Count > 0 && ret > 0)
                {
                    ret = new MessageSender.ORL_O22_Sender().Send(regInfo, ref o, ref this.err, orl022, isPositive);
                }

                if (orp010.Count > 0 && ret > 0)
                {
                    ret = new MessageSender.ORP_O10_Sender().Send(regInfo, ref o, ref this.err, orp010, isPositive);
                }
                return ret;
            }
            #endregion

            #region 其他系统
            else
            {

                return 1;
            }
            #endregion



        }

        #endregion

        #region IOrder 成员

        public string Err
        {
            get
            {
                return err;
            }
            set
            {
                err = value;
            }
        }

        #endregion

        #region IOrder 成员

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="regInfo"></param>
        /// <param name="alObj"></param>
        /// <param name="isPositive"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int SendFeeInfo(object regInfo, ArrayList alObj, bool isPositive, string flag)
        {
            #region 体检系统
            if (regInfo is FS.HISFC.HealthCheckup.Object.ChkRegister)
            {
                if (alObj == null || alObj.Count == 0)
                {
                    return 1;
                }

                //检查收费、退费确认消息
                ArrayList org020 = new ArrayList();
                //检验收费、退费确认消息
                ArrayList orl022 = new ArrayList();
                string[] sysClassNames = Enum.GetNames(typeof(FS.HISFC.Models.Base.EnumSysClass));

                foreach (FS.HISFC.HealthCheckup.Object.CHKFeeItem feeItemObj in alObj)
                {
                    object sysclass = feeItemObj.Item.SysClass.ID;
                    if (string.IsNullOrEmpty(feeItemObj.UndrugComb.Package.ID) == false)
                    {
                        sysclass = feeItemObj.UndrugComb.SysClass.ID;
                    }
                    if (sysClassNames.Contains(sysclass.ToString()))
                    {
                        switch ((FS.HISFC.Models.Base.EnumSysClass)Enum.Parse(typeof(FS.HISFC.Models.Base.EnumSysClass), sysclass.ToString()))
                        {

                            //检查
                            case EnumSysClass.UC:
                                org020.Add(feeItemObj);
                                break;
                            //检验
                            case EnumSysClass.UL:
                                orl022.Add(feeItemObj);
                                break;
                        }
                    }

                }

                int ret = 1;
                Object o = null;
                FS.HISFC.HealthCheckup.Object.ChkRegister ChkRegister = new FS.HISFC.HealthCheckup.Object.ChkRegister();
                ChkRegister = regInfo as FS.HISFC.HealthCheckup.Object.ChkRegister;

                if (org020.Count > 0 && ret > 0)//检查发送消息
                {

                    if (ChkRegister.CHKKind == "1") //个人体检
                    {
                        if (isPositive == true)
                        {
                            if (flag == "0") // 个人体检划价时发送检查申请单
                            {
                                ret = new MessageSender.OMG_O19_Sender().Send(regInfo, ref o, ref this.err, org020, isPositive);
                            }
                            else // 个人体检收费时发送检查确认
                            {
                                ret = new MessageSender.ORG_O20_Sender().Send(regInfo, ref o, ref this.err, org020, isPositive);
                            }
                        }
                        else
                        {
                            if (flag == "0")
                            {
                                ret = new MessageSender.OMG_O19_Sender().Send(regInfo, ref o, ref this.err, org020, isPositive);
                            }
                            else
                            {
                                ret = new MessageSender.ORG_O20_Sender().Send(regInfo, ref o, ref this.err, org020, isPositive);
                            }

                        }

                    }
                    if (ChkRegister.CHKKind == "2")
                    {
                        if (isPositive == true)
                        {
                            ret = new MessageSender.OMG_O19_Sender().Send(regInfo, ref o, ref this.err, org020, isPositive);
                            ret = new MessageSender.ORG_O20_Sender().Send(regInfo, ref o, ref this.err, org020, isPositive);

                        }
                        else
                        {
                            ret = new MessageSender.ORG_O20_Sender().Send(regInfo, ref o, ref this.err, org020, isPositive);
                            ret = new MessageSender.OMG_O19_Sender().Send(regInfo, ref o, ref this.err, org020, isPositive);

                        }


                    }

                }

                if (orl022.Count > 0 && ret > 0) //检验发送消息
                {
                    if (ChkRegister.CHKKind == "1") //个人体检
                    {
                        if (isPositive == true)
                        {
                            if (flag == "0") // 个人体检划价时发送检查申请单
                            {
                                ret = new MessageSender.OML_O21_Sender().Send(regInfo, ref o, ref this.err, orl022, isPositive);
                            }
                            else // 个人体检收费时发送检查确认
                            {
                                ret = new MessageSender.ORL_O22_Sender().Send(regInfo, ref o, ref this.err, orl022, isPositive);
                            }
                        }
                        else
                        {
                            if (flag == "0")
                            {
                                ret = new MessageSender.OML_O21_Sender().Send(regInfo, ref o, ref this.err, orl022, isPositive);
                            }
                            else
                            {
                                ret = new MessageSender.ORL_O22_Sender().Send(regInfo, ref o, ref this.err, orl022, isPositive);
                            }

                        }
                    }
                    if (ChkRegister.CHKKind == "2")
                    {
                        if (isPositive == true)
                        {
                            ret = new MessageSender.OML_O21_Sender().Send(regInfo, ref o, ref this.err, orl022, isPositive);
                            ret = new MessageSender.ORL_O22_Sender().Send(regInfo, ref o, ref this.err, orl022, isPositive);

                        }
                        else
                        {
                            ret = new MessageSender.ORL_O22_Sender().Send(regInfo, ref o, ref this.err, orl022, isPositive);
                            ret = new MessageSender.OML_O21_Sender().Send(regInfo, ref o, ref this.err, orl022, isPositive);

                        }


                    }

                }


                return ret;
            }
            #endregion

            else
            {
                err = string.Format("未知的类型T={0}", typeof(object));
                return -1;
            }
        }

        #endregion

        private int updateRegisterSeeNO(FS.HISFC.Models.Registration.Register register, out string errorInfo)
        {
            string sql = "update fin_opr_register set mark1=nvl(mark1,'{1}')  where clinic_code='{0}'";
            FS.FrameWork.Management.DataBaseManger dgMgr = new FS.FrameWork.Management.DataBaseManger();
            int i = dgMgr.ExecNoQuery(sql, register.ID, register.Memo);
            errorInfo = dgMgr.Err;
            return i;
        }
    }
}
