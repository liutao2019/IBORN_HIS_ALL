using System;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Order.OutPatient
{
	/// <summary>
	/// FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory<br></br>
	/// [功能描述: 门诊病历实体]<br></br>
	/// [创 建 者: 孙晓华]<br></br>
	/// [创建时间: 2006-09-01]<br></br>
	/// <修改记录
	///		修改人=''
	///		修改时间='yyyy-mm-dd'
	///		修改目的=''
	///		修改描述=''
	///  />
	/// </summary>
    [Serializable]
    public class ClinicCaseHistory : FS.FrameWork.Models.NeuObject
    {
        #region	变量

        #region 私有

        /// <summary>
        /// 主诉
        /// </summary>
        private string caseMain;

        /// <summary>
        /// 现病史
        /// </summary>
        private string caseNow;

        /// <summary>
        /// 既往史
        /// </summary>
        private string caseOld;

        /// <summary>
        /// 过敏史
        /// </summary>
        private string caseAllery;

        /// <summary>
        /// 查体
        /// </summary>
        private string checkBody;

        /// <summary>
        /// 诊断
        /// </summary>
        private string caseDiag;

        /// <summary>
        /// 是否感染
        /// </summary>
        private bool isInfect = false;

        /// <summary>
        /// 是否过敏
        /// </summary>
        private bool isAllery = false;

        /// <summary>
        /// 类别 1：科室 2：个人
        /// </summary>
        private string moduletype;

        /// <summary>
        /// 所属医生
        /// </summary>
        private string doctID;

        /// <summary>
        /// 所属科室
        /// </summary>
        private string deptID;


        // //  {4694CFAC-9041-496a-93C1-FAE7863E055E}
        /// <summary>
        /// 备注2，新增
        /// </summary>
        private string memo2;


        /// <summary>
        /// 辅助检查
        /// </summary>
        private string supexamination;

        /// <summary>
        /// 病历操作环境
        /// </summary>
        private OperEnvironment caseOper;

        /// <summary>
        /// 教育对象
        /// </summary>
        private string emr_educational;

        /// <summary>
        /// 教育内容
        /// </summary>
        private string  educationcontent; 

        /// <summary>
        /// 病人诊断
        /// </summary>
        private string patientdiagnose; 

        /// <summary>
        /// 用药知识
        /// </summary>
        private string medicationknowledge;

        /// <summary>
        /// 饮食知识
        /// </summary>
        private string diteknowledge;

        /// <summary>
        /// 疾病
        /// </summary>
        private string diseaseknowledge;

        /// <summary>
        /// 教育效果
        /// </summary>
        private string educationaleffect;

        /// <summary>
        /// 交通需求
        /// </summary>
        private string trafficknowledge;  

        #endregion

        #endregion

        #region	属性
        /// <summary>
        /// 主诉
        /// </summary>
        public string CaseMain
        {
            get
            {
                if (caseMain == null)
                {
                    caseMain = string.Empty;
                }
                return this.caseMain;
            }
            set
            {
                this.caseMain = value;
            }
        }

        /// <summary>
        /// 现病史
        /// </summary>
        public string CaseNow
        {
            get
            {
                if (caseNow == null)
                {
                    caseNow = string.Empty;
                }
                return this.caseNow;
            }
            set
            {
                this.caseNow = value;
            }
        }

        /// <summary>
        /// 既往史
        /// </summary>
        public string CaseOld
        {
            get
            {
                if (caseOld == null)
                {
                    caseOld = string.Empty;
                }
                return this.caseOld;
            }
            set
            {
                this.caseOld = value;
            }
        }

        /// <summary>
        /// 过敏史
        /// </summary>
        public string CaseAllery
        {
            get
            {
                if (caseAllery == null)
                {
                    caseAllery = string.Empty;
                }
                return this.caseAllery;
            }
            set
            {
                this.caseAllery = value;
            }
        }

      //  {4694CFAC-9041-496a-93C1-FAE7863E055E}
        /// <summary>
        /// 备注2,添加备注字段
        /// </summary>
        public string Memo2
        {
            get
            {
                if (memo2 == null)
                {
                    memo2 = string.Empty;
                }
                return this.memo2;
            }
            set
            {
                this.memo2 = value;
            }
        }


        /// <summary>
        /// 辅助检查
        /// </summary>
        public string SupExamination
        {
            get
            {
                if (supexamination == null)
                {
                    supexamination = string.Empty;
                }
                return this.supexamination;
            }
            set
            {
                this.supexamination = value;
            }
        
        }
       


        /// <summary>
        /// 查体
        /// </summary>
        public string CheckBody
        {
            get
            {
                if (checkBody == null)
                {
                    checkBody = string.Empty;
                }
                return this.checkBody;
            }
            set
            {
                this.checkBody = value;
            }
        }

        /// <summary>
        /// 诊断
        /// </summary>
        public string CaseDiag
        {
            get
            {
                if (caseDiag == null)
                {
                    caseDiag = string.Empty;
                }
                return this.caseDiag;
            }
            set
            {
                this.caseDiag = value;
            }
        }

        /// <summary>
        /// 是否感染
        /// </summary>
        public bool IsInfect
        {
            get
            {
                return this.isInfect;
            }
            set
            {
                this.isInfect = value;
            }
        }

        /// <summary>
        /// 是否过敏
        /// </summary>
        public bool IsAllery
        {
            get
            {
                return this.isAllery;
            }
            set
            {
                this.isAllery = value;
            }
        }

        /// <summary>
        /// 类别 1：科室 2：个人
        /// </summary>
        public string ModuleType
        {
            get
            {
                if (moduletype == null)
                {
                    moduletype = string.Empty;
                }
                return this.moduletype;
            }
            set
            {
                this.moduletype = value;
            }
        }

        /// <summary>
        /// 所属医生
        /// </summary>
        public string DoctID
        {
            get
            {
                if (doctID == null)
                {
                    doctID = string.Empty;
                }
                return this.doctID;
            }
            set
            {
                this.doctID = value;
            }
        }

        /// <summary>
        /// 所属科室
        /// </summary>
        public string DeptID
        {
            get
            {
                if (deptID == null)
                {
                    deptID = string.Empty;
                }
                return this.deptID;
            }
            set
            {
                this.deptID = value;
            }
        }

        /// <summary>
        /// 病理操作环境
        /// </summary>
        public OperEnvironment CaseOper
        {
            get
            {
                if (this.caseOper == null)
                {
                    this.caseOper = new OperEnvironment();
                }
                return this.caseOper;
            }
            set
            {
                this.caseOper = value;
            }
        }

        /// <summary>
        /// 教育对象
        /// </summary>
        public string Emr_Educational
        {
            get
            {
                if (emr_educational == null)
                {
                    emr_educational = string.Empty;
                }
                return this.emr_educational;
            }
            set
            {
                this.emr_educational = value;
            }
        }

        /// <summary>
        /// 教育内容
        /// </summary>
        public string EducationContent
        {
            get
            {
                if (educationcontent == null)
                {
                    educationcontent = string.Empty;
                }
                return this.educationcontent;
            }
            set
            {
                this.educationcontent = value;
            }
        }

        /// <summary>
        /// 病人诊断
        /// </summary>

        public string PatientDiagnose
        {
            get
            {
                if (patientdiagnose == null)
                {
                    patientdiagnose = string.Empty;
                }
                return this.patientdiagnose;
            }
            set
            {
                this.patientdiagnose = value;
            }
        }

        /// <summary>
        /// 用药知识
        /// </summary>
        public string MedicationKnowledge
        {
            get
            {
                if (medicationknowledge == null)
                {
                    medicationknowledge = string.Empty;
                }
                return this.medicationknowledge;
            }
            set
            {
                this.medicationknowledge = value;
            }
        }

        /// <summary>
        /// 饮食知识
        /// </summary>
        public string DiteKnowledge
        {
            get
            {
                if (diteknowledge == null)
                {
                    diteknowledge = string.Empty;
                }
                return this.diteknowledge;
            }
            set
            {
                this.diteknowledge = value;
            }
        }

        /// <summary>
        /// 疾病
        /// </summary>
        public string DiseaseKnowledge
        {
            get
            {
                if (diseaseknowledge == null)
                {
                    diseaseknowledge = string.Empty;
                }
                return this.diseaseknowledge;
            }
            set
            {
                this.diseaseknowledge = value;
            }
        }

        /// <summary>
        /// 教育效果
        /// </summary>
        public string EducationalEffect
        {
            get
            {
                if (educationaleffect == null)
                {
                    educationaleffect = string.Empty;
                }
                return this.educationaleffect;
            }
            set
            {
                this.educationaleffect = value;
            }
        }

        /// <summary>
        /// 交通需求
        /// </summary>
        public string TrafficKnowledge
        {
            get
            {
                if (trafficknowledge == null)
                {
                    trafficknowledge = string.Empty;
                }
                return this.trafficknowledge;
            }
            set
            {
                this.trafficknowledge = value;
            }
        }


        #endregion

        #region 方法

        #region	克隆

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new ClinicCaseHistory Clone()
        {
            ClinicCaseHistory obj = this.MemberwiseClone() as ClinicCaseHistory;
            return obj;
        }

        #endregion

        #endregion

    }
}
