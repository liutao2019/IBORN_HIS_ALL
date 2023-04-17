using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FoShanYDSI
{
    class test
    {

        public string testConnect()
        {
            FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            FoShanYDSI.Objects.SIPersonInfo personInfo = new FoShanYDSI.Objects.SIPersonInfo();
            patientInfo.SSN = "";
            patientInfo.IDCard = "440523195212100027";
            FoShanYDSI.Business.InPatient.InPatientService funtion = new FoShanYDSI.Business.InPatient.InPatientService();
            string status="";
            string msg="";
            //string xml = funtion.YDInPatientAccreditation(patientInfo, ref personInfo, ref status, ref msg);

            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            //参保地统筹区编码
            XmlElement sign = xml.CreateElement("sign");
            sign.InnerText = "";
            root.AppendChild(sign);

            XmlElement transid = xml.CreateElement("transid");
            sign.InnerText = "";
            root.AppendChild(transid);

            XmlElement aab301 = xml.CreateElement("aab301");
            aab301.InnerText = "440600";
            root.AppendChild(aab301);

            XmlElement akb026 = xml.CreateElement("akb026");
            akb026.InnerText = "45607420044060411A5201";
            root.AppendChild(akb026);

            XmlElement startrow = xml.CreateElement("startrow");
            startrow.InnerText = "1";
            root.AppendChild(startrow);


            FoShanYDSI.WebReference.Service1 myWebService1 = new FoShanYDSI.WebReference.Service1();
            msg = myWebService1.CallService("0713", xml.InnerXml.ToString());
            return "";
        }
    }
}
