using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Query.MFQ_M01
{
    public class RegLevel 
    {
        private int processMessage(NHapi.Model.V24.Message.MFQ_M01 o, ref NHapi.Model.V24.Message.MFR_M01 mfrM01, ref string errInfo)
        {

            FS.HISFC.BizLogic.Registration.RegLvlFee regLevelFeeMgr = new FS.HISFC.BizLogic.Registration.RegLvlFee();
            //目前只获取现金的
            ArrayList alRegLvel = regLevelFeeMgr.Query("1", true);
            if (alRegLvel == null)
            {
                errInfo = regLevelFeeMgr.Err;
                return -1;
            }

            #region QRD

            mfrM01.QRD.QueryID.Value = o.QRD.QueryID.Value;

            #endregion

            #region MFI

            //MFI
            mfrM01.MFI.MasterFileIdentifier.Identifier.Value = "RegLevel";
            mfrM01.MFI.MasterFileIdentifier.Text.Value = "挂号级别信息";
            mfrM01.MFI.MasterFileIdentifier.NameOfCodingSystem.Value = "HIS";
            //文件层事件代码
            mfrM01.MFI.FileLevelEventCode.Value = "UPD";
            mfrM01.MFI.EnteredDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
            mfrM01.MFI.EffectiveDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
            //应答层代码
            mfrM01.MFI.ResponseLevelCode.Value = "AL";

            #endregion


            for (int i = 0; i < alRegLvel.Count; i++)
            {
                
                FS.HISFC.Models.Registration.RegLvlFee regLvlFee = alRegLvel[i] as FS.HISFC.Models.Registration.RegLvlFee;

                if (FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetRegLevel(regLvlFee.RegLevel.ID) != null)
                {
                    regLvlFee.RegLevel = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetRegLevel(regLvlFee.RegLevel.ID);
                    if (regLvlFee.RegLevel.IsExpert)
                    {
                        continue;
                    }
                }
                #region MFE

                //MFE
                NHapi.Model.V24.Group.MFR_M01_MF_QUERY MF = mfrM01.GetMF_QUERY(mfrM01.MF_QUERYRepetitionsUsed);
                NHapi.Base.Model.Varies primaryKeyValue = MF.MFE.GetPrimaryKeyValueMFE(0);
                ((NHapi.Base.Model.GenericPrimitive)primaryKeyValue.Data).Value = regLvlFee.ID;
                NHapi.Model.V24.Datatype.ID primaryKeyValueType = MF.MFE.GetPrimaryKeyValueType(0);
                primaryKeyValueType.Value = "CE";//编码元素

                #endregion

                #region Z15

                //Z段字段名称
                MF.Zxx.Name = "Z15";
                MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(10, "").Value = "A2_B1";
                MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(20, "").Value = regLvlFee.RegLevel.ID;
                MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(100, "").Value = regLvlFee.RegLevel.Name;
                MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(4, "").Value = regLvlFee.RegFee.ToString("F2");
                MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(4, "").Value = regLvlFee.OwnDigFee.ToString("F2");
                MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(4, "").Value = "0";
                MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(4, "").Value = "0";

                #endregion

            }

            return 1;
        }


        public  int ProcessMessage(NHapi.Model.V24.Message.MFQ_M01 o, ref NHapi.Model.V24.Message.MFR_M01 ackMessage, ref string errInfo)
        {
            return this.processMessage(o, ref ackMessage,ref errInfo);
        }


    }
}
