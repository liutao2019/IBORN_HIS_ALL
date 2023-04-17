using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoShanSporadicUpload.Model
{
    /// <summary>
    /// 待上传处方实体
    /// </summary>
    public class NeedUploadRecipe
    {
        /// <summary>
        /// 证件号码
        /// </summary>
        private string idNO;

        /// <summary>
        /// 证件号码
        /// </summary>
        public string IdNO
        {
            get { return idNO; }
            set { idNO = value; }
        }

        /// <summary>
        /// 医保编号
        /// </summary>
        private string medicalCardNO;

        /// <summary>
        /// 医保编号
        /// </summary>
        public string MedicalCardNO
        {
            get { return medicalCardNO; }
            set { medicalCardNO = value; }
        }

        /// <summary>
        /// 姓名 
        /// </summary>
        private string patientName;

        /// <summary>
        /// 姓名 
        /// </summary>
        public string PatientName
        {
            get { return patientName; }
            set { patientName = value; }
        }

        /// <summary>
        /// 门诊住院标志：1-门诊、2-住院
        /// </summary>
        private string serviceType;

        /// <summary>
        /// 门诊住院标志：1-门诊、2-住院
        /// </summary>
        public string ServiceType
        {
            get { return serviceType; }
            set { serviceType = value; }
        }

        /// <summary>
        /// 发票号
        /// </summary>
        private string invoiceNO;

        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNO
        {
            get { return invoiceNO; }
            set { invoiceNO = value; }
        }

        /// <summary>
        /// 工伤标志 1-工伤、0-医疗
        /// </summary>
        private string gsType;
        
        /// <summary>
        /// 工伤标志 1-工伤、0-医疗
        /// </summary>
        public string GSType
        {
            get { return gsType; }
            set { gsType = value; }
        }

    }
}
