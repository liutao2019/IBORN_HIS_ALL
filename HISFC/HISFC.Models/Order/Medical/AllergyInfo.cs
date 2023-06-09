using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Base;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Order.Medical
{
    /// <summary>
    /// [功能描述: 过敏信息综合实体]<br></br>
    /// [创 建 者: 孙盟]<br></br>
    /// [创建时间: 2007-04-26]<br></br>
    /// <修改记录/>
    /// [修 改 人: Wangw]
    /// [修改时间: 2008-3-21]
    ///	 [修改目的: Null]
    ///	 [修改描述: 添加属性]
    /// </summary> 
    [System.Serializable]
    public class AllergyInfo : Allergy
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AllergyInfo()
        {

        }

        #region  变量

        /// <summary>
        /// 住院号或者门诊病历号
        /// </summary>
        private string patientNO;
        /// <summary>
        /// 患者类型
        /// </summary>
        private ServiceTypes patientType;
        /// <summary>
        /// 有效性
        /// </summary>
        private bool validState;


        /// <summary>
        /// 发生序号
        /// </summary>
        private int happenNo; 

        /// <summary>
        /// 操作人信息
        /// </summary>
        private Base.OperEnvironment oper = new Base.OperEnvironment();
        /// <summary>
        /// 作废人信息
        /// </summary>
        private Base.OperEnvironment cancelOper = new Base.OperEnvironment();

        #endregion

        #region  属性

        /// <summary>
        /// 住院号或者门诊病历号
        /// </summary>
        public string PatientNO
        {
            get
            {
                return this.patientNO;
            }
            set
            {
                this.patientNO = value;
            }
        }

        /// <summary>
        /// 患者类型
        /// </summary>
        public ServiceTypes PatientType
        {
            get
            {
                return this.patientType;
            }
            set
            {
                this.patientType = value;
            }
        }

        /// <summary>
        /// 有效性
        /// </summary>
        public bool ValidState
        {
            get
            {
                return this.validState;
            }
            set
            {
                this.validState = value;
            }
        }

        /// <summary>
        /// 发生序号
        /// </summary>
        public int HappenNo
        {
            get
            {
                return this.happenNo;
            }
            set
            {
                this.happenNo = value;
            }
        }

        /// <summary>
        /// 操作人信息
        /// </summary>
        public OperEnvironment Oper
        {
            get
            {
                return this.oper;
            }
            set
            {
                this.oper = value;
            }
        }

        /// <summary>
        /// 作废人信息
        /// </summary>
        public OperEnvironment CancelOper
        {
            get
            {
                return this.cancelOper;
            }
            set
            {
                this.cancelOper = value;
            }
        }

        #endregion

        #region  方法

        public new AllergyInfo clone()
        {
            AllergyInfo obj = base.Clone() as AllergyInfo;
            obj.oper = this.oper.Clone();
            obj.cancelOper = this.cancelOper.Clone();
            return obj;
        }

        #endregion
    }
}
