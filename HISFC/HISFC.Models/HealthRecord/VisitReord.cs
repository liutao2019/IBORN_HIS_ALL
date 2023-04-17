using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.FrameWork.Models;


namespace FS.HISFC.Models.HealthRecord
{



    [System.Serializable]
    public class VisitReord : NeuObject
    {

        #region 变量

        private string serialno;
        private string usermcno;
        private string doctorid;
        private string chiefcomplaint;
        private string consultdate;
        private string pasthistory;
        private string hpi;
        private string sign;
        private string allergies;
        private string prescription;
        private string height;
        private string weight;
        private string systolic;
        private string diastolic;
        private string temperature;
        private string blood;
        private string diagnose1;
        private string diagnosecode1;
        private string diagnose2;
        private string diagnosecode2;
        private string diagnose3;
        private string diagnosecode3;
        private string diagnose4;
        private string diagnosecode4;
        private string diagnose5;
        private string diagnosecode5;
        private string diagnose6;
        private string diagnosecode6;
        private string diagnose7;
        private string diagnosecode7;
        private string diagnose8;
        private string diagnosecode8;
        private string expectedtime;

        private List<Diatemple> diaglist = new List<Diatemple>();

        #endregion

        #region 属性

        /// <summary>
        /// 挂号流水号
        /// </summary>
        public string SERIALNO
        {
            get
            {
                return this.serialno;
            }
            set
            {
                this.serialno = value;
            }
        }


        /// <summary>
        /// 本次看诊就诊卡号
        /// </summary>
        public string USERMCNO
        {
            get
            {
                return this.usermcno;
            }
            set
            {
                this.usermcno = value;
            }
        }
        /// <summary>
        /// His医生工号
        /// </summary>
        public string DOCTORID
        {
            get
            {
                return this.doctorid;
            }
            set
            {
                this.doctorid = value;
            }
        }

        /// <summary>
        /// 主诉
        /// </summary>

        public string CHIEFCOMPLAINT
        {
            get
            {
                return this.chiefcomplaint;
            }
            set
            {
                this.chiefcomplaint = value;
            }
        }
        /// <summary>
        /// yyyy-MM-dd 就诊日期
        /// </summary>
        public string CONSULTDATE
        {
            get
            {
                return this.consultdate;
            }
            set
            {
                this.consultdate = value;
            }
        }
        /// <summary>
        /// 既往史
        /// </summary>
        public string PASTHISTORY
        {
            get
            {
                return this.pasthistory;
            }
            set
            {
                this.pasthistory = value;
            }
        }
        /// <summary>
        /// 现病史
        /// </summary>
        public string HPI
        {
            get
            {
                return this.hpi;
            }
            set
            {
                this.hpi = value;
            }
        }

        /// <summary>
        /// 查体
        /// </summary>
        public string SIGN
        {
            get
            {
                return this.sign;
            }
            set
            {
                this.sign = value;
            }
        }
        /// <summary>
        /// 过敏史
        /// </summary>
        public string ALLERGIES
        {
            get
            {
                return this.allergies;
            }
            set
            {
                this.allergies = value;
            }
        }

        /// <summary>
        /// 处理意见/嘱托
        /// </summary>
        public string PRESCRIPTION
        {
            get
            {
                return this.prescription;
            }
            set
            {
                this.prescription = value;
            }
        }
        /// <summary>
        /// 身高
        /// </summary>

        public string HEIGHT
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }

        /// <summary>
        /// 体重
        /// </summary>
        public string WEIGHT
        {
            get
            {
                return this.weight;
            }
            set
            {
                this.weight = value;
            }
        }
        /// <summary>
        /// 收缩压
        /// </summary>
        public string SYSTOLIC
        {
            get
            {
                return this.systolic;
            }
            set
            {
                this.systolic = value;
            }
        }
        /// <summary>
        /// 舒张压
        /// </summary>
        public string DIASTOLIC
        {
            get
            {
                return this.diastolic;
            }
            set
            {
                this.diastolic = value;
            }
        }
        /// <summary>
        /// 体温
        /// </summary>
        public string TEMPERATURE
        {
            get
            {
                return this.temperature;
            }
            set
            {
                this.temperature = value;
            }
        }

