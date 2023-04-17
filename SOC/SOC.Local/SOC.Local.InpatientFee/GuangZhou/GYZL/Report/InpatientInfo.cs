using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.InpatientFee.GuangZhou.GYZL.Report
{
    public class InpatientInfo
    {
        private string inPatientNo;

        /// <summary>
        /// 住院流水号
        /// </summary>
        public string InPatientNo
        {
            set { this.inPatientNo = value; }
            get { return this.inPatientNo; }
        }

        private string patientNo;

        /// <summary>
        /// 住院号
        /// </summary>
        public string PatientNo
        {
            set { this.patientNo = value; }
            get { return this.patientNo; }
        }

        private string inPatientName;

        /// <summary>
        /// 住院患者姓名
        /// </summary>
        public string InPatientName
        {
            set { this.inPatientName = value; }
            get { return this.inPatientName; }
        }

        private string bedNo;

        /// <summary>
        /// 床号
        /// </summary>
        public string BedNo
        {
            set { this.bedNo = value; }
            get { return this.bedNo; }
        }
    }
}
