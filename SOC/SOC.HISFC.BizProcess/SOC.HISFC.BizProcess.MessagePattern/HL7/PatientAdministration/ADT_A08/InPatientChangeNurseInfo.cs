using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A08
{

    public class InPatientChangeNurseInfo 
    {
        FS.HISFC.BizProcess.Integrate.RADT RADTIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();

        public  int ProcessMessage(NHapi.Model.V24.Message.ADT_A08 adtA08, ref NHapi.Base.Model.IMessage ackMessage,ref string errInfo)
        {
            FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

            if (string.IsNullOrEmpty(adtA08.PV1.VisitNumber.ID.Value))
            {
                errInfo = "修改患者信息失败，原因：患者流水号为空";
                return -1;
            }
            patientInfo = RADTIntegrate.QueryPatientInfoByInpatientNO(adtA08.PV1.VisitNumber.ID.Value);

            if (patientInfo == null)
            {
                errInfo = "修改患者信息失败，原因：找不到患者流水号";
                return -1;
            }
            //方正传入修改患者信息
            #region PID

            for (int i = 0; i < adtA08.PID.PatientAddressRepetitionsUsed; i++)
            {
                NHapi.Model.V24.Datatype.XAD homeAddress = adtA08.PID.GetPatientAddress(i);
                if (string.IsNullOrEmpty(homeAddress.AddressType.Value))
                {
                    continue;
                }

                if (homeAddress.AddressType.Value.Equals("H"))
                {
                    patientInfo.AddressHome = homeAddress.StreetAddress.StreetOrMailingAddress.Value == null ? patientInfo.AddressHome : homeAddress.StreetAddress.StreetOrMailingAddress.Value;
                    patientInfo.HomeZip = homeAddress.ZipOrPostalCode.Value == null ? patientInfo.HomeZip : homeAddress.ZipOrPostalCode.Value;
                }
                else if (homeAddress.AddressType.Value.Equals("O"))
                {
                    patientInfo.AddressBusiness = homeAddress.StreetAddress.StreetOrMailingAddress.Value == null ? patientInfo.AddressBusiness : homeAddress.StreetAddress.StreetOrMailingAddress.Value;
                    patientInfo.BusinessZip = homeAddress.ZipOrPostalCode.Value == null ? patientInfo.BusinessZip : homeAddress.ZipOrPostalCode.Value;
                }
            }
            if (adtA08.PID.PhoneNumberHomeRepetitionsUsed > 0)
            {
                NHapi.Model.V24.Datatype.XTN homePhone = adtA08.PID.GetPhoneNumberHome(0);
                patientInfo.PhoneHome = homePhone.AnyText.Value == null ? patientInfo.PhoneHome : homePhone.AnyText.Value;
            }
            if (adtA08.PID.PhoneNumberBusinessRepetitionsUsed > 0)
            {
                NHapi.Model.V24.Datatype.XTN businessPhone = adtA08.PID.GetPhoneNumberBusiness(0);
                patientInfo.PhoneBusiness = businessPhone.AnyText.Value == null ? patientInfo.PhoneBusiness : businessPhone.AnyText.Value;
            }

            if (Enum.GetNames(typeof(FS.HISFC.Models.Base.EnumMaritalStatus)).Contains(adtA08.PID.MaritalStatus.Identifier.Value))
            {
                patientInfo.MaritalStatus.ID = adtA08.PID.MaritalStatus.Identifier.Value ;
            }

            if (adtA08.PID.EthnicGroupRepetitionsUsed > 0)
            {
                NHapi.Model.V24.Datatype.CE ethnicGroup = adtA08.PID.GetEthnicGroup(0);
                if (FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetConstant(FS.HISFC.Models.Base.EnumConstant.NATION, ethnicGroup.Identifier.Value) != null)
                {
                    patientInfo.Nationality.ID = ethnicGroup.Identifier.Value;
                }
            }

            patientInfo.DIST = adtA08.PID.BirthPlace.Value == null ? patientInfo.DIST : adtA08.PID.BirthPlace.Value;
            if (FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetConstant(FS.HISFC.Models.Base.EnumConstant.COUNTRY, adtA08.PID.Nationality.Identifier.Value) != null)
            {
                patientInfo.Country.ID = adtA08.PID.Nationality.Identifier.Value ;
            }

            #endregion PID

            #region NK1

            //NK1 患者联系人信息
            if (adtA08.NK1RepetitionsUsed > 0)
            {
                NHapi.Model.V24.Segment.NK1 NK1 = adtA08.GetNK1(0);

                if (NK1.NameRepetitionsUsed > 0)
                {
                    NHapi.Model.V24.Datatype.XPN linkName = NK1.GetName(0);
                    patientInfo.Kin.Name = linkName.FamilyName.Surname.Value == null ? patientInfo.Kin.Name : linkName.FamilyName.Surname.Value;
                }

                patientInfo.Kin.Relation.ID = NK1.Relationship.Identifier.Value == null ? patientInfo.Kin.Relation.ID : NK1.Relationship.Identifier.Value;
                if (NK1.AddressRepetitionsUsed > 0)
                {
                    NHapi.Model.V24.Datatype.XAD linkAddress = NK1.GetAddress(0);
                    patientInfo.Kin.RelationAddress = linkAddress.StreetAddress.StreetOrMailingAddress.Value == null ? patientInfo.Kin.RelationAddress : linkAddress.StreetAddress.StreetOrMailingAddress.Value;
                }
                if (NK1.PhoneNumberRepetitionsUsed > 0)
                {
                    NHapi.Model.V24.Datatype.XTN linkPhone = NK1.GetPhoneNumber(0);
                    patientInfo.Kin.RelationPhone = linkPhone.AnyText.Value == null ? patientInfo.Kin.RelationPhone : linkPhone.AnyText.Value;
                }
            }

            #endregion NK1

            #region EVN
            //记录操作人
            FS.FrameWork.Management.Connection.Operator.ID = adtA08.EVN.GetOperatorID(0).IDNumber.Value;
            if (string.IsNullOrEmpty(FS.FrameWork.Management.Connection.Operator.ID))
            {
                errInfo = "患者信息失败，原因：操作员编码为空";
                return -1;
            }
            FS.FrameWork.Management.Connection.Operator = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetEmployee(FS.FrameWork.Management.Connection.Operator.ID);
            if (FS.FrameWork.Management.Connection.Operator == null)
            {
                errInfo = "患者信息失败，原因：传入的操作员编码，系统中找不到" + adtA08.EVN.GetOperatorID(0).IDNumber.Value;
                return -1;
            }
            #endregion EVN

            //选择性的更新信息
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.radtManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //更新患者基本信息
            if (radtManager.UpdatePatientInfo(patientInfo) != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "更新患者信息[基本信息表]失败，原因：" + this.radtManager.Err;
                return -1;
            }

            if (radtManager.UpdatePatient(patientInfo) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "更新患者信息[住院主表]失败，原因：" + this.radtManager.Err;
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

    }
}
