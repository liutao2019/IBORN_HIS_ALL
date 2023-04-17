using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Account
{
    [Serializable]
    public class CardVolume : NeuObject, IValidState
    {

        #region 变量

        /// <summary>
        /// 使用该卡卷的患者基本信息
        /// </summary>
        private FS.HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.Patient();
        /// <summary>
        /// 卡卷号
        /// </summary>
        private string volumeNo = string.Empty;

        /// <summary>
        /// 卡卷金额
        /// </summary>
        private decimal money = 0;

        /// <summary>
        /// 卡卷开始时间
        /// </summary>
        private DateTime begTime = new DateTime();


        /// <summary>
        /// 卡卷截止时间
        /// </summary>
        private DateTime endTime = new DateTime();

        /// <summary>
        /// 消费类型：P购买套餐；R门诊挂号；C门诊消费；I住院结算；// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        private EnumPayTypesService useType = new EnumPayTypesService();

        /// <summary>
        /// 对应消费发票号
        /// </summary>
        private string invoiceNo = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        private string mark = string.Empty;

        /// <summary>
        /// 是否有效
        /// </summary>
        private EnumValidState validState = EnumValidState.Valid;

        /// <summary>
        /// 操作信息
        /// </summary>				
        private OperEnvironment operEnvironment;

        /// <summary>
        /// 创建信息
        /// </summary>				
        private OperEnvironment createEnvironment;
   


        #endregion

        #region 属性

        /// <summary>
        /// 使用该卡卷的患者基本信息
        /// </summary>
        public HISFC.Models.RADT.Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
            }
        }

        /// <summary>
        /// 卡卷号
        /// </summary>
        public string VolumeNo
        {
            get { return volumeNo; }
            set { volumeNo = value; }
        }
        
        /// <summary>
        /// 卡卷金额
        /// </summary>
        public decimal Money
        {
            get
            {
                return this.money;
            }
            set
            {
                this.money = value;
            }
        }


        /// <summary>
        /// 卡卷开始时间
        /// </summary>
        public DateTime BegTime
        {
            get { return begTime; }
            set { begTime = value; }
        }

        /// <summary>
        /// 卡卷截止时间
        /// </summary>
        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
         /// <summary>
        /// 消费类型：P购买套餐；R门诊挂号；C门诊消费；I住院结算；// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public EnumPayTypesService UseType
        {
            get
            {
                return useType;
            }
            set
            {
                useType = value;
            }
        }

        /// <summary>
        /// 对应消费的发票号
        /// </summary>
        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Mark
        {
            get { return mark; }
            set { mark = value; }
        }

        /// <summary>
        /// 卡卷状态 0无效 1有效
        /// </summary>
        public EnumValidState ValidState
        {
            get
            {
                return validState;
            }
            set
            {
                validState = value;
            }
        }

        /// <summary>
        /// 创建信息
        /// </summary>
        public OperEnvironment CreateEnvironment
        {
            get
            {
                if (createEnvironment == null)
                {
                    createEnvironment = new OperEnvironment();
                }
                return this.createEnvironment;
            }
            set
            {
                this.createEnvironment = value;
            }
        }

        /// <summary>
        /// 操作环境
        /// </summary>
        public OperEnvironment OperEnvironment
        {
            get
            {
                if (operEnvironment == null)
                {
                    operEnvironment = new OperEnvironment();
                }
                return this.operEnvironment;
            }
            set
            {
                this.operEnvironment = value;
            }
        }

        #endregion


        #region 方法
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new CardVolume Clone()
        {
            CardVolume cardVolume = base.Clone() as CardVolume;
            cardVolume.patient = this.Patient.Clone();

            return cardVolume;
        }
        #endregion
    }
}
