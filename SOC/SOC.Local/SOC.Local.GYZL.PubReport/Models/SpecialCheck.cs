using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Local.GYZL.PubReport.Models
{
    /// <summary>
    /// 特殊检查
    /// </summary>
    public class SpecialCheck:FS.FrameWork.Models.NeuObject
    {
    string inpatient_no;	// '住院流水号',
	int seq;  // '流水号',
	string work_place; // '单位名称',
	string name; // '姓名',
	string mcard_no; // '医疗证号',
	string patient_no; // '住院号',
	DateTime check_date; // '检查治疗日期',
	string item_name; // '检查及治疗项目及部位',
	string diagnoze; // '临床诊断',
	string check_result; // '检查相机费检查诊断',
	decimal xiangji; // '检查相机费',
	decimal ct; // 'CT费',
	decimal gaofen; // '高分辨',
	decimal pianfei; // '片费',
	decimal zhushi; // '注射器费',
	decimal xianyin; // '显影剂费',
	decimal tot_cost; // '合计',
	decimal pay_rate; // '自付比例',
    decimal pub_cost; // '实报金额'
        string invoiceNo = "";
        DateTime static_month = new DateTime();

    public string InpatientNo
    {
        get
        {
            return inpatient_no;
        }
        set
        {
            this.inpatient_no = value;
        }
    }
        /// <summary>
    /// 流水号
        /// </summary>
    public int Seq
    {
        get{return seq;}
        set{seq = value;}
    }
        /// <summary>
    /// 单位名称
        /// </summary>

    public string WorkPlace
    {
        get{return work_place;}
        set{work_place = value;}
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
        public string McardNo
        {
            get
            {
                return mcard_no;
            }
            set
            {
                mcard_no = value;
            }
        }
        public string PatientNo
        {
            get
            {
                return patient_no;
            }
            set
            {
                patient_no = value;
            }
        }
        public DateTime CheckDate
        {
            get
            {
                return check_date;
            }
            set
            {
                check_date = value;
            }
        }
        public string ItemName
        {
            get
            {
                return item_name;
            }
            set
            {
                item_name = value;
            }
        }
        public string Diagnoze
        {
            get
            {
                return diagnoze;
            }
            set
            {
                diagnoze = value;
            }
        }
        public string CheckResult
        {
            get
            {
                return check_result;
            }
            set
            {
                check_result = value;
            }
        }
        public decimal Xiangji
        {
            get
            {
                return xiangji;
            }
            set
            {
                xiangji = value;
            }
        }
        public decimal CT
        {
            get
            {
                return ct;
            }
            set
            {
                ct = value;
            }
        }
        public decimal Gaofen
        {
            get
            {
                return gaofen;
            }
            set
            {
                gaofen = value;
            }
        }
        public decimal Pianfei
        {
            get
            {
                return pianfei;
            }
            set
            {
                pianfei = value;
            }
        }
        public decimal Zhushi
        {
            get
            {
                return zhushi;
            }
            set
            {
                zhushi = value;
            }
        }
        public decimal Xianyin
        {
            get
            {
                return xianyin;
            }
            set
            {
                xianyin = value;
            }
        }
        public decimal TotCost
        {
            get
            {
                return tot_cost;
            }
            set
            {
                tot_cost = value;
            }
        }
        public decimal PayRate
        {
            get
            {
                return pay_rate;
            }
            set
            {
                pay_rate = value;
            }
        }
        public decimal PubCost
        {
            get
            {
                return pub_cost;
            }
            set
            {
                pub_cost = value;
            }
        }

        public string InvoiceNo
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

        public DateTime StaticMonth
        {
            get
            {
                return static_month;
            }
            set
            {
                static_month = value;
            }
        }

        new public SpecialCheck Clone()
        {
            FS.SOC.Local.GYZL.PubReport.Models.SpecialCheck obj = base.Clone() as FS.SOC.Local.GYZL.PubReport.Models.SpecialCheck;
            return obj;
        }

    }
}
