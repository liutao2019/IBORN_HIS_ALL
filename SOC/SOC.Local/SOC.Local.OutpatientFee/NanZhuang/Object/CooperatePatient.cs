using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.SOC.Local.OutpatientFee.NanZhuang.Object
{
    [System.Serializable]
    class CooperatePatient : FS.FrameWork.Models.NeuObject
    {
        public CooperatePatient()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region "变量"

        /// <summary>
        /// 序列号
        /// </summary>
        private string seqNo = "";

        /// <summary>
        /// 姓名
        /// </summary>
        private string name = "";

        /// <summary>
        /// 身份证号
        /// </summary>
        private string idenNo = "";

        /// <summary>
        /// 社保号
        /// </summary>
        private string siNo = "";

        /// <summary>
        /// 出生日期
        /// </summary>
        private DateTime birthday = DateTime.MinValue;

        /// <summary>
        /// 性别
        /// </summary>
        private string sex = "";

        /// <summary>
        /// 开始时间
        /// </summary>
        private DateTime beginDate = DateTime.MinValue;

        /// <summary>
        /// 结束时间
        /// </summary>
        private DateTime endDate = DateTime.MinValue;

        /// <summary>
        /// 状态
        /// </summary>
        private string stateFlag = "";

        /// <summary>
        /// 单位名称
        /// </summary>
        private string address = "";

        /// <summary>
        ///备注
        /// </summary>
        private string memo = "";

        /// <summary>
        /// 所属单位
        /// </summary>
        private string department = "";
        #endregion

        #region "属性"
        /// <summary>
        /// 统计序号
        /// </summary>
        public string SeqNo
        {
            get
            {
                return this.seqNo;

            }
            set
            {
                seqNo = value;
            }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;

            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdennNo
        {
            get
            {
                return this.idenNo;

            }
            set
            {
                idenNo = value;
            }
        }

        /// <summary>
        /// 社保号
        /// </summary>
        public string SiNo
        {
            get
            {
                return this.siNo;

            }
            set
            {
                siNo = value;
            }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginDate
        {
            get
            {
                return this.beginDate;
            }
            set
            {
                beginDate = value;
            }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                return this.endDate;
            }
            set
            {
                endDate = value;
            }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string StateFlag
        {
            get
            {
                return this.stateFlag;

            }
            set
            {
                stateFlag = value;
            }
        }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string Address
        {
            get
            {
                return this.address;

            }
            set
            {
                address = value;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get
            {
                return this.memo;

            }
            set
            {
                memo = value;
            }
        }

        /// <summary>
        /// 所属单位
        /// </summary>
        public string Department
        {
            get
            {
                return this.department;
            }
            set
            {
                department = value;
            }
        }


        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday
        {
            get
            {
                return this.birthday;
            }
            set
            {
                birthday = value;
            }
        }
        #endregion

        #region "方法"
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new CooperatePatient Clone()
        {
            CooperatePatient cooperatePatient = base.Clone() as CooperatePatient;
            return cooperatePatient;
        }

        #endregion
    }

}
