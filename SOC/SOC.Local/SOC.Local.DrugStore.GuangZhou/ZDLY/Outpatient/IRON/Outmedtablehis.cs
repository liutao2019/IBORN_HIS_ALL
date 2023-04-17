using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.DrugStore.GuangZhou.ZDLY.Outpatient.IRON
{
    /// <summary>
    /// Outmedtablehis的摘要说明
    /// </summary>
    [System.Serializable]
    public class Outmedtablehis:FS.FrameWork.Models.NeuObject
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Outmedtablehis()
        {         
        }

        /// <summary>
        /// 处方号码
        /// </summary>
        private string prescriptionNO;

        public string PrescriptionNO
        {
            get { return prescriptionNO; }
            set { prescriptionNO = value; }
        }

        /// <summary>
        /// 药品在处方中的流水号
        /// </summary>
        private int medID;

        public int MedID
        {
            get { return medID; }
            set { medID = value; }
        }

        /// <summary>
        /// 药品唯一编码
        /// </summary>
        private string medOnlyCode;

        public string MedOnlyCode
        {
            get { return medOnlyCode; }
            set { medOnlyCode = value; }
        }

        /// <summary>
        /// 药品出药数量
        /// </summary>
        private int medAMT;

        public int MedAMT
        {
            get { return medAMT; }
            set { medAMT = value; }
        }

        /// <summary>
        /// 药品名称
        /// </summary>
        private string medName;

        public string MedName
        {
            get { return medName; }
            set { medName = value; }
        }

        /// <summary>
        /// 药品规格
        /// </summary>
        private string medUnit;

        public string MedUnit
        {
            get { return medUnit; }
            set { medUnit = value; }
        }

        /// <summary>
        /// 药品包装
        /// </summary>
        private string medPack;

        public string MedPack
        {
            get { return medPack; }
            set { medPack = value; }
        }

        /// <summary>
        /// 拆零系数
        /// </summary>
        private int medConvercof;

        public int MedConvercof
        {
            get { return medConvercof; }
            set { medConvercof = value; }
        }

        /// <summary>
        /// 厂商
        /// </summary>
        private string medFactory;

        public string MedFactory
        {
            get { return medFactory; }
            set { medFactory = value; }
        }

        /// <summary>
        /// 处方时间
        /// </summary>
        private DateTime medOutTime;

        public DateTime MedOutTime
        {
            get { return medOutTime; }
            set { medOutTime = value; }
        }

        /// <summary>
        /// 窗口号
        /// </summary>
        private int windowNO;

        public int WindowNO
        {
            get { return windowNO; }
            set { windowNO = value; }
        }

        /// <summary>
        /// 病人姓名
        /// </summary>
        private string patientName;

        public string PatientName
        {
            get { return patientName; }
            set { patientName = value; }
        }

        /// <summary>
        /// 病人性别
        /// </summary>
        private string patientSex;

        public string PatientSex
        {
            get { return patientSex; }
            set { patientSex = value; }
        }

        /// <summary>
        /// 病人年龄
        /// </summary>
        private int patientAge;

        public int PatientAge
        {
            get { return patientAge; }
            set { patientAge = value; }
        }

        /// <summary>
        /// 病人年龄单位
        /// </summary>
        private string patientAgeUnit;

        public string PatientAgeUnit
        {
            get { return patientAgeUnit; }
            set { patientAgeUnit = value; }
        }

        /// <summary>
        /// 发送标记 默认为0
        /// </summary>
        private char sendFlag;

        public char SendFlag
        {
            get { return sendFlag; }
            set { sendFlag = value; }
        }

        /// <summary>
        /// 出生日期
        /// </summary>
        private DateTime dateofbirth;

        public DateTime Dateofbirth
        {
            get { return dateofbirth; }
            set { dateofbirth = value; }
        }

        /// <summary>
        /// 科室代码
        /// </summary>
        private string wardNO;

        public string WardNO
        {
            get { return wardNO; }
            set { wardNO = value; }
        }

        /// <summary>
        /// 频次(1次/日)
        /// </summary>
        private string medUsage;

        public string MedUsage
        {
            get { return medUsage; }
            set { medUsage = value; }
        }

        /// <summary>
        /// 诊断
        /// </summary>
        private string diagnosis;

        public string Diagnosis
        {
            get { return diagnosis; }
            set { diagnosis = value; }
        }

        /// <summary>
        /// 用量
        /// </summary>
        private string medPerday;

        public string MedPerday
        {
            get { return medPerday; }
            set { medPerday = value; }
        }

        /// <summary>
        /// 用法
        /// </summary>
        private string medPerdos;

        public string MedPerdos
        {
            get { return medPerdos; }
            set { medPerdos = value; }
        }

        /// <summary>
        /// 医生姓名
        /// </summary>
        private string doctorName;

        public string DoctorName
        {
            get { return doctorName; }
            set { doctorName = value; }
        }

        /// <summary>
        /// 科室名称
        /// </summary>
        private string wardName;

        public string WardName
        {
            get { return wardName; }
            set { wardName = value; }
        }

        /// <summary>
        /// 病人ID
        /// </summary>
        private string patientID;

        public string PatientID
        {
            get { return patientID; }
            set { patientID = value; }
        }

        /// <summary>
        /// 发票号
        /// </summary>
        private string FPNO;

        public string FPNO1
        {
            get { return FPNO; }
            set { FPNO = value; }
        }

        /// <summary>
        /// 药品在本处方的单价格
        /// </summary>
        private decimal medUnitprice;

        public decimal MedUnitprice
        {
            get { return medUnitprice; }
            set { medUnitprice = value; }
        }
        
        /// <summary>
        /// 处方备注
        /// </summary>
        private string remark;

        public String Remark
        {
            get
            {
                return remark;
            }
            set
            {
                remark = value;
            }
        }
    }
}
