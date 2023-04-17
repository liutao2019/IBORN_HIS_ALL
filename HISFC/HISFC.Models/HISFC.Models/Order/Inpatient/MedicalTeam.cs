using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.Models.Order.Inpatient
{
    /// <summary>
    /// FS.HISFC.Models.Order.Inpatient.MedicalTeam<br></br>
    /// [功能描述: 医疗组实体]<br></br>
    /// [创 建 者: 牛鑫元]<br></br>
    /// [创建时间: 2010-06-29]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    [Serializable]
    public class MedicalTeam:FS.FrameWork.Models.NeuObject,Base.IValid
    {
        #region 变量
        ///// <summary>
        ///// 医疗组
        ///// </summary>
        //private FS.FrameWork.Models.NeuObject medicalTeamObject = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 科室
        /// </summary>
        private FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 操作员信息
        /// </summary>
        private Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// 是否有效
        /// </summary>
        bool isValid = true;

        #endregion

        #region
        ///// <summary>
        ///// 医疗组
        ///// </summary>
        //public FS.FrameWork.Models.NeuObject MedicalTeamObject
        //{
        //    get { return medicalTeamObject; }
        //    set { medicalTeamObject = value; }
        //}

        /// <summary>
        /// 科室
        /// </summary>
        public FS.FrameWork.Models.NeuObject Dept
        {
            get { return dept; }
            set { dept = value; }
        }
        /// <summary>
        /// 操作员信息
        /// </summary>
        public Base.OperEnvironment Oper
        {
            get { return oper; }
            set { oper = value; }
        }

        #region IValid 成员

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
            set
            {
                this.isValid = value;
            }
        }

        #endregion
        #endregion

        #region 方法

        public new MedicalTeam Clone()
        {
            MedicalTeam medicalTeam = base.Clone() as MedicalTeam;
            medicalTeam.Dept = this.dept.Clone();
            //medicalTeam.MedicalTeamObject = this.medicalTeamObject.Clone();
            medicalTeam.Oper = this.oper.Clone();
            return medicalTeam;
        }

        #endregion


       
    }
}
