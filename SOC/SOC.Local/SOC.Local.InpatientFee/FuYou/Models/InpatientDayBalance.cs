﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.InpatientFee.FuYou.Models
{
    public class InpatientDayBalance : FS.FrameWork.Models.NeuObject
    {
        #region 变量
        /// <summary>
        /// 日结序号
        /// </summary>
        private string blanceNo = string.Empty;
        /// <summary>
        /// 起始时间
        /// </summary>
        private DateTime beginTime = DateTime.MinValue;
        /// <summary>
        /// 终止时间
        /// </summary>
        private DateTime endTime = DateTime.MinValue;
        /// <summary>
        /// 总收入
        /// </summary>
        private decimal totCost = 0;
        /// <summary>
        /// 操作环境
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();
        /// <summary>
        /// 统计大类
        /// </summary>
        private FS.FrameWork.Models.NeuObject invoiceNo = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// 起始发票号
        /// </summary>
        private string begionInvoiceNo = string.Empty;
        /// <summary>
        /// 终止发票号
        /// </summary>
        private string endInvoiceNo = string.Empty;
        /// <summary>
        /// 作废发票号
        /// </summary>
        private string falseInvoiceNo = string.Empty;
        /// <summary>
        /// 退费发票号
        /// </summary>
        private string cancelInvoiceNo = string.Empty;
        /// <summary>
        /// 数据类型 4：项目明细 5：发票信息 6：金额信息
        /// </summary>
        private string typeStr = string.Empty;

        /// <summary>
        /// 数据显示的位置
        /// </summary>
        private string sortId = string.Empty;
        #endregion

        #region 属性
        /// <summary>
        /// 日结序号
        /// </summary>
        public string BlanceNO
        {
            get
            {
                return blanceNo;
            }
            set
            {
                blanceNo = value;
            }

        }

        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime BeginTime
        {
            get
            {
                return beginTime;
            }
            set
            {
                beginTime = value;
            }
        }

        /// <summary>
        /// 终止时间
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
            }
        }

        /// <summary>
        /// 总收入
        /// </summary>
        public decimal TotCost
        {
            get
            {
                return totCost;
            }
            set
            {
                totCost = value;
            }
        }

        /// <summary>
        /// 操作环境
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get
            {
                return oper;
            }
            set
            {
                oper = value;
            }
        }

        /// <summary>
        /// 统计大类
        /// </summary>
        public FS.FrameWork.Models.NeuObject InvoiceNO
        {
            get
            {
                return invoiceNo;
            }
            set
            {
                invoiceNo = value;
            }
        }

        /// <summary>
        /// 起始发票号
        /// </summary>
        public string BegionInvoiceNO
        {
            get
            {
                return begionInvoiceNo;
            }
            set
            {
                begionInvoiceNo = value;
            }
        }

        /// <summary>
        /// 终止发票号
        /// </summary>
        public string EndInvoiceNo
        {
            get
            {
                return endInvoiceNo;
            }
            set
            {
                endInvoiceNo = value;
            }
        }
        /// <summary>
        /// 作废发票号
        /// </summary>
        public string FalseInvoiceNo
        {
            get
            {
                return falseInvoiceNo;
            }
            set
            {
                falseInvoiceNo = value;
            }
        }

        /// <summary>
        /// 退费发票号
        /// </summary>
        public string CancelInvoiceNo
        {
            get
            {
                return cancelInvoiceNo;
            }
            set
            {
                cancelInvoiceNo = value;
            }
        }

        /// <summary>
        /// 数据类型 4：项目明细 5：发票信息 6：金额信息
        /// </summary>
        public string TypeStr
        {
            get
            {
                return typeStr;
            }
            set
            {
                typeStr = value;
            }
        }
        /// <summary>
        /// 数据显示位置
        /// </summary>
        public string SortID
        {
            get
            {
                return sortId;
            }
            set
            {
                sortId = value;
            }
        }
        #endregion

        #region 克隆
        public new InpatientDayBalance Clone()
        {
            InpatientDayBalance obj = base.Clone() as InpatientDayBalance;
            obj.Oper = this.Oper.Clone();
            obj.InvoiceNO = this.InvoiceNO.Clone();
            return obj;
        }
        #endregion
    }
}
