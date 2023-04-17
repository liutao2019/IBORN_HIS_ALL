using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.Models.Fee.Item
{
    /// <summary>
    /// 医保控费项目
    /// </summary>
    public class LimitAccountFeeItem
    {
        private string cardno;
        private string name;
        private string xmbh;
        private string xmmc;
        private DateTime createtime;
        private string cliniccode;
        private double je;
        private int qty;


        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo
        {
            get
            {
                return cardno;
            }
            set
            {
                cardno = value;
            }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }


        /// <summary>
        /// 项目编号
        /// </summary>
        public string XMBH
        {
            get
            {
                return xmbh;
            }
            set
            {
                xmbh = value;
            }
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string XMMC
        {
            get
            {
                return xmmc;
            }
            set
            {
                xmmc = value;
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get
            {
                return createtime;
            }
            set
            {
                createtime = value;
            }
        }

        /// <summary>
        /// 分诊号
        /// </summary>
        public string ClinicCode
        {
            get
            {
                return cliniccode;
            }
            set
            {
                cliniccode = value;
            }
        }

        /// <summary>
        /// 金额
        /// </summary>
        public double JE
        {
            get
            {
                return je;
            }
            set
            {
                je = value;
            }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public int QTY
        {
            get
            {
                return qty;
            }
            set
            {
                qty = value;
            }
        }

    }
}
