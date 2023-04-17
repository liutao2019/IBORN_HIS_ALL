using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Globalization;

using NHapi.Model.V231;
using NHapi.Model.V231.Segment;
using NHapi.Model.V231.Message;
using NHapi.Model.V231.Datatype;
using NHapi.Model.V231.Group;
using NHapi.Base.Parser;
using NHapi.Base.Model;

using FS.HISFC.Models;

namespace FS.HISFC.BizLogic.HL7
{
    /// <summary>
    /// [��������: HL7��Ϣ����]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-05-08]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public static class MessageFactory
    {
        private static PipeParser parser = new PipeParser();

        private static void ProduceMSH(MSH msh,string messageType,string triggerEvent, string controlId)
        {
            msh.FieldSeparator.Value = "|";
            msh.EncodingCharacters.Value = "^~\\&";
            msh.DateTimeOfMessage.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            msh.SendingApplication.NamespaceID.Value = "FS";
            msh.SendingApplication.UniversalID.Value = "HL7System";
            //msh.SequenceNumber.Value = "123";
            msh.MessageType.MessageType.Value = messageType;
            msh.MessageType.TriggerEvent.Value = triggerEvent;
            msh.MessageControlID.Value = controlId;
            msh.ProcessingID.ProcessingID.Value = "P";
            //msh.GetCharacterSet(0).Value = "8859/9";
        }
        private static void ProducePID(PID pid, FS.HISFC.Models.RADT.PatientInfo patient)
        {
            pid.SetIDPID.Value = "1";
            pid.GetPatientName(0).FamilyLastName.FamilyName.Value = patient.Name.Substring(0,1);
            pid.GetPatientName(0).GivenName.Value = patient.Name.Substring(1);
            pid.GetPatientIdentifierList(0).ID.Value = patient.ID;            
            pid.Sex.Value = patient.Sex.ID.ToString();
            pid.DateTimeOfBirth.TimeOfAnEvent.Value = patient.Birthday.ToString("yyyyMMdd",CultureInfo.InvariantCulture);
            
        }

        private static void ProducePV1(PV1 pv1, FS.HISFC.Models.RADT.PVisit patientVisit)
        {
            pv1.SetIDPV1.Value = "1";
            pv1.PatientClass.Value = patientVisit.PatientType.ID.ToString();
            pv1.PendingLocation.PointOfCare.Value = patientVisit.PatientLocation.Dept.ID;
            //pv1.PatientType.Value = patientVisit.PatientType.ID.ToString();
            pv1.AssignedPatientLocation.PointOfCare.Value = patientVisit.PatientLocation.Dept.ID;
            pv1.AssignedPatientLocation.Room.Value = patientVisit.PatientLocation.Room;
            pv1.AssignedPatientLocation.Bed.Value = patientVisit.PatientLocation.Bed.ID;
            pv1.AssignedPatientLocation.Building.Value = patientVisit.PatientLocation.Building;
            pv1.AssignedPatientLocation.Floor.Value = patientVisit.PatientLocation.Floor;
            pv1.AssignedPatientLocation.LocationDescription.Value = patientVisit.PatientLocation.Dept.Name;

            //����ҽ��
            XCN attendingDoctor = pv1.GetAttendingDoctor(0);
            attendingDoctor.IDNumber.Value = patientVisit.AttendingDoctor.ID;
            attendingDoctor.FamilyLastName.FamilyName.Value = patientVisit.AttendingDoctor.Name.Substring(0, 1);
            attendingDoctor.GivenName.Value = patientVisit.AttendingDoctor.Name.Substring(1);

            XCN referringDoctor = pv1.GetReferringDoctor(0);
            referringDoctor.IDNumber.Value = patientVisit.ReferringDoctor.ID;
            referringDoctor.FamilyLastName.FamilyName.Value = patientVisit.ReferringDoctor.Name.Substring(0, 1);
            referringDoctor.GivenName.Value = patientVisit.ReferringDoctor.Name.Substring(1);

            XCN consultingDoctor = pv1.GetConsultingDoctor(0);
            consultingDoctor.IDNumber.Value = patientVisit.ConsultingDoctor.ID;
            consultingDoctor.FamilyLastName.FamilyName.Value = patientVisit.ConsultingDoctor.Name.Substring(0, 1);
            consultingDoctor.GivenName.Value = patientVisit.ConsultingDoctor.Name.Substring(1);

            //��Ժ;��
            pv1.AdmitSource.Value = patientVisit.AdmitSource.ID;

            pv1.AdmitDateTime.TimeOfAnEvent.Value = patientVisit.InTime.ToString("yyyyMMddHHmmss",CultureInfo.InvariantCulture);


        }

        private static void ProduceORC(ORC orc, FS.HISFC.Models.Order.Order order)
        {            
            orc.OrderControl.Value = "NW";
            orc.DateTimeOfTransaction.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss",CultureInfo.InvariantCulture);

            //XCN enteredby = orc.GetEnteredBy(0);
            //enteredby.IDNumber.Value = order.Doctor.ID;
            //enteredby.FamilyLastName.FamilyName.Value = order.Doctor.Name.Substring(0, 1);
            //enteredby.GivenName.Value = order.Doctor.Name.Substring(1);

            XCN verifiedBy = orc.GetVerifiedBy(0);
            verifiedBy.IDNumber.Value = order.Nurse.ID;
            verifiedBy.FamilyLastName.FamilyName.Value = order.Nurse.Name.Substring(0, 1);
            verifiedBy.GivenName.Value = order.Nurse.Name.Substring(1);

            //����ҽʦ
            XCN op = orc.GetOrderingProvider(0);
            op.IDNumber.Value = order.Doctor.ID;
            op.FamilyLastName.FamilyName.Value = order.Doctor.Name.Substring(0, 1);
            op.GivenName.Value = order.Doctor.Name.Substring(1);

            //orc.EntererSLocation.Building.Value = "2";
            //orc.EntererSLocation.Floor.Value = "10";
            //orc.EntererSLocation.Room.Value = "23";
            //orc.EntererSLocation.Bed.Value = "234";


        }

        private static void ProduceOBR(OBR obr, FS.HISFC.Models.Order.Order order)
        {            
            obr.SetIDOBR.Value = "1";

            //ҽ������
            obr.PlacerOrderNumber.EntityIdentifier.Value = order.ID;

            //������Ŀ
            obr.UniversalServiceID.Identifier.Value = order.Item.ID;
            obr.UniversalServiceID.Text.Value = order.Item.Name;
            //obr.UniversalServiceID.NameOfCodingSystem.Value = "LN";

            //ȡ��ʱ��
            obr.RequestedDateTime.TimeOfAnEvent.SetLongDate(DateTime.Now);
            obr.ObservationDateTime.TimeOfAnEvent.SetLongDate(DateTime.Now);

            //��������
            obr.CollectionVolume.Quantity.Value = "20";
            obr.CollectionVolume.Units.Identifier.Value = "ml";
            obr.CollectionVolume.Units.Text.Value = "ml";
            obr.CollectionVolume.Units.NameOfCodingSystem.Value = "ISO-2955";

            //��������Ա
            //XCN collector = obr.GetCollectorIdentifier(0);
            //collector.GivenName.Value = "Ruby";
            //collector.IDNumber.Value = "123";

            //������������
            obr.SpecimenActionCode.Value = "G";

            //�ٴ������Ϣ
            //obr.RelevantClinicalInfo.Value = "���˻���";

            //������Դ
            obr.SpecimenSource.SpecimenSourceNameOrCode.Identifier.Value = "BLDV";
            obr.SpecimenSource.SpecimenSourceNameOrCode.Text.Value = "Blood  venous";
            obr.SpecimenSource.SpecimenSourceNameOrCode.NameOfCodingSystem.Value = "HL7";
            //������
            obr.SpecimenSource.Additives.Value = "EDTA";
            //ȡ�������岿λ
            obr.SpecimenSource.BodySite.Identifier.Value = "RA";
            obr.SpecimenSource.BodySite.Text.Value = "Right Arm";
            obr.SpecimenSource.BodySite.NameOfCodingSystem.Value = "HL7";

            //�ر�����绰����
            //XTN phoneNumber = obr.GetOrderCallbackPhoneNumber(0);
            //phoneNumber.PhoneNumber.Value = "83663012";
            //phoneNumber.CountryCode.Value = "86";
            //phoneNumber.AreaCityCode.Value = "24";

            //ʵ����
            obr.DiagnosticServSectID.Value = "LAB";
            //obr.DiagnosticServSectID.Description = "Laboratory";

        }

        private static void ProduceOBX(OBX obx, ObservationResult result,int index)
        {
            obx.SetIDOBX.Value = index.ToString();
            obx.ValueType.Value = result.ValueType.ToString();

            // LOINC
            obx.ObservationIdentifier.Identifier.Value = result.LoincId;
            obx.ObservationIdentifier.Text.Value = result.LoincName;
            obx.ObservationIdentifier.NameOfCodingSystem.Value = "LN";

            //���
            Varies value = obx.GetObservationValue(0);
            (value.Data as GenericPrimitive).Value = result.Value;

            //��λ
            obx.Units.Identifier.Value = result.Unit;
            //obx.Units.Text.Value = "Micromole / Liter";
            //obx.Units.NameOfCodingSystem.Value = "ISO+";

            //�ο�ֵ
            obx.ReferencesRange.Value = result.ReferencesRange;

            //���
            ID flag = obx.GetAbnormalFlags(0);
            flag.Value = result.AbnormalFlag.ToString();

            obx.NatureOfAbnormalTest.Value = "N";

            //���״̬
            obx.ObservationResultStatus.Value = result.ObservationResultStatus.ToString();

            //���ʱ��
            obx.DateTimeOfTheObservation.TimeOfAnEvent.Value = result.ObservationDateTime.ToString("yyyyMMddHHmmss");
        }

        /// <summary>
        /// ����ORM_O01��Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static ORM_O01 ProduceORM_O01(FS.HISFC.Models.Order.Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("order");
            }