        /// <summary>
        /// 血糖
        /// </summary>
        public string BLOOD
        {
            get
            {
                return this.blood;
            }
            set
            {
                this.blood = value;
            }
        }

        /// <summary>
        /// 诊断1
        /// </summary>
        public string DIAGNOSE1
        {
            get
            {
                return this.diagnose1;
            }
            set
            {
                this.diagnose1 = value;
            }
        }
        /// <summary>
        /// 诊断代码1
        /// </summary>

        public string DIAGNOSECODE1
        {
            get
            {
                return this.diagnosecode1;
            }
            set
            {
                this.diagnosecode1 = value;
            }
        }
        /// <summary>
        /// 诊断2
        /// </summary>

        public string DIAGNOSE2
        {
            get
            {
                return this.diagnose2;
            }
            set
            {
                this.diagnose2 = value;
            }
        }
        /// <summary>
        /// 诊断代码2
        /// </summary>
        public string DIAGNOSECODE2
        {
            get
            {
                return this.diagnosecode2;
            }
            set
            {
                this.diagnosecode2 = value;
            }
        }

        /// <summary>
        /// 诊断3
        /// </summary>
        public string DIAGNOSE3
        {
            get
            {
                return this.diagnose3;
            }
            set
            {
                this.diagnose3 = value;
            }
        }

        /// <summary>
        /// 诊断代码3
        /// </summary>
        public string DIAGNOSECODE3
        {
            get
            {
                return this.diagnosecode3;
            }
            set
            {
                this.diagnosecode3 = value;
            }
        }

        /// <summary>
        /// 诊断4
        /// </summary>
        public string DIAGNOSE4
        {
            get
            {
                return this.diagnose4;
            }
            set
            {
                this.diagnose4 = value;
            }
        }

        /// <summary>
        /// 诊断代码4
        /// </summary>
        public string DIAGNOSECODE4
        {
            get
            {
                return this.diagnosecode4;
            }
            set
            {
                this.diagnosecode4 = value;
            }
        }


        /// <summary>
        /// 诊断5
        /// </summary>
        public string DIAGNOSE5
        {
            get
            {
                return this.diagnose5;
            }
            set
            {
                this.diagnose5 = value;
            }
        }

        /// <summary>
        /// 诊断代码5
        /// </summary>
        public string DIAGNOSECODE5
        {
            get
            {
                return this.diagnosecode5;
            }
            set
            {
                this.diagnosecode5 = value;
            }
        }


        /// <summary>
        /// 诊断6
        /// </summary>
        public string DIAGNOSE6
        {
            get
            {
                return this.diagnose6;
            }
            set
            {
                this.diagnose6 = value;
            }
        }

        /// <summary>
        /// 诊断代码6
        /// </summary>
        public string DIAGNOSECODE6
        {
            get
            {
                return this.diagnosecode6;
            }
            set
            {
                this.diagnosecode6 = value;
            }
        }

        /// <summary>
        /// 诊断7
        /// </summary>
        public string DIAGNOSE7
        {
            get
            {
                return this.diagnose7;
            }
            set
            {
                this.diagnose7= value;
            }
        }

        /// <summary>
        /// 诊断代码7
        /// </summary>
        public string DIAGNOSECODE7
        {
            get
            {
                return this.diagnosecode7;
            }
            set
            {
                this.diagnosecode7 = value;
            }
        }

        /// <summary>
        /// 诊断8
        /// </summary>
        public string DIAGNOSE8
        {
            get
            {
                return this.diagnose8;
            }
            set
            {
                this.diagnose8 = value;
            }
        }

