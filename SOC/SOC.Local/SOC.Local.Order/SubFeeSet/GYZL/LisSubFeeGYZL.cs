using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections;

namespace FS.SOC.Local.Order.SubFeeSet.GYZL
{
    class LisSubFeeGYZL
    {
        private string errMsg = "";

        public string Err
        {
            get
            {
                return errMsg;
            }
            set
            {
                errMsg = value;
            }
        }

        /// <summary>
        /// 样本类型
        /// </summary>
        FS.FrameWork.Public.ObjectHelper sampleHelper = null;

        public ArrayList MakeTubeSubInfo(FS.HISFC.Models.Registration.Register regObj, ArrayList alLisOrd)
        {
            try
            {
                if (sampleHelper == null)
                {
                    FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                    ArrayList al = interMgr.GetConstantList("LABSAMPLE");
                    sampleHelper = new FS.FrameWork.Public.ObjectHelper(al);
                }

                ArrayList alSepTube = new ArrayList();
                ArrayList alTubeSub = new ArrayList();
                EasiLab.Models.LabManagementing.SeparationTubeTestInfo lisSep = null;

                //对于复合项目自定义明细的，只需要传过去复合项目编码即可
                //暂时没有考虑开立的复合项目数量大于1的情况

                Hashtable hsUndrugCombID = new Hashtable();

                //LIS项目编码
                string LisNum = "";
                foreach (FS.HISFC.Models.Order.OutPatient.Order ordTmp in alLisOrd)
                {
                    LisNum = ordTmp.ApplyNo;

                    //如果直接过滤开立复合项目 是没有存储ApplyNo这个东东的
                    if (string.IsNullOrEmpty(LisNum))
                    {
                        LisNum = ordTmp.Item.ID;
                    }

                    //这里ApplyNo存储了复合项目编码
                    if (!hsUndrugCombID.Contains(LisNum))
                    {
                        lisSep = new EasiLab.Models.LabManagementing.SeparationTubeTestInfo();
                        lisSep.SexId = regObj.Sex.ID.ToString();
                        lisSep.HospitalId = "00001";

                        //样本传给LIS的是样本类型编码
                        //lisSep.SpecimenTypeId = ordTmp.Sample.Name;
                        lisSep.SpecimenTypeId = sampleHelper.GetID(ordTmp.Sample.Name);

                        lisSep.ReferralLabId = "";

                        //这里传输复合项目编码
                        //lisSep.TestId = ordTmp.Item.ID;
                        lisSep.TestId = LisNum;

                        if (ordTmp.IsEmergency)
                        {
                            lisSep.PriorityId = "S";//紧急
                        }
                        else
                        {
                            lisSep.PriorityId = "R";//普通
                        }
                        alSepTube.Add(lisSep);
                        hsUndrugCombID.Add(LisNum, null);
                    }
                }
                if (alSepTube != null)
                {
                    if (alSepTube.Count > 0)
                    {
                        int k = 0;
                        EasiLab.Models.LabManagementing.SeparationTubeTestInfo[] lisSepTube = new EasiLab.Models.LabManagementing.SeparationTubeTestInfo[alSepTube.Count];
                        foreach (EasiLab.Models.LabManagementing.SeparationTubeTestInfo listmp in alSepTube)
                        {
                            if (listmp != null)
                            {
                                lisSepTube[k] = listmp;
                            }
                            k++;
                        }
                        EasiLab.DataAccess.Models.HisInterface.TubeGroupInfo tubeTmp = this.InvokeWebservice(lisSepTube);
                        if (tubeTmp != null)
                        {
                            alTubeSub = this.GetHisTubeSub(tubeTmp);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                #region 采血针和采血费写死处理

                if (alTubeSub.Count > 0)
                {
                    //一次保存带出一个采血针[F00000050985 ]和一个采血费[F00000048602 ]
                    FS.HISFC.Models.Base.Item cxzItem = new FS.HISFC.Models.Base.Item();
                    cxzItem.ID = "F00000050985";
                    cxzItem.Qty = 1;


                    FS.HISFC.Models.Base.Item cxfItem = new FS.HISFC.Models.Base.Item();
                    cxfItem.ID = "F00000048602";
                    cxfItem.Qty = 1;

                    alTubeSub.Add(cxfItem);
                    alTubeSub.Add(cxzItem);
                }

                #endregion

                return alTubeSub;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }

        public ArrayList MakeTubeSubInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo, ArrayList alLisOrd)
        {
            try
            {
                if (sampleHelper == null)
                {
                    FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                    ArrayList al = interMgr.GetConstantList("LABSAMPLE");
                    sampleHelper = new FS.FrameWork.Public.ObjectHelper(al);
                }

                ArrayList alSepTube = new ArrayList();
                ArrayList alTubeSub = new ArrayList();
                EasiLab.Models.LabManagementing.SeparationTubeTestInfo lisSep = null;

                //对于复合项目自定义明细的，只需要传过去复合项目编码即可
                //暂时没有考虑开立的复合项目数量大于1的情况

                Hashtable hsUndrugCombID = new Hashtable();

                //LIS项目编码
                string LisNum = "";
                foreach (FS.HISFC.Models.Order.Inpatient.Order ordTmp in alLisOrd)
                {
                    LisNum = ordTmp.ApplyNo;

                    //如果直接过滤开立复合项目 是没有存储ApplyNo这个东东的
                    if (string.IsNullOrEmpty(LisNum))
                    {
                        LisNum = ordTmp.Item.ID;
                    }

                    //这里ApplyNo存储了复合项目编码
                    if (!hsUndrugCombID.Contains(LisNum))
                    {
                        lisSep = new EasiLab.Models.LabManagementing.SeparationTubeTestInfo();
                        lisSep.SexId = patientInfo.Sex.ID.ToString();
                        lisSep.HospitalId = "00001";

                        //样本传给LIS的是样本类型编码
                        //lisSep.SpecimenTypeId = ordTmp.Sample.Name;
                        lisSep.SpecimenTypeId = sampleHelper.GetID(ordTmp.Sample.Name);

                        lisSep.ReferralLabId = "";

                        //这里传输复合项目编码
                        //lisSep.TestId = ordTmp.Item.ID;
                        lisSep.TestId = LisNum;

                        if (ordTmp.IsEmergency)
                        {
                            lisSep.PriorityId = "S";//紧急
                        }
                        else
                        {
                            lisSep.PriorityId = "R";//普通
                        }
                        alSepTube.Add(lisSep);
                        hsUndrugCombID.Add(LisNum, null);
                    }
                }
                if (alSepTube != null)
                {
                    if (alSepTube.Count > 0)
                    {
                        int k = 0;
                        EasiLab.Models.LabManagementing.SeparationTubeTestInfo[] lisSepTube = new EasiLab.Models.LabManagementing.SeparationTubeTestInfo[alSepTube.Count];
                        foreach (EasiLab.Models.LabManagementing.SeparationTubeTestInfo listmp in alSepTube)
                        {
                            if (listmp != null)
                            {
                                lisSepTube[k] = listmp;
                            }
                            k++;
                        }
                        EasiLab.DataAccess.Models.HisInterface.TubeGroupInfo tubeTmp = this.InvokeWebservice(lisSepTube);
                        if (tubeTmp != null)
                        {
                            alTubeSub = this.GetHisTubeSub(tubeTmp);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                #region 采血针和采血费写死处理

                if (alTubeSub.Count > 0)
                {
                    //一次保存带出一个采血针[F00000050985 ]和一个采血费[F00000048602 ]
                    FS.HISFC.Models.Base.Item cxzItem = new FS.HISFC.Models.Base.Item();
                    cxzItem.ID = "F00000050985";
                    cxzItem.Qty = 1;


                    FS.HISFC.Models.Base.Item cxfItem = new FS.HISFC.Models.Base.Item();
                    cxfItem.ID = "F00000048602";
                    cxfItem.Qty = 1;

                    alTubeSub.Add(cxfItem);
                    alTubeSub.Add(cxzItem);
                }

                #endregion

                return alTubeSub;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }

        private EasiLab.DataAccess.Models.HisInterface.TubeGroupInfo InvokeWebservice(EasiLab.Models.LabManagementing.SeparationTubeTestInfo[] sepTube)
        {
            try
            {
                HisInterfaceClient hisApi = new HisInterfaceClient();

                return hisApi.GetTubeGroupsForHisCode(sepTube);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取试管附材信息并插入费用表
        /// </summary>
        /// <param name="LisInfo"></param>
        private ArrayList GetHisTubeSub(EasiLab.DataAccess.Models.HisInterface.TubeGroupInfo LisInfo)
        {
            ArrayList alSubItems = new ArrayList();
            FS.HISFC.Models.Base.Item itemObj = new FS.HISFC.Models.Base.Item();
            FS.HISFC.Models.Fee.Item.Undrug undrugInfo = new FS.HISFC.Models.Fee.Item.Undrug();
            FS.HISFC.BizProcess.Integrate.Fee feeManagement = new FS.HISFC.BizProcess.Integrate.Fee();


            if (LisInfo != null)
            {
                EasiLab.DataAccess.Models.HisInterface.ContainerInfo[] lisCt = LisInfo.Containersk__BackingField;
                if (lisCt != null)
                {
                    foreach (EasiLab.DataAccess.Models.HisInterface.ContainerInfo hisTest in lisCt)
                    {
                        if (hisTest != null)
                        {
                            itemObj = new FS.HISFC.Models.Base.Item();
                            itemObj.ID = hisTest.ContainerId;
                            itemObj.Qty = 1;
                            alSubItems.Add(itemObj);
                            //EasiLab.DataAccess.Models.TestInfo[] hisItems = hisTest.TestItems;
                            //foreach (EasiLab.DataAccess.Models.TestInfo hisItem in hisItems)
                            //{
                            //    itemObj = new FS.HISFC.Models.Base.Item();
                            //    itemObj.ID = hisItem.TestId;
                            //    itemObj.Qty = 1;
                            //    alSubItems.Add(itemObj);
                            //}
                        }
                    }
                }
            }
            return alSubItems;
        }
    }
}
