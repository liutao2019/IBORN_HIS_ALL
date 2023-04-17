using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.DrugStore.ShenZhen.Models
{
    [Serializable]
    public  class DrugStoreAsign:FS.FrameWork.Models.NeuObject
    {
        public string recipeNO = string.Empty;
        /// <summary>
        /// 处方号
        /// </summary>
        public String  RecipeNO
        {
            get
            {
                return recipeNO;
            }
            set
            {
                recipeNO = value;
            }
        }

        public string patientId = string.Empty;
        /// <summary>
        /// 患者流水号
        /// </summary>
        public String PatientId
        {
            get
            {
                return patientId;
            }
            set
            {
                patientId = value;
            }
        }

        public string cardNO = string.Empty;
        /// <summary>
        /// 就诊卡号
        /// </summary>
        public String CardNO
        {
            get
            {
                return cardNO;
            }
            set
            {
                cardNO = value;
            }
        }

        public string patientName = string.Empty;
        /// <summary>
        /// 患者姓名
        /// </summary>
        public String PatientName
        {
            get
            {
                return patientName;
            }
            set 
            {
                patientName = value;
            }
        }

        public string patientSex = string.Empty;
        /// <summary>
        /// 患者性别
        /// </summary>
        public String PatientSex
        {
            get
            {
                return patientSex;
            }
            set
            {
                patientSex = value;
            }
        }

        public string deptCode = string.Empty;
        /// <summary>
        /// 科室编码
        /// </summary>
        public String DeptCode
        {
            get
            {
                return deptCode;
            }
            set
            {
                deptCode = value;
            }
        }

        public string drugDeptCode = string.Empty;
        /// <summary>
        /// 药房编码
        /// </summary>
        public String DrugDeptCode
        {
            get
            {
                return drugDeptCode;
            }
            set
            {
                drugDeptCode = value;
            }
        }

        public string sendTerminalCode = string.Empty;
        /// <summary>
        /// 发药终端代码
        /// </summary>
        public String SendTerminalCode
        {
            get
            {
                return sendTerminalCode;
            }
            set
            {
                sendTerminalCode = value;
            }
        }

        public string sendTerminalName = string.Empty;
        /// <summary>
        /// 发药终端名称
        /// </summary>
        public String SendTerminalName
        {
            get
            {
                return sendTerminalName;
            }
            set
            {
                sendTerminalName = value;
            }
        }

        public string memo = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public String Memo
        {
            get
            {
                return memo;
            }
            set
            {
                memo = value;
            }
        }

        private FS.HISFC.Models.Base.OperEnvironment oper = null;
        /// <summary>
        /// 操作员
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get
            {
                if (oper == null)
                {
                    oper = new FS.HISFC.Models.Base.OperEnvironment();
                }
                return oper;
            }
            set
            {
                oper = value;
            }
        }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new DrugStoreAsign Clone()
        {
            DrugStoreAsign c = base.Clone() as DrugStoreAsign;
            c.oper = this.Oper.Clone();
            return c;
        }
    }
}
