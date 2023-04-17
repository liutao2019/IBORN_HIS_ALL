using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Account
{
    /// <summary>
    /// FS.HISFC.Models.Account.AccountMedicalInsurance<br></br>
    /// [功能描述: 医保控费明细]<br></br>
    /// [创 建 者: 汪奇遇]<br></br>
    /// [创建时间: 2022-06-21]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    [Serializable]
    public class AccountMedicalInsurance : FS.FrameWork.Models.NeuObject
    {
        #region 变量
        /// <summary>
        /// 就诊卡号
        /// </summary>
        private string cardno = string.Empty;

        /// <summary>
        /// 姓名
        /// </summary>
        private string name = string.Empty;

        /// <summary>
        /// 项目编码
        /// </summary>
        private string xmbh = string.Empty;

        /// <summary>
        /// 项目名称
        /// </summary>
        private string xmmc = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        private DateTime createtime;

        /// <summary>
        /// 项目编码
        /// </summary>
        private string cliniccode = string.Empty;

        /// <summary>
        /// 金额
        /// </summary>
        private decimal je;

        /// <summary>
        /// 数量
        /// </summary>
        private decimal qty;

        /// <summary>
        /// 状态
        /// </summary>
        private string state;

        /// <summary>
        /// 审核
        /// </summary>
        private OperEnvironment operenvironment;

        #endregion

        public string Cardno
        {
            get
            {
                return this.cardno;
            }
            set
            {
                this.cardno = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public string Xmbh
        {
            get
            {
                return this.xmbh;
            }
            set
            {
                this.xmbh = value;
            }
        }

        public string Xmmc
        {
            get
            {
                return this.xmmc;
            }
            set
            {
                this.xmmc = value;
            }
        }

        public DateTime Createtime
        {
            get
            {
                return this.createtime;
            }
            set
            {
                this.createtime = value;
            }
        }

        public string Cliniccode
        {
            get
            {
                return this.cliniccode;
            }
            set
            {
                this.cliniccode = value;
            }
        }

        public decimal Je
        {
            get
            {
                return this.je;
            }
            set
            {
                this.je = value;
            }

        }

        public decimal Qty
        {
            get
            {
                return this.qty;
            }
            set
            {
                this.qty = value;
            }

        }

        public string State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }

        }

        public OperEnvironment Operenvironment
        {
            get
            {
                if (operenvironment == null)
                {
                    operenvironment = new OperEnvironment();
                }
                return this.operenvironment;
            }
            set
            {
                this.operenvironment = value;
            }
        }
    }
}