            NHapi.Model.V231.Message.ORM_O01 orm = new ORM_O01();

            ProduceMSH(orm.MSH, "ORM","O01","1001");

            ProducePID(orm.PATIENT.PID, order.Patient);
            ProducePV1(orm.PATIENT.PATIENT_VISIT.PV1, order.Patient.PVisit);

            NHapi.Model.V231.Group.ORM_O01_ORDER order1 = orm.GetORDER();
            
            ProduceORC(order1.ORC, order);
            ProduceOBR(order1.ORDER_DETAIL.OBR, order);

            return orm;
        }

        /// <summary>
        /// ����ORU_R01��Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="order"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static ORU_R01 ProduceORU_R01(FS.HISFC.Models.Order.Order order, List<ObservationResult> results)
        {
            ORU_R01 oru = new ORU_R01();
            ProduceMSH(oru.MSH, "ORU", "R01", "2005");
            ORU_R01_PATIENT_RESULT result = oru.GetPATIENT_RESULT();
            ProducePID(result.PATIENT.PID, order.Patient);
            ProducePV1(result.PATIENT.VISIT.PV1, order.Patient.PVisit);
            
            ORU_R01_ORDER_OBSERVATION orderObs = result.GetORDER_OBSERVATION();
            ProduceOBR(orderObs.OBR, order);

            

            int i = 1;
            foreach(ObservationResult observationResult in results)
            {
                ORU_R01_OBSERVATION obs = orderObs.GetOBSERVATION(i - 1);
                ProduceOBX(obs.OBX, observationResult, i);
                i++;
            }

            return oru;
        }

        public static ORU_R01 ProduceORU_R01(FS.HISFC.Models.Order.Order order, System.Collections.ArrayList results)
        {
            ORU_R01 oru = new ORU_R01();
            ProduceMSH(oru.MSH, "ORU", "R01", "2005");
            ORU_R01_PATIENT_RESULT result = oru.GetPATIENT_RESULT();
            ProducePID(result.PATIENT.PID, order.Patient);
            ProducePV1(result.PATIENT.VISIT.PV1, order.Patient.PVisit);

            ORU_R01_ORDER_OBSERVATION orderObs = result.GetORDER_OBSERVATION();
            ProduceOBR(orderObs.OBR, order);



            int i = 1;
            foreach (ObservationResult observationResult in results)
            {
                ORU_R01_OBSERVATION obs = orderObs.GetOBSERVATION(i - 1);
                ProduceOBX(obs.OBX, observationResult, i);
                i++;
            }

            return oru;
        }

        public static ACK ProduceACK(string messageControlId)
        {
            ACK ack = new ACK();

            ProduceMSH(ack.MSH, "ACK", "", "0001");
            ack.MSA.AcknowledgementCode.Value = "AA";
            ack.MSA.MessageControlID.Value = messageControlId;
            return ack;
        }

        public static string GetACKMessage(string message)
        {
            ACK ack = MessageFactory.parser.parse(message,"2.3.1") as ACK;
            if (ack == null)
            {
                return string.Empty;
            }
            else
                return ack.MSA.AcknowledgementCode.Value;

        }


    }
}
