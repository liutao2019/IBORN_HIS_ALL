using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Components.Speciment.Print
{
    public class ApplyTableForReport
    {
        #region 域变量
        public string applyID = "";
        private string applyUserName = "";
        private string deptName = "";
        private string subjectName = "";
        private string subjectID = "";
        private string resPlan = "";
        private string fundStartTime = "";
        private string fundEndTime = "";
        private string fundName = "";
        private string fundID = "";
        private string applyEmail = "";
        private string applyTel = "";
        private string applyTime = "";
        private string diseaseType = "";
        private string specAmount = "";
        private string otherDemand = "";
        private string sepcDetAmout = "";
        private string specType = "";
        private string cancerType = "";
        private string specCountInDept = "";
        private string percent = "";
        private string leftAmount = "";
        private string impName = "";
        private string impTel = "";
        private string impEmail = "";
        private string specList = "";
        
        #endregion

        #region 属性

        /// <summary>
        /// 申请单Id
        /// </summary>
        public string ApplyId
        {
            get { return applyID; }
            set { applyID = value; }
        }

        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string ApplyUserName
        {
            get
            {
                return applyUserName;
            }
            set
            {
                applyUserName = value;
            }
        }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }

        /// <summary>
        /// 课题名称
        /// </summary>
        public string SubjectName
        {
            get { return subjectName; }
            set { subjectName = value; }
        }

        /// <summary>
        /// 课题Id
        /// </summary>
        public string SubjectId
        {
            get { return subjectID; }
            set { subjectID = value; }
        }

        /// <summary>
        /// 研究计划
        /// </summary>
        public string ResPlan
        {
            get { return resPlan; }
            set { resPlan = value; }
        }

        /// <summary>
        /// 基金启动时间
        /// </summary>
        public string FundStartTime
        {
            get { return fundStartTime; }
            set { fundStartTime = value; }
        }

        /// <summary>
        /// 基金结束时间
        /// </summary>
        public string FundEndTime
        {
            get { return fundEndTime; }
            set { fundEndTime = value; }
        }

        /// <summary>
        /// 基金名称
        /// </summary>
        public string FundName
        {
            get { return fundName; }
            set { fundName = value; }
        }

        /// <summary>
        /// 基金Id
        /// </summary>
        public string FundId
        {
            get { return fundID; }
            set { fundID = value; }
        }

        /// <summary>
        /// 申请人Email
        /// </summary>
        public string ApplyEmail
        {
            get { return applyEmail; }
            set { applyEmail = value; }
        }

        /// <summary>
        /// 申请人电话
        /// </summary>
        public string ApplyTel
        {
            get { return applyTel; }
            set { applyTel = value; }
        }

        /// <summary>
        /// 申请时间
        /// </summary>
        public string ApplyTime
        {
            get { return applyTime; }
            set { applyTime = value; }
        }

        /// <summary>
        /// 申请病种类型，如有多种用分隔符分割
        /// </summary>
        public string DiseaseType
        {
            get { return diseaseType; }
            set { diseaseType = value; }
        }

        /// <summary>
        /// 申请标本数量
        /// </summary>
        public string SpecAmout
        {
            get { return specAmount; }
            set { specAmount = value; }
        }

        /// <summary>
        /// 其他要求
        /// </summary>
        public string OtherDemand
        {
            get { return otherDemand; }
            set { otherDemand = value; }
        }

        /// <summary>
        /// 申请标本种类对应的数量，如有多种，用分隔符分割，与DiseaseType 对应
        /// </summary>
        public string SpecDetAmout
        {
            get
            {
                return sepcDetAmout;
            }
            set
            {
                sepcDetAmout = value;
            }
        }

        /// <summary>
        /// 申请标本的类型，多种用分隔符分开
        /// </summary>
        public string SpecType
        {
            get { return specType; }
            set { specType = value; }
        }

        /// <summary>
        /// 标本是否是癌变，与标本类型一一对应，分隔符分割
        /// </summary>
        public string SpecIsCancer
        {
            get { return cancerType; }
            set { cancerType = value; }
        }

        /// <summary>
        /// 剩余标本数量
        /// </summary>
        public string LeftAmount
        {
            get
            {
                return leftAmount;
            }
            set
            {
                leftAmount = value;
            }
        }

        /// <summary>
        /// 占总量的比例
        /// </summary>
        public string Percent
        {
            get
            {
                return percent;
            }
            set
            {
                percent = value;
            }
        }

        /// <summary>
        /// 取用标本科室的所存标本的总量
        /// </summary>
        public string SpecCountInDpet
        {
            get
            {
                return specCountInDept;
            }
            set
            {
                specCountInDept = value;
            }
        }
        public string ImpName
        {
            get
            {
                return impName;
            }
            set
            {
                impName = value;
            }
        }

        public string ImpTel
        {
            get
            {
                return impTel;
            }
            set
            {
                impTel = value;
            }
        }

        public string ImpEmail
        {
            get
            {
                return impEmail;
            }
            set
            {
                impEmail = value;
            }
        }

        /// <summary>
        /// 标本列表
        /// </summary>
        public string SpecList
        {
            get
            {
                return specList;
            }
            set
            {
                specList = value;
            }
        }

        #endregion
    }
}
