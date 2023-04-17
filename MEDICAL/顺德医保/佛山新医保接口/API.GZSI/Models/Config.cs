using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace API.GZSI.Models
{
    [XmlRoot("Configulation")]
    public class Config
    {
        private static readonly string xmlFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GDShenZhenSI.xml");

        private static XmlDocument document = new XmlDocument();

        static Config()
        {
            document.Load(xmlFileName);
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement(ElementName = "SERVICEURL")]
        public static string ServiceUrl
        {
            get
            {
                return document.SelectSingleNode("Configulation").SelectSingleNode("SERVICEURL").InnerText.Trim();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement(ElementName = "HOSPITALCODE")]
        public static string HospitalCode
        {
            get
            {
                return document.SelectSingleNode("Configulation").SelectSingleNode("HOSPITALCODE").InnerText.Trim();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement(ElementName = "PACTCODE")]
        public static string PactCode
        {
            get
            {
                return document.SelectSingleNode("Configulation").SelectSingleNode("PACTCODE").InnerText.Trim();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement(ElementName = "PACTNAME")]
        public static string PactName
        {
            get
            {
                return document.SelectSingleNode("Configulation").SelectSingleNode("PACTNAME").InnerText.Trim();
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //[XmlElement(ElementName = "OPERATORCODE")]
        //public static string OperatorCode
        //{
        //    get
        //    {
        //        return document.SelectSingleNode("Configulation").SelectSingleNode("OPERATORCODE").InnerText.Trim();
        //    }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[XmlElement(ElementName = "OPERATORNAME")]
        //public static string OperatorName
        //{
        //    get
        //    {
        //        return document.SelectSingleNode("Configulation").SelectSingleNode("OPERATORNAME").InnerText.Trim();
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>//{136EBA18-D80B-41a9-A7D7-420966504658}
        [XmlElement(ElementName = "YZ")]
        public static string YZ
        {
            get
            {
                return document.SelectSingleNode("Configulation").SelectSingleNode("YZ").InnerText.Trim();
            }
        }

        /// <summary>
        /// 
        /// </summary>//{136EBA18-D80B-41a9-A7D7-420966504658}
        [XmlElement(ElementName = "LeaveDays")]
        public static string LeaveDays
        {
            get
            {
                return document.SelectSingleNode("Configulation").SelectSingleNode("LeaveDays").InnerText.Trim();
            }
        }

        /// <summary>
        /// 
        /// </summary>//{136EBA18-D80B-41a9-A7D7-420966504658}
        [XmlElement(ElementName = "BirthContralCard")]
        public static string BirthContralCard
        {
            get
            {
                return document.SelectSingleNode("Configulation").SelectSingleNode("BirthContralCard").InnerText.Trim();
            }
        }

        /// <summary>
        /// 
        /// </summary>//{136EBA18-D80B-41a9-A7D7-420966504658}
        [XmlElement(ElementName = "BookBuildDate")]
        public static string BookBuildDate
        {
            get
            {
                return document.SelectSingleNode("Configulation").SelectSingleNode("BookBuildDate").InnerText.Trim();
            }
        }

        /// <summary>
        /// 
        /// </summary>//{2A42810B-99C9-4b41-9078-A1132D672DB6}
        [XmlElement(ElementName = "RestrictiveWay")]
        public static string RestrictiveWay
        {
            get
            {
                return document.SelectSingleNode("Configulation").SelectSingleNode("RestrictiveWay").InnerText.Trim();
            }
        }

        /// <summary>
        /// 
        /// </summary>//{154F25C7-C066-48ab-A963-39BE4712DB42}
        [XmlElement(ElementName = "IsDrugSendMessage")]
        public static string IsDrugSendMessage
        {
            get
            {
                return document.SelectSingleNode("Configulation").SelectSingleNode("IsDrugSendMessage").InnerText.Trim();
            }
        }
    }
}
