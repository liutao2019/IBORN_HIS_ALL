using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Components.Speciment.Print
{
    public class ApplyTableForReport
    {
        #region �����
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

        #region ����

        /// <summary>
        /// ���뵥Id
        /// </summary>
        public string ApplyId
        {
            get { return applyID; }
            set { applyID = value; }
        }

        /// <summary>
        /// ����������
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
        /// ��������
        /// </summary>
        public string DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string SubjectName
        {
            get { return subjectName; }
            set { subjectName = value; }
        }

        /// <summary>
        /// ����Id
        /// </summary>
        public string SubjectId
        {
            get { return subjectID; }
            set { subjectID = value; }
        }

        /// <summary>
        /// �о��ƻ�
        /// </summary>
        public string ResPlan
        {
            get { return resPlan; }
            set { resPlan = value; }
        }

        /// <summary>
        /// ��������ʱ��
        /// </summary>
        public string FundStartTime
        {
            get { return fundStartTime; }
            set { fundStartTime = value; }
        }

        /// <summary>
        /// �������ʱ��
        /// </summary>
        public string FundEndTime
        {
            get { return fundEndTime; }
            set { fundEndTime = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string FundName
        {
            get { return fundName; }
            set { fundName = value; }
        }

        /// <summary>
        /// ����Id
        /// </summary>
        public string FundId
        {
            get { return fundID; }
            set { fundID = value; }
        }

        /// <summary>
        /// ������Email
        /// </summary>
        public string ApplyEmail
        {
            get { return applyEmail; }
            set { applyEmail = value; }
        }

        /// <summary>
        /// �����˵绰
        /// </summary>
        public string ApplyTel
        {
            get { return applyTel; }
            set { applyTel = value; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public string ApplyTime
        {
            get { return applyTime; }
            set { applyTime = value; }
        }

        /// <summary>
        /// ���벡�����ͣ����ж����÷ָ����ָ�
        /// </summary>
        public string DiseaseType
        {
            get { return diseaseType; }
            set { diseaseType = value; }
        }

        /// <summary>
        /// ����걾����
        /// </summary>
        public string SpecAmout
        {
            get { return specAmount; }
            set { specAmount = value; }
        }

        /// <summary>
        /// ����Ҫ��
        /// </summary>
        public string OtherDemand
        {
            get { return otherDemand; }
            set { otherDemand = value; }
        }

        /// <summary>
        /// ����걾�����Ӧ�����������ж��֣��÷ָ����ָ��DiseaseType ��Ӧ
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
        /// ����걾�����ͣ������÷ָ����ֿ�
        /// </summary>
        public string SpecType
        {
            get { return specType; }
            set { specType = value; }
        }

        /// <summary>
        /// �걾�Ƿ��ǰ��䣬��걾����һһ��Ӧ���ָ����ָ�
        /// </summary>
        public string SpecIsCancer
        {
            get { return cancerType; }
            set { cancerType = value; }
        }

        /// <summary>
        /// ʣ��걾����
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
        /// ռ�����ı���
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
        /// ȡ�ñ걾���ҵ�����걾������
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
        /// �걾�б�
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