        /// <summary>
        /// 诊断代码8
        /// </summary>
        public string DIAGNOSECODE8
        {
            get
            {
                return this.diagnosecode8;
            }
            set
            {
                this.diagnosecode8 = value;
            }
        }

        //{64FDFB25-6A75-42b4-9E00-80BDEE666706}

        /// <summary>
        /// 预产期
        /// </summary>
        public string EXPECTEDTIME
        {
            get
            {
                return this.expectedtime;
            }
            set
            {
                this.expectedtime = value;
            }
        }



        public List<Diatemple> Diaglist
        {
            get
            {
                return this.diaglist;
            }
            set
            {
                this.diaglist = value;
            }
        }

        #endregion



        #region 方法

        #region 克隆

        /// <summary>
        /// 克隆{39942c2e-ec7b-4db2-a9e6-3da84d4742fb}
        /// </summary>
        /// <returns>当前对象的实例副本</returns>
        public new VisitReord Clone()
        {
            VisitReord visitreBase = base.Clone() as VisitReord;
            visitreBase.SERIALNO = this.SERIALNO;
            visitreBase.USERMCNO = this.USERMCNO;
            visitreBase.DOCTORID = this.DOCTORID;
            visitreBase.CHIEFCOMPLAINT = this.CHIEFCOMPLAINT;
            visitreBase.CONSULTDATE = this.CONSULTDATE;
            visitreBase.PASTHISTORY = this.PASTHISTORY;
            visitreBase.HPI = this.HPI;
            visitreBase.SIGN = this.SIGN;
            visitreBase.ALLERGIES = this.ALLERGIES;
            visitreBase.PRESCRIPTION = this.PRESCRIPTION;
            visitreBase.HEIGHT = this.HEIGHT;
            visitreBase.WEIGHT = this.WEIGHT;
            visitreBase.SYSTOLIC = this.SYSTOLIC;
            visitreBase.DIASTOLIC = this.DIASTOLIC;
            visitreBase.TEMPERATURE = this.TEMPERATURE;
            visitreBase.BLOOD = this.BLOOD;
            visitreBase.DIAGNOSE1 = this.DIAGNOSE1;
            visitreBase.DIAGNOSECODE1 = this.DIAGNOSECODE1;
            visitreBase.DIAGNOSE2 = this.DIAGNOSE2;
            visitreBase.DIAGNOSECODE2 = this.DIAGNOSECODE2;
            visitreBase.DIAGNOSE3 = this.DIAGNOSE3;
            visitreBase.DIAGNOSECODE3 = this.DIAGNOSECODE3;
            visitreBase.DIAGNOSE4 = this.DIAGNOSE4;
            visitreBase.DIAGNOSECODE4 = this.DIAGNOSECODE4;
            visitreBase.DIAGNOSE5 = this.DIAGNOSE5;
            visitreBase.DIAGNOSECODE5 = this.DIAGNOSECODE5;
            visitreBase.DIAGNOSE6 = this.DIAGNOSE6;
            visitreBase.DIAGNOSECODE6 = this.DIAGNOSECODE6;
            visitreBase.DIAGNOSE7 = this.DIAGNOSE7;
            visitreBase.DIAGNOSECODE7 = this.DIAGNOSECODE7;
            visitreBase.DIAGNOSE8 = this.DIAGNOSE8;
            visitreBase.DIAGNOSECODE8 = this.DIAGNOSECODE8;
            return visitreBase;
        }
        #endregion



        #endregion
    }

    [System.Serializable]
    public class Diatemple : NeuObject
    {
        private string dianame;
        private string diacode;

        public string DiagName
        {
            get
            {
                return this.dianame;
            }
            set
            {
                this.dianame = value;
            }
        }

        public string DiagCode
        {
            get
            {
                return this.diacode;
            }
            set
            {
                this.diacode = value;
            }
        }
    }

}
